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

namespace TransactionHistoryReport
{
	[Serializable]
	public class TransactionHistoryReport : MarshalByRefObject, IDynamicReport
	{
		public TransactionHistoryReport()
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
		private const string ALL_CONDITION = "All";
		private const string GREATER_THAN_OR_EQUAL_CONDITION = "Greater than or equal 0";
		private const string LESS_THEN_CONDITION = "Less than 0";

		private const string CODE_FIELD = "Code";
		private const string NAME_FIELD = "Name";		

		private DataTable GetTransactionHistoryData(string pstrCCNID, string pstrFromDate, string pstrToDate, 
			string pstrTransTypeID, string pstrGreaterThan0,  string pstrMasLocID, string pstrLocationID, string pstrProductID)
		{
			const string SQL_DATE_FORMAT = "yyyy-MM-dd";

			OleDbConnection cn = null;
			
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbBOMData = new DataTable();
			
				//Build WHERE clause
				string strWhereClause = " WHERE MST_CCN.CCNID =" + pstrCCNID;

				strWhereClause += " AND (MST_TransactionHistory.PostDate BETWEEN '" + pstrFromDate + "' AND '" + pstrToDate + "')";
                
				//Transaction Type
				if(pstrTransTypeID != null && pstrTransTypeID != string.Empty)
				{
					strWhereClause += " AND MST_TransactionHistory.TranTypeID =" + pstrTransTypeID; 
				}

				//Master Location
				if(pstrMasLocID != null && pstrMasLocID != string.Empty)
				{
					strWhereClause += " AND MST_MasterLocation.MasterLocationID =" + pstrMasLocID; 
				}

				//Location
				if(pstrLocationID != null && pstrLocationID != string.Empty)
				{
					strWhereClause += " AND MST_Location.LocationID =" + pstrLocationID; 
				}
				
				//Product ID
				if(pstrProductID != null && pstrProductID != string.Empty)
				{
					strWhereClause += " AND ITM_Product.ProductID =" + pstrProductID; 
				}			

				if(pstrGreaterThan0.ToUpper().Equals(GREATER_THAN_OR_EQUAL_CONDITION.ToUpper()))
				{
					strWhereClause += " AND MST_TransactionHistory.Quantity >= 0"; 
				}

				if(pstrGreaterThan0.ToUpper().Equals(LESS_THEN_CONDITION.ToUpper()))
				{
					strWhereClause += " AND MST_TransactionHistory.Quantity < 0"; 
				}
				
				//Build SQL string
				string strSql = " SELECT  MST_TransactionHistory.PostDate,";
				strSql += " MST_TransactionHistory.TransDate,";
				strSql += " ITM_Category.Code as CategoryCode,";
				strSql += " ITM_Product.Code as PartNumber,";
				strSql += " ITM_Product.Description PartName,"; 
				strSql += " ITM_Product.Revision PartModel,";
				strSql += " MST_UnitOfMeasure.Code AS UMCode,";
				strSql += " MST_TransactionHistory.Quantity,";
				strSql += " MST_TranType.Description as TranTypeName,";
				strSql += " MST_MasterLocation.Code AS MasLocCode,";
				strSql += " MST_Location.Code AS LocationCode,";
				strSql += " MST_Bin.Code as BinCode,";
				strSql += " MST_Currency.Code AS CurrencyCode,";
				strSql += " MST_Party.Code AS PartyCode, ";
				strSql += " Case ";
				strSql += " When LEN(MST_Party.Name) <= 45 then MST_Party.Name";
				strSql += " Else LEFT(MST_Party.Name, 42) + '...'";
				strSql += " End as PartyName,";
				strSql += " MST_TransactionHistory.Comment,";
				strSql += " MST_CCN.Code as CCNCode,";
				strSql += " MST_TransactionHistory.UserName";
			 
				strSql += " FROM    MST_TransactionHistory";
				strSql += " INNER JOIN MST_CCN ON MST_CCN.CCNID = MST_TransactionHistory.CCNID";
				strSql += " LEFT JOIN MST_UnitOfMeasure ON MST_TransactionHistory.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID ";
				strSql += " LEFT JOIN MST_TranType ON MST_TransactionHistory.TranTypeID = MST_TranType.TranTypeID ";
				strSql += " LEFT JOIN MST_Location ON MST_TransactionHistory.LocationID = MST_Location.LocationID ";
				strSql += " LEFT JOIN MST_Bin ON MST_TransactionHistory.BinID = MST_Bin.BinID ";
				strSql += " LEFT JOIN ITM_Product ON MST_TransactionHistory.ProductID = ITM_Product.ProductID ";
				strSql += " LEFT JOIN MST_Party ON MST_TransactionHistory.PartyID = MST_Party.PartyID ";
				strSql += " LEFT JOIN MST_Currency ON MST_TransactionHistory.CurrencyID = MST_Currency.CurrencyID ";
				strSql += " LEFT JOIN MST_MasterLocation ON MST_TransactionHistory.MasterLocationID = MST_MasterLocation.MasterLocationID";
				strSql += " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID";
				
				//Add WHERE clause
				strSql += strWhereClause;

				strSql += " ORDER BY MST_TransactionHistory.PostDate,";
				strSql += " ITM_Category.Code,";
				strSql += " ITM_Product.Code,";
 				strSql += " MST_MasterLocation.Code,";
				strSql += " MST_Location.Code,";
				strSql += " MST_Bin.Code";
				
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
		/// Get Transaction Type Data for subreport
		/// </summary>
		/// <returns></returns>
		private String GetTranTypeName(string pstrTranTypeID)
		{
			const string DESCRIPTION_FLD = "Description";

			OleDbConnection cn = null;
			
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbTranTypeData = new DataTable();

				string strSql = " SELECT [Description]";
				strSql += " FROM MST_TranType";
				strSql += " WHERE TranTypeID = " + pstrTranTypeID;				
				
				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, cn);

				odad.Fill(dtbTranTypeData);
				if(dtbTranTypeData != null)
				{
					if(dtbTranTypeData.Rows.Count > 0)
					{
						return dtbTranTypeData.Rows[0][DESCRIPTION_FLD].ToString();
					}
				}

				return string.Empty;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (cn != null)
				{
					if (cn.State != ConnectionState.Closed)
						cn.Close();

					cn = null;
				}
			}
			
		}

		
		/// <summary>
		/// Get Master Location Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetMasterLocationInfoByID(string pstrID)
		{			
			DataSet dstPCS = new DataSet();

			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
					+ " FROM MST_MasterLocation"
					+ " WHERE MST_MasterLocation.MasterLocationID = " + pstrID;

			
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[CODE_FIELD].ToString().Trim() + " (" + odrPCS[NAME_FIELD].ToString().Trim() + ")";
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
		private string GetLocationInfoByID(string pstrID)
		{			
			DataSet dstPCS = new DataSet();

			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
					+ " FROM MST_Location"
					+ " WHERE MST_Location.LocationID = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[CODE_FIELD].ToString().Trim() + " (" + odrPCS[NAME_FIELD].ToString().Trim() + ")";
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
			DataSet dstPCS = new DataSet();

			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
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
						strResult = odrPCS[CODE_FIELD].ToString().Trim() + " (" + odrPCS[NAME_FIELD].ToString().Trim() + ")";
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
		/// Get Product Information
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private Hashtable GetProductInfoByID(string pstrID)
		{					
			Hashtable htbResult = new Hashtable();
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;

			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + PRODUCT_CODE + ", " + PRODUCT_NAME + ", " + PRODUCT_MODEL
					+ " FROM ITM_Product"
					+ " WHERE ITM_Product.ProductID = " + pstrID;
			
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						htbResult.Add(PRODUCT_CODE, odrPCS[PRODUCT_CODE]);
						htbResult.Add(PRODUCT_NAME, odrPCS[PRODUCT_NAME]);
						htbResult.Add(PRODUCT_MODEL, odrPCS[PRODUCT_MODEL]);
					}
				}
				return htbResult;
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
		
		public DataTable ExecuteReport(string pstrCCNID, string pstrFromDate, string pstrToDate, 
			string pstrTransTypeID, string pstrGreaterThan0, string pstrMasLocID, string pstrLocationID, string pstrProductID)
		{	
			try
			{
				const string DATE_FORMAT = "dd-MM-yyyy HH:mm";
				const string REPORT_NAME = "TransactionHistoryReport";				
				const string REPORT_LAYOUT = "TransactionHistoryReport.xml";

				const string RPT_PAGE_HEADER = "PageHeader";
				//Report field names
				const string RPT_TITLE_FIELD = "fldTitle";

				const string RPT_CCN	     = "CCN";
				const string RPT_FROM_DATE   = "From Date";
				const string RPT_TO_DATE     = "To Date";			
				const string RPT_LOCATION    = "Location";
				const string RPT_TRAN_TYPE   = "Transation Type";
				const string RPT_PART_NUMBER = "Part Number";
				const string RPT_MODEL       = "Model";
				const string RPT_PART_NAME   = "Part Name";
				const string RPT_MASTER_LOCATION = "Master Location";

				DataTable dtbTranHis = GetTransactionHistoryData(pstrCCNID, pstrFromDate, pstrToDate, pstrTransTypeID, pstrGreaterThan0, pstrMasLocID, pstrLocationID, pstrProductID);
							
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
				string strCode = GetCCNInfoByID(pstrCCNID);
				arrParamAndValue.Add(RPT_CCN, strCode);
				arrParamAndValue.Add(RPT_FROM_DATE, DateTime.Parse(pstrFromDate).ToString(DATE_FORMAT));
				arrParamAndValue.Add(RPT_TO_DATE, DateTime.Parse(pstrToDate).ToString(DATE_FORMAT));
				
				//Master Location
				if(pstrMasLocID != null && pstrMasLocID != string.Empty)
				{
					strCode = GetMasterLocationInfoByID(pstrMasLocID);
					arrParamAndValue.Add(RPT_MASTER_LOCATION, strCode);
				}				
			
				//Location
				if(pstrLocationID != null && pstrLocationID != string.Empty)
				{
					strCode = GetLocationInfoByID(pstrLocationID);
					arrParamAndValue.Add(RPT_LOCATION, strCode);
				}
				
				//Transaction Type
				if(pstrTransTypeID != null && pstrTransTypeID != string.Empty)
				{
					strCode = GetTranTypeName(pstrTransTypeID);
					arrParamAndValue.Add(RPT_TRAN_TYPE, strCode);
				}
				
				//Product Information
				if(pstrProductID != null && pstrProductID != string.Empty)
				{
					Hashtable htbProductInfo = GetProductInfoByID(pstrProductID);
				
					arrParamAndValue.Add(RPT_PART_NUMBER, htbProductInfo[PRODUCT_CODE].ToString());
					arrParamAndValue.Add(RPT_PART_NAME, htbProductInfo[PRODUCT_NAME].ToString());
					arrParamAndValue.Add(RPT_MODEL, htbProductInfo[PRODUCT_MODEL].ToString());
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