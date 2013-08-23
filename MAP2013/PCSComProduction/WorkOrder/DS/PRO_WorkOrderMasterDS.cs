using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComProduction.WorkOrder.DS
{
	public class PRO_WorkOrderMasterDS 
	{
		public PRO_WorkOrderMasterDS()
		{
		}
		private const string THIS = "PCSComProduction.WorkOrder.DS.PRO_WorkOrderMasterDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PRO_WorkOrderMaster
		///    </Description>
		///    <Inputs>
		///        PRO_WorkOrderMasterVO       
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
		///       Tuesday, May 31, 2005
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
				PRO_WorkOrderMasterVO objObject = (PRO_WorkOrderMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_WorkOrderMaster("
					+ PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + ","
					+ PRO_WorkOrderMasterTable.DESCRIPTION_FLD + ","
					+ PRO_WorkOrderMasterTable.CCNID_FLD + ","
					+ PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ","
					+ PRO_WorkOrderMasterTable.TRANSDATE_FLD + ")"
					+ "VALUES(?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.WORKORDERNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Value = objObject.WorkOrderNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.TRANSDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.TRANSDATE_FLD].Value = objObject.TransDate;


				
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
		///       This method uses to delete data from PRO_WorkOrderMaster
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
			strSql=	" DELETE " + PRO_WorkOrderMasterTable.TABLE_NAME + " WHERE  " + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + "=" + pintID.ToString()
				+   " UPDATE " + MTR_CPOTable.TABLE_NAME + " SET " + MTR_CPOTable.WOGENERATEDID_FLD + "= null WHERE " + MTR_CPOTable.WOGENERATEDID_FLD + "=" + pintID; 
					
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
		///       This method uses to get data from PRO_WorkOrderMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PRO_WorkOrderMasterVO
		///    </Outputs>
		///    <Returns>
		///       PRO_WorkOrderMasterVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, May 31, 2005
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
					+ PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + ","
					+ PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + ","
					+ PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD + ","
					+ PRO_WorkOrderMasterTable.DESCRIPTION_FLD + ","
					+ PRO_WorkOrderMasterTable.CCNID_FLD + ","
					+ PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ","
					+ PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_WorkOrderMasterTable.TRANSDATE_FLD
					+ " FROM " + PRO_WorkOrderMasterTable.TABLE_NAME
					+" WHERE " + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_WorkOrderMasterVO objObject = new PRO_WorkOrderMasterVO();

				while (odrPCS.Read())
				{ 
					objObject.MasterLocationID = int.Parse(odrPCS[PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.WorkOrderMasterID = int.Parse(odrPCS[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString().Trim());
					if ((odrPCS[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD].ToString().Trim() != string.Empty) 
						&& (odrPCS[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD] != DBNull.Value))
					{
						objObject.ProductionLineID = int.Parse(odrPCS[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD].ToString().Trim());
					}
					else
						objObject.ProductionLineID = 0;
					objObject.Description = odrPCS[PRO_WorkOrderMasterTable.DESCRIPTION_FLD].ToString().Trim();
					objObject.CCNID = int.Parse(odrPCS[PRO_WorkOrderMasterTable.CCNID_FLD].ToString().Trim());
					objObject.WorkOrderNo = odrPCS[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString().Trim();
					objObject.TransDate = DateTime.Parse(odrPCS[PRO_WorkOrderMasterTable.TRANSDATE_FLD].ToString().Trim());
					try
					{
						objObject.DCOptionMasterID = Convert.ToInt32(odrPCS[PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD]);
					}
					catch{}

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
		///       This method uses to update data to PRO_WorkOrderMaster
		///    </Description>
		///    <Inputs>
		///       PRO_WorkOrderMasterVO       
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

			PRO_WorkOrderMasterVO objObject = (PRO_WorkOrderMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_WorkOrderMaster SET "
					+ PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD + "=   ?" + ","
					+ PRO_WorkOrderMasterTable.DESCRIPTION_FLD + "=   ?" + ","
					+ PRO_WorkOrderMasterTable.CCNID_FLD + "=   ?" + ","
					+ PRO_WorkOrderMasterTable.WORKORDERNO_FLD + "=   ?" + ","
					+ PRO_WorkOrderMasterTable.TRANSDATE_FLD + "=  ?" + ","
					+ PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD + "=  ?"
					+" WHERE " + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD, OleDbType.Integer));
				if (objObject.ProductionLineID != 0)
				{
					ocmdPCS.Parameters[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD].Value = objObject.ProductionLineID;
				}
				else
					ocmdPCS.Parameters[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.WORKORDERNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Value = objObject.WorkOrderNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.TRANSDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.TRANSDATE_FLD].Value = objObject.TransDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD, OleDbType.Integer));
				// 19-04-2006 dungla: fix bug 3789 for NgaHT
				if (objObject.DCOptionMasterID > 0)
					ocmdPCS.Parameters[PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD].Value = objObject.DCOptionMasterID;
				else
					ocmdPCS.Parameters[PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD].Value = DBNull.Value;
				// 19-04-2006 dungla: fix bug 3789 for NgaHT

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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
		///       This method uses to get all data from PRO_WorkOrderMaster
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
		///       Tuesday, May 31, 2005
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
					+ PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + ","
					+ PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + ","
					+ PRO_WorkOrderMasterTable.DESCRIPTION_FLD + ","
					+ PRO_WorkOrderMasterTable.CCNID_FLD + ","
					+ PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ","
					+ PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_WorkOrderMasterTable.TRANSDATE_FLD
					+ " FROM " + PRO_WorkOrderMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_WorkOrderMasterTable.TABLE_NAME);

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
		/// <summary>
		/// Get WorkOrderNo array to search and make a new WorkOrder No.
		/// </summary>
		/// <param name="pstrQuery"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Mar 24 2006</date>
		public DataSet GetWorkOrderNo(String pstrQuery)
		{
			const string METHOD_NAME = THIS + ".GetWorkOrderNo()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	pstrQuery;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_WorkOrderMasterTable.TABLE_NAME);

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
		///       Tuesday, May 31, 2005
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
					+ PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + ","
					+ PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + ","
					+ PRO_WorkOrderMasterTable.DESCRIPTION_FLD + ","
					+ PRO_WorkOrderMasterTable.CCNID_FLD + ","
					+ PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ","
					+ PRO_WorkOrderMasterTable.TRANSDATE_FLD 
					+ "  FROM " + PRO_WorkOrderMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PRO_WorkOrderMasterTable.TABLE_NAME);

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

		/// <summary>
		/// add new work order to database and return new ID
		/// </summary>
		/// <param name="pobjObjectData">PRO_WorkOrderMasterVO</param>
		public int AddAndReturnID(object pobjObjectData)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_WorkOrderMasterVO objObject = (PRO_WorkOrderMasterVO) pobjObjectData;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_WorkOrderMaster("
					+ PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + ","
					+ PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD + ","
					+ PRO_WorkOrderMasterTable.DESCRIPTION_FLD + ","
					+ PRO_WorkOrderMasterTable.CCNID_FLD + ","
					+ PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ","
					+ PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_WorkOrderMasterTable.TRANSDATE_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?) "
					+ "SELECT @@IDENTITY";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD, OleDbType.Integer));
				if (objObject.ProductionLineID != 0)
				{
					ocmdPCS.Parameters[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD].Value = objObject.ProductionLineID;
				}
				else
					ocmdPCS.Parameters[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.WORKORDERNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Value = objObject.WorkOrderNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.DCOPTIONMASTERID_FLD].Value = objObject.DCOptionMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderMasterTable.TRANSDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_WorkOrderMasterTable.TRANSDATE_FLD].Value = objObject.TransDate;

				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objReturn = ocmdPCS.ExecuteScalar();	
				if (objReturn != null)
				{
					return int.Parse(objReturn.ToString());
				}
				else
				{
					return 0;
				}
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);	
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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

		public object GetWOMasterInfoByWODetailID(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetWOMasterInfoByWODetailID()";
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + ","
					+ PRO_WorkOrderMasterTable.WORKORDERNO_FLD
					+ " FROM " + PRO_WorkOrderMasterTable.TABLE_NAME
					+ " WHERE " + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD 
					+ "     IN (SELECT " + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD 
					+ "         FROM " + PRO_WorkOrderDetailTable.TABLE_NAME 
					+ "         WHERE " + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "=" + pintID
					+ "        )" ;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_WorkOrderMasterVO objObject = new PRO_WorkOrderMasterVO();

				while (odrPCS.Read())
				{ 
					objObject.WorkOrderMasterID = int.Parse(odrPCS[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString().Trim());
					objObject.WorkOrderNo = odrPCS[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString().Trim();

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

	}
}
