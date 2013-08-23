using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_CurrencyDS 
	{
		public MST_CurrencyDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_CurrencyDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_Currency
		///    </Description>
		///    <Inputs>
		///        MST_CurrencyVO       
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
				MST_CurrencyVO objObject = (MST_CurrencyVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_Currency("
				+ MST_CurrencyTable.CODE_FLD + ","
				+ MST_CurrencyTable.NAME_FLD + ","
				+ MST_CurrencyTable.MASK_FLD + ")"
				+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CurrencyTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CurrencyTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CurrencyTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CurrencyTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CurrencyTable.MASK_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CurrencyTable.MASK_FLD].Value = objObject.Mask;


				
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
		///       This method uses to delete data from MST_Currency
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
			strSql=	"DELETE " + MST_CurrencyTable.TABLE_NAME + " WHERE  " + "CurrencyID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_Currency
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_CurrencyVO
		///    </Outputs>
		///    <Returns>
		///       MST_CurrencyVO
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
				+ MST_CurrencyTable.CURRENCYID_FLD + ","
				+ MST_CurrencyTable.CODE_FLD + ","
				+ MST_CurrencyTable.NAME_FLD + ","
				+ MST_CurrencyTable.MASK_FLD
				+ " FROM " + MST_CurrencyTable.TABLE_NAME
				+" WHERE " + MST_CurrencyTable.CURRENCYID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_CurrencyVO objObject = new MST_CurrencyVO();

				while (odrPCS.Read())
				{ 
				objObject.CurrencyID = int.Parse(odrPCS[MST_CurrencyTable.CURRENCYID_FLD].ToString().Trim());
				objObject.Code = odrPCS[MST_CurrencyTable.CODE_FLD].ToString().Trim();
				objObject.Name = odrPCS[MST_CurrencyTable.NAME_FLD].ToString().Trim();
				objObject.Mask = odrPCS[MST_CurrencyTable.MASK_FLD].ToString().Trim();

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
		///       This method uses to update data to MST_Currency
		///    </Description>
		///    <Inputs>
		///       MST_CurrencyVO       
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

			MST_CurrencyVO objObject = (MST_CurrencyVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_Currency SET "
				+ MST_CurrencyTable.CODE_FLD + "=   ?" + ","
				+ MST_CurrencyTable.NAME_FLD + "=   ?" + ","
				+ MST_CurrencyTable.MASK_FLD + "=  ?"
				+" WHERE " + MST_CurrencyTable.CURRENCYID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CurrencyTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CurrencyTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CurrencyTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CurrencyTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CurrencyTable.MASK_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_CurrencyTable.MASK_FLD].Value = objObject.Mask;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_CurrencyTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_CurrencyTable.CURRENCYID_FLD].Value = objObject.CurrencyID;


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
		///       This method uses to get all data from MST_Currency
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
				+ MST_CurrencyTable.CURRENCYID_FLD + ","
				+ MST_CurrencyTable.CODE_FLD + ","
				+ MST_CurrencyTable.NAME_FLD + ","
				+ MST_CurrencyTable.MASK_FLD
					+ " FROM " + MST_CurrencyTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_CurrencyTable.TABLE_NAME);

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
				+ MST_CurrencyTable.CURRENCYID_FLD + ","
				+ MST_CurrencyTable.CODE_FLD + ","
				+ MST_CurrencyTable.NAME_FLD + ","
				+ MST_CurrencyTable.MASK_FLD 
		+ "  FROM " + MST_CurrencyTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_CurrencyTable.TABLE_NAME);

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
		///  Get home currency of CCN
		/// </summary>
		/// <param name="pintCCNID">CCN ID</param>
		/// <returns>Home Currency Code</returns>
		public string GetCurrencyByCCN(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetCurrencyByCCN()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT Code FROM MST_Currency WHERE CurrencyID = "
					+ " (SELECT HomeCurrencyID FROM MST_CCN WHERE CCNID = " + pintCCNID + ")";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				return ocmdPCS.ExecuteScalar().ToString();
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
		public object GetUSDCurrency()
		{
			const string METHOD_NAME = THIS + ".GetUSDCurrency()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT CurrencyID, Code, Name, Mask FROM MST_Currency WHERE Code = 'USD'";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataReader odrdPCS = ocmdPCS.ExecuteReader();
				MST_CurrencyVO voCurrency = null;
				if (odrdPCS.HasRows)
				{
					voCurrency = new MST_CurrencyVO();
					while (odrdPCS.Read())
					{
						voCurrency.CurrencyID = Convert.ToInt32(odrdPCS["CurrencyID"].ToString());
						voCurrency.Code = odrdPCS["Code"].ToString();
						voCurrency.Name = odrdPCS["Name"].ToString();
						voCurrency.Mask = odrdPCS["Mask"].ToString();
					}
				}
				return voCurrency;
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
