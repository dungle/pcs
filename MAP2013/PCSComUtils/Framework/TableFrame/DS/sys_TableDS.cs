using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Framework.TableFrame.DS
{
	public class sys_TableDS 
	{
		public sys_TableDS()
		{
		}

		private const string THIS = "PCSComUtils.Framework.TableFrame.DS.DS.sys_TableDS";


		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_Table
		///    </Description>
		///    <Inputs>
		///        sys_TableVO       
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
				sys_TableVO objObject = (sys_TableVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO " + sys_TableTable.TABLE_NAME + " ("
					+ sys_TableTable.CODE_FLD + ","
					+ sys_TableTable.TABLENAME_FLD + ","
					+ sys_TableTable.TABLEORVIEW_FLD + ","
					+ sys_TableTable.HEIGHT_FLD + ")"
					+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.CODE_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.TABLENAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableTable.TABLENAME_FLD].Value = objObject.TableName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.TABLEORVIEW_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableTable.TABLEORVIEW_FLD].Value = objObject.TableOrView;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.HEIGHT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableTable.HEIGHT_FLD].Value = objObject.Height;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
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
		///       This method uses to add a new Row into the sys_Table
		///       and then a new row with newest TableId into the sys_TableAndGroup
		///    </Description>
		///    <Inputs>
		///        sys_TableVO       
		///        GroupID
		///    </Inputs>
		///    <Outputs>
		///       a new row is inserted into the sys_Table
		///       and a new row is inserted into the sys_TableAndGroup
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
		public void AddTable(object pobjObjectVO, int pintGroupID)
		{
			const string METHOD_NAME = THIS + ".AddTable()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				//Get the highest table order
				int intTableOrder = MaxTableOrder(pintGroupID) + 1;

				sys_TableVO objObject = (sys_TableVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO " + sys_TableTable.TABLE_NAME + " ("
					+ sys_TableTable.CODE_FLD + ","
					+ sys_TableTable.TABLENAME_FLD + ","
					+ sys_TableTable.TABLEORVIEW_FLD + ","
					+ sys_TableTable.HEIGHT_FLD + ")"
					+ "VALUES(?,?,?,?)";

				//; Insert into THIEN_TEST values (@@IDENTITY,'aa','aa1')
				strSql += " ; INSERT INTO " + sys_TableAndGroupTable.TABLE_NAME + " ("
					+ sys_TableAndGroupTable.TABLEGROUPID_FLD + ","
					+ sys_TableAndGroupTable.TABLEID_FLD + ","
					+ sys_TableAndGroupTable.TABLEORDER_FLD + ")"
					+ "VALUES(" + pintGroupID + " ,@@IDENTITY," + intTableOrder + ")";


				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.CODE_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.TABLENAME_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableTable.TABLENAME_FLD].Value = objObject.TableName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.TABLEORVIEW_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableTable.TABLEORVIEW_FLD].Value = objObject.TableOrView;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.HEIGHT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableTable.HEIGHT_FLD].Value = objObject.Height;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
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
		///       This method uses to add a new Row into the sys_Table
		///       and then a new row with newest TableId into the sys_TableAndGroup
		///    </Description>
		///    <Inputs>
		///        sys_TableVO       
		///        GroupID
		///    </Inputs>
		///    <Outputs>
		///       a new row is inserted into the sys_Table
		///       and a new row is inserted into the sys_TableAndGroup
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
		public int AddTableAndReturnMaxID(object pobjObjectVO, int pintGroupID)
		{
			const string METHOD_NAME = THIS + ".AddTableAndReturnMaxID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				//Get the highest table order
				int intTableOrder = MaxTableOrder(pintGroupID) + 1;

				sys_TableVO objObject = (sys_TableVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO  " + sys_TableTable.TABLE_NAME + " ("
					+ sys_TableTable.CODE_FLD + ","
					+ sys_TableTable.TABLENAME_FLD + ","
					+ sys_TableTable.TABLEORVIEW_FLD + ","
					+ sys_TableTable.ISVIEWONLY_FLD + ","
					+ sys_TableTable.HEIGHT_FLD + ")"
					+ "VALUES(?,?,?,?,?)";

				//This query will be used to get the latest identity value 
				strSql += "; SELECT @@IDENTITY as MaxID";

				//; Insert into sys_TableAndGroup 
				strSql += " ; INSERT INTO " + sys_TableAndGroupTable.TABLE_NAME + " ("
					+ sys_TableAndGroupTable.TABLEGROUPID_FLD + ","
					+ sys_TableAndGroupTable.TABLEID_FLD + ","
					+ sys_TableAndGroupTable.TABLEORDER_FLD + ")"
					+ "VALUES(" + pintGroupID + " ,@@IDENTITY," + intTableOrder + ")";


				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.CODE_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.TABLENAME_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableTable.TABLENAME_FLD].Value = objObject.TableName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.TABLEORVIEW_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableTable.TABLEORVIEW_FLD].Value = objObject.TableOrView;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.ISVIEWONLY_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_TableTable.ISVIEWONLY_FLD].Value = objObject.IsViewOnly;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.HEIGHT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableTable.HEIGHT_FLD].Value = objObject.Height;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				//ocmdPCS.ExecuteNonQuery();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
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
		///       This method uses to delete data from sys_Table
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
		///      29-Dec-2004
		///    </History>
		///    <Notes>
		///		Modified by NgocHT
		///    </Notes>
		//**************************************************************************
		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = string.Empty;
			strSql = "DELETE " + sys_TableTable.TABLE_NAME +
				" WHERE  " + "TableID" + "=" + pintID.ToString();
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
		///       This method uses to get data from sys_Table
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_TableVO
		///    </Outputs>
		///    <Returns>
		///       sys_TableVO
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
					+ sys_TableTable.TABLEID_FLD + ","
					+ sys_TableTable.CODE_FLD + ","
					+ sys_TableTable.TABLENAME_FLD + ","
					+ sys_TableTable.TABLEORVIEW_FLD + ","
					+ sys_TableTable.HEIGHT_FLD
					+ " FROM " + sys_TableTable.TABLE_NAME
					+ " WHERE " + sys_TableTable.TABLEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_TableVO objObject = new sys_TableVO();

				while (odrPCS.Read())
				{
					objObject.TableID = int.Parse(odrPCS[sys_TableTable.TABLEID_FLD].ToString());
					objObject.Code = odrPCS[sys_TableTable.CODE_FLD].ToString().Trim();
					objObject.TableName = odrPCS[sys_TableTable.TABLENAME_FLD].ToString().Trim();
					objObject.TableOrView = odrPCS[sys_TableTable.TABLEORVIEW_FLD].ToString().Trim();
					if (odrPCS[sys_TableTable.HEIGHT_FLD] != DBNull.Value)
						objObject.Height = int.Parse(odrPCS[sys_TableTable.HEIGHT_FLD].ToString());
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
		///       This method uses to update data to sys_Table
		///    </Description>
		///    <Inputs>
		///       sys_TableVO       
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
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///			Modified by NgocHT
		///    </Notes>
		//**************************************************************************
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			sys_TableVO objObject = (sys_TableVO) pobjObjecVO;

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = string.Empty;
				strSql = "UPDATE " + sys_TableTable.TABLE_NAME + " SET "
					+ sys_TableTable.CODE_FLD + "=   ?" + ","
					+ sys_TableTable.TABLENAME_FLD + "=   ?" + ","
					+ sys_TableTable.TABLEORVIEW_FLD + "=   ?" + ","
					+ sys_TableTable.ISVIEWONLY_FLD + "=   ?" + ","
					+ sys_TableTable.HEIGHT_FLD + "=  ?"
					+ " WHERE " + sys_TableTable.TABLEID_FLD + "= ?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.CODE_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.TABLENAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableTable.TABLENAME_FLD].Value = objObject.TableName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.TABLEORVIEW_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableTable.TABLEORVIEW_FLD].Value = objObject.TableOrView;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.ISVIEWONLY_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_TableTable.ISVIEWONLY_FLD].Value = objObject.IsViewOnly;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.HEIGHT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableTable.HEIGHT_FLD].Value = objObject.Height;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableTable.TABLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableTable.TABLEID_FLD].Value = objObject.TableID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
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
		///       This method uses to get all data from sys_Table
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
		///       Monday, December 29, 2004
		///    </History>
		///    <Notes>
		///			Modified by NgocHT
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
				string strSql = "SELECT t." + sys_TableTable.TABLEID_FLD
					+ " ,t." + sys_TableTable.CODE_FLD
					+ ",t." + sys_TableTable.TABLENAME_FLD
					+ ",t." + sys_TableTable.TABLEORVIEW_FLD
					+ ",t." + sys_TableTable.HEIGHT_FLD
					+ ",t." + sys_TableTable.ISVIEWONLY_FLD
					+ " FROM " + sys_TableTable.TABLE_NAME + " t"
					+ " INNER JOIN " + sys_TableAndGroupTable.TABLE_NAME + " a "
					+ " ON t.TableID = a.TableID "
					+ " ORDER BY a." + sys_TableAndGroupTable.TABLEGROUPID_FLD
					+ ",a." + sys_TableAndGroupTable.TABLEORDER_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_TableTable.TABLE_NAME);

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
//			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql = "SELECT "
					+ sys_TableTable.TABLEID_FLD + ","
					+ sys_TableTable.CODE_FLD + ","
					+ sys_TableTable.TABLENAME_FLD + ","
					+ sys_TableTable.TABLEORVIEW_FLD + ","
					+ sys_TableTable.HEIGHT_FLD;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
