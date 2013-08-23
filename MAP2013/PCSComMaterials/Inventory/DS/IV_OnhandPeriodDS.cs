using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComMaterials.Inventory.DS
{
	public class IV_OnhandPeriodDS 
	{
		public IV_OnhandPeriodDS()
		{
		}

		private const string THIS = "PCSComMaterials.Inventory.DS.DS.IV_OnhandPeriodDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to IV_OnhandPeriod
		///    </Description>
		///    <Inputs>
		///        IV_OnhandPeriodVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       Code generate
		///    </Authors>
		///    <History>
		///       Thursday, October 05, 2006
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
				IV_OnhandPeriodVO objObject = (IV_OnhandPeriodVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO IV_OnhandPeriod("
					+ IV_OnhandPeriodTable.CODE_FLD + ","
					+ IV_OnhandPeriodTable.EFFECTDATE_FLD + ","
					+ IV_OnhandPeriodTable.STATUS_FLD + ")"
					+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_OnhandPeriodTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_OnhandPeriodTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_OnhandPeriodTable.EFFECTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_OnhandPeriodTable.EFFECTDATE_FLD].Value = objObject.EffectDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_OnhandPeriodTable.STATUS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[IV_OnhandPeriodTable.STATUS_FLD].Value = objObject.Status;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		/// AddAndReturnID
		/// </summary>
		/// <param name="pobjOnHandPeriodVO"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, October 6 2006</date>
		public int AddAndReturnID(object pobjOnHandPeriodVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				IV_OnhandPeriodVO objObject = (IV_OnhandPeriodVO) pobjOnHandPeriodVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO IV_OnhandPeriod("
					+ IV_OnhandPeriodTable.CODE_FLD + ","
					+ IV_OnhandPeriodTable.EFFECTDATE_FLD + ","
					+ IV_OnhandPeriodTable.STATUS_FLD + ")"
					+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_OnhandPeriodTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_OnhandPeriodTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_OnhandPeriodTable.EFFECTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_OnhandPeriodTable.EFFECTDATE_FLD].Value = objObject.EffectDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_OnhandPeriodTable.STATUS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[IV_OnhandPeriodTable.STATUS_FLD].Value = objObject.Status;

				strSql += " ; SELECT @@IDENTITY AS NEWID";

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objReturn = ocmdPCS.ExecuteScalar();
				if (objReturn != null)
				{
					return int.Parse(objReturn.ToString());
				}
				else
				{
					return 0;
				}
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
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to delete data from IV_OnhandPeriod
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
		///       Code generate
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
			strSql = "DELETE " + IV_OnhandPeriodTable.TABLE_NAME + " WHERE  " + "OnhandPeriodID" + "=" + pintID.ToString();
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
		///       This method uses to get data from IV_OnhandPeriod
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_OnhandPeriodVO
		///    </Outputs>
		///    <Returns>
		///       IV_OnhandPeriodVO
		///    </Returns>
		///    <Authors>
		///       Code Generate 
		///    </Authors>
		///    <History>
		///       Thursday, October 05, 2006
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
					+ IV_OnhandPeriodTable.ONHANDPERIODID_FLD + ","
					+ IV_OnhandPeriodTable.CODE_FLD + ","
					+ IV_OnhandPeriodTable.EFFECTDATE_FLD + ","
					+ IV_OnhandPeriodTable.STATUS_FLD
					+ " FROM " + IV_OnhandPeriodTable.TABLE_NAME
					+ " WHERE " + IV_OnhandPeriodTable.ONHANDPERIODID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_OnhandPeriodVO objObject = new IV_OnhandPeriodVO();

				while (odrPCS.Read())
				{
					objObject.OnhandPeriodID = int.Parse(odrPCS[IV_OnhandPeriodTable.ONHANDPERIODID_FLD].ToString());
					objObject.Code = odrPCS[IV_OnhandPeriodTable.CODE_FLD].ToString();
					objObject.EffectDate = DateTime.Parse(odrPCS[IV_OnhandPeriodTable.EFFECTDATE_FLD].ToString());
					objObject.Status = bool.Parse(odrPCS[IV_OnhandPeriodTable.STATUS_FLD].ToString());

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
		///       This method uses to update data to IV_OnhandPeriod
		///    </Description>
		///    <Inputs>
		///       IV_OnhandPeriodVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Code Generate 
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

			IV_OnhandPeriodVO objObject = (IV_OnhandPeriodVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE IV_OnhandPeriod SET "
					+ IV_OnhandPeriodTable.CODE_FLD + "=   ?" + ","
					+ IV_OnhandPeriodTable.EFFECTDATE_FLD + "=   ?" + ","
					+ IV_OnhandPeriodTable.STATUS_FLD + "=  ?"
					+ " WHERE " + IV_OnhandPeriodTable.ONHANDPERIODID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_OnhandPeriodTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_OnhandPeriodTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_OnhandPeriodTable.EFFECTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_OnhandPeriodTable.EFFECTDATE_FLD].Value = objObject.EffectDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_OnhandPeriodTable.STATUS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[IV_OnhandPeriodTable.STATUS_FLD].Value = objObject.Status;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_OnhandPeriodTable.ONHANDPERIODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_OnhandPeriodTable.ONHANDPERIODID_FLD].Value = objObject.OnhandPeriodID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to get all data from IV_OnhandPeriod
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
		///       Code Generate 
		///    </Authors>
		///    <History>
		///       Thursday, October 05, 2006
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
					+ IV_OnhandPeriodTable.ONHANDPERIODID_FLD + ","
					+ IV_OnhandPeriodTable.CODE_FLD + ","
					+ IV_OnhandPeriodTable.EFFECTDATE_FLD + ","
					+ IV_OnhandPeriodTable.STATUS_FLD
					+ " FROM " + IV_OnhandPeriodTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_OnhandPeriodTable.TABLE_NAME);

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
		/// <summary>
		/// GetAllProductAndLocation
		/// </summary>
		/// <param name="pdtmEffectDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, October 10 2006</date>
		public DataSet GetAllProductAndLocation(DateTime pdtmEffectDate)
		{
			const string METHOD_NAME = THIS + ".GetAllProductAndLocation()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT ProductID"
					+ " FROM " + ITM_ProductTable.TABLE_NAME 
					+ " ; SELECT DISTINCT LocationID " 
					+ " FROM (SELECT LocationID FROM " + IV_BalanceLocationTable.TABLE_NAME + " UNION ALL SELECT LocationID FROM "
					+ MST_TransactionHistoryTable.TABLE_NAME 
					+ " WHERE " + MST_TransactionHistoryTable.POSTDATE_FLD + " >= '" + pdtmEffectDate.ToShortDateString() + "'" 
					+ " AND " + MST_TransactionHistoryTable.POSTDATE_FLD + " < '" + pdtmEffectDate.AddMonths(1).ToShortDateString() + "') as MyTable";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

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
		/// <summary>
		/// Get all products and Master Location
		/// </summary>
		/// <param name="pdtmEffectDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, October 17 2006</date>
		public DataSet GetAllProductAndMasterLocation(DateTime pdtmEffectDate)
		{
			const string METHOD_NAME = THIS + ".GetAllProductAndMasterLocation()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT ProductID"
					+ " FROM " + ITM_ProductTable.TABLE_NAME 
					+ " ; SELECT DISTINCT MasterLocationID " 
					+ " FROM (SELECT MasterLocationID FROM " + IV_BalanceMasterLocationTable.TABLE_NAME + " UNION ALL SELECT MasterLocationID FROM "
					+ MST_TransactionHistoryTable.TABLE_NAME 
					+ " WHERE " + MST_TransactionHistoryTable.POSTDATE_FLD + " >= '" + pdtmEffectDate.ToShortDateString() + "'" 
					+ " AND " + MST_TransactionHistoryTable.POSTDATE_FLD + " < '" + pdtmEffectDate.AddMonths(1).ToShortDateString() + "') as MyTable";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

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
		/// <summary>
		/// Get all products with bin
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, October 9 2006</date>
		public DataSet GetAllProductAndBin(DateTime pdtmEffectDate)
		{
			const string METHOD_NAME = THIS + ".GetAllProductAndBin()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT ProductID"
					+ " FROM " + ITM_ProductTable.TABLE_NAME 
					+ " ; SELECT BinID, LocationID " 
					+ " FROM " + MST_BINTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

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
		/// <summary>
		/// GetAllTransactionHistoryInThisPeriod
		/// </summary>
		/// <param name="pdtmEffectDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, October 10 2006</date>
		public DataSet GetAllTransactionHistoryInThisPeriod(DateTime pdtmEffectDate)
		{
			const string METHOD_NAME = THIS + ".GetAllTransactionHistoryInThisPeriod()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ " H." + MST_TransactionHistoryTable.MASTERLOCATIONID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.BINID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.BUYSELLCOST_FLD + ","
					+ " H." + MST_TransactionHistoryTable.INSPSTATUS_FLD + ","
					+ " H." + MST_TransactionHistoryTable.TRANSACTIONHISTORYID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.TRANSDATE_FLD + ","
					+ " H." + MST_TransactionHistoryTable.POSTDATE_FLD + ","
					+ " H." + MST_TransactionHistoryTable.REFMASTERID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.REFDETAILID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.COST_FLD + ","
					+ " H." + MST_TransactionHistoryTable.CCNID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.TRANTYPEID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.PARTYID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.PARTYLOCATIONID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.LOCATIONID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.PRODUCTID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.STOCKUMID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.CURRENCYID_FLD + ","
					+ " H." + MST_TransactionHistoryTable.QUANTITY_FLD + ","
					+ " H." + MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD + ","
					+ " H." + MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD + ","
					+ " H." + MST_TransactionHistoryTable.BINOHQUANTITY_FLD + ","
					+ " H." + MST_TransactionHistoryTable.MASLOCCOMMITQUANTITY_FLD + ","
					+ " H." + MST_TransactionHistoryTable.LOCATIONCOMMITQUANTITY_FLD + ","
					+ " H." + MST_TransactionHistoryTable.BINCOMMITQUANTITY_FLD + ","
					+ " H." + MST_TransactionHistoryTable.COMMENT_FLD + ","
					+ " H." + MST_TransactionHistoryTable.EXCHANGERATE_FLD + ","
					+ " H." + MST_TransactionHistoryTable.LOT_FLD + ","
					+ " H." + MST_TransactionHistoryTable.SERIAL_FLD + ","
					+ " H." + MST_TransactionHistoryTable.OLDAVGCOST_FLD + ","
					+ " H." + MST_TransactionHistoryTable.USERNAME_FLD + ","
					+ " TT." + MST_TranTypeTable.TYPE_FLD + ","
					+ " H." + MST_TransactionHistoryTable.NEWAVGCOST_FLD
					+ " FROM " + MST_TransactionHistoryTable.TABLE_NAME + " H " 
					+ " INNER JOIN " + MST_TranTypeTable.TABLE_NAME + " TT ON TT." 
					+ MST_TranTypeTable.TRANTYPEID_FLD + " = H." + MST_TransactionHistoryTable.TRANTYPEID_FLD
					+ " WHERE H." + MST_TransactionHistoryTable.POSTDATE_FLD + " >= '" + pdtmEffectDate.ToShortDateString() + "'" 
					+ " AND H." + MST_TransactionHistoryTable.POSTDATE_FLD + " < '" + pdtmEffectDate.AddMonths(1).ToShortDateString() + "'";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

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
		/// <summary>
		/// GetProductInTransactionHistory
		/// </summary>
		/// <param name="pintProduct"></param>
		/// <param name="pintBinID"></param>
		/// <param name="pdtmEffectDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, October 9 2006</date>
		public DataSet GetProductAndBinInTransactionHistory(int pintProduct, int pintBinID, DateTime pdtmEffectDate)
		{
			const string METHOD_NAME = THIS + ".GetProductInTransactionHistory()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ " ISNULL(SUM(ISNULL(" + MST_TransactionHistoryTable.QUANTITY_FLD + ",0)),0) as Quantity" 
					+ " FROM " + MST_TransactionHistoryTable.TABLE_NAME
					+ " WHERE " + MST_TransactionHistoryTable.PRODUCTID_FLD + " = " + pintProduct.ToString()
					+ " AND " + MST_TransactionHistoryTable.BINID_FLD + " = " + pintBinID.ToString()
					+ " AND " + MST_TransactionHistoryTable.POSTDATE_FLD + " >= '" + pdtmEffectDate.ToShortDateString() + "'"
					+ " AND " + MST_TransactionHistoryTable.POSTDATE_FLD + " < '" + pdtmEffectDate.AddMonths(1).ToShortDateString() + "'"
					+ " AND " + MST_TransactionHistoryTable.QUANTITY_FLD + " > 0;";
				strSql += " SELECT "
					+ " ISNULL(SUM(ISNULL(" + MST_TransactionHistoryTable.QUANTITY_FLD + ",0)),0) as Quantity"  
					+ " FROM " + MST_TransactionHistoryTable.TABLE_NAME
					+ " WHERE " + MST_TransactionHistoryTable.PRODUCTID_FLD + " = " + pintProduct.ToString()
					+ " AND " + MST_TransactionHistoryTable.BINID_FLD + " = " + pintBinID.ToString()
					+ " AND " + MST_TransactionHistoryTable.POSTDATE_FLD + " >= '" + pdtmEffectDate.ToShortDateString() + "'"
					+ " AND " + MST_TransactionHistoryTable.POSTDATE_FLD + " < '" + pdtmEffectDate.AddMonths(1).AddSeconds(-1).ToShortDateString() + "'"
					+ " AND " + MST_TransactionHistoryTable.QUANTITY_FLD + " < 0";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

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
		/// <summary>
		/// GetProductAndLocationInTransactionHistory
		/// </summary>
		/// <param name="pintProduct"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pdtmEffectDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, October 10 2006</date>
		public DataSet GetProductAndLocationInTransactionHistory(int pintProduct, int pintLocationID, DateTime pdtmEffectDate)
		{
			const string METHOD_NAME = THIS + ".GetProductAndLocationInTransactionHistory()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ " ISNULL(SUM(ISNULL(" + MST_TransactionHistoryTable.QUANTITY_FLD + ",0)),0) as Quantity" 
					+ " FROM " + MST_TransactionHistoryTable.TABLE_NAME
					+ " WHERE " + MST_TransactionHistoryTable.PRODUCTID_FLD + " = " + pintProduct.ToString()
					+ " AND " + MST_TransactionHistoryTable.LOCATIONID_FLD + " = " + pintLocationID.ToString()
					+ " AND " + MST_TransactionHistoryTable.POSTDATE_FLD + " >= '" + pdtmEffectDate.ToShortDateString() + "'"
					+ " AND " + MST_TransactionHistoryTable.POSTDATE_FLD + " < '" + pdtmEffectDate.AddMonths(1).ToShortDateString() + "'"
					+ " AND " + MST_TransactionHistoryTable.QUANTITY_FLD + " > 0;";
				strSql += " SELECT "
					+ " ISNULL(SUM(ISNULL(" + MST_TransactionHistoryTable.QUANTITY_FLD + ",0)),0) as Quantity" 
					+ " FROM " + MST_TransactionHistoryTable.TABLE_NAME
					+ " WHERE " + MST_TransactionHistoryTable.PRODUCTID_FLD + " = " + pintProduct.ToString()
					+ " AND " + MST_TransactionHistoryTable.LOCATIONID_FLD + " = " + pintLocationID.ToString()
					+ " AND " + MST_TransactionHistoryTable.POSTDATE_FLD + " >= '" + pdtmEffectDate.ToShortDateString() + "'"
					+ " AND " + MST_TransactionHistoryTable.POSTDATE_FLD + " < '" + pdtmEffectDate.AddMonths(1).AddSeconds(-1).ToShortDateString() + "'"
					+ " AND " + MST_TransactionHistoryTable.QUANTITY_FLD + " < 0";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

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
		/// <summary>
		/// GetProductInBalanceOnHandLocation
		/// </summary>
		/// <param name="pintProduct"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pdtmEffectDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, October 10 2006</date>
		public DataSet GetProductInBalanceOnHandLocation(int pintProduct, int pintLocationID, DateTime pdtmEffectDate)
		{
			const string METHOD_NAME = THIS + ".GetProductInBalanceOnHandLocation()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ " ISNULL(" + IV_BalanceLocationTable.OHQUANTITY_FLD + ",0)," 
					+ IV_BalanceLocationTable.BALANCELOCATIONID_FLD
					+ " FROM " + IV_BalanceLocationTable.TABLE_NAME
					+ " WHERE " + IV_BalanceLocationTable.PRODUCTID_FLD + " = " + pintProduct.ToString()
					+ " AND " + IV_BalanceLocationTable.LOCATIONID_FLD + " = " + pintLocationID.ToString()
					+ " AND " + IV_BalanceLocationTable.EFFECTDATE_FLD + " = " + pdtmEffectDate.AddMonths(-1).ToShortDateString();
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_OnhandPeriodTable.TABLE_NAME);

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
		/// <summary>
		/// GetProductInBalanceOnHandBin
		/// </summary>
		/// <param name="pintProduct"></param>
		/// <param name="pintBinID"></param>
		/// <param name="pdtmEffectDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, October 9 2006</date>
		public DataSet GetProductInBalanceOnHandBin(int pintProduct, int pintBinID, DateTime pdtmEffectDate)
		{
			const string METHOD_NAME = THIS + ".GetProductInBalanceOnHandBin()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ " ISNULL(" + IV_BalanceBinTable.OHQUANTITY_FLD + ",0)," 
					+ IV_BalanceBinTable.BALANCEBINID_FLD
					+ " FROM " + IV_BalanceBinTable.TABLE_NAME
					+ " WHERE " + IV_BalanceBinTable.PRODUCTID_FLD + " = " + pintProduct.ToString()
					+ " AND " + IV_BalanceBinTable.BINID_FLD + " = " + pintBinID.ToString()
					+ " AND " + IV_BalanceBinTable.EFFECTDATE_FLD + " = " + pdtmEffectDate.AddMonths(-1).ToShortDateString();
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_OnhandPeriodTable.TABLE_NAME);

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
		///       Code Generate
		///    </Authors>
		///    <History>
		///       Thursday, October 05, 2006
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
					+ IV_OnhandPeriodTable.ONHANDPERIODID_FLD + ","
					+ IV_OnhandPeriodTable.CODE_FLD + ","
					+ IV_OnhandPeriodTable.EFFECTDATE_FLD + ","
					+ IV_OnhandPeriodTable.STATUS_FLD;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, IV_OnhandPeriodTable.TABLE_NAME);

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		/// Open period
		/// </summary>
		/// <param name="pintOnHandPeriodID"></param>
		/// <author>Trada</author>
		/// <date>Monday, October 9 2006</date>
		public void CloseOpenPeriod(int pintOnHandPeriodID, bool pblnClose)
		{
			const string METHOD_NAME = THIS + ".OpenPeriod()";
			
			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE IV_OnhandPeriod SET "
					+ IV_OnhandPeriodTable.STATUS_FLD + "=  ?"
					+ " WHERE " + IV_OnhandPeriodTable.ONHANDPERIODID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_OnhandPeriodTable.STATUS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[IV_OnhandPeriodTable.STATUS_FLD].Value = pblnClose;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_OnhandPeriodTable.ONHANDPERIODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_OnhandPeriodTable.ONHANDPERIODID_FLD].Value = pintOnHandPeriodID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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