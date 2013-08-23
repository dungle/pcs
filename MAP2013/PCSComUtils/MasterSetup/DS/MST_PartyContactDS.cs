using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_PartyContactDS 
	{
		public MST_PartyContactDS()
		{
		}

		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_PartyContactDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_PartyContact
		///    </Description>
		///    <Inputs>
		///        MST_PartyContactVO       
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
		///       Tuesday, January 25, 2005
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
				MST_PartyContactVO objObject = (MST_PartyContactVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO MST_PartyContact("
					+ MST_PartyContactTable.CODE_FLD + ","
					+ MST_PartyContactTable.NAME_FLD + ","
					+ MST_PartyContactTable.TITLE_FLD + ","
					+ MST_PartyContactTable.MEMO_FLD + ","
					+ MST_PartyContactTable.FAX_FLD + ","
					+ MST_PartyContactTable.PHONE_FLD + ","
					+ MST_PartyContactTable.EMAIL_FLD + ","
					+ MST_PartyContactTable.EXT_FLD + ","
					+ MST_PartyContactTable.PARTYLOCATIONID_FLD + ","
					+ MST_PartyContactTable.PARTYID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.TITLE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.TITLE_FLD].Value = objObject.Title;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.MEMO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.MEMO_FLD].Value = objObject.Memo;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.FAX_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.FAX_FLD].Value = objObject.Fax;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.PHONE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.PHONE_FLD].Value = objObject.Phone;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.EMAIL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.EMAIL_FLD].Value = objObject.Email;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.EXT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.EXT_FLD].Value = objObject.Ext;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyContactTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyContactTable.PARTYID_FLD].Value = objObject.PartyID;


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
		///       This method uses to add data to MST_PartyContact
		///    </Description>
		///    <Inputs>
		///        MST_PartyContactVO       
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
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int AddReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				MST_PartyContactVO objObject = (MST_PartyContactVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO MST_PartyContact("
					+ MST_PartyContactTable.CODE_FLD + ","
					+ MST_PartyContactTable.NAME_FLD + ","
					+ MST_PartyContactTable.TITLE_FLD + ","
					+ MST_PartyContactTable.MEMO_FLD + ","
					+ MST_PartyContactTable.FAX_FLD + ","
					+ MST_PartyContactTable.PHONE_FLD + ","
					+ MST_PartyContactTable.EMAIL_FLD + ","
					+ MST_PartyContactTable.EXT_FLD + ",";
				if (objObject.PartyLocationID > 0)
					strSql += MST_PartyContactTable.PARTYLOCATIONID_FLD + ",";
                strSql += MST_PartyContactTable.PARTYID_FLD + ")";
				if (objObject.PartyLocationID > 0)
					strSql += "VALUES(?,?,?,?,?,?,?,?,?,?)";
				else
					strSql += "VALUES(?,?,?,?,?,?,?,?,?)";
				strSql += " ; Select @@IDENTITY AS NEWID";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.TITLE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.TITLE_FLD].Value = objObject.Title;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.MEMO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.MEMO_FLD].Value = objObject.Memo;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.FAX_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.FAX_FLD].Value = objObject.Fax;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.PHONE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.PHONE_FLD].Value = objObject.Phone;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.EMAIL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.EMAIL_FLD].Value = objObject.Email;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.EXT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.EXT_FLD].Value = objObject.Ext;

				if (objObject.PartyLocationID > 0)
				{
					ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
					ocmdPCS.Parameters[MST_PartyContactTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyContactTable.PARTYID_FLD].Value = objObject.PartyID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());
				//ocmdPCS.ExecuteNonQuery();	

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
		///       This method uses to delete data from MST_PartyContact
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
			strSql = "DELETE " + MST_PartyContactTable.TABLE_NAME + " WHERE  " + "PartyContactID" + "=" + pintID.ToString();
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
		///       This method uses to delete data from MST_PartyContact by PartyID
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
		///       DungLa
		///    </Authors>
		///    <History>
		///       04-Mar-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void DeleteByParty(int pintID)
		{
			const string METHOD_NAME = THIS + ".DeleteByParty()";
			string strSql = String.Empty;
			strSql = "DELETE " + MST_PartyContactTable.TABLE_NAME + " WHERE  " + MST_PartyContactTable.PARTYID_FLD + "=" + pintID.ToString();
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
		///       This method uses to delete data from MST_PartyContact by PartyID
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
		///       DungLa
		///    </Authors>
		///    <History>
		///       04-Mar-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void DeleteByLocation(int pintID)
		{
			const string METHOD_NAME = THIS + ".DeleteByLocation()";
			string strSql = String.Empty;
			strSql = "DELETE " + MST_PartyContactTable.TABLE_NAME + " WHERE  " + MST_PartyContactTable.PARTYLOCATIONID_FLD + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_PartyContact
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_PartyContactVO
		///    </Outputs>
		///    <Returns>
		///       MST_PartyContactVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
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
					+ MST_PartyContactTable.PARTYCONTACTID_FLD + ","
					+ MST_PartyContactTable.CODE_FLD + ","
					+ MST_PartyContactTable.NAME_FLD + ","
					+ MST_PartyContactTable.TITLE_FLD + ","
					+ MST_PartyContactTable.MEMO_FLD + ","
					+ MST_PartyContactTable.FAX_FLD + ","
					+ MST_PartyContactTable.PHONE_FLD + ","
					+ MST_PartyContactTable.EMAIL_FLD + ","
					+ MST_PartyContactTable.EXT_FLD + ","
					+ MST_PartyContactTable.PARTYLOCATIONID_FLD + ","
					+ MST_PartyContactTable.PARTYID_FLD
					+ " FROM " + MST_PartyContactTable.TABLE_NAME
					+ " WHERE " + MST_PartyContactTable.PARTYCONTACTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_PartyContactVO objObject = new MST_PartyContactVO();

				while (odrPCS.Read())
				{
					objObject.PartyContactID = int.Parse(odrPCS[MST_PartyContactTable.PARTYCONTACTID_FLD].ToString().Trim());
					objObject.Code = odrPCS[MST_PartyContactTable.CODE_FLD].ToString().Trim();
					objObject.Name = odrPCS[MST_PartyContactTable.NAME_FLD].ToString().Trim();
					objObject.Title = odrPCS[MST_PartyContactTable.TITLE_FLD].ToString().Trim();
					objObject.Memo = odrPCS[MST_PartyContactTable.MEMO_FLD].ToString().Trim();
					objObject.Fax = odrPCS[MST_PartyContactTable.FAX_FLD].ToString().Trim();
					objObject.Phone = odrPCS[MST_PartyContactTable.PHONE_FLD].ToString().Trim();
					objObject.Email = odrPCS[MST_PartyContactTable.EMAIL_FLD].ToString().Trim();
					objObject.Ext = odrPCS[MST_PartyContactTable.EXT_FLD].ToString().Trim();
					if (odrPCS[MST_PartyContactTable.PARTYLOCATIONID_FLD] != DBNull.Value)
						objObject.PartyLocationID = int.Parse(odrPCS[MST_PartyContactTable.PARTYLOCATIONID_FLD].ToString().Trim());
					if (odrPCS[MST_PartyContactTable.PARTYID_FLD] != DBNull.Value)
						objObject.PartyID = int.Parse(odrPCS[MST_PartyContactTable.PARTYID_FLD].ToString().Trim());

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
		///       This method uses to get data from MST_PartyContact by code
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_PartyContactVO
		///    </Outputs>
		///    <Returns>
		///       MST_PartyContactVO
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(string pstrCode)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ MST_PartyContactTable.PARTYCONTACTID_FLD + ","
					+ MST_PartyContactTable.CODE_FLD + ","
					+ MST_PartyContactTable.NAME_FLD + ","
					+ MST_PartyContactTable.TITLE_FLD + ","
					+ MST_PartyContactTable.MEMO_FLD + ","
					+ MST_PartyContactTable.FAX_FLD + ","
					+ MST_PartyContactTable.PHONE_FLD + ","
					+ MST_PartyContactTable.EMAIL_FLD + ","
					+ MST_PartyContactTable.EXT_FLD + ","
					+ MST_PartyContactTable.PARTYLOCATIONID_FLD + ","
					+ MST_PartyContactTable.PARTYID_FLD
					+ " FROM " + MST_PartyContactTable.TABLE_NAME
					+ " WHERE " + MST_PartyContactTable.CODE_FLD + "='" + pstrCode + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_PartyContactVO objObject = new MST_PartyContactVO();

				while (odrPCS.Read())
				{
					objObject.PartyContactID = int.Parse(odrPCS[MST_PartyContactTable.PARTYCONTACTID_FLD].ToString().Trim());
					objObject.Code = odrPCS[MST_PartyContactTable.CODE_FLD].ToString().Trim();
					objObject.Name = odrPCS[MST_PartyContactTable.NAME_FLD].ToString().Trim();
					objObject.Title = odrPCS[MST_PartyContactTable.TITLE_FLD].ToString().Trim();
					objObject.Memo = odrPCS[MST_PartyContactTable.MEMO_FLD].ToString().Trim();
					objObject.Fax = odrPCS[MST_PartyContactTable.FAX_FLD].ToString().Trim();
					objObject.Phone = odrPCS[MST_PartyContactTable.PHONE_FLD].ToString().Trim();
					objObject.Email = odrPCS[MST_PartyContactTable.EMAIL_FLD].ToString().Trim();
					objObject.Ext = odrPCS[MST_PartyContactTable.EXT_FLD].ToString().Trim();
					if (odrPCS[MST_PartyContactTable.PARTYLOCATIONID_FLD] != DBNull.Value)
						objObject.PartyLocationID = int.Parse(odrPCS[MST_PartyContactTable.PARTYLOCATIONID_FLD].ToString().Trim());
					if (odrPCS[MST_PartyContactTable.PARTYID_FLD] != DBNull.Value)
						objObject.PartyID = int.Parse(odrPCS[MST_PartyContactTable.PARTYID_FLD].ToString().Trim());

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
		///       This method uses to update data to MST_PartyContact
		///    </Description>
		///    <Inputs>
		///       MST_PartyContactVO       
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

			MST_PartyContactVO objObject = (MST_PartyContactVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE MST_PartyContact SET "
					+ MST_PartyContactTable.CODE_FLD + "=   ?" + ","
					+ MST_PartyContactTable.NAME_FLD + "=   ?" + ","
					+ MST_PartyContactTable.TITLE_FLD + "=   ?" + ","
					+ MST_PartyContactTable.MEMO_FLD + "=   ?" + ","
					+ MST_PartyContactTable.FAX_FLD + "=   ?" + ","
					+ MST_PartyContactTable.PHONE_FLD + "=   ?" + ","
					+ MST_PartyContactTable.EMAIL_FLD + "=   ?" + ","
					+ MST_PartyContactTable.EXT_FLD + "=   ?" + ",";
				if (objObject.PartyLocationID > 0)
					strSql += MST_PartyContactTable.PARTYLOCATIONID_FLD + "=   ?" + ",";
				strSql += MST_PartyContactTable.PARTYID_FLD + "=  ?"
					+ " WHERE " + MST_PartyContactTable.PARTYCONTACTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.TITLE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.TITLE_FLD].Value = objObject.Title;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.MEMO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.MEMO_FLD].Value = objObject.Memo;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.FAX_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.FAX_FLD].Value = objObject.Fax;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.PHONE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.PHONE_FLD].Value = objObject.Phone;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.EMAIL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.EMAIL_FLD].Value = objObject.Email;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.EXT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyContactTable.EXT_FLD].Value = objObject.Ext;

				if (objObject.PartyLocationID > 0)
				{
					ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
					ocmdPCS.Parameters[MST_PartyContactTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyContactTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyContactTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyContactTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;


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
		///       This method uses to get all data from MST_PartyContact
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
		///       Tuesday, January 25, 2005
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
					+ MST_PartyContactTable.PARTYCONTACTID_FLD + ","
					+ MST_PartyContactTable.CODE_FLD + ","
					+ MST_PartyContactTable.NAME_FLD + ","
					+ MST_PartyContactTable.TITLE_FLD + ","
					+ MST_PartyContactTable.MEMO_FLD + ","
					+ MST_PartyContactTable.FAX_FLD + ","
					+ MST_PartyContactTable.PHONE_FLD + ","
					+ MST_PartyContactTable.EMAIL_FLD + ","
					+ MST_PartyContactTable.EXT_FLD + ","
					+ MST_PartyContactTable.PARTYLOCATIONID_FLD + ","
					+ MST_PartyContactTable.PARTYID_FLD
					+ " FROM " + MST_PartyContactTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_PartyContactTable.TABLE_NAME);

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
		
		public DataSet ListByPartyLocationID(int pintPartyLocationID)
		{
			return null;
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
		///       Tuesday, January 25, 2005
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
					+ MST_PartyContactTable.PARTYCONTACTID_FLD + ","
					+ MST_PartyContactTable.CODE_FLD + ","
					+ MST_PartyContactTable.NAME_FLD + ","
					+ MST_PartyContactTable.TITLE_FLD + ","
					+ MST_PartyContactTable.MEMO_FLD + ","
					+ MST_PartyContactTable.FAX_FLD + ","
					+ MST_PartyContactTable.PHONE_FLD + ","
					+ MST_PartyContactTable.EMAIL_FLD + ","
					+ MST_PartyContactTable.EXT_FLD + ","
					+ MST_PartyContactTable.PARTYLOCATIONID_FLD + ","
					+ MST_PartyContactTable.PARTYID_FLD
					+ "  FROM " + MST_PartyContactTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, MST_PartyContactTable.TABLE_NAME);

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
	}
}