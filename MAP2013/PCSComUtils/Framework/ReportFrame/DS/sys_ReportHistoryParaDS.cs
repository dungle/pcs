using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Framework.ReportFrame.DS
{
	public class sys_ReportHistoryParaDS 
	{
		public sys_ReportHistoryParaDS()
		{
		}

		private const string THIS = "PCSComUtils.Framework.ReportFrame.DS.DS.sys_ReportHistoryParaDS";

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_ReportHistoryPara
		///    </Description>
		///    <Inputs>
		///        sys_ReportHistoryParaVO       
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
				sys_ReportHistoryParaVO objObject = (sys_ReportHistoryParaVO) pobjObjectVO;
				string strSql = String.Empty;

				strSql = "INSERT INTO " + sys_ReportHistoryParaTable.TABLE_NAME + "("
					+ sys_ReportHistoryParaTable.HISTORYID_FLD + ","
					+ sys_ReportHistoryParaTable.PARANAME_FLD + ","
					+ sys_ReportHistoryParaTable.PARAVALUE_FLD + ","
					+ sys_ReportHistoryParaTable.TAGVALUE_FLD + ","
					+ sys_ReportHistoryParaTable.FILTERFIELD1VALUE_FLD + ","
					+ sys_ReportHistoryParaTable.FILTERFIELD2VALUE_FLD + ")"
					+ " VALUES(?,?,?,?,?,?)";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryParaTable.HISTORYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportHistoryParaTable.HISTORYID_FLD].Value = objObject.HistoryID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryParaTable.PARANAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryParaTable.PARANAME_FLD].Value = objObject.ParaName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryParaTable.PARAVALUE_FLD, OleDbType.VarWChar));
				if (objObject.ParaValue != string.Empty)
					ocmdPCS.Parameters[sys_ReportHistoryParaTable.PARAVALUE_FLD].Value = objObject.ParaValue;
				else
					ocmdPCS.Parameters[sys_ReportHistoryParaTable.PARAVALUE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryParaTable.TAGVALUE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryParaTable.TAGVALUE_FLD].Value = objObject.TagValue;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryParaTable.FILTERFIELD1VALUE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryParaTable.FILTERFIELD1VALUE_FLD].Value = objObject.FilterField1Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryParaTable.FILTERFIELD2VALUE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryParaTable.FILTERFIELD2VALUE_FLD].Value = objObject.FilterField2Value;


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
		///       This method uses to delete data from sys_ReportHistoryPara
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
			strSql = "DELETE " + sys_ReportHistoryParaTable.TABLE_NAME 
				+ " WHERE  " + sys_ReportHistoryParaTable.HISTORYID_FLD + "=" + pintID.ToString().Trim();
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
		///       This method uses to get data from sys_ReportHistoryPara
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_ReportHistoryParaVO
		///    </Outputs>
		///    <Returns>
		///       sys_ReportHistoryParaVO
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
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ sys_ReportHistoryParaTable.HISTORYID_FLD + ","
					+ sys_ReportHistoryParaTable.PARANAME_FLD + ","
					+ sys_ReportHistoryParaTable.PARAVALUE_FLD + ","
					+ sys_ReportHistoryParaTable.TAGVALUE_FLD + ","
					+ sys_ReportHistoryParaTable.FILTERFIELD1VALUE_FLD + ","
					+ sys_ReportHistoryParaTable.FILTERFIELD2VALUE_FLD
					+ " FROM " + sys_ReportHistoryParaTable.TABLE_NAME
					+ " WHERE " + sys_ReportHistoryParaTable.HISTORYID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_ReportHistoryParaVO objObject = new sys_ReportHistoryParaVO();

				while (odrPCS.Read())
				{
					objObject.HistoryID = int.Parse(odrPCS[sys_ReportHistoryParaTable.HISTORYID_FLD].ToString().Trim());
					objObject.ParaName = odrPCS[sys_ReportHistoryParaTable.PARANAME_FLD].ToString().Trim();
					objObject.ParaValue = odrPCS[sys_ReportHistoryParaTable.PARAVALUE_FLD].ToString().Trim();
					objObject.TagValue = odrPCS[sys_ReportHistoryParaTable.TAGVALUE_FLD].ToString().Trim();
					objObject.FilterField1Value = odrPCS[sys_ReportHistoryParaTable.FILTERFIELD1VALUE_FLD].ToString().Trim();
					objObject.FilterField2Value = odrPCS[sys_ReportHistoryParaTable.FILTERFIELD2VALUE_FLD].ToString().Trim();

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
		///       This method uses to update data to sys_ReportHistoryPara
		///    </Description>
		///    <Inputs>
		///       sys_ReportHistoryParaVO       
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

			sys_ReportHistoryParaVO objObject = (sys_ReportHistoryParaVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE " + sys_ReportHistoryParaTable.TABLE_NAME + " SET "
					+ sys_ReportHistoryParaTable.PARANAME_FLD + "=   ?" + ","
					+ sys_ReportHistoryParaTable.PARAVALUE_FLD + "=   ?" + ","
					+ sys_ReportHistoryParaTable.TAGVALUE_FLD + "=   ?" + ","
					+ sys_ReportHistoryParaTable.FILTERFIELD1VALUE_FLD + "=   ?" + ","
					+ sys_ReportHistoryParaTable.FILTERFIELD2VALUE_FLD + "=  ?"
					+ " WHERE " + sys_ReportHistoryParaTable.HISTORYID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryParaTable.PARANAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryParaTable.PARANAME_FLD].Value = objObject.ParaName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryParaTable.PARAVALUE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryParaTable.PARAVALUE_FLD].Value = objObject.ParaValue;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryParaTable.TAGVALUE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryParaTable.TAGVALUE_FLD].Value = objObject.TagValue;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryParaTable.FILTERFIELD1VALUE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryParaTable.FILTERFIELD1VALUE_FLD].Value = objObject.FilterField1Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryParaTable.FILTERFIELD2VALUE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportHistoryParaTable.FILTERFIELD2VALUE_FLD].Value = objObject.FilterField2Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportHistoryParaTable.HISTORYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportHistoryParaTable.HISTORYID_FLD].Value = objObject.HistoryID;


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
		///       This method uses to get all data from sys_ReportHistoryPara
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
					+ sys_ReportHistoryParaTable.HISTORYID_FLD + ","
					+ sys_ReportHistoryParaTable.PARANAME_FLD + ","
					+ sys_ReportHistoryParaTable.PARAVALUE_FLD + ","
					+ sys_ReportHistoryParaTable.TAGVALUE_FLD + ","
					+ sys_ReportHistoryParaTable.FILTERFIELD1VALUE_FLD + ","
					+ sys_ReportHistoryParaTable.FILTERFIELD2VALUE_FLD
					+ " FROM " + sys_ReportHistoryParaTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportHistoryParaTable.TABLE_NAME);

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
		///       This method uses to get all data from sys_ReportHistoryPara of specified History
		///    </Description>
		///    <Inputs>
		///       HistoryID
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
		///       21-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList ListByHistory(string pstrHistoryID)
		{
			const string METHOD_NAME = THIS + ".ListByHistory()";
			ArrayList arrObjects = new ArrayList();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataReader odrPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ sys_ReportHistoryParaTable.HISTORYID_FLD + ","
					+ sys_ReportHistoryParaTable.PARANAME_FLD + ","
					+ sys_ReportHistoryParaTable.PARAVALUE_FLD + ","
					+ sys_ReportHistoryParaTable.TAGVALUE_FLD + ","
					+ sys_ReportHistoryParaTable.FILTERFIELD1VALUE_FLD + ","
					+ sys_ReportHistoryParaTable.FILTERFIELD2VALUE_FLD
					+ " FROM " + sys_ReportHistoryParaTable.TABLE_NAME
					+ " WHERE " + sys_ReportHistoryParaTable.HISTORYID_FLD + "= ? ";// + pstrHistoryID;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportHistoryParaTable.HISTORYID_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportHistoryParaTable.HISTORYID_FLD ].Value = pstrHistoryID ;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();
				while (odrPCS.Read())
				{
					sys_ReportHistoryParaVO voHistoryPara = new sys_ReportHistoryParaVO();
					voHistoryPara.HistoryID = int.Parse(odrPCS[sys_ReportHistoryParaTable.HISTORYID_FLD].ToString().Trim());
					voHistoryPara.ParaName = odrPCS[sys_ReportHistoryParaTable.PARANAME_FLD].ToString().Trim();
					voHistoryPara.ParaValue = odrPCS[sys_ReportHistoryParaTable.PARAVALUE_FLD].ToString().Trim();
					voHistoryPara.TagValue = odrPCS[sys_ReportHistoryParaTable.TAGVALUE_FLD].ToString().Trim();
					voHistoryPara.FilterField1Value = odrPCS[sys_ReportHistoryParaTable.FILTERFIELD1VALUE_FLD].ToString().Trim();
					voHistoryPara.FilterField2Value = odrPCS[sys_ReportHistoryParaTable.FILTERFIELD2VALUE_FLD].ToString().Trim();

					arrObjects.Add(voHistoryPara);
				}
				arrObjects.TrimToSize();
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
					+ sys_ReportHistoryParaTable.HISTORYID_FLD + ","
					+ sys_ReportHistoryParaTable.PARANAME_FLD + ","
					+ sys_ReportHistoryParaTable.PARAVALUE_FLD + ","
					+ sys_ReportHistoryParaTable.TAGVALUE_FLD + ","
					+ sys_ReportHistoryParaTable.FILTERFIELD1VALUE_FLD + ","
					+ sys_ReportHistoryParaTable.FILTERFIELD2VALUE_FLD
					+ " FROM " + sys_ReportHistoryParaTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, sys_ReportHistoryParaTable.TABLE_NAME);

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
			strSql = "DELETE " + sys_ReportHistoryParaTable.TABLE_NAME 
				+ " WHERE  " + sys_ReportHistoryParaTable.HISTORYID_FLD + "<=" 
				+ (pintMaxID - 9).ToString().Trim()
				+ " AND " + sys_ReportHistoryParaTable.HISTORYID_FLD + " IN ("
				+ " SELECT " + sys_ReportHistoryTable.TABLE_NAME + "." + sys_ReportHistoryTable.HISTORYID_FLD
				+ " FROM " + sys_ReportHistoryTable.TABLE_NAME
				+ " WHERE " + sys_ReportHistoryTable.TABLE_NAME + "." + sys_ReportHistoryTable.USERID_FLD + "= ? )"; //"=  '" + pstrUserName + "')";
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
	}
}