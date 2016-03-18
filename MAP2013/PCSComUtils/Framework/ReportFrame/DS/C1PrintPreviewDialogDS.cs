using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;

using System.Text.RegularExpressions;

using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;


namespace PCSComUtils.Framework.ReportFrame.DS
{
	
	public class C1PrintPreviewDialogDS
	{

		private const string THIS = "PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogDS";		
		private const string DEMILITER_CHAR = "#";

		public C1PrintPreviewDialogDS()
		{
		}
			
		#region ISSUE SLIP REPORT. THACHNN

		//**************************************************************************              
		///    <Description>
		///       Get CategoryCode From Line And WorkOrderNo
		///    </Description>
		///    <Inputs>
		///        Line
		///    </Inputs>		
		///    <Inputs>
		///        WOrk Order No
		///    </Inputs>		
		///    <Returns>
		///       string Category Code
		///    </Returns>
		///    <Authors>
		///       Thachnn
		///    </Authors>
		///    <History>
		///       28-10-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public string GetCategoryCodeFromLineAndWorkOrderNo(int pnLine,string pstrWONo)
		{
			//TODO:L Kill Injection here
			const string METHOD_NAME = THIS + ".GetCategoryCodeFromLineAndWorkOrderNo()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	" select ITM_Category.Code CategoryCode " +
				"from PRO_WorkOrderDetail, ITM_Product, ITM_Category, PRO_WorkOrderMaster " +
				" where ITM_Category.CategoryID = ITM_Product.CategoryID " +
				" And ITM_Product.ProductID = PRO_WorkOrderDetail.ProductID " +
				" And PRO_WorkOrderMaster.WorkOrderMasterID = PRO_WorkOrderDetail.WorkOrderMasterID " +
				" And PRO_WorkOrderDetail.Line = " + pnLine + 
				" And PRO_WorkOrderMaster.WorkOrderNo = '" + pstrWONo +  "' ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString().Trim();
				else
					return string.Empty;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}


