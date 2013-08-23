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
	public class cst_FreightDetailDS 
	{
		private const string THIS = "PCSComMaterials.ActualCost.DS.cst_FreightDetailDS";
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				cst_FreightDetailVO objObject = (cst_FreightDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO cst_FreightDetail("
				+ cst_FreightDetailTable.QUANTITY_FLD + ","
				+ cst_FreightDetailTable.UNITPRICECIF_FLD + ","
				+ cst_FreightDetailTable.AMOUNT_FLD + ","
				+ cst_FreightDetailTable.FREIGHTMASTERID_FLD + ","
				+ cst_FreightDetailTable.PRODUCTID_FLD + ","
				+ cst_FreightDetailTable.BUYINGUMID_FLD + ","
				+ cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD + ","
					// 12-05-2006 dungla: removed in order to compliable
				//+ cst_FreightDetailTable.LINE_FLD + ","
				+ cst_FreightDetailTable.VATAMOUNT_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightDetailTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.UNITPRICECIF_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightDetailTable.UNITPRICECIF_FLD].Value = objObject.UnitPriceCIF;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.AMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightDetailTable.AMOUNT_FLD].Value = objObject.Amount;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.FREIGHTMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightDetailTable.FREIGHTMASTERID_FLD].Value = objObject.FreightMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.BUYINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightDetailTable.BUYINGUMID_FLD].Value = objObject.BuyingUMID;

				// 12-05-2006 dungla: removed in order to compliable
//				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.LINE_FLD, OleDbType.Integer));
//				ocmdPCS.Parameters[cst_FreightDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD].Value = objObject.ReturnToVendorDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.VATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightDetailTable.VATAMOUNT_FLD].Value = objObject.VATAmount;


				
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
	
		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + cst_FreightDetailTable.TABLE_NAME + " WHERE  " + "FreightDetailID" + "=" + pintID.ToString();
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
				+ cst_FreightDetailTable.FREIGHTDETAILID_FLD + ","
				+ cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD + ","
				+ cst_FreightDetailTable.QUANTITY_FLD + ","
				+ cst_FreightDetailTable.UNITPRICECIF_FLD + ","
				+ cst_FreightDetailTable.AMOUNT_FLD + ","
				+ cst_FreightDetailTable.FREIGHTMASTERID_FLD + ","
				+ cst_FreightDetailTable.PRODUCTID_FLD + ","
				+ cst_FreightDetailTable.BUYINGUMID_FLD + ","
					// 12-05-2006 dungla: removed in order to compliable
				//+ cst_FreightDetailTable.LINE_FLD + ","
				+ cst_FreightDetailTable.VATAMOUNT_FLD
				+ " FROM " + cst_FreightDetailTable.TABLE_NAME
				+" WHERE " + cst_FreightDetailTable.FREIGHTDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				cst_FreightDetailVO objObject = new cst_FreightDetailVO();

				while (odrPCS.Read())
				{ 
					objObject.FreightDetailID = int.Parse(odrPCS[cst_FreightDetailTable.FREIGHTDETAILID_FLD].ToString().Trim());
					objObject.Quantity = Decimal.Parse(odrPCS[cst_FreightDetailTable.QUANTITY_FLD].ToString().Trim());
					objObject.UnitPriceCIF = Decimal.Parse(odrPCS[cst_FreightDetailTable.UNITPRICECIF_FLD].ToString().Trim());
					objObject.Amount = Decimal.Parse(odrPCS[cst_FreightDetailTable.AMOUNT_FLD].ToString().Trim());
					objObject.FreightMasterID = int.Parse(odrPCS[cst_FreightDetailTable.FREIGHTMASTERID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[cst_FreightDetailTable.PRODUCTID_FLD].ToString().Trim());
					objObject.BuyingUMID = int.Parse(odrPCS[cst_FreightDetailTable.BUYINGUMID_FLD].ToString().Trim());
					// 12-05-2006 dungla: removed in order to compliable
					//objObject.Line = int.Parse(odrPCS[cst_FreightDetailTable.LINE_FLD].ToString().Trim());
					objObject.VATAmount = Decimal.Parse(odrPCS[cst_FreightDetailTable.VATAMOUNT_FLD].ToString().Trim());
					objObject.ReturnToVendorDetailID = int.Parse(odrPCS[cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD].ToString().Trim());

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

		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			cst_FreightDetailVO objObject = (cst_FreightDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE cst_FreightDetail SET "
				+ cst_FreightDetailTable.QUANTITY_FLD + "=   ?" + ","
				+ cst_FreightDetailTable.UNITPRICECIF_FLD + "=   ?" + ","
				+ cst_FreightDetailTable.AMOUNT_FLD + "=   ?" + ","
				+ cst_FreightDetailTable.FREIGHTMASTERID_FLD + "=   ?" + ","
				+ cst_FreightDetailTable.PRODUCTID_FLD + "=   ?" + ","
				+ cst_FreightDetailTable.BUYINGUMID_FLD + "=   ?" + ","
				+ cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD + "=   ?" + ","
					// 12-05-2006 dungla: removed in order to compliable
				//+ cst_FreightDetailTable.LINE_FLD + "=   ?" + ","
				+ cst_FreightDetailTable.VATAMOUNT_FLD + "=  ?"
				+" WHERE " + cst_FreightDetailTable.FREIGHTDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightDetailTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.UNITPRICECIF_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightDetailTable.UNITPRICECIF_FLD].Value = objObject.UnitPriceCIF;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.AMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightDetailTable.AMOUNT_FLD].Value = objObject.Amount;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.FREIGHTMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightDetailTable.FREIGHTMASTERID_FLD].Value = objObject.FreightMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.BUYINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightDetailTable.BUYINGUMID_FLD].Value = objObject.BuyingUMID;

				// 12-05-2006 dungla: removed in order to compliable
