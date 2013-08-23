using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComProcurement.Purchase.DS
{
	
	public class PO_PurchaseRequisitionMasterDS 
	{
		public PO_PurchaseRequisitionMasterDS()
		{
		}
		private const string THIS = "PCSComProcurement.Purchase.DS.DS.PO_PurchaseRequisitionMasterDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_PurchaseRequisitionMaster
		///    </Description>
		///    <Inputs>
		///        PO_PurchaseRequisitionMasterVO       
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
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PO_PurchaseRequisitionMasterVO objObject = (PO_PurchaseRequisitionMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PO_PurchaseRequisitionMaster("
				+ PO_PurchaseRequisitionMasterTable.ORDERDATE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.DELIVERYDATE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.VAT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.IMPORTTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.SPECIALTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CODE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CCNID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.REQUESTORID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.APPROVERID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.EXCHANGERATEID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.DELIVERYTERMSID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.PAYMENTTERMSID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CARRIERID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CURRENCYID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALIMPORTTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALVAT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALSPECIALTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALAMOUNT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALDISCOUNT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALNETAMOUNT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.APPROVALDATE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.PARTYID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.PARTYCONTACTID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.VENDORLOCID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.SHIPTOLOCID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.INVTOLOCID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.SOURCE_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.ORDERDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.ORDERDATE_FLD].Value = objObject.OrderDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.DELIVERYDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.DELIVERYDATE_FLD].Value = objObject.DeliveryDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.VAT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.IMPORTTAX_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.IMPORTTAX_FLD].Value = objObject.ImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.SPECIALTAX_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.REQUESTORID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.REQUESTORID_FLD].Value = objObject.RequestorID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.APPROVERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.APPROVERID_FLD].Value = objObject.ApproverID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.EXCHANGERATEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.EXCHANGERATEID_FLD].Value = objObject.ExchangeRateID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.DELIVERYTERMSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.DELIVERYTERMSID_FLD].Value = objObject.DeliveryTermsID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.PAYMENTTERMSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.PAYMENTTERMSID_FLD].Value = objObject.PaymentTermsID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.CARRIERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.CARRIERID_FLD].Value = objObject.CarrierID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.TOTALIMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.TOTALIMPORTTAX_FLD].Value = objObject.TotalImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.TOTALVAT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.TOTALVAT_FLD].Value = objObject.TotalVAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.TOTALSPECIALTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.TOTALSPECIALTAX_FLD].Value = objObject.TotalSpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.TOTALDISCOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.TOTALDISCOUNT_FLD].Value = objObject.TotalDiscount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.TOTALNETAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.TOTALNETAMOUNT_FLD].Value = objObject.TotalNetAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.APPROVALDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.APPROVALDATE_FLD].Value = objObject.ApprovalDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.VENDORLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.VENDORLOCID_FLD].Value = objObject.VendorLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.SHIPTOLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.SHIPTOLOCID_FLD].Value = objObject.ShipToLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.INVTOLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.INVTOLOCID_FLD].Value = objObject.InvToLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.SOURCE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.SOURCE_FLD].Value = objObject.Source;


				
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
		///       This method uses to delete data from PO_PurchaseRequisitionMaster
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
			strSql=	"DELETE " + PO_PurchaseRequisitionMasterTable.TABLE_NAME + " WHERE  " + "PurchaseRequisitionMasterID" + "=" + pintID.ToString();
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
		///       This method uses to get data from PO_PurchaseRequisitionMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_PurchaseRequisitionMasterVO
		///    </Outputs>
		///    <Returns>
		///       PO_PurchaseRequisitionMasterVO
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
				strSql=	"SELECT "
				+ PO_PurchaseRequisitionMasterTable.PURCHASEREQUISITIONMASTERID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.ORDERDATE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.DELIVERYDATE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.VAT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.IMPORTTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.SPECIALTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CODE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CCNID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.REQUESTORID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.APPROVERID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.EXCHANGERATEID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.DELIVERYTERMSID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.PAYMENTTERMSID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CARRIERID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CURRENCYID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALIMPORTTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALVAT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALSPECIALTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALAMOUNT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALDISCOUNT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALNETAMOUNT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.APPROVALDATE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.PARTYID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.PARTYCONTACTID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.VENDORLOCID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.SHIPTOLOCID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.INVTOLOCID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.SOURCE_FLD
				+ " FROM " + PO_PurchaseRequisitionMasterTable.TABLE_NAME
				+" WHERE " + PO_PurchaseRequisitionMasterTable.PURCHASEREQUISITIONMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_PurchaseRequisitionMasterVO objObject = new PO_PurchaseRequisitionMasterVO();

				while (odrPCS.Read())
				{ 
				objObject.PurchaseRequisitionMasterID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.PURCHASEREQUISITIONMASTERID_FLD].ToString());
				objObject.OrderDate = DateTime.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.ORDERDATE_FLD].ToString());
				objObject.DeliveryDate = DateTime.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.DELIVERYDATE_FLD].ToString());
				objObject.VAT = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.VAT_FLD].ToString());
				objObject.ImportTax = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.IMPORTTAX_FLD].ToString());
				objObject.SpecialTax = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.SPECIALTAX_FLD].ToString());
				objObject.Code = odrPCS[PO_PurchaseRequisitionMasterTable.CODE_FLD].ToString();
				objObject.CCNID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.CCNID_FLD].ToString());
				objObject.RequestorID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.REQUESTORID_FLD].ToString());
				objObject.ApproverID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.APPROVERID_FLD].ToString());
				objObject.ExchangeRateID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.EXCHANGERATEID_FLD].ToString());
				objObject.DeliveryTermsID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.DELIVERYTERMSID_FLD].ToString());
				objObject.PaymentTermsID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.PAYMENTTERMSID_FLD].ToString());
				objObject.CarrierID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.CARRIERID_FLD].ToString());
				objObject.CurrencyID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.CURRENCYID_FLD].ToString());
				objObject.TotalImportTax = Decimal.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.TOTALIMPORTTAX_FLD].ToString());
				objObject.TotalVAT = Decimal.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.TOTALVAT_FLD].ToString());
				objObject.TotalSpecialTax = Decimal.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.TOTALSPECIALTAX_FLD].ToString());
				objObject.TotalAmount = Decimal.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.TOTALAMOUNT_FLD].ToString());
				objObject.TotalDiscount = Decimal.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.TOTALDISCOUNT_FLD].ToString());
				objObject.TotalNetAmount = Decimal.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.TOTALNETAMOUNT_FLD].ToString());
				objObject.ApprovalDate = DateTime.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.APPROVALDATE_FLD].ToString());
				objObject.PartyID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.PARTYID_FLD].ToString());
				objObject.PartyContactID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.PARTYCONTACTID_FLD].ToString());
				objObject.VendorLocID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.VENDORLOCID_FLD].ToString());
				objObject.ShipToLocID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.SHIPTOLOCID_FLD].ToString());
				objObject.InvToLocID = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.INVTOLOCID_FLD].ToString());
				objObject.Source = int.Parse(odrPCS[PO_PurchaseRequisitionMasterTable.SOURCE_FLD].ToString());

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
		///       This method uses to update data to PO_PurchaseRequisitionMaster
		///    </Description>
		///    <Inputs>
		///       PO_PurchaseRequisitionMasterVO       
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

			PO_PurchaseRequisitionMasterVO objObject = (PO_PurchaseRequisitionMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PO_PurchaseRequisitionMaster SET "
				+ PO_PurchaseRequisitionMasterTable.ORDERDATE_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.DELIVERYDATE_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.VAT_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.IMPORTTAX_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.SPECIALTAX_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.CODE_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.CCNID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.REQUESTORID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.APPROVERID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.EXCHANGERATEID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.DELIVERYTERMSID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.PAYMENTTERMSID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.CARRIERID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.CURRENCYID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALIMPORTTAX_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALVAT_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALSPECIALTAX_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALAMOUNT_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALDISCOUNT_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALNETAMOUNT_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.APPROVALDATE_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.PARTYID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.PARTYCONTACTID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.VENDORLOCID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.SHIPTOLOCID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.INVTOLOCID_FLD + "=   ?" + ","
				+ PO_PurchaseRequisitionMasterTable.SOURCE_FLD + "=  ?"
				+" WHERE " + PO_PurchaseRequisitionMasterTable.PURCHASEREQUISITIONMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.ORDERDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.ORDERDATE_FLD].Value = objObject.OrderDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.DELIVERYDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.DELIVERYDATE_FLD].Value = objObject.DeliveryDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.VAT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.IMPORTTAX_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.IMPORTTAX_FLD].Value = objObject.ImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.SPECIALTAX_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.REQUESTORID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.REQUESTORID_FLD].Value = objObject.RequestorID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.APPROVERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.APPROVERID_FLD].Value = objObject.ApproverID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.EXCHANGERATEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.EXCHANGERATEID_FLD].Value = objObject.ExchangeRateID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.DELIVERYTERMSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.DELIVERYTERMSID_FLD].Value = objObject.DeliveryTermsID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.PAYMENTTERMSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.PAYMENTTERMSID_FLD].Value = objObject.PaymentTermsID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.CARRIERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.CARRIERID_FLD].Value = objObject.CarrierID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.TOTALIMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.TOTALIMPORTTAX_FLD].Value = objObject.TotalImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.TOTALVAT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.TOTALVAT_FLD].Value = objObject.TotalVAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.TOTALSPECIALTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.TOTALSPECIALTAX_FLD].Value = objObject.TotalSpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.TOTALDISCOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.TOTALDISCOUNT_FLD].Value = objObject.TotalDiscount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.TOTALNETAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.TOTALNETAMOUNT_FLD].Value = objObject.TotalNetAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.APPROVALDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.APPROVALDATE_FLD].Value = objObject.ApprovalDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.PARTYCONTACTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.PARTYCONTACTID_FLD].Value = objObject.PartyContactID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.VENDORLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.VENDORLOCID_FLD].Value = objObject.VendorLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.SHIPTOLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.SHIPTOLOCID_FLD].Value = objObject.ShipToLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.INVTOLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.INVTOLOCID_FLD].Value = objObject.InvToLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.SOURCE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.SOURCE_FLD].Value = objObject.Source;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseRequisitionMasterTable.PURCHASEREQUISITIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseRequisitionMasterTable.PURCHASEREQUISITIONMASTERID_FLD].Value = objObject.PurchaseRequisitionMasterID;


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
		///       This method uses to get all data from PO_PurchaseRequisitionMaster
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
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
				+ PO_PurchaseRequisitionMasterTable.PURCHASEREQUISITIONMASTERID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.ORDERDATE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.DELIVERYDATE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.VAT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.IMPORTTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.SPECIALTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CODE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CCNID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.REQUESTORID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.APPROVERID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.EXCHANGERATEID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.DELIVERYTERMSID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.PAYMENTTERMSID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CARRIERID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CURRENCYID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALIMPORTTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALVAT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALSPECIALTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALAMOUNT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALDISCOUNT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALNETAMOUNT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.APPROVALDATE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.PARTYID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.PARTYCONTACTID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.VENDORLOCID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.SHIPTOLOCID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.INVTOLOCID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.SOURCE_FLD
					+ " FROM " + PO_PurchaseRequisitionMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PO_PurchaseRequisitionMasterTable.TABLE_NAME);

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
		///       Tuesday, March 01, 2005
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
				+ PO_PurchaseRequisitionMasterTable.PURCHASEREQUISITIONMASTERID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.ORDERDATE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.DELIVERYDATE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.VAT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.IMPORTTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.SPECIALTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CODE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CCNID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.REQUESTORID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.APPROVERID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.EXCHANGERATEID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.DELIVERYTERMSID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.PAYMENTTERMSID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CARRIERID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.CURRENCYID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALIMPORTTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALVAT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALSPECIALTAX_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALAMOUNT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALDISCOUNT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.TOTALNETAMOUNT_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.APPROVALDATE_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.PARTYID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.PARTYCONTACTID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.VENDORLOCID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.SHIPTOLOCID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.INVTOLOCID_FLD + ","
				+ PO_PurchaseRequisitionMasterTable.SOURCE_FLD;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PO_PurchaseRequisitionMasterTable.TABLE_NAME);

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
	}
}
