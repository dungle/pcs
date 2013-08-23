using System;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;

using System.Data.OleDb;
using System.Reflection;
using C1.Win.C1Preview;

using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSUtils.Utils;

namespace InOutStockWithCostReportByItem
{
	[Serializable]
	public class InOutStockWithCostReportByItem : MarshalByRefObject, IDynamicReport
	{
		public InOutStockWithCostReportByItem()
		{
		}

		#region Constants
		private const int MAX_LENGTH = 130;
		private const string TABLE_NAME = "tbl_OutsideProcessing";	

		private const string NAME_FLD = "Name";
		private const string EXCHANGE_RATE_FLD = "ExchangeRate";		
		private const string PRODUCT_ID_FLD = "ProductID";
		private const string PRODUCT_CODE_FLD = "Code";
		private const string PRODUCT_NAME_FLD = "Description";	
		
		#endregion		

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

		private string mstrReportDefinitionFolder = string.Empty;
		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>				
		public string ReportDefinitionFolder
		{
			get { return mstrReportDefinitionFolder; }
			set { mstrReportDefinitionFolder = value; }
		}


		private string mstrReportLayoutFile = string.Empty;		
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
				return mstrReportLayoutFile;
			}
			set
			{
				mstrReportLayoutFile = value;
			}
		}

		#endregion
		
		#region In Out Stock With Cost Report: Tuan TQ		
		
		DataTable GetInOutStockWithCostData(string pstrCCNID, 
			string pstrYear, 
			string pstrMonth, 
			string pstrLocationIDList, 
			string pstrCategoryIDList, 
			string pstrProductIDList,
			int pintMakeItem,
			string pstrBinType)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strFormat = "yyyy-MM-dd HH:mm:ss";
				DateTime dtmStart = new DateTime(Convert.ToInt32(pstrYear), Convert.ToInt32(pstrMonth), 1);
				DateTime dtmEnd = dtmStart.AddMonths(1).AddSeconds(-1);
				DateTime dtmEndMonth = dtmStart.AddMonths(1).AddDays(-1);
				DateTime dtmPreMonth = dtmStart.AddDays(-1);
				string strFromDate = "'" + dtmStart.ToString(strFormat) + "'";
				string strToDate = "'" + dtmEnd.ToString(strFormat) + "'";

				string strSQL = "DECLARE @PeriodID int\n"
					+ " SELECT @PeriodID  = ActCostAllocationMasterID\n"
					+ " FROM cst_ActCostAllocationMaster\n"
					+ " WHERE FromDate <= '" + dtmStart.ToString(strFormat) + "'\n"
					+ " AND ToDate >= '" + dtmEndMonth.ToString(strFormat) + "'\n"
					+ " SELECT a.* FROM (";

				StringBuilder strSqlBuilder = new StringBuilder();
				strSqlBuilder.Append("SELECT  DISTINCT 0 as [No.], IV_BinCache.LocationID, MST_Location.Code AS LocationCode, ");
				strSqlBuilder.Append("ITM_Product.ProductID, ITM_Category.Code as CategoryCode, MST_Bin.Code AS Bin,");
				strSqlBuilder.Append("ITM_Product.Code as PartNO, ITM_Product.Description as PartName, ITM_Product.Revision as PartModel,  ");
				strSqlBuilder.Append("MST_UnitOfMeasure.Code as StockUM,  ");
				// begin stock
				strSqlBuilder.Append(" SUM(ISNULL(IV_BalanceBin.OHQuantity,0)) as BeginStock,");
				// begin value
				strSqlBuilder.Append(" SUM(ISNULL(IV_BalanceBin.OHQuantity,0)) *");
				strSqlBuilder.Append(" ISNULL((SELECT SUM(ISNULL(BeginCost,0)) FROM CST_ActualCostHistory");
				strSqlBuilder.Append(" WHERE ActCostAllocationMasterID = @PeriodID");
				strSqlBuilder.Append(" AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID)");
				strSqlBuilder.Append(" ,0) as BeginValue, ");
				// in quantity
				strSqlBuilder.Append("(SELECT ISNULL(SUM(TransQuantity), 0) 	 ");
				strSqlBuilder.Append("FROM v_TransactionHistory		      ");
				strSqlBuilder.Append("INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID 	 ");
				strSqlBuilder.Append("WHERE ProductID = ITM_Product.ProductID 		 ");
				strSqlBuilder.Append("AND TransQuantity > 0 		 ");
				strSqlBuilder.Append("AND LocationID = IV_BinCache.LocationID 		 ");
				strSqlBuilder.Append("AND BinID = IV_BinCache.BinID 		 ");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )  ");
				strSqlBuilder.Append("AND (MST_TranType.Type = 1 OR MST_TranType.Type = 2))");

				//-- component scrap
				strSqlBuilder.Append("- ISNULL((SELECT SUM(ISNULL(ScrapQuantity,0))");
				strSqlBuilder.Append(" FROM PRO_ComponentScrapDetail D JOIN PRO_ComponentScrapMaster M");
				strSqlBuilder.Append(" ON D.ComponentScrapMasterID = M.ComponentScrapMasterID");
				strSqlBuilder.Append(" WHERE (PostDate BETWEEN  " + strFromDate + "  AND  " + strToDate + "  ) ");
				strSqlBuilder.Append(" AND ToLocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND ToBinID = IV_BinCache.BinID");
				strSqlBuilder.Append(" AND ComponentID = ITM_Product.ProductID),0)");

				// -- misc. issue: Xuat huy
				strSqlBuilder.Append("- ISNULL((SELECT SUM(ISNULL(Quantity, 0))");
				strSqlBuilder.Append(" FROM IV_MiscellaneousIssueDetail D JOIN IV_MiscellaneousIssueMaster M");
				strSqlBuilder.Append(" ON D.MiscellaneousIssueMasterID = M.MiscellaneousIssueMasterID");
				strSqlBuilder.Append(" JOIN PRO_IssuePurpose ON M.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID");
				strSqlBuilder.Append(" WHERE (PostDate BETWEEN  " + strFromDate + "  AND  " + strToDate + "  ) ");
				strSqlBuilder.Append(" AND PRO_IssuePurpose.Code = 14");
				strSqlBuilder.Append(" AND DesLocationID =  IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND DesBinID =  IV_BinCache.BinID");
				strSqlBuilder.Append(" AND ProductID = ITM_Product.ProductID),0) as InQuantity, ");
				// in value
				strSqlBuilder.Append("((SELECT ISNULL(SUM(TransQuantity), 0) 	 ");
				strSqlBuilder.Append("FROM v_TransactionHistory		      ");
				strSqlBuilder.Append("INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID 	 ");
				strSqlBuilder.Append("WHERE ProductID = ITM_Product.ProductID 		 ");
				strSqlBuilder.Append("AND TransQuantity > 0 		 ");
				strSqlBuilder.Append("AND LocationID = IV_BinCache.LocationID 		 ");
				strSqlBuilder.Append("AND BinID = IV_BinCache.BinID ");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )  ");
				strSqlBuilder.Append("AND (MST_TranType.Type = 1 OR MST_TranType.Type = 2)");
				strSqlBuilder.Append("AND MST_TranType.Code NOT IN ('POPurchaseOrderReceipts','PROWorkOrderCompletion')) ");

				//-- component scrap
				strSqlBuilder.Append("- ISNULL((SELECT SUM(ISNULL(ScrapQuantity,0))");
				strSqlBuilder.Append(" FROM PRO_ComponentScrapDetail D JOIN PRO_ComponentScrapMaster M");
				strSqlBuilder.Append(" ON D.ComponentScrapMasterID = M.ComponentScrapMasterID");
				strSqlBuilder.Append(" WHERE (PostDate BETWEEN  " + strFromDate + "  AND  " + strToDate + "  ) ");
				strSqlBuilder.Append(" AND ToLocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND ToBinID = IV_BinCache.BinID");
				strSqlBuilder.Append(" AND ComponentID = ITM_Product.ProductID),0)");

				// misc. issue = xuat huy
				strSqlBuilder.Append("- ISNULL((SELECT SUM(ISNULL(Quantity, 0))");
				strSqlBuilder.Append(" FROM IV_MiscellaneousIssueDetail D JOIN IV_MiscellaneousIssueMaster M");
				strSqlBuilder.Append("  ON D.MiscellaneousIssueMasterID = M.MiscellaneousIssueMasterID");
				strSqlBuilder.Append(" JOIN PRO_IssuePurpose ON M.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID");
				strSqlBuilder.Append(" WHERE (PostDate BETWEEN  " + strFromDate + "  AND  " + strToDate + "  ) ");
				strSqlBuilder.Append(" AND PRO_IssuePurpose.Code = 14");
				strSqlBuilder.Append(" AND DesLocationID =  IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND DesBinID =  IV_BinCache.BinID");
				strSqlBuilder.Append(" AND ProductID = ITM_Product.ProductID),0)) * ");

				strSqlBuilder.Append("ISNULL((Case ");
				strSqlBuilder.Append("			When (SELECT SUM(cost.ActualCost) as ActualCost ");
				strSqlBuilder.Append("					FROM CST_ActualCostHistory cost ");
				strSqlBuilder.Append("					WHERE 	cost.ProductID  = ITM_Product.ProductID          	 ");
				strSqlBuilder.Append(" 						AND cost.ActCostAllocationMasterID = @PeriodID)");
				strSqlBuilder.Append("					Is Not Null Then  	    ");
				strSqlBuilder.Append("						(SELECT SUM(cost.ActualCost) as ActualCost  	     ");
				strSqlBuilder.Append("							FROM CST_ActualCostHistory cost  		 ");
				strSqlBuilder.Append("							WHERE cost.ProductID  = ITM_Product.ProductID  	       	  ");
				strSqlBuilder.Append(" 						AND cost.ActCostAllocationMasterID = @PeriodID)");
				strSqlBuilder.Append("					Else   	   ");
				strSqlBuilder.Append("							(SELECT SUM(Cost)  	    ");
				strSqlBuilder.Append("							FROM CST_STDItemCost   	    ");
				strSqlBuilder.Append("							WHERE ProductID = ITM_Product.ProductID)	End), 0) ");
				// work order completion
				strSqlBuilder.Append(" + (SELECT ISNULL(SUM(TransQuantity), 0) 	 ");
				strSqlBuilder.Append(" FROM v_TransactionHistory		      ");
				strSqlBuilder.Append(" INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID 	 ");
				strSqlBuilder.Append(" WHERE ProductID = ITM_Product.ProductID 		 ");
				strSqlBuilder.Append(" AND TransQuantity > 0 		 ");
				strSqlBuilder.Append(" AND LocationID = IV_BinCache.LocationID 		 ");
				strSqlBuilder.Append(" AND BinID = IV_BinCache.BinID ");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )  ");
				strSqlBuilder.Append(" AND (MST_TranType.Type = 1 OR MST_TranType.Type = 2)");
				strSqlBuilder.Append(" AND MST_TranType.Code IN ('PROWorkOrderCompletion')");
				strSqlBuilder.Append(" ) *");
				strSqlBuilder.Append(" ISNULL((SELECT SUM(CASE");
				strSqlBuilder.Append(" 	WHEN ISNULL(A.WOCompletionQty,0) = 0 THEN A.ActualCost");
				strSqlBuilder.Append(" 	ELSE (A.ActualCost*ISNULL(A.Quantity,0) - ISNULL(A.BeginQuantity,0)*ISNULL(A.BeginCost,0))/A.WOCompletionQty");
				strSqlBuilder.Append(" END) FROM CST_ActualCostHistory A ");
				strSqlBuilder.Append(" WHERE A.ProductID = ITM_Product.ProductID");
				strSqlBuilder.Append(" AND A.ActCostAllocationMasterID = @PeriodID");
				strSqlBuilder.Append(" ),0)");
				//po receipt by outside
				strSqlBuilder.Append(" + ISNULL((SELECT SUM(ISNULL(ReceiveQuantity,0)) FROM PO_PurchaseOrderReceiptDetail RD");
				strSqlBuilder.Append(" JOIN PO_PurchaseOrderReceiptMaster RM ON RD.PurchaseOrderReceiptID = RM.PurchaseOrderReceiptID");
				strSqlBuilder.Append(" WHERE RM.ReceiptType = 4");
				strSqlBuilder.Append(" AND RD.ProductID = ITM_Product.ProductID");
				strSqlBuilder.Append(" AND RD.LocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND RD.BinID = IV_BinCache.BinID");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )  ");
				strSqlBuilder.Append(" ),0)* ");
				strSqlBuilder.Append(" ISNULL((SELECT SUM(CASE");
				strSqlBuilder.Append(" 	WHEN ISNULL(A.WOCompletionQty,0) = 0 THEN A.ActualCost");
				strSqlBuilder.Append(" 	ELSE (A.ActualCost*ISNULL(A.Quantity,0) - ISNULL(A.BeginQuantity,0)*ISNULL(A.BeginCost,0))/A.WOCompletionQty");
				strSqlBuilder.Append(" END) FROM CST_ActualCostHistory A ");
				strSqlBuilder.Append(" WHERE A.ProductID = ITM_Product.ProductID");
				strSqlBuilder.Append(" AND A.ActCostAllocationMasterID = @PeriodID");
				strSqlBuilder.Append(" ),0)");
				//PO Receipt by slip amount
				strSqlBuilder.Append(" + ISNULL((SELECT SUM(ISNULL(ReceiveQuantity,0) * PD.UnitPrice * ISNULL(PM.ExchangeRate,0))");
				strSqlBuilder.Append(" FROM PO_PurchaseOrderReceiptDetail RD");
				strSqlBuilder.Append(" JOIN PO_PurchaseOrderReceiptMaster RM ON RD.PurchaseOrderReceiptID = RM.PurchaseOrderReceiptID");
				strSqlBuilder.Append(" JOIN PO_PurchaseOrderDetail PD ON RD.PurchaseOrderDetailID = PD.PurchaseOrderDetailID");
				strSqlBuilder.Append(" JOIN PO_PurchaseOrderMaster PM ON RD.PurchaseOrderMasterID = PM.PurchaseOrderMasterID");
				strSqlBuilder.Append(" WHERE RM.ReceiptType = 2");
				strSqlBuilder.Append(" AND RD.ProductID = ITM_Product.ProductID");
				strSqlBuilder.Append(" AND RD.LocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND RD.BinID = IV_BinCache.BinID");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )  ");
				strSqlBuilder.Append(" ),0)");
				// PO Receipt by invoice amount
				strSqlBuilder.Append(" + ISNULL((SELECT SUM(((ISNULL(invoice.CIPAmount,0) + ISNULL(invoice.ImportTaxAmount,0))");
				strSqlBuilder.Append(" * ISNULL(IM.ExchangeRate,0))");
				strSqlBuilder.Append(" + (ISNULL(FD.Amount,0) * ISNULL(FM.ExchangeRate,0)))");
				strSqlBuilder.Append(" FROM PO_PurchaseOrderReceiptDetail RD");
				strSqlBuilder.Append(" JOIN PO_PurchaseOrderReceiptMaster RM ON RD.PurchaseOrderReceiptID = RM.PurchaseOrderReceiptID");
				strSqlBuilder.Append(" JOIN PO_InvoiceMaster IM ON IM.InvoiceMasterID = RM.InvoiceMasterID");
				strSqlBuilder.Append(" JOIN PO_InvoiceDetail invoice ON invoice.InvoiceMasterID = RM.InvoiceMasterID");
				strSqlBuilder.Append(" LEFT JOIN cst_FreightMaster FM ON FM.PurchaseOrderReceiptID = RM.PurchaseOrderReceiptID");
				strSqlBuilder.Append(" AND FM.ACPurposeID IN (1,2)");
				strSqlBuilder.Append(" LEFT JOIN cst_FreightDetail FD ON FD.FreightMasterID = FM.FreightMasterID ");
				strSqlBuilder.Append(" AND FD.ProductID = RD.ProductID");
				strSqlBuilder.Append(" WHERE RM.ReceiptType = 3");
				strSqlBuilder.Append(" AND RD.ProductID = ITM_Product.ProductID");
				strSqlBuilder.Append(" AND RD.LocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND RD.BinID = IV_BinCache.BinID");
				strSqlBuilder.Append(" 	AND (RM.PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )),0)  ");
				strSqlBuilder.Append("as InValue, ");
				// out quantity
				strSqlBuilder.Append("((SELECT ISNULL(SUM(TransQuantity), 0) ");
				strSqlBuilder.Append("FROM v_TransactionHistory ");
				strSqlBuilder.Append("INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID ");
				strSqlBuilder.Append("WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append("AND LocationID = IV_BinCache.LocationID ");
				strSqlBuilder.Append("AND BinID = IV_BinCache.BinID ");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )  ");
				strSqlBuilder.Append("AND MST_TranType.Type = 0 ) ");

				//-- misc. issue: from ds to not ds
				strSqlBuilder.Append(" - ISNULL((SELECT SUM(ISNULL(Quantity, 0))");
				strSqlBuilder.Append(" FROM IV_MiscellaneousIssueDetail D JOIN IV_MiscellaneousIssueMaster M");
				strSqlBuilder.Append(" ON D.MiscellaneousIssueMasterID = M.MiscellaneousIssueMasterID");
				strSqlBuilder.Append(" JOIN PRO_IssuePurpose ON M.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID");
				strSqlBuilder.Append(" JOIN MST_BIN SourceBin ON M.SourceBinID = SourceBin.BinID");
				strSqlBuilder.Append(" AND SourceBin.BinTypeID = 3");
				strSqlBuilder.Append(" JOIN MST_BIN DesBin ON M.DesBinID = DesBin.BinID");
				strSqlBuilder.Append(" AND DesBin.BinTypeID <> 3");
				strSqlBuilder.Append(" WHERE (PostDate BETWEEN  " + strFromDate + "  AND  " + strToDate + "  ) ");
				strSqlBuilder.Append(" AND SourceLocationID =  IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND SourceBinID =  IV_BinCache.BinID");
				strSqlBuilder.Append(" AND ProductID = ITM_Product.ProductID),0)");

				// recover
				strSqlBuilder.Append(" - ISNULL((SELECT ISNULL(SUM(Quantity),0)");
				strSqlBuilder.Append(" FROM CST_RecoverMaterialMaster");
				strSqlBuilder.Append(" WHERE ProductID = ITM_Product.ProductID");
				strSqlBuilder.Append(" AND FromLocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND FromBinID = IV_BinCache.BinID");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )),0)  ");

				strSqlBuilder.Append("-  (SELECT ISNULL(SUM(TransQuantity), 0) ");
				strSqlBuilder.Append("FROM v_TransactionHistory ");
				strSqlBuilder.Append("INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID ");
				strSqlBuilder.Append("WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append("AND TransQuantity < 0 ");
				strSqlBuilder.Append("AND LocationID = IV_BinCache.LocationID ");
				strSqlBuilder.Append("AND BinID = IV_BinCache.BinID ");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )  ");
				strSqlBuilder.Append("AND (MST_TranType.Type = 1 OR MST_TranType.Type = 2) ) ) as OutQuantity,  ");
				// out value
				strSqlBuilder.Append("(((SELECT ISNULL(SUM(TransQuantity), 0) ");
				strSqlBuilder.Append("FROM v_TransactionHistory ");
				strSqlBuilder.Append("INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID ");
				strSqlBuilder.Append("WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append("AND LocationID = IV_BinCache.LocationID ");
				strSqlBuilder.Append("AND BinID = IV_BinCache.BinID ");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )  ");
				strSqlBuilder.Append("AND MST_TranType.Type = 0 ) ");

				//-- misc. issue: from ds to not ds
				strSqlBuilder.Append(" - ISNULL((SELECT SUM(ISNULL(Quantity, 0))");
				strSqlBuilder.Append(" FROM IV_MiscellaneousIssueDetail D JOIN IV_MiscellaneousIssueMaster M");
				strSqlBuilder.Append(" ON D.MiscellaneousIssueMasterID = M.MiscellaneousIssueMasterID");
				strSqlBuilder.Append(" JOIN PRO_IssuePurpose ON M.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID");
				strSqlBuilder.Append(" JOIN MST_BIN SourceBin ON M.SourceBinID = SourceBin.BinID");
				strSqlBuilder.Append(" AND SourceBin.BinTypeID = 3");
				strSqlBuilder.Append(" JOIN MST_BIN DesBin ON M.DesBinID = DesBin.BinID");
				strSqlBuilder.Append(" AND DesBin.BinTypeID <> 3");
				strSqlBuilder.Append(" WHERE (PostDate BETWEEN  " + strFromDate + "  AND  " + strToDate + "  ) ");
				strSqlBuilder.Append(" AND SourceLocationID =  IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND SourceBinID =  IV_BinCache.BinID");
				strSqlBuilder.Append(" AND ProductID = ITM_Product.ProductID),0)");

				// recover
				strSqlBuilder.Append(" - ISNULL((SELECT ISNULL(SUM(Quantity),0)");
				strSqlBuilder.Append(" FROM CST_RecoverMaterialMaster");
				strSqlBuilder.Append(" WHERE ProductID = ITM_Product.ProductID");
				strSqlBuilder.Append(" AND FromLocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND FromBinID = IV_BinCache.BinID");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )),0)  ");

				strSqlBuilder.Append("-  (SELECT ISNULL(SUM(TransQuantity), 0) ");
				strSqlBuilder.Append("FROM v_TransactionHistory ");
				strSqlBuilder.Append("INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID ");
				strSqlBuilder.Append("WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append("AND TransQuantity < 0 ");
				strSqlBuilder.Append("AND LocationID = IV_BinCache.LocationID ");
				strSqlBuilder.Append("AND BinID = IV_BinCache.BinID ");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )  ");
				strSqlBuilder.Append("AND (MST_TranType.Type = 1 OR MST_TranType.Type = 2)))  ");
				strSqlBuilder.Append(" - (SELECT ISNULL(SUM(TransQuantity), 0) ");
				strSqlBuilder.Append(" FROM v_TransactionHistory ");
				strSqlBuilder.Append(" INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID ");
				strSqlBuilder.Append(" WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" AND TransQuantity < 0 ");
				strSqlBuilder.Append(" AND LocationID = IV_BinCache.LocationID ");
				strSqlBuilder.Append(" AND BinID = IV_BinCache.BinID ");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )  ");
				strSqlBuilder.Append(" AND MST_TranType.Code = 'POReturnToVendor')) * ");
				strSqlBuilder.Append("ISNULL((Case ");
				strSqlBuilder.Append("			When (SELECT SUM(cost.ActualCost) as ActualCost ");
				strSqlBuilder.Append("					FROM CST_ActualCostHistory cost ");
				strSqlBuilder.Append("					WHERE 	cost.ProductID  = ITM_Product.ProductID          	 ");
				strSqlBuilder.Append(" 						AND cost.ActCostAllocationMasterID = @PeriodID)");
				strSqlBuilder.Append("					Is Not Null Then  	    ");
				strSqlBuilder.Append("						(SELECT SUM(cost.ActualCost) as ActualCost  	     ");
				strSqlBuilder.Append("							FROM CST_ActualCostHistory cost  		 ");
				strSqlBuilder.Append("							WHERE cost.ProductID  = ITM_Product.ProductID  	       	  ");
				strSqlBuilder.Append(" 						AND cost.ActCostAllocationMasterID = @PeriodID)");
				strSqlBuilder.Append("					Else   	   ");
				strSqlBuilder.Append("							(SELECT SUM(Cost)  	    ");
				strSqlBuilder.Append("							FROM CST_STDItemCost   	    ");
				strSqlBuilder.Append("							WHERE ProductID = ITM_Product.ProductID)	End), 0) ");
				strSqlBuilder.Append(" + ISNULL((SELECT SUM(cst_FreightDetail.Amount * ISNULL(freight.ExchangeRate,1))");
				strSqlBuilder.Append(" FROM cst_FreightMaster freight JOIN cst_FreightDetail ON freight.FreightMasterID = cst_FreightDetail.FreightMasterID");
				strSqlBuilder.Append(" WHERE freight.ACPurposeID = 4");
				strSqlBuilder.Append(" AND cst_FreightDetail.ProductID = ITM_Product.ProductID");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )  ");
				strSqlBuilder.Append(" ),0) ");
				strSqlBuilder.Append("as OutValue,  ");
				// begin item cost
				strSqlBuilder.Append(" ISNULL((SELECT SUM(ISNULL(BeginCost,0)) FROM CST_ActualCostHistory");
				strSqlBuilder.Append(" WHERE ActCostAllocationMasterID = @PeriodID");
				strSqlBuilder.Append(" AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID)");
				strSqlBuilder.Append(" , 0) AS BeginItemCost, ");;
				// item cost
				strSqlBuilder.Append("ISNULL((Case ");
				strSqlBuilder.Append("			When (SELECT SUM(cost.ActualCost) as ActualCost ");
				strSqlBuilder.Append("					FROM CST_ActualCostHistory cost ");
				strSqlBuilder.Append("					WHERE 	cost.ProductID  = ITM_Product.ProductID          	 ");
				strSqlBuilder.Append(" 						AND cost.ActCostAllocationMasterID = @PeriodID)");
				strSqlBuilder.Append("					Is Not Null Then  	    ");
				strSqlBuilder.Append("						(SELECT SUM(cost.ActualCost) as ActualCost  	     ");
				strSqlBuilder.Append("							FROM CST_ActualCostHistory cost  		 ");
				strSqlBuilder.Append("							WHERE cost.ProductID  = ITM_Product.ProductID  	       	  ");
				strSqlBuilder.Append(" 						AND cost.ActCostAllocationMasterID = @PeriodID)");
				strSqlBuilder.Append("					Else   	   ");
				strSqlBuilder.Append("							(SELECT SUM(Cost)  	    ");
				strSqlBuilder.Append("							FROM CST_STDItemCost   	    ");
				strSqlBuilder.Append("							WHERE ProductID = ITM_Product.ProductID)	End), 0) AS ItemCost, ");
				// end stock
				strSqlBuilder.Append("(SUM(ISNULL(IV_BalanceBin.OHQuantity,0)) + ( SELECT ISNULL(SUM(TransQuantity), 0)      ");
				strSqlBuilder.Append("	FROM v_TransactionHistory		      ");
				strSqlBuilder.Append("		INNER JOIN MST_TranType  ");
				strSqlBuilder.Append("			ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID 	 ");
				strSqlBuilder.Append("	WHERE ProductID = ITM_Product.ProductID");
				strSqlBuilder.Append("		AND LocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append("		AND BinID = IV_BinCache.BinID");
				strSqlBuilder.Append("		AND PostDate BETWEEN " + strFromDate + " AND " + strToDate);
				strSqlBuilder.Append("		AND (MST_TranType.Type = 1 OR MST_TranType.Type = 2))  ");

				//-- component scrap
				strSqlBuilder.Append("- ISNULL((SELECT SUM(ISNULL(ScrapQuantity,0))");
				strSqlBuilder.Append(" FROM PRO_ComponentScrapDetail D JOIN PRO_ComponentScrapMaster M");
				strSqlBuilder.Append(" ON D.ComponentScrapMasterID = M.ComponentScrapMasterID");
				strSqlBuilder.Append(" WHERE (PostDate BETWEEN  " + strFromDate + "  AND  " + strToDate + "  ) ");
				strSqlBuilder.Append(" AND ToLocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND ToBinID = IV_BinCache.BinID");
				strSqlBuilder.Append(" AND ComponentID = ITM_Product.ProductID),0)");

				// misc. issue: xuat huy
				strSqlBuilder.Append("- ISNULL((SELECT SUM(ISNULL(Quantity, 0))");
				strSqlBuilder.Append(" FROM IV_MiscellaneousIssueDetail D JOIN IV_MiscellaneousIssueMaster M");
				strSqlBuilder.Append(" ON D.MiscellaneousIssueMasterID = M.MiscellaneousIssueMasterID");
				strSqlBuilder.Append(" JOIN PRO_IssuePurpose ON M.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID");
				strSqlBuilder.Append(" WHERE (PostDate BETWEEN  " + strFromDate + "  AND  " + strToDate + "  ) ");
				strSqlBuilder.Append(" AND PRO_IssuePurpose.Code = 14");
				strSqlBuilder.Append(" AND DesLocationID =  IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND DesBinID =  IV_BinCache.BinID");
				strSqlBuilder.Append(" AND ProductID = ITM_Product.ProductID),0) ");

				// recover
				strSqlBuilder.Append(" + ISNULL((SELECT ISNULL(SUM(Quantity),0)");
				strSqlBuilder.Append(" FROM CST_RecoverMaterialMaster");
				strSqlBuilder.Append(" WHERE ProductID = ITM_Product.ProductID");
				strSqlBuilder.Append(" AND FromLocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND FromBinID = IV_BinCache.BinID");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )),0)  ");

				//-- misc. issue: from ds to not ds
				strSqlBuilder.Append(" + ISNULL((SELECT SUM(ISNULL(Quantity, 0))");
				strSqlBuilder.Append(" FROM IV_MiscellaneousIssueDetail D JOIN IV_MiscellaneousIssueMaster M");
				strSqlBuilder.Append(" ON D.MiscellaneousIssueMasterID = M.MiscellaneousIssueMasterID");
				strSqlBuilder.Append(" JOIN PRO_IssuePurpose ON M.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID");
				strSqlBuilder.Append(" JOIN MST_BIN SourceBin ON M.SourceBinID = SourceBin.BinID");
				strSqlBuilder.Append(" AND SourceBin.BinTypeID = 3");
				strSqlBuilder.Append(" JOIN MST_BIN DesBin ON M.DesBinID = DesBin.BinID");
				strSqlBuilder.Append(" AND DesBin.BinTypeID <> 3");
				strSqlBuilder.Append(" WHERE (PostDate BETWEEN  " + strFromDate + "  AND  " + strToDate + "  ) ");
				strSqlBuilder.Append(" AND SourceLocationID =  IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND SourceBinID =  IV_BinCache.BinID");
				strSqlBuilder.Append(" AND ProductID = ITM_Product.ProductID),0)");

				strSqlBuilder.Append("- ( SELECT ISNULL(SUM(TransQuantity), 0)  ");
				strSqlBuilder.Append("	FROM v_TransactionHistory		      ");
				strSqlBuilder.Append("		INNER JOIN MST_TranType  ");
				strSqlBuilder.Append("			ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID  ");
				strSqlBuilder.Append("	WHERE ProductID = ITM_Product.ProductID 	 ");
				strSqlBuilder.Append("		AND LocationID = IV_BinCache.LocationID 	 ");
				strSqlBuilder.Append("		AND BinID = IV_BinCache.BinID 	 ");
				strSqlBuilder.Append("		AND PostDate BETWEEN " + strFromDate + " AND " + strToDate);
				strSqlBuilder.Append("	AND MST_TranType.Type = 0 ) ) as EndQuantity,  ");
				// end stock value
				strSqlBuilder.Append("(SUM(ISNULL(IV_BalanceBin.OHQuantity,0)) + ( SELECT ISNULL(SUM(TransQuantity), 0)      ");
				strSqlBuilder.Append("	FROM v_TransactionHistory		      ");
				strSqlBuilder.Append("		INNER JOIN MST_TranType  ");
				strSqlBuilder.Append("			ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID 	 ");
				strSqlBuilder.Append("	WHERE ProductID = ITM_Product.ProductID");
				strSqlBuilder.Append("		AND LocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append("		AND BinID = IV_BinCache.BinID");
				strSqlBuilder.Append("		AND PostDate BETWEEN " + strFromDate + " AND " + strToDate);
				strSqlBuilder.Append("		AND (MST_TranType.Type = 1 OR MST_TranType.Type = 2))  ");

				//-- component scrap
				strSqlBuilder.Append("- ISNULL((SELECT SUM(ISNULL(ScrapQuantity,0))");
				strSqlBuilder.Append(" FROM PRO_ComponentScrapDetail D JOIN PRO_ComponentScrapMaster M");
				strSqlBuilder.Append(" ON D.ComponentScrapMasterID = M.ComponentScrapMasterID");
				strSqlBuilder.Append(" WHERE (PostDate BETWEEN  " + strFromDate + "  AND  " + strToDate + "  ) ");
				strSqlBuilder.Append(" AND ToLocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND ToBinID = IV_BinCache.BinID");
				strSqlBuilder.Append(" AND ComponentID = ITM_Product.ProductID),0)");

				// misc. issue: xuat huy
				strSqlBuilder.Append("- ISNULL((SELECT SUM(ISNULL(Quantity, 0))");
				strSqlBuilder.Append(" FROM IV_MiscellaneousIssueDetail D JOIN IV_MiscellaneousIssueMaster M");
				strSqlBuilder.Append(" ON D.MiscellaneousIssueMasterID = M.MiscellaneousIssueMasterID");
				strSqlBuilder.Append(" JOIN PRO_IssuePurpose ON M.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID");
				strSqlBuilder.Append(" WHERE (PostDate BETWEEN  " + strFromDate + "  AND  " + strToDate + "  ) ");
				strSqlBuilder.Append(" AND PRO_IssuePurpose.Code = 14");
				strSqlBuilder.Append(" AND DesLocationID =  IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND DesBinID =  IV_BinCache.BinID");
				strSqlBuilder.Append(" AND ProductID = ITM_Product.ProductID),0) ");

				// recover
				strSqlBuilder.Append(" + ISNULL((SELECT ISNULL(SUM(Quantity),0)");
				strSqlBuilder.Append(" FROM CST_RecoverMaterialMaster");
				strSqlBuilder.Append(" WHERE ProductID = ITM_Product.ProductID");
				strSqlBuilder.Append(" AND FromLocationID = IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND FromBinID = IV_BinCache.BinID");
				strSqlBuilder.Append(" 	AND (PostDate BETWEEN " + strFromDate + "  AND " + strToDate + " )),0)  ");

				//-- misc. issue: from ds to not ds
				strSqlBuilder.Append(" + ISNULL((SELECT SUM(ISNULL(Quantity, 0))");
				strSqlBuilder.Append(" FROM IV_MiscellaneousIssueDetail D JOIN IV_MiscellaneousIssueMaster M");
				strSqlBuilder.Append(" ON D.MiscellaneousIssueMasterID = M.MiscellaneousIssueMasterID");
				strSqlBuilder.Append(" JOIN PRO_IssuePurpose ON M.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID");
				strSqlBuilder.Append(" JOIN MST_BIN SourceBin ON M.SourceBinID = SourceBin.BinID");
				strSqlBuilder.Append(" AND SourceBin.BinTypeID = 3");
				strSqlBuilder.Append(" JOIN MST_BIN DesBin ON M.DesBinID = DesBin.BinID");
				strSqlBuilder.Append(" AND DesBin.BinTypeID <> 3");
				strSqlBuilder.Append(" WHERE (PostDate BETWEEN  " + strFromDate + "  AND  " + strToDate + "  ) ");
				strSqlBuilder.Append(" AND SourceLocationID =  IV_BinCache.LocationID");
				strSqlBuilder.Append(" AND SourceBinID =  IV_BinCache.BinID");
				strSqlBuilder.Append(" AND ProductID = ITM_Product.ProductID),0)");

				strSqlBuilder.Append("- ( SELECT ISNULL(SUM(TransQuantity), 0)  ");
				strSqlBuilder.Append("	FROM v_TransactionHistory		      ");
				strSqlBuilder.Append("		INNER JOIN MST_TranType  ");
				strSqlBuilder.Append("			ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID  ");
				strSqlBuilder.Append("	WHERE ProductID = ITM_Product.ProductID 	 ");
				strSqlBuilder.Append("		AND LocationID = IV_BinCache.LocationID 	 ");
				strSqlBuilder.Append("		AND BinID = IV_BinCache.BinID 	 ");
				strSqlBuilder.Append("		AND PostDate BETWEEN " + strFromDate + " AND " + strToDate);
				strSqlBuilder.Append("AND MST_TranType.Type = 0 	) ) * ");
				strSqlBuilder.Append("ISNULL((Case ");
				strSqlBuilder.Append("			When (SELECT SUM(cost.ActualCost) as ActualCost ");
				strSqlBuilder.Append("					FROM CST_ActualCostHistory cost ");
				strSqlBuilder.Append("					WHERE 	cost.ProductID  = ITM_Product.ProductID          	 ");
				strSqlBuilder.Append(" 						AND cost.ActCostAllocationMasterID = @PeriodID)");
				strSqlBuilder.Append("					Is Not Null Then  	    ");
				strSqlBuilder.Append("						(SELECT SUM(cost.ActualCost) as ActualCost  	     ");
				strSqlBuilder.Append("							FROM CST_ActualCostHistory cost  		 ");
				strSqlBuilder.Append("							WHERE cost.ProductID  = ITM_Product.ProductID  	       	  ");
				strSqlBuilder.Append(" 						AND cost.ActCostAllocationMasterID = @PeriodID)");
				strSqlBuilder.Append("					Else   	   ");
				strSqlBuilder.Append("							(SELECT SUM(Cost)  	    ");
				strSqlBuilder.Append("							FROM CST_STDItemCost   	    ");
				strSqlBuilder.Append("							WHERE ProductID = ITM_Product.ProductID)	End), 0) ");
				strSqlBuilder.Append("as EndValue ");
				strSqlBuilder.Append("FROM  	ITM_Product JOIN IV_BinCache ON ITM_Product.ProductID = IV_BinCache.ProductID ");
				strSqlBuilder.Append(" LEFT JOIN IV_BalanceBin ON ITM_Product.ProductID = IV_BalanceBin.ProductID ");
				strSqlBuilder.Append(" AND IV_BinCache.LocationID = IV_BalanceBin.LocationID");
				strSqlBuilder.Append(" AND IV_BalanceBin.BinID = IV_BinCache.BinID");
				strSqlBuilder.Append(" AND DATEPART(year, IV_BalanceBin.EffectDate) = " + dtmPreMonth.Year);
				strSqlBuilder.Append(" AND DATEPART(month, IV_BalanceBin.EffectDate) = " + dtmPreMonth.Month);
				strSqlBuilder.Append(" INNER JOIN MST_Location ON IV_BinCache.LocationID = MST_Location.LocationID ");
				strSqlBuilder.Append(" INNER JOIN MST_BIN ON IV_BinCache.BinID = MST_BIN.BinID ");
				strSqlBuilder.Append(" INNER JOIN MST_UnitOfMeasure ON MST_UnitOfMeasure.UnitOfMeasureID = ITM_Product.StockUMID ");
				strSqlBuilder.Append(" LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID ");
				strSqlBuilder.Append(" WHERE ITM_Product.CCNID = " + pstrCCNID);
				if (pstrLocationIDList != null && pstrLocationIDList.Length > 0)
					strSqlBuilder.Append(" AND IV_BinCache.LocationID IN (" + pstrLocationIDList + ")  ");
				if (pstrCategoryIDList != null && pstrCategoryIDList.Length > 0)
					strSqlBuilder.Append("AND ITM_Product.CategoryID IN (" + pstrCategoryIDList + ")  ");
				if (pstrProductIDList != null && pstrProductIDList.Length > 0)
					strSqlBuilder.Append("AND ITM_Product.ProductID IN (" + pstrProductIDList + ")  ");
				if (pintMakeItem >= 0)
					strSqlBuilder.Append("AND ITM_Product.MakeItem = " + pintMakeItem);
				if (pstrBinType != null && pstrBinType.Length > 0)
					strSqlBuilder.Append("AND MST_BIN.BinTypeID IN (" + pstrBinType + ")  ");
				strSqlBuilder.Append("GROUP BY 	MST_Location.Code, MST_Bin.Code, IV_BinCache.BinID, ITM_Product.ProductID, ITM_Category.Code, 	MST_UnitOfMeasure.Code, 	ITM_Product.Code, 	 ");
				strSqlBuilder.Append("ITM_Product.Revision, 	ITM_Product.Description, 	ITM_Product.ProductID, 	IV_BinCache.LocationID");
				//strSqlBuilder.Append("ORDER BY ITM_Category.Code, ITM_Product.Code ");

				oconPCS = new OleDbConnection(mConnectionString);

				strSQL += strSqlBuilder.ToString() + ") a ";
				strSQL += " WHERE 	a.BeginStock <> 0 ";				
				strSQL += " OR a.InQuantity <> 0 "; 
				strSQL += " OR a.OutQuantity <> 0";

				//ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
				ocmdPCS = new OleDbCommand(strSQL, oconPCS);
				//End hack

				ocmdPCS.CommandTimeout = 1000;

				Debug.WriteLine(strSqlBuilder.ToString());
				Debug.WriteLine(strSQL);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResult);

				return dtbResult;
			}		
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
					oconPCS = null;
				}
			}
		}
		
		/// <summary>
		/// Get CCN Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCompanyFullName()
		{			
			const string FULL_COMPANY_NAME = "CompanyFullName";
			OleDbConnection oconPCS = null;

			try
			{
				string strResult = string.Empty;
				OleDbDataReader odrPCS = null;				
				
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT [Value]"
					+ " FROM Sys_Param"
					+ " WHERE [Name] = '" + FULL_COMPANY_NAME + "'";
			
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS["Value"].ToString().Trim();
					}
				}
			
				return strResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;
				}
			}
		}

		/// <summary>
		/// Get CCN Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCCNInfoByID(string pstrID)
		{
			string strResult = string.Empty;
			OleDbConnection oconPCS = null;
			
			try
			{
				OleDbDataReader odrPCS = null;
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + PRODUCT_CODE_FLD + ", " + PRODUCT_NAME_FLD
					+ " FROM MST_CCN"
					+ " WHERE MST_CCN.CCNID = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[PRODUCT_CODE_FLD].ToString().Trim() + " (" + odrPCS[PRODUCT_NAME_FLD].ToString().Trim() + ")";
					}
				}

				return strResult;
			
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}
		
		/// <summary>
		/// Get Product Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetProductInfo(string pstrIDList)
		{			
			const string SEMI_COLON = "; ";
			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT Code, Description";
				strSql += " FROM ITM_Product";
				strSql += " WHERE ProductID IN (" +  pstrIDList + ")";
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					while(odrPCS.Read())
					{
						strResult += odrPCS[PRODUCT_CODE_FLD].ToString().Trim() + SEMI_COLON;
					}
				}

				if(strResult.Length > MAX_LENGTH)				
				{
					int i = strResult.IndexOf(SEMI_COLON, MAX_LENGTH);
					if(i > 0)
					{
						strResult = strResult.Substring(0, i + SEMI_COLON.Length) + "...";
					}
				}

				return strResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}

		/// <summary>
		/// Get Location Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetLocationInfo(string pstrIDList)
		{			
			const string SEMI_COLON = "; ";
			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT Code, Name";
				strSql += " FROM MST_Location";
				strSql += " WHERE LocationID IN (" +  pstrIDList + ")";
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					while(odrPCS.Read())
					{
						strResult += odrPCS[PRODUCT_CODE_FLD].ToString().Trim() + SEMI_COLON;
					}
				}

				if(strResult.Length > MAX_LENGTH)				
				{
					int i = strResult.IndexOf(SEMI_COLON, MAX_LENGTH);
					if(i > 0)
					{
						strResult = strResult.Substring(0, i + SEMI_COLON.Length) + "...";
					}
				}

				return strResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}	
		
		/// <summary>
		/// Get Category Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCategoryInfo(string pstrIDList)
		{			
			const string SEMI_COLON = "; ";
			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT Code, Name";
				strSql += " FROM ITM_Category";
				strSql += " WHERE CategoryID IN (" +  pstrIDList + ")";
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					while(odrPCS.Read())
					{
						strResult += odrPCS[PRODUCT_CODE_FLD].ToString().Trim() + SEMI_COLON;
					}
				}

				if(strResult.Length > MAX_LENGTH)				
				{
					int i = strResult.IndexOf(SEMI_COLON, MAX_LENGTH);
					if(i > 0)
					{
						strResult = strResult.Substring(0, i + SEMI_COLON.Length) + "...";
					}
				}

				return strResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}	
		
		#endregion
		
		#region Public Method

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}		
		
		/// <summary>
		/// Build and show Detai lItem Price By PO Receipt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Tuan TQ, 11 Apr, 2006</author>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrLocationIDList, string pstrCategoryIDList, string pstrProductIDList, string pstrMakeItem, string pstrBinType)
		{			
			const string REPORT_TEMPLATE = "InOutStockWithCostReportByItem.xml";
			const string RPT_PAGE_HEADER = "PageHeader";

			const string REPORT_NAME = "InOutStockWithCostReportByItem";
			const string RPT_TITLE_FLD = "fldTitle";
			const string RPT_COMPANY_FLD = "fldCompany";
				
			const string RPT_CCN_FLD = "CCN";
			const string RPT_YEAR_FLD = "Year";
			const string RPT_MONTH_FLD = "Month";
			const string RPT_LOCATION_FLD = "Location";
			const string RPT_CATEGORY_FLD = "Category";
			const string RPT_PART_NO_FLD = "Part No.";

			int intMakeItem = -1;
			if (pstrMakeItem != null && pstrMakeItem != string.Empty)
				intMakeItem = Convert.ToInt32(Convert.ToBoolean(pstrMakeItem));

			DataTable dtbDataSource = GetInOutStockWithCostData(pstrCCNID, pstrYear, pstrMonth, pstrLocationIDList, pstrCategoryIDList, pstrProductIDList, intMakeItem, pstrBinType);

			//Create builder object
			ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();				
								
			//Set report name
			reportBuilder.ReportName = REPORT_NAME;

			//Set Datasource
			reportBuilder.SourceDataTable = dtbDataSource;
								
			//Set report layout location
			reportBuilder.ReportDefinitionFolder = mstrReportDefinitionFolder;
			reportBuilder.ReportLayoutFile = REPORT_TEMPLATE;				
				
			reportBuilder.UseLayoutFile = true;
			reportBuilder.MakeDataTableForRender();

			// and show it in preview dialog				
			PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
								
			//Attach report viewer
			reportBuilder.ReportViewer = printPreview.ReportViewer;				
			reportBuilder.RenderReport();

			reportBuilder.DrawPredefinedField(RPT_COMPANY_FLD, GetCompanyFullName());
			//Draw parameters				
			System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				
			arrParamAndValue.Add(RPT_CCN_FLD, GetCCNInfoByID(pstrCCNID));
			arrParamAndValue.Add(RPT_YEAR_FLD, pstrYear);
			arrParamAndValue.Add(RPT_MONTH_FLD, pstrMonth);

			if (pstrLocationIDList != null && pstrLocationIDList != string.Empty)
				arrParamAndValue.Add(RPT_LOCATION_FLD, GetLocationInfo(pstrLocationIDList));
				
			if(pstrCategoryIDList != null && pstrCategoryIDList != string.Empty)
				arrParamAndValue.Add(RPT_CATEGORY_FLD, GetCategoryInfo(pstrCategoryIDList));

			if(pstrProductIDList != null && pstrProductIDList != string.Empty)
				arrParamAndValue.Add(RPT_PART_NO_FLD, GetProductInfo(pstrProductIDList));
				
			//Anchor the Parameter drawing canvas cordinate to the fldTitle
			C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FLD);
			double dblStartX = fldTitle.Left;
			double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
			reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
			reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);
								
			try
			{
				printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FLD).Text;
			}
			catch
			{}

			reportBuilder.RefreshReport();
			printPreview.Show();

			//return table
			return dtbDataSource;	
		}
		
		#endregion Public Method			
	}
}
