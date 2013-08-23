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
	public class PRO_ShiftCapacityDS 
	{
		public PRO_ShiftCapacityDS()
		{
		}
		private const string THIS = "PCSComProduction.DCP.DS.PRO_ShiftCapacityDS";

	
		///    <summary>
		///       This method uses to add data to PRO_ShiftCapacity
		///    </summary>
		///    <Inputs>
		///        PRO_ShiftCapacityVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_ShiftCapacityVO objObject = (PRO_ShiftCapacityVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_ShiftCapacity("
				+ PRO_ShiftCapacityTable.WCCAPACITYID_FLD + ","
				+ PRO_ShiftCapacityTable.SHIFTID_FLD + ")"
				+ "VALUES(?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftCapacityTable.WCCAPACITYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ShiftCapacityTable.WCCAPACITYID_FLD].Value = objObject.WCCapacityID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftCapacityTable.SHIFTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ShiftCapacityTable.SHIFTID_FLD].Value = objObject.ShiftID;


				
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
		///       This method uses to add data to PRO_ShiftCapacity
		///    </summary>
		///    <Inputs>
		///        PRO_ShiftCapacityVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + PRO_ShiftCapacityTable.TABLE_NAME + " WHERE  " + PRO_ShiftCapacityTable.SHIFTCAPACITYID_FLD + "=" + pintID;
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
		
		public void DeleteByWorkCenter(int pintCCNID, int pintWorkCenterID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
						
			string strSql = "DELETE " + PRO_ShiftCapacityTable.TABLE_NAME + " WHERE  " + PRO_ShiftCapacityTable.WCCAPACITYID_FLD; 
			strSql += " IN (SELECT " + PRO_WCCapacityTable.WCCAPACITYID_FLD + " FROM " + PRO_WCCapacityTable.TABLE_NAME; 
			strSql += " WHERE " + PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID;
			strSql += " AND " + PRO_WCCapacityTable.CCNID_FLD + "=" + pintCCNID + ")";
			
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
		///       This method uses to add data to PRO_ShiftCapacity
		///    </summary>
		///    <Inputs>
		///        PRO_ShiftCapacityVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
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
				+ PRO_ShiftCapacityTable.SHIFTCAPACITYID_FLD + ","
				+ PRO_ShiftCapacityTable.WCCAPACITYID_FLD + ","
				+ PRO_ShiftCapacityTable.SHIFTID_FLD
				+ " FROM " + PRO_ShiftCapacityTable.TABLE_NAME
				+" WHERE " + PRO_ShiftCapacityTable.SHIFTCAPACITYID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_ShiftCapacityVO objObject = new PRO_ShiftCapacityVO();

				while (odrPCS.Read())
				{ 
				objObject.ShiftCapacityID = int.Parse(odrPCS[PRO_ShiftCapacityTable.SHIFTCAPACITYID_FLD].ToString().Trim());
				objObject.WCCapacityID = int.Parse(odrPCS[PRO_ShiftCapacityTable.WCCAPACITYID_FLD].ToString().Trim());
				objObject.ShiftID = int.Parse(odrPCS[PRO_ShiftCapacityTable.SHIFTID_FLD].ToString().Trim());

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
		///       This method uses to add data to PRO_ShiftCapacity
		///    </summary>
		///    <Inputs>
		///        PRO_ShiftCapacityVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_ShiftCapacityVO objObject = (PRO_ShiftCapacityVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_ShiftCapacity SET "
				+ PRO_ShiftCapacityTable.WCCAPACITYID_FLD + "=   ?" + ","
				+ PRO_ShiftCapacityTable.SHIFTID_FLD + "=  ?"
				+" WHERE " + PRO_ShiftCapacityTable.SHIFTCAPACITYID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftCapacityTable.WCCAPACITYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ShiftCapacityTable.WCCAPACITYID_FLD].Value = objObject.WCCapacityID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftCapacityTable.SHIFTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ShiftCapacityTable.SHIFTID_FLD].Value = objObject.ShiftID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftCapacityTable.SHIFTCAPACITYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ShiftCapacityTable.SHIFTCAPACITYID_FLD].Value = objObject.ShiftCapacityID;


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
		///       This method uses to add data to PRO_ShiftCapacity
		///    </summary>
		///    <Inputs>
		///        PRO_ShiftCapacityVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
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
				+ PRO_ShiftCapacityTable.SHIFTCAPACITYID_FLD + ","
				+ PRO_ShiftCapacityTable.WCCAPACITYID_FLD + ","
				+ PRO_ShiftCapacityTable.SHIFTID_FLD
					+ " FROM " + PRO_ShiftCapacityTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ShiftCapacityTable.TABLE_NAME);

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
		/// GetShiftByWCCapacityID
		/// </summary>
		/// <param name="pintWCCapacityID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, January 6 2006</date>
		public DataTable GetShiftByWCCapacityID(int pintWCCapacityID)
		{
			const string METHOD_NAME = THIS + ".GetShiftByWCCapacityID()";
			DataSet dstPCS = new DataSet();
				
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ PRO_ShiftCapacityTable.SHIFTCAPACITYID_FLD + ","
					+ PRO_ShiftCapacityTable.WCCAPACITYID_FLD + ","
					+ PRO_ShiftCapacityTable.SHIFTID_FLD
					+ " FROM " + PRO_ShiftCapacityTable.TABLE_NAME
					+ " WHERE " + PRO_ShiftCapacityTable.WCCAPACITYID_FLD + " = "
					+ pintWCCapacityID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ShiftCapacityTable.TABLE_NAME);

				return dstPCS.Tables[0];
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

		public DataSet ListByWorkCenter(int pintCCNID, int pintWorkCenterId)
		{
			const string METHOD_NAME = THIS + ".ListByWorkCenter()";
			DataSet dstPCS = new DataSet();
				
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ PRO_ShiftCapacityTable.SHIFTCAPACITYID_FLD + ","
					+ PRO_ShiftCapacityTable.WCCAPACITYID_FLD + ","
					+ PRO_ShiftCapacityTable.SHIFTID_FLD
					+ " FROM " + PRO_ShiftCapacityTable.TABLE_NAME
				    + " WHERE " + PRO_ShiftCapacityTable.WCCAPACITYID_FLD + " IN (SELECT " + PRO_WCCapacityTable.WCCAPACITYID_FLD
					+ "	FROM " + PRO_WCCapacityTable.TABLE_NAME
					+ "	WHERE " + PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterId 
					+ " AND " + PRO_WCCapacityTable.CCNID_FLD + "=" + pintCCNID + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_ShiftCapacityTable.TABLE_NAME);

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
		///       This method uses to add data to PRO_ShiftCapacity
		///    </summary>
		///    <Inputs>
		///        PRO_ShiftCapacityVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
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
				+ PRO_ShiftCapacityTable.SHIFTCAPACITYID_FLD + ","
				+ PRO_ShiftCapacityTable.WCCAPACITYID_FLD + ","
				+ PRO_ShiftCapacityTable.SHIFTID_FLD 
				+ "  FROM " + PRO_ShiftCapacityTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,PRO_ShiftCapacityTable.TABLE_NAME);

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
