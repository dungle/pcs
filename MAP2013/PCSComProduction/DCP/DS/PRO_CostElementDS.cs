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
	public class PRO_CostElementDS 
	{
		public PRO_CostElementDS()
		{
		}
		private const string THIS = "PCSComProduction.DCP.DS.PRO_CostElementDS";

	
		///    <summary>
		///       This method uses to add data to PRO_CostElement
		///    </summary>
		///    <Inputs>
		///        PRO_CostElementVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
		///    </History>


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_CostElementVO objObject = (PRO_CostElementVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_CostElement("
				+ PRO_CostElementTable.CODE_FLD + ","
				+ PRO_CostElementTable.DESCRIPTION_FLD + ")"
				+ "VALUES(?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CostElementTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_CostElementTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CostElementTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_CostElementTable.DESCRIPTION_FLD].Value = objObject.Description;


				
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
		///       This method uses to add data to PRO_CostElement
		///    </summary>
		///    <Inputs>
		///        PRO_CostElementVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + PRO_CostElementTable.TABLE_NAME + " WHERE  " + "CostElementID" + "=" + pintID.ToString();
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
		///       This method uses to add data to PRO_CostElement
		///    </summary>
		///    <Inputs>
		///        PRO_CostElementVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
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
				+ PRO_CostElementTable.COSTELEMENTID_FLD + ","
				+ PRO_CostElementTable.CODE_FLD + ","
				+ PRO_CostElementTable.DESCRIPTION_FLD
				+ " FROM " + PRO_CostElementTable.TABLE_NAME
				+" WHERE " + PRO_CostElementTable.COSTELEMENTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_CostElementVO objObject = new PRO_CostElementVO();

				while (odrPCS.Read())
				{ 
				objObject.CostElementID = int.Parse(odrPCS[PRO_CostElementTable.COSTELEMENTID_FLD].ToString().Trim());
				objObject.Code = odrPCS[PRO_CostElementTable.CODE_FLD].ToString().Trim();
				objObject.Description = odrPCS[PRO_CostElementTable.DESCRIPTION_FLD].ToString().Trim();

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
		///       This method uses to add data to PRO_CostElement
		///    </summary>
		///    <Inputs>
		///        PRO_CostElementVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_CostElementVO objObject = (PRO_CostElementVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_CostElement SET "
				+ PRO_CostElementTable.CODE_FLD + "=   ?" + ","
				+ PRO_CostElementTable.DESCRIPTION_FLD + "=  ?"
				+" WHERE " + PRO_CostElementTable.COSTELEMENTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CostElementTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_CostElementTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CostElementTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_CostElementTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_CostElementTable.COSTELEMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_CostElementTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;


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
		///       This method uses to add data to PRO_CostElement
		///    </summary>
		///    <Inputs>
		///        PRO_CostElementVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
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
				+ PRO_CostElementTable.COSTELEMENTID_FLD + ","
				+ PRO_CostElementTable.CODE_FLD + ","
				+ PRO_CostElementTable.DESCRIPTION_FLD
					+ " FROM " + PRO_CostElementTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_CostElementTable.TABLE_NAME);

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
		///       This method uses to add data to PRO_CostElement
		///    </summary>
		///    <Inputs>
		///        PRO_CostElementVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
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
				+ PRO_CostElementTable.COSTELEMENTID_FLD + ","
				+ PRO_CostElementTable.CODE_FLD + ","
				+ PRO_CostElementTable.DESCRIPTION_FLD 
		+ "  FROM " + PRO_CostElementTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,PRO_CostElementTable.TABLE_NAME);

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
