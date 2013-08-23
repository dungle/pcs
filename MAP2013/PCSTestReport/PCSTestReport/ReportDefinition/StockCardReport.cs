using System;
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
using PCSUtils.Utils;

namespace StockCardReport
{
	[Serializable]
	public class StockCardReport : MarshalByRefObject, IDynamicReport
	{
		public StockCardReport()
		{
		}

		#region Constants
		
		private const string TABLE_NAME = "tbl_OutsideProcessing";	
		private const int DATA_MAX_LENGTH = 250;

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
		
		#region Detailed Item Price By PO Receipt Datat: Tuan TQ
				
		
		DataTable GetStockCardData(string pstrCCNID,
			string pstrMasLocID,
			string pstrLocationIDList,
			string pstrFromDate,
			string pstrToDate,
			string pstrTransTypeIDList,
			string pstrProductIDList
			)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			
            StringBuilder strSqlBuilder = new StringBuilder();

			strSqlBuilder.Append(" SELECT	 ");
			strSqlBuilder.Append(" a.MasLocCode, ");
			strSqlBuilder.Append(" a.LocationID, ");
			strSqlBuilder.Append(" a.LocationCode, ");
			strSqlBuilder.Append(" a.BinCode, ");
			strSqlBuilder.Append(" a.PostDate, ");
			strSqlBuilder.Append(" a.ProductID, ");
			strSqlBuilder.Append(" a.PartNumber, ");
			strSqlBuilder.Append(" a.PartName, ");
			strSqlBuilder.Append(" a.PartModel, ");
			strSqlBuilder.Append(" a.UMCode, ");
			strSqlBuilder.Append(" a.TransactionNo, ");
			strSqlBuilder.Append(" a.TranTypeID, ");
			strSqlBuilder.Append(" a.TranTypeName, ");
			strSqlBuilder.Append(" a.BeginStock, ");
			strSqlBuilder.Append(" a.BeginItemCost, ");
			strSqlBuilder.Append(" a.StockItemCost, ");
			strSqlBuilder.Append(" a.ItemCost,	 ");
			strSqlBuilder.Append(" a.RelatedLocation, ");
			strSqlBuilder.Append(" a.RelatedBin, ");

			strSqlBuilder.Append(" case a.TransactionType ");
	   		strSqlBuilder.Append(" when " + (int)PCSComUtils.Common.TransactionHistoryType.In + " then a.Quantity ");		     
			strSqlBuilder.Append(" when " + (int)PCSComUtils.Common.TransactionHistoryType.Both + " then  ");
			strSqlBuilder.Append(" case when a.Quantity >= 0 then a.Quantity ");
			strSqlBuilder.Append(" 	else null ");
			strSqlBuilder.Append(" end ");
			strSqlBuilder.Append(" else null  ");
			strSqlBuilder.Append(" end as InQuantity, ");
	
			strSqlBuilder.Append(" case a.TransactionType ");		
			strSqlBuilder.Append("    when " + (int)PCSComUtils.Common.TransactionHistoryType.In + " then a.Quantity * a.ItemCost ");		
			strSqlBuilder.Append("    when " + (int)PCSComUtils.Common.TransactionHistoryType.Both + " then ");
			strSqlBuilder.Append("       case a.TranTypeCode ");
			strSqlBuilder.Append("     	   when '" + PCSComUtils.Common.TransactionTypeEnum.POPurchaseOrderReceipts.ToString() + "' then ");
			strSqlBuilder.Append(" 	        case when a.Quantity >= 0 then a.TransactionAmount ");
			strSqlBuilder.Append(" 	          else  null ");
			strSqlBuilder.Append("          end ");
			strSqlBuilder.Append(" 	     else ");
			strSqlBuilder.Append(" 	      case when a.Quantity >= 0 then a.Quantity * a.ItemCost ");
			strSqlBuilder.Append(" 	      else null ");
			strSqlBuilder.Append("        end  ");
			strSqlBuilder.Append("        end	    ");
			strSqlBuilder.Append("    else null  ");
			strSqlBuilder.Append(" end as InAmount, ");

			strSqlBuilder.Append(" case a.TransactionType ");
			strSqlBuilder.Append("  when " + (int)PCSComUtils.Common.TransactionHistoryType.Out + " then a.Quantity ");
			strSqlBuilder.Append(" 	when " + (int)PCSComUtils.Common.TransactionHistoryType.Both + " then  ");
			strSqlBuilder.Append(" 	case when a.Quantity <= 0 then ABS(a.Quantity) ");
			strSqlBuilder.Append(" 		else null ");
			strSqlBuilder.Append(" 	   end ");
			strSqlBuilder.Append(" 	else null  ");
			strSqlBuilder.Append(" end as OutQuantity, ");
			
