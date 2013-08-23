using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;


namespace PCSComProcurement.Purchase.DS
{
	
	public class PO_PurchaseOrderDetailDS //
	{
		public PO_PurchaseOrderDetailDS()
		{
		}

		private const string THIS = "PCSComProcurement.Purchase.DS.DS.PO_PurchaseOrderDetailDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_PurchaseOrderDetail
		///    </Description>
		///    <Inputs>
		///        PO_PurchaseOrderDetailVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, March 01, 2005
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
				PO_PurchaseOrderDetailVO objObject = (PO_PurchaseOrderDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO PO_PurchaseOrderDetail("
					+ PO_PurchaseOrderDetailTable.LINE_FLD + ","
					+ PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.CLOSED_FLD + ","
					+ PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ PO_PurchaseOrderDetailTable.UNITPRICE_FLD + ","
					+ PO_PurchaseOrderDetailTable.VATAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.NETAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.VENDORITEM_FLD + ","
					+ PO_PurchaseOrderDetailTable.VENDORREVISION_FLD + ","
					+ PO_PurchaseOrderDetailTable.PRODUCTID_FLD + ","
					+ PO_PurchaseOrderDetailTable.STOCKUMID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + ","
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.BUYINGUMID_FLD + ","
					+ PO_PurchaseOrderDetailTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.VAT_FLD + ","
					+ PO_PurchaseOrderDetailTable.REOPEN_FLD + ","
					+ PO_PurchaseOrderDetailTable.APPROVERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.APPROVALDATE_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD].Value = objObject.RequiredDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.CLOSED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.CLOSED_FLD].Value = objObject.Closed;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].Value = objObject.OrderQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.VATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD].Value = objObject.VATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD].Value = objObject.ImportTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD].Value = objObject.SpecialTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].Value = objObject.DiscountAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.NETAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.NETAMOUNT_FLD].Value = objObject.NetAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.VENDORITEM_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.VENDORITEM_FLD].Value = objObject.VendorItem;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.VENDORREVISION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.VENDORREVISION_FLD].Value = objObject.VendorRevision;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD].Value = objObject.TotalDelivery;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.BUYINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].Value = objObject.BuyingUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.IMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].Value = objObject.ImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.SPECIALTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.VAT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.REOPEN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.REOPEN_FLD].Value = objObject.ReOpen;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.APPROVERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.APPROVERID_FLD].Value = objObject.ApproverID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.APPROVALDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.APPROVALDATE_FLD].Value = objObject.ApprovalDate;


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

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from PO_PurchaseOrderDetail
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
		///       HungLa
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
			strSql = "DELETE " + PO_PurchaseOrderDetailTable.TABLE_NAME + " WHERE  " + "PurchaseOrderDetailID" + "=" + pintID.ToString();
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from PO_PurchaseOrderDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_PurchaseOrderDetailVO
		///    </Outputs>
		///    <Returns>
		///       PO_PurchaseOrderDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, March 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public bool GetPurchaseOrderApprovalStatus(int pintPurchaseOrderDetailID)
		{
			const string METHOD_NAME = THIS + ".GetPurchaseOrderApprovalStatus()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ PO_PurchaseOrderDetailTable.APPROVERID_FLD 
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "=" + pintPurchaseOrderDetailID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();

				if (objResult == DBNull.Value)
				{
					return false;
				}
				else
				{
					string strResult = objResult.ToString();
					if (strResult == String.Empty)
					{
						return false;
					}
					else
					{
						return true;
					}
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


		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from PO_PurchaseOrderDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_PurchaseOrderDetailVO
		///    </Outputs>
		///    <Returns>
		///       PO_PurchaseOrderDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, March 01, 2005
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
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_PurchaseOrderDetailTable.LINE_FLD + ","
					+ PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.CLOSED_FLD + ","
					+ PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ PO_PurchaseOrderDetailTable.UNITPRICE_FLD + ","
					+ PO_PurchaseOrderDetailTable.VATAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.NETAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.VENDORITEM_FLD + ","
					+ PO_PurchaseOrderDetailTable.VENDORREVISION_FLD + ","
					+ PO_PurchaseOrderDetailTable.PRODUCTID_FLD + ","
					+ PO_PurchaseOrderDetailTable.STOCKUMID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + ","
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.BUYINGUMID_FLD + ","
					+ PO_PurchaseOrderDetailTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.VAT_FLD + ","
					+ PO_PurchaseOrderDetailTable.REOPEN_FLD + ","
					+ PO_PurchaseOrderDetailTable.APPROVERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.APPROVALDATE_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_PurchaseOrderDetailVO objObject = new PO_PurchaseOrderDetailVO();

				while (odrPCS.Read())
				{
					objObject.PurchaseOrderDetailID = int.Parse(odrPCS[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.LINE_FLD] != DBNull.Value)
						objObject.Line = int.Parse(odrPCS[PO_PurchaseOrderDetailTable.LINE_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD] != DBNull.Value)
						objObject.RequiredDate = DateTime.Parse(odrPCS[PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.CLOSED_FLD] != DBNull.Value)
						objObject.Closed = bool.Parse(odrPCS[PO_PurchaseOrderDetailTable.CLOSED_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] != DBNull.Value)
						objObject.OrderQuantity = Decimal.Parse(odrPCS[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.UNITPRICE_FLD] != DBNull.Value)
						objObject.UnitPrice = Decimal.Parse(odrPCS[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value)
						objObject.VATAmount = Decimal.Parse(odrPCS[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] != DBNull.Value)
						objObject.ImportTaxAmount = Decimal.Parse(odrPCS[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD] != DBNull.Value)
						objObject.SpecialTaxAmount = Decimal.Parse(odrPCS[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD] != DBNull.Value)
						objObject.TotalAmount = Decimal.Parse(odrPCS[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
						objObject.DiscountAmount = Decimal.Parse(odrPCS[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.NETAMOUNT_FLD] != DBNull.Value)
						objObject.NetAmount = Decimal.Parse(odrPCS[PO_PurchaseOrderDetailTable.NETAMOUNT_FLD].ToString());
					objObject.VendorItem = odrPCS[PO_PurchaseOrderDetailTable.VENDORITEM_FLD].ToString();
					objObject.VendorRevision = odrPCS[PO_PurchaseOrderDetailTable.VENDORREVISION_FLD].ToString();
					if (odrPCS[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] != DBNull.Value)
						objObject.ProductID = int.Parse(odrPCS[PO_PurchaseOrderDetailTable.PRODUCTID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.STOCKUMID_FLD] != DBNull.Value)
						objObject.StockUMID = int.Parse(odrPCS[PO_PurchaseOrderDetailTable.STOCKUMID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD] != DBNull.Value)
						objObject.TotalDelivery = Decimal.Parse(odrPCS[PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD] != DBNull.Value)
						objObject.PurchaseOrderMasterID = int.Parse(odrPCS[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] != DBNull.Value)
						objObject.BuyingUMID = int.Parse(odrPCS[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD] != DBNull.Value)
						objObject.ImportTax = Decimal.Parse(odrPCS[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD] != DBNull.Value)
						objObject.SpecialTax = Decimal.Parse(odrPCS[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.VAT_FLD] != DBNull.Value)
						objObject.VAT = Decimal.Parse(odrPCS[PO_PurchaseOrderDetailTable.VAT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.REOPEN_FLD] != DBNull.Value)
						objObject.ReOpen = bool.Parse(odrPCS[PO_PurchaseOrderDetailTable.REOPEN_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.APPROVERID_FLD] != DBNull.Value)
						objObject.ApproverID = int.Parse(odrPCS[PO_PurchaseOrderDetailTable.APPROVERID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderDetailTable.APPROVALDATE_FLD] != DBNull.Value)
						objObject.ApprovalDate = DateTime.Parse(odrPCS[PO_PurchaseOrderDetailTable.APPROVALDATE_FLD].ToString());

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
		///       This method uses to update data to PO_PurchaseOrderDetail
		///    </Description>
		///    <Inputs>
		///       PO_PurchaseOrderDetailVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
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

			PO_PurchaseOrderDetailVO objObject = (PO_PurchaseOrderDetailVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE PO_PurchaseOrderDetail SET "
					+ PO_PurchaseOrderDetailTable.LINE_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.CLOSED_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.UNITPRICE_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.VATAMOUNT_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.NETAMOUNT_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.VENDORITEM_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.VENDORREVISION_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.PRODUCTID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.STOCKUMID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.BUYINGUMID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.IMPORTTAX_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.SPECIALTAX_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.VAT_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.REOPEN_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.APPROVERID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.APPROVALDATE_FLD + "=  ?"
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD].Value = objObject.RequiredDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.CLOSED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.CLOSED_FLD].Value = objObject.Closed;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].Value = objObject.OrderQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.VATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD].Value = objObject.VATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD].Value = objObject.ImportTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD].Value = objObject.SpecialTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].Value = objObject.DiscountAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.NETAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.NETAMOUNT_FLD].Value = objObject.NetAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.VENDORITEM_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.VENDORITEM_FLD].Value = objObject.VendorItem;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.VENDORREVISION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.VENDORREVISION_FLD].Value = objObject.VendorRevision;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD].Value = objObject.TotalDelivery;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.BUYINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].Value = objObject.BuyingUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.IMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].Value = objObject.ImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.SPECIALTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.VAT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.REOPEN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.REOPEN_FLD].Value = objObject.ReOpen;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.APPROVERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.APPROVERID_FLD].Value = objObject.ApproverID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.APPROVALDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.APPROVALDATE_FLD].Value = objObject.ApprovalDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].Value = objObject.PurchaseOrderDetailID;


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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update TotalDelivery to PO_PurchaseOrderDetail each time receive
		///    </Description>
		///    <Inputs>
		///       ReceiveQuantity, PODetailID
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       29-Mar-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateTotalDelivery(decimal pdecReceiveQuantity, int pintPODetailID)
		{
			const string METHOD_NAME = THIS + ".UpdateTotalDelivery()";

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE " + PO_PurchaseOrderDetailTable.TABLE_NAME + " SET "
					+ PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + "= ?"
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD].Value = pdecReceiveQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].Value = pintPODetailID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 0)
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get TotalDelivery from PO_PurchaseOrderDetail
		///    </Description>
		///    <Inputs>
		///       PODetailID
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       29-Mar-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetTotalDelivery(int pintPODetailID)
		{
			const string METHOD_NAME = THIS + ".GetTotalDelivery()";

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "SELECT ISNULL("
					+ PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + ", 0)"
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].Value = pintPODetailID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objReturn = ocmdPCS.ExecuteScalar();
				if (objReturn != null)
				{
					return decimal.Parse(objReturn.ToString());
				}
				else
				{
					return 0;
				}
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 0)
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get TotalDelivery from PO_PurchaseOrderDetail
		///    </Description>
		///    <Inputs>
		///       PODetailID
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       29-Mar-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetOrderQuantity(int pintPODetailID)
		{
			const string METHOD_NAME = THIS + ".GetTotalDelivery()";

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "SELECT ISNULL("
					+ PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + ", 0)"
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].Value = pintPODetailID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objReturn = ocmdPCS.ExecuteScalar();
				if (objReturn != null)
				{
					return decimal.Parse(objReturn.ToString());
				}
				else
				{
					return 0;
				}
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 0)
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from PO_PurchaseOrderDetail
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
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, March 01, 2005
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
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_PurchaseOrderDetailTable.LINE_FLD + ","
					+ PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.CLOSED_FLD + ","
					+ PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ PO_PurchaseOrderDetailTable.UNITPRICE_FLD + ","
					+ PO_PurchaseOrderDetailTable.VATAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.NETAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.VENDORITEM_FLD + ","
					+ PO_PurchaseOrderDetailTable.VENDORREVISION_FLD + ","
					+ PO_PurchaseOrderDetailTable.PRODUCTID_FLD + ","
					+ PO_PurchaseOrderDetailTable.STOCKUMID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + ","
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.BUYINGUMID_FLD + ","
					+ PO_PurchaseOrderDetailTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.VAT_FLD + ","
					+ PO_PurchaseOrderDetailTable.REOPEN_FLD + ","
					+ PO_PurchaseOrderDetailTable.APPROVERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.APPROVALDATE_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderDetailTable.TABLE_NAME);

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
		public DataSet List(string pstrIDs)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + " IN " + pstrIDs;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderDetailTable.TABLE_NAME);

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
		/// GetPurchaseOrderDetail
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasterLocationID"></param>
		/// <param name="pintPurchaseOrderMasterID"></param>
		/// <param name="pblnClose"></param>
		/// <param name="pdtmFromScheduleDate"></param>
		/// <param name="pdtmToScheduleDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		public DataSet GetPurchaseOrderDetail(int pintCCNID, int pintMasterLocationID, int pintPurchaseOrderMasterID, bool pblnClose, DateTime pdtmFromScheduleDate, DateTime pdtmToScheduleDate)
		{
			const string METHOD_NAME = THIS + ".GetPurchaseOrderDetail()";
			DataSet dstPCS = new DataSet();
			DateTime dtmSpecialDate = new DateTime(1, 1, 1);
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT Distinct Sel, Status, PurchaseOrderNo, Line, PurchaseOrderDetailID, Closed, Code,"  
						+ "Description, Revision, UM, OrderQuantity, VendorCode, VendorName"
						+ " FROM V_CloseOrOpenPO"
						+ " WHERE " + PO_PurchaseOrderMasterTable.CCNID_FLD + " = " + pintCCNID.ToString()
						+ " AND " + PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + " = " + pintMasterLocationID.ToString();
				//Add some parametters
				//Close/Open
				if (pblnClose)
				{
					strSql += " AND " + PO_PurchaseOrderDetailTable.CLOSED_FLD + " = " + 1.ToString();
				}
				else
					strSql += " AND " + PO_PurchaseOrderDetailTable.CLOSED_FLD + " = " + 0.ToString();
				//PurchaseOrderMaster
				if (pintPurchaseOrderMasterID != 0)
				{
					strSql += " AND " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + " = " + pintPurchaseOrderMasterID.ToString();
				}
				//FromScheduleDate
				if (pdtmFromScheduleDate != dtmSpecialDate)
				{
					strSql += " AND DATEDIFF(Second, " + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ",'" + pdtmFromScheduleDate + "') <= 0 ";
				}
				//ToScheduleDate
				if (pdtmToScheduleDate != dtmSpecialDate)
				{
					strSql += " AND DATEDIFF(Second, " + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ",'" + pdtmToScheduleDate + "') >= 0 ";
				}
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderDetailTable.TABLE_NAME);

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
		///       This method uses to get all data from PO_PurchaseOrderDetail
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
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, March 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetListOfReceivedProductsFromPurchaseOrder(int pintPurchaseOrderMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT * FROM v_PO_PurchaseOrderDetailReceipt WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + " = " + pintPurchaseOrderMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderDetailTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get approval level
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
		///       Trada
		///    </Authors>
		///    <History>
		///       March 12, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetApprovalLevel(int pintApproverID)
		{
			const string METHOD_NAME = THIS + ".GetApprovalLevel()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				/*
				strSql=	"SELECT MST_EmployeeApprovalLevel.EmployeeID,Code,Name,Level,Amount FROM "
					+ "MST_ApprovalLevel,"
					+ "MST_EmployeeApprovalLevel,"
					+ "MST_Employee WHERE "
					+ "MST_ApprovalLevel.ApprovalLevelID = MST_EmployeeApprovalLevel.ApprovalLevelID "
					+ "AND MST_Employee.EmployeeID = MST_EmployeeApprovalLevel.EmployeeID "
					+ "AND MST_Employee.EmployeeID = " +  pintApproverID.ToString();
				*/
				strSql=	"SELECT " 
					+ MST_EmployeeApprovalLevelTable.TABLE_NAME + "." + MST_EmployeeApprovalLevelTable.EMPLOYEEID_FLD + ", " 
					+ MST_EmployeeTable.CODE_FLD + ", " 
					+ MST_EmployeeTable.NAME_FLD + ", "
					+ MST_ApprovalLevelTable.LEVEL_FLD + ", "
					+ MST_ApprovalLevelTable.AMOUNT_FLD 
					+ " FROM "
					+ MST_ApprovalLevelTable.TABLE_NAME + ", "
					+ MST_EmployeeApprovalLevelTable.TABLE_NAME + ", "
					+ MST_EmployeeTable.TABLE_NAME 
					+ " WHERE "
					+ MST_ApprovalLevelTable.TABLE_NAME + "." + MST_ApprovalLevelTable.APPROVALLEVELID_FLD + " = " 
					+ MST_EmployeeApprovalLevelTable.TABLE_NAME + "." + MST_EmployeeApprovalLevelTable.APPROVALLEVELID_FLD
					+ " AND " + MST_EmployeeTable.TABLE_NAME + "." + MST_EmployeeTable.EMPLOYEEID_FLD + " = " 
					+ MST_EmployeeApprovalLevelTable.TABLE_NAME + "." + MST_EmployeeApprovalLevelTable.EMPLOYEEID_FLD
					+ " AND " + MST_EmployeeTable.TABLE_NAME + "." + MST_EmployeeTable.EMPLOYEEID_FLD + " = " +  pintApproverID.ToString();
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderDetailTable.TABLE_NAME);

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
		///       This method uses to update data to PO_PurchaseOrderDetail
		///    </Description>
		///    <Inputs>
		///       PO_PurchaseOrderDetailVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       trada
		///    </Authors>
		///    <History>
		///       09-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateApprover(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".UpdateApprover()";

			PO_PurchaseOrderDetailVO objObject = (PO_PurchaseOrderDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PO_PurchaseOrderDetail SET "
					+ PO_PurchaseOrderDetailTable.APPROVERID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderDetailTable.APPROVALDATE_FLD + "=  ?"
					+" WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.APPROVERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.APPROVERID_FLD].Value = objObject.ApproverID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.APPROVALDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.APPROVALDATE_FLD].Value = objObject.ApprovalDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].Value = objObject.PurchaseOrderDetailID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
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
		
		/// <summary>
		/// CloseOrOpenPOLines
		/// </summary>
		/// <param name="pblnPOClose"></param>
		/// <param name="pstrListOfIDs"></param>
		/// <author>Trada</author>
		/// <date>Monday, Nov 29 2005</date>
		public void CloseOrOpenPOLines(bool pblnPOClose, string pstrListOfIDs)
		{
			const string METHOD_NAME = THIS + ".CloseOrOpenPOLines()";
			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE PO_PurchaseOrderDetail SET "
					+ PO_PurchaseOrderDetailTable.CLOSED_FLD + "=   ?" 
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + " IN (" + pstrListOfIDs + ")";
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderDetailTable.CLOSED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_PurchaseOrderDetailTable.CLOSED_FLD].Value = pblnPOClose;

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
		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_SaleOrderDetail
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
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet List(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.LINE_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + ","
					+ ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CODE_FLD + " AS " + ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ADJUSTMENT1_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ADJUSTMENT2_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.BUYINGUMID_FLD + ","
					+ MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " AS " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.UNITPRICE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.VAT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.VATAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.NETAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.CLOSED_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.REOPEN_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.VENDORITEM_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.VENDORREVISION_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.UMRATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.APPROVERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.APPROVALDATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.STOCKUMID_FLD + ","
					+ " ( SELECT Count(*) From " + PO_DeliveryScheduleTable.TABLE_NAME + " WHERE " + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD 
					+ " = " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ") as " + PO_DeliveryScheduleTable.DELIVERYLINE_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD
					+ " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
					+ " INNER JOIN " + MST_UnitOfMeasureTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.BUYINGUMID_FLD
					+ " = " + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " LEFT JOIN " + ITM_CategoryTable.TABLE_NAME
					+ " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + "="
					+ ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CATEGORYID_FLD
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + pintMasterID.ToString()
					+ " ORDER BY " + PO_PurchaseOrderDetailTable.LINE_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderDetailTable.TABLE_NAME);

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
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, March 01, 2005
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
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_PurchaseOrderDetailTable.LINE_FLD + ","
					+ PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.CLOSED_FLD + ","
					+ PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ PO_PurchaseOrderDetailTable.UNITPRICE_FLD + ","
					+ PO_PurchaseOrderDetailTable.VATAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.NETAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.VENDORITEM_FLD + ","
					+ PO_PurchaseOrderDetailTable.VENDORREVISION_FLD + ","
					+ PO_PurchaseOrderDetailTable.PRODUCTID_FLD + ","
					+ PO_PurchaseOrderDetailTable.STOCKUMID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + ","
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.BUYINGUMID_FLD + ","
					+ PO_PurchaseOrderDetailTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.VAT_FLD + ","
					+ PO_PurchaseOrderDetailTable.REOPEN_FLD + ","
					+ PO_PurchaseOrderDetailTable.APPROVERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.UMRATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.APPROVALDATE_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, PO_PurchaseOrderDetailTable.TABLE_NAME);

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
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, March 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSetForReceipt(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql = "SELECT "
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, PO_PurchaseOrderDetailTable.TABLE_NAME);

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
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, March 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSetForApproving(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSetForApproving()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql = "SELECT "
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_PurchaseOrderDetailTable.APPROVERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.APPROVALDATE_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, pData.Tables[0].TableName);
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to close PO_PurchaseOrderDetail
		///    </Description>
		///    <Inputs>
		///       int PurchaseOrderDetailID
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       09-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void CloseLine(int pintPurchaseOrderDetailID)
		{
			const string METHOD_NAME = THIS + ".CloseLine()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + PO_PurchaseOrderDetailTable.TABLE_NAME + " SET "
					+ PO_PurchaseOrderDetailTable.CLOSED_FLD + "= 1"
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "=" + pintPurchaseOrderDetailID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else
					{
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
					}
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
		
		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from PO_PurchaseOrderDetail
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
		///       TuanDM
		///    </Authors>
		///    <History>
		///       Tuesday, March 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListToGetID(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".ListToGetID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_PurchaseOrderDetailTable.LINE_FLD + ","
					+ PO_PurchaseOrderDetailTable.PRODUCTID_FLD + ""
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE "+ PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + pintMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderDetailTable.TABLE_NAME);

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


		public void UpdateDataSetForImport(DataSet pData, int intMasterID)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				string strSql = "SELECT "
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_PurchaseOrderDetailTable.LINE_FLD + ","
					+ PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.CLOSED_FLD + ","
					+ PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ PO_PurchaseOrderDetailTable.UNITPRICE_FLD + ","
					+ PO_PurchaseOrderDetailTable.VATAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.NETAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.VENDORITEM_FLD + ","
					+ PO_PurchaseOrderDetailTable.VENDORREVISION_FLD + ","
					+ PO_PurchaseOrderDetailTable.PRODUCTID_FLD + ","
					+ PO_PurchaseOrderDetailTable.STOCKUMID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + ","
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.BUYINGUMID_FLD + ","
					+ PO_PurchaseOrderDetailTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.VAT_FLD + ","
					+ PO_PurchaseOrderDetailTable.REOPEN_FLD + ","
					+ PO_PurchaseOrderDetailTable.APPROVERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.UMRATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.APPROVALDATE_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + intMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, PO_PurchaseOrderDetailTable.TABLE_NAME);
				pData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Clear();
				strSql = "SELECT "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.LINE_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + ","
					+ ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CODE_FLD + " AS " + ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ADJUSTMENT1_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ADJUSTMENT2_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.BUYINGUMID_FLD + ","
					+ MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " AS " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.UNITPRICE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.VAT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.VATAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.NETAMOUNT_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.CLOSED_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.REOPEN_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.VENDORITEM_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.VENDORREVISION_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.UMRATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.APPROVERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.APPROVALDATE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.STOCKUMID_FLD + ","
					+ " ( SELECT Count(*) From " + PO_DeliveryScheduleTable.TABLE_NAME + " WHERE " + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD 
					+ " = " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ") as " + PO_DeliveryScheduleTable.DELIVERYLINE_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD
					+ " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
					+ " INNER JOIN " + MST_UnitOfMeasureTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.BUYINGUMID_FLD
					+ " = " + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " LEFT JOIN " + ITM_CategoryTable.TABLE_NAME
					+ " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + "="
					+ ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CATEGORYID_FLD
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + intMasterID
					+ " ORDER BY " + PO_PurchaseOrderDetailTable.LINE_FLD;
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odadPCS.Fill(pData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME]);
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
		
		public void DeleteByMaster(string pstrMasterID)
		{
			const string METHOD_NAME = THIS + ".DeleteByPOMaster()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "DELETE " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + " IN ("
					+ pstrMasterID + " )";
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
	}
}