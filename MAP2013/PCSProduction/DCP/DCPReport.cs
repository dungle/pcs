using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1List;
using PCSComProduction.DCP.BO;
using PCSComProduction.DCP.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using C1.C1Report;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for DCPReport.
	/// </summary>
	public class DCPReport : Form
	{
		#region controls and variables
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		private const string THIS = "PCSProduction.DCP.DCPReport";
		private UtilsBO boUtils = new UtilsBO();
		private DCPReportBO boDCPReport = new DCPReportBO();
		private Panel pnControls;
		private Button btnClose;
		private Button btnHelp;
		private Button btnViewReport;
		private TextBox txtModel;
		private TextBox txtPartName;
		private TextBox txtPartNumber;
		private TextBox txtCategory;
		private TextBox txtWC;
		private TextBox txtCycle;
		private Button btnPartName;
		private Button btnPartNumber;
		private Button btnCategory;
		private Label lblModel;
		private Label lblPartName;
		private Label lblPartNumber;
		private Label lblCategory;
		private Label lblYear;
		private Label lblMonth;
		private Label lblWorkCenter;
		private Button btnSearchCycle;
		private Label lblCycle;
		private C1Combo cboCCN;
		private Label lblCCN;
		private ToolBarButton c1pBtnClosePPV;
		private DCPReportVO voDCPReport = null;
		const string PARTNUMBER_COLUMNNAME = "PartNumber";
		const string TYPE_COLUMNNAME = "Type";
		string mstrReportDefFolder = Application.StartupPath + "\\" +Constants.REPORT_DEFINITION_STORE_LOCATION;
		private ComboBox cboMonth;
		private ComboBox cboYear;
		const string LAYOUT_FILENAME = "DCPReport.xml";
		private System.Windows.Forms.Button btnWC;
		private System.Windows.Forms.TextBox txtShift;
		private System.Windows.Forms.Label lblShift;
		private System.Windows.Forms.Button btnShift;
		const string DCP_REPORT = "DCP Report";

		#endregion

		#region constants
		#endregion


		#region HACKED: Thachnn: 24/01/2006: public some control on form to call and control DCPReport form other form
		

		/// <summary>
		///  public the txtCycle TextBox
		/// </summary>
		public TextBox TxtCycle
		{
			get
			{
				return txtCycle;
			}
			set
			{
				txtCycle = value;
			}
		}

		/// <summary>
		///  public the TxtProductionLine TextBox
		/// </summary>
		public TextBox TxtProductionLine
		{
			get
			{
				return txtWC;
			}
			set
			{
				txtWC = value;
			}
		}
		
		/// <summary>
		///  public the txtCategory TextBox
		/// </summary>
		public TextBox TxtCategory
		{
			get
			{
				return txtCategory;
			}
			set
			{
				txtCategory = value;
			}
		}

		/// <summary>
		///  public the txtPartNumber TextBox
		/// </summary>
		public TextBox TxtPartNumber
		{
			get
			{
				return txtPartNumber;
			}
			set
			{
				txtPartNumber = value;
			}
		}

		/// <summary>
		///  public the txtPartName TextBox
		/// </summary>
		public TextBox TxtPartName
		{
			get
			{
				return txtPartName;
			}
			set
			{
				txtPartName = value;
			}
		}
		
		/// <summary>
		///  public the cboCCN combo box
		/// </summary>
		public C1Combo CboCCN
		{
			get
			{
				return cboCCN;
			}
			set
			{
				cboCCN = value;
			}
		}
		


		#endregion HACKED: Thachnn: 24/01/2006: public some control on form to call and control DCPReport form other form


		private enum RowType
		{
			Delivery = 1,
			Produce = 2,
			Inventory = 3
		}

		public DCPReport()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DCPReport));
			this.pnControls = new System.Windows.Forms.Panel();
			this.btnShift = new System.Windows.Forms.Button();
			this.txtShift = new System.Windows.Forms.TextBox();
			this.lblShift = new System.Windows.Forms.Label();
			this.cboYear = new System.Windows.Forms.ComboBox();
			this.cboMonth = new System.Windows.Forms.ComboBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnViewReport = new System.Windows.Forms.Button();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.txtPartName = new System.Windows.Forms.TextBox();
			this.txtPartNumber = new System.Windows.Forms.TextBox();
			this.txtCategory = new System.Windows.Forms.TextBox();
			this.txtWC = new System.Windows.Forms.TextBox();
			this.txtCycle = new System.Windows.Forms.TextBox();
			this.btnPartName = new System.Windows.Forms.Button();
			this.btnPartNumber = new System.Windows.Forms.Button();
			this.btnCategory = new System.Windows.Forms.Button();
			this.lblModel = new System.Windows.Forms.Label();
			this.lblPartName = new System.Windows.Forms.Label();
			this.lblPartNumber = new System.Windows.Forms.Label();
			this.lblCategory = new System.Windows.Forms.Label();
			this.lblYear = new System.Windows.Forms.Label();
			this.lblMonth = new System.Windows.Forms.Label();
			this.btnWC = new System.Windows.Forms.Button();
			this.lblWorkCenter = new System.Windows.Forms.Label();
			this.btnSearchCycle = new System.Windows.Forms.Button();
			this.lblCycle = new System.Windows.Forms.Label();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.c1pBtnClosePPV = new System.Windows.Forms.ToolBarButton();
			this.pnControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			this.SuspendLayout();
			// 
			// pnControls
			// 
			this.pnControls.AccessibleDescription = resources.GetString("pnControls.AccessibleDescription");
			this.pnControls.AccessibleName = resources.GetString("pnControls.AccessibleName");
			this.pnControls.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnControls.Anchor")));
			this.pnControls.AutoScroll = ((bool)(resources.GetObject("pnControls.AutoScroll")));
			this.pnControls.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnControls.AutoScrollMargin")));
			this.pnControls.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnControls.AutoScrollMinSize")));
			this.pnControls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnControls.BackgroundImage")));
			this.pnControls.Controls.Add(this.btnShift);
			this.pnControls.Controls.Add(this.txtShift);
			this.pnControls.Controls.Add(this.lblShift);
			this.pnControls.Controls.Add(this.cboYear);
			this.pnControls.Controls.Add(this.cboMonth);
			this.pnControls.Controls.Add(this.btnClose);
			this.pnControls.Controls.Add(this.btnHelp);
			this.pnControls.Controls.Add(this.btnViewReport);
			this.pnControls.Controls.Add(this.txtModel);
			this.pnControls.Controls.Add(this.txtPartName);
			this.pnControls.Controls.Add(this.txtPartNumber);
			this.pnControls.Controls.Add(this.txtCategory);
			this.pnControls.Controls.Add(this.txtWC);
			this.pnControls.Controls.Add(this.txtCycle);
			this.pnControls.Controls.Add(this.btnPartName);
			this.pnControls.Controls.Add(this.btnPartNumber);
			this.pnControls.Controls.Add(this.btnCategory);
			this.pnControls.Controls.Add(this.lblModel);
			this.pnControls.Controls.Add(this.lblPartName);
			this.pnControls.Controls.Add(this.lblPartNumber);
			this.pnControls.Controls.Add(this.lblCategory);
			this.pnControls.Controls.Add(this.lblYear);
			this.pnControls.Controls.Add(this.lblMonth);
			this.pnControls.Controls.Add(this.btnWC);
			this.pnControls.Controls.Add(this.lblWorkCenter);
			this.pnControls.Controls.Add(this.btnSearchCycle);
			this.pnControls.Controls.Add(this.lblCycle);
			this.pnControls.Controls.Add(this.cboCCN);
			this.pnControls.Controls.Add(this.lblCCN);
			this.pnControls.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnControls.Dock")));
			this.pnControls.Enabled = ((bool)(resources.GetObject("pnControls.Enabled")));
			this.pnControls.Font = ((System.Drawing.Font)(resources.GetObject("pnControls.Font")));
			this.pnControls.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnControls.ImeMode")));
			this.pnControls.Location = ((System.Drawing.Point)(resources.GetObject("pnControls.Location")));
			this.pnControls.Name = "pnControls";
			this.pnControls.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnControls.RightToLeft")));
			this.pnControls.Size = ((System.Drawing.Size)(resources.GetObject("pnControls.Size")));
			this.pnControls.TabIndex = ((int)(resources.GetObject("pnControls.TabIndex")));
			this.pnControls.Text = resources.GetString("pnControls.Text");
			this.pnControls.Visible = ((bool)(resources.GetObject("pnControls.Visible")));
			// 
			// btnShift
			// 
			this.btnShift.AccessibleDescription = resources.GetString("btnShift.AccessibleDescription");
			this.btnShift.AccessibleName = resources.GetString("btnShift.AccessibleName");
			this.btnShift.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnShift.Anchor")));
			this.btnShift.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShift.BackgroundImage")));
			this.btnShift.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnShift.Dock")));
			this.btnShift.Enabled = ((bool)(resources.GetObject("btnShift.Enabled")));
			this.btnShift.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnShift.FlatStyle")));
			this.btnShift.Font = ((System.Drawing.Font)(resources.GetObject("btnShift.Font")));
			this.btnShift.Image = ((System.Drawing.Image)(resources.GetObject("btnShift.Image")));
			this.btnShift.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnShift.ImageAlign")));
			this.btnShift.ImageIndex = ((int)(resources.GetObject("btnShift.ImageIndex")));
			this.btnShift.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnShift.ImeMode")));
			this.btnShift.Location = ((System.Drawing.Point)(resources.GetObject("btnShift.Location")));
			this.btnShift.Name = "btnShift";
			this.btnShift.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnShift.RightToLeft")));
			this.btnShift.Size = ((System.Drawing.Size)(resources.GetObject("btnShift.Size")));
			this.btnShift.TabIndex = ((int)(resources.GetObject("btnShift.TabIndex")));
			this.btnShift.Text = resources.GetString("btnShift.Text");
			this.btnShift.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnShift.TextAlign")));
			this.btnShift.Visible = ((bool)(resources.GetObject("btnShift.Visible")));
			this.btnShift.Click += new System.EventHandler(this.btnShift_Click);
			// 
			// txtShift
			// 
			this.txtShift.AccessibleDescription = resources.GetString("txtShift.AccessibleDescription");
			this.txtShift.AccessibleName = resources.GetString("txtShift.AccessibleName");
			this.txtShift.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtShift.Anchor")));
			this.txtShift.AutoSize = ((bool)(resources.GetObject("txtShift.AutoSize")));
			this.txtShift.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtShift.BackgroundImage")));
			this.txtShift.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtShift.Dock")));
			this.txtShift.Enabled = ((bool)(resources.GetObject("txtShift.Enabled")));
			this.txtShift.Font = ((System.Drawing.Font)(resources.GetObject("txtShift.Font")));
			this.txtShift.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtShift.ImeMode")));
			this.txtShift.Location = ((System.Drawing.Point)(resources.GetObject("txtShift.Location")));
			this.txtShift.MaxLength = ((int)(resources.GetObject("txtShift.MaxLength")));
			this.txtShift.Multiline = ((bool)(resources.GetObject("txtShift.Multiline")));
			this.txtShift.Name = "txtShift";
			this.txtShift.PasswordChar = ((char)(resources.GetObject("txtShift.PasswordChar")));
			this.txtShift.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtShift.RightToLeft")));
			this.txtShift.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtShift.ScrollBars")));
			this.txtShift.Size = ((System.Drawing.Size)(resources.GetObject("txtShift.Size")));
			this.txtShift.TabIndex = ((int)(resources.GetObject("txtShift.TabIndex")));
			this.txtShift.Text = resources.GetString("txtShift.Text");
			this.txtShift.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtShift.TextAlign")));
			this.txtShift.Visible = ((bool)(resources.GetObject("txtShift.Visible")));
			this.txtShift.WordWrap = ((bool)(resources.GetObject("txtShift.WordWrap")));
			// 
			// lblShift
			// 
			this.lblShift.AccessibleDescription = resources.GetString("lblShift.AccessibleDescription");
			this.lblShift.AccessibleName = resources.GetString("lblShift.AccessibleName");
			this.lblShift.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblShift.Anchor")));
			this.lblShift.AutoSize = ((bool)(resources.GetObject("lblShift.AutoSize")));
			this.lblShift.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblShift.Dock")));
			this.lblShift.Enabled = ((bool)(resources.GetObject("lblShift.Enabled")));
			this.lblShift.Font = ((System.Drawing.Font)(resources.GetObject("lblShift.Font")));
			this.lblShift.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblShift.Image = ((System.Drawing.Image)(resources.GetObject("lblShift.Image")));
			this.lblShift.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblShift.ImageAlign")));
			this.lblShift.ImageIndex = ((int)(resources.GetObject("lblShift.ImageIndex")));
			this.lblShift.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblShift.ImeMode")));
			this.lblShift.Location = ((System.Drawing.Point)(resources.GetObject("lblShift.Location")));
			this.lblShift.Name = "lblShift";
			this.lblShift.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblShift.RightToLeft")));
			this.lblShift.Size = ((System.Drawing.Size)(resources.GetObject("lblShift.Size")));
			this.lblShift.TabIndex = ((int)(resources.GetObject("lblShift.TabIndex")));
			this.lblShift.Text = resources.GetString("lblShift.Text");
			this.lblShift.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblShift.TextAlign")));
			this.lblShift.Visible = ((bool)(resources.GetObject("lblShift.Visible")));
			// 
			// cboYear
			// 
			this.cboYear.AccessibleDescription = resources.GetString("cboYear.AccessibleDescription");
			this.cboYear.AccessibleName = resources.GetString("cboYear.AccessibleName");
			this.cboYear.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboYear.Anchor")));
			this.cboYear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboYear.BackgroundImage")));
			this.cboYear.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboYear.Dock")));
			this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboYear.Enabled = ((bool)(resources.GetObject("cboYear.Enabled")));
			this.cboYear.Font = ((System.Drawing.Font)(resources.GetObject("cboYear.Font")));
			this.cboYear.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboYear.ImeMode")));
			this.cboYear.IntegralHeight = ((bool)(resources.GetObject("cboYear.IntegralHeight")));
			this.cboYear.ItemHeight = ((int)(resources.GetObject("cboYear.ItemHeight")));
			this.cboYear.Location = ((System.Drawing.Point)(resources.GetObject("cboYear.Location")));
			this.cboYear.MaxDropDownItems = ((int)(resources.GetObject("cboYear.MaxDropDownItems")));
			this.cboYear.MaxLength = ((int)(resources.GetObject("cboYear.MaxLength")));
			this.cboYear.Name = "cboYear";
			this.cboYear.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboYear.RightToLeft")));
			this.cboYear.Size = ((System.Drawing.Size)(resources.GetObject("cboYear.Size")));
			this.cboYear.TabIndex = ((int)(resources.GetObject("cboYear.TabIndex")));
			this.cboYear.Text = resources.GetString("cboYear.Text");
			this.cboYear.Visible = ((bool)(resources.GetObject("cboYear.Visible")));
			// 
			// cboMonth
			// 
			this.cboMonth.AccessibleDescription = resources.GetString("cboMonth.AccessibleDescription");
			this.cboMonth.AccessibleName = resources.GetString("cboMonth.AccessibleName");
			this.cboMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboMonth.Anchor")));
			this.cboMonth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboMonth.BackgroundImage")));
			this.cboMonth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboMonth.Dock")));
			this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMonth.Enabled = ((bool)(resources.GetObject("cboMonth.Enabled")));
			this.cboMonth.Font = ((System.Drawing.Font)(resources.GetObject("cboMonth.Font")));
			this.cboMonth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboMonth.ImeMode")));
			this.cboMonth.IntegralHeight = ((bool)(resources.GetObject("cboMonth.IntegralHeight")));
			this.cboMonth.ItemHeight = ((int)(resources.GetObject("cboMonth.ItemHeight")));
			this.cboMonth.Items.AddRange(new object[] {
														  resources.GetString("cboMonth.Items"),
														  resources.GetString("cboMonth.Items1"),
														  resources.GetString("cboMonth.Items2"),
														  resources.GetString("cboMonth.Items3"),
														  resources.GetString("cboMonth.Items4"),
														  resources.GetString("cboMonth.Items5"),
														  resources.GetString("cboMonth.Items6"),
														  resources.GetString("cboMonth.Items7"),
														  resources.GetString("cboMonth.Items8"),
														  resources.GetString("cboMonth.Items9"),
														  resources.GetString("cboMonth.Items10"),
														  resources.GetString("cboMonth.Items11")});
			this.cboMonth.Location = ((System.Drawing.Point)(resources.GetObject("cboMonth.Location")));
			this.cboMonth.MaxDropDownItems = ((int)(resources.GetObject("cboMonth.MaxDropDownItems")));
			this.cboMonth.MaxLength = ((int)(resources.GetObject("cboMonth.MaxLength")));
			this.cboMonth.Name = "cboMonth";
			this.cboMonth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboMonth.RightToLeft")));
			this.cboMonth.Size = ((System.Drawing.Size)(resources.GetObject("cboMonth.Size")));
			this.cboMonth.TabIndex = ((int)(resources.GetObject("cboMonth.TabIndex")));
			this.cboMonth.Text = resources.GetString("cboMonth.Text");
			this.cboMonth.Visible = ((bool)(resources.GetObject("cboMonth.Visible")));
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
			// btnViewReport
			// 
			this.btnViewReport.AccessibleDescription = resources.GetString("btnViewReport.AccessibleDescription");
			this.btnViewReport.AccessibleName = resources.GetString("btnViewReport.AccessibleName");
			this.btnViewReport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnViewReport.Anchor")));
			this.btnViewReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnViewReport.BackgroundImage")));
			this.btnViewReport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnViewReport.Dock")));
			this.btnViewReport.Enabled = ((bool)(resources.GetObject("btnViewReport.Enabled")));
			this.btnViewReport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnViewReport.FlatStyle")));
			this.btnViewReport.Font = ((System.Drawing.Font)(resources.GetObject("btnViewReport.Font")));
			this.btnViewReport.Image = ((System.Drawing.Image)(resources.GetObject("btnViewReport.Image")));
			this.btnViewReport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnViewReport.ImageAlign")));
			this.btnViewReport.ImageIndex = ((int)(resources.GetObject("btnViewReport.ImageIndex")));
			this.btnViewReport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnViewReport.ImeMode")));
			this.btnViewReport.Location = ((System.Drawing.Point)(resources.GetObject("btnViewReport.Location")));
			this.btnViewReport.Name = "btnViewReport";
			this.btnViewReport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnViewReport.RightToLeft")));
			this.btnViewReport.Size = ((System.Drawing.Size)(resources.GetObject("btnViewReport.Size")));
			this.btnViewReport.TabIndex = ((int)(resources.GetObject("btnViewReport.TabIndex")));
			this.btnViewReport.Text = resources.GetString("btnViewReport.Text");
			this.btnViewReport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnViewReport.TextAlign")));
			this.btnViewReport.Visible = ((bool)(resources.GetObject("btnViewReport.Visible")));
			this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
			// 
			// txtModel
			// 
			this.txtModel.AccessibleDescription = resources.GetString("txtModel.AccessibleDescription");
			this.txtModel.AccessibleName = resources.GetString("txtModel.AccessibleName");
			this.txtModel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtModel.Anchor")));
			this.txtModel.AutoSize = ((bool)(resources.GetObject("txtModel.AutoSize")));
			this.txtModel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtModel.BackgroundImage")));
			this.txtModel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtModel.Dock")));
			this.txtModel.Enabled = ((bool)(resources.GetObject("txtModel.Enabled")));
			this.txtModel.Font = ((System.Drawing.Font)(resources.GetObject("txtModel.Font")));
			this.txtModel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtModel.ImeMode")));
			this.txtModel.Location = ((System.Drawing.Point)(resources.GetObject("txtModel.Location")));
			this.txtModel.MaxLength = ((int)(resources.GetObject("txtModel.MaxLength")));
			this.txtModel.Multiline = ((bool)(resources.GetObject("txtModel.Multiline")));
			this.txtModel.Name = "txtModel";
			this.txtModel.PasswordChar = ((char)(resources.GetObject("txtModel.PasswordChar")));
			this.txtModel.ReadOnly = true;
			this.txtModel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtModel.RightToLeft")));
			this.txtModel.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtModel.ScrollBars")));
			this.txtModel.Size = ((System.Drawing.Size)(resources.GetObject("txtModel.Size")));
			this.txtModel.TabIndex = ((int)(resources.GetObject("txtModel.TabIndex")));
			this.txtModel.Text = resources.GetString("txtModel.Text");
			this.txtModel.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtModel.TextAlign")));
			this.txtModel.Visible = ((bool)(resources.GetObject("txtModel.Visible")));
			this.txtModel.WordWrap = ((bool)(resources.GetObject("txtModel.WordWrap")));
			// 
			// txtPartName
			// 
			this.txtPartName.AccessibleDescription = resources.GetString("txtPartName.AccessibleDescription");
			this.txtPartName.AccessibleName = resources.GetString("txtPartName.AccessibleName");
			this.txtPartName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPartName.Anchor")));
			this.txtPartName.AutoSize = ((bool)(resources.GetObject("txtPartName.AutoSize")));
			this.txtPartName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPartName.BackgroundImage")));
			this.txtPartName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPartName.Dock")));
			this.txtPartName.Enabled = ((bool)(resources.GetObject("txtPartName.Enabled")));
			this.txtPartName.Font = ((System.Drawing.Font)(resources.GetObject("txtPartName.Font")));
			this.txtPartName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPartName.ImeMode")));
			this.txtPartName.Location = ((System.Drawing.Point)(resources.GetObject("txtPartName.Location")));
			this.txtPartName.MaxLength = ((int)(resources.GetObject("txtPartName.MaxLength")));
			this.txtPartName.Multiline = ((bool)(resources.GetObject("txtPartName.Multiline")));
			this.txtPartName.Name = "txtPartName";
			this.txtPartName.PasswordChar = ((char)(resources.GetObject("txtPartName.PasswordChar")));
			this.txtPartName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPartName.RightToLeft")));
			this.txtPartName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPartName.ScrollBars")));
			this.txtPartName.Size = ((System.Drawing.Size)(resources.GetObject("txtPartName.Size")));
			this.txtPartName.TabIndex = ((int)(resources.GetObject("txtPartName.TabIndex")));
			this.txtPartName.Text = resources.GetString("txtPartName.Text");
			this.txtPartName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPartName.TextAlign")));
			this.txtPartName.Visible = ((bool)(resources.GetObject("txtPartName.Visible")));
			this.txtPartName.WordWrap = ((bool)(resources.GetObject("txtPartName.WordWrap")));
			this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
			this.txtPartName.Validating += new System.ComponentModel.CancelEventHandler(this.ControlValidating);
			// 
			// txtPartNumber
			// 
			this.txtPartNumber.AccessibleDescription = resources.GetString("txtPartNumber.AccessibleDescription");
			this.txtPartNumber.AccessibleName = resources.GetString("txtPartNumber.AccessibleName");
			this.txtPartNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPartNumber.Anchor")));
			this.txtPartNumber.AutoSize = ((bool)(resources.GetObject("txtPartNumber.AutoSize")));
			this.txtPartNumber.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPartNumber.BackgroundImage")));
			this.txtPartNumber.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPartNumber.Dock")));
			this.txtPartNumber.Enabled = ((bool)(resources.GetObject("txtPartNumber.Enabled")));
			this.txtPartNumber.Font = ((System.Drawing.Font)(resources.GetObject("txtPartNumber.Font")));
			this.txtPartNumber.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPartNumber.ImeMode")));
			this.txtPartNumber.Location = ((System.Drawing.Point)(resources.GetObject("txtPartNumber.Location")));
			this.txtPartNumber.MaxLength = ((int)(resources.GetObject("txtPartNumber.MaxLength")));
			this.txtPartNumber.Multiline = ((bool)(resources.GetObject("txtPartNumber.Multiline")));
			this.txtPartNumber.Name = "txtPartNumber";
			this.txtPartNumber.PasswordChar = ((char)(resources.GetObject("txtPartNumber.PasswordChar")));
			this.txtPartNumber.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPartNumber.RightToLeft")));
			this.txtPartNumber.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPartNumber.ScrollBars")));
			this.txtPartNumber.Size = ((System.Drawing.Size)(resources.GetObject("txtPartNumber.Size")));
			this.txtPartNumber.TabIndex = ((int)(resources.GetObject("txtPartNumber.TabIndex")));
			this.txtPartNumber.Text = resources.GetString("txtPartNumber.Text");
			this.txtPartNumber.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPartNumber.TextAlign")));
			this.txtPartNumber.Visible = ((bool)(resources.GetObject("txtPartNumber.Visible")));
			this.txtPartNumber.WordWrap = ((bool)(resources.GetObject("txtPartNumber.WordWrap")));
			this.txtPartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
			this.txtPartNumber.Validating += new System.ComponentModel.CancelEventHandler(this.ControlValidating);
			// 
			// txtCategory
			// 
			this.txtCategory.AccessibleDescription = resources.GetString("txtCategory.AccessibleDescription");
			this.txtCategory.AccessibleName = resources.GetString("txtCategory.AccessibleName");
			this.txtCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCategory.Anchor")));
			this.txtCategory.AutoSize = ((bool)(resources.GetObject("txtCategory.AutoSize")));
			this.txtCategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCategory.BackgroundImage")));
			this.txtCategory.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCategory.Dock")));
			this.txtCategory.Enabled = ((bool)(resources.GetObject("txtCategory.Enabled")));
			this.txtCategory.Font = ((System.Drawing.Font)(resources.GetObject("txtCategory.Font")));
			this.txtCategory.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCategory.ImeMode")));
			this.txtCategory.Location = ((System.Drawing.Point)(resources.GetObject("txtCategory.Location")));
			this.txtCategory.MaxLength = ((int)(resources.GetObject("txtCategory.MaxLength")));
			this.txtCategory.Multiline = ((bool)(resources.GetObject("txtCategory.Multiline")));
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.PasswordChar = ((char)(resources.GetObject("txtCategory.PasswordChar")));
			this.txtCategory.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCategory.RightToLeft")));
			this.txtCategory.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCategory.ScrollBars")));
			this.txtCategory.Size = ((System.Drawing.Size)(resources.GetObject("txtCategory.Size")));
			this.txtCategory.TabIndex = ((int)(resources.GetObject("txtCategory.TabIndex")));
			this.txtCategory.Text = resources.GetString("txtCategory.Text");
			this.txtCategory.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCategory.TextAlign")));
			this.txtCategory.Visible = ((bool)(resources.GetObject("txtCategory.Visible")));
			this.txtCategory.WordWrap = ((bool)(resources.GetObject("txtCategory.WordWrap")));
			this.txtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
			this.txtCategory.Validating += new System.ComponentModel.CancelEventHandler(this.ControlValidating);
			// 
			// txtWC
			// 
			this.txtWC.AccessibleDescription = resources.GetString("txtWC.AccessibleDescription");
			this.txtWC.AccessibleName = resources.GetString("txtWC.AccessibleName");
			this.txtWC.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtWC.Anchor")));
			this.txtWC.AutoSize = ((bool)(resources.GetObject("txtWC.AutoSize")));
			this.txtWC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtWC.BackgroundImage")));
			this.txtWC.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtWC.Dock")));
			this.txtWC.Enabled = ((bool)(resources.GetObject("txtWC.Enabled")));
			this.txtWC.Font = ((System.Drawing.Font)(resources.GetObject("txtWC.Font")));
			this.txtWC.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtWC.ImeMode")));
			this.txtWC.Location = ((System.Drawing.Point)(resources.GetObject("txtWC.Location")));
			this.txtWC.MaxLength = ((int)(resources.GetObject("txtWC.MaxLength")));
			this.txtWC.Multiline = ((bool)(resources.GetObject("txtWC.Multiline")));
			this.txtWC.Name = "txtWC";
			this.txtWC.PasswordChar = ((char)(resources.GetObject("txtWC.PasswordChar")));
			this.txtWC.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtWC.RightToLeft")));
			this.txtWC.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtWC.ScrollBars")));
			this.txtWC.Size = ((System.Drawing.Size)(resources.GetObject("txtWC.Size")));
			this.txtWC.TabIndex = ((int)(resources.GetObject("txtWC.TabIndex")));
			this.txtWC.Text = resources.GetString("txtWC.Text");
			this.txtWC.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtWC.TextAlign")));
			this.txtWC.Visible = ((bool)(resources.GetObject("txtWC.Visible")));
			this.txtWC.WordWrap = ((bool)(resources.GetObject("txtWC.WordWrap")));
			this.txtWC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
			this.txtWC.Validating += new System.ComponentModel.CancelEventHandler(this.ControlValidating);
			// 
			// txtCycle
			// 
			this.txtCycle.AccessibleDescription = resources.GetString("txtCycle.AccessibleDescription");
			this.txtCycle.AccessibleName = resources.GetString("txtCycle.AccessibleName");
			this.txtCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCycle.Anchor")));
			this.txtCycle.AutoSize = ((bool)(resources.GetObject("txtCycle.AutoSize")));
			this.txtCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCycle.BackgroundImage")));
			this.txtCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCycle.Dock")));
			this.txtCycle.Enabled = ((bool)(resources.GetObject("txtCycle.Enabled")));
			this.txtCycle.Font = ((System.Drawing.Font)(resources.GetObject("txtCycle.Font")));
			this.txtCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCycle.ImeMode")));
			this.txtCycle.Location = ((System.Drawing.Point)(resources.GetObject("txtCycle.Location")));
			this.txtCycle.MaxLength = ((int)(resources.GetObject("txtCycle.MaxLength")));
			this.txtCycle.Multiline = ((bool)(resources.GetObject("txtCycle.Multiline")));
			this.txtCycle.Name = "txtCycle";
			this.txtCycle.PasswordChar = ((char)(resources.GetObject("txtCycle.PasswordChar")));
			this.txtCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCycle.RightToLeft")));
			this.txtCycle.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCycle.ScrollBars")));
			this.txtCycle.Size = ((System.Drawing.Size)(resources.GetObject("txtCycle.Size")));
			this.txtCycle.TabIndex = ((int)(resources.GetObject("txtCycle.TabIndex")));
			this.txtCycle.Text = resources.GetString("txtCycle.Text");
			this.txtCycle.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCycle.TextAlign")));
			this.txtCycle.Visible = ((bool)(resources.GetObject("txtCycle.Visible")));
			this.txtCycle.WordWrap = ((bool)(resources.GetObject("txtCycle.WordWrap")));
			this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
			this.txtCycle.Validating += new System.ComponentModel.CancelEventHandler(this.ControlValidating);
			// 
			// btnPartName
			// 
			this.btnPartName.AccessibleDescription = resources.GetString("btnPartName.AccessibleDescription");
			this.btnPartName.AccessibleName = resources.GetString("btnPartName.AccessibleName");
			this.btnPartName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPartName.Anchor")));
			this.btnPartName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPartName.BackgroundImage")));
			this.btnPartName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPartName.Dock")));
			this.btnPartName.Enabled = ((bool)(resources.GetObject("btnPartName.Enabled")));
			this.btnPartName.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPartName.FlatStyle")));
			this.btnPartName.Font = ((System.Drawing.Font)(resources.GetObject("btnPartName.Font")));
			this.btnPartName.Image = ((System.Drawing.Image)(resources.GetObject("btnPartName.Image")));
			this.btnPartName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPartName.ImageAlign")));
			this.btnPartName.ImageIndex = ((int)(resources.GetObject("btnPartName.ImageIndex")));
			this.btnPartName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPartName.ImeMode")));
			this.btnPartName.Location = ((System.Drawing.Point)(resources.GetObject("btnPartName.Location")));
			this.btnPartName.Name = "btnPartName";
			this.btnPartName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPartName.RightToLeft")));
			this.btnPartName.Size = ((System.Drawing.Size)(resources.GetObject("btnPartName.Size")));
			this.btnPartName.TabIndex = ((int)(resources.GetObject("btnPartName.TabIndex")));
			this.btnPartName.Text = resources.GetString("btnPartName.Text");
			this.btnPartName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPartName.TextAlign")));
			this.btnPartName.Visible = ((bool)(resources.GetObject("btnPartName.Visible")));
			this.btnPartName.Click += new System.EventHandler(this.btnPartName_Click);
			// 
			// btnPartNumber
			// 
			this.btnPartNumber.AccessibleDescription = resources.GetString("btnPartNumber.AccessibleDescription");
			this.btnPartNumber.AccessibleName = resources.GetString("btnPartNumber.AccessibleName");
			this.btnPartNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPartNumber.Anchor")));
			this.btnPartNumber.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPartNumber.BackgroundImage")));
			this.btnPartNumber.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPartNumber.Dock")));
			this.btnPartNumber.Enabled = ((bool)(resources.GetObject("btnPartNumber.Enabled")));
			this.btnPartNumber.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPartNumber.FlatStyle")));
			this.btnPartNumber.Font = ((System.Drawing.Font)(resources.GetObject("btnPartNumber.Font")));
			this.btnPartNumber.Image = ((System.Drawing.Image)(resources.GetObject("btnPartNumber.Image")));
			this.btnPartNumber.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPartNumber.ImageAlign")));
			this.btnPartNumber.ImageIndex = ((int)(resources.GetObject("btnPartNumber.ImageIndex")));
			this.btnPartNumber.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPartNumber.ImeMode")));
			this.btnPartNumber.Location = ((System.Drawing.Point)(resources.GetObject("btnPartNumber.Location")));
			this.btnPartNumber.Name = "btnPartNumber";
			this.btnPartNumber.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPartNumber.RightToLeft")));
			this.btnPartNumber.Size = ((System.Drawing.Size)(resources.GetObject("btnPartNumber.Size")));
			this.btnPartNumber.TabIndex = ((int)(resources.GetObject("btnPartNumber.TabIndex")));
			this.btnPartNumber.Text = resources.GetString("btnPartNumber.Text");
			this.btnPartNumber.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPartNumber.TextAlign")));
			this.btnPartNumber.Visible = ((bool)(resources.GetObject("btnPartNumber.Visible")));
			this.btnPartNumber.Click += new System.EventHandler(this.btnPartNumber_Click);
			// 
			// btnCategory
			// 
			this.btnCategory.AccessibleDescription = resources.GetString("btnCategory.AccessibleDescription");
			this.btnCategory.AccessibleName = resources.GetString("btnCategory.AccessibleName");
			this.btnCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCategory.Anchor")));
			this.btnCategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCategory.BackgroundImage")));
			this.btnCategory.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCategory.Dock")));
			this.btnCategory.Enabled = ((bool)(resources.GetObject("btnCategory.Enabled")));
			this.btnCategory.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCategory.FlatStyle")));
			this.btnCategory.Font = ((System.Drawing.Font)(resources.GetObject("btnCategory.Font")));
			this.btnCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnCategory.Image")));
			this.btnCategory.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCategory.ImageAlign")));
			this.btnCategory.ImageIndex = ((int)(resources.GetObject("btnCategory.ImageIndex")));
			this.btnCategory.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCategory.ImeMode")));
			this.btnCategory.Location = ((System.Drawing.Point)(resources.GetObject("btnCategory.Location")));
			this.btnCategory.Name = "btnCategory";
			this.btnCategory.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCategory.RightToLeft")));
			this.btnCategory.Size = ((System.Drawing.Size)(resources.GetObject("btnCategory.Size")));
			this.btnCategory.TabIndex = ((int)(resources.GetObject("btnCategory.TabIndex")));
			this.btnCategory.Text = resources.GetString("btnCategory.Text");
			this.btnCategory.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCategory.TextAlign")));
			this.btnCategory.Visible = ((bool)(resources.GetObject("btnCategory.Visible")));
			this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
			// 
			// lblModel
			// 
			this.lblModel.AccessibleDescription = resources.GetString("lblModel.AccessibleDescription");
			this.lblModel.AccessibleName = resources.GetString("lblModel.AccessibleName");
			this.lblModel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblModel.Anchor")));
			this.lblModel.AutoSize = ((bool)(resources.GetObject("lblModel.AutoSize")));
			this.lblModel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblModel.Dock")));
			this.lblModel.Enabled = ((bool)(resources.GetObject("lblModel.Enabled")));
			this.lblModel.Font = ((System.Drawing.Font)(resources.GetObject("lblModel.Font")));
			this.lblModel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblModel.Image = ((System.Drawing.Image)(resources.GetObject("lblModel.Image")));
			this.lblModel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblModel.ImageAlign")));
			this.lblModel.ImageIndex = ((int)(resources.GetObject("lblModel.ImageIndex")));
			this.lblModel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblModel.ImeMode")));
			this.lblModel.Location = ((System.Drawing.Point)(resources.GetObject("lblModel.Location")));
			this.lblModel.Name = "lblModel";
			this.lblModel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblModel.RightToLeft")));
			this.lblModel.Size = ((System.Drawing.Size)(resources.GetObject("lblModel.Size")));
			this.lblModel.TabIndex = ((int)(resources.GetObject("lblModel.TabIndex")));
			this.lblModel.Text = resources.GetString("lblModel.Text");
			this.lblModel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblModel.TextAlign")));
			this.lblModel.Visible = ((bool)(resources.GetObject("lblModel.Visible")));
			// 
			// lblPartName
			// 
			this.lblPartName.AccessibleDescription = resources.GetString("lblPartName.AccessibleDescription");
			this.lblPartName.AccessibleName = resources.GetString("lblPartName.AccessibleName");
			this.lblPartName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPartName.Anchor")));
			this.lblPartName.AutoSize = ((bool)(resources.GetObject("lblPartName.AutoSize")));
			this.lblPartName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPartName.Dock")));
			this.lblPartName.Enabled = ((bool)(resources.GetObject("lblPartName.Enabled")));
			this.lblPartName.Font = ((System.Drawing.Font)(resources.GetObject("lblPartName.Font")));
			this.lblPartName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPartName.Image = ((System.Drawing.Image)(resources.GetObject("lblPartName.Image")));
			this.lblPartName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPartName.ImageAlign")));
			this.lblPartName.ImageIndex = ((int)(resources.GetObject("lblPartName.ImageIndex")));
			this.lblPartName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPartName.ImeMode")));
			this.lblPartName.Location = ((System.Drawing.Point)(resources.GetObject("lblPartName.Location")));
			this.lblPartName.Name = "lblPartName";
			this.lblPartName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPartName.RightToLeft")));
			this.lblPartName.Size = ((System.Drawing.Size)(resources.GetObject("lblPartName.Size")));
			this.lblPartName.TabIndex = ((int)(resources.GetObject("lblPartName.TabIndex")));
			this.lblPartName.Text = resources.GetString("lblPartName.Text");
			this.lblPartName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPartName.TextAlign")));
			this.lblPartName.Visible = ((bool)(resources.GetObject("lblPartName.Visible")));
			// 
			// lblPartNumber
			// 
			this.lblPartNumber.AccessibleDescription = resources.GetString("lblPartNumber.AccessibleDescription");
			this.lblPartNumber.AccessibleName = resources.GetString("lblPartNumber.AccessibleName");
			this.lblPartNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPartNumber.Anchor")));
			this.lblPartNumber.AutoSize = ((bool)(resources.GetObject("lblPartNumber.AutoSize")));
			this.lblPartNumber.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPartNumber.Dock")));
			this.lblPartNumber.Enabled = ((bool)(resources.GetObject("lblPartNumber.Enabled")));
			this.lblPartNumber.Font = ((System.Drawing.Font)(resources.GetObject("lblPartNumber.Font")));
			this.lblPartNumber.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPartNumber.Image = ((System.Drawing.Image)(resources.GetObject("lblPartNumber.Image")));
			this.lblPartNumber.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPartNumber.ImageAlign")));
			this.lblPartNumber.ImageIndex = ((int)(resources.GetObject("lblPartNumber.ImageIndex")));
			this.lblPartNumber.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPartNumber.ImeMode")));
			this.lblPartNumber.Location = ((System.Drawing.Point)(resources.GetObject("lblPartNumber.Location")));
			this.lblPartNumber.Name = "lblPartNumber";
			this.lblPartNumber.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPartNumber.RightToLeft")));
			this.lblPartNumber.Size = ((System.Drawing.Size)(resources.GetObject("lblPartNumber.Size")));
			this.lblPartNumber.TabIndex = ((int)(resources.GetObject("lblPartNumber.TabIndex")));
			this.lblPartNumber.Text = resources.GetString("lblPartNumber.Text");
			this.lblPartNumber.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPartNumber.TextAlign")));
			this.lblPartNumber.Visible = ((bool)(resources.GetObject("lblPartNumber.Visible")));
			// 
			// lblCategory
			// 
			this.lblCategory.AccessibleDescription = resources.GetString("lblCategory.AccessibleDescription");
			this.lblCategory.AccessibleName = resources.GetString("lblCategory.AccessibleName");
			this.lblCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCategory.Anchor")));
			this.lblCategory.AutoSize = ((bool)(resources.GetObject("lblCategory.AutoSize")));
			this.lblCategory.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCategory.Dock")));
			this.lblCategory.Enabled = ((bool)(resources.GetObject("lblCategory.Enabled")));
			this.lblCategory.Font = ((System.Drawing.Font)(resources.GetObject("lblCategory.Font")));
			this.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCategory.Image = ((System.Drawing.Image)(resources.GetObject("lblCategory.Image")));
			this.lblCategory.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCategory.ImageAlign")));
			this.lblCategory.ImageIndex = ((int)(resources.GetObject("lblCategory.ImageIndex")));
			this.lblCategory.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCategory.ImeMode")));
			this.lblCategory.Location = ((System.Drawing.Point)(resources.GetObject("lblCategory.Location")));
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCategory.RightToLeft")));
			this.lblCategory.Size = ((System.Drawing.Size)(resources.GetObject("lblCategory.Size")));
			this.lblCategory.TabIndex = ((int)(resources.GetObject("lblCategory.TabIndex")));
			this.lblCategory.Text = resources.GetString("lblCategory.Text");
			this.lblCategory.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCategory.TextAlign")));
			this.lblCategory.Visible = ((bool)(resources.GetObject("lblCategory.Visible")));
			// 
			// lblYear
			// 
			this.lblYear.AccessibleDescription = resources.GetString("lblYear.AccessibleDescription");
			this.lblYear.AccessibleName = resources.GetString("lblYear.AccessibleName");
			this.lblYear.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblYear.Anchor")));
			this.lblYear.AutoSize = ((bool)(resources.GetObject("lblYear.AutoSize")));
			this.lblYear.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblYear.Dock")));
			this.lblYear.Enabled = ((bool)(resources.GetObject("lblYear.Enabled")));
			this.lblYear.Font = ((System.Drawing.Font)(resources.GetObject("lblYear.Font")));
			this.lblYear.ForeColor = System.Drawing.Color.Maroon;
			this.lblYear.Image = ((System.Drawing.Image)(resources.GetObject("lblYear.Image")));
			this.lblYear.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblYear.ImageAlign")));
			this.lblYear.ImageIndex = ((int)(resources.GetObject("lblYear.ImageIndex")));
			this.lblYear.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblYear.ImeMode")));
			this.lblYear.Location = ((System.Drawing.Point)(resources.GetObject("lblYear.Location")));
			this.lblYear.Name = "lblYear";
			this.lblYear.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblYear.RightToLeft")));
			this.lblYear.Size = ((System.Drawing.Size)(resources.GetObject("lblYear.Size")));
			this.lblYear.TabIndex = ((int)(resources.GetObject("lblYear.TabIndex")));
			this.lblYear.Text = resources.GetString("lblYear.Text");
			this.lblYear.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblYear.TextAlign")));
			this.lblYear.Visible = ((bool)(resources.GetObject("lblYear.Visible")));
			// 
			// lblMonth
			// 
			this.lblMonth.AccessibleDescription = resources.GetString("lblMonth.AccessibleDescription");
			this.lblMonth.AccessibleName = resources.GetString("lblMonth.AccessibleName");
			this.lblMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMonth.Anchor")));
			this.lblMonth.AutoSize = ((bool)(resources.GetObject("lblMonth.AutoSize")));
			this.lblMonth.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMonth.Dock")));
			this.lblMonth.Enabled = ((bool)(resources.GetObject("lblMonth.Enabled")));
			this.lblMonth.Font = ((System.Drawing.Font)(resources.GetObject("lblMonth.Font")));
			this.lblMonth.ForeColor = System.Drawing.Color.Maroon;
			this.lblMonth.Image = ((System.Drawing.Image)(resources.GetObject("lblMonth.Image")));
			this.lblMonth.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMonth.ImageAlign")));
			this.lblMonth.ImageIndex = ((int)(resources.GetObject("lblMonth.ImageIndex")));
			this.lblMonth.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMonth.ImeMode")));
			this.lblMonth.Location = ((System.Drawing.Point)(resources.GetObject("lblMonth.Location")));
			this.lblMonth.Name = "lblMonth";
			this.lblMonth.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMonth.RightToLeft")));
			this.lblMonth.Size = ((System.Drawing.Size)(resources.GetObject("lblMonth.Size")));
			this.lblMonth.TabIndex = ((int)(resources.GetObject("lblMonth.TabIndex")));
			this.lblMonth.Text = resources.GetString("lblMonth.Text");
			this.lblMonth.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMonth.TextAlign")));
			this.lblMonth.Visible = ((bool)(resources.GetObject("lblMonth.Visible")));
			// 
			// btnWC
			// 
			this.btnWC.AccessibleDescription = resources.GetString("btnWC.AccessibleDescription");
			this.btnWC.AccessibleName = resources.GetString("btnWC.AccessibleName");
			this.btnWC.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnWC.Anchor")));
			this.btnWC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWC.BackgroundImage")));
			this.btnWC.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnWC.Dock")));
			this.btnWC.Enabled = ((bool)(resources.GetObject("btnWC.Enabled")));
			this.btnWC.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnWC.FlatStyle")));
			this.btnWC.Font = ((System.Drawing.Font)(resources.GetObject("btnWC.Font")));
			this.btnWC.Image = ((System.Drawing.Image)(resources.GetObject("btnWC.Image")));
			this.btnWC.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnWC.ImageAlign")));
			this.btnWC.ImageIndex = ((int)(resources.GetObject("btnWC.ImageIndex")));
			this.btnWC.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnWC.ImeMode")));
			this.btnWC.Location = ((System.Drawing.Point)(resources.GetObject("btnWC.Location")));
			this.btnWC.Name = "btnWC";
			this.btnWC.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnWC.RightToLeft")));
			this.btnWC.Size = ((System.Drawing.Size)(resources.GetObject("btnWC.Size")));
			this.btnWC.TabIndex = ((int)(resources.GetObject("btnWC.TabIndex")));
			this.btnWC.Text = resources.GetString("btnWC.Text");
			this.btnWC.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnWC.TextAlign")));
			this.btnWC.Visible = ((bool)(resources.GetObject("btnWC.Visible")));
			this.btnWC.Click += new System.EventHandler(this.btnWC_Click);
			// 
			// lblWorkCenter
			// 
			this.lblWorkCenter.AccessibleDescription = resources.GetString("lblWorkCenter.AccessibleDescription");
			this.lblWorkCenter.AccessibleName = resources.GetString("lblWorkCenter.AccessibleName");
			this.lblWorkCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblWorkCenter.Anchor")));
			this.lblWorkCenter.AutoSize = ((bool)(resources.GetObject("lblWorkCenter.AutoSize")));
			this.lblWorkCenter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblWorkCenter.Dock")));
			this.lblWorkCenter.Enabled = ((bool)(resources.GetObject("lblWorkCenter.Enabled")));
			this.lblWorkCenter.Font = ((System.Drawing.Font)(resources.GetObject("lblWorkCenter.Font")));
			this.lblWorkCenter.ForeColor = System.Drawing.Color.Black;
			this.lblWorkCenter.Image = ((System.Drawing.Image)(resources.GetObject("lblWorkCenter.Image")));
			this.lblWorkCenter.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWorkCenter.ImageAlign")));
			this.lblWorkCenter.ImageIndex = ((int)(resources.GetObject("lblWorkCenter.ImageIndex")));
			this.lblWorkCenter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblWorkCenter.ImeMode")));
			this.lblWorkCenter.Location = ((System.Drawing.Point)(resources.GetObject("lblWorkCenter.Location")));
			this.lblWorkCenter.Name = "lblWorkCenter";
			this.lblWorkCenter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblWorkCenter.RightToLeft")));
			this.lblWorkCenter.Size = ((System.Drawing.Size)(resources.GetObject("lblWorkCenter.Size")));
			this.lblWorkCenter.TabIndex = ((int)(resources.GetObject("lblWorkCenter.TabIndex")));
			this.lblWorkCenter.Text = resources.GetString("lblWorkCenter.Text");
			this.lblWorkCenter.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblWorkCenter.TextAlign")));
			this.lblWorkCenter.Visible = ((bool)(resources.GetObject("lblWorkCenter.Visible")));
			// 
			// btnSearchCycle
			// 
			this.btnSearchCycle.AccessibleDescription = resources.GetString("btnSearchCycle.AccessibleDescription");
			this.btnSearchCycle.AccessibleName = resources.GetString("btnSearchCycle.AccessibleName");
			this.btnSearchCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearchCycle.Anchor")));
			this.btnSearchCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchCycle.BackgroundImage")));
			this.btnSearchCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearchCycle.Dock")));
			this.btnSearchCycle.Enabled = ((bool)(resources.GetObject("btnSearchCycle.Enabled")));
			this.btnSearchCycle.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearchCycle.FlatStyle")));
			this.btnSearchCycle.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchCycle.Font")));
			this.btnSearchCycle.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchCycle.Image")));
			this.btnSearchCycle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchCycle.ImageAlign")));
			this.btnSearchCycle.ImageIndex = ((int)(resources.GetObject("btnSearchCycle.ImageIndex")));
			this.btnSearchCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearchCycle.ImeMode")));
			this.btnSearchCycle.Location = ((System.Drawing.Point)(resources.GetObject("btnSearchCycle.Location")));
			this.btnSearchCycle.Name = "btnSearchCycle";
			this.btnSearchCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearchCycle.RightToLeft")));
			this.btnSearchCycle.Size = ((System.Drawing.Size)(resources.GetObject("btnSearchCycle.Size")));
			this.btnSearchCycle.TabIndex = ((int)(resources.GetObject("btnSearchCycle.TabIndex")));
			this.btnSearchCycle.Text = resources.GetString("btnSearchCycle.Text");
			this.btnSearchCycle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchCycle.TextAlign")));
			this.btnSearchCycle.Visible = ((bool)(resources.GetObject("btnSearchCycle.Visible")));
			this.btnSearchCycle.Click += new System.EventHandler(this.btnSearchCycle_Click);
			// 
			// lblCycle
			// 
			this.lblCycle.AccessibleDescription = resources.GetString("lblCycle.AccessibleDescription");
			this.lblCycle.AccessibleName = resources.GetString("lblCycle.AccessibleName");
			this.lblCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCycle.Anchor")));
			this.lblCycle.AutoSize = ((bool)(resources.GetObject("lblCycle.AutoSize")));
			this.lblCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCycle.Dock")));
			this.lblCycle.Enabled = ((bool)(resources.GetObject("lblCycle.Enabled")));
			this.lblCycle.Font = ((System.Drawing.Font)(resources.GetObject("lblCycle.Font")));
			this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
			this.lblCycle.Image = ((System.Drawing.Image)(resources.GetObject("lblCycle.Image")));
			this.lblCycle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCycle.ImageAlign")));
			this.lblCycle.ImageIndex = ((int)(resources.GetObject("lblCycle.ImageIndex")));
			this.lblCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCycle.ImeMode")));
			this.lblCycle.Location = ((System.Drawing.Point)(resources.GetObject("lblCycle.Location")));
			this.lblCycle.Name = "lblCycle";
			this.lblCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCycle.RightToLeft")));
			this.lblCycle.Size = ((System.Drawing.Size)(resources.GetObject("lblCycle.Size")));
			this.lblCycle.TabIndex = ((int)(resources.GetObject("lblCycle.TabIndex")));
			this.lblCycle.Text = resources.GetString("lblCycle.Text");
			this.lblCycle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCycle.TextAlign")));
			this.lblCycle.Visible = ((bool)(resources.GetObject("lblCycle.Visible")));
			// 
			// cboCCN
			// 
			this.cboCCN.AccessibleDescription = resources.GetString("cboCCN.AccessibleDescription");
			this.cboCCN.AccessibleName = resources.GetString("cboCCN.AccessibleName");
			this.cboCCN.AddItemSeparator = ';';
			this.cboCCN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboCCN.Anchor")));
			this.cboCCN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboCCN.BackgroundImage")));
			this.cboCCN.Caption = "";
			this.cboCCN.CaptionHeight = 17;
			this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboCCN.ColumnCaptionHeight = 17;
			this.cboCCN.ColumnFooterHeight = 17;
			this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboCCN.ContentHeight = 15;
			this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboCCN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboCCN.Dock")));
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.Enabled = ((bool)(resources.GetObject("cboCCN.Enabled")));
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.Font = ((System.Drawing.Font)(resources.GetObject("cboCCN.Font")));
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboCCN.ImeMode")));
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = ((System.Drawing.Point)(resources.GetObject("cboCCN.Location")));
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboCCN.RightToLeft")));
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = ((System.Drawing.Size)(resources.GetObject("cboCCN.Size")));
			this.cboCCN.TabIndex = ((int)(resources.GetObject("cboCCN.TabIndex")));
			this.cboCCN.Text = resources.GetString("cboCCN.Text");
			this.cboCCN.Visible = ((bool)(resources.GetObject("cboCCN.Visible")));
			this.cboCCN.SelectedValueChanged += new System.EventHandler(this.cboCCN_SelectedValueChanged);
			this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}Reco" +
				"rdSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Raised,," +
				"1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}St" +
				"yle1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" " +
				"Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Vert" +
				"icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 118, 158</Client" +
				"Rect><VScrollBar><Width>17</Width></VScrollBar><HScrollBar><Height>17</Height></" +
				"HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Eve" +
				"nRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle paren" +
				"t=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLight" +
				"RowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me" +
				"=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle pare" +
				"nt=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" " +
				"/><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Nam" +
				"edStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><S" +
				"tyle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Styl" +
				"e parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style" +
				" parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Styl" +
				"e parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><S" +
				"tyle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horz" +
				"Splits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRec" +
				"SelWidth></Blob>";
			// 
			// lblCCN
			// 
			this.lblCCN.AccessibleDescription = resources.GetString("lblCCN.AccessibleDescription");
			this.lblCCN.AccessibleName = resources.GetString("lblCCN.AccessibleName");
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCCN.Anchor")));
			this.lblCCN.AutoSize = ((bool)(resources.GetObject("lblCCN.AutoSize")));
			this.lblCCN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCCN.Dock")));
			this.lblCCN.Enabled = ((bool)(resources.GetObject("lblCCN.Enabled")));
			this.lblCCN.Font = ((System.Drawing.Font)(resources.GetObject("lblCCN.Font")));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.Image = ((System.Drawing.Image)(resources.GetObject("lblCCN.Image")));
			this.lblCCN.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCCN.ImageAlign")));
			this.lblCCN.ImageIndex = ((int)(resources.GetObject("lblCCN.ImageIndex")));
			this.lblCCN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCCN.ImeMode")));
			this.lblCCN.Location = ((System.Drawing.Point)(resources.GetObject("lblCCN.Location")));
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCCN.RightToLeft")));
			this.lblCCN.Size = ((System.Drawing.Size)(resources.GetObject("lblCCN.Size")));
			this.lblCCN.TabIndex = ((int)(resources.GetObject("lblCCN.TabIndex")));
			this.lblCCN.Text = resources.GetString("lblCCN.Text");
			this.lblCCN.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCCN.TextAlign")));
			this.lblCCN.Visible = ((bool)(resources.GetObject("lblCCN.Visible")));
			// 
			// c1pBtnClosePPV
			// 
			this.c1pBtnClosePPV.Enabled = ((bool)(resources.GetObject("c1pBtnClosePPV.Enabled")));
			this.c1pBtnClosePPV.ImageIndex = ((int)(resources.GetObject("c1pBtnClosePPV.ImageIndex")));
			this.c1pBtnClosePPV.Text = resources.GetString("c1pBtnClosePPV.Text");
			this.c1pBtnClosePPV.ToolTipText = resources.GetString("c1pBtnClosePPV.ToolTipText");
			this.c1pBtnClosePPV.Visible = ((bool)(resources.GetObject("c1pBtnClosePPV.Visible")));
			// 
			// DCPReport
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
			this.Controls.Add(this.pnControls);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "DCPReport";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Load += new System.EventHandler(this.DCPReport_Load);
			this.pnControls.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// When load form, check security and intialize value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DCPReport_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".DCPReport_Load()";
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

				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, boUtils.ListCCN().Tables[MST_CCNTable.TABLE_NAME], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
				//cboCCN.SelectedValue = SystemProperty.CCNID;
				InitYearCombo();
				DateTime dtmServerDate = boUtils.GetDBDate();
				// set default month to server month
				cboMonth.SelectedIndex = dtmServerDate.Month - 1;
				// set selected default to server year
				cboYear.SelectedItem = dtmServerDate.Year;

				// temporary hide the select shift control
				lblShift.Visible = false;
				txtShift.Visible = false;
				btnShift.Visible = false;
				//lblWorkCenter.ForeColor = Color.Maroon;
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
		/// When user change CCN, clear all data in form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboCCN_SelectedValueChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboCCN_SelectedValueChanged()";
			try
			{
				// clear form if user change CCN
				//FormControlComponents.ClearForm(this);
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
		/// Display search form to select DCP Cycle
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearchCycle_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchCycle_Click()";
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
				htData.Add(PRO_DCOptionMasterTable.CCNID_FLD, int.Parse(cboCCN.SelectedValue.ToString()));
				if (sender is TextBox && sender != null)
					drowData = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCPCOptionTable.CYCLE_FLD, txtCycle.Text.Trim(), htData, false);
				else
					drowData = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCPCOptionTable.CYCLE_FLD, txtCycle.Text.Trim(), htData, true);

				if (drowData != null)
				{
					txtCycle.Text = drowData[PRO_DCOptionMasterTable.CYCLE_FLD].ToString().Trim();
					txtCycle.Tag = int.Parse(drowData[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString().Trim());
				}
				else
				{
					txtCycle.Focus();
					txtCycle.SelectAll();
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
		/// Display search form to select Work Center
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnWC_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnWC_Click()";
			const string END = "; ";
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
					dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtWC.Text.Trim(), null, false);
				else
					dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtWC.Text.Trim(), null, true);
				if (dtbData != null && dtbData.Rows.Count > 0)
				{
					Hashtable htLine = new Hashtable();
					string strLine = string.Empty;
					foreach (DataRow drowData in dtbData.Rows)
					{
						strLine += drowData[PRO_ProductionLineTable.CODE_FLD].ToString().Trim() + END;
						htLine.Add(int.Parse(drowData[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString().Trim()),
							drowData[PRO_ProductionLineTable.CODE_FLD].ToString().Trim());
					}
					txtWC.Text = strLine.Remove(strLine.Length - END.Length, END.Length);
					txtWC.Tag = htLine;
				}
				else
				{
					txtWC.Focus();
					txtWC.SelectAll();
				}
//				DataRowView drowData = null;
//				Hashtable htData = null;//new Hashtable();
//				//htData.Add(PRO_ProductionLineTable., int.Parse(cboCCN.SelectedValue.ToString()));
//				if (sender is TextBox && sender != null)
//					drowData = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtWC.Text.Trim(), htData, false);
//				else
//					drowData = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtWC.Text.Trim(), htData, true);
//
//				if (drowData != null)
//				{
//					txtWC.Text = drowData[PRO_ProductionLineTable.CODE_FLD].ToString().Trim();
//					Hashtable htLine = new Hashtable();
//					htLine.Add(int.Parse(drowData[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString().Trim()),
//						drowData[PRO_ProductionLineTable.CODE_FLD].ToString().Trim());
//					txtWC.Tag = htLine;
//				}
//				else
//				{
//					txtWC.Focus();
//					txtWC.SelectAll();
//				}
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
		/// Display search form to select Shift
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnShift_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnShift_Click()";
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
				Hashtable htData = null;//new Hashtable();
				//htData.Add(PRO_ProductionLineTable., int.Parse(cboCCN.SelectedValue.ToString()));
				if (sender is TextBox && sender != null)
					drowData = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, txtShift.Text.Trim(), htData, false);
				else
					drowData = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, txtShift.Text.Trim(), htData, true);

				if (drowData != null)
				{
					txtShift.Text = drowData[PRO_ShiftTable.SHIFTDESC_FLD].ToString().Trim();
					txtShift.Tag = int.Parse(drowData[PRO_ShiftTable.SHIFTID_FLD].ToString().Trim());
				}
				else
				{
					txtShift.Focus();
					txtShift.SelectAll();
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
		/// Display search form to select Category
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCategory_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCategory_Click()";
			try
			{
				DataRowView drowData = null;
				if (sender is TextBox && sender != null)
					drowData = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text.Trim(), null, false);
				else
					drowData = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text.Trim(), null, true);

				if (drowData != null)
				{
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

		/// <summary>
		/// Display search form to select Item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPartNumber_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNumber_Click()";
			try
			{
				// user must select CCN first
				if (cboCCN.SelectedValue == null || cboCCN.SelectedValue == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_WOROUTING_SELECT_CCN_FIRST, MessageBoxIcon.Error);
					cboCCN.Focus();
					return;
				}
				Hashtable htData = new Hashtable();
				htData.Add(ITM_ProductTable.CCNID_FLD, int.Parse(cboCCN.SelectedValue.ToString()));
				htData.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
				htData.Add(ITM_ProductTable.DELETEREASONID_FLD, DBNull.Value);
				// if user already select category, then item must belong to selected category
				if (txtCategory.Text.Trim() != string.Empty)
					htData.Add(ITM_ProductTable.CATEGORYID_FLD, (int)txtCategory.Tag);
				DataRowView drowData = null;
				if (sender is TextBox && sender != null)
					drowData = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text.Trim(), htData, false);
				else
					drowData = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text.Trim(), htData, true);

				if (drowData != null)
				{
					txtPartNumber.Text = drowData[ITM_ProductTable.CODE_FLD].ToString().Trim();
					txtPartName.Text = drowData[ITM_ProductTable.DESCRIPTION_FLD].ToString().Trim();
					txtModel.Text = drowData[ITM_ProductTable.REVISION_FLD].ToString().Trim();
					txtPartNumber.Tag = int.Parse(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString().Trim());
				}
				else
				{
					txtPartNumber.Focus();
					txtPartNumber.SelectAll();
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
		/// Display search form to select Item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPartName_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNumber_Click()";
			try
			{
				// user must select CCN first
				if (cboCCN.SelectedValue == null || cboCCN.SelectedValue == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_WOROUTING_SELECT_CCN_FIRST, MessageBoxIcon.Error);
					cboCCN.Focus();
					return;
				}
				Hashtable htData = new Hashtable();
				htData.Add(ITM_ProductTable.CCNID_FLD, int.Parse(cboCCN.SelectedValue.ToString()));
				htData.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
				htData.Add(ITM_ProductTable.DELETEREASONID_FLD, DBNull.Value);
				// if user already select category, then item must belong to selected category
				if (txtCategory.Text.Trim() != string.Empty)
					htData.Add(ITM_ProductTable.CATEGORYID_FLD, (int)txtCategory.Tag);
				DataRowView drowData = null;
				if (sender is TextBox && sender != null)
					drowData = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtPartName.Text.Trim(), htData, false);
				else
					drowData = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtPartName.Text.Trim(), htData, true);

				if (drowData != null)
				{
					txtPartNumber.Text = drowData[ITM_ProductTable.CODE_FLD].ToString().Trim();
					txtPartName.Text = drowData[ITM_ProductTable.DESCRIPTION_FLD].ToString().Trim();
					txtModel.Text = drowData[ITM_ProductTable.REVISION_FLD].ToString().Trim();
					txtPartNumber.Tag = int.Parse(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString().Trim());
				}
				else
				{
					txtPartName.Focus();
					txtPartName.SelectAll();
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
		/// Execute Report and render
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnViewReport_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnViewReport_Click()";
			const string REPORT_TITLE =  "PRODUCTION PLAN AND DELIVERY PLAN";
			try
			{
				// waiting cursor
				Cursor = Cursors.WaitCursor;
				#region Validate data

				// user must select CCN first
				if (cboCCN.SelectedValue == null || cboCCN.SelectedValue == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_WOROUTING_SELECT_CCN_FIRST, MessageBoxIcon.Error);
					cboCCN.Focus();
					return;
				}
				// user must select Cycle
				if (FormControlComponents.CheckMandatory(txtCycle))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DCP_SELECT_CYCLE, MessageBoxIcon.Error);
					txtCycle.Focus();
					txtCycle.SelectAll();
					return;
				}
				// user must select Production Line
