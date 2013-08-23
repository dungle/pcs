using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PCSComUtils.PCSExc;
using PCSUtils.Utils;
using PCSUtils.Log;
using PCSUtils.MasterSetup;
using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.Common.BO;
using PCSComProduction.WorkOrder.BO;
using PCSComProduction.WorkOrder.DS;
using C1.Win.C1TrueDBGrid;


namespace PCSProduction.WorkOrder
{
	/// <summary>
	/// Summary description for ReleaseWorkOrder.
	/// </summary>
	public class ReleaseWorkOrder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Label lblMasLoc;
		private System.Windows.Forms.CheckBox chkSelectAll;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnRelease;
		private System.Windows.Forms.Button btnMaintainWO;
		private System.Windows.Forms.TextBox txtMasLoc;
		private System.Windows.Forms.Button btnMasLoc;
		private System.Windows.Forms.Label lblWO;
		private System.Windows.Forms.Label lblFromStartDate;
		private System.Windows.Forms.Label lblToStartDate;
		private C1.Win.C1List.C1Combo cboCCN;
		private C1.Win.C1Input.C1DateEdit dtmToStartDate;
		private C1.Win.C1Input.C1DateEdit dtmFromStartDate;
		private System.Windows.Forms.Button btnWorkOrder;
		private System.Windows.Forms.TextBox txtWorkOrder;

