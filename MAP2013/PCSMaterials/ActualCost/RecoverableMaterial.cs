using System;
using System.Data;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSComMaterials.Inventory.BO;
using PCSComMaterials.ActualCost.BO;
using PCSComMaterials.ActualCost.DS;

namespace PCSMaterials.ActualCost
{
	/// <summary>
	/// Summary description for RecoverableMaterial.
	/// </summary>
	public class RecoverableMaterial : System.Windows.Forms.Form
	{
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtRevision;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.Button btnSearchProductDescription;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblRevision;
		private System.Windows.Forms.Button btnSearchProductCode;
		private System.Windows.Forms.Label lblCode;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox txtOrderNo;
		private System.Windows.Forms.Button btnOrderNo;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblOrderNo;
		private System.Windows.Forms.Label lblTransDate;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.TextBox txtBin;
		private C1.Win.C1Input.C1NumericEdit txtQuantity;
		private C1.Win.C1Input.C1NumericEdit txtAvailable;
		private System.Windows.Forms.Button btnLocation;
		private System.Windows.Forms.Button btnBin;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private const string THIS = "PCSMaterials.ActualCost.RecoverableMaterial";
		private C1.Win.C1Input.C1DateEdit dtmPostDate;
		private const string CUSTOMER = "Customer";
		private const string V_VENDOR_CUSTOMER = "V_VendorCustomer";
		private const string RECOVERABLE_QTY = "Recoverable Quantity";
		private const string LOC_OR_VENDOR = "Location/Bin or Vendor";
		private const string VIEW_PRODUCTINFOR = "V_ProductInfor";
		private const string VIEW_RECOVERABLEMATERIALMASTER = "v_RecoverMaterialMaster";
		
