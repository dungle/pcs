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

namespace PurchaseReportImportPartByMonth
{
	public class PurchaseReportImportPartByMonth : MarshalByRefObject, IDynamicReport
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

		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrCurrencyID, string pstrExRate)
		{
			DataTable dtbInvoice = GetInvoice(pstrCCNID, pstrYear);
			DataTable dtbCharge = GetAdditionalCharge(pstrCCNID, pstrYear);
			decimal decExRate = Convert.ToDecimal(pstrExRate);
			
			#region report table

			DataTable dtbData = new DataTable();
			dtbData.Columns.Add(new DataColumn("MyMonth", typeof (string)));
			dtbData.Columns.Add(new DataColumn("Quantity", typeof (decimal)));
			dtbData.Columns.Add(new DataColumn("EXGO", typeof (decimal)));
			dtbData.Columns.Add(new DataColumn("CIP", typeof (decimal)));
			dtbData.Columns.Add(new DataColumn("ImportTax", typeof (decimal)));
			
			#endregion
			
			#region data

			for (int i = 1; i <= 12; i++)
			{
				DateTime dtmDate = new DateTime(Convert.ToInt32(pstrYear),  i, 1);
				DataRow drowReport = dtbData.NewRow();
				
				string strFilter = "SMonth = '" + i.ToString() + "'";
				decimal decCIP = 0;
				try
				{
					decCIP = Convert.ToDecimal(dtbInvoice.Compute("SUM(CIP)", strFilter));
				}
				catch {}
				drowReport["CIP"] = decimal.Round(decCIP/decExRate, 2);
				
				// quantity from po invoice
				decimal decQuantity = 0;
				try
				{
					decQuantity = Convert.ToDecimal(dtbInvoice.Compute("SUM(Quantity)", strFilter));
				}
				catch {}
				drowReport["Quantity"] = decQuantity;
				
				// EX-GO from po invoice
				decimal decEXGO = 0;
				try
				{
					decEXGO = Convert.ToDecimal(dtbInvoice.Compute("SUM(EXGO)", strFilter));
				}
				catch {}
				drowReport["EXGO"] = decimal.Round(decEXGO/decExRate, 2);
				
				// Import Tax from invoice
				decimal decImportTax = 0;
				try
				{
					decImportTax = Convert.ToDecimal(dtbInvoice.Compute("SUM(ImportTaxAmount)", strFilter));
				}
				catch {}
				// import tax from additional charge
				try
				{
					decImportTax += Convert.ToDecimal(dtbCharge.Compute("SUM(ImportTaxAmount)", strFilter));
				}
				catch {}
				drowReport["ImportTax"] = decimal.Round(decImportTax/decExRate, 2);
				
				// Month
				drowReport["MyMonth"] = dtmDate.ToString("MMMM");
				
				// insert to result table
				dtbData.Rows.Add(drowReport);
			}
			
			#endregion
			
			#region report

			C1Report rptReport = new C1Report();
			
			mLayoutFile = "PurchaseReportImportPartByMonth.xml";
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
				rptReport.Fields["fldMonth"].Text = pstrYear;
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
			ppvViewer.FormTitle = "Purchase Report Import Part By Month";
			
			ppvViewer.ReportViewer.Document = rptReport.Document;
			ppvViewer.Show();
			
			#endregion

			return dtbData;
		}

		private DataTable GetInvoice(string pstrCCNID, string pstrYear)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT SUM(CIPAmount * PO_InvoiceMaster.ExchangeRate) AS CIP, SUM(InvoiceQuantity) AS Quantity, "
				                + " SUM(InvoiceQuantity * PO_PurchaseOrderDetail.UnitPrice * PO_PurchaseOrderMaster.ExchangeRate) AS EXGO,"
				                + " SUM(PO_InvoiceDetail.ImportTaxAmount) * PO_InvoiceMaster.ExchangeRate AS ImportTaxAmount,"
				                + " DATEPART(month, PO_InvoiceMaster.PostDate) AS SMonth"
				                + " FROM PO_InvoiceDetail JOIN PO_InvoiceMaster"
				                + " 	ON PO_InvoiceDetail.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID"
				                + " JOIN PO_PurchaseOrderMaster"
				                + " 	ON PO_InvoiceDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
				                + " JOIN PO_PurchaseOrderDetail"
				                + " 	ON PO_InvoiceDetail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
				                + " JOIN MST_Party"
				                + " 	ON PO_PurchaseOrderMaster.MakerID = MST_Party.PartyID"
				                + " WHERE PO_InvoiceMaster.CCNID = " + pstrCCNID
				                + " AND DATEPART(year, PO_InvoiceMaster.PostDate) = " + pstrYear
				                + " AND MST_Party.CountryID <> (SELECT CountryID FROM MST_CCN WHERE CCNID = " + pstrCCNID + ")"
				                + " AND MST_Party.Type <> 0"
				                + " GROUP BY DATEPART(month, PO_InvoiceMaster.PostDate), PO_InvoiceMaster.ExchangeRate"
				                + " ORDER BY DATEPART(month, PO_InvoiceMaster.PostDate)";
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
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

		private DataTable GetAdditionalCharge(string pstrCCNID, string pstrYear)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT CST_FreightDetail.ImportTax * CST_FreightMaster.ExchangeRate AS ImportTaxAmount,"
					+ " DATEPART(month, PO_InvoiceMaster.PostDate) AS SMonth"
					+ " FROM CST_FreightDetail JOIN CST_FreightMaster"
					+ " 	ON CST_FreightDetail.FreightMasterID = CST_FreightMaster.FreightMasterID"
					+ " JOIN PO_PurchaseOrderReceiptMaster"
					+ " 	ON CST_FreightMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID"
					+ " JOIN PO_InvoiceMaster"
					+ " 	ON PO_PurchaseOrderReceiptMaster.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID"
					+ " WHERE PO_InvoiceMaster.CCNID = " + pstrCCNID
					+ " AND DATEPART(year, PO_InvoiceMaster.PostDate) = " + pstrYear
					+ " ORDER BY DATEPART(month, PO_InvoiceMaster.PostDate)";
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
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