using System;
using System.Data;
using System.Data.OleDb;

using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProcurement.Purchase.DS
{
	
	public class PO_PurchaseOrderMasterDS //
	{
		public PO_PurchaseOrderMasterDS()
		{
		}

		private const string THIS = "PCSComProcurement.Purchase.DS.DS.PO_PurchaseOrderMasterDS";
		private const string OPEN_AMOUNT = "openAmount";
		private const string RECEIVE_AMOUNT = "ReceiveAmount";
		private const string PO_NO = "PONo";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_PurchaseOrderMaster
		///    </Description>
		///    <Inputs>
		///        PO_PurchaseOrderMasterVO       
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
				PO_PurchaseOrderMasterVO objObject = (PO_PurchaseOrderMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO PO_PurchaseOrderMaster("
					+ PO_PurchaseOrderMasterTable.CODE_FLD + ","
					+ PO_PurchaseOrderMasterTable.ORDERDATE_FLD + ","
					+ PO_PurchaseOrderMasterTable.VAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CURRENCYID_FLD + ","
					+ PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CARRIERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.BUYERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALVAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.CCNID_FLD + ","
					+ PO_PurchaseOrderMasterTable.POREVISION_FLD + ","
					+ PO_PurchaseOrderMasterTable.PARTYID_FLD + ","
					+ PO_PurchaseOrderMasterTable.VENDORLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.INVTOLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD + ","
					+ PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PAUSEID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PRIORITY_FLD + ","
					+ PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD + ","
					+ PO_PurchaseOrderMasterTable.COMMENT_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.ORDERDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.ORDERDATE_FLD].Value = objObject.OrderDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.VAT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.IMPORTTAX_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.IMPORTTAX_FLD].Value = objObject.ImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.SPECIALTAX_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD].Value = objObject.DeliveryTermsID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD].Value = objObject.PaymentTermsID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CARRIERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CARRIERID_FLD].Value = objObject.CarrierID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD].Value = objObject.TotalImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.BUYERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.BUYERID_FLD].Value = objObject.BuyerID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALVAT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALVAT_FLD].Value = objObject.TotalVAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD].Value = objObject.TotalSpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD].Value = objObject.TotalDiscount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD].Value = objObject.TotalNetAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.POREVISION_FLD, OleDbType.Integer));
				if (objObject.PORevision > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.POREVISION_FLD].Value = objObject.PORevision;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.POREVISION_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.VENDORLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD].Value = objObject.VendorLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD].Value = objObject.ShipToLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.INVTOLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.INVTOLOCID_FLD].Value = objObject.InvToLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD].Value = objObject.PurchaseTypeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD].Value = objObject.DiscountTermID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PAUSEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PAUSEID_FLD].Value = objObject.PauseID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PRIORITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PRIORITY_FLD].Value = objObject.Priority;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD].Value = objObject.RecCompleted;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.COMMENT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.COMMENT_FLD].Value = objObject.Comment;


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
		///       This method uses to add data to PO_PurchaseOrderMaster
		///    </Description>
		///    <Inputs>
		///        PO_PurchaseOrderMasterVO       
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
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				PO_PurchaseOrderMasterVO objObject = (PO_PurchaseOrderMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO PO_PurchaseOrderMaster("
					
					+ PO_PurchaseOrderMasterTable.CODE_FLD + ","
					+ PO_PurchaseOrderMasterTable.ORDERDATE_FLD + ","
					+ PO_PurchaseOrderMasterTable.VAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.SPECIALTAX_FLD + ","

					+ PO_PurchaseOrderMasterTable.CURRENCYID_FLD + ","
					+ PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD + ","
					+ PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CARRIERID_FLD + ","
					
					+ PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.BUYERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALVAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD + ","
					
					+ PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.CCNID_FLD + ","
					+ PO_PurchaseOrderMasterTable.POREVISION_FLD + ","
					+ PO_PurchaseOrderMasterTable.PARTYID_FLD + ","
					
					+ PO_PurchaseOrderMasterTable.VENDORLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.INVTOLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD + ","
					
					+ PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PAUSEID_FLD + ","
					+ PO_PurchaseOrderMasterTable.MAKERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.MAKERLOCATIONID_FLD + ","
					+ PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD + ","
					
					+ PO_PurchaseOrderMasterTable.PRIORITY_FLD + ","
					+ PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD + ","
					+ PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PRICINGTYPEID_FLD + ","
					+ PO_PurchaseOrderMasterTable.REFERENCENO_FLD + ","
					+ PO_PurchaseOrderMasterTable.USERNAME_FLD + ","
					+ PO_PurchaseOrderMasterTable.LASTCHANGE_FLD + ","
					+ PO_PurchaseOrderMasterTable.COMMENT_FLD + ")"
					+ "VALUES(?,?,?,?,?, ?,?,?,?,?, ?,?,?,?,?, ?,?,?,?,?, ?,?,?,?,?, ?,?,?,?,?, ?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.ORDERDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.ORDERDATE_FLD].Value = objObject.OrderDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.VAT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.IMPORTTAX_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.IMPORTTAX_FLD].Value = objObject.ImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.SPECIALTAX_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD, OleDbType.Integer));
				if (objObject.DeliveryTermsID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD].Value = objObject.DeliveryTermsID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD, OleDbType.Integer));
				if (objObject.PaymentTermsID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD].Value = objObject.PaymentTermsID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CARRIERID_FLD, OleDbType.Integer));
				if (objObject.CarrierID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CARRIERID_FLD].Value = objObject.CarrierID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CARRIERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD].Value = objObject.TotalImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.BUYERID_FLD, OleDbType.Integer));
				if (objObject.BuyerID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.BUYERID_FLD].Value = objObject.BuyerID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.BUYERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALVAT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALVAT_FLD].Value = objObject.TotalVAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD].Value = objObject.TotalSpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD].Value = objObject.TotalDiscount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD].Value = objObject.TotalNetAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.POREVISION_FLD, OleDbType.TinyInt));
				if (objObject.PORevision > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.POREVISION_FLD].Value = objObject.PORevision;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.POREVISION_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PARTYID_FLD, OleDbType.Integer));
				if (objObject.PartyID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PARTYID_FLD].Value = objObject.PartyID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PARTYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.VENDORLOCID_FLD, OleDbType.Integer));
				if (objObject.VendorLocID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD].Value = objObject.VendorLocID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD, OleDbType.Integer));
				if (objObject.ShipToLocID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD].Value = objObject.ShipToLocID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.INVTOLOCID_FLD, OleDbType.Integer));
				if (objObject.InvToLocID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.INVTOLOCID_FLD].Value = objObject.InvToLocID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.INVTOLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				if (objObject.PartyContactID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD, OleDbType.Integer));
				if (objObject.PurchaseTypeID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD].Value = objObject.PurchaseTypeID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD, OleDbType.Integer));
				if (objObject.DiscountTermID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD].Value = objObject.DiscountTermID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PAUSEID_FLD, OleDbType.Integer));
				if (objObject.PauseID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PAUSEID_FLD].Value = objObject.PauseID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PAUSEID_FLD].Value = DBNull.Value;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.MAKERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.MAKERID_FLD].Value = objObject.MakerID;
	
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.MAKERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.MAKERLOCATIONID_FLD].Value = objObject.MakerLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD, OleDbType.Integer));
				if (objObject.RequestDeliveryTime != 0)
				{
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD].Value = objObject.RequestDeliveryTime;
				}
				else
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PRIORITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PRIORITY_FLD].Value = objObject.Priority;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD].Value = objObject.RecCompleted;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.MasterLocationID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PRICINGTYPEID_FLD, OleDbType.Integer));
				if (objObject.PricingTypeID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PRICINGTYPEID_FLD].Value = objObject.PricingTypeID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PRICINGTYPEID_FLD].Value = DBNull.Value;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.REFERENCENO_FLD, OleDbType.WChar));
				if (objObject.ReferenceNo != string.Empty)
				{
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.REFERENCENO_FLD].Value = objObject.ReferenceNo;
				}
				else
				{
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.REFERENCENO_FLD].Value = DBNull.Value;
				}

                ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.USERNAME_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.USERNAME_FLD].Value = SystemProperty.UserName;
                ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.LASTCHANGE_FLD, OleDbType.Date));
                if (objObject.LastChange != DateTime.MinValue && objObject.LastChange != DateTime.MaxValue)
                {
                    ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.LASTCHANGE_FLD].Value = objObject.LastChange;
                }
                else
                {
                    ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.LASTCHANGE_FLD].Value = DateTime.Now;
                }

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.COMMENT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.COMMENT_FLD].Value = objObject.Comment;

				strSql += " ; Select @@IDENTITY as NEWID";

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());
				//ocmdPCS.ExecuteNonQuery();	

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					if (ex.ErrorCode == ErrorCode.SQL_DATA_NOT_EXIST_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.MESSAGE_SOME_DATA_DELETED, METHOD_NAME, ex);
					}
					else throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to delete data from PO_PurchaseOrderMaster
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
			strSql = "DELETE " + PO_PurchaseOrderMasterTable.TABLE_NAME + " WHERE  " + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + "=" + pintID.ToString();
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
		///       This method uses to get data from PO_PurchaseOrderMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_PurchaseOrderMasterVO
		///    </Outputs>
		///    <Returns>
		///       PO_PurchaseOrderMasterVO
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
		public DateTime GetOrderDateByPOMasterID(int pintPOMasterID)
		{
			const string METHOD_NAME = THIS + ".GetOrderDateByPOMasterID()";

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ PO_PurchaseOrderMasterTable.ORDERDATE_FLD
					+ " FROM " + PO_PurchaseOrderMasterTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + "=" + pintPOMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_PurchaseOrderMasterVO objObject = new PO_PurchaseOrderMasterVO();

				while (odrPCS.Read())
				{
					objObject.OrderDate = DateTime.Parse(odrPCS[PO_PurchaseOrderMasterTable.ORDERDATE_FLD].ToString());

				}
				return objObject.OrderDate;
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
		/// GetObjectVO
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, July 13 2005</date>
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
					+ PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CODE_FLD + ","
					+ PO_PurchaseOrderMasterTable.ORDERDATE_FLD + ","
					+ PO_PurchaseOrderMasterTable.VAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CURRENCYID_FLD + ","
					+ PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CARRIERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.BUYERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALVAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.CCNID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PARTYID_FLD + ","
					+ PO_PurchaseOrderMasterTable.VENDORLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.INVTOLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD + ","
					+ PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PAUSEID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PRIORITY_FLD + ","
					+ PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD + ","
					+ PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD + ","
					+ PO_PurchaseOrderMasterTable.COMMENT_FLD
					+ " FROM " + PO_PurchaseOrderMasterTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_PurchaseOrderMasterVO objObject = new PO_PurchaseOrderMasterVO();

				while (odrPCS.Read())
				{
					if (odrPCS[PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD] != DBNull.Value)
						objObject.MasterLocationID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD].ToString());
					objObject.Code = odrPCS[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
					objObject.OrderDate = DateTime.Parse(odrPCS[PO_PurchaseOrderMasterTable.ORDERDATE_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.VAT_FLD] != DBNull.Value)
						objObject.VAT = bool.Parse(odrPCS[PO_PurchaseOrderMasterTable.VAT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.IMPORTTAX_FLD] != DBNull.Value)
						objObject.ImportTax = bool.Parse(odrPCS[PO_PurchaseOrderMasterTable.IMPORTTAX_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.SPECIALTAX_FLD] != DBNull.Value)
						objObject.SpecialTax = bool.Parse(odrPCS[PO_PurchaseOrderMasterTable.SPECIALTAX_FLD].ToString());
					objObject.PurchaseOrderMasterID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.CURRENCYID_FLD] != DBNull.Value)
						objObject.CurrencyID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.CURRENCYID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD] != DBNull.Value)
						objObject.DeliveryTermsID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD] != DBNull.Value)
						objObject.PaymentTermsID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.CARRIERID_FLD] != DBNull.Value)
						objObject.CarrierID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.CARRIERID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD] != DBNull.Value)
						objObject.TotalImportTax = Decimal.Parse(odrPCS[PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.BUYERID_FLD] != DBNull.Value)
						objObject.BuyerID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.BUYERID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.TOTALVAT_FLD] != DBNull.Value)
						objObject.TotalVAT = Decimal.Parse(odrPCS[PO_PurchaseOrderMasterTable.TOTALVAT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD] != DBNull.Value)
						objObject.TotalSpecialTax = Decimal.Parse(odrPCS[PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD] != DBNull.Value)
						objObject.TotalAmount = Decimal.Parse(odrPCS[PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD] != DBNull.Value)
						objObject.TotalDiscount = Decimal.Parse(odrPCS[PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD] != DBNull.Value)
						objObject.TotalNetAmount = Decimal.Parse(odrPCS[PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD].ToString());
					objObject.CCNID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.CCNID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.PARTYID_FLD] != DBNull.Value)
						objObject.PartyID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PARTYID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD] != DBNull.Value)
						objObject.VendorLocID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD] != DBNull.Value)
						objObject.ShipToLocID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.INVTOLOCID_FLD] != DBNull.Value)
						objObject.InvToLocID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.INVTOLOCID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD] != DBNull.Value)
						objObject.PartyContactID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD] != DBNull.Value)
						objObject.PurchaseTypeID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD] != DBNull.Value)
						objObject.DiscountTermID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.PAUSEID_FLD] != DBNull.Value)
						objObject.PauseID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PAUSEID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.PRIORITY_FLD] != DBNull.Value)
						objObject.Priority = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PRIORITY_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD] != DBNull.Value)
						objObject.RecCompleted = bool.Parse(odrPCS[PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD] != DBNull.Value)
						objObject.ExchangeRate = Convert.ToDecimal(odrPCS[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD]);
					objObject.Comment = odrPCS[PO_PurchaseOrderMasterTable.COMMENT_FLD].ToString();

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
		///       This method uses to get data from PO_PurchaseOrderMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_PurchaseOrderMasterVO
		///    </Outputs>
		///    <Returns>
		///       PO_PurchaseOrderMasterVO
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
		public object GetObjectVO(string pstrCode)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CODE_FLD + ","
					+ PO_PurchaseOrderMasterTable.ORDERDATE_FLD + ","
					+ PO_PurchaseOrderMasterTable.VAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CURRENCYID_FLD + ","
					+ PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CARRIERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.BUYERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALVAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.CCNID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PARTYID_FLD + ","
					+ PO_PurchaseOrderMasterTable.VENDORLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.INVTOLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD + ","
					+ PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PAUSEID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PRIORITY_FLD + ","
					+ PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD + ","
					+ PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD + ","
					+ PO_PurchaseOrderMasterTable.COMMENT_FLD
					+ " FROM " + PO_PurchaseOrderMasterTable.TABLE_NAME
					// HACK: dungla 10-28-2005
					+ " WHERE " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CODE_FLD + "=?";
				// END: dungla 10-28-2005

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				// HACK: dungla 10-28-2005
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CODE_FLD].Value = pstrCode;
				// END: dungla 10-28-2005

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_PurchaseOrderMasterVO objObject = new PO_PurchaseOrderMasterVO();

				while (odrPCS.Read())
				{
					if (odrPCS[PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD] != DBNull.Value)
						objObject.MasterLocationID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD].ToString());
					objObject.Code = odrPCS[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
					objObject.OrderDate = DateTime.Parse(odrPCS[PO_PurchaseOrderMasterTable.ORDERDATE_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.VAT_FLD] != DBNull.Value)
						objObject.VAT = bool.Parse(odrPCS[PO_PurchaseOrderMasterTable.VAT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.IMPORTTAX_FLD] != DBNull.Value)
						objObject.ImportTax = bool.Parse(odrPCS[PO_PurchaseOrderMasterTable.IMPORTTAX_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.SPECIALTAX_FLD] != DBNull.Value)
						objObject.SpecialTax = bool.Parse(odrPCS[PO_PurchaseOrderMasterTable.SPECIALTAX_FLD].ToString());
					objObject.PurchaseOrderMasterID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.CURRENCYID_FLD] != DBNull.Value)
						objObject.CurrencyID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.CURRENCYID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD] != DBNull.Value)
						objObject.DeliveryTermsID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD] != DBNull.Value)
						objObject.PaymentTermsID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.CARRIERID_FLD] != DBNull.Value)
						objObject.CarrierID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.CARRIERID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD] != DBNull.Value)
						objObject.TotalImportTax = Decimal.Parse(odrPCS[PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.BUYERID_FLD] != DBNull.Value)
						objObject.BuyerID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.BUYERID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.TOTALVAT_FLD] != DBNull.Value)
						objObject.TotalVAT = Decimal.Parse(odrPCS[PO_PurchaseOrderMasterTable.TOTALVAT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD] != DBNull.Value)
						objObject.TotalSpecialTax = Decimal.Parse(odrPCS[PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD] != DBNull.Value)
						objObject.TotalAmount = Decimal.Parse(odrPCS[PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD] != DBNull.Value)
						objObject.TotalDiscount = Decimal.Parse(odrPCS[PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD] != DBNull.Value)
						objObject.TotalNetAmount = Decimal.Parse(odrPCS[PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD].ToString());
					objObject.CCNID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.CCNID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.PARTYID_FLD] != DBNull.Value)
						objObject.PartyID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PARTYID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD] != DBNull.Value)
						objObject.VendorLocID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD] != DBNull.Value)
						objObject.ShipToLocID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.INVTOLOCID_FLD] != DBNull.Value)
						objObject.InvToLocID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.INVTOLOCID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD] != DBNull.Value)
						objObject.PartyContactID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD] != DBNull.Value)
						objObject.PurchaseTypeID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD] != DBNull.Value)
						objObject.DiscountTermID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.PAUSEID_FLD] != DBNull.Value)
						objObject.PauseID = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PAUSEID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.PRIORITY_FLD] != DBNull.Value)
						objObject.Priority = int.Parse(odrPCS[PO_PurchaseOrderMasterTable.PRIORITY_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD] != DBNull.Value)
						objObject.RecCompleted = bool.Parse(odrPCS[PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD] != DBNull.Value)
						objObject.ExchangeRate = Convert.ToDecimal(odrPCS[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD]);
					objObject.Comment = odrPCS[PO_PurchaseOrderMasterTable.COMMENT_FLD].ToString();

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
		///       This method uses to update data to PO_PurchaseOrderMaster
		///    </Description>
		///    <Inputs>
		///       PO_PurchaseOrderMasterVO       
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
		public void ClosePurchaseOrder(int pintPurchaseOrderMasterID)
		{
			const string METHOD_NAME = THIS + ".ClosePurchaseOrder()";
			const int CLOSED_STATUS = 1;

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				//first we have to check if all the Purchase order line are closed

				strSql = " SELECT Count(*) FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + pintPurchaseOrderMasterID
					+ "     AND " + PO_PurchaseOrderDetailTable.CLOSED_FLD + "<>" + CLOSED_STATUS;
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();

				int intResult = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				if (intResult == 0)
				{
					//All line are closed , so the master must be closed too
					strSql = "UPDATE PO_PurchaseOrderMaster SET "
						+ PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD + "=  1 "
						+ " WHERE " + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + "=" + pintPurchaseOrderMasterID;

					ocmdPCS.CommandText = strSql;
					ocmdPCS.ExecuteNonQuery();
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to PO_PurchaseOrderMaster
		///    </Description>
		///    <Inputs>
		///       PO_PurchaseOrderMasterVO       
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
		public void ClosePurchaseOrderLine(int pintPOLineID)
		{
			const string METHOD_NAME = THIS + ".ClosePurchaseOrderLine()";
			const int CLOSED_STATUS = 1;

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
					+ PO_PurchaseOrderDetailTable.CLOSED_FLD + "=  1 "
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "=" + pintPOLineID;

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to PO_PurchaseOrderMaster
		///    </Description>
		///    <Inputs>
		///       PO_PurchaseOrderMasterVO       
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

			PO_PurchaseOrderMasterVO objObject = (PO_PurchaseOrderMasterVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE PO_PurchaseOrderMaster SET "
					+ PO_PurchaseOrderMasterTable.CODE_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.ORDERDATE_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.VAT_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.IMPORTTAX_FLD + "=   ?" + ","
				
					+ PO_PurchaseOrderMasterTable.SPECIALTAX_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.CURRENCYID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD + "=   ?" + ","
				
					+ PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.CARRIERID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.BUYERID_FLD + "=   ?" + ","
				
					+ PO_PurchaseOrderMasterTable.TOTALVAT_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD + "=   ?" + ","
				
					+ PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.CCNID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.PARTYID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.VENDORLOCID_FLD + "=   ?" + ","
				
					+ PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.INVTOLOCID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD + "=   ?" + ","
				
					+ PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.PAUSEID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.MAKERID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.MAKERLOCATIONID_FLD + "=   ?" + ","
				
					+ PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.PRIORITY_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD + "=   ?" + ","
					+ PO_PurchaseOrderMasterTable.COMMENT_FLD + "=  ?,"
					+ PO_PurchaseOrderMasterTable.REFERENCENO_FLD + "=  ?,"
					+ PO_PurchaseOrderMasterTable.POREVISION_FLD + "=  ?,"
					+ PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD + "=  ?,"
					+ PO_PurchaseOrderMasterTable.PRICINGTYPEID_FLD + "=  ?,"
					+ PO_PurchaseOrderMasterTable.VENDORSO_FLD + "=  ?"
				
					+ " WHERE " + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.ORDERDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.ORDERDATE_FLD].Value = objObject.OrderDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.VAT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.IMPORTTAX_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.IMPORTTAX_FLD].Value = objObject.ImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.SPECIALTAX_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.MasterLocationID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				if (objObject.CurrencyID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;
				else
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CURRENCYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD, OleDbType.Integer));
				if (objObject.DeliveryTermsID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD].Value = objObject.DeliveryTermsID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD, OleDbType.Integer));
				if (objObject.PaymentTermsID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD].Value = objObject.PaymentTermsID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CARRIERID_FLD, OleDbType.Integer));
				if (objObject.CarrierID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CARRIERID_FLD].Value = objObject.CarrierID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CARRIERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD].Value = objObject.TotalImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.BUYERID_FLD, OleDbType.Integer));
				if (objObject.BuyerID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.BUYERID_FLD].Value = objObject.BuyerID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.BUYERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALVAT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALVAT_FLD].Value = objObject.TotalVAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD].Value = objObject.TotalSpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD].Value = objObject.TotalDiscount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD].Value = objObject.TotalNetAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.CCNID_FLD, OleDbType.Integer));
				if (objObject.CCNID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CCNID_FLD].Value = objObject.CCNID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.CCNID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PARTYID_FLD, OleDbType.Integer));
				if (objObject.PartyID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PARTYID_FLD].Value = objObject.PartyID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PARTYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.VENDORLOCID_FLD, OleDbType.Integer));
				if (objObject.VendorLocID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD].Value = objObject.VendorLocID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD, OleDbType.Integer));
				if (objObject.ShipToLocID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD].Value = objObject.ShipToLocID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.INVTOLOCID_FLD, OleDbType.Integer));
				if (objObject.InvToLocID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.INVTOLOCID_FLD].Value = objObject.InvToLocID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.INVTOLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				if (objObject.PartyContactID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD, OleDbType.Integer));
				if (objObject.PurchaseTypeID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD].Value = objObject.PurchaseTypeID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD, OleDbType.Integer));
				if (objObject.DiscountTermID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD].Value = objObject.DiscountTermID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PAUSEID_FLD, OleDbType.Integer));
				if (objObject.PauseID > 0)
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PAUSEID_FLD].Value = objObject.PauseID;
				else ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PAUSEID_FLD].Value = DBNull.Value;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.MAKERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.MAKERID_FLD].Value = objObject.MakerID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.MAKERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.MAKERLOCATIONID_FLD].Value = objObject.MakerLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD, OleDbType.Integer));
				if (objObject.RequestDeliveryTime != 0)
				{
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD].Value = objObject.RequestDeliveryTime;
				}
				else
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PRIORITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PRIORITY_FLD].Value = objObject.Priority;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD].Value = objObject.RecCompleted;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.COMMENT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.COMMENT_FLD].Value = objObject.Comment;	
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.REFERENCENO_FLD, OleDbType.VarChar));
				if (objObject.ReferenceNo != string.Empty)
				{
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.REFERENCENO_FLD].Value = objObject.ReferenceNo;
				}
				else
				{
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.REFERENCENO_FLD].Value = DBNull.Value;
				}
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.POREVISION_FLD, OleDbType.Integer));
				if (objObject.PORevision > 0)
				{
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.POREVISION_FLD].Value = objObject.PORevision;
				}
				else
				{
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.POREVISION_FLD].Value = DBNull.Value;
				}
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PRICINGTYPEID_FLD, OleDbType.Integer));
				if (objObject.PricingTypeID != 0)
				{
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PRICINGTYPEID_FLD].Value = objObject.PricingTypeID;
				}
				else
					ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PRICINGTYPEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.VENDORSO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.VENDORSO_FLD].Value = objObject.VendorSO;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;


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
		///       This method uses to get all data from PO_PurchaseOrderMaster
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
					+ PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CODE_FLD + ","
					+ PO_PurchaseOrderMasterTable.ORDERDATE_FLD + ","
					+ PO_PurchaseOrderMasterTable.VAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CURRENCYID_FLD + ","
					+ PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CARRIERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.BUYERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALVAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.CCNID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PARTYID_FLD + ","
					+ PO_PurchaseOrderMasterTable.VENDORLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.INVTOLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD + ","
					+ PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PAUSEID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PRIORITY_FLD + ","
					+ PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD + ","
					+ PO_PurchaseOrderMasterTable.COMMENT_FLD
					+ " FROM " + PO_PurchaseOrderMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderMasterTable.TABLE_NAME);

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
		/// CheckReferenceAndRevisionNo
		/// </summary>
		/// <param name="pstrReferenceNo"></param>
		/// <param name="pstrRevision"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, September 22 2006</date>
		public DataSet CheckReferenceAndRevisionNo(string pstrReferenceNo, string pstrRevision, int pintPOMasterID)
		{
			const string METHOD_NAME = THIS + ".CheckReferenceAndRevisionNo()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT COUNT(" 
					+ PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + ")"
					+ " FROM " + PO_PurchaseOrderMasterTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderMasterTable.REFERENCENO_FLD + " = '" + pstrReferenceNo + "'"
					+ " AND " + PO_PurchaseOrderMasterTable.POREVISION_FLD + " = '" + pstrRevision + "'";
				if (pintPOMasterID != 0)
				{
					strSql += " AND " + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + " != " + pintPOMasterID.ToString();
				}
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderMasterTable.TABLE_NAME);

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
		///       This method uses to get all data from PO_PurchaseOrderMaster
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
		public DataRow LoadObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".LoadObjectVO()";
			const string CURRENCY_CODE = "CURRENCY_CODE";
			const string MASTERLOCATION_CODE = "MASTER_LOCATION_CODE";
			const string DELIVERYTERM_CODE = "DELIVERYTERM_CODE";
			const string PAYMENTTERM_CODE = "PAYMENTTERM_CODE";
			const string CARRIER_CODE = "CARRIER_CODE";
			const string EMPLOYEE_CODE = "EMPLOYEE_CODE";
			const string PARTY_CODE = "PARTY_CODE";
			const string PARTY_NAME = "PARTY_NAME";
			const string VENDORLOC_CODE = "VENDORLOC_CODE";
			const string SHIPTOLOC_CODE = "SHIPTOLOC_CODE";
			const string INVTOLOC_CODE = "INVTOLOC_CODE";
			const string PARTYCONTACT_CODE = "PARTYCONTACT_CODE";
			const string PURCHASETYPE_CODE = "PURCHASETYPE_CODE";
			const string DISCOUNTTERM_CODE = "DISCOUNTTERM_CODE";
			const string PAUSE_CODE = "PAUSE_CODE";

			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CODE_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.ORDERDATE_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.VAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.TOTALVAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PRIORITY_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.COMMENT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.VENDORSO_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CCNID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.POREVISION_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.REFERENCENO_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CURRENCYID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MAKERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MAKERLOCATIONID_FLD + ","
					+ MST_CurrencyTable.TABLE_NAME + "." + MST_CurrencyTable.CODE_FLD + " AS " + CURRENCY_CODE + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + ","
					+ MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.CODE_FLD + " AS " + MASTERLOCATION_CODE + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD + ","
					+ MST_DeliveryTermTable.TABLE_NAME + "." + MST_DeliveryTermTable.CODE_FLD + " AS " + DELIVERYTERM_CODE + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD + ","
					+ MST_PaymentTermTable.TABLE_NAME + "." + MST_PaymentTermTable.CODE_FLD + " AS " + PAYMENTTERM_CODE + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CARRIERID_FLD + ","
					+ MST_CarrierTable.TABLE_NAME + "." + MST_CarrierTable.CODE_FLD + " AS " + CARRIER_CODE + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.BUYERID_FLD + ","
					+ MST_EmployeeTable.TABLE_NAME + "." + MST_EmployeeTable.CODE_FLD + " AS " + EMPLOYEE_CODE + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PARTYID_FLD + ","
					+ MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.CODE_FLD + " AS " + PARTY_CODE + ","
					+ MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.NAME_FLD + " AS " + PARTY_NAME + ","
					+ "PA." + MST_PartyTable.CODE_FLD + " AS MakerCode" +  ","
					+ "PA." + MST_PartyTable.NAME_FLD + " AS MakerName" + ","
					+ "PO_PurchaseOrderMaster.PricingTypeID, enm_PricingType.Description PricingTypeCode,"
					+ MST_PartyLocationTable.TABLE_NAME + "." + MST_PartyLocationTable.CODE_FLD + " AS MakerLocationCode"  + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.VENDORLOCID_FLD + ","
					+ "VENDORLOC." + MST_PartyLocationTable.CODE_FLD + " AS " + VENDORLOC_CODE + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ "SHIPTOLOC." + MST_MasterLocationTable.CODE_FLD + " AS " + SHIPTOLOC_CODE + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.INVTOLOCID_FLD + ","
					+ "INVTOLOC." + MST_MasterLocationTable.CODE_FLD + " AS " + INVTOLOC_CODE + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD + ","
					+ MST_PartyContactTable.TABLE_NAME + "." + MST_PartyContactTable.CODE_FLD + " AS " + PARTYCONTACT_CODE + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD + ","
					+ PO_PurchaseTypeTable.TABLE_NAME + "." + PO_PurchaseTypeTable.CODE_FLD + " AS " + PURCHASETYPE_CODE + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD + ","
					+ MST_DiscountTermTable.TABLE_NAME + "." + MST_DiscountTermTable.CODE_FLD + " AS " + DISCOUNTTERM_CODE + ","
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PAUSEID_FLD + ","
					+ MST_PauseTable.TABLE_NAME + "." + MST_PauseTable.CODE_FLD + " AS " + PAUSE_CODE
				
					+ " FROM " + PO_PurchaseOrderMasterTable.TABLE_NAME
				
					+ " LEFT JOIN " + MST_CurrencyTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CURRENCYID_FLD
					+ " = " + MST_CurrencyTable.TABLE_NAME + "." + MST_CurrencyTable.CURRENCYID_FLD
					+ " LEFT JOIN " + MST_MasterLocationTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD
					+ " = " + MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.MASTERLOCATIONID_FLD
					+ " LEFT JOIN " + MST_DeliveryTermTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD
					+ " = " + MST_DeliveryTermTable.TABLE_NAME + "." + MST_DeliveryTermTable.DELIVERYTERMID_FLD
					+ " LEFT JOIN " + MST_PaymentTermTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD
					+ " = " + MST_PaymentTermTable.TABLE_NAME + "." + MST_PaymentTermTable.PAYMENTTERMID_FLD
					+ " LEFT JOIN " + MST_CarrierTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CARRIERID_FLD
					+ " = " + MST_CarrierTable.TABLE_NAME + "." + MST_CarrierTable.CARRIERID_FLD
					+ " LEFT JOIN " + MST_EmployeeTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.BUYERID_FLD
					+ " = " + MST_EmployeeTable.TABLE_NAME + "." + MST_EmployeeTable.EMPLOYEEID_FLD
					+ " LEFT JOIN " + MST_PartyTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PARTYID_FLD
					+ " = " + MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.PARTYID_FLD
					+ " LEFT JOIN " + MST_PartyLocationTable.TABLE_NAME + " VENDORLOC "
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.VENDORLOCID_FLD
					+ " = " + "VENDORLOC" + "." + MST_PartyLocationTable.PARTYLOCATIONID_FLD
					+ " LEFT JOIN " + MST_MasterLocationTable.TABLE_NAME + " SHIPTOLOC "
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD
					+ " = " + "SHIPTOLOC" + "." + MST_MasterLocationTable.MASTERLOCATIONID_FLD
					+ " LEFT JOIN " + MST_MasterLocationTable.TABLE_NAME + " INVTOLOC "
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.INVTOLOCID_FLD
					+ " = " + "INVTOLOC" + "." + MST_MasterLocationTable.MASTERLOCATIONID_FLD
					+ " LEFT JOIN " + MST_PartyContactTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD
					+ " = " + MST_PartyContactTable.TABLE_NAME + "." + MST_PartyContactTable.PARTYCONTACTID_FLD
					+ " LEFT JOIN " + PO_PurchaseTypeTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD
					+ " = " + PO_PurchaseTypeTable.TABLE_NAME + "." + PO_PurchaseTypeTable.PURCHASETYPEID_FLD
					+ " LEFT JOIN " + MST_DiscountTermTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD
					+ " = " + MST_DiscountTermTable.TABLE_NAME + "." + MST_DiscountTermTable.DISCOUNTTERMID_FLD
					+ " LEFT JOIN " + MST_PauseTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PAUSEID_FLD
					+ " = " + MST_PauseTable.TABLE_NAME + "." + MST_PauseTable.PAUSEID_FLD
					
					+ " LEFT JOIN " + MST_PartyTable.TABLE_NAME
					+ " PA ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MAKERID_FLD
					+ " = PA" + "." + MST_PartyTable.PARTYID_FLD
					
					+ " LEFT JOIN " + MST_PartyLocationTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MAKERLOCATIONID_FLD
					+ " = " + MST_PartyLocationTable.TABLE_NAME + "." + MST_PartyLocationTable.PARTYLOCATIONID_FLD	

					+ " LEFT JOIN " + enm_PricingTypeTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PRICINGTYPEID_FLD
					+ " = " + enm_PricingTypeTable.TABLE_NAME + "." + enm_PricingTypeTable.PRICINGTYPEID_FLD	

					+ " WHERE " + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderMasterTable.TABLE_NAME);
				if (dstPCS.Tables[0].Rows.Count > 0)
				{
					return dstPCS.Tables[0].Rows[0];
				}
				else
					return null;
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
		///       ListPODetailByPOMasterID
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
		///       Tuesday, March 12, 2005
		///    </History>
		///    <Notes>
		///    Modified: Trada 29/11/2005
		///    </Notes>
		//**************************************************************************
		public DataSet ListPODetailByPOMasterID(int pintPOMasterID, int pintCCNID, bool pblnApproved)
		{
			const string METHOD_NAME = THIS + ".ListPODetailByPOMasterID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				if (pintPOMasterID >= 0)
				{
					strSql = "SELECT POM.Code as PONo,"
						+ " T1.PurchaseOrderDetailID, "
						+ " T1.PurchaseOrderMasterID, "
						+ " T1.ApproverID, "
						+ " T1.ApprovalDate, "
						+ " T1.Line, "
						+ "CA.Code as ITM_CategoryCode,"
						+ " P.Code, P.Description, P.Revision,"
						+ " UOM.Code as BuyingUM,"
						+ " T1.OrderQuantity,"
						+ " (SELECT ISNULL(ISNULL(SUM(ISNUll(OHQuantity,0)),0) - ISNULL(SUM(ISNULL(CommitQuantity,0)),0),0)"
						+ " FROM dbo.IV_MasLocCache WHERE CCNID = " + pintCCNID.ToString() + " AND ProductID = T1.ProductID) as AvailableQty,"
						+ " C.Code as Currency, "
						+ " POM." + PO_PurchaseOrderMasterTable.ORDERDATE_FLD + ","
						+ " T1.TotalAmount FROM PO_PurchaseOrderDetail T1  "
						+ " INNER JOIN dbo.PO_PurchaseOrderMaster POM ON POM.PurchaseOrderMasterID = T1.PurchaseOrderMasterID"
						+ " INNER JOIN ITM_Product P ON P.ProductID = T1.ProductID"
						+ " LEFT JOIN dbo.ITM_Category CA ON CA.CategoryID = P.CategoryID"
						+ " INNER JOIN dbo.MST_UnitOfMeasure UOM ON UOM.UnitOfMeasureID = T1.BuyingUMID"
						+ " INNER JOIN dbo.MST_Currency C ON C.CurrencyID = POM.CurrencyID"
						+ " WHERE T1." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + " = "
						+ pintPOMasterID.ToString() + " and T1.PurchaseOrderDetailID not in (Select PurchaseOrderDetailID from PO_PurchaseOrderReceiptDetail) ";
					if (!pblnApproved)
						strSql += " AND T1." + PO_PurchaseOrderDetailTable.APPROVERID_FLD + " IS NULL ORDER BY Line";
					else
						strSql += " AND T1." + PO_PurchaseOrderDetailTable.APPROVERID_FLD + " IS NOT NULL ORDER BY Line";
				}
				else
				{
					strSql = " SELECT POM.Code as PONo, "
						+ " POM." + PO_PurchaseOrderMasterTable.ORDERDATE_FLD + ","
						+ " T1.ApproverID, "
						+ " T1.ApprovalDate, "
						+ " T1.PurchaseOrderMasterID, T1.PurchaseOrderDetailID,  T1.Line, CA.Code as ITM_CategoryCode, P.Code, P.Description, P.Revision, UOM.Code as BuyingUM, T1.OrderQuantity, (SELECT ISNULL(ISNULL(SUM(ISNUll(OHQuantity,0)),0) - ISNULL(SUM(ISNULL(CommitQuantity,0)),0),0) "
						+ " FROM dbo.IV_MasLocCache "
						+ " WHERE CCNID = " + pintCCNID + " AND ProductID = T1.ProductID) as AvailableQty, C.Code as Currency,  T1.TotalAmount FROM PO_PurchaseOrderDetail T1 "
						+ " INNER JOIN dbo.PO_PurchaseOrderMaster POM ON POM.PurchaseOrderMasterID = T1.PurchaseOrderMasterID "
						+ " INNER JOIN ITM_Product P ON P.ProductID = T1.ProductID "
						+ " LEFT JOIN dbo.ITM_Category CA ON CA.CategoryID = P.CategoryID "
						+ " INNER JOIN dbo.MST_UnitOfMeasure UOM ON UOM.UnitOfMeasureID = T1.BuyingUMID "
						+ " INNER JOIN dbo.MST_Currency C ON C.CurrencyID = POM.CurrencyID ";
					if (!pblnApproved)
						strSql += " WHERE  T1.PurchaseOrderDetailID not in (Select PurchaseOrderDetailID from PO_PurchaseOrderReceiptDetail) and T1.ApproverID IS NULL Order By POM." + PO_PurchaseOrderMasterTable.CODE_FLD + ", Line ";
					else
						strSql += " WHERE  T1.PurchaseOrderDetailID not in (Select PurchaseOrderDetailID from PO_PurchaseOrderReceiptDetail) and T1.ApproverID IS NOT NULL Order By POM." + PO_PurchaseOrderMasterTable.CODE_FLD + ", Line ";
				}
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to list two fields PurchaseOrderDetailID and 
		///       (sum(unitPrice*Q1.ReceiveQuantity) as ReceiveAmount) of PODetail table 
		///       by PurchaseOrderMasterID.
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
		///       March 17, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListPODetailByMasterIDOfPOReceiptDetail(int pintPOMasterID, bool pblnApproved)
		{
			const string METHOD_NAME = THIS + ".ListPODetailByMasterIDOfPOReceiptDetail()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				if (pintPOMasterID >= 0)
				{
					strSql = "SELECT T1."
						+ PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
						+ ", " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD
						+ ", sum(" + PO_PurchaseOrderDetailTable.UNITPRICE_FLD + "*Q1." + PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + ") as "
						+ RECEIVE_AMOUNT
						+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME + " T1 ,"
						+ " (SELECT " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD + ", " + PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD
						+ " FROM " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME + " WHERE " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD + " = "
						+ pintPOMasterID.ToString() + ") as Q1 "
						+ " WHERE T1." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + " = Q1." + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD
						+ " AND T1." + PO_PurchaseOrderDetailTable.APPROVERID_FLD + " IS NULL "
						+ " GROUP BY T1." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "," + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD;
				}
				else
					strSql = " SELECT T1.PurchaseOrderMasterID, T1.PurchaseOrderDetailID, sum(UnitPrice*Q1.ReceiveQuantity) as ReceiveAmount "
						+ " FROM PO_PurchaseOrderDetail T1 , (SELECT PurchaseOrderDetailID, ReceiveQuantity FROM PO_PurchaseOrderReceiptDetail) as Q1  "
						+ " WHERE T1.PurchaseOrderDetailID = Q1.PurchaseOrderDetailID AND T1.ApproverID IS NULL  "
						+ " GROUP BY T1.PurchaseOrderMasterID, T1.PurchaseOrderDetailID ";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderMasterTable.TABLE_NAME);

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
					+ PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CODE_FLD + ","
					+ PO_PurchaseOrderMasterTable.ORDERDATE_FLD + ","
					+ PO_PurchaseOrderMasterTable.VAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.IMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.SPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CURRENCYID_FLD + ","
					+ PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD + ","
					+ PO_PurchaseOrderMasterTable.CARRIERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALIMPORTTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.BUYERID_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALVAT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALSPECIALTAX_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALDISCOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.TOTALNETAMOUNT_FLD + ","
					+ PO_PurchaseOrderMasterTable.CCNID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PARTYID_FLD + ","
					+ PO_PurchaseOrderMasterTable.VENDORLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.INVTOLOCID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD + ","
					+ PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PAUSEID_FLD + ","
					+ PO_PurchaseOrderMasterTable.PRIORITY_FLD + ","
					+ PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD + ","
					+ PO_PurchaseOrderMasterTable.COMMENT_FLD
					+ " FROM " + PO_PurchaseOrderMasterTable.TABLE_NAME;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, PO_PurchaseOrderMasterTable.TABLE_NAME);

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
		///       This method uses to check validate data
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
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, March 24, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int IsValidateData(string pstrValue, string pstrTable, string pstrField, string pstrCodition)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT COUNT(*)"
					+ " FROM " + pstrTable
					+ " WHERE " + pstrField + " LIKE '" + pstrValue.Replace("'", "''") + "%' " + pstrCodition;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				int intCount = 0;
				while (odrPCS.Read())
				{
					intCount = int.Parse(odrPCS[0].ToString());
				}

				return intCount;
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
		///       This method uses to get id from code 
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
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Thursday, March 24, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataRow GetDataRow(string pstrListFields, string pstrValue, string pstrTable, string pstrField, string pstrCodition)
		{
			const string METHOD_NAME = THIS + ".GetDataRow()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT " + pstrListFields //+ "," + pstrField
					+ " FROM " + pstrTable
					+ " WHERE " + pstrField + " LIKE '" + pstrValue.Trim().Replace("'", "''") + "%' " + pstrCodition;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable(pstrTable);
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				if (dtbData.Rows.Count > 0)
					return dtbData.Rows[0];
				else return null;
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

		public void OpenPurchaseOrderLine(int printPurchaseOrderDetailID)
		{
			const string METHOD_NAME = THIS + ".OpenPurchaseOrderLine()";
			const int CLOSED_STATUS = 0;

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
					+ PO_PurchaseOrderDetailTable.CLOSED_FLD + "=  1 "
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "=" + printPurchaseOrderDetailID;

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
		public DataSet ListMasterToDelete(DateTime pdtmFromDate, DateTime pdtmToDate, int pintPOType, string pstrVendor, string pstrItem)
		{
			const string METHOD_NAME = THIS + ".ListMasterToDelete()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "Select DISTINCT M.PurchaseOrderMasterID"
					+ " FROM PO_PurchaseOrderMaster M JOIN PO_PurchaseOrderDetail D"
					+ " ON M.PurchaseOrderMasterID = D.PurchaseOrderMasterID"
					+ " JOIN PO_DeliverySchedule S ON D.PurchaseOrderDetailID = S.PurchaseOrderDetailID"
					+ " WHERE 1=1";
				if (pdtmFromDate != DateTime.MinValue)
					strSql += " AND S.ScheduleDate >= ?";
				if (pdtmToDate != DateTime.MinValue)
					strSql += " AND S.ScheduleDate <= ?";
				if (pintPOType > 0)
					strSql += " AND M.PurchaseTypeID = " + pintPOType;
				if (pstrVendor != string.Empty)
					strSql += " AND M.PartyID IN (" + pstrVendor + ")";
				if (pstrItem != string.Empty)
					strSql += " AND D.ProductID IN (" + pstrItem + ")";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				if (pdtmFromDate != DateTime.MinValue)
                    ocmdPCS.Parameters.AddWithValue("FromDate", pdtmFromDate);
				if (pdtmToDate != DateTime.MinValue)
                    ocmdPCS.Parameters.AddWithValue("ToDate", pdtmToDate);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderMasterTable.TABLE_NAME);

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
		public void Delete(string pstrMasterID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "DELETE " + PO_PurchaseOrderMasterTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + " IN ("
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