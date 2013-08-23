using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Common.DS;
using PCSComSale.Order.BO;
using PCSComSale.Order.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSSale.Order
{
	/// <summary>
	/// Summary description for SOCancelCommitment.
	/// </summary>
	public class SOCancelCommitment : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblLable3;
		private System.Windows.Forms.Label lblLable7;
		private System.Windows.Forms.Label lblLable12;
		private System.Windows.Forms.Label lblLable11;
		private System.Windows.Forms.Label lblLable1;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.CheckBox chkSelectAll;
		private System.Windows.Forms.TextBox txtSaleOrderCode;
		private System.Windows.Forms.Button btnFindSaleOrder;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		DataTable dtbGridLayout;

		string	CaptionLine, CaptionDelivery, CaptionMasterLocation, CaptionCommitQuantity, CaptionUnitOfMeasure, 
				CaptionLocation, CaptionBin, CaptionProductCode, CaptionProductDes, CaptionCancel;
		const string DELIVERY = "Delivery";
		const string UNITOFMEASURE = "Measure";
		const string MASTERLOCATION = "MasterLocation";
		const string LOCATION = "Location";
		const string BIN = "Bin";
		const string PRODUCTCODE = "ProductCode";
		const string PRODUCTDES = "ProductDes";
		const string CANCEL = "Cancel";
		const string TRUE = "True";

		private const string THIS = "PCSSale.Order.SOCancelCommitment";
		private bool blnStateOfCheck = false;

		public EnumAction enumAction = EnumAction.Default;
		private bool blnHasError = false;
		
		private DataSet dstCancelCommit = new DataSet();
		private System.Windows.Forms.Button btnCancelCommitment;
		private System.Windows.Forms.Button btnSearch;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private SOCancelCommitmentBO boCancelCommitment = new SOCancelCommitmentBO();
		private System.Windows.Forms.TextBox txtBuyingLoc;
		private System.Windows.Forms.TextBox txtCustomer;
		private System.Windows.Forms.TextBox txtCustomerName;
		private SO_SaleOrderMasterVO voSOMaster = new SO_SaleOrderMasterVO();

		public SOCancelCommitment()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SOCancelCommitment));
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnCancelCommitment = new System.Windows.Forms.Button();
			this.lblLable12 = new System.Windows.Forms.Label();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblLable11 = new System.Windows.Forms.Label();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.lblLable3 = new System.Windows.Forms.Label();
			this.txtSaleOrderCode = new System.Windows.Forms.TextBox();
			this.lblLable7 = new System.Windows.Forms.Label();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnFindSaleOrder = new System.Windows.Forms.Button();
			this.lblLable1 = new System.Windows.Forms.Label();
			this.btnSearch = new System.Windows.Forms.Button();
			this.txtBuyingLoc = new System.Windows.Forms.TextBox();
			this.txtCustomer = new System.Windows.Forms.TextBox();
			this.txtCustomerName = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.AccessibleDescription = "";
			this.btnClose.AccessibleName = "";
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(657, 422);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(65, 23);
			this.btnClose.TabIndex = 17;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.AccessibleDescription = "";
			this.btnHelp.AccessibleName = "";
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(591, 422);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(65, 23);
			this.btnHelp.TabIndex = 16;
			this.btnHelp.Text = "&Help";
			// 
			// btnCancelCommitment
			// 
			this.btnCancelCommitment.AccessibleDescription = "";
			this.btnCancelCommitment.AccessibleName = "";
			this.btnCancelCommitment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancelCommitment.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancelCommitment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCancelCommitment.Location = new System.Drawing.Point(6, 422);
			this.btnCancelCommitment.Name = "btnCancelCommitment";
			this.btnCancelCommitment.Size = new System.Drawing.Size(110, 23);
			this.btnCancelCommitment.TabIndex = 14;
			this.btnCancelCommitment.Text = "Cance&l Commitment";
			this.btnCancelCommitment.Click += new System.EventHandler(this.btnCancelCommitment_Click);
			// 
			// lblLable12
			// 
			this.lblLable12.AccessibleDescription = "";
			this.lblLable12.AccessibleName = "";
			this.lblLable12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblLable12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblLable12.ForeColor = System.Drawing.Color.Maroon;
			this.lblLable12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLable12.Location = new System.Drawing.Point(602, 8);
			this.lblLable12.Name = "lblLable12";
			this.lblLable12.Size = new System.Drawing.Size(32, 20);
			this.lblLable12.TabIndex = 0;
			this.lblLable12.Text = "CCN";
			this.lblLable12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboCCN
			// 
			this.cboCCN.AccessibleDescription = "";
			this.cboCCN.AccessibleName = "";
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
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(634, 6);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(88, 21);
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
			// lblLable11
			// 
			this.lblLable11.AccessibleDescription = "";
			this.lblLable11.AccessibleName = "";
			this.lblLable11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLable11.Location = new System.Drawing.Point(10, 28);
			this.lblLable11.Name = "lblLable11";
			this.lblLable11.Size = new System.Drawing.Size(87, 20);
			this.lblLable11.TabIndex = 5;
			this.lblLable11.Text = "Customer";
			this.lblLable11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.AccessibleDescription = "";
			this.chkSelectAll.AccessibleName = "";
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkSelectAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkSelectAll.Location = new System.Drawing.Point(120, 424);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(66, 20);
			this.chkSelectAll.TabIndex = 15;
			this.chkSelectAll.Text = "Select &All";
			this.chkSelectAll.Enter += new System.EventHandler(this.chkSelectAll_Enter);
			this.chkSelectAll.Leave += new System.EventHandler(this.chkSelectAll_Leave);
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// lblLable3
			// 
			this.lblLable3.AccessibleDescription = "";
			this.lblLable3.AccessibleName = "";
			this.lblLable3.ForeColor = System.Drawing.Color.Maroon;
			this.lblLable3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLable3.Location = new System.Drawing.Point(10, 6);
			this.lblLable3.Name = "lblLable3";
			this.lblLable3.Size = new System.Drawing.Size(87, 20);
			this.lblLable3.TabIndex = 2;
			this.lblLable3.Text = "Sale Order";
			this.lblLable3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtSaleOrderCode
			// 
			this.txtSaleOrderCode.AccessibleDescription = "";
			this.txtSaleOrderCode.AccessibleName = "";
			this.txtSaleOrderCode.Location = new System.Drawing.Point(91, 6);
			this.txtSaleOrderCode.Name = "txtSaleOrderCode";
			this.txtSaleOrderCode.Size = new System.Drawing.Size(139, 20);
			this.txtSaleOrderCode.TabIndex = 3;
			this.txtSaleOrderCode.Text = "Sale Order";
			this.txtSaleOrderCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSaleOrderCode_KeyDown);
			this.txtSaleOrderCode.Leave += new System.EventHandler(this.txtSaleOrderCode_Leave);
			this.txtSaleOrderCode.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblLable7
			// 
			this.lblLable7.AccessibleDescription = "";
			this.lblLable7.AccessibleName = "";
			this.lblLable7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLable7.Location = new System.Drawing.Point(10, 76);
			this.lblLable7.Name = "lblLable7";
			this.lblLable7.Size = new System.Drawing.Size(87, 18);
			this.lblLable7.TabIndex = 7;
			this.lblLable7.Text = "Buying Loc.";
			this.lblLable7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = "";
			this.dgrdData.AccessibleName = "";
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(6, 100);
			this.dgrdData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdData.Name = "dgrdData";
			this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.dgrdData.PreviewInfo.ZoomFactor = 75;
			this.dgrdData.PrintInfo.ShowOptionsDialog = false;
			this.dgrdData.RecordSelectorWidth = 16;
			this.dgrdData.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.dgrdData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.dgrdData.RowHeight = 15;
			this.dgrdData.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.dgrdData.Size = new System.Drawing.Size(716, 316);
			this.dgrdData.TabIndex = 13;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColEdit);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Sale Order " +
				"No.\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Del. Line\" DataField=\"Delivery\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Master Location\" DataField=\"MasterLo" +
				"cation\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Captio" +
				"n=\"Committed Qty\" DataField=\"CommitQuantity\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"Unit\" DataField=\"Measure\"><ValueItems />" +
				"<GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Location\" DataField" +
				"=\"Location\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Bin\" DataField=\"Bin\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCol" +
				"umn Level=\"0\" Caption=\"Part Number\" DataField=\"ProductCode\"><ValueItems /><Group" +
				"Info /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Part Name\" DataField=\"Pro" +
				"ductDes\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capti" +
				"on=\"Cancel\" DataField=\"Cancel\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
				"Column Level=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueItems /><GroupInfo /" +
				"></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Schedule Date\" DataField=\"Sched" +
				"uleDate\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.W" +
				"in.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightTex" +
				"t;BackColor:Highlight;}Inactive{ForeColor:InactiveCaptionText;BackColor:Inactive" +
				"Caption;}Style78{}Style79{}Style85{}Editor{}Style72{}Style73{}Style70{AlignHorz:" +
				"Near;}Style71{AlignHorz:Near;}Style76{AlignHorz:Near;}Style77{AlignHorz:Near;}St" +
				"yle74{}Style75{}Style84{}Style87{}Style86{}Style81{}Style80{}Style83{AlignHorz:N" +
				"ear;}Style82{AlignHorz:Near;}FilterBar{}Heading{Wrap:True;BackColor:Control;Bord" +
				"er:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style18{}Style19{A" +
				"lignHorz:Far;}Style14{}Style15{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;}" +
				"Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Selected{ForeColor:HighlightT" +
				"ext;BackColor:Highlight;}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}Style27" +
				"{}Style25{}Style22{AlignHorz:Near;}Style9{}Style8{}Style24{}Style26{}Style5{}Sty" +
				"le4{}Style7{}Style6{}Style1{}Style23{AlignHorz:Near;}Style3{}Style2{}Style21{}St" +
				"yle20{}OddRow{}Style38{}Style39{}Style36{}Style37{}Style34{AlignHorz:Near;}Style" +
				"35{AlignHorz:Near;}Style32{}Style33{}Style30{}Style49{}Style48{}Style31{}Normal{" +
				"}Style41{AlignHorz:Near;}Style40{AlignHorz:Near;}Style43{AlignHorz:Far;}Style42{" +
				"}Style45{}Style44{}Style47{AlignHorz:Near;}Style46{AlignHorz:Near;}EvenRow{BackC" +
				"olor:Aqua;}Style59{AlignHorz:Near;}Style58{AlignHorz:Near;}RecordSelector{AlignI" +
				"mage:Center;}Style51{}Style50{}Footer{}Style52{AlignHorz:Near;}Style53{AlignHorz" +
				":Near;}Style54{}Style55{}Style56{}Style57{}Caption{AlignHorz:Center;}Style69{}St" +
				"yle68{}Style63{}Style62{}Style61{}Style60{}Style67{}Style66{}Style65{AlignHorz:N" +
				"ear;}Style64{AlignHorz:Near;}Group{AlignVert:Center;Border:None,,0, 0, 0, 0;Back" +
				"Color:ControlDark;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"" +
				"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" MarqueeSty" +
				"le=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrol" +
				"lGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 712, 312</ClientRect><Bor" +
				"derSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle " +
				"parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><Filt" +
				"erBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"St" +
				"yle3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\"" +
				" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveS" +
				"tyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" />" +
				"<RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle paren" +
				"t=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C" +
				"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style" +
				"1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle paren" +
				"t=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><Grou" +
				"pFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivide" +
				"r>DarkGray,Single</ColumnDivider><Width>112</Width><Height>15</Height><DCIdx>0</" +
				"DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style" +
				"22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
				"le24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"St" +
				"yle1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>T" +
				"rue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>54</Width><Hei" +
				"ght>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style2\" me=\"Style82\" /><Style parent=\"Style1\" me=\"Style83\" /><FooterStyl" +
				"e parent=\"Style3\" me=\"Style84\" /><EditorStyle parent=\"Style5\" me=\"Style85\" /><Gr" +
				"oupHeaderStyle parent=\"Style1\" me=\"Style87\" /><GroupFooterStyle parent=\"Style1\" " +
				"me=\"Style86\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivi" +
				"der><Width>93</Width><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1Di" +
				"splayColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" " +
				"me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"" +
				"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFo" +
				"oterStyle parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>D" +
				"arkGray,Single</ColumnDivider><Width>81</Width><Height>15</Height><DCIdx>3</DCId" +
				"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" " +
				"/><Style parent=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66" +
				"\" /><EditorStyle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1" +
				"\" me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True<" +
				"/Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>120</Width><Height" +
				">15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=" +
				"\"Style74\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider" +
				"><Width>149</Width><Height>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=" +
				"\"Style77\" /><FooterStyle parent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Sty" +
				"le5\" me=\"Style79\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFoote" +
				"rStyle parent=\"Style1\" me=\"Style80\" /><Visible>True</Visible><ColumnDivider>Dark" +
				"Gray,Single</ColumnDivider><Width>70</Width><Height>15</Height><DCIdx>10</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" />" +
				"<Style parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" " +
				"/><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" " +
				"me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</V" +
				"isible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>49</Width><Height>15" +
				"</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent" +
				"=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle pare" +
				"nt=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHea" +
				"derStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"St" +
				"yle32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><W" +
				"idth>87</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayCo" +
				"lumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Sty" +
				"le53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\"" +
				" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterSty" +
				"le parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnDivider>DarkGray" +
				",Single</ColumnDivider><Width>85</Width><Height>15</Height><DCIdx>5</DCIdx></C1D" +
				"isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Styl" +
				"e parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><Ed" +
				"itorStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"S" +
				"tyle63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visibl" +
				"e><ColumnDivider>DarkGray,Single</ColumnDivider><Width>94</Width><Height>15</Hei" +
				"ght><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
				"le2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"S" +
				"tyle3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderSt" +
				"yle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38" +
				"\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>" +
				"51</Width><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn></internalCols></" +
				"C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\"" +
				" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><" +
				"Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><S" +
				"tyle parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style" +
				" parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Styl" +
				"e parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><S" +
				"tyle parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></Nam" +
				"edStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</L" +
				"ayout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 712, 312</Cli" +
				"entArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle par" +
				"ent=\"\" me=\"Style15\" /></Blob>";
			// 
			// btnFindSaleOrder
			// 
			this.btnFindSaleOrder.AccessibleDescription = "";
			this.btnFindSaleOrder.AccessibleName = "";
			this.btnFindSaleOrder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFindSaleOrder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnFindSaleOrder.Location = new System.Drawing.Point(232, 6);
			this.btnFindSaleOrder.Name = "btnFindSaleOrder";
			this.btnFindSaleOrder.Size = new System.Drawing.Size(24, 20);
			this.btnFindSaleOrder.TabIndex = 4;
			this.btnFindSaleOrder.Text = "...";
			this.btnFindSaleOrder.Click += new System.EventHandler(this.btnFindSaleOrder_Click);
			// 
			// lblLable1
			// 
			this.lblLable1.AccessibleDescription = "";
			this.lblLable1.AccessibleName = "";
			this.lblLable1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLable1.Location = new System.Drawing.Point(10, 50);
			this.lblLable1.Name = "lblLable1";
			this.lblLable1.Size = new System.Drawing.Size(87, 20);
			this.lblLable1.TabIndex = 9;
			this.lblLable1.Text = "Customer Name";
			this.lblLable1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSearch
			// 
			this.btnSearch.AccessibleDescription = "";
			this.btnSearch.AccessibleName = "";
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearch.Location = new System.Drawing.Point(646, 71);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.TabIndex = 12;
			this.btnSearch.Text = "&Search";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// txtBuyingLoc
			// 
			this.txtBuyingLoc.Location = new System.Drawing.Point(91, 74);
			this.txtBuyingLoc.Name = "txtBuyingLoc";
			this.txtBuyingLoc.ReadOnly = true;
			this.txtBuyingLoc.Size = new System.Drawing.Size(101, 20);
			this.txtBuyingLoc.TabIndex = 8;
			this.txtBuyingLoc.TabStop = false;
			this.txtBuyingLoc.Text = "";
			// 
			// txtCustomer
			// 
			this.txtCustomer.Location = new System.Drawing.Point(91, 28);
			this.txtCustomer.Name = "txtCustomer";
			this.txtCustomer.ReadOnly = true;
			this.txtCustomer.Size = new System.Drawing.Size(101, 20);
			this.txtCustomer.TabIndex = 6;
			this.txtCustomer.TabStop = false;
			this.txtCustomer.Text = "";
			// 
			// txtCustomerName
			// 
			this.txtCustomerName.Location = new System.Drawing.Point(91, 51);
			this.txtCustomerName.Name = "txtCustomerName";
			this.txtCustomerName.ReadOnly = true;
			this.txtCustomerName.Size = new System.Drawing.Size(252, 20);
			this.txtCustomerName.TabIndex = 10;
			this.txtCustomerName.TabStop = false;
			this.txtCustomerName.Text = "";
			// 
			// SOCancelCommitment
			// 
			this.AccessibleDescription = "";
			this.AccessibleName = "";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(726, 451);
			this.Controls.Add(this.txtCustomerName);
			this.Controls.Add(this.txtCustomer);
			this.Controls.Add(this.txtBuyingLoc);
			this.Controls.Add(this.txtSaleOrderCode);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.btnFindSaleOrder);
			this.Controls.Add(this.chkSelectAll);
			this.Controls.Add(this.lblLable7);
			this.Controls.Add(this.lblLable1);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnCancelCommitment);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.lblLable11);
			this.Controls.Add(this.lblLable3);
			this.Controls.Add(this.lblLable12);
			this.Controls.Add(this.cboCCN);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Name = "SOCancelCommitment";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Cancel Sales Order Commitment";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SOCancelCommitment_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.SOCancelCommitment_Closing);
			this.Load += new System.EventHandler(this.SOCancelCommitment_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		//**************************************************************************              
		///    <Description>
		///       Get current column's headers on the grid
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, March 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void GetCaptionOnGrid()
		{
			CaptionLine = dgrdData.Splits[0].DisplayColumns[SO_CommitInventoryDetailTable.LINE_FLD].DataColumn.Caption;
			CaptionDelivery = dgrdData.Splits[0].DisplayColumns[DELIVERY].DataColumn.Caption;
			CaptionMasterLocation = dgrdData.Splits[0].DisplayColumns[MASTERLOCATION].DataColumn.Caption;
			CaptionCommitQuantity = dgrdData.Splits[0].DisplayColumns[SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD].DataColumn.Caption;
			CaptionUnitOfMeasure = dgrdData.Splits[0].DisplayColumns[UNITOFMEASURE].DataColumn.Caption;
			CaptionLocation = dgrdData.Splits[0].DisplayColumns[LOCATION].DataColumn.Caption;
			CaptionBin = dgrdData.Splits[0].DisplayColumns[BIN].DataColumn.Caption;
			CaptionProductCode = dgrdData.Splits[0].DisplayColumns[PRODUCTCODE].DataColumn.Caption;
			CaptionProductDes = dgrdData.Splits[0].DisplayColumns[PRODUCTDES].DataColumn.Caption;
			CaptionCancel = dgrdData.Splits[0].DisplayColumns[CANCEL].DataColumn.Caption;
			for(int i =0; i<dgrdData.Splits[0].DisplayColumns.Count; i++)
			{
				dgrdData.Splits[0].DisplayColumns[i].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       Reset all control and set focus into txtSaleOrderCode
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, March 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ResetForm()
		{
			txtSaleOrderCode.Text = string.Empty;
			txtCustomer.Text = string.Empty;
			txtCustomerName.Text = string.Empty;
			txtBuyingLoc.Text = string.Empty;

			chkSelectAll.Checked = false;
			txtSaleOrderCode.Focus();

			dgrdData.Splits[0].Rows.Clear();
		}


		//**************************************************************************              
		///    <Description>
		///       Load all data in to Combo CCN 
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Friday, February 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void LoadComboCCN()
		{
			try
			{
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedIndex = 0;
				}
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


		//**************************************************************************              
		///    <Description>
		///       Execute searching items to display on the grid by Code or by ID
		///    </Description>
		///    <Inputs>
		///		 pstrSaleOrderCode is Code of SaleOrder
		///		 pintSOID is ID of SaleOrder
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Friday, February 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void BindDataToGrid(int pintSOID)
		{
			try
			{
				dstCancelCommit = new DataSet();
				chkSelectAll.Checked = false;
				dstCancelCommit = boCancelCommitment.ListCancelable(pintSOID);
				dstCancelCommit.Tables[SO_CommitInventoryDetailTable.TABLE_NAME].Columns.Add(CANCEL, typeof(bool));
				
				if (dstCancelCommit.Tables[0].Rows.Count != 0)
				{
					foreach (DataRow drow in dstCancelCommit.Tables[SO_CommitInventoryDetailTable.TABLE_NAME].Rows)
					{
						drow[CANCEL] = false;
					}
				}
				dgrdData.DataSource = dstCancelCommit.Tables[SO_CommitInventoryDetailTable.TABLE_NAME];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
				dgrdData.Splits[0].DisplayColumns[SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].DataColumn.NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					if (dgrdData.Splits[0].DisplayColumns[i].DataColumn.DataField != CANCEL)
						dgrdData.Splits[0].DisplayColumns[i].Locked = true;
				}
				btnCancelCommitment.Enabled = dgrdData.RowCount >0;
				dgrdData.AllowDelete = dgrdData.RowCount >0;
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


		//**************************************************************************              
		///    <Description>
		///       Check the checking on the grid
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///			Throw PCSExecption if have no item is check
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, March 3, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			int intCountCheck = 0;
			foreach (DataRow drow in dstCancelCommit.Tables[SO_CommitInventoryDetailTable.TABLE_NAME].Rows)
			{
				if (drow.RowState != DataRowState.Deleted)
				{
					if (drow[CANCEL].ToString().Trim() == TRUE)
					{
						intCountCheck++;
						return;
					}
				}
			}
			throw new PCSException(ErrorCode.MESSAGE_CANCELCOMMIT_ATLISTITEMCHECK, METHOD_NAME, null);
		}


		//**************************************************************************              
		///    <Description>
		///       Load form event
		///			Reset and init data
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, March 3, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void SOCancelCommitment_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".SOCancelCommitment_Load()";
			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					return;
				}
				
				btnCancelCommitment.Enabled = false;
				ResetForm();	
				LoadComboCCN();
				dtbGridLayout = FormControlComponents.StoreGridLayout(dgrdData);
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
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


		//**************************************************************************              
		///    <Description>
		///       Get custumer's inf and Buying location's inf by CustomerID and LocationID
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, March 3, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void GetInfors(int pintPartyID, int pintLocationID)
		{
            var voCustomer = Utilities.Instance.GetCustomerInfo(pintPartyID);
            txtCustomer.Text = voCustomer.Code;
            txtCustomerName.Text = voCustomer.Name;

            txtBuyingLoc.Text = new SOCancelCommitmentBO().GetBuyingLocName(pintLocationID);
		}


		//**************************************************************************              
		///    <Description>
		///       Find a SaleOrder
		///			fill data if search success
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, March 3, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnFindSaleOrder_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnFindSaleOrder_Click()";
			try 
			{
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(SO_SaleOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				
				drwResult = FormControlComponents.OpenSearchForm(v_SOCancelCommitment.VIEW_NAME , SO_SaleOrderMasterTable.CODE_FLD, txtSaleOrderCode.Text, htbCriteria, true);
				if (drwResult != null)
				{
					txtSaleOrderCode.Text = drwResult[SO_SaleOrderMasterTable.CODE_FLD].ToString();
					GetInfors(int.Parse(drwResult[SO_SaleOrderMasterTable.PARTYID_FLD].ToString()), int.Parse(drwResult[SO_SaleOrderMasterTable.BUYINGLOCID_FLD].ToString()));
					voSOMaster.SaleOrderMasterID = int.Parse(drwResult[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString());
				}
				else
				{
					txtSaleOrderCode.Focus();
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
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


		//**************************************************************************              
		///    <Description>
		///       Check all checkbox on grid and then autocheck chkCheckAll
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, March 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void CheckOrNochkCheckAll()
		{
			for (int i =0; i <dgrdData.RowCount; i++)
			{
				if (dgrdData[i, CANCEL].ToString().Trim() != TRUE)
				{
					chkSelectAll.Checked = false;
					return;
				}
			}
			chkSelectAll.Checked = true;
		}


		//**************************************************************************              
		///    <Description>
		///       Check all on uncheck all item on grid
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, March 3, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void chkSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if (blnStateOfCheck)
			{
				dgrdData.UpdateData();
				if (dstCancelCommit.Tables.Count == 0) return;
				if (chkSelectAll.Checked)
				{
					foreach (DataRow drow in dstCancelCommit.Tables[SO_CommitInventoryDetailTable.TABLE_NAME].Rows)
					{
						if (drow.RowState != DataRowState.Deleted)
							drow[CANCEL] = true;
					}
				}
				else
				{
					foreach (DataRow drow in dstCancelCommit.Tables[SO_CommitInventoryDetailTable.TABLE_NAME].Rows)
					{
						if (drow.RowState != DataRowState.Deleted)
							drow[CANCEL] = false;
					}
				}
			}
		}


		private void dgrdData_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			if (e.Column.DataColumn.DataField == CANCEL)
			{
				CheckOrNochkCheckAll();
			}
		}


		private void chkSelectAll_Enter(object sender, System.EventArgs e)
		{
			blnStateOfCheck = true;
		}


		private void chkSelectAll_Leave(object sender, System.EventArgs e)
		{
			blnStateOfCheck = false;
		}

		
		//**************************************************************************              
		///    <Description>
		///	      Check data and call UpdateSOCancelCommit of BO class
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, March 3, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void  btnCancelCommitment_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ". btnCancelCommitment_Click()";
			blnHasError = true;
			try
			{
				dgrdData.UpdateData();
				ValidateData();	
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_CANCELCOMIIT_AREYOURSURE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					boCancelCommitment.CancelCommitment(dstCancelCommit.Tables[0],int.Parse(cboCCN.SelectedValue.ToString()));
					blnHasError = false;
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					BindDataToGrid(voSOMaster.SaleOrderMasterID);
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				if (ex.CauseException != null)
				{
					try
					{
						Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
					}
				}
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

		//**************************************************************************              
		///    <Description>
		///       Close form event
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Friday, March 4, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		//**************************************************************************              
		///    <Description>
		///       Check to throw question if user check but not save before close
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       true : if user has changed on form
		///       false: if else
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Friday, March 4, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool CheckBeforeExit()
		{
			if (enumAction != EnumAction.Default)
			{
				foreach (DataRow drow in dstCancelCommit.Tables[SO_CommitInventoryDetailTable.TABLE_NAME].Rows)
				{
					if (drow.RowState != DataRowState.Deleted)
					{
						if (drow[CANCEL].ToString() == TRUE)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		//**************************************************************************              
		///    <Description>
		///       Close form
		///			check and throw question for user
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Friday, March 4, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void SOCancelCommitment_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (CheckBeforeExit())
			{
				DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				switch (confirmDialog)
				{
					case DialogResult.Yes:
						//Save before exit
						 btnCancelCommitment_Click( btnCancelCommitment, new EventArgs());
						if (blnHasError)
						{
							e.Cancel = true;
						}
						break;
					case DialogResult.No:
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
		}


		private void txtSaleOrderCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				btnFindSaleOrder_Click(sender, new EventArgs());
			}
		}

		//**************************************************************************              
		///    <Description>
		///		 Change the backgroud and open the search form if need
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, Mar 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void txtSaleOrderCode_Leave(object sender, System.EventArgs e)
		{
			string METHOD_NAME = THIS + ".txtSaleOrderCode_Leave()";
			OnLeaveControl(sender, e);
			if (!txtSaleOrderCode.Modified) return;
			if (txtSaleOrderCode.Text == string.Empty)
			{
				return;
			}
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(SO_SaleOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				
				drwResult = FormControlComponents.OpenSearchForm(v_SOCancelCommitment.VIEW_NAME , SO_SaleOrderMasterTable.CODE_FLD, txtSaleOrderCode.Text, htbCriteria, false);
				if (drwResult != null)
				{
					txtSaleOrderCode.Text = drwResult[SO_SaleOrderMasterTable.CODE_FLD].ToString();
					GetInfors(int.Parse(drwResult[SO_SaleOrderMasterTable.PARTYID_FLD].ToString()), int.Parse(drwResult[SO_SaleOrderMasterTable.BUYINGLOCID_FLD].ToString()));
					voSOMaster.SaleOrderMasterID = int.Parse(drwResult[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString());
				}
				else
				{
					txtSaleOrderCode.Focus();
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch(Exception ex)
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

		#region Change background when focus
		private void OnEnterControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ". OnEnterControl()";
			try
			{
				FormControlComponents.OnEnterControl(sender, e);
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

		private void OnLeaveControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ". OnLeaveControl()";
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
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

		private void SOCancelCommitment_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}


		//**************************************************************************              
		///    <Description>
		///    List all cancelable
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, Apr 27, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			try
			{
				if (FormControlComponents.CheckMandatory(txtSaleOrderCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					BindDataToGrid(0);
					txtSaleOrderCode.Focus();
					txtSaleOrderCode.Select();
					return;
				}
				BindDataToGrid(voSOMaster.SaleOrderMasterID);
				enumAction = EnumAction.Edit;
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
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

		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.Delete)
				{
					FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
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
