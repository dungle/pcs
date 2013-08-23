using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_CCNDS 
	{
		public MST_CCNDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_CCNDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_CCN
		///    </Description>
		///    <Inputs>
		///        MST_CCNVO       
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
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				MST_CCNVO objObject = (MST_CCNVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_CCN("
				+ MST_CCNTable.CODE_FLD + ","
				+ MST_CCNTable.DESCRIPTION_FLD + ","
				+ MST_CCNTable.NAME_FLD + ","
				+ MST_CCNTable.STATE_FLD + ","
				+ MST_CCNTable.ZIPCODE_FLD + ","
				+ MST_CCNTable.PHONE_FLD + ","
				+ MST_CCNTable.FAX_FLD + ","
				+ MST_CCNTable.WEBSITE_FLD + ","
				+ MST_CCNTable.EMAIL_FLD + ","
				+ MST_CCNTable.VAT_FLD + ","
				+ MST_CCNTable.COUNTRYID_FLD + ","
				+ MST_CCNTable.CITYID_FLD + ","
				+ MST_CCNTable.HOMECURRENCYID_FLD + ","
				+ MST_CCNTable.EXCHANGERATE_FLD + ","
				+ MST_CCNTable.DEFAULTCURRENCYID_FLD + ","
				+ MST_CCNTable.EXCHANGERATEOPERATOR_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.STATE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.STATE_FLD].Value = objObject.State;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.ZIPCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.ZIPCODE_FLD].Value = objObject.ZipCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.PHONE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.PHONE_FLD].Value = objObject.Phone;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.FAX_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.FAX_FLD].Value = objObject.Fax;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.WEBSITE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.WEBSITE_FLD].Value = objObject.WebSite;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.EMAIL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.EMAIL_FLD].Value = objObject.Email;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.VAT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_CCNTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.COUNTRYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_CCNTable.COUNTRYID_FLD].Value = objObject.CountryID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.CITYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_CCNTable.CITYID_FLD].Value = objObject.CityID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.HOMECURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_CCNTable.HOMECURRENCYID_FLD].Value = objObject.HomeCurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_CCNTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.DEFAULTCURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_CCNTable.DEFAULTCURRENCYID_FLD].Value = objObject.DefaultCurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.EXCHANGERATEOPERATOR_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.EXCHANGERATEOPERATOR_FLD].Value = objObject.ExchangeRateOperator;


				
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
	

	

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from MST_CCN
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
			strSql=	"DELETE " + MST_CCNTable.TABLE_NAME + " WHERE  " + "CCNID" + "=" + pintID.ToString();
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
	

	

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from MST_CCN
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_CCNVO
		///    </Outputs>
		///    <Returns>
		///       MST_CCNVO
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
				strSql=	"SELECT "
				+ MST_CCNTable.CCNID_FLD + ","
				+ MST_CCNTable.CODE_FLD + ","
				+ MST_CCNTable.DESCRIPTION_FLD + ","
				+ MST_CCNTable.NAME_FLD + ","
				+ MST_CCNTable.STATE_FLD + ","
				+ MST_CCNTable.ZIPCODE_FLD + ","
				+ MST_CCNTable.PHONE_FLD + ","
				+ MST_CCNTable.FAX_FLD + ","
				+ MST_CCNTable.WEBSITE_FLD + ","
				+ MST_CCNTable.EMAIL_FLD + ","
				+ MST_CCNTable.VAT_FLD + ","
				+ MST_CCNTable.COUNTRYID_FLD + ","
				+ MST_CCNTable.CITYID_FLD + ","
				+ MST_CCNTable.HOMECURRENCYID_FLD + ","
				+ MST_CCNTable.EXCHANGERATE_FLD + ","
				+ MST_CCNTable.DEFAULTCURRENCYID_FLD + ","
				+ MST_CCNTable.EXCHANGERATEOPERATOR_FLD
				+ " FROM " + MST_CCNTable.TABLE_NAME
				+" WHERE " + MST_CCNTable.CCNID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_CCNVO objObject = new MST_CCNVO();

				while (odrPCS.Read())
				{ 
					objObject.CCNID = int.Parse(odrPCS[MST_CCNTable.CCNID_FLD].ToString().Trim());
					objObject.Code = odrPCS[MST_CCNTable.CODE_FLD].ToString().Trim();
					objObject.Description = odrPCS[MST_CCNTable.DESCRIPTION_FLD].ToString().Trim();
					objObject.Name = odrPCS[MST_CCNTable.NAME_FLD].ToString().Trim();
					objObject.State = odrPCS[MST_CCNTable.STATE_FLD].ToString().Trim();
					objObject.ZipCode = odrPCS[MST_CCNTable.ZIPCODE_FLD].ToString().Trim();
					objObject.Phone = odrPCS[MST_CCNTable.PHONE_FLD].ToString().Trim();
					objObject.Fax = odrPCS[MST_CCNTable.FAX_FLD].ToString().Trim();
					objObject.WebSite = odrPCS[MST_CCNTable.WEBSITE_FLD].ToString().Trim();
					objObject.Email = odrPCS[MST_CCNTable.EMAIL_FLD].ToString().Trim();
					objObject.VAT = odrPCS[MST_CCNTable.VAT_FLD].ToString().Trim();
					// HACK: dungla 10-28-2005
					try
					{
						objObject.CountryID = int.Parse(odrPCS[MST_CCNTable.COUNTRYID_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.CityID = int.Parse(odrPCS[MST_CCNTable.CITYID_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.HomeCurrencyID = int.Parse(odrPCS[MST_CCNTable.HOMECURRENCYID_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.ExchangeRate = float.Parse(odrPCS[MST_CCNTable.EXCHANGERATE_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.DefaultCurrencyID = int.Parse(odrPCS[MST_CCNTable.DEFAULTCURRENCYID_FLD].ToString().Trim());
					}
					catch{}
					// END: dungla 10-28-2005
					objObject.ExchangeRateOperator = odrPCS[MST_CCNTable.EXCHANGERATEOPERATOR_FLD].ToString().Trim();
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from MST_CCN
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_CCNVO
		///    </Outputs>
		///    <Returns>
		///       MST_CCNVO
		///    </Returns>
		///    <Authors>
		///       Sonht
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetObjectVO(string pstrUserName)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ MST_CCNTable.TABLE_NAME + "." + MST_CCNTable.CCNID_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.CODE_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.DESCRIPTION_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.NAME_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.STATE_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.ZIPCODE_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.PHONE_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.FAX_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.WEBSITE_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.EMAIL_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.VAT_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.COUNTRYID_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.CITYID_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.HOMECURRENCYID_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.EXCHANGERATE_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.DEFAULTCURRENCYID_FLD + ","
					+ MST_CCNTable.TABLE_NAME + "."+ MST_CCNTable.EXCHANGERATEOPERATOR_FLD
					+ " FROM " + MST_CCNTable.TABLE_NAME
					+ " INNER JOIN " + Sys_UserTable.TABLE_NAME + " ON "
					+ MST_CCNTable.TABLE_NAME + "." + MST_CCNTable.CCNID_FLD + " = " + Sys_UserTable.TABLE_NAME + "." + Sys_UserTable.CCNID_FLD
					+" WHERE " + Sys_UserTable.USERNAME_FLD + "='" + pstrUserName.Replace("'","''") + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_CCNVO objObject = new MST_CCNVO();

				while (odrPCS.Read())
				{ 
					objObject.CCNID = int.Parse(odrPCS[MST_CCNTable.CCNID_FLD].ToString().Trim());
					objObject.Code = odrPCS[MST_CCNTable.CODE_FLD].ToString().Trim();
					objObject.Description = odrPCS[MST_CCNTable.DESCRIPTION_FLD].ToString().Trim();
					objObject.Name = odrPCS[MST_CCNTable.NAME_FLD].ToString().Trim();
					objObject.State = odrPCS[MST_CCNTable.STATE_FLD].ToString().Trim();
					objObject.ZipCode = odrPCS[MST_CCNTable.ZIPCODE_FLD].ToString().Trim();
					objObject.Phone = odrPCS[MST_CCNTable.PHONE_FLD].ToString().Trim();
					objObject.Fax = odrPCS[MST_CCNTable.FAX_FLD].ToString().Trim();
					objObject.WebSite = odrPCS[MST_CCNTable.WEBSITE_FLD].ToString().Trim();
					objObject.Email = odrPCS[MST_CCNTable.EMAIL_FLD].ToString().Trim();
					if(odrPCS[MST_CCNTable.VAT_FLD] != DBNull.Value)
						objObject.VAT = odrPCS[MST_CCNTable.VAT_FLD].ToString().Trim();
					if(odrPCS[MST_CCNTable.COUNTRYID_FLD] != DBNull.Value)
						objObject.CountryID = int.Parse(odrPCS[MST_CCNTable.COUNTRYID_FLD].ToString().Trim());
					if(odrPCS[MST_CCNTable.CITYID_FLD] != DBNull.Value)
						objObject.CityID = int.Parse(odrPCS[MST_CCNTable.CITYID_FLD].ToString().Trim());
					if(odrPCS[MST_CCNTable.HOMECURRENCYID_FLD] != DBNull.Value)
						objObject.HomeCurrencyID = int.Parse(odrPCS[MST_CCNTable.HOMECURRENCYID_FLD].ToString().Trim());
					if(odrPCS[MST_CCNTable.EXCHANGERATE_FLD] != DBNull.Value)
						objObject.ExchangeRate = float.Parse(odrPCS[MST_CCNTable.EXCHANGERATE_FLD].ToString().Trim());
					if(odrPCS[MST_CCNTable.DEFAULTCURRENCYID_FLD] != DBNull.Value)
						objObject.DefaultCurrencyID = int.Parse(odrPCS[MST_CCNTable.DEFAULTCURRENCYID_FLD].ToString().Trim());
					objObject.ExchangeRateOperator = odrPCS[MST_CCNTable.EXCHANGERATEOPERATOR_FLD].ToString().Trim();

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to MST_CCN
		///    </Description>
		///    <Inputs>
		///       MST_CCNVO       
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

			MST_CCNVO objObject = (MST_CCNVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_CCN SET "
				+ MST_CCNTable.CODE_FLD + "=   ?" + ","
				+ MST_CCNTable.DESCRIPTION_FLD + "=   ?" + ","
				+ MST_CCNTable.NAME_FLD + "=   ?" + ","
				+ MST_CCNTable.STATE_FLD + "=   ?" + ","
				+ MST_CCNTable.ZIPCODE_FLD + "=   ?" + ","
				+ MST_CCNTable.PHONE_FLD + "=   ?" + ","
				+ MST_CCNTable.FAX_FLD + "=   ?" + ","
				+ MST_CCNTable.WEBSITE_FLD + "=   ?" + ","
				+ MST_CCNTable.EMAIL_FLD + "=   ?" + ","
				+ MST_CCNTable.VAT_FLD + "=   ?" + ","
				+ MST_CCNTable.COUNTRYID_FLD + "=   ?" + ","
				+ MST_CCNTable.CITYID_FLD + "=   ?" + ","
				+ MST_CCNTable.HOMECURRENCYID_FLD + "=   ?" + ","
				+ MST_CCNTable.EXCHANGERATE_FLD + "=   ?" + ","
				+ MST_CCNTable.DEFAULTCURRENCYID_FLD + "=   ?" + ","
				+ MST_CCNTable.EXCHANGERATEOPERATOR_FLD + "=  ?"
				+" WHERE " + MST_CCNTable.CCNID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.STATE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.STATE_FLD].Value = objObject.State;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.ZIPCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.ZIPCODE_FLD].Value = objObject.ZipCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.PHONE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.PHONE_FLD].Value = objObject.Phone;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.FAX_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.FAX_FLD].Value = objObject.Fax;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.WEBSITE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.WEBSITE_FLD].Value = objObject.WebSite;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.EMAIL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.EMAIL_FLD].Value = objObject.Email;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.VAT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_CCNTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.COUNTRYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_CCNTable.COUNTRYID_FLD].Value = objObject.CountryID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.CITYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_CCNTable.CITYID_FLD].Value = objObject.CityID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.HOMECURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_CCNTable.HOMECURRENCYID_FLD].Value = objObject.HomeCurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_CCNTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.DEFAULTCURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_CCNTable.DEFAULTCURRENCYID_FLD].Value = objObject.DefaultCurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.EXCHANGERATEOPERATOR_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CCNTable.EXCHANGERATEOPERATOR_FLD].Value = objObject.ExchangeRateOperator;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CCNTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_CCNTable.CCNID_FLD].Value = objObject.CCNID;


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


		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from MST_CCN
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
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
				+ MST_CCNTable.CCNID_FLD + ","
				+ MST_CCNTable.CODE_FLD + ","
				+ MST_CCNTable.DESCRIPTION_FLD + ","
				+ MST_CCNTable.NAME_FLD + ","
				+ MST_CCNTable.STATE_FLD + ","
				+ MST_CCNTable.ZIPCODE_FLD + ","
				+ MST_CCNTable.PHONE_FLD + ","
				+ MST_CCNTable.FAX_FLD + ","
				+ MST_CCNTable.WEBSITE_FLD + ","
				+ MST_CCNTable.EMAIL_FLD + ","
				+ MST_CCNTable.VAT_FLD + ","
				+ MST_CCNTable.COUNTRYID_FLD + ","
				+ MST_CCNTable.CITYID_FLD + ","
				+ MST_CCNTable.HOMECURRENCYID_FLD + ","
				+ MST_CCNTable.EXCHANGERATE_FLD + ","
				+ MST_CCNTable.DEFAULTCURRENCYID_FLD + ","
				+ MST_CCNTable.EXCHANGERATEOPERATOR_FLD
					+ " FROM " + MST_CCNTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_CCNTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from MST_CCN
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

		public DataTable ListAllCCN()
		{
			const string METHOD_NAME = THIS + ".ListAllCCN()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ MST_CCNTable.CCNID_FLD + ","
					+ MST_CCNTable.CODE_FLD
					+ " FROM " + MST_CCNTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_CCNTable.TABLE_NAME);

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



		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from MST_CCN
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

		public DataSet ListCCNForCombo()
		{
			const string METHOD_NAME = THIS + ".ListCCNForCombo()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ MST_CCNTable.CCNID_FLD + ","
					+ MST_CCNTable.CODE_FLD + ","
					+ MST_CCNTable.DESCRIPTION_FLD 
					+ " FROM " + MST_CCNTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_CCNTable.TABLE_NAME);

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
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
				+ MST_CCNTable.CCNID_FLD + ","
				+ MST_CCNTable.CODE_FLD + ","
				+ MST_CCNTable.DESCRIPTION_FLD + ","
				+ MST_CCNTable.NAME_FLD + ","
				+ MST_CCNTable.STATE_FLD + ","
				+ MST_CCNTable.ZIPCODE_FLD + ","
				+ MST_CCNTable.PHONE_FLD + ","
				+ MST_CCNTable.FAX_FLD + ","
				+ MST_CCNTable.WEBSITE_FLD + ","
				+ MST_CCNTable.EMAIL_FLD + ","
				+ MST_CCNTable.VAT_FLD + ","
				+ MST_CCNTable.COUNTRYID_FLD + ","
				+ MST_CCNTable.CITYID_FLD + ","
				+ MST_CCNTable.HOMECURRENCYID_FLD + ","
				+ MST_CCNTable.EXCHANGERATE_FLD + ","
				+ MST_CCNTable.DEFAULTCURRENCYID_FLD + ","
				+ MST_CCNTable.EXCHANGERATEOPERATOR_FLD 
		+ "  FROM " + MST_CCNTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_CCNTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get CCN Code from ID
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       string
		///    </Outputs>
		///    <Returns>
		///       string
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       17-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public string GetCCNCodeFromID(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetCCNCodeFromID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ MST_CCNTable.CODE_FLD
					+ " FROM " + MST_CCNTable.TABLE_NAME
					+ " WHERE " + MST_CCNTable.CCNID_FLD + "=" + pintCCNID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataSet dstData = new DataSet();
				odadPCS.Fill(dstData, MST_CCNTable.TABLE_NAME);
				
				string strCCNCode = string.Empty;
				if (dstData.Tables.Count > 0)
				{
					if (dstData.Tables[0].Rows.Count > 0)
					{
						strCCNCode = dstData.Tables[0].Rows[0][0].ToString().Trim();
					}
				}
				
				return strCCNCode;
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
	}
}
