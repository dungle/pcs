using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComProduction.DCP.DS
{
	public class PRO_DCOptionDetailDS 
	{
		public PRO_DCOptionDetailDS()
		{
		}
		private const string THIS = "PCSComProduction.DCP.DS.PRO_DCOptionDetailDS";

	
		///    <summary>
		///       This method uses to add data to PRO_DCOptionDetail
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
		///    </History>


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_DCOptionDetailVO objObject = (PRO_DCOptionDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_DCOptionDetail("
				+ PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD + ","
				+ PRO_DCOptionDetailTable.WORKORDER_FLD + ","
				+ PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD + ")"
				+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionDetailTable.WORKORDER_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionDetailTable.WORKORDER_FLD].Value = objObject.WorkOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD].Value = objObject.DCOptionMasterID;


				
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
	

	

		///    <summary>
		///       This method uses to add data to PRO_DCOptionDetail
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + PRO_DCOptionDetailTable.TABLE_NAME + " WHERE  " + "DCOptionDetailID" + "=" + pintID.ToString();
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
	

	

		///    <summary>
		///       This method uses to add data to PRO_DCOptionDetail
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
		///    </History>

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
				+ PRO_DCOptionDetailTable.DCOPTIONDETAILID_FLD + ","
				+ PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD + ","
				+ PRO_DCOptionDetailTable.WORKORDER_FLD + ","
				+ PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD
				+ " FROM " + PRO_DCOptionDetailTable.TABLE_NAME
				+ " WHERE " + PRO_DCOptionDetailTable.DCOPTIONDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_DCOptionDetailVO objObject = new PRO_DCOptionDetailVO();

				while (odrPCS.Read())
				{ 
				objObject.DCOptionDetailID = int.Parse(odrPCS[PRO_DCOptionDetailTable.DCOPTIONDETAILID_FLD].ToString().Trim());
				objObject.MasterLocationID = int.Parse(odrPCS[PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD].ToString().Trim());
				objObject.WorkOrder = bool.Parse(odrPCS[PRO_DCOptionDetailTable.WORKORDER_FLD].ToString().Trim());
				objObject.DCOptionMasterID = int.Parse(odrPCS[PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD].ToString().Trim());

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


		///    <summary>
		///       This method uses to add data to PRO_DCOptionDetail
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_DCOptionDetailVO objObject = (PRO_DCOptionDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_DCOptionDetail SET "
				+ PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD + "=   ?" + ","
				+ PRO_DCOptionDetailTable.WORKORDER_FLD + "=   ?" + ","
				+ PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD + "=  ?"
				+" WHERE " + PRO_DCOptionDetailTable.DCOPTIONDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionDetailTable.WORKORDER_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionDetailTable.WORKORDER_FLD].Value = objObject.WorkOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD].Value = objObject.DCOptionMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionDetailTable.DCOPTIONDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionDetailTable.DCOPTIONDETAILID_FLD].Value = objObject.DCOptionDetailID;


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


		///    <summary>
		///       This method uses to add data to PRO_DCOptionDetail
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
		///    </History>

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
				+ PRO_DCOptionDetailTable.DCOPTIONDETAILID_FLD + ","
				+ PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD + ","
				+ PRO_DCOptionDetailTable.WORKORDER_FLD + ","
				+ PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD
					+ " FROM " + PRO_DCOptionDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCOptionDetailTable.TABLE_NAME);

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
		
		public DataSet GetDetailByMaster(int pintDCOptionMaster)
		{
			const string METHOD_NAME = THIS + ".GetDetailByMaster()";
			DataSet dstPCS = new DataSet();		

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			
			try
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ PRO_DCOptionDetailTable.TABLE_NAME + "." + PRO_DCOptionDetailTable.DCOPTIONDETAILID_FLD + ", "
					+ PRO_DCOptionDetailTable.TABLE_NAME + "." + PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD + ", "
					+ MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.CODE_FLD + " as " + MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD +  ", "
					+ PRO_DCOptionDetailTable.TABLE_NAME + "." + PRO_DCOptionDetailTable.WORKORDER_FLD + ", "					
					+ PRO_DCOptionDetailTable.TABLE_NAME + "." + PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD
					+ " FROM " + PRO_DCOptionDetailTable.TABLE_NAME 
					+ " LEFT JOIN " + MST_MasterLocationTable.TABLE_NAME
					+ " ON " + MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.MASTERLOCATIONID_FLD
					+ "=" + PRO_DCOptionDetailTable.TABLE_NAME + "." + PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD
					+ " WHERE " + PRO_DCOptionDetailTable.TABLE_NAME + "." + PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD + "=" + pintDCOptionMaster;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCOptionDetailTable.TABLE_NAME);

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


		///    <summary>
		///       This method uses to add data to PRO_DCOptionDetail
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
		///    </History>
		
		public void UpdateDataSet(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();
			
			try
			{
				strSql=	"SELECT "
				+ PRO_DCOptionDetailTable.DCOPTIONDETAILID_FLD + ","
				+ PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD + ","
				+ PRO_DCOptionDetailTable.WORKORDER_FLD + ","
				+ PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD 
				+ " FROM " + PRO_DCOptionDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				
				odadPCS.Update(pdstData, PRO_DCOptionDetailTable.TABLE_NAME);
				pdstData.AcceptChanges();
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
	}
}
