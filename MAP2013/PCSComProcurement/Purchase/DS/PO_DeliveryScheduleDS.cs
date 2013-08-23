using System;
using System.Data;
using System.Data.OleDb;
using System.Text;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProcurement.Purchase.DS
{
	
	public class PO_DeliveryScheduleDS 
	{
		public PO_DeliveryScheduleDS()
		{
		}

		private const string THIS = "PCSComProcurement.Purchase.DS.DS.PO_DeliveryScheduleDS";
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				PO_DeliveryScheduleVO objObject = (PO_DeliveryScheduleVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO PO_DeliverySchedule("
					+ PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ","
					+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.ADJUSTMENT_FLD + ","
					+ PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.DELIVERYLINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.DELIVERYLINE_FLD].Value = objObject.DeliveryLine;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.SCHEDULEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Value = objObject.ScheduleDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Value = objObject.DeliveryQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD].Value = objObject.ReceivedQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.ADJUSTMENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.ADJUSTMENT_FLD].Value = objObject.Adjustment;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD].Value = objObject.PurchaseOrderDetailID;


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

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + PO_DeliveryScheduleTable.TABLE_NAME + " WHERE  " + "DeliveryScheduleID" + "=" + pintID.ToString();
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

		public void DeleteDeliveryDetail(int pintPurchaseOrderLineID)
		{
			const string METHOD_NAME = THIS + ".DeleteDeliveryDetail()";
			string strSql = String.Empty;
			strSql = "DELETE " + PO_DeliveryScheduleTable.TABLE_NAME
				+ " WHERE  " + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + "=" + pintPurchaseOrderLineID.ToString();
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
					+ PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ","
					+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.ADJUSTMENT_FLD + ","
					+ PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_DeliveryScheduleTable.TABLE_NAME
					+ " WHERE " + PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_DeliveryScheduleVO objObject = new PO_DeliveryScheduleVO();

				while (odrPCS.Read())
				{
					objObject.DeliveryScheduleID = int.Parse(odrPCS[PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString());
					objObject.DeliveryLine = int.Parse(odrPCS[PO_DeliveryScheduleTable.DELIVERYLINE_FLD].ToString());
					objObject.ScheduleDate = DateTime.Parse(odrPCS[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString());
					objObject.DeliveryQuantity = Decimal.Parse(odrPCS[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
					try
					{
						objObject.ReceivedQuantity = Decimal.Parse(odrPCS[PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD].ToString());
					}
					catch
					{
						objObject.ReceivedQuantity = 0;
					}
					try
					{
						objObject.Adjustment = Decimal.Parse(odrPCS[PO_DeliveryScheduleTable.ADJUSTMENT_FLD].ToString());
					}
					catch
					{
						objObject.Adjustment = 0;
					}
					objObject.PurchaseOrderDetailID = int.Parse(odrPCS[PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD].ToString());

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

			PO_DeliveryScheduleVO objObject = (PO_DeliveryScheduleVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE PO_DeliverySchedule SET "
					+ PO_DeliveryScheduleTable.DELIVERYLINE_FLD + "=   ?" + ","
					+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "=   ?" + ","
					+ PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + "=   ?" + ","
					+ PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + "=   ?" + ","
					+ PO_DeliveryScheduleTable.ADJUSTMENT_FLD + "=   ?" + ","
					+ PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + "=  ?"
					+ " WHERE " + PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.DELIVERYLINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.DELIVERYLINE_FLD].Value = objObject.DeliveryLine;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.SCHEDULEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Value = objObject.ScheduleDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Value = objObject.DeliveryQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD].Value = objObject.ReceivedQuantity;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.ADJUSTMENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.ADJUSTMENT_FLD].Value = objObject.Adjustment;

                ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD].Value = objObject.PurchaseOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].Value = objObject.DeliveryScheduleID;


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
					+ PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ","
					+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.ADJUSTMENT_FLD + ","
					+ PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_DeliveryScheduleTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_DeliveryScheduleTable.TABLE_NAME);

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

		public DataSet List(string pstrScheduleIDs)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ","
					+ PO_DeliveryScheduleTable.STARTDATE_FLD + ","
					+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.ADJUSTMENT_FLD + ","
					+ PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_DeliveryScheduleTable.TABLE_NAME
					+ " WHERE " + PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + " IN (" + pstrScheduleIDs + ")";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_DeliveryScheduleTable.TABLE_NAME);

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

		public DataSet GetDeliverySchedule(int pintPurchaseOrderLineID)
		{
			const string METHOD_NAME = THIS + ".GetDeliverySchedule()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ","
					+ "0 Warning,"
					+ PO_DeliveryScheduleTable.STARTDATE_FLD + ","
					+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.ADJUSTMENT_FLD + ","
					+ PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_DeliveryScheduleTable.TABLE_NAME
					+ " WHERE " + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + "=" + pintPurchaseOrderLineID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_DeliveryScheduleTable.TABLE_NAME);

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
					+ PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ","
					+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_DeliveryScheduleTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, PO_DeliveryScheduleTable.TABLE_NAME);

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

		public void UpdateDeliveryDataSet(DataSet pData, int pintPurchaseOrderDetailID)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				UpdateDataSet(pData);
				strSql = "SELECT "
					+ PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ","
					+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.ADJUSTMENT_FLD + ","
					+ PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_DeliveryScheduleTable.TABLE_NAME;
				strSql += " WHERE " + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + "=" + pintPurchaseOrderDetailID;
				pData.Clear();
				odadPCS.SelectCommand.CommandText = strSql;
				odadPCS.SelectCommand.CommandType = CommandType.Text;
				odadPCS.Fill(pData, pData.Tables[0].TableName);
				pData.AcceptChanges();
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

		public int GetMaxDeliveryScheduleLine(int pintPurchaseOrderDetailID)
		{
			const string METHOD_NAME = THIS + ".GetMaxDeliveryScheduleLine()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT max ("
					+ PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ")"
					+ " FROM " + PO_DeliveryScheduleTable.TABLE_NAME
					+ " WHERE " + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + "=" + pintPurchaseOrderDetailID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				string strResult = ocmdPCS.ExecuteScalar().ToString();
				int intResult;
				try
				{
					intResult = int.Parse(strResult);
				}
				catch
				{
					intResult = 0;
				}
				return intResult;
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

		public decimal GetTotalDeliveryQuantityOfLine(int pintPODetailID)
		{
			const string METHOD_NAME = THIS + ".GetTotalDeliveryQuantityOfLine()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT ISNULL(SUM(ISNULL(DeliveryQuantity,0)),0) AS DeliveryQuantity"
					+ " FROM PO_DeliverySchedule WHERE PurchaseOrderDetailID = " + pintPODetailID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				try
				{
					return Convert.ToDecimal(objResult);
				}
				catch
				{
					return decimal.Zero;
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

		public void UpdateSubstractQuantityReceipt(int printDeliveryScheduleId, decimal decQuantityReceipt)
		{
			const string METHOD_NAME = THIS + ".UpdateSubstractQuantityReceipt()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				strSql = "UPDATE PO_DeliverySchedule SET "
					+ PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + "=" + PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + " -   ?"
					+ " WHERE " + PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD].Value = decQuantityReceipt;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].Value = printDeliveryScheduleId;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
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
		public void UpdateCancelDelivery(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql = "SELECT "
					+ PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ","
					+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.ADJUSTMENT_FLD + ","
					+ PO_DeliveryScheduleTable.CANCELDELIVERY_FLD + ","
					+ PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_DeliveryScheduleTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, PO_DeliveryScheduleTable.TABLE_NAME);
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
		public DataSet SearchDeliverySchedule(DateTime pdtmFromDate, DateTime pdtmToDate, int pintPOMasterID,
			int pintCategoryID, string pstrProductID)
		{
			const string METHOD_NAME = THIS + ".SearchDeliverySchedule()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT M.Code PONo, D.Line, S.DeliveryLine, C.Code ITM_CategoryCode, P.Code, P.Description, Revision, U.Code BuyingUM, "
					+ " S.ScheduleDate, D.OrderQuantity, S.DeliveryQuantity, "
					+ " ISNULL(S.CancelDelivery,0) CancelDelivery,"
					+ " P.CategoryID, D.PurchaseOrderMasterID, D.PurchaseOrderDetailID, S.DeliveryScheduleID, P.ProductID,"
					+ " ReceivedQuantity, Adjustment, StartDate"
					+ " FROM PO_PurchaseOrderMaster M JOIN PO_PurchaseOrderDetail D"
					+ " ON M.PurchaseOrderMasterID = D.PurchaseOrderMasterID"
					+ " JOIN PO_DeliverySchedule S ON D.PurchaseOrderDetailID = S.PurchaseOrderDetailID"
					+ " JOIN ITM_Product P ON D.ProductID = P.ProductID"
					+ " LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ " LEFT JOIN MST_UnitOfMeasure U ON D.BuyingUMID = U.UnitOfMeasureID"
					+ " WHERE 1=1";
				if (pdtmFromDate != DateTime.MinValue)
					strSql += " AND ScheduleDate >= ?";
				if (pdtmToDate != DateTime.MinValue)
					strSql += " AND ScheduleDate <= ?";
				if (pintPOMasterID > 0)
					strSql += " AND D.PurchaseOrderMasterID = " + pintPOMasterID;
				if (pintCategoryID > 0)
					strSql += " AND P.CategoryID = " + pintCategoryID;
				if (pstrProductID != null && pstrProductID != string.Empty)
					strSql += " AND P.ProductID IN (" + pstrProductID + ")";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				if (pdtmFromDate != DateTime.MinValue)
					ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				if (pdtmToDate != DateTime.MinValue)
					ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_DeliveryScheduleTable.TABLE_NAME);

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

		public void UpdateDelivery(string pstrCancelList, string pstrApprovedList)
		{
			const string METHOD_NAME = THIS + ".UpdateDelivery()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = string.Empty;
				if (pstrCancelList != string.Empty)
					strSql += "UPDATE PO_DeliverySchedule SET CancelDelivery = 1"
						+ " WHERE DeliveryScheduleID IN (" + pstrCancelList.Substring(0, pstrCancelList.Length - 1) + ");";
				if (pstrApprovedList != string.Empty)
					strSql += "UPDATE PO_DeliverySchedule SET CancelDelivery = 0"
						+ " WHERE DeliveryScheduleID IN (" + pstrApprovedList.Substring(0, pstrApprovedList.Length - 1) + ");";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
				ocmdPCS = null;
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
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
		public DataSet ListForImport(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".ListForImport()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ "A." + PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ "A." + PO_DeliveryScheduleTable.STARTDATE_FLD + ","
					+ "A." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ "A." + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ "A." + PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ","
					+ "A." + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_DeliveryScheduleTable.TABLE_NAME + " A "
					+ " INNER JOIN " + PO_PurchaseOrderDetailTable.TABLE_NAME + " B "
					+ " ON " + "A." + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + "=" + "B." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
					+ " INNER JOIN " + PO_PurchaseOrderMasterTable.TABLE_NAME + " C "
					+ " ON " + "B." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + "C." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD
					+ " WHERE C." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + "=?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				ocmdPCS.Parameters.AddWithValue(PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD, pintMasterID);
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_DeliveryScheduleTable.TABLE_NAME);

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
		public void UpdateInsertedRowInDataSet(DataSet pData, int pintPOMasterID)
		{
			const string METHOD_NAME = THIS + ".UpdateInsertedRowInDataSet()";
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				string strSql=	"SELECT "
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ","
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD
					+ " FROM " + PO_DeliveryScheduleTable.TABLE_NAME; 

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				//odadPCS.Update(pData.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.Added));
				odadPCS.Update(pData.Tables[0]);
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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

		public DataSet GetDeliveryScheduleByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryScheduleByMaster()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ","
					+ "0 Warning,"
					+ PO_DeliveryScheduleTable.STARTDATE_FLD + ","
					+ PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ "A." + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.ADJUSTMENT_FLD + ","
					+ "A." + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_DeliveryScheduleTable.TABLE_NAME + " A"
					+ " JOIN " + PO_PurchaseOrderDetailTable.TABLE_NAME + " B"
					+ " ON A." + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + "= B." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + pintMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_DeliveryScheduleTable.TABLE_NAME);

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
		public void UpdateDeletedRowInDataSet(DataSet pData, int pintSaleOrderMasterID)
		{
			const string METHOD_NAME = THIS + ".UpdateDeletedRowInDataSet()";
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				string strSql=	"SELECT "
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ","
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ","
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD
					+ " FROM " + PO_DeliveryScheduleTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData.Tables[0].Select(string.Empty,string.Empty,DataViewRowState.Deleted));
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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
		public void DeleteByPOMaster(string pstrMasterID)
		{
			const string METHOD_NAME = THIS + ".DeleteByPOMaster()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "DELETE " + PO_DeliveryScheduleTable.TABLE_NAME
					+ " WHERE " + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + " IN"
					+ " (SELECT DISTINCT " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + " IN ("
					+ pstrMasterID + " ))";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

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
        public DataTable GetDiffrentDelivery(DateTime fromDate, DateTime toDate)
        {
            const string METHOD_NAME = THIS + ".GetDiffrentDelivery()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                var strSql = new StringBuilder();
                strSql.AppendLine(" DECLARE @MyFromDate AS DATETIME");
                strSql.AppendLine(" DECLARE @MyToDate AS DATETIME");
                strSql.AppendLine(" SET @MyFromDate = ?");
                strSql.AppendLine(" SET @MyToDate = ?");
                strSql.AppendLine(" SELECT  DL.ScheduleDate, POD.ProductID, POM.PartyID,");
                strSql.AppendLine("         DL.DeliveryQuantity, DL.DeliveryScheduleID,");
                strSql.AppendLine("         X.DeliveryQuantity NewQty");
                strSql.AppendLine(" FROM    PO_DeliverySchedule DL");
                strSql.AppendLine("         INNER JOIN PO_PurchaseOrderDetail POD ON POD.PurchaseOrderDetailID = DL.PurchaseOrderDetailID");
                strSql.AppendLine("         INNER JOIN PO_PurchaseOrderMaster POM ON POM.PurchaseOrderMasterID = POD.PurchaseOrderMasterID");
                strSql.AppendLine("         INNER JOIN ( SELECT POM.PartyID, DL.DeliveryQuantity, DL.ScheduleDate, POD.ProductID");
                strSql.AppendLine("                      FROM   PO_DeliverySchedule DL");
                strSql.AppendLine("                             INNER JOIN PO_PurchaseOrderDetail POD ON POD.PurchaseOrderDetailID = DL.PurchaseOrderDetailID");
                strSql.AppendLine("                             INNER JOIN PO_PurchaseOrderMaster POM ON POM.PurchaseOrderMasterID = POD.PurchaseOrderMasterID");
                strSql.AppendLine("                      WHERE  POD.ApproverID IS NULL");
                strSql.AppendLine("                             AND DL.ScheduleDate >= @MyFromDate");
                strSql.AppendLine("                             AND DL.ScheduleDate <= @MyToDate");
                strSql.AppendLine("                    ) X ON X.PartyID = POM.PartyID");
                strSql.AppendLine("                           AND X.ProductID = POD.ProductID");
                strSql.AppendLine("                           AND X.ScheduleDate = DL.ScheduleDate");
                strSql.AppendLine(" WHERE   POD.ApproverID IS NOT NULL");
                strSql.AppendLine("         AND ISNULL(DL.ReceivedQuantity, 0) = 0");
                strSql.AppendLine("         AND X.DeliveryQuantity <> DL.DeliveryQuantity");
                strSql.AppendLine("         AND DL.ScheduleDate >= @MyFromDate");
                strSql.AppendLine("         AND DL.ScheduleDate <= @MyToDate");
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql.ToString(), oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = fromDate;
                ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = toDate;
                ocmdPCS.Connection.Open();

                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PO_DeliveryScheduleTable.TABLE_NAME);

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
        public DataTable GetNotExistDelivery(DateTime fromDate, DateTime toDate)
        {
            const string METHOD_NAME = THIS + ".GetDiffrentDelivery()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                var strSql = new StringBuilder();
                strSql.AppendLine(" DECLARE @MyFromDate AS DATETIME");
                strSql.AppendLine(" DECLARE @MyToDate AS DATETIME");
                strSql.AppendLine(" SET @MyFromDate = ?");
                strSql.AppendLine(" SET @MyToDate = ?");
                strSql.AppendLine(" SELECT  DL.ScheduleDate, POD.ProductID, POM.PartyID,");
                strSql.AppendLine("         SUM(DL.DeliveryQuantity) DeliveryQuantity,");
                strSql.AppendLine("         ISNULL(SUM(ISNULL(X.DeliveryQuantity, 0)), 0) NewQty");
                strSql.AppendLine(" FROM    PO_DeliverySchedule DL");
                strSql.AppendLine("         INNER JOIN PO_PurchaseOrderDetail POD ON POD.PurchaseOrderDetailID = DL.PurchaseOrderDetailID");
                strSql.AppendLine("         INNER JOIN PO_PurchaseOrderMaster POM ON POM.PurchaseOrderMasterID = POD.PurchaseOrderMasterID");
                strSql.AppendLine("         LEFT JOIN ( SELECT  POM.PartyID, DL.DeliveryQuantity, DL.ScheduleDate,");
                strSql.AppendLine("                             POD.ApproverID, POD.ProductID");
                strSql.AppendLine("                     FROM    PO_DeliverySchedule DL");
                strSql.AppendLine("                             INNER JOIN PO_PurchaseOrderDetail POD ON POD.PurchaseOrderDetailID = DL.PurchaseOrderDetailID");
                strSql.AppendLine("                             INNER JOIN PO_PurchaseOrderMaster POM ON POM.PurchaseOrderMasterID = POD.PurchaseOrderMasterID");
                strSql.AppendLine("                     WHERE   POD.ApproverID IS NULL");
                strSql.AppendLine("                             AND DL.ScheduleDate >= @MyFromDate");
                strSql.AppendLine("                             AND DL.ScheduleDate <= @MyToDate");
                strSql.AppendLine("                   ) X ON X.PartyID = POM.PartyID");
                strSql.AppendLine("                          AND X.ProductID = POD.ProductID");
                strSql.AppendLine("                          AND X.ScheduleDate = DL.ScheduleDate");
                strSql.AppendLine(" WHERE   POD.ApproverID IS NOT NULL");
                strSql.AppendLine("         AND X.DeliveryQuantity <> DL.DeliveryQuantity");
                strSql.AppendLine("         AND DL.ScheduleDate >= @MyFromDate");
                strSql.AppendLine("         AND DL.ScheduleDate <= @MyToDate");
                strSql.AppendLine(" GROUP BY POM.PartyID, POD.ProductID, DL.ScheduleDate, X.DeliveryQuantity");
                strSql.AppendLine(" HAVING  ISNULL(SUM(ISNULL(DL.ReceivedQuantity, 0)), 0) = 0");
                strSql.AppendLine("         AND ISNULL(SUM(ISNULL(X.DeliveryQuantity, 0)), 0) = 0");
                strSql.AppendLine("         AND ISNULL(SUM(ISNULL(DL.DeliveryQuantity, 0)), 0) <> 0");
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql.ToString(), oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = fromDate;
                ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = toDate;
                ocmdPCS.Connection.Open();

                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PO_DeliveryScheduleTable.TABLE_NAME);

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
	}
}