//				if (FormControlComponents.CheckMandatory(txtWC))
//				{
//					MessageBox.Show("You must select a Production Line in order to view report", "PCS", MessageBoxButtons.OK, MessageBoxIcon.Error);
//					//PCSMessageBox.Show(ErrorCode.MESSAGE_WCDISPATCH_SELECT_WORKCENTER, MessageBoxIcon.Error);
//					txtWC.Focus();
//					txtWC.SelectAll();
//					return;
//				}
				// check report layout file is exist or not
				if (!File.Exists(mstrReportDefFolder + @"\" + LAYOUT_FILENAME))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
					return;
				}
				#endregion

				int intProductID = 0;
				int intCategoryID = 0;
				int intCCNID = int.Parse(cboCCN.SelectedValue.ToString());
				try
				{
					intCategoryID = (int)(txtCategory.Tag);
				}
				catch{}
				try
				{
					intProductID = (int)(txtPartNumber.Tag);
				}
				catch{}
				UtilsBO boUtils = new UtilsBO();
				DateTime dtmReportDate = boUtils.GetDBDate();
				Hashtable htLine = new Hashtable();
				//ArrayList arrProductionLineID = new ArrayList();
				if (txtWC.Text.Trim() != string.Empty)
					htLine = (Hashtable)txtWC.Tag;
				else
				{
					DataTable dtbAllPro = boDCPReport.GetAllProductionLine(intCCNID);
					foreach (DataRow drowData in dtbAllPro.Rows)
					{
						try
						{
							htLine.Add(int.Parse(drowData[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString().Trim()),
								drowData[PRO_ProductionLineTable.CODE_FLD].ToString().Trim());
							//arrProductionLineID.Add(int.Parse(drowData[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString().Trim()));
						}
						catch{}
					}
				}
				foreach (int intProductionLineID in htLine.Keys)
				{
					// retrieve data to value object
//					voDCPReport = (DCPReportVO)boDCPReport.ExecuteReport(intCCNID, (int)txtCycle.Tag, intProductionLineID, cboMonth.SelectedIndex + 1,
//						int.Parse(cboYear.SelectedItem.ToString()), intProductID, intCategoryID, txtShift.Text);
					if (voDCPReport == null)
						return;
					DataTable dtbCycleDetail = boDCPReport.GetCycleDetail((int)txtCycle.Tag);
					StringBuilder strMasterLocationIDs = new StringBuilder();
					for (int i = 0; i < dtbCycleDetail.Rows.Count; i++)
					{
						if (i == 0)
							strMasterLocationIDs.Append(dtbCycleDetail.Rows[i][PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD].ToString().Trim());
						else
							strMasterLocationIDs.Append(",").Append(dtbCycleDetail.Rows[i][PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD].ToString().Trim());
					}
					ArrayList arrUsedItem = new ArrayList();
					ArrayList arrDayOfWeek = new ArrayList();
					int intNumOfWorkdays = 0;
					int intNumOfOffdays = 0;
//					voDCPReport.ReportData = GetReportData(voDCPReport.HugeData, voDCPReport.Products, 
//						voDCPReport.ReportDates, strMasterLocationIDs.ToString(), 
//						dtmReportDate, out arrDayOfWeek, out arrUsedItem,
//						out intNumOfWorkdays, out intNumOfOffdays);
					voDCPReport.TableName = DCP_REPORT;
				
					DCPReportBuilder objDCPReportBuilder = new DCPReportBuilder();
					objDCPReportBuilder.SourceDataTable = voDCPReport.ReportData;	
					objDCPReportBuilder.ProductColumnName = PARTNUMBER_COLUMNNAME;	
					objDCPReportBuilder.TypeColumnName = TYPE_COLUMNNAME;	
					objDCPReportBuilder.ReportDefinitionFolder = mstrReportDefFolder;	
					objDCPReportBuilder.ReportFileName = LAYOUT_FILENAME;	

					objDCPReportBuilder.CCN = cboCCN.SelectedText;
					objDCPReportBuilder.DCPCycle = txtCycle.Text.Trim();
					objDCPReportBuilder.Month = voDCPReport.Month;
					objDCPReportBuilder.Year = voDCPReport.Year;
					// production line
					objDCPReportBuilder.WorkCenter = htLine[intProductionLineID].ToString();
					objDCPReportBuilder.Category = txtCategory.Text.Trim();
					objDCPReportBuilder.PartNumber = txtPartNumber.Text.Trim();
					objDCPReportBuilder.PartName = txtPartName.Text.Trim();
					objDCPReportBuilder.Model = txtModel.Text.Trim();	
				
					voDCPReport.Shifts = MappingShift(voDCPReport.Shifts);
					objDCPReportBuilder.Shift = voDCPReport.Shifts;				
					objDCPReportBuilder.StandardCapacity = voDCPReport.StandardCapacity;
					objDCPReportBuilder.TotalRequiredCapacity = voDCPReport.TotalRequiredCapacity;
					objDCPReportBuilder.TotalChangeTime = voDCPReport.TotalChangeTime;
					objDCPReportBuilder.TotalCheckpointTime = voDCPReport.TotalCheckpointTime;
					objDCPReportBuilder.Effective = voDCPReport.Effective;
					objDCPReportBuilder.RemainCapacity = voDCPReport.RemainCapacity;
					objDCPReportBuilder.CategoriesOnDay = voDCPReport.CategoriesOnDay;
					objDCPReportBuilder.ReportDates = voDCPReport.ReportDates;
					objDCPReportBuilder.ReportDayOfWeek = arrDayOfWeek;
				
					objDCPReportBuilder.StartDay = voDCPReport.StartDay;			
					objDCPReportBuilder.EndDay = voDCPReport.EndDay;

					objDCPReportBuilder.NumOfOffdays = intNumOfOffdays;
					objDCPReportBuilder.NumOfWorkdays = intNumOfWorkdays;

					objDCPReportBuilder.MakeDataTableForRender();
					PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog ppdViewer = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
					objDCPReportBuilder.ReportViewer = ppdViewer.ReportViewer;
					objDCPReportBuilder.RenderReport();
					ppdViewer.Name = htLine[intProductionLineID].ToString();
				
					ppdViewer.FormTitle = REPORT_TITLE;				
					ppdViewer.Show();
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
			finally
			{
				// restore cursor
				Cursor = Cursors.Default;
			}
		}

		/// <summary>
		/// Close the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Help button pressed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnHelp_Click(object sender, EventArgs e)
		{
		
		}
		/// <summary>
		/// Validating event of all searchable textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ControlValidating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnLeave()";
			try
			{
				TextBox txtSender = (TextBox)sender;
				if (txtSender.Modified)
				{
					string strValue = txtSender.Text.Trim();
					if (txtSender.Equals(txtCycle))
					{
						if (strValue != string.Empty)
							btnSearchCycle_Click(sender, e);
						else
							txtCycle.Tag = null;
					}
					else if (txtSender.Equals(txtWC))
					{
						if (strValue != string.Empty)
							btnWC_Click(sender, e);
						else
							txtWC.Tag = null;
					}
					else if (txtSender.Equals(txtShift))
					{
						if (strValue != string.Empty)
							btnShift_Click(sender, e);
						else
							txtShift.Tag = null;
					}
					else if (txtSender.Equals(txtCategory))
					{
						if (strValue != string.Empty)
							btnCategory_Click(sender, e);
						else
							txtCategory.Tag = null;
					}
					else if (txtSender.Equals(txtPartNumber))
					{
						if (strValue != string.Empty)
							btnPartNumber_Click(sender, e);
						else
						{
							txtPartNumber.Tag = null;
							txtPartName.Text = string.Empty;
							txtModel.Text = string.Empty;
						}
					}
					else if (txtSender.Equals(txtPartName))
					{
						if (strValue != string.Empty)
							btnPartName_Click(sender, e);
						else
						{
							txtPartName.Tag = null;
							txtPartNumber.Text = string.Empty;
							txtModel.Text = string.Empty;
						}
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
		/// <summary>
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
		/// When user press F4, open search form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtPartName_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					if (sender.Equals(txtCycle))
						btnSearchCycle_Click(null, null);
					else if (sender.Equals(txtWC))
						btnWC_Click(null, null);
					else if (sender.Equals(txtCategory))
						btnCategory_Click(null, null);
					else if (sender.Equals(txtPartNumber))
						btnPartNumber_Click(null, null);
					else if (sender.Equals(txtPartName))
						btnPartName_Click(null, null);
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
		/// Builds the report data table based on DataSet return from database
		/// </summary>
		/// <param name="pdstHugeData">DataSet from database</param>
		/// <param name="parrProducts">List of Products</param>
		/// <param name="parrDays">List of Days</param>
		/// <param name="pstrMasterLocationID">List of Master Locations</param>
		/// <param name="pdtmReportDate">The Report Date</param>
		/// <param name="oarrDayOfWeek">List of DayOfWeek instance for all report day</param>
		/// <param name="oarrUsedItem">Used Items</param>
		/// <returns>Report DataTable</returns>
		/*private DataTable GetReportData(DataSet pdstHugeData, ArrayList parrProducts, ArrayList parrDays, 
			string pstrMasterLocationID, DateTime pdtmReportDate, 
			out ArrayList oarrDayOfWeek, out ArrayList oarrUsedItem,
			out int ointNumOfWorkday, out int ointNumOfOffdays)
		{
			const string CATEGORY_COL = "Category";
			const string COL_PREFIX = "D";
			const string MONTH_COL_PREFIX = "M";
			const string DAY_FORMAT = "00";
			const string DELIVERY_TABLE = "Delivery";
			const string MONTH_DELIVERY_TABLE = "DeliveryMonth";
			const string P_MONTH_DELIVERY_TABLE = "DeliveryPMonth";
			const string PRODUCE_TABLE = "Produce";
			const string MONTH_PRODUCE_TABLE = "ProduceMonth";
			const string P_MONTH_PRODUCE_TABLE = "ProducePMonth";
			const string START_YEAR_COL = "StartYear";
			const string START_MONTH_COL = "StartMonth";
			const string START_DAY_COL = "StartDay";
			const int DECIMAL_ROUND_NUMBER = 0;
			const int CATEGORY_MAX_LENGTH = 6;
//			const int START_HOUR = 6;
//			const int START_MINUTE = 15;
			const string DOTS = "..";

			#region prepare data for report

			oarrDayOfWeek = new ArrayList();

			oarrUsedItem = new ArrayList();
			// build list of Items
			StringBuilder strItems = new StringBuilder();
			for (int i = 0; i < parrProducts.Count; i++)
			{
				if (i == 0)
					strItems.Append(parrProducts[i].ToString());
				else
					strItems.Append(",").Append(parrProducts[i].ToString());
			}
			DateTime dtmBeginDate = (DateTime)parrDays[0];
			DataTable dtbNetAvailableQuantity = boDCPReport.GetNetBeginQuantity(strItems.ToString(), pstrMasterLocationID);
			DataTable dtbTotalIn = new DataTable();
			DataTable dtbTotalSO = new DataTable();
			DataTable dtbTotalPO = new DataTable();
			DataTable dtbTotalWO = new DataTable();
			// from begin date to report date
			if (dtmBeginDate < pdtmReportDate)
				dtbTotalIn = boDCPReport.GetTotalIn(strItems.ToString(), pstrMasterLocationID, dtmBeginDate, pdtmReportDate);
			//else // from report date to begin date
			//	dtbTotalIn = boDCPReport.GetTotalIn(strItems.ToString(), pstrMasterLocationID, pdtmReportDate, dtmBeginDate);

			DataTable dtbTotalOut = new DataTable();
			// from begin date to report date
			if (dtmBeginDate < pdtmReportDate)
				dtbTotalOut = boDCPReport.GetTotalOut(strItems.ToString(), pstrMasterLocationID, dtmBeginDate, pdtmReportDate);
			//else // from report date to begin date
			//	dtbTotalOut = boDCPReport.GetTotalOut(strItems.ToString(), pstrMasterLocationID, pdtmReportDate, dtmBeginDate);
			if (dtmBeginDate > pdtmReportDate)
			{
				dtbTotalSO = boDCPReport.GetTotalSO(strItems.ToString(), pstrMasterLocationID, pdtmReportDate, dtmBeginDate);
				dtbTotalPO = boDCPReport.GetTotalPO(strItems.ToString(), pstrMasterLocationID, pdtmReportDate, dtmBeginDate);
				dtbTotalWO = boDCPReport.GetTotalWO(strItems.ToString(), pstrMasterLocationID, pdtmReportDate, dtmBeginDate);
			}

			DataTable dtbWorkingTime = boDCPReport.GetWorkingTime();

			#endregion

			#region Create report data table

			DataTable dtbReportData = new DataTable(DCP_REPORT);
			dtbReportData.Columns.Add(new DataColumn(CATEGORY_COL, typeof(string)));
			dtbReportData.Columns.Add(new DataColumn(PARTNUMBER_COLUMNNAME, typeof(string)));
			dtbReportData.Columns.Add(new DataColumn(PARTNAME_COL, typeof(string)));
			dtbReportData.Columns.Add(new DataColumn(MODEL_COL, typeof(string)));
			dtbReportData.Columns.Add(new DataColumn(ITM_ProductTable.PRODUCTID_FLD, typeof(int)));
			dtbReportData.Columns.Add(new DataColumn(ITM_CategoryTable.CATEGORYID_FLD, typeof(int)));
			// 1 = Delivery, 2 = Product, 3 = Inventory
			dtbReportData.Columns.Add(new DataColumn(TYPE_COLUMNNAME, typeof(int)));
			// begin quantity column
			dtbReportData.Columns.Add(new DataColumn(BEGIN_COL, typeof(decimal)));
			// building column for each day
			foreach (DateTime dtmDate in parrDays)
			{
				dtbReportData.Columns.Add(new DataColumn(COL_PREFIX + dtmDate.Day.ToString(DAY_FORMAT), typeof(decimal)));
				oarrDayOfWeek.Add(dtmDate.DayOfWeek.ToString().Substring(0, 3));
			}
			// we need to insert missing field in report for 31 days
			int intNumOfMissDay = 31 - parrDays.Count;
			DateTime dtmLastDate = (DateTime)parrDays[parrDays.Count - 1];
			for (int i = 1; i <= intNumOfMissDay; i++)
			{
				int intDay = dtmLastDate.Day + i;
				dtbReportData.Columns.Add(new DataColumn(COL_PREFIX + intDay.ToString(DAY_FORMAT), typeof(decimal)));
				dtbReportData.Columns[COL_PREFIX + intDay.ToString(DAY_FORMAT)].AllowDBNull = true;
			}
			// now create two columns for next two months
			for (int i = 1; i <= 2; i++)
			{
				dtbReportData.Columns.Add(new DataColumn(MONTH_COL_PREFIX + i.ToString(DAY_FORMAT), typeof(decimal)));
				dtbReportData.Columns[MONTH_COL_PREFIX + i.ToString(DAY_FORMAT)].AllowDBNull = true;
			}

			#endregion

			#region Calculate data for each item in the list

			foreach (int intProductID in parrProducts)
			{
				bool blnHasDelivery = false;
				bool blnHasProduce = false;
				decimal decBeginQuantity = 0;
				decimal decInQuantity = 0;
				decimal decOutQuantity = 0;
				DataRow drowDelivery = dtbReportData.NewRow();
				DataRow drowProduce = dtbReportData.NewRow();
				DataRow drowInventory = dtbReportData.NewRow();
				drowDelivery[ITM_ProductTable.PRODUCTID_FLD] = intProductID;
				drowDelivery[TYPE_COLUMNNAME] = (int)RowType.Delivery;
				drowDelivery[BEGIN_COL] = DBNull.Value;
				drowProduce[ITM_ProductTable.PRODUCTID_FLD] = intProductID;
				drowProduce[TYPE_COLUMNNAME] = (int)RowType.Produce;
				drowProduce[BEGIN_COL] = DBNull.Value;
				drowInventory[TYPE_COLUMNNAME] = (int)RowType.Inventory;
				// get delivery table of current day
				DataTable dtbDelivery = pdstHugeData.Tables[DELIVERY_TABLE];
				// get produce table of current day
				DataTable dtbProduce = pdstHugeData.Tables[PRODUCE_TABLE];
				// get delivery table of next two month
				DataTable dtbDeliveryNextMonth = pdstHugeData.Tables[MONTH_DELIVERY_TABLE];
				// get produce table of next two month
				DataTable dtbProduceNextMonth = pdstHugeData.Tables[MONTH_PRODUCE_TABLE];
				// get delivery table of previous month
				DataTable dtbDeliveryPMonth = pdstHugeData.Tables[P_MONTH_DELIVERY_TABLE];
				// get produce table of previous month
				DataTable dtbProducePMonth = pdstHugeData.Tables[P_MONTH_PRODUCE_TABLE];

				#region Calculate stock begin quantity

				#region inventory quantity

				DataRow[] drowsInventory = dtbNetAvailableQuantity.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID.ToString() + "'");
				if (drowsInventory != null && drowsInventory.Length > 0)
				{
					foreach (DataRow drowStock in drowsInventory)
					{
						try
						{
							decBeginQuantity += decimal.Parse(drowStock[AVAILABLE_QUANTITY_COL].ToString());
						}
						catch{}
					}
				}

				#endregion

				#region total in quantity

				// get total in quantity of product
				DataRow[] drowTotalIn = null;
				DataRow[] drowTotalPO = null;
				if (dtbTotalIn.Rows.Count > 0)
					drowTotalIn = dtbTotalIn.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID.ToString() + "'");
				else if (dtbTotalPO.Rows.Count > 0)
					drowTotalPO = dtbTotalPO.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID.ToString() + "'");
				if (dtbTotalWO.Rows.Count > 0)
					drowTotalIn = dtbTotalWO.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID.ToString() + "'");
				if (drowTotalIn != null && drowTotalIn.Length > 0)
				{
					foreach (DataRow drowIn in drowTotalIn)
					{
						try
						{
							decInQuantity += decimal.Parse(drowIn[AVAILABLE_QUANTITY_COL].ToString());
						}
						catch{}
					}
				}
				if (drowTotalPO != null && drowTotalPO.Length > 0)
				{
					foreach (DataRow drowIn in drowTotalPO)
					{
						try
						{
							decInQuantity += decimal.Parse(drowIn[AVAILABLE_QUANTITY_COL].ToString());
						}
						catch{}
					}
				}

				#endregion

				#region total out quantity

				// get total out quantity of product
				DataRow[] drowTotalOut = null;
				if (dtbTotalOut.Rows.Count > 0)
					drowTotalOut = dtbTotalOut.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID.ToString() + "'");
				else if (dtbTotalSO.Rows.Count > 0)
					drowTotalOut = dtbTotalSO.Select(ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID.ToString() + "'");
				if (drowTotalOut != null && drowTotalOut.Length > 0)
				{
					foreach (DataRow drowOut in drowTotalOut)
					{
						try
						{
							decOutQuantity += decimal.Parse(drowOut[AVAILABLE_QUANTITY_COL].ToString());
						}
						catch{}
					}
				}

				#endregion

				DateTime dtmStartPMonth = dtmBeginDate.AddMonths(-1);
				dtmStartPMonth = new DateTime(dtmStartPMonth.Year, dtmStartPMonth.Month, dtmStartPMonth.Day, 0, 0, 0);
				DateTime dtmEndPMonth = dtmStartPMonth.AddMonths(1).AddDays(-1);
				string strPProduceFilter = ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID.ToString() + "' AND "
					+ PRO_DCPResultDetailTable.WORKINGDATE_FLD + " >='" + dtmStartPMonth.ToString() + "' AND "
					+ PRO_DCPResultDetailTable.WORKINGDATE_FLD + " <= '" + dtmEndPMonth.ToString() + "'";
				DateTime dtmStartTimeP = dtmStartPMonth;
				DateTime dtmEndTimeP = dtmEndPMonth;
				// get start time of the month
				GetStartAndEndTime(dtmStartPMonth, ref dtmStartTimeP, ref dtmStartPMonth, dtbWorkingTime);
				// get end time of the month
				GetStartAndEndTime(dtmEndPMonth, ref dtmEndTimeP, ref dtmEndTimeP, dtbWorkingTime);
				string strPDeliveryFilter = ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID.ToString() + "' AND "
					+ MTR_CPOTable.DUEDATE_FLD + ">='" + dtmStartTimeP.ToString("G") + "' AND "
					+ MTR_CPOTable.DUEDATE_FLD + "<='" + dtmEndTimeP.ToString("G") + "'";

				#region total produce plan (from DCP Result Detail)

				decimal decTotalProducePlan = 0;
				DataRow[] drowTotalProducePlan = dtbProducePMonth.Select(strPProduceFilter);
				foreach (DataRow drowData in drowTotalProducePlan)
				{
					try
					{
						decTotalProducePlan += decimal.Parse(drowData[PRO_DCPResultDetailTable.QUANTITY_FLD].ToString().Trim());
					}
					catch{}
				}

				#endregion

				#region total delivery plan (from CPO)
				
				decimal decTotalDeliveryPlan = 0;
				DataRow[] drowDeliveryPlan = dtbDeliveryPMonth.Select(strPDeliveryFilter);
				foreach (DataRow drowData in drowDeliveryPlan)
				{
					try
					{
						decTotalDeliveryPlan += decimal.Parse(drowData[MTR_CPOTable.QUANTITY_FLD].ToString().Trim());
					}
					catch{}
				}

				#endregion

				if (dtmBeginDate < pdtmReportDate)
				{
					// begin quantity = net begin (inventory) - total in quantity + total out quantity
					decBeginQuantity = decBeginQuantity - decInQuantity + decOutQuantity;
				}
				else
				{
					// begin quantity = net begin (inventory) + total in quantity - total out quantity
					decBeginQuantity = decBeginQuantity + decInQuantity - decOutQuantity;
				}
				// begin quantity = begin quantity + total produce plan - total delivery plan
				decBeginQuantity = decBeginQuantity + decTotalProducePlan - decTotalDeliveryPlan;
				drowInventory[BEGIN_COL] = decBeginQuantity;

				#endregion

				#region Delivery and produce for all Days in Month

				foreach (DateTime dtmReportDays in parrDays)
				{
					DateTime dtmStartTime = dtmReportDays;
					DateTime dtmEndTime = dtmReportDays;
					GetStartAndEndTime(dtmReportDays, ref dtmStartTime, ref dtmEndTime, dtbWorkingTime);
					DateTime dtmStart = new DateTime(dtmReportDays.Year, dtmReportDays.Month, dtmReportDays.Day, 0, 0, 0);
					string strProduceFilter = ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID.ToString() + "' AND "
						+ PRO_DCPResultDetailTable.WORKINGDATE_FLD + " ='" + dtmStart.ToString() + "'";
					string strDeliveryFilter = ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID.ToString() + "' AND "
						+ MTR_CPOTable.DUEDATE_FLD + ">='" + dtmStartTime.ToString("G") + "' AND "
						+ MTR_CPOTable.DUEDATE_FLD + " <'" + dtmEndTime.ToString("G") + "'";
//						+ START_YEAR_COL + "='" + dtmReportDays.Year + "' AND "
//						+ START_MONTH_COL + "='" + dtmReportDays.Month + "' AND "
//						+ START_DAY_COL + "='" + dtmReportDays.Day + "'";
					// get the delivery row which have item
					DataRow[] drowsDeliver = dtbDelivery.Select(strDeliveryFilter);
					// get the produce row which have item
					DataRow[] drowsProduce = dtbProduce.Select(strProduceFilter);

					#region Delivery

					if (drowsDeliver != null && drowsDeliver.Length > 0)
					{
						decimal decQuantity = 0;
						foreach (DataRow drowData in drowsDeliver)
						{
							try
							{
								decQuantity += decimal.Parse(drowData[MTR_CPOTable.QUANTITY_FLD].ToString());
							}
							catch{}
						}
						drowDelivery[COL_PREFIX + dtmReportDays.Day.ToString(DAY_FORMAT)] = decimal.Round(decQuantity, DECIMAL_ROUND_NUMBER);
						drowDelivery[CATEGORY_COL] = drowsDeliver[0][CATEGORY_COL];
						drowDelivery[ITM_CategoryTable.CATEGORYID_FLD] = drowsDeliver[0][ITM_CategoryTable.CATEGORYID_FLD];
						drowDelivery[PARTNUMBER_COLUMNNAME] = drowsDeliver[0][PARTNUMBER_COLUMNNAME];
						drowDelivery[PARTNAME_COL] = drowsDeliver[0][PARTNAME_COL];
						drowDelivery[MODEL_COL] = drowsDeliver[0][MODEL_COL];
						blnHasDelivery = true;
					}
					else
						drowDelivery[COL_PREFIX + dtmReportDays.Day.ToString(DAY_FORMAT)] = DBNull.Value;

					#endregion

					#region Produce

					if (drowsProduce != null && drowsProduce.Length > 0)
					{
						decimal decQuantity = 0;
						foreach (DataRow drowData in drowsProduce)
						{
							try
							{
								decQuantity += decimal.Parse(drowData[PRO_DCPResultDetailTable.QUANTITY_FLD].ToString());
							}
							catch{}
						}
						drowProduce[COL_PREFIX + dtmReportDays.Day.ToString(DAY_FORMAT)] = decimal.Round(decQuantity, DECIMAL_ROUND_NUMBER);
						drowProduce[CATEGORY_COL] = drowsProduce[0][CATEGORY_COL];
						drowProduce[ITM_CategoryTable.CATEGORYID_FLD] = drowsProduce[0][ITM_CategoryTable.CATEGORYID_FLD];
						drowProduce[PARTNUMBER_COLUMNNAME] = drowsProduce[0][PARTNUMBER_COLUMNNAME];
						drowProduce[PARTNAME_COL] = drowsProduce[0][PARTNAME_COL];
						drowProduce[MODEL_COL] = drowsProduce[0][MODEL_COL];
						blnHasProduce = true;
					}
					else
						drowProduce[COL_PREFIX + dtmReportDays.Day.ToString(DAY_FORMAT)] = DBNull.Value;

					#endregion
				}

				#endregion

				#region Delivery and produce for Next two months

				for (int i = 1; i <= 2; i++)
				{
					DateTime dtmStart = dtmBeginDate.AddMonths(i);
					dtmStart = new DateTime(dtmStart.Year, dtmStart.Month, dtmStart.Day, 0, 0, 0);
					DateTime dtmEnd = dtmStart.AddMonths(1).AddDays(-1);
					string strProduceFilter = ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID.ToString() + "' AND "
						+ PRO_DCPResultDetailTable.WORKINGDATE_FLD + " >='" + dtmStart.ToString() + "' AND "
						+ PRO_DCPResultDetailTable.WORKINGDATE_FLD + " <= '" + dtmEnd.ToString() + "'";
					DateTime dtmStartTime = dtmStart;
					DateTime dtmEndTime = dtmEnd;
					// get start time of the month
					GetStartAndEndTime(dtmStart, ref dtmStartTime, ref dtmStart, dtbWorkingTime);
					// get end time of the month
					GetStartAndEndTime(dtmEnd, ref dtmEnd, ref dtmEndTime, dtbWorkingTime);
					string strDeliveryFilter = ITM_ProductTable.PRODUCTID_FLD + "='" + intProductID.ToString() + "' AND "
						+ MTR_CPOTable.DUEDATE_FLD + ">='" + dtmStartTime.ToString("G") + "' AND "
						+ MTR_CPOTable.DUEDATE_FLD + "<='" + dtmEndTime.ToString("G") + "'";
//						+ START_YEAR_COL + "='" + dtmStart.Year + "' AND "
//						+ START_MONTH_COL + "='" + dtmStart.Month + "'";
					// get the delivery row which have item
					DataRow[] drowsDeliver = dtbDeliveryNextMonth.Select(strDeliveryFilter);
					// get the produce row which have item
					DataRow[] drowsProduce = dtbProduceNextMonth.Select(strProduceFilter);

					#region Delivery

					if (drowsDeliver != null && drowsDeliver.Length > 0)
					{
						decimal decQuantity = 0;
						foreach (DataRow drowData in drowsDeliver)
						{
							try
							{
								decQuantity += decimal.Parse(drowData[MTR_CPOTable.QUANTITY_FLD].ToString());
							}
							catch{}
						}
						drowDelivery[MONTH_COL_PREFIX + i.ToString(DAY_FORMAT)] = decimal.Round(decQuantity, DECIMAL_ROUND_NUMBER);
						drowDelivery[CATEGORY_COL] = drowsDeliver[0][CATEGORY_COL];
						drowDelivery[ITM_CategoryTable.CATEGORYID_FLD] = drowsDeliver[0][ITM_CategoryTable.CATEGORYID_FLD];
						drowDelivery[PARTNUMBER_COLUMNNAME] = drowsDeliver[0][PARTNUMBER_COLUMNNAME];
						drowDelivery[PARTNAME_COL] = drowsDeliver[0][PARTNAME_COL];
						drowDelivery[MODEL_COL] = drowsDeliver[0][MODEL_COL];
						blnHasDelivery = true;
					}
					else
						drowDelivery[MONTH_COL_PREFIX + i.ToString(DAY_FORMAT)] = DBNull.Value;

					#endregion

					#region Produce

					if (drowsProduce != null && drowsProduce.Length > 0)
					{
						decimal decQuantity = 0;
						foreach (DataRow drowData in drowsProduce)
						{
							try
							{
								decQuantity += decimal.Parse(drowData[PRO_DCPResultDetailTable.QUANTITY_FLD].ToString());
							}
							catch{}
						}
						drowProduce[MONTH_COL_PREFIX + i.ToString(DAY_FORMAT)] = decimal.Round(decQuantity, DECIMAL_ROUND_NUMBER);
						drowProduce[CATEGORY_COL] = drowsProduce[0][CATEGORY_COL];
						drowProduce[ITM_CategoryTable.CATEGORYID_FLD] = drowsProduce[0][ITM_CategoryTable.CATEGORYID_FLD];
						drowProduce[PARTNUMBER_COLUMNNAME] = drowsProduce[0][PARTNUMBER_COLUMNNAME];
						drowProduce[PARTNAME_COL] = drowsProduce[0][PARTNAME_COL];
						drowProduce[MODEL_COL] = drowsProduce[0][MODEL_COL];
						blnHasProduce = true;
					}
					else
						drowProduce[MONTH_COL_PREFIX + i.ToString(DAY_FORMAT)] = DBNull.Value;

					#endregion
				}

				#endregion

				// check if has delivery or produce than add to report data
				if (blnHasDelivery || blnHasProduce)
				{
					decimal decDelivery = 0;
					decimal decProduce = 0;
					decimal decAvailableOfPrevDay = 0;

					#region Refine information

					if (drowDelivery[CATEGORY_COL] == DBNull.Value)
						drowDelivery[CATEGORY_COL] = drowProduce[CATEGORY_COL];
					if (drowDelivery[PARTNUMBER_COLUMNNAME] == DBNull.Value)
						drowDelivery[PARTNUMBER_COLUMNNAME] = drowProduce[PARTNUMBER_COLUMNNAME];
					else
						drowProduce[PARTNUMBER_COLUMNNAME] = drowDelivery[PARTNUMBER_COLUMNNAME];
					if (drowDelivery[PARTNAME_COL] == DBNull.Value)
						drowDelivery[PARTNAME_COL] = drowProduce[PARTNAME_COL];
					else
						drowProduce[PARTNAME_COL] = drowDelivery[PARTNAME_COL];
					if (drowDelivery[MODEL_COL] == DBNull.Value)
						drowDelivery[MODEL_COL] = drowProduce[MODEL_COL];
					else
						drowProduce[MODEL_COL] = drowDelivery[MODEL_COL];
					
					string strCategory = drowDelivery[CATEGORY_COL].ToString().Trim();
					if (strCategory.Length > CATEGORY_MAX_LENGTH)
					{
						strCategory = strCategory.Substring(0, CATEGORY_MAX_LENGTH - DOTS.Length);
						// name123 = name..
						strCategory += DOTS;
						drowDelivery[CATEGORY_COL] = strCategory;
					}

					// don't display category in produce and stock row
					drowProduce[CATEGORY_COL] = drowInventory[CATEGORY_COL] = DBNull.Value;
					// displays part name in produce row
					drowProduce[PARTNUMBER_COLUMNNAME] = drowDelivery[PARTNAME_COL];
					// displays model in stock row
					drowInventory[PARTNUMBER_COLUMNNAME] = drowDelivery[MODEL_COL];

					#endregion

					// insert delivery row
					dtbReportData.Rows.Add(drowDelivery);
					// insert produce row
					dtbReportData.Rows.Add(drowProduce);
					// add product to used list
					oarrUsedItem.Add(intProductID);

					drowInventory[ITM_ProductTable.PRODUCTID_FLD] = drowDelivery[ITM_ProductTable.PRODUCTID_FLD];
					drowInventory[ITM_CategoryTable.CATEGORYID_FLD] = drowDelivery[ITM_CategoryTable.CATEGORYID_FLD];

					#region Calculate Stock quantity for all Days in Month

					// the day columns will be the data in each day
					for (int i = 0; i < parrDays.Count; i++)
					{
						DateTime dtmDay = (DateTime)parrDays[i];
						// get delivery quantity of current day
						try
						{
							decDelivery = decimal.Parse(drowDelivery[COL_PREFIX + dtmDay.Day.ToString(DAY_FORMAT)].ToString());
						}
						catch
						{
							decDelivery = 0;
						}
						// get produce quantity of current day
						try
						{
							decProduce = decimal.Parse(drowProduce[COL_PREFIX + dtmDay.Day.ToString(DAY_FORMAT)].ToString());
						}
						catch
						{
							decProduce = 0;
						}
						// first day in month
						if (i == 0)
							decAvailableOfPrevDay = decimal.Round(decBeginQuantity + decProduce - decDelivery, DECIMAL_ROUND_NUMBER);
						else
						{
							// inventory = available quantity of previous day + produce - delivery
							decAvailableOfPrevDay = decimal.Round(decAvailableOfPrevDay + decProduce - decDelivery, DECIMAL_ROUND_NUMBER);
						}
						drowInventory[COL_PREFIX + dtmDay.Day.ToString(DAY_FORMAT)] = decAvailableOfPrevDay;
						// reset variable
						decDelivery = 0;
						decProduce = 0;
					}
					// data for missing day
//					for (int i = 1; i <= intNumOfMissDay; i++)
//						drowInventory[COL_PREFIX + (dtmLastDate.Day + i).ToString(DAY_FORMAT)] = decAvailableOfPrevDay;
					decDelivery = 0;
					decProduce = 0;
					// data for next two months
					for (int i = 1; i <= 2; i++)
					{
						// get delivery quantity of current month
						try
						{
							decDelivery = decimal.Parse(drowDelivery[MONTH_COL_PREFIX + i.ToString(DAY_FORMAT)].ToString());
						}
						catch
						{
							decDelivery = 0;
						}
						// get produce quantity of current month
						try
						{
							decProduce = decimal.Parse(drowProduce[MONTH_COL_PREFIX + i.ToString(DAY_FORMAT)].ToString());
						}
						catch
						{
							decProduce = 0;
						}
						decAvailableOfPrevDay = decimal.Round(decAvailableOfPrevDay + decProduce - decDelivery, DECIMAL_ROUND_NUMBER);
						drowInventory[MONTH_COL_PREFIX + i.ToString(DAY_FORMAT)] = decAvailableOfPrevDay;

						decDelivery = 0;
						decProduce = 0;
					}

					#endregion

					// add new row to data table
					dtbReportData.Rows.Add(drowInventory);
				}
			}
			oarrUsedItem.TrimToSize();
			
			#endregion

			#region Number of Produce product for each day

			ArrayList arrCategoriesOnDay = new ArrayList();
			foreach (DateTime dtmReportDays in parrDays)
			{
				// if current day is off day or holiday
				if (voDCPReport.Offdays.Contains(dtmReportDays.DayOfWeek) || voDCPReport.Holidays.Contains(dtmReportDays))
				{
					// there is no category on this day
					arrCategoriesOnDay.Add(0);
					// go to next day
					continue;
				}
				int intNumOfCategory = 0;
				foreach (DataRow drowData in dtbReportData.Rows)
				{
					// row is produce row
					if (int.Parse(drowData[TYPE_COLUMNNAME].ToString()) == (int)RowType.Produce)
					{
						decimal decProduceQuantity = 0;
						try
						{
							decProduceQuantity = decimal.Parse(drowData[COL_PREFIX + dtmReportDays.Day.ToString(DAY_FORMAT)].ToString());
						}
						catch{}
						// if current day has produce quantity, get the category
						if (decProduceQuantity > decimal.Zero)
						{
							// if current product has category, increase by one
							if (drowData[ITM_CategoryTable.CATEGORYID_FLD] != DBNull.Value && drowData[ITM_CategoryTable.CATEGORYID_FLD].ToString() != string.Empty)
							{
								intNumOfCategory++;
							}
						}
					}
				}
				arrCategoriesOnDay.Add(intNumOfCategory);
			}
			voDCPReport.CategoriesOnDay = arrCategoriesOnDay;
			ointNumOfWorkday = 0;
			ointNumOfOffdays = 0;
			for (int i = 8; i < dtbReportData.Columns.Count; i++)
			{
				DataColumn dcolData = dtbReportData.Columns[i];
				if (dcolData.ColumnName.ToUpper().StartsWith(MONTH_COL_PREFIX))
					continue;
				foreach (DataRow drowData in dtbReportData.Rows)
				{
					// row is produce row
					if (int.Parse(drowData[TYPE_COLUMNNAME].ToString()) == (int)RowType.Produce)
					{
						decimal decProduceQuantity = 0;
						try
						{
							decProduceQuantity = decimal.Parse(drowData[dcolData.ColumnName].ToString());
						}
						catch{}
						// if current day has produce quantity, get the category
						if (decProduceQuantity > decimal.Zero)
						{
							// increase number of work days
							ointNumOfWorkday++;
							// go to next columns
							break;
						}
					}
				}
			}
			TimeSpan ts = dtmLastDate - dtmBeginDate;
			ointNumOfOffdays = ts.Days - ointNumOfWorkday +1;

			#endregion

			oarrDayOfWeek.TrimToSize();
			oarrUsedItem.TrimToSize();
			return dtbReportData;
		}*/

		/// <summary>
		/// Mapping shift pattern to display
		/// </summary>
		/// <param name="parrShifts">Shift to map</param>
		/// <returns>Mapped Shifts</returns>
		private ArrayList MappingShift(ArrayList parrShifts)
		{
			ArrayList arrShifts = new ArrayList();
			foreach (string strShift in parrShifts)
			{
				if ((strShift.ToUpper().IndexOf("1S") >= 0) &&
					(strShift.ToUpper().IndexOf("2S") >= 0) &&
					(strShift.ToUpper().IndexOf("3S") >= 0))
				{
					arrShifts.Add("3S-Full");
				}
				else if ((strShift.ToUpper().IndexOf("1S") >= 0) &&
					(strShift.ToUpper().IndexOf("A") >= 0) &&
					(strShift.ToUpper().IndexOf("2S") >= 0) &&
					(strShift.Length == "1S,A,2S,A".Length))
				{
					arrShifts.Add("2S-C");
				}
				else if ((strShift.ToUpper().IndexOf("1S") >= 0) &&
					(strShift.ToUpper().IndexOf("A") >= 0) &&
					(strShift.ToUpper().IndexOf("2S") >= 0) &&
					(strShift.Length == "1S,A,2S".Length))
				{
					arrShifts.Add("2S-B");
				}
				else if ((strShift.ToUpper().IndexOf("1S") >= 0) &&
					(strShift.ToUpper().IndexOf("2S") >= 0) &&
					(strShift.Length == "1S,2S".Length))
				{
					arrShifts.Add("2S-A");
				}
				else if ((strShift.ToUpper().IndexOf("1S") >= 0) &&
					(strShift.ToUpper().IndexOf("A") >= 0) &&
					(strShift.Length == "1S,A".Length))
				{
					arrShifts.Add("1S-A");
				}
				else
					arrShifts.Add(strShift);
			}
			arrShifts.TrimToSize();
			return arrShifts;
		}

		/// <summary>
		/// Get working start time and end time of work center in a day
		/// </summary>
		/// <param name="pdtmCurrentDay">Current Day</param>
		/// <param name="pdtmStartTime">Start Time</param>
		/// <param name="pdtmEndTime">End Time</param>
		/// <param name="pdtmWorkingTime">Working Time</param>
		/// <param name="pintProductionLineID">Production Line</param>
		private void GetStartAndEndTime(DateTime pdtmCurrentDay, ref DateTime pdtmStartTime,
			ref DateTime pdtmEndTime, DataTable pdtmWorkingTime, int pintProductionLineID)
		{
			DataRow[] drowShifts = pdtmWorkingTime.Select(MST_WorkCenterTable.PRODUCTIONLINEID_FLD
				+ "=" + pintProductionLineID
				+ " AND " + PRO_WCCapacityTable.BEGINDATE_FLD + " <= '" + pdtmCurrentDay + "'"
				+ " AND " + PRO_WCCapacityTable.ENDDATE_FLD + " >= '" + pdtmCurrentDay + "'"
				,PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");

			if (drowShifts.Length <= 0)
			{
				return;
			}
			//change shift configured day to working day
			pdtmStartTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
			pdtmEndTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Hour,
				((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Minute,
				((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Second);
			double dblDiff = ((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).
				Subtract((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Days;
			pdtmEndTime = pdtmEndTime.AddDays(dblDiff);
		}
		/// <summary>
		/// Get working start time and end time of work center in a day
		/// </summary>
		/// <param name="pdtmCurrentDay">Current Day</param>
		/// <param name="pdtmStartTime">Start Time</param>
		/// <param name="pdtmEndTime">End Time</param>
		/// <param name="pdtmWorkingTime">Working Time</param>
		private void GetStartAndEndTime(DateTime pdtmCurrentDay, ref DateTime pdtmStartTime,
			ref DateTime pdtmEndTime, DataTable pdtmWorkingTime)
		{
			DataRow[] drowShifts = pdtmWorkingTime.Select(string.Empty, PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");

			if (drowShifts.Length <= 0)
			{
				return;
			}
			//change shift configured day to working day
			pdtmStartTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Hour,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Minute,
				((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Second);
			pdtmEndTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Hour,
				((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Minute,
				((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).Second);
			double dblDiff = ((DateTime)drowShifts[drowShifts.Length - 1][PRO_ShiftPatternTable.WORKTIMETO_FLD]).
				Subtract((DateTime)drowShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Days;
			pdtmEndTime = pdtmEndTime.AddDays(dblDiff);
		}
	}


	/// <summary>
	/// Thachnn: 17/10/2005
	/// Summary description for DCPReportBuilder.
	/// </summary>
	public class DCPReportBuilder
	{
		#region	PROPERTIES FOR BUILD REPORT "FOR RENDERING" DATATABLE

		private DataTable mdtbSourceDataTable ;
		public DataTable SourceDataTable
		{
			get
			{
				return mdtbSourceDataTable;
			}
			set
			{
				mdtbSourceDataTable = value;
			}
		}

		
		private DataTable mdtbRenderDataTable ;
		public DataTable RenderDataTable
		{
			get
			{
				return mdtbRenderDataTable;
			}
			set
			{
				mdtbRenderDataTable = value;
			}
		}

		
		private string mstrProductColumnName ;
		public string ProductColumnName
		{
			get 
			{
				return mstrProductColumnName;
			}
			set
			{
				mstrProductColumnName = value;
			}
		}


		private string mstrTypeColumnName ;
		public string TypeColumnName
		{
			get 
			{
				return mstrTypeColumnName;
			}
			set
			{
				mstrTypeColumnName = value;
			}
		}


		private string mstrReportName;
		public string ReportName
		{
			get 
			{
				return mstrReportName;
			}
			set
			{
				mstrReportName = value;
			}
		}


		#endregion

		#region	PROPERTIES FOR DISPLAYING REPORT PARAMETERS

		private int mintMonth;
		public int Month
		{
			get 
			{
				return mintMonth;
			}
			set
			{
				mintMonth = value;
			}
		}

		private int mintYear;
		public int Year
		{
			get 
			{
				return mintYear;
			}
			set
			{
				mintYear = value;
			}
		}


		private string mstrCCN;
		public string CCN
		{
			get 
			{
				return mstrCCN;
			}
			set
			{
				mstrCCN = value;
			}
		}

		private string mstrDCPCycle;
		public string DCPCycle
		{
			get 
			{
				return mstrDCPCycle;
			}
			set
			{
				mstrDCPCycle = value;
			}
		}

		private string mstrWorkCenter;
		public string WorkCenter
		{
			get 
			{
				return mstrWorkCenter;
			}
			set
			{
				mstrWorkCenter = value;
			}
		}

		private string mstrCategory;
		public string Category
		{
			get 
			{
				return mstrCategory;
			}
			set
			{
				mstrCategory = value;
			}
		}

		private string mstrPartNumber;
		public string PartNumber
		{
			get 
			{
				return mstrPartNumber;
			}
			set
			{
				mstrPartNumber = value;
			}
		}

		private string mstrPartName;
		public string PartName
		{
			get 
			{
				return mstrPartName;
			}
			set
			{
				mstrPartName = value;
			}
		}

		private string mstrModel;
		public string Model
		{
			get 
			{
				return mstrModel;
			}
			set
			{
				mstrModel = value;
			}
		}


		private int mintStartDay;
		public int StartDay
		{
			get 
			{
				return mintStartDay;
			}
			set
			{
				mintStartDay = value;
			}
		}

		private int mintEndDay;
		public int EndDay
		{
			get 
			{
				return mintEndDay;
			}
			set
			{
				mintEndDay = value;
			}
		}



		#endregion

		#region	PROPERTIES FOR RENDERING REPORT ON PREVIEW CONTROL

		private string mstrReportDefinitionFolder;
		public string ReportDefinitionFolder
		{
			get 
			{
				return mstrReportDefinitionFolder;
			}
			set
			{
				mstrReportDefinitionFolder = value;
			}
		}

		private string mstrReportFileName = "DCP Report";
		public string ReportFileName
		{
			get 
			{
				return mstrReportFileName;
			}
			set
			{
				mstrReportFileName = value;
			}
		}


		private ArrayList marrHolidays;

		public ArrayList Holidays
		{
			get { return marrHolidays; }
			set { marrHolidays = value; }
		}
		private ArrayList mOffdays;
		public ArrayList Offdays
		{
			get { return mOffdays; }
			set { mOffdays = value; }
		}

		private ArrayList marrIntersection;
		public ArrayList Intersection
		{
			get 
			{
				return marrIntersection;
			}
			set
			{
				marrIntersection = value;
			}
		}

		private ArrayList marrShift;
		public ArrayList Shift
		{
			get 
			{
				return marrShift;
			}
			set
			{
				marrShift = value;
			}
		}

		private ArrayList marrStandardCapacity;
		public ArrayList StandardCapacity
		{
			get 
			{
				return marrStandardCapacity;
			}
			set
			{
				marrStandardCapacity = value;
			}
		}

		private ArrayList marrTotalRequiredCapacity;
		public ArrayList TotalRequiredCapacity
		{
			get 
			{
				return marrTotalRequiredCapacity;
			}
			set
			{
				marrTotalRequiredCapacity = value;
			}
		}

		private ArrayList marrTotalChangeTime;
		public ArrayList TotalChangeTime
		{
			get 
			{
				return marrTotalChangeTime;
			}
			set
			{
				marrTotalChangeTime = value;
			}
		}

		private ArrayList marrTotalCheckpointTime;
		public ArrayList TotalCheckpointTime
		{
			get 
			{
				return marrTotalCheckpointTime;
			}
			set
			{
				marrTotalCheckpointTime = value;
			}
		}

		private ArrayList marrEffective;
		public ArrayList Effective
		{
			get 
			{
				return marrEffective;
			}
			set
			{
				marrEffective = value;
			}
		}

		private ArrayList marrRemainCapacity;
		public ArrayList RemainCapacity
		{
			get 
			{
				return marrRemainCapacity;
			}
			set
			{
				marrRemainCapacity = value;
			}
		}

		private ArrayList marrCategoriesOnDay;
		public ArrayList CategoriesOnDay
		{
			get 
			{
				return marrCategoriesOnDay;
			}
			set
			{
				marrCategoriesOnDay = value;
			}
		}

		private ArrayList marrReportDates;
		public ArrayList ReportDates
		{
			get { return marrReportDates; }
			set { marrReportDates = value; }
		}

		private ArrayList marrDayOfWeek;
		private int mNumOfWorkdays;
		private bool mViewOneShift;

		public bool ViewOneShift
		{
			get { return mViewOneShift; }
			set { mViewOneShift = value; }
		}

		public int NumOfWorkdays
		{
			get { return mNumOfWorkdays; }
			set { mNumOfWorkdays = value; }
		}

		private int mNumOfOffdays;

		public int NumOfOffdays
		{
			get { return mNumOfOffdays; }
			set { mNumOfOffdays = value; }
		}

		public ArrayList ReportDayOfWeek
		{
			get { return marrDayOfWeek; }
			set { marrDayOfWeek = value; }
		}

		private C1.Win.C1Preview.C1PrintPreviewControl mppvReportViewer;
		private PrintPreviewDialog mppdViewer;

		public PrintPreviewDialog ViewerDialog
		{
			get { return mppdViewer; }
			set { mppdViewer = value; }
		}

		public C1.Win.C1Preview.C1PrintPreviewControl ReportViewer
		{
			get 
			{
				return mppvReportViewer;
			}
			set
			{
				mppvReportViewer = value;
			}
		}


		private C1.C1Report.C1Report mrptDCPReport = new C1Report();

		#endregion

		/// <summary>
		/// Thachnn: 17/10/2005
		/// Standard empty Contructor
		/// </summary>
		public DCPReportBuilder()
		{
		}

		
		/// <summary>
		/// Thachnn: 17/10/2005
		/// Helper constructor, short and easy to build DCPReportObject with a single code line.
		/// </summary>
		/// <param name="pdtbSource">Source DataTable get from Dynamic report object</param>
		/// <param name="pstrProductColumnName">Product column name</param>
		/// <param name="pstrTypeColumnName">Row type column name</param>
		/// <param name="pstrReportDefinitionFolder">report definition folder</param>
		/// <param name="pstrReportFileName">report layout file name</param>
		public DCPReportBuilder(DataTable pdtbSource, 
			string pstrProductColumnName, 
			string pstrTypeColumnName, 
			string pstrReportDefinitionFolder, 
			string pstrReportFileName,
			string pstrCCN,
			string pstrDCPCycle,
			int pintMonth,
			int pintYear,
			string pstrWorkCenter,
			string pstrCategory,
			string pstrPartNumber,
			string pstrPartName,
			string pstrModel			
			)
		{			
			mdtbSourceDataTable = pdtbSource;
			mstrProductColumnName = pstrProductColumnName;
			mstrTypeColumnName = pstrTypeColumnName ;
			mstrReportDefinitionFolder = pstrReportDefinitionFolder ;
			mstrReportFileName = pstrReportFileName;						
			mstrCCN = pstrCCN;
			mstrDCPCycle = pstrDCPCycle;
			mintMonth = pintMonth;
			mintYear = pintYear;			
			mstrWorkCenter = pstrWorkCenter;
			mstrCategory = pstrCategory;
			mstrPartNumber = pstrPartNumber;
			mstrPartName = pstrPartName;
			mstrModel = pstrModel;
		}


		/// <summary>
		/// /// Thachnn: 17/10/2005
		/// Analyse the Source DataTable, make some changes to easily render the DCPReport.
		/// This function will make change to the RenderDataTable Property.
		/// THROW: Exception
		/// </summary>
		/// <returns>DataTable that will be using (by this DCPReportBuilder) for rendering on mppvReportViewer object</returns>
		public DataTable MakeDataTableForRender()
		{	
			try
			{			
				mdtbRenderDataTable = mdtbSourceDataTable.Copy();
				//ArrayList arrProductList = GetColumnValuesFromTable(mdtbSourceDataTable,mstrProductColumnName);
				//CreateDummyRowForDCPProduct(ref mdtbRenderDataTable ,arrProductList,mstrProductColumnName,mstrTypeColumnName);
				//mdtbRenderDataTable = GetSortedDCPDataTableFromDataTable(mdtbRenderDataTable,mstrProductColumnName,mstrTypeColumnName);
				return mdtbRenderDataTable;
			}
			catch(Exception ex)
			{
				throw ex;
			}			
		}
		
		/// <summary>
		/// /// Thachnn: 17/10/2005
		/// Render the DCPReport using mdtbRenderDataTable on mppvReportViewer object
		/// THROW: Exception
		/// </summary>
		public void RenderReport()
		{
			#region constants

			const int DEFAULT_COLOR_NUMBER = 200;
			const int BLUE_COLOR_NUMBER = DEFAULT_COLOR_NUMBER / 2;
			const int FIELD_WIDTH = 20000;
			const int FIELD_HEIGHT = 300;
			const int FIELD_TOP = 50;

			#endregion
			
			try
			{
				#region RENDER WITH DEFINITION FILE	

				// prevent reentrant calls
				if (mrptDCPReport.IsBusy)
				{
					return;
				}
				
				// initialize control
				mrptDCPReport.Clear();		// clear any existing fields
				mrptDCPReport.ReportName = mstrReportName; //"DCP Report";

				string[] arrstrReportInDefinitionFile = mrptDCPReport.GetReportInfo( mstrReportDefinitionFolder + "\\" + mstrReportFileName);
				mrptDCPReport.Load(mstrReportDefinitionFolder + "\\" + mstrReportFileName, arrstrReportInDefinitionFile[0]);
	
				#endregion	

				#region PUSH PARAMETER VALUE

				// HACK: dungla 10-21-2005
				// header information get from system params
				try
				{
					mrptDCPReport.Fields["fldCompany"].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_NAME);
				}
				catch{}
				try
				{
					mrptDCPReport.Fields["fldAddress"].Text = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
				}
				catch{}
				try
				{
					mrptDCPReport.Fields["fldTel"].Text = SystemProperty.SytemParams.Get(SystemParam.TEL);
				}
				catch{}
				try
				{
					mrptDCPReport.Fields["fldFax"].Text = SystemProperty.SytemParams.Get(SystemParam.FAX);
				}
				catch{}
				// END: dungla 10-21-2005
				// UNDONE: Thachnn: get from inputed report parameter
				try
				{
					mrptDCPReport.Fields["fldParameterMonth"].Text		= mintMonth.ToString("00");
				}
				catch{}
				try
				{
					mrptDCPReport.Fields["fldParameterYear"].Text		= mintYear.ToString("0000");
				}
				catch{}

				try
				{
					mrptDCPReport.Fields["fldParameterCCN"].Text		= mstrCCN;
				}
				catch{}
				try
				{
					mrptDCPReport.Fields["fldParameterDCPCycle"].Text	= mstrDCPCycle;
				}
				catch{}
				try
				{
					mrptDCPReport.Fields["fldParameterWorkCenter"].Text = mstrWorkCenter;
				}
				catch{}
				try
				{
					mrptDCPReport.Fields["fldParameterCategory"].Text	= mNumOfWorkdays.ToString("00");
				}
				catch{}
				try
				{
					mrptDCPReport.Fields["fldParameterPartNumber"].Text = mNumOfOffdays.ToString("00");
				}
				catch{}
				try
				{
					mrptDCPReport.Fields["fldParameterPartName"].Text	= mstrPartName;
				}
				catch{}
				try
				{
					mrptDCPReport.Fields["fldParameterModel"].Text		= mstrModel;
				}
				catch{}
                
				#endregion

				#region arrShifts
				int nDayCounter = mintStartDay;
				for(int i = 0; i < marrShift.Count; i++)
				{
					try
					{
						if(nDayCounter <= mintEndDay)
						{
							//string str = marrShift[i].ToString();
							mrptDCPReport.Fields["fldShiftD"+nDayCounter.ToString("00")].Text = marrShift[i].ToString();
							nDayCounter++;
						}
					}
					catch//(Exception ex)
					{
						//string strasdf = ex.Message;
						//break;
					}
				}
				#endregion

				#region arrDayOfWeek
				nDayCounter = mintStartDay;
				for(int i = 0; i < marrDayOfWeek.Count; i++)
				{
					try
					{
						if(nDayCounter <= mintEndDay)
						{
							mrptDCPReport.Fields["fldDayD" + nDayCounter.ToString("00")].Text = marrDayOfWeek[i].ToString();
							nDayCounter++;
						}
					}
					catch
					{
					}
				}
				#endregion
				
				#region arrStandardCapacity				
				nDayCounter = mintStartDay;
				for(int i = 0; i < marrStandardCapacity.Count; i++)
				{
					try
					{
						if(nDayCounter <= mintEndDay)
						{
							if (marrStandardCapacity[i].ToString() != "0")
								mrptDCPReport.Fields["fldStandardCapacityD" + nDayCounter.ToString("00")].Text = decimal.Parse(marrStandardCapacity[i].ToString()).ToString(Constants.INTERGER_NUMBERFORMAT);
							else
								mrptDCPReport.Fields["fldStandardCapacityD" + nDayCounter.ToString("00")].Text = string.Empty;
							nDayCounter++;
						}
					}
					catch//(Exception ex)
					{
						//string strasdf = ex.Message;
						//break;
					}
				}
				#endregion
				
				#region arrTotalRequiredCapacity 
				nDayCounter = mintStartDay;
				for(int i = 0; i < marrTotalRequiredCapacity.Count; i++)
				{
					try
					{
						if(nDayCounter <= mintEndDay)
						{
							if (marrTotalRequiredCapacity[i].ToString() != "0")
								mrptDCPReport.Fields["fldTotalRequiredCapacityD"+nDayCounter.ToString("00")].Text = decimal.Parse(marrTotalRequiredCapacity[i].ToString()).ToString(Constants.INTERGER_NUMBERFORMAT);
							else
								mrptDCPReport.Fields["fldTotalRequiredCapacityD"+nDayCounter.ToString("00")].Text = string.Empty;
							nDayCounter++;
						}
					}
					catch//(Exception ex)
					{
						//string strasdf = ex.Message;
						//break;
					}
				}
				#endregion
				
				#region arrTotalChangeTime 
				nDayCounter = mintStartDay;
				for(int i = 0; i < marrTotalChangeTime.Count; i++)
				{
					try
					{
						if(nDayCounter <= mintEndDay)
						{
							if (marrTotalChangeTime[i].ToString() != "0")
								mrptDCPReport.Fields["fldChangeCategoryTimeD"+nDayCounter.ToString("00")].Text = decimal.Parse(marrTotalChangeTime[i].ToString()).ToString(Constants.INTERGER_NUMBERFORMAT);
							else
								mrptDCPReport.Fields["fldChangeCategoryTimeD"+nDayCounter.ToString("00")].Text = string.Empty;
							nDayCounter++;
						}
					}
					catch//(Exception ex)
					{
						//string strasdf = ex.Message;
						//break;
					}
				}
				#endregion
				
				#region arrTotalCheckpointTime 
				nDayCounter = mintStartDay;
				for(int i = 0; i < marrTotalCheckpointTime.Count; i++)
				{
					try
					{
						if(nDayCounter <= mintEndDay)
						{
							if (marrTotalCheckpointTime[i].ToString() != "0")
								mrptDCPReport.Fields["fldCheckpointTimeD"+nDayCounter.ToString("00")].Text = decimal.Parse(marrTotalCheckpointTime[i].ToString()).ToString(Constants.INTERGER_NUMBERFORMAT);
							else
								mrptDCPReport.Fields["fldCheckpointTimeD"+nDayCounter.ToString("00")].Text = string.Empty;
							nDayCounter++;
						}
					}
					catch//(Exception ex)
					{
						//string strasdf = ex.Message;
						//break;
					}
				}
				#endregion
				
				#region arrEffective 
				nDayCounter = mintStartDay;
				for(int i = 0; i < marrEffective.Count; i++)
				{
					try
					{
						if(nDayCounter <= mintEndDay)
						{
							if (marrEffective[i].ToString() != "0")
								mrptDCPReport.Fields["fldEffectiveD"+nDayCounter.ToString("00")].Text = decimal.Parse(marrEffective[i].ToString()).ToString(Constants.INTERGER_NUMBERFORMAT);
							else
								mrptDCPReport.Fields["fldEffectiveD"+nDayCounter.ToString("00")].Text = string.Empty;
							nDayCounter++;
						}
					}
					catch//(Exception ex)
					{
						//string strasdf = ex.Message;
						//break;
					}
				}
				#endregion
	
				#region arrRemainCapacity 
				nDayCounter = mintStartDay;
				for(int i = 0; i < marrRemainCapacity.Count; i++)
				{
					try
					{
						if(nDayCounter <= mintEndDay)
						{
							if (marrRemainCapacity[i].ToString() != "0")
								mrptDCPReport.Fields["fldRemainCapacityD"+nDayCounter.ToString("00")].Text = decimal.Parse(marrRemainCapacity[i].ToString()).ToString(Constants.INTERGER_NUMBERFORMAT);
							else
								mrptDCPReport.Fields["fldRemainCapacityD"+nDayCounter.ToString("00")].Text = string.Empty;
							nDayCounter++;
						}
					}
					catch//(Exception ex)
					{
						//string strasdf = ex.Message;
						//break;
					}
				}
				#endregion
				
				#region arrCategoriesOnDay
				nDayCounter = mintStartDay;
				for(int i = 0; i < marrCategoriesOnDay.Count; i++)
				{
					try
					{
						if(nDayCounter <= mintEndDay)
						{
							if (marrCategoriesOnDay[i].ToString() != "0")
								mrptDCPReport.Fields["fldCategoriesOnDayD"+nDayCounter.ToString("00")].Text = decimal.Parse(marrCategoriesOnDay[i].ToString()).ToString(Constants.INTERGER_NUMBERFORMAT);
							else
								mrptDCPReport.Fields["fldCategoriesOnDayD"+nDayCounter.ToString("00")].Text = string.Empty;
							nDayCounter++;
						}
					}
					catch//(Exception ex)
					{
						//string strasdf = ex.Message;
						//break;
					}
				}
				#endregion

				#region Next two month label
				DateTime dtmDate = (DateTime)marrReportDates[0];
				try
				{
					mrptDCPReport.Fields["M1Lbl"].Text = dtmDate.AddMonths(1).ToString(Constants.MONTH_YEAR_FORMAT);
					mrptDCPReport.Fields["M2Lbl"].Text = dtmDate.AddMonths(2).ToString(Constants.MONTH_YEAR_FORMAT);
				}
				catch{}
				#endregion

				#region Refine report layout based on holidays, num of days in month

				// get num of days in month
				int intDaysInMonth = DateTime.DaysInMonth(mintYear, mintMonth);
				DateTime dtmStart = new DateTime(mintYear, mintMonth, 1);
				string strMonth = dtmStart.ToString("MMM");

				#region Fill date label and color of off day

				StringBuilder sbSum1Total = new StringBuilder();
				StringBuilder sbSum2Total = new StringBuilder();
				StringBuilder sbSum3Total = new StringBuilder();
				StringBuilder sbSumCategoryTotal = new StringBuilder();
				StringBuilder sbSumSCTotal = new StringBuilder();
				StringBuilder sbSumTRCTotal = new StringBuilder();
				StringBuilder sbSumRCTotal = new StringBuilder();
				StringBuilder sbSumEffectTotal = new StringBuilder();
				StringBuilder sbSumDetailTotal = new StringBuilder();
				for (int i = 1; i <= intDaysInMonth; i++)
				{
					string strDate = "D" + i.ToString("00") + "Lbl";
					string strDay = "fldDayD" + i.ToString("00");
					DateTime dtmDay = new DateTime(mintYear, mintMonth, i);
					try
					{
						mrptDCPReport.Fields[strDate].Text = i + "-" + strMonth;
					}
					catch{}
					if (mOffdays.Contains(dtmDay.DayOfWeek) || marrHolidays.Contains(dtmDay))
					{
						try
						{
							mrptDCPReport.Fields[strDate].ForeColor = Color.Red;
							mrptDCPReport.Fields[strDate].BackColor = Color.Yellow;
						}
						catch
						{
						}
						try
						{
							mrptDCPReport.Fields[strDay].ForeColor = Color.Red;
							mrptDCPReport.Fields[strDay].BackColor = Color.Yellow;
						}
						catch
						{
						}
					}
					sbSum1Total.Append("fldSum1D" + i.ToString("00")).Append(" + ");
					sbSum2Total.Append("fldSum2D" + i.ToString("00")).Append(" + ");
					sbSum3Total.Append("fldSum3D" + i.ToString("00")).Append(" + ");
					sbSumCategoryTotal.Append("fldCategoriesOnDayD" + i.ToString("00")).Append(" + ");
					sbSumSCTotal.Append("fldStandardCapacityD" + i.ToString("00")).Append(" + ");
					sbSumTRCTotal.Append("fldTotalRequiredCapacityD" + i.ToString("00")).Append(" + ");
					sbSumRCTotal.Append("fldRemainCapacityD" + i.ToString("00")).Append(" + ");
					sbSumEffectTotal.Append("fldEffectiveD" + i.ToString("00")).Append(" + ");
					sbSumDetailTotal.Append("D" + i.ToString("00") + "Ctl").Append(" + ");
				}
				for (int i = 1; i <= 2; i++)
				{
					sbSum1Total.Append("fldSum1M" + i.ToString("00")).Append(" + ");
					sbSum2Total.Append("fldSum2M" + i.ToString("00")).Append(" + ");
					sbSum3Total.Append("fldSum3M" + i.ToString("00")).Append(" + ");
					sbSumCategoryTotal.Append("fldCategoriesOnDayM" + i.ToString("00")).Append(" + ");
					sbSumSCTotal.Append("fldStandardCapacityM" + i.ToString("00")).Append(" + ");
					sbSumTRCTotal.Append("fldTotalRequiredCapacityM" + i.ToString("00")).Append(" + ");
					sbSumRCTotal.Append("fldRemainCapacityM" + i.ToString("00")).Append(" + ");
					sbSumEffectTotal.Append("fldEffectiveM" + i.ToString("00")).Append(" + ");
					sbSumDetailTotal.Append("M" + i.ToString("00") + "Ctl").Append(" + ");
				}
				try
				{
					mrptDCPReport.Fields["fldSum1Total"].Text = sbSum1Total.ToString(0, sbSum1Total.Length - 1);
					mrptDCPReport.Fields["fldSuTotalTotal"].Text = sbSum2Total.ToString(0, sbSum2Total.Length - 1);
					mrptDCPReport.Fields["fldSum3Total"].Text = sbSum3Total.ToString(0, sbSum3Total.Length - 1);
					mrptDCPReport.Fields["fldCategoriesOnDayTotal"].Text = sbSumCategoryTotal.ToString(0, sbSumCategoryTotal.Length - 1);
					mrptDCPReport.Fields["fldStandardCapacityTotal"].Text = sbSumSCTotal.ToString(0, sbSumSCTotal.Length - 1);
					mrptDCPReport.Fields["fldTotalRequiredCapacityTotal"].Text = sbSumTRCTotal.ToString(0, sbSum1Total.Length - 1);
					mrptDCPReport.Fields["fldRemainCapacityTotal"].Text = sbSumRCTotal.ToString(0, sbSum1Total.Length - 1);
					mrptDCPReport.Fields["fldEffectiveTotal"].Text = sbSumEffectTotal.ToString(0, sbSum1Total.Length - 1);
					mrptDCPReport.Fields["fldTotal"].Text = sbSumDetailTotal.ToString(0, sbSum1Total.Length - 1);
				}
				catch{}

				#endregion

				#region Layout the format based on days in month
				if (intDaysInMonth < 31)
				{
					for (int i = intDaysInMonth + 1; i <= 31; i++)
					{
						string strDate = "D" + i.ToString("00") + "Lbl";
						string strDayOfWeek = "fldDayD" + i.ToString("00");
						string strShift = "fldShiftD" + i.ToString("00");
						string strDiv = "div" + i.ToString("00");
						string strDivPage = "divPage" + i.ToString("00");
						string strDivDetail = "divDetail" + i.ToString("00");
						string strDetail = "D" + i.ToString("00") + "Ctl";
						string strSum1 = "fldSum1D" + i.ToString("00");
						string strSum2 = "fldSum2D" + i.ToString("00");
						string strSum3 = "fldSum3D" + i.ToString("00");
						string strCategories = "fldCategoriesOnDayD" + i.ToString();
						string strSC = "fldStandardCapacityD" + i.ToString();
						string strTRC = "fldTotalRequiredCapacityD" + i.ToString();
						string strRC = "fldRemainCapacityD" + i.ToString();
						string strEffect = "fldEffectiveD" + i.ToString();

						#region Report Header

						try
						{
							mrptDCPReport.Fields[strShift].Visible = false;
						}
						catch
						{}
						try
						{
							mrptDCPReport.Fields[strDivPage].Visible = false;
						}
						catch
						{}
						try
						{
							mrptDCPReport.Fields[strSum1].Visible = false;
						}
						catch
						{}
						try
						{
							mrptDCPReport.Fields[strSum2].Visible = false;
						}
						catch
						{}
						try
						{
							mrptDCPReport.Fields[strSum3].Visible = false;
						}
						catch
						{}
						try
						{
							mrptDCPReport.Fields[strCategories].Visible = false;
						}
						catch
						{}
						try
						{
							mrptDCPReport.Fields[strSC].Visible = false;
						}
						catch
						{}
						try
						{
							mrptDCPReport.Fields[strTRC].Visible = false;
						}
						catch
						{}
						try
						{
							mrptDCPReport.Fields[strRC].Visible = false;
						}
						catch
						{}
						try
						{
							mrptDCPReport.Fields[strEffect].Visible = false;
						}
						catch
						{}

						#endregion

						#region Page Header

						try
						{
							mrptDCPReport.Fields[strDate].Visible = false;
						}
						catch
						{}
						try
						{
							mrptDCPReport.Fields[strDayOfWeek].Visible = false;
						}
						catch
						{}
						try
						{
							mrptDCPReport.Fields[strDiv].Visible = false;
						}
						catch
						{}

						#endregion

						#region Detail

						try
						{
							mrptDCPReport.Fields[strDivDetail].Visible = false;
						}
						catch
						{}
						try
						{
							mrptDCPReport.Fields[strDetail].Visible = false;
						}
						catch
						{}

						#endregion
					}
					try
					{
						#region Resize all line

						double dWidth = mrptDCPReport.Fields["line1"].Width;
						for (int i = 1; i <= 9; i++)
							mrptDCPReport.Fields["line" + i].Width = dWidth - (31 - intDaysInMonth)*450;
						mrptDCPReport.Fields["line10"].Width = 
							mrptDCPReport.Fields["line13"].Width = mrptDCPReport.Fields["line10"].Width - (31 - intDaysInMonth)*450;
						mrptDCPReport.Fields["line11"].Width = dWidth - (31 - intDaysInMonth)*450;
						mrptDCPReport.Fields["line12"].Width = dWidth - (31 - intDaysInMonth)*450;
						mrptDCPReport.Fields["line14"].Width = mrptDCPReport.Fields["line14"].Width - (31 - intDaysInMonth)*450;

						#endregion

						// gets new line width in report header
						dWidth = mrptDCPReport.Fields["line1"].Width;

						#region moving rest of field in report header

						#region M1 columns
						mrptDCPReport.Fields["fldSum1M01"].Left = 
							mrptDCPReport.Fields["fldSum2M01"].Left = 
							mrptDCPReport.Fields["fldSum3M01"].Left = 
							mrptDCPReport.Fields["fldCategoriesOnDayM01"].Left = 
							mrptDCPReport.Fields["fldStandardCapacityM01"].Left = 
							mrptDCPReport.Fields["fldTotalRequiredCapacityM01"].Left = 
							mrptDCPReport.Fields["fldRemainCapacityM01"].Left = 
							mrptDCPReport.Fields["fldEffectiveM01"].Left = 
							mrptDCPReport.Fields["M01Lbl"].Left = 
							mrptDCPReport.Fields["M01Ctl"].Left = mrptDCPReport.Fields["fldSum1M01"].Left - (31 - intDaysInMonth)*450;
						mrptDCPReport.Fields["divM1"].Left = 
							mrptDCPReport.Fields["divPageM1"].Left = 
							mrptDCPReport.Fields["divDetailM1"].Left = mrptDCPReport.Fields["divDetailM1"].Left - (32 - intDaysInMonth)*450;
						#endregion

						#region M2 columns
						mrptDCPReport.Fields["fldSum1M02"].Left = 
							mrptDCPReport.Fields["fldSum2M02"].Left = 
							mrptDCPReport.Fields["fldSum3M02"].Left = 
							mrptDCPReport.Fields["fldCategoriesOnDayM02"].Left = 
							mrptDCPReport.Fields["fldStandardCapacityM02"].Left = 
							mrptDCPReport.Fields["fldTotalRequiredCapacityM02"].Left = 
							mrptDCPReport.Fields["fldRemainCapacityM02"].Left = 
							mrptDCPReport.Fields["fldEffectiveM02"].Left = 
							mrptDCPReport.Fields["M02Lbl"].Left = 
							mrptDCPReport.Fields["M02Ctl"].Left = mrptDCPReport.Fields["fldSum1M02"].Left - (31 - intDaysInMonth)*450;
						mrptDCPReport.Fields["divM2"].Left = 
							mrptDCPReport.Fields["divPageM2"].Left = 
							mrptDCPReport.Fields["divDetailM2"].Left = mrptDCPReport.Fields["divDetailM2"].Left - (32 - intDaysInMonth)*450;
						#endregion

						#region Total columns
						mrptDCPReport.Fields["fldSum1Total"].Left = 
							mrptDCPReport.Fields["fldSuTotalTotal"].Left = 
							mrptDCPReport.Fields["fldSum3Total"].Left = 
							mrptDCPReport.Fields["fldCategoriesOnDayTotal"].Left = 
							mrptDCPReport.Fields["fldStandardCapacityTotal"].Left = 
							mrptDCPReport.Fields["fldTotalRequiredCapacityTotal"].Left = 
							mrptDCPReport.Fields["fldRemainCapacityTotal"].Left = 
							mrptDCPReport.Fields["fldEffectiveTotal"].Left = 
							mrptDCPReport.Fields["lblTotal"].Left = 
							mrptDCPReport.Fields["fldTotal"].Left = mrptDCPReport.Fields["fldSum1Total"].Left - (31 - intDaysInMonth)*450;
						mrptDCPReport.Fields["divTotal"].Left = 
							mrptDCPReport.Fields["divPageTotal"].Left = 
							mrptDCPReport.Fields["divDetailTotal"].Left = mrptDCPReport.Fields["divDetailTotal"].Left - (32 - intDaysInMonth)*450;
						#endregion

						#endregion
					}
					catch
					{}
				}

				#endregion

				#endregion
				
				// set datasource object that provides data to report.
				mrptDCPReport.DataSource.Recordset = mdtbRenderDataTable;

				// render the report into the PrintPreviewControl
				mppvReportViewer.Document = mrptDCPReport.Document;
			}			
			catch (Exception ex)
			{
				throw ex;
			}						
		}

	}
}