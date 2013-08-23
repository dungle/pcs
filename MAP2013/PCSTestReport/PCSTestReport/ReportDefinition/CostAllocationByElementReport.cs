using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using C1.Win.C1Preview;
using PCSUtils.Utils;

namespace CostAllocationByElementReport
{
	[Serializable]
	public class CostAllocationByElementReport : MarshalByRefObject, IDynamicReport
	{
		private const string PRODUCT_CODE = "Code";
		private const string PRODUCT_NAME = "Description";
		private const string PRODUCT_MODEL = "Revision";

		private const string CODE_FIELD = "Code";
		private const string NAME_FIELD = "Name";

		public CostAllocationByElementReport()
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
		
		/// <summary>
		/// Get Location Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetDepartmentInfo(string pstrID)
		{
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
					+ " FROM MST_Department"
					+ " WHERE MST_Department.DepartmentID = " + pstrID;
				
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
		/// Get Production Line Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetProductionLineInfo(string pstrID)
		{
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
					+ " FROM PRO_ProductionLine"
					+ " WHERE PRO_ProductionLine.ProductionLineID = " + pstrID;
				
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
		/// Get Product Group Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetProductGroupInfo(string pstrID)
		{
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + PRODUCT_NAME
					+ " FROM CST_ProductGroup"
					+ " WHERE CST_ProductGroup.ProductGroupID = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[CODE_FIELD].ToString().Trim() + " (" + odrPCS[PRODUCT_NAME].ToString().Trim() + ")";
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
		/// Get Period Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetPeriodInfo(string pstrID)
		{
			const string PERIOD_NAME_FLD = "PeriodName";
			const string FROM_DATE_FLD = "FromDate";
			const string TO_DATE_FLD = "ToDate";
			const string DATE_TIME_FORMAT = "dd-MM-yyyy";


			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT PeriodName, FromDate, ToDate"
					+ " FROM cst_ActCostAllocationMaster"
					+ " WHERE cst_ActCostAllocationMaster.ActCostAllocationMasterID = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[PERIOD_NAME_FLD].ToString().Trim() + " ( From " + ((DateTime)odrPCS[FROM_DATE_FLD]).ToString(DATE_TIME_FORMAT)  + " to " + ((DateTime)odrPCS[TO_DATE_FLD]).ToString(DATE_TIME_FORMAT) + ")";
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
		/// Get Product Group Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCostElementInfo(string pstrID)
		{
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
					+ " FROM STD_CostElement"
					+ " WHERE STD_CostElement.CostElementID = " + pstrID;
				
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

		private DataTable GetAllocationResultData(string pstrCCNID, string pstrPeriodID, string pstrDepartmentID, 
			string pstrProductionLineID, string pstrProductGroupID, string pstrProductID, string pstrModel, string pstrCostElementID)
		{
			OleDbConnection cn = new OleDbConnection(mConnectionString);
			DataTable dtbAllocationResult = new DataTable();

			try
			{
				//Build WHERE clause
				string strWhereClause = " AND ITM_Product.CCNID =" + pstrCCNID;
				strWhereClause += " AND cst_AllocationResult.ActCostAllocationMasterID=" + pstrPeriodID;

				//Department
				if(pstrDepartmentID != null && pstrDepartmentID != string.Empty)
				{
					strWhereClause += " AND cst_AllocationResult.DepartmentID =" + pstrDepartmentID; 
				}

				//Production Line
				if(pstrProductionLineID != null && pstrProductionLineID != string.Empty)
				{
					strWhereClause += " AND cst_AllocationResult.ProductionLineID =" + pstrProductionLineID; 
				}

				//Product group
				if(pstrProductGroupID != null && pstrProductGroupID != string.Empty)
				{
					strWhereClause += " AND cst_AllocationResult.ProductGroupID =" + pstrProductGroupID; 
				}

				//Product
				if(pstrProductID != null && pstrProductID != string.Empty)
				{
					strWhereClause += " AND cst_AllocationResult.ProductID =" + pstrProductID; 
				}

				//Model 
				if(pstrModel != null && pstrModel != string.Empty)
				{
					strWhereClause += " AND ITM_Product.Revision ='" + pstrModel.Replace("'", "''") + "'"; 
				}

				//Cost Element ID
				if(pstrCostElementID != null && pstrCostElementID != string.Empty)
				{
					strWhereClause += " AND cst_AllocationResult.CostElementID =" + pstrCostElementID;
				}
				
				//Build SQL string

				//Case: By Product
				string strSql = "SELECT  MST_Department.Code AS MST_DepartmentCode,";
				strSql += " PRO_ProductionLine.Code AS PRO_ProductionLineCode,";
 				strSql += " CST_ProductGroup.Code AS CST_ProductGroupCode,";
				strSql += " STD_CostElement.Name AS STD_CostElementName,";
				strSql += " ITM_Product.Code AS ITM_ProductCode,";
				strSql += " ITM_Product.Description AS ITM_ProductDescription,";
				strSql += " ITM_Product.Revision AS ITM_ProductRevision,";
				strSql += " ISNULL(ITM_Product.LTVariableTime,0) AS LeadTime,";
				strSql += " MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode,";

				strSql += " cst_AllocationResult.Rate as FactoryRate,";
				strSql += " NULL as DepartmentRate,";
				strSql += " NULL as ProductionLineRate,";
				strSql += " NULL as ProductGroupRate,";

				strSql += " cst_AllocationResult.Amount as FactoryCost,";
				strSql += " NULL as DepartmentCost,";
				strSql += " NULL as ProductionLineCost,";
				strSql += " NULL as ProductGroupCost,";

				strSql += " cst_AllocationResult.ProductID,";
				strSql += " cst_ActCostAllocationMaster.PeriodName,";
				strSql += " cst_AllocationResult.CompletedQuantity,"; 
				strSql += " ISNULL(cst_AllocationResult.CompletedQuantity,0) * ISNULL(ITM_Product.LTVariableTime,0) AS TotalTime,"; 
				strSql += " cst_AllocationResult.DepartmentID,";
				strSql += " cst_AllocationResult.ProductionLineID,";
				strSql += " cst_AllocationResult.AllocationResultID,";
				strSql += " cst_AllocationResult.ActCostAllocationMasterID,";
				strSql += " cst_AllocationResult.ProductGroupID,";
				strSql += " cst_AllocationResult.CostElementID,";	
				strSql += " ITM_Product.CCNID";

				strSql += " FROM cst_AllocationResult";
				strSql += " INNER JOIN STD_CostElement ON cst_AllocationResult.CostElementID = STD_CostElement.CostElementID";
				strSql += " INNER JOIN cst_ActCostAllocationMaster ON cst_AllocationResult.ActCostAllocationMasterID = cst_ActCostAllocationMaster.ActCostAllocationMasterID";
				strSql += " INNER JOIN ITM_Product ON cst_AllocationResult.ProductID = ITM_Product.ProductID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";

				strSql += " LEFT JOIN CST_ProductGroup ON cst_AllocationResult.ProductGroupID = CST_ProductGroup.ProductGroupID";
				strSql += " LEFT JOIN MST_Department ON cst_AllocationResult.DepartmentID = MST_Department.DepartmentID";
				strSql += " LEFT JOIN PRO_ProductionLine ON cst_AllocationResult.ProductionLineID = PRO_ProductionLine.ProductionLineID";

				strSql += " WHERE 	cst_AllocationResult.DepartmentID IS NULL";
				strSql += " AND cst_AllocationResult.ProductionLineID IS NULL";
				strSql += " AND cst_AllocationResult.ProductGroupID IS NULL";

				//add where clause
				strSql += strWhereClause;

				strSql += " UNION ";
				
				//Case: By Department
				strSql += " SELECT  MST_Department.Code AS MST_DepartmentCode,";
				strSql += " PRO_ProductionLine.Code AS PRO_ProductionLineCode,";
 				strSql += " CST_ProductGroup.Code AS CST_ProductGroupCode,";
				strSql += " STD_CostElement.Name AS STD_CostElementName,";
				strSql += " ITM_Product.Code AS ITM_ProductCode,";
				strSql += " ITM_Product.Description AS ITM_ProductDescription,";
				strSql += " ITM_Product.Revision AS ITM_ProductRevision,";
				strSql += " ISNULL(ITM_Product.LTVariableTime,0) AS LeadTime,";
				strSql += " MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode,";
				
				strSql += " NULL as FactoryRate,";
				strSql += " cst_AllocationResult.Rate as DepartmentRate,";
				strSql += " NULL as ProductionLineRate,";
				strSql += " NULL as ProductGroupRate,";
				
				strSql += " NULL as FactoryCost,";
				strSql += " cst_AllocationResult.Amount as DepartmentCost,";
				strSql += " NULL as ProductionLineCost,";
				strSql += " NULL as ProductGroupCost,";

				strSql += " cst_AllocationResult.ProductID,";
				strSql += " cst_ActCostAllocationMaster.PeriodName,";
				strSql += " cst_AllocationResult.CompletedQuantity,";
				strSql += " ISNULL(cst_AllocationResult.CompletedQuantity,0) * ISNULL(ITM_Product.LTVariableTime,0) AS TotalTime,"; 
				strSql += " cst_AllocationResult.DepartmentID,";
				strSql += " cst_AllocationResult.ProductionLineID,";
				strSql += " cst_AllocationResult.AllocationResultID,"; 
				strSql += " cst_AllocationResult.ActCostAllocationMasterID,";
				strSql += " cst_AllocationResult.ProductGroupID,";
				strSql += " cst_AllocationResult.CostElementID,";	
				strSql += " ITM_Product.CCNID";

				strSql += " FROM cst_AllocationResult";
				strSql += " INNER JOIN STD_CostElement ON cst_AllocationResult.CostElementID = STD_CostElement.CostElementID";
				strSql += " INNER JOIN cst_ActCostAllocationMaster ON cst_AllocationResult.ActCostAllocationMasterID = cst_ActCostAllocationMaster.ActCostAllocationMasterID";
				strSql += " INNER JOIN ITM_Product ON cst_AllocationResult.ProductID = ITM_Product.ProductID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";

				strSql += " INNER JOIN MST_Department ON cst_AllocationResult.DepartmentID = MST_Department.DepartmentID";
				strSql += " LEFT JOIN PRO_ProductionLine ON cst_AllocationResult.ProductionLineID = PRO_ProductionLine.ProductionLineID";
				strSql += " LEFT JOIN CST_ProductGroup ON cst_AllocationResult.ProductGroupID = CST_ProductGroup.ProductGroupID";

				strSql += " WHERE 	cst_AllocationResult.DepartmentID IS NOT NULL";
				strSql += " AND cst_AllocationResult.ProductionLineID IS NULL";
				strSql += " AND cst_AllocationResult.ProductGroupID IS NULL";
				
				//add where clause
				strSql += strWhereClause;

				strSql += " UNION ";
				
				//Case: By Production Line
				strSql += " SELECT  MST_Department.Code AS MST_DepartmentCode,";
				strSql += " PRO_ProductionLine.Code AS PRO_ProductionLineCode,";
 				strSql += " CST_ProductGroup.Code AS CST_ProductGroupCode,";
				strSql += " STD_CostElement.Name AS STD_CostElementName,";
				strSql += " ITM_Product.Code AS ITM_ProductCode,";
				strSql += " ITM_Product.Description AS ITM_ProductDescription,";
				strSql += " ITM_Product.Revision AS ITM_ProductRevision,";
				strSql += " ISNULL(ITM_Product.LTVariableTime,0) AS LeadTime,";
				strSql += " MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode,";
				
				strSql += " NULL as FactoryRate,";
				strSql += " NULL as DepartmentRate,";
				strSql += " cst_AllocationResult.Rate as ProductionLineRate,";
				strSql += " NULL as ProductGroupRate,";

				strSql += " NULL as FactoryCost,";
				strSql += " NULL as DepartmentCost,";
				strSql += " cst_AllocationResult.Amount as ProductionLineCost,";
				strSql += " NULL as ProductGroupCost,";

				strSql += " cst_AllocationResult.ProductID,";
				strSql += " cst_ActCostAllocationMaster.PeriodName,";
				strSql += " cst_AllocationResult.CompletedQuantity,";
				strSql += " ISNULL(cst_AllocationResult.CompletedQuantity,0) * ISNULL(ITM_Product.LTVariableTime,0) AS TotalTime,"; 
				strSql += " cst_AllocationResult.DepartmentID,";
				strSql += " cst_AllocationResult.ProductionLineID,";
				strSql += " cst_AllocationResult.AllocationResultID,"; 
				strSql += " cst_AllocationResult.ActCostAllocationMasterID,";
				strSql += " cst_AllocationResult.ProductGroupID,";
				strSql += " cst_AllocationResult.CostElementID,";
				strSql += " ITM_Product.CCNID";

				strSql += " FROM cst_AllocationResult";
				strSql += " INNER JOIN STD_CostElement ON cst_AllocationResult.CostElementID = STD_CostElement.CostElementID";
				strSql += " INNER JOIN cst_ActCostAllocationMaster ON cst_AllocationResult.ActCostAllocationMasterID = cst_ActCostAllocationMaster.ActCostAllocationMasterID";
				strSql += " INNER JOIN ITM_Product ON cst_AllocationResult.ProductID = ITM_Product.ProductID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";
				
				strSql += " INNER JOIN MST_Department ON cst_AllocationResult.DepartmentID = MST_Department.DepartmentID";
				strSql += " INNER JOIN PRO_ProductionLine ON cst_AllocationResult.ProductionLineID = PRO_ProductionLine.ProductionLineID";
				strSql += " LEFT JOIN CST_ProductGroup ON cst_AllocationResult.ProductGroupID = CST_ProductGroup.ProductGroupID";

				strSql += " WHERE 	cst_AllocationResult.DepartmentID IS NOT NULL";
				strSql += " AND cst_AllocationResult.ProductionLineID IS NOT NULL";
				strSql += " AND cst_AllocationResult.ProductGroupID IS NULL";

				//add where clause
				strSql += strWhereClause;

				strSql += " UNION ";

				//Case: By Product Group
				strSql += " SELECT  MST_Department.Code AS MST_DepartmentCode,";
				strSql += " PRO_ProductionLine.Code AS PRO_ProductionLineCode,";
 				strSql += " CST_ProductGroup.Code AS CST_ProductGroupCode,";
				strSql += " STD_CostElement.Name AS STD_CostElementName,";
				strSql += " ITM_Product.Code AS ITM_ProductCode,";
				strSql += " ITM_Product.Description AS ITM_ProductDescription,";
				strSql += " ITM_Product.Revision AS ITM_ProductRevision,";
				strSql += " ISNULL(ITM_Product.LTVariableTime,0) AS LeadTime,";
				strSql += " MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode,";
				
				strSql += " NULL as FactoryRate,";
				strSql += " NULL as DepartmentRate,";
				strSql += " NULL as ProductionLineRate,";
				strSql += " cst_AllocationResult.Rate as ProductGroupRate,";
				
				strSql += " NULL as FactoryCost,";
				strSql += " NULL as DepartmentCost,";
				strSql += " NULL as ProductionLineCost,";
				strSql += " cst_AllocationResult.Amount as ProductGroupCost,";

				strSql += " cst_AllocationResult.ProductID,";
				strSql += " cst_ActCostAllocationMaster.PeriodName,";
				strSql += " cst_AllocationResult.CompletedQuantity,";
				strSql += " ISNULL(cst_AllocationResult.CompletedQuantity,0) * ISNULL(ITM_Product.LTVariableTime,0) AS TotalTime,"; 
				strSql += " cst_AllocationResult.DepartmentID,";
				strSql += " cst_AllocationResult.ProductionLineID,";
				strSql += " cst_AllocationResult.AllocationResultID,";
				strSql += " cst_AllocationResult.ActCostAllocationMasterID,";
				strSql += " cst_AllocationResult.ProductGroupID,";
				strSql += " cst_AllocationResult.CostElementID,";
				strSql += " ITM_Product.CCNID";

				strSql += " FROM    cst_AllocationResult";
				strSql += " INNER JOIN STD_CostElement ON cst_AllocationResult.CostElementID = STD_CostElement.CostElementID";
				strSql += " INNER JOIN cst_ActCostAllocationMaster ON cst_AllocationResult.ActCostAllocationMasterID = cst_ActCostAllocationMaster.ActCostAllocationMasterID";
				strSql += " INNER JOIN ITM_Product ON cst_AllocationResult.ProductID = ITM_Product.ProductID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";

				strSql += " INNER JOIN CST_ProductGroup ON cst_AllocationResult.ProductGroupID = CST_ProductGroup.ProductGroupID";
				strSql += " INNER JOIN MST_Department ON cst_AllocationResult.DepartmentID = MST_Department.DepartmentID";
				strSql += " INNER JOIN PRO_ProductionLine ON cst_AllocationResult.ProductionLineID = PRO_ProductionLine.ProductionLineID";

				strSql += " WHERE 	cst_AllocationResult.DepartmentID IS NOT NULL";
				strSql += " AND cst_AllocationResult.ProductionLineID IS NOT NULL";
				strSql += " AND cst_AllocationResult.ProductGroupID IS NOT NULL";
				
				//add where clause
				strSql += strWhereClause;

				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, cn);

				odad.Fill(dtbAllocationResult);				
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

			return dtbAllocationResult;
		}

		#endregion Private Method

		#region Public Method

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		public DataTable ExecuteReport(string pstrCCNID, string pstrPeriodID, string pstrDepartmentID, 
			string pstrProductionLineID, string pstrProductGroupID, string pstrProductID, string pstrModel, string pstrCostElementID)
		{	
			//Report name
			const string REPORT_NAME = "CostAllocationByElementReport";
			const string REPORT_LAYOUT = "CostAllocationByElementReport.xml";
								
			const string RPT_PAGE_HEADER = "PageHeader";
			//Report field names
			const string RPT_TITLE_FIELD = "fldTitle";
			//Report field names
			const string RPT_CCN		= "CCN";
			const string RPT_PERIOD		= "Period";
			const string RPT_DEPARTMENT = "Department";
			const string RPT_PRODUCTION_LINE = "Production Line";
			const string RPT_PRODUCT_GROUP = "Product Group";
				
			const string RPT_PART_NO   = "Part No.";
			const string RPT_PART_NAME = "Part Name";
			const string RPT_MODEL     = "Model";

			const string RPT_COST_ELEMENT = "Cost Element";
			const string CompletedQuantity = "CompletedQuantity";
			const string FactoryCost = "FactoryCost";
			const string DepartmentCost = "DepartmentCost";
			const string ProductionLineCost = "ProductionLineCost";
			const string ProductGroupCost = "ProductGroupCost";
			const string TotalTime = "TotalTime";

			DataTable dtbItemList = GetAllocationResultData(pstrCCNID, pstrPeriodID, pstrDepartmentID, pstrProductionLineID, pstrProductGroupID, pstrProductID, pstrModel, pstrCostElementID);

			#region adding more column to report table

			string[] arrCol1 = new string[]{"Department", "Production", "Group"};
			string[] arrCol2 = new string[]{CompletedQuantity, FactoryCost, DepartmentCost, ProductionLineCost, ProductGroupCost, TotalTime};
			foreach (string strCol1 in arrCol1)
			{
				foreach (string strCol2 in arrCol2)
				{
					dtbItemList.Columns.Add(new DataColumn(strCol1 + strCol2, typeof(decimal)));
					dtbItemList.Columns[strCol1 + strCol2].AllowDBNull = true;
				}
			}

			#endregion

			DataTable dtbReportData = dtbItemList.Clone();

			#region 03-08-2006 dungla

			DataTable dtbDeptData = new DataTable();
			dtbDeptData.Columns.Add(new DataColumn("ID", typeof(int)));
			dtbDeptData.Columns["ID"].AllowDBNull = true;
			dtbDeptData.Columns.Add(new DataColumn(CompletedQuantity, typeof(decimal)));
			dtbDeptData.Columns.Add(new DataColumn(FactoryCost, typeof(decimal)));
			dtbDeptData.Columns.Add(new DataColumn(DepartmentCost, typeof(decimal)));
			dtbDeptData.Columns.Add(new DataColumn(ProductionLineCost, typeof(decimal)));
			dtbDeptData.Columns.Add(new DataColumn(ProductGroupCost, typeof(decimal)));
			dtbDeptData.Columns.Add(new DataColumn(TotalTime, typeof(decimal)));
			DataTable dtbProductionData = dtbDeptData.Clone();
			DataTable dtbGroupData = dtbDeptData.Clone();
			
			#region calculate data for department

			// order by Department id
			DataRow[] drowsData = dtbItemList.Select(string.Empty, "DepartmentID ASC");
			// build the id list
			ArrayList arrID = new ArrayList();
			foreach (DataRow drowData in drowsData)
			{
				if (!arrID.Contains(drowData["DepartmentID"].ToString()))
					arrID.Add(drowData["DepartmentID"].ToString());
			}
			foreach (string strID in arrID)
			{
				#region row id

				DataRow drowData = dtbDeptData.NewRow();
				try
				{
					drowData["ID"] = Convert.ToInt32(strID);
				}
				catch{}

				#endregion

				#region data

				string strFilter = string.Empty;
				if (strID != string.Empty)
					strFilter = "DepartmentID = " + strID;
				else
					strFilter = "DepartmentID IS NULL ";
				DataRow[] drowsDept = dtbItemList.Select(strFilter, "ProductID ASC");
				string strLastID = string.Empty, strLastCostElementID = string.Empty;
				string strLastLineID = string.Empty, strLastGroupID = string.Empty;
				decimal decCompletedQuantity = 0, decFactoryCost = 0, decProductionCost = 0;
				decimal decGroupCost = 0, decDepartmentCost = 0, decTotalTime = 0;
				foreach (DataRow row in drowsDept)
				{
					if (strLastCostElementID != row["CostElementID"].ToString() ||
						strLastLineID != row["ProductionLineID"].ToString() ||
						strLastGroupID != row["ProductGroupID"].ToString() ||
						strLastID != row["ProductID"].ToString())
					{
						strLastCostElementID = (strLastCostElementID != row["CostElementID"].ToString()) ? row["CostElementID"].ToString() : strLastCostElementID;
						strLastLineID = (strLastLineID != row["ProductionLineID"].ToString()) ? row["ProductionLineID"].ToString() : strLastLineID;
						strLastGroupID = (strLastGroupID != row["ProductGroupID"].ToString()) ? row["ProductGroupID"].ToString() : strLastGroupID;
						try
						{
							decFactoryCost += Convert.ToDecimal(row[FactoryCost]);
						}
						catch{}
						try
						{
							decDepartmentCost += Convert.ToDecimal(row[DepartmentCost]);
						}
						catch{}
						try
						{
							decProductionCost += Convert.ToDecimal(row[ProductionLineCost]);
						}
						catch{}
						try
						{
							decGroupCost += Convert.ToDecimal(row[ProductGroupCost]);
						}
						catch{}
					}
					if (strLastID != row["ProductID"].ToString())
					{
						strLastID = row["ProductID"].ToString();
						try
						{
							 decCompletedQuantity += Convert.ToDecimal(row[CompletedQuantity]);
						}
						catch{}
						try
						{
							decTotalTime += Convert.ToDecimal(row[CompletedQuantity]) * Convert.ToDecimal(row["LeadTime"]);
						}
						catch{}
					}
				}
				drowData[CompletedQuantity] = decCompletedQuantity;
				drowData[TotalTime] = decTotalTime;
				drowData[FactoryCost] = decFactoryCost;
				drowData[DepartmentCost] = decDepartmentCost;
				drowData[ProductionLineCost] = decProductionCost;
				drowData[ProductGroupCost] = decGroupCost;

				#endregion

				// insert to table
				dtbDeptData.Rows.Add(drowData);
			}

			#endregion

			#region calculate data for production line

			// order by production line id
			drowsData = dtbItemList.Select(string.Empty, "ProductionLineID ASC");
			// build the id list
			arrID = new ArrayList();
			foreach (DataRow drowData in drowsData)
			{
				if (!arrID.Contains(drowData["ProductionLineID"].ToString()))
					arrID.Add(drowData["ProductionLineID"].ToString());
			}
			foreach (string strID in arrID)
			{
				#region row id

				DataRow drowData = dtbProductionData.NewRow();
				try
				{
					drowData["ID"] = Convert.ToInt32(strID);
				}
				catch{}

				#endregion

				#region data

				string strFilter = string.Empty;
				if (strID != string.Empty)
					strFilter = "ProductionLineID = " + strID;
				else
					strFilter = "ProductionLineID IS NULL ";
				DataRow[] drowsDept = dtbItemList.Select(strFilter, "ProductID ASC");
				string strLastID = string.Empty, strLastCostElementID = string.Empty;
				string strLastGroupID = string.Empty;
				decimal decCompletedQuantity = 0, decFactoryCost = 0, decProductionCost = 0;
				decimal decGroupCost = 0, decDepartmentCost = 0, decTotalTime = 0;
				foreach (DataRow row in drowsDept)
				{
					if (strLastCostElementID != row["CostElementID"].ToString() ||
						strLastGroupID != row["ProductGroupID"].ToString() ||
						strLastID != row["ProductID"].ToString())
					{
						strLastCostElementID = (strLastCostElementID != row["CostElementID"].ToString()) ? row["CostElementID"].ToString() : strLastCostElementID;
						strLastGroupID = (strLastGroupID != row["ProductGroupID"].ToString()) ? row["ProductGroupID"].ToString() : strLastGroupID;
						try
						{
							decFactoryCost += Convert.ToDecimal(row[FactoryCost]);
						}
						catch{}
						try
						{
							decDepartmentCost += Convert.ToDecimal(row[DepartmentCost]);
						}
						catch{}
						try
						{
							decProductionCost += Convert.ToDecimal(row[ProductionLineCost]);
						}
						catch{}
						try
						{
							decGroupCost += Convert.ToDecimal(row[ProductGroupCost]);
						}
						catch{}
					}
					if (strLastID != row["ProductID"].ToString())
					{
						strLastID = row["ProductID"].ToString();
						try
						{
							decCompletedQuantity += Convert.ToDecimal(row[CompletedQuantity]);
						}
						catch{}
						try
						{
							decTotalTime += Convert.ToDecimal(row[CompletedQuantity]) * Convert.ToDecimal(row["LeadTime"]);
						}
						catch{}
					}
				}
				drowData[CompletedQuantity] = decCompletedQuantity;
				drowData[TotalTime] = decTotalTime;
				drowData[FactoryCost] = decFactoryCost;
				drowData[DepartmentCost] = decDepartmentCost;
				drowData[ProductionLineCost] = decProductionCost;
				drowData[ProductGroupCost] = decGroupCost;

				#endregion
					
				// insert to table
				dtbProductionData.Rows.Add(drowData);
			}

			#endregion

			#region calculate data for product group

			// order by product group id
			drowsData = dtbItemList.Select(string.Empty, "ProductGroupID ASC");
			// build the id list
			arrID = new ArrayList();
			foreach (DataRow drowData in drowsData)
			{
				if (!arrID.Contains(drowData["ProductGroupID"].ToString()))
					arrID.Add(drowData["ProductGroupID"].ToString());
			}
			foreach (string strID in arrID)
			{
				#region row id

				DataRow drowData = dtbGroupData.NewRow();
				try
				{
					drowData["ID"] = Convert.ToInt32(strID);
				}
				catch{}

				#endregion

				#region data

				string strFilter = string.Empty;
				if (strID != string.Empty)
					strFilter = "ProductGroupID = " + strID;
				else
					strFilter = "ProductGroupID IS NULL ";
				DataRow[] drowsDept = dtbItemList.Select(strFilter, "ProductID ASC");
				string strLastID = string.Empty, strLastCostElementID = string.Empty;
				decimal decCompletedQuantity = 0, decFactoryCost = 0, decProductionCost = 0;
				decimal decGroupCost = 0, decDepartmentCost = 0, decTotalTime = 0;
				foreach (DataRow row in drowsDept)
				{
					if (strLastCostElementID != row["CostElementID"].ToString())
					{
						strLastCostElementID = row["CostElementID"].ToString();
						try
						{
							decFactoryCost += Convert.ToDecimal(row[FactoryCost]);
						}
						catch{}
						try
						{
							decDepartmentCost += Convert.ToDecimal(row[DepartmentCost]);
						}
						catch{}
						try
						{
							decProductionCost += Convert.ToDecimal(row[ProductionLineCost]);
						}
						catch{}
						try
						{
							decGroupCost += Convert.ToDecimal(row[ProductGroupCost]);
						}
						catch{}
					}
					else if (strLastID != row["ProductID"].ToString())
					{
						try
						{
							decFactoryCost += Convert.ToDecimal(row[FactoryCost]);
						}
						catch{}
						try
						{
							decDepartmentCost += Convert.ToDecimal(row[DepartmentCost]);
						}
						catch{}
						try
						{
							decProductionCost += Convert.ToDecimal(row[ProductionLineCost]);
						}
						catch{}
						try
						{
							decGroupCost += Convert.ToDecimal(row[ProductGroupCost]);
						}
						catch{}
					}
					if (strLastID != row["ProductID"].ToString())
					{
						strLastID = row["ProductID"].ToString();
						try
						{
							decCompletedQuantity += Convert.ToDecimal(row[CompletedQuantity]);
						}
						catch{}
						try
						{
							decTotalTime += Convert.ToDecimal(row[CompletedQuantity]) * Convert.ToDecimal(row["LeadTime"]);
						}
						catch{}
					}
				}
				drowData[CompletedQuantity] = decCompletedQuantity;
				drowData[TotalTime] = decTotalTime;
				drowData[FactoryCost] = decFactoryCost;
				drowData[DepartmentCost] = decDepartmentCost;
				drowData[ProductionLineCost] = decProductionCost;
				drowData[ProductGroupCost] = decGroupCost;

				#endregion
					
				// insert to table
				dtbGroupData.Rows.Add(drowData);
			}

			#endregion

			#region refine data for each row

			// order data by ProductID
			DataRow[] drowsItemList = dtbItemList.Select(string.Empty, "ProductID ASC");
			// we need to remove the quantity of duplicate item in order to sum the quantity for each element
			string strLastProductID = string.Empty, strLastElementID = string.Empty;
			string strLastDeptID = string.Empty, strLastProLineID = string.Empty, strLastProGroupID = string.Empty;
			decimal decTotalQuantity = 0, decTotalFactory = 0, decTotalDepartment = 0;
			decimal decTotalProductionLine = 0, decTotalProductGroup = 0;
			foreach (DataRow drowData in drowsItemList)
			{
				string strProductID = drowData["ProductID"].ToString();
				if (strLastElementID != drowData["CostElementID"].ToString() ||
					(strLastDeptID != drowData["DepartmentID"].ToString()) ||
					(strLastProLineID != drowData["ProductionLineID"].ToString()) ||
					(strLastProGroupID != drowData["ProductGroupID"].ToString()) ||
					(strLastProductID != strProductID))
				{
					strLastElementID = (strLastElementID != drowData["CostElementID"].ToString()) ? drowData["CostElementID"].ToString() : strLastElementID;
					strLastDeptID = (strLastDeptID != drowData["DepartmentID"].ToString()) ? drowData["DepartmentID"].ToString() : strLastDeptID;
					strLastProLineID = (strLastProLineID != drowData["ProductionLineID"].ToString()) ? drowData["ProductionLineID"].ToString() : strLastProLineID;
					strLastProGroupID = (strLastProGroupID != drowData["ProductGroupID"].ToString()) ? drowData["ProductGroupID"].ToString() : strLastProGroupID;

					#region count total factory cost

					try
					{
						decTotalFactory += Convert.ToDecimal(drowData[FactoryCost]);
					}
					catch{}

					#endregion

					#region count total department cost

					try
					{
						decTotalDepartment += Convert.ToDecimal(drowData[DepartmentCost]);
					}
					catch{}

					#endregion
						
					#region count total production line cost

					try
					{
						decTotalProductionLine += Convert.ToDecimal(drowData[ProductionLineCost]);
					}
					catch{}

					#endregion
						
					#region count total product group cost

					try
					{
						decTotalProductGroup += Convert.ToDecimal(drowData[ProductGroupCost]);
					}
					catch{}

					#endregion
				}
				if (strLastProductID != strProductID)
				{
					strLastProductID = strProductID;

					#region count total quantity

					try
					{
						decTotalQuantity += Convert.ToDecimal(drowData[CompletedQuantity]);
					}
					catch{}

					#endregion
				}
				
				string strDepartmentID = drowData["DepartmentID"].ToString();
				string strDepFilter = string.Empty;
				if (strDepartmentID != string.Empty)
					strDepFilter = "ID = " + strDepartmentID;
				else
					strDepFilter = "ID = IS NULL";
				string strProductionID = drowData["ProductionLineID"].ToString();
				string strProductionFilter = string.Empty;
				if (strProductionID != string.Empty)
					strProductionFilter = "ID = " + strProductionID;
				else
					strProductionFilter = "ID = IS NULL";
				string strGroupID = drowData["ProductGroupID"].ToString();
				string strGroupFilter = string.Empty;
				if (strGroupID != string.Empty)
					strGroupFilter = "ID = " + strGroupID;
				else
					strGroupFilter = "ID = IS NULL";
				
				#region department data
				try
				{
					drowData["Department" + CompletedQuantity] = dtbDeptData.Compute("SUM(" + CompletedQuantity + ")", strDepFilter);
				}
				catch{}
				try
				{
					drowData["Department" + TotalTime] = dtbDeptData.Compute("SUM(" + TotalTime + ")", strDepFilter);
				}
				catch{}
				try
				{
					drowData["Department" + FactoryCost] = dtbDeptData.Compute("SUM(" + FactoryCost + ")", strDepFilter);
				}
				catch{}
				try
				{
					drowData["Department" + DepartmentCost] = dtbDeptData.Compute("SUM(" + DepartmentCost + ")", strDepFilter);
				}
				catch{}
				try
				{
					drowData["Department" + ProductionLineCost] = dtbDeptData.Compute("SUM(" + ProductionLineCost + ")", strDepFilter);
				}
				catch{}
				try
				{
					drowData["Department" + ProductGroupCost] = dtbDeptData.Compute("SUM(" + ProductGroupCost + ")", strDepFilter);
				}
				catch{}
				#endregion

				#region production line data
				try
				{
					drowData["Production" + CompletedQuantity] = dtbProductionData.Compute("SUM(" + CompletedQuantity + ")", strProductionFilter);
				}
				catch{}
				try
				{
					drowData["Production" + TotalTime] = dtbProductionData.Compute("SUM(" + TotalTime + ")", strProductionFilter);
				}
				catch{}
				try
				{
					drowData["Production" + FactoryCost] = dtbProductionData.Compute("SUM(" + FactoryCost + ")", strProductionFilter);
				}
				catch{}
				try
				{
					drowData["Production" + DepartmentCost] = dtbProductionData.Compute("SUM(" + DepartmentCost + ")", strProductionFilter);
				}
				catch{}
				try
				{
					drowData["Production" + ProductionLineCost] = dtbProductionData.Compute("SUM(" + ProductionLineCost + ")", strProductionFilter);
				}
				catch{}
				try
				{
					drowData["Production" + ProductGroupCost] = dtbProductionData.Compute("SUM(" + ProductGroupCost + ")", strProductionFilter);
				}
				catch{}
				#endregion

				#region product group data
				try
				{
					drowData["Group" + CompletedQuantity] = dtbGroupData.Compute("SUM(" + CompletedQuantity + ")", strGroupFilter);
				}
				catch{}
				try
				{
					drowData["Group" + TotalTime] = dtbGroupData.Compute("SUM(" + TotalTime + ")", strGroupFilter);
				}
				catch{}
				try
				{
					drowData["Group" + FactoryCost] = dtbGroupData.Compute("SUM(" + FactoryCost + ")", strGroupFilter);
				}
				catch{}
				try
				{
					drowData["Group" + DepartmentCost] = dtbGroupData.Compute("SUM(" + DepartmentCost + ")", strGroupFilter);
				}
				catch{}
				try
				{
					drowData["Group" + ProductionLineCost] = dtbGroupData.Compute("SUM(" + ProductionLineCost + ")", strGroupFilter);
				}
				catch{}
				try
				{
					drowData["Group" + ProductGroupCost] = dtbGroupData.Compute("SUM(" + ProductGroupCost + ")", strGroupFilter);
				}
				catch{}
				#endregion

				// import to result table
				dtbReportData.ImportRow(drowData);
			}

			#endregion

			#endregion 03-08-2006 dungla

			//Create builder object
			ReportBuilder reportBuilder = new ReportBuilder();
			//Set report name
			reportBuilder.ReportName = REPORT_NAME;
			
			//Set Datasource
			reportBuilder.SourceDataTable = dtbReportData; //dtbItemList;		
			
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
				
			//CCN
			string strLabelValue = GetCCNInfoByID(pstrCCNID);
			arrParamAndValue.Add(RPT_CCN, strLabelValue);
				
			//Period
			strLabelValue = GetPeriodInfo(pstrPeriodID);
			arrParamAndValue.Add(RPT_PERIOD, strLabelValue);

			//Department				
			if(pstrDepartmentID != string.Empty)
			{
				strLabelValue = GetDepartmentInfo(pstrDepartmentID);
				arrParamAndValue.Add(RPT_DEPARTMENT, strLabelValue);
			}

			//Production Line
			if(pstrProductionLineID != string.Empty)
			{
				strLabelValue = GetProductionLineInfo(pstrProductionLineID);
				arrParamAndValue.Add(RPT_PRODUCTION_LINE, strLabelValue);
			}

			//Product Group
			if(pstrProductGroupID != string.Empty)
			{
				strLabelValue = GetProductGroupInfo(pstrProductGroupID);
				arrParamAndValue.Add(RPT_PRODUCT_GROUP, strLabelValue);
			}
				
			//Product
			if(pstrProductID != string.Empty)
			{
				Hashtable htbProduct = GetProductInfoByID(pstrProductID);
				arrParamAndValue.Add(RPT_PART_NO, htbProduct[PRODUCT_CODE].ToString());
				arrParamAndValue.Add(RPT_PART_NAME, htbProduct[PRODUCT_NAME].ToString());
				arrParamAndValue.Add(RPT_MODEL, htbProduct[PRODUCT_MODEL].ToString());
			}
				
			//Product Model
			if((pstrModel != string.Empty) && (pstrProductID == string.Empty))
			{
				arrParamAndValue.Add(RPT_MODEL, pstrModel);
			}
				
			//Cost Element
			if(pstrCostElementID != string.Empty)
			{
				strLabelValue = GetCostElementInfo(pstrCostElementID);
				arrParamAndValue.Add(RPT_COST_ELEMENT, strLabelValue);
			}

			//Anchor the Parameter drawing canvas cordinate to the fldTitle
			C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD);
			double dblStartX = fldTitle.Left;
			double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
			//reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
			reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);
				
			try
			{
				printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text;
			}
			catch{}

			try
			{
				reportBuilder.DrawPredefinedField("fldTotalCompletedQuantity", decTotalQuantity.ToString());
			}
			catch{}
			try
			{
				reportBuilder.DrawPredefinedField("fldTotalFactoryCost", decTotalFactory.ToString());
			}
			catch{}
			try
			{
				reportBuilder.DrawPredefinedField("fldTotalDepartmentCost", decTotalDepartment.ToString());
			}
			catch{}
			try
			{
				reportBuilder.DrawPredefinedField("fldTotalProductionCost", decTotalProductionLine.ToString());
			}
			catch{}
			try
			{
				reportBuilder.DrawPredefinedField("fldTotalGroupCost", decTotalProductGroup.ToString());
			}
			catch{}
				
			//Show report
			reportBuilder.RefreshReport();
			printPreview.Show();
			
			return dtbItemList;
		}
		
		#endregion Public Method			
	}
}