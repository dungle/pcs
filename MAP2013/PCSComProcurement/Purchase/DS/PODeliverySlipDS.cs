using System;
using System.Data;
using System.Data.OleDb;

using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProcurement.Purchase.DS
{
	/// <summary>
	/// Summary description for PODeliverySlipDS.
	/// </summary>
	public class PODeliverySlipDS 
	{
		private const string THIS = "PCSComProcurement.Purchase.DS.PODeliverySlipDS";
		public PODeliverySlipDS()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public object GetObjectVO(int pintID)
		{
			throw new NotImplementedException();
		}

		public DataSet List()
		{
			throw new NotImplementedException();
		}

		public void Delete(int pintID)
		{
			throw new NotImplementedException();
		}

		public void Update(object pobjObjecVO)
		{
			throw new NotImplementedException();
		}

		public void Add(object pobjObjectVO)
		{
			throw new NotImplementedException();
		}

		public void UpdateDataSet(DataSet pData)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get all delivery for a PO from FromDate to ToDate
		/// </summary>
		/// <param name="pintPurchaseOrderMasterID">PO Master</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Delivery Slip</returns>
		public DataTable GetReportData(int pintPurchaseOrderMasterID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".GetReportData()";

			DataTable dtbData = new DataTable("Delivery Slip");
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT	DISTINCT PO_PurchaseOrderMaster.Code AS PoNo, PO_DeliverySchedule.DeliveryQuantity,"
					+ " PO_DeliverySchedule.ScheduleDate, PO_DeliverySchedule.ScheduleHour, ITM_Product.ProductID,"
					+ " ITM_Product.Code AS PartNumber, ITM_Product.Description AS PartName,"
					+ " ITM_Product.Revision AS Model, ITM_Category.Code AS Category,"
					+ " MST_Party.Code AS CustCode, MST_Party.Name AS CustName,"
					+ " MST_CCN.Code AS CCNCode, MST_CCN.Name AS CCNName,"
					+ " MST_UnitOfMeasure.Code AS Unit"
					+ " FROM ITM_Product JOIN PO_PurchaseOrderDetail"
					+ " ON PO_PurchaseOrderDetail.ProductID = ITM_Product.ProductID"
					+ " JOIN PO_PurchaseOrderMaster "
					+ " ON PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID"
					+ " JOIN MST_Party"
					+ " ON PO_PurchaseOrderMaster.PartyID = MST_Party.PartyID"
					+ " JOIN MST_CCN"
					+ " ON PO_PurchaseOrderMaster.CCNID = MST_CCN.CCNID"
					+ " JOIN (SELECT SUM(ISNULL(DeliveryQuantity, 0)) DeliveryQuantity,"
					+ "		CAST((CAST(DATEPART(year, ScheduleDate) AS VARCHAR)"
					+ "		+ '-' + CAST(DATEPART(month, ScheduleDate) AS VARCHAR)"
					+ "		+ '-' + CAST(DATEPART(day, ScheduleDate) AS VARCHAR)) AS DATETIME) ScheduleDate,"
					+ "		DATEPART(hh, ScheduleDate) ScheduleHour, ITM_Product.ProductID"
					+ "		FROM PO_PurchaseOrderMaster"
					+ " 	JOIN PO_PurchaseOrderDetail"
					+ "			ON PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID"
					+ " 	JOIN  PO_DeliverySchedule"
					+ " 		ON PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID "
					+ " 	JOIN ITM_Product"
					+ " 		ON PO_PurchaseOrderDetail.ProductID = ITM_Product.ProductID"
					+ " 	WHERE	ISNULL(PO_PurchaseOrderDetail.ApproverID, 0) > 0"
					+ " 		AND ISNULL(PO_PurchaseOrderDetail.Closed, 0) = 0"
					+ " 		AND PO_PurchaseOrderMaster.PurchaseOrderMasterID = " + pintPurchaseOrderMasterID;
				if (pdtmFromDate != DateTime.MinValue)
					strSql += "			AND PO_DeliverySchedule.ScheduleDate >= ?";
				if (pdtmToDate != DateTime.MaxValue)
					strSql += "			AND PO_DeliverySchedule.ScheduleDate <= ?";
				strSql += " GROUP BY CAST((CAST(DATEPART(year, ScheduleDate) AS VARCHAR)"
					+ " + '-' + CAST(DATEPART(month, ScheduleDate) AS VARCHAR)"
					+ " + '-' + CAST(DATEPART(day, ScheduleDate) AS VARCHAR)) AS DATETIME),"
					+ " DATEPART(hh, ScheduleDate), ITM_Product.ProductID) PO_DeliverySchedule"
					+ " ON PO_DeliverySchedule.ProductID = PO_PurchaseOrderDetail.ProductID"
					+ " LEFT JOIN ITM_Category"
					+ " ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " JOIN MST_UnitOfMeasure"
					+ " ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID"
					+ " WHERE	PO_PurchaseOrderMaster.PurchaseOrderMasterID = " + pintPurchaseOrderMasterID;
				strSql += " ORDER BY PO_DeliverySchedule.ScheduleDate, PO_DeliverySchedule.ScheduleHour,"
					+ " ITM_Category.Code, ITM_Product.Revision, ITM_Product.Code";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				if (pdtmFromDate != DateTime.MinValue && pdtmToDate != DateTime.MaxValue)
				{
					ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date));
					ocmdPCS.Parameters["FromDate"].Value = pdtmFromDate;

					ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date));
					ocmdPCS.Parameters["ToDate"].Value = pdtmToDate;
				}

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();

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
	}
}
