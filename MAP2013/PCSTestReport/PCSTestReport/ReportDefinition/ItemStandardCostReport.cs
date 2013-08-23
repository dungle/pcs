using System;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using C1.Win.C1Preview;

using PCSComUtils.Common;
using PCSUtils.Utils;

namespace ItemStandardCostReport
{
	/// <summary>
	/// Summary description for ItemStandardCostReport.
	/// </summary>
	[Serializable]
	public class ItemStandardCostReport : MarshalByRefObject, IDynamicReport
	{
		public ItemStandardCostReport()
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

		private const string COST_ELEMENT_TABLE = "CostElementTable";
		private const string ITEM_COST_TABLE = "ItemCostTable";
		private const string INDENT_CHAR = "-";
				
		private const string COST_ELEMENT_NAME_FLD = "CostElementName";
		private const string COST_ELEMENT_ID_FLD = "CostElementID";		
		private const string PARENT_ELEMENT_ID_FLD = "ParentID";
		
		private const string CODE_FLD = "Code";
		private const string REVISION_FLD = "Revision";
		private const string DESCRIPTION_FLD = "Description";
		private const string NAME_FLD = "Name";
		private const string COST_FLD = "Cost";

		private const string ROLLUP_DATE_FLD = "RollupDate";
		
		private const string PART_NUMBER_FLD = "PartNumber";
		private const string PART_NAME_FLD = "PartName";
		private const string PART_MODEL_FLD = "PartModel";		
		private const string PRODUCT_ID_FLD = "ProductID";
		private const string CATEGORY_ID_FLD = "CategoryID";		

		private string strListProductIDs = string.Empty;
		
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

				string strSql = "SELECT Code, Description, Revision FROM ITM_Product WHERE ProductID IN (" + pstrID + ")";;
			
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				string strPartNo = string.Empty, strPartName = string.Empty, strModel = string.Empty; 
				foreach (DataRow drowData in dtbData.Rows)
				{
					strPartNo += drowData["Code"].ToString() + ",";
					strPartName += drowData["Description"].ToString() + ",";
					strModel += drowData["Revision"].ToString() + ",";
				}
				if (strPartNo.Length > 0)
					strPartNo = strPartNo.Substring(0, strPartNo.Length - 1);
				if (strPartName.Length > 0)
					strPartName = strPartName.Substring(0, strPartName.Length - 1);
				if (strModel.Length > 0)
					strModel = strModel.Substring(0, strModel.Length - 1);
				odrPCS = ocmdPCS.ExecuteReader();
				htbResult.Add(CODE_FLD, strPartNo);
				htbResult.Add(DESCRIPTION_FLD, strPartName);
				htbResult.Add(REVISION_FLD, strModel);
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

