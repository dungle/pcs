using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using C1.Win.C1Input;
using PCSComProcurement.Purchase.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using AddNewModeEnum = C1.Win.C1TrueDBGrid.AddNewModeEnum;
using PresentationEnum = C1.Win.C1TrueDBGrid.PresentationEnum;
using ValueItem = C1.Win.C1TrueDBGrid.ValueItem;

namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for PODeliveryByHour.
	/// </summary>
	public class PODeliveryByHour : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Panel pnlRoleProtection;
		private System.Windows.Forms.Button btnHelp;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Label lblType;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		const string THIS = "PCSProcurement.Purchase.PODeliveryByHour";		
		DataSet dstVendor = new DataSet();
		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.Label lblDaily;
		private System.Windows.Forms.Label lblWeekly;
		private System.Windows.Forms.Label lblMonthly;
		DataSet dstData = new DataSet();
		DataTable dtbData = new DataTable();
		int intOlderRow = 0;
		private DataTable dtbGridLayOut;
		private const string TIME = "Time";
		private System.Windows.Forms.Panel pnlLeft;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel pnlRight;
		private System.Windows.Forms.Label lblItems;
		private System.Windows.Forms.Label label1;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdVendor;
		private const string TIME_FORMAT = "HH:mm";
		const int MIN_DATE = 1;
		const int MAX_DATE = 31;

		public PODeliveryByHour()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PODeliveryByHour));
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.pnlRoleProtection = new System.Windows.Forms.Panel();
			this.pnlRight = new System.Windows.Forms.Panel();
			this.lblItems = new System.Windows.Forms.Label();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.dgrdVendor = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.lblType = new System.Windows.Forms.Label();
			this.cboType = new System.Windows.Forms.ComboBox();
			this.lblDaily = new System.Windows.Forms.Label();
			this.lblWeekly = new System.Windows.Forms.Label();
			this.lblMonthly = new System.Windows.Forms.Label();
			this.pnlRoleProtection.SuspendLayout();
			this.pnlRight.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.pnlLeft.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrdVendor)).BeginInit();
			this.SuspendLayout();
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(8, 416);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(564, 416);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// pnlRoleProtection
			// 
			this.pnlRoleProtection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlRoleProtection.Controls.Add(this.pnlRight);
			this.pnlRoleProtection.Controls.Add(this.splitter1);
			this.pnlRoleProtection.Controls.Add(this.pnlLeft);
			this.pnlRoleProtection.Location = new System.Drawing.Point(8, 32);
			this.pnlRoleProtection.Name = "pnlRoleProtection";
			this.pnlRoleProtection.Size = new System.Drawing.Size(616, 376);
			this.pnlRoleProtection.TabIndex = 1;
			// 
			// pnlRight
			// 
			this.pnlRight.Controls.Add(this.lblItems);
			this.pnlRight.Controls.Add(this.dgrdData);
			this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlRight.Location = new System.Drawing.Point(256, 0);
			this.pnlRight.Name = "pnlRight";
			this.pnlRight.Size = new System.Drawing.Size(360, 376);
			this.pnlRight.TabIndex = 1;
			// 
			// lblItems
			// 
			this.lblItems.Location = new System.Drawing.Point(8, 0);
			this.lblItems.Name = "lblItems";
			this.lblItems.Size = new System.Drawing.Size(100, 16);
			this.lblItems.TabIndex = 0;
			this.lblItems.Text = "List of Items";
			// 
			// dgrdData
			// 
			this.dgrdData.AllowAddNew = true;
			this.dgrdData.AllowDelete = true;
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.FilterBar = true;
			this.dgrdData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(0, 16);
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
			this.dgrdData.Size = new System.Drawing.Size(360, 360);
			this.dgrdData.TabIndex = 0;
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdData_BeforeDelete);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.Error += new C1.Win.C1TrueDBGrid.ErrorEventHandler(this.dgrdData_Error);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Time\" DataF" +
				"ield=\"Time\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Part Number\" DataField=\"ITM_ProductCode\"><ValueItems /><GroupInfo /></C1D" +
				"ataColumn><C1DataColumn Level=\"0\" Caption=\"Model\" DataField=\"ITM_ProductRevision" +
				"\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Par" +
				"t Name\" DataField=\"ITM_ProductDescription\"><ValueItems /><GroupInfo /></C1DataCo" +
				"lumn><C1DataColumn Level=\"0\" Caption=\"Weekly Day\" DataField=\"WeeklyDay\"><ValueIt" +
				"ems Presentation=\"ComboBox\"><internalValues><ValueItem type=\"C1.Win.C1TrueDBGrid" +
				".ValueItem\" Value=\"1\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"2" +
				"\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"3\" /><ValueItem type=" +
				"\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"4\" /><ValueItem type=\"C1.Win.C1TrueDBGrid" +
				".ValueItem\" Value=\"5\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"6" +
				"\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"7\" /></internalValues" +
				"></ValueItems><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Month" +
				"ly Date\" DataField=\"MonthlyDate\"><ValueItems Presentation=\"ComboBox\"><internalVa" +
				"lues><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"1\" /><ValueItem type" +
				"=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"2\" /><ValueItem type=\"C1.Win.C1TrueDBGri" +
				"d.ValueItem\" Value=\"3\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"" +
				"4\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"5\" /><ValueItem type" +
				"=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"6\" /><ValueItem type=\"C1.Win.C1TrueDBGri" +
				"d.ValueItem\" Value=\"7\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"" +
				"8\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"9\" /><ValueItem type" +
				"=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"10\" /><ValueItem type=\"C1.Win.C1TrueDBGr" +
				"id.ValueItem\" Value=\"11\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value" +
				"=\"12\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"13\" /><ValueItem " +
				"type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"14\" /><ValueItem type=\"C1.Win.C1True" +
				"DBGrid.ValueItem\" Value=\"15\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" V" +
				"alue=\"16\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"17\" /><ValueI" +
				"tem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"18\" /><ValueItem type=\"C1.Win.C1" +
				"TrueDBGrid.ValueItem\" Value=\"19\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueIte" +
				"m\" Value=\"20\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"21\" /><Va" +
				"lueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"22\" /><ValueItem type=\"C1.Wi" +
				"n.C1TrueDBGrid.ValueItem\" Value=\"23\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.Valu" +
				"eItem\" Value=\"24\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"25\" /" +
				"><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"26\" /><ValueItem type=\"C" +
				"1.Win.C1TrueDBGrid.ValueItem\" Value=\"27\" /><ValueItem type=\"C1.Win.C1TrueDBGrid." +
				"ValueItem\" Value=\"28\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"2" +
				"9\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"30\" /><ValueItem typ" +
				"e=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"31\" /></internalValues></ValueItems><Gr" +
				"oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Comment\" DataField=\"Co" +
				"mment\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
				"=\"Category\" DataField=\"ITM_CategoryCode\"><ValueItems /><GroupInfo /></C1DataColu" +
				"mn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Sty" +
				"le36{}Style108{}Style109{}HighlightRow{ForeColor:HighlightText;BackColor:Highlig" +
				"ht;}Style104{}Style105{}Style106{AlignHorz:Center;}Style107{AlignHorz:Near;}Capt" +
				"ion{AlignHorz:Center;}Style101{AlignHorz:Near;}Normal{}Style103{}Selected{ForeCo" +
				"lor:HighlightText;BackColor:Highlight;}Style22{AlignHorz:Center;ForeColor:Maroon" +
				";}Editor{}Style18{}Style19{ForeColor:WindowText;}Style110{}Style14{}Style15{}Sty" +
				"le16{AlignHorz:Center;ForeColor:Maroon;}Style17{AlignHorz:Near;}Style10{AlignHor" +
				"z:Near;}Style11{}Style12{}Style13{}Style45{}Style44{}Style43{}Style98{}Style99{}" +
				"Style38{}Style39{}Style20{}Style37{}Style35{AlignHorz:Near;}Style4{}OddRow{}Styl" +
				"e29{AlignHorz:Near;}Style28{AlignHorz:Near;}Style27{ForeColor:WindowText;}Style2" +
				"6{}RecordSelector{AlignImage:Center;}Footer{}Style23{AlignHorz:Near;}Style100{Al" +
				"ignHorz:Center;ForeColor:Maroon;}Style21{ForeColor:WindowText;}Style102{}Group{B" +
				"ackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Inactive{ForeColo" +
				"r:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading" +
				"{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;Back" +
				"Color:Control;}Style25{ForeColor:WindowText;}Style24{}Style6{}Style94{AlignHorz:" +
				"Center;ForeColor:Maroon;}Style3{}Style41{AlignHorz:Near;}Style40{AlignHorz:Cente" +
				"r;ForeColor:Maroon;}Style111{}Style42{}Style95{AlignHorz:Near;}Style96{}Style97{" +
				"}Style9{}Style8{}Style1{}FilterBar{}Style5{}Style34{AlignHorz:Center;ForeColor:M" +
				"aroon;}Style7{}Style32{}Style33{}Style30{}Style31{}Style2{}</Data></Styles><Spli" +
				"ts><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight" +
				"=\"17\" ColumnFooterHeight=\"17\" FilterBar=\"True\" MarqueeStyle=\"DottedCellBorder\" R" +
				"ecordSelectorWidth=\"17\" DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" HorizontalSc" +
				"rollGroup=\"1\"><ClientRect>0, 0, 356, 356</ClientRect><BorderSide>0</BorderSide><" +
				"CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Sty" +
				"le5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"Filt" +
				"erBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle par" +
				"ent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLig" +
				"htRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" " +
				"me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle pa" +
				"rent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6" +
				"\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><Heading" +
				"Style parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><Foot" +
				"erStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\"" +
				" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"St" +
				"yle1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colu" +
				"mnDivider><Width>39</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn>" +
				"<C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style94\" /><Style parent=\"Sty" +
				"le1\" me=\"Style95\" /><FooterStyle parent=\"Style3\" me=\"Style96\" /><EditorStyle par" +
				"ent=\"Style5\" me=\"Style97\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style99\" /><Gr" +
				"oupFooterStyle parent=\"Style1\" me=\"Style98\" /><Visible>True</Visible><ColumnDivi" +
				"der>DarkGray,Single</ColumnDivider><Width>84</Width><Height>15</Height><Button>T" +
				"rue</Button><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style100\" /><Style parent=\"Style1\" me=\"Style101\" /><FooterStyle" +
				" parent=\"Style3\" me=\"Style102\" /><EditorStyle parent=\"Style5\" me=\"Style103\" /><G" +
				"roupHeaderStyle parent=\"Style1\" me=\"Style105\" /><GroupFooterStyle parent=\"Style1" +
				"\" me=\"Style104\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Width>82</Width><Height>15</Height><Button>True</Button><DCIdx>5</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" />" +
				"<Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" " +
				"/><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" " +
				"me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</V" +
				"isible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>7" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
				"le22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"S" +
				"tyle24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"" +
				"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible" +
				">True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>162</Width><" +
				"Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterS" +
				"tyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style" +
				"1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Width>200</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C" +
				"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style" +
				"1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle paren" +
				"t=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><Grou" +
				"pFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivide" +
				"r>DarkGray,Single</ColumnDivider><Width>97</Width><Height>15</Height><DCIdx>2</D" +
				"CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style1" +
				"06\" /><Style parent=\"Style1\" me=\"Style107\" /><FooterStyle parent=\"Style3\" me=\"St" +
				"yle108\" /><EditorStyle parent=\"Style5\" me=\"Style109\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style111\" /><GroupFooterStyle parent=\"Style1\" me=\"Style110\" /><Visi" +
				"ble>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>154</Widt" +
				"h><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn></internalCols></C1.Win.C" +
				"1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Styl" +
				"e parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style pa" +
				"rent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style par" +
				"ent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=" +
				"\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent" +
				"=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style par" +
				"ent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles" +
				"><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><D" +
				"efaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 356, 356</ClientArea>" +
				"<PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" m" +
				"e=\"Style15\" /></Blob>";
			// 
			// splitter1
			// 
			this.splitter1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.splitter1.Location = new System.Drawing.Point(248, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(8, 376);
			this.splitter1.TabIndex = 0;
			this.splitter1.TabStop = false;
			// 
			// pnlLeft
			// 
			this.pnlLeft.Controls.Add(this.dgrdVendor);
			this.pnlLeft.Controls.Add(this.label1);
			this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLeft.Location = new System.Drawing.Point(0, 0);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(248, 376);
			this.pnlLeft.TabIndex = 0;
			// 
			// dgrdVendor
			// 
			this.dgrdVendor.AllowUpdate = false;
			this.dgrdVendor.CaptionHeight = 17;
			this.dgrdVendor.CollapseColor = System.Drawing.Color.Black;
			this.dgrdVendor.ExpandColor = System.Drawing.Color.Black;
			this.dgrdVendor.FilterBar = true;
			this.dgrdVendor.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdVendor.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.dgrdVendor.Location = new System.Drawing.Point(0, 16);
			this.dgrdVendor.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdVendor.Name = "dgrdVendor";
			this.dgrdVendor.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.dgrdVendor.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.dgrdVendor.PreviewInfo.ZoomFactor = 75;
			this.dgrdVendor.PrintInfo.ShowOptionsDialog = false;
			this.dgrdVendor.RecordSelectorWidth = 17;
			this.dgrdVendor.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.dgrdVendor.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.dgrdVendor.RowHeight = 15;
			this.dgrdVendor.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.dgrdVendor.Size = new System.Drawing.Size(248, 360);
			this.dgrdVendor.TabIndex = 0;
			this.dgrdVendor.Text = "c1TrueDBGrid1";
			this.dgrdVendor.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.dgrdVendor_RowColChange);
			this.dgrdVendor.BeforeRowColChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdVendor_BeforeRowColChange);
			this.dgrdVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdVendor_KeyDown);
			this.dgrdVendor.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Vendor Code" +
				"\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Vendor Name\" DataField=\"Name\"><ValueItems /><GroupInfo /></C1DataC" +
				"olumn><C1DataColumn Level=\"0\" Caption=\"Address\" DataField=\"Address\"><ValueItems " +
				"/><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Desig" +
				"n.ContextWrapper\"><Data>RecordSelector{AlignImage:Center;}Caption{AlignHorz:Cent" +
				"er;}Normal{}Style24{}Editor{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHo" +
				"rz:Near;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{}Style" +
				"13{}OddRow{}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}Style27{}Style26{}St" +
				"yle25{}Footer{}Style23{AlignHorz:Near;}Style22{AlignHorz:Near;}Style21{}Style20{" +
				"}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow{BackC" +
				"olor:Aqua;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColo" +
				"r:ControlText;BackColor:Control;}Style3{}Style2{}Style6{}FilterBar{}Selected{For" +
				"eColor:HighlightText;BackColor:Highlight;}Style4{}Style9{}Style8{}Style1{}Style5" +
				"{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style7{}" +
				"Style32{}Style33{}Style30{}Style31{}HighlightRow{ForeColor:HighlightText;BackCol" +
				"or:Highlight;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" Cap" +
				"tionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" FilterBar=\"True" +
				"\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"17\" DefRecSelWidth=\"17\" V" +
				"erticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 244, 356</Cli" +
				"entRect><BorderSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><" +
				"EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Sty" +
				"le8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Fo" +
				"oter\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle pare" +
				"nt=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" " +
				"/><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me" +
				"=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><Selecte" +
				"dStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><int" +
				"ernalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style p" +
				"arent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><Edito" +
				"rStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
				"e21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><" +
				"ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>0</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" />" +
				"<Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" " +
				"/><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" " +
				"me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</V" +
				"isible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>208</Width><Height>1" +
				"5</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle paren" +
				"t=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle par" +
				"ent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHe" +
				"aderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"S" +
				"tyle32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><" +
				"Width>214</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn></internal" +
				"Cols></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"" +
				"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Foot" +
				"er\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactiv" +
				"e\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /" +
				"><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" " +
				"/><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelecto" +
				"r\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" " +
				"/></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modi" +
				"fied</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 244, 3" +
				"56</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterSt" +
				"yle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "List of Vendors";
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(503, 416);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 3;
			this.btnHelp.Text = "&Help";
			// 
			// lblType
			// 
			this.lblType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblType.Location = new System.Drawing.Point(480, 8);
			this.lblType.Name = "lblType";
			this.lblType.Size = new System.Drawing.Size(32, 21);
			this.lblType.TabIndex = 0;
			this.lblType.Text = "Type";
			this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboType
			// 
			this.cboType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboType.Location = new System.Drawing.Point(520, 8);
			this.cboType.Name = "cboType";
			this.cboType.Size = new System.Drawing.Size(104, 21);
			this.cboType.TabIndex = 0;
			this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
			// 
			// lblDaily
			// 
			this.lblDaily.Location = new System.Drawing.Point(320, 8);
			this.lblDaily.Name = "lblDaily";
			this.lblDaily.Size = new System.Drawing.Size(32, 23);
			this.lblDaily.TabIndex = 0;
			this.lblDaily.Text = "Daily";
			this.lblDaily.Visible = false;
			// 
			// lblWeekly
			// 
			this.lblWeekly.Location = new System.Drawing.Point(360, 8);
			this.lblWeekly.Name = "lblWeekly";
			this.lblWeekly.Size = new System.Drawing.Size(48, 23);
			this.lblWeekly.TabIndex = 1;
			this.lblWeekly.Text = "Weekly";
			this.lblWeekly.Visible = false;
			// 
			// lblMonthly
			// 
			this.lblMonthly.Location = new System.Drawing.Point(408, 8);
			this.lblMonthly.Name = "lblMonthly";
			this.lblMonthly.Size = new System.Drawing.Size(48, 23);
			this.lblMonthly.TabIndex = 2;
			this.lblMonthly.Text = "Monthly";
			this.lblMonthly.Visible = false;
			// 
			// PODeliveryByHour
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(632, 446);
			this.Controls.Add(this.lblMonthly);
			this.Controls.Add(this.lblWeekly);
			this.Controls.Add(this.lblDaily);
			this.Controls.Add(this.cboType);
			this.Controls.Add(this.lblType);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.pnlRoleProtection);
			this.Controls.Add(this.btnHelp);
			this.KeyPreview = true;
			this.Name = "PODeliveryByHour";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PO Delivery By Hour";
			this.Load += new System.EventHandler(this.PODeliveryByHour_Load);
			this.pnlRoleProtection.ResumeLayout(false);
			this.pnlRight.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.pnlLeft.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrdVendor)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// ConfigGrid
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 7 2006</date>
		private void ConfigGrid(bool pblnLock)
		{
			const string METHOD_NAME = THIS + ".ConfigGrid()";
//			try
//			{
				for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].Locked = true;
				}
			dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.COMMENT_FLD].Locked = false;
				dgrdData.Enabled = true;
				switch (cboType.SelectedIndex)
				{
					case 0: //Daily
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Locked = pblnLock;
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Locked = pblnLock;
						break;
					case 1: //Weekly
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Visible = false;
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Locked = pblnLock;
						break;
					case 2:	//Monthly
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Visible = false;
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Locked = pblnLock;
						break;
				}
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.CCNID_FLD].Visible = false;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[TIME].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Locked = pblnLock;
				
				if (!pblnLock)
				{
					dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Button = true;
					dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Button = true;
				}
