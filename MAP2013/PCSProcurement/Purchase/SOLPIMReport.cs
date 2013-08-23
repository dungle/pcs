using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
#region USING FOR C1 REPORT
using System.Drawing.Printing;
using System.Data.OleDb;
using System.Collections.Specialized;
using System.IO;
using C1.Win.C1Preview;
//using C1.C1Report;
//using BorderStyleEnum = C1.C1Report.BorderStyleEnum;
//using Group = C1.C1Report.Group;
#endregion


namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for SOLPIMReport.
	/// </summary>
	public class SOLPIMReport : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnCategory;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.TextBox txtCategory;
		private System.Windows.Forms.Label lblCCN;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.ComboBox cboMonth;
		private System.Windows.Forms.Label lblMonth;
		private System.Windows.Forms.Label lblYear;
		private System.Windows.Forms.ComboBox cboYear;
		private System.Windows.Forms.Label lblReportTitle;
		private System.Windows.Forms.Button btnItem;
		private System.Windows.Forms.Label lblItem;
		private System.Windows.Forms.TextBox txtItem;
		private System.Windows.Forms.Button btnVendor;
		private System.Windows.Forms.Label lblVendor;
		private System.Windows.Forms.TextBox txtVendor;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		#region My variables
		const string THIS = "PCSProcurement.Purchase.SOLPIMReport";
		private const string ZERO_STRING = "0";

		private const string VENDOR_VIEW = "V_LocalVendor";
		private const string VENDOR_COLUMN = "Vendor";

		#region Constants
		const string ISSUE_DATE_FORMAT = "dd-MMM-yyyy";
		const string NEXTMONTH_DATE_FORMAT = "MMM-yy";

		/// Result Data Table Column name
		const string CCN = "CCN";				
		const string ITEM = "Item";

		const string VENDOR = "Vendor";
		const string PARTNO = "Part No.";
		const string PARTNAME = "Part Name";
		const string CATEGORY = "Category";
		const string QUANTITYSET = "QuantitySet";
		const string MODEL = "Model";
		const string UM = "UM";
		const string SCHEDULE_DATE = "ScheduleDate";
		const string QUANTITY = "Quantity";

		const string SUMROWNEXT = "SumRowNext";
		const string SUMROWNEXTNEXT = "SumRowNextNext";
						
		const string REPORT_LAYOUT_FILE = "ScheduleOfLocalPartsInMonthReport.xml";
		const string REPORT_NAME = "ScheduleOfLocalPartsInMonth Report";

		const string REPORTFLD_TITLE			= "fldTitle";
		const string REPORTFLD_COMPANY			= "fldCompany";
		const string REPORTFLD_ADDRESS			= "fldAddress";
		const string REPORTFLD_TEL				= "fldTel";
		const string REPORTFLD_FAX				= "fldFax";

		const string REPORTFLD_ISSUE_DATE					= "fldIssueDate";
		const string REPORTFLD_SUMROW_NEXT					= "lblSumRowNext";
		const string REPORTFLD_SUMROW_NEXTNEXT				= "lblSumRowNextNext";

		const string REPORTFLD_PARAMETER_MONTH				= "fldParameterMonth";
		const string REPORTFLD_PARAMETER_YEAR				= "fldParameterYear";			
		const string REPORTFLD_PARAMETER_VENDOR				= "fldParameterVendor";
		const string REPORTFLD_PARAMETER_CATEGORY			= "fldParameterCategory";
		const string REPORTFLD_PARAMETER_CCN				= "fldParameterCCN";				
		const string REPORTFLD_PARAMETER_ITEM				= "fldParameterItem";

		const string PAGE = "Page";
		const string HEADER = "Header";

		const string PREFIX_DAYINMONTH = "lblDayInMonth";
		const string PREFIX_DAYOFWEEK = "lblDayOfWeek";
		const string PREFIX_DETAILDAY = "ColumnOfDay" ;

		const string PARAM_DELIMINATE = ": ";
		#endregion				



		UtilsBO boUtil = new UtilsBO();
		#endregion

		public SOLPIMReport()
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
			this.btnCategory = new System.Windows.Forms.Button();
			this.btnItem = new System.Windows.Forms.Button();
			this.lblCategory = new System.Windows.Forms.Label();
			this.lblItem = new System.Windows.Forms.Label();
			this.txtCategory = new System.Windows.Forms.TextBox();
			this.txtItem = new System.Windows.Forms.TextBox();
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
			this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPrint.Location = new System.Drawing.Point(8, 128);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(60, 23);
			this.btnPrint.TabIndex = 9;
			this.btnPrint.Text = "&Execute";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(280, 128);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 10;
			this.btnHelp.Text = "&Help";
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(342, 128);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 11;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnCategory
			// 
			this.btnCategory.Location = new System.Drawing.Point(218, 74);
			this.btnCategory.Name = "btnCategory";
			this.btnCategory.Size = new System.Drawing.Size(22, 20);
			this.btnCategory.TabIndex = 6;
			this.btnCategory.Text = "...";
			this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
			// 
			// btnItem
			// 
			this.btnItem.Location = new System.Drawing.Point(218, 96);
			this.btnItem.Name = "btnItem";
			this.btnItem.Size = new System.Drawing.Size(22, 20);
			this.btnItem.TabIndex = 8;
			this.btnItem.Text = "...";
			this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
			// 
			// lblCategory
			// 
			this.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCategory.Location = new System.Drawing.Point(6, 74);
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.Size = new System.Drawing.Size(52, 20);
			this.lblCategory.TabIndex = 16;
			this.lblCategory.Text = "Category";
			this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblItem
			// 
			this.lblItem.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblItem.Location = new System.Drawing.Point(6, 96);
			this.lblItem.Name = "lblItem";
			this.lblItem.Size = new System.Drawing.Size(52, 20);
			this.lblItem.TabIndex = 17;
			this.lblItem.Text = "Item";
			this.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtCategory
			// 
			this.txtCategory.Location = new System.Drawing.Point(60, 74);
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.Size = new System.Drawing.Size(156, 20);
			this.txtCategory.TabIndex = 5;
			this.txtCategory.Text = "";
			this.txtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategory_KeyDown);
			this.txtCategory.Validating += new System.ComponentModel.CancelEventHandler(this.txtCategory_Validating);
			// 
			// txtItem
			// 
			this.txtItem.Location = new System.Drawing.Point(60, 96);
			this.txtItem.Name = "txtItem";
			this.txtItem.Size = new System.Drawing.Size(156, 20);
			this.txtItem.TabIndex = 7;
			this.txtItem.Text = "";
			this.txtItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
			this.txtItem.Validating += new System.ComponentModel.CancelEventHandler(this.txtItem_Validating);
			// 
			// lblCCN
			// 
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.Location = new System.Drawing.Point(282, 6);
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
			this.cboCCN.Location = new System.Drawing.Point(322, 6);
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
				" 116, 156</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Hei" +
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
			this.cboMonth.Location = new System.Drawing.Point(60, 29);
			this.cboMonth.Name = "cboMonth";
			this.cboMonth.Size = new System.Drawing.Size(82, 21);
			this.cboMonth.TabIndex = 2;
			// 
			// lblMonth
			// 
			this.lblMonth.ForeColor = System.Drawing.Color.Maroon;
			this.lblMonth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMonth.Location = new System.Drawing.Point(6, 27);
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
			this.lblYear.Location = new System.Drawing.Point(6, 4);
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
			this.cboYear.Location = new System.Drawing.Point(60, 6);
			this.cboYear.Name = "cboYear";
			this.cboYear.Size = new System.Drawing.Size(82, 21);
			this.cboYear.TabIndex = 1;
			// 
			// btnVendor
			// 
			this.btnVendor.Location = new System.Drawing.Point(218, 52);
			this.btnVendor.Name = "btnVendor";
			this.btnVendor.Size = new System.Drawing.Size(22, 20);
			this.btnVendor.TabIndex = 4;
			this.btnVendor.Text = "...";
			this.btnVendor.Click += new System.EventHandler(this.btnVendor_Click);
			// 
			// lblVendor
			// 
			this.lblVendor.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVendor.Location = new System.Drawing.Point(6, 52);
			this.lblVendor.Name = "lblVendor";
			this.lblVendor.Size = new System.Drawing.Size(52, 20);
			this.lblVendor.TabIndex = 15;
			this.lblVendor.Text = "Vendor";
			this.lblVendor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtVendor
			// 
			this.txtVendor.Location = new System.Drawing.Point(60, 52);
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
			this.lblReportTitle.Location = new System.Drawing.Point(280, 58);
			this.lblReportTitle.Name = "lblReportTitle";
			this.lblReportTitle.Size = new System.Drawing.Size(98, 44);
			this.lblReportTitle.TabIndex = 18;
			this.lblReportTitle.Text = "SCHEDULE OF LOCAL PARTS IN MONTH";
			this.lblReportTitle.Visible = false;
			// 
			// SOLPIMReport
			// 
			this.AcceptButton = this.btnPrint;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(408, 161);
			this.Controls.Add(this.btnVendor);
			this.Controls.Add(this.lblVendor);
			this.Controls.Add(this.txtVendor);
			this.Controls.Add(this.txtCategory);
			this.Controls.Add(this.txtItem);
			this.Controls.Add(this.cboYear);
			this.Controls.Add(this.lblYear);
			this.Controls.Add(this.lblMonth);
			this.Controls.Add(this.cboMonth);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnCategory);
			this.Controls.Add(this.btnItem);
			this.Controls.Add(this.lblCategory);
			this.Controls.Add(this.lblItem);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblReportTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "SOLPIMReport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Schedule of Local Parts In Month Report";
			this.Load += new System.EventHandler(this.CPOReport_Load);
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
		private void CPOReport_Load(object sender, System.EventArgs e)
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
		/// /// thachnn: 14/Nov/2005
		/// OpenSearchForm when leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtItem_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtItem_Validating()";
			try
			{
				if (!txtItem.Modified) return;
				if (txtItem.Text.Trim() == string.Empty)
				{
					txtItem.Tag = null;
					//txtLocation.Text = string.Empty;
					//txtLocation.Tag = null;
					return;
				}
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();
				//User has selected CCN
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue);	
				}
				else //User has not selected CCN
				{
					htbCriteria.Add(ITM_ProductTable.CCNID_FLD, 0);
				}
				if(txtCategory.Text != string.Empty)
				{
					htbCriteria.Add(ITM_ProductTable.CATEGORYID_FLD, txtCategory.Tag);
				}
				drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtItem.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					if ((txtItem.Tag != null) && (int.Parse(txtItem.Tag.ToString())) != int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString()))
					{
						//txtLocation.Text = string.Empty;
						//txtLocation.Tag = null;
					}
					txtItem.Text = drwResult[ITM_ProductTable.CODE_FLD].ToString();
					txtItem.Tag = drwResult[ITM_ProductTable.PRODUCTID_FLD];
				}
				else
					e.Cancel = true;
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
		/// txtItem_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Thachnn</author>
		/// <date>Friday, October 28 2005</date>
		private void txtItem_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtItem_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnItem_Click(sender, e);
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
		/// txtCategory_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Thachnn</author>
		/// <date>Friday, October 28 2005</date>
		private void txtCategory_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_Validating()";
			try
			{
				if (!txtCategory.Modified) return;

				/// process come here , mean Category Change, we also reset the txtItem
				txtItem.Text = string.Empty;
				txtItem.Tag = null;

				if (txtCategory.Text.Trim() == string.Empty)
				{
					txtCategory.Tag = null;					
					return;
				}
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text.Trim(), null, false);
				if (drwResult != null)
				{
					txtCategory.Text = drwResult[ITM_CategoryTable.CODE_FLD].ToString();
					txtCategory.Tag = drwResult[ITM_CategoryTable.CATEGORYID_FLD];
				}
				e.Cancel = true;
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
		/// txtCategory_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Thachnn</author>
		/// <date>Friday, October 28 2005</date>
		private void txtCategory_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnCategory_Click(sender, e);
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
		/// btnMasLoc_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Thachnn</author>
		/// <date>Friday, October 28 2005</date>
		private void btnItem_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnItem_Click()";			
			try
			{
				// user must select CCN first
				if (cboCCN.SelectedValue == null || cboCCN.SelectedValue == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_WOROUTING_SELECT_CCN_FIRST, MessageBoxIcon.Error);
					cboCCN.Focus();
					return;
				}
				DataRowView drowData = null;
				Hashtable htData = new Hashtable();
				if(txtCategory.Text != string.Empty)
				{
					htData.Add(ITM_ProductTable.CATEGORYID_FLD, int.Parse(txtCategory.Tag.ToString()));
				}
				if (sender is TextBox && sender != null)
					drowData = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtItem.Text.Trim(), htData, false);
				else
					drowData = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtItem.Text.Trim(), htData, true);

				if (drowData != null)
				{
					txtItem.Text = drowData[ITM_ProductTable.CODE_FLD].ToString().Trim();
					txtItem.Tag = int.Parse(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString().Trim());
				}
				else
				{
					txtItem.Focus();
					txtItem.SelectAll();
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
		/// btnCategory_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Thachnn</author>
		/// <date>Friday, October 28 2005</date>
		private void btnCategory_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCategory_Click()";
			try
			{
				// user must select CCN first
				if (cboCCN.SelectedValue == null || cboCCN.SelectedValue == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_WOROUTING_SELECT_CCN_FIRST, MessageBoxIcon.Error);
					cboCCN.Focus();
					return;
				}
				DataRowView drowData = null;
				//Hashtable htData = new Hashtable();
				//htData.Add(MST_WorkCenterTable.CCNID_FLD, int.Parse(cboCCN.SelectedValue.ToString()));
				if (sender is TextBox && sender != null)
					drowData = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text.Trim(), null, false);// htData, false);
				else
					drowData = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text.Trim(), null, true);

				if (drowData != null)
				{
					if(txtCategory.Text != drowData[ITM_CategoryTable.CODE_FLD].ToString().Trim())
					{
						/// clear the txtItem
						txtItem.Tag = null;
						txtItem.Text = string.Empty;
					}
					txtCategory.Text = drowData[ITM_CategoryTable.CODE_FLD].ToString().Trim();
					txtCategory.Tag = int.Parse(drowData[ITM_CategoryTable.CATEGORYID_FLD].ToString().Trim());
				}
				else
				{
					txtCategory.Focus();
					txtCategory.SelectAll();
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

		
		/// HACK:
		/// <summary>
		/// Thachnn: 28/10/2005
		/// Preview the report for this form
		/// Using the "InventoryStatusReport.xml" layout
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, System.EventArgs e)		
		{
			const string METHOD_NAME = THIS + ".btnPrint_Click()";	
			try
			{			
				string mstrReportDefFolder = Application.StartupPath + "\\" +Constants.REPORT_DEFINITION_STORE_LOCATION;
				DataTable dtbResult;
				DataTable dtbResultNext;
				DataTable dtbResultNextNext;
				PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
				int nCCNID;
				int nMonth;
				int nYear;
				int nVendorID = int.MinValue;				// optional parameter
				int nCategoryID = int.MinValue;				// optional parameter
				int nProductID = int.MinValue;				// optional parameter

			
				string strCCN = string.Empty;				
				string strVendor = string.Empty;				
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
				//Check if user does not select Year
				if(cboYear.SelectedItem == null)
				{
					string[] arrParams = {lblYear.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error,arrParams);
					cboYear.Focus();
					Cursor = Cursors.Default;
					return;
				}
				//Check if user does not select Month
				if(cboMonth.SelectedItem == null)
				{
					string[] arrParams = {lblMonth.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error,arrParams);
					cboMonth.Focus();
					Cursor = Cursors.Default;
					return;
				}

				#endregion			
			
				nCCNID = int.Parse(cboCCN.SelectedValue.ToString());
				strCCN = boUtil.GetCCNCodeFromID(nCCNID);
				nYear = int.Parse(cboYear.SelectedItem.ToString());			
				nMonth = int.Parse(cboMonth.SelectedItem.ToString());			
				

				// if input null, then we send the int.MinValue to the BO function
				// Not mandatory id field will have int.MinValue if it is not selected
				try
				{
					nVendorID = (int)(txtVendor.Tag);
					strVendor = objBO.GetVendorCodeFromID(nVendorID) + PARAM_DELIMINATE + objBO.GetVendorNameFromID(nVendorID);
				}
				catch
				{
					strVendor = string.Empty;
				}
				try
				{
					nProductID = (int)(txtItem.Tag);
					strProduct = objBO.GetProductCodeFromID(nProductID) + PARAM_DELIMINATE + objBO.GetProductNameFromID(nProductID);
				}
				catch
				{
					strProduct = string.Empty;
				}
				try
				{
					nCategoryID = (int)(txtCategory.Tag);
					strCategory = objBO.GetCategoryCodeFromID(nCategoryID) + PARAM_DELIMINATE + objBO.GetCategoryNameFromID(nCategoryID);
				}
				catch
				{
					strCategory = string.Empty;
				}			
			
				#endregion
			
				#region BUILDING THE TABLE (getting from database by BO)
				DataSet dstResult = objBO.GetScheduleOfLocalPartsInMonthReportData(nCCNID,  nMonth,  nYear,  nVendorID, nCategoryID, nProductID);
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
				ArrayList arrItems = GetPartNo_Model_UM_GROUPFromTable(dtbResult,PARTNO,MODEL,UM);

				foreach(DateTime dtm in arrDueDate)
				{
					string strColumnName = PREFIX_DETAILDAY + dtm.Day.ToString("00");
					arrDayNumberMapToDayWithDayOfWeek.Add(dtm.Day.ToString("00"), dtm.Day.ToString("00")+"-"+dtm.DayOfWeek.ToString().Substring(0,3)  );
					arrDueDateHeading.Add(strColumnName);
				}
			

				#endregion		


			
				DataTable dtbTransform = BuildSOLPIMTable(arrDueDateHeading);
				foreach(string strItem in arrItems)
				{
					// Create DUMMYROW FIRST
					DataRow dtrNew = dtbTransform.NewRow();

					double dblSumRowNext = 0;
					double dblSumRowNextNext = 0;				
					
					
					string strFilter = string.Empty;

					/// HACKED: Thachnn: 28/12/2005: OLD code, use Category as one of unique element, now we use PartNo and Model
					/// if strItem.Split('#')[0] ==  string.empty, its mean Category value is null
					/// so we put IsNull in the filter string (to select from dtbResult);					
//					if(strItem.Split('#')[1] == string.Empty)
//					{
//						strFilter = 
//							string.Format("[{0}]='{1}' AND [{2}] is null AND [{3}]='{4}'",
//							VENDOR,
//							strItem.Split('#')[0],
//							CATEGORY,							
//							PARTNO,
//							strItem.Split('#')[2]);
//					}
//					else
					/// ENDHACKED: Thachnn : 28/12/2005

					strFilter = string.Format("[{0}]='{1}' AND [{2}]='{3}' AND [{4}]='{5}'",
						PARTNO,
						strItem.Split(Constants.REPORT_FONT_SEPARATOR)[0],
						MODEL,
						strItem.Split(Constants.REPORT_FONT_SEPARATOR)[1],
						UM,
						strItem.Split(Constants.REPORT_FONT_SEPARATOR)[2]
						);

					string strSort = string.Format("[{0}] ASC, [{1}] ASC , [{2}] ASC  ",     PARTNO, MODEL, UM);

					DataRow[] dtrowsNext = dtbResultNext.Select(strFilter,strSort);
					foreach(DataRow dtr in dtrowsNext)
					{
                        dblSumRowNext += Convert.ToDouble(dtr[QUANTITY]);
					}
					DataRow[] dtrowsNextNext = dtbResultNextNext.Select(strFilter,strSort);
					foreach(DataRow dtr in dtrowsNextNext)
					{
						dblSumRowNextNext += Convert.ToDouble(dtr[QUANTITY]);
					}

					DataRow[] dtrows = dtbResult.Select(strFilter,strSort);
					foreach(DataRow dtr in dtrows)
					{
						/// fill data to the dummy row
						/// these column is persistance, we always set to the first rows
						dtrNew[VENDOR] = dtrows[0][VENDOR];
						dtrNew[PARTNO] = dtrows[0][PARTNO];
						dtrNew[PARTNAME] = dtrows[0][PARTNAME];
						dtrNew[CATEGORY] = dtrows[0][CATEGORY];
						dtrNew[MODEL] = dtrows[0][MODEL];
						dtrNew[QUANTITYSET] = dtrows[0][QUANTITYSET];
						dtrNew[UM] = dtrows[0][UM];

						/// fill the Quantity of the day to the cell (indicate by column ColumnOfDayxx in this dummy rows)
						string strDateColumnToFill = PREFIX_DETAILDAY + ((DateTime)dtr[SCHEDULE_DATE]).Day.ToString(ReportBuilder.FORMAT_DAY_2CHAR);
						dtrNew[strDateColumnToFill] = dtr[QUANTITY];
						
						/// fill the SumRow of next month and next next month to this dummy rows
						/// we calculated these values before
						dtrNew[SUMROWNEXT] = dblSumRowNext;
						dtrNew[SUMROWNEXTNEXT] = dblSumRowNextNext;
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
				DateTime dtmDBTime = (new UtilsBO()).GetDBDate();
				objRB.DrawPredefinedField(REPORTFLD_ISSUE_DATE, dtmDBTime.ToString(ISSUE_DATE_FORMAT));

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


				#region PUSH PARAMETER VALUE	- DRAW Parameters

				objRB.DrawPredefinedField(REPORTFLD_PARAMETER_MONTH, nMonth.ToString("00"));
				objRB.DrawPredefinedField(REPORTFLD_PARAMETER_YEAR, nYear.ToString("0000"));

				DateTime dtmNextMonth = new DateTime(nYear,nMonth,1);				
				objRB.DrawPredefinedField(REPORTFLD_SUMROW_NEXT,		dtmNextMonth.AddMonths(1).ToString(NEXTMONTH_DATE_FORMAT));
				objRB.DrawPredefinedField(REPORTFLD_SUMROW_NEXTNEXT,	dtmNextMonth.AddMonths(2).ToString(NEXTMONTH_DATE_FORMAT));
				

				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				arrParamAndValue.Add(CCN, strCCN);				
				if(strVendor.Trim() != string.Empty)
				{
					arrParamAndValue.Add(VENDOR, strVendor);
				}
				if(strCategory.Trim() != string.Empty)
				{
					arrParamAndValue.Add(CATEGORY, strCategory);
				}
				if(strProduct.Trim() != string.Empty)
				{
					arrParamAndValue.Add(ITEM, strProduct);
				}
		

				/// anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = objRB.GetFieldByName(REPORTFLD_TITLE);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3*fldTitle.RenderHeight;
				objRB.GetSectionByName(HEADER).CanGrow = true;
				objRB.DrawParameters( objRB.GetSectionByName(PAGE + HEADER) ,dblStartX , dblStartY , arrParamAndValue, objRB.Report.Font.Size);

					
				#endregion		
			
				#region RENAME THE COLUMN HEADING TEXT
				ArrayList arrColumnHeadings = new ArrayList();				
				for(int i = 0; i <= 31; i++) /// clear the heading text
				{
					objRB.DrawPredefinedField(PREFIX_DAYINMONTH+i.ToString(ReportBuilder.FORMAT_DAY_2CHAR),"");
				}                
				objRB.DrawPredefinedList_DaysOfWeek(nYear, nMonth,
					PREFIX_DAYINMONTH,
					PREFIX_DAYOFWEEK,
					1 ,DateTime.DaysInMonth(nYear , nMonth ) );

				#endregion
			
				#endregion	
				

				objRB.RefreshReport();
				printPreview.Show();						
				this.Cursor = Cursors.Default;

				#endregion

			}
			catch(Exception ex)
			{
				//DEBUG: PCSMessageBox.Show(ErrorCode.MESSAGE_RENVIEW_REPORT,MessageBoxIcon.Error);
				//
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
		/// Display search form to select Production Line
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnVendor_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendor_Click()";
			try
			{
				// user must select CCN first
				if (cboCCN.SelectedValue == null || cboCCN.SelectedValue == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_WOROUTING_SELECT_CCN_FIRST, MessageBoxIcon.Error);
					cboCCN.Focus();
					return;
				}

				DataRowView drowData = null;
				Hashtable htbCriteria = new Hashtable();				
				//htbCriteria.Add(VENDOR_COLUMN, 1);

				if (sender is TextBox && sender != null)
					drowData = FormControlComponents.OpenSearchForm(VENDOR_VIEW, MST_PartyTable.CODE_FLD, txtVendor.Text.Trim(), htbCriteria, false);
				else
					drowData = FormControlComponents.OpenSearchForm(VENDOR_VIEW, MST_PartyTable.CODE_FLD, txtVendor.Text.Trim(), htbCriteria, true);

				if (drowData != null)
				{
					txtVendor.Text = drowData[MST_PartyTable.CODE_FLD].ToString().Trim();
					txtVendor.Tag = int.Parse(drowData[MST_PartyTable.PARTYID_FLD].ToString().Trim());
				}
				else
				{
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
				if (!txtVendor.Modified) return;
				if (txtVendor.Text.Trim() == string.Empty)
				{
					txtVendor.Tag = null;
					return;
				}
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();				
				//htbCriteria.Add(VENDOR_COLUMN, 1);

				drwResult = FormControlComponents.OpenSearchForm(VENDOR_VIEW, MST_PartyTable.CODE_FLD, txtVendor.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendor.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
				}
				e.Cancel = true;
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
		/// Thachnn: 10/11/2005
		/// Clear all parameter on form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboPlanType_SelectedIndexChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboPlanType_SelectedIndexChanged()";

			try
			{
				ClearSearchingCondition();
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
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

		#endregion



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
		/// /// thachnn: 14/Nov/2005
		/// Build the crosstab table to render on report
		/// contain D01 ---> D31 , and NextMonth SumRow, NextNextMonth SumRow
		/// see Schedule of local parts in month UseCase under PCS/06-Project Design/DesignReport folder.
		/// </summary>
		/// <remarks>		
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildSOLPIMTable(ArrayList parrScheduleDateHeading)
		{
			const string strSOLPIMTableName = "SOLPIMTable";
			try
			{
				//Create table
				DataTable dtbRet = new DataTable(strSOLPIMTableName);
				
				//Add columns
				dtbRet.Columns.Add(VENDOR, typeof(System.String));
				dtbRet.Columns.Add(PARTNO, typeof(System.String));											
				dtbRet.Columns.Add(PARTNAME, typeof(System.String));
				dtbRet.Columns.Add(CATEGORY, typeof(System.String));                				
				dtbRet.Columns.Add(QUANTITYSET, typeof(System.Double));
				dtbRet.Columns.Add(MODEL, typeof(System.String));
				dtbRet.Columns.Add(UM, typeof(System.String));

				dtbRet.Columns.Add(SUMROWNEXT, typeof(System.Double));
				dtbRet.Columns.Add(SUMROWNEXTNEXT, typeof(System.Double));

				foreach(string strColumnName in parrScheduleDateHeading)
				{					
					try
					{
						dtbRet.Columns.Add(strColumnName,typeof(System.Double));
					}
					catch{}
				}
				// FILL the null column				
				for(int i = 1; i <=31; i++)												  
				{
					if(parrScheduleDateHeading.Contains(PREFIX_DETAILDAY + i.ToString(ReportBuilder.FORMAT_DAY_2CHAR)) == false )
					{		
						try
						{
							dtbRet.Columns.Add(PREFIX_DETAILDAY + i.ToString(ReportBuilder.FORMAT_DAY_2CHAR),typeof(System.String));
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
					string str = objPartNoGet.ToString() +"|"+  objModelGet.ToString() + "|" + objUMGet.ToString() ;
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

				txtItem.Text = string.Empty;
				txtItem.Tag = ZERO_STRING;

				txtCategory.Text = string.Empty;
				txtCategory.Tag = ZERO_STRING;

				cboMonth.Focus();				
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

	
	
		

	}
}
