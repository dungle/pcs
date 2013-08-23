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

namespace StockTakingReport
{
	[Serializable]
	public class StockTakingReport : MarshalByRefObject, IDynamicReport
	{
		public StockTakingReport()
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
				
		
		DataTable GetStockTakingData(string pstrCCNID,
			string pstrStockTakingPeriodID,
			string pstrDepartmentIDs,
			string pstrProductionLineIDs,
			string pstrLocationIDs,
			string pstrBinIDs
			)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			
            StringBuilder strSqlBuilder = new StringBuilder();

			strSqlBuilder.Append(" SELECT IV_StockTaking.StockTakingID, IV_StockTaking.Quantity,IV_StockTaking.BookQuantity, IV_StockTaking.SlipCode, IV_StockTaking.Note,  ");
				strSqlBuilder.Append(" IV_StockTaking.StockTakingPeriodID, IV_StockTaking.DepartmentID, IV_StockTaking.ProductionLineID,  ");
				strSqlBuilder.Append(" IV_StockTaking.LocationID, IV_StockTaking.BinID, IV_StockTaking.ProductID, IV_StockTaking.StockUMID,  ");
				strSqlBuilder.Append(" IV_StockTaking.CountingMethodID, ");
				strSqlBuilder.Append(" MST_Department.Code Department_Code, ");
				strSqlBuilder.Append(" PRO_ProductionLine.Code ProductionLine_Code, ");
				strSqlBuilder.Append(" MST_Location.Code Location_Code, ");
				strSqlBuilder.Append(" MST_Bin.Code Bin_Code, ");
				strSqlBuilder.Append(" ITM_Category.Code Category_Code, ");
				strSqlBuilder.Append(" ITM_Product.Code PartNumber, ITM_Product.Description PartName, ITM_Product.Revision PartModel, ITM_Product.SourceID,  ");
				strSqlBuilder.Append(" MST_UnitOfMeasure.Code UMCode, ");
				strSqlBuilder.Append(" ITM_Source.Code Source_Code, ");
				strSqlBuilder.Append(" ActualCost.ActualCost UnitPrice, ");
				strSqlBuilder.Append(" ActualCost.ActualCost*IV_StockTaking.Quantity ActualAmount, ");
				strSqlBuilder.Append(" ActualCost.ActualCost*IV_StockTaking.BookQuantity BookAmount, ");
				strSqlBuilder.Append(" IV_StockTaking.Quantity -IV_StockTaking.BookQuantity DiffQuantity, ");
				strSqlBuilder.Append(" (ActualCost.ActualCost*IV_StockTaking.Quantity  - ");
				strSqlBuilder.Append(" ActualCost.ActualCost *IV_StockTaking.BookQuantity ) DiffAmount");/*ActualAmount-BookAmount*/
			strSqlBuilder.Append(" FROM  IV_StockTaking ");
			strSqlBuilder.Append(" INNER JOIN IV_StockTakingPeriod ON IV_StockTaking.StockTakingPeriodID = IV_StockTakingPeriod.StockTakingPeriodID ");
			strSqlBuilder.Append(" INNER JOIN ITM_Product ON IV_StockTaking.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID ");
			strSqlBuilder.Append(" LEFT JOIN ITM_Source  ON ITM_Product.SourceID = ITM_Source.SourceID ");
			strSqlBuilder.Append(" LEFT JOIN MST_UnitOfMeasure  ON IV_StockTaking.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID ");
			strSqlBuilder.Append(" LEFT JOIN MST_Department  ON IV_StockTaking.DepartmentID = MST_Department.DepartmentID ");
			strSqlBuilder.Append(" LEFT JOIN PRO_ProductionLine  ON IV_StockTaking.ProductionLineID = PRO_ProductionLine.ProductionLineID ");
			strSqlBuilder.Append(" LEFT JOIN MST_Location  ON IV_StockTaking.LocationID = MST_Location.LocationID ");
			strSqlBuilder.Append(" LEFT JOIN MST_Bin  ON IV_StockTaking.BinID = MST_Bin.BinID ");
			strSqlBuilder.Append(" LEFT JOIN ( ");
				strSqlBuilder.Append(" SELECT CST_ActualCostHistory.ProductID,SUM(CST_ActualCostHistory.ActualCost) ActualCost  ");
				strSqlBuilder.Append(" FROM CST_ActualCostHistory ");
				strSqlBuilder.Append(" INNER JOIN cst_ActCostAllocationMaster ON CST_ActualCostHistory.ActCostAllocationMasterID=cst_ActCostAllocationMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" AND ((SELECT StockTakingDate FROM IV_StockTakingPeriod WHERE StockTakingPeriodID="+pstrStockTakingPeriodID+") >= cst_ActCostAllocationMaster.FromDate)  ");
				strSqlBuilder.Append(" AND ((SELECT StockTakingDate FROM IV_StockTakingPeriod WHERE StockTakingPeriodID="+pstrStockTakingPeriodID+") < cst_ActCostAllocationMaster.ToDate) ");
				strSqlBuilder.Append(" GROUP BY CST_ActualCostHistory.ProductID ");
			strSqlBuilder.Append(" ) ActualCost ON IV_StockTaking.ProductID = ActualCost.ProductID ");

			strSqlBuilder.Append(" WHERE IV_StockTakingPeriod.CCNID = "+pstrCCNID+" AND IV_StockTaking.StockTakingPeriodID = "+pstrStockTakingPeriodID+" ");
            
			if(pstrDepartmentIDs != string.Empty && pstrDepartmentIDs != null)
			{
				strSqlBuilder.Append(" AND IV_StockTaking.DepartmentID IN ("+pstrDepartmentIDs+") ");
			}
			if(pstrProductionLineIDs != string.Empty && pstrProductionLineIDs != null)
			{
				strSqlBuilder.Append(" AND IV_StockTaking.ProductionLineID IN ("+pstrProductionLineIDs+") ");
			}
			if(pstrLocationIDs != string.Empty && pstrLocationIDs != null)
			{
				strSqlBuilder.Append(" AND IV_StockTaking.LocationID IN ("+pstrLocationIDs+") ");
			}
			if(pstrBinIDs != string.Empty && pstrBinIDs != null)
			{
				strSqlBuilder.Append(" AND IV_StockTaking.BinID IN (" + pstrBinIDs + ") ");
			}

			strSqlBuilder.Append(" ORDER BY MST_Department.Code, ");
			strSqlBuilder.Append(" PRO_ProductionLine.Code, ");
			strSqlBuilder.Append(" MST_Location.Code, ");
			strSqlBuilder.Append(" MST_Bin.Code, ");
			strSqlBuilder.Append(" ITM_Category.Code, ");
			strSqlBuilder.Append(" ITM_Product.Code, ");
			strSqlBuilder.Append(" ITM_Product.Description ");

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
		

		private string GetFieldInfoFromTable(string pstrTableName,string pstrFieldName,string pstrFieldKey, string pstrIDList)
		{			
			const string SEMI_COLON = "; ";
			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + pstrFieldName
						+ " FROM " + pstrTableName
						+ " WHERE " + pstrFieldKey + " IN (" +  pstrIDList + ")";
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					while(odrPCS.Read())
					{
						strResult += odrPCS[pstrFieldName].ToString().Trim() + SEMI_COLON;
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
		/// <author> Tuan TQ, 11 Apr, 2006</author>
		public DataTable ExecuteReport(string pstrCCNID,
			string pstrStockTakingPeriodID,
			string pstrDepartmentIDs,
			string pstrProductionLineIDs,
			string pstrLocationIDs,
			string pstrBinIDs
			)
		{
//			try
//			{
				string strPOTypeID = Convert.ToString((int)PCSComUtils.Common.POReceiptTypeEnum.ByInvoice);

				const string DATE_HOUR_FORMAT = "dd-MM-yyyy HH:mm";				

				const string REPORT_TEMPLATE = "StockTakingReport.xml";
				const string RPT_PAGE_HEADER = "PageHeader";

				const string REPORT_NAME = "StockTakingReport";
				const string RPT_TITLE_FLD = "fldTitle";
				const string RPT_COMPANY_FLD = "fldCompany";
				
				const string RPT_CCN_FLD = "CCN";		
				const string RPT_MASTER_LOCATION_FLD = "Master Location";
				const string RPT_LOCATION_FLD = "Master Location";
				const string RPT_FROM_DATE_FLD = "From Date";
				const string RPT_TO_DATE_FLD = "To Date";
				const string RPT_PART_NO_FLD = "Part No.";
				const string RPT_TRANSACTION_TYPE_FLD = "Trans. Type";
				
				DataTable dtbDataSource = null;

				dtbDataSource = GetStockTakingData(pstrCCNID,
										pstrStockTakingPeriodID,
										pstrDepartmentIDs,
										pstrProductionLineIDs,
										pstrLocationIDs,
										pstrBinIDs);

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
				arrParamAndValue.Add("StockTakingPeriod ", GetFieldInfoFromTable("IV_StockTakingPeriod","Description","StockTakingPeriodID", pstrStockTakingPeriodID));

			//
			if(pstrDepartmentIDs != null && pstrDepartmentIDs != string.Empty)
			{
				arrParamAndValue.Add("Department ", GetFieldInfoFromTable(MST_DepartmentTable.TABLE_NAME,"Code",MST_DepartmentTable.DEPARTMENTID_FLD, pstrDepartmentIDs));
			}
			//
			if(pstrProductionLineIDs != null && pstrProductionLineIDs != string.Empty)
			{
				arrParamAndValue.Add("Production Line ", GetFieldInfoFromTable(PRO_ProductionLineTable.TABLE_NAME,"Code",PRO_ProductionLineTable.PRODUCTIONLINEID_FLD,pstrProductionLineIDs));
			}
			//Location list
			if(pstrLocationIDs != null && pstrLocationIDs != string.Empty)
			{
				arrParamAndValue.Add("Location ", GetLocationInfo(pstrLocationIDs));
			}

			//Part no list
			if(pstrBinIDs != null && pstrBinIDs != string.Empty)
			{
				arrParamAndValue.Add("Bin " , GetFieldInfoFromTable(MST_BINTable.TABLE_NAME,"Code",MST_BINTable.BINID_FLD,pstrBinIDs));
			}
//
//				//Trans. type list
//				if(pstrTransTypeIDList != null && pstrTransTypeIDList != string.Empty)
//				{
//					arrParamAndValue.Add(RPT_TRANSACTION_TYPE_FLD, GetTransTypeInfo(pstrTransTypeIDList));
//				}
				
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
//			}
//			catch (Exception ex)
//			{
//				throw ex;
//			}			
		}
		
		#endregion Public Method			
	}
}
