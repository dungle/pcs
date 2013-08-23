using System;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using C1.Win.C1Preview;

using PCSComUtils.Common;
using PCSUtils.Utils;

namespace ProductRoutingReport
{
	[Serializable]
	public class ProductRoutingReport : MarshalByRefObject, IDynamicReport
	{
		public ProductRoutingReport()
		{
		}

		string strListProductIDs = string.Empty;

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

		private const string BOM_PRODUCT_TABLE = "BOM_Product";
		private const string ROUTING_PRODUCT_TABLE = "Routing_Product";
		
		
		private const string PARENT_PART_NUMBER_FLD = "ParentPartNumber";
		private const string PARENT_PART_NAME_FLD = "ParentPartName";
		private const string PARENT_PART_MODEL_FLD = "ParentPartModel";

		private const string PART_INDEX_FLD = "PartIndex";
		private const string PART_NUMBER_FLD = "PartNumber";
		private const string PART_NAME_FLD = "PartName";
		private const string PART_MODEL_FLD = "PartModel";
		
		private const string PARENT_PRODUCT_ID_FLD = "ParentProductID";
		private const string PRODUCT_ID_FLD = "ProductID";		

		private DataTable BuildReportDataTemplate()
		{
			try
			{
				DataTable dtbTemplate = new DataTable(BOM_PRODUCT_TABLE);
			
				dtbTemplate.Columns.Add(PRODUCT_ID_FLD, typeof(System.Int32));

				dtbTemplate.Columns.Add(PART_INDEX_FLD, typeof(System.String));			
				dtbTemplate.Columns.Add(PART_MODEL_FLD, typeof(System.String));
				dtbTemplate.Columns.Add(PART_NAME_FLD, typeof(System.String));
				dtbTemplate.Columns.Add(PART_NUMBER_FLD, typeof(System.String));

				dtbTemplate.Columns.Add(PARENT_PART_MODEL_FLD, typeof(System.String));
				dtbTemplate.Columns.Add(PARENT_PART_NAME_FLD, typeof(System.String));
				dtbTemplate.Columns.Add(PARENT_PART_NUMBER_FLD, typeof(System.String));
	
				return dtbTemplate;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Build BOM table structure
		/// </summary>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		private DataTable BuildBOMDataTable(int pintProductID)
		{
			try
			{
				DataTable dtbResult = BuildReportDataTemplate();			
				DataTable dtbBOMSource = GetBOMInfo();

				int iProductIndex = 0;
			
				DataRow[] arrBomInfo = GetBOMInfoOfProduct(dtbBOMSource, pintProductID);

				if(arrBomInfo != null)
				{
					//Add parent info
					if(arrBomInfo.Length != 0)
					{					
						//create new row
						DataRow drNewRow = dtbResult.NewRow();
			
						//Copy data
						drNewRow[PART_INDEX_FLD] = iProductIndex;
						drNewRow[PARENT_PART_MODEL_FLD] = arrBomInfo[0][PARENT_PART_MODEL_FLD];
						drNewRow[PARENT_PART_NAME_FLD] = arrBomInfo[0][PARENT_PART_NAME_FLD];
						drNewRow[PARENT_PART_NUMBER_FLD] = arrBomInfo[0][PARENT_PART_NUMBER_FLD];
						drNewRow[PART_MODEL_FLD] = arrBomInfo[0][PARENT_PART_MODEL_FLD];
						drNewRow[PART_NAME_FLD] = arrBomInfo[0][PARENT_PART_NAME_FLD];
						drNewRow[PART_NUMBER_FLD] = arrBomInfo[0][PARENT_PART_NUMBER_FLD];
						drNewRow[PRODUCT_ID_FLD] = pintProductID;
			
						//Add to collection
						dtbResult.Rows.Add(drNewRow);
					}

					foreach(DataRow drow in arrBomInfo)
					{
						iProductIndex++;
						AddRow2Table(dtbResult, dtbBOMSource, iProductIndex.ToString(), drow);					
					}
				}

				return dtbResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}
		
		private void AddRow2Table(DataTable pdtbResultData, DataTable pdtbBOMSource,  string pstrIndex, DataRow pdrow)
		{
			try
			{
				//create new row
				DataRow drNewRow = pdtbResultData.NewRow();
			
				//Copy data
				drNewRow[PART_INDEX_FLD] = pstrIndex;			
				drNewRow[PARENT_PART_MODEL_FLD] = pdrow[PARENT_PART_MODEL_FLD];
				drNewRow[PARENT_PART_NAME_FLD] = pdrow[PARENT_PART_NAME_FLD];
				drNewRow[PARENT_PART_NUMBER_FLD] = pdrow[PARENT_PART_NUMBER_FLD];
				drNewRow[PART_MODEL_FLD] = pdrow[PART_MODEL_FLD];
				drNewRow[PART_NAME_FLD] = pdrow[PART_NAME_FLD];
				drNewRow[PART_NUMBER_FLD] = pdrow[PART_NUMBER_FLD];
				drNewRow[PRODUCT_ID_FLD] = pdrow[PRODUCT_ID_FLD];

				//Add to collection
				pdtbResultData.Rows.Add(drNewRow);
			
				//Get product Id
				int iProductID =  int.Parse(pdrow[PRODUCT_ID_FLD].ToString());

				//Collect ProductIDs
				strListProductIDs += ", " + iProductID.ToString();
 
				//Get BOM information
				DataRow[] arrBomInfo = GetBOMInfoOfProduct(pdtbBOMSource, iProductID);			

				//Loop then call this function recursively
				if(arrBomInfo != null)
				{
					int iIndex = 1;
					foreach(DataRow drow in arrBomInfo)
					{
						//Keep Parent product infor
						drow[PARENT_PART_MODEL_FLD] = pdrow[PARENT_PART_MODEL_FLD];
						drow[PARENT_PART_NAME_FLD] = pdrow[PARENT_PART_NAME_FLD];
						drow[PARENT_PART_NUMBER_FLD] = pdrow[PARENT_PART_NUMBER_FLD];

						//increase index
						string strIndex = pstrIndex + "." + iIndex.ToString();
					
						//Call recursively
						AddRow2Table(pdtbResultData, pdtbBOMSource, strIndex, drow);
						iIndex++;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}
		
		private DataTable GetRootProductInfo(string pstrProductID)
		{

			OleDbConnection cn = null;
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbBOMData = BuildReportDataTemplate();

				string strSql = "SELECT  0 as PartIndex,"; 
				strSql += " ITM_Product.ProductID as ParentProductID,";
				strSql += " ITM_Product.Code as ParentPartNumber,";
				strSql += " ITM_Product.Revision as ParentPartModel,";
				strSql += " ITM_Product.Description as ParentPartName,";	
				strSql += " ITM_Product.ProductID as ProductID,";
				strSql += " ITM_Product.Code AS PartNumber,";
				strSql += " ITM_Product.Description AS PartName,";
				strSql += " ITM_Product.Revision AS PartModel";
				strSql += " FROM ITM_Product";
				strSql += " WHERE ITM_Product.MakeItem = 1 AND ITM_Product.ProductID =" + pstrProductID;
			
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
				{
					if (cn.State != ConnectionState.Closed)
						cn.Close();
					cn = null;

				}
			}
		}

		private DataTable GetBOMInfo()
		{
			OleDbConnection cn = null;
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbBOMData = new DataTable();

				string strSql = "SELECT  ITM_BOM.ProductID as ParentProductID,";
				strSql += " parent.Code as ParentPartNumber,";
				strSql += " parent.Revision as ParentPartModel,";
				strSql += " parent.Description as ParentPartName,";
				strSql += " child.ProductID as ProductID,";
				strSql += " child.Code AS PartNumber,";
				strSql += " child.Description AS PartName,";
				strSql += " child.Revision AS PartModel";

				strSql += " FROM    ITM_BOM";
				strSql += " INNER JOIN ITM_Product parent ON parent.ProductID = ITM_BOM.ProductID";
				strSql += " INNER JOIN ITM_Product child ON ITM_BOM.ComponentID = child.ProductID";
			
				strSql += " WHERE child.MakeItem = 1";

				strSql += " ORDER BY ParentProductID, ITM_BOM.Line ASC";
			
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
				{
					if (cn.State != ConnectionState.Closed)
						cn.Close();
					cn = null;
				}
			}
		}

		private DataRow[] GetBOMInfoOfProduct(DataTable pdtbBOMInfo, int pintProducID)
		{
			try
			{
				string strFilter =  PARENT_PRODUCT_ID_FLD + " = " + pintProducID;
				return pdtbBOMInfo.Select(strFilter);
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}

		private DataTable GetRoutingData(string pstrListProductIDs)
		{
			OleDbConnection cn = null;
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbRoutingData = new DataTable();
			
				string strSql = " SELECT ITM_Routing.ProductID,";

				strSql += " ITM_Routing.Step as RoutingStep,";
				strSql += " Case ITM_Routing.Type";
				strSql += " When 0 then 'Inside'";
				strSql += " Else 'Ouside'";
				strSql += " End as RoutingType,";

				strSql += " Case ITM_Routing.MachineSetupTime";
				strSql += " When 0 THEN Null";
				strSql += " Else ITM_Routing.MachineSetupTime";
				strSql += " End as MachineSetupTime,";

				strSql += " Case ITM_Routing.MachineRunTime";
				strSql += " When 0 THEN Null";
				strSql += " Else ITM_Routing.MachineRunTime";
				strSql += " End as MachineRunTime,";

				strSql += " Case ITM_Routing.Machines";
				strSql += " When 0 THEN Null";
				strSql += " Else ITM_Routing.Machines";
				strSql += " End as Machines,";

				strSql += " Case ITM_Routing.LaborRunTime";
				strSql += " When 0 THEN Null";
				strSql += " Else ITM_Routing.LaborRunTime";
				strSql += " End as LaborRunTime,";

				strSql += " Case ITM_Routing.LaborSetupTime";
				strSql += " When 0 THEN Null";
				strSql += " Else ITM_Routing.LaborSetupTime";
				strSql += " End as LaborSetupTime,";

				strSql += " Case ITM_Routing.CrewSize";
				strSql += " When 0 THEN Null";
				strSql += " Else ITM_Routing.CrewSize";
				strSql += " End as CrewSize,";

				strSql += " MST_Function.Code as FunctionCode,";
				strSql += " Case ITM_Routing.VarLT";
				strSql += " When 0 THEN Null";
				strSql += " Else ITM_Routing.VarLT";
				strSql += " End as VarLT,";

				strSql += " Case ITM_Routing.FixLT";
				strSql += " When 0 THEN Null";
				strSql += " Else ITM_Routing.FixLT";
				strSql += " End as FixLT,";

				strSql += " Case ITM_Routing.Pacer";
				strSql += " When 'L' then 'Labor'";
				strSql += " When 'M' then 'Machine'";
				strSql += " Else 'Both'";
				strSql += " End as Pacer,";
				
				strSql += " Case ITM_Routing.Pacer";
				strSql += " When 'L'  then ISNULL(ITM_Routing.LaborRunTime, 0) + ISNULL(ITM_Routing.LaborSetupTime, 0)";
				strSql += " When 'M'  then ISNULL(ITM_Routing.MachineRunTime, 0) + ISNULL(ITM_Routing.MachineSetupTime, 0)";
				strSql += " When 'B'  then ISNULL(ITM_Routing.LaborRunTime, 0) + ISNULL(ITM_Routing.LaborSetupTime, 0) + ISNULL(ITM_Routing.MachineRunTime, 0) + ISNULL(ITM_Routing.MachineSetupTime, 0)";
				strSql += " End as LeadTime,";
				
				strSql += " Case";
				strSql += " when ITM_Routing.OverlapPercent > 0 then 'Overlap ' + Convert( varchar(15), FLOOR(ITM_Routing.OverlapPercent)) + '%'";
				strSql += " when ITM_Routing.OverlapQty > 0 then 'Overlap ' + Convert(varchar(15), FLOOR(ITM_Routing.OverlapQty)) + ' ' + MST_UnitOfMeasure.Code";
				strSql += " when ITM_Routing.ScheduleSeq > 0 then 'Sequence ' + Convert(varchar(15), FLOOR(ITM_Routing.ScheduleSeq))";
				strSql += " else 'Linear'";
				strSql += " End as ScheduleType,";

				strSql += " MST_Party.Code as PartyCode,";
				strSql += " MST_WorkCenter.Code + ' (' +  MST_WorkCenter.Name + ')' as WorkCenterName,";
				strSql += " MST_WorkCenter.IsMain";

				strSql += " FROM    ITM_Routing";
				strSql += " INNER JOIN  ITM_Product ON ITM_Routing.ProductID = ITM_Product.ProductID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";
				strSql += " LEFT JOIN  MST_Function ON ITM_Routing.FunctionID = MST_Function.FunctionID";
				strSql += " LEFT JOIN  MST_Party ON ITM_Routing.PartyID = MST_Party.PartyID";
				strSql += " LEFT JOIN MST_WorkCenter ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID";
				
				strSql += " WHERE ITM_Routing.ProductID IN " + pstrListProductIDs;

				strSql += " ORDER BY ITM_Routing.ProductID,	ITM_Routing.Step ASC";
				
				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, cn);

				odad.Fill(dtbRoutingData);		
				return dtbRoutingData;
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

		#endregion Processing Data		
		
		#region Public Method

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		public DataTable ExecuteReport(string pstrCCNID, string pstrProductID)
		{			
			try
			{
				const string REPORT_NAME = "RoutingReport";
				const string SUB_REPORT_NAME = "SubRoutingReport";
				const string REPORT_LAYOUT = "RoutingReport.xml";
				const string REPORT_TITLE = "Routing Management Report";
				
				const string RPT_TITLE_FIELD = "fldTitle";

				DataTable dtbBOMData = new DataTable(BOM_PRODUCT_TABLE);
				DataTable dtbRoutingData = new DataTable(ROUTING_PRODUCT_TABLE);

				strListProductIDs = "(" + pstrProductID;

				int intProductID = 0;				
				intProductID = int.Parse(pstrProductID);
				//Build data
				dtbBOMData = BuildBOMDataTable(intProductID);
				if(dtbBOMData != null)
				{
					if(dtbBOMData.Rows.Count == 0)
					{
						dtbBOMData = GetRootProductInfo(pstrProductID);
					}
				}

				strListProductIDs += ")";
				dtbRoutingData = GetRoutingData(strListProductIDs);
	
				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
			
				//Set Datasource
				reportBuilder.SourceDataTable = dtbBOMData;
				reportBuilder.SubReportDataSources.Add(SUB_REPORT_NAME, dtbRoutingData);
			
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = mstrReportDefinitionFolder;
				reportBuilder.ReportLayoutFile = REPORT_LAYOUT;
			
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
			
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text;
				}
				catch
				{
					printPreview.FormTitle = REPORT_TITLE;
				}

				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();
			
				reportBuilder.RefreshReport();
				printPreview.Show();
			
				return dtbBOMData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		
		#endregion Public Method	
		
	}
}