		#region My variable
		const string THIS = "PCSProduction.WorkOrder.ReleaseWorkOrder";
		#endregion My variable


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private DataTable dtbGridStore;
		private MST_MasterLocationVO voMasLoc = new MST_MasterLocationVO();
		const string GRID_COL_NAME ="NAME";
		const string GRID_COL_CAPTION = "CAPTION";
		const string GRID_COL_WIDTH = "WIDTH";
		const string SELECT = "Selected";
		const string RELEASE = "release";
		const string SEL = "Select";
		const string WONO = "WorkOrderNo";
		const string PARTNUMBER = "PartNumber";
		const string PARTNAME = "PartName";
		const string MODEL = "Model";
		const string UM = "UM";
		const string QUANTITY = "OrderQuantity";
		const string ESTCOST = "EstCst";
		const string V_WOUNRELEASE = "V_WORelease";
		const string V_WOUNRELEASED = "v_ReleasedWO";
		const string RELEASEWO_TABLE = "ReleaseWOTable";

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid gridData;
		private DataTable dtbGridLayOut;
		const string GRID_COL_VISIBLE = "VISIBLE";
		const string V_SELECT_WO_PROLINE_NAME = "V_SelectWOBaseProductionLine";
		private bool blnStateOfCheck = false;
		private bool blnSearchResult = false;
		private const string TRUE = "True";
		DataSet dstUnreleaseWO = new DataSet();
		private System.Windows.Forms.TextBox txtProductionLine;
		private System.Windows.Forms.Button btnProductionLine;
		private System.Windows.Forms.Label lblProductionLine;
		private System.Windows.Forms.ComboBox cboRelease;
		private System.Windows.Forms.Label lblRelease;
		private System.Windows.Forms.Label lblReleased;
		private System.Windows.Forms.Label lblNotReleased;
		DataSet dstGridData;
		public ReleaseWorkOrder()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ReleaseWorkOrder));
			this.gridData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnWorkOrder = new System.Windows.Forms.Button();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.txtWorkOrder = new System.Windows.Forms.TextBox();
			this.lblWO = new System.Windows.Forms.Label();
			this.lblCCN = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnRelease = new System.Windows.Forms.Button();
			this.btnSearch = new System.Windows.Forms.Button();
			this.lblFromStartDate = new System.Windows.Forms.Label();
			this.lblMasLoc = new System.Windows.Forms.Label();
			this.btnMaintainWO = new System.Windows.Forms.Button();
			this.lblToStartDate = new System.Windows.Forms.Label();
			this.txtMasLoc = new System.Windows.Forms.TextBox();
			this.btnMasLoc = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.dtmToStartDate = new C1.Win.C1Input.C1DateEdit();
			this.dtmFromStartDate = new C1.Win.C1Input.C1DateEdit();
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.btnProductionLine = new System.Windows.Forms.Button();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.cboRelease = new System.Windows.Forms.ComboBox();
			this.lblRelease = new System.Windows.Forms.Label();
			this.lblReleased = new System.Windows.Forms.Label();
			this.lblNotReleased = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToStartDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromStartDate)).BeginInit();
			this.SuspendLayout();
			// 
			// gridData
			// 
			this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridData.CaptionHeight = 17;
			this.gridData.CollapseColor = System.Drawing.Color.Black;
			this.gridData.ExpandColor = System.Drawing.Color.Black;
			this.gridData.FilterBar = true;
			this.gridData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.gridData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridData.GroupByCaption = "Drag a column header here to group by that column";
			this.gridData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.gridData.Location = new System.Drawing.Point(4, 98);
			this.gridData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.gridData.Name = "gridData";
			this.gridData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.gridData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.gridData.PreviewInfo.ZoomFactor = 75;
			this.gridData.PrintInfo.ShowOptionsDialog = false;
			this.gridData.RecordSelectorWidth = 17;
			this.gridData.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.gridData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.gridData.RowHeight = 15;
			this.gridData.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.gridData.Size = new System.Drawing.Size(624, 304);
			this.gridData.TabIndex = 16;
			this.gridData.Text = "c1TrueDBGrid1";
			this.gridData.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridData_AfterColEdit);
			this.gridData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridData_AfterColUpdate);
			this.gridData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Work Order " +
				"No.\" DataField=\"WorkOrderNo\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCo" +
				"lumn Level=\"0\" Caption=\"Line\" DataField=\"Line\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"Part Number\" DataField=\"PartNumber\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Model\" " +
				"DataField=\"Model\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"Select\" DataField=\"Selected\" NumberFormat=\"Yes/No\"><ValueItems Pres" +
				"entation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Captio" +
				"n=\"Part Name\" DataField=\"PartName\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"UM\" DataField=\"UM\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Quantity\" DataField=\"OrderQuantity\">" +
				"<ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Estim" +
				"ated Cost\" DataField=\"EstCst\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
				"olumn Level=\"0\" Caption=\"Production Line\" DataField=\"ProductionLine\"><ValueItems" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Start Date, Time" +
				"\" DataField=\"StartDate\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"Due Date, Time\" DataField=\"DueDate\"><ValueItems /><GroupInfo " +
				"/></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrap" +
				"per\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Inactive{Fo" +
				"reColor:InactiveCaptionText;BackColor:InactiveCaption;}Style78{}Style79{}Style85" +
				"{}Editor{}Style72{}Style73{}Style70{AlignHorz:Near;}Style71{AlignHorz:Near;}Styl" +
				"e76{AlignHorz:Near;}Style77{AlignHorz:Near;}Style74{}Style75{}Style84{}Style87{}" +
				"Style86{}Style81{}Style80{}Style83{AlignHorz:Near;}Style82{AlignHorz:Near;}Filte" +
				"rBar{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:Con" +
				"trolText;BackColor:Control;}Style18{}Style19{}Style14{}Style15{}Style16{AlignHor" +
				"z:Center;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{}Styl" +
				"e13{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style29{AlignHorz:Nea" +
				"r;}Style28{AlignHorz:Center;}Style27{}Style25{}Style22{AlignHorz:Center;}Style9{" +
				"}Style8{}Style24{}Style26{}Style5{}Style4{}Style7{}Style6{}Style1{}Style23{Align" +
				"Horz:Near;}Style3{}Style2{}Style21{}Style20{}OddRow{}Style38{}Style39{}Style36{}" +
				"Style37{}Style34{AlignHorz:Center;}Style35{AlignHorz:Near;}Style32{}Style33{}Sty" +
				"le30{}Style49{}Style48{}Style31{}Normal{Font:Microsoft Sans Serif, 8.25pt;}Style" +
				"41{AlignHorz:Near;}Style40{AlignHorz:Center;}Style43{}Style42{}Style45{}Style44{" +
				"}Style47{AlignHorz:Near;}Style46{AlignHorz:Center;}EvenRow{BackColor:Aqua;}Style" +
				"59{AlignHorz:Near;}Style58{AlignHorz:Center;}RecordSelector{AlignImage:Center;}S" +
				"tyle51{}Style50{}Footer{}Style52{AlignHorz:Center;}Style53{AlignHorz:Near;}Style" +
				"54{}Style55{}Style56{}Style57{}Caption{AlignHorz:Center;}Style69{}Style68{}Style" +
				"63{}Style62{}Style61{}Style60{}Style67{}Style66{}Style65{AlignHorz:Near;}Style64" +
				"{AlignHorz:Center;}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert" +
				":Center;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionH" +
				"eight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" FilterBar=\"True\" Mar" +
				"queeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"17\" DefRecSelWidth=\"17\" Vertic" +
				"alScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 620, 300</ClientRe" +
				"ct><BorderSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><Edito" +
				"rStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" " +
				"/><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\"" +
				" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"H" +
				"eading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><In" +
				"activeStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Sty" +
				"le9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyl" +
				"e parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internal" +
				"Cols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent" +
				"=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><Colum" +
				"nDivider>DarkGray,Single</ColumnDivider><Width>57</Width><Height>15</Height><DCI" +
				"dx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Vis" +
				"ible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Hei" +
				"ght><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
				"le2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"S" +
				"tyle3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderSt" +
				"yle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32" +
				"\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>" +
				"117</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn" +
				"><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35" +
				"\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=" +
				"\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle p" +
				"arent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Sin" +
				"gle</ColumnDivider><Width>42</Width><Height>15</Height><DCIdx>1</DCIdx></C1Displ" +
				"ayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style pa" +
				"rent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><Editor" +
				"Style parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style" +
				"45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><C" +
				"olumnDivider>DarkGray,Single</ColumnDivider><Width>145</Width><Height>15</Height" +
				"><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Styl" +
				"e3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle" +
				" parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /" +
				"><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>172" +
				"</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /" +
				"><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"St" +
				"yle49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle pare" +
				"nt=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single" +
				"</ColumnDivider><Width>71</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayC" +
				"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style paren" +
				"t=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorSty" +
				"le parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\"" +
				" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><Colu" +
				"mnDivider>DarkGray,Single</ColumnDivider><Width>47</Width><Height>15</Height><DC" +
				"Idx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
				"=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" " +
				"me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle par" +
				"ent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Vi" +
				"sible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>64</Wid" +
				"th><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
				"gStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" /><Foo" +
				"terStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"Style67" +
				"\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupFooterStyle parent=\"S" +
				"tyle1\" me=\"Style68\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</" +
				"Height><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e80\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Hei" +
				"ght>15</Height><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle" +
				" parent=\"Style2\" me=\"Style82\" /><Style parent=\"Style1\" me=\"Style83\" /><FooterSty" +
				"le parent=\"Style3\" me=\"Style84\" /><EditorStyle parent=\"Style5\" me=\"Style85\" /><G" +
				"roupHeaderStyle parent=\"Style1\" me=\"Style87\" /><GroupFooterStyle parent=\"Style1\"" +
				" me=\"Style86\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDiv" +
				"ider><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn></internalCols></C1.W" +
				"in.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><" +
				"Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Styl" +
				"e parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style" +
				" parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style par" +
				"ent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style pa" +
				"rent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style" +
				" parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedSt" +
				"yles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layou" +
				"t><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 620, 300</ClientA" +
				"rea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=" +
				"\"\" me=\"Style15\" /></Blob>";
			// 
			// btnWorkOrder
			// 
			this.btnWorkOrder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnWorkOrder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnWorkOrder.Location = new System.Drawing.Point(421, 50);
			this.btnWorkOrder.Name = "btnWorkOrder";
			this.btnWorkOrder.Size = new System.Drawing.Size(22, 20);
			this.btnWorkOrder.TabIndex = 10;
			this.btnWorkOrder.Text = "...";
			this.btnWorkOrder.Click += new System.EventHandler(this.btnWorkOrder_Click);
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkSelectAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkSelectAll.Location = new System.Drawing.Point(72, 406);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(78, 23);
			this.chkSelectAll.TabIndex = 18;
			this.chkSelectAll.Text = "Select &All";
			this.chkSelectAll.Enter += new System.EventHandler(this.chkSelectAll_Enter);
			this.chkSelectAll.Leave += new System.EventHandler(this.chkSelectAll_Leave);
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// txtWorkOrder
			// 
			this.txtWorkOrder.Location = new System.Drawing.Point(308, 50);
			this.txtWorkOrder.Name = "txtWorkOrder";
			this.txtWorkOrder.Size = new System.Drawing.Size(112, 20);
			this.txtWorkOrder.TabIndex = 9;
			this.txtWorkOrder.Text = "";
			this.txtWorkOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWorkOrder_KeyDown);
			this.txtWorkOrder.Validating += new System.ComponentModel.CancelEventHandler(this.txtWorkOrder_Validating);
			this.txtWorkOrder.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblWO
			// 
			this.lblWO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblWO.Location = new System.Drawing.Point(230, 50);
			this.lblWO.Name = "lblWO";
			this.lblWO.Size = new System.Drawing.Size(74, 20);
			this.lblWO.TabIndex = 8;
			this.lblWO.Text = "Work Order";
			this.lblWO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCCN
			// 
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(518, 6);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(30, 20);
			this.lblCCN.TabIndex = 1;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(562, 406);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(66, 23);
			this.btnClose.TabIndex = 21;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(495, 406);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(66, 23);
			this.btnHelp.TabIndex = 20;
			this.btnHelp.Text = "&Help";
			// 
			// btnRelease
			// 
			this.btnRelease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRelease.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRelease.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnRelease.Location = new System.Drawing.Point(4, 406);
			this.btnRelease.Name = "btnRelease";
			this.btnRelease.Size = new System.Drawing.Size(66, 23);
			this.btnRelease.TabIndex = 17;
			this.btnRelease.Text = "&Release";
			this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearch.Location = new System.Drawing.Point(562, 72);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(66, 23);
			this.btnSearch.TabIndex = 15;
			this.btnSearch.Text = "&Search";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// lblFromStartDate
			// 
			this.lblFromStartDate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFromStartDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblFromStartDate.Location = new System.Drawing.Point(4, 74);
			this.lblFromStartDate.Name = "lblFromStartDate";
			this.lblFromStartDate.Size = new System.Drawing.Size(86, 20);
			this.lblFromStartDate.TabIndex = 11;
			this.lblFromStartDate.Text = "From Start Date";
			this.lblFromStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMasLoc
			// 
			this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
			this.lblMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMasLoc.Location = new System.Drawing.Point(4, 30);
			this.lblMasLoc.Name = "lblMasLoc";
			this.lblMasLoc.Size = new System.Drawing.Size(86, 20);
			this.lblMasLoc.TabIndex = 2;
			this.lblMasLoc.Text = "Mas. Location";
			this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnMaintainWO
			// 
			this.btnMaintainWO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMaintainWO.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnMaintainWO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnMaintainWO.Location = new System.Drawing.Point(374, 406);
			this.btnMaintainWO.Name = "btnMaintainWO";
			this.btnMaintainWO.Size = new System.Drawing.Size(120, 23);
			this.btnMaintainWO.TabIndex = 19;
			this.btnMaintainWO.Text = "Maintain &Work Order";
			this.btnMaintainWO.Click += new System.EventHandler(this.btnMaintainWO_Click);
			// 
			// lblToStartDate
			// 
			this.lblToStartDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblToStartDate.Location = new System.Drawing.Point(230, 72);
			this.lblToStartDate.Name = "lblToStartDate";
			this.lblToStartDate.Size = new System.Drawing.Size(74, 20);
			this.lblToStartDate.TabIndex = 13;
			this.lblToStartDate.Text = "To Start Date";
			this.lblToStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtMasLoc
			// 
			this.txtMasLoc.Location = new System.Drawing.Point(92, 30);
			this.txtMasLoc.Name = "txtMasLoc";
			this.txtMasLoc.Size = new System.Drawing.Size(112, 20);
			this.txtMasLoc.TabIndex = 3;
			this.txtMasLoc.Text = "";
			this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
			this.txtMasLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasLoc_Validating);
			this.txtMasLoc.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// btnMasLoc
			// 
			this.btnMasLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnMasLoc.Location = new System.Drawing.Point(205, 30);
			this.btnMasLoc.Name = "btnMasLoc";
			this.btnMasLoc.Size = new System.Drawing.Size(22, 20);
			this.btnMasLoc.TabIndex = 4;
			this.btnMasLoc.Text = "...";
			this.btnMasLoc.Click += new System.EventHandler(this.btnMasLoc_Click);
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
			this.cboCCN.TabIndex = 0;
			this.cboCCN.Text = "CCN";
			this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboCCN.Leave += new System.EventHandler(this.OnLeaveControl);
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
			// dtmToStartDate
			// 
			// 
			// dtmToStartDate.Calendar
			// 
			this.dtmToStartDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmToStartDate.CustomFormat = "dd-MM-yyyy";
			this.dtmToStartDate.EmptyAsNull = true;
			this.dtmToStartDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmToStartDate.Location = new System.Drawing.Point(308, 72);
			this.dtmToStartDate.Name = "dtmToStartDate";
			this.dtmToStartDate.Size = new System.Drawing.Size(134, 20);
			this.dtmToStartDate.TabIndex = 14;
			this.dtmToStartDate.Tag = null;
			this.dtmToStartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmToStartDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmToStartDate.Enter += new System.EventHandler(this.OnEnterControl);
			this.dtmToStartDate.Leave += new System.EventHandler(this.dtmToStartDate_Leave);
			// 
			// dtmFromStartDate
			// 
			// 
			// dtmFromStartDate.Calendar
			// 
			this.dtmFromStartDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmFromStartDate.CustomFormat = "dd-MM-yyyy";
			this.dtmFromStartDate.EmptyAsNull = true;
			this.dtmFromStartDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmFromStartDate.Location = new System.Drawing.Point(92, 72);
			this.dtmFromStartDate.Name = "dtmFromStartDate";
			this.dtmFromStartDate.Size = new System.Drawing.Size(134, 20);
			this.dtmFromStartDate.TabIndex = 12;
			this.dtmFromStartDate.Tag = null;
			this.dtmFromStartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmFromStartDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmFromStartDate.Enter += new System.EventHandler(this.OnEnterControl);
			this.dtmFromStartDate.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// txtProductionLine
			// 
			this.txtProductionLine.Location = new System.Drawing.Point(92, 50);
			this.txtProductionLine.Name = "txtProductionLine";
			this.txtProductionLine.Size = new System.Drawing.Size(112, 20);
			this.txtProductionLine.TabIndex = 6;
			this.txtProductionLine.Text = "";
			this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
			// 
			// btnProductionLine
			// 
			this.btnProductionLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnProductionLine.Location = new System.Drawing.Point(205, 50);
			this.btnProductionLine.Name = "btnProductionLine";
			this.btnProductionLine.Size = new System.Drawing.Size(22, 20);
			this.btnProductionLine.TabIndex = 7;
			this.btnProductionLine.Text = "...";
			this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
			// 
			// lblProductionLine
			// 
			this.lblProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblProductionLine.Location = new System.Drawing.Point(4, 50);
			this.lblProductionLine.Name = "lblProductionLine";
			this.lblProductionLine.Size = new System.Drawing.Size(86, 20);
			this.lblProductionLine.TabIndex = 5;
			this.lblProductionLine.Text = "Production Line";
			this.lblProductionLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboRelease
			// 
			this.cboRelease.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboRelease.Location = new System.Drawing.Point(92, 7);
			this.cboRelease.Name = "cboRelease";
			this.cboRelease.Size = new System.Drawing.Size(112, 21);
			this.cboRelease.TabIndex = 22;
			this.cboRelease.SelectedIndexChanged += new System.EventHandler(this.cboRelease_SelectedIndexChanged);
			// 
			// lblRelease
			// 
			this.lblRelease.ForeColor = System.Drawing.Color.Maroon;
			this.lblRelease.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblRelease.Location = new System.Drawing.Point(4, 8);
			this.lblRelease.Name = "lblRelease";
			this.lblRelease.Size = new System.Drawing.Size(86, 20);
			this.lblRelease.TabIndex = 23;
			this.lblRelease.Text = "Status";
			this.lblRelease.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblReleased
			// 
			this.lblReleased.ForeColor = System.Drawing.Color.Maroon;
			this.lblReleased.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblReleased.Location = new System.Drawing.Point(260, 12);
			this.lblReleased.Name = "lblReleased";
			this.lblReleased.Size = new System.Drawing.Size(86, 20);
			this.lblReleased.TabIndex = 24;
			this.lblReleased.Text = "Released";
			this.lblReleased.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblReleased.Visible = false;
			// 
			// lblNotReleased
			// 
			this.lblNotReleased.ForeColor = System.Drawing.Color.Maroon;
			this.lblNotReleased.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblNotReleased.Location = new System.Drawing.Point(368, 16);
			this.lblNotReleased.Name = "lblNotReleased";
			this.lblNotReleased.Size = new System.Drawing.Size(86, 20);
			this.lblNotReleased.TabIndex = 25;
			this.lblNotReleased.Text = "Not Released";
			this.lblNotReleased.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblNotReleased.Visible = false;
			// 
			// ReleaseWorkOrder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 431);
			this.Controls.Add(this.lblNotReleased);
			this.Controls.Add(this.lblReleased);
			this.Controls.Add(this.lblRelease);
			this.Controls.Add(this.cboRelease);
			this.Controls.Add(this.txtProductionLine);
			this.Controls.Add(this.btnProductionLine);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.dtmToStartDate);
			this.Controls.Add(this.dtmFromStartDate);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.txtMasLoc);
			this.Controls.Add(this.gridData);
			this.Controls.Add(this.txtWorkOrder);
			this.Controls.Add(this.btnMasLoc);
			this.Controls.Add(this.lblToStartDate);
			this.Controls.Add(this.btnMaintainWO);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnRelease);
			this.Controls.Add(this.chkSelectAll);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnWorkOrder);
			this.Controls.Add(this.lblMasLoc);
			this.Controls.Add(this.lblFromStartDate);
			this.Controls.Add(this.lblWO);
			this.KeyPreview = true;
			this.Name = "ReleaseWorkOrder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Release Work Order";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReleaseWorkOrder_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ReleaseWorkOrder_Closing);
			this.Load += new System.EventHandler(this.ReleaseWorkOrder_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToStartDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromStartDate)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		private void ReleaseWorkOrder_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ReleaseWorkOrder_Load()";
			try
			{
				//Set authorization for user
				
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW);
					return;
				}
				//Set format for DateTime Controls
				dtmFromStartDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				dtmToStartDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				// StoreGridLayout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(gridData);
				//InitVariable
				InitVariable();
				//Init Combo Box
				cboRelease.Items.Clear();
				cboRelease.Items.Add(lblNotReleased.Text);
				cboRelease.Items.Add(lblReleased.Text);
				cboRelease.SelectedIndex = 0;
				//Fill Default Master Location 
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
		/// InitVariable()
		/// </summary>
		/// <Author>TraDa</Author>
		/// <Date>Thursday, June 2, 2005</Date>
		private void InitVariable()
		{
			const string METHOD_NAME = THIS + ".InitVariable()";
			try
			{
				
				// Load combo box
				UtilsBO boUtil = new UtilsBO();
				DataSet dstCCN = boUtil.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
				dtmFromStartDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				dtmToStartDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				//chkSelectAll.Visible = false;
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
		/// 5. System display match work order line on grid based on search conditions and unreleased work order line 
		///		•	Work Order: Work Order number
		///		•	Line: Work Order line
		///		•	Part Number: part number in work order line
		///		•	Part Name: part name in work order line
		///		•	Model: model in work order line
		///		•	UM: stock unit of item
		///		•	Quantity: quantity in work order line
		///		•	Estimated  Cost: standard cost
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 6 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			try
			{
				if (cboRelease.SelectedIndex == -1)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					cboRelease.Focus();
					return;
				}
				if (txtMasLoc.Text.ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					txtMasLoc.Focus();
					return;
				}
				if (!IsValidateToDate())
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RELEASE_WO_VALIDATETODATE, MessageBoxIcon.Error);
					dtmToStartDate.Focus();
					return;
				}
				else 
				{
					
					//Bind data to grid
					BindDataToGrid();
					if (blnSearchResult)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_WCDISPATCH_FOUND_NOROW, MessageBoxIcon.Information);
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
		/// Bind data to Grid
		///  </summary>
		///  <author>TraDa</author>
		/// <date>Monday, June 6 2005</date>
		private void BindDataToGrid()
		{
			const string METHOD_NAME = THIS + ".BindDataToGrid()";
			int pintCCN, pintMasLoc;
			string pstrWONo;
			DateTime pdtmFromStartDate = new DateTime();
			DateTime pdtmToStartDate = new DateTime();

			try
			{
				
				pintCCN = int.Parse(cboCCN.SelectedValue.ToString());
				pintMasLoc = voMasLoc.MasterLocationID;
				pstrWONo = txtWorkOrder.Text.ToString();
				
				int intProLineID = 0;
				if (txtProductionLine.Text != string.Empty) intProLineID = (int) txtProductionLine.Tag;

				if (dtmFromStartDate.Value.ToString() != string.Empty)
				{
					pdtmFromStartDate = DateTime.Parse(dtmFromStartDate.Value.ToString());
				}
				if (dtmToStartDate.Value.ToString() != string.Empty)
				{
					pdtmToStartDate = DateTime.Parse(dtmToStartDate.Value.ToString());
				}	
				//List unrelease WO
				
				ReleaseWorkOrderBO boUnreleaseWO = new ReleaseWorkOrderBO();
				if (cboRelease.SelectedIndex == 0)
				{
					dstUnreleaseWO = boUnreleaseWO.SearchUnReleaseWO(pintCCN, pintMasLoc, pstrWONo, intProLineID, pdtmFromStartDate, pdtmToStartDate, (int) WOLineStatus.Unreleased);
				}
				else
					dstUnreleaseWO = boUnreleaseWO.SearchUnReleaseWO(pintCCN, pintMasLoc, pstrWONo, intProLineID, pdtmFromStartDate, pdtmToStartDate, (int) WOLineStatus.Released);
				if (dstUnreleaseWO.Tables[0].Rows.Count == 0)
				{
//					PCSMessageBox.Show(ErrorCode.MESSAGE_WCDISPATCH_FOUND_NOROW, MessageBoxIcon.Information);
					blnSearchResult = true;

				}
				else
				{
					blnSearchResult = false;
				}
				
				foreach (DataRow drow in dstUnreleaseWO.Tables[0].Rows)
				{
					drow[SELECT] = false;
				}
				gridData.DataSource = dstUnreleaseWO.Tables[0];
				FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
				gridData.Splits[0].DisplayColumns[SELECT].DataColumn.ValueItems.Presentation  = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
				gridData.Splits[0].DisplayColumns[SELECT].Style.HorizontalAlignment = AlignHorzEnum.Center;
				gridData.Splits[0].DisplayColumns[WONO].Locked = true;
				gridData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].Visible = false;
				gridData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.STATUS_FLD].Visible = false;

				gridData.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.CCNID_FLD].Visible = false;
				gridData.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD].Visible = false;
				gridData.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].Visible = false;
				gridData.Splits[0].DisplayColumns[ITM_ProductTable.PRODUCTID_FLD].Visible = false;
				
				gridData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.LINE_FLD].Locked = true;
				gridData.Splits[0].DisplayColumns[PARTNUMBER].Locked = true;
				gridData.Splits[0].DisplayColumns[PARTNAME].Locked = true;
				gridData.Splits[0].DisplayColumns[MODEL].Locked = true;
				gridData.Splits[0].DisplayColumns[UM].Locked = true;
				gridData.Splits[0].DisplayColumns[QUANTITY].Locked = true;
				gridData.Columns[PRO_WorkOrderDetailTable.STARTDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				gridData.Columns[PRO_WorkOrderDetailTable.DUEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				gridData.Columns[QUANTITY].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				gridData.Splits[0].DisplayColumns[ESTCOST].Locked = true;
				gridData.Columns[ESTCOST].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				gridData.Splits[0].DisplayColumns[ESTCOST].Visible = false;
				//Set new status for chkSelectAll
				
				if (gridData.RowCount == 0)
				{
					chkSelectAll.Checked = false;
				}
				else
				{
					for (int i =0; i <gridData.RowCount; i++)
					{
						if (gridData[i, SELECT].ToString().Trim() != TRUE)
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
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}		
		
		}
		/// <summary>
		/// CreateDataSet
		/// </summary>
		/// <author>Trada</author>
		/// <Date>Thursday, October 13 2005</Date>
		private void CreateDataSet()
		{
			const string METHOD_NAME = THIS + ".CreateDataSet()";
			const string PRODUCTION_LINE = "ProductionLine";
			try
			{
				
				dstGridData = new DataSet();
				dstGridData.Tables.Add(RELEASEWO_TABLE);
				dstGridData.Tables[RELEASEWO_TABLE].Columns.Add(SELECT);
				dstGridData.Tables[RELEASEWO_TABLE].Columns.Add(PRODUCTION_LINE);
				dstGridData.Tables[RELEASEWO_TABLE].Columns.Add(PRO_WorkOrderMasterTable.WORKORDERNO_FLD);
				dstGridData.Tables[RELEASEWO_TABLE].Columns.Add(PRO_WorkOrderDetailTable.LINE_FLD);
				dstGridData.Tables[RELEASEWO_TABLE].Columns.Add(PARTNUMBER);
				dstGridData.Tables[RELEASEWO_TABLE].Columns.Add(PARTNAME);
				dstGridData.Tables[RELEASEWO_TABLE].Columns.Add(MODEL);
				dstGridData.Tables[RELEASEWO_TABLE].Columns.Add(UM);
				dstGridData.Tables[RELEASEWO_TABLE].Columns.Add(PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD);
				dstGridData.Tables[RELEASEWO_TABLE].Columns.Add(ESTCOST);
				dstGridData.Tables[RELEASEWO_TABLE].Columns.Add(PRO_WorkOrderDetailTable.STARTDATE_FLD);
				dstGridData.Tables[RELEASEWO_TABLE].Columns.Add(PRO_WorkOrderDetailTable.DUEDATE_FLD);
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
		/// Set check all row in grid
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 6 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chkSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAll_CheckedChanged()";
			try
			{
				
				if ((blnStateOfCheck)&& (gridData.RowCount != 0))
				{
					for (int i  = 0; i < gridData.RowCount; i++)
					{
						gridData[i, SELECT] = chkSelectAll.Checked? true.ToString(): false.ToString();
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
		/// Call Release() from BO class
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 6 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRelease_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnRelease_Click()";
			try 
			{
				if (gridData.RowCount == 0)
				{
					throw new PCSException(ErrorCode.MESSAGE_PO_APPROVE_NO_DATA_IN_GRID, METHOD_NAME, null);
				}
				
				if (CheckDataGridToRelease())
				{
					ReleaseWorkOrderBO boReleaseWO = new ReleaseWorkOrderBO();
					if (cboRelease.SelectedIndex == 0)
					{
						boReleaseWO.ReleaseWO(dstUnreleaseWO);
					}
					else
						boReleaseWO.UnReleaseWO(dstUnreleaseWO);
					BindDataToGrid();
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
				}
				else 
					PCSMessageBox.Show(ErrorCode.MESSAGE_RELEASE_WO_SELECT_WOLINE, MessageBoxIcon.Error);

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
		/// CheckDataGridToRelease
		/// </summary>
		/// <author>Trada</author>
		/// <date>Thursday, June 16 2005</date>
		/// <returns>bool</returns>
		private bool CheckDataGridToRelease()
		{
			const string METHOD_NAME = THIS + ".CheckDataGridToRelease()";
			try 
			{
				int pintNumRow = 0;
				for (int i = 0; i < gridData.RowCount; i++)
				{
					if (gridData[i, SELECT].ToString().Trim() != TRUE)
					{
						pintNumRow++;
					}
				}
				//there is not any rows checked
				if (pintNumRow == gridData.RowCount)
				{
					return false;					
				}
				else return true;
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
		/// <Autho>Trada</Autho>
		/// <Date>Thursday, June 2, 2005</Date>
		/// <Description>
		/// btnMasLoc_Click
		/// </Description>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		
		private void btnMasLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMasLoc_Click()";
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
					if (voMasLoc.MasterLocationID != int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
					{
						txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
						voMasLoc.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
						txtWorkOrder.Text = string.Empty;
						CreateDataSet();
						gridData.DataSource = dstGridData.Tables[0];
						FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
					}
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
		/// btnWorkOrder_Click
		/// </summary>
		/// <Autho>Trada</Autho>
		/// <Date>Thursday, June 2, 2005</Date>
		/// <Description>
		/// btnWorkOrder_Click
		/// </Description>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnWorkOrder_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnWorkOrder_Click()";
			try 
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				//User has entered MasLoc
				#region Unused Code
//				if (txtMasLoc.Text.ToString() != string.Empty)
//				{
//					htbCriteria.Add(PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD, voMasLoc.MasterLocationID.ToString());
//				}
//				else //User has not entered MasLoc
//				{
//					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
//					txtMasLoc.Focus();
//					return;
//				}
//				htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int)WOLineStatus.Unreleased);
//				drwResult = FormControlComponents.OpenSearchForm(V_WOUNRELEASE, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, txtWorkOrder.Text, htbCriteria, true);
//				if (drwResult != null)
//				{
//					if ((txtWorkOrder.Tag != null) && (txtWorkOrder.Tag.ToString() != drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString()))
//					{
//						CreateDataSet();
//						gridData.DataSource = dstGridData.Tables[0];
//						FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
//						txtWorkOrder.Text = drwResult[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString();
//						txtWorkOrder.Tag = drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD];
//					}
//					else
//					{
//						txtWorkOrder.Text = drwResult[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString();
//						txtWorkOrder.Tag = drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD];
//					}
//				}
//				else
//				{
//					txtWorkOrder.Focus();
//				}
				#endregion
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
				
				if (txtProductionLine.Text == string.Empty)
				{
					string[] strMessages = new string[2];
					strMessages[0] = lblProductionLine.Text.Trim();
					strMessages[1] = lblWO.Text.Trim();
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strMessages);
					txtProductionLine.Focus();
					return;
				}
				else
				{
					htbCriteria.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, txtProductionLine.Tag);
					if (cboRelease.SelectedIndex == 0)
					{
						htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int)WOLineStatus.Unreleased);
						drwResult = FormControlComponents.OpenSearchForm(V_WOUNRELEASE, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, txtWorkOrder.Text.Trim(), htbCriteria, true);
					}
					else
					{
						htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int)WOLineStatus.Released);
						drwResult = FormControlComponents.OpenSearchForm(V_WOUNRELEASED, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, txtWorkOrder.Text.Trim(), htbCriteria, true);
					}
					
					if (drwResult != null)
					{
						if ((txtWorkOrder.Tag != null) && (txtWorkOrder.Tag.ToString() != drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString()))
						{
							CreateDataSet();
							gridData.DataSource = dstGridData.Tables[0];
							FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
						}	
						txtWorkOrder.Text = drwResult[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString();
						txtWorkOrder.Tag = drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD];
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
		/// btnMaintainWO_Click
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 6 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnMaintainWO_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMaintainWO_Click()";
			try 
			{
				if(gridData.RowCount > 0)
				{
					int pintWOMasterID = int.Parse(gridData[gridData.Row,PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString());
					WorkOrder frmWO = new WorkOrder(pintWOMasterID, int.Parse(cboCCN.SelectedValue.ToString()), voMasLoc.MasterLocationID, txtMasLoc.Text.Trim());
					frmWO.Show();
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
		/// Compare From Start date & To Start date
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, June 3, 2005</date>
		/// <returns></returns>
		private bool IsValidateToDate()
		{
			const string METHOD_NAME = THIS + ".IsValidateToDate()";
			try
			{
				//if ToDate < FromDate then return false else return true
				if ((dtmFromStartDate.Value.ToString() != string.Empty)&&(dtmToStartDate.Value.ToString() != string.Empty))
				{
					if (DateTime.Parse(dtmFromStartDate.Value.ToString()) > DateTime.Parse(dtmToStartDate.Value.ToString())) 
					{
						return false;
					}
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
		/// txtMasLoc_Leave
		/// </summary>
		/// <author>Trada</author>
		/// <date>Thursday, June 2 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtMasLoc_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_Leave()";
			if (txtMasLoc.Text == string.Empty)
			{
				voMasLoc.MasterLocationID =0;
				txtWorkOrder.Text = string.Empty;
				txtWorkOrder.Tag = null;
				CreateDataSet();
				gridData.DataSource = dstGridData.Tables[0];
				FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
				return;
			}
			try
			{
				OnLeaveControl(sender, e);
				UtilsBO boUtil = new UtilsBO();
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (!txtMasLoc.Modified) return;
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
					if (voMasLoc.MasterLocationID != int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
					{
						txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
						voMasLoc.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
						txtWorkOrder.Text = string.Empty;
						CreateDataSet();
						gridData.DataSource = dstGridData.Tables[0];
						FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
					}
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
		/// txtWorkOrder_Leave
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 6 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtWorkOrder_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtWorkOrder_Leave()";
			try
			{
				if (txtWorkOrder.Text != string.Empty)
				{
						
					DataRowView drwResult = null;
					Hashtable htbCriteria = new Hashtable();
					if (!txtWorkOrder.Modified) return;
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
					if (txtProductionLine.Text == string.Empty)
					{
						string[] strMessages = new string[2];
						strMessages[0] = lblProductionLine.Text.Trim();
						strMessages[1] = lblWO.Text.Trim();
						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strMessages);
						txtWorkOrder.Focus();
						return;
					}
					else
					{
						htbCriteria.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, txtProductionLine.Tag);
						if (cboRelease.SelectedIndex == 0)
						{
							htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int)WOLineStatus.Unreleased);
							drwResult = FormControlComponents.OpenSearchForm(V_WOUNRELEASE, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, txtWorkOrder.Text.Trim(), htbCriteria, true);
						}
						else
						{
							htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int)WOLineStatus.Released);
							drwResult = FormControlComponents.OpenSearchForm(V_WOUNRELEASED, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, txtWorkOrder.Text.Trim(), htbCriteria, true);
						}
						if (drwResult != null)
						{
							if ((txtWorkOrder.Tag != null) && (txtWorkOrder.Tag.ToString() != drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString()))
							{
								CreateDataSet();
								gridData.DataSource = dstGridData.Tables[0];
								FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
							}
							txtWorkOrder.Text = drwResult[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString();
							txtWorkOrder.Tag = drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD];
						}
						else txtWorkOrder.Focus();	
					}
				}
				else
				{
					CreateDataSet();
					gridData.DataSource = dstGridData.Tables[0];
					FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
					return;
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
		/// dtmToStartDate_Leave
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, June 3 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dtmToStartDate_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmToStartDate_Leave()";
			try
			{
				OnLeaveControl(sender, e);
							

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
		/// ReleaseWorkOrder_Closing
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 6 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ReleaseWorkOrder_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ReleaseWorkOrder_Closing()";
			try 
			{
				if(gridData.RowCount > 0)
				{
					for(int i = 0; i < gridData.RowCount; i++)
					{
						if (gridData[i, SELECT].ToString() == true.ToString())
						{
							DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_RELEASE_BEFORE_CLOSE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, RELEASE);
							switch (confirmDialog)
							{
								case DialogResult.Yes:
									//Save before exit
									btnRelease_Click(sender, new EventArgs());
									break;
								case DialogResult.No:
									break;
								case DialogResult.Cancel:
									e.Cancel = true;
									break;
							}
							return;
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
		/// gridData_AfterColUpdate
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 6 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			//blnStateOfCheck = false;
		}
		/// <summary>
		/// btnClose_Click
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 6 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		/// <summary>
		/// CheckOrNochkCheckAll
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 6 2005</date>
		private void CheckOrNochkCheckAll()
		{
			const string METHOD_NAME = THIS + ".CheckOrNochkCheckAll()";
			try
			{
				for (int i =0; i <gridData.RowCount; i++)
				{
					if (gridData[i, SELECT].ToString().Trim() != TRUE)
					{
						chkSelectAll.Checked = false;
						return;
					}
				}
				chkSelectAll.Checked = true;
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
		/// gridData_AfterColEdit
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, June 6 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridData_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			if (e.Column.DataColumn.DataField == SELECT)
			{
				CheckOrNochkCheckAll();
			}

		}

		//**************************************************************************              
		///    <Description>
		///       OnEnterControl
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
		/// ReleaseWorkOrder_KeyDown
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, June 7 2005</date>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ReleaseWorkOrder_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}

		/// <summary>
		/// txtMasLoc_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, June 22 2005</date>
		private void txtMasLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				btnMasLoc_Click(sender, e);
			}
		}

		/// <summary>
		/// chkSelectAll_Enter
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, June 22 2005</date>
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
		/// <date>Wednesday, June 22 2005</date>
		private void chkSelectAll_Leave(object sender, System.EventArgs e)
		{
			blnStateOfCheck = false;	
		}
		/// <summary>
		/// txtWorkOrder_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, June 29 2005</date>
		private void txtWorkOrder_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				btnWorkOrder_Click(sender, e);
			}
		}

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
					if (txtProductionLine.Tag != null)
					{
						if (int.Parse(txtProductionLine.Tag.ToString()) != int.Parse(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString()))
						{
							//clear work order
							txtWorkOrder.Text = string.Empty;
							txtWorkOrder.Tag = null;
						}
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
		private void txtProductionLine_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";
			try
			{
				if (!txtProductionLine.Modified) return;
				if (txtProductionLine.Text == string.Empty)
				{
					txtWorkOrder.Text = string.Empty;
					txtWorkOrder.Tag = null;
					CreateDataSet();
					gridData.DataSource = dstGridData.Tables[0];
					FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
					return;
				}
				Hashtable htbCriteria = new Hashtable();				
				htbCriteria.Add(MST_CCNTable.CCNID_FLD, (int) cboCCN.SelectedValue);

				//Call OpenSearchForm for selecting Production Line
				DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), null, false);
				
				//If has Production Line matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					if (txtProductionLine.Tag != null)
					{
						if (int.Parse(txtProductionLine.Tag.ToString()) != int.Parse(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString()))
						{
							//clear work order
							txtWorkOrder.Text = string.Empty;
							txtWorkOrder.Tag = null;
						}
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

		private void txtProductionLine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (btnProductionLine.Enabled && e.KeyCode == Keys.F4)
			{
				btnProductionLine_Click(sender, new EventArgs());
			}
		}
		/// <summary>
		/// cboRelease_SelectedIndexChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, May 24 2006</date>
		private void cboRelease_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboRelease_SelectedIndexChanged()";
			try
			{
				txtWorkOrder.Text = string.Empty;
				txtWorkOrder.Tag = null;
				const string RELEASE = "Release";
				const string UNRELEASE = "UnRelease";
				if (cboRelease.SelectedIndex == 0)
				{
					btnRelease.Text = RELEASE;
				}
				else
					if (cboRelease.SelectedIndex == 1)
					{
						btnRelease.Text = UNRELEASE;
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
		/// txtWorkOrder_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 30 2006</date>
		private void txtWorkOrder_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtWorkOrder_Validating()";
			try
			{
				if (!txtWorkOrder.Modified) return;
				if (txtWorkOrder.Text == string.Empty)
				{
					txtWorkOrder.Tag = null;
					CreateDataSet();
					gridData.DataSource = dstGridData.Tables[0];
					FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
					return;
				}
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();
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
				if (txtProductionLine.Text == string.Empty)
				{
					string[] strMessages = new string[2];
					strMessages[0] = lblProductionLine.Text.Trim();
					strMessages[1] = lblWO.Text.Trim();
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strMessages);
					txtProductionLine.Focus();
					return;
				}
				else
				{
					htbCriteria.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, txtProductionLine.Tag);
					if (cboRelease.SelectedIndex == 0)
					{
						htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int)WOLineStatus.Unreleased);
						drwResult = FormControlComponents.OpenSearchForm(V_WOUNRELEASE, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, txtWorkOrder.Text.Trim(), htbCriteria, false);
					}
					else
					{
						htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int)WOLineStatus.Released);
						drwResult = FormControlComponents.OpenSearchForm(V_WOUNRELEASED, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, txtWorkOrder.Text.Trim(), htbCriteria, false);
					}
					if (drwResult != null)
					{
						if ((txtWorkOrder.Tag != null) && (txtWorkOrder.Tag.ToString() != drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString()))
						{
							CreateDataSet();
							gridData.DataSource = dstGridData.Tables[0];
							FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
						}
						txtWorkOrder.Text = drwResult[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString();
						txtWorkOrder.Tag = drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD];
					}
					else 
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
		/// txtMasLoc_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 30 2006</date>
		private void txtMasLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_Validating()";
			if (txtMasLoc.Text == string.Empty)
			{
				voMasLoc.MasterLocationID =0;
				txtWorkOrder.Text = string.Empty;
				txtWorkOrder.Tag = null;
				CreateDataSet();
				gridData.DataSource = dstGridData.Tables[0];
				FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
				return;
			}
			try
			{
				OnLeaveControl(sender, e);
				UtilsBO boUtil = new UtilsBO();
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (!txtMasLoc.Modified) return;
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
					if (voMasLoc.MasterLocationID != int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
					{
						txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
						voMasLoc.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
						txtWorkOrder.Text = string.Empty;
						CreateDataSet();
						gridData.DataSource = dstGridData.Tables[0];
						FormControlComponents.RestoreGridLayout(gridData, dtbGridLayOut);
					}
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					voMasLoc.MasterLocationID = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
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
	}
}
