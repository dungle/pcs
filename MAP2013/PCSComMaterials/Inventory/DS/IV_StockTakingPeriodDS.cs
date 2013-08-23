using System;
using System.Data;
using System.Data.OleDb;
using System.Text;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComMaterials.Inventory.DS
{
	public class IV_StockTakingPeriodDS 
	{
		private const string THIS = "PCSComMaterials.Inventory.DS.DS.IV_StockTakingPeriodDS";
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				IV_StockTakingPeriodVO objObject = (IV_StockTakingPeriodVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO IV_StockTakingPeriod("
					+ IV_StockTakingPeriodTable.DESCRIPTION_FLD + ","
					+ IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD + ","
					+ IV_StockTakingPeriodTable.FROMDATE_FLD + ","
					+ IV_StockTakingPeriodTable.TODATE_FLD + ","
					+ IV_StockTakingPeriodTable.CLOSED_FLD + ","
					+ IV_StockTakingPeriodTable.CCNID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.DESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD, OleDbType.Date));
				if (objObject.StockTakingDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD].Value = objObject.StockTakingDate;
				}
				else
					ocmdPCS.Parameters[IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.FROMDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.FROMDATE_FLD].Value = objObject.FromDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.TODATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.TODATE_FLD].Value = objObject.ToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.CLOSED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.CLOSED_FLD].Value = objObject.Closed;

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
		/// <param name="pobjObjectVO"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				IV_StockTakingPeriodVO objObject = (IV_StockTakingPeriodVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO IV_StockTakingPeriod("
					+ IV_StockTakingPeriodTable.DESCRIPTION_FLD + ","
					+ IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD + ","
					+ IV_StockTakingPeriodTable.FROMDATE_FLD + ","
					+ IV_StockTakingPeriodTable.TODATE_FLD + ","
					+ IV_StockTakingPeriodTable.CCNID_FLD + ","
					+ IV_StockTakingPeriodTable.CLOSED_FLD + ")"
					+ "VALUES(?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.DESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD, OleDbType.Date));
				if (objObject.StockTakingDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD].Value = objObject.StockTakingDate;
				}
				else
					ocmdPCS.Parameters[IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.FROMDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.FROMDATE_FLD].Value = objObject.FromDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.TODATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.TODATE_FLD].Value = objObject.ToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.CLOSED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.CLOSED_FLD].Value = objObject.Closed;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
