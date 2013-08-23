using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for PageSetup.
	/// </summary>
	public class PageSetup : Form
	{
		private TabControl tabPageSetup;
		private TabPage tabGeneral;
		private TabPage tabFonts;
		private ComboBox cboGutterPosition;
		private Button btnFieldProperties;
		private TextBox txtSignatures;
		private Label lblSignatures;
		private ComboBox cboTable;
		private Label lblTable;
		private ComboBox cboPaperSize;
		private Label lblPaperSize;
		private ComboBox cboOrientation;
		private Label lblOrientation;
		private GroupBox grpMargins;
		private Label lblGutterPosition;
		private Label lblGutter;
		private Label lblBottom;
		private Label lblLeft;
		private Label lblRight;
		private Label lblTop;
		private Button btnClose;
		private Button btnDefault;
		private Button btnSave;
		private Button btnHelp;
		private Button btnReportHeaderFont;
		private GroupBox grpReportFooter;
		private Button btnReportFooter;
		private GroupBox grpPageFooter;
		private Button btnPageFooter;
		private GroupBox grpPageHeader;
		private Button btnPageHeader;
		private GroupBox grpDetail;
		private Button btnDetail;
		private GroupBox grpGroupBy;
		private Button btnGroupBy;
		private GroupBox grpParameter;
		private Button btnParameterFont;
		private GroupBox grpReportHeader;
		private Button btnReportFooterDefault;
		private Button btnPageFooterDefault;
		private Button btnPageHeaderDefault;
		private Button btnDetailDefault;
		private Button btnGroupByDefault;
		private Button btnParaDefault;
		private Button btnReportHeaderDefault;
		private FontDialog dlgFonts;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private sys_ReportVO mvoSysReport;
		private EditReportBO boEditReport = new EditReportBO();
		private ReportManagementBO boReportManagement = new ReportManagementBO();
		private const string THIS = "PCSUtils.Framework.ReportFrame.PageSetup";
		private System.Windows.Forms.TextBox txtReportHeaderFont;
		private System.Windows.Forms.TextBox txtParamFont;
		private System.Windows.Forms.TextBox txtPageHeaderFont;
		private System.Windows.Forms.TextBox txtGroupFont;
		private System.Windows.Forms.TextBox txtPageFooterFont;
		private System.Windows.Forms.TextBox txtReportFooterFont;
		private System.Windows.Forms.TextBox txtDetailFont;
		private const string FONT_SEPARATOR = "|";
		private bool blnIsEditing = false;

		#region DEFAULT REPORT PAGE SETUP SETTING

		/// <summary>
		/// Exist in Constant class
		/// </summary>
//		private const decimal decMarginTop = 5;
//		private const decimal decMarginBottom = 5;
//		private const decimal decMarginLeft = 5;
//		private const decimal decMarginRight = 5;
//		private const decimal decGutter = 5;
//        private const int intGutterPosition = 0;
		private const int intOrientation = 0;
		private const int intPaperSize = (int)PaperKind.A4;
		private const int intTable = 0;
		private const string strSignature = "Chief Of Accountant (sign, name) \t Accountant (sign, name)";
		private C1.Win.C1Input.C1NumericEdit nudMarginTop;
		private C1.Win.C1Input.C1NumericEdit nudMarginLeft;
		private C1.Win.C1Input.C1NumericEdit nudMarginGutter;
		private C1.Win.C1Input.C1NumericEdit nudMarginBottom;
		private C1.Win.C1Input.C1NumericEdit nudMarginRight;
		private System.Windows.Forms.Label lblDefaultDetailFont;
		private System.Windows.Forms.Label lblDefaultReportFooterFont;
		private System.Windows.Forms.Label lblDefaultPageFooterFont;
		private System.Windows.Forms.Label lblDefaultGroupByFont;
		private System.Windows.Forms.Label lblDefaultPageHeaderFont;
		private System.Windows.Forms.Label lblDefaultReportHeaderFont;
		private System.Windows.Forms.Label lblDefaultParameterFont;
		private System.Windows.Forms.Button btnMarginDefault;
		private const string strDefaultFont = "ArialNarrow|6|Regular|1|False|Point";
		
		#endregion

		public sys_ReportVO VoSysReport
		{
			get { return this.mvoSysReport; }
			set { this.mvoSysReport = value; }
		}

		public PageSetup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			InitializeComboPaperSize();
		}


		/// <summary>
		/// Thachnn: 13/Oct/2005
		/// Init Display Text and report Value for cboPaperSize
		/// cboPaperSize will display Text of enum PaperKind and return int Value of enum PaperKind		
		/// Using ClscboPaperSizeData (define at the end of this source file)
		/// </summary>
		private void InitializeComboPaperSize()
		{
			string METHOD_NAME = THIS + ".InitializeComboPaperSize()";
			try
			{			
				ArrayList arrcboData = new ArrayList();
				arrcboData.Add(new ClscboPaperSizeData(PaperKind.A4));
				arrcboData.Add(new ClscboPaperSizeData(PaperKind.Letter));
				arrcboData.Add(new ClscboPaperSizeData(PaperKind.Legal));
				arrcboData.Add(new ClscboPaperSizeData(PaperKind.A3));			
				cboPaperSize.DataSource = arrcboData;

				/// Define here or define when designing by change properties of cboPaperSize
				cboPaperSize.DisplayMember = "DisplayMember";
				cboPaperSize.ValueMember = "ValueMember";
			}
			catch (Exception ex)
			{
				/// TODO: Define ErrorCode or Display Message Here
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
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PageSetup));
			this.tabPageSetup = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.btnMarginDefault = new System.Windows.Forms.Button();
			this.btnFieldProperties = new System.Windows.Forms.Button();
			this.txtSignatures = new System.Windows.Forms.TextBox();
			this.lblSignatures = new System.Windows.Forms.Label();
			this.cboTable = new System.Windows.Forms.ComboBox();
			this.lblTable = new System.Windows.Forms.Label();
			this.cboPaperSize = new System.Windows.Forms.ComboBox();
			this.lblPaperSize = new System.Windows.Forms.Label();
			this.cboOrientation = new System.Windows.Forms.ComboBox();
			this.lblOrientation = new System.Windows.Forms.Label();
			this.grpMargins = new System.Windows.Forms.GroupBox();
			this.nudMarginBottom = new C1.Win.C1Input.C1NumericEdit();
			this.nudMarginRight = new C1.Win.C1Input.C1NumericEdit();
			this.nudMarginGutter = new C1.Win.C1Input.C1NumericEdit();
			this.nudMarginLeft = new C1.Win.C1Input.C1NumericEdit();
			this.cboGutterPosition = new System.Windows.Forms.ComboBox();
			this.lblGutterPosition = new System.Windows.Forms.Label();
			this.lblGutter = new System.Windows.Forms.Label();
			this.lblBottom = new System.Windows.Forms.Label();
			this.lblLeft = new System.Windows.Forms.Label();
			this.lblRight = new System.Windows.Forms.Label();
			this.lblTop = new System.Windows.Forms.Label();
			this.nudMarginTop = new C1.Win.C1Input.C1NumericEdit();
			this.tabFonts = new System.Windows.Forms.TabPage();
			this.grpReportFooter = new System.Windows.Forms.GroupBox();
			this.txtReportFooterFont = new System.Windows.Forms.TextBox();
			this.btnReportFooterDefault = new System.Windows.Forms.Button();
			this.btnReportFooter = new System.Windows.Forms.Button();
			this.grpPageFooter = new System.Windows.Forms.GroupBox();
			this.txtPageFooterFont = new System.Windows.Forms.TextBox();
			this.btnPageFooterDefault = new System.Windows.Forms.Button();
			this.btnPageFooter = new System.Windows.Forms.Button();
			this.grpPageHeader = new System.Windows.Forms.GroupBox();
			this.txtPageHeaderFont = new System.Windows.Forms.TextBox();
			this.btnPageHeaderDefault = new System.Windows.Forms.Button();
			this.btnPageHeader = new System.Windows.Forms.Button();
			this.grpDetail = new System.Windows.Forms.GroupBox();
			this.btnDetailDefault = new System.Windows.Forms.Button();
			this.btnDetail = new System.Windows.Forms.Button();
			this.txtDetailFont = new System.Windows.Forms.TextBox();
			this.grpGroupBy = new System.Windows.Forms.GroupBox();
			this.txtGroupFont = new System.Windows.Forms.TextBox();
			this.btnGroupByDefault = new System.Windows.Forms.Button();
			this.btnGroupBy = new System.Windows.Forms.Button();
			this.grpParameter = new System.Windows.Forms.GroupBox();
			this.txtParamFont = new System.Windows.Forms.TextBox();
			this.btnParaDefault = new System.Windows.Forms.Button();
			this.btnParameterFont = new System.Windows.Forms.Button();
			this.grpReportHeader = new System.Windows.Forms.GroupBox();
			this.txtReportHeaderFont = new System.Windows.Forms.TextBox();
			this.btnReportHeaderDefault = new System.Windows.Forms.Button();
			this.btnReportHeaderFont = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnDefault = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.dlgFonts = new System.Windows.Forms.FontDialog();
			this.lblDefaultDetailFont = new System.Windows.Forms.Label();
			this.lblDefaultReportFooterFont = new System.Windows.Forms.Label();
			this.lblDefaultPageFooterFont = new System.Windows.Forms.Label();
			this.lblDefaultGroupByFont = new System.Windows.Forms.Label();
			this.lblDefaultPageHeaderFont = new System.Windows.Forms.Label();
			this.lblDefaultReportHeaderFont = new System.Windows.Forms.Label();
			this.lblDefaultParameterFont = new System.Windows.Forms.Label();
			this.tabPageSetup.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.grpMargins.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMarginBottom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMarginRight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMarginGutter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMarginLeft)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMarginTop)).BeginInit();
			this.tabFonts.SuspendLayout();
			this.grpReportFooter.SuspendLayout();
			this.grpPageFooter.SuspendLayout();
			this.grpPageHeader.SuspendLayout();
			this.grpDetail.SuspendLayout();
			this.grpGroupBy.SuspendLayout();
			this.grpParameter.SuspendLayout();
			this.grpReportHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabPageSetup
			// 
			this.tabPageSetup.AccessibleDescription = resources.GetString("tabPageSetup.AccessibleDescription");
			this.tabPageSetup.AccessibleName = resources.GetString("tabPageSetup.AccessibleName");
			this.tabPageSetup.Alignment = ((System.Windows.Forms.TabAlignment)(resources.GetObject("tabPageSetup.Alignment")));
			this.tabPageSetup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabPageSetup.Anchor")));
			this.tabPageSetup.Appearance = ((System.Windows.Forms.TabAppearance)(resources.GetObject("tabPageSetup.Appearance")));
			this.tabPageSetup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPageSetup.BackgroundImage")));
			this.tabPageSetup.Controls.Add(this.tabGeneral);
			this.tabPageSetup.Controls.Add(this.tabFonts);
			this.tabPageSetup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabPageSetup.Dock")));
			this.tabPageSetup.Enabled = ((bool)(resources.GetObject("tabPageSetup.Enabled")));
			this.tabPageSetup.Font = ((System.Drawing.Font)(resources.GetObject("tabPageSetup.Font")));
			this.tabPageSetup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabPageSetup.ImeMode")));
			this.tabPageSetup.ItemSize = ((System.Drawing.Size)(resources.GetObject("tabPageSetup.ItemSize")));
			this.tabPageSetup.Location = ((System.Drawing.Point)(resources.GetObject("tabPageSetup.Location")));
			this.tabPageSetup.Name = "tabPageSetup";
			this.tabPageSetup.Padding = ((System.Drawing.Point)(resources.GetObject("tabPageSetup.Padding")));
			this.tabPageSetup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabPageSetup.RightToLeft")));
			this.tabPageSetup.SelectedIndex = 0;
			this.tabPageSetup.ShowToolTips = ((bool)(resources.GetObject("tabPageSetup.ShowToolTips")));
			this.tabPageSetup.Size = ((System.Drawing.Size)(resources.GetObject("tabPageSetup.Size")));
			this.tabPageSetup.TabIndex = ((int)(resources.GetObject("tabPageSetup.TabIndex")));
			this.tabPageSetup.Text = resources.GetString("tabPageSetup.Text");
			this.tabPageSetup.Visible = ((bool)(resources.GetObject("tabPageSetup.Visible")));
			// 
			// tabGeneral
			// 
			this.tabGeneral.AccessibleDescription = resources.GetString("tabGeneral.AccessibleDescription");
			this.tabGeneral.AccessibleName = resources.GetString("tabGeneral.AccessibleName");
			this.tabGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabGeneral.Anchor")));
			this.tabGeneral.AutoScroll = ((bool)(resources.GetObject("tabGeneral.AutoScroll")));
			this.tabGeneral.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabGeneral.AutoScrollMargin")));
			this.tabGeneral.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabGeneral.AutoScrollMinSize")));
			this.tabGeneral.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabGeneral.BackgroundImage")));
			this.tabGeneral.Controls.Add(this.btnMarginDefault);
			this.tabGeneral.Controls.Add(this.btnFieldProperties);
			this.tabGeneral.Controls.Add(this.txtSignatures);
			this.tabGeneral.Controls.Add(this.lblSignatures);
			this.tabGeneral.Controls.Add(this.cboTable);
			this.tabGeneral.Controls.Add(this.lblTable);
			this.tabGeneral.Controls.Add(this.cboPaperSize);
			this.tabGeneral.Controls.Add(this.lblPaperSize);
			this.tabGeneral.Controls.Add(this.cboOrientation);
			this.tabGeneral.Controls.Add(this.lblOrientation);
			this.tabGeneral.Controls.Add(this.grpMargins);
			this.tabGeneral.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabGeneral.Dock")));
			this.tabGeneral.Enabled = ((bool)(resources.GetObject("tabGeneral.Enabled")));
			this.tabGeneral.Font = ((System.Drawing.Font)(resources.GetObject("tabGeneral.Font")));
			this.tabGeneral.ImageIndex = ((int)(resources.GetObject("tabGeneral.ImageIndex")));
			this.tabGeneral.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabGeneral.ImeMode")));
			this.tabGeneral.Location = ((System.Drawing.Point)(resources.GetObject("tabGeneral.Location")));
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabGeneral.RightToLeft")));
			this.tabGeneral.Size = ((System.Drawing.Size)(resources.GetObject("tabGeneral.Size")));
			this.tabGeneral.TabIndex = ((int)(resources.GetObject("tabGeneral.TabIndex")));
			this.tabGeneral.Text = resources.GetString("tabGeneral.Text");
			this.tabGeneral.ToolTipText = resources.GetString("tabGeneral.ToolTipText");
			this.tabGeneral.Visible = ((bool)(resources.GetObject("tabGeneral.Visible")));
			this.tabGeneral.Click += new System.EventHandler(this.tabGeneral_Click);
			// 
			// btnMarginDefault
			// 
			this.btnMarginDefault.AccessibleDescription = resources.GetString("btnMarginDefault.AccessibleDescription");
			this.btnMarginDefault.AccessibleName = resources.GetString("btnMarginDefault.AccessibleName");
			this.btnMarginDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnMarginDefault.Anchor")));
			this.btnMarginDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMarginDefault.BackgroundImage")));
			this.btnMarginDefault.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnMarginDefault.Dock")));
			this.btnMarginDefault.Enabled = ((bool)(resources.GetObject("btnMarginDefault.Enabled")));
			this.btnMarginDefault.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnMarginDefault.FlatStyle")));
			this.btnMarginDefault.Font = ((System.Drawing.Font)(resources.GetObject("btnMarginDefault.Font")));
			this.btnMarginDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnMarginDefault.Image")));
			this.btnMarginDefault.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMarginDefault.ImageAlign")));
			this.btnMarginDefault.ImageIndex = ((int)(resources.GetObject("btnMarginDefault.ImageIndex")));
			this.btnMarginDefault.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnMarginDefault.ImeMode")));
			this.btnMarginDefault.Location = ((System.Drawing.Point)(resources.GetObject("btnMarginDefault.Location")));
			this.btnMarginDefault.Name = "btnMarginDefault";
			this.btnMarginDefault.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnMarginDefault.RightToLeft")));
			this.btnMarginDefault.Size = ((System.Drawing.Size)(resources.GetObject("btnMarginDefault.Size")));
			this.btnMarginDefault.TabIndex = ((int)(resources.GetObject("btnMarginDefault.TabIndex")));
			this.btnMarginDefault.Text = resources.GetString("btnMarginDefault.Text");
			this.btnMarginDefault.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMarginDefault.TextAlign")));
			this.btnMarginDefault.Visible = ((bool)(resources.GetObject("btnMarginDefault.Visible")));
			this.btnMarginDefault.Click += new System.EventHandler(this.btnMarginDefault_Click);
			// 
			// btnFieldProperties
			// 
			this.btnFieldProperties.AccessibleDescription = resources.GetString("btnFieldProperties.AccessibleDescription");
			this.btnFieldProperties.AccessibleName = resources.GetString("btnFieldProperties.AccessibleName");
			this.btnFieldProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnFieldProperties.Anchor")));
			this.btnFieldProperties.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFieldProperties.BackgroundImage")));
			this.btnFieldProperties.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnFieldProperties.Dock")));
			this.btnFieldProperties.Enabled = ((bool)(resources.GetObject("btnFieldProperties.Enabled")));
			this.btnFieldProperties.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnFieldProperties.FlatStyle")));
			this.btnFieldProperties.Font = ((System.Drawing.Font)(resources.GetObject("btnFieldProperties.Font")));
			this.btnFieldProperties.Image = ((System.Drawing.Image)(resources.GetObject("btnFieldProperties.Image")));
			this.btnFieldProperties.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnFieldProperties.ImageAlign")));
			this.btnFieldProperties.ImageIndex = ((int)(resources.GetObject("btnFieldProperties.ImageIndex")));
			this.btnFieldProperties.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnFieldProperties.ImeMode")));
			this.btnFieldProperties.Location = ((System.Drawing.Point)(resources.GetObject("btnFieldProperties.Location")));
			this.btnFieldProperties.Name = "btnFieldProperties";
			this.btnFieldProperties.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnFieldProperties.RightToLeft")));
			this.btnFieldProperties.Size = ((System.Drawing.Size)(resources.GetObject("btnFieldProperties.Size")));
			this.btnFieldProperties.TabIndex = ((int)(resources.GetObject("btnFieldProperties.TabIndex")));
			this.btnFieldProperties.Text = resources.GetString("btnFieldProperties.Text");
			this.btnFieldProperties.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnFieldProperties.TextAlign")));
			this.btnFieldProperties.Visible = ((bool)(resources.GetObject("btnFieldProperties.Visible")));
			this.btnFieldProperties.Click += new System.EventHandler(this.btnFieldProperties_Click);
			// 
			// txtSignatures
			// 
			this.txtSignatures.AccessibleDescription = resources.GetString("txtSignatures.AccessibleDescription");
			this.txtSignatures.AccessibleName = resources.GetString("txtSignatures.AccessibleName");
			this.txtSignatures.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtSignatures.Anchor")));
			this.txtSignatures.AutoSize = ((bool)(resources.GetObject("txtSignatures.AutoSize")));
			this.txtSignatures.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtSignatures.BackgroundImage")));
			this.txtSignatures.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtSignatures.Dock")));
			this.txtSignatures.Enabled = ((bool)(resources.GetObject("txtSignatures.Enabled")));
			this.txtSignatures.Font = ((System.Drawing.Font)(resources.GetObject("txtSignatures.Font")));
			this.txtSignatures.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtSignatures.ImeMode")));
			this.txtSignatures.Location = ((System.Drawing.Point)(resources.GetObject("txtSignatures.Location")));
			this.txtSignatures.MaxLength = ((int)(resources.GetObject("txtSignatures.MaxLength")));
			this.txtSignatures.Multiline = ((bool)(resources.GetObject("txtSignatures.Multiline")));
			this.txtSignatures.Name = "txtSignatures";
			this.txtSignatures.PasswordChar = ((char)(resources.GetObject("txtSignatures.PasswordChar")));
			this.txtSignatures.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtSignatures.RightToLeft")));
			this.txtSignatures.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtSignatures.ScrollBars")));
			this.txtSignatures.Size = ((System.Drawing.Size)(resources.GetObject("txtSignatures.Size")));
			this.txtSignatures.TabIndex = ((int)(resources.GetObject("txtSignatures.TabIndex")));
			this.txtSignatures.Text = resources.GetString("txtSignatures.Text");
			this.txtSignatures.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtSignatures.TextAlign")));
			this.txtSignatures.Visible = ((bool)(resources.GetObject("txtSignatures.Visible")));
			this.txtSignatures.WordWrap = ((bool)(resources.GetObject("txtSignatures.WordWrap")));
			this.txtSignatures.TextChanged += new System.EventHandler(this.txtSignatures_TextChanged);
			// 
			// lblSignatures
			// 
			this.lblSignatures.AccessibleDescription = resources.GetString("lblSignatures.AccessibleDescription");
			this.lblSignatures.AccessibleName = resources.GetString("lblSignatures.AccessibleName");
			this.lblSignatures.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblSignatures.Anchor")));
			this.lblSignatures.AutoSize = ((bool)(resources.GetObject("lblSignatures.AutoSize")));
			this.lblSignatures.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblSignatures.Dock")));
			this.lblSignatures.Enabled = ((bool)(resources.GetObject("lblSignatures.Enabled")));
			this.lblSignatures.Font = ((System.Drawing.Font)(resources.GetObject("lblSignatures.Font")));
			this.lblSignatures.Image = ((System.Drawing.Image)(resources.GetObject("lblSignatures.Image")));
			this.lblSignatures.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblSignatures.ImageAlign")));
			this.lblSignatures.ImageIndex = ((int)(resources.GetObject("lblSignatures.ImageIndex")));
			this.lblSignatures.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblSignatures.ImeMode")));
			this.lblSignatures.Location = ((System.Drawing.Point)(resources.GetObject("lblSignatures.Location")));
			this.lblSignatures.Name = "lblSignatures";
			this.lblSignatures.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblSignatures.RightToLeft")));
			this.lblSignatures.Size = ((System.Drawing.Size)(resources.GetObject("lblSignatures.Size")));
			this.lblSignatures.TabIndex = ((int)(resources.GetObject("lblSignatures.TabIndex")));
			this.lblSignatures.Text = resources.GetString("lblSignatures.Text");
			this.lblSignatures.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblSignatures.TextAlign")));
			this.lblSignatures.Visible = ((bool)(resources.GetObject("lblSignatures.Visible")));
			// 
			// cboTable
			// 
			this.cboTable.AccessibleDescription = resources.GetString("cboTable.AccessibleDescription");
			this.cboTable.AccessibleName = resources.GetString("cboTable.AccessibleName");
			this.cboTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboTable.Anchor")));
			this.cboTable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboTable.BackgroundImage")));
			this.cboTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboTable.Dock")));
			this.cboTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTable.Enabled = ((bool)(resources.GetObject("cboTable.Enabled")));
			this.cboTable.Font = ((System.Drawing.Font)(resources.GetObject("cboTable.Font")));
			this.cboTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboTable.ImeMode")));
			this.cboTable.IntegralHeight = ((bool)(resources.GetObject("cboTable.IntegralHeight")));
			this.cboTable.ItemHeight = ((int)(resources.GetObject("cboTable.ItemHeight")));
			this.cboTable.Items.AddRange(new object[] {
														  resources.GetString("cboTable.Items"),
														  resources.GetString("cboTable.Items1"),
														  resources.GetString("cboTable.Items2"),
														  resources.GetString("cboTable.Items3")});
			this.cboTable.Location = ((System.Drawing.Point)(resources.GetObject("cboTable.Location")));
			this.cboTable.MaxDropDownItems = ((int)(resources.GetObject("cboTable.MaxDropDownItems")));
			this.cboTable.MaxLength = ((int)(resources.GetObject("cboTable.MaxLength")));
			this.cboTable.Name = "cboTable";
			this.cboTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboTable.RightToLeft")));
			this.cboTable.Size = ((System.Drawing.Size)(resources.GetObject("cboTable.Size")));
			this.cboTable.TabIndex = ((int)(resources.GetObject("cboTable.TabIndex")));
			this.cboTable.Text = resources.GetString("cboTable.Text");
			this.cboTable.Visible = ((bool)(resources.GetObject("cboTable.Visible")));
			this.cboTable.SelectedIndexChanged += new System.EventHandler(this.cboGutterPosition_SelectedIndexChanged);
			// 
			// lblTable
			// 
			this.lblTable.AccessibleDescription = resources.GetString("lblTable.AccessibleDescription");
			this.lblTable.AccessibleName = resources.GetString("lblTable.AccessibleName");
			this.lblTable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTable.Anchor")));
			this.lblTable.AutoSize = ((bool)(resources.GetObject("lblTable.AutoSize")));
			this.lblTable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTable.Dock")));
			this.lblTable.Enabled = ((bool)(resources.GetObject("lblTable.Enabled")));
			this.lblTable.Font = ((System.Drawing.Font)(resources.GetObject("lblTable.Font")));
			this.lblTable.Image = ((System.Drawing.Image)(resources.GetObject("lblTable.Image")));
			this.lblTable.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTable.ImageAlign")));
			this.lblTable.ImageIndex = ((int)(resources.GetObject("lblTable.ImageIndex")));
			this.lblTable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTable.ImeMode")));
			this.lblTable.Location = ((System.Drawing.Point)(resources.GetObject("lblTable.Location")));
			this.lblTable.Name = "lblTable";
			this.lblTable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTable.RightToLeft")));
			this.lblTable.Size = ((System.Drawing.Size)(resources.GetObject("lblTable.Size")));
			this.lblTable.TabIndex = ((int)(resources.GetObject("lblTable.TabIndex")));
			this.lblTable.Text = resources.GetString("lblTable.Text");
			this.lblTable.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTable.TextAlign")));
			this.lblTable.Visible = ((bool)(resources.GetObject("lblTable.Visible")));
			// 
			// cboPaperSize
			// 
			this.cboPaperSize.AccessibleDescription = resources.GetString("cboPaperSize.AccessibleDescription");
			this.cboPaperSize.AccessibleName = resources.GetString("cboPaperSize.AccessibleName");
			this.cboPaperSize.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboPaperSize.Anchor")));
			this.cboPaperSize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboPaperSize.BackgroundImage")));
			this.cboPaperSize.DisplayMember = "DisplayMember";
			this.cboPaperSize.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboPaperSize.Dock")));
			this.cboPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPaperSize.Enabled = ((bool)(resources.GetObject("cboPaperSize.Enabled")));
			this.cboPaperSize.Font = ((System.Drawing.Font)(resources.GetObject("cboPaperSize.Font")));
			this.cboPaperSize.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboPaperSize.ImeMode")));
			this.cboPaperSize.IntegralHeight = ((bool)(resources.GetObject("cboPaperSize.IntegralHeight")));
			this.cboPaperSize.ItemHeight = ((int)(resources.GetObject("cboPaperSize.ItemHeight")));
			this.cboPaperSize.Location = ((System.Drawing.Point)(resources.GetObject("cboPaperSize.Location")));
			this.cboPaperSize.MaxDropDownItems = ((int)(resources.GetObject("cboPaperSize.MaxDropDownItems")));
			this.cboPaperSize.MaxLength = ((int)(resources.GetObject("cboPaperSize.MaxLength")));
			this.cboPaperSize.Name = "cboPaperSize";
			this.cboPaperSize.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboPaperSize.RightToLeft")));
			this.cboPaperSize.Size = ((System.Drawing.Size)(resources.GetObject("cboPaperSize.Size")));
			this.cboPaperSize.TabIndex = ((int)(resources.GetObject("cboPaperSize.TabIndex")));
			this.cboPaperSize.Text = resources.GetString("cboPaperSize.Text");
			this.cboPaperSize.ValueMember = "ValueMember";
			this.cboPaperSize.Visible = ((bool)(resources.GetObject("cboPaperSize.Visible")));
			this.cboPaperSize.SelectedIndexChanged += new System.EventHandler(this.cboGutterPosition_SelectedIndexChanged);
			// 
			// lblPaperSize
			// 
			this.lblPaperSize.AccessibleDescription = resources.GetString("lblPaperSize.AccessibleDescription");
			this.lblPaperSize.AccessibleName = resources.GetString("lblPaperSize.AccessibleName");
			this.lblPaperSize.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPaperSize.Anchor")));
			this.lblPaperSize.AutoSize = ((bool)(resources.GetObject("lblPaperSize.AutoSize")));
			this.lblPaperSize.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPaperSize.Dock")));
			this.lblPaperSize.Enabled = ((bool)(resources.GetObject("lblPaperSize.Enabled")));
			this.lblPaperSize.Font = ((System.Drawing.Font)(resources.GetObject("lblPaperSize.Font")));
			this.lblPaperSize.Image = ((System.Drawing.Image)(resources.GetObject("lblPaperSize.Image")));
			this.lblPaperSize.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPaperSize.ImageAlign")));
			this.lblPaperSize.ImageIndex = ((int)(resources.GetObject("lblPaperSize.ImageIndex")));
			this.lblPaperSize.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPaperSize.ImeMode")));
			this.lblPaperSize.Location = ((System.Drawing.Point)(resources.GetObject("lblPaperSize.Location")));
			this.lblPaperSize.Name = "lblPaperSize";
			this.lblPaperSize.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPaperSize.RightToLeft")));
			this.lblPaperSize.Size = ((System.Drawing.Size)(resources.GetObject("lblPaperSize.Size")));
			this.lblPaperSize.TabIndex = ((int)(resources.GetObject("lblPaperSize.TabIndex")));
			this.lblPaperSize.Text = resources.GetString("lblPaperSize.Text");
			this.lblPaperSize.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPaperSize.TextAlign")));
			this.lblPaperSize.Visible = ((bool)(resources.GetObject("lblPaperSize.Visible")));
			// 
			// cboOrientation
			// 
			this.cboOrientation.AccessibleDescription = resources.GetString("cboOrientation.AccessibleDescription");
			this.cboOrientation.AccessibleName = resources.GetString("cboOrientation.AccessibleName");
			this.cboOrientation.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboOrientation.Anchor")));
			this.cboOrientation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboOrientation.BackgroundImage")));
			this.cboOrientation.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboOrientation.Dock")));
			this.cboOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOrientation.Enabled = ((bool)(resources.GetObject("cboOrientation.Enabled")));
			this.cboOrientation.Font = ((System.Drawing.Font)(resources.GetObject("cboOrientation.Font")));
			this.cboOrientation.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboOrientation.ImeMode")));
			this.cboOrientation.IntegralHeight = ((bool)(resources.GetObject("cboOrientation.IntegralHeight")));
			this.cboOrientation.ItemHeight = ((int)(resources.GetObject("cboOrientation.ItemHeight")));
			this.cboOrientation.Items.AddRange(new object[] {
																resources.GetString("cboOrientation.Items"),
																resources.GetString("cboOrientation.Items1")});
			this.cboOrientation.Location = ((System.Drawing.Point)(resources.GetObject("cboOrientation.Location")));
			this.cboOrientation.MaxDropDownItems = ((int)(resources.GetObject("cboOrientation.MaxDropDownItems")));
			this.cboOrientation.MaxLength = ((int)(resources.GetObject("cboOrientation.MaxLength")));
			this.cboOrientation.Name = "cboOrientation";
			this.cboOrientation.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboOrientation.RightToLeft")));
			this.cboOrientation.Size = ((System.Drawing.Size)(resources.GetObject("cboOrientation.Size")));
			this.cboOrientation.TabIndex = ((int)(resources.GetObject("cboOrientation.TabIndex")));
			this.cboOrientation.Text = resources.GetString("cboOrientation.Text");
			this.cboOrientation.Visible = ((bool)(resources.GetObject("cboOrientation.Visible")));
			this.cboOrientation.SelectedIndexChanged += new System.EventHandler(this.cboGutterPosition_SelectedIndexChanged);
			// 
			// lblOrientation
			// 
			this.lblOrientation.AccessibleDescription = resources.GetString("lblOrientation.AccessibleDescription");
			this.lblOrientation.AccessibleName = resources.GetString("lblOrientation.AccessibleName");
			this.lblOrientation.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblOrientation.Anchor")));
			this.lblOrientation.AutoSize = ((bool)(resources.GetObject("lblOrientation.AutoSize")));
			this.lblOrientation.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblOrientation.Dock")));
			this.lblOrientation.Enabled = ((bool)(resources.GetObject("lblOrientation.Enabled")));
			this.lblOrientation.Font = ((System.Drawing.Font)(resources.GetObject("lblOrientation.Font")));
			this.lblOrientation.Image = ((System.Drawing.Image)(resources.GetObject("lblOrientation.Image")));
			this.lblOrientation.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblOrientation.ImageAlign")));
			this.lblOrientation.ImageIndex = ((int)(resources.GetObject("lblOrientation.ImageIndex")));
			this.lblOrientation.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblOrientation.ImeMode")));
			this.lblOrientation.Location = ((System.Drawing.Point)(resources.GetObject("lblOrientation.Location")));
			this.lblOrientation.Name = "lblOrientation";
			this.lblOrientation.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblOrientation.RightToLeft")));
			this.lblOrientation.Size = ((System.Drawing.Size)(resources.GetObject("lblOrientation.Size")));
			this.lblOrientation.TabIndex = ((int)(resources.GetObject("lblOrientation.TabIndex")));
			this.lblOrientation.Text = resources.GetString("lblOrientation.Text");
			this.lblOrientation.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblOrientation.TextAlign")));
			this.lblOrientation.Visible = ((bool)(resources.GetObject("lblOrientation.Visible")));
			// 
			// grpMargins
			// 
			this.grpMargins.AccessibleDescription = resources.GetString("grpMargins.AccessibleDescription");
			this.grpMargins.AccessibleName = resources.GetString("grpMargins.AccessibleName");
			this.grpMargins.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpMargins.Anchor")));
			this.grpMargins.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpMargins.BackgroundImage")));
			this.grpMargins.Controls.Add(this.nudMarginBottom);
			this.grpMargins.Controls.Add(this.nudMarginRight);
			this.grpMargins.Controls.Add(this.nudMarginGutter);
			this.grpMargins.Controls.Add(this.nudMarginLeft);
			this.grpMargins.Controls.Add(this.cboGutterPosition);
			this.grpMargins.Controls.Add(this.lblGutterPosition);
			this.grpMargins.Controls.Add(this.lblGutter);
			this.grpMargins.Controls.Add(this.lblBottom);
			this.grpMargins.Controls.Add(this.lblLeft);
			this.grpMargins.Controls.Add(this.lblRight);
			this.grpMargins.Controls.Add(this.lblTop);
			this.grpMargins.Controls.Add(this.nudMarginTop);
			this.grpMargins.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpMargins.Dock")));
			this.grpMargins.Enabled = ((bool)(resources.GetObject("grpMargins.Enabled")));
			this.grpMargins.Font = ((System.Drawing.Font)(resources.GetObject("grpMargins.Font")));
			this.grpMargins.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpMargins.ImeMode")));
			this.grpMargins.Location = ((System.Drawing.Point)(resources.GetObject("grpMargins.Location")));
			this.grpMargins.Name = "grpMargins";
			this.grpMargins.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpMargins.RightToLeft")));
			this.grpMargins.Size = ((System.Drawing.Size)(resources.GetObject("grpMargins.Size")));
			this.grpMargins.TabIndex = ((int)(resources.GetObject("grpMargins.TabIndex")));
			this.grpMargins.TabStop = false;
			this.grpMargins.Text = resources.GetString("grpMargins.Text");
			this.grpMargins.Visible = ((bool)(resources.GetObject("grpMargins.Visible")));
			// 
			// nudMarginBottom
			// 
			this.nudMarginBottom.AcceptsEscape = ((bool)(resources.GetObject("nudMarginBottom.AcceptsEscape")));
			this.nudMarginBottom.AccessibleDescription = resources.GetString("nudMarginBottom.AccessibleDescription");
			this.nudMarginBottom.AccessibleName = resources.GetString("nudMarginBottom.AccessibleName");
			this.nudMarginBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("nudMarginBottom.Anchor")));
			this.nudMarginBottom.AutoSize = ((bool)(resources.GetObject("nudMarginBottom.AutoSize")));
			this.nudMarginBottom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nudMarginBottom.BackgroundImage")));
			this.nudMarginBottom.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("nudMarginBottom.BorderStyle")));
			// 
			// nudMarginBottom.Calculator
			// 
			this.nudMarginBottom.Calculator.AccessibleDescription = resources.GetString("nudMarginBottom.Calculator.AccessibleDescription");
			this.nudMarginBottom.Calculator.AccessibleName = resources.GetString("nudMarginBottom.Calculator.AccessibleName");
			this.nudMarginBottom.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nudMarginBottom.Calculator.BackgroundImage")));
			this.nudMarginBottom.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("nudMarginBottom.Calculator.ButtonFlatStyle")));
			this.nudMarginBottom.Calculator.DisplayFormat = resources.GetString("nudMarginBottom.Calculator.DisplayFormat");
			this.nudMarginBottom.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("nudMarginBottom.Calculator.Font")));
			this.nudMarginBottom.Calculator.FormatOnClose = ((bool)(resources.GetObject("nudMarginBottom.Calculator.FormatOnClose")));
			this.nudMarginBottom.Calculator.StoredFormat = resources.GetString("nudMarginBottom.Calculator.StoredFormat");
			this.nudMarginBottom.Calculator.UIStrings.Content = ((string[])(resources.GetObject("nudMarginBottom.Calculator.UIStrings.Content")));
			this.nudMarginBottom.CaseSensitive = ((bool)(resources.GetObject("nudMarginBottom.CaseSensitive")));
			this.nudMarginBottom.Culture = ((int)(resources.GetObject("nudMarginBottom.Culture")));
			this.nudMarginBottom.CustomFormat = resources.GetString("nudMarginBottom.CustomFormat");
			this.nudMarginBottom.DataType = ((System.Type)(resources.GetObject("nudMarginBottom.DataType")));
			this.nudMarginBottom.DisplayFormat.CustomFormat = resources.GetString("nudMarginBottom.DisplayFormat.CustomFormat");
			this.nudMarginBottom.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginBottom.DisplayFormat.FormatType")));
			this.nudMarginBottom.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("nudMarginBottom.DisplayFormat.Inherit")));
			this.nudMarginBottom.DisplayFormat.NullText = resources.GetString("nudMarginBottom.DisplayFormat.NullText");
			this.nudMarginBottom.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("nudMarginBottom.DisplayFormat.TrimEnd")));
			this.nudMarginBottom.DisplayFormat.TrimStart = ((bool)(resources.GetObject("nudMarginBottom.DisplayFormat.TrimStart")));
			this.nudMarginBottom.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("nudMarginBottom.Dock")));
			this.nudMarginBottom.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("nudMarginBottom.DropDownFormAlign")));
			this.nudMarginBottom.EditFormat.CustomFormat = resources.GetString("nudMarginBottom.EditFormat.CustomFormat");
			this.nudMarginBottom.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginBottom.EditFormat.FormatType")));
			this.nudMarginBottom.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("nudMarginBottom.EditFormat.Inherit")));
			this.nudMarginBottom.EditFormat.NullText = resources.GetString("nudMarginBottom.EditFormat.NullText");
			this.nudMarginBottom.EditFormat.TrimEnd = ((bool)(resources.GetObject("nudMarginBottom.EditFormat.TrimEnd")));
			this.nudMarginBottom.EditFormat.TrimStart = ((bool)(resources.GetObject("nudMarginBottom.EditFormat.TrimStart")));
			this.nudMarginBottom.EditMask = resources.GetString("nudMarginBottom.EditMask");
			this.nudMarginBottom.EmptyAsNull = ((bool)(resources.GetObject("nudMarginBottom.EmptyAsNull")));
			this.nudMarginBottom.Enabled = ((bool)(resources.GetObject("nudMarginBottom.Enabled")));
			this.nudMarginBottom.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("nudMarginBottom.ErrorInfo.BeepOnError")));
			this.nudMarginBottom.ErrorInfo.ErrorMessage = resources.GetString("nudMarginBottom.ErrorInfo.ErrorMessage");
			this.nudMarginBottom.ErrorInfo.ErrorMessageCaption = resources.GetString("nudMarginBottom.ErrorInfo.ErrorMessageCaption");
			this.nudMarginBottom.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("nudMarginBottom.ErrorInfo.ShowErrorMessage")));
			this.nudMarginBottom.ErrorInfo.ValueOnError = ((object)(resources.GetObject("nudMarginBottom.ErrorInfo.ValueOnError")));
			this.nudMarginBottom.Font = ((System.Drawing.Font)(resources.GetObject("nudMarginBottom.Font")));
			this.nudMarginBottom.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginBottom.FormatType")));
			this.nudMarginBottom.GapHeight = ((int)(resources.GetObject("nudMarginBottom.GapHeight")));
			this.nudMarginBottom.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("nudMarginBottom.ImeMode")));
			this.nudMarginBottom.Increment = ((object)(resources.GetObject("nudMarginBottom.Increment")));
			this.nudMarginBottom.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("nudMarginBottom.InitialSelection")));
			this.nudMarginBottom.Location = ((System.Drawing.Point)(resources.GetObject("nudMarginBottom.Location")));
			this.nudMarginBottom.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("nudMarginBottom.MaskInfo.AutoTabWhenFilled")));
			this.nudMarginBottom.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("nudMarginBottom.MaskInfo.CaseSensitive")));
			this.nudMarginBottom.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("nudMarginBottom.MaskInfo.CopyWithLiterals")));
			this.nudMarginBottom.MaskInfo.EditMask = resources.GetString("nudMarginBottom.MaskInfo.EditMask");
			this.nudMarginBottom.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("nudMarginBottom.MaskInfo.EmptyAsNull")));
			this.nudMarginBottom.MaskInfo.ErrorMessage = resources.GetString("nudMarginBottom.MaskInfo.ErrorMessage");
			this.nudMarginBottom.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("nudMarginBottom.MaskInfo.Inherit")));
			this.nudMarginBottom.MaskInfo.PromptChar = ((char)(resources.GetObject("nudMarginBottom.MaskInfo.PromptChar")));
			this.nudMarginBottom.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("nudMarginBottom.MaskInfo.ShowLiterals")));
			this.nudMarginBottom.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("nudMarginBottom.MaskInfo.StoredEmptyChar")));
			this.nudMarginBottom.MaxLength = ((int)(resources.GetObject("nudMarginBottom.MaxLength")));
			this.nudMarginBottom.Name = "nudMarginBottom";
			this.nudMarginBottom.NullText = resources.GetString("nudMarginBottom.NullText");
			this.nudMarginBottom.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("nudMarginBottom.ParseInfo.CaseSensitive")));
			this.nudMarginBottom.ParseInfo.CustomFormat = resources.GetString("nudMarginBottom.ParseInfo.CustomFormat");
			this.nudMarginBottom.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("nudMarginBottom.ParseInfo.EmptyAsNull")));
			this.nudMarginBottom.ParseInfo.ErrorMessage = resources.GetString("nudMarginBottom.ParseInfo.ErrorMessage");
			this.nudMarginBottom.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginBottom.ParseInfo.FormatType")));
			this.nudMarginBottom.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("nudMarginBottom.ParseInfo.Inherit")));
			this.nudMarginBottom.ParseInfo.NullText = resources.GetString("nudMarginBottom.ParseInfo.NullText");
			this.nudMarginBottom.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("nudMarginBottom.ParseInfo.NumberStyle")));
			this.nudMarginBottom.ParseInfo.TrimEnd = ((bool)(resources.GetObject("nudMarginBottom.ParseInfo.TrimEnd")));
			this.nudMarginBottom.ParseInfo.TrimStart = ((bool)(resources.GetObject("nudMarginBottom.ParseInfo.TrimStart")));
			this.nudMarginBottom.PasswordChar = ((char)(resources.GetObject("nudMarginBottom.PasswordChar")));
			this.nudMarginBottom.PostValidation.CaseSensitive = ((bool)(resources.GetObject("nudMarginBottom.PostValidation.CaseSensitive")));
			this.nudMarginBottom.PostValidation.ErrorMessage = resources.GetString("nudMarginBottom.PostValidation.ErrorMessage");
			this.nudMarginBottom.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("nudMarginBottom.PostValidation.Inherit")));
			this.nudMarginBottom.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																										  ((C1.Win.C1Input.ValueInterval)(resources.GetObject("nudMarginBottom.PostValidation.Intervals")))});
			this.nudMarginBottom.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("nudMarginBottom.PostValidation.Validation")));
			this.nudMarginBottom.PostValidation.Values = ((System.Array)(resources.GetObject("nudMarginBottom.PostValidation.Values")));
			this.nudMarginBottom.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("nudMarginBottom.PostValidation.ValuesExcluded")));
			this.nudMarginBottom.PreValidation.CaseSensitive = ((bool)(resources.GetObject("nudMarginBottom.PreValidation.CaseSensitive")));
			this.nudMarginBottom.PreValidation.ErrorMessage = resources.GetString("nudMarginBottom.PreValidation.ErrorMessage");
			this.nudMarginBottom.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("nudMarginBottom.PreValidation.Inherit")));
			this.nudMarginBottom.PreValidation.ItemSeparator = resources.GetString("nudMarginBottom.PreValidation.ItemSeparator");
			this.nudMarginBottom.PreValidation.PatternString = resources.GetString("nudMarginBottom.PreValidation.PatternString");
			this.nudMarginBottom.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("nudMarginBottom.PreValidation.RegexOptions")));
			this.nudMarginBottom.PreValidation.TrimEnd = ((bool)(resources.GetObject("nudMarginBottom.PreValidation.TrimEnd")));
			this.nudMarginBottom.PreValidation.TrimStart = ((bool)(resources.GetObject("nudMarginBottom.PreValidation.TrimStart")));
			this.nudMarginBottom.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("nudMarginBottom.PreValidation.Validation")));
			this.nudMarginBottom.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("nudMarginBottom.RightToLeft")));
			this.nudMarginBottom.ShowFocusRectangle = ((bool)(resources.GetObject("nudMarginBottom.ShowFocusRectangle")));
			this.nudMarginBottom.Size = ((System.Drawing.Size)(resources.GetObject("nudMarginBottom.Size")));
			this.nudMarginBottom.TabIndex = ((int)(resources.GetObject("nudMarginBottom.TabIndex")));
			this.nudMarginBottom.Tag = ((object)(resources.GetObject("nudMarginBottom.Tag")));
			this.nudMarginBottom.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("nudMarginBottom.TextAlign")));
			this.nudMarginBottom.TrimEnd = ((bool)(resources.GetObject("nudMarginBottom.TrimEnd")));
			this.nudMarginBottom.TrimStart = ((bool)(resources.GetObject("nudMarginBottom.TrimStart")));
			this.nudMarginBottom.UserCultureOverride = ((bool)(resources.GetObject("nudMarginBottom.UserCultureOverride")));
			this.nudMarginBottom.Value = ((object)(resources.GetObject("nudMarginBottom.Value")));
			this.nudMarginBottom.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("nudMarginBottom.VerticalAlign")));
			this.nudMarginBottom.Visible = ((bool)(resources.GetObject("nudMarginBottom.Visible")));
			this.nudMarginBottom.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("nudMarginBottom.VisibleButtons")));
			this.nudMarginBottom.ValueChanged += new System.EventHandler(this.nudMarginTop_ValueChanged);
			// 
			// nudMarginRight
			// 
			this.nudMarginRight.AcceptsEscape = ((bool)(resources.GetObject("nudMarginRight.AcceptsEscape")));
			this.nudMarginRight.AccessibleDescription = resources.GetString("nudMarginRight.AccessibleDescription");
			this.nudMarginRight.AccessibleName = resources.GetString("nudMarginRight.AccessibleName");
			this.nudMarginRight.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("nudMarginRight.Anchor")));
			this.nudMarginRight.AutoSize = ((bool)(resources.GetObject("nudMarginRight.AutoSize")));
			this.nudMarginRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nudMarginRight.BackgroundImage")));
			this.nudMarginRight.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("nudMarginRight.BorderStyle")));
			// 
			// nudMarginRight.Calculator
			// 
			this.nudMarginRight.Calculator.AccessibleDescription = resources.GetString("nudMarginRight.Calculator.AccessibleDescription");
			this.nudMarginRight.Calculator.AccessibleName = resources.GetString("nudMarginRight.Calculator.AccessibleName");
			this.nudMarginRight.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nudMarginRight.Calculator.BackgroundImage")));
			this.nudMarginRight.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("nudMarginRight.Calculator.ButtonFlatStyle")));
			this.nudMarginRight.Calculator.DisplayFormat = resources.GetString("nudMarginRight.Calculator.DisplayFormat");
			this.nudMarginRight.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("nudMarginRight.Calculator.Font")));
			this.nudMarginRight.Calculator.FormatOnClose = ((bool)(resources.GetObject("nudMarginRight.Calculator.FormatOnClose")));
			this.nudMarginRight.Calculator.StoredFormat = resources.GetString("nudMarginRight.Calculator.StoredFormat");
			this.nudMarginRight.Calculator.UIStrings.Content = ((string[])(resources.GetObject("nudMarginRight.Calculator.UIStrings.Content")));
			this.nudMarginRight.CaseSensitive = ((bool)(resources.GetObject("nudMarginRight.CaseSensitive")));
			this.nudMarginRight.Culture = ((int)(resources.GetObject("nudMarginRight.Culture")));
			this.nudMarginRight.CustomFormat = resources.GetString("nudMarginRight.CustomFormat");
			this.nudMarginRight.DataType = ((System.Type)(resources.GetObject("nudMarginRight.DataType")));
			this.nudMarginRight.DisplayFormat.CustomFormat = resources.GetString("nudMarginRight.DisplayFormat.CustomFormat");
			this.nudMarginRight.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginRight.DisplayFormat.FormatType")));
			this.nudMarginRight.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("nudMarginRight.DisplayFormat.Inherit")));
			this.nudMarginRight.DisplayFormat.NullText = resources.GetString("nudMarginRight.DisplayFormat.NullText");
			this.nudMarginRight.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("nudMarginRight.DisplayFormat.TrimEnd")));
			this.nudMarginRight.DisplayFormat.TrimStart = ((bool)(resources.GetObject("nudMarginRight.DisplayFormat.TrimStart")));
			this.nudMarginRight.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("nudMarginRight.Dock")));
			this.nudMarginRight.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("nudMarginRight.DropDownFormAlign")));
			this.nudMarginRight.EditFormat.CustomFormat = resources.GetString("nudMarginRight.EditFormat.CustomFormat");
			this.nudMarginRight.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginRight.EditFormat.FormatType")));
			this.nudMarginRight.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("nudMarginRight.EditFormat.Inherit")));
			this.nudMarginRight.EditFormat.NullText = resources.GetString("nudMarginRight.EditFormat.NullText");
			this.nudMarginRight.EditFormat.TrimEnd = ((bool)(resources.GetObject("nudMarginRight.EditFormat.TrimEnd")));
			this.nudMarginRight.EditFormat.TrimStart = ((bool)(resources.GetObject("nudMarginRight.EditFormat.TrimStart")));
			this.nudMarginRight.EditMask = resources.GetString("nudMarginRight.EditMask");
			this.nudMarginRight.EmptyAsNull = ((bool)(resources.GetObject("nudMarginRight.EmptyAsNull")));
			this.nudMarginRight.Enabled = ((bool)(resources.GetObject("nudMarginRight.Enabled")));
			this.nudMarginRight.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("nudMarginRight.ErrorInfo.BeepOnError")));
			this.nudMarginRight.ErrorInfo.ErrorMessage = resources.GetString("nudMarginRight.ErrorInfo.ErrorMessage");
			this.nudMarginRight.ErrorInfo.ErrorMessageCaption = resources.GetString("nudMarginRight.ErrorInfo.ErrorMessageCaption");
			this.nudMarginRight.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("nudMarginRight.ErrorInfo.ShowErrorMessage")));
			this.nudMarginRight.ErrorInfo.ValueOnError = ((object)(resources.GetObject("nudMarginRight.ErrorInfo.ValueOnError")));
			this.nudMarginRight.Font = ((System.Drawing.Font)(resources.GetObject("nudMarginRight.Font")));
			this.nudMarginRight.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginRight.FormatType")));
			this.nudMarginRight.GapHeight = ((int)(resources.GetObject("nudMarginRight.GapHeight")));
			this.nudMarginRight.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("nudMarginRight.ImeMode")));
			this.nudMarginRight.Increment = ((object)(resources.GetObject("nudMarginRight.Increment")));
			this.nudMarginRight.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("nudMarginRight.InitialSelection")));
			this.nudMarginRight.Location = ((System.Drawing.Point)(resources.GetObject("nudMarginRight.Location")));
			this.nudMarginRight.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("nudMarginRight.MaskInfo.AutoTabWhenFilled")));
			this.nudMarginRight.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("nudMarginRight.MaskInfo.CaseSensitive")));
			this.nudMarginRight.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("nudMarginRight.MaskInfo.CopyWithLiterals")));
			this.nudMarginRight.MaskInfo.EditMask = resources.GetString("nudMarginRight.MaskInfo.EditMask");
			this.nudMarginRight.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("nudMarginRight.MaskInfo.EmptyAsNull")));
			this.nudMarginRight.MaskInfo.ErrorMessage = resources.GetString("nudMarginRight.MaskInfo.ErrorMessage");
			this.nudMarginRight.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("nudMarginRight.MaskInfo.Inherit")));
			this.nudMarginRight.MaskInfo.PromptChar = ((char)(resources.GetObject("nudMarginRight.MaskInfo.PromptChar")));
			this.nudMarginRight.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("nudMarginRight.MaskInfo.ShowLiterals")));
			this.nudMarginRight.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("nudMarginRight.MaskInfo.StoredEmptyChar")));
			this.nudMarginRight.MaxLength = ((int)(resources.GetObject("nudMarginRight.MaxLength")));
			this.nudMarginRight.Name = "nudMarginRight";
			this.nudMarginRight.NullText = resources.GetString("nudMarginRight.NullText");
			this.nudMarginRight.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("nudMarginRight.ParseInfo.CaseSensitive")));
			this.nudMarginRight.ParseInfo.CustomFormat = resources.GetString("nudMarginRight.ParseInfo.CustomFormat");
			this.nudMarginRight.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("nudMarginRight.ParseInfo.EmptyAsNull")));
			this.nudMarginRight.ParseInfo.ErrorMessage = resources.GetString("nudMarginRight.ParseInfo.ErrorMessage");
			this.nudMarginRight.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginRight.ParseInfo.FormatType")));
			this.nudMarginRight.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("nudMarginRight.ParseInfo.Inherit")));
			this.nudMarginRight.ParseInfo.NullText = resources.GetString("nudMarginRight.ParseInfo.NullText");
			this.nudMarginRight.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("nudMarginRight.ParseInfo.NumberStyle")));
			this.nudMarginRight.ParseInfo.TrimEnd = ((bool)(resources.GetObject("nudMarginRight.ParseInfo.TrimEnd")));
			this.nudMarginRight.ParseInfo.TrimStart = ((bool)(resources.GetObject("nudMarginRight.ParseInfo.TrimStart")));
			this.nudMarginRight.PasswordChar = ((char)(resources.GetObject("nudMarginRight.PasswordChar")));
			this.nudMarginRight.PostValidation.CaseSensitive = ((bool)(resources.GetObject("nudMarginRight.PostValidation.CaseSensitive")));
			this.nudMarginRight.PostValidation.ErrorMessage = resources.GetString("nudMarginRight.PostValidation.ErrorMessage");
			this.nudMarginRight.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("nudMarginRight.PostValidation.Inherit")));
			this.nudMarginRight.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																										 ((C1.Win.C1Input.ValueInterval)(resources.GetObject("nudMarginRight.PostValidation.Intervals")))});
			this.nudMarginRight.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("nudMarginRight.PostValidation.Validation")));
			this.nudMarginRight.PostValidation.Values = ((System.Array)(resources.GetObject("nudMarginRight.PostValidation.Values")));
			this.nudMarginRight.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("nudMarginRight.PostValidation.ValuesExcluded")));
			this.nudMarginRight.PreValidation.CaseSensitive = ((bool)(resources.GetObject("nudMarginRight.PreValidation.CaseSensitive")));
			this.nudMarginRight.PreValidation.ErrorMessage = resources.GetString("nudMarginRight.PreValidation.ErrorMessage");
			this.nudMarginRight.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("nudMarginRight.PreValidation.Inherit")));
			this.nudMarginRight.PreValidation.ItemSeparator = resources.GetString("nudMarginRight.PreValidation.ItemSeparator");
			this.nudMarginRight.PreValidation.PatternString = resources.GetString("nudMarginRight.PreValidation.PatternString");
			this.nudMarginRight.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("nudMarginRight.PreValidation.RegexOptions")));
			this.nudMarginRight.PreValidation.TrimEnd = ((bool)(resources.GetObject("nudMarginRight.PreValidation.TrimEnd")));
			this.nudMarginRight.PreValidation.TrimStart = ((bool)(resources.GetObject("nudMarginRight.PreValidation.TrimStart")));
			this.nudMarginRight.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("nudMarginRight.PreValidation.Validation")));
			this.nudMarginRight.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("nudMarginRight.RightToLeft")));
			this.nudMarginRight.ShowFocusRectangle = ((bool)(resources.GetObject("nudMarginRight.ShowFocusRectangle")));
			this.nudMarginRight.Size = ((System.Drawing.Size)(resources.GetObject("nudMarginRight.Size")));
			this.nudMarginRight.TabIndex = ((int)(resources.GetObject("nudMarginRight.TabIndex")));
			this.nudMarginRight.Tag = ((object)(resources.GetObject("nudMarginRight.Tag")));
			this.nudMarginRight.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("nudMarginRight.TextAlign")));
			this.nudMarginRight.TrimEnd = ((bool)(resources.GetObject("nudMarginRight.TrimEnd")));
			this.nudMarginRight.TrimStart = ((bool)(resources.GetObject("nudMarginRight.TrimStart")));
			this.nudMarginRight.UserCultureOverride = ((bool)(resources.GetObject("nudMarginRight.UserCultureOverride")));
			this.nudMarginRight.Value = ((object)(resources.GetObject("nudMarginRight.Value")));
			this.nudMarginRight.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("nudMarginRight.VerticalAlign")));
			this.nudMarginRight.Visible = ((bool)(resources.GetObject("nudMarginRight.Visible")));
			this.nudMarginRight.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("nudMarginRight.VisibleButtons")));
			this.nudMarginRight.ValueChanged += new System.EventHandler(this.nudMarginTop_ValueChanged);
			// 
			// nudMarginGutter
			// 
			this.nudMarginGutter.AcceptsEscape = ((bool)(resources.GetObject("nudMarginGutter.AcceptsEscape")));
			this.nudMarginGutter.AccessibleDescription = resources.GetString("nudMarginGutter.AccessibleDescription");
			this.nudMarginGutter.AccessibleName = resources.GetString("nudMarginGutter.AccessibleName");
			this.nudMarginGutter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("nudMarginGutter.Anchor")));
			this.nudMarginGutter.AutoSize = ((bool)(resources.GetObject("nudMarginGutter.AutoSize")));
			this.nudMarginGutter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nudMarginGutter.BackgroundImage")));
			this.nudMarginGutter.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("nudMarginGutter.BorderStyle")));
			// 
			// nudMarginGutter.Calculator
			// 
			this.nudMarginGutter.Calculator.AccessibleDescription = resources.GetString("nudMarginGutter.Calculator.AccessibleDescription");
			this.nudMarginGutter.Calculator.AccessibleName = resources.GetString("nudMarginGutter.Calculator.AccessibleName");
			this.nudMarginGutter.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nudMarginGutter.Calculator.BackgroundImage")));
			this.nudMarginGutter.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("nudMarginGutter.Calculator.ButtonFlatStyle")));
			this.nudMarginGutter.Calculator.DisplayFormat = resources.GetString("nudMarginGutter.Calculator.DisplayFormat");
			this.nudMarginGutter.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("nudMarginGutter.Calculator.Font")));
			this.nudMarginGutter.Calculator.FormatOnClose = ((bool)(resources.GetObject("nudMarginGutter.Calculator.FormatOnClose")));
			this.nudMarginGutter.Calculator.StoredFormat = resources.GetString("nudMarginGutter.Calculator.StoredFormat");
			this.nudMarginGutter.Calculator.UIStrings.Content = ((string[])(resources.GetObject("nudMarginGutter.Calculator.UIStrings.Content")));
			this.nudMarginGutter.CaseSensitive = ((bool)(resources.GetObject("nudMarginGutter.CaseSensitive")));
			this.nudMarginGutter.Culture = ((int)(resources.GetObject("nudMarginGutter.Culture")));
			this.nudMarginGutter.CustomFormat = resources.GetString("nudMarginGutter.CustomFormat");
			this.nudMarginGutter.DataType = ((System.Type)(resources.GetObject("nudMarginGutter.DataType")));
			this.nudMarginGutter.DisplayFormat.CustomFormat = resources.GetString("nudMarginGutter.DisplayFormat.CustomFormat");
			this.nudMarginGutter.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginGutter.DisplayFormat.FormatType")));
			this.nudMarginGutter.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("nudMarginGutter.DisplayFormat.Inherit")));
			this.nudMarginGutter.DisplayFormat.NullText = resources.GetString("nudMarginGutter.DisplayFormat.NullText");
			this.nudMarginGutter.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("nudMarginGutter.DisplayFormat.TrimEnd")));
			this.nudMarginGutter.DisplayFormat.TrimStart = ((bool)(resources.GetObject("nudMarginGutter.DisplayFormat.TrimStart")));
			this.nudMarginGutter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("nudMarginGutter.Dock")));
			this.nudMarginGutter.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("nudMarginGutter.DropDownFormAlign")));
			this.nudMarginGutter.EditFormat.CustomFormat = resources.GetString("nudMarginGutter.EditFormat.CustomFormat");
			this.nudMarginGutter.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginGutter.EditFormat.FormatType")));
			this.nudMarginGutter.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("nudMarginGutter.EditFormat.Inherit")));
			this.nudMarginGutter.EditFormat.NullText = resources.GetString("nudMarginGutter.EditFormat.NullText");
			this.nudMarginGutter.EditFormat.TrimEnd = ((bool)(resources.GetObject("nudMarginGutter.EditFormat.TrimEnd")));
			this.nudMarginGutter.EditFormat.TrimStart = ((bool)(resources.GetObject("nudMarginGutter.EditFormat.TrimStart")));
			this.nudMarginGutter.EditMask = resources.GetString("nudMarginGutter.EditMask");
			this.nudMarginGutter.EmptyAsNull = ((bool)(resources.GetObject("nudMarginGutter.EmptyAsNull")));
			this.nudMarginGutter.Enabled = ((bool)(resources.GetObject("nudMarginGutter.Enabled")));
			this.nudMarginGutter.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("nudMarginGutter.ErrorInfo.BeepOnError")));
			this.nudMarginGutter.ErrorInfo.ErrorMessage = resources.GetString("nudMarginGutter.ErrorInfo.ErrorMessage");
			this.nudMarginGutter.ErrorInfo.ErrorMessageCaption = resources.GetString("nudMarginGutter.ErrorInfo.ErrorMessageCaption");
			this.nudMarginGutter.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("nudMarginGutter.ErrorInfo.ShowErrorMessage")));
			this.nudMarginGutter.ErrorInfo.ValueOnError = ((object)(resources.GetObject("nudMarginGutter.ErrorInfo.ValueOnError")));
			this.nudMarginGutter.Font = ((System.Drawing.Font)(resources.GetObject("nudMarginGutter.Font")));
			this.nudMarginGutter.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginGutter.FormatType")));
			this.nudMarginGutter.GapHeight = ((int)(resources.GetObject("nudMarginGutter.GapHeight")));
			this.nudMarginGutter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("nudMarginGutter.ImeMode")));
			this.nudMarginGutter.Increment = ((object)(resources.GetObject("nudMarginGutter.Increment")));
			this.nudMarginGutter.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("nudMarginGutter.InitialSelection")));
			this.nudMarginGutter.Location = ((System.Drawing.Point)(resources.GetObject("nudMarginGutter.Location")));
			this.nudMarginGutter.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("nudMarginGutter.MaskInfo.AutoTabWhenFilled")));
			this.nudMarginGutter.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("nudMarginGutter.MaskInfo.CaseSensitive")));
			this.nudMarginGutter.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("nudMarginGutter.MaskInfo.CopyWithLiterals")));
			this.nudMarginGutter.MaskInfo.EditMask = resources.GetString("nudMarginGutter.MaskInfo.EditMask");
			this.nudMarginGutter.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("nudMarginGutter.MaskInfo.EmptyAsNull")));
			this.nudMarginGutter.MaskInfo.ErrorMessage = resources.GetString("nudMarginGutter.MaskInfo.ErrorMessage");
			this.nudMarginGutter.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("nudMarginGutter.MaskInfo.Inherit")));
			this.nudMarginGutter.MaskInfo.PromptChar = ((char)(resources.GetObject("nudMarginGutter.MaskInfo.PromptChar")));
			this.nudMarginGutter.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("nudMarginGutter.MaskInfo.ShowLiterals")));
			this.nudMarginGutter.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("nudMarginGutter.MaskInfo.StoredEmptyChar")));
			this.nudMarginGutter.MaxLength = ((int)(resources.GetObject("nudMarginGutter.MaxLength")));
			this.nudMarginGutter.Name = "nudMarginGutter";
			this.nudMarginGutter.NullText = resources.GetString("nudMarginGutter.NullText");
			this.nudMarginGutter.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("nudMarginGutter.ParseInfo.CaseSensitive")));
			this.nudMarginGutter.ParseInfo.CustomFormat = resources.GetString("nudMarginGutter.ParseInfo.CustomFormat");
			this.nudMarginGutter.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("nudMarginGutter.ParseInfo.EmptyAsNull")));
			this.nudMarginGutter.ParseInfo.ErrorMessage = resources.GetString("nudMarginGutter.ParseInfo.ErrorMessage");
			this.nudMarginGutter.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginGutter.ParseInfo.FormatType")));
			this.nudMarginGutter.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("nudMarginGutter.ParseInfo.Inherit")));
			this.nudMarginGutter.ParseInfo.NullText = resources.GetString("nudMarginGutter.ParseInfo.NullText");
			this.nudMarginGutter.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("nudMarginGutter.ParseInfo.NumberStyle")));
			this.nudMarginGutter.ParseInfo.TrimEnd = ((bool)(resources.GetObject("nudMarginGutter.ParseInfo.TrimEnd")));
			this.nudMarginGutter.ParseInfo.TrimStart = ((bool)(resources.GetObject("nudMarginGutter.ParseInfo.TrimStart")));
			this.nudMarginGutter.PasswordChar = ((char)(resources.GetObject("nudMarginGutter.PasswordChar")));
			this.nudMarginGutter.PostValidation.CaseSensitive = ((bool)(resources.GetObject("nudMarginGutter.PostValidation.CaseSensitive")));
			this.nudMarginGutter.PostValidation.ErrorMessage = resources.GetString("nudMarginGutter.PostValidation.ErrorMessage");
			this.nudMarginGutter.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("nudMarginGutter.PostValidation.Inherit")));
			this.nudMarginGutter.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																										  ((C1.Win.C1Input.ValueInterval)(resources.GetObject("nudMarginGutter.PostValidation.Intervals")))});
			this.nudMarginGutter.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("nudMarginGutter.PostValidation.Validation")));
			this.nudMarginGutter.PostValidation.Values = ((System.Array)(resources.GetObject("nudMarginGutter.PostValidation.Values")));
			this.nudMarginGutter.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("nudMarginGutter.PostValidation.ValuesExcluded")));
			this.nudMarginGutter.PreValidation.CaseSensitive = ((bool)(resources.GetObject("nudMarginGutter.PreValidation.CaseSensitive")));
			this.nudMarginGutter.PreValidation.ErrorMessage = resources.GetString("nudMarginGutter.PreValidation.ErrorMessage");
			this.nudMarginGutter.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("nudMarginGutter.PreValidation.Inherit")));
			this.nudMarginGutter.PreValidation.ItemSeparator = resources.GetString("nudMarginGutter.PreValidation.ItemSeparator");
			this.nudMarginGutter.PreValidation.PatternString = resources.GetString("nudMarginGutter.PreValidation.PatternString");
			this.nudMarginGutter.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("nudMarginGutter.PreValidation.RegexOptions")));
			this.nudMarginGutter.PreValidation.TrimEnd = ((bool)(resources.GetObject("nudMarginGutter.PreValidation.TrimEnd")));
			this.nudMarginGutter.PreValidation.TrimStart = ((bool)(resources.GetObject("nudMarginGutter.PreValidation.TrimStart")));
			this.nudMarginGutter.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("nudMarginGutter.PreValidation.Validation")));
			this.nudMarginGutter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("nudMarginGutter.RightToLeft")));
			this.nudMarginGutter.ShowFocusRectangle = ((bool)(resources.GetObject("nudMarginGutter.ShowFocusRectangle")));
			this.nudMarginGutter.Size = ((System.Drawing.Size)(resources.GetObject("nudMarginGutter.Size")));
			this.nudMarginGutter.TabIndex = ((int)(resources.GetObject("nudMarginGutter.TabIndex")));
			this.nudMarginGutter.Tag = ((object)(resources.GetObject("nudMarginGutter.Tag")));
			this.nudMarginGutter.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("nudMarginGutter.TextAlign")));
			this.nudMarginGutter.TrimEnd = ((bool)(resources.GetObject("nudMarginGutter.TrimEnd")));
			this.nudMarginGutter.TrimStart = ((bool)(resources.GetObject("nudMarginGutter.TrimStart")));
			this.nudMarginGutter.UserCultureOverride = ((bool)(resources.GetObject("nudMarginGutter.UserCultureOverride")));
			this.nudMarginGutter.Value = ((object)(resources.GetObject("nudMarginGutter.Value")));
			this.nudMarginGutter.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("nudMarginGutter.VerticalAlign")));
			this.nudMarginGutter.Visible = ((bool)(resources.GetObject("nudMarginGutter.Visible")));
			this.nudMarginGutter.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("nudMarginGutter.VisibleButtons")));
			this.nudMarginGutter.ValueChanged += new System.EventHandler(this.nudMarginTop_ValueChanged);
			// 
			// nudMarginLeft
			// 
			this.nudMarginLeft.AcceptsEscape = ((bool)(resources.GetObject("nudMarginLeft.AcceptsEscape")));
			this.nudMarginLeft.AccessibleDescription = resources.GetString("nudMarginLeft.AccessibleDescription");
			this.nudMarginLeft.AccessibleName = resources.GetString("nudMarginLeft.AccessibleName");
			this.nudMarginLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("nudMarginLeft.Anchor")));
			this.nudMarginLeft.AutoSize = ((bool)(resources.GetObject("nudMarginLeft.AutoSize")));
			this.nudMarginLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nudMarginLeft.BackgroundImage")));
			this.nudMarginLeft.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("nudMarginLeft.BorderStyle")));
			// 
			// nudMarginLeft.Calculator
			// 
			this.nudMarginLeft.Calculator.AccessibleDescription = resources.GetString("nudMarginLeft.Calculator.AccessibleDescription");
			this.nudMarginLeft.Calculator.AccessibleName = resources.GetString("nudMarginLeft.Calculator.AccessibleName");
			this.nudMarginLeft.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nudMarginLeft.Calculator.BackgroundImage")));
			this.nudMarginLeft.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("nudMarginLeft.Calculator.ButtonFlatStyle")));
			this.nudMarginLeft.Calculator.DisplayFormat = resources.GetString("nudMarginLeft.Calculator.DisplayFormat");
			this.nudMarginLeft.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("nudMarginLeft.Calculator.Font")));
			this.nudMarginLeft.Calculator.FormatOnClose = ((bool)(resources.GetObject("nudMarginLeft.Calculator.FormatOnClose")));
			this.nudMarginLeft.Calculator.StoredFormat = resources.GetString("nudMarginLeft.Calculator.StoredFormat");
			this.nudMarginLeft.Calculator.UIStrings.Content = ((string[])(resources.GetObject("nudMarginLeft.Calculator.UIStrings.Content")));
			this.nudMarginLeft.CaseSensitive = ((bool)(resources.GetObject("nudMarginLeft.CaseSensitive")));
			this.nudMarginLeft.Culture = ((int)(resources.GetObject("nudMarginLeft.Culture")));
			this.nudMarginLeft.CustomFormat = resources.GetString("nudMarginLeft.CustomFormat");
			this.nudMarginLeft.DataType = ((System.Type)(resources.GetObject("nudMarginLeft.DataType")));
			this.nudMarginLeft.DisplayFormat.CustomFormat = resources.GetString("nudMarginLeft.DisplayFormat.CustomFormat");
			this.nudMarginLeft.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginLeft.DisplayFormat.FormatType")));
			this.nudMarginLeft.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("nudMarginLeft.DisplayFormat.Inherit")));
			this.nudMarginLeft.DisplayFormat.NullText = resources.GetString("nudMarginLeft.DisplayFormat.NullText");
			this.nudMarginLeft.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("nudMarginLeft.DisplayFormat.TrimEnd")));
			this.nudMarginLeft.DisplayFormat.TrimStart = ((bool)(resources.GetObject("nudMarginLeft.DisplayFormat.TrimStart")));
			this.nudMarginLeft.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("nudMarginLeft.Dock")));
			this.nudMarginLeft.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("nudMarginLeft.DropDownFormAlign")));
			this.nudMarginLeft.EditFormat.CustomFormat = resources.GetString("nudMarginLeft.EditFormat.CustomFormat");
			this.nudMarginLeft.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginLeft.EditFormat.FormatType")));
			this.nudMarginLeft.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("nudMarginLeft.EditFormat.Inherit")));
			this.nudMarginLeft.EditFormat.NullText = resources.GetString("nudMarginLeft.EditFormat.NullText");
			this.nudMarginLeft.EditFormat.TrimEnd = ((bool)(resources.GetObject("nudMarginLeft.EditFormat.TrimEnd")));
			this.nudMarginLeft.EditFormat.TrimStart = ((bool)(resources.GetObject("nudMarginLeft.EditFormat.TrimStart")));
			this.nudMarginLeft.EditMask = resources.GetString("nudMarginLeft.EditMask");
			this.nudMarginLeft.EmptyAsNull = ((bool)(resources.GetObject("nudMarginLeft.EmptyAsNull")));
			this.nudMarginLeft.Enabled = ((bool)(resources.GetObject("nudMarginLeft.Enabled")));
			this.nudMarginLeft.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("nudMarginLeft.ErrorInfo.BeepOnError")));
			this.nudMarginLeft.ErrorInfo.ErrorMessage = resources.GetString("nudMarginLeft.ErrorInfo.ErrorMessage");
			this.nudMarginLeft.ErrorInfo.ErrorMessageCaption = resources.GetString("nudMarginLeft.ErrorInfo.ErrorMessageCaption");
			this.nudMarginLeft.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("nudMarginLeft.ErrorInfo.ShowErrorMessage")));
			this.nudMarginLeft.ErrorInfo.ValueOnError = ((object)(resources.GetObject("nudMarginLeft.ErrorInfo.ValueOnError")));
			this.nudMarginLeft.Font = ((System.Drawing.Font)(resources.GetObject("nudMarginLeft.Font")));
			this.nudMarginLeft.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginLeft.FormatType")));
			this.nudMarginLeft.GapHeight = ((int)(resources.GetObject("nudMarginLeft.GapHeight")));
			this.nudMarginLeft.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("nudMarginLeft.ImeMode")));
			this.nudMarginLeft.Increment = ((object)(resources.GetObject("nudMarginLeft.Increment")));
			this.nudMarginLeft.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("nudMarginLeft.InitialSelection")));
			this.nudMarginLeft.Location = ((System.Drawing.Point)(resources.GetObject("nudMarginLeft.Location")));
			this.nudMarginLeft.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("nudMarginLeft.MaskInfo.AutoTabWhenFilled")));
			this.nudMarginLeft.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("nudMarginLeft.MaskInfo.CaseSensitive")));
			this.nudMarginLeft.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("nudMarginLeft.MaskInfo.CopyWithLiterals")));
			this.nudMarginLeft.MaskInfo.EditMask = resources.GetString("nudMarginLeft.MaskInfo.EditMask");
			this.nudMarginLeft.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("nudMarginLeft.MaskInfo.EmptyAsNull")));
			this.nudMarginLeft.MaskInfo.ErrorMessage = resources.GetString("nudMarginLeft.MaskInfo.ErrorMessage");
			this.nudMarginLeft.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("nudMarginLeft.MaskInfo.Inherit")));
			this.nudMarginLeft.MaskInfo.PromptChar = ((char)(resources.GetObject("nudMarginLeft.MaskInfo.PromptChar")));
			this.nudMarginLeft.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("nudMarginLeft.MaskInfo.ShowLiterals")));
			this.nudMarginLeft.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("nudMarginLeft.MaskInfo.StoredEmptyChar")));
			this.nudMarginLeft.MaxLength = ((int)(resources.GetObject("nudMarginLeft.MaxLength")));
			this.nudMarginLeft.Name = "nudMarginLeft";
			this.nudMarginLeft.NullText = resources.GetString("nudMarginLeft.NullText");
			this.nudMarginLeft.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("nudMarginLeft.ParseInfo.CaseSensitive")));
			this.nudMarginLeft.ParseInfo.CustomFormat = resources.GetString("nudMarginLeft.ParseInfo.CustomFormat");
			this.nudMarginLeft.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("nudMarginLeft.ParseInfo.EmptyAsNull")));
			this.nudMarginLeft.ParseInfo.ErrorMessage = resources.GetString("nudMarginLeft.ParseInfo.ErrorMessage");
			this.nudMarginLeft.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginLeft.ParseInfo.FormatType")));
			this.nudMarginLeft.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("nudMarginLeft.ParseInfo.Inherit")));
			this.nudMarginLeft.ParseInfo.NullText = resources.GetString("nudMarginLeft.ParseInfo.NullText");
			this.nudMarginLeft.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("nudMarginLeft.ParseInfo.NumberStyle")));
			this.nudMarginLeft.ParseInfo.TrimEnd = ((bool)(resources.GetObject("nudMarginLeft.ParseInfo.TrimEnd")));
			this.nudMarginLeft.ParseInfo.TrimStart = ((bool)(resources.GetObject("nudMarginLeft.ParseInfo.TrimStart")));
			this.nudMarginLeft.PasswordChar = ((char)(resources.GetObject("nudMarginLeft.PasswordChar")));
			this.nudMarginLeft.PostValidation.CaseSensitive = ((bool)(resources.GetObject("nudMarginLeft.PostValidation.CaseSensitive")));
			this.nudMarginLeft.PostValidation.ErrorMessage = resources.GetString("nudMarginLeft.PostValidation.ErrorMessage");
			this.nudMarginLeft.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("nudMarginLeft.PostValidation.Inherit")));
			this.nudMarginLeft.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																										((C1.Win.C1Input.ValueInterval)(resources.GetObject("nudMarginLeft.PostValidation.Intervals")))});
			this.nudMarginLeft.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("nudMarginLeft.PostValidation.Validation")));
			this.nudMarginLeft.PostValidation.Values = ((System.Array)(resources.GetObject("nudMarginLeft.PostValidation.Values")));
			this.nudMarginLeft.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("nudMarginLeft.PostValidation.ValuesExcluded")));
			this.nudMarginLeft.PreValidation.CaseSensitive = ((bool)(resources.GetObject("nudMarginLeft.PreValidation.CaseSensitive")));
			this.nudMarginLeft.PreValidation.ErrorMessage = resources.GetString("nudMarginLeft.PreValidation.ErrorMessage");
			this.nudMarginLeft.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("nudMarginLeft.PreValidation.Inherit")));
			this.nudMarginLeft.PreValidation.ItemSeparator = resources.GetString("nudMarginLeft.PreValidation.ItemSeparator");
			this.nudMarginLeft.PreValidation.PatternString = resources.GetString("nudMarginLeft.PreValidation.PatternString");
			this.nudMarginLeft.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("nudMarginLeft.PreValidation.RegexOptions")));
			this.nudMarginLeft.PreValidation.TrimEnd = ((bool)(resources.GetObject("nudMarginLeft.PreValidation.TrimEnd")));
			this.nudMarginLeft.PreValidation.TrimStart = ((bool)(resources.GetObject("nudMarginLeft.PreValidation.TrimStart")));
			this.nudMarginLeft.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("nudMarginLeft.PreValidation.Validation")));
			this.nudMarginLeft.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("nudMarginLeft.RightToLeft")));
			this.nudMarginLeft.ShowFocusRectangle = ((bool)(resources.GetObject("nudMarginLeft.ShowFocusRectangle")));
			this.nudMarginLeft.Size = ((System.Drawing.Size)(resources.GetObject("nudMarginLeft.Size")));
			this.nudMarginLeft.TabIndex = ((int)(resources.GetObject("nudMarginLeft.TabIndex")));
			this.nudMarginLeft.Tag = ((object)(resources.GetObject("nudMarginLeft.Tag")));
			this.nudMarginLeft.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("nudMarginLeft.TextAlign")));
			this.nudMarginLeft.TrimEnd = ((bool)(resources.GetObject("nudMarginLeft.TrimEnd")));
			this.nudMarginLeft.TrimStart = ((bool)(resources.GetObject("nudMarginLeft.TrimStart")));
			this.nudMarginLeft.UserCultureOverride = ((bool)(resources.GetObject("nudMarginLeft.UserCultureOverride")));
			this.nudMarginLeft.Value = ((object)(resources.GetObject("nudMarginLeft.Value")));
			this.nudMarginLeft.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("nudMarginLeft.VerticalAlign")));
			this.nudMarginLeft.Visible = ((bool)(resources.GetObject("nudMarginLeft.Visible")));
			this.nudMarginLeft.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("nudMarginLeft.VisibleButtons")));
			this.nudMarginLeft.ValueChanged += new System.EventHandler(this.nudMarginTop_ValueChanged);
			// 
			// cboGutterPosition
			// 
			this.cboGutterPosition.AccessibleDescription = resources.GetString("cboGutterPosition.AccessibleDescription");
			this.cboGutterPosition.AccessibleName = resources.GetString("cboGutterPosition.AccessibleName");
			this.cboGutterPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboGutterPosition.Anchor")));
			this.cboGutterPosition.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboGutterPosition.BackgroundImage")));
			this.cboGutterPosition.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboGutterPosition.Dock")));
			this.cboGutterPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGutterPosition.Enabled = ((bool)(resources.GetObject("cboGutterPosition.Enabled")));
			this.cboGutterPosition.Font = ((System.Drawing.Font)(resources.GetObject("cboGutterPosition.Font")));
			this.cboGutterPosition.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboGutterPosition.ImeMode")));
			this.cboGutterPosition.IntegralHeight = ((bool)(resources.GetObject("cboGutterPosition.IntegralHeight")));
			this.cboGutterPosition.ItemHeight = ((int)(resources.GetObject("cboGutterPosition.ItemHeight")));
			this.cboGutterPosition.Items.AddRange(new object[] {
																   resources.GetString("cboGutterPosition.Items"),
																   resources.GetString("cboGutterPosition.Items1")});
			this.cboGutterPosition.Location = ((System.Drawing.Point)(resources.GetObject("cboGutterPosition.Location")));
			this.cboGutterPosition.MaxDropDownItems = ((int)(resources.GetObject("cboGutterPosition.MaxDropDownItems")));
			this.cboGutterPosition.MaxLength = ((int)(resources.GetObject("cboGutterPosition.MaxLength")));
			this.cboGutterPosition.Name = "cboGutterPosition";
			this.cboGutterPosition.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboGutterPosition.RightToLeft")));
			this.cboGutterPosition.Size = ((System.Drawing.Size)(resources.GetObject("cboGutterPosition.Size")));
			this.cboGutterPosition.TabIndex = ((int)(resources.GetObject("cboGutterPosition.TabIndex")));
			this.cboGutterPosition.Text = resources.GetString("cboGutterPosition.Text");
			this.cboGutterPosition.Visible = ((bool)(resources.GetObject("cboGutterPosition.Visible")));
			this.cboGutterPosition.SelectedIndexChanged += new System.EventHandler(this.cboGutterPosition_SelectedIndexChanged);
			// 
			// lblGutterPosition
			// 
			this.lblGutterPosition.AccessibleDescription = resources.GetString("lblGutterPosition.AccessibleDescription");
			this.lblGutterPosition.AccessibleName = resources.GetString("lblGutterPosition.AccessibleName");
			this.lblGutterPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblGutterPosition.Anchor")));
			this.lblGutterPosition.AutoSize = ((bool)(resources.GetObject("lblGutterPosition.AutoSize")));
			this.lblGutterPosition.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblGutterPosition.Dock")));
			this.lblGutterPosition.Enabled = ((bool)(resources.GetObject("lblGutterPosition.Enabled")));
			this.lblGutterPosition.Font = ((System.Drawing.Font)(resources.GetObject("lblGutterPosition.Font")));
			this.lblGutterPosition.Image = ((System.Drawing.Image)(resources.GetObject("lblGutterPosition.Image")));
			this.lblGutterPosition.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGutterPosition.ImageAlign")));
			this.lblGutterPosition.ImageIndex = ((int)(resources.GetObject("lblGutterPosition.ImageIndex")));
			this.lblGutterPosition.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblGutterPosition.ImeMode")));
			this.lblGutterPosition.Location = ((System.Drawing.Point)(resources.GetObject("lblGutterPosition.Location")));
			this.lblGutterPosition.Name = "lblGutterPosition";
			this.lblGutterPosition.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblGutterPosition.RightToLeft")));
			this.lblGutterPosition.Size = ((System.Drawing.Size)(resources.GetObject("lblGutterPosition.Size")));
			this.lblGutterPosition.TabIndex = ((int)(resources.GetObject("lblGutterPosition.TabIndex")));
			this.lblGutterPosition.Text = resources.GetString("lblGutterPosition.Text");
			this.lblGutterPosition.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGutterPosition.TextAlign")));
			this.lblGutterPosition.Visible = ((bool)(resources.GetObject("lblGutterPosition.Visible")));
			// 
			// lblGutter
			// 
			this.lblGutter.AccessibleDescription = resources.GetString("lblGutter.AccessibleDescription");
			this.lblGutter.AccessibleName = resources.GetString("lblGutter.AccessibleName");
			this.lblGutter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblGutter.Anchor")));
			this.lblGutter.AutoSize = ((bool)(resources.GetObject("lblGutter.AutoSize")));
			this.lblGutter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblGutter.Dock")));
			this.lblGutter.Enabled = ((bool)(resources.GetObject("lblGutter.Enabled")));
			this.lblGutter.Font = ((System.Drawing.Font)(resources.GetObject("lblGutter.Font")));
			this.lblGutter.Image = ((System.Drawing.Image)(resources.GetObject("lblGutter.Image")));
			this.lblGutter.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGutter.ImageAlign")));
			this.lblGutter.ImageIndex = ((int)(resources.GetObject("lblGutter.ImageIndex")));
			this.lblGutter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblGutter.ImeMode")));
			this.lblGutter.Location = ((System.Drawing.Point)(resources.GetObject("lblGutter.Location")));
			this.lblGutter.Name = "lblGutter";
			this.lblGutter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblGutter.RightToLeft")));
			this.lblGutter.Size = ((System.Drawing.Size)(resources.GetObject("lblGutter.Size")));
			this.lblGutter.TabIndex = ((int)(resources.GetObject("lblGutter.TabIndex")));
			this.lblGutter.Text = resources.GetString("lblGutter.Text");
			this.lblGutter.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblGutter.TextAlign")));
			this.lblGutter.Visible = ((bool)(resources.GetObject("lblGutter.Visible")));
			// 
			// lblBottom
			// 
			this.lblBottom.AccessibleDescription = resources.GetString("lblBottom.AccessibleDescription");
			this.lblBottom.AccessibleName = resources.GetString("lblBottom.AccessibleName");
			this.lblBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblBottom.Anchor")));
			this.lblBottom.AutoSize = ((bool)(resources.GetObject("lblBottom.AutoSize")));
			this.lblBottom.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblBottom.Dock")));
			this.lblBottom.Enabled = ((bool)(resources.GetObject("lblBottom.Enabled")));
			this.lblBottom.Font = ((System.Drawing.Font)(resources.GetObject("lblBottom.Font")));
			this.lblBottom.Image = ((System.Drawing.Image)(resources.GetObject("lblBottom.Image")));
			this.lblBottom.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblBottom.ImageAlign")));
			this.lblBottom.ImageIndex = ((int)(resources.GetObject("lblBottom.ImageIndex")));
			this.lblBottom.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblBottom.ImeMode")));
			this.lblBottom.Location = ((System.Drawing.Point)(resources.GetObject("lblBottom.Location")));
			this.lblBottom.Name = "lblBottom";
			this.lblBottom.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblBottom.RightToLeft")));
			this.lblBottom.Size = ((System.Drawing.Size)(resources.GetObject("lblBottom.Size")));
			this.lblBottom.TabIndex = ((int)(resources.GetObject("lblBottom.TabIndex")));
			this.lblBottom.Text = resources.GetString("lblBottom.Text");
			this.lblBottom.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblBottom.TextAlign")));
			this.lblBottom.Visible = ((bool)(resources.GetObject("lblBottom.Visible")));
			// 
			// lblLeft
			// 
			this.lblLeft.AccessibleDescription = resources.GetString("lblLeft.AccessibleDescription");
			this.lblLeft.AccessibleName = resources.GetString("lblLeft.AccessibleName");
			this.lblLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblLeft.Anchor")));
			this.lblLeft.AutoSize = ((bool)(resources.GetObject("lblLeft.AutoSize")));
			this.lblLeft.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblLeft.Dock")));
			this.lblLeft.Enabled = ((bool)(resources.GetObject("lblLeft.Enabled")));
			this.lblLeft.Font = ((System.Drawing.Font)(resources.GetObject("lblLeft.Font")));
			this.lblLeft.Image = ((System.Drawing.Image)(resources.GetObject("lblLeft.Image")));
			this.lblLeft.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLeft.ImageAlign")));
			this.lblLeft.ImageIndex = ((int)(resources.GetObject("lblLeft.ImageIndex")));
			this.lblLeft.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblLeft.ImeMode")));
			this.lblLeft.Location = ((System.Drawing.Point)(resources.GetObject("lblLeft.Location")));
			this.lblLeft.Name = "lblLeft";
			this.lblLeft.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblLeft.RightToLeft")));
			this.lblLeft.Size = ((System.Drawing.Size)(resources.GetObject("lblLeft.Size")));
			this.lblLeft.TabIndex = ((int)(resources.GetObject("lblLeft.TabIndex")));
			this.lblLeft.Text = resources.GetString("lblLeft.Text");
			this.lblLeft.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblLeft.TextAlign")));
			this.lblLeft.Visible = ((bool)(resources.GetObject("lblLeft.Visible")));
			// 
			// lblRight
			// 
			this.lblRight.AccessibleDescription = resources.GetString("lblRight.AccessibleDescription");
			this.lblRight.AccessibleName = resources.GetString("lblRight.AccessibleName");
			this.lblRight.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblRight.Anchor")));
			this.lblRight.AutoSize = ((bool)(resources.GetObject("lblRight.AutoSize")));
			this.lblRight.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblRight.Dock")));
			this.lblRight.Enabled = ((bool)(resources.GetObject("lblRight.Enabled")));
			this.lblRight.Font = ((System.Drawing.Font)(resources.GetObject("lblRight.Font")));
			this.lblRight.Image = ((System.Drawing.Image)(resources.GetObject("lblRight.Image")));
			this.lblRight.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblRight.ImageAlign")));
			this.lblRight.ImageIndex = ((int)(resources.GetObject("lblRight.ImageIndex")));
			this.lblRight.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblRight.ImeMode")));
			this.lblRight.Location = ((System.Drawing.Point)(resources.GetObject("lblRight.Location")));
			this.lblRight.Name = "lblRight";
			this.lblRight.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblRight.RightToLeft")));
			this.lblRight.Size = ((System.Drawing.Size)(resources.GetObject("lblRight.Size")));
			this.lblRight.TabIndex = ((int)(resources.GetObject("lblRight.TabIndex")));
			this.lblRight.Text = resources.GetString("lblRight.Text");
			this.lblRight.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblRight.TextAlign")));
			this.lblRight.Visible = ((bool)(resources.GetObject("lblRight.Visible")));
			// 
			// lblTop
			// 
			this.lblTop.AccessibleDescription = resources.GetString("lblTop.AccessibleDescription");
			this.lblTop.AccessibleName = resources.GetString("lblTop.AccessibleName");
			this.lblTop.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTop.Anchor")));
			this.lblTop.AutoSize = ((bool)(resources.GetObject("lblTop.AutoSize")));
			this.lblTop.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTop.Dock")));
			this.lblTop.Enabled = ((bool)(resources.GetObject("lblTop.Enabled")));
			this.lblTop.Font = ((System.Drawing.Font)(resources.GetObject("lblTop.Font")));
			this.lblTop.Image = ((System.Drawing.Image)(resources.GetObject("lblTop.Image")));
			this.lblTop.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTop.ImageAlign")));
			this.lblTop.ImageIndex = ((int)(resources.GetObject("lblTop.ImageIndex")));
			this.lblTop.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTop.ImeMode")));
			this.lblTop.Location = ((System.Drawing.Point)(resources.GetObject("lblTop.Location")));
			this.lblTop.Name = "lblTop";
			this.lblTop.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTop.RightToLeft")));
			this.lblTop.Size = ((System.Drawing.Size)(resources.GetObject("lblTop.Size")));
			this.lblTop.TabIndex = ((int)(resources.GetObject("lblTop.TabIndex")));
			this.lblTop.Text = resources.GetString("lblTop.Text");
			this.lblTop.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTop.TextAlign")));
			this.lblTop.Visible = ((bool)(resources.GetObject("lblTop.Visible")));
			// 
			// nudMarginTop
			// 
			this.nudMarginTop.AcceptsEscape = ((bool)(resources.GetObject("nudMarginTop.AcceptsEscape")));
			this.nudMarginTop.AccessibleDescription = resources.GetString("nudMarginTop.AccessibleDescription");
			this.nudMarginTop.AccessibleName = resources.GetString("nudMarginTop.AccessibleName");
			this.nudMarginTop.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("nudMarginTop.Anchor")));
			this.nudMarginTop.AutoSize = ((bool)(resources.GetObject("nudMarginTop.AutoSize")));
			this.nudMarginTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nudMarginTop.BackgroundImage")));
			this.nudMarginTop.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("nudMarginTop.BorderStyle")));
			// 
			// nudMarginTop.Calculator
			// 
			this.nudMarginTop.Calculator.AccessibleDescription = resources.GetString("nudMarginTop.Calculator.AccessibleDescription");
			this.nudMarginTop.Calculator.AccessibleName = resources.GetString("nudMarginTop.Calculator.AccessibleName");
			this.nudMarginTop.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nudMarginTop.Calculator.BackgroundImage")));
			this.nudMarginTop.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("nudMarginTop.Calculator.ButtonFlatStyle")));
			this.nudMarginTop.Calculator.DisplayFormat = resources.GetString("nudMarginTop.Calculator.DisplayFormat");
			this.nudMarginTop.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("nudMarginTop.Calculator.Font")));
			this.nudMarginTop.Calculator.FormatOnClose = ((bool)(resources.GetObject("nudMarginTop.Calculator.FormatOnClose")));
			this.nudMarginTop.Calculator.StoredFormat = resources.GetString("nudMarginTop.Calculator.StoredFormat");
			this.nudMarginTop.Calculator.UIStrings.Content = ((string[])(resources.GetObject("nudMarginTop.Calculator.UIStrings.Content")));
			this.nudMarginTop.CaseSensitive = ((bool)(resources.GetObject("nudMarginTop.CaseSensitive")));
			this.nudMarginTop.Culture = ((int)(resources.GetObject("nudMarginTop.Culture")));
			this.nudMarginTop.CustomFormat = resources.GetString("nudMarginTop.CustomFormat");
			this.nudMarginTop.DataType = ((System.Type)(resources.GetObject("nudMarginTop.DataType")));
			this.nudMarginTop.DisplayFormat.CustomFormat = resources.GetString("nudMarginTop.DisplayFormat.CustomFormat");
			this.nudMarginTop.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginTop.DisplayFormat.FormatType")));
			this.nudMarginTop.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("nudMarginTop.DisplayFormat.Inherit")));
			this.nudMarginTop.DisplayFormat.NullText = resources.GetString("nudMarginTop.DisplayFormat.NullText");
			this.nudMarginTop.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("nudMarginTop.DisplayFormat.TrimEnd")));
			this.nudMarginTop.DisplayFormat.TrimStart = ((bool)(resources.GetObject("nudMarginTop.DisplayFormat.TrimStart")));
			this.nudMarginTop.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("nudMarginTop.Dock")));
			this.nudMarginTop.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("nudMarginTop.DropDownFormAlign")));
			this.nudMarginTop.EditFormat.CustomFormat = resources.GetString("nudMarginTop.EditFormat.CustomFormat");
			this.nudMarginTop.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginTop.EditFormat.FormatType")));
			this.nudMarginTop.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("nudMarginTop.EditFormat.Inherit")));
			this.nudMarginTop.EditFormat.NullText = resources.GetString("nudMarginTop.EditFormat.NullText");
			this.nudMarginTop.EditFormat.TrimEnd = ((bool)(resources.GetObject("nudMarginTop.EditFormat.TrimEnd")));
			this.nudMarginTop.EditFormat.TrimStart = ((bool)(resources.GetObject("nudMarginTop.EditFormat.TrimStart")));
			this.nudMarginTop.EditMask = resources.GetString("nudMarginTop.EditMask");
			this.nudMarginTop.EmptyAsNull = ((bool)(resources.GetObject("nudMarginTop.EmptyAsNull")));
			this.nudMarginTop.Enabled = ((bool)(resources.GetObject("nudMarginTop.Enabled")));
			this.nudMarginTop.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("nudMarginTop.ErrorInfo.BeepOnError")));
			this.nudMarginTop.ErrorInfo.ErrorMessage = resources.GetString("nudMarginTop.ErrorInfo.ErrorMessage");
			this.nudMarginTop.ErrorInfo.ErrorMessageCaption = resources.GetString("nudMarginTop.ErrorInfo.ErrorMessageCaption");
			this.nudMarginTop.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("nudMarginTop.ErrorInfo.ShowErrorMessage")));
			this.nudMarginTop.ErrorInfo.ValueOnError = ((object)(resources.GetObject("nudMarginTop.ErrorInfo.ValueOnError")));
			this.nudMarginTop.Font = ((System.Drawing.Font)(resources.GetObject("nudMarginTop.Font")));
			this.nudMarginTop.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginTop.FormatType")));
			this.nudMarginTop.GapHeight = ((int)(resources.GetObject("nudMarginTop.GapHeight")));
			this.nudMarginTop.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("nudMarginTop.ImeMode")));
			this.nudMarginTop.Increment = ((object)(resources.GetObject("nudMarginTop.Increment")));
			this.nudMarginTop.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("nudMarginTop.InitialSelection")));
			this.nudMarginTop.Location = ((System.Drawing.Point)(resources.GetObject("nudMarginTop.Location")));
			this.nudMarginTop.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("nudMarginTop.MaskInfo.AutoTabWhenFilled")));
			this.nudMarginTop.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("nudMarginTop.MaskInfo.CaseSensitive")));
			this.nudMarginTop.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("nudMarginTop.MaskInfo.CopyWithLiterals")));
			this.nudMarginTop.MaskInfo.EditMask = resources.GetString("nudMarginTop.MaskInfo.EditMask");
			this.nudMarginTop.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("nudMarginTop.MaskInfo.EmptyAsNull")));
			this.nudMarginTop.MaskInfo.ErrorMessage = resources.GetString("nudMarginTop.MaskInfo.ErrorMessage");
			this.nudMarginTop.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("nudMarginTop.MaskInfo.Inherit")));
			this.nudMarginTop.MaskInfo.PromptChar = ((char)(resources.GetObject("nudMarginTop.MaskInfo.PromptChar")));
			this.nudMarginTop.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("nudMarginTop.MaskInfo.ShowLiterals")));
			this.nudMarginTop.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("nudMarginTop.MaskInfo.StoredEmptyChar")));
			this.nudMarginTop.MaxLength = ((int)(resources.GetObject("nudMarginTop.MaxLength")));
			this.nudMarginTop.Name = "nudMarginTop";
			this.nudMarginTop.NullText = resources.GetString("nudMarginTop.NullText");
			this.nudMarginTop.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("nudMarginTop.ParseInfo.CaseSensitive")));
			this.nudMarginTop.ParseInfo.CustomFormat = resources.GetString("nudMarginTop.ParseInfo.CustomFormat");
			this.nudMarginTop.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("nudMarginTop.ParseInfo.EmptyAsNull")));
			this.nudMarginTop.ParseInfo.ErrorMessage = resources.GetString("nudMarginTop.ParseInfo.ErrorMessage");
			this.nudMarginTop.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("nudMarginTop.ParseInfo.FormatType")));
			this.nudMarginTop.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("nudMarginTop.ParseInfo.Inherit")));
			this.nudMarginTop.ParseInfo.NullText = resources.GetString("nudMarginTop.ParseInfo.NullText");
			this.nudMarginTop.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("nudMarginTop.ParseInfo.NumberStyle")));
			this.nudMarginTop.ParseInfo.TrimEnd = ((bool)(resources.GetObject("nudMarginTop.ParseInfo.TrimEnd")));
			this.nudMarginTop.ParseInfo.TrimStart = ((bool)(resources.GetObject("nudMarginTop.ParseInfo.TrimStart")));
			this.nudMarginTop.PasswordChar = ((char)(resources.GetObject("nudMarginTop.PasswordChar")));
			this.nudMarginTop.PostValidation.CaseSensitive = ((bool)(resources.GetObject("nudMarginTop.PostValidation.CaseSensitive")));
			this.nudMarginTop.PostValidation.ErrorMessage = resources.GetString("nudMarginTop.PostValidation.ErrorMessage");
			this.nudMarginTop.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("nudMarginTop.PostValidation.Inherit")));
			this.nudMarginTop.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									   ((C1.Win.C1Input.ValueInterval)(resources.GetObject("nudMarginTop.PostValidation.Intervals")))});
			this.nudMarginTop.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("nudMarginTop.PostValidation.Validation")));
			this.nudMarginTop.PostValidation.Values = ((System.Array)(resources.GetObject("nudMarginTop.PostValidation.Values")));
			this.nudMarginTop.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("nudMarginTop.PostValidation.ValuesExcluded")));
			this.nudMarginTop.PreValidation.CaseSensitive = ((bool)(resources.GetObject("nudMarginTop.PreValidation.CaseSensitive")));
			this.nudMarginTop.PreValidation.ErrorMessage = resources.GetString("nudMarginTop.PreValidation.ErrorMessage");
			this.nudMarginTop.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("nudMarginTop.PreValidation.Inherit")));
			this.nudMarginTop.PreValidation.ItemSeparator = resources.GetString("nudMarginTop.PreValidation.ItemSeparator");
			this.nudMarginTop.PreValidation.PatternString = resources.GetString("nudMarginTop.PreValidation.PatternString");
			this.nudMarginTop.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("nudMarginTop.PreValidation.RegexOptions")));
			this.nudMarginTop.PreValidation.TrimEnd = ((bool)(resources.GetObject("nudMarginTop.PreValidation.TrimEnd")));
			this.nudMarginTop.PreValidation.TrimStart = ((bool)(resources.GetObject("nudMarginTop.PreValidation.TrimStart")));
			this.nudMarginTop.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("nudMarginTop.PreValidation.Validation")));
			this.nudMarginTop.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("nudMarginTop.RightToLeft")));
			this.nudMarginTop.ShowFocusRectangle = ((bool)(resources.GetObject("nudMarginTop.ShowFocusRectangle")));
			this.nudMarginTop.Size = ((System.Drawing.Size)(resources.GetObject("nudMarginTop.Size")));
			this.nudMarginTop.TabIndex = ((int)(resources.GetObject("nudMarginTop.TabIndex")));
			this.nudMarginTop.Tag = ((object)(resources.GetObject("nudMarginTop.Tag")));
			this.nudMarginTop.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("nudMarginTop.TextAlign")));
			this.nudMarginTop.TrimEnd = ((bool)(resources.GetObject("nudMarginTop.TrimEnd")));
			this.nudMarginTop.TrimStart = ((bool)(resources.GetObject("nudMarginTop.TrimStart")));
			this.nudMarginTop.UserCultureOverride = ((bool)(resources.GetObject("nudMarginTop.UserCultureOverride")));
			this.nudMarginTop.Value = ((object)(resources.GetObject("nudMarginTop.Value")));
			this.nudMarginTop.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("nudMarginTop.VerticalAlign")));
			this.nudMarginTop.Visible = ((bool)(resources.GetObject("nudMarginTop.Visible")));
			this.nudMarginTop.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("nudMarginTop.VisibleButtons")));
			this.nudMarginTop.ValueChanged += new System.EventHandler(this.nudMarginTop_ValueChanged);
			// 
			// tabFonts
			// 
			this.tabFonts.AccessibleDescription = resources.GetString("tabFonts.AccessibleDescription");
			this.tabFonts.AccessibleName = resources.GetString("tabFonts.AccessibleName");
			this.tabFonts.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabFonts.Anchor")));
			this.tabFonts.AutoScroll = ((bool)(resources.GetObject("tabFonts.AutoScroll")));
			this.tabFonts.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabFonts.AutoScrollMargin")));
			this.tabFonts.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabFonts.AutoScrollMinSize")));
			this.tabFonts.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabFonts.BackgroundImage")));
			this.tabFonts.Controls.Add(this.grpReportFooter);
			this.tabFonts.Controls.Add(this.grpPageFooter);
			this.tabFonts.Controls.Add(this.grpPageHeader);
			this.tabFonts.Controls.Add(this.grpDetail);
			this.tabFonts.Controls.Add(this.grpGroupBy);
			this.tabFonts.Controls.Add(this.grpParameter);
			this.tabFonts.Controls.Add(this.grpReportHeader);
			this.tabFonts.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabFonts.Dock")));
			this.tabFonts.Enabled = ((bool)(resources.GetObject("tabFonts.Enabled")));
			this.tabFonts.Font = ((System.Drawing.Font)(resources.GetObject("tabFonts.Font")));
			this.tabFonts.ImageIndex = ((int)(resources.GetObject("tabFonts.ImageIndex")));
			this.tabFonts.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabFonts.ImeMode")));
			this.tabFonts.Location = ((System.Drawing.Point)(resources.GetObject("tabFonts.Location")));
			this.tabFonts.Name = "tabFonts";
			this.tabFonts.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabFonts.RightToLeft")));
			this.tabFonts.Size = ((System.Drawing.Size)(resources.GetObject("tabFonts.Size")));
			this.tabFonts.TabIndex = ((int)(resources.GetObject("tabFonts.TabIndex")));
			this.tabFonts.Text = resources.GetString("tabFonts.Text");
			this.tabFonts.ToolTipText = resources.GetString("tabFonts.ToolTipText");
			this.tabFonts.Visible = ((bool)(resources.GetObject("tabFonts.Visible")));
			this.tabFonts.Click += new System.EventHandler(this.tabFonts_Click);
			// 
			// grpReportFooter
			// 
			this.grpReportFooter.AccessibleDescription = resources.GetString("grpReportFooter.AccessibleDescription");
			this.grpReportFooter.AccessibleName = resources.GetString("grpReportFooter.AccessibleName");
			this.grpReportFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpReportFooter.Anchor")));
			this.grpReportFooter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpReportFooter.BackgroundImage")));
			this.grpReportFooter.Controls.Add(this.txtReportFooterFont);
			this.grpReportFooter.Controls.Add(this.btnReportFooterDefault);
			this.grpReportFooter.Controls.Add(this.btnReportFooter);
			this.grpReportFooter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpReportFooter.Dock")));
			this.grpReportFooter.Enabled = ((bool)(resources.GetObject("grpReportFooter.Enabled")));
			this.grpReportFooter.Font = ((System.Drawing.Font)(resources.GetObject("grpReportFooter.Font")));
			this.grpReportFooter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpReportFooter.ImeMode")));
			this.grpReportFooter.Location = ((System.Drawing.Point)(resources.GetObject("grpReportFooter.Location")));
			this.grpReportFooter.Name = "grpReportFooter";
			this.grpReportFooter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpReportFooter.RightToLeft")));
			this.grpReportFooter.Size = ((System.Drawing.Size)(resources.GetObject("grpReportFooter.Size")));
			this.grpReportFooter.TabIndex = ((int)(resources.GetObject("grpReportFooter.TabIndex")));
			this.grpReportFooter.TabStop = false;
			this.grpReportFooter.Text = resources.GetString("grpReportFooter.Text");
			this.grpReportFooter.Visible = ((bool)(resources.GetObject("grpReportFooter.Visible")));
			// 
			// txtReportFooterFont
			// 
			this.txtReportFooterFont.AccessibleDescription = resources.GetString("txtReportFooterFont.AccessibleDescription");
			this.txtReportFooterFont.AccessibleName = resources.GetString("txtReportFooterFont.AccessibleName");
			this.txtReportFooterFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtReportFooterFont.Anchor")));
			this.txtReportFooterFont.AutoSize = ((bool)(resources.GetObject("txtReportFooterFont.AutoSize")));
			this.txtReportFooterFont.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtReportFooterFont.BackgroundImage")));
			this.txtReportFooterFont.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtReportFooterFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtReportFooterFont.Dock")));
			this.txtReportFooterFont.Enabled = ((bool)(resources.GetObject("txtReportFooterFont.Enabled")));
			this.txtReportFooterFont.Font = ((System.Drawing.Font)(resources.GetObject("txtReportFooterFont.Font")));
			this.txtReportFooterFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtReportFooterFont.ImeMode")));
			this.txtReportFooterFont.Location = ((System.Drawing.Point)(resources.GetObject("txtReportFooterFont.Location")));
			this.txtReportFooterFont.MaxLength = ((int)(resources.GetObject("txtReportFooterFont.MaxLength")));
			this.txtReportFooterFont.Multiline = ((bool)(resources.GetObject("txtReportFooterFont.Multiline")));
			this.txtReportFooterFont.Name = "txtReportFooterFont";
			this.txtReportFooterFont.PasswordChar = ((char)(resources.GetObject("txtReportFooterFont.PasswordChar")));
			this.txtReportFooterFont.ReadOnly = true;
			this.txtReportFooterFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtReportFooterFont.RightToLeft")));
			this.txtReportFooterFont.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtReportFooterFont.ScrollBars")));
			this.txtReportFooterFont.Size = ((System.Drawing.Size)(resources.GetObject("txtReportFooterFont.Size")));
			this.txtReportFooterFont.TabIndex = ((int)(resources.GetObject("txtReportFooterFont.TabIndex")));
			this.txtReportFooterFont.TabStop = false;
			this.txtReportFooterFont.Text = resources.GetString("txtReportFooterFont.Text");
			this.txtReportFooterFont.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtReportFooterFont.TextAlign")));
			this.txtReportFooterFont.Visible = ((bool)(resources.GetObject("txtReportFooterFont.Visible")));
			this.txtReportFooterFont.WordWrap = ((bool)(resources.GetObject("txtReportFooterFont.WordWrap")));
			// 
			// btnReportFooterDefault
			// 
			this.btnReportFooterDefault.AccessibleDescription = resources.GetString("btnReportFooterDefault.AccessibleDescription");
			this.btnReportFooterDefault.AccessibleName = resources.GetString("btnReportFooterDefault.AccessibleName");
			this.btnReportFooterDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnReportFooterDefault.Anchor")));
			this.btnReportFooterDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReportFooterDefault.BackgroundImage")));
			this.btnReportFooterDefault.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnReportFooterDefault.Dock")));
			this.btnReportFooterDefault.Enabled = ((bool)(resources.GetObject("btnReportFooterDefault.Enabled")));
			this.btnReportFooterDefault.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnReportFooterDefault.FlatStyle")));
			this.btnReportFooterDefault.Font = ((System.Drawing.Font)(resources.GetObject("btnReportFooterDefault.Font")));
			this.btnReportFooterDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnReportFooterDefault.Image")));
			this.btnReportFooterDefault.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnReportFooterDefault.ImageAlign")));
			this.btnReportFooterDefault.ImageIndex = ((int)(resources.GetObject("btnReportFooterDefault.ImageIndex")));
			this.btnReportFooterDefault.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnReportFooterDefault.ImeMode")));
			this.btnReportFooterDefault.Location = ((System.Drawing.Point)(resources.GetObject("btnReportFooterDefault.Location")));
			this.btnReportFooterDefault.Name = "btnReportFooterDefault";
			this.btnReportFooterDefault.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnReportFooterDefault.RightToLeft")));
			this.btnReportFooterDefault.Size = ((System.Drawing.Size)(resources.GetObject("btnReportFooterDefault.Size")));
			this.btnReportFooterDefault.TabIndex = ((int)(resources.GetObject("btnReportFooterDefault.TabIndex")));
			this.btnReportFooterDefault.Text = resources.GetString("btnReportFooterDefault.Text");
			this.btnReportFooterDefault.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnReportFooterDefault.TextAlign")));
			this.btnReportFooterDefault.Visible = ((bool)(resources.GetObject("btnReportFooterDefault.Visible")));
			this.btnReportFooterDefault.Click += new System.EventHandler(this.ButtonDefaultFont_Click);
			// 
			// btnReportFooter
			// 
			this.btnReportFooter.AccessibleDescription = resources.GetString("btnReportFooter.AccessibleDescription");
			this.btnReportFooter.AccessibleName = resources.GetString("btnReportFooter.AccessibleName");
			this.btnReportFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnReportFooter.Anchor")));
			this.btnReportFooter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReportFooter.BackgroundImage")));
			this.btnReportFooter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnReportFooter.Dock")));
			this.btnReportFooter.Enabled = ((bool)(resources.GetObject("btnReportFooter.Enabled")));
			this.btnReportFooter.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnReportFooter.FlatStyle")));
			this.btnReportFooter.Font = ((System.Drawing.Font)(resources.GetObject("btnReportFooter.Font")));
			this.btnReportFooter.Image = ((System.Drawing.Image)(resources.GetObject("btnReportFooter.Image")));
			this.btnReportFooter.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnReportFooter.ImageAlign")));
			this.btnReportFooter.ImageIndex = ((int)(resources.GetObject("btnReportFooter.ImageIndex")));
			this.btnReportFooter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnReportFooter.ImeMode")));
			this.btnReportFooter.Location = ((System.Drawing.Point)(resources.GetObject("btnReportFooter.Location")));
			this.btnReportFooter.Name = "btnReportFooter";
			this.btnReportFooter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnReportFooter.RightToLeft")));
			this.btnReportFooter.Size = ((System.Drawing.Size)(resources.GetObject("btnReportFooter.Size")));
			this.btnReportFooter.TabIndex = ((int)(resources.GetObject("btnReportFooter.TabIndex")));
			this.btnReportFooter.Text = resources.GetString("btnReportFooter.Text");
			this.btnReportFooter.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnReportFooter.TextAlign")));
			this.btnReportFooter.Visible = ((bool)(resources.GetObject("btnReportFooter.Visible")));
			this.btnReportFooter.Click += new System.EventHandler(this.ButtonFont_Click);
			// 
			// grpPageFooter
			// 
			this.grpPageFooter.AccessibleDescription = resources.GetString("grpPageFooter.AccessibleDescription");
			this.grpPageFooter.AccessibleName = resources.GetString("grpPageFooter.AccessibleName");
			this.grpPageFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpPageFooter.Anchor")));
			this.grpPageFooter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpPageFooter.BackgroundImage")));
			this.grpPageFooter.Controls.Add(this.txtPageFooterFont);
			this.grpPageFooter.Controls.Add(this.btnPageFooterDefault);
			this.grpPageFooter.Controls.Add(this.btnPageFooter);
			this.grpPageFooter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpPageFooter.Dock")));
			this.grpPageFooter.Enabled = ((bool)(resources.GetObject("grpPageFooter.Enabled")));
			this.grpPageFooter.Font = ((System.Drawing.Font)(resources.GetObject("grpPageFooter.Font")));
			this.grpPageFooter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpPageFooter.ImeMode")));
			this.grpPageFooter.Location = ((System.Drawing.Point)(resources.GetObject("grpPageFooter.Location")));
			this.grpPageFooter.Name = "grpPageFooter";
			this.grpPageFooter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpPageFooter.RightToLeft")));
			this.grpPageFooter.Size = ((System.Drawing.Size)(resources.GetObject("grpPageFooter.Size")));
			this.grpPageFooter.TabIndex = ((int)(resources.GetObject("grpPageFooter.TabIndex")));
			this.grpPageFooter.TabStop = false;
			this.grpPageFooter.Text = resources.GetString("grpPageFooter.Text");
			this.grpPageFooter.Visible = ((bool)(resources.GetObject("grpPageFooter.Visible")));
			// 
			// txtPageFooterFont
			// 
			this.txtPageFooterFont.AccessibleDescription = resources.GetString("txtPageFooterFont.AccessibleDescription");
			this.txtPageFooterFont.AccessibleName = resources.GetString("txtPageFooterFont.AccessibleName");
			this.txtPageFooterFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPageFooterFont.Anchor")));
			this.txtPageFooterFont.AutoSize = ((bool)(resources.GetObject("txtPageFooterFont.AutoSize")));
			this.txtPageFooterFont.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPageFooterFont.BackgroundImage")));
			this.txtPageFooterFont.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtPageFooterFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPageFooterFont.Dock")));
			this.txtPageFooterFont.Enabled = ((bool)(resources.GetObject("txtPageFooterFont.Enabled")));
			this.txtPageFooterFont.Font = ((System.Drawing.Font)(resources.GetObject("txtPageFooterFont.Font")));
			this.txtPageFooterFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPageFooterFont.ImeMode")));
			this.txtPageFooterFont.Location = ((System.Drawing.Point)(resources.GetObject("txtPageFooterFont.Location")));
			this.txtPageFooterFont.MaxLength = ((int)(resources.GetObject("txtPageFooterFont.MaxLength")));
			this.txtPageFooterFont.Multiline = ((bool)(resources.GetObject("txtPageFooterFont.Multiline")));
			this.txtPageFooterFont.Name = "txtPageFooterFont";
			this.txtPageFooterFont.PasswordChar = ((char)(resources.GetObject("txtPageFooterFont.PasswordChar")));
			this.txtPageFooterFont.ReadOnly = true;
			this.txtPageFooterFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPageFooterFont.RightToLeft")));
			this.txtPageFooterFont.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPageFooterFont.ScrollBars")));
			this.txtPageFooterFont.Size = ((System.Drawing.Size)(resources.GetObject("txtPageFooterFont.Size")));
			this.txtPageFooterFont.TabIndex = ((int)(resources.GetObject("txtPageFooterFont.TabIndex")));
			this.txtPageFooterFont.TabStop = false;
			this.txtPageFooterFont.Text = resources.GetString("txtPageFooterFont.Text");
			this.txtPageFooterFont.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPageFooterFont.TextAlign")));
			this.txtPageFooterFont.Visible = ((bool)(resources.GetObject("txtPageFooterFont.Visible")));
			this.txtPageFooterFont.WordWrap = ((bool)(resources.GetObject("txtPageFooterFont.WordWrap")));
			// 
			// btnPageFooterDefault
			// 
			this.btnPageFooterDefault.AccessibleDescription = resources.GetString("btnPageFooterDefault.AccessibleDescription");
			this.btnPageFooterDefault.AccessibleName = resources.GetString("btnPageFooterDefault.AccessibleName");
			this.btnPageFooterDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPageFooterDefault.Anchor")));
			this.btnPageFooterDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPageFooterDefault.BackgroundImage")));
			this.btnPageFooterDefault.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPageFooterDefault.Dock")));
			this.btnPageFooterDefault.Enabled = ((bool)(resources.GetObject("btnPageFooterDefault.Enabled")));
			this.btnPageFooterDefault.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPageFooterDefault.FlatStyle")));
			this.btnPageFooterDefault.Font = ((System.Drawing.Font)(resources.GetObject("btnPageFooterDefault.Font")));
			this.btnPageFooterDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnPageFooterDefault.Image")));
			this.btnPageFooterDefault.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPageFooterDefault.ImageAlign")));
			this.btnPageFooterDefault.ImageIndex = ((int)(resources.GetObject("btnPageFooterDefault.ImageIndex")));
			this.btnPageFooterDefault.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPageFooterDefault.ImeMode")));
			this.btnPageFooterDefault.Location = ((System.Drawing.Point)(resources.GetObject("btnPageFooterDefault.Location")));
			this.btnPageFooterDefault.Name = "btnPageFooterDefault";
			this.btnPageFooterDefault.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPageFooterDefault.RightToLeft")));
			this.btnPageFooterDefault.Size = ((System.Drawing.Size)(resources.GetObject("btnPageFooterDefault.Size")));
			this.btnPageFooterDefault.TabIndex = ((int)(resources.GetObject("btnPageFooterDefault.TabIndex")));
			this.btnPageFooterDefault.Text = resources.GetString("btnPageFooterDefault.Text");
			this.btnPageFooterDefault.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPageFooterDefault.TextAlign")));
			this.btnPageFooterDefault.Visible = ((bool)(resources.GetObject("btnPageFooterDefault.Visible")));
			this.btnPageFooterDefault.Click += new System.EventHandler(this.ButtonDefaultFont_Click);
			// 
			// btnPageFooter
			// 
			this.btnPageFooter.AccessibleDescription = resources.GetString("btnPageFooter.AccessibleDescription");
			this.btnPageFooter.AccessibleName = resources.GetString("btnPageFooter.AccessibleName");
			this.btnPageFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPageFooter.Anchor")));
			this.btnPageFooter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPageFooter.BackgroundImage")));
			this.btnPageFooter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPageFooter.Dock")));
			this.btnPageFooter.Enabled = ((bool)(resources.GetObject("btnPageFooter.Enabled")));
			this.btnPageFooter.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPageFooter.FlatStyle")));
			this.btnPageFooter.Font = ((System.Drawing.Font)(resources.GetObject("btnPageFooter.Font")));
			this.btnPageFooter.Image = ((System.Drawing.Image)(resources.GetObject("btnPageFooter.Image")));
			this.btnPageFooter.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPageFooter.ImageAlign")));
			this.btnPageFooter.ImageIndex = ((int)(resources.GetObject("btnPageFooter.ImageIndex")));
			this.btnPageFooter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPageFooter.ImeMode")));
			this.btnPageFooter.Location = ((System.Drawing.Point)(resources.GetObject("btnPageFooter.Location")));
			this.btnPageFooter.Name = "btnPageFooter";
			this.btnPageFooter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPageFooter.RightToLeft")));
			this.btnPageFooter.Size = ((System.Drawing.Size)(resources.GetObject("btnPageFooter.Size")));
			this.btnPageFooter.TabIndex = ((int)(resources.GetObject("btnPageFooter.TabIndex")));
			this.btnPageFooter.Text = resources.GetString("btnPageFooter.Text");
			this.btnPageFooter.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPageFooter.TextAlign")));
			this.btnPageFooter.Visible = ((bool)(resources.GetObject("btnPageFooter.Visible")));
			this.btnPageFooter.Click += new System.EventHandler(this.ButtonFont_Click);
			// 
			// grpPageHeader
			// 
			this.grpPageHeader.AccessibleDescription = resources.GetString("grpPageHeader.AccessibleDescription");
			this.grpPageHeader.AccessibleName = resources.GetString("grpPageHeader.AccessibleName");
			this.grpPageHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpPageHeader.Anchor")));
			this.grpPageHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpPageHeader.BackgroundImage")));
			this.grpPageHeader.Controls.Add(this.txtPageHeaderFont);
			this.grpPageHeader.Controls.Add(this.btnPageHeaderDefault);
			this.grpPageHeader.Controls.Add(this.btnPageHeader);
			this.grpPageHeader.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpPageHeader.Dock")));
			this.grpPageHeader.Enabled = ((bool)(resources.GetObject("grpPageHeader.Enabled")));
			this.grpPageHeader.Font = ((System.Drawing.Font)(resources.GetObject("grpPageHeader.Font")));
			this.grpPageHeader.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpPageHeader.ImeMode")));
			this.grpPageHeader.Location = ((System.Drawing.Point)(resources.GetObject("grpPageHeader.Location")));
			this.grpPageHeader.Name = "grpPageHeader";
			this.grpPageHeader.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpPageHeader.RightToLeft")));
			this.grpPageHeader.Size = ((System.Drawing.Size)(resources.GetObject("grpPageHeader.Size")));
			this.grpPageHeader.TabIndex = ((int)(resources.GetObject("grpPageHeader.TabIndex")));
			this.grpPageHeader.TabStop = false;
			this.grpPageHeader.Text = resources.GetString("grpPageHeader.Text");
			this.grpPageHeader.Visible = ((bool)(resources.GetObject("grpPageHeader.Visible")));
			// 
			// txtPageHeaderFont
			// 
			this.txtPageHeaderFont.AccessibleDescription = resources.GetString("txtPageHeaderFont.AccessibleDescription");
			this.txtPageHeaderFont.AccessibleName = resources.GetString("txtPageHeaderFont.AccessibleName");
			this.txtPageHeaderFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPageHeaderFont.Anchor")));
			this.txtPageHeaderFont.AutoSize = ((bool)(resources.GetObject("txtPageHeaderFont.AutoSize")));
			this.txtPageHeaderFont.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPageHeaderFont.BackgroundImage")));
			this.txtPageHeaderFont.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtPageHeaderFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPageHeaderFont.Dock")));
			this.txtPageHeaderFont.Enabled = ((bool)(resources.GetObject("txtPageHeaderFont.Enabled")));
			this.txtPageHeaderFont.Font = ((System.Drawing.Font)(resources.GetObject("txtPageHeaderFont.Font")));
			this.txtPageHeaderFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPageHeaderFont.ImeMode")));
			this.txtPageHeaderFont.Location = ((System.Drawing.Point)(resources.GetObject("txtPageHeaderFont.Location")));
			this.txtPageHeaderFont.MaxLength = ((int)(resources.GetObject("txtPageHeaderFont.MaxLength")));
			this.txtPageHeaderFont.Multiline = ((bool)(resources.GetObject("txtPageHeaderFont.Multiline")));
			this.txtPageHeaderFont.Name = "txtPageHeaderFont";
			this.txtPageHeaderFont.PasswordChar = ((char)(resources.GetObject("txtPageHeaderFont.PasswordChar")));
			this.txtPageHeaderFont.ReadOnly = true;
			this.txtPageHeaderFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPageHeaderFont.RightToLeft")));
			this.txtPageHeaderFont.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPageHeaderFont.ScrollBars")));
			this.txtPageHeaderFont.Size = ((System.Drawing.Size)(resources.GetObject("txtPageHeaderFont.Size")));
			this.txtPageHeaderFont.TabIndex = ((int)(resources.GetObject("txtPageHeaderFont.TabIndex")));
			this.txtPageHeaderFont.TabStop = false;
			this.txtPageHeaderFont.Text = resources.GetString("txtPageHeaderFont.Text");
			this.txtPageHeaderFont.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPageHeaderFont.TextAlign")));
			this.txtPageHeaderFont.Visible = ((bool)(resources.GetObject("txtPageHeaderFont.Visible")));
			this.txtPageHeaderFont.WordWrap = ((bool)(resources.GetObject("txtPageHeaderFont.WordWrap")));
			// 
			// btnPageHeaderDefault
			// 
			this.btnPageHeaderDefault.AccessibleDescription = resources.GetString("btnPageHeaderDefault.AccessibleDescription");
			this.btnPageHeaderDefault.AccessibleName = resources.GetString("btnPageHeaderDefault.AccessibleName");
			this.btnPageHeaderDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPageHeaderDefault.Anchor")));
			this.btnPageHeaderDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPageHeaderDefault.BackgroundImage")));
			this.btnPageHeaderDefault.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPageHeaderDefault.Dock")));
			this.btnPageHeaderDefault.Enabled = ((bool)(resources.GetObject("btnPageHeaderDefault.Enabled")));
			this.btnPageHeaderDefault.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPageHeaderDefault.FlatStyle")));
			this.btnPageHeaderDefault.Font = ((System.Drawing.Font)(resources.GetObject("btnPageHeaderDefault.Font")));
			this.btnPageHeaderDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnPageHeaderDefault.Image")));
			this.btnPageHeaderDefault.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPageHeaderDefault.ImageAlign")));
			this.btnPageHeaderDefault.ImageIndex = ((int)(resources.GetObject("btnPageHeaderDefault.ImageIndex")));
			this.btnPageHeaderDefault.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPageHeaderDefault.ImeMode")));
			this.btnPageHeaderDefault.Location = ((System.Drawing.Point)(resources.GetObject("btnPageHeaderDefault.Location")));
			this.btnPageHeaderDefault.Name = "btnPageHeaderDefault";
			this.btnPageHeaderDefault.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPageHeaderDefault.RightToLeft")));
			this.btnPageHeaderDefault.Size = ((System.Drawing.Size)(resources.GetObject("btnPageHeaderDefault.Size")));
			this.btnPageHeaderDefault.TabIndex = ((int)(resources.GetObject("btnPageHeaderDefault.TabIndex")));
			this.btnPageHeaderDefault.Text = resources.GetString("btnPageHeaderDefault.Text");
			this.btnPageHeaderDefault.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPageHeaderDefault.TextAlign")));
			this.btnPageHeaderDefault.Visible = ((bool)(resources.GetObject("btnPageHeaderDefault.Visible")));
			this.btnPageHeaderDefault.Click += new System.EventHandler(this.ButtonDefaultFont_Click);
			// 
			// btnPageHeader
			// 
			this.btnPageHeader.AccessibleDescription = resources.GetString("btnPageHeader.AccessibleDescription");
			this.btnPageHeader.AccessibleName = resources.GetString("btnPageHeader.AccessibleName");
			this.btnPageHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPageHeader.Anchor")));
			this.btnPageHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPageHeader.BackgroundImage")));
			this.btnPageHeader.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPageHeader.Dock")));
			this.btnPageHeader.Enabled = ((bool)(resources.GetObject("btnPageHeader.Enabled")));
			this.btnPageHeader.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPageHeader.FlatStyle")));
			this.btnPageHeader.Font = ((System.Drawing.Font)(resources.GetObject("btnPageHeader.Font")));
			this.btnPageHeader.Image = ((System.Drawing.Image)(resources.GetObject("btnPageHeader.Image")));
			this.btnPageHeader.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPageHeader.ImageAlign")));
			this.btnPageHeader.ImageIndex = ((int)(resources.GetObject("btnPageHeader.ImageIndex")));
			this.btnPageHeader.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPageHeader.ImeMode")));
			this.btnPageHeader.Location = ((System.Drawing.Point)(resources.GetObject("btnPageHeader.Location")));
			this.btnPageHeader.Name = "btnPageHeader";
			this.btnPageHeader.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPageHeader.RightToLeft")));
			this.btnPageHeader.Size = ((System.Drawing.Size)(resources.GetObject("btnPageHeader.Size")));
			this.btnPageHeader.TabIndex = ((int)(resources.GetObject("btnPageHeader.TabIndex")));
			this.btnPageHeader.Text = resources.GetString("btnPageHeader.Text");
			this.btnPageHeader.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPageHeader.TextAlign")));
			this.btnPageHeader.Visible = ((bool)(resources.GetObject("btnPageHeader.Visible")));
			this.btnPageHeader.Click += new System.EventHandler(this.ButtonFont_Click);
			// 
			// grpDetail
			// 
			this.grpDetail.AccessibleDescription = resources.GetString("grpDetail.AccessibleDescription");
			this.grpDetail.AccessibleName = resources.GetString("grpDetail.AccessibleName");
			this.grpDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpDetail.Anchor")));
			this.grpDetail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpDetail.BackgroundImage")));
			this.grpDetail.Controls.Add(this.btnDetailDefault);
			this.grpDetail.Controls.Add(this.btnDetail);
			this.grpDetail.Controls.Add(this.txtDetailFont);
			this.grpDetail.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpDetail.Dock")));
			this.grpDetail.Enabled = ((bool)(resources.GetObject("grpDetail.Enabled")));
			this.grpDetail.Font = ((System.Drawing.Font)(resources.GetObject("grpDetail.Font")));
			this.grpDetail.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpDetail.ImeMode")));
			this.grpDetail.Location = ((System.Drawing.Point)(resources.GetObject("grpDetail.Location")));
			this.grpDetail.Name = "grpDetail";
			this.grpDetail.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpDetail.RightToLeft")));
			this.grpDetail.Size = ((System.Drawing.Size)(resources.GetObject("grpDetail.Size")));
			this.grpDetail.TabIndex = ((int)(resources.GetObject("grpDetail.TabIndex")));
			this.grpDetail.TabStop = false;
			this.grpDetail.Text = resources.GetString("grpDetail.Text");
			this.grpDetail.Visible = ((bool)(resources.GetObject("grpDetail.Visible")));
			// 
			// btnDetailDefault
			// 
			this.btnDetailDefault.AccessibleDescription = resources.GetString("btnDetailDefault.AccessibleDescription");
			this.btnDetailDefault.AccessibleName = resources.GetString("btnDetailDefault.AccessibleName");
			this.btnDetailDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDetailDefault.Anchor")));
			this.btnDetailDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDetailDefault.BackgroundImage")));
			this.btnDetailDefault.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDetailDefault.Dock")));
			this.btnDetailDefault.Enabled = ((bool)(resources.GetObject("btnDetailDefault.Enabled")));
			this.btnDetailDefault.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDetailDefault.FlatStyle")));
			this.btnDetailDefault.Font = ((System.Drawing.Font)(resources.GetObject("btnDetailDefault.Font")));
			this.btnDetailDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnDetailDefault.Image")));
			this.btnDetailDefault.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDetailDefault.ImageAlign")));
			this.btnDetailDefault.ImageIndex = ((int)(resources.GetObject("btnDetailDefault.ImageIndex")));
			this.btnDetailDefault.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDetailDefault.ImeMode")));
			this.btnDetailDefault.Location = ((System.Drawing.Point)(resources.GetObject("btnDetailDefault.Location")));
			this.btnDetailDefault.Name = "btnDetailDefault";
			this.btnDetailDefault.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDetailDefault.RightToLeft")));
			this.btnDetailDefault.Size = ((System.Drawing.Size)(resources.GetObject("btnDetailDefault.Size")));
			this.btnDetailDefault.TabIndex = ((int)(resources.GetObject("btnDetailDefault.TabIndex")));
			this.btnDetailDefault.Text = resources.GetString("btnDetailDefault.Text");
			this.btnDetailDefault.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDetailDefault.TextAlign")));
			this.btnDetailDefault.Visible = ((bool)(resources.GetObject("btnDetailDefault.Visible")));
			this.btnDetailDefault.Click += new System.EventHandler(this.ButtonDefaultFont_Click);
			// 
			// btnDetail
			// 
			this.btnDetail.AccessibleDescription = resources.GetString("btnDetail.AccessibleDescription");
			this.btnDetail.AccessibleName = resources.GetString("btnDetail.AccessibleName");
			this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDetail.Anchor")));
			this.btnDetail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDetail.BackgroundImage")));
			this.btnDetail.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDetail.Dock")));
			this.btnDetail.Enabled = ((bool)(resources.GetObject("btnDetail.Enabled")));
			this.btnDetail.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDetail.FlatStyle")));
			this.btnDetail.Font = ((System.Drawing.Font)(resources.GetObject("btnDetail.Font")));
			this.btnDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnDetail.Image")));
			this.btnDetail.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDetail.ImageAlign")));
			this.btnDetail.ImageIndex = ((int)(resources.GetObject("btnDetail.ImageIndex")));
			this.btnDetail.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDetail.ImeMode")));
			this.btnDetail.Location = ((System.Drawing.Point)(resources.GetObject("btnDetail.Location")));
			this.btnDetail.Name = "btnDetail";
			this.btnDetail.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDetail.RightToLeft")));
			this.btnDetail.Size = ((System.Drawing.Size)(resources.GetObject("btnDetail.Size")));
			this.btnDetail.TabIndex = ((int)(resources.GetObject("btnDetail.TabIndex")));
			this.btnDetail.Text = resources.GetString("btnDetail.Text");
			this.btnDetail.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDetail.TextAlign")));
			this.btnDetail.Visible = ((bool)(resources.GetObject("btnDetail.Visible")));
			this.btnDetail.Click += new System.EventHandler(this.ButtonFont_Click);
			// 
			// txtDetailFont
			// 
			this.txtDetailFont.AccessibleDescription = resources.GetString("txtDetailFont.AccessibleDescription");
			this.txtDetailFont.AccessibleName = resources.GetString("txtDetailFont.AccessibleName");
			this.txtDetailFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtDetailFont.Anchor")));
			this.txtDetailFont.AutoSize = ((bool)(resources.GetObject("txtDetailFont.AutoSize")));
			this.txtDetailFont.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtDetailFont.BackgroundImage")));
			this.txtDetailFont.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtDetailFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtDetailFont.Dock")));
			this.txtDetailFont.Enabled = ((bool)(resources.GetObject("txtDetailFont.Enabled")));
			this.txtDetailFont.Font = ((System.Drawing.Font)(resources.GetObject("txtDetailFont.Font")));
			this.txtDetailFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtDetailFont.ImeMode")));
			this.txtDetailFont.Location = ((System.Drawing.Point)(resources.GetObject("txtDetailFont.Location")));
			this.txtDetailFont.MaxLength = ((int)(resources.GetObject("txtDetailFont.MaxLength")));
			this.txtDetailFont.Multiline = ((bool)(resources.GetObject("txtDetailFont.Multiline")));
			this.txtDetailFont.Name = "txtDetailFont";
			this.txtDetailFont.PasswordChar = ((char)(resources.GetObject("txtDetailFont.PasswordChar")));
			this.txtDetailFont.ReadOnly = true;
			this.txtDetailFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtDetailFont.RightToLeft")));
			this.txtDetailFont.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtDetailFont.ScrollBars")));
			this.txtDetailFont.Size = ((System.Drawing.Size)(resources.GetObject("txtDetailFont.Size")));
			this.txtDetailFont.TabIndex = ((int)(resources.GetObject("txtDetailFont.TabIndex")));
			this.txtDetailFont.TabStop = false;
			this.txtDetailFont.Text = resources.GetString("txtDetailFont.Text");
			this.txtDetailFont.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtDetailFont.TextAlign")));
			this.txtDetailFont.Visible = ((bool)(resources.GetObject("txtDetailFont.Visible")));
			this.txtDetailFont.WordWrap = ((bool)(resources.GetObject("txtDetailFont.WordWrap")));
			// 
			// grpGroupBy
			// 
			this.grpGroupBy.AccessibleDescription = resources.GetString("grpGroupBy.AccessibleDescription");
			this.grpGroupBy.AccessibleName = resources.GetString("grpGroupBy.AccessibleName");
			this.grpGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpGroupBy.Anchor")));
			this.grpGroupBy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpGroupBy.BackgroundImage")));
			this.grpGroupBy.Controls.Add(this.txtGroupFont);
			this.grpGroupBy.Controls.Add(this.btnGroupByDefault);
			this.grpGroupBy.Controls.Add(this.btnGroupBy);
			this.grpGroupBy.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpGroupBy.Dock")));
			this.grpGroupBy.Enabled = ((bool)(resources.GetObject("grpGroupBy.Enabled")));
			this.grpGroupBy.Font = ((System.Drawing.Font)(resources.GetObject("grpGroupBy.Font")));
			this.grpGroupBy.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpGroupBy.ImeMode")));
			this.grpGroupBy.Location = ((System.Drawing.Point)(resources.GetObject("grpGroupBy.Location")));
			this.grpGroupBy.Name = "grpGroupBy";
			this.grpGroupBy.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpGroupBy.RightToLeft")));
			this.grpGroupBy.Size = ((System.Drawing.Size)(resources.GetObject("grpGroupBy.Size")));
			this.grpGroupBy.TabIndex = ((int)(resources.GetObject("grpGroupBy.TabIndex")));
			this.grpGroupBy.TabStop = false;
			this.grpGroupBy.Text = resources.GetString("grpGroupBy.Text");
			this.grpGroupBy.Visible = ((bool)(resources.GetObject("grpGroupBy.Visible")));
			// 
			// txtGroupFont
			// 
			this.txtGroupFont.AccessibleDescription = resources.GetString("txtGroupFont.AccessibleDescription");
			this.txtGroupFont.AccessibleName = resources.GetString("txtGroupFont.AccessibleName");
			this.txtGroupFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtGroupFont.Anchor")));
			this.txtGroupFont.AutoSize = ((bool)(resources.GetObject("txtGroupFont.AutoSize")));
			this.txtGroupFont.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtGroupFont.BackgroundImage")));
			this.txtGroupFont.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtGroupFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtGroupFont.Dock")));
			this.txtGroupFont.Enabled = ((bool)(resources.GetObject("txtGroupFont.Enabled")));
			this.txtGroupFont.Font = ((System.Drawing.Font)(resources.GetObject("txtGroupFont.Font")));
			this.txtGroupFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtGroupFont.ImeMode")));
			this.txtGroupFont.Location = ((System.Drawing.Point)(resources.GetObject("txtGroupFont.Location")));
			this.txtGroupFont.MaxLength = ((int)(resources.GetObject("txtGroupFont.MaxLength")));
			this.txtGroupFont.Multiline = ((bool)(resources.GetObject("txtGroupFont.Multiline")));
			this.txtGroupFont.Name = "txtGroupFont";
			this.txtGroupFont.PasswordChar = ((char)(resources.GetObject("txtGroupFont.PasswordChar")));
			this.txtGroupFont.ReadOnly = true;
			this.txtGroupFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtGroupFont.RightToLeft")));
			this.txtGroupFont.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtGroupFont.ScrollBars")));
			this.txtGroupFont.Size = ((System.Drawing.Size)(resources.GetObject("txtGroupFont.Size")));
			this.txtGroupFont.TabIndex = ((int)(resources.GetObject("txtGroupFont.TabIndex")));
			this.txtGroupFont.TabStop = false;
			this.txtGroupFont.Text = resources.GetString("txtGroupFont.Text");
			this.txtGroupFont.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtGroupFont.TextAlign")));
			this.txtGroupFont.Visible = ((bool)(resources.GetObject("txtGroupFont.Visible")));
			this.txtGroupFont.WordWrap = ((bool)(resources.GetObject("txtGroupFont.WordWrap")));
			// 
			// btnGroupByDefault
			// 
			this.btnGroupByDefault.AccessibleDescription = resources.GetString("btnGroupByDefault.AccessibleDescription");
			this.btnGroupByDefault.AccessibleName = resources.GetString("btnGroupByDefault.AccessibleName");
			this.btnGroupByDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnGroupByDefault.Anchor")));
			this.btnGroupByDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGroupByDefault.BackgroundImage")));
			this.btnGroupByDefault.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnGroupByDefault.Dock")));
			this.btnGroupByDefault.Enabled = ((bool)(resources.GetObject("btnGroupByDefault.Enabled")));
			this.btnGroupByDefault.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnGroupByDefault.FlatStyle")));
			this.btnGroupByDefault.Font = ((System.Drawing.Font)(resources.GetObject("btnGroupByDefault.Font")));
			this.btnGroupByDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnGroupByDefault.Image")));
			this.btnGroupByDefault.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnGroupByDefault.ImageAlign")));
			this.btnGroupByDefault.ImageIndex = ((int)(resources.GetObject("btnGroupByDefault.ImageIndex")));
			this.btnGroupByDefault.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnGroupByDefault.ImeMode")));
			this.btnGroupByDefault.Location = ((System.Drawing.Point)(resources.GetObject("btnGroupByDefault.Location")));
			this.btnGroupByDefault.Name = "btnGroupByDefault";
			this.btnGroupByDefault.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnGroupByDefault.RightToLeft")));
			this.btnGroupByDefault.Size = ((System.Drawing.Size)(resources.GetObject("btnGroupByDefault.Size")));
			this.btnGroupByDefault.TabIndex = ((int)(resources.GetObject("btnGroupByDefault.TabIndex")));
			this.btnGroupByDefault.Text = resources.GetString("btnGroupByDefault.Text");
			this.btnGroupByDefault.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnGroupByDefault.TextAlign")));
			this.btnGroupByDefault.Visible = ((bool)(resources.GetObject("btnGroupByDefault.Visible")));
			this.btnGroupByDefault.Click += new System.EventHandler(this.ButtonDefaultFont_Click);
			// 
			// btnGroupBy
			// 
			this.btnGroupBy.AccessibleDescription = resources.GetString("btnGroupBy.AccessibleDescription");
			this.btnGroupBy.AccessibleName = resources.GetString("btnGroupBy.AccessibleName");
			this.btnGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnGroupBy.Anchor")));
			this.btnGroupBy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGroupBy.BackgroundImage")));
			this.btnGroupBy.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnGroupBy.Dock")));
			this.btnGroupBy.Enabled = ((bool)(resources.GetObject("btnGroupBy.Enabled")));
			this.btnGroupBy.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnGroupBy.FlatStyle")));
			this.btnGroupBy.Font = ((System.Drawing.Font)(resources.GetObject("btnGroupBy.Font")));
			this.btnGroupBy.Image = ((System.Drawing.Image)(resources.GetObject("btnGroupBy.Image")));
			this.btnGroupBy.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnGroupBy.ImageAlign")));
			this.btnGroupBy.ImageIndex = ((int)(resources.GetObject("btnGroupBy.ImageIndex")));
			this.btnGroupBy.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnGroupBy.ImeMode")));
			this.btnGroupBy.Location = ((System.Drawing.Point)(resources.GetObject("btnGroupBy.Location")));
			this.btnGroupBy.Name = "btnGroupBy";
			this.btnGroupBy.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnGroupBy.RightToLeft")));
			this.btnGroupBy.Size = ((System.Drawing.Size)(resources.GetObject("btnGroupBy.Size")));
			this.btnGroupBy.TabIndex = ((int)(resources.GetObject("btnGroupBy.TabIndex")));
			this.btnGroupBy.Text = resources.GetString("btnGroupBy.Text");
			this.btnGroupBy.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnGroupBy.TextAlign")));
			this.btnGroupBy.Visible = ((bool)(resources.GetObject("btnGroupBy.Visible")));
			this.btnGroupBy.Click += new System.EventHandler(this.ButtonFont_Click);
			// 
			// grpParameter
			// 
			this.grpParameter.AccessibleDescription = resources.GetString("grpParameter.AccessibleDescription");
			this.grpParameter.AccessibleName = resources.GetString("grpParameter.AccessibleName");
			this.grpParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpParameter.Anchor")));
			this.grpParameter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpParameter.BackgroundImage")));
			this.grpParameter.Controls.Add(this.txtParamFont);
			this.grpParameter.Controls.Add(this.btnParaDefault);
			this.grpParameter.Controls.Add(this.btnParameterFont);
			this.grpParameter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpParameter.Dock")));
			this.grpParameter.Enabled = ((bool)(resources.GetObject("grpParameter.Enabled")));
			this.grpParameter.Font = ((System.Drawing.Font)(resources.GetObject("grpParameter.Font")));
			this.grpParameter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpParameter.ImeMode")));
			this.grpParameter.Location = ((System.Drawing.Point)(resources.GetObject("grpParameter.Location")));
			this.grpParameter.Name = "grpParameter";
			this.grpParameter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpParameter.RightToLeft")));
			this.grpParameter.Size = ((System.Drawing.Size)(resources.GetObject("grpParameter.Size")));
			this.grpParameter.TabIndex = ((int)(resources.GetObject("grpParameter.TabIndex")));
			this.grpParameter.TabStop = false;
			this.grpParameter.Text = resources.GetString("grpParameter.Text");
			this.grpParameter.Visible = ((bool)(resources.GetObject("grpParameter.Visible")));
			// 
			// txtParamFont
			// 
			this.txtParamFont.AccessibleDescription = resources.GetString("txtParamFont.AccessibleDescription");
			this.txtParamFont.AccessibleName = resources.GetString("txtParamFont.AccessibleName");
			this.txtParamFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtParamFont.Anchor")));
			this.txtParamFont.AutoSize = ((bool)(resources.GetObject("txtParamFont.AutoSize")));
			this.txtParamFont.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtParamFont.BackgroundImage")));
			this.txtParamFont.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtParamFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtParamFont.Dock")));
			this.txtParamFont.Enabled = ((bool)(resources.GetObject("txtParamFont.Enabled")));
			this.txtParamFont.Font = ((System.Drawing.Font)(resources.GetObject("txtParamFont.Font")));
			this.txtParamFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtParamFont.ImeMode")));
			this.txtParamFont.Location = ((System.Drawing.Point)(resources.GetObject("txtParamFont.Location")));
			this.txtParamFont.MaxLength = ((int)(resources.GetObject("txtParamFont.MaxLength")));
			this.txtParamFont.Multiline = ((bool)(resources.GetObject("txtParamFont.Multiline")));
			this.txtParamFont.Name = "txtParamFont";
			this.txtParamFont.PasswordChar = ((char)(resources.GetObject("txtParamFont.PasswordChar")));
			this.txtParamFont.ReadOnly = true;
			this.txtParamFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtParamFont.RightToLeft")));
			this.txtParamFont.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtParamFont.ScrollBars")));
			this.txtParamFont.Size = ((System.Drawing.Size)(resources.GetObject("txtParamFont.Size")));
			this.txtParamFont.TabIndex = ((int)(resources.GetObject("txtParamFont.TabIndex")));
			this.txtParamFont.TabStop = false;
			this.txtParamFont.Text = resources.GetString("txtParamFont.Text");
			this.txtParamFont.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtParamFont.TextAlign")));
			this.txtParamFont.Visible = ((bool)(resources.GetObject("txtParamFont.Visible")));
			this.txtParamFont.WordWrap = ((bool)(resources.GetObject("txtParamFont.WordWrap")));
			// 
			// btnParaDefault
			// 
			this.btnParaDefault.AccessibleDescription = resources.GetString("btnParaDefault.AccessibleDescription");
			this.btnParaDefault.AccessibleName = resources.GetString("btnParaDefault.AccessibleName");
			this.btnParaDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnParaDefault.Anchor")));
			this.btnParaDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnParaDefault.BackgroundImage")));
			this.btnParaDefault.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnParaDefault.Dock")));
			this.btnParaDefault.Enabled = ((bool)(resources.GetObject("btnParaDefault.Enabled")));
			this.btnParaDefault.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnParaDefault.FlatStyle")));
			this.btnParaDefault.Font = ((System.Drawing.Font)(resources.GetObject("btnParaDefault.Font")));
			this.btnParaDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnParaDefault.Image")));
			this.btnParaDefault.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnParaDefault.ImageAlign")));
			this.btnParaDefault.ImageIndex = ((int)(resources.GetObject("btnParaDefault.ImageIndex")));
			this.btnParaDefault.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnParaDefault.ImeMode")));
			this.btnParaDefault.Location = ((System.Drawing.Point)(resources.GetObject("btnParaDefault.Location")));
			this.btnParaDefault.Name = "btnParaDefault";
			this.btnParaDefault.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnParaDefault.RightToLeft")));
			this.btnParaDefault.Size = ((System.Drawing.Size)(resources.GetObject("btnParaDefault.Size")));
			this.btnParaDefault.TabIndex = ((int)(resources.GetObject("btnParaDefault.TabIndex")));
			this.btnParaDefault.Text = resources.GetString("btnParaDefault.Text");
			this.btnParaDefault.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnParaDefault.TextAlign")));
			this.btnParaDefault.Visible = ((bool)(resources.GetObject("btnParaDefault.Visible")));
			this.btnParaDefault.Click += new System.EventHandler(this.ButtonDefaultFont_Click);
			// 
			// btnParameterFont
			// 
			this.btnParameterFont.AccessibleDescription = resources.GetString("btnParameterFont.AccessibleDescription");
			this.btnParameterFont.AccessibleName = resources.GetString("btnParameterFont.AccessibleName");
			this.btnParameterFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnParameterFont.Anchor")));
			this.btnParameterFont.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnParameterFont.BackgroundImage")));
			this.btnParameterFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnParameterFont.Dock")));
			this.btnParameterFont.Enabled = ((bool)(resources.GetObject("btnParameterFont.Enabled")));
			this.btnParameterFont.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnParameterFont.FlatStyle")));
			this.btnParameterFont.Font = ((System.Drawing.Font)(resources.GetObject("btnParameterFont.Font")));
			this.btnParameterFont.Image = ((System.Drawing.Image)(resources.GetObject("btnParameterFont.Image")));
			this.btnParameterFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnParameterFont.ImageAlign")));
			this.btnParameterFont.ImageIndex = ((int)(resources.GetObject("btnParameterFont.ImageIndex")));
			this.btnParameterFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnParameterFont.ImeMode")));
			this.btnParameterFont.Location = ((System.Drawing.Point)(resources.GetObject("btnParameterFont.Location")));
			this.btnParameterFont.Name = "btnParameterFont";
			this.btnParameterFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnParameterFont.RightToLeft")));
			this.btnParameterFont.Size = ((System.Drawing.Size)(resources.GetObject("btnParameterFont.Size")));
			this.btnParameterFont.TabIndex = ((int)(resources.GetObject("btnParameterFont.TabIndex")));
			this.btnParameterFont.Text = resources.GetString("btnParameterFont.Text");
			this.btnParameterFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnParameterFont.TextAlign")));
			this.btnParameterFont.Visible = ((bool)(resources.GetObject("btnParameterFont.Visible")));
			this.btnParameterFont.Click += new System.EventHandler(this.ButtonFont_Click);
			// 
			// grpReportHeader
			// 
			this.grpReportHeader.AccessibleDescription = resources.GetString("grpReportHeader.AccessibleDescription");
			this.grpReportHeader.AccessibleName = resources.GetString("grpReportHeader.AccessibleName");
			this.grpReportHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpReportHeader.Anchor")));
			this.grpReportHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpReportHeader.BackgroundImage")));
			this.grpReportHeader.Controls.Add(this.txtReportHeaderFont);
			this.grpReportHeader.Controls.Add(this.btnReportHeaderDefault);
			this.grpReportHeader.Controls.Add(this.btnReportHeaderFont);
			this.grpReportHeader.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpReportHeader.Dock")));
			this.grpReportHeader.Enabled = ((bool)(resources.GetObject("grpReportHeader.Enabled")));
			this.grpReportHeader.Font = ((System.Drawing.Font)(resources.GetObject("grpReportHeader.Font")));
			this.grpReportHeader.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpReportHeader.ImeMode")));
			this.grpReportHeader.Location = ((System.Drawing.Point)(resources.GetObject("grpReportHeader.Location")));
			this.grpReportHeader.Name = "grpReportHeader";
			this.grpReportHeader.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpReportHeader.RightToLeft")));
			this.grpReportHeader.Size = ((System.Drawing.Size)(resources.GetObject("grpReportHeader.Size")));
			this.grpReportHeader.TabIndex = ((int)(resources.GetObject("grpReportHeader.TabIndex")));
			this.grpReportHeader.TabStop = false;
			this.grpReportHeader.Text = resources.GetString("grpReportHeader.Text");
			this.grpReportHeader.Visible = ((bool)(resources.GetObject("grpReportHeader.Visible")));
			// 
			// txtReportHeaderFont
			// 
			this.txtReportHeaderFont.AccessibleDescription = resources.GetString("txtReportHeaderFont.AccessibleDescription");
			this.txtReportHeaderFont.AccessibleName = resources.GetString("txtReportHeaderFont.AccessibleName");
			this.txtReportHeaderFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtReportHeaderFont.Anchor")));
			this.txtReportHeaderFont.AutoSize = ((bool)(resources.GetObject("txtReportHeaderFont.AutoSize")));
			this.txtReportHeaderFont.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtReportHeaderFont.BackgroundImage")));
			this.txtReportHeaderFont.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtReportHeaderFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtReportHeaderFont.Dock")));
			this.txtReportHeaderFont.Enabled = ((bool)(resources.GetObject("txtReportHeaderFont.Enabled")));
			this.txtReportHeaderFont.Font = ((System.Drawing.Font)(resources.GetObject("txtReportHeaderFont.Font")));
			this.txtReportHeaderFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtReportHeaderFont.ImeMode")));
			this.txtReportHeaderFont.Location = ((System.Drawing.Point)(resources.GetObject("txtReportHeaderFont.Location")));
			this.txtReportHeaderFont.MaxLength = ((int)(resources.GetObject("txtReportHeaderFont.MaxLength")));
			this.txtReportHeaderFont.Multiline = ((bool)(resources.GetObject("txtReportHeaderFont.Multiline")));
			this.txtReportHeaderFont.Name = "txtReportHeaderFont";
			this.txtReportHeaderFont.PasswordChar = ((char)(resources.GetObject("txtReportHeaderFont.PasswordChar")));
			this.txtReportHeaderFont.ReadOnly = true;
			this.txtReportHeaderFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtReportHeaderFont.RightToLeft")));
			this.txtReportHeaderFont.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtReportHeaderFont.ScrollBars")));
			this.txtReportHeaderFont.Size = ((System.Drawing.Size)(resources.GetObject("txtReportHeaderFont.Size")));
			this.txtReportHeaderFont.TabIndex = ((int)(resources.GetObject("txtReportHeaderFont.TabIndex")));
			this.txtReportHeaderFont.TabStop = false;
			this.txtReportHeaderFont.Text = resources.GetString("txtReportHeaderFont.Text");
			this.txtReportHeaderFont.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtReportHeaderFont.TextAlign")));
			this.txtReportHeaderFont.Visible = ((bool)(resources.GetObject("txtReportHeaderFont.Visible")));
			this.txtReportHeaderFont.WordWrap = ((bool)(resources.GetObject("txtReportHeaderFont.WordWrap")));
			// 
			// btnReportHeaderDefault
			// 
			this.btnReportHeaderDefault.AccessibleDescription = resources.GetString("btnReportHeaderDefault.AccessibleDescription");
			this.btnReportHeaderDefault.AccessibleName = resources.GetString("btnReportHeaderDefault.AccessibleName");
			this.btnReportHeaderDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnReportHeaderDefault.Anchor")));
			this.btnReportHeaderDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReportHeaderDefault.BackgroundImage")));
			this.btnReportHeaderDefault.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnReportHeaderDefault.Dock")));
			this.btnReportHeaderDefault.Enabled = ((bool)(resources.GetObject("btnReportHeaderDefault.Enabled")));
			this.btnReportHeaderDefault.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnReportHeaderDefault.FlatStyle")));
			this.btnReportHeaderDefault.Font = ((System.Drawing.Font)(resources.GetObject("btnReportHeaderDefault.Font")));
			this.btnReportHeaderDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnReportHeaderDefault.Image")));
			this.btnReportHeaderDefault.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnReportHeaderDefault.ImageAlign")));
			this.btnReportHeaderDefault.ImageIndex = ((int)(resources.GetObject("btnReportHeaderDefault.ImageIndex")));
			this.btnReportHeaderDefault.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnReportHeaderDefault.ImeMode")));
			this.btnReportHeaderDefault.Location = ((System.Drawing.Point)(resources.GetObject("btnReportHeaderDefault.Location")));
			this.btnReportHeaderDefault.Name = "btnReportHeaderDefault";
			this.btnReportHeaderDefault.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnReportHeaderDefault.RightToLeft")));
			this.btnReportHeaderDefault.Size = ((System.Drawing.Size)(resources.GetObject("btnReportHeaderDefault.Size")));
			this.btnReportHeaderDefault.TabIndex = ((int)(resources.GetObject("btnReportHeaderDefault.TabIndex")));
			this.btnReportHeaderDefault.Text = resources.GetString("btnReportHeaderDefault.Text");
			this.btnReportHeaderDefault.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnReportHeaderDefault.TextAlign")));
			this.btnReportHeaderDefault.Visible = ((bool)(resources.GetObject("btnReportHeaderDefault.Visible")));
			this.btnReportHeaderDefault.Click += new System.EventHandler(this.ButtonDefaultFont_Click);
			// 
			// btnReportHeaderFont
			// 
			this.btnReportHeaderFont.AccessibleDescription = resources.GetString("btnReportHeaderFont.AccessibleDescription");
			this.btnReportHeaderFont.AccessibleName = resources.GetString("btnReportHeaderFont.AccessibleName");
			this.btnReportHeaderFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnReportHeaderFont.Anchor")));
			this.btnReportHeaderFont.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReportHeaderFont.BackgroundImage")));
			this.btnReportHeaderFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnReportHeaderFont.Dock")));
			this.btnReportHeaderFont.Enabled = ((bool)(resources.GetObject("btnReportHeaderFont.Enabled")));
			this.btnReportHeaderFont.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnReportHeaderFont.FlatStyle")));
			this.btnReportHeaderFont.Font = ((System.Drawing.Font)(resources.GetObject("btnReportHeaderFont.Font")));
			this.btnReportHeaderFont.Image = ((System.Drawing.Image)(resources.GetObject("btnReportHeaderFont.Image")));
			this.btnReportHeaderFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnReportHeaderFont.ImageAlign")));
			this.btnReportHeaderFont.ImageIndex = ((int)(resources.GetObject("btnReportHeaderFont.ImageIndex")));
			this.btnReportHeaderFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnReportHeaderFont.ImeMode")));
			this.btnReportHeaderFont.Location = ((System.Drawing.Point)(resources.GetObject("btnReportHeaderFont.Location")));
			this.btnReportHeaderFont.Name = "btnReportHeaderFont";
			this.btnReportHeaderFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnReportHeaderFont.RightToLeft")));
			this.btnReportHeaderFont.Size = ((System.Drawing.Size)(resources.GetObject("btnReportHeaderFont.Size")));
			this.btnReportHeaderFont.TabIndex = ((int)(resources.GetObject("btnReportHeaderFont.TabIndex")));
			this.btnReportHeaderFont.Text = resources.GetString("btnReportHeaderFont.Text");
			this.btnReportHeaderFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnReportHeaderFont.TextAlign")));
			this.btnReportHeaderFont.Visible = ((bool)(resources.GetObject("btnReportHeaderFont.Visible")));
			this.btnReportHeaderFont.Click += new System.EventHandler(this.ButtonFont_Click);
			// 
			// btnClose
			// 
			this.btnClose.AccessibleDescription = resources.GetString("btnClose.AccessibleDescription");
			this.btnClose.AccessibleName = resources.GetString("btnClose.AccessibleName");
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnClose.Anchor")));
			this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnClose.Dock")));
			this.btnClose.Enabled = ((bool)(resources.GetObject("btnClose.Enabled")));
			this.btnClose.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnClose.FlatStyle")));
			this.btnClose.Font = ((System.Drawing.Font)(resources.GetObject("btnClose.Font")));
			this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
			this.btnClose.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClose.ImageAlign")));
			this.btnClose.ImageIndex = ((int)(resources.GetObject("btnClose.ImageIndex")));
			this.btnClose.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnClose.ImeMode")));
			this.btnClose.Location = ((System.Drawing.Point)(resources.GetObject("btnClose.Location")));
			this.btnClose.Name = "btnClose";
			this.btnClose.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnClose.RightToLeft")));
			this.btnClose.Size = ((System.Drawing.Size)(resources.GetObject("btnClose.Size")));
			this.btnClose.TabIndex = ((int)(resources.GetObject("btnClose.TabIndex")));
			this.btnClose.Text = resources.GetString("btnClose.Text");
			this.btnClose.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClose.TextAlign")));
			this.btnClose.Visible = ((bool)(resources.GetObject("btnClose.Visible")));
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnDefault
			// 
			this.btnDefault.AccessibleDescription = resources.GetString("btnDefault.AccessibleDescription");
			this.btnDefault.AccessibleName = resources.GetString("btnDefault.AccessibleName");
			this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDefault.Anchor")));
			this.btnDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDefault.BackgroundImage")));
			this.btnDefault.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDefault.Dock")));
			this.btnDefault.Enabled = ((bool)(resources.GetObject("btnDefault.Enabled")));
			this.btnDefault.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDefault.FlatStyle")));
			this.btnDefault.Font = ((System.Drawing.Font)(resources.GetObject("btnDefault.Font")));
			this.btnDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnDefault.Image")));
			this.btnDefault.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDefault.ImageAlign")));
			this.btnDefault.ImageIndex = ((int)(resources.GetObject("btnDefault.ImageIndex")));
			this.btnDefault.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDefault.ImeMode")));
			this.btnDefault.Location = ((System.Drawing.Point)(resources.GetObject("btnDefault.Location")));
			this.btnDefault.Name = "btnDefault";
			this.btnDefault.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDefault.RightToLeft")));
			this.btnDefault.Size = ((System.Drawing.Size)(resources.GetObject("btnDefault.Size")));
			this.btnDefault.TabIndex = ((int)(resources.GetObject("btnDefault.TabIndex")));
			this.btnDefault.Text = resources.GetString("btnDefault.Text");
			this.btnDefault.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDefault.TextAlign")));
			this.btnDefault.Visible = ((bool)(resources.GetObject("btnDefault.Visible")));
			this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
			// 
			// btnSave
			// 
			this.btnSave.AccessibleDescription = resources.GetString("btnSave.AccessibleDescription");
			this.btnSave.AccessibleName = resources.GetString("btnSave.AccessibleName");
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSave.Anchor")));
			this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
			this.btnSave.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSave.Dock")));
			this.btnSave.Enabled = ((bool)(resources.GetObject("btnSave.Enabled")));
			this.btnSave.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSave.FlatStyle")));
			this.btnSave.Font = ((System.Drawing.Font)(resources.GetObject("btnSave.Font")));
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSave.ImageAlign")));
			this.btnSave.ImageIndex = ((int)(resources.GetObject("btnSave.ImageIndex")));
			this.btnSave.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSave.ImeMode")));
			this.btnSave.Location = ((System.Drawing.Point)(resources.GetObject("btnSave.Location")));
			this.btnSave.Name = "btnSave";
			this.btnSave.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSave.RightToLeft")));
			this.btnSave.Size = ((System.Drawing.Size)(resources.GetObject("btnSave.Size")));
			this.btnSave.TabIndex = ((int)(resources.GetObject("btnSave.TabIndex")));
			this.btnSave.Text = resources.GetString("btnSave.Text");
			this.btnSave.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSave.TextAlign")));
			this.btnSave.Visible = ((bool)(resources.GetObject("btnSave.Visible")));
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.AccessibleDescription = resources.GetString("btnHelp.AccessibleDescription");
			this.btnHelp.AccessibleName = resources.GetString("btnHelp.AccessibleName");
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnHelp.Anchor")));
			this.btnHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHelp.BackgroundImage")));
			this.btnHelp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnHelp.Dock")));
			this.btnHelp.Enabled = ((bool)(resources.GetObject("btnHelp.Enabled")));
			this.btnHelp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnHelp.FlatStyle")));
			this.btnHelp.Font = ((System.Drawing.Font)(resources.GetObject("btnHelp.Font")));
			this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
			this.btnHelp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.ImageAlign")));
			this.btnHelp.ImageIndex = ((int)(resources.GetObject("btnHelp.ImageIndex")));
			this.btnHelp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnHelp.ImeMode")));
			this.btnHelp.Location = ((System.Drawing.Point)(resources.GetObject("btnHelp.Location")));
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnHelp.RightToLeft")));
			this.btnHelp.Size = ((System.Drawing.Size)(resources.GetObject("btnHelp.Size")));
			this.btnHelp.TabIndex = ((int)(resources.GetObject("btnHelp.TabIndex")));
			this.btnHelp.Text = resources.GetString("btnHelp.Text");
			this.btnHelp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.TextAlign")));
			this.btnHelp.Visible = ((bool)(resources.GetObject("btnHelp.Visible")));
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// dlgFonts
			// 
			this.dlgFonts.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			// 
			// lblDefaultDetailFont
			// 
			this.lblDefaultDetailFont.AccessibleDescription = resources.GetString("lblDefaultDetailFont.AccessibleDescription");
			this.lblDefaultDetailFont.AccessibleName = resources.GetString("lblDefaultDetailFont.AccessibleName");
			this.lblDefaultDetailFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultDetailFont.Anchor")));
			this.lblDefaultDetailFont.AutoSize = ((bool)(resources.GetObject("lblDefaultDetailFont.AutoSize")));
			this.lblDefaultDetailFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultDetailFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultDetailFont.Dock")));
			this.lblDefaultDetailFont.Enabled = ((bool)(resources.GetObject("lblDefaultDetailFont.Enabled")));
			this.lblDefaultDetailFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultDetailFont.Font")));
			this.lblDefaultDetailFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultDetailFont.Image")));
			this.lblDefaultDetailFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultDetailFont.ImageAlign")));
			this.lblDefaultDetailFont.ImageIndex = ((int)(resources.GetObject("lblDefaultDetailFont.ImageIndex")));
			this.lblDefaultDetailFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultDetailFont.ImeMode")));
			this.lblDefaultDetailFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultDetailFont.Location")));
			this.lblDefaultDetailFont.Name = "lblDefaultDetailFont";
			this.lblDefaultDetailFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultDetailFont.RightToLeft")));
			this.lblDefaultDetailFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultDetailFont.Size")));
			this.lblDefaultDetailFont.TabIndex = ((int)(resources.GetObject("lblDefaultDetailFont.TabIndex")));
			this.lblDefaultDetailFont.Text = resources.GetString("lblDefaultDetailFont.Text");
			this.lblDefaultDetailFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultDetailFont.TextAlign")));
			this.lblDefaultDetailFont.Visible = ((bool)(resources.GetObject("lblDefaultDetailFont.Visible")));
			// 
			// lblDefaultReportFooterFont
			// 
			this.lblDefaultReportFooterFont.AccessibleDescription = resources.GetString("lblDefaultReportFooterFont.AccessibleDescription");
			this.lblDefaultReportFooterFont.AccessibleName = resources.GetString("lblDefaultReportFooterFont.AccessibleName");
			this.lblDefaultReportFooterFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultReportFooterFont.Anchor")));
			this.lblDefaultReportFooterFont.AutoSize = ((bool)(resources.GetObject("lblDefaultReportFooterFont.AutoSize")));
			this.lblDefaultReportFooterFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultReportFooterFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultReportFooterFont.Dock")));
			this.lblDefaultReportFooterFont.Enabled = ((bool)(resources.GetObject("lblDefaultReportFooterFont.Enabled")));
			this.lblDefaultReportFooterFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultReportFooterFont.Font")));
			this.lblDefaultReportFooterFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultReportFooterFont.Image")));
			this.lblDefaultReportFooterFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultReportFooterFont.ImageAlign")));
			this.lblDefaultReportFooterFont.ImageIndex = ((int)(resources.GetObject("lblDefaultReportFooterFont.ImageIndex")));
			this.lblDefaultReportFooterFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultReportFooterFont.ImeMode")));
			this.lblDefaultReportFooterFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultReportFooterFont.Location")));
			this.lblDefaultReportFooterFont.Name = "lblDefaultReportFooterFont";
			this.lblDefaultReportFooterFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultReportFooterFont.RightToLeft")));
			this.lblDefaultReportFooterFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultReportFooterFont.Size")));
			this.lblDefaultReportFooterFont.TabIndex = ((int)(resources.GetObject("lblDefaultReportFooterFont.TabIndex")));
			this.lblDefaultReportFooterFont.Text = resources.GetString("lblDefaultReportFooterFont.Text");
			this.lblDefaultReportFooterFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultReportFooterFont.TextAlign")));
			this.lblDefaultReportFooterFont.Visible = ((bool)(resources.GetObject("lblDefaultReportFooterFont.Visible")));
			// 
			// lblDefaultPageFooterFont
			// 
			this.lblDefaultPageFooterFont.AccessibleDescription = resources.GetString("lblDefaultPageFooterFont.AccessibleDescription");
			this.lblDefaultPageFooterFont.AccessibleName = resources.GetString("lblDefaultPageFooterFont.AccessibleName");
			this.lblDefaultPageFooterFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultPageFooterFont.Anchor")));
			this.lblDefaultPageFooterFont.AutoSize = ((bool)(resources.GetObject("lblDefaultPageFooterFont.AutoSize")));
			this.lblDefaultPageFooterFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultPageFooterFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultPageFooterFont.Dock")));
			this.lblDefaultPageFooterFont.Enabled = ((bool)(resources.GetObject("lblDefaultPageFooterFont.Enabled")));
			this.lblDefaultPageFooterFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultPageFooterFont.Font")));
			this.lblDefaultPageFooterFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultPageFooterFont.Image")));
			this.lblDefaultPageFooterFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultPageFooterFont.ImageAlign")));
			this.lblDefaultPageFooterFont.ImageIndex = ((int)(resources.GetObject("lblDefaultPageFooterFont.ImageIndex")));
			this.lblDefaultPageFooterFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultPageFooterFont.ImeMode")));
			this.lblDefaultPageFooterFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultPageFooterFont.Location")));
			this.lblDefaultPageFooterFont.Name = "lblDefaultPageFooterFont";
			this.lblDefaultPageFooterFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultPageFooterFont.RightToLeft")));
			this.lblDefaultPageFooterFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultPageFooterFont.Size")));
			this.lblDefaultPageFooterFont.TabIndex = ((int)(resources.GetObject("lblDefaultPageFooterFont.TabIndex")));
			this.lblDefaultPageFooterFont.Text = resources.GetString("lblDefaultPageFooterFont.Text");
			this.lblDefaultPageFooterFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultPageFooterFont.TextAlign")));
			this.lblDefaultPageFooterFont.Visible = ((bool)(resources.GetObject("lblDefaultPageFooterFont.Visible")));
			// 
			// lblDefaultGroupByFont
			// 
			this.lblDefaultGroupByFont.AccessibleDescription = resources.GetString("lblDefaultGroupByFont.AccessibleDescription");
			this.lblDefaultGroupByFont.AccessibleName = resources.GetString("lblDefaultGroupByFont.AccessibleName");
			this.lblDefaultGroupByFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultGroupByFont.Anchor")));
			this.lblDefaultGroupByFont.AutoSize = ((bool)(resources.GetObject("lblDefaultGroupByFont.AutoSize")));
			this.lblDefaultGroupByFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultGroupByFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultGroupByFont.Dock")));
			this.lblDefaultGroupByFont.Enabled = ((bool)(resources.GetObject("lblDefaultGroupByFont.Enabled")));
			this.lblDefaultGroupByFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultGroupByFont.Font")));
			this.lblDefaultGroupByFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultGroupByFont.Image")));
			this.lblDefaultGroupByFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultGroupByFont.ImageAlign")));
			this.lblDefaultGroupByFont.ImageIndex = ((int)(resources.GetObject("lblDefaultGroupByFont.ImageIndex")));
			this.lblDefaultGroupByFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultGroupByFont.ImeMode")));
			this.lblDefaultGroupByFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultGroupByFont.Location")));
			this.lblDefaultGroupByFont.Name = "lblDefaultGroupByFont";
			this.lblDefaultGroupByFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultGroupByFont.RightToLeft")));
			this.lblDefaultGroupByFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultGroupByFont.Size")));
			this.lblDefaultGroupByFont.TabIndex = ((int)(resources.GetObject("lblDefaultGroupByFont.TabIndex")));
			this.lblDefaultGroupByFont.Text = resources.GetString("lblDefaultGroupByFont.Text");
			this.lblDefaultGroupByFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultGroupByFont.TextAlign")));
			this.lblDefaultGroupByFont.Visible = ((bool)(resources.GetObject("lblDefaultGroupByFont.Visible")));
			// 
			// lblDefaultPageHeaderFont
			// 
			this.lblDefaultPageHeaderFont.AccessibleDescription = resources.GetString("lblDefaultPageHeaderFont.AccessibleDescription");
			this.lblDefaultPageHeaderFont.AccessibleName = resources.GetString("lblDefaultPageHeaderFont.AccessibleName");
			this.lblDefaultPageHeaderFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultPageHeaderFont.Anchor")));
			this.lblDefaultPageHeaderFont.AutoSize = ((bool)(resources.GetObject("lblDefaultPageHeaderFont.AutoSize")));
			this.lblDefaultPageHeaderFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultPageHeaderFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultPageHeaderFont.Dock")));
			this.lblDefaultPageHeaderFont.Enabled = ((bool)(resources.GetObject("lblDefaultPageHeaderFont.Enabled")));
			this.lblDefaultPageHeaderFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultPageHeaderFont.Font")));
			this.lblDefaultPageHeaderFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultPageHeaderFont.Image")));
			this.lblDefaultPageHeaderFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultPageHeaderFont.ImageAlign")));
			this.lblDefaultPageHeaderFont.ImageIndex = ((int)(resources.GetObject("lblDefaultPageHeaderFont.ImageIndex")));
			this.lblDefaultPageHeaderFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultPageHeaderFont.ImeMode")));
			this.lblDefaultPageHeaderFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultPageHeaderFont.Location")));
			this.lblDefaultPageHeaderFont.Name = "lblDefaultPageHeaderFont";
			this.lblDefaultPageHeaderFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultPageHeaderFont.RightToLeft")));
			this.lblDefaultPageHeaderFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultPageHeaderFont.Size")));
			this.lblDefaultPageHeaderFont.TabIndex = ((int)(resources.GetObject("lblDefaultPageHeaderFont.TabIndex")));
			this.lblDefaultPageHeaderFont.Text = resources.GetString("lblDefaultPageHeaderFont.Text");
			this.lblDefaultPageHeaderFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultPageHeaderFont.TextAlign")));
			this.lblDefaultPageHeaderFont.Visible = ((bool)(resources.GetObject("lblDefaultPageHeaderFont.Visible")));
			// 
			// lblDefaultReportHeaderFont
			// 
			this.lblDefaultReportHeaderFont.AccessibleDescription = resources.GetString("lblDefaultReportHeaderFont.AccessibleDescription");
			this.lblDefaultReportHeaderFont.AccessibleName = resources.GetString("lblDefaultReportHeaderFont.AccessibleName");
			this.lblDefaultReportHeaderFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultReportHeaderFont.Anchor")));
			this.lblDefaultReportHeaderFont.AutoSize = ((bool)(resources.GetObject("lblDefaultReportHeaderFont.AutoSize")));
			this.lblDefaultReportHeaderFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultReportHeaderFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultReportHeaderFont.Dock")));
			this.lblDefaultReportHeaderFont.Enabled = ((bool)(resources.GetObject("lblDefaultReportHeaderFont.Enabled")));
			this.lblDefaultReportHeaderFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultReportHeaderFont.Font")));
			this.lblDefaultReportHeaderFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultReportHeaderFont.Image")));
			this.lblDefaultReportHeaderFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultReportHeaderFont.ImageAlign")));
			this.lblDefaultReportHeaderFont.ImageIndex = ((int)(resources.GetObject("lblDefaultReportHeaderFont.ImageIndex")));
			this.lblDefaultReportHeaderFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultReportHeaderFont.ImeMode")));
			this.lblDefaultReportHeaderFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultReportHeaderFont.Location")));
			this.lblDefaultReportHeaderFont.Name = "lblDefaultReportHeaderFont";
			this.lblDefaultReportHeaderFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultReportHeaderFont.RightToLeft")));
			this.lblDefaultReportHeaderFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultReportHeaderFont.Size")));
			this.lblDefaultReportHeaderFont.TabIndex = ((int)(resources.GetObject("lblDefaultReportHeaderFont.TabIndex")));
			this.lblDefaultReportHeaderFont.Text = resources.GetString("lblDefaultReportHeaderFont.Text");
			this.lblDefaultReportHeaderFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultReportHeaderFont.TextAlign")));
			this.lblDefaultReportHeaderFont.Visible = ((bool)(resources.GetObject("lblDefaultReportHeaderFont.Visible")));
			// 
			// lblDefaultParameterFont
			// 
			this.lblDefaultParameterFont.AccessibleDescription = resources.GetString("lblDefaultParameterFont.AccessibleDescription");
			this.lblDefaultParameterFont.AccessibleName = resources.GetString("lblDefaultParameterFont.AccessibleName");
			this.lblDefaultParameterFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDefaultParameterFont.Anchor")));
			this.lblDefaultParameterFont.AutoSize = ((bool)(resources.GetObject("lblDefaultParameterFont.AutoSize")));
			this.lblDefaultParameterFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblDefaultParameterFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDefaultParameterFont.Dock")));
			this.lblDefaultParameterFont.Enabled = ((bool)(resources.GetObject("lblDefaultParameterFont.Enabled")));
			this.lblDefaultParameterFont.Font = ((System.Drawing.Font)(resources.GetObject("lblDefaultParameterFont.Font")));
			this.lblDefaultParameterFont.Image = ((System.Drawing.Image)(resources.GetObject("lblDefaultParameterFont.Image")));
			this.lblDefaultParameterFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultParameterFont.ImageAlign")));
			this.lblDefaultParameterFont.ImageIndex = ((int)(resources.GetObject("lblDefaultParameterFont.ImageIndex")));
			this.lblDefaultParameterFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDefaultParameterFont.ImeMode")));
			this.lblDefaultParameterFont.Location = ((System.Drawing.Point)(resources.GetObject("lblDefaultParameterFont.Location")));
			this.lblDefaultParameterFont.Name = "lblDefaultParameterFont";
			this.lblDefaultParameterFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDefaultParameterFont.RightToLeft")));
			this.lblDefaultParameterFont.Size = ((System.Drawing.Size)(resources.GetObject("lblDefaultParameterFont.Size")));
			this.lblDefaultParameterFont.TabIndex = ((int)(resources.GetObject("lblDefaultParameterFont.TabIndex")));
			this.lblDefaultParameterFont.Text = resources.GetString("lblDefaultParameterFont.Text");
			this.lblDefaultParameterFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDefaultParameterFont.TextAlign")));
			this.lblDefaultParameterFont.Visible = ((bool)(resources.GetObject("lblDefaultParameterFont.Visible")));
			// 
			// PageSetup
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnClose;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.lblDefaultParameterFont);
			this.Controls.Add(this.lblDefaultReportHeaderFont);
			this.Controls.Add(this.lblDefaultPageHeaderFont);
			this.Controls.Add(this.lblDefaultGroupByFont);
			this.Controls.Add(this.lblDefaultPageFooterFont);
			this.Controls.Add(this.lblDefaultReportFooterFont);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDefault);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lblDefaultDetailFont);
			this.Controls.Add(this.tabPageSetup);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "PageSetup";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.PageSetup_Closing);
			this.Load += new System.EventHandler(this.PageSetup_Load);
			this.tabPageSetup.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.grpMargins.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nudMarginBottom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMarginRight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMarginGutter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMarginLeft)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMarginTop)).EndInit();
			this.tabFonts.ResumeLayout(false);
			this.grpReportFooter.ResumeLayout(false);
			this.grpPageFooter.ResumeLayout(false);
			this.grpPageHeader.ResumeLayout(false);
			this.grpDetail.ResumeLayout(false);
			this.grpGroupBy.ResumeLayout(false);
			this.grpParameter.ResumeLayout(false);
			this.grpReportHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#region Form Events

		private void PageSetup_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".PageSetup_Load()";
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
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				#endregion
				

				// set title for form
				this.Text = this.Text + this.mvoSysReport.ReportName + Constants.CLOSE_SBRACKET;

				// load all value to setup
				RefreshFormViewFromReportVO();							

				blnIsEditing = false;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		private void tabGeneral_Click(object sender, EventArgs e)
		{// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.WaitCursor;
#endregion Code Inserted Automatically

// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

		}

		private void tabFonts_Click(object sender, EventArgs e)
		{// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.WaitCursor;
#endregion Code Inserted Automatically

// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

		}

		private void btnFieldProperties_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnFieldProperties_Click()";
			try
			{
				FieldProperties frmFieldProperties = new FieldProperties();
				frmFieldProperties.VoReport = this.mvoSysReport;
				frmFieldProperties.ShowDialog();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnDefault_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDefault_Click()";
			try
			{
				// display confirm message
				DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIRM_SET_DEFAULT, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
				if (dlgResult == DialogResult.Yes)
				{
					#region default margins

					mvoSysReport.MarginTop = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_TOP / 2);
					mvoSysReport.MarginBottom = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_BOTTOM / 2);
					mvoSysReport.MarginLeft = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_LEFT / 2);
					mvoSysReport.MarginRight = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_RIGHT / 2);
					mvoSysReport.MarginGutter = 0;
					mvoSysReport.MarginGutterPos = Convert.ToBoolean(Constants.REPORT_DEFAULT_GUTTER_POSITION);

					mvoSysReport.Orientation = Constants.REPORT_DEFAULT_ORIENTATION;
					mvoSysReport.PaperSize = (int)PaperKind.A4;	//Constants.REPORT_DEFAULT_PAPER_SIZE = 0;
					mvoSysReport.TableBorder = Constants.REPORT_DEFAULT_TABLE_BORDER;
					mvoSysReport.Signatures = string.Empty;//Constants.REPORT_DEFAULT_SIGNATURE;

					#endregion

					#region default fonts
		
					mvoSysReport.FontReportHeader = GetSelectedFont(lblDefaultReportHeaderFont.Font);
                    mvoSysReport.FontReportFooter = GetSelectedFont(lblDefaultReportFooterFont.Font);
					mvoSysReport.FontParameter = GetSelectedFont(lblDefaultParameterFont.Font);
					mvoSysReport.FontPageHeader = GetSelectedFont(lblDefaultPageHeaderFont.Font);
					mvoSysReport.FontGroupBy = GetSelectedFont(lblDefaultGroupByFont.Font);
					mvoSysReport.FontDetail = GetSelectedFont(lblDefaultDetailFont.Font);
					mvoSysReport.FontPageFooter = GetSelectedFont(lblDefaultPageFooterFont.Font);					

					#endregion

					// save changes
					boEditReport.Update(mvoSysReport);

					RefreshFormViewFromReportVO();
					blnIsEditing = false;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				// validate signature in right format
				if (txtSignatures.Text.Trim() != string.Empty)
				{
					if (!ValidateSignature(txtSignatures.Text.Trim()))
					{
						string[] strMsg = new string[2];
						strMsg[0] = sys_ReportTable.SIGNATURES_FLD;
						strMsg[1] = "Title#(sign, name)";
						// displays the error message.
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_FORMAT, MessageBoxIcon.Error, strMsg);
						txtSignatures.Focus();
						// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

						return;
					}
				}

				if (SaveData())
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
					blnIsEditing = false;
				}
				else
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxIcon.Error);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			// close the form
			this.Close();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.WaitCursor;
