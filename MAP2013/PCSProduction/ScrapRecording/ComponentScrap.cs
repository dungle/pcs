using System;
using System.Data;
using System.Drawing;
using C1.Win.C1TrueDBGrid;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PCSComMaterials.Inventory.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Utils;
using PCSUtils.Log;
using PCSUtils.MasterSetup;
using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.Common.BO;
using PCSComProduction.ScrapRecording.BO;
using PCSComProduction.ScrapRecording.DS;

namespace PCSProduction.ScrapRecording
{
	/// <summary>
	/// Summary description for ComponentScrap.
	/// </summary>
	public class ComponentScrap : System.Windows.Forms.Form
	{

		private System.Windows.Forms.Label lblMasLoc;
		private System.Windows.Forms.Label lblScrapNo;
		private System.Windows.Forms.Label lblPostDate;
		private System.Windows.Forms.Label lblCCN;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnFindScrapNo;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Button btnMasLoc;
		private System.Windows.Forms.TextBox txtMasLoc;
		private System.Windows.Forms.TextBox txtScrapNo;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		#region My variable
		protected const string THIS = "PCSProduction.ScrapRecording.ComponentScrap";
		#endregion My variable
		UtilsBO boUtil = new UtilsBO();
		private MST_MasterLocationVO voMasLoc = new MST_MasterLocationVO();
		private System.ComponentModel.Container components = null;
		protected DataTable dtbGridLayOut;
		private C1.Win.C1Input.C1DateEdit dtmPostDate;
		EnumAction formMode;
		private DataSet dstGridData;
		private const string UM = "MST_UnitOfMeasureCode";
		private const string AVAILABLE_QUANTITY = "AvailableQuantity";
		private const string ITM_PRODUCTCODE = "ITM_ProductCode";
		private const string MODEL = "ITM_ProductRevision";
		private const string ITM_PRODUCTDESCRIPTION = "ITM_ProductDescription";
		private const string WOLINE = "WOLine";
		private const string V_WODETAILANDPRODUCTINFO = "V_WODetailAndProductInfo";
		private const string V_PRO_WORKORDERBOMDETAIL = "v_PRO_WorkOrderBomDetail";
		private const string V_LOTBYWODETAILANDPRODUCT = "v_LotByWODetailAndProduct";
		private const string V_RELEASEDWORKORDER = "v_ReleasedAndMFClosedWorkOrder";
		private const string FROM_LOCATION = "FromLocation";
		private const string FROM_BIN = "FromBin";
		private const string TO_LOCATION = "ToLocation";
		private const string TO_BIN = "ToBin";
		private const string V_COMPONENTSCRAP = "V_ComponentScrap";
		private const string V_LOCATIONANDPRODUCTIONLINE = "V_LocationAndProductionLine";
		private const string V_BINEXCEPTDESTROY = "v_BinExceptDestroy";
		DataSet dstBackup;
		DateTime dtmCurrentDate = new DateTime();
		private bool blnHasError = false;
		private bool blnHasException = false;
		private int pintComponentScrapMasterID;
		private System.Windows.Forms.TextBox txtProductionLine;
		private System.Windows.Forms.Button btnProductionLine;
		private System.Windows.Forms.Label lblProductionLine;
		private decimal decAvailableQty;
		

		//int intMaxLine = 0;
		
