using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using PCSComProduction.DCP.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for ImportPlanData.
	/// </summary>
	public class ImportPlanData : Form
	{
		const string THIS = "PCSProduction.DCP.ImportPlanData";
		private ComboBox cboSheetnames;
		private TextBox txtRange;
		private Button btnOpenFileDlg;
		private TextBox txtFileName;
		private Label lblRange;
		private Label lblFileName;
		private Label lblSheet;
		private Button btnSave;
		private Button btnClose;
		private Button btnGetData;
		private C1TrueDBGrid dgrdData;
		private TextBox txtCycle;
		private Button btnSearchCycle;
		private Label lblCycle;
		private C1DateEdit dtmMonth;
		private Label lblMonth;
		private TextBox txtProductionLine;
		private Label lblProductionLine;
		private Button btnProductionLine;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		private string strExcelFilename = "";
		private ExcelReader objExcelReader = null;
		private DataTable dtbData;
		private System.Windows.Forms.TextBox txtShiftCode;
		private System.Windows.Forms.Button btnShiftCode;
		private System.Windows.Forms.Label lblShiftCode;
		private DataTable dtbGridLayout = new DataTable();
		ImportPlanDataBO boImport = new ImportPlanDataBO(); 
		
		public ImportPlanData()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ImportPlanData));
			this.cboSheetnames = new System.Windows.Forms.ComboBox();
			this.txtRange = new System.Windows.Forms.TextBox();
			this.lblRange = new System.Windows.Forms.Label();
			this.btnOpenFileDlg = new System.Windows.Forms.Button();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.lblFileName = new System.Windows.Forms.Label();
			this.lblSheet = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnGetData = new System.Windows.Forms.Button();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.txtCycle = new System.Windows.Forms.TextBox();
			this.btnSearchCycle = new System.Windows.Forms.Button();
			this.lblCycle = new System.Windows.Forms.Label();
			this.dtmMonth = new C1.Win.C1Input.C1DateEdit();
			this.lblMonth = new System.Windows.Forms.Label();
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.btnProductionLine = new System.Windows.Forms.Button();
			this.txtShiftCode = new System.Windows.Forms.TextBox();
			this.btnShiftCode = new System.Windows.Forms.Button();
			this.lblShiftCode = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmMonth)).BeginInit();
			this.SuspendLayout();
			// 
			// cboSheetnames
			// 
			this.cboSheetnames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSheetnames.Location = new System.Drawing.Point(92, 95);
			this.cboSheetnames.Name = "cboSheetnames";
			this.cboSheetnames.Size = new System.Drawing.Size(126, 21);
			this.cboSheetnames.TabIndex = 15;
			// 
			// txtRange
			// 
			this.txtRange.Location = new System.Drawing.Point(261, 95);
			this.txtRange.MaxLength = 20;
			this.txtRange.Name = "txtRange";
			this.txtRange.Size = new System.Drawing.Size(82, 20);
			this.txtRange.TabIndex = 17;
			this.txtRange.Text = "A2:AF29";
			this.txtRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lblRange
			// 
			this.lblRange.Location = new System.Drawing.Point(220, 95);
			this.lblRange.Name = "lblRange";
			this.lblRange.Size = new System.Drawing.Size(39, 20);
			this.lblRange.TabIndex = 16;
			this.lblRange.Text = "Range";
			this.lblRange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnOpenFileDlg
			// 
			this.btnOpenFileDlg.Location = new System.Drawing.Point(620, 73);
			this.btnOpenFileDlg.Name = "btnOpenFileDlg";
			this.btnOpenFileDlg.Size = new System.Drawing.Size(24, 20);
			this.btnOpenFileDlg.TabIndex = 13;
			this.btnOpenFileDlg.Text = "...";
			this.btnOpenFileDlg.Click += new System.EventHandler(this.btnOpenFileDlg_Click);
			// 
			// txtFileName
			// 
			this.txtFileName.Enabled = false;
			this.txtFileName.Location = new System.Drawing.Point(92, 73);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.ReadOnly = true;
			this.txtFileName.Size = new System.Drawing.Size(526, 20);
			this.txtFileName.TabIndex = 12;
			this.txtFileName.Text = "";
			// 
			// lblFileName
			// 
			this.lblFileName.ForeColor = System.Drawing.Color.Maroon;
			this.lblFileName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblFileName.Location = new System.Drawing.Point(4, 73);
			this.lblFileName.Name = "lblFileName";
			this.lblFileName.Size = new System.Drawing.Size(86, 20);
			this.lblFileName.TabIndex = 11;
			this.lblFileName.Text = "File Name";
			this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSheet
			// 
			this.lblSheet.ForeColor = System.Drawing.Color.Maroon;
			this.lblSheet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblSheet.Location = new System.Drawing.Point(4, 95);
			this.lblSheet.Name = "lblSheet";
			this.lblSheet.Size = new System.Drawing.Size(86, 20);
			this.lblSheet.TabIndex = 14;
			this.lblSheet.Text = "Sheet";
			this.lblSheet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(4, 420);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 19;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(592, 420);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 21;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnGetData
			// 
			this.btnGetData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnGetData.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnGetData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnGetData.Location = new System.Drawing.Point(66, 420);
			this.btnGetData.Name = "btnGetData";
			this.btnGetData.Size = new System.Drawing.Size(60, 23);
			this.btnGetData.TabIndex = 20;
			this.btnGetData.Text = "&Get Data";
			this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
			// 
			// dgrdData
			// 
			this.dgrdData.AllowUpdate = false;
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(4, 118);
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
			this.dgrdData.Size = new System.Drawing.Size(648, 300);
			this.dgrdData.TabIndex = 18;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"ProductID\" " +
				"DataField=\"F1\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
				" Caption=\"F1\" DataField=\"F2\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCo" +
				"lumn Level=\"0\" Caption=\"F2\" DataField=\"F3\"><ValueItems /><GroupInfo /></C1DataCo" +
				"lumn><C1DataColumn Level=\"0\" Caption=\"F3\" DataField=\"F4\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F4\" DataField=\"F5\"><ValueIte" +
				"ms /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F5\" DataField=" +
				"\"F6\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
				"F6\" DataField=\"F7\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"F7\" DataField=\"F8\"><ValueItems /><GroupInfo /></C1DataColumn><C1Da" +
				"taColumn Level=\"0\" Caption=\"F8\" DataField=\"F9\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"F9\" DataField=\"F10\"><ValueItems /><Gro" +
				"upInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F10\" DataField=\"F11\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F11\" Da" +
				"taField=\"F12\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"F12\" DataField=\"F13\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
				"olumn Level=\"0\" Caption=\"F13\" DataField=\"F14\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"F14\" DataField=\"F15\"><ValueItems /><Gro" +
				"upInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F15\" DataField=\"F16\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F16\" Da" +
				"taField=\"F17\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"F17\" DataField=\"F18\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
				"olumn Level=\"0\" Caption=\"F18\" DataField=\"F19\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"F19\" DataField=\"F20\"><ValueItems /><Gro" +
				"upInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F20\" DataField=\"F21\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F21\" Da" +
				"taField=\"F22\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"F22\" DataField=\"F23\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
				"olumn Level=\"0\" Caption=\"F23\" DataField=\"F24\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"F24\" DataField=\"F25\"><ValueItems /><Gro" +
				"upInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F25\" DataField=\"F26\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F26\" Da" +
				"taField=\"F27\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"F27\" DataField=\"F28\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
				"olumn Level=\"0\" Caption=\"F28\" DataField=\"F29\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"F29\" DataField=\"F30\"><ValueItems /><Gro" +
				"upInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F30\" DataField=\"F31\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F31\" Da" +
				"taField=\"F32\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=" +
				"\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Style195{}Style194{}Style197{A" +
				"lignHorz:Far;}Style196{AlignHorz:Center;}Style191{AlignHorz:Far;}Style190{AlignH" +
				"orz:Center;}Style193{}Style192{}Style199{}Style198{}Selected{ForeColor:Highlight" +
				"Text;BackColor:Highlight;}Caption{AlignHorz:Center;}Style184{AlignHorz:Center;}S" +
				"tyle185{AlignHorz:Far;}Style186{}Style187{}Style180{}Style181{}Style182{}Style18" +
				"3{}Style188{}Style189{}Style85{}Style84{}Style87{}Style86{}Style81{}Style80{}Sty" +
				"le83{AlignHorz:Far;}Style82{AlignHorz:Center;}Style89{AlignHorz:Far;}Style88{Ali" +
				"gnHorz:Center;}Normal{}Style94{AlignHorz:Center;}Style95{AlignHorz:Far;}Style96{" +
				"}Style97{}Style90{}Style91{}Style92{}Style93{}Style98{}Style99{}OddRow{}Group{Al" +
				"ignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style159{}Style158" +
				"{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:Contro" +
				"lText;AlignVert:Center;}EvenRow{BackColor:Aqua;}Style151{}Style150{}Style153{}St" +
				"yle152{}Style155{AlignHorz:Far;}Style154{AlignHorz:Center;}Style157{}Style156{}S" +
				"tyle148{AlignHorz:Center;}Style149{AlignHorz:Far;}Footer{}Style75{}Style140{}Sty" +
				"le141{}Style142{AlignHorz:Center;}Style143{AlignHorz:Far;}Style144{}Style145{}St" +
				"yle146{}Style147{}Style49{}Style48{}Style41{AlignHorz:Far;}Style40{AlignHorz:Cen" +
				"ter;}Style43{}Style42{}Style45{}Style44{}Style47{AlignHorz:Far;}Style46{AlignHor" +
				"z:Center;}Style173{AlignHorz:Far;}Style172{AlignHorz:Center;}Style171{}Style170{" +
				"}Style177{}Style176{}Style175{}Style174{}Editor{}Style58{AlignHorz:Center;}Style" +
				"59{AlignHorz:Far;}Style50{}Style51{}Style52{AlignHorz:Center;}Style53{AlignHorz:" +
				"Far;}Style54{}Style55{}Style56{}Style57{}Style162{}Style163{}Style160{AlignHorz:" +
				"Center;}Style161{AlignHorz:Far;}Style166{AlignHorz:Center;}Style167{AlignHorz:Fa" +
				"r;}Style164{}Style165{}Style69{}Style68{}Style63{}Style62{}Style61{}Style60{}Sty" +
				"le67{}Style66{}Style65{AlignHorz:Far;}Style64{AlignHorz:Center;}Style115{}Style1" +
				"14{}Style117{}Style116{}Style111{}Style110{}Style113{AlignHorz:Far;}Style112{Ali" +
				"gnHorz:Center;}Style78{}Style79{}Style72{}Style73{}Style70{AlignHorz:Center;}Sty" +
				"le71{AlignHorz:Far;}Style76{AlignHorz:Center;}Style77{AlignHorz:Far;}Style108{}S" +
				"tyle109{}Style104{}Style105{}Style106{AlignHorz:Center;}Style107{AlignHorz:Far;}" +
				"Style100{AlignHorz:Center;}Style101{AlignHorz:Far;}Style102{}Style103{}FilterBar" +
				"{}Style139{}Style138{}Style137{AlignHorz:Far;}Style136{AlignHorz:Center;}Style13" +
				"5{}Style134{}Style133{}Style132{}Style131{AlignHorz:Far;}Style130{AlignHorz:Cent" +
				"er;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style18{}Style19{}" +
				"Style14{}Style15{}Style16{AlignHorz:Center;}Style17{AlignHorz:Near;}Style10{Alig" +
				"nHorz:Near;}Style11{}Style12{}Style13{}Style128{}Style129{}Style126{}Style127{}S" +
				"tyle124{AlignHorz:Center;}Style125{AlignHorz:Far;}Style122{}Style123{}Style120{}" +
				"Style121{}Style29{AlignHorz:Far;}Style28{AlignHorz:Center;}Style27{}Style26{}Sty" +
				"le25{}Style24{}Style23{AlignHorz:Far;}Style22{AlignHorz:Center;}Style21{}Style20" +
				"{}Style207{}Style206{}Style205{}Style204{}Style203{AlignHorz:Far;}Style202{Align" +
				"Horz:Center;}Style38{}Style39{}Style36{}Style37{}Style34{AlignHorz:Center;}Style" +
				"35{AlignHorz:Far;}Style32{}Style33{}Style30{}Style31{}Style74{}Inactive{ForeColo" +
				"r:InactiveCaptionText;BackColor:InactiveCaption;}Style179{AlignHorz:Far;}Style17" +
				"8{AlignHorz:Center;}Style9{}Style8{}Style5{}Style4{}Style7{}Style6{}Style1{}Styl" +
				"e3{}Style2{}Style168{}Style169{}Style201{}Style200{}RecordSelector{AlignImage:Ce" +
				"nter;}Style119{AlignHorz:Far;}Style118{AlignHorz:Center;}</Data></Styles><Splits" +
				"><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"" +
				"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=" +
				"\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><Clie" +
				"ntRect>0, 0, 644, 296</ClientRect><BorderSide>0</BorderSide><CaptionStyle parent" +
				"=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyl" +
				"e parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\"" +
				" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Sty" +
				"le12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"" +
				"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddR" +
				"owStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelecto" +
				"r\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"" +
				"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style" +
				"2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Sty" +
				"le3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyl" +
				"e parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" " +
				"/><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>1" +
				"5</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle paren" +
				"t=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle par" +
				"ent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHe" +
				"aderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"S" +
				"tyle26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><" +
				"Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterS" +
				"tyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style" +
				"1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" />" +
				"<FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
				"le37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle paren" +
				"t=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single<" +
				"/ColumnDivider><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayCo" +
				"lumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Sty" +
				"le41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\"" +
				" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterSty" +
				"le parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>DarkGray" +
				",Single</ColumnDivider><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\"" +
				" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>" +
				"DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>5</DCIdx></C1DisplayCol" +
				"umn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=" +
				"\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle" +
				" parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /" +
				"><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><Column" +
				"Divider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>6</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style" +
				" parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><Edi" +
				"torStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
				"yle63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible" +
				"><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>7</DCId" +
				"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" " +
				"/><Style parent=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66" +
				"\" /><EditorStyle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1" +
				"\" me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True<" +
				"/Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx" +
				">8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Visib" +
				"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Heigh" +
				"t><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
				"2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle parent=\"Sty" +
				"le3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><GroupHeaderStyl" +
				"e parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Style80\" " +
				"/><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>1" +
				"5</Height><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pare" +
				"nt=\"Style2\" me=\"Style82\" /><Style parent=\"Style1\" me=\"Style83\" /><FooterStyle pa" +
				"rent=\"Style3\" me=\"Style84\" /><EditorStyle parent=\"Style5\" me=\"Style85\" /><GroupH" +
				"eaderStyle parent=\"Style1\" me=\"Style87\" /><GroupFooterStyle parent=\"Style1\" me=\"" +
				"Style86\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider>" +
				"<Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
				"tyle parent=\"Style2\" me=\"Style88\" /><Style parent=\"Style1\" me=\"Style89\" /><Foote" +
				"rStyle parent=\"Style3\" me=\"Style90\" /><EditorStyle parent=\"Style5\" me=\"Style91\" " +
				"/><GroupHeaderStyle parent=\"Style1\" me=\"Style93\" /><GroupFooterStyle parent=\"Sty" +
				"le1\" me=\"Style92\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colum" +
				"nDivider><Height>15</Height><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColumn>" +
				"<HeadingStyle parent=\"Style2\" me=\"Style94\" /><Style parent=\"Style1\" me=\"Style95\"" +
				" /><FooterStyle parent=\"Style3\" me=\"Style96\" /><EditorStyle parent=\"Style5\" me=\"" +
				"Style97\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style99\" /><GroupFooterStyle pa" +
				"rent=\"Style1\" me=\"Style98\" /><Visible>True</Visible><ColumnDivider>DarkGray,Sing" +
				"le</ColumnDivider><Height>15</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style100\" /><Style parent=\"Style1\" me" +
				"=\"Style101\" /><FooterStyle parent=\"Style3\" me=\"Style102\" /><EditorStyle parent=\"" +
				"Style5\" me=\"Style103\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style105\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style104\" /><Visible>True</Visible><ColumnDivide" +
				"r>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>14</DCIdx></C1Display" +
				"Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style106\" /><Style par" +
				"ent=\"Style1\" me=\"Style107\" /><FooterStyle parent=\"Style3\" me=\"Style108\" /><Edito" +
				"rStyle parent=\"Style5\" me=\"Style109\" /><GroupHeaderStyle parent=\"Style1\" me=\"Sty" +
				"le111\" /><GroupFooterStyle parent=\"Style1\" me=\"Style110\" /><Visible>True</Visibl" +
				"e><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>15</DC" +
				"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style11" +
				"2\" /><Style parent=\"Style1\" me=\"Style113\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
				"le114\" /><EditorStyle parent=\"Style5\" me=\"Style115\" /><GroupHeaderStyle parent=\"" +
				"Style1\" me=\"Style117\" /><GroupFooterStyle parent=\"Style1\" me=\"Style116\" /><Visib" +
				"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Heigh" +
				"t><DCIdx>16</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Styl" +
				"e2\" me=\"Style118\" /><Style parent=\"Style1\" me=\"Style119\" /><FooterStyle parent=\"" +
				"Style3\" me=\"Style120\" /><EditorStyle parent=\"Style5\" me=\"Style121\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style123\" /><GroupFooterStyle parent=\"Style1\" me=\"Sty" +
				"le122\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><H" +
				"eight>15</Height><DCIdx>17</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style124\" /><Style parent=\"Style1\" me=\"Style125\" /><Foote" +
				"rStyle parent=\"Style3\" me=\"Style126\" /><EditorStyle parent=\"Style5\" me=\"Style127" +
				"\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style129\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style128\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</C" +
				"olumnDivider><Height>15</Height><DCIdx>18</DCIdx></C1DisplayColumn><C1DisplayCol" +
				"umn><HeadingStyle parent=\"Style2\" me=\"Style130\" /><Style parent=\"Style1\" me=\"Sty" +
				"le131\" /><FooterStyle parent=\"Style3\" me=\"Style132\" /><EditorStyle parent=\"Style" +
				"5\" me=\"Style133\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style135\" /><GroupFoote" +
				"rStyle parent=\"Style1\" me=\"Style134\" /><Visible>True</Visible><ColumnDivider>Dar" +
				"kGray,Single</ColumnDivider><Height>15</Height><DCIdx>19</DCIdx></C1DisplayColum" +
				"n><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style136\" /><Style parent=\"" +
				"Style1\" me=\"Style137\" /><FooterStyle parent=\"Style3\" me=\"Style138\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style139\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style141" +
				"\" /><GroupFooterStyle parent=\"Style1\" me=\"Style140\" /><Visible>True</Visible><Co" +
				"lumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>20</DCIdx><" +
				"/C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style142\" />" +
				"<Style parent=\"Style1\" me=\"Style143\" /><FooterStyle parent=\"Style3\" me=\"Style144" +
				"\" /><EditorStyle parent=\"Style5\" me=\"Style145\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style147\" /><GroupFooterStyle parent=\"Style1\" me=\"Style146\" /><Visible>Tr" +
				"ue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DC" +
				"Idx>21</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style148\" /><Style parent=\"Style1\" me=\"Style149\" /><FooterStyle parent=\"Style" +
				"3\" me=\"Style150\" /><EditorStyle parent=\"Style5\" me=\"Style151\" /><GroupHeaderStyl" +
				"e parent=\"Style1\" me=\"Style153\" /><GroupFooterStyle parent=\"Style1\" me=\"Style152" +
				"\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height" +
				">15</Height><DCIdx>22</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pa" +
				"rent=\"Style2\" me=\"Style154\" /><Style parent=\"Style1\" me=\"Style155\" /><FooterStyl" +
				"e parent=\"Style3\" me=\"Style156\" /><EditorStyle parent=\"Style5\" me=\"Style157\" /><" +
				"GroupHeaderStyle parent=\"Style1\" me=\"Style159\" /><GroupFooterStyle parent=\"Style" +
				"1\" me=\"Style158\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Column" +
				"Divider><Height>15</Height><DCIdx>23</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style2\" me=\"Style160\" /><Style parent=\"Style1\" me=\"Style161" +
				"\" /><FooterStyle parent=\"Style3\" me=\"Style162\" /><EditorStyle parent=\"Style5\" me" +
				"=\"Style163\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style165\" /><GroupFooterStyl" +
				"e parent=\"Style1\" me=\"Style164\" /><Visible>True</Visible><ColumnDivider>DarkGray" +
				",Single</ColumnDivider><Height>15</Height><DCIdx>24</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style166\" /><Style parent=\"Style" +
				"1\" me=\"Style167\" /><FooterStyle parent=\"Style3\" me=\"Style168\" /><EditorStyle par" +
				"ent=\"Style5\" me=\"Style169\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style171\" /><" +
				"GroupFooterStyle parent=\"Style1\" me=\"Style170\" /><Visible>True</Visible><ColumnD" +
				"ivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>25</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style172\" /><Styl" +
				"e parent=\"Style1\" me=\"Style173\" /><FooterStyle parent=\"Style3\" me=\"Style174\" /><" +
				"EditorStyle parent=\"Style5\" me=\"Style175\" /><GroupHeaderStyle parent=\"Style1\" me" +
				"=\"Style177\" /><GroupFooterStyle parent=\"Style1\" me=\"Style176\" /><Visible>True</V" +
				"isible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>2" +
				"6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle178\" /><Style parent=\"Style1\" me=\"Style179\" /><FooterStyle parent=\"Style3\" me" +
				"=\"Style180\" /><EditorStyle parent=\"Style5\" me=\"Style181\" /><GroupHeaderStyle par" +
				"ent=\"Style1\" me=\"Style183\" /><GroupFooterStyle parent=\"Style1\" me=\"Style182\" /><" +
				"Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</" +
				"Height><DCIdx>27</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
				"\"Style2\" me=\"Style184\" /><Style parent=\"Style1\" me=\"Style185\" /><FooterStyle par" +
				"ent=\"Style3\" me=\"Style186\" /><EditorStyle parent=\"Style5\" me=\"Style187\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style189\" /><GroupFooterStyle parent=\"Style1\" me" +
				"=\"Style188\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivid" +
				"er><Height>15</Height><DCIdx>28</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style190\" /><Style parent=\"Style1\" me=\"Style191\" /><" +
				"FooterStyle parent=\"Style3\" me=\"Style192\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
				"le193\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style195\" /><GroupFooterStyle par" +
				"ent=\"Style1\" me=\"Style194\" /><Visible>True</Visible><ColumnDivider>DarkGray,Sing" +
				"le</ColumnDivider><Height>15</Height><DCIdx>29</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style196\" /><Style parent=\"Style1\" me" +
				"=\"Style197\" /><FooterStyle parent=\"Style3\" me=\"Style198\" /><EditorStyle parent=\"" +
				"Style5\" me=\"Style199\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style201\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style200\" /><Visible>True</Visible><ColumnDivide" +
				"r>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>30</DCIdx></C1Display" +
				"Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style202\" /><Style par" +
				"ent=\"Style1\" me=\"Style203\" /><FooterStyle parent=\"Style3\" me=\"Style204\" /><Edito" +
				"rStyle parent=\"Style5\" me=\"Style205\" /><GroupHeaderStyle parent=\"Style1\" me=\"Sty" +
				"le207\" /><GroupFooterStyle parent=\"Style1\" me=\"Style206\" /><Visible>True</Visibl" +
				"e><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>31</DC" +
				"Idx></C1DisplayColumn></internalCols></C1.Win.C1TrueDBGrid.MergeView></Splits><N" +
				"amedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" />" +
				"<Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><St" +
				"yle parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Sty" +
				"le parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Sty" +
				"le parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style pa" +
				"rent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><St" +
				"yle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzS" +
				"plits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecS" +
				"elWidth><ClientArea>0, 0, 644, 296</ClientArea><PrintPageHeaderStyle parent=\"\" m" +
				"e=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// txtCycle
			// 
			this.txtCycle.Location = new System.Drawing.Point(92, 6);
			this.txtCycle.MaxLength = 20;
			this.txtCycle.Name = "txtCycle";
			this.txtCycle.Size = new System.Drawing.Size(126, 20);
			this.txtCycle.TabIndex = 1;
			this.txtCycle.Text = "";
			this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCycle_KeyDown);
			this.txtCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtCycle_Validating);
			// 
			// btnSearchCycle
			// 
			this.btnSearchCycle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearchCycle.Location = new System.Drawing.Point(220, 6);
			this.btnSearchCycle.Name = "btnSearchCycle";
			this.btnSearchCycle.Size = new System.Drawing.Size(24, 20);
			this.btnSearchCycle.TabIndex = 2;
			this.btnSearchCycle.Text = "...";
			this.btnSearchCycle.Click += new System.EventHandler(this.btnSearchCycle_Click);
			// 
			// lblCycle
			// 
			this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
			this.lblCycle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCycle.Location = new System.Drawing.Point(4, 6);
			this.lblCycle.Name = "lblCycle";
			this.lblCycle.Size = new System.Drawing.Size(86, 20);
			this.lblCycle.TabIndex = 0;
			this.lblCycle.Text = "Cycle";
			this.lblCycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dtmMonth
			// 
			// 
			// dtmMonth.Calendar
			// 
			this.dtmMonth.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmMonth.CustomFormat = "dd-MM-yyyy";
			this.dtmMonth.EmptyAsNull = true;
			this.dtmMonth.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmMonth.Location = new System.Drawing.Point(92, 51);
			this.dtmMonth.Name = "dtmMonth";
			this.dtmMonth.Size = new System.Drawing.Size(126, 20);
			this.dtmMonth.TabIndex = 7;
			this.dtmMonth.Tag = null;
			this.dtmMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmMonth.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			// 
			// lblMonth
			// 
			this.lblMonth.ForeColor = System.Drawing.Color.Maroon;
			this.lblMonth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMonth.Location = new System.Drawing.Point(4, 51);
			this.lblMonth.Name = "lblMonth";
			this.lblMonth.Size = new System.Drawing.Size(86, 20);
			this.lblMonth.TabIndex = 6;
			this.lblMonth.Text = "Month";
			this.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtProductionLine
			// 
			this.txtProductionLine.Location = new System.Drawing.Point(92, 28);
			this.txtProductionLine.MaxLength = 24;
			this.txtProductionLine.Name = "txtProductionLine";
			this.txtProductionLine.Size = new System.Drawing.Size(126, 20);
			this.txtProductionLine.TabIndex = 4;
			this.txtProductionLine.Text = "";
			this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
			// 
			// lblProductionLine
			// 
			this.lblProductionLine.ForeColor = System.Drawing.Color.Maroon;
			this.lblProductionLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblProductionLine.Location = new System.Drawing.Point(4, 28);
			this.lblProductionLine.Name = "lblProductionLine";
			this.lblProductionLine.Size = new System.Drawing.Size(86, 21);
			this.lblProductionLine.TabIndex = 3;
			this.lblProductionLine.Text = "Production Line";
			this.lblProductionLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnProductionLine
			// 
			this.btnProductionLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnProductionLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnProductionLine.Location = new System.Drawing.Point(220, 28);
			this.btnProductionLine.Name = "btnProductionLine";
			this.btnProductionLine.Size = new System.Drawing.Size(24, 20);
			this.btnProductionLine.TabIndex = 5;
			this.btnProductionLine.Text = "...";
			this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
			// 
			// txtShiftCode
			// 
			this.txtShiftCode.Location = new System.Drawing.Point(305, 50);
			this.txtShiftCode.MaxLength = 24;
			this.txtShiftCode.Name = "txtShiftCode";
			this.txtShiftCode.Size = new System.Drawing.Size(68, 20);
			this.txtShiftCode.TabIndex = 9;
			this.txtShiftCode.Text = "";
			this.txtShiftCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShiftCode_KeyDown);
			this.txtShiftCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtShiftCode_Validating);
			// 
			// btnShiftCode
			// 
			this.btnShiftCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnShiftCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnShiftCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnShiftCode.Location = new System.Drawing.Point(375, 50);
			this.btnShiftCode.Name = "btnShiftCode";
			this.btnShiftCode.Size = new System.Drawing.Size(24, 20);
			this.btnShiftCode.TabIndex = 10;
			this.btnShiftCode.Text = "...";
			this.btnShiftCode.Click += new System.EventHandler(this.btnShiftCode_Click);
			// 
			// lblShiftCode
			// 
			this.lblShiftCode.ForeColor = System.Drawing.Color.Maroon;
			this.lblShiftCode.Location = new System.Drawing.Point(246, 50);
			this.lblShiftCode.Name = "lblShiftCode";
			this.lblShiftCode.Size = new System.Drawing.Size(57, 20);
			this.lblShiftCode.TabIndex = 8;
			this.lblShiftCode.Text = "Shift Code";
			this.lblShiftCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ImportPlanData
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(656, 445);
			this.Controls.Add(this.txtShiftCode);
			this.Controls.Add(this.txtProductionLine);
			this.Controls.Add(this.txtCycle);
			this.Controls.Add(this.txtRange);
			this.Controls.Add(this.txtFileName);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.btnShiftCode);
			this.Controls.Add(this.lblShiftCode);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.btnProductionLine);
			this.Controls.Add(this.dtmMonth);
			this.Controls.Add(this.lblMonth);
			this.Controls.Add(this.btnSearchCycle);
			this.Controls.Add(this.lblCycle);
			this.Controls.Add(this.btnGetData);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lblSheet);
			this.Controls.Add(this.lblFileName);
			this.Controls.Add(this.cboSheetnames);
			this.Controls.Add(this.lblRange);
			this.Controls.Add(this.btnOpenFileDlg);
			this.Name = "ImportPlanData";
			this.Text = "Import Planning Data";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ImportPlanData_Closing);
			this.Load += new System.EventHandler(this.ImportPlanData_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmMonth)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ImportPlanData_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ImportPlanData_Load()";
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
				foreach (C1DisplayColumn col in dgrdData.Splits[0].DisplayColumns)
					if (col.DataColumn.DataField != ITM_ProductTable.PRODUCTID_FLD)
						col.DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				// store grid layout
				dtbGridLayout = FormControlComponents.StoreGridLayout(dgrdData);
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

		private void btnSearchCycle_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycleSearch_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable hshCondition = null;
				drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME,PRO_DCOptionMasterTable.CYCLE_FLD,txtCycle.Text,hshCondition);
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

		private void txtCycle_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_Validating()";
			try
			{
				if (!txtCycle.Modified)
					return;
				if (txtCycle.Text.Trim() == string.Empty)
				{
					txtCycle.Tag = null;
					return;
				}
				Hashtable htbCriterial = null;
				DataRowView drvResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htbCriterial, false);
				if (drvResult != null)
				{
					txtCycle.Text = drvResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
					txtCycle.Tag = drvResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString();
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

		private void txtCycle_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyCode == Keys.F4) && (btnSearchCycle.Enabled))
			{
				btnSearchCycle_Click(sender,e);
			}
		}

		private void btnProductionLine_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable hshCondition = null;
				drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME,PRO_ProductionLineTable.CODE_FLD,txtProductionLine.Text,hshCondition);
				if (drwResult != null)
				{
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
				}
				else
				{
					txtProductionLine.Focus();
					txtProductionLine.SelectAll();
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

		private void txtProductionLine_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
			try
			{
				if (txtProductionLine.Modified)
				{
					if (txtProductionLine.Text.Trim().Length == 0)
					{
						txtProductionLine.Tag = null;
						return;
					}
					DataRowView drwResult = null;
					Hashtable hshCondition = null;
					drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME,PRO_ProductionLineTable.CODE_FLD,txtProductionLine.Text,hshCondition, false);
					if (drwResult != null)
					{
						txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
						txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
					}
					else
						e.Cancel = true;
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

		private void txtProductionLine_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
				btnProductionLine_Click(sender, e);
		}

		private void btnOpenFileDlg_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnOpenFileDlg_Click()";
			try
			{
				OpenFileDialog f = new OpenFileDialog(); 
				f.Filter ="Excel files | *.xls";
				f.InitialDirectory = Application.ExecutablePath;   
			
				if (f.ShowDialog()==DialogResult.OK)
					if (f.FileName != null && f.CheckFileExists==true )
					{
						strExcelFilename =f.FileName;
						txtFileName.Text = f.FileName;
						RetrieveSheetnames();
						if (this.cboSheetnames.Items.Count >0) 
							cboSheetnames.SelectedIndex =0;
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

		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				Cursor = Cursors.WaitCursor; 
				if (ValidateData())
				{
					if (dtbData == null)
					{
						InitExcel(ref objExcelReader);
						dtbData = objExcelReader.GetTable("A1");
						dtbData.DefaultView.Sort = "F1";
						dgrdData.DataSource=dtbData;
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
						objExcelReader.Close();
						objExcelReader.Dispose();
						objExcelReader=null;
					}
					DataTable dtbImportData = new DataTable("A1");
					dtbImportData.Columns.Add(new DataColumn(ITM_ProductTable.PRODUCTID_FLD, typeof(int)));
					for (int i = 1; i<=31; i++)
						dtbImportData.Columns.Add(new DataColumn("F" + i, typeof(decimal)));
					foreach (DataRow drowData in dtbData.Rows)
					{
						DataRow drowImported = dtbImportData.NewRow();
						drowImported[ITM_ProductTable.PRODUCTID_FLD] = drowData["F1"];
						for (int i = 1; i<=31; i++)
							drowImported["F" + i] = drowData["F" + (i+1).ToString()];
						dtbImportData.Rows.Add(drowImported);
					}
					int intMainWorkCenterID = boImport.GetMainWorkCenter(Convert.ToInt32(txtProductionLine.Tag));
					boImport.ImportData(dtbImportData, Convert.ToInt32(txtCycle.Tag), intMainWorkCenterID, Convert.ToInt32(txtShiftCode.Tag), Convert.ToDateTime(dtmMonth.Value));
					string[] strMsg = new string[]{this.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, strMsg);
				}
				Cursor = Cursors.Default;
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
				Cursor = Cursors.Default;
			}
		}

		private void btnGetData_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnGetData_Click()";
			try
			{
				Cursor = Cursors.WaitCursor; 
				InitExcel(ref objExcelReader);
				dtbData = objExcelReader.GetTable("A1");
				dtbData.DefaultView.Sort = "F1";
				dgrdData.DataSource=dtbData;
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
				
				objExcelReader.Close();
				objExcelReader.Dispose();
				objExcelReader=null;
				Cursor = Cursors.Default;
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
				Cursor = Cursors.Default;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ImportPlanData_Closing(object sender, CancelEventArgs e)
		{
		
		}
		private void btnShiftCode_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnShiftCode_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable hshCondition = null;
				drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME,PRO_ShiftTable.SHIFTDESC_FLD,txtShiftCode.Text,hshCondition);
				if (drwResult != null)
				{
					txtShiftCode.Text = drwResult[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
					txtShiftCode.Tag = drwResult[PRO_ShiftTable.SHIFTID_FLD].ToString();
				}
				else
				{
					txtShiftCode.Focus();
					txtShiftCode.SelectAll();
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

		private void txtShiftCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtShiftCode_Validating()";
			try
			{
				if (!txtShiftCode.Modified)
					return;
				if (txtShiftCode.Text.Trim() == string.Empty)
				{
					txtShiftCode.Tag = null;
					return;
				}
				Hashtable htbCriterial = null;
				DataRowView drvResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, txtShiftCode.Text.Trim(), htbCriterial, false);
				if (drvResult != null)
				{
					txtShiftCode.Text = drvResult[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
					txtShiftCode.Tag = drvResult[PRO_ShiftTable.SHIFTID_FLD].ToString();
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

		private void txtShiftCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ((e.KeyCode == Keys.F4) && (btnShiftCode.Enabled))
			{
				btnShiftCode_Click(sender,e);
			}
		}
	
		private void RetrieveSheetnames()
		{
			this.cboSheetnames.Items.Clear();
			
			if (objExcelReader !=null)
			{
				objExcelReader.Dispose();
				objExcelReader=null;
			}
				
			objExcelReader = new ExcelReader();
			objExcelReader.ExcelFilename = strExcelFilename;
			objExcelReader.Headers =false;
			objExcelReader.MixedData =true;
			string[] sheetnames = this.objExcelReader.GetExcelSheetNames();
			this.cboSheetnames.Items.AddRange(sheetnames);
		}

		private void InitExcel(ref ExcelReader exr)
		{
			//Excel must be open
			if (exr == null)
			{
				exr = new ExcelReader();
				exr.ExcelFilename = strExcelFilename;
				exr.Headers =false;
				exr.MixedData =true;
			}
			if  (dtbData==null) dtbData = new DataTable("par");			
			exr.KeepConnectionOpen =true;
			
			//Check excel sheetname is selected
			if (this.cboSheetnames.SelectedIndex>-1) 
				exr.SheetName = this.cboSheetnames.Text; 
			else
				throw new PCSException(ErrorCode.MESSAGE_SELECT_SHEET, string.Empty, null);

			//Set excel sheet range
			exr.SheetRange = this.txtRange.Text; 
		}

		private bool ValidateData()
		{
			if (txtCycle.Tag == null)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtCycle.Focus();
				return false;
			}
			if (txtProductionLine.Tag == null)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtProductionLine.Focus();
				return false;
			}
			if (txtShiftCode.Tag == null)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtShiftCode.Focus();
				return false;
			}
			if (dtmMonth.Value == null)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				dtmMonth.Focus();
				return false;
			}
			if (txtFileName.Text.Trim() == string.Empty)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtFileName.Focus();
				return false;
			}
			return true;
		}
	}
}