//			}
//			catch (PCSException ex)
//			{
//				throw new PCSException(ex.mCode, METHOD_NAME, ex);
//			}
//			catch (Exception ex)
//			{
//				throw new Exception(ex.Message, ex);
//			}
		}
		/// <summary>
		/// Fill Data to grid by PartyID
		/// </summary>
		/// <param name="pintPartyID"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 7 2006</date>
		private void FillDataToGrid(int pintPartyID, int pintType)
		{
			if(dstData.Tables.Count == 0) return;
			string strWeekOrMonth = string.Empty;
			if(cboType.SelectedIndex == (int)PODeliveryTypeEnum.Weekly)
			{
				strWeekOrMonth = PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD + " ASC,";
			}
			else if (cboType.SelectedIndex == (int)PODeliveryTypeEnum.Monthly)
			{
				strWeekOrMonth = PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD + " ASC,";
			}

			DataRow[] adrowData = dstData.Tables[0].Select(PO_VendorDeliveryScheduleTable.PARTYID_FLD + " = " + pintPartyID.ToString() 
				+ " AND " + PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD + " = " + pintType.ToString(),
				ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD + " ASC," + strWeekOrMonth + " Time ASC");
			//Convert to DataTable
			dtbData = dstData.Tables[0].Clone();
			for (int i = 0; i < adrowData.Length; i++)
			{
				DataRow drowNew = dtbData.NewRow();
				foreach (DataColumn dcol in dtbData.Columns)
				{
					drowNew[dcol.Caption] = adrowData[i][dcol.Caption];
				}
				dtbData.Rows.Add(drowNew);
			}
			dtbData.AcceptChanges();
			dgrdData.DataSource = dtbData;
			C1DateEdit dtmTime = new C1DateEdit();
			dtmTime.FormatType = FormatTypeEnum.CustomFormat;
			dtmTime.CustomFormat = TIME_FORMAT;
			dtmTime.ShowUpDownButtons = false;
			dtmTime.ShowDropDownButton = false;
			
			dgrdData.Columns[TIME].Editor = dtmTime;
			dgrdData.Columns[TIME].NumberFormat = TIME_FORMAT;
			// cboDayOfMonth.
			ComboBox cboDayOfMonth = new ComboBox();
			//cboDayOfMonth.DropDownStyle = ComboBoxStyle.;
			FormControlComponents.InitDayOfMonthComboBox(cboDayOfMonth);
			dgrdData.Columns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Editor = cboDayOfMonth;
			// cboWeek.
//			C1Combo cboDayOfWeek = new C1Combo();
//			DataTable dtbWeek = new DataTable();
//			dtbWeek.Columns.Add("ID",typeof(int));
//			dtbWeek.Columns.Add("Week day",typeof(string));
			Array arrValue = DayOfWeekEnum.GetValues(typeof(DayOfWeekEnum));
			string[] arrItems = DayOfWeekEnum.GetNames(typeof(DayOfWeekEnum));
			dgrdData.Columns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].ValueItems.Presentation = PresentationEnum.ComboBox;
			dgrdData.Columns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].ValueItems.Translate = true;
			dgrdData.Columns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].ValueItems.MaxComboItems = 7;
			dgrdData.Columns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].ValueItems.Validate = true;
			for(int i = 0;i < arrItems.Length; i++)
			{
				//dtbWeek.Rows.Add(new object[] {i+1,arrItems[i]});
				ValueItem vi = new ValueItem((i).ToString(),arrItems[i]); //(arrValue.GetValue(i).ToString(),arrItems[i]);	
				dgrdData.Columns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].ValueItems.Values.Add(vi);
			}
