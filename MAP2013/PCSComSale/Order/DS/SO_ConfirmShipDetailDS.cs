using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComSale.Order.DS
{
	public class SO_ConfirmShipDetailDS 
	{
		private const string THIS = "PCSComSale.Order.DS.SO_ConfirmShipDetailDS";
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				SO_ConfirmShipDetailVO objObject = (SO_ConfirmShipDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO SO_ConfirmShipDetail("
					+ SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD + ","
					+ SO_ConfirmShipDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_ConfirmShipDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_ConfirmShipDetailTable.PRODUCTID_FLD + ")"
					+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD].Value = objObject.ConfirmShipMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipDetailTable.SALEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipDetailTable.SALEORDERDETAILID_FLD].Value = objObject.SaleOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipDetailTable.DELIVERYSCHEDULEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipDetailTable.DELIVERYSCHEDULEID_FLD].Value = objObject.DeliveryScheduleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;


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
			strSql = "DELETE " + SO_ConfirmShipDetailTable.TABLE_NAME + " WHERE  " + "ConfirmShipDetailID" + "=" + pintID.ToString();
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
					+ SO_ConfirmShipDetailTable.CONFIRMSHIPDETAILID_FLD + ","
					+ SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD + ","
					+ SO_ConfirmShipDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_ConfirmShipDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_ConfirmShipDetailTable.PRODUCTID_FLD
					+ " FROM " + SO_ConfirmShipDetailTable.TABLE_NAME
					+ " WHERE " + SO_ConfirmShipDetailTable.CONFIRMSHIPDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_ConfirmShipDetailVO objObject = new SO_ConfirmShipDetailVO();

				while (odrPCS.Read())
				{
					objObject.ConfirmShipDetailID = int.Parse(odrPCS[SO_ConfirmShipDetailTable.CONFIRMSHIPDETAILID_FLD].ToString().Trim());
					objObject.ConfirmShipMasterID = int.Parse(odrPCS[SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD].ToString().Trim());
					objObject.SaleOrderDetailID = int.Parse(odrPCS[SO_ConfirmShipDetailTable.SALEORDERDETAILID_FLD].ToString().Trim());
					objObject.DeliveryScheduleID = int.Parse(odrPCS[SO_ConfirmShipDetailTable.DELIVERYSCHEDULEID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[SO_ConfirmShipDetailTable.PRODUCTID_FLD].ToString().Trim());

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

			SO_ConfirmShipDetailVO objObject = (SO_ConfirmShipDetailVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE SO_ConfirmShipDetail SET "
					+ SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD + "=   ?" + ","
					+ SO_ConfirmShipDetailTable.SALEORDERDETAILID_FLD + "=   ?" + ","
					+ SO_ConfirmShipDetailTable.DELIVERYSCHEDULEID_FLD + "=   ?" + ","
					+ SO_ConfirmShipDetailTable.PRODUCTID_FLD + "=  ?"
					+ " WHERE " + SO_ConfirmShipDetailTable.CONFIRMSHIPDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD].Value = objObject.ConfirmShipMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipDetailTable.SALEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipDetailTable.SALEORDERDETAILID_FLD].Value = objObject.SaleOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipDetailTable.DELIVERYSCHEDULEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipDetailTable.DELIVERYSCHEDULEID_FLD].Value = objObject.DeliveryScheduleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipDetailTable.CONFIRMSHIPDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipDetailTable.CONFIRMSHIPDETAILID_FLD].Value = objObject.ConfirmShipDetailID;


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
					+ SO_ConfirmShipDetailTable.CONFIRMSHIPDETAILID_FLD + ","
					+ SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD + ","
					+ SO_ConfirmShipDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_ConfirmShipDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_ConfirmShipDetailTable.PRODUCTID_FLD
					+ " FROM " + SO_ConfirmShipDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_ConfirmShipDetailTable.TABLE_NAME);

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
					+ SO_ConfirmShipDetailTable.CONFIRMSHIPDETAILID_FLD + ","
					+ SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD + ","
					+ SO_ConfirmShipDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_ConfirmShipDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_ConfirmShipDetailTable.PRICE_FLD + ","
					+ SO_ConfirmShipDetailTable.INVOICEQTY_FLD + ","
					+ SO_ConfirmShipDetailTable.NETAMOUNT_FLD + ","
					+ SO_ConfirmShipDetailTable.VATAMOUNT_FLD + ","
					+ SO_ConfirmShipDetailTable.VATPERCENT_FLD + ","
					+ SO_ConfirmShipDetailTable.PRODUCTID_FLD
					+ "  FROM " + SO_ConfirmShipDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand cmdSelect = new OleDbCommand(strSql, oconPCS);
				cmdSelect.CommandTimeout = 10000;
				odadPCS.SelectCommand = cmdSelect;
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, SO_ConfirmShipDetailTable.TABLE_NAME);

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
		public DataSet ListByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ SO_ConfirmShipDetailTable.CONFIRMSHIPDETAILID_FLD + ","
					+ SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD + ","
					+ SO_ConfirmShipDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_ConfirmShipDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_ConfirmShipDetailTable.PRICE_FLD + ","
					+ SO_ConfirmShipDetailTable.INVOICEQTY_FLD + ","
					+ SO_ConfirmShipDetailTable.NETAMOUNT_FLD + ","
					+ SO_ConfirmShipDetailTable.VATPERCENT_FLD + ","
					+ SO_ConfirmShipDetailTable.VATAMOUNT_FLD + ","
					+ SO_ConfirmShipDetailTable.PRODUCTID_FLD
					+ " FROM " + SO_ConfirmShipDetailTable.TABLE_NAME
					+ " WHERE " + SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD + "=?";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.AddWithValue(SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD, pintMasterID);

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_ConfirmShipDetailTable.TABLE_NAME);

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
		public DataSet ListForReview(int pintMasterID)
		{
            const string METHOD_NAME = THIS + ".ListForReview()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
                var strSql = " SELECT DISTINCT Bin.Code AS BCode, L.Code As LCode," +
                    "  CA.Code ITM_CategoryCode, G.Code PartNo, G.Description, G.Revision,H.Code UMCode, " +
                    " C.SaleOrderDetailID, E.DeliveryScheduleID, G.ProductID, ISNULL(G.AllowNegativeQty,0) AllowNegativeQty," +
                    " E.ScheduleDate, GA.Code SO_GateCode, E.DeliveryQuantity AS CommittedQuantity," +
                    " A.InvoiceQty, A.InvoiceQty OldInvoiceQty, ISNULL(BC.OHQuantity,0) - ISNULL(BC.CommitQuantity,0) AvailableQty, " +
                    " A.Price, A.NetAmount, A.NetAmount * ISNULL(B.ExchangeRate,1) AS NetAmountRate, A.VATPercent, A.VATAmount, A.ConfirmShipMasterID ," +
                    " A.ConfirmShipDetailID, B.LocationID,B.BinID," +
                    " C.SellingUMID, C.SaleOrderMasterID,D.Code, C.SaleOrderLine, E.Line" +
                    " FROM 	SO_ConfirmShipDetail A INNER JOIN SO_ConfirmShipMaster B ON A.ConfirmShipMasterID = B.ConfirmShipMasterID" +
                    " INNER JOIN SO_SaleOrderDetail C ON C.SaleOrderDetailID = A.SaleOrderDetailID" +
                    " INNER JOIN SO_SaleOrderMaster D ON C.SaleOrderMasterID = D.SaleOrderMasterID" +
                    " INNER JOIN SO_DeliverySchedule E ON E.DeliveryScheduleID = A.DeliveryScheduleID" +
                    " LEFT JOIN SO_Gate GA ON E.GateID = GA.GateID" +
                    " INNER JOIN ITM_Product G ON C.ProductID = G.ProductID" +
                    " LEFT JOIN ITM_Category CA ON CA.CategoryID = G.CategoryID" +
                    " LEFT JOIN MST_Location L ON L.LocationID= B.LocationID " +
                    " LEFT JOIN MST_BIN Bin ON Bin.BinID = B.BinID " +
                    " LEFT JOIN IV_BinCache BC ON B.BinID = BC.BinID" +
                    " AND BC.ProductID = G.ProductID" +
                    " INNER JOIN MST_UnitOfMeasure H ON C.SellingUMID = H.UnitOfMeasureID " +
                    " WHERE	B.ConfirmShipMasterID = ?";

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.AddWithValue(SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD, pintMasterID);

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_ConfirmShipDetailTable.TABLE_NAME);

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
	}
}