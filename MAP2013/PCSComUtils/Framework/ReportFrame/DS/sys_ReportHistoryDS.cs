using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;
using System.Collections.Specialized;

namespace PCSComUtils.Framework.ReportFrame.DS
{
	public class sys_ReportHistoryDS 
	{
		public sys_ReportHistoryDS()
		{
		}

		private const string THIS = "PCSComUtils.Framework.ReportFrame.DS.DS.sys_ReportHistoryDS";


		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_ReportHistory
		///    </Description>
		///    <Inputs>
		///        sys_ReportHistoryVO       
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
		///       Monday, December 27, 2004
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
				sys_ReportHistoryVO objObject = (sys_ReportHistoryVO) pobjObjectVO;
				string strSql = String.Empty;

				strSql = "INSERT INTO " + sys_ReportHistoryTable.TABLE_NAME + "("
					+ sys_ReportHistoryTable.USERID_FLD + ","
					+ sys_ReportHistoryTable.EXECDATETIME_FLD + ","
					+ sys_ReportHistoryTable.REPORTID_FLD + ","
					+ sys_ReportHistoryTable.TABLENAME_FLD + ")"
					+ " VALUES(?,?,?,?)";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryTable.USERID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryTable.USERID_FLD].Value = objObject.UserID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryTable.EXECDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[sys_ReportHistoryTable.EXECDATETIME_FLD].Value = objObject.ExecDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryTable.TABLENAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryTable.TABLENAME_FLD].Value = objObject.TableName;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
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


		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from sys_ReportHistory
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
			/// UNDONE: Thachnn says: I think this function is useless
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + sys_ReportHistoryTable.TABLE_NAME + " WHERE  " 
				+ sys_ReportHistoryTable.HISTORYID_FLD + "=" + pintID.ToString().Trim();
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
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to delete data from sys_ReportHistory
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       03-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void DeleteByReportID(string pstrID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + sys_ReportHistoryTable.TABLE_NAME + " WHERE  " 
				+ sys_ReportHistoryTable.REPORTID_FLD + "= ? ";// + pstrID + "'";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryTable.REPORTID_FLD].Value = pstrID;
				ocmdPCS.Connection.Open();

				ocmdPCS.ExecuteNonQuery();
				ocmdPCS = null;

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to get data from sys_ReportHistory
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_ReportHistoryVO
		///    </Outputs>
		///    <Returns>
		///       sys_ReportHistoryVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Monday, December 27, 2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ sys_ReportHistoryTable.HISTORYID_FLD + ","
					+ sys_ReportHistoryTable.USERID_FLD + ","
					+ sys_ReportHistoryTable.EXECDATETIME_FLD + ","
					+ sys_ReportHistoryTable.REPORTID_FLD + ","
					+ sys_ReportHistoryTable.TABLENAME_FLD
					+ " FROM " + sys_ReportHistoryTable.TABLE_NAME
					+ " WHERE " + sys_ReportHistoryTable.HISTORYID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_ReportHistoryVO objObject = new sys_ReportHistoryVO();

				while (odrPCS.Read())
				{
					objObject.HistoryID = int.Parse(odrPCS[sys_ReportHistoryTable.HISTORYID_FLD].ToString().Trim());
					objObject.UserID = odrPCS[sys_ReportHistoryTable.USERID_FLD].ToString().Trim();
					objObject.ExecDateTime = DateTime.Parse(odrPCS[sys_ReportHistoryTable.EXECDATETIME_FLD].ToString().Trim());
					objObject.ReportID = odrPCS[sys_ReportHistoryTable.REPORTID_FLD].ToString().Trim();
					objObject.TableName = odrPCS[sys_ReportHistoryTable.TABLENAME_FLD].ToString().Trim();

				}
				return objObject;
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


		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to sys_ReportHistory
		///    </Description>
		///    <Inputs>
		///       sys_ReportHistoryVO       
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

			sys_ReportHistoryVO objObject = (sys_ReportHistoryVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE sys_ReportHistory SET "
					+ sys_ReportHistoryTable.USERID_FLD + "=   ?" + ","
					+ sys_ReportHistoryTable.EXECDATETIME_FLD + "=   ?" + ","
					+ sys_ReportHistoryTable.REPORTID_FLD + "=   ?" + ","
					+ sys_ReportHistoryTable.TABLENAME_FLD + "=  ?"
					+ " WHERE " + sys_ReportHistoryTable.HISTORYID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryTable.USERID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryTable.USERID_FLD].Value = objObject.UserID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryTable.EXECDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[sys_ReportHistoryTable.EXECDATETIME_FLD].Value = objObject.ExecDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryTable.TABLENAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryTable.TABLENAME_FLD].Value = objObject.TableName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryTable.HISTORYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportHistoryTable.HISTORYID_FLD].Value = objObject.HistoryID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
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


		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from sys_ReportHistory
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
		///       Monday, December 27, 2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet List()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ sys_ReportHistoryTable.HISTORYID_FLD + ","
					+ sys_ReportHistoryTable.USERID_FLD + ","
					+ sys_ReportHistoryTable.EXECDATETIME_FLD + ","
					+ sys_ReportHistoryTable.REPORTID_FLD + ","
					+ sys_ReportHistoryTable.TABLENAME_FLD
					+ " FROM " + sys_ReportHistoryTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportHistoryTable.TABLE_NAME);

				return dstPCS;
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
		///       Monday, December 27, 2004
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
				strSql = "SELECT "
					+ sys_ReportHistoryTable.HISTORYID_FLD + ","
					+ sys_ReportHistoryTable.USERID_FLD + ","
					+ sys_ReportHistoryTable.EXECDATETIME_FLD + ","
					+ sys_ReportHistoryTable.REPORTID_FLD + ","
					+ sys_ReportHistoryTable.TABLENAME_FLD
					+ " FROM " + sys_ReportHistoryTable.TABLE_NAME;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, sys_ReportHistoryTable.TABLE_NAME);

			}
			catch (OleDbException ex)
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
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
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

		
		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from sys_ReportHistory of specified report
		///    </Description>
		///    <Inputs>
		///       ReportID
		///    </Inputs>
		///    <Outputs>
		///       ArrayList
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       03-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList ListByReport(string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".ListByReport()";
			ArrayList arrObjects = new ArrayList();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataReader odrPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ sys_ReportHistoryTable.HISTORYID_FLD + ","
					+ sys_ReportHistoryTable.USERID_FLD + ","
					+ sys_ReportHistoryTable.EXECDATETIME_FLD + ","
					+ sys_ReportHistoryTable.REPORTID_FLD + ","
					+ sys_ReportHistoryTable.TABLENAME_FLD
					+ " FROM " + sys_ReportHistoryTable.TABLE_NAME
					+ " WHERE " + sys_ReportHistoryTable.REPORTID_FLD + "= ? "; // + pstrReportID + "'";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportHistoryTable.REPORTID_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportHistoryTable.REPORTID_FLD ].Value = pstrReportID ;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();
				while(odrPCS.Read())
				{
					sys_ReportHistoryVO voReportHistory = new sys_ReportHistoryVO();
					voReportHistory.HistoryID = int.Parse(odrPCS[sys_ReportHistoryTable.HISTORYID_FLD].ToString().Trim());
					voReportHistory.ReportID = odrPCS[sys_ReportHistoryTable.REPORTID_FLD].ToString().Trim();
					voReportHistory.ExecDateTime = DateTime.Parse(odrPCS[sys_ReportHistoryTable.EXECDATETIME_FLD].ToString().Trim());
					voReportHistory.UserID = odrPCS[sys_ReportHistoryTable.USERID_FLD].ToString().Trim();
					voReportHistory.TableName = odrPCS[sys_ReportHistoryTable.TABLENAME_FLD].ToString().Trim();

					arrObjects.Add(voReportHistory);
				}
				// trim to actual size
				arrObjects.TrimToSize();
				// return
				return arrObjects;
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from sys_ReportHistoryPara
		///       but keep 10 last report
		///    </Description>
		///    <Inputs>
		///        HistoryID
		///    </Inputs>
		///    <Outputs>
		///       void
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       09-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void DeleteButKeep10Last(int pintMaxID, string pstrUserName)
		{
			const string METHOD_NAME = THIS + ".DeleteButKeep10Last()";
			string strSql = String.Empty;
			strSql = "DELETE " + sys_ReportHistoryTable.TABLE_NAME 
				+ " WHERE  " + sys_ReportHistoryTable.HISTORYID_FLD + "<=" 
				+ (pintMaxID - 9).ToString().Trim()
				+ " AND " + sys_ReportHistoryTable.USERID_FLD + "= ? "; // + pstrUserName + "'";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportHistoryTable.USERID_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportHistoryTable.USERID_FLD ].Value = pstrUserName ;
				ocmdPCS.Connection.Open();

				ocmdPCS.ExecuteNonQuery();
				ocmdPCS = null;

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to drop report history table from system
		///    </Description>
		///    <Inputs>
		///        HistoryID
		///    </Inputs>
		///    <Outputs>
		///       void
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       09-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void DropHistoryTables(int pintHistoryID)
		{
			const string METHOD_NAME = THIS + ".DropHistoryTables()";
			string strSql = String.Empty;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			strSql = "DROP TABLE " + Constants.REPORT_HISTORY_PREFIX + pintHistoryID.ToString().Trim();
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
				ocmdPCS = null;

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to drop report history table from system
		///    </Description>
		///    <Inputs>
		///        History Table Name
		///    </Inputs>
		///    <Outputs>
		///       void
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Thachnn
		///    </Authors>
		///    <History>
		///       06-Oct-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void DropHistoryTables(string pstrHistoryTableName)
		{
			const string METHOD_NAME = THIS + ".DropHistoryTables()";
			string strSql = String.Empty;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			strSql = "DROP TABLE " + pstrHistoryTableName;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
				ocmdPCS = null;
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to create report history table and insert data
		///       using SELECT INTO statement.
		///    </Description>
		///    <Inputs>
		///        Sql command
		///    </Inputs>
		///    <Outputs>
		///       DataSet hold records of new table
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       10-Jan-2005		
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet CreateHistoryTable(string[] parrCommand, int pintMaxID, out string ostrTableName)
		{
			/// TODO: Bro. DungLA, I feel bug may be appear here, but I can't resolve
			/// Please help. Thachnn
			const string METHOD_NAME = THIS + ".CreateHistoryTable()";
			const string WHERE_CLAUSE = "WHERE";
			DataSet dstResult = new DataSet();
			
			string strHistoryCommand = string.Empty;
			ostrTableName = string.Empty;

			foreach (string strElement in parrCommand)
			{
				// create sql statement to create Report History table and insert data to it
				// i.e: strHistoryCommand = "SELECT * INTO ReportHistory_1 FROM Orders".
				string strTemp = strElement;
				if (strElement.Trim().ToUpper().Equals(Constants.FROM_STR.Trim()))
				{
					ostrTableName = Constants.REPORT_HISTORY_PREFIX + (pintMaxID + 1).ToString().Trim();
					strTemp = Constants.INTO_STR + ostrTableName + Constants.WHITE_SPACE + strElement;
				}
				
				strHistoryCommand += strTemp + Constants.WHITE_SPACE;
			}
			strHistoryCommand = strHistoryCommand.Trim();
			if (strHistoryCommand.ToUpper().EndsWith(WHERE_CLAUSE))
			{
				// trim the WHERE keyword in case of no where clause
				strHistoryCommand = strHistoryCommand.ToUpper().Replace(WHERE_CLAUSE, string.Empty).Trim();
			}
			// now we got the sql statement, execute it and return value
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strHistoryCommand, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstResult, ostrTableName);
				return dstResult;
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}
			catch (Exception ex)
			{				
				throw new PCSDBException(ErrorCode.MESSAGE_CANT_CREATE_REPORT_HISTORY, METHOD_NAME, ex);
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
		///       This method uses to get max report history id
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       Max ID
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       09-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetMaxReportHistoryID()
		{
			const string METHOD_NAME = THIS + ".GetMaxReportHistoryID()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT ISNULL(MAX("
					+ sys_ReportHistoryTable.HISTORYID_FLD + "), 0)"
					+ " FROM " + sys_ReportHistoryTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString().Trim());
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

		//**************************************************************************              
		///    <Description>
		///       Get last 10 executed report from history
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       20-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList GetLast10Report(string pstrUserName)
		{
			const string METHOD_NAME = THIS + ".GetLast10Report()";
			
			ArrayList arrReports = new ArrayList();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataReader odrPCS = null;
			try 
			{
				string strSql = "SELECT TOP 10 " + sys_ReportHistoryTable.HISTORYID_FLD + ","
					+ sys_ReportHistoryTable.EXECDATETIME_FLD + ","
					+ sys_ReportHistoryTable.REPORTID_FLD + ","
					+ sys_ReportHistoryTable.TABLENAME_FLD + ","
					+ sys_ReportHistoryTable.USERID_FLD
					+ " FROM " + sys_ReportHistoryTable.TABLE_NAME
					+ " WHERE " + sys_ReportHistoryTable.USERID_FLD + "= ? " // + pstrUserName + "'"
					+ " ORDER BY " + sys_ReportHistoryTable.HISTORYID_FLD + " DESC";

				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportHistoryTable.USERID_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportHistoryTable.USERID_FLD ].Value = pstrUserName ;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();
				while(odrPCS.Read())
				{
					sys_ReportHistoryVO voReportHistory = new sys_ReportHistoryVO();
					voReportHistory.HistoryID = int.Parse(odrPCS[sys_ReportHistoryTable.HISTORYID_FLD].ToString().Trim());
					voReportHistory.ReportID = odrPCS[sys_ReportHistoryTable.REPORTID_FLD].ToString().Trim();
					voReportHistory.ExecDateTime = DateTime.Parse(odrPCS[sys_ReportHistoryTable.EXECDATETIME_FLD].ToString().Trim());
					voReportHistory.UserID = odrPCS[sys_ReportHistoryTable.USERID_FLD].ToString().Trim();
					voReportHistory.TableName = odrPCS[sys_ReportHistoryTable.TABLENAME_FLD].ToString().Trim();

					arrReports.Add(voReportHistory);
				}
				// trim to actual size
				arrReports.TrimToSize();
				// return
				return arrReports;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch(FormatException ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
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
		///       This method uses to get all data from History Table in order to show
		///       executed report
		///    </Description>
		///    <Inputs>
		///        History Table Name
		///    </Inputs>
		///    <Outputs>
		///      DataTable
		///    </Outputs>
		///    <Returns>
		///       DataTable
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       20-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetDataFromHistoryTable(string pstrHistoryTableName)
		{
			const string METHOD_NAME = THIS + ".GetDataFromHistoryTable()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT * "
					+ " FROM " + pstrHistoryTableName;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, pstrHistoryTableName);

				return dstPCS.Tables[pstrHistoryTableName];
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
		//**************************************************************************              
		///    <Description>
		///       Get last 10 executed report from history
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       20-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList GetHistoryReportByUser(string pstrUsername)
		{
			const string METHOD_NAME = THIS + ".GetHistoryReportByUser()";
			
			ArrayList arrReports = new ArrayList();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataReader odrPCS = null;
			try 
			{
				string strSql = "SELECT " + sys_ReportHistoryTable.HISTORYID_FLD + ","
					+ sys_ReportHistoryTable.EXECDATETIME_FLD + ","
					+ sys_ReportHistoryTable.REPORTID_FLD + ","
					+ sys_ReportHistoryTable.TABLENAME_FLD + ","
					+ sys_ReportHistoryTable.USERID_FLD
					+ " FROM " + sys_ReportHistoryTable.TABLE_NAME
					+ " WHERE " + sys_ReportHistoryTable.USERID_FLD + "= ? " // + pstrUsername + "'"
					+ " ORDER BY " + sys_ReportHistoryTable.HISTORYID_FLD + " DESC";

				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportHistoryTable.USERID_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportHistoryTable.USERID_FLD ].Value = pstrUsername ;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();
				while(odrPCS.Read())
				{
					sys_ReportHistoryVO voReportHistory = new sys_ReportHistoryVO();
					voReportHistory.HistoryID = int.Parse(odrPCS[sys_ReportHistoryTable.HISTORYID_FLD].ToString().Trim());
					voReportHistory.ReportID = odrPCS[sys_ReportHistoryTable.REPORTID_FLD].ToString().Trim();
					if (odrPCS[sys_ReportHistoryTable.EXECDATETIME_FLD] != DBNull.Value)
						voReportHistory.ExecDateTime = DateTime.Parse(odrPCS[sys_ReportHistoryTable.EXECDATETIME_FLD].ToString().Trim());
					voReportHistory.UserID = odrPCS[sys_ReportHistoryTable.USERID_FLD].ToString().Trim();
					voReportHistory.TableName = odrPCS[sys_ReportHistoryTable.TABLENAME_FLD].ToString().Trim();

					arrReports.Add(voReportHistory);
				}
				// trim to actual size
				arrReports.TrimToSize();
				// return
				return arrReports;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch(FormatException ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
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
		/// Thachnn: 01/Oct/2005
		/// Create a new table (with provided name) in the database from the provided System.Data.DataTable object
		/// </summary>
		/// <param name="pdtb">source data table, contain schema and data to create in the database</param>
		/// <param name="pstrDBNewTableName">new table name to create in the database</param>		
		/// <returns>true if function finish SUCCESSfully</returns>
		public bool PushDataTableIntoNewDatabaseTable(DataTable pdtb, string pstrDBNewTableName)
		{
			bool blnRet = false;
			string strConnectionString = Utils.Instance.OleDbConnectionString;
            
			#region FIRST, create the new table (in the database)
			string strSQL = GenerateCREATE_QUERY_FromDataTable(pdtb,pstrDBNewTableName);
			if(strSQL.Equals(string.Empty) || pdtb == null || pstrDBNewTableName.Equals(string.Empty) || strConnectionString.Equals(string.Empty) )
			{
				return false;
			}
			/// DO create executeNonQuery
			OleDbConnection ocon =null;
			OleDbCommand ocmd =null;			
			try
			{							
				ocon = new OleDbConnection(strConnectionString);
				ocmd = new OleDbCommand(strSQL, ocon);				
				ocmd.Connection.Open();
				ocmd.ExecuteNonQuery();

				if (ocon!=null) 
				{
					if (ocon.State != ConnectionState.Closed) 
					{
						ocon.Close();
					}
				}				
			}
			catch(Exception ex) 
			{
				//DEBUG: MessageBox.Show (ex.Message);
				if (ocon!=null) 
				{
					if (ocon.State != ConnectionState.Closed) 
					{
						ocon.Close();
					}
				}
				return false;
			}			
			
			#endregion
			
			#region SECOND, fill it value into the brand new table (in the database)
			System.Data.DataSet ods = new System.Data.DataSet();
			pdtb.TableName = pstrDBNewTableName;	// really need to reassign the table name to avoid exception
			ods.Tables.Add(pdtb);

			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;	// builder, auto make the Insert and Update query to update
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();
			try
			{
				strSql=	"SELECT  TOP 1  * FROM " + pstrDBNewTableName;				
				oconPCS = new OleDbConnection(strConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				ods.EnforceConstraints = false;
				odadPCS.Update(ods, pstrDBNewTableName);

				blnRet = true;
			}			
			catch (Exception ex)  //DEBUG: 				
			{
				//DEBUG: MessageBox.Show(ex.Message);				
				blnRet = false;
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
			#endregion	
			return blnRet;
		}


		/// <summary>
		/// Thachnn: 30/Oct/2005
		/// Generate CREATE TABLE QUERY From DataTable schema
		/// </summary>
		/// <param name="pdtb">DataTable object to get meta-data</param>
		/// <param name="pstrDBNewTableName">TableName to create in the return CREATE TABLE QUERY string</param>
		/// <returns></returns>
		public string  GenerateCREATE_QUERY_FromDataTable(DataTable pdtb, string pstrDBNewTableName)
		{
			try	// any error, pdtb is null, pstrDBNewTableName is empty, we return "string.Empty" immediatelly
			{
				#region Mapping setting
			
				NameValueCollection nvarrDBType = new NameValueCollection();
			
				nvarrDBType.Add("Int64","bigint");
				nvarrDBType.Add("Int32","int");
				nvarrDBType.Add("Int16","smallint");
				nvarrDBType.Add("Byte","tinyint");			
			
				nvarrDBType.Add("Boolean","bit");
				nvarrDBType.Add("Object","sql_variant");									
				nvarrDBType.Add("Guid","uniqueidentifier");
			
				//nvarrDBType.Add("String","char");			
				//nvarrDBType.Add("String","nchar");			
				//nvarrDBType.Add("String","ntext");			
				nvarrDBType.Add("String","nvarchar");						
				//nvarrDBType.Add("String","text");			
				//nvarrDBType.Add("String","varchar");

				nvarrDBType.Add("DateTime","datetime");
				//nvarrDBType.Add("DateTime","smalldatetime");

				//nvarrDBType.Add("Decimal","money");
				//nvarrDBType.Add("Decimal","numeric");
				//nvarrDBType.Add("Decimal","smallmoney");
				nvarrDBType.Add("Decimal","decimal");

				nvarrDBType.Add("Double","float");
				nvarrDBType.Add("Single","real");

				//nvarrDBType.Add("Byte[]","image");
				//nvarrDBType.Add("Byte[]","timestamp");			
				nvarrDBType.Add("Byte[]","varbinary");						
				//nvarrDBType.Add("Byte[]","binary");
						
				//nvarrDBType.Add("Char[]","nchar");			
				//nvarrDBType.Add("Char[]","ntext");
				//nvarrDBType.Add("Char[]","text");
				//nvarrDBType.Add("Char[]","varchar");			
				nvarrDBType.Add("Char[]","nvarchar");
				//nvarrDBType.Add("Char[]","char");
				#endregion

				#region SQL query Generate
							
				string strCreateSQL = "CREATE TABLE " + pstrDBNewTableName + " (";

				string strColName, strColType;
				foreach (DataColumn col in pdtb.Columns)
				{
					strColName = "[" + col.ColumnName + "]";				
					strColType = nvarrDBType[col.DataType.Name];
					if (strColType.Equals("nvarchar"))
					{
						strColType += " (4000)";
					}
					strCreateSQL += strColName + "  " + strColType + "  NULL ,";
				}

				strCreateSQL = strCreateSQL.Substring(0,strCreateSQL.Length - 1); 
				strCreateSQL += ")";

				#endregion
				
				return  strCreateSQL;
			}
			catch
			{
				return string.Empty;
			}
		}

	}
}