//			cboDayOfWeek.DataSource = dtbWeek;
//			cboDayOfWeek.Splits[0].DisplayColumns[0].Visible = false;
//			cboDayOfWeek.Splits[0].DisplayColumns[0].AllowSizing=false;
		//	dgrdData.Columns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Editor = cboDayOfWeek;

			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
			if(cboType.SelectedIndex == (int)PODeliveryTypeEnum.Weekly)
			{
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Visible = true;
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Locked = false;
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Visible = false;
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Locked = true;
			}
			else if(cboType.SelectedIndex == (int)PODeliveryTypeEnum.Monthly)
			{
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Visible = false;
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Locked = true;
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Visible = true;
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Locked = false;
			}
			else
			{
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Visible = false;
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Locked = true;
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Visible = false;
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Locked = true;
			}

			ConfigGrid(false);
		}
		
		/// <summary>
		/// PODeliveryByHour_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 7 2006</date>
		private void PODeliveryByHour_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".PODeliveryByHour_Load()";
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
				//Fill data to grid
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Visible = 
					dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Visible = false;
				dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Locked = 
					dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Locked = true;
				//Store grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
                //Get Vendor 
				PODeliveryByHourBO boPODeliveryByHour = new PODeliveryByHourBO();
				dstVendor = boPODeliveryByHour.GetVendor();
				DataTable dtbStoreVendorLayout = FormControlComponents.StoreGridLayout(dgrdVendor);
				dgrdVendor.DataSource = dstVendor.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdVendor, dtbStoreVendorLayout);
				dgrdVendor.Splits[0].DisplayColumns[MST_PartyTable.PARTYID_FLD].Visible = false;
				dgrdVendor.Splits[0].DisplayColumns[MST_PartyTable.PARTYID_FLD].Locked = true;
				for(int i=4; i < dgrdVendor.Splits[0].DisplayColumns.Count;i++)
				{
					dgrdVendor.Splits[0].DisplayColumns[i].Visible = false;	
					dgrdVendor.Splits[0].DisplayColumns[i].Locked = true;
				}

				dgrdVendor.Row = 0;
				dgrdVendor.Focus();
				//Load Type
				cboType.Items.Clear();
				cboType.Items.Add(lblDaily.Text);	// = 0
				cboType.Items.Add(lblWeekly.Text);   // = 1
				cboType.Items.Add(lblMonthly.Text);   // = 2
				cboType.SelectedIndex = 0;
				//Get all VendorDeliverySchedule
				dstData = boPODeliveryByHour.GetVendorDeliverySchedule();
				if(dgrdVendor[dgrdVendor.Row, MST_PartyTable.PARTYID_FLD].ToString() != string.Empty)
				{
					FillDataToGrid(int.Parse(dgrdVendor[dgrdVendor.Row, MST_PartyTable.PARTYID_FLD].ToString()), cboType.SelectedIndex);
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
		/// fill data to grid 
		/// </summary>
		/// <param name="pdrowData"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 9 2006</date>
		private void FillItemData(DataRow pdrowData)
		{
			int i = dgrdData.Row;
			dgrdData.EditActive = true;
//			dgrdData[i, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = pdrowData[ITM_ProductTable.CODE_FLD].ToString();
//			dgrdData[i, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = pdrowData[ITM_ProductTable.DESCRIPTION_FLD].ToString();
//			dgrdData[i, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = pdrowData[ITM_ProductTable.REVISION_FLD].ToString();
//			dgrdData[i, ITM_ProductTable.PRODUCTID_FLD] = pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString();
			dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = pdrowData[ITM_ProductTable.CODE_FLD].ToString();
			dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value = pdrowData[ITM_ProductTable.DESCRIPTION_FLD].ToString();
			dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = pdrowData[ITM_ProductTable.REVISION_FLD].ToString();
			dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString();

		}
		/// <summary>	
		/// dgrdData_ButtonClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 9 2006</date>
		private void dgrdData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				//Part Number
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD])) 
				{
					//open the search form to select Product
					Hashtable htbCondition = new Hashtable();
					DataRowView drwResult = null;
					htbCondition.Add(ITM_ProductTable.PRIMARYVENDORID_FLD, int.Parse(dgrdVendor[dgrdVendor.Row, MST_PartyTable.PARTYID_FLD].ToString()));
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].ToString(), htbCondition, true);
						//drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, "ProductID NOT IN ()" );
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Text.Trim(), htbCondition, true);
						//drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].ToString(),)
					}
					if (drwResult != null)
					{
						FillItemData(drwResult.Row);
					}
				}
				//Part Name
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD])) 
				{
					//open the search form to select Product
					Hashtable htbCondition = new Hashtable();
					DataRowView drwResult = null;
					htbCondition.Add(ITM_ProductTable.PRIMARYVENDORID_FLD, int.Parse(dgrdVendor[dgrdVendor.Row, MST_PartyTable.PARTYID_FLD].ToString()));
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						FillItemData(drwResult.Row);
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
		/// dgrdData_BeforeColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 9 2006</date>
		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				if (e.Column.DataColumn.Value.ToString() == string.Empty) return;
				string strCondition = string.Empty;
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				htbCriteria.Add(ITM_ProductTable.PRIMARYVENDORID_FLD, int.Parse(dgrdVendor[dgrdVendor.Row, MST_PartyTable.PARTYID_FLD].ToString()));
				//Part Number 
				if (e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD)
				{
					if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult.Row;
						}
						else
						{
							e.Cancel = true;
						}
					}
				}
				//Part Name
				if (e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD)
				{
					if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult.Row;
						}
						else
						{
							e.Cancel = true;
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
		/// dgrdData_AfterColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 9 2006</date>
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				if (e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD 
					|| e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||( e.Column.DataColumn.Value.ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD] = string.Empty;
					}
					else
					{
						FillItemData((DataRow) e.Column.DataColumn.Tag);
						return;
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
		/// cboType_SelectedIndexChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 9 2006</date>
		private void cboType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboType_SelectedIndexChanged()";
			try
			{
//				if(dtbData.GetChanges() != null)
//				{
//					dgrdVendor_BeforeRowColChange(sender,null);
//					foreach(DataRow drow in dtbData.Rows)
//					{
//						if(drow.RowState == DataRowState.Deleted ) continue;
//						drow[PO_VendorDeliveryScheduleTable.DELHOUR_FLD] = ((DateTime)drow[TIME]).Hour;
//						drow[PO_VendorDeliveryScheduleTable.DELMIN_FLD] = ((DateTime)drow[TIME]).Minute;
//						drow[PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD] = cboType.SelectedIndex;
//						drow[PO_VendorDeliveryScheduleTable.CCNID_FLD] = SystemProperty.CCNID;
//						drow[PO_VendorDeliveryScheduleTable.PARTYID_FLD] = dgrdVendor[dgrdVendor.Row, MST_PartyTable.PARTYID_FLD];
//					}
//				}
				dgrdVendor_RowColChange(sender,null);

				switch (cboType.SelectedIndex)
				{
					case 0: //Daily
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Visible = 
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Visible = false;
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Locked = 
							dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Locked = true;
						break;
					case 1: //Weekly
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Visible = false;
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Locked = true;
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Visible = true;
						
						break;
					case 2:	//Monthly
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Visible = false;
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD].Locked = true;
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].Visible = true;
						
						break;
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
		/// dgrdVendor_AfterRowColChange
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 9 2006</date>
//		private void dgrdVendor_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
//		{
//
//		}
		/// <summary>
		/// ValidateData
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 9 2006</date>
		private bool ValidateData()
		{
            for (int i = 0; i < dgrdData.RowCount; i++)
            {
                if (dgrdData[i, TIME].ToString() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    dgrdData.Row = i;
                    dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[TIME]);
                    dgrdData.Focus();
                    return false;
                }
                if (dgrdData[i, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].ToString() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    dgrdData.Row = i;
                    dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
                    dgrdData.Focus();
                    return false;
                }
                if (dgrdData[i, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].ToString() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    dgrdData.Row = i;
                    dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD]);
                    dgrdData.Focus();
                    return false;
                }
            }
            return true;
		}
		
		/// <summary>
		/// btnSave_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 9 2006</date>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				dgrdVendor_BeforeRowColChange(sender,null);
				//SaveData();
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

