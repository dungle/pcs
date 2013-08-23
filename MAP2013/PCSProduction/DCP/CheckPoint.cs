using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComProduction.DCP.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for CheckPoint.
	/// </summary>
	public class CheckPoint : System.Windows.Forms.Form
	{
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblCCN;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;

		private DataTable dtbGridLayOut = new DataTable();
		private CheckPointBO boCheckPoint = new CheckPointBO();
		private DataSet dstData;
		const string V_PRODUCT_IN_WO = "V_ProductInWorkCenter";
		private const string MODEL_FLD = "Model";
		private const string UM_FLD = "UM";
		private const string THIS = "PCSProduction.DCP.CheckPoint";
		private const string BY_QUANTITY_FLD = "By Quantity";
		private const string BY_ITEM_FLD = "By Time";
		private bool blnHasError = false;
		private System.Windows.Forms.TextBox txtWorkCenter;
		private System.Windows.Forms.Button btnWorkCenterSearch;
		private System.Windows.Forms.Label lblWorkCenter;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CheckPoint()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CheckPoint));
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.txtWorkCenter = new System.Windows.Forms.TextBox();
			this.btnWorkCenterSearch = new System.Windows.Forms.Button();
			this.lblWorkCenter = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
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
			this.cboCCN.Location = new System.Drawing.Point(550, 6);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(78, 21);
			this.cboCCN.TabIndex = 1;
			this.cboCCN.Text = "CCN";
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
			this.lblCCN.AccessibleDescription = "";
			this.lblCCN.AccessibleName = "";
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(518, 6);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(32, 20);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = "";
			this.dgrdData.AccessibleName = "";
			this.dgrdData.AllowAddNew = true;
			this.dgrdData.AllowDelete = true;
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
			this.dgrdData.Location = new System.Drawing.Point(2, 32);
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
			this.dgrdData.Size = new System.Drawing.Size(626, 392);
			this.dgrdData.TabIndex = 5;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Part Number" +
				"\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Sample Pattern\" DataField=\"SamplePattern\"><ValueItems /><GroupInfo" +
				" /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Sample Rate\" DataField=\"Sampl" +
				"eRate\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
				"=\"Delay Time\" DataField=\"DelayTime\"><ValueItems /><GroupInfo /></C1DataColumn><C" +
				"1DataColumn Level=\"0\" Caption=\"Part Name\" DataField=\"Description\"><ValueItems />" +
				"<GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"UM\" DataField=\"MST_" +
				"UnitOfMeasureCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"Category\" DataField=\"ITM_CategoryCode\"><Val" +
				"ueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGr" +
				"id.Design.ContextWrapper\"><Data>Style58{AlignHorz:Near;}Style59{AlignHorz:Near;}" +
				"RecordSelector{AlignImage:Center;}Style50{}Style51{}Style52{AlignHorz:Near;}Styl" +
				"e53{AlignHorz:Near;}Style54{}Caption{AlignHorz:Center;}Style56{}Normal{Font:Micr" +
				"osoft Sans Serif, 8.25pt;}Selected{ForeColor:HighlightText;BackColor:Highlight;}" +
				"Editor{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;ForeColor:Mar" +
				"oon;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}" +
				"Style47{AlignHorz:Near;}Style46{AlignHorz:Near;ForeColor:Maroon;}Style63{}Style6" +
				"2{}Style61{}Style60{}Style36{}Style4{}OddRow{}Style3{}Style29{AlignHorz:Near;}St" +
				"yle28{AlignHorz:Near;ForeColor:Maroon;}HighlightRow{ForeColor:HighlightText;Back" +
				"Color:Highlight;}Style26{}Style25{}Footer{}Style23{AlignHorz:Near;}Style22{Align" +
				"Horz:Near;}Style21{}Style55{}Group{AlignVert:Center;Border:None,,0, 0, 0, 0;Back" +
				"Color:ControlDark;}Style57{}Inactive{ForeColor:InactiveCaptionText;BackColor:Ina" +
				"ctiveCaption;}EvenRow{BackColor:Aqua;}Style6{}Style27{}Style49{}Style48{}Style24" +
				"{}Style7{}Style8{}Style1{}Style20{}Style5{}Style41{AlignHorz:Near;}Style40{Align" +
				"Horz:Near;ForeColor:Maroon;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1" +
				", 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style42{}Style43{}Style44{}Sty" +
				"le45{}Style9{}Style38{}Style39{}FilterBar{}Style37{}Style34{AlignHorz:Near;ForeC" +
				"olor:Maroon;}Style35{AlignHorz:Near;}Style32{}Style33{}Style30{}Style31{}Style2{" +
				"}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"1" +
				"7\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBord" +
				"er\" RecordSelectorWidth=\"17\" DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" Horizon" +
				"talScrollGroup=\"1\"><ClientRect>0, 0, 622, 388</ClientRect><BorderSide>0</BorderS" +
				"ide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me" +
				"=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=" +
				"\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyl" +
				"e parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><Hi" +
				"ghLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inact" +
				"ive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorSty" +
				"le parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"S" +
				"tyle6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><He" +
				"adingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" />" +
				"<FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
				"le19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle paren" +
				"t=\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single<" +
				"/ColumnDivider><Width>123</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayC" +
				"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style paren" +
				"t=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorSty" +
				"le parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\"" +
				" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><Colu" +
				"mnDivider>DarkGray,Single</ColumnDivider><Width>191</Width><Height>15</Height><D" +
				"CIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\"" +
				" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><V" +
				"isible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>60</Wi" +
				"dth><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style2" +
				"5\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Co" +
				"lumnDivider><Width>57</Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColum" +
				"n><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"S" +
				"tyle1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle p" +
				"arent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><" +
				"GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnDi" +
				"vider>DarkGray,Single</ColumnDivider><Width>44</Width><Height>15</Height><DCIdx>" +
				"5</DCIdx></C1DisplayColumn><C1DisplayColumn><DropDownList>True</DropDownList><He" +
				"adingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" />" +
				"<FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
				"le31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle paren" +
				"t=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single<" +
				"/ColumnDivider><Width>92</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent" +
				"=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><Colum" +
				"nDivider>DarkGray,Single</ColumnDivider><Width>73</Width><Height>15</Height><DCI" +
				"dx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Vis" +
				"ible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>63</Widt" +
				"h><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn></internalCols></C1.Win.C" +
				"1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Styl" +
				"e parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style pa" +
				"rent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style par" +
				"ent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=" +
				"\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent" +
				"=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style par" +
				"ent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles" +
				"><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><D" +
				"efaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 622, 388</ClientArea>" +
				"<PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" m" +
				"e=\"Style15\" /></Blob>";
			// 
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = "";
			this.btnDelete.AccessibleName = "";
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(63, 428);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 7;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.AccessibleDescription = "";
			this.btnSave.AccessibleName = "";
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(2, 428);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClose
			// 
			this.btnClose.AccessibleDescription = "";
			this.btnClose.AccessibleName = "";
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(568, 428);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 9;
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
			this.btnHelp.Location = new System.Drawing.Point(507, 428);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 8;
			this.btnHelp.Text = "&Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// txtWorkCenter
			// 
			this.txtWorkCenter.AccessibleDescription = "";
			this.txtWorkCenter.AccessibleName = "";
			this.txtWorkCenter.Location = new System.Drawing.Point(72, 6);
			this.txtWorkCenter.Name = "txtWorkCenter";
			this.txtWorkCenter.Size = new System.Drawing.Size(119, 20);
			this.txtWorkCenter.TabIndex = 3;
			this.txtWorkCenter.Text = "";
			this.txtWorkCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWorkCenter_KeyDown);
			this.txtWorkCenter.Validating += new System.ComponentModel.CancelEventHandler(this.txtWorkCenter_Validating);
			// 
			// btnWorkCenterSearch
			// 
			this.btnWorkCenterSearch.AccessibleDescription = "";
			this.btnWorkCenterSearch.AccessibleName = "";
			this.btnWorkCenterSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnWorkCenterSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnWorkCenterSearch.Location = new System.Drawing.Point(194, 6);
			this.btnWorkCenterSearch.Name = "btnWorkCenterSearch";
			this.btnWorkCenterSearch.Size = new System.Drawing.Size(24, 20);
			this.btnWorkCenterSearch.TabIndex = 4;
			this.btnWorkCenterSearch.Text = "...";
			this.btnWorkCenterSearch.Click += new System.EventHandler(this.btnWorkCenterSearch_Click);
			// 
			// lblWorkCenter
			// 
			this.lblWorkCenter.AccessibleDescription = "";
			this.lblWorkCenter.AccessibleName = "";
			this.lblWorkCenter.ForeColor = System.Drawing.Color.Maroon;
			this.lblWorkCenter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblWorkCenter.Location = new System.Drawing.Point(2, 6);
			this.lblWorkCenter.Name = "lblWorkCenter";
			this.lblWorkCenter.Size = new System.Drawing.Size(74, 20);
			this.lblWorkCenter.TabIndex = 2;
			this.lblWorkCenter.Text = "Work Center";
			this.lblWorkCenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CheckPoint
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(634, 455);
			this.Controls.Add(this.txtWorkCenter);
			this.Controls.Add(this.btnWorkCenterSearch);
			this.Controls.Add(this.lblWorkCenter);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(640, 480);
			this.Name = "CheckPoint";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Check Point";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CheckPoint_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.CheckPoint_Closing);
			this.Load += new System.EventHandler(this.CheckPoint_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// Save event:
		///		-Validate all data
		///		-Save into DB
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnHasError = true;
				if (!dgrdData.EditActive && ValidateData())
				{
					boCheckPoint.UpdateDataSet(dstData);
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);

					//reload grid form database
					dstData = boCheckPoint.ListByWorkCenterID(int.Parse(cboCCN.SelectedValue.ToString()), int.Parse(txtWorkCenter.Tag.ToString()));
					dgrdData.DataSource = dstData.Tables[0];

					//restore the layout of grid
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					ConfigGrid();
					blnHasError = false;
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

		/// <summary>
		/// Validate data
		/// </summary>
		/// <returns></returns>
		private bool ValidateData()
		{
			try
			{
				//check mandatory
				if (FormControlComponents.CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboCCN.Focus();
					cboCCN.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtWorkCenter))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtWorkCenter.Focus();
					txtWorkCenter.Select();
					return false;
				}

				for (int i =0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, ITM_ProductTable.CODE_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
						dgrdData.Focus();
						return false;
					}
					if (dgrdData[i, PRO_CheckPointTable.SAMPLEPATTERN_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.SAMPLEPATTERN_FLD]);
						dgrdData.Focus();
						return false;
					}
					if (dgrdData[i, PRO_CheckPointTable.SAMPLERATE_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_CHECKPOINT_SAMPLE_RATE, MessageBoxIcon.Warning);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.SAMPLERATE_FLD]);
						dgrdData.Focus();
						return false;
					}
					if (dgrdData[i, PRO_CheckPointTable.DELAYTIME_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_CHECKPOINT_DELAY_TIME, MessageBoxIcon.Warning);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.DELAYTIME_FLD]);
						dgrdData.Focus();
						return false;
					}
				}

				//check dupplicate Workcenter and Item on the grid
				for (int i =0; i <dgrdData.RowCount; i++)
				{
					for (int j = i+1; j <dgrdData.RowCount; j++)
					{
						if ((dgrdData[i, ITM_ProductTable.PRODUCTID_FLD].ToString().Trim() == dgrdData[j, ITM_ProductTable.PRODUCTID_FLD].ToString().Trim())
							&& (dgrdData[i, MST_WorkCenterTable.WORKCENTERID_FLD].ToString().Trim() == dgrdData[j, MST_WorkCenterTable.WORKCENTERID_FLD].ToString().Trim()))
						{
							PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Error);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]);
							dgrdData.Focus();
							return false;
						}
					}
				}
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
			return true;
		}

		/// <summary>
		/// Delete event:
		///		Delete all CheckPoint which has CCNID defined by cboCCNID
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if (dgrdData.EditActive) return;
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					dstData = boCheckPoint.ListByWorkCenterID(int.Parse(cboCCN.SelectedValue.ToString()), int.Parse(txtWorkCenter.Tag.ToString()));
					foreach (DataRow drowData in dstData.Tables[0].Rows)
					{
						drowData.Delete();
					}
					boCheckPoint.UpdateDataSet(dstData);
					dgrdData.DataSource = dstData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					ConfigGrid();
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


		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if (dgrdData.EditActive) return;
			this.Close();
		}


		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}
		
		/// <summary>
		/// Load Form, Set default CCN
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CheckPoint_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".WorkOrder_Load()";
			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					return;
				}

				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				
				//Load CCN and set default CCN
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember	= MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember		= MST_CCNTable.CCNID_FLD;
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
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

		/// <summary>
		/// Reload grid if CCN's value was changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboCCN_SelectedValueChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboCCN_SelectedValueChanged()";
			try
			{
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
		/// Config all columns
		/// </summary>
		private void ConfigGrid()
		{
			try
			{
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Locked = true;
				dgrdData.Splits[0].DisplayColumns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Locked = true;

				dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.DELAYTIME_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.SAMPLERATE_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.SAMPLEPATTERN_FLD].DataColumn.ValueItems.Presentation = PresentationEnum.ComboBox;
				if (dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.SAMPLEPATTERN_FLD].DataColumn.ValueItems.Values.Count > 0)
				{
					dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.SAMPLEPATTERN_FLD].DataColumn.ValueItems.Values.Clear();
				}
				dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.SAMPLEPATTERN_FLD].DataColumn.ValueItems.Values.Add(new ValueItem("1", BY_QUANTITY_FLD));
				dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.SAMPLEPATTERN_FLD].DataColumn.ValueItems.Values.Add(new ValueItem("2", BY_ITEM_FLD));
				dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.SAMPLEPATTERN_FLD].DataColumn.ValueItems.Translate = true;
				dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.SAMPLEPATTERN_FLD].DropDownList = true;
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Fill Item's infor
		/// </summary>
		/// <param name="pdrowData"></param>
		private void FillItemData(DataRow pdrowData)
		{
			const string METHOD_NAME = THIS + ".FillItemData()";
			try
			{
				dgrdData.EditActive = true;
				dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD] = pdrowData[ITM_ProductTable.CODE_FLD].ToString();
				dgrdData[dgrdData.Row, ITM_ProductTable.CCNID_FLD] = pdrowData[ITM_ProductTable.CCNID_FLD].ToString();
				dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = pdrowData[ITM_ProductTable.DESCRIPTION_FLD].ToString();
				dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = pdrowData[ITM_ProductTable.REVISION_FLD].ToString();
				dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.PRODUCTID_FLD] = pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString();
				dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = pdrowData[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].ToString();
				dgrdData[dgrdData.Row, MST_WorkCenterTable.WORKCENTERID_FLD] = pdrowData[MST_WorkCenterTable.WORKCENTERID_FLD];
				dgrdData[dgrdData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = pdrowData[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD];
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
		/// Open search form to select item & operation
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition= new Hashtable();
				if (!btnSave.Enabled) return;
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD])
					|| dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD]))
				{
					//open the search form to select Product
					if (txtWorkCenter.Text.Trim() != string.Empty)
					{
						htbCondition.Add(MST_WorkCenterTable.WORKCENTERID_FLD, txtWorkCenter.Tag);
						htbCondition.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
					}
					else
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_WCDISPATCH_SELECT_WORKCENTER, MessageBoxIcon.Warning);
						txtWorkCenter.Focus();
						return;
					}
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(V_PRODUCT_IN_WO, dgrdData.Columns[dgrdData.Col].DataField, dgrdData[dgrdData.Row, dgrdData.Columns[dgrdData.Col].DataField].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(V_PRODUCT_IN_WO, dgrdData.Columns[dgrdData.Col].DataField, dgrdData.Columns[dgrdData.Columns[dgrdData.Col].DataField].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						FillItemData(drwResult.Row);
					}
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
		
		/// <summary>
		/// udpate data after select on the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
	    private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				if (e.Column.DataColumn.DataField== ITM_ProductTable.CODE_FLD
					|| e.Column.DataColumn.DataField== ITM_ProductTable.DESCRIPTION_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, e.Column.DataColumn.DataField].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, ITM_ProductTable.CODE_FLD]= string.Empty;
						dgrdData[row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.REVISION_FLD] = null;
						dgrdData[row, ITM_ProductTable.PRODUCTID_FLD] = null;
					}
					else
					{
						FillItemData((DataRow) e.Column.DataColumn.Tag);
						return;
					}
				}
				if (e.Column.DataColumn.DataField == MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) || (dgrdData[dgrdData.Row,MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD].ToString() == string.Empty))
					{
						dgrdData[dgrdData.Row, MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_WorkCenterTable.WORKCENTERID_FLD] = null;
						dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD]= string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = null;
						dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = null;
						return;
					}
					else
					{
						if (((DataRow) e.Column.DataColumn.Tag)[MST_WorkCenterTable.WORKCENTERID_FLD].ToString() != dgrdData[dgrdData.Row, MST_WorkCenterTable.WORKCENTERID_FLD].ToString())
						{
							dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD]= string.Empty;
							dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
							dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = string.Empty;
							dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = null;
							dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = null;
						}
						dgrdData[dgrdData.Row, MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD] = ((DataRow) e.Column.DataColumn.Tag)[MST_WorkCenterTable.CODE_FLD];
						dgrdData[dgrdData.Row, MST_WorkCenterTable.WORKCENTERID_FLD] = ((DataRow) e.Column.DataColumn.Tag)[MST_WorkCenterTable.WORKCENTERID_FLD];
					}
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

		/// <summary>
		/// check data before leave from column in the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				switch (e.Column.DataColumn.DataField)
				{
					case ITM_ProductTable.CODE_FLD:
						# region open Product search form 
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							//open the search form to select Product
							if (txtWorkCenter.Text.Trim() != string.Empty)
							{
								htbCriteria.Add(MST_WorkCenterTable.WORKCENTERID_FLD, txtWorkCenter.Tag);
								htbCriteria.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
							}
							else
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_WCDISPATCH_SELECT_WORKCENTER, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCT_IN_WO, ITM_ProductTable.CODE_FLD, e.Column.DataColumn.Value.ToString(), htbCriteria, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;
							}
							else
								e.Cancel = true;
						}
						#endregion
						break;
					case ITM_ProductTable.DESCRIPTION_FLD:
						# region open Product search form 
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							//open the search form to select Product
							if (txtWorkCenter.Text.Trim() != string.Empty)
							{
								htbCriteria.Add(MST_WorkCenterTable.WORKCENTERID_FLD, txtWorkCenter.Tag);
								htbCriteria.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
							}
							else
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_WCDISPATCH_SELECT_WORKCENTER, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCT_IN_WO, ITM_ProductTable.DESCRIPTION_FLD, e.Column.DataColumn.Value.ToString(), htbCriteria, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;
							}
							else
								e.Cancel = true;
						}
						#endregion
						break;
					case MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD:
						#region open Sale Order search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							drwResult = FormControlComponents.OpenSearchForm(MST_WorkCenterTable.TABLE_NAME,MST_WorkCenterTable.CODE_FLD, e.Column.DataColumn.Value.ToString(), htbCriteria, false); 
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;
							}
							else
								e.Cancel = true;
						}
						#endregion
						break;
					case PRO_CheckPointTable.SAMPLEPATTERN_FLD:
						if (e.Column.DataColumn.Text.Trim() != BY_QUANTITY_FLD && e.Column.DataColumn.Text.Trim() != BY_ITEM_FLD)
						{
							e.Column.DataColumn.Value = DBNull.Value;
						}
					break;
				}

				//check sample rate
				if (dgrdData.Splits[0].DisplayColumns[dgrdData.Col].DataColumn.DataField == PRO_CheckPointTable.SAMPLERATE_FLD)
				{
					try
					{
						float fQuantity = float.Parse(dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.SAMPLERATE_FLD].DataColumn.Value.ToString().Trim());
						if (fQuantity <= 0.0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_CHECKPOINT_SAMPLE_RATE, MessageBoxIcon.Error);
							e.Cancel = true;
						}
					}
					catch
					{
						//cancel update and throw PCSException
						e.Cancel = true;
						PCSMessageBox.Show(ErrorCode.MESSAGE_WO_ORDERQUANTITY, MessageBoxIcon.Error);
					}
				}

				//check delay time
				if (dgrdData.Splits[0].DisplayColumns[dgrdData.Col].DataColumn.DataField == PRO_CheckPointTable.DELAYTIME_FLD)
				{
					try
					{
						float fQuantity = float.Parse(dgrdData.Splits[0].DisplayColumns[PRO_CheckPointTable.DELAYTIME_FLD].DataColumn.Value.ToString().Trim());
						if (fQuantity <= 0.0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_CHECKPOINT_DELAY_TIME, MessageBoxIcon.Error);
							e.Cancel = true;
						}
					}
					catch
					{
						//cancel update and throw PCSException
						e.Cancel = true;
						PCSMessageBox.Show(ErrorCode.MESSAGE_WO_ORDERQUANTITY, MessageBoxIcon.Error);
					}
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

		/// <summary>
		/// Check state of form and close form 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CheckPoint_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".CheckPoint_Closing()";
			try
			{
				if (dstData != null)
				if (dstData.GetChanges() != null)
				{
					DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					switch (confirmDialog)
					{
						case DialogResult.Yes:
							//Save before exit
							btnSave_Click(btnSave, new EventArgs());
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

		private void CheckPoint_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".CheckPoint_KeyDown()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.F12:
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]);
						dgrdData.Row = dgrdData.RowCount;
						dgrdData.Focus();
						break;
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

		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.F4:
						if (btnSave.Enabled)
						{
							dgrdData_ButtonClick(sender, null);
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

		private void dgrdData_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
		}

		private void btnWorkCenterSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = ".btnWorkCenterSearch_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbCondition.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_WorkCenterTable.TABLE_NAME, MST_WorkCenterTable.CODE_FLD, txtWorkCenter.Text.Trim(), htbCondition, true); 
				if (drwResult != null)
				{
					txtWorkCenter.Text = drwResult[MST_WorkCenterTable.CODE_FLD].ToString();
					txtWorkCenter.Tag = drwResult[MST_WorkCenterTable.WORKCENTERID_FLD].ToString();
					
					dstData = boCheckPoint.ListByWorkCenterID(int.Parse(cboCCN.SelectedValue.ToString()), int.Parse(txtWorkCenter.Tag.ToString()));
					dgrdData.DataSource = dstData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					ConfigGrid();
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

		private void txtWorkCenter_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = ".txtWorkCenter_Validating()";
			try
			{
				if (!txtWorkCenter.Modified) return;
				if (txtWorkCenter.Text.Trim() == string.Empty)
				{
					dstData = boCheckPoint.ListByWorkCenterID(0, 0);
					dgrdData.DataSource = dstData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					ConfigGrid();
					return;
				}
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbCondition.Add(PRO_CheckPointTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					e.Cancel = true;
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_WorkCenterTable.TABLE_NAME, MST_WorkCenterTable.CODE_FLD, txtWorkCenter.Text.Trim(), htbCondition, false); 
				if (drwResult != null)
				{
					txtWorkCenter.Text = drwResult[MST_WorkCenterTable.CODE_FLD].ToString();
					txtWorkCenter.Tag = drwResult[MST_WorkCenterTable.WORKCENTERID_FLD].ToString();
					dstData = boCheckPoint.ListByWorkCenterID(int.Parse(cboCCN.SelectedValue.ToString()), int.Parse(txtWorkCenter.Tag.ToString()));
					dgrdData.DataSource = dstData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					ConfigGrid();
				}
				else
					e.Cancel = true;
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

		private void txtWorkCenter_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = ".txtWorkCenter_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnWorkCenterSearch_Click(btnWorkCenterSearch, new EventArgs());
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
	}
}