		private const string PART_NUMBER = "PartNumber";
		private const string PART_NAME = "PartName";
		private const string MODEL = "Model";
		private const string UM = "UM";
		DateTime dtmCurrentDate = new DateTime();	
		DataTable dtbGridLayout;
		DataSet dstData;
		RecoverableMaterialBO boRecoverable = new RecoverableMaterialBO();
		private System.Windows.Forms.Label lblLocation;
		private EnumAction FormMode = EnumAction.Default;
		CST_RecoverMaterialMasterVO voRecoverMaterialMaster = new CST_RecoverMaterialMasterVO();
		bool blnHasError = false;
		private C1.C1Report.C1Report rptSlip;
		private int intRecoverableMasterID = 0;
		public RecoverableMaterial()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RecoverableMaterial));
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.btnLocation = new System.Windows.Forms.Button();
			this.lblLocation = new System.Windows.Forms.Label();
			this.txtBin = new System.Windows.Forms.TextBox();
			this.btnBin = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtRevision = new System.Windows.Forms.TextBox();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.btnSearchProductDescription = new System.Windows.Forms.Button();
			this.lblDescription = new System.Windows.Forms.Label();
			this.lblRevision = new System.Windows.Forms.Label();
			this.btnSearchProductCode = new System.Windows.Forms.Button();
			this.lblCode = new System.Windows.Forms.Label();
			this.txtQuantity = new C1.Win.C1Input.C1NumericEdit();
			this.label10 = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.dtmPostDate = new C1.Win.C1Input.C1DateEdit();
			this.txtOrderNo = new System.Windows.Forms.TextBox();
			this.btnOrderNo = new System.Windows.Forms.Button();
			this.lblCCN = new System.Windows.Forms.Label();
			this.lblOrderNo = new System.Windows.Forms.Label();
			this.lblTransDate = new System.Windows.Forms.Label();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.txtAvailable = new C1.Win.C1Input.C1NumericEdit();
			this.label3 = new System.Windows.Forms.Label();
			this.rptSlip = new C1.C1Report.C1Report();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtQuantity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmPostDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtAvailable)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rptSlip)).BeginInit();
			this.SuspendLayout();
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
			this.dgrdData.Location = new System.Drawing.Point(5, 152);
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
			this.dgrdData.Size = new System.Drawing.Size(622, 267);
			this.dgrdData.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
			this.dgrdData.TabIndex = 25;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.dgrdData_RowColChange);
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Recoverable" +
				" Qty\" DataField=\"RecoverQuantity\"><ValueItems /><GroupInfo /></C1DataColumn><C1D" +
				"ataColumn Level=\"0\" Caption=\"Component No.\" DataField=\"Code\"><ValueItems /><Grou" +
				"pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Component Name\" DataFiel" +
				"d=\"Description\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
				"\" Caption=\"Model\" DataField=\"Revision\"><ValueItems /><GroupInfo /></C1DataColumn" +
				"><C1DataColumn Level=\"0\" Caption=\"Stock UM\" DataField=\"MST_UnitOfMeasureCode\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"No.\" Da" +
				"taField=\"Line\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
				" Caption=\"To Customer\" DataField=\"MST_PartyCode\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"To Bin\" DataField=\"MST_BINCode\"><Val" +
				"ueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"To Locati" +
				"on\" DataField=\"MST_LocationCode\"><ValueItems /><GroupInfo /></C1DataColumn></Dat" +
				"aCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRo" +
				"w{ForeColor:HighlightText;BackColor:Highlight;}Inactive{ForeColor:InactiveCaptio" +
				"nText;BackColor:InactiveCaption;}Selected{ForeColor:HighlightText;BackColor:High" +
				"light;}Editor{}Style72{}Style73{}Style70{AlignHorz:Near;}Style71{AlignHorz:Near;" +
				"}Style74{}Style75{}FilterBar{}Heading{Wrap:True;BackColor:Control;Border:Raised," +
				",1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style18{}Style19{}Style14{}S" +
				"tyle15{}Style16{AlignHorz:Near;ForeColor:Maroon;}Style17{AlignHorz:Near;}Style10" +
				"{AlignHorz:Near;}Style11{}Style12{}Style13{}Style27{}Style29{AlignHorz:Near;}Sty" +
				"le28{AlignHorz:Near;}Style26{}Style25{}Style9{}Style8{}Style24{}Style23{AlignHor" +
				"z:Near;}Style5{}Style4{}Style7{}Style6{}Style1{}Style22{AlignHorz:Near;}Style3{}" +
				"Style2{}Style21{}Style20{}OddRow{}Style38{}Style39{}Style36{}Style37{}Style34{Al" +
				"ignHorz:Near;}Style35{AlignHorz:Near;}Style32{}Style33{}Style30{}Style49{}Style4" +
				"8{}Style31{}Normal{Font:Microsoft Sans Serif, 8.25pt;}Style47{AlignHorz:Near;}St" +
				"yle46{AlignHorz:Center;ForeColor:Maroon;}EvenRow{BackColor:Aqua;}Style59{AlignHo" +
				"rz:Near;}Style58{AlignHorz:Near;ForeColor:Maroon;}RecordSelector{AlignImage:Cent" +
				"er;}Style51{}Style50{}Footer{}Style52{AlignHorz:Near;ForeColor:Maroon;}Style53{A" +
				"lignHorz:Near;}Style54{}Style55{}Style56{}Style57{}Caption{AlignHorz:Center;}Sty" +
				"le69{}Style68{}Style63{}Style62{}Style61{}Style60{}Style67{}Style66{}Style65{Ali" +
				"gnHorz:Near;}Style64{AlignHorz:Near;}Group{AlignVert:Center;Border:None,,0, 0, 0" +
				", 0;BackColor:ControlDark;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeVie" +
				"w Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Ma" +
				"rqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" Verti" +
				"calScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 618, 263</ClientR" +
				"ect><BorderSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><Edit" +
				"orStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\"" +
				" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer" +
				"\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"" +
				"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><I" +
				"nactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"St" +
				"yle9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedSty" +
				"le parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><interna" +
				"lCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style paren" +
				"t=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorSty" +
				"le parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\"" +
				" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><Colu" +
				"mnDivider>DarkGray,Single</ColumnDivider><Width>31</Width><Height>15</Height><DC" +
				"Idx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
				"=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" " +
				"me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle par" +
				"ent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Vi" +
				"sible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>133</Wi" +
				"dth><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style6" +
				"1\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Co" +
				"lumnDivider><Width>138</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColu" +
				"mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"" +
				"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle " +
				"parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><ColumnD" +
				"ivider>DarkGray,Single</ColumnDivider><Width>85</Width><Height>15</Height><DCIdx" +
				">3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Visib" +
				"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>60</Width>" +
				"<Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
				"yle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><Footer" +
				"Style parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /" +
				"><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Styl" +
				"e1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Column" +
				"Divider><Width>99</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C" +
				"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style" +
				"1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle paren" +
				"t=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><Grou" +
				"pFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivide" +
				"r>DarkGray,Single</ColumnDivider><Width>106</Width><Height>15</Height><DCIdx>8</" +
				"DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style" +
				"28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
				"le30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"St" +
				"yle1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>T" +
				"rue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>99</Width><Hei" +
				"ght>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyl" +
				"e parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><Gr" +
				"oupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" " +
				"me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivi" +
				"der><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn></internalCols></C1.Win" +
				".C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><St" +
				"yle parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style " +
				"parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style p" +
				"arent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style paren" +
				"t=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style pare" +
				"nt=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style p" +
				"arent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyl" +
				"es><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout>" +
				"<DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 618, 263</ClientAre" +
				"a><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\"" +
				" me=\"Style15\" /></Blob>";
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPrint.Location = new System.Drawing.Point(126, 423);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(60, 23);
			this.btnPrint.TabIndex = 28;
			this.btnPrint.Text = "&Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(66, 423);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 27;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAdd.Location = new System.Drawing.Point(6, 423);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(60, 23);
			this.btnAdd.TabIndex = 26;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// txtLocation
			// 
			this.txtLocation.AccessibleDescription = "";
			this.txtLocation.AccessibleName = "";
			this.txtLocation.Location = new System.Drawing.Point(88, 54);
			this.txtLocation.MaxLength = 40;
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(134, 20);
			this.txtLocation.TabIndex = 8;
			this.txtLocation.Text = "";
			this.txtLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocation_KeyDown);
			this.txtLocation.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocation_Validating);
			// 
			// btnLocation
			// 
			this.btnLocation.AccessibleDescription = "";
			this.btnLocation.AccessibleName = "";
			this.btnLocation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnLocation.Location = new System.Drawing.Point(224, 54);
			this.btnLocation.Name = "btnLocation";
			this.btnLocation.Size = new System.Drawing.Size(24, 20);
			this.btnLocation.TabIndex = 9;
			this.btnLocation.Text = "...";
			this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
			// 
			// lblLocation
			// 
			this.lblLocation.AccessibleDescription = "";
			this.lblLocation.AccessibleName = "";
			this.lblLocation.ForeColor = System.Drawing.Color.Maroon;
			this.lblLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLocation.Location = new System.Drawing.Point(6, 54);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.Size = new System.Drawing.Size(78, 20);
			this.lblLocation.TabIndex = 7;
			this.lblLocation.Text = "From Location";
			this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtBin
			// 
			this.txtBin.AccessibleDescription = "";
			this.txtBin.AccessibleName = "";
			this.txtBin.Location = new System.Drawing.Point(306, 54);
			this.txtBin.MaxLength = 40;
			this.txtBin.Name = "txtBin";
			this.txtBin.Size = new System.Drawing.Size(94, 20);
			this.txtBin.TabIndex = 11;
			this.txtBin.Text = "";
			this.txtBin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBin_KeyDown);
			this.txtBin.Validating += new System.ComponentModel.CancelEventHandler(this.txtBin_Validating);
			// 
			// btnBin
			// 
			this.btnBin.AccessibleDescription = "";
			this.btnBin.AccessibleName = "";
			this.btnBin.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnBin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnBin.Location = new System.Drawing.Point(401, 54);
			this.btnBin.Name = "btnBin";
			this.btnBin.Size = new System.Drawing.Size(24, 20);
			this.btnBin.TabIndex = 12;
			this.btnBin.Text = "...";
			this.btnBin.Click += new System.EventHandler(this.btnBin_Click);
			// 
			// label2
			// 
			this.label2.AccessibleDescription = "";
			this.label2.AccessibleName = "";
			this.label2.ForeColor = System.Drawing.Color.Maroon;
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(257, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 20);
			this.label2.TabIndex = 10;
			this.label2.Text = "From Bin";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtRevision
			// 
			this.txtRevision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtRevision.Location = new System.Drawing.Point(306, 78);
			this.txtRevision.MaxLength = 20;
			this.txtRevision.Name = "txtRevision";
			this.txtRevision.ReadOnly = true;
			this.txtRevision.Size = new System.Drawing.Size(94, 20);
			this.txtRevision.TabIndex = 17;
			this.txtRevision.Text = "";
			// 
			// txtDescription
			// 
			this.txtDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtDescription.Location = new System.Drawing.Point(88, 102);
			this.txtDescription.MaxLength = 200;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(312, 20);
			this.txtDescription.TabIndex = 19;
			this.txtDescription.Text = "";
			this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
			this.txtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.txtDescription_Validating);
			// 
			// txtCode
			// 
			this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtCode.Location = new System.Drawing.Point(88, 78);
			this.txtCode.MaxLength = 50;
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(134, 20);
			this.txtCode.TabIndex = 14;
			this.txtCode.Text = "";
			this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
			this.txtCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtCode_Validating);
			// 
			// btnSearchProductDescription
			// 
			this.btnSearchProductDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSearchProductDescription.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSearchProductDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearchProductDescription.Location = new System.Drawing.Point(401, 102);
			this.btnSearchProductDescription.Name = "btnSearchProductDescription";
			this.btnSearchProductDescription.Size = new System.Drawing.Size(24, 20);
			this.btnSearchProductDescription.TabIndex = 20;
			this.btnSearchProductDescription.Text = "...";
			this.btnSearchProductDescription.Click += new System.EventHandler(this.btnSearchProductDescription_Click);
			// 
			// lblDescription
			// 
			this.lblDescription.ForeColor = System.Drawing.Color.Maroon;
			this.lblDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblDescription.Location = new System.Drawing.Point(6, 102);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(78, 20);
			this.lblDescription.TabIndex = 18;
			this.lblDescription.Text = "Part Name";
			this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRevision
			// 
			this.lblRevision.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRevision.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblRevision.Location = new System.Drawing.Point(257, 78);
			this.lblRevision.Name = "lblRevision";
			this.lblRevision.Size = new System.Drawing.Size(50, 19);
			this.lblRevision.TabIndex = 16;
			this.lblRevision.Text = "Model";
			this.lblRevision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSearchProductCode
			// 
			this.btnSearchProductCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSearchProductCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSearchProductCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearchProductCode.Location = new System.Drawing.Point(224, 78);
			this.btnSearchProductCode.Name = "btnSearchProductCode";
			this.btnSearchProductCode.Size = new System.Drawing.Size(24, 20);
			this.btnSearchProductCode.TabIndex = 15;
			this.btnSearchProductCode.Text = "...";
			this.btnSearchProductCode.Click += new System.EventHandler(this.btnSearchProductCode_Click);
			// 
			// lblCode
			// 
			this.lblCode.ForeColor = System.Drawing.Color.Maroon;
			this.lblCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCode.Location = new System.Drawing.Point(6, 78);
			this.lblCode.Name = "lblCode";
			this.lblCode.Size = new System.Drawing.Size(78, 20);
			this.lblCode.TabIndex = 13;
			this.lblCode.Text = "Part Number";
			this.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtQuantity
			// 
			this.txtQuantity.AccessibleDescription = "";
			this.txtQuantity.AccessibleName = "";
			// 
			// txtQuantity.Calculator
			// 
			this.txtQuantity.Calculator.AccessibleDescription = "";
			this.txtQuantity.Calculator.AccessibleName = "";
			this.txtQuantity.CustomFormat = "###############,0.00";
			this.txtQuantity.EmptyAsNull = true;
			this.txtQuantity.ErrorInfo.ShowErrorMessage = false;
			this.txtQuantity.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtQuantity.Location = new System.Drawing.Point(88, 126);
			this.txtQuantity.Name = "txtQuantity";
			this.txtQuantity.Size = new System.Drawing.Size(100, 20);
			this.txtQuantity.TabIndex = 22;
			this.txtQuantity.Tag = null;
			this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtQuantity.Value = new System.Decimal(new int[] {
																	  0,
																	  0,
																	  0,
																	  0});
			this.txtQuantity.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtQuantity.Validating += new System.ComponentModel.CancelEventHandler(this.txtQuantity_Validating);
			// 
			// label10
			// 
			this.label10.AccessibleDescription = "";
			this.label10.AccessibleName = "";
			this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label10.ForeColor = System.Drawing.Color.Maroon;
			this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label10.Location = new System.Drawing.Point(6, 126);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(78, 20);
			this.label10.TabIndex = 21;
			this.label10.Text = "Destroy Qty";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(508, 423);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 29;
			this.btnHelp.Text = "&Help";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(568, 423);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 30;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// dtmPostDate
			// 
			this.dtmPostDate.AccessibleDescription = "";
			this.dtmPostDate.AccessibleName = "";
			// 
			// dtmPostDate.Calendar
			// 
			this.dtmPostDate.Calendar.AccessibleDescription = "";
			this.dtmPostDate.Calendar.AccessibleName = "";
			this.dtmPostDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmPostDate.CustomFormat = "dd-MM-yyyy";
			this.dtmPostDate.ErrorInfo.ShowErrorMessage = false;
			this.dtmPostDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmPostDate.Location = new System.Drawing.Point(88, 6);
			this.dtmPostDate.Name = "dtmPostDate";
			this.dtmPostDate.Size = new System.Drawing.Size(134, 20);
			this.dtmPostDate.TabIndex = 3;
			this.dtmPostDate.Tag = null;
			this.dtmPostDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmPostDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmPostDate.Leave += new System.EventHandler(this.dtmPostDate_Leave);
			// 
			// txtOrderNo
			// 
			this.txtOrderNo.AccessibleDescription = "";
			this.txtOrderNo.AccessibleName = "";
			this.txtOrderNo.Location = new System.Drawing.Point(88, 30);
			this.txtOrderNo.MaxLength = 20;
			this.txtOrderNo.Name = "txtOrderNo";
			this.txtOrderNo.Size = new System.Drawing.Size(134, 20);
			this.txtOrderNo.TabIndex = 5;
			this.txtOrderNo.Text = "";
			this.txtOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderNo_KeyDown);
			this.txtOrderNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtOrderNo_Validating);
			// 
			// btnOrderNo
			// 
			this.btnOrderNo.AccessibleDescription = "";
			this.btnOrderNo.AccessibleName = "";
			this.btnOrderNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOrderNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnOrderNo.Location = new System.Drawing.Point(224, 30);
			this.btnOrderNo.Name = "btnOrderNo";
			this.btnOrderNo.Size = new System.Drawing.Size(24, 20);
			this.btnOrderNo.TabIndex = 6;
			this.btnOrderNo.Text = "...";
			this.btnOrderNo.Click += new System.EventHandler(this.btnOrderNo_Click);
			// 
			// lblCCN
			// 
			this.lblCCN.AccessibleDescription = "";
			this.lblCCN.AccessibleName = "";
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(512, 6);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(30, 20);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblOrderNo
			// 
			this.lblOrderNo.AccessibleDescription = "";
			this.lblOrderNo.AccessibleName = "";
			this.lblOrderNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblOrderNo.ForeColor = System.Drawing.Color.Maroon;
			this.lblOrderNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblOrderNo.Location = new System.Drawing.Point(6, 30);
			this.lblOrderNo.Name = "lblOrderNo";
			this.lblOrderNo.Size = new System.Drawing.Size(78, 20);
			this.lblOrderNo.TabIndex = 4;
			this.lblOrderNo.Text = "Trans. No.";
			this.lblOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTransDate
			// 
			this.lblTransDate.AccessibleDescription = "";
			this.lblTransDate.AccessibleName = "";
			this.lblTransDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblTransDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblTransDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblTransDate.Location = new System.Drawing.Point(6, 6);
			this.lblTransDate.Name = "lblTransDate";
			this.lblTransDate.Size = new System.Drawing.Size(78, 20);
			this.lblTransDate.TabIndex = 2;
			this.lblTransDate.Text = "Post Date";
			this.lblTransDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.cboCCN.DropDownWidth = 200;
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(544, 6);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(84, 21);
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
			// txtAvailable
			// 
			this.txtAvailable.AccessibleDescription = "";
			this.txtAvailable.AccessibleName = "";
			// 
			// txtAvailable.Calculator
			// 
			this.txtAvailable.Calculator.AccessibleDescription = "";
			this.txtAvailable.Calculator.AccessibleName = "";
			this.txtAvailable.CustomFormat = "###############,0.00";
			this.txtAvailable.EmptyAsNull = true;
			this.txtAvailable.ErrorInfo.ShowErrorMessage = false;
			this.txtAvailable.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtAvailable.Location = new System.Drawing.Point(306, 126);
			this.txtAvailable.Name = "txtAvailable";
			this.txtAvailable.ReadOnly = true;
			this.txtAvailable.Size = new System.Drawing.Size(94, 20);
			this.txtAvailable.TabIndex = 24;
			this.txtAvailable.Tag = null;
			this.txtAvailable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtAvailable.Value = new System.Decimal(new int[] {
																	   0,
																	   0,
																	   0,
																	   0});
			this.txtAvailable.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// label3
			// 
			this.label3.AccessibleDescription = "";
			this.label3.AccessibleName = "";
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label3.Location = new System.Drawing.Point(234, 126);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(73, 20);
			this.label3.TabIndex = 23;
			this.label3.Text = "Available Qty";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// rptSlip
			// 
			this.rptSlip.ReportName = "RecycledSlip";
			// 
			// RecoverableMaterial
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(632, 453);
			this.Controls.Add(this.txtAvailable);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.dtmPostDate);
			this.Controls.Add(this.txtOrderNo);
			this.Controls.Add(this.txtRevision);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.txtCode);
			this.Controls.Add(this.txtBin);
			this.Controls.Add(this.txtLocation);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.btnOrderNo);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblOrderNo);
			this.Controls.Add(this.lblTransDate);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.txtQuantity);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.btnSearchProductDescription);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.lblRevision);
			this.Controls.Add(this.btnSearchProductCode);
			this.Controls.Add(this.lblCode);
			this.Controls.Add(this.btnBin);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnLocation);
			this.Controls.Add(this.lblLocation);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnAdd);
			this.KeyPreview = true;
			this.Name = "RecoverableMaterial";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Recoverable Material";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.RecoverableMaterial_Closing);
			this.Load += new System.EventHandler(this.RecoverableMaterial_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtQuantity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmPostDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtAvailable)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rptSlip)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// btnLocation_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Tuandm</author>
		/// <date>Monday, Feb 27 2006</date>
		private void btnLocation_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnLocation_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, txtLocation.Text, htbCondition, true);
				if (drwResult != null)
				{
					if (txtLocation.Tag != null)
					{
						if (txtLocation.Tag.ToString() != drwResult[MST_LocationTable.LOCATIONID_FLD].ToString())
						{
							//Clear Bin
							txtBin.Text = string.Empty;
							txtBin.Tag = null;
						}
					}
					txtLocation.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
					lblLocation.Tag = drwResult[MST_LocationTable.MASTERLOCATIONID_FLD];
					txtLocation.Tag = int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString());
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
		/// btnBin_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Tuandm</author>
		/// <date>Monday, Feb 27 2006</date>
		private void btnBin_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnBin_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				
				if (txtLocation.Tag != null)
				{
					htbCondition.Add(MST_BINTable.LOCATIONID_FLD, txtLocation.Tag.ToString());
					htbCondition.Add(MST_BINTable.BINTYPEID_FLD, (int)BinTypeEnum.LS);
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Warning);
					txtLocation.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, txtBin.Text, htbCondition, true);
				if (drwResult != null)
				{
					txtBin.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
					txtBin.Tag = int.Parse(drwResult[MST_BINTable.BINID_FLD].ToString());
					if (txtCode.Tag != null)
					{
//						decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//							Convert.ToInt32(lblLocation.Tag), Convert.ToInt32(txtLocation.Tag.ToString()), Convert.ToInt32(txtBin.Tag), Convert.ToInt32(txtCode.Tag));
						decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate(dtmCurrentDate, int.Parse(cboCCN.SelectedValue.ToString()),
							Convert.ToInt32(lblLocation.Tag), Convert.ToInt32(txtLocation.Tag.ToString()), Convert.ToInt32(txtBin.Tag), Convert.ToInt32(txtCode.Tag));

						txtQuantity.Value = txtAvailable.Value = decAvailableQuantityBin;	
					}
					
				}
				else
				{
					txtBin.Focus();
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
		/// Fill data to controls
		/// </summary>
		/// <param name="pdrwResult"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		private void FillDataFromDataRowViewToControls(DataRowView pdrwResult)
		{
			const string METHOD_NAME = THIS + ".FillDataFromDataRowViewToControls()";
			try
			{
				txtCode.Text = pdrwResult[ITM_ProductTable.CODE_FLD].ToString();
				txtDescription.Text = pdrwResult[ITM_ProductTable.DESCRIPTION_FLD].ToString();
				txtRevision.Text = pdrwResult[ITM_ProductTable.REVISION_FLD].ToString();
				txtCode.Tag = int.Parse(pdrwResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
				if (txtBin.Text.Trim() != string.Empty)
				{
//					decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//						Convert.ToInt32(lblLocation.Tag), Convert.ToInt32(txtLocation.Tag.ToString()), Convert.ToInt32(txtBin.Tag), Convert.ToInt32(txtCode.Tag));
					decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate(dtmCurrentDate, int.Parse(cboCCN.SelectedValue.ToString()),
						Convert.ToInt32(lblLocation.Tag), Convert.ToInt32(txtLocation.Tag.ToString()), Convert.ToInt32(txtBin.Tag), Convert.ToInt32(txtCode.Tag));
					txtQuantity.Value = txtAvailable.Value = decAvailableQuantityBin;
				}
				else
				{
					if (txtLocation.Text.Trim() != string.Empty)
					{
//						decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//							Convert.ToInt32(lblLocation.Tag), Convert.ToInt32(txtLocation.Tag.ToString()), 0, Convert.ToInt32(txtCode.Tag));
						decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate(dtmCurrentDate, int.Parse(cboCCN.SelectedValue.ToString()),
							Convert.ToInt32(lblLocation.Tag), Convert.ToInt32(txtLocation.Tag.ToString()), 0, Convert.ToInt32(txtCode.Tag));
						txtQuantity.Value =	txtAvailable.Value = decAvailableQuantityBin;
					}
				}
				BindDataToGrid(0, boRecoverable.ListBomOfProduct(Convert.ToInt32(txtCode.Tag)));
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
		/// btnSearchProductCode_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		private void btnSearchProductCode_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchProductCode_Click()";
			try 
			{
				DataRowView drwResult = null;
				Hashtable htbConditon = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbConditon.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				htbConditon.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
				drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtCode.Text, htbConditon, true);
				if (drwResult != null)
				{
					FillDataFromDataRowViewToControls(drwResult);
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
		/// btnSearchProductDescription_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <daet>Thursday, Mar 2 2006</daet>
		private void btnSearchProductDescription_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchProductDescription_Click()";
			try 
			{
				DataRowView drwResult = null;
				Hashtable htbConditon = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbConditon.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				htbConditon.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
				drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtDescription.Text, htbConditon, true);
				if (drwResult != null)
				{
					FillDataFromDataRowViewToControls(drwResult);
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

		private void RecoverableMaterial_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".RecoverableMaterial_Load()";
			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					return;
				}

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
				//HACK: added by Tuan TQ. Format post date
				dtmPostDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				dtmPostDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				dtmCurrentDate = (new UtilsBO()).GetDBDate().AddDays(1);
				//Reset form and save grid's layout
				dtbGridLayout = FormControlComponents.StoreGridLayout(dgrdData);
				FormMode = EnumAction.Default;
				SwitchFormMode();
				BindDataToGrid(0, boRecoverable.ListByMasterID(0));
				ConfigGrid(true);

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
		/// Change the state of control when FormMode is changed
		/// </summary>
		private void SwitchFormMode()
		{
			switch (FormMode)
			{
				case EnumAction.Add:
					btnAdd.Enabled = false;
					btnSave.Enabled = true;
					btnOrderNo.Enabled = false;
					txtOrderNo.Enabled = true;
					dtmPostDate.Enabled = true;

					txtLocation.Enabled = true;
					btnLocation.Enabled = true;
					txtBin.Enabled = true;
					btnBin.Enabled = true;
					txtCode.Enabled = true;
					btnSearchProductCode.Enabled = true;
					txtDescription.Enabled = true;
					btnSearchProductDescription.Enabled = true;
					txtQuantity.Enabled = true;
					dgrdData.AllowDelete = true;
					dgrdData.AllowUpdate = true;
					dgrdData.AllowAddNew = true;

					dtmPostDate.Value = new UtilsBO().GetDBDate();
					txtOrderNo.Text = string.Empty;
					txtLocation.Text = string.Empty;
					txtBin.Text = string.Empty;
					txtQuantity.Value = DBNull.Value;
					txtAvailable.Value = DBNull.Value;
					txtCode.Text = string.Empty;
					txtDescription.Text = string.Empty;
					txtRevision.Text = string.Empty;
					break;
				case EnumAction.Edit:
					break;
				case EnumAction.Default:
					btnAdd.Enabled = true;
					btnSave.Enabled = false;
					btnOrderNo.Enabled = true;
					txtOrderNo.Enabled = true;
					dtmPostDate.Enabled = false;

					txtLocation.Enabled = false;
					btnLocation.Enabled = false;
					txtBin.Enabled = false;
					btnBin.Enabled = false;
					txtCode.Enabled = false;
					btnSearchProductCode.Enabled = false;
					txtDescription.Enabled = false;
					btnSearchProductDescription.Enabled = false;
					txtQuantity.Enabled = false;
					dgrdData.AllowDelete = false;
					dgrdData.AllowUpdate = false;
					dgrdData.AllowAddNew = false;
					break;
			}
		}
		/// <summary>
		/// FillDataToAllControls
		/// </summary>
		/// <param name="pintRecoverableMasterID"></param>
		/// <author>Trada</author>
		/// <date>Friday, Mar 3 2006</date>
		private void FillDataToAllControls(int pintRecoverableMasterID)
		{
			const string METHOD_NAME = THIS + ".FillDataToAllControls()";
			try
			{
				//Get information for master
				DataSet dstMaster = new	DataSet();
				dstMaster = boRecoverable.GetMasterByID(pintRecoverableMasterID);
				if (dstMaster.Tables[0].Rows.Count != 0)
				{
					dtmPostDate.Value = (DateTime)dstMaster.Tables[0].Rows[0][CST_RecoverMaterialMasterTable.POSTDATE_FLD]; 
					txtOrderNo.Text = dstMaster.Tables[0].Rows[0][CST_RecoverMaterialMasterTable.TRANSNO_FLD].ToString();
					txtLocation.Text = dstMaster.Tables[0].Rows[0][MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString();
					txtBin.Text = dstMaster.Tables[0].Rows[0][MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].ToString();
					txtCode.Text = dstMaster.Tables[0].Rows[0][ITM_ProductTable.CODE_FLD].ToString();
					txtCode.Tag = dstMaster.Tables[0].Rows[0][ITM_ProductTable.PRODUCTID_FLD];
					txtLocation.Tag = dstMaster.Tables[0].Rows[0][CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD];
					txtBin.Tag = dstMaster.Tables[0].Rows[0][CST_RecoverMaterialMasterTable.FROMBINID_FLD];
					txtDescription.Text = dstMaster.Tables[0].Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
					txtRevision.Text = dstMaster.Tables[0].Rows[0][ITM_ProductTable.REVISION_FLD].ToString();
					txtQuantity.Value = (Decimal) dstMaster.Tables[0].Rows[0][CST_RecoverMaterialMasterTable.QUANTITY_FLD];
//					decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//						SystemProperty.MasterLocationID, Convert.ToInt32(txtLocation.Tag.ToString()), Convert.ToInt32(txtBin.Tag), Convert.ToInt32(txtCode.Tag));
					if ((dstMaster.Tables[0].Rows[0][CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD] != DBNull.Value)
						&& (dstMaster.Tables[0].Rows[0][CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD].ToString() != string.Empty))
					txtAvailable.Value = (Decimal) dstMaster.Tables[0].Rows[0][CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD];
				}
				//Get information detail
				dstData = boRecoverable.ListByMasterID(pintRecoverableMasterID);
				//Update Line colum
				for (int i = 0; i < dstData.Tables[0].Rows.Count; i++)
				{
					dstData.Tables[0].Rows[i][PRO_WorkOrderDetailTable.LINE_FLD] = i + 1; 
				}
				dgrdData.DataSource = dstData.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
				ConfigGrid(true);
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
		/// btnOrderNo_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Mar 3 2006</date>
		private void btnOrderNo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnOrderNo_Click()";
			try
			{
				Hashtable htbCriterial = new Hashtable();
				DataRowView drwResult = null;
				if (cboCCN.SelectedValue != null)
				{
					htbCriterial.Add(CST_RecoverMaterialMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(VIEW_RECOVERABLEMATERIALMASTER, CST_RecoverMaterialMasterTable.TRANSNO_FLD, txtOrderNo.Text.Trim(), htbCriterial, true);
				if (drwResult != null)
				{
					txtOrderNo.Text = drwResult[CST_RecoverMaterialMasterTable.TRANSNO_FLD].ToString().Trim();
					txtOrderNo.Tag = drwResult[CST_RecoverMaterialMasterTable.RECOVERMATERIALMASTERID_FLD].ToString().Trim();
					intRecoverableMasterID = Convert.ToInt32(txtOrderNo.Tag);
					FillDataToAllControls(int.Parse(txtOrderNo.Tag.ToString()));
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
		/// Clear all controls in form
		/// </summary>
		/// <author>Traa</author>
		/// <date>Friday, Mar 10 2006</date>
		private void ClearForm()
		{
			const string METHOD_NAME = THIS + ".ClearForm()";
			try
			{
				dtmPostDate.Value = null;
				txtAvailable.Value = null;
				txtBin.Text = string.Empty;
				txtBin.Tag = null;
				txtCode.Text = string.Empty;
				txtCode.Tag = null;
				txtDescription.Text = string.Empty;
				txtLocation.Text = string.Empty;
				txtLocation.Tag = null;
				txtOrderNo.Text = string.Empty;
				txtOrderNo.Tag = null;
				txtQuantity.Value = null;
				txtRevision.Text = string.Empty;
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
		/// btnAdd_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Mar 3 2006</date>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				FormMode = EnumAction.Add;
				ClearForm();
				SwitchFormMode();
				//InitForm
				dtmPostDate.Value = new UtilsBO().GetDBDate();
				txtOrderNo.Text = FormControlComponents.GetNoByMask(this);
				//BindDataToGrid(0, null);
				dstData = boRecoverable.ListByMasterID(0);
				dgrdData.DataSource = dstData.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
				ConfigGrid(false);
				dtmPostDate.Focus();
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
		/// dtmPostDate_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, April 17 2006</date>
		private void dtmPostDate_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmPostDate_Leave()";
			try 
			{
				if (txtBin.Text.Trim() != string.Empty)
				{
//					decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//						Convert.ToInt32(lblLocation.Tag), Convert.ToInt32(txtLocation.Tag.ToString()), Convert.ToInt32(txtBin.Tag), Convert.ToInt32(txtCode.Tag));
					decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate(dtmCurrentDate, int.Parse(cboCCN.SelectedValue.ToString()),
						Convert.ToInt32(lblLocation.Tag), Convert.ToInt32(txtLocation.Tag.ToString()), Convert.ToInt32(txtBin.Tag), Convert.ToInt32(txtCode.Tag));					
					txtQuantity.Value = txtAvailable.Value = decAvailableQuantityBin;
				}
				else
				{
					if (txtLocation.Text.Trim() != string.Empty)
					{
//						decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//							Convert.ToInt32(lblLocation.Tag), Convert.ToInt32(txtLocation.Tag.ToString()), 0, Convert.ToInt32(txtCode.Tag));
						decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate(dtmCurrentDate, int.Parse(cboCCN.SelectedValue.ToString()),
							Convert.ToInt32(lblLocation.Tag), Convert.ToInt32(txtLocation.Tag.ToString()), 0, Convert.ToInt32(txtCode.Tag));
						txtQuantity.Value = txtAvailable.Value = decAvailableQuantityBin;
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
		/// dgrdData_ButtonClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		private void dgrdData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition= new Hashtable();
				string strWhereClause = string.Empty;
				if (!btnSave.Enabled) return;
				//Select Item
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD])
					|| dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD]))
				{
					if (txtCode.Text.Trim() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_SELECT_PRODUCT, MessageBoxIcon.Warning);
						txtCode.Focus();
						return;
					}
					else
					{
						strWhereClause += ITM_ProductTable.PRODUCTID_FLD + " != " + txtCode.Tag.ToString();
						strWhereClause += " AND ((ParentProductID = " + txtCode.Tag.ToString() + " and MakeItem = 1) OR MakeItem = 0)";
					}
					string strCode = string.Empty, strDescription = string.Empty;
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]))
							strCode = dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD].ToString();
						else
							strDescription = dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD].ToString();
//						if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]))
//							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, PART_NUMBER, dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD].ToString(), strWhereClause);
//						else
//							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, PART_NAME, dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD].ToString(), strWhereClause);
					}
					else
					{
						if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]))
							strCode = dgrdData.Columns[ITM_ProductTable.CODE_FLD].Text.Trim();
						else
							strDescription = dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Text.Trim();
//						if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]))
//							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, PART_NUMBER, dgrdData.Columns[ITM_ProductTable.CODE_FLD].Text.Trim(), strWhereClause);
//						else
//							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, PART_NAME, dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Text.Trim(), strWhereClause);
					}
					SelectItems frmSelectItem = new SelectItems(Convert.ToInt32(txtCode.Tag), strCode, strDescription);
					if (frmSelectItem.ShowDialog() == DialogResult.OK)
						drwResult = frmSelectItem.drvReturnDataRowView;
					
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						FillItemDataToGrid(drwResult.Row);
					}
				}
				//Select Location, Bin
				if ((dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]))
					&& (dgrdData[dgrdData.Row, MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].ToString() == string.Empty))
				{
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Text.Trim(), htbCondition, true);
					}	
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						if ((dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD] != null)
							&&(dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD] != DBNull.Value))
						{
							if (int.Parse(dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()) != int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString()))
							{
								dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Value = string.Empty;
								dgrdData.Columns[CST_RecoverMaterialDetailTable.TOBINID_FLD].Value = string.Empty;
							}
						}
						dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Value = drwResult[MST_LocationTable.CODE_FLD].ToString(); 
						dgrdData.Columns[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].Value = int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString()); 
					}
				}
				//select Bin
				if ((dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD]))
					&& (dgrdData[dgrdData.Row, MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].ToString() == string.Empty))
				{
					
					if (dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD] == DBNull.Value
						|| dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString().Trim() == string.Empty
						|| int.Parse(dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()) == 0)
					{
						//MessageBox.Show("Please select the Location first");
						PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_SELECT_LOCATION, MessageBoxIcon.Warning);
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD]);
						dgrdData.Focus();
						dgrdData.Select();
						return;
					}
					else
					{
						htbCondition.Add(MST_BINTable.LOCATIONID_FLD, int.Parse(dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()));
					}
					string strConditionValue = ((int)BinTypeEnum.OK).ToString();
					strConditionValue += "' OR " + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINTYPEID_FLD + "='" + (int)BinTypeEnum.IN;
					htbCondition.Add(MST_BINTable.BINTYPEID_FLD, strConditionValue);
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Text.Trim(), htbCondition, true);
					}	
					if (drwResult != null)
					{
						dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Value = drwResult[MST_BINTable.CODE_FLD].ToString(); 
						dgrdData.Columns[CST_RecoverMaterialDetailTable.TOBINID_FLD].Value = int.Parse(drwResult[MST_BINTable.BINID_FLD].ToString()); 
					}
				}
				//Select Vendor
				if ((dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD]))
					&& ((dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() == string.Empty)
					&& (dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].ToString() == string.Empty)))
				{
					htbCondition.Add(CUSTOMER, (int)PartyTypeEnum.CUSTOMER);
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(V_VENDOR_CUSTOMER, MST_PartyTable.CODE_FLD, dgrdData[dgrdData.Row, MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(V_VENDOR_CUSTOMER, MST_PartyTable.CODE_FLD, dgrdData.Columns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Text.Trim(), htbCondition, true);
					}	
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						dgrdData.Columns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Value = drwResult[MST_PartyTable.CODE_FLD].ToString(); 
						dgrdData.Columns[CST_RecoverMaterialDetailTable.PARTYID_FLD].Value = int.Parse(drwResult[MST_PartyTable.PARTYID_FLD].ToString()); 
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
		/// Fill data for item after selected
		/// </summary>
		/// <param name="pdrowData"></param>
		private void FillItemDataToGrid(DataRow pdrowData)
		{
			try
			{
				dgrdData.EditActive = true;
				dgrdData.Columns[SO_DeliveryScheduleTable.LINE_FLD].Value = dgrdData.Row + 1;
				dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = int.Parse(pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString());
				dgrdData.Columns[ITM_ProductTable.CODE_FLD].Value = pdrowData[PART_NUMBER];
				dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Value	= pdrowData[PART_NAME];
				dgrdData.Columns[ITM_ProductTable.REVISION_FLD].Value = pdrowData[MODEL];
				dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value  = pdrowData[UM];
				dgrdData.Columns[CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD].Value  = pdrowData[ITM_ProductTable.STOCKUMID_FLD];
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
		/// ConfigGrid
		/// </summary>
		/// <param name="pblnLock"></param>
		/// <author>Trada</author>
		/// <date>Friday, Mar 3 2006</date>
		private void ConfigGrid(bool pblnLock)
		{
			try
			{
				dgrdData.Enabled = true;
				for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].Locked = true;
				}
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Locked = pblnLock;

				dgrdData.Splits[0].DisplayColumns[CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD].Locked = pblnLock;
				
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = !pblnLock;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = !pblnLock;
				dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Button = !pblnLock;
				dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Button = !pblnLock;
				dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Button = !pblnLock;
				dgrdData.Splits[0].DisplayColumns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Button = !pblnLock;
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
		/// Bind data to grid
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <param name="pdstSource"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		public void BindDataToGrid(int pintMasterID, DataSet pdstSource)
		{
			dstData = new DataSet() ;
			//if (txtQuantity.Text.Trim() == string.Empty) return;
			decimal decQuantity = 0;
			try
			{
				decQuantity = decimal.Parse(txtQuantity.Value.ToString());
			}
			catch
			{}
			if (pdstSource == null)
			{
				dstData = boRecoverable.ListByMasterID(pintMasterID);
			}
			else
			{
				
				if (pdstSource.Tables[0].Rows.Count != 0)
				{
					//Calculate Recoverable Quantity
					foreach (DataRow drow in pdstSource.Tables[0].Rows)
					{
						if (drow.RowState == DataRowState.Deleted) continue;
						drow[CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD] = (decQuantity * (decimal)drow[ITM_BOMTable.QUANTITY_FLD]);
					}
				}
				dstData	= pdstSource.Copy();
				for (int i = 0; i <dstData.Tables[0].Rows.Count; i++)
				{
					dstData.Tables[0].Rows[i][SO_DeliveryScheduleTable.LINE_FLD] = i+1;
				}
			}
			
			dgrdData.DataSource = dstData.Tables[0];
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
			ConfigGrid(false);
			
		}

		/// <summary>
		/// txtLocation_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		private void txtLocation_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtLocation_Validating()";
			try
			{
				if (txtLocation.Text.Trim() == string.Empty)
				{
					//clear form 
					txtLocation.Tag = null;
					lblLocation.Tag = null;
					txtBin.Text = string.Empty;
					txtBin.Tag = null;
					txtQuantity.Value = null;
					txtAvailable.Value = null;
					return;
				}
				if (!txtLocation.Modified) return;
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, txtLocation.Text, htbCondition, false);
				if (drwResult != null)
				{
					if (txtLocation.Tag != null)
					{
						if (txtLocation.Tag.ToString() != drwResult[MST_LocationTable.LOCATIONID_FLD].ToString())
						{
							//Clear Bin
							txtBin.Text = string.Empty;
							txtBin.Tag = null;
						}
					}
					txtLocation.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
					lblLocation.Tag = drwResult[MST_LocationTable.MASTERLOCATIONID_FLD];
					txtLocation.Tag = int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString());
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
		/// <summary>
		/// txtBin_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		private void txtBin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBin_Validating()";
			try
			{
				if(txtBin.Text.Trim() == string.Empty)
				{
					txtBin.Tag = null;
					txtAvailable.Value = null;
					txtQuantity.Value = null;
					return;
				}
				if (!txtBin.Modified) return;
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				
				if (txtLocation.Tag != null)
				{
					htbCondition.Add(MST_BINTable.LOCATIONID_FLD, txtLocation.Tag.ToString());
					htbCondition.Add(MST_BINTable.BINTYPEID_FLD, (int)BinTypeEnum.LS);
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Warning);
					e.Cancel = true;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, txtBin.Text, htbCondition, false);
				if (drwResult != null)
				{
					txtBin.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
					txtBin.Tag = int.Parse(drwResult[MST_BINTable.BINID_FLD].ToString());
					if (txtCode.Tag != null)
					{
//						decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//							Convert.ToInt32(lblLocation.Tag), Convert.ToInt32(txtLocation.Tag.ToString()), Convert.ToInt32(txtBin.Tag), Convert.ToInt32(txtCode.Tag));
						decimal decAvailableQuantityBin = new InventoryUtilsBO().GetAvailableQtyByPostDate(dtmCurrentDate, int.Parse(cboCCN.SelectedValue.ToString()),
							Convert.ToInt32(lblLocation.Tag), Convert.ToInt32(txtLocation.Tag.ToString()), Convert.ToInt32(txtBin.Tag), Convert.ToInt32(txtCode.Tag));
						txtQuantity.Value = txtAvailable.Value = decAvailableQuantityBin;	
					}
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
		/// <summary>
		/// txtCode_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		private void txtCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCode_Validating()";
			try 
			{
				if (txtCode.Text.Trim() == string.Empty)
				{
					txtCode.Tag = null;
					txtCode.Text = string.Empty;
					txtDescription.Text = string.Empty;
					txtRevision.Text = string.Empty;
					txtAvailable.Value = null;
					txtQuantity.Value = null;
					//Clear grid
					BindDataToGrid(0, boRecoverable.ListBomOfProduct(0));
					return;
				}
				if (!txtCode.Modified) return;
				DataRowView drwResult = null;
				Hashtable htbConditon = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbConditon.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					e.Cancel = true;
				}
				htbConditon.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
				drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtCode.Text, htbConditon, false);
				if (drwResult != null)
				{
					FillDataFromDataRowViewToControls(drwResult);
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
		/// <summary>
		/// txtDescription_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		private void txtDescription_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDescription_Validating()";
			try 
			{
				if (txtDescription.Text.Trim() == string.Empty)
				{
					txtCode.Tag = null;
					txtCode.Text = string.Empty;
					txtDescription.Text = string.Empty;
					txtRevision.Text = string.Empty;
					txtAvailable.Value = null;
					txtQuantity.Value = null;
					//Clear grid
					BindDataToGrid(0, boRecoverable.ListBomOfProduct(0));
					return;
				}
				if (!txtDescription.Modified) return;
				DataRowView drwResult = null;
				Hashtable htbConditon = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbConditon.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					e.Cancel = true;
				}
				htbConditon.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
				drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtDescription.Text, htbConditon, false);
				if (drwResult != null)
				{
					FillDataFromDataRowViewToControls(drwResult);
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
		/// <summary>
		/// txtQuantity_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		private void txtQuantity_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtQuantity_Validating()";
			try 
			{
				if (txtQuantity.Text.Trim() == string.Empty)
					return;
				Decimal decQuantity = 0;
				try
				{
					decQuantity = decimal.Parse(txtQuantity.Value.ToString());
				}
				catch
				{
					
				}
				
				if (decQuantity <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_WO_ORDERQUANTITY, MessageBoxIcon.Warning);
					e.Cancel = true;
				}
				if (txtAvailable.Text.Trim() != string.Empty)
				{
					Decimal decAvailableQty = 0;
					try
					{
						decAvailableQty = decimal.Parse(txtAvailable.Value.ToString());
					}
					catch
					{
						
					}
					
					if (decQuantity > decAvailableQty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MRB_OTHER_QUANTITY, MessageBoxIcon.Warning);
						e.Cancel = true;
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
		/// btnClose_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// dgrdData_BeforeColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try 
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				string strWhereClause = string.Empty;
				switch (e.Column.DataColumn.DataField)
				{
					case ITM_ProductTable.CODE_FLD:
					case ITM_ProductTable.DESCRIPTION_FLD:
						# region Open ComName search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (txtCode.Text.Trim() == string.Empty)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_SELECT_PRODUCT, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
							else
							{
								strWhereClause += ITM_ProductTable.PRODUCTID_FLD + " != " + txtCode.Tag.ToString();
								strWhereClause += " AND ((ParentProductID = " + txtCode.Tag.ToString() + " and MakeItem = 1) OR MakeItem = 0)";
							}
							string strCode = string.Empty, strDescription = string.Empty;
							if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
							{
								if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]))
									strCode = dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD].ToString();
								else
									strDescription = dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD].ToString();
							}
							else
							{
								if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]))
									strCode = dgrdData.Columns[ITM_ProductTable.CODE_FLD].Text.Trim();
								else
									strDescription = dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Text.Trim();
							}
							SelectItems frmSelectItem = new SelectItems(Convert.ToInt32(txtCode.Tag), strCode, strDescription);
							if (frmSelectItem.ShowDialog() == DialogResult.OK)
								drwResult = frmSelectItem.drvReturnDataRowView;
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;	
							}
							else
							{
								e.Cancel = true;
							}
						}
						#endregion
						break;
					case MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD:
						# region Open Location search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (dgrdData[dgrdData.Row, MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].ToString() != string.Empty)
							{
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;	
							}
							else
							{
								e.Cancel = true;
							}
						}
						else
						{
							//Clear Bin
							dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Value = string.Empty;
							//dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Value = string.Empty;
							dgrdData.Columns[CST_RecoverMaterialDetailTable.TOBINID_FLD].Value = null;
							dgrdData.Columns[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].Value = null;
							//dgrdData.Refresh();

						}
						#endregion
						break;
					case MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD:
						# region Open Bin search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (dgrdData[dgrdData.Row, MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].ToString() != string.Empty)
							{
								return;
							}
							//User has entered Location
							if (dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() != string.Empty
								&& int.Parse(dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()) != 0)
							{
								htbCriteria.Add(MST_BINTable.LOCATIONID_FLD, int.Parse(dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()));
							}
							else //User has not entered Location
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_LOCATION, MessageBoxIcon.Warning);
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
								dgrdData.Focus();
								return;
							}
							string strConditionValue = ((int)BinTypeEnum.OK).ToString();
							strConditionValue += "' OR " + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINTYPEID_FLD + "='" + (int)BinTypeEnum.IN;
							htbCriteria.Add(MST_BINTable.BINTYPEID_FLD, strConditionValue);
							drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;	
							}
							else
							{
								e.Cancel = true;
							}
						}
						#endregion
						break;
					case MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD:
						# region Open Party search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (((dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() != string.Empty)
								//&&(int.Parse(dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString()) != 0))
								&&(int.Parse(dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()) != 0))
								||((dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].ToString() != string.Empty)
								//&& (int.Parse(dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].ToString()) != 0)))
								&& (int.Parse(dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString()) != 0)))
							{
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(V_VENDOR_CUSTOMER, MST_PartyTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
							if (drwResult != null)
							{
								e.Column.DataColumn.Tag = drwResult.Row;	
							}
							else
							{
								e.Cancel = true;
							}
						}
						#endregion
						break;
					case CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD:
						# region Check Quantity
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (e.Column.DataColumn.Text == string.Empty)
							{
								return;
							}
							try
							{
								if (decimal.Parse(e.Column.DataColumn.Text) <= 0)
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_WO_ORDERQUANTITY, MessageBoxIcon.Warning);
									e.Cancel = true;
								}
							}
							catch
							{
								e.Cancel = true;
							}
						}
						#endregion
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
		/// <summary>
		/// dgrdData_AfterColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try 
			{
				DataRow drwResult = (DataRow) e.Column.DataColumn.Tag;
				//Fill Data to ComNumber
				if (e.Column.DataColumn.DataField == ITM_ProductTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.REVISION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = null;
						dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						FillItemDataToGrid(drwResult);
						
					}
				}
				//Fill Data to ComName
				if(e.Column.DataColumn.DataField == ITM_ProductTable.DESCRIPTION_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.REVISION_FLD].Value	 = string.Empty;
						dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = null;
						dgrdData.Columns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Value = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;

						FillItemDataToGrid(drwResult);
					}
				}
				//Fill Data to Location
				if (e.Column.DataColumn.DataField == MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() == string.Empty))
					{
						
					}
					else
					{
						if ((dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD] != null)
							&&(dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD] != DBNull.Value))
						{
							if (int.Parse(dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()) != int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString()))
							{
								dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Value = string.Empty;
								dgrdData.Columns[CST_RecoverMaterialDetailTable.TOBINID_FLD].Value = string.Empty;
							}
						}
						dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Value = drwResult[MST_LocationTable.CODE_FLD].ToString();
						dgrdData.Columns[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].Value = int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString());
					}
				}
				//Fill Data to Bin
				if (e.Column.DataColumn.DataField.ToLower() == (MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD).ToLower())
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[CST_RecoverMaterialDetailTable.TOBINID_FLD].Value	 = null;
					}
					else
					{
						dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Value = drwResult[MST_BINTable.CODE_FLD];
						dgrdData.Columns[CST_RecoverMaterialDetailTable.TOBINID_FLD].Value = int.Parse(drwResult[MST_BINTable.BINID_FLD].ToString());
					}
				}
				//Fill Data to Customer
				if (e.Column.DataColumn.DataField == MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[CST_RecoverMaterialDetailTable.PARTYID_FLD].Value = string.Empty;
					}
					else
					{
						dgrdData.Columns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Value = drwResult[MST_PartyTable.CODE_FLD];
						dgrdData.Columns[CST_RecoverMaterialDetailTable.PARTYID_FLD].Value = int.Parse(drwResult[MST_PartyTable.PARTYID_FLD].ToString());
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
		/// ValidateData
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		private bool ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			try
			{
				if (FormControlComponents.CheckMandatory(dtmPostDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return false;
				}
				if (!FormControlComponents.CheckDateInCurrentPeriod((DateTime)dtmPostDate.Value))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return false;
				}
				//check the PostDate smaller than the current date
				if ((DateTime)dtmPostDate.Value > new UtilsBO().GetDBDate())
				{
					//MessageBox.Show("The Post Date you input is not in the current period");
					PCSMessageBox.Show(ErrorCode.MESSAGE_INV_TRANSACTION_CANNOT_IN_FUTURE,MessageBoxIcon.Warning);
					dtmPostDate.Focus();
					return false;
				}
				if (FormControlComponents.CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboCCN.Focus();
					cboCCN.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtOrderNo))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtOrderNo.Focus();
					txtOrderNo.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtLocation))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtLocation.Focus();
					txtLocation.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtBin))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtBin.Focus();
					txtBin.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtCode.Focus();
					txtCode.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtDescription))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtDescription.Focus();
					txtDescription.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtQuantity))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtQuantity.Focus();
					txtQuantity.Select();
					return false;
				}
				if (txtQuantity.Text != string.Empty)
				{
					if ((decimal)txtQuantity.Value == 0)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_WO_ORDERQUANTITY, MessageBoxIcon.Warning);
						txtQuantity.Focus();
						txtQuantity.Select();
						return false;	
					}
				}
				//check data in the grid
				if (dgrdData.RowCount == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_PLEASE_ENTER_DETAIL_INFOR, MessageBoxIcon.Warning);
					dgrdData.Focus();
					return false;
				}
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, ITM_ProductTable.CODE_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_BOM_COMPONENTNOTEXIST, MessageBoxIcon.Warning);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
						dgrdData.Focus();
						return false;
					}
					if (dgrdData[i, ITM_ProductTable.DESCRIPTION_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_BOM_COMPONENTNOTEXIST, MessageBoxIcon.Warning);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD]);
						dgrdData.Focus();
						return false;
					}
					if (dgrdData[i, CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD].ToString() == string.Empty)
					{
						string[] strParam = new string[1];
						strParam[0] = RECOVERABLE_QTY;
						PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Warning, strParam);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD]);
						dgrdData.Focus();
						return false;
					}
					else
					{
						if (decimal.Parse(dgrdData[i, CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD].ToString()) <= 0)
						{
							string[] strParam = new string[2];
							strParam[0] = RECOVERABLE_QTY;
							strParam[1] = 0.ToString();
							PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD]);
							dgrdData.Focus();
							return false;	
						}
					}
					if ((dgrdData[i, CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString() == string.Empty)
						&&(dgrdData[i, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString() == string.Empty)
						&&(dgrdData[i, CST_RecoverMaterialDetailTable.PARTYID_FLD].ToString() == string.Empty))
					{
						string[] strParam = new string[1];
						strParam[0] = LOC_OR_VENDOR;
						PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Warning, strParam);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
						dgrdData.Focus();
						return false;
					}
					if ((dgrdData[i, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString() != string.Empty)
						&& (int.Parse(dgrdData[i, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()) != 0))
					{
						if ((dgrdData[i, CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString() == string.Empty)
							||(int.Parse(dgrdData[i, CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString()) == 0))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_BIN_FOR_LOCATION, MessageBoxIcon.Warning);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD]);
							dgrdData.Focus();
							return false;
						}
					}
					
					if ((dgrdData[i, CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString() != string.Empty)
						&&(dgrdData[i, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString() != string.Empty)
						&&(dgrdData[i, CST_RecoverMaterialDetailTable.PARTYID_FLD].ToString() != string.Empty))
					{
						if ((int.Parse(dgrdData[i, CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString()) == 0)
						&&(int.Parse(dgrdData[i, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()) == 0)
						&&(int.Parse(dgrdData[i, CST_RecoverMaterialDetailTable.PARTYID_FLD].ToString()) == 0))
						{
							string[] strParam = new string[1];
							strParam[0] = LOC_OR_VENDOR;
							PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Warning, strParam);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
							dgrdData.Focus();
							return false;
						}
						if ((int.Parse(dgrdData[i, CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString()) == 0)
							&&(int.Parse(dgrdData[i, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()) != 0)
							&&(int.Parse(dgrdData[i, CST_RecoverMaterialDetailTable.PARTYID_FLD].ToString()) == 0))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_BIN_FOR_LOCATION, MessageBoxIcon.Warning);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD]);
							dgrdData.Focus();
							return false;
						}
					}
				}
				//Check postdate in configuration
				if (!FormControlComponents.CheckPostDateInConfiguration((DateTime)dtmPostDate.Value))
				{
					return false;
				}
				return true;
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
		/// btnSave_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, </date>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try 
			{
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIRM_BEFORE_SAVE_DATA, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					blnHasError = true;
					if (!dgrdData.EditActive && ValidateData())
					{
						//Assign value to MasterVO
						voRecoverMaterialMaster = new CST_RecoverMaterialMasterVO();
						voRecoverMaterialMaster.CCNID = SystemProperty.CCNID;
						voRecoverMaterialMaster.MasterLocationID = SystemProperty.MasterLocationID;
						voRecoverMaterialMaster.FromBinID = int.Parse(txtBin.Tag.ToString());
						voRecoverMaterialMaster.FromLocationID = int.Parse(txtLocation.Tag.ToString());
						voRecoverMaterialMaster.PostDate = (DateTime) dtmPostDate.Value;
						voRecoverMaterialMaster.ProductID = int.Parse(txtCode.Tag.ToString());
						voRecoverMaterialMaster.Quantity = (decimal)txtQuantity.Value;
						voRecoverMaterialMaster.AvailableQty = (decimal)txtAvailable.Value;
						voRecoverMaterialMaster.TransNo = txtOrderNo.Text;
						voRecoverMaterialMaster.UserName = SystemProperty.UserName;
						//Save to database
						//New a dataset to update
						DataSet dstToUpdate = dstData.Clone();
						foreach (DataRow drow in dstData.Tables[0].Rows)
						{
							if (drow.RowState == DataRowState.Deleted) continue;
							DataRow drowData = dstToUpdate.Tables[0].NewRow();
							drowData.ItemArray = drow.ItemArray;
							dstToUpdate.Tables[0].Rows.Add(drowData);
						}
						//Reset ToBinID, ToLocationID, PartyID if needed
						foreach(DataRow drow in dstToUpdate.Tables[0].Rows)
						{
							if ((drow[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString() != string.Empty)
								&& (drow[CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString() != string.Empty))
							{
								if ((int.Parse(drow[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()) == 0) 
									&& (int.Parse(drow[CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString()) == 0))
								{
									drow[CST_RecoverMaterialDetailTable.TOBINID_FLD] = DBNull.Value;
									drow[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD] = DBNull.Value;
								}
							}
							if ((drow[CST_RecoverMaterialDetailTable.PARTYID_FLD].ToString() != string.Empty)
								&& (int.Parse(drow[CST_RecoverMaterialDetailTable.PARTYID_FLD].ToString()) == 0))
							{
								drow[CST_RecoverMaterialDetailTable.PARTYID_FLD] = DBNull.Value;
							}
							
						}
						intRecoverableMasterID = boRecoverable.AddAndReturnID(voRecoverMaterialMaster, dstToUpdate);
						PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
						FormMode = EnumAction.Default;
						SwitchFormMode();
						//reload grid form database
						dstData = boRecoverable.ListByMasterID(intRecoverableMasterID);
						//update Line column
						if (dstData.Tables[0].Rows.Count > 0)
						{
							for (int i = 0; i < dstData.Tables[0].Rows.Count; i++)
							{
								dstData.Tables[0].Rows[i][PRO_WorkOrderDetailTable.LINE_FLD] = i + 1;
							}
						}
						dgrdData.DataSource = dstData.Tables[0];
						//restore the layout of grid
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
						//Lock grid
						for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
						{
							dgrdData.Splits[0].DisplayColumns[i].Locked = true;
						}
						ConfigGrid(true);
						dgrdData.Refresh();
						blnHasError = false;
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
		/// txtOrderNo_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Mar 3 2006</date>
		private void txtOrderNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtOrderNo_Validating()";
			try 
			{
				if (btnOrderNo.Enabled)
				{
					if (txtOrderNo.Text.Trim() == string.Empty)
					{
						//ClearForm();
						dtmPostDate.Value = null;
						txtLocation.Text = string.Empty;
						txtLocation.Tag = null;
						txtBin.Text = string.Empty;
						txtBin.Tag = null;
						txtCode.Text = string.Empty;
						txtDescription.Text = string.Empty;
						txtRevision.Text = string.Empty;
						txtCode.Tag = null;
						txtQuantity.Value = null;
						txtAvailable.Value = null;
						dstData = boRecoverable.ListByMasterID(0);
						txtOrderNo.Tag = null;
						dgrdData.DataSource = dstData.Tables[0];
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
						return;
					}
					if (!txtOrderNo.Modified) return;
					DataRowView drwResult = null;
					Hashtable htbCondition = new Hashtable();
				
					if (cboCCN.SelectedValue != null)
					{
						htbCondition.Add(cst_FreightMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
					}
					else
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
						cboCCN.Focus();
						return;
					}
					drwResult = FormControlComponents.OpenSearchForm(VIEW_RECOVERABLEMATERIALMASTER, CST_RecoverMaterialMasterTable.TRANSNO_FLD, txtOrderNo.Text.Trim(), htbCondition, false);
					if (drwResult != null)
					{
						txtOrderNo.Text = drwResult[CST_RecoverMaterialMasterTable.TRANSNO_FLD].ToString();
						txtOrderNo.Tag = int.Parse(drwResult[CST_RecoverMaterialMasterTable.RECOVERMATERIALMASTERID_FLD].ToString());
						FillDataToAllControls(int.Parse(txtOrderNo.Tag.ToString()));
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
		/// <summary>
		/// RecoverableMaterial_Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <AUTHOR>Trada</AUTHOR>
		/// <date>Friday, Mar 3 2006</date>
		private void RecoverableMaterial_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".RecoverableMaterial_Closing()";
			try
			{
				if (FormMode != EnumAction.Default)
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

		
		private void txtOrderNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if((e.KeyCode == Keys.F4) && (btnOrderNo.Enabled))
			{
				btnOrderNo_Click(sender, e);
			}
		}

		private void txtLocation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4) 
			{
				btnLocation_Click(sender, e);
			}
		}

		private void txtBin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4) 
			{
				btnBin_Click(sender, e);
			}
		}

		private void txtCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4) 
			{
				btnSearchProductCode_Click(sender, e);
			}
		}

		private void txtDescription_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4) 
			{
				btnSearchProductDescription_Click(sender, e);
			}
		}
		/// <summary>
		/// dgrdData_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Mar 10 2006</date>
		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.Delete)
				{
					if (btnSave.Enabled)
					{
						FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
						int intCount  =0;
						foreach (DataRow objRow in dstData.Tables[0].Rows)
						{
							if(objRow.RowState != DataRowState.Deleted) 
								intCount++;
						}
						for (int i =0; i <intCount; i++)
							dgrdData[i, PRO_WorkOrderDetailTable.LINE_FLD] = i+1;
						
						dgrdData.Refresh();
						return;
					
					}
				}
				if (e.KeyCode == Keys.F4)
				{
					if (btnSave.Enabled)
					{
						dgrdData_ButtonClick(sender, null);
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
		/// Displays Recycled Slip
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPrint_Click()";
			const int MAX_ROW = 30;
			const string NO_FLD = "No";
			try
			{
				if (intRecoverableMasterID == 0)
					return;
				string REPORT_FILE = Application.StartupPath + @"\ReportDefinition\RecycledSlip.xml";
				if (!File.Exists(REPORT_FILE))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_FILE_NOT_FOUND, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				// if in grid has more than one to location or to bin, display error message and exit
				if (dstData == null || dstData.Tables.Count == 0)
					return;
				int intMasterID = Convert.ToInt32(txtOrderNo.Tag);
				bool blnHasMultiDest = boRecoverable.HasMultiDestination(intMasterID);
				if (blnHasMultiDest)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RECYCLED_HAS_MORE_THAN_ONE_DESTINATION, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				// gets report data
				DataTable dtbReportData = boRecoverable.GetDataForSlip(intMasterID);
				// insert No column
				dtbReportData.Columns.Add(new DataColumn(NO_FLD, typeof(int)));
				dtbReportData.Columns[NO_FLD].AllowDBNull = true;
				dtbReportData.Columns[NO_FLD].DefaultValue = DBNull.Value;
				// numbering
				int intNumber = 1;
				foreach (DataRow drowData in dtbReportData.Rows)
				{
					drowData[NO_FLD] = intNumber;
					intNumber ++;
					// reset row number for next page
					if (intNumber > MAX_ROW)
						intNumber = 1;
				}
				// remainder row of last page
				int intDiff = MAX_ROW - (dtbReportData.Rows.Count % MAX_ROW);
				// insert dummie rows
				for (int i = 0; i < intDiff; i++)
				{
					DataRow drowNew = dtbReportData.NewRow();
					dtbReportData.Rows.Add(drowNew);
				}
				while (rptSlip.IsBusy)
					Application.DoEvents();
				// load report information
				string[] arrstrReportInDefinitionFile = rptSlip.GetReportInfo(REPORT_FILE);
				rptSlip.Load(REPORT_FILE, arrstrReportInDefinitionFile[0]);
				arrstrReportInDefinitionFile = null;
				// bind data to report
				rptSlip.DataSource.Recordset = dtbReportData;
				try
				{
					rptSlip.Fields["fldCompany"].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
				}
				catch{}
				// render report
				rptSlip.Render();
				// displays report
				var dlgPreview = new C1PrintPreviewDialog();
				dlgPreview.ReportViewer.PreviewNavigationPanel.Visible = false;
				dlgPreview.ReportViewer.Document = rptSlip.Document;
				dlgPreview.FormTitle = this.Text;
				dlgPreview.Show();
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
		/// dgrdData_RowColChange
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Mar 21 2006</date>
		private void dgrdData_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_RowColChange()";
			try
			{
				if (dgrdData.DataSource == null) return;
				if (((dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString() != string.Empty)
					&&(int.Parse(dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()) != 0))
					||((dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString() != string.Empty)
					&& (int.Parse(dgrdData[dgrdData.Row, CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString()) != 0)))
				{
					dgrdData.Splits[0].DisplayColumns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Locked = true;
				}
				else
					dgrdData.Splits[0].DisplayColumns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Locked = false;

				if (dgrdData[dgrdData.Row, MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].ToString() != string.Empty)
				{
					dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Locked = true;
					dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Locked = true;
				}
				else
				{
					dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Locked = false;
					dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Locked = false;
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
	}
}
