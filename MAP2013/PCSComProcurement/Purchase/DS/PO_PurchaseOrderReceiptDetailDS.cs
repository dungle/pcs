using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;


namespace PCSComProcurement.Purchase.DS
{
	public class PO_PurchaseOrderReceiptDetailDS 
	{
		private const string THIS = "PCSComProcurement.Purchase.DS.DS.PO_PurchaseOrderReceiptDetailDS";
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				PO_PurchaseOrderReceiptDetailVO objObject = (PO_PurchaseOrderReceiptDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO PO_PurchaseOrderReceiptDetail("
					+ PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.LOT_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.BINID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD, OleDbType.Integer));
				if (objObject.LocationId > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD].Value = objObject.LocationId;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD].Value = DBNull.Value;

				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].Value = objObject.ReceiveQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD].Value = objObject.PurchaseOrderReceiptID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				if (objObject.ProductID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				if (objObject.PurchaseOrderMasterID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD].Value = objObject.BuyingUMID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				if (objObject.PurchaseOrderDetailID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD].Value = objObject.PurchaseOrderDetailID;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.LOT_FLD, OleDbType.VarWChar));
				if (objObject.Lot != string.Empty)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.LOT_FLD].Value = objObject.Lot;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.LOT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD, OleDbType.TinyInt));
				if (objObject.QAStatus > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD].Value = objObject.QAStatus;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD, OleDbType.VarWChar));
				if (objObject.Serial != string.Empty)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD].Value = objObject.Serial;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.BINID_FLD, OleDbType.Integer));
				if (objObject.BinId > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.BINID_FLD].Value = objObject.BinId;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.BINID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].Value = objObject.UmRate;
				
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
			strSql = "DELETE " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME + " WHERE  " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD + "=" + pintID.ToString();
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
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ PO_PurchaseOrderReceiptDetailTable.BINID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.LOT_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_PurchaseOrderReceiptDetailVO objObject = new PO_PurchaseOrderReceiptDetailVO();

				while (odrPCS.Read())
				{
					if (odrPCS[PO_PurchaseOrderReceiptDetailTable.BINID_FLD] != DBNull.Value)
						objObject.BinId = int.Parse(odrPCS[PO_PurchaseOrderReceiptDetailTable.BINID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD] != DBNull.Value)
						objObject.LocationId = int.Parse(odrPCS[PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD] != DBNull.Value)
						objObject.PurchaseOrderReceiptDetailID = int.Parse(odrPCS[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD] != DBNull.Value)
						objObject.ReceiveQuantity = Decimal.Parse(odrPCS[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD] != DBNull.Value)
						objObject.PurchaseOrderReceiptID = int.Parse(odrPCS[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD] != DBNull.Value)
						objObject.StockUMID = int.Parse(odrPCS[PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD] != DBNull.Value)
						objObject.ProductID = int.Parse(odrPCS[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD] != DBNull.Value)
						objObject.PurchaseOrderMasterID = int.Parse(odrPCS[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD] != DBNull.Value)
						objObject.BuyingUMID = int.Parse(odrPCS[PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD] != DBNull.Value)
						objObject.PurchaseOrderDetailID = int.Parse(odrPCS[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD] != DBNull.Value)
						objObject.QAStatus = int.Parse(odrPCS[PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD] != DBNull.Value)
						objObject.UmRate = decimal.Parse(odrPCS[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].ToString());
					objObject.Lot = odrPCS[PO_PurchaseOrderReceiptDetailTable.LOT_FLD].ToString();
					objObject.Serial = odrPCS[PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD].ToString();

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

			PO_PurchaseOrderReceiptDetailVO objObject = (PO_PurchaseOrderReceiptDetailVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE PO_PurchaseOrderReceiptDetail SET "
					+ PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderReceiptDetailTable.BINID_FLD + "= ?" + ","
					+ PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + "=   ?" + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD + "=  ?,"
					+ PO_PurchaseOrderReceiptDetailTable.LOT_FLD + "=  ?,"
					+ PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD + "=  ?,"
					+ PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD + "=  ?,"
					+ PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD + "=  ?"
					+ " WHERE " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD + "=   ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD, OleDbType.Integer));
				if (objObject.LocationId > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD].Value = objObject.LocationId;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.BINID_FLD, OleDbType.Integer));
				if (objObject.BinId > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.BINID_FLD].Value = objObject.BinId;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.BINID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].Value = objObject.ReceiveQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD].Value = objObject.PurchaseOrderReceiptID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD, OleDbType.Char));
				if (objObject.ProductID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				if (objObject.PurchaseOrderMasterID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD].Value = objObject.BuyingUMID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				if (objObject.PurchaseOrderDetailID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD].Value = objObject.PurchaseOrderDetailID;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.LOT_FLD, OleDbType.VarWChar));
				if (objObject.Lot != string.Empty)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.LOT_FLD].Value = objObject.Lot;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.LOT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD, OleDbType.TinyInt));
				if (objObject.QAStatus > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD].Value = objObject.QAStatus;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].Value = objObject.UmRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD, OleDbType.VarWChar));
				if (objObject.Serial != string.Empty)
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD].Value = objObject.Serial;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD].Value = objObject.PurchaseOrderReceiptDetailID;

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
					+ PO_PurchaseOrderReceiptDetailTable.BINID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.LOT_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderReceiptDetailTable.TABLE_NAME);

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

		public DataTable ListByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".ListByMaster()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT * FROM PO_PurchaseOrderReceiptDetail WHERE PurchaseOrderReceiptID = " + pintMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable(PO_PurchaseOrderReceiptDetailTable.TABLE_NAME);
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

		public DataTable GetLocationBin(int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "Select PL.LocationID, BinID From pro_productionline PL"
					+ " Inner Join MST_Bin B on PL.LocationID=B.LocationID and B.BinTypeID=4"
					+ " Where ProductionLineID = " + pintProductionLineID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_ProductionLineTable.TABLE_NAME);

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

		public DataSet List(int pintReceiptMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

			    strSql = "SELECT PO_PurchaseOrderMaster.Code AS PO_PurchaseOrderMasterCode, "
			             + " PO_PurchaseOrderDetail.Line AS PO_PurchaseOrderDetailLine, MST_Party.Code AS Maker, "
                         + " ITM_Category.Code ITM_CategoryCode, ITM_Product.Code AS ITM_ProductCode, ITM_Product.Description, ITM_Product.Revision, "
			             + " MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode, PO_PurchaseOrderReceiptDetail.ReceiveQuantity, "
			             + " ISNULL(PO_DeliverySchedule.ReceivedQuantity, 0) AS ReceivedQuantity, PO_DeliverySchedule.DeliveryQuantity, "
                         + " MST_Location.Code AS MST_LocationCode, MST_BIN.Code AS MST_BINCode,"
			             + " PO_PurchaseOrderReceiptDetail.Lot, PO_PurchaseOrderReceiptDetail.QAStatus, PO_PurchaseOrderReceiptDetail.Serial, "
			             + " PO_PurchaseOrderReceiptDetail.StockUMID, PO_PurchaseOrderReceiptDetail.BuyingUMID, "
			             + " PO_PurchaseOrderReceiptDetail.PurchaseOrderMasterID, PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID, "
			             + " PO_PurchaseOrderDetail.PurchaseOrderDetailID, PO_PurchaseOrderDetail.ProductID, "
			             + " PO_PurchaseOrderReceiptDetail.BinID, PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptDetailID, "
			             + " PO_PurchaseOrderReceiptDetail.DeliveryScheduleID,PO_PurchaseOrderReceiptDetail.UMRate,"
			             + " PO_PurchaseOrderReceiptDetail.InvoiceDetailID,PO_PurchaseOrderReceiptDetail.LocationID "
			             + " FROM PO_PurchaseOrderReceiptDetail JOIN PO_DeliverySchedule "
			             + " 	ON PO_PurchaseOrderReceiptDetail.DeliveryScheduleID = PO_DeliverySchedule.DeliveryScheduleID "
			             + " JOIN PO_PurchaseOrderMaster "
			             + " 	ON PO_PurchaseOrderReceiptDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID "
			             + " JOIN PO_PurchaseOrderDetail "
			             + " 	ON PO_PurchaseOrderReceiptDetail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID "
			             + " JOIN ITM_Product "
			             + " 	ON ITM_Product.ProductID = PO_PurchaseOrderReceiptDetail.ProductID "
                         + " LEFT JOIN ITM_Category"
                         + "    ON ITM_Product.CategoryID = ITM_Category.CategoryID"
			             + " JOIN MST_UnitOfMeasure "
			             + " ON MST_UnitOfMeasure.UnitOfMeasureID = PO_PurchaseOrderReceiptDetail.BuyingUMID "
			             + " JOIN MST_Party "
			             + " ON PO_PurchaseOrderMaster.MakerID = MST_Party.PartyID "
                         + " JOIN MST_Location ON PO_PurchaseOrderReceiptDetail.LocationID = MST_Location.LocationID"
                         + " JOIN MST_BIN ON PO_PurchaseOrderReceiptDetail.BinID = MST_BIN.BinID"
			             + " WHERE PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID=" + pintReceiptMasterID;

				var tblData = new DataTable(PO_PurchaseOrderReceiptDetailTable.TABLE_NAME);
				tblData.Columns.Add(new DataColumn("ReceiptLine", typeof(int)));
				tblData.Columns["ReceiptLine"].AutoIncrement = true;
				tblData.Columns["ReceiptLine"].AutoIncrementSeed = 1;
				tblData.Columns["ReceiptLine"].AutoIncrementStep = 1;
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn("Maker", typeof(string)));
                tblData.Columns.Add(new DataColumn("ITM_CategoryCode", typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.DESCRIPTION_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.REVISION_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(IV_LotItemTable.LOT_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(IV_ItemSerialTable.SERIAL_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.QASTATUS_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.STOCKUMID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.BUYINGUMID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PRODUCTID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(MST_BINTable.BINID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(MST_LocationTable.LOCATIONID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.INVOICEDETAILID_FLD, typeof(int)));
				DataColumn[] objColumns = new DataColumn[1];
				objColumns[0] = tblData.Columns[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD];

				tblData.PrimaryKey = objColumns;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(tblData);
				dstPCS.Tables.Add(tblData);

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

		public DataSet ListByPOID(int pintPOMasterID)
		{
			const string METHOD_NAME = THIS + ".ListByPOID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CODE_FLD + " AS " + PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.LINE_FLD + " AS " + PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD + ", "
					+ MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.CODE_FLD + " AS Maker, "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + " AS " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + ", "
					+ MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " AS " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + " AS " + PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + ", "
					//+ MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.CODE_FLD + " AS " + MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD + ", "
					//+ MST_BINTable.TABLE_NAME + "." + MST_BINTable.CODE_FLD + " AS " + MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD + ", "
					+ "'' AS " + IV_LotItemTable.LOT_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.QASTATUS_FLD + ", "
					+ "'' AS " + IV_ItemSerialTable.SERIAL_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.STOCKUMID_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.BUYINGUMID_FLD + ", "
					+ pintPOMasterID + " AS " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + ", "
					+ " 0 AS " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD + ", "
					+ " 0 AS " + PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD
					//+ MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINID_FLD + ", "
					//+ MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.LOCATIONID_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME + " JOIN " + PO_PurchaseOrderMasterTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD
					+ " = " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD
					+ " JOIN " + ITM_ProductTable.TABLE_NAME + " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
					+ " = " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD
					+ " JOIN " + MST_UnitOfMeasureTable.TABLE_NAME + " ON " + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " = " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.BUYINGUMID_FLD
					+ " JOIN " + MST_PartyTable.TABLE_NAME + " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MAKERID_FLD
					+ " = " + MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.PARTYID_FLD
					//+ " JOIN " + MST_BINTable.TABLE_NAME + " ON " 
					//+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.BINID_FLD
					//+ " = " + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINID_FLD
					//+ " JOIN " + MST_LocationTable.TABLE_NAME + " ON "
					//+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.LOCATIONID_FLD
					//+ " = " + MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.LOCATIONID_FLD
					+ " WHERE " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + pintPOMasterID
					+ " AND " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.APPROVERID_FLD + " > 0"
					+ " AND " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.CLOSED_FLD + " <> 1"
					+ " AND ((" + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + " < "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + ")"
					+ " OR (" + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + " IS NULL))";// + DBNull.Value;

				DataTable tblData = new DataTable(PO_PurchaseOrderReceiptDetailTable.TABLE_NAME);
				tblData.Columns.Add(new DataColumn("ReceiptLine", typeof(int)));
				tblData.Columns["ReceiptLine"].AutoIncrement = true;
				tblData.Columns["ReceiptLine"].AutoIncrementSeed = 1;
				tblData.Columns["ReceiptLine"].AutoIncrementStep = 1;
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn("Maker", typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.DESCRIPTION_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.REVISION_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(IV_LotItemTable.LOT_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(IV_ItemSerialTable.SERIAL_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.QASTATUS_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.STOCKUMID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.BUYINGUMID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PRODUCTID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(MST_BINTable.BINID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(MST_LocationTable.LOCATIONID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD, typeof(decimal)));
				tblData.Columns[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].AllowDBNull = false;
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD, typeof(decimal)));
				DataColumn dcolDetailID = new DataColumn(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD, typeof(int));
				dcolDetailID.Unique = true;
				dcolDetailID.AutoIncrement = true;
				dcolDetailID.AutoIncrementSeed = 1;
				dcolDetailID.AutoIncrementStep = 1;
				DataColumn[] objColumns = new DataColumn[1];
				objColumns[0] = dcolDetailID;

				tblData.Columns.Add(dcolDetailID);
				tblData.PrimaryKey = objColumns;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable tblTemp = new DataTable();
				odadPCS.Fill(tblTemp);

				foreach (DataRow drow in tblTemp.Rows)
				{
					DataRow drowTemp = tblData.NewRow();
					foreach (DataColumn dcolData in tblTemp.Columns)
					{
						if ((dcolData.ColumnName != dcolDetailID.ColumnName) && (dcolData.ColumnName != "ReceiptLine"))
						{
							drowTemp[dcolData.ColumnName] = drow[dcolData.ColumnName];
						}
					}
					tblData.Rows.Add(drowTemp);
				}

				dstPCS.Tables.Add(tblData);
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
		
		public DataSet ListByItem(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".ListByItem()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CODE_FLD + " AS " + PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.LINE_FLD + " AS " + PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD + ", "
					+ MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.CODE_FLD + " AS Maker, "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + " AS " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + ", "
					+ MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " AS " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + " AS " + PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + ", "
					//+ MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.CODE_FLD + " AS " + MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD + ", "
					//+ MST_BINTable.TABLE_NAME + "." + MST_BINTable.CODE_FLD + " AS " + MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.QASTATUS_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.STOCKUMID_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.BUYINGUMID_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + ", "
					//+ MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINID_FLD + ", "
					+ " 0 AS " + PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD
					//+ MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.LOCATIONID_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME + " JOIN " + PO_PurchaseOrderMasterTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD
					+ " = " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD
					+ " JOIN " + ITM_ProductTable.TABLE_NAME + " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
					+ " = " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD
					+ " JOIN " + MST_UnitOfMeasureTable.TABLE_NAME + " ON " + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " = " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.BUYINGUMID_FLD
					+ " JOIN " + MST_PartyTable.TABLE_NAME + " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MAKERID_FLD
					+ " = " + MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.PARTYID_FLD
					//+ " JOIN " + MST_BINTable.TABLE_NAME + " ON " 
					//+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.BINID_FLD
					//+ " = " + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINID_FLD
					//+ " JOIN " + MST_LocationTable.TABLE_NAME + " ON "
					//+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.LOCATIONID_FLD
					//+ " = " + MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.LOCATIONID_FLD
					+ " WHERE " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.APPROVERID_FLD + " > 0";// + DBNull.Value;

				DataTable tblData = new DataTable(PO_PurchaseOrderReceiptDetailTable.TABLE_NAME);
				tblData.Columns.Add(new DataColumn("ReceiptLine", typeof(int)));
				tblData.Columns["ReceiptLine"].AutoIncrement = true;
				tblData.Columns["ReceiptLine"].AutoIncrementSeed = 1;
				tblData.Columns["ReceiptLine"].AutoIncrementStep = 1;
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn("Maker", typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.DESCRIPTION_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.REVISION_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(IV_LotItemTable.LOT_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(IV_ItemSerialTable.SERIAL_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.QASTATUS_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.STOCKUMID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.BUYINGUMID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PRODUCTID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(MST_BINTable.BINID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(MST_LocationTable.LOCATIONID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD, typeof(int)));
				tblData.Columns[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD].AutoIncrement = true;
				tblData.Columns[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD].AutoIncrementSeed = 1;
				tblData.Columns[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD].AutoIncrementStep = 1;
				DataColumn[] objColumns = new DataColumn[1];
				objColumns[0] = tblData.Columns[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD];

				tblData.PrimaryKey = objColumns;

//				Utils utils = new Utils();
//				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
//				ocmdPCS = new OleDbCommand(strSql, oconPCS);
//				ocmdPCS.Connection.Open();

				//OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				//odadPCS.Fill(dstPCS, PO_PurchaseOrderReceiptDetailTable.TABLE_NAME);
				//odadPCS.Fill(tblData);
				dstPCS.Tables.Add(tblData);

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
					+ PO_PurchaseOrderReceiptDetailTable.BINID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.LOT_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD + ","
					+ "DeliveryScheduleID, InvoiceDetailID,"
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, PO_PurchaseOrderReceiptDetailTable.TABLE_NAME);

			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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
		
		public decimal GetTotalReceiveQuantity(int pintPODetailID)
		{
			const string METHOD_NAME = THIS + ".GetTotalReceiveQuantity()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT ISNULL(SUM("
					+ PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + "), 0)"
					+ " FROM " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD + "=" + pintPODetailID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return decimal.Parse(objResult.ToString());
				else
					return 0;
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
		/// GetTotalReceiptQuantityByDeliveryScheduleID
		/// </summary>
		/// <param name="pintDeliveryScheduleID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Saturday, June 10 2006</date>
		public decimal GetTotalReceiptQuantityByDeliveryScheduleID(int pintDeliveryScheduleID)
		{
			const string METHOD_NAME = THIS + ".GetTotalReceiptQuantityByDeliveryScheduleID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT ISNULL(SUM("
					+ PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + "), 0)"
					+ " FROM " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD + "=" + pintDeliveryScheduleID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return decimal.Parse(objResult.ToString());
				else
					return 0;
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
		
		public bool IsReceived(int pintPODetailID)
		{
			const string METHOD_NAME = THIS + ".IsReceived()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT ISNULL(COUNT(*), 0)"
					+ " FROM " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD + "=" + pintPODetailID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
				{
					if (int.Parse(objResult.ToString()) > 0)
						return true;
					else
						return false;
				}
				else
					return false;
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
		/// Check if Invoice detail has been receipt
		/// </summary>
		/// <param name="pintInvoiceDetailID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, September 28 2006</date>
		public bool IsInvoiceHasBeenReceipt(int pintInvoiceDetailID)
		{
			const string METHOD_NAME = THIS + ".IsInvoiceHasBeenReceipt()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT ISNULL(COUNT(*), 0)"
					+ " FROM " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderReceiptDetailTable.INVOICEDETAILID_FLD + " ="  + pintInvoiceDetailID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
				{
					if (int.Parse(objResult.ToString()) > 0)
						return true;
					else
						return false;
				}
				else
					return false;
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
		/// This method uses to get all data from PO_PurchaseOrderDetail by OrderMaster
		/// List only the PO_Lines which have delivery schedule and already approved and not Closed
		/// Receive quantity is the Delivery Quantity - Received Quantity in Schedule
		/// </summary>
		/// <param name="pstrPOCode">PO Master Code</param>
		/// <param name="pdtmSlipDate">The schedule date</param>
		/// <returns>DataSet</returns>
		public DataSet ListByPOCode(string pstrPOCode, DateTime pdtmSlipDate)
		{
			const string METHOD_NAME = THIS + ".ListByPOCode()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT PO_PurchaseOrderMaster.Code AS PO_PurchaseOrderMasterCode,"
					+ " PO_PurchaseOrderDetail.Line AS PO_PurchaseOrderDetailLine,"
					+ " MST_Party.Code AS Maker, "
					+ " ITM_Product.Code AS ITM_ProductCode, ITM_Product.Description,"
					+ " ITM_Product.Revision, MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,"
					+ " PO_DeliverySchedule.DeliveryQuantity AS ReceiveQuantity, '' AS Lot,"
					+ " ITM_Product.QAStatus, '' AS Serial, PO_PurchaseOrderDetail.StockUMID,"
					+ " PO_PurchaseOrderDetail.BuyingUMID, PO_PurchaseOrderDetail.PurchaseOrderMasterID,"
					+ " 0 AS PurchaseOrderReceiptID,  0 AS UMRate, PO_DeliverySchedule.DeliveryScheduleID,"
					+ " PO_PurchaseOrderDetail.PurchaseOrderDetailID, PO_PurchaseOrderDetail.ProductID,"
					+ " PO_PurchaseOrderDetail.OrderQuantity, PO_PurchaseOrderDetail.TotalDelivery,"
					+ " PO_DeliverySchedule.DeliveryQuantity, ISNULL(PO_DeliverySchedule.ReceivedQuantity, 0) AS ReceivedQuantity"
					+ " FROM PO_PurchaseOrderDetail JOIN PO_PurchaseOrderMaster"
					+ "		ON PO_PurchaseOrderDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
					+ " JOIN ITM_Product"
					+ "		ON ITM_Product.ProductID = PO_PurchaseOrderDetail.ProductID"
					+ " JOIN MST_UnitOfMeasure"
					+ "		ON MST_UnitOfMeasure.UnitOfMeasureID = PO_PurchaseOrderDetail.BuyingUMID"
					+ " JOIN PO_DeliverySchedule"
					+ "		ON PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
					+ " JOIN MST_Party"
					+ "		ON PO_PurchaseOrderMaster.MakerID = MST_Party.PartyID"
					+ " WHERE PO_PurchaseOrderMaster.Code= ?"
					+ " AND ISNULL(PO_PurchaseOrderDetail.ApproverID, 0) > 0";
					//+ " AND ISNULL(PO_PurchaseOrderDetail.Closed, 0) = 0"
					//+ " AND ((ISNULL(PO_DeliverySchedule.DeliveryQuantity, 0) - ISNULL(PO_DeliverySchedule.ReceivedQuantity, 0)) > 0)";
				if (pdtmSlipDate != DateTime.MaxValue)
					strSql += " AND DATEPART(year, ScheduleDate) = " + pdtmSlipDate.Year
						+ " AND DATEPART(month, ScheduleDate) = " + pdtmSlipDate.Month
						+ " AND DATEPART(day, ScheduleDate) = " + pdtmSlipDate.Day
						+ " AND DATEPART(hour, ScheduleDate) = " + pdtmSlipDate.Hour;
                strSql += " ORDER BY ScheduleDate";

				DataTable tblData = new DataTable(PO_PurchaseOrderReceiptDetailTable.TABLE_NAME);
				tblData.Columns.Add(new DataColumn("ReceiptLine", typeof(int)));
				tblData.Columns["ReceiptLine"].AutoIncrement = true;
				tblData.Columns["ReceiptLine"].AutoIncrementSeed = 1;
				tblData.Columns["ReceiptLine"].AutoIncrementStep = 1;
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn("Maker", typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.DESCRIPTION_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.REVISION_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(IV_LotItemTable.LOT_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(IV_ItemSerialTable.SERIAL_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.QASTATUS_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.STOCKUMID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.BUYINGUMID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PRODUCTID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(MST_BINTable.BINID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(MST_LocationTable.LOCATIONID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.INVOICEDETAILID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD, typeof(decimal)));
				tblData.Columns[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].AllowDBNull = false;
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD, typeof(decimal)));
				DataColumn dcolDetailID = new DataColumn(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD, typeof(int));
				dcolDetailID.Unique = true;
				dcolDetailID.AutoIncrement = true;
				dcolDetailID.AutoIncrementSeed = 1;
				dcolDetailID.AutoIncrementStep = 1;
				DataColumn[] objColumns = new DataColumn[1];
				objColumns[0] = dcolDetailID;

				tblData.Columns.Add(dcolDetailID);
				tblData.PrimaryKey = objColumns;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CODE_FLD].Value = pstrPOCode;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable tblTemp = new DataTable();
				odadPCS.Fill(tblTemp);

				foreach (DataRow drow in tblTemp.Rows)
				{
					DataRow drowTemp = tblData.NewRow();
					foreach (DataColumn dcolData in tblTemp.Columns)
					{
						if ((dcolData.ColumnName != dcolDetailID.ColumnName) && (dcolData.ColumnName != "ReceiptLine"))
						{
							drowTemp[dcolData.ColumnName] = drow[dcolData.ColumnName];
						}
					}
					tblData.Rows.Add(drowTemp);
				}

				dstPCS.Tables.Add(tblData);

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

        public DataSet ListByPurchaseOrderCode(string pstrPOCode, DateTime pdtmSlipDate)
        {
            const string METHOD_NAME = THIS + ".ListByPurchaseOrderCode()";
            DataSet dstPCS = new DataSet();


            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT PO_PurchaseOrderMaster.Code AS PO_PurchaseOrderMasterCode,"
                    + " PO_PurchaseOrderDetail.Line AS PO_PurchaseOrderDetailLine,"
                    + " MST_Party.Code AS Maker, "
                    + " ITM_Category.Code ITM_CategoryCode, ITM_Product.Code AS ITM_ProductCode, ITM_Product.Description,"
                    + " ITM_Product.Revision, MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,"
                    + " PO_DeliverySchedule.DeliveryQuantity - ISNULL(PO_DeliverySchedule.ReceivedQuantity, 0) AS ReceiveQuantity,"
                    + " MST_Location.Code AS MST_LocationCode, MST_BIN.Code AS MST_BINCode,"
                    + " '' AS Lot, ITM_Product.LocationID, ITM_Product.BinID,"
                    + " ITM_Product.QAStatus, '' AS Serial, PO_PurchaseOrderDetail.StockUMID,"
                    + " PO_PurchaseOrderDetail.BuyingUMID, PO_PurchaseOrderDetail.PurchaseOrderMasterID,"
                    + " 0 AS PurchaseOrderReceiptID,  0 AS UMRate, PO_DeliverySchedule.DeliveryScheduleID,"
                    + " PO_PurchaseOrderDetail.PurchaseOrderDetailID, PO_PurchaseOrderDetail.ProductID,"
                    + " PO_PurchaseOrderDetail.OrderQuantity, PO_PurchaseOrderDetail.TotalDelivery,"
                    + " PO_DeliverySchedule.DeliveryQuantity, ISNULL(PO_DeliverySchedule.ReceivedQuantity, 0) AS ReceivedQuantity"
                    + " FROM PO_PurchaseOrderDetail JOIN PO_PurchaseOrderMaster"
                    + "		ON PO_PurchaseOrderDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
                    + " JOIN ITM_Product"
                    + "		ON ITM_Product.ProductID = PO_PurchaseOrderDetail.ProductID"
                    + " LEFT JOIN ITM_Category"
                    + "    ON ITM_Product.CategoryID = ITM_Category.CategoryID"
                    + " JOIN MST_UnitOfMeasure"
                    + "		ON MST_UnitOfMeasure.UnitOfMeasureID = PO_PurchaseOrderDetail.BuyingUMID"
                    + " JOIN PO_DeliverySchedule"
                    + "		ON PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
                    + " JOIN MST_Party"
                    + "		ON PO_PurchaseOrderMaster.MakerID = MST_Party.PartyID"
                    + " LEFT JOIN MST_Location ON ITM_Product.LocationID = MST_Location.LocationID"
                    + " LEFT JOIN MST_BIN ON ITM_Product.BinID = MST_BIN.BinID"
                    + " WHERE PO_PurchaseOrderMaster.Code= ?"
                    + " AND ISNULL(PO_PurchaseOrderDetail.ApproverID, 0) > 0";
                if (pdtmSlipDate != DateTime.MaxValue)
                    strSql += " AND DATEPART(year, ScheduleDate) = " + pdtmSlipDate.Year
                        + " AND DATEPART(month, ScheduleDate) = " + pdtmSlipDate.Month
                        + " AND DATEPART(day, ScheduleDate) = " + pdtmSlipDate.Day
                        + " AND DATEPART(hour, ScheduleDate) = " + pdtmSlipDate.Hour;
                strSql += " ORDER BY ScheduleDate";

                DataTable tblData = new DataTable(PO_PurchaseOrderReceiptDetailTable.TABLE_NAME);
                tblData.Columns.Add(new DataColumn("ReceiptLine", typeof(int)));
                tblData.Columns["ReceiptLine"].AutoIncrement = true;
                tblData.Columns["ReceiptLine"].AutoIncrementSeed = 1;
                tblData.Columns["ReceiptLine"].AutoIncrementStep = 1;
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD, typeof(string)));
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD, typeof(string)));
                tblData.Columns.Add(new DataColumn("Maker", typeof(string)));
                tblData.Columns.Add(new DataColumn("ITM_CategoryCode", typeof(string)));
                tblData.Columns.Add(new DataColumn(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD, typeof(string)));
                tblData.Columns.Add(new DataColumn(ITM_ProductTable.DESCRIPTION_FLD, typeof(string)));
                tblData.Columns.Add(new DataColumn(ITM_ProductTable.REVISION_FLD, typeof(string)));
                tblData.Columns.Add(new DataColumn(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD, typeof(string)));
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD, typeof(decimal)));
                tblData.Columns.Add(new DataColumn(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, typeof(decimal)));
                tblData.Columns.Add(new DataColumn(PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, typeof(decimal)));
                tblData.Columns.Add(new DataColumn(MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD, typeof(string)));
                tblData.Columns.Add(new DataColumn(MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD, typeof(string)));
                tblData.Columns.Add(new DataColumn(IV_LotItemTable.LOT_FLD, typeof(string)));
                tblData.Columns.Add(new DataColumn(IV_ItemSerialTable.SERIAL_FLD, typeof(string)));
                tblData.Columns.Add(new DataColumn(ITM_ProductTable.QASTATUS_FLD, typeof(int)));
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.STOCKUMID_FLD, typeof(int)));
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.BUYINGUMID_FLD, typeof(int)));
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD, typeof(int)));
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD, typeof(int)));
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD, typeof(int)));
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PRODUCTID_FLD, typeof(int)));
                tblData.Columns.Add(new DataColumn(MST_BINTable.BINID_FLD, typeof(int)));
                tblData.Columns.Add(new DataColumn(MST_LocationTable.LOCATIONID_FLD, typeof(int)));
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD, typeof(int)));
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.INVOICEDETAILID_FLD, typeof(int)));
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD, typeof(decimal)));
                tblData.Columns[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].AllowDBNull = false;
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD, typeof(decimal)));
                tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD, typeof(decimal)));
                DataColumn dcolDetailID = new DataColumn(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD, typeof(int));
                dcolDetailID.Unique = true;
                dcolDetailID.AutoIncrement = true;
                dcolDetailID.AutoIncrementSeed = 1;
                dcolDetailID.AutoIncrementStep = 1;
                DataColumn[] objColumns = new DataColumn[1];
                objColumns[0] = dcolDetailID;

                tblData.Columns.Add(dcolDetailID);
                tblData.PrimaryKey = objColumns;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CODE_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CODE_FLD].Value = pstrPOCode;

                ocmdPCS.CommandText = strSql;
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                DataTable tblTemp = new DataTable();
                odadPCS.Fill(tblTemp);

                foreach (DataRow drow in tblTemp.Rows)
                {
                    DataRow drowTemp = tblData.NewRow();
                    foreach (DataColumn dcolData in tblTemp.Columns)
                    {
                        if ((dcolData.ColumnName != dcolDetailID.ColumnName) && (dcolData.ColumnName != "ReceiptLine"))
                        {
                            drowTemp[dcolData.ColumnName] = drow[dcolData.ColumnName];
                        }
                    }
                    tblData.Rows.Add(drowTemp);
                }

                dstPCS.Tables.Add(tblData);

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
		/// List receipt detail by Invoice
		/// </summary>
		/// <param name="pInvoiceMasterID">Invoice Master ID</param>
		/// <returns>Receipt Detail</returns>
		public DataSet ListByInvoice(int pInvoiceMasterID)
		{
			const string METHOD_NAME = THIS + ".ListByInvoice()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT PO_PurchaseOrderMaster.Code AS PO_PurchaseOrderMasterCode,"
					+ " PO_PurchaseOrderDetail.Line AS PO_PurchaseOrderDetailLine,"
					+ " MST_Party.Code AS Maker, ITM_Category.Code ITM_CategoryCode,"
					+ " ITM_Product.Code AS ITM_ProductCode, ITM_Product.Description,"
					+ " ITM_Product.Revision, MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,"
                    + " ISNULL(PO_InvoiceDetail.InvoiceQuantity, 0) - ISNULL(PO_DeliverySchedule.ReceivedQuantity, 0) AS ReceiveQuantity,"
                    + " MST_Location.Code AS MST_LocationCode, MST_BIN.Code AS MST_BINCode,"
                    + " '' AS Lot, ITM_Product.LocationID, ITM_Product.BinID,"
					+ " ITM_Product.QAStatus, '' AS Serial, PO_PurchaseOrderDetail.StockUMID,"
					+ " PO_PurchaseOrderDetail.BuyingUMID, PO_InvoiceDetail.PurchaseOrderMasterID,"
					+ " 0 AS PurchaseOrderReceiptID,  0 AS UMRate, PO_InvoiceDetail.DeliveryScheduleID,"
					+ " PO_InvoiceDetail.PurchaseOrderDetailID, PO_PurchaseOrderDetail.ProductID, PO_InvoiceDetail.InvoiceDetailID,"
					+ " PO_PurchaseOrderDetail.OrderQuantity, PO_PurchaseOrderDetail.TotalDelivery,"
					+ " PO_DeliverySchedule.DeliveryQuantity, ISNULL(PO_DeliverySchedule.ReceivedQuantity, 0) AS ReceivedQuantity"
					+ " FROM PO_InvoiceDetail JOIN PO_DeliverySchedule"
					+ "		ON PO_InvoiceDetail.DeliveryScheduleID = PO_DeliverySchedule.DeliveryScheduleID"
					+ " JOIN PO_PurchaseOrderDetail"
					+ "		ON PO_InvoiceDetail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
					+ " JOIN PO_PurchaseOrderMaster"
					+ "		ON PO_InvoiceDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
					+ " JOIN ITM_Product"
					+ "		ON ITM_Product.ProductID = PO_PurchaseOrderDetail.ProductID"
                    + " LEFT JOIN ITM_Category"
                    + "    ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " JOIN MST_UnitOfMeasure"
					+ "		ON MST_UnitOfMeasure.UnitOfMeasureID = PO_InvoiceDetail.InvoiceUMID"
					+ " JOIN MST_Party"
					+ "		ON PO_PurchaseOrderMaster.MakerID = MST_Party.PartyID"
                    + " LEFT JOIN MST_Location ON ITM_Product.LocationID = MST_Location.LocationID"
                    + " LEFT JOIN MST_BIN ON ITM_Product.BinID = MST_BIN.BinID"
					+ " WHERE PO_InvoiceDetail.InvoiceMasterID = " + pInvoiceMasterID
					+ " AND ISNULL(PO_PurchaseOrderDetail.ApproverID, 0) > 0"
                    + " AND ISNULL(PO_DeliverySchedule.ReceivedQuantity,0) < PO_DeliverySchedule.DeliveryQuantity";

				DataTable tblData = new DataTable(PO_PurchaseOrderReceiptDetailTable.TABLE_NAME);
				tblData.Columns.Add(new DataColumn("ReceiptLine", typeof(int)));
				tblData.Columns["ReceiptLine"].AutoIncrement = true;
				tblData.Columns["ReceiptLine"].AutoIncrementSeed = 1;
				tblData.Columns["ReceiptLine"].AutoIncrementStep = 1;
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn("Maker", typeof(string)));
                tblData.Columns.Add(new DataColumn("ITM_CategoryCode", typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.DESCRIPTION_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.REVISION_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(IV_LotItemTable.LOT_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(IV_ItemSerialTable.SERIAL_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(ITM_ProductTable.QASTATUS_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.STOCKUMID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.BUYINGUMID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.PRODUCTID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(MST_BINTable.BINID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(MST_LocationTable.LOCATIONID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.INVOICEDETAILID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD, typeof(decimal)));
				tblData.Columns[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].AllowDBNull = false;
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD, typeof(decimal)));
				DataColumn dcolDetailID = new DataColumn(PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD, typeof(int));
				dcolDetailID.Unique = true;
				dcolDetailID.AutoIncrement = true;
				dcolDetailID.AutoIncrementSeed = 1;
				dcolDetailID.AutoIncrementStep = 1;
				DataColumn[] objColumns = new DataColumn[1];
				objColumns[0] = dcolDetailID;

				tblData.Columns.Add(dcolDetailID);
				tblData.PrimaryKey = objColumns;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable tblTemp = new DataTable();
				odadPCS.Fill(tblTemp);

				foreach (DataRow drow in tblTemp.Rows)
				{
					DataRow drowTemp = tblData.NewRow();
					foreach (DataColumn dcolData in tblTemp.Columns)
					{
						if ((dcolData.ColumnName != dcolDetailID.ColumnName) && (dcolData.ColumnName != "ReceiptLine"))
						{
							drowTemp[dcolData.ColumnName] = drow[dcolData.ColumnName];
						}
					}
					tblData.Rows.Add(drowTemp);
				}

				dstPCS.Tables.Add(tblData);

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
		/// 
		/// </summary>
		/// <param name="pintPurchaseOrderMasterID"></param>
		public void CheckToClosePO(int pintPurchaseOrderMasterID, string pstrPODetailIDs)
		{
			const string METHOD_NAME = THIS + ".CheckToClosePO()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				if(pstrPODetailIDs != string.Empty) pstrPODetailIDs = " AND XX.PurchaseOrderDetailID IN " + pstrPODetailIDs;
				string strSql = strSql = " UPDATE PO_PurchaseOrderDetail SET Closed=1"
						+ " WHERE PurchaseOrderMasterID="+pintPurchaseOrderMasterID+" AND PurchaseOrderDetailID IN ("
						+ " SELECT PurchaseOrderDetailID FROM"
						+ " 	(SELECT PORD.PurchaseOrderMasterID, PORD.PurchaseOrderDetailID, POD.OrderQuantity, SUM(ReceiveQuantity) ReceiveQuantity"
						+ " 	FROM PO_PurchaseOrderReceiptDetail PORD"
						+ " 	INNER JOIN PO_PurchaseOrderDetail POD ON PORD.PurchaseOrderDetailID=POD.PurchaseOrderDetailID "
						+ " 	AND POD.PurchaseOrderMasterID="+pintPurchaseOrderMasterID
						+ " 	GROUP BY PORD.PurchaseOrderMasterID, PORD.PurchaseOrderDetailID, POD.OrderQuantity) XX "
						+ " 	WHERE XX.ReceiveQuantity >= XX.OrderQuantity " + pstrPODetailIDs
						+ ")"; 
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

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

		public void DeleteRowDetail(int printPoReceiptMasterId, int printPoReceiptDetailId)
		{
			const string METHOD_NAME = THIS + ".DeleteRowDetail()";
			string strSql = String.Empty;
			strSql = "DELETE " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME 
					+ " WHERE  " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD + "=" + printPoReceiptMasterId.ToString()
					+ " AND " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD + "=" + printPoReceiptDetailId.ToString();
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

		public DataRow GetRow(int printReceiptMasterId, int printPoReceiptDetailId)
		{
			const string METHOD_NAME = THIS + ".()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT * FROM " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME 
							+ " WHERE " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD + " = " + printReceiptMasterId.ToString()
							+ " AND " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD + " = " + printPoReceiptDetailId.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

				return dstPCS.Tables[0].Rows[0];
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

		public void UpdateTranType(int printPoReceiptMasterId, int printPoReceiptDetailId, int oldTranTypeId, int tranTypeId, int inspStatus)
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

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.TRANTYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.TRANTYPEID_FLD].Value = tranTypeId;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.INSPSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.INSPSTATUS_FLD].Value = inspStatus;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.REFMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.REFMASTERID_FLD].Value = printPoReceiptMasterId;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_TransactionHistoryTable.REFDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_TransactionHistoryTable.REFDETAILID_FLD].Value = printPoReceiptDetailId;

				ocmdPCS.Parameters.Add(new OleDbParameter("OldTranTypeID", OleDbType.Integer)).Value = oldTranTypeId;
				
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
					throw ex;
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