using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1TrueDBGrid;
using PCSComProduction.DCP.BO;
using PCSComProduction.DCP.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using AddNewModeEnum = C1.Win.C1TrueDBGrid.AddNewModeEnum;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using ColEventArgs = C1.Win.C1TrueDBGrid.ColEventArgs;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for ManualProductionPlanning.
	/// </summary>
	public class ManualProductionPlanning : Form
	{
		private TextBox txtProductionLine;
		private Label lblProductionLine;
		private Button btnProductionLine;
		private C1Combo cboCCN;
		private Button btnCycleSearch;
		private TextBox txtCycle;
		private Label lblCycle;
		private Label lblCCN;
		private C1TrueDBGrid dgrdData;
		private Button btnSave;
		private Button btnClose;
		private Button btnHelp;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public const string THIS = "PCSProduction.DCP.ManualProductionPlanning";
		private DataTable dtbGridLayOut;
		private int intCCNID = 0;
		private Button btnGenerate;
		private Button btnSearch;
		private C1DateEdit dtmDate; 
		private DataSet dstData;
		private Button btnCutOver;
		private Button btnIgnore;
		DataTable dtbProductInfo;
		DataTable dtbIgnoreList = new DataTable();
		Thread thrProcess = null;
		private System.Windows.Forms.Button btnCalculateTime;
		DataTable dtbBeginStock = new DataTable();

		public ManualProductionPlanning()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ManualProductionPlanning));
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.btnProductionLine = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.btnCycleSearch = new System.Windows.Forms.Button();
			this.txtCycle = new System.Windows.Forms.TextBox();
			this.lblCycle = new System.Windows.Forms.Label();
			this.lblCCN = new System.Windows.Forms.Label();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.btnSearch = new System.Windows.Forms.Button();
			this.dtmDate = new C1.Win.C1Input.C1DateEdit();
			this.btnCutOver = new System.Windows.Forms.Button();
			this.btnIgnore = new System.Windows.Forms.Button();
			this.btnCalculateTime = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmDate)).BeginInit();
			this.SuspendLayout();
			// 
			// txtProductionLine
			// 
			this.txtProductionLine.Location = new System.Drawing.Point(90, 29);
			this.txtProductionLine.MaxLength = 24;
			this.txtProductionLine.Name = "txtProductionLine";
			this.txtProductionLine.Size = new System.Drawing.Size(126, 20);
			this.txtProductionLine.TabIndex = 6;
			this.txtProductionLine.Text = "";
			this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
			// 
			// lblProductionLine
			// 
			this.lblProductionLine.ForeColor = System.Drawing.Color.Maroon;
			this.lblProductionLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblProductionLine.Location = new System.Drawing.Point(4, 29);
			this.lblProductionLine.Name = "lblProductionLine";
			this.lblProductionLine.Size = new System.Drawing.Size(86, 21);
			this.lblProductionLine.TabIndex = 5;
			this.lblProductionLine.Text = "Production Line";
			this.lblProductionLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnProductionLine
			// 
			this.btnProductionLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnProductionLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnProductionLine.Location = new System.Drawing.Point(218, 29);
			this.btnProductionLine.Name = "btnProductionLine";
			this.btnProductionLine.Size = new System.Drawing.Size(24, 20);
			this.btnProductionLine.TabIndex = 7;
			this.btnProductionLine.Text = "...";
			this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
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
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(558, 4);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(94, 21);
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
				"th>17</DefaultRecSelWidth></Blob>";
			// 
			// btnCycleSearch
			// 
			this.btnCycleSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCycleSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCycleSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCycleSearch.Location = new System.Drawing.Point(218, 4);
			this.btnCycleSearch.Name = "btnCycleSearch";
			this.btnCycleSearch.Size = new System.Drawing.Size(24, 20);
			this.btnCycleSearch.TabIndex = 4;
			this.btnCycleSearch.Text = "...";
			this.btnCycleSearch.Click += new System.EventHandler(this.btnCycleSearch_Click);
			// 
			// txtCycle
			// 
			this.txtCycle.Location = new System.Drawing.Point(90, 4);
			this.txtCycle.MaxLength = 20;
			this.txtCycle.Name = "txtCycle";
			this.txtCycle.Size = new System.Drawing.Size(126, 20);
			this.txtCycle.TabIndex = 3;
			this.txtCycle.Text = "";
			this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCycle_KeyDown);
			this.txtCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtCycle_Validating);
			// 
			// lblCycle
			// 
			this.lblCycle.AccessibleDescription = "";
			this.lblCycle.AccessibleName = "";
			this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
			this.lblCycle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCycle.Location = new System.Drawing.Point(4, 4);
			this.lblCycle.Name = "lblCycle";
			this.lblCycle.Size = new System.Drawing.Size(86, 20);
			this.lblCycle.TabIndex = 2;
			this.lblCycle.Text = "Cycle";
			this.lblCycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCCN
			// 
			this.lblCCN.AccessibleDescription = "";
			this.lblCCN.AccessibleName = "";
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(522, 4);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(29, 20);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = "";
			this.dgrdData.AccessibleName = "";
			this.dgrdData.AllowDelete = true;
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.FilterBar = true;
			this.dgrdData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.dgrdData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(4, 54);
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
			this.dgrdData.Size = new System.Drawing.Size(648, 362);
			this.dgrdData.TabIndex = 10;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Part No\" Da" +
				"taField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
				" Caption=\"Part Name\" DataField=\"Description\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"Start Time\" DataField=\"StartTime\"><Value" +
				"Items /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"End Time\" D" +
				"ataField=\"EndTime\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Quantity\" DataField=\"Quantity\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"Shift\" DataField=\"ShiftDesc\"><ValueItems" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Safety Stock Amo" +
				"unt\" DataField=\"SafetyStockAmount\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextW" +
				"rapper\"><Data>Style58{AlignHorz:Near;}Style59{AlignHorz:Near;}RecordSelector{Ali" +
				"gnImage:Center;}Style50{}Style51{}Style52{AlignHorz:Near;}Style53{AlignHorz:Near" +
				";}Style54{}Caption{AlignHorz:Center;}Style56{}Normal{Font:Tahoma, 11world;}Selec" +
				"ted{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style31{}Style18{}Style" +
				"19{}Style14{}Style15{}Style16{AlignHorz:Near;ForeColor:Maroon;}Style17{AlignHorz" +
				":Near;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style47{AlignHorz:Near" +
				";}Style46{AlignHorz:Near;ForeColor:Maroon;}Style63{}Style62{}Style61{}Style60{}S" +
				"tyle38{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style37{}Style" +
				"34{AlignHorz:Near;ForeColor:Maroon;}Style35{AlignHorz:Near;}Style32{}Style33{}Od" +
				"dRow{}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;ForeColor:Maroon;}Style27{}" +
				"Style26{}Style25{}Footer{}Style23{AlignHorz:Near;}Style22{AlignHorz:Near;ForeCol" +
				"or:Maroon;}Style21{}Style55{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0" +
				";AlignVert:Center;}Style57{}Inactive{ForeColor:InactiveCaptionText;BackColor:Ina" +
				"ctiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;AlignVert:Center;Border:" +
				"Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style49{}Style48{}St" +
				"yle24{}Style20{}Style41{AlignHorz:Near;}Style40{AlignHorz:Near;ForeColor:Maroon;" +
				"}Style43{}FilterBar{}Style42{}Style45{}Style44{}Style9{}Style8{}Style39{}Style36" +
				"{}Style5{}Style4{}Style7{}Style6{}Style1{}Style30{}Style3{}Style2{}</Data></Styl" +
				"es><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCapti" +
				"onHeight=\"17\" ColumnFooterHeight=\"17\" FilterBar=\"True\" MarqueeStyle=\"DottedCellB" +
				"order\" RecordSelectorWidth=\"17\" DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" Hori" +
				"zontalScrollGroup=\"1\"><ClientRect>0, 0, 644, 358</ClientRect><BorderSide>0</Bord" +
				"erSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\"" +
				" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle pare" +
				"nt=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupS" +
				"tyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" />" +
				"<HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"In" +
				"active\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelector" +
				"Style parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me" +
				"=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn>" +
				"<HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\"" +
				" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"" +
				"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle pa" +
				"rent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider>DarkGray,Sing" +
				"le</ColumnDivider><Width>122</Width><Height>15</Height><Button>True</Button><DCI" +
				"dx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Vis" +
				"ible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>151</Wid" +
				"th><Height>15</Height><Button>True</Button><DCIdx>1</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1" +
				"\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>7</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent" +
				"=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><Colum" +
				"nDivider>DarkGray,Single</ColumnDivider><Width>122</Width><Height>15</Height><DC" +
				"Idx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
				"=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" " +
				"me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle par" +
				"ent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Vi" +
				"sible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>137</Wi" +
				"dth><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style4" +
				"3\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Co" +
				"lumnDivider><Width>115</Width><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColu" +
				"mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"" +
				"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle " +
				"parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnD" +
				"ivider>DarkGray,Single</ColumnDivider><Width>113</Width><Height>15</Height><DCId" +
				"x>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"" +
				"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me" +
				"=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle paren" +
				"t=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visi" +
				"ble>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Heig" +
				"ht><Button>True</Button><DCIdx>5</DCIdx></C1DisplayColumn></internalCols></C1.Wi" +
				"n.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><S" +
				"tyle parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style" +
				" parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style " +
				"parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style pare" +
				"nt=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style par" +
				"ent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style " +
				"parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedSty" +
				"les><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout" +
				"><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 644, 358</ClientAr" +
				"ea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"" +
				"\" me=\"Style15\" /></Blob>";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(4, 422);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 11;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(594, 422);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 16;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(532, 422);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 15;
			this.btnHelp.Text = "&Help";
			// 
			// btnGenerate
			// 
			this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGenerate.Location = new System.Drawing.Point(454, 422);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(76, 23);
			this.btnGenerate.TabIndex = 14;
			this.btnGenerate.Text = "&Generate";
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(244, 29);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(60, 20);
			this.btnSearch.TabIndex = 8;
			this.btnSearch.Text = "S&earch";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// dtmDate
			// 
			this.dtmDate.CustomFormat = "dd-MM-yyyy h:mm tt";
			this.dtmDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmDate.Location = new System.Drawing.Point(261, 214);
			this.dtmDate.Name = "dtmDate";
			this.dtmDate.Size = new System.Drawing.Size(134, 20);
			this.dtmDate.TabIndex = 17;
			this.dtmDate.Tag = null;
			this.dtmDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			// 
			// btnCutOver
			// 
			this.btnCutOver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCutOver.Location = new System.Drawing.Point(376, 422);
			this.btnCutOver.Name = "btnCutOver";
			this.btnCutOver.TabIndex = 13;
			this.btnCutOver.Text = "&Cut Over";
			this.btnCutOver.Click += new System.EventHandler(this.btnCutOver_Click);
			// 
			// btnIgnore
			// 
			this.btnIgnore.Location = new System.Drawing.Point(306, 29);
			this.btnIgnore.Name = "btnIgnore";
			this.btnIgnore.Size = new System.Drawing.Size(140, 20);
			this.btnIgnore.TabIndex = 9;
			this.btnIgnore.Text = "Ignore Production Line";
			this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
			// 
			// btnCalculateTime
			// 
			this.btnCalculateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCalculateTime.Location = new System.Drawing.Point(66, 422);
			this.btnCalculateTime.Name = "btnCalculateTime";
			this.btnCalculateTime.Size = new System.Drawing.Size(94, 23);
			this.btnCalculateTime.TabIndex = 12;
			this.btnCalculateTime.Text = "Calculate &Time";
			this.btnCalculateTime.Click += new System.EventHandler(this.btnCalculateTime_Click);
			// 
			// ManualProductionPlanning
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(656, 449);
			this.Controls.Add(this.btnIgnore);
			this.Controls.Add(this.btnCutOver);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.btnGenerate);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.txtProductionLine);
			this.Controls.Add(this.txtCycle);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.btnProductionLine);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.btnCycleSearch);
			this.Controls.Add(this.lblCycle);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.dtmDate);
			this.Controls.Add(this.btnCalculateTime);
			this.KeyPreview = true;
			this.Name = "ManualProductionPlanning";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Manual Production Planning";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ManualProductionPlanning_Closing);
			this.Load += new System.EventHandler(this.ManualProductionPlanning_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmDate)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ManualProductionPlanning_Load(object sender, EventArgs e)
		{		
			const string METHOD_NAME = THIS + ".ManualProductionPlanning_Load()";
			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					return;
				}
				

				//Load CCN and set default CCN
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember	= MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember		= MST_CCNTable.CCNID_FLD;
				
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);

				
				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);

				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}


				//load data structure
				DCRegenerateBO boDCRegenerate = new DCRegenerateBO();
				dstData = boDCRegenerate.GetDCPResultData(0,0,true);
				dgrdData.DataSource = dstData.Tables[0];

				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);

				#region dungla: build ignore table schema
				dtbIgnoreList.Columns.Add(new DataColumn(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, typeof(int)));
				dtbIgnoreList.Columns.Add(new DataColumn(MST_WorkCenterTable.WORKCENTERID_FLD, typeof(int)));
				#endregion
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();		
		}

		private void btnCycleSearch_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycle_Click()";
			try
			{
				DataRowView drwResult = null;
				// Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					intCCNID = int.Parse(cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				Hashtable hshCondition = new Hashtable();
				hshCondition.Add(MST_CCNTable.CCNID_FLD, intCCNID);
				drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME,PRO_DCOptionMasterTable.CYCLE_FLD,txtCycle.Text,hshCondition, true);
				if (drwResult != null)
				{
					txtCycle.Text = drwResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					txtCycle.Tag = drwResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString();
				}
				else
				{
					txtCycle.Focus();
					txtCycle.SelectAll();
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

		private void btnProductionLine_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
			
			try
			{
				SelectProductionLine(METHOD_NAME, true);
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

		private bool SelectProductionLine(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			UtilsBO boUtil = new UtilsBO();
			Hashtable htbCriteria = new Hashtable();				

			//Call OpenSearchForm for selecting Production Line
			DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), htbCriteria, pblnAlwaysShowDialog);
			
			//If has Production Line matched searching condition, fill values to form's controls
			if(drwResult != null)
			{
				txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
				txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];

				//Reset Modified status
				txtProductionLine.Modified = false;
			}
			else if(!pblnAlwaysShowDialog)
			{
				txtProductionLine.Focus();
				return false;
			}

			return true;
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{		
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			
			try
			{
				int intDCOptionMasterID = Convert.ToInt32(txtCycle.Tag);
				if (intDCOptionMasterID <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtCycle.Focus();
					return;
				}
				int intProductionLineID = Convert.ToInt32(txtProductionLine.Tag);
				if (intProductionLineID <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtProductionLine.Focus();
					return;
				}

				dtbProductInfo = (new PRO_DCOptionMasterDS()).GetProductInfoTable(intProductionLineID);

				//load data
				DCRegenerateBO boDCRegenerate = new DCRegenerateBO();
				dstData = boDCRegenerate.GetDCPResultData(intProductionLineID,intDCOptionMasterID,false);
				dgrdData.DataSource = dstData.Tables[0];

				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Locked = true;

				dgrdData.Columns[PRO_DCPResultDetailTable.STARTTIME_FLD].Editor = dtmDate;
				dgrdData.Columns[PRO_DCPResultDetailTable.STARTTIME_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				dgrdData.Columns[PRO_DCPResultDetailTable.ENDTIME_FLD].Editor = dtmDate;
				dgrdData.Columns[PRO_DCPResultDetailTable.ENDTIME_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				dgrdData.Columns[PRO_DCPResultDetailTable.QUANTITY_FLD].NumberFormat = Constants.INTERGER_NUMBERFORMAT;

				dgrdData.AllowAddNew = true;
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

		private void dgrdData_ButtonClick(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				if ((e.Column.DataColumn.DataField == ITM_ProductTable.CODE_FLD) || (e.Column.DataColumn.DataField == ITM_ProductTable.DESCRIPTION_FLD))
				{
					Hashtable htbCriteria = new Hashtable();	
					htbCriteria.Add(ITM_ProductTable.PRODUCTIONLINEID_FLD,txtProductionLine.Tag);

					//Call OpenSearchForm for selecting product
					DataRowView drwResult;
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD].ToString().Trim(), htbCriteria, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, dgrdData.Columns[ITM_ProductTable.CODE_FLD].Text.Trim(), htbCriteria, true);
					}
				
					if(drwResult != null)
					{
						FillItemData(drwResult,e.Column.DataColumn.DataField);
					}
				}
				else if (e.Column.DataColumn.DataField == PRO_ShiftTable.SHIFTDESC_FLD)
				{
					Hashtable htbCriteria = new Hashtable();	
					DataRowView drwResult;
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, dgrdData[dgrdData.Row, PRO_ShiftTable.SHIFTDESC_FLD].ToString().Trim(), htbCriteria, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD].Text.Trim(), htbCriteria, true);
					}
				
					if(drwResult != null)
					{
						FillShiftData(drwResult);
					}
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
		}

		private void FillItemData(DataRowView pdrowData, string pstrDataField)
		{			
			int i = dgrdData.Row;

			dgrdData.EditActive = true;
			dgrdData[i, ITM_ProductTable.PRODUCTID_FLD] = pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString();
			dgrdData[i, ITM_ProductTable.CODE_FLD] = pdrowData[ITM_ProductTable.CODE_FLD].ToString();
			dgrdData[i, ITM_ProductTable.DESCRIPTION_FLD] = pdrowData[ITM_ProductTable.DESCRIPTION_FLD].ToString();
			dgrdData[i, ITM_ProductTable.REVISION_FLD] = pdrowData[ITM_ProductTable.REVISION_FLD].ToString();
			//dgrdData[i, MST_UnitOfMeasureTable.CODE_FLD] = pdrowData[ITM_ProductTable.REVISION_FLD].ToString();

			dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[pstrDataField]);
			dgrdData.Focus();
		}

		private void FillShiftData(DataRowView pdrowData)
		{			
			int i = dgrdData.Row;

			dgrdData.EditActive = true;
			dgrdData[i,PRO_ShiftTable.SHIFTID_FLD] = Convert.ToInt32(pdrowData[PRO_ShiftTable.SHIFTID_FLD]);
			dgrdData[i,PRO_ShiftTable.SHIFTDESC_FLD] = pdrowData[PRO_ShiftTable.SHIFTDESC_FLD].ToString();

			dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD]);
			dgrdData.Focus();
		}

		private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				#region recalculate
				
				double dblVarLT =0;
				double dblMachineNo =0;
				try
				{
					dblVarLT = Convert.ToInt32(dtbProductInfo.Select("ProductID = " + dgrdData.Columns["ProductID"])[0]["LTVariableTime"]);
					dblMachineNo = Convert.ToInt32(dtbProductInfo.Select("ProductID = " + dgrdData.Columns["ProductID"])[0]["MachineNo"]);
				}catch{}
				double dblQuantity = 0;
				try
				{
					dblQuantity = Convert.ToDouble(dgrdData.Columns[PRO_DCPResultDetailTable.QUANTITY_FLD].Value);
				}catch{}

				if(e.Column.DataColumn.DataField == PRO_DCPResultDetailTable.STARTTIME_FLD)
				{
					// update DueDate: End Date Time = Start Date Time + (Quantity * Product.VariableLT/86400)
					if(dgrdData.Columns[PRO_DCPResultDetailTable.STARTTIME_FLD].Value != DBNull.Value)
					{
						dgrdData.Columns[PRO_DCPResultDetailTable.ENDTIME_FLD].Value =
							((DateTime)dgrdData.Columns[PRO_DCPResultDetailTable.STARTTIME_FLD].Value).AddSeconds(dblQuantity * dblVarLT/dblMachineNo);
					}
				}
				else if(e.Column.DataColumn.DataField == PRO_DCPResultDetailTable.ENDTIME_FLD)
				{
					// update StartDate: Start Date Time = End Date Time - (Quantity * Product.VariableLT/86400)
					if(dgrdData.Columns[PRO_DCPResultDetailTable.ENDTIME_FLD].Value != DBNull.Value)
					{
						dgrdData.Columns[PRO_DCPResultDetailTable.STARTTIME_FLD].Value =
							((DateTime)dgrdData.Columns[PRO_DCPResultDetailTable.ENDTIME_FLD].Value).AddSeconds(- dblQuantity * dblVarLT/dblMachineNo);
					}
				}
				else if(e.Column.DataColumn.DataField == PRO_DCPResultDetailTable.QUANTITY_FLD)
				{
					// update DueDate: End Date Time = Start Date Time + (Quantity * Product.VariableLT/86400)
					if(dgrdData.Columns[PRO_DCPResultDetailTable.STARTTIME_FLD].Value != DBNull.Value)
					{
						dgrdData.Columns[PRO_DCPResultDetailTable.ENDTIME_FLD].Value =
							((DateTime)dgrdData.Columns[PRO_DCPResultDetailTable.STARTTIME_FLD].Value).AddSeconds(dblQuantity * dblVarLT/dblMachineNo);
					}
				}
				if(dgrdData.Columns[PRO_DCPResultDetailTable.STARTTIME_FLD].Value != DBNull.Value)				
					dgrdData.Columns[PRO_DCPResultDetailTable.WORKINGDATE_FLD].Value = ((DateTime)dgrdData.Columns[PRO_DCPResultDetailTable.STARTTIME_FLD].Value).Date;
				#endregion
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

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnGenerate_Click()";
			try
			{
				if(txtCycle.Text.Length == 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtCycle.Focus();
					return;
				}
				if(txtProductionLine.Text.Length == 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtProductionLine.Focus();
					return;
				}

				int intDCPOptionMasterID;//, intWorkCenterID, intProductionLineID;
				//DCRegenerateBO boDCRegenerate = new DCRegenerateBO();
				intDCPOptionMasterID = Convert.ToInt32(txtCycle.Tag);
				//intProductionLineID = Convert.ToInt32(txtProductionLine.Tag);
				//intWorkCenterID = boDCRegenerate.GetMainWorkCenterID(intProductionLineID);
				btnSearch_Click(null,null);

				//boDCRegenerate.RunMPPNew(intDCPOptionMasterID, intWorkCenterID, intProductionLineID,dstData, dtbIgnoreList, dtbBeginStock);
				//PCSMessageBox.Show(ErrorCode.MESSAGE_GENERATED_SUCCESSFULLY,MessageBoxIcon.Information,new string[]{"Manual DCP"});

				btnGenerate.Enabled = false;
				thrProcess = new Thread(new ThreadStart(ManualPP));
				thrProcess.Start();
				if (thrProcess.ThreadState == ThreadState.Stopped || !thrProcess.IsAlive)
					thrProcess.Abort();
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

		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				if(txtCycle.Text.Length == 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtCycle.Focus();
					return;
				}
				if(txtProductionLine.Text.Length == 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtProductionLine.Focus();
					return;
				}
				for(int i=0; i < dgrdData.RowCount; i++)
				{
					if(dgrdData[i, ITM_ProductTable.CODE_FLD] == DBNull.Value)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
						dgrdData.Focus();
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]);
						return;
					}
					if(dgrdData[i, PRO_DCPResultDetailTable.STARTTIME_FLD] == DBNull.Value)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
						dgrdData.Focus();
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_DCPResultDetailTable.STARTTIME_FLD]);
						return;
					}
					if(dgrdData[i, PRO_DCPResultDetailTable.ENDTIME_FLD] == DBNull.Value)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
						dgrdData.Focus();
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_DCPResultDetailTable.ENDTIME_FLD]);
						return;
					}
					if(dgrdData[i, PRO_DCPResultDetailTable.QUANTITY_FLD] == DBNull.Value)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
						dgrdData.Focus();
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_DCPResultDetailTable.QUANTITY_FLD]);
						return;
					}
					if(dgrdData[i, PRO_ShiftTable.SHIFTID_FLD] == DBNull.Value)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
						dgrdData.Focus();
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ShiftTable.SHIFTID_FLD]);
						return;
					}
				}


				DCRegenerateBO boDCRegenerate = new DCRegenerateBO();
				int intDCPOptionMasterID, intWorkCenterID, intProductionLineID;
				intDCPOptionMasterID = Convert.ToInt32(txtCycle.Tag);
				intProductionLineID = Convert.ToInt32(txtProductionLine.Tag);
				intWorkCenterID = boDCRegenerate.GetMainWorkCenterID(intProductionLineID);
				boDCRegenerate.SaveManualProductionPlanning(intDCPOptionMasterID,intWorkCenterID,intProductionLineID,dstData.Tables[0] );
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA,MessageBoxIcon.Information);
				// refresh the grid
				btnSearch_Click(null, null);
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

		private void btnCutOver_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCutOver_Click()";
			try
			{
				if(txtCycle.Text.Length == 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtCycle.Focus();
					return;
				}
				if(txtProductionLine.Text.Length == 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtProductionLine.Focus();
					return;
				}
				RoughCutCapacity frmRoughCut = new RoughCutCapacity(Convert.ToInt32(txtProductionLine.Tag), Convert.ToInt32(txtCycle.Tag));
				frmRoughCut.Show();
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
		/// txtCycle_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, September 29 2006</date>
		private void txtCycle_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycle_Click()";
			try
			{
				if (txtCycle.Text == string.Empty)
				{
					txtCycle.Tag = null;
					return;
				}
				if (!txtCycle.Modified) return;
				DataRowView drwResult = null;
				// Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					intCCNID = int.Parse(cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				Hashtable hshCondition = new Hashtable();
				hshCondition.Add(MST_CCNTable.CCNID_FLD, intCCNID);
				drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME,PRO_DCOptionMasterTable.CYCLE_FLD,txtCycle.Text,hshCondition, false);
				if (drwResult != null)
				{
					txtCycle.Text = drwResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					txtCycle.Tag = drwResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString();
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
		/// txtCycle_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, September 29 2006</date>
		private void txtCycle_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				btnCycleSearch_Click(null, null);
			}
		}
		/// <summary>
		/// txtProductionLine_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, September 29 2006</date>
		private void txtProductionLine_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";
			
			try
			{
				SelectProductionLine(METHOD_NAME, false);
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
		/// <summary>
		/// txtProductionLine_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, September 29 2006</date>
		private void txtProductionLine_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				btnProductionLine_Click(null, null);
			}
		}

		private void btnIgnore_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnIgnore_Click()";
			
			try
			{
				IgnoreProductionLine frmIgnore = new IgnoreProductionLine(dtbIgnoreList);
				frmIgnore.ShowDialog();
				dtbIgnoreList = frmIgnore.IgnoreList;
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
		private void ManualPP()
		{
			const string METHOD_NAME = THIS + ".ManualPP()";
			try
			{
				Cursor = Cursors.WaitCursor;

				int intDCPOptionMasterID, intWorkCenterID, intProductionLineID;
				DCRegenerateBO boDCRegenerate = new DCRegenerateBO();
				intDCPOptionMasterID = Convert.ToInt32(txtCycle.Tag);
				intProductionLineID = Convert.ToInt32(txtProductionLine.Tag);
				intWorkCenterID = boDCRegenerate.GetMainWorkCenterID(intProductionLineID);

				#region dungla: calculate begin quantity first

				DateTime dtmServerDate = new UtilsBO().GetDBDate();
				dtbBeginStock.Clear();
				dtbBeginStock = CalculateBeginQuantity(intDCPOptionMasterID, cboCCN.SelectedValue.ToString(), dtmServerDate, dtbIgnoreList);

				#endregion

				//btnSearch_Click(null,null);
				boDCRegenerate.RunMPPNew(intDCPOptionMasterID, intWorkCenterID, intProductionLineID,dstData, dtbIgnoreList, dtbBeginStock);
				PCSMessageBox.Show(ErrorCode.MESSAGE_GENERATED_SUCCESSFULLY,MessageBoxIcon.Information,new string[]{"Manual DCP"});
				btnGenerate.Enabled = true;
				Cursor = Cursors.Default;
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex.Message, METHOD_NAME, Level.DEBUG);
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch (Exception ex)
			{
				string[] strMsg = new string[]{this.Text};
				PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_ROLL_UP, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, strMsg);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			finally
			{
				Cursor = Cursors.Default;
				btnGenerate.Enabled = true;
			}
		}

		private void ManualProductionPlanning_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ManualProductionPlanning_Closing()";
			try
			{
				// ask user to stop the thread
				if (thrProcess != null)
				{
					if (thrProcess.IsAlive || thrProcess.ThreadState == ThreadState.Running)
					{
						string[] strMsg = {this.Text};					
						DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_PROCESS_IS_RUNNING, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, strMsg);
						switch (dlgResult)
						{
							case DialogResult.OK:
								// try to stop the thread
								try
								{
									thrProcess.Abort();
								}
								catch
								{
									e.Cancel = false;
								}
								break;
							case DialogResult.Cancel:
								e.Cancel = true;
								break;
						}
					}
				}
			}
			catch (ThreadAbortException ex)
			{
				Logger.LogMessage(ex.Message, METHOD_NAME, Level.DEBUG);
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
					case Keys.Delete:
						if ((e.KeyCode == Keys.Delete)&&(dgrdData.SelectedRows.Count > 0))
						{
							if (btnSave.Enabled)
							{
								dgrdData.AllowDelete = true;
								FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
							}
						}
						break;
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
		}
		/// <summary>
		/// Calculate begin quantity for all item in seleted production line
		/// </summary>
		/// <param name="voCycle"></param>
		/// <param name="pstrCCNID"></param>
		/// <param name="dtmServerDate"></param>
		/// <param name="pdtbIgnoreList"></param>
		private DataTable CalculateBeginQuantity(int pintCycleID, string pstrCCNID, DateTime dtmServerDate, DataTable pdtbIgnoreList)
		{
			DCPReportBO boReport = new DCPReportBO();

			PRO_DCOptionMasterVO voCycle = (PRO_DCOptionMasterVO)boReport.GetCyclerMasterObject(pintCycleID);
			
			DataTable dtbBeginStock = boReport.GetBeginData(pintCycleID);
			// get planning offset
			DataTable dtbPlanningOffset = boReport.GetPlanningOffset(pstrCCNID);
			// get list of production line
			DataTable dtbListProductionLine = boReport.ListProductionLine();
			StringBuilder sbListLine = new StringBuilder();
			foreach (DataRow drowLine in dtbListProductionLine.Rows)
			{
				// check ignore list
				if (pdtbIgnoreList.Select("ProductionLineID = " + drowLine["ProductionLineID"].ToString()).Length == 0)
					sbListLine.Append(drowLine["ProductionLineID"].ToString()).Append(",");
			}
			sbListLine.Append("0");
			// get list of all product
			DataTable dtbListProduct = boReport.ListProduct(pstrCCNID, sbListLine.ToString());
			// cache quantity
			DataTable dtbCache = boReport.GetBeginNetQuantity(pstrCCNID);
			// delivery for parent
			DataTable dtbDeliveryForParent = boReport.GetDeliveryForParent(pstrCCNID);
			// delivery for SO
			DataTable dtbDeliveryForSO = boReport.GetDeliveryForSO();
			// produce from work order
			DataTable dtbProduce = boReport.GetTotalWO(pstrCCNID);
			// working time
			DataTable dtbWorkingTime = boReport.GetWorkingTime();
			// calculate foreach product
			foreach (DataRow drowProduct in dtbListProduct.Rows)
			{
				string strProductID = drowProduct["ProductID"].ToString();
				string strProductionLineID = drowProduct["ProductionLineID"].ToString();
				
				// refine cycle
				voCycle = RefineCycle(strProductionLineID, dtbPlanningOffset, voCycle);
				// only update for effect month by planning period
				string strFilter = "ProductID = " + strProductID
					+ " AND DCOptionMasterID = " + voCycle.DCOptionMasterID;
				decimal decScrapPercent = Convert.ToDecimal(drowProduct[ITM_ProductTable.SCRAPPERCENT_FLD]);
				// check if product already has data in database, need update
				if (dtbBeginStock.Select(strFilter).Length > 0)
				{
					DataRow drowProductStock = dtbBeginStock.Select(strFilter)[0];
					CalculateQuantity(voCycle, voCycle.PlanningPeriod.Date, dtmServerDate, strProductID, dtbCache,
						ref drowProductStock, dtbProduce, dtbDeliveryForSO,
						dtbDeliveryForParent, dtbWorkingTime, decScrapPercent);
				}
				else // else add new record
				{
					DataRow drowProductStock = dtbBeginStock.NewRow();
					drowProductStock["ProductID"] = strProductID;
					CalculateQuantity(voCycle, voCycle.PlanningPeriod.Date, dtmServerDate, strProductID, dtbCache,
						ref drowProductStock, dtbProduce, dtbDeliveryForSO,
						dtbDeliveryForParent, dtbWorkingTime, decScrapPercent);
					dtbBeginStock.Rows.Add(drowProductStock);
				}
			}
			return dtbBeginStock;
		}
		private DataRow CalculateQuantity(PRO_DCOptionMasterVO voCycle, DateTime dtmEffectDate, DateTime dtmServerDate,
			string strProductID, DataTable dtbCache, ref DataRow drowProductStock, DataTable dtbProduce,
			DataTable dtbDeliveryForSO, DataTable dtbDeliveryForParent, DataTable dtbWorkingTime, decimal pdecScrapPercent)
		{
			decimal decQuantity = 0, decCacheQuantity = 0;

			drowProductStock[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD] = voCycle.DCOptionMasterID;

			#region quantity from cache

			string strFilter = "ProductID = " + strProductID;
			try
			{
				decCacheQuantity = Convert.ToDecimal(dtbCache.Compute("SUM(Quantity)", strFilter));
			}
			catch{}

			#endregion

			// use cache as begin quantity
			if (voCycle.UseCacheAsBegin)
				drowProductStock["Quantity"] = decCacheQuantity;
			else
			{
				#region Delivery for parent and produce

				decimal decDeliveryForParent = 0, decProduce = 0;
				DateTime dtmStartTime = dtmEffectDate;
				DateTime dtmEndTime = dtmEffectDate;
				GetStartAndEndTime(dtmEffectDate, ref dtmStartTime, ref dtmEndTime, dtbWorkingTime);
				string strFilterForParent = "ProductID = '" + strProductID + "' AND"
					+ " StartDate >='" + dtmServerDate.ToString() + "' AND"
					+ " StartDate <'" + dtmStartTime.ToString() + "'";
				try
				{
					decDeliveryForParent += Convert.ToDecimal(dtbDeliveryForParent.Compute("SUM(Quantity)", strFilterForParent));
				}
				catch{}

				#region produce

				string strFilterProduce = "ProductID = '" + strProductID + "' AND"
					+ " DueDate >='" + dtmServerDate.ToString() + "' AND"
					+ " DueDate <'" + dtmStartTime.ToString() + "'";
				try
				{
					decProduce += Convert.ToDecimal(dtbProduce.Compute("SUM(Quantity)", strFilterProduce));
				}
				catch{}

				#endregion

				#endregion
					
				#region Delivery for SO
					
				decimal decDeliveryForSO = 0;
				DateTime dtmStartTimeSO = dtmEffectDate;
				DateTime dtmEndTimeSO = dtmEffectDate;
				GetStartAndEndTime(dtmEffectDate, ref dtmStartTimeSO, ref dtmEndTimeSO, dtbWorkingTime);
				string strFilterSO = "ProductID = " + strProductID + " AND"
					+ " ScheduleDate >='" + dtmServerDate.ToString() + "' AND"
					+ " ScheduleDate <'" + dtmStartTimeSO.ToString() + "'";
				try
				{
					decDeliveryForSO += Convert.ToDecimal(dtbDeliveryForSO.Compute("SUM(Quantity)", strFilterSO));
				}
				catch{}
					
				#endregion 

				// quantity = cache + produce - delivery (so + parent)
				decQuantity = decCacheQuantity + decProduce - (decDeliveryForParent + decDeliveryForSO);

				decQuantity = (decQuantity < 0) ? 0 : decQuantity;
				drowProductStock["Quantity"] = decQuantity * (1 - pdecScrapPercent / 100);
			}

			return drowProductStock;
		}
		private PRO_DCOptionMasterVO RefineCycle(string pstrProductionLineID, DataTable pdtbPlanningOffset, PRO_DCOptionMasterVO voCycle)
		{
			string strFilter = "DCOptionMasterID = " + voCycle.DCOptionMasterID + " AND ProductionLineID = " +
				pstrProductionLineID;
			DataRow[] drowOffset = pdtbPlanningOffset.Select(strFilter);
			// refine as of date of the cycle based on planning offset of current production line
			if (drowOffset.Length > 0)
			{
				if (drowOffset[0]["PlanningStartDate"] != null && drowOffset[0]["PlanningStartDate"] != DBNull.Value)
				{
					DateTime dtmStartDate = (DateTime) drowOffset[0]["PlanningStartDate"];
					voCycle.AsOfDate = new DateTime(dtmStartDate.Year, dtmStartDate.Month, dtmStartDate.Day);
				}
			}
			return voCycle;
		}
		private void GetStartAndEndTime(DateTime pdtmCurrentDay, ref DateTime pdtmStartTime,
			ref DateTime pdtmEndTime, DataTable pdtmWorkingTime)
		{
			DataRow[] drowShifts = pdtmWorkingTime.Select(string.Empty, "WorkTimeFrom ASC");

			if (drowShifts.Length <= 0)
				return;
			//change shift configured day to working day
			pdtmStartTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Hour,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Minute,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Second);
			pdtmEndTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Hour,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Minute,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Second);
			double dblDiff = ((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).
				Subtract((DateTime)drowShifts[0]["WorkTimeFrom"]).Days;
			pdtmEndTime = pdtmEndTime.AddDays(dblDiff);
		}

		private void btnCalculateTime_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCalculateTime_Click()";
			try
			{
				int intDCOptionMasterID = Convert.ToInt32(txtCycle.Tag);
				if (intDCOptionMasterID <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtCycle.Focus();
					return;
				}
				int intProductionLineID = Convert.ToInt32(txtProductionLine.Tag);
				if (intProductionLineID <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtProductionLine.Focus();
					return;
				}
				if (dstData == null || dstData.Tables.Count == 0 || dstData.Tables[0].Rows.Count == 0)
					return;
				this.Cursor = Cursors.WaitCursor;
				DCOptionsBO boOption = new DCOptionsBO();
				DataRow drowCycle = boOption.GetDCOptionMaster(intDCOptionMasterID);
				DateTime dtmFromDate = Convert.ToDateTime(drowCycle[PRO_DCOptionMasterTable.ASOFDATE_FLD]);
				DateTime dtmToDate = dtmFromDate.AddDays(Convert.ToDouble(drowCycle[PRO_DCOptionMasterTable.PLANHORIZON_FLD]));
				DataSet dstTemp = dstData.Copy();
				dstTemp.Tables[0].Columns.Add("Seq", typeof(int));
				DataTable dtbProductSequence = boOption.GetProductSequence(intProductionLineID).Tables[0];
				DataTable dtbShift = boOption.ListShift();
				foreach (DataRow drowTemp in dstTemp.Tables[0].Rows)
				{
					string strProductID = drowTemp[ITM_ProductTable.PRODUCTID_FLD].ToString();
					int intSequence = 0;
					try
					{
						intSequence = Convert.ToInt32(dtbProductSequence.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + strProductID)[0]["Seq"]);
					}
					catch{}
					drowTemp["Seq"] = intSequence;
				}
				DCPReportBO boReport = new DCPReportBO();
				DataTable dtbWorkingTime = boReport.GetWorkingTime(intProductionLineID);
				for (DateTime dtmDay = dtmFromDate; dtmDay <= dtmToDate; dtmDay = dtmDay.AddDays(1))
				{
					string strFilter = "WorkingDate = '" + dtmDay.ToString("G") + "'";
					DataRow[] drowDays = dstTemp.Tables[0].Select(strFilter, "Seq ASC, ProductID ASC");
					DateTime dtmStartDay = dtmDay;
					DateTime dtmEndDay = dtmDay;
					GetStartAndEndTime(dtmDay, ref dtmStartDay, ref dtmEndDay, dtbWorkingTime);
					DateTime dtmEndTime = dtmStartDay;
					for (int i = 0; i < drowDays.Length; i++)
					{
						DataRow drowData = drowDays[i];
						DateTime dtmStartTime = dtmEndTime;
						string strItem = drowData[ITM_ProductTable.PRODUCTID_FLD].ToString();

						int intShiftID = 0;
						
						double dblVarLT =0;
						double dblMachineNo =0;
						try
						{
							dblVarLT = Convert.ToInt32(dtbProductInfo.Select("ProductID = " + strItem)[0]["LTVariableTime"]);
							dblMachineNo = Convert.ToInt32(dtbProductInfo.Select("ProductID = " + strItem)[0]["MachineNo"]);
						}
						catch{}
						double dblQuantity = 0;
						try
						{
							dblQuantity = Convert.ToDouble(drowData[PRO_DCPResultDetailTable.QUANTITY_FLD]);
						}
						catch{}

						double dblTotalTime = dblQuantity * dblVarLT/dblMachineNo;

						dtmEndTime = CalculateEndTime(dtmStartTime, dblTotalTime, dtbWorkingTime, ref intShiftID, dtmDay);
						
						drowData[PRO_DCPResultDetailTable.STARTTIME_FLD] = dtmStartTime;
						drowData[PRO_DCPResultDetailTable.ENDTIME_FLD] = dtmEndTime;
						drowData[PRO_DCPResultDetailTable.SHIFTID_FLD] = intShiftID;
						drowData[PRO_ShiftTable.SHIFTDESC_FLD] = dtbShift.Select(PRO_ShiftTable.SHIFTID_FLD + "=" + intShiftID)[0][PRO_ShiftTable.SHIFTDESC_FLD];
					}
				}
				dstData = dstTemp;
				dgrdData.DataSource = dstData.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Locked = true;

				dgrdData.Columns[PRO_DCPResultDetailTable.STARTTIME_FLD].Editor = dtmDate;
				dgrdData.Columns[PRO_DCPResultDetailTable.STARTTIME_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				dgrdData.Columns[PRO_DCPResultDetailTable.ENDTIME_FLD].Editor = dtmDate;
				dgrdData.Columns[PRO_DCPResultDetailTable.ENDTIME_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				dgrdData.Columns[PRO_DCPResultDetailTable.QUANTITY_FLD].NumberFormat = Constants.INTERGER_NUMBERFORMAT;
//				DCRegenerateBO boDCRegenerate = new DCRegenerateBO();
//				int intWorkCenterID;
//				intProductionLineID = Convert.ToInt32(txtProductionLine.Tag);
//				intWorkCenterID = Convert.ToInt32(dtbWorkingTime.Rows[0][MST_WorkCenterTable.WORKCENTERID_FLD]);
//				boDCRegenerate.SaveManualProductionPlanning(intDCOptionMasterID,intWorkCenterID,intProductionLineID,dstTemp.Tables[0] );
//				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA,MessageBoxIcon.Information);
				// refresh the grid
//				btnSearch_Click(null, null);
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}
		private DateTime CalculateEndTime(DateTime pdtmStartTime, double pdblTotalTime, DataTable pdtbWorkingTime, ref int pintShiftID, DateTime pdtmDay)
		{
			DataRow[] arrShifts = pdtbWorkingTime.Select(PRO_WCCapacityTable.BEGINDATE_FLD + " <= '" + pdtmDay.Date + "' AND " + PRO_WCCapacityTable.ENDDATE_FLD + " >= '" + pdtmDay.Date + "'",PRO_ShiftPatternTable.WORKTIMEFROM_FLD + " ASC");
			ArrayList arrTimeMarks = new ArrayList();

			if (arrShifts.Length == 0)
				return DateTime.MinValue;
			foreach (DataRow drowShift in arrShifts)
			{
				int intShiftID = Convert.ToInt32(drowShift[PRO_ShiftPatternTable.SHIFTID_FLD]);
				DateTime dtmWorkTimeFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.WORKTIMEFROM_FLD]);
				DateTime dtmWorkTimeTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.WORKTIMETO_FLD]);
				arrTimeMarks.Add(new TimeMark(dtmWorkTimeFrom,1,intShiftID,false));
				arrTimeMarks.Add(new TimeMark(dtmWorkTimeTo,-1,intShiftID,true));
				try
				{
					DateTime dtmRefreshingFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REFRESHINGFROM_FLD]);
					DateTime dtmRefreshingTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REFRESHINGTO_FLD]);
					arrTimeMarks.Add(new TimeMark(dtmRefreshingFrom,-1,intShiftID,false));
					arrTimeMarks.Add(new TimeMark(dtmRefreshingTo,1,intShiftID,false));
				}
				catch {}
				try
				{
					DateTime dtmRegularStopFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD]);
					DateTime dtmRegularStopTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.REGULARSTOPTO_FLD]);
					arrTimeMarks.Add(new TimeMark(dtmRegularStopFrom,-1,intShiftID,false));
					arrTimeMarks.Add(new TimeMark(dtmRegularStopTo,1,intShiftID,false));
				}
				catch {}
				try
				{
					DateTime dtmExtraStopFrom = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD]);
					DateTime dtmExtraStopTo = Convert.ToDateTime(drowShift[PRO_ShiftPatternTable.EXTRASTOPTO_FLD]);
					arrTimeMarks.Add(new TimeMark(dtmExtraStopFrom,-1,intShiftID,false));
					arrTimeMarks.Add(new TimeMark(dtmExtraStopTo,1,intShiftID,false));
				}
				catch {}
			}
			arrTimeMarks.Sort(new TimeMarkComparer());

			double dblObtainedTime = 0;
			bool blnNote = false;
			DateTime dtmCheckPoint = pdtmStartTime.AddDays(-pdtmStartTime.Date.Subtract(Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Date).Days);
			if (dtmCheckPoint < Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD])) 
			{
				dtmCheckPoint = dtmCheckPoint.AddDays(1);
				blnNote = true;
			}
			foreach (TimeMark objTimeMark in arrTimeMarks)
			{
				if ((objTimeMark.m_dtmTime < dtmCheckPoint) || ((objTimeMark.m_dtmTime == dtmCheckPoint) && (objTimeMark.m_blnEndOfShift)))
				{
					continue;
				}
				if (objTimeMark.m_shtFlag == -1)
				{
					dblObtainedTime += objTimeMark.m_dtmTime.Subtract(dtmCheckPoint).TotalSeconds;
					dtmCheckPoint = objTimeMark.m_dtmTime;
				}
				else if (objTimeMark.m_shtFlag == 1)
				{
					dtmCheckPoint = objTimeMark.m_dtmTime;
				}
				else
				{
					//Error, never occurs
				}
				if (dblObtainedTime >= pdblTotalTime)
				{
					pintShiftID = objTimeMark.m_intShiftID;
					return dtmCheckPoint.AddSeconds(pdblTotalTime - dblObtainedTime).AddDays(pdtmStartTime.Date.Subtract(Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Date).Days);
				}
				else if (objTimeMark.m_blnEndOfShift)
				{
					pintShiftID = objTimeMark.m_intShiftID;
					return dtmCheckPoint.AddDays(pdtmStartTime.Date.Subtract(Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Date).Days);
				}		
			}
			DateTime dtmReturn = dtmCheckPoint.AddSeconds(pdblTotalTime - dblObtainedTime).AddDays(pdtmStartTime.Date.Subtract(Convert.ToDateTime(arrShifts[0][PRO_ShiftPatternTable.WORKTIMEFROM_FLD]).Date).Days);
			if (blnNote)
			{
				dtmReturn = dtmReturn.AddDays(-1);
			}
			return dtmReturn;
		}
		private class TimeMark
		{
			public TimeMark(DateTime pdtmTime, short pshtFlag,int pintShiftID,bool p_blnEndOfShift)
			{
				m_dtmTime = pdtmTime;
				m_shtFlag = pshtFlag;
				m_intShiftID = pintShiftID;
				m_blnEndOfShift = p_blnEndOfShift;
			}
			public DateTime m_dtmTime;
			public short m_shtFlag;
			public int m_intShiftID;
			public bool m_blnEndOfShift;
		}

		private class TimeMarkComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				if (((TimeMark)x).m_dtmTime > ((TimeMark)y).m_dtmTime) 
				{
					return 1;
				}
				else if (((TimeMark)x).m_dtmTime < ((TimeMark)y).m_dtmTime) 
				{
					return -1;
				}
				else
				{
					if (((TimeMark)x).m_shtFlag > ((TimeMark)y).m_shtFlag)
					{
						return 1;
					}
					else if (((TimeMark)x).m_shtFlag < ((TimeMark)y).m_shtFlag)
					{
						return -1;
					}
					else
					{
						return 0;
					}
				}
			}
		}
	}
}
