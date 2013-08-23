using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_TransactionHistoryDS 
	{
		public MST_TransactionHistoryDS()
		{
		}

		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_TransactionHistoryDS";

		/// <summary>
		/// This method uses to add data to MST_TransactionHistory
		/// </summary>
		/// <param name="pobjObjectVO">MST_TransactionHistoryVO</param>
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				MST_TransactionHistoryVO objObject = (MST_TransactionHistoryVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO " + MST_TransactionHistoryTable.TABLE_NAME + "("
					+ MST_TransactionHistoryTable.MASTERLOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.BINID_FLD + ","
					+ MST_TransactionHistoryTable.BUYSELLCOST_FLD + ","
					+ MST_TransactionHistoryTable.INSPSTATUS_FLD + ","
					+ MST_TransactionHistoryTable.TRANSDATE_FLD + ","
					+ MST_TransactionHistoryTable.POSTDATE_FLD + ","
					+ MST_TransactionHistoryTable.REFMASTERID_FLD + ","
					+ MST_TransactionHistoryTable.REFDETAILID_FLD + ","
					+ MST_TransactionHistoryTable.COST_FLD + ","
					+ MST_TransactionHistoryTable.CCNID_FLD + ","
					+ MST_TransactionHistoryTable.TRANTYPEID_FLD + ","
					+ MST_TransactionHistoryTable.PARTYID_FLD + ","
					+ MST_TransactionHistoryTable.PARTYLOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.PRODUCTID_FLD + ","
					+ MST_TransactionHistoryTable.STOCKUMID_FLD + ","
					+ MST_TransactionHistoryTable.CURRENCYID_FLD + ","
					+ MST_TransactionHistoryTable.QUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.BINOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.MASLOCCOMMITQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONCOMMITQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.BINCOMMITQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.COMMENT_FLD + ","
					+ MST_TransactionHistoryTable.EXCHANGERATE_FLD + ","
					+ MST_TransactionHistoryTable.LOT_FLD + ","
					+ MST_TransactionHistoryTable.SERIAL_FLD + ","
					+ MST_TransactionHistoryTable.OLDAVGCOST_FLD + ","
					+ MST_TransactionHistoryTable.ISSUEPUROSEID_FLD + ","
					+ MST_TransactionHistoryTable.USERNAME_FLD + ","
					+ MST_TransactionHistoryTable.NEWAVGCOST_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.MasterLocationID > 0)
					ocmdPCS.Parameters[MST_TransactionHistoryTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;
				else
					ocmdPCS.Parameters[MST_TransactionHistoryTable.MASTERLOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.BINID_FLD, OleDbType.Integer));
				if (objObject.BinID > 0)
					ocmdPCS.Parameters[MST_TransactionHistoryTable.BINID_FLD].Value = objObject.BinID;
				else
					ocmdPCS.Parameters[MST_TransactionHistoryTable.BINID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.BUYSELLCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.BUYSELLCOST_FLD].Value = objObject.BuySellCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.INSPSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.INSPSTATUS_FLD].Value = objObject.InspStatus;

				//ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD, OleDbType.Integer));
				//ocmdPCS.Parameters[MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD].Value = objObject.TransactionHistoryID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.TRANSDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.TRANSDATE_FLD].Value = objObject.TransDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.REFMASTERID_FLD, OleDbType.Integer));
				if (objObject.RefMasterID > 0)
					ocmdPCS.Parameters[MST_TransactionHistoryTable.REFMASTERID_FLD].Value = objObject.RefMasterID;
				else
					ocmdPCS.Parameters[MST_TransactionHistoryTable.REFMASTERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.REFDETAILID_FLD, OleDbType.Integer));
				if (objObject.RefDetailID > 0)
					ocmdPCS.Parameters[MST_TransactionHistoryTable.REFDETAILID_FLD].Value = objObject.RefDetailID;
				else
					ocmdPCS.Parameters[MST_TransactionHistoryTable.REFDETAILID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.COST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.COST_FLD].Value = objObject.Cost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.TRANTYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.TRANTYPEID_FLD].Value = objObject.TranTypeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.PARTYID_FLD, OleDbType.Integer));
				if (objObject.PartyID > 0)
					ocmdPCS.Parameters[MST_TransactionHistoryTable.PARTYID_FLD].Value = objObject.PartyID;
				else
					ocmdPCS.Parameters[MST_TransactionHistoryTable.PARTYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.PartyLocationID > 0)
					ocmdPCS.Parameters[MST_TransactionHistoryTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;
				else
					ocmdPCS.Parameters[MST_TransactionHistoryTable.PARTYLOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.LOCATIONID_FLD, OleDbType.Integer));
				if (objObject.LocationID > 0)
					ocmdPCS.Parameters[MST_TransactionHistoryTable.LOCATIONID_FLD].Value = objObject.LocationID;
				else
					ocmdPCS.Parameters[MST_TransactionHistoryTable.LOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.PRODUCTID_FLD, OleDbType.Integer));
				if (objObject.ProductID > 0)
					ocmdPCS.Parameters[MST_TransactionHistoryTable.PRODUCTID_FLD].Value = objObject.ProductID;
				else
					ocmdPCS.Parameters[MST_TransactionHistoryTable.PRODUCTID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.STOCKUMID_FLD, OleDbType.Integer));
				if (objObject.StockUMID > 0)
					ocmdPCS.Parameters[MST_TransactionHistoryTable.STOCKUMID_FLD].Value = objObject.StockUMID;
				else
					ocmdPCS.Parameters[MST_TransactionHistoryTable.STOCKUMID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.CURRENCYID_FLD, OleDbType.Integer));
				if (objObject.CurrencyID > 0)
					ocmdPCS.Parameters[MST_TransactionHistoryTable.CURRENCYID_FLD].Value = objObject.CurrencyID;
				else
					ocmdPCS.Parameters[MST_TransactionHistoryTable.CURRENCYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD].Value = objObject.MasLocOHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD].Value = objObject.LocationOHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.BINOHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.BINOHQUANTITY_FLD].Value = objObject.BinOHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.MASLOCCOMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.MASLOCCOMMITQUANTITY_FLD].Value = objObject.MasLocCommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.LOCATIONCOMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.LOCATIONCOMMITQUANTITY_FLD].Value = objObject.LocationCommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.BINCOMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.BINCOMMITQUANTITY_FLD].Value = objObject.BinCommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.COMMENT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.OLDAVGCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.OLDAVGCOST_FLD].Value = objObject.OldAvgCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.ISSUEPUROSEID_FLD, OleDbType.Integer));
				if (objObject.PurposeID > 0)
					ocmdPCS.Parameters[MST_TransactionHistoryTable.ISSUEPUROSEID_FLD].Value = objObject.PurposeID;
				else
					ocmdPCS.Parameters[MST_TransactionHistoryTable.ISSUEPUROSEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.USERNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.USERNAME_FLD].Value = objObject.Username;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.NEWAVGCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.NEWAVGCOST_FLD].Value = objObject.NewAvgCost;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
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

		///    <summary>
		///       This method uses to add data to MST_TransactionHistory
		///    </summary>
		///    <Inputs>
		///        MST_TransactionHistoryVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 30, 2005
		///    </History>
		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + MST_TransactionHistoryTable.TABLE_NAME + " WHERE  " + "MasterLocationID" + "=" + pintID.ToString();
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

		///    <summary>
		///       This method uses to add data to MST_TransactionHistory
		///    </summary>
		///    <Inputs>
		///        MST_TransactionHistoryVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 30, 2005
		///    </History>
		public object GetObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			//DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ MST_TransactionHistoryTable.MASTERLOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.BINID_FLD + ","
					+ MST_TransactionHistoryTable.BUYSELLCOST_FLD + ","
					+ MST_TransactionHistoryTable.INSPSTATUS_FLD + ","
					+ MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD + ","
					+ MST_TransactionHistoryTable.TRANSDATE_FLD + ","
					+ MST_TransactionHistoryTable.POSTDATE_FLD + ","
					+ MST_TransactionHistoryTable.REFMASTERID_FLD + ","
					+ MST_TransactionHistoryTable.REFDETAILID_FLD + ","
					+ MST_TransactionHistoryTable.COST_FLD + ","
					+ MST_TransactionHistoryTable.CCNID_FLD + ","
					+ MST_TransactionHistoryTable.TRANTYPEID_FLD + ","
					+ MST_TransactionHistoryTable.PARTYID_FLD + ","
					+ MST_TransactionHistoryTable.PARTYLOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.PRODUCTID_FLD + ","
					+ MST_TransactionHistoryTable.STOCKUMID_FLD + ","
					+ MST_TransactionHistoryTable.CURRENCYID_FLD + ","
					+ MST_TransactionHistoryTable.QUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.BINOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.MASLOCCOMMITQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONCOMMITQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.BINCOMMITQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.COMMENT_FLD + ","
					+ MST_TransactionHistoryTable.EXCHANGERATE_FLD + ","
					+ MST_TransactionHistoryTable.LOT_FLD + ","
					+ MST_TransactionHistoryTable.SERIAL_FLD + ","
					+ MST_TransactionHistoryTable.OLDAVGCOST_FLD + ","
					+ MST_TransactionHistoryTable.USERNAME_FLD + ","
					+ MST_TransactionHistoryTable.NEWAVGCOST_FLD
					+ " FROM " + MST_TransactionHistoryTable.TABLE_NAME
					+ " WHERE " + MST_TransactionHistoryTable.MASTERLOCATIONID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_TransactionHistoryVO objObject = new MST_TransactionHistoryVO();

				while (odrPCS.Read())
				{
					objObject.MasterLocationID = int.Parse(odrPCS[MST_TransactionHistoryTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.BinID = int.Parse(odrPCS[MST_TransactionHistoryTable.BINID_FLD].ToString().Trim());
					objObject.BuySellCost = Decimal.Parse(odrPCS[MST_TransactionHistoryTable.BUYSELLCOST_FLD].ToString().Trim());
					objObject.InspStatus = int.Parse(odrPCS[MST_TransactionHistoryTable.INSPSTATUS_FLD].ToString().Trim());
					objObject.TransactionHistoryID = int.Parse(odrPCS[MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD].ToString().Trim());
					objObject.TransDate = DateTime.Parse(odrPCS[MST_TransactionHistoryTable.TRANSDATE_FLD].ToString().Trim());
					objObject.PostDate = DateTime.Parse(odrPCS[MST_TransactionHistoryTable.POSTDATE_FLD].ToString().Trim());
					objObject.RefMasterID = int.Parse(odrPCS[MST_TransactionHistoryTable.REFMASTERID_FLD].ToString().Trim());
					objObject.RefDetailID = int.Parse(odrPCS[MST_TransactionHistoryTable.REFDETAILID_FLD].ToString().Trim());
					objObject.Cost = Decimal.Parse(odrPCS[MST_TransactionHistoryTable.COST_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[MST_TransactionHistoryTable.CCNID_FLD].ToString().Trim());
					objObject.TranTypeID = int.Parse(odrPCS[MST_TransactionHistoryTable.TRANTYPEID_FLD].ToString().Trim());
					objObject.PartyID = int.Parse(odrPCS[MST_TransactionHistoryTable.PARTYID_FLD].ToString().Trim());
					objObject.PartyLocationID = int.Parse(odrPCS[MST_TransactionHistoryTable.PARTYLOCATIONID_FLD].ToString().Trim());
					objObject.LocationID = int.Parse(odrPCS[MST_TransactionHistoryTable.LOCATIONID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[MST_TransactionHistoryTable.PRODUCTID_FLD].ToString().Trim());
					objObject.StockUMID = int.Parse(odrPCS[MST_TransactionHistoryTable.STOCKUMID_FLD].ToString().Trim());
					objObject.CurrencyID = int.Parse(odrPCS[MST_TransactionHistoryTable.CURRENCYID_FLD].ToString().Trim());
					objObject.Quantity = Decimal.Parse(odrPCS[MST_TransactionHistoryTable.QUANTITY_FLD].ToString().Trim());
					objObject.MasLocOHQuantity = Decimal.Parse(odrPCS[MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD].ToString().Trim());
					objObject.LocationOHQuantity = Decimal.Parse(odrPCS[MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD].ToString().Trim());
					objObject.BinOHQuantity = Decimal.Parse(odrPCS[MST_TransactionHistoryTable.BINOHQUANTITY_FLD].ToString().Trim());
					objObject.MasLocCommitQuantity = Decimal.Parse(odrPCS[MST_TransactionHistoryTable.MASLOCCOMMITQUANTITY_FLD].ToString().Trim());
					objObject.LocationCommitQuantity = Decimal.Parse(odrPCS[MST_TransactionHistoryTable.LOCATIONCOMMITQUANTITY_FLD].ToString().Trim());
					objObject.BinCommitQuantity = Decimal.Parse(odrPCS[MST_TransactionHistoryTable.BINCOMMITQUANTITY_FLD].ToString().Trim());
					objObject.Comment = odrPCS[MST_TransactionHistoryTable.COMMENT_FLD].ToString().Trim();
					objObject.ExchangeRate = Decimal.Parse(odrPCS[MST_TransactionHistoryTable.EXCHANGERATE_FLD].ToString().Trim());
					objObject.Lot = odrPCS[MST_TransactionHistoryTable.LOT_FLD].ToString().Trim();
					objObject.Serial = odrPCS[MST_TransactionHistoryTable.SERIAL_FLD].ToString().Trim();
					objObject.OldAvgCost = Decimal.Parse(odrPCS[MST_TransactionHistoryTable.OLDAVGCOST_FLD].ToString().Trim());
					objObject.NewAvgCost = Decimal.Parse(odrPCS[MST_TransactionHistoryTable.NEWAVGCOST_FLD].ToString().Trim());
					objObject.Username = odrPCS[MST_TransactionHistoryTable.USERNAME_FLD].ToString().Trim();
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

		///    <summary>
		///       This method uses to add data to MST_TransactionHistory
		///    </summary>
		///    <Inputs>
		///        MST_TransactionHistoryVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 30, 2005
		///    </History>
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			MST_TransactionHistoryVO objObject = (MST_TransactionHistoryVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE MST_TransactionHistory SET "
					+ MST_TransactionHistoryTable.BINID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.BUYSELLCOST_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.INSPSTATUS_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.TRANSDATE_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.POSTDATE_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.REFMASTERID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.REFDETAILID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.COST_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.CCNID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.TRANTYPEID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.PARTYID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.PARTYLOCATIONID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.LOCATIONID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.PRODUCTID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.STOCKUMID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.CURRENCYID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.QUANTITY_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.BINOHQUANTITY_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.COMMENT_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.EXCHANGERATE_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.LOT_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.SERIAL_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.OLDAVGCOST_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.NEWAVGCOST_FLD + "=  ?"
					+ " WHERE " + MST_TransactionHistoryTable.MASTERLOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.BUYSELLCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.BUYSELLCOST_FLD].Value = objObject.BuySellCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.INSPSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.INSPSTATUS_FLD].Value = objObject.InspStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD].Value = objObject.TransactionHistoryID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.TRANSDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.TRANSDATE_FLD].Value = objObject.TransDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.REFMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.REFMASTERID_FLD].Value = objObject.RefMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.REFDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.REFDETAILID_FLD].Value = objObject.RefDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.COST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.COST_FLD].Value = objObject.Cost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.TRANTYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.TRANTYPEID_FLD].Value = objObject.TranTypeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD].Value = objObject.MasLocOHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD].Value = objObject.LocationOHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.BINOHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.BINOHQUANTITY_FLD].Value = objObject.BinOHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.COMMENT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.OLDAVGCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.OLDAVGCOST_FLD].Value = objObject.OldAvgCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.NEWAVGCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.NEWAVGCOST_FLD].Value = objObject.NewAvgCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;


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

		///    <summary>
		///       This method uses to add data to MST_TransactionHistory
		///    </summary>
		///    <Inputs>
		///        MST_TransactionHistoryVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 30, 2005
		///    </History>
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
					+ MST_TransactionHistoryTable.MASTERLOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.BINID_FLD + ","
					+ MST_TransactionHistoryTable.BUYSELLCOST_FLD + ","
					+ MST_TransactionHistoryTable.INSPSTATUS_FLD + ","
					+ MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD + ","
					+ MST_TransactionHistoryTable.TRANSDATE_FLD + ","
					+ MST_TransactionHistoryTable.POSTDATE_FLD + ","
					+ MST_TransactionHistoryTable.REFMASTERID_FLD + ","
					+ MST_TransactionHistoryTable.REFDETAILID_FLD + ","
					+ MST_TransactionHistoryTable.COST_FLD + ","
					+ MST_TransactionHistoryTable.CCNID_FLD + ","
					+ MST_TransactionHistoryTable.TRANTYPEID_FLD + ","
					+ MST_TransactionHistoryTable.PARTYID_FLD + ","
					+ MST_TransactionHistoryTable.PARTYLOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.PRODUCTID_FLD + ","
					+ MST_TransactionHistoryTable.STOCKUMID_FLD + ","
					+ MST_TransactionHistoryTable.CURRENCYID_FLD + ","
					+ MST_TransactionHistoryTable.QUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.BINOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.MASLOCCOMMITQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONCOMMITQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.BINCOMMITQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.COMMENT_FLD + ","
					+ MST_TransactionHistoryTable.EXCHANGERATE_FLD + ","
					+ MST_TransactionHistoryTable.LOT_FLD + ","
					+ MST_TransactionHistoryTable.SERIAL_FLD + ","
					+ MST_TransactionHistoryTable.OLDAVGCOST_FLD + ","
					+ MST_TransactionHistoryTable.USERNAME_FLD + ","
					+ MST_TransactionHistoryTable.NEWAVGCOST_FLD
					+ " FROM " + MST_TransactionHistoryTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_TransactionHistoryTable.TABLE_NAME);

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

		public DataSet List(int pintID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ MST_TransactionHistoryTable.MASTERLOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.BINID_FLD + ","
					+ MST_TransactionHistoryTable.BUYSELLCOST_FLD + ","
					+ MST_TransactionHistoryTable.INSPSTATUS_FLD + ","
					+ MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD + ","
					+ MST_TransactionHistoryTable.TRANSDATE_FLD + ","
					+ MST_TransactionHistoryTable.POSTDATE_FLD + ","
					+ MST_TransactionHistoryTable.REFMASTERID_FLD + ","
					+ MST_TransactionHistoryTable.REFDETAILID_FLD + ","
					+ MST_TransactionHistoryTable.COST_FLD + ","
					+ MST_TransactionHistoryTable.CCNID_FLD + ","
					+ MST_TransactionHistoryTable.TRANTYPEID_FLD + ","
					+ MST_TransactionHistoryTable.PARTYID_FLD + ","
					+ MST_TransactionHistoryTable.PARTYLOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.PRODUCTID_FLD + ","
					+ MST_TransactionHistoryTable.STOCKUMID_FLD + ","
					+ MST_TransactionHistoryTable.CURRENCYID_FLD + ","
					+ MST_TransactionHistoryTable.QUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.BINOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.MASLOCCOMMITQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONCOMMITQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.BINCOMMITQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.COMMENT_FLD + ","
					+ MST_TransactionHistoryTable.EXCHANGERATE_FLD + ","
					+ MST_TransactionHistoryTable.LOT_FLD + ","
					+ MST_TransactionHistoryTable.SERIAL_FLD + ","
					+ MST_TransactionHistoryTable.OLDAVGCOST_FLD + ","
					+ MST_TransactionHistoryTable.USERNAME_FLD + ","
					+ MST_TransactionHistoryTable.NEWAVGCOST_FLD
					+ " FROM " + MST_TransactionHistoryTable.TABLE_NAME
					+ " WHERE " + MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD + "=" + pintID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_TransactionHistoryTable.TABLE_NAME);

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
		public DataTable GetSchema()
		{
			const string METHOD_NAME = THIS + ".GetSchema()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT TOP 0 * FROM MST_TransactionHistory";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable(MST_TransactionHistoryTable.TABLE_NAME);
				odadPCS.Fill(dtbData);

				return dtbData;
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

		///    <summary>
		///       This method uses to add data to MST_TransactionHistory
		///    </summary>
		///    <Inputs>
		///        MST_TransactionHistoryVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 30, 2005
		///    </History>
		public void UpdateDataSet(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql = "SELECT "
					+ MST_TransactionHistoryTable.MASTERLOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.BINID_FLD + ","
					+ MST_TransactionHistoryTable.BUYSELLCOST_FLD + ","
					+ MST_TransactionHistoryTable.INSPSTATUS_FLD + ","
					+ MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD + ","
					+ MST_TransactionHistoryTable.TRANSDATE_FLD + ","
					+ MST_TransactionHistoryTable.POSTDATE_FLD + ","
					+ MST_TransactionHistoryTable.REFMASTERID_FLD + ","
					+ MST_TransactionHistoryTable.REFDETAILID_FLD + ","
					+ MST_TransactionHistoryTable.COST_FLD + ","
					+ MST_TransactionHistoryTable.CCNID_FLD + ","
					+ MST_TransactionHistoryTable.TRANTYPEID_FLD + ","
					+ MST_TransactionHistoryTable.PARTYID_FLD + ","
					+ MST_TransactionHistoryTable.PARTYLOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONID_FLD + ","
					+ MST_TransactionHistoryTable.PRODUCTID_FLD + ","
					+ MST_TransactionHistoryTable.STOCKUMID_FLD + ","
					+ MST_TransactionHistoryTable.CURRENCYID_FLD + ","
					+ MST_TransactionHistoryTable.QUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.BINOHQUANTITY_FLD + ","
					+ MST_TransactionHistoryTable.COMMENT_FLD + ","
					+ MST_TransactionHistoryTable.EXCHANGERATE_FLD + ","
					+ MST_TransactionHistoryTable.LOT_FLD + ","
					+ MST_TransactionHistoryTable.SERIAL_FLD + ","
					+ MST_TransactionHistoryTable.OLDAVGCOST_FLD + ","
					+ MST_TransactionHistoryTable.USERNAME_FLD + ","
					+ MST_TransactionHistoryTable.NEWAVGCOST_FLD
					+ "  FROM " + MST_TransactionHistoryTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand cmdSelect = new OleDbCommand(strSql, oconPCS);
				cmdSelect.CommandTimeout = 10000;
				odadPCS.SelectCommand = cmdSelect;
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData, MST_TransactionHistoryTable.TABLE_NAME);

			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 0)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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

		/// <summary>
		/// Gets in quantity in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>DataTable</returns>
		public DataTable GetInQuantity(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT SUM(ISNULL(Quantity,0)) AS " + MST_TransactionHistoryTable.QUANTITY_FLD + ", ProductID FROM MST_TransactionHistory"
					+ " JOIN MST_TranType ON MST_TransactionHistory.TranTypeID = MST_TranType.TranTypeID"
					+ " WHERE Type IN ( " + (int) TransactionHistoryType.In + "," + (int) TransactionHistoryType.Both + ")"
					+ " AND CCNID = " + pintCCNID
					+ " AND TransDate >= ? AND TransDate <= ?"
					+ " GROUP BY ProductID";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				cmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Gets out quantity in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>DataTable</returns>
		public DataTable GetOutQuantity(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT SUM(ISNULL(Quantity,0)) AS " + MST_TransactionHistoryTable.QUANTITY_FLD + ", ProductID FROM MST_TransactionHistory"
					+ " JOIN MST_TranType ON MST_TransactionHistory.TranTypeID = MST_TranType.TranTypeID"
					+ " WHERE Type = " + (int) TransactionHistoryType.Out
					+ " AND CCNID = " + pintCCNID
					+ " AND TransDate >= ? AND TransDate <= ?"
					+ " GROUP BY ProductID";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				cmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		public DataSet RetrieveCacheData(int pintMasterLocationID, string pstrLocationID, string pstrBinID, string pstrProductID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT MasterLocationID, ProductID, OHQuantity, CommitQuantity"
					+ " FROM IV_MasLocCache WHERE MasterLocationID IN (" + pintMasterLocationID + ")"
					+ " AND ProductID IN (" + pstrProductID + ");"
					+ " SELECT LocationID, ProductID, OHQuantity, CommitQuantity"
					+ " FROM IV_LocationCache"
					+ " WHERE LocationID IN (" + pstrLocationID + ")"
					+ " AND ProductID IN (" + pstrProductID + ");"
					+ " SELECT BinID, ProductID, OHQuantity, CommitQuantity"
					+ " FROM IV_BinCache"
					+ " WHERE BinID IN (" + pstrBinID + ")"
					+ " AND ProductID IN (" + pstrProductID + ");";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Connection.Open();
				DataSet dstData = new DataSet();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dstData);
				return dstData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}


		public void UpdatebyRefMasterID(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".UpdatebyRefMasterID()";

			MST_TransactionHistoryVO objObject = (MST_TransactionHistoryVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE MST_TransactionHistory SET "
					+ MST_TransactionHistoryTable.BINID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.CCNID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.TRANTYPEID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.LOCATIONID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.PRODUCTID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.QUANTITY_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.BINOHQUANTITY_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.OLDAVGCOST_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.NEWAVGCOST_FLD + "=  ?"
					+ " WHERE " + MST_TransactionHistoryTable.REFMASTERID_FLD + "=   ?" 
                    	+ " AND " + MST_TransactionHistoryTable.PRODUCTID_FLD + "=   ?"
						+ " AND " + MST_TransactionHistoryTable.TRANTYPEID_FLD + "=   ?" ;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.TRANTYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.TRANTYPEID_FLD].Value = objObject.TranTypeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD].Value = objObject.MasLocOHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD].Value = objObject.LocationOHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.BINOHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.BINOHQUANTITY_FLD].Value = objObject.BinOHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.OLDAVGCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.OLDAVGCOST_FLD].Value = objObject.OldAvgCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.NEWAVGCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.NEWAVGCOST_FLD].Value = objObject.NewAvgCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.REFMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.REFMASTERID_FLD].Value = objObject.RefMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter("PRODUCTID_FLD", OleDbType.Integer)).Value = objObject.ProductID;
				//ocmdPCS.Parameters[MST_TransactionHistoryTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter("TRANTYPEID_FLD", OleDbType.Integer)).Value = objObject.TranTypeID;
				//ocmdPCS.Parameters[MST_TransactionHistoryTable.TRANTYPEID_FLD].Value = objObject.TranTypeID;

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

		public int GetTransactionHistoryID(int refMasterId, int refDetailId)
		{
			const string METHOD_NAME = THIS + ".GetTransactionHistoryID()";

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "Select TransactionHistoryId " +
					"FROM MST_TransactionHistory "+
					"Where refmasterid = " + refMasterId.ToString().Trim() + " and Refdetailid = "+ refDetailId.ToString().Trim();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				int intTransactionHistoryID = 0;

				while (odrPCS.Read())
				{
					intTransactionHistoryID = int.Parse(odrPCS[MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD].ToString().Trim());
				}
				return intTransactionHistoryID;
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

		public void DeleteByRefMaster(int printRefMasterID, int printRefDetailID)
		{
			const string METHOD_NAME = THIS + ".DeleteByRefMaster()";
			string strSql = String.Empty;
			strSql = "DELETE " + MST_TransactionHistoryTable.TABLE_NAME + " WHERE  " + MST_TransactionHistoryTable.REFMASTERID_FLD + "=" + printRefMasterID.ToString()
					+" AND "+ MST_TransactionHistoryTable.REFDETAILID_FLD + " = " + printRefDetailID.ToString();
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

		public void UpdateTranType(int refPurchaseMaster, int oldTranTypeID, int tranTypeId, int inspStatus)
		{
			const string METHOD_NAME = THIS + ".UpdateTranType()";

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE MST_TransactionHistory SET "
					+ MST_TransactionHistoryTable.TRANTYPEID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.INSPSTATUS_FLD + "= ?"
					+ " WHERE " + MST_TransactionHistoryTable.REFMASTERID_FLD + "=   ?" 
					+ " AND " + MST_TransactionHistoryTable.TRANTYPEID_FLD + "=   ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.TRANTYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.TRANTYPEID_FLD].Value = tranTypeId;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.INSPSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.INSPSTATUS_FLD].Value = inspStatus;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.REFMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.REFMASTERID_FLD].Value = refPurchaseMaster;

				ocmdPCS.Parameters.Add(new OleDbParameter("OldTranTypeID", OleDbType.Integer)).Value = oldTranTypeID;
				
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
		/// <summary>
		/// Update transaction type for detail information
		/// </summary>
		/// <param name="pintRefMasterID">Reference transaction master id</param>
		/// <param name="pintRefDetailID">Reference transaction detail id</param>
		/// <param name="oldTranTypeID">Old transaction type</param>
		/// <param name="newTranTypeID">New transaction type</param>
		/// <param name="inspStatus">Inspection status</param>
		public void UpdateTranType(int pintRefMasterID, int pintRefDetailID, int oldTranTypeID, int newTranTypeID, int inspStatus)
		{
			const string METHOD_NAME = THIS + ".UpdateTranType()";

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE MST_TransactionHistory SET "
					+ MST_TransactionHistoryTable.TRANTYPEID_FLD + "=   ?" + ","
					+ MST_TransactionHistoryTable.INSPSTATUS_FLD + "= ?"
					+ " WHERE " + MST_TransactionHistoryTable.REFMASTERID_FLD + "=   ?" 
					+ " AND " + MST_TransactionHistoryTable.REFDETAILID_FLD + "=   ?"
					+ " AND " + MST_TransactionHistoryTable.TRANTYPEID_FLD + "=   ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.TRANTYPEID_FLD, OleDbType.Integer)).Value = newTranTypeID;
				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.INSPSTATUS_FLD, OleDbType.Integer)).Value = inspStatus;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.REFMASTERID_FLD, OleDbType.Integer)).Value = pintRefMasterID;
				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.REFDETAILID_FLD, OleDbType.Integer)).Value = pintRefDetailID;
				ocmdPCS.Parameters.Add(new OleDbParameter("OldTranTypeID", OleDbType.Integer)).Value = oldTranTypeID;
				
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

		public DataTable ListForUpdateStockTaking(DateTime pdtmStockTakingDate)
		{
			const string METHOD_NAME = THIS + ".ListForUpdateStockTaking()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DateTime dtmEndDate = new DateTime(pdtmStockTakingDate.Year, pdtmStockTakingDate.Month, pdtmStockTakingDate.Day, 23, 59, 59);
				string strSql = "SELECT MST_TranType.TranTypeID, MST_TranType.Code TranType, LocationID, BinID, ProductID, PostDate,"
					+ " CASE MST_TranType.Type WHEN 0 THEN -Quantity ELSE Quantity END AS Quantity"
					+ " FROM MST_TransactionHistory JOIN MST_TranType ON MST_TransactionHistory.TranTypeID = MST_TranType.TranTypeID"
					+ " AND MST_TranType.Type IN (" + (int)TransactionHistoryType.In + "," + (int)TransactionHistoryType.Out + "," + (int)TransactionHistoryType.Both + ")"
					+ " WHERE PostDate BETWEEN ? AND ?"
					+ " AND Quantity <> 0"
					+ " ORDER BY MST_TranType.TranTypeID, LocationID, BinID, ProductID, PostDate";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add("FromDate", OleDbType.Date).Value = pdtmStockTakingDate;
				ocmdPCS.Parameters.Add("ToDate", OleDbType.Date).Value = dtmEndDate;

				ocmdPCS.Connection.Open();
				DataTable dtbData = new DataTable(MST_TransactionHistoryTable.TABLE_NAME);
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
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