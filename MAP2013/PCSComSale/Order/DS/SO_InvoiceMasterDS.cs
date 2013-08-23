using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComSale.Order.DS
{
	/// <summary>
	/// Summary description for SO_InvoiceMasterDS.
	/// </summary>
	public class SO_InvoiceMasterDS 
	{
		private const string THIS = "PCSComSale.Order.DS.SO_InvoiceMasterDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to get master vo
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Nguyen An Duong - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, October 28, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetCommitedDelSchLines(int pintSOMasterID, string strGateIDs)
		{
			const string METHOD_NAME = THIS + ".GetCommitedDelSchLines()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{
				string strSql = String.Empty;
				strSql = " SELECT"
					+ " C.Code, B.SaleOrderLine, A.Line,"
					+
					" D.Code PartNo, D.Description, D.Revision, CA.Code ITM_CategoryCode, F.Code UMCode, B.SaleOrderDetailID, A.DeliveryScheduleID, D.ProductID, "
					+
					" IsNull((SELECT SUM(CommitQuantity) FROM SO_CommitInventoryDetail E WHERE E.DeliveryScheduleID = A.DeliveryScheduleID AND IsNull(E.Shipped,0) = 0),0) CommittedQuantity,"
					+
					" 0.0 InvoiceQty, 0.0 OldInvoiceQty, 0.0 AvailableQty, B.UnitPrice Price, B.VATPercent, 0.0 VATAmount,G.Code SO_GateCode,"
					+ " CID.LocationID, CID.BINID "
					+ " FROM "
					+ " SO_DeliverySchedule A INNER JOIN SO_SaleOrderDetail B ON A.SaleOrderDetailID = B.SaleOrderDetailID"
					+ " INNER JOIN SO_SaleOrderMaster C ON B.SaleOrderMasterID = C.SaleOrderMasterID"
					+ " INNER JOIN ITM_Product D ON B.ProductID = D.ProductID"
					+ " LEFT JOIN ITM_Category CA ON CA.CategoryID = D.CategoryID"
					+ " LEFT JOIN SO_CommitInventoryDetail CID ON CID.DeliveryScheduleID = A.DeliveryScheduleID"
					+ " LEFT JOIN SO_Gate G ON G.GateID = A.GateID"
					+ " INNER JOIN MST_UnitOfMeasure F ON B.SellingUMID = F.UnitOfMeasureID"
					+ " WHERE C.SaleOrderMasterID = ?"
					+
					" AND IsNull((SELECT SUM(CommitQuantity) FROM SO_CommitInventoryDetail E WHERE E.DeliveryScheduleID = A.DeliveryScheduleID AND IsNull(E.Shipped,0) = 0),0) > 0";
				if (strGateIDs != string.Empty)
				{
					strSql += " AND A." + SO_DeliveryScheduleTable.GATEID_FLD + " IN (" + strGateIDs + ")";
				}
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				ocmdPCS.Parameters.AddWithValue(SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD, pintSOMasterID);

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
			const string METHOD_NAME = THIS + ".Update()";

			SO_InvoiceMasterVO objObject = (SO_InvoiceMasterVO) pobjObjecVO;

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE SO_InvoiceMaster SET "
					+ SO_InvoiceMasterTable.CONFIRMSHIPNO_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.SHIPPEDDATE_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.SALEORDERMASTERID_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.CURRENCYID_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.EXCHANGERATE_FLD + "=   ?" + ","
					//+ SO_InvoiceMasterTable.GATEID_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.SHIPCODE_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.FROMPORT_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.CNO_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.MEASUREMENT_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.GROSSWEIGHT_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.NETWEIGHT_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.ISSUINGBANK_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.LCNO_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.VESSELNAME_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.COMMENT_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.REFERENCENO_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.INVOICENO_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.LCDATE_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.ONBOARDDATE_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.INVOICEDATE_FLD + "=   ?" + ","
					+ SO_InvoiceMasterTable.CCNID_FLD + "=  ?"
					+ " WHERE " + SO_InvoiceMasterTable.INVOICEMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.CONFIRMSHIPNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.CONFIRMSHIPNO_FLD].Value = objObject.ConfirmShipNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.SHIPPEDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.SHIPPEDDATE_FLD].Value = objObject.ShippedDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.SHIPCODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.SHIPCODE_FLD].Value = objObject.ShipCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.FROMPORT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.FROMPORT_FLD].Value = objObject.FromPort;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.CNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.CNO_FLD].Value = objObject.CNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.MEASUREMENT_FLD, OleDbType.Decimal));
				if (objObject.Measurement != 0)
				{
					ocmdPCS.Parameters[SO_InvoiceMasterTable.MEASUREMENT_FLD].Value = objObject.Measurement;
				}
				else
					ocmdPCS.Parameters[SO_InvoiceMasterTable.MEASUREMENT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.GROSSWEIGHT_FLD, OleDbType.Decimal));
				if (objObject.GrossWeight != 0)
				{
					ocmdPCS.Parameters[SO_InvoiceMasterTable.GROSSWEIGHT_FLD].Value = objObject.GrossWeight;
				}
				else
					ocmdPCS.Parameters[SO_InvoiceMasterTable.GROSSWEIGHT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.NETWEIGHT_FLD, OleDbType.Decimal));
				if (objObject.NetWeight != 0)
				{
					ocmdPCS.Parameters[SO_InvoiceMasterTable.NETWEIGHT_FLD].Value = objObject.NetWeight;
				}
				else
					ocmdPCS.Parameters[SO_InvoiceMasterTable.NETWEIGHT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.ISSUINGBANK_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.ISSUINGBANK_FLD].Value = objObject.IssuingBank;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.LCNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.LCNO_FLD].Value = objObject.LCNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.VESSELNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.VESSELNAME_FLD].Value = objObject.VesselName;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.COMMENT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.REFERENCENO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.REFERENCENO_FLD].Value = objObject.ReferenceNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.INVOICENO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.INVOICENO_FLD].Value = objObject.InvoiceNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.LCDATE_FLD, OleDbType.Date));
				if (objObject.LCDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[SO_InvoiceMasterTable.LCDATE_FLD].Value = objObject.LCDate;
				}
				else
					ocmdPCS.Parameters[SO_InvoiceMasterTable.LCDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.ONBOARDDATE_FLD, OleDbType.Date));
				if (objObject.OnBoardDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[SO_InvoiceMasterTable.ONBOARDDATE_FLD].Value = objObject.OnBoardDate;
				}
				else
					ocmdPCS.Parameters[SO_InvoiceMasterTable.ONBOARDDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.INVOICEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.INVOICEDATE_FLD].Value = objObject.InvoiceDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.INVOICEMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.INVOICEMASTERID_FLD].Value = objObject.InvoiceMasterID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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

		public void Add(Object pobjObjectVO)
		{
			throw new NotImplementedException();
		}

		public void UpdateDataSet(DataSet pData)
		{
			throw new NotImplementedException();
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_InvoiceMaster
		///    </Description>
		///    <Inputs>
		///        SO_InvoiceMasterVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       DuongNA
		///    </Authors>
		///    <History>
		///       Friday, October 28, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int AddReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddReturnID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			object objScalar = null;
			try
			{
				SO_InvoiceMasterVO objObject = (SO_InvoiceMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO SO_InvoiceMaster("
					+ SO_InvoiceMasterTable.CONFIRMSHIPNO_FLD + ","
					+ SO_InvoiceMasterTable.SHIPPEDDATE_FLD + ","
					+ SO_InvoiceMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_InvoiceMasterTable.MASTERLOCATIONID_FLD + ","
					+ SO_InvoiceMasterTable.CURRENCYID_FLD + ","
					+ SO_InvoiceMasterTable.EXCHANGERATE_FLD + ","
					+ SO_InvoiceMasterTable.SHIPCODE_FLD + ","
					+ SO_InvoiceMasterTable.FROMPORT_FLD + ","
					+ SO_InvoiceMasterTable.CNO_FLD + ","
					+ SO_InvoiceMasterTable.MEASUREMENT_FLD + ","
					+ SO_InvoiceMasterTable.GROSSWEIGHT_FLD + ","
					+ SO_InvoiceMasterTable.NETWEIGHT_FLD + ","
					+ SO_InvoiceMasterTable.ISSUINGBANK_FLD + ","
					+ SO_InvoiceMasterTable.LCNO_FLD + ","
					+ SO_InvoiceMasterTable.VESSELNAME_FLD + ","
					+ SO_InvoiceMasterTable.COMMENT_FLD + ","
					+ SO_InvoiceMasterTable.REFERENCENO_FLD + ","
					+ SO_InvoiceMasterTable.INVOICENO_FLD + ","
					+ SO_InvoiceMasterTable.LCDATE_FLD + ","
					+ SO_InvoiceMasterTable.ONBOARDDATE_FLD + ","
					+ SO_InvoiceMasterTable.INVOICEDATE_FLD + ","
					+ SO_InvoiceMasterTable.CCNID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"
					+ " SELECT @@IDENTITY";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.CONFIRMSHIPNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.CONFIRMSHIPNO_FLD].Value = objObject.ConfirmShipNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.SHIPPEDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.SHIPPEDDATE_FLD].Value = objObject.ShippedDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.SHIPCODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.SHIPCODE_FLD].Value = objObject.ShipCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.FROMPORT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.FROMPORT_FLD].Value = objObject.FromPort;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.CNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.CNO_FLD].Value = objObject.CNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.MEASUREMENT_FLD, OleDbType.Decimal));
				if (objObject.Measurement != 0)
				{
					ocmdPCS.Parameters[SO_InvoiceMasterTable.MEASUREMENT_FLD].Value = objObject.Measurement;
				}
				else
					ocmdPCS.Parameters[SO_InvoiceMasterTable.MEASUREMENT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.GROSSWEIGHT_FLD, OleDbType.Decimal));
				if (objObject.GrossWeight != 0)
				{
					ocmdPCS.Parameters[SO_InvoiceMasterTable.GROSSWEIGHT_FLD].Value = objObject.GrossWeight;
				}
				else
					ocmdPCS.Parameters[SO_InvoiceMasterTable.GROSSWEIGHT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.NETWEIGHT_FLD, OleDbType.Decimal));
				if (objObject.NetWeight != 0)
				{
					ocmdPCS.Parameters[SO_InvoiceMasterTable.NETWEIGHT_FLD].Value = objObject.NetWeight;
				}
				else
					ocmdPCS.Parameters[SO_InvoiceMasterTable.NETWEIGHT_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.ISSUINGBANK_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.ISSUINGBANK_FLD].Value = objObject.IssuingBank;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.LCNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.LCNO_FLD].Value = objObject.LCNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.VESSELNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.VESSELNAME_FLD].Value = objObject.VesselName;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.COMMENT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.REFERENCENO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.REFERENCENO_FLD].Value = objObject.ReferenceNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.INVOICENO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.INVOICENO_FLD].Value = objObject.InvoiceNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.LCDATE_FLD, OleDbType.Date));
				if (objObject.LCDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[SO_InvoiceMasterTable.LCDATE_FLD].Value = objObject.LCDate;
				}
				else
					ocmdPCS.Parameters[SO_InvoiceMasterTable.LCDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.ONBOARDDATE_FLD, OleDbType.Date));
				if (objObject.OnBoardDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[SO_InvoiceMasterTable.ONBOARDDATE_FLD].Value = objObject.OnBoardDate;
				}
				else
					ocmdPCS.Parameters[SO_InvoiceMasterTable.ONBOARDDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.INVOICEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.INVOICEDATE_FLD].Value = objObject.InvoiceDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_InvoiceMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_InvoiceMasterTable.CCNID_FLD].Value = objObject.CCNID;

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
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
	}
}
