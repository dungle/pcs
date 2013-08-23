using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComMaterials.Inventory.DS
{
	public class IV_AdjustmentDS 
	{
		public IV_AdjustmentDS()
		{
		}

		private const string THIS = "PCSComMaterials.Inventory.DS.IV_AdjustmentDS";

		///    <summary>
		///       This method uses to add data to IV_Adjustment
		///    </summary>
		///    <Inputs>
		///        IV_AdjustmentVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 23, 2005
		///    </History>
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				IV_AdjustmentVO objObject = (IV_AdjustmentVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO IV_Adjustment("
					+ IV_AdjustmentTable.MASTERLOCATIONID_FLD + ","
					+ IV_AdjustmentTable.LOCATIONID_FLD + ","
					+ IV_AdjustmentTable.ADJUSTMENTID_FLD + ","
					+ IV_AdjustmentTable.POSTDATE_FLD + ","
					+ IV_AdjustmentTable.COMMENT_FLD + ","
					+ IV_AdjustmentTable.CCNID_FLD + ","
					+ IV_AdjustmentTable.TRANSNO_FLD + ","
					+ IV_AdjustmentTable.PRODUCTID_FLD + ","
					+ IV_AdjustmentTable.STOCKUMID_FLD + ","
					+ IV_AdjustmentTable.SERIAL_FLD + ","
					+ IV_AdjustmentTable.ADJUSTQUANTITY_FLD + ","
					+ IV_AdjustmentTable.AVAILABLEQTY_FLD + ","
					+ IV_AdjustmentTable.LOT_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.ADJUSTMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.ADJUSTMENTID_FLD].Value = objObject.AdjustmentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_AdjustmentTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.COMMENT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_AdjustmentTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.TRANSNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_AdjustmentTable.TRANSNO_FLD].Value = objObject.TransNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_AdjustmentTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.ADJUSTQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_AdjustmentTable.ADJUSTQUANTITY_FLD].Value = objObject.AdjustQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.AVAILABLEQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_AdjustmentTable.AVAILABLEQTY_FLD].Value = objObject.AvailableQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_AdjustmentTable.LOT_FLD].Value = objObject.Lot;


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
		///       This method uses to add data to IV_Adjustment
		///    </summary>
		///    <Inputs>
		///        IV_AdjustmentVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 23, 2005
		///    </History>
		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + IV_AdjustmentTable.TABLE_NAME + " WHERE  " + "BinID" + "=" + pintID.ToString();
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
		///       This method uses to add data to IV_Adjustment
		///    </summary>
		///    <Inputs>
		///        IV_AdjustmentVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 23, 2005
		///    </History>
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
					+ IV_AdjustmentTable.BINID_FLD + ","
					+ IV_AdjustmentTable.MASTERLOCATIONID_FLD + ","
					+ IV_AdjustmentTable.LOCATIONID_FLD + ","
					+ IV_AdjustmentTable.ADJUSTMENTID_FLD + ","
					+ IV_AdjustmentTable.POSTDATE_FLD + ","
					+ IV_AdjustmentTable.COMMENT_FLD + ","
					+ IV_AdjustmentTable.CCNID_FLD + ","
					+ IV_AdjustmentTable.TRANSNO_FLD + ","
					+ IV_AdjustmentTable.PRODUCTID_FLD + ","
					+ IV_AdjustmentTable.STOCKUMID_FLD + ","
					+ IV_AdjustmentTable.SERIAL_FLD + ","
					+ IV_AdjustmentTable.ADJUSTQUANTITY_FLD + ","
					+ IV_AdjustmentTable.LOT_FLD
					+ " FROM " + IV_AdjustmentTable.TABLE_NAME
					+ " WHERE " + IV_AdjustmentTable.BINID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_AdjustmentVO objObject = new IV_AdjustmentVO();

				while (odrPCS.Read())
				{
					objObject.BinID = int.Parse(odrPCS[IV_AdjustmentTable.BINID_FLD].ToString().Trim());
					objObject.MasterLocationID = int.Parse(odrPCS[IV_AdjustmentTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.LocationID = int.Parse(odrPCS[IV_AdjustmentTable.LOCATIONID_FLD].ToString().Trim());
					objObject.AdjustmentID = int.Parse(odrPCS[IV_AdjustmentTable.ADJUSTMENTID_FLD].ToString().Trim());
					objObject.PostDate = DateTime.Parse(odrPCS[IV_AdjustmentTable.POSTDATE_FLD].ToString().Trim());
					objObject.Comment = odrPCS[IV_AdjustmentTable.COMMENT_FLD].ToString().Trim();
					objObject.CCNID = int.Parse(odrPCS[IV_AdjustmentTable.CCNID_FLD].ToString().Trim());
					objObject.TransNo = odrPCS[IV_AdjustmentTable.TRANSNO_FLD].ToString().Trim();
					objObject.ProductID = int.Parse(odrPCS[IV_AdjustmentTable.PRODUCTID_FLD].ToString().Trim());
					objObject.StockUMID = int.Parse(odrPCS[IV_AdjustmentTable.STOCKUMID_FLD].ToString().Trim());
					objObject.Serial = odrPCS[IV_AdjustmentTable.SERIAL_FLD].ToString().Trim();
					objObject.AdjustQuantity = Decimal.Parse(odrPCS[IV_AdjustmentTable.ADJUSTQUANTITY_FLD].ToString().Trim());
					objObject.Lot = odrPCS[IV_AdjustmentTable.LOT_FLD].ToString().Trim();

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
		///       This method uses to add data to IV_Adjustment
		///    </summary>
		///    <Inputs>
		///        IV_AdjustmentVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 23, 2005
		///    </History>
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			IV_AdjustmentVO objObject = (IV_AdjustmentVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE IV_Adjustment SET "
					+ IV_AdjustmentTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ IV_AdjustmentTable.LOCATIONID_FLD + "=   ?" + ","
					+ IV_AdjustmentTable.ADJUSTMENTID_FLD + "=   ?" + ","
					+ IV_AdjustmentTable.POSTDATE_FLD + "=   ?" + ","
					+ IV_AdjustmentTable.COMMENT_FLD + "=   ?" + ","
					+ IV_AdjustmentTable.CCNID_FLD + "=   ?" + ","
					+ IV_AdjustmentTable.TRANSNO_FLD + "=   ?" + ","
					+ IV_AdjustmentTable.PRODUCTID_FLD + "=   ?" + ","
					+ IV_AdjustmentTable.STOCKUMID_FLD + "=   ?" + ","
					+ IV_AdjustmentTable.SERIAL_FLD + "=   ?" + ","
					+ IV_AdjustmentTable.ADJUSTQUANTITY_FLD + "=   ?" + ","
					+ IV_AdjustmentTable.LOT_FLD + "=  ?"
					+ " WHERE " + IV_AdjustmentTable.BINID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.ADJUSTMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.ADJUSTMENTID_FLD].Value = objObject.AdjustmentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_AdjustmentTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.COMMENT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_AdjustmentTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.TRANSNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_AdjustmentTable.TRANSNO_FLD].Value = objObject.TransNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_AdjustmentTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.ADJUSTQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_AdjustmentTable.ADJUSTQUANTITY_FLD].Value = objObject.AdjustQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_AdjustmentTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.BINID_FLD].Value = objObject.BinID;


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
		///       This method uses to add data to IV_Adjustment
		///    </summary>
		///    <Inputs>
		///        IV_AdjustmentVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 23, 2005
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
					+ IV_AdjustmentTable.BINID_FLD + ","
					+ IV_AdjustmentTable.MASTERLOCATIONID_FLD + ","
					+ IV_AdjustmentTable.LOCATIONID_FLD + ","
					+ IV_AdjustmentTable.ADJUSTMENTID_FLD + ","
					+ IV_AdjustmentTable.POSTDATE_FLD + ","
					+ IV_AdjustmentTable.COMMENT_FLD + ","
					+ IV_AdjustmentTable.CCNID_FLD + ","
					+ IV_AdjustmentTable.TRANSNO_FLD + ","
					+ IV_AdjustmentTable.PRODUCTID_FLD + ","
					+ IV_AdjustmentTable.STOCKUMID_FLD + ","
					+ IV_AdjustmentTable.SERIAL_FLD + ","
					+ IV_AdjustmentTable.ADJUSTQUANTITY_FLD + ","
					+ IV_AdjustmentTable.LOT_FLD
					+ " FROM " + IV_AdjustmentTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_AdjustmentTable.TABLE_NAME);

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

		public DataSet List(string pstrComment)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ IV_AdjustmentTable.BINID_FLD + ","
					+ IV_AdjustmentTable.MASTERLOCATIONID_FLD + ","
					+ IV_AdjustmentTable.LOCATIONID_FLD + ","
					+ IV_AdjustmentTable.ADJUSTMENTID_FLD + ","
					+ IV_AdjustmentTable.POSTDATE_FLD + ","
					+ IV_AdjustmentTable.COMMENT_FLD + ","
					+ IV_AdjustmentTable.CCNID_FLD + ","
					+ IV_AdjustmentTable.TRANSNO_FLD + ","
					+ IV_AdjustmentTable.PRODUCTID_FLD + ","
					+ IV_AdjustmentTable.STOCKUMID_FLD + ","
					+ IV_AdjustmentTable.SERIAL_FLD + ","
					+ IV_AdjustmentTable.ADJUSTQUANTITY_FLD + ","
					+ IV_AdjustmentTable.USERNAME_FLD + ","
					+ IV_AdjustmentTable.LOT_FLD
					+ " FROM " + IV_AdjustmentTable.TABLE_NAME
					+ " WHERE " + IV_AdjustmentTable.COMMENT_FLD + " = ?";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.COMMENT_FLD, OleDbType.VarWChar)).Value = pstrComment;
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_AdjustmentTable.TABLE_NAME);

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
		/// List all transaction in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		public DataTable List(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".List()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT ADJUSTQUANTITY AS Quantity, PRODUCTID, BinTypeID, ISNULL(UsedByCosting,0) AS UsedByCosting"
					+ " FROM " + IV_AdjustmentTable.TABLE_NAME
					+ " JOIN " + MST_BINTable.TABLE_NAME
					+ " ON IV_Adjustment.BinID = MST_Bin.BinID"
					+ " WHERE " + IV_AdjustmentTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_AdjustmentTable.POSTDATE_FLD + " >= ?"
					+ " AND " + IV_AdjustmentTable.POSTDATE_FLD + " <= ?";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
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

		///    <summary>
		///       This method uses to add data to IV_Adjustment
		///    </summary>
		///    <Inputs>
		///        IV_AdjustmentVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 23, 2005
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
					+ IV_AdjustmentTable.POSTDATE_FLD + ","
					+ IV_AdjustmentTable.COMMENT_FLD + ","
					+ IV_AdjustmentTable.PRODUCTID_FLD + ","
					+ IV_AdjustmentTable.STOCKUMID_FLD + ","
					+ IV_AdjustmentTable.CCNID_FLD + ","
					+ IV_AdjustmentTable.LOCATIONID_FLD + ","
					+ IV_AdjustmentTable.BINID_FLD + ","
					+ IV_AdjustmentTable.MASTERLOCATIONID_FLD + ","
					+ IV_AdjustmentTable.ADJUSTQUANTITY_FLD + ","
					+ IV_AdjustmentTable.AVAILABLEQTY_FLD + ","
					+ IV_AdjustmentTable.USEDBYCOSTING_FLD + ","
					+ IV_AdjustmentTable.USERNAME_FLD + ","
					+ IV_AdjustmentTable.TRANSNO_FLD
					+ "  FROM " + IV_AdjustmentTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData, IV_AdjustmentTable.TABLE_NAME);

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
		/// AddAndReturnID
		/// </summary>
		/// <param name="pobjObjectVO"></param>
		/// <author>Trada</author>
		/// <date>Thursday, July 28 2005</date>
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				IV_AdjustmentVO objObject = (IV_AdjustmentVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO "
					+ IV_AdjustmentTable.TABLE_NAME + "("
					+ IV_AdjustmentTable.MASTERLOCATIONID_FLD + ","
					+ IV_AdjustmentTable.LOCATIONID_FLD + ","
					+ IV_AdjustmentTable.BINID_FLD + ","
					+ IV_AdjustmentTable.POSTDATE_FLD + ","
					+ IV_AdjustmentTable.COMMENT_FLD + ","
					+ IV_AdjustmentTable.CCNID_FLD + ","
					+ IV_AdjustmentTable.TRANSNO_FLD + ","
					+ IV_AdjustmentTable.PRODUCTID_FLD + ","
					+ IV_AdjustmentTable.STOCKUMID_FLD + ","
					+ IV_AdjustmentTable.SERIAL_FLD + ","
					+ IV_AdjustmentTable.ADJUSTQUANTITY_FLD + ","
					+ IV_AdjustmentTable.AVAILABLEQTY_FLD + ","
					+ "UsedByCosting,"
					+ IV_AdjustmentTable.LOT_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.BINID_FLD, OleDbType.Integer));
				if (objObject.BinID > 0)
				{
					ocmdPCS.Parameters[IV_AdjustmentTable.BINID_FLD].Value = objObject.BinID;
				}
				else
					ocmdPCS.Parameters[IV_AdjustmentTable.BINID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_AdjustmentTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.COMMENT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_AdjustmentTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.TRANSNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_AdjustmentTable.TRANSNO_FLD].Value = objObject.TransNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_AdjustmentTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_AdjustmentTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.ADJUSTQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_AdjustmentTable.ADJUSTQUANTITY_FLD].Value = objObject.AdjustQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.AVAILABLEQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_AdjustmentTable.AVAILABLEQTY_FLD].Value = objObject.AvailableQuantity;
				
				ocmdPCS.Parameters.Add(new OleDbParameter("UsedByCosting", OleDbType.Boolean));
				ocmdPCS.Parameters["UsedByCosting"].Value = objObject.UsedByCosting;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_AdjustmentTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_AdjustmentTable.LOT_FLD].Value = objObject.Lot;


				strSql += " ; SELECT @@IDENTITY AS NEWID";

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				//ocmdPCS.ExecuteNonQuery();	
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
				string strSql = String.Empty;

				strSql = "SELECT BinID, MasterLocationID, LocationID, AdjustmentID, PostDate,"
					+ " Comment, CCNID, TransNo, ProductID, StockUMID, Serial, AdjustQuantity,"
					+ " Lot, UserName, LastChange, AvailableQty, UsedByCosting"
					+ " FROM " + IV_AdjustmentTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable(IV_AdjustmentTable.TABLE_NAME);
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
		///       This method uses to add data to IV_Adjustment
		///    </summary>
		///    <Inputs>
		///        IV_AdjustmentVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 23, 2005
		///    </History>
		public object GetObjectVOByAdjustmentID(int pintID)
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
					+ IV_AdjustmentTable.BINID_FLD + ","
					+ IV_AdjustmentTable.MASTERLOCATIONID_FLD + ","
					+ IV_AdjustmentTable.LOCATIONID_FLD + ","
					+ IV_AdjustmentTable.ADJUSTMENTID_FLD + ","
					+ IV_AdjustmentTable.POSTDATE_FLD + ","
					+ IV_AdjustmentTable.COMMENT_FLD + ","
					+ IV_AdjustmentTable.CCNID_FLD + ","
					+ IV_AdjustmentTable.TRANSNO_FLD + ","
					+ IV_AdjustmentTable.PRODUCTID_FLD + ","
					+ IV_AdjustmentTable.STOCKUMID_FLD + ","
					+ IV_AdjustmentTable.SERIAL_FLD + ","
					+ IV_AdjustmentTable.ADJUSTQUANTITY_FLD + ","
					+ IV_AdjustmentTable.LOT_FLD
					+ " FROM " + IV_AdjustmentTable.TABLE_NAME
					+ " WHERE " + IV_AdjustmentTable.ADJUSTMENTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_AdjustmentVO objObject = new IV_AdjustmentVO();

				while (odrPCS.Read())
				{
					objObject.BinID = int.Parse(odrPCS[IV_AdjustmentTable.BINID_FLD].ToString().Trim());
					objObject.MasterLocationID = int.Parse(odrPCS[IV_AdjustmentTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.LocationID = int.Parse(odrPCS[IV_AdjustmentTable.LOCATIONID_FLD].ToString().Trim());
					objObject.AdjustmentID = int.Parse(odrPCS[IV_AdjustmentTable.ADJUSTMENTID_FLD].ToString().Trim());
					objObject.PostDate = DateTime.Parse(odrPCS[IV_AdjustmentTable.POSTDATE_FLD].ToString().Trim());
					objObject.Comment = odrPCS[IV_AdjustmentTable.COMMENT_FLD].ToString().Trim();
					objObject.CCNID = int.Parse(odrPCS[IV_AdjustmentTable.CCNID_FLD].ToString().Trim());
					objObject.TransNo = odrPCS[IV_AdjustmentTable.TRANSNO_FLD].ToString().Trim();
					objObject.ProductID = int.Parse(odrPCS[IV_AdjustmentTable.PRODUCTID_FLD].ToString().Trim());
					objObject.StockUMID = int.Parse(odrPCS[IV_AdjustmentTable.STOCKUMID_FLD].ToString().Trim());
					objObject.Serial = odrPCS[IV_AdjustmentTable.SERIAL_FLD].ToString().Trim();
					objObject.AdjustQuantity = Decimal.Parse(odrPCS[IV_AdjustmentTable.ADJUSTQUANTITY_FLD].ToString().Trim());
					objObject.Lot = odrPCS[IV_AdjustmentTable.LOT_FLD].ToString().Trim();

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
		///       This method uses to add data to IV_Adjustment
		///    </summary>
		///    <Inputs>
		///        IV_AdjustmentVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, June 23, 2005
		///    </History>
		public void DeleteByAdjustmentID(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + IV_AdjustmentTable.TABLE_NAME + " WHERE  " + IV_AdjustmentTable.ADJUSTMENTID_FLD + " =" + pintID.ToString();
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
	}
}