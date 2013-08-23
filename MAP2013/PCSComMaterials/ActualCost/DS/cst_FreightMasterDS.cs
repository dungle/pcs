using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComMaterials.ActualCost.DS
{
	public class cst_FreightMasterDS 
	{
		public cst_FreightMasterDS()
		{
		}
		private const string THIS = "PCSComMaterials.ActualCost.DS.cst_FreightMasterDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to cst_FreightMaster
		///    </Description>
		///    <Inputs>
		///        cst_FreightMasterVO       
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
		///       Thursday, February 23, 2006
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
				cst_FreightMasterVO objObject = (cst_FreightMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO cst_FreightMaster("
				+ cst_FreightMasterTable.TRANNO_FLD + ","
				+ cst_FreightMasterTable.NOTE_FLD + ","
				+ cst_FreightMasterTable.EXCHANGERATE_FLD + ","
				+ cst_FreightMasterTable.TOTALAMOUNT_FLD + ","
				+ cst_FreightMasterTable.CCNID_FLD + ","
				+ cst_FreightMasterTable.CURRENCYID_FLD + ","
				+ cst_FreightMasterTable.TRANSPORTERID_FLD + ","
				+ cst_FreightMasterTable.VENDORID_FLD + ","
//				+ cst_FreightMasterTable.COSTELEMENTID_FLD + ","
				+ cst_FreightMasterTable.RECEIPTMASTERID_FLD + ","
				+ cst_FreightMasterTable.GRANDTOTAL_FLD + ","
				+ cst_FreightMasterTable.SUBTOTAL_FLD + ","
				+ cst_FreightMasterTable.TOTALVAT_FLD + ","
				+ cst_FreightMasterTable.VATPERCENT_FLD + ","
				+ cst_FreightMasterTable.POSTDATE_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.TRANNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[cst_FreightMasterTable.TRANNO_FLD].Value = objObject.TranNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.NOTE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[cst_FreightMasterTable.NOTE_FLD].Value = objObject.Note;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightMasterTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.TRANSPORTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.TRANSPORTERID_FLD].Value = objObject.TransporterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.VENDORID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.VENDORID_FLD].Value = objObject.VendorID;

//				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.COSTELEMENTID_FLD, OleDbType.Integer));
//				ocmdPCS.Parameters[cst_FreightMasterTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;
//
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.RECEIPTMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.RECEIPTMASTERID_FLD].Value = objObject.ReceiveMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.GRANDTOTAL_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightMasterTable.GRANDTOTAL_FLD].Value = objObject.GrandTotal;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.SUBTOTAL_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightMasterTable.SUBTOTAL_FLD].Value = objObject.SubTotal;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.TOTALVAT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightMasterTable.TOTALVAT_FLD].Value = objObject.TotalVAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.VATPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightMasterTable.VATPERCENT_FLD].Value = objObject.VATPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[cst_FreightMasterTable.POSTDATE_FLD].Value = objObject.PostDate;


				
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
		/// AddAndReturnID
		/// </summary>
		/// <param name="pobjObjectVO"></param>
		///<author>Trada</author>
		///<date>Monday, Feb 27 2006</date>
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				cst_FreightMasterVO objObject = (cst_FreightMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO cst_FreightMaster("
					+ cst_FreightMasterTable.TRANNO_FLD + ","
					+ cst_FreightMasterTable.NOTE_FLD + ","
					+ cst_FreightMasterTable.EXCHANGERATE_FLD + ","
					+ cst_FreightMasterTable.TOTALAMOUNT_FLD + ","
					+ cst_FreightMasterTable.CCNID_FLD + ","
					+ cst_FreightMasterTable.CURRENCYID_FLD + ","
					+ cst_FreightMasterTable.TRANSPORTERID_FLD + ","
					+ cst_FreightMasterTable.VENDORID_FLD + ","
					+ cst_FreightMasterTable.ACOBJECTID_FLD + ","
					+ cst_FreightMasterTable.ACPURPOSEID_FLD + ","
					+ cst_FreightMasterTable.MAKERID_FLD + ","
//					+ cst_FreightMasterTable.COSTELEMENTID_FLD + ","
					+ cst_FreightMasterTable.RECEIPTMASTERID_FLD + ","
					+ cst_FreightMasterTable.RETURNTOVENDORMASTERID_FLD + ","
					+ cst_FreightMasterTable.GRANDTOTAL_FLD + ","
					+ cst_FreightMasterTable.SUBTOTAL_FLD + ","
					+ cst_FreightMasterTable.TOTALVAT_FLD + ","
					+ cst_FreightMasterTable.VATPERCENT_FLD + ","
					+ cst_FreightMasterTable.POSTDATE_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"
					+ "; SELECT @@IDENTITY as LatestID";
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.TRANNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[cst_FreightMasterTable.TRANNO_FLD].Value = objObject.TranNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.NOTE_FLD, OleDbType.WChar));
				if (objObject.Note != string.Empty)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.NOTE_FLD].Value = objObject.Note;
				}
				else 
					ocmdPCS.Parameters[cst_FreightMasterTable.NOTE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				if (objObject.TotalAmount != 0)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;
				}
				else
					ocmdPCS.Parameters[cst_FreightMasterTable.TOTALAMOUNT_FLD].Value = DBNull.Value;	

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.TRANSPORTERID_FLD, OleDbType.Integer));
				if (objObject.TransporterID != 0)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.TRANSPORTERID_FLD].Value = objObject.TransporterID;
				}
				else
					ocmdPCS.Parameters[cst_FreightMasterTable.TRANSPORTERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.VENDORID_FLD, OleDbType.Integer));
				if (objObject.VendorID != 0)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.VENDORID_FLD].Value = objObject.VendorID;
				}
				else
					ocmdPCS.Parameters[cst_FreightMasterTable.VENDORID_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.ACOBJECTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.ACOBJECTID_FLD].Value = objObject.ACObjectID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.ACPURPOSEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.ACPURPOSEID_FLD].Value = objObject.ACPurposeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.MAKERID_FLD, OleDbType.Integer));
				if (objObject.MakerID != 0)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.MAKERID_FLD].Value = objObject.MakerID;
				}
				else
					ocmdPCS.Parameters[cst_FreightMasterTable.MAKERID_FLD].Value = DBNull.Value;
