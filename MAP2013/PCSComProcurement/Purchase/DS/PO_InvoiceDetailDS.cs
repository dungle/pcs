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
	public class PO_InvoiceDetailDS 
	{
		public PO_InvoiceDetailDS()
		{
		}
		private const string THIS = "PCSComProcurement.Purchase.DS.PO_InvoiceDetailDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_InvoiceDetail
		///    </Description>
		///    <Inputs>
		///        PO_InvoiceDetailVO       
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
		///       Thursday, September 29, 2005
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
				PO_InvoiceDetailVO objObject = (PO_InvoiceDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PO_InvoiceDetail("
				+ PO_InvoiceDetailTable.INVOICELINE_FLD + ","
				+ PO_InvoiceDetailTable.INVOICEMASTERID_FLD + ","
				+ PO_InvoiceDetailTable.INVOICEQUANTITY_FLD + ","
				+ PO_InvoiceDetailTable.UNITPRICE_FLD + ","
				+ PO_InvoiceDetailTable.VAT_FLD + ","
				+ PO_InvoiceDetailTable.VATAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.IMPORTTAX_FLD + ","
				+ PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.INLAND_FLD + ","
				+ PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.CIFAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.CIPAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.NOTE_FLD + ","
				+ PO_InvoiceDetailTable.PRODUCTID_FLD + ","
				+ PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD + ","
				+ PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD + ","
				+ PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD + ","
				+ PO_InvoiceDetailTable.INVOICEUMID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.INVOICELINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.INVOICELINE_FLD].Value = objObject.InvoiceLine;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.INVOICEMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.INVOICEMASTERID_FLD].Value = objObject.InvoiceMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.INVOICEQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].Value = objObject.InvoiceQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.VAT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.VATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.VATAMOUNT_FLD].Value = objObject.VATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.IMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.IMPORTTAX_FLD].Value = objObject.ImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD].Value = objObject.ImportTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.INLAND_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.INLAND_FLD].Value = objObject.Inland;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD].Value = objObject.BeforeVATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.CIFAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.CIFAMOUNT_FLD].Value = objObject.CIFAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.CIPAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.CIPAMOUNT_FLD].Value = objObject.CIPAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.NOTE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.NOTE_FLD].Value = objObject.Note;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD].Value = objObject.PurchaseOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD].Value = objObject.DeliveryScheduleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.INVOICEUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.INVOICEUMID_FLD].Value = objObject.InvoiceUMID;
				
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
		///       This method uses to delete data from PO_InvoiceDetail
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
			strSql=	"DELETE " + PO_InvoiceDetailTable.TABLE_NAME + " WHERE  " + "InvoiceDetailID" + "=" + pintID.ToString();
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
		///       This method uses to get data from PO_InvoiceDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_InvoiceDetailVO
		///    </Outputs>
		///    <Returns>
		///       PO_InvoiceDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Thursday, September 29, 2005
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
				+ PO_InvoiceDetailTable.INVOICEDETAILID_FLD + ","
				+ PO_InvoiceDetailTable.INVOICELINE_FLD + ","
				+ PO_InvoiceDetailTable.INVOICEMASTERID_FLD + ","
				+ PO_InvoiceDetailTable.INVOICEQUANTITY_FLD + ","
				+ PO_InvoiceDetailTable.UNITPRICE_FLD + ","
				+ PO_InvoiceDetailTable.VAT_FLD + ","
				+ PO_InvoiceDetailTable.VATAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.IMPORTTAX_FLD + ","
				+ PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.INLAND_FLD + ","
				+ PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.CIFAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.CIPAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.NOTE_FLD + ","
				+ PO_InvoiceDetailTable.PRODUCTID_FLD + ","
				+ PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD + ","
				+ PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD + ","
				+ PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD + ","
				+ PO_InvoiceDetailTable.INVOICEUMID_FLD
				+ " FROM " + PO_InvoiceDetailTable.TABLE_NAME
				+" WHERE " + PO_InvoiceDetailTable.INVOICEDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_InvoiceDetailVO objObject = new PO_InvoiceDetailVO();

				while (odrPCS.Read())
				{ 
				objObject.InvoiceDetailID = int.Parse(odrPCS[PO_InvoiceDetailTable.INVOICEDETAILID_FLD].ToString().Trim());
				objObject.InvoiceLine = int.Parse(odrPCS[PO_InvoiceDetailTable.INVOICELINE_FLD].ToString().Trim());
				objObject.InvoiceMasterID = int.Parse(odrPCS[PO_InvoiceDetailTable.INVOICEMASTERID_FLD].ToString().Trim());
				objObject.InvoiceQuantity = Decimal.Parse(odrPCS[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].ToString().Trim());
				objObject.UnitPrice = Decimal.Parse(odrPCS[PO_InvoiceDetailTable.UNITPRICE_FLD].ToString().Trim());
				objObject.VAT = float.Parse(odrPCS[PO_InvoiceDetailTable.VAT_FLD].ToString().Trim());
				objObject.VATAmount = Decimal.Parse(odrPCS[PO_InvoiceDetailTable.VATAMOUNT_FLD].ToString().Trim());
				objObject.ImportTax = float.Parse(odrPCS[PO_InvoiceDetailTable.IMPORTTAX_FLD].ToString().Trim());
				objObject.ImportTaxAmount = Decimal.Parse(odrPCS[PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD].ToString().Trim());
				objObject.Inland = Decimal.Parse(odrPCS[PO_InvoiceDetailTable.INLAND_FLD].ToString().Trim());
				objObject.BeforeVATAmount = Decimal.Parse(odrPCS[PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD].ToString().Trim());
				objObject.CIFAmount = Decimal.Parse(odrPCS[PO_InvoiceDetailTable.CIFAMOUNT_FLD].ToString().Trim());
				objObject.CIPAmount = Decimal.Parse(odrPCS[PO_InvoiceDetailTable.CIPAMOUNT_FLD].ToString().Trim());
				objObject.Note = odrPCS[PO_InvoiceDetailTable.NOTE_FLD].ToString().Trim();
				objObject.ProductID = int.Parse(odrPCS[PO_InvoiceDetailTable.PRODUCTID_FLD].ToString().Trim());
				objObject.PurchaseOrderMasterID = int.Parse(odrPCS[PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD].ToString().Trim());
				objObject.PurchaseOrderDetailID = int.Parse(odrPCS[PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD].ToString().Trim());
				objObject.DeliveryScheduleID = int.Parse(odrPCS[PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD].ToString().Trim());
				objObject.InvoiceUMID = int.Parse(odrPCS[PO_InvoiceDetailTable.INVOICEUMID_FLD].ToString().Trim());

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
		///       This method uses to update data to PO_InvoiceDetail
		///    </Description>
		///    <Inputs>
		///       PO_InvoiceDetailVO       
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

			PO_InvoiceDetailVO objObject = (PO_InvoiceDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PO_InvoiceDetail SET "
				+ PO_InvoiceDetailTable.INVOICELINE_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.INVOICEMASTERID_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.INVOICEQUANTITY_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.UNITPRICE_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.VAT_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.VATAMOUNT_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.IMPORTTAX_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.INLAND_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.CIFAMOUNT_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.CIPAMOUNT_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.NOTE_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.PRODUCTID_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD + "=   ?" + ","
				+ PO_InvoiceDetailTable.INVOICEUMID_FLD + "=  ?"
				+" WHERE " + PO_InvoiceDetailTable.INVOICEDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.INVOICELINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.INVOICELINE_FLD].Value = objObject.InvoiceLine;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.INVOICEMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.INVOICEMASTERID_FLD].Value = objObject.InvoiceMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.INVOICEQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].Value = objObject.InvoiceQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.VAT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.VATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.VATAMOUNT_FLD].Value = objObject.VATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.IMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.IMPORTTAX_FLD].Value = objObject.ImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD].Value = objObject.ImportTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.INLAND_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.INLAND_FLD].Value = objObject.Inland;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD].Value = objObject.BeforeVATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.CIFAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.CIFAMOUNT_FLD].Value = objObject.CIFAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.CIPAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.CIPAMOUNT_FLD].Value = objObject.CIPAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.NOTE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.NOTE_FLD].Value = objObject.Note;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD].Value = objObject.PurchaseOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD].Value = objObject.DeliveryScheduleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.INVOICEUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.INVOICEUMID_FLD].Value = objObject.InvoiceUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceDetailTable.INVOICEDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceDetailTable.INVOICEDETAILID_FLD].Value = objObject.InvoiceDetailID;


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
		///       This method uses to get all data from PO_InvoiceDetail
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
		///       Thursday, September 29, 2005
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
				+ PO_InvoiceDetailTable.INVOICEDETAILID_FLD + ","
				+ PO_InvoiceDetailTable.INVOICELINE_FLD + ","
				+ PO_InvoiceDetailTable.INVOICEMASTERID_FLD + ","
				+ PO_InvoiceDetailTable.INVOICEQUANTITY_FLD + ","
				+ PO_InvoiceDetailTable.UNITPRICE_FLD + ","
				+ PO_InvoiceDetailTable.VAT_FLD + ","
				+ PO_InvoiceDetailTable.VATAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.IMPORTTAX_FLD + ","
				+ PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.INLAND_FLD + ","
				+ PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.CIFAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.CIPAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.NOTE_FLD + ","
				+ PO_InvoiceDetailTable.PRODUCTID_FLD + ","
				+ PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD + ","
				+ PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD + ","
				+ PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD + ","
				+ PO_InvoiceDetailTable.INVOICEUMID_FLD
					+ " FROM " + PO_InvoiceDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PO_InvoiceDetailTable.TABLE_NAME);

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
		/// GetDetailByInvoiceMasterToReturn
		/// </summary>
		/// <param name="pintInvoiceMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, July 11 2006</date>
		public DataSet GetDetailByInvoiceMasterToReturn(int pintInvoiceMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDetailByInvoiceMasterToReturn()";
			DataSet dstPCS = new DataSet();			
		
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	" select "
					+ " 0 Line,IVD.InvoiceQuantity, IVD.ProductID, P.Code ProductCode, P.Description, "
					+ " P.Revision,IVD.InvoiceUMID BuyingUMID, UM.Code BuyingUnitCode, DL.Code LocationCode, P.LocationID, DB.Code BinCode, "
					+ " P.BINID, 0.0 Quantity, 0 MRB, '' Lot, '' Serial, IVD.UnitPrice, 0.0 Amount, 0.0 UMRate,"
					+ " IVD.VAT VatPercent, 0.0 VATAmount, 0.0 TotalAmount "
					+ " from PO_InvoiceDetail IVD  "
					+ " inner join ITM_Product P on P.ProductID = IVD.ProductID "
					+ " inner join MST_UnitOfMeasure UM ON UM.UnitOfMeasureID = IVD.InvoiceUMID "
					+ " left join MST_Location DL on DL.LocationID = P.LocationID "
					+ " left join MST_BIN DB on DB.BINID = P.BINID "
					+ " where " + PO_InvoiceDetailTable.INVOICEMASTERID_FLD + " = " + pintInvoiceMasterID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PO_ReturnToVendorDetailTable.TABLE_NAME);

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
		///       This method uses to get all data from PO_InvoiceDetail
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
		///       Thursday, September 29, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet GetDetailByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();			
		
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT " 				
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.INVOICEMASTERID_FLD  + ", " 				
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.INVOICEDETAILID_FLD  + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.INVOICELINE_FLD  + ", " 
				+  PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CODE_FLD  + " as " + PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD + ", " 
				+  PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.LINE_FLD  + " as " + PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD + ", "
				+  PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.DELIVERYLINE_FLD + ", "
				+  ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + " AS " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD  + ", "				
				+  ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + " as " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD  + ", "
				+  ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + " as " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD  + ", "
				+  ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.TAXCODE_FLD + ", "
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.INVOICEQUANTITY_FLD  + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.UNITPRICE_FLD  + ", "
				+  MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " AS " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.CIFAMOUNT_FLD + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.IMPORTTAX_FLD + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.VAT_FLD + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.VATAMOUNT_FLD + ", "				
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.INLAND_FLD + ", "				
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.CIPAMOUNT_FLD + ", " 
				+  ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.OTHERINFO1_FLD + ", " 
				+  ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PARTNAMEVN_FLD + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.NOTE_FLD  + ", "
				+  MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.CODE_FLD + " as " + MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD + ", "
				
				+ "ISNULL(PO_DeliverySchedule.RECEIVEDQUANTITY, 0) RECEIVEDQUANTITY, "
				
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.PRODUCTID_FLD + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD + ", " 
				+  PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.INVOICEUMID_FLD
				+  " FROM  " + PO_InvoiceDetailTable.TABLE_NAME
				+  " INNER JOIN " + PO_InvoiceMasterTable.TABLE_NAME + " ON " + PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.INVOICEMASTERID_FLD + " = " + PO_InvoiceMasterTable.TABLE_NAME + "." + PO_InvoiceMasterTable.INVOICEMASTERID_FLD
				+  " INNER JOIN " + PO_DeliveryScheduleTable.TABLE_NAME + " ON " + PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + " = " + PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD 
				+  " INNER JOIN " + ITM_ProductTable.TABLE_NAME + " ON " + PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.PRODUCTID_FLD + " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD 
				+  " INNER JOIN " + PO_PurchaseOrderMasterTable.TABLE_NAME + " ON " + PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD + " = " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD 
				+  " INNER JOIN " + PO_PurchaseOrderDetailTable.TABLE_NAME + " ON " + PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD + " = " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD 
				+  " INNER JOIN " + MST_UnitOfMeasureTable.TABLE_NAME  + " ON " + PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.INVOICEUMID_FLD + " = " + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
				+  " LEFT JOIN " + MST_PartyTable.TABLE_NAME + " ON " + MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.PARTYID_FLD + " = " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MAKERID_FLD

				+  " WHERE " + PO_InvoiceMasterTable.TABLE_NAME + "." + PO_InvoiceMasterTable.INVOICEMASTERID_FLD + " = ?"
				+  " ORDER BY " + PO_InvoiceDetailTable.TABLE_NAME + "." + PO_InvoiceDetailTable.INVOICELINE_FLD + " ASC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(PO_InvoiceMasterTable.INVOICEMASTERID_FLD, OleDbType.Integer);
				ocmdPCS.Parameters[PO_InvoiceMasterTable.INVOICEMASTERID_FLD].Value = pintMasterID;

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_InvoiceDetailTable.TABLE_NAME);

				return dstPCS;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB , METHOD_NAME , ex);
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
		///       Thursday, September 29, 2005
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
				+ PO_InvoiceDetailTable.INVOICEDETAILID_FLD + ","
				+ PO_InvoiceDetailTable.INVOICELINE_FLD + ","
				+ PO_InvoiceDetailTable.INVOICEMASTERID_FLD + ","
				+ PO_InvoiceDetailTable.INVOICEQUANTITY_FLD + ","
				+ PO_InvoiceDetailTable.UNITPRICE_FLD + ","
				+ PO_InvoiceDetailTable.VAT_FLD + ","
				+ PO_InvoiceDetailTable.VATAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.IMPORTTAX_FLD + ","
				+ PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.INLAND_FLD + ","
				+ PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.CIFAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.CIPAMOUNT_FLD + ","
				+ PO_InvoiceDetailTable.NOTE_FLD + ","
				+ PO_InvoiceDetailTable.PRODUCTID_FLD + ","
				+ PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD + ","
				+ PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD + ","
				+ PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD + ","
				+ PO_InvoiceDetailTable.INVOICEUMID_FLD 
				+ "  FROM " + PO_InvoiceDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PO_InvoiceDetailTable.TABLE_NAME);

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
