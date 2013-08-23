using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_ExchangeRateDS 
	{
		public MST_ExchangeRateDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_ExchangeRateDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_ExchangeRate
		///    </Description>
		///    <Inputs>
		///        MST_ExchangeRateVO       
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
				MST_ExchangeRateVO objObject = (MST_ExchangeRateVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_ExchangeRate("
				+ MST_ExchangeRateTable.CODE_FLD + ","
				+ MST_ExchangeRateTable.RATE_FLD + ","
				+ MST_ExchangeRateTable.DESCRIPTION_FLD + ","
				+ MST_ExchangeRateTable.APPROVED_FLD + ","
				+ MST_ExchangeRateTable.APPROVALDATE_FLD + ","
				+ MST_ExchangeRateTable.BEGINDATE_FLD + ","
				+ MST_ExchangeRateTable.ENDDATE_FLD + ","
				+ MST_ExchangeRateTable.CCNID_FLD + ","
				+ MST_ExchangeRateTable.CURRENCYID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_ExchangeRateTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.RATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_ExchangeRateTable.RATE_FLD].Value = objObject.Rate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_ExchangeRateTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.APPROVED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_ExchangeRateTable.APPROVED_FLD].Value = objObject.Approved;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.APPROVALDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_ExchangeRateTable.APPROVALDATE_FLD].Value = objObject.ApprovalDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.BEGINDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_ExchangeRateTable.BEGINDATE_FLD].Value = objObject.BeginDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.ENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_ExchangeRateTable.ENDDATE_FLD].Value = objObject.EndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_ExchangeRateTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_ExchangeRateTable.CURRENCYID_FLD].Value = objObject.CurrencyID;


				
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
		///       This method uses to delete data from MST_ExchangeRate
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
			strSql=	"DELETE " + MST_ExchangeRateTable.TABLE_NAME + " WHERE  " + "ExchangeRateID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_ExchangeRate
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_ExchangeRateVO
		///    </Outputs>
		///    <Returns>
		///       MST_ExchangeRateVO
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
				+ MST_ExchangeRateTable.EXCHANGERATEID_FLD + ","
				+ MST_ExchangeRateTable.CODE_FLD + ","
				+ MST_ExchangeRateTable.RATE_FLD + ","
				+ MST_ExchangeRateTable.DESCRIPTION_FLD + ","
				+ MST_ExchangeRateTable.APPROVED_FLD + ","
				+ MST_ExchangeRateTable.APPROVALDATE_FLD + ","
				+ MST_ExchangeRateTable.BEGINDATE_FLD + ","
				+ MST_ExchangeRateTable.ENDDATE_FLD + ","
				+ MST_ExchangeRateTable.CCNID_FLD + ","
				+ MST_ExchangeRateTable.CURRENCYID_FLD
				+ " FROM " + MST_ExchangeRateTable.TABLE_NAME
				+" WHERE " + MST_ExchangeRateTable.EXCHANGERATEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_ExchangeRateVO objObject = new MST_ExchangeRateVO();

				while (odrPCS.Read())
				{ 
				objObject.ExchangeRateID = int.Parse(odrPCS[MST_ExchangeRateTable.EXCHANGERATEID_FLD].ToString().Trim());
				objObject.Code = odrPCS[MST_ExchangeRateTable.CODE_FLD].ToString().Trim();
				objObject.Rate = Decimal.Parse(odrPCS[MST_ExchangeRateTable.RATE_FLD].ToString().Trim());
				objObject.Description = odrPCS[MST_ExchangeRateTable.DESCRIPTION_FLD].ToString().Trim();
				objObject.Approved = bool.Parse(odrPCS[MST_ExchangeRateTable.APPROVED_FLD].ToString().Trim());
				objObject.ApprovalDate = DateTime.Parse(odrPCS[MST_ExchangeRateTable.APPROVALDATE_FLD].ToString().Trim());
				objObject.BeginDate = DateTime.Parse(odrPCS[MST_ExchangeRateTable.BEGINDATE_FLD].ToString().Trim());
				objObject.EndDate = DateTime.Parse(odrPCS[MST_ExchangeRateTable.ENDDATE_FLD].ToString().Trim());
				objObject.CCNID = int.Parse(odrPCS[MST_ExchangeRateTable.CCNID_FLD].ToString().Trim());
				objObject.CurrencyID = int.Parse(odrPCS[MST_ExchangeRateTable.CURRENCYID_FLD].ToString().Trim());

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
		///       This method uses to get data from MST_ExchangeRate
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_ExchangeRateVO
		///    </Outputs>
		///    <Returns>
		///       MST_ExchangeRateVO
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
		public object GetExchangeRate(int pintCurrencyID,DateTime pdtmOrderDate)
		{
			const string METHOD_NAME = THIS + ".GetExchangeRate()";
			const string YYYYMMDD = "yyyyMMdd";
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ MST_ExchangeRateTable.EXCHANGERATEID_FLD + ","
					+ MST_ExchangeRateTable.CODE_FLD + ","
					+ MST_ExchangeRateTable.RATE_FLD + ","
					+ MST_ExchangeRateTable.DESCRIPTION_FLD + ","
					+ MST_ExchangeRateTable.APPROVED_FLD + ","
					+ MST_ExchangeRateTable.APPROVALDATE_FLD + ","
					+ MST_ExchangeRateTable.BEGINDATE_FLD + ","
					+ MST_ExchangeRateTable.ENDDATE_FLD + ","
					+ MST_ExchangeRateTable.CCNID_FLD + ","
					+ MST_ExchangeRateTable.CURRENCYID_FLD
					+ " FROM " + MST_ExchangeRateTable.TABLE_NAME
					+ " WHERE " + MST_ExchangeRateTable.CURRENCYID_FLD + "=" + pintCurrencyID
					+ " AND " + MST_ExchangeRateTable.APPROVED_FLD + "=1 "
					+ " AND DATEDIFF(dayofyear," + MST_ExchangeRateTable.BEGINDATE_FLD + ",'" + pdtmOrderDate.ToString(YYYYMMDD) + "') >= 0"
					+ " AND DATEDIFF(dayofyear," + MST_ExchangeRateTable.ENDDATE_FLD + ",'" + pdtmOrderDate.ToString(YYYYMMDD) + "') <= 0";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_ExchangeRateVO objObject = new MST_ExchangeRateVO();

				while (odrPCS.Read())
				{ 
					objObject.ExchangeRateID = int.Parse(odrPCS[MST_ExchangeRateTable.EXCHANGERATEID_FLD].ToString().Trim());
					objObject.Code = odrPCS[MST_ExchangeRateTable.CODE_FLD].ToString().Trim();
					objObject.Rate = Decimal.Parse(odrPCS[MST_ExchangeRateTable.RATE_FLD].ToString().Trim());
					objObject.Description = odrPCS[MST_ExchangeRateTable.DESCRIPTION_FLD].ToString().Trim();
					objObject.Approved = bool.Parse(odrPCS[MST_ExchangeRateTable.APPROVED_FLD].ToString().Trim());
					objObject.ApprovalDate = DateTime.Parse(odrPCS[MST_ExchangeRateTable.APPROVALDATE_FLD].ToString().Trim());
					objObject.BeginDate = DateTime.Parse(odrPCS[MST_ExchangeRateTable.BEGINDATE_FLD].ToString().Trim());
					objObject.EndDate = DateTime.Parse(odrPCS[MST_ExchangeRateTable.ENDDATE_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[MST_ExchangeRateTable.CCNID_FLD].ToString().Trim());
					objObject.CurrencyID = int.Parse(odrPCS[MST_ExchangeRateTable.CURRENCYID_FLD].ToString().Trim());

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
		///       This method uses to get data from MST_ExchangeRate
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_ExchangeRateVO
		///    </Outputs>
		///    <Returns>
		///       MST_ExchangeRateVO
		///    </Returns>
		///    <Authors>
		///       SonHT
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetLastExchangeRate(int pintCurrencyID,DateTime pdtmOrderDate)
		{
			const string METHOD_NAME = THIS + ".GetExchangeRate()";
			const string YYYYMMDD = "yyyyMMdd";
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT " + MST_ExchangeRateTable.RATE_FLD
					+ " FROM " + MST_ExchangeRateTable.TABLE_NAME
					+ " WHERE " + MST_ExchangeRateTable.CURRENCYID_FLD + "=" + pintCurrencyID
					+ " AND " + MST_ExchangeRateTable.APPROVED_FLD + "=1 "
					+ " AND DATEDIFF(dayofyear," + MST_ExchangeRateTable.BEGINDATE_FLD + ",'" + pdtmOrderDate.ToString(YYYYMMDD) + "') >= 0"
					+ " AND DATEDIFF(dayofyear," + MST_ExchangeRateTable.ENDDATE_FLD + ",'" + pdtmOrderDate.ToString(YYYYMMDD) + "') <= 0"
					+ " ORDER BY " + MST_ExchangeRateTable.EXCHANGERATEID_FLD + " DESC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{
					return Convert.ToDecimal(odrPCS[MST_ExchangeRateTable.RATE_FLD]);
				}		
				return 1;
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
		///       This method uses to update data to MST_ExchangeRate
		///    </Description>
		///    <Inputs>
		///       MST_ExchangeRateVO       
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

			MST_ExchangeRateVO objObject = (MST_ExchangeRateVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_ExchangeRate SET "
				+ MST_ExchangeRateTable.CODE_FLD + "=   ?" + ","
				+ MST_ExchangeRateTable.RATE_FLD + "=   ?" + ","
				+ MST_ExchangeRateTable.DESCRIPTION_FLD + "=   ?" + ","
				+ MST_ExchangeRateTable.APPROVED_FLD + "=   ?" + ","
				+ MST_ExchangeRateTable.APPROVALDATE_FLD + "=   ?" + ","
				+ MST_ExchangeRateTable.BEGINDATE_FLD + "=   ?" + ","
				+ MST_ExchangeRateTable.ENDDATE_FLD + "=   ?" + ","
				+ MST_ExchangeRateTable.CCNID_FLD + "=   ?" + ","
				+ MST_ExchangeRateTable.CURRENCYID_FLD + "=  ?"
				+" WHERE " + MST_ExchangeRateTable.EXCHANGERATEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_ExchangeRateTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.RATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_ExchangeRateTable.RATE_FLD].Value = objObject.Rate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_ExchangeRateTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.APPROVED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_ExchangeRateTable.APPROVED_FLD].Value = objObject.Approved;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.APPROVALDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_ExchangeRateTable.APPROVALDATE_FLD].Value = objObject.ApprovalDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.BEGINDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_ExchangeRateTable.BEGINDATE_FLD].Value = objObject.BeginDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.ENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_ExchangeRateTable.ENDDATE_FLD].Value = objObject.EndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_ExchangeRateTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_ExchangeRateTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ExchangeRateTable.EXCHANGERATEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_ExchangeRateTable.EXCHANGERATEID_FLD].Value = objObject.ExchangeRateID;


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
		///       This method uses to get all data from MST_ExchangeRate
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
				+ MST_ExchangeRateTable.EXCHANGERATEID_FLD + ","
				+ MST_ExchangeRateTable.CODE_FLD + ","
				+ MST_ExchangeRateTable.RATE_FLD + ","
				+ MST_ExchangeRateTable.DESCRIPTION_FLD + ","
				+ MST_ExchangeRateTable.APPROVED_FLD + ","
				+ MST_ExchangeRateTable.APPROVALDATE_FLD + ","
				+ MST_ExchangeRateTable.BEGINDATE_FLD + ","
				+ MST_ExchangeRateTable.ENDDATE_FLD + ","
				+ MST_ExchangeRateTable.CCNID_FLD + ","
				+ MST_ExchangeRateTable.CURRENCYID_FLD
					+ " FROM " + MST_ExchangeRateTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_ExchangeRateTable.TABLE_NAME);

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
				+ MST_ExchangeRateTable.EXCHANGERATEID_FLD + ","
				+ MST_ExchangeRateTable.CODE_FLD + ","
				+ MST_ExchangeRateTable.RATE_FLD + ","
				+ MST_ExchangeRateTable.DESCRIPTION_FLD + ","
				+ MST_ExchangeRateTable.APPROVED_FLD + ","
				+ MST_ExchangeRateTable.APPROVALDATE_FLD + ","
				+ MST_ExchangeRateTable.BEGINDATE_FLD + ","
				+ MST_ExchangeRateTable.ENDDATE_FLD + ","
				+ MST_ExchangeRateTable.CCNID_FLD + ","
				+ MST_ExchangeRateTable.CURRENCYID_FLD 
		+ "  FROM " + MST_ExchangeRateTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_ExchangeRateTable.TABLE_NAME);

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
		/// List all exchange rate of CCN
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <returns></returns>
		public DataTable List(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ MST_ExchangeRateTable.EXCHANGERATEID_FLD + ","
					+ MST_ExchangeRateTable.CODE_FLD + ","
					+ MST_ExchangeRateTable.RATE_FLD + ","
					+ MST_ExchangeRateTable.DESCRIPTION_FLD + ","
					+ MST_ExchangeRateTable.APPROVED_FLD + ","
					+ MST_ExchangeRateTable.APPROVALDATE_FLD + ","
					+ MST_ExchangeRateTable.BEGINDATE_FLD + ","
					+ MST_ExchangeRateTable.ENDDATE_FLD + ","
					+ MST_ExchangeRateTable.CCNID_FLD + ","
					+ MST_ExchangeRateTable.CURRENCYID_FLD
					+ " FROM " + MST_ExchangeRateTable.TABLE_NAME
					+ " WHERE " + MST_ExchangeRateTable.CCNID_FLD + "=" + pintCCNID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_ExchangeRateTable.TABLE_NAME);

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

	}
}