			strSqlBuilder.Append(" case a.TransactionType ");
			strSqlBuilder.Append("    when " + (int)PCSComUtils.Common.TransactionHistoryType.Out + " then ");
			strSqlBuilder.Append("       case a.TranTypeCode ");
			strSqlBuilder.Append("     	   when '" + PCSComUtils.Common.TransactionTypeEnum.POReturnToVendor.ToString() + "' then ");
			strSqlBuilder.Append(" 			   (SELECT SUM(ISNULL((ISNULL(ACD.Amount,0) * ISNULL(ACM.ExchangeRate,0)),0))"
				+ " FROM CST_FreightMaster ACM JOIN CST_FreightDetail ACD ON ACM.FreightMasterID = ACD.FreightMasterID"
				+ " WHERE ACM.ReturnToVendorMasterID = a.RefMasterID"
				+ " AND ACM.PostDate BETWEEN '" + pstrFromDate + "'  AND '" + pstrToDate + "'"
				+ " AND ACD.ProductID = a.ProductID)");
			strSqlBuilder.Append(" 	     else a.Quantity * a.ItemCost end");
			strSqlBuilder.Append("    when " + (int)PCSComUtils.Common.TransactionHistoryType.Both + " then  ");
			strSqlBuilder.Append("       case a.TranTypeCode ");
			strSqlBuilder.Append("     	   when '" + PCSComUtils.Common.TransactionTypeEnum.POPurchaseOrderReceipts.ToString() + "' then ");
			strSqlBuilder.Append(" 	        case when a.Quantity <= 0 then ABS(a.Quantity) * a.ItemCost ");
			strSqlBuilder.Append(" 	          else  null ");
			strSqlBuilder.Append("                     end ");
			strSqlBuilder.Append(" 	   else ");
			strSqlBuilder.Append(" 	      case when a.Quantity <= 0 then ABS(a.Quantity) * a.ItemCost ");
			strSqlBuilder.Append(" 	      else null ");
			strSqlBuilder.Append("                   end   ");
			strSqlBuilder.Append("        end ");
			strSqlBuilder.Append("    else null  ");
			strSqlBuilder.Append(" end as OutAmount ");
		
			strSqlBuilder.Append(" FROM ");
			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" SELECT  DISTINCT  ");
			strSqlBuilder.Append(" MST_TransactionHistory.TranTypeID, ");
			strSqlBuilder.Append(" MST_TransactionHistory.LocationID, ");
			strSqlBuilder.Append(" MST_TransactionHistory.RefMasterID, ");

			strSqlBuilder.Append(" MST_MasterLocation.Code AS MasLocCode, ");	
			strSqlBuilder.Append(" MST_Location.Code AS LocationCode, ");
			strSqlBuilder.Append(" MST_Bin.Code as BinCode, ");
			strSqlBuilder.Append(" MST_TransactionHistory.PostDate, ");
			strSqlBuilder.Append(" ITM_Product.ProductID, ");
			strSqlBuilder.Append(" ITM_Product.Code as PartNumber, ");
			strSqlBuilder.Append(" ITM_Product.Description PartName, ");
			strSqlBuilder.Append(" ITM_Product.Revision PartModel, ");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code AS UMCode, ");

			strSqlBuilder.Append(" MST_TranType.Description as TranTypeName, ");	
			strSqlBuilder.Append(" MST_TranType.Code as TranTypeCode, ");
			strSqlBuilder.Append(" MST_TranType.Type as TransactionType, ");

			strSqlBuilder.Append(" (IV_BinCache.OHQuantity -  ");
			strSqlBuilder.Append(" ( SELECT ISNULL(SUM(TransQuantity), 0)  ");
			strSqlBuilder.Append(" FROM v_TransactionHistory  ");
			strSqlBuilder.Append("   INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID  ");
			strSqlBuilder.Append("   WHERE ProductID = IV_BinCache.ProductID  ");
			strSqlBuilder.Append(" 	AND BinID = IV_BinCache.BinID  ");
			strSqlBuilder.Append(" 	AND PostDate >= '" + pstrFromDate + "' ");
			strSqlBuilder.Append(" 	AND (MST_TranType.Type = " + (int)PCSComUtils.Common.TransactionHistoryType.In);
			strSqlBuilder.Append("  OR MST_TranType.Type = " + (int)PCSComUtils.Common.TransactionHistoryType.Both + ")  ");
			strSqlBuilder.Append(" ) ");
			strSqlBuilder.Append(" + ( SELECT ISNULL(SUM(TransQuantity), 0)  ");
			strSqlBuilder.Append("     FROM v_TransactionHistory INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID  ");
			strSqlBuilder.Append("     WHERE ProductID = IV_BinCache.ProductID  ");
			strSqlBuilder.Append(" 	AND BinID = IV_BinCache.BinID AND PostDate >= '" + pstrFromDate + "' ");
			strSqlBuilder.Append(" 	AND MST_TranType.Type = " + (int)PCSComUtils.Common.TransactionHistoryType.Out + " )  ");
			strSqlBuilder.Append("   )  ");
			strSqlBuilder.Append(" as BeginStock, ");
			
