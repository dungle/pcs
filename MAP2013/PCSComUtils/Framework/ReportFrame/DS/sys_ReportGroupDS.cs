using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Framework.ReportFrame.DS
{
	public class sys_ReportGroupDS 
	{
		public sys_ReportGroupDS()
		{
		}

		private const string THIS = "PCSComUtils.Framework.ReportFrame.DS.DS.sys_ReportGroupDS";

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_ReportGroup
		///    </Description>
		///    <Inputs>
		///        sys_ReportGroupVO       
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
				sys_ReportGroupVO objObject = (sys_ReportGroupVO) pobjObjectVO;
				string strSql = String.Empty;

				strSql = "INSERT INTO " + sys_ReportGroupTable.TABLE_NAME + "("
					+ sys_ReportGroupTable.GROUPID_FLD + ","
					+ sys_ReportGroupTable.GROUPNAME_FLD + ","
					+ sys_ReportGroupTable.GROUPORDER_FLD + ")"
					+ " VALUES(?,?,?)";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportGroupTable.GROUPID_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportGroupTable.GROUPID_FLD].Value = objObject.GroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportGroupTable.GROUPNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportGroupTable.GROUPNAME_FLD].Value = objObject.GroupName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportGroupTable.GROUPORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportGroupTable.GROUPORDER_FLD].Value = objObject.GroupOrder;

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
				else if (ex.Errors[1].NativeError == ErrorCode.SQL_VIOLATION_CONSTRAINT)
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
		///       This method uses to delete data from sys_ReportGroup
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
			/// UNDONE: Thachnn says: I feel this function is useless.
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + sys_ReportGroupTable.TABLE_NAME + " WHERE  " + sys_ReportGroupTable.GROUPID_FLD + "=" + pintID.ToString().Trim();
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
		///       This method uses to delete data from sys_ReportGroup
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
		///       31-Dec-2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Delete(string pstrID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + sys_ReportGroupTable.TABLE_NAME + " WHERE  " 
				+ sys_ReportGroupTable.GROUPID_FLD + "= ? "; // + pstrID + "'";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportGroupTable.GROUPID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportGroupTable.GROUPID_FLD].Value = pstrID;
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
		///       This method uses to get data from sys_ReportGroup
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_ReportGroupVO
		///    </Outputs>
		///    <Returns>
		///       sys_ReportGroupVO
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
			/// UNDONE: Thachnn says: I feel this function is useless.
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ sys_ReportGroupTable.GROUPID_FLD + ","
					+ sys_ReportGroupTable.GROUPNAME_FLD + ","
					+ sys_ReportGroupTable.GROUPORDER_FLD
					+ " FROM " + sys_ReportGroupTable.TABLE_NAME
					+ " WHERE " + sys_ReportGroupTable.GROUPID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_ReportGroupVO objObject = new sys_ReportGroupVO();

				while (odrPCS.Read())
				{
					objObject.GroupID = odrPCS[sys_ReportGroupTable.GROUPID_FLD].ToString().Trim();
					objObject.GroupName = odrPCS[sys_ReportGroupTable.GROUPNAME_FLD].ToString().Trim();
					objObject.GroupOrder = int.Parse(odrPCS[sys_ReportGroupTable.GROUPORDER_FLD].ToString().Trim());

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
		///       This method uses to get data from sys_ReportGroup
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_ReportGroupVO
		///    </Outputs>
		///    <Returns>
		///       sys_ReportGroupVO
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       31-Dec-2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(string pstrID)
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
					+ sys_ReportGroupTable.GROUPID_FLD + ","
					+ sys_ReportGroupTable.GROUPNAME_FLD + ","
					+ sys_ReportGroupTable.GROUPORDER_FLD
					+ " FROM " + sys_ReportGroupTable.TABLE_NAME
					+ " WHERE " + sys_ReportGroupTable.GROUPID_FLD + "= ? "; // + pstrID + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportGroupTable.GROUPID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportGroupTable.GROUPID_FLD].Value = pstrID;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();

				sys_ReportGroupVO objObject = new sys_ReportGroupVO();

				while (odrPCS.Read())
				{
					objObject.GroupID = odrPCS[sys_ReportGroupTable.GROUPID_FLD].ToString().Trim();
					objObject.GroupName = odrPCS[sys_ReportGroupTable.GROUPNAME_FLD].ToString().Trim();
					objObject.GroupOrder = int.Parse(odrPCS[sys_ReportGroupTable.GROUPORDER_FLD].ToString().Trim());

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
		///       This method uses to update data to sys_ReportGroup
		///    </Description>
		///    <Inputs>
		///       sys_ReportGroupVO       
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

			sys_ReportGroupVO objObject = (sys_ReportGroupVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "UPDATE " + sys_ReportGroupTable.TABLE_NAME + " SET "
					+ sys_ReportGroupTable.GROUPNAME_FLD + "=   ?" + ","
					+ sys_ReportGroupTable.GROUPORDER_FLD + "=  ?"
					+ " WHERE " + sys_ReportGroupTable.GROUPID_FLD + "= ?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportGroupTable.GROUPNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportGroupTable.GROUPNAME_FLD].Value = objObject.GroupName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportGroupTable.GROUPORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportGroupTable.GROUPORDER_FLD].Value = objObject.GroupOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportGroupTable.GROUPID_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportGroupTable.GROUPID_FLD].Value = objObject.GroupID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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
		///       This method uses to update data to sys_ReportGroup
		///    </Description>
		///    <Inputs>
		///       sys_ReportGroupVO       
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
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Update(object pobjObjecVO, string pstrOldGroupID)
		{
			const string METHOD_NAME = THIS + ".Update()";

			sys_ReportGroupVO objObject = (sys_ReportGroupVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "UPDATE " + sys_ReportGroupTable.TABLE_NAME + " SET "
					+ sys_ReportGroupTable.GROUPNAME_FLD + "=   ?" + ","
					+ sys_ReportGroupTable.GROUPORDER_FLD + "=  ?" + ","
					+ sys_ReportGroupTable.GROUPID_FLD + "= ? "
					+ " WHERE " + sys_ReportGroupTable.GROUPID_FLD + "= ?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportGroupTable.GROUPNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportGroupTable.GROUPNAME_FLD].Value = objObject.GroupName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportGroupTable.GROUPORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportGroupTable.GROUPORDER_FLD].Value = objObject.GroupOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportGroupTable.GROUPID_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportGroupTable.GROUPID_FLD].Value = objObject.GroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.REPORTANDGROUPID_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.REPORTANDGROUPID_FLD].Value = pstrOldGroupID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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
		///       This method uses to get all data from sys_ReportGroup
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
					+ sys_ReportGroupTable.GROUPID_FLD + ","
					+ sys_ReportGroupTable.GROUPNAME_FLD + ","
					+ sys_ReportGroupTable.GROUPORDER_FLD
					+ " FROM " + sys_ReportGroupTable.TABLE_NAME
					+ " ORDER BY " + sys_ReportGroupTable.GROUPORDER_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportGroupTable.TABLE_NAME);

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
					+ sys_ReportGroupTable.GROUPID_FLD + ","
					+ sys_ReportGroupTable.GROUPNAME_FLD + ","
					+ sys_ReportGroupTable.GROUPORDER_FLD
					+ " FROM " + sys_ReportGroupTable.TABLE_NAME;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, sys_ReportGroupTable.TABLE_NAME);

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
		///       This method uses get the max Order of sys_ReportGroup table
		///    </Description>
		///    <Inputs>
		///        N/A      
		///    </Inputs>
		///    <Outputs>
		///      the order of group
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       30-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetMaxGroupOrder()
		{
			const string METHOD_NAME = THIS + ".GetMaxGroupOrder()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT ISNULL (Max("
					+ sys_ReportGroupTable.GROUPORDER_FLD
					+ "),0) FROM "
					+ sys_ReportGroupTable.TABLE_NAME;

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
			catch (FormatException ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		///       This method uses to get rows in sys_ReportGroup
		///    </Description>
		///    <Inputs>
		///        N/A      
		///    </Inputs>
		///    <Outputs>
		///      row
		///    </Outputs>
		///    <Returns>
		///       array
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       30-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList GetObjectVOs()
		{
			ArrayList arrObjects = new ArrayList();
			const string METHOD_NAME = THIS + ".GetObjectVOs()";

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT  "
					+ sys_ReportGroupTable.GROUPID_FLD
					+ "," + sys_ReportGroupTable.GROUPNAME_FLD
					+ "," + sys_ReportGroupTable.GROUPORDER_FLD
					+ " FROM " + sys_ReportGroupTable.TABLE_NAME
					+ " ORDER BY " + sys_ReportGroupTable.GROUPORDER_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				while (odrPCS.Read())
				{
					sys_ReportGroupVO objGroupVO = new sys_ReportGroupVO();
					objGroupVO.GroupID = odrPCS[sys_ReportGroupTable.GROUPID_FLD].ToString().Trim();
					objGroupVO.GroupName = odrPCS[sys_ReportGroupTable.GROUPNAME_FLD].ToString().Trim();
					objGroupVO.GroupOrder = int.Parse(odrPCS[sys_ReportGroupTable.GROUPORDER_FLD].ToString().Trim());

					arrObjects.Add(objGroupVO);
				}
				// trim to actually size
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
	}
}