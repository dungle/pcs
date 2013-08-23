using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProduction.DCP.DS
{
	public class PRO_DCPResultDetailDS 
	{
		public PRO_DCPResultDetailDS()
		{
		}

		private const string THIS = "PCSComProduction.DCP.DS.PRO_DCPResultDetailDS";

		///    <summary>
		///       This method uses to add data to PRO_DCPResultDetail
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
		///    </History>
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				PRO_DCPResultDetailVO objObject = (PRO_DCPResultDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO PRO_DCPResultDetail("
					+ PRO_DCPResultDetailTable.STARTTIME_FLD + ","
					+ PRO_DCPResultDetailTable.ENDTIME_FLD + ","
					+ PRO_DCPResultDetailTable.TOTALSECOND_FLD + ","
					+ PRO_DCPResultDetailTable.QUANTITY_FLD + ","
					+ PRO_DCPResultDetailTable.PERCENTAGE_FLD + ","
					+ PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.STARTTIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.STARTTIME_FLD].Value = objObject.StartTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.ENDTIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.ENDTIME_FLD].Value = objObject.EndTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.TOTALSECOND_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Value = objObject.TotalSecond;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.PERCENTAGE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.PERCENTAGE_FLD].Value = objObject.Percentage;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].Value = objObject.DCPResultMasterID;


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
		///       This method uses to add data to PRO_DCPResultDetail
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
		///    </History>
		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + PRO_DCPResultDetailTable.TABLE_NAME + " WHERE  " + "DCPResultDetailID" + "=" + pintID.ToString();
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

		
		/// <summary>
		/// Delete result by Cycle and Work Center and month
		/// </summary>
		/// <param name="pintCycleID">DCP Cycle</param>
		/// <param name="pintWorkCenterID">Main Work Center</param>
		/// <param name="pdtmMonth">Month to delete</param>
		public void Delete(int pintCycleID, int pintWorkCenterID, int pintShiftID, DateTime pdtmMonth)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DateTime dtmBegin = new DateTime(pdtmMonth.Year, pdtmMonth.Month, 1);
				DateTime dtmEnd = dtmBegin.AddMonths(1).AddDays(-1);
				string strSql = "DELETE PRO_DCPResultDetail WHERE DCPResultMasterID IN\n"
					+ " (SELECT DCPResultMasterID FROM PRO_DCPResultMaster\n"
					+ " WHERE DCOptionMasterID=" + pintCycleID + " AND WorkCenterID=" + pintWorkCenterID + ")\n"
					+ " AND ShiftID = " + pintShiftID + "\n"
					+ " AND WorkingDate >= ? \n"
					+ " AND WorkingDate <= ?; \n";
				strSql += "DELETE PRO_DCPResultMaster\n"
					+ " WHERE DCOptionMasterID=" + pintCycleID + " AND WorkCenterID=" + pintWorkCenterID + "\n"
					+ " AND MasterShiftID = " + pintShiftID + "\n"
					+ " AND StartDateTime >= ? \n"
					+ " AND DueDateTime <= ?";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate1", OleDbType.Date)).Value = dtmBegin;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate1", OleDbType.Date)).Value = dtmEnd;
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate2", OleDbType.Date)).Value = dtmBegin;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate2", OleDbType.Date)).Value = dtmEnd;
				
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
				ocmdPCS = null;
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
				else
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
		///       This method uses to add data to PRO_DCPResultDetail
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
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
					+ PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD + ","
					+ PRO_DCPResultDetailTable.STARTTIME_FLD + ","
					+ PRO_DCPResultDetailTable.ENDTIME_FLD + ","
					+ PRO_DCPResultDetailTable.TOTALSECOND_FLD + ","
					+ PRO_DCPResultDetailTable.QUANTITY_FLD + ","
					+ PRO_DCPResultDetailTable.PERCENTAGE_FLD + ","
					+ PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD
					+ " FROM " + PRO_DCPResultDetailTable.TABLE_NAME
					+ " WHERE " + PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_DCPResultDetailVO objObject = new PRO_DCPResultDetailVO();

				while (odrPCS.Read())
				{
					objObject.DCPResultDetailID = int.Parse(odrPCS[PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD].ToString().Trim());
					objObject.StartTime = DateTime.Parse(odrPCS[PRO_DCPResultDetailTable.STARTTIME_FLD].ToString().Trim());
					objObject.EndTime = DateTime.Parse(odrPCS[PRO_DCPResultDetailTable.ENDTIME_FLD].ToString().Trim());
					objObject.TotalSecond = Decimal.Parse(odrPCS[PRO_DCPResultDetailTable.TOTALSECOND_FLD].ToString().Trim());
					objObject.Quantity = Decimal.Parse(odrPCS[PRO_DCPResultDetailTable.QUANTITY_FLD].ToString().Trim());
					objObject.Percentage = Decimal.Parse(odrPCS[PRO_DCPResultDetailTable.PERCENTAGE_FLD].ToString().Trim());
					objObject.DCPResultMasterID = int.Parse(odrPCS[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString().Trim());

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
		///       This method uses to add data to PRO_DCPResultDetail
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
		///    </History>
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_DCPResultDetailVO objObject = (PRO_DCPResultDetailVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE PRO_DCPResultDetail SET "
					+ PRO_DCPResultDetailTable.STARTTIME_FLD + "=   ?" + ","
					+ PRO_DCPResultDetailTable.ENDTIME_FLD + "=   ?" + ","
					+ PRO_DCPResultDetailTable.TOTALSECOND_FLD + "=   ?" + ","
					+ PRO_DCPResultDetailTable.QUANTITY_FLD + "=   ?" + ","
					+ PRO_DCPResultDetailTable.PERCENTAGE_FLD + "=   ?" + ","
					+ PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=  ?"
					+ " WHERE " + PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.STARTTIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.STARTTIME_FLD].Value = objObject.StartTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.ENDTIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.ENDTIME_FLD].Value = objObject.EndTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.TOTALSECOND_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Value = objObject.TotalSecond;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.PERCENTAGE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.PERCENTAGE_FLD].Value = objObject.Percentage;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].Value = objObject.DCPResultMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD].Value = objObject.DCPResultDetailID;


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
		///       This method uses to add data to PRO_DCPResultDetail
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
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
					+ PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD + ","
					+ PRO_DCPResultDetailTable.STARTTIME_FLD + ","
					+ PRO_DCPResultDetailTable.ENDTIME_FLD + ","
					+ PRO_DCPResultDetailTable.TOTALSECOND_FLD + ","
					+ PRO_DCPResultDetailTable.QUANTITY_FLD + ","
					+ PRO_DCPResultDetailTable.PERCENTAGE_FLD + ","
					+ PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + ","
					+ PRO_DCPResultDetailTable.TYPE_FLD
					+ " FROM " + PRO_DCPResultDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_DCPResultDetailTable.TABLE_NAME);

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
		/// GetDCPResultDetailByMasterID
		/// </summary>
		/// <param name="pstrMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, April 25 2006</date>
		public DataSet GetDCPResultDetailByMasterID(string pstrMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDCPResultDetailByMasterID()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD + ","
					+ PRO_DCPResultDetailTable.STARTTIME_FLD + ","
					+ PRO_DCPResultDetailTable.ENDTIME_FLD + ","
					+ PRO_DCPResultDetailTable.TOTALSECOND_FLD + ","
					+ PRO_DCPResultDetailTable.QUANTITY_FLD + ","
					+ PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD
					+ " FROM " + PRO_DCPResultDetailTable.TABLE_NAME
					+ " WHERE " + PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + " IN (" + pstrMasterID + ")"
					+ " ORDER BY " + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + "," + PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD
					+ "," + PRO_DCPResultDetailTable.STARTTIME_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_DCPResultDetailTable.TABLE_NAME);

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


		///    <summary>
		///       This method uses to add data to PRO_DCPResultDetail
		///    </summary>
		///    <Inputs>
		///        PRO_DCPResultDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, September 05, 2005
		///    </History>
		public void UpdateDataSet(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				string strSql = "SELECT DCPResultDetailID, StartTime, EndTime, TotalSecond, Quantity, Percentage,"
					+ " DCPResultMasterID, Type, WorkingDate, Converted, WOGeneratedID, ShiftID, POGeneratedID,"
					+ " SafetyStockAmount, IsManual"
					+ " FROM PRO_DCPResultDetail";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odadPCS.SelectCommand.CommandTimeout = 1000;
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData, PRO_DCPResultDetailTable.TABLE_NAME);
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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
		/// UpdateDataSetManual
		/// </summary>
		/// <param name="pdstData"></param>
		/// <author>Trada</author>
		/// <date>Monday, April 24 2006</date>
		public void UpdateDataSetManual(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSetManual()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql = "SELECT "
					+ PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD + ","
					+ PRO_DCPResultDetailTable.STARTTIME_FLD + ","
					+ PRO_DCPResultDetailTable.ENDTIME_FLD + ","
					+ PRO_DCPResultDetailTable.TOTALSECOND_FLD + ","
					+ PRO_DCPResultDetailTable.QUANTITY_FLD + ","
					+ PRO_DCPResultDetailTable.PERCENTAGE_FLD + ","
					+ PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + ","
					+ PRO_DCPResultDetailTable.WORKINGDATE_FLD + ","
					+ PRO_DCPResultDetailTable.WOCONVERTED_FLD + ","
					+ PRO_DCPResultDetailTable.ISMANUAL_FLD + ","
					+ PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT + ","
					+ PRO_DCPResultDetailTable.SHIFTID_FLD
					+ "  FROM " + PRO_DCPResultDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData, PRO_DCPResultDetailTable.TABLE_NAME);

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

		public void DeleteOverData(string pstrResultMasterIDs, string pstrDeleteMasterID, string pstrResultDetailID)
		{
			const string METHOD_NAME = THIS + ".DeleteOverData()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "DELETE " + PRO_DCPResultDetailTable.TABLE_NAME
					+ " WHERE  " + PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + " IN (" + pstrResultMasterIDs + ")"
					+ " AND " + PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD + " NOT IN (" + pstrResultDetailID + ")"
					+ " AND " + PRO_DCPResultDetailTable.SHIFTID_FLD + " IS NULL;";
				strSql += " DELETE " + PRO_DCPResultMasterTable.TABLE_NAME
					+ " WHERE " + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + " IN (" + pstrDeleteMasterID + ")";
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.CommandTimeout = 1000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
				ocmdPCS = null;
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
				else
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
		public void UpdateOver(DataTable pdtbData)
		{
			const string METHOD_NAME = THIS + ".UpdateOver()";
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				string strSql = "SELECT DCPResultDetailID, TotalSecond, Quantity, DCPResultMasterID"
					+ " FROM PRO_DCPResultDetail";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odadPCS.SelectCommand.CommandTimeout = 1000;
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				odadPCS.Update(pdtbData);
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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

		public DataTable GetNotDeleteMasterID(string pstrMasterID, int pintCycleID)
		{
			const string METHOD_NAME = THIS + ".GetNotDeleteMasterID()";
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{
				string strSql = "SELECT DISTINCT D.DCPResultMasterID"
					+ " FROM PRO_DCPResultDetail D JOIN PRO_DCPResultMaster M ON D.DCPResultMasterID = M.DCPResultMasterID"
					+ " WHERE D.DCPResultMasterID IN (" + pstrMasterID + ")"
					+ " AND ShiftID IS NOT NULL"
					+ " AND M.DCOptionMasterID = " + pintCycleID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				DataTable dtbData = new DataTable();
				odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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
	}

}