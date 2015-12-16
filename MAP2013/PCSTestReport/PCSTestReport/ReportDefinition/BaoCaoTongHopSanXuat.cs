using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Reflection;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace BaoCaoTongHopSanXuat
{
	[Serializable]
	public class BaoCaoTongHopSanXuat : MarshalByRefObject, IDynamicReport
	{
		#region constants

		const string BeginTableName = "BeginTable";
		const string WOComleptionTableName = "WOComleptionTable";
		const string InPlanTableName = "InPlanTable";
		const string ReturnGoodsTableName = "ReturnGoodsTable";
		const string POReceiptTableName = "POReceiptTable";
		const string InQCTableName = "InQCTable";
		const string RecycledTableName = "RecycledTable";
		const string InMiscTableName = "InMiscTable";
		const string InAbnormalTableName = "InAbnormalTable";
		const string OutPlanTableName = "OutPlanTable";
		const string DestroyTableName = "DestroyTable";
		const string OutQCTableName = "OutQCTable";
		const string ReturnToVendorTableName = "ReturnToVendorTable";
		const string OutMiscTableName = "OutMiscTable";
		const string OutAbnormalTableName = "OutAbnormalTable";
		const string ShippingTableName = "ShippingTable";
		const string AdjustmentTableName = "AdjustmentTable";
		const string TransactionHistoryTableName = "TransactionHistoryTable";
		const string RecoverTableName = "RecoverTable";

		#endregion

		public BaoCaoTongHopSanXuat()
		{
		}

		#region IDynamicReport Members
		
		private bool mUseReportViewerRenderEngine = false;
		private string mConnectionString;
		private ReportBuilder mReportBuilder;
		private C1PrintPreviewControl mReportViewer;

		private object mResult;

		/// <summary>
		/// ConnectionString, provide for the Dynamic Report
		/// ALlow Dynamic Report to access the DataBase of PCS
		/// </summary>
		public string PCSConnectionString
		{
			get { return mConnectionString; }
			set { mConnectionString = value; }
		}

		/// <summary>
		/// Report Builder Utility Object
		/// Dynamic Report can use this object to render, modify, layout the report
		/// </summary>
		public ReportBuilder PCSReportBuilder
		{
			get { return mReportBuilder; }
			set { mReportBuilder = value; }
		}

		/// <summary>
		/// ReportViewer Object, provide for the DynamicReport, 
		/// allow Dynamic Report to manipulate with the REportViewer, 
		/// modify the report after rendered if needed
		/// </summary>
		public C1PrintPreviewControl PCSReportViewer
		{
			get { return mReportViewer; }
			set { mReportViewer = value; }
		}

		/// <summary>
		/// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
		/// </summary>
		public object Result
		{
			get { return mResult; }
			set { mResult = value; }
		}		

		/// <summary>
		/// Notify PCS whether the rendering report process is run by
		/// this IDynamicReport or the ReportViewer Engine (in the ReportViewer form)
		/// </summary>
		public bool UseReportViewerRenderEngine
		{
			get { return mUseReportViewerRenderEngine; }
			set { mUseReportViewerRenderEngine = value; }
		}

		private string mReportFolder = string.Empty;
		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>				
		public string ReportDefinitionFolder
		{
			get { return mReportFolder; }
			set { mReportFolder = value; }
		}


		private string mLayoutFile = string.Empty;		
		/// <summary>
		/// Inform External Process about the Layout file
		/// in which PCS instruct to use
		/// (PCS will assign this property while ReportViewer Form execute,
		/// ReportVIewer form will use the layout file in the report config entry to put in this property)
		/// </summary>		
		public string ReportLayoutFile
		{
			get 
			{
				return mLayoutFile;
			}
			set
			{
				mLayoutFile = value;
			}
		}

		#endregion		
		
		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		public DataTable ExecuteReport(string pstrMonth, string pstrDepartmentID, string pstrProductionLineID, string pstrLocationID, string pstrBinID, string pstrCategoryID, string pstrMakeItem)
		{
			const string MONTH_FLD = "fldMonth";
			const string DEPARTMENT_FLD = "fldDepartment";
			const string PRODUCTIONLINE_FLD = "fldProductionLine";
			const string LOCATION_FLD = "fldLocation";
			const string BIN_FLD = "fldBin";
			DateTime dtmMonth = Convert.ToDateTime(pstrMonth);
			dtmMonth = new DateTime(dtmMonth.Year, dtmMonth.Month, 1);

			int intMakeItem = -1;
			if (pstrMakeItem != null && pstrMakeItem != string.Empty)
				intMakeItem = Convert.ToInt32(Convert.ToBoolean(pstrMakeItem));

			#region Prepares

			DataTable dtbReportData = MakeTable();
			DataSet dstData = GetDataAndCache(dtmMonth, pstrDepartmentID, pstrProductionLineID, pstrLocationID, pstrBinID, pstrCategoryID, intMakeItem);
			DataTable dtbBegin = dstData.Tables[BeginTableName];
			DataTable dtbWO = dstData.Tables[WOComleptionTableName];
			DataTable dtbInPlan = dstData.Tables[InPlanTableName];
			DataTable dtbReturnGoods = dstData.Tables[ReturnGoodsTableName];
			DataTable dtbPOReceipt = dstData.Tables[POReceiptTableName];
			DataTable dtbInQC = dstData.Tables[InQCTableName];
			DataTable dtbRecycled = dstData.Tables[RecycledTableName];
			DataTable dtbInMisc = dstData.Tables[InMiscTableName];
			DataTable dtbInAbnormal = dstData.Tables[InAbnormalTableName];
			DataTable dtbOutPlan = dstData.Tables[OutPlanTableName];
			DataTable dtbDestroy = dstData.Tables[DestroyTableName];
			DataTable dtbOutQC = dstData.Tables[OutQCTableName];
			DataTable dtbRTV = dstData.Tables[ReturnToVendorTableName];
			DataTable dtbOutMisc = dstData.Tables[OutMiscTableName];
			DataTable dtbOutAbnormal = dstData.Tables[OutAbnormalTableName];
			DataTable dtbShipping = dstData.Tables[ShippingTableName];
			DataTable dtbAdjustment = dstData.Tables[AdjustmentTableName];
			DataTable dtbTransactionHistory = dstData.Tables[TransactionHistoryTableName];
			DataTable dtbRecover = dstData.Tables[RecoverTableName];

			#endregion

			#region Put data to report table

			#region use begin table as default data

			foreach (DataRow drData in dtbBegin.Rows)
			{
				DataRow drowReport = dtbReportData.NewRow();
				drowReport.ItemArray = drData.ItemArray;
				string strProductID = drData["ProductID"].ToString();
				string strBinID = drData["BinID"].ToString();
				string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

				#region work order completion
				decimal decQty = 0;
				DataRow[] drowData = dtbWO.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["WOQty"]);
					}
					catch{}
					dr.Delete();
				}
				dtbWO.AcceptChanges();
				if (decQty > 0)
					if (decQty > 0) drowReport["WOQty"] = decQty;
				#endregion

				#region In plan
				decQty = 0;
				drowData = dtbInPlan.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["InPlan"]);
					}
					catch{}
					dr.Delete();
				}
				dtbInPlan.AcceptChanges();
				if (decQty > 0)
					if (decQty > 0) drowReport["InPlan"] = decQty;
				#endregion

				#region return goods receipt
				decQty = 0;
				drowData = dtbReturnGoods.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["RGR"]);
					}
					catch{}
					dr.Delete();
				}
				dtbReturnGoods.AcceptChanges();
				if (decQty > 0)
					if (decQty > 0) drowReport["RGR"] = decQty;
				#endregion

				#region po receipt
				decQty = 0;
				drowData = dtbPOReceipt.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["Receipt"]);
					}
					catch{}
					dr.Delete();
				}
				dtbPOReceipt.AcceptChanges();
				if (decQty > 0)
					if (decQty > 0) drowReport["Receipt"] = decQty;
				#endregion

				#region In QC
				decQty = 0;
				drowData = dtbInQC.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["InQC"]);
					}
					catch{}
					dr.Delete();
				}
				dtbInQC.AcceptChanges();
				if (decQty > 0)
					if (decQty > 0) drowReport["InQC"] = decQty;
				#endregion

				#region recycled
				decQty = 0;
				drowData = dtbRecycled.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["Recycled"]);
					}
					catch{}
					dr.Delete();
				}
				dtbRecycled.AcceptChanges();
				if (decQty > 0)
					if (decQty > 0) drowReport["Recycled"] = decQty;
				#endregion

				#region In Misc
				decQty = 0;
				drowData = dtbInMisc.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["InMisc"]);
					}
					catch{}
					dr.Delete();
				}
				dtbInMisc.AcceptChanges();
				if (decQty > 0)
					if (decQty > 0) drowReport["InMisc"] = decQty;
				#endregion

				#region In Abnormal
				decQty = 0;
				drowData = dtbInAbnormal.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["InAbnormal"]);
					}
					catch{}
					dr.Delete();
				}
				dtbInAbnormal.AcceptChanges();
				drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity > 0");
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["AdjustQuantity"]);
					}
					catch{}
					dr.Delete();
				}
				dtbAdjustment.AcceptChanges();
				if (decQty > 0)
					if (decQty > 0) drowReport["InAbnormal"] = decQty;
				#endregion

				#region Out Plan
				decQty = 0;
				drowData = dtbOutPlan.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["OutPlan"]);
					}
					catch{}
					dr.Delete();
				}
				dtbOutPlan.AcceptChanges();
				drowData = dtbTransactionHistory.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["Quantity"]);
					}
					catch{}
					dr.Delete();
				}
				dtbTransactionHistory.AcceptChanges();
				if (decQty > 0)
					if (decQty > 0) drowReport["OutPlan"] = decQty;
				#endregion

				#region Destroy
				decQty = 0;
				drowData = dtbDestroy.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["Destroy"]);
					}
					catch{}
					dr.Delete();
				}
				dtbDestroy.AcceptChanges();
				if (decQty > 0)
					if (decQty > 0) drowReport["Destroy"] = decQty;
				#endregion

				#region Out QC
				decQty = 0;
				drowData = dtbOutQC.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["OutQC"]);
					}
					catch{}
					dr.Delete();
				}
				dtbOutQC.AcceptChanges();
				if (decQty > 0) drowReport["OutQC"] = decQty;
				#endregion

				#region Return to vendor
				decQty = 0;
				drowData = dtbRTV.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["RTV"]);
					}
					catch{}
					dr.Delete();
				}
				dtbRTV.AcceptChanges();
				if (decQty > 0) drowReport["RTV"] = decQty;
				#endregion

				#region Out Misc
				decQty = 0;
				drowData = dtbOutMisc.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["OutMisc"]);
					}
					catch{}
					dr.Delete();
				}
				dtbOutMisc.AcceptChanges();
				if (decQty > 0) drowReport["OutMisc"] = decQty;
				#endregion

				#region Out Abnormal
				decQty = 0;
				drowData = dtbOutAbnormal.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["OutAbnormal"]);
					}
					catch{}
					dr.Delete();
				}
				dtbOutAbnormal.AcceptChanges();
				drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
					}
					catch{}
					dr.Delete();
				}
				dtbAdjustment.AcceptChanges();
				drowData = dtbRecover.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += -Convert.ToDecimal(dr["Recover"]);
					}
					catch{}
					dr.Delete();
				}
				dtbRecover.AcceptChanges();
				if (decQty > 0) drowReport["OutAbnormal"] = decQty;
				#endregion

				#region Shipping
				decQty = 0;
				drowData = dtbShipping.Select(strFilter);
				foreach (DataRow dr in drowData)
				{
					try
					{
						decQty += Convert.ToDecimal(dr["Shipping"]);
					}
					catch{}
					dr.Delete();
				}
				dtbShipping.AcceptChanges();
				if (decQty > 0) drowReport["Shipping"] = decQty;
				#endregion

				dtbReportData.Rows.Add(drowReport);
			}

			#endregion

			#region look up work order table
			foreach (DataRow drData in dtbWO.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbWO.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					DataRow[] drowData = null;
					
					#region In plan
					decQty = 0;
					drowData = dtbInPlan.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InPlan"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInPlan.AcceptChanges();
					if (decQty > 0) drowReport["InPlan"] = decQty;
					#endregion

					#region return goods receipt
					decQty = 0;
					drowData = dtbReturnGoods.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RGR"]);
						}
						catch{}
						dr.Delete();
					}
					dtbReturnGoods.AcceptChanges();
					if (decQty > 0) drowReport["RGR"] = decQty;
					#endregion

					#region po receipt
					decQty = 0;
					drowData = dtbPOReceipt.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Receipt"]);
						}
						catch{}
						dr.Delete();
					}
					dtbPOReceipt.AcceptChanges();
					if (decQty > 0) drowReport["Receipt"] = decQty;
					#endregion

					#region In QC
					decQty = 0;
					drowData = dtbInQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInQC.AcceptChanges();
					if (decQty > 0) drowReport["InQC"] = decQty;
					#endregion

					#region recycled
					decQty = 0;
					drowData = dtbRecycled.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Recycled"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecycled.AcceptChanges();
					if (decQty > 0) drowReport["Recycled"] = decQty;
					#endregion

					#region In Misc
					decQty = 0;
					drowData = dtbInMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInMisc.AcceptChanges();
					if (decQty > 0) drowReport["InMisc"] = decQty;
					#endregion

					#region In Abnormal
					decQty = 0;
					drowData = dtbInAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity > 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					if (decQty > 0) drowReport["InAbnormal"] = decQty;
					#endregion

					#region Out Plan
					decQty = 0;
					drowData = dtbOutPlan.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutPlan"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutPlan.AcceptChanges();
					drowData = dtbTransactionHistory.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Quantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbTransactionHistory.AcceptChanges();
					if (decQty > 0) drowReport["OutPlan"] = decQty;
					#endregion

					#region Destroy
					decQty = 0;
					drowData = dtbDestroy.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Destroy"]);
						}
						catch{}
						dr.Delete();
					}
					dtbDestroy.AcceptChanges();
					if (decQty > 0) drowReport["Destroy"] = decQty;
					#endregion

					#region Out QC
					decQty = 0;
					drowData = dtbOutQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutQC.AcceptChanges();
					if (decQty > 0) drowReport["OutQC"] = decQty;
					#endregion

					#region Return to vendor
					decQty = 0;
					drowData = dtbRTV.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RTV"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRTV.AcceptChanges();
					if (decQty > 0) drowReport["RTV"] = decQty;
					#endregion

					#region Out Misc
					decQty = 0;
					drowData = dtbOutMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutMisc.AcceptChanges();
					if (decQty > 0) drowReport["OutMisc"] = decQty;
					#endregion

					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#region look up In Plan table
			foreach (DataRow drData in dtbInPlan.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbInPlan.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					DataRow[] drowData = null;
					
					#region return goods receipt
					decQty = 0;
					drowData = dtbReturnGoods.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RGR"]);
						}
						catch{}
						dr.Delete();
					}
					dtbReturnGoods.AcceptChanges();
					if (decQty > 0) drowReport["RGR"] = decQty;
					#endregion

					#region po receipt
					decQty = 0;
					drowData = dtbPOReceipt.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Receipt"]);
						}
						catch{}
						dr.Delete();
					}
					dtbPOReceipt.AcceptChanges();
					if (decQty > 0) drowReport["Receipt"] = decQty;
					#endregion

					#region In QC
					decQty = 0;
					drowData = dtbInQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInQC.AcceptChanges();
					if (decQty > 0) drowReport["InQC"] = decQty;
					#endregion

					#region recycled
					decQty = 0;
					drowData = dtbRecycled.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Recycled"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecycled.AcceptChanges();
					if (decQty > 0) drowReport["Recycled"] = decQty;
					#endregion

					#region In Misc
					decQty = 0;
					drowData = dtbInMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInMisc.AcceptChanges();
					if (decQty > 0) drowReport["InMisc"] = decQty;
					#endregion

					#region In Abnormal
					decQty = 0;
					drowData = dtbInAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity > 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					if (decQty > 0) drowReport["InAbnormal"] = decQty;
					#endregion

					#region Out Plan
					decQty = 0;
					drowData = dtbOutPlan.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutPlan"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutPlan.AcceptChanges();
					drowData = dtbTransactionHistory.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Quantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbTransactionHistory.AcceptChanges();
					if (decQty > 0) drowReport["OutPlan"] = decQty;
					#endregion

					#region Destroy
					decQty = 0;
					drowData = dtbDestroy.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Destroy"]);
						}
						catch{}
						dr.Delete();
					}
					dtbDestroy.AcceptChanges();
					if (decQty > 0) drowReport["Destroy"] = decQty;
					#endregion

					#region Out QC
					decQty = 0;
					drowData = dtbOutQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutQC.AcceptChanges();
					if (decQty > 0) drowReport["OutQC"] = decQty;
					#endregion

					#region Return to vendor
					decQty = 0;
					drowData = dtbRTV.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RTV"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRTV.AcceptChanges();
					if (decQty > 0) drowReport["RTV"] = decQty;
					#endregion

					#region Out Misc
					decQty = 0;
					drowData = dtbOutMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutMisc.AcceptChanges();
					if (decQty > 0) drowReport["OutMisc"] = decQty;
					#endregion

					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#region look up return goods received table
			foreach (DataRow drData in dtbReturnGoods.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbReturnGoods.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					DataRow[] drowData = null;
					
					#region po receipt
					decQty = 0;
					drowData = dtbPOReceipt.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Receipt"]);
						}
						catch{}
						dr.Delete();
					}
					dtbPOReceipt.AcceptChanges();
					if (decQty > 0) drowReport["Receipt"] = decQty;
					#endregion

					#region In QC
					decQty = 0;
					drowData = dtbInQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInQC.AcceptChanges();
					if (decQty > 0) drowReport["InQC"] = decQty;
					#endregion

					#region recycled
					decQty = 0;
					drowData = dtbRecycled.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Recycled"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecycled.AcceptChanges();
					if (decQty > 0) drowReport["Recycled"] = decQty;
					#endregion

					#region In Misc
					decQty = 0;
					drowData = dtbInMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInMisc.AcceptChanges();
					if (decQty > 0) drowReport["InMisc"] = decQty;
					#endregion

					#region In Abnormal
					decQty = 0;
					drowData = dtbInAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity > 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					if (decQty > 0) drowReport["InAbnormal"] = decQty;
					#endregion

					#region Out Plan
					decQty = 0;
					drowData = dtbOutPlan.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutPlan"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutPlan.AcceptChanges();
					drowData = dtbTransactionHistory.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Quantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbTransactionHistory.AcceptChanges();
					if (decQty > 0) drowReport["OutPlan"] = decQty;
					#endregion

					#region Destroy
					decQty = 0;
					drowData = dtbDestroy.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Destroy"]);
						}
						catch{}
						dr.Delete();
					}
					dtbDestroy.AcceptChanges();
					if (decQty > 0) drowReport["Destroy"] = decQty;
					#endregion

					#region Out QC
					decQty = 0;
					drowData = dtbOutQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutQC.AcceptChanges();
					if (decQty > 0) drowReport["OutQC"] = decQty;
					#endregion

					#region Return to vendor
					decQty = 0;
					drowData = dtbRTV.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RTV"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRTV.AcceptChanges();
					if (decQty > 0) drowReport["RTV"] = decQty;
					#endregion

					#region Out Misc
					decQty = 0;
					drowData = dtbOutMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutMisc.AcceptChanges();
					if (decQty > 0) drowReport["OutMisc"] = decQty;
					#endregion

					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#region look up PO receipt table
			foreach (DataRow drData in dtbPOReceipt.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbPOReceipt.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					DataRow[] drowData = null;
					
					#region In QC
					decQty = 0;
					drowData = dtbInQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInQC.AcceptChanges();
					if (decQty > 0) drowReport["InQC"] = decQty;
					#endregion

					#region recycled
					decQty = 0;
					drowData = dtbRecycled.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Recycled"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecycled.AcceptChanges();
					if (decQty > 0) drowReport["Recycled"] = decQty;
					#endregion

					#region In Misc
					decQty = 0;
					drowData = dtbInMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInMisc.AcceptChanges();
					if (decQty > 0) drowReport["InMisc"] = decQty;
					#endregion

					#region In Abnormal
					decQty = 0;
					drowData = dtbInAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity > 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					if (decQty > 0) drowReport["InAbnormal"] = decQty;
					#endregion

					#region Out Plan
					decQty = 0;
					drowData = dtbOutPlan.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutPlan"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutPlan.AcceptChanges();
					drowData = dtbTransactionHistory.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Quantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbTransactionHistory.AcceptChanges();
					if (decQty > 0) drowReport["OutPlan"] = decQty;
					#endregion

					#region Destroy
					decQty = 0;
					drowData = dtbDestroy.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Destroy"]);
						}
						catch{}
						dr.Delete();
					}
					dtbDestroy.AcceptChanges();
					if (decQty > 0) drowReport["Destroy"] = decQty;
					#endregion

					#region Out QC
					decQty = 0;
					drowData = dtbOutQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutQC.AcceptChanges();
					if (decQty > 0) drowReport["OutQC"] = decQty;
					#endregion

					#region Return to vendor
					decQty = 0;
					drowData = dtbRTV.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RTV"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRTV.AcceptChanges();
					if (decQty > 0) drowReport["RTV"] = decQty;
					#endregion

					#region Out Misc
					decQty = 0;
					drowData = dtbOutMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutMisc.AcceptChanges();
					if (decQty > 0) drowReport["OutMisc"] = decQty;
					#endregion

					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#region look up IN QC table
			foreach (DataRow drData in dtbInQC.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbInQC.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					DataRow[] drowData = null;
					
					#region recycled
					decQty = 0;
					drowData = dtbRecycled.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Recycled"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecycled.AcceptChanges();
					if (decQty > 0) drowReport["Recycled"] = decQty;
					#endregion

					#region In Misc
					decQty = 0;
					drowData = dtbInMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInMisc.AcceptChanges();
					if (decQty > 0) drowReport["InMisc"] = decQty;
					#endregion

					#region In Abnormal
					decQty = 0;
					drowData = dtbInAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity > 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					if (decQty > 0) drowReport["InAbnormal"] = decQty;
					#endregion

					#region Out Plan
					decQty = 0;
					drowData = dtbOutPlan.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutPlan"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutPlan.AcceptChanges();
					drowData = dtbTransactionHistory.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Quantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbTransactionHistory.AcceptChanges();
					if (decQty > 0) drowReport["OutPlan"] = decQty;
					#endregion

					#region Destroy
					decQty = 0;
					drowData = dtbDestroy.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Destroy"]);
						}
						catch{}
						dr.Delete();
					}
					dtbDestroy.AcceptChanges();
					if (decQty > 0) drowReport["Destroy"] = decQty;
					#endregion

					#region Out QC
					decQty = 0;
					drowData = dtbOutQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutQC.AcceptChanges();
					if (decQty > 0) drowReport["OutQC"] = decQty;
					#endregion

					#region Return to vendor
					decQty = 0;
					drowData = dtbRTV.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RTV"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRTV.AcceptChanges();
					if (decQty > 0) drowReport["RTV"] = decQty;
					#endregion

					#region Out Misc
					decQty = 0;
					drowData = dtbOutMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutMisc.AcceptChanges();
					if (decQty > 0) drowReport["OutMisc"] = decQty;
					#endregion

					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#region look up recycled table
			foreach (DataRow drData in dtbRecycled.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbRecycled.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					DataRow[] drowData = null;
					
					#region In Misc
					decQty = 0;
					drowData = dtbInMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInMisc.AcceptChanges();
					if (decQty > 0) drowReport["InMisc"] = decQty;
					#endregion

					#region In Abnormal
					decQty = 0;
					drowData = dtbInAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity > 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					if (decQty > 0) drowReport["InAbnormal"] = decQty;
					#endregion

					#region Out Plan
					decQty = 0;
					drowData = dtbOutPlan.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutPlan"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutPlan.AcceptChanges();
					drowData = dtbTransactionHistory.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Quantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbTransactionHistory.AcceptChanges();
					if (decQty > 0) drowReport["OutPlan"] = decQty;
					#endregion

					#region Destroy
					decQty = 0;
					drowData = dtbDestroy.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Destroy"]);
						}
						catch{}
						dr.Delete();
					}
					dtbDestroy.AcceptChanges();
					if (decQty > 0) drowReport["Destroy"] = decQty;
					#endregion

					#region Out QC
					decQty = 0;
					drowData = dtbOutQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutQC.AcceptChanges();
					if (decQty > 0) drowReport["OutQC"] = decQty;
					#endregion

					#region Return to vendor
					decQty = 0;
					drowData = dtbRTV.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RTV"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRTV.AcceptChanges();
					if (decQty > 0) drowReport["RTV"] = decQty;
					#endregion

					#region Out Misc
					decQty = 0;
					drowData = dtbOutMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutMisc.AcceptChanges();
					if (decQty > 0) drowReport["OutMisc"] = decQty;
					#endregion

					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#region look up In misc table
			foreach (DataRow drData in dtbInMisc.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbInMisc.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					DataRow[] drowData = null;
					
					#region In Abnormal
					decQty = 0;
					drowData = dtbInAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["InAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbInAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity > 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					if (decQty > 0) drowReport["InAbnormal"] = decQty;
					#endregion

					#region Out Plan
					decQty = 0;
					drowData = dtbOutPlan.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutPlan"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutPlan.AcceptChanges();
					drowData = dtbTransactionHistory.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Quantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbTransactionHistory.AcceptChanges();
					if (decQty > 0) drowReport["OutPlan"] = decQty;
					#endregion

					#region Destroy
					decQty = 0;
					drowData = dtbDestroy.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Destroy"]);
						}
						catch{}
						dr.Delete();
					}
					dtbDestroy.AcceptChanges();
					if (decQty > 0) drowReport["Destroy"] = decQty;
					#endregion

					#region Out QC
					decQty = 0;
					drowData = dtbOutQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutQC.AcceptChanges();
					if (decQty > 0) drowReport["OutQC"] = decQty;
					#endregion

					#region Return to vendor
					decQty = 0;
					drowData = dtbRTV.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RTV"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRTV.AcceptChanges();
					if (decQty > 0) drowReport["RTV"] = decQty;
					#endregion

					#region Out Misc
					decQty = 0;
					drowData = dtbOutMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutMisc.AcceptChanges();
					if (decQty > 0) drowReport["OutMisc"] = decQty;
					#endregion

					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#region look up In abnormal table
			foreach (DataRow drData in dtbInAbnormal.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbInAbnormal.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					try
					{
						decQty = Convert.ToDecimal(drowReport["InAbnormal"]);
					}
					catch{}
					DataRow[] drowData = null;

					#region Inventory Adjustment
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity > 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					if (decQty > 0) drowReport["InAbnormal"] = decQty;
					#endregion
					
					#region Out Plan
					decQty = 0;
					drowData = dtbOutPlan.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutPlan"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutPlan.AcceptChanges();
					drowData = dtbTransactionHistory.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Quantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbTransactionHistory.AcceptChanges();
					if (decQty > 0) drowReport["OutPlan"] = decQty;
					#endregion

					#region Destroy
					decQty = 0;
					drowData = dtbDestroy.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Destroy"]);
						}
						catch{}
						dr.Delete();
					}
					dtbDestroy.AcceptChanges();
					if (decQty > 0) drowReport["Destroy"] = decQty;
					#endregion

					#region Out QC
					decQty = 0;
					drowData = dtbOutQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutQC.AcceptChanges();
					if (decQty > 0) drowReport["OutQC"] = decQty;
					#endregion

					#region Return to vendor
					decQty = 0;
					drowData = dtbRTV.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RTV"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRTV.AcceptChanges();
					if (decQty > 0) drowReport["RTV"] = decQty;
					#endregion

					#region Out Misc
					decQty = 0;
					drowData = dtbOutMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutMisc.AcceptChanges();
					if (decQty > 0) drowReport["OutMisc"] = decQty;
					#endregion

					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}

			if (dtbInAbnormal.Rows.Count == 0)
			{
				DataRow[] drowsAdjust = dtbAdjustment.Select("AdjustQuantity > 0");
				foreach (DataRow drData in drowsAdjust)
				{
					if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
					{
						DataRow drowReport = dtbReportData.NewRow();
						foreach (DataColumn dcol in dtbAdjustment.Columns)
						{
							if (dcol.ColumnName != "AdjustQuantity")
								drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
						}
						string strProductID = drData["ProductID"].ToString();
						string strBinID = drData["BinID"].ToString();
						string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

						decimal decQty = 0;
						DataRow[] drowData = null;

						#region Inventory Adjustment
						drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity > 0");
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += Convert.ToDecimal(dr["AdjustQuantity"]);
							}
							catch{}
							dr.Delete();
						}
						dtbAdjustment.AcceptChanges();
						if (decQty > 0) drowReport["InAbnormal"] = decQty;
						#endregion
					
						#region Out Plan
						decQty = 0;
						drowData = dtbOutPlan.Select(strFilter);
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += Convert.ToDecimal(dr["OutPlan"]);
							}
							catch{}
							dr.Delete();
						}
						dtbOutPlan.AcceptChanges();
						drowData = dtbTransactionHistory.Select(strFilter);
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += Convert.ToDecimal(dr["Quantity"]);
							}
							catch{}
							dr.Delete();
						}
						dtbTransactionHistory.AcceptChanges();
						if (decQty > 0) drowReport["OutPlan"] = decQty;
						#endregion

						#region Destroy
						decQty = 0;
						drowData = dtbDestroy.Select(strFilter);
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += Convert.ToDecimal(dr["Destroy"]);
							}
							catch{}
							dr.Delete();
						}
						dtbDestroy.AcceptChanges();
						if (decQty > 0) drowReport["Destroy"] = decQty;
						#endregion

						#region Out QC
						decQty = 0;
						drowData = dtbOutQC.Select(strFilter);
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += Convert.ToDecimal(dr["OutQC"]);
							}
							catch{}
							dr.Delete();
						}
						dtbOutQC.AcceptChanges();
						if (decQty > 0) drowReport["OutQC"] = decQty;
						#endregion

						#region Return to vendor
						decQty = 0;
						drowData = dtbRTV.Select(strFilter);
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += Convert.ToDecimal(dr["RTV"]);
							}
							catch{}
							dr.Delete();
						}
						dtbRTV.AcceptChanges();
						if (decQty > 0) drowReport["RTV"] = decQty;
						#endregion

						#region Out Misc
						decQty = 0;
						drowData = dtbOutMisc.Select(strFilter);
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += Convert.ToDecimal(dr["OutMisc"]);
							}
							catch{}
							dr.Delete();
						}
						dtbOutMisc.AcceptChanges();
						if (decQty > 0) drowReport["OutMisc"] = decQty;
						#endregion

						#region Out Abnormal
						decQty = 0;
						drowData = dtbOutAbnormal.Select(strFilter);
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += Convert.ToDecimal(dr["OutAbnormal"]);
							}
							catch{}
							dr.Delete();
						}
						dtbOutAbnormal.AcceptChanges();
						drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
							}
							catch{}
							dr.Delete();
						}
						dtbAdjustment.AcceptChanges();
						drowData = dtbRecover.Select(strFilter);
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += -Convert.ToDecimal(dr["Recover"]);
							}
							catch{}
							dr.Delete();
						}
						dtbRecover.AcceptChanges();
						if (decQty > 0) drowReport["OutAbnormal"] = decQty;
						#endregion

						#region Shipping
						decQty = 0;
						drowData = dtbShipping.Select(strFilter);
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += Convert.ToDecimal(dr["Shipping"]);
							}
							catch{}
							dr.Delete();
						}
						dtbShipping.AcceptChanges();
						if (decQty > 0) drowReport["Shipping"] = decQty;
						#endregion

						dtbReportData.Rows.Add(drowReport);
					}
				}
			}
			#endregion

			#region look up out plan table
			foreach (DataRow drData in dtbOutPlan.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbOutPlan.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					DataRow[] drowData = null;

					#region po receipt by outside && work order completion

					drowData = dtbTransactionHistory.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Quantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbTransactionHistory.AcceptChanges();
					if (decQty > 0)
						if (decQty > 0) drowReport["OutPlan"] = decQty;

					#endregion

					#region Destroy
					decQty = 0;
					drowData = dtbDestroy.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Destroy"]);
						}
						catch{}
						dr.Delete();
					}
					dtbDestroy.AcceptChanges();
					if (decQty > 0) drowReport["Destroy"] = decQty;
					#endregion

					#region Out QC
					decQty = 0;
					drowData = dtbOutQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutQC.AcceptChanges();
					if (decQty > 0) drowReport["OutQC"] = decQty;
					#endregion

					#region Return to vendor
					decQty = 0;
					drowData = dtbRTV.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RTV"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRTV.AcceptChanges();
					if (decQty > 0) drowReport["RTV"] = decQty;
					#endregion

					#region Out Misc
					decQty = 0;
					drowData = dtbOutMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutMisc.AcceptChanges();
					if (decQty > 0) drowReport["OutMisc"] = decQty;
					#endregion

					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#region look up destroy table
			foreach (DataRow drData in dtbDestroy.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbDestroy.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					DataRow[] drowData = null;
					
					#region Out QC
					decQty = 0;
					drowData = dtbOutQC.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutQC"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutQC.AcceptChanges();
					if (decQty > 0) drowReport["OutQC"] = decQty;
					#endregion

					#region Return to vendor
					decQty = 0;
					drowData = dtbRTV.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RTV"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRTV.AcceptChanges();
					if (decQty > 0) drowReport["RTV"] = decQty;
					#endregion

					#region Out Misc
					decQty = 0;
					drowData = dtbOutMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutMisc.AcceptChanges();
					if (decQty > 0) drowReport["OutMisc"] = decQty;
					#endregion

					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#region look up Out QC table
			foreach (DataRow drData in dtbOutQC.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbOutQC.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					DataRow[] drowData = null;
					
					#region Return to vendor
					decQty = 0;
					drowData = dtbRTV.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["RTV"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRTV.AcceptChanges();
					if (decQty > 0) drowReport["RTV"] = decQty;
					#endregion

					#region Out Misc
					decQty = 0;
					drowData = dtbOutMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutMisc.AcceptChanges();
					if (decQty > 0) drowReport["OutMisc"] = decQty;
					#endregion

					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#region look up return to vendor table
			foreach (DataRow drData in dtbReturnGoods.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbReturnGoods.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					DataRow[] drowData = null;
					
					#region Out Misc
					decQty = 0;
					drowData = dtbOutMisc.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutMisc"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutMisc.AcceptChanges();
					if (decQty > 0) drowReport["OutMisc"] = decQty;
					#endregion

					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#region look up out misc table
			foreach (DataRow drData in dtbOutMisc.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbOutMisc.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					DataRow[] drowData = null;
					
					#region Out Abnormal
					decQty = 0;
					drowData = dtbOutAbnormal.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["OutAbnormal"]);
						}
						catch{}
						dr.Delete();
					}
					dtbOutAbnormal.AcceptChanges();
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion

					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#region look up out abnormal table
			foreach (DataRow drData in dtbOutAbnormal.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					foreach (DataColumn dcol in dtbOutAbnormal.Columns)
						drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
					string strProductID = drData["ProductID"].ToString();
					string strBinID = drData["BinID"].ToString();
					string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

					decimal decQty = 0;
					try
					{
						decQty = Convert.ToDecimal(drowReport["OutAbnormal"]);
					}
					catch{}
					DataRow[] drowData = null;

					#region Inventory Adjustment
					drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity < 0");
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["AdjustQuantity"]);
						}
						catch{}
						dr.Delete();
					}
					dtbAdjustment.AcceptChanges();
					drowData = dtbRecover.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += -Convert.ToDecimal(dr["Recover"]);
						}
						catch{}
						dr.Delete();
					}
					dtbRecover.AcceptChanges();
					if (decQty > 0) drowReport["OutAbnormal"] = decQty;
					#endregion
					
					#region Shipping
					decQty = 0;
					drowData = dtbShipping.Select(strFilter);
					foreach (DataRow dr in drowData)
					{
						try
						{
							decQty += Convert.ToDecimal(dr["Shipping"]);
						}
						catch{}
						dr.Delete();
					}
					dtbShipping.AcceptChanges();
					if (decQty > 0) drowReport["Shipping"] = decQty;
					#endregion

					dtbReportData.Rows.Add(drowReport);
				}
			}
			if (dtbOutAbnormal.Rows.Count == 0)
			{
				DataRow[] drowsAdjust = dtbAdjustment.Select("AdjustQuantity < 0");
				foreach (DataRow drData in drowsAdjust)
				{
					if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
					{
						DataRow drowReport = dtbReportData.NewRow();
						foreach (DataColumn dcol in dtbAdjustment.Columns)
						{
							if (dcol.ColumnName != "AdjustQuantity")
								drowReport[dcol.ColumnName] = drData[dcol.ColumnName];
						}
						string strProductID = drData["ProductID"].ToString();
						string strBinID = drData["BinID"].ToString();
						string strFilter = "ProductID = " + strProductID + " AND BinID = " + strBinID;

						decimal decQty = 0;
						DataRow[] drowData = null;

						#region Inventory Adjustment
						drowData = dtbAdjustment.Select(strFilter + " AND AdjustQuantity > 0");
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += Convert.ToDecimal(dr["AdjustQuantity"]);
							}
							catch{}
							dr.Delete();
						}
						dtbAdjustment.AcceptChanges();
						if (decQty > 0) drowReport["InAbnormal"] = decQty;
						#endregion
					
						#region Shipping
						decQty = 0;
						drowData = dtbShipping.Select(strFilter);
						foreach (DataRow dr in drowData)
						{
							try
							{
								decQty += Convert.ToDecimal(dr["Shipping"]);
							}
							catch{}
							dr.Delete();
						}
						dtbShipping.AcceptChanges();
						if (decQty > 0) drowReport["Shipping"] = decQty;
						#endregion

						dtbReportData.Rows.Add(drowReport);
					}
				}
			}
			#endregion

			#region look up shipping table
			foreach (DataRow drData in dtbShipping.Rows)
			{
				if (drData.RowState != DataRowState.Deleted && drData.RowState != DataRowState.Detached)
				{
					DataRow drowReport = dtbReportData.NewRow();
					drowReport.ItemArray = drData.ItemArray;
					dtbReportData.Rows.Add(drowReport);
				}
			}
			#endregion

			#endregion

			#region calculate total in & out

			// calculate total in & out
			foreach (DataRow dr in dtbReportData.Rows)
			{
				decimal decTotalIn = 0, decTotalOut = 0, decEndQuantity = 0;
				decimal decBeginQty = 0, decWOQty = 0, decInPlan = 0, decRGR = 0, decPOReceipt = 0;
				decimal decInQC = 0, decRecycled = 0, decInMisc = 0, decInAbnormal = 0, decOutPlan = 0;
				decimal decDestroy = 0, decOutQC = 0, decRTV = 0, decOutMisc = 0, decOutAbnormal = 0, decShip = 0;
				try
				{
					decBeginQty = Convert.ToDecimal(dr["BeginQty"]);
				}
				catch{}
				try
				{
					decWOQty = Convert.ToDecimal(dr["WOQty"]);
				}
				catch{}
				try
				{
					decInPlan = Convert.ToDecimal(dr["InPlan"]);
				}
				catch{}
				try
				{
					decRGR = Convert.ToDecimal(dr["RGR"]);
				}
				catch{}
				try
				{
					decPOReceipt = Convert.ToDecimal(dr["Receipt"]);
				}
				catch{}
				try
				{
					decInQC = Convert.ToDecimal(dr["InQC"]);
				}
				catch{}
				try
				{
					decRecycled = Convert.ToDecimal(dr["Recycled"]);
				}
				catch{}
				try
				{
					decInMisc = Convert.ToDecimal(dr["InMisc"]);
				}
				catch{}
				try
				{
					decInAbnormal = Convert.ToDecimal(dr["InAbnormal"]);
				}
				catch{}
				try
				{
					decOutPlan = Convert.ToDecimal(dr["OutPlan"]);
				}
				catch{}
				try
				{
					decDestroy = Convert.ToDecimal(dr["Destroy"]);
				}
				catch{}
				try
				{
					decOutQC = Convert.ToDecimal(dr["OutQC"]);
				}
				catch{}
				try
				{
					decRTV = Convert.ToDecimal(dr["RTV"]);
				}
				catch{}
				try
				{
					decOutMisc = Convert.ToDecimal(dr["OutMisc"]);
				}
				catch{}
				try
				{
					decOutAbnormal = Convert.ToDecimal(dr["OutAbnormal"]);
				}
				catch{}
				try
				{
					decShip = Convert.ToDecimal(dr["Shipping"]);
				}
				catch{}
				decTotalIn = decWOQty + decInPlan + decRGR + decPOReceipt +
					decInQC + decRecycled + decInMisc + decInAbnormal;
				decTotalOut = decDestroy + decOutQC + decRTV + decOutMisc + decOutPlan + decOutAbnormal + decShip;
				decEndQuantity = decBeginQty + decTotalIn - decTotalOut;
				dr["TotalIn"] = decTotalIn;
				dr["TotalOut"] = decTotalOut;
				dr["EndQuantity"] = decEndQuantity;
			}

			#endregion

			C1Report rptReport = new C1Report();

			if (mLayoutFile == string.Empty)
				mLayoutFile = "BaoCaoTongHopSanXuat.xml";
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile)[0]);
			rptReport.Layout.PaperSize = PaperKind.A4;

			#region report parameter

			DateTime dtmDate = Convert.ToDateTime(pstrMonth);
			try
			{
				rptReport.Fields[MONTH_FLD].Text = dtmDate.ToString("MMMM-yyyy");
			}
			catch{}

			#region Department

			if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
			{
				string strCode = string.Empty;
				DataTable dtbData = GetDepartment(pstrDepartmentID);
				foreach (DataRow drowItem in dtbData.Rows)
					strCode += drowItem["Code"].ToString() + ", ";
				// remove the last ","
				if (strCode.IndexOf(",") >= 0)
					strCode = strCode.Substring(0, strCode.Length - 2);
				try
				{
					rptReport.Fields[DEPARTMENT_FLD].Text = strCode;
				}
				catch{}
			}

			#endregion

			#region ProductionLine

			if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
			{
				string strCode = string.Empty;
				DataTable dtbData = GetProductionLine(pstrProductionLineID);
				foreach (DataRow drowItem in dtbData.Rows)
					strCode += drowItem["Code"].ToString() + ", ";
				// remove the last ","
				if (strCode.IndexOf(",") >= 0)
					strCode = strCode.Substring(0, strCode.Length - 2);
				try
				{
					rptReport.Fields[PRODUCTIONLINE_FLD].Text = strCode;
				}
				catch{}
			}

			#endregion

			#region Location

			if (pstrLocationID != null && pstrLocationID.Length > 0)
			{
				string strCode = string.Empty;
				DataTable dtbData = GetLocation(pstrLocationID);
				foreach (DataRow drowItem in dtbData.Rows)
					strCode += drowItem["Code"].ToString() + ", ";
				// remove the last ","
				if (strCode.IndexOf(",") >= 0)
					strCode = strCode.Substring(0, strCode.Length - 2);
				try
				{
					rptReport.Fields[LOCATION_FLD].Text = strCode;
				}
				catch{}
			}

			#endregion

			#region Bin

			if (pstrBinID != null && pstrBinID.Length > 0)
			{
				string strCode = string.Empty;
				DataTable dtbData = GetBin(pstrBinID);
				foreach (DataRow drowItem in dtbData.Rows)
					strCode += drowItem["Code"].ToString() + ", ";
				// remove the last ","
				if (strCode.IndexOf(",") >= 0)
					strCode = strCode.Substring(0, strCode.Length - 2);
				try
				{
					rptReport.Fields[BIN_FLD].Text = strCode;
				}
				catch{}
			}

			#endregion
			#endregion

			// set datasource object that provides data to report.
			rptReport.DataSource.Recordset = dtbReportData;
			// render report
			rptReport.Render();

			// render the report into the PrintPreviewControl
			C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
			ppvViewer.FormTitle = "Bao Cao Tong Hop San Xuat " + dtmDate.ToString("MMMM-yyyy");
			ppvViewer.ReportViewer.Document = rptReport.Document;
			ppvViewer.Show();
			return dtbReportData;
		}
		private DataSet GetDataAndCache(DateTime pdtmMonth, string pstrDepartmentID, string pstrProductionLineID, string pstrLocationID, string pstrBinID, string pstrCategoryID, int pintMakeItem)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				#region Main Query

				string strSql = " DECLARE @CurrentMonth datetime,"
					+ "\n 	@EndMonth datetime,"
					+ "\n 	@PreviousMonth datetime"
					+ "\n SET @CurrentMonth = '" + pdtmMonth.ToString("yyyy-MM-dd") + "'"
					+ "\n SET @EndMonth = DATEADD(s, -1, DATEADD(m, 1, @CurrentMonth))"
					+ "\n SET @PreviousMonth = DATEADD(m, -1, @CurrentMonth)";

				#endregion

				#region BeginTable
				string BeginTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(ISNULL(OHQuantity,0)) BeginQty"
					+ "\n FROM IV_BalanceBin"
					+ "\n JOIN ITM_Product P ON IV_BalanceBin.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON IV_BalanceBin.BinID = B.BinID"
					+ "\n JOIN MST_Location L ON IV_BalanceBin.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE EffectDate = @PreviousMonth";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					BeginTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					BeginTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					BeginTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					BeginTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					BeginTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					BeginTable += " AND P.MakeItem = " + pintMakeItem;
				BeginTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				BeginTable += "\n HAVING SUM(ISNULL(OHQuantity,0)) > 0";
				BeginTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region WOComleptionTable
				string WOComleptionTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(CompletedQuantity) WOQty"
					+ "\n FROM PRO_WorkOrderCompletion"
					+ "\n JOIN ITM_Product P ON PRO_WorkOrderCompletion.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON PRO_WorkOrderCompletion.BinID = B.BinID"
					+ "\n JOIN MST_Location L ON PRO_WorkOrderCompletion.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					WOComleptionTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					WOComleptionTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					WOComleptionTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					WOComleptionTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					WOComleptionTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					WOComleptionTable += " AND P.MakeItem = " + pintMakeItem;
				WOComleptionTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				WOComleptionTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region InPlanTable
				string InPlanTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(CommitQuantity) InPlan"
					+ "\n FROM PRO_IssueMaterialMaster JOIN PRO_IssueMaterialDetail"
					+ "\n ON PRO_IssueMaterialMaster.IssueMaterialMasterID = PRO_IssueMaterialDetail.IssueMaterialMasterID"
					+ "\n JOIN ITM_Product P ON PRO_IssueMaterialDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON PRO_IssueMaterialMaster.ToBinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					InPlanTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					InPlanTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					InPlanTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					InPlanTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					InPlanTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pintMakeItem >= 0)
					InPlanTable += " AND P.MakeItem = " + pintMakeItem;
				InPlanTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				InPlanTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region ReturnGoodsTable
				string ReturnGoodsTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(ReceiveQuantity) RGR"
					+ "\n FROM SO_ReturnedGoodsMaster JOIN SO_ReturnedGoodsDetail"
					+ "\n ON SO_ReturnedGoodsMaster.ReturnedGoodsMasterID = SO_ReturnedGoodsDetail.ReturnedGoodsMasterID"
					+ "\n JOIN ITM_Product P ON SO_ReturnedGoodsDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON SO_ReturnedGoodsDetail.BinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					ReturnGoodsTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					ReturnGoodsTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					ReturnGoodsTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					ReturnGoodsTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					ReturnGoodsTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					ReturnGoodsTable += " AND P.MakeItem = " + pintMakeItem;
				ReturnGoodsTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				ReturnGoodsTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region POReceiptTable
				string POReceiptTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(ReceiveQuantity) Receipt"
					+ "\n FROM PO_PurchaseOrderReceiptMaster JOIN PO_PurchaseOrderReceiptDetail"
					+ "\n ON PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID"
					+ "\n JOIN ITM_Product P ON PO_PurchaseOrderReceiptDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON PO_PurchaseOrderReceiptDetail.BinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					POReceiptTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					POReceiptTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					POReceiptTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					POReceiptTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					POReceiptTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					POReceiptTable += " AND P.MakeItem = " + pintMakeItem;
				POReceiptTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				POReceiptTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region InQCTable
				string InQCTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(Quantity) InQC"
					+ "\n FROM IV_MiscellaneousIssueMaster JOIN IV_MiscellaneousIssueDetail"
					+ "\n ON IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID"
					+ "\n JOIN ITM_Product P ON IV_MiscellaneousIssueDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON IV_MiscellaneousIssueMaster.DesBinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth"
					+ "\n AND SourceLocationID in (148,250) /*Hardcode: 148 = QC-QC-WH Qualtity Control Warehouse*/"
					+ "\n AND DesLocationID not in (148,250) /*Hardcode: 148 = QC-QC-WH Qualtity Control Warehouse*/";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					InQCTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					InQCTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					InQCTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					InQCTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					InQCTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					InQCTable += " AND P.MakeItem = " + pintMakeItem;
				InQCTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				InQCTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region RecycledTable
				string RecycledTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(RecoverQuantity) Recycled"
					+ "\n FROM CST_RecoverMaterialMaster JOIN CST_RecoverMaterialDetail"
					+ "\n ON CST_RecoverMaterialMaster.RecoverMaterialMasterID = CST_RecoverMaterialDetail.RecoverMaterialMasterID"
					+ "\n JOIN ITM_Product P ON CST_RecoverMaterialDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON CST_RecoverMaterialDetail.ToBinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					RecycledTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					RecycledTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					RecycledTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					RecycledTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					RecycledTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					RecycledTable += " AND P.MakeItem = " + pintMakeItem;
				RecycledTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				RecycledTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region InMiscTable
				string InMiscTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(Quantity) InMisc"
					+ "\n FROM IV_MiscellaneousIssueMaster JOIN IV_MiscellaneousIssueDetail"
					+ "\n ON IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID"
					+ "\n JOIN ITM_Product P ON IV_MiscellaneousIssueDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON IV_MiscellaneousIssueMaster.DesBinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth"
					+ "\n AND (SourceLocationID not in (148,250)  OR (SourceLocationID = 148 AND DesLocationID = 148) OR (SourceLocationID = 250 AND DesLocationID = 250)) /*Hardcode: 148 = QC-QC-WH Qualtity Control Warehouse*/"
					+ "\n AND IssuePurposeID <> 2 /*HardCode: IssuePurposeID = 2: Xuat Bat Thuong*/";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					InMiscTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					InMiscTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					InMiscTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					InMiscTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					InMiscTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					InMiscTable += " AND P.MakeItem = " + pintMakeItem;
				InMiscTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				InMiscTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region InAbnormalTable
				string InAbnormalTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(Quantity) InAbnormal"
					+ "\n FROM IV_MiscellaneousIssueMaster JOIN IV_MiscellaneousIssueDetail"
					+ "\n ON IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID"
					+ "\n JOIN ITM_Product P ON IV_MiscellaneousIssueDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON IV_MiscellaneousIssueMaster.DesBinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth"
					+ "\n AND IssuePurposeID = 2 /*HardCode: IssuePurposeID = 2: Xuat Bat Thuong*/";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					InAbnormalTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					InAbnormalTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					InAbnormalTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					InAbnormalTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					InAbnormalTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					InAbnormalTable += " AND P.MakeItem = " + pintMakeItem;
				InAbnormalTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				InAbnormalTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region OutPlanTable
				string OutPlanTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(CommitQuantity) OutPlan"
					+ "\n FROM PRO_IssueMaterialMaster JOIN PRO_IssueMaterialDetail"
					+ "\n ON PRO_IssueMaterialMaster.IssueMaterialMasterID = PRO_IssueMaterialDetail.IssueMaterialMasterID"
					+ "\n JOIN ITM_Product P ON PRO_IssueMaterialDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON PRO_IssueMaterialDetail.BinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					OutPlanTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					OutPlanTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					OutPlanTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					OutPlanTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					OutPlanTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					OutPlanTable += " AND P.MakeItem = " + pintMakeItem;
				OutPlanTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				OutPlanTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region DestroyTable
				string DestroyTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(Quantity) Destroy"
					+ "\n FROM IV_MiscellaneousIssueMaster JOIN IV_MiscellaneousIssueDetail"
					+ "\n ON IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID"
					+ "\n JOIN ITM_Product P ON IV_MiscellaneousIssueDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON IV_MiscellaneousIssueMaster.SourceBinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth"
					+ "\n AND IssuePurposeID = 14 /*HardCode: IssuePurposeID = 14: Xuat Huy*/";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					DestroyTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					DestroyTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					DestroyTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					DestroyTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					DestroyTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					DestroyTable += " AND P.MakeItem = " + pintMakeItem;
				DestroyTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				DestroyTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region OutQCTable
				string OutQCTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(Quantity) OutQC"
					+ "\n FROM IV_MiscellaneousIssueMaster JOIN IV_MiscellaneousIssueDetail"
					+ "\n ON IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID"
					+ "\n JOIN ITM_Product P ON IV_MiscellaneousIssueDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON IV_MiscellaneousIssueMaster.SourceBinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth"
					+ "\n AND DesLocationID in (148,250) /*Hardcode: 148 = QC-QC-WH Qualtity Control Warehouse*/"
					+ "\n AND SourceLocationID not in (148,250) /*Hardcode: 148 = QC-QC-WH Qualtity Control Warehouse*/";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					OutQCTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					OutQCTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					OutQCTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					OutQCTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					OutQCTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					OutQCTable += " AND P.MakeItem = " + pintMakeItem;
				OutQCTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				OutQCTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region ReturnToVendorTable
				string ReturnToVendorTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(Quantity) RTV"
					+ "\n FROM PO_ReturnToVendorMaster JOIN PO_ReturnToVendorDetail"
					+ "\n ON PO_ReturnToVendorMaster.ReturnToVendorMasterID = PO_ReturnToVendorDetail.ReturnToVendorMasterID"
					+ "\n JOIN ITM_Product P ON PO_ReturnToVendorDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON PO_ReturnToVendorDetail.BinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					ReturnToVendorTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					ReturnToVendorTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					ReturnToVendorTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					ReturnToVendorTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					ReturnToVendorTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					ReturnToVendorTable += " AND P.MakeItem = " + pintMakeItem;
				ReturnToVendorTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				ReturnToVendorTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region OutMiscTable
				string OutMiscTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(Quantity) OutMisc"
					+ "\n FROM IV_MiscellaneousIssueMaster JOIN IV_MiscellaneousIssueDetail"
					+ "\n ON IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID"
					+ "\n JOIN ITM_Product P ON IV_MiscellaneousIssueDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON IV_MiscellaneousIssueMaster.SourceBinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth"
					+ "\n  AND (DesLocationID not in (148,250) OR (DesLocationID = 148 AND SourceLocationID = 148)OR (DesLocationID = 250 AND SourceLocationID = 250)) /*Hardcode: 148 = QC-QC-WH Qualtity Control Warehouse*/"
					+ "\n AND IssuePurposeID NOT IN (2, 14) /*HardCode: IssuePurposeID = 2: Xuat Bat Thuong, 14: Xuat Huy*/";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					OutMiscTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					OutMiscTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					OutMiscTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					OutMiscTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					OutMiscTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					OutMiscTable += " AND P.MakeItem = " + pintMakeItem;
				OutMiscTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				OutMiscTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region OutAbnormalTable
				string OutAbnormalTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(Quantity) OutAbnormal"
					+ "\n FROM IV_MiscellaneousIssueMaster JOIN IV_MiscellaneousIssueDetail"
					+ "\n ON IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID"
					+ "\n JOIN ITM_Product P ON IV_MiscellaneousIssueDetail.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON IV_MiscellaneousIssueMaster.SourceBinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth"
					+ "\n AND IssuePurposeID = 2 /*HardCode: IssuePurposeID = 2: Xuat Bat Thuong, 14: Xuat Huy*/";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					OutAbnormalTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					OutAbnormalTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					OutAbnormalTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					OutAbnormalTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					OutAbnormalTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					OutAbnormalTable += " AND P.MakeItem = " + pintMakeItem;
				OutAbnormalTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				OutAbnormalTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region ShippingTable
				string ShippingTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
                    + "\n SUM(ISNULL(SO_DeliverySchedule.DeliveryQuantity,0)) Shipping"
					+ "\n FROM SO_ConfirmShipMaster JOIN SO_ConfirmShipDetail"
					+ "\n ON SO_ConfirmShipMaster.ConfirmShipMasterID = SO_ConfirmShipDetail.ConfirmShipMasterID"
					+ "\n JOIN SO_DeliverySchedule ON SO_ConfirmShipDetail.DeliveryScheduleID = SO_DeliverySchedule.DeliveryScheduleID"
					+ "\n JOIN ITM_Product P ON SO_ConfirmShipDetail.ProductID = P.ProductID"
                    + "\n JOIN MST_Bin B ON SO_ConfirmShipMaster.BinID = B.BinID"
                    + "\n JOIN MST_Location L ON SO_ConfirmShipMaster.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE ShippedDate BETWEEN @CurrentMonth AND @EndMonth";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					ShippingTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					ShippingTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					ShippingTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					ShippingTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					ShippingTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					ShippingTable += " AND P.MakeItem = " + pintMakeItem;
				ShippingTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				ShippingTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region AdjustmentTable
				string AdjustmentTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n AdjustQuantity"
					+ "\n FROM IV_Adjustment"
					+ "\n JOIN ITM_Product P ON IV_Adjustment.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON IV_Adjustment.BinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					AdjustmentTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					AdjustmentTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					AdjustmentTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					AdjustmentTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					AdjustmentTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					AdjustmentTable += " AND P.MakeItem = " + pintMakeItem;
				AdjustmentTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region TransactionHistoryTable (Outside + WO Completion)
				string TransactionHistoryTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(-Quantity) Quantity"
					+ "\n FROM MST_TransactionHistory"
					+ "\n JOIN ITM_Product P ON MST_TransactionHistory.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON MST_TransactionHistory.BinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth"
					+ "\n AND TranTypeID IN (11, 19)" // 11 = PO Receipt By Outside, 19 = WO Completion
					+ "\n AND Quantity < 0";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					TransactionHistoryTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					TransactionHistoryTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					TransactionHistoryTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					TransactionHistoryTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					TransactionHistoryTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					TransactionHistoryTable += " AND P.MakeItem = " + pintMakeItem;
				TransactionHistoryTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				TransactionHistoryTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				#region Recover Material
				string RecoverTable = "\n SELECT D.Code Department, D.DepartmentID, L.Code Location, L.LocationID, B.Code Bin, B.BinID,"
					+ "\n C.Code Category, P.Code PartNo, P.Description PartName, P.Revision Model, P.ProductID,"
					+ "\n V.Code Vendor, S.Code Source,"
					+ "\n SUM(Quantity) Recover"
					+ "\n FROM CST_RecoverMaterialMaster"
					+ "\n JOIN ITM_Product P ON CST_RecoverMaterialMaster.ProductID = P.ProductID"
					+ "\n JOIN MST_Bin B ON CST_RecoverMaterialMaster.FromBinID = B.BinID"
					+ "\n JOIN MST_Location L ON B.LocationID = L.LocationID"
					+ "\n LEFT JOIN MST_Department D ON L.DepartmentID = D.DepartmentID"
					+ "\n LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ "\n LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ "\n LEFT JOIN ITM_Source S ON P.SourceID = S.SourceID"
					+ "\n WHERE PostDate BETWEEN @CurrentMonth AND @EndMonth";
				if (pstrDepartmentID != null && pstrDepartmentID.Length > 0)
					RecoverTable += "\n AND D.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrLocationID != null && pstrLocationID.Length > 0)
					RecoverTable += "\n AND L.LocationID IN (" + pstrLocationID + ")";
				if (pstrBinID != null && pstrBinID.Length > 0)
					RecoverTable += "\n AND B.BinID IN (" + pstrBinID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Length > 0)
					RecoverTable += "\n AND P.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					RecoverTable += "\n AND P.CategoryID IN (" + pstrCategoryID + ")";
				if (pintMakeItem >= 0)
					RecoverTable += " AND P.MakeItem = " + pintMakeItem;
				RecoverTable += "\n GROUP BY D.Code, D.DepartmentID, L.Code, L.LocationID, B.Code, B.BinID,"
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description, P.ProductID";
				RecoverTable += "\n ORDER BY D.Code, L.Code, B.Code, "
					+ " C.Code, V.Code, S.Code, P.Revision, P.Code, P.Description";
				#endregion

				strSql += BeginTable + "\n"
					+ WOComleptionTable + "\n"
					+ InPlanTable + "\n"
					+ ReturnGoodsTable + "\n"
					+ POReceiptTable + "\n"
					+ InQCTable + "\n"
					+ RecycledTable + "\n"
					+ InMiscTable + "\n"
					+ InAbnormalTable + "\n"
					+ OutPlanTable + "\n"
					+ DestroyTable + "\n"
					+ OutQCTable + "\n"
					+ ReturnToVendorTable + "\n"
					+ OutMiscTable + "\n"
					+ OutAbnormalTable + "\n"
					+ ShippingTable + "\n"
					+ AdjustmentTable + "\n"
					+ TransactionHistoryTable + "\n"
					+ RecoverTable;
				Debug.WriteLine(strSql);
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				
				DataSet dstData = new DataSet();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstData);

				dstData.Tables[0].TableName = BeginTableName;
				dstData.Tables[1].TableName = WOComleptionTableName;
				dstData.Tables[2].TableName = InPlanTableName;
				dstData.Tables[3].TableName = ReturnGoodsTableName;
				dstData.Tables[4].TableName = POReceiptTableName;
				dstData.Tables[5].TableName = InQCTableName;
				dstData.Tables[6].TableName = RecycledTableName;
				dstData.Tables[7].TableName = InMiscTableName;
				dstData.Tables[8].TableName = InAbnormalTableName;
				dstData.Tables[9].TableName = OutPlanTableName;
				dstData.Tables[10].TableName = DestroyTableName;
				dstData.Tables[11].TableName = OutQCTableName;
				dstData.Tables[12].TableName = ReturnToVendorTableName;
				dstData.Tables[13].TableName = OutMiscTableName;
				dstData.Tables[14].TableName = OutAbnormalTableName;
				dstData.Tables[15].TableName = ShippingTableName;
				dstData.Tables[16].TableName = AdjustmentTableName;
				dstData.Tables[17].TableName = TransactionHistoryTableName;
				dstData.Tables[18].TableName = RecoverTableName;
				
				return dstData;
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
		private DataTable GetDepartment(string pstrID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code FROM MST_Department WHERE DepartmentID IN (" + pstrID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmdData);
				odad.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DataTable GetProductionLine(string pstrID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code FROM PRO_ProductionLine WHERE ProductionLineID IN (" + pstrID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmdData);
				odad.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DataTable GetLocation(string pstrID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code FROM MST_Location WHERE LocationID IN (" + pstrID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmdData);
				odad.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DataTable GetBin(string pstrID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code FROM MST_Bin WHERE BinID IN (" + pstrID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmdData);
				odad.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DataTable MakeTable()
		{
			DataTable dtbData = new DataTable();
			dtbData.Columns.Add("Department", typeof(string));
			dtbData.Columns.Add("DepartmentID", typeof(int));
			dtbData.Columns.Add("Location", typeof(string));
			dtbData.Columns.Add("LocationID", typeof(int));
			dtbData.Columns.Add("Bin", typeof(string));
			dtbData.Columns.Add("BinID", typeof(int));
			dtbData.Columns.Add("Category", typeof(string));
			dtbData.Columns.Add("PartNo", typeof(string));
			dtbData.Columns.Add("PartName", typeof(string));
			dtbData.Columns.Add("Model", typeof(string));
			dtbData.Columns.Add("ProductID", typeof(int));
			dtbData.Columns.Add("Vendor", typeof(string));
			dtbData.Columns.Add("Source", typeof(string));
			dtbData.Columns.Add("BeginQty", typeof(decimal));
			dtbData.Columns.Add("WOQty", typeof(decimal));
			dtbData.Columns.Add("InPlan", typeof(decimal));
			dtbData.Columns.Add("RGR", typeof(decimal));
			dtbData.Columns.Add("Receipt", typeof(decimal));
			dtbData.Columns.Add("InQC", typeof(decimal));
			dtbData.Columns.Add("Recycled", typeof(decimal));
			dtbData.Columns.Add("InMisc", typeof(decimal));
			dtbData.Columns.Add("InAbnormal", typeof(decimal));
			dtbData.Columns.Add("TotalIn", typeof(decimal));
			dtbData.Columns.Add("OutPlan", typeof(decimal));
			dtbData.Columns.Add("Destroy", typeof(decimal));
			dtbData.Columns.Add("OutQC", typeof(decimal));
			dtbData.Columns.Add("RTV", typeof(decimal));
			dtbData.Columns.Add("OutMisc", typeof(decimal));
			dtbData.Columns.Add("OutAbnormal", typeof(decimal));
			dtbData.Columns.Add("Shipping", typeof(decimal));
			dtbData.Columns.Add("TotalOut", typeof(decimal));
			dtbData.Columns.Add("EndQuantity", typeof(decimal));
			return dtbData;
		}
	}
}