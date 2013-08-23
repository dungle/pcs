using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using C1.Win.C1Input;
using PCSComUtils.PCSExc;
using PCSUtils.Utils;
using PCSUtils.Log;
using PCSUtils.MasterSetup;
using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.Common.BO;
using PCSComMaterials.Plan.BO;
using PCSComMaterials.Plan.DS;
using C1.Win.C1TrueDBGrid;
namespace PCSMaterials.Mps
{
	/// <summary>
	/// Summary description for MPSCycleOption.
	/// </summary>
	public class MPSCycleOption : System.Windows.Forms.Form
	{
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Label lblCycle;
		private System.Windows.Forms.Label lblCycleDesc;
		private System.Windows.Forms.Label lblMPSGenDate;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnPrint;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.TextBox txtCycle;
		private System.Windows.Forms.TextBox txtCycleDecription;
		private System.Windows.Forms.Button btnCycleSearch;
		private DataSet dstGridData;
		private DataTable dtbGridLayOut;
		private DateTime dtmDateOnly;
		private DateTime dtmSpecialDate = new DateTime(1899,12,30, 0,0,0);
		private const string MASTERLOCATIONCODE = "MasterLocationCode";
		const string THIS = "PCSMaterials.Mps.MPSCycleOption";
		EnumAction formMode;
		UtilsBO boUtil = new UtilsBO();
		private bool blnHasError = false;
		private int pintCycleOptionMasterID;
		private C1.Win.C1Input.C1DateEdit dtmToDate;
		private System.Windows.Forms.Label lblFromDate;
		private System.Windows.Forms.Label lblToDate;
		private C1.Win.C1Input.C1DateEdit dtmFromDate;
		private System.Windows.Forms.Label lblPlanHorizon;
		private C1.Win.C1Input.C1NumericEdit txtPlanHorizon;
		private C1.Win.C1Input.C1DateEdit txtGenDateTime;
		private System.Windows.Forms.Label lblGroupBy;
		private System.Windows.Forms.ComboBox cboGroupBy;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MPSCycleOption()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public MPSCycleOption(int pintMPSCycleOptionMasterID)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.pintCycleOptionMasterID = pintMPSCycleOptionMasterID;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MPSCycleOption));
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.lblCycle = new System.Windows.Forms.Label();
			this.lblFromDate = new System.Windows.Forms.Label();
			this.lblCycleDesc = new System.Windows.Forms.Label();
			this.txtCycleDecription = new System.Windows.Forms.TextBox();
			this.lblMPSGenDate = new System.Windows.Forms.Label();
			this.lblToDate = new System.Windows.Forms.Label();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
			this.btnCycleSearch = new System.Windows.Forms.Button();
			this.txtCycle = new System.Windows.Forms.TextBox();
			this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
			this.lblPlanHorizon = new System.Windows.Forms.Label();
			this.txtPlanHorizon = new C1.Win.C1Input.C1NumericEdit();
			this.txtGenDateTime = new C1.Win.C1Input.C1DateEdit();
			this.lblGroupBy = new System.Windows.Forms.Label();
			this.cboGroupBy = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPlanHorizon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtGenDateTime)).BeginInit();
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
			this.dgrdData.Location = new System.Drawing.Point(5, 128);
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
			this.dgrdData.Size = new System.Drawing.Size(453, 158);
			this.dgrdData.TabIndex = 17;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Master Loca" +
				"tion\" DataField=\"MasterLocationCode\"><ValueItems /><GroupInfo /></C1DataColumn><" +
				"C1DataColumn Level=\"0\" Caption=\"On Hand\" DataField=\"OnHand\" DefaultValue=\"false\"" +
				"><ValueItems Presentation=\"CheckBox\" Translate=\"True\" /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"Approved P/O\" DataField=\"PurchaseOrder\" Def" +
				"aultValue=\"false\"><ValueItems Presentation=\"CheckBox\" Translate=\"True\" /><GroupI" +
				"nfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Supply WO\" DataField=\"Supp" +
				"lyWO\" DefaultValue=\"false\"><ValueItems Presentation=\"CheckBox\" Translate=\"True\" " +
				"/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Sale Order\" DataF" +
				"ield=\"SaleOrder\" DefaultValue=\"false\"><ValueItems Presentation=\"CheckBox\" Transl" +
				"ate=\"True\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Demand" +
				" WO\" DataField=\"DemandWO\" DefaultValue=\"false\"><ValueItems Presentation=\"CheckBo" +
				"x\" Translate=\"True\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Captio" +
				"n=\"Safety Stock\" DataField=\"SafetyStock\" DefaultValue=\"false\"><ValueItems Presen" +
				"tation=\"CheckBox\" Translate=\"True\" /><GroupInfo /></C1DataColumn></DataCols><Sty" +
				"les type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Style50{}Style51{}Sty" +
				"le52{AlignHorz:Near;}Style53{AlignHorz:Near;}Style54{}Caption{AlignHorz:Center;}" +
				"Style27{}Normal{Font:Microsoft Sans Serif, 8.25pt;}Style25{}Selected{ForeColor:H" +
				"ighlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}Style14{}Style15{}St" +
				"yle16{AlignHorz:Near;ForeColor:Maroon;}Style17{AlignHorz:Near;}Style10{AlignHorz" +
				":Near;}Style11{}OddRow{}Style13{}Style46{AlignHorz:Near;}Group{BackColor:Control" +
				"Dark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style38{}Style36{}Style37{}Style3" +
				"3{}Style4{}Style3{}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}HighlightRow{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Style26{}RecordSelector{AlignImage:" +
				"Center;}Footer{}Style23{AlignHorz:Near;}Style22{AlignHorz:Near;}Style21{}Style55" +
				"{}Style56{}Style57{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCap" +
				"tion;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;AlignVert:Center;Border:Raised,," +
				"1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style49{}Style48{}Style24{}S" +
				"tyle7{}Style6{}Style20{}Style5{}Style41{AlignHorz:Near;}Style40{AlignHorz:Near;}" +
				"Style43{}Style42{}Style45{}Style44{}Style47{AlignHorz:Near;}Style9{}Style8{}Styl" +
				"e39{}FilterBar{}Style12{}Style34{AlignHorz:Near;}Style35{AlignHorz:Near;}Style32" +
				"{}Style1{}Style30{}Style31{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid" +
				".MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeigh" +
				"t=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"" +
				"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 449, 154" +
				"</ClientRect><BorderSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10" +
				"\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me" +
				"=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle paren" +
				"t=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle" +
				" parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Sty" +
				"le7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRo" +
				"w\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><Se" +
				"lectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /" +
				"><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><St" +
				"yle parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><" +
				"EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=" +
				"\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visi" +
				"ble><ColumnDivider>DarkGray,Single</ColumnDivider><Width>88</Width><Height>15</H" +
				"eight><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
				"tyle2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=" +
				"\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeader" +
				"Style parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
				"26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Widt" +
				"h>54</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColum" +
				"n><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style5" +
				"3\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me" +
				"=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle " +
				"parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnDivider>DarkGray,Si" +
				"ngle</ColumnDivider><Width>70</Width><Height>15</Height><DCIdx>6</DCIdx></C1Disp" +
				"layColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style p" +
				"arent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><Edito" +
				"rStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
				"e33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><" +
				"ColumnDivider>DarkGray,Single</ColumnDivider><Width>77</Width><Height>15</Height" +
				"><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Styl" +
				"e3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle" +
				" parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /" +
				"><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>66<" +
				"/Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" />" +
				"<FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
				"le49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle paren" +
				"t=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single<" +
				"/ColumnDivider><Width>70</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent" +
				"=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><Colum" +
				"nDivider>DarkGray,Single</ColumnDivider><Width>72</Width><Height>15</Height><DCI" +
				"dx>4</DCIdx></C1DisplayColumn></internalCols></C1.Win.C1TrueDBGrid.MergeView></S" +
				"plits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Hea" +
				"ding\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Captio" +
				"n\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected" +
				"\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow" +
				"\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><" +
				"Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBa" +
				"r\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplit" +
				"s><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</Def" +
				"aultRecSelWidth><ClientArea>0, 0, 449, 154</ClientArea><PrintPageHeaderStyle par" +
				"ent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// lblCycle
			// 
			this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
			this.lblCycle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCycle.Location = new System.Drawing.Point(6, 6);
			this.lblCycle.Name = "lblCycle";
			this.lblCycle.Size = new System.Drawing.Size(104, 20);
			this.lblCycle.TabIndex = 2;
			this.lblCycle.Text = "Cycle";
			this.lblCycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblFromDate
			// 
			this.lblFromDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblFromDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblFromDate.Location = new System.Drawing.Point(6, 54);
			this.lblFromDate.Name = "lblFromDate";
			this.lblFromDate.Size = new System.Drawing.Size(104, 20);
			this.lblFromDate.TabIndex = 7;
			this.lblFromDate.Text = "From Date";
			this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCycleDesc
			// 
			this.lblCycleDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCycleDesc.Location = new System.Drawing.Point(6, 30);
			this.lblCycleDesc.Name = "lblCycleDesc";
			this.lblCycleDesc.Size = new System.Drawing.Size(104, 20);
			this.lblCycleDesc.TabIndex = 5;
			this.lblCycleDesc.Text = "Cycle Description";
			this.lblCycleDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtCycleDecription
			// 
			this.txtCycleDecription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCycleDecription.Location = new System.Drawing.Point(124, 30);
			this.txtCycleDecription.MaxLength = 200;
			this.txtCycleDecription.Name = "txtCycleDecription";
			this.txtCycleDecription.Size = new System.Drawing.Size(334, 20);
			this.txtCycleDecription.TabIndex = 6;
			this.txtCycleDecription.Text = "";
			this.txtCycleDecription.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtCycleDecription.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblMPSGenDate
			// 
			this.lblMPSGenDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMPSGenDate.Location = new System.Drawing.Point(6, 101);
			this.lblMPSGenDate.Name = "lblMPSGenDate";
			this.lblMPSGenDate.Size = new System.Drawing.Size(120, 17);
			this.lblMPSGenDate.TabIndex = 15;
			this.lblMPSGenDate.Text = "Generation Date, Time";
			this.lblMPSGenDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblToDate
			// 
			this.lblToDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblToDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblToDate.Location = new System.Drawing.Point(6, 76);
			this.lblToDate.Name = "lblToDate";
			this.lblToDate.Size = new System.Drawing.Size(104, 20);
			this.lblToDate.TabIndex = 11;
			this.lblToDate.Text = "To Date";
			this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnEdit.Location = new System.Drawing.Point(125, 290);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(60, 23);
			this.btnEdit.TabIndex = 20;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(184, 290);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 21;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(65, 290);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 19;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAdd.Location = new System.Drawing.Point(5, 290);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(60, 23);
			this.btnAdd.TabIndex = 18;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(398, 290);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 24;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(338, 290);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 23;
			this.btnHelp.Text = "&Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPrint.Location = new System.Drawing.Point(243, 290);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(60, 23);
			this.btnPrint.TabIndex = 22;
			this.btnPrint.Text = "&Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
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
			this.cboCCN.ContentHeight = 15;
			this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(378, 5);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(80, 21);
			this.cboCCN.TabIndex = 1;
			this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
				"ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:N" +
				"ear;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Cente" +
				"r;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Sty" +
				"le10{}Style11{}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
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
			// lblCCN
			// 
			this.lblCCN.AccessibleDescription = "";
			this.lblCCN.AccessibleName = "";
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(346, 6);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(30, 19);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dtmFromDate
			// 
			this.dtmFromDate.AccessibleDescription = "";
			this.dtmFromDate.AccessibleName = "";
			// 
			// dtmFromDate.Calendar
			// 
			this.dtmFromDate.Calendar.AccessibleDescription = "";
			this.dtmFromDate.Calendar.AccessibleName = "";
			this.dtmFromDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmFromDate.CustomFormat = "dd-MM-yyyy";
			this.dtmFromDate.EmptyAsNull = true;
			this.dtmFromDate.ErrorInfo.ShowErrorMessage = false;
			this.dtmFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmFromDate.Location = new System.Drawing.Point(124, 54);
			this.dtmFromDate.Name = "dtmFromDate";
			this.dtmFromDate.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									  new C1.Win.C1Input.ValueInterval(new System.DateTime(1000, 10, 21, 0, 0, 0, 0), new System.DateTime(3000, 10, 21, 0, 0, 0, 0), true, true)});
			this.dtmFromDate.Size = new System.Drawing.Size(98, 20);
			this.dtmFromDate.TabIndex = 8;
			this.dtmFromDate.Tag = null;
			this.dtmFromDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmFromDate.TextChanged += new System.EventHandler(this.dtmToDate_TextChanged);
			this.dtmFromDate.Leave += new System.EventHandler(this.dtmToDate_TextChanged);
			// 
			// btnCycleSearch
			// 
			this.btnCycleSearch.AccessibleDescription = "";
			this.btnCycleSearch.AccessibleName = "";
			this.btnCycleSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCycleSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCycleSearch.Location = new System.Drawing.Point(224, 6);
			this.btnCycleSearch.Name = "btnCycleSearch";
			this.btnCycleSearch.Size = new System.Drawing.Size(24, 20);
			this.btnCycleSearch.TabIndex = 4;
			this.btnCycleSearch.Text = "...";
			this.btnCycleSearch.Click += new System.EventHandler(this.btnCycleSearch_Click);
			// 
			// txtCycle
			// 
			this.txtCycle.AccessibleDescription = "";
			this.txtCycle.AccessibleName = "";
			this.txtCycle.Location = new System.Drawing.Point(124, 6);
			this.txtCycle.MaxLength = 20;
			this.txtCycle.Name = "txtCycle";
			this.txtCycle.Size = new System.Drawing.Size(98, 20);
			this.txtCycle.TabIndex = 3;
			this.txtCycle.Text = "";
			this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCycle_KeyDown);
			this.txtCycle.Leave += new System.EventHandler(this.txtCycle_Leave);
			this.txtCycle.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// dtmToDate
			// 
			this.dtmToDate.CustomFormat = "dd-MM-yyyy";
			this.dtmToDate.EmptyAsNull = true;
			this.dtmToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmToDate.Location = new System.Drawing.Point(124, 78);
			this.dtmToDate.Name = "dtmToDate";
			this.dtmToDate.Size = new System.Drawing.Size(98, 20);
			this.dtmToDate.TabIndex = 12;
			this.dtmToDate.Tag = null;
			this.dtmToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmToDate.TextChanged += new System.EventHandler(this.dtmToDate_TextChanged);
			this.dtmToDate.Leave += new System.EventHandler(this.dtmToDate_TextChanged);
			// 
			// lblPlanHorizon
			// 
			this.lblPlanHorizon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblPlanHorizon.Location = new System.Drawing.Point(304, 54);
			this.lblPlanHorizon.Name = "lblPlanHorizon";
			this.lblPlanHorizon.Size = new System.Drawing.Size(74, 20);
			this.lblPlanHorizon.TabIndex = 9;
			this.lblPlanHorizon.Text = "Plan Horizon";
			this.lblPlanHorizon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtPlanHorizon
			// 
			this.txtPlanHorizon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPlanHorizon.EmptyAsNull = true;
			this.txtPlanHorizon.Location = new System.Drawing.Point(378, 54);
			this.txtPlanHorizon.Name = "txtPlanHorizon";
			this.txtPlanHorizon.ReadOnly = true;
			this.txtPlanHorizon.Size = new System.Drawing.Size(80, 20);
			this.txtPlanHorizon.TabIndex = 10;
			this.txtPlanHorizon.Tag = null;
			this.txtPlanHorizon.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// txtGenDateTime
			// 
			this.txtGenDateTime.Location = new System.Drawing.Point(124, 102);
			this.txtGenDateTime.Name = "txtGenDateTime";
			this.txtGenDateTime.ReadOnly = true;
			this.txtGenDateTime.Size = new System.Drawing.Size(132, 20);
			this.txtGenDateTime.TabIndex = 16;
			this.txtGenDateTime.Tag = null;
			this.txtGenDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtGenDateTime.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// lblGroupBy
			// 
			this.lblGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblGroupBy.Location = new System.Drawing.Point(304, 80);
			this.lblGroupBy.Name = "lblGroupBy";
			this.lblGroupBy.Size = new System.Drawing.Size(74, 20);
			this.lblGroupBy.TabIndex = 13;
			this.lblGroupBy.Text = "Group";
			this.lblGroupBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboGroupBy
			// 
			this.cboGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGroupBy.Items.AddRange(new object[] {
															"By Hour",
															"By Day"});
			this.cboGroupBy.Location = new System.Drawing.Point(378, 80);
			this.cboGroupBy.Name = "cboGroupBy";
			this.cboGroupBy.Size = new System.Drawing.Size(80, 21);
			this.cboGroupBy.TabIndex = 14;
			// 
			// MPSCycleOption
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(464, 319);
			this.Controls.Add(this.cboGroupBy);
			this.Controls.Add(this.txtGenDateTime);
			this.Controls.Add(this.txtPlanHorizon);
			this.Controls.Add(this.lblPlanHorizon);
			this.Controls.Add(this.dtmToDate);
			this.Controls.Add(this.txtCycle);
			this.Controls.Add(this.txtCycleDecription);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.btnCycleSearch);
			this.Controls.Add(this.dtmFromDate);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.lblCycleDesc);
			this.Controls.Add(this.lblCycle);
			this.Controls.Add(this.lblMPSGenDate);
			this.Controls.Add(this.lblToDate);
			this.Controls.Add(this.lblFromDate);
			this.Controls.Add(this.lblGroupBy);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "MPSCycleOption";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MPS Cycle Option";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MPSCycleOption_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MPSCycleOption_Closing);
			this.Load += new System.EventHandler(this.MPSCycleOption_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPlanHorizon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtGenDateTime)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// MPSCycleOption_Load. 
		/// When user call form load with parameter is the MPSCycleOptionMasterID
		/// we need to load form with all value retrieve from database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>
		/// - Tuesday, August 9 2005
		/// - August 12 2005 : DungLA
		/// </date>
		private void MPSCycleOption_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".MPSCycleOption_Load()";
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
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
				#region set default selected group by method - DungLA - Dec 13, 2005
				cboGroupBy.SelectedIndex = 0;
				#endregion
				//Store grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				//Switch form Mode
				formMode = EnumAction.Default;
				SwitchFormMode();
				//Set format for txtPlanHorizon
				txtPlanHorizon.FormatType = FormatTypeEnum.CustomFormat;
				txtPlanHorizon.CustomFormat = Constants.INTERGER_NUMBERFORMAT;
				//Set format for txtGenerateDate
				txtGenDateTime.FormatType = FormatTypeEnum.CustomFormat;
				txtGenDateTime.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				
				#region Load form with ID - DungLA - August 12, 2005

				// check if form load with parameter is Cycle option master ID
				if (pintCycleOptionMasterID > 0)
				{
					// get detail value and fill to grid
					FillDataGrid(pintCycleOptionMasterID);
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					//Hack: Trada 15-11-2005
					btnEdit.Enabled = true;			
					btnDelete.Enabled = true;
					// END: Trada 15-11-2005
				}

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
		/// <summary>
		/// ClearForm
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, August 9 2005</date>
		private void ClearForm()
		{
			const string METHOD_NAME = THIS + ".ClearForm()";
			try
			{
				txtCycle.Text = string.Empty;
				txtCycleDecription.Text = string.Empty;
				txtGenDateTime.Value = null;
				dtmToDate.Value = null;
				dtmFromDate.Value = null;
				txtPlanHorizon.Value = null;
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
		/// <date>Wednesday, August 10 2005</date>
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

                //dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.ONHAND_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.SALEORDER_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[MASTERLOCATIONCODE].Locked = pblnLock;
				
				//Set check boxes to all columns
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.ONHAND_FLD].DataColumn.ValueItems.Presentation  = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;				
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.ONHAND_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;

				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD].DataColumn.ValueItems.Presentation  = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;

				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD].DataColumn.ValueItems.Presentation  = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD].DataColumn.ValueItems.Presentation  = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.SALEORDER_FLD].DataColumn.ValueItems.Presentation  = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.SALEORDER_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD].DataColumn.ValueItems.Presentation  = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox;
				dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;

				if (!pblnLock)
				{
					dgrdData.Splits[0].DisplayColumns[MASTERLOCATIONCODE].Button = true;
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
		/// SwitchFormMode
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, August 9 2005</date>
		private void SwitchFormMode()
		{
			const string METHOD_NAME = THIS + ".SwitchFormMode()";
			try
			{
				switch (formMode)
				{
					case EnumAction.Default:
						txtCycleDecription.Enabled = false;
						txtGenDateTime.Enabled = false;
						dtmToDate.Enabled = false;
						if (pintCycleOptionMasterID > 0)
						{
							btnDelete.Enabled = true;
							btnEdit.Enabled = true;
						}
						else
						{
							btnDelete.Enabled = false;
							btnEdit.Enabled = false;
						}
						btnPrint.Enabled = false;
						btnSave.Enabled = false;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						btnCycleSearch.Enabled = true;
						dtmFromDate.Enabled = false;
						cboCCN.Enabled = false;
						cboGroupBy.Enabled = false;
						//Lock the grid
						ConfigGrid(true);	
						break;
					case EnumAction.Add:
						txtCycleDecription.Enabled = true;
						txtGenDateTime.Enabled = true;
						dtmToDate.Enabled = true;
						btnDelete.Enabled = false;
						btnEdit.Enabled = false;
						btnPrint.Enabled = true;
						btnSave.Enabled = true;
						btnCycleSearch.Enabled = false;
						dtmFromDate.Enabled = true;
						cboCCN.Enabled = false;
						cboGroupBy.Enabled = true;
						ConfigGrid(false);
						dgrdData.AllowDelete = true;
						break;
					case EnumAction.Edit:
						txtCycleDecription.Enabled = true;
						txtGenDateTime.Enabled = true;
						dtmToDate.Enabled = true;
						btnDelete.Enabled = false;
						btnEdit.Enabled = false;
						btnPrint.Enabled = true;
						btnSave.Enabled = true;
						btnCycleSearch.Enabled = false;
						btnAdd.Enabled = false;
						dtmFromDate.Enabled = true;
						cboCCN.Enabled = false;
						cboGroupBy.Enabled = true;
						ConfigGrid(false);
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
		/// btnAdd_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, August 9 2005</date>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				formMode = EnumAction.Add;
				ClearForm();
				SwitchFormMode();
				txtCycle.Focus();
				// Load PostDate
				MTR_MPSCycleOptionMasterVO voMTR_MPSCycleOptionMaster = new MTR_MPSCycleOptionMasterVO();
				voMTR_MPSCycleOptionMaster.AsOfDate = boUtil.GetDBDate();

				if((DateTime.MinValue < voMTR_MPSCycleOptionMaster.AsOfDate) && (voMTR_MPSCycleOptionMaster.AsOfDate < DateTime.MaxValue))
				{
					dtmFromDate.Value = voMTR_MPSCycleOptionMaster.AsOfDate;
				}
				else
				{
					dtmFromDate.Value = DBNull.Value;
				}

				//Disable Add button
				btnAdd.Enabled = false;
				//Fill data to controls
				CreateDataSet();
				dgrdData.DataSource = dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME];
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
		/// CreateDataSet
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, August 9 2005</date>
		private void CreateDataSet()
		{
			const string METHOD_NAME = THIS + ".CreateDataSet()";
			try
			{
				dstGridData = new DataSet();
				dstGridData.Tables.Add(MTR_MPSCycleOptionDetailTable.TABLE_NAME);

				//insert columns which is invisible but use to update
				dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONDETAILID_FLD);
				dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MPSCycleOptionDetailTable.MPSCYCLEOPTIONMASTERID_FLD);
				dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD);
				
				//insert display columns
				dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME].Columns.Add(MASTERLOCATIONCODE);
				dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MPSCycleOptionDetailTable.ONHAND_FLD);
				dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD);
				dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD);
				
				dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD);
				dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD);
				dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME].Columns.Add(MTR_MPSCycleOptionDetailTable.SALEORDER_FLD);
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
		/// <date>Wednesday, August 10 2005</date>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnHasError = true;
				if (IsValidateData())
				{
					MPSCycleOptionBO boMPSCycleOption = new MPSCycleOptionBO();
					//Make a new MPSCycleOptionMasterVO
					MTR_MPSCycleOptionMasterVO voMTR_MPSCycleOptionMaster = new MTR_MPSCycleOptionMasterVO();
					voMTR_MPSCycleOptionMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
					voMTR_MPSCycleOptionMaster.AsOfDate = dtmDateOnly;
					voMTR_MPSCycleOptionMaster.Cycle = txtCycle.Text.Trim();
					voMTR_MPSCycleOptionMaster.Description = txtCycleDecription.Text.Trim();
					voMTR_MPSCycleOptionMaster.PlanHorizon = ((DateTime)dtmToDate.Value - dtmDateOnly).Days;
					// HACK: dungla 12-13-2005
					voMTR_MPSCycleOptionMaster.GroupBy = cboGroupBy.SelectedIndex;
					// END: dungla 12-13-2005
					//Add
					if (formMode == EnumAction.Add)
					{
						//Add this new VO to MTR_MPSCycleOptionMaster Table
						pintCycleOptionMasterID = boMPSCycleOption.Add(dstGridData, voMTR_MPSCycleOptionMaster);
						voMTR_MPSCycleOptionMaster.MPSCycleOptionMasterID = pintCycleOptionMasterID;
					}
					if (formMode == EnumAction.Edit)
					{
						voMTR_MPSCycleOptionMaster.MPSCycleOptionMasterID = pintCycleOptionMasterID;
						//Update MTR_MPSCycleOptionMaster and Detail
						boMPSCycleOption.UpdateMasterAndDetail(dstGridData, voMTR_MPSCycleOptionMaster);
					}
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					//reload grid form database
					dstGridData = boMPSCycleOption.GetDetailByMasterID(pintCycleOptionMasterID);
					dgrdData.DataSource = dstGridData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					formMode = EnumAction.Default;
					SwitchFormMode();
					dgrdData.AllowDelete = false;
					btnAdd.Enabled = true;
					btnEdit.Enabled = true;
					btnDelete.Enabled = true;	
					blnHasError = false;
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				//Check if ScrapNo was duplicated
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					txtCycle.Focus();
					txtCycle.Select();
				}
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
		/// IsValidateData
		/// </summary>
		/// <author>Trada</author>
		/// <date>Wednesday, August 10 2005</date>
		private bool IsValidateData()
		{
			const string METHOD_NAME = THIS + ".IsValidateData()";
			try
			{
				//Check mandatory fields
				if (FormControlComponents.CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboCCN.Focus();
					cboCCN.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtCycle))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtCycle.Focus();
					txtCycle.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(dtmFromDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmFromDate.Focus();
					dtmFromDate.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(dtmToDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmToDate.Focus();
					dtmToDate.Select();
					return false;
				}
				//Check if To Date is smaller than From Date
				// HACK: Trada 23-11-2005
				dtmDateOnly = new DateTime(((DateTime)dtmFromDate.Value).Year, ((DateTime)dtmFromDate.Value).Month,((DateTime)dtmFromDate.Value).Day) ;
				if ((DateTime) dtmToDate.Value <= dtmDateOnly)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Warning);
					dtmToDate.Focus();
					return false;
				} 

				 // END: Trada 23-11-2005
				//Check if Master Location rows are NOT unique, raise error message 
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					for (int j = i + 1; j < dgrdData.RowCount; j++)
					{
						if (dgrdData[i, MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].ToString().Trim() == dgrdData[j, MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].ToString().Trim())
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_MPS_CYCLE_OPTION_DUPLICATE_MASTERLOCATION);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MASTERLOCATIONCODE]);
							dgrdData.Focus();
							return false;
						}
					}
				}
				//check if row in grid has data
				int intCountRow =0;
				for (int i =0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, MASTERLOCATIONCODE].ToString() != string.Empty)
					{
						intCountRow++;
					}
				}
				if (intCountRow ==0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_PO_APPROVE_NO_DATA_IN_GRID);
					dgrdData.Row = 0;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MASTERLOCATIONCODE]);
					dgrdData.Focus();
					return false;
				}
				//check mandatory field in grid
				for (int i =0; i < dgrdData.RowCount; i++)
				{
					//Check MasterLocation column
					if (dgrdData[i, MASTERLOCATIONCODE].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_SELECT_MASTERLOCATION, MessageBoxIcon.Error);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MASTERLOCATIONCODE]);
						dgrdData.Focus();
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
		/// btnEdit_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, August 10 2005</date>
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				//Switch form Mode
				formMode = EnumAction.Edit;
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
		/// btnDelete_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				const int DCP = 1;
				const int MRP = 2;
				const int DCPOPTION = 3;
				const string STR_DCP = "DCP";
				const string STR_DCPOPTION = "DCP Option";
				const string STR_MRP = "MRP";
				const string STR_MPS = "MPS";
				string[] strParam = new string[3];
				//Delete Detail and Master
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					MPSCycleOptionBO boMPSCycleOption = new MPSCycleOptionBO();
					int intType = boMPSCycleOption.DeleteCycleOptionMasterAndDetail(pintCycleOptionMasterID, dstGridData);
					switch (intType)
					{
						case DCP:
							
							strParam[0] = STR_MPS;
							strParam[1] = STR_DCP;
							strParam[2] = STR_DCP;
							PCSMessageBox.Show(ErrorCode.MESSAGE_CYCLE_OPTION_ALREADY_USED, MessageBoxIcon.Error, strParam);
							break;
						case DCPOPTION:
							
							strParam[0] = STR_MPS;
							strParam[1] = STR_DCPOPTION;
							strParam[2] = STR_DCPOPTION;
							PCSMessageBox.Show(ErrorCode.MESSAGE_CYCLE_OPTION_ALREADY_USED, MessageBoxIcon.Error, strParam);
							break;
						case MRP:
							strParam[0] = STR_MPS;
							strParam[1] = STR_MRP;
							strParam[2] = STR_MRP;
							PCSMessageBox.Show(ErrorCode.MESSAGE_CYCLE_OPTION_ALREADY_USED, MessageBoxIcon.Error, strParam);
							break;
						case 0:
							PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
							formMode = EnumAction.Default;
							ClearForm();
							CreateDataSet();
							dgrdData.DataSource = dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME];
							FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
							//Lock the grid
							SwitchFormMode();
							txtCycle.Focus();		
							break;
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

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// btnClose_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, August 9 2005</date>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// MPSCycleOption_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, August 9 2005</date>
		private void MPSCycleOption_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".MPSCycleOption_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F12)
				{
					if((formMode == EnumAction.Edit) || (formMode == EnumAction.Add)) 
					{
						DataRow drowNew = dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME].NewRow();
						dstGridData.Tables[MTR_MPSCycleOptionDetailTable.TABLE_NAME].Rows.Add(drowNew);
						dgrdData.Row = dgrdData.RowCount;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MASTERLOCATIONCODE]);
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
		/// dgrdData_ButtonClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, August 10 2005</date>
		private void dgrdData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (!btnSave.Enabled) return;
				//open the search form to select Master Location
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[MASTERLOCATIONCODE]))
				{
					if (cboCCN.SelectedIndex != -1)
					{
						htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);	
					}
					else //User has not selected CCN
					{
						htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
					}
					drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, dgrdData[dgrdData.Row, MASTERLOCATIONCODE].ToString(), htbCriteria, true);
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						int pintRow = dgrdData.Row;
						//dgrdData[pintRow, PRO_ComponentScrapDetailTable.LINE_FLD] = pintRow + 1;
						dgrdData[dgrdData.Row, MASTERLOCATIONCODE] = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
						if (dgrdData.Columns[MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].Text == string.Empty)
						{
							//Set false value for all check boxes
//							dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.ONHAND_FLD].DataColumn.Value = false;
//							dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD].DataColumn.Value = false;
//							dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.SALEORDER_FLD].DataColumn.Value = false;
//							dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD].DataColumn.Value = false;
//							dgrdData.Splits[0].DisplayColumns[MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD].DataColumn.Value = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.ONHAND_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.SALEORDER_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD] = false;
						}
						dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD] = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
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
		/// dgrdData_BeforeColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, August 10 2005</date>
		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";	
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (e.Column.DataColumn.DataField == MASTERLOCATIONCODE)
				{
					if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						if (cboCCN.SelectedIndex != -1)
						{
							htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);	
						}
						else //User has not selected CCN
						{
							htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
						}
						drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
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
		/// <date>Wednesday, August 10 2005</date>
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				DataRow drowResult = (DataRow) e.Column.DataColumn.Tag;
				//Fill Data to Master Location Column
				if (e.Column.DataColumn.DataField == MASTERLOCATIONCODE)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, MASTERLOCATIONCODE].ToString() == string.Empty))
					{
						int row = dgrdData.Row;
						dgrdData[row, MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD] = null;
						dgrdData[dgrdData.Row, MASTERLOCATIONCODE] = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row, MASTERLOCATIONCODE] = drowResult[MST_MasterLocationTable.CODE_FLD].ToString();
						if (dgrdData.Columns[MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD].Text == string.Empty)
						{
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.ONHAND_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.SAFETYSTOCK_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.PURCHASEORDER_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.SALEORDER_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.SUPPLYWO_FLD] = false;
							dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.DEMANDWO_FLD] = false;
						}
						dgrdData[dgrdData.Row, MTR_MPSCycleOptionDetailTable.MASTERLOCATIONID_FLD] = drowResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
						return;
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
		/// FillDataGrid
		/// </summary>
		/// <param name="pintCycleOptionMasterID"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void FillDataGrid(int pintCycleOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".FillDataGrid()";
			try
			{
				MPSCycleOptionBO boMPSCycleOption = new MPSCycleOptionBO();
				//Get data from CycleOptionMaster
				MTR_MPSCycleOptionMasterVO voMTR_MPSCycleOptionMaster = (MTR_MPSCycleOptionMasterVO) boMPSCycleOption.GetCycleOptionMaster(pintCycleOptionMasterID);
				//Fill Data to all controls
				txtCycle.Text = voMTR_MPSCycleOptionMaster.Cycle;
				txtCycle.Tag = voMTR_MPSCycleOptionMaster.MPSCycleOptionMasterID;
				cboCCN.SelectedValue = voMTR_MPSCycleOptionMaster.CCNID;
				if (voMTR_MPSCycleOptionMaster.Description != null)
				{
					txtCycleDecription.Text = voMTR_MPSCycleOptionMaster.Description;
				}
				else
					txtCycleDecription.Text = string.Empty;
				if ((voMTR_MPSCycleOptionMaster.MPSGenDate.ToString() != string.Empty) && (voMTR_MPSCycleOptionMaster.MPSGenDate != dtmSpecialDate))
				{
					txtGenDateTime.Value = voMTR_MPSCycleOptionMaster.MPSGenDate;
				}
				else
					txtGenDateTime.Value = null;
				int intPlanHorizon = 0;
				if (voMTR_MPSCycleOptionMaster.PlanHorizon != 0)
				{
					intPlanHorizon = voMTR_MPSCycleOptionMaster.PlanHorizon;
				}
				else 
					intPlanHorizon = 0;
				dtmFromDate.Value = voMTR_MPSCycleOptionMaster.AsOfDate;
				dtmToDate.Value = voMTR_MPSCycleOptionMaster.AsOfDate.AddDays(intPlanHorizon);
				// HACK: dungla 12-13-2005
				cboGroupBy.SelectedIndex = voMTR_MPSCycleOptionMaster.GroupBy;
				// END: dungla 12-13-2005
				//Get data from CycleOptionDetail Table by CycleOptionMasterID
				dstGridData = boMPSCycleOption.GetDetailByMasterID(pintCycleOptionMasterID);
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
		/// btnCycleSearch_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void btnCycleSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycleSearch_Click()";
			try 
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				//User has selected CCN
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);	
				}
				else //User has not selected CCN
				{
					htbCriteria.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, 0);
				}
				drwResult = FormControlComponents.OpenSearchForm(MTR_MPSCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtCycle.Text = drwResult[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].ToString();
					//Keep valua of CycleOptionMasterID 
					txtCycle.Tag = drwResult[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD];
					pintCycleOptionMasterID = int.Parse(txtCycle.Tag.ToString());
				}
				else
				{
					txtCycle.Focus();
					return;
				}
				FillDataGrid(pintCycleOptionMasterID);
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
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
		/// txtCycle_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void txtCycle_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_KeyDown()";
			if (!btnCycleSearch.Enabled)
			{
				return;
			}
			if (e.KeyCode == Keys.F4)
			{
				btnCycleSearch_Click(sender, e);	
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
		/// txtCycle_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void txtCycle_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_Leave()";
			try 
			{
				OnLeaveControl(sender, e);
				if (btnCycleSearch.Enabled)
				{
					if (!txtCycle.Modified) return;
					Hashtable htbCriteria = new Hashtable();
					DataRowView drwResult = null;
					//User has enter MasLoc
					if (cboCCN.SelectedIndex != -1)
					{
						htbCriteria.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);	
					}
					else //User has not selected CCN
					{
						htbCriteria.Add(MTR_MPSCycleOptionMasterTable.CCNID_FLD, 0);
					}
					if (txtCycle.Text != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(MTR_MPSCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							txtCycle.Text = drwResult[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].ToString();
							//Keep valua of CycleOptionMasterID 
							txtCycle.Tag = drwResult[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD];
							pintCycleOptionMasterID = int.Parse(txtCycle.Tag.ToString());
						}
						else
						{
							txtCycle.Focus();
							return;
						}
						FillDataGrid(pintCycleOptionMasterID);
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
						btnEdit.Enabled = true;			
						btnDelete.Enabled = true;
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
		/// MPSCycleOption_Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, August 11 2005</date>
		private void MPSCycleOption_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".MPSCycleOption_Closing()";
			try
			{
				if (formMode != EnumAction.Default)
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
		/// dtmToDate_TextChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Nov 24 2005</date>
		private void dtmToDate_TextChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmToDate_TextChanged()";
			try
			{
				
				if ((dtmFromDate.Value != null)&&(dtmToDate.Value != null)
					&&(dtmFromDate.Value != DBNull.Value)&&(dtmToDate.Value != DBNull.Value))
				{
					dtmDateOnly = new DateTime(((DateTime)dtmFromDate.Value).Year, ((DateTime)dtmFromDate.Value).Month,((DateTime)dtmFromDate.Value).Day) ;
					txtPlanHorizon.Value = ((DateTime)dtmToDate.Value - dtmDateOnly).Days;
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
