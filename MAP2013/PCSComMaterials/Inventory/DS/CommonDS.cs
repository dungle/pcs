using System;
using System.Data;
using System.Data.OleDb;

//Using PCS's namespace
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComMaterials.Inventory.DS
{
	/// <summary>
	/// Summary description for CommonDS.
	/// </summary>
	public class CommonDS
	{
		private const string THIS = "PCSComMaterials.Inventory.DS.CommonDS";

		public CommonDS()
		{
		}
		
		/// <summary>
		/// Get Work Order Master by ID
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetWOMaster(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetWOMaster()";
			DataTable dtbPCS = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT "
					+ PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + ","
					+ PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + ","
					+ PRO_WorkOrderMasterTable.DESCRIPTION_FLD + ","
					+ PRO_WorkOrderMasterTable.CCNID_FLD + ","
					+ PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ","
					+ PRO_WorkOrderMasterTable.TRANSDATE_FLD
					+ " FROM " + PRO_WorkOrderMasterTable.TABLE_NAME
					+ " WHERE " + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS.Rows[0];
				}
				else
				{
					return null;
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
		/// Get Work Order Detail by ID
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetWODetail(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetWODetail()";
			DataTable dtbPCS = new DataTable();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT "
					+ PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD + ","
					+ PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ","
					+ PRO_WorkOrderDetailTable.LINE_FLD + ","
					+ PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD + ","
					+ PRO_WorkOrderDetailTable.DUEDATE_FLD + ","
					+ PRO_WorkOrderDetailTable.STARTDATE_FLD + ","
					+ PRO_WorkOrderDetailTable.PRODUCTID_FLD + ","
					+ PRO_WorkOrderDetailTable.PRIORITY_FLD + ","
					+ PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD + ","
					+ PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + ","
					+ PRO_WorkOrderDetailTable.AGC_FLD + ","
					+ PRO_WorkOrderDetailTable.ESTCST_FLD + ","
					+ PRO_WorkOrderDetailTable.STOCKUMID_FLD + ","
					+ PRO_WorkOrderDetailTable.FINCLOSEDATE_FLD + ","
					+ PRO_WorkOrderDetailTable.STATUS_FLD
					+ " FROM " + PRO_WorkOrderDetailTable.TABLE_NAME
					+ " WHERE " + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS.Rows[0];
				}
				else
				{
					return null;
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
		/// Get Work Order Detail by ID
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataTable GetWODetailByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetWODetail()";
			DataTable dtbPCS = new DataTable();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT "
					+ PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD + ","
					+ PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ","
					+ PRO_WorkOrderDetailTable.LINE_FLD + ","
					+ PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD + ","
					+ PRO_WorkOrderDetailTable.DUEDATE_FLD + ","
					+ PRO_WorkOrderDetailTable.STARTDATE_FLD + ","
					+ PRO_WorkOrderDetailTable.PRODUCTID_FLD + ","
					+ PRO_WorkOrderDetailTable.PRIORITY_FLD + ","
					+ PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD + ","
					+ PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + ","
					+ PRO_WorkOrderDetailTable.AGC_FLD + ","
					+ PRO_WorkOrderDetailTable.ESTCST_FLD + ","
					+ PRO_WorkOrderDetailTable.STOCKUMID_FLD + ","
					+ PRO_WorkOrderDetailTable.FINCLOSEDATE_FLD + ","
					+ PRO_WorkOrderDetailTable.STATUS_FLD
					+ " FROM " + PRO_WorkOrderDetailTable.TABLE_NAME
					+ " WHERE " + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + "=" + pintMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS;
				}
				else
				{
					return null;
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
		/// Get return to vendor detail information
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetPOReturnToVendorDetail(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetPOReturnToVendorDetail()";
			DataTable dtbPCS = new DataTable();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT "
					+ PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD + ","
					+ PO_ReturnToVendorDetailTable.LINE_FLD + ","
					+ PO_ReturnToVendorDetailTable.QUANTITY_FLD + ","
					+ PO_ReturnToVendorDetailTable.LOT_FLD + ","
					+ PO_ReturnToVendorDetailTable.SERIAL_FLD + ","
					+ PO_ReturnToVendorDetailTable.STOCKUMID_FLD + ","
					+ PO_ReturnToVendorDetailTable.BUYINGUMID_FLD + ","
					+ PO_ReturnToVendorDetailTable.MRB_FLD + ","
					+ PO_ReturnToVendorDetailTable.LOCATIONID_FLD + ","
					+ PO_ReturnToVendorDetailTable.BINID_FLD + ","
					+ PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + ","
					+ PO_ReturnToVendorDetailTable.PRODUCTID_FLD + ","
					+ PO_ReturnToVendorDetailTable.UMRATE_FLD
					+ " FROM " + PO_ReturnToVendorDetailTable.TABLE_NAME
					+ " WHERE " + PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD + "=" + pintID;					

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS.Rows[0];
				}
				else
				{
					return null;
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
		/// Get return to vendor detail information
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataTable GetPOReturnToVendorDetailByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetPOReturnToVendorDetail()";
			DataTable dtbPCS = new DataTable();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT "
					+ PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD + ","
					+ PO_ReturnToVendorDetailTable.LINE_FLD + ","
					+ PO_ReturnToVendorDetailTable.QUANTITY_FLD + ","
					+ PO_ReturnToVendorDetailTable.LOT_FLD + ","
					+ PO_ReturnToVendorDetailTable.SERIAL_FLD + ","
					+ PO_ReturnToVendorDetailTable.STOCKUMID_FLD + ","
					+ PO_ReturnToVendorDetailTable.BUYINGUMID_FLD + ","
					+ PO_ReturnToVendorDetailTable.MRB_FLD + ","
					+ PO_ReturnToVendorDetailTable.LOCATIONID_FLD + ","
					+ PO_ReturnToVendorDetailTable.BINID_FLD + ","
					+ PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + ","
					+ PO_ReturnToVendorDetailTable.PRODUCTID_FLD + ","
					+ PO_ReturnToVendorDetailTable.UMRATE_FLD
					+ " FROM " + PO_ReturnToVendorDetailTable.TABLE_NAME
					+ " WHERE " + PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + "=" + pintMasterID;					

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS;
				}
				else
				{
					return null;
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
		/// Get POReturn to vendor master information
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetPOReturnToVendorMaster(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetPOReturnToVendorMaster()";
			DataTable dtbPCS = new DataTable();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT "
					+ PO_ReturnToVendorMasterTable.PARTYID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.POSTDATE_FLD + ","
					+ PO_ReturnToVendorMasterTable.RTVNO_FLD + ","
					+ PO_ReturnToVendorMasterTable.CCNID_FLD 

					#region  DEL Trada 12-12-2005

					//+ PO_ReturnToVendorMasterTable.SHIPFORMLOCID_FLD

					#endregion	

					+ " FROM " + PO_ReturnToVendorMasterTable.TABLE_NAME
					+ " WHERE " + PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS.Rows[0];
				}
				else
				{
					return null;
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
		/// Get Sale Order commit detail information
		/// </summary>
		/// <param name="pintCCN"></param>
		/// <param name="pintMasLoc"></param>
		/// <param name="pintProductId"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetSOCommitDetail(int pintCCN, int pintMasLoc, int pintProductId)
		{
			const string METHOD_NAME = THIS + ".GetSOCommitDetail()";
			DataTable dtbPCS = new DataTable();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPDATE_FLD + ","
					+ SO_CommitInventoryDetailTable.CCNID_FLD + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryDetailTable.CCNID_FLD + "=" + pintCCN
					+ " AND " + SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + "=" + pintMasLoc
					+ " AND " + SO_CommitInventoryDetailTable.PRODUCTID_FLD + "=" + pintProductId;	

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS.Rows[0];
				}
				else
				{
					return null;
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
		/// Get Sale Order commit detail information
		/// </summary>
		/// <param name="pintCCN"></param>
		/// <param name="pintMasLoc"></param>
		/// <param name="pintProductId"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataTable GetSOCommitDetailByMaster(int pintMasterId)
		{
			const string METHOD_NAME = THIS + ".GetSOCommitDetailByMaster()";
			DataTable dtbPCS = new DataTable();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPDATE_FLD + ","
					+ SO_CommitInventoryDetailTable.CCNID_FLD + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + "=" + pintMasterId;	

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS;
				}
				else
				{
					return null;
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
		/// Get SO Return Goods Detail by Id
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetSOReturnGoodsDetail(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetSOReturnGoodsDetail()";
			DataTable dtbPCS = new DataTable();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITPRICE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.BINID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.QASTATUS_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOT_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LINE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.SERIAL_FLD
					+ " FROM " + SO_ReturnedGoodsDetailTable.TABLE_NAME
					+ " WHERE " + SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS.Rows[0];
				}
				else
				{
					return null;
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
		/// Get SO Return Goods Detail by Id
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataTable GetSOReturnGoodsDetailByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetSOReturnGoodsDetailByMaster()";
			DataTable dtbPCS = new DataTable();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITPRICE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.BINID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.QASTATUS_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOT_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LINE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.SERIAL_FLD
					+ " FROM " + SO_ReturnedGoodsDetailTable.TABLE_NAME
					+ " WHERE " + SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + "=" + pintMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS;
				}
				else
				{
					return null;
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
		/// Get SO return goods master by id
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetSOReturnGoodsMaster(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataTable dtbPCS = new DataTable();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD + ","
					+ SO_ReturnedGoodsMasterTable.RECEIVERID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.CCNID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.TRANSDATE_FLD + ","
					+ SO_ReturnedGoodsMasterTable.DESCRIPTION_FLD + ","
					+ SO_ReturnedGoodsMasterTable.POSTDATE_FLD + ","
					+ SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.PARTYID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD + ","
					+ SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD
					+ " FROM " + SO_ReturnedGoodsMasterTable.TABLE_NAME
					+ " WHERE " + SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS.Rows[0];
				}
				else
				{
					return null;
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
		/// Get SO detail by Id
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetSODetail(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetSODetail()";
			DataTable dtbPCS = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
					+ SO_SaleOrderDetailTable.AUTOCOMMIT_FLD + ","
					+ SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.UNITPRICE_FLD + ","
					+ SO_SaleOrderDetailTable.VATAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.NETAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD + ","
					+ SO_SaleOrderDetailTable.CANCELREASONID_FLD + ","
					+ SO_SaleOrderDetailTable.VATPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.UMRATE_FLD + ","
					+ SO_SaleOrderDetailTable.SHIPQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.BACKORDERQTY_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.PRODUCTID_FLD + ","
					+ SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.REASONID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKUMID_FLD + ","
					+ SO_SaleOrderDetailTable.SELLINGUMID_FLD
					+ " FROM " + SO_SaleOrderDetailTable.TABLE_NAME
					+ " WHERE " + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS.Rows[0];
				}
				else
				{
					return null;
				}
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
		
		
		/// <summary>
		/// Get SO detail by Id
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataTable GetSODetailByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetSODetailByMaster()";
			DataTable dtbPCS = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
					+ SO_SaleOrderDetailTable.AUTOCOMMIT_FLD + ","
					+ SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.UNITPRICE_FLD + ","
					+ SO_SaleOrderDetailTable.VATAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.NETAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD + ","
					+ SO_SaleOrderDetailTable.CANCELREASONID_FLD + ","
					+ SO_SaleOrderDetailTable.VATPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.UMRATE_FLD + ","
					+ SO_SaleOrderDetailTable.SHIPQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.BACKORDERQTY_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.PRODUCTID_FLD + ","
					+ SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.REASONID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKUMID_FLD + ","
					+ SO_SaleOrderDetailTable.SELLINGUMID_FLD
					+ " FROM " + SO_SaleOrderDetailTable.TABLE_NAME
					+ " WHERE " + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + pintMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS;
				}
				else
				{
					return null;
				}
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
		
		/// <summary>
		/// Get SO master by id
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetSOMaster(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetSOMaster()";
			DataTable dtbPCS = new DataTable();			
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
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
					+ " WHERE " + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS.Rows[0];
				}
				else
				{
					return null;
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
		/// Get Purchase order by Id
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetPOMaster(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetPOMaster()";

			// HACK: dungla 11-29-2005
			DataTable dtbPCS = new DataTable();
			// END: dungla 11-29-2005
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			
			try
			{
				string strSql = "SELECT "
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
					+" WHERE " + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS.Rows[0];
				}
				else
				{
					return null;
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
				if(oconPCS != null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		
		/// <summary>
		/// Get Purchase order detail by Id
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetPODetail(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetPODetail()";
			
			DataTable dtbPCS = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
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
					+ PO_PurchaseOrderDetailTable.APPROVALDATE_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();			

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS.Rows[0];
				}
				else
				{
					return null;
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
		
		
		/// <summary>
		/// Get Purchase order detail by master Id
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataTable GetPODetailByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetPODetailByMaster()";
			
			DataTable dtbPCS = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
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
					+ PO_PurchaseOrderDetailTable.APPROVALDATE_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + pintMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();			

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS;
				}
				else
				{
					return null;
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
		
		/// <summary>
		/// Get Purchase order detail by Id
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetPODetail(int pintMasterID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetPODetail()";
			
			DataTable dtbPCS = new DataTable();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
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
					+ PO_PurchaseOrderDetailTable.APPROVALDATE_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + pintMasterID
					+ " AND " + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + "=" + pintProductID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();			

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPCS);

				if(dtbPCS.Rows.Count != 0)
				{
					return dtbPCS.Rows[0];
				}
				else
				{
					return null;
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
		
		/// <summary>
		/// Get PO Receipt Detail by ID
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetPOReceiptDetail(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetPOReceiptDetail()";
			
			DataTable dtbPOReceiptDetail = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ PO_PurchaseOrderReceiptDetailTable.BINID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.LOT_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPOReceiptDetail);
				if(dtbPOReceiptDetail.Rows.Count != 0)
				{
					return dtbPOReceiptDetail.Rows[0];
				}
				else
				{
					return null;
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
		

		/// <summary>
		/// Get PO Receipt Detail by ID
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataTable GetPOReceiptDetailByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetPOReceiptDetailByMaster()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ PO_PurchaseOrderReceiptDetailTable.BINID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.LOT_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD + ","
					+ PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD + "=" + pintMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				DataTable dtbPOReceiptDetail = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPOReceiptDetail);
				if(dtbPOReceiptDetail.Rows.Count != 0)
				{
					return dtbPOReceiptDetail;
				}
				else
				{
					return null;
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
		
		/// <summary>
		/// Get PO receipt master by ID
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ </author>
		public DataRow GetPOReceiptMaster(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetPOReceiptMaster()";
			
			DataTable dtbPOReceipt = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD
					+ " FROM " + PO_PurchaseOrderReceiptMasterTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbPOReceipt);

				if(dtbPOReceipt.Rows.Count != 0)
				{
					return dtbPOReceipt.Rows[0];
				}
				else
				{
					return null;
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

		public int GetPurposeCode(int pintPurposeId)
		{
			const string METHOD_NAME = THIS + ".GetPurposeCode()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT Code FROM PRO_IssuePurpose WHERE IssuePurposeID = " + pintPurposeId;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();

				try
				{
					return Convert.ToInt32(objResult.ToString().Trim());
				}
				catch
				{
					return -1;
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
	}
}