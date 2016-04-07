using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Reflection;
using System.Text;
using PCSComUtils.Common;
using PCSUtils.Utils;
using C1.Win.C1Preview;
using C1.C1Report;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace InvoiceForSaleOrderReport
{
	public class InvoiceForSaleOrderReport : MarshalByRefObject, IDynamicReport
	{
        private const string DemiliterChar = "#";
        private const string SO_INVOICE_STANDARD_REPORT = "Invoice4SaleOrderByDay.xml";
        private const string SO_INVOICE_APPENDIX_REPORT = "Invoice4SaleOrderByDay_Appendix.xml";
        private const string REPORT_NAME = "Sale Order Invoice";
        private const string REPORTFLD_AMOUNT_IN_WORD = "fldAmountInWord";
        private const string REPORTFLD_AMOUNT_IN_WORD1 = "fldAmountInWord1";

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

	    public DataTable ExecuteReport(string day, string strPartyId, string sSaleTypeId, string sPartyLocationId,
	        string sMasterId, string sdocumentNumber, string sType)
	    {
            var scheduleDate = DateTime.Parse(day);
            scheduleDate = new DateTime(scheduleDate.Year, scheduleDate.Month, scheduleDate.Day);
	        int partyId, saleTypeId, partyLocId, masterId;
	        int.TryParse(strPartyId, out partyId);
	        int.TryParse(sSaleTypeId, out saleTypeId);
	        int.TryParse(sPartyLocationId, out partyLocId);
	        int.TryParse(sMasterId, out masterId);

            DataTable reportData = GetReportData(scheduleDate, partyId, saleTypeId, partyLocId, masterId, sdocumentNumber, sType);

            #region report

            var arlVat = new ArrayList();
            decimal totalAmount = 0;
            foreach (DataRow row in reportData.Rows)
            {
                if (!arlVat.Contains(row[SO_SaleOrderDetailTable.VATPERCENT_FLD]))
                {
                    arlVat.Add(row[SO_SaleOrderDetailTable.VATPERCENT_FLD]);
                }
                if (arlVat.Count > 1)
                {
                    continue;
                }
                decimal netAmount = Convert.ToDecimal(row[SO_ConfirmShipDetailTable.NETAMOUNT_FLD]);
                decimal vatPercent = Convert.ToDecimal(row[SO_SaleOrderDetailTable.VATPERCENT_FLD]);
                totalAmount += netAmount + netAmount * vatPercent / 100;
            }

            //Check rows to select valid report lay out
            mLayoutFile = CountDistinctProduct(reportData) >= 15 ? SO_INVOICE_APPENDIX_REPORT : SO_INVOICE_STANDARD_REPORT;

            C1Report rptReport = new C1Report();

            rptReport.Load(mReportFolder + "\\" + mLayoutFile, rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile)[0]);
            rptReport.Layout.PaperSize = PaperKind.Letter;
            try
	        {
                string totalAmountInWord = ConvertNumberToWord.ChuyenSoThanhChu(decimal.Round(totalAmount, 0));
                rptReport.Fields[REPORTFLD_AMOUNT_IN_WORD].Text = totalAmountInWord;
                rptReport.Fields[REPORTFLD_AMOUNT_IN_WORD1].Text = totalAmountInWord;
            }
	        catch
	        {
	        }

            // set datasource object that provides data to report.
            rptReport.DataSource.Recordset = reportData;
            // render report
            rptReport.Render();
	        var reportDocument = rptReport.Document;
            reportDocument.DefaultPageSettings.Margins = new Margins(20, 0, 20, 0);

            // render the report into the PrintPreviewControl
	        C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog
	        {
	            FormTitle = REPORT_NAME,
	            Report = rptReport,
                HandlePrintEvent = true
	        };
	        ppvViewer.ReportViewer.PreviewNavigationPanel.Visible = false;
	        ppvViewer.ReportViewer.Document = reportDocument;

	        ppvViewer.Show();

            #endregion

            return reportData;
	    }

        private int CountDistinctProduct(DataTable pdtbTable)
        {
            var arlItem = new ArrayList();
            foreach (DataRow drow in pdtbTable.Rows)
                if (!arlItem.Contains(drow[ITM_ProductTable.PRODUCTID_FLD]))
                    arlItem.Add(drow[ITM_ProductTable.PRODUCTID_FLD]);
            return arlItem.Count;
        }

        public DataTable GetReportData(DateTime day, int partyId, int saleTypeId, int partyLocId, int masterId, string documentNumber, string type)
        {
            DataSet dstPcs = new DataSet();
            OleDbConnection oconPcs = null;

            try
            {
                var sqlText = new StringBuilder();
                sqlText.AppendLine("SELECT  DISTINCT ROW_NUMBER() OVER(ORDER BY ITM_Product.Revision) AS Seq, ITM_Product.ProductID,  refDetail.CustomerItemCode AS ITM_ProductCode, ITM_Product.Description AS ITM_ProductDescription, ");
                sqlText.AppendLine("ITM_Product.PartNameVN AS ITM_ProductDescriptionVN, MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode, ITM_Product.Revision AS ITM_ProductRevision,");
                sqlText.AppendLine("SUM(SO_InvoiceDetail.InvoiceQty) as DeliveryQuantity, SO_InvoiceDetail.Price as AVGPrice,");
                sqlText.AppendLine("SUM(SO_InvoiceDetail.InvoiceQty) * SO_InvoiceDetail.Price AS NetAmount, ISNULL(SO_InvoiceDetail.VATPercent, 0) As VATPercent,");
                sqlText.AppendLine("SUM(ISNULL(SO_InvoiceDetail.VatAmount, 0)) as VatAmount,  SO_InvoiceMaster.InvoiceNo,  CAST(CAST(SO_InvoiceMaster.InvoiceDate AS DATE) AS DATETIME) as ShippedDate,");
                sqlText.AppendLine("MST_Party.Name AS MST_PartyName, MST_Party.Address AS MST_PartyAddress, Case when CharIndex('#', MST_Party.BankAccount) > 0 then");
                sqlText.AppendLine("Substring(MST_Party.BankAccount, 1, CharIndex('#', MST_Party.BankAccount) - 1)  else MST_Party.BankAccount	 End As MST_PartyBankAccount, Case when CharIndex('#', MST_Party.BankAccount) > 0");
                sqlText.AppendLine("then Substring(MST_Party.BankAccount, CharIndex('#', MST_Party.BankAccount) + 1, Len(MST_Party.BankAccount)) Else '' End As MST_PartyBankName, MST_Party.VATCode AS MST_PartyTaxCode,");
                sqlText.AppendLine("MST_Party.MAPBankAccountNo as BankAccountNo, Case when CharIndex('#', MST_Party.MAPBankAccountName) > 0 then Substring(MST_Party.MAPBankAccountName, 1,");
                sqlText.AppendLine("CharIndex('#', MST_Party.MAPBankAccountName) - 1)  else MST_Party.MAPBankAccountName End As BankAccountName, SO_InvoiceMaster.DocumentNumber CustomerPurchaseOrderNo, MST_PartyLocation.[Description] AS ShipToLocation, ST.Code AS SaleType1");
                sqlText.AppendLine("FROM    SO_InvoiceDetail  INNER JOIN SO_InvoiceMaster ON SO_InvoiceDetail.InvoiceMasterID = SO_InvoiceMaster.InvoiceMasterID");
                sqlText.AppendLine("INNER JOIN SO_SaleOrderMaster ON SO_InvoiceMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID  ");
                sqlText.AppendLine("INNER JOIN MST_Party ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID  ");
                sqlText.AppendLine("INNER JOIN MST_PartyLocation ON SO_SaleOrderMaster.ShipToLocID = MST_PartyLocation.PartyLocationID  ");
                sqlText.AppendLine("INNER JOIN SO_SaleOrderDetail ON SO_InvoiceDetail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID  ");
                sqlText.AppendLine("INNER JOIN ITM_Product ON SO_InvoiceDetail.ProductID = ITM_Product.ProductID  ");
                sqlText.AppendLine("INNER JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID ");
                sqlText.AppendLine("INNER JOIN SO_DeliverySchedule E ON E.DeliveryScheduleID = SO_InvoiceDetail.DeliveryScheduleID ");
                sqlText.AppendLine("LEFT JOIN MST_UnitOfMeasure ON SO_SaleOrderDetail.SellingUMID = MST_UnitOfMeasure.UnitOfMeasureID  ");
                sqlText.AppendLine("LEFT JOIN SO_CustomerItemRefMaster refMaster ON refMaster.PartyID = MST_Party.PartyID ");
                sqlText.AppendLine("LEFT JOIN SO_CustomerItemRefDetail refDetail ON refMaster.CustomerItemRefMasterID = refDetail.CustomerItemRefMasterID AND refDetail.ProductID = ITM_Product.ProductID ");
                sqlText.AppendLine("LEFT JOIN SO_SaleType ST ON SO_SaleOrderMaster.SaleTypeID = ST.SaleTypeID");
                sqlText.AppendLine("WHERE SO_InvoiceDetail.InvoiceQty > 0 ");
                sqlText.AppendLine("AND SO_SaleOrderMaster.TypeID = 1");
                if (masterId > 0)
                {
                    sqlText.AppendLine(" AND SO_InvoiceMaster.InvoiceMasterID = " + masterId);
                }
                if (partyId > 0)
                {
                    sqlText.AppendLine(" AND SO_SaleOrderMaster.PartyID = " + partyId);
                }
                if (saleTypeId > 0)
                {
                    sqlText.AppendLine(" AND SO_SaleOrderMaster.SaleTypeID = " + saleTypeId);
                }
                if (partyLocId > 0)
                {
                    sqlText.AppendLine(" AND SO_SaleOrderMaster.ShipToLocID = " + partyLocId);
                }
                if (!string.IsNullOrEmpty(documentNumber))
                {
                    sqlText.AppendLine(" AND SO_InvoiceMaster.DocumentNumber = '" + documentNumber + "'");
                }
                switch (type)
                {
                    case "Car":
                        sqlText.AppendLine(" AND ITM_Category.Code LIKE 'A%'");
                        break;
                    case "Motorbike":
                        sqlText.AppendLine(" AND ITM_Category.Code LIKE 'M%'");
                        break;
                }
                
                if (day > DateTime.MinValue)
                {
                    sqlText.AppendLine(string.Format(" AND E.ScheduleDate BETWEEN '{0:yyyy-MM-dd}' AND '{1:yyyy-MM-dd}'", day, day.AddDays(1)));
                }
                sqlText.AppendLine("GROUP BY SO_InvoiceDetail.Price, CAST(SO_InvoiceMaster.InvoiceDate AS Date),");
                sqlText.AppendLine("ITM_Product.ProductID, refDetail.CustomerItemCode, ITM_Product.Description, ITM_Product.PartNameVN, MST_UnitOfMeasure.Code,");
                sqlText.AppendLine("ITM_Product.Revision, SO_InvoiceDetail.VATPercent, SO_InvoiceMaster.InvoiceNo,");
                sqlText.AppendLine("MST_Party.Name, MST_Party.Address, MST_Party.BankAccount, MST_Party.VATCode, MST_Party.MAPBankAccountNo, MST_Party.MAPBankAccountName, ");
                sqlText.AppendLine("SO_InvoiceMaster.DocumentNumber, MST_PartyLocation.Description, ST.Code, CAST(E.ScheduleDate as DATE)");
                sqlText.AppendLine("ORDER BY ITM_Product.Revision");
                
                oconPcs = new OleDbConnection(mConnectionString);
                var ocmdPcs = new OleDbCommand(sqlText.ToString(), oconPcs) { CommandTimeout = 3600 };

                ocmdPcs.Connection.Open();
                var odadPcs = new OleDbDataAdapter(ocmdPcs);
                odadPcs.Fill(dstPcs, SO_ConfirmShipDetailTable.TABLE_NAME);

                return dstPcs.Tables.Count > 0 ? dstPcs.Tables[0] : new DataTable();
            }
            finally
            {
                if (oconPcs != null)
                {
                    if (oconPcs.State != ConnectionState.Closed)
                    {
                        oconPcs.Close();
                    }
                }
            }
        }
    }
}