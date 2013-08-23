using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;

//Using PCS's namespaces
using PCSComProcurement.Purchase.BO;
using PCSComProcurement.Purchase.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSUtils.Framework.ReportFrame;

namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for POInvoice.
	/// </summary>
	public partial class POInvoice : Form
	{
		#region Declaration
		
		#region Constants
		
		private const string THIS = "PCSProcurement.Purchase.POInvoice";
		private const string ZERO_STRING = "0";
		private const string DETAILED_INVOICE_MASTER_VIEW = "v_DetailedPOInvoiceMaster";
		private const string PERCENT_VALUE_RANGE = "(0, 100]";
		private const string VENDOR_CUSTOMER_VIEW = "V_VendorCustomer";
		private const string VENDOR_COLUMN = "Vendor";		

		#endregion Constants
		
		private EnumAction enuFormAction = EnumAction.Default;
		private bool blnDataIsValid = false;		
		private DataTable dtbGridLayOut;
		private DataTable dtbDetail;
		private PO_InvoiceMasterVO voMaster;
		private int intCurrentRow = 0;
		
		
		private POInvoiceBO boMaster = new POInvoiceBO();
        private List<int> _removedId = new List<int>();

		#endregion Declaration

        public POInvoice()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

		#region Private Methods

		/// <summary>
		/// Build and show PO Invoice Report
		/// </summary>
		/// <Author> Tuan TQ, 05 Jan, 2006</Author>
		private void ShowPOInvoiceReport(object sender, System.EventArgs e)
		{	
			const string METHOD_NAME = THIS + ".ShowPOInvoiceReport()";
			try
			{				
				const string APPLICATION_PATH  = @"PCSMain\bin\Debug";

				const string REPORTFLD_TITLE = "fldTitle";

				const string REPORT_COMPANY_FLD = "fldCompany";
				const string REPORT_GRAND_TOTAL_FLD = "fldGrandTotal";
				const string REPORT_TOTAL_INLAND_FLD = "fldTotalInlandAmount";
				const string REPORT_TOTAL_CIF_AMOUNT_FLD = "fldTotalCIFAmount";
				const string REPORT_TOTAL_BEFORE_VAT_FLD = "fldTotalBeforeVATAMount";
				const string REPORT_TOTAL_CIP_AMOUNT_FLD = "fldTotalCIPAmount";
				const string REPORT_TOTAL_IMPORT_TAX_FLD = "fldTotalImportTax";
				const string REPORT_TOTAL_VAT_AMOUNT_FLD = "fldTotalVATAmount";

				const float CHARWIDTH_SCALE_TO_FONT_SIZE = 9;
				const float CHARACTER_WIDTH_RATIO = 80f;

				//const string REPORT_FAX_FLD = "fldCompanyFax";
				//const string REPORT_ADDRESS_FLD = "fldCompanyAddress";

				const string INVOICE_REPORT_LAYOUT = "InvoiceReport.xml";
				
				//return if no record was selected
				if(voMaster.InvoiceMasterID <= 0)
				{
					return;
				}				
				
				this.Cursor = Cursors.WaitCursor;

				C1PrintPreviewDialog   printPreview = new C1PrintPreviewDialog();
				C1PrintPreviewDialogBO boDataReport = new C1PrintPreviewDialogBO();
			
				DataTable dtbResult;
				dtbResult = boDataReport.GetPOInvoiceData(voMaster.InvoiceMasterID);

				// Check data source
				if(dtbResult == null)
				{
					this.Cursor = Cursors.Default;
					return;
				}

				ReportBuilder reportBuilder = new ReportBuilder();
			
				//Get actual application path
				string strReportPath = Application.StartupPath;
				int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
				if ( intIndex > -1 ) 
				{
					strReportPath = strReportPath.Substring(0, intIndex);
				}

				if(strReportPath.Substring(strReportPath.Length -1) == @"\")
				{
					strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
				}
				else
				{
					strReportPath += @"\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
			
				//Set datasource and lay-out path for reports
				reportBuilder.SourceDataTable = dtbResult;
				reportBuilder.ReportDefinitionFolder = strReportPath;
			
				reportBuilder.ReportLayoutFile = INVOICE_REPORT_LAYOUT;

				//check if layout is valid
				if(reportBuilder.AnalyseLayoutFile())
				{					
					reportBuilder.UseLayoutFile = true;
				}
				else
				{
					this.Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
					return;
				}

				reportBuilder.MakeDataTableForRender();

				// and show it in preview dialog				
				
				reportBuilder.ReportViewer = printPreview.ReportViewer;
				reportBuilder.RenderReport();
		
				//Header information get from system params
				reportBuilder.DrawPredefinedField(REPORT_COMPANY_FLD, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
				//reportBuilder.RefreshReport();

				//Calculate numeric field width				
				//int iLength = reportBuilder.Report.Fields[REPORT_GRAND_TOTAL_FLD].Text.Trim().Length;
				int iLength = reportBuilder.Report.Fields[REPORT_GRAND_TOTAL_FLD].Value.ToString().Trim().Length;
				iLength += (iLength/3) + 2;
				float fFontSize = reportBuilder.Report.Fields[REPORT_GRAND_TOTAL_FLD].Font.Size;
				double dActualWidth = iLength * CHARACTER_WIDTH_RATIO * fFontSize / CHARWIDTH_SCALE_TO_FONT_SIZE;
				//Reset width
				reportBuilder.Report.Fields[REPORT_GRAND_TOTAL_FLD].Width = dActualWidth;
				reportBuilder.Report.Fields[REPORT_TOTAL_BEFORE_VAT_FLD].Width = dActualWidth;
				reportBuilder.Report.Fields[REPORT_TOTAL_CIF_AMOUNT_FLD].Width = dActualWidth;
				reportBuilder.Report.Fields[REPORT_TOTAL_CIP_AMOUNT_FLD].Width = dActualWidth;
				reportBuilder.Report.Fields[REPORT_TOTAL_IMPORT_TAX_FLD].Width = dActualWidth;
				reportBuilder.Report.Fields[REPORT_TOTAL_INLAND_FLD].Width = dActualWidth;
				reportBuilder.Report.Fields[REPORT_TOTAL_VAT_AMOUNT_FLD].Width = dActualWidth;

				reportBuilder.RefreshReport();				
			
				//Print report
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(REPORTFLD_TITLE).Text;
				}
				catch{}
				printPreview.Show();		
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch(Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}		

		private void ClearGrid()
		{			
			int iRowCount = GetActualRowCount(dtbDetail);

			for(int i =0; i < iRowCount; i++)
			{
				dgrdData.Delete();
			}
		}

		private int GetActualRowCount(DataTable pdtbDataTable)
		{
			int iCount = 0;
			foreach(DataRow dtRow in pdtbDataTable.Rows)
			{
				if(dtRow.RowState != DataRowState.Deleted)
				{
					iCount++;
				}
			}

			return iCount;
		}

		private void AssignControlValue2VO()
		{
			const string METHOD_NAME = THIS + ".AssignControlValue2VO()";
			try
			{
				//voMaster.PostDate = DateTime.Parse(dtmPostDate.Value.ToString());
				voMaster.PostDate = (DateTime)dtmPostDate.Value;
				voMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
				voMaster.InvoiceNo = txtInvoiceNo.Text.Trim();				
				voMaster.PaymentTermID = int.Parse(txtPayTerms.Tag.ToString());
				voMaster.PartyID = int.Parse(txtVendor.Tag.ToString());
				voMaster.CarrierID = int.Parse(txtCarrier.Tag.ToString());
				voMaster.DeliveryTermID = int.Parse(txtDeliveryTerms.Tag.ToString());
				voMaster.CurrencyID = int.Parse(txtCurrency.Tag.ToString());

				voMaster.TaxDeclarationNumber = txtDeclaredNo.Text.Trim();
				voMaster.TaxInformNumber = txtTaxInformNo.Text.Trim();
				voMaster.BLNumber = txtBLNo.Text.Trim();			
				
				if(!numExchRate.ValueIsDbNull)
				{
					voMaster.ExchangeRate = decimal.Parse(numExchRate.Value.ToString());
				}
				
				//BLDate
				if(!dtmBLDate.ValueIsDbNull)
				{
					voMaster.BLDate = (DateTime)dtmBLDate.Value;
					//voMaster.BLDate = DateTime.Parse(dtmBLDate.Value.ToString());
				}
				else
				{
					voMaster.BLDate = DateTime.MinValue;
				}

				//Declaration Date
				if(!dtmDeclareDate.ValueIsDbNull)
				{
					voMaster.DeclarationDate = (DateTime)dtmDeclareDate.Value;
					//voMaster.DeclarationDate = DateTime.Parse(dtmDeclareDate.Value.ToString());
				}
				else
				{
					voMaster.DeclarationDate = DateTime.MinValue;
				}

				//Inform Date
				if(!dtmInformDate.ValueIsDbNull)
				{
					voMaster.InformDate = (DateTime)dtmInformDate.Value;
					//voMaster.InformDate = DateTime.Parse(dtmInformDate.Value.ToString());
				}
				else
				{
					voMaster.InformDate = DateTime.MinValue;
				}

				//Before VAT amount
				if(!numTotalBeforeVAT.ValueIsDbNull)
				{
					voMaster.TotalBeforeVATAmount = decimal.Parse(numTotalBeforeVAT.Value.ToString());
				}
				
				//CIF amount
				if(!numTotalCIFAmount.ValueIsDbNull)
				{
					voMaster.TotalCIFAmount = decimal.Parse(numTotalCIFAmount.Value.ToString());				
				}

				//CIP amount
				if(!numTotalCIPAmt.ValueIsDbNull)
				{
					voMaster.TotalCIPAmount = decimal.Parse(numTotalCIPAmt.Value.ToString());
				}
				
				//TotalImportTax amount
				if(!numTotalImpTax.ValueIsDbNull)
				{
					voMaster.TotalImportTax = decimal.Parse(numTotalImpTax.Value.ToString());
				}

				//TotalInlandAmt amount
				if(!numTotalInlandAmt.ValueIsDbNull)
				{
					voMaster.TotalInlandAmount = decimal.Parse(numTotalInlandAmt.Value.ToString());
				}

				//TotalVATAmount amount
				if(!numTotalVatAmount.ValueIsDbNull)
				{
					voMaster.TotalVATAmount = decimal.Parse(numTotalVatAmount.Value.ToString());
				}
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Validate data before updating into database
		/// </summary>
		/// <returns></returns>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private bool ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			try
			{
				if (cboCCN.SelectedIndex <0)
				{					
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					cboCCN.Focus();				
					return false;
				}				

				//Check As Of date
				if (dtmPostDate.ValueIsDbNull || (dtmPostDate.Text == string.Empty))
				{					
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					dtmPostDate.Focus();				
					return false;
				}

				#region no need to check period

//				if (!FormControlComponents.CheckDateInCurrentPeriod((DateTime)dtmPostDate.Value))
//				{
//					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD, MessageBoxIcon.Exclamation);
//					dtmPostDate.Focus();
//					return false;
//				}

				#endregion

				//Check Cycle no
				if (FormControlComponents.CheckMandatory(txtInvoiceNo))
				{					
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtInvoiceNo.Focus();				
					return false;
				}
				
				if(boMaster.GetObjectVO(txtInvoiceNo.Text) != null && !txtInvoiceNo.Text.Trim().ToUpper().Equals(voMaster.InvoiceNo.ToUpper()))
				{					
					PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtInvoiceNo.Focus();
					return false;
				}
				
				//Check Cycle no
				if (FormControlComponents.CheckMandatory(txtCurrency))
				{					
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCurrency.Focus();				
					return false;
				}				
				
				if (numExchRate.ValueIsDbNull)
				{					
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					numExchRate.Focus();				
					return false;
				}

				if(!FormControlComponents.IsPositiveNumeric(numExchRate.Value.ToString())
					|| decimal.Parse(numExchRate.Value.ToString()) <= 0)
				{					
					PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					numExchRate.Focus();				
					return false;
				}
				
				//Check Cycle no
				if (FormControlComponents.CheckMandatory(txtVendor))
				{					
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtVendor.Focus();				
					return false;
				}
				
				//Call update data to force grid update data
				dgrdData.UpdateData();

				//Check data in the grid
				int iRowCount = GetActualRowCount(dtbDetail);
				if(iRowCount == 0)
				{					
					PCSMessageBox.Show(ErrorCode.MESSAGE_PLEASE_ENTER_DETAIL_INFOR, MessageBoxIcon.Exclamation);
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD]);
					dgrdData.Row = 0;
					dgrdData.Focus();
					return false;
				}
				
				//variable to indicate grid's row index
				int intRowIndex = -1;

				//Collect PO Detail ID to compare approved date
				string strPODetailIDs = "0";

				foreach (DataRow drowRow in dtbDetail.Rows)
				{
					//Inorge deleted row
					if(drowRow.RowState == DataRowState.Deleted) continue;

					intRowIndex++;

					if(drowRow[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].Equals(DBNull.Value)
					|| drowRow[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].ToString().Equals(string.Empty))
					{
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
						dgrdData.Row = intRowIndex;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD]);
						dgrdData.Focus();
						return false;
					}

					if(!FormControlComponents.IsPositiveNumeric(drowRow[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].ToString()))
					{						
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
						dgrdData.Row = intRowIndex;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD]);
						dgrdData.Focus();
						return false;
					}

					if(decimal.Parse(drowRow[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].ToString()) == 0)
					{						
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
						dgrdData.Row = intRowIndex;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD]);
						dgrdData.Focus();
						return false;
					}

					if(!FormControlComponents.IsPositiveNumeric(drowRow[PO_InvoiceDetailTable.UNITPRICE_FLD].ToString()))
					{						
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
						dgrdData.Row = intRowIndex;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_InvoiceDetailTable.UNITPRICE_FLD]);
						dgrdData.Focus();
						return false;
					}

					if(decimal.Parse(drowRow[PO_InvoiceDetailTable.UNITPRICE_FLD].ToString()) == 0)
					{						
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
						dgrdData.Row = intRowIndex;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_InvoiceDetailTable.UNITPRICE_FLD]);
						dgrdData.Focus();
						return false;
					}

					strPODetailIDs += ", " + drowRow[PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD].ToString();
				}
				
				DateTime dtmApprovedDate = boMaster.GetEarliestApprovedDate(strPODetailIDs);

				if(((DateTime)dtmPostDate.Value) < dtmApprovedDate)
				{
					string[] arrMessage = new string[2];
					arrMessage[0] = lblOrderDate.Text;
					arrMessage[1] = lblPOApprovalDate.Text + " (" + dtmApprovedDate.ToString(Constants.DATETIME_FORMAT) + ")";

					PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Exclamation, arrMessage);
					//dtmPostDate.Value = dtmApprovedDate;
					dtmPostDate.Focus();
					return false;
				}

				return true;				
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		

		/// <summary>
		/// Lock controls for updating
		/// </summary>
		/// <param name="pblnLock"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 04 Otc 2005
		/// </created>
		private void LockControl(bool pblnLock)
		{
			try
			{	
				int iRowCount = GetActualRowCount(dtbDetail);

				btnCarrier.Enabled = !pblnLock;				
				btnCurrency.Enabled = !pblnLock;
				btnDeliveryTerms.Enabled = !pblnLock;
				btnPayTerms.Enabled = !pblnLock;
				btnVendor.Enabled = !pblnLock;
				btnVendorName.Enabled = !pblnLock;
				btnSave.Enabled = !pblnLock;
				dtmPostDate.Enabled = !pblnLock;

				txtBLNo.Enabled = !pblnLock;
				txtCarrier.Enabled = !pblnLock;
				txtCurrency.Enabled = !pblnLock;
				txtDeclaredNo.Enabled = !pblnLock;
				txtDeliveryTerms.Enabled = !pblnLock;				
				txtPayTerms.Enabled = !pblnLock;
				txtTaxInformNo.Enabled = !pblnLock;
				txtVendor.Enabled = !pblnLock;
				txtVendorName.Enabled = !pblnLock;
				dtmBLDate.Enabled = !pblnLock;
				dtmDeclareDate.Enabled = !pblnLock;
				dtmInformDate.Enabled = !pblnLock;				
				dtmPostDate.Enabled = !pblnLock;

				numExchRate.Enabled = !pblnLock && !voMaster.CurrencyID.Equals(SystemProperty.DefaultCurrencyID);//false;
				
				btnInvoiceNo.Enabled = pblnLock;
				btnAdd.Enabled = pblnLock;
				btnEdit.Enabled = pblnLock && (iRowCount > 0);
				btnDelete.Enabled = pblnLock && (iRowCount > 0);
				btnPrint.Enabled = pblnLock && (iRowCount > 0);
				
				dgrdData.AllowAddNew = !pblnLock;
				dgrdData.AllowUpdate = !pblnLock;
				dgrdData.AllowDelete = !pblnLock;				

				dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].Button = !pblnLock;
				
				if(!pblnLock)
				{
					dtmPostDate.Focus();
				}
				else
				{
					txtInvoiceNo.Focus();
				}
			}
			catch
			{}
		}

		/// <summary>
		/// Reset value of controls for updating
		/// </summary>
		/// <param name="penuFormAction"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private void ResetControlValue(EnumAction penuFormAction)
		{
			const string METHOD_NAME = THIS +  ".ResetControlValue()";

			try
			{
				//if action id add then get default value for Post Date and TransNo
				if(penuFormAction == EnumAction.Add)
				{
					UtilsBO boUtil = new UtilsBO();
					dtmPostDate.Value = boUtil.GetDBDate();
					//txtInvoiceNo.Text = boUtil.GetNoByMask(PO_InvoiceMasterTable.TABLE_NAME, PO_InvoiceMasterTable.INVOICENO_FLD, DateTime.Parse(dtmPostDate.Value.ToString()), string.Empty);
					txtInvoiceNo.Text = FormControlComponents.GetNoByMask(this);
				}	
				else
				{
					dtmPostDate.Value = DBNull.Value;
					txtInvoiceNo.Text = string.Empty;
				}
				
				txtInvoiceNo.Tag = ZERO_STRING;
				txtDeclaredNo.Text = string.Empty;
				txtTaxInformNo.Text = string.Empty;
				txtVendorName.Text = string.Empty;
				txtBLNo.Text = string.Empty;

				numExchRate.Value = DBNull.Value;
				numTotalCIPAmt.Value = DBNull.Value;
				numTotalBeforeVAT.Value = DBNull.Value;
				numTotalImpTax.Value = DBNull.Value;
				numTotalInlandAmt.Value = DBNull.Value;
				numTotalCIFAmount.Value = DBNull.Value;
				numTotalVatAmount.Value = DBNull.Value;
                				
				dtmBLDate.Value = DBNull.Value;
				dtmDeclareDate.Value = DBNull.Value;
				dtmInformDate.Value = DBNull.Value;
				
                txtCurrency.Text = string.Empty;				
				txtCurrency.Tag = ZERO_STRING;
				
				txtDeliveryTerms.Text = string.Empty;
				txtDeliveryTerms.Tag = ZERO_STRING;

				txtPayTerms.Text = string.Empty;
				txtPayTerms.Tag = ZERO_STRING;

				txtCarrier.Text = string.Empty;
				txtCarrier.Tag = ZERO_STRING;
				
				txtVendor.Text = string.Empty;
				txtVendorName.Text = string.Empty;
				txtVendor.Tag = ZERO_STRING;

				voMaster = new PO_InvoiceMasterVO();
				voMaster.InvoiceMasterID = 0;
				voMaster.InvoiceNo = string.Empty;
			
				//Create blank table then bind to grid
				dtbDetail = BuildDetailTable();
				dgrdData.DataSource = dtbDetail;
				FormatDataGrid();				
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}

		}
		
		/// <summary>
		/// This method will retrieve OutsideProcessing information to fill into controls
		/// in the form, including detail data for the grid
		/// </summary>
		/// <param name="pintMasterId"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 August 2005
		/// </created>		
		private void LoadDetailGrid(int pintMasterId)
		{
			const string METHOD_NAME = THIS + ".LoadDetailGrid()";
			try
			{
				//Load blank data if master id is invalid id 
				if(pintMasterId <= 0)
				{
					//create blank detail table
					dtbDetail = BuildDetailTable();
				}
				else
				{	
					//call bo's method tho retrieve data					
					dtbDetail = boMaster.GetDetailByMaster(pintMasterId).Tables[0];
				}

				// bind to grid & reformat grid
				dgrdData.DataSource = dtbDetail;				
				FormatDataGrid();
			}
			catch(PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Update CIF Amount column's value of in grid
		/// <usage> Call when update in grid</usage>
		/// </summary>
		private void CalculateCIFAmount()
		{	
			//CIF Amount = Quantity * Unit Price; System update Total CIF Amount
			try
			{
				decimal decQuantity = Decimal.Zero;
				decimal decUnitPrice = Decimal.Zero;

				if(!dgrdData[intCurrentRow, PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].Equals(DBNull.Value)
				&& !dgrdData[intCurrentRow, PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].ToString().Equals(string.Empty))
				{
					decQuantity = decimal.Parse(dgrdData[intCurrentRow, PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].ToString());
				}

				if(!dgrdData[intCurrentRow, PO_InvoiceDetailTable.UNITPRICE_FLD].Equals(DBNull.Value)
				&& !dgrdData[intCurrentRow, PO_InvoiceDetailTable.UNITPRICE_FLD].ToString().Equals(string.Empty))
				{
					decUnitPrice = decimal.Parse(dgrdData[intCurrentRow, PO_InvoiceDetailTable.UNITPRICE_FLD].ToString());
				}
				//Update CIF amount
				dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIFAMOUNT_FLD]=  decQuantity * decUnitPrice;

				// Calculate Total CIF amount
				decimal decTotalCIFAmount = Decimal.Zero;
				int iRowCount = GetActualRowCount(dtbDetail);

				for(int i = 0; i < iRowCount; i++)
				{
					if(!dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIFAMOUNT_FLD].Equals(DBNull.Value))
					{
						decTotalCIFAmount += decimal.Parse(dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIFAMOUNT_FLD].ToString());
					}
				}

				//Update Total CIF Amount
				numTotalCIFAmount.Value = decTotalCIFAmount;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Update TotalCIFAmount
		/// <usage> Call when update in grid or delete row</usage>
		/// </summary>
		private void CalculateTotalCIFAmount(int pintRowCount)
		{
			try
			{				
				// Calculate Total CIF amount
				decimal decTotalCIFAmount = Decimal.Zero;				
				for(int i = 0; i < pintRowCount; i++)
				{
					if(!dgrdData[i, PO_InvoiceDetailTable.CIFAMOUNT_FLD].Equals(DBNull.Value)
					&& !dgrdData[i, PO_InvoiceDetailTable.CIFAMOUNT_FLD].ToString().Equals(string.Empty))
					{
						decTotalCIFAmount += decimal.Parse(dgrdData[i, PO_InvoiceDetailTable.CIFAMOUNT_FLD].ToString());
					}
				}

				//Update Total CIF Amount
				numTotalCIFAmount.Value = decTotalCIFAmount;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Calculate value of ImportAmount column
		/// <usage> Call when update in grid</usage>
		/// </summary>
		private void CalculateImportAmount()
		{			
			//Imp Amount = CIF Amount * 0.01 * % Imp; System update Total Imp Amount
			try
			{
				decimal decImportVAT = Decimal.Zero;
				decimal decCIFAmount = Decimal.Zero;

				if(!dgrdData[intCurrentRow, PO_InvoiceDetailTable.IMPORTTAX_FLD].Equals(DBNull.Value)
				&& !dgrdData[intCurrentRow, PO_InvoiceDetailTable.IMPORTTAX_FLD].ToString().Equals(string.Empty))
				{
					decImportVAT = decimal.Parse(dgrdData[intCurrentRow, PO_InvoiceDetailTable.IMPORTTAX_FLD].ToString());
				}

				if(!dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIFAMOUNT_FLD].Equals(DBNull.Value)
				&& !dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIFAMOUNT_FLD].ToString().Equals(string.Empty))
				{
					decCIFAmount = decimal.Parse(dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIFAMOUNT_FLD].ToString());
				}
				//Calculate Import amount
				dgrdData[intCurrentRow, PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD]=  decCIFAmount * (decimal)0.01 * decImportVAT;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Update TotalCIFAmount
		/// <usage> Call when update in grid or delete row</usage>
		/// </summary>
		private void CalculateTotalImportAmount(int pintRowCount)
		{
			try
			{				
				// Calculate Total Import amount
				decimal decTotalImportAmount = Decimal.Zero;				
				for(int i = 0; i < pintRowCount; i++)
				{
					if(!dgrdData[i, PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD].Equals(DBNull.Value)
					&& !dgrdData[i, PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD].ToString().Equals(string.Empty))
					{
						decTotalImportAmount += decimal.Parse(dgrdData[i, PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD].ToString());
					}
				}

				//Update Total CIF Amount
				numTotalImpTax.Value = decTotalImportAmount;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Calculate value of BeforeVATAmount column
		/// <usage> Call when update in grid</usage>
		/// </summary>
		private void CalculateBeforeVATAmount()
		{			
			//Before VAT = CIF Amount + Imp Amount; System update Total Before VAT Amount
            decimal decImportAmount = Decimal.Zero;
            decimal decCIFAmount = Decimal.Zero;

            if (!dgrdData[intCurrentRow, PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD].Equals(DBNull.Value)
            && !dgrdData[intCurrentRow, PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD].ToString().Equals(string.Empty))
            {
                decImportAmount = decimal.Parse(dgrdData[intCurrentRow, PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD].ToString());
            }

            if (!dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIFAMOUNT_FLD].Equals(DBNull.Value)
            && !dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIFAMOUNT_FLD].ToString().Equals(string.Empty))
            {
                decCIFAmount = decimal.Parse(dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIFAMOUNT_FLD].ToString());
            }

            //Update BeforeVATAmount
            dgrdData[intCurrentRow, PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD] = decCIFAmount + decImportAmount;
		}
		
		/// <summary>
		/// Update TotalCIFAmount
		/// <usage> Call when update in grid or delete row</usage>
		/// </summary>
		private void CalculateTotalBeforeVATAmount(int pintRowCount)
		{
            // Calculate Total BeforeVATAmount
            decimal decTotalBeforeVATAmount = Decimal.Zero;
            for (int i = 0; i < pintRowCount; i++)
            {
                if (!dgrdData[i, PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD].Equals(DBNull.Value)
                && !dgrdData[i, PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD].ToString().Equals(string.Empty))
                {
                    decTotalBeforeVATAmount += decimal.Parse(dgrdData[i, PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD].ToString());
                }
            }

            //Update Total BeforeVATAmount
            numTotalBeforeVAT.Value = decTotalBeforeVATAmount;
		}
		
		/// <summary>
		/// Calculate value of VATAmount column
		/// <usage> Call when update in grid</usage>
		/// </summary>
		private void CalculateVATAmount()
		{			
			//VAT Amount = Before VAT * 0.01 * % VAT; System update Total VAT Amount
            decimal decBeforVATAmount = Decimal.Zero;
            decimal decVAT = Decimal.Zero;

            if (!dgrdData[intCurrentRow, PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD].Equals(DBNull.Value)
            && !dgrdData[intCurrentRow, PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD].ToString().Equals(string.Empty))
            {
                decBeforVATAmount = decimal.Parse(dgrdData[intCurrentRow, PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD].ToString());
            }

            if (!dgrdData[intCurrentRow, PO_InvoiceDetailTable.VAT_FLD].Equals(DBNull.Value)
            && !dgrdData[intCurrentRow, PO_InvoiceDetailTable.VAT_FLD].ToString().Equals(string.Empty))
            {
                decVAT = decimal.Parse(dgrdData[intCurrentRow, PO_InvoiceDetailTable.VAT_FLD].ToString());
            }

            //Calculate VATAmount
            dgrdData[intCurrentRow, PO_InvoiceDetailTable.VATAMOUNT_FLD] = decBeforVATAmount * (decimal)0.01 * decVAT;
		}
		
		/// <summary>
		/// Update TotalCIFAmount
		/// <usage> Call when update in grid or delete row</usage>
		/// </summary>
		private void CalculateTotalVATAmount(int pintRowCount)
		{
            // Calculate Total VAT amount
            decimal decTotalVATAmount = Decimal.Zero;
            for (int i = 0; i < pintRowCount; i++)
            {
                if (!dgrdData[i, PO_InvoiceDetailTable.VATAMOUNT_FLD].Equals(DBNull.Value)
                && !dgrdData[i, PO_InvoiceDetailTable.VATAMOUNT_FLD].ToString().Equals(string.Empty))
                {
                    decTotalVATAmount += decimal.Parse(dgrdData[i, PO_InvoiceDetailTable.VATAMOUNT_FLD].ToString());
                }
            }

            //Update Total VAT Amount
            numTotalVatAmount.Value = decTotalVATAmount;
		}
		
		/// <summary>
		/// Calculate value of CIPAmount column
		/// <usage> Call when update in grid</usage>
		/// </summary>
		private void CalculateCIPAmount()
		{
			//CIP = CIF + Inland; System update Total Inland Amount; System update Total CIP Amount
			try
			{
				decimal decCIF = Decimal.Zero;
				decimal decInland = Decimal.Zero;

				if(!dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIFAMOUNT_FLD].Equals(DBNull.Value)
				&& !dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIFAMOUNT_FLD].ToString().Equals(string.Empty))
				{
					decCIF = decimal.Parse(dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIFAMOUNT_FLD].ToString());
				}

				if(!dgrdData[intCurrentRow, PO_InvoiceDetailTable.INLAND_FLD].Equals(DBNull.Value)
				&& !dgrdData[intCurrentRow, PO_InvoiceDetailTable.INLAND_FLD].ToString().Equals(string.Empty))
				{
					decInland = decimal.Parse(dgrdData[intCurrentRow, PO_InvoiceDetailTable.INLAND_FLD].ToString());
				}

				//Calculate CIP Amount
				dgrdData[intCurrentRow, PO_InvoiceDetailTable.CIPAMOUNT_FLD]=  decCIF + decInland;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Update TotalCIFAmount
		/// <usage> Call when update in grid or delete row</usage>
		/// </summary>
		private void CalculateTotalCIPAmount(int pintRowCount)
		{
			try
			{				
				// Calculate Total CIP amount
				decimal decTotalCIPAmount = Decimal.Zero;
				for(int i = 0; i < pintRowCount; i++)
				{
					if(!dgrdData[i, PO_InvoiceDetailTable.CIPAMOUNT_FLD].Equals(DBNull.Value)
					&& !dgrdData[i, PO_InvoiceDetailTable.CIPAMOUNT_FLD].ToString().Equals(string.Empty))
					{
						decTotalCIPAmount += decimal.Parse(dgrdData[i, PO_InvoiceDetailTable.CIPAMOUNT_FLD].ToString());
					}
				}

				//Update Total CIP Amount
				numTotalCIPAmt.Value = decTotalCIPAmount;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Update TotalCIFAmount
		/// <usage> Call when update in grid or delete row</usage>
		/// </summary>
		private void CalculateTotalInlandAmount(int pintRowCount)
		{
			try
			{				
				// Calculate Total Inland amount
				decimal decTotalInLandAmount = Decimal.Zero;
				for(int i = 0; i < pintRowCount; i++)
				{
					if(!dgrdData[i, PO_InvoiceDetailTable.INLAND_FLD].Equals(DBNull.Value)
					&& !dgrdData[i, PO_InvoiceDetailTable.INLAND_FLD].ToString().Equals(string.Empty))
					{
						decTotalInLandAmount += decimal.Parse(dgrdData[i, PO_InvoiceDetailTable.INLAND_FLD].ToString());
					}
				}

				//Update Total Inland Amount
				numTotalInlandAmt.Value = decTotalInLandAmount;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// process data inputed into the grid
		/// </summary>
		/// <param name="pstrColumnName"></param>
		/// <param name="pstrColumValue"></param>
		/// <returns>If no error then return true, else return false</returns>
		private bool ProcessInputDataInGrid(string pstrColumnName, string pstrColumValue, bool pblnAlwaysShow)
		{
			const string METHOD_NAME = THIS +  ".ProcessInputDataInGrid()";
			try
			{	
				bool blnResult = true;
				Hashtable htbCondition = new Hashtable();
				Hashtable htbSelectedRows;

				//Check Cycle no
				if (cboCCN.SelectedIndex < 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_SELECT_CNN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					cboCCN.Focus();				
					return false;
				}

				if (FormControlComponents.CheckMandatory(txtVendor))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INPUT_VENDOR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtVendor.Focus();				
					return false;
				}

				//Check for each column
				switch (pstrColumnName)
				{
					case PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD:					
						// 
						if(dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INVOICEDETAILID_FLD] != DBNull.Value
							&& dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INVOICEDETAILID_FLD].ToString() != string.Empty)
						{
							//Check if Invoice has been receipt
							POInvoiceBO boPOInvoice = new POInvoiceBO();
							if (boPOInvoice.CheckIfInvoiceHasBeenReceipt(int.Parse(dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INVOICEDETAILID_FLD].ToString())))
							{
								return false;
							}
						}
						//clear hash table for new condition
						htbCondition.Clear();
						htbCondition.Add(PO_PurchaseOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue);
						htbCondition.Add(PO_PurchaseOrderMasterTable.PARTYID_FLD, txtVendor.Tag);						
						// Call OpenSearchForm for Work Order Line selecting

						SelectPurchaseOrders frmSelect = new SelectPurchaseOrders(htbCondition);
						frmSelect.ShowDialog();

						htbSelectedRows = frmSelect.SelectedRows;
						if(htbSelectedRows == null)
						{
							blnResult = pblnAlwaysShow;
						}
						else
						{
							if (htbSelectedRows.Count != 0)
							{
								AddSelectedData2DataGrid(htbSelectedRows);								
							}
							else
							{
								blnResult = pblnAlwaysShow;
							}
						}

						break;
					
					case PO_InvoiceDetailTable.INVOICEQUANTITY_FLD:
						//Check if invoice amount is a positive numeric
						if(pstrColumValue.Length != 0)
						{
							if(!FormControlComponents.IsPositiveNumeric(pstrColumValue) || pstrColumValue.Equals(ZERO_STRING))
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD]);
								dgrdData.Focus();							
								blnResult = false;
							}						
						}
						break;
					
					case PO_InvoiceDetailTable.UNITPRICE_FLD:
						//Check if invoice amount is a positive numeric
						if(pstrColumValue.Length != 0)
						{
							if(!FormControlComponents.IsPositiveNumeric(pstrColumValue) || pstrColumValue.Equals(ZERO_STRING))
							{
								if(pstrColumValue.Equals(ZERO_STRING)) break; // do nothing
								PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_InvoiceDetailTable.UNITPRICE_FLD]);
								dgrdData.Focus();							
								blnResult = false;
							}
						}
						break;

					case PO_InvoiceDetailTable.IMPORTTAX_FLD:
						if(pstrColumValue.Length != 0)
						{
							//Check if invoice amount is a positive numeric
							if(!FormControlComponents.IsValidPercent(pstrColumValue))
							{
								//HACK: VAT may be zero
								decimal dblTempVAT = decimal.Zero; 
								try
								{
									dblTempVAT = decimal.Parse(pstrColumValue);
								}
								catch
								{
									dblTempVAT = decimal.MinusOne;
								}
								
								if(dblTempVAT != decimal.Zero)
								{								
									PCSMessageBox.Show(ErrorCode.MESSAGE_C1NUMBER_INPUT_VALUE, MessageBoxIcon.Exclamation, new string[]{PERCENT_VALUE_RANGE});
									dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_InvoiceDetailTable.IMPORTTAX_FLD]);
									dgrdData.Focus();							
									blnResult = false;
								}
							}
						}
						break;

					case PO_InvoiceDetailTable.VAT_FLD:
						if(pstrColumValue.Length != 0)
						{
							//Check if invoice amount is a positive numeric
							if(!FormControlComponents.IsValidPercent(pstrColumValue))
							{
								//HACK: VAT may be zero
								decimal dblTempVAT = decimal.Zero; 
								try
								{
									dblTempVAT = decimal.Parse(pstrColumValue);
								}
								catch
								{
									dblTempVAT = decimal.MinusOne;
								}
								
								if(dblTempVAT != decimal.Zero)
								{									
									PCSMessageBox.Show(ErrorCode.MESSAGE_C1NUMBER_INPUT_VALUE, MessageBoxIcon.Exclamation, new string[]{PERCENT_VALUE_RANGE});
									dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_InvoiceDetailTable.VAT_FLD]);
									dgrdData.Focus();
									blnResult = false;
								}
								//End hack
							}
						}
						break;

					case PO_InvoiceDetailTable.INLAND_FLD:
						if(pstrColumValue.Length != 0)
						{
							//Check if invoice amount is a positive numeric
							if(!FormControlComponents.IsPositiveNumeric(pstrColumValue) && !pstrColumValue.Equals(ZERO_STRING))
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_InvoiceDetailTable.INLAND_FLD]);
								dgrdData.Focus();
								blnResult = false;
							}
						}						
						break;
				}

				return blnResult;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Calculate data in the grid
		/// </summary>
		/// <param name="pstrColumnName"></param>
		private void CalculateDataInGrid(string pstrColumnName)
		{
			const string METHOD_NAME = THIS +  ".CalculateDataInGrid()";
			try
			{
				//Get actual number of rows
				int intRowCount = GetActualRowCount(dtbDetail);

				//Check for each column
				switch (pstrColumnName)
				{
					case PO_InvoiceDetailTable.INVOICEQUANTITY_FLD:
						//User enter Unit Price (system do not allow user Price < 0)
						//Update value for CIF Amount = Quantity * Unit Price; System update Total CIF Amount
						CalculateCIFAmount();
						CalculateImportAmount();
						CalculateBeforeVATAmount();
						CalculateVATAmount();
						CalculateCIPAmount();
						//Update total
						CalculateTotalCIFAmount(intRowCount);
						CalculateTotalImportAmount(intRowCount);
						CalculateTotalBeforeVATAmount(intRowCount);
						CalculateTotalVATAmount(intRowCount);
						CalculateTotalCIPAmount(intRowCount);
						
						break;
					
					case PO_InvoiceDetailTable.UNITPRICE_FLD:
						//User enter Unit Price (system do not allow user Price < 0)
						//Update value for CIF Amount = Quantity * Unit Price; System update Total CIF Amount
						CalculateCIFAmount();
						CalculateImportAmount();
						CalculateBeforeVATAmount();
						CalculateVATAmount();
						CalculateCIPAmount();
						//Update total
						CalculateTotalCIFAmount(intRowCount);
						CalculateTotalImportAmount(intRowCount);
						CalculateTotalBeforeVATAmount(intRowCount);
						CalculateTotalVATAmount(intRowCount);
						CalculateTotalCIPAmount(intRowCount);
						
						break;

					case PO_InvoiceDetailTable.IMPORTTAX_FLD:
						//31. System auto update value for Imp Amount
						//32. System auto update value for Before VAT = CIF Amount + Imp Amount
						CalculateImportAmount();
						CalculateBeforeVATAmount();
						CalculateVATAmount();
						CalculateCIPAmount();
						//Update total						
						CalculateTotalImportAmount(intRowCount);
						CalculateTotalBeforeVATAmount(intRowCount);
						CalculateTotalVATAmount(intRowCount);
						CalculateTotalCIPAmount(intRowCount);
						
						break;

					case PO_InvoiceDetailTable.VAT_FLD:
						//System auto update value for VAT Amount = Before VAT * 0.01 * % VAT;
						CalculateVATAmount();
						CalculateCIPAmount();
						//Update total
						CalculateTotalVATAmount(intRowCount);
						CalculateTotalCIPAmount(intRowCount);
						
						break;

					case PO_InvoiceDetailTable.INLAND_FLD:
						//System auto update value for CIP = CIF + Inland;
						CalculateCIPAmount();
						//Update total
						CalculateTotalInlandAmount(intRowCount);
						CalculateTotalCIPAmount(intRowCount);
						break;
				}
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Get max Invoice Line number
		/// </summary>
		/// <returns></returns>
		private int GetMaxLineInGrid(int pintRowCount)
		{
			try
			{
				int intMaxLine = 0;
				for(int i = 0; i < pintRowCount; i++)
				{					
					if(!dgrdData[i, PO_InvoiceDetailTable.INVOICELINE_FLD].Equals(DBNull.Value)
						&& !dgrdData[i, PO_InvoiceDetailTable.INVOICELINE_FLD].ToString().Equals(string.Empty))
					{
						int itemp = int.Parse(dgrdData[i, PO_InvoiceDetailTable.INVOICELINE_FLD].ToString());
						if(intMaxLine < itemp)
						{
							intMaxLine = itemp;
						}
					}
				}

				return intMaxLine;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		private bool IsDuplicateData(string pstrPOMasterID, string pstrPODetailID, string pstrDeliveryID, int pintRowCount)
		{
			//int intRowCount = GetActualRowCount(dtbDetail);

			for(int i = 0; i< pintRowCount - 1; i++)
			{
				if( dgrdData[i, PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].ToString() == pstrPOMasterID
					&& dgrdData[i, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString() == pstrPODetailID
					&& dgrdData[i, PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString() == pstrDeliveryID)
					return true;
			}

			return false;			
		}
		
		/// <summary>
		/// Add selected rows into data grid
		/// </summary>
		/// <param name="htbData"></param>
		private void AddSelectedData2DataGrid(Hashtable htbData)
		{			
			try
			{
				if(htbData == null) return;
				
				//Get actual number of rows
				int intRowCount = GetActualRowCount(dtbDetail);

				//get max line
				int intMaxLine = GetMaxLineInGrid(intRowCount);
				
				CurrencyManager currencyManger;
				currencyManger = (CurrencyManager) dgrdData.BindingContext[dgrdData.DataSource];				
				dgrdData.Focus();
				
				//not duplicate --> insert into grid
				dgrdData.EditActive = true;

				//loop through selected rows
				for(int i = 0; i< htbData.Count; i++)
				{					
					DataRow drowSourceData = (DataRow)htbData[i];												
					
					//Check if selected row is duplicated
					bool blnDuplicate = IsDuplicateData(drowSourceData[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].ToString(),
									   drowSourceData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString(),
									   drowSourceData[PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString(), intRowCount);
					if(blnDuplicate)
					{
						continue;
					}

					currencyManger.AddNew();					
					dgrdData.Row = intMaxLine;	
					
					//Increase row count
					intRowCount++;

					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INVOICELINE_FLD] = ++intMaxLine;					 
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INVOICEMASTERID_FLD] = voMaster.InvoiceMasterID;
					dgrdData[dgrdData.Row, PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD] = drowSourceData[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD];
					dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD] = drowSourceData[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD];
					dgrdData[dgrdData.Row, PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = drowSourceData[PO_DeliveryScheduleTable.DELIVERYLINE_FLD];
					dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = drowSourceData[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD];
					dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = drowSourceData[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD];
					dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = drowSourceData[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD];
					dgrdData[dgrdData.Row, ITM_ProductTable.TAXCODE_FLD] = drowSourceData[ITM_ProductTable.TAXCODE_FLD];
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INVOICEQUANTITY_FLD] = drowSourceData[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.UNITPRICE_FLD] = drowSourceData[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.UNITPRICE_FLD];
					dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drowSourceData[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];					
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.IMPORTTAX_FLD] = drowSourceData[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.IMPORTTAX_FLD];					
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.VAT_FLD] = drowSourceData[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.VAT_FLD];					
					dgrdData[dgrdData.Row, ITM_ProductTable.OTHERINFO1_FLD]  = drowSourceData[ITM_ProductTable.OTHERINFO1_FLD];
					dgrdData[dgrdData.Row, ITM_ProductTable.PARTNAMEVN_FLD] = drowSourceData[ITM_ProductTable.PARTNAMEVN_FLD];
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.NOTE_FLD] = string.Empty;
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.PRODUCTID_FLD] = drowSourceData[PO_InvoiceDetailTable.PRODUCTID_FLD];
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD] = drowSourceData[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD];
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD] = drowSourceData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD];
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD] = drowSourceData[PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD];
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INVOICEUMID_FLD] = drowSourceData[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD];
					dgrdData[dgrdData.Row, MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD] = drowSourceData[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD];
					
					dgrdData[dgrdData.Row, PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD] = drowSourceData[PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD];
					
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INLAND_FLD] = Decimal.Zero;
					//dgrdData.UpdateData();

					//Update value of calculated-fields
					decimal decValue1 = Decimal.Zero;
					decimal decValue2 = Decimal.Zero;

					//CIF Amount = Quantity * Unit Price
					if(!dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].Equals(DBNull.Value)
					&& !dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].ToString().Equals(string.Empty))
					{
						decValue1 = decimal.Parse(dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].ToString());
					}

					if(!dgrdData[dgrdData.Row, PO_InvoiceDetailTable.UNITPRICE_FLD].Equals(DBNull.Value)
					&& !dgrdData[dgrdData.Row, PO_InvoiceDetailTable.UNITPRICE_FLD].ToString().Equals(string.Empty))
					{
						decValue2 = decimal.Parse(dgrdData[dgrdData.Row, PO_InvoiceDetailTable.UNITPRICE_FLD].ToString());
					}
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.CIFAMOUNT_FLD] = decValue1 * decValue2;					
					
					//----------------------------------------------------------------------
					decValue1 *= decValue2; // = CIF Amount, see line above

					//----------------------------------------------------------------------					
					if(!dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INLAND_FLD].Equals(DBNull.Value)
						&& !dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INLAND_FLD].ToString().Equals(string.Empty))
					{
						decValue2 = decimal.Parse(dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INLAND_FLD].ToString());
					}
					else
					{
						decValue2 = Decimal.Zero;
					}
					//CIP = CIF + Inland
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.CIPAMOUNT_FLD] = decValue1 + decValue2;

					//Now decValue1 = CIF Amount
					if(!dgrdData[dgrdData.Row, PO_InvoiceDetailTable.IMPORTTAX_FLD].Equals(DBNull.Value)
					&& !dgrdData[dgrdData.Row, PO_InvoiceDetailTable.IMPORTTAX_FLD].ToString().Equals(string.Empty))
					{
						decValue2 = decimal.Parse(dgrdData[dgrdData.Row, PO_InvoiceDetailTable.IMPORTTAX_FLD].ToString());
					}
					else
					{
						decValue2 = Decimal.Zero;						
					}

					//Imp Amount = CIF Amount * 0.01 * % Imp
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD] = decValue1 * (decimal)0.01 * decValue2;
					
					//----------------------------------------------------------------------
					decValue2 = decValue1 * (decimal)0.01 * decValue2;// = Import Amount, see line above					
					
					//Before VAT = CIF Amount + Imp Amount
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD] = decValue1 + decValue2;
					
					//----------------------------------------------------------------------
					decValue1 += decValue2;// = Before VAT Amount, see line above.

					if(!dgrdData[dgrdData.Row, PO_InvoiceDetailTable.VAT_FLD].Equals(DBNull.Value)
					&& !dgrdData[dgrdData.Row, PO_InvoiceDetailTable.VAT_FLD].ToString().Equals(string.Empty))
					{
						decValue2 = decimal.Parse(dgrdData[dgrdData.Row, PO_InvoiceDetailTable.VAT_FLD].ToString());
					}
					else
					{
						decValue2 = Decimal.Zero;
					}

					//VAT Amount = Before VAT * 0.01 * % VAT
					dgrdData[dgrdData.Row, PO_InvoiceDetailTable.VATAMOUNT_FLD] = decValue1 * (decimal)0.01 * decValue2;					
				}

				currencyManger = null;
				//Turn on EditActive status of grid
				dgrdData.EditActive = false;
				dgrdData.UpdateData();
				dgrdData.Refresh();
				
				//Get actual number of rows
				intRowCount = GetActualRowCount(dtbDetail);

				//Update total values
				CalculateTotalBeforeVATAmount(intRowCount);
				CalculateTotalCIFAmount(intRowCount);
				CalculateTotalCIPAmount(intRowCount);
				CalculateTotalInlandAmount(intRowCount);
				CalculateTotalImportAmount(intRowCount);
				CalculateTotalVATAmount(intRowCount);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Format grid
		/// </summary>
		private void FormatDataGrid()
		{
			string FORMAT_0000 = "00";
			try
			{	
				//Restore layout
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				
				//lock columns
				for(int i= 0; i < dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].Locked = true;
				}

				//dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].Locked = false;
				//dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Locked = false;
				//dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.DELIVERYLINE_FLD].Locked = false;

				dgrdData.Splits[0].DisplayColumns[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].Locked = false;
				dgrdData.Splits[0].DisplayColumns[PO_InvoiceDetailTable.UNITPRICE_FLD].Locked = false;
				dgrdData.Splits[0].DisplayColumns[PO_InvoiceDetailTable.IMPORTTAX_FLD].Locked = false;
				dgrdData.Splits[0].DisplayColumns[PO_InvoiceDetailTable.VAT_FLD].Locked = false;
				dgrdData.Splits[0].DisplayColumns[PO_InvoiceDetailTable.INLAND_FLD].Locked = false;
				dgrdData.Splits[0].DisplayColumns[PO_InvoiceDetailTable.NOTE_FLD].Locked = false;
				
				//assign editor to updatable coulumns
				dgrdData.Columns[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].Editor = numQuantity;
				dgrdData.Columns[PO_InvoiceDetailTable.UNITPRICE_FLD].Editor = numValue;
				dgrdData.Columns[PO_InvoiceDetailTable.IMPORTTAX_FLD].Editor = numValue;
				dgrdData.Columns[PO_InvoiceDetailTable.VAT_FLD].Editor = numValue;
				dgrdData.Columns[PO_InvoiceDetailTable.INLAND_FLD].Editor = numValue;
		
				//Change display format of nummeric fields
				dgrdData.Columns[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PO_InvoiceDetailTable.UNITPRICE_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT+ FORMAT_0000;
				dgrdData.Columns[PO_InvoiceDetailTable.IMPORTTAX_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PO_InvoiceDetailTable.VAT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PO_InvoiceDetailTable.INLAND_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT + FORMAT_0000;
				dgrdData.Columns[PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Columns[PO_InvoiceDetailTable.CIFAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT + FORMAT_0000;
				dgrdData.Columns[PO_InvoiceDetailTable.CIPAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT + FORMAT_0000;
				dgrdData.Columns[PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT + FORMAT_0000;
				dgrdData.Columns[PO_InvoiceDetailTable.VATAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT + FORMAT_0000;

				numValue.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				numValue.CustomFormat = Constants.DECIMAL_NUMBERFORMAT + FORMAT_0000;				
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Build structure of xx table for binding to grid
		/// </summary>
		/// <remarks>
		/// Structure of this table based on struct which be returned by calling
		/// xx.GetDetailByMaster() method.
		/// So we should keep them always are identical.
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildDetailTable()
		{
			try
			{
				//Create table
				DataTable dtbTmpDetail = new DataTable(PO_InvoiceDetailTable.TABLE_NAME);
				
				//Add columns				
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.INVOICEMASTERID_FLD, typeof(System.Int32));
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.INVOICEDETAILID_FLD, typeof(System.Int32));
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.INVOICELINE_FLD, typeof(System.Int32));
				dtbTmpDetail.Columns.Add(PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD, typeof(System.String));
				dtbTmpDetail.Columns.Add(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD, typeof(System.Int32));				
				dtbTmpDetail.Columns.Add(PO_DeliveryScheduleTable.DELIVERYLINE_FLD, typeof(System.Int32)); 
				dtbTmpDetail.Columns.Add(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD, typeof(System.String));				
				dtbTmpDetail.Columns.Add(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD, typeof(System.String));
				dtbTmpDetail.Columns.Add(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD, typeof(System.String));
				dtbTmpDetail.Columns.Add(ITM_ProductTable.TAXCODE_FLD, typeof(System.String));
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.INVOICEQUANTITY_FLD, typeof(System.Decimal));
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.UNITPRICE_FLD, typeof(System.Decimal));
				dtbTmpDetail.Columns.Add(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD, typeof(System.String)); 
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.CIFAMOUNT_FLD, typeof(System.Decimal)); 
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.IMPORTTAX_FLD, typeof(System.Decimal)); 
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD, typeof(System.Decimal)); 				
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD, typeof(System.Decimal)); 
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.VAT_FLD, typeof(System.Decimal)); 
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.VATAMOUNT_FLD, typeof(System.Decimal));				
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.INLAND_FLD, typeof(System.Decimal));
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.CIPAMOUNT_FLD, typeof(System.Decimal)); 
				dtbTmpDetail.Columns.Add(ITM_ProductTable.OTHERINFO1_FLD, typeof(System.String)); 
				dtbTmpDetail.Columns.Add(ITM_ProductTable.PARTNAMEVN_FLD, typeof(System.String)); 
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.NOTE_FLD, typeof(System.String));
				dtbTmpDetail.Columns.Add(MST_PartyTable.TABLE_NAME +  MST_PartyTable.CODE_FLD, typeof(System.String));

				dtbTmpDetail.Columns.Add(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, typeof(System.Decimal));

				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.PRODUCTID_FLD, typeof(System.Int32)); 
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD, typeof(System.Int32)); 
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD, typeof(System.Int32)); 
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD, typeof(System.Int32)); 
				dtbTmpDetail.Columns.Add(PO_InvoiceDetailTable.INVOICEUMID_FLD, typeof(System.Int32));
				dtbTmpDetail.Columns.Add(PO_PurchaseOrderDetailTable.APPROVALDATE_FLD, typeof(System.DateTime));
				
				return dtbTmpDetail;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Fill related data on controls when select Invoice
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectInvoice(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				
				if(cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(PO_InvoiceMasterTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					htbCriteria.Add(PO_InvoiceMasterTable.CCNID_FLD, 0);
				}

				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(DETAILED_INVOICE_MASTER_VIEW, PO_InvoiceMasterTable.INVOICENO_FLD, txtInvoiceNo.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					//Assign value to VO object
					voMaster.InvoiceMasterID = int.Parse(drwResult[PO_InvoiceMasterTable.INVOICEMASTERID_FLD].ToString());
					voMaster.CCNID = int.Parse(drwResult[PO_InvoiceMasterTable.CCNID_FLD].ToString());
					voMaster.PostDate = DateTime.Parse(drwResult[PO_InvoiceMasterTable.POSTDATE_FLD].ToString());
					voMaster.CurrencyID = int.Parse(drwResult[PO_InvoiceMasterTable.CURRENCYID_FLD].ToString());
					voMaster.ExchangeRate = decimal.Parse(drwResult[PO_InvoiceMasterTable.EXCHANGERATE_FLD].ToString());
					voMaster.InvoiceNo = drwResult[PO_InvoiceMasterTable.INVOICENO_FLD].ToString();

					voMaster.TotalBeforeVATAmount = decimal.Parse(drwResult[PO_InvoiceMasterTable.TOTALBEFOREVATAMOUNT_FLD].ToString());
					voMaster.TotalCIPAmount = decimal.Parse(drwResult[PO_InvoiceMasterTable.TOTALCIPAMOUNT_FLD].ToString());
					voMaster.TotalCIFAmount = decimal.Parse(drwResult[PO_InvoiceMasterTable.TOTALCIFAMOUNT_FLD].ToString());
					voMaster.TotalImportTax = decimal.Parse(drwResult[PO_InvoiceMasterTable.TOTALIMPORTTAX_FLD].ToString());
					voMaster.TotalInlandAmount = decimal.Parse(drwResult[PO_InvoiceMasterTable.TOTALINLANDAMOUNT_FLD].ToString());
					voMaster.TotalVATAmount = decimal.Parse(drwResult[PO_InvoiceMasterTable.TOTALVATAMOUNT_FLD].ToString());					
					//Declare Date
					if(!drwResult[PO_InvoiceMasterTable.DECLARATIONDATE_FLD].Equals(DBNull.Value))
					{
						voMaster.DeclarationDate = DateTime.Parse(drwResult[PO_InvoiceMasterTable.DECLARATIONDATE_FLD].ToString());
						dtmDeclareDate.Value = voMaster.DeclarationDate;
					}
					else
					{
						dtmDeclareDate.Value = DBNull.Value;
					}
					
					//BL Date
					if(!drwResult[PO_InvoiceMasterTable.BLDATE_FLD].Equals(DBNull.Value))
					{
						voMaster.BLDate = DateTime.Parse(drwResult[PO_InvoiceMasterTable.BLDATE_FLD].ToString());
						dtmBLDate.Value = voMaster.BLDate;
					}
					else
					{
						dtmBLDate.Value = DBNull.Value;
					}

					//Inform Date
					if(!drwResult[PO_InvoiceMasterTable.INFORMDATE_FLD].Equals(DBNull.Value))
					{
						voMaster.InformDate = DateTime.Parse(drwResult[PO_InvoiceMasterTable.INFORMDATE_FLD].ToString());
						dtmInformDate.Value = voMaster.InformDate;
					}
					else
					{
						dtmInformDate.Value = DBNull.Value;
					}

					//Assign to controls
					dtmPostDate.Value = voMaster.PostDate;
					txtBLNo.Text = drwResult[PO_InvoiceMasterTable.BLNUMBER_FLD].ToString();
					txtDeclaredNo.Text = drwResult[PO_InvoiceMasterTable.TAXDECLARATIONNUMBER_FLD].ToString();
					txtTaxInformNo.Text = drwResult[PO_InvoiceMasterTable.TAXINFORMNUMBER_FLD].ToString();
					txtInvoiceNo.Text = drwResult[PO_InvoiceMasterTable.INVOICENO_FLD].ToString();
					
					txtCurrency.Text = drwResult[MST_CurrencyTable.TABLE_NAME + MST_CurrencyTable.CODE_FLD].ToString();
					txtCurrency.Tag = drwResult[PO_InvoiceMasterTable.CURRENCYID_FLD].ToString();
					numExchRate.Value = voMaster.ExchangeRate;
					
					//Delivery Terms
					if(!drwResult[PO_InvoiceMasterTable.DELIVERYTERMID_FLD].Equals(DBNull.Value))
					{
						txtDeliveryTerms.Text = drwResult[MST_DeliveryTermTable.TABLE_NAME + MST_DeliveryTermTable.CODE_FLD].ToString();
						txtDeliveryTerms.Tag = drwResult[MST_DeliveryTermTable.DELIVERYTERMID_FLD].ToString();
					}
					else
					{
						txtDeliveryTerms.Text = string.Empty;
						txtDeliveryTerms.Tag = ZERO_STRING;
					}
					
					//Pay Terms
					if(!drwResult[PO_InvoiceMasterTable.PAYMENTTERMID_FLD].Equals(DBNull.Value))
					{
						txtPayTerms.Text = drwResult[MST_PaymentTermTable.TABLE_NAME + MST_PaymentTermTable.CODE_FLD].ToString();
						txtPayTerms.Tag = drwResult[PO_InvoiceMasterTable.PAYMENTTERMID_FLD].ToString();
					}
					else
					{
						txtPayTerms.Text = string.Empty;
						txtPayTerms.Tag = ZERO_STRING;
					}
					
					//Carrier 
					if(!drwResult[PO_InvoiceMasterTable.CARRIERID_FLD].Equals(DBNull.Value))
					{
						txtCarrier.Text = drwResult[MST_CarrierTable.TABLE_NAME + MST_CarrierTable.CODE_FLD].ToString();
						txtCarrier.Tag = drwResult[PO_InvoiceMasterTable.CARRIERID_FLD].ToString();
					}
					else
					{
						txtCarrier.Text = string.Empty;
						txtCarrier.Tag = ZERO_STRING;
					}

					//txtTaxInformNo.Text = drwResult[PO_InvoiceMasterTable.BLNUMBER_FLD].ToString();
					
					txtVendor.Tag = drwResult[PO_InvoiceMasterTable.PARTYID_FLD];
					txtVendor.Text = drwResult[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].ToString();
					txtVendorName.Text = drwResult[MST_PartyTable.TABLE_NAME + MST_PartyTable.NAME_FLD].ToString();                    
					
					//Total value
					numTotalBeforeVAT.Value = voMaster.TotalBeforeVATAmount;
					numTotalCIFAmount.Value = voMaster.TotalCIFAmount;
					numTotalCIPAmt.Value = voMaster.TotalCIPAmount;
					numTotalImpTax.Value = voMaster.TotalImportTax;
					numTotalInlandAmt.Value = voMaster.TotalInlandAmount;					
					numTotalVatAmount.Value = voMaster.TotalVATAmount;
					
					//Reset modify status
					txtInvoiceNo.Modified = false;					
				}
				else
				{
					if(!pblnAlwaysShowDialog)
					{
						txtInvoiceNo.Tag = ZERO_STRING;
						txtInvoiceNo.Focus();
						return false;
					}					
				}
				
				//Fill data from datarow to controls				
				LoadDetailGrid(voMaster.InvoiceMasterID);
				LockControl(true);
				return true;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		/// <summary>
		/// Fill exchange rate
		/// </summary>
		/// <param name="pintCurrencyId"></param>
		/// <param name="pdtmDate"></param>
		private void FillExchangeRate(int pintCurrencyId, DateTime pdtmDate)
		{
			const string METHOD_NAME = THIS + ".FillExchangeRate()";
			try				
			{
				if(pintCurrencyId == SystemProperty.DefaultCurrencyID)
				{
					numExchRate.Value = Decimal.One;
					numExchRate.Enabled = false;
					return;
				}

				MST_ExchangeRateVO voExchangeRate = (MST_ExchangeRateVO)boMaster.GetExchangeRate(pintCurrencyId, pdtmDate);
				//Have exchange rate
				if(voExchangeRate.ExchangeRateID > 0)
				{
					numExchRate.Value = voExchangeRate.Rate;
					//numExchRate.Enabled = false;
				}
				else if(pintCurrencyId > 0)
				{
					numExchRate.Value = DBNull.Value;
					//numExchRate.Enabled = true;
				}
				else
				{
					numExchRate.Value = DBNull.Value;
					//numExchRate.Enabled = false;
				}
				numExchRate.Enabled = true;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		/// <summary>
		/// Fill related data on controls when select Currency
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectCurrency(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try
			{				
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;			

				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME, MST_CurrencyTable.CODE_FLD, txtCurrency.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					//Check if data was changed then reassign
					txtCurrency.Text = drwResult[MST_CurrencyTable.CODE_FLD].ToString();
					txtCurrency.Tag = drwResult[MST_CurrencyTable.CURRENCYID_FLD];
					if(!dtmPostDate.ValueIsDbNull)
					{
						FillExchangeRate(int.Parse(txtCurrency.Tag.ToString()), DateTime.Parse(dtmPostDate.Value.ToString()));
					}
					else
					{
						FillExchangeRate(0, DateTime.Now);
					}

					//Reset modify status
					txtCurrency.Modified = false;
				}
				else
				{
					if(!pblnAlwaysShowDialog)
					{
						txtCurrency.Tag = ZERO_STRING;
						txtCurrency.Focus();
						return false;
					}					
				}

				return true;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}


		/// <summary>
		/// Fill related data on controls when select Vendor Code
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectVendorCode(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;				
				
				htbCriteria.Add(VENDOR_COLUMN, 1);

				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(VENDOR_CUSTOMER_VIEW, MST_PartyTable.CODE_FLD, txtVendor.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					if(!txtVendor.Tag.Equals(drwResult[MST_PartyTable.PARTYID_FLD]))
					{
						ClearGrid();
					}

					txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					txtVendor.Tag = drwResult[MST_PartyTable.PARTYID_FLD];

					//Reset modify status
					txtVendor.Modified = false;
					txtVendorName.Modified = false;
				}
				else
				{
					if(!pblnAlwaysShowDialog)
					{
						txtVendor.Tag = ZERO_STRING;
						txtVendor.Focus();

						return false;
					}					
				}
				return true;
			}
			catch(PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}


		/// <summary>
		/// Fill related data on controls when select Vendor Name
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectVendorName(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				
				htbCriteria.Add(VENDOR_COLUMN, 1);
				
				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(VENDOR_CUSTOMER_VIEW, MST_PartyTable.NAME_FLD, txtVendorName.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					if(!txtVendor.Tag.Equals(drwResult[MST_PartyTable.PARTYID_FLD]))
					{
						ClearGrid();
					}

					txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendor.Tag = drwResult[MST_PartyTable.PARTYID_FLD];					

					//Reset modify status
					txtVendorName.Modified = false;
					txtVendor.Modified = false;
				}
				else
				{
					if(!pblnAlwaysShowDialog)
					{
						txtVendorName.Tag = ZERO_STRING;
						txtVendorName.Focus();

						return false;
					}
				}

				return true;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		/// <summary>
		/// Fill related data on controls when select Delivery Terms
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectDeliveryTerm(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try
			{
				

				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;		
				
				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(MST_DeliveryTermTable.TABLE_NAME, MST_DeliveryTermTable.CODE_FLD, txtDeliveryTerms.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					txtDeliveryTerms.Text = drwResult[MST_DeliveryTermTable.CODE_FLD].ToString();
					txtDeliveryTerms.Tag = drwResult[MST_DeliveryTermTable.DELIVERYTERMID_FLD];					
					
					//Reset modify status
					txtDeliveryTerms.Modified = false;
				}
				else
				{
					if(!pblnAlwaysShowDialog)
					{
						txtDeliveryTerms.Tag = ZERO_STRING;
						txtDeliveryTerms.Focus();
						return false;
					}					
				}

				return true;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}


		/// <summary>
		/// Fill related data on controls when select Payment Term
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectPaymentTerm(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				

				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				
				if(cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(MST_PaymentTermTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					htbCriteria.Add(MST_PaymentTermTable.CCNID_FLD, 0);
				}

				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(MST_PaymentTermTable.TABLE_NAME, MST_PaymentTermTable.CODE_FLD, txtPayTerms.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{					
					txtPayTerms.Text = drwResult[MST_PaymentTermTable.CODE_FLD].ToString();
					txtPayTerms.Tag = drwResult[MST_PaymentTermTable.PAYMENTTERMID_FLD];					
					
					//Reset modify status
					txtPayTerms.Modified = false;					
				}
				else
				{
					if(!pblnAlwaysShowDialog)
					{
						txtPayTerms.Tag = ZERO_STRING;
						txtPayTerms.Focus();
						return false;
					}					
				}

				return true;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		
		/// <summary>
		/// Fill related data on controls when select Carrier
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectCarrier(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try
			{
				

				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;			

				//Call OpenSearchForm for selecting MPS planning cycle
				drwResult = FormControlComponents.OpenSearchForm(MST_CarrierTable.TABLE_NAME, MST_CarrierTable.CODE_FLD, txtCarrier.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					//Check if data was changed then reassign
					txtCarrier.Text = drwResult[MST_CarrierTable.CODE_FLD].ToString();
					txtCarrier.Tag = drwResult[MST_CarrierTable.CARRIERID_FLD];
					
					//Reset modify status
					txtCarrier.Modified = false;	
				}
				else
				{
					if(!pblnAlwaysShowDialog)
					{
						txtCarrier.Tag = ZERO_STRING;
						txtCarrier.Focus();
						return false;
					}					
				}

				return true;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		
		#endregion Private Methods

		#region Event Processing
		
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}		

		private void POInvoice_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".POInvoice_Load()";
			try
			{
				enuFormAction = EnumAction.Default;
				
				//Set form security
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();
					return;
				}

				//Load CCN and set default CCN
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();				
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
				
				//Set default CCN for CNN combobox
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
				else
				{
					cboCCN.SelectedIndex = 0;
				}
				
				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);

				//Clear controls value and lock for editing				
				ResetControlValue(enuFormAction);											

				LockControl(true);

				/// HACKED: Thachnn: 06/06/2006 Report COnfiguration
				/// REVIEW: these lines need to be call automatically in the Formload Section, not manual like this point
				this.btnPrintConfiguration.Click += new EventHandler(FormControlComponents.ShowMenuReportListHandler);
				this.btnPrint.Click += new EventHandler(FormControlComponents.RunDefaultReportEntriesHandler);				
				/// ENDHACKED: Thachnn: 06/06/2006 Report COnfiguration
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}

		private void POInvoice_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".POInvoice_KeyDown()";

			try
			{
				
				switch (e.KeyCode)
				{
					case Keys.F12:					
						//if column's value then exit immediately
						if(enuFormAction == EnumAction.Default) return;

						//Get actual number of rows
						int intRowCount = GetActualRowCount(dtbDetail);

						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD]);
						dgrdData.Row = intRowCount;
						dgrdData.Focus();
						break;					
				}
			}
			catch(PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch(Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void POInvoice_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".POInvoice_Closing()";
			try
			{	
				// if the form has been changed then ask to store database
				if(enuFormAction != EnumAction.Default) 
				{
					DialogResult enumDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
					if( enumDialog == DialogResult.Yes)
					{
						btnSave_Click(sender, e);
						e.Cancel = !blnDataIsValid;
					}
					else if( enumDialog == DialogResult.Cancel)//click Cancel button
					{
						e.Cancel = true;
					}
				}				
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// Displays the error message if throwed from system.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}		
		

		private void btnCurrency_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCurrency_Click()";
			try
			{
				SelectCurrency(METHOD_NAME, true);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnVendor_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendor_Click()";
			try
			{
				SelectVendorCode(METHOD_NAME, true);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnVendorName_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendorName_Click()";
			try
			{
				SelectVendorName(METHOD_NAME, true);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnDeliveryTerms_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDeliveryTerms_Click()";
			try
			{
				SelectDeliveryTerm(METHOD_NAME, true);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnPayTerms_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPayTerms_Click()";
			try
			{
				SelectPaymentTerm(METHOD_NAME, true);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnCarrier_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCarrier_Click()";
			try
			{
				SelectCarrier(METHOD_NAME, true);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";

			try
			{
				enuFormAction = EnumAction.Add;				
				ResetControlValue(enuFormAction);
				LockControl(false);
				dtmPostDate.Focus();
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";

			try
			{
				if(Security.IsDifferencePrefix(this,lblInvoiceNo,txtInvoiceNo))
				{
					return;
				}

				// check data, if data is invalid then exit immediately
				blnDataIsValid = ValidateData() && !dgrdData.EditActive;
				if(!blnDataIsValid)
				{
				    return;
				}
				//Reset Invoice line
				int iLineIndex = 0;
				foreach (DataRow drow in dtbDetail.Rows.Cast<DataRow>().Where(drow => drow.RowState != DataRowState.Deleted))
				{
				    drow[PO_InvoiceDetailTable.INVOICELINE_FLD] = ++iLineIndex;
				}

				//Assign value from controls to VO
				AssignControlValue2VO();

				// check form action to save data
				switch(enuFormAction)
				{
					case EnumAction.Add:
						int intInvoiceID;
						if(dtbDetail.DataSet != null)
						{
							intInvoiceID = boMaster.AddAndReturn(voMaster, dtbDetail.DataSet);							
						}
						else
						{
							DataSet dtsDC = new DataSet();
							dtsDC.Tables.Add(dtbDetail);
							intInvoiceID = boMaster.AddAndReturn(voMaster, dtsDC);							
						}

						voMaster.InvoiceMasterID = intInvoiceID;
						break;

					case EnumAction.Edit:
					
						if(dtbDetail.DataSet != null)
						{
                            boMaster.Update(voMaster, dtbDetail.DataSet, _removedId);
						}
						else
						{
							DataSet dtsDC = new DataSet();
							dtsDC.Tables.Add(dtbDetail);
                            boMaster.Update(voMaster, dtsDC, _removedId);
						}						
						break;
				}

			    _removedId.Clear();
				//Reload detail grid				
				LoadDetailGrid(voMaster.InvoiceMasterID);				
				enuFormAction = EnumAction.Default;			

				//Update edit user
				Security.UpdateUserNameModifyTransaction(this, PO_InvoiceMasterTable.INVOICEMASTERID_FLD, voMaster.InvoiceMasterID);
				
				//Lock controls
				LockControl(true);
				
				//Show sucessful message
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
				
			}
			catch (PCSException ex)
			{
				blnDataIsValid = false;
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_DUPLICATE_TRANSNO, MessageBoxIcon.Exclamation);
					txtInvoiceNo.Focus();
				}
				else
				{				
					PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
					try
					{
						Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			catch (Exception ex)
			{
				blnDataIsValid = false;
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				if(Security.NoRightToEditTransaction(this, PO_InvoiceMasterTable.INVOICEMASTERID_FLD, voMaster.InvoiceMasterID))
				{
					return;
				}
				//return if user has not select DC Option
				if(voMaster.InvoiceMasterID == 0 ) return;

				enuFormAction = EnumAction.Edit;
				LockControl(false);
				dtmPostDate.Focus();
			}			
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";

			try
			{
				//Check right
				if(Security.NoRightToDeleteTransaction(this, PO_InvoiceMasterTable.INVOICEMASTERID_FLD, voMaster.InvoiceMasterID))
				{
					return;
				}

				if(voMaster.InvoiceMasterID == 0)
				{ 
					return;
				}

				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if(dtbDetail.DataSet != null)
					{
						boMaster.Delete(voMaster, dtbDetail.DataSet);
					}
					else
					{
						DataSet dtsTemp = new DataSet();
						dtsTemp.Tables.Add(dtbDetail);
						boMaster.Delete(voMaster, dtsTemp);
					}

					ResetControlValue(EnumAction.Delete);					
					LockControl(true);
				}
			}
			catch (PCSException ex)
			{	
				//HACK: added by Tuan TQ. Fix error no. 3823
				try
				{
					LoadDetailGrid(voMaster.InvoiceMasterID);
				}
				catch{}
				//End hack
                if (ex.mCode == ErrorCode.CASCADE_DELETE_PREVENT)
                {
                    PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                }
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			catch (Exception ex)
			{
				//HACK: added by Tuan TQ. Fix error no. 3823
				try
				{
					LoadDetailGrid(voMaster.InvoiceMasterID);
				}
				catch{}
				//End hack

				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				
			}
		}


		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		
		private void txtCurrency_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{			
			const string METHOD_NAME = THIS + ".txtCurrency_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode
				if(enuFormAction == EnumAction.Default) return;
				if(txtCurrency.Text.Length == 0)
				{
					txtCurrency.Tag = ZERO_STRING;
					if(!dtmPostDate.ValueIsDbNull)
					{
						FillExchangeRate(0, DateTime.Parse(dtmPostDate.Value.ToString()));
					}
					else
					{
						FillExchangeRate(0, DateTime.Now);
					}
					return;
				}
				else if(!txtCurrency.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectCurrency(METHOD_NAME, false);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtCurrency_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCurrency_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnCurrency.Enabled))
				{
					SelectCurrency(METHOD_NAME, true);
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtCarrier_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCarrier_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode
				if(enuFormAction == EnumAction.Default) return;
				if(txtCarrier.Text.Length == 0)
				{
					txtCarrier.Tag = ZERO_STRING;
					return;
				}
				else if(!txtCarrier.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectCarrier(METHOD_NAME, false);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtCarrier_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCarrier_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnCarrier.Enabled))
				{
					SelectCarrier(METHOD_NAME, true);
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtPayTerms_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPayTerms_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode
				if(enuFormAction == EnumAction.Default) return;
				if(txtPayTerms.Text.Length == 0)
				{
					return;
				}
				else if(!txtPayTerms.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectPaymentTerm(METHOD_NAME, false);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtPayTerms_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPayTerms_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnPayTerms.Enabled))
				{
					SelectPaymentTerm(METHOD_NAME, true);
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtDeliveryTerms_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDeliveryTerms_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode
				if(enuFormAction == EnumAction.Default) return;
				if(txtDeliveryTerms.Text.Length == 0)
				{
					return;
				}
				else if(!txtDeliveryTerms.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectDeliveryTerm(METHOD_NAME, false);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtDeliveryTerms_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDeliveryTerms_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnDeliveryTerms.Enabled))
				{
					SelectDeliveryTerm(METHOD_NAME, true);
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}		

		private void txtVendor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_Validating()";
			try
			{
				//Exit immediately if empty or in default mode
				if(enuFormAction == EnumAction.Default) return;

				if(txtVendor.Text.Length == 0)
				{
					txtVendor.Tag = ZERO_STRING;
					txtVendorName.Text = string.Empty;
					return;
				}
				else if(!txtVendor.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectVendorCode(METHOD_NAME, false);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtVendor_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnVendor.Enabled))
				{
					SelectVendorCode(METHOD_NAME, true);
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtVendorName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendorName_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode
				if(enuFormAction == EnumAction.Default) return;
				if(txtVendorName.Text.Length == 0)
				{
					txtVendor.Tag = ZERO_STRING;
					txtVendor.Text = string.Empty;
					return;
				}
				else if(!txtVendorName.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectVendorName(METHOD_NAME, false);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtVendorName_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendorName_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnVendorName.Enabled))
				{
					SelectVendorName(METHOD_NAME, true);
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnInvoiceNo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnInvoiceNo_Click()";
			try
			{
				SelectInvoice(METHOD_NAME, true);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		
		private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtInvoiceNo_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnInvoiceNo.Enabled))
				{
					SelectInvoice(METHOD_NAME, true);
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtInvoiceNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtInvoiceNo_Validating()";
			try
			{				
				//Exit immediately if empty or in default mode
				if(enuFormAction != EnumAction.Default) return;
				if(txtInvoiceNo.Text.Length == 0)
				{
					ResetControlValue(EnumAction.Default);
					return;
				}
				else if(!txtInvoiceNo.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectInvoice(METHOD_NAME, false);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
        
		private void dgrdData_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
            try
			{
				if(enuFormAction == EnumAction.Default)
				{
				    return;
				}
				
				switch (e.KeyCode)
				{
					case Keys.F4:						
						ProcessInputDataInGrid(dgrdData.Columns[dgrdData.Col].DataField, dgrdData.Columns[dgrdData.Col].Value.ToString(), true);
						break;
					case Keys.Delete:
						if(dgrdData.SelectedRows.Count <= 0)
						{
							return;
						}

                        foreach (int rowIndex in dgrdData.SelectedRows)
                        {
                            if (dgrdData[rowIndex, PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD] != null
                                && dgrdData[rowIndex, PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD] != DBNull.Value
                                && dgrdData[rowIndex, PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD].ToString() != string.Empty)
                            {
                                decimal receivedQty = 0;
                                if (decimal.TryParse(dgrdData[rowIndex, PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD].ToString(), out receivedQty) && receivedQty > 0)
                                {
                                    // not allow to delete the line already received
                                    PCSMessageBox.Show("There is a line already recieved. Can not delete!");
                                    return;
                                }
                            }

                            if (enuFormAction == EnumAction.Edit)
                            {
                                int detailId = Convert.ToInt32(dgrdData[rowIndex, PO_InvoiceDetailTable.INVOICEDETAILID_FLD]);
                                if (!_removedId.Contains(detailId))
                                {
                                    _removedId.Add(detailId);
                                }
                            }
                        }

						dgrdData.DeleteMultiRows();

				        int intRowCount = dgrdData.RowCount;
						//reset invoice line value
						for (int i = 0; i < dgrdData.RowCount; i++)
						{
							dgrdData[i, PO_InvoiceDetailTable.INVOICELINE_FLD] = i + 1;
						}
							
						//Update total values
						CalculateTotalBeforeVATAmount(intRowCount);
						CalculateTotalCIFAmount(intRowCount);
						CalculateTotalCIPAmount(intRowCount);
						CalculateTotalInlandAmount(intRowCount);
						CalculateTotalImportAmount(intRowCount);
						CalculateTotalVATAmount(intRowCount);
						break;
				}
			}
			catch(PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch(Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void dgrdData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";

			try
			{				
				//if(e.Column.DataColumn.Text.Trim().Length == 0) return;
				e.Cancel = !ProcessInputDataInGrid(e.Column.DataColumn.DataField, e.Column.DataColumn.Value.ToString().Trim(), false);				
			}
			catch(PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch(Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}		

		private void dgrdData_ButtonClick(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";

			try
			{					
				ProcessInputDataInGrid(e.Column.DataColumn.DataField, e.Column.DataColumn.Value.ToString().Trim(), true);				
			}
			catch(PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch(Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		
		private void dgrdData_BeforeColEdit(object sender, BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit()";

			try
			{				
				intCurrentRow = dgrdData.Row;
			}
			catch(Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		
		private void dgrdData_AfterDelete(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterDelete()";

			try
			{	
				//Get actual number of rows 
				int intRowCount = GetActualRowCount(dtbDetail);
				//Update total values
				CalculateTotalBeforeVATAmount(intRowCount);
				CalculateTotalCIFAmount(intRowCount);
				CalculateTotalCIPAmount(intRowCount);
				CalculateTotalInlandAmount(intRowCount);
				CalculateTotalImportAmount(intRowCount);
				CalculateTotalVATAmount(intRowCount);				
			}
			catch(Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		
		private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";

			try
			{
				CalculateDataInGrid(e.Column.DataColumn.DataField);
			}
			catch(Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		
		private void dgrdData_MouseDown(object sender, MouseEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_MouseDown()";
			try
			{
				//Check if in Default (Idle) mode
				if(enuFormAction == EnumAction.Default)
				{
					return;
				}
				
				//Check if current row is empty
				if(dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].Value.Equals(DBNull.Value)
				|| dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].Text == string.Empty)
				{
					return;
				}

				//Force update data in the grid
				//dgrdData.UpdateData();

				if(dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[PO_InvoiceDetailTable.INLAND_FLD]))
				{
					if(e.Button == MouseButtons.Right)
					{
						PCSInputBoxResult  inputBoxResult = PCSInputBox.Show(lblInputTotalInland.Text, lblInputTotalInland.Text);
						if(inputBoxResult.ReturnCode == DialogResult.OK)
						{
							decimal decTotalInlandAmount = decimal.Parse(inputBoxResult.Value.ToString());
							decimal decTotalCIFAMount = decimal.One;

							if(!numTotalCIFAmount.ValueIsDbNull && numTotalCIFAmount.Text != string.Empty)
							{
								decTotalCIFAMount = Decimal.Parse(numTotalCIFAmount.Value.ToString());

								//except zero value of total CIF amount
								if(decTotalCIFAMount == Decimal.Zero)
								{
									decTotalCIFAMount = Decimal.One;
								}
							}

							//Get actual number of rows 
							int intRowCount = GetActualRowCount(dtbDetail);
							for(int i = 0; i < intRowCount; i++)
							{								
								decimal decCIFAMount = decimal.Zero;
								
								//Inorge blank row
								if(dgrdData[i, PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].Equals(DBNull.Value)
								|| dgrdData[i, PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].ToString().Length == 0)
									continue;

								if(!dgrdData[i, PO_InvoiceDetailTable.CIFAMOUNT_FLD].Equals(DBNull.Value)
									&& !dgrdData[i, PO_InvoiceDetailTable.CIFAMOUNT_FLD].ToString().Equals(string.Empty))
								{
									decCIFAMount = decimal.Parse(dgrdData[i, PO_InvoiceDetailTable.CIFAMOUNT_FLD].ToString());
								}								

								dgrdData[i, PO_InvoiceDetailTable.INLAND_FLD] = (decCIFAMount / decTotalCIFAMount) * decTotalInlandAmount;

								//Update calculate value
								intCurrentRow = i;
								CalculateDataInGrid(PO_InvoiceDetailTable.INLAND_FLD);
							}

							//Force update data in the grid
							//dgrdData.UpdateData();
						}
					}
				}
			}
			catch(Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}		

		#endregion Event Processing				
        
		#region Thachnn: Report Section: add print configuration and add Customs List Report
		/// <summary>
		/// Make btnPrintConfiguration always enable/disable like the btnPrint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_EnabledChanged(object sender, System.EventArgs e)
		{
			btnPrintConfiguration.Enabled = ((Control)sender).Enabled;
		}

		#region Thachnn : Customs List Report
				
		#region CUSTOMS LIST REPORT variables	
						
		const string REPORT_LAYOUT_FILE = "CustomsList.xml";
		const string REPORT_NAME = "CustomsList";

		const string REPORTFLD_TITLE			= "fldTitle";
		const string REPORTFLD_COMPANY			= "fldCompany";
		const string REPORTFLD_ADDRESS			= "fldAddress";
		const string REPORTFLD_TEL				= "fldTel";
		const string REPORTFLD_FAX				= "fldFax";

		const string REPORTFLD_HOA_DON_SO					= "fldParameterHoaDonSo";
		const string REPORTFLD_TY_GIA					= "fldParameterCurrencyCode";		
        	
		#endregion

		/// <summary>
		/// <author>thachnn</author>
		/// <date>08/06/2006</date>
		/// Print the Customs List Report
		/// Using the "CustomsList.xml" layout
		/// </summary>		
		private void ShowCustomsList(object sender, System.EventArgs e)
		{			
			const string METHOD_NAME = THIS + "ShowCustomsList() event handler";		
			Cursor = Cursors.WaitCursor;

			try	// function starting
			{						
				//return if no record was selected
				if(voMaster.InvoiceMasterID <= 0)
				{
					Cursor = Cursors.Default;
					return;
				}

				string strInvoiceNo = txtInvoiceNo.Text;			// YY
				string strCurrencyCode = txtCurrency.Text;	// XX			

				int nCCNID = int.MinValue;
				int nInvoiceMasterID = int.MinValue;
				int nMonth = DateTime.MinValue.Month; //minimum month
				int nYear = DateTime.MinValue.Year; // Minimum year			

				#region PREPARE			

				string mstrReportDefFolder = Application.StartupPath + "\\" +Constants.REPORT_DEFINITION_STORE_LOCATION;
				DataTable dtbResult;
				DataTable dtbCheckVAT;
				DataTable dtbCheckImportTAX;
				
				PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();			
		
				// check report layout file is exist or not
				if (!System.IO.File.Exists(mstrReportDefFolder + @"\" + REPORT_LAYOUT_FILE))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
					Cursor = Cursors.Default;
					return;
				}
				#endregion

				nCCNID = int.Parse(cboCCN.SelectedValue.ToString());
				nInvoiceMasterID = voMaster.InvoiceMasterID;	

				#region BUILDING THE TABLE (getting from database by BO)
				C1PrintPreviewDialogBO boDataReport = new C1PrintPreviewDialogBO();
				DataSet dstResult = boDataReport.GetCustomsListReportData(nCCNID,  nInvoiceMasterID);
				dtbResult = dstResult.Tables[0];
				dtbCheckVAT = dstResult.Tables[1];
				dtbCheckImportTAX = dstResult.Tables[2];
				#endregion
		
				#region Validate data
		
				// user must select CCN first
				if (cboCCN.SelectedValue == null || cboCCN.SelectedValue == DBNull.Value)
				{
					string[] arrParams = {lblCCN.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error,arrParams);
					cboCCN.Focus();
					Cursor = Cursors.Default;
					return;
				}
				// Check if user does not select Invoice
				if(txtInvoiceNo.Text.Trim() == string.Empty)
				{
					string[] arrParams = {lblInvoiceNo.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error,arrParams);
					txtInvoiceNo.Focus();
					Cursor = Cursors.Default;
					return;
				}

				foreach(DataRow drow in dtbCheckVAT.Rows)
				{
					string[] arrParams = {lblVATTaxRate.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_PRODUCT_WITH_MULTIPLE_TAXRATE_IN_INVOICE, MessageBoxIcon.Error,arrParams);
//					txtInvoiceNo.Focus();
					Cursor = Cursors.Default;
					return;					
				}
				foreach(DataRow drow in dtbCheckImportTAX.Rows)
				{
					string[] arrParams = {lblImportTaxRate.Text};
						PCSMessageBox.Show(ErrorCode.MESSAGE_PRODUCT_WITH_MULTIPLE_TAXRATE_IN_INVOICE, MessageBoxIcon.Error,arrParams);
//					txtInvoiceNo.Focus();					
					Cursor = Cursors.Default;
					return;					
				}


				#endregion			 Validate data	

			
				#region RENDER REPORT
		
				ReportBuilder objRB;	
				objRB = new ReportBuilder();
				objRB.ReportName = REPORT_NAME;
				objRB.SourceDataTable = dtbResult;

				#region INIT REPORT BUIDER OBJECT
				try
				{
					objRB.ReportDefinitionFolder = mstrReportDefFolder;
					objRB.ReportLayoutFile = REPORT_LAYOUT_FILE;					
					if(objRB.AnalyseLayoutFile() == false)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
						return;
					}
					//objRB.UseLayoutFile = objRB.AnalyseLayoutFile();	// use layout file if any , auto drawing if not found layout file
					objRB.UseLayoutFile = true;	// always use layout file
				}
				catch
				{
					objRB.UseLayoutFile = false;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND,MessageBoxIcon.Error);
				}
				#endregion				

				objRB.MakeDataTableForRender();

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();

				objRB.ReportViewer = printPreview.ReportViewer;
				objRB.RenderReport();

				#region MODIFY THE REPORT LAYOUT				
				objRB.DrawPredefinedField(REPORTFLD_TY_GIA, strCurrencyCode );
				objRB.DrawPredefinedField(REPORTFLD_HOA_DON_SO, strInvoiceNo );				

				#region COMPANY INFO // header information get from system params
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_COMPANY,SystemProperty.SytemParams.Get(SystemParam.COMPANY_NAME));
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_ADDRESS,SystemProperty.SytemParams.Get(SystemParam.ADDRESS));					
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_TEL,SystemProperty.SytemParams.Get(SystemParam.TEL));					
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_FAX,SystemProperty.SytemParams.Get(SystemParam.FAX));					
				}
				catch{}
				#endregion
			
				#endregion						
				objRB.RefreshReport();
				
				/// force the copies number
                printPreview.FormTitle = objRB.GetFieldByName(REPORTFLD_TITLE).Text ;
				Cursor = Cursors.Default;
				printPreview.Show();
				#endregion

			}
			catch(Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			finally
			{
				Cursor = Cursors.Default;				
			}			
		}
	
		#endregion Thachnn: Customs List report

		private void dgrdData_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			if(dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INVOICEDETAILID_FLD] != DBNull.Value)
			{
				//Check if Invoice has been receipt
				POInvoiceBO boPOInvoice = new POInvoiceBO();
				if (boPOInvoice.CheckIfInvoiceHasBeenReceipt(int.Parse(dgrdData[dgrdData.Row, PO_InvoiceDetailTable.INVOICEDETAILID_FLD].ToString())))
				{
					e.Cancel = true;
				}
			}
		}

		#endregion Thachnn: Report Section: add print configuration and add Customs List Report
	}
}