		//**************************************************************************              
		///    <Description>
		///       Get CategoryName From Line And WorkOrderNo
		///    </Description>
		///    <Inputs>
		///        Line
		///    </Inputs>		
		///    <Inputs>
		///        WOrk Order No
		///    </Inputs>		
		///    <Returns>
		///       string Category Name
		///    </Returns>
		///    <Authors>
		///       Thachnn
		///    </Authors>
		///    <History>
		///       28-10-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public string GetCategoryNameFromLineAndWorkOrderNo(int pnLine,string pstrWONo)
		{
			//TODO:L Kill Injection here
			const string METHOD_NAME = THIS + ".GetCategoryNameFromLineAndWorkOrderNo()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	" select ITM_Category.Name CategoryName " +
					"from PRO_WorkOrderDetail, ITM_Product, ITM_Category, PRO_WorkOrderMaster " +
					" where ITM_Category.CategoryID = ITM_Product.CategoryID " +
					" And ITM_Product.ProductID = PRO_WorkOrderDetail.ProductID " +
					" And PRO_WorkOrderMaster.WorkOrderMasterID = PRO_WorkOrderDetail.WorkOrderMasterID " +
					" And PRO_WorkOrderDetail.Line = " + pnLine + 
					" And PRO_WorkOrderMaster.WorkOrderNo = '" + pstrWONo +  "' ";


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString().Trim();
				else
					return string.Empty;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		
		//**************************************************************************              
		///    <Description>
		///       Get Product Model From Line And WorkOrderNo
		///    </Description>
		///    <Inputs>
		///        Line
		///    </Inputs>		
		///    <Inputs>
		///        WOrk Order No
		///    </Inputs>		
		///    <Returns>
		///       string Model
		///    </Returns>
		///    <Authors>
		///       Thachnn
		///    </Authors>
		///    <History>
		///       28-10-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public string GetProductModelFromLineAndWorkOrderNo(int pnLine,string pstrWONo)
		{
			//TODO:L Kill Injection here
			const string METHOD_NAME = THIS + ".GetProductModelFromLineAndWorkOrderNo()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	" select ITM_Product.Revision Model " +
					"from PRO_WorkOrderDetail, ITM_Product, ITM_Category, PRO_WorkOrderMaster " +
					" where ITM_Category.CategoryID = ITM_Product.CategoryID " +
					" And ITM_Product.ProductID = PRO_WorkOrderDetail.ProductID " +
					" And PRO_WorkOrderMaster.WorkOrderMasterID = PRO_WorkOrderDetail.WorkOrderMasterID " +
					" And PRO_WorkOrderDetail.Line = " + pnLine + 
					" And PRO_WorkOrderMaster.WorkOrderNo = '" + pstrWONo +  "' ";


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString().Trim();
				else
					return string.Empty;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}


		#endregion
		
		#region INVENTORY STATUS REPORT. THACHNN

		/// <summary>
		/// Thachnn: 28/10/2005 - my bd
		/// Return data for Inventory Status Report
		/// </summary>
		/// <param name="pnCCNID"></param>
		/// <param name="pnMasterLocationID"></param>
		/// <param name="pnLocationID"></param>
		/// <param name="pnCategoryID"></param>
		/// <returns></returns>
		public DataTable GetInventoryStatusFromCCNMasLocLocationCategory(int pnCCNID, int pnMasterLocationID,int pnLocationID, int pnCategoryID)
		{
			const string METHOD_NAME = THIS + ".GetInventoryStatusFromCCNMasLocLocationCategory()";
			
			const string LOCATIONID_PARAM_MASK = "@@LocationID";
			const string CATEGORYID_PARAM_MASK = "@@CategoryID";

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{	
				string strSql =	
					"SELECT     ITM_Category.Code AS Category, ITM_Product.Code as [Part No.], ITM_Product.Description as [Part Name], ITM_Product.Revision as [Model], MST_UnitOfMeasure.Code AS [Stock UM], MST_Location.Code AS Location,  " + 
					"                      IV_LocationCache.OHQuantity AS [OH Qty], IV_LocationCache.CommitQuantity AS [Commit Qty],  " + 
					"                    isnull(IV_LocationCache.OHQuantity,0) - isnull(IV_LocationCache.CommitQuantity,0) AS [Available Qty], ITM_ProductType.Code AS Type, ITM_Source.Code AS Source,  " + 
					"                   ITM_Product.SafetyStock AS [Safety Stock], " + 
					"IV_LocationCache.Lot AS [Lot], " + 
					"[Warning] = case  " + 
					"		when isnull(IV_LocationCache.OHQuantity,0) - isnull(IV_LocationCache.CommitQuantity,0) - isnull(ITM_Product.SafetyStock,0) < 0 then '1' " + 
					"		when isnull(IV_LocationCache.OHQuantity,0) - isnull(IV_LocationCache.CommitQuantity,0) - isnull(ITM_Product.SafetyStock,0) > 0 then '0' " + 
					"	end " + 

					"FROM         	ITM_Product INNER JOIN " + 
					"		IV_LocationCache ON ITM_Product.ProductID = IV_LocationCache.ProductID INNER JOIN " + 
					"		MST_Location ON IV_LocationCache.LocationID = MST_Location.LocationID INNER JOIN " + 
					"		dbo.MST_MasterLocation ON dbo.MST_Location.MasterLocationID = dbo.MST_MasterLocation.MasterLocationID INNER JOIN " + 
					"		dbo.MST_CCN ON dbo.MST_MasterLocation.CCNID = dbo.MST_CCN.CCNID INNER JOIN " + 
					"		MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID LEFT OUTER JOIN " + 
					"		ITM_Source ON ITM_Product.SourceID = ITM_Source.SourceID LEFT OUTER JOIN " + 
					"		ITM_ProductType ON ITM_Product.ProductTypeID = ITM_ProductType.ProductTypeID LEFT OUTER JOIN " + 
					"		ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID " + 

					" WHERE  " + 					
					"	 	MST_Location.LocationID =  " + LOCATIONID_PARAM_MASK  +
					"  AND	ITM_Category.CategoryID =  " + CATEGORYID_PARAM_MASK  +
					"  AND	MST_MasterLocation.CCNID =  " + pnCCNID +
					"  AND 	MST_MasterLocation.MasterLocationID =   " + pnMasterLocationID +

					" ORDER BY ITM_Product.Description " 
					;


				
				#region    //Kill the empty parameter (if null)
				string strResult;
				if(pnLocationID == int.MinValue)
				{
					strResult = FindAndReplaceParameterRegEx(strSql,LOCATIONID_PARAM_MASK,null);
				}
				else
				{
					strResult = FindAndReplaceParameterRegEx(strSql,LOCATIONID_PARAM_MASK,pnLocationID.ToString());
				}
				strResult = Kill_SQL_BooleanOperator_AtLast(strResult);
				strResult = Kill_SQL_WHERE_AtLast(strResult);

				if(pnCategoryID == int.MinValue)
				{
					strResult = FindAndReplaceParameterRegEx(strResult,CATEGORYID_PARAM_MASK,null);
				}
				else
				{
					strResult = FindAndReplaceParameterRegEx(strResult,CATEGORYID_PARAM_MASK,pnCategoryID.ToString());
				}
				strResult = Kill_SQL_BooleanOperator_AtLast(strResult);
				strResult = Kill_SQL_WHERE_AtLast(strResult);
				#endregion


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strResult, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportFieldsTable.TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					return dstPCS.Tables[0];
				}
				else
				{
					return new DataTable();
				}
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		
		/// <summary>
		/// Thachnn: 07/01/2006
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetLocationCodeFromID(int pnID)
		{	
			string strSql=	" select MST_Location.Code from MST_Location where MST_Location.LocationID = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;			
		}


		/// <summary>
		/// Thachnn: 07/01/2006
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetLocationNameFromID(int pnID)
		{	
			string strSql=	" select MST_Location.Name from MST_Location where MST_Location.LocationID = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;			
		}



		
		/// <summary>
		/// Thachnn: 22/01/2006
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetBinCodeFromID(int pnID)
		{	
			string strSql=	" select MST_Bin.Code from MST_Bin where BinID = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;			
		}


		#endregion		
		
		#region CPO REPORT. THACHNN
		/// <summary>
		/// THACHNN: 08/10/2005
		/// Getting the data for CPO REport with MRP Plan Type
		/// </summary>
		/// <param name="pnCCNID"></param>
		/// <param name="pnMonth"></param>
		/// <param name="pnYear"></param>
		/// <param name="pnCycle"></param>
		/// <param name="pnProductionLineID"></param>
		/// <param name="pnMasterLocationID"></param>
		/// <param name="pnCategoryID"></param>
		/// <returns></returns>
		public DataTable GetMRP_CPOReportData(int pnCCNID, int pnMonth, int pnYear, int pnCycle, int pnProductionLineID,
			int pnMasterLocationID, int pnCategoryID, int pnVendorID, int pnProductID, string pstrModel)
		{
			const string METHOD_NAME = THIS + ".GetMRP_CPOReportData()";
			const string TABLE_NAME = "MRP CPO Report";

			//const string CCNID_PARAM_MASK = "@@CCNID";
			//const string YEAR_PARAM_MASK = "@@YearID";
			//const string MONTH_PARAM_MASK = "@@MonthID";
			//const string CYCLEID_PARAM_MASK = "@@CycleID";
			const string PRODUCTIONLINEID_PARAM_CLAUSE_MASK = "@@WHEREProductionLineID@@";
			const string MASTERLOCATIONID_PARAM_MASK = "@@MasterLocationID";
			const string CATEGORYID_PARAM_MASK = "@@CategoryID";
			const string VENDORID_PARAM_MASK = "@@VendorID";
			const string PRODUCTID_PARAM_MASK = "@@ProductID";
			const string MODEL_PARAM_MASK = "@@Model";

			string strProductionLineCondition = " MTR_CPO.ProductID IN  " + 
				" (select ITM_Product.ProductID " + 
				" from ITM_Product  " + 
				" join ITM_Routing " + 
				" 	on ITM_Routing.ProductID = ITM_Product.ProductID " + 
				" join MST_WorkCenter " + 
				" 	on MST_WorkCenter.WorkCenterID = ITM_Routing.WorkCenterID " + 
				" join PRO_ProductionLine " + 
				" 	on MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID " + 				
				" where  PRO_ProductionLine.ProductionLineID  =  " + pnProductionLineID + " ) AND ";

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{	
				string strSql = 
					" SELECT     TOP 100 PERCENT  " + 
					"  MST_CCN.Code AS CCN, " + 
					"  ITM_Category.Code AS Category, " + 
					"  ITM_Product.Code AS [Part No.],  " + 
					"  ITM_Product.Description AS [Part Name], " + 
					"  ITM_Product.Revision AS [Model], " + 
					
					"  DatePart(yyyy,MTR_CPO.DueDate) AS [Year], " + 
					"  DatePart(mm,MTR_CPO.DueDate) AS [Month], " + 
					"  DatePart(dd,MTR_CPO.DueDate) AS [Day], " + 

					"  Min(MTR_CPO.DueDate) AS [DueDate], " + 

					"  MST_UnitOfMeasure.Code AS [UM], " + 
					"  SUM(MTR_CPO.Quantity) AS [Quantity] " + 
					"   " + 
					" FROM          " + 
					" MTR_CPO  " + 
					" INNER JOIN MST_CCN  " + 
					" 	ON MTR_CPO.CCNID = MST_CCN.CCNID  " + 
					" INNER JOIN ITM_Product  " + 
					" 	ON MTR_CPO.ProductID = ITM_Product.ProductID  " + 
					" LEFT OUTER JOIN ITM_Category  " + 
					" 	ON ITM_Product.CategoryID = ITM_Category.CategoryID  " + 
					" INNER JOIN MST_UnitOfMeasure  " + 
					" 	ON MTR_CPO.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID " + 
 
					" WHERE      " + 
					" (MTR_CPO.CCNID = " + pnCCNID + ") AND  " + 
					" (MTR_CPO.MRPCycleOptionMasterID = " + pnCycle + ") AND  " + 
 
					PRODUCTIONLINEID_PARAM_CLAUSE_MASK +					

					" MTR_CPO.MasterLocationID = " + MASTERLOCATIONID_PARAM_MASK + 
					" AND ITM_Product.CategoryID = " + CATEGORYID_PARAM_MASK + "   " + 
					" AND ITM_Product.PrimaryVendorID = " + VENDORID_PARAM_MASK + "   " + 
					" AND ITM_Product.ProductID = " + PRODUCTID_PARAM_MASK + "   " + 
					" AND ITM_Product.Revision = " + MODEL_PARAM_MASK + "   " + 
					" AND DATEPART(yyyy, MTR_CPO.DueDate) = " + pnYear + " AND  " + 
					" DATEPART(mm, MTR_CPO.DueDate) = " + pnMonth + " " + 
					"  " + 
					" GROUP BY  " + 
					"  MST_CCN.Code, " + 
					"  ITM_Category.Code, " + 
					"  ITM_Product.Code, " + 
					"  ITM_Product.Description, " + 
					"  ITM_Product.Revision, " + 
					"   DatePart(yyyy,MTR_CPO.DueDate), "  + 
					"   DatePart(mm,MTR_CPO.DueDate),  "  +
					"   DATEPART(dd, MTR_CPO.DueDate),  "  +		
					"  MST_UnitOfMeasure.Code " + 
					"  " + 
					" ORDER BY 	[DueDate], [Category],  [Part No.], [Part Name]";

				
				#region    //Kill the empty parameter (if null)
				string strResult;
				if(pnProductionLineID == int.MinValue)
					strResult = Regex.Replace(strSql,PRODUCTIONLINEID_PARAM_CLAUSE_MASK," ",RegexOptions.IgnoreCase);
				else
					strResult = Regex.Replace(strSql,PRODUCTIONLINEID_PARAM_CLAUSE_MASK,strProductionLineCondition,RegexOptions.IgnoreCase);
				if(pnMasterLocationID == int.MinValue)
					strResult = FindAndReplaceParameterRegEx(strResult,MASTERLOCATIONID_PARAM_MASK,null);
				else
					strResult = FindAndReplaceParameterRegEx(strResult,MASTERLOCATIONID_PARAM_MASK,pnMasterLocationID.ToString());
				if(pnCategoryID == int.MinValue)
					strResult = FindAndReplaceParameterRegEx(strResult,CATEGORYID_PARAM_MASK,null);
				else
					strResult = FindAndReplaceParameterRegEx(strResult,CATEGORYID_PARAM_MASK,pnCategoryID.ToString());
				if(pnVendorID == int.MinValue)
					strResult = FindAndReplaceParameterRegEx(strResult,VENDORID_PARAM_MASK,null);
				else
					strResult = FindAndReplaceParameterRegEx(strResult,VENDORID_PARAM_MASK,pnVendorID.ToString());
				if(pnProductID == int.MinValue)
					strResult = FindAndReplaceParameterRegEx(strResult,PRODUCTID_PARAM_MASK,null);
				else
					strResult = FindAndReplaceParameterRegEx(strResult,PRODUCTID_PARAM_MASK,pnProductID.ToString());
				if(pstrModel == string.Empty)
					strResult = FindAndReplaceParameterRegEx(strResult,MODEL_PARAM_MASK,null);
				else
					strResult = FindAndReplaceParameterRegEx(strResult,MODEL_PARAM_MASK,pstrModel.ToString());
				#endregion


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strResult, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
					return dstPCS.Tables[0];
				else
					return new DataTable();
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}			
		}


		/// <summary>
		/// THACHNN: 08/10/2005
		/// Getting the data for CPO REport with plan type = MPS
		/// </summary>
		/// <param name="pnCCNID"></param>
		/// <param name="pnMonth"></param>
		/// <param name="pnYear"></param>
		/// <param name="pnCycle"></param>
		/// <param name="pnProductionLineID"></param>
		/// <param name="pnMasterLocationID"></param>
		/// <param name="pnCategoryID"></param>
		/// <returns></returns>
		public DataTable GetMPS_CPOReportData(int pnCCNID, int pnMonth, int pnYear, int pnCycle, int pnProductionLineID,
			int pnMasterLocationID, int pnCategoryID, int pnVendorID, int pnProductID, string pstrModel)
		{
			const string METHOD_NAME = THIS + ".GetMPS_CPOReportData()";
			const string TABLE_NAME = "MPS CPO Report";

			//const string CCNID_PARAM_MASK = "@@CCNID";
			//const string YEAR_PARAM_MASK = "@@YearID";
			//const string MONTH_PARAM_MASK = "@@MonthID";
			//const string CYCLEID_PARAM_MASK = "@@CycleID";
			const string PRODUCTIONLINEID_PARAM_CLAUSE_MASK = "@@WHEREProductionLineID@@";
			const string MASTERLOCATIONID_PARAM_MASK = "@@MasterLocationID";
			const string CATEGORYID_PARAM_MASK = "@@CategoryID";
			const string VENDORID_PARAM_MASK = "@@VendorID";
			const string PRODUCTID_PARAM_MASK = "@@ProductID";
			const string MODEL_PARAM_MASK = "@@Model";

			string strProductionLineCondition = " MTR_CPO.ProductID IN  " + 
				" (select ITM_Product.ProductID " + 
				" from ITM_Product  " + 
				" join ITM_Routing " + 
				" 	on ITM_Routing.ProductID = ITM_Product.ProductID " + 
				" join MST_WorkCenter " + 
				" 	on MST_WorkCenter.WorkCenterID = ITM_Routing.WorkCenterID " + 
				" join PRO_ProductionLine " + 
				" 	on MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID " + 				
				" where  PRO_ProductionLine.ProductionLineID  =  " + pnProductionLineID + " ) AND ";

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{	
				string strSql = 
					" SELECT      " + 
					"  MST_CCN.Code AS CCN, " + 
					"  ITM_Category.Code AS Category, " + 
					"  ITM_Product.Code AS [Part No.],  " + 
					"  ITM_Product.Description AS [Part Name], " + 
					"  ITM_Product.Revision AS [Model], " + 

					"  DatePart(yyyy,MTR_CPO.DueDate) AS [Year], " + 
					"  DatePart(mm,MTR_CPO.DueDate) AS [Month], " + 
					"  DatePart(dd,MTR_CPO.DueDate) AS [Day], " + 

					"  Min(MTR_CPO.DueDate) AS [DueDate], " + 
					"  MST_UnitOfMeasure.Code AS [UM], " + 
					"  SUM(MTR_CPO.Quantity) AS [Quantity] " + 
					"   " + 
					" FROM          " + 
					" MTR_CPO  " + 
					" INNER JOIN MST_CCN  " + 
					" ON MTR_CPO.CCNID = MST_CCN.CCNID  " + 
					" INNER JOIN ITM_Product  " + 
					" ON MTR_CPO.ProductID = ITM_Product.ProductID  " + 
					" LEFT OUTER JOIN ITM_Category  " + 
					" ON ITM_Product.CategoryID = ITM_Category.CategoryID  " + 
					" INNER JOIN MST_UnitOfMeasure  " + 
					" ON MTR_CPO.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID " + 
 
					" WHERE      " + 
					" (MTR_CPO.CCNID = " + pnCCNID + ") AND  " + 
					" (MTR_CPO.MPSCycleOptionMasterID = " + pnCycle + ") AND  " + 
 
					PRODUCTIONLINEID_PARAM_CLAUSE_MASK +					

					"     MTR_CPO.MasterLocationID = " + MASTERLOCATIONID_PARAM_MASK + "  " + 
					" AND ITM_Product.CategoryID = " + CATEGORYID_PARAM_MASK + "   " + 
					" AND ITM_Product.PrimaryVendorID = " + VENDORID_PARAM_MASK + "   " + 
					" AND ITM_Product.ProductID = " + PRODUCTID_PARAM_MASK + "   " + 
					" AND ITM_Product.Revision = " + MODEL_PARAM_MASK + "   " + 
					" AND DATEPART(yyyy, MTR_CPO.DueDate) = " + pnYear + "  " + 
					" AND DATEPART(mm, MTR_CPO.DueDate) = " + pnMonth + " " + 
					"  " + 
					" GROUP BY  " + 
					"  MST_CCN.Code, " + 
					"  ITM_Category.Code, " + 
					"  ITM_Product.Code, " + 
					"  ITM_Product.Description, " + 
					"  ITM_Product.Revision, " + 
					"   DatePart(yyyy,MTR_CPO.DueDate), "  + 
					"   DatePart(mm,MTR_CPO.DueDate),  "  +
					"   DATEPART(dd, MTR_CPO.DueDate),  "  +					
					"  MST_UnitOfMeasure.Code " + 
					"  " + 
					" ORDER BY 	[DueDate], [Category],  [Part No.], [Part Name]";

				
				#region    //Kill the empty parameter (if null)
				string strResult;
				if(pnProductionLineID == int.MinValue)
				{
					strResult = Regex.Replace(strSql,PRODUCTIONLINEID_PARAM_CLAUSE_MASK," ",RegexOptions.IgnoreCase);
				}
				else
				{
					strResult = Regex.Replace(strSql,PRODUCTIONLINEID_PARAM_CLAUSE_MASK,strProductionLineCondition,RegexOptions.IgnoreCase);
				}
				if(pnMasterLocationID == int.MinValue)
				{
					strResult = FindAndReplaceParameterRegEx(strResult,MASTERLOCATIONID_PARAM_MASK,null);
				}
				else
				{
					strResult = FindAndReplaceParameterRegEx(strResult,MASTERLOCATIONID_PARAM_MASK,pnMasterLocationID.ToString());
				}
				if(pnCategoryID == int.MinValue)
				{
					strResult = FindAndReplaceParameterRegEx(strResult,CATEGORYID_PARAM_MASK,null);
				}
				else
				{
					strResult = FindAndReplaceParameterRegEx(strResult,CATEGORYID_PARAM_MASK,pnCategoryID.ToString());
				}		
				if(pnVendorID == int.MinValue)
					strResult = FindAndReplaceParameterRegEx(strResult,VENDORID_PARAM_MASK,null);
				else
					strResult = FindAndReplaceParameterRegEx(strResult,VENDORID_PARAM_MASK,pnVendorID.ToString());
				if(pnProductID == int.MinValue)
					strResult = FindAndReplaceParameterRegEx(strResult,PRODUCTID_PARAM_MASK,null);
				else
					strResult = FindAndReplaceParameterRegEx(strResult,PRODUCTID_PARAM_MASK,pnProductID.ToString());
				if(pstrModel == string.Empty)
					strResult = FindAndReplaceParameterRegEx(strResult,MODEL_PARAM_MASK,null);
				else
					strResult = FindAndReplaceParameterRegEx(strResult,MODEL_PARAM_MASK,pstrModel.ToString());
				#endregion


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strResult, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
					return dstPCS.Tables[0];
				else
					return new DataTable();
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}			
		}


		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetMRPCycleFromID(int pnID)
		{
			
			const string METHOD_NAME = THIS + ".GetMRPCycleFromID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql = "select Cycle from mtr_mRpCycleOptionMaster where mRpcycleOptionMasterID = " + pnID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString().Trim();
				else
					return string.Empty;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}                      			

		}
		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetMPSCycleFromID(int pnID)
		{
			
			const string METHOD_NAME = THIS + ".GetMPSCycleFromID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql = "select Cycle from mtr_mpsCycleOptionMaster where mpscycleOptionMasterID = " + pnID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString().Trim();
				else
					return string.Empty;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}                      			

		}
		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetMRPCycleDescriptionFromID(int pnID)
		{			
			string strSql = "select Description from mtr_mrpCycleOptionMaster where mrpCycleOptionMasterID = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;		
		}
		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetMPSCycleDescriptionFromID(int pnID)
		{			
			const string METHOD_NAME = THIS + ".GetMPSCycleDescriptionFromID()";
			string strSql = "select Description from mtr_mpsCycleOptionMaster where mpsCycleOptionMasterID = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;		
			
		}
		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetProductLineCodeFromID(int pnID)
		{
			const string METHOD_NAME = THIS + ".GetProductLineCodeFromID()";		
		
			string strSql = " select Code from pro_productionLine where productionlineid = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;
		}

		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetProductLineNameFromID(int pnID)
		{
			const string METHOD_NAME = THIS + ".GetProductLineNameFromID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql = " select name from pro_productionLine where productionlineid = " + pnID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString().Trim();
				else
					return string.Empty;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}                      		
			
		}

		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetMasterLocationCodeFromID(int pnID)
		{	
			string strSql =	" select COde from mst_masterlocation where masterlocationid = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;					
		}
		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetMasterLocationNameFromID(int pnID)
		{
			const string METHOD_NAME = THIS + ".GetMasterLocationNameFromID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql =	" select name from mst_masterlocation where masterlocationid = " + pnID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString().Trim();
				else
					return string.Empty;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}                        
		}

		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetCategoryCodeFromID(int pnID)
		{	
			string strSql=	" select ITM_Category.Code from ITM_Category where ITM_Category.CategoryID = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;		
			
		}
		public string GetCategoryCodeFromID(string pstrID)
		{	
			string strSql=	" select ITM_Category.Code from ITM_Category where ITM_Category.CategoryID IN (" + pstrID + ")";
			DataTable dtbData = ExecuteQuery(strSql);
			string strCode = string.Empty;
			foreach (DataRow drowData in dtbData.Rows)
				strCode += drowData["Code"].ToString() + ",";
			if (strCode.Length > 0)
				strCode = strCode.Substring(0, strCode.Length - 1);
			return strCode;
		}

		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetCategoryNameFromID(int pnID)
		{			
			const string METHOD_NAME = THIS + ".GetCategoryNameFromID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	" select ITM_Category.Name from ITM_Category where ITM_Category.CategoryID = " + pnID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString().Trim();
				else
					return string.Empty;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		public string GetVendorInfo(int pnID)
		{			
			const string METHOD_NAME = THIS + ".GetVendorInfo()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"select Code + ' (' + Name + ')' from MST_Party where PartyID = " + pnID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString().Trim();
				else
					return string.Empty;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		public string GetItemInfor(int pnID)
		{			
			const string METHOD_NAME = THIS + ".GetItemInfor()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"select Code + ' (' + Description + ')' from ITM_Product where ProductID = " + pnID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString().Trim();
				else
					return string.Empty;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}


		#endregion

		#region SCHEDULE OF LOCAL PARTS IN MONTH REPORT. THACHNN

		/// <summary>
		/// THACHNN: 11/10/2005
		/// Getting the data for Schedule of local parts in month REport
		/// </summary>		
		/// <returns></returns>
		public DataTable GetScheduleOfLocalPartsInMonthReportData(int pnCCNID, int pnMonth, int pnYear, int pnVendorID,  int pnCategoryID, int pnProductID)
		{
			// TODO:
			const string METHOD_NAME = THIS + ".GetScheduleOfLocalPartsInMonthReportData()";
			const string TABLE_NAME = "ScheduleOfLocalPartsInMonth Report";

			//const string CCNID_PARAM_MASK = "@@CCNID";
			//const string YEAR_PARAM_MASK = "@@YearID";
			//const string MONTH_PARAM_MASK = "@@MonthID";			
			const string VENDORID_PARAM_MASK = "@@VendorID";						
			const string CATEGORYID_PARAM_MASK = "@@CategoryID";
			const string PRODUCTID_PARAM_MASK = "@@ProductID";			

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{	
				//string strSql = string.Empty;
				string strSql = 
					" SELECT    " + 
					" V_LocalVendor.Code as [Vendor], " + 
					" ITM_Product.Code AS [Part No.],   " + 
					" ITM_Product.Description AS [Part Name],  " + 
					" ITM_Category.Code AS [Category],  " + 
					" ITM_Product.QuantitySet AS [QuantitySet],  " + 
					" ITM_Product.Revision AS [Model],  " + 
					" MST_UnitOfMeasure.Code AS [UM],  " + 
					"  DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate) as [Year], " + 
					"  DATEPART(mm, PO_DeliverySchedule.ScheduleDate)as [Month], " + 
					"  DATEPART(dd, PO_DeliverySchedule.ScheduleDate)as [Day], " + 
					" Min(PO_DeliverySchedule.ScheduleDate) as [ScheduleDate], " + 
					" SUM(PO_DeliverySchedule.DeliveryQuantity) AS [Quantity]  " + 
					"  " + 

					" FROM        " + 
					" PO_DeliverySchedule INNER JOIN PO_PurchaseOrderDetail " + 
					"   on PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID " + 
					" INNER JOIN ITM_Product " + 
					"   on PO_PurchaseOrderDetail.ProductID = ITM_Product.ProductID " + 
					" INNER JOIN MST_UnitOfMeasure  " + 
					"   ON PO_PurchaseOrderDetail.BuyingUMID = MST_UnitOfMeasure.UnitOfMeasureID  " + 
					" LEFT OUTER JOIN ITM_Category " + 
					"   ON ITM_Product.CategoryID = ITM_Category.CategoryID " + 
					" INNER JOIN PO_PurchaseOrderMaster " + 
					"   ON PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID " + 
					" INNER JOIN MST_CCN  " + 
					"   ON PO_PurchaseOrderMaster.CCNID = MST_CCN.CCNID   " + 
					" INNER JOIN V_LocalVendor  " + 
					"   ON V_LocalVendor.PartyID = PO_PurchaseOrderMaster.PartyID " + 
					"  " + 

					" WHERE     " + 
					" (MST_CCN.CCNID = " +pnCCNID+ ") AND   " + 					
					" V_LocalVendor.PartyID = " +VENDORID_PARAM_MASK+ " AND  " + 
					" ITM_Category.CategoryID = " +CATEGORYID_PARAM_MASK+ " AND  " + 
					" ITM_Product.ProductID = " +PRODUCTID_PARAM_MASK+ "  AND  " + 
					" DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate) = " +pnYear.ToString("0000")+ " AND  " + 
					" DATEPART(mm, PO_DeliverySchedule.ScheduleDate) = " +pnMonth.ToString("00")+ "   " + 

					"  " + 
					"   GROUP BY    " + 
					" MST_CCN.Code,  " + 
					" V_LocalVendor.Code , " + 
					" ITM_Product.Code,   " + 
					" ITM_Product.Description,  " + 
					" ITM_Category.Code,  " + 
					" ITM_Product.QuantitySet, " + 
					" ITM_Product.Revision,  " + 
					" MST_UnitOfMeasure.Code ,  " + 
					" DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate), " + 
					"  DATEPART(mm, PO_DeliverySchedule.ScheduleDate), " + 
					"  DATEPART(dd, PO_DeliverySchedule.ScheduleDate) " + 
					"  " + 

					" ORDER BY " + 
					" [ScheduleDate], " + 
					" [Vendor], " + 
					" [Category],   " + 
					" [Part No.], " + 
					" [Part Name], " + 
					" [Model] " +  "  ";
				//"  COMPUTE Sum(SUM(PO_DeliverySchedule.DeliveryQuantity)) " ;

				
				#region    //Kill the empty parameter (if null)
				string strResult = strSql;
				if(pnProductID == int.MinValue)
				{
					strResult = FindAndReplaceParameterRegEx(strResult,PRODUCTID_PARAM_MASK,null);
				}
				else
				{
					strResult = FindAndReplaceParameterRegEx(strResult,PRODUCTID_PARAM_MASK,pnProductID.ToString());
				}
				
				if(pnVendorID == int.MinValue)
				{
					strResult = FindAndReplaceParameterRegEx(strResult,VENDORID_PARAM_MASK,null);
				}
				else
				{
					strResult = FindAndReplaceParameterRegEx(strResult,VENDORID_PARAM_MASK,pnVendorID.ToString());
				}

				if(pnCategoryID == int.MinValue)
				{
					strResult = FindAndReplaceParameterRegEx(strResult,CATEGORYID_PARAM_MASK,null);
				}
				else
				{
					strResult = FindAndReplaceParameterRegEx(strResult,CATEGORYID_PARAM_MASK,pnCategoryID.ToString());
				}				
				#endregion


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strResult, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					return dstPCS.Tables[0].Copy();
				}
				else
				{
					return new DataTable();
				}
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}			
		}


		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetVendorCodeFromID(int pnID)
		{	
			string strSql=	" select MST_PARTY.Code from MST_PARTY where MST_PARTY.PartyID = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;		
			
		}


		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetVendorNameFromID(int pnID)
		{	
			string strSql=	" select MST_PARTY.Name from MST_PARTY where MST_PARTY.PartyID = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;		
			
		}



		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetProductCodeFromID(int pnID)
		{	
			string strSql=	" select ITM_PRODUCT.Code from ITM_PRODUCT where ITM_PRODUCT.ProductID = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;
		}


		/// <summary>
		/// Thachnn: 10/11/2005
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>
		public string GetProductNameFromID(int pnID)
		{	
			string strSql=	" select ITM_PRODUCT.Description from ITM_PRODUCT where ITM_PRODUCT.ProductID = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;		
			
		}




		#endregion
				
		#region PART ORDER SHEET REPORT
		/// <summary>
		/// THACHNN: 11/10/2005
		/// Getting the data for Part Order Sheet Report
		/// </summary>		
		/// <returns></returns>
		public DataSet GetPartOrderSheetReportData(int pnCCNID, int pnPartyID, string pstrPurchaseOrderMasterID)
		{
			// TODO:
			const string METHOD_NAME = THIS + ".GetPartOrderSheetReportData()";
			const string TABLE_NAME = "PartOrderSheet Report";			

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{	
				//string strSql = string.Empty;
				string strSql = 
					
					" Declare @pstrPartyID int " + 
					" Declare @pstrCCNID int " + 
					" Declare @pstrPurchaseOrderMasterID varchar(12) " + 
					"  " + 
					" /*-----------------------------------*/ " + 
					" Set @pstrPartyID = " +pnPartyID+ " " + 
					" Set @pstrCCNID = " +pnCCNID+ " " + 
					" Set @pstrPurchaseOrderMasterID = " +pstrPurchaseOrderMasterID+ " " + 
					"  " + 
					" /*-----------------------------------*/ " + 
					" SELECT " + 
					" ITM_Product.ProductID as [ProductID],    " + 
					" ITM_Product.Code AS [PartNo],   " + 
					" ITM_Product.Description AS [PartName],  " + 
					" DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate) as [Year], " + 
					" DATEPART(mm, PO_DeliverySchedule.ScheduleDate)as [Month], " + 
					" DATEPART(dd, PO_DeliverySchedule.ScheduleDate)as [Day], " + 
					" Min(PO_DeliverySchedule.ScheduleDate) as [ScheduleDate],  " + 
					" SUM(IsNull(PO_DeliverySchedule.DeliveryQuantity, 0.00) ) AS [Quantity] , " + 
					" SUM(IsNull(PO_DeliverySchedule.Adjustment, 0.00) ) AS [Adjustment] " + 
					"  " + 
					" FROM        " + 
					" PO_DeliverySchedule INNER JOIN PO_PurchaseOrderDetail " + 
					" 	 on PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID " + 
					" INNER JOIN ITM_Product " + 
					" 	on PO_PurchaseOrderDetail.ProductID = ITM_Product.ProductID " + 
					" INNER JOIN PO_PurchaseOrderMaster " + 
					" 	on PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID " + 
					"   INNER JOIN MST_CCN  " + 
					" 	ON PO_PurchaseOrderMaster.CCNID = MST_CCN.CCNID   " + 
					" WHERE     " + 
					" MST_CCN.CCNID = @pstrCCNID   " + 
					" AND PO_PurchaseOrderMaster.PartyID = @pstrPartyID " + 
					" AND PO_PurchaseOrderMaster.PurchaseOrderMasterID = @pstrPurchaseOrderMasterID " + 
					"  " + 
					" GROUP BY    " + 
					" MST_CCN.Code,  " + 
					" ITM_Product.ProductID, " + 
					" ITM_Product.Code,   " + 
					" ITM_Product.Description,  " + 
					" DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate), " + 
					" DATEPART(mm, PO_DeliverySchedule.ScheduleDate), " + 
					" DATEPART(dd, PO_DeliverySchedule.ScheduleDate) " + 
					"  " + 
					" ORDER BY " + 
					" [ScheduleDate], " + 
					" [PartNo], " + 
					" [PartName] " + 
					"  " +
					
/*----------------GROUP BY AND SUM ALL TABLE-------------------*/
 " SELECT " + 
 " PO_PurchaseOrderDetail.ProductID as [ProductID],    " + 
 " DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate) as [Year], " + 
 " DATEPART(mm, PO_DeliverySchedule.ScheduleDate)as [Month], " + 
 " SUM(IsNull(PO_DeliverySchedule.DeliveryQuantity, 0.00) ) AS [Quantity] , " + 
 " SUM(IsNull(PO_DeliverySchedule.Adjustment, 0.00) ) AS [Adjustment] " + 
 "  " + 
 " FROM        " + 
 " PO_DeliverySchedule INNER JOIN PO_PurchaseOrderDetail " + 
 " 	 on PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID " + 
 " INNER JOIN PO_PurchaseOrderMaster " + 
 " 	on PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID " + 
 " WHERE     " + 
 " PO_PurchaseOrderMaster.CCNID = @pstrCCNID   " + 
 " AND PO_PurchaseOrderMaster.PartyID = @pstrPartyID " + 
 " AND PO_PurchaseOrderMaster.PurchaseOrderMasterID = @pstrPurchaseOrderMasterID " + 
 "  " + 
 " GROUP BY    " + 
 " PO_PurchaseOrderDetail.ProductID , " + 
 " DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate), " + 
 " DATEPART(mm, PO_DeliverySchedule.ScheduleDate) " + 
 "  " 	;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);
				
				return dstPCS;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}			
		}

		#endregion

		#region PART ORDER SHEET MULTI VENDOR REPORT
		/// <summary>
		/// THACHNN: 11/10/2005
		/// Getting the data for Part Order Sheet Multi Vendor Report
		/// </summary>		
		/// <returns></returns>
		public DataTable GetPartOrderSheetMultiVendorReportData(int pnCCNID, int pnMonth, int pnYear, string pstrVendorID_List)
		{
			// TODO:
			const string METHOD_NAME = THIS + ".GetPartOrderSheetMultiVendorReportData()";
			const string TABLE_NAME = "PartOrderSheet Report";

			//const string CCNID_PARAM_MASK = "@@CCNID";
			//const string YEAR_PARAM_MASK = "@@YearID";
			//const string MONTH_PARAM_MASK = "@@MonthID";			
			//const string VENDORID_PARAM_MASK = "@@VendorID";						
			//const string CATEGORYID_PARAM_MASK = "@@CategoryID";
			//const string PRODUCTID_PARAM_MASK = "@@ProductID";			

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{	
				//string strSql = string.Empty;
				string strSql =

 " Declare @pstrYear char(4) " + 
 " Declare @pstrMonth char(2) " + 
 " Declare @pstrCCNID int " + 
 " /*-----------------------------------*/ " + 
 " Set @pstrYear =   '" +pnYear.ToString("0000")+ "' " + 
 " Set @pstrMonth = '" +pnMonth.ToString("00")+ "' " + 
 " Set @pstrCCNID = " +pnCCNID+ " " + 
 " /*-----------------------------------*/ " + 
 "  " + 
 
 " SELECT " + 
 " PO_PurchaseOrderMaster.PORevision as [Revision], " + 
 " MST_Party.PartyID as [PartyID], " +
 " MST_Party.Code + ': ' + MST_Party.Name as [Vendor], " + 
 " ITM_Product.Code AS [Part No.],   " + 
 " ITM_Product.Description AS [Part Name],  " + 
 " ITM_Product.Revision AS [Model], " + 
 " DATEPART(dd, PO_DeliverySchedule.ScheduleDate)as [Day], " + 
 " Min(PO_DeliverySchedule.ScheduleDate) as [ScheduleDate], " + 
 " SUM(PO_DeliverySchedule.DeliveryQuantity) AS [Quantity] , " + 
 " SUM(PO_DeliverySchedule.Adjustment) AS [Adjustment] " + 
 "  " + 
 " FROM        " + 
 " PO_DeliverySchedule  " + 
 " INNER JOIN PO_PurchaseOrderDetail " + 
 " 	 on PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID " + 
 " INNER JOIN ITM_Product " + 
 " 	on PO_PurchaseOrderDetail.ProductID = ITM_Product.ProductID " + 
 " INNER JOIN PO_PurchaseOrderMaster " + 
 " 	on PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID " + 
 " INNER JOIN MST_CCN  " + 
 " 	ON PO_PurchaseOrderMaster.CCNID = MST_CCN.CCNID   " + 
 " INNER JOIN MST_Party " + 
 " 	ON PO_PurchaseOrderMaster.PartyID = MST_Party.PartyID " + 
 " WHERE     " + 
 " MST_CCN.CCNID = @pstrCCNID   " + 
 " AND DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate) = @pstrYear  " + 
 " AND DATEPART(mm, PO_DeliverySchedule.ScheduleDate) = @pstrMonth " + 
 (pstrVendorID_List.Trim() == string.Empty ? (string.Empty)	: (" AND PO_PurchaseOrderMaster.PartyID IN (" +pstrVendorID_List+ ") ") ) + 
 "  " + 
 " GROUP BY    " + 
 " PO_PurchaseOrderMaster.PORevision , " + 
 " MST_Party.PartyID, " +
 " MST_Party.Code, " + 
 " MST_Party.Name, " + 
 " MST_CCN.Code,  " + 
 " ITM_Product.Code,   " + 
 " ITM_Product.Description,  " + 
 " ITM_Product.Revision , " + 
 " DATEPART(dd  , PO_DeliverySchedule.ScheduleDate) " + 
 "  " + 
 " ORDER BY " + 
 " [Vendor], " + 
 " [Part No.], " + 
 " [Part Name], " + 
 " [Model] " +
 "  " 					;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					return dstPCS.Tables[0].Copy();
				}
				else
				{
					return new DataTable();
				}
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}			
		}

		#endregion

		/// UNDONE: waiting for Mr.Cuong Usecase
		#region MONTHLY DELIVERY REPORT. THACHNN

		/// <summary>
		/// THACHNN: 11/10/2005
		/// Getting the data for Schedule of local parts in month REport
		/// </summary>		
		/// <returns></returns>
		public DataTable GetMonthlyDeliveryReportData(int pnCCNID, int pnMonth, int pnYear, int pnCustomerID,  int pnCategoryID, int pnProductID)
		{
			// TODO:
			const string METHOD_NAME = THIS + ".GetScheduleOfLocalPartsInMonthReportData()";
			const string TABLE_NAME = "ScheduleOfLocalPartsInMonth Report";

			//const string CCNID_PARAM_MASK = "@@CCNID";
			//const string YEAR_PARAM_MASK = "@@YearID";
			//const string MONTH_PARAM_MASK = "@@MonthID";			
			const string CUSTOMERID_PARAM_MASK = "@@CustomerID";
			const string CATEGORYID_PARAM_MASK = "@@CategoryID";
			const string PRODUCTID_PARAM_MASK = "@@ProductID";			

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{	
				//string strSql = string.Empty;
				string strSql = 
					" SELECT    " + 
					" MST_Party.Code as [Vendor], " + 
					" ITM_Product.Code AS [Part No.],   " + 
					" ITM_Product.Description AS [Part Name],  " + 
					" ITM_Category.Code AS [Category],  " + 
					" ITM_Product.QuantitySet AS [QuantitySet],  " + 
					" ITM_Product.Revision AS [Model],  " + 
					" MST_UnitOfMeasure.Code AS [UM],  " + 
					"  DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate) as [Year], " + 
					"  DATEPART(mm, PO_DeliverySchedule.ScheduleDate)as [Month], " + 
					"  DATEPART(dd, PO_DeliverySchedule.ScheduleDate)as [Day], " + 
					" Min(PO_DeliverySchedule.ScheduleDate) as [ScheduleDate], " + 
					" SUM(PO_DeliverySchedule.DeliveryQuantity) AS [Quantity]  " + 
					"  " + 

					" FROM        " + 
					" PO_DeliverySchedule INNER JOIN PO_PurchaseOrderDetail " + 
					"   on PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID " + 
					" INNER JOIN ITM_Product " + 
					"   on PO_PurchaseOrderDetail.ProductID = ITM_Product.ProductID " + 
					" INNER JOIN MST_UnitOfMeasure  " + 
					"   ON PO_PurchaseOrderDetail.BuyingUMID = MST_UnitOfMeasure.UnitOfMeasureID  " + 
					" LEFT OUTER JOIN ITM_Category " + 
					"   ON ITM_Product.CategoryID = ITM_Category.CategoryID " + 
					" INNER JOIN PO_PurchaseOrderMaster " + 
					"   ON PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID " + 
					" INNER JOIN MST_CCN  " + 
					"   ON PO_PurchaseOrderMaster.CCNID = MST_CCN.CCNID   " + 
					" INNER JOIN MST_Party  " + 
					"   ON MST_Party.PartyID = PO_PurchaseOrderMaster.PartyID " + 
					"  " + 

					" WHERE     " + 
					" (MST_CCN.CCNID = " +pnCCNID+ ") AND   " + 					
					" MST_Party.PartyID = " +CUSTOMERID_PARAM_MASK+ " AND  " + 
					" ITM_Category.CategoryID = " +CATEGORYID_PARAM_MASK+ " AND  " + 
					" ITM_Product.ProductID = " +PRODUCTID_PARAM_MASK+ "  AND  " + 
					" DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate) = " +pnYear+ " AND  " + 
					" DATEPART(mm, PO_DeliverySchedule.ScheduleDate) = " +pnMonth+ "   " + 

					"  " + 
					"   GROUP BY    " + 
					" MST_CCN.Code,  " + 
					" MST_Party.Code , " + 
					" ITM_Product.Code,   " + 
					" ITM_Product.Description,  " + 
					" ITM_Category.Code,  " + 
					" ITM_Product.QuantitySet, " + 
					" ITM_Product.Revision,  " + 
					" MST_UnitOfMeasure.Code ,  " + 
					" DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate), " + 
					"  DATEPART(mm, PO_DeliverySchedule.ScheduleDate), " + 
					"  DATEPART(dd, PO_DeliverySchedule.ScheduleDate) " + 
					"  " + 

					" ORDER BY " + 
					" [ScheduleDate], " + 
					" [Vendor], " + 
					" [Category],   " + 
					" [Part No.], " + 
					" [Part Name], " + 
					" [Model] " +  "  ";
				//"  COMPUTE Sum(SUM(PO_DeliverySchedule.DeliveryQuantity)) " ;

				
				#region    //Kill the empty parameter (if null)
				string strResult = strSql;
				if(pnProductID == int.MinValue)
				{
					strResult = FindAndReplaceParameterRegEx(strResult,PRODUCTID_PARAM_MASK,null);
				}
				else
				{
					strResult = FindAndReplaceParameterRegEx(strResult,PRODUCTID_PARAM_MASK,pnProductID.ToString());
				}
				
				if(pnCustomerID == int.MinValue)
				{
					strResult = FindAndReplaceParameterRegEx(strResult,CUSTOMERID_PARAM_MASK,null);
				}
				else
				{
					strResult = FindAndReplaceParameterRegEx(strResult,CUSTOMERID_PARAM_MASK,pnCustomerID.ToString());
				}

				if(pnCategoryID == int.MinValue)
				{
					strResult = FindAndReplaceParameterRegEx(strResult,CATEGORYID_PARAM_MASK,null);
				}
				else
				{
					strResult = FindAndReplaceParameterRegEx(strResult,CATEGORYID_PARAM_MASK,pnCategoryID.ToString());
				}				
				#endregion


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strResult, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					return dstPCS.Tables[0].Copy();
				}
				else
				{
					return new DataTable();
				}
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}			
		}


		#endregion		

		#region PRODUCTION LINE  REPORTS . THACHNN
		/// <summary>
		/// Thachnn 06/02/2006		
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>		
		public string GetWorkOrderMasterNoFromID(int pnID)
		{
			string strSql=	"select PRO_WorkOrderMaster.WorkOrderNo From PRO_WorkOrderMaster where workOrderMasterID = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;		
		}
		#endregion

		#region VENDOR DELIVERY ASSESSMENT - THACHNN
		
		/// <summary>
		/// Thachnn 10/03/2006
		/// </summary>
		/// <param name="pnID"></param>
		/// <returns></returns>				
		public string GetPurchaseOrderMasterCodeFromID(int pnID)
		{
			string strSql=	"select PO_PurchaseOrderMaster.Code from PO_PurchaseOrderMaster where PO_PurchaseOrderMaster.PurchaseOrderMasterID = " + pnID;
			object objResult = ExecuteScalar(strSql);
			if (objResult != null)
				return objResult.ToString().Trim();
			else
				return string.Empty;
		}
		#endregion VENDOR DELIVERY ASSESSMENT - THACHNN

		#region Execute SQL query
		/// <summary>
		/// Thachnn: 10/11/2005
		/// execute the input sql clause
		/// return the object result
		/// throw all exception to outside
		/// </summary>
		/// <param name="pstrSql">SQL clause to execute</param>
		/// <returns>object</returns>
		public object ExecuteScalar(string pstrSql)
		{
			
			const string METHOD_NAME = THIS + ".ExecuteScalar()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = pstrSql;				

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				return ocmdPCS.ExecuteScalar();				
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		/// <summary>
		/// Thachnn: 10/11/2005
		/// execute the input sql clause
		/// return the object result
		/// throw all exception to outside
		/// </summary>
		/// <param name="pstrSql">SQL clause to execute</param>
		/// <returns>object</returns>
		public DataTable ExecuteQuery(string pstrSql)
		{
			
			const string METHOD_NAME = THIS + ".ExecuteQuery()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = pstrSql;				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		/// <summary>
		/// Thachnn: 10/11/2005
		/// execute the input sql clause, non query clause
		/// return the int result
		/// throw all exception to outside
		/// </summary>
		/// <param name="pstrSql">Non Query SQL clause to execute (INSERT,UPDATE, CREATE ...)</param>
		/// <returns>int, numbers of affect rows</returns>
		public int ExecuteNonQuery(string pstrSql)
		{			
			const string METHOD_NAME = THIS + ".ExecuteNonQuery()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = pstrSql;				

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				return ocmdPCS.ExecuteNonQuery();
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		
		#endregion Execute SQL query

		#region Thachnn Util FUNCTIONS Replace Param with VALUE in string. Copy from PCSUtils.FormControlComponent Class
		
		/// <summary>
		/// Thachnn: 06 Oct 2005
		/// Find in the SQL clause		
		/// Replace SQL parameter (named pstrPara) with its value (named pstrValue)
		/// if pstr value is string.Empty or null then I remove the whole parameter (remove pattern: a=@a)
		/// if I can't find the pattern a=@a or something like that, I return the original SQL clause
		/// THROW: Exception if error
		/// </summary>
		/// <history>
		/// Thachnn: 20/10/2005: make this function can work with [TableName].[FieldName] syntax style
		/// </history>		
		/// <param name="pstrSQL">input SQL clause</param>
		/// <param name="pstrPara">parameter name (like @something)</param>
		/// <param name="pstrValue">parameter value, can be null or string.Empty</param>
		/// <returns>result SQL clause, if I can't find the pattern a=@a or something like that, I return the original SQL clause</returns>
		public static string FindAndReplaceParameterRegEx(string pstrSQL, string pstrPara, string pstrValue)
		{
			string METHOD_NAME = THIS + ".FindAndReplaceParameterRegEx()";
			try
			{
				string strPattern = string.Empty;
				string strReplacePattern = string.Empty;				
				
				pstrSQL += "   ";	//refine the SQL clause, do not allow it end with "\n", fix bug when replace the last parameter
				string strRet = StardandizeSQL(pstrSQL);
				
				// We need to define pattern right upper the Match command to easy to debug.
				strPattern = @"(?<fieldGR>(\s*)((\[|)(\w*)(\]|)(\.|)(\[|)(\w*)(\]|))(\s*))(?<operatorGR>((\s)like(\s))|=|>|<|>=|<=|<>)(\s*)(?<valueGR>" +pstrPara+  @")(\s+)"; 				   
				Match oMatch = Regex.Match(pstrSQL, strPattern, RegexOptions.IgnoreCase);				

				if (oMatch.Success)
				{
					// if value is null of empty, then we remove parameter from the sql command
					if ((pstrValue == string.Empty) || (pstrValue == null))
					{
						/// Kill non-last PARAM
						strPattern = @"(?<fieldGR>(\s*)((\[|)(\w*)(\]|)(\.|)(\[|)(\w*)(\]|))(\s*))(?<operatorGR>((\s)like(\s))|=|>|<|>=|<=|<>)(\s*)(?<valueGR>" +pstrPara+  @")(\s+)(?<booloperatorGR>(and)|(or))";						
						strReplacePattern = " ";
						strRet = Regex.Replace(pstrSQL, strPattern, strReplacePattern, RegexOptions.IgnoreCase);
                        
						if(strRet == pstrSQL)	// mean SQL clause is "SELECT * FROM XXX WHERE a = @a   . Kill param pair Not follow by AND,OR (the last PARAM)
						{
							strPattern = @"(?<fieldGR>(\s*)((\[|)(\w*)(\]|)(\.|)(\[|)(\w*)(\]|))(\s*))(?<operatorGR>((\s)like(\s))|=|>|<|>=|<=|<>)(\s*)(?<valueGR>" +pstrPara+  @")(\s+)"; 				   
							strReplacePattern = " ";
							strRet = Regex.Replace(pstrSQL, strPattern, strReplacePattern , RegexOptions.IgnoreCase);
						}
					}
					else // replace parameter by its value
					{
						strPattern = @"(?<fieldGR>(\s*)((\[|)(\w*)(\]|)(\.|)(\[|)(\w*)(\]|))(\s*))(?<operatorGR>((\s)like(\s))|=|>|<|>=|<=|<>)(\s*)(?<valueGR>" +pstrPara+  @")(\s+)"; 					
						strReplacePattern = @" ${fieldGR}${operatorGR} "+ pstrValue +" ";
						strRet = Regex.Replace(pstrSQL, strPattern, strReplacePattern, RegexOptions.IgnoreCase);
					}
				}
				strRet = Kill_SQL_BooleanOperator_AtLast(strRet);
				strRet = Kill_SQL_WHERE_AtLast(strRet);
				return strRet;
			}			
			catch (Exception ex)
			{				
				throw new Exception(METHOD_NAME,ex);				
			}
		}


		/// <summary>
		/// Thachnn: 06 Oct 2005
		/// Remove the AND, OR operator at the end of the input SQL clause
		/// </summary>
		/// <param name="pstrSQL">input string</param>
		/// <returns>string with no SQL boolean operator at last</returns>
		public static string Kill_SQL_BooleanOperator_AtLast(string pstrSQL)
		{
			string METHOD_NAME = THIS + ".Kill_SQL_BooleanOperator_AtLast()";
			try
			{
				string strPattern = string.Empty;
				string strReplacePattern = string.Empty;				
				
				pstrSQL += "   ";	//refine the SQL clause, do not allow it end with "\n", fix bug when replace the last parameter
				string strRet = pstrSQL;

				strPattern = @"(\s+)(?<booloperatorGR>(and|or)(\s*)$)";
				Match oMatch = Regex.Match(pstrSQL, strPattern,RegexOptions.IgnoreCase);

				if (oMatch.Success)
				{					
					strPattern = @"(\s+)(?<booloperatorGR>(and|or)(\s*)$)";
					strReplacePattern = " ";
					strRet = Regex.Replace(pstrSQL, strPattern, strReplacePattern, RegexOptions.IgnoreCase);
				}
				return strRet;
			}			
			catch (Exception ex)
			{
				throw new Exception(METHOD_NAME,ex);
			}
		}


		/// <summary>
		/// Thachnn: 06 Oct 2005
		/// Remove the WHERE keyword at the end of the input SQL clause		
		/// </summary>
		/// <param name="pstrSQL">input string</param>
		/// <returns>string with no WHERE keyword at last</returns>
		public static string Kill_SQL_WHERE_AtLast(string pstrSQL)
		{
			string METHOD_NAME = THIS + ".Kill_SQL_WHERE_AtLast()";
			try
			{
				string strPattern = string.Empty;
				string strReplacePattern = string.Empty;				
				
				pstrSQL += "   ";	//refine the SQL clause, do not allow it end with "\n", fix bug when replace the last parameter
				string strRet = pstrSQL;

				strPattern = @"(\s+)(?<booloperatorGR>(where)(\s*)$)";
				Match oMatch = Regex.Match(pstrSQL, strPattern,RegexOptions.IgnoreCase);

				if (oMatch.Success)
				{					
					strPattern = @"(\s+)(?<booloperatorGR>(where)(\s*)$)";
					strReplacePattern = " ";
					strRet = Regex.Replace(pstrSQL, strPattern, strReplacePattern, RegexOptions.IgnoreCase);
				}
				return strRet;
			}			
			catch (Exception ex)
			{
				throw new Exception(METHOD_NAME,ex);
			}
		}

		
		/// <summary>
		/// Thachnn: 06 Oct 2005		
		/// replace all \n \t \r in the SQL clause to _space_ character
		/// </summary>
		/// <param name="pstrOld"></param>
		/// <returns>standard string</returns>
		public static string StardandizeSQL(string pstrOld)
		{
			string METHOD_NAME = THIS + ".StardandizeSQL()";
			try
			{			
				string strRet = pstrOld;
				strRet = strRet.Replace("\t"," ");
				strRet = strRet.Replace("\n"," ");
				strRet = strRet.Replace("\r"," ");
				strRet = strRet.Replace(@"\t"," ");
				strRet = strRet.Replace(@"\n"," ");
				strRet = strRet.Replace(@"\r"," ");
				return strRet;
			}
			catch (Exception ex)
			{
				throw new Exception(METHOD_NAME,ex);
			}
		}


		#endregion

		#region Return Goods Receipt Slip: Trada
		/// <summary>
		/// GetReturnGoodsReceiptByMasterID
		/// </summary>
		/// <param name="pintReturnGoodsReceiptMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, Mar 13 2006</date>
		public DataTable GetReturnGoodsReceiptByMasterID(int pintReturnGoodsReceiptMasterID)
		{			
			const string METHOD_NAME = THIS + ".GetReturnGoodsReceiptByMasterID()";

			DataTable dtbResultTable = new DataTable(SO_SaleOrderMasterTable.TABLE_NAME);
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "select M.ReturnedGoodsNumber, M.TransDate, PA.Name +'('+ PA.Code + ')' Customer, "
					+ " PL.Code, SM.Code SONo, D.Line, P.Code PartNumber, P.Description PartName, P.Revision Model, "
					+ " D.ReceiveQuantity, UM.Code UM, ML.Code Masloc, L.Code Loc, B.Code Bin "
					+ " from dbo.SO_ReturnedGoodsMaster M "
					+ " left join MST_Party PA on PA.PartyID = M.PartyID "
					+ " left join MST_PartyLocation PL on PL.PartyLocationID = M.PartyLocationID "
					+ " left join dbo.SO_SaleOrderMaster SM on SM.SaleOrderMasterID = M.SaleOrderMasterID "
					+ " inner join SO_ReturnedGoodsDetail D on D.ReturnedGoodsMasterID = M.ReturnedGoodsMasterID "
					+ " inner join ITM_Product P on P.ProductID = D.ProductID "
					+ " inner join MST_UnitOfMeasure UM on UM.UnitOfMeasureID = D.UnitID "
					+ " inner join MST_MasterLocation ML on ML.MasterLocationID = D.MasterLocationID "
					+ " inner join MST_Location L on L.LocationID = D.LocationID "
					+ " left join MST_BIN B on B.BinID = D.BinID "
					+ " WHERE M.ReturnedGoodsMasterID = " + pintReturnGoodsReceiptMasterID.ToString()
					+ " order by P.Code, P.Description, P.Revision "; 
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;				
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion

		#region Sale Order Invoice Report: Tuan TQ
		/// <summary>
		/// Get Sale Order commit information of a specific commit
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ. Oct 28, 2005</author>
		public DataTable GetSaleOrderCommitData(int pintSOCommitMasterID)
		{
			//TODO:L Kill Injection here
			const string METHOD_NAME = THIS + ".GetSaleOrderCommitData()";
			
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = string.Empty;

				strSql = "SELECT  DISTINCT ITM_Product.ProductID, "
					+ " refDetail.CustomerItemCode AS ITM_ProductCode,"
					+ " ITM_Product.Description AS ITM_ProductDescription,"
                    + " ITM_Product.PartNameVN AS ITM_ProductDescriptionVN,"
					+ " MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,"
					+ " ITM_Product.Revision AS ITM_ProductRevision,"

					+ " SO_ConfirmShipDetail.InvoiceQty as DeliveryQuantity,"

					+ " ((SELECT SUM( detail.Price * detail.InvoiceQty)"
					+ " FROM SO_ConfirmShipDetail detail"
					+ " WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID"
					+ " AND detail.ProductID = ITM_Product.ProductID"
					+ " )	"
					+ " /"
			
					//validate data to avoid division by zero error 
					+ " ( "
					+ " Case  "
					+ " When ( "
					+ " SELECT SUM(detail.InvoiceQty) "
					+ " FROM SO_ConfirmShipDetail detail "
					+ " WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID "
					+ " AND detail.ProductID = ITM_Product.ProductID "
					+ "    ) = 0  "
					+ " then 1 "
					+ "     else   " 
					+ " ( "
					+ " SELECT SUM(detail.InvoiceQty) "
					+ " FROM SO_ConfirmShipDetail detail "
					+ " WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID "
					+ " AND detail.ProductID = ITM_Product.ProductID "
					+ "    ) "
					+ " end      " 
					+ " ) "
					//end validate

					+ " )	"
					+ " as AVGPrice,"

					+ " SO_ConfirmShipDetail.Price as UnitPrice,"
					+ " (SO_ConfirmShipDetail.InvoiceQty * SO_ConfirmShipDetail.Price) AS NetAmount,"					

					+ " ISNULL(SO_ConfirmShipDetail.VATPercent, 0) As VATPercent,"
					+ " ISNULL(SO_ConfirmShipDetail.VatAmount, 0) as VatAmount, "

					+ " SO_ConfirmShipMaster.InvoiceNo,"
					+ " SO_ConfirmShipMaster.ConfirmShipNo,"

					+ " SO_ConfirmShipMaster.InvoiceDate as ShippedDate,"

					+ " MST_Party.Name AS MST_PartyName,"
					+ " MST_Party.Address AS MST_PartyAddress,"
					+ " Case when CharIndex('" + DEMILITER_CHAR + "', MST_Party.BankAccount) > 0 then"
					+ "    Substring(MST_Party.BankAccount, 1, CharIndex('" + DEMILITER_CHAR + "', MST_Party.BankAccount) - 1) "
					+ " else MST_Party.BankAccount	"
					+ " End As MST_PartyBankAccount,"

					+ " Case when CharIndex('" + DEMILITER_CHAR + "', MST_Party.BankAccount) > 0 then"
					+ " Substring(MST_Party.BankAccount, CharIndex('" + DEMILITER_CHAR + "', MST_Party.BankAccount) + 1, Len(MST_Party.BankAccount))"
					+ " Else ''"
					+ " End As MST_PartyBankName,"

					+ " MST_Party.VATCode AS MST_PartyTaxCode,"

					+ " MST_Party.MAPBankAccountNo as BankAccountNo,"
					+ " Case when CharIndex('" + DEMILITER_CHAR + "', MST_Party.MAPBankAccountName) > 0 then"
					+ " Substring(MST_Party.MAPBankAccountName, 1, CharIndex('" + DEMILITER_CHAR + "', MST_Party.MAPBankAccountName) - 1) "
					+ " else MST_Party.MAPBankAccountName"
					+ " End As BankAccountName,"					

					+ " ("
					+ " SELECT TOP 1 enm_GateType.Description"
					+ " FROM SO_DeliverySchedule"
					+ " LEFT JOIN SO_Gate ON SO_DeliverySchedule.GateID = SO_Gate.GateID"
					+ " LEFT JOIN enm_GateType ON enm_GateType.GateTypeID = SO_Gate.GateTypeID"
					+ " WHERE SO_DeliverySchedule.DeliveryScheduleID = SO_ConfirmShipDetail.DeliveryScheduleID"
					+ " )AS SaleType,"
                    + " GA.Code SOGate,"
                    + " SO_ConfirmShipMaster.DocumentNumber AS CustomerPurchaseOrderNo,"
                    + " SO_ConfirmShipDetail.ConfirmShipDetailID, MST_PartyLocation.[Description] AS ShipToLocation, ST.Code AS SaleType1"

                    + " FROM    SO_ConfirmShipDetail "
					+ " INNER JOIN SO_ConfirmShipMaster ON SO_ConfirmShipDetail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID "
					+ " INNER JOIN SO_SaleOrderMaster ON SO_ConfirmShipMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID "
					+ " INNER JOIN MST_Party ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID "
					+ " INNER JOIN MST_PartyLocation ON SO_SaleOrderMaster.ShipToLocID = MST_PartyLocation.PartyLocationID "
                    + " INNER JOIN SO_SaleOrderDetail ON SO_ConfirmShipDetail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID "
					+ " INNER JOIN ITM_Product ON SO_ConfirmShipDetail.ProductID = ITM_Product.ProductID "
                    + " INNER JOIN SO_DeliverySchedule E ON E.DeliveryScheduleID = SO_ConfirmShipDetail.DeliveryScheduleID"
					+ " LEFT JOIN MST_UnitOfMeasure ON SO_SaleOrderDetail.SellingUMID = MST_UnitOfMeasure.UnitOfMeasureID "					
					+ " LEFT JOIN SO_CustomerItemRefMaster refMaster ON refMaster.PartyID = MST_Party.PartyID"
					+ " LEFT JOIN SO_CustomerItemRefDetail refDetail ON refMaster.CustomerItemRefMasterID = refDetail.CustomerItemRefMasterID"
                    + " AND refDetail.ProductID = ITM_Product.ProductID"
                    + " LEFT JOIN SO_Gate GA ON E.GateID = GA.GateID"
                    + " LEFT JOIN SO_SaleType ST ON SO_SaleOrderMaster.SaleTypeID = ST.SaleTypeID"
                    + " WHERE SO_ConfirmShipMaster.ConfirmShipMasterID = " + pintSOCommitMasterID
					+ " AND SO_ConfirmShipDetail.InvoiceQty > 0"
					+ " ORDER BY ITM_Product.Revision";
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS) {CommandTimeout = 3600};
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_ConfirmShipDetailTable.TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					return dstPCS.Tables[0];
				}
				else
				{
					return new DataTable();
				}
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		/// <summary>
		/// Gets data for print sale invoice only
		/// </summary>
		/// <param name="pintSOInvoiceMasterID">Sale Invoice ID</param>
		/// <returns>DataTable</returns>
		/// <author> DungLA. Sep 13, 2006</author>
		public DataTable GetSaleOrderInvoiceData(int pintSOInvoiceMasterID)
		{
			const string METHOD_NAME = THIS + ".GetSaleOrderInvoiceData()";
			
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = string.Empty;

				strSql = "SELECT  DISTINCT ITM_Product.ProductID, "
					+ " refDetail.CustomerItemCode AS ITM_ProductCode,"
					+ " ITM_Product.Description AS ITM_ProductDescription,"
                    + " ITM_Product.PartNameVN AS ITM_ProductDescriptionVN,"
					+ " MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,"
					+ " ITM_Product.Revision AS ITM_ProductRevision,"

					+ " SO_InvoiceDetail.InvoiceQty as DeliveryQuantity,"

					+ " ((SELECT SUM( detail.Price * detail.InvoiceQty)"
					+ " FROM SO_InvoiceDetail detail"
					+ " WHERE detail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID"
					+ " AND detail.ProductID = ITM_Product.ProductID"
					+ " )	"
					+ " /"
			
					//validate data to avoid division by zero error 
					+ " ( "
					+ " Case  "
					+ " When ( "
					+ " SELECT SUM(detail.InvoiceQty) "
					+ " FROM SO_InvoiceDetail detail "
					+ " WHERE detail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID "
					+ " AND detail.ProductID = ITM_Product.ProductID "
					+ "    ) = 0  "
					+ " then 1 "
					+ "     else   " 
					+ " ( "
					+ " SELECT SUM(detail.InvoiceQty) "
					+ " FROM SO_InvoiceDetail detail "
					+ " WHERE detail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID "
					+ " AND detail.ProductID = ITM_Product.ProductID "
					+ "    ) "
					+ " end      " 
					+ " ) "
					//end validate

					+ " )	"
					+ " as AVGPrice,"

					+ " SO_InvoiceDetail.Price as UnitPrice,"
					+ " (SO_InvoiceDetail.InvoiceQty * SO_InvoiceDetail.Price) AS NetAmount,"					

					+ " ISNULL(SO_InvoiceDetail.VATPercent, 0) As VATPercent,"
					+ " ISNULL(SO_InvoiceDetail.VatAmount, 0) as VatAmount, "

					+ " SO_InvoiceMaster.InvoiceNo,"
					+ " SO_InvoiceMaster.ConfirmShipNo,"

					+ " SO_InvoiceMaster.InvoiceDate as ShippedDate,"

					+ " MST_Party.Name AS MST_PartyName,"
					+ " MST_Party.Address AS MST_PartyAddress,"
					+ " Case when CharIndex('" + DEMILITER_CHAR + "', MST_Party.BankAccount) > 0 then"
					+ "    Substring(MST_Party.BankAccount, 1, CharIndex('" + DEMILITER_CHAR + "', MST_Party.BankAccount) - 1) "
					+ " else MST_Party.BankAccount	"
					+ " End As MST_PartyBankAccount,"

					+ " Case when CharIndex('" + DEMILITER_CHAR + "', MST_Party.BankAccount) > 0 then"
					+ " Substring(MST_Party.BankAccount, CharIndex('" + DEMILITER_CHAR + "', MST_Party.BankAccount) + 1, Len(MST_Party.BankAccount))"
					+ " Else ''"
					+ " End As MST_PartyBankName,"

					+ " MST_Party.VATCode AS MST_PartyTaxCode,"

					+ " MST_Party.MAPBankAccountNo as BankAccountNo,"
					+ " Case when CharIndex('" + DEMILITER_CHAR + "', MST_Party.MAPBankAccountName) > 0 then"
					+ " Substring(MST_Party.MAPBankAccountName, 1, CharIndex('" + DEMILITER_CHAR + "', MST_Party.MAPBankAccountName) - 1) "
					+ " else MST_Party.MAPBankAccountName"
					+ " End As BankAccountName,"					

					+ " ("
					+ " SELECT TOP 1 enm_GateType.Description"
					+ " FROM SO_DeliverySchedule"
					+ " LEFT JOIN SO_Gate ON SO_DeliverySchedule.GateID = SO_Gate.GateID"
					+ " LEFT JOIN enm_GateType ON enm_GateType.GateTypeID = SO_Gate.GateTypeID"
					+ " WHERE SO_DeliverySchedule.DeliveryScheduleID = SO_InvoiceDetail.DeliveryScheduleID"
					+ " )AS SaleType,"
                    + " GA.Code SOGate,"
					+ " SO_InvoiceMaster.DocumentNumber AS CustomerPurchaseOrderNo,"
                    + " SO_InvoiceDetail.InvoiceDetailID, MST_PartyLocation.[Description] AS ShipToLocation, ST.Code AS SaleType1"

                    + " FROM    SO_InvoiceDetail "
					+ " INNER JOIN SO_InvoiceMaster ON SO_InvoiceDetail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID "
					+ " INNER JOIN SO_SaleOrderMaster ON SO_InvoiceMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID "
					+ " INNER JOIN MST_PartyLocation ON SO_SaleOrderMaster.ShipToLocID = MST_PartyLocation.PartyLocationID "
                    + " INNER JOIN MST_Party ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID "
					+ " INNER JOIN SO_SaleOrderDetail ON SO_InvoiceDetail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID "
					+ " INNER JOIN ITM_Product ON SO_InvoiceDetail.ProductID = ITM_Product.ProductID "
                    + " INNER JOIN SO_DeliverySchedule E ON E.DeliveryScheduleID = SO_InvoiceDetail.DeliveryScheduleID"
					+ " LEFT JOIN MST_UnitOfMeasure ON SO_SaleOrderDetail.SellingUMID = MST_UnitOfMeasure.UnitOfMeasureID "					
					+ " LEFT JOIN SO_CustomerItemRefMaster refMaster ON refMaster.PartyID = MST_Party.PartyID"
					+ " LEFT JOIN SO_CustomerItemRefDetail refDetail ON refMaster.CustomerItemRefMasterID = refDetail.CustomerItemRefMasterID"
                    + " AND refDetail.ProductID = ITM_Product.ProductID"
                    + " LEFT JOIN SO_Gate GA ON E.GateID = GA.GateID"
                    + " LEFT JOIN SO_SaleType ST ON SO_SaleOrderMaster.SaleTypeID = ST.SaleTypeID"
                    + " WHERE SO_InvoiceMaster.InvoiceMasterID = " + pintSOInvoiceMasterID
					+ " AND SO_InvoiceDetail.InvoiceQty > 0"
					+ " ORDER BY ITM_Product.Revision";
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			    ocmdPCS = new OleDbCommand(strSql, oconPCS) {CommandTimeout = 3600};

			    ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_InvoiceDetailTable.TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
					return dstPCS.Tables[0];
				else
					return new DataTable();
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion

		#region Orders Summary Report: Tuan TQ
		/// <summary>
		/// Get Order summary information
		/// </summary>
		/// <param name="pnLine"></param>
		/// <param name="pstrWONo"></param>
		/// <returns></returns>
		/// <author> Tuan TQ. Nov 21, 2005</author>
		public DataTable GetOrderSummaryData(int pintCCNID, int pintYear, string pstrOtherCondition)
		{
			//TODO:L Kill Injection here
			const string METHOD_NAME = THIS + ".GetOrderSummaryData()";		

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT  SO_SaleOrderMaster.CCNID, ";

				strSql += " ITM_Category.CategoryID, ";
				strSql += " ITM_Category.Code AS ITM_CategoryCode, ";
				strSql += " MST_Party.Code AS MST_PartyCode, ";
				strSql += " ITM_Product.Code AS ITM_ProductCode, ";
				strSql += " ITM_Product.ProductID, ";
				strSql += " MST_UnitOfMeasure.Code AS SellingUM, ";				
				strSql += " ITM_Product.Description AS ITM_ProductDescription, ";
				strSql += " ITM_Product.Revision AS ITM_ProductRevision, ";
				strSql += " SO_SaleType.Code AS SO_SaleTypeCode, ";
				strSql += " Year(SO_DeliverySchedule.ScheduleDate) as TransYear, ";
				strSql += " Month(SO_DeliverySchedule.ScheduleDate) as TransMonth, ";				
				strSql += " Case ";
				strSql += " When ITM_Product.QuantitySet IS NULL then SUM(SO_DeliverySchedule.DeliveryQuantity)";
				strSql += " When ITM_Product.QuantitySet = 0 then SUM(SO_DeliverySchedule.DeliveryQuantity)";
				strSql += " Else SUM(SO_DeliverySchedule.DeliveryQuantity/ITM_Product.QuantitySet)";
				strSql += " End as OrderQuantity";				
				strSql += " FROM  SO_SaleOrderMaster ";
				strSql += " INNER JOIN MST_Party ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID ";
				strSql += " INNER JOIN SO_SaleOrderDetail ON SO_SaleOrderDetail.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID ";
				strSql += " INNER JOIN MST_UnitOfMeasure ON MST_UnitOfMeasure.UnitOfMeasureID = SO_SaleOrderDetail.SellingUMID ";
				strSql += " INNER JOIN ITM_Product ON SO_SaleOrderDetail.ProductID = ITM_Product.ProductID ";
				strSql += " INNER JOIN SO_DeliverySchedule ON SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID";
				strSql += " LEFT JOIN  ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID ";				
				strSql += " LEFT JOIN SO_SaleType ON SO_SaleOrderMaster.SaleTypeID = SO_SaleType.SaleTypeID ";
				strSql += " WHERE SO_SaleOrderMaster.CCNID = " + pintCCNID;
				strSql += " AND Year(SO_DeliverySchedule.ScheduleDate) = " + pintYear;

				//Add other condition
				strSql += pstrOtherCondition;

				strSql += " GROUP BY SO_SaleOrderMaster.CCNID, ";
				strSql += " MST_Party.Code, ";
				strSql += " ITM_Category.Code, ";
				strSql += " ITM_Product.Code, ";
				strSql += " ITM_Product.Description, ";
       			strSql += " ITM_Product.Revision, ";
				strSql += " MST_UnitOfMeasure.Code, ";				
				strSql += " SO_SaleType.Code, ";
				strSql += " Year(SO_DeliverySchedule.ScheduleDate), ";
				strSql += " Month(SO_DeliverySchedule.ScheduleDate), ";
				strSql += " ITM_Category.CategoryID,";
				strSql += " ITM_Product.ProductID,";				
				strSql += " ITM_Product.QuantitySet";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_ConfirmShipDetailTable.TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					return dstPCS.Tables[0];
				}
				else
				{
					return new DataTable();
				}
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		
		public DataTable GetProductCapacityOfCategory(string pstrCategoryID, string pstrListOfProducts)
		{			
			const string METHOD_NAME = THIS + ".GetProductCapacityOfCategory()";		

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT TOP 1 ProductID,";
				strSql += "	CategoryID,";
				strSql += "	WorkCenterID,";
				strSql += "	IsMain,";
				strSql += "	Code,";
				strSql += "	LeadTime,";
				strSql += "	QuantitySet,";
				strSql += "	Capacity";
				strSql += "	FROM v_LeadTimeByMainWorkCenter";
				strSql += "	WHERE  CategoryID = "  + pstrCategoryID;
				strSql += "	AND ProductID IN " + pstrListOfProducts;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					return dstPCS.Tables[0];
				}
				else
				{
					return new DataTable();
				}
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion

		#region Issuance Slip Report: Tuan TQ
		
		/// <summary>
		/// Get Delivery To Next Stage Slip data
		/// </summary>
		/// <param name="pintIssueMasterialMasterId">Issue Masterial Master Id</param>
		/// <returns>Top 10 row of result</returns>
		public DataTable GetIssuanceSlipData(int pintIssueMasterialMasterId, string pstrMaxRow)
		{			
			const string METHOD_NAME = THIS + ".GetIssuanceSlipData()";

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT  TOP " + pstrMaxRow;
				strSql += " 0 as RowIndex,";
				strSql += " PRO_WorkOrderMaster.WorkOrderNo,";
				strSql += " PRO_WorkOrderDetail.Line as WOLine,";
				strSql += " PRO_IssueMaterialMaster.PostDate,";
				strSql += " PRO_IssueMaterialMaster.IssueNo as SlipNo,";
				strSql += " PRO_IssuePurpose.Description as Purpose,";
				strSql += " PRO_Shift.ShiftDesc,";
				strSql += " Loc_From.Code as FromLoc_Code,";
				strSql += " Loc_From.Name as FromLoc_Name,";
				strSql += " Loc_To.Code as ToLoc_Code,";
				strSql += " Loc_To.Name as ToLoc_Name,";
				strSql += " make_Item.Code as MakeItem_Code,";
				strSql += " make_Item.Revision as MakeItem_Model,";
				strSql += " make_Item.Description as MakeItem_Name,"; 
				strSql += " sub_Item.Code as SubItem_Code,";
				strSql += " sub_Item.Revision as SubItem_Model,";
				strSql += " sub_Item.Description as SubItem_Name,";
				strSql += @" ((PRO_WorkOrderDetail.OrderQuantity * ITM_BOM.Quantity) / ((100 - ISNULL(ITM_BOM.Shrink, 0))/100)) as PlannedQty,";							
				strSql += " PRO_IssueMaterialDetail.CommitQuantity,";
				strSql += " MST_UnitOfMeasure.Code as StockUM,";
				strSql += " MST_Party.Code as Vendor";

				strSql += " FROM   PRO_IssueMaterialMaster";
				strSql += " INNER JOIN PRO_IssuePurpose ON PRO_IssuePurpose.IssuePurposeID = PRO_IssueMaterialMaster.IssuePurposeID";
				strSql += " INNER JOIN PRO_Shift ON PRO_Shift.ShiftID = PRO_IssueMaterialMaster.ShiftID";
				strSql += " INNER JOIN PRO_IssueMaterialDetail ON PRO_IssueMaterialMaster.IssueMaterialMasterID = PRO_IssueMaterialDetail.IssueMaterialMasterID";
				strSql += " INNER JOIN PRO_WorkOrderMaster ON PRO_WorkOrderMaster.WorkOrderMasterID = PRO_IssueMaterialDetail.WorkOrderMasterID";
				strSql += " INNER JOIN PRO_WorkOrderDetail ON PRO_WorkOrderDetail.WorkOrderDetailID = PRO_IssueMaterialDetail.WorkOrderDetailID";

				strSql += " INNER JOIN MST_Location Loc_To ON PRO_IssueMaterialMaster.ToLocationID = Loc_To.LocationID";
				strSql += " INNER JOIN MST_Location Loc_From ON PRO_IssueMaterialDetail.LocationID = Loc_From.LocationID"; 
				strSql += " INNER JOIN ITM_Product sub_Item ON PRO_IssueMaterialDetail.ProductID = sub_Item.ProductID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON PRO_IssueMaterialDetail.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";

				strSql += " INNER JOIN ITM_Product make_Item ON PRO_WorkOrderDetail.ProductID = make_Item.ProductID";

				strSql += " INNER JOIN ITM_BOM ON ITM_BOM.ProductID = PRO_WorkOrderDetail.ProductID";
				strSql += " AND ITM_BOM.ComponentID = sub_Item.ProductID";

				strSql += " LEFT JOIN MST_Party ON MST_Party.PartyID = sub_Item.PrimaryVendorID";
				strSql += " WHERE PRO_IssueMaterialMaster.IssueMaterialMasterID = " + pintIssueMasterialMasterId; 
				strSql += " ORDER BY PRO_IssueMaterialDetail.Line ASC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_IssueMaterialMasterTable.TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					return dstPCS.Tables[0];
				}
				else
				{
					return new DataTable();
				}
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion Issuance Slip Report

		#region Delivery To Next Stage Slip Report: Tuan TQ
		
		/// <summary>
		/// Get Delivery To Next Stage Slip data
		/// </summary>
		/// <param name="pintIssueMasterialMasterId">Issue Masterial Master Id</param>
		/// <returns>Delivery To Next Slip Data</returns>
		public DataTable GetDelivery2NextSlipData(int pintIssueMasterialMasterId, string pstrMaxRow)
		{			
			const string METHOD_NAME = THIS + ".GetDelivery2NextSlipData()";

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT  TOP " + pstrMaxRow;
				strSql += " 0 as RowIndex,";
				strSql += " PRO_WorkOrderMaster.WorkOrderNo,";
				strSql += " PRO_WorkOrderDetail.Line as WOLine,";
				strSql += " PRO_IssueMaterialMaster.PostDate,";
				strSql += " PRO_IssueMaterialMaster.IssueNo as SlipNo,";
				strSql += " PRO_IssuePurpose.Description as Purpose,";
				strSql += " PRO_Shift.ShiftDesc,";
				strSql += " Loc_From.Code as FromLoc_Code,";
				strSql += " Loc_From.Name as FromLoc_Name,";
				strSql += " Loc_To.Code as ToLoc_Code,";
				strSql += " Loc_To.Name as ToLoc_Name,";
				strSql += " make_Item.Code as MakeItem_Code,";
				strSql += " make_Item.Revision as MakeItem_Model,";
				strSql += " make_Item.Description as MakeItem_Name,"; 
				strSql += " sub_Item.Code as SubItem_Code,";
				strSql += " sub_Item.Revision as SubItem_Model,";
				strSql += " sub_Item.Description as SubItem_Name,";
				strSql += @" ((PRO_WorkOrderDetail.OrderQuantity * ITM_BOM.Quantity) / ((100 - ISNULL(ITM_BOM.Shrink, 0))/100)) as PlannedQty,";
				strSql += " PRO_IssueMaterialDetail.CommitQuantity,";
				strSql += " MST_UnitOfMeasure.Code as StockUM,";
				strSql += " ITM_Category.Code as Category";

				strSql += " FROM   PRO_IssueMaterialMaster";
				strSql += " INNER JOIN PRO_IssuePurpose ON PRO_IssuePurpose.IssuePurposeID = PRO_IssueMaterialMaster.IssuePurposeID";
				strSql += " INNER JOIN PRO_Shift ON PRO_Shift.ShiftID = PRO_IssueMaterialMaster.ShiftID";
				strSql += " INNER JOIN PRO_IssueMaterialDetail ON PRO_IssueMaterialMaster.IssueMaterialMasterID = PRO_IssueMaterialDetail.IssueMaterialMasterID";
				strSql += " INNER JOIN PRO_WorkOrderMaster ON PRO_WorkOrderMaster.WorkOrderMasterID = PRO_IssueMaterialDetail.WorkOrderMasterID";
				strSql += " INNER JOIN PRO_WorkOrderDetail ON PRO_WorkOrderDetail.WorkOrderDetailID = PRO_IssueMaterialDetail.WorkOrderDetailID";

				strSql += " INNER JOIN MST_Location Loc_To ON PRO_IssueMaterialMaster.ToLocationID = Loc_To.LocationID";
				strSql += " INNER JOIN MST_Location Loc_From ON PRO_IssueMaterialDetail.LocationID = Loc_From.LocationID"; 
				strSql += " INNER JOIN ITM_Product sub_Item ON PRO_IssueMaterialDetail.ProductID = sub_Item.ProductID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON PRO_IssueMaterialDetail.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";

				strSql += " INNER JOIN ITM_Product make_Item ON PRO_WorkOrderDetail.ProductID = make_Item.ProductID";
				
				strSql += " INNER JOIN ITM_BOM ON ITM_BOM.ProductID = PRO_WorkOrderDetail.ProductID";
				strSql += " AND ITM_BOM.ComponentID = sub_Item.ProductID";

				strSql += " LEFT JOIN ITM_Category ON sub_Item.CategoryID = ITM_Category.CategoryID";
				strSql += " WHERE PRO_IssueMaterialMaster.IssueMaterialMasterID = " + pintIssueMasterialMasterId; 
				strSql += " ORDER BY PRO_IssueMaterialDetail.Line ASC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_IssueMaterialMasterTable.TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					return dstPCS.Tables[0];
				}
				else
				{
					return new DataTable();
				}
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion Delivery To Next Stage Slip Report

		#region Other Issuance Slip Report: Tuan TQ
		
		/// <summary>
		/// Get Delivery To Next Stage Slip data
		/// </summary>
		/// <param name="pintIssueMasterialMasterId">Issue Masterial Master Id</param>
		/// <returns>Top 40 row of result</returns>
		public DataTable GetOtherIssuanceSlipData(int pintIssueMasterialMasterId, string pstrMaxRow)
		{			
			const string METHOD_NAME = THIS + ".GetOtherIssuanceSlipData()";

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT  TOP " + pstrMaxRow;
				strSql += " 0 as RowIndex,";
				strSql += " PRO_WorkOrderMaster.WorkOrderNo,";
				strSql += " PRO_WorkOrderDetail.Line as WOLine,";
				strSql += " PRO_IssueMaterialMaster.PostDate,";
				strSql += " PRO_IssueMaterialMaster.IssueNo as SlipNo,";
				strSql += " PRO_IssuePurpose.Description as Purpose,";
				strSql += " PRO_Shift.ShiftDesc,";
				strSql += " Loc_From.Code as FromLoc_Code,";
				strSql += " Loc_From.Name as FromLoc_Name,";
				strSql += " Loc_To.Code as ToLoc_Code,";
				strSql += " Loc_To.Name as ToLoc_Name,";
				strSql += " make_Item.Code as MakeItem_Code,";
				strSql += " make_Item.Revision as MakeItem_Model,";
				strSql += " make_Item.Description as MakeItem_Name,"; 

				strSql += " sub_Item.Code as SubItem_Code,";
				strSql += " sub_Item.Revision as SubItem_Model,";
				strSql += " sub_Item.Description as SubItem_Name,";				
				strSql += " PRO_IssueMaterialDetail.CommitQuantity,";
				strSql += " MST_UnitOfMeasure.Code as StockUM,";
				strSql += " ITM_Category.Code as Category,";
				strSql += " MST_Party.Code as Vendor";

				strSql += " FROM   PRO_IssueMaterialMaster";
				strSql += " INNER JOIN PRO_IssuePurpose ON PRO_IssuePurpose.IssuePurposeID = PRO_IssueMaterialMaster.IssuePurposeID";
				strSql += " INNER JOIN PRO_Shift ON PRO_Shift.ShiftID = PRO_IssueMaterialMaster.ShiftID";
				strSql += " INNER JOIN PRO_IssueMaterialDetail ON PRO_IssueMaterialMaster.IssueMaterialMasterID = PRO_IssueMaterialDetail.IssueMaterialMasterID";
				strSql += " INNER JOIN PRO_WorkOrderMaster ON PRO_WorkOrderMaster.WorkOrderMasterID = PRO_IssueMaterialDetail.WorkOrderMasterID";
				strSql += " INNER JOIN PRO_WorkOrderDetail ON PRO_WorkOrderDetail.WorkOrderDetailID = PRO_IssueMaterialDetail.WorkOrderDetailID";

				strSql += " INNER JOIN MST_Location Loc_To ON PRO_IssueMaterialMaster.ToLocationID = Loc_To.LocationID";
				strSql += " INNER JOIN MST_Location Loc_From ON PRO_IssueMaterialDetail.LocationID = Loc_From.LocationID"; 
				strSql += " INNER JOIN ITM_Product sub_Item ON PRO_IssueMaterialDetail.ProductID = sub_Item.ProductID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON PRO_IssueMaterialDetail.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";

				strSql += " INNER JOIN ITM_Product make_Item ON PRO_WorkOrderDetail.ProductID = make_Item.ProductID";
				
				strSql += " LEFT JOIN MST_Party ON MST_Party.PartyID = sub_Item.PrimaryVendorID";
				strSql += " LEFT JOIN ITM_Category ON sub_Item.CategoryID = ITM_Category.CategoryID";

				strSql += " WHERE PRO_IssueMaterialMaster.IssueMaterialMasterID = " + pintIssueMasterialMasterId; 
				strSql += " ORDER BY PRO_IssueMaterialDetail.Line ASC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_IssueMaterialMasterTable.TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					return dstPCS.Tables[0];
				}
				else
				{
					return new DataTable();
				}
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion

		#region Delivery To Customer Schedule Report: Tuan TQ
		
		/// <summary>
		/// Get Delivery To Customer Schedule data
		/// </summary>
		/// <param name="pintIssueMasterialMasterId">Issue Masterial Master Id</param>
		/// <returns>Top 40 row of result</returns>
		public DataTable GetDelivery2CustomerData(int pintSaleOrderMasterId)
		{			
			const string METHOD_NAME = THIS + ".GetDelivery2CustomerData()";

			DataTable dtbResultTable = new DataTable(SO_SaleOrderMasterTable.TABLE_NAME);
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT  DatePart(hour, SO_DeliverySchedule.ScheduleDate) as DeliveryTime,";
				strSql += " Day(SO_DeliverySchedule.ScheduleDate) as DeliveryDay,";
				strSql += " Month(SO_DeliverySchedule.ScheduleDate) as DeliveryMonth,";
				strSql += " Year(SO_DeliverySchedule.ScheduleDate) as DeliveryYear,";

				strSql += " ITM_Product.Code as ITM_ProductCode,";
				strSql += " ITM_Product.Description as ITM_ProductDescription,";
				strSql += " ITM_Category.Code as ITM_CategoryCode,";
				strSql += " ITM_Product.Revision as ITM_ProductRevision,";
				strSql += " MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode,";
				
				strSql += " SO_SaleOrderMaster.Code as SO_SaleOrderMasterCode,";
				strSql += " MST_Party.Code + ' (' + MST_Party.Name + ')' as MST_PartyCodeName,";				
				strSql += " SUM(SO_DeliverySchedule.DeliveryQuantity) as DeliveryQuantity";

				strSql += " FROM    SO_DeliverySchedule";
				strSql += " INNER JOIN SO_SaleOrderDetail ON SO_SaleOrderDetail.SaleOrderDetailID = SO_DeliverySchedule.SaleOrderDetailID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON SO_SaleOrderDetail.SellingUMID = MST_UnitOfMeasure.UnitOfMeasureID";
				strSql += " INNER JOIN ITM_Product ON SO_SaleOrderDetail.ProductID = ITM_Product.ProductID";
				strSql += " INNER JOIN SO_SaleOrderMaster ON SO_SaleOrderMaster.SaleOrderMasterID = SO_SaleOrderDetail.SaleOrderMasterID";
				strSql += " INNER JOIN MST_Party ON MST_Party.PartyID = SO_SaleOrderMaster.PartyID";
				strSql += " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID";
				
				strSql += " WHERE SO_SaleOrderMaster.SaleOrderMasterID = " + pintSaleOrderMasterId;

				strSql += " GROUP BY DatePart(hour, SO_DeliverySchedule.ScheduleDate),";
				strSql += " Day(SO_DeliverySchedule.ScheduleDate),";
				strSql += " Month(SO_DeliverySchedule.ScheduleDate),";
				strSql += " Year(SO_DeliverySchedule.ScheduleDate),";
				strSql += " ITM_Product.Code,";
				strSql += " ITM_Product.Description,";
				strSql += " ITM_Category.Code,";
				strSql += " ITM_Product.Revision,";
				strSql += " MST_UnitOfMeasure.Code,";
				strSql += " SO_SaleOrderMaster.Code,";
				strSql += " MST_Party.Code + ' (' + MST_Party.Name + ')'";

				strSql += " ORDER BY DeliveryTime, DeliveryDay ASC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;				
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion		

		#region WO Completion Slip Report: Tuan TQ
		
		/// <summary>
		/// Get WO Completion Slip data
		/// </summary>
		/// <param name="pintIssueMasterialMasterId">WO Completion Id</param>
		/// <returns>Data Table</returns>
		public DataTable GetWOCompletionData(int pintWOCompletionId)
		{			
			const string METHOD_NAME = THIS + ".GetWOCompletionData()";

			DataTable dtbResultTable = new DataTable(PRO_WorkOrderCompletionTable.TABLE_NAME);
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT  DISTINCT PRO_WorkOrderCompletion.WOCompletionNo,";
				strSql += " PRO_WorkOrderCompletion.PostDate,";
				strSql += " ITM_Product.Code AS ProductCode,";
				strSql += " ITM_Product.Revision as ProductModel,";
				strSql += " ITM_Product.Description AS ProductName,";
				strSql += " PRO_WorkOrderCompletion.CompletedQuantity,";
				strSql += " PRO_WorkOrderCompletion.Remark,";
				strSql += " PRO_IssuePurpose.Description as Purpose,";
				strSql += " PRO_Shift.ShiftDesc,";
				strSql += " MST_Location.Code as LocationCode,";
				strSql += " MST_Location.Name as LocationName,";
				strSql += " MST_UnitOfMeasure.Code AS UMCode,";
				strSql += " ITM_Category.Code AS CategoryCode,";
				strSql += " PRO_WorkOrderDetail.OrderQuantity as PlannedQty,";
				strSql += " PRO_WorkOrderDetail.Line as WOLine,";
				strSql += " PRO_WorkOrderMaster.WorkOrderNo";
				strSql += " FROM    PRO_WorkOrderCompletion";	
				strSql += " INNER JOIN MST_Location ON PRO_WorkOrderCompletion.LocationID = MST_Location.LocationID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON PRO_WorkOrderCompletion.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";
				strSql += " INNER JOIN PRO_WorkOrderDetail ON PRO_WorkOrderCompletion.WorkOrderDetailID = PRO_WorkOrderDetail.WorkOrderDetailID";
				strSql += " INNER JOIN PRO_WorkOrderMaster ON PRO_WorkOrderCompletion.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID";
				strSql += " INNER JOIN ITM_Product ON ITM_Product.ProductID = PRO_WorkOrderCompletion.ProductID";
				strSql += " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID";
				strSql += " LEFT JOIN PRO_IssuePurpose ON PRO_WorkOrderCompletion.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID";
				strSql += " LEFT JOIN PRO_Shift ON PRO_WorkOrderCompletion.ShiftID = PRO_Shift.ShiftID";
				
				strSql += " WHERE PRO_WorkOrderCompletion.WorkOrderCompletionID = " + pintWOCompletionId;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;				
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion		
		
		#region BOM Shortage Report: Tuan TQ
		
		private DataTable BuilBomShortageTemplate()
		{
			const string WO_LINE_FLD = "WorkOrderLine";
			const string PARENT_PART_NO_FLD = "ParentPartNumber";
			const string PARENT_PART_MODEL_FLD = "ParentPartModel";
			const string PARENT_PART_NAME_FLD = "ParentPartName";
			const string PARENT_STOCKUM_FLD = "ParentStockUM";			
			const string PART_NO_FLD = "PartNumber";
			const string PART_NAME_FLD = "PartName";
			const string PART_MODEL_FLD = "PartModel";
			const string STOCK_UM_FLD = "ChildStockUM";		
			const string NEED_QUANTITY_FLD = "NeedQuantity";
			const string AVAILABLE_QTY_FLD = "AvailableQuantity";
			const string SHORTAGE_QTY_FLD = "ShortageQuantity";
						
			DataTable dtbTemplate = new DataTable();
			dtbTemplate.Columns.Add(PRO_WorkOrderBomDetailTable.LINE_FLD, typeof(System.Int32));
			dtbTemplate.Columns.Add(PRO_WorkOrderMasterTable.WORKORDERNO_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(WO_LINE_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(PARENT_PART_NO_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(PARENT_PART_NAME_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(PARENT_PART_MODEL_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(PARENT_STOCKUM_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(ITM_ProductTable.PRODUCTID_FLD, typeof(System.Int32));
			dtbTemplate.Columns.Add(PART_NO_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(PART_NAME_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(PART_MODEL_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(STOCK_UM_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(NEED_QUANTITY_FLD, typeof(System.Decimal));
			dtbTemplate.Columns.Add(AVAILABLE_QTY_FLD, typeof(System.Decimal));
			dtbTemplate.Columns.Add(SHORTAGE_QTY_FLD, typeof(System.Decimal), NEED_QUANTITY_FLD + "-" + AVAILABLE_QTY_FLD);
			
			dtbTemplate.DefaultView.RowFilter = SHORTAGE_QTY_FLD + " > 0";
			return dtbTemplate;
		}

        /// <summary>
        /// Gets the multi bom shortage data.
        /// </summary>
        /// <param name="pintWODetailId">The pint WO detail id.</param>
        /// <returns></returns>
		public DataTable GetMultiBomShortageData(List<string> pintWODetailId)
		{
            const string METHOD_NAME = THIS + ".GetMultiBomShortageData()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;

			try 
			{
			    var sql = new StringBuilder();
                sql.AppendLine(" SELECT D.WorkOrderDetailID, C.Code Category, P.ProductID, P.Code, P.Description, P.Revision, U.Code UM, ");
                sql.AppendLine(" B.Quantity NeedQuantity, ISNULL(SUM(ISNULL(BC.OHQuantity,0)),0) OHQuantity, BC.BinID");
			    sql.AppendLine(" FROM PRO_WorkOrderDetail D JOIN ITM_BOM B ON D.ProductID = B.ProductID");
			    sql.AppendLine(" JOIN ITM_Product P ON B.ComponentID = P.ProductID");
			    sql.AppendLine(" JOIN MST_UnitOfMeasure U ON P.StockUMID = U.UnitOfMeasureID");
			    sql.AppendLine(" LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID");
			    sql.AppendLine(" LEFT JOIN IV_BinCache BC ON P.ProductID = BC.ProductID AND P.BinID = BC.BinID");
                sql.AppendLine(" LEFT JOIN MST_BIN BI ON BC.BinID = BI.BinID AND BI.BinTypeID = " + (int)BinTypeEnum.IN);
			    sql.AppendLine(string.Format(" WHERE D.WorkOrderDetailID IN ({0})", string.Join(",", pintWODetailId.ToArray())));
                sql.AppendLine(" GROUP BY D.WorkOrderDetailID, C.Code, P.ProductID, P.Code, P.Description, P.Revision, U.Code, B.Quantity, BC.BinID");
			    sql.AppendLine(" ORDER BY P.Code, P.Description, P.Revision");

				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(sql.ToString(), oconPCS);
				
				ocmdPCS.Connection.Open();				
				var odadPCS = new OleDbDataAdapter(ocmdPCS);
			    var resulTable = new DataTable();
                odadPCS.Fill(resulTable);

			    return resulTable;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

        public DataTable GetBOMShortageData(List<string> pintWODetailId, decimal pdecCompletedQty)
        {
            const string METHOD_NAME = THIS + ".GetBOMShortageData()";
            DataTable dtbResultTable = BuilBomShortageTemplate();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;

            try
            {
                string strSql = "SELECT  bom.Line,";
                strSql += " wo.WorkOrderNo,";
                strSql += " woi.Line as WorkOrderLine,";
                strSql += " parent.Code as ParentPartNumber,";
                strSql += " parent.Revision as ParentPartModel,";
                strSql += " parent.Description as ParentPartName,";
                strSql += " um.Code as ParentStockUM,";
                strSql += " child.ProductID as ProductID,";
                strSql += " child.Code AS PartNumber,";
                strSql += " child.Description AS PartName,";
                strSql += " child.Revision AS PartModel,";
                strSql += " childUM.Code as ChildStockUM,";

                strSql += " (" + pdecCompletedQty + " * bom.Quantity) as NeedQuantity,";

                strSql += " (";
                strSql += " SELECT ISNULL(SUM(b.OHQuantity), 0)";
                strSql += " FROM";
                strSql += " (";
                strSql += " SELECT";
                strSql += " (";
                strSql += " ISNULL(SUM(ISNULL(bc.OHQuantity, 0) - ISNULL(bc.CommitQuantity, 0)), 0)";

                strSql += " ) as OHQuantity";
                strSql += " , bc.ProductID";
                strSql += " , bc.BinID";
                strSql += " , bc.LocationID";

                strSql += " FROM IV_BinCache bc";
                strSql += " 		INNER JOIN  MST_BIN ON MST_BIN.BinID = bc.BinID";

                strSql += " WHERE MST_BIN.BinTypeID = " + (int)BinTypeEnum.IN;

                strSql += " GROUP BY bc.ProductID,";
                strSql += " 	bc.BinID,";
                strSql += " 	bc.LocationID,";
                strSql += " 	bc.CCNID,";
                strSql += " 	bc.MasterLocationID";
                strSql += " ) b";

                strSql += " WHERE b.ProductID = child.ProductID";
                strSql += " AND b.LocationID = proline.LocationID";

                strSql += " )  as AvailableQuantity";

                strSql += " FROM    PRO_WorkOrderDetail woi";
                strSql += " INNER JOIN ITM_Product parent ON woi.ProductID = parent.ProductID";
                strSql += " INNER JOIN MST_UnitOfMeasure um ON parent.StockUMID = um.UnitOfMeasureID";

                strSql += " INNER JOIN PRO_WorkOrderMaster wo ON woi.WorkOrderMasterID = wo.WorkOrderMasterID";
                strSql += " INNER JOIN PRO_ProductionLine proline ON proline.ProductionLineID = wo.ProductionLineID";
                strSql += " INNER JOIN ITM_BOM bom ON woi.ProductID = bom.ProductID";

                strSql += " INNER JOIN ITM_Product child ON bom.ComponentID = child.ProductID";
                strSql += " INNER JOIN MST_UnitOfMeasure childUM ON child.StockUMID = childUM.UnitOfMeasureID";
                var idList = string.Join(",", pintWODetailId.ToArray());
                strSql += " WHERE woi.WorkOrderDetailID IN (" + idList + ")";

                strSql += " ORDER BY bom.Line ASC";

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dtbResultTable);

                if (dtbResultTable != null)
                {
                    if (dtbResultTable.Rows.Count == 0)
                    {
                        strSql = "SELECT  wo.WorkOrderNo,";
                        strSql += " woi.Line as WorkOrderLine,";
                        strSql += " parent.Code as ParentPartNumber,";
                        strSql += " parent.Revision as ParentPartModel,";
                        strSql += " parent.Description as ParentPartName,";
                        strSql += " um.Code as ParentStockUM,";
                        strSql += " null as ProductID,";
                        strSql += " '' AS PartNumber,";
                        strSql += " '' AS PartName,";
                        strSql += " '' AS PartModel,";
                        strSql += " '' as ChildStockUM,";
                        strSql += " null as NeedQuantity,";
                        strSql += " null as CompletedQuantity,";
                        strSql += " null as ShortageQuantity,";
                        strSql += " null as AvailableQuantity";

                        strSql += " FROM    PRO_WorkOrderDetail woi";
                        strSql += " INNER JOIN ITM_Product parent ON woi.ProductID = parent.ProductID";
                        strSql += " INNER JOIN MST_UnitOfMeasure um ON parent.StockUMID = um.UnitOfMeasureID";
                        strSql += " INNER JOIN PRO_WorkOrderMaster wo ON woi.WorkOrderMasterID = wo.WorkOrderMasterID";

                        strSql += " WHERE woi.WorkOrderDetailID  = " + pintWODetailId;

                        ocmdPCS = new OleDbCommand(strSql, oconPCS);
                        odadPCS = new OleDbDataAdapter(ocmdPCS);
                        odadPCS.Fill(dtbResultTable);
                    }
                }

                return dtbResultTable;
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }
            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

		#endregion
		
		#region Delivery To Outsourcing Vendor Report: Tuan TQ
		
		/// <summary>
		/// Get Delivery To Outsourcing Vendor data
		/// </summary>
		/// <param name="pintWODetailId"></param>
		/// <returns></returns>
		/// <author> Tuan TQ, 29 Dec, 2005</author>
		public DataTable GetDelivery2OutsourcingData(int pintIssueMasterialMasterId, string pstrMaxRow)
		{			
			const string METHOD_NAME = THIS + ".GetDelivery2OutsourcingData()";

			DataTable dtbResultTable = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT  TOP " + pstrMaxRow;
				strSql += " 0 as RowIndex,";
				strSql += " IV_MiscellaneousIssueMaster.PostDate,";
				strSql += " IV_MiscellaneousIssueMaster.TransNo,";
				strSql += " PRO_IssuePurpose.Description as Purpose,";
				strSql += " Loc_From.Code as FromLoc_Code,";
				strSql += " Loc_From.Name as FromLoc_Name,";
				
				strSql += " Case ";
				strSql += " When IV_MiscellaneousIssueMaster.PartyID IS NULL then Loc_To.Code";
				strSql += " Else MST_Party.Code";
				strSql += " End as ToLoc_Code,";

				strSql += " Case ";
				strSql += " When IV_MiscellaneousIssueMaster.PartyID IS NULL then Loc_To.Name";
				strSql += " Else MST_Party.Name";
				strSql += " End as ToLoc_Name,";
				
				strSql += " ITM_Product.Code as ITM_ProductCode,";
				strSql += " ITM_Product.Description as ITM_ProductDescription,";
				strSql += " ITM_Product.Revision as ITM_ProductRevision,";
				strSql += " IV_MiscellaneousIssueDetail.Quantity,";
				strSql += " MST_UnitOfMeasure.Code as StockUM,";
				strSql += " ITM_Category.Code as CategoryCode";

				strSql += " FROM   IV_MiscellaneousIssueDetail";
				strSql += " 	INNER JOIN IV_MiscellaneousIssueMaster ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID";
				strSql += " 	INNER JOIN PRO_IssuePurpose ON PRO_IssuePurpose.IssuePurposeID = IV_MiscellaneousIssueMaster.IssuePurposeID";

				strSql += " 	INNER JOIN ITM_Product ON IV_MiscellaneousIssueDetail.ProductID = ITM_Product.ProductID";
				strSql += " 	INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";

				strSql += " 	INNER JOIN MST_Location Loc_From ON IV_MiscellaneousIssueMaster.SourceLocationID = Loc_From.LocationID";
  				strSql += " 	LEFT JOIN MST_Location Loc_To ON IV_MiscellaneousIssueMaster.DesLocationID = Loc_To.LocationID";
				strSql += " 	LEFT JOIN MST_Party ON MST_Party.PartyID = IV_MiscellaneousIssueMaster.PartyID";
				strSql += " 	LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID";

				strSql += " WHERE IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = " + pintIssueMasterialMasterId.ToString();
				strSql += " ORDER BY ITM_Category.Code, ITM_Product.Revision, ITM_Product.Code";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;				
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion

		#region Destroy Slip Report: Tuan TQ
		
		/// <summary>
		/// Get Delivery To Outsourcing Vendor data
		/// </summary>
		/// <param name="pintWODetailId"></param>
		/// <returns></returns>
		/// <author> Tuan TQ, 14 Mar, 2006</author>
		public DataTable GetDestroySlipData(int pintIssueMasterialMasterId, string pstrMaxRow)
		{			
			const string METHOD_NAME = THIS + ".GetDestroySlipData()";

			DataTable dtbResultTable = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT  TOP " + pstrMaxRow;
				strSql += " 0 as RowIndex,";
				strSql += " IV_MiscellaneousIssueMaster.TransNo,";
				strSql += " IV_MiscellaneousIssueMaster.PostDate,";
				strSql += " IV_MiscellaneousIssueMaster.Comment,";
				strSql += " Loc_From.Name + ' (' + Loc_From.Code + ')' as FromLocInfo,";
				strSql += " Bin_From.Name + ' (' + Bin_From.Code + ')' as FromBinInfo,";
				strSql += " Loc_To.Name + ' (' + Loc_To.Code + ')' as ToLocInfo,";
				strSql += " Bin_To.Name + ' (' + Bin_To.Code + ')' as ToBinInfo,";
				strSql += " ITM_Product.Code as ITM_ProductCode,";
				strSql += " ITM_Product.Description as ITM_ProductDescription,";
				strSql += " ITM_Product.Revision as ITM_ProductRevision,";
				strSql += " IV_MiscellaneousIssueDetail.Quantity,";
				strSql += " MST_UnitOfMeasure.Code as StockUM,";
				strSql += " ITM_Category.Code as ITM_CategoryCode,";
				strSql += " MST_Party.Code as MST_PartyCode";

				strSql += " FROM   IV_MiscellaneousIssueDetail";
				strSql += " INNER JOIN IV_MiscellaneousIssueMaster ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID";

				strSql += " INNER JOIN ITM_Product ON IV_MiscellaneousIssueDetail.ProductID = ITM_Product.ProductID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";

				strSql += " INNER JOIN MST_Location Loc_From ON IV_MiscellaneousIssueMaster.SourceLocationID = Loc_From.LocationID";
				strSql += " INNER JOIN MST_BIN Bin_From ON IV_MiscellaneousIssueMaster.SourceBinID = Bin_From.BinID";

				strSql += " LEFT JOIN MST_Location Loc_To ON IV_MiscellaneousIssueMaster.DesLocationID = Loc_To.LocationID";
				strSql += " LEFT JOIN MST_BIN Bin_To ON IV_MiscellaneousIssueMaster.DesBinID = Bin_To.BinID";

				strSql += " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID";
				strSql += " LEFT JOIN MST_Party ON MST_Party.PartyID = ITM_Product.PrimaryVendorID";
				
				strSql += " WHERE IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID = " + pintIssueMasterialMasterId.ToString();
				strSql += " ORDER BY ITM_Category.Code, ITM_Product.Revision, ITM_Product.Code";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;				
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion

		#region PO Summary Report: Tuan TQ

		const string PO_SUMMARY_TABLE = "POSummaryTable";

		private DataTable BuildPOSummaryTable()
		{
			#region Field name constants

			const string PO_NO_FLD = "PurchaseOrderNo";
			const string ORDER_DATE_FLD = "OrderDate";
			const string PRODUCT_ID_FLD = "ProductID";
			const string CUSTOMS_CODE_FLD = "CustomsCode";
			const string PART_NUMBER_FLD = "PartNumber";
			const string PART_NAME_FLD = "PartName";
			const string PART_MODEL_FLD = "PartModel";
			const string UNIT_PRICE_FLD = "UnitPrice";
			const string CURRENCY_CODE_FLD = "CurrencyCode";
			const string CATEGORY_CODE_FLD = "CategoryCode";
			const string QUANTITY_SET_FLD = "QuantitySet";

			const string ORDER_QUANTITY_FLD = "OrderQuantity";
			const string ORDER_QUANTITY_SET_FLD = "OrderQuantitySet";

			const string NEXT_1_MONTH_QUANTITY_FLD = "Next1MonthQuantity";
			const string NEXT_2_MONTHS_QUANTITY_FLD = "Next2MonthsQuantity";

			const string NEXT_1_MONTH_QUANTITY_SET_FLD = "Next1MonthQuantitySet";
			const string NEXT_2_MONTHS_QUANTITY_SET_FLD = "Next2MonthsQuantitySet";
						
			#endregion

			DataTable dtbTemplate = new DataTable(PO_SUMMARY_TABLE);
			//Add columns
			dtbTemplate.Columns.Add(PO_NO_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(ORDER_DATE_FLD, typeof(System.DateTime));

			dtbTemplate.Columns.Add(PRODUCT_ID_FLD, typeof(System.Int32));
			dtbTemplate.Columns.Add(CUSTOMS_CODE_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(PART_NUMBER_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(PART_NAME_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(PART_MODEL_FLD, typeof(System.String));

			dtbTemplate.Columns.Add(UNIT_PRICE_FLD, typeof(System.Double));
			dtbTemplate.Columns.Add(CURRENCY_CODE_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(CATEGORY_CODE_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(QUANTITY_SET_FLD, typeof(System.Double));

			dtbTemplate.Columns.Add(ORDER_QUANTITY_FLD, typeof(System.Double));
			dtbTemplate.Columns.Add(NEXT_1_MONTH_QUANTITY_FLD, typeof(System.Double));
			dtbTemplate.Columns.Add(NEXT_2_MONTHS_QUANTITY_FLD, typeof(System.Double));
			
			DataColumn dcolOrderQtySet = new DataColumn(ORDER_QUANTITY_SET_FLD, typeof(System.Double), ORDER_QUANTITY_FLD + @"/" + QUANTITY_SET_FLD);
			DataColumn dcolNext1QtySet = new DataColumn(NEXT_1_MONTH_QUANTITY_SET_FLD, typeof(System.Double), NEXT_1_MONTH_QUANTITY_FLD + @"/" + QUANTITY_SET_FLD);
			DataColumn dcolNext2QtySet = new DataColumn(NEXT_2_MONTHS_QUANTITY_SET_FLD, typeof(System.Double), NEXT_2_MONTHS_QUANTITY_FLD + @"/" + QUANTITY_SET_FLD);
            
			dtbTemplate.Columns.Add(dcolOrderQtySet);
			dtbTemplate.Columns.Add(dcolNext1QtySet);
			dtbTemplate.Columns.Add(dcolNext2QtySet);			

			return dtbTemplate;
			

		}
		/// <summary>
		/// PO Summary Report data
		/// </summary>
		/// <param name="pintWODetailId"></param>
		/// <returns></returns>
		/// <author> Tuan TQ, 30 Dec, 2005</author>
		public DataTable GetPOSummaryData(int pintPOId, DateTime pdtmOrderDate)
		{			
			const string METHOD_NAME = THIS + ".GetPOSummaryData()";
			const string SQL_DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

			string strOrderDate = (new DateTime(pdtmOrderDate.Year, pdtmOrderDate.Month, 1)).ToString(SQL_DATETIME_FORMAT);

			DataTable dtbResultTable = BuildPOSummaryTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT  po.Code as PurchaseOrderNo,";
				strSql += " po.OrderDate,";
				strSql += " product.ProductID,";
				strSql += " product.OtherInfo1 as CustomsCode,";
				strSql += " product.Code AS PartNumber,";
				strSql += " product.Description as PartName,";
				strSql += " product.Revision as PartModel,";
				strSql += " PO_PurchaseOrderDetail.UnitPrice,";
				strSql += " MST_Currency.Code AS CurrencyCode,"; 
				strSql += " ITM_Category.Code AS CategoryCode,";
				strSql += " Case ";
				strSql += " When product.QuantitySet IS NULL Then 1";
				strSql += " When product.QuantitySet = 0 then 1";
				strSql += " Else product.QuantitySet";
				strSql += " End as QuantitySet,";
				strSql += " PO_PurchaseOrderDetail.OrderQuantity,";

				strSql += " (";
				strSql += " SELECT SUM(PO_PurchaseOrderDetail.OrderQuantity)";
				strSql += " FROM PO_PurchaseOrderDetail";
				strSql += " INNER JOIN PO_PurchaseOrderMaster ON PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID";
				strSql += " WHERE PO_PurchaseOrderMaster.OrderDate BETWEEN DateAdd(month, 1, '" + strOrderDate + "') AND DateAdd(second, -1, DateAdd(month, 2, '" + strOrderDate + "'))";
				strSql += " AND PO_PurchaseOrderMaster.PartyID = po.PartyID";
				strSql += " AND PO_PurchaseOrderDetail.ProductID = product.ProductID";
				strSql += " ) as Next1MonthQuantity,";

				strSql += " (";
				strSql += " SELECT SUM(PO_PurchaseOrderDetail.OrderQuantity)";
				strSql += " FROM PO_PurchaseOrderDetail";
				strSql += " INNER JOIN PO_PurchaseOrderMaster ON PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID";
				strSql += " WHERE PO_PurchaseOrderMaster.OrderDate BETWEEN DateAdd(month, 2, '" + strOrderDate + "') AND DateAdd(second, -1, DateAdd(month, 3, '" + strOrderDate + "'))";
				strSql += " AND PO_PurchaseOrderMaster.PartyID = po.PartyID";
				strSql += " AND PO_PurchaseOrderDetail.ProductID = product.ProductID";
				strSql += " ) as Next2MonthsQuantity";

				strSql += " FROM    PO_PurchaseOrderMaster po";
				strSql += " INNER JOIN MST_Currency ON po.CurrencyID = MST_Currency.CurrencyID";
				strSql += " INNER JOIN PO_PurchaseOrderDetail ON po.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID";
				strSql += " INNER JOIN ITM_Product product ON PO_PurchaseOrderDetail.ProductID = product.ProductID";
				strSql += " LEFT JOIN ITM_Category ON product.CategoryID = ITM_Category.CategoryID";
				
				strSql += " WHERE po.PurchaseOrderMasterID =" + pintPOId.ToString();

				strSql += " ORDER BY PO_PurchaseOrderDetail.Line";

				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;				
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion

		#region PO Bom Shortage Report: Tuan TQ

		const string PO_BOM_SHORTAGE_TABLE = "POBOMShortageTable";

		private DataTable BuildPOBOMShortageTable()
		{
			#region Field name constants
			
			const string PRODUCT_ID_FLD = "ProductID";
			const string COMPONENT_NUMBER_FLD = "ComponentNo";
			const string COMPONENT_NAME_FLD = "ComponentName";
			const string COMPONENT_MODEL_FLD = "ComponentModel";
			const string STOCK_UM_FLD = "StockUM";
			
			const string BOM_QUANTITY_FLD = "BOMQuantity";
			const string RECEIVE_QUANTITY_FLD = "ReceiveQuantity";
			const string NEEDED_QUANTITY_FLD = "NeededQuantity";
			const string SHORTAGE_QUANTITY_FLD = "ShortageQuantity";
			const string AVAILABLE_QUANTITY_FLD = "AvailableQuantity";

			#endregion

			DataTable dtbTemplate = new DataTable(PO_BOM_SHORTAGE_TABLE);
			//Add columns			

			dtbTemplate.Columns.Add(PRODUCT_ID_FLD, typeof(System.Int32));			
			dtbTemplate.Columns.Add(COMPONENT_NUMBER_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(COMPONENT_NAME_FLD, typeof(System.String));
			dtbTemplate.Columns.Add(COMPONENT_MODEL_FLD, typeof(System.String));			
			dtbTemplate.Columns.Add(STOCK_UM_FLD, typeof(System.String));			

			dtbTemplate.Columns.Add(BOM_QUANTITY_FLD, typeof(System.Double));
			dtbTemplate.Columns.Add(RECEIVE_QUANTITY_FLD, typeof(System.Double));
			dtbTemplate.Columns.Add(NEEDED_QUANTITY_FLD, typeof(System.Double), BOM_QUANTITY_FLD + "*" + RECEIVE_QUANTITY_FLD);			
			dtbTemplate.Columns.Add(AVAILABLE_QUANTITY_FLD, typeof(System.Double));
			dtbTemplate.Columns.Add(SHORTAGE_QUANTITY_FLD, typeof(System.Double), NEEDED_QUANTITY_FLD + "-" + AVAILABLE_QUANTITY_FLD);

			return dtbTemplate;
		}

		/// <summary>
		/// PO BOM Shortage Report data
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ, 06 Apr, 2006</author>
		public DataTable GetPOBOMShortageData(DateTime pdtmPostDate, int pintProductionLineID, string pstrPONo, string pstrProductIDList)
		{			
			const string METHOD_NAME = THIS + ".GetPOSummaryData()";
			
			DataTable dtbResultTable = BuildPOBOMShortageTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT  DISTINCT D.PurchaseOrderDetailID, B.ComponentID as ProductID,"
					+ " C.Code as ComponentCode, C.Description as ComponentName, C.Revision as ComponentModel,"
					+ " um.Code as StockUM, B.Quantity as BOMQuantity, BC.OHQuantity AS AvailableQuantity"
					+ " FROM    PO_PurchaseOrderMaster M"
					+ " INNER JOIN PO_PurchaseOrderDetail D ON M.PurchaseOrderMasterID = D.PurchaseOrderMasterID  "
					+ " INNER JOIN ITM_Product P ON D.ProductID = P.ProductID  "
					+ " INNER JOIN ITM_BOM B ON P.ProductID = B.ProductID  "
					+ " INNER JOIN ITM_Product C ON C.ProductID = B.ComponentID  "
					+ " INNER JOIN MST_UnitOfMeasure um ON C.StockUMID = um.UnitOfMeasureID"
					+ " INNER JOIN PRO_ProductionLine PL ON P.ProductionLineID = PL.ProductionLineID"
					+ " INNER JOIN IV_BinCache BC ON PL.LocationID = BC.LocationID AND BC.ProductID = C.ProductID"
					+ " INNER JOIN MST_BIN Bin ON BC.BinID = Bin.BinID AND Bin.BinTypeID = " + (int)BinTypeEnum.IN
					+ " WHERE M.Code = ?"
					+ " AND PL.ProductionLineID = " + pintProductionLineID;
				if (pstrProductIDList.Length > 0)
					strSql += " AND D.ProductID IN (" + pstrProductIDList + ")";
                strSql += " ORDER BY C.Code";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("POCode", OleDbType.VarWChar)).Value = pstrPONo;
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion
		
		#region PO Invoice Report: Tuan TQ		
		
		/// <summary>
		/// PO Invoice Report data
		/// </summary>
		/// <param name="pintInvoiceMasterId">Invoice ID</param>
		/// <returns>Data table</returns>
		/// <author> Tuan TQ, 05 Jan, 2006</author>
		public DataTable GetPOInvoiceData(int pintInvoiceMasterId)
		{			
			const string METHOD_NAME = THIS + ".GetPOInvoiceData()";
						
			DataTable dtbResultTable = new DataTable();
			OleDbConnection oconPCS = null;
			

			try 
			{
				string strSql = "SELECT  PO_InvoiceMaster.InvoiceNo,";
				strSql += " PO_InvoiceMaster.PostDate,";
				strSql += " PO_InvoiceMaster.ExchangeRate,";
				strSql += " PO_InvoiceMaster.BLDate,";
				strSql += " PO_InvoiceMaster.InformDate,";
				strSql += " PO_InvoiceMaster.DeclarationDate,";
				strSql += " PO_InvoiceMaster.BLNumber,";
				strSql += " PO_InvoiceMaster.TaxInformNumber,";
				strSql += " PO_InvoiceMaster.TaxDeclarationNumber,";
				strSql += " PO_InvoiceMaster.TotalInlandAmount,";
				strSql += " PO_InvoiceMaster.TotalCIPAmount,";
				strSql += " PO_InvoiceMaster.TotalCIFAmount,";
				strSql += " PO_InvoiceMaster.TotalImportTax,";
				strSql += " PO_InvoiceMaster.TotalBeforeVATAmount,";
				strSql += " PO_InvoiceMaster.TotalVATAmount,";
				strSql += " MST_Party.Code + ' (' + MST_Party.Name + ')' as PartyCodeName,";
				strSql += " MST_DeliveryTerm.Code AS DeliveryTermCode,";
				strSql += " MST_Currency.Code AS CurrencyCode,";
				strSql += " MST_Carrier.Code AS CarrierCode,";
				strSql += " MST_PaymentTerm.Code AS PaymentTermCode,";
				strSql += " PO_PurchaseOrderMaster.Code AS PONo,";
				strSql += " PO_PurchaseOrderDetail.Line as POLine,";
				strSql += " ITM_Product.Code as PartNumber,";
				strSql += " ITM_Product.Revision as PartModel,";
				strSql += " ITM_Product.Description as PartName,";
				strSql += " ITM_Product.OtherInfo1 as CustomsCode,";
				strSql += " ITM_Product.PartNameVN,";
				strSql += " ITM_Product.TaxCode,";
				strSql += " PO_DeliverySchedule.DeliveryLine,";
				strSql += " MST_UnitOfMeasure.Code AS UMCode,";
				strSql += " PO_InvoiceDetail.InvoiceLine,";
				strSql += " PO_InvoiceDetail.InvoiceQuantity,";
				strSql += " PO_InvoiceDetail.UnitPrice,";
				strSql += " PO_InvoiceDetail.VAT,";
				strSql += " PO_InvoiceDetail.VATAmount,";
				strSql += " PO_InvoiceDetail.ImportTax,";
				strSql += " PO_InvoiceDetail.ImportTaxAmount,";
				strSql += " PO_InvoiceDetail.Inland,";
				strSql += " PO_InvoiceDetail.BeforeVATAmount,";
				strSql += " PO_InvoiceDetail.CIFAmount,";
				strSql += " PO_InvoiceDetail.CIPAmount,";
				strSql += " PO_InvoiceDetail.Note";

				strSql += " FROM    PO_InvoiceMaster";
				strSql += " INNER JOIN PO_InvoiceDetail ON PO_InvoiceDetail.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID";
				strSql += " INNER JOIN PO_PurchaseOrderMaster ON PO_InvoiceDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID";
				strSql += " INNER JOIN ITM_Product ON PO_InvoiceDetail.ProductID = ITM_Product.ProductID";
				strSql += " INNER JOIN MST_Currency ON PO_InvoiceMaster.CurrencyID = MST_Currency.CurrencyID";
				strSql += " INNER JOIN PO_PurchaseOrderDetail ON PO_InvoiceDetail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID";
				strSql += " INNER JOIN MST_Party ON PO_InvoiceMaster.PartyID = MST_Party.PartyID";
				strSql += " LEFT JOIN MST_Carrier ON PO_InvoiceMaster.CarrierID = MST_Carrier.CarrierID";
				strSql += " LEFT JOIN MST_PaymentTerm ON PO_InvoiceMaster.PaymentTermID = MST_PaymentTerm.PaymentTermID";
				strSql += " LEFT JOIN MST_DeliveryTerm ON PO_InvoiceMaster.DeliveryTermID = MST_DeliveryTerm.DeliveryTermID";
				strSql += " LEFT JOIN PO_DeliverySchedule ON PO_InvoiceDetail.DeliveryScheduleID = PO_DeliverySchedule.DeliveryScheduleID";
				strSql += " LEFT JOIN MST_UnitOfMeasure ON PO_InvoiceDetail.InvoiceUMID = MST_UnitOfMeasure.UnitOfMeasureID";

				strSql += " WHERE PO_InvoiceMaster.InvoiceMasterID = " + pintInvoiceMasterId.ToString();

				strSql += " ORDER BY PO_InvoiceDetail.InvoiceLine ASC";
				
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;				
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion

		#region In-Out Stock Report: DungLA
		
		/// <summary>
		/// Get In-Out Stock Info for selecting (in In-Out Stock Report)
		/// </summary>		
		/// <returns></returns>
		/// <author> Tuan TQ. 16 Jan, 2006</author>
		public DataTable GetInOutStockData(string pstrCCNID, 
			string pstrMasterLocID, 
			string pstrLocationID, 
			string pstrBinType, 
			string pstrBinID, 
			DateTime pdtmFromDate, 
			DateTime pdtmToDate, 
			string pstrCategoryID, 
			string pstrProductSourceID, 
			string pstrModel, string pstrProductID)
		{									
			OleDbConnection oconPCS = null;
			
			const string TABLE_NAME = "InOutStockReport";

			string strFormat = "yyyy-MM-dd HH:mm:ss";
			
			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			DateTime dtmBeginMonth = new DateTime(pdtmFromDate.Year, pdtmFromDate.Month, 1);
			DateTime dtmLastMonth = dtmBeginMonth.AddMonths(-1);

			DataTable dtbResult = new DataTable(TABLE_NAME);

			strSql =  "SELECT  DISTINCT";
			strSql += " MST_Location.Code as LocationCode,";
			strSql += " enm_BINType.Name as BinTypeName,";
			strSql += " MST_Bin.Code as BinCode,";
			strSql += " ITM_Category.Code as CategoryCode,";
			strSql += " ITM_Product.Code as PartNumber,";
			strSql += " ITM_Product.Description as PartName,";
			strSql += " ITM_Product.Revision as PartModel,";
			strSql += " MST_UnitOfMeasure.Code as StockUM,";
			strSql += " ITM_Source.Code as SourceCode,";

			strSql += " (ISNULL(IV_BalanceBin.OHQuantity,0) + ( SELECT ISNULL(SUM(TransQuantity), 0)";
			strSql += " 	FROM v_TransactionHistory";
			strSql += " 		INNER JOIN MST_TranType  ";
			strSql += " 			ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID 	 ";
			strSql += " 	WHERE ProductID = ITM_Product.ProductID 		 ";
			strSql += " 		AND BinID = IV_BinCache.BinID 		 ";
			strSql += " 		AND PostDate >=  '" + dtmBeginMonth.ToString(strFormat) + "'";
			strSql += " 		AND PostDate < '" + pdtmFromDate.ToString(strFormat) + "'";
			strSql += " 		AND (MST_TranType.Type = 1 OR MST_TranType.Type = 2))  ";
			strSql += " - ( SELECT ISNULL(SUM(TransQuantity), 0)  ";
			strSql += " 	FROM v_TransactionHistory		      ";
			strSql += " 		INNER JOIN MST_TranType  ";
			strSql += " 			ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID  ";
			strSql += " 	WHERE ProductID = ITM_Product.ProductID 	 ";
			strSql += " 		AND BinID = IV_BINCache.BinID 	 ";
			strSql += " 		AND PostDate >=  '" + dtmBeginMonth.ToString(strFormat) + "'";
			strSql += " 		AND PostDate < '" + pdtmFromDate.ToString(strFormat) + "'";
			strSql += " AND MST_TranType.Type = 0 	) ) as BeginStock,";

			strSql += " (SELECT ISNULL(SUM(TransQuantity), 0)";
			strSql += " FROM v_TransactionHistory";
			strSql += " INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
			strSql += " WHERE ProductID = ITM_Product.ProductID";
			strSql += " AND TransQuantity > 0";
			strSql += " AND BinID = IV_BinCache.BinID";
			strSql += " AND (PostDate BETWEEN '" + pdtmFromDate.ToString(strFormat) + "' AND '" + pdtmToDate.ToString(strFormat) + "')";
			strSql += " AND (MST_TranType.Type = " + (int)TransactionHistoryType.In + " OR MST_TranType.Type = " + (int)TransactionHistoryType.Both + ")";
			strSql += " )";
			strSql += " as InQuantity,";
			
			strSql += " (";
			strSql += " (SELECT ISNULL(SUM(TransQuantity), 0)";
			strSql += " FROM v_TransactionHistory";
			strSql += " INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
			strSql += " WHERE ProductID = ITM_Product.ProductID";
			strSql += " AND BinID = IV_BinCache.BinID";
			strSql += " AND (PostDate BETWEEN '" + pdtmFromDate.ToString(strFormat) + "' AND '" + pdtmToDate.ToString(strFormat) + "')";
			strSql += " AND MST_TranType.Type = " + (int)TransactionHistoryType.Out;
			strSql += "   )";
			strSql += " - ";
			strSql += " (SELECT ISNULL(SUM(TransQuantity), 0)";
			strSql += " FROM v_TransactionHistory";
			strSql += " INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
			strSql += " WHERE ProductID = ITM_Product.ProductID";
			strSql += " AND TransQuantity < 0";
			strSql += " AND BinID = IV_BinCache.BinID";
			strSql += " AND (PostDate BETWEEN '" + pdtmFromDate.ToString(strFormat) + "' AND '" + pdtmToDate.ToString(strFormat) + "')";
			strSql += " AND (MST_TranType.Type = " + (int)TransactionHistoryType.In + " OR MST_TranType.Type = " + (int)TransactionHistoryType.Both + ")";
			strSql += " )";
			strSql += " )";
			strSql += " as OutQuantity,";
			
			strSql += " ((ISNULL(IV_BalanceBin.OHQuantity,0) + ( SELECT ISNULL(SUM(TransQuantity), 0)";
			strSql += " 	FROM v_TransactionHistory";
			strSql += " 		INNER JOIN MST_TranType  ";
			strSql += " 			ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID 	 ";
			strSql += " 	WHERE ProductID = ITM_Product.ProductID 		 ";
			strSql += " 		AND BinID = IV_BinCache.BinID 		 ";
			strSql += " 		AND PostDate >=  '" + dtmBeginMonth.ToString(strFormat) + "'";
			strSql += " 		AND PostDate < '" + pdtmFromDate.ToString(strFormat) + "'";
			strSql += " 		AND (MST_TranType.Type = 1 OR MST_TranType.Type = 2))  ";
			strSql += " - ( SELECT ISNULL(SUM(TransQuantity), 0)  ";
			strSql += " 	FROM v_TransactionHistory		      ";
			strSql += " 		INNER JOIN MST_TranType  ";
			strSql += " 			ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID  ";
			strSql += " 	WHERE ProductID = ITM_Product.ProductID 	 ";
			strSql += " 		AND BinID = IV_BINCache.BinID 	 ";
			strSql += " 		AND PostDate >=  '" + dtmBeginMonth.ToString(strFormat) + "'";
			strSql += " 		AND PostDate < '" + pdtmFromDate.ToString(strFormat) + "'";
			strSql += " AND MST_TranType.Type = 0 	) ) ";
			strSql += " + ( SELECT ISNULL(SUM(TransQuantity), 0)      ";
			strSql += " 	FROM v_TransactionHistory		      ";
			strSql += " 		INNER JOIN MST_TranType  ";
			strSql += " 			ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID 	 ";
			strSql += " 	WHERE ProductID = ITM_Product.ProductID 		 ";
			strSql += " 		AND BinID = IV_BINCache.BinID 		 ";
			strSql += " 		AND PostDate >=  '" + pdtmFromDate.ToString(strFormat) + "'";
			strSql += " 		AND PostDate <= '" + pdtmToDate.ToString(strFormat) + "'";
			strSql += " 		AND (MST_TranType.Type = 1 OR MST_TranType.Type = 2))  ";
			strSql += " - ( SELECT ISNULL(SUM(TransQuantity), 0)  ";
			strSql += " 	FROM v_TransactionHistory		      ";
			strSql += " 		INNER JOIN MST_TranType  ";
			strSql += " 			ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID  ";
			strSql += " 	WHERE ProductID = ITM_Product.ProductID 	 ";
			strSql += " 		AND BinID = IV_BINCache.BinID 		 ";
			strSql += " 		AND PostDate >=  '" + pdtmFromDate.ToString(strFormat) + "'";
			strSql += " 		AND PostDate <= '" + pdtmToDate.ToString(strFormat) + "'";
			strSql += " AND MST_TranType.Type = 0 	) ) as EndStock";

			strSql += " FROM ITM_Product";
			strSql += " INNER JOIN IV_BinCache ON IV_BinCache.ProductID = ITM_Product.ProductID";
			strSql += " LEFT JOIN IV_BalanceBin ON ITM_Product.ProductID = IV_BalanceBin.ProductID";
			strSql += " AND IV_BinCache.BinID = IV_BalanceBin.BinID";
			strSql += " AND DATEPART(year, IV_BalanceBin.EffectDate) = " + dtmLastMonth.Year;
			strSql += " AND DATEPART(month, IV_BalanceBin.EffectDate) = " + dtmLastMonth.Month;
			strSql += " INNER JOIN MST_UnitOfMeasure ON MST_UnitOfMeasure.UnitOfMeasureID = ITM_Product.StockUMID";
			strSql += " INNER JOIN MST_BIN ON MST_BIN.BinID = IV_BinCache.BinID";
			strSql += " INNER JOIN MST_Location ON MST_BIN.LocationID = MST_Location.LocationID";
			strSql += " INNER JOIN enm_BINType ON enm_BINType.BinTypeID = MST_BIN.BinTypeID";

			strSql += " LEFT JOIN ITM_Source ON ITM_Product.SourceID = ITM_Source.SourceID";
			strSql += " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID";

			strSql += " WHERE ITM_Product.CCNID = " + pstrCCNID;
			strSql += " AND MST_Location.LocationID IN (" + pstrLocationID + ")";
			
			if(pstrBinType != null && pstrBinType != string.Empty)
				strSql += " AND MST_BIN.BinTypeID IN (" + pstrBinType + ")";

			if(pstrBinID != null && pstrBinID != string.Empty)
				strSql += " AND MST_BIN.BinID = " + pstrBinID;
			
			if(pstrCategoryID != null && pstrCategoryID != string.Empty)
				strSql += " AND ITM_Category.CategoryID IN (" + pstrCategoryID + ")";

			if(pstrModel != null && pstrModel != string.Empty)
				strSql += " AND ITM_Product.Revision IN (" + pstrModel + ")";

			if(pstrProductSourceID != null && pstrProductSourceID != string.Empty)
				strSql += " AND ITM_Source.SourceID = " + pstrProductSourceID;

			if(pstrProductID != null && pstrProductID != string.Empty)
				strSql += " AND ITM_Product.ProductID IN (" + pstrProductID + ")";

			strSql += " GROUP BY MST_Location.Code,";
			strSql += " ITM_Category.Code,";
			strSql += " MST_UnitOfMeasure.Code,";
			strSql += " ITM_Product.Code,";
			strSql += " ITM_Product.Revision,";
			strSql += " ITM_Product.Description,";
			strSql += " ITM_Source.Code,";
			strSql += " MST_Bin.Code,";
			strSql += " IV_BinCache.BinID,";
			strSql += " ITM_Product.ProductID,";
			strSql += " IV_BalanceBin.OHQuantity,";
			strSql += " enm_BINType.Name";

			//HACK: added by Tuan TQ: Only show rows which all have none-zero value
			string strNewSQL = " SELECT a.* FROM (" + strSql + ") a ";
			strNewSQL += " WHERE 	a.BeginStock > 0 ";
			strNewSQL += " OR a.EndStock <> 0 ";
			strNewSQL += " OR a.InQuantity <> 0 "; 
			strNewSQL += " OR a.OutQuantity <> 0";

			strNewSQL += " ORDER BY a.LocationCode, a.BinTypeName, a.BinCode, a.CategoryCode, a.PartNumber ASC";
			//End hack

			Debug.WriteLine(strNewSQL);
			ocmdPCS = new OleDbCommand(strNewSQL, oconPCS);
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);			

			if(dtbResult != null)
			{					
				return dtbResult;
			}
			else
			{				
				return new DataTable(TABLE_NAME);
			}			
		}

		#endregion
		
		#region In-Out Stock Report: Tuan TQ		
		
		/// <summary>
		/// Get In-Out Stock Info for selecting (in In-Out Stock Report)
		/// </summary>		
		/// <returns></returns>
		/// <author> Tuan TQ. 16 Jan, 2006</author>
		public DataTable GetInOutStockData(string pstrCCNID, 
			string pstrMasterLocID, 
			string pstrLocationID, 
			string pstrBinType, 
			string pstrBinID, 
			string pstrFromDate, 
			string pstrToDate, 
			string pstrCategoryID, 
			string pstrProductSourceID, 
			string pstrModel,
			string pstrProductID)
		{									
			OleDbConnection oconPCS = null;
			
			const string TABLE_NAME = "InOutStockReport";

			const int    SQL_DATE_LENGTH = 10;
			const string END_TIME_OF_DAY = " 23:59:59";
			const string START_TIME_OF_DAY = " 00:00:00";		
			
			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;			

			//Processing data
			//HACK: SonHT request: datetime parameters have time part
			//pstrFromDate = pstrFromDate.Substring(0, SQL_DATE_LENGTH) + START_TIME_OF_DAY;
			//pstrToDate = pstrToDate.Substring(0, SQL_DATE_LENGTH) + END_TIME_OF_DAY;
			
			DataTable dtbResult = new DataTable(TABLE_NAME);

			strSql =  "SELECT  DISTINCT";
			strSql += " MST_Location.Code as LocationCode,";
			strSql += " enm_BINType.Name as BinTypeName,";
			strSql += " MST_Bin.Code as BinCode,";
			strSql += " ITM_Category.Code as CategoryCode,";
			strSql += " ITM_Product.Code as PartNumber,";
			strSql += " ITM_Product.Description as PartName,";
			strSql += " ITM_Product.Revision as PartModel,";
			strSql += " MST_UnitOfMeasure.Code as StockUM,";
			strSql += " ITM_Source.Code as SourceCode,";

			strSql += " (IV_BinCache.OHQuantity";
			strSql += " - ( SELECT ISNULL(SUM(TransQuantity), 0)";
			strSql += " FROM v_TransactionHistory";
			strSql += " INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
			strSql += " WHERE ProductID = IV_BinCache.ProductID";
			strSql += " AND BinID = IV_BinCache.BinID";
			strSql += " AND PostDate >= '" + pstrFromDate + "'";
			strSql += " AND (MST_TranType.Type = " + (int)TransactionHistoryType.In + " OR MST_TranType.Type = " + (int)TransactionHistoryType.Both + ")";
			strSql += " )";
			strSql += " + ( SELECT ISNULL(SUM(TransQuantity), 0)";
			strSql += " FROM v_TransactionHistory";
			strSql += " INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
			strSql += " WHERE ProductID = IV_BinCache.ProductID";
			strSql += " AND BinID = IV_BinCache.BinID";
			strSql += " AND PostDate >= '" + pstrFromDate + "'";
			strSql += " AND MST_TranType.Type = " + (int)TransactionHistoryType.Out;
			strSql += " )";
			strSql += " ) as BeginStock,";

			strSql += " (SELECT ISNULL(SUM(TransQuantity), 0)";
			strSql += " FROM v_TransactionHistory";
			strSql += " INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
			strSql += " WHERE ProductID = IV_BinCache.ProductID";
			strSql += " AND TransQuantity > 0";
			strSql += " AND BinID = IV_BinCache.BinID";
			strSql += " AND (PostDate BETWEEN '" + pstrFromDate + "' AND '" + pstrToDate + "')";
			strSql += " AND (MST_TranType.Type = " + (int)TransactionHistoryType.In + " OR MST_TranType.Type = " + (int)TransactionHistoryType.Both + ")";
			strSql += " )";
			strSql += " as InQuantity,";
			
			strSql += " (";
			strSql += " (SELECT ISNULL(SUM(TransQuantity), 0)";
			strSql += " FROM v_TransactionHistory";
			strSql += " INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
			strSql += " WHERE ProductID = IV_BinCache.ProductID";
			strSql += " AND BinID = IV_BinCache.BinID";
			strSql += " AND (PostDate BETWEEN '" + pstrFromDate + "' AND '" + pstrToDate + "')";
			strSql += " AND MST_TranType.Type = " + (int)TransactionHistoryType.Out;
			strSql += "   )";
			strSql += " - ";
			strSql += " (SELECT ISNULL(SUM(TransQuantity), 0)";
			strSql += " FROM v_TransactionHistory";
			strSql += " INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
			strSql += " WHERE ProductID = IV_BinCache.ProductID";
			strSql += " AND TransQuantity < 0";
			strSql += " AND BinID = IV_BinCache.BinID";
			strSql += " AND (PostDate BETWEEN '" + pstrFromDate + "' AND '" + pstrToDate + "')";
			strSql += " AND (MST_TranType.Type = " + (int)TransactionHistoryType.In + " OR MST_TranType.Type = " + (int)TransactionHistoryType.Both + ")";
			strSql += " )";
			strSql += " )";
			strSql += " as OutQuantity,";
			
			strSql += " (";
			strSql += " IV_BinCache.OHQuantity";
			strSql += " - ( SELECT ISNULL(SUM(TransQuantity), 0)";
			strSql += " FROM v_TransactionHistory";
			strSql += " INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
			strSql += " WHERE ProductID = IV_BinCache.ProductID";
			strSql += " AND BinID = IV_BinCache.BinID";
			strSql += " AND PostDate > '" + pstrToDate + "'";
			strSql += " AND (MST_TranType.Type = " + (int)TransactionHistoryType.In + " OR MST_TranType.Type = " + (int)TransactionHistoryType.Both + ")";
			strSql += " )";
			strSql += " + ( SELECT ISNULL(SUM(TransQuantity), 0)";
			strSql += " FROM v_TransactionHistory";
			strSql += " INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
			strSql += " WHERE ProductID = IV_BinCache.ProductID";
			strSql += " AND BinID = IV_BinCache.BinID";
			strSql += " AND PostDate > '" + pstrToDate + "'";
			strSql += " AND MST_TranType.Type = " + (int)TransactionHistoryType.Out;
			strSql += " )";
			strSql += " ) as EndStock";

			strSql += " FROM ITM_Product";
			strSql += " INNER JOIN IV_BinCache ON IV_BinCache.ProductID = ITM_Product.ProductID";
			strSql += " INNER JOIN MST_UnitOfMeasure ON MST_UnitOfMeasure.UnitOfMeasureID = ITM_Product.StockUMID";
			strSql += " INNER JOIN MST_BIN ON MST_BIN.BinID = IV_BinCache.BinID";
			strSql += " INNER JOIN MST_Location ON MST_BIN.LocationID = MST_Location.LocationID";
			strSql += " INNER JOIN enm_BINType ON enm_BINType.BinTypeID = MST_BIN.BinTypeID";

			strSql += " LEFT JOIN ITM_Source ON ITM_Product.SourceID = ITM_Source.SourceID";
			strSql += " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID";

			strSql += " WHERE ITM_Product.CCNID = " + pstrCCNID;
			strSql += " AND MST_Location.LocationID IN (" + pstrLocationID + ")";
			
			if(pstrBinType != null && pstrBinType != string.Empty)
				strSql += " AND MST_BIN.BinTypeID IN (" + pstrBinType + ")";

			if(pstrBinID != null && pstrBinID != string.Empty)
				strSql += " AND MST_BIN.BinID = " + pstrBinID;
			
			if(pstrCategoryID != null && pstrCategoryID != string.Empty)
				strSql += " AND ITM_Category.CategoryID IN (" + pstrCategoryID + ")";

			if(pstrModel != null && pstrModel != string.Empty)
				strSql += " AND ITM_Product.Revision IN (" + pstrModel + ")";

			if(pstrProductSourceID != null && pstrProductSourceID != string.Empty)
				strSql += " AND ITM_Source.SourceID = " + pstrProductSourceID;

			if(pstrProductID != null && pstrProductID != string.Empty)
				strSql += " AND ITM_Product.ProductID IN (" + pstrProductID + ")";

			strSql += " GROUP BY MST_Location.Code,";
			strSql += " ITM_Category.Code,";
			strSql += " MST_UnitOfMeasure.Code,";
			strSql += " ITM_Product.Code,";
			strSql += " ITM_Product.Revision,";
			strSql += " ITM_Product.Description,";
			strSql += " ITM_Source.Code,";
			strSql += " MST_Bin.Code,";
			strSql += " IV_BinCache.BinID,";
			strSql += " IV_BinCache.ProductID,";
			strSql += " IV_BinCache.OHQuantity,";
			strSql += " enm_BINType.Name";

			//HACK: added by Tuan TQ: Only show rows which all have none-zero value
			string strNewSQL = " SELECT a.* FROM (" + strSql + ") a ";
			strNewSQL += " WHERE 	a.BeginStock > 0 ";
			strNewSQL += " OR a.EndStock > 0 ";
			strNewSQL += " OR a.InQuantity > 0 "; 
			strNewSQL += " OR a.OutQuantity > 0";

			strNewSQL += " ORDER BY a.LocationCode, a.BinTypeName, a.BinCode, a.CategoryCode, a.PartNumber ASC";
			//End hack

			ocmdPCS = new OleDbCommand(strNewSQL, oconPCS);
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);			

			if(dtbResult != null)
			{					
				return dtbResult;
			}
			else
			{				
				return new DataTable(TABLE_NAME);
			}			
		}

		#endregion
		
		#region Destroy Material Report: Tuan TQ
		/// <summary>
		/// Get Destroy Material
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrListDepartmentID"></param>
		/// <param name="pstrListProductionLineID"></param>
		/// <param name="pstrListCategoryID"></param>
		/// <param name="pstrListProductID"></param>
		/// <returns></returns>
		public DataTable GetDestroyMaterialData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrListDepartmentID, string pstrListProductionLineID, string pstrListCategoryID, string pstrListProductID)
		{			

			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			StringBuilder strSqlBuilder = new StringBuilder();
			strSqlBuilder.Append("SELECT DISTINCT MST_Department.Code AS MST_DepartmentCode,");
			strSqlBuilder.Append(" PRO_ProductionLine.Code AS PRO_ProductionLineCode,");
			strSqlBuilder.Append(" ITM_Category.Code as ITM_CategoryCode,");
			strSqlBuilder.Append(" ITM_Product.ProductID,");
			strSqlBuilder.Append(" ITM_Product.Revision as ITM_ProductRevision,");
			strSqlBuilder.Append(" ITM_Product.Code AS ITM_ProductCode,");
			strSqlBuilder.Append(" ITM_Product.Description as ITM_ProductDescription,"); 
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode,");
			strSqlBuilder.Append(" SUM(IV_MiscellaneousIssueDetail.Quantity) as DestroyQty,");
			
			strSqlBuilder.Append(" (SELECT SUM(actHis.ActualCost)");
			strSqlBuilder.Append(" FROM CST_ActualCostHistory actHis");
			strSqlBuilder.Append(" 	INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID");
			strSqlBuilder.Append(" 	INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID");
			strSqlBuilder.Append(" WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.Machine );
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID");
			strSqlBuilder.Append(" 	AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ")");
			strSqlBuilder.Append(" 		OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + "))");
			strSqlBuilder.Append(" ) as ActualCostMachine,");
			
			strSqlBuilder.Append(" (SELECT SUM(actHis.ActualCost)");
			strSqlBuilder.Append(" FROM CST_ActualCostHistory actHis");
			strSqlBuilder.Append(" 	INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID");
			strSqlBuilder.Append(" 	INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID");
			strSqlBuilder.Append(" WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.Labor);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID");
			strSqlBuilder.Append(" 	AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ")");
			strSqlBuilder.Append(" 		OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + "))");
			strSqlBuilder.Append(" ) as ActualCostLabor,");

			strSqlBuilder.Append(" (SELECT SUM(actHis.ActualCost)");
			strSqlBuilder.Append(" FROM CST_ActualCostHistory actHis");
			strSqlBuilder.Append(" 	INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID");
			strSqlBuilder.Append(" 	INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID");
			strSqlBuilder.Append(" WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.Material);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID");
			strSqlBuilder.Append(" 	AND actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID");
			strSqlBuilder.Append(" 	AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ")");
			strSqlBuilder.Append(" 		OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + "))");
			strSqlBuilder.Append(" ) as ActualCostMaterial,");

			strSqlBuilder.Append(" (SELECT SUM(actHis.ActualCost)");
			strSqlBuilder.Append(" FROM CST_ActualCostHistory actHis");
			strSqlBuilder.Append(" 	INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID");
			strSqlBuilder.Append(" 	INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID");
			strSqlBuilder.Append(" WHERE  STD_CostElement.CostElementTypeID = " + + (int)CostElementType.SubMaterial);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID");
			strSqlBuilder.Append(" 	AND actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID");
			strSqlBuilder.Append(" 	AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ")");
			strSqlBuilder.Append(" 		OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + "))");
			strSqlBuilder.Append(" ) as ActualCostSubMaterial,");

			strSqlBuilder.Append(" (SELECT SUM(actHis.ActualCost)");
			strSqlBuilder.Append(" FROM CST_ActualCostHistory actHis");
			strSqlBuilder.Append(" 	INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID");
			strSqlBuilder.Append(" 	INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID");
			strSqlBuilder.Append(" WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.OverHead);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID");
			strSqlBuilder.Append(" 	AND actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID");
			strSqlBuilder.Append(" 	AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ")");
			strSqlBuilder.Append(" 		OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + "))");
			strSqlBuilder.Append(" ) as ActualCostOverHead");

			strSqlBuilder.Append(" FROM    ITM_Product");
			strSqlBuilder.Append(" INNER JOIN IV_MiscellaneousIssueDetail ON ITM_Product.ProductID = IV_MiscellaneousIssueDetail.ProductID");
			strSqlBuilder.Append(" INNER JOIN IV_MiscellaneousIssueMaster ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID");
			strSqlBuilder.Append(" INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID");
			strSqlBuilder.Append(" LEFT JOIN PRO_ProductionLine ON ITM_Product.ProductionLineID = PRO_ProductionLine.ProductionLineID");
			strSqlBuilder.Append(" LEFT JOIN MST_Department ON PRO_ProductionLine.DepartmentID = MST_Department.DepartmentID");
			strSqlBuilder.Append(" LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID");

			strSqlBuilder.Append(" WHERE ITM_Product.CCNID = " + pstrCCNID);
			strSqlBuilder.Append(" AND IV_MiscellaneousIssueMaster.IssuePurposeID = " + (int)PurposeEnum.Scrap);
			strSqlBuilder.Append(" AND MONTH(IV_MiscellaneousIssueMaster.PostDate) = " + pstrMonth);
			strSqlBuilder.Append(" AND YEAR(IV_MiscellaneousIssueMaster.PostDate) = " + pstrYear);
			
			if(pstrListDepartmentID != string.Empty)
			{
				strSqlBuilder.Append(" AND MST_Department.DepartmentID IN " + pstrListDepartmentID);
			}

			if(pstrListProductionLineID != string.Empty)
			{
				strSqlBuilder.Append(" AND ITM_Product.ProductionLineID IN " + pstrListProductionLineID);
			}

			if(pstrListCategoryID != string.Empty)
			{
				strSqlBuilder.Append(" AND ITM_Product.CategoryID IN " + pstrListCategoryID);
			}

			if(pstrListProductID != string.Empty)
			{
				strSqlBuilder.Append(" AND ITM_Product.ProductID IN " + pstrListProductID);
			}

			strSqlBuilder.Append(" GROUP BY MST_Department.Code,");
			strSqlBuilder.Append(" PRO_ProductionLine.Code,");
			strSqlBuilder.Append(" ITM_Category.Code,");
			strSqlBuilder.Append(" ITM_Product.ProductID,");
			strSqlBuilder.Append(" ITM_Product.Revision,");
			strSqlBuilder.Append(" ITM_Product.Code,");
			strSqlBuilder.Append(" ITM_Product.Description,");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code");

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}
	

		#endregion

		#region Item Cost By Month: Tuan TQ
		/// <summary>
		/// Get Item Cost By Month
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrListDepartmentID"></param>
		/// <param name="pstrListProductionLineID"></param>
		/// <param name="pstrListCategoryID"></param>
		/// <param name="pstrListProductID"></param>
		/// <returns></returns>
		public DataTable GetItemCostByMonthData(string pstrCCNID, string pstrYear, string pstrListDepartmentID, string pstrListProductionLineID, string pstrListCategoryID, string pstrListProductID)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			StringBuilder strSqlBuilder = new StringBuilder();

			strSqlBuilder.Append(" SELECT ");
			strSqlBuilder.Append(" a.MST_DepartmentCode, ");
			strSqlBuilder.Append(" a.PRO_ProductionLineCode,	 ");
			strSqlBuilder.Append(" a.ITM_CategoryCode, ");
			strSqlBuilder.Append(" a.ProductID, ");
			strSqlBuilder.Append(" a.ITM_ProductRevision,  ");
			strSqlBuilder.Append(" a.ITM_ProductCode,  ");
			strSqlBuilder.Append(" a.ITM_ProductDescription,  ");
			strSqlBuilder.Append(" a.MST_UnitOfMeasureCode, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" ((ISNULL(a.JanCost, 0) * ISNULL(a.JanTotalQuantity, 0)) -  ");
			strSqlBuilder.Append(" (a.JanBeginQuantity * a.PreviousJanCost))/ ");
			strSqlBuilder.Append(" (Case when a.JanActualQuantity = 0 then 1 else ISNULL(a.JanActualQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as JanCost, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" ((ISNULL(a.FebCost, 0) * ISNULL(a.FebTotalQuantity, 0)) -  ");
			strSqlBuilder.Append(" (a.FebBeginQuantity * a.PreviousFebCost))/ ");
			strSqlBuilder.Append(" (Case when a.FebActualQuantity = 0 then 1 else ISNULL(a.FebActualQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as FebCost, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" ((ISNULL(a.MarCost, 0) * ISNULL(a.MarTotalQuantity, 0)) -  ");
			strSqlBuilder.Append(" (a.MarBeginQuantity * a.PreviousMarCost))/ ");
			strSqlBuilder.Append(" (Case when a.MarActualQuantity = 0 then 1 else ISNULL(a.MarActualQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as MarCost, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" ((ISNULL(a.AprCost, 0) * ISNULL(a.AprTotalQuantity, 0)) -  ");
			strSqlBuilder.Append(" (a.AprBeginQuantity * a.PreviousAprCost))/ ");
			strSqlBuilder.Append(" (Case when a.AprActualQuantity = 0 then 1 else ISNULL(a.AprActualQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as AprCost, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" ((ISNULL(a.MayCost, 0) * ISNULL(a.MayTotalQuantity, 0)) -  ");
			strSqlBuilder.Append(" (a.MayBeginQuantity * a.PreviousMayCost))/ ");
			strSqlBuilder.Append(" (Case when a.MayActualQuantity = 0 then 1 else ISNULL(a.MayActualQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as MayCost, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" ((ISNULL(a.JunCost, 0) * ISNULL(a.JunTotalQuantity, 0)) -  ");
			strSqlBuilder.Append(" (a.JunBeginQuantity * a.PreviousJunCost))/ ");
			strSqlBuilder.Append(" (Case when a.JunActualQuantity = 0 then 1 else ISNULL(a.JunActualQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as JunCost, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" ((ISNULL(a.JulCost, 0) * ISNULL(a.JulTotalQuantity, 0)) -  ");
			strSqlBuilder.Append(" (a.JulBeginQuantity * a.PreviousJulCost))/ ");
			strSqlBuilder.Append(" (Case when a.JulActualQuantity = 0 then 1 else ISNULL(a.JulActualQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as JulCost, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" ((ISNULL(a.AugCost, 0) * ISNULL(a.AugTotalQuantity, 0)) -  ");
			strSqlBuilder.Append(" (a.AugBeginQuantity * a.PreviousAugCost))/ ");
			strSqlBuilder.Append(" (Case when a.AugActualQuantity = 0 then 1 else ISNULL(a.AugActualQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as AugCost, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" ((ISNULL(a.SepCost, 0) * ISNULL(a.SepTotalQuantity, 0)) -  ");
			strSqlBuilder.Append(" (a.SepBeginQuantity * a.PreviousSepCost))/ ");
			strSqlBuilder.Append(" (Case when a.SepActualQuantity = 0 then 1 else ISNULL(a.SepActualQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as SepCost, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" ((ISNULL(a.OctCost, 0) * ISNULL(a.OctTotalQuantity, 0)) -  ");
			strSqlBuilder.Append(" (a.OctBeginQuantity * a.PreviousOctCost))/ ");
			strSqlBuilder.Append(" (Case when a.OctActualQuantity = 0 then 1 else ISNULL(a.OctActualQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as OctCost, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" ((ISNULL(a.NovCost, 0) * ISNULL(a.NovTotalQuantity, 0)) -  ");
			strSqlBuilder.Append(" (a.NovBeginQuantity * a.PreviousNovCost))/ ");
			strSqlBuilder.Append(" (Case when a.NovActualQuantity = 0 then 1 else ISNULL(a.NovActualQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as NovCost, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" ((ISNULL(a.DecCost, 0) * ISNULL(a.DecTotalQuantity, 0)) -  ");
			strSqlBuilder.Append(" (a.DecBeginQuantity * a.PreviousDecCost))/ ");
			strSqlBuilder.Append(" (Case when a.DecActualQuantity = 0 then 1 else ISNULL(a.DecActualQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as DecCost ");
			 
			strSqlBuilder.Append(" FROM ( ");
			strSqlBuilder.Append(" SELECT  DISTINCT MST_Department.Code AS MST_DepartmentCode, ");
			strSqlBuilder.Append(" 	PRO_ProductionLine.Code AS PRO_ProductionLineCode,	 ");
			strSqlBuilder.Append(" 	ITM_Category.Code as ITM_CategoryCode, ");
			strSqlBuilder.Append(" 	ITM_Product.ProductID, ");
			strSqlBuilder.Append(" 	ITM_Product.Revision as ITM_ProductRevision,  ");
			strSqlBuilder.Append(" 	ITM_Product.Code AS ITM_ProductCode,  ");
			strSqlBuilder.Append(" 	ITM_Product.Description as ITM_ProductDescription,  ");
			strSqlBuilder.Append(" 	MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode, ");

			//January Information ");
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 1 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as JanTotalQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 1 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as JanBeginQuantity, ");
	
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 1 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as JanActualQuantity, ");
			
			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(   ");
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	When	  ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 12 AND YEAR(allocMaster.FromDate) = " + pstrYear + " - 1) ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 12 AND YEAR(allocMaster.ToDate) = " + pstrYear + " - 1)) ");
			strSqlBuilder.Append(" 	 )	 ");
			strSqlBuilder.Append(" 	 Is Not Null Then ");
			strSqlBuilder.Append(" 	    ( ");
			strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 12 AND YEAR(allocMaster.FromDate) = " + pstrYear + " - 1) ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 12 AND YEAR(allocMaster.ToDate) = " + pstrYear + " - 1)) ");
			strSqlBuilder.Append(" 	    ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	   )		 ");
			strSqlBuilder.Append(" 	End), 0)  ");
			strSqlBuilder.Append(" 	as PreviousJanCost, ");

			strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 1 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as JanCost, ");

			//February Information	 ");
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 2 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as FebTotalQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 2 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as FebBeginQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 2 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as FebActualQuantity, ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(   ");
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	When	  ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append("    SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 1 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 1 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	 )	 ");
			strSqlBuilder.Append(" 	 Is Not Null Then ");
			strSqlBuilder.Append(" 	    ( ");
			strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 1 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 1 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	    ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	   )		 ");
			strSqlBuilder.Append(" 	End), 0)  ");
			strSqlBuilder.Append(" 	as PreviousFebCost, ");

			strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 2 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as FebCost, ");

			//March Information ");
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 3 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as MarTotalQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 3 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as MarBeginQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 3 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as MarActualQuantity, ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(   ");
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	When	  ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 2 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 2 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	 )	 ");
			strSqlBuilder.Append(" 	 Is Not Null Then ");
			strSqlBuilder.Append(" 	    ( ");
			strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 2 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 2 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	    ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	   )		 ");
			strSqlBuilder.Append(" 	End), 0)  ");
			strSqlBuilder.Append(" 	as PreviousMarCost, ");

			strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 3 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as MarCost, ");

			//April Information ");
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 4 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as AprTotalQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 4 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as AprBeginQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 4 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as AprActualQuantity, ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(   ");
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	When	  ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 3 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 3 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	 )	 ");
			strSqlBuilder.Append(" 	 Is Not Null Then ");
			strSqlBuilder.Append(" 	    ( ");
			strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 3 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 3 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	    ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	   )		 ");
			strSqlBuilder.Append(" 	End), 0)  ");
			strSqlBuilder.Append(" 	as PreviousAprCost, ");

			strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 4 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as AprCost, ");

			//May Information ");
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 5 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as MayTotalQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 5 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as MayBeginQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 5 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as MayActualQuantity, ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(   ");
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	When	  ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 4 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 4 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	 )	 ");
			strSqlBuilder.Append(" 	 Is Not Null Then ");
			strSqlBuilder.Append(" 	    ( ");
			strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 4 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 4 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	    ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	   )		 ");
			strSqlBuilder.Append(" 	End), 0)  ");
			strSqlBuilder.Append(" 	as PreviousMayCost, ");

			strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append("	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 1 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as MayCost, ");

			//June Information ");
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 6 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as JunTotalQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 6 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as JunBeginQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 6 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as JunActualQuantity, ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(   ");
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	When	  ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append("    SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 5 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 5 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	 )	 ");
			strSqlBuilder.Append(" 	 Is Not Null Then ");
			strSqlBuilder.Append(" 	    ( ");
			strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 5 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 5 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	    ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	   )		 ");
			strSqlBuilder.Append(" 	End), 0)  ");
			strSqlBuilder.Append(" 	as PreviousJunCost, ");

			strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 6 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as JunCost, ");

			//July Information ");
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 7 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as JulTotalQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 7 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as JulBeginQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 7 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as JulActualQuantity, ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(   ");
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	When	  ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 6 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 6 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	 )	 ");
			strSqlBuilder.Append(" 	 Is Not Null Then ");
			strSqlBuilder.Append(" 	    ( ");
			strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 6 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 6 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	    ) ");
			strSqlBuilder.Append(" 	Else ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	   ) ");
			strSqlBuilder.Append(" 	End), 0)  ");
			strSqlBuilder.Append(" 	as PreviousJulCost, ");

			strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 7 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as JulCost, ");

			//August Information ");
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 8 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as AugTotalQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 8 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as AugBeginQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 8 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as AugActualQuantity, ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(   ");
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	When	  ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 7 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 7 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	 )	 ");
			strSqlBuilder.Append(" 	 Is Not Null Then ");
			strSqlBuilder.Append(" 	    ( ");
			strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 7 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 7 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	    ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	   )		 ");
			strSqlBuilder.Append(" 	End), 0)  ");
			strSqlBuilder.Append(" 	as PreviousAugCost, ");

			strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 8 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as AugCost, ");

			//September Information ");
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 9 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as SepTotalQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 9 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as SepBeginQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 9 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as SepActualQuantity, ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(   ");
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	When	  ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 8 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 8 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	 )	 ");
			strSqlBuilder.Append(" 	 Is Not Null Then ");
			strSqlBuilder.Append(" 	    ( ");
			strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 8 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 8 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	    ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	   )		 ");
			strSqlBuilder.Append(" 	End), 0)  ");
			strSqlBuilder.Append(" 	as PreviousSepCost, ");

			strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 9 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as SepCost, ");

			//October Information ");
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 10 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as OctTotalQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 10 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as OctBeginQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 10 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as OctActualQuantity, ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(   ");
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	When	  ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 9 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 9 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	 )	 ");
			strSqlBuilder.Append(" 	 Is Not Null Then ");
			strSqlBuilder.Append(" 	    ( ");
			strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 9 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 9 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	    ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	   )		 ");
			strSqlBuilder.Append(" 	End), 0)  ");
			strSqlBuilder.Append(" 	as PreviousOctCost, ");

			strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 10 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as OctCost, ");

			//November Information ");
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 11 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as NovTotalQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 11 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as NovBeginQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 11 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as NovActualQuantity, ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(   ");
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	When	  ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 10 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 10 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	 )	 ");
			strSqlBuilder.Append(" 	 Is Not Null Then ");
			strSqlBuilder.Append(" 	    ( ");
			strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 10 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 10 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	    ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	   )		 ");
			strSqlBuilder.Append(" 	End), 0)  ");
			strSqlBuilder.Append(" 	as PreviousNovCost, ");

			strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 11 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as NovCost, ");

			//December Information
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 12 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as DecTotalQuantity, ");
			
			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 12 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as DecBeginQuantity, ");

			strSqlBuilder.Append(" 	( ");
			strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
			strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 12 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as DecActualQuantity, ");

			strSqlBuilder.Append(" 	 ");
			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(   ");
			strSqlBuilder.Append(" 	Case  ");
			strSqlBuilder.Append(" 	When	  ");
			strSqlBuilder.Append(" 	 ( ");
			strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 11 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 11 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	 )	 ");
			strSqlBuilder.Append(" 	 Is Not Null Then ");
			strSqlBuilder.Append(" 	    ( ");
			strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 11 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 11 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	    ) ");
			strSqlBuilder.Append(" 	Else  ");
			strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
			strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
			strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	   )		 ");
			strSqlBuilder.Append(" 	End), 0)  ");
			strSqlBuilder.Append(" 	as PreviousDecCost, ");
			strSqlBuilder.Append("  ");
			strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	       AND ( 1 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
			strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	) as DecCost ");
			strSqlBuilder.Append("  ");
			strSqlBuilder.Append(" FROM    ITM_Product ");
			strSqlBuilder.Append(" 	INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID  ");
			strSqlBuilder.Append(" 	INNER JOIN CST_ActualCostHistory ON CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	LEFT JOIN PRO_ProductionLine ON ITM_Product.ProductionLineID = PRO_ProductionLine.ProductionLineID  ");
			strSqlBuilder.Append(" 	LEFT JOIN MST_Department ON PRO_ProductionLine.DepartmentID = MST_Department.DepartmentID ");
			strSqlBuilder.Append(" 	LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID ");
			strSqlBuilder.Append("  ");
			strSqlBuilder.Append(" WHERE ITM_Product.CCNID = " + pstrCCNID);
						
			if(pstrListDepartmentID != string.Empty)
			{
				strSqlBuilder.Append(" AND MST_Department.DepartmentID IN " + pstrListDepartmentID);
			}

			if(pstrListProductionLineID != string.Empty)
			{
				strSqlBuilder.Append(" AND ITM_Product.ProductionLineID IN " + pstrListProductionLineID);
			}

			if(pstrListCategoryID != string.Empty)
			{
				strSqlBuilder.Append(" AND ITM_Product.CategoryID IN " + pstrListCategoryID);
			}

			if(pstrListProductID != string.Empty)
			{
				strSqlBuilder.Append(" AND ITM_Product.ProductID IN " + pstrListProductID);
			}
			strSqlBuilder.Append(" ) a ");
			strSqlBuilder.Append("  ");
			strSqlBuilder.Append(" WHERE a.JanCost > 0 ");
			strSqlBuilder.Append("  OR a.FebCost > 0	 ");
			strSqlBuilder.Append("  OR a.MarCost > 0 ");
			strSqlBuilder.Append("  OR a.AprCost > 0 ");
			strSqlBuilder.Append("  OR a.MayCost > 0 ");
			strSqlBuilder.Append("  OR a.JunCost > 0 ");
			strSqlBuilder.Append("  OR a.JulCost > 0 ");
			strSqlBuilder.Append("  OR a.AugCost > 0 ");
			strSqlBuilder.Append("  OR a.SepCost > 0 ");
			strSqlBuilder.Append("  OR a.OctCost > 0 ");
			strSqlBuilder.Append("  OR a.NovCost > 0 ");
			strSqlBuilder.Append("  OR a.DecCost > 0 ");


			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);

			ocmdPCS.CommandTimeout = 1000;
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}
		#endregion

		#region Item Cost Detailed By Element - BOD: Tuan TQ
		public DataTable GetItemCostDetailedByElementData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrListDepartmentID, string pstrListProductionLineID, string pstrListCategoryID, string pstrListProductID, int pintMakeItem)
		{			

			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			StringBuilder strSqlBuilder = new StringBuilder();
			strSqlBuilder.Append(" SELECT ");
			strSqlBuilder.Append(" a.MST_DepartmentCode, ");
			strSqlBuilder.Append(" a.PRO_ProductionLineCode,	 ");
			strSqlBuilder.Append(" a.ITM_CategoryCode, ");
			strSqlBuilder.Append(" a.ProductID, ");
			strSqlBuilder.Append(" a.ITM_ProductRevision,  ");
			strSqlBuilder.Append(" a.ITM_ProductCode,  ");
			strSqlBuilder.Append(" a.ITM_ProductDescription,  ");
			strSqlBuilder.Append(" a.MST_UnitOfMeasureCode, ");
			strSqlBuilder.Append(" a.ActualPeriodQuantity, ");
			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" (Case when a.WOCompletionQty = 0 then 0 else ");
			strSqlBuilder.Append(" ((a.ActualCostMachine * a.TotalPeriodQuantity) -  ");
			strSqlBuilder.Append(" (a.BeginQuantity * a.PreviousMachineCost))/ ");
			strSqlBuilder.Append(" ISNULL(a.WOCompletionQty, 1) end) ");
			strSqlBuilder.Append(" ) as ActualCostMachine, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" (Case when a.WOCompletionQty = 0 then 0 else ");
			strSqlBuilder.Append(" ((a.ActualCostLabor * a.TotalPeriodQuantity) -  ");
			strSqlBuilder.Append(" (a.BeginQuantity * a.PreviousLaborCost))/ ISNULL(a.WOCompletionQty, 1) end) ");
			strSqlBuilder.Append(" ) as ActualCostLabor, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" (Case when a.WOCompletionQty = 0 then 0 else ");
			strSqlBuilder.Append(" ((a.ActualCostMaterial * a.TotalPeriodQuantity) -  ");
			strSqlBuilder.Append(" (a.BeginQuantity * a.PreviousMaterialCost))/ ISNULL(a.WOCompletionQty, 1) end) ");
			strSqlBuilder.Append(" ) as ActualCostMaterial, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" (Case when a.ActualPeriodQuantity = 0 then 0 else ");
			strSqlBuilder.Append(" ((a.ActualCostSubMaterial * a.TotalPeriodQuantity) -  ");
			strSqlBuilder.Append(" (a.BeginQuantity * a.PreviousSubMaterialCost))/ ISNULL(a.ActualPeriodQuantity, 1) end) ");
			strSqlBuilder.Append(" ) as ActualCostSubMaterial, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" (Case when a.WOCompletionQty = 0 then 0 else ");
			strSqlBuilder.Append(" ((a.ActualCostOverHead * a.TotalPeriodQuantity) -  ");
			strSqlBuilder.Append(" (a.BeginQuantity * a.PreviousOverHeadCost))/ ISNULL(a.WOCompletionQty, 1) end) ");
			strSqlBuilder.Append(" ) as ActualCostOverHead ");

			strSqlBuilder.Append(" FROM  ");
			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" SELECT  DISTINCT MST_Department.Code AS MST_DepartmentCode, ");
			strSqlBuilder.Append(" PRO_ProductionLine.Code AS PRO_ProductionLineCode,	 ");
			strSqlBuilder.Append(" ITM_Category.Code as ITM_CategoryCode, ");
			strSqlBuilder.Append(" ITM_Product.ProductID, ");
			strSqlBuilder.Append(" ITM_Product.Revision as ITM_ProductRevision,  ");
			strSqlBuilder.Append(" ITM_Product.Code AS ITM_ProductCode,  ");
			strSqlBuilder.Append(" ITM_Product.Description as ITM_ProductDescription,  ");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode, ");

			strSqlBuilder.Append(" CST_ActualCostHistory.Quantity as TotalPeriodQuantity, ");
			strSqlBuilder.Append(" CST_ActualCostHistory.BeginQuantity, ");
			strSqlBuilder.Append(" CST_ActualCostHistory.WOCompletionQty, ");
			strSqlBuilder.Append(" CST_ActualCostHistory.Quantity - CST_ActualCostHistory.BeginQuantity as ActualPeriodQuantity, ");

			strSqlBuilder.Append(" 	ISNULL((SELECT BeginCost FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID ");
			strSqlBuilder.Append(" 	WHERE STD_CostElement.CostElementTypeID = " + (int)CostElementType.Machine);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	AND actHis.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID),0) as PreviousMachineCost,");

			strSqlBuilder.Append(" (SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append("  FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append("      INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID	 ");
			strSqlBuilder.Append("      INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append("  WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.Machine);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	     OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" ) ");
			strSqlBuilder.Append(" as ActualCostMachine, ");

			strSqlBuilder.Append(" 	ISNULL((SELECT BeginCost FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID ");
			strSqlBuilder.Append(" 	WHERE STD_CostElement.CostElementTypeID = " + (int)CostElementType.Labor);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	AND actHis.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID),0) as PreviousLaborCost,");
			
			strSqlBuilder.Append(" (SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append("  FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append("      INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID	 ");
			strSqlBuilder.Append("      INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append("  WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.Labor);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	     OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" ) as ActualCostLabor, ");

			strSqlBuilder.Append(" 	ISNULL((SELECT BeginCost FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID ");
			strSqlBuilder.Append(" 	WHERE STD_CostElement.CostElementTypeID = " + (int)CostElementType.Material);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	AND actHis.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID),0) as PreviousMaterialCost,");
			
			strSqlBuilder.Append(" (SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append("  FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append("      INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID	 ");
			strSqlBuilder.Append("      INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID	 ");
			strSqlBuilder.Append("  WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.Material);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	AND actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	     OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");

			strSqlBuilder.Append(" ) as ActualCostMaterial, ");

			strSqlBuilder.Append(" 	ISNULL((SELECT BeginCost FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID ");
			strSqlBuilder.Append(" 	WHERE STD_CostElement.CostElementTypeID = " + (int)CostElementType.SubMaterial);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	AND actHis.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID),0) as PreviousSubMaterialCost,");
			
			strSqlBuilder.Append(" (SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append("  FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append("      INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID	 ");
			strSqlBuilder.Append("      INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID	 ");
			strSqlBuilder.Append("  WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.SubMaterial);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	AND actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	     OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");

			strSqlBuilder.Append(" ) as ActualCostSubMaterial, ");

			strSqlBuilder.Append(" 	ISNULL((SELECT BeginCost FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID ");
			strSqlBuilder.Append(" 	WHERE STD_CostElement.CostElementTypeID = " + (int)CostElementType.OverHead);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	AND actHis.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID),0) as PreviousOverHeadCost,");
			
			strSqlBuilder.Append(" (SELECT SUM(actHis.ActualCost) ");
			strSqlBuilder.Append("  FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append("      INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID	 ");
			strSqlBuilder.Append("      INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append("  WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.OverHead);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	AND actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 	     OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");

			strSqlBuilder.Append(" ) as ActualCostOverHead, ");

			strSqlBuilder.Append(" CST_ActualCostHistory.ActCostAllocationMasterID ");

			strSqlBuilder.Append(" FROM    CST_ActualCostHistory ");
			strSqlBuilder.Append(" 	INNER JOIN ITM_Product ON CST_ActualCostHistory.ProductID = ITM_Product.ProductID  ");
			strSqlBuilder.Append(" 	INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID 	 ");
			strSqlBuilder.Append(" 	INNER JOIN cst_ActCostAllocationMaster ON CST_ActualCostHistory.ActCostAllocationMasterID = cst_ActCostAllocationMaster.ActCostAllocationMasterID	 ");
			strSqlBuilder.Append(" 	LEFT JOIN PRO_ProductionLine ON ITM_Product.ProductionLineID = PRO_ProductionLine.ProductionLineID  ");
			strSqlBuilder.Append(" 	LEFT JOIN MST_Department ON PRO_ProductionLine.DepartmentID = MST_Department.DepartmentID ");
			strSqlBuilder.Append(" 	LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID ");

			strSqlBuilder.Append(" WHERE ITM_Product.CCNID = " + pstrCCNID);
			strSqlBuilder.Append(" AND"); 
			strSqlBuilder.Append(" (");
			strSqlBuilder.Append(" (MONTH(cst_ActCostAllocationMaster.FromDate) = " + pstrMonth + " AND YEAR(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + ")");
			strSqlBuilder.Append(" 	OR (MONTH(cst_ActCostAllocationMaster.ToDate) = " + pstrMonth + " AND YEAR(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ")");
			strSqlBuilder.Append(" )");

			if(pstrListDepartmentID != string.Empty)
				strSqlBuilder.Append(" AND MST_Department.DepartmentID IN " + pstrListDepartmentID);
			if(pstrListProductionLineID != string.Empty)
				strSqlBuilder.Append(" AND ITM_Product.ProductionLineID IN " + pstrListProductionLineID);
			if(pstrListCategoryID != string.Empty)
				strSqlBuilder.Append(" AND ITM_Product.CategoryID IN " + pstrListCategoryID);
			if(pstrListProductID != string.Empty)
				strSqlBuilder.Append(" AND ITM_Product.ProductID IN " + pstrListProductID);
			if (pintMakeItem > 0)
				strSqlBuilder.Append(" 	AND ITM_Product.MakeItem = " + pintMakeItem);
			      
			strSqlBuilder.Append(" ) a ");
			string strSQL = " SELECT a.* FROM (" + strSqlBuilder.ToString() + ") a";
			strSQL += " WHERE a.ActualPeriodQuantity > 0 ";
			strSQL +="       OR a.ActualCostMachine > 0 ";
			strSQL +="       OR a.ActualCostLabor > 0 ";
			strSQL +="       OR a.ActualCostMaterial > 0 ";
			strSQL +="       OR a.ActualCostSubMaterial > 0 ";
			strSQL +="       OR a.ActualCostOverHead > 0 ";
			
			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSQL, oconPCS);
			ocmdPCS.CommandTimeout = 1000;
			//End hack

			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}
		
		#endregion
		
		#region Item Cost Detailed By Element And Production Line: Tuan TQ
		public DataTable GetItemCostDetailedByElementAndProductionLineData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrListDepartmentID, string pstrListProductionLineID, string pstrListCategoryID, string pstrListProductID, int pintMakeItem)
		{			

			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			StringBuilder strSqlBuilder = new StringBuilder();
			strSqlBuilder.Append(" SELECT ");
			strSqlBuilder.Append(" a.MST_DepartmentCode, ");
			strSqlBuilder.Append(" a.PRO_ProductionLineCode,	 ");
			strSqlBuilder.Append(" a.ITM_CategoryCode, ");
			strSqlBuilder.Append(" a.ProductID, ");
			strSqlBuilder.Append(" a.MST_UnitOfMeasureCode, ");
			strSqlBuilder.Append(" a.ITM_ProductRevision,  ");
			strSqlBuilder.Append(" a.ITM_ProductCode,  ");
			strSqlBuilder.Append(" a.ITM_ProductDescription,  ");
			strSqlBuilder.Append(" a.ActualPeriodQuantity, ");

			strSqlBuilder.Append(" (( ");
			strSqlBuilder.Append(" ((a.ActualCostMaterial * a.TotalPeriodQuantity) - (a.BeginQuantity * a.PreviousMaterialCost))/ ");
			strSqlBuilder.Append(" (Case when a.WOCompletionQty = 0 then 1 else ISNULL(a.WOCompletionQty, 1) end) ");
			strSqlBuilder.Append(" )  ");
			strSqlBuilder.Append(" + a.AllComponentValue - a.MaterialComponentValue) ");
			strSqlBuilder.Append(" as ActualCostMaterial, ");
			
			strSqlBuilder.Append(" (Case when a.ActualPeriodQuantity = 0 then 0 else a.ActualCostLabor / ISNULL(a.ActualPeriodQuantity, 1) end) ");
			strSqlBuilder.Append("  as ActualCostLabor, ");
			
			strSqlBuilder.Append(" (Case when a.ActualPeriodQuantity = 0 then 0 else a.ActualCostMachine / ISNULL(a.ActualPeriodQuantity, 1) end) ");
			strSqlBuilder.Append("  as ActualCostMachine, ");
			
			strSqlBuilder.Append(" (Case when a.ActualPeriodQuantity = 0 then 0 else a.ActualCostSubMaterial / ISNULL(a.ActualPeriodQuantity, 1) end) ");
			strSqlBuilder.Append("  as ActualCostSubMaterial, ");
			
			strSqlBuilder.Append(" (Case when a.ActualPeriodQuantity = 0 then 0 else a.ActualCostOverHead / ISNULL(a.ActualPeriodQuantity, 1) end) ");
			strSqlBuilder.Append("  as ActualCostOverHead ");

			strSqlBuilder.Append(" FROM  ");
			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" SELECT  DISTINCT MST_Department.Code AS MST_DepartmentCode, ");
			strSqlBuilder.Append(" 	PRO_ProductionLine.Code AS PRO_ProductionLineCode,	 ");
			strSqlBuilder.Append(" 	ITM_Category.Code as ITM_CategoryCode, ");
			strSqlBuilder.Append(" 	ITM_Product.ProductID, ");
			strSqlBuilder.Append(" 	ITM_Product.Revision as ITM_ProductRevision,  ");
			strSqlBuilder.Append(" 	ITM_Product.Code AS ITM_ProductCode,  ");
			strSqlBuilder.Append(" 	ITM_Product.Description as ITM_ProductDescription,  ");
			strSqlBuilder.Append(" 	MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode, ");

			strSqlBuilder.Append(" 	CST_ActualCostHistory.Quantity as TotalPeriodQuantity, ");
			strSqlBuilder.Append(" 	CST_ActualCostHistory.BeginQuantity, ");
			strSqlBuilder.Append(" 	CST_ActualCostHistory.WOCompletionQty, ");
			strSqlBuilder.Append(" 	CST_ActualCostHistory.Quantity - CST_ActualCostHistory.BeginQuantity   ");
			strSqlBuilder.Append(" 	as ActualPeriodQuantity, ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(SELECT SUM(ISNULL(actHis.ComponentValue, 0)) ");
			strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	     INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	  WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.Material);
			strSqlBuilder.Append(" 		AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	), 0) as MaterialComponentValue, ");
			
			strSqlBuilder.Append(" ISNULL( ");
			strSqlBuilder.Append(" (SELECT SUM(ISNULL(actHis.ComponentValue, 0)) ");
			strSqlBuilder.Append(" FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append("	INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID	 ");
			strSqlBuilder.Append("	INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" WHERE actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
		    strSqlBuilder.Append(" OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" ), 0) as AllComponentValue, ");

			strSqlBuilder.Append(" 	ISNULL((SELECT BeginCost FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID ");
			strSqlBuilder.Append(" 	WHERE STD_CostElement.CostElementTypeID = " + (int)CostElementType.Material);
			strSqlBuilder.Append(" 	AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	AND actHis.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID),0) as PreviousMaterialCost,");
			
			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(SELECT SUM(ISNULL(actHis.ActualCost, 0)) ");
			strSqlBuilder.Append(" 	FROM CST_ActualCostHistory actHis ");
			strSqlBuilder.Append(" 	     INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID	 ");
			strSqlBuilder.Append(" 	 WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.Material);
			strSqlBuilder.Append(" 		AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	), 0) as ActualCostMaterial, ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(SELECT SUM(ISNULL(actHis.Amount, 0)) ");
			strSqlBuilder.Append(" 	FROM cst_AllocationResult actHis ");			
			strSqlBuilder.Append(" 	     INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.Machine);
			strSqlBuilder.Append(" 		AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	), 0) as ActualCostMachine,	 ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(SELECT SUM(ISNULL(actHis.Amount, 0)) ");
			strSqlBuilder.Append(" 	FROM cst_AllocationResult actHis ");			
			strSqlBuilder.Append(" 	     INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE  STD_CostElement.CostElementTypeID = "  + (int)CostElementType.Labor);
			strSqlBuilder.Append(" 		AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	), 0) as ActualCostLabor,	 ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(SELECT SUM(ISNULL(actHis.Amount, 0)) ");
			strSqlBuilder.Append(" 	FROM cst_AllocationResult actHis ");			
			strSqlBuilder.Append(" 	     INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID	 ");
			strSqlBuilder.Append(" 	 WHERE  STD_CostElement.CostElementTypeID = "  + (int)CostElementType.SubMaterial);
			strSqlBuilder.Append(" 		AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	), 0) as ActualCostSubMaterial,	 ");

			strSqlBuilder.Append(" 	ISNULL( ");
			strSqlBuilder.Append(" 	(SELECT SUM(ISNULL(actHis.Amount, 0)) ");
			strSqlBuilder.Append(" 	FROM cst_AllocationResult actHis ");			
			strSqlBuilder.Append(" 	     INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = actHis.CostElementID	 ");
			strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 	 WHERE  STD_CostElement.CostElementTypeID = "  + (int)CostElementType.OverHead);
			strSqlBuilder.Append(" 		AND actHis.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 		AND actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
			strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = " + pstrMonth + " AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
			strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = " + pstrMonth + " AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
			strSqlBuilder.Append(" 	), 0) as ActualCostOverHead, ");
			 
			strSqlBuilder.Append(" 	CST_ActualCostHistory.ActCostAllocationMasterID ");

			strSqlBuilder.Append(" FROM    CST_ActualCostHistory ");
			strSqlBuilder.Append(" 	INNER JOIN ITM_Product ON CST_ActualCostHistory.ProductID = ITM_Product.ProductID  ");
			strSqlBuilder.Append(" 	INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID 	 ");
			strSqlBuilder.Append(" 	INNER JOIN cst_ActCostAllocationMaster ON CST_ActualCostHistory.ActCostAllocationMasterID = cst_ActCostAllocationMaster.ActCostAllocationMasterID	 ");
			strSqlBuilder.Append(" 	LEFT JOIN PRO_ProductionLine ON ITM_Product.ProductionLineID = PRO_ProductionLine.ProductionLineID  ");
			strSqlBuilder.Append(" 	LEFT JOIN MST_Department ON PRO_ProductionLine.DepartmentID = MST_Department.DepartmentID ");
			strSqlBuilder.Append(" 	LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID ");

			strSqlBuilder.Append(" WHERE ITM_Product.CCNID = " + pstrCCNID);
			strSqlBuilder.Append(" AND "); 
			strSqlBuilder.Append(" (");
			strSqlBuilder.Append(" (MONTH(cst_ActCostAllocationMaster.FromDate) = " + pstrMonth + " AND YEAR(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + ")");
			strSqlBuilder.Append(" 	OR (MONTH(cst_ActCostAllocationMaster.ToDate) = " + pstrMonth + " AND YEAR(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ")");
			strSqlBuilder.Append(" )");

			if(pstrListDepartmentID != string.Empty)
				strSqlBuilder.Append(" AND MST_Department.DepartmentID IN " + pstrListDepartmentID);
			if(pstrListProductionLineID != string.Empty)
				strSqlBuilder.Append(" AND ITM_Product.ProductionLineID IN " + pstrListProductionLineID);
			if(pstrListCategoryID != string.Empty)
				strSqlBuilder.Append(" AND ITM_Product.CategoryID IN " + pstrListCategoryID);
			if(pstrListProductID != string.Empty)
				strSqlBuilder.Append(" AND ITM_Product.ProductID IN " + pstrListProductID);
			if (pintMakeItem > 0)
				strSqlBuilder.Append(" 	AND ITM_Product.MakeItem = " + pintMakeItem);

			strSqlBuilder.Append(" ) a ");

			strSqlBuilder.Append(" WHERE a.ActualPeriodQuantity > 0 ");
			strSqlBuilder.Append("       OR a.ActualCostMachine > 0 ");
			strSqlBuilder.Append("       OR a.ActualCostLabor > 0 ");
			strSqlBuilder.Append("       OR a.ActualCostMaterial > 0 ");
			strSqlBuilder.Append("       OR a.ActualCostSubMaterial > 0 ");
			strSqlBuilder.Append("       OR a.ActualCostOverHead > 0 ");		
			
			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
			ocmdPCS.CommandTimeout = 1000;			

			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}
		
		#endregion

		#region SALVAGING MATERIAL REPORT: Tuan TQ
		public DataTable GetSalvagingMaterialData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrListDepartmentID, string pstrListProductionLineID, string pstrListCategoryID, string pstrListProductID)
		{			

			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			StringBuilder strSqlBuilder = new StringBuilder();
			
			strSqlBuilder.Append("SELECT  DISTINCT MST_Department.Code AS MST_DepartmentCode,");
			strSqlBuilder.Append(" PRO_ProductionLine.Code AS PRO_ProductionLineCode,");
			strSqlBuilder.Append(" ITM_Category.Code as ITM_CategoryCode,");
			strSqlBuilder.Append(" ITM_Product.ProductID,");
			strSqlBuilder.Append(" ITM_Product.Revision as ITM_ProductRevision,");
			strSqlBuilder.Append(" ITM_Product.Code AS ITM_ProductCode,");
			strSqlBuilder.Append(" ITM_Product.Description as ITM_ProductDescription,");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode,");
			strSqlBuilder.Append(" SUM(CST_RecoverMaterialDetail.RecoverQuantity) as CompletedQuantity,");
			
			strSqlBuilder.Append(" (SELECT SUM(stdCost.Cost)");
			strSqlBuilder.Append(" FROM CST_STDItemCost stdCost");
			strSqlBuilder.Append(" INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = stdCost.CostElementID");
			strSqlBuilder.Append(" WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.Machine);
			strSqlBuilder.Append(" AND stdCost.ProductID = ITM_Product.ProductID");
			strSqlBuilder.Append(" ) as StdCostMachine,");

			strSqlBuilder.Append(" (SELECT SUM(stdCost.Cost)");
			strSqlBuilder.Append(" FROM CST_STDItemCost stdCost");
			strSqlBuilder.Append(" INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = stdCost.CostElementID");
			strSqlBuilder.Append(" WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.Labor);
			strSqlBuilder.Append(" AND stdCost.ProductID = ITM_Product.ProductID");			
			strSqlBuilder.Append(" ) as StdCostLabor,");

			strSqlBuilder.Append(" (SELECT SUM(stdCost.Cost)");
			strSqlBuilder.Append(" FROM CST_STDItemCost stdCost");
			strSqlBuilder.Append(" INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = stdCost.CostElementID");
			strSqlBuilder.Append(" WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.Material);
			strSqlBuilder.Append(" AND stdCost.ProductID = ITM_Product.ProductID");
			strSqlBuilder.Append(" ) as StdCostMaterial,");

			strSqlBuilder.Append(" (SELECT SUM(stdCost.Cost)");
			strSqlBuilder.Append(" FROM CST_STDItemCost stdCost");
			strSqlBuilder.Append(" INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = stdCost.CostElementID");
			strSqlBuilder.Append(" WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.SubMaterial);
			strSqlBuilder.Append(" AND stdCost.ProductID = ITM_Product.ProductID");			
			strSqlBuilder.Append(" ) as StdCostSubMaterial,");

			strSqlBuilder.Append(" (SELECT SUM(stdCost.Cost)");
			strSqlBuilder.Append(" FROM CST_STDItemCost stdCost");
			strSqlBuilder.Append(" INNER JOIN STD_CostElement ON STD_CostElement.CostElementID = stdCost.CostElementID");
			strSqlBuilder.Append(" WHERE  STD_CostElement.CostElementTypeID = " + (int)CostElementType.OverHead);
			strSqlBuilder.Append(" AND stdCost.ProductID = ITM_Product.ProductID");			
			strSqlBuilder.Append(" ) as StdCostOverHead");

			strSqlBuilder.Append(" FROM    CST_RecoverMaterialDetail");
			strSqlBuilder.Append(" INNER JOIN CST_RecoverMaterialMaster ON CST_RecoverMaterialDetail.RecoverMaterialMasterID = CST_RecoverMaterialMaster.RecoverMaterialMasterID");
			strSqlBuilder.Append(" INNER JOIN ITM_Product ON CST_RecoverMaterialDetail.ProductID = ITM_Product.ProductID");
			strSqlBuilder.Append(" INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID");
			strSqlBuilder.Append(" LEFT JOIN PRO_ProductionLine ON ITM_Product.ProductionLineID = PRO_ProductionLine.ProductionLineID");
			strSqlBuilder.Append(" LEFT JOIN MST_Department ON PRO_ProductionLine.DepartmentID = MST_Department.DepartmentID");
			strSqlBuilder.Append(" LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID");

			strSqlBuilder.Append(" WHERE ITM_Product.CCNID = " + pstrCCNID);
			strSqlBuilder.Append(" AND"); 			
			strSqlBuilder.Append(" MONTH(CST_RecoverMaterialMaster.PostDate) = " + pstrMonth + " AND YEAR(CST_RecoverMaterialMaster.PostDate) = " + pstrYear);			

			if(pstrListDepartmentID != string.Empty)
			{
				strSqlBuilder.Append(" AND MST_Department.DepartmentID IN " + pstrListDepartmentID);
			}

			if(pstrListProductionLineID != string.Empty)
			{
				strSqlBuilder.Append(" AND ITM_Product.ProductionLineID IN " + pstrListProductionLineID);
			}

			if(pstrListCategoryID != string.Empty)
			{
				strSqlBuilder.Append(" AND ITM_Product.CategoryID IN " + pstrListCategoryID);
			}

			if(pstrListProductID != string.Empty)
			{
				strSqlBuilder.Append(" AND ITM_Product.ProductID IN " + pstrListProductID);
			}		
			
			strSqlBuilder.Append(" GROUP BY MST_Department.Code,");
			strSqlBuilder.Append(" PRO_ProductionLine.Code,");
			strSqlBuilder.Append(" ITM_Category.Code,");
			strSqlBuilder.Append(" ITM_Product.ProductID,");
			strSqlBuilder.Append(" ITM_Product.Revision,");
			strSqlBuilder.Append(" ITM_Product.Code,");
			strSqlBuilder.Append(" ITM_Product.Description,");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code");

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}
		
		#endregion

		#region Detailed Item Cost  By Month: Tuan TQ
		public DataTable GetDetailedItemCostByMonthData(string pstrCCNID, string pstrYear, string pstrListDepartmentID, string pstrListProductionLineID, string pstrListCategoryID, string pstrListProductID)
		{
			const string METHOD_NAME = THIS + ".GetDetailedItemCostByMonthData()";
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{
				StringBuilder strSqlBuilder = new StringBuilder();

				strSqlBuilder.Append(" SELECT ");
				strSqlBuilder.Append(" a.MST_DepartmentCode, ");
				strSqlBuilder.Append(" a.PRO_ProductionLineCode,	 ");
				strSqlBuilder.Append(" a.ITM_CategoryCode, ");
				strSqlBuilder.Append(" a.ProductID, ");
				strSqlBuilder.Append(" a.ITM_ProductRevision,  ");
				strSqlBuilder.Append(" a.ITM_ProductCode,  ");
				strSqlBuilder.Append(" a.ITM_ProductDescription,  ");
				strSqlBuilder.Append(" a.MST_UnitOfMeasureCode, ");
				strSqlBuilder.Append(" a.STD_CostElementName, ");
			
				strSqlBuilder.Append(" a.JanActualQuantity as JanQty, ");
				strSqlBuilder.Append(" ( ");
				strSqlBuilder.Append(" ((ISNULL(a.JanCost, 0) * ISNULL(a.JanTotalQuantity, 0)) -  ");
				strSqlBuilder.Append(" (a.JanBeginQuantity * a.PreviousJanCost))/ ");
				strSqlBuilder.Append(" (Case when a.JanActualQuantity = 0 then 1 else ISNULL(a.JanActualQuantity, 1) end) ");
				strSqlBuilder.Append(" ) as JanCost, ");
			
				strSqlBuilder.Append(" a.FebActualQuantity as FebQty, ");
				strSqlBuilder.Append(" ( ");
				strSqlBuilder.Append(" ((ISNULL(a.FebCost, 0) * ISNULL(a.FebTotalQuantity, 0)) -  ");
				strSqlBuilder.Append(" (a.FebBeginQuantity * a.PreviousFebCost))/ ");
				strSqlBuilder.Append(" (Case when a.FebActualQuantity = 0 then 1 else ISNULL(a.FebActualQuantity, 1) end) ");
				strSqlBuilder.Append(" ) as FebCost, ");
			
				strSqlBuilder.Append(" a.MarActualQuantity as MarQty, ");
				strSqlBuilder.Append(" ( ");
				strSqlBuilder.Append(" ((ISNULL(a.MarCost, 0) * ISNULL(a.MarTotalQuantity, 0)) -  ");
				strSqlBuilder.Append(" (a.MarBeginQuantity * a.PreviousMarCost))/ ");
				strSqlBuilder.Append(" (Case when a.MarActualQuantity = 0 then 1 else ISNULL(a.MarActualQuantity, 1) end) ");
				strSqlBuilder.Append(" ) as MarCost, ");
			
				strSqlBuilder.Append(" a.AprActualQuantity as AprQty, ");
				strSqlBuilder.Append(" ( ");
				strSqlBuilder.Append(" ((ISNULL(a.AprCost, 0) * ISNULL(a.AprTotalQuantity, 0)) -  ");
				strSqlBuilder.Append(" (a.AprBeginQuantity * a.PreviousAprCost))/ ");
				strSqlBuilder.Append(" (Case when a.AprActualQuantity = 0 then 1 else ISNULL(a.AprActualQuantity, 1) end) ");
				strSqlBuilder.Append(" ) as AprCost, ");
			
				strSqlBuilder.Append(" a.MayActualQuantity as MayQty, ");
				strSqlBuilder.Append(" ( ");
				strSqlBuilder.Append(" ((ISNULL(a.MayCost, 0) * ISNULL(a.MayTotalQuantity, 0)) -  ");
				strSqlBuilder.Append(" (a.MayBeginQuantity * a.PreviousMayCost))/ ");
				strSqlBuilder.Append(" (Case when a.MayActualQuantity = 0 then 1 else ISNULL(a.MayActualQuantity, 1) end) ");
				strSqlBuilder.Append(" ) as MayCost, ");
			
				strSqlBuilder.Append(" a.JunActualQuantity as JunQty, ");
				strSqlBuilder.Append(" ( ");
				strSqlBuilder.Append(" ((ISNULL(a.JunCost, 0) * ISNULL(a.JunTotalQuantity, 0)) -  ");
				strSqlBuilder.Append(" (a.JunBeginQuantity * a.PreviousJunCost))/ ");
				strSqlBuilder.Append(" (Case when a.JunActualQuantity = 0 then 1 else ISNULL(a.JunActualQuantity, 1) end) ");
				strSqlBuilder.Append(" ) as JunCost, ");
			
				strSqlBuilder.Append(" a.JulActualQuantity as JulQty, ");
				strSqlBuilder.Append(" ( ");
				strSqlBuilder.Append(" ((ISNULL(a.JulCost, 0) * ISNULL(a.JulTotalQuantity, 0)) -  ");
				strSqlBuilder.Append(" (a.JulBeginQuantity * a.PreviousJulCost))/ ");
				strSqlBuilder.Append(" (Case when a.JulActualQuantity = 0 then 1 else ISNULL(a.JulActualQuantity, 1) end) ");
				strSqlBuilder.Append(" ) as JulCost, ");
			
				strSqlBuilder.Append(" a.AugActualQuantity as AugQty, ");
				strSqlBuilder.Append(" ( ");
				strSqlBuilder.Append(" ((ISNULL(a.AugCost, 0) * ISNULL(a.AugTotalQuantity, 0)) -  ");
				strSqlBuilder.Append(" (a.AugBeginQuantity * a.PreviousAugCost))/ ");
				strSqlBuilder.Append(" (Case when a.AugActualQuantity = 0 then 1 else ISNULL(a.AugActualQuantity, 1) end) ");
				strSqlBuilder.Append(" ) as AugCost, ");
			
				strSqlBuilder.Append(" a.SepActualQuantity as SepQty, ");
				strSqlBuilder.Append(" ( ");
				strSqlBuilder.Append(" ((ISNULL(a.SepCost, 0) * ISNULL(a.SepTotalQuantity, 0)) -  ");
				strSqlBuilder.Append(" (a.SepBeginQuantity * a.PreviousSepCost))/ ");
				strSqlBuilder.Append(" (Case when a.SepActualQuantity = 0 then 1 else ISNULL(a.SepActualQuantity, 1) end) ");
				strSqlBuilder.Append(" ) as SepCost, ");
			
				strSqlBuilder.Append(" a.OctActualQuantity as OctQty, ");
				strSqlBuilder.Append(" ( ");
				strSqlBuilder.Append(" ((ISNULL(a.OctCost, 0) * ISNULL(a.OctTotalQuantity, 0)) -  ");
				strSqlBuilder.Append(" (a.OctBeginQuantity * a.PreviousOctCost))/ ");
				strSqlBuilder.Append(" (Case when a.OctActualQuantity = 0 then 1 else ISNULL(a.OctActualQuantity, 1) end) ");
				strSqlBuilder.Append(" ) as OctCost, ");
			
				strSqlBuilder.Append(" a.NovActualQuantity as NovQty, ");
				strSqlBuilder.Append(" ( ");
				strSqlBuilder.Append(" ((ISNULL(a.NovCost, 0) * ISNULL(a.NovTotalQuantity, 0)) -  ");
				strSqlBuilder.Append(" (a.NovBeginQuantity * a.PreviousNovCost))/ ");
				strSqlBuilder.Append(" (Case when a.NovActualQuantity = 0 then 1 else ISNULL(a.NovActualQuantity, 1) end) ");
				strSqlBuilder.Append(" ) as NovCost, ");
			
				strSqlBuilder.Append(" a.DecActualQuantity as DecQty, ");
				strSqlBuilder.Append(" ( ");
				strSqlBuilder.Append(" ((ISNULL(a.DecCost, 0) * ISNULL(a.DecTotalQuantity, 0)) -  ");
				strSqlBuilder.Append(" (a.DecBeginQuantity * a.PreviousDecCost))/ ");
				strSqlBuilder.Append(" (Case when a.DecActualQuantity = 0 then 1 else ISNULL(a.DecActualQuantity, 1) end) ");
				strSqlBuilder.Append(" ) as DecCost ");
			
				strSqlBuilder.Append(" FROM ( ");
				strSqlBuilder.Append(" SELECT  DISTINCT MST_Department.Code AS MST_DepartmentCode,  ");
				strSqlBuilder.Append(" 	PRO_ProductionLine.Code AS PRO_ProductionLineCode,  ");
				strSqlBuilder.Append(" 	ITM_Category.Code as ITM_CategoryCode,  ");
				strSqlBuilder.Append(" 	ITM_Product.ProductID,  ");
				strSqlBuilder.Append(" 	ITM_Product.Revision as ITM_ProductRevision,  ");
				strSqlBuilder.Append(" 	ITM_Product.Code AS ITM_ProductCode,  ");
				strSqlBuilder.Append(" 	STD_CostElement.OrderNo, ");
				strSqlBuilder.Append(" 	ITM_Product.Description as ITM_ProductDescription,  ");
				strSqlBuilder.Append(" 	MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode,  ");
				strSqlBuilder.Append(" 	STD_CostElement.Name as STD_CostElementName,  ");
			
				// January Information
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");			
				strSqlBuilder.Append(" 	       AND ( 1 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as JanTotalQuantity, ");
			
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");			
				strSqlBuilder.Append(" 	       AND ( 1 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as JanBeginQuantity, ");

				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");			
				strSqlBuilder.Append(" 	       AND ( 1 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as JanActualQuantity, ");			

				strSqlBuilder.Append(" 	 ");
				strSqlBuilder.Append(" 	ISNULL( ");
				strSqlBuilder.Append(" 	(   ");
				strSqlBuilder.Append(" 	Case  ");
				strSqlBuilder.Append(" 	When	  ");
				strSqlBuilder.Append(" 	 ( ");
				strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
				strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 		AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 12 AND YEAR(allocMaster.FromDate) = " + pstrYear + " - 1) ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 12 AND YEAR(allocMaster.ToDate) = " + pstrYear + " - 1)) ");
				strSqlBuilder.Append(" 	 )	 ");
				strSqlBuilder.Append(" 	 Is Not Null Then ");
				strSqlBuilder.Append(" 	    ( ");
				strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
				strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			 AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 12 AND YEAR(allocMaster.FromDate) = " + pstrYear + " - 1) ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 12 AND YEAR(allocMaster.ToDate) = " + pstrYear + " - 1)) ");
				strSqlBuilder.Append(" 	    ) ");
				strSqlBuilder.Append(" 	Else  ");
				strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
				strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
				strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	   AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   )		 ");
				strSqlBuilder.Append(" 	End), 0)  ");
				strSqlBuilder.Append(" 	as PreviousJanCost, ");
			
				strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   AND CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	       AND ( 1 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as JanCost, ");
				strSqlBuilder.Append(" 	 ");

				// February Information
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 2 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as FebTotalQuantity, ");
			
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 2 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as FebBeginQuantity, ");

				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 2 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as FebActualQuantity, ");
			
				strSqlBuilder.Append(" 	 ");
				strSqlBuilder.Append(" 	ISNULL( ");
				strSqlBuilder.Append(" 	(   ");
				strSqlBuilder.Append(" 	Case  ");
				strSqlBuilder.Append(" 	When	  ");
				strSqlBuilder.Append(" 	 ( ");
				strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
				strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 		AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 1 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 1 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	 )	 ");
				strSqlBuilder.Append(" 	 Is Not Null Then ");
				strSqlBuilder.Append(" 	    ( ");
				strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
				strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			 AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 1 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 1 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	    ) ");
				strSqlBuilder.Append(" 	Else  ");
				strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
				strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
				strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	   AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   )		 ");
				strSqlBuilder.Append(" 	End), 0)  ");
				strSqlBuilder.Append(" 	as PreviousFebCost, ");
			
				strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   AND CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	       AND ( 2 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as FebCost, ");
			
				// March Information
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 3 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as MarTotalQuantity, ");
			
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 3 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as MarBeginQuantity, ");

				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 3 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as MarActualQuantity, ");

				strSqlBuilder.Append(" 	 ");
				strSqlBuilder.Append(" 	ISNULL( ");
				strSqlBuilder.Append(" 	(   ");
				strSqlBuilder.Append(" 	Case  ");
				strSqlBuilder.Append(" 	When	  ");
				strSqlBuilder.Append(" 	 ( ");
				strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
				strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 		AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 2 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 2 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	 )	 ");
				strSqlBuilder.Append(" 	 Is Not Null Then ");
				strSqlBuilder.Append(" 	    ( ");
				strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
				strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			 AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 			AND ((MONTH(allocMaster.FromDate) = 2 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 2 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	    ) ");
				strSqlBuilder.Append(" 	Else  ");
				strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
				strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
				strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	   AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   )		 ");
				strSqlBuilder.Append(" 	End), 0)  ");
				strSqlBuilder.Append(" 	as PreviousMarCost, ");
			
				strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   AND CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	       AND ( 3 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as MarCost, ");
				strSqlBuilder.Append(" 	 ");
			
				// April Information
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 4 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as AprTotalQuantity, ");
			
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 4 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as AprBeginQuantity, ");

				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 4 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as AprActualQuantity, ");

				strSqlBuilder.Append(" 	 ");
				strSqlBuilder.Append(" 	ISNULL( ");
				strSqlBuilder.Append(" 	(   ");
				strSqlBuilder.Append(" 	Case  ");
				strSqlBuilder.Append(" 	When	  ");
				strSqlBuilder.Append(" 	 ( ");
				strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
				strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 		AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 3 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		OR (MONTH(allocMaster.ToDate) = 3 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	 )	 ");
				strSqlBuilder.Append(" 	 Is Not Null Then ");
				strSqlBuilder.Append(" 	    ( ");
				strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
				strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			 AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 			 AND ((MONTH(allocMaster.FromDate) = 3 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 3 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	    ) ");
				strSqlBuilder.Append(" 	Else  ");
				strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
				strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
				strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	   AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   )		 ");
				strSqlBuilder.Append(" 	End), 0)  ");
				strSqlBuilder.Append(" 	as PreviousAprCost, ");
			
				strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   AND CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	       AND ( 4 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as AprCost, ");
			
				// May Information
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 5 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as MayTotalQuantity, ");
			
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 5 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as MayBeginQuantity, ");

				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 5 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as MayActualQuantity, ");


				strSqlBuilder.Append(" 	 ");
				strSqlBuilder.Append(" 	ISNULL( ");
				strSqlBuilder.Append(" 	(   ");
				strSqlBuilder.Append(" 	Case  ");
				strSqlBuilder.Append(" 	When	  ");
				strSqlBuilder.Append(" 	 ( ");
				strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
				strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 		AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 4 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		OR (MONTH(allocMaster.ToDate) = 4 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	 )	 ");
				strSqlBuilder.Append(" 	 Is Not Null Then ");
				strSqlBuilder.Append(" 	    ( ");
				strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
				strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			 AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 			 AND ((MONTH(allocMaster.FromDate) = 4 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 4 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	    ) ");
				strSqlBuilder.Append(" 	Else  ");
				strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
				strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
				strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	   AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   )		 ");
				strSqlBuilder.Append(" 	End), 0)  ");
				strSqlBuilder.Append(" 	as PreviousMayCost, ");
			
				strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   AND CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	       AND ( 1 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as MayCost, ");
			
				// June Information
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 6 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as JunTotalQuantity, ");
			
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 6 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as JunBeginQuantity, ");

				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 6 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as JunActualQuantity, ");

				strSqlBuilder.Append(" 	 ");
				strSqlBuilder.Append(" 	ISNULL( ");
				strSqlBuilder.Append(" 	(   ");
				strSqlBuilder.Append(" 	Case  ");
				strSqlBuilder.Append(" 	When	  ");
				strSqlBuilder.Append(" 	 ( ");
				strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
				strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 			AND ((MONTH(allocMaster.FromDate) = 5 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		    OR (MONTH(allocMaster.ToDate) = 5 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	 )	 ");
				strSqlBuilder.Append(" 	 Is Not Null Then ");
				strSqlBuilder.Append(" 	    ( ");
				strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
				strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 			AND ((MONTH(allocMaster.FromDate) = 5 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		    OR (MONTH(allocMaster.ToDate) = 5 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	    ) ");
				strSqlBuilder.Append(" 	Else  ");
				strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
				strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
				strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	   AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   )		 ");
				strSqlBuilder.Append(" 	End), 0)  ");
				strSqlBuilder.Append(" 	as PreviousJunCost, ");
			
				strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   AND CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	       AND ( 6 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as JunCost, ");
			
				// July Information
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 7 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as JulTotalQuantity, ");
			
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 7 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as JulBeginQuantity, ");

				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 7 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as JulActualQuantity, ");

				strSqlBuilder.Append(" 	 ");
				strSqlBuilder.Append(" 	ISNULL( ");
				strSqlBuilder.Append(" 	(   ");
				strSqlBuilder.Append(" 	Case  ");
				strSqlBuilder.Append(" 	When	  ");
				strSqlBuilder.Append(" 	 ( ");
				strSqlBuilder.Append("    SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
				strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			 AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 6 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 6 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	 )	 ");
				strSqlBuilder.Append(" 	 Is Not Null Then ");
				strSqlBuilder.Append(" 	    ( ");
				strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
				strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			 AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 6 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 6 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	    ) ");
				strSqlBuilder.Append(" 	Else ");
				strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
				strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
				strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	   AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   ) ");
				strSqlBuilder.Append(" 	End), 0)  ");
				strSqlBuilder.Append(" 	as PreviousJulCost, ");
			
				strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   AND CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	       AND ( 7 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as JulCost, ");
			
				// August Information
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 8 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as AugTotalQuantity, ");
			
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 8 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as AugBeginQuantity, ");

				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 8 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as AugActualQuantity, ");

				strSqlBuilder.Append(" 	 ");
				strSqlBuilder.Append(" 	ISNULL( ");
				strSqlBuilder.Append(" 	(   ");
				strSqlBuilder.Append(" 	Case  ");
				strSqlBuilder.Append(" 	When	  ");
				strSqlBuilder.Append(" 	 ( ");
				strSqlBuilder.Append("    SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
				strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 		AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 7 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		OR (MONTH(allocMaster.ToDate) = 7 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	 )	 ");
				strSqlBuilder.Append(" 	 Is Not Null Then ");
				strSqlBuilder.Append(" 	    ( ");
				strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
				strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 			AND ((MONTH(allocMaster.FromDate) = 7 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		    OR (MONTH(allocMaster.ToDate) = 7 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	    ) ");
				strSqlBuilder.Append(" 	Else  ");
				strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
				strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
				strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	   AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   )		 ");
				strSqlBuilder.Append(" 	End), 0)  ");
				strSqlBuilder.Append(" 	as PreviousAugCost, ");
			
				strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   AND CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	       AND ( 8 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as AugCost, ");
			
				// September Information
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 9 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as SepTotalQuantity, ");
			
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 9 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as SepBeginQuantity, ");

				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 9 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as SepActualQuantity, ");

				strSqlBuilder.Append(" 	 ");
				strSqlBuilder.Append(" 	ISNULL( ");
				strSqlBuilder.Append(" 	(   ");
				strSqlBuilder.Append(" 	Case  ");
				strSqlBuilder.Append(" 	When	  ");
				strSqlBuilder.Append(" 	 ( ");
				strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
				strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 		AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 8 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		OR (MONTH(allocMaster.ToDate) = 8 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	 )	 ");
				strSqlBuilder.Append(" 	 Is Not Null Then ");
				strSqlBuilder.Append(" 	    ( ");
				strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
				strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			 AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 			 AND ((MONTH(allocMaster.FromDate) = 8 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 8 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	    ) ");
				strSqlBuilder.Append(" 	Else  ");
				strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
				strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
				strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");			
				strSqlBuilder.Append(" 			AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   )	");
				strSqlBuilder.Append(" 	End), 0)  ");
				strSqlBuilder.Append(" 	as PreviousSepCost, ");
			
				strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   AND CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	       AND ( 9 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as SepCost, ");
			
				// October Information
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 10 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as OctTotalQuantity, ");			
			
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 10 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as OctBeginQuantity, ");

				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 10 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as OctActualQuantity, ");

				strSqlBuilder.Append(" 	 ");
				strSqlBuilder.Append(" 	ISNULL( ");
				strSqlBuilder.Append(" 	(   ");
				strSqlBuilder.Append(" 	Case  ");
				strSqlBuilder.Append(" 	When	  ");
				strSqlBuilder.Append(" 	 ( ");
				strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
				strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 		  AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		  AND ((MONTH(allocMaster.FromDate) = 9 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   OR (MONTH(allocMaster.ToDate) = 9 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	 )	 ");
				strSqlBuilder.Append(" 	 Is Not Null Then ");
				strSqlBuilder.Append(" 	    ( ");
				strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
				strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			 AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		     AND ((MONTH(allocMaster.FromDate) = 9 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 9 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	    ) ");
				strSqlBuilder.Append(" 	Else  ");
				strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
				strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
				strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	   AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   )		 ");
				strSqlBuilder.Append(" 	End), 0)  ");
				strSqlBuilder.Append(" 	as PreviousOctCost, ");
			
				strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   AND CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	       AND ( 10 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as OctCost, ");
			
				// November Information
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 11 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as NovTotalQuantity, ");
			
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 11 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as NovBeginQuantity, ");

				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 	       AND ( 11 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as NovActualQuantity, ");

				strSqlBuilder.Append(" 	 ");
				strSqlBuilder.Append(" 	ISNULL( ");
				strSqlBuilder.Append(" 	(   ");
				strSqlBuilder.Append(" 	Case  ");
				strSqlBuilder.Append(" 	When	  ");
				strSqlBuilder.Append(" 	 ( ");
				strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
				strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 		AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 10 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		OR (MONTH(allocMaster.ToDate) = 10 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	 )	 ");
				strSqlBuilder.Append(" 	 Is Not Null Then ");
				strSqlBuilder.Append(" 	    ( ");
				strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
				strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		    AND ((MONTH(allocMaster.FromDate) = 10 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		    OR (MONTH(allocMaster.ToDate) = 10 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	    ) ");
				strSqlBuilder.Append(" 	Else  ");
				strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
				strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
				strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			 AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   )		 ");
				strSqlBuilder.Append(" 	End), 0)  ");
				strSqlBuilder.Append(" 	as PreviousNovCost, ");
			
				strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   AND CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	       AND ( 11 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as NovCost, ");
			
				// December Information
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");			
				strSqlBuilder.Append(" 	       AND ( 12 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as DecTotalQuantity, ");
			
				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 BeginQuantity ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");			
				strSqlBuilder.Append(" 	       AND ( 12 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as DecBeginQuantity, ");

				strSqlBuilder.Append(" 	( ");
				strSqlBuilder.Append(" 	SELECT TOP 1 Quantity - BeginQuantity");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory  ");
				strSqlBuilder.Append(" 	 INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");			
				strSqlBuilder.Append(" 	       AND ( 12 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as DecActualQuantity, ");

				strSqlBuilder.Append(" 	 ");
				strSqlBuilder.Append(" 	ISNULL( ");
				strSqlBuilder.Append(" 	(   ");
				strSqlBuilder.Append(" 	Case  ");
				strSqlBuilder.Append(" 	When	  ");
				strSqlBuilder.Append(" 	 ( ");
				strSqlBuilder.Append("           SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	  FROM CST_ActualCostHistory actHis	 ");
				strSqlBuilder.Append(" 	     INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	  WHERE actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 		AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 		AND ((MONTH(allocMaster.FromDate) = 11 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		OR (MONTH(allocMaster.ToDate) = 11 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	 )	");
				strSqlBuilder.Append(" 	 Is Not Null Then ");
				strSqlBuilder.Append(" 	    ( ");
				strSqlBuilder.Append(" 	     SELECT SUM(actHis.ActualCost) ");
				strSqlBuilder.Append(" 	     FROM CST_ActualCostHistory actHis ");
				strSqlBuilder.Append(" 	         INNER JOIN cst_ActCostAllocationMaster allocMaster ON actHis.ActCostAllocationMasterID = allocMaster.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	     WHERE  actHis.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 			 AND actHis.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 			 AND ((MONTH(allocMaster.FromDate) = 11 AND YEAR(allocMaster.FromDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		     OR (MONTH(allocMaster.ToDate) = 11 AND YEAR(allocMaster.ToDate) = " + pstrYear + ")) ");
				strSqlBuilder.Append(" 	    ) ");
				strSqlBuilder.Append(" 	Else  ");
				strSqlBuilder.Append(" 	  (SELECT SUM(Cost)   ");
				strSqlBuilder.Append(" 	   FROM CST_STDItemCost ");
				strSqlBuilder.Append(" 	   WHERE ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	   AND CST_STDItemCost.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	   )	");
				strSqlBuilder.Append(" 	End), 0)  ");
				strSqlBuilder.Append(" 	as PreviousDecCost, ");
			
				strSqlBuilder.Append(" 	(SELECT SUM(ActualCost) ");
				strSqlBuilder.Append(" 	 FROM CST_ActualCostHistory INNER JOIN cst_ActCostAllocationMaster ON cst_ActCostAllocationMaster.ActCostAllocationMasterID = CST_ActualCostHistory.ActCostAllocationMasterID ");
				strSqlBuilder.Append(" 	 WHERE (Year(cst_ActCostAllocationMaster.FromDate) = " + pstrYear + " OR Year(cst_ActCostAllocationMaster.ToDate) = " + pstrYear + ") ");
				strSqlBuilder.Append(" 		   AND CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	       AND ( 1 BETWEEN Month(cst_ActCostAllocationMaster.FromDate) AND Month(cst_ActCostAllocationMaster.ToDate)) ");
				strSqlBuilder.Append(" 	       AND CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	) as DecCost ");
			
				strSqlBuilder.Append(" FROM    ITM_Product ");
				strSqlBuilder.Append(" 	INNER JOIN CST_ActualCostHistory ON CST_ActualCostHistory.ProductID = ITM_Product.ProductID ");
				strSqlBuilder.Append(" 	INNER JOIN STD_CostElement ON CST_ActualCostHistory.CostElementID = STD_CostElement.CostElementID ");
				strSqlBuilder.Append(" 	INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID ");
				strSqlBuilder.Append(" 	LEFT JOIN PRO_WorkOrderCompletion ON PRO_WorkOrderCompletion.ProductID = ITM_Product.ProductID AND Year(PRO_WorkOrderCompletion.PostDate) = " + pstrYear + " ");
				strSqlBuilder.Append(" 	LEFT JOIN PRO_ProductionLine ON ITM_Product.ProductionLineID = PRO_ProductionLine.ProductionLineID ");
				strSqlBuilder.Append(" 	LEFT JOIN MST_Department ON PRO_ProductionLine.DepartmentID = MST_Department.DepartmentID   ");
				strSqlBuilder.Append(" 	LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID  ");
			
				strSqlBuilder.Append(" WHERE ITM_Product.CCNID = " + pstrCCNID);
			
				if(pstrListDepartmentID != string.Empty)
				{
					strSqlBuilder.Append(" AND MST_Department.DepartmentID IN " + pstrListDepartmentID);
				}

				if(pstrListProductionLineID != string.Empty)
				{
					strSqlBuilder.Append(" AND ITM_Product.ProductionLineID IN " + pstrListProductionLineID);
				}

				if(pstrListCategoryID != string.Empty)
				{
					strSqlBuilder.Append(" AND ITM_Product.CategoryID IN " + pstrListCategoryID);
				}

				if(pstrListProductID != string.Empty)
				{
					strSqlBuilder.Append(" AND ITM_Product.ProductID IN " + pstrListProductID);
				}		

				strSqlBuilder.Append(" ) a ");
			
				strSqlBuilder.Append(" WHERE a.JanCost > 0 ");
				strSqlBuilder.Append("  OR a.FebCost > 0	 ");
				strSqlBuilder.Append("  OR a.MarCost > 0 ");
				strSqlBuilder.Append("  OR a.AprCost > 0 ");
				strSqlBuilder.Append("  OR a.MayCost > 0 ");
				strSqlBuilder.Append("  OR a.JunCost > 0 ");
				strSqlBuilder.Append("  OR a.JulCost > 0 ");
				strSqlBuilder.Append("  OR a.AugCost > 0 ");
				strSqlBuilder.Append("  OR a.SepCost > 0 ");
				strSqlBuilder.Append("  OR a.OctCost > 0 ");
				strSqlBuilder.Append("  OR a.NovCost > 0 ");
				strSqlBuilder.Append("  OR a.DecCost > 0 ");			
			
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
				ocmdPCS.CommandTimeout = 1000;
			
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResult);

				return dtbResult;
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		
		#endregion
		
		#region PO Slip Data: Tuan TQ
		public DataTable GetPOSlipData(int pintPOReceiptMasterID)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			StringBuilder strSqlBuilder = new StringBuilder();

			strSqlBuilder.Append("SELECT DISTINCT MST_MasterLocation.Code + ' (' + MST_MasterLocation.Name + ')'  as MasterLocation,");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptMaster.ReceiveNo,");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptMaster.PostDate,");
			strSqlBuilder.Append(" PO_InvoiceMaster.InvoiceNo,");
			strSqlBuilder.Append(" Case PO_PurchaseOrderReceiptMaster.ReceiptType");
			strSqlBuilder.Append(" When " + (int)POReceiptTypeEnum.ByInvoice + " then '(Import)'");
			strSqlBuilder.Append(" When " + (int)POReceiptTypeEnum.ByOutside + " then '(From Maker)'");
			strSqlBuilder.Append(" Else '(Domestic)'");
			strSqlBuilder.Append(" End as ReceiptType,");
			strSqlBuilder.Append(" PO_PurchaseOrderMaster.Code AS PONo,");
			strSqlBuilder.Append(" PO_PurchaseOrderDetail.Line as POLine,");
			strSqlBuilder.Append(" ITM_Product.Code AS PartNo,");
			strSqlBuilder.Append(" ITM_Product.Description as PartName,");
			strSqlBuilder.Append(" ITM_Product.Revision as PartModel,");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code as BuyingUM,");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptDetail.ReceiveQuantity,");
			strSqlBuilder.Append(" MST_Location.Code AS LocationCode,");
			strSqlBuilder.Append(" MST_BIN.Code AS BinCode, PO_PurchaseOrderReceiptDetail.DeliveryScheduleID, ");
			
			strSqlBuilder.Append(" Case ");
			strSqlBuilder.Append("    When PO_PurchaseOrderReceiptMaster.InvoiceMasterID is null then POParty.Code");
			strSqlBuilder.Append("    Else InvoiceParty.Code");
			strSqlBuilder.Append(" End as PartyCode,");
			strSqlBuilder.Append(" Case ");
			strSqlBuilder.Append("    When PO_PurchaseOrderReceiptMaster.InvoiceMasterID is null then POParty.Name");
			strSqlBuilder.Append("    Else InvoiceParty.Name");
			strSqlBuilder.Append(" End as PartyName,");
			
			strSqlBuilder.Append(" Case ");
			strSqlBuilder.Append("    When PO_PurchaseOrderReceiptMaster.InvoiceMasterID is null then POInMaster.ExchangeRate");
			strSqlBuilder.Append("    Else PO_InvoiceMaster.ExchangeRate");
			strSqlBuilder.Append(" End as ExchangeRate,");

			strSqlBuilder.Append(" Case ");
			strSqlBuilder.Append("    When PO_PurchaseOrderReceiptMaster.InvoiceMasterID is null then POCurrency.Code");
			strSqlBuilder.Append("    Else InvoiceCurrency.Code");
			strSqlBuilder.Append(" End as CurrencyCode,");
			
			strSqlBuilder.Append(" Case");
			strSqlBuilder.Append("    When PO_PurchaseOrderReceiptMaster.InvoiceMasterID is null then PO_PurchaseOrderDetail.UnitPrice");
			strSqlBuilder.Append("    Else PO_InvoiceDetail.CIPAmount/PO_InvoiceDetail.InvoiceQuantity");
			strSqlBuilder.Append(" End as UnitPrice,");
			strSqlBuilder.Append(" PO_DeliverySchedule.ScheduleDate,");
			strSqlBuilder.Append(" PO_DeliverySchedule.DeliveryQuantity");

			strSqlBuilder.Append(" FROM    PO_PurchaseOrderReceiptMaster");
			strSqlBuilder.Append(" INNER JOIN PO_PurchaseOrderReceiptDetail ON PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID");
			strSqlBuilder.Append(" INNER JOIN ITM_Product ON ITM_Product.ProductID = PO_PurchaseOrderReceiptDetail.ProductID");
			strSqlBuilder.Append(" INNER JOIN MST_UnitOfMeasure ON PO_PurchaseOrderReceiptDetail.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID");
			strSqlBuilder.Append(" INNER JOIN MST_MasterLocation ON PO_PurchaseOrderReceiptMaster.MasterLocationID = MST_MasterLocation.MasterLocationID");
			
			strSqlBuilder.Append(" LEFT JOIN PO_DeliverySchedule ON PO_DeliverySchedule.DeliveryScheduleID = PO_PurchaseOrderReceiptDetail.DeliveryScheduleID");
			strSqlBuilder.Append(" LEFT JOIN PO_PurchaseOrderMaster POInMaster ON PO_PurchaseOrderReceiptMaster.PurchaseOrderMasterID = POInMaster.PurchaseOrderMasterID");

			strSqlBuilder.Append(" LEFT JOIN MST_Location ON PO_PurchaseOrderReceiptDetail.LocationID = MST_Location.LocationID");
			strSqlBuilder.Append(" LEFT JOIN MST_BIN ON MST_BIN.BinID = PO_PurchaseOrderReceiptDetail.BinID");
			
			strSqlBuilder.Append(" LEFT JOIN PO_PurchaseOrderMaster ON PO_PurchaseOrderReceiptDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"); 
			strSqlBuilder.Append(" LEFT JOIN PO_PurchaseOrderDetail ON PO_PurchaseOrderReceiptDetail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"); 
			strSqlBuilder.Append(" LEFT JOIN PO_InvoiceMaster ON PO_PurchaseOrderReceiptMaster.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID");
			
			strSqlBuilder.Append(" LEFT JOIN MST_Party InvoiceParty ON PO_InvoiceMaster.PartyID = InvoiceParty.PartyID");
			strSqlBuilder.Append(" LEFT JOIN MST_Party POParty ON PO_PurchaseOrderMaster.PartyID = POParty.PartyID");
			
			strSqlBuilder.Append(" LEFT JOIN MST_Currency InvoiceCurrency ON InvoiceCurrency.CurrencyID = PO_InvoiceMaster.CurrencyID");
			strSqlBuilder.Append(" LEFT JOIN MST_Currency POCurrency ON POCurrency.CurrencyID = POInMaster.CurrencyID");
			strSqlBuilder.Append(" LEFT JOIN PO_InvoiceDetail ON PO_InvoiceDetail.InvoiceMasterID = PO_PurchaseOrderReceiptMaster.InvoiceMasterID AND PO_InvoiceDetail.PurchaseOrderDetailID = PO_PurchaseOrderReceiptDetail.PurchaseOrderDetailID");
			
			strSqlBuilder.Append(" WHERE PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = " + pintPOReceiptMasterID);

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}
		
		#endregion

		#region PO Slip Data: CanhNv
		public DataTable GetPOSlipDatavedor(int pintPOReceiptMasterID)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			StringBuilder strSqlBuilder = new StringBuilder();

			strSqlBuilder.Append("SELECT P.Code PartNo,");
			strSqlBuilder.Append(" P.Description PartName,");
			strSqlBuilder.Append(" P.Revision Model,");
			strSqlBuilder.Append(" UM.Code Unit,");
			strSqlBuilder.Append(" L.Code FromLoc,");
			strSqlBuilder.Append(" B.Code FromBin,");
			strSqlBuilder.Append(" RD.Quantity");

			strSqlBuilder.Append(" FROM PO_ReturnToVendorDetail RD");
			strSqlBuilder.Append(" inner join PO_ReturnToVendorMaster RM On RM.ReturnToVendorMasterID=RD.ReturnToVendorMasterID");
			strSqlBuilder.Append(" inner join ITM_Product P On P.ProductID = RD.ProductID");
			strSqlBuilder.Append(" inner join MST_UnitOfMeasure UM on UM.UnitOfMeasureID=P.StockUMID"); 
			strSqlBuilder.Append(" inner join MST_Location L On L.LocationID=RD.LocationID"); 
			strSqlBuilder.Append(" inner join MST_Bin B on B.BinID=RD.BinID");
			strSqlBuilder.Append(" Where RD.ReturnToVendorMasterID = " + pintPOReceiptMasterID.ToString().Trim());

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}
		
		#endregion

		#region SO Shipping Data - Tuan TQ

		/// <summary>
		/// Get SO Shipping MasterData for Importing Invoice Report 
		/// </summary>
		/// <param name="pintShippingMasterID"></param>
		/// <returns></returns>
		/// <author> Tuan TQ</author>
		/// <Created Date> 01 June, 2006</Created>
		public DataTable GetSOShippingMasterData4ImportInvoice(int pintShippingMasterID)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			StringBuilder strSqlBuilder = new StringBuilder();
			
			strSqlBuilder.Append("SELECT  SO_ConfirmShipMaster.ConfirmShipMasterID, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.InvoiceNo as ConfirmShipNo,  ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.ShippedDate, ");			
			
			strSqlBuilder.Append(" SO_ConfirmShipMaster.ReferenceNo as CustomerPurchaseOrderNo, ");

			strSqlBuilder.Append(" MST_Country.Name as LocationCountry, ");
			strSqlBuilder.Append(" MST_Party.Name AS PartyName, ");
			strSqlBuilder.Append(" MST_Party.Address AS PartyAddress, ");
			strSqlBuilder.Append(" partyCountry.Name as PartyCountry, ");
			strSqlBuilder.Append(" MST_Currency.Code CurrencyCode, ");

			strSqlBuilder.Append(" SO_ConfirmShipMaster.Comment, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.IssuingBank, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.OnBoardDate, ");

			strSqlBuilder.Append(" SO_ConfirmShipMaster.LCNo, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.LCDate, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.Measurement, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.GrossWeight, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.NetWeight,	 ");
			
			strSqlBuilder.Append(" SO_ConfirmShipMaster.CNo, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.VesselName, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.FromPort, ");
			strSqlBuilder.Append(" shippingLoc.Description as ShippingLocationName, ");
			strSqlBuilder.Append(" MST_PaymentTerm.Description as PaymentTerm,	 ");	
			strSqlBuilder.Append(" MST_PartyContact.Name as PartyContactName, ");
			strSqlBuilder.Append(" MST_PartyContact.Phone as PartyContactPhone, ");
			strSqlBuilder.Append(" MST_PartyContact.Fax as PartyContactFax, ");
			strSqlBuilder.Append(" MST_Carrier.Name as CarrierName, ");

			strSqlBuilder.Append(" (SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append("  FROM SO_ConfirmShipDetail detail");
			strSqlBuilder.Append("  WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID ");
			strSqlBuilder.Append("  ) as TotalQuantity, ");
			
			strSqlBuilder.Append(" (SELECT SUM( detail.Price * detail.InvoiceQty) ");
			strSqlBuilder.Append("  FROM SO_ConfirmShipDetail detail");
			strSqlBuilder.Append("  WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID ");
			strSqlBuilder.Append("  ) as TotalAmount ");

			strSqlBuilder.Append(" FROM    SO_ConfirmShipMaster ");
			strSqlBuilder.Append(" INNER JOIN MST_Currency ON SO_ConfirmShipMaster.CurrencyID = MST_Currency.CurrencyID  ");
			strSqlBuilder.Append(" INNER JOIN SO_SaleOrderMaster ON SO_ConfirmShipMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID  ");
			strSqlBuilder.Append(" INNER JOIN MST_Party ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID ");
			strSqlBuilder.Append(" LEFT JOIN MST_PartyContact ON SO_SaleOrderMaster.PartyContactID = MST_PartyContact.PartyContactID ");
			strSqlBuilder.Append(" LEFT JOIN MST_Country partyCountry ON partyCountry.CountryID = MST_Party.CountryID ");
			strSqlBuilder.Append(" LEFT JOIN MST_PaymentTerm ON SO_SaleOrderMaster.PaymentTermsID = MST_PaymentTerm.PaymentTermID ");
			strSqlBuilder.Append(" LEFT JOIN MST_PartyLocation shippingLoc ON shippingLoc.PartyLocationID = SO_SaleOrderMaster.ShipToLocID  ");
			strSqlBuilder.Append(" LEFT JOIN MST_Country ON shippingLoc.CountryID = MST_Country.CountryID ");
			strSqlBuilder.Append(" LEFT JOIN MST_Carrier ON MST_Carrier.CarrierID = SO_SaleOrderMaster.CarrierID ");

			strSqlBuilder.Append(" WHERE SO_ConfirmShipMaster.ConfirmShipMasterID = " + pintShippingMasterID.ToString()); 			

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;

		}

		
		/// <summary>
		/// Get SO Shipping Detail for Importing Invoice Report 
		/// </summary>
		/// <param name="pintShippingMasterID"></param>
		/// <returns></returns>
		/// <author> Tuan TQ</author>
		/// <Created Date> 01 June, 2006</Created>
		public DataTable GetSOShippingDetailData4ImportInvoice(int pintShippingMasterID)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			StringBuilder strSqlBuilder = new StringBuilder();

			strSqlBuilder.Append(" SELECT  SO_ConfirmShipMaster.ConfirmShipMasterID, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.InvoiceNo as ConfirmShipNo, "); 
			strSqlBuilder.Append(" SO_ConfirmShipMaster.ShippedDate, ");
			
			strSqlBuilder.Append(" SO_ConfirmShipMaster.ReferenceNo as CustomerPurchaseOrderNo, ");			
			
			strSqlBuilder.Append(" MST_Country.Name as LocationCountry, ");
			strSqlBuilder.Append(" MST_Party.Name AS PartyName, ");
			strSqlBuilder.Append(" MST_Party.Address AS PartyAddress, ");
	
			strSqlBuilder.Append(" MST_Party.MAPBankAccountNo, ");
			strSqlBuilder.Append(" Case when CharIndex('" + DEMILITER_CHAR + "', MST_Party.MAPBankAccountName) > 0 then ");
            strSqlBuilder.Append("      Substring(MST_Party.MAPBankAccountName, 1, CharIndex('" + DEMILITER_CHAR + "', MST_Party.MAPBankAccountName) - 1)  ");
			strSqlBuilder.Append(" else MST_Party.MAPBankAccountName ");
			strSqlBuilder.Append(" End As MAPBankAccountName, ");

			strSqlBuilder.Append(" Case when CharIndex('" + DEMILITER_CHAR + "', MST_Party.MAPBankAccountName) > 0 then ");
            strSqlBuilder.Append("      Substring(MST_Party.MAPBankAccountName, CharIndex('" + DEMILITER_CHAR + "', MST_Party.MAPBankAccountName) + 1, Len(MST_Party.MAPBankAccountName)) ");
			strSqlBuilder.Append(" else '' ");
			strSqlBuilder.Append(" End As MAPBankAccountAddress, ");

			strSqlBuilder.Append(" partyCountry.Name as PartyCountry, ");
			strSqlBuilder.Append(" MST_Currency.Code CurrencyCode, ");
			
			strSqlBuilder.Append(" ((SELECT SUM( detail.Price * detail.InvoiceQty)");
			strSqlBuilder.Append(" FROM SO_ConfirmShipDetail detail");
			strSqlBuilder.Append(" WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID");
			strSqlBuilder.Append(" AND detail.ProductID = ITM_Product.ProductID");
			strSqlBuilder.Append(" )	");
			strSqlBuilder.Append(" /");
			
			//validate data to avoid division by zero error 
			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" Case  ");
			strSqlBuilder.Append(" When ( ");
			strSqlBuilder.Append(" SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append(" FROM SO_ConfirmShipDetail detail ");
			strSqlBuilder.Append(" WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID ");
			strSqlBuilder.Append(" AND detail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append("    ) = 0  ");
			strSqlBuilder.Append(" then 1 ");
			strSqlBuilder.Append("     else   "); 
  			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append(" FROM SO_ConfirmShipDetail detail ");
			strSqlBuilder.Append(" WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID ");
			strSqlBuilder.Append(" AND detail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append("    ) ");
			strSqlBuilder.Append(" end      "); 
			strSqlBuilder.Append(" ) ");
			//end validate

			strSqlBuilder.Append(" )	");
			strSqlBuilder.Append(" as AVGPrice,");

			strSqlBuilder.Append(" SO_ConfirmShipMaster.Comment, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.IssuingBank, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.OnBoardDate, ");			
			strSqlBuilder.Append(" SO_ConfirmShipMaster.Measurement, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.GrossWeight, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.NetWeight,	 ");

			strSqlBuilder.Append(" SO_ConfirmShipMaster.LCNo, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.LCDate, ");

			strSqlBuilder.Append(" SO_ConfirmShipMaster.CNo, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.VesselName, ");
			strSqlBuilder.Append(" SO_ConfirmShipMaster.FromPort, ");
			strSqlBuilder.Append(" shippingLoc.Description as ShippingLocationName, ");
			strSqlBuilder.Append(" MST_PaymentTerm.Description as PaymentTerm,	 ");	
			strSqlBuilder.Append(" MST_PartyContact.Name as PartyContactName, ");
			strSqlBuilder.Append(" MST_PartyContact.Phone as PartyContactPhone, ");
			strSqlBuilder.Append(" MST_PartyContact.Fax as PartyContactFax,	 ");
			strSqlBuilder.Append(" MST_Carrier.Name as CarrierName, ");

			strSqlBuilder.Append(" (SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append(" FROM SO_ConfirmShipDetail detail");
			strSqlBuilder.Append("  WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID ");
			strSqlBuilder.Append("  ) as TotalQuantity, ");

			strSqlBuilder.Append(" (SELECT SUM( detail.Price * detail.InvoiceQty) ");
			strSqlBuilder.Append("  FROM SO_ConfirmShipDetail detail");
			strSqlBuilder.Append("  WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID ");
			strSqlBuilder.Append("  ) as TotalAmount,	 ");

			strSqlBuilder.Append(" ITM_Product.ProductID, ");
			strSqlBuilder.Append(" SO_ConfirmShipDetail.ConfirmShipDetailID, ");
			strSqlBuilder.Append(" SO_CustomerItemRefDetail.CustomerItemCode as PartNo, ");
			strSqlBuilder.Append(" ITM_Product.Description as PartName, ");
			strSqlBuilder.Append(" ITM_Product.Revision as PartModel, ");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode, ");
			strSqlBuilder.Append(" ITM_Category.Name as CategoryName,  ");
			
			strSqlBuilder.Append(" ITM_Product.QuantitySet, ");

			strSqlBuilder.Append(" (SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append("  FROM SO_ConfirmShipDetail detail");
			strSqlBuilder.Append("  WHERE detail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID");
	        strSqlBuilder.Append("  AND detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID");			
			strSqlBuilder.Append("  AND detail.ConfirmShipDetailID = SO_ConfirmShipDetail.ConfirmShipDetailID ");
			strSqlBuilder.Append(" ) as ShippingQuantity, ");

			strSqlBuilder.Append(" SO_ConfirmShipDetail.Price as UnitPrice, ");

			strSqlBuilder.Append(" (SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append("  FROM SO_ConfirmShipDetail detail ");
			strSqlBuilder.Append(" 	WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID ");
			strSqlBuilder.Append("  ) * SO_ConfirmShipDetail.Price AS NetAmount ");

			strSqlBuilder.Append(" FROM SO_ConfirmShipDetail ");
			strSqlBuilder.Append(" 	INNER JOIN SO_ConfirmShipMaster ON SO_ConfirmShipDetail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID ");
			strSqlBuilder.Append(" 	INNER JOIN SO_SaleOrderMaster ON SO_ConfirmShipMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID ");
			strSqlBuilder.Append(" 	INNER JOIN SO_SaleOrderDetail ON SO_ConfirmShipDetail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID  ");
			strSqlBuilder.Append(" 	INNER JOIN ITM_Product ON ITM_Product.ProductID = SO_SaleOrderDetail.ProductID ");
			strSqlBuilder.Append(" 	INNER JOIN MST_UnitOfMeasure ON SO_SaleOrderDetail.SellingUMID = MST_UnitOfMeasure.UnitOfMeasureID ");
			strSqlBuilder.Append(" 	INNER JOIN MST_Currency ON SO_ConfirmShipMaster.CurrencyID = MST_Currency.CurrencyID ");
			strSqlBuilder.Append(" 	INNER JOIN MST_Party ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID ");
			strSqlBuilder.Append("	LEFT JOIN MST_PartyContact ON SO_SaleOrderMaster.PartyContactID = MST_PartyContact.PartyContactID ");
			strSqlBuilder.Append("  LEFT JOIN MST_Country partyCountry ON partyCountry.CountryID = MST_Party.CountryID ");
			strSqlBuilder.Append("  LEFT JOIN MST_PaymentTerm ON SO_SaleOrderMaster.PaymentTermsID = MST_PaymentTerm.PaymentTermID ");
			strSqlBuilder.Append("  LEFT JOIN MST_PartyLocation shippingLoc ON shippingLoc.PartyLocationID = SO_SaleOrderMaster.ShipToLocID ");
			strSqlBuilder.Append("  LEFT JOIN MST_Country ON shippingLoc.CountryID = MST_Country.CountryID ");
			strSqlBuilder.Append("  LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID ");
			strSqlBuilder.Append("  LEFT JOIN MST_Carrier ON MST_Carrier.CarrierID = SO_SaleOrderMaster.CarrierID ");
			
			strSqlBuilder.Append("  LEFT JOIN SO_CustomerItemRefMaster ON SO_CustomerItemRefMaster.PartyID = MST_Party.PartyID ");
			strSqlBuilder.Append("  LEFT JOIN SO_CustomerItemRefDetail ON SO_CustomerItemRefMaster.CustomerItemRefMasterID = SO_CustomerItemRefDetail.CustomerItemRefMasterID ");
			strSqlBuilder.Append("		AND SO_CustomerItemRefDetail.ProductID = ITM_Product.ProductID ");

			strSqlBuilder.Append(" WHERE SO_ConfirmShipMaster.ConfirmShipMasterID = " + pintShippingMasterID.ToString());
			
			strSqlBuilder.Append(" AND (SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append("  FROM SO_ConfirmShipDetail detail");
			strSqlBuilder.Append("  WHERE detail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID");
			strSqlBuilder.Append("  AND detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID");			
			strSqlBuilder.Append(" ) > 0");

			strSqlBuilder.Append(" ORDER BY ITM_Product.Revision, ITM_Product.Description ");

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}

		public DataTable GetSOInvoiceDetailData4ImportInvoice(int pintSOInvoiceMasterID)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			StringBuilder strSqlBuilder = new StringBuilder();

			strSqlBuilder.Append(" SELECT  SO_InvoiceMaster.InvoiceMasterID, ");
			strSqlBuilder.Append(" SO_InvoiceMaster.InvoiceNo as ConfirmShipNo, "); 
			strSqlBuilder.Append(" SO_InvoiceMaster.ShippedDate, ");
			
			strSqlBuilder.Append(" SO_InvoiceMaster.ReferenceNo as CustomerPurchaseOrderNo, ");			
			
			strSqlBuilder.Append(" MST_Country.Name as LocationCountry, ");
			strSqlBuilder.Append(" MST_Party.Name AS PartyName, ");
			strSqlBuilder.Append(" MST_Party.Address AS PartyAddress, ");
	
			strSqlBuilder.Append(" MST_Party.MAPBankAccountNo, ");
			strSqlBuilder.Append(" Case when CharIndex('" + DEMILITER_CHAR + "', MST_Party.MAPBankAccountName) > 0 then ");
			strSqlBuilder.Append("      Substring(MST_Party.MAPBankAccountName, 1, CharIndex('" + DEMILITER_CHAR + "', MST_Party.MAPBankAccountName) - 1)  ");
			strSqlBuilder.Append(" else MST_Party.MAPBankAccountName ");
			strSqlBuilder.Append(" End As MAPBankAccountName, ");

			strSqlBuilder.Append(" Case when CharIndex('" + DEMILITER_CHAR + "', MST_Party.MAPBankAccountName) > 0 then ");
			strSqlBuilder.Append("      Substring(MST_Party.MAPBankAccountName, CharIndex('" + DEMILITER_CHAR + "', MST_Party.MAPBankAccountName) + 1, Len(MST_Party.MAPBankAccountName)) ");
			strSqlBuilder.Append(" else '' ");
			strSqlBuilder.Append(" End As MAPBankAccountAddress, ");

			strSqlBuilder.Append(" partyCountry.Name as PartyCountry, ");
			strSqlBuilder.Append(" MST_Currency.Code CurrencyCode, ");
			
			strSqlBuilder.Append(" ((SELECT SUM( detail.Price * detail.InvoiceQty)");
			strSqlBuilder.Append(" FROM SO_InvoiceDetail detail");
			strSqlBuilder.Append(" WHERE detail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID");
			strSqlBuilder.Append(" AND detail.ProductID = ITM_Product.ProductID");
			strSqlBuilder.Append(" )	");
			strSqlBuilder.Append(" /");
			
			//validate data to avoid division by zero error 
			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" Case  ");
			strSqlBuilder.Append(" When ( ");
			strSqlBuilder.Append(" SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append(" FROM SO_InvoiceDetail detail ");
			strSqlBuilder.Append(" WHERE detail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID ");
			strSqlBuilder.Append(" AND detail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append("    ) = 0  ");
			strSqlBuilder.Append(" then 1 ");
			strSqlBuilder.Append("     else   "); 
			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append(" FROM SO_InvoiceDetail detail ");
			strSqlBuilder.Append(" WHERE detail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID ");
			strSqlBuilder.Append(" AND detail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append("    ) ");
			strSqlBuilder.Append(" end      "); 
			strSqlBuilder.Append(" ) ");
			//end validate

			strSqlBuilder.Append(" )	");
			strSqlBuilder.Append(" as AVGPrice,");

			strSqlBuilder.Append(" SO_InvoiceMaster.Comment, ");
			strSqlBuilder.Append(" SO_InvoiceMaster.IssuingBank, ");
			strSqlBuilder.Append(" SO_InvoiceMaster.OnBoardDate, ");			
			strSqlBuilder.Append(" SO_InvoiceMaster.Measurement, ");
			strSqlBuilder.Append(" SO_InvoiceMaster.GrossWeight, ");
			strSqlBuilder.Append(" SO_InvoiceMaster.NetWeight,	 ");

			strSqlBuilder.Append(" SO_InvoiceMaster.LCNo, ");
			strSqlBuilder.Append(" SO_InvoiceMaster.LCDate, ");

			strSqlBuilder.Append(" SO_InvoiceMaster.CNo, ");
			strSqlBuilder.Append(" SO_InvoiceMaster.VesselName, ");
			strSqlBuilder.Append(" SO_InvoiceMaster.FromPort, ");
			strSqlBuilder.Append(" shippingLoc.Description as ShippingLocationName, ");
			strSqlBuilder.Append(" MST_PaymentTerm.Description as PaymentTerm,	 ");	
			strSqlBuilder.Append(" MST_PartyContact.Name as PartyContactName, ");
			strSqlBuilder.Append(" MST_PartyContact.Phone as PartyContactPhone, ");
			strSqlBuilder.Append(" MST_PartyContact.Fax as PartyContactFax,	 ");
			strSqlBuilder.Append(" MST_Carrier.Name as CarrierName, ");

			strSqlBuilder.Append(" (SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append(" FROM SO_InvoiceDetail detail");
			strSqlBuilder.Append("  WHERE detail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID ");
			strSqlBuilder.Append("  ) as TotalQuantity, ");

			strSqlBuilder.Append(" (SELECT SUM( detail.Price * detail.InvoiceQty) ");
			strSqlBuilder.Append("  FROM SO_InvoiceDetail detail");
			strSqlBuilder.Append("  WHERE detail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID ");
			strSqlBuilder.Append("  ) as TotalAmount,	 ");

			strSqlBuilder.Append(" ITM_Product.ProductID, ");
			strSqlBuilder.Append(" SO_InvoiceDetail.InvoiceDetailID, ");
			strSqlBuilder.Append(" SO_CustomerItemRefDetail.CustomerItemCode as PartNo, ");
			strSqlBuilder.Append(" ITM_Product.Description as PartName, ");
			strSqlBuilder.Append(" ITM_Product.Revision as PartModel, ");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode, ");
			strSqlBuilder.Append(" ITM_Category.Name as CategoryName,  ");
			
			strSqlBuilder.Append(" ITM_Product.QuantitySet, ");

			strSqlBuilder.Append(" (SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append("  FROM SO_InvoiceDetail detail");
			strSqlBuilder.Append("  WHERE detail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID");
			strSqlBuilder.Append("  AND detail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID");			
			strSqlBuilder.Append("  AND detail.InvoiceDetailID = SO_InvoiceDetail.InvoiceDetailID ");
			strSqlBuilder.Append(" ) as ShippingQuantity, ");

			strSqlBuilder.Append(" SO_InvoiceDetail.Price as UnitPrice, ");

			strSqlBuilder.Append(" (SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append("  FROM SO_InvoiceDetail detail ");
			strSqlBuilder.Append(" 	WHERE detail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID ");
			strSqlBuilder.Append("  ) * SO_InvoiceDetail.Price AS NetAmount ");

			strSqlBuilder.Append(" FROM SO_InvoiceDetail ");
			strSqlBuilder.Append(" 	INNER JOIN SO_InvoiceMaster ON SO_InvoiceDetail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID ");
			strSqlBuilder.Append(" 	INNER JOIN SO_SaleOrderMaster ON SO_InvoiceMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID ");
			strSqlBuilder.Append(" 	INNER JOIN SO_SaleOrderDetail ON SO_InvoiceDetail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID  ");
			strSqlBuilder.Append(" 	INNER JOIN ITM_Product ON ITM_Product.ProductID = SO_SaleOrderDetail.ProductID ");
			strSqlBuilder.Append(" 	INNER JOIN MST_UnitOfMeasure ON SO_SaleOrderDetail.SellingUMID = MST_UnitOfMeasure.UnitOfMeasureID ");
			strSqlBuilder.Append(" 	INNER JOIN MST_Currency ON SO_InvoiceMaster.CurrencyID = MST_Currency.CurrencyID ");
			strSqlBuilder.Append(" 	INNER JOIN MST_Party ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID ");
			strSqlBuilder.Append("	LEFT JOIN MST_PartyContact ON SO_SaleOrderMaster.PartyContactID = MST_PartyContact.PartyContactID ");
			strSqlBuilder.Append("  LEFT JOIN MST_Country partyCountry ON partyCountry.CountryID = MST_Party.CountryID ");
			strSqlBuilder.Append("  LEFT JOIN MST_PaymentTerm ON SO_SaleOrderMaster.PaymentTermsID = MST_PaymentTerm.PaymentTermID ");
			strSqlBuilder.Append("  LEFT JOIN MST_PartyLocation shippingLoc ON shippingLoc.PartyLocationID = SO_SaleOrderMaster.ShipToLocID ");
			strSqlBuilder.Append("  LEFT JOIN MST_Country ON shippingLoc.CountryID = MST_Country.CountryID ");
			strSqlBuilder.Append("  LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID ");
			strSqlBuilder.Append("  LEFT JOIN MST_Carrier ON MST_Carrier.CarrierID = SO_SaleOrderMaster.CarrierID ");
			
			strSqlBuilder.Append("  LEFT JOIN SO_CustomerItemRefMaster ON SO_CustomerItemRefMaster.PartyID = MST_Party.PartyID ");
			strSqlBuilder.Append("  LEFT JOIN SO_CustomerItemRefDetail ON SO_CustomerItemRefMaster.CustomerItemRefMasterID = SO_CustomerItemRefDetail.CustomerItemRefMasterID ");
			strSqlBuilder.Append("		AND SO_CustomerItemRefDetail.ProductID = ITM_Product.ProductID ");

			strSqlBuilder.Append(" WHERE SO_InvoiceMaster.InvoiceMasterID = " + pintSOInvoiceMasterID);
			
			strSqlBuilder.Append(" AND (SELECT SUM(detail.InvoiceQty) ");
			strSqlBuilder.Append("  FROM SO_InvoiceDetail detail");
			strSqlBuilder.Append("  WHERE detail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID");
			strSqlBuilder.Append("  AND detail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID");			
			strSqlBuilder.Append(" ) > 0");

			strSqlBuilder.Append(" ORDER BY ITM_Product.Revision, ITM_Product.Description ");

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}

		#endregion SO Invoice Data
		
		#region Inventory Adjustment Slip Data: Tuan TQ
		public DataTable GetInventoryAdjustmentData(int pintMasterID)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			StringBuilder strSqlBuilder = new StringBuilder();
			strSqlBuilder.Append(" SELECT  IV_Adjustment.AdjustmentID, ");
			strSqlBuilder.Append(" IV_Adjustment.AdjustQuantity, ");
			strSqlBuilder.Append(" IV_Adjustment.AdjustQuantity *");
			strSqlBuilder.Append(" ISNULL((");
			strSqlBuilder.Append(" 	CASE WHEN (SELECT SUM(ActualCost) FROM CST_ActualCostHistory actual");
			strSqlBuilder.Append(" 				JOIN cst_ActCostAllocationMaster period ON actual.ActCostAllocationMasterID = period.ActCostAllocationMasterID");
			strSqlBuilder.Append(" 				WHERE ProductID = IV_Adjustment.ProductID");
			strSqlBuilder.Append(" 				AND IV_Adjustment.PostDate BETWEEN period.FromDate AND period.ToDate) IS NOT NULL");
			strSqlBuilder.Append(" 		THEN (SELECT SUM(ActualCost) FROM CST_ActualCostHistory actual");
			strSqlBuilder.Append(" 				JOIN cst_ActCostAllocationMaster period ON actual.ActCostAllocationMasterID = period.ActCostAllocationMasterID");
			strSqlBuilder.Append(" 				WHERE ProductID = IV_Adjustment.ProductID");
			strSqlBuilder.Append(" 				AND IV_Adjustment.PostDate BETWEEN period.FromDate AND period.ToDate)");
			strSqlBuilder.Append(" 		ELSE (SELECT SUM(Cost) FROM CST_STDItemCost WHERE ProductID = IV_Adjustment.ProductID)");
			strSqlBuilder.Append(" 	END");
			strSqlBuilder.Append(" ),0) AS AdjustAmount,");
			strSqlBuilder.Append(" IV_Adjustment.TransNo, ");
			strSqlBuilder.Append(" IV_Adjustment.PostDate, ");
			strSqlBuilder.Append(" IV_Adjustment.Comment, ");
			strSqlBuilder.Append(" MST_Employee.Name as EmployeeName, ");
			strSqlBuilder.Append(" IV_Adjustment.UserName, ");
			strSqlBuilder.Append(" ITM_Category.Code as CategoryCode, ");
			strSqlBuilder.Append(" MST_Location.Code AS LocationCode, ");
			strSqlBuilder.Append(" MST_BIN.Code AS BinCode, ");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code AS StockUM, ");
			strSqlBuilder.Append(" MST_MasterLocation.Code AS MasLocCode, "); 
			strSqlBuilder.Append(" ITM_Product.Code AS PartNo, ");
			strSqlBuilder.Append(" ITM_Product.Description as PartName, ");
			strSqlBuilder.Append(" ITM_Product.Revision as PartModel, ");
			strSqlBuilder.Append(" case IV_Adjustment.UsedByCosting ");
			strSqlBuilder.Append(" when 1 then 'Yes' ");
			strSqlBuilder.Append(" else 'No' ");
			strSqlBuilder.Append(" End as UsedByCosting ");
			
			strSqlBuilder.Append(" FROM    IV_Adjustment "); 
			strSqlBuilder.Append("	INNER JOIN ITM_Product ON IV_Adjustment.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" 	INNER JOIN MST_UnitOfMeasure ON IV_Adjustment.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID ");
			strSqlBuilder.Append("	LEFT JOIN MST_Location ON IV_Adjustment.LocationID = MST_Location.LocationID ");
			strSqlBuilder.Append("	LEFT JOIN MST_MasterLocation ON IV_Adjustment.MasterLocationID = MST_MasterLocation.MasterLocationID ");
			strSqlBuilder.Append("	LEFT JOIN MST_BIN ON IV_Adjustment.BinID = MST_BIN.BinID ");
			strSqlBuilder.Append("	LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID ");
			strSqlBuilder.Append("	LEFT JOIN Sys_User ON IV_Adjustment.UserName = Sys_User.UserName ");
			strSqlBuilder.Append("	LEFT JOIN MST_Employee ON Sys_User.EmployeeID = MST_Employee.EmployeeID ");

			strSqlBuilder.Append(" WHERE IV_Adjustment.AdjustmentID = " + pintMasterID.ToString());

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}
		#endregion				

		#region Debit Note Data: Tuan TQ
		/// <summary>
		/// Get Debit Note information for Debit note report
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <returns></returns>
		public DataTable GetDebitNoteData(int pintMasterID)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			StringBuilder strSqlBuilder = new StringBuilder();

			
			strSqlBuilder.Append(" SELECT 	cst_FreightMaster.TranNo, ");
			strSqlBuilder.Append(" cst_FreightMaster.PostDate, ");
			strSqlBuilder.Append(" cst_FreightMaster.Note, ");
			strSqlBuilder.Append(" cst_FreightMaster.ExchangeRate, ");
			strSqlBuilder.Append(" MST_Currency.Code as CurrencyCode, ");	
			strSqlBuilder.Append(" vendor.Code AS VendorCode, ");
			strSqlBuilder.Append(" vendor.Name as VendorName, ");
			strSqlBuilder.Append(" maker.Code AS MakerCode, ");
			strSqlBuilder.Append(" maker.Name AS MakerName, ");
			strSqlBuilder.Append(" PO_ReturnToVendorMaster.RTVNo, "); 
			strSqlBuilder.Append(" PO_InvoiceMaster.InvoiceNo, ");
			strSqlBuilder.Append(" PO_PurchaseOrderMaster.Code AS PONo, ");
			strSqlBuilder.Append(" ITM_Product.Code as PartNo, ");
			strSqlBuilder.Append(" ITM_Product.Revision as PartModel, ");
			strSqlBuilder.Append(" ITM_Product.Description as PartName, ");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code AS BuyingUM, "); 
			strSqlBuilder.Append(" cst_FreightDetail.Quantity, ");
			strSqlBuilder.Append(" cst_FreightDetail.UnitPriceCIF, "); 
			strSqlBuilder.Append(" cst_FreightDetail.Amount, ");
			strSqlBuilder.Append(" cst_FreightDetail.VATAmount, ");
			strSqlBuilder.Append(" cst_FreightDetail.ImportTax ");

			strSqlBuilder.Append(" FROM    cst_FreightDetail "); 
			strSqlBuilder.Append(" INNER JOIN cst_FreightMaster ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID "); 
			strSqlBuilder.Append(" INNER JOIN ITM_Product ON cst_FreightDetail.ProductID = ITM_Product.ProductID "); 
			strSqlBuilder.Append(" LEFT JOIN MST_Currency ON MST_Currency.CurrencyID = cst_FreightMaster.CurrencyID ");
			strSqlBuilder.Append(" LEFT JOIN MST_UnitOfMeasure ON cst_FreightDetail.BuyingUMID = MST_UnitOfMeasure.UnitOfMeasureID "); 
			strSqlBuilder.Append(" LEFT JOIN MST_Party maker ON cst_FreightMaster.MakerID = maker.PartyID "); 
			strSqlBuilder.Append(" LEFT JOIN MST_Party vendor ON cst_FreightMaster.VendorID = vendor.PartyID "); 
			strSqlBuilder.Append(" LEFT JOIN PO_ReturnToVendorMaster ON cst_FreightMaster.ReturnToVendorMasterID = PO_ReturnToVendorMaster.ReturnToVendorMasterID ");
			strSqlBuilder.Append(" LEFT JOIN PO_PurchaseOrderMaster ON PO_ReturnToVendorMaster.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID "); 
			strSqlBuilder.Append(" LEFT JOIN PO_InvoiceMaster ON PO_ReturnToVendorMaster.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID ");

			strSqlBuilder.Append(" WHERE cst_FreightMaster.FreightMasterID = " + pintMasterID.ToString());

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}
		#endregion				
		/// <summary>
		/// Get Credit Note Data for Credit Note report
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, August 2 2006</date>
		public DataTable GetCreditNoteData(int pintMasterID)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			StringBuilder strSqlBuilder = new StringBuilder();

			
			strSqlBuilder.Append(" SELECT 	cst_FreightMaster.TranNo, ");
			strSqlBuilder.Append(" cst_FreightMaster.PostDate, ");
			strSqlBuilder.Append(" cst_FreightMaster.Note, ");
			strSqlBuilder.Append(" cst_FreightMaster.ExchangeRate, ");
			strSqlBuilder.Append(" MST_Currency.Code as CurrencyCode, ");	
			strSqlBuilder.Append(" vendor.Code AS VendorCode, ");
			strSqlBuilder.Append(" vendor.Name as VendorName, ");
			strSqlBuilder.Append(" maker.Code AS MakerCode, ");
			strSqlBuilder.Append(" maker.Name AS MakerName, ");
			strSqlBuilder.Append(" PO_ReturnToVendorMaster.RTVNo, "); 
			strSqlBuilder.Append(" PO_InvoiceMaster.InvoiceNo, ");
			strSqlBuilder.Append(" PO_PurchaseOrderMaster.Code AS PONo, ");
			strSqlBuilder.Append(" ITM_Product.Code as PartNo, ");
			strSqlBuilder.Append(" ITM_Product.Revision as PartModel, ");
			strSqlBuilder.Append(" ITM_Product.Description as PartName, ");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code AS BuyingUM, "); 
			strSqlBuilder.Append(" cst_FreightDetail.Quantity, ");
			strSqlBuilder.Append(" cst_FreightDetail.UnitPriceCIF, "); 
			strSqlBuilder.Append(" cst_FreightDetail.Amount, ");
			strSqlBuilder.Append(" cst_FreightDetail.VATAmount, ");
			strSqlBuilder.Append(" cst_FreightDetail.ImportTax, ");
			strSqlBuilder.Append(" IV_Adjustment.TransNo ");

			strSqlBuilder.Append(" FROM    cst_FreightDetail "); 
			strSqlBuilder.Append(" INNER JOIN cst_FreightMaster ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID "); 
			strSqlBuilder.Append(" INNER JOIN ITM_Product ON cst_FreightDetail.ProductID = ITM_Product.ProductID "); 
			strSqlBuilder.Append(" LEFT JOIN MST_Currency ON MST_Currency.CurrencyID = cst_FreightMaster.CurrencyID ");
			strSqlBuilder.Append(" LEFT JOIN MST_UnitOfMeasure ON cst_FreightDetail.BuyingUMID = MST_UnitOfMeasure.UnitOfMeasureID "); 
			strSqlBuilder.Append(" LEFT JOIN MST_Party maker ON cst_FreightMaster.MakerID = maker.PartyID "); 
			strSqlBuilder.Append(" LEFT JOIN MST_Party vendor ON cst_FreightMaster.VendorID = vendor.PartyID "); 
			strSqlBuilder.Append(" LEFT JOIN PO_ReturnToVendorMaster ON cst_FreightMaster.ReturnToVendorMasterID = PO_ReturnToVendorMaster.ReturnToVendorMasterID ");
			strSqlBuilder.Append(" LEFT JOIN PO_PurchaseOrderMaster ON PO_ReturnToVendorMaster.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID "); 
			strSqlBuilder.Append(" LEFT JOIN PO_InvoiceMaster ON PO_ReturnToVendorMaster.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID ");
			strSqlBuilder.Append(" LEFT JOIN IV_Adjustment ON cst_FreightDetail.AdjustmentID = IV_Adjustment.AdjustmentID ");

			strSqlBuilder.Append(" WHERE cst_FreightMaster.FreightMasterID = " + pintMasterID.ToString());

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);
			
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}

		#region CUSTOMS LIST REPORT. THACHNN

		/// <summary>
		/// Thachnn : 08/Jun/2006		
		/// </summary>		
		public DataSet GetCustomsListReportData(int pnCCNID, int pnInvoiceMasterID)
		{
			DataSet dstRET = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;		

			#region MAIN SQL QUERY
				
			string strSql = 		
				
				" Declare @pstrCCNID int " + 
				" Declare @pstrInvoiceMasterID int " + 
				" /*-----------------------------------*/ " + 				
				" Set @pstrCCNID = " +pnCCNID+ " " + 
				" Set @pstrInvoiceMasterID = " +pnInvoiceMasterID+ " " + 
				" /*-----------------------------------*/ " + 
				"  " + 
				"  " ;

			#endregion MAIN QUERY

			
			#region META _DATA and PODATA

			string strSql_META_TABLE =
				"  " + 
				" select " + 
				" ProductID, " + 
				" IsNull(CategoryName, '') as CategoryName, " + 
				" SOItems, " + 
				" TenHang, " + 
				" ChungLoai, " + 
				" MaSoThue, " + 
				" Sum(IsNull(SoLuong, 0.00)) as SoLuong,  " + 
				" DonGia, " + 
				" Sum(IsNull(TriGia, 0.00)) as TriGia, " + 
				" PercentNK, " + 
				" Sum(IsNull(TienThueNK, 0.00)) as TienThueNK, " + 
				" TriGiaTinhThueNK, " + 
				" PercentVAT, " + 
				" Sum(IsNull(TienThueVATPhaiNop, 0.00)) as TienThueVATPhaiNop" + 
				"  " + 
				"  " + 
				" from  " + 
				" (	 " + 
				" 	/*start group by section */ " + 
				" 	SELECT " + 
				" 	PRODUCT.ProductID, " + 
				" 	CATEGORY.CatalogName as [CategoryName],	 " + 
				" 	PRODUCT.TaxCode as [SoItems], " + 
				" 	PRODUCT.PartNameVN as [TenHang], " + 
				" 	PRODUCT.Revision as [ChungLoai], " + 
				" 	PRODUCT.OtherInfo1 /*CustomsCode*/ as [MaSoThue], " + 
				" 	IVDETAIL.InvoiceQuantity as [SoLuong], " + 
				" 	0.00 as [DonGia], " + 
				" 	IVDETAIL.InvoiceQuantity * IVDETAIL.UnitPrice as [TriGia], " + 
				" 	IVDETAIL.ImportTax as [PercentNK], " + 
				" 	IVDETAIL.ImportTaxAmount as [TienThueNK], " + 
				" 	0.00 as [TriGiaTinhThueNK], " + 
				" 	IVDETAIL.VAT as [PercentVAT], " + 
				" 	IVDETAIL.VATAmount as [TienThueVATPhaiNop] " + 
				" 	 " + 
				" 	FROM " + 
				" 	PO_InvoiceMaster as IVMASTER	 " + 
				" 	join PO_InvoiceDetail as IVDETAIL " + 
				" 		on IVMASTER.InvoiceMasterID = IVDETAIL.InvoiceMasterID " + 
				" 		and IVMASTER.CCNID = @pstrCCNID " + 
				" 		and IVMASTER.InvoiceMasterID = @pstrInvoiceMasterID " + 
				" 	join ITM_Product as PRODUCT " + 
				" 		on IVDETAIL.ProductID = PRODUCT.ProductID " + 
				" 	LEFT JOIN ITM_Category as CATEGORY " + 
				" 		on PRODUCT.CategoryID = CATEGORY.CategoryID	 " + 
				"  " + 
				" ) as DETAILDATA /* end group by section */ " + 
				"  " + 
				" GROUP BY " + 
				" ProductID, " + 
				" CategoryName, " + 
				" SOItems, " + 
				" TenHang, " + 
				" ChungLoai, " + 
				" MaSoThue, " + 
				" DonGia, " + 
				" PercentNK, " + 
				" TriGiaTinhThueNK, " + 
				" PercentVAT " + 
				"  " + 

				"  " ; 
			#endregion META _DATA


			#region Check with same VAT

			string strSql_SAME_VAT_TABLE =

/*--------------------------------------  check if same VAT -----------------------------------------*/
 " select  " + 
 " distinct  " + 
 " T1.ProductID " + 
 "  " + 
 " from  " + 
 " ( " + 
 " 	/*---------  inner Data table ---------*/ " + 
 " 	SELECT " + 
 " 	IVDETAIL.ProductID, " + 
 " 	IVDETAIL.VAT " + 
 " 	 " + 
 " 	FROM " + 
 " 	PO_InvoiceMaster as IVMASTER	 " + 
 " 	join PO_InvoiceDetail as IVDETAIL " + 
 " 		on IVMASTER.InvoiceMasterID = IVDETAIL.InvoiceMasterID " + 
 " 		and IVMASTER.CCNID = @pstrCCNID " + 
 " 		and IVMASTER.InvoiceMasterID = @pstrInvoiceMasterID " + 
 " 	/*---------  inner Data table ----------*/ " + 
 " ) as T1  " + 
 " inner join  " + 
 " ( " + 
 " 	/*--------  inner Data table ----------*/ " + 
 " 	SELECT " + 
 " 	IVDETAIL.ProductID, " + 
 " 	IVDETAIL.VAT " + 
 " 	 " + 
 " 	FROM " + 
 " 	PO_InvoiceMaster as IVMASTER	 " + 
 " 	join PO_InvoiceDetail as IVDETAIL " + 
 " 		on IVMASTER.InvoiceMasterID = IVDETAIL.InvoiceMasterID " + 
 " 		and IVMASTER.CCNID = @pstrCCNID " + 
 " 		and IVMASTER.InvoiceMasterID = @pstrInvoiceMasterID " + 
 " 	/*----------  inner Data table --------*/ " + 
 " ) as T2 " + 
 "  " + 
 " on T1.ProductID = T2.ProductID " + 
 " and IsNull(T1.VAT,0) < IsNull(T2.VAT,0) " + 
				" " ;
/*-----------------------------------  check if same VAT ----------------------------------------------*/

#endregion Check with same VAT


			#region Check with same ImportTAX

			string strSql_SAME_IMPORTTAX_TABLE =
				
/*------------------------------------  check if same IMPORT -----------------------------------------*/
 " select  " + 
 " distinct  " + 
 " T1.ProductID " + 
 "  " + 
 " from  " + 
 " ( " + 
 " 	/*-----------  inner Data table ----------*/ " + 
 " 	SELECT " + 
 " 	IVDETAIL.ProductID, " + 
 " 	IVDETAIL.ImportTax " + 
 " 	 " + 
 " 	FROM " + 
 " 	PO_InvoiceMaster as IVMASTER	 " + 
 " 	join PO_InvoiceDetail as IVDETAIL " + 
 " 		on IVMASTER.InvoiceMasterID = IVDETAIL.InvoiceMasterID " + 
 " 		and IVMASTER.CCNID = @pstrCCNID " + 
 " 		and IVMASTER.InvoiceMasterID = @pstrInvoiceMasterID " + 
 " 	/*----------  inner Data table ----------*/ " + 
 " ) as T1  " + 
 " inner join  " + 
 " ( " + 
 " 	/*----------  inner Data table -----------*/ " + 
 " 	SELECT " + 
 " 	IVDETAIL.ProductID, " + 
 " 	IVDETAIL.ImportTax " + 
 " 	 " + 
 " 	FROM " + 
 " 	PO_InvoiceMaster as IVMASTER	 " + 
 " 	join PO_InvoiceDetail as IVDETAIL " + 
 " 		on IVMASTER.InvoiceMasterID = IVDETAIL.InvoiceMasterID " + 
 " 		and IVMASTER.CCNID = @pstrCCNID " + 
 " 		and IVMASTER.InvoiceMasterID = @pstrInvoiceMasterID " + 
 " 	/*----------  inner Data table -----------*/ " + 
 " ) as T2 " + 
 "  " + 
 " on T1.ProductID = T2.ProductID " + 
 " and IsNull(T1.ImportTax,0) < IsNull(T2.ImportTax,0) " + 
				" "
/*-----------------------------------------  check if same IMPORT --------------------------------------*/
;

			#endregion Check with same ImportTAX


			try 
			{
				
				oconPCS = null;
				ocmdPCS = null;
			
				strSql += 
					strSql_META_TABLE + "\n" + 					
					strSql_SAME_VAT_TABLE + "\n" + 					
					strSql_SAME_IMPORTTAX_TABLE + "\n" 
					;	

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstRET);				
			}
			catch(OleDbException ex)
			{
				throw new Exception(strSql,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}			
			
			return dstRET;
		}



		#endregion		

		#region BUSINESS FUNCTION FOR PRODUCTION LINE PLANNING REPORT. THACHNN
	
		/// <summary>
		/// NOTE : GET WORKING TIME OF MAIN WORK CENTER ONLY
		/// 
		/// get the reference table for GetRealWorkingDay() function
		/// result is the table with each record contain: 
		/// BeginDate, EndDate (of configured WCCapacity)
		/// WorkTimeFrom, WorkTimeTo	(Real working time of each shift in a working day)
		/// 
		/// SCHEMA: BeginDate, EndDate, WorkTimeFrom, WorkTimeTo
		/// 
		/// </summary>
		/// <author>Thachnn</author>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrProductionLineID"></param>
		/// <returns></returns>
		public DataTable GetAllPeriodOfWorkingTime(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID)
		{
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = 
  
					" Declare @pstrCCNID int " + 
					" Declare @pstrMonth char(2) " + 
					" Declare @pstrYear char(4) " + 
					" Declare @pstrProductionLineID int " + 
					/*-----------------------------------*/
					"  " + 
					" Set @pstrCCNID = " +pstrCCNID+ " " + 
					" Set @pstrYear = '" +pstrYear+ "' " + 
					" Set @pstrMonth = '" +pstrMonth+ "' " + 
					" Set @pstrProductionLineID = " +pstrProductionLineID+ " " + 
					" /*-----------------------------------*/ " + 
					"  " + 
					" 	 " + 

					" select distinct     " + 
					"  " + 
					" WCC.BeginDate,    " + 
					" WCC.EndDate,    " + 
					" SP.WorkTimeFrom,    " + 
					" SP.WorkTimeTo     " + 
					" from     " + 
					" PRO_Shift as S    " + 
					" join PRO_ShiftPattern as SP    " + 
					" 	on S.ShiftID = SP.ShiftID    " + 
					" /*  	and ShiftDesc IN ('1S','2S','3S')   */ /*allow all shift*/ " + 
					" join PRO_ShiftCapacity as SC    " + 
					" 	on S.ShiftID = SC.ShiftID    " + 
					" join PRO_WCCapacity as WCC    " + 
					" 	on WCC.WCCapacityID = SC.WCCapacityID    " + 
					" 	 " + 
					" 	/* Take all the relate to Parameter Year-month period of WCCapacity. BeginDate < first day of NextMonth. EndDate >= first day of CurrentProvidedMonth */ " + 
					" 	and WCC.BeginDate < dateadd (month, 1, convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) ) /*beginning of next month*/ " + 
					" 	and convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) <= WCC.EndDate  " + 
					"  " + 
					" join MST_WorkCenter as WC    " + 
					" 	on WCC.WorkCenterID = WC.WorkCenterID    " + 
					" 	and WC.ProductionLineID = @pstrProductionLineID    " + 
					" 	and WC.CCNID = @pstrCCNID    " + 

					" 	and WC.IsMain  = 1    " +			/* GET WORKING TIME OF MAIN WORK CENTER ONLY */

					"  " + 
					"  " + 
					"  " ;
 
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				odadPCS = new OleDbDataAdapter(cmdData);
				cmdData.Connection.Open();
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		
		#endregion BUSINESS FUNCTION FOR PRODUCTION LINE PLANNING REPORT. THACHNN

	}
}