		public ComponentScrap()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ComponentScrap));
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnFindScrapNo = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.txtMasLoc = new System.Windows.Forms.TextBox();
			this.lblCCN = new System.Windows.Forms.Label();
			this.lblMasLoc = new System.Windows.Forms.Label();
			this.lblScrapNo = new System.Windows.Forms.Label();
			this.lblPostDate = new System.Windows.Forms.Label();
			this.txtScrapNo = new System.Windows.Forms.TextBox();
			this.btnMasLoc = new System.Windows.Forms.Button();
			this.dtmPostDate = new C1.Win.C1Input.C1DateEdit();
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.btnProductionLine = new System.Windows.Forms.Button();
			this.lblProductionLine = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmPostDate)).BeginInit();
			this.SuspendLayout();
			// 
			// dgrdData
			// 
			this.dgrdData.AllowAddNew = true;
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
			this.dgrdData.Location = new System.Drawing.Point(6, 72);
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
			this.dgrdData.Size = new System.Drawing.Size(625, 340);
			this.dgrdData.TabIndex = 13;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Scrap Quant" +
				"ity\" DataField=\"ScrapQuantity\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
				"Column Level=\"0\" Caption=\"Available Quantity\" DataField=\"AvailableQuantity\"><Val" +
				"ueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Work Orde" +
				"r\" DataField=\"WorkOrderNo\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColu" +
				"mn Level=\"0\" Caption=\"WO Line\" DataField=\"WOLine\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"Part Number\" DataField=\"Code\"><Valu" +
				"eItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Com. Numbe" +
				"r\" DataField=\"ITM_ProductCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
				"Column Level=\"0\" Caption=\"Com. Name\" DataField=\"ITM_ProductDescription\"><ValueIt" +
				"ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"UM\" DataField" +
				"=\"MST_UnitOfMeasureCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
				" Level=\"0\" Caption=\"Model\" DataField=\"ITM_ProductRevision\"><ValueItems /><GroupI" +
				"nfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Line\" DataField=\"Line\"><Va" +
				"lueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Reason\" " +
				"DataField=\"ScrapReasonDesc\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCol" +
				"umn Level=\"0\" Caption=\"From Location\" DataField=\"FromLocation\"><ValueItems /><Gr" +
				"oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"From Bin\" DataField=\"F" +
				"romBin\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Captio" +
				"n=\"To Location\" DataField=\"ToLocation\"><ValueItems /><GroupInfo /></C1DataColumn" +
				"><C1DataColumn Level=\"0\" Caption=\"To Bin\" DataField=\"ToBin\"><ValueItems /><Group" +
				"Info /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Part Name\" DataField=\"Des" +
				"cription\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capt" +
				"ion=\"Model\" DataField=\"Revision\"><ValueItems /><GroupInfo /></C1DataColumn></Dat" +
				"aCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRo" +
				"w{ForeColor:HighlightText;BackColor:Highlight;}Inactive{ForeColor:InactiveCaptio" +
				"nText;BackColor:InactiveCaption;}Style119{AlignHorz:Near;}Style118{AlignHorz:Nea" +
				"r;ForeColor:Maroon;}Style78{}Style79{}Style85{}Style73{}Style117{}Style116{}Styl" +
				"e72{}Style110{}Style70{AlignHorz:Near;ForeColor:Maroon;}Style71{AlignHorz:Near;}" +
				"Style76{AlignHorz:Near;}Style77{AlignHorz:Near;}Style74{}Style75{}Style84{}Style" +
				"87{}Style86{}Style81{}Style80{}Style83{AlignHorz:Near;}Style82{AlignHorz:Near;}F" +
				"ooter{}Style89{AlignHorz:Near;}Style88{AlignHorz:Near;ForeColor:Maroon;}Style108" +
				"{}Style109{}Style104{}Style105{}Style106{AlignHorz:Near;ForeColor:Maroon;}Style1" +
				"07{AlignHorz:Near;}Editor{}Style101{AlignHorz:Near;}Style102{}Style103{}Style94{" +
				"AlignHorz:Near;}Style95{AlignHorz:Near;}Style96{}Style97{}Style90{}Style91{}Styl" +
				"e92{}Style93{}RecordSelector{AlignImage:Center;}Style98{}Style99{}Heading{Wrap:T" +
				"rue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:" +
				"Center;}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Style17{Alig" +
				"nHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Selected{ForeColo" +
				"r:HighlightText;BackColor:Highlight;}Style122{}Style123{}Style120{}Style28{Align" +
				"Horz:Near;}Style27{}Style9{}Style26{}Style25{}Style29{AlignHorz:Near;}Style5{}St" +
				"yle4{}Style24{}Style6{}Style23{AlignHorz:Near;}Style121{}Style3{}Style22{AlignHo" +
				"rz:Near;ForeColor:Maroon;}Style21{}Style20{}OddRow{}Style38{}Style39{}Style36{}F" +
				"ilterBar{}Style34{AlignHorz:Near;}Style35{AlignHorz:Near;}Style32{}Style33{}Styl" +
				"e30{}Style49{}Style48{}Style31{}Style37{}Normal{Font:Microsoft Sans Serif, 8.25p" +
				"t;}Style47{AlignHorz:Near;}Style46{AlignHorz:Near;ForeColor:Maroon;}EvenRow{Back" +
				"Color:Aqua;}Style8{}Style7{}Style58{AlignHorz:Near;}Style59{AlignHorz:Near;}Styl" +
				"e2{}Style50{}Style51{}Style52{AlignHorz:Near;ForeColor:Maroon;}Style53{AlignHorz" +
				":Near;}Style54{}Style55{}Style56{}Style57{}Caption{AlignHorz:Center;}Style64{Ali" +
				"gnHorz:Near;ForeColor:Maroon;}Style112{AlignHorz:Near;ForeColor:Maroon;}Style69{" +
				"}Style68{}Style1{}Style67{}Style63{}Style62{}Style61{}Style60{}Style100{AlignHor" +
				"z:Near;ForeColor:Maroon;}Style66{}Style65{AlignHorz:Near;}Style115{}Style114{}St" +
				"yle111{}Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}St" +
				"yle113{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Na" +
				"me=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Marque" +
				"eStyle=\"DottedCellBorder\" RecordSelectorWidth=\"17\" DefRecSelWidth=\"17\" VerticalS" +
				"crollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 621, 336</ClientRect>" +
				"<BorderSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorSt" +
				"yle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><" +
				"FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me" +
				"=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Head" +
				"ing\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><Inact" +
				"iveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9" +
				"\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle p" +
				"arent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCol" +
				"s><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style88\" /><Style parent=\"S" +
				"tyle1\" me=\"Style89\" /><FooterStyle parent=\"Style3\" me=\"Style90\" /><EditorStyle p" +
				"arent=\"Style5\" me=\"Style91\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style93\" /><" +
				"GroupFooterStyle parent=\"Style1\" me=\"Style92\" /><Visible>True</Visible><ColumnDi" +
				"vider>DarkGray,Single</ColumnDivider><Width>38</Width><Height>15</Height><DCIdx>" +
				"9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle46\" /><Style parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visibl" +
				"e>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>131</Width>" +
				"<Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
				"yle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><Footer" +
				"Style parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /" +
				"><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Styl" +
				"e1\" me=\"Style56\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Column" +
				"Divider><Width>53</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C" +
				"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style" +
				"1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle paren" +
				"t=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><Grou" +
				"pFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivide" +
				"r>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>4</DCIdx></C1DisplayC" +
				"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style paren" +
				"t=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorSty" +
				"le parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\"" +
				" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><Colu" +
				"mnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>15</DCIdx></C" +
				"1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><St" +
				"yle parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><" +
				"EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=" +
				"\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visi" +
				"ble><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>16</" +
				"DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style" +
				"64\" /><Style parent=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
				"le66\" /><EditorStyle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"St" +
				"yle1\" me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>T" +
				"rue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>108</Width><He" +
				"ight>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle" +
				" parent=\"Style2\" me=\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterSty" +
				"le parent=\"Style3\" me=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><G" +
				"roupHeaderStyle parent=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\"" +
				" me=\"Style74\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDiv" +
				"ider><Width>129</Width><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style82\" /><Style parent=\"Style1\"" +
				" me=\"Style83\" /><FooterStyle parent=\"Style3\" me=\"Style84\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style85\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style87\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible><ColumnDivider>" +
				"DarkGray,Single</ColumnDivider><Width>54</Width><Height>15</Height><DCIdx>8</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style76\"" +
				" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle parent=\"Style3\" me=\"Style7" +
				"8\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Style80\" /><Visible>True" +
				"</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>33</Width><Height" +
				">15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style100\" /><Style parent=\"Style1\" me=\"Style101\" /><FooterStyle" +
				" parent=\"Style3\" me=\"Style102\" /><EditorStyle parent=\"Style5\" me=\"Style103\" /><G" +
				"roupHeaderStyle parent=\"Style1\" me=\"Style105\" /><GroupFooterStyle parent=\"Style1" +
				"\" me=\"Style104\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style2\" me=\"Style106\" /><Style parent=\"Style1\" me=\"Style107\"" +
				" /><FooterStyle parent=\"Style3\" me=\"Style108\" /><EditorStyle parent=\"Style5\" me=" +
				"\"Style109\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style111\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style110\" /><Visible>True</Visible><ColumnDivider>DarkGray," +
				"Single</ColumnDivider><Height>15</Height><DCIdx>12</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\"" +
				" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>" +
				"DarkGray,Single</ColumnDivider><Width>98</Width><Height>15</Height><DCIdx>0</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\"" +
				" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style3" +
				"0\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True" +
				"</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>97</Width><Height" +
				">15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style112\" /><Style parent=\"Style1\" me=\"Style113\" /><FooterStyle" +
				" parent=\"Style3\" me=\"Style114\" /><EditorStyle parent=\"Style5\" me=\"Style115\" /><G" +
				"roupHeaderStyle parent=\"Style1\" me=\"Style117\" /><GroupFooterStyle parent=\"Style1" +
				"\" me=\"Style116\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Height>15</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style2\" me=\"Style118\" /><Style parent=\"Style1\" me=\"Style119\"" +
				" /><FooterStyle parent=\"Style3\" me=\"Style120\" /><EditorStyle parent=\"Style5\" me=" +
				"\"Style121\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style123\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style122\" /><Visible>True</Visible><ColumnDivider>DarkGray," +
				"Single</ColumnDivider><Height>15</Height><DCIdx>14</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style94\" /><Style parent=\"Style1\"" +
				" me=\"Style95\" /><FooterStyle parent=\"Style3\" me=\"Style96\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style97\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style99\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style98\" /><Visible>True</Visible><ColumnDivider>" +
				"DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>10</DCIdx></C1DisplayCo" +
				"lumn></internalCols></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style" +
				" parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"He" +
				"ading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Headi" +
				"ng\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal" +
				"\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal" +
				"\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me" +
				"=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Capti" +
				"on\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSpli" +
				"ts><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientAr" +
				"ea>0, 0, 621, 336</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><Pr" +
				"intPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnEdit.Location = new System.Drawing.Point(230, 426);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(60, 23);
			this.btnEdit.TabIndex = 17;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.Visible = false;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(567, 424);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 20;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(507, 424);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 19;
			this.btnHelp.Text = "&Help";
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(334, 428);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 18;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Visible = false;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(64, 424);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 15;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAdd.Location = new System.Drawing.Point(4, 424);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(60, 23);
			this.btnAdd.TabIndex = 14;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPrint.Location = new System.Drawing.Point(124, 424);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(60, 23);
			this.btnPrint.TabIndex = 16;
			this.btnPrint.Text = "&Print";
			// 
			// btnFindScrapNo
			// 
			this.btnFindScrapNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFindScrapNo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnFindScrapNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnFindScrapNo.Location = new System.Drawing.Point(196, 50);
			this.btnFindScrapNo.Name = "btnFindScrapNo";
			this.btnFindScrapNo.Size = new System.Drawing.Size(24, 20);
			this.btnFindScrapNo.TabIndex = 9;
			this.btnFindScrapNo.Text = "...";
			this.btnFindScrapNo.Click += new System.EventHandler(this.btnFindScrapNo_Click);
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
			this.cboCCN.ContentHeight = 15;
			this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
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
			this.cboCCN.Text = "CCN";
			this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboCCN.Leave += new System.EventHandler(this.OnLeaveControl);
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
			// txtMasLoc
			// 
			this.txtMasLoc.Location = new System.Drawing.Point(74, 28);
			this.txtMasLoc.Name = "txtMasLoc";
			this.txtMasLoc.Size = new System.Drawing.Size(120, 20);
			this.txtMasLoc.TabIndex = 5;
			this.txtMasLoc.Text = "";
			this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
			this.txtMasLoc.Leave += new System.EventHandler(this.txtMasLoc_Leave);
			this.txtMasLoc.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblCCN
			// 
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(508, 6);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(32, 20);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMasLoc
			// 
			this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
			this.lblMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMasLoc.Location = new System.Drawing.Point(5, 50);
			this.lblMasLoc.Name = "lblMasLoc";
			this.lblMasLoc.Size = new System.Drawing.Size(63, 20);
			this.lblMasLoc.TabIndex = 7;
			this.lblMasLoc.Text = "Scrap No.";
			this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblScrapNo
			// 
			this.lblScrapNo.ForeColor = System.Drawing.Color.Maroon;
			this.lblScrapNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblScrapNo.Location = new System.Drawing.Point(5, 26);
			this.lblScrapNo.Name = "lblScrapNo";
			this.lblScrapNo.Size = new System.Drawing.Size(63, 20);
			this.lblScrapNo.TabIndex = 4;
			this.lblScrapNo.Text = "Mas. Loc.";
			this.lblScrapNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPostDate
			// 
			this.lblPostDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblPostDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPostDate.Location = new System.Drawing.Point(5, 6);
			this.lblPostDate.Name = "lblPostDate";
			this.lblPostDate.Size = new System.Drawing.Size(63, 20);
			this.lblPostDate.TabIndex = 2;
			this.lblPostDate.Text = "Post Date";
			this.lblPostDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtScrapNo
			// 
			this.txtScrapNo.Location = new System.Drawing.Point(74, 50);
			this.txtScrapNo.Name = "txtScrapNo";
			this.txtScrapNo.Size = new System.Drawing.Size(120, 20);
			this.txtScrapNo.TabIndex = 8;
			this.txtScrapNo.Text = "";
			this.txtScrapNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScrapNo_KeyDown);
			this.txtScrapNo.Leave += new System.EventHandler(this.txtScrapNo_Leave);
			this.txtScrapNo.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// btnMasLoc
			// 
			this.btnMasLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnMasLoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnMasLoc.Location = new System.Drawing.Point(196, 28);
			this.btnMasLoc.Name = "btnMasLoc";
			this.btnMasLoc.Size = new System.Drawing.Size(24, 20);
			this.btnMasLoc.TabIndex = 6;
			this.btnMasLoc.Text = "...";
			this.btnMasLoc.Click += new System.EventHandler(this.btnMasLoc_Click);
			// 
			// dtmPostDate
			// 
			// 
			// dtmPostDate.Calendar
			// 
			this.dtmPostDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmPostDate.CustomFormat = "dd-MM-yyyy HH:mm";
			this.dtmPostDate.EmptyAsNull = true;
			this.dtmPostDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmPostDate.Location = new System.Drawing.Point(74, 6);
			this.dtmPostDate.Name = "dtmPostDate";
			this.dtmPostDate.Size = new System.Drawing.Size(120, 20);
			this.dtmPostDate.TabIndex = 3;
			this.dtmPostDate.Tag = null;
			this.dtmPostDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmPostDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtmPostDate_Validating);
			this.dtmPostDate.Enter += new System.EventHandler(this.OnEnterControl);
			this.dtmPostDate.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtProductionLine
			// 
			this.txtProductionLine.Location = new System.Drawing.Point(314, 50);
			this.txtProductionLine.Name = "txtProductionLine";
			this.txtProductionLine.Size = new System.Drawing.Size(120, 20);
			this.txtProductionLine.TabIndex = 11;
			this.txtProductionLine.Text = "";
			this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
			// 
			// btnProductionLine
			// 
			this.btnProductionLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnProductionLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnProductionLine.Location = new System.Drawing.Point(436, 50);
			this.btnProductionLine.Name = "btnProductionLine";
			this.btnProductionLine.Size = new System.Drawing.Size(24, 20);
			this.btnProductionLine.TabIndex = 12;
			this.btnProductionLine.Text = "...";
			this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
			// 
			// lblProductionLine
			// 
			this.lblProductionLine.ForeColor = System.Drawing.Color.Maroon;
			this.lblProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblProductionLine.Location = new System.Drawing.Point(226, 50);
			this.lblProductionLine.Name = "lblProductionLine";
			this.lblProductionLine.Size = new System.Drawing.Size(92, 20);
			this.lblProductionLine.TabIndex = 10;
			this.lblProductionLine.Text = "Production Line";
			this.lblProductionLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ComponentScrap
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(632, 453);
			this.Controls.Add(this.txtProductionLine);
			this.Controls.Add(this.btnProductionLine);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.dtmPostDate);
			this.Controls.Add(this.txtScrapNo);
			this.Controls.Add(this.txtMasLoc);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.btnMasLoc);
			this.Controls.Add(this.btnFindScrapNo);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblMasLoc);
			this.Controls.Add(this.lblScrapNo);
			this.Controls.Add(this.lblPostDate);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnPrint);
			this.KeyPreview = true;
			this.Name = "ComponentScrap";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Component Scrap Recording";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ComponentScrap_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ComponentScrap_Closing);
			this.Load += new System.EventHandler(this.ComponentScrap_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmPostDate)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		private void btnFindScrapNo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnFindScrapNo_Click()";
			try 
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
					//User has enter MasLoc
					if (txtMasLoc.Text.ToString() != string.Empty)
					{
						htbCriteria.Add(PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD, voMasLoc.MasterLocationID.ToString());	
					}
					else //User has not entered MasLoc
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
						txtMasLoc.Focus();
						return;
					}
					drwResult = FormControlComponents.OpenSearchForm(PRO_ComponentScrapMasterTable.TABLE_NAME, PRO_ComponentScrapMasterTable.SCRAPNO_FLD, txtScrapNo.Text.Trim(), htbCriteria, true);
					if (drwResult != null)
					{
						txtScrapNo.Text = drwResult[PRO_ComponentScrapMasterTable.SCRAPNO_FLD].ToString();
						//Keep valua of ComponentScrapMasterID 
						txtScrapNo.Tag = drwResult[PRO_ComponentScrapMasterTable.COMPONENTSCRAPMASTERID_FLD];
						pintComponentScrapMasterID = int.Parse(txtScrapNo.Tag.ToString());
						dtmPostDate.Value = (DateTime) drwResult[PRO_ComponentScrapMasterTable.POSTDATE_FLD];
					}
					else
					{
						txtScrapNo.Focus();
						return;
					}
					
					FillDataGrid(int.Parse(txtScrapNo.Tag.ToString()));
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					ConfigGrid(true);
					btnEdit.Enabled = true;			
					btnDelete.Enabled = true;
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
		/// FillDataGrid by ComponentScrapMasterID
		/// </summary>
		/// <param name="pintComponentScrapMasterID"></param>
		private void FillDataGrid(int pintComponentScrapMasterID)
		{
			const string METHOD_NAME = THIS + ".FillDataGrid()";
			try
			{
				//Get data from PRO_ComponentScrapDetail Table by ComponentScrapMasterID
				ComponentScrapBO boComponentScrap = new ComponentScrapBO();
				//fill data to Production Line
				DataTable dtbProductionLine = boComponentScrap.GetProductionLineByScrapMasterID(pintComponentScrapMasterID);
				if (dtbProductionLine.Rows.Count > 0)
				{
					txtProductionLine.Text = dtbProductionLine.Rows[0][PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = dtbProductionLine.Rows[0][PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
				}
				dstGridData = boComponentScrap.GetComponentScrapDetailByMasterID(pintComponentScrapMasterID);
				//Get Available Quantity by WODetailID and ProductID
//				foreach (DataRow drow in dstGridData.Tables[0].Rows)
//				{
//					decAvailableQty = boComponentScrap.GetAvailableQuantity(int.Parse(drow[PRO_ComponentScrapDetailTable.COMPONENTID_FLD].ToString()), int.Parse(drow[PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD].ToString()));
//					drow[AVAILABLE_QUANTITY] = decAvailableQty;
//				}
				dgrdData.DataSource = dstGridData.Tables[0];
				//Lock grid
				for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].Locked = true;
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
		/// <summary>
		/// ComponentScrap_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, June 27 2005</date>
		private void ComponentScrap_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ComponentScrap_Load()";
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
				// Load CCN combo box
				DataSet dstCCN = boUtil.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
				dtmCurrentDate = boUtil.GetDBDate().AddDays(1);
				//Store grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				//Disable all controls except: Add, Update, Close, Help buttons
				formMode = EnumAction.Default;
                SwitchFormMode();
				btnEdit.Visible = false;
				btnDelete.Visible = false;
				//Set default Master Location
				FormControlComponents.SetDefaultMasterLocation(txtMasLoc);
				voMasLoc.MasterLocationID = SystemProperty.MasterLocationID;
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
		/// SwitchFormMode
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, June 28 2005</date>
		private void SwitchFormMode()
		{
			const string METHOD_NAME = THIS + ".SwitchFormMode()";
			try
			{
				switch (formMode)
				{
					case EnumAction.Default:
						btnAdd.Enabled = true;
						dtmPostDate.Enabled = false;
						txtMasLoc.Enabled = true;
						txtScrapNo.Enabled = true;
						btnSave.Enabled = false;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						dgrdData.AllowDelete = false;
						btnPrint.Enabled = false;
						btnFindScrapNo.Enabled = true;
						btnMasLoc.Enabled = true;
						txtProductionLine.Enabled = false;
						btnProductionLine.Enabled = false;
						ConfigGrid(true);
						break;
					case EnumAction.Add:
						btnAdd.Enabled = false;
						dtmPostDate.Enabled = true;
						txtMasLoc.Enabled = true;
						txtScrapNo.Enabled = true;
						btnSave.Enabled = true;
						btnMasLoc.Enabled = true;
						btnFindScrapNo.Enabled = false;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						txtProductionLine.Enabled = true;
						btnProductionLine.Enabled = true;
						ConfigGrid(false);
						dtmPostDate.Focus();
						dgrdData.AllowDelete = true;
						break;
					case EnumAction.Edit:
						btnEdit.Enabled = false;
						btnAdd.Enabled = false;
						dtmPostDate.Enabled = true;
						txtMasLoc.Enabled = true;
						txtScrapNo.Enabled = true;
						btnMasLoc.Enabled = true;
						btnFindScrapNo.Enabled = true;
						txtProductionLine.Enabled = true;
						btnProductionLine.Enabled = true;
						btnSave.Enabled = true;
						ConfigGrid(false);
						dtmPostDate.Focus();
						dgrdData.AllowDelete = true;
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
		/// <summary>
		/// 
		/// </summary>
		private void DisableSomeControls()
		{
			const string METHOD_NAME = THIS + ".DisableSomeControls()";
			try
			{
				dtmPostDate.Enabled = false;
				txtMasLoc.Enabled = false;
				txtScrapNo.Enabled = false;
				btnDelete.Enabled = false;
				btnEdit.Enabled = false;
				btnSave.Enabled = false;
				btnPrint.Enabled = false;
				btnFindScrapNo.Enabled = false;
				btnMasLoc.Enabled = false;
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
		/// btnMasLoc_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, June 30 2005</date>
		private void btnMasLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ComponentScrap_Load()";
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
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text, htbCriteria, true);
				if (drwResult != null)
				{
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					voMasLoc.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
				}
				else
				{
					txtMasLoc.Focus();
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
		/// InitVariable
		/// </summary>
		/// <author>Trada</author>
		/// <date>Thursday, June 30 2005</date>
		private void InitVariable()
		{
			const string METHOD_NAME = THIS + ".InitVariable()";
			try
			{
				// Load PostDate
				PRO_ComponentScrapMasterVO voComponentScrapMaster = new PRO_ComponentScrapMasterVO();
				voComponentScrapMaster.PostDate = boUtil.GetDBDate();
				if((DateTime.MinValue < voComponentScrapMaster.PostDate) && (voComponentScrapMaster.PostDate < DateTime.MaxValue))
					dtmPostDate.Value = voComponentScrapMaster.PostDate;
				else
					dtmPostDate.Value = DBNull.Value;
				//Fill Completion Number
				//txtScrapNo.Text = boUtil.GetNoByMask(PRO_ComponentScrapMasterTable.TABLE_NAME, PRO_ComponentScrapMasterTable.SCRAPNO_FLD, DateTime.Parse(dtmPostDate.Value.ToString()), string.Empty);
				txtScrapNo.Text = FormControlComponents.GetNoByMask(this);
				//Set focus to PostDate
				dtmPostDate.Focus();
				
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
		/// <date>Monday, June 27 2005</date>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ComponentScrap_Load()";
			try
			{
				//Switch form mode
				formMode = EnumAction.Add;
				//Clear form
				ClearForm();
				//Switch form Mode
				SwitchFormMode();
				//Disable Add, Delete
				btnAdd.Enabled = false;
				//Fill data to controls
				InitVariable();
				//Fill Default Master Location 
				FormControlComponents.SetDefaultMasterLocation(txtMasLoc);
				voMasLoc.MasterLocationID = SystemProperty.MasterLocationID;
				CreateDataSet();
				dgrdData.DataSource = dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				ConfigGrid(false);

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
		/// ConfigGrid
		/// </summary>
		/// <param name="pblnLock"></param>
		/// <author>Trada</author>
		/// <date>Thursday, June 30 2005</date>
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
				dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[WOLINE].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[ITM_PRODUCTCODE].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[FROM_BIN].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[FROM_LOCATION].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[TO_BIN].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[TO_LOCATION].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[PRO_ScrapReasonTable.SCRAPREASONDESC_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[AVAILABLE_QUANTITY].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[AVAILABLE_QUANTITY].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				if (!pblnLock)
				{
					dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Button = true;
					dgrdData.Splits[0].DisplayColumns[ITM_PRODUCTCODE].Button = true;
					dgrdData.Splits[0].DisplayColumns[FROM_BIN].Button = true;
					dgrdData.Splits[0].DisplayColumns[FROM_LOCATION].Button = true;
					dgrdData.Splits[0].DisplayColumns[TO_BIN].Button = true;
					dgrdData.Splits[0].DisplayColumns[TO_LOCATION].Button = true;
					dgrdData.Splits[0].DisplayColumns[WOLINE].Button = true;
					dgrdData.Splits[0].DisplayColumns[PRO_ScrapReasonTable.SCRAPREASONDESC_FLD].Button = true;
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
	
		/// <summary>
		/// ClearForm
		/// </summary>
		/// <author>Trada</author>
		/// <date>Thursday, June 30 2005</date>
		private void ClearForm()
		{
			const string METHOD_NAME = THIS + ".ClearForm()";
			try
			{
				dtmPostDate.Value = DBNull.Value;
				txtMasLoc.Text = string.Empty;
				txtScrapNo.Text = string.Empty;
				txtProductionLine.Text = string.Empty;
				txtProductionLine.Tag = null;
				txtMasLoc.Tag = null;
				voMasLoc = new MST_MasterLocationVO();
				txtScrapNo.Tag = null;
			
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
		/// <date>Thursday, June 30 2005</date>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{	
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIRM_BEFORE_SAVE_WOCOMPLETION, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, new string[] {dtmPostDate.Text}) == DialogResult.Yes)
				{
					blnHasError = true;
					if(Security.IsDifferencePrefix(this,lblScrapNo,txtScrapNo))
					{
						return;
					}
					if (!dgrdData.EditActive && ValidateData())
					{
						if (!blnHasException)
						{
							dstBackup = dstGridData.Copy();	
						}
						ComponentScrapBO boComponentScrap =	new ComponentScrapBO();
						//Make a new ComponentScrapMasterVO
						PRO_ComponentScrapMasterVO voComponentScrapMaster = new PRO_ComponentScrapMasterVO();
						voComponentScrapMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
						voComponentScrapMaster.MasterLocationID = voMasLoc.MasterLocationID;
						voComponentScrapMaster.ScrapNo = txtScrapNo.Text.Trim();
						voComponentScrapMaster.PostDate = (DateTime)dtmPostDate.Value;
						voComponentScrapMaster.ProductionLineID = int.Parse(txtProductionLine.Tag.ToString());
						if (formMode == EnumAction.Add)
						{
//							if (blnHasException && !dstGridData.HasChanges())
//							{
//								dstGridData = dstBackup.Copy();
//							}
							//Add this new VO to PRO_ComponentScrapMaster Table
							pintComponentScrapMasterID = boComponentScrap.AddComponentScrapAndReturnID(voComponentScrapMaster, dstGridData);
						}
						if (formMode == EnumAction.Edit)
						{
							//Update ComponentScrapMaster and Detail
							boComponentScrap.UpdateCompScrapMasterAndDetail(voComponentScrapMaster, dstGridData);
						}
						Security.UpdateUserNameModifyTransaction(this, PRO_ComponentScrapMasterTable.COMPONENTSCRAPMASTERID_FLD, pintComponentScrapMasterID);
						PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
						formMode = EnumAction.Default;
						SwitchFormMode();
						//reload grid form database
						dstGridData = boComponentScrap.GetComponentScrapDetailByMasterID(pintComponentScrapMasterID);
					
						dgrdData.DataSource = dstGridData.Tables[0];
						//restore the layout of grid
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
						ConfigGrid(false);
						//Lock all column in the grid
						for (int i = 0; i < dgrdData.Splits[0].DisplayColumns.Count; i++)
						{
							dgrdData.Splits[0].DisplayColumns[i].Locked = true;
						}
						//					ExtendedProButton extend = (ExtendedProButton) btnDelete.Tag;
						//					if (!extend.IsDisable)
						//					{
						btnDelete.Enabled = true;
						//					}
						//					extend = (ExtendedProButton) btnEdit.Tag;
						//					if (!extend.IsDisable)
						//					{
						btnEdit.Enabled = true;
						//					}
						blnHasError = false;
						btnAdd.Focus();
					}
				}
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				blnHasException = true;
				//Check if ScrapNo was duplicated
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					txtScrapNo.Focus();
					txtScrapNo.Select();
				}
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
				blnHasException = true;
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
		/// btnEdit_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{	
				formMode = EnumAction.Edit;
				SwitchFormMode();
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
		/// btnDelete_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, July 1 2005</date>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				//Delete ComponentScrapDetail and Master
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					ComponentScrapBO boComponentScrap = new ComponentScrapBO();
					boComponentScrap.DeleteComponentScrapMasterAndDetail(pintComponentScrapMasterID, dstGridData);
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					formMode = EnumAction.Default;
					string strMasLoc = txtMasLoc.Text.Trim();
					ClearForm();
					txtMasLoc.Text = strMasLoc;
					CreateDataSet();
					dgrdData.DataSource = dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					ConfigGrid(false);
					txtScrapNo.Focus();
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
		/// btnClose_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, June 27 2005</date>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// txtMasLoc_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void txtMasLoc_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_Leave()";
			try
			{
				OnLeaveControl(sender, e);
				UtilsBO boUtil = new UtilsBO();
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					if (btnAdd.Enabled)
					{
						txtScrapNo.Text = string.Empty;
					}
					CreateDataSet();
					dgrdData.DataSource = dstGridData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					ConfigGrid(false);
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
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text, htbCriteria, false);
				if (drwResult != null)
				{
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					voMasLoc.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
				}
				else
				{
					txtMasLoc.Focus();
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
		/// txtMasLoc_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void txtMasLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_KeyDown()";
			if (e.KeyCode == Keys.F4)
			{
				btnMasLoc_Click(sender, e);	
			}
		}
		/// <summary>
		/// CreateDataSet
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 27 2005</date></date?>
		private void CreateDataSet()
		{
			const string METHOD_NAME = THIS + ".CreateDataSet()";
			try
			{
				dstGridData = new DataSet();
				dstGridData.Tables.Add(PRO_ComponentScrapDetailTable.TABLE_NAME);

				//insert columns which is invisible but use to update
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_ComponentScrapDetailTable.COMPONENTSCRAPDETAILID_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderDetailTable.PRODUCTID_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderBomDetailTable.COMPONENTID_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_ComponentScrapDetailTable.SCRAPREASONID_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_ComponentScrapDetailTable.FROMBINID_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_ComponentScrapDetailTable.TOBINID_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.STOCKUMID_FLD);
				
				//insert display columns
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_ComponentScrapDetailTable.LINE_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_WorkOrderMasterTable.WORKORDERNO_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(WOLINE);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.CODE_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.DESCRIPTION_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.REVISION_FLD);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(ITM_PRODUCTCODE);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(ITM_PRODUCTDESCRIPTION);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(MODEL);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(UM);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(FROM_LOCATION);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(FROM_BIN);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD, typeof(decimal));
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(AVAILABLE_QUANTITY, typeof(decimal));
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(TO_LOCATION);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(TO_BIN);
				dstGridData.Tables[PRO_ComponentScrapDetailTable.TABLE_NAME].Columns.Add(PRO_ScrapReasonTable.SCRAPREASONDESC_FLD);
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
		/// ValidateData
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private bool ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			try
			{
				decimal decTotalScrapQty = 0;
				if (FormControlComponents.CheckMandatory(dtmPostDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return false;
				}
				//Check period
				if (!FormControlComponents.CheckDateInCurrentPeriod(DateTime.Parse(dtmPostDate.Value.ToString())))
				{	
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return false;
				}
				//Check Mandatory fields
				
				//check the PostDate smaller than the current date
				if ((DateTime)dtmPostDate.Value > new UtilsBO().GetDBDate())
				{
					//MessageBox.Show("The Post Date you input is not in the current period");
					PCSMessageBox.Show(ErrorCode.MESSAGE_INV_TRANSACTION_CANNOT_IN_FUTURE,MessageBoxIcon.Warning);
					dtmPostDate.Focus();
					return false;
				}

				if (FormControlComponents.CheckMandatory(txtMasLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtMasLoc.Focus();
					txtMasLoc.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtScrapNo))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtScrapNo.Focus();
					txtScrapNo.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtProductionLine))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtProductionLine.Focus();
					txtProductionLine.Select();
					return false;
				}
				//Check postdate in configuration
				if (!FormControlComponents.CheckPostDateInConfiguration((DateTime)dtmPostDate.Value))
				{
					return false;
				}
				//check if row in grid has data
				int intCountRow =0;
				for (int i =0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString() != string.Empty)
					{
						intCountRow++;
					}
				}
				if (intCountRow ==0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_PO_APPROVE_NO_DATA_IN_GRID);
					dgrdData.Row = 0;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.WORKORDERNO_FLD]);
					dgrdData.Focus();
					return false;
				}
				//check mandatory field in grid
				for (int i =0; i < dgrdData.RowCount; i++)
				{
					//Reset TotalScrap Quantity
					decTotalScrapQty = 0;
					//Check Line column
					if (dgrdData[i, PRO_ComponentScrapDetailTable.LINE_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_COMPONENT_SCRAP_ORDER_CANNOT_BE_NULL, MessageBoxIcon.Error);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_ComponentScrapDetailTable.LINE_FLD]);
						dgrdData.Focus();
						return false;
					}
					//Check Work Order column
					if (dgrdData[i, PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INPUT_WO, MessageBoxIcon.Error);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.WORKORDERNO_FLD]);
						dgrdData.Focus();
						return false;
					}
					else
					{
						//Check Work Order Line column
						if (dgrdData[i, WOLINE].ToString() == string.Empty)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INPUT_WO_LINE, MessageBoxIcon.Error);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[WOLINE]);
							dgrdData.Focus();
							return false;
						}
						else
						{
							//Check Component Number column
							if (dgrdData[i, ITM_PRODUCTCODE].ToString() == string.Empty)
							{
								PCSMessageBox.Show(ErrorCode.MSG_WOISSUE_MATERIAL_SELECT_COMPONENT_FIRST, MessageBoxIcon.Error);
								dgrdData.Row = i;
								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_PRODUCTCODE]);
								dgrdData.Focus();
								return false;
							}
							//Check Component Name column
							if (dgrdData[i, ITM_PRODUCTDESCRIPTION].ToString() == string.Empty)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_COMPONENT_SCRAP_PLS_INPUT_PARTNAME, MessageBoxIcon.Error);
								dgrdData.Row = i;
								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_PRODUCTDESCRIPTION]);
								dgrdData.Focus();
								return false;
							}
							//Check From Location
							if (dgrdData[i, FROM_LOCATION].ToString() == string.Empty)
							{
								PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
								dgrdData.Row = i;
								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[FROM_LOCATION]);
								dgrdData.Focus();
								return false;
							}
							//Check From Bin
							if (dgrdData[i, FROM_BIN].ToString() == string.Empty)
							{
								PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
								dgrdData.Row = i;
								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[FROM_BIN]);
								dgrdData.Focus();
								return false;
							}
							//Check Scrap Quantity
							if (dgrdData[i, PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].ToString() == string.Empty)
							{
								PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
								dgrdData.Row = i;
								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD]);
								dgrdData.Focus();
								return false;
							}
							//Check To Location
							if (dgrdData[i, TO_LOCATION].ToString() == string.Empty)
							{
								PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
								dgrdData.Row = i;
								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[TO_LOCATION]);
								dgrdData.Focus();
								return false;
							}
							//Check To Bin
							if (dgrdData[i, TO_BIN].ToString() == string.Empty)
							{
								PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
								dgrdData.Row = i;
								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[TO_BIN]);
								dgrdData.Focus();
								return false;
							}
						}
					}
					//Check Scrap Quantity column
					if (dgrdData[i, PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_COMPONENT_SCRAP_PLS_INPUT_SCRAPQUANTITY, MessageBoxIcon.Error);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD]);
						dgrdData.Focus();
						return false;
					}
					else
					{
						//Check if Total Scrap Quantity > Available quantity then show error message
						for (int j = i; j < dgrdData.RowCount; j++)
						{
							if (dgrdData[i, PRO_WorkOrderBomDetailTable.COMPONENTID_FLD].ToString() == dgrdData[j, PRO_WorkOrderBomDetailTable.COMPONENTID_FLD].ToString())
							{
								decTotalScrapQty += decimal.Parse(dgrdData[j, PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].ToString());
								//Check if Scrap Quantity > Available quantity then show error message
								if (decTotalScrapQty > decimal.Parse(dgrdData[i, AVAILABLE_QUANTITY].ToString()))
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_COMPONENT_SCRAP_SCRAPQUANTITY_MUST_BE_SMALLER_AVAILABLEQTY, MessageBoxIcon.Warning);
									dgrdData.Row = j;
									dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD]);
									dgrdData.Focus();
									return false;
								}
							}
						}
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
			return true;
		}
		/// <summary>
		/// dgrdData_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.F12:

						if((formMode == EnumAction.Edit) || (formMode == EnumAction.Add)) 
						{
							dgrdData.Row = dgrdData.RowCount;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WorkOrderMasterTable.WORKORDERNO_FLD]);
							dgrdData.Focus();
							dgrdData.EditActive = false;
						}
						
						break;
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
		/// <summary>
		/// Fill available Qty to grid
		/// </summary>
		/// <author>Trada</author>
		/// <date>Thursday, August 3 2006</date>
		private void FillAvailableQuantity()
		{
			decimal decAvailableQty = 0;
			if (dtmPostDate.Value != DBNull.Value 
				&& txtMasLoc.Text.Trim() != string.Empty
				&& dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].ToString() != string.Empty
				&& dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMBINID_FLD].ToString() != string.Empty
				&& dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.COMPONENTID_FLD].ToString() != string.Empty)
			{
//				decAvailableQty = (new InventoryUtilsBO()).GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value, int.Parse(cboCCN.SelectedValue.ToString()),
//					int.Parse(txtMasLoc.Tag.ToString()), int.Parse(dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].ToString()),
//					int.Parse(dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMBINID_FLD].ToString()), int.Parse(dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.COMPONENTID_FLD].ToString()));
				decAvailableQty = (new InventoryUtilsBO()).GetAvailableQtyByPostDate(dtmCurrentDate, int.Parse(cboCCN.SelectedValue.ToString()),
					int.Parse(txtMasLoc.Tag.ToString()), int.Parse(dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].ToString()),
					int.Parse(dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMBINID_FLD].ToString()), int.Parse(dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.COMPONENTID_FLD].ToString()));
				dgrdData[dgrdData.Row, AVAILABLE_QUANTITY] = decAvailableQty;
			}
		}
		/// <summary>
		/// dgrdData_ButtonClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, June 28 2005</date>
		private void dgrdData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				string strWhereClause = string.Empty;		
				if (!btnSave.Enabled) return;
				//open the search form to select Work Order Master
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WorkOrderMasterTable.WORKORDERNO_FLD]))
				{
					//User has entered Masloc
					if (txtMasLoc.Text.ToString() != string.Empty)
					{
						htbCriteria.Add(PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD, voMasLoc.MasterLocationID.ToString());
					}
					else //User has not entered MasLoc
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
						txtMasLoc.Focus();
						return;
					}
					//User has entered Production Line
					if (txtProductionLine.Text.ToString() != string.Empty)
					{
						htbCriteria.Add(PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD, txtProductionLine.Tag);
					}
					else //User has not entered MasLoc
					{
						string[] strParam = new string[2];
						strParam[0] = lblProductionLine.Text;
						strParam[1] = dgrdData.Columns[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Caption;
						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
						txtProductionLine.Focus();
						return;
					}
					if (dgrdData.AddNewMode != AddNewModeEnum.AddNewPending)
					{
						drwResult = FormControlComponents.OpenSearchForm(V_RELEASEDWORKORDER, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, dgrdData[dgrdData.Row, PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString(), htbCriteria, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(V_RELEASEDWORKORDER, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, dgrdData.Columns[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Text.Trim(), htbCriteria, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						int pintRow = dgrdData.Row;
						if (dgrdData[dgrdData.Row, PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString() != string.Empty)
						{
							if (int.Parse(dgrdData[dgrdData.Row,PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString()) != int.Parse(drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString()))
							{
								//clear relate information
								dgrdData[pintRow, WOLINE] = string.Empty;
								dgrdData[pintRow, PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD] = string.Empty;
								dgrdData[pintRow, ITM_ProductTable.CODE_FLD] = string.Empty;
								dgrdData[pintRow, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
								dgrdData[pintRow, ITM_ProductTable.REVISION_FLD] = string.Empty;
								dgrdData[pintRow, ITM_ProductTable.PRODUCTID_FLD] = string.Empty;
								dgrdData[pintRow, ITM_PRODUCTCODE] = string.Empty;
								dgrdData[pintRow, ITM_PRODUCTDESCRIPTION] = string.Empty;
								dgrdData[pintRow, MODEL] = string.Empty;
								dgrdData[pintRow, UM] = string.Empty;
								dgrdData[pintRow, AVAILABLE_QUANTITY] = string.Empty;
							}
						}
						dgrdData[pintRow, PRO_ComponentScrapDetailTable.LINE_FLD] = pintRow + 1;
						dgrdData[pintRow, PRO_WorkOrderMasterTable.WORKORDERNO_FLD] = drwResult[PRO_WorkOrderMasterTable.WORKORDERNO_FLD];
						dgrdData[pintRow,PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD] = drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD];
					}
				}
				//open the search form to select Work Order Line
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[WOLINE]))
				{
					//User has entered Work Order Master
					if (dgrdData[dgrdData.Row, PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString() != string.Empty)
					{
//						htbCriteria.Add(PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD, dgrdData.Columns[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].Value);
//						htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int)WOLineStatus.Released);
						strWhereClause += PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + " = " + dgrdData.Columns[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].Value.ToString();
						strWhereClause += " AND (" + PRO_WorkOrderDetailTable.STATUS_FLD + " = " + (int)WOLineStatus.Released;
						strWhereClause += " OR " + PRO_WorkOrderDetailTable.STATUS_FLD + " = " + (int)WOLineStatus.MfgClose + ")";
					}
					else //User has not entered Work Order Master
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INPUT_WO, MessageBoxIcon.Warning);
//						dgrdData[dgrdData.Row, WOLINE] = DBNull.Value;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WorkOrderMasterTable.WORKORDERNO_FLD]);
						dgrdData.Focus();
						return;
					}

					if (dgrdData.AddNewMode != AddNewModeEnum.AddNewPending)
					{
						//drwResult = FormControlComponents.OpenSearchForm(V_WODETAILANDPRODUCTINFO, PRO_WorkOrderDetailTable.LINE_FLD, dgrdData[dgrdData.Row, WOLINE].ToString(), htbCriteria, true);
						drwResult = FormControlComponents.OpenSearchForm(V_WODETAILANDPRODUCTINFO, PRO_WorkOrderDetailTable.LINE_FLD, dgrdData[dgrdData.Row, WOLINE].ToString(), strWhereClause);
					}
					else
					{
						//drwResult = FormControlComponents.OpenSearchForm(V_WODETAILANDPRODUCTINFO, PRO_WorkOrderDetailTable.LINE_FLD, dgrdData.Columns[WOLINE].Text.Trim(), htbCriteria, true);
						drwResult = FormControlComponents.OpenSearchForm(V_WODETAILANDPRODUCTINFO, PRO_WorkOrderDetailTable.LINE_FLD, dgrdData.Columns[WOLINE].Text.Trim(), strWhereClause);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						if (dgrdData[dgrdData.Row, WOLINE].ToString() != string.Empty)
						{
							if (int.Parse(dgrdData[dgrdData.Row,PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString()) != int.Parse(drwResult[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString()))
							{
								//clear relate information
								int row = dgrdData.Row;
								dgrdData[row, ITM_ProductTable.CODE_FLD] = string.Empty;
								dgrdData[row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
								dgrdData[row, ITM_ProductTable.REVISION_FLD] = string.Empty;
								dgrdData[row, ITM_ProductTable.PRODUCTID_FLD] = string.Empty;
								dgrdData[row, ITM_PRODUCTCODE] = string.Empty;
								dgrdData[row, ITM_PRODUCTDESCRIPTION] = string.Empty;
								dgrdData[row, MODEL] = string.Empty;
								dgrdData[row, UM] = string.Empty;
								dgrdData[row, AVAILABLE_QUANTITY] = string.Empty;
							}
						}
						dgrdData[dgrdData.Row, WOLINE] = drwResult[PRO_WorkOrderDetailTable.LINE_FLD].ToString();
						dgrdData[dgrdData.Row,ITM_ProductTable.CODE_FLD] = drwResult[ITM_ProductTable.CODE_FLD];
						dgrdData[dgrdData.Row,ITM_ProductTable.DESCRIPTION_FLD] = drwResult[ITM_ProductTable.DESCRIPTION_FLD];
						dgrdData[dgrdData.Row,ITM_ProductTable.REVISION_FLD] = drwResult[ITM_ProductTable.REVISION_FLD];
						dgrdData[dgrdData.Row,PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD] = drwResult[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD];
						dgrdData[dgrdData.Row,PRO_WorkOrderDetailTable.PRODUCTID_FLD] = drwResult[PRO_WorkOrderDetailTable.PRODUCTID_FLD];
						dgrdData[dgrdData.Row, PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD] = drwResult[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD];
					}
				}
				//open the search form to select From Location
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[FROM_LOCATION]))
				{
					//User has entered Work Order Detail
					if (dgrdData[dgrdData.Row, WOLINE].ToString() != string.Empty)
					{
						htbCriteria.Add(PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD, dgrdData.Columns[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD].Value);
					}
					else //User has not entered Work Order Detail 
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INPUT_WO_LINE, MessageBoxIcon.Warning);
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[WOLINE]);
						dgrdData.Focus();
						return;
					}
					if (dgrdData.AddNewMode != AddNewModeEnum.AddNewPending)
					{
						drwResult = FormControlComponents.OpenSearchForm(V_LOCATIONANDPRODUCTIONLINE, MST_LocationTable.CODE_FLD, dgrdData[dgrdData.Row, FROM_LOCATION].ToString(), htbCriteria, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(V_LOCATIONANDPRODUCTIONLINE, MST_LocationTable.CODE_FLD, dgrdData.Columns[FROM_LOCATION].Text.Trim(), htbCriteria, true);
					}
					//drwResult = FormControlComponents.OpenSearchForm(PRO_WORoutingTable.TABLE_NAME, PRO_WORoutingTable.STEP_FLD, dgrdData[dgrdData.Row, PRO_WORoutingTable.STEP_FLD].ToString(), htbCriteria, true);
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						if (dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].ToString() != string.Empty)
						{
							if (int.Parse(dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].ToString()) != int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString()))
							{
								//Clear Bin
								dgrdData[dgrdData.Row, FROM_BIN] = string.Empty;
								dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMBINID_FLD] = string.Empty;
								//clear available quantity
								dgrdData[dgrdData.Row, AVAILABLE_QUANTITY] = string.Empty;
							}
						}
						dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD] = drwResult[MST_LocationTable.LOCATIONID_FLD];
						dgrdData[dgrdData.Row, FROM_LOCATION] = drwResult[MST_LocationTable.CODE_FLD].ToString();

					}
				}
				//open the search form to select From Bin
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[FROM_BIN]))
				{
					//User has entered From Location
					if (dgrdData[dgrdData.Row, FROM_LOCATION].ToString() != string.Empty)
					{
						htbCriteria.Add(MST_BINTable.LOCATIONID_FLD, dgrdData.Columns[PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].Value);
					}
					else //User has not entered Work Order Detail 
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, MessageBoxIcon.Warning);
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[FROM_LOCATION]);
						dgrdData.Focus();
						return;
					}
					if (dgrdData.AddNewMode != AddNewModeEnum.AddNewPending)
					{
						drwResult = FormControlComponents.OpenSearchForm(V_BINEXCEPTDESTROY, MST_BINTable.CODE_FLD, dgrdData[dgrdData.Row, FROM_BIN].ToString(), htbCriteria, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(V_BINEXCEPTDESTROY, MST_BINTable.CODE_FLD, dgrdData.Columns[FROM_BIN].Text.Trim(), htbCriteria, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMBINID_FLD] = drwResult[MST_BINTable.BINID_FLD];
						dgrdData[dgrdData.Row, FROM_BIN] = drwResult[MST_BINTable.CODE_FLD].ToString();
						//fill available quantity 
						FillAvailableQuantity();
					}
				}
				//open the search form to select Component
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_PRODUCTCODE]))
				{
					//User has entered Work Order Detail
					if (dgrdData[dgrdData.Row, WOLINE].ToString() != string.Empty)
					{
						htbCriteria.Add(PRO_WorkOrderBomMasterTable.WORKORDERDETAILID_FLD, dgrdData.Columns[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].Value);
					}
					else //User has not entered Work Order Detail 
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INPUT_WO_LINE, MessageBoxIcon.Warning);
						#region HACK: DEL Trada 31-10-2005

