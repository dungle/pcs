using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComSale.Order.BO;
using PCSComSale.Order.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSSale.Order
{
	/// <summary>
	/// Summary description for CustomerAndItemReference.
	/// </summary>
	public class CustomerAndItemReference : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnSave;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Button btnCustomerSearch;
		private System.Windows.Forms.Label lblCustomerName;
		private System.Windows.Forms.Label lblCustomer;
		private System.Windows.Forms.TextBox txtCustomerName;
		private System.Windows.Forms.Button btnCustomerNameSearch;
		private System.Windows.Forms.Label lblCCN;
		private C1.Win.C1List.C1Combo cboCCN;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private const string THIS = "PCSSale.Order.CustomerAndItemReference";
		private const string V_VENDORCUSTOMER = "V_VendorCustomer";
		private const string CUSTOMER_FLD = "Customer";
		private bool blnHasError = false;
		private System.Windows.Forms.TextBox txtCustomer;
		private DataTable dtbGridLayOut = new DataTable();
		private CustomerAndItemReferenceBO boCustomerAndItem = new CustomerAndItemReferenceBO();
		private SO_CustomerItemRefMasterVO voCustomMaster = new SO_CustomerItemRefMasterVO();
		private System.Windows.Forms.TextBox txtCurrency;
		private System.Windows.Forms.Label lblCurrency;
		private DataSet dstData = new DataSet();

		public CustomerAndItemReference()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CustomerAndItemReference));
			this.btnClose = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.btnCustomerSearch = new System.Windows.Forms.Button();
			this.lblCCN = new System.Windows.Forms.Label();
			this.btnCustomerNameSearch = new System.Windows.Forms.Button();
			this.txtCustomerName = new System.Windows.Forms.TextBox();
			this.txtCustomer = new System.Windows.Forms.TextBox();
			this.lblCustomerName = new System.Windows.Forms.Label();
			this.lblCustomer = new System.Windows.Forms.Label();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.txtCurrency = new System.Windows.Forms.TextBox();
			this.lblCurrency = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.AccessibleDescription = "";
			this.btnClose.AccessibleName = "";
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(608, 386);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 14;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = "";
			this.btnDelete.AccessibleName = "";
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.Enabled = false;
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(64, 386);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 12;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.AccessibleDescription = "";
			this.btnHelp.AccessibleName = "";
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(547, 386);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 13;
			this.btnHelp.Text = "&Help";
			// 
			// btnSave
			// 
			this.btnSave.AccessibleDescription = "";
			this.btnSave.AccessibleName = "";
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.Enabled = false;
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(3, 386);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 11;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
			this.cboCCN.Location = new System.Drawing.Point(588, 6);
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
			// btnCustomerSearch
			// 
			this.btnCustomerSearch.AccessibleDescription = "";
			this.btnCustomerSearch.AccessibleName = "";
			this.btnCustomerSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCustomerSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCustomerSearch.Location = new System.Drawing.Point(190, 6);
			this.btnCustomerSearch.Name = "btnCustomerSearch";
			this.btnCustomerSearch.Size = new System.Drawing.Size(24, 20);
			this.btnCustomerSearch.TabIndex = 4;
			this.btnCustomerSearch.Text = "...";
			this.btnCustomerSearch.Click += new System.EventHandler(this.btnCustomerSearch_Click);
			// 
			// lblCCN
			// 
			this.lblCCN.AccessibleDescription = "";
			this.lblCCN.AccessibleName = "";
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(550, 8);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(38, 19);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnCustomerNameSearch
			// 
			this.btnCustomerNameSearch.AccessibleDescription = "";
			this.btnCustomerNameSearch.AccessibleName = "";
			this.btnCustomerNameSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCustomerNameSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCustomerNameSearch.Location = new System.Drawing.Point(374, 28);
			this.btnCustomerNameSearch.Name = "btnCustomerNameSearch";
			this.btnCustomerNameSearch.Size = new System.Drawing.Size(24, 20);
			this.btnCustomerNameSearch.TabIndex = 9;
			this.btnCustomerNameSearch.Text = "...";
			this.btnCustomerNameSearch.Click += new System.EventHandler(this.btnCustomerNameSearch_Click);
			// 
			// txtCustomerName
			// 
			this.txtCustomerName.AccessibleDescription = "";
			this.txtCustomerName.AccessibleName = "";
			this.txtCustomerName.Location = new System.Drawing.Point(92, 28);
			this.txtCustomerName.MaxLength = 400;
			this.txtCustomerName.Name = "txtCustomerName";
			this.txtCustomerName.Size = new System.Drawing.Size(281, 20);
			this.txtCustomerName.TabIndex = 8;
			this.txtCustomerName.Text = "";
			this.txtCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerName_KeyDown);
			this.txtCustomerName.Validating += new System.ComponentModel.CancelEventHandler(this.txtCustomerName_Validating);
			// 
			// txtCustomer
			// 
			this.txtCustomer.AccessibleDescription = "";
			this.txtCustomer.AccessibleName = "";
			this.txtCustomer.Location = new System.Drawing.Point(92, 6);
			this.txtCustomer.MaxLength = 40;
			this.txtCustomer.Name = "txtCustomer";
			this.txtCustomer.Size = new System.Drawing.Size(98, 20);
			this.txtCustomer.TabIndex = 3;
			this.txtCustomer.Text = "";
			this.txtCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomer_KeyDown);
			this.txtCustomer.Validating += new System.ComponentModel.CancelEventHandler(this.txtCustomer_Validating);
			// 
			// lblCustomerName
			// 
			this.lblCustomerName.AccessibleDescription = "";
			this.lblCustomerName.AccessibleName = "";
			this.lblCustomerName.ForeColor = System.Drawing.Color.Maroon;
			this.lblCustomerName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCustomerName.Location = new System.Drawing.Point(4, 28);
			this.lblCustomerName.Name = "lblCustomerName";
			this.lblCustomerName.Size = new System.Drawing.Size(88, 20);
			this.lblCustomerName.TabIndex = 7;
			this.lblCustomerName.Text = "Customer Name";
			this.lblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCustomer
			// 
			this.lblCustomer.AccessibleDescription = "";
			this.lblCustomer.AccessibleName = "";
			this.lblCustomer.ForeColor = System.Drawing.Color.Maroon;
			this.lblCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCustomer.Location = new System.Drawing.Point(4, 6);
			this.lblCustomer.Name = "lblCustomer";
			this.lblCustomer.Size = new System.Drawing.Size(75, 20);
			this.lblCustomer.TabIndex = 2;
			this.lblCustomer.Text = "Customer";
			this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.dgrdData.FilterBar = true;
			this.dgrdData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.dgrdData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(3, 53);
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
			this.dgrdData.Size = new System.Drawing.Size(665, 327);
			this.dgrdData.TabIndex = 10;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Part Number" +
				"\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Part Name\" DataField=\"Description\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueIt" +
				"ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Customer Part" +
				" No.\" DataField=\"CustomerItemCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"Customer Code\" DataField=\"CustomerItemModel\"><Valu" +
				"eItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Unit Price" +
				"\" DataField=\"UnitPrice\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"Selling UM\" DataField=\"MST_UnitOfMeasureCode\"><ValueItems /><" +
				"GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Category\" DataField=" +
				"\"ITM_CategoryCode\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles " +
				"type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Style58{AlignHorz:Near;}S" +
				"tyle59{AlignHorz:Near;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;" +
				"}Style50{}Style51{}Style52{AlignHorz:Near;ForeColor:Maroon;}Style53{AlignHorz:Ne" +
				"ar;}Style54{}Caption{AlignHorz:Center;}Style56{}Normal{Font:Microsoft Sans Serif" +
				", 8.25pt;}Style25{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{" +
				"}Style31{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;ForeColor:M" +
				"aroon;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13" +
				"{}Style44{}Style46{AlignHorz:Near;ForeColor:Maroon;}Style63{}Style62{}Style61{}S" +
				"tyle60{}Style38{}Style37{}Style34{AlignHorz:Near;ForeColor:Maroon;}Style35{Align" +
				"Horz:Near;}Style32{}OddRow{}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}Styl" +
				"e27{}Style26{}RecordSelector{AlignImage:Center;}Footer{}Style23{AlignHorz:Near;}" +
				"Style22{AlignHorz:Near;ForeColor:Maroon;}Style21{}Style55{}Group{AlignVert:Cente" +
				"r;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style57{}Inactive{ForeColor:Ina" +
				"ctiveCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap" +
				":True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVer" +
				"t:Center;}Style49{}Style48{}Style24{}Style1{}Style20{}Style41{AlignHorz:Near;}St" +
				"yle40{AlignHorz:Near;ForeColor:Maroon;}Style43{}FilterBar{}Style42{}Style45{}Sty" +
				"le47{AlignHorz:Near;}Style9{}Style8{}Style39{}Style36{}Style5{}Style4{}Style7{}S" +
				"tyle6{}Style33{}Style30{}Style3{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueD" +
				"BGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooter" +
				"Height=\"17\" FilterBar=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth" +
				"=\"17\" DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><Cli" +
				"entRect>0, 0, 661, 323</ClientRect><BorderSide>0</BorderSide><CaptionStyle paren" +
				"t=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowSty" +
				"le parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13" +
				"\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"St" +
				"yle12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=" +
				"\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><Odd" +
				"RowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelect" +
				"or\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=" +
				"\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Styl" +
				"e2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"St" +
				"yle3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderSty" +
				"le parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\"" +
				" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>1" +
				"49</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn>" +
				"<HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\"" +
				" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"" +
				"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle pa" +
				"rent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Sing" +
				"le</ColumnDivider><Width>175</Width><Height>15</Height><DCIdx>1</DCIdx></C1Displ" +
				"ayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style pa" +
				"rent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><Editor" +
				"Style parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style" +
				"33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><C" +
				"olumnDivider>DarkGray,Single</ColumnDivider><Width>63</Width><Height>15</Height>" +
				"<DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\"" +
				" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style" +
				"3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle " +
				"parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" />" +
				"<Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15<" +
				"/Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
				"\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle paren" +
				"t=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHead" +
				"erStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Sty" +
				"le56\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wi" +
				"dth>65</Width><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayCol" +
				"umn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Styl" +
				"e47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" " +
				"me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyl" +
				"e parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray," +
				"Single</ColumnDivider><Width>81</Width><Height>15</Height><DCIdx>5</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style" +
				" parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><Edi" +
				"torStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
				"yle39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible" +
				"><ColumnDivider>DarkGray,Single</ColumnDivider><Width>113</Width><Height>15</Hei" +
				"ght><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
				"le2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"S" +
				"tyle3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderSt" +
				"yle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44" +
				"\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>" +
				"116</Width><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn></internalCols><" +
				"/C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal" +
				"\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" />" +
				"<Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><" +
				"Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Styl" +
				"e parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Sty" +
				"le parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><" +
				"Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></Na" +
				"medStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</" +
				"Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 661, 323</Cl" +
				"ientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle pa" +
				"rent=\"\" me=\"Style15\" /></Blob>";
			// 
			// txtCurrency
			// 
			this.txtCurrency.AccessibleDescription = "";
			this.txtCurrency.AccessibleName = "";
			this.txtCurrency.Location = new System.Drawing.Point(298, 6);
			this.txtCurrency.MaxLength = 40;
			this.txtCurrency.Name = "txtCurrency";
			this.txtCurrency.ReadOnly = true;
			this.txtCurrency.Size = new System.Drawing.Size(98, 20);
			this.txtCurrency.TabIndex = 6;
			this.txtCurrency.Text = "";
			// 
			// lblCurrency
			// 
			this.lblCurrency.AccessibleDescription = "";
			this.lblCurrency.AccessibleName = "";
			this.lblCurrency.ForeColor = System.Drawing.Color.Black;
			this.lblCurrency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCurrency.Location = new System.Drawing.Point(224, 6);
			this.lblCurrency.Name = "lblCurrency";
			this.lblCurrency.Size = new System.Drawing.Size(56, 20);
			this.lblCurrency.TabIndex = 5;
			this.lblCurrency.Text = "Currency";
			this.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CustomerAndItemReference
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(672, 413);
			this.Controls.Add(this.txtCurrency);
			this.Controls.Add(this.lblCurrency);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.txtCustomerName);
			this.Controls.Add(this.txtCustomer);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.btnCustomerSearch);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnCustomerNameSearch);
			this.Controls.Add(this.lblCustomerName);
			this.Controls.Add(this.lblCustomer);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Name = "CustomerAndItemReference";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Customer And Item Reference";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CustomerAndItemReference_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.CustomerAndItemReference_Closing);
			this.Load += new System.EventHandler(this.CustomerAndItemReference_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		private void CustomerAndItemReference_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".CustomerAndItemReference_Load()";
			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName);

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
				BindDataToGrid(0, 0);
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
		/// 
		/// </summary>
		/// <param name="pintPartyID"></param>
		/// <param name="pintCCNID"></param>
		private void BindDataToGrid(int pintPartyID, int pintCCNID)
		{
            dstData = boCustomerAndItem.ListDetailByMasterID(pintPartyID, pintCCNID);
            dgrdData.DataSource = dstData.Tables[0];

            FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
            dgrdData.Columns[SO_CustomerItemRefDetailTable.UNITPRICE_FLD].NumberFormat = "##############,0.0000";
            dgrdData.Columns[SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].DataWidth = 50;
            dgrdData.Columns[SO_CustomerItemRefDetailTable.UNITPRICE_FLD].DataWidth = 10;
            dgrdData.Columns[SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD].DataWidth = 20;
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
            dgrdData.Splits[0].DisplayColumns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Button = true;

            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Locked = true;
            
            voCustomMaster.CustomerItemRefMasterID = ((SO_CustomerItemRefMasterVO)boCustomerAndItem.GetObjectMasterByID(pintPartyID, pintCCNID)).CustomerItemRefMasterID;
            if (pintPartyID != 0)
            {
                dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[SO_CustomerItemRefDetailTable.UNITPRICE_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Locked = false;

                btnSave.Enabled = true;
                btnDelete.Enabled = true;
            }
		}
		
		/// <summary>
		/// Select Customer by Code
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCustomerSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCustomerSearch_Click()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				htbCriteria = new Hashtable();
				htbCriteria.Add(CUSTOMER_FLD, (int)PartyTypeEnum.CUSTOMER);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtCustomer.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtCustomer.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtCustomer.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					BindDataToGrid(int.Parse(txtCustomer.Tag.ToString()), int.Parse(cboCCN.SelectedValue.ToString()));
					voCustomMaster.PartyID = int.Parse(txtCustomer.Tag.ToString());
					voCustomMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
					txtCurrency.Text = drwResult["MST_PartyCurrency"].ToString();
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
		/// Select Customer by Name
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCustomerNameSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCustomerNameSearch_Click()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				htbCriteria = new Hashtable();
				htbCriteria.Add(CUSTOMER_FLD, (int)PartyTypeEnum.CUSTOMER);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtCustomerName.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtCustomer.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtCustomer.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					BindDataToGrid(int.Parse(txtCustomer.Tag.ToString()), int.Parse(cboCCN.SelectedValue.ToString()));
					voCustomMaster.PartyID = int.Parse(txtCustomer.Tag.ToString());
					voCustomMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
					txtCurrency.Text = drwResult["MST_PartyCurrency"].ToString();
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

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		/// <summary>
		/// Validating event: - OpenSeachForm
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtCustomer_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCustomer_Validating()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if(txtCustomer.Text.Trim() == string.Empty)
				{
					txtCustomer.Tag = null;
					txtCustomerName.Text = string.Empty;
					btnSave.Enabled = false;
					btnDelete.Enabled = false;
					BindDataToGrid(0, 0);
					return;
				}
				htbCriteria = new Hashtable();
				htbCriteria.Add(CUSTOMER_FLD, 0);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtCustomer.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtCustomer.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtCustomer.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					voCustomMaster.PartyID = int.Parse(txtCustomer.Tag.ToString());
					voCustomMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
					txtCurrency.Text = drwResult["MST_PartyCurrency"].ToString();
					BindDataToGrid(int.Parse(txtCustomer.Tag.ToString()), int.Parse(cboCCN.SelectedValue.ToString()));
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
		/// Validating data on Customer Name
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtCustomerName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCustomer_Validating()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if(txtCustomerName.Text.Trim() == string.Empty)
				{
					txtCustomer.Tag = null;
					txtCustomer.Text= string.Empty;
					txtCustomerName.Text = string.Empty;
					btnSave.Enabled = false;
					btnDelete.Enabled = false;
					BindDataToGrid(0, 0);
					return;
				}
				htbCriteria = new Hashtable();
				htbCriteria.Add(CUSTOMER_FLD, 0);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtCustomerName.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtCustomer.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtCustomer.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					voCustomMaster.PartyID = int.Parse(txtCustomer.Tag.ToString());
					voCustomMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
					txtCurrency.Text = drwResult["MST_PartyCurrency"].ToString();
					BindDataToGrid(int.Parse(txtCustomer.Tag.ToString()), int.Parse(cboCCN.SelectedValue.ToString()));
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
		/// Button click to open search form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				if (!btnSave.Enabled)
				{
				    return;
				}

                DataRowView drwResult = null;
                Hashtable htbCondition = new Hashtable();
			    if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]))
				{
					//open the search form to select Product
					if (cboCCN.SelectedValue != null)
					{
						htbCondition.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
					}
					else
					{
						htbCondition.Add(ITM_ProductTable.CCNID_FLD, 0);
					}
				    drwResult = dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent
				                    ? FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD,
				                                                           dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD].
				                                                               ToString(), htbCondition, true)
				                    : FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD,
				                                                           dgrdData.Columns[ITM_ProductTable.CODE_FLD].Text.Trim(),
				                                                           htbCondition, true);
				    if (drwResult != null)
				    {
				        dgrdData.EditActive = true;
						FillItemData(drwResult.Row);
					}
				}

				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD]))
				{
					//open the search form to select Product
					if (cboCCN.SelectedValue != null)
					{
						htbCondition.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
					}
					else
					{
						htbCondition.Add(ITM_ProductTable.CCNID_FLD, 0);
					}
				    drwResult = dgrdData.AddNewMode != AddNewModeEnum.NoAddNew
				                    ? FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,
				                                                           ITM_ProductTable.DESCRIPTION_FLD,
				                                                           dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD].
				                                                               ToString(), htbCondition, true)
				                    : FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,
				                                                           ITM_ProductTable.DESCRIPTION_FLD,
				                                                           dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Text.
				                                                               Trim(), htbCondition, true);
				    if (drwResult != null)
					{
                        dgrdData.EditActive = true;
						FillItemData(drwResult.Row);
					}
				}

				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD]))
				{
					//open the search form to select Unit Of Measure
				    drwResult = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME, MST_UnitOfMeasureTable.CODE_FLD,
				                                                     dgrdData.AddNewMode != AddNewModeEnum.NoAddNew
				                                                         ? dgrdData[dgrdData.Row,MST_UnitOfMeasureTable.TABLE_NAME +MST_UnitOfMeasureTable.CODE_FLD].ToString()
				                                                         : dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME +MST_UnitOfMeasureTable.CODE_FLD].Text.Trim(),
				                                                     htbCondition, true);
				    if (drwResult != null)
					{
						dgrdData[dgrdData.Row, SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD] = drwResult[SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD];
						dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drwResult[MST_UnitOfMeasureTable.CODE_FLD];	
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
		/// - Fill data into item code, item description, item revision, stock unit of measure 
		/// - Fill data into start/due date time based on current server date time
		/// - Fill data into AGC, cost method, estimated cost based on product item setup
		/// </summary>
		private void FillItemData(DataRow pdrowData)
		{
			int i = dgrdData.Row;
            dgrdData[i, ITM_ProductTable.CODE_FLD] = pdrowData[ITM_ProductTable.CODE_FLD].ToString();
            dgrdData[i, ITM_ProductTable.DESCRIPTION_FLD] = pdrowData[ITM_ProductTable.DESCRIPTION_FLD].ToString();
            dgrdData[i, ITM_ProductTable.REVISION_FLD] = pdrowData[ITM_ProductTable.REVISION_FLD].ToString();
            dgrdData[i, SO_CustomerItemRefDetailTable.PRODUCTID_FLD] = pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString();
            dgrdData[i, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = pdrowData[ITM_ProductTable.CATEGORYID_FLD
                + "_" + ITM_CategoryTable.CODE_FLD].ToString();
            dgrdData[i, SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD] = pdrowData[ITM_ProductTable.SELLINGUMID_FLD].ToString();
            DataRowView drwViewUM = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME, MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD,
                pdrowData[ITM_ProductTable.SELLINGUMID_FLD].ToString(), null, false);
            if (drwViewUM != null)
            {
                dgrdData[i, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drwViewUM[MST_UnitOfMeasureTable.CODE_FLD];
            }
            dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
            dgrdData.Focus();
		}

		private void CustomerAndItemReference_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F12)
			{
				dgrdData.Row = dgrdData.RowCount;
				dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]);
				dgrdData.Focus();
			}
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}
		
		/// <summary>
		/// Open search form & check data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				if (e.Column.DataColumn.Value.ToString() == string.Empty)
				{
				    return;
				}
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				switch (e.Column.DataColumn.DataField)
				{
					case ITM_ProductTable.CODE_FLD:
						# region open Product search form 
						if (cboCCN.SelectedIndex >= 0)
						{
							htbCriteria.Add(MST_CCNTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
						}
						else
						{
							htbCriteria.Add(MST_CCNTable.CCNID_FLD, 0);
						}
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult.Row;
						}
						else
						{
							e.Cancel = true;
						}
						#endregion
						break;
					case ITM_ProductTable.DESCRIPTION_FLD:
						# region open Product search form 
						if (cboCCN.SelectedIndex >= 0)
						{
							htbCriteria.Add(MST_CCNTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
						}
						else
						{
							htbCriteria.Add(MST_CCNTable.CCNID_FLD, 0);
						}
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult.Row;
						}
						else
						{
							e.Cancel = true;
						}
						#endregion
						break;
					case MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD:
						#region open Unit form
						drwResult = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME, MST_UnitOfMeasureTable.CODE_FLD, dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult.Row;
						}
						else
						{
						    e.Cancel = true;
						}
						#endregion
						break;
				}

				//check lead time offset
				if (e.Column.DataColumn.DataField == SO_CustomerItemRefDetailTable.UNITPRICE_FLD)
				{
					try
					{
						if (e.Column.DataColumn.Value.ToString() != string.Empty)
						{
							decimal intUnitPrice = decimal.Parse(e.Column.DataColumn.Value.ToString());
							if (intUnitPrice <= 0)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_INPUT_NEGATIVE_NUMBER, MessageBoxIcon.Error);
								e.Cancel = true;
							}
						}
					}
					catch (Exception ex)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_INPUT_NEGATIVE_NUMBER, MessageBoxIcon.Error);
						e.Cancel = true;
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
		/// Commit data into grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				if (e.Column.DataColumn.DataField == ITM_ProductTable.CODE_FLD || e.Column.DataColumn.DataField == ITM_ProductTable.DESCRIPTION_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(e.Column.DataColumn.Text.Trim() == string.Empty))
					{
						dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = DBNull.Value;
						dgrdData[dgrdData.Row, SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD] = DBNull.Value;
						dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = DBNull.Value;
					}
					else
					{
						FillItemData((DataRow) e.Column.DataColumn.Tag);
						return;
					}
				}
				if (e.Column.DataColumn.DataField == MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(e.Column.DataColumn.Text.Trim() == string.Empty))
					{
						dgrdData[dgrdData.Row, SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD] = DBNull.Value;
						dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = DBNull.Value;
					}
					else
					{
						dgrdData[dgrdData.Row, SO_CustomerItemRefDetailTable.UNITOFMEASUREID_FLD] = ((DataRow) e.Column.DataColumn.Tag)[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD];
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
		/// Keydown event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F4:
					if (btnSave.Enabled)
					{
						dgrdData_ButtonClick(sender, null);
					}
					break;
				case Keys.Delete:
					FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
					break;
			}
		}

		/// <summary>
		/// Validate all data, and some business rules
		/// </summary>
		/// <returns></returns>
		private bool ValidateData()
		{
            if (txtCustomer.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                txtCustomer.Focus();
                return false;
            }
            //check madatory field in grid
            for (int i = 0; i < dgrdData.RowCount; i++)
            {
                if (dgrdData[i, ITM_ProductTable.CODE_FLD].ToString().Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
                    dgrdData.Row = i;
                    dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
                    dgrdData.Focus();
                    return false;
                }
                if (dgrdData[i, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].ToString().Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
                    dgrdData.Row = i;
                    dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD]);
                    dgrdData.Focus();
                    return false;
                }
                if (dgrdData[i, SO_CustomerItemRefDetailTable.UNITPRICE_FLD].ToString().Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
                    dgrdData.Row = i;
                    dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[SO_CustomerItemRefDetailTable.UNITPRICE_FLD]);
                    dgrdData.Focus();
                    return false;
                }
                if (dgrdData[i, SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].ToString() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
                    dgrdData.Row = i;
                    dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD]);
                    dgrdData.Focus();
                    return false;
                }
            }

            for (int i = 0; i < dgrdData.RowCount; i++)
            {
                for (int j = i + 1; j < dgrdData.RowCount; j++)
                {
                    if (dgrdData[i, SO_CustomerItemRefDetailTable.PRODUCTID_FLD].ToString() == dgrdData[j, SO_CustomerItemRefDetailTable.PRODUCTID_FLD].ToString())
                    {
                        PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Error);
                        dgrdData.Row = i;
                        dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
                        dgrdData.Focus();
                        return false;
                    }

                    if (dgrdData[i, SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].ToString() == dgrdData[j, SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD].ToString()
                        && dgrdData[i, SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD].ToString() == dgrdData[j, SO_CustomerItemRefDetailTable.CUSTOMERITEMMODEL_FLD].ToString())
                    {
                        PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Error);
                        dgrdData.Row = i;
                        dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[SO_CustomerItemRefDetailTable.CUSTOMERITEMCODE_FLD]);
                        dgrdData.Focus();
                        return false;
                    }
                }
            }
			return true;
		}
		
		/// <summary>
		/// Save event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnHasError = true;
				if (!dgrdData.EditActive && ValidateData() && btnSave.Enabled)
				{
					boCustomerAndItem.UpdateMasterAndDetail(voCustomMaster, dstData);
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);

					//Reload
					BindDataToGrid(int.Parse(txtCustomer.Tag.ToString()), int.Parse(cboCCN.SelectedValue.ToString()));
					blnHasError = false;
					txtCustomer.Focus();
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

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".AutoFillItemReference()";
			try
			{
				if (btnDelete.Enabled && !dgrdData.EditActive && PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					boCustomerAndItem.DeleteMasterAndDetail(voCustomMaster);

					txtCustomer.Text = string.Empty;
					txtCustomer.Tag = null;
					txtCustomerName.Text = string.Empty;
					BindDataToGrid(0, 0);
					btnSave.Enabled = false;
					btnDelete.Enabled = false;
					txtCustomer.Focus();
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

		private void txtCustomer_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				btnCustomerSearch_Click(sender, new EventArgs());
			}
		}

		private void txtCustomerName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				btnCustomerNameSearch_Click(sender, new EventArgs());
			}
		}

		private void CustomerAndItemReference_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".AutoFillItemReference()";
			try
			{
				if (dstData != null && dstData.GetChanges() != null)
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

		private void dgrdData_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			if (txtCustomer.Text.Trim() == string.Empty)
			{
				e.Cancel = true;
			}
		}
	}
}
