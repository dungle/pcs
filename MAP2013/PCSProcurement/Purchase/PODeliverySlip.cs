using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSComProcurement.Purchase.BO;
using PCSComProcurement.Purchase.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSUtils.Framework.ReportFrame;

namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for PODeliverySlip.
	/// </summary>
	public class PODeliverySlip : System.Windows.Forms.Form
	{
		#region controls

		private Panel pnlControls;
		private Button btnClose;
		private Button btnHelp;
		private Button btnView;
		private C1DateEdit dtmToDateTime;
		private C1DateEdit dtmFromDateTime;
		private Label lblToDateTime;
		private Label lblFromDateTime;
		private RadioButton radPeriod;
		private RadioButton radAll;
		private ToolBarButton c1pBtnClose;

		#endregion

		private const string THIS = "PCSProcurement.Purchase.PODeliverySlip";
		private C1.C1Report.C1Report rptDeliverySlip;
		private C1.C1Report.C1Report rptOverDeliverySlip;
		private C1.C1Report.C1Report rptPrintable;
		private C1.C1Report.C1Report rptOverPrintable;
		private C1.C1Report.C1Report rptPrintableReceive;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private PO_PurchaseOrderMasterVO voPOMaster;
		private string strReportFilePath = null;
		private string strPrintFilePath = null;
		private const string RECEIVE_SLIP = "ReceiveSlip";
		private const string mstrReportFileName = "DeliverySlip.xml";
		private const string mstrViewFileName = "DeliverySlipView.xml";
		private const string SCHEDULE_HOUR_FLD = "ScheduleHour";
		private const string SHORT_TIME_FORMAT = "HH:mm";
		private const string COMPANY_FLD = "fldCompany";
		private const string ADDRESS_FLD = "fldAddress";
		private const string TEL_FLD = "fldTel";
		private const string FAX_FLD = "fldFax";
		private const string SLIP_NO_FLD = "fldSlipNo";
		private const string SEPERATOR = "-";
		private const string DATE_FORMAT = "yyyyMMddHH";
		private const string PONO_FLD = "fldPoNo";
		private const string ISSUE_DATE_FLD = "fldIssueDate";
		private const string ISSUEHOUR_FLD = "fldIssueHour";
		private const string CUST_CODE_FLD = "fldCustCode";
		private const string CUST_NAME_FLD = "fldCustName";
		private const string CCN_CODE_FLD = "fldCCNCode";
		private const string CCN_NAME_FLD = "fldCCNName";
		private const string SUB_REPORT_FLD = "subReceiveSlip";
		private const string TITLE_FLD = "fldTitle";
		private const string COPIED_FLD = "fldCopied";
		private const string TITLE_VN_FLD = "fldTitleVN";
		private const int MAX_LINE_PER_SLIP = 10;
        private C1PrintPreviewControl ppvSlip;
		private const string TABLE_NAME = "Delivery Slip";
		
		public PODeliverySlip(object pobjPOMaster)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			voPOMaster = (PO_PurchaseOrderMasterVO)pobjPOMaster;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PODeliverySlip));
            this.c1pBtnClose = new System.Windows.Forms.ToolBarButton();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.dtmToDateTime = new C1.Win.C1Input.C1DateEdit();
            this.dtmFromDateTime = new C1.Win.C1Input.C1DateEdit();
            this.lblToDateTime = new System.Windows.Forms.Label();
            this.lblFromDateTime = new System.Windows.Forms.Label();
            this.radPeriod = new System.Windows.Forms.RadioButton();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.rptDeliverySlip = new C1.C1Report.C1Report();
            this.rptPrintable = new C1.C1Report.C1Report();
            this.rptPrintableReceive = new C1.C1Report.C1Report();
            this.ppvSlip = new C1.Win.C1Preview.C1PrintPreviewControl();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptDeliverySlip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptPrintable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptPrintableReceive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppvSlip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppvSlip.PreviewPane)).BeginInit();
            this.ppvSlip.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1pBtnClose
            // 
            resources.ApplyResources(this.c1pBtnClose, "c1pBtnClose");
            this.c1pBtnClose.Name = "c1pBtnClose";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnClose);
            this.pnlControls.Controls.Add(this.btnHelp);
            this.pnlControls.Controls.Add(this.btnView);
            this.pnlControls.Controls.Add(this.dtmToDateTime);
            this.pnlControls.Controls.Add(this.dtmFromDateTime);
            this.pnlControls.Controls.Add(this.lblToDateTime);
            this.pnlControls.Controls.Add(this.lblFromDateTime);
            this.pnlControls.Controls.Add(this.radPeriod);
            this.pnlControls.Controls.Add(this.radAll);
            resources.ApplyResources(this.pnlControls, "pnlControls");
            this.pnlControls.Name = "pnlControls";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnView
            // 
            resources.ApplyResources(this.btnView, "btnView");
            this.btnView.Name = "btnView";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // dtmToDateTime
            // 
            // 
            // 
            // 
            this.dtmToDateTime.Calendar.DayNameLength = 1;
            this.dtmToDateTime.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmToDateTime.Calendar.ImeMode")));
            this.dtmToDateTime.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmToDateTime.Calendar.RightToLeft")));
            this.dtmToDateTime.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDateTime.DisplayFormat.Inherit")));
            this.dtmToDateTime.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDateTime.EditFormat.Inherit")));
            resources.ApplyResources(this.dtmToDateTime, "dtmToDateTime");
            this.dtmToDateTime.Name = "dtmToDateTime";
            this.dtmToDateTime.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmToDateTime.ParseInfo.Inherit")));
            this.dtmToDateTime.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmToDateTime.PreValidation.Inherit")));
            this.dtmToDateTime.Tag = null;
            // 
            // dtmFromDateTime
            // 
            // 
            // 
            // 
            this.dtmFromDateTime.Calendar.DayNameLength = 1;
            this.dtmFromDateTime.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmFromDateTime.Calendar.ImeMode")));
            this.dtmFromDateTime.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmFromDateTime.Calendar.RightToLeft")));
            this.dtmFromDateTime.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDateTime.DisplayFormat.Inherit")));
            this.dtmFromDateTime.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDateTime.EditFormat.Inherit")));
            resources.ApplyResources(this.dtmFromDateTime, "dtmFromDateTime");
            this.dtmFromDateTime.Name = "dtmFromDateTime";
            this.dtmFromDateTime.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmFromDateTime.ParseInfo.Inherit")));
            this.dtmFromDateTime.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmFromDateTime.PreValidation.Inherit")));
            this.dtmFromDateTime.Tag = null;
            // 
            // lblToDateTime
            // 
            this.lblToDateTime.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblToDateTime, "lblToDateTime");
            this.lblToDateTime.Name = "lblToDateTime";
            // 
            // lblFromDateTime
            // 
            this.lblFromDateTime.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblFromDateTime, "lblFromDateTime");
            this.lblFromDateTime.Name = "lblFromDateTime";
            // 
            // radPeriod
            // 
            resources.ApplyResources(this.radPeriod, "radPeriod");
            this.radPeriod.Name = "radPeriod";
            this.radPeriod.CheckedChanged += new System.EventHandler(this.radPeriod_CheckedChanged);
            // 
            // radAll
            // 
            this.radAll.Checked = true;
            resources.ApplyResources(this.radAll, "radAll");
            this.radAll.Name = "radAll";
            this.radAll.TabStop = true;
            this.radAll.CheckedChanged += new System.EventHandler(this.radAll_CheckedChanged);
            // 
            // rptDeliverySlip
            // 
            this.rptDeliverySlip.ReportDefinition = resources.GetString("rptDeliverySlip.ReportDefinition");
            this.rptDeliverySlip.ReportName = "Deliver Slip";
            // 
            // rptPrintable
            // 
            this.rptPrintable.ReportDefinition = resources.GetString("rptPrintable.ReportDefinition");
            this.rptPrintable.ReportName = "Delivery Slip";
            // 
            // rptPrintableReceive
            // 
            this.rptPrintableReceive.ReportDefinition = resources.GetString("rptPrintableReceive.ReportDefinition");
            this.rptPrintableReceive.ReportName = "Receive Slip";
            // 
            // ppvSlip
            // 
            resources.ApplyResources(this.ppvSlip, "ppvSlip");
            this.ppvSlip.Name = "ppvSlip";
            // 
            // ppvSlip.OutlineView
            // 
            resources.ApplyResources(this.ppvSlip.PreviewOutlineView, "ppvSlip.OutlineView");
            this.ppvSlip.PreviewOutlineView.Name = "OutlineView";
            // 
            // ppvSlip.PreviewPane
            // 
            this.ppvSlip.PreviewPane.IntegrateExternalTools = true;
            resources.ApplyResources(this.ppvSlip.PreviewPane, "ppvSlip.PreviewPane");
            // 
            // ppvSlip.PreviewTextSearchPanel
            // 
            resources.ApplyResources(this.ppvSlip.PreviewTextSearchPanel, "ppvSlip.PreviewTextSearchPanel");
            this.ppvSlip.PreviewTextSearchPanel.MinimumSize = new System.Drawing.Size(200, 240);
            this.ppvSlip.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel";
            // 
            // ppvSlip.ThumbnailView
            // 
            resources.ApplyResources(this.ppvSlip.PreviewThumbnailView, "ppvSlip.ThumbnailView");
            this.ppvSlip.PreviewThumbnailView.Name = "ThumbnailView";
            this.ppvSlip.PreviewThumbnailView.UseImageAsThumbnail = false;
            // 
            // PODeliverySlip
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.ppvSlip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PODeliverySlip";
            this.Load += new System.EventHandler(this.PODeliverySlipReport_Load);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptDeliverySlip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptPrintable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptPrintableReceive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppvSlip.PreviewPane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppvSlip)).EndInit();
            this.ppvSlip.ResumeLayout(false);
            this.ppvSlip.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void PODeliverySlipReport_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".PODeliverySlipReport_Load()";
			try
			{
				#region Security

				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					return;
				}

				#endregion

				strReportFilePath = Application.StartupPath + "\\" +Constants.REPORT_DEFINITION_STORE_LOCATION + "\\" + mstrViewFileName;
				strPrintFilePath = Application.StartupPath + "\\" +Constants.REPORT_DEFINITION_STORE_LOCATION + "\\" + mstrReportFileName;
				// format date time
				dtmFromDateTime.FormatType = FormatTypeEnum.CustomFormat;
				dtmFromDateTime.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				dtmToDateTime.FormatType = FormatTypeEnum.CustomFormat;
				dtmToDateTime.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				ppvSlip.PreviewNavigationPanel.Visible = false;
				ppvSlip.PreviewPane.ZoomMode = ZoomModeEnum.PageWidth;

				EnableButtons();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
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
		}

		private void radAll_CheckedChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".radAll_CheckedChanged()";
			try
			{
				EnableButtons();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
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
		}

		private void radPeriod_CheckedChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".radPeriod_CheckedChanged()";
			try
			{
				EnableButtons();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
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
		}

		private void btnView_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnView_Click()";
			try
			{
				Cursor = Cursors.WaitCursor;
				DateTime dtmToDate = DateTime.MaxValue;
				DateTime dtmFromDate = DateTime.MinValue;
				if (radPeriod.Checked)
				{
					if (dtmFromDateTime.Value == DBNull.Value)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
						dtmFromDateTime.Focus();
						dtmFromDateTime.SelectAll();
						return;
					}
					if (dtmToDateTime.Value == DBNull.Value)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
						dtmToDateTime.Focus();
						dtmToDateTime.SelectAll();
						return;
					}
					// to date must greater than from date
					dtmToDate = (DateTime)dtmToDateTime.Value;
					dtmFromDate = (DateTime)dtmFromDateTime.Value;
				}
				if (dtmFromDate > dtmToDate)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Error);
					dtmFromDateTime.Focus();
					return;
				}
				// check if report file is exist or not
				if (!File.Exists(strReportFilePath))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
					return;
				}
				// check if report file is exist or not
				if (!File.Exists(strPrintFilePath))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
					return;
				}
				// only care about date and hour, 
				// ignore minutes and the other information
				if (dtmToDate != DateTime.MaxValue)
					dtmToDate = new DateTime(dtmToDate.Year, dtmToDate.Month, dtmToDate.Day, dtmToDate.Hour, 59, 59);
				if (dtmFromDate != DateTime.MinValue)
					dtmFromDate = new DateTime(dtmFromDate.Year, dtmFromDate.Month, dtmFromDate.Day, dtmFromDate.Hour, 0, 0);
				// get the issue date
				DateTime dtmIssueDate = new UtilsBO().GetDBDate();
				PODeliverySlipBO boDeliverySlip = new PODeliverySlipBO();
				// execute report
				DataTable dtbData = boDeliverySlip.ExecuteReport(voPOMaster.PurchaseOrderMasterID, dtmFromDate, dtmToDate);
				// refine data source
				DataSet dstData = BuildReportData(dtbData);
				RenderReport(dtmIssueDate, dstData);
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
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
				Cursor = Cursors.Default;
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// Rendering the report with all parameter
		/// </summary>
		/// <param name="pdtmIssueDate">The Issue Date</param>
		/// <param name="pdtbReportData">Report Data</param>
		private void RenderReport(DateTime pdtmIssueDate, DataTable pdtbReportData, bool pblnPrintTwoSlipInOnePage)
		{
			// prevent reentrant calls
			if (rptDeliverySlip.IsBusy)
				return;
			// initialize control
			rptDeliverySlip.Clear(); // clear any existing fields
			rptPrintable.Clear();
			string[] strReportInfo = rptDeliverySlip.GetReportInfo(strPrintFilePath);
			string[] strPrintInfo = rptPrintable.GetReportInfo(strPrintFilePath);
			if (strReportInfo != null && strReportInfo.Length > 0)
				rptDeliverySlip.Load(strReportFilePath, strReportInfo[0]);
			if (strPrintInfo != null && strPrintInfo.Length > 0)
				rptPrintable.Load(strPrintFilePath, strPrintInfo[0]);

			int intNumOfRows = pdtbReportData.Rows.Count;
			// set datasource object that provides data to report.
			rptDeliverySlip.DataSource.Recordset = pdtbReportData;
			rptPrintable.DataSource.Recordset = pdtbReportData;
			C1Report rptSubReport = rptDeliverySlip.Fields[SUB_REPORT_FLD].Subreport;
			C1Report rptPrintSubReport = rptPrintable.Fields[SUB_REPORT_FLD].Subreport;
			if (intNumOfRows > 0 && !pblnPrintTwoSlipInOnePage)
			{
				rptDeliverySlip.Fields[SUB_REPORT_FLD].Text = string.Empty;
				rptPrintable.Fields[SUB_REPORT_FLD].Text = string.Empty;
				rptDeliverySlip.Groups[SCHEDULE_HOUR_FLD].SectionFooter.Visible = false;
				rptPrintable.Groups[SCHEDULE_HOUR_FLD].SectionFooter.Visible = false;

				#region PUSH PARAMETER VALUE

				// header information get from system params
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[PONO_FLD].Text = voPOMaster.Code;
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[PONO_FLD].Text = voPOMaster.Code;
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
				}
				catch{}

				#endregion

				// render the report into the PrintPreviewControl
				PreviewOnlyNoPrintDialog dlgDeliveryPreview = new PreviewOnlyNoPrintDialog();
				dlgDeliveryPreview.ReportViewer.Document = rptDeliverySlip.Document;
				dlgDeliveryPreview.ReportViewer.PreviewNavigationPanel.Visible = false;
				dlgDeliveryPreview.ReportViewer.PreviewPane.ZoomMode = ZoomModeEnum.PageWidth;
				try
				{
					dlgDeliveryPreview.FormTitle = rptDeliverySlip.Fields[TITLE_FLD].Text;
				}
				catch{}
				dlgDeliveryPreview.Show();

				// make a copy of delivery slip to receive slip and show in different printpreview
				C1Report rptReceiveSlip = new C1Report();
				rptReceiveSlip.CopyFrom(rptDeliverySlip);
				rptPrintableReceive.CopyFrom(rptPrintable);
				try
				{
					rptReceiveSlip.Fields[TITLE_FLD].Text = rptSubReport.Fields[TITLE_FLD].Text;
					rptPrintableReceive.Fields[TITLE_FLD].Text = rptPrintSubReport.Fields[TITLE_FLD].Text;
				}
				catch{}
				try
				{
					rptReceiveSlip.Fields["fldCopied"].Text = rptSubReport.Fields["fldCopied"].Text;
					rptPrintableReceive.Fields["fldCopied"].Text = rptPrintSubReport.Fields["fldCopied"].Text;
				}
				catch{}
				try
				{
					rptReceiveSlip.Fields["fldTitleVN"].Text = rptSubReport.Fields["fldTitleVN"].Text;
					rptPrintableReceive.Fields["fldTitleVN"].Text = rptPrintSubReport.Fields["fldTitleVN"].Text;
				}
				catch{}
				PreviewOnlyNoPrintDialog dlgReceivePreview = new PreviewOnlyNoPrintDialog();
				dlgReceivePreview.ReportViewer.Document = rptReceiveSlip.Document;
				dlgReceivePreview.ReportViewer.PreviewNavigationPanel.Visible = false;
				dlgReceivePreview.ReportViewer.PreviewPane.ZoomMode = ZoomModeEnum.PageWidth;
				dlgReceivePreview.ReportViewer.Name = RECEIVE_SLIP;
				try
				{
					dlgReceivePreview.FormTitle = rptReceiveSlip.Fields[TITLE_FLD].Text;
				}
				catch{}
				dlgReceivePreview.Show();
			}
			else if (intNumOfRows > 0 && pblnPrintTwoSlipInOnePage)
			{
				rptSubReport.DataSource.Recordset = pdtbReportData;
				rptPrintSubReport.DataSource.Recordset = pdtbReportData;

				#region PUSH PARAMETER VALUE

				// header information get from system params
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[PONO_FLD].Text = voPOMaster.Code;
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[PONO_FLD].Text = voPOMaster.Code;
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[PONO_FLD].Text = voPOMaster.Code;
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[PONO_FLD].Text = voPOMaster.Code;
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
				}
				catch{}

				#endregion

				// render the report into the PrintPreviewControl
				PreviewOnlyNoPrintDialog dlgPreview = new PreviewOnlyNoPrintDialog();
				dlgPreview.ReportViewer.Document = rptDeliverySlip.Document;
				dlgPreview.ReportViewer.PreviewNavigationPanel.Visible = false;
				dlgPreview.ReportViewer.PreviewPane.ZoomMode = ZoomModeEnum.PageWidth;
				try
				{
					dlgPreview.FormTitle = rptDeliverySlip.Fields[TITLE_FLD].Text;
				}
				catch{}
				dlgPreview.Show();
			}
			else
			{
				// enable report header section
				rptDeliverySlip.Sections[SectionTypeEnum.Header].Visible = true;
				rptPrintable.Sections[SectionTypeEnum.Header].Visible = true;
				// enable page header section
				//rptDeliverySlip.Sections[SectionTypeEnum.PageHeader].Visible = true;

				#region PUSH PARAMETER VALUE

				// header information get from system params
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[SLIP_NO_FLD].Text = voPOMaster.Code + SEPERATOR + pdtmIssueDate.ToString(DATE_FORMAT);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[SLIP_NO_FLD].Text = voPOMaster.Code + SEPERATOR + pdtmIssueDate.ToString(DATE_FORMAT);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[PONO_FLD].Text = voPOMaster.Code;
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[PONO_FLD].Text = voPOMaster.Code;
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
				}
				catch{}
				UtilsBO boUtils = new UtilsBO();
				MST_PartyVO voParty = (MST_PartyVO)boUtils.GetPartyInfo(voPOMaster.PartyID);
				MST_CCNVO voCCN = (MST_CCNVO)boUtils.GetCCNInfo(voPOMaster.CCNID);
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[CUST_CODE_FLD].Text = voParty.Code;
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[CUST_CODE_FLD].Text = voParty.Code;
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[CUST_NAME_FLD].Text = voParty.Name;
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[CUST_NAME_FLD].Text = voParty.Name;
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[CCN_CODE_FLD].Text = voCCN.Code;
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[CCN_CODE_FLD].Text = voCCN.Code;
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[CCN_NAME_FLD].Text = voCCN.Name;
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[CCN_NAME_FLD].Text = voCCN.Name;
				}
				catch{}

				#endregion

				// render the report into the PrintPreviewControl
				PreviewOnlyNoPrintDialog dlgPreview = new PreviewOnlyNoPrintDialog();
				dlgPreview.ReportViewer.Document = rptDeliverySlip.Document;
				dlgPreview.ReportViewer.PreviewNavigationPanel.Visible = false;
				dlgPreview.ReportViewer.PreviewPane.ZoomMode = ZoomModeEnum.PageWidth;
				try
				{
					dlgPreview.FormTitle = rptDeliverySlip.Fields[TITLE_FLD].Text;
				}
				catch{}
				dlgPreview.Show();
			}
		}
		/// <summary>
		/// Rendering the report with all parameter
		/// </summary>
		/// <param name="pdtmIssueDate">The Issue Date</param>
		/// <param name="pdtbReportData">Report Data</param>
		private void RenderReport(DateTime pdtmIssueDate, DataSet pdstReportData)
		{
			// prevent reentrant calls
			if (rptDeliverySlip.IsBusy)
				return;
			// initialize control
			rptDeliverySlip.Clear(); // clear any existing fields
			rptPrintable.Clear();
			rptOverDeliverySlip = new C1Report();
			rptOverPrintable = new C1Report();
			rptOverDeliverySlip.CopyFrom(rptDeliverySlip);
			rptOverPrintable.CopyFrom(rptPrintable);
			rptOverDeliverySlip.Clear(); // clear any existing fields
			rptOverPrintable.Clear();
			string[] strReportInfo = rptDeliverySlip.GetReportInfo(strPrintFilePath);
			string[] strPrintInfo = rptPrintable.GetReportInfo(strPrintFilePath);
			if (strReportInfo != null && strReportInfo.Length > 0)
			{
				rptDeliverySlip.Load(strReportFilePath, strReportInfo[0]);
				rptOverDeliverySlip.Load(strReportFilePath, strReportInfo[0]);
			}
			if (strPrintInfo != null && strPrintInfo.Length > 0)
			{
				rptPrintable.Load(strPrintFilePath, strPrintInfo[0]);
				rptOverPrintable.Load(strPrintFilePath, strPrintInfo[0]);
			}

			DataTable dtbOriginalData = pdstReportData.Tables[TABLE_NAME];
			DataTable dtbSecondData = new DataTable();
			if (pdstReportData.Tables.Count > 1)
				dtbSecondData = pdstReportData.Tables[TABLE_NAME + MAX_LINE_PER_SLIP.ToString()];

			//int intNumOfRows = dtbOriginalData.Rows.Count;
			// set datasource object that provides data to report.
			rptDeliverySlip.DataSource.Recordset = dtbOriginalData;
			rptPrintable.DataSource.Recordset = dtbOriginalData;
			C1Report rptSubReport = rptDeliverySlip.Fields[SUB_REPORT_FLD].Subreport;
			C1Report rptPrintSubReport = rptPrintable.Fields[SUB_REPORT_FLD].Subreport;

			if (dtbOriginalData.Rows.Count > 0)
			{
				rptSubReport.DataSource.Recordset = dtbOriginalData;
				rptPrintSubReport.DataSource.Recordset = dtbOriginalData;

				#region PUSH PARAMETER VALUE

				// header information get from system params
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[PONO_FLD].Text = voPOMaster.Code;
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[PONO_FLD].Text = voPOMaster.Code;
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[PONO_FLD].Text = voPOMaster.Code;
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[PONO_FLD].Text = voPOMaster.Code;
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
					rptPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
					rptSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
					rptPrintSubReport.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
				}
				catch{}

				#endregion

				// render the report into the PrintPreviewControl
				PreviewOnlyNoPrintDialog dlgPreview = new PreviewOnlyNoPrintDialog();
				dlgPreview.ReportViewer.Document = rptDeliverySlip.Document;
				dlgPreview.ReportViewer.PreviewNavigationPanel.Visible = false;
				dlgPreview.ReportViewer.PreviewPane.ZoomMode = ZoomModeEnum.ActualSize;
				try
				{
					dlgPreview.FormTitle = rptDeliverySlip.Fields[TITLE_FLD].Text;
				}
				catch{}
				dlgPreview.Show();
			}

			// print delivery slip and receive slip seperated
			if (pdstReportData.Tables.Count > 1)
			{
				#region Displays the over data
				if (dtbSecondData != null && dtbSecondData.Rows.Count > 0)
				{
					rptOverDeliverySlip.DataSource.Recordset = dtbSecondData;
					rptOverPrintable.DataSource.Recordset = dtbSecondData;
					rptOverDeliverySlip.Fields[SUB_REPORT_FLD].Text = string.Empty;
					rptOverPrintable.Fields[SUB_REPORT_FLD].Text = string.Empty;
					rptOverDeliverySlip.Groups[SCHEDULE_HOUR_FLD].SectionFooter.Visible = false;
					rptOverPrintable.Groups[SCHEDULE_HOUR_FLD].SectionFooter.Visible = false;

					#region Delivery Slip first

					#region PUSH PARAMETER VALUE

					// header information get from system params
					try
					{
						rptOverDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
						rptOverPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
					}
					catch{}
					try
					{
						rptOverDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
						rptOverPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
					}
					catch{}
					try
					{
						rptOverDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
						rptOverPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
					}
					catch{}
					try
					{
						rptOverDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
						rptOverPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
					}
					catch{}
					try
					{
						rptOverDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[PONO_FLD].Text = voPOMaster.Code;
						rptOverPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[PONO_FLD].Text = voPOMaster.Code;
					}
					catch{}
					try
					{
						rptOverDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
						rptOverPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
					}
					catch{}
					try
					{
						rptOverDeliverySlip.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
						rptOverPrintable.Sections[SectionTypeEnum.GroupHeader2].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
					}
					catch{}

					#endregion

					// render the report into the PrintPreviewControl
					PreviewOnlyNoPrintDialog dlgDeliveryPreview = new PreviewOnlyNoPrintDialog();
					dlgDeliveryPreview.ReportViewer.Document = rptOverDeliverySlip.Document;
					dlgDeliveryPreview.ReportViewer.PreviewNavigationPanel.Visible = false;
					dlgDeliveryPreview.ReportViewer.PreviewPane.ZoomMode = ZoomModeEnum.PageWidth;
					try
					{
						dlgDeliveryPreview.FormTitle = rptOverDeliverySlip.Fields[TITLE_FLD].Text;
					}
					catch{}
					dlgDeliveryPreview.Show();

					#endregion

					#region Receive Slip

					// make a copy of delivery slip to receive slip and show in different printpreview
					C1Report rptReceiveSlip = new C1Report();
					rptReceiveSlip.CopyFrom(rptOverDeliverySlip);
					rptPrintableReceive.CopyFrom(rptOverPrintable);
					try
					{
						rptReceiveSlip.Fields[TITLE_FLD].Text = rptSubReport.Fields[TITLE_FLD].Text;
						rptPrintableReceive.Fields[TITLE_FLD].Text = rptPrintSubReport.Fields[TITLE_FLD].Text;
					}
					catch{}
					try
					{
						rptReceiveSlip.Fields[COPIED_FLD].Text = rptSubReport.Fields[COPIED_FLD].Text;
						rptPrintableReceive.Fields[COPIED_FLD].Text = rptPrintSubReport.Fields[COPIED_FLD].Text;
					}
					catch{}
					try
					{
						rptReceiveSlip.Fields[TITLE_VN_FLD].Text = rptSubReport.Fields[TITLE_VN_FLD].Text;
						rptPrintableReceive.Fields[TITLE_VN_FLD].Text = rptPrintSubReport.Fields[TITLE_VN_FLD].Text;
					}
					catch{}
					PreviewOnlyNoPrintDialog dlgReceivePreview = new PreviewOnlyNoPrintDialog();
					dlgReceivePreview.ReportViewer.Document = rptReceiveSlip.Document;
					dlgReceivePreview.ReportViewer.PreviewNavigationPanel.Visible = false;
					dlgReceivePreview.ReportViewer.PreviewPane.ZoomMode = ZoomModeEnum.PageWidth;
					dlgReceivePreview.ReportViewer.Name = RECEIVE_SLIP;
					try
					{
						dlgReceivePreview.FormTitle = rptReceiveSlip.Fields[TITLE_FLD].Text;
					}
					catch{}
					dlgReceivePreview.Show();

					#endregion
				}
				#endregion
			}
			else if (dtbOriginalData.Rows.Count == 0)
			{
				// enable report header section
				rptDeliverySlip.Sections[SectionTypeEnum.Header].Visible = true;
				rptPrintable.Sections[SectionTypeEnum.Header].Visible = true;
				// enable page header section
				//rptDeliverySlip.Sections[SectionTypeEnum.PageHeader].Visible = true;

				#region PUSH PARAMETER VALUE

				// header information get from system params
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[COMPANY_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[ADDRESS_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[TEL_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[FAX_FLD].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[SLIP_NO_FLD].Text = voPOMaster.Code + SEPERATOR + pdtmIssueDate.ToString(DATE_FORMAT);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[SLIP_NO_FLD].Text = voPOMaster.Code + SEPERATOR + pdtmIssueDate.ToString(DATE_FORMAT);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[PONO_FLD].Text = voPOMaster.Code;
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[PONO_FLD].Text = voPOMaster.Code;
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[ISSUE_DATE_FLD].Text = pdtmIssueDate.ToString(Constants.DATETIME_FORMAT);
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[ISSUEHOUR_FLD].Text = pdtmIssueDate.ToString(SHORT_TIME_FORMAT);
				}
				catch{}
				UtilsBO boUtils = new UtilsBO();
				MST_PartyVO voParty = (MST_PartyVO)boUtils.GetPartyInfo(voPOMaster.PartyID);
				MST_CCNVO voCCN = (MST_CCNVO)boUtils.GetCCNInfo(voPOMaster.CCNID);
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[CUST_CODE_FLD].Text = voParty.Code;
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[CUST_CODE_FLD].Text = voParty.Code;
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[CUST_NAME_FLD].Text = voParty.Name;
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[CUST_NAME_FLD].Text = voParty.Name;
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[CCN_CODE_FLD].Text = voCCN.Code;
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[CCN_CODE_FLD].Text = voCCN.Code;
				}
				catch{}
				try
				{
					rptDeliverySlip.Sections[SectionTypeEnum.Header].Fields[CCN_NAME_FLD].Text = voCCN.Name;
					rptPrintable.Sections[SectionTypeEnum.Header].Fields[CCN_NAME_FLD].Text = voCCN.Name;
				}
				catch{}

				#endregion

				// render the report into the PrintPreviewControl
				PreviewOnlyNoPrintDialog dlgPreview = new PreviewOnlyNoPrintDialog();
				dlgPreview.ReportViewer.Document = rptDeliverySlip.Document;
				dlgPreview.ReportViewer.PreviewNavigationPanel.Visible = false;
				dlgPreview.ReportViewer.PreviewPane.ZoomMode = ZoomModeEnum.PageWidth;
				try
				{
					dlgPreview.FormTitle = rptDeliverySlip.Fields[TITLE_FLD].Text;
				}
				catch{}
				dlgPreview.Show();
			}
		}
		/// <summary>
		/// Enable button based on the option All or Period
		/// </summary>
		private void EnableButtons()
		{
			lblFromDateTime.Enabled = radPeriod.Checked;
			dtmFromDateTime.Enabled = radPeriod.Checked;
			lblToDateTime.Enabled = radPeriod.Checked;
			dtmToDateTime.Enabled = radPeriod.Checked;
		}

		/// <summary>
		/// Build report data match with the report template from source data
		/// </summary>
		/// <param name="pdtbData">Source Data</param>
		/// <returns>True if more than 10 rows</returns>
		private bool BuildReportData(ref DataTable pdtbData)
		{
			bool blnIsOver = false;
			DataTable dtbResult = pdtbData.Clone();
			foreach (DataColumn dcolData in dtbResult.Columns)
				dcolData.AllowDBNull = true;
			ArrayList arrDates = new ArrayList();
			// get all schedule date
			foreach (DataRow drowData in pdtbData.Rows)
			{
				DateTime dtmRowDate = (DateTime)drowData[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD];
				int intRowHour = (int)drowData[SCHEDULE_HOUR_FLD];
				dtmRowDate = dtmRowDate.AddHours(intRowHour);
				if (!arrDates.Contains(dtmRowDate))
					arrDates.Add(dtmRowDate);
			}
			arrDates.Sort();
			arrDates.TrimToSize();
			// foreach schedule date
			foreach (DateTime dtmScheduleDate in arrDates)
			{
				DateTime dtmDate = new DateTime(dtmScheduleDate.Year, dtmScheduleDate.Month, dtmScheduleDate.Day);
				// select all delivery line of today in source table
				string strExpr = PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + Constants.EQUAL + "'" + dtmDate.ToString() + "'"
					+ Constants.AND + SCHEDULE_HOUR_FLD + Constants.EQUAL + "'" + dtmScheduleDate.Hour + "'";
				DataRow[] drowLines = pdtbData.Select(strExpr);
				int intMissing = MAX_LINE_PER_SLIP - drowLines.Length;
				if (drowLines.Length > MAX_LINE_PER_SLIP)
					blnIsOver = true;
				//if (intMissing < 0)
				//	continue;
				for (int i = 0; i < drowLines.Length; i++)
				{
					dtbResult.ImportRow(drowLines[i]);
				}
				for (int i = 1; i <= intMissing; i++)
				{
					DataRow drowNew = dtbResult.NewRow();
					drowNew[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmDate;
					drowNew[SCHEDULE_HOUR_FLD] = dtmScheduleDate.Hour;
					dtbResult.Rows.Add(drowNew);
				}
			}
			pdtbData.Clear();
			pdtbData = dtbResult.Copy();
			return blnIsOver;
		}
		/// <summary>
		/// Build report data match with the report template from source data
		/// </summary>
		/// <param name="pdtbData">Source Data</param>
		/// <returns>DataSet will have more than one DataTable inside if any time have more than 10 rows</returns>
		private DataSet BuildReportData(DataTable pdtbData)
		{
			DataSet dstData = new DataSet();
			DataTable dtbResult = pdtbData.Clone();
			DataTable dtbOver = pdtbData.Clone();
			dtbOver.TableName += MAX_LINE_PER_SLIP.ToString();
			foreach (DataColumn dcolData in dtbResult.Columns)
				dcolData.AllowDBNull = true;
			ArrayList arrDates = new ArrayList();
			// get all schedule date
			foreach (DataRow drowData in pdtbData.Rows)
			{
				DateTime dtmRowDate = (DateTime)drowData[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD];
				int intRowHour = (int)drowData[SCHEDULE_HOUR_FLD];
				dtmRowDate = dtmRowDate.AddHours(intRowHour);
				if (!arrDates.Contains(dtmRowDate))
					arrDates.Add(dtmRowDate);
			}
			arrDates.Sort();
			arrDates.TrimToSize();
			// foreach schedule date
			foreach (DateTime dtmScheduleDate in arrDates)
			{
				DateTime dtmDate = new DateTime(dtmScheduleDate.Year, dtmScheduleDate.Month, dtmScheduleDate.Day);
				// select all delivery line of today in source table
				string strExpr = PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + Constants.EQUAL + "'" + dtmDate.ToString() + "'"
					+ Constants.AND + SCHEDULE_HOUR_FLD + Constants.EQUAL + "'" + dtmScheduleDate.Hour + "'";
				DataRow[] drowLines = pdtbData.Select(strExpr);
				int intMissing = MAX_LINE_PER_SLIP - drowLines.Length;
				if (drowLines.Length > MAX_LINE_PER_SLIP)
				{
					for (int i = 0; i < drowLines.Length; i++)
						dtbOver.ImportRow(drowLines[i]);
				}
				else
				{
					for (int i = 0; i < drowLines.Length; i++)
						dtbResult.ImportRow(drowLines[i]);
					for (int i = 1; i <= intMissing; i++)
					{
						DataRow drowNew = dtbResult.NewRow();
						drowNew[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmDate;
						drowNew[SCHEDULE_HOUR_FLD] = dtmScheduleDate.Hour;
						dtbResult.Rows.Add(drowNew);
					}
				}
			}
			dstData.Tables.Add(dtbResult);
			if (dtbOver.Rows.Count > 0)
				dstData.Tables.Add(dtbOver);
			return dstData;
		}
	}
}