//						dgrdData[dgrdData.Row, ITM_PRODUCTCODE] =  DBNull.Value;
//						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[WOLINE]);
//						dgrdData.Focus();

						#endregion 
						return;
					}
					if (dgrdData.AddNewMode != AddNewModeEnum.AddNewPending)
					{
						drwResult = FormControlComponents.OpenSearchForm(V_COMPONENTSCRAP, ITM_PRODUCTCODE, dgrdData[dgrdData.Row, ITM_PRODUCTCODE].ToString(), htbCriteria, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(V_COMPONENTSCRAP, ITM_PRODUCTCODE, dgrdData.Columns[ITM_PRODUCTCODE].Text.Trim(), htbCriteria, true);
					}
					//drwResult = FormControlComponents.OpenSearchForm(V_COMPONENTSCRAP, ITM_PRODUCTCODE, dgrdData[dgrdData.Row, ITM_PRODUCTCODE].ToString(), htbCriteria, true);
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row,ITM_PRODUCTCODE] = drwResult[ITM_PRODUCTCODE];
						dgrdData[dgrdData.Row,ITM_PRODUCTDESCRIPTION] = drwResult[ITM_PRODUCTDESCRIPTION];
						dgrdData[dgrdData.Row,UM] = drwResult[UM];
						dgrdData[dgrdData.Row,MODEL] = drwResult[MODEL];
						dgrdData[dgrdData.Row,PRO_ComponentScrapDetailTable.COMPONENTID_FLD] = drwResult[PRO_ComponentScrapDetailTable.COMPONENTID_FLD]; 
						dgrdData[dgrdData.Row, ITM_ProductTable.STOCKUMID_FLD] = drwResult[ITM_ProductTable.STOCKUMID_FLD];
						//Fill Available Quantity
						FillAvailableQuantity();	
					}
				}
				//open the search form to select To Location
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[TO_LOCATION]))
				{
					//User has entered Work Order Detail
					if (dgrdData[dgrdData.Row, WOLINE].ToString() != string.Empty)
					{
						htbCriteria.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, dgrdData.Columns[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].Value);
					}
					else //User has not entered Work Order Detail 
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INPUT_WO_LINE, MessageBoxIcon.Warning);
						return;
					}
					if (dgrdData.AddNewMode != AddNewModeEnum.AddNewPending)
					{
						drwResult = FormControlComponents.OpenSearchForm(V_LOCATIONANDPRODUCTIONLINE, MST_LocationTable.CODE_FLD, dgrdData[dgrdData.Row, TO_LOCATION].ToString(), htbCriteria, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(V_LOCATIONANDPRODUCTIONLINE, MST_LocationTable.CODE_FLD, dgrdData.Columns[TO_LOCATION].Text.Trim(), htbCriteria, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						if (dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD].ToString() != string.Empty)
						{
							if (int.Parse(dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD].ToString()) != int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString()))
							{
								//Clear Bin
								dgrdData[dgrdData.Row, TO_BIN] = string.Empty;
								dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.TOBINID_FLD] = string.Empty;
							}
						}
						dgrdData.Columns[TO_LOCATION].Value = drwResult[MST_LocationTable.CODE_FLD];
						//dgrdData[dgrdData.Row, TO_LOCATION] = drwResult[MST_LocationTable.CODE_FLD];
						dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD] = drwResult[MST_LocationTable.LOCATIONID_FLD];	
					}
				}
				//open the search form to select To Bin
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[TO_BIN]))
				{
					//User has entered To Location
					if (dgrdData[dgrdData.Row, TO_LOCATION].ToString() != string.Empty)
					{
						htbCriteria.Add(MST_BINTable.LOCATIONID_FLD, dgrdData.Columns[PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD].Value);
					}
					else
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, MessageBoxIcon.Warning);
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[TO_LOCATION]);
						dgrdData.Focus();
						return;
					}
					//Bin DS
					htbCriteria.Add(MST_BINTable.BINTYPEID_FLD, (int)BinTypeEnum.LS);
					if (dgrdData.AddNewMode != AddNewModeEnum.AddNewPending)
					{
						drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, dgrdData[dgrdData.Row, TO_BIN].ToString(), htbCriteria, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, dgrdData.Columns[TO_BIN].Text.Trim(), htbCriteria, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						//dgrdData[dgrdData.Row, TO_BIN] = drwResult[MST_BINTable.CODE_FLD];
						dgrdData.Columns[TO_BIN].Value = drwResult[MST_BINTable.CODE_FLD];
						dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.TOBINID_FLD] = drwResult[MST_BINTable.BINID_FLD];	
					}
				}
				//open the search form to select Scrap Reason
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ScrapReasonTable.SCRAPREASONDESC_FLD]))
				{
					if (dgrdData.AddNewMode != AddNewModeEnum.AddNewPending)
					{
						drwResult = FormControlComponents.OpenSearchForm(PRO_ScrapReasonTable.TABLE_NAME, PRO_ScrapReasonTable.SCRAPREASONDESC_FLD, dgrdData[dgrdData.Row, PRO_ScrapReasonTable.SCRAPREASONDESC_FLD].ToString(), null, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(PRO_ScrapReasonTable.TABLE_NAME, PRO_ScrapReasonTable.SCRAPREASONDESC_FLD, dgrdData.Columns[PRO_ScrapReasonTable.SCRAPREASONDESC_FLD].Text.Trim(), null, true);
					}
					//drwResult = FormControlComponents.OpenSearchForm(PRO_ScrapReasonTable.TABLE_NAME, PRO_ScrapReasonTable.SCRAPREASONDESC_FLD, dgrdData[dgrdData.Row, PRO_ScrapReasonTable.SCRAPREASONDESC_FLD].ToString(), null, true);
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row,PRO_ScrapReasonTable.SCRAPREASONDESC_FLD] = drwResult[PRO_ScrapReasonTable.SCRAPREASONDESC_FLD];
						dgrdData[dgrdData.Row,PRO_ScrapReasonTable.SCRAPREASONID_FLD] = drwResult[PRO_ScrapReasonTable.SCRAPREASONID_FLD];
						
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
		/// dgrdData_AfterColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, July 4 2005</date>
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				DataRow drowResult = (DataRow) e.Column.DataColumn.Tag;
				//Fill Data to Work Order Master Column
				if (e.Column.DataColumn.DataField == PRO_WorkOrderMasterTable.WORKORDERNO_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, WOLINE] = string.Empty;
						dgrdData[row, ITM_ProductTable.CODE_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.REVISION_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.PRODUCTID_FLD] = string.Empty;
						dgrdData[row, ITM_PRODUCTCODE] = string.Empty;
						dgrdData[row, ITM_PRODUCTDESCRIPTION] = string.Empty;
						dgrdData[row, MODEL] = string.Empty;
						dgrdData[row, UM] = string.Empty;
						dgrdData[row, AVAILABLE_QUANTITY] = string.Empty;
						
					}
					else
					{
						dgrdData.EditActive = true;
						int pintRow = dgrdData.Row;
						if (dgrdData[dgrdData.Row, PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString() != string.Empty)
						{
							if (int.Parse(dgrdData[dgrdData.Row,PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString()) != int.Parse(drowResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString()))
							{
								//clear relate information
								dgrdData[pintRow, WOLINE] = string.Empty;
								dgrdData[pintRow, PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD] = string.Empty;
								dgrdData[pintRow, ITM_ProductTable.CODE_FLD] = string.Empty;
								dgrdData[pintRow, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
								dgrdData[pintRow, ITM_ProductTable.REVISION_FLD] = string.Empty;
								dgrdData[pintRow, ITM_ProductTable.PRODUCTID_FLD] = string.Empty;
								dgrdData[pintRow, ITM_PRODUCTCODE] = string.Empty;
								dgrdData[pintRow, ITM_PRODUCTDESCRIPTION] = string.Empty;
								dgrdData[pintRow, MODEL] = string.Empty;
								dgrdData[pintRow, UM] = string.Empty;
								dgrdData[pintRow, AVAILABLE_QUANTITY] = string.Empty;
								
							}
						}
						dgrdData[pintRow, PRO_ComponentScrapDetailTable.LINE_FLD] = pintRow + 1;
						dgrdData[pintRow,PRO_WorkOrderMasterTable.WORKORDERNO_FLD] = drowResult[PRO_WorkOrderMasterTable.WORKORDERNO_FLD];
						dgrdData[pintRow,PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD] = drowResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD];
					}
				}
				//Fill Data to Work Order Detail Column
				if (e.Column.DataColumn.DataField == WOLINE)
				{
					if ((e.Column.DataColumn.Tag == null) || (dgrdData[dgrdData.Row, WOLINE].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, ITM_ProductTable.CODE_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.REVISION_FLD] = string.Empty;
						dgrdData[row, ITM_ProductTable.PRODUCTID_FLD] = string.Empty;
						dgrdData[row, ITM_PRODUCTCODE] = string.Empty;
						dgrdData[row, ITM_PRODUCTDESCRIPTION] = string.Empty;
						dgrdData[row, MODEL] = string.Empty;
						dgrdData[row, UM] = string.Empty;
						dgrdData[row, AVAILABLE_QUANTITY] = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						//e.Column.DataColumn.Tag = drowResult.Row;
						if (dgrdData[dgrdData.Row, PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString() != string.Empty)
						{
							if (int.Parse(dgrdData[dgrdData.Row,PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString()) != int.Parse(drowResult[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString()))
							{
								//clear relate information
								int row = dgrdData.Row;
								dgrdData[row, ITM_ProductTable.CODE_FLD] = string.Empty;
								dgrdData[row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
								dgrdData[row, ITM_ProductTable.REVISION_FLD] = string.Empty;
								dgrdData[row, ITM_ProductTable.PRODUCTID_FLD] = string.Empty;
								dgrdData[row, ITM_PRODUCTCODE] = string.Empty;
								dgrdData[row, ITM_PRODUCTDESCRIPTION] = string.Empty;
								dgrdData[row, MODEL] = string.Empty;
								dgrdData[row, UM] = string.Empty;
								dgrdData[row, AVAILABLE_QUANTITY] = string.Empty;
							}
						}
						dgrdData[dgrdData.Row, WOLINE] = drowResult[PRO_WorkOrderDetailTable.LINE_FLD].ToString();
						dgrdData[dgrdData.Row,ITM_ProductTable.CODE_FLD] = drowResult[ITM_ProductTable.CODE_FLD];
						dgrdData[dgrdData.Row,ITM_ProductTable.DESCRIPTION_FLD] = drowResult[ITM_ProductTable.DESCRIPTION_FLD];
						dgrdData[dgrdData.Row,ITM_ProductTable.REVISION_FLD] = drowResult[ITM_ProductTable.REVISION_FLD];
						dgrdData[dgrdData.Row,PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD] = drowResult[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD];
						dgrdData[dgrdData.Row,PRO_WorkOrderDetailTable.PRODUCTID_FLD] = drowResult[PRO_WorkOrderDetailTable.PRODUCTID_FLD];
						dgrdData[dgrdData.Row, PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD] = drowResult[PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD];
					}
				}
				//Fill Data to From Location Column
				if ((e.Column.DataColumn.DataField == FROM_LOCATION))
				{
					if ((e.Column.DataColumn.Tag == null)|| (dgrdData[dgrdData.Row, FROM_LOCATION].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, AVAILABLE_QUANTITY] = string.Empty;
						dgrdData[row, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD] = string.Empty;
						dgrdData[row, PRO_ComponentScrapDetailTable.FROMBINID_FLD] = string.Empty;
						dgrdData[row, FROM_BIN] = string.Empty;
						
					}
					else
					{
						dgrdData.EditActive = true;
						if (dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].ToString() != string.Empty)
						{
							if (int.Parse(dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].ToString()) != int.Parse(drowResult[MST_LocationTable.LOCATIONID_FLD].ToString()))
							{
								//Clear Bin
								dgrdData[dgrdData.Row, FROM_BIN] = string.Empty;
								dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMBINID_FLD] = string.Empty;
								//clear available Quantity
								dgrdData[dgrdData.Row, AVAILABLE_QUANTITY] = string.Empty;
							}
						}
						dgrdData[dgrdData.Row, FROM_LOCATION] = drowResult[MST_LocationTable.CODE_FLD];
						dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD] = drowResult[MST_LocationTable.LOCATIONID_FLD];
					}
				}
				//Fill Data to From Bin Column
				if ((e.Column.DataColumn.DataField == FROM_BIN))
				{
					if ((e.Column.DataColumn.Tag == null)|| (dgrdData[dgrdData.Row, FROM_BIN].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, AVAILABLE_QUANTITY] = string.Empty;
						dgrdData[row, PRO_ComponentScrapDetailTable.FROMBINID_FLD] = string.Empty;
						
					}
					else
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row, FROM_BIN] = drowResult[MST_BINTable.CODE_FLD];
						dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.FROMBINID_FLD] = drowResult[MST_BINTable.BINID_FLD];
						//fill available quantity
						FillAvailableQuantity();
					}
				}
				//Fill Data to Component (Com. Number) Column
				if (e.Column.DataColumn.DataField == ITM_PRODUCTCODE)
				{
					if ((e.Column.DataColumn.Tag == null) || (dgrdData[dgrdData.Row, ITM_PRODUCTCODE].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, ITM_PRODUCTDESCRIPTION] = string.Empty;
						dgrdData[row, MODEL] = string.Empty;
						dgrdData[row, UM] = string.Empty;
						dgrdData[row, AVAILABLE_QUANTITY] = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row,ITM_PRODUCTCODE] = drowResult[ITM_PRODUCTCODE];
						dgrdData[dgrdData.Row,ITM_PRODUCTDESCRIPTION] = drowResult[ITM_PRODUCTDESCRIPTION];
						dgrdData[dgrdData.Row,UM] = drowResult[UM];
						dgrdData[dgrdData.Row,MODEL] = drowResult[MODEL];
						dgrdData[dgrdData.Row,PRO_ComponentScrapDetailTable.COMPONENTID_FLD] = drowResult[PRO_ComponentScrapDetailTable.COMPONENTID_FLD]; 
						dgrdData[dgrdData.Row, ITM_ProductTable.STOCKUMID_FLD] = drowResult[ITM_ProductTable.STOCKUMID_FLD];
						//Fill Available Quantity	
						FillAvailableQuantity();
					}
				}
				//Fill Data to Scrap Reason Column
				if ((e.Column.DataColumn.DataField == PRO_ScrapReasonTable.SCRAPREASONDESC_FLD))
				{
					if ((e.Column.DataColumn.Tag == null)|| (dgrdData[dgrdData.Row, PRO_ScrapReasonTable.SCRAPREASONDESC_FLD].ToString() == string.Empty))
					{

						return;
					}
					else
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row,PRO_ScrapReasonTable.SCRAPREASONDESC_FLD] = drowResult[PRO_ScrapReasonTable.SCRAPREASONDESC_FLD];
						dgrdData[dgrdData.Row,PRO_ScrapReasonTable.SCRAPREASONID_FLD] = drowResult[PRO_ScrapReasonTable.SCRAPREASONID_FLD];
					}
				}
				//Fill Data to To Location Column
				if ((e.Column.DataColumn.DataField == TO_LOCATION))
				{
					if ((e.Column.DataColumn.Tag == null)|| (dgrdData[dgrdData.Row, TO_LOCATION].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD] = string.Empty;
						dgrdData[row, PRO_ComponentScrapDetailTable.TOBINID_FLD] = string.Empty;
						dgrdData[row, AVAILABLE_QUANTITY] = string.Empty;
						dgrdData[row, TO_BIN] = string.Empty;
						
					}
					else
					{
						dgrdData.EditActive = true;
						if (dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD].ToString() != string.Empty)
						{
							if (int.Parse(dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD].ToString()) != int.Parse(drowResult[MST_LocationTable.LOCATIONID_FLD].ToString()))
							{
								//Clear Bin
								dgrdData[dgrdData.Row, TO_BIN] = string.Empty;
								dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.TOBINID_FLD] = string.Empty;
							}
						}
						dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD] = drowResult[MST_LocationTable.LOCATIONID_FLD];
						dgrdData[dgrdData.Row, TO_LOCATION] = drowResult[MST_LocationTable.CODE_FLD];	
					}
				}
				//Fill Data to To Bin Column
				if ((e.Column.DataColumn.DataField == TO_BIN))
				{
					if ((e.Column.DataColumn.Tag == null)|| (dgrdData[dgrdData.Row, TO_BIN].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, PRO_ComponentScrapDetailTable.TOBINID_FLD] = string.Empty;
						dgrdData[row, AVAILABLE_QUANTITY] = string.Empty;
						
					}
					else
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row, PRO_ComponentScrapDetailTable.TOBINID_FLD] = drowResult[MST_BINTable.BINID_FLD];
						dgrdData[dgrdData.Row, TO_BIN] = drowResult[MST_BINTable.CODE_FLD];	
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
		/// dgrdData_OnAddNew
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void dgrdData_OnAddNew(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_OnAddNew()";
			try
			{/*
				
				// if detail has no row
				if(dgrdData.Row > 0)
				{
					intMaxLine = int.Parse(dgrdData[dgrdData.Row-1, ORDER].ToString());
				}
				dgrdData.Columns[ORDER].Value = ++intMaxLine;
				*/
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
		/// txtScrapNo_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada, August 11 2005</author>
		private void txtScrapNo_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtScrapNo_Leave()";
			try 
			{
				//OnLeaveControl(sender, e);
				if (btnFindScrapNo.Enabled)
				{
					if (txtScrapNo.Text.Trim() == string.Empty)
					{
						dtmPostDate.Value = null;
						CreateDataSet();
						dgrdData.DataSource = dstGridData.Tables[0];
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
						return;
					}
					Hashtable htbCriteria = new Hashtable();
					DataRowView drwResult = null;
					//User has enter MasLoc
					if (txtMasLoc.Text.ToString() != string.Empty)
					{
						htbCriteria.Add(PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD, voMasLoc.MasterLocationID.ToString());	
					}
					else //User has not entered MasLoc
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
						txtScrapNo.Text = string.Empty;
						txtMasLoc.Focus();
						return;
					}
					
					drwResult = FormControlComponents.OpenSearchForm(PRO_ComponentScrapMasterTable.TABLE_NAME, PRO_ComponentScrapMasterTable.SCRAPNO_FLD, txtScrapNo.Text.Trim(), htbCriteria, false);
					if (drwResult != null)
					{
						txtScrapNo.Text = drwResult[PRO_ComponentScrapMasterTable.SCRAPNO_FLD].ToString();
						//Keep valua of ComponentScrapMasterID 
						txtScrapNo.Tag = drwResult[PRO_ComponentScrapMasterTable.COMPONENTSCRAPMASTERID_FLD];
						pintComponentScrapMasterID = int.Parse(txtScrapNo.Tag.ToString());
						dtmPostDate.Value = (DateTime) drwResult[PRO_ComponentScrapMasterTable.POSTDATE_FLD];
					}
					else
					{
						txtScrapNo.Focus();
						return;
					}
					FillDataGrid(int.Parse(txtScrapNo.Tag.ToString()));
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					btnEdit.Enabled = true;			
					btnDelete.Enabled = true;
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
		/// dgrdData_BeforeColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				string strCondition = string.Empty;
				Hashtable htbCriteria = new Hashtable();
				string strWhereClause = string.Empty;
				DataRowView drwResult = null;
				switch (e.Column.DataColumn.DataField)
				{
					case PRO_WorkOrderMasterTable.WORKORDERNO_FLD:
						# region Open Work Order Master search form 
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							
							//User has entered Masloc
							if (txtMasLoc.Text.ToString() != string.Empty)
							{
								htbCriteria.Add(PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD, voMasLoc.MasterLocationID.ToString());
							}
							else //User has not entered MasLoc
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
								//txtMasLoc.Focus();
								e.Cancel = true;
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(V_RELEASEDWORKORDER, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
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
					case WOLINE:
						#region Open Work Order Line search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							//User has entered Work Order Master
							if (dgrdData[dgrdData.Row, PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString() != string.Empty)
							{
//								htbCriteria.Add(PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD, dgrdData.Columns[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].Value);
//								htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int)WOLineStatus.Released);
								strWhereClause += PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + " = " + dgrdData.Columns[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].Value.ToString();
								strWhereClause += " AND (" + PRO_WorkOrderDetailTable.STATUS_FLD + " = " + (int)WOLineStatus.Released;
								strWhereClause += " OR " + PRO_WorkOrderDetailTable.STATUS_FLD + " = " + (int)WOLineStatus.MfgClose + ")";
							}
							else //User has not entered Work Order Master
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INPUT_WO, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
							//drwResult = FormControlComponents.OpenSearchForm(V_WODETAILANDPRODUCTINFO, PRO_WorkOrderDetailTable.LINE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
							drwResult = FormControlComponents.OpenSearchForm(V_WODETAILANDPRODUCTINFO, PRO_WorkOrderDetailTable.LINE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), strWhereClause);
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
					case FROM_LOCATION:
						#region Open Operation search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							//User has entered Work Order Detail
							if (dgrdData[dgrdData.Row, WOLINE].ToString() != string.Empty)
							{
								htbCriteria.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, dgrdData.Columns[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].Value);
							}
							else //User has not entered Work Order Detail 
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INPUT_WO_LINE, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(V_LOCATIONANDPRODUCTIONLINE, MST_LocationTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
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
					case FROM_BIN:
						#region Open Operation search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							//User has entered From Location
							if (dgrdData[dgrdData.Row, FROM_LOCATION].ToString() != string.Empty)
							{
								htbCriteria.Add(MST_LocationTable.LOCATIONID_FLD, dgrdData.Columns[PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].Value);
							}
							else //User has not entered Work Order Detail 
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(V_BINEXCEPTDESTROY, MST_BINTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
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
					case ITM_PRODUCTCODE:
						#region Open Component search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							//User has entered Work Order Detail
							if (dgrdData[dgrdData.Row, WOLINE].ToString() != string.Empty)
							{
								htbCriteria.Add(PRO_WorkOrderBomMasterTable.WORKORDERDETAILID_FLD, dgrdData.Columns[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].Value);
							}
							else //User has not entered Work Order Detail 
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INPUT_WO_LINE, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(V_COMPONENTSCRAP, ITM_PRODUCTCODE, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
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
					case TO_LOCATION:
						#region Open To Location
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							//User has entered Work Order Detail
							if (dgrdData[dgrdData.Row, WOLINE].ToString() != string.Empty)
							{
								htbCriteria.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, dgrdData.Columns[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].Value);
							}
							else //User has not entered Work Order Detail 
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INPUT_WO_LINE, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
							drwResult = FormControlComponents.OpenSearchForm(V_LOCATIONANDPRODUCTIONLINE, MST_LocationTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
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
					case TO_BIN:
						#region Open To Bin
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							//User has entered Work Order Detail
							if (dgrdData[dgrdData.Row, TO_LOCATION].ToString() != string.Empty)
							{
								htbCriteria.Add(MST_BINTable.LOCATIONID_FLD, dgrdData.Columns[PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD].Value);
							}
							else //User has not entered Work Order Detail 
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INPUT_WO_LINE, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
							//Bin DS
							htbCriteria.Add(MST_BINTable.BINTYPEID_FLD, (int)BinTypeEnum.LS);
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
					case PRO_ScrapReasonTable.SCRAPREASONDESC_FLD:
						#region Open Scrap Reason search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							drwResult = FormControlComponents.OpenSearchForm(PRO_ScrapReasonTable.TABLE_NAME, PRO_ScrapReasonTable.SCRAPREASONDESC_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), null, false);
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
					case PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD:
						if (e.Column.DataColumn.Text == string.Empty)
						{
							return;
						}
						try
						{
							decimal.Parse(e.Column.DataColumn.Text);
						}
						catch
						{
							e.Cancel = true;
							return;
						}
						if(decimal.Parse(e.Column.DataColumn.Text) < 0) 
						{
							e.Cancel = true;
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

		/// <summary>
		/// ComponentScrap_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void ComponentScrap_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ComponentScrap_KeyDown()";
			try
			{
				#region // HACK: DEL SonHT 2005-10-20
				//			if (e.KeyCode == Keys.Escape)
				//			{
				//				this.Close();
				//			}
				#endregion // END: DEL SonHT 2005-10-20
				if (e.KeyCode == Keys.F12)
				{
					if((formMode == EnumAction.Edit) || (formMode == EnumAction.Add)) 
					{
						dgrdData.Row = dgrdData.RowCount;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WorkOrderMasterTable.WORKORDERNO_FLD]);
						dgrdData.Focus();
					}
					dgrdData.EditActive = false;
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
		/// ComponentScrap_Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void ComponentScrap_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ComponentScrap_Closing()";
			try
			{
				if (btnSave.Enabled)
				{
					if ((formMode != EnumAction.Add)||(formMode != EnumAction.Edit))
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
		/// txtScrapNo_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void txtScrapNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtScrapNo_KeyDown()";
			try
			{
				if (btnFindScrapNo.Enabled)
				{
					if ((e.KeyCode == Keys.F4) && (btnFindScrapNo.Enabled))
					{
						btnFindScrapNo_Click(sender, e);
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
		/// OnEnterControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void OnEnterControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnEnterControl()";
			try
			{
				FormControlComponents.OnEnterControl(sender, e);
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
		//**************************************************************************              
		///    <Description>
		///       OnLeaveControl
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       Dungla
		///    </Authors>
		///    <History>
		///       Tuesday, March 29, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void OnLeaveControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnLeaveControl()";
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
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
		/// dgrdData_AfterDelete
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, September 28 2005</date>
		private void dgrdData_AfterDelete(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterDelete()";
			try
			{
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString() != string.Empty)
					{
						dgrdData[i, PRO_WorkOrderDetailTable.LINE_FLD] = i+1;
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
		/// dtmPostDate_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 3 2006</date>
		private void dtmPostDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmPostDate_Validating()";
			try
			{
				if (dtmPostDate.Value == DBNull.Value)
				{
					//clear available quantity
					for (int i = 0; i < dgrdData.RowCount; i++)
					{
						dgrdData[i, AVAILABLE_QUANTITY]  = string.Empty;
					}
				}
				if (txtMasLoc.Text.Trim() != string.Empty && dtmPostDate.Value != DBNull.Value)
				{
					//Re-calculate Available Qty
					for (int i = 0; i < dgrdData.RowCount; i++)
					{
						if (dgrdData[i, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].ToString() != string.Empty
							&& dgrdData[i, PRO_ComponentScrapDetailTable.FROMBINID_FLD].ToString() != string.Empty
							&& dgrdData[i, PRO_ComponentScrapDetailTable.COMPONENTID_FLD].ToString() != string.Empty)
                        {
//							dgrdData[i, AVAILABLE_QUANTITY]	= (new InventoryUtilsBO()).GetAvailableQtyByPostDate((DateTime)dtmPostDate.Value,
//								int.Parse(cboCCN.SelectedValue.ToString()), int.Parse(txtMasLoc.Tag.ToString()), int.Parse(dgrdData[i, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].ToString()),
//								int.Parse(dgrdData[i, PRO_ComponentScrapDetailTable.FROMBINID_FLD].ToString()), int.Parse(dgrdData[i, PRO_ComponentScrapDetailTable.COMPONENTID_FLD].ToString()));
							dgrdData[i, AVAILABLE_QUANTITY]	= (new InventoryUtilsBO()).GetAvailableQtyByPostDate(dtmCurrentDate,
								int.Parse(cboCCN.SelectedValue.ToString()), int.Parse(txtMasLoc.Tag.ToString()), int.Parse(dgrdData[i, PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].ToString()),
								int.Parse(dgrdData[i, PRO_ComponentScrapDetailTable.FROMBINID_FLD].ToString()), int.Parse(dgrdData[i, PRO_ComponentScrapDetailTable.COMPONENTID_FLD].ToString()));
						}
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
		/// btnProductionLine_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, August 9 2006</date>
		private void btnProductionLine_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
			try
			{
				Hashtable htbCriteria = new Hashtable();				
				htbCriteria.Add(MST_CCNTable.CCNID_FLD, (int) cboCCN.SelectedValue);

				//Call OpenSearchForm for selecting Production Line
				DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, true);
				
				//If has Production Line matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					if (txtProductionLine.Tag != null && int.Parse(txtProductionLine.Tag.ToString()) != int.Parse(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString()))
					{
						//Clear related information
						CreateDataSet();
						dgrdData.DataSource = dstGridData.Tables[0];
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
						ConfigGrid(false);
					}
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
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
		/// txtProductionLine_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, August 9 2006</date>
		private void txtProductionLine_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";
			try
			{
				if (!txtProductionLine.Modified) return;
				if (txtProductionLine.Text == string.Empty)
				{
					CreateDataSet();
					dgrdData.DataSource = dstGridData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					ConfigGrid(false);
					return;
				}
				Hashtable htbCriteria = new Hashtable();				
				htbCriteria.Add(MST_CCNTable.CCNID_FLD, (int) cboCCN.SelectedValue);

				//Call OpenSearchForm for selecting Production Line
				DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, false);
				
				//If has Production Line matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					if (txtProductionLine.Tag != null && int.Parse(txtProductionLine.Tag.ToString()) != int.Parse(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString()))
					{
						//Clear related information
						CreateDataSet();
						dgrdData.DataSource = dstGridData.Tables[0];
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
						ConfigGrid(false);
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
		/// txtProductionLine_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, August 9 2006</date>
		private void txtProductionLine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (btnProductionLine.Enabled && e.KeyCode == Keys.F4)
			{
				btnProductionLine_Click(sender, new EventArgs());
			}
		}
	}
}
