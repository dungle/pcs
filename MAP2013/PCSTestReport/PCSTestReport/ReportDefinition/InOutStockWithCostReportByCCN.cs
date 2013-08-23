using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Text;
using C1.Win.C1Preview;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace InOutStockWithCostReportByCCN
{
	[Serializable]
	public class InOutStockWithCostReportByCCN : MarshalByRefObject, IDynamicReport
	{
		public InOutStockWithCostReportByCCN()
		{
		}

		#region Constants
		private const int MAX_LENGTH = 130;
		private const string PRODUCT_CODE_FLD = "Code";
		
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
		
		#region In Out Stock With Cost Report Classify by CCN
		
		DataTable GetInOutStockWithCostData(string pstrYear, string pstrMonth, string pstrCategoryIDList, string pstrProductIDList, int pintMakeItem, string pstrProductType)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DateTime dtmMonth = new DateTime(Convert.ToInt32(pstrYear), Convert.ToInt32(pstrMonth), 1);
				string strFormat = "yyyy-MM-dd HH:mm:ss";
				
				StringBuilder strSqlBuilder = new StringBuilder();
				strSqlBuilder.Append(" 	DECLARE @PeriodID int, @FromDate datetime,@FromDateStr nvarchar(100),@ToDate datetime");
				strSqlBuilder.Append(" 	SET @FromDateStr = '" + dtmMonth.ToString(strFormat) + "'");
				strSqlBuilder.Append(" 	SET @FromDate = CAST(SUBSTRING(@FromDateStr, 1, 4) + '-' + SUBSTRING(@FromDateStr, 6, 2) + '-' + SUBSTRING(@FromDateStr, 9, 2) AS datetime)");
				strSqlBuilder.Append(" 	SET @FromDate = DATEADD(day, -DATEPART(day,@FromDate) + 1, @FromDate)");
				strSqlBuilder.Append(" 	SET @ToDate = DATEADD(month, 1, @FromDate)");
				strSqlBuilder.Append(" 	SET @ToDate = DATEADD(day, -1, @ToDate)");
				strSqlBuilder.Append(" 	SELECT @PeriodID  = ActCostAllocationMasterID");
				strSqlBuilder.Append(" 	FROM cst_ActCostAllocationMaster");
				strSqlBuilder.Append(" 	WHERE FromDate <= @FromDate");
				strSqlBuilder.Append(" 	AND ToDate >= @ToDate");
				strSqlBuilder.Append(" 	SELECT T.* FROM");
				strSqlBuilder.Append(" 	(");
				strSqlBuilder.Append(" 	SELECT  DISTINCT P.ProductID, C.Code as Category, ");
				strSqlBuilder.Append(" 	P.Code as PartNo, P.Description as PartName, P.Revision as Model,  ");
				strSqlBuilder.Append(" 	U.Code as StockUM, SUM(ISNULL(A.ActualCost,0)) AS AverageCost,");
//				strSqlBuilder.Append(" 	-- begin stock");
				strSqlBuilder.Append(" 	ISNULL(A.BeginQuantity,0) as BeginQuantity,  ");
//				strSqlBuilder.Append(" 	-- begin value");
				strSqlBuilder.Append(" 	ISNULL(A.BeginQuantity,0) *  ISNULL(SUM(ISNULL(A.BeginCost,0)),0) as BeginValue, ");
//				strSqlBuilder.Append(" 	-- in quantity");
				strSqlBuilder.Append(" 	(ISNULL(A.Quantity,0) - ISNULL(A.BeginQuantity,0)");
//				strSqlBuilder.Append(" 	-- inventory adjustment");
				strSqlBuilder.Append(" 	+ ISNULL((SELECT SUM(ISNULL(AdjustQuantity,0))");
				strSqlBuilder.Append(" 	FROM IV_Adjustment");
				strSqlBuilder.Append(" 	WHERE ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1");
				strSqlBuilder.Append(" 	AND ISNULL(AdjustQuantity,0) > 0),0)");
//				strSqlBuilder.Append(" 	-- recover detail");
				strSqlBuilder.Append(" 	+ ISNULL((SELECT SUM(ISNULL(RecoverQuantity,0))");
				strSqlBuilder.Append(" 	FROM CST_RecoverMaterialDetail RD JOIN CST_RecoverMaterialMaster RM");
				strSqlBuilder.Append(" 	ON RD.RecoverMaterialMasterID = RM.RecoverMaterialMasterID");
				strSqlBuilder.Append(" 	WHERE RD.ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1),0)");
//				strSqlBuilder.Append(" 	-- return goods receive");
				strSqlBuilder.Append(" 	+ ISNULL((SELECT SUM(ISNULL(ReceiveQuantity,0))");
				strSqlBuilder.Append(" 	FROM SO_ReturnedGoodsDetail RD JOIN SO_ReturnedGoodsMaster RM");
				strSqlBuilder.Append(" 	ON RD.ReturnedGoodsMasterID = RM.ReturnedGoodsMasterID");
				strSqlBuilder.Append(" 	WHERE RD.ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1),0)) AS InQuantity, ");
				strSqlBuilder.Append(" 	");
//				strSqlBuilder.Append(" 	-- in value");
				strSqlBuilder.Append(" 	(SUM(ISNULL(A.Quantity,0) * ISNULL(A.ActualCost,0))");
				strSqlBuilder.Append(" 	- SUM(ISNULL(A.BeginQuantity,0) * ISNULL(A.BeginCost,0))");
				strSqlBuilder.Append(" 	+ (ISNULL((SELECT SUM(ISNULL(AdjustQuantity,0))");
				strSqlBuilder.Append(" 	FROM IV_Adjustment");
				strSqlBuilder.Append(" 	WHERE ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1");
				strSqlBuilder.Append(" 	AND ISNULL(AdjustQuantity,0) > 0),0)");
//				strSqlBuilder.Append(" 	-- recover detail");
				strSqlBuilder.Append(" 	+ ISNULL((SELECT SUM(ISNULL(RecoverQuantity,0))");
				strSqlBuilder.Append(" 	FROM CST_RecoverMaterialDetail RD JOIN CST_RecoverMaterialMaster RM");
				strSqlBuilder.Append(" 	ON RD.RecoverMaterialMasterID = RM.RecoverMaterialMasterID");
				strSqlBuilder.Append(" 	WHERE RD.ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1),0)");
//				strSqlBuilder.Append(" 	-- return goods receive");
				strSqlBuilder.Append(" 	+ ISNULL((SELECT SUM(ISNULL(ReceiveQuantity,0))");
				strSqlBuilder.Append(" 	FROM SO_ReturnedGoodsDetail RD JOIN SO_ReturnedGoodsMaster RM");
				strSqlBuilder.Append(" 	ON RD.ReturnedGoodsMasterID = RM.ReturnedGoodsMasterID");
				strSqlBuilder.Append(" 	WHERE RD.ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1),0)) * SUM(ISNULL(A.ActualCost,0))");
				strSqlBuilder.Append(" 	) as InValue, ");
//				strSqlBuilder.Append(" 	-- out quantity");
				strSqlBuilder.Append(" 	(ISNULL((SELECT SUM(ISNULL(InvoiceQty,0))");
				strSqlBuilder.Append(" 	FROM SO_ConfirmshipDetail CD JOIN SO_ConfirmshipMaster CM");
				strSqlBuilder.Append(" 	ON CD.ConfirmshipMasterID = CM.ConfirmshipMasterID");
				strSqlBuilder.Append(" 	WHERE CD.ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND ShippedDate >= @FromDate");
				strSqlBuilder.Append(" 	AND ShippedDate < @ToDate+1");
				strSqlBuilder.Append(" 	),0)");
//				strSqlBuilder.Append(" 	-- component scrap detail");
				strSqlBuilder.Append(" 	+ ISNULL((SELECT SUM(ISNULL(ScrapQuantity,0))");
				strSqlBuilder.Append(" 	FROM PRO_ComponentScrapDetail CD JOIN PRO_ComponentScrapMaster CM");
				strSqlBuilder.Append(" 	ON CD.ComponentScrapMasterID = CM.ComponentScrapMasterID");
				strSqlBuilder.Append(" 	WHERE CD.ComponentID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1),0)");
				strSqlBuilder.Append(" 	+ ISNULL((SELECT SUM(ISNULL(ABS(AdjustQuantity),0))");
				strSqlBuilder.Append(" 	FROM IV_Adjustment");
				strSqlBuilder.Append(" 	WHERE ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1");
				strSqlBuilder.Append(" 	AND ISNULL(AdjustQuantity,0) < 0),0)");
//				strSqlBuilder.Append(" 	-- misc. issue purpose = destroy");
				strSqlBuilder.Append(" 	+ ISNULL((SELECT SUM(ISNULL(Quantity,0))");
				strSqlBuilder.Append(" 	FROM IV_MiscellaneousIssueDetail MD JOIN IV_MiscellaneousIssueMaster MM");
				strSqlBuilder.Append(" 	ON MD.MiscellaneousIssueMasterID = MM.MiscellaneousIssueMasterID");
				strSqlBuilder.Append(" 	WHERE ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1");
				strSqlBuilder.Append(" 	AND IssuePurposeID = 14),0)");
//				strSqlBuilder.Append(" 	-- work order and po receipt by outside (withdrawval)");
				strSqlBuilder.Append(" 	+ ISNULL((SELECT SUM(ISNULL(ABS(Quantity),0))");
				strSqlBuilder.Append(" 	FROM MST_TransactionHistory");
				strSqlBuilder.Append(" 	WHERE ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1");
				strSqlBuilder.Append(" 	AND TranTypeID IN (11,19)");
				strSqlBuilder.Append(" 	AND Quantity < 0),0)");
				strSqlBuilder.Append(" 	) AS OutQuantity,");
//				strSqlBuilder.Append(" 	-- out value");
				strSqlBuilder.Append(" 	(ISNULL((SELECT SUM(ISNULL(InvoiceQty,0))");
				strSqlBuilder.Append(" 	FROM SO_ConfirmshipDetail CD JOIN SO_ConfirmshipMaster CM");
				strSqlBuilder.Append(" 	ON CD.ConfirmshipMasterID = CM.ConfirmshipMasterID");
				strSqlBuilder.Append(" 	WHERE CD.ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND ShippedDate >= @FromDate");
				strSqlBuilder.Append(" 	AND ShippedDate < @ToDate+1");
				strSqlBuilder.Append(" 	),0)");
//				strSqlBuilder.Append(" 	-- component scrap detail");
				strSqlBuilder.Append(" 	+ ISNULL((SELECT SUM(ISNULL(ScrapQuantity,0))");
				strSqlBuilder.Append(" 	FROM PRO_ComponentScrapDetail CD JOIN PRO_ComponentScrapMaster CM");
				strSqlBuilder.Append(" 	ON CD.ComponentScrapMasterID = CM.ComponentScrapMasterID");
				strSqlBuilder.Append(" 	WHERE CD.ComponentID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1),0)");
				strSqlBuilder.Append(" 	+ ABS(ISNULL((SELECT SUM(ISNULL(AdjustQuantity,0))");
				strSqlBuilder.Append(" 	FROM IV_Adjustment");
				strSqlBuilder.Append(" 	WHERE ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1");
				strSqlBuilder.Append(" 	AND ISNULL(AdjustQuantity,0) < 0),0))");
//				strSqlBuilder.Append(" 	-- misc. issue purpose = destroy");
				strSqlBuilder.Append(" 	+ ISNULL((SELECT SUM(ISNULL(Quantity,0))");
				strSqlBuilder.Append(" 	FROM IV_MiscellaneousIssueDetail MD JOIN IV_MiscellaneousIssueMaster MM");
				strSqlBuilder.Append(" 	ON MD.MiscellaneousIssueMasterID = MM.MiscellaneousIssueMasterID");
				strSqlBuilder.Append(" 	WHERE ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1");
				strSqlBuilder.Append(" 	AND IssuePurposeID = 14),0)");
//				strSqlBuilder.Append(" 	-- work order and po receipt by outside (withdrawval)");
				strSqlBuilder.Append(" 	+ ABS(ISNULL((SELECT SUM(ISNULL(Quantity,0))");
				strSqlBuilder.Append(" 	FROM MST_TransactionHistory");
				strSqlBuilder.Append(" 	WHERE ProductID = P.ProductID");
				strSqlBuilder.Append(" 	AND PostDate >= @FromDate");
				strSqlBuilder.Append(" 	AND PostDate < @ToDate+1");
				strSqlBuilder.Append(" 	AND TranTypeID IN (11,19)");
				strSqlBuilder.Append(" 	AND Quantity < 0");
				strSqlBuilder.Append(" 	),0))) * SUM(ISNULL(A.ActualCost,0))");
				strSqlBuilder.Append(" 	AS OutValue, 0.0000 AS EndQuantity, 0.0000 AS EndValue");
				strSqlBuilder.Append(" 	FROM  	ITM_Product P JOIN CST_ActualCostHistory A");
				strSqlBuilder.Append(" 	ON P.ProductID = A.ProductID");
				strSqlBuilder.Append(" 	INNER JOIN MST_UnitOfMeasure U ON U.UnitOfMeasureID = P.StockUMID");
				strSqlBuilder.Append(" 	LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID");
				strSqlBuilder.Append(" 	WHERE P.CCNID =  1");
				strSqlBuilder.Append(" 	AND A.ActCostAllocationMasterID = @PeriodID");
				if (pstrCategoryIDList != null && pstrCategoryIDList.Trim() != string.Empty)
					strSqlBuilder.Append(" 	AND P.CategoryID IN (" + pstrCategoryIDList + ")  ");
				if (pstrProductIDList != null && pstrProductIDList.Trim() != string.Empty)
					strSqlBuilder.Append(" 	AND P.ProductID IN (" + pstrProductIDList + ")  ");
				if (pstrProductType != null && pstrProductType.Trim() != string.Empty)
					strSqlBuilder.Append(" 	AND P.ProductTypeID IN (" + pstrProductType + ")  ");
				if (pintMakeItem >= 0)
					strSqlBuilder.Append(" 	AND P.MakeItem = " + pintMakeItem);
				strSqlBuilder.Append(" 	GROUP BY 	P.ProductID, C.Code, U.Code, 	P.Code, 	 ");
				strSqlBuilder.Append(" 	P.Revision, P.Description, A.BeginQuantity, A.Quantity) AS T");
				strSqlBuilder.Append(" 	WHERE AverageCost <> 0 OR BeginQuantity <> 0");
				strSqlBuilder.Append(" 	OR InQuantity <> 0 OR OutQuantity <> 0 OR EndQuantity <> 0");


				oconPCS = new OleDbConnection(mConnectionString);

				//ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
				ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
				//End hack

				ocmdPCS.CommandTimeout = 1000;

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
		/// Get Product Info
		/// </summary>
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
		/// Get Category Info
		/// </summary>
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
		public DataTable ExecuteReport(string pstrYear, string pstrMonth, string pstrCategoryIDList, string pstrProductIDList, string pstrMakeItem, string pstrProductType)
		{			
			const string REPORT_TEMPLATE = "InOutReportWithCost.xml";
			
			const string REPORT_NAME = "InOutReportWithCost";
			const string RPT_TITLE_FLD = "fldTitle";
			const string RPT_COMPANY_FLD = "fldCompany";
				
			const string RPT_YEAR_FLD = "fldYear";
			const string RPT_MONTH_FLD = "fldMonth";
			const string RPT_CATEGORY_FLD = "fldCategory";
			const string RPT_PART_NO_FLD = "fldProduct";

			int intMakeItem = -1;
			if (pstrMakeItem != null && pstrMakeItem != string.Empty)
				intMakeItem = Convert.ToInt32(Convert.ToBoolean(pstrMakeItem));

			DataTable dtbDataSource = GetInOutStockWithCostData(pstrYear, pstrMonth, pstrCategoryIDList, pstrProductIDList, intMakeItem, pstrProductType);
			// update end quantity & end value
			foreach (DataRow drowData in dtbDataSource.Rows)
			{
				decimal decBeginQuantity = 0, decBeginValue = 0, decInQuantity = 0, decInValue = 0;
				decimal decOutQuantity = 0, decOutValue = 0, decEndQuantity = 0, decEndValue = 0;
				try
				{
					decBeginQuantity = Convert.ToDecimal(drowData["BeginQuantity"]);
				}
				catch{}
				try
				{
					decBeginValue = Convert.ToDecimal(drowData["BeginValue"]);
				}
				catch{}
				try
				{
					decInQuantity = Convert.ToDecimal(drowData["InQuantity"]);
				}
				catch{}
				try
				{
					decInValue = Convert.ToDecimal(drowData["InValue"]);
				}
				catch{}
				try
				{
					decOutQuantity = Convert.ToDecimal(drowData["OutQuantity"]);
				}
				catch{}
				try
				{
					decOutValue = Convert.ToDecimal(drowData["OutValue"]);
				}
				catch{}
				decEndQuantity = decBeginQuantity + decInQuantity - decOutQuantity;
				decEndValue = decBeginValue + decInValue - decOutValue;
				drowData["EndQuantity"] = decEndQuantity;
				drowData["EndValue"] = decEndValue;
			}

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
			C1PrintPreviewDialog	printPreview = new C1PrintPreviewDialog();
								
			//Attach report viewer
			reportBuilder.ReportViewer = printPreview.ReportViewer;				
			reportBuilder.RenderReport();

			reportBuilder.DrawPredefinedField(RPT_COMPANY_FLD, GetCompanyFullName());
			reportBuilder.DrawPredefinedField(RPT_YEAR_FLD, pstrYear);
			reportBuilder.DrawPredefinedField(RPT_MONTH_FLD, pstrMonth);
			//Draw parameters				
			NameValueCollection arrParamAndValue = new NameValueCollection();
			arrParamAndValue.Add(RPT_YEAR_FLD, pstrYear);
			arrParamAndValue.Add(RPT_MONTH_FLD, pstrMonth);

			if(pstrCategoryIDList != null && pstrCategoryIDList != string.Empty)
				reportBuilder.DrawPredefinedField(RPT_CATEGORY_FLD, GetCategoryInfo(pstrCategoryIDList));

			if(pstrProductIDList != null && pstrProductIDList != string.Empty)
				reportBuilder.DrawPredefinedField(RPT_PART_NO_FLD, GetProductInfo(pstrProductIDList));
				
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
