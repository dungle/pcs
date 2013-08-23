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


namespace PCSMaterials.Mps
{
	/// <summary>
	/// Summary description for CPOReport.
	/// </summary>
	public class CPOReport : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnCategory;
		private System.Windows.Forms.Button btnMasLoc;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.Label lblMasLoc;
		private System.Windows.Forms.TextBox txtCategory;
		public System.Windows.Forms.TextBox txtMasLoc;
		private System.Windows.Forms.Label lblCCN;
		public C1.Win.C1List.C1Combo cboCCN;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region My variables
		const string THIS = "PCSMaterials.Mps.CPOReport";
		private const string ZERO_STRING = "0";

		const string PREFIX_DAYINMONTH = "lblDayInMonth";
		const string PREFIX_DAYOFWEEK = "lblDayOfWeek";
		const string FLD = "fld";


		private System.Windows.Forms.ComboBox cboMonth;
		private System.Windows.Forms.Label lblMonth;
		private System.Windows.Forms.Label lblYear;
		private System.Windows.Forms.ComboBox cboYear;
		private System.Windows.Forms.Label lblCycle;
		public System.Windows.Forms.TextBox txtCycle;
		private System.Windows.Forms.Button btnProductionLine;
		private System.Windows.Forms.Label lblProductionLine;
		public System.Windows.Forms.TextBox txtProductionLine;
		private System.Windows.Forms.Button btnCycle;
		public System.Windows.Forms.ComboBox cboPlanType;
		private System.Windows.Forms.Label lblPlanType;
		private System.Windows.Forms.Label lblMPSReportTitle;
		private System.Windows.Forms.Label lblMRPReportTitle;
		private System.Windows.Forms.Button btnModel;
		private System.Windows.Forms.Button btnPartName;
		private System.Windows.Forms.Label lblModel;
		private System.Windows.Forms.Label lblPartName;
		public System.Windows.Forms.TextBox txtPartName;
		public System.Windows.Forms.TextBox txtVendor;
		private System.Windows.Forms.Label lblVendor;
		private System.Windows.Forms.Button btnVendor;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.Label lblPartNo;
		public System.Windows.Forms.TextBox txtPartNo;
		private System.Windows.Forms.Button btnPartNo;
		UtilsBO boUtil = new UtilsBO();
		#endregion

