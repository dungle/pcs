using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Reflection;
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
            rptReport.Layout.PaperSize = PaperKind.A3;
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

            // render the report into the PrintPreviewControl
	        C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog
	        {
	            FormTitle = REPORT_NAME,
	            Report = rptReport,
                HandlePrintEvent = true
	        };
	        ppvViewer.ReportViewer.PreviewNavigationPanel.Visible = false;
	        ppvViewer.ReportViewer.Document = rptReport.Document;

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
                string strSql = string.Empty;

                strSql = "SELECT  DISTINCT ITM_Product.ProductID, "
                         + " refDetail.CustomerItemCode AS ITM_ProductCode,"
                         + " ITM_Product.Description AS ITM_ProductDescription,"
                         + " ITM_Product.PartNameVN AS ITM_ProductDescriptionVN,"
                         + " MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode,"
                         + " ITM_Product.Revision AS ITM_ProductRevision,"

                         + " SO_ConfirmShipDetail.InvoiceQty as DeliveryQuantity,"

                         + " ((SELECT SUM( detail.Price * detail.InvoiceQty)"
                         + " FROM SO_ConfirmShipDetail detail"
                         + " WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID"
                         + " AND detail.ProductID = ITM_Product.ProductID"
                         + " )	"
                         + " /"

                    //validate data to avoid division by zero error 
                         + " ( "
                         + " Case  "
                         + " When ( "
                         + " SELECT SUM(detail.InvoiceQty) "
                         + " FROM SO_ConfirmShipDetail detail "
                         + " WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID "
                         + " AND detail.ProductID = ITM_Product.ProductID "
                         + "    ) = 0  "
                         + " then 1 "
                         + "     else   "
                         + " ( "
                         + " SELECT SUM(detail.InvoiceQty) "
                         + " FROM SO_ConfirmShipDetail detail "
                         + " WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID "
                         + " AND detail.ProductID = ITM_Product.ProductID "
                         + "    ) "
                         + " end      "
                         + " ) "
                    //end validate

                         + " )	"
                         + " as AVGPrice,"

                         + " SO_ConfirmShipDetail.Price as UnitPrice,"
                         + " (SO_ConfirmShipDetail.InvoiceQty * SO_ConfirmShipDetail.Price) AS NetAmount,"

                         + " ISNULL(SO_ConfirmShipDetail.VATPercent, 0) As VATPercent,"
                         + " ISNULL(SO_ConfirmShipDetail.VatAmount, 0) as VatAmount, "

                         + " SO_ConfirmShipMaster.InvoiceNo,"
                         + " SO_ConfirmShipMaster.ConfirmShipNo,"

                         + " SO_ConfirmShipMaster.InvoiceDate as ShippedDate,"

                         + " MST_Party.Name AS MST_PartyName,"
                         + " MST_Party.Address AS MST_PartyAddress,"
                         + " Case when CharIndex('" + DemiliterChar + "', MST_Party.BankAccount) > 0 then"
                         + "    Substring(MST_Party.BankAccount, 1, CharIndex('" + DemiliterChar +
                         "', MST_Party.BankAccount) - 1) "
                         + " else MST_Party.BankAccount	"
                         + " End As MST_PartyBankAccount,"

                         + " Case when CharIndex('" + DemiliterChar + "', MST_Party.BankAccount) > 0 then"
                         + " Substring(MST_Party.BankAccount, CharIndex('" + DemiliterChar +
                         "', MST_Party.BankAccount) + 1, Len(MST_Party.BankAccount))"
                         + " Else ''"
                         + " End As MST_PartyBankName,"

                         + " MST_Party.VATCode AS MST_PartyTaxCode,"

                         + " MST_Party.MAPBankAccountNo as BankAccountNo,"
                         + " Case when CharIndex('" + DemiliterChar + "', MST_Party.MAPBankAccountName) > 0 then"
                         + " Substring(MST_Party.MAPBankAccountName, 1, CharIndex('" + DemiliterChar +
                         "', MST_Party.MAPBankAccountName) - 1) "
                         + " else MST_Party.MAPBankAccountName"
                         + " End As BankAccountName,"

                         + " ("
                         + " SELECT TOP 1 enm_GateType.Description"
                         + " FROM SO_DeliverySchedule"
                         + " LEFT JOIN SO_Gate ON SO_DeliverySchedule.GateID = SO_Gate.GateID"
                         + " LEFT JOIN enm_GateType ON enm_GateType.GateTypeID = SO_Gate.GateTypeID"
                         + " WHERE SO_DeliverySchedule.DeliveryScheduleID = SO_ConfirmShipDetail.DeliveryScheduleID"
                         + " )AS SaleType,"
                         + " GA.Code SOGate,"
                         + " SO_ConfirmShipMaster.DocumentNumber AS CustomerPurchaseOrderNo,"
                         + " SO_ConfirmShipDetail.ConfirmShipDetailID, MST_PartyLocation.[Description] AS ShipToLocation, ST.Code AS SaleType1"
                         + " FROM    SO_ConfirmShipDetail "
                         + " INNER JOIN SO_ConfirmShipMaster ON SO_ConfirmShipDetail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID "
                         + " INNER JOIN SO_SaleOrderMaster ON SO_ConfirmShipMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID "
                         + " INNER JOIN MST_Party ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID "
                         + " INNER JOIN MST_PartyLocation ON SO_SaleOrderMaster.ShipToLocID = MST_PartyLocation.PartyLocationID "
                         + " INNER JOIN SO_SaleOrderDetail ON SO_ConfirmShipDetail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID "
                         + " INNER JOIN ITM_Product ON SO_ConfirmShipDetail.ProductID = ITM_Product.ProductID "
                         + " INNER JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID"
                         + " INNER JOIN SO_DeliverySchedule E ON E.DeliveryScheduleID = SO_ConfirmShipDetail.DeliveryScheduleID"
                         + " LEFT JOIN MST_UnitOfMeasure ON SO_SaleOrderDetail.SellingUMID = MST_UnitOfMeasure.UnitOfMeasureID "
                         + " LEFT JOIN SO_CustomerItemRefMaster refMaster ON refMaster.PartyID = MST_Party.PartyID"
                         + " LEFT JOIN SO_CustomerItemRefDetail refDetail ON refMaster.CustomerItemRefMasterID = refDetail.CustomerItemRefMasterID"
                         + " AND refDetail.ProductID = ITM_Product.ProductID"
                         + " LEFT JOIN SO_Gate GA ON E.GateID = GA.GateID"
                         + " LEFT JOIN SO_SaleType ST ON SO_SaleOrderMaster.SaleTypeID = ST.SaleTypeID"
                         + " WHERE SO_ConfirmShipDetail.InvoiceQty > 0"
                         + " AND SO_SaleOrderMaster.TypeID = 1"; // domestic only
                if (masterId > 0)
                {
                    strSql += " AND SO_ConfirmShipMaster.ConfirmShipMasterID = " + masterId;
                }
                if (partyId > 0)
                {
                    strSql += " AND SO_SaleOrderMaster.PartyID = " + partyId;
                }
                if (saleTypeId > 0)
                {
                    strSql += " AND SO_SaleOrderMaster.SaleTypeID = " + saleTypeId;
                }
                if (partyLocId > 0)
                {
                    strSql += " AND SO_SaleOrderMaster.ShipToLocID = " + partyLocId;
                }
                if (!string.IsNullOrEmpty(documentNumber))
                {
                    strSql += " AND SO_ConfirmShipMaster.DocumentNumber = '" + documentNumber + "'";
                }
                switch (type)
                {
                    case "Car":
                        strSql += " AND ITM_Category.Code LIKE 'A%'";
                        break;
                    case "Motorbike":
                        strSql += " AND ITM_Category.Code LIKE 'M%'";
                        break;
                }
                
                if (day > DateTime.MinValue)
                {
                    strSql += string.Format(" AND E.ScheduleDate BETWEEN '{0:yyyy-MM-dd}' AND '{1:yyyy-MM-dd}'", day, day.AddDays(1));
                }
                strSql += " ORDER BY ITM_Product.Revision";

                oconPcs = new OleDbConnection(mConnectionString);
                var ocmdPcs = new OleDbCommand(strSql, oconPcs) { CommandTimeout = 3600 };

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