//				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.LINE_FLD, OleDbType.Integer));
//				ocmdPCS.Parameters[cst_FreightDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.VATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[cst_FreightDetailTable.VATAMOUNT_FLD].Value = objObject.VATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD].Value = objObject.ReturnToVendorDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(cst_FreightDetailTable.FREIGHTDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[cst_FreightDetailTable.FREIGHTDETAILID_FLD].Value = objObject.FreightDetailID;


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
				+ cst_FreightDetailTable.FREIGHTDETAILID_FLD + ","
				+ cst_FreightDetailTable.QUANTITY_FLD + ","
				+ cst_FreightDetailTable.UNITPRICECIF_FLD + ","
				+ cst_FreightDetailTable.AMOUNT_FLD + ","
				+ cst_FreightDetailTable.FREIGHTMASTERID_FLD + ","
				+ cst_FreightDetailTable.PRODUCTID_FLD + ","
				+ cst_FreightDetailTable.BUYINGUMID_FLD + ","
				+ cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD + ","
					// 12-05-2006 dungla: removed in order to compliable
				//+ cst_FreightDetailTable.LINE_FLD + ","
				+ cst_FreightDetailTable.VATAMOUNT_FLD
					+ " FROM " + cst_FreightDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,cst_FreightDetailTable.TABLE_NAME);

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
		/// GetFreightDetailByMasterID
		/// </summary>
		/// <param name="pintFreightMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		public DataSet GetFreightDetailByMasterID(int pintFreightMasterID)
		{
			const string METHOD_NAME = THIS + ".GetFreightDetailByMasterID()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					// 12-05-2006 dungla: removed in order to compliable
					//+ " FD." + cst_FreightDetailTable.LINE_FLD + ","
					+ " FD." + cst_FreightDetailTable.FREIGHTDETAILID_FLD + ","
					+ " P." + ITM_ProductTable.CODE_FLD + ","
					+ " P." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ " P." + ITM_ProductTable.REVISION_FLD + ","
					+ " UM." + MST_UnitOfMeasureTable.CODE_FLD + Constants.WHITE_SPACE + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD +  " ,"
					+ " FD." + cst_FreightDetailTable.QUANTITY_FLD + ","
					+ " FD." + cst_FreightDetailTable.UNITPRICECIF_FLD + ","
					+ " FD." + cst_FreightDetailTable.IMPORTTAXPERCENT_FLD + ","
					+ " FD." + cst_FreightDetailTable.AMOUNT_FLD + ","
					+ " FD." + cst_FreightDetailTable.FREIGHTMASTERID_FLD + ","
					+ " FD." + cst_FreightDetailTable.PRODUCTID_FLD + ","
					+ " FD." + cst_FreightDetailTable.BUYINGUMID_FLD + ","
					+ " FD." + cst_FreightDetailTable.VATAMOUNT_FLD + ", 0.0 TotalAmount, "
					+ " FD." + cst_FreightDetailTable.ADJUSTMENTID_FLD + ","
					+ " FD." + cst_FreightDetailTable.INVOICEMASTERID_FLD + ","
					+ " FD." + cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD + ","
					+ " AD." + IV_AdjustmentTable.TRANSNO_FLD + ","
					+ " IVM." + PO_InvoiceMasterTable.INVOICENO_FLD + ","
					+ " 0 PurchaseOrderReceiptID, 0 PurchaseOrderReceiptDetailID, 0 PurchaseOrderMasterID"
					+ " FROM " + cst_FreightDetailTable.TABLE_NAME + " FD "
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME + " P ON FD."
					+ cst_FreightDetailTable.PRODUCTID_FLD + " = P." + ITM_ProductTable.PRODUCTID_FLD
					+ " INNER JOIN " + MST_UnitOfMeasureTable.TABLE_NAME + " UM ON FD."
					+ cst_FreightDetailTable.BUYINGUMID_FLD + " = UM." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " LEFT JOIN " + IV_AdjustmentTable.TABLE_NAME + " AD ON AD." + IV_AdjustmentTable.ADJUSTMENTID_FLD
					+ " = FD." + cst_FreightDetailTable.ADJUSTMENTID_FLD
					+ " LEFT JOIN " + PO_InvoiceMasterTable.TABLE_NAME + " IVM ON IVM." + PO_InvoiceMasterTable.INVOICEMASTERID_FLD
					+ " = FD." + cst_FreightDetailTable.INVOICEMASTERID_FLD
					+ " WHERE FD." + cst_FreightDetailTable.FREIGHTMASTERID_FLD + " = " + pintFreightMasterID.ToString();
					// 12-05-2006 dungla: removed in order to compliable
					//+ " ORDER BY FD." + cst_FreightDetailTable.LINE_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,cst_FreightDetailTable.TABLE_NAME);

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
		/// GetReturnToVendorByMasterID
		/// </summary>
		/// <param name="pintReturnToVendorMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, July 6 2006</date>
		public DataSet GetReturnToVendorByMasterID(int pintReturnToVendorMasterID)
		{
			const string METHOD_NAME = THIS + ".GetReturnToVendorByMasterID()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ "RD.ProductID, P.Code, P.Description, RD.ReturnToVendorDetailID, "
					+ " P.Revision,RD.BuyingUMID, UM.Code MST_UnitOfMeasureCode, RD.Quantity, "
					+ " RD.UnitPrice UnitPriceCIF,0.0 ImportTax, RD.Amount,RD.VATPercent VAT, RD.VATAmount, RD.TotalAmount,0 FreightMasterID, 0 FreightDetailID, "
					+ "0.0 VAT, 0 AdjustmentID, '' TransNo, '' InvoiceNo, 0 InvoiceMasterID "
					+ " FROM PO_returnTovendorMaster RM "
					+ " inner join PO_ReturnToVendorDetail RD On RD.ReturnToVendorMasterID = RM.ReturnToVendorMasterID "
					+ " left Join ITM_Product P ON P.ProductID = RD.ProductID "
					+ " left join MST_UnitOfMeasure UM ON UM.UnitOfMeasureID = RD.BuyingUMID "
					+ " WHERE RM." + PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + " = " + pintReturnToVendorMasterID.ToString();

				strSql += " select PM.Code, IM.InvoiceNo from PO_ReturnToVendorMaster RM "
					+ " Left join dbo.PO_PurchaseOrderMaster PM ON PM.PurchaseOrderMasterID = RM.PurchaseOrderMasterID "
					+ " Left join dbo.PO_InvoiceMaster IM ON IM.InvoiceMasterID = RM.InvoiceMasterID "
					+ " where " + PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + " = " + pintReturnToVendorMasterID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_ReturnToVendorMasterTable.TABLE_NAME);

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
				+ cst_FreightDetailTable.FREIGHTDETAILID_FLD + ","
				+ cst_FreightDetailTable.QUANTITY_FLD + ","
				+ cst_FreightDetailTable.UNITPRICECIF_FLD + ","
				+ cst_FreightDetailTable.AMOUNT_FLD + ","
				+ cst_FreightDetailTable.FREIGHTMASTERID_FLD + ","
				+ cst_FreightDetailTable.PRODUCTID_FLD + ","
				+ cst_FreightDetailTable.BUYINGUMID_FLD + ","
				+ cst_FreightDetailTable.IMPORTTAXPERCENT_FLD + ","	
				+ cst_FreightDetailTable.ADJUSTMENTID_FLD + ","	
				+ cst_FreightDetailTable.INVOICEMASTERID_FLD + ","	
					// 12-05-2006 dungla: removed in order to compliable
				//+ cst_FreightDetailTable.LINE_FLD + ","
				+ cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD + ","
				+ cst_FreightDetailTable.VATAMOUNT_FLD 
				+ "  FROM " + cst_FreightDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,cst_FreightDetailTable.TABLE_NAME);

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
		/// <summary>
		/// List all freight amount in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		public DataTable ListAll(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".ListAll()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT (cst_FreightDetail.Amount * cst_FreightMaster.ExchangeRate) AS Amount,"
					+ "cst_FreightDetail.ReturnToVendorDetailID,"
					+ " cst_FreightDetail.ProductID, cst_FreightMaster.ACPurposeID, cst_FreightMaster.ACObjectID"
					+ " FROM cst_FreightDetail JOIN cst_FreightMaster"
					+ " ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID"
					+ " WHERE cst_FreightMaster.CCNID = " + pintCCNID
					+ " AND PostDate >= ?"
					+ " AND PostDate <= ?";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;

				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				return dtbData;
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
