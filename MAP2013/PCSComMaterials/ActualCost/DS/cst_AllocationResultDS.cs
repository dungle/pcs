using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComMaterials.ActualCost.DS
{
	public class CST_AllocationResultDS 
	{
		private const string THIS = "PCSComMaterials.ActualCost.DS.CST_AllocationResultDS";
		private const string SQL_DATE_FORMAT = "yyyy-MM-dd";		

		public CST_AllocationResultDS()
		{
		}
		
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to CST_AllocationResult
		///    </Description>
		///    <Inputs>
		///        CST_AllocationResultVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, February 22, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				CST_AllocationResultVO objObject = (CST_AllocationResultVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO CST_AllocationResult("
				+ CST_AllocationResultTable.COMPLETEDQUANTITY_FLD + ","
				+ CST_AllocationResultTable.RATE_FLD + ","
				+ CST_AllocationResultTable.AMOUNT_FLD + ","
				+ CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
				+ CST_AllocationResultTable.DEPARTMENTID_FLD + ","
				+ CST_AllocationResultTable.PRODUCTIONLINEID_FLD + ","
				+ CST_AllocationResultTable.PRODUCTGROUPID_FLD + ","
				+ CST_AllocationResultTable.COSTELEMENTID_FLD + ","
				+ CST_AllocationResultTable.PRODUCTID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.COMPLETEDQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_AllocationResultTable.COMPLETEDQUANTITY_FLD].Value = objObject.CompletedQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.RATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_AllocationResultTable.RATE_FLD].Value = objObject.Rate;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.AMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_AllocationResultTable.AMOUNT_FLD].Value = objObject.Amount;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD].Value = objObject.ActCostAllocationMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.DEPARTMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_AllocationResultTable.DEPARTMENTID_FLD].Value = objObject.DepartmentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.PRODUCTIONLINEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_AllocationResultTable.PRODUCTIONLINEID_FLD].Value = objObject.ProductionLineID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.PRODUCTGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_AllocationResultTable.PRODUCTGROUPID_FLD].Value = objObject.ProductGroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.COSTELEMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_AllocationResultTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_AllocationResultTable.PRODUCTID_FLD].Value = objObject.ProductID;
				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
																   
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			

			catch(InvalidOperationException ex)
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
		///       This method uses to delete data from CST_AllocationResult
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       void
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       09-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + CST_AllocationResultTable.TABLE_NAME + " WHERE  " + "AllocationResultID" + "=" + pintID.ToString();
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
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
	
		public void DeleteByAllocationMasterID(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".DeleteByAllocationMasterID()";
			
			string strSql = "DELETE " + CST_AllocationResultTable.TABLE_NAME 
						 + " WHERE ActCostAllocationMasterID" + "=" + pintMasterID.ToString();

			OleDbConnection oconPCS = null;			
			OleDbCommand ocmdPCS = null;

			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
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
		///       This method uses to get data from CST_AllocationResult
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       CST_AllocationResultVO
		///    </Outputs>
		///    <Returns>
		///       CST_AllocationResultVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, February 22, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
				+ CST_AllocationResultTable.ALLOCATIONRESULTID_FLD + ","
				+ CST_AllocationResultTable.COMPLETEDQUANTITY_FLD + ","
				+ CST_AllocationResultTable.RATE_FLD + ","
				+ CST_AllocationResultTable.AMOUNT_FLD + ","
				+ CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
				+ CST_AllocationResultTable.DEPARTMENTID_FLD + ","
				+ CST_AllocationResultTable.PRODUCTIONLINEID_FLD + ","
				+ CST_AllocationResultTable.PRODUCTGROUPID_FLD + ","
				+ CST_AllocationResultTable.COSTELEMENTID_FLD + ","
				+ CST_AllocationResultTable.PRODUCTID_FLD
				+ " FROM " + CST_AllocationResultTable.TABLE_NAME
				+" WHERE " + CST_AllocationResultTable.ALLOCATIONRESULTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				CST_AllocationResultVO objObject = new CST_AllocationResultVO();

				while (odrPCS.Read())
				{ 
				objObject.AllocationResultID = int.Parse(odrPCS[CST_AllocationResultTable.ALLOCATIONRESULTID_FLD].ToString().Trim());
				objObject.CompletedQuantity = Decimal.Parse(odrPCS[CST_AllocationResultTable.COMPLETEDQUANTITY_FLD].ToString().Trim());
				objObject.Rate = Decimal.Parse(odrPCS[CST_AllocationResultTable.RATE_FLD].ToString().Trim());
				objObject.Amount = Decimal.Parse(odrPCS[CST_AllocationResultTable.AMOUNT_FLD].ToString().Trim());
				objObject.ActCostAllocationMasterID = int.Parse(odrPCS[CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD].ToString().Trim());
				objObject.DepartmentID = int.Parse(odrPCS[CST_AllocationResultTable.DEPARTMENTID_FLD].ToString().Trim());
				objObject.ProductionLineID = int.Parse(odrPCS[CST_AllocationResultTable.PRODUCTIONLINEID_FLD].ToString().Trim());
				objObject.ProductGroupID = int.Parse(odrPCS[CST_AllocationResultTable.PRODUCTGROUPID_FLD].ToString().Trim());
				objObject.CostElementID = int.Parse(odrPCS[CST_AllocationResultTable.COSTELEMENTID_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[CST_AllocationResultTable.PRODUCTID_FLD].ToString().Trim());

				}		
				return objObject;					
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
		///       This method uses to update data to CST_AllocationResult
		///    </Description>
		///    <Inputs>
		///       CST_AllocationResultVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       09-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			CST_AllocationResultVO objObject = (CST_AllocationResultVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE CST_AllocationResult SET "
				+ CST_AllocationResultTable.COMPLETEDQUANTITY_FLD + "=   ?" + ","
				+ CST_AllocationResultTable.RATE_FLD + "=   ?" + ","
				+ CST_AllocationResultTable.AMOUNT_FLD + "=   ?" + ","
				+ CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD + "=   ?" + ","
				+ CST_AllocationResultTable.DEPARTMENTID_FLD + "=   ?" + ","
				+ CST_AllocationResultTable.PRODUCTIONLINEID_FLD + "=   ?" + ","
				+ CST_AllocationResultTable.PRODUCTGROUPID_FLD + "=   ?" + ","
				+ CST_AllocationResultTable.COSTELEMENTID_FLD + "=   ?" + ","
				+ CST_AllocationResultTable.PRODUCTID_FLD + "=  ?"
				+" WHERE " + CST_AllocationResultTable.ALLOCATIONRESULTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.COMPLETEDQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_AllocationResultTable.COMPLETEDQUANTITY_FLD].Value = objObject.CompletedQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.RATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_AllocationResultTable.RATE_FLD].Value = objObject.Rate;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.AMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_AllocationResultTable.AMOUNT_FLD].Value = objObject.Amount;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD].Value = objObject.ActCostAllocationMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.DEPARTMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_AllocationResultTable.DEPARTMENTID_FLD].Value = objObject.DepartmentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.PRODUCTIONLINEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_AllocationResultTable.PRODUCTIONLINEID_FLD].Value = objObject.ProductionLineID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.PRODUCTGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_AllocationResultTable.PRODUCTGROUPID_FLD].Value = objObject.ProductGroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.COSTELEMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_AllocationResultTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_AllocationResultTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_AllocationResultTable.ALLOCATIONRESULTID_FLD, OleDbType.BigInt));
				ocmdPCS.Parameters[CST_AllocationResultTable.ALLOCATIONRESULTID_FLD].Value = objObject.AllocationResultID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
																   
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			

			catch(InvalidOperationException ex)
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
		///       This method uses to get all data from CST_AllocationResult
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, February 22, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet List()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
				+ CST_AllocationResultTable.ALLOCATIONRESULTID_FLD + ","
				+ CST_AllocationResultTable.COMPLETEDQUANTITY_FLD + ","
				+ CST_AllocationResultTable.RATE_FLD + ","
				+ CST_AllocationResultTable.AMOUNT_FLD + ","
				+ CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
				+ CST_AllocationResultTable.DEPARTMENTID_FLD + ","
				+ CST_AllocationResultTable.PRODUCTIONLINEID_FLD + ","
				+ CST_AllocationResultTable.PRODUCTGROUPID_FLD + ","
				+ CST_AllocationResultTable.COSTELEMENTID_FLD + ","
				+ CST_AllocationResultTable.PRODUCTID_FLD
					+ " FROM " + CST_AllocationResultTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,CST_AllocationResultTable.TABLE_NAME);

				return dstPCS;
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
		
		public DataTable GetLeadTimeData(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetMaxLeadTimeData()";						

			DataTable dtbResult = new DataTable();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			
			try
			{
//				string strSql = "SELECT SUM( DISTINCT ISNULL(ITM_Routing.LaborRunTime, 0) + ISNULL(ITM_Routing.LaborSetupTime, 0)";
//				strSql += " + ";
//				strSql += " ISNULL(ITM_Routing.MachineRunTime, 0) + ISNULL(ITM_Routing.MachineSetupTime, 0)";
//				strSql += " )";
//				strSql += " as LeadTime,";
				string strSql = "SELECT ITM_Product.LTVariableTime LeadTime,";
				strSql += " ITM_Product.ProductID ";
				strSql += ", PRO_WorkOrderCompletion.WorkOrderDetailID";
				
				strSql += " FROM ITM_Product";
				//strSql += " 	left JOIN PRO_WorkOrderDetail ON PRO_WorkOrderDetail.WorkOrderDetailID = ITM_Routing.WorkOrderDetailID";
				//strSql += " 	INNER JOIN ITM_Product ON ITM_Product.ProductID = ITM_Routing.ProductID";
				strSql += " 	INNER JOIN PRO_WorkOrderCompletion ON PRO_WorkOrderCompletion.ProductID = ITM_Product.ProductID";

				strSql += " WHERE  ITM_Product.CCNID = " + pintCCNID.ToString();
				strSql += " AND PRO_WorkOrderCompletion.PostDate BETWEEN '" + pdtmFromDate.ToString(SQL_DATE_FORMAT) + " 00:00:00' AND '" + pdtmToDate.ToString(SQL_DATE_FORMAT) + " 23:59:59'";

//				strSql += " GROUP BY ITM_Routing.ProductID,";
//				strSql += " PRO_WorkOrderDetail.WorkOrderDetailID";
									
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
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

		public DataTable GetCompletedQuantityByProductionLine(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetCompletedQuantityByProductionLine()";						
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			DataTable dtbResult = new DataTable();

			try 
			{
				string strSql = "SELECT SUM(CompletedQuantity) as CompletedQuantity,ProductID,ProductionLineID,DepartmentID,CCNID,WorkOrderDetailID"
					+ " FROM (";

				string strSql1 = "SELECT  SUM(PRO_WorkOrderCompletion.CompletedQuantity) as CompletedQuantity,";
				strSql1 += " PRO_WorkOrderCompletion.ProductID,";
				strSql1 += " PRO_ProductionLine.ProductionLineID,";
				strSql1 += " MST_Department.DepartmentID,";
				strSql1 += " MST_Department.CCNID";
				strSql1 += ",PRO_WorkOrderCompletion.WorkOrderDetailID ";
				strSql1 += " FROM    PRO_WorkOrderCompletion";
				strSql1 += " INNER JOIN PRO_WorkOrderMaster ON PRO_WorkOrderMaster.WorkOrderMasterID = PRO_WorkOrderCompletion.WorkOrderMasterID";
				strSql1 += " INNER JOIN PRO_ProductionLine ON PRO_ProductionLine.ProductionLineID = PRO_WorkOrderMaster.ProductionLineID";
				strSql1 += " INNER JOIN MST_Department ON MST_Department.DepartmentID = PRO_ProductionLine.DepartmentID";
				strSql1 += " INNER JOIN ITM_Product ON ITM_Product.ProductionLineID = PRO_ProductionLine.ProductionLineID";
				strSql1 += " AND ITM_Product.ProductID = PRO_WorkOrderCompletion.ProductID";
				strSql1 += " WHERE MST_Department.CCNID = " + pintCCNID;				
				strSql1 += " AND (PRO_WorkOrderCompletion.PostDate BETWEEN '" + pdtmFromDate.ToString(SQL_DATE_FORMAT) + " 00:00:00' AND '" + pdtmToDate.ToString(SQL_DATE_FORMAT) + " 23:59:59')";
				strSql1 += " GROUP BY PRO_WorkOrderCompletion.ProductID,";
				strSql1 += " PRO_ProductionLine.ProductionLineID,";
				strSql1 += " MST_Department.DepartmentID,";
				strSql1 += " MST_Department.CCNID, WorkOrderDetailID ";
				strSql1 += ", PRO_WorkOrderCompletion.WorkOrderDetailID";

				string strSql2 = "SELECT SUM(PO_PurchaseOrderReceiptDetail.ReceiveQuantity) CompletedQuantity" 
				+ ",PO_PurchaseOrderReceiptDetail.ProductID"
				+ ",PRO_ProductionLine.ProductionLineID"
				+ ",MST_Department.DepartmentID"
				+ ",MST_Department.CCNID, 0 WorkOrderDetailID"
				+ " FROM PO_PurchaseOrderReceiptDetail "
				+ " INNER JOIN PO_PurchaseOrderReceiptMaster ON PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID=PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID"
				+ " INNER JOIN PRO_ProductionLine ON PRO_ProductionLine.ProductionLineID = PO_PurchaseOrderReceiptMaster.ProductionLineID"
				+ " INNER JOIN MST_Department ON MST_Department.DepartmentID = PRO_ProductionLine.DepartmentID"
				+ " WHERE PO_PurchaseOrderReceiptMaster.ReceiptType=4 "
				+ " AND (PO_PurchaseOrderReceiptMaster.PostDate BETWEEN '" + pdtmFromDate.ToString(SQL_DATE_FORMAT) + " 00:00:00' AND '" + pdtmToDate.ToString(SQL_DATE_FORMAT) + " 23:59:59')"
				+ " GROUP BY PO_PurchaseOrderReceiptDetail.ProductID"
				+ " ,PRO_ProductionLine.ProductionLineID"
				+ " ,MST_Department.DepartmentID"
				+ " ,MST_Department.CCNID";

				strSql = strSql1;
//					+ strSql1 
//					+ " UNION ALL "PRO_WorkOrderCompletion.WorkOrderDetailID
//					+ strSql2
//					+ ") XXX "
//					+ " GROUP BY ProductID, ProductionLineID, DepartmentID,CCNID,WorkOrderDetailID";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
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

		public DataTable GetCompletedQuantityByProductGroup(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetCompletedQuantityByProductGroup()";						
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			DataTable dtbResult = new DataTable();

			try 
			{
				string strSql = "SELECT  SUM(PRO_WorkOrderCompletion.CompletedQuantity) as CompletedQuantity,";

				strSql += " PRO_WorkOrderCompletion.ProductID,";
				strSql += " CST_ProductGroup.ProductGroupID,";
				strSql += " CST_ProductGroup.ProductionLineID,";
				strSql += " PRO_WorkOrderCompletion.WorkOrderDetailID";

				strSql += " FROM PRO_WorkOrderCompletion";
				strSql += " INNER JOIN PRO_WorkOrderMaster ON PRO_WorkOrderMaster.WorkOrderMasterID = PRO_WorkOrderCompletion.WorkOrderMasterID";
				strSql += " INNER JOIN CST_ProductGroup ON CST_ProductGroup.ProductionLineID = PRO_WorkOrderMaster.ProductionLineID";				
				strSql += " INNER JOIN ITM_Product ON ITM_Product.ProductGroupID = CST_ProductGroup.ProductGroupID";
				strSql += " AND ITM_Product.ProductID = PRO_WorkOrderCompletion.ProductID";

				strSql += " WHERE PRO_WorkOrderMaster.CCNID = " + pintCCNID;				
				strSql += " AND (PRO_WorkOrderCompletion.PostDate BETWEEN '" + pdtmFromDate.ToString(SQL_DATE_FORMAT) + " 00:00:00' AND '" + pdtmToDate.ToString(SQL_DATE_FORMAT) + " 23:59:59')";

				strSql += " GROUP BY PRO_WorkOrderCompletion.ProductID,";
				strSql += " CST_ProductGroup.ProductGroupID,";
				strSql += " CST_ProductGroup.ProductionLineID,";
				strSql += " PRO_WorkOrderCompletion.WorkOrderDetailID";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update a DataSet
		///    </Description>
		///    <Inputs>
		///        DataSet       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, February 22, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		public void UpdateDataSet(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
				+ CST_AllocationResultTable.ALLOCATIONRESULTID_FLD + ","
				+ CST_AllocationResultTable.COMPLETEDQUANTITY_FLD + ","
				+ CST_AllocationResultTable.RATE_FLD + ","
				+ CST_AllocationResultTable.AMOUNT_FLD + ","
				+ CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
				+ CST_AllocationResultTable.DEPARTMENTID_FLD + ","
				+ CST_AllocationResultTable.PRODUCTIONLINEID_FLD + ","
				+ CST_AllocationResultTable.PRODUCTGROUPID_FLD + ","
				+ CST_AllocationResultTable.COSTELEMENTID_FLD + ","
				+ CST_AllocationResultTable.PRODUCTID_FLD 
				+ "  FROM " + CST_AllocationResultTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);				
				pData.EnforceConstraints = false;

				//HACK: added by Tuan TQ. 21 Jun, 2006. Avoid timeout
				if(odadPCS.UpdateCommand != null)
				{
					odadPCS.UpdateCommand.CommandTimeout = 1000;
				}
				else
				{
					odadPCS.SelectCommand.CommandTimeout = 1000;
				}
				//End hack

				odadPCS.Update(pData, CST_AllocationResultTable.TABLE_NAME);
			}
			catch(OleDbException ex)
			{
				if(ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}
			catch(InvalidOperationException ex)
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

		public void InsertNewAllocation(int pintActCostAllocationMasterID,DateTime pdtmFromDate,DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".DeleteByAllocationMasterID()";
			
			string strSql = 
				" INSERT INTO cst_AllocationResult(ActCostAllocationMasterID,DepartmentID, " + "\n"
				+ "   ProductionLineID, ProductID, /*ProductGroupID,*/ CostElementID, CompletedQuantity, " + "\n"
				+ "   Rate, Amount  " + "\n"
				+ " ) " + "\n"
				+ " SELECT " + pintActCostAllocationMasterID + ",DepartmentID, " + "\n"
				+ " xx.ProductionLineID,xx.ProductID, /*xx.ProductGroupID,*/ CostElementID,SUMReceiveQuantity CompletedQuantity, " + "\n"
				+ " TotalTimeGroup/( " + "\n"
 				+ "  SELECT SUM(TotalTimeGroup) TotalTimeALL " + "\n"
 				+ "  FROM ( " + "\n"
    			+ "    SELECT M.ProductionLineID,d.ProductID, /*I.ProductGroupID,*/ " + "\n"
				+ "    ISNULL(SUM(D.ReceiveQuantity)*ISNULL(I.LTVariableTime,0),0) TotalTimeGroup " + "\n"
				+ "    FROM PO_PurchaseOrderReceiptDetail D " + "\n"
				+ "    INNER JOIN PO_PurchaseOrderReceiptMaster m on m.PurchaseOrderReceiptID=D.PurchaseOrderReceiptID " + "\n"
				+ "    INNER JOIN ITM_PRODUCT I ON I.PRODUCTID=D.PRODUCTID " + "\n"
				+ "   WHERE " 
				+ "    (M.PostDate BETWEEN '" + pdtmFromDate.ToString(SQL_DATE_FORMAT) + " 00:00:00' AND '" + pdtmToDate.ToString(SQL_DATE_FORMAT) + " 23:59:59')"
				+ "    and ReceiptType=4" + " \n"
				+ "    GROUP BY M.ProductionLineID,d.ProductID, /*I.ProductGroupID,*/ I.LTVariableTime " + "\n"
				+ "   ) oIo WHERE ProductionLineID = xx.ProductionLineID " + "\n"
				+ " )*100 AS RATE, " + "\n"
				+ " ALF.AllocationAmount* /*Rate Amount*/ " + "\n"
				+ "  TotalTimeGroup/( " + "\n"
				+ "  SELECT SUM(TotalTimeGroup) TotalTimeALL " + "\n"
				+ "  FROM ( " + "\n"
				+ "    SELECT M.ProductionLineID,d.ProductID, /*I.ProductGroupID,*/ " + "\n"
				+ "    ISNULL(SUM(D.ReceiveQuantity)*ISNULL(I.LTVariableTime,0),0) TotalTimeGroup " + "\n"
				+ "    FROM PO_PurchaseOrderReceiptDetail D " + "\n"
				+ "    INNER JOIN PO_PurchaseOrderReceiptMaster m on m.PurchaseOrderReceiptID=D.PurchaseOrderReceiptID " + "\n"
				+ "    INNER JOIN ITM_PRODUCT I ON I.PRODUCTID=D.PRODUCTID " + "\n"
				+ "   WHERE " 
				+ "    (M.PostDate BETWEEN '" + pdtmFromDate.ToString(SQL_DATE_FORMAT) + " 00:00:00' AND '" + pdtmToDate.ToString(SQL_DATE_FORMAT) + " 23:59:59')"
				+ "    and ReceiptType=4" + " \n"
				+ "    GROUP BY M.ProductionLineID,d.ProductID, /*I.ProductGroupID,*/ I.LTVariableTime " + "\n"
				+ "   ) oIo WHERE ProductionLineID = xx.ProductionLineID" + "\n"
				+ " ) AS Amount " + "\n"

				+ " FROM( " + "\n"
				+ "   SELECT M.ProductionLineID,d.ProductID, /*I.ProductGroupID,*/ " + "\n"
				+ "   SUM(D.ReceiveQuantity) SUMReceiveQuantity, /* ISNULL(I.LTVariableTime,0) LeadTime,*/ " + "\n"
				+ "   ISNULL(SUM(D.ReceiveQuantity)*ISNULL(I.LTVariableTime,0),0) TotalTimeGroup " + "\n"
				+ "   FROM PO_PurchaseOrderReceiptDetail D " + "\n"
				+ "   INNER JOIN PO_PurchaseOrderReceiptMaster m on m.PurchaseOrderReceiptID=D.PurchaseOrderReceiptID " + "\n"
				+ "   INNER JOIN ITM_PRODUCT I ON I.PRODUCTID=D.PRODUCTID " + "\n"
				+ "   WHERE " 
				+ "    (M.PostDate BETWEEN '" + pdtmFromDate.ToString(SQL_DATE_FORMAT) + " 00:00:00' AND '" + pdtmToDate.ToString(SQL_DATE_FORMAT) + " 23:59:59')"
				+ "    and ReceiptType=4"+ "\n"
				+ "   GROUP BY M.ProductionLineID,d.ProductID, /*I.ProductGroupID,*/ I.LTVariableTime " + "\n"
				+ "   Having ISNULL(SUM(D.ReceiveQuantity)*ISNULL(I.LTVariableTime,0),0) > 0 " + "\n"
				+ " ) xx " + "\n"
				+ " INNER JOIN ( " + "\n"
				+ "  Select PRO_ProductionLine.ProductionLineID,MST_Department.DepartmentID,CostElementID,AllocationAmount from cst_ActCostAllocationDetail" + "\n"
				+ "  inner join PRO_ProductionLine ON PRO_ProductionLine.ProductionLineID=cst_ActCostAllocationDetail.ProductionLineID " + "\n"
				+ "  inner join MST_Department ON MST_Department.DepartmentID=PRO_ProductionLine.DepartmentID" + "\n"
				+ "  where ActCostAllocationMasterID= " + pintActCostAllocationMasterID + "\n"
				+ "  AND PRO_ProductionLine.ProductionLineID IS NOT NULL " + " \n" 
				+ "  AND cst_ActCostAllocationDetail.ProductGroupID IS NULL " + " \n" 
				+ "  AND cst_ActCostAllocationDetail.ProductID IS NULL " + " \n"
				+ "  AND MST_Department.Code = 'Maker'" + "\n"
				+ "  ) ALF ON ALF.ProductionLineID=xx.ProductionLineID; " + "\n" + " \n";

			string strSqlGroup = 
				" INSERT INTO cst_AllocationResult(ActCostAllocationMasterID,DepartmentID, " + " \n"
				+ "   ProductionLineID, ProductID, ProductGroupID, CostElementID, CompletedQuantity, " + " \n"
				+ "   Rate, Amount  " + " \n"
				+ " ) " + " \n"
				+ " SELECT " + pintActCostAllocationMasterID + ",DepartmentID, " + " \n"
				+ " xx.ProductionLineID,xx.ProductID, xx.ProductGroupID, CostElementID,SUMReceiveQuantity CompletedQuantity, " + " \n"
				+ " TotalTimeGroup/( " + " \n"
				+ "  SELECT SUM(TotalTimeGroup) TotalTimeALL " + " \n"
				+ "  FROM ( " + " \n"
				+ "    SELECT M.ProductionLineID,d.ProductID, I.ProductGroupID, " + " \n"
				+ "    ISNULL(SUM(D.ReceiveQuantity)*ISNULL(I.LTVariableTime,0),0) TotalTimeGroup " + " \n"
				+ "    FROM PO_PurchaseOrderReceiptDetail D " + " \n"
				+ "    INNER JOIN PO_PurchaseOrderReceiptMaster m on m.PurchaseOrderReceiptID=D.PurchaseOrderReceiptID " + " \n"
				+ "    INNER JOIN ITM_PRODUCT I ON I.PRODUCTID=D.PRODUCTID " + " \n"
				+ "   WHERE "  + " \n"
				+ "    (M.PostDate BETWEEN '" + pdtmFromDate.ToString(SQL_DATE_FORMAT) + " 00:00:00' AND '" + pdtmToDate.ToString(SQL_DATE_FORMAT) + " 23:59:59')" + " \n"
				+ "    and ReceiptType=4" + " \n"
				+ "    GROUP BY M.ProductionLineID,d.ProductID, I.ProductGroupID, I.LTVariableTime " + " \n"
				+ "   ) oIo WHERE ProductGroupID = xx.ProductGroupID" + " \n"
				+ " )*100 AS RATE, " + " \n"
				+ " ALF.AllocationAmount* /*Rate Amount */" + " \n"
				+ " TotalTimeGroup/( " + " \n"
				+ "  SELECT SUM(TotalTimeGroup) TotalTimeALL " + " \n"
				+ "  FROM ( " + " \n"
				+ "    SELECT M.ProductionLineID,d.ProductID, I.ProductGroupID, " + " \n"
				+ "    ISNULL(SUM(D.ReceiveQuantity)*ISNULL(I.LTVariableTime,0),0) TotalTimeGroup " + " \n"
				+ "    FROM PO_PurchaseOrderReceiptDetail D " + " \n"
				+ "    INNER JOIN PO_PurchaseOrderReceiptMaster m on m.PurchaseOrderReceiptID=D.PurchaseOrderReceiptID " + " \n"
				+ "    INNER JOIN ITM_PRODUCT I ON I.PRODUCTID=D.PRODUCTID " + " \n"
				+ "   WHERE "  + " \n"
				+ "    (M.PostDate BETWEEN '" + pdtmFromDate.ToString(SQL_DATE_FORMAT) + " 00:00:00' AND '" + pdtmToDate.ToString(SQL_DATE_FORMAT) + " 23:59:59')" + " \n"
				+ "    and ReceiptType=4" + " \n"
				+ "    GROUP BY M.ProductionLineID,d.ProductID, I.ProductGroupID, I.LTVariableTime " + " \n"
				+ "   ) oIo WHERE ProductGroupID = xx.ProductGroupID" + " \n"
				+ " ) AS Amount " + " \n"
				+ " FROM( " + " \n"
				+ "   SELECT M.ProductionLineID,d.ProductID, I.ProductGroupID, " + " \n"
				+ "   SUM(D.ReceiveQuantity) SUMReceiveQuantity, /* ISNULL(I.LTVariableTime,0) LeadTime,*/ " + " \n"
				+ "   ISNULL(SUM(D.ReceiveQuantity)*ISNULL(I.LTVariableTime,0),0) TotalTimeGroup " + " \n"
				+ "   FROM PO_PurchaseOrderReceiptDetail D " + " \n"
				+ "   INNER JOIN PO_PurchaseOrderReceiptMaster m on m.PurchaseOrderReceiptID=D.PurchaseOrderReceiptID " + " \n"
				+ "   INNER JOIN ITM_PRODUCT I ON I.PRODUCTID=D.PRODUCTID " + " \n"
				+ "   WHERE "  + " \n"
				+ "    (M.PostDate BETWEEN '" + pdtmFromDate.ToString(SQL_DATE_FORMAT) + " 00:00:00' AND '" + pdtmToDate.ToString(SQL_DATE_FORMAT) + " 23:59:59')" + " \n"
				+ "    and ReceiptType=4" + " \n"
				+ "   GROUP BY M.ProductionLineID,d.ProductID, I.ProductGroupID, I.LTVariableTime " + " \n"
				+ "   Having ISNULL(SUM(D.ReceiveQuantity)*ISNULL(I.LTVariableTime,0),0) > 0 " + " \n"
				+ " ) xx " + " \n"
				+ " INNER JOIN ( " + " \n"
				+ "  Select PRO_ProductionLine.ProductionLineID,MST_Department.DepartmentID,cst_ActCostAllocationDetail.ProductGroupID,CostElementID,AllocationAmount " + " \n" 
				+ "  from cst_ActCostAllocationDetail" + " \n"
				+ "  inner join PRO_ProductionLine ON PRO_ProductionLine.ProductionLineID=cst_ActCostAllocationDetail.ProductionLineID " + " \n"
				+ "  inner join MST_Department ON MST_Department.DepartmentID=PRO_ProductionLine.DepartmentID" + " \n"
				+ "  where ActCostAllocationMasterID= " + pintActCostAllocationMasterID + " \n"
				+ "  AND cst_ActCostAllocationDetail.ProductGroupID IS NOT NULL " + " \n"
				+ "  AND cst_ActCostAllocationDetail.ProductID IS NULL " + " \n"
				+ "  AND MST_Department.Code = 'Maker'" + " \n"
				+ "  AND PRO_ProductionLine.ProductionLineID IS NOT NULL  ) ALF ON ALF.ProductionLineID=xx.ProductionLineID" + " \n"
				+ "  AND ALF.ProductGroupID=xx.ProductGroupID; " + " \n";

			string strSqlProduct = " \n"
				+ " INSERT INTO cst_AllocationResult(ActCostAllocationMasterID,DepartmentID, " + " \n"
				+ "   ProductionLineID, ProductID, ProductGroupID, CostElementID, CompletedQuantity, " + " \n"
				+ "   Rate, Amount  " + " \n"
				+ " ) " + " \n"
				+ " SELECT " + pintActCostAllocationMasterID + ",DepartmentID, " + " \n"
				+ " xx.ProductionLineID,xx.ProductID, xx.ProductGroupID, CostElementID,CompletedQuantity,100, AllocationAmount" + " \n"
				+ " FROM ( " + " \n"
				+ "  Select cst_ActCostAllocationDetail.ProductionLineID,cst_ActCostAllocationDetail.ProductID,cst_ActCostAllocationDetail.DepartmentID," + " \n" 
				+ "    cst_ActCostAllocationDetail.ProductGroupID,cst_ActCostAllocationDetail.CostElementID,AllocationAmount, POReceipt.CompletedQuantity " + " \n" 
				+ "  from cst_ActCostAllocationDetail" + " \n"
				+ "  inner join PRO_ProductionLine ON PRO_ProductionLine.ProductionLineID=cst_ActCostAllocationDetail.ProductionLineID " + " \n"
				+ "  inner join MST_Department ON MST_Department.DepartmentID=PRO_ProductionLine.DepartmentID" + " \n"
				+ "  inner join ( " + "\n"
				+ "    SELECT M.ProductionLineID, I.ProductGroupID, D.ProductID," + " \n"
				+ "    ISNULL(SUM(D.ReceiveQuantity),0) CompletedQuantity " + " \n"
				+ "    FROM PO_PurchaseOrderReceiptDetail D " + " \n"
				+ "    INNER JOIN PO_PurchaseOrderReceiptMaster m on m.PurchaseOrderReceiptID=D.PurchaseOrderReceiptID " + " \n"
				+ "    INNER JOIN ITM_PRODUCT I ON I.PRODUCTID=D.PRODUCTID " + " \n"
				+ "   WHERE "  + " \n"
				+ "    (M.PostDate BETWEEN '" + pdtmFromDate.ToString(SQL_DATE_FORMAT) + " 00:00:00' AND '" + pdtmToDate.ToString(SQL_DATE_FORMAT) + " 23:59:59')" + " \n"
				+ "    and ReceiptType=4" + " \n"
				+ "    GROUP BY M.ProductionLineID,d.ProductID, I.ProductGroupID, I.LTVariableTime " + " \n"
				+ "  ) POReceipt ON POReceipt.ProductID=cst_ActCostAllocationDetail.ProductID" + "\n"
				+ "  where ActCostAllocationMasterID= " + pintActCostAllocationMasterID + " \n"
				+ "  AND cst_ActCostAllocationDetail.ProductGroupID IS NOT NULL " + " \n"
				+ "  AND cst_ActCostAllocationDetail.ProductID IS NOT NULL " + " \n"
				+ "  AND MST_Department.Code = 'Maker'" + " \n"
				+ "  AND POReceipt.CompletedQuantity > 0" + " \n"
				+ "  AND PRO_ProductionLine.ProductionLineID IS NOT NULL  ) xx" + " \n";

				strSql += strSqlGroup + strSqlProduct;

			OleDbConnection oconPCS = null;			
			OleDbCommand ocmdPCS = null;

			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
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
		/// List by Allocation master id
		/// </summary>
		/// <param name="pintPeriodID">Allocation Master ID</param>
		/// <returns></returns>
		public DataTable List(int pintPeriodID)
		{
			const string METHOD_NAME = THIS + ".List()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(Amount, 0)), 0) AS Amount, ProductID, CostElementID"
					+ " FROM CST_AllocationResult"
					+ " WHERE " + CST_AllocationResultTable.ACTCOSTALLOCATIONMASTERID_FLD + "=" + pintPeriodID
					+ " GROUP BY ProductID, CostElementID";
				Utils utils = new Utils();
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
	}
}
