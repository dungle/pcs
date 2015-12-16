using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComSale.Order.DS
{
	/// <summary>
	/// Summary description for SO_InvoiceDetailDS.
	/// </summary>
	public class SO_InvoiceDetailDS 
	{
		private const string THIS = "PCSComSale.Order.DS.SO_ConfirmShipDetailDS";
		public Object GetObjectVO(int pintID)
		{
			throw new NotImplementedException();
		}

		public DataSet List()
		{
			throw new NotImplementedException();
		}

		public void Delete(int pintID)
		{
			throw new NotImplementedException();
		}

		public void Update(Object pobjObjecVO)
		{
			throw new NotImplementedException();
		}

		public void Add(Object pobjObjectVO)
		{
			throw new NotImplementedException();
		}

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
					+ SO_InvoiceDetailTable.INVOICEDETAILID_FLD + ","
					+ SO_InvoiceDetailTable.INVOICEMASTERID_FLD + ","
					+ SO_InvoiceDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_InvoiceDetailTable.PRICE_FLD + ","
					+ SO_InvoiceDetailTable.INVOICEQTY_FLD + ","
					+ SO_InvoiceDetailTable.NETAMOUNT_FLD + ","
					+ SO_InvoiceDetailTable.VATAMOUNT_FLD + ","
					+ SO_InvoiceDetailTable.VATPERCENT_FLD + ","
					+ SO_InvoiceDetailTable.PRODUCTID_FLD 
					+ "  FROM " + SO_InvoiceDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,SO_InvoiceDetailTable.TABLE_NAME);

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
		public DataSet ListByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".ListByMaster()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql=	"SELECT "
					+ SO_InvoiceDetailTable.INVOICEDETAILID_FLD + ","
					+ SO_InvoiceDetailTable.INVOICEMASTERID_FLD + ","
					+ SO_InvoiceDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_InvoiceDetailTable.PRICE_FLD + ","
					+ SO_InvoiceDetailTable.INVOICEQTY_FLD + ","
					+ SO_InvoiceDetailTable.NETAMOUNT_FLD + ","
					+ SO_InvoiceDetailTable.VATPERCENT_FLD + ","
					+ SO_InvoiceDetailTable.VATAMOUNT_FLD + ","
					+ SO_InvoiceDetailTable.PRODUCTID_FLD
					+ " FROM " + SO_InvoiceDetailTable.TABLE_NAME
					+ " WHERE " + SO_InvoiceDetailTable.INVOICEMASTERID_FLD + "=?";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.AddWithValue(SO_InvoiceDetailTable.INVOICEMASTERID_FLD, pintMasterID);
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_InvoiceDetailTable.TABLE_NAME);

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
		public DataSet ListForReview(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				var strSql = " SELECT DISTINCT Bin.Code AS BCode, L.Code As LCode," +
                    "  CA.Code ITM_CategoryCode, G.Code PartNo, G.Description, G.Revision,H.Code UMCode, " +
                    " C.SaleOrderDetailID, E.DeliveryScheduleID, G.ProductID, ISNULL(G.AllowNegativeQty,0) AllowNegativeQty," +
                    " E.ScheduleDate, A.PONumber, GA.Code SO_GateCode, E.DeliveryQuantity AS CommittedQuantity," +
                    " A.InvoiceQty, A.InvoiceQty OldInvoiceQty, ISNULL(BC.OHQuantity,0) - ISNULL(BC.CommitQuantity,0) AvailableQty, " +
                    " A.Price, A.NetAmount, A.NetAmount * ISNULL(B.ExchangeRate,1) AS NetAmountRate, A.VATPercent, A.VATAmount, A.InvoiceMasterID ," +
					" A.InvoiceDetailID, B.LocationID,B.BinID," +
                    " C.SellingUMID, C.SaleOrderMasterID,D.Code, C.SaleOrderLine, E.Line" +
					" FROM 	SO_InvoiceDetail A INNER JOIN SO_InvoiceMaster B ON A.InvoiceMasterID = B.InvoiceMasterID" +
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
					" WHERE	B.InvoiceMasterID = ?";

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				ocmdPCS.Parameters.AddWithValue(SO_ConfirmShipDetailTable.CONFIRMSHIPMASTERID_FLD,pintMasterID);
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_InvoiceDetailTable.TABLE_NAME);

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
	}
}