//				ocmdPCS.ExecuteNonQuery();
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
		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + IV_StockTakingPeriodTable.TABLE_NAME + " WHERE  " + "StockTakingPeriodID" + "=" + pintID.ToString();
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
					+ IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD + ","
					+ IV_StockTakingPeriodTable.DESCRIPTION_FLD + ","
					+ IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD + ","
					+ IV_StockTakingPeriodTable.FROMDATE_FLD + ","
					+ IV_StockTakingPeriodTable.TODATE_FLD + ","
					+ IV_StockTakingPeriodTable.CLOSED_FLD + ","
					+ IV_StockTakingPeriodTable.CCNID_FLD
					+ " FROM " + IV_StockTakingPeriodTable.TABLE_NAME
					+ " WHERE " + IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_StockTakingPeriodVO objObject = new IV_StockTakingPeriodVO();

				while (odrPCS.Read())
				{
					objObject.StockTakingPeriodID = int.Parse(odrPCS[IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD].ToString());
					objObject.Description = odrPCS[IV_StockTakingPeriodTable.DESCRIPTION_FLD].ToString();
					try
					{
						objObject.StockTakingDate = DateTime.Parse(odrPCS[IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD].ToString());
					}
					catch{}
					objObject.FromDate = DateTime.Parse(odrPCS[IV_StockTakingPeriodTable.FROMDATE_FLD].ToString());
					objObject.ToDate = DateTime.Parse(odrPCS[IV_StockTakingPeriodTable.TODATE_FLD].ToString());
					objObject.CCNID = int.Parse(odrPCS[IV_StockTakingPeriodTable.CCNID_FLD].ToString());
					try
					{
						objObject.Closed = (bool) odrPCS[IV_StockTakingPeriodTable.CLOSED_FLD];
					}
					catch{}

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

		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			IV_StockTakingPeriodVO objObject = (IV_StockTakingPeriodVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE IV_StockTakingPeriod SET "
					+ IV_StockTakingPeriodTable.DESCRIPTION_FLD + "=   ?" + ","
					+ IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD + "=   ?" + ","
					+ IV_StockTakingPeriodTable.FROMDATE_FLD + "=   ?" + ","
					+ IV_StockTakingPeriodTable.TODATE_FLD + "=   ?" + ","
					+ IV_StockTakingPeriodTable.CLOSED_FLD + "=   ?" + ","
					+ IV_StockTakingPeriodTable.CCNID_FLD + "=  ?"
					+ " WHERE " + IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.DESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD, OleDbType.Date));
				if (objObject.StockTakingDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD].Value = objObject.StockTakingDate;
				}
				else
					ocmdPCS.Parameters[IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.FROMDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.FROMDATE_FLD].Value = objObject.FromDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.TODATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.TODATE_FLD].Value = objObject.ToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.CLOSED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.CLOSED_FLD].Value = objObject.Closed;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD].Value = objObject.StockTakingPeriodID;

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
		/// UpdateStockTakingPeriod
		/// </summary>
		/// <param name="pobjObjecVO"></param>
		/// <author>Trada</author>
		/// <date>Thursday, July 27 2006</date>
		public void UpdateStockTakingPeriod(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".UpdateStockTakingPeriod()";

			IV_StockTakingPeriodVO objObject = (IV_StockTakingPeriodVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE IV_StockTakingPeriod SET "
					+ IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD + "=   ?" + ","
					+ IV_StockTakingPeriodTable.FROMDATE_FLD + "=   ?" + ","
					+ IV_StockTakingPeriodTable.TODATE_FLD + "=   ?" + ","
					+ IV_StockTakingPeriodTable.CLOSED_FLD + "=   ?" + ","
					+ IV_StockTakingPeriodTable.CCNID_FLD + "=  ?"
					+ " WHERE " + IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD, OleDbType.Date));
				if (objObject.StockTakingDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD].Value = objObject.StockTakingDate;
				}
				else
					ocmdPCS.Parameters[IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.FROMDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.FROMDATE_FLD].Value = objObject.FromDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.TODATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.TODATE_FLD].Value = objObject.ToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD].Value = objObject.StockTakingPeriodID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingPeriodTable.CLOSED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[IV_StockTakingPeriodTable.CLOSED_FLD].Value = objObject.Closed;

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
					+ IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD + ","
					+ IV_StockTakingPeriodTable.DESCRIPTION_FLD + ","
					+ IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD + ","
					+ IV_StockTakingPeriodTable.FROMDATE_FLD + ","
					+ IV_StockTakingPeriodTable.TODATE_FLD + ","
					+ IV_StockTakingPeriodTable.CLOSED_FLD + ","
					+ IV_StockTakingPeriodTable.CCNID_FLD
					+ " FROM " + IV_StockTakingPeriodTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_StockTakingPeriodTable.TABLE_NAME);

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
		/// GetDataByStockTakingID
		/// </summary>
		/// <param name="pintStockTakingMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, July 24 2006</date>
		public DataSet GetDataByStockTakingID(int pintStockTakingMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDataByStockTakingID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT StockTakingMasterID, STM.Code, STM.DepartmentID, STM.ProductionLineID, STM.LocationID, STM.BinID,"
					+ " D.Code Department, PL.Code ProductionLine, L.Code Location, B.Code Bin,"
					+ " STP." + IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD + ","
					+ " STP." + IV_StockTakingPeriodTable.DESCRIPTION_FLD + ","
					+ " STM." + IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD + ","
					+ " STP." + IV_StockTakingPeriodTable.FROMDATE_FLD + ","
					+ " STP." + IV_StockTakingPeriodTable.TODATE_FLD + ","
					+ " STP." + IV_StockTakingPeriodTable.CLOSED_FLD + ","
					+ " CCN." + MST_CCNTable.CODE_FLD + " CCN, ISNULL(STP.Closed,0) Closed, "
					+ " STP." + IV_StockTakingPeriodTable.CCNID_FLD
					+ " FROM IV_StockTakingMaster STM"
					+ " INNER JOIN IV_StockTakingPeriod STP ON STM.StockTakingPeriodID = STP.StockTakingPeriodID"
					+ " INNER JOIN " + MST_CCNTable.TABLE_NAME + " CCN ON CCN." + MST_CCNTable.CCNID_FLD
					+ " = " + " STP." + IV_StockTakingPeriodTable.CCNID_FLD
					+ " INNER JOIN MST_Department D ON D.DepartmentID=STM.DepartmentID"
					+ " INNER JOIN PRO_ProductionLine PL ON PL.ProductionLineID=STM.ProductionLineID"
					+ " INNER JOIN MST_Location L ON L.LocationID=STM.LocationID"
					+ " INNER JOIN MST_Bin B ON B.BinID=STM.BinID"
					+ " WHERE StockTakingMasterID" + " = " + pintStockTakingMasterID.ToString()
					+ " SELECT StockTakingMasterID,"
					+ " ST." + IV_StockTakingTable.SLIPCODE_FLD + ","
					+ " P.StockTakingCode,"
					//+ " DE." + MST_DepartmentTable.CODE_FLD + Constants.WHITE_SPACE + MST_DepartmentTable.TABLE_NAME + MST_DepartmentTable.CODE_FLD + ", "
					//+ " PL." + PRO_ProductionLineTable.CODE_FLD + Constants.WHITE_SPACE + PRO_ProductionLineTable.TABLE_NAME + PRO_ProductionLineTable.CODE_FLD + ", "
					//+ " L." + MST_LocationTable.CODE_FLD + Constants.WHITE_SPACE + MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD + ", "
					//+ " B." + MST_BINTable.CODE_FLD + Constants.WHITE_SPACE + MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD + ", " 
					+ " CA." + ITM_CategoryTable.CODE_FLD + Constants.WHITE_SPACE + ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ", "
					+ " P." + ITM_ProductTable.CODE_FLD + ", "
					+ " P." + ITM_ProductTable.DESCRIPTION_FLD + ", "
					+ " P." + ITM_ProductTable.REVISION_FLD + ", "
					+ " itm_source.code Source,itm_productType.Code ProductType,mst_party.code Vendor, "
					+ " UM." + MST_UnitOfMeasureTable.CODE_FLD + Constants.WHITE_SPACE + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD + ", "
					+ " ST." + IV_StockTakingTable.STOCKTAKINGID_FLD + ","
					+ " ST." + IV_StockTakingTable.QUANTITY_FLD + ","
					+ " ST." + IV_StockTakingTable.BOOKQUANTITY_FLD + ","
//					+ " ST." + IV_StockTakingTable.STOCKTAKINGPERIODID_FLD + ","
//					+ " ST." + IV_StockTakingTable.DEPARTMENTID_FLD + ","
//					+ " ST." + IV_StockTakingTable.PRODUCTIONLINEID_FLD + ","
//					+ " ST." + IV_StockTakingTable.LOCATIONID_FLD + ","
//					+ " ST." + IV_StockTakingTable.BINID_FLD + ","
					+ " ST." + IV_StockTakingTable.PRODUCTID_FLD + ","
					+ " ST." + IV_StockTakingTable.STOCKUMID_FLD + ","
					+ " CO." + IV_CoutingMethodTable.CODE_FLD + Constants.WHITE_SPACE + IV_CoutingMethodTable.TABLE_NAME + IV_CoutingMethodTable.CODE_FLD + ", "
					+ " ST." + IV_StockTakingTable.NOTE_FLD + ","
					+ " ST." + IV_StockTakingTable.COUNTINGMETHODID_FLD
					+ " FROM " + IV_StockTakingTable.TABLE_NAME + " ST"
//					+ " LEFT JOIN " + MST_DepartmentTable.TABLE_NAME + " DE ON DE."
//					+ MST_DepartmentTable.DEPARTMENTID_FLD + " = ST." + IV_StockTakingTable.DEPARTMENTID_FLD
//					+ " LEFT JOIN " + PRO_ProductionLineTable.TABLE_NAME + " PL ON PL."
//					+ PRO_ProductionLineTable.PRODUCTIONLINEID_FLD + " = ST." + IV_StockTakingTable.PRODUCTIONLINEID_FLD
//					+ " LEFT JOIN " + MST_LocationTable.TABLE_NAME + " L ON L."
//					+ MST_LocationTable.LOCATIONID_FLD + " = ST." + IV_StockTakingTable.LOCATIONID_FLD
//					+ " LEFT JOIN " + MST_BINTable.TABLE_NAME + " B ON B."
//					+ MST_BINTable.BINID_FLD + " = ST." + IV_StockTakingTable.BINID_FLD
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME + " P ON P."
					+ ITM_ProductTable.PRODUCTID_FLD + " = ST." + IV_StockTakingTable.PRODUCTID_FLD
					+ " LEFT JOIN " + ITM_CategoryTable.TABLE_NAME + " CA ON CA."
					+ ITM_CategoryTable.CATEGORYID_FLD + " = P." + ITM_ProductTable.CATEGORYID_FLD
					+ " LEFT JOIN " + MST_UnitOfMeasureTable.TABLE_NAME + " UM ON UM."
					+ MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD + " = P." + ITM_ProductTable.STOCKUMID_FLD
					+ " LEFT JOIN " + IV_CoutingMethodTable.TABLE_NAME + " CO ON CO."
					+ IV_CoutingMethodTable.COUNTINGMETHODID_FLD + " = ST." + IV_StockTakingTable.COUNTINGMETHODID_FLD
					+ " LEFT JOIN itm_source on P.SourceID = itm_source.SourceID"
					+ " LEFT JOIN itm_productType on P.ProductTypeID = itm_productType.ProductTypeID"
					+ " LEFT JOIN mst_party on P.PrimaryVendorID = mst_party.PartyID"
					+ " WHERE StockTakingMasterID = " + pintStockTakingMasterID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_StockTakingPeriodTable.TABLE_NAME);

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
					+ IV_StockTakingPeriodTable.STOCKTAKINGPERIODID_FLD + ","
					+ IV_StockTakingPeriodTable.DESCRIPTION_FLD + ","
					+ IV_StockTakingPeriodTable.STOCKTAKINGDATE_FLD + ","
					+ IV_StockTakingPeriodTable.FROMDATE_FLD + ","
					+ IV_StockTakingPeriodTable.TODATE_FLD + ","
					+ IV_StockTakingPeriodTable.CLOSED_FLD + ","
					+ IV_StockTakingPeriodTable.CCNID_FLD;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, IV_StockTakingPeriodTable.TABLE_NAME);

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
        ///     Call store procedure to update cache, balance, close period (stock taking and working period)
        ///     then active next working period
        /// </summary>
        /// <param name="periodId">Stock Taking Period</param>
        /// <param name="stockTakingDate">Effect Date</param>
	    public void CloseStockTaking(int periodId, DateTime stockTakingDate)
	    {
            const string methodName = THIS + ".CloseStockTaking()";

			//prepare value for parameters
			OleDbConnection connection = null;
			try
			{
			    var nextMonth = stockTakingDate.AddMonths(1);
			    connection = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				var command = new OleDbCommand("spCloseStockTakingPeriod", connection) {CommandType = CommandType.StoredProcedure};
			    command.Parameters.Add(new OleDbParameter("@PeriodId", OleDbType.Integer)).Value = periodId;
                command.Parameters.Add(new OleDbParameter("@EffectDate", OleDbType.DBDate)).Value = stockTakingDate;
                command.Parameters.Add(new OleDbParameter("@NextMonth", OleDbType.DBDate)).Value = nextMonth;

			    command.Connection.Open();
				command.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, methodName, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, methodName, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, methodName, ex);
			}

			finally
			{
				if (connection != null)
				{
					if (connection.State != ConnectionState.Closed)
					{
						connection.Close();
					}
				}
			}
	    }

        /// <summary>
        ///     Update begin DCP report stock
        /// </summary>
        /// <param name="periodId">Stock Taking Period</param>
        /// <param name="effectDate">Effect Date</param>
        public void UpdateBeginStock(int periodId, DateTime effectDate)
        {
            const string methodName = THIS + ".UpdateBeginStock()";

            OleDbConnection connection = null;
            try
            {
                connection = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                var command = new OleDbCommand("spUpdateBeginStock", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new OleDbParameter("@PeriodId", OleDbType.Integer)).Value = periodId;
                command.Parameters.Add(new OleDbParameter("@EffectDate", OleDbType.DBDate)).Value = effectDate;
                command.Parameters.Add(new OleDbParameter("@UserName", OleDbType.VarWChar)).Value = SystemProperty.UserName;

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, methodName, ex);
            }

            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.DUPLICATE_KEY, methodName, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, methodName, ex);
            }

            finally
            {
                if (connection != null)
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }
	}
}