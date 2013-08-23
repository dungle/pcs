using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Reflection;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSComUtils.Common;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace SaleByLicensorBreakDown
{
	public class SaleByLicensorBreakDown : MarshalByRefObject, IDynamicReport
	{
		#region IDynamicReport Members

		private string mConnectionString;

		/// <summary>
		/// ConnectionString, provide for the Dynamic Report
		/// ALlow Dynamic Report to access the DataBase of PCS
		/// </summary>
		public string PCSConnectionString
		{
			get { return mConnectionString; }
			set { mConnectionString = value; }
		}

		private ReportBuilder mReportBuilder;

		/// <summary>
		/// Report Builder Utility Object
		/// Dynamic Report can use this object to render, modify, layout the report
		/// </summary>
		public ReportBuilder PCSReportBuilder
		{
			get { return mReportBuilder; }
			set { mReportBuilder = value; }
		}

		private C1PrintPreviewControl mViewer;

		/// <summary>
		/// ReportViewer Object, provide for the DynamicReport, 
		/// allow Dynamic Report to manipulate with the REportViewer, 
		/// modify the report after rendered if needed
		/// </summary>
		public C1PrintPreviewControl PCSReportViewer
		{
			get { return mViewer; }
			set { mViewer = value; }
		}

		private object mResult;

		/// <summary>
		/// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
		/// </summary>
		public object Result
		{
			get { return mResult; }
			set { mResult = value; }
		}

		private bool mUseEngine;

		/// <summary>
		/// Notify PCS whether the rendering report process is run by 
		/// this IDynamicReport
		/// or the ReportViewer Engine (in the ReportViewer form)
		/// </summary>
		public bool UseReportViewerRenderEngine
		{
			get { return mUseEngine; }
			set { mUseEngine = value; }
		}

		private string mReportFolder;

		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>		
		public string ReportDefinitionFolder
		{
			get { return mReportFolder; }
			set { mReportFolder = value; }
		}

		private string mLayoutFile;

		/// <summary>
		/// Inform External Process about the Layout file
		/// in which PCS instruct to use
		/// (PCS will assign this property while ReportViewer Form execute,
		/// ReportVIewer form will use the layout file in the report config entry to put in this property)
		/// </summary>		
		public string ReportLayoutFile
		{
			get { return mLayoutFile; }
			set { mLayoutFile = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pstrMethod">name of the method to call (which declare in the DynamicReport C# file)</param>
		/// <param name="pobjParameters">Array of parameters provide to call the Method with method name = pstrMethod</param>
		/// <returns></returns>
		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		#endregion

		public DataTable ExecuteReport(string pstrCCNID, string pstrFromDate, string pstrToDate, string pstrLicensorID, string pstrMakeItem)
		{
			int intMakeItem = -1;
			if (pstrMakeItem != null && pstrMakeItem != string.Empty)
				intMakeItem = Convert.ToInt32(Convert.ToBoolean(pstrMakeItem));
			DateTime dtmFromDate = Convert.ToDateTime(pstrFromDate);
			DateTime dtmToDate = Convert.ToDateTime(pstrToDate);
			
			#region report table

			DataTable dtbData = new DataTable();
			dtbData.Columns.Add(new DataColumn("Licensor", typeof (string)));
			dtbData.Columns.Add(new DataColumn("Model", typeof (string)));
			dtbData.Columns.Add(new DataColumn("PartNo", typeof (string)));
			dtbData.Columns.Add(new DataColumn("PartName", typeof (string)));
			dtbData.Columns.Add(new DataColumn("Quantity", typeof (decimal)));
			dtbData.Columns.Add(new DataColumn("Amount", typeof (decimal)));
			dtbData.Columns.Add(new DataColumn(CostElementType.Material.ToString(), typeof (decimal)));
			dtbData.Columns.Add(new DataColumn(CostElementType.SubMaterial.ToString(), typeof (decimal)));
			dtbData.Columns.Add(new DataColumn(CostElementType.Labor.ToString(), typeof (decimal)));
			dtbData.Columns.Add(new DataColumn(CostElementType.Machine.ToString(), typeof (decimal)));
			dtbData.Columns.Add(new DataColumn(CostElementType.OverHead.ToString(), typeof (decimal)));
			dtbData.Columns.Add(new DataColumn("CostOfGoodsSold", typeof (decimal)));

			#endregion
			
			#region build report data
			
			DataTable dtbReportData = GetReportData(pstrCCNID, dtmFromDate, dtmToDate, pstrLicensorID, intMakeItem);
			DataTable dtbActualCost = GetActualCost(pstrCCNID);
			DataTable dtbStdCost = GetStdCost();
			DataTable dtbChargeAllocation = GetChargeAllocation(pstrCCNID);
			string strLastProductID = string.Empty;
			string strLastPartyID = string.Empty;
			foreach (DataRow drowData in dtbReportData.Rows)
			{
				// party id
				string strPartyID = drowData["PartyID"].ToString();
				// product id
				string strProductID = drowData["ProductID"].ToString();
				if (strLastProductID == strProductID && strLastPartyID == strPartyID)
					continue;
				strLastProductID = strProductID;
				strLastPartyID = strPartyID;
				DataRow drowReport = dtbData.NewRow();
				drowReport["Licensor"] = drowData["Licensor"];
				drowReport["Model"] = drowData["Model"];
				drowReport["PartNo"] = drowData["PartNo"];
				drowReport["PartName"] = drowData["PartName"];
				string strFilter = "PartyID = '" + strPartyID + "' AND ProductID = '" + strProductID + "'";
				decimal decQuantity = 0, decAmount = 0, decDSAmount = 0;
				decimal decRecycleAmount = 0, decAdjustAmount = 0;
				decimal decOHDSAmount = 0, decOHRecAmount = 0, decOHAdjAmount = 0;
				decimal decSumQuantity = 0, decCharge = 0, decRate = 0;
				decimal decRawCost = 0, decSubCost = 0, decLaborCost = 0, decMachineCost = 0, decOverCost = 0;
				
				try
				{
					decQuantity = Convert.ToDecimal(dtbReportData.Compute("SUM(Quantity)", strFilter));
				}
				catch{}
				try
				{
					decSumQuantity = Convert.ToDecimal(dtbReportData.Compute("SUM(Quantity)", "ProductID = " + strProductID));
				}
				catch{}
				decRate = decQuantity / decSumQuantity;
				try
				{
					decAmount = Convert.ToDecimal(dtbReportData.Compute("SUM(Amount)", strFilter));
				}
				catch{}
				
				drowReport["Quantity"] = decQuantity;
				drowReport["Amount"] = decAmount;

				#region calculate cost of goods sold for each item

				// shipped date to determine which cost period will take effect here
				DateTime dtmShippedDate = (DateTime) drowData["ShippedDate"];
				dtmShippedDate = new DateTime(dtmShippedDate.Year, dtmShippedDate.Month, dtmShippedDate.Day);
				// filter condition
				string strFilterCondition = "ProductID = '" + strProductID + "'"
					+ " AND FromDate <= '" + dtmShippedDate.ToString("G") + "'"
					+ " AND ToDate >= '" + dtmShippedDate.ToString("G") + "'";
				
				#region Cost Of Goods Sold - Raw Material
				
				#region DS Amount
				try
				{
					decDSAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(DSAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Material).ToString() + "'"));
				}
				catch
				{
					decDSAmount = 0;
				}
				#endregion

				#region Recycle Amount
				try
				{
					decRecycleAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(RecycleAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Material).ToString() + "'"));
				}
				catch
				{
					decRecycleAmount = 0;
				}
				#endregion

				#region Adjust Amount
				try
				{
					decAdjustAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(AdjustAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Material).ToString() + "'"));
				}
				catch
				{
					decAdjustAmount = 0;
				}
				#endregion

				#region OH DS Amount
				try
				{
					decOHDSAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_DSAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Material).ToString() + "'"));
				}
				catch
				{
					decOHDSAmount = 0;
				}
				#endregion

				#region OH Recycle Amount
				try
				{
					decOHRecAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_RecycleAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Material).ToString() + "'"));
				}
				catch
				{
					decOHRecAmount = 0;
				}
				#endregion

				#region OH Adjust Amount
				try
				{
					decOHAdjAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_AdjustAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Material).ToString() + "'"));
				}
				catch
				{
					decOHAdjAmount = 0;
				}
				#endregion

				decCharge = (decDSAmount - decRecycleAmount - decAdjustAmount)
					+ (decOHDSAmount - decOHRecAmount - decOHAdjAmount);
				DataRow[] drowActualCost = dtbActualCost.Select(strFilterCondition);
				if (drowActualCost.Length > 0)
				{
					try
					{
						string strFilterDetail = strFilterCondition + " AND CostElementType = '" +
							((int) CostElementType.Material).ToString() + "'";
						decimal decActualCost = Convert.ToDecimal(dtbActualCost.Compute("SUM(ActualCost)", strFilterDetail));
						decRawCost = decQuantity * decActualCost + decCharge * decRate;
						drowReport[CostElementType.Material.ToString()] = decRawCost;
					}
					catch{}
				}
				else
				{
					// if item not yet rollup actual cost, try to get standard cost
					string strFilterDetail = "ProductID = '" + strProductID + "' AND CostElementType = '" +
						((int) CostElementType.Material).ToString() + "'";
					DataRow[] drowStdCost = dtbStdCost.Select(strFilterDetail);
					if (drowStdCost.Length > 0)
					{
						try
						{
							decimal decStdCost = Convert.ToDecimal(dtbStdCost.Compute("SUM(Cost)", strFilterDetail));
							decRawCost = decQuantity * decStdCost + decCharge * decRate;
							drowReport[CostElementType.Material.ToString()] = decRawCost;
						}
						catch{}
					}
					else // if item dot have any cost
						drowReport[CostElementType.Material.ToString()] = decimal.Zero;
				}
				
				#endregion
				
				#region Cost Of Goods Sold - Sub Material
				
				#region DS Amount
				try
				{
					decDSAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(DSAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.SubMaterial).ToString() + "'"));
				}
				catch
				{
					decDSAmount = 0;
				}
				#endregion

				#region Recycle Amount
				try
				{
					decRecycleAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(RecycleAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.SubMaterial).ToString() + "'"));
				}
				catch
				{
					decRecycleAmount = 0;
				}
				#endregion

				#region Adjust Amount
				try
				{
					decAdjustAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(AdjustAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.SubMaterial).ToString() + "'"));
				}
				catch
				{
					decAdjustAmount = 0;
				}
				#endregion

				#region OH DS Amount
				try
				{
					decOHDSAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_DSAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.SubMaterial).ToString() + "'"));
				}
				catch
				{
					decOHDSAmount = 0;
				}
				#endregion

				#region OH Recycle Amount
				try
				{
					decOHRecAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_RecycleAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.SubMaterial).ToString() + "'"));
				}
				catch
				{
					decOHRecAmount = 0;
				}
				#endregion

				#region OH Adjust Amount
				try
				{
					decOHAdjAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_AdjustAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.SubMaterial).ToString() + "'"));
				}
				catch
				{
					decOHAdjAmount = 0;
				}
				#endregion

				decCharge = (decDSAmount - decRecycleAmount - decAdjustAmount)
					+ (decOHDSAmount - decOHRecAmount - decOHAdjAmount);
				if (drowActualCost.Length > 0)
				{
					try
					{
						string strFilterDetail = strFilterCondition + " AND CostElementType = '" +
							((int) CostElementType.SubMaterial).ToString() + "'";
						decimal decActualCost = Convert.ToDecimal(dtbActualCost.Compute("SUM(ActualCost)", strFilterDetail));
						decSubCost = decQuantity * decActualCost + decCharge * decRate;
						drowReport[CostElementType.SubMaterial.ToString()] = decSubCost;
					}
					catch{}
				}
				else
				{
					// if item not yet rollup actual cost, try to get standard cost
					string strFilterDetail = "ProductID = '" + strProductID + "' AND CostElementType = '" +
						((int) CostElementType.SubMaterial).ToString() + "'";
					DataRow[] drowStdCost = dtbStdCost.Select(strFilterDetail);
					if (drowStdCost.Length > 0)
					{
						try
						{
							decimal decStdCost = Convert.ToDecimal(dtbStdCost.Compute("SUM(Cost)", strFilterDetail));
							decSubCost = decQuantity * decStdCost + decCharge * decRate;
							drowReport[CostElementType.SubMaterial.ToString()] = decSubCost;
						}
						catch{}
					}
					else // if item dot have any cost
						drowReport[CostElementType.SubMaterial.ToString()] = decimal.Zero;
				}
				
				#endregion
				
				#region Cost Of Goods Sold - Labor
				
				#region DS Amount
				try
				{
					decDSAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(DSAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Labor).ToString() + "'"));
				}
				catch
				{
					decDSAmount = 0;
				}
				#endregion

				#region Recycle Amount
				try
				{
					decRecycleAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(RecycleAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Labor).ToString() + "'"));
				}
				catch
				{
					decRecycleAmount = 0;
				}
				#endregion

				#region Adjust Amount
				try
				{
					decAdjustAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(AdjustAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Labor).ToString() + "'"));
				}
				catch
				{
					decAdjustAmount = 0;
				}
				#endregion

				#region OH DS Amount
				try
				{
					decOHDSAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_DSAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Labor).ToString() + "'"));
				}
				catch
				{
					decOHDSAmount = 0;
				}
				#endregion

				#region OH Recycle Amount
				try
				{
					decOHRecAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_RecycleAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Labor).ToString() + "'"));
				}
				catch
				{
					decOHRecAmount = 0;
				}
				#endregion

				#region OH Adjust Amount
				try
				{
					decOHAdjAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_AdjustAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Labor).ToString() + "'"));
				}
				catch
				{
					decOHAdjAmount = 0;
				}
				#endregion

				decCharge = (decDSAmount - decRecycleAmount - decAdjustAmount)
					+ (decOHDSAmount - decOHRecAmount - decOHAdjAmount);
				if (drowActualCost.Length > 0)
				{
					try
					{
						string strFilterDetail = strFilterCondition + " AND CostElementType = '" +
							((int) CostElementType.Labor).ToString() + "'";
						decimal decActualCost = Convert.ToDecimal(dtbActualCost.Compute("SUM(ActualCost)", strFilterDetail));
						decLaborCost = decQuantity * decActualCost + decCharge * decRate;
						drowReport[CostElementType.Labor.ToString()] = decLaborCost;
					}
					catch{}
				}
				else
				{
					// if item not yet rollup actual cost, try to get standard cost
					string strFilterDetail = "ProductID = '" + strProductID + "' AND CostElementType = '" +
						((int) CostElementType.Labor).ToString() + "'";
					DataRow[] drowStdCost = dtbStdCost.Select(strFilterDetail);
					if (drowStdCost.Length > 0)
					{
						try
						{
							decimal decStdCost = Convert.ToDecimal(dtbStdCost.Compute("SUM(Cost)", strFilterDetail));
							decLaborCost = decQuantity * decStdCost + decCharge * decRate;
							drowReport[CostElementType.Labor.ToString()] = decLaborCost;
						}
						catch{}
					}
					else // if item dot have any cost
						drowReport[CostElementType.Labor.ToString()] = decimal.Zero;
				}
				
				#endregion
				
				#region Cost Of Goods Sold - Machine
				
				#region DS Amount
				try
				{
					decDSAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(DSAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Machine).ToString() + "'"));
				}
				catch
				{
					decDSAmount = 0;
				}
				#endregion

				#region Recycle Amount
				try
				{
					decRecycleAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(RecycleAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Machine).ToString() + "'"));
				}
				catch
				{
					decRecycleAmount = 0;
				}
				#endregion

				#region Adjust Amount
				try
				{
					decAdjustAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(AdjustAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Machine).ToString() + "'"));
				}
				catch
				{
					decAdjustAmount = 0;
				}
				#endregion

				#region OH DS Amount
				try
				{
					decOHDSAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_DSAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Machine).ToString() + "'"));
				}
				catch
				{
					decOHDSAmount = 0;
				}
				#endregion

				#region OH Recycle Amount
				try
				{
					decOHRecAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_RecycleAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Machine).ToString() + "'"));
				}
				catch
				{
					decOHRecAmount = 0;
				}
				#endregion

				#region OH Adjust Amount
				try
				{
					decOHAdjAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_AdjustAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.Machine).ToString() + "'"));
				}
				catch
				{
					decOHAdjAmount = 0;
				}
				#endregion

				decCharge = (decDSAmount - decRecycleAmount - decAdjustAmount)
					+ (decOHDSAmount - decOHRecAmount - decOHAdjAmount);
				if (drowActualCost.Length > 0)
				{
					try
					{
						string strFilterDetail = strFilterCondition + " AND CostElementType = '" +
							((int) CostElementType.Machine).ToString() + "'";
						decimal decActualCost = Convert.ToDecimal(dtbActualCost.Compute("SUM(ActualCost)", strFilterDetail));
						decMachineCost = decQuantity * decActualCost + decCharge * decRate;
						drowReport[CostElementType.Machine.ToString()] = decMachineCost;
					}
					catch{}
				}
				else
				{
					// if item not yet rollup actual cost, try to get standard cost
					string strFilterDetail = "ProductID = '" + strProductID + "' AND CostElementType = '" +
						((int) CostElementType.Machine).ToString() + "'";
					DataRow[] drowStdCost = dtbStdCost.Select(strFilterDetail);
					if (drowStdCost.Length > 0)
					{
						try
						{
							decimal decStdCost = Convert.ToDecimal(dtbStdCost.Compute("SUM(Cost)", strFilterDetail));
							decMachineCost = decQuantity * decStdCost + decCharge * decRate;
							drowReport[CostElementType.Machine.ToString()] = decMachineCost;
						}
						catch{}
					}
					else // if item dot have any cost
						drowReport[CostElementType.Machine.ToString()] = decimal.Zero;
				}
				
				#endregion
				
				#region Cost Of Goods Sold - Overhead
				
				#region DS Amount
				try
				{
					decDSAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(DSAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.OverHead).ToString() + "'"));
				}
				catch
				{
					decDSAmount = 0;
				}
				#endregion

				#region Recycle Amount
				try
				{
					decRecycleAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(RecycleAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.OverHead).ToString() + "'"));
				}
				catch
				{
					decRecycleAmount = 0;
				}
				#endregion

				#region Adjust Amount
				try
				{
					decAdjustAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(AdjustAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.OverHead).ToString() + "'"));
				}
				catch
				{
					decAdjustAmount = 0;
				}
				#endregion

				#region OH DS Amount
				try
				{
					decOHDSAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_DSAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.OverHead).ToString() + "'"));
				}
				catch
				{
					decOHDSAmount = 0;
				}
				#endregion

				#region OH Recycle Amount
				try
				{
					decOHRecAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_RecycleAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.OverHead).ToString() + "'"));
				}
				catch
				{
					decOHRecAmount = 0;
				}
				#endregion

				#region OH Adjust Amount
				try
				{
					decOHAdjAmount = Convert.ToDecimal(dtbChargeAllocation.Compute("SUM(OH_AdjustAmount)", strFilterCondition
						+ " AND CostElementType = '" + ((int) CostElementType.OverHead).ToString() + "'"));
				}
				catch
				{
					decOHAdjAmount = 0;
				}
				#endregion

				decCharge = (decDSAmount - decRecycleAmount - decAdjustAmount)
					+ (decOHDSAmount - decOHRecAmount - decOHAdjAmount);
				if (drowActualCost.Length > 0)
				{
					try
					{
						string strFilterDetail = strFilterCondition + " AND CostElementType = '" +
							((int) CostElementType.OverHead).ToString() + "'";
						decimal decActualCost = Convert.ToDecimal(dtbActualCost.Compute("SUM(ActualCost)", strFilterDetail));
						decOverCost = decQuantity * decActualCost + decCharge * decRate;
						drowReport[CostElementType.OverHead.ToString()] = decOverCost;
					}
					catch{}
				}
				else
				{
					// if item not yet rollup actual cost, try to get standard cost
					string strFilterDetail = "ProductID = '" + strProductID + "' AND CostElementType = '" +
						((int) CostElementType.OverHead).ToString() + "'";
					DataRow[] drowStdCost = dtbStdCost.Select(strFilterDetail);
					if (drowStdCost.Length > 0)
					{
						try
						{
							decimal decStdCost = Convert.ToDecimal(dtbStdCost.Compute("SUM(Cost)", strFilterDetail));
							decOverCost = decQuantity * decStdCost + decCharge * decRate;
							drowReport[CostElementType.OverHead.ToString()] = decOverCost;
						}
						catch{}
					}
					else // if item dot have any cost
						drowReport[CostElementType.OverHead.ToString()] = decimal.Zero;
				}
				
				#endregion

				#region Total Cost Of Goods Sold

				try
				{
					drowReport["CostOfGoodsSold"] = decRawCost + decSubCost + decMachineCost + decLaborCost + decOverCost;
				}
				catch{}

				#endregion

				#endregion
				
				dtbData.Rows.Add(drowReport);
			}
			
			#endregion
			
			#region report

			C1Report rptReport = new C1Report();
			
			mLayoutFile = "SaleByLicensorBreakDown.xml";
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile)[0]);
			rptReport.Layout.PaperSize = PaperKind.A3;

			#region report parameter
				
			try
			{
				rptReport.Fields["fldCCN"].Text = GetCCN(pstrCCNID);
			}
			catch{}
			try
			{
				rptReport.Fields["fldFromDate"].Text = dtmFromDate.ToString("dd-MM-yyyy HH:mm");
			}
			catch{}
			try
			{
				rptReport.Fields["fldToDate"].Text = dtmToDate.ToString("dd-MM-yyyy HH:mm");
			}
			catch{}
			try
			{
				if (pstrLicensorID.Split(",".ToCharArray()).Length > 1)
					rptReport.Fields["fldLic"].Text = "Multi-Selection";
				else if (pstrLicensorID.Length > 0)
					rptReport.Fields["fldLic"].Text = GetCustomerInfo(pstrLicensorID);
			}
			catch{}
				
			#endregion
				
			// set datasource object that provides data to report.
			rptReport.DataSource.Recordset = dtbData;
			// render report
			rptReport.Render();

			// render the report into the PrintPreviewControl
			C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
			ppvViewer.FormTitle = "Sale revenue and cost of goods sold break-down report (Classified By Licensors)";
			
			ppvViewer.ReportViewer.Document = rptReport.Document;
			ppvViewer.Show();
			
			#endregion

			return dtbData;
		}

		private DataTable GetReportData(string pstrCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, string pstrLicensorID, int pintMakeItem)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = "SELECT SO_ConfirmShipDetail.InvoiceQty AS Quantity, SO_ConfirmShipDetail.InvoiceQty * ISNULL(SO_ConfirmShipDetail.Price,0) * SO_ConfirmShipMaster.ExchangeRate AS Amount,"
					+ " ITM_Product.Code AS PartNo, ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
					+ " MST_Party.Code AS Licensor, SO_ConfirmShipDetail.ProductID,"
					+ " ShippedDate, ITM_Product.InventorID AS PartyID"
					+ " FROM SO_ConfirmShipMaster JOIN SO_ConfirmShipDetail"
					+ " 	ON SO_ConfirmShipMaster.ConfirmShipMasterID = SO_ConfirmShipDetail.ConfirmShipMasterID"
					+ " JOIN ITM_Product"
					+ " 	ON SO_ConfirmShipDetail.ProductID = ITM_Product.ProductID"
					+ " JOIN SO_SaleOrderMaster"
					+ " 	ON SO_ConfirmShipMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID"
					+ " JOIN SO_SaleOrderDetail"
					+ " 	ON SO_ConfirmShipDetail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID"
					+ " JOIN MST_Party"
					+ " 	ON ITM_Product.InventorID = MST_Party.PartyID"
					+ " WHERE ShippedDate >= ?"
					+ " AND ShippedDate <= ?"
					+ " AND SO_ConfirmShipDetail.InvoiceQty > 0"
					+ " AND SO_SaleOrderMaster.CCNID = " + pstrCCNID;
				if (pstrLicensorID.Length > 0)
					strSql += " AND ITM_Product.InventorID IN (" + pstrLicensorID + ")";
				if (pintMakeItem >= 0)
					strSql += " AND ITM_Product.MakeItem = " + pintMakeItem;
				//hacked by duongna
				strSql +=
					" UNION ALL " + 
					"  " + 
					" SELECT  " + 
					" 	-SO_ReturnedGoodsDetail.ReceiveQuantity AS Quantity,  " + 
					" 	-SO_ReturnedGoodsDetail.ReceiveQuantity * ISNULL(SO_ReturnedGoodsDetail.UnitPrice,0) * SO_ReturnedGoodsMaster.ExchangeRate AS Amount,  " + 
					" 	ITM_Product.Code AS PartNo,  " + 
					" 	ITM_Product.Description AS PartName,  " + 
					" 	ITM_Product.Revision AS Model,  " + 
					" 	MST_Party.Code AS Licensor,  " + 
					" 	SO_ReturnedGoodsDetail.ProductID,  " + 
					" 	PostDate AS ShippedDate,  " + 
					" 	ITM_Product.InventorID AS PartyID  " + 
					" FROM  " + 
					" 	SO_ReturnedGoodsMaster  " + 
					" 	JOIN SO_ReturnedGoodsDetail   " + 
					" 		ON SO_ReturnedGoodsMaster.ReturnedGoodsMasterID = SO_ReturnedGoodsDetail.ReturnedGoodsMasterID  " + 
					" 	JOIN ITM_Product   " + 
					" 		ON SO_ReturnedGoodsDetail.ProductID = ITM_Product.ProductID  " + 
					" 	JOIN MST_Party   " + 
					" 		ON ITM_Product.InventorID = MST_Party.PartyID  " + 
					" WHERE  " + 
					" 	PostDate >= ?  " + 
					" 	AND PostDate <= ? " + 
					" 	AND SO_ReturnedGoodsDetail.ReceiveQuantity > 0  " + 
					"	AND SO_ReturnedGoodsMaster.CCNID = " + pstrCCNID;

				if (pstrLicensorID.Length > 0)
					strSql += " AND ITM_Product.InventorID IN (" + pstrLicensorID + ")";
				if (pintMakeItem >= 0)
					strSql += " AND ITM_Product.MakeItem = " + pintMakeItem;
				strSql += " ORDER BY MST_Party.Code, ITM_Product.Revision, ITM_Product.Code, ITM_Product.Description";
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDateReturn", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDateReturn", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				
				return dtbData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DataTable GetActualCost(string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = "SELECT SUM(CST_ActualCostHistory.ActualCost) AS ActualCost, CST_ActualCostHistory.ProductID," 
					+ " CST_ActualCostHistory.ActCostAllocationMasterID,"
					+ " CST_ActCostAllocationMaster.FromDate, CST_ActCostAllocationMaster.ToDate, STD_CostElementType.Code AS CostElementType"
					+ " FROM CST_ActualCostHistory JOIN CST_ActCostAllocationMaster"
					+ " 	ON CST_ActualCostHistory.ActCostAllocationMasterID = CST_ActCostAllocationMaster.ActCostAllocationMasterID"
					+ " JOIN STD_CostElement"
					+ " 	ON CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID"
					+ " JOIN STD_CostElementType"
					+ "		ON STD_CostElement.CostElementTypeID = STD_CostElementType.CostElementTypeID"
					+ " WHERE CST_ActCostAllocationMaster.CCNID = " + pstrCCNID
					+ " GROUP BY CST_ActualCostHistory.ProductID, CST_ActualCostHistory.ActCostAllocationMasterID,"
					+ " CST_ActCostAllocationMaster.FromDate, CST_ActCostAllocationMaster.ToDate, STD_CostElementType.Code"
					+ " ORDER BY CST_ActCostAllocationMaster.FromDate";
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				
				return dtbData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private DataTable GetChargeAllocation(string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = "SELECT SUM(CST_DSAndRecycleAllocation.DSAmount) AS DSAmount,"
					+ " SUM(CST_DSAndRecycleAllocation.RecycleAmount) AS RecycleAmount,"
					+ " SUM(CST_DSAndRecycleAllocation.AdjustAmount) AS AdjustAmount,"
					+ " SUM(CST_DSAndRecycleAllocation.OH_RecycleAmount) AS OH_RecycleAmount,"
					+ " SUM(CST_DSAndRecycleAllocation.OH_DSAmount) AS OH_DSAmount,"
					+ " SUM(CST_DSAndRecycleAllocation.OH_AdjustAmount) AS OH_AdjustAmount,"
					+ " CST_DSAndRecycleAllocation.ProductID," 
					+ " CST_DSAndRecycleAllocation.ActCostAllocationMasterID,"
					+ " CST_ActCostAllocationMaster.FromDate, CST_ActCostAllocationMaster.ToDate, STD_CostElementType.Code AS CostElementType"
					+ " FROM CST_DSAndRecycleAllocation JOIN CST_ActCostAllocationMaster"
					+ " 	ON CST_DSAndRecycleAllocation.ActCostAllocationMasterID = CST_ActCostAllocationMaster.ActCostAllocationMasterID"
					+ " JOIN STD_CostElement"
					+ " 	ON CST_DSAndRecycleAllocation.CostElementID = STD_CostElement.CostElementID"
					+ " JOIN STD_CostElementType"
					+ "		ON STD_CostElement.CostElementTypeID = STD_CostElementType.CostElementTypeID"
					+ " WHERE CST_ActCostAllocationMaster.CCNID = " + pstrCCNID
					+ " GROUP BY CST_DSAndRecycleAllocation.ProductID, CST_DSAndRecycleAllocation.ActCostAllocationMasterID,"
					+ " CST_ActCostAllocationMaster.FromDate, CST_ActCostAllocationMaster.ToDate, STD_CostElementType.Code"
					+ " ORDER BY CST_ActCostAllocationMaster.FromDate";
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				
				return dtbData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private DataTable GetStdCost()
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = "SELECT SUM(Cost) AS Cost, ProductID, STD_CostElementType.Code AS CostElementType"
					+ " FROM CST_STDItemCost JOIN STD_CostElement"
					+ " 	ON CST_STDItemCost.CostElementID = STD_CostElement.CostElementID"
					+ " JOIN STD_CostElementType"
					+ " 	ON STD_CostElement.CostElementTypeID = STD_CostElementType.CostElementTypeID"
					+ " GROUP BY ProductID, STD_CostElementType.Code";
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				
				return dtbData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private string GetCCN(string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = "SELECT Code + ' (' + Description + ')' FROM MST_CCN WHERE CCNID = " + pstrCCNID;
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objResult = ocmdPCS.ExecuteScalar();
				return objResult.ToString();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private string GetCustomerInfo(string pstrCustomerID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = "SELECT Code + ' (' + Name + ')' FROM MST_Party WHERE PartyID = " + pstrCustomerID;
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objResult = ocmdPCS.ExecuteScalar();
				return objResult.ToString();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

	}
}