//		private void dgrdVendor_BeforeRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
//		{
//
//		}

		private void dgrdVendor_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdVendor_AfterRowColChange()";
			try
			{
				//if(dtbData.DataSet == null) return;
				//Fill data to grid
				try // if load form dgrdVendor has no data
				{
					if(dgrdVendor[dgrdVendor.Row, MST_PartyTable.PARTYID_FLD].ToString() != string.Empty)
					{
						PODeliveryByHourBO boPODelivery = new PODeliveryByHourBO();
						dstData = boPODelivery.GetVendorDeliverySchedule();
						FillDataToGrid(int.Parse(dgrdVendor[dgrdVendor.Row, MST_PartyTable.PARTYID_FLD].ToString()), cboType.SelectedIndex);
					}
				}
				catch{}

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

		private void dgrdData_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeDelete()";
			try
			{
				if(dgrdData[dgrdData.Row,PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD] != DBNull.Value)
				{
					DataRow[] drows = dstData.Tables[0].Select(PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD + 
						" = " + dgrdData[dgrdData.Row,PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD]);
					foreach(DataRow drow in drows)
					{
						drow.Delete();
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
		/// Save data into database
		/// </summary>
		/// <returns></returns>
		bool SaveData()
		{
			// Check mandatory
			for(int i = 0; i < dgrdData.RowCount; i++)
			{
				if(dgrdData[i,TIME] == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxButtons.OK,MessageBoxIcon.Error);
					dgrdData.Row = i;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[TIME]);
					return false;
				}
				if(dgrdData[i,ITM_ProductTable.TABLE_NAME+ITM_ProductTable.CODE_FLD] == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxButtons.OK,MessageBoxIcon.Error);
					dgrdData.Row = i;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME+ITM_ProductTable.CODE_FLD]);
					return false;
				}
				if(cboType.SelectedIndex == (int)PODeliveryTypeEnum.Weekly)
				{
					if(dgrdData[i,PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD] == DBNull.Value)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxButtons.OK,MessageBoxIcon.Error);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD]);
						return false;
					}
				}
				else if(cboType.SelectedIndex == (int)PODeliveryTypeEnum.Monthly)
				{
					if(dgrdData[i,PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD] == DBNull.Value)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxButtons.OK,MessageBoxIcon.Error);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
						return false;
					}
				}
					
			}
			// Check duplicate data
			for(int i = 0; i < dgrdData.RowCount; i++)
			{
				int intProductID = 0;
				if(dgrdData[i,ITM_ProductTable.PRODUCTID_FLD].ToString() != string.Empty)
					intProductID = Convert.ToInt32(dgrdData[i,ITM_ProductTable.PRODUCTID_FLD]);
				DataRow[] drowDups = dstData.Tables[0].Select(ITM_ProductTable.PRODUCTID_FLD + "=" + intProductID
					+ " AND " + PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD + "<>" + cboType.SelectedIndex);
				if(drowDups.Length > 0)
				{
					//PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY,MessageBoxButtons.OK,MessageBoxIcon.Error);
					PCSMessageBox.Show(ErrorCode.MESSAGE_ITEM_BELONG_ANOTHER_TYPE,MessageBoxButtons.OK,MessageBoxIcon.Error);
					dgrdData.Row = i; 
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME+ITM_ProductTable.CODE_FLD]);
					return false;
				}
				// check date in month [1..31]
				if(cboType.SelectedIndex == (int)PODeliveryTypeEnum.Monthly)
				{
					int intDate = 0;
					if(dgrdData[i,PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD].ToString() != string.Empty)
					{
						intDate = Convert.ToInt32(dgrdData[i,PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
						if((intDate < MIN_DATE) || (intDate > MAX_DATE))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_NUMBER_MUST_IN_SCOPE,MessageBoxIcon.Error, new string[]{MIN_DATE.ToString(),MAX_DATE.ToString()});
							dgrdData.Row = i; 
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
							return false;
						}
					}
				}
				for(int j = i + 1; j < dgrdData.RowCount; j++)
				{
					if(dgrdData[j,ITM_ProductTable.PRODUCTID_FLD].ToString() != string.Empty)
					if(Convert.ToInt32(dgrdData[i,ITM_ProductTable.PRODUCTID_FLD]) == Convert.ToInt32(dgrdData[j,ITM_ProductTable.PRODUCTID_FLD]))
					{
						// Dialy
						if (cboType.SelectedIndex == (int)PODeliveryTypeEnum.Daily)
						{
							int iHour = ((DateTime)dgrdData[i,TIME]).Hour;
							int jHour = ((DateTime)dgrdData[j,TIME]).Hour;
							int iMin = ((DateTime)dgrdData[i,TIME]).Minute;
							int jMin = ((DateTime)dgrdData[j,TIME]).Minute;
							if((iHour == jHour) && (iMin == jMin))
							{
								PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY,MessageBoxButtons.OK,MessageBoxIcon.Error);
								dgrdData.Row = j; 
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[TIME]);
								return false;
							}
						}
						else if (cboType.SelectedIndex == (int)PODeliveryTypeEnum.Weekly) // Weekly
						{
							int iHour = ((DateTime)dgrdData[i,TIME]).Hour;
							int jHour = ((DateTime)dgrdData[j,TIME]).Hour;
							int iMin = ((DateTime)dgrdData[i,TIME]).Minute;
							int jMin = ((DateTime)dgrdData[j,TIME]).Minute;
							int iWeek = Convert.ToInt32(dgrdData[i,PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD]);
							int jWeek = Convert.ToInt32(dgrdData[j,PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD]);
							if((iHour == jHour) && (iMin == jMin) && (iWeek==jWeek))
							{
								PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY,MessageBoxButtons.OK,MessageBoxIcon.Error);
								dgrdData.Row = j; 
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[TIME]);
								return false;
							}
						}
						else if (cboType.SelectedIndex == (int)PODeliveryTypeEnum.Monthly) // Monthly
						{
							int iHour = ((DateTime)dgrdData[i,TIME]).Hour;
							int jHour = ((DateTime)dgrdData[j,TIME]).Hour;
							int iMin = ((DateTime)dgrdData[i,TIME]).Minute;
							int jMin = ((DateTime)dgrdData[j,TIME]).Minute;
							int iMonth = Convert.ToInt32(dgrdData[i,PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
							int jMonth = Convert.ToInt32(dgrdData[j,PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
							if((iHour == jHour) && (iMin == jMin) && (iMonth==jMonth))
							{
								PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY,MessageBoxButtons.OK,MessageBoxIcon.Error);
								dgrdData.Row = j; 
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[TIME]);
								return false;
							}
						}

					}
				}
                    
			}


			// Update to dstData
			foreach(DataRow drow1 in dstData.Tables[0].Rows)
			{
				if(drow1.RowState == DataRowState.Deleted) continue;
				foreach(DataRow drow2 in dtbData.Rows)
				{
					//if(drow2.RowState == DataRowState.Deleted) continue;
					if(drow2.RowState == DataRowState.Modified)
						if(Convert.ToInt32(drow2[PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD])
							==Convert.ToInt32(drow1[PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD]))
						{
							drow1.ItemArray = drow2.ItemArray;
							drow1[PO_VendorDeliveryScheduleTable.DELHOUR_FLD] = ((DateTime)drow1[TIME]).Hour;
							drow1[PO_VendorDeliveryScheduleTable.DELMIN_FLD] = ((DateTime)drow1[TIME]).Minute;
							//drow2.Delete();
						}
				}
			}
			// Insert new row
			foreach(DataRow drow2 in dtbData.Rows)
			{
				if(drow2.RowState == DataRowState.Added)
				{
					drow2[PO_VendorDeliveryScheduleTable.DELHOUR_FLD] = ((DateTime)drow2[TIME]).Hour;
					drow2[PO_VendorDeliveryScheduleTable.DELMIN_FLD] = ((DateTime)drow2[TIME]).Minute;
					dstData.Tables[0].Rows.Add(drow2.ItemArray);
					//drow2.Delete();
				}
			}

			// Delete row
			DataRow[] drowDels = dtbData.Select(string.Empty,string.Empty,DataViewRowState.Deleted);
			foreach(DataRow drow2 in drowDels)
			{
				drow2.RejectChanges();
				int intID = 0;
				if(drow2[PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD] != DBNull.Value)
				{
					intID = Convert.ToInt32(drow2[PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD]);
					DataRow[] drows = dstData.Tables[0].Select(PO_VendorDeliveryScheduleTable.VENDORDELIVERYSCHEDULE_FLD + "=" + intID);
					if(drows.Length > 0 ) drows[0].Delete();
				}
			}

			PODeliveryByHourBO boPODelivery = new PODeliveryByHourBO();
			boPODelivery.UpdateDataSet(dstData);
			dstData = boPODelivery.GetVendorDeliverySchedule();
			FillDataToGrid(Convert.ToInt32(dgrdVendor[dgrdVendor.Row,PO_VendorDeliveryScheduleTable.PARTYID_FLD]),cboType.SelectedIndex);
			PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
			return true;
		}

		private void dgrdData_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
		{
			e.Handled = true;
			e.Continue = true;
		}

		private void dgrdVendor_BeforeRowColChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdVendor_BeforeRowColChange()";
			try
			{
				//if(e != null)
				//if(e.NewRange.r1 != e.OldRange.r1)
			{
				//intOlderRow = e.OldRange.r1;
				if(dtbData.GetChanges() != null)
				{
					foreach(DataRow drow in dtbData.Rows)
					{
						if(drow.RowState == DataRowState.Added ) 
							if(drow[TIME] != DBNull.Value)
							{
								drow[PO_VendorDeliveryScheduleTable.DELHOUR_FLD] = ((DateTime)drow[TIME]).Hour;
								drow[PO_VendorDeliveryScheduleTable.DELMIN_FLD] = ((DateTime)drow[TIME]).Minute;
								drow[PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD] = cboType.SelectedIndex;
								drow[PO_VendorDeliveryScheduleTable.CCNID_FLD] = SystemProperty.CCNID;
								drow[PO_VendorDeliveryScheduleTable.PARTYID_FLD] = dgrdVendor[dgrdVendor.Row, MST_PartyTable.PARTYID_FLD];
							}
					}
				}
			}

				if(dtbData.GetChanges() != null)
					if (dtbData.GetChanges().Rows.Count > 0)
					{
						DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
						if (confirmDialog == DialogResult.Yes)
						{
							//btnSave_Click(sender, e);
							if(!SaveData())
							{
								if(e != null)
								{
									e.Cancel = true;
									//dgrdVendor.Row = e.OldRange.r1;
									return;
								}
							}
						}
						else if(confirmDialog == DialogResult.Cancel)
						{
							if(e != null)
							{
								e.Cancel = true;
								//dgrdVendor.Row = e.OldRange.r1;
							}
							return;
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

		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode) 
			{
				case Keys.F1:
					break;
				case Keys.F2:
					// ClearAllFilter();
					break;
				case Keys.F3:
					// FilterWithCurrentValue(false);
					break;
				case Keys.F4:
					// Open search form
					dgrdData_ButtonClick(null,null);
					break;
				case Keys.F5:
					// ReturnPreviousFilter();
					break;
				case Keys.F6:
					// RowFilter();
					break;
				case Keys.F7:
					break;
				case Keys.F8:
					// SumCurrentColumn();
					break;
				case Keys.F9:
					// ExportDataToExcel();
					break;
				case Keys.F10:
					// PrintDataToPrinter();
					break;
				case Keys.F11:
					// SelectDataFromTable();
					break;
				case Keys.Delete:
					FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
					break;
			}		
		}
		/// <summary>
		/// dgrdVendor_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, April 12 2006</date>
		private void dgrdVendor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdVendor_KeyDown()";
			try
			{
				if ((e.KeyCode == Keys.Up)||(e.KeyCode == Keys.Down))
				{
					dgrdVendor_RowColChange(sender, null);
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
