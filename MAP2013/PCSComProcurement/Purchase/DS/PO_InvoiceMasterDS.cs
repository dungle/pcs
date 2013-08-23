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
	public class PO_InvoiceMasterDS 
	{		
		const string SELECTED_COL = "Selected";

		public PO_InvoiceMasterDS()
		{
		}
		private const string THIS = "PCSComProcurement.Purchase.DS.PO_InvoiceMasterDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_InvoiceMaster
		///    </Description>
		///    <Inputs>
		///        PO_InvoiceMasterVO       
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
				PO_InvoiceMasterVO objObject = (PO_InvoiceMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO " + PO_InvoiceMasterTable.TABLE_NAME + "("
				+ PO_InvoiceMasterTable.INVOICENO_FLD + ","
				+ PO_InvoiceMasterTable.POSTDATE_FLD + ","
				+ PO_InvoiceMasterTable.EXCHANGERATE_FLD + ","
				+ PO_InvoiceMasterTable.BLDATE_FLD + ","
				+ PO_InvoiceMasterTable.INFORMDATE_FLD + ","
				+ PO_InvoiceMasterTable.DECLARATIONDATE_FLD + ","
				+ PO_InvoiceMasterTable.BLNUMBER_FLD + ","
				+ PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD + ","
				+ PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD + ","
				+ PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD + ","
				+ PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.CCNID_FLD + ","
				+ PO_InvoiceMasterTable.PARTYID_FLD + ","
				+ PO_InvoiceMasterTable.CURRENCYID_FLD + ","
				+ PO_InvoiceMasterTable.CARRIERID_FLD + ","
				+ PO_InvoiceMasterTable.PAYMENTTERMID_FLD + ","
				+ PO_InvoiceMasterTable.DELIVERYTERMID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.INVOICENO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.INVOICENO_FLD].Value = objObject.InvoiceNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.BLDATE_FLD, OleDbType.Date));
				if(objObject.BLDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.BLDATE_FLD].Value = objObject.BLDate;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.BLDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.INFORMDATE_FLD, OleDbType.Date));
				if(objObject.InformDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.INFORMDATE_FLD].Value = objObject.InformDate;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.INFORMDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.DECLARATIONDATE_FLD, OleDbType.Date));
				if(objObject.DeclarationDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.DECLARATIONDATE_FLD].Value = objObject.DeclarationDate;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.DECLARATIONDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.BLNUMBER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.BLNUMBER_FLD].Value = objObject.BLNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD].Value = objObject.TaxInformNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD].Value = objObject.TaxDeclarationNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD].Value = objObject.TotalInlandAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD].Value = objObject.TotalCIPAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD].Value = objObject.TotalCIFAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD].Value = objObject.TotalImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD].Value = objObject.TotalBeforeVATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD].Value = objObject.TotalVATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.CARRIERID_FLD, OleDbType.Integer));
				if(objObject.CarrierID > 0)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.CARRIERID_FLD].Value = objObject.CarrierID;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.CARRIERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.PAYMENTTERMID_FLD, OleDbType.Integer));
				if(objObject.PaymentTermID > 0)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.PAYMENTTERMID_FLD].Value = objObject.PaymentTermID;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.PAYMENTTERMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.DELIVERYTERMID_FLD, OleDbType.Integer));
				if(objObject.DeliveryTermID > 0)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.DELIVERYTERMID_FLD].Value = objObject.DeliveryTermID;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.DELIVERYTERMID_FLD].Value = DBNull.Value;
				}
				
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
		///       This method uses to add data to PO_InvoiceMaster
		///    </Description>
		///    <Inputs>
		///        PO_InvoiceMasterVO       
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


		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PO_InvoiceMasterVO objObject = (PO_InvoiceMasterVO) pobjObjectVO;				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				string strSql = String.Empty;
				strSql=	"INSERT INTO " + PO_InvoiceMasterTable.TABLE_NAME + "("
					+ PO_InvoiceMasterTable.INVOICENO_FLD + ","
					+ PO_InvoiceMasterTable.POSTDATE_FLD + ","
					+ PO_InvoiceMasterTable.EXCHANGERATE_FLD + ","
					+ PO_InvoiceMasterTable.BLDATE_FLD + ","
					+ PO_InvoiceMasterTable.INFORMDATE_FLD + ","
					+ PO_InvoiceMasterTable.DECLARATIONDATE_FLD + ","
					+ PO_InvoiceMasterTable.BLNUMBER_FLD + ","
					+ PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD + ","
					+ PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD + ","
					+ PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD + ","
					+ PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD + ","
					+ PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD + ","
					+ PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD + ","
					+ PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD + ","
					+ PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD + ","
					+ PO_InvoiceMasterTable.CCNID_FLD + ","
					+ PO_InvoiceMasterTable.PARTYID_FLD + ","
					+ PO_InvoiceMasterTable.CURRENCYID_FLD + ","
					+ PO_InvoiceMasterTable.CARRIERID_FLD + ","
					+ PO_InvoiceMasterTable.PAYMENTTERMID_FLD + ","
					+ PO_InvoiceMasterTable.DELIVERYTERMID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				strSql += "; SELECT @@IDENTITY as LatestID";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.INVOICENO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.INVOICENO_FLD].Value = objObject.InvoiceNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.EXCHANGERATE_FLD].Value = objObject.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.BLDATE_FLD, OleDbType.Date));
				if(objObject.BLDate!= DateTime.MinValue)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.BLDATE_FLD].Value = objObject.BLDate;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.BLDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.INFORMDATE_FLD, OleDbType.Date));
				if(objObject.InformDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.INFORMDATE_FLD].Value = objObject.InformDate;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.INFORMDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.DECLARATIONDATE_FLD, OleDbType.Date));
				if(objObject.DeclarationDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.DECLARATIONDATE_FLD].Value = objObject.DeclarationDate;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.DECLARATIONDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.BLNUMBER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.BLNUMBER_FLD].Value = objObject.BLNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD].Value = objObject.TaxInformNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD].Value = objObject.TaxDeclarationNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD].Value = objObject.TotalInlandAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD].Value = objObject.TotalCIPAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD].Value = objObject.TotalCIFAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD].Value = objObject.TotalImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD].Value = objObject.TotalBeforeVATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD].Value = objObject.TotalVATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.CCNID_FLD].Value = objObject.CCNID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.CURRENCYID_FLD].Value = objObject.CurrencyID;				
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.CARRIERID_FLD, OleDbType.Integer));
				if(objObject.CarrierID > 0)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.CARRIERID_FLD].Value = objObject.CarrierID;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.CARRIERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.PAYMENTTERMID_FLD, OleDbType.Integer));
				if(objObject.PaymentTermID > 0)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.PAYMENTTERMID_FLD].Value = objObject.PaymentTermID;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.PAYMENTTERMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.DELIVERYTERMID_FLD, OleDbType.Integer));
				if(objObject.DeliveryTermID > 0)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.DELIVERYTERMID_FLD].Value = objObject.DeliveryTermID;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.DELIVERYTERMID_FLD].Value = DBNull.Value;
				}
				
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());	

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
		///       This method uses to delete data from PO_InvoiceMaster
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
			strSql=	"DELETE " + PO_InvoiceMasterTable.TABLE_NAME + " WHERE  " + "InvoiceMasterID" + "=" + pintID.ToString();
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
		
		public DataTable SelectPO4Invoice(Hashtable phtbCondition)
		{
			const string METHOD_NAME = THIS + ".SelectPO4Invoice()";
			DataSet dstPCS = new DataSet();
		
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{

				string strSql = "SELECT Convert(bit, 0) as " + SELECTED_COL + ", ";
				strSql += " PurchaseOrderMasterId,";
				strSql += " PO_PurchaseOrderMasterCode,";
				strSql += " PO_PurchaseOrderDetailLine,";
				strSql += " OrderDate,";
				strSql += " DeliveryLine,";
				strSql += " ScheduleDate,";
				strSql += " ITM_ProductCode,";
				strSql += " ITM_ProductRevision,";
				strSql += " ITM_ProductDescription,";
				strSql += " TaxCode,";
				strSql += " DeliveryTermsID,";
				strSql += " PaymentTermsID,";
				strSql += " CarrierID,";
				strSql += " PartyContactID,";
				strSql += " DeliveryQuantity,";
				strSql += " PurchaseOrderDetailId,";
				strSql += " PO_PurchaseOrderDetailUnitPrice,";
				strSql += " PO_PurchaseOrderDetailVAT,";
				strSql += " PO_PurchaseOrderDetailSpecialTax,";
				strSql += " PO_PurchaseOrderDetailImportTax,";
				strSql += " BuyingUMID,";
				strSql += " MST_UnitOfMeasureCode,";
				strSql += " OtherInfo1,";
				strSql += " PartNameVN,";
				strSql += " ProductID,";
				strSql += " CCNID,";
				strSql += " PartyID,";
				strSql += " Closed,";
				strSql += " DeliveryScheduleID,";
				strSql += " MST_PartyCode,ReceivedQuantity";

				strSql += " FROM " + v_SelectPurchaseOrders.VIEW_NAME;

				string strWhereClause = string.Empty;
				//build the where clause
				if(phtbCondition != null)
				{
					IDictionaryEnumerator myEnumerator = phtbCondition.GetEnumerator();

					while(myEnumerator.MoveNext())
					{
						if(strWhereClause.Length != 0)
						{
							strWhereClause += " AND " + v_SelectPurchaseOrders.VIEW_NAME + "." + myEnumerator.Key.ToString().Trim()+  Constants.EQUAL  + "'" +  myEnumerator.Value + "'";
						}
						else
						{
							strWhereClause += v_SelectPurchaseOrders.VIEW_NAME + "." + myEnumerator.Key.ToString().Trim()+  Constants.EQUAL  + "'" +  myEnumerator.Value + "'";
						}
					}
				}

				if(strWhereClause.Length != 0)
				{
					strSql += " " + Constants.WHERE_KEYWORD + " " + strWhereClause;
				}

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, v_SelectPurchaseOrders.VIEW_NAME);
				
				if(dstPCS != null)
				{
					return dstPCS.Tables[v_SelectPurchaseOrders.VIEW_NAME];
				}
		
				return null;
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
		///       This method uses to get data from PO_InvoiceMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_InvoiceMasterVO
		///    </Outputs>
		///    <Returns>
		///       PO_InvoiceMasterVO
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
				+ PO_InvoiceMasterTable.INVOICEMASTERID_FLD + ","
				+ PO_InvoiceMasterTable.INVOICENO_FLD + ","
				+ PO_InvoiceMasterTable.POSTDATE_FLD + ","
				+ PO_InvoiceMasterTable.EXCHANGERATE_FLD + ","
				+ PO_InvoiceMasterTable.BLDATE_FLD + ","
				+ PO_InvoiceMasterTable.INFORMDATE_FLD + ","
				+ PO_InvoiceMasterTable.DECLARATIONDATE_FLD + ","
				+ PO_InvoiceMasterTable.BLNUMBER_FLD + ","
				+ PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD + ","
				+ PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD + ","
				+ PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD + ","
				+ PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.CCNID_FLD + ","
				+ PO_InvoiceMasterTable.PARTYID_FLD + ","
				+ PO_InvoiceMasterTable.CURRENCYID_FLD + ","
				+ PO_InvoiceMasterTable.CARRIERID_FLD + ","
				+ PO_InvoiceMasterTable.PAYMENTTERMID_FLD + ","
				+ PO_InvoiceMasterTable.DELIVERYTERMID_FLD
				+ " FROM " + PO_InvoiceMasterTable.TABLE_NAME
				+" WHERE " + PO_InvoiceMasterTable.INVOICEMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_InvoiceMasterVO objObject = new PO_InvoiceMasterVO();

				if(odrPCS.Read())
				{ 
					objObject.InvoiceMasterID = int.Parse(odrPCS[PO_InvoiceMasterTable.INVOICEMASTERID_FLD].ToString().Trim());
					objObject.InvoiceNo = odrPCS[PO_InvoiceMasterTable.INVOICENO_FLD].ToString().Trim();
					objObject.PostDate = DateTime.Parse(odrPCS[PO_InvoiceMasterTable.POSTDATE_FLD].ToString().Trim());
					objObject.ExchangeRate = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.EXCHANGERATE_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[PO_InvoiceMasterTable.CCNID_FLD].ToString().Trim());
					objObject.PartyID = int.Parse(odrPCS[PO_InvoiceMasterTable.PARTYID_FLD].ToString().Trim());
					objObject.CurrencyID = int.Parse(odrPCS[PO_InvoiceMasterTable.CURRENCYID_FLD].ToString().Trim());

					if(!odrPCS[PO_InvoiceMasterTable.BLDATE_FLD].Equals(DBNull.Value))
					{
						objObject.BLDate = DateTime.Parse(odrPCS[PO_InvoiceMasterTable.BLDATE_FLD].ToString().Trim());
					}
					else
					{
						objObject.BLDate = DateTime.MinValue;
					}
					
					if(!odrPCS[PO_InvoiceMasterTable.INFORMDATE_FLD].Equals(DBNull.Value))
					{
						objObject.InformDate = DateTime.Parse(odrPCS[PO_InvoiceMasterTable.INFORMDATE_FLD].ToString().Trim());
					}
					else
					{
						objObject.InformDate = DateTime.MinValue;
					}
					
					if(!odrPCS[PO_InvoiceMasterTable.DECLARATIONDATE_FLD].Equals(DBNull.Value))
					{
						objObject.DeclarationDate = DateTime.Parse(odrPCS[PO_InvoiceMasterTable.DECLARATIONDATE_FLD].ToString().Trim());
					}
					else
					{
						objObject.DeclarationDate = DateTime.MinValue;
					}
					
					if(!odrPCS[PO_InvoiceMasterTable.CARRIERID_FLD].Equals(DBNull.Value))
					{
						objObject.CarrierID = int.Parse(odrPCS[PO_InvoiceMasterTable.CARRIERID_FLD].ToString().Trim());
					}
					
					if(!odrPCS[PO_InvoiceMasterTable.PAYMENTTERMID_FLD].Equals(DBNull.Value))
					{
						objObject.PaymentTermID = int.Parse(odrPCS[PO_InvoiceMasterTable.PAYMENTTERMID_FLD].ToString().Trim());
					}

					if(!odrPCS[PO_InvoiceMasterTable.DELIVERYTERMID_FLD].Equals(DBNull.Value))
					{
						objObject.DeliveryTermID = int.Parse(odrPCS[PO_InvoiceMasterTable.DELIVERYTERMID_FLD].ToString().Trim());
					}					

					objObject.BLNumber = odrPCS[PO_InvoiceMasterTable.BLNUMBER_FLD].ToString().Trim();
					objObject.TaxInformNumber = odrPCS[PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD].ToString().Trim();
					objObject.TaxDeclarationNumber = odrPCS[PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD].ToString().Trim();

					objObject.TotalInlandAmount = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD].ToString().Trim());
					objObject.TotalCIPAmount = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD].ToString().Trim());
					objObject.TotalCIFAmount = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD].ToString().Trim());
					objObject.TotalImportTax = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD].ToString().Trim());
					objObject.TotalBeforeVATAmount = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD].ToString().Trim());
					objObject.TotalVATAmount = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD].ToString().Trim());
					
					

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
		///       This method uses to get data from PO_InvoiceMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_InvoiceMasterVO
		///    </Outputs>
		///    <Returns>
		///       PO_InvoiceMasterVO
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
		

		public object GetObjectVO(string pstrInvoiceNo)
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
					+ PO_InvoiceMasterTable.INVOICEMASTERID_FLD + ","
					+ PO_InvoiceMasterTable.INVOICENO_FLD + ","
					+ PO_InvoiceMasterTable.POSTDATE_FLD + ","
					+ PO_InvoiceMasterTable.EXCHANGERATE_FLD + ","
					+ PO_InvoiceMasterTable.BLDATE_FLD + ","
					+ PO_InvoiceMasterTable.INFORMDATE_FLD + ","
					+ PO_InvoiceMasterTable.DECLARATIONDATE_FLD + ","
					+ PO_InvoiceMasterTable.BLNUMBER_FLD + ","
					+ PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD + ","
					+ PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD + ","
					+ PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD + ","
					+ PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD + ","
					+ PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD + ","
					+ PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD + ","
					+ PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD + ","
					+ PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD + ","
					+ PO_InvoiceMasterTable.CCNID_FLD + ","
					+ PO_InvoiceMasterTable.PARTYID_FLD + ","
					+ PO_InvoiceMasterTable.CURRENCYID_FLD + ","
					+ PO_InvoiceMasterTable.CARRIERID_FLD + ","
					+ PO_InvoiceMasterTable.PAYMENTTERMID_FLD + ","
					+ PO_InvoiceMasterTable.DELIVERYTERMID_FLD
					+ " FROM " + PO_InvoiceMasterTable.TABLE_NAME
					+ " WHERE " + PO_InvoiceMasterTable.INVOICENO_FLD + "='" + pstrInvoiceNo.Replace("'", "''") + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_InvoiceMasterVO objObject = new PO_InvoiceMasterVO();

				if (odrPCS.Read())
				{ 
					objObject.InvoiceMasterID = int.Parse(odrPCS[PO_InvoiceMasterTable.INVOICEMASTERID_FLD].ToString().Trim());
					objObject.InvoiceNo = odrPCS[PO_InvoiceMasterTable.INVOICENO_FLD].ToString().Trim();
					objObject.PostDate = DateTime.Parse(odrPCS[PO_InvoiceMasterTable.POSTDATE_FLD].ToString().Trim());
					objObject.ExchangeRate = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.EXCHANGERATE_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[PO_InvoiceMasterTable.CCNID_FLD].ToString().Trim());
					objObject.PartyID = int.Parse(odrPCS[PO_InvoiceMasterTable.PARTYID_FLD].ToString().Trim());
					objObject.CurrencyID = int.Parse(odrPCS[PO_InvoiceMasterTable.CURRENCYID_FLD].ToString().Trim());

					if(!odrPCS[PO_InvoiceMasterTable.BLDATE_FLD].Equals(DBNull.Value))
					{
						objObject.BLDate = DateTime.Parse(odrPCS[PO_InvoiceMasterTable.BLDATE_FLD].ToString().Trim());
					}
					else
					{
						objObject.BLDate = DateTime.MinValue;
					}
					
					if(!odrPCS[PO_InvoiceMasterTable.INFORMDATE_FLD].Equals(DBNull.Value))
					{
						objObject.InformDate = DateTime.Parse(odrPCS[PO_InvoiceMasterTable.INFORMDATE_FLD].ToString().Trim());
					}
					else
					{
						objObject.InformDate = DateTime.MinValue;
					}
					
					if(!odrPCS[PO_InvoiceMasterTable.DECLARATIONDATE_FLD].Equals(DBNull.Value))
					{
						objObject.DeclarationDate = DateTime.Parse(odrPCS[PO_InvoiceMasterTable.DECLARATIONDATE_FLD].ToString().Trim());
					}
					else
					{
						objObject.DeclarationDate = DateTime.MinValue;
					}
					
					if(!odrPCS[PO_InvoiceMasterTable.CARRIERID_FLD].Equals(DBNull.Value))
					{
						objObject.CarrierID = int.Parse(odrPCS[PO_InvoiceMasterTable.CARRIERID_FLD].ToString().Trim());
					}
					
					if(!odrPCS[PO_InvoiceMasterTable.PAYMENTTERMID_FLD].Equals(DBNull.Value))
					{
						objObject.PaymentTermID = int.Parse(odrPCS[PO_InvoiceMasterTable.PAYMENTTERMID_FLD].ToString().Trim());
					}

					if(!odrPCS[PO_InvoiceMasterTable.DELIVERYTERMID_FLD].Equals(DBNull.Value))
					{
						objObject.DeliveryTermID = int.Parse(odrPCS[PO_InvoiceMasterTable.DELIVERYTERMID_FLD].ToString().Trim());
					}					

					objObject.BLNumber = odrPCS[PO_InvoiceMasterTable.BLNUMBER_FLD].ToString().Trim();
					objObject.TaxInformNumber = odrPCS[PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD].ToString().Trim();
					objObject.TaxDeclarationNumber = odrPCS[PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD].ToString().Trim();

					objObject.TotalInlandAmount = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD].ToString().Trim());
					objObject.TotalCIPAmount = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD].ToString().Trim());
					objObject.TotalCIFAmount = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD].ToString().Trim());
					objObject.TotalImportTax = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD].ToString().Trim());
					objObject.TotalBeforeVATAmount = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD].ToString().Trim());
					objObject.TotalVATAmount = Decimal.Parse(odrPCS[PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD].ToString().Trim());

					return objObject;
				}
				else
				{
					return null;
				}
				
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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

		public DateTime GetEarliestApprovedDate(string pstrPODetailIDs)
		{
			const string METHOD_NAME = THIS + ".GetEarliestApprovedDate()";
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				string strSql = "SELECT TOP 1 ApprovalDate";
				strSql += " FROM PO_PurchaseOrderDetail";
				strSql += " WHERE PurchaseOrderDetailID IN (" + pstrPODetailIDs + ")";
				strSql += " ORDER BY ApprovalDate ASC";
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				object objReturnValue = ocmdPCS.ExecuteScalar();

				if((objReturnValue == null) || (objReturnValue == DBNull.Value))
				{
					return DateTime.MinValue;
				}
				else
				{
					DateTime dtmTemp = (DateTime)objReturnValue;

					//Truncate hour, minute, second before return
					return (new DateTime(dtmTemp.Year, dtmTemp.Month, dtmTemp.Day));
				}
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to update data to PO_InvoiceMaster
		///    </Description>
		///    <Inputs>
		///       PO_InvoiceMasterVO       
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

			PO_InvoiceMasterVO voInvoiceMaster = (PO_InvoiceMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				string strSql = "UPDATE " + PO_InvoiceMasterTable.TABLE_NAME + " SET ";
				strSql += PO_InvoiceMasterTable.INVOICENO_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.POSTDATE_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.EXCHANGERATE_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.BLDATE_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.INFORMDATE_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.DECLARATIONDATE_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.BLNUMBER_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.CCNID_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.PARTYID_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.CURRENCYID_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.CARRIERID_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.PAYMENTTERMID_FLD + "= ?, ";
				strSql += PO_InvoiceMasterTable.DELIVERYTERMID_FLD + "= ?";
				strSql += " WHERE " + PO_InvoiceMasterTable.INVOICEMASTERID_FLD + "= ?";
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.INVOICENO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.INVOICENO_FLD].Value = voInvoiceMaster.InvoiceNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.POSTDATE_FLD].Value = voInvoiceMaster.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.EXCHANGERATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.EXCHANGERATE_FLD].Value = voInvoiceMaster.ExchangeRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.BLDATE_FLD, OleDbType.Date));
				if(voInvoiceMaster.BLDate!= DateTime.MinValue)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.BLDATE_FLD].Value = voInvoiceMaster.BLDate;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.BLDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.INFORMDATE_FLD, OleDbType.Date));
				if(voInvoiceMaster.InformDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.INFORMDATE_FLD].Value = voInvoiceMaster.InformDate;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.INFORMDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.DECLARATIONDATE_FLD, OleDbType.Date));
				if(voInvoiceMaster.DeclarationDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.DECLARATIONDATE_FLD].Value = voInvoiceMaster.DeclarationDate;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.DECLARATIONDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.BLNUMBER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.BLNUMBER_FLD].Value = voInvoiceMaster.BLNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD].Value = voInvoiceMaster.TaxInformNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD].Value = voInvoiceMaster.TaxDeclarationNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD].Value = voInvoiceMaster.TotalInlandAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD].Value = voInvoiceMaster.TotalCIPAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD].Value = voInvoiceMaster.TotalCIFAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD].Value = voInvoiceMaster.TotalImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD].Value = voInvoiceMaster.TotalBeforeVATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD].Value = voInvoiceMaster.TotalVATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.CCNID_FLD].Value = voInvoiceMaster.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.PARTYID_FLD].Value = voInvoiceMaster.PartyID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.CURRENCYID_FLD].Value = voInvoiceMaster.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.CARRIERID_FLD, OleDbType.Integer));
				if(voInvoiceMaster.CarrierID > 0)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.CARRIERID_FLD].Value = voInvoiceMaster.CarrierID;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.CARRIERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.PAYMENTTERMID_FLD, OleDbType.Integer));
				if(voInvoiceMaster.PaymentTermID > 0)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.PAYMENTTERMID_FLD].Value = voInvoiceMaster.PaymentTermID;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.PAYMENTTERMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.DELIVERYTERMID_FLD, OleDbType.Integer));
				if(voInvoiceMaster.DeliveryTermID > 0)
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.DELIVERYTERMID_FLD].Value = voInvoiceMaster.DeliveryTermID;
				}
				else
				{
					ocmdPCS.Parameters[PO_InvoiceMasterTable.DELIVERYTERMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_InvoiceMasterTable.INVOICEMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_InvoiceMasterTable.INVOICEMASTERID_FLD].Value = voInvoiceMaster.InvoiceMasterID;
								
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[0].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
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
		///       This method uses to get all data from PO_InvoiceMaster
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
				+ PO_InvoiceMasterTable.INVOICEMASTERID_FLD + ","
				+ PO_InvoiceMasterTable.INVOICENO_FLD + ","
				+ PO_InvoiceMasterTable.POSTDATE_FLD + ","
				+ PO_InvoiceMasterTable.EXCHANGERATE_FLD + ","
				+ PO_InvoiceMasterTable.BLDATE_FLD + ","
				+ PO_InvoiceMasterTable.INFORMDATE_FLD + ","
				+ PO_InvoiceMasterTable.DECLARATIONDATE_FLD + ","
				+ PO_InvoiceMasterTable.BLNUMBER_FLD + ","
				+ PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD + ","
				+ PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD + ","
				+ PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD + ","
				+ PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.CCNID_FLD + ","
				+ PO_InvoiceMasterTable.PARTYID_FLD + ","
				+ PO_InvoiceMasterTable.CURRENCYID_FLD + ","
				+ PO_InvoiceMasterTable.CARRIERID_FLD + ","
				+ PO_InvoiceMasterTable.PAYMENTTERMID_FLD + ","
				+ PO_InvoiceMasterTable.DELIVERYTERMID_FLD
					+ " FROM " + PO_InvoiceMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PO_InvoiceMasterTable.TABLE_NAME);

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
				+ PO_InvoiceMasterTable.INVOICEMASTERID_FLD + ","
				+ PO_InvoiceMasterTable.INVOICENO_FLD + ","
				+ PO_InvoiceMasterTable.POSTDATE_FLD + ","
				+ PO_InvoiceMasterTable.EXCHANGERATE_FLD + ","
				+ PO_InvoiceMasterTable.BLDATE_FLD + ","
				+ PO_InvoiceMasterTable.INFORMDATE_FLD + ","
				+ PO_InvoiceMasterTable.DECLARATIONDATE_FLD + ","
				+ PO_InvoiceMasterTable.BLNUMBER_FLD + ","
				+ PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD + ","
				+ PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD + ","
				+ PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD + ","
				+ PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD + ","
				+ PO_InvoiceMasterTable.CCNID_FLD + ","
				+ PO_InvoiceMasterTable.PARTYID_FLD + ","
				+ PO_InvoiceMasterTable.CURRENCYID_FLD + ","
				+ PO_InvoiceMasterTable.CARRIERID_FLD + ","
				+ PO_InvoiceMasterTable.PAYMENTTERMID_FLD + ","
				+ PO_InvoiceMasterTable.DELIVERYTERMID_FLD 
		+ "  FROM " + PO_InvoiceMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PO_InvoiceMasterTable.TABLE_NAME);

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
