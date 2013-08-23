using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;


//Using PCS's namespaces
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComMaterials.ActualCost.DS
{
	public class CST_ActCostAllocationDetailDS   
	{
		private const string THIS = ".CST_ActCostAllocationDetailDS";

		public CST_ActCostAllocationDetailDS()
		{
		}
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to CST_ActCostAllocationDetail
		///    </Description>
		///    <Inputs>
		///        CST_ActCostAllocationDetailVO       
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
		///       Tuesday, February 21, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				CST_ActCostAllocationDetailVO objObject = (CST_ActCostAllocationDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO CST_ActCostAllocationDetail("
					+ CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD + ","
					+ CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
					+ CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD + ","
					+ CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTID_FLD + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + ","
					+ CST_ActCostAllocationDetailTable.LINE_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD].Value = objObject.AllocationAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD].Value = objObject.ActCostAllocationMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD, OleDbType.Integer));
				if(objObject.DepartmentID > 0)
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].Value = objObject.DepartmentID;
				}
				else
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD, OleDbType.Integer));
				if(objObject.ProductionLineID > 0)
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].Value = objObject.ProductionLineID;
				}
				else
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				if(objObject.ProductID > 0)
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;
				}
				else
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD, OleDbType.Integer));
				if(objObject.ProductGroupID > 0)
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Value = objObject.ProductGroupID;
				}
				else
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Value = DBNull.Value;
				}
				
				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.LINE_FLD].Value = objObject.Line;
				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
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
		///       This method uses to delete data from CST_ActCostAllocationDetail
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
			strSql=	"DELETE " + CST_ActCostAllocationDetailTable.TABLE_NAME + " WHERE  " + "ActCostAllocationDetailID" + "=" + pintID.ToString();
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
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
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
		///       This method uses to get data from CST_ActCostAllocationDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       CST_ActCostAllocationDetailVO
		///    </Outputs>
		///    <Returns>
		///       CST_ActCostAllocationDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 21, 2006
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
					+ CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONDETAILID_FLD + ","
					+ CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD + ","
					+ CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
					+ CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD + ","
					+ CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTID_FLD + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + ","
					+ CST_ActCostAllocationDetailTable.LINE_FLD
					+ " FROM " + CST_ActCostAllocationDetailTable.TABLE_NAME
					+" WHERE " + CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				CST_ActCostAllocationDetailVO objObject = new CST_ActCostAllocationDetailVO();

				while (odrPCS.Read())
				{ 
					objObject.ActCostAllocationDetailID = int.Parse(odrPCS[CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONDETAILID_FLD].ToString().Trim());
					objObject.AllocationAmount = Decimal.Parse(odrPCS[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD].ToString().Trim());
					objObject.ActCostAllocationMasterID = int.Parse(odrPCS[CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD].ToString().Trim());
					objObject.CostElementID = int.Parse(odrPCS[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].ToString().Trim());
					
					if(!odrPCS[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].Equals(DBNull.Value))
					{
						objObject.DepartmentID = int.Parse(odrPCS[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].ToString().Trim());
					}
					else
					{
						objObject.DepartmentID = 0;
					}
					
					if(!odrPCS[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].Equals(DBNull.Value))
					{
						objObject.ProductionLineID = int.Parse(odrPCS[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].ToString().Trim());
					}
					else
					{
						objObject.ProductionLineID = 0;
					}

					if(!odrPCS[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].Equals(DBNull.Value))
					{
						objObject.ProductID = int.Parse(odrPCS[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].ToString().Trim());
					}
					else
					{
						objObject.ProductID = 0;
					}
					
					if(!odrPCS[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Equals(DBNull.Value))
					{
						objObject.ProductGroupID = int.Parse(odrPCS[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].ToString().Trim());
					}
					else
					{
						objObject.ProductGroupID = 0;
					}

					objObject.Line = int.Parse(odrPCS[CST_ActCostAllocationDetailTable.LINE_FLD].ToString().Trim());
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

		public DataTable GetDetailByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDetailByMaster()";

			DataTable dtbTable = new DataTable(CST_ActCostAllocationDetailTable.TABLE_NAME);
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT cst_ActCostAllocationDetail.Line,";
				strSql += " STD_CostElement.Name as STD_CostElementName,";
				strSql += " cst_ActCostAllocationDetail.AllocationAmount,";	
				strSql += " MST_Department.Code AS MST_DepartmentCode,";
				strSql += " PRO_ProductionLine.Code AS PRO_ProductionLineCode,";
				strSql += " CST_ProductGroup.Code AS CST_ProductGroupCode,";
				strSql += " ITM_Product.Code AS ITM_ProductCode,";
				strSql += " ITM_Product.Description as ITM_ProductDescription,";
				strSql += " ITM_Product.Revision as ITM_ProductRevision,";
				strSql += " MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,";
				strSql += " cst_ActCostAllocationDetail.ActCostAllocationMasterID,";
				strSql += " cst_ActCostAllocationDetail.ActCostAllocationDetailID,";
				strSql += " cst_ActCostAllocationDetail.CostElementID,";
				strSql += " cst_ActCostAllocationDetail.DepartmentID,";
				strSql += " cst_ActCostAllocationDetail.ProductionLineID,";
				strSql += " cst_ActCostAllocationDetail.ProductID,";
				strSql += " cst_ActCostAllocationDetail.ProductGroupID";
      
				strSql += " FROM    cst_ActCostAllocationDetail";
				strSql += " 	INNER JOIN STD_CostElement ON cst_ActCostAllocationDetail.CostElementID = STD_CostElement.CostElementID";
				strSql += " 	LEFT JOIN MST_Department ON cst_ActCostAllocationDetail.DepartmentID = MST_Department.DepartmentID";
				strSql += " 	LEFT JOIN CST_ProductGroup ON cst_ActCostAllocationDetail.ProductGroupID = CST_ProductGroup.ProductGroupID";
				strSql += " 	LEFT JOIN ITM_Product ON cst_ActCostAllocationDetail.ProductID = ITM_Product.ProductID";
				strSql += " 	LEFT JOIN PRO_ProductionLine ON cst_ActCostAllocationDetail.ProductionLineID = PRO_ProductionLine.ProductionLineID";
				strSql += " 	LEFT JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";
				strSql += " WHERE cst_ActCostAllocationDetail.ActCostAllocationMasterID = " + pintMasterID;
				strSql += " ORDER BY cst_ActCostAllocationDetail.Line ASC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbTable);
								
				return dtbTable;
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
		///       This method uses to update data to CST_ActCostAllocationDetail
		///    </Description>
		///    <Inputs>
		///       CST_ActCostAllocationDetailVO       
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

			CST_ActCostAllocationDetailVO objObject = (CST_ActCostAllocationDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE CST_ActCostAllocationDetail SET "
					+ CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD + "=   ?" + ","
					+ CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD + "=   ?" + ","
					+ CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD + "=   ?" + ","
					+ CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + "=   ?" + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + "=   ?" + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTID_FLD + "=   ?" + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + "=   ?" + ","
					+ CST_ActCostAllocationDetailTable.LINE_FLD + "=  ?"
					+" WHERE " + CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD].Value = objObject.AllocationAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD].Value = objObject.ActCostAllocationMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD, OleDbType.Integer));
				if(objObject.DepartmentID > 0)
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].Value = objObject.DepartmentID;
				}
				else
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD, OleDbType.Integer));
				if(objObject.ProductionLineID > 0)
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].Value = objObject.ProductionLineID;
				}
				else
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				if(objObject.ProductID > 0)
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;
				}
				else
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.PRODUCTID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD, OleDbType.Integer));
				if(objObject.ProductGroupID > 0)
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Value = objObject.ProductGroupID;
				}
				else
				{
					ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONDETAILID_FLD].Value = objObject.ActCostAllocationDetailID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
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
		///       This method uses to get all data from CST_ActCostAllocationDetail
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
		///       Tuesday, February 21, 2006
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
					+ CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONDETAILID_FLD + ","
					+ CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD + ","
					+ CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
					+ CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD + ","
					+ CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTID_FLD + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + ","
					+ CST_ActCostAllocationDetailTable.LINE_FLD
					+ " FROM " + CST_ActCostAllocationDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,CST_ActCostAllocationDetailTable.TABLE_NAME);

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
		///       Tuesday, February 21, 2006
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
					+ CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONDETAILID_FLD + ","
					+ CST_ActCostAllocationDetailTable.ALLOCATIONAMOUNT_FLD + ","
					+ CST_ActCostAllocationDetailTable.ACTCOSTALLOCATIONMASTERID_FLD + ","
					+ CST_ActCostAllocationDetailTable.COSTELEMENTID_FLD + ","
					+ CST_ActCostAllocationDetailTable.DEPARTMENTID_FLD + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTIONLINEID_FLD + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTID_FLD + ","
					+ CST_ActCostAllocationDetailTable.PRODUCTGROUPID_FLD + ","
					+ CST_ActCostAllocationDetailTable.LINE_FLD 
					+ "  FROM " + CST_ActCostAllocationDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,CST_ActCostAllocationDetailTable.TABLE_NAME);
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
				}
				else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{

					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
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
		///       This method uses to get all product used for import actual cost distribution setup
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
		///       DuongNA
		///    </Authors>
		///    <History>
		///       Tuesday, March 07, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetAllProducts()
		{
			const string METHOD_NAME = THIS + ".GetAllProducts()";
			DataSet dstPCS = new DataSet();		
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ProductID, ITM_Product.Code, ITM_Product.Description, Revision,"
					+ " U.Code AS " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD
					+ " FROM ITM_Product JOIN MST_UnitOfMeasure U ON ITM_Product.StockUMID = U.UnitOfMeasureID";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,CST_ActCostAllocationDetailTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all product used for import actual cost distribution setup
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
		///       DuongNA
		///    </Authors>
		///    <History>
		///       Tuesday, March 07, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetAllProductionLine()
		{
			const string METHOD_NAME = THIS + ".GetAllProductionLine()";
			DataSet dstPCS = new DataSet();		
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ProductionLineID, Code, Name, DepartmentID, LocationID, BalancePlanning, RoundUpDaysException"
					+ " FROM PRO_ProductionLine";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ProductionLineTable.TABLE_NAME);

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

		public DataSet GetAllGroup()
		{
			const string METHOD_NAME = THIS + ".GetAllGroup()";
			DataSet dstPCS = new DataSet();		
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ProductGroupID, Code, Description, ProductionLineID"
					+ " FROM CST_ProductGroup";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ProductionLineTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all product used for import actual cost distribution setup
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
		///       DuongNA
		///    </Authors>
		///    <History>
		///       Tuesday, March 07, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetAllCostElements()
		{
			const string METHOD_NAME = THIS + ".GetAllCostElements()";
			DataSet dstPCS = new DataSet();		
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
			
				strSql=	"SELECT "
					+ STD_CostElementTable.COSTELEMENTID_FLD + ","
					+ STD_CostElementTable.NAME_FLD
					+ " FROM " + STD_CostElementTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
			
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,CST_ActCostAllocationDetailTable.TABLE_NAME);

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
	}
}
