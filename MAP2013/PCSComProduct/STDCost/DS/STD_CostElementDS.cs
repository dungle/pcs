using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;


//using PCS's namespace
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComProduct.STDCost.DS
{
	public class STD_CostElementDS 
	{
		public STD_CostElementDS()
		{
		}
		private const string THIS = "PCSComProduct.STDCost.DS.STD_CostElementDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to STD_CostElement
		///    </Description>
		///    <Inputs>
		///        STD_CostElementVO       
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
		///       Tuesday, February 07, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS =null;
			try
			{
				STD_CostElementVO voCostElement = (STD_CostElementVO) pobjObjectVO;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
								
				string strSql =	"INSERT INTO STD_CostElement("
				+ STD_CostElementTable.CODE_FLD + ","
				+ STD_CostElementTable.NAME_FLD + ","
				+ STD_CostElementTable.ORDERNO_FLD + ","
				+ STD_CostElementTable.PARENTID_FLD + ","
				+ STD_CostElementTable.ISLEAF_FLD + ","
				+ STD_CostElementTable.COSTELEMENTTYPEID_FLD + ")"
				+ "VALUES(?, ?,?,?,?,?)";
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[STD_CostElementTable.CODE_FLD].Value = voCostElement.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.NAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[STD_CostElementTable.NAME_FLD].Value = voCostElement.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.ORDERNO_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostElementTable.ORDERNO_FLD].Value = voCostElement.OrderNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.PARENTID_FLD, OleDbType.Integer));
				if(voCostElement.ParentID > 0)
				{
					ocmdPCS.Parameters[STD_CostElementTable.PARENTID_FLD].Value = voCostElement.ParentID;
				}
				else
				{
					ocmdPCS.Parameters[STD_CostElementTable.PARENTID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.ISLEAF_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostElementTable.ISLEAF_FLD].Value = voCostElement.IsLeaf;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.COSTELEMENTTYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostElementTable.COSTELEMENTTYPEID_FLD].Value = voCostElement.CostElementTypeID;
				
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
	
		public bool IsCostElementUsed(int pintCostElementID)
		{
			const string METHOD_NAME = THIS + ".IsUsed()";
			 
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{	
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				string strSql = "SELECT ";

				//Count in STD_CostCenterRateDetail table
				strSql += " (";
				strSql += " SELECT Count(CostElementID)";
				strSql += " FROM STD_CostCenterRateDetail";
				strSql += " WHERE CostElementID = " + pintCostElementID ;
				strSql += ")";

				//Count in CST_STDItemCost table
				strSql += " + (";
				strSql += " SELECT Count(CostElementID)";
				strSql += " FROM CST_STDItemCost";
				strSql += " WHERE CostElementID = " + pintCostElementID ;
				strSql += ")";

				//Count in cst_AllocationResult table
				strSql += " + (";
				strSql += " SELECT Count(CostElementID)";
				strSql += " FROM cst_AllocationResult";
				strSql += " WHERE CostElementID = " + pintCostElementID ;
				strSql += ")";	

				/*
				//Count in cst_FreightMaster table
				strSql += " + (";
				strSql += " SELECT Count(CostElementID)";
				strSql += " FROM cst_FreightMaster";
				strSql += " WHERE CostElementID = " + pintCostElementID ;
				strSql += ")";
				*/

				//Count in CST_ActualCostHistory table
				strSql += " + (";
				strSql += " SELECT Count(CostElementID)";
				strSql += " FROM CST_ActualCostHistory";
				strSql += " WHERE CostElementID = " + pintCostElementID ;
				strSql += ")";

				//Count in cst_ActCostAllocationDetail table
				strSql += " + (";
				strSql += " SELECT Count(CostElementID)";
				strSql += " FROM cst_ActCostAllocationDetail";
				strSql += " WHERE CostElementID = " + pintCostElementID ;
				strSql += ")";

				strSql += " as TotalRecord";					   

				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturnValue = ocmdPCS.ExecuteScalar();
				if(objReturnValue == null)
				{
					return false;
				}
				else if(int.Parse(objReturnValue.ToString()) > 0)
				{ 
					return true;
				}

				return false;
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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

		/// <summary>
		/// Check duplicate key
		/// </summary>
		/// <param name="pstrField"></param>
		/// <param name="pstrValue"></param>
		/// <param name="pintCostElementID"></param>
		/// <returns></returns>
		public bool IsCostElementDuplicate(string pstrField, string pstrValue, int pintCostElementID)
		{
			const string METHOD_NAME = THIS + ".IsCostElementDuplicate()";
			 
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{				
				string strSql = "SELECT Count(*)";
					   strSql += " FROM STD_CostElement";
					   strSql += " WHERE " + pstrField + " = '" + pstrValue.Replace("'", "''") + "'";					   
					   strSql += " AND ( CostElementID <> " + pintCostElementID + ")";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);				
				ocmdPCS.Connection.Open();
				
				object objReturnValue = ocmdPCS.ExecuteScalar();
				if(objReturnValue == null)
				{
					return false;
				}
				else if(int.Parse(objReturnValue.ToString()) > 0)
				{ 
					return true;
				}

				return false;
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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

		/// <summary>
		/// Check if element type is duplicate
		/// </summary>
		/// <param name="pstrValue"></param>
		/// <param name="pintCostElementID"></param>
		/// <returns></returns>
		
		public bool IsElementTypeDuplicate(string pstrValue, int pintCostElementID)
		{
			const string METHOD_NAME = THIS + ".IsElementTypeDuplicate()";
			 
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{
				string strSql = "SELECT Count(STD_CostElement.CostElementID)";
				strSql += " FROM STD_CostElement";
				strSql += " INNER JOIN STD_CostElementType ON STD_CostElementType.CostElementTypeID = STD_CostElement.CostElementTypeID";
				strSql += " WHERE STD_CostElementType.CostElementTypeID = " + pstrValue;
				strSql += " AND ( STD_CostElement.CostElementID <> " + pintCostElementID + ")";
				strSql += " AND ( STD_CostElement.ParentID IS NULL)";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);				
				ocmdPCS.Connection.Open();
				
				object objReturnValue = ocmdPCS.ExecuteScalar();
				if(objReturnValue == null)
				{
					return false;
				}
				else if(int.Parse(objReturnValue.ToString()) > 0)
				{ 
					return true;
				}

				return false;
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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

		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{
				STD_CostElementVO voCostElement = (STD_CostElementVO) pobjObjectVO;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				string strSql = "INSERT INTO STD_CostElement("
					+ STD_CostElementTable.CODE_FLD + ","
					+ STD_CostElementTable.NAME_FLD + ","
					+ STD_CostElementTable.ORDERNO_FLD + ","
					+ STD_CostElementTable.PARENTID_FLD + ","
					+ STD_CostElementTable.ISLEAF_FLD + ","
					+ STD_CostElementTable.COSTELEMENTTYPEID_FLD + ")"
					+ "VALUES(?, ?,?,?,?,?)";

				strSql += "; SELECT @@IDENTITY as LatestID";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[STD_CostElementTable.CODE_FLD].Value = voCostElement.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.NAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[STD_CostElementTable.NAME_FLD].Value = voCostElement.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.ORDERNO_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostElementTable.ORDERNO_FLD].Value = voCostElement.OrderNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.PARENTID_FLD, OleDbType.Integer));
				if(voCostElement.ParentID > 0)
				{
					ocmdPCS.Parameters[STD_CostElementTable.PARENTID_FLD].Value = voCostElement.ParentID;
				}
				else
				{
					ocmdPCS.Parameters[STD_CostElementTable.PARENTID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.ISLEAF_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostElementTable.ISLEAF_FLD].Value = voCostElement.IsLeaf;				
				
				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.COSTELEMENTTYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostElementTable.COSTELEMENTTYPEID_FLD].Value = voCostElement.CostElementTypeID;
				
				ocmdPCS.Connection.Open();

				return int.Parse(ocmdPCS.ExecuteScalar().ToString());				
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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
		///       This method uses to delete data from STD_CostElement
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

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS;

			try
			{
				string strSql = "DELETE " + STD_CostElementTable.TABLE_NAME 
					+ " WHERE CostElementID" + "=" + pintID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
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
				if (oconPCS != null) 
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
		///       This method uses to get data from STD_CostElement
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       STD_CostElementVO
		///    </Outputs>
		///    <Returns>
		///       STD_CostElementVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 07, 2006
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
				string strSql = "SELECT "
				+ STD_CostElementTable.COSTELEMENTID_FLD + ","
				+ STD_CostElementTable.CODE_FLD + ","
				+ STD_CostElementTable.NAME_FLD + ","
				+ STD_CostElementTable.ORDERNO_FLD + ","
				+ STD_CostElementTable.PARENTID_FLD + ","
				+ STD_CostElementTable.ISLEAF_FLD + ","
				+ STD_CostElementTable.COSTELEMENTTYPEID_FLD
				+ " FROM " + STD_CostElementTable.TABLE_NAME
				+" WHERE " + STD_CostElementTable.COSTELEMENTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				STD_CostElementVO voCostElement = new STD_CostElementVO();

				while (odrPCS.Read())
				{ 
					voCostElement.CostElementID = int.Parse(odrPCS[STD_CostElementTable.COSTELEMENTID_FLD].ToString().Trim());
					voCostElement.Code = odrPCS[STD_CostElementTable.CODE_FLD].ToString().Trim();
					voCostElement.Name = odrPCS[STD_CostElementTable.NAME_FLD].ToString().Trim();
					voCostElement.OrderNo = int.Parse(odrPCS[STD_CostElementTable.ORDERNO_FLD].ToString().Trim());
					voCostElement.CostElementTypeID = int.Parse(odrPCS[STD_CostElementTable.COSTELEMENTTYPEID_FLD].ToString().Trim());

					if(!odrPCS[STD_CostElementTable.PARENTID_FLD].Equals(DBNull.Value))
					{
						voCostElement.ParentID = int.Parse(odrPCS[STD_CostElementTable.PARENTID_FLD].ToString().Trim());
					}
					else
					{
						voCostElement.ParentID = 0;
					}

					if(!odrPCS[STD_CostElementTable.ISLEAF_FLD].Equals(DBNull.Value))
					{
						voCostElement.IsLeaf = int.Parse(odrPCS[STD_CostElementTable.ISLEAF_FLD].ToString().Trim());
					}
					else
					{
						voCostElement.IsLeaf = 0; 
					}
				}

				return voCostElement;					
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
		///       This method uses to update data to STD_CostElement
		///    </Description>
		///    <Inputs>
		///       STD_CostElementVO       
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

			STD_CostElementVO voCostElement = (STD_CostElementVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				string strSql = "UPDATE STD_CostElement SET "
				+ STD_CostElementTable.CODE_FLD + "=   ?" + ","
				+ STD_CostElementTable.NAME_FLD + "=   ?" + ","
				+ STD_CostElementTable.ORDERNO_FLD + "=   ?" + ","
				+ STD_CostElementTable.PARENTID_FLD + "=   ?" + ","
				+ STD_CostElementTable.ISLEAF_FLD + "=   ?" + ","
				+ STD_CostElementTable.COSTELEMENTTYPEID_FLD + "=  ?"
				+ " WHERE " + STD_CostElementTable.COSTELEMENTID_FLD + "= ?";
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[STD_CostElementTable.CODE_FLD].Value = voCostElement.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.NAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[STD_CostElementTable.NAME_FLD].Value = voCostElement.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.ORDERNO_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostElementTable.ORDERNO_FLD].Value = voCostElement.OrderNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.PARENTID_FLD, OleDbType.Integer));
				if(voCostElement.ParentID > 0)
				{
					ocmdPCS.Parameters[STD_CostElementTable.PARENTID_FLD].Value = voCostElement.ParentID;
				}
				else
				{
					ocmdPCS.Parameters[STD_CostElementTable.PARENTID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.ISLEAF_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostElementTable.ISLEAF_FLD].Value = voCostElement.IsLeaf;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.COSTELEMENTTYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostElementTable.COSTELEMENTTYPEID_FLD].Value = voCostElement.CostElementTypeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(STD_CostElementTable.COSTELEMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[STD_CostElementTable.COSTELEMENTID_FLD].Value = voCostElement.CostElementID;
				
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
		///       This method uses to get all data from STD_CostElement
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
		///       Tuesday, February 07, 2006
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
				+ STD_CostElementTable.COSTELEMENTID_FLD + ","
				+ STD_CostElementTable.CODE_FLD + ","
				+ STD_CostElementTable.NAME_FLD + ","
				+ STD_CostElementTable.ORDERNO_FLD + ","
				+ STD_CostElementTable.PARENTID_FLD + ","
				+ STD_CostElementTable.ISLEAF_FLD + ","
				+ STD_CostElementTable.COSTELEMENTTYPEID_FLD
				+ " FROM " + STD_CostElementTable.TABLE_NAME
				+ " ORDER BY OrderNo ASC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, STD_CostElementTable.TABLE_NAME);

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


		public DataSet ListLeafElements()
		{
			const string METHOD_NAME = THIS + ".ListLeafElements()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT STD_CostElement.*, STD_CostElementType.Code AS TypeCode"
					+ " FROM STD_CostElement JOIN STD_CostElementType"
					+ " ON STD_CostElement.CostElementTypeID = STD_CostElementType.CostElementTypeID"
					+ " WHERE IsLeaf = 1"
					+ " ORDER BY OrderNo ASC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, STD_CostElementTable.TABLE_NAME);

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

		public DataTable ListAll()
		{
			const string METHOD_NAME = THIS + ".ListAll()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT STD_CostElement.*, STD_CostElementType.Code AS TypeCode"
					+ " FROM STD_CostElement JOIN STD_CostElementType"
					+ " ON STD_CostElement.CostElementTypeID = STD_CostElementType.CostElementTypeID"
					+ " WHERE IsLeaf = 1"
					+ " ORDER BY OrderNo ASC";

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
		///       Tuesday, February 07, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		public void UpdateDataSet(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;

			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
				+ STD_CostElementTable.COSTELEMENTID_FLD + ","
				+ STD_CostElementTable.CODE_FLD + ","
				+ STD_CostElementTable.NAME_FLD + ","
				+ STD_CostElementTable.ORDERNO_FLD + ","
				+ STD_CostElementTable.PARENTID_FLD + ","
				+ STD_CostElementTable.ISLEAF_FLD + ","
				+ STD_CostElementTable.COSTELEMENTTYPEID_FLD 
				+ "  FROM " + STD_CostElementTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, STD_CostElementTable.TABLE_NAME);

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
	}
}
