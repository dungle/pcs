using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using PCSComProcurement.Purchase.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for DeliveryApproval.
	/// </summary>
	public class DeliveryApproval : Form
	{
		private TextBox txtPONo;
		private Button btnPONo;
		private Label lblPONo;
		private Label lblToStartDate;
		private Label lblFromStartDate;
		private Label lblCategory;
		private TextBox txtCategory;
		private Button btnCategory;
		private TextBox txtPartName;
		private TextBox txtModel;
		private TextBox txtPartNumber;
		private Button btnPartName;
		private Label lblPartName;
		private Button btnPartNumber;
		private Label lblPartNumber;
		private Label lblModel;
		private C1DateEdit dtmToDate;
		private C1DateEdit dtmFromDate;
		private Button btnShowDetail;
		private Button btnSearch;
		private Button btnHelp;
		private Button btnApprove;
		private Button btnClose;
		private CheckBox chkSelectAll;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		const string THIS = "PCSProcurement.Purchase.DeliveryApproval";
		private C1TrueDBGrid dgrdData;
		private DataTable dtbGridLayOut = new DataTable();
		private DataSet dstData = new DataSet();
		private bool blnStateOfCheck = false;

		public DeliveryApproval()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DeliveryApproval));
			this.txtPONo = new System.Windows.Forms.TextBox();
			this.btnPONo = new System.Windows.Forms.Button();
			this.lblPONo = new System.Windows.Forms.Label();
			this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
			this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
			this.lblToStartDate = new System.Windows.Forms.Label();
			this.lblFromStartDate = new System.Windows.Forms.Label();
			this.lblCategory = new System.Windows.Forms.Label();
			this.txtCategory = new System.Windows.Forms.TextBox();
			this.btnCategory = new System.Windows.Forms.Button();
			this.txtPartName = new System.Windows.Forms.TextBox();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.txtPartNumber = new System.Windows.Forms.TextBox();
			this.btnPartName = new System.Windows.Forms.Button();
			this.lblPartName = new System.Windows.Forms.Label();
			this.btnPartNumber = new System.Windows.Forms.Button();
			this.lblPartNumber = new System.Windows.Forms.Label();
			this.lblModel = new System.Windows.Forms.Label();
			this.btnShowDetail = new System.Windows.Forms.Button();
			this.btnSearch = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnApprove = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// txtPONo
			// 
			this.txtPONo.Location = new System.Drawing.Point(90, 34);
			this.txtPONo.Name = "txtPONo";
			this.txtPONo.Size = new System.Drawing.Size(122, 20);
			this.txtPONo.TabIndex = 5;
			this.txtPONo.Text = "";
			this.txtPONo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPONo_KeyDown);
			this.txtPONo.Validating += new System.ComponentModel.CancelEventHandler(this.txtPONo_Validating);
			// 
			// btnPONo
			// 
			this.btnPONo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPONo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPONo.Location = new System.Drawing.Point(214, 34);
			this.btnPONo.Name = "btnPONo";
			this.btnPONo.Size = new System.Drawing.Size(24, 20);
			this.btnPONo.TabIndex = 6;
			this.btnPONo.Text = "...";
			this.btnPONo.Click += new System.EventHandler(this.btnPONo_Click);
			// 
			// lblPONo
			// 
			this.lblPONo.ForeColor = System.Drawing.Color.Black;
			this.lblPONo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPONo.Location = new System.Drawing.Point(6, 34);
			this.lblPONo.Name = "lblPONo";
			this.lblPONo.Size = new System.Drawing.Size(82, 20);
			this.lblPONo.TabIndex = 4;
			this.lblPONo.Text = "PO No.";
			this.lblPONo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dtmToDate
			// 
			// 
			// dtmToDate.Calendar
			// 
			this.dtmToDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmToDate.CustomFormat = "dd-MM-yyyy";
			this.dtmToDate.EmptyAsNull = true;
			this.dtmToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmToDate.Location = new System.Drawing.Point(288, 12);
			this.dtmToDate.Name = "dtmToDate";
			this.dtmToDate.Size = new System.Drawing.Size(122, 20);
			this.dtmToDate.TabIndex = 3;
			this.dtmToDate.Tag = null;
			this.dtmToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			// 
			// dtmFromDate
			// 
			// 
			// dtmFromDate.Calendar
			// 
			this.dtmFromDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmFromDate.CustomFormat = "dd-MM-yyyy";
			this.dtmFromDate.EmptyAsNull = true;
			this.dtmFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmFromDate.Location = new System.Drawing.Point(90, 12);
			this.dtmFromDate.Name = "dtmFromDate";
			this.dtmFromDate.Size = new System.Drawing.Size(122, 20);
			this.dtmFromDate.TabIndex = 1;
			this.dtmFromDate.Tag = null;
			this.dtmFromDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			// 
			// lblToStartDate
			// 
			this.lblToStartDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblToStartDate.Location = new System.Drawing.Point(240, 12);
			this.lblToStartDate.Name = "lblToStartDate";
			this.lblToStartDate.Size = new System.Drawing.Size(82, 20);
			this.lblToStartDate.TabIndex = 2;
			this.lblToStartDate.Text = "To Date";
			this.lblToStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblFromStartDate
			// 
			this.lblFromStartDate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFromStartDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblFromStartDate.Location = new System.Drawing.Point(6, 12);
			this.lblFromStartDate.Name = "lblFromStartDate";
			this.lblFromStartDate.Size = new System.Drawing.Size(82, 20);
			this.lblFromStartDate.TabIndex = 0;
			this.lblFromStartDate.Text = "From Date";
			this.lblFromStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCategory
			// 
			this.lblCategory.AccessibleDescription = "";
			this.lblCategory.AccessibleName = "";
			this.lblCategory.ForeColor = System.Drawing.Color.Black;
			this.lblCategory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCategory.Location = new System.Drawing.Point(6, 56);
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.Size = new System.Drawing.Size(82, 20);
			this.lblCategory.TabIndex = 7;
			this.lblCategory.Text = "Category";
			this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtCategory
			// 
			this.txtCategory.Location = new System.Drawing.Point(90, 56);
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.Size = new System.Drawing.Size(122, 20);
			this.txtCategory.TabIndex = 8;
			this.txtCategory.Text = "";
			this.txtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategory_KeyDown);
			this.txtCategory.Validating += new System.ComponentModel.CancelEventHandler(this.txtCategory_Validating);
			// 
			// btnCategory
			// 
			this.btnCategory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCategory.Location = new System.Drawing.Point(214, 56);
			this.btnCategory.Name = "btnCategory";
			this.btnCategory.Size = new System.Drawing.Size(24, 20);
			this.btnCategory.TabIndex = 9;
			this.btnCategory.Text = "...";
			this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
			// 
			// txtPartName
			// 
			this.txtPartName.Location = new System.Drawing.Point(90, 100);
			this.txtPartName.Name = "txtPartName";
			this.txtPartName.Size = new System.Drawing.Size(332, 20);
			this.txtPartName.TabIndex = 16;
			this.txtPartName.Text = "";
			this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
			this.txtPartName.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartName_Validating);
			// 
			// txtModel
			// 
			this.txtModel.Location = new System.Drawing.Point(288, 78);
			this.txtModel.Name = "txtModel";
			this.txtModel.ReadOnly = true;
			this.txtModel.Size = new System.Drawing.Size(134, 20);
			this.txtModel.TabIndex = 14;
			this.txtModel.Text = "";
			// 
			// txtPartNumber
			// 
			this.txtPartNumber.Location = new System.Drawing.Point(90, 78);
			this.txtPartNumber.Name = "txtPartNumber";
			this.txtPartNumber.Size = new System.Drawing.Size(122, 20);
			this.txtPartNumber.TabIndex = 11;
			this.txtPartNumber.Text = "";
			this.txtPartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNumber_KeyDown);
			this.txtPartNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartNumber_Validating);
			// 
			// btnPartName
			// 
			this.btnPartName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPartName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPartName.Location = new System.Drawing.Point(424, 100);
			this.btnPartName.Name = "btnPartName";
			this.btnPartName.Size = new System.Drawing.Size(24, 20);
			this.btnPartName.TabIndex = 17;
			this.btnPartName.Text = "...";
			this.btnPartName.Click += new System.EventHandler(this.btnPartName_Click);
			// 
			// lblPartName
			// 
			this.lblPartName.ForeColor = System.Drawing.Color.Black;
			this.lblPartName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPartName.Location = new System.Drawing.Point(6, 100);
			this.lblPartName.Name = "lblPartName";
			this.lblPartName.Size = new System.Drawing.Size(82, 20);
			this.lblPartName.TabIndex = 15;
			this.lblPartName.Text = "Part Name";
			this.lblPartName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnPartNumber
			// 
			this.btnPartNumber.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPartNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPartNumber.Location = new System.Drawing.Point(214, 78);
			this.btnPartNumber.Name = "btnPartNumber";
			this.btnPartNumber.Size = new System.Drawing.Size(24, 20);
			this.btnPartNumber.TabIndex = 12;
			this.btnPartNumber.Text = "...";
			this.btnPartNumber.Click += new System.EventHandler(this.btnPartNumber_Click);
			// 
			// lblPartNumber
			// 
			this.lblPartNumber.ForeColor = System.Drawing.Color.Black;
			this.lblPartNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPartNumber.Location = new System.Drawing.Point(6, 78);
			this.lblPartNumber.Name = "lblPartNumber";
			this.lblPartNumber.Size = new System.Drawing.Size(82, 20);
			this.lblPartNumber.TabIndex = 10;
			this.lblPartNumber.Text = "Part Number";
			this.lblPartNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblModel
			// 
			this.lblModel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblModel.Location = new System.Drawing.Point(240, 78);
			this.lblModel.Name = "lblModel";
			this.lblModel.Size = new System.Drawing.Size(82, 20);
			this.lblModel.TabIndex = 13;
			this.lblModel.Text = "Model";
			this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnShowDetail
			// 
			this.btnShowDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnShowDetail.Location = new System.Drawing.Point(453, 442);
			this.btnShowDetail.Name = "btnShowDetail";
			this.btnShowDetail.Size = new System.Drawing.Size(84, 22);
			this.btnShowDetail.TabIndex = 22;
			this.btnShowDetail.Text = "Show D&etail";
			this.btnShowDetail.Click += new System.EventHandler(this.btnShowDetail_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearch.Location = new System.Drawing.Point(586, 100);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(80, 22);
			this.btnSearch.TabIndex = 18;
			this.btnSearch.Text = "Sea&rch";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(538, 442);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(64, 22);
			this.btnHelp.TabIndex = 23;
			this.btnHelp.Text = "&Help";
			// 
			// btnApprove
			// 
			this.btnApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnApprove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnApprove.Location = new System.Drawing.Point(6, 442);
			this.btnApprove.Name = "btnApprove";
			this.btnApprove.Size = new System.Drawing.Size(76, 22);
			this.btnApprove.TabIndex = 21;
			this.btnApprove.Text = "&Save";
			this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(603, 442);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(64, 22);
			this.btnClose.TabIndex = 24;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(6, 124);
			this.dgrdData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdData.Name = "dgrdData";
			this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.dgrdData.PreviewInfo.ZoomFactor = 75;
			this.dgrdData.PrintInfo.ShowOptionsDialog = false;
			this.dgrdData.RecordSelectorWidth = 17;
			this.dgrdData.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.dgrdData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.dgrdData.RowHeight = 14;
			this.dgrdData.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.dgrdData.Size = new System.Drawing.Size(660, 314);
			this.dgrdData.TabIndex = 19;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"PO No.\" Dat" +
				"aField=\"PONo\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"Cancel\" DataField=\"CancelDelivery\"><ValueItems Presentation=\"CheckBox\" " +
				"/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"PO Line\" DataFiel" +
				"d=\"Line\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capti" +
				"on=\"Category\" DataField=\"ITM_CategoryCode\"><ValueItems /><GroupInfo /></C1DataCo" +
				"lumn><C1DataColumn Level=\"0\" Caption=\"Part Number\" DataField=\"Code\"><ValueItems " +
				"/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Part Name\" DataFi" +
				"eld=\"Description\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueItems /><GroupInfo /></C1DataColu" +
				"mn><C1DataColumn Level=\"0\" Caption=\"Buying UM\" DataField=\"BuyingUM\"><ValueItems " +
				"/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Order Quantity \" " +
				"DataField=\"OrderQuantity\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColum" +
				"n Level=\"0\" Caption=\"Del. Line\" DataField=\"DeliveryLine\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Schedule Date\" DataField=\"Sc" +
				"heduleDate\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Delivery Quantity\" DataField=\"DeliveryQuantity\"><ValueItems /><GroupInfo " +
				"/></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrap" +
				"per\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Inactive{Fo" +
				"reColor:InactiveCaptionText;BackColor:InactiveCaption;}Style78{}Style79{}Style85" +
				"{}Editor{}Style72{}Style73{}Style70{AlignHorz:Center;}Style71{AlignHorz:Near;}St" +
				"yle76{AlignHorz:Center;}Style77{AlignHorz:Near;}Style74{}Style75{}Style84{}Style" +
				"87{}Style86{}Style81{}Style80{}Style83{AlignHorz:Far;}Style82{AlignHorz:Center;}" +
				"FilterBar{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColo" +
				"r:ControlText;BackColor:Control;}Style18{}Style19{}Style14{}Style15{}Style16{Ali" +
				"gnHorz:Center;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{" +
				"}Style13{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style29{AlignHor" +
				"z:Center;}Style28{AlignHorz:Center;}Style27{}Style25{}Style22{AlignHorz:Center;}" +
				"Style9{}Style8{}Style24{}Style26{}Style5{}Style4{}Style7{}Style6{}Style1{}Style2" +
				"3{AlignHorz:Far;}Style3{}Style2{}Style21{}Style20{}OddRow{}Style38{}Style39{}Sty" +
				"le36{}Style37{}Style34{AlignHorz:Center;}Style35{AlignHorz:Center;}Style32{}Styl" +
				"e33{}Style30{}Style49{}Style48{}Style31{}Normal{}Style41{AlignHorz:Near;}Style40" +
				"{AlignHorz:Center;}Style43{}Style42{}Style45{}Style44{}Style47{AlignHorz:Far;}St" +
				"yle46{AlignHorz:Center;}EvenRow{BackColor:Aqua;}Style59{AlignHorz:Near;}Style58{" +
				"AlignHorz:Center;}RecordSelector{AlignImage:Center;}Style51{}Style50{}Footer{}St" +
				"yle52{AlignHorz:Center;}Style53{AlignHorz:Far;}Style54{}Style55{}Style56{}Style5" +
				"7{}Caption{AlignHorz:Center;}Style69{}Style68{}Style63{}Style62{}Style61{}Style6" +
				"0{}Style67{}Style66{}Style65{AlignHorz:Near;}Style64{AlignHorz:Center;}Group{Bac" +
				"kColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}</Data></Styles><Sp" +
				"lits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeig" +
				"ht=\"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWi" +
				"dth=\"17\" DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><" +
				"ClientRect>0, 0, 656, 310</ClientRect><BorderSide>0</BorderSide><CaptionStyle pa" +
				"rent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRow" +
				"Style parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Styl" +
				"e13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=" +
				"\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle pare" +
				"nt=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><" +
				"OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSel" +
				"ector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style pare" +
				"nt=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"S" +
				"tyle2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=" +
				"\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeader" +
				"Style parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
				"20\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Widt" +
				"h>121</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColu" +
				"mn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style" +
				"47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" m" +
				"e=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,S" +
				"ingle</ColumnDivider><Width>46</Width><Height>15</Height><DCIdx>2</DCIdx></C1Dis" +
				"playColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style " +
				"parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><Edit" +
				"orStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Sty" +
				"le27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible>" +
				"<ColumnDivider>DarkGray,Single</ColumnDivider><Width>55</Width><Height>14</Heigh" +
				"t><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
				"2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Sty" +
				"le3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyl" +
				"e parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" " +
				"/><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>75" +
				"</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /" +
				"><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"St" +
				"yle61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle pare" +
				"nt=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single" +
				"</ColumnDivider><Width>145</Width><Height>15</Height><DCIdx>4</DCIdx></C1Display" +
				"Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style pare" +
				"nt=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorSt" +
				"yle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69" +
				"\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><Col" +
				"umnDivider>DarkGray,Single</ColumnDivider><Width>150</Width><Height>15</Height><" +
				"DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" " +
				"me=\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3" +
				"\" me=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle p" +
				"arent=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><" +
				"Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>79</W" +
				"idth><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><Head" +
				"ingStyle parent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><F" +
				"ooterStyle parent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style" +
				"79\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=" +
				"\"Style1\" me=\"Style80\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</C" +
				"olumnDivider><Width>61</Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColu" +
				"mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"" +
				"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle " +
				"parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnD" +
				"ivider>DarkGray,Single</ColumnDivider><Height>14</Height><DCIdx>10</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style82\" /><Style" +
				" parent=\"Style1\" me=\"Style83\" /><FooterStyle parent=\"Style3\" me=\"Style84\" /><Edi" +
				"torStyle parent=\"Style5\" me=\"Style85\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
				"yle87\" /><GroupFooterStyle parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible" +
				"><ColumnDivider>DarkGray,Single</ColumnDivider><Width>86</Width><Height>15</Heig" +
				"ht><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Styl" +
				"e2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"St" +
				"yle3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderSty" +
				"le parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\"" +
				" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>" +
				"14</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=" +
				"\"Style38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider" +
				"><Width>57</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn></interna" +
				"lCols></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=" +
				"\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Foo" +
				"ter\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inacti" +
				"ve\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" " +
				"/><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\"" +
				" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelect" +
				"or\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\"" +
				" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Mod" +
				"ified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 656, " +
				"310</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterS" +
				"tyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkSelectAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkSelectAll.Location = new System.Drawing.Point(84, 442);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(70, 22);
			this.chkSelectAll.TabIndex = 20;
			this.chkSelectAll.Text = "Se&lect All";
			this.chkSelectAll.Enter += new System.EventHandler(this.chkSelectAll_Enter);
			this.chkSelectAll.Leave += new System.EventHandler(this.chkSelectAll_Leave);
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// DeliveryApproval
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(670, 468);
			this.Controls.Add(this.btnShowDetail);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnApprove);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.txtPartName);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.txtPartNumber);
			this.Controls.Add(this.txtCategory);
			this.Controls.Add(this.txtPONo);
			this.Controls.Add(this.chkSelectAll);
			this.Controls.Add(this.btnPartName);
			this.Controls.Add(this.lblPartName);
			this.Controls.Add(this.btnPartNumber);
			this.Controls.Add(this.lblPartNumber);
			this.Controls.Add(this.lblModel);
			this.Controls.Add(this.lblCategory);
			this.Controls.Add(this.btnCategory);
			this.Controls.Add(this.dtmToDate);
			this.Controls.Add(this.dtmFromDate);
			this.Controls.Add(this.lblToStartDate);
			this.Controls.Add(this.lblFromStartDate);
			this.Controls.Add(this.btnPONo);
			this.Controls.Add(this.lblPONo);
			this.Name = "DeliveryApproval";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Approve/Cancel Delivery Schedule";
			this.Load += new System.EventHandler(this.DeliveryApproval_Load);
			((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void DeliveryApproval_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".DeliveryApproval_Load()";
			try
			{
				//Set authorization for user
				
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					return;
				}
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				
				txtPONo.Tag = string.Empty;
				txtCategory.Tag = string.Empty;
				txtPartNumber.Tag = string.Empty;
				dtmFromDate.FormatType = FormatTypeEnum.CustomFormat;
				dtmFromDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				dtmToDate.FormatType = FormatTypeEnum.CustomFormat;
				dtmToDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
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
		
		private void txtPONo_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPONo_Validating()";
			try
			{
				if (!txtPONo.Modified) return;
				if (txtPONo.Text.Trim() == string.Empty)
				{
					txtPONo.Tag = string.Empty;
					return;
				}
				DataRowView drvResult = FormControlComponents.OpenSearchForm(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, txtPONo.Text.Trim(), null, false);
				if (drvResult != null)
				{
					txtPONo.Text = drvResult[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
					txtPONo.Tag = drvResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD];
				}
				else
					e.Cancel = true;
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

		private void txtPONo_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPONo_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnPONo_Click(null, null);
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

		private void btnPONo_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPONo_Click()";
			try
			{
				DataRowView drvResult = FormControlComponents.OpenSearchForm(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, txtPONo.Text.Trim(), null, true);
				if (drvResult != null)
				{
					txtPONo.Text = drvResult[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
					txtPONo.Tag = drvResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD];
				}
				else
					txtPONo.Focus();
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

		private void txtCategory_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_Validating()";
			try
			{
				if (!txtCategory.Modified) return;
				if (txtCategory.Text.Trim() == string.Empty)
				{
					txtCategory.Tag = string.Empty;
					txtPartNumber.Text = string.Empty;
					txtPartNumber.Tag = string.Empty;
					txtPartName.Text = string.Empty;
					return;
				}
				DataRowView drvResult = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text.Trim(), null, false);
				if (drvResult != null)
				{
					txtCategory.Text = drvResult[ITM_CategoryTable.CODE_FLD].ToString();
					txtCategory.Tag = drvResult[ITM_CategoryTable.CATEGORYID_FLD];
					txtPartNumber.Text = string.Empty;
					txtPartNumber.Tag = string.Empty;
					txtPartName.Text = string.Empty;
				}
				else
					e.Cancel = true;
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

		private void txtCategory_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnCategory_Click(null, null);
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

		private void btnCategory_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCategory_Click()";
			try
			{
				DataRowView drvResult = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text.Trim(), null, true);
				if (drvResult != null)
				{
					txtCategory.Text = drvResult[ITM_CategoryTable.CODE_FLD].ToString();
					txtCategory.Tag = drvResult[ITM_CategoryTable.CATEGORYID_FLD];
					txtPartNumber.Text = string.Empty;
					txtPartNumber.Tag = string.Empty;
					txtPartName.Text = string.Empty;
				}
				else
					txtPONo.Focus();
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

		private void txtPartNumber_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPONo_Validating()";
			try
			{
				if (!txtPartNumber.Modified) return;
				if (txtPartNumber.Text.Trim() == string.Empty)
				{
					txtPartNumber.Tag = string.Empty;
					txtPartName.Text = string.Empty;
					return;
				}
				string strFilter = string.Empty;
				
				if(txtCategory.Tag != null && txtCategory.Tag.ToString() != string.Empty)
					strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + " = " + txtCategory.Tag.ToString();
				DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strFilter, false);
				if (dtbData != null && dtbData.Rows.Count > 0)
				{
					StringBuilder sbID = new StringBuilder();
					foreach (DataRow drowData in dtbData.Rows)
						sbID.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
					txtPartNumber.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
					txtPartNumber.Tag = sbID.ToString(0, sbID.Length - 1);
					txtPartName.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
				}
				else
					e.Cancel = true;
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

		private void txtPartNumber_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNumber_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnPartNumber_Click(null, null);
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

		private void btnPartNumber_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNumber_Click()";
			try
			{
				string strFilter = string.Empty;
				
				if(txtCategory.Tag != null && txtCategory.Tag.ToString() != string.Empty)
					strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + "=" + txtCategory.Tag.ToString();
				DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strFilter, true);
				if (dtbData != null && dtbData.Rows.Count > 0)
				{
					StringBuilder sbID = new StringBuilder();
					foreach (DataRow drowData in dtbData.Rows)
						sbID.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
					txtPartNumber.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
					txtPartNumber.Tag = sbID.ToString(0, sbID.Length - 1);
					txtPartName.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
				}
				else
					txtPartNumber.Focus();
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

		private void txtPartName_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_Validating()";
			try
			{
				if (!txtPartName.Modified) return;
				if (txtPartName.Text.Trim() == string.Empty)
				{
					txtPartNumber.Text = string.Empty;
					txtPartNumber.Tag = null;
					txtPartName.Text = string.Empty;
					return;
				}
				string strFilter = string.Empty;
				
				if(txtCategory.Tag != null && txtCategory.Tag.ToString() != string.Empty)
					strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + "=" + txtCategory.Tag.ToString();
				DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strFilter, false);
				if (dtbData != null && dtbData.Rows.Count > 0)
				{
					StringBuilder sbID = new StringBuilder();
					foreach (DataRow drowData in dtbData.Rows)
						sbID.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
					txtPartNumber.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
					txtPartNumber.Tag = sbID.ToString(0, sbID.Length - 1);
					txtPartName.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
				}
				else
					e.Cancel = true;
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

		private void txtPartName_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
					btnPartName_Click(null, null);
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

		private void btnPartName_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartName_Click()";
			try
			{
				string strFilter = string.Empty;
				
				if(txtCategory.Tag != null && txtCategory.Tag.ToString() != string.Empty)
					strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + "=" + txtCategory.Tag.ToString();
				DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strFilter, true);
				if (dtbData != null && dtbData.Rows.Count > 0)
				{
					StringBuilder sbID = new StringBuilder();
					foreach (DataRow drowData in dtbData.Rows)
						sbID.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
					txtPartNumber.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
					txtPartNumber.Tag = sbID.ToString(0, sbID.Length - 1);
					txtPartName.Text = (dtbData.Rows.Count > 1) ? "Multi Selection" : dtbData.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
				}
				else
					txtPartName.Focus();
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
		private void btnSearch_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			try
			{
				Cursor = Cursors.WaitCursor;
				DeliveryApprovalBO boDeliveryApprove = new DeliveryApprovalBO();
				DateTime dtmStartDate = DateTime.MinValue, dtmEndDate = DateTime.MinValue;
				try
				{
					dtmStartDate = Convert.ToDateTime(dtmFromDate.Value);
				}
				catch{}
				try
				{
					dtmEndDate = Convert.ToDateTime(dtmToDate.Value);
				}
				catch{}
				int intPOMasterID = 0, intCategoryID = 0;
				try
				{
					intPOMasterID = Convert.ToInt32(txtPONo.Tag);
				}
				catch{}
				try
				{
					intCategoryID = Convert.ToInt32(txtCategory.Tag);
				}
				catch{}
				dstData = boDeliveryApprove.SearchDeliverySchedule(dtmStartDate, dtmEndDate, 
					intPOMasterID, intCategoryID, txtPartNumber.Tag.ToString());
				dstData.AcceptChanges();
				BindDataToGrid();
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
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		private void btnShowDetail_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnShowDetail_Click()";
			try
			{
				if (dgrdData.RowCount > 0)
				{
					PurchaseOrder frmPurchaseOrder = new PurchaseOrder();
					frmPurchaseOrder.Show();
					frmPurchaseOrder.LoadMaster((int) dgrdData[dgrdData.Row, PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD]);
				}
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

		private void btnApprove_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnApprove_Click()";
			try
			{
				if (dstData == null || dstData.Tables.Count == 0 || dstData.Tables[0].Rows.Count == 0)
					return;
				Cursor = Cursors.WaitCursor;
				DeliveryApprovalBO boApproval = new DeliveryApprovalBO();
				string strCancelList = string.Empty, strApprovedList = string.Empty;
				DataTable dtbModified = dstData.Tables[0].GetChanges(DataRowState.Modified);
				foreach (DataRow drowData in dtbModified.Rows)
				{
					bool blnCancel = Convert.ToBoolean(drowData[PO_DeliveryScheduleTable.CANCELDELIVERY_FLD]);
					if (blnCancel)
						strCancelList += drowData[PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD] + ",";
					else
						strApprovedList += drowData[PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD] + ",";
				}
				boApproval.UpdateDelivery(strCancelList, strApprovedList);
				btnSearch_Click(null, null);
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
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
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAll_CheckedChanged()";
			try
			{
				Cursor = Cursors.WaitCursor;
				if (blnStateOfCheck)
					foreach (DataRow drowData in dstData.Tables[0].Rows)
						if (drowData.RowState != DataRowState.Deleted)
							drowData[PO_DeliveryScheduleTable.CANCELDELIVERY_FLD] = chkSelectAll.Checked;
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
			finally
			{
				Cursor = Cursors.Default;
			}
		}
		private void BindDataToGrid()
		{
			dgrdData.DataSource = dstData.Tables[0];
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);

			foreach (C1DisplayColumn c1Col in dgrdData.Splits[0].DisplayColumns)
				c1Col.Locked = true;
			dgrdData.Columns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			dgrdData.Columns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			dgrdData.Columns[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
			dgrdData.Splits[0].DisplayColumns[PO_DeliveryScheduleTable.CANCELDELIVERY_FLD].Locked = false;
		}

		private void chkSelectAll_Enter(object sender, System.EventArgs e)
		{
			blnStateOfCheck = true;
		}

		private void chkSelectAll_Leave(object sender, System.EventArgs e)
		{
			blnStateOfCheck = false;
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				if (e.Column.DataColumn.DataField == PO_DeliveryScheduleTable.CANCELDELIVERY_FLD)
				{
					for (int i =0; i <dgrdData.RowCount; i++)
					{
						if (!Convert.ToBoolean(dgrdData[i, PO_DeliveryScheduleTable.CANCELDELIVERY_FLD]))
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
	}
}
