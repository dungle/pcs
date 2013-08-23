using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComSale.Order.DS
{
	
	public class SO_SaleOrderMasterDS 
	{
		public SO_SaleOrderMasterDS()
		{
		}
		private const string THIS = "PCSComSale.Order.DS.SO_SaleOrderMasterDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_SaleOrderMaster
		///    </Description>
		///    <Inputs>
		///        SO_SaleOrderMasterVO       
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
		///       Monday, February 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				SO_SaleOrderMasterVO objObject = (SO_SaleOrderMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO SO_SaleOrderMaster("
				+ SO_SaleOrderMasterTable.CODE_FLD + ","
				+ SO_SaleOrderMasterTable.TRANSDATE_FLD + ","
				+ SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD + ","
				+ SO_SaleOrderMasterTable.VAT_FLD + ","
				+ SO_SaleOrderMasterTable.VATRATE_FLD + ","
				+ SO_SaleOrderMasterTable.EXPORTTAX_FLD + ","
				+ SO_SaleOrderMasterTable.SPECIALTAX_FLD + ","
				+ SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD + ","
				+ SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + ","
				+ SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD + ","
				+ SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD + ","
				+ SO_SaleOrderMasterTable.CCNID_FLD + ","
				+ SO_SaleOrderMasterTable.CURRENCYID_FLD + ","
				+ SO_SaleOrderMasterTable.EXCHANGERATE_FLD + ","
				+ SO_SaleOrderMasterTable.CARRIERID_FLD + ","
				+ SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD + ","
				+ SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD + ","
				+ SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD + ","
				+ SO_SaleOrderMasterTable.PAUSEID_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD + ","
				+ SO_SaleOrderMasterTable.PRIORITY_FLD + ","
				+ SO_SaleOrderMasterTable.BUYINGLOCID_FLD + ","
				+ SO_SaleOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.BILLTOLOCID_FLD + ","
				+ SO_SaleOrderMasterTable.PARTYCONTACTID_FLD + ","
				+ SO_SaleOrderMasterTable.SALESTATUSID_FLD + ","
				+ SO_SaleOrderMasterTable.CANCELREASONID_FLD + ","
				+ SO_SaleOrderMasterTable.SALETYPEID_FLD + ","
				+ SO_SaleOrderMasterTable.PARTYID_FLD + ","
				+ SO_SaleOrderMasterTable.LOCATIONID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TRANSDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TRANSDATE_FLD].Value = objObject.TransDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD].Value = objObject.CustomerPurchaseOrderNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.VAT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.VATRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.VATRATE_FLD].Value = objObject.VATRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.EXPORTTAX_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.EXPORTTAX_FLD].Value = objObject.ExportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SPECIALTAX_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD].Value = objObject.ExportTaxRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD].Value = objObject.ShipCompleted;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD].Value = objObject.SalesRepresentativeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD].Value = objObject.SpecialTaxRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.EXCHANGERATE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CARRIERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.CARRIERID_FLD].Value = objObject.CarrierID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD].Value = objObject.PaymentTermsID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD].Value = objObject.DeliveryTermsID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD].Value = objObject.DiscountTermsID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PAUSEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAUSEID_FLD].Value = objObject.PauseID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD].Value = objObject.TotalVATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD].Value = objObject.TotalExportAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD].Value = objObject.TotalSpecialTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD].Value = objObject.TotalDiscountAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD].Value = objObject.TotalNetAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD].Value = objObject.PaymentMethodID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PRIORITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.PRIORITY_FLD].Value = objObject.Priority;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.BUYINGLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.BUYINGLOCID_FLD].Value = objObject.BuyingLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SHIPTOLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD].Value = objObject.ShipToLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD].Value = objObject.ShipToLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.BILLTOLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.BILLTOLOCID_FLD].Value = objObject.BillToLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;


				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SALESTATUSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALESTATUSID_FLD].Value = objObject.SaleStatusID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CANCELREASONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.CANCELREASONID_FLD].Value = objObject.CancelReasonID;


				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SALETYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALETYPEID_FLD].Value = objObject.SaleTypeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.LOCATIONID_FLD].Value = objObject.LocationID;


				
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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_SaleOrderMaster
		///    </Description>
		///    <Inputs>
		///        SO_SaleOrderMasterVO       
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
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				SO_SaleOrderMasterVO objObject = (SO_SaleOrderMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO SO_SaleOrderMaster("
					+ SO_SaleOrderMasterTable.CODE_FLD + ","
					+ SO_SaleOrderMasterTable.TRANSDATE_FLD + ","
					+ SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD + ","
					+ SO_SaleOrderMasterTable.VAT_FLD + ","
					+ SO_SaleOrderMasterTable.VATRATE_FLD + ","
					+ SO_SaleOrderMasterTable.EXPORTTAX_FLD + ","
					+ SO_SaleOrderMasterTable.SPECIALTAX_FLD + ","
					+ SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD + ","
					+ SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + ","
					+ SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD + ","
					+ SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD + ","
					+ SO_SaleOrderMasterTable.CCNID_FLD + ","
					+ SO_SaleOrderMasterTable.CURRENCYID_FLD + ","
					+ SO_SaleOrderMasterTable.EXCHANGERATE_FLD + ","
					+ SO_SaleOrderMasterTable.CARRIERID_FLD + ","
					+ SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD + ","
					+ SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD + ","
					+ SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD + ","
					+ SO_SaleOrderMasterTable.PAUSEID_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD + ","
					+ SO_SaleOrderMasterTable.PRIORITY_FLD + ","
					+ SO_SaleOrderMasterTable.BUYINGLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.BILLTOLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.PARTYCONTACTID_FLD + ","
					+ SO_SaleOrderMasterTable.SALESTATUSID_FLD + ","
					+ SO_SaleOrderMasterTable.CANCELREASONID_FLD + ","
					+ SO_SaleOrderMasterTable.SALETYPEID_FLD + ","
					+ SO_SaleOrderMasterTable.PARTYID_FLD + ","
					+ SO_SaleOrderMasterTable.TYPEID_FLD + ","
					+ SO_SaleOrderMasterTable.LOCATIONID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				strSql += " ; Select @@IDENTITY as NEWID";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TRANSDATE_FLD, OleDbType.Date));
				if((DateTime.MinValue < objObject.TransDate) && (objObject.TransDate < DateTime.MaxValue))
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.TRANSDATE_FLD].Value = objObject.TransDate;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.TRANSDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD].Value = objObject.CustomerPurchaseOrderNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.VAT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.VATRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.VATRATE_FLD].Value = objObject.VATRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.EXPORTTAX_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.EXPORTTAX_FLD].Value = objObject.ExportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SPECIALTAX_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD].Value = objObject.ExportTaxRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD].Value = objObject.ShipCompleted;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD, OleDbType.Integer));
				if(objObject.SalesRepresentativeID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD].Value = objObject.SalesRepresentativeID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD].Value = objObject.SpecialTaxRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CCNID_FLD, OleDbType.Integer));
				if(objObject.CCNID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.CCNID_FLD].Value = objObject.CCNID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.CCNID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				if(objObject.CurrencyID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.CURRENCYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.EXCHANGERATE_FLD, OleDbType.Integer));
				if(objObject.ExchangeRate > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.EXCHANGERATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CARRIERID_FLD, OleDbType.Integer));
				if(objObject.CarrierID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.CARRIERID_FLD].Value = objObject.CarrierID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.CARRIERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD, OleDbType.Integer));
				if(objObject.PaymentTermsID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD].Value = objObject.PaymentTermsID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD, OleDbType.Integer));
				if(objObject.DeliveryTermsID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD].Value = objObject.DeliveryTermsID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD, OleDbType.Integer));
				if(objObject.DiscountTermsID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD].Value = objObject.DiscountTermsID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PAUSEID_FLD, OleDbType.Integer));
				if(objObject.PauseID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAUSEID_FLD].Value = objObject.PauseID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAUSEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD].Value = objObject.TotalVATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD].Value = objObject.TotalExportAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD].Value = objObject.TotalSpecialTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD].Value = objObject.TotalDiscountAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD].Value = objObject.TotalNetAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD, OleDbType.Integer));
				if(objObject.PaymentMethodID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD].Value = objObject.PaymentMethodID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PRIORITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.PRIORITY_FLD].Value = objObject.Priority;


				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.BUYINGLOCID_FLD, OleDbType.Integer));
				if(objObject.BuyingLocID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.BUYINGLOCID_FLD].Value = objObject.BuyingLocID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.BUYINGLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SHIPTOLOCID_FLD, OleDbType.Integer));
				if(objObject.ShipToLocID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD].Value = objObject.ShipToLocID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD, OleDbType.Integer));
				if(objObject.ShipFromLocID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD].Value = objObject.ShipFromLocID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.BILLTOLOCID_FLD, OleDbType.Integer));
				if(objObject.BillToLocID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.BILLTOLOCID_FLD].Value = objObject.BillToLocID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.BILLTOLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				if(objObject.PartyContactID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD].Value = DBNull.Value;


				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SALESTATUSID_FLD, OleDbType.Integer));
				if(objObject.SaleStatusID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALESTATUSID_FLD].Value = objObject.SaleStatusID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALESTATUSID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CANCELREASONID_FLD, OleDbType.Integer));
				if(objObject.CancelReasonID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.CANCELREASONID_FLD].Value = objObject.CancelReasonID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.CANCELREASONID_FLD].Value = DBNull.Value;


				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SALETYPEID_FLD, OleDbType.Integer));
				if(objObject.SaleTypeID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALETYPEID_FLD].Value = objObject.SaleTypeID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALETYPEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PARTYID_FLD, OleDbType.Integer));
				if(objObject.PartyID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PARTYID_FLD].Value = objObject.PartyID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PARTYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TYPEID_FLD, OleDbType.Integer));
				if(objObject.TypeID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.TYPEID_FLD].Value = objObject.TypeID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.TYPEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.LOCATIONID_FLD, OleDbType.Integer));
				if(objObject.LocationID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.LOCATIONID_FLD].Value = objObject.LocationID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.LOCATIONID_FLD].Value = DBNull.Value;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());
				// ocmdPCS.ExecuteNonQuery();	

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from SO_SaleOrderMaster
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
			strSql=	"DELETE " + SO_SaleOrderMasterTable.TABLE_NAME + " WHERE  " + "SaleOrderMasterID" + "=" + pintID.ToString();
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
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
		///       This method uses to get data from SO_SaleOrderMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_SaleOrderMasterVO
		///    </Outputs>
		///    <Returns>
		///       SO_SaleOrderMasterVO
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
				strSql=	"SELECT "
					+ SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_SaleOrderMasterTable.CODE_FLD + ","
					+ SO_SaleOrderMasterTable.TRANSDATE_FLD + ","
					+ SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD + ","
					+ SO_SaleOrderMasterTable.VAT_FLD + ","
					+ SO_SaleOrderMasterTable.VATRATE_FLD + ","
					+ SO_SaleOrderMasterTable.EXPORTTAX_FLD + ","
					+ SO_SaleOrderMasterTable.SPECIALTAX_FLD + ","
					+ SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD + ","
					+ SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + ","
					+ SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD + ","
					+ SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD + ","
					+ SO_SaleOrderMasterTable.CCNID_FLD + ","
					+ SO_SaleOrderMasterTable.CURRENCYID_FLD + ","
					+ SO_SaleOrderMasterTable.EXCHANGERATE_FLD + ","
					+ SO_SaleOrderMasterTable.CARRIERID_FLD + ","
					+ SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD + ","
					+ SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD + ","
					+ SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD + ","
					+ SO_SaleOrderMasterTable.PAUSEID_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD + ","
					+ SO_SaleOrderMasterTable.PRIORITY_FLD + ","
					+ SO_SaleOrderMasterTable.BUYINGLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.BILLTOLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.PARTYCONTACTID_FLD + ","
				
					+ SO_SaleOrderMasterTable.SALESTATUSID_FLD + ","
					+ SO_SaleOrderMasterTable.CANCELREASONID_FLD + ","
				
					+ SO_SaleOrderMasterTable.SALETYPEID_FLD + ","
					+ SO_SaleOrderMasterTable.PARTYID_FLD + ","
					+ SO_SaleOrderMasterTable.LOCATIONID_FLD
					+ " FROM " + SO_SaleOrderMasterTable.TABLE_NAME
					+" WHERE " + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_SaleOrderMasterVO objObject = new SO_SaleOrderMasterVO();

				while (odrPCS.Read())
				{ 
					objObject.SaleOrderMasterID = int.Parse(odrPCS[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString().Trim());
					objObject.Code = odrPCS[SO_SaleOrderMasterTable.CODE_FLD].ToString().Trim();
					if(odrPCS[SO_SaleOrderMasterTable.TRANSDATE_FLD] != DBNull.Value)
						objObject.TransDate = DateTime.Parse(odrPCS[SO_SaleOrderMasterTable.TRANSDATE_FLD].ToString().Trim());
				
					objObject.CustomerPurchaseOrderNo = odrPCS[SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD].ToString().Trim();
					if(odrPCS[SO_SaleOrderMasterTable.VAT_FLD] != DBNull.Value)
						objObject.VAT = bool.Parse(odrPCS[SO_SaleOrderMasterTable.VAT_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.VATRATE_FLD] != DBNull.Value)
						objObject.VATRate = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.VATRATE_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.EXPORTTAX_FLD] != DBNull.Value)
						objObject.ExportTax = bool.Parse(odrPCS[SO_SaleOrderMasterTable.EXPORTTAX_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.SPECIALTAX_FLD] != DBNull.Value)
						objObject.SpecialTax = bool.Parse(odrPCS[SO_SaleOrderMasterTable.SPECIALTAX_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD] != DBNull.Value)
						objObject.ExportTaxRate = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD] != DBNull.Value)
						objObject.ShipCompleted = bool.Parse(odrPCS[SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD] != DBNull.Value)
						objObject.SalesRepresentativeID = int.Parse(odrPCS[SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD] != DBNull.Value)
						objObject.SpecialTaxRate = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.CCNID_FLD] != DBNull.Value)
						objObject.CCNID = int.Parse(odrPCS[SO_SaleOrderMasterTable.CCNID_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.CURRENCYID_FLD] != DBNull.Value)
						objObject.CurrencyID = int.Parse(odrPCS[SO_SaleOrderMasterTable.CURRENCYID_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.EXCHANGERATE_FLD] != DBNull.Value)
						objObject.ExchangeRate = decimal.Parse(odrPCS[SO_SaleOrderMasterTable.EXCHANGERATE_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.CARRIERID_FLD] != DBNull.Value)
						objObject.CarrierID = int.Parse(odrPCS[SO_SaleOrderMasterTable.CARRIERID_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD] != DBNull.Value)
						objObject.PaymentTermsID = int.Parse(odrPCS[SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD] != DBNull.Value)
						objObject.DeliveryTermsID = int.Parse(odrPCS[SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD] != DBNull.Value)
						objObject.DiscountTermsID = int.Parse(odrPCS[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.PAUSEID_FLD] != DBNull.Value)
						objObject.PauseID = int.Parse(odrPCS[SO_SaleOrderMasterTable.PAUSEID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD] != DBNull.Value)
						objObject.TotalVATAmount = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD] != DBNull.Value)
						objObject.TotalExportAmount = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD] != DBNull.Value)
						objObject.TotalSpecialTaxAmount = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.TOTALAMOUNT_FLD] != DBNull.Value)
						objObject.TotalAmount = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.TOTALAMOUNT_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD] != DBNull.Value)
						objObject.TotalDiscountAmount = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD] != DBNull.Value)
						objObject.TotalNetAmount = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD] != DBNull.Value)
						objObject.PaymentMethodID = int.Parse(odrPCS[SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.PRIORITY_FLD] != DBNull.Value)
						objObject.Priority = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.PRIORITY_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.BUYINGLOCID_FLD] != DBNull.Value)
						objObject.BuyingLocID = int.Parse(odrPCS[SO_SaleOrderMasterTable.BUYINGLOCID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD] != DBNull.Value)
						objObject.ShipToLocID = int.Parse(odrPCS[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD] != DBNull.Value)
						objObject.ShipFromLocID = int.Parse(odrPCS[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.BILLTOLOCID_FLD] != DBNull.Value)
						objObject.BillToLocID = int.Parse(odrPCS[SO_SaleOrderMasterTable.BILLTOLOCID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD] != DBNull.Value)
						objObject.PartyContactID = int.Parse(odrPCS[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.SALESTATUSID_FLD] != DBNull.Value)
						objObject.SaleStatusID = int.Parse(odrPCS[SO_SaleOrderMasterTable.SALESTATUSID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.CANCELREASONID_FLD] != DBNull.Value)
						objObject.CancelReasonID = int.Parse(odrPCS[SO_SaleOrderMasterTable.CANCELREASONID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.SALETYPEID_FLD] != DBNull.Value)
						objObject.SaleTypeID = int.Parse(odrPCS[SO_SaleOrderMasterTable.SALETYPEID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.PARTYID_FLD] != DBNull.Value)
						objObject.PartyID = int.Parse(odrPCS[SO_SaleOrderMasterTable.PARTYID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.LOCATIONID_FLD] != DBNull.Value)
						objObject.LocationID = int.Parse(odrPCS[SO_SaleOrderMasterTable.LOCATIONID_FLD].ToString().Trim());

				}		
				return objObject;					
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from SO_SaleOrderMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_SaleOrderMasterVO
		///    </Outputs>
		///    <Returns>
		///       SO_SaleOrderMasterVO
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

		public object GetObjectVO(string pstrCode)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_SaleOrderMasterTable.CODE_FLD + ","
					+ SO_SaleOrderMasterTable.TRANSDATE_FLD + ","
					+ SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD + ","
					+ SO_SaleOrderMasterTable.VAT_FLD + ","
					+ SO_SaleOrderMasterTable.VATRATE_FLD + ","
					+ SO_SaleOrderMasterTable.EXPORTTAX_FLD + ","
					+ SO_SaleOrderMasterTable.SPECIALTAX_FLD + ","
					+ SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD + ","
					+ SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + ","
					+ SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD + ","
					+ SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD + ","
					+ SO_SaleOrderMasterTable.CCNID_FLD + ","
					+ SO_SaleOrderMasterTable.CURRENCYID_FLD + ","
					+ SO_SaleOrderMasterTable.EXCHANGERATE_FLD + ","
					+ SO_SaleOrderMasterTable.CARRIERID_FLD + ","
					+ SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD + ","
					+ SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD + ","
					+ SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD + ","
					+ SO_SaleOrderMasterTable.PAUSEID_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD + ","
					+ SO_SaleOrderMasterTable.PRIORITY_FLD + ","
					+ SO_SaleOrderMasterTable.BUYINGLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.BILLTOLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.PARTYCONTACTID_FLD + ","
					
					+ SO_SaleOrderMasterTable.SALESTATUSID_FLD + ","
					+ SO_SaleOrderMasterTable.CANCELREASONID_FLD + ","
				
					+ SO_SaleOrderMasterTable.SALETYPEID_FLD + ","
					+ SO_SaleOrderMasterTable.PARTYID_FLD + ","
					+ SO_SaleOrderMasterTable.LOCATIONID_FLD
					+ " FROM " + SO_SaleOrderMasterTable.TABLE_NAME
					+" WHERE " + SO_SaleOrderMasterTable.CODE_FLD + "='" + pstrCode.Replace("'","''") + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_SaleOrderMasterVO objObject = new SO_SaleOrderMasterVO();

				while (odrPCS.Read())
				{ 
					objObject.SaleOrderMasterID = int.Parse(odrPCS[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString().Trim());
					objObject.Code = odrPCS[SO_SaleOrderMasterTable.CODE_FLD].ToString().Trim();
					if(odrPCS[SO_SaleOrderMasterTable.TRANSDATE_FLD] != DBNull.Value)
						objObject.TransDate = DateTime.Parse(odrPCS[SO_SaleOrderMasterTable.TRANSDATE_FLD].ToString().Trim());
				
					objObject.CustomerPurchaseOrderNo = odrPCS[SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD].ToString().Trim();
					if(odrPCS[SO_SaleOrderMasterTable.VAT_FLD] != DBNull.Value)
						objObject.VAT = bool.Parse(odrPCS[SO_SaleOrderMasterTable.VAT_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.VATRATE_FLD] != DBNull.Value)
						objObject.VATRate = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.VATRATE_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.EXPORTTAX_FLD] != DBNull.Value)
						objObject.ExportTax = bool.Parse(odrPCS[SO_SaleOrderMasterTable.EXPORTTAX_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.SPECIALTAX_FLD] != DBNull.Value)
						objObject.SpecialTax = bool.Parse(odrPCS[SO_SaleOrderMasterTable.SPECIALTAX_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD] != DBNull.Value)
						objObject.ExportTaxRate = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD] != DBNull.Value)
						objObject.ShipCompleted = bool.Parse(odrPCS[SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD] != DBNull.Value)
						objObject.SalesRepresentativeID = int.Parse(odrPCS[SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD] != DBNull.Value)
						objObject.SpecialTaxRate = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.CCNID_FLD] != DBNull.Value)
						objObject.CCNID = int.Parse(odrPCS[SO_SaleOrderMasterTable.CCNID_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.CURRENCYID_FLD] != DBNull.Value)
						objObject.CurrencyID = int.Parse(odrPCS[SO_SaleOrderMasterTable.CURRENCYID_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.EXCHANGERATE_FLD] != DBNull.Value)
						objObject.ExchangeRate = decimal.Parse(odrPCS[SO_SaleOrderMasterTable.EXCHANGERATE_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.CARRIERID_FLD] != DBNull.Value)
						objObject.CarrierID = int.Parse(odrPCS[SO_SaleOrderMasterTable.CARRIERID_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD] != DBNull.Value)
						objObject.PaymentTermsID = int.Parse(odrPCS[SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD].ToString().Trim());
					if(odrPCS[SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD] != DBNull.Value)
						objObject.DeliveryTermsID = int.Parse(odrPCS[SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD] != DBNull.Value)
						objObject.DiscountTermsID = int.Parse(odrPCS[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.PAUSEID_FLD] != DBNull.Value)
						objObject.PauseID = int.Parse(odrPCS[SO_SaleOrderMasterTable.PAUSEID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD] != DBNull.Value)
						objObject.TotalVATAmount = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD] != DBNull.Value)
						objObject.TotalExportAmount = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD] != DBNull.Value)
						objObject.TotalSpecialTaxAmount = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.TOTALAMOUNT_FLD] != DBNull.Value)
						objObject.TotalAmount = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.TOTALAMOUNT_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD] != DBNull.Value)
						objObject.TotalDiscountAmount = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD] != DBNull.Value)
						objObject.TotalNetAmount = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD] != DBNull.Value)
						objObject.PaymentMethodID = int.Parse(odrPCS[SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.PRIORITY_FLD] != DBNull.Value)
						objObject.Priority = Decimal.Parse(odrPCS[SO_SaleOrderMasterTable.PRIORITY_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.BUYINGLOCID_FLD] != DBNull.Value)
						objObject.BuyingLocID = int.Parse(odrPCS[SO_SaleOrderMasterTable.BUYINGLOCID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD] != DBNull.Value)
						objObject.ShipToLocID = int.Parse(odrPCS[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD] != DBNull.Value)
						objObject.ShipFromLocID = int.Parse(odrPCS[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.BILLTOLOCID_FLD] != DBNull.Value)
						objObject.BillToLocID = int.Parse(odrPCS[SO_SaleOrderMasterTable.BILLTOLOCID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD] != DBNull.Value)
						objObject.PartyContactID = int.Parse(odrPCS[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.SALESTATUSID_FLD] != DBNull.Value)
						objObject.SaleStatusID = int.Parse(odrPCS[SO_SaleOrderMasterTable.SALESTATUSID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.CANCELREASONID_FLD] != DBNull.Value)
						objObject.CancelReasonID = int.Parse(odrPCS[SO_SaleOrderMasterTable.CANCELREASONID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.SALETYPEID_FLD] != DBNull.Value)
						objObject.SaleTypeID = int.Parse(odrPCS[SO_SaleOrderMasterTable.SALETYPEID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.PARTYID_FLD] != DBNull.Value)
						objObject.PartyID = int.Parse(odrPCS[SO_SaleOrderMasterTable.PARTYID_FLD].ToString().Trim());
					if (odrPCS[SO_SaleOrderMasterTable.LOCATIONID_FLD] != DBNull.Value)
						objObject.LocationID = int.Parse(odrPCS[SO_SaleOrderMasterTable.LOCATIONID_FLD].ToString().Trim());

				}		
				return objObject;					
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to SO_SaleOrderMaster
		///    </Description>
		///    <Inputs>
		///       SO_SaleOrderMasterVO       
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

			SO_SaleOrderMasterVO objObject = (SO_SaleOrderMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE SO_SaleOrderMaster SET "
					+ SO_SaleOrderMasterTable.CODE_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.TRANSDATE_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.VAT_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.VATRATE_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.EXPORTTAX_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.SPECIALTAX_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.CCNID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.CURRENCYID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.EXCHANGERATE_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.CARRIERID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.PAUSEID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.TOTALAMOUNT_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.PRIORITY_FLD + "=   ?" + ","
				
					+ SO_SaleOrderMasterTable.BUYINGLOCID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.SHIPTOLOCID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.BILLTOLOCID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.PARTYCONTACTID_FLD + "=   ?" + ","
				
					+ SO_SaleOrderMasterTable.SALESTATUSID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.CANCELREASONID_FLD + "=   ?" + ","
					
					+ SO_SaleOrderMasterTable.SALETYPEID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.PARTYID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.TYPEID_FLD + "=   ?" + ","
					+ SO_SaleOrderMasterTable.LOCATIONID_FLD + "=  ?"
					+" WHERE " + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TRANSDATE_FLD, OleDbType.Date));
				if((DateTime.MinValue < objObject.TransDate) && (objObject.TransDate < DateTime.MaxValue))
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.TRANSDATE_FLD].Value = objObject.TransDate;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.TRANSDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD].Value = objObject.CustomerPurchaseOrderNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.VAT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.VATRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.VATRATE_FLD].Value = objObject.VATRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.EXPORTTAX_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.EXPORTTAX_FLD].Value = objObject.ExportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SPECIALTAX_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD].Value = objObject.ExportTaxRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD].Value = objObject.ShipCompleted;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD, OleDbType.Integer));
				if(objObject.SalesRepresentativeID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD].Value = objObject.SalesRepresentativeID;
				else
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD].Value = objObject.SpecialTaxRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CCNID_FLD, OleDbType.Integer));
				if(objObject.CCNID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.CCNID_FLD].Value = objObject.CCNID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.CCNID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				if(objObject.CurrencyID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.CURRENCYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.EXCHANGERATE_FLD, OleDbType.Integer));
				if(objObject.ExchangeRate > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.EXCHANGERATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CARRIERID_FLD, OleDbType.Integer));
				if(objObject.CarrierID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.CARRIERID_FLD].Value = objObject.CarrierID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.CARRIERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD, OleDbType.Integer));
				if(objObject.PaymentTermsID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD].Value = objObject.PaymentTermsID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD, OleDbType.Integer));
				if(objObject.DeliveryTermsID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD].Value = objObject.DeliveryTermsID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD, OleDbType.Integer));
				if(objObject.DiscountTermsID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD].Value = objObject.DiscountTermsID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PAUSEID_FLD, OleDbType.Integer));
				if(objObject.PauseID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAUSEID_FLD].Value = objObject.PauseID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAUSEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD].Value = objObject.TotalVATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD].Value = objObject.TotalExportAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD].Value = objObject.TotalSpecialTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD].Value = objObject.TotalDiscountAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD].Value = objObject.TotalNetAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD, OleDbType.Integer));
				if(objObject.PaymentMethodID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD].Value = objObject.PaymentMethodID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PRIORITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.PRIORITY_FLD].Value = objObject.Priority;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.BUYINGLOCID_FLD, OleDbType.Integer));
				if(objObject.BuyingLocID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.BUYINGLOCID_FLD].Value = objObject.BuyingLocID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.BUYINGLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SHIPTOLOCID_FLD, OleDbType.Integer));
				if(objObject.ShipToLocID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD].Value = objObject.ShipToLocID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD, OleDbType.Integer));
				if(objObject.ShipFromLocID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD].Value = objObject.ShipFromLocID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.BILLTOLOCID_FLD, OleDbType.Integer));
				if(objObject.BillToLocID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.BILLTOLOCID_FLD].Value = objObject.BillToLocID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.BILLTOLOCID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				if(objObject.PartyContactID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD].Value = DBNull.Value;


				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SALESTATUSID_FLD, OleDbType.Integer));
				if(objObject.SaleStatusID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALESTATUSID_FLD].Value = objObject.SaleStatusID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALESTATUSID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CANCELREASONID_FLD, OleDbType.Integer));
				if(objObject.CancelReasonID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.CANCELREASONID_FLD].Value = objObject.CancelReasonID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.CANCELREASONID_FLD].Value = DBNull.Value;


				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SALETYPEID_FLD, OleDbType.Integer));
				if(objObject.SaleTypeID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALETYPEID_FLD].Value = objObject.SaleTypeID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALETYPEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.PARTYID_FLD, OleDbType.Integer));
				if(objObject.PartyID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.PARTYID_FLD].Value = objObject.PartyID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.PARTYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.TYPEID_FLD, OleDbType.Integer));
				if(objObject.TypeID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.TYPEID_FLD].Value = objObject.TypeID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.TYPEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.LOCATIONID_FLD, OleDbType.Integer));
				if(objObject.LocationID > 0)
					ocmdPCS.Parameters[SO_SaleOrderMasterTable.LOCATIONID_FLD].Value = objObject.LocationID;
				else ocmdPCS.Parameters[SO_SaleOrderMasterTable.LOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;


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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_SaleOrderMaster
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
		///       Monday, February 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet List()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
				+ SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + ","
				+ SO_SaleOrderMasterTable.CODE_FLD + ","
				+ SO_SaleOrderMasterTable.TRANSDATE_FLD + ","
				+ SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD + ","
				+ SO_SaleOrderMasterTable.VAT_FLD + ","
				+ SO_SaleOrderMasterTable.VATRATE_FLD + ","
				+ SO_SaleOrderMasterTable.EXPORTTAX_FLD + ","
				+ SO_SaleOrderMasterTable.SPECIALTAX_FLD + ","
				+ SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD + ","
				+ SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + ","
				+ SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD + ","
				+ SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD + ","
				+ SO_SaleOrderMasterTable.CCNID_FLD + ","
				+ SO_SaleOrderMasterTable.CURRENCYID_FLD + ","
				+ SO_SaleOrderMasterTable.EXCHANGERATE_FLD + ","
				+ SO_SaleOrderMasterTable.CARRIERID_FLD + ","
				+ SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD + ","
				+ SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD + ","
				+ SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD + ","
				+ SO_SaleOrderMasterTable.PAUSEID_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD + ","
				+ SO_SaleOrderMasterTable.PRIORITY_FLD + ","
			
				+ SO_SaleOrderMasterTable.BUYINGLOCID_FLD + ","
				+ SO_SaleOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.BILLTOLOCID_FLD + ","
				+ SO_SaleOrderMasterTable.PARTYCONTACTID_FLD + ","
				
				+ SO_SaleOrderMasterTable.SALESTATUSID_FLD + ","
				+ SO_SaleOrderMasterTable.CANCELREASONID_FLD + ","
		
				+ SO_SaleOrderMasterTable.SALETYPEID_FLD + ","
				+ SO_SaleOrderMasterTable.PARTYID_FLD + ","
				+ SO_SaleOrderMasterTable.LOCATIONID_FLD
					+ " FROM " + SO_SaleOrderMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_SaleOrderMasterTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_SaleOrderMaster by developer's codition
		///       condition contions MasterLocationID and CustomerID
		///    </Description>
		///    <Inputs>
		///       pstrQueryContion : contains conditions          
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, February 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet List(string[] pstrParams)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
			string strWhere = string.Empty;
			if ((pstrParams[0] != string.Empty)&&(pstrParams[1] == "0"))
			{
				strWhere = " FROM SO_SALEORDERMASTER A INNER JOIN MST_LOCATION B on A.LocationID = B.LocationID "
					+ " INNER JOIN MST_MASTERLOCATION C on B.MasterLocationID = C.MasterLocationID "
					+ " WHERE B.MasterLocationID = " + pstrParams[0].Trim();
			}
			if ((pstrParams[0] == string.Empty)&&(pstrParams[1] != "0"))
			{
				strWhere = " FROM SO_SALEORDERMASTER A INNER JOIN MST_LOCATION B on A.LocationID = B.LocationID "
					+ " INNER JOIN MST_MASTERLOCATION C on B.MasterLocationID = C.MasterLocationID "
					+ " WHERE B.MasterLocationID = " + pstrParams[0].Trim();
			}
			if ((pstrParams[0] != string.Empty)&&(pstrParams[1] != "0"))
			{
				strWhere = " FROM SO_SALEORDERMASTER A INNER JOIN MST_LOCATION B on A.LocationID = B.LocationID "
					+ " INNER JOIN MST_MASTERLOCATION C on B.MasterLocationID = C.MasterLocationID "
					+ " WHERE B.MasterLocationID = " + pstrParams[0].Trim() + " and A.PartyID = " + pstrParams[1].Trim();
			}

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + ","
					+ "A." + SO_SaleOrderMasterTable.CODE_FLD + ","
					////				+ SO_SaleOrderMasterTable.TRANSDATE_FLD + ","
					//					+ SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD + ","
					//					+ SO_SaleOrderMasterTable.VAT_FLD + ","
					//					+ SO_SaleOrderMasterTable.VATRATE_FLD + ","
					//					+ SO_SaleOrderMasterTable.EXPORTTAX_FLD + ","
					//					+ SO_SaleOrderMasterTable.SPECIALTAX_FLD + ","
					//					+ SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD + ","
					//					+ SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + ","
					//					+ SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD + ","
					//					+ SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD + ","
					+ "A." + SO_SaleOrderMasterTable.CCNID_FLD + ""
					//					+ SO_SaleOrderMasterTable.CURRENCYID_FLD + ","
					//					+ SO_SaleOrderMasterTable.EXCHANGERATE_FLD + ","
					//					+ SO_SaleOrderMasterTable.CARRIERID_FLD + ","
					//					+ SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD + ","
					//					+ SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD + ","
					//					+ SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD + ","
					//					+ SO_SaleOrderMasterTable.PAUSEID_FLD + ","
					//					+ SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD + ","
					//					+ SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD + ","
					//					+ SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD + ","
					//					+ SO_SaleOrderMasterTable.TOTALAMOUNT_FLD + ","
					//					+ SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD + ","
					//					+ SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD + ","
					//					+ SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD + ","
					//					+ SO_SaleOrderMasterTable.PRIORITY_FLD + ","
					//					+ SO_SaleOrderMasterTable.SHIPPEDDATETIME_FLD + ","
					//					+ SO_SaleOrderMasterTable.BUYINGLOCID_FLD + ","
					//					+ SO_SaleOrderMasterTable.SHIPTOLOCID_FLD + ","
					//					+ SO_SaleOrderMasterTable.BILLTOLOCID_FLD + ","
					//					+ SO_SaleOrderMasterTable.PARTYCONTACTID_FLD + ","
					//					+ SO_SaleOrderMasterTable.PACKEDDATETIME_FLD + ","
					//					+ SO_SaleOrderMasterTable.SALESTATUSID_FLD + ","
					//					+ SO_SaleOrderMasterTable.CANCELREASONID_FLD + ","
					//					+ SO_SaleOrderMasterTable.SHIPPEDEMPLOYEEID_FLD + ","
					//					+ SO_SaleOrderMasterTable.SALETYPEID_FLD + ","
					//					+ SO_SaleOrderMasterTable.PARTYID_FLD + ","
					//					+ SO_SaleOrderMasterTable.LOCATIONID_FLD
					+ strWhere;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_SaleOrderMasterTable.TABLE_NAME);

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
		///       Monday, February 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
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
				+ SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + ","
				+ SO_SaleOrderMasterTable.CODE_FLD + ","
				+ SO_SaleOrderMasterTable.TRANSDATE_FLD + ","
				+ SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD + ","
				+ SO_SaleOrderMasterTable.VAT_FLD + ","
				+ SO_SaleOrderMasterTable.VATRATE_FLD + ","
				+ SO_SaleOrderMasterTable.EXPORTTAX_FLD + ","
				+ SO_SaleOrderMasterTable.SPECIALTAX_FLD + ","
				+ SO_SaleOrderMasterTable.EXPORTTAXRATE_FLD + ","
				+ SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + ","
				+ SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD + ","
				+ SO_SaleOrderMasterTable.SPECIALTAXRATE_FLD + ","
				+ SO_SaleOrderMasterTable.CCNID_FLD + ","
				+ SO_SaleOrderMasterTable.CURRENCYID_FLD + ","
				+ SO_SaleOrderMasterTable.EXCHANGERATE_FLD + ","
				+ SO_SaleOrderMasterTable.CARRIERID_FLD + ","
				+ SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD + ","
				+ SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD + ","
				+ SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD + ","
				+ SO_SaleOrderMasterTable.PAUSEID_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD + ","
				+ SO_SaleOrderMasterTable.PAYMENTMETHODID_FLD + ","
				+ SO_SaleOrderMasterTable.PRIORITY_FLD + ","
				
				+ SO_SaleOrderMasterTable.BUYINGLOCID_FLD + ","
				+ SO_SaleOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + ","
				+ SO_SaleOrderMasterTable.BILLTOLOCID_FLD + ","
				+ SO_SaleOrderMasterTable.PARTYCONTACTID_FLD + ","
			
				+ SO_SaleOrderMasterTable.SALESTATUSID_FLD + ","
				+ SO_SaleOrderMasterTable.CANCELREASONID_FLD + ","
			
				+ SO_SaleOrderMasterTable.SALETYPEID_FLD + ","
				+ SO_SaleOrderMasterTable.PARTYID_FLD + ","
				+ SO_SaleOrderMasterTable.LOCATIONID_FLD 
				+ "  FROM " + SO_SaleOrderMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,SO_SaleOrderMasterTable.TABLE_NAME);
			}
			catch(OleDbException ex)
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
		//**************************************************************************              
		///    <Description>
		///       This method uses to search all sale oder that not ship completed
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
		///       Monday, February 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetSaleOrderNotShipCompleted()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_SaleOrderMasterTable.CODE_FLD + ","
					+ SO_SaleOrderMasterTable.TRANSDATE_FLD
					+ " FROM " + SO_SaleOrderMasterTable.TABLE_NAME
					+ " WHERE " + SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + "= 0";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_SaleOrderMasterTable.TABLE_NAME);

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
		/// <summary>
		/// GetDupleSODelivery
		/// </summary>
		/// <param name="pdtmDate"></param>
		/// <param name="pintPartyID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
		public DataSet GetDupleSODelivery(DateTime pdtmDate, int pintPartyID)
		{
			const string METHOD_NAME = THIS + ".GetDupleSODelivery()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			try 
			{
				//SO_DeliveryScheduleVO objObject = new SO_DeliveryScheduleVO();
				string strSql = String.Empty;
				
				strSql=	"SELECT '' Line, "
					+ "SOM." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + ","
					+ "SOM." + SO_SaleOrderMasterTable.CODE_FLD + Constants.WHITE_SPACE + SO_SaleOrderMasterTable.TABLE_NAME + SO_SaleOrderMasterTable.CODE_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
					+ "D." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ","
					+ "D." + SO_DeliveryScheduleTable.LINE_FLD + Constants.WHITE_SPACE + SO_DeliveryScheduleTable.TABLE_NAME + SO_DeliveryScheduleTable.LINE_FLD + ","
					+ "P." + ITM_ProductTable.PRODUCTID_FLD + ","
					+ "P." + ITM_ProductTable.CODE_FLD + ","
					+ "P." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ "P." + ITM_ProductTable.REVISION_FLD + ","
					+ "D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
					+ "D." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD
					+ " FROM " + SO_SaleOrderMasterTable.TABLE_NAME + " SOM "
					+ " INNER JOIN " + SO_SaleOrderDetailTable.TABLE_NAME + " SOD "
					+ " ON " + " SOM." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + " = " + "SOD." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD
					+ " INNER JOIN " + SO_DeliveryScheduleTable.TABLE_NAME + " D "
					+ " ON " + "D." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + " = " + "SOD." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME + " P "
					+ " ON " + "P." + ITM_ProductTable.PRODUCTID_FLD + " = " + "SOD." + SO_SaleOrderDetailTable.PRODUCTID_FLD
					+ " WHERE SOM." + SO_SaleOrderMasterTable.PARTYID_FLD + " = " + pintPartyID.ToString()
					+ " AND D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " >= ? " 
					+ " AND D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " <= ? " ;
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.SCHEDULEDATE_FLD, OleDbType.DBDate));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Value = pdtmDate;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(SO_DeliveryScheduleTable.TABLE_NAME + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD, OleDbType.DBDate));
				ocmdPCS.Parameters[SO_DeliveryScheduleTable.TABLE_NAME + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Value = pdtmDate.AddMonths(1);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_SaleOrderMasterTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_PackListMaster
		///    </Description>
		///    <Inputs>
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
		public DataSet ListAllSaleOrderToShipment(int pintMasterLocationID)
		{
			string strCondition = " C." + MST_LocationTable.MASTERLOCATIONID_FLD + " = " + pintMasterLocationID.ToString();
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"Select Distinct '' as " + SO_PackListMasterTable.PACKLISTNO_FLD 
					+ ", A." + SO_SaleOrderMasterTable.CODE_FLD
					+ ", B." + MST_CarrierTable.NAME_FLD
					+ ", A." + MST_CarrierTable.CARRIERID_FLD
					+ " From " + SO_SaleOrderMasterTable.TABLE_NAME + " A "
					+ " inner join " + MST_CarrierTable.TABLE_NAME + " B on A." + MST_CarrierTable.CARRIERID_FLD + " = B." + MST_CarrierTable.CARRIERID_FLD
					+ " inner join " + MST_LocationTable.TABLE_NAME+ " C on A." + SO_SaleOrderMasterTable.LOCATIONID_FLD + " = C." + MST_LocationTable.LOCATIONID_FLD
					+ " Where " + strCondition + " and (Select Count(*) "
					+ " From SO_SaleOrderMaster B inner join SO_CommitInventoryMaster C on B.SaleOrderMasterID = C.SaleOrderMasterID"
                    + " inner join SO_CommitInventoryDetail D on C.CommitInventoryMasterID = D.CommitInventoryMasterID "
					+ " Where (D.Shipped = 0 or D.Shipped is null) and A.SaleOrderMasterID = B.SaleOrderMasterID) > 0";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_PackListDetailTable.TABLE_NAME);

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
		public int IsValidateData(string pstrValue,string pstrTable,string pstrField,string pstrCodition)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT COUNT(*)"
					+ " FROM " + pstrTable
					+ " WHERE " + pstrField + " LIKE '" + pstrValue.Replace("'","''") + "%' " + pstrCodition;

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
		public DataRow GetDataRow(string pstrListFields,string pstrValue,string pstrTable,string pstrField,string pstrCodition)
		{
			const string METHOD_NAME = THIS + ".GetDataRow()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT " + pstrListFields //+ "," + pstrField
					+ " FROM " + pstrTable
					+ " WHERE " + pstrField + " LIKE '" + pstrValue.Trim().Replace("'","''") + "%' " + pstrCodition;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable(pstrTable);
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				if(dtbData.Rows.Count > 0)
					return dtbData.Rows[0];
				else return null;
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
		///       Sonht
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
			const string DELIVERYTERM_CODE = "DELIVERYTERM_CODE";
			const string PAYMENTTERM_CODE = "PAYMENTTERM_CODE";
			const string CARRIER_CODE = "CARRIER_CODE";
			const string EMPLOYEE_CODE = "EMPLOYEE_CODE";
			const string PARTY_CODE = "PARTY_CODE";
			const string PARTY_NAME = "PARTY_NAME";
			const string SHIPTOLOC_CODE = "SHIPTOLOC_CODE";
			const string BILLTOLOC_CODE = "BILLTOLOC_CODE";
			const string PARTYCONTACT_CODE = "PARTYCONTACT_CODE";
			const string DISCOUNTTERM_CODE = "DISCOUNTTERM_CODE";
			const string PAUSE_CODE = "PAUSE_CODE";

			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CODE_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TRANSDATE_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.EXCHANGERATE_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.VAT_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.EXPORTTAX_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TOTALEXPORTAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SPECIALTAX_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TOTALVATAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TOTALSPECIALTAXAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TOTALAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TOTALDISCOUNTAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TOTALNETAMOUNT_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.PRIORITY_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD + ","
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CCNID_FLD + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CURRENCYID_FLD + ","
					+ MST_CurrencyTable.TABLE_NAME + "." + MST_CurrencyTable.CODE_FLD + " AS " + CURRENCY_CODE + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD + ","
					+ MST_DeliveryTermTable.TABLE_NAME + "." + MST_DeliveryTermTable.CODE_FLD + " AS " + DELIVERYTERM_CODE + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD + ","
					+ MST_PaymentTermTable.TABLE_NAME + "." + MST_PaymentTermTable.CODE_FLD + " AS " + PAYMENTTERM_CODE + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CARRIERID_FLD + ","
					+ MST_CarrierTable.TABLE_NAME + "." + MST_CarrierTable.CODE_FLD + " AS " + CARRIER_CODE + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD + ","
					+ MST_EmployeeTable.TABLE_NAME + "." + MST_EmployeeTable.CODE_FLD + " AS " + EMPLOYEE_CODE + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.PARTYID_FLD + ","
					+ MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.CODE_FLD + " AS " + PARTY_CODE + ","
					+ MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.NAME_FLD + " AS " + PARTY_NAME + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.BUYINGLOCID_FLD + ","
					+ "BuyingLoc.CODE AS BuyingLoc_Code,"

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SHIPTOLOCID_FLD + ","
					+ "SHIPTOLOC.CODE" + " AS " + SHIPTOLOC_CODE + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.BILLTOLOCID_FLD + ","
					+ "BILLTOLOC.CODE AS " + BILLTOLOC_CODE + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.PARTYCONTACTID_FLD + ","
					+ MST_PartyContactTable.TABLE_NAME + "." + MST_PartyContactTable.CODE_FLD + " AS " + PARTYCONTACT_CODE + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALETYPEID_FLD + ","
					+ SO_SaleTypeTable.TABLE_NAME + "." + SO_SaleTypeTable.CODE_FLD + " AS SALETYPE_CODE" + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD + ","
					+ MST_DiscountTermTable.TABLE_NAME + "." + MST_DiscountTermTable.CODE_FLD + " AS " + DISCOUNTTERM_CODE + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.PAUSEID_FLD + ","
					+ MST_PauseTable.TABLE_NAME + "." + MST_PauseTable.CODE_FLD + " AS " + PAUSE_CODE + ","

					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + ","
					+ " ShipFromLoc.CODE ShipFromLoc_Code, SO_SaleOrderMaster.TypeID, SO_Type.Description GateType"

					+ " FROM " + SO_SaleOrderMasterTable.TABLE_NAME

					+ " LEFT JOIN " + MST_CurrencyTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CURRENCYID_FLD
					+ " = " + MST_CurrencyTable.TABLE_NAME + "." + MST_CurrencyTable.CURRENCYID_FLD

					+ " LEFT JOIN " + MST_PartyTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.PARTYID_FLD
					+ " = " + MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.PARTYID_FLD

					+ " LEFT JOIN " + MST_PartyLocationTable.TABLE_NAME + " BuyingLoc "
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.BUYINGLOCID_FLD
					+ " = " + " BuyingLoc." + MST_PartyLocationTable.PARTYLOCATIONID_FLD

					+ " LEFT JOIN " + MST_PartyContactTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.PARTYCONTACTID_FLD
					+ " = " + MST_PartyContactTable.TABLE_NAME + "." + MST_PartyContactTable.PARTYCONTACTID_FLD

					+ " LEFT JOIN " + MST_PartyLocationTable.TABLE_NAME + " ShipToLoc "
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SHIPTOLOCID_FLD
					+ " = " + " ShipToLoc" + "." + MST_PartyLocationTable.PARTYLOCATIONID_FLD

					+ " LEFT JOIN " + MST_PartyLocationTable.TABLE_NAME + " BillToLoc "
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.BILLTOLOCID_FLD
					+ " = " + " BillToLoc" + "." + MST_PartyLocationTable.PARTYLOCATIONID_FLD

					+ " LEFT JOIN " + MST_MasterLocationTable.TABLE_NAME + " ShipFromLoc "
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD
					+ " = " + " ShipFromLoc." + MST_MasterLocationTable.MASTERLOCATIONID_FLD

					+ " LEFT JOIN " + MST_EmployeeTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD
					+ " = " + MST_EmployeeTable.TABLE_NAME + "." + MST_EmployeeTable.EMPLOYEEID_FLD

					+ " LEFT JOIN " + SO_SaleTypeTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALETYPEID_FLD
					+ " = " + SO_SaleTypeTable.TABLE_NAME + "." + SO_SaleTypeTable.SALETYPEID_FLD

					+ " LEFT JOIN " + MST_DiscountTermTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD
					+ " = " + MST_DiscountTermTable.TABLE_NAME + "." + MST_DiscountTermTable.DISCOUNTTERMID_FLD

					+ " LEFT JOIN " + MST_DeliveryTermTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD
					+ " = " + MST_DeliveryTermTable.TABLE_NAME + "." + MST_DeliveryTermTable.DELIVERYTERMID_FLD

					+ " LEFT JOIN " + MST_PaymentTermTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD
					+ " = " + MST_PaymentTermTable.TABLE_NAME + "." + MST_PaymentTermTable.PAYMENTTERMID_FLD

					+ " LEFT JOIN " + MST_CarrierTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CARRIERID_FLD
					+ " = " + MST_CarrierTable.TABLE_NAME + "." + MST_CarrierTable.CARRIERID_FLD

					+ " LEFT JOIN " + MST_PauseTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.PAUSEID_FLD
					+ " = " + MST_PauseTable.TABLE_NAME + "." + MST_PauseTable.PAUSEID_FLD

					+ " LEFT JOIN " + SO_TypeTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TYPEID_FLD
					+ " = " + SO_TypeTable.TABLE_NAME + "." + SO_TypeTable.TYPEID_FLD

					+ " WHERE " + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SO_SaleOrderMasterTable.TABLE_NAME);

				return dstPCS.Tables[0].Rows[0];
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to SO_SaleOrderMaster
		///    </Description>
		///    <Inputs>
		///       SO_SaleOrderMasterVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       TuanDM
		///    </Authors>
		///    <History>
		///       07-Jul-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
	
		public void UpdateShipComplete(string pstrSaleOrderCode)
		{
			const string METHOD_NAME = THIS + ".UpdateShipComplete()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE SO_SaleOrderMaster SET "
					+ SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + "=   1"
					+" WHERE " + SO_SaleOrderMasterTable.CODE_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.CODE_FLD].Value = pstrSaleOrderCode;

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to SO_SaleOrderMaster
		///    </Description>
		///    <Inputs>
		///       SO_SaleOrderMasterVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DuongNA
		///    </Authors>
		///    <History>
		///       28-Oct-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetShippedQuantity(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetShippedQuantity()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			object objScalar = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"SELECT SUM(IsNull(ShipQuantity,0)) FROM SO_SaleOrderDetail WHERE SaleOrderMasterID = ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CODE_FLD, pintMasterID));

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				objScalar = ocmdPCS.ExecuteScalar();
				return decimal.Parse(objScalar.ToString());
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to SO_SaleOrderMaster
		///    </Description>
		///    <Inputs>
		///       SO_SaleOrderMasterVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DuongNA
		///    </Authors>
		///    <History>
		///       28-Oct-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetTotalQuantity(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetTotalQuantity()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			object objScalar = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"SELECT SUM(IsNull(OrderQuantity,0)) FROM SO_SaleOrderDetail WHERE SaleOrderMasterID = ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CODE_FLD, pintMasterID));

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				objScalar = ocmdPCS.ExecuteScalar();
				return decimal.Parse(objScalar.ToString());
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to SO_SaleOrderMaster
		///    </Description>
		///    <Inputs>
		///       SO_SaleOrderMasterVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DuongNA
		///    </Authors>
		///    <History>
		///       28-Oct-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************	
		public void UpdateShipCompleteByID(int pintSOMasterID)
		{
			const string METHOD_NAME = THIS + ".UpdateShipComplete()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE SO_SaleOrderMaster SET "
					+ SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + "=   1"
					+" WHERE " + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderMasterTable.CODE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderMasterTable.CODE_FLD].Value = pintSOMasterID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
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

	}
}
