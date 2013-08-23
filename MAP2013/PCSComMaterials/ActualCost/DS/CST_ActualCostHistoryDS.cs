using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComMaterials.ActualCost.DS
{
	public class CST_ActualCostHistoryDS 
	{
		public CST_ActualCostHistoryDS()
		{
		}
		private const string THIS = "PCSComMaterials.ActualCost.DS.CST_ActualCostHistoryDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to CST_ActualCostHistory
		///    </Description>
		///    <Inputs>
		///        CST_ActualCostHistoryVO       
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
		///       Monday, February 27, 2006
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
				CST_ActualCostHistoryVO objObject = (CST_ActualCostHistoryVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO CST_ActualCostHistory("
				+ CST_ActualCostHistoryTable.ACTUALCOST_FLD + ","
				+ CST_ActualCostHistoryTable.STDCOST_FLD + ","
				+ CST_ActualCostHistoryTable.PRODUCTID_FLD + ","
				+ CST_ActualCostHistoryTable.COSTELEMENTID_FLD + ","
				+ CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD + ")"
				+ "VALUES(?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActualCostHistoryTable.ACTUALCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_ActualCostHistoryTable.ACTUALCOST_FLD].Value = objObject.ActualCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActualCostHistoryTable.STDCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_ActualCostHistoryTable.STDCOST_FLD].Value = objObject.StdCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActualCostHistoryTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActualCostHistoryTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActualCostHistoryTable.COSTELEMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActualCostHistoryTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD].Value = objObject.ActCostAllocationMasterID;


				
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
		///       This method uses to delete data from CST_ActualCostHistory
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
			strSql=	"DELETE " + CST_ActualCostHistoryTable.TABLE_NAME + " WHERE  " + CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD + "=" + pintID.ToString();
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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from CST_ActualCostHistory
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       CST_ActualCostHistoryVO
		///    </Outputs>
		///    <Returns>
		///       CST_ActualCostHistoryVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Monday, February 27, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ CST_ActualCostHistoryTable.ACTUALCOSTHISTORY_FLD + ","
					+ CST_ActualCostHistoryTable.ACTUALCOST_FLD + ","
					+ CST_ActualCostHistoryTable.STDCOST_FLD + ","
					+ CST_ActualCostHistoryTable.PRODUCTID_FLD + ","
					+ CST_ActualCostHistoryTable.COSTELEMENTID_FLD + ","
					+ CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD
					+ " FROM " + CST_ActualCostHistoryTable.TABLE_NAME
					+ " WHERE " + CST_ActualCostHistoryTable.ACTUALCOSTHISTORY_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				CST_ActualCostHistoryVO objObject = new CST_ActualCostHistoryVO();

				while (odrPCS.Read())
				{ 
				objObject.ActualCostHistory = int.Parse(odrPCS[CST_ActualCostHistoryTable.ACTUALCOSTHISTORY_FLD].ToString().Trim());
				objObject.ActualCost = Decimal.Parse(odrPCS[CST_ActualCostHistoryTable.ACTUALCOST_FLD].ToString().Trim());
				objObject.StdCost = Decimal.Parse(odrPCS[CST_ActualCostHistoryTable.STDCOST_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[CST_ActualCostHistoryTable.PRODUCTID_FLD].ToString().Trim());
				objObject.CostElementID = int.Parse(odrPCS[CST_ActualCostHistoryTable.COSTELEMENTID_FLD].ToString().Trim());
				objObject.ActCostAllocationMasterID = int.Parse(odrPCS[CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD].ToString().Trim());

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
		///       This method uses to update data to CST_ActualCostHistory
		///    </Description>
		///    <Inputs>
		///       CST_ActualCostHistoryVO       
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

			CST_ActualCostHistoryVO objObject = (CST_ActualCostHistoryVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE CST_ActualCostHistory SET "
				+ CST_ActualCostHistoryTable.ACTUALCOST_FLD + "=   ?" + ","
				+ CST_ActualCostHistoryTable.STDCOST_FLD + "=   ?" + ","
				+ CST_ActualCostHistoryTable.PRODUCTID_FLD + "=   ?" + ","
				+ CST_ActualCostHistoryTable.COSTELEMENTID_FLD + "=   ?" + ","
				+ CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD + "=  ?"
				+" WHERE " + CST_ActualCostHistoryTable.ACTUALCOSTHISTORY_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActualCostHistoryTable.ACTUALCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_ActualCostHistoryTable.ACTUALCOST_FLD].Value = objObject.ActualCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActualCostHistoryTable.STDCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_ActualCostHistoryTable.STDCOST_FLD].Value = objObject.StdCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActualCostHistoryTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActualCostHistoryTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActualCostHistoryTable.COSTELEMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActualCostHistoryTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD].Value = objObject.ActCostAllocationMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_ActualCostHistoryTable.ACTUALCOSTHISTORY_FLD, OleDbType.BigInt));
				ocmdPCS.Parameters[CST_ActualCostHistoryTable.ACTUALCOSTHISTORY_FLD].Value = objObject.ActualCostHistory;


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
		///       This method uses to get all data from CST_ActualCostHistory
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
		///       Monday, February 27, 2006
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
					+ CST_ActualCostHistoryTable.ACTUALCOSTHISTORY_FLD + ","
					+ CST_ActualCostHistoryTable.ACTUALCOST_FLD + ","
					+ CST_ActualCostHistoryTable.STDCOST_FLD + ","
					+ CST_ActualCostHistoryTable.PRODUCTID_FLD + ","
					+ CST_ActualCostHistoryTable.COSTELEMENTID_FLD + ","
					+ CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD
					+ " FROM " + CST_ActualCostHistoryTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,CST_ActualCostHistoryTable.TABLE_NAME);

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
		///       Monday, February 27, 2006
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
					+ CST_ActualCostHistoryTable.ACTUALCOSTHISTORY_FLD + ","
					+ CST_ActualCostHistoryTable.ACTUALCOST_FLD + ","
					+ CST_ActualCostHistoryTable.STDCOST_FLD + ","
					+ CST_ActualCostHistoryTable.PRODUCTID_FLD + ","
					+ CST_ActualCostHistoryTable.COSTELEMENTID_FLD + ","
					+ CST_ActualCostHistoryTable.QUANTITY_FLD + ","
					+ CST_ActualCostHistoryTable.BEGIN_QUANTITY_FLD + ","
					+ CST_ActualCostHistoryTable.COMPONENTVALUE_FLD + ","
					+ CST_ActualCostHistoryTable.COMPONENTDSAMOUNT_FLD + ","
					+ CST_ActualCostHistoryTable.COMBEGINCOST_FLD + ","
					+ CST_ActualCostHistoryTable.WOCOMPLETIONQTY_FLD + ","
					+ CST_ActualCostHistoryTable.TRANSACTIONAMOUNT_FLD + ","
					+ CST_ActualCostHistoryTable.RECYCLEAMOUNT_FLD + ","
					+ CST_ActualCostHistoryTable.RECOVERABLEAMOUNT_FLD + ","
					+ CST_ActualCostHistoryTable.FREIGHTAMOUNT_FLD + ","
					+ CST_ActualCostHistoryTable.DSAMOUNT_FLD + ","
					+ CST_ActualCostHistoryTable.DSOKAMOUNT_FLD + ","
					+ CST_ActualCostHistoryTable.ADJUSTAMOUNT_FLD + ","
					+ CST_ActualCostHistoryTable.BEGINCOST_FLD + ","
					+ CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD
					+ "  FROM " + CST_ActualCostHistoryTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,CST_ActualCostHistoryTable.TABLE_NAME);

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
		/// Get beginning item cost (cost of previous period)
		/// </summary>
		/// <param name="pintPeriodID">Previous Period ID</param>
		/// <returns></returns>
		public DataTable List(int pintPeriodID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT CST_ActualCostHistory.*, ITM_Product.CategoryID"
					+ " FROM " + CST_ActualCostHistoryTable.TABLE_NAME
					+ " JOIN ITM_Product ON CST_ActualCostHistory.ProductID = ITM_Product.ProductID"
					+ " WHERE " + CST_ActualCostHistoryTable.ACTCOSTALLOCATIONMASTERID_FLD + "=" + pintPeriodID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,CST_ActualCostHistoryTable.TABLE_NAME);

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

		/// <summary>
		/// Gets data from PO Receipt which not by invoice transaction in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		public DataTable GetPOReceipt(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetPOReceipt()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT	PO_PurchaseOrderReceiptDetail.ProductID, ISNULL(ReceiveQuantity, 0) Quantity,"
					+ " PO_PurchaseOrderMaster.CurrencyID, ISNULL(PO_PurchaseOrderDetail.UnitPrice, 0) AS UnitPrice, PO_PurchaseOrderReceiptMaster.ReceiptType,"
					+ " PO_PurchaseOrderReceiptDetail.PurchaseOrderDetailID, PO_PurchaseOrderMaster.ExchangeRate, MST_Bin.BinTypeID"
					+ " FROM PO_PurchaseOrderReceiptDetail JOIN PO_PurchaseOrderReceiptMaster"
					+ " ON PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID"
					+ " JOIN PO_PurchaseOrderMaster ON PO_PurchaseOrderReceiptDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
					+ " JOIN PO_PurchaseOrderDetail ON PO_PurchaseOrderReceiptDetail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
					+ " JOIN MST_BIN ON PO_PurchaseOrderReceiptDetail.BinID = MST_Bin.BinID"
					+ " WHERE PO_PurchaseOrderReceiptMaster.CCNID = " + pintCCNID
					+ " AND PO_PurchaseOrderReceiptMaster.PostDate >= ?"
					+ " AND PO_PurchaseOrderReceiptMaster.PostDate <= ?"
					+ " AND PO_PurchaseOrderReceiptMaster.ReceiptType IN (" + (int)POReceiptTypeEnum.ByDeliverySlip + "," + (int)POReceiptTypeEnum.ByOutside+ ")"
					+ " ORDER BY PO_PurchaseOrderReceiptDetail.ProductID";
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

		/// <summary>
		/// Gets data from PO Receipt by invoice transaction in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		public DataTable GetPOReceiptByInvoice(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetPOReceipt()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT	PO_PurchaseOrderReceiptDetail.ProductID, ISNULL(ReceiveQuantity, 0) Quantity,"
					+ " ((ISNULL(PO_InvoiceDetail.ImportTaxAmount, 0) + ISNULL(PO_InvoiceDetail.CIPAmount, 0))  "
					+ " * (SELECT ExchangeRate FROM PO_InvoiceMaster WHERE InvoiceMasterID = PO_PurchaseOrderReceiptMaster.InvoiceMasterID)) AS CIPAmount"
					+ " , PO_InvoiceDetail.InvoiceQuantity,"
					+ " PO_PurchaseOrderReceiptMaster.ReceiptType, PO_PurchaseOrderReceiptDetail.PurchaseOrderDetailID,"
					+ " (SELECT ExchangeRate FROM PO_InvoiceMaster WHERE InvoiceMasterID = PO_PurchaseOrderReceiptMaster.InvoiceMasterID) AS ExchangeRate,"
					+ " (SELECT CurrencyID FROM PO_InvoiceMaster WHERE InvoiceMasterID = PO_PurchaseOrderReceiptMaster.InvoiceMasterID) AS CurrencyID"
					+ " , MST_Bin.BinTypeID"
					+ " FROM PO_PurchaseOrderReceiptDetail JOIN PO_PurchaseOrderReceiptMaster"
					+ " ON PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID"
					+ " JOIN PO_InvoiceDetail ON PO_InvoiceDetail.InvoiceMasterID = PO_PurchaseOrderReceiptMaster.InvoiceMasterID"
					+ " AND PO_InvoiceDetail.ProductID = PO_PurchaseOrderReceiptDetail.ProductID"
					+ " AND PO_InvoiceDetail.DeliveryScheduleID = PO_PurchaseOrderReceiptDetail.DeliveryScheduleID"
					+ " JOIN MST_BIN ON PO_PurchaseOrderReceiptDetail.BinID = MST_Bin.BinID"
					+ " WHERE PO_PurchaseOrderReceiptMaster.CCNID = " + pintCCNID
					+ " AND PO_PurchaseOrderReceiptMaster.PostDate >= ?"
					+ " AND PO_PurchaseOrderReceiptMaster.PostDate <= ?"
					+ " AND PO_PurchaseOrderReceiptMaster.ReceiptType =" + (int)POReceiptTypeEnum.ByInvoice
					+ " ORDER BY PO_PurchaseOrderReceiptDetail.ProductID";
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

		/// <summary>
		/// Get work order completed quantity in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		public DataTable GetCompletionQuantity(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetCompletionQuantity()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT CompletedQuantity AS Quantity, ProductID, PRO_WorkOrderCompletion.BinID, BinTypeID"
					+ " FROM PRO_WorkOrderCompletion JOIN MST_Bin"
					+ " ON PRO_WorkOrderCompletion.BinID = MST_Bin.BinID"
					+ " WHERE CCNID = " + pintCCNID
					+ " AND PostDate >= ?"
					+ " AND PostDate <= ?"
					+ " ORDER BY ProductID";
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

		public DataTable GetCompletionQuantityForComponent(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetCompletionQuantityForComponent()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT Quantity, ProductID, BinID, MST_TranType.Code"
					+ " FROM MST_TransactionHistory JOIN MST_TranType"
					+ " ON MST_TransactionHistory.TranTypeID = MST_TranType.TranTypeID"
					+ " WHERE Quantity < 0"
					+ " AND MST_TranType.Code IN ('" + TransactionTypeEnum.PROWorkOrderCompletion.ToString() + "',"
					+ " '" + TransactionTypeEnum.POPurchaseOrderReceipts.ToString() + "')"
					+ " AND PostDate >= ?"
					+ " AND PostDate <= ?"
					+ " ORDER BY ProductID";
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

		/// <summary>
		/// Get work order bom of CCN
		/// </summary>
		/// <param name="pintCCNID">CCN ID</param>
		/// <returns></returns>
		public DataTable GetWOBOM(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetWOBOM()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			DataSet dstPCS = null;
			try
			{
				string strSql = "SELECT DISTINCT PRO_WorkOrderDetail.ProductID AS ParentID, PRO_WorkOrderBOMDetail.ComponentID AS ProductID, RequiredQuantity,"
					+ " ITM_Product.Code, ITM_Product.Description, ITM_Product.Revision, MakeItem,"
					+ " ISNULL(ITM_Product.CostCenterRateMasterID, 0) CostCenterRateMasterID"
					+ " FROM PRO_WorkOrderDetail JOIN PRO_WorkOrderBOMMaster"
					+ " ON PRO_WorkOrderDetail.WorkOrderDetailID = PRO_WorkOrderBOMMaster.WorkOrderDetailID"
					+ " JOIN PRO_WorkOrderBOMDetail"
					+ " ON PRO_WorkOrderBOMMaster.WorkOrderBomMasterID = PRO_WorkOrderBOMDetail.WorkOrderBomMasterID"
					+ " JOIN ITM_Product ON PRO_WorkOrderBOMDetail.ComponentID = ITM_Product.ProductID"
					+ " WHERE ITM_Product.CCNID = " + pintCCNID
					+ " ORDER BY PRO_WorkOrderDetail.ProductID, PRO_WorkOrderBOMDetail.ComponentID";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				dstPCS = new DataSet();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_WorkOrderBomDetailTable.TABLE_NAME);

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
		/// <summary>
		/// Check if period is rollup or not
		/// </summary>
		/// <param name="pintPeriodID">PeriodID</param>
		/// <returns>True: already roll, False if failure</returns>
		public bool IsRollup(int pintPeriodID)
		{
			const string METHOD_NAME = THIS + ".IsRollup()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT COUNT(ActCostAllocationMasterID) FROM CST_ActualCostHistory"
					+ " WHERE ActCostAllocationMasterID = " + pintPeriodID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objResult = ocmdPCS.ExecuteScalar();
				try
				{
					if (Convert.ToInt32(objResult) > 0)
						return true;
					else
						return false;
				}
				catch
				{
					return false;
				}
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
		/// Find the last period which have item cost to use as beginning cost of current period
		/// </summary>
		/// <param name="pdtmFromDate">From Date of Current Period</param>
		/// <returns>period which have item cost</returns>
		public DataTable FindLastPeriodHasCost(DateTime pdtmFromDate)
		{
			const string METHOD_NAME = THIS + ".FindLastPeriodHasCost()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT SUM(ActualCost) AS ActualCost, ProductID,"
					+ " CST_ActualCostHistory.ActCostAllocationMasterID, cst_ActCostAllocationMaster.FromDate"
					+ " FROM CST_ActualCostHistory"
					+ " JOIN cst_ActCostAllocationMaster"
					+ " ON CST_ActualCostHistory.ActCostAllocationMasterID = cst_ActCostAllocationMaster.ActCostAllocationMasterID"
					+ " WHERE cst_ActCostAllocationMaster.FromDate < ?"
					+ " GROUP BY ProductID, CST_ActualCostHistory.ActCostAllocationMasterID, cst_ActCostAllocationMaster.FromDate"
					+ " HAVING SUM(ActualCost) > 0"
					+ " ORDER BY cst_ActCostAllocationMaster.FromDate DESC";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				int intPeriodID = 0;
				if (dtbData.Rows.Count > 0)
				{
					try
					{
						intPeriodID = Convert.ToInt32(dtbData.Rows[0]["ActCostAllocationMasterID"]);
					}
					catch{}
				}
				// if found and period which has data then return its actual cost
				if (intPeriodID > 0)
				{
					DataTable dtbResult = new DataTable("CST_ActualCostHistory");
					strSql = "SELECT ActualCost, StdCost, ProductID, CostElementID, Quantity FROM CST_ActualCostHistory"
						+ " WHERE ActCostAllocationMasterID = " + intPeriodID;
					ocmdPCS = new OleDbCommand(strSql, oconPCS);
					odadPCS = new OleDbDataAdapter(ocmdPCS);
					odadPCS.Fill(dtbResult);
					return dtbResult;
				}
				else 
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
		/// <summary>
		/// Gets return goods receipt transaction in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>DataTable</returns>
		public DataTable GetReturnGoodsReceipt(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetReturnGoodsReceipt()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT ReceiveQuantity, ProductID, UnitPrice, BinID, LocationID, SO_ReturnedGoodsDetail.MasterLocationID"
					+ " FROM SO_ReturnedGoodsDetail JOIN SO_ReturnedGoodsMaster"
					+ " ON SO_ReturnedGoodsDetail.ReturnedGoodsMasterID = SO_ReturnedGoodsMaster.ReturnedGoodsMasterID"
					+ " WHERE SO_ReturnedGoodsMaster.CCNID = " + pintCCNID
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
		public DataTable GetShipTransaction(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetShipTransaction()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT ISNULL(InvoiceQty,0) AS InvoiceQty, SO_ConfirmShipDetail.ProductID,"
					+ " ISNULL(ITM_Product.CategoryID,0) CategoryID, ITM_Product.MakeItem"
					+ " FROM SO_ConfirmShipDetail JOIN SO_ConfirmShipMaster"
					+ " ON SO_ConfirmShipDetail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID"
					+ " JOIN ITM_Product ON SO_ConfirmShipDetail.ProductID = ITM_Product.ProductID"
					+ " WHERE SO_ConfirmShipMaster.CCNID = " + pintCCNID
					+ " AND ShippedDate >= ?"
					+ " AND ShippedDate <= ?"
					+ " ORDER BY SO_ConfirmShipDetail.ProductID";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;

				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable(SO_ConfirmShipMasterTable.TABLE_NAME);
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

		public DataTable GetShipAdjustment(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetShipAdjustment()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT Quantity, ProductID, BinID, MST_TransactionHistory.TranTypeID"
					+ " FROM MST_TransactionHistory JOIN MST_TranType"
					+ " ON MST_TransactionHistory.TranTypeID = MST_TranType.TranTypeID"
					+ " WHERE MST_TranType.Code = '" + TransactionTypeEnum.ShippingAdjustment.ToString() + "'"  // 19
					+ " AND PostDate >= ?"
					+ " AND PostDate <= ?"
					+ " ORDER BY ProductID";
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

		public DataTable GetReturnToVendor(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetReturnToVendor()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT Quantity, ProductID, BinTypeID"
					+ " FROM PO_ReturnToVendorDetail JOIN PO_ReturnToVendorMaster"
					+ " ON PO_ReturnToVendorDetail.ReturnToVendorMasterID = PO_ReturnToVendorMaster.ReturnToVendorMasterID"
					+ " JOIN MST_BIN ON PO_ReturnToVendorDetail.BinID = MST_BIN.BINID"
					+ " WHERE PostDate >= ?"
					+ " AND PostDate <= ?"
					+ " ORDER BY ProductID";
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

		public DataTable GetCostingAdjustment(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetCostingAdjustment()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT ACAdjustmentDetail, cst_ACAdjustmentDetail.ACAdjustmentMasterID, CostElementID, Cost"
					+ " FROM cst_ACAdjustmentDetail JOIN cst_ACAdjustmentMaster"
					+ " ON cst_ACAdjustmentDetail.ACAdjustmentMasterID = cst_ACAdjustmentMaster.ACAdjustmentMasterID"
					+ " WHERE cst_ACAdjustmentMaster.CCNID = " + pintCCNID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				return dtbData;
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

		public DataTable GetComponentScrap(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetComponentScrap()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT ProductID, ComponentID, ScrapQuantity, PostDate"
					+ " FROM PRO_ComponentScrapDetail JOIN PRO_ComponentScrapMaster"
					+ " ON PRO_ComponentScrapDetail.ComponentScrapMasterID = PRO_ComponentScrapMaster.ComponentScrapMasterID"
					+ " WHERE PRO_ComponentScrapMaster.CCNID = " + pintCCNID
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
		public DataTable ListItemNotSold(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCycleID)
		{
			const string METHOD_NAME = THIS + ".ListItemNotSold()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "select CostElementID,Sum(ISNULL(RecycleAmount,0))as RecycleAmount,"
					+ " Sum(ISNULL(DSAmount,0))as DSAmount,"
					+ " Sum(ISNULL(DS_OKAmount,0))as DS_OKAmount,Sum(ISNULL(AdjustAmount,0))as AdjustAmount "
					+ " from CST_ActualCostHistory"
					+ " where ProductID  NOT IN (SELECT PRODUCTID FROM SO_ConfirmShipDetail D "
					+ " INNER JOIN SO_ConfirmShipMaster M ON M.ConfirmShipMasterID=D.ConfirmShipMasterID"
					+ " where M.ShippedDate>= ? AND M.ShippedDate<= ?)"
					+ " and ActCostAllocationMasterID= " + pintCycleID
					+ " Group by CostElementID";
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
		public DataTable ListItemCostPrice(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCycleID)
		{
			const string METHOD_NAME = THIS + ".ListItemCostPrice()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT D.ProductID,C.CostElementID,"
					+ " ((SUM(D.InvoiceQty * C.ActualCost)"
					+ " - ISNULL((SELECT SUM(D1.ReceiveQuantity*C1.ActualCost)"
					+ " FROM SO_ReturnedGoodsDetail D1 "
					+ " INNER JOIN SO_ReturnedGoodsMaster  M ON M.ReturnedGoodsMasterID=D1.ReturnedGoodsMasterID"
					+ " INNER JOIN v_UnitOfActualCost_ByCostElement AS C1 ON C1.ProductID=D1.ProductID"
					+ " WHERE M.PostDate>=C1.FromDate AND M.PostDate<=C1.ToDate+1"
					+ " AND C1.CostElementID = C.CostElementID"
					+ " AND C1.ActCostAllocationMasterID = C.ActCostAllocationMasterID"
					+ " AND D1.ProductID=D.ProductID"
					+ " GROUP BY D.ProductID),0))/CGS.TotalCGS1) * " + Math.Pow(10, 5) + " AS Rate"
					+ " FROM SO_ConfirmShipDetail D"
					+ " INNER JOIN SO_ConfirmShipMaster  M ON M.ConfirmShipMasterID=D.ConfirmShipMasterID"
					+ " INNER JOIN v_UnitOfActualCost_ByCostElement AS C ON C.ProductID=D.ProductID"
					+ " INNER JOIN v_CGS1 CGS ON CGS.ActCostAllocationMasterID=C.ActCostAllocationMasterID AND CGS.CostElementID=C.CostElementID "
					+ " INNER JOIN v_Total_DS_Before_Allocation DS ON DS.ActCostAllocationMasterID=C.ActCostAllocationMasterID AND DS.CostElementID=C.CostElementID"
					+ " WHERE M.ShippedDate>=C.FromDate and M.ShippedDate<=C.ToDate+1"
					+ " AND CGS.TotalCGS1<>0"
					+ " AND C.ActCostAllocationMasterID=" + pintCycleID
					+ " GROUP BY d.ProductID,C.CostElementID,c.ActCostAllocationMasterID,CGS.TotalCGS1";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				return dtbData;
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
		public DataTable ChargeOHAmount(int pintCycleID)
		{
			const string METHOD_NAME = THIS + ".ChargeOHAmount()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = " Select D.ProductID,I.Code as PartNo,I.Description as PartName,I.Revision as Model,I.CategoryID,CAT.Code as Category,"
					+ " Sum(D.InvoiceQty*C.ActualCost)"
					+ " - ISNULL((Select Sum(D1.ReceiveQuantity*C1.ActualCost)"
					+ " from SO_ReturnedGoodsDetail D1 "
					+ " INNER JOIN SO_ReturnedGoodsMaster  M ON M.ReturnedGoodsMasterID=D1.ReturnedGoodsMasterID"
					+ " INNER JOIN v_UnitOfActualCost_ByCostElement AS C1 ON C1.ProductID=D1.ProductID"
					+ " WHERE M.PostDate>=C1.FromDate and M.PostDate<=C1.ToDate+1"
					+ " AND C1.CostElementID = C.CostElementID"
					+ " AND C1.ActCostAllocationMasterID = C.ActCostAllocationMasterID"
					+ " AND D1.ProductID=D.ProductID"
					+ " Group by D.ProductID),0)"
					+ " AS CGS1,"
					+ " CGS.TotalCGS1,100000*(Sum(D.InvoiceQty*C.ActualCost)"
					+ " - ISNULL((Select Sum(D1.ReceiveQuantity*C1.ActualCost)"
					+ " from SO_ReturnedGoodsDetail D1 "
					+ " INNER JOIN SO_ReturnedGoodsMaster  M ON M.ReturnedGoodsMasterID=D1.ReturnedGoodsMasterID"
					+ " INNER JOIN v_UnitOfActualCost_ByCostElement AS C1 ON C1.ProductID=D1.ProductID"
					+ " WHERE M.PostDate>=C1.FromDate and M.PostDate<=C1.ToDate+1"
					+ " AND C1.CostElementID = C.CostElementID"
					+ " AND C1.ActCostAllocationMasterID = C.ActCostAllocationMasterID"
					+ " AND D1.ProductID=D.ProductID"
					+ " Group by D.ProductID),0))/CGS.TotalCGS1 AS Rate,"
					+ " DS.DSAmount*(Sum(D.InvoiceQty*C.ActualCost)"
					+ " - ISNULL((Select Sum(D1.ReceiveQuantity*C1.ActualCost)"
					+ " from SO_ReturnedGoodsDetail D1 "
					+ " INNER JOIN SO_ReturnedGoodsMaster  M ON M.ReturnedGoodsMasterID=D1.ReturnedGoodsMasterID"
					+ " INNER JOIN v_UnitOfActualCost_ByCostElement AS C1 ON C1.ProductID=D1.ProductID"
					+ " WHERE M.PostDate>=C1.FromDate and M.PostDate<=C1.ToDate+1"
					+ " AND C1.CostElementID = C.CostElementID"
					+ " AND C1.ActCostAllocationMasterID = C.ActCostAllocationMasterID"
					+ " AND D1.ProductID=D.ProductID"
					+ " Group by D.ProductID),0))"
					+ " /CGS.TotalCGS1*100000/100000 AS " + CST_DSAndRecycleAllocationTable.OH_DSAMOUNT_FLD + ","
					+ " DS.RecycleAmount*(Sum(D.InvoiceQty*C.ActualCost)"
					+ " - ISNULL((Select Sum(D1.ReceiveQuantity*C1.ActualCost)"
					+ " from SO_ReturnedGoodsDetail D1 "
					+ " INNER JOIN SO_ReturnedGoodsMaster  M ON M.ReturnedGoodsMasterID=D1.ReturnedGoodsMasterID"
					+ " INNER JOIN v_UnitOfActualCost_ByCostElement AS C1 ON C1.ProductID=D1.ProductID"
					+ " WHERE M.PostDate>=C1.FromDate and M.PostDate<=C1.ToDate+1"
					+ " AND C1.CostElementID = C.CostElementID"
					+ " AND C1.ActCostAllocationMasterID = C.ActCostAllocationMasterID"
					+ " AND D1.ProductID=D.ProductID"
					+ " Group by D.ProductID),0))"
					+ " /CGS.TotalCGS1*100000/100000 AS " + CST_DSAndRecycleAllocationTable.OH_RECYCLEAMOUNT_FLD + " ,"
					+ " DS.AdjustAmount*(Sum(D.InvoiceQty*C.ActualCost)"
					+ " - ISNULL((Select Sum(D1.ReceiveQuantity*C1.ActualCost)"
					+ " from SO_ReturnedGoodsDetail D1 "
					+ " INNER JOIN SO_ReturnedGoodsMaster  M ON M.ReturnedGoodsMasterID=D1.ReturnedGoodsMasterID"
					+ " INNER JOIN v_UnitOfActualCost_ByCostElement AS C1 ON C1.ProductID=D1.ProductID"
					+ " WHERE M.PostDate>=C1.FromDate and M.PostDate<=C1.ToDate+1"
					+ " AND C1.CostElementID = C.CostElementID"
					+ " AND C1.ActCostAllocationMasterID = C.ActCostAllocationMasterID"
					+ " AND D1.ProductID=D.ProductID"
					+ " Group by D.ProductID),0))"
					+ " /CGS.TotalCGS1*100000/100000 AS " + CST_DSAndRecycleAllocationTable.OH_ADJUSTAMOUNT_FLD + " ,"
					+ " C.CostElementID,"
					+ " C.ActCostAllocationMasterID"
					+ " from SO_ConfirmShipDetail D"
					+ " INNER JOIN ITM_Product I ON I.ProductID=D.ProductID"
					+ " LEFT JOIN ITM_Category CAT ON CAT.CategoryID=I.CategoryID"
					+ " INNER JOIN SO_ConfirmShipMaster  M ON M.ConfirmShipMasterID=D.ConfirmShipMasterID"
					+ " INNER JOIN v_UnitOfActualCost_ByCostElement AS C ON C.ProductID=D.ProductID"
					+ " INNER JOIN v_CGS1 CGS ON CGS.ActCostAllocationMasterID=C.ActCostAllocationMasterID AND CGS.CostElementID=C.CostElementID "
					+ " INNER JOIN v_Total_DS_Before_Allocation DS ON DS.ActCostAllocationMasterID=C.ActCostAllocationMasterID AND DS.CostElementID=C.CostElementID"
					+ " INNER JOIN STD_CostElement CE ON CE.CostElementID=C.CostElementID"
					+ " WHERE M.ShippedDate>=C.FromDate and M.ShippedDate<=C.ToDate+1"
					+ " AND CGS.TotalCGS1<>0"
					+ " AND C.ActCostAllocationMasterID = " + pintCycleID
					+ " group by d.ProductID,C.CostElementID, C.ActCostAllocationMasterID,CGS.TotalCGS1,DS.DSAmount,"
					+ " DS.RecycleAmount,DS.AdjustAmount,I.ProductID,I.Code,I.Revision,I.Description,I.CategoryID,CAT.Code";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				return dtbData;
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
		public DataTable GetCategorySoldInPeriod(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetCategorySoldInPeriod()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT DISTINCT ISNULL(ITM_Product.CategoryID,0) CategoryID"
					+ " FROM SO_ConfirmShipDetail JOIN SO_ConfirmShipMaster"
					+ " ON SO_ConfirmShipDetail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID"
					+ " JOIN ITM_Product ON SO_ConfirmShipDetail.ProductID = ITM_Product.ProductID"
					+ " WHERE SO_ConfirmShipMaster.CCNID = " + pintCCNID
					+ " AND ShippedDate >= ?"
					+ " AND ShippedDate <= ?"
					+ " AND ITM_Product.MakeItem = 1"
					+ " ORDER BY ITM_Product.CategoryID";
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

		public DataTable GetItemSoldInPeriod(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetItemSoldInPeriod()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT DISTINCT SO_ConfirmShipDetail.ProductID, ISNULL(CategoryID,0) CategoryID"
					+ " FROM SO_ConfirmShipDetail JOIN SO_ConfirmShipMaster"
					+ " ON SO_ConfirmShipDetail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID"
					+ " JOIN ITM_Product ON SO_ConfirmShipDetail.ProductID = ITM_Product.ProductID"
					+ " WHERE SO_ConfirmShipMaster.CCNID = " + pintCCNID
					+ " AND ShippedDate >= ?"
					+ " AND ShippedDate <= ?"
					+ " AND ITM_Product.MakeItem = 1";
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