		/// <summary>
		/// Get Location Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCategoryInfoByID(string pstrID)
		{			
			DataSet dstPCS = new DataSet();

			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FLD + ", " + NAME_FLD
					+ " FROM ITM_Category"
					+ " WHERE ITM_Category.CategoryID = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[CODE_FLD].ToString().Trim() + " (" + odrPCS[NAME_FLD].ToString().Trim() + ")";
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
		private string GetHomeCurrency(string pstrCCNID)
		{			
			const string FULL_COMPANY_NAME = "CompanyFullName";
			OleDbConnection oconPCS = null;

			try
			{
				string strResult = string.Empty;
				OleDbDataReader odrPCS = null;				
				
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT MST_Currency.Code";

				strSql += " FROM MST_CCN ccn";
				strSql += " INNER JOIN MST_Currency ON MST_Currency.CurrencyID = ccn.HomeCurrencyID";
				strSql += " WHERE ccn.CCNID = " + pstrCCNID;
			
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS["Code"].ToString().Trim();
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

				string strSql =	"SELECT " + CODE_FLD + ", " + DESCRIPTION_FLD
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
						strResult = odrPCS[CODE_FLD].ToString().Trim() + " (" + odrPCS[DESCRIPTION_FLD].ToString().Trim() + ")";
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
		/// Get all cost element from database
		/// </summary>
		/// <returns></returns>
		private DataTable GetAllCostElements()
		{

			OleDbConnection cn = null;
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbCostElementData = new DataTable(COST_ELEMENT_TABLE);

				string strSql = "SELECT [CostElementID],";
				strSql += " [Code],";
				strSql += " [Name],";
				strSql += " [OrderNo],";
				strSql += " [CostElementTypeID],";
				strSql += " [ParentID],";
				strSql += " [IsLeaf]";
				strSql += " FROM STD_CostElement"; 
				
				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, cn);

				odad.Fill(dtbCostElementData);
			
				return dtbCostElementData;
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
		/// Get Item standard cost data
		/// </summary>
		/// <returns></returns>
		private DataTable GetItemStandardCostData(string pstrCCNID, string pstrCategoryID, string pstrModel, string pstrProductID)
		{
			OleDbConnection cn = null;
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbItemCostData = new DataTable(ITEM_COST_TABLE);

				string strSql = "SELECT DISTINCT cate.Code as CategoryCode,";
				strSql += " product.Code as ProductCode,";
				strSql += " product.Revision as ProductModel,";
				strSql += " product.Code + product.Revision as ProductCodeModel,";
				strSql += " product.Description as ProductName,";
				strSql += " Case product.MakeItem";
				strSql += " when 1 then 'x'";
				strSql += " else ''";
				strSql += " End";
				strSql += " as MakeItem,";
				strSql += " um.Code as StockUM,";
				strSql += " case product.CostMethod";
				strSql += " when 0 then 'ACT'";
				strSql += " when 1 then 'STD'";
				strSql += " when 2 then 'AVG'";
				strSql += " end as CostMethod,";
				strSql += " cost.RollupDate,";
				strSql += " element.Name as CostElementName,";

				strSql += " (SELECT Cost FROM CST_STDItemCost";
				strSql += " WHERE ProductID = product_cost.ProductID";
				strSql += " AND CostElementID = product_cost.CostElementID";
				strSql += " ) as Cost,";			

				strSql += " MST_Currency.Code as HomeCurrency,";
				strSql += " product.CCNID,";
				strSql += " product.ProductID,";
				strSql += " cate.CategoryID,";
				strSql += " element.CostElementID,";
				strSql += " element.ParentID, element.OrderNo";

				strSql += " FROM ";
				strSql += " (SELECT * ";
				strSql += " FROM (SELECT ProductID FROM ITM_Product) product, (SELECT CostElementID FROM STD_CostElement) cost";
				strSql += " )product_cost";

				strSql += " INNER JOIN ITM_Product product ON product.ProductID = product_cost.ProductID";
				strSql += " INNER JOIN STD_CostElement element ON element.CostElementID = product_cost.CostElementID";
				strSql += " INNER JOIN CST_STDItemCost cost ON cost.ProductID = product.ProductID";
				
				strSql += " LEFT JOIN ITM_Category cate ON cate.CategoryID = product.CategoryID";
				strSql += " LEFT JOIN MST_UnitOfMeasure um ON um.UnitOfMeasureID = product.StockUMID";
				strSql += " LEFT JOIN MST_CCN ON MST_CCN.CCNID = product.CCNID";
				strSql += " LEFT JOIN MST_Currency ON MST_Currency.CurrencyID = MST_CCN.HomeCurrencyID";

				strSql += " WHERE product.CCNID = " + pstrCCNID;

				if(pstrCategoryID != string.Empty)
				{
					strSql += " AND product.CategoryID IN (" + pstrCategoryID + ")";
				}

				if(pstrModel != string.Empty)
				{
					strSql += " AND product.Revision IN (" + pstrModel + ")";
				}
				
				if(pstrProductID != string.Empty)
				{
					strSql += " AND product.ProductID IN (" + pstrProductID + ")";
				}

				strSql += " ORDER BY cate.Code, product.Code + product.Revision, product.Revision, product.Description, element.OrderNo";				
				
				Console.WriteLine(strSql);

				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, cn);

				odad.Fill(dtbItemCostData);				
			
				return dtbItemCostData;
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

		
		private void BuildCostElementTree(DataTable pdtbData, string pstrParentID, string pstrIndent)
		{
			try
			{
				string strFilter = PARENT_ELEMENT_ID_FLD;
				if(pstrParentID != string.Empty)
				{
					strFilter += " = " + pstrParentID;
				}
				else
				{
					strFilter += " IS NULL";
				}

				DataRow[] arrChildren = pdtbData.Select(strFilter);

				foreach(DataRow drow in arrChildren)
				{
					drow[NAME_FLD] = pstrIndent + drow[NAME_FLD].ToString();
					BuildCostElementTree(pdtbData, drow[COST_ELEMENT_ID_FLD].ToString(), pstrIndent + INDENT_CHAR);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private DataTable BuildItemStandardCostData(string pstrCCNID, string pstrCategoryID, string pstrModel, string pstrProductID)
		{
			try
			{
				DataTable dtbItemSTDCost;
				DataTable dtbCostElement;

				dtbItemSTDCost = GetItemStandardCostData(pstrCCNID, pstrCategoryID, pstrModel, pstrProductID);				
				dtbCostElement = GetAllCostElements();

				string strSelectCondition = string.Empty;
				ArrayList arlProcessedItems = new ArrayList();
				
				if(dtbCostElement == null || dtbItemSTDCost == null)
				{
					return null;
				}

				//Build cost element tree
				BuildCostElementTree(dtbCostElement, string.Empty, string.Empty);
				
				//Set element name to a value like a tree
				foreach(DataRow drowCostElement in dtbCostElement.Rows)
				{
					strSelectCondition = COST_ELEMENT_ID_FLD + "=" + drowCostElement[COST_ELEMENT_ID_FLD].ToString();

					DataRow[] arrSameCostElementRows = dtbItemSTDCost.Select(strSelectCondition);

					foreach(DataRow drow in arrSameCostElementRows)
					{
						drow[COST_ELEMENT_NAME_FLD] = drowCostElement[NAME_FLD];
					}
				}

				DateTime dtmRollupDate;
				//Calculate cost for each element from leaf element
				foreach(DataRow drowProduct in dtbItemSTDCost.Rows)
				{					
					if(!arlProcessedItems.Contains(drowProduct[PRODUCT_ID_FLD]))
					{
						strSelectCondition = PRODUCT_ID_FLD + " = " + drowProduct[PRODUCT_ID_FLD].ToString();
						strSelectCondition += " AND " + PARENT_ELEMENT_ID_FLD + " IS NULL";

						DataRow[] arrSameProductOfParentElement = dtbItemSTDCost.Select(strSelectCondition);
						
						CalculateParentCostValue(dtbItemSTDCost, arrSameProductOfParentElement, drowProduct[PRODUCT_ID_FLD].ToString(), out dtmRollupDate);

						arlProcessedItems.Add(drowProduct[PRODUCT_ID_FLD]);
					}
				}

				//return result
				return dtbItemSTDCost;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		
		private decimal CalculateParentCostValue(DataTable pdtbSource, DataRow[] parrCalculatingRow, string pstrProductID, out DateTime pdtmRollupDate)
		{			
			decimal decTotal = decimal.Zero;
			DateTime dtmRollupDate = DateTime.MinValue;

			foreach(DataRow drow in parrCalculatingRow)
			{	
				if(!drow[ROLLUP_DATE_FLD].Equals(DBNull.Value) 
				&& !drow[ROLLUP_DATE_FLD].ToString().Equals(string.Empty))
				{
					dtmRollupDate = (DateTime)drow[ROLLUP_DATE_FLD];
				}

				string strSelectCondition = PRODUCT_ID_FLD + "=" + drow[PRODUCT_ID_FLD].ToString();
                strSelectCondition += " AND " + PARENT_ELEMENT_ID_FLD + "=" + drow[COST_ELEMENT_ID_FLD].ToString();

				DataRow[] arrCalculatingRow = pdtbSource.Select(strSelectCondition);

				if(arrCalculatingRow.Length != 0)
				{					
					drow[COST_FLD] = CalculateParentCostValue(pdtbSource, arrCalculatingRow, pstrProductID, out dtmRollupDate);

					if((drow[ROLLUP_DATE_FLD].Equals(DBNull.Value) || drow[ROLLUP_DATE_FLD].ToString().Equals(string.Empty))
					&& (dtmRollupDate != DateTime.MinValue))
					{
						drow[ROLLUP_DATE_FLD] = dtmRollupDate;
					}
				}

				if(!drow[COST_FLD].Equals(DBNull.Value)
				&& !drow[COST_FLD].ToString().Equals(string.Empty))
				{
					decTotal += decimal.Parse(drow[COST_FLD].ToString());
				}
			}

			pdtmRollupDate = dtmRollupDate;

			return decTotal;
		}

		#endregion Processing Data		
		
		#region Public Method

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		public DataTable ExecuteReport(string pstrCCNID, string pstrCategoryID, string pstrModel, string pstrProductID)
		{			
			try
			{
				const string REPORT_NAME = "ItemStandardCostReport";				
				const string REPORT_LAYOUT = "ItemStandardCostReport.xml";				
				
				const string RPT_PAGE_HEADER = "PageHeader";

				const string RPT_TITLE_FLD = "fldTitle";
				const string RPT_CURRENCY_FLD = "fldCurrency";
				const string RPT_CCN_FLD = "CCN";
				const string RPT_CATEGORY_FLD = "Category";
				const string RPT_MODEL_FLD = "Model";
				const string RPT_PART_NO_FLD = "Part No.";
				const string RPT_PART_NAME_FLD = "Part Name";

				DataTable dtbData = BuildItemStandardCostData(pstrCCNID, pstrCategoryID, pstrModel, pstrProductID);
				
				//Create builder object
				ReportBuilder reportBuilder = new ReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
			
				//Set Datasource
				reportBuilder.SourceDataTable = dtbData;				
			
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = mstrReportDefinitionFolder;
				reportBuilder.ReportLayoutFile = REPORT_LAYOUT;
			
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			
				
				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
				PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
				
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();
				
				reportBuilder.DrawPredefinedField(RPT_CURRENCY_FLD, GetHomeCurrency(pstrCCNID));

				//Draw parameters
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				string strCode = GetCCNInfoByID(pstrCCNID);
				arrParamAndValue.Add(RPT_CCN_FLD, strCode);				
				
				//Category 
				if(pstrCategoryID != null && pstrCategoryID != string.Empty)
				{
					strCode = objBO.GetCategoryCodeFromID(pstrCategoryID);
					arrParamAndValue.Add(RPT_CATEGORY_FLD, strCode);
				}				
			
				//Model
				if(pstrModel != null && pstrModel != string.Empty)
				{					
					arrParamAndValue.Add(RPT_MODEL_FLD, pstrModel);
				}		
				
				//Product Information
				if(pstrProductID != null && pstrProductID != string.Empty)
				{
					Hashtable htbProductInfo = GetProductInfoByID(pstrProductID);
				
					arrParamAndValue.Add(RPT_PART_NO_FLD, htbProductInfo[CODE_FLD].ToString());
					arrParamAndValue.Add(RPT_PART_NAME_FLD, htbProductInfo[DESCRIPTION_FLD].ToString());					
					if(pstrModel == string.Empty || pstrModel == null)
					{
						arrParamAndValue.Add(RPT_MODEL_FLD, htbProductInfo[REVISION_FLD].ToString());
					}
					else
					{
						arrParamAndValue[RPT_MODEL_FLD] = htbProductInfo[REVISION_FLD].ToString(); 
					}
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
				{

				}
			
				reportBuilder.RefreshReport();
				printPreview.Show();
			
				return dtbData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		
		#endregion Public Method	
	}
}