#endregion Code Inserted Automatically

// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

		}

		private void ButtonFont_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".ButtonFont_Click()";
			try
			{
				if (dlgFonts.ShowDialog() == DialogResult.OK)
				{
					// make sure that user select exist font
					if ((dlgFonts.Font.Name != string.Empty) && (!dlgFonts.FontMustExist))
					{
						Button btn = (Button) sender;
						btn.Tag = dlgFonts.Font;
						if (btn.Equals(btnReportHeaderFont))
							txtReportHeaderFont.Text = GetSelectedFont(dlgFonts.Font);
						if (btn.Equals(btnParameterFont))
							txtParamFont.Text = GetSelectedFont(dlgFonts.Font);
						if (btn.Equals(btnPageHeader))
							txtPageHeaderFont.Text = GetSelectedFont(dlgFonts.Font);
						if (btn.Equals(btnGroupBy))
							txtGroupFont.Text = GetSelectedFont(dlgFonts.Font);
						if (btn.Equals(btnDetail))
							txtDetailFont.Text = GetSelectedFont(dlgFonts.Font);
						if (btn.Equals(btnPageFooter))
							txtPageFooterFont.Text = GetSelectedFont(dlgFonts.Font);
						if (btn.Equals(btnReportFooter))
							txtReportFooterFont.Text = GetSelectedFont(dlgFonts.Font);
						// reset default font for Fonts Dialog
						dlgFonts.Font = DefaultFont;
						blnIsEditing = true;
					}
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void ButtonDefaultFont_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDefault_Click()";
			try
			{
				Button btnClicked = (Button)sender;
				// display confirm message
				// TODO: Thachnn: Replace with the new error code
				DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_SETUP_DEFAULT_FONT, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

				if (dlgResult == DialogResult.Yes)
				{
					/// HACKED: Thachnn: set default font style as described report convention
					if (btnClicked == btnReportHeaderDefault)
					{
						mvoSysReport.FontReportHeader = GetSelectedFont(lblDefaultReportHeaderFont.Font);
						txtReportHeaderFont.Text = GetSelectedFont(lblDefaultReportHeaderFont.Font);
					}
					if (btnClicked == btnReportFooterDefault)
					{
						mvoSysReport.FontReportFooter = GetSelectedFont(lblDefaultReportFooterFont.Font);
						txtReportFooterFont.Text = GetSelectedFont(lblDefaultReportFooterFont.Font);
					}
					if (btnClicked == btnParaDefault)
					{
						mvoSysReport.FontParameter = GetSelectedFont(lblDefaultParameterFont.Font);
						txtParamFont.Text = GetSelectedFont(lblDefaultParameterFont.Font);
					}
					if (btnClicked == btnPageHeaderDefault)
					{
						mvoSysReport.FontPageHeader = GetSelectedFont(lblDefaultPageHeaderFont.Font);
						txtPageHeaderFont.Text = GetSelectedFont(lblDefaultPageHeaderFont.Font);
					}
					if (btnClicked == btnGroupByDefault)
					{
						mvoSysReport.FontGroupBy = GetSelectedFont(lblDefaultGroupByFont.Font);
						txtGroupFont.Text = GetSelectedFont(lblDefaultGroupByFont.Font);
					}
					if (btnClicked == btnDetailDefault)
					{
						mvoSysReport.FontDetail = GetSelectedFont(lblDefaultDetailFont.Font);
						txtDetailFont.Text = GetSelectedFont(lblDefaultDetailFont.Font);
					}
					if (btnClicked == btnPageFooterDefault)
					{
						mvoSysReport.FontPageFooter = GetSelectedFont(lblDefaultPageFooterFont.Font);
						txtPageFooterFont.Text = GetSelectedFont(lblDefaultPageFooterFont.Font);
					}
					// save changes
					boEditReport.Update(mvoSysReport);
					blnIsEditing = false;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		
		private void PageSetup_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".PageSetup_Closing()";
			try
			{
				if (blnIsEditing)
				{					
					DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
					switch (dlgResult)
					{
						case DialogResult.Yes:
							try
							{
								if (!SaveData())
									e.Cancel = true;
							}
							catch
							{
								e.Cancel = true;
							}
							break;
						case DialogResult.No:
							e.Cancel = false;
							break;
						case DialogResult.Cancel:
							e.Cancel = true;
							break;
					}
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

		private void nudMarginTop_ValueChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".nudMarginTop_ValueChanged()";
			try
			{
				blnIsEditing = true;
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

		private void cboGutterPosition_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboGutterPosition_SelectedIndexChanged()";
			try
			{
				blnIsEditing = true;
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

		private void txtSignatures_TextChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtSignatures_TextChanged()";
			try
			{
				blnIsEditing = true;
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
	
		#endregion

		#region private methods
		/// <summary>
		/// Save object to database
		/// </summary>
		/// <returns>true if succeed | false if failure</returns>
		private bool SaveData()
		{
			bool blnIsOK = false;
			try
			{
				// prepare data
				try
				{
					mvoSysReport.MarginTop = FormControlComponents.ConvertIncheToTwips((decimal)nudMarginTop.Value);
				}
				catch
				{
					mvoSysReport.MarginTop = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_TOP);	
				}
				try
				{
					mvoSysReport.MarginBottom = FormControlComponents.ConvertIncheToTwips((decimal)nudMarginBottom.Value);	
				}
				catch
				{
					mvoSysReport.MarginBottom = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_BOTTOM);	
				}

				try
				{
					mvoSysReport.MarginLeft = FormControlComponents.ConvertIncheToTwips((decimal)nudMarginLeft.Value);
				}
				catch
				{
					mvoSysReport.MarginLeft = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_LEFT);
				}
				try
				{
					mvoSysReport.MarginRight = FormControlComponents.ConvertIncheToTwips((decimal)nudMarginRight.Value);
				}
				catch
				{
					mvoSysReport.MarginRight = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_RIGHT);
				}
				try
				{
					mvoSysReport.MarginGutter = FormControlComponents.ConvertIncheToTwips((decimal)nudMarginGutter.Value);
				}
				catch
				{
					mvoSysReport.MarginGutter = 0;
				}
				try
				{
					mvoSysReport.MarginGutterPos = Convert.ToBoolean(cboGutterPosition.SelectedIndex);
				}
				catch
				{
					mvoSysReport.MarginGutterPos = Constants.REPORT_DEFAULT_GUTTER_POSITION;
				}
				try
				{
					mvoSysReport.Orientation = Convert.ToBoolean(cboOrientation.SelectedIndex);
				}
				catch
				{
					mvoSysReport.Orientation = Constants.REPORT_DEFAULT_ORIENTATION;
				}

				if (cboPaperSize.SelectedValue != null)
				{
					mvoSysReport.PaperSize = (int)cboPaperSize.SelectedValue;
				}
				else 
				{
					mvoSysReport.PaperSize = (int)PaperKind.A4;
				}

				if(cboTable.SelectedIndex > 0)
				{
					mvoSysReport.TableBorder = cboTable.SelectedIndex;
				}
				else
				{
					mvoSysReport.TableBorder = Constants.REPORT_DEFAULT_TABLE_BORDER;
				}

				mvoSysReport.Signatures = txtSignatures.Text;

				#region Fonts

				// font
				// font will be store in database as following format
				// "Name|Size|FontStyle|GdiCharSet|GdiVerticalFont|GraphicsUnit"
				Font objFont;
				string strSelectedFont = string.Empty;

				// if cannot cast Tag property to Font object
				// it mean user did not made any change to font then we do not update font
				try
				{
					objFont = (Font)btnReportHeaderFont.Tag;
					strSelectedFont = this.GetSelectedFont(objFont);
					mvoSysReport.FontReportHeader = strSelectedFont;
				}
				catch{}

				try
				{
					objFont = (Font)btnPageHeader.Tag;
					strSelectedFont = this.GetSelectedFont(objFont);
					mvoSysReport.FontPageHeader = strSelectedFont;
				}
				catch{}

				try
				{
					objFont = (Font)btnParameterFont.Tag;
					strSelectedFont = this.GetSelectedFont(objFont);
					mvoSysReport.FontParameter = strSelectedFont;
				}
				catch{}

				try
				{
					objFont = (Font)btnGroupBy.Tag;
					strSelectedFont = this.GetSelectedFont(objFont);
					mvoSysReport.FontGroupBy = strSelectedFont;
				}
				catch{}

				try
				{
					objFont = (Font)btnDetail.Tag;
					strSelectedFont = this.GetSelectedFont(objFont);
					mvoSysReport.FontDetail = strSelectedFont;
				}
				catch{}

				try
				{
					objFont = (Font)btnPageFooter.Tag;
					strSelectedFont = this.GetSelectedFont(objFont);
					mvoSysReport.FontPageFooter = strSelectedFont;
				}
				catch{}

				try
				{
					objFont = (Font)btnReportFooter.Tag;
					strSelectedFont = this.GetSelectedFont(objFont);
					mvoSysReport.FontReportFooter = strSelectedFont;
				}
				catch{}

				#endregion

				// save changes
				boEditReport.Update(mvoSysReport);
				blnIsOK = true;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return blnIsOK;
		}
		private string GetSelectedFont(Font pobjFont)
		{
			try
			{
				StringBuilder strSelectedFont = new StringBuilder();
				strSelectedFont.Append(pobjFont.Name).Append(FONT_SEPARATOR).Append(pobjFont.Size).Append(FONT_SEPARATOR);
				strSelectedFont.Append(pobjFont.Style).Append(FONT_SEPARATOR).Append(pobjFont.GdiCharSet.ToString()).Append(FONT_SEPARATOR);
				strSelectedFont.Append(pobjFont.GdiVerticalFont).Append(FONT_SEPARATOR).Append(pobjFont.Unit);
				return strSelectedFont.ToString();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	
		/// <summary>
		/// Thachnn: 11/10/2005
		/// Get all properties of ReportVO to layout on the form's component
		/// </summary>
		private void RefreshFormViewFromReportVO()
		{
			string METHOD_NAME = THIS + ".RefreshFormViewFromReportVO()";
			try
			{
				nudMarginTop.Value = FormControlComponents.ConvertTwipsToInches(mvoSysReport.MarginTop);
				nudMarginBottom.Value = FormControlComponents.ConvertTwipsToInches(mvoSysReport.MarginBottom);
				nudMarginLeft.Value = FormControlComponents.ConvertTwipsToInches(mvoSysReport.MarginLeft);
				nudMarginRight.Value = FormControlComponents.ConvertTwipsToInches(mvoSysReport.MarginRight);
				nudMarginGutter.Value = FormControlComponents.ConvertTwipsToInches(mvoSysReport.MarginGutter);
				
				// if GutterPost is true then select Top, else select Left
				try
				{
					cboGutterPosition.SelectedIndex = Convert.ToInt32(mvoSysReport.MarginGutterPos);
				}
				catch
				{
					cboGutterPosition.SelectedIndex = Convert.ToInt32(Constants.REPORT_DEFAULT_GUTTER_POSITION);
				}
				// if Orientation is true then select Landscape, else select Potrait
				try
				{
					cboOrientation.SelectedIndex = Convert.ToInt32(mvoSysReport.Orientation);
				}
				catch
				{
					cboOrientation.SelectedIndex = Convert.ToInt32(Constants.REPORT_DEFAULT_ORIENTATION);
				}

				cboPaperSize.SelectedValue = mvoSysReport.PaperSize;
				cboTable.SelectedIndex = mvoSysReport.TableBorder;
				txtSignatures.Text = mvoSysReport.Signatures;

				// assign font tab
				// get font object then assign to  Tag property
				btnReportHeaderFont.Tag = txtReportHeaderFont.Text = mvoSysReport.FontReportHeader;
				btnPageHeader.Tag = txtPageHeaderFont.Text = mvoSysReport.FontPageHeader;
				btnParameterFont.Tag = txtParamFont.Text = mvoSysReport.FontParameter;
				btnGroupBy.Tag = txtGroupFont.Text = mvoSysReport.FontGroupBy;
				btnDetail.Tag = txtDetailFont.Text = mvoSysReport.FontDetail;
				btnPageFooter.Tag = txtPageFooterFont.Text = mvoSysReport.FontPageFooter;
				btnReportFooter.Tag = txtReportFooterFont.Text = mvoSysReport.FontReportFooter;
			}
			catch (Exception ex)
			{
				/// TODO: Define ErrorCode or Display Message Here
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
		/// Validate signature in right format
		/// </summary>
		/// <param name="pstrSignatures">Signature to validate</param>
		/// <returns>true if valid, false if invalid</returns>
		private bool ValidateSignature(string pstrSignatures)
		{
			ArrayList arrSigns = new ArrayList();
			// each line in signatures will be one signature
			string[] strSigns = pstrSignatures.Split("\r\n".ToCharArray());
			foreach (string strSign in strSigns)
			{
				if (strSign.Trim() != string.Empty)
					arrSigns.Add(strSign);
			}
			arrSigns.TrimToSize();
			for (int i = 0; i < arrSigns.Count; i++)
			{
				if (arrSigns[i].ToString().Split(Constants.VIEW_TABLE_ITEM_SEPARATOR).Length < 2)
					return false;
			}
			return true;
		}
		#endregion

		private void btnMarginDefault_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnMarginDefault_Click()";
			try
			{
				// display confirm message
				DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_DEFAULT_MARGIN, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
				if (dlgResult == DialogResult.Yes)
				{
					#region default margins

					mvoSysReport.MarginTop = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_TOP / 2);
					mvoSysReport.MarginBottom = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_BOTTOM / 2);
					mvoSysReport.MarginLeft = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_LEFT / 2);
					mvoSysReport.MarginRight = FormControlComponents.ConvertIncheToTwips(Constants.REPORT_DEFAULT_RIGHT / 2);
					mvoSysReport.MarginGutter = 0;
					mvoSysReport.MarginGutterPos = Convert.ToBoolean(Constants.REPORT_DEFAULT_GUTTER_POSITION);

					/// TODO: Use value in Constant class
					mvoSysReport.Orientation = Constants.REPORT_DEFAULT_ORIENTATION;
					mvoSysReport.PaperSize = (int)PaperKind.A4;	//Constants.REPORT_DEFAULT_PAPER_SIZE = 0;
					mvoSysReport.TableBorder = Constants.REPORT_DEFAULT_TABLE_BORDER;
					mvoSysReport.Signatures = "";//Constants.REPORT_DEFAULT_SIGNATURE;

					#endregion				

					// save changes
					boEditReport.Update(mvoSysReport);

					RefreshFormViewFromReportVO();
					blnIsEditing = false;
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
	}


	/// <summary>
	/// Class helper, help to display Text and Value data on combo
	/// Provide 2 public properties: DisplayMember and ValueMember for ComboBox control to access
	/// Use with cboPaperSize in this form
	/// </summary>
	class ClscboPaperSizeData
	{
		private string mstrDisplayMember;
		private int mintValueMember;

		public string DisplayMember
		{
			get
			{
				return mstrDisplayMember;
			}
			set
			{
				mstrDisplayMember = value;
			}	
		}
		
		public int ValueMember
		{
			get
			{
				return mintValueMember;
			}
			set
			{
				mintValueMember = value;
			}	
		}

		
		public ClscboPaperSizeData(string pstrDisplayMember,int pintValueMember)
		{
			this.mstrDisplayMember = pstrDisplayMember;
			this.mintValueMember = pintValueMember;
		}

		public ClscboPaperSizeData(PaperKind pEnum)
		{			
			this.mstrDisplayMember = pEnum.ToString();
			this.mintValueMember = (int)pEnum;
		}
	}

}