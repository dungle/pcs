using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Text;
using PCSComUtils.Common;
using PCSUtils.Utils;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSUtils.Framework.ReportFrame;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace OrderBalanceForImportParts
{
	/// <summary>
	/// Summary description for OrderBalanceForImportParts.
	/// </summary>
	public class OrderBalanceForImportParts : MarshalByRefObject, IDynamicReport
	{
		#region IDynamicReport Members

		private string mConnectionString;

		/// <summary>
		/// ConnectionString, provide for the Dynamic Report
		/// ALlow Dynamic Report to access the DataBase of PCS
		/// </summary>
		public string PCSConnectionString
		{
			get { return mConnectionString; }
			set { mConnectionString = value; }
		}

		private ReportBuilder mReportBuilder;

		/// <summary>
		/// Report Builder Utility Object
		/// Dynamic Report can use this object to render, modify, layout the report
		/// </summary>
		public ReportBuilder PCSReportBuilder
		{
			get { return mReportBuilder; }
			set { mReportBuilder = value; }
		}

		private C1PrintPreviewControl mViewer;

		/// <summary>
		/// ReportViewer Object, provide for the DynamicReport, 
		/// allow Dynamic Report to manipulate with the REportViewer, 
		/// modify the report after rendered if needed
		/// </summary>
		public C1PrintPreviewControl PCSReportViewer
		{
			get { return mViewer; }
			set { mViewer = value; }
		}

		private object mResult;

		/// <summary>
		/// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
		/// </summary>
		public object Result
		{
			get { return mResult; }
			set { mResult = value; }
		}

		private bool mUseEngine;

		/// <summary>
		/// Notify PCS whether the rendering report process is run by 
		/// this IDynamicReport
		/// or the ReportViewer Engine (in the ReportViewer form)
		/// </summary>
		public bool UseReportViewerRenderEngine
		{
			get { return mUseEngine; }
			set { mUseEngine = value; }
		}

		private string mReportFolder;

		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>		
		public string ReportDefinitionFolder
		{
			get { return mReportFolder; }
			set { mReportFolder = value; }
		}

		private string mLayoutFile;

		/// <summary>
		/// Inform External Process about the Layout file
		/// in which PCS instruct to use
		/// (PCS will assign this property while ReportViewer Form execute,
		/// ReportVIewer form will use the layout file in the report config entry to put in this property)
		/// </summary>		
		public string ReportLayoutFile
		{
			get { return mLayoutFile; }
			set { mLayoutFile = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pstrMethod">name of the method to call (which declare in the DynamicReport C# file)</param>
		/// <param name="pobjParameters">Array of parameters provide to call the Method with method name = pstrMethod</param>
		/// <returns></returns>
		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		#endregion
		
		public DataTable ExecuteReport(string pstrCCNID, string pstrVendorID, string pstrProductID, string pstrPOMasterID, string pstrOrderDate)
		{
			DateTime dtmOrderDate = DateTime.MinValue;
			try
			{
				dtmOrderDate = Convert.ToDateTime(pstrOrderDate);
				dtmOrderDate = new DateTime(dtmOrderDate.Year, dtmOrderDate.Month, dtmOrderDate.Day);
			}
			catch{}

			#region Report table

			DataTable dtbReportData = new DataTable();
			dtbReportData = GetReportData(pstrCCNID, pstrVendorID, pstrProductID, pstrPOMasterID, dtmOrderDate);
			
			#region calculate total order, total delivery and total balance quantity
			
			decimal decTotalOrder = 0, decTotalDelivery = 0, decTotalBalance = 0, decTotalInvoice = 0;
			int intDeliveryScheduleID = 0;
			foreach (DataRow drowData in dtbReportData.Rows)
			{
				try
				{
					decTotalInvoice += Convert.ToDecimal(drowData["InvoiceQuantity"]);
				}
				catch{}
				if (intDeliveryScheduleID == Convert.ToInt32(drowData["DeliveryScheduleID"]))
					continue;
				intDeliveryScheduleID = Convert.ToInt32(drowData["DeliveryScheduleID"]);
				try
				{
					decTotalOrder += Convert.ToDecimal(drowData["OrderQuantity"]);
				}
				catch{}
				try
				{
					decTotalDelivery += Convert.ToDecimal(drowData["DeliveryQuantity"]);
				}
				catch{}
			}
			decTotalBalance = decTotalInvoice - decTotalOrder;
			
			#endregion
			
			#endregion
			
			#region report

			C1Report rptReport = new C1Report();
			
			mLayoutFile = "OrderBalanceForImportParts.xml";
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile)[0]);
			rptReport.Layout.PaperSize = PaperKind.A3;
			
			#region total row
			
			try
			{
				rptReport.Fields["fldTotalOrder"].Text = decTotalOrder.ToString();
			}
			catch{}
			try
			{
				rptReport.Fields["fldTotalDelivery"].Text = decTotalDelivery.ToString();
			}
			catch{}
			try
			{
				rptReport.Fields["fldTotalInvoice"].Text = decTotalInvoice.ToString();
			}
			catch{}
			try
			{
				rptReport.Fields["fldTotalBalance"].Text = decTotalBalance.ToString();
			}
			catch{}
			
			#endregion

			#region report parameter
				
			try
			{
				rptReport.Fields["fldCCN"].Text = GetCCN(pstrCCNID);
			}
			catch{}
			try
			{
				string strCode = string.Empty;
				rptReport.Fields["fldMonth"].Text = GetVendor(pstrVendorID, out strCode);
				try
				{
					rptReport.Fields["lblETA"].Text = rptReport.Fields["lblETA"].Text + " " + strCode;
				}
				catch{}
			}
			catch{}
			try
			{
				if (pstrProductID.Length > 0 && pstrProductID.Split(",".ToCharArray()).Length > 0)
					rptReport.Fields["fldPartParam"].Text = "Multi-Selection";
				else if (pstrProductID.Length > 0)
						rptReport.Fields["fldPartParam"].Text = GetItem(pstrProductID);
			}
			catch{}
			try
			{
				if (pstrPOMasterID.Length > 0 && pstrPOMasterID.Split(",".ToCharArray()).Length > 0)
					rptReport.Fields["fldPO"].Text = "Multi-Selection";
				else if (pstrPOMasterID.Length > 0)
					rptReport.Fields["fldPO"].Text = GetPO(pstrPOMasterID);
			}
			catch{}
			try
			{
				if (pstrOrderDate.Length > 0)
					rptReport.Fields["fldOrderDate"].Text = dtmOrderDate.ToString("dd-MMM-yyyy");
			}
			catch{}
			
			#endregion
				
			// set datasource object that provides data to report.
			rptReport.DataSource.Recordset = dtbReportData;
			// render report
			rptReport.Render();

			// render the report into the PrintPreviewControl
			C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
			ppvViewer.FormTitle = "Order Balance For Import Parts";
			
			ppvViewer.ReportViewer.Document = rptReport.Document;
			ppvViewer.Show();
			
			#endregion
			
			return dtbReportData;
		}
		private DataTable GetReportData(string pstrCCNID, string pstrVendorID, string pstrProductID, string pstrPOMasterID, DateTime pdtmOrderDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT PO_PurchaseOrderMaster.OrderDate, PO_PurchaseOrderMaster.Code AS PONO,"
				                + " PO_PurchaseOrderDetail.Line AS POLine, ITM_Product.Code AS PartNo,"
				                + " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model,"
				                + " DATEADD(day, -ISNULL(PO_PurchaseOrderMaster.RequestDeliveryTime, 0), ScheduleDate) AS EtaCust, "
				                + " ScheduleDate, PO_InvoiceMaster.InvoiceNo, PO_InvoiceMaster.BLDate,"
				                + " PO_InvoiceMaster.PostDate AS InvoiceDate, PO_DeliverySchedule.DeliveryQuantity,"
				                + " InvoiceQuantity, ISNULL(InvoiceQuantity,0) - DeliveryQuantity AS Balance,"
				                + " CASE"
				                + " 	WHEN ABS(DATEDIFF(day, BLDate, DATEADD(day, -ISNULL(PO_PurchaseOrderMaster.RequestDeliveryTime, 0), ScheduleDate))) <= 5"
				                + " 	AND ISNULL(InvoiceQuantity,0) - DeliveryQuantity = 0 THEN 'OK'"
				                + "		WHEN ABS(DATEDIFF(day, BLDate, DATEADD(day, -ISNULL(PO_PurchaseOrderMaster.RequestDeliveryTime, 0), ScheduleDate))) > 5"
				                + "		OR ISNULL(InvoiceQuantity,0) - DeliveryQuantity <> 0 THEN 'NG'"
				                + " END AS Evaluation, MST_Carrier.Code AS Method,"
								+ " PO_DeliverySchedule.DeliveryScheduleID, PO_PurchaseOrderDetail.OrderQuantity "
				                + " FROM PO_PurchaseOrderMaster JOIN PO_PurchaseOrderDetail"
				                + " ON PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID"
				                + " JOIN ITM_Product"
				                + " ON PO_PurchaseOrderDetail.ProductID = ITM_Product.ProductID"
				                + " JOIN PO_DeliverySchedule"
				                + " ON PO_PurchaseOrderDetail.PurchaseOrderDetailID = PO_DeliverySchedule.PurchaseOrderDetailID"
				                + " LEFT JOIN (PO_InvoiceDetail"
				                + " JOIN PO_InvoiceMaster ON PO_InvoiceDetail.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID)"
				                + " ON PO_DeliverySchedule.DeliveryScheduleID = PO_InvoiceDetail.DeliveryScheduleID"
				                + " LEFT JOIN MST_Carrier"
				                + " ON PO_InvoiceMaster.CarrierID = MST_Carrier.CarrierID"
				                + " WHERE PO_PurchaseOrderMaster.CCNID = " + pstrCCNID
				                + " AND PO_PurchaseOrderMaster.MakerID = " + pstrVendorID
				                + " AND PO_PurchaseOrderDetail.ApproverID > 0";
				if (pstrProductID.Length > 0)
					strSql += " AND PO_PurchaseOrderDetail.ProductID IN (" + pstrProductID + ")";
				if (pstrPOMasterID.Length > 0)
					strSql += " AND PO_PurchaseOrderMaster.PurchaseOrderMasterID IN (" + pstrPOMasterID + ")";
				if (pdtmOrderDate > DateTime.MinValue)
					strSql += " AND PO_PurchaseOrderMaster.OrderDate = ?";
				strSql += " ORDER BY PO_DeliverySchedule.DeliveryScheduleID";
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				if (pdtmOrderDate > DateTime.MinValue)
					ocmdPCS.Parameters.Add(new OleDbParameter("OrderDate", OleDbType.Date)).Value = pdtmOrderDate;
				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				
				return dtbData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private string GetCCN(string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT Code + ' (' + Description + ')' FROM MST_CCN WHERE CCNID = " + pstrCCNID;
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objResult = ocmdPCS.ExecuteScalar();
				return objResult.ToString();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private string GetVendor(string pstrVendorID, out string strCode)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				strCode = string.Empty;
				string strRet = string.Empty;
				string strSql = "SELECT Code, Name FROM MST_Party WHERE PartyID = " + pstrVendorID;
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter adapter = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable();
				adapter.Fill(dtbData);
				strCode = dtbData.Rows[0]["Code"].ToString();
				strRet = strCode + " (" + dtbData.Rows[0]["Name"].ToString() + ")";
				return strRet;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private string GetItem(string pstrProductID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT Code + ' (' + Description + ')' FROM ITM_Product WHERE ProductID = " + pstrProductID;
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objResult = ocmdPCS.ExecuteScalar();
				return objResult.ToString();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private string GetPO(string pstrPOID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT Code FROM PO_PurchaseOrderMaster WHERE PurchaseOrderMasterID = " + pstrPOID;
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objResult = ocmdPCS.ExecuteScalar();
				return objResult.ToString();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

	}
}
