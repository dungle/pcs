using System;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using C1.Win.C1Preview;

using PCSComUtils.Common;
using PCSUtils.Utils;

namespace ItemListReport
{
	[Serializable]
	public class ItemListReport : MarshalByRefObject, IDynamicReport
	{
		public ItemListReport()
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
		
		#region Private Method

		private DataTable GetItemListData(string pstrCCN, string pstrCategory, string pstrModel, string pstrSource, string pstrType, string pstrMakeItem)
		{
			OleDbConnection cn = new OleDbConnection(mConnectionString);
			DataTable dtbItemData = new DataTable();

			try
			{
				//Build WHERE clause
				string strWhereClause = " WHERE MST_CCN.Code ='" + pstrCCN.Replace("'", "''") + "'";
				
				//Category
				if(pstrCategory != null && pstrCategory != string.Empty)
				{
					strWhereClause += " AND ITM_Category.Code IN (" + pstrCategory + ")"; 
				}
				//Model 
				if(pstrModel != null && pstrModel != string.Empty)
				{
					strWhereClause += " AND ITM_Product.Revision IN (" + pstrModel + ")"; 
				}
				
				//Source 
				if(pstrSource != null && pstrSource != string.Empty)
				{
					strWhereClause += " AND ITM_Source.Code ='" + pstrSource.Replace("'", "''") + "'"; 
				}
				
				//Type
				if(pstrType != null && pstrType != string.Empty)
				{
					strWhereClause += " AND ITM_ProductType.Code ='" + pstrType.Replace("'", "''") + "'"; 
				}
				
				//Make Item
				if(pstrMakeItem != null && pstrMakeItem != string.Empty)
				{
					if(pstrMakeItem.ToUpper().Equals("TRUE")
						|| pstrMakeItem == "1")
					{
						strWhereClause += " AND ITM_Product.MakeItem =1";
					}
					else
					{
						strWhereClause += " AND (ITM_Product.MakeItem = 0 OR ITM_Product.MakeItem IS NULL )";
					}
				}
				
				//Build SQL string
				string strSql = "SELECT  ITM_Category.Code as CategoryCode,";
				strSql += " ITM_Product.Code as PartNumber,";
				strSql += " ITM_Product.Description as PartName,";
				strSql += " ITM_Product.Revision as PartModel,";
				strSql += " Case ITM_Product.SafetyStock";
				strSql += " When 0 then NULL";
				strSql += " Else ITM_Product.SafetyStock";
				strSql += " End as SafetyStock,";
				strSql += " Case ITM_Product.ScrapPercent";
				strSql += " When 0 then NULL";
				strSql += " Else ITM_Product.ScrapPercent";
				strSql += " End as ScrapPercent,";
				strSql += " Case ITM_Product.MakeItem";
				strSql += " When 1 then 'x'";
				strSql += " Else ''";
				strSql += " End as MakeItem,";
				strSql += " MST_UnitOfMeasure.Code AS UMCode,";
				strSql += " ITM_Source.Code AS SourceCode,";
				strSql += " ITM_ProductType.Code AS ProductTypeCode,";
				strSql += " MST_Party.Code AS PartyCode, ";
				strSql += " Case ";
				strSql += " When Len(MST_Party.Name) <= 45 then MST_Party.Name";
				strSql += " Else LEFT(MST_Party.Name, 42) + '...'";
				strSql += " End as PartyName,";
				strSql += " MST_MasterLocation.Code AS MasLocCode,";
				strSql += " MST_Location.Code AS LocationCode,";
				strSql += " MST_CCN.Code as CCNCode";

				strSql += " FROM ITM_Product";
				strSql += " INNER JOIN MST_CCN ON MST_CCN.CCNID = ITM_Product.CCNID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";
				strSql += " LEFT JOIN MST_MasterLocation ON ITM_Product.MasterLocationID = MST_MasterLocation.MasterLocationID";
				strSql += " LEFT JOIN MST_Location ON ITM_Product.LocationID = MST_Location.LocationID";
				strSql += " LEFT JOIN ITM_ProductType ON ITM_ProductType.ProductTypeID = ITM_Product.ProductTypeID";
				strSql += " LEFT JOIN MST_Party ON ITM_Product.PrimaryVendorID = MST_Party.PartyID";
				strSql += " LEFT JOIN ITM_Source ON ITM_Product.SourceID = ITM_Source.SourceID";
				strSql += " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID";
				
				//Add WHERE clause
				strSql += strWhereClause;

				//Add ORDER clause
				strSql += " ORDER BY CategoryCode, PartName ASC";
				
				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, cn);

				odad.Fill(dtbItemData);				
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

			return dtbItemData;
		}

		#endregion Private Method

		#region Public Method

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		public DataTable ExecuteReport(string pstrCCN, string pstrCategory, string pstrModel, string pstrSource, string pstrType, string pstrMakeItem)
		{	
			try
			{
				//const char UNCHECK_SQUARE_CHAR = (char)111;
				//const char CHECK_SQUARE_CHAR = (char)120;
				//const char SPACE_CHAR = (char)32;

				//Report name
				const string REPORT_NAME = "ItemListReport";
				const string REPORT_LAYOUT = "ItemListReport.xml";
								
				const string RPT_HEADER = "Header";
				//Report field names
				const string RPT_TITLE_FIELD = "fldTitle";
				//Report field names
				const string RPT_CCN	   = "CCN";
				const string RPT_CATEGORY  = "Category";
				const string RPT_MODEL     = "Model";
				const string RPT_SOURCE    = "Source";
				const string RPT_TYPE      = "Type";
				const string RPT_MAKE_ITEM = "Make";

				DataTable dtbItemList = GetItemListData(pstrCCN, pstrCategory, pstrModel, pstrSource, pstrType, pstrMakeItem);

				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
			
				//Set Datasource
				reportBuilder.SourceDataTable = dtbItemList;		
			
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = mstrReportDefinitionFolder;
				reportBuilder.ReportLayoutFile = REPORT_LAYOUT;
			
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();						

				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				arrParamAndValue.Add(RPT_CCN, pstrCCN);
				if(pstrCategory != string.Empty)
				{
					arrParamAndValue.Add(RPT_CATEGORY, pstrCategory);
				}				
				
				if(pstrModel != string.Empty)
				{
					arrParamAndValue.Add(RPT_MODEL, pstrModel);
				}

				if(pstrSource != string.Empty)
				{
					arrParamAndValue.Add(RPT_SOURCE, pstrSource);
				}

				if(pstrType != string.Empty)
				{
					arrParamAndValue.Add(RPT_TYPE, pstrType);
				}

				if(pstrMakeItem.ToUpper().Equals("TRUE") || pstrMakeItem.Equals("1"))
				{
					arrParamAndValue.Add(RPT_MAKE_ITEM, "x");
				}

				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				reportBuilder.GetSectionByName(RPT_HEADER).CanGrow = true;
				reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);			
				
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text;
				}
				catch{}
				
				//Show report
				reportBuilder.RefreshReport();
				printPreview.Show();
			
				return dtbItemList;
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}
		
		#endregion Public Method	
		
	}
}