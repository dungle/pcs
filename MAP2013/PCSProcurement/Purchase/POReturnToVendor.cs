using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1TrueDBGrid;
using PCSComProcurement.Purchase.BO;
using PCSComProcurement.Purchase.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;
using AddNewModeEnum = C1.Win.C1TrueDBGrid.AddNewModeEnum;
using BeforeColUpdateEventArgs = C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs;
using C1DisplayColumn = C1.Win.C1TrueDBGrid.C1DisplayColumn;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using ColEventArgs = C1.Win.C1TrueDBGrid.ColEventArgs;

namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for POReturnToVendor.
	/// </summary>
	public class POReturnToVendor : Form
	{
		private Label lblPostDate;
		private C1DateEdit dtmPostDate;
		private C1Combo cboCCN;
		private Label lblCCN;
		private Button btnClose;
		private Button btnHelp;
		private Button btnDelete;
		private Button btnSave;
		private Button btnAdd;
		private Button btnPrint;
		private Label lblReturnNo;
		private Label lblMasLoc;
		private Label lblRefNo;
		private Label lblProductionLine;
		private Label lblReturnBy;
		private ComboBox cboReturnBy;
		private TextBox txtReturnNo;
		private TextBox txtMasLoc;
		private TextBox txtRefNo;
		private TextBox txtProductionLine;
		private Button btnReturnNo;
		private Button btnMasLoc;
		private Button btnRefNo;
		private Button btnProductionLine;
		private Label lblVendor;
		private Label lblVendorLocation;
		private TextBox txtVendorCode;
		private TextBox txtVendorName;
		private TextBox txtVendorLoc;
		private Button btnVendorCode;
		private Button btnVendorLoc;
		private C1TrueDBGrid dgrdData;
		private Label lblInvoice;
		private Label lblPurchaseOrder;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		const string THIS = "PCSProcurement.Purchase.POReturnToVendor";
		const string V_PORETURNTOVENDOR ="V_POReturnToVendor";
		const string V_INVOICETORETURN ="V_InvoiceToReturn";
		const string PURCHASELOCCODE_FLD = "PurchaseLocCode";
		const string PARTYCODE_FLD = "PartyCode";
		const string PARTYNAME_FLD = "PartyName";
		const string MASTERLOCCODE_FLD ="MasterLocCode";
		const string PURCHASEORDERCODE_FLD ="PurchaseOrderCode";
		const string PRODUCTIONLINECODE_FLD = "ProductionLine";
		const string TOTAL_REMAIN_FLD ="TotalRemain";

		private DataTable dtbGridLayOut;
		private DataSet dstData;
		private EnumAction enumAction = EnumAction.Default;
		private POReturnToVendorBO boReturn = new POReturnToVendorBO();
		private PO_ReturnToVendorMasterVO voReturnMaster;
		DataTable dtbReceived = new DataTable();
		private DateTime dtmOrderDate;

		public POReturnToVendor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(POReturnToVendor));
			this.lblPostDate = new System.Windows.Forms.Label();
			this.dtmPostDate = new C1.Win.C1Input.C1DateEdit();
			this.lblReturnNo = new System.Windows.Forms.Label();
			this.lblMasLoc = new System.Windows.Forms.Label();
			this.lblRefNo = new System.Windows.Forms.Label();
			this.lblVendor = new System.Windows.Forms.Label();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.lblVendorLocation = new System.Windows.Forms.Label();
			this.lblReturnBy = new System.Windows.Forms.Label();
			this.cboReturnBy = new System.Windows.Forms.ComboBox();
			this.txtReturnNo = new System.Windows.Forms.TextBox();
			this.txtMasLoc = new System.Windows.Forms.TextBox();
			this.txtRefNo = new System.Windows.Forms.TextBox();
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.txtVendorCode = new System.Windows.Forms.TextBox();
			this.txtVendorName = new System.Windows.Forms.TextBox();
			this.txtVendorLoc = new System.Windows.Forms.TextBox();
			this.btnReturnNo = new System.Windows.Forms.Button();
			this.btnMasLoc = new System.Windows.Forms.Button();
			this.btnRefNo = new System.Windows.Forms.Button();
			this.btnProductionLine = new System.Windows.Forms.Button();
			this.btnVendorCode = new System.Windows.Forms.Button();
			this.btnVendorLoc = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.lblInvoice = new System.Windows.Forms.Label();
			this.lblPurchaseOrder = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dtmPostDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// lblPostDate
			// 
			this.lblPostDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblPostDate.Location = new System.Drawing.Point(6, 6);
			this.lblPostDate.Name = "lblPostDate";
			this.lblPostDate.Size = new System.Drawing.Size(100, 20);
			this.lblPostDate.TabIndex = 2;
			this.lblPostDate.Text = "Post Date";
			this.lblPostDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dtmPostDate
			// 
			this.dtmPostDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmPostDate.Location = new System.Drawing.Point(108, 6);
			this.dtmPostDate.Name = "dtmPostDate";
			this.dtmPostDate.Size = new System.Drawing.Size(114, 20);
			this.dtmPostDate.TabIndex = 3;
			this.dtmPostDate.Tag = null;
			this.dtmPostDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmPostDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			// 
			// lblReturnNo
			// 
			this.lblReturnNo.ForeColor = System.Drawing.Color.Maroon;
			this.lblReturnNo.Location = new System.Drawing.Point(6, 28);
			this.lblReturnNo.Name = "lblReturnNo";
			this.lblReturnNo.Size = new System.Drawing.Size(100, 20);
			this.lblReturnNo.TabIndex = 7;
			this.lblReturnNo.Text = "Return No";
			this.lblReturnNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMasLoc
			// 
			this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
			this.lblMasLoc.Location = new System.Drawing.Point(6, 50);
			this.lblMasLoc.Name = "lblMasLoc";
			this.lblMasLoc.Size = new System.Drawing.Size(100, 20);
			this.lblMasLoc.TabIndex = 10;
			this.lblMasLoc.Text = "Master Location";
			this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRefNo
			// 
			this.lblRefNo.ForeColor = System.Drawing.Color.Maroon;
			this.lblRefNo.Location = new System.Drawing.Point(224, 73);
			this.lblRefNo.Name = "lblRefNo";
			this.lblRefNo.Size = new System.Drawing.Size(78, 20);
			this.lblRefNo.TabIndex = 15;
			this.lblRefNo.Text = "Reference No.";
			this.lblRefNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblVendor
			// 
			this.lblVendor.ForeColor = System.Drawing.Color.Maroon;
			this.lblVendor.Location = new System.Drawing.Point(6, 94);
			this.lblVendor.Name = "lblVendor";
			this.lblVendor.Size = new System.Drawing.Size(100, 20);
			this.lblVendor.TabIndex = 21;
			this.lblVendor.Text = "Vendor";
			this.lblVendor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblProductionLine
			// 
			this.lblProductionLine.Location = new System.Drawing.Point(448, 73);
			this.lblProductionLine.Name = "lblProductionLine";
			this.lblProductionLine.Size = new System.Drawing.Size(86, 20);
			this.lblProductionLine.TabIndex = 18;
			this.lblProductionLine.Text = "Production Line";
			this.lblProductionLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblVendorLocation
			// 
			this.lblVendorLocation.ForeColor = System.Drawing.Color.Maroon;
			this.lblVendorLocation.Location = new System.Drawing.Point(6, 116);
			this.lblVendorLocation.Name = "lblVendorLocation";
			this.lblVendorLocation.Size = new System.Drawing.Size(100, 20);
			this.lblVendorLocation.TabIndex = 25;
			this.lblVendorLocation.Text = "Vendor Location";
			this.lblVendorLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblReturnBy
			// 
			this.lblReturnBy.Location = new System.Drawing.Point(6, 72);
			this.lblReturnBy.Name = "lblReturnBy";
			this.lblReturnBy.Size = new System.Drawing.Size(100, 20);
			this.lblReturnBy.TabIndex = 13;
			this.lblReturnBy.Text = "Return By";
			this.lblReturnBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboReturnBy
			// 
			this.cboReturnBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboReturnBy.Location = new System.Drawing.Point(108, 72);
			this.cboReturnBy.Name = "cboReturnBy";
			this.cboReturnBy.Size = new System.Drawing.Size(114, 21);
			this.cboReturnBy.TabIndex = 14;
			this.cboReturnBy.SelectedIndexChanged += new System.EventHandler(this.cboReturnBy_SelectedIndexChanged);
			// 
			// txtReturnNo
			// 
			this.txtReturnNo.Location = new System.Drawing.Point(108, 28);
			this.txtReturnNo.Name = "txtReturnNo";
			this.txtReturnNo.Size = new System.Drawing.Size(114, 20);
			this.txtReturnNo.TabIndex = 8;
			this.txtReturnNo.Text = "";
			this.txtReturnNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReturnNo_KeyDown);
			this.txtReturnNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtReturnNo_Validating);
			// 
			// txtMasLoc
			// 
			this.txtMasLoc.Location = new System.Drawing.Point(108, 50);
			this.txtMasLoc.Name = "txtMasLoc";
			this.txtMasLoc.Size = new System.Drawing.Size(114, 20);
			this.txtMasLoc.TabIndex = 11;
			this.txtMasLoc.Text = "";
			this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
			this.txtMasLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasLoc_Validating);
			// 
			// txtRefNo
			// 
			this.txtRefNo.Location = new System.Drawing.Point(304, 73);
			this.txtRefNo.Name = "txtRefNo";
			this.txtRefNo.Size = new System.Drawing.Size(114, 20);
			this.txtRefNo.TabIndex = 16;
			this.txtRefNo.Text = "";
			this.txtRefNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRefNo_KeyDown);
			this.txtRefNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtRefNo_Validating);
			// 
			// txtProductionLine
			// 
			this.txtProductionLine.Location = new System.Drawing.Point(536, 73);
			this.txtProductionLine.Name = "txtProductionLine";
			this.txtProductionLine.Size = new System.Drawing.Size(114, 20);
			this.txtProductionLine.TabIndex = 19;
			this.txtProductionLine.Text = "";
			this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
			// 
			// txtVendorCode
			// 
			this.txtVendorCode.Location = new System.Drawing.Point(108, 95);
			this.txtVendorCode.Name = "txtVendorCode";
			this.txtVendorCode.Size = new System.Drawing.Size(114, 20);
			this.txtVendorCode.TabIndex = 22;
			this.txtVendorCode.Text = "";
			this.txtVendorCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendorCode_KeyDown);
			this.txtVendorCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendorCode_Validating);
			// 
			// txtVendorName
			// 
			this.txtVendorName.Location = new System.Drawing.Point(250, 95);
			this.txtVendorName.Name = "txtVendorName";
			this.txtVendorName.ReadOnly = true;
			this.txtVendorName.Size = new System.Drawing.Size(262, 20);
			this.txtVendorName.TabIndex = 24;
			this.txtVendorName.Text = "";
			// 
			// txtVendorLoc
			// 
			this.txtVendorLoc.Location = new System.Drawing.Point(108, 117);
			this.txtVendorLoc.Name = "txtVendorLoc";
			this.txtVendorLoc.Size = new System.Drawing.Size(114, 20);
			this.txtVendorLoc.TabIndex = 26;
			this.txtVendorLoc.Text = "";
			this.txtVendorLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendorLoc_KeyDown);
			this.txtVendorLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendorLoc_Validating);
			// 
			// btnReturnNo
			// 
			this.btnReturnNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnReturnNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnReturnNo.Location = new System.Drawing.Point(224, 28);
			this.btnReturnNo.Name = "btnReturnNo";
			this.btnReturnNo.Size = new System.Drawing.Size(24, 20);
			this.btnReturnNo.TabIndex = 9;
			this.btnReturnNo.Text = "...";
			this.btnReturnNo.Click += new System.EventHandler(this.btnReturnNo_Click);
			// 
			// btnMasLoc
			// 
			this.btnMasLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnMasLoc.Location = new System.Drawing.Point(224, 50);
			this.btnMasLoc.Name = "btnMasLoc";
			this.btnMasLoc.Size = new System.Drawing.Size(24, 20);
			this.btnMasLoc.TabIndex = 12;
			this.btnMasLoc.Text = "...";
			this.btnMasLoc.Click += new System.EventHandler(this.btnMasLoc_Click);
			// 
			// btnRefNo
			// 
			this.btnRefNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRefNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnRefNo.Location = new System.Drawing.Point(420, 73);
			this.btnRefNo.Name = "btnRefNo";
			this.btnRefNo.Size = new System.Drawing.Size(26, 20);
			this.btnRefNo.TabIndex = 17;
			this.btnRefNo.Text = "...";
			this.btnRefNo.Click += new System.EventHandler(this.btnRefNo_Click);
			// 
			// btnProductionLine
			// 
			this.btnProductionLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnProductionLine.Location = new System.Drawing.Point(652, 73);
			this.btnProductionLine.Name = "btnProductionLine";
			this.btnProductionLine.Size = new System.Drawing.Size(24, 20);
			this.btnProductionLine.TabIndex = 20;
			this.btnProductionLine.Text = "...";
			this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
			// 
			// btnVendorCode
			// 
			this.btnVendorCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnVendorCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnVendorCode.Location = new System.Drawing.Point(224, 95);
			this.btnVendorCode.Name = "btnVendorCode";
			this.btnVendorCode.Size = new System.Drawing.Size(24, 20);
			this.btnVendorCode.TabIndex = 23;
			this.btnVendorCode.Text = "...";
			this.btnVendorCode.Click += new System.EventHandler(this.btnVendorCode_Click);
			// 
			// btnVendorLoc
			// 
			this.btnVendorLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnVendorLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnVendorLoc.Location = new System.Drawing.Point(224, 117);
			this.btnVendorLoc.Name = "btnVendorLoc";
			this.btnVendorLoc.Size = new System.Drawing.Size(24, 20);
			this.btnVendorLoc.TabIndex = 27;
			this.btnVendorLoc.Text = "...";
			this.btnVendorLoc.Click += new System.EventHandler(this.btnVendorLoc_Click);
			// 
			// cboCCN
			// 
			this.cboCCN.AddItemSeparator = ';';
			this.cboCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboCCN.Caption = "";
			this.cboCCN.CaptionHeight = 17;
			this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboCCN.ColumnCaptionHeight = 17;
			this.cboCCN.ColumnFooterHeight = 17;
			this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboCCN.ContentHeight = 15;
			this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboCCN.DropDownWidth = 200;
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(566, 5);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(110, 21);
			this.cboCCN.TabIndex = 1;
			this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
				"ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}Re" +
				"cordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raise" +
				"d,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}" +
				"Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
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
				"th>17</DefaultRecSelWidth></Blob>";
			// 
			// lblCCN
			// 
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(532, 6);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(30, 20);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(616, 424);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 34;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(554, 424);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 33;
			this.btnHelp.Text = "&Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(130, 424);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 31;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(68, 424);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 30;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAdd.Location = new System.Drawing.Point(6, 424);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(60, 23);
			this.btnAdd.TabIndex = 29;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPrint.Location = new System.Drawing.Point(192, 424);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(60, 23);
			this.btnPrint.TabIndex = 32;
			this.btnPrint.Text = "&Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// dgrdData
			// 
			this.dgrdData.AllowDelete = true;
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(6, 140);
			this.dgrdData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdData.Name = "dgrdData";
			this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.dgrdData.PreviewInfo.ZoomFactor = 75;
			this.dgrdData.PrintInfo.ShowOptionsDialog = false;
			this.dgrdData.RecordSelectorWidth = 17;
			this.dgrdData.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.dgrdData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.dgrdData.RowHeight = 15;
			this.dgrdData.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.dgrdData.Size = new System.Drawing.Size(668, 280);
			this.dgrdData.TabIndex = 28;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdData_BeforeDelete);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Line\" DataF" +
				"ield=\"Line\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Part Number\" DataField=\"ITM_ProductCode\"><ValueItems /><GroupInfo /></C1D" +
				"ataColumn><C1DataColumn Level=\"0\" Caption=\"Part Name\" DataField=\"Description\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Model\" " +
				"DataField=\"Revision\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
				"el=\"0\" Caption=\"Buying Unit\" DataField=\"MST_UnitOfMeasureCode\"><ValueItems /><Gr" +
				"oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"From Location\" DataFie" +
				"ld=\"MST_LocationCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"From Bin\" DataField=\"MST_BINCode\"><ValueItems /><GroupInfo /></" +
				"C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Quantity\" DataField=\"Quantity\"><Va" +
				"lueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"UnitPric" +
				"e\" DataField=\"UnitPrice\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
				" Level=\"0\" Caption=\"Amount\" DataField=\"Amount\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"% VAT\" DataField=\"VATPercent\"><ValueIt" +
				"ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"VAT Amount\" D" +
				"ataField=\"VATAmount\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
				"el=\"0\" Caption=\"Total Amount\" DataField=\"TotalAmount\"><ValueItems /><GroupInfo /" +
				"></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapp" +
				"er\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style85{}Ina" +
				"ctive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style78{}Style79{" +
				"}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style72{}Style73{" +
				"}Style70{AlignHorz:Center;}Style71{AlignHorz:Far;}Style76{AlignHorz:Center;}Styl" +
				"e77{AlignHorz:Far;}Style74{}Style75{}Style84{}Style87{}Style86{}Style81{}Style80" +
				"{}Style83{AlignHorz:Far;}Style82{AlignHorz:Center;}Style89{AlignHorz:Far;}Style8" +
				"8{AlignHorz:Center;}Style90{}Style91{}Style92{}Style93{}Heading{Wrap:True;BackCo" +
				"lor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}St" +
				"yle18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Center;ForeColor:Maroon;}Sty" +
				"le17{AlignHorz:Far;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style29{A" +
				"lignHorz:Near;}Style28{AlignHorz:Center;ForeColor:Maroon;}Style27{}Style22{Align" +
				"Horz:Center;ForeColor:Maroon;}Style24{}Style9{}Style8{}Style26{}Style25{}Style5{" +
				"}Style4{}Style7{}Style6{}Style1{}Style23{AlignHorz:Near;}Style3{}Style2{}Style21" +
				"{}Style20{}OddRow{}Style49{}Style38{}Style39{}Style36{}FilterBar{}Style34{AlignH" +
				"orz:Center;ForeColor:Maroon;}Style35{AlignHorz:Near;}Style32{}Style33{}Style30{}" +
				"Style37{}Style48{}Style31{}Normal{}Style41{AlignHorz:Near;}Style40{AlignHorz:Cen" +
				"ter;ForeColor:Maroon;}Style43{}Style42{}Style45{}Style44{}Style47{AlignHorz:Near" +
				";}Style46{AlignHorz:Center;ForeColor:Maroon;}EvenRow{BackColor:Aqua;}Style59{Ali" +
				"gnHorz:Far;}Style58{AlignHorz:Center;ForeColor:Maroon;}RecordSelector{AlignImage" +
				":Center;}Style51{}Style50{}Footer{}Style52{AlignHorz:Center;ForeColor:Maroon;}St" +
				"yle53{AlignHorz:Near;}Style54{}Style55{}Style56{}Style57{}Caption{AlignHorz:Cent" +
				"er;}Style69{}Style68{}Style63{}Style62{}Style61{}Style60{}Style67{}Style66{}Styl" +
				"e65{AlignHorz:Far;}Style64{AlignHorz:Center;ForeColor:Maroon;}Group{AlignVert:Ce" +
				"nter;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}</Data></Styles><Splits><C1." +
				"Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" C" +
				"olumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"17\" " +
				"DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRec" +
				"t>0, 0, 664, 276</ClientRect><BorderSide>0</BorderSide><CaptionStyle parent=\"Sty" +
				"le2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle par" +
				"ent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><F" +
				"ooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\"" +
				" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"Highl" +
				"ightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowSty" +
				"le parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me" +
				"=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Norma" +
				"l\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
				"=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" " +
				"me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle par" +
				"ent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Vi" +
				"sible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>33</Wid" +
				"th><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
				"gStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><Foo" +
				"terStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25" +
				"\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"S" +
				"tyle1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Col" +
				"umnDivider><Width>140</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColum" +
				"n><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"S" +
				"tyle1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle p" +
				"arent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><" +
				"GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDi" +
				"vider>DarkGray,Single</ColumnDivider><Width>233</Width><Height>15</Height><DCIdx" +
				">2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visib" +
				"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>110</Width" +
				"><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
				"tyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><Foote" +
				"rStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" " +
				"/><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Sty" +
				"le1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colum" +
				"nDivider><Width>65</Width><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><" +
				"C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Styl" +
				"e1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle pare" +
				"nt=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><Gro" +
				"upFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivid" +
				"er>DarkGray,Single</ColumnDivider><Width>110</Width><Height>15</Height><DCIdx>5<" +
				"/DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Styl" +
				"e52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"St" +
				"yle54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"S" +
				"tyle1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>" +
				"True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>150</Width><H" +
				"eight>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
				"e parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterSt" +
				"yle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><" +
				"GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1" +
				"\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDi" +
				"vider><Width>72</Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\"" +
				" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><ColumnDivider>" +
				"DarkGray,Single</ColumnDivider><Width>73</Width><Height>15</Height><DCIdx>8</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style70\"" +
				" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=\"Style7" +
				"2\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Visible>True" +
				"</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>88</Width><Height" +
				">15</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=" +
				"\"Style80\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider" +
				"><Width>66</Width><Height>15</Height><DCIdx>10</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style82\" /><Style parent=\"Style1\" me=" +
				"\"Style83\" /><FooterStyle parent=\"Style3\" me=\"Style84\" /><EditorStyle parent=\"Sty" +
				"le5\" me=\"Style85\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style87\" /><GroupFoote" +
				"rStyle parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible><ColumnDivider>Dark" +
				"Gray,Single</ColumnDivider><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn" +
				"><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style88\" /><Style parent=\"St" +
				"yle1\" me=\"Style89\" /><FooterStyle parent=\"Style3\" me=\"Style90\" /><EditorStyle pa" +
				"rent=\"Style5\" me=\"Style91\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style93\" /><G" +
				"roupFooterStyle parent=\"Style1\" me=\"Style92\" /><Visible>True</Visible><ColumnDiv" +
				"ider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>12</DCIdx></C1Disp" +
				"layColumn></internalCols></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><" +
				"Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style paren" +
				"t=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"" +
				"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"N" +
				"ormal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"N" +
				"ormal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Headin" +
				"g\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"" +
				"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</hor" +
				"zSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><Cli" +
				"entArea>0, 0, 664, 276</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" " +
				"/><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// lblInvoice
			// 
			this.lblInvoice.Location = new System.Drawing.Point(348, 36);
			this.lblInvoice.Name = "lblInvoice";
			this.lblInvoice.TabIndex = 35;
			this.lblInvoice.Text = "Invoice";
			this.lblInvoice.Visible = false;
			// 
			// lblPurchaseOrder
			// 
			this.lblPurchaseOrder.Location = new System.Drawing.Point(346, 10);
			this.lblPurchaseOrder.Name = "lblPurchaseOrder";
			this.lblPurchaseOrder.TabIndex = 36;
			this.lblPurchaseOrder.Text = "Purchase Order";
			this.lblPurchaseOrder.Visible = false;
			// 
			// POReturnToVendor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(682, 453);
			this.Controls.Add(this.lblPurchaseOrder);
			this.Controls.Add(this.lblInvoice);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.txtReturnNo);
			this.Controls.Add(this.txtMasLoc);
			this.Controls.Add(this.txtRefNo);
			this.Controls.Add(this.txtProductionLine);
			this.Controls.Add(this.txtVendorCode);
			this.Controls.Add(this.txtVendorName);
			this.Controls.Add(this.txtVendorLoc);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnReturnNo);
			this.Controls.Add(this.cboReturnBy);
			this.Controls.Add(this.lblReturnNo);
			this.Controls.Add(this.dtmPostDate);
			this.Controls.Add(this.lblPostDate);
			this.Controls.Add(this.lblMasLoc);
			this.Controls.Add(this.lblRefNo);
			this.Controls.Add(this.lblVendor);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.lblVendorLocation);
			this.Controls.Add(this.lblReturnBy);
			this.Controls.Add(this.btnMasLoc);
			this.Controls.Add(this.btnRefNo);
			this.Controls.Add(this.btnProductionLine);
			this.Controls.Add(this.btnVendorCode);
			this.Controls.Add(this.btnVendorLoc);
			this.Name = "POReturnToVendor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Return To Vendor";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.POReturnToVendor_Closing);
			this.Load += new System.EventHandler(this.POReturnToVendor_Load);
			((System.ComponentModel.ISupportInitialize)(this.dtmPostDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region private methods
		private void SwitchFormMode()
		{
			switch (enumAction)
			{
				case EnumAction.Add:
					#region disable
					btnAdd.Enabled = false;
					btnDelete.Enabled = false;
					btnReturnNo.Enabled = false;
					#endregion

					#region enable
					dtmPostDate.Enabled = true;
					txtMasLoc.Enabled = true;
					btnMasLoc.Enabled = true;
					cboReturnBy.Enabled = true;
					txtRefNo.Enabled = true;
					btnRefNo.Enabled = true;
					txtProductionLine.Enabled = true;
					btnProductionLine.Enabled = true;
					txtVendorCode.Enabled = true;
					btnVendorCode.Enabled = true;
					txtVendorLoc.Enabled = true;
					btnVendorLoc.Enabled = true;
					btnSave.Enabled = true;
					// grid
					dgrdData.Splits[0].Locked = false;
					//dgrdData.AllowAddNew = true;
					#endregion
					break;
				default:
					#region enable
					btnAdd.Enabled = true;
					if (voReturnMaster != null && voReturnMaster.ReturnToVendorMasterID > 0)
						btnDelete.Enabled = true;
					else
						btnDelete.Enabled = false;
					btnReturnNo.Enabled = true;
					#endregion

					#region disable
					dtmPostDate.Enabled = false;
					txtMasLoc.Enabled = false;
					btnMasLoc.Enabled = false;
					cboReturnBy.Enabled = false;
					txtRefNo.Enabled = false;
					btnRefNo.Enabled = false;
					txtProductionLine.Enabled = false;
					btnProductionLine.Enabled = false;
					txtVendorCode.Enabled = false;
					btnVendorCode.Enabled = false;
					txtVendorLoc.Enabled = false;
					btnVendorLoc.Enabled = false;
					btnSave.Enabled = false;
					// grid
					dgrdData.Splits[0].Locked = true;
					foreach (C1DisplayColumn c1Col in dgrdData.Splits[0].DisplayColumns)
						c1Col.Button = false;
					//dgrdData.AllowAddNew = false;
					#endregion
					break;
			}
		}
		private void LoadReturnData(int pintReturnMasterID)
		{
			// master data
			DataRow drowInfo;
			voReturnMaster = (PO_ReturnToVendorMasterVO)boReturn.GetMasterInfo(pintReturnMasterID, out drowInfo);
			if (pintReturnMasterID > 0)
			{
				// display data to form
				dtmPostDate.Value = voReturnMaster.PostDate;
				txtReturnNo.Text = voReturnMaster.RTVNo;
				txtReturnNo.Tag = voReturnMaster.ReturnToVendorMasterID;
				txtMasLoc.Text = drowInfo[MASTERLOCCODE_FLD].ToString();
				txtMasLoc.Tag = voReturnMaster.MasterLocationID;
				txtVendorCode.Text = drowInfo[PARTYCODE_FLD].ToString().Trim();
				txtVendorCode.Tag = voReturnMaster.PartyID;
				txtVendorName.Text = drowInfo[PARTYNAME_FLD].ToString().Trim();
				txtVendorLoc.Text = drowInfo[PURCHASELOCCODE_FLD].ToString().Trim();
				txtProductionLine.Text = drowInfo[PRODUCTIONLINECODE_FLD].ToString().Trim();
				txtProductionLine.Tag = voReturnMaster.ProductionLineId;
				if (Convert.ToBoolean(drowInfo[PO_ReturnToVendorMasterTable.BYPO_FLD]))
				{
					cboReturnBy.SelectedIndex = 0;
					txtRefNo.Text = drowInfo[PURCHASEORDERCODE_FLD].ToString();
					txtRefNo.Tag = voReturnMaster.PurchaseOrderMasterID;
				}
				else
				{
					cboReturnBy.SelectedIndex = 1;
					txtRefNo.Text = drowInfo[PO_InvoiceMasterTable.INVOICENO_FLD].ToString();
					txtRefNo.Tag = voReturnMaster.InvoiceMasterID;
				}
			}
			// detail data
			dstData = boReturn.GetDetailData(pintReturnMasterID);
			BindDataToGrid();
			SwitchFormMode();
		}
		private void BindDataToGrid()
		{
			dgrdData.DataSource = dstData.Tables[0];
			// restore grid layout
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
			foreach (C1DisplayColumn c1Col in dgrdData.Splits[0].DisplayColumns)
				c1Col.Locked = true;

			dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Locked = false;
			dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Button = true;
			dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Locked = false;
			dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Button = true;
			dgrdData.Splits[0].DisplayColumns[PO_ReturnToVendorDetailTable.QUANTITY_FLD].Locked = false;
			dgrdData.Splits[0].DisplayColumns[PO_ReturnToVendorDetailTable.UNITPRICE_FLD].Locked = false;
			dgrdData.Splits[0].DisplayColumns[PO_ReturnToVendorDetailTable.VATPERCENT_FLD].Locked = false;

			// number format in grid
			dgrdData.Columns[PO_ReturnToVendorDetailTable.QUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			dgrdData.Columns[PO_ReturnToVendorDetailTable.VATAMOUNT_FLD].NumberFormat = Constants.DECIMAL_LONG_FORMAT;
			dgrdData.Columns[PO_ReturnToVendorDetailTable.VATPERCENT_FLD].NumberFormat = Constants.DECIMAL_LONG_FORMAT;
			dgrdData.Columns[PO_ReturnToVendorDetailTable.TOTALAMOUNT_FLD].NumberFormat = Constants.DECIMAL_LONG_FORMAT;
			dgrdData.Columns[PO_ReturnToVendorDetailTable.AMOUNT_FLD].NumberFormat = Constants.DECIMAL_LONG_FORMAT;
			dgrdData.Columns[PO_ReturnToVendorDetailTable.UNITPRICE_FLD].NumberFormat = Constants.DECIMAL_LONG_FORMAT;
		}
		private void ResetForm()
		{
			txtReturnNo.Text = string.Empty;
			txtReturnNo.Tag = null;
			txtMasLoc.Text = string.Empty;
			txtMasLoc.Tag = null;
			cboReturnBy.SelectedIndex = -1;
			txtRefNo.Text = string.Empty;
			txtRefNo.Tag = null;
			txtProductionLine.Text = string.Empty;
			txtProductionLine.Tag = null;
			txtVendorCode.Text = string.Empty;
			txtVendorCode.Tag = null;
			txtVendorName.Text = string.Empty;
			txtVendorLoc.Text = string.Empty;

			if (dstData != null && dstData.Tables.Count > 0)
				dstData.Tables[0].Clear();
		}
		private void ChangeMasLoc()
		{
			if (dstData != null && dstData.Tables.Count > 0)
			{
				foreach (DataRow drowData in dstData.Tables[0].Rows)
				{
					if (drowData.RowState != DataRowState.Deleted)
					{
						drowData[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = string.Empty;
						drowData[MST_LocationTable.LOCATIONID_FLD] = null;
						drowData[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = string.Empty;
						drowData[MST_BINTable.BINID_FLD] = null;
					}
				}
			}
		}
		private void ReturnByPO(DataRow pdrowData)
		{
			txtRefNo.Text = pdrowData[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
			txtRefNo.Tag = pdrowData[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].ToString();

			dtmOrderDate = (DateTime) pdrowData[PO_PurchaseOrderMasterTable.ORDERDATE_FLD];

			DataRow drowVendorInfo = boReturn.GetVendorInfo(Convert.ToInt32(pdrowData[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD]));
			txtVendorCode.Text = drowVendorInfo[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].ToString();
			txtVendorCode.Tag = Convert.ToInt32(pdrowData[PO_PurchaseOrderMasterTable.PARTYID_FLD]);
			txtVendorName.Text = drowVendorInfo[MST_PartyTable.NAME_FLD].ToString();
			txtVendorLoc.Text = drowVendorInfo[MST_PartyLocationTable.TABLE_NAME + MST_PartyLocationTable.CODE_FLD].ToString();
			txtVendorLoc.Tag = Convert.ToInt32(pdrowData[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD]);
			dtbReceived = boReturn.GetListOfReceivedProductsFromPurchaseOrder(Convert.ToInt32(txtRefNo.Tag));
			InputPurchaseOrderDetailIntoGrid(dtbReceived);
			BindDataToGrid();
		}
		private void InputPurchaseOrderDetailIntoGrid(DataTable dtData)
		{
			if (dstData != null && dstData.Tables.Count > 0)
				dstData.Tables[0].Clear();
			else
				dstData = boReturn.GetDetailData(0);

			int intLine = 1;
			foreach (DataRow drowTmp in dtData.Rows)
			{
				DataRow drowNewRow = dstData.Tables[0].NewRow();
				drowNewRow[PO_ReturnToVendorDetailTable.LINE_FLD] = intLine;
				// purchase order detail id as ref detail
				drowNewRow[PO_ReturnToVendorDetailTable.REFDETAILID_FLD] = drowTmp[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD];
				// ProductID
				drowNewRow[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD] = drowTmp[PO_PurchaseOrderDetailTable.PRODUCTID_FLD];
				//LocationID
				drowNewRow[PO_ReturnToVendorDetailTable.LOCATIONID_FLD] = drowTmp[ITM_ProductTable.LOCATIONID_FLD];
				//Location Code
				drowNewRow[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = drowTmp["LocationCode"];
				//BinID
				drowNewRow[PO_ReturnToVendorDetailTable.BINID_FLD] = drowTmp[ITM_ProductTable.BINID_FLD];
				//Bin Code
				drowNewRow[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = drowTmp["BinCode"];
				//Product code
				drowNewRow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = drowTmp[ITM_ProductTable.CODE_FLD];
				//product description
				drowNewRow[ITM_ProductTable.DESCRIPTION_FLD] = drowTmp[ITM_ProductTable.DESCRIPTION_FLD];
				//revision
				drowNewRow[ITM_ProductTable.REVISION_FLD] = drowTmp[ITM_ProductTable.REVISION_FLD];
				//Stock unit
				drowNewRow[PO_ReturnToVendorDetailTable.STOCKUMID_FLD] = drowTmp[ITM_ProductTable.STOCKUMID_FLD];
				//Buying unit
				drowNewRow[PO_ReturnToVendorDetailTable.BUYINGUMID_FLD] = drowTmp[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD];
				drowNewRow[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drowTmp[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
				drowNewRow[PO_ReturnToVendorDetailTable.UMRATE_FLD] = drowTmp[PO_PurchaseOrderDetailTable.UMRATE_FLD];
				
				drowNewRow[PO_ReturnToVendorDetailTable.UNITPRICE_FLD] = drowTmp[PO_ReturnToVendorDetailTable.UNITPRICE_FLD];
				drowNewRow[PO_ReturnToVendorDetailTable.AMOUNT_FLD] = DBNull.Value;
				drowNewRow[PO_ReturnToVendorDetailTable.VATPERCENT_FLD] = drowTmp[PO_ReturnToVendorDetailTable.VATPERCENT_FLD];
				drowNewRow[PO_ReturnToVendorDetailTable.VATAMOUNT_FLD] = DBNull.Value;
				drowNewRow[PO_ReturnToVendorDetailTable.TOTALAMOUNT_FLD] = DBNull.Value;
				
				dstData.Tables[0].Rows.Add(drowNewRow);
				intLine = intLine + 1;
			}
		}

		private void ReturnByInvoice(DataRow pdrowData)
		{
			txtRefNo.Text = pdrowData[PO_InvoiceMasterTable.INVOICENO_FLD].ToString();
			txtRefNo.Tag = pdrowData[PO_InvoiceMasterTable.INVOICEMASTERID_FLD];

			txtVendorCode.Text = pdrowData["VendorCode"].ToString();
			txtVendorCode.Tag = Convert.ToInt32(pdrowData[MST_PartyTable.PARTYID_FLD]);
			txtVendorName.Text = pdrowData["VendorName"].ToString();
			txtVendorLoc.Text = pdrowData[MST_PartyLocationTable.TABLE_NAME + MST_PartyLocationTable.CODE_FLD].ToString();
			txtVendorLoc.Tag = Convert.ToInt32(pdrowData[MST_PartyLocationTable.PARTYLOCATIONID_FLD]);

			dstData = boReturn.GetDetailByInvoiceMasterID(Convert.ToInt32(txtRefNo.Tag));
			BindDataToGrid();
		}
		private void ShowPOSlipReport()
		{	
			#region Const
			const string APPLICATION_PATH  = @"PCSMain\bin\Debug";
								
			const string REPORT_LAYOUT = "ReturnToVendorSlip.xml";
			const string REPORT_COMPANY_FLD = "fldCompany";
			const string RPT_TITLE_FLD = "fldTitle";
			const string RPT_INVOICE_NO_FLD = "FldInvoiceNo";
			const string RPT_RETURN_NO_FLD = "FldREturnNo";
			const string RPT_POSTDATE_FLD = "FldPostDate";
			const string RPT_PO_NO_FLD = "FldPONo";
			const string RPT_VEDORCODE_FLD = "fldInvoiceNoValue";
			const string RPT_VEDORNAME_FLD = "FldVedorName";
			const string RPT_RETURN_NO2_FLD = "FldREturnNo2";

			#endregion

			this.Cursor = Cursors.WaitCursor;
			C1PrintPreviewDialog   printPreview = new C1PrintPreviewDialog();
			C1PrintPreviewDialogBO boReport = new C1PrintPreviewDialogBO();

			DataTable dtbResult = boReport.GetPOSlipDatavedor(voReturnMaster.ReturnToVendorMasterID);

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
			//				reportBuilder.SourceDataTable = dtbResult;
			reportBuilder.ReportDefinitionFolder = strReportPath;
			
			reportBuilder.ReportLayoutFile = REPORT_LAYOUT;

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

			reportBuilder.SourceDataTable = dtbResult;
			reportBuilder.MakeDataTableForRender();

			// And show it in preview dialog				
			reportBuilder.ReportViewer = printPreview.ReportViewer;
			reportBuilder.RenderReport();				
			// get Return to vendor master
			DataRow drowInfo;
			boReturn.GetMasterInfo(voReturnMaster.ReturnToVendorMasterID, out drowInfo);

			//Header information get from system params
			reportBuilder.DrawPredefinedField(REPORT_COMPANY_FLD, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
			reportBuilder.DrawPredefinedField(RPT_POSTDATE_FLD,voReturnMaster.PostDate.ToString(Constants.DATETIME_FORMAT_HOUR));
			reportBuilder.DrawPredefinedField(RPT_RETURN_NO_FLD,voReturnMaster.RTVNo);
			reportBuilder.DrawPredefinedField(RPT_VEDORCODE_FLD,drowInfo[PARTYCODE_FLD].ToString().Trim());
			reportBuilder.DrawPredefinedField(RPT_VEDORNAME_FLD,drowInfo[PARTYNAME_FLD].ToString().Trim());		
			reportBuilder.DrawPredefinedField(RPT_PO_NO_FLD,drowInfo[PURCHASEORDERCODE_FLD].ToString().Trim());
			reportBuilder.DrawPredefinedField(RPT_INVOICE_NO_FLD,drowInfo[PO_InvoiceMasterTable.INVOICENO_FLD].ToString().Trim());
			reportBuilder.DrawPredefinedField(RPT_RETURN_NO2_FLD,voReturnMaster.RTVNo);
	
				
			reportBuilder.RefreshReport();				
			
			//Print report
			try
			{
				printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FLD).Text;
			}
			catch
			{}
				
			printPreview.Show();
		}
		private void CalculateAmount()
		{
			if (dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.QUANTITY_FLD] != DBNull.Value 
				&& dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] != DBNull.Value)
			{
				dgrdData.Columns[PO_ReturnToVendorDetailTable.AMOUNT_FLD].Value = Decimal.Parse(dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString())
					* decimal.Parse(dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString());
				if (dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.VATPERCENT_FLD] != DBNull.Value)
				{
					dgrdData.Columns[PO_ReturnToVendorDetailTable.VATAMOUNT_FLD].Value = decimal.Parse(dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.VATPERCENT_FLD].ToString()) 
						* decimal.Parse(dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.AMOUNT_FLD].ToString()) /100;
				}
				else
					dgrdData.Columns[PO_ReturnToVendorDetailTable.VATAMOUNT_FLD].Value = DBNull.Value;
				if (dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.VATAMOUNT_FLD].ToString() != string.Empty)
				{
					dgrdData.Columns[PO_ReturnToVendorDetailTable.TOTALAMOUNT_FLD].Value = decimal.Parse(dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.AMOUNT_FLD].ToString())
						+ decimal.Parse(dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.VATAMOUNT_FLD].ToString());
				}
				else
					dgrdData.Columns[PO_ReturnToVendorDetailTable.TOTALAMOUNT_FLD].Value = decimal.Parse(dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.AMOUNT_FLD].ToString());
			}
			else
			{
				dgrdData[dgrdData.Row,PO_ReturnToVendorDetailTable.AMOUNT_FLD] = DBNull.Value;
				dgrdData[dgrdData.Row,PO_ReturnToVendorDetailTable.VATAMOUNT_FLD] = DBNull.Value;
				dgrdData[dgrdData.Row,PO_ReturnToVendorDetailTable.TOTALAMOUNT_FLD] = DBNull.Value;
			}

		}
		private bool IsExisted(int pintProductID, int pintCurrentRow)
		{
			for (int i=0 ; i<dgrdData.RowCount; i++)
			{
				if (i != pintCurrentRow)
				{
					if (dgrdData[i,SO_ReturnedGoodsDetailTable.PRODUCTID_FLD] != DBNull.Value 
						&& dgrdData[i,SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString().Trim() != String.Empty
						&& int.Parse(dgrdData[i,SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString().Trim()) == pintProductID)
					{
						return true;
					}
				}
			}
			return false;
		}
		private bool ValidateMandatoryControl() 
		{
			if (cboCCN.SelectedIndex < 0) 
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				cboCCN.Focus();
				return false;
			}
			if (dtmPostDate.Value == DBNull.Value || dtmPostDate.Text.Trim() == String.Empty) 
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				dtmPostDate.Focus();
				return false;
			}
			//check the PostDate in the current period
			if (!FormControlComponents.CheckDateInCurrentPeriod((DateTime)dtmPostDate.Value))
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD, MessageBoxIcon.Warning);
				dtmPostDate.Focus();
				return false;
			}

			//check the PostDate smaller than the current date
			if ((DateTime)dtmPostDate.Value > new UtilsBO().GetDBDate())
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_INV_TRANSACTION_CANNOT_IN_FUTURE, MessageBoxIcon.Warning);
				dtmPostDate.Focus();
				return false;
			}
		
			if (txtReturnNo.Text.Trim() == String.Empty) 
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
				txtReturnNo.Focus();
				return false;
			}
			if (txtMasLoc.Tag == null)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				txtMasLoc.Focus();
				return false;
			}
			if (txtRefNo.Tag == null)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
				txtRefNo.Focus();
				return false;
			}
			//check the Post data must be greater than the Transdate in the PO Purchase Order
			if ((DateTime)dtmPostDate.Value < DateTime.Parse(dtmOrderDate.ToShortDateString()))
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_MUST_HIGHER_THANPODATE, MessageBoxIcon.Warning);
				dtmPostDate.Focus();
				return false;
			}

			if (txtVendorCode.Tag == null)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_SELECTVENDOR,MessageBoxIcon.Warning);
				txtVendorCode.Focus();
				return false;
			}

			if (txtVendorLoc.Tag == null)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_PURLOC,MessageBoxIcon.Warning);
				txtVendorLoc.Focus();
				return false;
			}

            int intDetailRows = dgrdData.RowCount;
			
			if (intDetailRows <= 0)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_NOPRODUCT,MessageBoxIcon.Warning);
				dgrdData.Focus();
				dgrdData.Row = 0;
				dgrdData.Col = dstData.Tables[0].Columns.IndexOf(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD);

				return false;
			}
			//Check postdate in configuration
			if (!FormControlComponents.CheckPostDateInConfiguration((DateTime)dtmPostDate.Value))
				return false;
			return true;
		}
		private bool CheckGrid()
		{
			if(dstData.Tables.Count <= 0) return false;
			int intRowNumber = dgrdData.RowCount;
			
			for (int i = 0; i < intRowNumber; i++)
			{
				if(dgrdData[i, PO_ReturnToVendorDetailTable.PRODUCTID_FLD] == null ||
					dgrdData[i, PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString().Trim() == String.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_PRODUCT,MessageBoxIcon.Warning);
					
					dgrdData.Row = i;
					dgrdData.Col = dstData.Tables[0].Columns.IndexOf(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD);
					dgrdData.Focus();
					return false;
				}
				if (dgrdData[i, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_BIN_FOR_LOCATION,MessageBoxIcon.Warning);
					dgrdData.Focus();
					dgrdData.Row = i;
					dgrdData.Col = dstData.Tables[0].Columns.IndexOf(MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD);
					return false;
				}
				if(dgrdData[i, PO_ReturnToVendorDetailTable.QUANTITY_FLD] == null ||
					decimal.Parse(dgrdData[i, PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString()) <=0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_RETURNQTY,MessageBoxIcon.Warning);
					dgrdData.Focus();
					dgrdData.Row = i;
					dgrdData.Col = dstData.Tables[0].Columns.IndexOf(PO_ReturnToVendorDetailTable.QUANTITY_FLD);

					return false;
				}
				else
				{
					if (cboReturnBy.SelectedIndex == 0)	// return by purchase order
					{
						//First get the product Id for this row
						string strProductID = dgrdData[i, PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString();
						
						//get the total commit quantity
						decimal decTotalRemain = 0;
						decimal decReturnQuantity = 0;
						
						DataRow[] drow = dtbReceived.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID);
						if (drow.Length > 1)
						{
						    decTotalRemain = drow.Sum(t => Decimal.Parse(t[TOTAL_REMAIN_FLD].ToString()));
						    for (int j = i; j < dgrdData.RowCount; j++)
							{
								if (dgrdData[i, ITM_ProductTable.PRODUCTID_FLD].ToString() == dgrdData[j, ITM_ProductTable.PRODUCTID_FLD].ToString())
									decReturnQuantity += Decimal.Parse(dgrdData[j, PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString());
								if (decReturnQuantity > decTotalRemain)
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_RETURNQTY,MessageBoxIcon.Warning);
									dgrdData.Row = j;
									dgrdData.Col = dstData.Tables[0].Columns.IndexOf(PO_ReturnToVendorDetailTable.QUANTITY_FLD);
									dgrdData.Focus();
									return false;
								}
							}
						}
					}
					else
					{
						// return by invoice
					}
				}
				if(dgrdData[i, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] == null ||
					decimal.Parse(dgrdData[i, PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) <=0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					dgrdData.Focus();
					dgrdData.Row = i;
					dgrdData.Col = dstData.Tables[0].Columns.IndexOf(PO_PurchaseOrderDetailTable.UNITPRICE_FLD);

					return false;
				}
				
				if(dgrdData[i, PO_ReturnToVendorDetailTable.BUYINGUMID_FLD] == null ||
					dgrdData[i, PO_ReturnToVendorDetailTable.BUYINGUMID_FLD].ToString().Trim() == String.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_BUYINGUNIT,MessageBoxIcon.Warning);
					dgrdData.Focus();
					dgrdData.Row = i;
					dgrdData.Col = dstData.Tables[0].Columns.IndexOf(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD);

					return false;
				}

				
				if(dgrdData[i, PO_ReturnToVendorDetailTable.LOCATIONID_FLD] == null ||
					dgrdData[i, PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString().Trim() == String.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_LOCATION,MessageBoxIcon.Warning);
					dgrdData.Focus();
					dgrdData.Row = i;
					dgrdData.Col = dstData.Tables[0].Columns.IndexOf(MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD);

					return false;
				}
			}
			
			return true;
		}

		private bool SaveData()
		{
			// assign master value
			voReturnMaster = new PO_ReturnToVendorMasterVO();
			voReturnMaster.CCNID = Convert.ToInt32(cboCCN.SelectedValue);
			voReturnMaster.PostDate = (DateTime)dtmPostDate.Value;
			voReturnMaster.RTVNo = txtReturnNo.Text.Trim();
			voReturnMaster.MasterLocationID = Convert.ToInt32(txtMasLoc.Tag);
			int intRefMasterID = Convert.ToInt32(txtRefNo.Tag);
			if (cboReturnBy.SelectedIndex == 0)
			{
				voReturnMaster.ByPO = true;
				voReturnMaster.PurchaseOrderMasterID = intRefMasterID;
			}
			else
			{
				voReturnMaster.ByInvoice = true;
				voReturnMaster.InvoiceMasterID = intRefMasterID;
			}
			try
			{
				voReturnMaster.ProductionLineId = Convert.ToInt32(txtProductionLine.Tag);
			}
			catch{}
			voReturnMaster.PartyID = Convert.ToInt32(txtVendorCode.Tag);
			voReturnMaster.PurchaseLocID = Convert.ToInt32(txtVendorLoc.Tag);

			FormControlComponents.SynchronyGridData(dgrdData);
			voReturnMaster.ReturnToVendorMasterID = boReturn.AddNewReturnToVendor(voReturnMaster, dstData);
			return true;
		}
		#endregion

		private void POReturnToVendor_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".POReturnToVendor_Load()";
			try
			{
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
					cboCCN.SelectedValue = SystemProperty.CCNID;
				else
					cboCCN.SelectedIndex = 0;

				// init combo
				cboReturnBy.Items.AddRange(new string[]{lblPurchaseOrder.Text, lblInvoice.Text});
				
				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				// set custom format for post date control
				dtmPostDate.CustomFormat = Constants.DATETIME_FORMAT;
				SwitchFormMode();
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

		private void txtReturnNo_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtReturnNo_Validating()";
			try
			{
				if (txtReturnNo.Modified)
				{
					if (txtReturnNo.Text != string.Empty)
					{
						Hashtable htCondition = new Hashtable();					
						htCondition.Add(PO_ReturnToVendorMasterTable.CCNID_FLD, Convert.ToInt32(cboCCN.SelectedValue));						
						DataRowView drvResult = FormControlComponents.OpenSearchForm(PO_ReturnToVendorMasterTable.TABLE_NAME, PO_ReturnToVendorMasterTable.RTVNO_FLD, txtReturnNo.Text.Trim(), htCondition, false);
						if (drvResult != null)
						{
							LoadReturnData(Convert.ToInt32(drvResult[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD]));
							btnDelete.Enabled = true;
						}
						else
							e.Cancel = true;
					}
					else // reset form
						ResetForm();
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

		private void txtReturnNo_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtReturnNo_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnReturnNo_Click(null, null);
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

		private void btnReturnNo_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnReturnNo_Click()";
			try
			{
				Hashtable htCondition = new Hashtable();				
				htCondition.Add(PO_ReturnToVendorMasterTable.CCNID_FLD, Convert.ToInt32(cboCCN.SelectedValue));
				DataRowView drvResult = FormControlComponents.OpenSearchForm(PO_ReturnToVendorMasterTable.TABLE_NAME, PO_ReturnToVendorMasterTable.RTVNO_FLD, txtReturnNo.Text.Trim(), htCondition, true);
				if (drvResult != null)
				{
					LoadReturnData(Convert.ToInt32(drvResult[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD]));
					btnDelete.Enabled = true;
				}
				else
					txtReturnNo.Focus();
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

		private void txtMasLoc_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_Validating()";
			try
			{
				if (!txtMasLoc.Modified) return;
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					txtMasLoc.Tag = null;
					ChangeMasLoc();
					return;
				}
				Hashtable htCondition = new Hashtable();
				htCondition.Add(PO_ReturnToVendorMasterTable.CCNID_FLD, Convert.ToInt32(cboCCN.SelectedValue));
				DataRowView drvResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text.Trim(), htCondition, false);
				if (drvResult != null)
				{
					txtMasLoc.Text = drvResult[MST_MasterLocationTable.CODE_FLD].ToString();
					txtMasLoc.Tag = drvResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString();
					ChangeMasLoc();
				}
				else
					e.Cancel = true;
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

		private void txtMasLoc_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnMasLoc_Click(null, null);
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

		private void btnMasLoc_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMasLoc_Click()";
			try
			{
				Hashtable htCondition = new Hashtable();
				htCondition.Add(PO_ReturnToVendorMasterTable.CCNID_FLD, Convert.ToInt32(cboCCN.SelectedValue));
				DataRowView drvResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text.Trim(), htCondition, true);
				if (drvResult != null)
				{
					txtMasLoc.Text = drvResult[MST_MasterLocationTable.CODE_FLD].ToString();
					txtMasLoc.Tag = drvResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString();
					ChangeMasLoc();
				}
				else
					txtMasLoc.Focus();
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

		private void cboReturnBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboReturnBy_SelectedIndexChanged()";
			try
			{
				if (enumAction == EnumAction.Add)
				{
					txtRefNo.Text = string.Empty;
					txtRefNo.Tag = null;
					txtVendorCode.Text = string.Empty;
					txtVendorCode.Tag = null;
					txtVendorName.Text = string.Empty;
					txtVendorLoc.Text = string.Empty;
					txtVendorLoc.Tag = null;

					// clear data set
					if (dstData != null && dstData.Tables.Count > 0)
						dstData.Tables[0].Clear();
					if (cboReturnBy.SelectedIndex == 0)
					{
						txtProductionLine.Enabled = true;
						btnProductionLine.Enabled = true;
					}
					else
					{
						txtProductionLine.Text = string.Empty;
						txtProductionLine.Tag = null;
						txtProductionLine.Enabled = false;
						btnProductionLine.Enabled = false;
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
		private void txtRefNo_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtRefNo_Validating()";
			try
			{
				if (txtRefNo.Modified)
				{
					if (txtRefNo.Text.Trim() == string.Empty)
					{
						txtRefNo.Tag = null;
						txtVendorCode.Text = string.Empty;
						txtVendorCode.Tag = null;
						txtVendorName.Text = string.Empty;
						txtVendorLoc.Text = string.Empty;
						txtVendorLoc.Tag = null;
						if (dstData != null && dstData.Tables.Count > 0)
							dstData.Tables[0].Clear();
						return;
					}
					string strTableName = string.Empty, strFilterField = string.Empty, strFilterValue = txtRefNo.Text.Trim();
					if (cboReturnBy.SelectedIndex == 0) // return by purchase order
					{
						strTableName = V_PORETURNTOVENDOR;
						strFilterField = PO_PurchaseOrderMasterTable.CODE_FLD;
					}
					else // return by invoice
					{
						strTableName = V_INVOICETORETURN;
						strFilterField = PO_InvoiceMasterTable.INVOICENO_FLD;
					}
					DataRowView drvResult = FormControlComponents.OpenSearchForm(strTableName, strFilterField, strFilterValue, null, false);
					if (drvResult != null)
					{
						if (cboReturnBy.SelectedIndex == 0)
							ReturnByPO(drvResult.Row);
						else
							ReturnByInvoice(drvResult.Row);
					}
					else
						e.Cancel = true;
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

		private void txtRefNo_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtRefNo_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnRefNo_Click(null, null);
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

		private void btnRefNo_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnRefNo_Click()";
			try
			{
				string strTableName = string.Empty, strFilterField = string.Empty, strFilterValue = txtRefNo.Text.Trim();
				if (cboReturnBy.SelectedIndex == 0) // return by purchase order
				{
					strTableName = V_PORETURNTOVENDOR;
					strFilterField = PO_PurchaseOrderMasterTable.CODE_FLD;
				}
				else // return by invoice
				{
					strTableName = V_INVOICETORETURN;
					strFilterField = PO_InvoiceMasterTable.INVOICENO_FLD;
				}
				DataRowView drvResult = FormControlComponents.OpenSearchForm(strTableName, strFilterField, strFilterValue, null, true);
				if (drvResult != null)
				{
					if (cboReturnBy.SelectedIndex == 0)
						ReturnByPO(drvResult.Row);
					else
						ReturnByInvoice(drvResult.Row);
				}
				else
					txtRefNo.Focus();
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

		private void txtProductionLine_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";
			try
			{
				if (!txtProductionLine.Modified) return;
				if (txtProductionLine.Text.Trim() == string.Empty)
				{
					txtProductionLine.Tag = null;
					return;
				}
				DataRowView drvResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, false);
				if (drvResult != null)
				{
					txtProductionLine.Text = drvResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = drvResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
				}
				else
					e.Cancel = true;
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

		private void txtProductionLine_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnProductionLine_Click(null, null);
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

		private void btnProductionLine_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
			try
			{
				DataRowView drvResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, true);
				if (drvResult != null)
				{
					txtProductionLine.Text = drvResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = drvResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
				}
				else
					txtProductionLine.Focus();
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

		private void txtVendorCode_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendorCode_Validating()";
			try
			{
				if (!txtVendorCode.Modified) return;
				if (txtVendorCode.Text.Trim() == string.Empty)
				{
					txtVendorCode.Tag = null;
					txtVendorName.Text = string.Empty;
					txtVendorLoc.Text = string.Empty;
					txtVendorLoc.Tag = null;
					return;
				}
				DataRowView drvResult = FormControlComponents.OpenSearchForm(MST_PartyTable.TABLE_NAME, MST_PartyTable.CODE_FLD, txtVendorCode.Text.Trim(), null, false);
				if (drvResult != null)
				{
					txtVendorCode.Text = drvResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendorCode.Tag = drvResult[MST_PartyTable.PARTYID_FLD];
					txtVendorName.Text = drvResult[MST_PartyTable.NAME_FLD].ToString();
					txtVendorLoc.Text = string.Empty;
					txtVendorLoc.Tag = null;
				}
				else
					e.Cancel = true;
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

		private void txtVendorCode_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendorCode_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnVendorCode_Click(null, null);
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

		private void btnVendorCode_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendorCode_Click()";
			try
			{
				DataRowView drvResult = FormControlComponents.OpenSearchForm(MST_PartyTable.TABLE_NAME, MST_PartyTable.CODE_FLD, txtVendorCode.Text.Trim(), null, true);
				if (drvResult != null)
				{
					txtVendorCode.Text = drvResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendorCode.Tag = drvResult[MST_PartyTable.PARTYID_FLD];
					txtVendorName.Text = drvResult[MST_PartyTable.NAME_FLD].ToString();
					txtVendorLoc.Text = string.Empty;
					txtVendorLoc.Tag = null;
				}
				else
					txtVendorCode.Focus();
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

		private void txtVendorLoc_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendorLoc_Validating()";
			try
			{
				if (!txtVendorLoc.Modified) return;
				if (txtVendorLoc.Text.Trim() == string.Empty)
				{
					txtVendorLoc.Tag = null;
					return;
				}
				DataRowView drvResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtVendorLoc.Text.Trim(), null, false);
				if (drvResult != null)
				{
					txtVendorLoc.Text = drvResult[MST_PartyLocationTable.CODE_FLD].ToString();
					txtVendorLoc.Tag = drvResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
				}
				else
					e.Cancel = true;
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

		private void txtVendorLoc_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendorLoc_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnVendorLoc_Click(null, null);
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

		private void btnVendorLoc_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendorLoc_Click()";
			try
			{
				DataRowView drvResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtVendorLoc.Text.Trim(), null, true);
				if (drvResult != null)
				{
					txtVendorLoc.Text = drvResult[MST_PartyLocationTable.CODE_FLD].ToString();
					txtVendorLoc.Tag = drvResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
				}
				else
					txtVendorLoc.Focus();
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

		private void dgrdData_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.F4:
						if (btnSave.Enabled)
							dgrdData_ButtonClick(sender, null);
						break;
					case Keys.Delete:
						if ((e.KeyCode == Keys.Delete)&&(dgrdData.SelectedRows.Count > 0))
						{
							if (btnSave.Enabled)
							{
								dgrdData.AllowDelete = true;
								FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
								int intCount  = 0;
								foreach (DataRow objRow in dstData.Tables[0].Rows)
								{
									if(objRow.RowState != DataRowState.Deleted) 
										intCount++;
								}
								for (int i =0; i <intCount; i++)
									dgrdData[i, PO_PurchaseOrderDetailTable.LINE_FLD] = i+1;
							}
						}
						break;
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

		private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				switch (e.Column.DataColumn.DataField)
				{
					case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD:
					case ITM_ProductTable.DESCRIPTION_FLD:
						if ((e.Column.DataColumn.Tag == null) ||( e.Column.DataColumn.Value.ToString() == string.Empty))
						{
							dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = string.Empty;
							dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = string.Empty;
							dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
							dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = string.Empty;
							dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.STOCKUMID_FLD] = string.Empty;
						}
						else
						{
							DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
							dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = drvResult[ITM_ProductTable.CODE_FLD];
							dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = drvResult[ITM_ProductTable.PRODUCTID_FLD];
							dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
							dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = drvResult[ITM_ProductTable.REVISION_FLD];
							dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.STOCKUMID_FLD] = drvResult[ITM_ProductTable.STOCKUMID_FLD];
						}
						dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.BUYINGUMID_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = string.Empty;
						break;
					case MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD:
						dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = String.Empty;
						dgrdData[dgrdData.Row, MST_BINTable.BINID_FLD] = String.Empty;
						if ((e.Column.DataColumn.Tag == null) ||( e.Column.DataColumn.Value.ToString() == string.Empty))
							dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD] = String.Empty;
						else
						{
							DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
							dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = drvResult[MST_LocationTable.CODE_FLD];
							dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD] = drvResult[MST_LocationTable.LOCATIONID_FLD];
						}
						break;
					case MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD:
						if ((e.Column.DataColumn.Tag == null) ||( e.Column.DataColumn.Value.ToString() == string.Empty))
							dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.BINID_FLD] = String.Empty;
						else
						{
							DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
							dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = drvResult[MST_BINTable.CODE_FLD];
							dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.BINID_FLD] = drvResult[MST_BINTable.BINID_FLD];
						}
						break;
					case MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD:
						decimal dcmUMRate = 0;
						int intBuyingUnitID;
						int intStockID;
						if ((e.Column.DataColumn.Tag == null) ||( e.Column.DataColumn.Value.ToString() == string.Empty))
						{
							dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = String.Empty;
							dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.UMRATE_FLD] = String.Empty;
						}
						else
						{
							DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
								
							dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.BUYINGUMID_FLD] = drvResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD];
							dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drvResult[MST_UnitOfMeasureTable.CODE_FLD];
							intStockID = int.Parse(dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.STOCKUMID_FLD].ToString());
							intBuyingUnitID = int.Parse(drvResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD].ToString());
							if ( intBuyingUnitID != intStockID)
							{
								dcmUMRate = FormControlComponents.GetUMRate(intBuyingUnitID,intStockID);
								dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.UMRATE_FLD] = dcmUMRate;
							}
							else
								dgrdData[dgrdData.Row, PO_ReturnToVendorDetailTable.UMRATE_FLD] = decimal.One;
						}
						break;
					case PO_ReturnToVendorDetailTable.QUANTITY_FLD:
					case PO_ReturnToVendorDetailTable.UNITPRICE_FLD:
					case PO_ReturnToVendorDetailTable.VATPERCENT_FLD:
						CalculateAmount();
						break;
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

		private void dgrdData_AfterDelete(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterDelete()";
			try
			{
				for (int i = 0; i < dgrdData.RowCount; i++)
					dgrdData[i, PO_PurchaseOrderDetailTable.LINE_FLD] = i + 1;
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

		private void dgrdData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				DataRowView drvResult = null;
				string strColumnName = e.Column.DataColumn.DataField;
				string strColumValue = e.Column.DataColumn.Text.Trim();
				Hashtable htbCondition;
				string strProductID;
				switch (strColumnName)
				{
					case ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD:
						if (strColumValue != string.Empty)
						{
							drvResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, strColumValue, null, false);
							if (drvResult != null)
							{
								// check for existing product in grid
								if (IsExisted(Convert.ToInt32(drvResult[ITM_ProductTable.PRODUCTID_FLD]), dgrdData.Row))
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_DUPLICATEPRO, MessageBoxIcon.Warning);
									e.Cancel = true;
								}
								else
									e.Column.DataColumn.Tag = drvResult;
							}
							else
								e.Cancel = true;
						}
						break;
					case ITM_ProductTable.DESCRIPTION_FLD:
						if (strColumValue != string.Empty)
						{
							drvResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, strColumValue, null, false);
							if (drvResult != null)
							{
								// check for existing product in grid
								if (IsExisted(Convert.ToInt32(drvResult[ITM_ProductTable.PRODUCTID_FLD]), dgrdData.Row))
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_DUPLICATEPRO, MessageBoxIcon.Warning);
									e.Cancel = true;
								}
								else
									e.Column.DataColumn.Tag = drvResult;
							}
							else
								e.Cancel = true;
						}
						break;
					case MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD:
						if (txtMasLoc.Tag == null)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTMASLOC,MessageBoxIcon.Warning);
							e.Cancel = true;
						}
						if (strColumValue != string.Empty)
						{
							htbCondition = new Hashtable();
							htbCondition.Add(MST_LocationTable.MASTERLOCATIONID_FLD, Convert.ToInt32(txtMasLoc.Tag));
							drvResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, strColumValue, htbCondition, false);
							if (drvResult != null)
								e.Column.DataColumn.Tag = drvResult;
							else
								e.Cancel = true;
						}
						break;
					case MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD:
						string strLocationID = string.Empty;
						try
						{
							strLocationID = dgrdData[dgrdData.Row,PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString().Trim();
						}
						catch{}
						if (strLocationID == string.Empty)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Warning);
							e.Cancel = true;
						}
						if (strColumValue != string.Empty)
						{
							htbCondition = new Hashtable();
							htbCondition.Add(MST_LocationTable.LOCATIONID_FLD, strLocationID);
							drvResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, strColumValue, htbCondition, false);
							if (drvResult != null)
								e.Column.DataColumn.Tag = drvResult;
							else
								e.Cancel = true;
						}
						break;
					case PO_ReturnToVendorDetailTable.QUANTITY_FLD:
						if (e.Column.DataColumn.Value != null &&
							e.Column.DataColumn.Value.ToString().Trim() != string.Empty)
						{
							decimal decQuantity = 0;
							try
							{
								decQuantity = Convert.ToDecimal(e.Column.DataColumn.Value);
								if (decQuantity < 0)
									throw new Exception();
							}
							catch
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_QUANTITY_HIGHER_0, MessageBoxIcon.Warning);
								e.Cancel = true;
							}
							e.Column.DataColumn.Tag = e.Column.DataColumn.Value;
							if (cboReturnBy.SelectedIndex == 0 && txtRefNo.Tag != null)
							{
								//First get the product Id for this row
								strProductID = dgrdData[dgrdData.Row,PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString();
								
								//get the total commit quantity
								decimal dcmTotalCommit = 0;

								//find the Product ID in the commit Sale order 
								DataRow[] drow = dtbReceived.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID);
								//get the total commit quantity

								if (drow.Length == 0)
									dcmTotalCommit = 0;
							
								try
								{
									//get the total quantity remain after reach return to vendor
									dcmTotalCommit = decimal.Parse( drow[0][TOTAL_REMAIN_FLD].ToString());
								}
								catch
								{}
								if (drow.Length > 0)
								{
									if (decQuantity > dcmTotalCommit)
									{
										PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_RETURNQTY,MessageBoxIcon.Warning);
										e.Cancel = true;
									}
								}
							}
						}
						break;
					case PO_ReturnToVendorDetailTable.UNITPRICE_FLD:
					case PO_ReturnToVendorDetailTable.VATPERCENT_FLD:
						if (e.Column.DataColumn.Value != null && e.Column.DataColumn.Value.ToString().Trim() != string.Empty)
						{
							try
							{
								if (Convert.ToDecimal(e.Column.DataColumn.Value) < 0)
									throw new Exception();
							}
							catch
							{
								e.Cancel = true;
							}
						}
						break;
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

		private void dgrdData_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeDelete()";
			try
			{
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					e.Cancel = false;
				else
					e.Cancel = true;
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

		private void dgrdData_ButtonClick(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				if (!dgrdData.AllowAddNew && !dgrdData.AllowUpdate)
					return;
				string strColumnName = dgrdData.Columns[dgrdData.Col].DataField;
				DataRowView drvResult = null;
				Hashtable htbCondition = null;
				switch (strColumnName)
				{
					case MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD:
						if (txtMasLoc.Tag == null)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTMASLOC,MessageBoxIcon.Warning);
							return;
						}
						htbCondition = new Hashtable();
						htbCondition.Add(MST_LocationTable.MASTERLOCATIONID_FLD, Convert.ToInt32(txtMasLoc.Tag));
						if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
							drvResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString(), htbCondition, true);
						else
							drvResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Text.Trim(), htbCondition, true);
						if (drvResult != null)
						{
							dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = drvResult[MST_LocationTable.CODE_FLD];
							dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD] = drvResult[MST_LocationTable.LOCATIONID_FLD];
							// clear bin
							dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = string.Empty;
							dgrdData[dgrdData.Row, MST_BINTable.BINID_FLD] = string.Empty;
						}
						else
						{
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
							dgrdData.Focus();
						}
						break;
					case MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD:
						string strLocationID = dgrdData[dgrdData.Row,PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString().Trim();
						if (strLocationID == string.Empty)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Warning);
							return;
						}
						htbCondition = new Hashtable();
						htbCondition.Add(MST_LocationTable.LOCATIONID_FLD, strLocationID);
						if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
							drvResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].ToString(), htbCondition, true);
						else
							drvResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Text.Trim(), htbCondition, true);
						if (drvResult != null)
						{
							dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = drvResult[MST_BINTable.CODE_FLD];
							dgrdData[dgrdData.Row, MST_BINTable.BINID_FLD] = drvResult[MST_BINTable.BINID_FLD];
						}
						else
						{
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD]);
							dgrdData.Focus();
						}
						break;
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

		private void btnAdd_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				enumAction = EnumAction.Add;
				ResetForm();
				voReturnMaster = new PO_ReturnToVendorMasterVO();
				dtmPostDate.Value = new UtilsBO().GetDBDate();
				txtReturnNo.Text = FormControlComponents.GetNoByMask(this);
				FormControlComponents.SetDefaultMasterLocation(txtMasLoc);
				SwitchFormMode();
				dtmPostDate.Focus();
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

		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIRM_BEFORE_SAVE_DATA, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					if (!dgrdData.EditActive)
					{
						if(Security.IsDifferencePrefix(this, lblReturnNo, txtReturnNo))
							return;
					}
					if (!ValidateMandatoryControl())
						return;
					if (!CheckGrid())
						return;

					if (SaveData())
					{
						LoadReturnData(voReturnMaster.ReturnToVendorMasterID);

						enumAction = EnumAction.Default;

						Security.UpdateUserNameModifyTransaction(this, PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD, voReturnMaster.ReturnToVendorMasterID);
						PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
					
						SwitchFormMode();
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

		private void btnDelete_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if(PCSMessageBox.Show(ErrorCode.YES_NO_MESSAGE,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
				{
					boReturn.DeleteReturnToVendor(voReturnMaster.ReturnToVendorMasterID);
					
					ResetForm();

					if (dstData != null && dstData.Tables.Count > 0)
					{
						dstData.Tables[0].Clear();
						dstData.AcceptChanges();
					}
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
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

		private void btnPrint_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPrint_Click()";
			try
			{
				if(voReturnMaster == null || voReturnMaster.ReturnToVendorMasterID <= 0)
					return;
				ShowPOSlipReport();
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

		private void btnHelp_Click(object sender, EventArgs e)
		{
			
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void POReturnToVendor_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".POReturnToVendor_Closing()";
			try
			{
				if (enumAction == EnumAction.Add)
				{
					DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					switch (dlgResult)
					{
						case DialogResult.Yes:
							//Save before exit
							try
							{
								if (!ValidateMandatoryControl())
								{
									e.Cancel = true;
									break;
								}
								if (!CheckGrid())
								{
									e.Cancel = true;
									break;
								}
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
	}
}
