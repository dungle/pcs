using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Reflection;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace CostingDescription
{
	[Serializable]
	public class CostingDescription : MarshalByRefObject, IDynamicReport
	{
		public CostingDescription()
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

		public DataTable ExecuteReport(string pstrMonth, string pstrProductID, string pstrMakeItem, string pstrProductType)
		{
			int intMakeItem = -1;
			if (pstrMakeItem != null && pstrMakeItem != string.Empty)
				intMakeItem = Convert.ToInt32(Convert.ToBoolean(pstrMakeItem));

			DataTable dtbReportData = GetReportData(pstrMonth, pstrProductID, intMakeItem, pstrProductType);
			C1Report rptReport = new C1Report();

			if (mLayoutFile == string.Empty)
				mLayoutFile = "CostingDescription.xml";
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile)[0]);
			rptReport.Layout.PaperSize = PaperKind.A3;

			#region report parameter

			DateTime dtmDate = Convert.ToDateTime(pstrMonth);
			try
			{
				rptReport.Fields["fldMonth"].Text = dtmDate.ToString("MMMM-yyyy");
			}
			catch{}
			if (pstrProductID != null && pstrProductID.Length > 0)
			{
				string strPartNo = string.Empty, strPartName = string.Empty;
				DataTable dtbItem = GetItemsInfo(pstrProductID);
				foreach (DataRow drowItem in dtbItem.Rows)
				{
					strPartNo += drowItem["Code"].ToString() + ", ";
					strPartName += drowItem["Description"].ToString() + ", ";
				}
				// remove the last ","
				if (strPartNo.IndexOf(",") >= 0)
					strPartNo = strPartNo.Substring(0, strPartNo.Length - 2);
				if (strPartName.IndexOf(",") >= 0)
					strPartName = strPartName.Substring(0, strPartName.Length - 2);
				try
				{
					rptReport.Fields["fldPartParam"].Text = strPartNo;
					rptReport.Fields["fldPartNameParam"].Text = strPartName;
				}
				catch{}
			}
			
			#endregion
				
			// set datasource object that provides data to report.
			rptReport.DataSource.Recordset = dtbReportData;
			// render report
			rptReport.Render();

			// render the report into the PrintPreviewControl
			C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
			ppvViewer.FormTitle = "Costing Description " + dtmDate.ToString("MMMM-yyyy");
			
			ppvViewer.ReportViewer.Document = rptReport.Document;
			ppvViewer.Show();
			return dtbReportData;
		}
		private DataTable GetReportData(string pstrMonth, string pstrProductID, int pintMakeItem, string pstrProductType)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;
			try
			{
				string strSql = " DECLARE @PeriodID int, @FromDate datetime,@FromDateStr nvarchar(100),@ToDate datetime"
					+ " SET @FromDateStr = '" + pstrMonth + "'"
					+ " SET @FromDate = CAST(SUBSTRING(@FromDateStr, 1, 4) + '-' + SUBSTRING(@FromDateStr, 6, 2) + '-' + SUBSTRING(@FromDateStr, 9, 2) AS datetime)"
					+ " SET @FromDate = DATEADD(day, -DATEPART(day,@FromDate) + 1, @FromDate)"
					+ " SET @ToDate = DATEADD(month, 1, @FromDate)"
					+ " SET @ToDate = DATEADD(day, -1, @ToDate)"
					+ " SELECT @PeriodID  = ActCostAllocationMasterID"
					+ " FROM cst_ActCostAllocationMaster"
					+ " WHERE FromDate <= @FromDate"
					+ " AND ToDate >= @ToDate"
					+ " SELECT DISTINCT P.Code PartNumber, P.Description PartName, P.Revision Model, P.MakeItem, M.Code UM,"
					+ " A.BeginQuantity,SUM(ISNULL(A.BeginCost,0)) BeginCost, A.WOCompletionQty, SUM(A.ActualCost) ActualCost,"
					+ " SUM(CASE WHEN ISNULL(A.WOCompletionQty,0) <> 0 THEN "
					+ " 	((A.ActualCost*A.Quantity-A.BeginQuantity*A.BeginCost)/A.WOCompletionQty)"
					+ " ELSE 0"
					+ " END) WOCompletionCost,"
					+ " SUM((ISNULL(D.DSAmount,0)+ISNULL(OH_DSAmount,0))) DSAmount,"
					+ " SUM(ISNULL(D.RecycleAmount,0) + ISNULL(OH_RecycleAmount,0)) RecycleAmount,"
					+ " SUM(ISNULL(D.AdjustAmount,0) + ISNULL(OH_AdjustAmount,0)) AdjustAmount,"
					+ " "
					+ " ISNULL((SELECT SUM(ISNULL(InvoiceQty,0)) FROM SO_ConfirmShipDetail sd "
					+ " JOIN SO_ConfirmShipMaster sm ON sd.ConfirmShipMasterID = sm.ConfirmShipMasterID"
					+ " WHERE sd.ProductID = A.ProductID"
					+ " AND (sm.ShippedDate BETWEEN @FromDate AND @ToDate+1)),0) * U.UnitCost -"
					+ " ISNULL((SELECT SUM(ISNULL(ReceiveQuantity,0)) FROM SO_ReturnedGoodsDetail sd"
					+ " JOIN SO_ReturnedGoodsMaster sm ON sd.ReturnedGoodsMasterID = sm.ReturnedGoodsMasterID"
					+ " WHERE sd.ProductID = A.ProductID"
					+ " AND sm.PostDate BETWEEN @FromDate AND @ToDate+1),0) * U.UnitCost +"
					+ " SUM((ISNULL(D.DSAmount,0)"
					+ " + ISNULL(OH_DSAmount,0)"
					+ " - ISNULL(D.RecycleAmount,0)"
					+ " - ISNULL(OH_RecycleAmount,0)"
					+ " - ISNULL(D.AdjustAmount,0)"
					+ " - ISNULL(OH_AdjustAmount,0))) TotalCGS2,"
					+ " CASE WHEN (ISNULL((SELECT SUM(ISNULL(InvoiceQty,0)) FROM SO_ConfirmShipDetail sd "
					+ " JOIN SO_ConfirmShipMaster sm ON sd.ConfirmShipMasterID = sm.ConfirmShipMasterID"
					+ " WHERE sd.ProductID = A.ProductID"
					+ " AND (sm.ShippedDate BETWEEN @FromDate AND @ToDate+1)),0) -"
					+ " ISNULL((SELECT SUM(ISNULL(ReceiveQuantity,0)) FROM SO_ReturnedGoodsDetail sd"
					+ " JOIN SO_ReturnedGoodsMaster sm ON sd.ReturnedGoodsMasterID = sm.ReturnedGoodsMasterID"
					+ " WHERE sd.ProductID = A.ProductID"
					+ " AND sm.PostDate BETWEEN @FromDate AND @ToDate+1),0)) <> 0 THEN"
					+ " (ISNULL((SELECT SUM(ISNULL(InvoiceQty,0)) FROM SO_ConfirmShipDetail sd "
					+ " JOIN SO_ConfirmShipMaster sm ON sd.ConfirmShipMasterID = sm.ConfirmShipMasterID"
					+ " WHERE sd.ProductID = A.ProductID"
					+ " AND (sm.ShippedDate BETWEEN @FromDate AND @ToDate+1)),0) * U.UnitCost -"
					+ " ISNULL((SELECT SUM(ISNULL(ReceiveQuantity,0)) FROM SO_ReturnedGoodsDetail sd"
					+ " JOIN SO_ReturnedGoodsMaster sm ON sd.ReturnedGoodsMasterID = sm.ReturnedGoodsMasterID"
					+ " WHERE sd.ProductID = A.ProductID"
					+ " AND sm.PostDate BETWEEN @FromDate AND @ToDate+1),0) * U.UnitCost +"
					+ " SUM((ISNULL(D.DSAmount,0)"
					+ " + ISNULL(OH_DSAmount,0)"
					+ " - ISNULL(D.RecycleAmount,0)"
					+ " - ISNULL(OH_RecycleAmount,0)"
					+ " - ISNULL(D.AdjustAmount,0)"
					+ " - ISNULL(OH_AdjustAmount,0)))) / (ISNULL((SELECT SUM(ISNULL(InvoiceQty,0)) FROM SO_ConfirmShipDetail sd "
					+ " JOIN SO_ConfirmShipMaster sm ON sd.ConfirmShipMasterID = sm.ConfirmShipMasterID"
					+ " WHERE sd.ProductID = A.ProductID"
					+ " AND (sm.ShippedDate BETWEEN @FromDate AND @ToDate+1)),0) -"
					+ " ISNULL((SELECT SUM(ISNULL(ReceiveQuantity,0)) FROM SO_ReturnedGoodsDetail sd"
					+ " JOIN SO_ReturnedGoodsMaster sm ON sd.ReturnedGoodsMasterID = sm.ReturnedGoodsMasterID"
					+ " WHERE sd.ProductID = A.ProductID"
					+ " AND sm.PostDate BETWEEN @FromDate AND @ToDate+1),0))"
					+ " ELSE 0"
					+ " END AS CGS,"
					+ " (ISNULL((SELECT SUM(ISNULL(InvoiceQty,0)) FROM SO_ConfirmShipDetail sd "
					+ " JOIN SO_ConfirmShipMaster sm ON sd.ConfirmShipMasterID = sm.ConfirmShipMasterID"
					+ " WHERE sd.ProductID = A.ProductID"
					+ " AND (sm.ShippedDate BETWEEN @FromDate AND @ToDate+1)),0) -"
					+ " ISNULL((SELECT SUM(ISNULL(ReceiveQuantity,0)) FROM SO_ReturnedGoodsDetail sd"
					+ " JOIN SO_ReturnedGoodsMaster sm ON sd.ReturnedGoodsMasterID = sm.ReturnedGoodsMasterID"
					+ " WHERE sd.ProductID = A.ProductID"
					+ " AND sm.PostDate BETWEEN @FromDate AND @ToDate+1),0)) AS SellingQuantity,"
					+ " ISNULL((SELECT SUM(ISNULL(InvoiceQty,0)) FROM SO_ConfirmShipDetail sd "
					+ " JOIN SO_ConfirmShipMaster sm ON sd.ConfirmShipMasterID = sm.ConfirmShipMasterID"
					+ " WHERE sd.ProductID = A.ProductID"
					+ " AND (sm.ShippedDate BETWEEN @FromDate AND @ToDate+1)),0) * U.UnitCost -"
					+ " ISNULL((SELECT SUM(ISNULL(ReceiveQuantity,0)) FROM SO_ReturnedGoodsDetail sd"
					+ " JOIN SO_ReturnedGoodsMaster sm ON sd.ReturnedGoodsMasterID = sm.ReturnedGoodsMasterID"
					+ " WHERE sd.ProductID = A.ProductID"
					+ " AND sm.PostDate BETWEEN @FromDate AND @ToDate+1),0) * U.UnitCost AS TotalCGS1,"
					+ " ITM_Category.Code AS Category"
					+ " FROM CST_ActualCostHistory A "
					+ " LEFT JOIN CST_DSAndRecycleAllocation D ON A.ProductID = D.ProductID"
					+ " AND A.ActCostAllocationMasterID = D.ActCostAllocationMasterID"
					+ " AND A.CostElementID = D.CostElementID"
					+ " JOIN ITM_Product P ON A.ProductID = P.ProductID"
					+ " JOIN MST_UnitOfMeasure M ON P.StockUMID = M.UnitOfMeasureID"
					+ " JOIN v_UnitOfActualCost U ON A.ActCostAllocationMasterID = U.ActCostAllocationMasterID"
					+ " AND A.ProductID = U.ProductID"
					+ " LEFT JOIN ITM_Category ON P.CategoryID = ITM_Category.CategoryID"
					+ " WHERE A.ActCostAllocationMasterID = @PeriodID";
				if (pstrProductID != null && pstrProductID.Length > 0)
					strSql += " AND A.ProductID IN (" + pstrProductID + ")";
				if (pstrProductType != null && pstrProductType.Length > 0)
					strSql += " AND P.ProductTypeID IN (" + pstrProductType + ")";
				if (pintMakeItem >= 0)
					strSql += " AND P.MakeItem = " + pintMakeItem;
				strSql += " GROUP BY ITM_Category.Code, A.ProductID, A.ActCostAllocationMasterID,"
					+ " P.Code, P.Description, P.Revision, P.MakeItem, M.Code, "
					+ " A.BeginQuantity, A.WOCompletionQty, U.UnitCost"
					+ " ORDER BY ITM_Category.Code, P.Code, P.Description, P.Revision";
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
		
		private DataTable GetItemsInfo(string pstrProductID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code, Description FROM ITM_Product WHERE ProductID IN (" + pstrProductID + ")";
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
	}
}