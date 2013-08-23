using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Reflection;
using System.Text;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSComUtils.Common;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace StdActCostComparisionBOD
{
	/// <summary>
	/// Compare Standard and Actual Cost
	/// </summary>
	[Serializable]
	public class StdActCostComparisionBOD : MarshalByRefObject, IDynamicReport
	{
		public StdActCostComparisionBOD()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IDynamicReport Members

		private ReportBuilder mReportBuilder;
		public ReportBuilder PCSReportBuilder
		{
			get
			{
				return mReportBuilder;
			}
			set
			{
				mReportBuilder = value;
			}
		}

		private string mConnectionString = string.Empty;
		public string PCSConnectionString
		{
			get
			{
				return mConnectionString;
			}
			set
			{
				mConnectionString = value;
			}
		}

		private string mLayoutFile = string.Empty;
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

		private object mResult;
		public object Result
		{
			get
			{
				return mResult;
			}
			set
			{
				mResult = value;
			}
		}

		private string mDefFolder = string.Empty;
		public string ReportDefinitionFolder
		{
			get
			{
				return mDefFolder;
			}
			set
			{
				mDefFolder = value;
			}
		}

		private bool mUseEngine;
		public bool UseReportViewerRenderEngine
		{
			get
			{
				return mUseEngine;
			}
			set
			{
				mUseEngine = value;
			}
		}

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		private C1PrintPreviewControl mPreview;
		public C1PrintPreviewControl PCSReportViewer
		{
			get
			{
				return mPreview;
			}
			set
			{
				mPreview = value;
			}
		}

		#endregion

		#region constants
		private const string CATEGORY_FLD = "Category";
		private const string MODEL_FLD = "Model";
		private const string PRODUCTID_FLD = "ProductID";
		private const string PART_NO_FLD = "PartNo";
		private const string PART_NAME_FLD = "PartName";
		private const string UM_FLD = "UM";
		private const string COST_ELEMENT_FLD = "CostElement";
		private const string STDCOST_FLD = "Std";
		private const string ACTUAL_COST_FLD = "Actual";
		private const string DIFFERENCE_FLD = "Diff";
		private const string FROMDATE_FLD = "FromDate";
		private const string TODATE_FLD = "ToDate";
		private const string PERIODID_FLD = "ActCostAllocationMasterID";
		#endregion

		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrCategoryID, string pstrDepartmentID,
			string pstrProductionLineID, string pstrModel, string pstrProductID)
		{
			#region create table schema

			DataTable dtbData = new DataTable();
			dtbData.Columns.Add(new DataColumn(CATEGORY_FLD, typeof(string)));
			dtbData.Columns.Add(new DataColumn(MODEL_FLD, typeof(string)));
			dtbData.Columns.Add(new DataColumn(PART_NO_FLD, typeof(string)));
			dtbData.Columns.Add(new DataColumn(PART_NAME_FLD, typeof(string)));
			dtbData.Columns.Add(new DataColumn(UM_FLD, typeof(string)));
			dtbData.Columns.Add(new DataColumn(COST_ELEMENT_FLD, typeof(string)));
			dtbData.Columns.Add(new DataColumn(PRODUCTID_FLD, typeof(string)));
			// create 3 columns for each month
			for (int i = 1; i <= 12; i++)
			{
				dtbData.Columns.Add(new DataColumn(STDCOST_FLD + i.ToString("00"), typeof(decimal)));
				dtbData.Columns.Add(new DataColumn(ACTUAL_COST_FLD + i.ToString("00"), typeof(decimal)));
				dtbData.Columns.Add(new DataColumn(DIFFERENCE_FLD + i.ToString("00"), typeof(decimal)));
			}

			#endregion

			// list all periods
			DataTable dtbAllPeriod = GetAllPeriod(pstrCCNID);
			// list all elements
			DataTable dtbElements = GetCostElements();
			// list all products
			DataTable dtbAllItems = ListItems(pstrCCNID);
			// build the periods id which pass thru selected year
			StringBuilder sbPeriods = new StringBuilder();
			sbPeriods.Append("0");
			foreach (DataRow drowData in dtbAllPeriod.Rows)
			{
				DateTime dtmFromDate = DateTime.Parse(drowData[FROMDATE_FLD].ToString());
				DateTime dtmToDate = DateTime.Parse(drowData[TODATE_FLD].ToString());
				for (DateTime dtmDate = dtmFromDate; dtmDate <= dtmToDate; dtmDate = dtmDate.AddYears(1))
				{
					if (dtmDate.Year == int.Parse(pstrYear))
					{
						sbPeriods.Append("," + drowData[PERIODID_FLD].ToString());
						break;
					}
				}
			}
			// gets temp report data
			DataTable dtbReportData = GetReportData(pstrCCNID, sbPeriods.ToString(), pstrCategoryID, pstrDepartmentID, pstrProductionLineID, pstrProductID, pstrModel);
			// standard cost
			DataTable dtbSTDCost = GetSTDCost();
			// get list of product in report data
			ArrayList arrProducts = new ArrayList();
			foreach (DataRow drowData in dtbReportData.Rows)
			{
				string strProductID = drowData[PRODUCTID_FLD].ToString();
				if (!arrProducts.Contains(strProductID))
					arrProducts.Add(strProductID);
			}
			// loops thru the product list to build original data
			foreach (string strProductID in arrProducts)
			{
				DataRow drowProductInfo = GetItemInfo(strProductID, dtbAllItems);
				foreach (DataRow drowElement in dtbElements.Rows)
				{
					// each element will appears as one row in report
					DataRow drowReport = dtbData.NewRow();
					drowReport[PRODUCTID_FLD] = strProductID;
					string strElementID = drowElement["CostElementID"].ToString();
					for (int i = 1; i <= 12; i++)
					{
						DateTime dtmMonth = new DateTime(Convert.ToInt32(pstrYear), i, 1);
						// find the period if of current month
						string strCurrentPeriodID = FindPeriodOfMonth(dtmMonth, dtbAllPeriod);
						// current month does not have any period
						if (strCurrentPeriodID == string.Empty)
						{
							drowReport[CATEGORY_FLD] = drowProductInfo[CATEGORY_FLD];
							drowReport[MODEL_FLD] = drowProductInfo[MODEL_FLD];
							drowReport[PART_NO_FLD] = drowProductInfo[PART_NO_FLD];
							drowReport[PART_NAME_FLD] = drowProductInfo[PART_NAME_FLD];
							drowReport[UM_FLD] = drowProductInfo[UM_FLD];
							drowReport[COST_ELEMENT_FLD] = drowElement["Code"];

							#region standard cost

							decimal decSTDCost = 0;
							string strFilterSTD = "ProductID = " + strProductID + " AND CostElementID = " + strElementID;
							try
							{
								decSTDCost = Convert.ToDecimal(dtbSTDCost.Compute("SUM(Cost)", strFilterSTD));
							}
							catch{}
							if (decSTDCost != 0)
							{
								drowReport[STDCOST_FLD + i.ToString("00")] = decSTDCost;
								drowReport[DIFFERENCE_FLD + i.ToString("00")] = decSTDCost;
							}
							else
							{
								drowReport[STDCOST_FLD + i.ToString("00")] = DBNull.Value;
								drowReport[DIFFERENCE_FLD + i.ToString("00")] = DBNull.Value;
							}

							#endregion

							drowReport[ACTUAL_COST_FLD + i.ToString("00")] = DBNull.Value;
							drowReport[DIFFERENCE_FLD + i.ToString("00")] = DBNull.Value;
						}
						else
						{
							string strFilter = PRODUCTID_FLD + "='" + strProductID + "'"
							                   + " AND ActCostAllocationMasterID ='" + strCurrentPeriodID + "'"
							                   + " AND CostElementID = '" + strElementID + "'";
							DataRow[] drowsElement = dtbReportData.Select(strFilter);
							if (drowsElement.Length > 0)
							{
								drowReport[CATEGORY_FLD] = drowsElement[0][CATEGORY_FLD];
								drowReport[MODEL_FLD] = drowsElement[0][MODEL_FLD];
								drowReport[PART_NO_FLD] = drowsElement[0][PART_NO_FLD];
								drowReport[PART_NAME_FLD] = drowsElement[0][PART_NAME_FLD];
								drowReport[UM_FLD] = drowsElement[0][UM_FLD];
								drowReport[COST_ELEMENT_FLD] = drowsElement[0][COST_ELEMENT_FLD];
								decimal decSTDCost = 0, decActualCost = 0, decQuantity = 0;
								decimal decBeginQuantity = 0, decPreviousActCost = 0;
								
								#region standard cost

								string strFilterSTD = "ProductID = " + strProductID + " AND CostElementID = " + strElementID;
								try
								{
									decSTDCost = Convert.ToDecimal(dtbSTDCost.Compute("SUM(Cost)", strFilterSTD));
								}
								catch{}
								if (decSTDCost != 0)
								{
									drowReport[STDCOST_FLD + i.ToString("00")] = decSTDCost;
									drowReport[DIFFERENCE_FLD + i.ToString("00")] = decSTDCost;
								}
								else
								{
									drowReport[STDCOST_FLD + i.ToString("00")] = DBNull.Value;
									drowReport[DIFFERENCE_FLD + i.ToString("00")] = DBNull.Value;
								}

								#endregion

								#region Calculate actual cost

								// quantity of current period
								try
								{
									decQuantity = Convert.ToDecimal(drowsElement[0]["Quantity"]);
								}
								catch{}
								// begin quantity of current period
								try
								{
									decBeginQuantity = Convert.ToDecimal(drowsElement[0]["BeginQuantity"]);
								}
								catch{}
								
								// actual cost of current preiod
								try
								{
									decActualCost = Convert.ToDecimal(drowsElement[0]["ActualCost"]);
								}
								catch{}
								
								#region begin actual cost
								try
								{
									decPreviousActCost = Convert.ToDecimal(drowsElement[0]["BeginCost"]);
								}
								catch{}
								#endregion
										
								// =(CST_ActualCostHistory.ElementType.ActualCost* CST_ActualCostHistory.Qty
								// - CST_ActualCostHistory.BeginCost* CST_ActualCostHistory.BeginQty ) /
								// (CST_ActualCostHistory.Qty- CST_ActualCostHistory.BeginQty)
								try
								{
									decActualCost = (decActualCost*decQuantity - decPreviousActCost*decBeginQuantity)/
										(decQuantity - decBeginQuantity);
								}
								catch (DivideByZeroException)
								{
									decActualCost = 0;
								}
								if (decQuantity == 0)
									decActualCost = 0;
								
								#endregion
							
								if (decActualCost != 0)
									drowReport[ACTUAL_COST_FLD + i.ToString("00")] = decActualCost;
								else
									drowReport[ACTUAL_COST_FLD + i.ToString("00")] = DBNull.Value;
								if ((decSTDCost - decActualCost) != 0)
									drowReport[DIFFERENCE_FLD + i.ToString("00")] = decSTDCost - decActualCost;
								else
									drowReport[DIFFERENCE_FLD + i.ToString("00")] = DBNull.Value;
							}
							else
							{
								drowReport[CATEGORY_FLD] = drowProductInfo[CATEGORY_FLD];
								drowReport[MODEL_FLD] = drowProductInfo[MODEL_FLD];
								drowReport[PART_NO_FLD] = drowProductInfo[PART_NO_FLD];
								drowReport[PART_NAME_FLD] = drowProductInfo[PART_NAME_FLD];
								drowReport[UM_FLD] = drowProductInfo[UM_FLD];
								drowReport[COST_ELEMENT_FLD] = drowElement["Code"];

								#region standard cost

								decimal decSTDCost = 0;
								string strFilterSTD = "ProductID = " + strProductID + " AND CostElementID = " + strElementID;
								try
								{
									decSTDCost = Convert.ToDecimal(dtbSTDCost.Compute("SUM(Cost)", strFilterSTD));
								}
								catch{}
								if (decSTDCost != 0)
								{
									drowReport[STDCOST_FLD + i.ToString("00")] = decSTDCost;
									drowReport[DIFFERENCE_FLD + i.ToString("00")] = decSTDCost;
								}
								else
								{
									drowReport[STDCOST_FLD + i.ToString("00")] = DBNull.Value;
									drowReport[DIFFERENCE_FLD + i.ToString("00")] = DBNull.Value;
								}

								#endregion

								drowReport[ACTUAL_COST_FLD + i.ToString("00")] = DBNull.Value;
							}
						}
					} // loop year
					// insert to report data
					dtbData.Rows.Add(drowReport);
				} // loop elements
			} //loop products

			// report layout
			C1Report rptReport = new C1Report();
			if (mLayoutFile == null || mLayoutFile.Trim() == string.Empty)
				mLayoutFile = "StdActCostComparisionBOD.xml";
			string[] arrstrReportInDefinitionFile = rptReport.GetReportInfo(mDefFolder + "\\" + mLayoutFile);
			rptReport.Load(mDefFolder + "\\" + mLayoutFile, arrstrReportInDefinitionFile[0]);
			//arrstrReportInDefinitionFile = null;
			rptReport.Layout.PaperSize = PaperKind.A3;

			#region PUSH PARAMETER VALUE

			#region General information

			try
			{
				rptReport.Fields["fldCompany"].Text = SystemProperty.SytemParams.Get("CompanyFullName");
			}
			catch{}
			try
			{
				rptReport.Fields["fldCCN"].Text		= GetCCNCode(pstrCCNID);;
			}
			catch{}
			try
			{
				rptReport.Fields["fldYear"].Text = pstrYear;
			}
			catch{}

			#endregion

			#region Category

			if (pstrCategoryID != null && pstrCategoryID.Trim().Length > 0)
			{
				try
				{
					rptReport.Fields["fldCategoryParam"].Text = GetCategory(pstrCategoryID);
				}
				catch{}
			}
			
			#endregion

			#region Department

			if (pstrDepartmentID != null && pstrDepartmentID.Trim().Length > 0)
			{
				try
				{
					rptReport.Fields["fldDepartmentParam"].Text = GetDepartment(pstrDepartmentID);
				}
				catch{}
			}
			
			#endregion

			#region Production Line

			if (pstrProductionLineID != null && pstrProductionLineID.Trim().Length > 0)
			{
				try
				{
					rptReport.Fields["fldProductionLine"].Text = GetProductionLineCode(pstrProductionLineID);
				}
				catch{}
			}
			
			#endregion

			#region Part

			if (pstrProductID != null && pstrProductID.Trim().Length > 0)
			{
				try
				{
					string strPartNo = string.Empty, strPartName = string.Empty;
					DataRow[] drowItems = GetItemsInfo(pstrProductID, dtbAllItems);
					foreach (DataRow drowItem in drowItems)
					{
						strPartNo += drowItem[PART_NO_FLD].ToString() + ",";
						strPartName += drowItem[PART_NAME_FLD].ToString() + ",";
					}
					// remove the last ","
					if (strPartNo.IndexOf(",") >= 0)
						strPartNo = strPartNo.Substring(0, strPartNo.Length - 1);
					if (strPartName.IndexOf(",") >= 0)
						strPartName = strPartName.Substring(0, strPartName.Length - 1);
					rptReport.Fields["fldPartNoParam"].Text = strPartNo;
					rptReport.Fields["fldPartNameParam"].Text = strPartName;
				}
				catch{}
			}
			
			#endregion

			#region Model

			if (pstrModel != null && pstrModel.Trim().Length > 0)
			{
				try
				{
					// refine Model string
					pstrModel = pstrModel.Replace("'", string.Empty);
					rptReport.Fields["fldModelParam"].Text = pstrModel;
				}
				catch{}
			}
			
			#endregion
			
			#region Home Currency

			try
			{
				rptReport.Fields["lblCurrency"].Text = rptReport.Fields["lblCurrency"].Text + GetHomeCurrency(pstrCCNID);
			}
			catch{}
			
			#endregion

			#endregion

			// set datasource object that provides data to report.
			rptReport.DataSource.Recordset = dtbData;
			// render report
			rptReport.Render();

			// render the report into the PrintPreviewControl
			C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
			ppvViewer.FormTitle = " STANDARD COST & ACTUAL COST COMPARISON - BOD";
			
			ppvViewer.ReportViewer.Document = rptReport.Document;
			ppvViewer.Show();
			return dtbData;
		}

		private DataTable GetReportData(string pstrCCNID, string pstrPeriodIDs, string pstrCategoryID, string pstrDepartmentID,
			string pstrProductionLineID, string pstrProductID, string pstrModel)
		{
			OleDbConnection cn = null;
			OleDbCommand cmd = null;
			try
			{
				cn = new OleDbConnection(mConnectionString);
				string strSql = "SELECT	cst_ActCostAllocationMaster.FromDate, cst_ActCostAllocationMaster.ToDate,"
					+ " ITM_Category.Code AS Category, ITM_Product.Revision AS Model, ITM_Product.Code AS PartNo, ITM_Product.Description AS PartName,"
					+ " MST_UnitOfMeasure.Code AS UM, ActualCost, StdCost, STD_CostElement.Name AS CostElement,"
					+ " CST_ActualCostHistory.ProductID, CST_ActualCostHistory.CostElementID,"
					+ " ISNULL(CST_ActualCostHistory.Quantity, 0) AS Quantity, "
					+ " ISNULL(CST_ActualCostHistory.BeginQuantity, 0) AS BeginQuantity,"
					+ " ISNULL(CST_ActualCostHistory.BeginCost, 0) AS BeginCost,"
					+ " CST_ActualCostHistory.ActCostAllocationMasterID, ComponentValue, ComponentDSAmount"
					+ " FROM CST_ActualCostHistory JOIN cst_ActCostAllocationMaster"
					+ " ON CST_ActualCostHistory.ActCostAllocationMasterID = cst_ActCostAllocationMaster.ActCostAllocationMasterID"
					+ " JOIN STD_CostElement"
					+ " ON CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID"
					+ " JOIN ITM_Product"
					+ " ON CST_ActualCostHistory.ProductID = ITM_Product.ProductID"
					+ " JOIN MST_UnitOfMeasure"
					+ " ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID"
					+ " LEFT JOIN ITM_Category"
					+ " ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " LEFT JOIN PRO_ProductionLine"
					+ " ON ITM_Product.ProductionLineID = PRO_ProductionLine.ProductionLineID"
					+ " WHERE cst_ActCostAllocationMaster.CCNID = " + pstrCCNID
					+ " AND CST_ActualCostHistory.ActCostAllocationMasterID IN (" + pstrPeriodIDs + ")";
				if (pstrCategoryID != null && pstrCategoryID.Trim() != string.Empty)
					strSql += " AND ITM_Product.CategoryID IN (" + pstrCategoryID + ")" ;
				if (pstrDepartmentID != null && pstrDepartmentID.Trim() != string.Empty)
					strSql += " AND PRO_ProductionLine.DepartmentID IN (" + pstrDepartmentID + ")";
				if (pstrProductionLineID != null && pstrProductionLineID.Trim() != string.Empty)
					strSql += " AND ITM_Product.ProductionLineID IN (" + pstrProductionLineID + ")";
				if (pstrProductID != null && pstrProductID.Trim() != string.Empty)
					strSql += " AND CST_ActualCostHistory.ProductID IN (" + pstrProductID + ")";
				if (pstrModel != null && pstrModel.Trim() != string.Empty)
					strSql += " AND ITM_Product.Revision IN ( " + pstrModel + " )";
				strSql += " ORDER BY CST_ActualCostHistory.ProductID ASC, STD_CostElement.OrderNo ASC";
				
				cmd = new OleDbCommand(strSql, cn);
				
				cmd.Connection.Open();

				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmd);
				odad.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (cn != null)
					if (cn.State != ConnectionState.Closed)
						cn.Close();
			}
		}

		private DataTable GetSTDCost()
		{
			DataTable dtbData = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT * FROM CST_STDItemCost";

				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				return dtbData;
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		/// <summary>
		/// Gets all periods of CCN
		/// </summary>
		/// <param name="pstrCCNID">CCN</param>
		/// <returns>All periods</returns>
		private DataTable GetAllPeriod(string pstrCCNID)
		{
			OleDbConnection cn = null;
			OleDbCommand cmd = null;
			try
			{
				cn = new OleDbConnection(mConnectionString);
				string strSql = "SELECT * FROM cst_ActCostAllocationMaster"
				                + " WHERE cst_ActCostAllocationMaster.CCNID = " + pstrCCNID
				                + " ORDER BY FromDate ASC";

				cmd = new OleDbCommand(strSql, cn);

				cmd.Connection.Open();

				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmd);
				odad.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (cn != null)
					if (cn.State != ConnectionState.Closed)
						cn.Close();
			}
		}
		private DataTable GetCostElements()
		{
			DataTable dtbData = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT STD_CostElement.*, STD_CostElementType.Code AS TypeCode"
					+ " FROM STD_CostElement JOIN STD_CostElementType"
					+ " ON STD_CostElement.CostElementTypeID = STD_CostElementType.CostElementTypeID"
					+ " WHERE IsLeaf = 1"
					+ " ORDER BY OrderNo ASC";

				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				return dtbData;
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		private DataTable ListItems(string pstrCCNID)
		{
			DataTable dtbData = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT ITM_Category.Code AS Category, ITM_Product.Revision AS Model,"
					+ " ITM_Product.Code AS PartNo, ITM_Product.Description AS PartName,"
					+ " MST_UnitOfMeasure.Code AS UM, ITM_Product.ProductID, ITM_Product.CostCenterRateMasterID"
					+ " FROM ITM_Product JOIN MST_UnitOfMeasure"
					+ " ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID"
					+ " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " WHERE ITM_Product.CCNID = " + pstrCCNID;

				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				return dtbData;
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		private DataRow GetItemInfo(string pstrProductID, DataTable pdtbAllItems)
		{
			return pdtbAllItems.Select(PRODUCTID_FLD + "='" + pstrProductID + "'")[0];
		}
		private DataRow[] GetItemsInfo(string pstrProductIDs, DataTable pdtbAllItems)
		{
			return pdtbAllItems.Select(PRODUCTID_FLD + " IN (" + pstrProductIDs + ")");
		}
		/// <summary>
		/// Get CCN Code from ID
		/// </summary>
		/// <param name="pstrCCNID">CCN ID</param>
		/// <returns>CCN Code</returns>
		private string GetCCNCode(string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT	Code + ' (' + Description + ')' FROM MST_CCN WHERE CCNID = " + pstrCCNID;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				object objResult = cmdData.ExecuteScalar();
				try
				{
					return objResult.ToString();
				}
				catch
				{
					return string.Empty;
				}
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Get Production Line Code and Name from ID
		/// </summary>
		/// <param name="pstrProID">Production Line ID</param>
		/// <returns>Pro Code (Pro Name)</returns>
		private string GetProductionLineCode(string pstrProID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code + ' (' + Name + ')' AS 'Code' FROM PRO_ProductionLine WHERE ProductionLineID IN (" + pstrProID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmdData);
				odad.Fill(dtbData);
				string strCode = string.Empty;
				foreach (DataRow drowData in dtbData.Rows)
					strCode += drowData["Code"].ToString() + ",";
				if (strCode.IndexOf(",") >= 0)
					strCode = strCode.Substring(0, strCode.Length - 1);
				return strCode;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private string GetDepartment(string pstrProID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code + ' (' + Name + ')' AS 'Code' FROM MST_Department WHERE DepartmentID IN (" + pstrProID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmdData);
				odad.Fill(dtbData);
				string strCode = string.Empty;
				foreach (DataRow drowData in dtbData.Rows)
					strCode += drowData["Code"].ToString() + ",";
				if (strCode.IndexOf(",") >= 0)
					strCode = strCode.Substring(0, strCode.Length - 1);
				return strCode;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private string GetCategory(string pstrProID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code + ' (' + Name + ')' AS 'Code' FROM ITM_Category WHERE CategoryID IN (" + pstrProID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmdData);
				odad.Fill(dtbData);
				string strCode = string.Empty;
				foreach (DataRow drowData in dtbData.Rows)
					strCode += drowData["Code"].ToString() + ",";
				if (strCode.IndexOf(",") >= 0)
					strCode = strCode.Substring(0, strCode.Length - 1);
				return strCode;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private string GetHomeCurrency(string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code FROM MST_Currency WHERE CurrencyID = (SELECT HomeCurrencyID FROM MST_CCN WHERE CCNID = " + pstrCCNID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				object objResult = cmdData.ExecuteScalar();
				if (objResult != null && objResult != DBNull.Value)
					return objResult.ToString();
				else
					return string.Empty;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private string FindPeriodOfMonth(DateTime pdtmMonth, DataTable pdtbAllPeriod)
		{
			DataRow[] drowCurrentPeriod = pdtbAllPeriod.Select("FromDate <='" + pdtmMonth.ToString() + "'"
				+ " AND ToDate >='" + pdtmMonth.ToString() + "'");
			string strPeriodID = string.Empty;
			if (drowCurrentPeriod.Length > 0)
				strPeriodID = drowCurrentPeriod[0]["ActCostAllocationMasterID"].ToString();
			return strPeriodID;
		}
	}
}
