using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using C1.Win.C1Preview;

using PCSComUtils.Common;
using PCSUtils.Utils;

namespace MonthlyReturnGoodsReceipt
{
	[Serializable]
	public class MonthlyReturnGoodsReceipt : MarshalByRefObject, IDynamicReport
	{
		public MonthlyReturnGoodsReceipt()
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

		#region Processing Data
		
		private const string PRODUCT_CODE = "Code";
		private const string PRODUCT_NAME = "Description";
		private const string PRODUCT_MODEL = "Revision";
		private const string SEMI_COLON = "; ";

		private const int MAX_LENGTH = 250;

		private const string CODE_FIELD = "Code";
		private const string NAME_FIELD = "Name";		

		private DataTable GetMonthlyReturnGoodsReceiptData(string pstrMonth, string pstrYear, string pstrCustomerIDList)
		{
			OleDbConnection cn = null;
			
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbBOMData = new DataTable();
			
				//Build WHERE clause
				string strWhereClause = " WHERE 1 = 1 ";

				//Month
				if(pstrMonth != null && pstrMonth != string.Empty)
				{
					strWhereClause += " AND Month(SO_ReturnedGoodsMaster.TransDate) =" + pstrMonth;
				}

				//To Date
				if(pstrYear != null && pstrYear != string.Empty)
				{
					strWhereClause += " AND Year(SO_ReturnedGoodsMaster.TransDate) =" + pstrYear;
				}
				
				//Customer ID List
				if(pstrCustomerIDList != null && pstrCustomerIDList != string.Empty)
				{
					strWhereClause += " AND MST_Party.PartyID IN (" + pstrCustomerIDList + ") ";
				}

				//Build SQL string
				string strSql = " SELECT  DISTINCT SO_ReturnedGoodsDetail.ReturnedGoodsDetailID, ";
				strSql += " SO_ReturnedGoodsMaster.TransDate as PostDate, ";
				strSql += " SO_ReturnedGoodsMaster.ReturnedGoodsNumber as RGRNo, ";
				strSql += " refDetail.CustomerItemCode PartNo, ";
				strSql += " ITM_Product.ProductID, ";
				strSql += " ITM_Product.Description as PartName, ";
				strSql += " ITM_Product.Revision as PartModel, ";
				strSql += " SO_ReturnedGoodsDetail.ReceiveQuantity as ReturnQuantity, ";
				strSql += " SO_ReturnedGoodsDetail.ReceiveQuantity *  ";
				strSql += " ISNULL(SO_ReturnedGoodsDetail.UnitPrice, 0) as Amount, ";
				strSql += " (SO_ReturnedGoodsDetail.ReceiveQuantity  ";
				strSql += " * ISNULL(SO_ReturnedGoodsDetail.UnitPrice, 0))  ";
				strSql += " * 0.01 ";
				strSql += " * ISNULL(ITM_Product.VAT, 0) as VATAmount, ";		

				strSql += " ISNULL(";
				strSql += " (  ";
				strSql += " Case  ";
				strSql += " When ";
				strSql += "  ( ";
				strSql += "  SELECT SUM(cost.ActualCost) as ActualCost ";
				strSql += "  FROM CST_ActualCostHistory cost ";
				strSql += " 		INNER JOIN  cst_ActCostAllocationMaster cycle ON cycle.ActCostAllocationMasterID = cost.ActCostAllocationMasterID ";
				strSql += "  WHERE 	cost.ProductID  = ITM_Product.ProductID ";
				strSql += "     	AND (Year(cycle.FromDate) <= " + pstrYear + " AND Month(cycle.FromDate) <= " + pstrMonth + ") ";
				strSql += " 	AND (Year(cycle.ToDate) >= " + pstrYear + " AND Month(cycle.ToDate) >= " + pstrMonth + ") ";
				strSql += "  ) ";
				strSql += " Is Not Null Then ";
				strSql += "    (SELECT SUM(cost.ActualCost) as ActualCost ";
				strSql += "     FROM CST_ActualCostHistory cost ";
				strSql += " 	INNER JOIN  cst_ActCostAllocationMaster cycle ON cycle.ActCostAllocationMasterID = cost.ActCostAllocationMasterID ";
				strSql += "     WHERE cost.ProductID  = ITM_Product.ProductID ";
				strSql += "        	 AND (Year(cycle.FromDate) <= " + pstrYear + " AND Month(cycle.FromDate) <= " + pstrMonth + ") ";
				strSql += " 	 AND (Year(cycle.ToDate) >= " + pstrYear + " AND Month(cycle.ToDate) >= " + pstrMonth + ") ";
				strSql += "    ) ";
				strSql += " Else  ";
				strSql += "   (SELECT SUM(Cost)   ";
				strSql += "    FROM CST_STDItemCost  ";
				strSql += "    WHERE ProductID = ITM_Product.ProductID)		 ";
				strSql += "  End) ";
				strSql += " , 0) as ItemCost, ";
/*
				strSql += " ISNULL((SELECT SUM(ISNULL(DSAmount,0) + ISNULL(OH_DSAmount,0))";
				strSql += " FROM CST_DSAndRecycleAllocation charge JOIN CST_ActCostAllocationMaster cycle";
				strSql += " ON charge.ActCostAllocationMasterID = cycle.ActCostAllocationMasterID";
				strSql += " WHERE charge.ProductID = ITM_Product.ProductID";
				strSql += " AND (Year(cycle.FromDate) <= " + pstrYear + " AND Month(cycle.FromDate) <= " + pstrMonth + ")  ";
				strSql += "  AND (Year(cycle.ToDate) >= " + pstrYear + " AND Month(cycle.ToDate) >= " + pstrMonth + ")";
				strSql += " ),0) AS DSAmount, ";
				strSql += " ISNULL((SELECT SUM(ISNULL(AdjustAmount,0) + ISNULL(OH_AdjustAmount,0))";
				strSql += " FROM CST_DSAndRecycleAllocation charge JOIN CST_ActCostAllocationMaster cycle";
				strSql += " ON charge.ActCostAllocationMasterID = cycle.ActCostAllocationMasterID";
				strSql += " WHERE charge.ProductID = ITM_Product.ProductID";
				strSql += " AND (Year(cycle.FromDate) <= " + pstrYear + " AND Month(cycle.FromDate) <= " + pstrMonth + ")  ";
				strSql += "  AND (Year(cycle.ToDate) >= " + pstrYear + " AND Month(cycle.ToDate) >= " + pstrMonth + ")";
				strSql += " ),0) AS AdjustAmount, ";
				strSql += " ISNULL((SELECT SUM(ISNULL(RecycleAmount,0) + ISNULL(OH_RecycleAmount,0))";
				strSql += " FROM CST_DSAndRecycleAllocation charge JOIN CST_ActCostAllocationMaster cycle";
				strSql += " ON charge.ActCostAllocationMasterID = cycle.ActCostAllocationMasterID";
				strSql += " WHERE charge.ProductID = ITM_Product.ProductID";
				strSql += " AND (Year(cycle.FromDate) <= " + pstrYear + " AND Month(cycle.FromDate) <= " + pstrMonth + ")  ";
				strSql += "  AND (Year(cycle.ToDate) >= " + pstrYear + " AND Month(cycle.ToDate) >= " + pstrMonth + ")";
				strSql += " ),0) AS RecycleAmount,";*/

				strSql += " MST_Party.Code as PartyCode, ";
				strSql += " MST_Party.Name as PartyName ";
				
				strSql += " FROM    SO_ReturnedGoodsDetail ";
				strSql += " INNER JOIN SO_ReturnedGoodsMaster ON SO_ReturnedGoodsMaster.ReturnedGoodsMasterID = SO_ReturnedGoodsDetail.ReturnedGoodsMasterID  ";				
				strSql += " INNER JOIN MST_Party ON SO_ReturnedGoodsMaster.PartyID = MST_Party.PartyID ";
				strSql += " INNER JOIN ITM_Product ON SO_ReturnedGoodsDetail.ProductID = ITM_Product.ProductID ";
				strSql += " LEFT JOIN SO_CustomerItemRefMaster refMaster ON refMaster.PartyID = MST_Party.PartyID ";
				strSql += " LEFT JOIN SO_CustomerItemRefDetail refDetail ON refMaster.CustomerItemRefMasterID = refDetail.CustomerItemRefMasterID ";
				strSql += " AND refDetail.ProductID = ITM_Product.ProductID";

				//Add WHERE clause
				strSql += strWhereClause;				
				
				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, cn);

