using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_PartyDS 
	{
		public MST_PartyDS()
		{
		}

		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_PartyDS";


		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_Party
		///    </Description>
		///    <Inputs>
		///        MST_PartyVO       
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
		///       Monday, February 14, 2005
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
				MST_PartyVO objObject = (MST_PartyVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO " + MST_PartyTable.TABLE_NAME + "("
					+ MST_PartyTable.CODE_FLD + ","
					+ MST_PartyTable.NAME_FLD + ","
					+ MST_PartyTable.ADDRESS_FLD + ","
					+ MST_PartyTable.WEBSITE_FLD + ","
					+ MST_PartyTable.STATE_FLD + ","
					+ MST_PartyTable.DELETEREASON_FLD + ","
					+ MST_PartyTable.TYPE_FLD + ","
					+ MST_PartyTable.ZIPPOST_FLD + ","
					+ MST_PartyTable.VATCODE_FLD + ","
					+ MST_PartyTable.COUNTRYID_FLD + ","					
					+ MST_PartyTable.PHONE_FLD + ", "
					+ MST_PartyTable.FAX_FLD + ", "					
					+ MST_PartyTable.BANKACCOUNT_FLD + ", "					
					+ MST_PartyTable.CITYID_FLD + ")"
					+ "VALUES(?, ?, ?, ?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.ADDRESS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.ADDRESS_FLD].Value = objObject.Address;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.WEBSITE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.WEBSITE_FLD].Value = objObject.WebSite;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.STATE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.STATE_FLD].Value = objObject.State;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.DELETEREASON_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_PartyTable.DELETEREASON_FLD].Value = objObject.DeleteReason;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.ZIPPOST_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.ZIPPOST_FLD].Value = objObject.ZipPost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.VATCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.VATCODE_FLD].Value = objObject.VATCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.COUNTRYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyTable.COUNTRYID_FLD].Value = objObject.CountryID;
				
				//Begin_ Added by Tuan TQ -2005-09-22
				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.PHONE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.PHONE_FLD].Value = objObject.Phone;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.FAX_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.FAX_FLD].Value = objObject.Fax;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.BANKACCOUNT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.BANKACCOUNT_FLD].Value = objObject.BankAccount;
				//End_ Added by Tuan TQ -2005-09-22

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
		///       This method uses to add data to MST_Party
		///    </Description>
		///    <Inputs>
		///        MST_PartyVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       04-Mar-2005
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
				MST_PartyVO objObject = (MST_PartyVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO MST_Party("
					+ MST_PartyTable.CODE_FLD + ","
					+ MST_PartyTable.NAME_FLD + ","
					+ MST_PartyTable.ADDRESS_FLD + ","
					+ MST_PartyTable.WEBSITE_FLD + ","
					+ MST_PartyTable.STATE_FLD + ","
					+ MST_PartyTable.DELETEREASON_FLD + ","
					+ MST_PartyTable.TYPE_FLD + ","
					+ MST_PartyTable.ZIPPOST_FLD + ","
					+ MST_PartyTable.MAPBANKACCOUNTNAME_FLD + ","
					+ MST_PartyTable.MAPBANKACCOUNTNO_FLD + ","
					+ MST_PartyTable.VATCODE_FLD + ","
					+ MST_PartyTable.COUNTRYID_FLD + ","
					+ MST_PartyTable.PHONE_FLD + ", "
					+ MST_PartyTable.FAX_FLD + ", "
					+ MST_PartyTable.BANKACCOUNT_FLD + ", "
					+ "CurrencyID, "
					+ "PaymentTermID, "
					+ MST_PartyTable.CITYID_FLD + ")"
					+ "VALUES(?, ?, ?, ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
				strSql += " ; Select @@IDENTITY AS NEWID";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.ADDRESS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.ADDRESS_FLD].Value = objObject.Address;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.WEBSITE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.WEBSITE_FLD].Value = objObject.WebSite;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.STATE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.STATE_FLD].Value = objObject.State;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.DELETEREASON_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_PartyTable.DELETEREASON_FLD].Value = objObject.DeleteReason;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.ZIPPOST_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.ZIPPOST_FLD].Value = objObject.ZipPost;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.MAPBANKACCOUNTNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.MAPBANKACCOUNTNAME_FLD].Value = objObject.MAPBankAccountName;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.MAPBANKACCOUNTNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.MAPBANKACCOUNTNO_FLD].Value = objObject.MAPBankAccountNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.VATCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.VATCODE_FLD].Value = objObject.VATCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.COUNTRYID_FLD, OleDbType.Integer));
				if (objObject.CountryID > 0)
					ocmdPCS.Parameters[MST_PartyTable.COUNTRYID_FLD].Value = objObject.CountryID;
				else
					ocmdPCS.Parameters[MST_PartyTable.COUNTRYID_FLD].Value = DBNull.Value;
				
				//Begin_ Added by Tuan TQ -2005-09-22
				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.PHONE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.PHONE_FLD].Value = objObject.Phone;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.FAX_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.FAX_FLD].Value = objObject.Fax;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.BANKACCOUNT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.BANKACCOUNT_FLD].Value = objObject.BankAccount;
				//End_ Added by Tuan TQ -2005-09-22

				ocmdPCS.Parameters.Add(new OleDbParameter("CurrencyID", OleDbType.Integer));
				if (objObject.CurrencyID > 0)
					ocmdPCS.Parameters["CurrencyID"].Value = objObject.CurrencyID;
				else
					ocmdPCS.Parameters["CurrencyID"].Value = DBNull.Value;
				
				ocmdPCS.Parameters.Add(new OleDbParameter("PaymentTermID", OleDbType.Integer));
				if (objObject.PaymentTermID > 0)
					ocmdPCS.Parameters["PaymentTermID"].Value = objObject.PaymentTermID;
				else
					ocmdPCS.Parameters["PaymentTermID"].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.CITYID_FLD, OleDbType.Integer));
				if (objObject.CityID > 0)
					ocmdPCS.Parameters[MST_PartyTable.CITYID_FLD].Value = objObject.CityID;
				else
					ocmdPCS.Parameters[MST_PartyTable.CITYID_FLD].Value = DBNull.Value;


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
		///       This method uses to delete data from MST_Party
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
			strSql = "DELETE " + MST_PartyTable.TABLE_NAME + " WHERE  " + "PartyID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_Party
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_PartyVO
		///    </Outputs>
		///    <Returns>
		///       MST_PartyVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Monday, February 14, 2005
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
				string strSql = "SELECT PartyID, Code, Name, Address, WebSite, State,"
				                + " DeleteReason, Type, ZipPost,MAPBankAccountName,MAPBankAccountNo, VATCode, CountryID, CityID, Phone, Fax,"
				                + " BankAccount, PaymentTermID, CurrencyID"
				                + " FROM MST_Party WHERE PartyID = " + pintID;
				//strSql = string.Format("SELECT {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}, {12}, {13}, PaymentTermID, {14} FROM {15} WHERE {16}={17}", MST_PartyTable.PARTYID_FLD, MST_PartyTable.CODE_FLD, MST_PartyTable.NAME_FLD, MST_PartyTable.ADDRESS_FLD, MST_PartyTable.WEBSITE_FLD, MST_PartyTable.STATE_FLD, MST_PartyTable.DELETEREASON_FLD, MST_PartyTable.TYPE_FLD, MST_PartyTable.ZIPPOST_FLD, MST_PartyTable.VATCODE_FLD, MST_PartyTable.COUNTRYID_FLD, MST_PartyTable.PHONE_FLD, MST_PartyTable.FAX_FLD, MST_PartyTable.BANKACCOUNT_FLD, MST_PartyTable.CITYID_FLD, MST_PartyTable.TABLE_NAME, MST_PartyTable.PARTYID_FLD, pintID);

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_PartyVO objObject = new MST_PartyVO();

				while (odrPCS.Read())
				{
					if (odrPCS[MST_PartyTable.PARTYID_FLD] != DBNull.Value)
					{
						objObject.PartyID = int.Parse(odrPCS[MST_PartyTable.PARTYID_FLD].ToString().Trim());
					}
					if (odrPCS[MST_PartyTable.CODE_FLD] != DBNull.Value)
					{
						objObject.Code = odrPCS[MST_PartyTable.CODE_FLD].ToString().Trim();
					}
					if (odrPCS[MST_PartyTable.NAME_FLD] != DBNull.Value)
					{
						objObject.Name = odrPCS[MST_PartyTable.NAME_FLD].ToString().Trim();
					}
					if (odrPCS[MST_PartyTable.ADDRESS_FLD] != DBNull.Value)
					{
						objObject.Address = odrPCS[MST_PartyTable.ADDRESS_FLD].ToString().Trim();
					}
					if (odrPCS[MST_PartyTable.WEBSITE_FLD] != DBNull.Value)
					{
						objObject.WebSite = odrPCS[MST_PartyTable.WEBSITE_FLD].ToString().Trim();
					}
					if (odrPCS[MST_PartyTable.STATE_FLD] != DBNull.Value)
					{
						objObject.State = odrPCS[MST_PartyTable.STATE_FLD].ToString().Trim();
					}
					try
					{
						objObject.DeleteReason = bool.Parse(odrPCS[MST_PartyTable.DELETEREASON_FLD].ToString().Trim());
					}
					catch
					{
						objObject.DeleteReason = false;
					}
					if (odrPCS[MST_PartyTable.TYPE_FLD] != DBNull.Value)
					{
						objObject.Type = int.Parse(odrPCS[MST_PartyTable.TYPE_FLD].ToString().Trim());
					}
					if (odrPCS[MST_PartyTable.ZIPPOST_FLD] != DBNull.Value)
					{
						objObject.ZipPost = odrPCS[MST_PartyTable.ZIPPOST_FLD].ToString().Trim();
					}
					if (odrPCS[MST_PartyTable.MAPBANKACCOUNTNAME_FLD] != DBNull.Value)
					{
						objObject.MAPBankAccountName = odrPCS[MST_PartyTable.MAPBANKACCOUNTNAME_FLD].ToString().Trim();
					}
					if (odrPCS[MST_PartyTable.MAPBANKACCOUNTNO_FLD] != DBNull.Value)
					{
						objObject.MAPBankAccountNo = odrPCS[MST_PartyTable.MAPBANKACCOUNTNO_FLD].ToString().Trim();
					}
					if (odrPCS[MST_PartyTable.VATCODE_FLD] != DBNull.Value)
					{
						objObject.VATCode = odrPCS[MST_PartyTable.VATCODE_FLD].ToString().Trim();
					}
					else
					{
						objObject.VATCode = string.Empty;
					}

					if (odrPCS[MST_PartyTable.COUNTRYID_FLD] != DBNull.Value)
					{
						objObject.CountryID = int.Parse(odrPCS[MST_PartyTable.COUNTRYID_FLD].ToString().Trim());
					}
					if (odrPCS[MST_PartyTable.CITYID_FLD] != DBNull.Value)
					{
						objObject.CityID = int.Parse(odrPCS[MST_PartyTable.CITYID_FLD].ToString().Trim());
					}					

					if (odrPCS[MST_PartyTable.PHONE_FLD] != DBNull.Value)
					{
						objObject.Phone = odrPCS[MST_PartyTable.PHONE_FLD].ToString().Trim();
					}

					if (odrPCS[MST_PartyTable.FAX_FLD] != DBNull.Value)
					{
						objObject.Fax = odrPCS[MST_PartyTable.FAX_FLD].ToString().Trim();
					}

					if (odrPCS[MST_PartyTable.BANKACCOUNT_FLD] != DBNull.Value)
					{
						objObject.BankAccount = odrPCS[MST_PartyTable.BANKACCOUNT_FLD].ToString().Trim();
					}
					if (odrPCS["PaymentTermID"] != DBNull.Value)
					{
						objObject.PaymentTermID = Convert.ToInt32(odrPCS["PaymentTermID"]);
					}
					if (odrPCS["CurrencyID"] != DBNull.Value)
					{
						objObject.CurrencyID = Convert.ToInt32(odrPCS["CurrencyID"]);
					}
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
		///       This method uses to get data from MST_Party
		///    </Description>
		///    <Inputs>
		///        Code      
		///    </Inputs>
		///    <Outputs>
		///       MST_PartyVO
		///    </Outputs>
		///    <Returns>
		///       MST_PartyVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Monday, February 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(string pstrCode)
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
					+ MST_PartyTable.PARTYID_FLD + ","
					+ MST_PartyTable.CODE_FLD + ","
					+ MST_PartyTable.NAME_FLD + ","
					+ MST_PartyTable.ADDRESS_FLD + ","
					+ MST_PartyTable.WEBSITE_FLD + ","
					+ MST_PartyTable.STATE_FLD + ","
					+ MST_PartyTable.DELETEREASON_FLD + ","
					+ MST_PartyTable.TYPE_FLD + ","
					+ MST_PartyTable.ZIPPOST_FLD + ","
					+ MST_PartyTable.VATCODE_FLD + ","
					+ MST_PartyTable.COUNTRYID_FLD + ","
					+ MST_PartyTable.PHONE_FLD + ", "
					+ MST_PartyTable.FAX_FLD + ", "
					+ MST_PartyTable.BANKACCOUNT_FLD + ", "
					+ MST_PartyTable.CITYID_FLD
					+ " FROM " + MST_PartyTable.TABLE_NAME
					+ " WHERE " + MST_PartyTable.CODE_FLD + "= '" + pstrCode + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_PartyVO objObject = new MST_PartyVO();

				while (odrPCS.Read())
				{
					objObject.PartyID = int.Parse(odrPCS[MST_PartyTable.PARTYID_FLD].ToString().Trim());
					objObject.Code = odrPCS[MST_PartyTable.CODE_FLD].ToString().Trim();
					objObject.Name = odrPCS[MST_PartyTable.NAME_FLD].ToString().Trim();
					objObject.Address = odrPCS[MST_PartyTable.ADDRESS_FLD].ToString().Trim();
					objObject.WebSite = odrPCS[MST_PartyTable.WEBSITE_FLD].ToString().Trim();
					objObject.State = odrPCS[MST_PartyTable.STATE_FLD].ToString().Trim();
					try
					{
						objObject.DeleteReason = bool.Parse(odrPCS[MST_PartyTable.DELETEREASON_FLD].ToString().Trim());
					}
					catch
					{
						objObject.DeleteReason = false;
					}
					try
					{
						objObject.Type = int.Parse(odrPCS[MST_PartyTable.TYPE_FLD].ToString().Trim());
					}
					catch
					{
						objObject.Type = 0;
					}
					
					objObject.Phone = odrPCS[MST_PartyTable.PHONE_FLD].ToString().Trim();
					objObject.Fax = odrPCS[MST_PartyTable.FAX_FLD].ToString().Trim();
					objObject.BankAccount = odrPCS[MST_PartyTable.BANKACCOUNT_FLD].ToString().Trim();

					objObject.ZipPost = odrPCS[MST_PartyTable.ZIPPOST_FLD].ToString().Trim();
					objObject.VATCode = odrPCS[MST_PartyTable.VATCODE_FLD].ToString().Trim();
					if (odrPCS[MST_PartyTable.COUNTRYID_FLD] != DBNull.Value)
						objObject.CountryID = int.Parse(odrPCS[MST_PartyTable.COUNTRYID_FLD].ToString().Trim());
					if (odrPCS[MST_PartyTable.CITYID_FLD] != DBNull.Value)
						objObject.CityID = int.Parse(odrPCS[MST_PartyTable.CITYID_FLD].ToString().Trim());
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
		///       This method uses to update data to MST_Party
		///    </Description>
		///    <Inputs>
		///       MST_PartyVO       
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
			MST_PartyVO objObject = (MST_PartyVO) pobjObjecVO;

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE MST_Party SET "
					+ MST_PartyTable.CODE_FLD + "=   ?" + ","
					+ MST_PartyTable.NAME_FLD + "=   ?" + ","
					+ MST_PartyTable.ADDRESS_FLD + "=   ?" + ","
					+ MST_PartyTable.WEBSITE_FLD + "=   ?" + ","
					+ MST_PartyTable.STATE_FLD + "=   ?" + ","
					+ MST_PartyTable.DELETEREASON_FLD + "=   ?" + ","
					+ MST_PartyTable.TYPE_FLD + "=   ?" + ","
					+ MST_PartyTable.ZIPPOST_FLD + "=   ?" + ","
					+ MST_PartyTable.MAPBANKACCOUNTNAME_FLD + "=   ?" + ","
					+ MST_PartyTable.MAPBANKACCOUNTNO_FLD + "=   ?" + ","
					+ MST_PartyTable.VATCODE_FLD + "=   ?" + ","
					+ MST_PartyTable.COUNTRYID_FLD + "=   ?" + ","
					+ MST_PartyTable.PHONE_FLD + " =?, "
					+ MST_PartyTable.FAX_FLD + " =?, "
					+ MST_PartyTable.BANKACCOUNT_FLD + " =?, "
					+ MST_PartyTable.CITYID_FLD + "=  ?,"
					+ " CurrencyID =  ?,"
					+ " PaymentTermID =  ?"
					+ " WHERE " + MST_PartyTable.PARTYID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.ADDRESS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.ADDRESS_FLD].Value = objObject.Address;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.WEBSITE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.WEBSITE_FLD].Value = objObject.WebSite;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.STATE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.STATE_FLD].Value = objObject.State;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.DELETEREASON_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_PartyTable.DELETEREASON_FLD].Value = objObject.DeleteReason;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.ZIPPOST_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.ZIPPOST_FLD].Value = objObject.ZipPost;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.MAPBANKACCOUNTNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.MAPBANKACCOUNTNAME_FLD].Value = objObject.MAPBankAccountName;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.MAPBANKACCOUNTNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.MAPBANKACCOUNTNO_FLD].Value = objObject.MAPBankAccountNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.VATCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.VATCODE_FLD].Value = objObject.VATCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.COUNTRYID_FLD, OleDbType.Integer));
				if (objObject.CountryID > 0)
					ocmdPCS.Parameters[MST_PartyTable.COUNTRYID_FLD].Value = objObject.CountryID;
				else
					ocmdPCS.Parameters[MST_PartyTable.COUNTRYID_FLD].Value = DBNull.Value;
				
				//Begin_ Added by Tuan TQ -2005-09-22
				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.PHONE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.PHONE_FLD].Value = objObject.Phone;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.FAX_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.FAX_FLD].Value = objObject.Fax;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.BANKACCOUNT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PartyTable.BANKACCOUNT_FLD].Value = objObject.BankAccount;
				//End_ Added by Tuan TQ -2005-09-22

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.CITYID_FLD, OleDbType.Integer));
				if (objObject.CityID > 0)
					ocmdPCS.Parameters[MST_PartyTable.CITYID_FLD].Value = objObject.CityID;
				else
					ocmdPCS.Parameters[MST_PartyTable.CITYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter("CurrencyID", OleDbType.Integer));
				if (objObject.CurrencyID > 0)
					ocmdPCS.Parameters["CurrencyID"].Value = objObject.CurrencyID;
				else
					ocmdPCS.Parameters["CurrencyID"].Value = DBNull.Value;
				
				ocmdPCS.Parameters.Add(new OleDbParameter("PaymentTermID", OleDbType.Integer));
				if (objObject.PaymentTermID > 0)
					ocmdPCS.Parameters["PaymentTermID"].Value = objObject.PaymentTermID;
				else
					ocmdPCS.Parameters["PaymentTermID"].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PartyTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PartyTable.PARTYID_FLD].Value = objObject.PartyID;

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
		///       This method uses to get all data from MST_Party
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
		///       Monday, February 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetCustomerInfo(int pintPartyID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ MST_PartyTable.PARTYID_FLD + ","
					+ MST_PartyTable.CODE_FLD + ","
					+ MST_PartyTable.NAME_FLD
					+ " FROM " + MST_PartyTable.TABLE_NAME
					+ " WHERE " + MST_PartyTable.PARTYID_FLD + "=" + pintPartyID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_PartyTable.TABLE_NAME);

				return dstPCS.Tables[0];
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
		///       This method uses to get all data from MST_Party
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
		///       Monday, February 14, 2005
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
					+ MST_PartyTable.PARTYID_FLD + ","
					+ MST_PartyTable.CODE_FLD + ","
					+ MST_PartyTable.NAME_FLD + ","
					+ MST_PartyTable.ADDRESS_FLD + ","
					+ MST_PartyTable.WEBSITE_FLD + ","
					+ MST_PartyTable.STATE_FLD + ","
					+ MST_PartyTable.DELETEREASON_FLD + ","
					+ MST_PartyTable.TYPE_FLD + ","
					+ MST_PartyTable.ZIPPOST_FLD + ","
					+ MST_PartyTable.VATCODE_FLD + ","
					+ MST_PartyTable.COUNTRYID_FLD + ","
					+ MST_PartyTable.PHONE_FLD + ", "
					+ MST_PartyTable.FAX_FLD + ", "
					+ MST_PartyTable.BANKACCOUNT_FLD + ", "
					+ MST_PartyTable.CITYID_FLD
					+ " FROM " + MST_PartyTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_PartyTable.TABLE_NAME);

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
		///       This method uses to get all data from MST_Party
		///    </Description>
		///    <Inputs>
		///       pstrQueryString : string        
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
		public DataSet List(string pstrQueryString)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ MST_PartyTable.PARTYID_FLD + ","
					+ MST_PartyTable.CODE_FLD + ","
					+ MST_PartyTable.NAME_FLD + ","
					+ MST_PartyTable.ADDRESS_FLD + ","
					+ MST_PartyTable.WEBSITE_FLD + ","
					+ MST_PartyTable.STATE_FLD + ","
					+ MST_PartyTable.DELETEREASON_FLD + ","
					+ MST_PartyTable.TYPE_FLD + ","
					+ MST_PartyTable.ZIPPOST_FLD + ","
					+ MST_PartyTable.CITYID_FLD + ","
					+ MST_PartyTable.PHONE_FLD + ", "
					+ MST_PartyTable.FAX_FLD + ", "
					+ MST_PartyTable.BANKACCOUNT_FLD + ", "
					+ MST_PartyTable.VATCODE_FLD
					+ " FROM " + MST_PartyTable.TABLE_NAME
					+ " WHERE " + pstrQueryString;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_PartyTable.TABLE_NAME);

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
		///       This method uses to get PartyID
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_PartyVO
		///    </Outputs>
		///    <Returns>
		///       MST_PartyVO
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
		public int GetPartyID(string pstrCode)
		{
			const string METHOD_NAME = THIS + ".GetPartyID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ MST_PartyTable.PARTYID_FLD
					+ " FROM " + MST_PartyTable.TABLE_NAME
					+ " WHERE " + MST_PartyTable.CODE_FLD + "='" + pstrCode + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objReturnValue = ocmdPCS.ExecuteScalar(); //				.ToString().Trim();
				if (objReturnValue == null)
				{
					return 0;
				}
				else
				{
					string strID = objReturnValue.ToString();
					if (strID == String.Empty)
					{
						return 0;
					}
					else
					{
						return int.Parse(strID);
					}
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
		///       This method uses to get PartyName
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_PartyVO
		///    </Outputs>
		///    <Returns>
		///       MST_PartyVO
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
		public string GetPartyCode(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetPartyName()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ MST_PartyTable.CODE_FLD
					+ " FROM " + MST_PartyTable.TABLE_NAME
					+ " WHERE " + MST_PartyTable.PARTYID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
				{
					return objResult.ToString().Trim();
					
				}
				else
				{
					return String.Empty;
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
		

		public string GetPartyCodeAndName(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetPartyCodeAndName()";
			const string SEPARATE_STRING = "##";

			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ MST_PartyTable.CODE_FLD + " + '" + SEPARATE_STRING + "' + " + MST_PartyTable.NAME_FLD
					+ " FROM " + MST_PartyTable.TABLE_NAME
					+ " WHERE " + MST_PartyTable.PARTYID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
				{
					return objResult.ToString().Trim();
					
				}
				else
				{
					return String.Empty;
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
		///       Monday, February 14, 2005
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
					+ MST_PartyTable.PARTYID_FLD + ","
					+ MST_PartyTable.CODE_FLD + ","
					+ MST_PartyTable.NAME_FLD + ","
					+ MST_PartyTable.ADDRESS_FLD + ","
					+ MST_PartyTable.WEBSITE_FLD + ","
					+ MST_PartyTable.STATE_FLD + ","
					+ MST_PartyTable.DELETEREASON_FLD + ","
					+ MST_PartyTable.TYPE_FLD + ","
					+ MST_PartyTable.ZIPPOST_FLD + ","
					+ MST_PartyTable.VATCODE_FLD + ","
					+ MST_PartyTable.COUNTRYID_FLD + ","
					+ MST_PartyTable.PHONE_FLD + ", "
					+ MST_PartyTable.FAX_FLD + ", "
					+ MST_PartyTable.BANKACCOUNT_FLD + ", "
					+ MST_PartyTable.CITYID_FLD
					+ "  FROM " + MST_PartyTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, MST_PartyTable.TABLE_NAME);

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

		/// <summary>
		/// Get Party Type data from enum table
		/// </summary>
		/// <returns></returns>
		public DataTable GetPartyType()
		{
			const string METHOD_NAME = THIS + ".GetPartyType()";
			DataTable dtbResultTable = new DataTable(enm_PartyTypeEnumTable.TABLE_NAME);

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT [Value],";

				strSql += " [Name],";
				strSql += " [Description]";
				strSql += " FROM enm_PartyTypeEnum";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);
				
				return dtbResultTable;
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

		public DataTable ListVendor()
		{
			const string METHOD_NAME = THIS + ".ListVendor()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ MST_PartyTable.PARTYID_FLD + ","
					+ MST_PartyTable.CODE_FLD + ","
					+ MST_PartyTable.NAME_FLD + ","
					+ MST_PartyTable.ADDRESS_FLD + ","
					+ MST_PartyTable.WEBSITE_FLD + ","
					+ MST_PartyTable.STATE_FLD + ","
					+ MST_PartyTable.DELETEREASON_FLD + ","
					+ MST_PartyTable.TYPE_FLD + ","
					+ MST_PartyTable.ZIPPOST_FLD + ","
					+ MST_PartyTable.VATCODE_FLD + ","
					+ MST_PartyTable.COUNTRYID_FLD + ","
					+ MST_PartyTable.PHONE_FLD + ", "
					+ MST_PartyTable.FAX_FLD + ", "
					+ MST_PartyTable.BANKACCOUNT_FLD + ", "
					+ MST_PartyTable.CITYID_FLD
					+ " FROM " + MST_PartyTable.TABLE_NAME
					+ " WHERE " + MST_PartyTable.TYPE_FLD + " IN (" + (int)PartyTypeEnum.BOTH + "," + (int)PartyTypeEnum.VENDOR 
					// sonht bo xung outside
					+ "," + (int)PartyTypeEnum.OUTSIDE + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_PartyTable.TABLE_NAME);

				return dstPCS.Tables[0];
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

		public DataRow GetVendorInfo(int pintVendorLocationID)
		{
			const string METHOD_NAME = THIS + ".GetVendorInfo()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT MST_Party.Code AS MST_PartyCode, MST_Party.Name, MST_PartyLocation.Code AS MST_PartyLocationCode"
					+ " FROM MST_Party JOIN MST_PartyLocation ON MST_Party.PartyID = MST_PartyLocation.PartyID"
					+ " WHERE MST_PartyLocation.PartyLocationID = " + pintVendorLocationID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable();
				odadPCS.Fill(dtbData);

				return dtbData.Rows[0];
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