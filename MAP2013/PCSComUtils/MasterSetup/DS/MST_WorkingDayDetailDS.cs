using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_WorkingDayDetailDS 
	{
		public MST_WorkingDayDetailDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_WorkingDayDetailDS";

	
		///    <summary>
		///       This method uses to add data to MST_WorkingDayDetail
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
		///    </History>


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				MST_WorkingDayDetailVO objObject = (MST_WorkingDayDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_WorkingDayDetail("
					+ MST_WorkingDayDetailTable.OFFDAY_FLD + ","
					+ MST_WorkingDayDetailTable.COMMENT_FLD + ","
					+ MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD + ")"
					+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayDetailTable.OFFDAY_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_WorkingDayDetailTable.OFFDAY_FLD].Value = objObject.OffDay;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayDetailTable.COMMENT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_WorkingDayDetailTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD].Value = objObject.WorkingDayMasterID;


				
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
		///       This method uses to add data to MST_WorkingDayDetail
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + MST_WorkingDayDetailTable.TABLE_NAME + " WHERE  " + "WorkingDayDetailID" + "=" + pintID.ToString();
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
		///       This method uses to add data to MST_WorkingDayDetail
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
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
					+ MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD + ","
					+ MST_WorkingDayDetailTable.OFFDAY_FLD + ","
					+ MST_WorkingDayDetailTable.COMMENT_FLD + ","
					+ MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD
					+ " FROM " + MST_WorkingDayDetailTable.TABLE_NAME
					+" WHERE " + MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_WorkingDayDetailVO objObject = new MST_WorkingDayDetailVO();

				while (odrPCS.Read())
				{ 
					objObject.WorkingDayDetailID = int.Parse(odrPCS[MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD].ToString().Trim());
					objObject.OffDay = DateTime.Parse(odrPCS[MST_WorkingDayDetailTable.OFFDAY_FLD].ToString().Trim());
					objObject.Comment = odrPCS[MST_WorkingDayDetailTable.COMMENT_FLD].ToString().Trim();
					objObject.WorkingDayMasterID = int.Parse(odrPCS[MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD].ToString().Trim());

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
		///       This method uses to add data to MST_WorkingDayDetail
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			MST_WorkingDayDetailVO objObject = (MST_WorkingDayDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_WorkingDayDetail SET "
					+ MST_WorkingDayDetailTable.OFFDAY_FLD + "=   ?" + ","
					+ MST_WorkingDayDetailTable.COMMENT_FLD + "=   ?" + ","
					+ MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD + "=  ?"
					+" WHERE " + MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayDetailTable.OFFDAY_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_WorkingDayDetailTable.OFFDAY_FLD].Value = objObject.OffDay;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayDetailTable.COMMENT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_WorkingDayDetailTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD].Value = objObject.WorkingDayMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD].Value = objObject.WorkingDayDetailID;


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
		///       This method uses to add data to MST_WorkingDayDetail
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
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
					+ MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD + ","
					+ MST_WorkingDayDetailTable.OFFDAY_FLD + ","
					+ MST_WorkingDayDetailTable.COMMENT_FLD + ","
					+ MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD
					+ " FROM " + MST_WorkingDayDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_WorkingDayDetailTable.TABLE_NAME);

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
		///       This method uses to add data to MST_WorkingDayDetail
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
		///    </History>
		public DataSet GetDetailByMasterID(int pintWDMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDetailByMasterID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
			
				strSql=	"SELECT "
					+ MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD + ","
					+ MST_WorkingDayDetailTable.OFFDAY_FLD + ","
					+ "'' as Day,"
					+ MST_WorkingDayDetailTable.COMMENT_FLD + ","
					+ MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD
					+ " FROM " + MST_WorkingDayDetailTable.TABLE_NAME
					+ " WHERE " + MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD + "=" + pintWDMasterID 
					+ " ORDER BY " + MST_WorkingDayDetailTable.OFFDAY_FLD + " ASC";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
			
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_WorkingDayDetailTable.TABLE_NAME);

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
		///       This method uses to add data to MST_WorkingDayDetail
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
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
					+ MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD + ","
					+ MST_WorkingDayDetailTable.OFFDAY_FLD + ","
					+ MST_WorkingDayDetailTable.COMMENT_FLD + ","
					+ MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD 
					+ "  FROM " + MST_WorkingDayDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,MST_WorkingDayDetailTable.TABLE_NAME);

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
		/// List all MST_WorkingDayDetailVO object by WorkingDayMaster object
		/// </summary>
		public ArrayList List(int pintWorkingDayMasterID)
		{
			return null;
		}
		public DataSet getCollectionOffDates(int pintYear)
		{
			return null;
		}
	}
}
