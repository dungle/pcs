using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.IO;
using System.Drawing.Printing;
using System.Data.OleDb;

using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSComMaterials.Plan.DS;
using PCSComProcurement.Purchase.BO;
using PCSComProcurement.Purchase.DS;
using PCSComProduct.Items.DS;
using PCSComUtils.DataAccess;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSUtils.Framework.ReportFrame;

using AddNewModeEnum = C1.Win.C1TrueDBGrid.AddNewModeEnum;
using AlignHorzEnum = C1.Win.C1TrueDBGrid.AlignHorzEnum;
using C1DataColumn = C1.Win.C1TrueDBGrid.C1DataColumn;
using C1DisplayColumn = C1.Win.C1TrueDBGrid.C1DisplayColumn;
using PresentationEnum = C1.Win.C1TrueDBGrid.PresentationEnum;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.C1Report;
using C1.Win.C1Preview;


namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for PartOrderSheetMultiVendorReport.
	/// </summary>
	public class PartOrderSheetMultiVendorReport : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label lblCCN;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.ComboBox cboMonth;
		private System.Windows.Forms.Label lblMonth;
		private System.Windows.Forms.Label lblYear;
		private System.Windows.Forms.ComboBox cboYear;
		private System.Windows.Forms.Label lblReportTitle;
		private System.Windows.Forms.Button btnVendor;
		private System.Windows.Forms.Label lblVendor;
		private System.Windows.Forms.TextBox txtVendor;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		#region My variables
		const string THIS = "PCSProcurement.Purchase.PartOrderSheetMultiVendorReport";

		private const string VENDOR_CUSTOMER_VIEW = "V_VendorCustomer";
		private const string VENDOR_VIEW = "V_Vendor";
		private const string VENDOR_COLUMN = "Vendor";		
				
		
		const string ZERO_STRING = "0";
		const string ISSUE_DATE_FORMAT = "dd-MMM-yyyy";
		const string NEXTMONTH_DATE_FORMAT = "MMM-yy";

		const string HEADER = "PageHeader";

		/// Result Data Table Column name
		//const string VENDOR = "Vendor";
		const string PARTYID = "PartyID";
		const string PARTNO = "Part No.";
		const string PARTNAME = "Part Name";
		const string CATEGORY = "Category";
		const string QUANTITYSET = "QuantitySet";
		const string MODEL = "Model";
		const string UM = "UM";
		const string SCHEDULE_DATE = "ScheduleDate";
		const string QUANTITY = "Quantity";
		const string ADJUSTMENT = "Adjustment";

		const string SUMROWNEXT_O = "SumRowNextO";
		const string SUMROWNEXTNEXT_O = "SumRowNextNextO";
		const string SUMROWNEXT_A = "SumRowNextA";
		const string SUMROWNEXTNEXT_A = "SumRowNextNextA";
						
		const string REPORT_LAYOUT_FILE = "PartOrderSheetMultiVendorReport.xml";
		const string REPORT_NAME = "PartOrderSheetReport";

		const string REPORTFLD_TITLE			= "fldTitle";
		const string REPORTFLD_COMPANY			= "fldCompany";
		const string REPORTFLD_ADDRESS			= "fldAddress";
		const string REPORTFLD_TEL				= "fldTel";
		const string REPORTFLD_FAX				= "fldFax";

		const string REPORTFLD_ISSUE_DATE					= "fldIssueDate";
		const string REPORTFLD_SUMROW_NEXT					= "lblSumRowNext";
		const string REPORTFLD_SUMROW_NEXTNEXT				= "lblSumRowNextNext";

		const string REPORTFLD_ORDER_MONTH				= "fldMonth";
		const string REPORTFLD_ATTENTION				= "fldAttention";
		const string REPORTFLD_REVISION					= "fldRevision";	

		const string COL_ORDER_PREFIX			= "D";
		const string COL_ADJUSTMENT_PREFIX		= "A";
		
		


		#region Constants	

		/// Result Data Table Column name
		const string VENDOR = "Vendor";		

		const string SUMROWNEXT = "SumRowNext";
		const string SUMROWNEXTNEXT = "SumRowNextNext";	

		const string REPORTFLD_PARAMETER_MONTH			= "fldParameterMonth";
		const string REPORTFLD_PARAMETER_YEAR				= "fldParameterYear";
		const string REPORTFLD_PARAMETER_VENDOR			= "fldParameterVendor";
		const string REPORTFLD_PARAMETER_CCN				= "fldParameterCCN";	

		const string COL_PREFIX = "D";
		const string PARAM_DELIMINATE = ": ";
		#endregion				





		private PCSComUtils.Framework.ReportFrame.DS.Sys_PrintConfigurationVO  mPrintConfigurationVO;
		/// <summary>
		/// External properties.
		/// PrintConfiguration MenuItem Selected function will set the Sys_PrintConfigurationVO here
		/// This form will use this Print Config to print the report
		/// </summary>
		public PCSComUtils.Framework.ReportFrame.DS.Sys_PrintConfigurationVO PrintConfigurationVO 
		{
			get
			{
				return mPrintConfigurationVO;
			}
			set
			{
				mPrintConfigurationVO = value;
			}
		}



		UtilsBO boUtil = new UtilsBO();
		#endregion

		public PartOrderSheetMultiVendorReport()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();			
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
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.lblCCN = new System.Windows.Forms.Label();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.cboMonth = new System.Windows.Forms.ComboBox();
			this.lblMonth = new System.Windows.Forms.Label();
			this.lblYear = new System.Windows.Forms.Label();
			this.cboYear = new System.Windows.Forms.ComboBox();
			this.btnVendor = new System.Windows.Forms.Button();
			this.lblVendor = new System.Windows.Forms.Label();
			this.txtVendor = new System.Windows.Forms.TextBox();
			this.lblReportTitle = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			this.SuspendLayout();
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPrint.Location = new System.Drawing.Point(4, 92);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(60, 23);
			this.btnPrint.TabIndex = 9;
			this.btnPrint.Text = "&Execute";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(264, 92);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 10;
			this.btnHelp.Text = "&Help";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(326, 92);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 11;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lblCCN
			// 
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.Location = new System.Drawing.Point(264, 6);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(32, 20);
			this.lblCCN.TabIndex = 12;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboCCN
			// 
			this.cboCCN.AddItemSeparator = ';';
			this.cboCCN.Caption = "";
			this.cboCCN.CaptionHeight = 17;
			this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboCCN.ColumnCaptionHeight = 17;
			this.cboCCN.ColumnFooterHeight = 17;
			this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboCCN.ContentHeight = 15;
			this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(304, 6);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(80, 21);
			this.cboCCN.TabIndex = 0;
			this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
				"ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:N" +
				"ear;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Cente" +
				"r;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Sty" +
				"le10{}Style11{}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
				"olSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoote" +
				"rHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0," +
				" 118, 158</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Hei" +
				"ght>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
				"Style parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" />" +
				"<GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Sty" +
				"le2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle par" +
				"ent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordS" +
				"electorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selec" +
				"ted\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxV" +
				"iew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" " +
				"me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=" +
				"\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"S" +
				"elected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=" +
				"\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Rec" +
				"ordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1<" +
				"/vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWid" +
				"th>16</DefaultRecSelWidth></Blob>";
			// 
			// cboMonth
			// 
			this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMonth.ItemHeight = 13;
			this.cboMonth.Items.AddRange(new object[] {
														  "1",
														  "2",
														  "3",
														  "4",
														  "5",
														  "6",
														  "7",
														  "8",
														  "9",
														  "10",
														  "11",
														  "12"});
			this.cboMonth.Location = new System.Drawing.Point(58, 29);
			this.cboMonth.Name = "cboMonth";
			this.cboMonth.Size = new System.Drawing.Size(82, 21);
			this.cboMonth.TabIndex = 2;
			// 
			// lblMonth
			// 
			this.lblMonth.ForeColor = System.Drawing.Color.Maroon;
			this.lblMonth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMonth.Location = new System.Drawing.Point(4, 27);
			this.lblMonth.Name = "lblMonth";
			this.lblMonth.Size = new System.Drawing.Size(52, 23);
			this.lblMonth.TabIndex = 14;
			this.lblMonth.Text = "Month";
			this.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblYear
			// 
			this.lblYear.ForeColor = System.Drawing.Color.Maroon;
			this.lblYear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblYear.Location = new System.Drawing.Point(4, 4);
			this.lblYear.Name = "lblYear";
			this.lblYear.Size = new System.Drawing.Size(52, 23);
			this.lblYear.TabIndex = 13;
			this.lblYear.Text = "Year";
			this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboYear
			// 
			this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboYear.ItemHeight = 13;
			this.cboYear.Location = new System.Drawing.Point(58, 6);
			this.cboYear.Name = "cboYear";
			this.cboYear.Size = new System.Drawing.Size(82, 21);
			this.cboYear.TabIndex = 1;
			// 
			// btnVendor
			// 
			this.btnVendor.Location = new System.Drawing.Point(216, 52);
			this.btnVendor.Name = "btnVendor";
			this.btnVendor.Size = new System.Drawing.Size(22, 20);
			this.btnVendor.TabIndex = 4;
			this.btnVendor.Text = "...";
			this.btnVendor.Click += new System.EventHandler(this.btnVendor_Click);
			// 
			// lblVendor
			// 
			this.lblVendor.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVendor.Location = new System.Drawing.Point(4, 52);
			this.lblVendor.Name = "lblVendor";
			this.lblVendor.Size = new System.Drawing.Size(52, 20);
			this.lblVendor.TabIndex = 15;
			this.lblVendor.Text = "Vendor";
			this.lblVendor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtVendor
			// 
			this.txtVendor.Location = new System.Drawing.Point(58, 52);
			this.txtVendor.Name = "txtVendor";
			this.txtVendor.Size = new System.Drawing.Size(156, 20);
			this.txtVendor.TabIndex = 3;
			this.txtVendor.Text = "";
			this.txtVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendor_KeyDown);
			this.txtVendor.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendor_Validating);
			// 
			// lblReportTitle
			// 
			this.lblReportTitle.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.lblReportTitle.Location = new System.Drawing.Point(272, 32);
			this.lblReportTitle.Name = "lblReportTitle";
			this.lblReportTitle.Size = new System.Drawing.Size(98, 28);
			this.lblReportTitle.TabIndex = 18;
			this.lblReportTitle.Text = "PART ORDER SHEET";
			this.lblReportTitle.Visible = false;
			// 
			// PartOrderSheetMultiVendorReport
			// 
			this.AcceptButton = this.btnPrint;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(388, 118);
			this.Controls.Add(this.btnVendor);
			this.Controls.Add(this.lblVendor);
			this.Controls.Add(this.txtVendor);
			this.Controls.Add(this.cboYear);
			this.Controls.Add(this.lblYear);
			this.Controls.Add(this.lblMonth);
			this.Controls.Add(this.cboMonth);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblReportTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "PartOrderSheetMultiVendorReport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Part Order Sheet Report";
			this.Load += new System.EventHandler(this.PartOrderSheetMultiVendorReport_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		
		#region FORM UI EVENT HANDLE

		/// <summary>
		/// thachnn: 14/Nov/2005
		/// Form load
		/// Check security
		/// Make CCN COmbobox
		/// Make Year combo box
		/// set current selected month and year to current DB Time
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>		
		private void PartOrderSheetMultiVendorReport_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".CPOReport_Load()";
			try
			{
				#region Check Security

				Security objSecurity = new Security();
				this.Name = THIS;
				//objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName);
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Close();
					return;
				}

				#endregion

				// inititalize form				
				UtilsBO boUtils = new UtilsBO();

				// Load combo box CCN
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, boUtils.ListCCN().Tables[MST_CCNTable.TABLE_NAME], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
				//cboCCN.SelectedValue = SystemProperty.CCNID;
				InitYearCombo();
				DateTime dtmServerDate = boUtils.GetDBDate();
				// set default month to server month
				cboMonth.SelectedIndex = dtmServerDate.Month - 1;
				// set selected default to server year
				cboYear.SelectedItem = dtmServerDate.Year;
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

		/// <summary>
		/// /// thachnn: 14/Nov/2005
		/// close form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}				
		
		/// <summary>
		/// Open the PartOrderSheet Report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			ShowPartOrderSheetMultiVendorReport(sender, e);
		}
	
		/// <summary>
		/// Multi select the Vendor
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnVendor_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".TESTbtnVendor_Click()";			
			try
			{
				// user must select CCN first
				if (cboCCN.SelectedValue == null || cboCCN.SelectedValue == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_WOROUTING_SELECT_CCN_FIRST, MessageBoxIcon.Error);
					cboCCN.Focus();
					return;
				}

				DataTable dtbData = null;
				if (sender is TextBox && sender != null)
					dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(VENDOR_VIEW, MST_PartyTable.CODE_FLD, txtVendor.Text.Trim(), null, false);
				else
					dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(VENDOR_VIEW, MST_PartyTable.CODE_FLD, txtVendor.Text.Trim(), null, true);
				if (dtbData != null && dtbData.Rows.Count > 0)
				{
					string strVendor_List = string.Empty;
					string strVendorCode_List = string.Empty;
					foreach (DataRow drowData in dtbData.Rows)
					{
						strVendor_List += drowData[MST_PartyTable.PARTYID_FLD] + ",";
						strVendorCode_List += drowData[MST_PartyTable.CODE_FLD] + ",";
					}
					txtVendor.Tag = strVendor_List.TrimEnd(',');
					txtVendor.Text = strVendorCode_List.TrimEnd(',');
					// grdSelectedVendors.DataSource = dtbData;
				}
				else
				{				
					txtVendor.Text = string.Empty;
					txtVendor.Tag = null;
					txtVendor.Focus();
					txtVendor.SelectAll();
				}			
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


	
		/// <summary>
		/// open search forms
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtVendor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnVendor_Click(sender, e);
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		
		}

		/// <summary>
		/// /// thachnn: 14/Nov/2005
		/// OpenSearchForm when leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtVendor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_Validating()";
			try
			{
				if (txtVendor.Modified) 
				{
					if (txtVendor.Text.Trim() == string.Empty)
					{
						txtVendor.Tag = null;
						//					grdSelectedVendors.DataSource = null;
						//					grdSelectedVendors.Refresh();
						return;
					}
					btnVendor_Click(txtVendor,null);					
					e.Cancel = true;
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

		
		#endregion

		/// <summary>
		/// /// thachnn: 14/Nov/2005
		/// Clear all condition information on the form
		/// </summary>
		private void ClearSearchingCondition()
		{
			try
			{	
				DateTime dtmServerDate = (new UtilsBO()).GetDBDate();
				// set default month to server month
				cboMonth.SelectedIndex = dtmServerDate.Month - 1;
				// set selected default to server year
				cboYear.SelectedItem = dtmServerDate.Year;
				
				txtVendor.Text = string.Empty;
				txtVendor.Tag = ZERO_STRING;
				
				cboMonth.Focus();				
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		/// <summary>
		/// /// thachnn: 14/Nov/2005
		/// Init Year combo
		/// </summary>
		private void InitYearCombo()
		{
			try
			{
				// year start from 2000 to 2050
				for (int i = 2000; i < 2051; i++)
					cboYear.Items.Add(i);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		



		/// <summary>
		/// thachnn: 17/11/2005
		/// Print the Part Order Sheet Report
		/// Using the "PartOrderSheetReport.xml" layout
		/// </summary>		
		private void ShowPartOrderSheetMultiVendorReport(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + "ShowPartOrderSheetMultiVendorReport()";	

			string strAttention = "txtVendorName.Text";
			string strRevision = "txtRevision.Text";

			try
			{						
				#region PREPARE			

				string mstrReportDefFolder = Application.StartupPath + "\\" +Constants.REPORT_DEFINITION_STORE_LOCATION;
				DataTable dtbResult;
				DataTable dtbResultNext;
				DataTable dtbResultNextNext;
				PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
				int nCCNID;
				int nMonth	= System.Convert.ToInt32(cboMonth.SelectedItem);
				int nYear	= System.Convert.ToInt32(cboYear.SelectedItem);
				string strVendorID_List = txtVendor.Tag == null? string.Empty : txtVendor.Tag.ToString();				
				string strCCN = string.Empty;
				string strPurchaseOrderMasterCode = string.Empty;
				//string strVendor = string.Empty;
				string strCategory = string.Empty;
				string strProduct = string.Empty;

				/// contain array of string: 01 02 03 .. of day of month in the dtbResult, except the missing day			
				ArrayList arrDueDateHeading = new ArrayList();	

				/// Build to keep value of pair: 01 --> 01-Mon, ... depend on the real data of dtbResule
				NameValueCollection arrDayNumberMapToDayWithDayOfWeek = new NameValueCollection();

				/// Build to keep value of pair: 01 --> 01-Mon, ... NOT DEPEND on the real data
				NameValueCollection arrFullDayNumberMapToDayWithDayOfWeek = new NameValueCollection();

	
				Cursor = Cursors.WaitCursor;		
		
				// check report layout file is exist or not
				if (!File.Exists(mstrReportDefFolder + @"\" + REPORT_LAYOUT_FILE))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
					Cursor = Cursors.Default;
					return;
				}
				#endregion

				#region	GETTING THE PARAMETER
		
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
//				// Check if user does not select PO Number
//				if("txtOrderNo.Text".Trim() == string.Empty)
//				{
//					string[] arrParams = {"lblOrderNo.Text"};
//					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error,arrParams);
//					//OLD: txtOrderNo.Focus();
//					Cursor = Cursors.Default;
//					return;
//				}				

				#endregion			
			
				nCCNID = int.Parse(cboCCN.SelectedValue.ToString());
				strCCN = boUtil.GetCCNCodeFromID(nCCNID);
				//strPurchaseOrderMasterCode = "txtOrderNo.Text".Trim();
				
//				DateTime dtmOrderMonth = DateTime.MinValue;		
//				///	Order Date will getting like the grid of PO_Delivery Schedule Form
//				///	Build the data table like PO_Delivery Schedule Form. getting the first record, schedule date
//				try
//				{
//					PODeliveryScheduleBO objPODeliveryScheduleBO = new PODeliveryScheduleBO();
//					//OLD: int nCurrentPurchaseOrderDetail = int.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
//					int nCurrentPurchaseOrderDetail = 1;
//
//					DataSet dstTempDeliverySchedule = objPODeliveryScheduleBO.GetDeliverySchedule(nCurrentPurchaseOrderDetail);
//					if(dstTempDeliverySchedule != null)
//					{
//						if(dstTempDeliverySchedule.Tables.Count > 0)
//						{
//							if(dstTempDeliverySchedule.Tables[0].Rows.Count > 0)
//							{
//								try
//								{
//									dtmOrderMonth = (DateTime)dstTempDeliverySchedule.Tables[0].Rows[0][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD];									
//								}
//								catch{}
//							}
//						}
//					}
//				}
//				catch
//				{
//					///if can't get dtmOrderMonth, may be there is error in the other module
//					///we still generate the report with month= 1 and year = 1900, it will be a empty report
//				}
//				nYear = dtmOrderMonth.Year;
//				nMonth = dtmOrderMonth.Month;				
//
//				// if input null, then we send the int.MinValue to the BO function
//				// Not mandatory id field will have int.MinValue if it is not selected
//				try
//				{
//					nVendorID = (int)(txtVendor.Tag);
//					strVendor = objBO.GetVendorCodeFromID(nVendorID) + "-" + objBO.GetVendorNameFromID(nVendorID);
//				}
//				catch
//				{
//					strVendor = string.Empty;
//				}
				//				try
				//				{
				//					nProductID = (int)(txtItem.Tag);
				//					strProduct = objBO.GetProductCodeFromID(nProductID) + "-" + objBO.GetProductNameFromID(nProductID);
				//				}
				//				catch
				//				{
				//					strProduct = string.Empty;
				//				}
				//				try
				//				{
				//					nCategoryID = (int)(txtCategory.Tag);
				//					strCategory = objBO.GetCategoryCodeFromID(nCategoryID) + "-" + objBO.GetCategoryNameFromID(nCategoryID);
				//				}
				//				catch
				//				{
				//					strCategory = string.Empty;
				//				}			
			
				#endregion
			
				#region BUILDING THE TABLE (getting from database by BO)
				DataSet dstResult = objBO.GetPartOrderSheetMultiVendorReportData(nCCNID,  nMonth,  nYear,  strVendorID_List);
				dtbResult = dstResult.Tables[0];
				dtbResultNext = dstResult.Tables[1];
				dtbResultNextNext = dstResult.Tables[2];
				#endregion

				#region TRANSFORM ORIGINAL TABLE FOR REPORT
				

				#region BUILD THE FULL DayWithDayOfWeek Pair	// full from 1 to 31
				DateTime dtmTemp = new DateTime(nYear,nMonth,1);
				for(int i = 0 ; i <31 ; i++)
				{
					DateTime dtm = dtmTemp.AddDays(i);
					arrFullDayNumberMapToDayWithDayOfWeek.Add(dtm.Day.ToString("00"), dtm.Day.ToString("00")+"-"+dtm.DayOfWeek.ToString().Substring(0,3)  );
				}

				#endregion
	
				#region GETTING THE DATE HEADING
				ArrayList arrDueDate = GetColumnValuesFromTable(dtbResult,SCHEDULE_DATE);				
				ArrayList arrItems = GetPartyID_PartNoGROUPFromTable(dtbResult,PARTYID, PARTNO);

				foreach(DateTime dtm in arrDueDate)
				{
					string strColumnName = COL_ORDER_PREFIX + dtm.Day.ToString("00");
					string strColumnNameA = COL_ADJUSTMENT_PREFIX + dtm.Day.ToString("00");
					arrDayNumberMapToDayWithDayOfWeek.Add(dtm.Day.ToString("00"), dtm.Day.ToString("00")+"-"+dtm.DayOfWeek.ToString().Substring(0,3)  );
					arrDueDateHeading.Add(strColumnName);
					arrDueDateHeading.Add(strColumnNameA);
				}		

				#endregion

			
				DataTable dtbTransform = BuildPartOrderSheetTable(arrDueDateHeading);
				/// fill data to the dtbTransform
				foreach(string strItem in arrItems)
				{
					// Create DUMMYROW FIRST
					DataRow dtrNew = dtbTransform.NewRow();

					double dblSumRowNext = 0;
					double dblSumRowNextNext = 0;				

					double dblSumRowNextA = 0;
					double dblSumRowNextNextA = 0;

					string strFilter = 	strFilter = string.Format("[{0}]='{1}' AND [{2}]='{3}' " ,
						PARTYID, strItem.Split('#')[0] ,
						PARTNO,strItem.Split('#')[1]);
					string strSort = string.Format("[{0}] ASC, [{1}] ASC", PARTYID, PARTNO);

					/// Getting next total for the next, and next next month of order quantity
					/// Getting next total for the next, and next next month of adjustment
					DataRow[] dtrowsNext = dtbResultNext.Select(strFilter,strSort);
					foreach(DataRow dtr in dtrowsNext)
					{
						try
						{
							dblSumRowNext += Convert.ToDouble(dtr[QUANTITY]);
						}
						catch{}
						try
						{
							dblSumRowNextA += Convert.ToDouble(dtr[ADJUSTMENT]);
						}
						catch{}
					}
					DataRow[] dtrowsNextNext = dtbResultNextNext.Select(strFilter,strSort);
					foreach(DataRow dtr in dtrowsNextNext)
					{
						try
						{
							dblSumRowNextNext += Convert.ToDouble(dtr[QUANTITY]);
						}
						catch{}
						try
						{
							dblSumRowNextNextA += Convert.ToDouble(dtr[ADJUSTMENT]);
						}
						catch{}
					}
					
					DataRow[] dtrows = dtbResult.Select(strFilter,strSort);
					foreach(DataRow dtr in dtrows)
					{
						/// fill data to the dummy row
						/// these column is persistance, we always set to the first rows
						dtrNew[VENDOR] = dtrows[0][VENDOR];
						dtrNew[PARTNO] = dtrows[0][PARTNO];
						dtrNew[PARTNAME] = dtrows[0][PARTNAME];
						dtrNew[MODEL] = dtrows[0][MODEL];
						
						/// fill the Quantity of the day to the cell (indicate by column Dxx in this dummy rows)
						string strDateColumnToFill = COL_ORDER_PREFIX + ((DateTime)dtr[SCHEDULE_DATE]).Day.ToString("00");
						dtrNew[strDateColumnToFill] = dtr[QUANTITY];

						/// fill the Quantity of the day to the cell (indicate by column Dxx in this dummy rows)
						strDateColumnToFill = COL_ADJUSTMENT_PREFIX + ((DateTime)dtr[SCHEDULE_DATE]).Day.ToString("00");
						dtrNew[strDateColumnToFill] = dtr[ADJUSTMENT];
						
						/// fill the SumRow of next month and next next month to this dummy rows
						/// we calculated these values before
						
						/// Order Delivery quantity
						dtrNew[SUMROWNEXT_O] = dblSumRowNext;
						dtrNew[SUMROWNEXTNEXT_O] = dblSumRowNextNext;

						/// Adjustment Quantity
						dtrNew[SUMROWNEXT_A] = dblSumRowNextA;
						dtrNew[SUMROWNEXTNEXT_A] = dblSumRowNextNextA;
					}
				
					// add to the transform data table
					dtbTransform.Rows.Add(dtrNew);				
				}	    
			
				#endregion

				#region RENDER REPORT
		
		
				ReportBuilder objRB;	
				objRB = new ReportBuilder();				
				try
				{
					objRB.ReportName = REPORT_NAME;
					//					string strSort = string.Format("[{0}] ASC, [{1}] ASC, [{2}] ASC,[{3}] ASC,[{4}] ASC,[{5}] ASC,[{6}] ASC "   ,     VENDOR,CATEGORY,PARTNO,PARTNAME,MODEL,UM,QUANTITYSET );
					//					DataTable dtbToDisplayInReport = new DataTable();
					//					foreach(DataRow dtr in dtbTransform.Select(string.Empty,strSort))
					//					{
					//						dtbToDisplayInReport.ImportRow(dtr);
					//					}
					//					objRB.SourceDataTable = dtbToDisplayInReport;
					objRB.SourceDataTable = dtbTransform;
				}
				catch// (Exception ex)
				{
					/// we can't preview while we don't have any data
					return;
				}

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
				//grid.DataSource = objRB.RenderDataTable;

			
				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
				
				printPreview.FormTitle = lblReportTitle.Text + " "  + nMonth.ToString("00") + "-"+ nYear.ToString("0000") ;			

				objRB.ReportViewer = printPreview.ReportViewer;				
				objRB.RenderReport();			

						

				#region MODIFY THE REPORT LAYOUT
				
				objRB.DrawPredefinedField(REPORTFLD_TITLE, lblReportTitle.Text.Trim());
				//objRB.DrawPredefinedField(REPORTFLD_ATTENTION, strAttention.ToUpper());								

				
				DateTime dtmOrderMonth = new DateTime(nYear,nMonth,1);
				objRB.DrawPredefinedField(REPORTFLD_ORDER_MONTH,			dtmOrderMonth.ToString(NEXTMONTH_DATE_FORMAT).ToUpper());
				objRB.DrawPredefinedField(REPORTFLD_SUMROW_NEXT,			dtmOrderMonth.AddMonths(1).ToString(NEXTMONTH_DATE_FORMAT));
				objRB.DrawPredefinedField(REPORTFLD_SUMROW_NEXTNEXT,	dtmOrderMonth.AddMonths(2).ToString(NEXTMONTH_DATE_FORMAT));					

				
				//get this date time from the Form (TransDate on form)
				// OLD:  DateTime dtmTransDate = (DateTime)cboOrderDate.Value;
				//DateTime dtmTransDate = new DateTime(2006,1,4);
				//objRB.DrawPredefinedField(REPORTFLD_ISSUE_DATE, dtmTransDate.ToString(ISSUE_DATE_FORMAT));
				//objRB.DrawPredefinedField(REPORTFLD_REVISION, strRevision);			


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

				#region DRAW Parameters
/*				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				arrParamAndValue.Add(lblCCN.Text,cboCCN.Text);
				arrParamAndValue.Add(lblYear.Text, nYear.ToString());
				arrParamAndValue.Add(lblMonth.Text, nMonth.ToString());								
				arrParamAndValue.Add(lblVendor.Text,txtVendor.Text);
				arrParamAndValue.Add(lblVendor.Text,txtVendor.Text);
				arrParamAndValue.Add(lblVendor.Text,txtVendor.Text);
				arrParamAndValue.Add(lblVendor.Text,txtVendor.Text);
			
				/// anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = objRB.GetFieldByName(REPORTFLD_TITLE);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3*fldTitle.RenderHeight;
				objRB.GetSectionByName(HEADER).CanGrow = true;
				objRB.DrawParameters( objRB.GetSectionByName(HEADER) ,dblStartX , dblStartY , arrParamAndValue, objRB.Report.Font.Size);
*/
				#endregion			

			
				#region RENAME THE COLUMN HEADING TEXT
				ArrayList arrColumnHeadings = new ArrayList();				
				for(int i = 0; i <= 31; i++) /// clear the heading text
				{
					objRB.DrawPredefinedField(COL_ORDER_PREFIX+i.ToString("00")+"Lbl","");
				}

				for(int i = 0; i <= 31; i++)
				{
					/// Paint the EMPTY Colummn to
					try
					{
						if(arrDueDateHeading.Contains(COL_ORDER_PREFIX+i.ToString("00"))   )
						{
							string strHeading = arrDayNumberMapToDayWithDayOfWeek[i.ToString("00")].Substring(0,6);
							objRB.DrawPredefinedField(COL_ORDER_PREFIX+i.ToString("00")+"Lbl",strHeading);
						}
						else
						{
							string strHeading = arrFullDayNumberMapToDayWithDayOfWeek[i.ToString("00")].Substring(0,6);
							objRB.DrawPredefinedField(COL_ORDER_PREFIX+i.ToString("00")+"Lbl",strHeading);
							/// HACKED: Thachnn: from now we don't paint the empty Column to WhiteSmoke.
							/// objRB.Report.Fields[COL_ORDER_PREFIX+i.ToString("00")+"Lbl"].BackColor = Color.WhiteSmoke;
						}
					}
					catch	// draw continue, don't care about error value in the parrValuesToFill
					{
						//break;
					}

					/// Paint the WEEKEND Colummn to Red on Yealow
					try
					{
						if(objRB.Report.Fields[COL_ORDER_PREFIX+i.ToString("00")+"Lbl"] != null)
						{
							/// this variable contain sat, sun, mon tue, ...
							string strDateName = objRB.GetFieldByName(COL_ORDER_PREFIX+i.ToString("00")+"Lbl").Text.Substring(3,3);
							//if(strDateName == "Sat" || strDateName == "Sun")
							if(strDateName == "Sun")
							{
								objRB.Report.Fields[COL_ORDER_PREFIX+i.ToString("00")+"Lbl"].BackColor = Color.Yellow;
								objRB.Report.Fields[COL_ORDER_PREFIX+i.ToString("00")+"Lbl"].ForeColor = Color.Red;								
							}
						}
					}
					catch	// draw continue, don't care about error value in the parrValuesToFill
					{
						//break;
					}

					/// REMOVE THE DATE STRING (Mon, Tue, ..) as Mr.CuongNT's request, hic hic hic. I add, then I have to remove
					/// Rename it to 1 2 3 4 5 6
					try
					{
						objRB.Report.Fields[COL_ORDER_PREFIX+i.ToString("00")+"Lbl"].Text = i.ToString();
						objRB.Report.Fields[COL_ORDER_PREFIX+i.ToString("00")+"Lbl"].Font.Bold = false;
					}
					catch{}
				}		

				#endregion
			
				#endregion	
					
				objRB.RefreshReport();
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
		
		/// <summary>
		/// Thachnn : 15/Oct/2005
		/// Browse the DataTable, get all value of column with provided named.
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>
		/// <param name="pstrColumnName">COlumn Name in pdtb DataTable to collect values from</param>
		/// <returns>ArrayList of object, collect from pdtb's column named pstrColumnName. Empty ArrayList if error or not found any row in pdtb.</returns>
		private static ArrayList GetColumnValuesFromTable(DataTable pdtb, string pstrColumnName)
		{
			ArrayList arrRet = new ArrayList();
			try
			{
				foreach (DataRow drow in pdtb.Rows)
				{
					object objGet = drow[pstrColumnName];
					if( !arrRet.Contains(objGet)  )
					{
						arrRet.Add(objGet);
					}
				}
			}
			catch
			{
				arrRet.Clear();
			}
			return arrRet;
		}
		
		/// <summary>
		/// Thachnn : 08/Nov/2005
		/// Browse the DataTable, get all value of Vendor, Category, PartNo column, insert into ArraysList as VendorValue#CategoryValue#PartNoValue
		/// Because Item differ from each other by Vendor, Category and partNo
		/// So this triple group will be unique between Items
		/// </summary>
		/// <history>Thachnn: 28/12/2005: change unique condition. PartNo and Model are Unique for each Item</history>
		/// <param name="pdtb">DataTable to collect values</param>			
		/// <param name="pstrCategoryColName"></param>
		/// <param name="pstrPartNoColName"></param>
		/// <returns>ArrayList of object, collect CategoryValue#PartNoValue pairs from pdtb. Empty ArrayList if error or not found any row in pdtb.</returns>		
		private static ArrayList GetPartNo_Model_UM_GROUPFromTable(DataTable pdtb, string pstrPartNoColName, string pstrModel, string pstrUM)
		{
			ArrayList arrRet = new ArrayList();
			try
			{				
				foreach (DataRow drow in pdtb.Rows)
				{										
					object objPartNoGet = drow[pstrPartNoColName];
					object objModelGet = drow[pstrModel];
					object objUMGet = drow[pstrUM];					
					string str = objPartNoGet.ToString() +"#"+  objModelGet.ToString() + "#" + objUMGet.ToString() ;
					if( !arrRet.Contains(str)  )
					{
						arrRet.Add(str);
					}
				}
			}
			catch
			{
				arrRet.Clear();
			}
			return arrRet;
		}

		/// <summary>
		/// /// thachnn: 14/Nov/2005
		/// Build the crosstab table to render on report
		/// contain D01 ---> D31 , A01 ---> A31, and NextMonth SumRow O A, NextNextMonth SumRow O A
		/// see Schedule of local parts in month UseCase under PCS/06-Project Design/DesignReport folder.
		/// </summary>
		/// <remarks>		
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildPartOrderSheetTable(ArrayList parrScheduleDateHeading)
		{
			const string strPartOrderSheetTableName = "PartOrderSheetTable";
			try
			{
				//Create table
				DataTable dtbRet = new DataTable(strPartOrderSheetTableName);
				
				//Add columns
				dtbRet.Columns.Add(PARTYID, typeof(System.String));
				dtbRet.Columns.Add(VENDOR, typeof(System.String));
				dtbRet.Columns.Add(PARTNO, typeof(System.String));											
				dtbRet.Columns.Add(PARTNAME, typeof(System.String));
				dtbRet.Columns.Add(MODEL, typeof(System.String));

				dtbRet.Columns.Add(SUMROWNEXT_O, typeof(System.Double));
				dtbRet.Columns.Add(SUMROWNEXTNEXT_O, typeof(System.Double));

				dtbRet.Columns.Add(SUMROWNEXT_A, typeof(System.Double));
				dtbRet.Columns.Add(SUMROWNEXTNEXT_A, typeof(System.Double));

				foreach(string strColumnName in parrScheduleDateHeading)
				{					
					try
					{
						dtbRet.Columns.Add(strColumnName,typeof(System.Double));
					}
					catch{}
				}
				// FILL the null column, if not exist null column (not existed date.) report will gen ###,#0 to the cell	
				for(int i = 1; i <=31; i++)												  
				{
					if(parrScheduleDateHeading.Contains(COL_ORDER_PREFIX + i.ToString("00")) == false )
					{		
						try
						{
							dtbRet.Columns.Add(COL_ORDER_PREFIX + i.ToString("00"),typeof(System.String));							
						}
						catch{}						
					}
					if(parrScheduleDateHeading.Contains(COL_ADJUSTMENT_PREFIX + i.ToString("00")) == false )
					{		
						try
						{
							dtbRet.Columns.Add(COL_ADJUSTMENT_PREFIX + i.ToString("00"),typeof(System.String));							
						}
						catch{}						
					}
				}
				//				dtbRet.Columns.Add("Boolean", typeof(System.Boolean));
				//				dtbRet.Columns.Add("Int32", typeof(System.Int32));											
				//				dtbRet.Columns.Add("String", typeof(System.String));
				//				dtbRet.Columns.Add("Double", typeof(System.Double));
				//				dtbRet.Columns.Add("DateTime", typeof(System.DateTime));		
				
				return dtbRet;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Thachnn : 08/Nov/2005
		/// Browse the DataTable, get all value of PartyID-PartNo pair column, insert into ArraysList as PartyIDPartNoValue
		/// Because Item differ from each other by PartyID and partNo
		/// So this group will be unique between Items of report
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>			
		/// <param name="pstrPartyIDColName"></param>
		/// <param name="pstrPartNoColName"></param>
		/// <returns>ArrayList of object, collect PartyID - PartNoValue pairs from pdtb. Empty ArrayList if error or not found any row in pdtb.</returns>
		private static ArrayList GetPartyID_PartNoGROUPFromTable(DataTable pdtb , string pstrPartyIDColName, string pstrPartNoColName)
		{
			ArrayList arrRet = new ArrayList();
			try
			{	
				foreach (DataRow drow in pdtb.Rows)
				{
					object objPartyIDGet = drow[pstrPartyIDColName];
					object objPartNoGet = drow[pstrPartNoColName];
					string str = objPartyIDGet.ToString().Trim() +"#"+ objPartNoGet.ToString().Trim();					
					if( !arrRet.Contains(str)  )
					{
						arrRet.Add(str);
					}
				}
			}
			catch
			{
				arrRet.Clear();
			}
			return arrRet;
		}

	
	

	}
}
