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

namespace PurchasingPriceTrendInYear
{
	/// <summary>
	/// Summary description for PurchasingPriceTrendInYear.
	/// </summary>
	public class PurchasingPriceTrendInYear : MarshalByRefObject, IDynamicReport
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
		
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrProductID)
		{
			int intYear = Convert.ToInt32(pstrYear);
			
			#region report table

			DataTable dtbData = new DataTable();
			dtbData.Columns.Add(new DataColumn("Category", typeof (string)));
			dtbData.Columns.Add(new DataColumn("PartNo", typeof (string)));
			dtbData.Columns.Add(new DataColumn("PartName", typeof (string)));
			dtbData.Columns.Add(new DataColumn("Model", typeof (string)));
			
			for (int i = 1; i <= 12; i++)
			{
				DateTime dtmDate = new DateTime(intYear, i, 1);
				string strColName = "M" + dtmDate.Month.ToString();
				dtbData.Columns.Add(new DataColumn(strColName, typeof (decimal)));
			}
			
			#endregion
			
			#region data
			
			DataTable dtbReportData = GetReportData(pstrCCNID, pstrYear, pstrProductID);
			
			string strLastProductID = string.Empty;
			foreach (DataRow drowData in dtbReportData.Rows)
			{
				string strProductID = drowData["ProductID"].ToString();
				if (strLastProductID == strProductID)
					continue;
				
				strLastProductID = strProductID;
				
				string strFilter = "ProductID = '" + strProductID + "'";
				DataRow[] drowProducts = dtbReportData.Select(strFilter, "SMonth ASC");
				
				foreach (DataRow drowItem in drowProducts)
				{
					DataRow drowReport = dtbData.NewRow();
				
					// general information
					drowReport["Category"] = drowItem["Category"];
					drowReport["PartNo"] = drowItem["PartNo"];
					drowReport["PartName"] = drowItem["PartName"];
					drowReport["Model"] = drowItem["Model"];
					
					int intMonth = Convert.ToInt32(drowItem["SMonth"]);
					DateTime dtmDate = new DateTime(intYear, intMonth, 1);
					string strColName = "M" + dtmDate.Month.ToString();
					
					try
					{
						drowReport[strColName] = Convert.ToDecimal(drowItem["Price"]);
					}
					catch{}
					
					// insert to result table
					dtbData.Rows.Add(drowReport);
				}
			}
			
			#endregion
			
			#region report

			C1Report rptReport = new C1Report();
			
			mLayoutFile = "PurchasingPriceTrendInYear.xml";
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile)[0]);
			rptReport.Layout.PaperSize = PaperKind.A3;

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
				if (pstrProductID.Length > 0 && pstrProductID.Split(",".ToCharArray()).Length > 0)
					rptReport.Fields["fldPartParam"].Text = "Multi-Selection";
				else if (pstrProductID.Length > 0)
					rptReport.Fields["fldPartParam"].Text = GetPartNo(pstrProductID);
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
		private DataTable GetReportData(string pstrCCNID, string pstrYear, string pstrProductID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT DISTINCT SUM(ISNULL(CIFAmount,0))/SUM(ISNULL(InvoiceQuantity, 0)) AS Price,"
				                + " PO_InvoiceDetail.ProductID, DATEPART(month, PostDate) AS SMonth,"
				                + " ITM_Category.Code AS Category, ITM_Product.Code AS PartNo,"
				                + " ITM_Product.Description AS PartName, ITM_Product.Revision AS Model"
				                + " FROM PO_InvoiceDetail JOIN PO_InvoiceMaster"
				                + " ON PO_InvoiceDetail.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID"
				                + " JOIN PO_PurchaseOrderMaster"
				                + " ON PO_InvoiceDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
				                + " JOIN MST_Party"
				                + " ON PO_PurchaseOrderMaster.MakerID = MST_Party.PartyID"
				                + " JOIN ITM_Product"
				                + " ON PO_InvoiceDetail.ProductID = ITM_Product.ProductID"
				                + " LEFT JOIN ITM_Category"
				                + " ON ITM_Product.CategoryID = ITM_Category.CategoryID"
				                + " WHERE PO_InvoiceMaster.CCNID = " + pstrCCNID
				                + " AND DATEPART(year, PostDate) = " + pstrYear
				                + " AND MST_Party.CountryID <> (SELECT CountryID FROM MST_CCN WHERE CCNID = " + pstrCCNID + ")"
				                + " AND MST_Party.Type <> 0";
				if (pstrProductID.Length > 0)
					strSql += " AND PO_InvoiceDetail.ProductID IN (" + pstrProductID + ")";
				strSql += " GROUP BY ITM_Category.Code, PO_InvoiceDetail.ProductID, ITM_Product.Code,"
				          + " ITM_Product.Description, ITM_Product.Revision, DATEPART(month, PostDate)"
				          + " ORDER BY ITM_Category.Code, ITM_Product.Code, ITM_Product.Description,"
				          + " ITM_Product.Revision, DATEPART(month, PostDate)";
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

	}
}