//				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, sys_TableTable.TABLE_NAME);

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
		///       This method uses to get all row in sys_table 
		///    </Description>
		///    <Inputs>
		///        N/A       
		///    </Inputs>
		///    <Outputs>
		///      dataset sys_TableVO
		///    </Outputs>
		///    <Returns>
		///       dataset
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListTableOrView()
		{
			const string METHOD_NAME = THIS + ".ListTableOrView()";
			DataSet dstPCS = new DataSet();

			string strSql = "SELECT " + SchemaTableTable.TABLENAME_FLD
				+ "," + SchemaTableTable.TABLETYPE_FLD
				+ "  FROM " + SchemaTableTable.TABLE_NAME
				+ " ORDER BY " + SchemaTableTable.TABLENAME_FLD;

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SchemaTableTable.TABLE_NAME);
				return dstPCS;

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
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
		///       This method uses to get the max value of last row in sys_table 
		///    </Description>
		///    <Inputs>
		///        GroupID      
		///    </Inputs>
		///    <Outputs>
		///      Max Table Order
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int MaxTableOrder(int pintGroupID)
		{
			const string METHOD_NAME = THIS + ".ReturnTableOrder()";
			string strSql = "SELECT ISNULL(MAX(" + sys_TableAndGroupTable.TABLEORDER_FLD + "),0)"
				+ " FROM  " + sys_TableAndGroupTable.TABLE_NAME
				+ " WHERE " + sys_TableAndGroupTable.TABLEGROUPID_FLD
				+ " = " + pintGroupID.ToString();

			int nMax;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				nMax = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				return nMax;

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
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
		///       This method uses to get the min value of row in sys_table 
		///    </Description>
		///    <Inputs>
		///        GroupID      
		///    </Inputs>
		///    <Outputs>
		///      MIN Table Order
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       30-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int MinTableOrder(int pintGroupID)
		{
			const string METHOD_NAME = THIS + ".ReturnTableOrder()";
			string strSql = "SELECT ISNULL(MIN(" + sys_TableAndGroupTable.TABLEORDER_FLD + "),0)"
				+ " FROM  " + sys_TableAndGroupTable.TABLE_NAME
				+ " WHERE " + sys_TableAndGroupTable.TABLEGROUPID_FLD
				+ " = " + pintGroupID.ToString();

			int nMax;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				nMax = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				return nMax;

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
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
		///       This method uses to get the max value of TableID in sys_table 
		///    </Description>
		///    <Inputs>
		///        N/A     
		///    </Inputs>
		///    <Outputs>
		///      Max TableID
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int MaxTableID()
		{
			const string METHOD_NAME = THIS + ".MaxTableID()";
			string strSql = "SELECT ISNULL(MAX(" + sys_TableTable.TABLEID_FLD + "),0)"
				+ " FROM  " + sys_TableTable.TABLE_NAME;

			int nMax;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				nMax = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				return nMax;

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
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
		///       This method uses to get the TableID from sys_Table table
		///    </Description>
		///    <Inputs>
		///        TableName     
		///    </Inputs>
		///    <Outputs>
		///      TableID
		///    </Outputs>
		///    <Returns>
		///       string
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       04-JAN-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetTableID(string pstrTableName)
		{
			const string METHOD_NAME = THIS + ".GetTableID()";
			string strSql = "SELECT " + sys_TableTable.TABLEID_FLD
				+ " FROM  " + sys_TableTable.TABLE_NAME
				+ " WHERE " + sys_TableTable.TABLEORVIEW_FLD + "='" + pstrTableName + "'";

			int intTableID;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				if (ocmdPCS.ExecuteScalar() == null || ocmdPCS.ExecuteScalar().ToString().Trim() == String.Empty)
				{
					intTableID = -1;
				}
				else
				{
					intTableID = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				}
				return intTableID;

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
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

		/// <summary>
		/// GetAllColumnNameOfTable
		/// </summary>
		/// <param name="pstrTableOrViewName"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 30 2005</date>
		public DataSet GetAllColumnNameOfTable(string pstrTableOrViewName)
		{
			const string METHOD_NAME = THIS + ".GetAllColumnNameOfTable()";
			DataSet dstTables = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "select Table_Name, Column_Name, Is_Nullable, Data_Type, Ordinal_Position "
								+ " from information_schema.columns where Table_Name = '" 
								+ pstrTableOrViewName + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstTables, sys_TableTable.TABLE_NAME);
				return dstTables;
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
		///       This method uses to get all data from sys_Table
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///    </History>
		///    <Notes>
		///			
		///    </Notes>
		//**************************************************************************
		public DataSet GetAllTables()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstTables = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT " + sys_TableTable.TABLEID_FLD
					+ " ," + sys_TableTable.CODE_FLD
					+ "," + sys_TableTable.TABLENAME_FLD
					+ "," + sys_TableTable.TABLEORVIEW_FLD
					+ "," + sys_TableTable.HEIGHT_FLD
					+ " FROM " + sys_TableTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstTables, sys_TableTable.TABLE_NAME);
				return dstTables;
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
		///       This method use to get data from specified table
		///    </Description>
		///    <Inputs>
		///       TableName, FromField, FilterField1, FilterField2
		///    </Inputs>
		///    <Outputs>
		///      DataTable
		///    </Outputs>
		///    <Returns>
		///      DataTable
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///     02-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetDataFromTable(string pstrTableName, string pstrFromField, string pstrFilterField1, string pstrFilterField2)
		{
			const string METHOD_NAME = THIS + ".GetDataFromTable()";
			DataSet dstTables = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT " + pstrFromField;
				if ((pstrFilterField1 != string.Empty) && (pstrFilterField1 != null))
				{
					strSql += "," + pstrFilterField1;
				}
				if ((pstrFilterField2 != string.Empty) && (pstrFilterField2 != null))
				{
					strSql += "," + pstrFilterField2;
				}
				strSql += " FROM " + pstrTableName;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstTables, pstrTableName);
				return dstTables.Tables[0];
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