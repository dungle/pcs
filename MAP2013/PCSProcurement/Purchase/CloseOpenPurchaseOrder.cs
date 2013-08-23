using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSComProcurement.Purchase.BO;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for CloseOpenPurchaseOrder.
	/// </summary>
	public class CloseOpenPurchaseOrder : System.Windows.Forms.Form
	{
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Button btnSearchMasLoc;
		private System.Windows.Forms.TextBox txtMasLoc;
		private System.Windows.Forms.Label lblMasLoc;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label lblCCN;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		const string THIS = "PCSProcurement.Purchase.CloseOpenPurchaseOrder";
		UtilsBO boUtil = new UtilsBO();
		private DataTable dtbGridLayOut;
		private DataSet dstGridData;
		//For clearing datagrid purpose
		DataSet dstData = new DataSet();
		private const string SELECT = "Sel";
		private const string PURCHASEORDERNO = "PurchaseOrderNo";
		private const string VENDORCODE = "VendorCode";
		private const string VENDORNAME = "VendorName";
		private const string UM = "UM";
		private bool blnStateOfCheck = false;
		private bool blnPOClose = false;
		private DateTime dtmSpecialDate = new DateTime(1, 1, 1);
		private ArrayList arrSelectedLines;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private C1.Win.C1Input.C1DateEdit dtmToScheduleDateTime;
		private C1.Win.C1Input.C1DateEdit dtmFromScheduleDateTime;
		private System.Windows.Forms.Button btnClosePO;
		private System.Windows.Forms.CheckBox chkSelectAll;
		private System.Windows.Forms.Label lblFromScheduleDateTime;
		private System.Windows.Forms.TextBox txtPONo;
		private System.Windows.Forms.Label lblPONo;
		private System.Windows.Forms.Label lblToScheduleDateTime;
		private System.Windows.Forms.ComboBox cboCloseOrOpen;
		private System.Windows.Forms.Label lblCloseOrOpen;
		private System.Windows.Forms.Label lblClose;
		private System.Windows.Forms.Label lblOpen;
		private System.Windows.Forms.Button btnSearchPONo;
		private System.ComponentModel.Container components = null;

		public CloseOpenPurchaseOrder()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CloseOpenPurchaseOrder));
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.dtmToScheduleDateTime = new C1.Win.C1Input.C1DateEdit();
			this.dtmFromScheduleDateTime = new C1.Win.C1Input.C1DateEdit();
			this.btnSearchMasLoc = new System.Windows.Forms.Button();
			this.txtMasLoc = new System.Windows.Forms.TextBox();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.lblMasLoc = new System.Windows.Forms.Label();
			this.btnSearch = new System.Windows.Forms.Button();
			this.btnClosePO = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.lblCCN = new System.Windows.Forms.Label();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.lblFromScheduleDateTime = new System.Windows.Forms.Label();
			this.btnSearchPONo = new System.Windows.Forms.Button();
			this.txtPONo = new System.Windows.Forms.TextBox();
			this.lblPONo = new System.Windows.Forms.Label();
			this.lblToScheduleDateTime = new System.Windows.Forms.Label();
			this.cboCloseOrOpen = new System.Windows.Forms.ComboBox();
			this.lblCloseOrOpen = new System.Windows.Forms.Label();
			this.lblClose = new System.Windows.Forms.Label();
			this.lblOpen = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToScheduleDateTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromScheduleDateTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
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
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(600, 4);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(78, 21);
			this.cboCCN.TabIndex = 18;
			this.cboCCN.Text = "CCN";
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
				"icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</Client" +
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
				"Splits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRec" +
				"SelWidth></Blob>";
			// 
			// dtmToScheduleDateTime
			// 
			// 
			// dtmToScheduleDateTime.Calendar
			// 
			this.dtmToScheduleDateTime.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmToScheduleDateTime.CustomFormat = "dd-MM-yyyy";
			this.dtmToScheduleDateTime.EmptyAsNull = true;
			this.dtmToScheduleDateTime.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmToScheduleDateTime.Location = new System.Drawing.Point(364, 28);
			this.dtmToScheduleDateTime.Name = "dtmToScheduleDateTime";
			this.dtmToScheduleDateTime.Size = new System.Drawing.Size(126, 20);
			this.dtmToScheduleDateTime.TabIndex = 27;
			this.dtmToScheduleDateTime.Tag = null;
			this.dtmToScheduleDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmToScheduleDateTime.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			// 
			// dtmFromScheduleDateTime
			// 
			// 
			// dtmFromScheduleDateTime.Calendar
			// 
			this.dtmFromScheduleDateTime.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmFromScheduleDateTime.CustomFormat = "dd-MM-yyyy HH:mm";
			this.dtmFromScheduleDateTime.EmptyAsNull = true;
			this.dtmFromScheduleDateTime.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmFromScheduleDateTime.Location = new System.Drawing.Point(364, 3);
			this.dtmFromScheduleDateTime.Name = "dtmFromScheduleDateTime";
			this.dtmFromScheduleDateTime.Size = new System.Drawing.Size(126, 20);
			this.dtmFromScheduleDateTime.TabIndex = 25;
			this.dtmFromScheduleDateTime.Tag = null;
			this.dtmFromScheduleDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmFromScheduleDateTime.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			// 
			// btnSearchMasLoc
			// 
			this.btnSearchMasLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSearchMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearchMasLoc.Location = new System.Drawing.Point(172, 28);
			this.btnSearchMasLoc.Name = "btnSearchMasLoc";
			this.btnSearchMasLoc.Size = new System.Drawing.Size(24, 20);
			this.btnSearchMasLoc.TabIndex = 23;
			this.btnSearchMasLoc.Text = "...";
			this.btnSearchMasLoc.Click += new System.EventHandler(this.btnSearchMasLoc_Click);
			// 
			// txtMasLoc
			// 
			this.txtMasLoc.Location = new System.Drawing.Point(78, 28);
			this.txtMasLoc.Name = "txtMasLoc";
			this.txtMasLoc.Size = new System.Drawing.Size(93, 20);
			this.txtMasLoc.TabIndex = 22;
			this.txtMasLoc.Text = "";
			this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
			this.txtMasLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasLoc_Validating);
			// 
			// dgrdData
			// 
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.dgrdData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(3, 76);
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
			this.dgrdData.Size = new System.Drawing.Size(675, 348);
			this.dgrdData.TabIndex = 29;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColEdit);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Select\" Dat" +
				"aField=\"Sel\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><" +
				"C1DataColumn Level=\"0\" Caption=\"Pur. Oder No.\" DataField=\"PurchaseOrderNo\"><Valu" +
				"eItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"PO Line\" D" +
				"ataField=\"Line\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
				"\" Caption=\"Quantity\" DataField=\"OrderQuantity\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"Part Number\" DataField=\"Code\"><ValueIt" +
				"ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Part Name\" Da" +
				"taField=\"Description\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"UM\" DataField=\"UM\"><ValueItems /><GroupI" +
				"nfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Status\" DataField=\"Status\"" +
				"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Vend" +
				"or Code\" DataField=\"VendorCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
				"aColumn Level=\"0\" Caption=\"Vendor Name\" DataField=\"VendorName\"><ValueItems /><Gr" +
				"oupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.Con" +
				"textWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Ina" +
				"ctive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style78{}Style79{" +
				"}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style72{}Style73{" +
				"}Style70{AlignHorz:Near;}Style71{AlignHorz:Near;}Style76{AlignHorz:Near;}Style77" +
				"{AlignHorz:Center;}Style74{}Style75{}Style81{}Style80{}FilterBar{}Heading{Wrap:T" +
				"rue;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:C" +
				"ontrol;}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Center;}Style17{Al" +
				"ignHorz:Center;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style29{Align" +
				"Horz:Near;}Style27{}Style22{AlignHorz:Near;}Style28{AlignHorz:Center;}Style26{}S" +
				"tyle9{}Style8{}Style25{}Style24{}Style5{}Style4{}Style7{}Style6{}Style1{}Style23" +
				"{AlignHorz:Near;}Style3{}Style2{}Style21{}Style20{}OddRow{}Style38{}Style39{}Sty" +
				"le36{}Style37{}Style34{AlignHorz:Center;}Style35{AlignHorz:Center;}Style32{}Styl" +
				"e33{}Style30{}Style49{}Style48{}Style31{}Normal{Font:Microsoft Sans Serif, 8.25p" +
				"t;}Style41{AlignHorz:Far;}Style40{AlignHorz:Center;}Style43{}Style42{}Style45{}S" +
				"tyle44{}Style47{AlignHorz:Near;}Style46{AlignHorz:Center;}EvenRow{BackColor:Aqua" +
				";}Style59{AlignHorz:Near;}Style58{AlignHorz:Center;}RecordSelector{AlignImage:Ce" +
				"nter;}Style51{}Style50{}Footer{}Style52{AlignHorz:Center;}Style53{AlignHorz:Near" +
				";}Style54{}Style55{}Style56{}Style57{}Caption{AlignHorz:Center;}Style69{}Style68" +
				"{}Style63{}Style62{}Style61{}Style60{}Style67{}Style66{}Style65{AlignHorz:Center" +
				";}Style64{AlignHorz:Center;}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;" +
				"AlignVert:Center;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\"" +
				" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" MarqueeStyl" +
				"e=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScroll" +
				"Group=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 671, 344</ClientRect><Bord" +
				"erSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle p" +
				"arent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><Filte" +
				"rBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Sty" +
				"le3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" " +
				"me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveSt" +
				"yle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><" +
				"RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent" +
				"=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1" +
				"\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Width>47</Width><Height>15</Height><DCIdx>0</DC" +
				"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style76" +
				"\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle parent=\"Style3\" me=\"Style" +
				"78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><GroupHeaderStyle parent=\"Styl" +
				"e1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Style80\" /><Visible>Tru" +
				"e</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>48</Width><Heigh" +
				"t>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pa" +
				"rent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle " +
				"parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><Grou" +
				"pHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me" +
				"=\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivide" +
				"r><Width>108</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1Disp" +
				"layColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me" +
				"=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"St" +
				"yle5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFoot" +
				"erStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>Dar" +
				"kGray,Single</ColumnDivider><Width>56</Width><Height>15</Height><DCIdx>2</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" />" +
				"<Style parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" " +
				"/><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" " +
				"me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</V" +
				"isible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>155</Width><Height>1" +
				"5</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle paren" +
				"t=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle par" +
				"ent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHe" +
				"aderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"S" +
				"tyle56\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><" +
				"Width>168</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1Display" +
				"Column><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"S" +
				"tyle59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style" +
				"5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterS" +
				"tyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>DarkGr" +
				"ay,Single</ColumnDivider><Width>78</Width><Height>15</Height><DCIdx>6</DCIdx></C" +
				"1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><St" +
				"yle parent=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><" +
				"EditorStyle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=" +
				"\"Style69\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visi" +
				"ble><ColumnDivider>DarkGray,Single</ColumnDivider><Width>39</Width><Height>15</H" +
				"eight><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
				"tyle2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=" +
				"\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeader" +
				"Style parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
				"44\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Widt" +
				"h>81</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColum" +
				"n><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style2" +
				"3\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me" +
				"=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle " +
				"parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Si" +
				"ngle</ColumnDivider><Width>80</Width><Height>15</Height><DCIdx>9</DCIdx></C1Disp" +
				"layColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style70\" /><Style p" +
				"arent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=\"Style72\" /><Edito" +
				"rStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
				"e75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Visible>True</Visible><" +
				"ColumnDivider>DarkGray,Single</ColumnDivider><Width>222</Width><Height>15</Heigh" +
				"t><DCIdx>10</DCIdx></C1DisplayColumn></internalCols></C1.Win.C1TrueDBGrid.MergeV" +
				"iew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" " +
				"me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=" +
				"\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"S" +
				"elected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"Highl" +
				"ightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddR" +
				"ow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"F" +
				"ilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</ve" +
				"rtSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>" +
				"16</DefaultRecSelWidth><ClientArea>0, 0, 671, 344</ClientArea><PrintPageHeaderSt" +
				"yle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Bl" +
				"ob>";
			// 
			// lblMasLoc
			// 
			this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
			this.lblMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMasLoc.Location = new System.Drawing.Point(6, 28);
			this.lblMasLoc.Name = "lblMasLoc";
			this.lblMasLoc.Size = new System.Drawing.Size(80, 20);
			this.lblMasLoc.TabIndex = 21;
			this.lblMasLoc.Text = "Mas. Location";
			this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearch.Location = new System.Drawing.Point(618, 49);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(60, 23);
			this.btnSearch.TabIndex = 28;
			this.btnSearch.Text = "&Search";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// btnClosePO
			// 
			this.btnClosePO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClosePO.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClosePO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClosePO.Location = new System.Drawing.Point(3, 426);
			this.btnClosePO.Name = "btnClosePO";
			this.btnClosePO.Size = new System.Drawing.Size(70, 23);
			this.btnClosePO.TabIndex = 31;
			this.btnClosePO.Text = "&OK";
			this.btnClosePO.Click += new System.EventHandler(this.btnClosePO_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(618, 426);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 33;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(556, 426);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 32;
			this.btnHelp.Text = "&Help";
			// 
			// lblCCN
			// 
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(570, 4);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(32, 20);
			this.lblCCN.TabIndex = 17;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkSelectAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkSelectAll.Location = new System.Drawing.Point(84, 428);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(85, 20);
			this.chkSelectAll.TabIndex = 30;
			this.chkSelectAll.Text = "Select &All";
			this.chkSelectAll.Enter += new System.EventHandler(this.chkSelectAll_Enter);
			this.chkSelectAll.Leave += new System.EventHandler(this.chkSelectAll_Leave);
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// lblFromScheduleDateTime
			// 
			this.lblFromScheduleDateTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblFromScheduleDateTime.ForeColor = System.Drawing.Color.Black;
			this.lblFromScheduleDateTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblFromScheduleDateTime.Location = new System.Drawing.Point(232, 3);
			this.lblFromScheduleDateTime.Name = "lblFromScheduleDateTime";
			this.lblFromScheduleDateTime.Size = new System.Drawing.Size(139, 20);
			this.lblFromScheduleDateTime.TabIndex = 24;
			this.lblFromScheduleDateTime.Text = "From Schedule Date, Time";
			this.lblFromScheduleDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSearchPONo
			// 
			this.btnSearchPONo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSearchPONo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearchPONo.Location = new System.Drawing.Point(217, 52);
			this.btnSearchPONo.Name = "btnSearchPONo";
			this.btnSearchPONo.Size = new System.Drawing.Size(24, 20);
			this.btnSearchPONo.TabIndex = 36;
			this.btnSearchPONo.Text = "...";
			this.btnSearchPONo.Click += new System.EventHandler(this.btnSearchPONo_Click);
			// 
			// txtPONo
			// 
			this.txtPONo.Location = new System.Drawing.Point(78, 52);
			this.txtPONo.Name = "txtPONo";
			this.txtPONo.Size = new System.Drawing.Size(138, 20);
			this.txtPONo.TabIndex = 35;
			this.txtPONo.Text = "";
			this.txtPONo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPONo_KeyDown);
			this.txtPONo.Validating += new System.ComponentModel.CancelEventHandler(this.txtPONo_Validating);
			// 
			// lblPONo
			// 
			this.lblPONo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPONo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPONo.Location = new System.Drawing.Point(6, 52);
			this.lblPONo.Name = "lblPONo";
			this.lblPONo.Size = new System.Drawing.Size(80, 20);
			this.lblPONo.TabIndex = 34;
			this.lblPONo.Text = "Pur. Order No.";
			this.lblPONo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblToScheduleDateTime
			// 
			this.lblToScheduleDateTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblToScheduleDateTime.ForeColor = System.Drawing.Color.Black;
			this.lblToScheduleDateTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblToScheduleDateTime.Location = new System.Drawing.Point(232, 28);
			this.lblToScheduleDateTime.Name = "lblToScheduleDateTime";
			this.lblToScheduleDateTime.Size = new System.Drawing.Size(139, 20);
			this.lblToScheduleDateTime.TabIndex = 37;
			this.lblToScheduleDateTime.Text = "To Schedule Date, Time";
			this.lblToScheduleDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboCloseOrOpen
			// 
			this.cboCloseOrOpen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCloseOrOpen.Location = new System.Drawing.Point(78, 3);
			this.cboCloseOrOpen.Name = "cboCloseOrOpen";
			this.cboCloseOrOpen.Size = new System.Drawing.Size(94, 21);
			this.cboCloseOrOpen.TabIndex = 38;
			// 
			// lblCloseOrOpen
			// 
			this.lblCloseOrOpen.ForeColor = System.Drawing.Color.Maroon;
			this.lblCloseOrOpen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCloseOrOpen.Location = new System.Drawing.Point(6, 4);
			this.lblCloseOrOpen.Name = "lblCloseOrOpen";
			this.lblCloseOrOpen.Size = new System.Drawing.Size(64, 20);
			this.lblCloseOrOpen.TabIndex = 39;
			this.lblCloseOrOpen.Text = "Close/Open";
			this.lblCloseOrOpen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblClose
			// 
			this.lblClose.Location = new System.Drawing.Point(504, 4);
			this.lblClose.Name = "lblClose";
			this.lblClose.Size = new System.Drawing.Size(64, 23);
			this.lblClose.TabIndex = 40;
			this.lblClose.Text = "Close";
			this.lblClose.Visible = false;
			// 
			// lblOpen
			// 
			this.lblOpen.Location = new System.Drawing.Point(504, 30);
			this.lblOpen.Name = "lblOpen";
			this.lblOpen.TabIndex = 41;
			this.lblOpen.Text = "Open";
			this.lblOpen.Visible = false;
			// 
			// CloseOpenPurchaseOrder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(682, 453);
			this.Controls.Add(this.lblOpen);
			this.Controls.Add(this.lblClose);
			this.Controls.Add(this.cboCloseOrOpen);
			this.Controls.Add(this.txtPONo);
			this.Controls.Add(this.txtMasLoc);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.lblCloseOrOpen);
			this.Controls.Add(this.dtmToScheduleDateTime);
			this.Controls.Add(this.dtmFromScheduleDateTime);
			this.Controls.Add(this.lblToScheduleDateTime);
			this.Controls.Add(this.btnSearchPONo);
			this.Controls.Add(this.lblPONo);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.btnSearchMasLoc);
			this.Controls.Add(this.lblMasLoc);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.btnClosePO);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.chkSelectAll);
			this.Controls.Add(this.lblFromScheduleDateTime);
			this.Name = "CloseOpenPurchaseOrder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Close/Open Purchase Order";
			this.Load += new System.EventHandler(this.CloseOpenPurchaseOrder_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToScheduleDateTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromScheduleDateTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// CloseOpenPurchaseOrder_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Nov 24 2005</date>
		private void CloseOpenPurchaseOrder_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".CloseOpenPurchaseOrder_Load()";
			try
			{
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW ,MessageBoxIcon.Warning);
					return;
				}
				// Load combo box
				
				DataSet dstCCN = boUtil.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
				//Fill Default Master Location 
				FormControlComponents.SetDefaultMasterLocation(txtMasLoc);

				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
				//Store grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				//Load Close/Open combo box
				cboCloseOrOpen.Items.Clear();
				cboCloseOrOpen.Items.Add(lblClose.Text);// = 0
				cboCloseOrOpen.Items.Add(lblOpen.Text);   // = 1
				cboCloseOrOpen.SelectedIndex = 0;
				//format datetime
				dtmFromScheduleDateTime.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				dtmToScheduleDateTime.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
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

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// btnSearchMasLoc_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void btnSearchMasLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchMasLoc_Click()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				//User has selected CCN
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);	
				}
				else //User has not selected CCN
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					txtMasLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
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
		/// <summary>
		/// txtMasLoc_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void txtMasLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_Validating()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					return;
				}
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
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					txtMasLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
				}
				else
				{
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
		/// btnSearchPONo_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void btnSearchPONo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchPONo_Click()";
			try
			{
				Hashtable htbCondition = new Hashtable();
				if (txtMasLoc.Text != string.Empty)
				{
					htbCondition.Add(PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD, int.Parse(txtMasLoc.Tag.ToString()));	
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
					txtPONo.Text = string.Empty;
					txtMasLoc.Focus();
					return;
				}
				DataRowView drwResult = FormControlComponents.OpenSearchForm(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, txtPONo.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					txtPONo.Text = drwResult[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
					txtPONo.Tag = drwResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD];
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
		/// <summary>
		/// txtPONo_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void txtPONo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPONo_Validating()";
			try
			{
				if (txtPONo.Text == string.Empty)
				{
					txtPONo.Tag = null;
//					CreateDataSet();
//					dgrdData.DataSource = dstData.Tables[0];
//					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					return;
				}
				Hashtable htbCondition = new Hashtable();
				if (txtMasLoc.Text != string.Empty)
				{
					htbCondition.Add(PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD, int.Parse(txtMasLoc.Tag.ToString()));	
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
					txtPONo.Text = string.Empty;
					txtMasLoc.Focus();
					return;
				} 
				DataRowView drwResult = FormControlComponents.OpenSearchForm(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, txtPONo.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					txtPONo.Text = drwResult[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
					txtPONo.Tag = drwResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD];
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
		/// Create DataSet to bind blank grid
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void CreateDataSet()
		{
			const string METHOD_NAME = THIS + ".CreateDataSet()";
			try
			{
				dstData.Tables.Add(PO_PurchaseOrderDetailTable.TABLE_NAME);
				//insert columns which is invisible but use to update
				dstData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Columns.Add(SELECT);
				dstData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.STATUS_FLD);
				dstData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Columns.Add(PURCHASEORDERNO);
				dstData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Columns.Add(PO_PurchaseOrderDetailTable.LINE_FLD);
				dstData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.CODE_FLD);
				dstData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.DESCRIPTION_FLD);
				dstData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.REVISION_FLD);
				dstData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Columns.Add(UM);
				dstData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Columns.Add(PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD);
				dstData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Columns.Add(VENDORCODE);
				dstData.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Columns.Add(VENDORNAME);
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
		/// ConfigGrid
		/// </summary>
		/// <param name="pblnLock"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void ConfigGrid(bool pblnLock)
		{
			const string METHOD_NAME = THIS + ".ConfigGrid()";
			try
			{
				dgrdData.Enabled = true;
				for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].Locked = true;
				}
				dgrdData.Splits[0].DisplayColumns[SELECT].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[SELECT].DataColumn.ValueItems.Presentation  = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
				dgrdData.Splits[0].DisplayColumns[SELECT].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
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
		/// BindDataToGrid
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, Nov 29 2005</date>
		private void BindDataToGrid()
		{
			const string METHOD_NAME = THIS + ".BindDataToGrid()";
			try
			{
				int intPurchaseOrderMasterID = 0;
				if (txtPONo.Text != string.Empty)
				{
					intPurchaseOrderMasterID = int.Parse(txtPONo.Tag.ToString());
				}
				else
					intPurchaseOrderMasterID = 0;
				DateTime dtmFromScheduleDate = new DateTime(1,1,1);
				DateTime dtmToScheduleDate = new DateTime(1,1,1);
				if (dtmFromScheduleDateTime.Value.ToString() != string.Empty)
				{
					dtmFromScheduleDate = (DateTime)dtmFromScheduleDateTime.Value;
				}
				else
					dtmFromScheduleDate = dtmSpecialDate;
				if (dtmToScheduleDateTime.Value.ToString() != string.Empty)
				{
					dtmToScheduleDate = (DateTime)dtmToScheduleDateTime.Value;
				}
				else
					dtmToScheduleDate = dtmSpecialDate;
				CloseOpenPurchaseOrderBO boCloseOpenPurchaseOrder = new CloseOpenPurchaseOrderBO();
				if (cboCloseOrOpen.SelectedIndex == 0)
				{
					blnPOClose = true;
					dstGridData = boCloseOpenPurchaseOrder.GetPurchaseOrderDetail(int.Parse(cboCCN.SelectedValue.ToString()), int.Parse(txtMasLoc.Tag.ToString()), intPurchaseOrderMasterID, false, dtmFromScheduleDate, dtmToScheduleDate);
					foreach (DataRow drow in dstGridData.Tables[0].Rows)
					{
						drow[PRO_WorkOrderDetailTable.STATUS_FLD] = lblOpen.Text;
					}
				}
				else
				{
					blnPOClose = false;
					dstGridData = boCloseOpenPurchaseOrder.GetPurchaseOrderDetail(int.Parse(cboCCN.SelectedValue.ToString()), int.Parse(txtMasLoc.Tag.ToString()), intPurchaseOrderMasterID, true, dtmFromScheduleDate, dtmToScheduleDate);
					foreach (DataRow drow in dstGridData.Tables[0].Rows)
					{
						drow[PRO_WorkOrderDetailTable.STATUS_FLD] = lblClose.Text;
					}
				}
				foreach (DataRow drow in dstGridData.Tables[0].Rows)
				{
					drow[SELECT] = false;
				}
				//Bind Data to grid
				dgrdData.DataSource = dstGridData.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				ConfigGrid(false);
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
		/// btnSearch_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			try
			{
				if (ValidateData())
				{
					BindDataToGrid();
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
		/// <summary>
		/// ValidateData
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private bool ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			try
			{
				if (FormControlComponents.CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboCCN.Focus();
					cboCCN.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(cboCloseOrOpen))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboCloseOrOpen.Focus();
					cboCloseOrOpen.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtMasLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtMasLoc.Focus();
					txtMasLoc.Select();
					return false;
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
			return true;
		}
		/// <summary>
		/// txtMasLoc_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void txtMasLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnSearchMasLoc_Click(sender, e);
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
		/// txtPONo_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void txtPONo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPONo_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnSearchPONo_Click(sender, e);
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
		/// chkSelectAll_CheckedChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void chkSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAll_CheckedChanged()";
			try
			{
				
				if ((blnStateOfCheck)&&(dgrdData.RowCount != 0))
				{
					if (chkSelectAll.Checked)
					{
						foreach (DataRow drow in dstGridData.Tables[0].Rows)
						{
							if (drow.RowState != DataRowState.Deleted)
							{
								drow[SELECT] = true;
							}
						}
					}
					else
					{
						foreach (DataRow drow in dstGridData.Tables[0].Rows)
						{
							if (drow.RowState != DataRowState.Deleted)
							{
								drow[SELECT] = false;
							}
						}
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
		/// <summary>
		/// chkSelectAll_Enter
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author>Trada</Author>
		/// <date>Friday, Nov 25 2005</date>
		private void chkSelectAll_Enter(object sender, System.EventArgs e)
		{
			blnStateOfCheck = true;
		}
		/// <summary>
		/// chkSelectAll_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void chkSelectAll_Leave(object sender, System.EventArgs e)
		{
			blnStateOfCheck = false;
		}
		/// <summary>
		/// dgrdData_AfterColEdit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void dgrdData_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColEdit()";
			try
			{
				if (e.Column.DataColumn.DataField == SELECT)
				{
					for (int i =0; i <dgrdData.RowCount; i++)
					{
						if (dgrdData[i, SELECT].ToString().Trim() != true.ToString())
						{
							chkSelectAll.Checked = false;
							return;
						}
					}
					chkSelectAll.Checked = true;
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
		/// <summary>
		/// CheckGridData
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private bool CheckGridData()
		{
            int intNumRowSelected = 0;
            for (int i = 0; i < dgrdData.RowCount; i++)
            {
                if (dgrdData[i, SELECT].ToString() == false.ToString())
                {
                    intNumRowSelected++;
                }
            }
            return intNumRowSelected != dgrdData.RowCount;
		}
		/// <summary>
		/// StoreSelectedLines
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, Nov 29 2005</date>
		private void StoreSelectedLines()
		{
            arrSelectedLines = new ArrayList();
            for (int i = 0; i < dgrdData.RowCount; i++)
            {
                if (dgrdData[i, SELECT].ToString().Trim() == true.ToString())
                {
                    int intPOLineID;
                    intPOLineID = int.Parse(dgrdData[i, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
                    arrSelectedLines.Add(intPOLineID);
                }
            }
		}
		/// <summary>
		/// btnClosePO_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 25 2005</date>
		private void btnClosePO_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClosePO_Click()";
			try
			{
				if (CheckGridData())
				{
					string[] strParam = new string[1];
					if (blnPOClose)
					{
						strParam[0] = lblClose.Text;
					}
					else
						strParam[0] = lblOpen.Text;
					DialogResult enumResult = PCSMessageBox.Show(ErrorCode.MESSAGE_CLOSE_OR_OPEN_POLINE, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, strParam);
					if(enumResult == DialogResult.Yes)
					{
						StoreSelectedLines();
						CloseOpenPurchaseOrderBO boCloseOpenPurchaseOrder = new CloseOpenPurchaseOrderBO();
						boCloseOpenPurchaseOrder.CloseOrOpenPOLines(blnPOClose, arrSelectedLines);
						if (PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA) == DialogResult.OK)
						{
							BindDataToGrid();
						}
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
	}
}