		public CPOReport()
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
			this.btnMasLoc = new System.Windows.Forms.Button();
			this.lblCategory = new System.Windows.Forms.Label();
			this.lblMasLoc = new System.Windows.Forms.Label();
			this.txtCategory = new System.Windows.Forms.TextBox();
			this.txtMasLoc = new System.Windows.Forms.TextBox();
			this.lblCCN = new System.Windows.Forms.Label();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.cboMonth = new System.Windows.Forms.ComboBox();
			this.lblMonth = new System.Windows.Forms.Label();
			this.lblYear = new System.Windows.Forms.Label();
			this.cboYear = new System.Windows.Forms.ComboBox();
			this.lblCycle = new System.Windows.Forms.Label();
			this.txtCycle = new System.Windows.Forms.TextBox();
			this.btnProductionLine = new System.Windows.Forms.Button();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.btnCycle = new System.Windows.Forms.Button();
			this.cboPlanType = new System.Windows.Forms.ComboBox();
			this.lblPlanType = new System.Windows.Forms.Label();
			this.lblMPSReportTitle = new System.Windows.Forms.Label();
			this.lblMRPReportTitle = new System.Windows.Forms.Label();
			this.btnModel = new System.Windows.Forms.Button();
			this.btnPartName = new System.Windows.Forms.Button();
			this.lblModel = new System.Windows.Forms.Label();
			this.lblPartName = new System.Windows.Forms.Label();
			this.txtPartName = new System.Windows.Forms.TextBox();
			this.txtVendor = new System.Windows.Forms.TextBox();
			this.lblVendor = new System.Windows.Forms.Label();
			this.btnVendor = new System.Windows.Forms.Button();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.lblPartNo = new System.Windows.Forms.Label();
			this.txtPartNo = new System.Windows.Forms.TextBox();
			this.btnPartNo = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			this.SuspendLayout();
			// 
			// btnPrint
			// 
			this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPrint.Location = new System.Drawing.Point(8, 168);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(60, 23);
			this.btnPrint.TabIndex = 32;
			this.btnPrint.Text = "&Execute";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(366, 168);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 33;
			this.btnHelp.Text = "&Help";
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(426, 168);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 34;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnCategory
			// 
			this.btnCategory.Location = new System.Drawing.Point(220, 141);
			this.btnCategory.Name = "btnCategory";
			this.btnCategory.Size = new System.Drawing.Size(22, 20);
			this.btnCategory.TabIndex = 19;
			this.btnCategory.Text = "...";
			this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
			// 
			// btnMasLoc
			// 
			this.btnMasLoc.Location = new System.Drawing.Point(220, 119);
			this.btnMasLoc.Name = "btnMasLoc";
			this.btnMasLoc.Size = new System.Drawing.Size(22, 20);
			this.btnMasLoc.TabIndex = 16;
			this.btnMasLoc.Text = "...";
			this.btnMasLoc.Click += new System.EventHandler(this.btnMasLoc_Click);
			// 
			// lblCategory
			// 
			this.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCategory.Location = new System.Drawing.Point(6, 141);
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.Size = new System.Drawing.Size(94, 20);
			this.lblCategory.TabIndex = 17;
			this.lblCategory.Text = "Category";
			this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMasLoc
			// 
			this.lblMasLoc.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblMasLoc.Location = new System.Drawing.Point(6, 119);
			this.lblMasLoc.Name = "lblMasLoc";
			this.lblMasLoc.Size = new System.Drawing.Size(94, 20);
			this.lblMasLoc.TabIndex = 14;
			this.lblMasLoc.Text = "Master Loccation";
			this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtCategory
			// 
			this.txtCategory.Location = new System.Drawing.Point(98, 141);
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.Size = new System.Drawing.Size(120, 20);
			this.txtCategory.TabIndex = 18;
			this.txtCategory.Text = "";
			this.txtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategory_KeyDown);
			this.txtCategory.Validating += new System.ComponentModel.CancelEventHandler(this.txtCategory_Validating);
			// 
			// txtMasLoc
			// 
			this.txtMasLoc.Location = new System.Drawing.Point(98, 119);
			this.txtMasLoc.Name = "txtMasLoc";
			this.txtMasLoc.Size = new System.Drawing.Size(120, 20);
			this.txtMasLoc.TabIndex = 15;
			this.txtMasLoc.Text = "";
			this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
			this.txtMasLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasLoc_Validating);
			// 
			// lblCCN
			// 
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.Location = new System.Drawing.Point(378, 7);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(32, 20);
			this.lblCCN.TabIndex = 0;
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
			this.cboCCN.Location = new System.Drawing.Point(406, 6);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(80, 21);
			this.cboCCN.TabIndex = 1;
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
			this.cboMonth.Location = new System.Drawing.Point(98, 52);
			this.cboMonth.Name = "cboMonth";
			this.cboMonth.Size = new System.Drawing.Size(76, 21);
			this.cboMonth.TabIndex = 7;
			// 
			// lblMonth
			// 
			this.lblMonth.ForeColor = System.Drawing.Color.Maroon;
			this.lblMonth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMonth.Location = new System.Drawing.Point(6, 50);
			this.lblMonth.Name = "lblMonth";
			this.lblMonth.Size = new System.Drawing.Size(94, 23);
			this.lblMonth.TabIndex = 6;
			this.lblMonth.Text = "Month";
			this.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblYear
			// 
			this.lblYear.ForeColor = System.Drawing.Color.Maroon;
			this.lblYear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblYear.Location = new System.Drawing.Point(6, 27);
			this.lblYear.Name = "lblYear";
			this.lblYear.Size = new System.Drawing.Size(94, 23);
			this.lblYear.TabIndex = 4;
			this.lblYear.Text = "Year";
			this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboYear
			// 
			this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboYear.ItemHeight = 13;
			this.cboYear.Location = new System.Drawing.Point(98, 29);
			this.cboYear.Name = "cboYear";
			this.cboYear.Size = new System.Drawing.Size(76, 21);
			this.cboYear.TabIndex = 5;
			// 
			// lblCycle
			// 
			this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
			this.lblCycle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCycle.Location = new System.Drawing.Point(6, 71);
			this.lblCycle.Name = "lblCycle";
			this.lblCycle.Size = new System.Drawing.Size(94, 23);
			this.lblCycle.TabIndex = 8;
			this.lblCycle.Text = "Cycle";
			this.lblCycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtCycle
			// 
			this.txtCycle.Location = new System.Drawing.Point(98, 75);
			this.txtCycle.Name = "txtCycle";
			this.txtCycle.Size = new System.Drawing.Size(120, 20);
			this.txtCycle.TabIndex = 9;
			this.txtCycle.Text = "";
			this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCycle_KeyDown);
			this.txtCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtCycle_Validating);
			// 
			// btnProductionLine
			// 
			this.btnProductionLine.Location = new System.Drawing.Point(220, 97);
			this.btnProductionLine.Name = "btnProductionLine";
			this.btnProductionLine.Size = new System.Drawing.Size(22, 20);
			this.btnProductionLine.TabIndex = 13;
			this.btnProductionLine.Text = "...";
			this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
			// 
			// lblProductionLine
			// 
			this.lblProductionLine.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblProductionLine.Location = new System.Drawing.Point(6, 97);
			this.lblProductionLine.Name = "lblProductionLine";
			this.lblProductionLine.Size = new System.Drawing.Size(94, 20);
			this.lblProductionLine.TabIndex = 11;
			this.lblProductionLine.Text = "Production Line";
			this.lblProductionLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtProductionLine
			// 
			this.txtProductionLine.Location = new System.Drawing.Point(98, 97);
			this.txtProductionLine.Name = "txtProductionLine";
			this.txtProductionLine.Size = new System.Drawing.Size(120, 20);
			this.txtProductionLine.TabIndex = 12;
			this.txtProductionLine.Text = "";
			this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
			// 
			// btnCycle
			// 
			this.btnCycle.Location = new System.Drawing.Point(220, 75);
			this.btnCycle.Name = "btnCycle";
			this.btnCycle.Size = new System.Drawing.Size(22, 20);
			this.btnCycle.TabIndex = 10;
			this.btnCycle.Text = "...";
			this.btnCycle.Click += new System.EventHandler(this.btnCycle_Click);
			// 
			// cboPlanType
			// 
			this.cboPlanType.AccessibleDescription = "";
			this.cboPlanType.AccessibleName = "";
			this.cboPlanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPlanType.ItemHeight = 13;
			this.cboPlanType.Items.AddRange(new object[] {
															 "MPS",
															 "MRP"});
			this.cboPlanType.Location = new System.Drawing.Point(98, 6);
			this.cboPlanType.Name = "cboPlanType";
			this.cboPlanType.Size = new System.Drawing.Size(76, 21);
			this.cboPlanType.TabIndex = 3;
			this.cboPlanType.SelectedIndexChanged += new System.EventHandler(this.cboPlanType_SelectedIndexChanged);
			// 
			// lblPlanType
			// 
			this.lblPlanType.AccessibleDescription = "";
			this.lblPlanType.AccessibleName = "";
			this.lblPlanType.ForeColor = System.Drawing.Color.Maroon;
			this.lblPlanType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPlanType.Location = new System.Drawing.Point(6, 6);
			this.lblPlanType.Name = "lblPlanType";
			this.lblPlanType.Size = new System.Drawing.Size(86, 21);
			this.lblPlanType.TabIndex = 2;
			this.lblPlanType.Text = "MPS/MRP";
			this.lblPlanType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMPSReportTitle
			// 
			this.lblMPSReportTitle.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.lblMPSReportTitle.Location = new System.Drawing.Point(232, 0);
			this.lblMPSReportTitle.Name = "lblMPSReportTitle";
			this.lblMPSReportTitle.Size = new System.Drawing.Size(98, 44);
			this.lblMPSReportTitle.TabIndex = 35;
			this.lblMPSReportTitle.Text = "MASTER PRODUCTION SCHEDULE IN";
			this.lblMPSReportTitle.Visible = false;
			// 
			// lblMRPReportTitle
			// 
			this.lblMRPReportTitle.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.lblMRPReportTitle.Location = new System.Drawing.Point(226, 50);
			this.lblMRPReportTitle.Name = "lblMRPReportTitle";
			this.lblMRPReportTitle.Size = new System.Drawing.Size(112, 40);
			this.lblMRPReportTitle.TabIndex = 36;
			this.lblMRPReportTitle.Text = "MATERIAL REQUIREMENTS PLAN IN";
			this.lblMRPReportTitle.Visible = false;
			// 
			// btnModel
			// 
			this.btnModel.Location = new System.Drawing.Point(464, 97);
			this.btnModel.Name = "btnModel";
			this.btnModel.Size = new System.Drawing.Size(22, 20);
			this.btnModel.TabIndex = 25;
			this.btnModel.Text = "...";
			this.btnModel.Click += new System.EventHandler(this.btnModel_Click);
			// 
			// btnPartName
			// 
			this.btnPartName.Location = new System.Drawing.Point(464, 141);
			this.btnPartName.Name = "btnPartName";
			this.btnPartName.Size = new System.Drawing.Size(22, 20);
			this.btnPartName.TabIndex = 31;
			this.btnPartName.Text = "...";
			this.btnPartName.Click += new System.EventHandler(this.btnPartName_Click);
			// 
			// lblModel
			// 
			this.lblModel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblModel.Location = new System.Drawing.Point(250, 97);
			this.lblModel.Name = "lblModel";
			this.lblModel.Size = new System.Drawing.Size(94, 20);
			this.lblModel.TabIndex = 23;
			this.lblModel.Text = "Model";
			this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPartName
			// 
			this.lblPartName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPartName.Location = new System.Drawing.Point(250, 141);
			this.lblPartName.Name = "lblPartName";
			this.lblPartName.Size = new System.Drawing.Size(94, 20);
			this.lblPartName.TabIndex = 29;
			this.lblPartName.Text = "Part Name";
			this.lblPartName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtPartName
			// 
			this.txtPartName.Location = new System.Drawing.Point(344, 141);
			this.txtPartName.Name = "txtPartName";
			this.txtPartName.Size = new System.Drawing.Size(120, 20);
			this.txtPartName.TabIndex = 30;
			this.txtPartName.Text = "";
			this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
			this.txtPartName.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartName_Validating);
			// 
			// txtVendor
			// 
			this.txtVendor.Location = new System.Drawing.Point(344, 75);
			this.txtVendor.Name = "txtVendor";
			this.txtVendor.Size = new System.Drawing.Size(120, 20);
			this.txtVendor.TabIndex = 21;
			this.txtVendor.Text = "";
			this.txtVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendor_KeyDown);
			this.txtVendor.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendor_Validating);
			// 
			// lblVendor
			// 
			this.lblVendor.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVendor.Location = new System.Drawing.Point(250, 75);
			this.lblVendor.Name = "lblVendor";
			this.lblVendor.Size = new System.Drawing.Size(94, 20);
			this.lblVendor.TabIndex = 20;
			this.lblVendor.Text = "Vendor";
			this.lblVendor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnVendor
			// 
			this.btnVendor.Location = new System.Drawing.Point(464, 75);
			this.btnVendor.Name = "btnVendor";
			this.btnVendor.Size = new System.Drawing.Size(22, 20);
			this.btnVendor.TabIndex = 22;
			this.btnVendor.Text = "...";
			this.btnVendor.Click += new System.EventHandler(this.btnVendor_Click);
			// 
			// txtModel
			// 
			this.txtModel.Location = new System.Drawing.Point(344, 97);
			this.txtModel.Name = "txtModel";
			this.txtModel.Size = new System.Drawing.Size(120, 20);
			this.txtModel.TabIndex = 24;
			this.txtModel.Text = "";
			this.txtModel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModel_KeyDown);
			this.txtModel.Validating += new System.ComponentModel.CancelEventHandler(this.txtModel_Validating);
			// 
			// lblPartNo
			// 
			this.lblPartNo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPartNo.Location = new System.Drawing.Point(250, 119);
			this.lblPartNo.Name = "lblPartNo";
			this.lblPartNo.Size = new System.Drawing.Size(94, 20);
			this.lblPartNo.TabIndex = 26;
			this.lblPartNo.Text = "Part No.";
			this.lblPartNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtPartNo
			// 
			this.txtPartNo.Location = new System.Drawing.Point(344, 119);
			this.txtPartNo.Name = "txtPartNo";
			this.txtPartNo.Size = new System.Drawing.Size(120, 20);
			this.txtPartNo.TabIndex = 27;
			this.txtPartNo.Text = "";
			this.txtPartNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNo_KeyDown);
			this.txtPartNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartNo_Validating);
			// 
			// btnPartNo
			// 
			this.btnPartNo.Location = new System.Drawing.Point(464, 119);
			this.btnPartNo.Name = "btnPartNo";
			this.btnPartNo.Size = new System.Drawing.Size(22, 20);
			this.btnPartNo.TabIndex = 28;
			this.btnPartNo.Text = "...";
			this.btnPartNo.Click += new System.EventHandler(this.btnPartNo_Click);
			// 
			// CPOReport
			// 
			this.AcceptButton = this.btnPrint;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(490, 201);
			this.Controls.Add(this.cboPlanType);
			this.Controls.Add(this.txtProductionLine);
			this.Controls.Add(this.txtCycle);
			this.Controls.Add(this.txtCategory);
			this.Controls.Add(this.txtMasLoc);
			this.Controls.Add(this.cboYear);
			this.Controls.Add(this.cboMonth);
			this.Controls.Add(this.lblPlanType);
			this.Controls.Add(this.btnCycle);
			this.Controls.Add(this.btnProductionLine);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.lblCycle);
			this.Controls.Add(this.lblYear);
			this.Controls.Add(this.lblMonth);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnCategory);
			this.Controls.Add(this.btnMasLoc);
			this.Controls.Add(this.lblCategory);
			this.Controls.Add(this.lblMasLoc);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblMPSReportTitle);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnModel);
			this.Controls.Add(this.btnPartName);
			this.Controls.Add(this.lblModel);
			this.Controls.Add(this.lblPartName);
			this.Controls.Add(this.txtPartName);
			this.Controls.Add(this.txtVendor);
			this.Controls.Add(this.lblVendor);
			this.Controls.Add(this.btnVendor);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.lblPartNo);
			this.Controls.Add(this.txtPartNo);
			this.Controls.Add(this.btnPartNo);
			this.Controls.Add(this.lblMRPReportTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "CPOReport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Computer Planned Order Report";
			this.Load += new System.EventHandler(this.CPOReport_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

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

	
		#region EVENT HANDLERS		

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void txtMasLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMasLoc_Click()";
			try
			{
				if (!txtMasLoc.Modified) return;
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					txtMasLoc.Tag = null;
					//txtLocation.Text = string.Empty;
					//txtLocation.Tag = null;
					return;
				}
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();
				//User has selected CCN
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);	
				}
				else //User has not selected CCN
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					if ((txtMasLoc.Tag != null) && (int.Parse(txtMasLoc.Tag.ToString())) != int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
					{
						//txtLocation.Text = string.Empty;
						//txtLocation.Tag = null;
					}
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					txtMasLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
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
		/// txtMasLoc_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void txtMasLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnMasLoc_Click(sender, e);
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
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void txtCategory_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_Validating()";
			try
			{
				if (!txtCategory.Modified) return;
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
		/// <author>Trada</author>
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
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void btnMasLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMasLoc_Click()";			
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
					drowData = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text.Trim(), null, false);// htData, false);
				else
					drowData = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text.Trim(), null, true);

				if (drowData != null)
				{
					txtMasLoc.Text = drowData[MST_MasterLocationTable.CODE_FLD].ToString().Trim();
					txtMasLoc.Tag = int.Parse(drowData[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString().Trim());
				}
				else
				{
					txtMasLoc.Focus();
					txtMasLoc.SelectAll();
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
		/// <author>Trada</author>
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
				PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
				int nCCNID;
				int nMonth;
				int nYear;
				int nCycle;
				int nProductionLineID = int.MinValue;
				int nMasterLocationID = int.MinValue;
				int nCategoryID = int.MinValue;
				int nVendorID = int.MinValue;
				int nProductID = int.MinValue;
				
				string strCCN = string.Empty;
				string strCycle = string.Empty;
				string strProductionLine = string.Empty;
				string strMasterLocation = string.Empty;
				string strCategory = string.Empty;
				string strVendor = string.Empty;
				string strProduct = string.Empty;
				string strModel = string.Empty;

				/// contain array of string: 01 02 03 .. of day of month in the dtbResult, except the missing day			
				ArrayList arrDueDateHeading = new ArrayList();	

				/// Build to keep value of pair: 01 --> 01-Mon, ... depend on the real data of dtbResule
				NameValueCollection arrDayNumberMapToDayWithDayOfWeek = new NameValueCollection();

				/// Build to keep value of pair: 01 --> 01-Mon, ... NOT DEPEND on the real data
				NameValueCollection arrFullDayNumberMapToDayWithDayOfWeek = new NameValueCollection();




				#region Constants
				/// Result Data Table Column name
				const string DUE_DATE = "DueDate";
				const string QUANTITY = "Quantity";
				const string CATEGORY = "Category";
				const string PARTNO = "Part No.";
				const string PARTNAME = "Part Name";
				const string MODEL = "Model";
				const string UM = "UM";			
		
				const string REPORT_LAYOUT_FILE = "CPOReport.xml";
				const string REPORT_NAME = "CPO Report";

				const string REPORTFLD_TITLE			= "fldTitle";
				const string REPORTFLD_COMPANY			= "fldCompany";
				const string REPORTFLD_ADDRESS			= "fldAddress";
				const string REPORTFLD_TEL				= "fldTel";
				const string REPORTFLD_FAX				= "fldFax";

				const string REPORTFLD_PARAMETER_MONTH				= "fldParameterMonth";
				const string REPORTFLD_PARAMETER_YEAR				= "fldParameterYear";			
				const string REPORTFLD_PARAMETER_MASTER_LOCATION	= "fldParameterMasterLocation";
				const string REPORTFLD_PARAMETER_CATEGORY			= "fldParameterCategory";
				const string REPORTFLD_PARAMETER_CCN				= "fldParameterCCN";
				const string REPORTFLD_PARAMETER_CYCLE				= "fldParameterCycle";
				const string REPORTFLD_PARAMETER_PRODUCTIONLINE		= "fldParameterProductionLine";
				const string REPORTFLD_PARAMETER_VENDOR				= "fldParameterVendor";
				const string REPORTFLD_PARAMETER_ITEM				= "fldParameterItem";
				const string REPORTFLD_PARAMETER_MODEL				= "fldParameterModel";

				#endregion				

			
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
				//Check if user does not select plan type
				if(cboPlanType.SelectedItem == null)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_PLAN_TYPE);
					cboPlanType.Focus();
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

				// user must select Cycle
				if (FormControlComponents.CheckMandatory(txtCycle))
				{
					string[] arrParams = {lblCycle.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error,arrParams);
					txtCycle.Focus();
					txtCycle.SelectAll();
					Cursor = Cursors.Default;
					return;
				}

				#endregion			
			
				nCCNID = int.Parse(cboCCN.SelectedValue.ToString());
				strCCN = boUtil.GetCCNCodeFromID(nCCNID);
				nYear = int.Parse(cboYear.SelectedItem.ToString());			
				nMonth = int.Parse(cboMonth.SelectedItem.ToString());
				nCycle = (int)(txtCycle.Tag);
				if(cboPlanType.SelectedItem.ToString().Trim() == PlanTypeEnum.MPS.ToString())
					strCycle = objBO.GetMPSCycleFromID(nCycle) + ReportBuilder.SEPERATOR_PARAMETER_DISPLAY_INFO + objBO.GetMPSCycleDescriptionFromID(nCycle);
				else
					strCycle = objBO.GetMRPCycleFromID(nCycle) + ReportBuilder.SEPERATOR_PARAMETER_DISPLAY_INFO + objBO.GetMRPCycleDescriptionFromID(nCycle);

				// if input null, then we send the int.MinValue to the BO function
				// Not mandatory id field will have int.MinValue if it is not selected
				try
				{
					nProductionLineID = (int)(txtProductionLine.Tag);
					strProductionLine = objBO.GetProductLineCodeFromID(nProductionLineID) + ReportBuilder.SEPERATOR_PARAMETER_DISPLAY_INFO + objBO.GetProductLineNameFromID(nProductionLineID);
				}
				catch
				{
					strProductionLine = string.Empty;
				}
				try
				{
					nMasterLocationID = (int)(txtMasLoc.Tag);
					strMasterLocation = objBO.GetMasterLocationCodeFromID(nMasterLocationID) + ReportBuilder.SEPERATOR_PARAMETER_DISPLAY_INFO + objBO.GetMasterLocationNameFromID(nMasterLocationID);
				}
				catch
				{
					strMasterLocation = string.Empty;
				}
				try
				{
					nCategoryID = (int)(txtCategory.Tag);
					strCategory = objBO.GetCategoryCodeFromID(nCategoryID) + ReportBuilder.SEPERATOR_PARAMETER_DISPLAY_INFO + objBO.GetCategoryNameFromID(nCategoryID);
				}
				catch
				{
					strCategory = string.Empty;
				}			
				try
				{
					nVendorID = (int)(txtVendor.Tag);
					strVendor = objBO.GetVendorInfo(nVendorID);
				}
				catch
				{
					strVendor = string.Empty;
				}
				try
				{
					nProductID = (int)(txtPartNo.Tag);
					strProduct = objBO.GetItemInfor(nProductID);
				}
				catch
				{
					strProduct = string.Empty;
				}
			
				#endregion
			
				#region BUILDING THE TABLE (getting from database by BO)
				dtbResult = objBO.GetCPOReportData(cboPlanType.SelectedItem.ToString().Trim(), nCCNID,  nMonth,  nYear,  
					nCycle, nProductionLineID,  nMasterLocationID,  nCategoryID,
					nVendorID, nProductID, strModel);
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
				ArrayList arrDueDate = GetColumnValuesFromTable(dtbResult,DUE_DATE);
				ArrayList arrItems = GetCategoryPartNoPairsFromTable(dtbResult,CATEGORY,PARTNO);

				foreach(DateTime dtm in arrDueDate)
				{
					string strColumnName = "D" + dtm.Day.ToString("00");
					arrDayNumberMapToDayWithDayOfWeek.Add(dtm.Day.ToString("00"), dtm.Day.ToString("00")+"-"+dtm.DayOfWeek.ToString().Substring(0,3)  );
					arrDueDateHeading.Add(strColumnName);
				}
			

				#endregion		


			
				DataTable dtbTransform = BuildCPOTable(arrDueDateHeading);
				foreach(string strItem in arrItems)
				{
					// Create DUMMYROW FIRST
					DataRow dtrNew = dtbTransform.NewRow();

				
					/// if strItem.Split('#')[0] ==  string.empty, its mean Category value is null
					/// so we put IsNull in the filter string (to select from dtbResult);
					string strFilter = string.Empty;
					if(strItem.Split('#')[0] == string.Empty)
					{
						strFilter = 
							string.Format("[{0}] is null AND [{1}]='{2}'",
							CATEGORY,							
							PARTNO,
							strItem.Split('#')[1]);
					}
					else
					{
						strFilter = 
							string.Format("[{0}]='{1}' AND [{2}]='{3}'",
							CATEGORY,
							strItem.Split('#')[0],
							PARTNO,
							strItem.Split('#')[1]);
					}

					string strSort = string.Format("[{0}] ASC,[{1}] ASC",CATEGORY,PARTNO);
					DataRow[] dtrows = dtbResult.Select(strFilter,strSort);
					foreach(DataRow dtr in dtrows)
					{
						// fill data to the dummy row
						dtrNew[CATEGORY] = dtrows[0][CATEGORY];
						dtrNew[PARTNO] = dtrows[0][PARTNO];
						dtrNew[PARTNAME] = dtrows[0][PARTNAME];
						dtrNew[MODEL] = dtrows[0][MODEL];
						dtrNew[UM] = dtrows[0][UM];

						string strDateColumnToFill = "D" + ((DateTime)dtr[DUE_DATE]).Day.ToString("00");
						dtrNew[strDateColumnToFill] = dtr[QUANTITY];
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
				
				if(cboPlanType.SelectedItem.ToString().Trim() == PlanTypeEnum.MPS.ToString())
				{
					printPreview.FormTitle = lblMPSReportTitle.Text + " "  + nMonth.ToString("00") +"-"+ nYear.ToString("0000") ;
				}
				else
				{
					printPreview.FormTitle = lblMRPReportTitle.Text + " "  + nMonth.ToString("00") + "-"+ nYear.ToString("0000") ;
				}

				objRB.ReportViewer = printPreview.ReportViewer;				
				objRB.RenderReport();			

						

				#region MODIFY THE REPORT LAYOUT
				
				if(cboPlanType.SelectedItem.ToString().Trim() == PlanTypeEnum.MPS.ToString())
				{
					objRB.DrawPredefinedField(REPORTFLD_TITLE, lblMPSReportTitle.Text.Trim());
				}
				else
				{
					objRB.DrawPredefinedField(REPORTFLD_TITLE, lblMRPReportTitle.Text.Trim());
				}
//				objRB.GetFieldByName(REPORTFLD_TITLE + "Footer").Text =  
//					objRB.GetFieldByName(REPORTFLD_TITLE).Text + " " +
//					objRB.GetFieldByName(REPORTFLD_PARAMETER_MONTH).Text + "-" +
//					objRB.GetFieldByName(REPORTFLD_PARAMETER_YEAR).Text;				

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

				#region PUSH PARAMETER VALUE				

				objRB.DrawPredefinedField(REPORTFLD_PARAMETER_MONTH, nMonth.ToString("00"));
				objRB.DrawPredefinedField(REPORTFLD_PARAMETER_YEAR, nYear.ToString("0000"));

				objRB.DrawPredefinedField(REPORTFLD_PARAMETER_CCN,strCCN);
				objRB.DrawPredefinedField(REPORTFLD_PARAMETER_CYCLE,strCycle);
				objRB.DrawPredefinedField(REPORTFLD_PARAMETER_PRODUCTIONLINE, strProductionLine);
				objRB.DrawPredefinedField(REPORTFLD_PARAMETER_MASTER_LOCATION, strMasterLocation);
				objRB.DrawPredefinedField(REPORTFLD_PARAMETER_CATEGORY, strCategory);
				objRB.DrawPredefinedField(REPORTFLD_PARAMETER_VENDOR, strVendor);
				objRB.DrawPredefinedField(REPORTFLD_PARAMETER_ITEM, strProduct);
				objRB.DrawPredefinedField(REPORTFLD_PARAMETER_MODEL, strModel);

				/// Invisible the Parameter if it is not set
				const string REPORTLBL_PRODUCTIONLINE = "lblProductionLine";
				const string REPORTLBL_MASTER_LOCATION = "lblMasterLocation";
				const string REPORTLBL_CATEGORY = "lblCategory";
				const string REPORTLBL_VENDOR = "lblVendor";
				const string REPORTLBL_ITEM = "lblItem";
				const string REPORTLBL_MODEL = "lblModel";
				const string REPORTLBL_COLON_SUFFIX = "Colon";

				double nParamHeight = objRB.GetFieldByName(REPORTLBL_PRODUCTIONLINE).Height;
				if(nProductionLineID == int.MinValue)
				{
					objRB.GetFieldByName(REPORTLBL_PRODUCTIONLINE).Visible = 
						objRB.GetFieldByName(REPORTLBL_PRODUCTIONLINE+REPORTLBL_COLON_SUFFIX ).Visible = false;
					objRB.GetFieldByName(REPORTLBL_MASTER_LOCATION).Top = 
						objRB.GetFieldByName(REPORTLBL_MASTER_LOCATION+REPORTLBL_COLON_SUFFIX).Top = 
						objRB.GetFieldByName(REPORTFLD_PARAMETER_MASTER_LOCATION).Top -= nParamHeight;
					objRB.GetFieldByName(REPORTLBL_CATEGORY).Top =
						objRB.GetFieldByName(REPORTLBL_CATEGORY+REPORTLBL_COLON_SUFFIX).Top = 
						objRB.GetFieldByName(REPORTFLD_PARAMETER_CATEGORY).Top -= nParamHeight;						
					objRB.GetSectionByName("Header").Height -= nParamHeight;
				}
				if(nMasterLocationID == int.MinValue)
				{
					objRB.GetFieldByName(REPORTLBL_MASTER_LOCATION).Visible = 
						objRB.GetFieldByName(REPORTLBL_MASTER_LOCATION+REPORTLBL_COLON_SUFFIX ).Visible = false;
					objRB.GetFieldByName(REPORTLBL_CATEGORY).Top =
						objRB.GetFieldByName(REPORTLBL_CATEGORY+REPORTLBL_COLON_SUFFIX).Top = 
						objRB.GetFieldByName(REPORTFLD_PARAMETER_CATEGORY).Top -= nParamHeight;						
					objRB.GetSectionByName("Header").Height -= nParamHeight;
				}
				if(nCategoryID == int.MinValue)
				{
					objRB.GetFieldByName(REPORTLBL_CATEGORY).Visible = 
						objRB.GetFieldByName(REPORTLBL_CATEGORY+REPORTLBL_COLON_SUFFIX ).Visible = false;
					objRB.GetSectionByName("Header").Height -= nParamHeight;
				}

				#region other param
				if(nVendorID == int.MinValue)
				{
					objRB.GetFieldByName(REPORTLBL_VENDOR).Visible = 
						objRB.GetFieldByName(REPORTLBL_VENDOR+REPORTLBL_COLON_SUFFIX ).Visible = false;
					objRB.GetFieldByName(REPORTLBL_ITEM).Top = 
						objRB.GetFieldByName(REPORTLBL_ITEM+REPORTLBL_COLON_SUFFIX).Top = 
						objRB.GetFieldByName(REPORTFLD_PARAMETER_ITEM).Top -= nParamHeight;
					objRB.GetFieldByName(REPORTLBL_MODEL).Top =
						objRB.GetFieldByName(REPORTLBL_MODEL+REPORTLBL_COLON_SUFFIX).Top = 
						objRB.GetFieldByName(REPORTFLD_PARAMETER_MODEL).Top -= nParamHeight;
				}
				if(nProductID == int.MinValue)
				{
					objRB.GetFieldByName(REPORTLBL_ITEM).Visible = 
						objRB.GetFieldByName(REPORTLBL_ITEM+REPORTLBL_COLON_SUFFIX ).Visible = false;
					objRB.GetFieldByName(REPORTLBL_MODEL).Top =
						objRB.GetFieldByName(REPORTLBL_MODEL+REPORTLBL_COLON_SUFFIX).Top = 
						objRB.GetFieldByName(REPORTFLD_PARAMETER_MODEL).Top -= nParamHeight;
				}
				if(strModel == string.Empty)
				{
					objRB.GetFieldByName(REPORTLBL_MODEL).Visible = 
						objRB.GetFieldByName(REPORTLBL_MODEL+REPORTLBL_COLON_SUFFIX ).Visible = false;
				}
				#endregion

				#endregion		
			
				#region RENAME THE COLUMN HEADING TEXT				
				for(int i = 0; i <= 31; i++) /// clear the heading text
				{
					objRB.DrawPredefinedField(PREFIX_DAYINMONTH+i.ToString(ReportBuilder.FORMAT_DAY_2CHAR),"");
				}                
				objRB.DrawPredefinedList_DaysOfWeek(nYear, nMonth,
					PREFIX_DAYINMONTH,
					PREFIX_DAYOFWEEK,
					1 ,DateTime.DaysInMonth(nYear , nMonth ) );

				#endregion


				#region HIDE the column of not-existed day in current month
				// 1. IN1T :: what to clear
				string[] arrFieldToClear = {											   
											   PREFIX_DAYINMONTH,	/*// also hide the Day Heading */
											   PREFIX_DAYOFWEEK,
											   "_" + FLD ,
											   "_" + FLD + "SumD" 
										   };		// contain name of field need to clear if day column is not exist
            
				objRB.HideColumnNotExistInMonth(nYear,nMonth, arrFieldToClear);

				#endregion HIDE the column of not-existed day in current month

				StringCollection arrFieldNames = new StringCollection();
				arrFieldNames.AddRange(arrFieldToClear);			

				string LEFT_ANCHOR_FLD = PREFIX_DAYINMONTH + "01";
				// string LEFT_MARGIN_FLD = "lblLeftMarginToSpread";
				string RIGHT_MARGIN_FLD = "lblSumRow";

				double dblWidthToSpead = objRB.ActualPageWidth - 
					(objRB.GetFieldByName(PREFIX_DAYINMONTH + "01").Left + 	objRB.GetFieldByName(RIGHT_MARGIN_FLD).Width	) ;
				objRB.SpreadColumnsWithinWidth(arrFieldNames, 1,  DateTime.DaysInMonth(nYear,nMonth),
					objRB.GetFieldByName(LEFT_ANCHOR_FLD).Left, dblWidthToSpead  );

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
		/// Display search form to select MPS Cycle
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCycle_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycle_Click()";
			try
			{
				// user must select CCN first
				if (cboCCN.SelectedValue == null || cboCCN.SelectedValue == DBNull.Value)
				{					
					string[] arrParams = {lblCCN.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error,arrParams);
					cboCCN.Focus();
					Cursor = Cursors.Default;
					return;
				}
				// user must select PlanType first
				if (cboPlanType.SelectedItem == null)
				{					
					string[] arrParams = {lblPlanType.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error,arrParams);
					cboPlanType.Focus();
					Cursor = Cursors.Default;
					return;
				}
				DataRowView drowData = null;
				Hashtable htData = new Hashtable();

				if(cboPlanType.SelectedItem.ToString().Trim() == PlanTypeEnum.MPS.ToString())
				{
					htData.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, int.Parse(cboCCN.SelectedValue.ToString()));


					if (sender is TextBox && sender != null)
						drowData = FormControlComponents.OpenSearchForm(MTR_MPSCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htData, false);
					else
						drowData = FormControlComponents.OpenSearchForm(MTR_MPSCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htData, true);

					if (drowData != null)
					{
						txtCycle.Text = drowData[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].ToString().Trim();
						txtCycle.Tag = int.Parse(drowData[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].ToString().Trim());
					}
					else
					{
						txtCycle.Focus();
						txtCycle.SelectAll();
					}					
				}
				else
				{
					htData.Add(MTR_MRPCycleOptionMasterTable.CCNID_FLD, int.Parse(cboCCN.SelectedValue.ToString()));
					if (sender is TextBox && sender != null)
						drowData = FormControlComponents.OpenSearchForm(MTR_MRPCycleOptionMasterTable.TABLE_NAME, MTR_MRPCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htData, false);
					else
						drowData = FormControlComponents.OpenSearchForm(MTR_MRPCycleOptionMasterTable.TABLE_NAME, MTR_MRPCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htData, true);

					if (drowData != null)
					{
						txtCycle.Text = drowData[MTR_MRPCycleOptionMasterTable.CYCLE_FLD].ToString().Trim();
						txtCycle.Tag = int.Parse(drowData[MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD].ToString().Trim());
					}
					else
					{
						txtCycle.Focus();
						txtCycle.SelectAll();
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
		/// Display search form to select Production Line
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnProductionLine_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
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
					drowData = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, false);// htData, false);
				else
					drowData = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, true);

				if (drowData != null)
				{
					txtProductionLine.Text = drowData[PRO_ProductionLineTable.CODE_FLD].ToString().Trim();
					txtProductionLine.Tag = int.Parse(drowData[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString().Trim());
				}
				else
				{
					txtProductionLine.Focus();
					txtProductionLine.SelectAll();
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

		private void txtCycle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_Validating()";
			try
			{
				if (!txtCycle.Modified) return;
				if (txtCycle.Text.Trim() == string.Empty)
				{
					txtCycle.Tag = null;
					return;
				}
				DataRowView drwResult = null;

				if(cboPlanType.SelectedItem != null)	// user selected the PlanType
				{
					if(cboPlanType.SelectedItem.ToString().Trim() == PlanTypeEnum.MPS.ToString())
					{
						drwResult = FormControlComponents.OpenSearchForm(MTR_MPSCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), null, false);
						if (drwResult != null)
						{
							txtCycle.Text = drwResult[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].ToString();
							txtCycle.Tag = drwResult[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD];
						}
						else
						{
							e.Cancel = true;
						}
					}
					else	// MRP plan type
					{
						drwResult = FormControlComponents.OpenSearchForm(MTR_MRPCycleOptionMasterTable.TABLE_NAME, MTR_MRPCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), null, false);
						if (drwResult != null)
						{
							txtCycle.Text = drwResult[MTR_MRPCycleOptionMasterTable.CYCLE_FLD].ToString();
							txtCycle.Tag = drwResult[MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD];
						}
						else
						{
							e.Cancel = true;
						}
					}
				}
				else	// user did not select the PlanType
				{
					/// HACKED: Thachnn : 09/12/2005
					PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_PLAN_TYPE);
					cboPlanType.Focus();
					Cursor = Cursors.Default;
					return;
					/// HACKED: Thachnn : 09/12/2005
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

		private void txtProductionLine_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";
			try
			{
				if (!txtProductionLine.Modified) return;
				if (txtProductionLine.Text.Trim() == string.Empty)
				{
					txtProductionLine.Tag = null;
					//txtLocation.Text = string.Empty;
					//txtLocation.Tag = null;
					return;
				}
				DataRowView drwResult = null;
				//Hashtable htbCriteria = new Hashtable();
				
				//User has selected CCN
				//				if (cboCCN.SelectedIndex != -1)
				//				{
				//					htbCriteria.Add(PRO_ProductionLineTable.CCNID_FLD, cboCCN.SelectedValue);	
				//				}
				//				else //User has not selected CCN
				//				{
				//					htbCriteria.Add(PRO_ProductionLineTable.CCNID_FLD, 0);
				//				}
				drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), /*htbCriteria*/ null, false);
				if (drwResult != null)
				{
					if ((txtProductionLine.Tag != null) && (int.Parse(txtProductionLine.Tag.ToString())) != int.Parse(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString()))
					{
						//txtLocation.Text = string.Empty;
						//txtLocation.Tag = null;
					}
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
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
		/// Open search form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtCycle_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnCycle_Click(sender, e);
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
		/// open search forms
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtProductionLine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnProductionLine_Click(sender, e);
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



		#endregion

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

				txtCycle.Text = string.Empty;
				txtCycle.Tag = ZERO_STRING;

				txtProductionLine.Text = string.Empty;
				txtProductionLine.Tag = ZERO_STRING;					

				txtMasLoc.Text = string.Empty;
				txtMasLoc.Tag = ZERO_STRING;

				txtCategory.Text = string.Empty;
				txtCategory.Tag = ZERO_STRING;

				cboPlanType.Focus();				
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		
		#region REPORT RELATE FUNCTIONS
		
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
		/// AddDueDateColumnsList to the exist datatable
		/// </summary>
		/// <remarks>		
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildCPOTable(ArrayList parrDueDateHeading)
		{
			const string strCPOTableName = "CPOTable";
			try
			{
				//Create table
				DataTable dtbRet = new DataTable(strCPOTableName);
				
				//Add columns
				
				dtbRet.Columns.Add("Category", typeof(System.String));
				dtbRet.Columns.Add("Part No.", typeof(System.String));											
				dtbRet.Columns.Add("Part Name", typeof(System.String));
				dtbRet.Columns.Add("Model", typeof(System.String));
				dtbRet.Columns.Add("UM", typeof(System.String));		
				foreach(string strColumnName in parrDueDateHeading)
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
					if(parrDueDateHeading.Contains("D" + i.ToString("00")) == false )
					{		
						try
						{
							dtbRet.Columns.Add("D" + i.ToString("00"),typeof(System.String));
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
		/// Browse the DataTable, get all value of Category, PartNo column, insert into ArraysList as CategoryValue#PartNoValue
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>			
		/// <param name="pstrCategoryColName"></param>
		/// <param name="pstrPartNoColName"></param>
		/// <returns>ArrayList of object, collect CategoryValue#PartNoValue pairs from pdtb. Empty ArrayList if error or not found any row in pdtb.</returns>		
		private static ArrayList GetCategoryPartNoPairsFromTable(DataTable pdtb, string pstrCategoryColName, string pstrPartNoColName)
		{
			ArrayList arrRet = new ArrayList();
			try
			{
				foreach (DataRow drow in pdtb.Rows)
				{
					object objCategoryGet = drow[pstrCategoryColName];
					object objPartNoGet = drow[pstrPartNoColName];
					string str = objCategoryGet.ToString().Trim() + "#" + objPartNoGet.ToString().Trim();
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

		

		#endregion

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
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyTable.TABLE_NAME, MST_PartyTable.CODE_FLD, txtVendor.Text.Trim(), null, false);
				if (drwResult != null)
				{
					txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendor.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
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

		private void txtVendor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnVendor_Click(sender, null);
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

		private void btnVendor_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendor_Click()";
			try
			{
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyTable.TABLE_NAME, MST_PartyTable.CODE_FLD, txtVendor.Text.Trim(), null, false);
				if (drwResult != null)
				{
					txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendor.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
				}
				else
					txtVendor.Focus();
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

		private void txtPartNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNo_Validating()";
			try
			{
				if (!txtPartNo.Modified) return;
				if (txtPartNo.Text.Trim() == string.Empty)
				{
					txtPartNo.Tag = null;
					txtPartName.Text = string.Empty;
					return;
				}
				Hashtable htCondition = new Hashtable();
				if (txtCategory.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.CATEGORYID_FLD, txtCategory.Tag);
				if (txtVendor.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.PRIMARYVENDORID_FLD, txtVendor.Tag);
				if (txtModel.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.REVISION_FLD, txtModel.Text);
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNo.Text.Trim(), htCondition, false);
				if (drwResult != null)
				{
					txtPartNo.Text = drwResult[ITM_ProductTable.CODE_FLD].ToString();
					txtPartNo.Tag = drwResult[ITM_ProductTable.PRODUCTID_FLD];
					txtPartName.Text = drwResult[ITM_ProductTable.DESCRIPTION_FLD].ToString();
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

		private void txtPartNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNo_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnPartNo_Click(sender, null);
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

		private void btnPartNo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNo_Click()";
			try
			{
				Hashtable htCondition = new Hashtable();
				if (txtCategory.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.CATEGORYID_FLD, txtCategory.Tag);
				if (txtVendor.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.PRIMARYVENDORID_FLD, txtVendor.Tag);
				if (txtModel.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.REVISION_FLD, txtModel.Text);
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNo.Text.Trim(), htCondition, false);
				if (drwResult != null)
				{
					txtPartNo.Text = drwResult[ITM_ProductTable.CODE_FLD].ToString();
					txtPartNo.Tag = drwResult[ITM_ProductTable.PRODUCTID_FLD];
					txtPartName.Text = drwResult[ITM_ProductTable.DESCRIPTION_FLD].ToString();
				}
				else
					txtPartNo.Focus();
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

		private void txtPartName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_Validating()";
			try
			{
				if (!txtPartName.Modified) return;
				if (txtPartName.Text.Trim() == string.Empty)
				{
					txtPartNo.Text = string.Empty;
					txtPartNo.Tag = null;
					txtPartName.Text = string.Empty;
					return;
				}
				Hashtable htCondition = new Hashtable();
				if (txtCategory.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.CATEGORYID_FLD, txtCategory.Tag);
				if (txtVendor.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.PRIMARYVENDORID_FLD, txtVendor.Tag);
				if (txtModel.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.REVISION_FLD, txtModel.Text);
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtPartName.Text.Trim(), htCondition, false);
				if (drwResult != null)
				{
					txtPartNo.Text = drwResult[ITM_ProductTable.CODE_FLD].ToString();
					txtPartNo.Tag = drwResult[ITM_ProductTable.PRODUCTID_FLD];
					txtPartName.Text = drwResult[ITM_ProductTable.DESCRIPTION_FLD].ToString();
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

		private void txtPartName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnPartName_Click(sender, null);
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

		private void btnPartName_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartName_Click()";
			try
			{
				Hashtable htCondition = new Hashtable();
				if (txtCategory.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.CATEGORYID_FLD, txtCategory.Tag);
				if (txtVendor.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.PRIMARYVENDORID_FLD, txtVendor.Tag);
				if (txtModel.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.REVISION_FLD, txtModel.Text);
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtPartName.Text.Trim(), htCondition, false);
				if (drwResult != null)
				{
					txtPartNo.Text = drwResult[ITM_ProductTable.CODE_FLD].ToString();
					txtPartNo.Tag = drwResult[ITM_ProductTable.PRODUCTID_FLD];
					txtPartName.Text = drwResult[ITM_ProductTable.DESCRIPTION_FLD].ToString();
				}
				else
					txtPartName.Focus();
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

		private void txtModel_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtModel_Validating()";
			const string MODEL_VIEW = "v_ModelList";
			try
			{
				if (!txtPartName.Modified) return;
				if (txtPartName.Text.Trim() == string.Empty)
				{
					txtPartNo.Text = string.Empty;
					txtPartNo.Tag = null;
					txtPartName.Text = string.Empty;
					return;
				}
				Hashtable htCondition = new Hashtable();
				if (txtCategory.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.CATEGORYID_FLD, txtCategory.Tag);
				if (txtVendor.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.PRIMARYVENDORID_FLD, txtVendor.Tag);
				if (txtModel.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.REVISION_FLD, txtModel.Text);
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MODEL_VIEW, ITM_ProductTable.REVISION_FLD, txtModel.Text.Trim(), htCondition, false);
				if (drwResult != null)
					txtModel.Text = drwResult[ITM_ProductTable.REVISION_FLD].ToString();
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

		private void txtModel_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtModel_Validating()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnModel_Click(sender, null);
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

		private void btnModel_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtModel_Validating()";
			const string MODEL_VIEW = "v_ModelList";
			try
			{
				Hashtable htCondition = new Hashtable();
				if (txtCategory.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.CATEGORYID_FLD, txtCategory.Tag);
				if (txtVendor.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.PRIMARYVENDORID_FLD, txtVendor.Tag);
				if (txtModel.Text != string.Empty)
					htCondition.Add(ITM_ProductTable.REVISION_FLD, txtModel.Text);
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(MODEL_VIEW, ITM_ProductTable.REVISION_FLD, txtModel.Text.Trim(), htCondition, false);
				if (drwResult != null)
					txtModel.Text = drwResult[ITM_ProductTable.REVISION_FLD].ToString();
				else
					txtModel.Focus();
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
		
	}
}