				odad.Fill(dtbBOMData);
				
				return dtbBOMData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (cn != null)
					if (cn.State != ConnectionState.Closed)
						cn.Close();
			}			
		}
		
		/// <summary>
		/// Get Customer Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCustomerInfoByID(string pstrIDList)
		{			
			DataSet dstPCS = new DataSet();

			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
					+ " FROM MST_Party"
					+ " WHERE MST_Party.PartyID IN (" + pstrIDList + ")";
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				if(odrPCS != null)
				{
					while(odrPCS.Read())
					{
						strResult += odrPCS[CODE_FIELD].ToString().Trim() + SEMI_COLON;
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
		
		#endregion Processing Data		
		
		#region Public Method

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}
		
		public DataTable ExecuteReport(string pstrMonth, string pstrYear, string pstrCustomerIDList)
		{	
			try
			{
				const string DATE_FORMAT = "MMM-yyyy";
				const string REPORT_NAME = "MonthlyReturnGoodsReceipt";				
				const string REPORT_LAYOUT = "MonthlyReturnGoodsReceipt.xml";				

				const string RPT_PAGE_HEADER = "PageHeader";				
				
				//Report field names
				const string RPT_TITLE_FIELD = "fldTitle";

				const string RPT_REPORT_MONTH = "Month";				
				const string RPT_CUSTOMER = "Customer";

				DataTable dtbTranHis = GetMonthlyReturnGoodsReceiptData(pstrMonth, pstrYear, pstrCustomerIDList);

				//Create builder object
				ReportBuilder reportBuilder = new ReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
			
				//Set Datasource
				reportBuilder.SourceDataTable = dtbTranHis;				
			
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = mstrReportDefinitionFolder;
				reportBuilder.ReportLayoutFile = REPORT_LAYOUT;
			
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			

				// And show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
							
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();				
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				
				//From Date
				if(pstrMonth != null && pstrMonth != string.Empty
				&& pstrYear != null && pstrYear != string.Empty)
				{
					DateTime dtmTemp = new DateTime(int.Parse(pstrYear), int.Parse(pstrMonth), 1);
					arrParamAndValue.Add(RPT_REPORT_MONTH, dtmTemp.ToString(DATE_FORMAT));
				}				

				//Customer Information
				if(pstrCustomerIDList != null && pstrCustomerIDList != string.Empty)
				{				
					arrParamAndValue.Add(RPT_CUSTOMER, GetCustomerInfoByID(pstrCustomerIDList));					
				}
				
				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
				reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);			
				
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text;
				}
				catch{}				

				reportBuilder.RefreshReport();
				printPreview.Show();
			
				return dtbTranHis;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		
		#endregion Public Method
	}
}