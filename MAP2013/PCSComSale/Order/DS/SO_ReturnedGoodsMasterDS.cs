using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComSale.Order.DS
{
	
	public class SO_ReturnedGoodsMasterDS 
	{
		public SO_ReturnedGoodsMasterDS()
		{
		}
		private const string THIS = "PCSComSale.Order.DS.SO_ReturnedGoodsMasterDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_ReturnedGoodsMaster
		///    </Description>
		///    <Inputs>
		///        SO_ReturnedGoodsMasterVO       
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
		///       Tuesday, February 22, 2005
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
				SO_ReturnedGoodsMasterVO objObject = (SO_ReturnedGoodsMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO SO_ReturnedGoodsMaster("
				+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD + ","
				+ SO_ReturnedGoodsMasterTable.RECEIVERID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.CCNID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.TRANSDATE_FLD + ","
				+ SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD + ","
				+ SO_ReturnedGoodsMasterTable.POSTDATE_FLD + ","
				+ SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.PARTYID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD].Value = objObject.ReturnedGoodsNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.RECEIVERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RECEIVERID_FLD].Value = objObject.ReceiverID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.TRANSDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.TRANSDATE_FLD].Value = objObject.TransDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;


				
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
		///       This method uses to add data to SO_ReturnedGoodsMaster
		///    </Description>
		///    <Inputs>
		///        SO_ReturnedGoodsMasterVO       
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
		///       Tuesday, February 22, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


		public int AddReturnedGoodsAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddReturnedGoodsAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				SO_ReturnedGoodsMasterVO objObject = (SO_ReturnedGoodsMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO SO_ReturnedGoodsMaster("
					+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD + ","
					+ SO_ReturnedGoodsMasterTable.RECEIVERID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.CCNID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TRANSDATE_FLD + ","
					+ SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD + ","
					+ SO_ReturnedGoodsMasterTable.POSTDATE_FLD + ","
					+ SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.PARTYID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.CURRENCYID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.EXCHANGERATE_FLD + ","
					+ SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)";

				//get the latest inserted new ID
				strSql += " ; Select @@IDENTITY as NEWID";
				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD].Value = objObject.ReturnedGoodsNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.RECEIVERID_FLD, OleDbType.Integer));
				if (objObject.ReceiverID > 0)
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RECEIVERID_FLD].Value = objObject.ReceiverID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RECEIVERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.CCNID_FLD, OleDbType.Integer));
				if (objObject.CCNID > 0)
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.CCNID_FLD].Value = objObject.CCNID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.CCNID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.TRANSDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.TRANSDATE_FLD].Value = objObject.TransDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.POSTDATE_FLD, OleDbType.Date));
				
				if (objObject.PostDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.POSTDATE_FLD].Value = objObject.PostDate;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.POSTDATE_FLD].Value = DBNull.Value;
				}
				

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				if (objObject.SaleOrderMasterID > 0)
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.PARTYID_FLD, OleDbType.Integer));
				if (objObject.PartyID > 0)
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYID_FLD].Value = objObject.PartyID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				if (objObject.PartyContactID > 0) 
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.MasterLocationID > 0) 
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD].Value = DBNull.Value;
				}
				
				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.PartyLocationID > 0) 
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD].Value = DBNull.Value;
				}
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				//insert and return new id
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
				}				
				else if (ex.Errors[1].NativeError == ErrorCode.SQLDBNULL_VIALATION_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DBNULL_VIALATION,METHOD_NAME,ex);
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
		///       This method uses to delete data from SO_ReturnedGoodsMaster
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
			strSql=	"DELETE " + SO_ReturnedGoodsMasterTable.TABLE_NAME + " WHERE  " + "ReturnedGoodsMasterID" + "=" + pintID.ToString();
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
		///       This method uses to get data from SO_ReturnedGoodsMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_ReturnedGoodsMasterVO
		///    </Outputs>
		///    <Returns>
		///       SO_ReturnedGoodsMasterVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 22, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetReturnedGoodsMasterVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetReturnedGoodsMasterVO()";
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD + ","
					+ SO_ReturnedGoodsMasterTable.RECEIVERID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.CCNID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TRANSDATE_FLD + ","
					+ SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD + ","
					+ SO_ReturnedGoodsMasterTable.POSTDATE_FLD + ","
					+ SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.PARTYID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD
					+ " FROM " + SO_ReturnedGoodsMasterTable.TABLE_NAME
					+" WHERE " + SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_ReturnedGoodsMasterVO objObject = new SO_ReturnedGoodsMasterVO();

				while (odrPCS.Read())
				{ 
					objObject.ReturnedGoodsMasterID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD].ToString().Trim());
					objObject.ReturnedGoodsNumber = odrPCS[SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD].ToString().Trim();
					if (odrPCS[SO_ReturnedGoodsMasterTable.RECEIVERID_FLD] != DBNull.Value && odrPCS[SO_ReturnedGoodsMasterTable.RECEIVERID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.ReceiverID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.RECEIVERID_FLD].ToString().Trim());
					}
					if (odrPCS[SO_ReturnedGoodsMasterTable.CCNID_FLD] != DBNull.Value && odrPCS[SO_ReturnedGoodsMasterTable.CCNID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.CCNID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.CCNID_FLD].ToString().Trim());
					}
					if (odrPCS[SO_ReturnedGoodsMasterTable.TRANSDATE_FLD] != DBNull.Value && odrPCS[SO_ReturnedGoodsMasterTable.TRANSDATE_FLD].ToString().Trim() != String.Empty)
					{
						objObject.TransDate = DateTime.Parse(odrPCS[SO_ReturnedGoodsMasterTable.TRANSDATE_FLD].ToString().Trim());
					}
					objObject.Description = odrPCS[SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[SO_ReturnedGoodsMasterTable.POSTDATE_FLD] != DBNull.Value && odrPCS[SO_ReturnedGoodsMasterTable.POSTDATE_FLD].ToString().Trim() != String.Empty)
					{
						objObject.PostDate = DateTime.Parse(odrPCS[SO_ReturnedGoodsMasterTable.POSTDATE_FLD].ToString().Trim());
					}
					if (odrPCS[SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD] != DBNull.Value && odrPCS[SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.SaleOrderMasterID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].ToString().Trim());
					}
					if (odrPCS[SO_ReturnedGoodsMasterTable.PARTYID_FLD] != DBNull.Value && odrPCS[SO_ReturnedGoodsMasterTable.PARTYID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.PartyID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.PARTYID_FLD].ToString().Trim());
					}
					if (odrPCS[SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD] != DBNull.Value && odrPCS[SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.PartyContactID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD].ToString().Trim());
					}
					if (odrPCS[SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD] != DBNull.Value && odrPCS[SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.MasterLocationID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD].ToString().Trim());
					}

					if (odrPCS[SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD] != DBNull.Value && odrPCS[SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.PartyLocationID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD].ToString().Trim());
					}

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
		///       This method uses to get data from SO_ReturnedGoodsMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_ReturnedGoodsMasterVO
		///    </Outputs>
		///    <Returns>
		///       SO_ReturnedGoodsMasterVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 22, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public string GetReturnedGoodsMasterIDByNumber(string pstrReturnedGoodsNumber)
		{
			const string METHOD_NAME = THIS + ".GetReturnedGoodsMasterIDByNumber()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;

				strSql=	"SELECT "
					+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD  
					+ " FROM " + SO_ReturnedGoodsMasterTable.TABLE_NAME
					+ " WHERE " + SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD + "=?" ;


				Utils utils = new Utils();

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);


				ocmdPCS.Connection.Open();

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD].Value = pstrReturnedGoodsNumber;

				object objReturnValue  = null;
				objReturnValue = ocmdPCS.ExecuteScalar();
				if (objReturnValue != null)
				{
					return objReturnValue.ToString();
				}
				else
				{
					return String.Empty;
				}

				

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
		///       This method uses to get data from SO_ReturnedGoodsMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_ReturnedGoodsMasterVO
		///    </Outputs>
		///    <Returns>
		///       SO_ReturnedGoodsMasterVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 22, 2005
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
				+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD + ","
				+ SO_ReturnedGoodsMasterTable.RECEIVERID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.CCNID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.TRANSDATE_FLD + ","
				+ SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD + ","
				+ SO_ReturnedGoodsMasterTable.POSTDATE_FLD + ","
				+ SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.PARTYID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD
				+ " FROM " + SO_ReturnedGoodsMasterTable.TABLE_NAME
				+" WHERE " + SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_ReturnedGoodsMasterVO objObject = new SO_ReturnedGoodsMasterVO();

				while (odrPCS.Read())
				{ 
				objObject.ReturnedGoodsMasterID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD].ToString().Trim());
				objObject.ReturnedGoodsNumber = odrPCS[SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD].ToString().Trim();
				objObject.ReceiverID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.RECEIVERID_FLD].ToString().Trim());
				objObject.CCNID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.CCNID_FLD].ToString().Trim());
				objObject.TransDate = DateTime.Parse(odrPCS[SO_ReturnedGoodsMasterTable.TRANSDATE_FLD].ToString().Trim());
				objObject.Description = odrPCS[SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD].ToString().Trim();
				objObject.PostDate = DateTime.Parse(odrPCS[SO_ReturnedGoodsMasterTable.POSTDATE_FLD].ToString().Trim());
				try
				{
					objObject.SaleOrderMasterID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.SaleOrderMasterID = 0;
				}
				try
				{
					objObject.PartyID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.PARTYID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.PartyID = 0;
				}
				try
				{
					objObject.PartyContactID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.PartyContactID = 0;
				}
				try
				{
					objObject.MasterLocationID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.MasterLocationID = 0;
				}
				try
				{
					objObject.PartyLocationID = int.Parse(odrPCS[SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.PartyLocationID = 0;
				}

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
		///       This method uses to update data to SO_ReturnedGoodsMaster
		///    </Description>
		///    <Inputs>
		///       SO_ReturnedGoodsMasterVO       
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

			SO_ReturnedGoodsMasterVO objObject = (SO_ReturnedGoodsMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE SO_ReturnedGoodsMaster SET "
				+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD + "=   ?" + ","
				+ SO_ReturnedGoodsMasterTable.RECEIVERID_FLD + "=   ?" + ","
				+ SO_ReturnedGoodsMasterTable.CCNID_FLD + "=   ?" + ","
				+ SO_ReturnedGoodsMasterTable.TRANSDATE_FLD + "=   ?" + ","
				+ SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD + "=   ?" + ","
				+ SO_ReturnedGoodsMasterTable.POSTDATE_FLD + "=   ?" + ","
				+ SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD + "=   ?" + ","
				+ SO_ReturnedGoodsMasterTable.PARTYID_FLD + "=   ?" + ","
				+ SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD + "=   ?" + ","
				+ SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD + "=  ?" + ","
				+ SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD + "=  ?"
				+" WHERE " + SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD].Value = objObject.ReturnedGoodsNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.RECEIVERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RECEIVERID_FLD].Value = objObject.ReceiverID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.TRANSDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.TRANSDATE_FLD].Value = objObject.TransDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD].Value = objObject.ReturnedGoodsMasterID;


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
		///       This method uses to update data to SO_ReturnedGoodsMaster
		///    </Description>
		///    <Inputs>
		///       SO_ReturnedGoodsMasterVO       
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
		
	
		public void UpdateReturnedGoods(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".UpdateReturnedGoods()";

			SO_ReturnedGoodsMasterVO objObject = (SO_ReturnedGoodsMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE SO_ReturnedGoodsMaster SET "
					+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsMasterTable.RECEIVERID_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsMasterTable.CCNID_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsMasterTable.TRANSDATE_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsMasterTable.POSTDATE_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsMasterTable.PARTYID_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD + "=  ?"
					+" WHERE " + SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD].Value = objObject.ReturnedGoodsNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.RECEIVERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RECEIVERID_FLD].Value = DBNull.Value;

				/*
				if (objObject.ReceiverID > 0)
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RECEIVERID_FLD].Value = objObject.ReceiverID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RECEIVERID_FLD].Value = DBNull.Value;
				}
				*/

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.CCNID_FLD, OleDbType.Integer));
				if (objObject.CCNID > 0) 
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.CCNID_FLD].Value = objObject.CCNID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.CCNID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.TRANSDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.TRANSDATE_FLD].Value = objObject.TransDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.POSTDATE_FLD, OleDbType.Date));
				if (objObject.PostDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.POSTDATE_FLD].Value = objObject.PostDate;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.POSTDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				if (objObject.SaleOrderMasterID > 0)
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.PARTYID_FLD, OleDbType.Integer));
				if (objObject.PartyID > 0)
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYID_FLD].Value = objObject.PartyID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				if (objObject.PartyContactID > 0) 
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.MasterLocationID > 0)
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD].Value = DBNull.Value;
				}


				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.PartyLocationID > 0)
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;
				}
				else
				{
					ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD].Value = objObject.ReturnedGoodsMasterID;


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
		///       This method uses to get all data from SO_ReturnedGoodsMaster
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
		///       Tuesday, February 22, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataTable GetReturnGoodsMaster(int pintReturnedGoodsID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.RECEIVERID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.CCNID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.TRANSDATE_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.CURRENCYID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.EXCHANGERATE_FLD + ","
					+ " CUR." + MST_CurrencyTable.CODE_FLD + " " + MST_CurrencyTable.TABLE_NAME + MST_CurrencyTable.CODE_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.POSTDATE_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.PARTYID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD + "," 
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CODE_FLD + " " + SO_SaleOrderMasterTable.TABLE_NAME + SO_SaleOrderMasterTable.CODE_FLD + "," 
					+ MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.CODE_FLD + " "  + MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD + ","
					+ MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.NAME_FLD + " "  + MST_PartyTable.TABLE_NAME + MST_PartyTable.NAME_FLD
					+ " FROM " + SO_ReturnedGoodsMasterTable.TABLE_NAME
					+ " left join " + SO_SaleOrderMasterTable.TABLE_NAME + " on " 
					+					SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD 
					+					"=" 
					+					SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD 
					+ " Left join " + MST_PartyTable.TABLE_NAME + " on " 
					+					SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.PARTYID_FLD 
					+					"=" 
					+					MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.PARTYID_FLD
					+ " left join " + MST_CurrencyTable.TABLE_NAME + " CUR ON CUR." + MST_CurrencyTable.CURRENCYID_FLD
					+ " = " + SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.CURRENCYID_FLD
					+ " WHERE " + SO_ReturnedGoodsMasterTable.TABLE_NAME + "." + SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD + "=" + pintReturnedGoodsID; 
 

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_ReturnedGoodsMasterTable.TABLE_NAME);

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
		///       This method uses to get all data from SO_ReturnedGoodsMaster
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
		///       Tuesday, February 22, 2005
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
				+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD + ","
				+ SO_ReturnedGoodsMasterTable.RECEIVERID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.CCNID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.TRANSDATE_FLD + ","
				+ SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD + ","
				+ SO_ReturnedGoodsMasterTable.POSTDATE_FLD + ","
				+ SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.PARTYID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD
					+ " FROM " + SO_ReturnedGoodsMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_ReturnedGoodsMasterTable.TABLE_NAME);

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
		///       Tuesday, February 22, 2005
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
				+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD + ","
				+ SO_ReturnedGoodsMasterTable.RECEIVERID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.CCNID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.TRANSDATE_FLD + ","
				+ SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD + ","
				+ SO_ReturnedGoodsMasterTable.POSTDATE_FLD + ","
				+ SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.PARTYID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD + ","
				+ SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD 
		+ "  FROM " + SO_ReturnedGoodsMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,SO_ReturnedGoodsMasterTable.TABLE_NAME);

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
	}
}
