using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_PartyLocationDS 
	{
		public MST_PartyLocationDS()
		{
		}

		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_PartyLocationDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_PartyLocation
		///    </Description>
		///    <Inputs>
		///        MST_PartyLocationVO       
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
				MST_PartyLocationVO objObject = (MST_PartyLocationVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO MST_PartyLocation("
					+ MST_PartyLocationTable.CODE_FLD + ","
					+ MST_PartyLocationTable.DESCRIPTION_FLD + ","
					+ MST_PartyLocationTable.DELETEREASON_FLD + ","
					+ MST_PartyLocationTable.ADDRESS_FLD + ","
					+ MST_PartyLocationTable.COUNTRYID_FLD + ","
					+ MST_PartyLocationTable.CITYID_FLD + ","
					+ MST_PartyLocationTable.STATE_FLD + ","
					+ MST_PartyLocationTable.ZIPPOST_FLD + ","
					+ MST_PartyLocationTable.PARTYID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.DELETEREASON_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_PartyLocationTable.DELETEREASON_FLD].Value = objObject.DeleteReason;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.ADDRESS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.ADDRESS_FLD].Value = objObject.Address;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.COUNTRYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyLocationTable.COUNTRYID_FLD].Value = objObject.CountryID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.CITYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyLocationTable.CITYID_FLD].Value = objObject.CityID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.STATE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.STATE_FLD].Value = objObject.State;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.ZIPPOST_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.ZIPPOST_FLD].Value = objObject.ZipPost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyLocationTable.PARTYID_FLD].Value = objObject.PartyID;


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
		///       This method uses to add data to MST_PartyLocation
		///    </Description>
		///    <Inputs>
		///        MST_PartyLocationVO       
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
			const string METHOD_NAME = THIS + ".AddReturnID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				MST_PartyLocationVO objObject = (MST_PartyLocationVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO MST_PartyLocation("
					+ MST_PartyLocationTable.CODE_FLD + ","
					+ MST_PartyLocationTable.DESCRIPTION_FLD + ","
					+ MST_PartyLocationTable.DELETEREASON_FLD + ","
					+ MST_PartyLocationTable.ADDRESS_FLD + ","
					+ MST_PartyLocationTable.COUNTRYID_FLD + ","
					+ MST_PartyLocationTable.CITYID_FLD + ","
					+ MST_PartyLocationTable.STATE_FLD + ","
					+ MST_PartyLocationTable.ZIPPOST_FLD + ","
					+ MST_PartyLocationTable.PARTYID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?)";
				strSql += " ; Select @@IDENTITY AS NEWID";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.DELETEREASON_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_PartyLocationTable.DELETEREASON_FLD].Value = objObject.DeleteReason;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.ADDRESS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.ADDRESS_FLD].Value = objObject.Address;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.COUNTRYID_FLD, OleDbType.Integer));
				if (objObject.CountryID > 0)
					ocmdPCS.Parameters[MST_PartyLocationTable.COUNTRYID_FLD].Value = objObject.CountryID;
				else
					ocmdPCS.Parameters[MST_PartyLocationTable.COUNTRYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.CITYID_FLD, OleDbType.Integer));
				if (objObject.CityID > 0)
					ocmdPCS.Parameters[MST_PartyLocationTable.CITYID_FLD].Value = objObject.CityID;
				else
					ocmdPCS.Parameters[MST_PartyLocationTable.CITYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.STATE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.STATE_FLD].Value = objObject.State;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.ZIPPOST_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.ZIPPOST_FLD].Value = objObject.ZipPost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyLocationTable.PARTYID_FLD].Value = objObject.PartyID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());
				//ocmdPCS.ExecuteNonQuery();	

			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 0)
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
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to delete data from MST_PartyLocation
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
			strSql = "DELETE " + MST_PartyLocationTable.TABLE_NAME + " WHERE  " + "PartyLocationID" + "=" + pintID.ToString();
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
		///       This method uses to delete data from MST_PartyLocation by Party
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
			strSql = "DELETE " + MST_PartyLocationTable.TABLE_NAME + " WHERE  " + MST_PartyLocationTable.PARTYID_FLD + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_PartyLocation
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_PartyLocationVO
		///    </Outputs>
		///    <Returns>
		///       MST_PartyLocationVO
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

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ MST_PartyLocationTable.PARTYLOCATIONID_FLD + ","
					+ MST_PartyLocationTable.CODE_FLD + ","
					+ MST_PartyLocationTable.DESCRIPTION_FLD + ","
					+ MST_PartyLocationTable.DELETEREASON_FLD + ","
					+ MST_PartyLocationTable.ADDRESS_FLD + ","
					+ MST_PartyLocationTable.COUNTRYID_FLD + ","
					+ MST_PartyLocationTable.CITYID_FLD + ","
					+ MST_PartyLocationTable.STATE_FLD + ","
					+ MST_PartyLocationTable.ZIPPOST_FLD + ","
					+ MST_PartyLocationTable.PARTYID_FLD
					+ " FROM " + MST_PartyLocationTable.TABLE_NAME
					+ " WHERE " + MST_PartyLocationTable.PARTYLOCATIONID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_PartyLocationVO objObject = new MST_PartyLocationVO();

				while (odrPCS.Read())
				{
					objObject.PartyLocationID = int.Parse(odrPCS[MST_PartyLocationTable.PARTYLOCATIONID_FLD].ToString().Trim());
					objObject.Code = odrPCS[MST_PartyLocationTable.CODE_FLD].ToString().Trim();
					objObject.Description = odrPCS[MST_PartyLocationTable.DESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[MST_PartyLocationTable.DELETEREASON_FLD] != DBNull.Value)
						objObject.DeleteReason = bool.Parse(odrPCS[MST_PartyLocationTable.DELETEREASON_FLD].ToString().Trim());
					else
						objObject.DeleteReason = false;
					objObject.Address = odrPCS[MST_PartyLocationTable.ADDRESS_FLD].ToString().Trim();
					if (odrPCS[MST_PartyLocationTable.COUNTRYID_FLD] != DBNull.Value)
						objObject.CountryID = int.Parse(odrPCS[MST_PartyLocationTable.COUNTRYID_FLD].ToString().Trim());
					if (odrPCS[MST_PartyLocationTable.CITYID_FLD] != DBNull.Value)
						objObject.CityID = int.Parse(odrPCS[MST_PartyLocationTable.CITYID_FLD].ToString().Trim());
					objObject.State = odrPCS[MST_PartyLocationTable.STATE_FLD].ToString().Trim();
					objObject.ZipPost = odrPCS[MST_PartyLocationTable.ZIPPOST_FLD].ToString().Trim();
					objObject.PartyID = int.Parse(odrPCS[MST_PartyLocationTable.PARTYID_FLD].ToString().Trim());

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
		///       This method uses to get data from MST_PartyLocation by code
		///    </Description>
		///    <Inputs>
		///        Code
		///    </Inputs>
		///    <Outputs>
		///       MST_PartyLocationVO
		///    </Outputs>
		///    <Returns>
		///       MST_PartyLocationVO
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
		public object GetObjectVO(int pintPartyID, string pstrCode)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ MST_PartyLocationTable.PARTYLOCATIONID_FLD + ","
					+ MST_PartyLocationTable.CODE_FLD + ","
					+ MST_PartyLocationTable.DESCRIPTION_FLD + ","
					+ MST_PartyLocationTable.DELETEREASON_FLD + ","
					+ MST_PartyLocationTable.ADDRESS_FLD + ","
					+ MST_PartyLocationTable.COUNTRYID_FLD + ","
					+ MST_PartyLocationTable.CITYID_FLD + ","
					+ MST_PartyLocationTable.STATE_FLD + ","
					+ MST_PartyLocationTable.ZIPPOST_FLD + ","
					+ MST_PartyLocationTable.PARTYID_FLD
					+ " FROM " + MST_PartyLocationTable.TABLE_NAME
					+ " WHERE " + MST_PartyLocationTable.CODE_FLD + "='" + pstrCode + "'"
					+ " AND " + MST_PartyLocationTable.PARTYID_FLD + "=" + pintPartyID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_PartyLocationVO objObject = new MST_PartyLocationVO();

				while (odrPCS.Read())
				{
					objObject.PartyLocationID = int.Parse(odrPCS[MST_PartyLocationTable.PARTYLOCATIONID_FLD].ToString().Trim());
					objObject.Code = odrPCS[MST_PartyLocationTable.CODE_FLD].ToString().Trim();
					objObject.Description = odrPCS[MST_PartyLocationTable.DESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[MST_PartyLocationTable.DELETEREASON_FLD] != DBNull.Value)
						objObject.DeleteReason = bool.Parse(odrPCS[MST_PartyLocationTable.DELETEREASON_FLD].ToString().Trim());
					else
						objObject.DeleteReason = false;
					objObject.Address = odrPCS[MST_PartyLocationTable.ADDRESS_FLD].ToString().Trim();
					if (odrPCS[MST_PartyLocationTable.COUNTRYID_FLD] != DBNull.Value)
						objObject.CountryID = int.Parse(odrPCS[MST_PartyLocationTable.COUNTRYID_FLD].ToString().Trim());
					if (odrPCS[MST_PartyLocationTable.CITYID_FLD] != DBNull.Value)
						objObject.CityID = int.Parse(odrPCS[MST_PartyLocationTable.CITYID_FLD].ToString().Trim());
					objObject.State = odrPCS[MST_PartyLocationTable.STATE_FLD].ToString().Trim();
					objObject.ZipPost = odrPCS[MST_PartyLocationTable.ZIPPOST_FLD].ToString().Trim();
					objObject.PartyID = int.Parse(odrPCS[MST_PartyLocationTable.PARTYID_FLD].ToString().Trim());

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
		///       This method uses to get data from MST_PartyLocation
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_PartyLocationVO
		///    </Outputs>
		///    <Returns>
		///       MST_PartyLocationVO
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
		public string GetLocationCode(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetLocationCode()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ MST_PartyLocationTable.CODE_FLD 
					+ " FROM " + MST_PartyLocationTable.TABLE_NAME
					+ " WHERE " + MST_PartyLocationTable.PARTYLOCATIONID_FLD + "=" + pintID;

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();

				if (objResult == null)
				{
					return String.Empty;
				}
				else
				{
					return objResult.ToString();
				}
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
		///       This method uses to get data from MST_PartyLocation by code
		///    </Description>
		///    <Inputs>
		///        Code
		///    </Inputs>
		///    <Outputs>
		///       MST_PartyLocationVO
		///    </Outputs>
		///    <Returns>
		///       MST_PartyLocationVO
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
					+ MST_PartyLocationTable.PARTYLOCATIONID_FLD + ","
					+ MST_PartyLocationTable.CODE_FLD + ","
					+ MST_PartyLocationTable.DESCRIPTION_FLD + ","
					+ MST_PartyLocationTable.DELETEREASON_FLD + ","
					+ MST_PartyLocationTable.ADDRESS_FLD + ","
					+ MST_PartyLocationTable.COUNTRYID_FLD + ","
					+ MST_PartyLocationTable.CITYID_FLD + ","
					+ MST_PartyLocationTable.STATE_FLD + ","
					+ MST_PartyLocationTable.ZIPPOST_FLD + ","
					+ MST_PartyLocationTable.PARTYID_FLD
					+ " FROM " + MST_PartyLocationTable.TABLE_NAME
					+ " WHERE " + MST_PartyLocationTable.CODE_FLD + "=" + pstrCode;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_PartyLocationVO objObject = new MST_PartyLocationVO();

				while (odrPCS.Read())
				{
					objObject.PartyLocationID = int.Parse(odrPCS[MST_PartyLocationTable.PARTYLOCATIONID_FLD].ToString().Trim());
					objObject.Code = odrPCS[MST_PartyLocationTable.CODE_FLD].ToString().Trim();
					objObject.Description = odrPCS[MST_PartyLocationTable.DESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[MST_PartyLocationTable.DELETEREASON_FLD] != DBNull.Value)
						objObject.DeleteReason = bool.Parse(odrPCS[MST_PartyLocationTable.DELETEREASON_FLD].ToString().Trim());
					else
						objObject.DeleteReason = false;
					objObject.Address = odrPCS[MST_PartyLocationTable.ADDRESS_FLD].ToString().Trim();
					if (odrPCS[MST_PartyLocationTable.COUNTRYID_FLD] != DBNull.Value)
						objObject.CountryID = int.Parse(odrPCS[MST_PartyLocationTable.COUNTRYID_FLD].ToString().Trim());
					if (odrPCS[MST_PartyLocationTable.CITYID_FLD] != DBNull.Value)
						objObject.CityID = int.Parse(odrPCS[MST_PartyLocationTable.CITYID_FLD].ToString().Trim());
					objObject.State = odrPCS[MST_PartyLocationTable.STATE_FLD].ToString().Trim();
					objObject.ZipPost = odrPCS[MST_PartyLocationTable.ZIPPOST_FLD].ToString().Trim();
					objObject.PartyID = int.Parse(odrPCS[MST_PartyLocationTable.PARTYID_FLD].ToString().Trim());

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
		///       This method uses to update data to MST_PartyLocation
		///    </Description>
		///    <Inputs>
		///       MST_PartyLocationVO       
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

			MST_PartyLocationVO objObject = (MST_PartyLocationVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE MST_PartyLocation SET "
					+ MST_PartyLocationTable.CODE_FLD + "=   ?" + ","
					+ MST_PartyLocationTable.DESCRIPTION_FLD + "=   ?" + ","
					+ MST_PartyLocationTable.DELETEREASON_FLD + "=   ?" + ","
					+ MST_PartyLocationTable.ADDRESS_FLD + "=   ?" + ","
					+ MST_PartyLocationTable.COUNTRYID_FLD + "=   ?" + ","
					+ MST_PartyLocationTable.CITYID_FLD + "=   ?" + ","
					+ MST_PartyLocationTable.STATE_FLD + "=   ?" + ","
					+ MST_PartyLocationTable.ZIPPOST_FLD + "=   ?" + ","
					+ MST_PartyLocationTable.PARTYID_FLD + "=  ?"
					+ " WHERE " + MST_PartyLocationTable.PARTYLOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.DELETEREASON_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_PartyLocationTable.DELETEREASON_FLD].Value = objObject.DeleteReason;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.ADDRESS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.ADDRESS_FLD].Value = objObject.Address;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.COUNTRYID_FLD, OleDbType.Integer));
				if (objObject.CountryID > 0)
					ocmdPCS.Parameters[MST_PartyLocationTable.COUNTRYID_FLD].Value = objObject.CountryID;
				else
					ocmdPCS.Parameters[MST_PartyLocationTable.COUNTRYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.CITYID_FLD, OleDbType.Integer));
				if (objObject.CityID > 0)
					ocmdPCS.Parameters[MST_PartyLocationTable.CITYID_FLD].Value = objObject.CityID;
				else
					ocmdPCS.Parameters[MST_PartyLocationTable.CITYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.STATE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.STATE_FLD].Value = objObject.State;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.ZIPPOST_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyLocationTable.ZIPPOST_FLD].Value = objObject.ZipPost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.PARTYID_FLD, OleDbType.Integer));
				if (objObject.PartyID > 0)
					ocmdPCS.Parameters[MST_PartyLocationTable.PARTYID_FLD].Value = objObject.PartyID;
				else
					ocmdPCS.Parameters[MST_PartyLocationTable.PARTYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyLocationTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.PartyLocationID > 0)
					ocmdPCS.Parameters[MST_PartyLocationTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;
				else
					ocmdPCS.Parameters[MST_PartyLocationTable.PARTYLOCATIONID_FLD].Value = DBNull.Value;


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
		///       This method uses to get all data from MST_PartyLocation
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
		///       Thursday, January 06, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListPartyLocation(int pintPartyID)
		{
			const string METHOD_NAME = THIS + ".ListPartyLocation()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ MST_PartyLocationTable.PARTYLOCATIONID_FLD + ","
					+ MST_PartyLocationTable.CODE_FLD + ","
					+ MST_PartyLocationTable.DESCRIPTION_FLD + ","
					+ MST_PartyLocationTable.PARTYID_FLD
					+ " FROM " + MST_PartyLocationTable.TABLE_NAME
					+ " WHERE " + MST_PartyLocationTable.PARTYID_FLD + "=" + pintPartyID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_PartyLocationTable.TABLE_NAME);

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
		///       This method uses to get all data from MST_PartyLocation
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
					+ MST_PartyLocationTable.PARTYLOCATIONID_FLD + ","
					+ MST_PartyLocationTable.CODE_FLD + ","
					+ MST_PartyLocationTable.DESCRIPTION_FLD + ","
					+ MST_PartyLocationTable.DELETEREASON_FLD + ","
					+ MST_PartyLocationTable.ADDRESS_FLD + ","
					+ MST_PartyLocationTable.COUNTRYID_FLD + ","
					+ MST_PartyLocationTable.CITYID_FLD + ","
					+ MST_PartyLocationTable.STATE_FLD + ","
					+ MST_PartyLocationTable.ZIPPOST_FLD + ","
					+ MST_PartyLocationTable.PARTYID_FLD
					+ " FROM " + MST_PartyLocationTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_PartyLocationTable.TABLE_NAME);

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
		///       This method uses to get all data from MST_PartyLocation
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
		public DataSet ListForCombo()
		{
			const string METHOD_NAME = THIS + ".ListForCombo()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ MST_PartyLocationTable.CODE_FLD + ","
					+ MST_PartyLocationTable.DESCRIPTION_FLD + ","
					+ MST_PartyLocationTable.ADDRESS_FLD + ","
					+ MST_PartyLocationTable.STATE_FLD + ","
					+ MST_PartyLocationTable.ZIPPOST_FLD + ","
					+ MST_PartyLocationTable.PARTYID_FLD + ","
					+ MST_PartyLocationTable.PARTYLOCATIONID_FLD
					+ " FROM " + MST_PartyLocationTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_PartyLocationTable.TABLE_NAME);

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
					+ MST_PartyLocationTable.PARTYLOCATIONID_FLD + ","
					+ MST_PartyLocationTable.CODE_FLD + ","
					+ MST_PartyLocationTable.DESCRIPTION_FLD + ","
					+ MST_PartyLocationTable.DELETEREASON_FLD + ","
					+ MST_PartyLocationTable.ADDRESS_FLD + ","
					+ MST_PartyLocationTable.COUNTRYID_FLD + ","
					+ MST_PartyLocationTable.CITYID_FLD + ","
					+ MST_PartyLocationTable.STATE_FLD + ","
					+ MST_PartyLocationTable.ZIPPOST_FLD + ","
					+ MST_PartyLocationTable.PARTYID_FLD
					+ "  FROM " + MST_PartyLocationTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, MST_PartyLocationTable.TABLE_NAME);

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