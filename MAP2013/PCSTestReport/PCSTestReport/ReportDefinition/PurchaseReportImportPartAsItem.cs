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

namespace PurchaseReportImportPartAsItem
{
	public class PurchaseReportImportPartAsItem : MarshalByRefObject, IDynamicReport
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

		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrMakerID, string pstrTarget, string pstrProductID, string pstrCurrencyID, string pstrExRate)
		{
			// start of month
			DateTime dtmStartOfMonth = new DateTime(Convert.ToInt32(pstrYear), Convert.ToInt32(pstrMonth), 1);
			// end of month
			DateTime dtmEndOfMonth = dtmStartOfMonth.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
			DataTable dtbData = GetInvoice(pstrCCNID, pstrMakerID, dtmStartOfMonth, dtmEndOfMonth, pstrProductID, 
				pstrTarget, pstrExRate);
			
			#region report

			C1Report rptReport = new C1Report();
			
			mLayoutFile = "PurchaseReportImportPartAsItem.xml";
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile)[0]);
			rptReport.Layout.PaperSize = PaperKind.A4;

			#region report parameter
				
			try
			{
				rptReport.Fields["fldCCN"].Text = GetCCN(pstrCCNID);
			}
			catch{}
			try
			{
				rptReport.Fields["fldSupplier"].Text = GetSupplier(pstrMakerID);
			}
			catch{}
			try
			{
				rptReport.Fields["fldMonth"].Text = dtmStartOfMonth.ToString("MMM-yyyy");
			}
			catch{}
			try
			{
				rptReport.Fields["fldTarget"].Text = pstrTarget + "%";
			}
			catch{}
			try
			{
				if (pstrProductID.Length > 0 && pstrProductID.Split(",".ToCharArray()).Length > 0)
					rptReport.Fields["fldPartNo"].Text = "Multi-Selection";
				else if (pstrProductID.Length > 0)
					rptReport.Fields["fldPartNo"].Text = GetPartNo(pstrProductID);
			}
			catch{}
			try
			{
				rptReport.Fields["fldCurrency"].Text = GetCurrency(pstrCurrencyID);
			}
			catch{}
			try
			{
				rptReport.Fields["fldExRate"].Text = pstrExRate;
			}
			catch{}
				
			#endregion
				
			// set datasource object that provides data to report.
			rptReport.DataSource.Recordset = dtbData;
			// render report
			rptReport.Render();

			// render the report into the PrintPreviewControl
			C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
			ppvViewer.FormTitle = "Purchase Report Import Part As Items";
			
			ppvViewer.ReportViewer.Document = rptReport.Document;
			ppvViewer.Show();
			
			#endregion

			return dtbData;
		}

		private DataTable GetInvoice(string pstrCCNID, string pstrMakerID, DateTime pdtmStartOfMonth, DateTime pdtmEndOfMonth, string pstrProductID, string pstrTarget, string pstrExRate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT SUM(CIPAmount * PO_InvoiceMaster.ExchangeRate)/? AS CIP, "
								+ " SUM(InvoiceQuantity) AS Quantity, " + pstrTarget + " AS Target,"
				                + " SUM(InvoiceQuantity * PO_PurchaseOrderDetail.UnitPrice * PO_PurchaseOrderMaster.ExchangeRate)/? AS EXGO,"
				                + " PO_InvoiceDetail.ProductID, ITM_Product.Code AS PartNo,"
				                + " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model"
				                + " FROM PO_InvoiceDetail JOIN PO_InvoiceMaster"
				                + " 	ON PO_InvoiceDetail.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID"
				                + " JOIN PO_PurchaseOrderMaster"
				                + " 	ON PO_InvoiceDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
				                + " JOIN PO_PurchaseOrderDetail"
				                + " 	ON PO_InvoiceDetail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
				                + " JOIN ITM_Product"
				                + " 	ON PO_InvoiceDetail.ProductID = ITM_Product.ProductID"
				                + " WHERE PO_InvoiceMaster.CCNID = " + pstrCCNID
				                + " AND PO_PurchaseOrderMaster.MakerID = " + pstrMakerID
				                + " AND PO_InvoiceMaster.PostDate >= ?"
				                + " AND PO_InvoiceMaster.PostDate <= ?";
				if (pstrProductID.Length > 0)
					strSql += " AND PO_InvoiceDetail.ProductID IN (" + pstrProductID + ")";
				strSql += " GROUP BY PO_InvoiceDetail.ProductID, ITM_Product.Code, ITM_Product.Description, ITM_Product.Revision"
				          + " ORDER BY ITM_Product.Code, ITM_Product.Description, ITM_Product.Revision";
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FirstNum", OleDbType.Decimal)).Value = pstrExRate;
				ocmdPCS.Parameters.Add(new OleDbParameter("NextNum", OleDbType.Decimal)).Value = pstrExRate;
				ocmdPCS.Parameters.Add(new OleDbParameter("StartOfMonth", OleDbType.Date)).Value = pdtmStartOfMonth;
				ocmdPCS.Parameters.Add(new OleDbParameter("EndOfMonth", OleDbType.Date)).Value = pdtmEndOfMonth;
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
		private string GetSupplier(string pstrMakerID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT Code + ' (' + Name + ')' FROM MST_Party WHERE PartyID = " + pstrMakerID;
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

		private string GetPartNo(string pstrProductID)
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
		private string GetCurrency(string pstrCurrencyID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT Code + ' (' + Name + ')' FROM MST_Currency WHERE CurrencyID = " + pstrCurrencyID;
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