//				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.COSTELEMENTID_FLD, OleDbType.Integer));
//				ocmdPCS.Parameters[cst_FreightMasterTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;
//
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.RECEIPTMASTERID_FLD, OleDbType.Integer));
				if (objObject.ReceiveMasterID != 0)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.RECEIPTMASTERID_FLD].Value = objObject.ReceiveMasterID;
				}
				else 
					ocmdPCS.Parameters[cst_FreightMasterTable.RECEIPTMASTERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.RETURNTOVENDORMASTERID_FLD, OleDbType.Integer));
				if (objObject.ReturnToVendorMasterID != 0)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.RETURNTOVENDORMASTERID_FLD].Value = objObject.ReturnToVendorMasterID;
				}
				else 
					ocmdPCS.Parameters[cst_FreightMasterTable.RETURNTOVENDORMASTERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.GRANDTOTAL_FLD, OleDbType.Decimal));
				if (objObject.GrandTotal != 0)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.GRANDTOTAL_FLD].Value = objObject.GrandTotal;
				}
				else
					ocmdPCS.Parameters[cst_FreightMasterTable.GRANDTOTAL_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.SUBTOTAL_FLD, OleDbType.Decimal));
				if (objObject.SubTotal != 0)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.SUBTOTAL_FLD].Value = objObject.SubTotal;
				}
				else
					ocmdPCS.Parameters[cst_FreightMasterTable.SUBTOTAL_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.TOTALVAT_FLD, OleDbType.Decimal));
				if (objObject.TotalVAT != 0)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.TOTALVAT_FLD].Value = objObject.TotalVAT;
				}
				else
					ocmdPCS.Parameters[cst_FreightMasterTable.TOTALVAT_FLD].Value = DBNull.Value;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.VATPERCENT_FLD, OleDbType.Decimal));
				if (objObject.VATPercent != 0)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.VATPERCENT_FLD].Value = objObject.VATPercent;
				}
				else
					ocmdPCS.Parameters[cst_FreightMasterTable.VATPERCENT_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[cst_FreightMasterTable.POSTDATE_FLD].Value = objObject.PostDate;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();

				object objReturnValue = ocmdPCS.ExecuteScalar();
				if(objReturnValue != null)
				{
					return int.Parse(objReturnValue.ToString());
				}
				else
				{
					return -1;
				}
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
		///       This method uses to delete data from cst_FreightMaster
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
			strSql=	"DELETE " + cst_FreightMasterTable.TABLE_NAME + " WHERE  " + "FreightMasterID" + "=" + pintID.ToString();
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
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
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
	
		/// <summary>
		/// Get PO detail to freight
		/// </summary>
		/// <param name="pintPOReceiveMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		public DataSet GetPOReceive(int pintPOReceiveMasterID)
		{
			const string METHOD_NAME = THIS + ".GetPOReceive()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = " SELECT 0 Line, P.Code,  P.ProductID,  P.Description,  P.Revision, "
						+ " UM.Code MST_UnitOfMeasureCode,  RD.BuyingUMID, RD.ReceiveQuantity Quantity, "
						+ " case RM.ReceiptType when 3 then Invoice.UnitPrice "
						+ " when 4 Then PO.UnitPrice when 2 Then PO.UnitPrice End UnitPriceCIF,"
						+ " case RM.ReceiptType when 3 then Invoice.ImportTax "
						+ " when 4 Then PO.ImportTax when 2 Then PO.ImportTax End ImportTax,"
						+ " case RM.ReceiptType when 3 then Invoice.VAT "
						+ " when 4 Then PO.VAT when 2 Then PO.VAT End VAT,"
						+ " 0.0 Amount, 0.0 VATAmount, 0.0 TotalAmount, "
						+ " 0 FreightMasterID, 0 FreightDetailID, RM.PurchaseOrderReceiptID, 0 AdjustmentID, '' TransNo, '' InvoiceNo, 0 InvoiceMasterID,"
						+ " RD.PurchaseOrderReceiptDetailID,RM.PurchaseOrderMasterID "
						+ " FROM PO_PurchaseOrderReceiptDetail RD  INNER JOIN PO_PurchaseOrderReceiptMaster RM  "
						+ " ON  RM.PurchaseOrderReceiptID =  RD.PurchaseOrderReceiptID "
						+ " INNER JOIN ITM_Product P  ON P.ProductID =  RD.ProductID "
						+ " INNER JOIN MST_UnitOfMeasure UM  ON  UM.UnitOfMeasureID =  RD.BuyingUMID "
						+ " Left Join (Select IVD.InvoiceMasterID, DeliveryScheduleID, IVD.UnitPrice,"
						+ " IVD.ImportTax, IVD.VAT "
						+ " from PO_InvoiceDetail IVD INNER JOIN PO_InvoiceMaster IVM  "
						+ " ON IVM.InvoiceMasterID =  IVD.InvoiceMasterID ) Invoice ON RM.InvoiceMasterID=Invoice.InvoiceMasterID and RD.DeliveryScheduleID=invoice.DeliveryScheduleID "
						+ " Left Join (Select UnitPrice,DeliveryScheduleID,PD.PurchaseOrderMasterID, "
						+ " PD.ImportTax,PD.VAT from PO_PurchaseOrderDetail PD INNER JOIN PO_PurchaseOrderMaster PM  "
						+ " ON PD.PurchaseOrderMasterID =  PM.PurchaseOrderMasterID "
						+ " INNER JOIN PO_DeliverySchedule DS  ON PD.PurchaseOrderDetailID =  DS.PurchaseOrderDetailID"
						+ ") PO ON RM.PurchaseOrderMasterID=PO.PurchaseOrderMasterID and RD.DeliveryScheduleID=PO.DeliveryScheduleID"
						+ " WHERE RM.PurchaseOrderReceiptID = " + pintPOReceiveMasterID.ToString();

				strSql += " select PM.Code, IM.InvoiceNo from PO_PurchaseOrderReceiptMaster PR "
						+ " Left join dbo.PO_PurchaseOrderMaster PM ON PM.PurchaseOrderMasterID = PR.PurchaseOrderMasterID "
						+ " Left join dbo.PO_InvoiceMaster IM ON IM.InvoiceMasterID = PR.InvoiceMasterID "
						+ " where " + PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD + " = " + pintPOReceiveMasterID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, cst_FreightDetailTable.TABLE_NAME);

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
		///       This method uses to get data from cst_FreightMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       cst_FreightMasterVO
		///    </Outputs>
		///    <Returns>
		///       cst_FreightMasterVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Thursday, February 23, 2006
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
				+ cst_FreightMasterTable.FREIGHTMASTERID_FLD + ","
				+ cst_FreightMasterTable.TRANNO_FLD + ","
				+ cst_FreightMasterTable.NOTE_FLD + ","
				+ cst_FreightMasterTable.EXCHANGERATE_FLD + ","
				+ cst_FreightMasterTable.TOTALAMOUNT_FLD + ","
				+ cst_FreightMasterTable.CCNID_FLD + ","
				+ cst_FreightMasterTable.CURRENCYID_FLD + ","
				+ cst_FreightMasterTable.TRANSPORTERID_FLD + ","
				+ cst_FreightMasterTable.VENDORID_FLD + ","
//				+ cst_FreightMasterTable.COSTELEMENTID_FLD + ","
				+ cst_FreightMasterTable.RECEIPTMASTERID_FLD + ","
				+ cst_FreightMasterTable.GRANDTOTAL_FLD + ","
				+ cst_FreightMasterTable.SUBTOTAL_FLD + ","
				+ cst_FreightMasterTable.TOTALVAT_FLD + ","
				+ cst_FreightMasterTable.VATPERCENT_FLD + ","
				+ cst_FreightMasterTable.POSTDATE_FLD
				+ " FROM " + cst_FreightMasterTable.TABLE_NAME
				+" WHERE " + cst_FreightMasterTable.FREIGHTMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				cst_FreightMasterVO objObject = new cst_FreightMasterVO();

				while (odrPCS.Read())
				{ 
				objObject.FreightMasterID = int.Parse(odrPCS[cst_FreightMasterTable.FREIGHTMASTERID_FLD].ToString().Trim());
				objObject.TranNo = odrPCS[cst_FreightMasterTable.TRANNO_FLD].ToString().Trim();
				if (odrPCS[cst_FreightMasterTable.NOTE_FLD] != DBNull.Value)
				{
					objObject.Note = odrPCS[cst_FreightMasterTable.NOTE_FLD].ToString().Trim();
				}
				else objObject.Note = string.Empty;
				objObject.ExchangeRate = Decimal.Parse(odrPCS[cst_FreightMasterTable.EXCHANGERATE_FLD].ToString().Trim());
				objObject.TotalAmount = Decimal.Parse(odrPCS[cst_FreightMasterTable.TOTALAMOUNT_FLD].ToString().Trim());
				objObject.CCNID = int.Parse(odrPCS[cst_FreightMasterTable.CCNID_FLD].ToString().Trim());
				objObject.CurrencyID = int.Parse(odrPCS[cst_FreightMasterTable.CURRENCYID_FLD].ToString().Trim());
				objObject.TransporterID = int.Parse(odrPCS[cst_FreightMasterTable.TRANSPORTERID_FLD].ToString().Trim());
				objObject.VendorID = int.Parse(odrPCS[cst_FreightMasterTable.VENDORID_FLD].ToString().Trim());
//				objObject.CostElementID = int.Parse(odrPCS[cst_FreightMasterTable.COSTELEMENTID_FLD].ToString().Trim());
				objObject.ReceiveMasterID = int.Parse(odrPCS[cst_FreightMasterTable.RECEIPTMASTERID_FLD].ToString().Trim());
				objObject.GrandTotal = Decimal.Parse(odrPCS[cst_FreightMasterTable.GRANDTOTAL_FLD].ToString().Trim());
				objObject.SubTotal = Decimal.Parse(odrPCS[cst_FreightMasterTable.SUBTOTAL_FLD].ToString().Trim());
				if (odrPCS[cst_FreightMasterTable.TOTALVAT_FLD] != DBNull.Value)
				{
					objObject.TotalVAT = Decimal.Parse(odrPCS[cst_FreightMasterTable.TOTALVAT_FLD].ToString().Trim());
				}
				else objObject.TotalVAT = 0;
				if (odrPCS[cst_FreightMasterTable.VATPERCENT_FLD] != DBNull.Value)
				{
					objObject.VATPercent = Decimal.Parse(odrPCS[cst_FreightMasterTable.VATPERCENT_FLD].ToString().Trim());
				}
				else objObject.VATPercent = 0;
				objObject.PostDate = DateTime.Parse(odrPCS[cst_FreightMasterTable.POSTDATE_FLD].ToString().Trim());

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
		///       This method uses to update data to cst_FreightMaster
		///    </Description>
		///    <Inputs>
		///       cst_FreightMasterVO       
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

			cst_FreightMasterVO objObject = (cst_FreightMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE cst_FreightMaster SET "
				+ cst_FreightMasterTable.TRANNO_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.NOTE_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.EXCHANGERATE_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.TOTALAMOUNT_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.CCNID_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.CURRENCYID_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.TRANSPORTERID_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.VENDORID_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.MAKERID_FLD + " = ?,"
				+ cst_FreightMasterTable.ACOBJECTID_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.ACPURPOSEID_FLD + "=   ?" + ","
//				+ cst_FreightMasterTable.COSTELEMENTID_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.RECEIPTMASTERID_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.GRANDTOTAL_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.SUBTOTAL_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.TOTALVAT_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.VATPERCENT_FLD + "=   ?" + ","
				+ cst_FreightMasterTable.POSTDATE_FLD + "=  ?"
				+" WHERE " + cst_FreightMasterTable.FREIGHTMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.TRANNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[cst_FreightMasterTable.TRANNO_FLD].Value = objObject.TranNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.NOTE_FLD, OleDbType.WChar));
				if (objObject.Note != null)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.NOTE_FLD].Value = objObject.Note;
				}
				else 
					ocmdPCS.Parameters[cst_FreightMasterTable.NOTE_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightMasterTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.TRANSPORTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.TRANSPORTERID_FLD].Value = objObject.TransporterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.VENDORID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.VENDORID_FLD].Value = objObject.VendorID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.MAKERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.MAKERID_FLD].Value = objObject.MakerID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.ACOBJECTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.ACOBJECTID_FLD].Value = objObject.ACObjectID;
	
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.ACPURPOSEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.ACPURPOSEID_FLD].Value = objObject.ACPurposeID;
				
				
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.RECEIPTMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.RECEIPTMASTERID_FLD].Value = objObject.ReceiveMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.GRANDTOTAL_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightMasterTable.GRANDTOTAL_FLD].Value = objObject.GrandTotal;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.SUBTOTAL_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightMasterTable.SUBTOTAL_FLD].Value = objObject.SubTotal;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.TOTALVAT_FLD, OleDbType.Decimal));
				if (objObject.TotalVAT != 0)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.TOTALVAT_FLD].Value = objObject.TotalVAT;
				}
				else  
					ocmdPCS.Parameters[cst_FreightMasterTable.TOTALVAT_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.VATPERCENT_FLD, OleDbType.Decimal));
				if (objObject.VATPercent != 0)
				{
					ocmdPCS.Parameters[cst_FreightMasterTable.VATPERCENT_FLD].Value = objObject.VATPercent;
				}
				else
					ocmdPCS.Parameters[cst_FreightMasterTable.VATPERCENT_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[cst_FreightMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightMasterTable.FREIGHTMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightMasterTable.FREIGHTMASTERID_FLD].Value = objObject.FreightMasterID;


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
		///       This method uses to get all data from cst_FreightMaster
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
		///       Thursday, February 23, 2006
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
				+ cst_FreightMasterTable.FREIGHTMASTERID_FLD + ","
				+ cst_FreightMasterTable.TRANNO_FLD + ","
				+ cst_FreightMasterTable.NOTE_FLD + ","
				+ cst_FreightMasterTable.EXCHANGERATE_FLD + ","
				+ cst_FreightMasterTable.TOTALAMOUNT_FLD + ","
				+ cst_FreightMasterTable.CCNID_FLD + ","
				+ cst_FreightMasterTable.CURRENCYID_FLD + ","
				+ cst_FreightMasterTable.TRANSPORTERID_FLD + ","
				+ cst_FreightMasterTable.VENDORID_FLD + ","
//				+ cst_FreightMasterTable.COSTELEMENTID_FLD + ","
				+ cst_FreightMasterTable.RECEIPTMASTERID_FLD + ","
				+ cst_FreightMasterTable.GRANDTOTAL_FLD + ","
				+ cst_FreightMasterTable.SUBTOTAL_FLD + ","
				+ cst_FreightMasterTable.TOTALVAT_FLD + ","
				+ cst_FreightMasterTable.VATPERCENT_FLD + ","
				+ cst_FreightMasterTable.POSTDATE_FLD
					+ " FROM " + cst_FreightMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,cst_FreightMasterTable.TABLE_NAME);

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
		/// GetInvoice_PONumber
		/// </summary>
		/// <param name="pintReturnToVendorMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, July 6 2006</date>
		public DataSet GetInvoice_PONumber(int pintReturnToVendorMasterID)
		{
			const string METHOD_NAME = THIS + ".GetInvoice_PONumber()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ " IM.InvoiceNo, PM.Code "
					+ " from PO_ReturnToVendorMaster RM "
					+ " left join PO_PurchaseOrderMaster PM "
					+ " ON PM.PurchaseOrderMasterID = RM.PurchaseOrderMasterID "
					+ " left join PO_InvoiceMaster IM "
					+ " ON IM.InvoiceMasterID = RM.InvoiceMasterID "
					+ " Where RM." + PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + " = " + pintReturnToVendorMasterID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,cst_FreightMasterTable.TABLE_NAME);

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
		/// List Freight Master by ID
		/// </summary>
		/// <param name="pintFreightMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		public DataTable ListMasterByID(int pintFreightMasterID)
		{
			const string METHOD_NAME = THIS + ".ListMasterByID()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ " FM." + cst_FreightMasterTable.FREIGHTMASTERID_FLD + ","
					+ " FM." + cst_FreightMasterTable.TRANNO_FLD + ","
					+ " FM." + cst_FreightMasterTable.NOTE_FLD + ","
					+ " FM." + cst_FreightMasterTable.EXCHANGERATE_FLD + ","
					+ " FM." + cst_FreightMasterTable.TOTALAMOUNT_FLD + ","
					+ " FM." + cst_FreightMasterTable.CCNID_FLD + ","
					+ " FM." + cst_FreightMasterTable.CURRENCYID_FLD + ","
					+ " FM." + cst_FreightMasterTable.TRANSPORTERID_FLD + ","
					+ " FM." + cst_FreightMasterTable.VENDORID_FLD + ","
					+ "FM.MakerID,"
					+ " PM.Code PONo,"
					+ " IM." + PO_InvoiceMasterTable.INVOICENO_FLD + ","
					+ " FM." + cst_FreightMasterTable.ACOBJECTID_FLD + ","
					+ " FM." + cst_FreightMasterTable.ACPURPOSEID_FLD + ","
//					+ " FM." + cst_FreightMasterTable.COSTELEMENTID_FLD + ","
					+ " FM." + cst_FreightMasterTable.RECEIPTMASTERID_FLD + ","
					+ " FM." + cst_FreightMasterTable.RETURNTOVENDORMASTERID_FLD + ","
					+ " FM." + cst_FreightMasterTable.GRANDTOTAL_FLD + ","
					+ " FM." + cst_FreightMasterTable.SUBTOTAL_FLD + ","
					+ " FM." + cst_FreightMasterTable.TOTALVAT_FLD + ","
					+ " FM." + cst_FreightMasterTable.VATPERCENT_FLD + ","
					+ " CR." + MST_CurrencyTable.CODE_FLD + Constants.WHITE_SPACE + MST_CurrencyTable.TABLE_NAME + MST_CurrencyTable.CODE_FLD + " ,"
					+ " V." + MST_PartyTable.CODE_FLD + Constants.WHITE_SPACE + " VendorCode,"
					//+ " CE." + STD_CostElementTable.CODE_FLD + Constants.WHITE_SPACE + STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD + ","
					+ " V." + MST_PartyTable.NAME_FLD + Constants.WHITE_SPACE + " VendorName,"
					+ " T." + MST_PartyTable.CODE_FLD + Constants.WHITE_SPACE + " TransporterCode,"
					+ " T." + MST_PartyTable.NAME_FLD + Constants.WHITE_SPACE + " TransporterName,"
					+ " MK.Name  MakerName, MK.Code  MakerCode, FM.ACObjectID, OB.Description OBDes, "
					+ " FM.ACPurposeID, PU.Description PUDes, "
					+ " PRM." + PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD + ","
					+ " RVM." + PO_ReturnToVendorMasterTable.RTVNO_FLD + ","
					+ " FM." + cst_FreightMasterTable.POSTDATE_FLD
					+ " FROM " + cst_FreightMasterTable.TABLE_NAME + " FM"
					+ " INNER JOIN " + MST_CurrencyTable.TABLE_NAME + " CR ON FM."
					+ cst_FreightMasterTable.CURRENCYID_FLD + " = " + " CR." + MST_CurrencyTable.CURRENCYID_FLD
					+ " LEFT JOIN " + MST_PartyTable.TABLE_NAME + " V ON FM."
					+ cst_FreightMasterTable.VENDORID_FLD + " = " + " V." + MST_PartyTable.PARTYID_FLD
					+ " LEFT JOIN " + MST_PartyTable.TABLE_NAME + " T ON FM."
					+ cst_FreightMasterTable.TRANSPORTERID_FLD + " = " + " T." + MST_PartyTable.PARTYID_FLD
					+ " LEFT JOIN " + MST_PartyTable.TABLE_NAME + " MK ON FM."
					+ "MakerID" + " = " + " MK." + MST_PartyTable.PARTYID_FLD
					+ " INNER JOIN " + "enm_ACObject" + " OB ON FM."
					+ "ACObjectID" + " = " + " OB.ACObjectID " 
					+ " INNER JOIN " + "enm_ACPurpose" + " PU ON FM."
					+ "ACPurposeID" + " = " + " PU.ACPurposeID"
					+ " LEFT JOIN " + PO_PurchaseOrderReceiptMasterTable.TABLE_NAME + " PRM ON FM."
					+ cst_FreightMasterTable.RECEIPTMASTERID_FLD + " = " + " PRM." + PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD
					+ " LEFT JOIN " + PO_ReturnToVendorMasterTable.TABLE_NAME + " RVM ON FM."
					+ cst_FreightMasterTable.RETURNTOVENDORMASTERID_FLD + " = " + " RVM." + PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD
					+ " LEFT JOIN PO_PurchaseOrderMaster PM ON PRM.PurchaseOrderMasterID = PM.PurchaseOrderMasterID "
					+ " LEFT JOIN dbo.PO_InvoiceMaster IM ON IM.InvoiceMasterID = PRM.InvoiceMasterID "
					+ " WHERE " + cst_FreightMasterTable.FREIGHTMASTERID_FLD + " = " + pintFreightMasterID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,cst_FreightMasterTable.TABLE_NAME);

				return dstPCS.Tables[0];
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
		///       Thursday, February 23, 2006
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
				+ cst_FreightMasterTable.FREIGHTMASTERID_FLD + ","
				+ cst_FreightMasterTable.TRANNO_FLD + ","
				+ cst_FreightMasterTable.NOTE_FLD + ","
				+ cst_FreightMasterTable.EXCHANGERATE_FLD + ","
				+ cst_FreightMasterTable.TOTALAMOUNT_FLD + ","
				+ cst_FreightMasterTable.CCNID_FLD + ","
				+ cst_FreightMasterTable.CURRENCYID_FLD + ","
				+ cst_FreightMasterTable.TRANSPORTERID_FLD + ","
				+ cst_FreightMasterTable.VENDORID_FLD + ","
//				+ cst_FreightMasterTable.COSTELEMENTID_FLD + ","
				+ cst_FreightMasterTable.RECEIPTMASTERID_FLD + ","
				+ cst_FreightMasterTable.GRANDTOTAL_FLD + ","
				+ cst_FreightMasterTable.SUBTOTAL_FLD + ","
				+ cst_FreightMasterTable.TOTALVAT_FLD + ","
				+ cst_FreightMasterTable.VATPERCENT_FLD + ","
				+ cst_FreightMasterTable.POSTDATE_FLD 
		+ "  FROM " + cst_FreightMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,cst_FreightMasterTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				if(ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
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
