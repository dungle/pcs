using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComSale.Order.DS
{
	public class SO_ConfirmShipMasterDS 
	{
		private const string THIS = "PCSComSale.Order.DS.SO_ConfirmShipMasterDS";

		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				SO_ConfirmShipMasterVO objObject = (SO_ConfirmShipMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO SO_ConfirmShipMaster("
					+ SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD + ","
					+ SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD + ","
					+ SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD + ","
					+ SO_ConfirmShipMasterTable.CCNID_FLD + ")"
					+ "VALUES(?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD].Value = objObject.ConfirmShipNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD].Value = objObject.ShippedDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.CCNID_FLD].Value = objObject.CCNID;

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
			strSql = "DELETE " + SO_ConfirmShipMasterTable.TABLE_NAME + " WHERE  " + "ConfirmShipMasterID" + "=" +
				pintID.ToString();
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
					+ SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD + ","
					+ SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD + ","
					+ SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD + ","
					+ SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD + ","
					+ SO_ConfirmShipMasterTable.CCNID_FLD
					+ " FROM " + SO_ConfirmShipMasterTable.TABLE_NAME
					+ " WHERE " + SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_ConfirmShipMasterVO objObject = new SO_ConfirmShipMasterVO();

				while (odrPCS.Read())
				{
					objObject.ConfirmShipMasterID =
						int.Parse(odrPCS[SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD].ToString().Trim());
					objObject.ConfirmShipNo = odrPCS[SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD].ToString().Trim();
					objObject.ShippedDate = DateTime.Parse(odrPCS[SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD].ToString().Trim());
					objObject.SaleOrderMasterID = int.Parse(odrPCS[SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD].ToString().Trim());
					objObject.MasterLocationID = int.Parse(odrPCS[SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[SO_ConfirmShipMasterTable.CCNID_FLD].ToString().Trim());
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

			SO_ConfirmShipMasterVO objObject = (SO_ConfirmShipMasterVO) pobjObjecVO;

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE SO_ConfirmShipMaster SET "
					+ SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.CURRENCYID_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.EXCHANGERATE_FLD + "=   ?" + ","
					//+ SO_ConfirmShipMasterTable.GATEID_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.SHIPCODE_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.FROMPORT_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.CNO_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.MEASUREMENT_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.GROSSWEIGHT_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.NETWEIGHT_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.ISSUINGBANK_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.LCNO_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.VESSELNAME_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.COMMENT_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.REFERENCENO_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.INVOICENO_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.LCDATE_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.ONBOARDDATE_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.INVOICEDATE_FLD + "=   ?" + ","
					+ SO_ConfirmShipMasterTable.CCNID_FLD + "=  ?"
					+ " WHERE " + SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD].Value = objObject.ConfirmShipNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD].Value = objObject.ShippedDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

//				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.GATEID_FLD, OleDbType.Integer));
//				if (objObject.GateID != 0)
//				{
//					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.GATEID_FLD].Value = objObject.GateID;
//				}
//				else
//					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.GATEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.SHIPCODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.SHIPCODE_FLD].Value = objObject.ShipCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.FROMPORT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.FROMPORT_FLD].Value = objObject.FromPort;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.CNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.CNO_FLD].Value = objObject.CNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.MEASUREMENT_FLD, OleDbType.Decimal));
				if (objObject.Measurement != 0)
				{
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.MEASUREMENT_FLD].Value = objObject.Measurement;
				}
				else
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.MEASUREMENT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.GROSSWEIGHT_FLD, OleDbType.Decimal));
				if (objObject.GrossWeight != 0)
				{
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.GROSSWEIGHT_FLD].Value = objObject.GrossWeight;
				}
				else
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.GROSSWEIGHT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.NETWEIGHT_FLD, OleDbType.Decimal));
				if (objObject.NetWeight != 0)
				{
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.NETWEIGHT_FLD].Value = objObject.NetWeight;
				}
				else
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.NETWEIGHT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.ISSUINGBANK_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.ISSUINGBANK_FLD].Value = objObject.IssuingBank;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.LCNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.LCNO_FLD].Value = objObject.LCNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.VESSELNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.VESSELNAME_FLD].Value = objObject.VesselName;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.COMMENT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.REFERENCENO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.REFERENCENO_FLD].Value = objObject.ReferenceNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.INVOICENO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.INVOICENO_FLD].Value = objObject.InvoiceNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.LCDATE_FLD, OleDbType.Date));
				if (objObject.LCDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.LCDATE_FLD].Value = objObject.LCDate;
				}
				else
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.LCDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.ONBOARDDATE_FLD, OleDbType.Date));
				if (objObject.OnBoardDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.ONBOARDDATE_FLD].Value = objObject.OnBoardDate;
				}
				else
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.ONBOARDDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.INVOICEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.INVOICEDATE_FLD].Value = objObject.InvoiceDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD].Value = objObject.ConfirmShipMasterID;

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
					+ SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD + ","
					+ SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD + ","
					+ SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD + ","
					+ SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD + ","
					+ SO_ConfirmShipMasterTable.CCNID_FLD
					+ " FROM " + SO_ConfirmShipMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_ConfirmShipMasterTable.TABLE_NAME);

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
					+ SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD + ","
					+ SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD + ","
					+ SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD + ","
					+ SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD + ","
					+ SO_ConfirmShipMasterTable.CCNID_FLD
					+ "  FROM " + SO_ConfirmShipMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, SO_ConfirmShipMasterTable.TABLE_NAME);
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

        public DataSet GetCommitedDelSchLines(int pintSOMasterId, string strGateIDs, DateTime pdtmFromDate, DateTime pdtmToDate, int locationId, int binId, int type, decimal exchangeRate = 1)
		{
			const string METHOD_NAME = THIS + ".GetCommitedDelSchLines()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{
			    string strSql = " SELECT CA.Code ITM_CategoryCode, D.Code PartNo, D.Description, D.Revision,"
			                    + "  F.Code UMCode, B.SaleOrderDetailID, A.DeliveryScheduleID, D.ProductID, "
                                + " A.ScheduleDate, G.Code SO_GateCode, DeliveryQuantity AS CommittedQuantity,"
			                    + " DeliveryQuantity InvoiceQty, 0.0 OldInvoiceQty, ISNULL(D.AllowNegativeQty,0) AllowNegativeQty,"
			                    + " ISNULL(BC.OHQuantity,0) AvailableQty, "
			                    + " B.UnitPrice Price, DeliveryQuantity * B.UnitPrice NetAmount, DeliveryQuantity * B.UnitPrice * " + exchangeRate + " NetAmountRate, "
                                + " B.VATPercent, DeliveryQuantity * ISNULL(B.VATPercent,0) * B.UnitPrice VATAmount,"
			                    + locationId + "  LocationID, " + binId + " BINID , C.Code, B.SaleOrderLine, A.Line"
			                    + " FROM SO_DeliverySchedule A INNER JOIN SO_SaleOrderDetail B ON A.SaleOrderDetailID = B.SaleOrderDetailID"
			                    + " INNER JOIN SO_SaleOrderMaster C ON B.SaleOrderMasterID = C.SaleOrderMasterID"
			                    + " INNER JOIN ITM_Product D ON B.ProductID = D.ProductID"
			                    + " LEFT JOIN ITM_Category CA ON CA.CategoryID = D.CategoryID"
			                    + " LEFT JOIN SO_Gate G ON G.GateID = A.GateID"
			                    + " INNER JOIN MST_UnitOfMeasure F ON B.SellingUMID = F.UnitOfMeasureID"
			                    + " LEFT JOIN IV_BinCache BC ON D.ProductID = BC.ProductID AND BC.BinID = " + binId
			                    + " WHERE C.SaleOrderMasterID = ?"
			                    + " AND A.ScheduleDate >= ?"
			                    + " AND A.ScheduleDate <= ?"
			                    + " AND A.DeliveryScheduleID NOT IN";
                if (type == (int)ShipViewType.PrintInvoice)
                {
                    strSql += " (SELECT DISTINCT DeliveryScheduleID FROM SO_InvoiceDetail)";
                }
                else
                {
                    strSql += string.Format(" (SELECT DISTINCT DeliveryScheduleID FROM {0})", SO_ConfirmShipDetailTable.TABLE_NAME);
                }
				if (strGateIDs != string.Empty)
				{
				    strSql += " AND A." + SO_DeliveryScheduleTable.GATEID_FLD + " IN (" + strGateIDs + ")";
				}
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.AddWithValue(SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD, pintSOMasterId);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate == DateTime.MinValue ? DateTime.Now : pdtmFromDate;
                ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate == DateTime.MinValue ? DateTime.Now : pdtmToDate;

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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

		public int AddReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddReturnID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			object objScalar = null;
			try
			{
				SO_ConfirmShipMasterVO objObject = (SO_ConfirmShipMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO SO_ConfirmShipMaster("
					+ SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD + ","
					+ SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD + ","
					+ SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD + ","
					+ SO_ConfirmShipMasterTable.CURRENCYID_FLD + ","
					+ SO_ConfirmShipMasterTable.EXCHANGERATE_FLD + ","
					//+ SO_ConfirmShipMasterTable.GATEID_FLD + ","
					+ SO_ConfirmShipMasterTable.SHIPCODE_FLD + ","
					+ SO_ConfirmShipMasterTable.FROMPORT_FLD + ","
					+ SO_ConfirmShipMasterTable.CNO_FLD + ","
					+ SO_ConfirmShipMasterTable.MEASUREMENT_FLD + ","
					+ SO_ConfirmShipMasterTable.GROSSWEIGHT_FLD + ","
					+ SO_ConfirmShipMasterTable.NETWEIGHT_FLD + ","
					+ SO_ConfirmShipMasterTable.ISSUINGBANK_FLD + ","
					+ SO_ConfirmShipMasterTable.LCNO_FLD + ","
					+ SO_ConfirmShipMasterTable.VESSELNAME_FLD + ","
					+ SO_ConfirmShipMasterTable.COMMENT_FLD + ","
					+ SO_ConfirmShipMasterTable.REFERENCENO_FLD + ","
					+ SO_ConfirmShipMasterTable.INVOICENO_FLD + ","
					+ SO_ConfirmShipMasterTable.LCDATE_FLD + ","
					+ SO_ConfirmShipMasterTable.ONBOARDDATE_FLD + ","
					+ SO_ConfirmShipMasterTable.INVOICEDATE_FLD + ","
					+ SO_ConfirmShipMasterTable.CCNID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"
					+ " SELECT @@IDENTITY";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD].Value = objObject.ConfirmShipNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD].Value = objObject.ShippedDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

//				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.GATEID_FLD, OleDbType.Integer));
//				if (objObject.GateID != 0)
//				{
//					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.GATEID_FLD].Value = objObject.GateID;
//				}
//				else
//					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.GATEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.SHIPCODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.SHIPCODE_FLD].Value = objObject.ShipCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.FROMPORT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.FROMPORT_FLD].Value = objObject.FromPort;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.CNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.CNO_FLD].Value = objObject.CNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.MEASUREMENT_FLD, OleDbType.Decimal));
				if (objObject.Measurement != 0)
				{
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.MEASUREMENT_FLD].Value = objObject.Measurement;
				}
				else
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.MEASUREMENT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.GROSSWEIGHT_FLD, OleDbType.Decimal));
				if (objObject.GrossWeight != 0)
				{
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.GROSSWEIGHT_FLD].Value = objObject.GrossWeight;
				}
				else
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.GROSSWEIGHT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.NETWEIGHT_FLD, OleDbType.Decimal));
				if (objObject.NetWeight != 0)
				{
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.NETWEIGHT_FLD].Value = objObject.NetWeight;
				}
				else
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.NETWEIGHT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.ISSUINGBANK_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.ISSUINGBANK_FLD].Value = objObject.IssuingBank;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.LCNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.LCNO_FLD].Value = objObject.LCNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.VESSELNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.VESSELNAME_FLD].Value = objObject.VesselName;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.COMMENT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.REFERENCENO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.REFERENCENO_FLD].Value = objObject.ReferenceNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.INVOICENO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.INVOICENO_FLD].Value = objObject.InvoiceNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.LCDATE_FLD, OleDbType.Date));
				if (objObject.LCDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.LCDATE_FLD].Value = objObject.LCDate;
				}
				else
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.LCDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.ONBOARDDATE_FLD, OleDbType.Date));
				if (objObject.OnBoardDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.ONBOARDDATE_FLD].Value = objObject.OnBoardDate;
				}
				else
					ocmdPCS.Parameters[SO_ConfirmShipMasterTable.ONBOARDDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.INVOICEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.INVOICEDATE_FLD].Value = objObject.InvoiceDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ConfirmShipMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ConfirmShipMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				objScalar = ocmdPCS.ExecuteScalar();
				return int.Parse(objScalar.ToString());
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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