			strSqlBuilder.Append(" ISNULL( ");
            strSqlBuilder.Append(" (   ");
			strSqlBuilder.Append(" Case  ");
			strSqlBuilder.Append(" When ");
			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" SELECT SUM(cost.ActualCost) ");
			strSqlBuilder.Append(" FROM CST_ActualCostHistory cost ");
	    	strSqlBuilder.Append(" 	INNER JOIN  cst_ActCostAllocationMaster cycle ON cycle.ActCostAllocationMasterID = cost.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" WHERE 	cost.ProductID  = ITM_Product.ProductID ");
	        strSqlBuilder.Append(" 	AND (DATEADD(day, -1, '" + pstrFromDate + "') BETWEEN cycle.FromDate AND DATEADD(second, -1, DATEADD(day, 1, cycle.ToDate))) ");
			strSqlBuilder.Append(" ) ");
			strSqlBuilder.Append(" Is Not Null Then ");
		    strSqlBuilder.Append(" (SELECT SUM(cost.ActualCost) ");
		    strSqlBuilder.Append(" FROM CST_ActualCostHistory cost ");
			strSqlBuilder.Append(" INNER JOIN  cst_ActCostAllocationMaster cycle ON cycle.ActCostAllocationMasterID = cost.ActCostAllocationMasterID ");
		    strSqlBuilder.Append(" WHERE cost.ProductID  = ITM_Product.ProductID ");
	       	strSqlBuilder.Append("        	AND (DATEADD(day, -1, '" + pstrFromDate + "') BETWEEN cycle.FromDate AND DATEADD(second, -1, DATEADD(day, 1, cycle.ToDate))) ");
		    strSqlBuilder.Append(" ) ");
			strSqlBuilder.Append(" Else  ");
			strSqlBuilder.Append(" (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" FROM CST_STDItemCost  ");
			strSqlBuilder.Append(" WHERE ProductID = ITM_Product.ProductID)		 ");
			strSqlBuilder.Append(" End) ");
			strSqlBuilder.Append(" , 0)  ");
			strSqlBuilder.Append(" as BeginItemCost, ");

			strSqlBuilder.Append(" MST_TransactionHistory.Quantity, ");

			//-- Get item cost based on @ToDate
			strSqlBuilder.Append(" ISNULL( ");
			strSqlBuilder.Append("         (   ");
			strSqlBuilder.Append("     Case  ");
			strSqlBuilder.Append(" 	When ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append(" 	 SELECT SUM(cost.ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory cost ");
			strSqlBuilder.Append("     		INNER JOIN  cst_ActCostAllocationMaster cycle ON cycle.ActCostAllocationMasterID = cost.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE 	cost.ProductID  = ITM_Product.ProductID ");
			strSqlBuilder.Append("         	AND ('" + pstrToDate + "'  BETWEEN cycle.FromDate AND DATEADD(second, -1, DATEADD(day, 1, cycle.ToDate))) ");
			strSqlBuilder.Append(" 	 ) ");
			strSqlBuilder.Append(" 	Is Not Null Then ");
			strSqlBuilder.Append(" 	   (SELECT SUM(cost.ActualCost) ");
			strSqlBuilder.Append(" 	    FROM CST_ActualCostHistory cost ");
			strSqlBuilder.Append(" 		INNER JOIN  cst_ActCostAllocationMaster cycle ON cycle.ActCostAllocationMasterID = cost.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	    WHERE cost.ProductID  = ITM_Product.ProductID ");
			strSqlBuilder.Append("        	       	AND ('" + pstrToDate + "'  BETWEEN cycle.FromDate AND DATEADD(second, -1, DATEADD(day, 1, cycle.ToDate))) ");
			strSqlBuilder.Append(" 	   ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost  ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID)		 ");
			strSqlBuilder.Append("      End) ");
			strSqlBuilder.Append(" , 0)  ");
			strSqlBuilder.Append(" as StockItemCost, ");

			strSqlBuilder.Append(" ISNULL( ");
			strSqlBuilder.Append("     (   ");
			strSqlBuilder.Append("     Case  ");
			strSqlBuilder.Append(" 	When ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append(" 	 SELECT SUM(cost.ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory cost ");
			strSqlBuilder.Append("    		INNER JOIN  cst_ActCostAllocationMaster cycle ON cycle.ActCostAllocationMasterID = cost.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE 	cost.ProductID  = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND (MST_TransactionHistory.PostDate BETWEEN cycle.FromDate AND DATEADD(second, -1, DATEADD(day, 1, cycle.ToDate))) ");

			strSqlBuilder.Append(" 	 ) ");
			strSqlBuilder.Append(" 	Is Not Null Then ");
			strSqlBuilder.Append(" 	   (SELECT SUM(cost.ActualCost) ");
			strSqlBuilder.Append(" 	    FROM CST_ActualCostHistory cost ");
			strSqlBuilder.Append(" 		INNER JOIN  cst_ActCostAllocationMaster cycle ON cycle.ActCostAllocationMasterID = cost.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	    WHERE cost.ProductID  = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		  AND (MST_TransactionHistory.PostDate BETWEEN cycle.FromDate AND DATEADD(second, -1, DATEADD(day, 1, cycle.ToDate))) ");
			strSqlBuilder.Append(" 	   ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost  ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID)		 ");
			strSqlBuilder.Append("      End) ");
			strSqlBuilder.Append(" , 0)  ");
			strSqlBuilder.Append(" as ItemCost, ");

			strSqlBuilder.Append(" case MST_TranType.Code ");
			//     -- 11: PO Receipt
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.POPurchaseOrderReceipts.ToString() + "' then ");
			//	-- Receipt by PO 	
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	   when (SELECT InvoiceMasterID ");
			strSqlBuilder.Append(" 	           FROM PO_PurchaseOrderReceiptMaster ");
			strSqlBuilder.Append(" 		   WHERE PurchaseOrderReceiptID = MST_TransactionHistory.RefMasterID) is null  ");
			strSqlBuilder.Append(" 	   	then ");
			strSqlBuilder.Append(" 		( ");
			strSqlBuilder.Append(" 		( ");
			strSqlBuilder.Append(" 		SELECT ISNULL(po.ExchangeRate, 1) * poi.UnitPrice  ");
			strSqlBuilder.Append(" 		* ABS(MST_TransactionHistory.Quantity) ");
						    
			strSqlBuilder.Append(" 		FROM PO_PurchaseOrderDetail poi  ");
			strSqlBuilder.Append(" 		     INNER JOIN PO_PurchaseOrderMaster po ON po.PurchaseOrderMasterID = poi.PurchaseOrderMasterID      ");
			strSqlBuilder.Append(" 		     INNER JOIN PO_PurchaseOrderReceiptMaster prm ON prm.PurchaseOrderMasterID = po.PurchaseOrderMasterID ");
			strSqlBuilder.Append(" 		     INNER JOIN PO_PurchaseOrderReceiptDetail prd ON prd.PurchaseOrderDetailID = poi.PurchaseOrderDetailID	 ");
			strSqlBuilder.Append(" 			   AND prd.PurchaseOrderReceiptID = prm.PurchaseOrderReceiptID ");
					
			strSqlBuilder.Append(" 		WHERE prm.PurchaseOrderReceiptID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 			AND prd.PurchaseOrderReceiptDetailID = MST_TransactionHistory.RefDetailID ");
			strSqlBuilder.Append(" 		)	 ");		
			strSqlBuilder.Append(" 		+ ");
			strSqlBuilder.Append(" 		( ");
			strSqlBuilder.Append(" 		SELECT 	ISNULL(SUM(cst_FreightDetail.Amount * cst_FreightMaster.ExchangeRate), 0)  ");
			strSqlBuilder.Append(" 		FROM    PO_PurchaseOrderReceiptMaster ");
			strSqlBuilder.Append(" 			INNER JOIN cst_FreightMaster ON cst_FreightMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID ");
			strSqlBuilder.Append(" 			INNER JOIN cst_FreightDetail ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID ");
			strSqlBuilder.Append(" 		WHERE PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 		      AND cst_FreightDetail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		      AND cst_FreightMaster.ACPurposeID = 3 ");// -- Credit
			strSqlBuilder.Append(" 		)	 ");		
			strSqlBuilder.Append(" 		- ");
					
			strSqlBuilder.Append(" 		( ");
			strSqlBuilder.Append(" 		SELECT 	ISNULL(SUM(cst_FreightDetail.Amount * cst_FreightMaster.ExchangeRate), 0)  ");
			strSqlBuilder.Append(" 		FROM    PO_PurchaseOrderReceiptMaster ");
			strSqlBuilder.Append(" 			INNER JOIN cst_FreightMaster ON cst_FreightMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID ");
			strSqlBuilder.Append(" 			INNER JOIN cst_FreightDetail ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID ");
			strSqlBuilder.Append(" 		WHERE PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 		      AND cst_FreightDetail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		      AND cst_FreightMaster.ACPurposeID = 4 ");// -- Debit ");
			strSqlBuilder.Append(" 		) ");
			strSqlBuilder.Append(" 		) ");
			//	   -- Receipt by invoice
			strSqlBuilder.Append(" 	   else  ");
			strSqlBuilder.Append(" 		( ");
			strSqlBuilder.Append(" 		SELECT TOP 1 (ISNULL(inv.ExchangeRate, 1) * invd.UnitPrice * ABS(MST_TransactionHistory.Quantity)) ");
			strSqlBuilder.Append(" 		 + ISNULL(inv.ExchangeRate, 1) * invd.Inland ");
			strSqlBuilder.Append(" 		 + ISNULL(inv.ExchangeRate, 1) * invd.ImportTaxAmount ");
					
			strSqlBuilder.Append(" 		FROM PO_InvoiceDetail invd ");
			strSqlBuilder.Append(" 		     INNER JOIN dbo.PO_InvoiceMaster inv ON inv.InvoiceMasterID = invd.InvoiceMasterID      ");
			strSqlBuilder.Append(" 		     INNER JOIN PO_PurchaseOrderReceiptMaster prm ON prm.InvoiceMasterID = inv.InvoiceMasterID ");
			strSqlBuilder.Append(" 		     INNER JOIN PO_PurchaseOrderReceiptDetail prd ON prd.PurchaseOrderDetailID = invd.PurchaseOrderDetailID	 ");
			strSqlBuilder.Append(" 			   AND prd.PurchaseOrderReceiptID = prm.PurchaseOrderReceiptID ");
					
			strSqlBuilder.Append(" 		WHERE prm.PurchaseOrderReceiptID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 			AND prd.PurchaseOrderReceiptDetailID = MST_TransactionHistory.RefDetailID ");
			strSqlBuilder.Append(" 		) ");
					
			strSqlBuilder.Append(" 		+ ");
			strSqlBuilder.Append(" 		( ");
			strSqlBuilder.Append(" 		SELECT 	ISNULL(SUM(cst_FreightDetail.Amount * cst_FreightMaster.ExchangeRate), 0)  ");
			strSqlBuilder.Append(" 		FROM    PO_PurchaseOrderReceiptMaster ");
			strSqlBuilder.Append(" 			INNER JOIN cst_FreightMaster ON cst_FreightMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID ");
			strSqlBuilder.Append(" 			INNER JOIN cst_FreightDetail ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID ");
			strSqlBuilder.Append(" 		WHERE PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 		      AND cst_FreightDetail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		      AND cst_FreightMaster.ACPurposeID <> 3  ");//-- Credit, Freight, Import Tax
			strSqlBuilder.Append(" 		) ");
					
			strSqlBuilder.Append(" 		- ");
					
			strSqlBuilder.Append(" 		( ");
			strSqlBuilder.Append(" 		SELECT 	ISNULL(SUM(cst_FreightDetail.Amount * cst_FreightMaster.ExchangeRate), 0)  ");
			strSqlBuilder.Append(" 		FROM    PO_PurchaseOrderReceiptMaster ");
			strSqlBuilder.Append(" 			INNER JOIN cst_FreightMaster ON cst_FreightMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID ");
			strSqlBuilder.Append(" 			INNER JOIN cst_FreightDetail ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID ");
			strSqlBuilder.Append(" 		WHERE PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 		      AND cst_FreightDetail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		      AND cst_FreightMaster.ACPurposeID = 4 ");// -- Debit);
			strSqlBuilder.Append(" 		) ");

			strSqlBuilder.Append(" 	end	      ");
			strSqlBuilder.Append(" End as TransactionAmount, ");

			strSqlBuilder.Append(" case MST_TranType.Code ");
			//     -- SOReturnGoodsReceive	
			strSqlBuilder.Append("      When '" + PCSComUtils.Common.TransactionTypeEnum.SOReturnGoodsReceive.ToString() + "' then  ");
			strSqlBuilder.Append(" 	(Select ReturnedGoodsNumber ");
			strSqlBuilder.Append(" 	 From SO_ReturnedGoodsMaster ");
			strSqlBuilder.Append(" 	 Where ReturnedGoodsMasterID = MST_TransactionHistory.RefMasterID) ");
				
			//     -- SOCancelCommitment
			strSqlBuilder.Append("      When '" + PCSComUtils.Common.TransactionTypeEnum.SOCancelCommitment.ToString() + "' then  ");
			strSqlBuilder.Append(" 	(Select CommitmentNo ");
			strSqlBuilder.Append(" 	 From SO_CommitInventoryMaster ");
			strSqlBuilder.Append(" 	 Where CommitInventoryMasterID = MST_TransactionHistory.RefMasterID) ");
				
			//     -- POPurchaseOrderReceipts		
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.POPurchaseOrderReceipts.ToString() + "' then ");
			strSqlBuilder.Append("              (SELECT ReceiveNo ");
			strSqlBuilder.Append(" 	            FROM    PO_PurchaseOrderReceiptMaster			 ");
			strSqlBuilder.Append(" 				WHERE PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 				) ");

			//     --12 POReturnToVendor
			strSqlBuilder.Append("      When '" + PCSComUtils.Common.TransactionTypeEnum.POReturnToVendor.ToString() + "' then  ");
			strSqlBuilder.Append("		(Select RTVNo ");
			strSqlBuilder.Append(" 		 From dbo.PO_ReturnToVendorMaster ");
			strSqlBuilder.Append(" 		 Where ReturnToVendorMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("       ) ");

			//     --13 IVMaterialReceipt
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.IVMaterialReceipt.ToString() + "' then  ");
			strSqlBuilder.Append(" 			(Select TransNo ");
			strSqlBuilder.Append(" 			From dbo.IV_MaterialReceipt ");
			strSqlBuilder.Append(" 			Where MaterialReceiptID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("          ) ");

			//     --14 IVMaterialIssue
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.IVMaterialIssue.ToString() + "' then  ");
			strSqlBuilder.Append(" 	(Select TransNo ");
			strSqlBuilder.Append(" 	 From dbo.IV_MaterialIssue ");
			strSqlBuilder.Append(" 	 Where MaterialIssueMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("              ) ");
				    
			//     --17 IVInventoryAdjustment
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.IVInventoryAdjustment.ToString() + "' then  ");	
			strSqlBuilder.Append(" 			(Select TransNo ");
			strSqlBuilder.Append(" 			From dbo.IV_Adjustment ");
			strSqlBuilder.Append(" 			Where AdjustmentID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("          )	      ");
				    
			//     --19 PROWorkOrderCompletion
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.PROWorkOrderCompletion.ToString() + "' then  ");
			strSqlBuilder.Append("         (Select WOCompletionNo ");
			strSqlBuilder.Append(" 			From dbo.PRO_WorkOrderCompletion ");
			strSqlBuilder.Append(" 			Where WorkOrderCompletionID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("          )	 ");

			//     --21 PROIssueMaterial
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.PROIssueMaterial.ToString() + "' then  ");
			strSqlBuilder.Append(" 		(Select IssueNo ");
			strSqlBuilder.Append(" 		From dbo.PRO_IssueMaterialMaster ");
			strSqlBuilder.Append(" 		Where IssueMaterialMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("		) ");

			//     --22 ShippingManagement
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.ShippingManagement.ToString() + "' then  ");
			strSqlBuilder.Append(" 		(Select ConfirmShipNo ");
			strSqlBuilder.Append(" 		From dbo.SO_ConfirmShipMaster ");
			strSqlBuilder.Append(" 		Where ConfirmShipMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("      ) ");
				
			//     --23 SOCommitment
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.SOCommitment.ToString() + "' then  ");
			strSqlBuilder.Append(" 		 (Select CommitmentNo ");
			strSqlBuilder.Append(" 		 From dbo.SO_CommitInventoryMaster ");
			strSqlBuilder.Append(" 		 Where CommitInventoryMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("      ) ");

			//     --24 IVMiscellaneousIssue
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.IVMiscellaneousIssue.ToString() + "' then  ");
			strSqlBuilder.Append(" 		(Select TransNo ");
			strSqlBuilder.Append(" 		From dbo.IV_MiscellaneousIssueMaster ");
			strSqlBuilder.Append(" 		Where MiscellaneousIssueMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("              ) ");

			//     --25 RecoverableMaterial
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.RecoverableMaterial.ToString() + "' then  ");
			strSqlBuilder.Append(" 		(Select TransNo ");
			strSqlBuilder.Append(" 		From dbo.CST_RecoverMaterialMaster ");
			strSqlBuilder.Append(" 		Where RecoverMaterialMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("      ) ");

			//     --26 ShippingAdjustment
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.ShippingAdjustment.ToString() + "'  then  ");
			strSqlBuilder.Append(" 	(Select ConfirmShipNo ");
			strSqlBuilder.Append(" 	 From dbo.SO_ConfirmShipMaster ");
			strSqlBuilder.Append(" 	 Where ConfirmShipMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("            )		      ");
				
			strSqlBuilder.Append(" End as TransactionNo, ");

			strSqlBuilder.Append(" case MST_TranType.Code ");
			//     --21 PROIssueMaterial
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.PROIssueMaterial.ToString() + "' then		 ");
			strSqlBuilder.Append(" 	Case  ");
			//                -- Get From location Code
			strSqlBuilder.Append(" 	    when MST_TransactionHistory.Quantity >= 0 then ");
			strSqlBuilder.Append(" 		(SELECT Code ");
			strSqlBuilder.Append(" 		  FROM MST_Location loc ");
			strSqlBuilder.Append("      		  INNER JOIN PRO_IssueMaterialDetail ON PRO_IssueMaterialDetail.LocationID = loc.LocationID ");
			strSqlBuilder.Append(" 		  WHERE PRO_IssueMaterialDetail.IssueMaterialDetailID = MST_TransactionHistory.RefDetailID ");
			strSqlBuilder.Append(" 		) ");
			strSqlBuilder.Append(" 	     Else ");
			//	    	-- Get To location Code
			strSqlBuilder.Append(" 		(SELECT Code ");
			strSqlBuilder.Append(" 		  FROM MST_Location loc ");
			strSqlBuilder.Append("      		  INNER JOIN PRO_IssueMaterialMaster ON PRO_IssueMaterialMaster.ToLocationID = loc.LocationID ");
			strSqlBuilder.Append(" 		  WHERE PRO_IssueMaterialMaster.IssueMaterialMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("                      ) ");
			strSqlBuilder.Append(" 	End ");

			//     --24 IVMiscellaneousIssue
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.IVMiscellaneousIssue.ToString() + "' then  ");
			strSqlBuilder.Append(" 	Case  ");
			//                -- Get From location Code
			strSqlBuilder.Append(" 	    when MST_TransactionHistory.Quantity >= 0 then ");
			strSqlBuilder.Append(" 		(SELECT Code ");
			strSqlBuilder.Append(" 		  FROM MST_Location loc ");
			strSqlBuilder.Append("      		  INNER JOIN IV_MiscellaneousIssueMaster ON IV_MiscellaneousIssueMaster.SourceLocationID = loc.LocationID ");
			strSqlBuilder.Append(" 		  WHERE IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 		) ");
			strSqlBuilder.Append(" 	     Else ");
			//	    	-- Get To location Code
			strSqlBuilder.Append(" 		(SELECT Code ");
			strSqlBuilder.Append(" 		  FROM MST_Location loc ");
			strSqlBuilder.Append("      		  INNER JOIN IV_MiscellaneousIssueMaster ON IV_MiscellaneousIssueMaster.DesLocationID = loc.LocationID ");
			strSqlBuilder.Append(" 		  WHERE IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 		) ");
			strSqlBuilder.Append(" 	End ");
				    
			//     --25 RecoverableMaterial
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.RecoverableMaterial.ToString() + "' then  ");
			strSqlBuilder.Append(" 	Case ");
			//                -- Get From location Code
			strSqlBuilder.Append(" 	    when MST_TransactionHistory.Quantity >= 0 then ");
			strSqlBuilder.Append(" 		(SELECT Code ");
			strSqlBuilder.Append(" 		  FROM MST_Location loc ");
			strSqlBuilder.Append("      		  INNER JOIN CST_RecoverMaterialMaster ON CST_RecoverMaterialMaster.FromLocationID = loc.LocationID ");
			strSqlBuilder.Append(" 		  WHERE CST_RecoverMaterialMaster.RecoverMaterialMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 		) ");
			strSqlBuilder.Append(" 	     Else ''		 ");
			strSqlBuilder.Append(" 	End ");
			strSqlBuilder.Append("      Else '' ");
			strSqlBuilder.Append(" End as RelatedLocation, ");

			strSqlBuilder.Append(" case MST_TranType.Code ");
			//     --21 PROIssueMaterial
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.PROIssueMaterial.ToString() + "' then		 ");
			strSqlBuilder.Append(" 	Case  ");
			//                -- Get From location Code
			strSqlBuilder.Append(" 	    when MST_TransactionHistory.Quantity >= 0 then ");
			strSqlBuilder.Append(" 		(SELECT Code ");
			strSqlBuilder.Append(" 		  FROM MST_Bin bin ");
			strSqlBuilder.Append("      		  INNER JOIN PRO_IssueMaterialDetail ON PRO_IssueMaterialDetail.BinID = bin.BinID ");
			strSqlBuilder.Append(" 		  WHERE PRO_IssueMaterialDetail.IssueMaterialDetailID = MST_TransactionHistory.RefDetailID ");
			strSqlBuilder.Append(" 		) ");
			strSqlBuilder.Append(" 	     Else ");
			//	    	-- Get To location Code
			strSqlBuilder.Append(" 		(SELECT Code ");
			strSqlBuilder.Append(" 		  FROM MST_Bin bin ");
			strSqlBuilder.Append("      		  INNER JOIN PRO_IssueMaterialMaster ON PRO_IssueMaterialMaster.ToBinID = bin.BinID ");
			strSqlBuilder.Append(" 		  WHERE PRO_IssueMaterialMaster.IssueMaterialMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append("                      ) ");
			strSqlBuilder.Append(" 	End ");

			//     --24 IVMiscellaneousIssue
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.IVMiscellaneousIssue.ToString() + "' then  ");
			strSqlBuilder.Append(" 	Case  ");
			//                -- Get From location Code
			strSqlBuilder.Append(" 	    when MST_TransactionHistory.Quantity >= 0 then ");
			strSqlBuilder.Append(" 		(SELECT Code ");
			strSqlBuilder.Append(" 		  FROM MST_Bin bin ");
			strSqlBuilder.Append("      		  INNER JOIN IV_MiscellaneousIssueMaster ON IV_MiscellaneousIssueMaster.SourceBinID = bin.BinID ");
			strSqlBuilder.Append(" 		  WHERE IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 		) ");
			strSqlBuilder.Append(" 	     Else ");
			//	    	-- Get To location Code
			strSqlBuilder.Append(" 		(SELECT Code ");
			strSqlBuilder.Append(" 		  FROM MST_Bin bin ");
			strSqlBuilder.Append("     		  INNER JOIN IV_MiscellaneousIssueMaster ON IV_MiscellaneousIssueMaster.DesBinID = bin.BinID ");
			strSqlBuilder.Append(" 		  WHERE IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 		) ");
			strSqlBuilder.Append(" 	End ");
				    
			//     --25 RecoverableMaterial
			strSqlBuilder.Append("      when '" + PCSComUtils.Common.TransactionTypeEnum.RecoverableMaterial.ToString() + "' then  ");
			strSqlBuilder.Append(" 	Case ");
			//     -- Get From location Code
			strSqlBuilder.Append(" 	    when MST_TransactionHistory.Quantity >= 0 then ");
			strSqlBuilder.Append(" 		(SELECT Code ");
			strSqlBuilder.Append(" 		  FROM MST_Bin bin ");
			strSqlBuilder.Append("      		  INNER JOIN CST_RecoverMaterialMaster ON CST_RecoverMaterialMaster.FromBinID = bin.BinID ");
			strSqlBuilder.Append(" 		  WHERE CST_RecoverMaterialMaster.RecoverMaterialMasterID = MST_TransactionHistory.RefMasterID ");
			strSqlBuilder.Append(" 		) ");
			strSqlBuilder.Append(" 	     Else ''		 ");
			strSqlBuilder.Append(" 	End ");
			strSqlBuilder.Append("      Else '' ");
			strSqlBuilder.Append(" End as RelatedBin ");

			strSqlBuilder.Append(" FROM    MST_TransactionHistory ");
			strSqlBuilder.Append(" INNER JOIN IV_BinCache ON MST_TransactionHistory.ProductID = IV_BinCache.ProductID ");
			strSqlBuilder.Append(" AND MST_TransactionHistory.BinID = IV_BinCache.BinID ");

			strSqlBuilder.Append(" INNER JOIN MST_TranType ON MST_TransactionHistory.TranTypeID = MST_TranType.TranTypeID  ");
			strSqlBuilder.Append(" INNER JOIN MST_Location ON MST_TransactionHistory.LocationID = MST_Location.LocationID  ");
			strSqlBuilder.Append(" INNER JOIN MST_Bin ON MST_TransactionHistory.BinID = MST_Bin.BinID  ");
			strSqlBuilder.Append(" INNER JOIN MST_MasterLocation ON MST_TransactionHistory.MasterLocationID = MST_MasterLocation.MasterLocationID ");
			strSqlBuilder.Append(" INNER JOIN ITM_Product ON MST_TransactionHistory.ProductID = ITM_Product.ProductID ");

			strSqlBuilder.Append(" LEFT JOIN MST_UnitOfMeasure ON MST_TransactionHistory.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID ");

			//WHERE clause
			strSqlBuilder.Append(" WHERE (MST_TranType.Type IN (" + (int)PCSComUtils.Common.TransactionHistoryType.Out + ",");
			strSqlBuilder.Append((int)PCSComUtils.Common.TransactionHistoryType.In + ",");
			strSqlBuilder.Append((int)PCSComUtils.Common.TransactionHistoryType.Both + "))");

			strSqlBuilder.Append(" AND (MST_TransactionHistory.CCNID = " + pstrCCNID + ")" );
			strSqlBuilder.Append(" AND (MST_TransactionHistory.MasterLocationID = " + pstrMasLocID + ")" );
			strSqlBuilder.Append(" AND (MST_TransactionHistory.PostDate BETWEEN '" + pstrFromDate + "'  AND '" + pstrToDate + "' )	 ");
            
			if(pstrLocationIDList != string.Empty && pstrLocationIDList != null)
			{
				strSqlBuilder.Append("        AND (MST_TransactionHistory.LocationID IN (" +  pstrLocationIDList + "))");
			}

			if(pstrProductIDList != string.Empty && pstrProductIDList != null)
			{
				strSqlBuilder.Append("        AND (MST_TransactionHistory.ProductID IN (" +  pstrProductIDList + "))");
			}

			if(pstrTransTypeIDList != string.Empty && pstrTransTypeIDList != null)
			{
				strSqlBuilder.Append("        AND (MST_TransactionHistory.TranTypeID IN (" +  pstrTransTypeIDList + "))");
			}

			strSqlBuilder.Append(" ) a ");

			strSqlBuilder.Append(" ORDER BY a.MasLocCode, ");
			strSqlBuilder.Append(" 	 a.LocationCode, ");
			strSqlBuilder.Append(" 	 a.BinCode, ");
			strSqlBuilder.Append(" 	 a.PostDate ");

			//Write SQL string for debugging
			Console.WriteLine(strSqlBuilder.ToString());
			
			oconPCS = new OleDbConnection(mConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);

			ocmdPCS.CommandTimeout = 1000;
			ocmdPCS.Connection.Open();
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
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
		/// Get Master Location Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetMasLocInfoByID(string pstrID)
		{
			const string NAME_FLD = "Name";

			string strResult = string.Empty;
			OleDbConnection oconPCS = null;
			
			try
			{
				OleDbDataReader odrPCS = null;
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT Code, Name"
					+ " FROM MST_MasterLocation"
					+ " WHERE MasterLocationID = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[PRODUCT_CODE_FLD].ToString().Trim() + " (" + odrPCS[NAME_FLD].ToString().Trim() + ")";
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

				string strSql =	"SELECT Code";
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

				if(strResult.Length > DATA_MAX_LENGTH)				
				{
					int i = strResult.IndexOf(SEMI_COLON, DATA_MAX_LENGTH);
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
		private string GetTransTypeInfo(string pstrIDList)
		{			
			const string SEMI_COLON = "; ";
			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT Description";
				strSql += " FROM MST_TranType";
				strSql += " WHERE TranTypeID IN (" +  pstrIDList + ")";
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					while(odrPCS.Read())
					{
						strResult += odrPCS[PRODUCT_NAME_FLD].ToString().Trim() + SEMI_COLON;
					}
				}

				if(strResult.Length > DATA_MAX_LENGTH)				
				{
					int i = strResult.IndexOf(SEMI_COLON, DATA_MAX_LENGTH);
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
		/// Get Product Code List
		/// </summary>
		/// <param name="pstrIDList"></param>
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

				string strSql =	"SELECT Code";
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

				if(strResult.Length > DATA_MAX_LENGTH)				
				{
					int i = strResult.IndexOf(SEMI_COLON, DATA_MAX_LENGTH);
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
		

		#endregion Delivery To Customer Schedule Report: Tuan TQ
		
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
		public DataTable ExecuteReport(string pstrCCNID,
			string pstrMasLocID,
			string pstrLocationIDList,
			string pstrFromDate,
			string pstrToDate,
			string pstrTransTypeIDList,
			string pstrProductIDList
			)
		{
			try
			{
				string strPOTypeID = Convert.ToString((int)PCSComUtils.Common.POReceiptTypeEnum.ByInvoice);

				const string DATE_HOUR_FORMAT = "dd-MM-yyyy HH:mm";				

				const string REPORT_TEMPLATE = "StockCardReport.xml";
				const string RPT_PAGE_HEADER = "PageHeader";

				const string REPORT_NAME = "StockCardReport";
				const string RPT_TITLE_FLD = "fldTitle";
				const string RPT_COMPANY_FLD = "fldCompany";
				
				const string RPT_CCN_FLD = "CCN";		
				const string RPT_MASTER_LOCATION_FLD = "Master Location";
				const string RPT_LOCATION_FLD = "Location";
				const string RPT_FROM_DATE_FLD = "From Date";
				const string RPT_TO_DATE_FLD = "To Date";
				const string RPT_PART_NO_FLD = "Part No.";
				const string RPT_TRANSACTION_TYPE_FLD = "Trans. Type";
				
				DataTable dtbDataSource = null;

				dtbDataSource = GetStockCardData(pstrCCNID,
												pstrMasLocID,
												pstrLocationIDList,
												pstrFromDate,
												pstrToDate,
												pstrTransTypeIDList,
												pstrProductIDList);

				//Create builder object
				ReportBuilder reportBuilder = new ReportBuilder();				
								
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
				arrParamAndValue.Add(RPT_MASTER_LOCATION_FLD, GetMasLocInfoByID(pstrMasLocID));

                //Location list
				if(pstrLocationIDList != null && pstrLocationIDList != string.Empty)
				{
					arrParamAndValue.Add(RPT_LOCATION_FLD, GetLocationInfo(pstrLocationIDList));
				}

				//From date
				if(pstrFromDate != null && pstrFromDate != string.Empty)
				{
					arrParamAndValue.Add(RPT_FROM_DATE_FLD, DateTime.Parse(pstrFromDate).ToString(DATE_HOUR_FORMAT));
				}

				//To date
				if(pstrToDate != null && pstrToDate != string.Empty)
				{
					arrParamAndValue.Add(RPT_TO_DATE_FLD, DateTime.Parse(pstrToDate).ToString(DATE_HOUR_FORMAT));
				}

				//Part no list
				if(pstrProductIDList != null && pstrProductIDList != string.Empty)
				{
					arrParamAndValue.Add(RPT_PART_NO_FLD, GetProductInfo(pstrProductIDList));
				}

				//Trans. type list
				if(pstrTransTypeIDList != null && pstrTransTypeIDList != string.Empty)
				{
					arrParamAndValue.Add(RPT_TRANSACTION_TYPE_FLD, GetTransTypeInfo(pstrTransTypeIDList));
				}
				
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
			catch (Exception ex)
			{
				throw ex;
			}			
		}
		
		#endregion Public Method			
	}
}
