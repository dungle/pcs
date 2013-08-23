using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using PCSComMaterials.Inventory.BO;
using PCSComSale.Order.BO;
using PCSComSale.Order.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

namespace PCSSale.Order
{
	/// <summary>
	/// Summary description for SOReleaseOrder.
	/// </summary>
	public class SOReleaseOrder : Form
	{
		#region My variables and consts
		const string THIS = "PCSSale.Order.SOReleaseOrder";
		const string AVAILABLE_QUANTITY = "AVAILABLEQUANTITY";
		const string UM = "UMCode";
		const string RELEASE = "RELEASE";
		const string RELEASE_VALUE = "RELEASEVALUE"; 
		const int CHECKED = 1;
		const int UNCHECKED = 0;
		const string V_SOMASTERNOTRELEASE = "V_SOMasterNotRelease";
		const string V_LOCATIONITEM = "V_LocationItem";
		const string V_BINITEM = "V_BinItem";
		const string AVAILQUANTITY = "AvailQuantity";
		const string MASTER_LOCATION = "MasterLocation";

		private EnumAction mFormAction = EnumAction.Default;
		public EnumAction FormAction
		{
			set{mFormAction = value;}
			get{return mFormAction;}
		}
		private DataTable dtbData = new DataTable();
		private bool blnIsChanged = false;		
		private bool blnFromDateFirstTimeEnter = false;
		private bool blnToDateFirstTimeEnter = false;
		DataTable dtbGridLayout;
		#endregion My variables and consts

		#region Windows Form Designer generated code

		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnRelease;
		private System.Windows.Forms.CheckBox chkSelectAll;
		private System.Windows.Forms.Button btnSaleOrder;
		private System.Windows.Forms.CheckBox chkBySaleOrder;
		private System.Windows.Forms.Button btnOrderNo;
		private System.Windows.Forms.Button btnItem;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblOrderNo;
		private System.Windows.Forms.TextBox txtItem;
		private System.Windows.Forms.Label lblItem;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TextBox txtRevision;
		private System.Windows.Forms.Label lblRevision;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Button btnDescription;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.TextBox txtOrderNo;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid gridData;
		private System.Windows.Forms.Label lblTo;
		private C1.Win.C1Input.C1DateEdit dtmFromDeliveryDate;
		private C1.Win.C1Input.C1DateEdit dtmToDeliveryDate;
		private System.Windows.Forms.Label lblDeliveryDateFrom;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.Button btnLocation;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.GroupBox grpLocation;
		private System.Windows.Forms.Label lblBin;
		private System.Windows.Forms.TextBox txtBin;
		private System.Windows.Forms.Button btnBin;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SOReleaseOrder()
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


		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SOReleaseOrder));
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnRelease = new System.Windows.Forms.Button();
			this.btnOrderNo = new System.Windows.Forms.Button();
			this.btnItem = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.txtOrderNo = new System.Windows.Forms.TextBox();
			this.lblOrderNo = new System.Windows.Forms.Label();
			this.txtItem = new System.Windows.Forms.TextBox();
			this.lblItem = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.txtRevision = new System.Windows.Forms.TextBox();
			this.lblRevision = new System.Windows.Forms.Label();
			this.lblCCN = new System.Windows.Forms.Label();
			this.btnDescription = new System.Windows.Forms.Button();
			this.lblDeliveryDateFrom = new System.Windows.Forms.Label();
			this.lblTo = new System.Windows.Forms.Label();
			this.btnSearch = new System.Windows.Forms.Button();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.btnSaleOrder = new System.Windows.Forms.Button();
			this.chkBySaleOrder = new System.Windows.Forms.CheckBox();
			this.dtmFromDeliveryDate = new C1.Win.C1Input.C1DateEdit();
			this.dtmToDeliveryDate = new C1.Win.C1Input.C1DateEdit();
			this.gridData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.btnLocation = new System.Windows.Forms.Button();
			this.lblLocation = new System.Windows.Forms.Label();
			this.grpLocation = new System.Windows.Forms.GroupBox();
			this.txtBin = new System.Windows.Forms.TextBox();
			this.btnBin = new System.Windows.Forms.Button();
			this.lblBin = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDeliveryDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDeliveryDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(568, 465);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 31;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(507, 465);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 30;
			this.btnHelp.Text = "&Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// btnRelease
			// 
			this.btnRelease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRelease.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRelease.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnRelease.Location = new System.Drawing.Point(4, 465);
			this.btnRelease.Name = "btnRelease";
			this.btnRelease.Size = new System.Drawing.Size(84, 23);
			this.btnRelease.TabIndex = 25;
			this.btnRelease.Text = "Com&mit";
			this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
			// 
			// btnOrderNo
			// 
			this.btnOrderNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOrderNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnOrderNo.Location = new System.Drawing.Point(241, 28);
			this.btnOrderNo.Name = "btnOrderNo";
			this.btnOrderNo.Size = new System.Drawing.Size(23, 20);
			this.btnOrderNo.TabIndex = 8;
			this.btnOrderNo.Text = "...";
			this.btnOrderNo.Click += new System.EventHandler(this.btnOrderNo_Click);
			// 
			// btnItem
			// 
			this.btnItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnItem.Location = new System.Drawing.Point(241, 50);
			this.btnItem.Name = "btnItem";
			this.btnItem.Size = new System.Drawing.Size(23, 20);
			this.btnItem.TabIndex = 11;
			this.btnItem.Text = "...";
			this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
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
			this.cboCCN.DropDownWidth = 200;
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(547, 6);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 50;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(81, 21);
			this.cboCCN.TabIndex = 1;
			this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboCCN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboCCN_KeyDown);
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
			// txtOrderNo
			// 
			this.txtOrderNo.Location = new System.Drawing.Point(118, 28);
			this.txtOrderNo.MaxLength = 40;
			this.txtOrderNo.Name = "txtOrderNo";
			this.txtOrderNo.Size = new System.Drawing.Size(122, 20);
			this.txtOrderNo.TabIndex = 7;
			this.txtOrderNo.Text = "";
			this.txtOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderNo_KeyDown);
			this.txtOrderNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtOrderNo_Validating);
			this.txtOrderNo.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblOrderNo
			// 
			this.lblOrderNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblOrderNo.ForeColor = System.Drawing.Color.Black;
			this.lblOrderNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblOrderNo.Location = new System.Drawing.Point(5, 28);
			this.lblOrderNo.Name = "lblOrderNo";
			this.lblOrderNo.Size = new System.Drawing.Size(115, 19);
			this.lblOrderNo.TabIndex = 6;
			this.lblOrderNo.Text = "Order No.";
			// 
			// txtItem
			// 
			this.txtItem.Location = new System.Drawing.Point(118, 50);
			this.txtItem.MaxLength = 50;
			this.txtItem.Name = "txtItem";
			this.txtItem.Size = new System.Drawing.Size(122, 20);
			this.txtItem.TabIndex = 10;
			this.txtItem.Text = "";
			this.txtItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
			this.txtItem.Validating += new System.ComponentModel.CancelEventHandler(this.txtItem_Validating);
			// 
			// lblItem
			// 
			this.lblItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblItem.ForeColor = System.Drawing.Color.Black;
			this.lblItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblItem.Location = new System.Drawing.Point(5, 50);
			this.lblItem.Name = "lblItem";
			this.lblItem.Size = new System.Drawing.Size(115, 19);
			this.lblItem.TabIndex = 9;
			this.lblItem.Text = "Part Number";
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(118, 72);
			this.txtDescription.MaxLength = 200;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(274, 20);
			this.txtDescription.TabIndex = 15;
			this.txtDescription.Text = "";
			this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
			this.txtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.txtDescription_Validating);
			// 
			// lblDescription
			// 
			this.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblDescription.ForeColor = System.Drawing.Color.Black;
			this.lblDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblDescription.Location = new System.Drawing.Point(5, 72);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(115, 20);
			this.lblDescription.TabIndex = 14;
			this.lblDescription.Text = "Part Name";
			// 
			// txtRevision
			// 
			this.txtRevision.Enabled = false;
			this.txtRevision.Location = new System.Drawing.Point(310, 50);
			this.txtRevision.MaxLength = 20;
			this.txtRevision.Name = "txtRevision";
			this.txtRevision.Size = new System.Drawing.Size(82, 20);
			this.txtRevision.TabIndex = 13;
			this.txtRevision.Text = "";
			this.txtRevision.Leave += new System.EventHandler(this.OnLeaveControl);
			this.txtRevision.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblRevision
			// 
			this.lblRevision.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblRevision.ForeColor = System.Drawing.Color.Black;
			this.lblRevision.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblRevision.Location = new System.Drawing.Point(273, 50);
			this.lblRevision.Name = "lblRevision";
			this.lblRevision.Size = new System.Drawing.Size(37, 20);
			this.lblRevision.TabIndex = 12;
			this.lblRevision.Text = "Model";
			this.lblRevision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCCN
			// 
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(514, 5);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(32, 20);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnDescription
			// 
			this.btnDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDescription.Location = new System.Drawing.Point(393, 72);
			this.btnDescription.Name = "btnDescription";
			this.btnDescription.Size = new System.Drawing.Size(23, 20);
			this.btnDescription.TabIndex = 16;
			this.btnDescription.Text = "...";
			this.btnDescription.Click += new System.EventHandler(this.btnDescription_Click);
			// 
			// lblDeliveryDateFrom
			// 
			this.lblDeliveryDateFrom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblDeliveryDateFrom.ForeColor = System.Drawing.Color.Black;
			this.lblDeliveryDateFrom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblDeliveryDateFrom.Location = new System.Drawing.Point(5, 5);
			this.lblDeliveryDateFrom.Name = "lblDeliveryDateFrom";
			this.lblDeliveryDateFrom.Size = new System.Drawing.Size(115, 20);
			this.lblDeliveryDateFrom.TabIndex = 2;
			this.lblDeliveryDateFrom.Text = "Del. Date, Time From";
			this.lblDeliveryDateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTo
			// 
			this.lblTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblTo.ForeColor = System.Drawing.Color.Black;
			this.lblTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblTo.Location = new System.Drawing.Point(242, 6);
			this.lblTo.Name = "lblTo";
			this.lblTo.Size = new System.Drawing.Size(25, 20);
			this.lblTo.TabIndex = 4;
			this.lblTo.Text = "To";
			this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearch.Location = new System.Drawing.Point(547, 72);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(81, 23);
			this.btnSearch.TabIndex = 17;
			this.btnSearch.Text = "&Search";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkSelectAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkSelectAll.Location = new System.Drawing.Point(91, 465);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(80, 24);
			this.chkSelectAll.TabIndex = 26;
			this.chkSelectAll.Text = "S&elect All";
			this.chkSelectAll.Click += new System.EventHandler(this.chkSelectAll_Click);
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// btnSaleOrder
			// 
			this.btnSaleOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaleOrder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSaleOrder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSaleOrder.Location = new System.Drawing.Point(390, 465);
			this.btnSaleOrder.Name = "btnSaleOrder";
			this.btnSaleOrder.Size = new System.Drawing.Size(116, 23);
			this.btnSaleOrder.TabIndex = 28;
			this.btnSaleOrder.Text = "Sales &Order Detail";
			this.btnSaleOrder.Click += new System.EventHandler(this.btnSaleOrder_Click);
			// 
			// chkBySaleOrder
			// 
			this.chkBySaleOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkBySaleOrder.Enabled = false;
			this.chkBySaleOrder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkBySaleOrder.Location = new System.Drawing.Point(176, 465);
			this.chkBySaleOrder.Name = "chkBySaleOrder";
			this.chkBySaleOrder.Size = new System.Drawing.Size(110, 24);
			this.chkBySaleOrder.TabIndex = 27;
			this.chkBySaleOrder.Text = "&By Sales Order";
			this.chkBySaleOrder.CheckedChanged += new System.EventHandler(this.chkBySaleOrder_CheckedChanged);
			// 
			// dtmFromDeliveryDate
			// 
			// 
			// dtmFromDeliveryDate.Calendar
			// 
			this.dtmFromDeliveryDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmFromDeliveryDate.CustomFormat = "dd-MM-yyyy";
			this.dtmFromDeliveryDate.EmptyAsNull = true;
			this.dtmFromDeliveryDate.ErrorInfo.ShowErrorMessage = false;
			this.dtmFromDeliveryDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmFromDeliveryDate.Location = new System.Drawing.Point(118, 6);
			this.dtmFromDeliveryDate.Name = "dtmFromDeliveryDate";
			this.dtmFromDeliveryDate.Size = new System.Drawing.Size(122, 20);
			this.dtmFromDeliveryDate.TabIndex = 3;
			this.dtmFromDeliveryDate.Tag = null;
			this.dtmFromDeliveryDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmFromDeliveryDate.ValueChanged += new System.EventHandler(this.dtmFromDeliveryDate_ValueChanged);
			this.dtmFromDeliveryDate.GotFocus += new System.EventHandler(this.dtmFromDeliveryDate_GotFocus);
			this.dtmFromDeliveryDate.Enter += new System.EventHandler(this.OnEnterControl);
			this.dtmFromDeliveryDate.DropDownClosed += new DropDownClosedEventHandler(this.dtmFromDeliveryDate_DropDownClosed);
			this.dtmFromDeliveryDate.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// dtmToDeliveryDate
			// 
			// 
			// dtmToDeliveryDate.Calendar
			// 
			this.dtmToDeliveryDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmToDeliveryDate.CustomFormat = "dd-MM-yyyy";
			this.dtmToDeliveryDate.EmptyAsNull = true;
			this.dtmToDeliveryDate.ErrorInfo.ShowErrorMessage = false;
			this.dtmToDeliveryDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmToDeliveryDate.Location = new System.Drawing.Point(270, 6);
			this.dtmToDeliveryDate.Name = "dtmToDeliveryDate";
			this.dtmToDeliveryDate.Size = new System.Drawing.Size(122, 20);
			this.dtmToDeliveryDate.TabIndex = 5;
			this.dtmToDeliveryDate.Tag = null;
			this.dtmToDeliveryDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmToDeliveryDate.ValueChanged += new System.EventHandler(this.dtmToDeliveryDate_ValueChanged);
			this.dtmToDeliveryDate.GotFocus += new System.EventHandler(this.dtmToDeliveryDate_GotFocus);
			this.dtmToDeliveryDate.Enter += new System.EventHandler(this.OnEnterControl);
			this.dtmToDeliveryDate.DropDownClosed += new DropDownClosedEventHandler(this.dtmToDeliveryDate_DropDownClosed);
			this.dtmToDeliveryDate.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// gridData
			// 
			this.gridData.AllowSort = false;
			this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridData.CaptionHeight = 17;
			this.gridData.CollapseColor = System.Drawing.Color.Black;
			this.gridData.ExpandColor = System.Drawing.Color.Black;
			this.gridData.FilterBar = true;
			this.gridData.GroupByCaption = "Drag a column header here to group by that column";
			this.gridData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.gridData.Location = new System.Drawing.Point(4, 136);
			this.gridData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.gridData.Name = "gridData";
			this.gridData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.gridData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.gridData.PreviewInfo.ZoomFactor = 75;
			this.gridData.PrintInfo.ShowOptionsDialog = false;
			this.gridData.RecordSelectorWidth = 16;
			this.gridData.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.gridData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.gridData.RowHeight = 15;
			this.gridData.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.gridData.Size = new System.Drawing.Size(624, 322);
			this.gridData.TabIndex = 24;
			this.gridData.Text = "c1TrueDBGrid1";
			this.gridData.Click += new System.EventHandler(this.gridData_Click);
			this.gridData.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridData_AfterColEdit);
			this.gridData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridData_ButtonClick);
			this.gridData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridData_AfterColUpdate);
			this.gridData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.gridData_BeforeColUpdate);
			this.gridData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridData_KeyDown);
			this.gridData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Sale Order " +
				"No.\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Del. Line\" DataField=\"Line\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"Delivery Qty\" DataField=\"DeliveryQuantit" +
				"y\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Sc" +
				"hedule Date\" DataField=\"ScheduleDate\"><ValueItems /><GroupInfo /></C1DataColumn>" +
				"<C1DataColumn Level=\"0\" Caption=\"Committed\" DataField=\"RELEASE\"><ValueItems /><G" +
				"roupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Available Qty\" DataFi" +
				"eld=\"AvailableQuantity\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"Unit\" DataField=\"UMCode\"><ValueItems /><GroupInfo /></C1DataC" +
				"olumn><C1DataColumn Level=\"0\" Caption=\"Priority\" DataField=\"Priority\"><ValueItem" +
				"s /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Part Number\" Da" +
				"taField=\"ITEM\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
				" Caption=\"Part Name\" DataField=\"Description\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueItems " +
				"/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Ship From Master " +
				"Location\" DataField=\"MasterLocation\"><ValueItems /><GroupInfo /></C1DataColumn><" +
				"C1DataColumn Level=\"0\" Caption=\"Location\" DataField=\"MST_LocationCode\"><ValueIte" +
				"ms /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Bin\" DataField" +
				"=\"MST_BINCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
				" Caption=\"Customer Item Code\" DataField=\"CusCode\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"Customer Item Name\" DataField=\"CusN" +
				"ame\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
				"Priority\" DataField=\"Priority\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
				"Column Level=\"0\" Caption=\"Gate\" DataField=\"SO_GateCode\"><ValueItems /><GroupInfo" +
				" /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWra" +
				"pper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Inactive{F" +
				"oreColor:InactiveCaptionText;BackColor:InactiveCaption;}Style119{AlignHorz:Near;" +
				"}Style118{AlignHorz:Near;}Style78{}Style79{}Style85{}Editor{}Style117{}Style116{" +
				"}Style72{}Style73{}Style70{AlignHorz:Center;}Style71{AlignHorz:Near;}Style76{Ali" +
				"gnHorz:Center;}Style77{AlignHorz:Near;}Style74{}Style75{}Style84{}Style87{}Style" +
				"86{}Style81{}Style80{}Style83{AlignHorz:Near;}Style82{AlignHorz:Center;}Style89{" +
				"AlignHorz:Near;}Style88{AlignHorz:Center;}Style108{}Style109{}Style104{}Style105" +
				"{}Style106{AlignHorz:Near;ForeColor:ControlText;}Style107{AlignHorz:Near;}Style1" +
				"00{AlignHorz:Near;ForeColor:Maroon;}Style101{AlignHorz:Near;}Style102{}Style103{" +
				"}Style94{AlignHorz:Near;}Style95{AlignHorz:Near;}Style96{}Style97{}Style90{}Styl" +
				"e91{}Style92{}Style93{}Style98{}Style99{}Heading{Wrap:True;BackColor:Control;Bor" +
				"der:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style18{}Style19{" +
				"}Style14{}Style15{}Style16{AlignHorz:Center;}Style17{AlignHorz:Near;}Style10{Ali" +
				"gnHorz:Near;}Style11{}Style12{}Style13{}Selected{ForeColor:HighlightText;BackCol" +
				"or:Highlight;}Style122{}Style123{}Style120{}Style28{AlignHorz:Center;}Style27{}S" +
				"tyle9{}Style24{}Style26{}Style29{AlignHorz:Near;}Style5{}Style4{}Style7{}Style6{" +
				"}Style25{}Style121{}Style23{AlignHorz:Near;}Style22{AlignHorz:Near;}Style21{}Sty" +
				"le20{}OddRow{}Style110{}Style38{}Style39{}Style36{}FilterBar{}Style34{AlignHorz:" +
				"Center;}Style35{AlignHorz:Near;}Style32{}Style33{}Style30{}Style49{}Style48{}Sty" +
				"le37{}Style31{}Normal{}Style41{AlignHorz:Near;}Style40{AlignHorz:Center;}Style43" +
				"{}Style42{}Style45{}Style44{}Style47{AlignHorz:Near;}Style46{AlignHorz:Center;}E" +
				"venRow{BackColor:Aqua;}Style8{}Style59{AlignHorz:Near;}Style51{}Style58{AlignHor" +
				"z:Center;}RecordSelector{AlignImage:Center;}Style3{}Style2{}Style50{}Footer{}Sty" +
				"le52{AlignHorz:Center;}Style53{AlignHorz:Near;}Style54{}Style55{}Style56{}Style5" +
				"7{}Caption{AlignHorz:Center;}Style64{AlignHorz:Near;}Style112{AlignHorz:Near;}St" +
				"yle69{}Style68{}Style1{}Style63{}Style62{}Style61{}Style60{}Style67{}Style66{}St" +
				"yle65{AlignHorz:Near;}Style115{}Style114{}Style111{}Group{AlignVert:Center;Borde" +
				"r:None,,0, 0, 0, 0;BackColor:ControlDark;}Style113{AlignHorz:Near;}</Data></Styl" +
				"es><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCapti" +
				"onHeight=\"17\" ColumnFooterHeight=\"17\" FilterBar=\"True\" MarqueeStyle=\"DottedCellB" +
				"order\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" Hori" +
				"zontalScrollGroup=\"1\"><ClientRect>0, 0, 620, 318</ClientRect><BorderSide>0</Bord" +
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
				"le</ColumnDivider><Width>122</Width><Height>15</Height><DCIdx>0</DCIdx></C1Displ" +
				"ayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style pa" +
				"rent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><Editor" +
				"Style parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style" +
				"33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><C" +
				"olumnDivider>DarkGray,Single</ColumnDivider><Width>58</Width><Height>15</Height>" +
				"<DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\"" +
				" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style" +
				"3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle " +
				"parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" />" +
				"<Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>108<" +
				"/Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" />" +
				"<FooterStyle parent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
				"le79\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle paren" +
				"t=\"Style1\" me=\"Style80\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single<" +
				"/ColumnDivider><Width>140</Width><Height>15</Height><DCIdx>8</DCIdx></C1DisplayC" +
				"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style82\" /><Style paren" +
				"t=\"Style1\" me=\"Style83\" /><FooterStyle parent=\"Style3\" me=\"Style84\" /><EditorSty" +
				"le parent=\"Style5\" me=\"Style85\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style87\"" +
				" /><GroupFooterStyle parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible><Colu" +
				"mnDivider>DarkGray,Single</ColumnDivider><Width>156</Width><Height>15</Height><D" +
				"CIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style88\" /><Style parent=\"Style1\" me=\"Style89\" /><FooterStyle parent=\"Style3\"" +
				" me=\"Style90\" /><EditorStyle parent=\"Style5\" me=\"Style91\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style93\" /><GroupFooterStyle parent=\"Style1\" me=\"Style92\" /><V" +
				"isible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>92</Wi" +
				"dth><Height>15</Height><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><Head" +
				"ingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><F" +
				"ooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style" +
				"61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=" +
				"\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</C" +
				"olumnDivider><Width>36</Width><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColu" +
				"mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style70\" /><Style parent=\"" +
				"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=\"Style72\" /><EditorStyle " +
				"parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style75\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><ColumnDivider>DarkGray,Single<" +
				"/ColumnDivider><Width>28</Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style94\" /><Style parent" +
				"=\"Style1\" me=\"Style95\" /><FooterStyle parent=\"Style3\" me=\"Style96\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style97\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style99\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style98\" /><ColumnDivider>DarkGray,Singl" +
				"e</ColumnDivider><Width>119</Width><Height>15</Height><DCIdx>11</DCIdx></C1Displ" +
				"ayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style100\" /><Style p" +
				"arent=\"Style1\" me=\"Style101\" /><FooterStyle parent=\"Style3\" me=\"Style102\" /><Edi" +
				"torStyle parent=\"Style5\" me=\"Style103\" /><GroupHeaderStyle parent=\"Style1\" me=\"S" +
				"tyle105\" /><GroupFooterStyle parent=\"Style1\" me=\"Style104\" /><ColumnDivider>Dark" +
				"Gray,Single</ColumnDivider><Width>73</Width><Height>15</Height><DCIdx>12</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style106\" /" +
				"><Style parent=\"Style1\" me=\"Style107\" /><FooterStyle parent=\"Style3\" me=\"Style10" +
				"8\" /><EditorStyle parent=\"Style5\" me=\"Style109\" /><GroupHeaderStyle parent=\"Styl" +
				"e1\" me=\"Style111\" /><GroupFooterStyle parent=\"Style1\" me=\"Style110\" /><ColumnDiv" +
				"ider>DarkGray,Single</ColumnDivider><Width>46</Width><Height>15</Height><DCIdx>1" +
				"3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visibl" +
				"e>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>78</Width><" +
				"Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterS" +
				"tyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style" +
				"1\" me=\"Style56\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Width>81</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1" +
				"\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Width>121</Width><Height>15</Height><DCIdx>14</" +
				"DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style" +
				"64\" /><Style parent=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
				"le66\" /><EditorStyle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"St" +
				"yle1\" me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>T" +
				"rue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>141</Width><He" +
				"ight>15</Height><DCIdx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
				"e parent=\"Style2\" me=\"Style112\" /><Style parent=\"Style1\" me=\"Style113\" /><Footer" +
				"Style parent=\"Style3\" me=\"Style114\" /><EditorStyle parent=\"Style5\" me=\"Style115\"" +
				" /><GroupHeaderStyle parent=\"Style1\" me=\"Style117\" /><GroupFooterStyle parent=\"S" +
				"tyle1\" me=\"Style116\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Co" +
				"lumnDivider><Width>44</Width><Height>15</Height><DCIdx>16</DCIdx></C1DisplayColu" +
				"mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"" +
				"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle " +
				"parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnD" +
				"ivider>DarkGray,Single</ColumnDivider><Width>63</Width><Height>15</Height><DCIdx" +
				">4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle118\" /><Style parent=\"Style1\" me=\"Style119\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style120\" /><EditorStyle parent=\"Style5\" me=\"Style121\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style123\" /><GroupFooterStyle parent=\"Style1\" me=\"Style122\" />" +
				"<Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15<" +
				"/Height><DCIdx>17</DCIdx></C1DisplayColumn></internalCols></C1.Win.C1TrueDBGrid." +
				"MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"No" +
				"rmal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Headin" +
				"g\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\"" +
				" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=" +
				"\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me" +
				"=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\"" +
				" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits" +
				">1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSel" +
				"Width>16</DefaultRecSelWidth><ClientArea>0, 0, 620, 318</ClientArea><PrintPageHe" +
				"aderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" " +
				"/></Blob>";
			// 
			// txtLocation
			// 
			this.txtLocation.AccessibleDescription = "";
			this.txtLocation.AccessibleName = "";
			this.txtLocation.Location = new System.Drawing.Point(118, 110);
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(122, 20);
			this.txtLocation.TabIndex = 19;
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
			this.btnLocation.Location = new System.Drawing.Point(241, 110);
			this.btnLocation.Name = "btnLocation";
			this.btnLocation.Size = new System.Drawing.Size(24, 20);
			this.btnLocation.TabIndex = 20;
			this.btnLocation.Text = "...";
			this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
			// 
			// lblLocation
			// 
			this.lblLocation.AccessibleDescription = "";
			this.lblLocation.AccessibleName = "";
			this.lblLocation.ForeColor = System.Drawing.Color.Maroon;
			this.lblLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLocation.Location = new System.Drawing.Point(6, 110);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.Size = new System.Drawing.Size(110, 20);
			this.lblLocation.TabIndex = 18;
			this.lblLocation.Text = "Location";
			this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grpLocation
			// 
			this.grpLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpLocation.Location = new System.Drawing.Point(4, 100);
			this.grpLocation.Name = "grpLocation";
			this.grpLocation.Size = new System.Drawing.Size(624, 3);
			this.grpLocation.TabIndex = 48;
			this.grpLocation.TabStop = false;
			// 
			// txtBin
			// 
			this.txtBin.AccessibleDescription = "";
			this.txtBin.AccessibleName = "";
			this.txtBin.Location = new System.Drawing.Point(310, 110);
			this.txtBin.Name = "txtBin";
			this.txtBin.Size = new System.Drawing.Size(82, 20);
			this.txtBin.TabIndex = 22;
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
			this.btnBin.Location = new System.Drawing.Point(393, 110);
			this.btnBin.Name = "btnBin";
			this.btnBin.Size = new System.Drawing.Size(24, 20);
			this.btnBin.TabIndex = 23;
			this.btnBin.Text = "...";
			this.btnBin.Click += new System.EventHandler(this.btnBin_Click);
			// 
			// lblBin
			// 
			this.lblBin.AccessibleDescription = "";
			this.lblBin.AccessibleName = "";
			this.lblBin.ForeColor = System.Drawing.Color.Maroon;
			this.lblBin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblBin.Location = new System.Drawing.Point(273, 110);
			this.lblBin.Name = "lblBin";
			this.lblBin.Size = new System.Drawing.Size(37, 20);
			this.lblBin.TabIndex = 21;
			this.lblBin.Text = "Bin";
			this.lblBin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// SOReleaseOrder
			// 
			this.AcceptButton = this.btnSearch;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(632, 492);
			this.Controls.Add(this.txtBin);
			this.Controls.Add(this.txtItem);
			this.Controls.Add(this.gridData);
			this.Controls.Add(this.txtOrderNo);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.txtRevision);
			this.Controls.Add(this.txtLocation);
			this.Controls.Add(this.btnBin);
			this.Controls.Add(this.lblBin);
			this.Controls.Add(this.grpLocation);
			this.Controls.Add(this.dtmToDeliveryDate);
			this.Controls.Add(this.dtmFromDeliveryDate);
			this.Controls.Add(this.chkBySaleOrder);
			this.Controls.Add(this.btnSaleOrder);
			this.Controls.Add(this.chkSelectAll);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.lblTo);
			this.Controls.Add(this.lblDeliveryDateFrom);
			this.Controls.Add(this.btnDescription);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblOrderNo);
			this.Controls.Add(this.lblItem);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.lblRevision);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnRelease);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnItem);
			this.Controls.Add(this.btnOrderNo);
			this.Controls.Add(this.btnLocation);
			this.Controls.Add(this.lblLocation);
			this.Name = "SOReleaseOrder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sale Order Commitment";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.SOReleaseOrder_Closing);
			this.Load += new System.EventHandler(this.SOReleaseOrder_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDeliveryDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDeliveryDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Form Events, clicks, leave, load or closing
		//**************************************************************************              
		///    <summary>
		///       This method uses to 
		///    </summary>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, February 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void SOReleaseOrder_Load(object sender, EventArgs e)
		{	
			const string METHOD_NAME = THIS + ".SOReleaseOrder_Load()";
			try
			{
				#region Security
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
				#endregion

				// init cboCCN
				UtilsBO boUtil = new UtilsBO();
				DataSet dstCCN = boUtil.ListCCN();
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[0],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				// set default CCN
				cboCCN.SelectedValue = SystemProperty.CCNID;
				
				//HACKED by Tuan TQ: Nov 03, 2005: Set format for datetime controls
				dtmFromDeliveryDate.FormatType = FormatTypeEnum.CustomFormat;
				dtmFromDeliveryDate.CustomFormat = Constants.DATETIME_FORMAT + HOUR_MINUTE_FORMAT;

				dtmToDeliveryDate.FormatType = FormatTypeEnum.CustomFormat;
				dtmToDeliveryDate.CustomFormat = Constants.DATETIME_FORMAT + HOUR_MINUTE_FORMAT;
				//End Tuan TQ
				dtbGridLayout = FormControlComponents.StoreGridLayout(gridData);
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
		///    <summary>
		///       This method uses to 
		///    </summary>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, February 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		//**************************************************************************              
		///    <summary>
		///       This method uses to 
		///    </summary>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, February 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnOrderNo_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnOrderNo_Click()";			
			try
			{
				Hashtable htbCriteria = new Hashtable();
				
				//User has selected CCN
				if (cboCCN.SelectedIndex != -1 )
				{
					htbCriteria.Add(SO_SaleOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue);	
					
				}
					//User has not selected CCN
				else
				{
					htbCriteria.Add(SO_SaleOrderMasterTable.CCNID_FLD, 0);
					
				}

				DataRowView drowView = FormControlComponents.OpenSearchForm(V_SOMASTERNOTRELEASE, SO_SaleOrderMasterTable.CODE_FLD, txtOrderNo.Text.Trim(), htbCriteria, true);
				if(drowView == null) return;
				txtOrderNo.Text = drowView[SO_SaleOrderMasterTable.CODE_FLD].ToString();

				//HACKED: Added by Tuan TQ - 2005-11-07: Fix error no. 2623
				//Reset modified status
				txtOrderNo.Modified = false;				
				//End
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
		///    <summary>
		///       This method uses to 
		///    </summary>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, February 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnItem_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnItem_Click()";
			try
			{
				DataRowView drowView = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtItem.Text.Trim(), null, true);
				if(drowView == null) return;
				txtItem.Text = drowView[ITM_ProductTable.CODE_FLD].ToString();
				txtRevision.Text = drowView[ITM_ProductTable.REVISION_FLD].ToString();
				txtDescription.Text = drowView[ITM_ProductTable.DESCRIPTION_FLD].ToString();

				//HACKED: Added by Tuan TQ - 2005-11-07: Fix error no. 2623
				//Reset modified status
				txtDescription.Modified = false;
				txtItem.Modified = false;
				//End
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
		///    <summary>
		///       This method uses to 
		///    </summary>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, February 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSearch_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			try
			{
				Cursor = Cursors.WaitCursor;
				if(CheckMandatory(cboCCN))
				{
					Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					cboCCN.Focus();
					return;
				}

				if((dtmFromDeliveryDate.Value != DBNull.Value) && dtmToDeliveryDate.Value != DBNull.Value)
					if((DateTime)dtmFromDeliveryDate.Value > (DateTime)dtmToDeliveryDate.Value)
					{
						Cursor = Cursors.Default;
						PCSMessageBox.Show(ErrorCode.MESSAGE_ENDDATE_GREATER_THAN_BEGINDATE,MessageBoxIcon.Exclamation);
						dtmToDeliveryDate.Focus();
						return;
					}

				if(CheckMandatory(txtLocation))
				{
					Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtLocation.Focus();
					return;
				}
				if(CheckMandatory(txtBin))
				{
					Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtBin.Focus();
					return;
				}

				SOReleaseOrderBO boRelease = new SOReleaseOrderBO();
				DateTime dtmTransFrom, dtmTransTo;
				dtmTransFrom = dtmTransTo = DateTime.MinValue;

				if(dtmFromDeliveryDate.Value != DBNull.Value)
					dtmTransFrom = (DateTime)dtmFromDeliveryDate.Value;

				if(dtmToDeliveryDate.Value != DBNull.Value)
					dtmTransTo = (DateTime)dtmToDeliveryDate.Value;

				dtbData = null;
				dtbData = boRelease.Search(int.Parse(cboCCN.SelectedValue.ToString()), dtmTransFrom, dtmTransTo,
				                 txtOrderNo.Text.Trim(), txtItem.Text.Trim(), txtRevision.Text.Trim(), txtDescription.Text.Trim(),
					Convert.ToInt32(txtLocation.Tag), Convert.ToInt32(txtBin.Tag));

				InitGrid(dtbData);
				//FillAvailableQty();
				Cursor = Cursors.Default;
			}
			catch (PCSException ex)
			{
				Cursor = Cursors.Default;
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
				Cursor = Cursors.Default;
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
			finally
			{
				Cursor = Cursors.Default;
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
			const string RELEASEVALUE = "RELEASEVALUE";
			for (int i = 0; i < gridData.RowCount; i++)
			{
				if (gridData[i, RELEASE_VALUE].ToString() == CHECKED.ToString())
					return true;
			}
			return false;
		}

		private void btnRelease_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnRelease_Click()";
			const string SO_MSG = "Sale Order Line";
			const int DELETED_CODE = -2147217873;
			try
			{
				Cursor = Cursors.WaitCursor;	
				if(dtbData.Rows.Count == 0) return;
				if (!CheckDataGridToRelease())
				{
					Cursor = Cursors.Default;
					// You have to select at least a line to commit 
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_AT_LEAST_LINE_TO_COMMIT, MessageBoxIcon.Information);
					return;
				}

				if (txtLocation.Text.Trim() == string.Empty)
				{
					Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_DOCK_TO_STOCK_YOU_MUST_SELECT_ATLEAST_ONE_LOCATION, MessageBoxIcon.Warning);
					txtLocation.Focus();
					return;
				}
				if (txtBin.Text.Trim() == string.Empty)
				{
					Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_BIN_FOR_LOCATION, MessageBoxIcon.Warning);
					txtBin.Focus();
					return;
				}
				if (!blnIsChanged)
				{
					Cursor = Cursors.Default;
					return;
				}
				int intCount = 0;
				for (int i = 0; i < gridData.RowCount; i++)
				{
					if (gridData[i, RELEASE].ToString() == 1.ToString())
					{
						intCount++;
					}

				}
				// get list of checked row only
				DataTable dtbReleasedData = dtbData.Clone();
				foreach (DataRow drowData in dtbData.Rows)
				{
					if(drowData.RowState == DataRowState.Deleted) continue;
					if(int.Parse(drowData["RELEASEVALUE"].ToString()) == CHECKED)
						dtbReleasedData.ImportRow(drowData);
				}
				UpdateRelease(dtbReleasedData, int.Parse(txtLocation.Tag.ToString()), int.Parse(txtBin.Tag.ToString()));
				// Refresh list
				btnSearch_Click(null,null);
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA,MessageBoxIcon.Information);
				chkSelectAll.Checked = false;
				blnIsChanged = false;
			}
			catch (PCSException ex)
			{
				Cursor = Cursors.Default;
				if (ex.CauseException is OleDbException)
				{
					OleDbException exOle = (OleDbException) ex.CauseException;
					if (exOle.ErrorCode == DELETED_CODE)
					{
						string[] strMsg = new string[1];
						strMsg[0] = SO_MSG;

						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECTION_NOT_EXIST, MessageBoxIcon.Error, strMsg);	
					}
				}
				else
				{
					if (ex.mCode == ErrorCode.MSG_WOISSUE_MATERIAL_COMMIT_QTY_DONOT_MEET_AVAILABLE_QTY)
					{
						int intDeliveryScheduleID = 0;
						if (ex.Hash != null)
						{
							IDictionaryEnumerator myEnumerator = ex.Hash.GetEnumerator();
							while ( myEnumerator.MoveNext() )
							{
								if (myEnumerator.Value != DBNull.Value)
								{
									intDeliveryScheduleID = (int) myEnumerator.Value;
								}
							}
							for (int i = 0; i < gridData.RowCount; i++)
							{
								if (gridData[i, SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString() == intDeliveryScheduleID.ToString())
								{
									gridData.Row = i;
									PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
									gridData.Focus();
								}
							}
						}
					}
					else
						PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
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
				Cursor = Cursors.Default;
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
			Cursor = Cursors.Default;
		}

		//**************************************************************************              
		///    <summary>
		///       This method uses to 
		///    </summary>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, February 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void chkBySaleOrder_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if(dtbData.Rows.Count ==  0) return;
				bool blnIsNotAll = false;
				InventoryUtilsBO boInventory = new InventoryUtilsBO();
				UtilsBO boUtils = new UtilsBO();
				DateTime dtmDBDate = (new UtilsBO()).GetDBDate();
				int intSOMasterID = int.Parse(gridData[gridData.Row,SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString());
				for(int i = 0; i < gridData.RowCount; i++)
				{
					if(intSOMasterID == int.Parse(gridData[i,SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString()))
					{
						if(chkBySaleOrder.Checked == true)
						{
							if(Convert.ToInt32(gridData[i,SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD]) == Convert.ToInt32(gridData[i,ITM_ProductTable.MASTERLOCATIONID_FLD]))
							{
								gridData[i,MST_LocationTable.LOCATIONID_FLD] = gridData[i,ITM_ProductTable.TABLE_NAME + MST_LocationTable.LOCATIONID_FLD];
								gridData[i,MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = gridData[i,ITM_ProductTable.TABLE_NAME + MST_LocationTable.LOCATIONID_FLD + MST_LocationTable.CODE_FLD];
								if(Convert.ToInt32(gridData[i,ITM_ProductTable.TABLE_NAME + MST_BINTable.BINID_FLD]) == 0)
								{
									// Get avail qty by Loc
									decimal decAvailQty = boInventory.GetAvailableQtyByPostDate(dtmDBDate,
										Convert.ToInt32(gridData[i,SO_SaleOrderMasterTable.CCNID_FLD]),
										Convert.ToInt32(gridData[i,SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD]),
										Convert.ToInt32(gridData[i,MST_LocationTable.LOCATIONID_FLD]),0,
										Convert.ToInt32(gridData[i,ITM_ProductTable.PRODUCTID_FLD]));
									decimal decRate = boUtils.GetUMRate(Convert.ToInt32(gridData[i,SO_SaleOrderDetailTable.STOCKUMID_FLD]),
										Convert.ToInt32(gridData[i,SO_SaleOrderDetailTable.SELLINGUMID_FLD]));
									if(decRate == 0)
									{
										PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SET_UMRATE,MessageBoxIcon.Error);
										gridData.Focus();
										gridData.Row = i;
										gridData.Col = gridData.Columns.IndexOf(gridData.Columns[UM]);
										return;
									}

									gridData[i,AVAILABLE_QUANTITY] = decAvailQty*decRate;
									if(Convert.ToDecimal(gridData[i,AVAILABLE_QUANTITY]) >= Convert.ToDecimal(gridData[i,SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]))
									{
										gridData[i,RELEASE] = CHECKED;
										gridData[i,RELEASE_VALUE] = CHECKED;
										blnIsChanged = true;
									}
								}
								else
								{
									gridData[i,MST_LocationTable.LOCATIONID_FLD] = gridData[i,ITM_ProductTable.TABLE_NAME + MST_LocationTable.LOCATIONID_FLD];
									gridData[i,MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = gridData[i,ITM_ProductTable.TABLE_NAME + MST_LocationTable.LOCATIONID_FLD + MST_LocationTable.CODE_FLD];

									gridData[i,MST_BINTable.BINID_FLD] = gridData[i,ITM_ProductTable.TABLE_NAME + MST_BINTable.BINID_FLD];
									gridData[i,MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = gridData[i,ITM_ProductTable.TABLE_NAME + MST_BINTable.BINID_FLD + MST_BINTable.CODE_FLD];

									// Get avail qty by Bin
									decimal decAvailQty = boInventory.GetAvailableQtyByPostDate(dtmDBDate,
										Convert.ToInt32(gridData[i,SO_SaleOrderMasterTable.CCNID_FLD]),
										Convert.ToInt32(gridData[i,SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD]),
										Convert.ToInt32(gridData[i,ITM_ProductTable.TABLE_NAME + MST_LocationTable.LOCATIONID_FLD]),
										Convert.ToInt32(gridData[i,ITM_ProductTable.TABLE_NAME + MST_BINTable.BINID_FLD]),
										Convert.ToInt32(gridData[i,ITM_ProductTable.PRODUCTID_FLD]));
									decimal decRate = boUtils.GetUMRate(Convert.ToInt32(gridData[i,SO_SaleOrderDetailTable.STOCKUMID_FLD]),
										Convert.ToInt32(gridData[i,SO_SaleOrderDetailTable.SELLINGUMID_FLD]));
									if(decRate == 0)
									{
										PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SET_UMRATE,MessageBoxIcon.Error);
										gridData.Focus();
										gridData.Row = i;
										gridData.Col = gridData.Columns.IndexOf(gridData.Columns[UM]);
										return;
									}

									gridData[i,AVAILABLE_QUANTITY] = decAvailQty*decRate;
									if(Convert.ToDecimal(gridData[i,AVAILABLE_QUANTITY]) >= Convert.ToDecimal(gridData[i,SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]))
									{
										gridData[i,RELEASE] = CHECKED;
										gridData[i,RELEASE_VALUE] = CHECKED;
										blnIsChanged = true;
									}
								}
							
								// if OnHand < Delivery Quantity - Commited Quantity then user unable to commit
								decimal decOnHand = decimal.Parse(gridData[i,AVAILABLE_QUANTITY].ToString());
								decimal decDeliveryQuantity = decimal.Parse(gridData[i,SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
								decimal decCommitedQuantity = 0; //decimal.Parse(gridData[i,COMMITED_QUANTITY].ToString());
								if(decOnHand >= decDeliveryQuantity - decCommitedQuantity)
								{
									//gridData.SetCellCheck(i,gridData.Cols[RELEASE].Index,CheckEnum.Checked);
									gridData[i,RELEASE] = CHECKED;
									gridData[i,RELEASE_VALUE] = CHECKED; 
									blnIsChanged = true;
								}
								else
								{
									blnIsNotAll = true;
								}
							}
						}
						else
						{
							gridData[i,RELEASE] = UNCHECKED;
							gridData[i,RELEASE_VALUE] = UNCHECKED; 
						}
					}
				}
				if(blnIsNotAll)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_NOT_ENOUGH_QUANTITY,MessageBoxIcon.Warning);
				}
			}
			catch
			{
				return;
			}
		}

		//**************************************************************************              
		///    <summary>
		///       This method uses to 
		///    </summary>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, February 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSaleOrder_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSaleOrder_Click()";
			if(dtbData.Rows.Count == 0) return;
			try
			{
				SaleOrder frmSale = new SaleOrder();
				frmSale.SaleOrderMasterID = int.Parse(gridData[gridData.Row,SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString());
				frmSale.FormAction = EnumAction.Default;
				frmSale.Show();
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
		///    <summary>
		///       This method uses to 
		///    </summary>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, February 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnHelp_Click(object sender, EventArgs e)
		{
			// Not implement
		}
		
		private void btnDescription_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDescription_Click()";
			try
			{
				DataRowView drowView = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtDescription.Text.Trim(), null, true);
				if(drowView == null) return;
				txtItem.Text = drowView[ITM_ProductTable.CODE_FLD].ToString();
				txtRevision.Text = drowView[ITM_ProductTable.REVISION_FLD].ToString();
				txtDescription.Text = drowView[ITM_ProductTable.DESCRIPTION_FLD].ToString();
				
				//HACKED: Added by Tuan TQ - 2005-11-07: Fix error no. 2623
				//Reset modified status
				txtDescription.Modified = false;
				txtItem.Modified = false;
				//End				
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
		///    <summary>
		///       This method uses to 
		///    </summary>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, February 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void SOReleaseOrder_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".SOReleaseOrder_Closing()";
			try
			{
				//if(blnIsChanged)
				//{
					// Do you want to Release 
					//DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
					//if(dlgResult == DialogResult.Yes) btnRelease_Click(null,null);
					//else if(dlgResult == DialogResult.Cancel) e.Cancel = true;
				//}
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
		private void txtOrderNo_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				btnOrderNo_Click(sender,e);
		}

		private void txtItem_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				btnItem_Click(sender,e);
		}

		private void txtDescription_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				btnDescription_Click(sender,e);
		}
		
		#endregion Form Events, clicks, leave, load or closing

		#region Other functions
		//**************************************************************************              
		///    <summary>
		///       This method uses to init grid
		///    </summary>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, February 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void InitGrid(DataTable dtbData)
		{
			const string BOOLEAN = "Boolean";
			const int ROWSTART = 1;

			const string METHOD_NAME = THIS + ".InitGrid()";
			gridData.DataSource = dtbData;				
			
			for(int i = 0; i < gridData.Splits[0].DisplayColumns.Count; i++)
			{
				gridData.Splits[0].DisplayColumns[i].Locked = true;
			}

			gridData.Columns[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;

			gridData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
			gridData.Columns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT + HOUR_MINUTE_FORMAT;

			gridData.Columns[RELEASE].ValueItems.Presentation = PresentationEnum.CheckBox;	
			gridData.Splits[0].DisplayColumns[RELEASE].Style.HorizontalAlignment = AlignHorzEnum.Center;
			gridData.Splits[0].DisplayColumns[RELEASE].Locked = false;

			gridData.Columns[AVAILABLE_QUANTITY].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				
			//gridData.Columns[SO_SaleOrderMasterTable.PRIORITY_FLD].ValueItems.Presentation = PresentationEnum.CheckBox;
			gridData.Columns[SO_SaleOrderMasterTable.PRIORITY_FLD].NumberFormat = Constants.INTERGER_NUMBERFORMAT;
			gridData.Splits[0].DisplayColumns[SO_SaleOrderMasterTable.PRIORITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				
			for(int i=0; i < gridData.RowCount; i++)
			{					
				gridData[i, RELEASE] = UNCHECKED;
			}
			FormControlComponents.RestoreGridLayout(gridData,dtbGridLayout);
			gridData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Visible = false;
			gridData.Splits[0].DisplayColumns[MASTER_LOCATION].Visible = false;
			gridData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Visible = false;
		}

		//**************************************************************************              
		///    <summary>
		///       This method uses to check mandatory
		///    </summary>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, February 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool CheckMandatory(Control pobjControl)
		{
			if (pobjControl.Text.Trim() == string.Empty)
			{
				return true;
			}
			return false;
		}


		#endregion Other functions

		private void gridData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridData_BeforeColUpdate()";
			try
			{
				if (e.Column.DataColumn.Value.ToString() == string.Empty)
				{
					if(gridData.RowCount > 0)
					{
						e.Column.DataColumn.Value = DBNull.Value;	
					}
					return;
				}
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				switch (e.Column.DataColumn.DataField)
				{
					case MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD:
						# region open Location search form 
						if (gridData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							htbCriteria.Add(ITM_ProductTable.PRODUCTID_FLD,gridData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value);
							htbCriteria.Add(ITM_ProductTable.MASTERLOCATIONID_FLD,gridData.Columns[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD].Value);
							drwResult = FormControlComponents.OpenSearchForm(V_LOCATIONITEM, MST_LocationTable.CODE_FLD, gridData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
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
					case MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD:
						# region open Bin search form 
						if (gridData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (gridData[gridData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() != string.Empty)
							{
								htbCriteria.Add(MST_LocationTable.LOCATIONID_FLD, int.Parse(gridData[gridData.Row, PRO_WOReversalDetailTable.LOCATIONID_FLD].ToString()));
							}
							else //User has not entered Location
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Warning);
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
								gridData.Focus();
								return;
							}
							htbCriteria.Add(ITM_ProductTable.PRODUCTID_FLD,gridData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value);
							drwResult = FormControlComponents.OpenSearchForm(V_BINITEM, MST_BINTable.CODE_FLD, gridData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
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

		private void gridData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridData_BeforeColUpdate()";
			try
			{
				if(dtbData.Rows.Count == 0) return;
				DataRow drwResult = (DataRow) e.Column.DataColumn.Tag;
				//Fill Data to Location
				if (e.Column.DataColumn.DataField == MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(gridData[gridData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() == string.Empty))
					{
						gridData[gridData.Row,MST_LocationTable.LOCATIONID_FLD] = DBNull.Value;
						gridData[gridData.Row,AVAILABLE_QUANTITY] = 0;
						gridData[gridData.Row,RELEASE] = UNCHECKED;
						gridData[gridData.Row,RELEASE_VALUE] = UNCHECKED;
						gridData[gridData.Row,MST_BINTable.BINID_FLD] = DBNull.Value;
						gridData[gridData.Row,MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = DBNull.Value;
					}
					else
					{
						decimal decRate = (new UtilsBO()).GetUMRate(Convert.ToInt32(gridData[gridData.Row,SO_SaleOrderDetailTable.STOCKUMID_FLD]),
							Convert.ToInt32(gridData[gridData.Row,SO_SaleOrderDetailTable.SELLINGUMID_FLD]));
						if(decRate == 0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SET_UMRATE,MessageBoxIcon.Error);
							return;
						}

						gridData[gridData.Row,gridData.Col] = drwResult[MST_LocationTable.CODE_FLD];
						gridData[gridData.Row,MST_LocationTable.LOCATIONID_FLD] = drwResult[MST_LocationTable.LOCATIONID_FLD];
						gridData[gridData.Row,MST_LocationTable.BIN_FLD] = drwResult[MST_LocationTable.BIN_FLD];

						if(!Convert.ToBoolean(gridData[gridData.Row,MST_LocationTable.BIN_FLD]))
						{
							gridData[gridData.Row,AVAILABLE_QUANTITY] = Convert.ToDecimal(drwResult[AVAILQUANTITY])*decRate;
							if(Convert.ToDecimal(gridData[gridData.Row,AVAILABLE_QUANTITY]) >= Convert.ToDecimal(gridData[gridData.Row,SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]))
							{
								gridData[gridData.Row,RELEASE] = CHECKED;
								gridData[gridData.Row,RELEASE_VALUE] = CHECKED;
								blnIsChanged = true;
							}
						}
						
					}
				}
				if (e.Column.DataColumn.DataField == MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(gridData[gridData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() == string.Empty))
					{
						gridData[gridData.Row,MST_BINTable.BINID_FLD] = DBNull.Value;
						gridData[gridData.Row,AVAILABLE_QUANTITY] = 0;
						gridData[gridData.Row,RELEASE] = UNCHECKED;
						gridData[gridData.Row,RELEASE_VALUE] = UNCHECKED;
					}
					else
					{
						decimal decRate = (new UtilsBO()).GetUMRate(Convert.ToInt32(gridData[gridData.Row,SO_SaleOrderDetailTable.STOCKUMID_FLD]),
							Convert.ToInt32(gridData[gridData.Row,SO_SaleOrderDetailTable.SELLINGUMID_FLD]));
						if(decRate == 0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SET_UMRATE,MessageBoxIcon.Error);
							return;
						}

						gridData[gridData.Row,gridData.Col] = drwResult[MST_BINTable.CODE_FLD];
						gridData[gridData.Row,MST_BINTable.BINID_FLD] = drwResult[MST_BINTable.BINID_FLD];
						gridData[gridData.Row,AVAILABLE_QUANTITY] = Convert.ToDecimal(drwResult[AVAILQUANTITY])*decRate;
						if(Convert.ToDecimal(gridData[gridData.Row,AVAILABLE_QUANTITY]) >= Convert.ToDecimal(gridData[gridData.Row,SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]))
						{
							gridData[gridData.Row,RELEASE] = CHECKED;
							gridData[gridData.Row,RELEASE_VALUE] = CHECKED;
							blnIsChanged = true;
						}
						
					}
				}
				
				if(e.Column.DataColumn.DataField == RELEASE)
				{ 
					if (txtLocation.Text.Trim() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_DOCK_TO_STOCK_YOU_MUST_SELECT_ATLEAST_ONE_LOCATION, MessageBoxIcon.Warning);
						txtLocation.Focus();
						gridData[gridData.Row,gridData.Col] = UNCHECKED;
						return;
					}
					if (txtBin.Text.Trim() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_BIN_FOR_LOCATION, MessageBoxIcon.Warning);
						txtBin.Focus();
						gridData[gridData.Row,gridData.Col] = UNCHECKED;
						return;
					}
					// if OnHand < Delivery Quantity - Commited Quantity then user unable to commit
					decimal decOnHand = decimal.Parse(gridData[gridData.Row,AVAILABLE_QUANTITY].ToString());
					decimal decDeliveryQuantity = decimal.Parse(gridData[gridData.Row,SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
					decimal decCommitedQuantity = 0; //decimal.Parse(gridData[gridData.Row,COMMITED_QUANTITY].ToString());
					if(decOnHand < decDeliveryQuantity - decCommitedQuantity)
					{
						// You unable to commit this item because the [On Hand Quantity] < [Delivery Quantity]-[Commited Quantity] 
						PCSMessageBox.Show(ErrorCode.MESSAGE_NOT_ENOUGH_QUANTITY,MessageBoxIcon.Warning);
						gridData[gridData.Row,gridData.Col] = UNCHECKED;
						return;
					}
					else if(bool.Parse(gridData[gridData.Row, gridData.Col].ToString()))
					{
						// gridData.SetCellCheck(gridData.RowSel,gridData.ColSel,CheckEnum.Checked);
						gridData[gridData.Row, RELEASE_VALUE] = CHECKED; 
						blnIsChanged = true;
					}
					else if(!bool.Parse(gridData[gridData.Row,gridData.Col].ToString()))
					{
						// gridData.SetCellCheck(gridData.RowSel,gridData.ColSel,CheckEnum.Checked);
						gridData[gridData.Row, RELEASE_VALUE] = UNCHECKED;
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
		/// txtOrderNo_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, July 7 2005</date>
		
		//**************************************************************************              
		///    <summary>
		///       OnEnterControl
		///    </summary>
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
		private void OnEnterControl(object sender, EventArgs e)
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
		///    <summary>
		///       OnLeaveControl
		///    </summary>
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
		private void OnLeaveControl(object sender, EventArgs e)
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
		
		private void cboCCN_KeyDown(object sender, KeyEventArgs e)
		{
			//if(e.KeyCode == Keys.Escape) this.Close();
		}

		private void chkSelectAll_Click(object sender, EventArgs e)
		{
			try
			{
				if(dtbData.Rows.Count ==  0) return;
				bool blnIsNotAll = false;
				if (txtLocation.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_DOCK_TO_STOCK_YOU_MUST_SELECT_ATLEAST_ONE_LOCATION, MessageBoxIcon.Warning);
					txtLocation.Focus();
					chkSelectAll.Checked = false;
					return;
				}
				if (txtBin.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_BIN_FOR_LOCATION, MessageBoxIcon.Warning);
					txtBin.Focus();
					chkSelectAll.Checked = false;
					return;
				}
				int intRowError = 0;
				for(int i = 0; i < gridData.RowCount; i++)
				{
					if(chkSelectAll.Checked == true)
					{
						if(Convert.ToInt32(gridData[i,SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD]) == Convert.ToInt32(gridData[i,ITM_ProductTable.MASTERLOCATIONID_FLD]))
						{
							// if OnHand < Delivery Quantity - Commited Quantity then user unable to commit
							decimal decOnHand = decimal.Parse(gridData[i,AVAILABLE_QUANTITY].ToString());
							decimal decDeliveryQuantity = decimal.Parse(gridData[i,SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
							decimal decCommitedQuantity = 0; //decimal.Parse(gridData[i,COMMITED_QUANTITY].ToString());
							if(decOnHand >= decDeliveryQuantity - decCommitedQuantity)
							{
								//gridData.SetCellCheck(i,gridData.Cols[RELEASE].Index,CheckEnum.Checked);
								gridData[i,RELEASE] = CHECKED;
								gridData[i,RELEASE_VALUE] = CHECKED; 
								blnIsChanged = true;
							}
							else
							{
								blnIsNotAll = true;
								intRowError = i;
							}
						}
					}
					else
					{
						gridData[i, RELEASE] = UNCHECKED;
						gridData[i, RELEASE_VALUE] = UNCHECKED; 
					}
				}

				if(blnIsNotAll)
				{
					// You unable to commit this item because the [On Hand Quantity] < [Delivery Quantity]-[Commited Quantity] 
					PCSMessageBox.Show(ErrorCode.MESSAGE_NOT_ENOUGH_QUANTITY,MessageBoxIcon.Warning);
					chkSelectAll.Checked = false;
					gridData.Focus();
					gridData.Row = intRowError;
					gridData.Col = gridData.Columns.IndexOf(gridData.Columns[AVAILABLE_QUANTITY]);
				}
			}
			catch
			{
				return;
			}
		}

		#region HACKED by Tuan TQ -- Nov 03, 2005. Set hour, minute to 0 when select delivery date
		
		const string ZERO_TIME_FORMAT = " 00:00";
		const string HOUR_MINUTE_FORMAT = " HH:mm";
		const string SQL_DATE_TIME_FORMAT = "yyyy-MM-dd";

		private void dtmFromDeliveryDate_DropDownClosed(object sender, EventArgs e)
		{
			const string METHOD_NAME = "dtmFromDeliveryDate_DropDownClosed";
			try
			{
				if(!dtmFromDeliveryDate.ValueIsDbNull && (dtmFromDeliveryDate.Text != string.Empty))
				{
					dtmFromDeliveryDate.Value = ((DateTime)dtmFromDeliveryDate.Value).ToString(SQL_DATE_TIME_FORMAT + ZERO_TIME_FORMAT);
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

		private void dtmToDeliveryDate_DropDownClosed(object sender, EventArgs e)
		{
			const string METHOD_NAME = "dtmFromDeliveryDate_DropDownClosed";
			try
			{
				if(!dtmToDeliveryDate.ValueIsDbNull && (dtmToDeliveryDate.Text != string.Empty))
				{
					dtmToDeliveryDate.Value = ((DateTime)dtmToDeliveryDate.Value).ToString(SQL_DATE_TIME_FORMAT + ZERO_TIME_FORMAT);
				}
			}
			catch(Exception ex)
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

		private void dtmToDeliveryDate_ValueChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = "dtmToDeliveryDate_ValueChanged";
			try
			{
				if(blnToDateFirstTimeEnter)
				{
					if(!dtmToDeliveryDate.ValueIsDbNull && (dtmToDeliveryDate.Text != string.Empty))
					{
						dtmToDeliveryDate.Value = ((DateTime)dtmToDeliveryDate.Value).ToString(SQL_DATE_TIME_FORMAT + ZERO_TIME_FORMAT);
					}
					blnToDateFirstTimeEnter = false;
				}
				else
				{
					if(dtmToDeliveryDate.ValueIsDbNull || (dtmToDeliveryDate.Text == string.Empty))
					{
						blnToDateFirstTimeEnter = true;
					}
					
				}

				
			}
			catch(Exception ex)
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
		
		private void dtmFromDeliveryDate_ValueChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = "dtmFromDeliveryDate_ValueChanged";
			try
			{
				if(blnFromDateFirstTimeEnter)
				{
					if(!dtmFromDeliveryDate.ValueIsDbNull && (dtmFromDeliveryDate.Text != string.Empty))
					{
						dtmFromDeliveryDate.Value = ((DateTime)dtmFromDeliveryDate.Value).ToString(SQL_DATE_TIME_FORMAT + ZERO_TIME_FORMAT);
					}

					blnFromDateFirstTimeEnter = false;
				}
				else
				{
					if(dtmFromDeliveryDate.ValueIsDbNull || (dtmFromDeliveryDate.Text == string.Empty))
					{
						blnFromDateFirstTimeEnter = true;
					}
				}
			}
			catch(Exception ex)
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
		
		private void dtmFromDeliveryDate_GotFocus(object sender, EventArgs e)
		{
			try
			{
				if(dtmFromDeliveryDate.ValueIsDbNull || dtmFromDeliveryDate.Value.ToString().Equals(string.Empty))
				{
					blnFromDateFirstTimeEnter = true;
				}
			}
			catch
			{}
		}
		
		private void dtmToDeliveryDate_GotFocus(object sender, EventArgs e)
		{
			try
			{
				if(dtmToDeliveryDate.ValueIsDbNull || dtmToDeliveryDate.Value.ToString().Equals(string.Empty))
				{
					blnToDateFirstTimeEnter = true;
				}
			}
			catch
			{}
		}
		
		private void txtDescription_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDescription_Validating()";
			try
			{				
				if(txtDescription.Text.Trim() == string.Empty)
				{	
					//HACK: Added by Tuan TQ - 2005-10-21: Fix error No. 2243.
					txtItem.Text = string.Empty;
					txtRevision.Text = string.Empty;
					//End HACK
					return;
				}
				
				//HACKED: Added by Tuan TQ - 2005-11-07: Fix error no. 2623
				if(!txtDescription.Modified)
				{
					return;
				}
				//END HACK

				DataRowView drowView = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtDescription.Text.Trim(), null, false);
				if(drowView == null)
				{
					//HACKED: Added by Tuan TQ - 2005-11-07: Fix error no. 2623
					txtDescription.Focus();
					//END HACK
					e.Cancel = true;
					return;
				}

				txtItem.Text = drowView[ITM_ProductTable.CODE_FLD].ToString();
				txtRevision.Text = drowView[ITM_ProductTable.REVISION_FLD].ToString();
				txtDescription.Text = drowView[ITM_ProductTable.DESCRIPTION_FLD].ToString();
				
				//HACKED: Added by Tuan TQ - 2005-11-07: Fix error no. 2623
				//Reset modify status
				txtDescription.Modified = false;
				//END
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
		
		private void txtItem_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtItem_Validating()";
			try
			{				
				if (txtItem.Text.Trim() == string.Empty)
				{	
					//HACKED: Added by Tuan TQ - 2005-10-21: Fix error no. 2243
					txtRevision.Text = string.Empty;
					txtDescription.Text = string.Empty;
					//END HACK
					return;
				}

				//HACKED: Added by Tuan TQ - 2005-11-07: Fix error no. 2623
				if(!txtItem.Modified)
				{
					return;
				}
				//END HACK

				DataRowView drowView = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtItem.Text.Trim(), null, false);
				if(drowView == null)
				{
					//HACKED: Added by Tuan TQ - 2005-11-07: Fix error no. 2623
					txtItem.Focus();
					e.Cancel = true;
					//END HACK
					return;
				}

				txtItem.Text = drowView[ITM_ProductTable.CODE_FLD].ToString();
				txtRevision.Text = drowView[ITM_ProductTable.REVISION_FLD].ToString();
				txtDescription.Text = drowView[ITM_ProductTable.DESCRIPTION_FLD].ToString();

				//HACKED: Added by Tuan TQ - 2005-11-07: Fix error no. 2623
				//Reset modified status
				txtItem.Modified = false;
				//END HACK
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
		
		private void txtOrderNo_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtOrderNo_Validating()";
			try
			{				
				if (txtOrderNo.Text.ToString() == string.Empty)
				{
					return;
				}
				
				//HACKED: Added by Tuan TQ - 2005-11-07: Fix error no. 2623
				if(!txtOrderNo.Modified) return;
				//END

				Hashtable htbCriteria = new Hashtable();
				//User has selected CCN
				if (cboCCN.SelectedIndex != -1 )
				{
					htbCriteria.Add(SO_SaleOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					htbCriteria.Add(SO_SaleOrderMasterTable.CCNID_FLD, 0);
				}
				
				//HACKED: Modified by Tuan TQ - 2005-11-10: Fix error no. 2629
				DataRowView drowView = FormControlComponents.OpenSearchForm(V_SOMASTERNOTRELEASE, SO_SaleOrderMasterTable.CODE_FLD, txtOrderNo.Text.Trim(), htbCriteria, false);
				//END HACK

				if(drowView == null) 
				{
					//HACKED: Added by Tuan TQ - 2005-11-07: Fix error no. 2623
					txtOrderNo.Focus();
					e.Cancel = true;
					//END HACK
					return;
				}

				txtOrderNo.Text = drowView[SO_SaleOrderMasterTable.CODE_FLD].ToString();				
				//HACKED: Added by Tuan TQ - 2005-11-07: Fix error no. 2623
				//Reset modified status
				txtOrderNo.Modified = false;
				//End
			}
			catch(PCSException ex)
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
			catch(Exception ex)
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

		#endregion HACKED by Tuan TQ -- Nov 03, 2005		

		private void gridData_Click(object sender, EventArgs e)
		{
		
			

		}

		private void gridData_ButtonClick(object sender, ColEventArgs e)
		{
			DataRowView drowView = null;
			if(gridData.Columns[gridData.Col] == gridData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD])
			{
				Hashtable hash = new Hashtable();
				hash.Add(ITM_ProductTable.PRODUCTID_FLD,gridData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value);
				hash.Add(ITM_ProductTable.MASTERLOCATIONID_FLD,gridData.Columns[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD].Value);
				drowView = FormControlComponents.OpenSearchForm(V_LOCATIONITEM,string.Empty,string.Empty,hash);
				if(drowView != null)
				{
					decimal decRate = (new UtilsBO()).GetUMRate(Convert.ToInt32(gridData[gridData.Row,SO_SaleOrderDetailTable.STOCKUMID_FLD]),
						Convert.ToInt32(gridData[gridData.Row,SO_SaleOrderDetailTable.SELLINGUMID_FLD]));
					if(decRate == 0)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SET_UMRATE,MessageBoxIcon.Error);
						return;
					}

					gridData[gridData.Row,gridData.Col] = drowView[MST_LocationTable.CODE_FLD];
					gridData[gridData.Row,MST_LocationTable.LOCATIONID_FLD] = drowView[MST_LocationTable.LOCATIONID_FLD];
					gridData[gridData.Row,MST_LocationTable.BIN_FLD] = drowView[MST_LocationTable.BIN_FLD];

					if (!Convert.ToBoolean(gridData[gridData.Row,MST_LocationTable.BIN_FLD]))
					{
						gridData[gridData.Row,AVAILABLE_QUANTITY] = Convert.ToDecimal(drowView[AVAILQUANTITY])*decRate;
						if(Convert.ToDecimal(gridData[gridData.Row,AVAILABLE_QUANTITY]) >= Convert.ToDecimal(gridData[gridData.Row,SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]))
						{
							gridData[gridData.Row,RELEASE] = CHECKED;
							gridData[gridData.Row,RELEASE_VALUE] = CHECKED;
							blnIsChanged = true;
						}
					}
				}
			}
			else if(gridData.Columns[gridData.Col] == gridData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD])
			{
//				if((!Convert.ToBoolean(gridData[gridData.Row,MST_LocationTable.BIN_FLD]))
//					&& (gridData[gridData.Row,MST_LocationTable.LOCATIONID_FLD].ToString() != string.Empty))
//				{
//					return;
//				}
																						
				if(gridData[gridData.Row,MST_LocationTable.LOCATIONID_FLD].ToString() != string.Empty)
					//&& (Convert.ToBoolean(gridData[gridData.Row,MST_LocationTable.BIN_FLD])))
				{
//					if(gridData[gridData.Row,ITM_ProductTable.LOCATIONID_FLD] == DBNull.Value)
//					{
//						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC,MessageBoxIcon.Error);
//						gridData.Focus();
//						gridData.Col = gridData.Columns.IndexOf(gridData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
//						return;
//					}

					Hashtable hash = new Hashtable();
					hash.Add(ITM_ProductTable.PRODUCTID_FLD,gridData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value);
					hash.Add(ITM_ProductTable.LOCATIONID_FLD,gridData.Columns[ITM_ProductTable.LOCATIONID_FLD].Value);
					drowView = FormControlComponents.OpenSearchForm("V_BinItem",string.Empty,string.Empty,hash);
					if(drowView != null)
					{
						decimal decRate = (new UtilsBO()).GetUMRate(Convert.ToInt32(gridData[gridData.Row,SO_SaleOrderDetailTable.STOCKUMID_FLD]),
							Convert.ToInt32(gridData[gridData.Row,SO_SaleOrderDetailTable.SELLINGUMID_FLD]));
						if(decRate == 0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SET_UMRATE,MessageBoxIcon.Error);
							return;
						}

						gridData[gridData.Row,gridData.Col] = drowView[MST_BINTable.CODE_FLD];
						gridData[gridData.Row,MST_BINTable.BINID_FLD] = drowView[MST_BINTable.BINID_FLD];
						gridData[gridData.Row,AVAILABLE_QUANTITY] = Convert.ToDecimal(drowView[AVAILQUANTITY])*decRate;
						if(Convert.ToDecimal(gridData[gridData.Row,AVAILABLE_QUANTITY]) >= Convert.ToDecimal(gridData[gridData.Row,SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]))
						{
							gridData[gridData.Row,RELEASE] = CHECKED;
							gridData[gridData.Row,RELEASE_VALUE] = CHECKED;
							blnIsChanged = true;
						}
					}
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC,MessageBoxIcon.Error);
					gridData.Col = gridData.Columns.IndexOf(gridData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
				}
			}
		}

		private void gridData_KeyDown(object sender, KeyEventArgs e)
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
					if(sender != null)
						if(sender.GetType().Equals(typeof(C1TrueDBGrid)))
							gridData_ButtonClick(null,null);
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
				case Keys.F12:
					// New record
					// if(voSaleOrderMaster.SaleOrderMasterID == 0) break;
					break;
			}	
		}
		/// <summary>
		/// btnLocation_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, Feb 6 2006</date>
		private void btnLocation_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnLocation_Click()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				htbCriteria.Add(MST_LocationTable.LOCATIONTYPEID_FLD, (int) LocationTypeEnum.WareHouse);
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, txtLocation.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					if ((txtLocation.Tag != null) && (int.Parse(txtLocation.Tag.ToString()) != int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString())))
					{
						txtBin.Tag = null;
						txtBin.Text = string.Empty;
						//Clear Quantity Columns
						for (int i = 0; i < gridData.RowCount; i++)
						{
							gridData[i, AVAILABLE_QUANTITY] = 0;
						}
					}
					txtLocation.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
					txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
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
		/// txtLocation_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, Feb 6 2006</date>
		private void txtLocation_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnLocation_Click()";
			try
			{
				if (!txtLocation.Modified) return;
				if (txtLocation.Text.Trim() == string.Empty)
				{
					//Clear Quantity Columns
					for (int i = 0; i < gridData.RowCount; i++)
					{
						gridData[i, AVAILABLE_QUANTITY] = 0;
					}
					txtLocation.Tag = null;
					//Clear Bin
					txtBin.Tag = null;
					txtBin.Text = string.Empty;
					return;
				}
				Hashtable htbCriteria = new Hashtable();
				htbCriteria.Add(MST_LocationTable.LOCATIONTYPEID_FLD, (int) LocationTypeEnum.WareHouse);
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, txtLocation.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					if ((txtLocation.Tag != null) && (int.Parse(txtLocation.Tag.ToString()) != int.Parse(drwResult[MST_LocationTable.LOCATIONID_FLD].ToString())))
					{
						txtBin.Tag = null;
						txtBin.Text = string.Empty;
						//Clear Quantity Columns
						for (int i = 0; i < gridData.RowCount; i++)
						{
							gridData[i, AVAILABLE_QUANTITY] = 0;
						}
					}
					txtLocation.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
					txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];

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
		/// btnBin_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, Feb 6 2006</date>
		private void btnBin_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnBin_Click()";
			Cursor = Cursors.WaitCursor;
			try
			{
				Hashtable htbCriteria = new Hashtable();
				if (txtLocation.Text.Trim() != string.Empty)
				{
					htbCriteria.Add(MST_LocationTable.LOCATIONID_FLD, int.Parse(txtLocation.Tag.ToString()));
				}
				else
				{
					Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_SELECT_LOCATION, MessageBoxIcon.Warning);
					txtLocation.Focus();
					return;
				}
				htbCriteria.Add(MST_BINTable.BINTYPEID_FLD, (int)BinTypeEnum.OK);
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, txtBin.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtBin.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
					txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];
				}
			}
			catch (PCSException ex)
			{
				Cursor = Cursors.Default;
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
				Cursor = Cursors.Default;
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
			Cursor = Cursors.Default;
		}
		/// <summary>
		/// Fill available Qty to grid
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 7 2006</date>
		private void FillAvailableQty()
		{
			InventoryUtilsBO boInventory = new InventoryUtilsBO();
			DataTable dtbAvailable = boInventory.GetAvailableQuantity(Convert.ToInt32(txtBin.Tag));
			for (int i = 0; i < gridData.RowCount; i++)
			{
				string strFilter = IV_BinCacheTable.PRODUCTID_FLD + "=" + Convert.ToInt32(gridData[i,ITM_ProductTable.PRODUCTID_FLD]);
				decimal decAvailQty = 0;
				try
				{
					decAvailQty = Convert.ToDecimal(dtbAvailable.Compute("SUM(" + Constants.AVAILABLE_QTY_COL + ")", strFilter));
				}
				catch{}
				decimal decRate = Convert.ToDecimal(gridData[i,SO_CommitInventoryDetailTable.UMRATE_FLD]);
				if(decRate == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SET_UMRATE,MessageBoxIcon.Error);
					gridData.Focus();
					gridData.Row = i;
					gridData.Col = gridData.Columns.IndexOf(gridData.Columns[UM]);
					return;
				}

				gridData[i, AVAILABLE_QUANTITY] = decAvailQty*decRate;
			}
		}
		/// <summary>
		/// txtBin_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, Feb 6 2006</date>
		private void txtBin_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBin_Validating()";
			try
			{
				if (!txtBin.Modified) return;
				if (txtBin.Text.Trim() == string.Empty)
				{
					//Clear Quantity Columns
					for (int i = 0; i < gridData.RowCount; i++)
					{
						gridData[i, AVAILABLE_QUANTITY] = 0;
					}
					//Clear Bin
					txtBin.Tag = null;
					return;
				}
				Hashtable htbCriteria = new Hashtable();
				if (txtLocation.Text.Trim() != string.Empty)
				{
					htbCriteria.Add(MST_LocationTable.LOCATIONID_FLD, int.Parse(txtLocation.Tag.ToString()));
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALRECEIPT_SELECT_LOCATION, MessageBoxIcon.Warning);
					e.Cancel = true;
					return;
				}
				htbCriteria.Add(MST_BINTable.BINTYPEID_FLD, (int) BinTypeEnum.OK);
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, txtBin.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtBin.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
					txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];
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

		private void txtBin_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				btnBin_Click(sender,e);
		}

		private void txtLocation_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				btnLocation_Click(sender,e);
		}
		/// <summary>
		/// CheckOrNochkCheckAll
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 7 2006</date>
		private void CheckOrNochkCheckAll()
		{
			const string METHOD_NAME = THIS + ".CheckOrNochkCheckAll()";
			const string TRUE = "True";
			try
			{
				for (int i =0; i <gridData.RowCount; i++)
				{
					if (gridData[i, RELEASE].ToString().Trim() != TRUE)
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
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>TRada</author>
		/// <date>Tuesday, Feb 7 2006</date>
		private void gridData_AfterColEdit(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridData_AfterColEdit()";
			try
			{
				if (e.Column.DataColumn.DataField == RELEASE)
				{
					CheckOrNochkCheckAll();
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

		private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
		{
		
		}

		/// <summary>
		/// We now calculate data in client site first, then store all data to database at the end of process.
		/// </summary>
		/// <param name="pdstData">Detail Data</param>
		/// <param name="pintLocationID">Location</param>
		/// <param name="pintBinID">Bin</param>
		private void UpdateRelease(DataTable pdstData, int pintLocationID, int pintBinID)
		{
			const string METHOD_NAME = THIS + ".UpdateRelease()";
			
			InventoryUtilsBO boIVUtils = new InventoryUtilsBO();
			SOReleaseOrderBO boRelease = new SOReleaseOrderBO();
			UtilsBO boUtils = new UtilsBO();
			DateTime dtmServerDate = boUtils.GetDBDate();
			DataTable dtbAvailable = boIVUtils.GetAvailableQtyByPostDate(dtmServerDate);
			// commit detail schema only
			DataSet dstCommitDetail = boRelease.List(0);
			DataSet dtbBinCache = boIVUtils.ListAllBinCache();
			DataSet dtbLocCache = boIVUtils.ListAllLocationCache();
			DataSet dtbMasLocCache = boIVUtils.ListAllMasLocCache();
			// get transaction history table schema
			DataSet dstTransaction = boIVUtils.ListTransactionHistory(0);
			ArrayList arrReleasedSO = new ArrayList();
			int intTranTypeID = boUtils.GetTransTypeIDByCode(TransactionTypeEnum.SOCommitment.ToString());
			int intMasterID = -1;
			ArrayList arrMaster = new ArrayList();
			foreach(DataRow drowData in pdstData.Rows)
			{
				int intSaleOrderMasterID = int.Parse(drowData[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString());
				if (arrReleasedSO.Contains(intSaleOrderMasterID))
					continue;
				arrReleasedSO.Add(intSaleOrderMasterID);
				
				SO_CommitInventoryMasterVO voMaster = new SO_CommitInventoryMasterVO();
				// CommitInventoryMaster
				voMaster.SaleOrderMasterID = intSaleOrderMasterID;
				voMaster.CommitDate = dtmServerDate;
				voMaster.Username = SystemProperty.UserName;
				voMaster.CommitInventoryMasterID = intMasterID;
				arrMaster.Add(voMaster);
				intMasterID = intMasterID - 1;
				
				int intMasterLocationID = Convert.ToInt32(drowData[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD]);

				DataRow[] drowDetails = pdstData.Select(SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + "=" + intSaleOrderMasterID);
				int intLine = 1;
				foreach (DataRow drowDetail in drowDetails)
				{
					// Update Inventory Cache
					decimal decDeliveryQuantity = decimal.Parse(drowDetail[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
					int intProductID = int.Parse(drowDetail[ITM_ProductTable.PRODUCTID_FLD].ToString());
					
					#region check available quantity

					string strFilter = IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + intMasterLocationID
						+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
						+ " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID
						+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "=" + intProductID;
					string strLocFilter = IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + intMasterLocationID
						+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
						+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "=" + intProductID;
					string strMasLocFilter = IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + intMasterLocationID
						+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "=" + intProductID;
					decimal decAvailQty = 0, decUMRate = 0;
					try
					{
						decAvailQty = Convert.ToDecimal(dtbAvailable.Compute("SUM(AvailableQuantity)", strFilter));
					}
					catch{}
					try
					{
						decUMRate = Convert.ToDecimal(drowData[SO_CommitInventoryDetailTable.UMRATE_FLD]);
					}
					catch{}

					if(decAvailQty < decDeliveryQuantity*decUMRate)
					{
						Hashtable hstIndex = new Hashtable();
						hstIndex.Add(SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD, int.Parse(drowData[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString()));
						throw new PCSBOException(ErrorCode.MSG_WOISSUE_MATERIAL_COMMIT_QTY_DONOT_MEET_AVAILABLE_QTY,METHOD_NAME, new Exception(), hstIndex);
					}
					
					#endregion

					#region CommitInventoryDetail

					DataRow drowCommitDetail = dstCommitDetail.Tables[0].NewRow();
					drowCommitDetail[SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD] = voMaster.CommitInventoryMasterID;
					drowCommitDetail[SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD] = drowDetail[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD];
					drowCommitDetail[SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD] = intMasterLocationID;
					drowCommitDetail[SO_CommitInventoryDetailTable.LINE_FLD] = intLine;
					drowCommitDetail[SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD] = decDeliveryQuantity; 
					drowCommitDetail[SO_CommitInventoryDetailTable.PRODUCTID_FLD] = drowDetail[ITM_ProductTable.PRODUCTID_FLD];
					drowCommitDetail[SO_CommitInventoryDetailTable.BINID_FLD] = pintBinID;
					drowCommitDetail[SO_CommitInventoryDetailTable.LOCATIONID_FLD] = pintLocationID;
						
					if(drowData[SO_SaleOrderDetailTable.STOCKUMID_FLD] != DBNull.Value)
						drowCommitDetail[SO_CommitInventoryDetailTable.STOCKUMID_FLD] = drowDetail[SO_SaleOrderDetailTable.STOCKUMID_FLD];
					if(drowData[SO_SaleOrderDetailTable.SELLINGUMID_FLD] != DBNull.Value)
						drowCommitDetail[SO_CommitInventoryDetailTable.SELLINGUMID_FLD] = drowDetail[SO_SaleOrderDetailTable.SELLINGUMID_FLD];
					drowCommitDetail[SO_CommitInventoryDetailTable.UMRATE_FLD] = decUMRate;
					drowCommitDetail[SO_CommitInventoryDetailTable.CCNID_FLD] = drowDetail[ITM_ProductTable.CCNID_FLD];
					dstCommitDetail.Tables[0].Rows.Add(drowCommitDetail);

					#endregion

					#region Cache

					DataRow[] drowBinCache = dtbBinCache.Tables[0].Select(strFilter);
					DataRow[] drowLocCache = dtbLocCache.Tables[0].Select(strLocFilter);
					DataRow[] drowMasLocCache = dtbMasLocCache.Tables[0].Select(strMasLocFilter);
					decimal decBinCommit = 0, decLocCommit = 0, decMasLocCommit = 0;
					try
					{
						decBinCommit = Convert.ToDecimal(drowBinCache[0][IV_BinCacheTable.COMMITQUANTITY_FLD]);
					}
					catch{}
					try
					{
						decLocCommit = Convert.ToDecimal(drowLocCache[0][IV_LocationCacheTable.COMMITQUANTITY_FLD]);
					}
					catch{}
					try
					{
						decMasLocCommit = Convert.ToDecimal(drowMasLocCache[0][IV_MasLocCacheTable.COMMITQUANTITY_FLD]);
					}
					catch{}
					drowBinCache[0][IV_BinCacheTable.COMMITQUANTITY_FLD] = decBinCommit + decDeliveryQuantity*decUMRate;
					drowLocCache[0][IV_LocationCacheTable.COMMITQUANTITY_FLD] = decLocCommit + decDeliveryQuantity*decUMRate;
					drowMasLocCache[0][IV_MasLocCacheTable.COMMITQUANTITY_FLD] = decMasLocCommit + decDeliveryQuantity*decUMRate;

					#endregion

					#region Transaction history
					//SaveTransactionHistory
					DataRow drowTransaction = dstTransaction.Tables[0].NewRow();
					drowTransaction[MST_TransactionHistoryTable.CCNID_FLD] = drowDetail[ITM_ProductTable.CCNID_FLD];
					drowTransaction[MST_TransactionHistoryTable.TRANSDATE_FLD] = DateTime.Now;
					drowTransaction[MST_TransactionHistoryTable.POSTDATE_FLD] = dtmServerDate;
					drowTransaction[MST_TransactionHistoryTable.REFMASTERID_FLD] = voMaster.CommitInventoryMasterID;
					drowTransaction[MST_TransactionHistoryTable.REFDETAILID_FLD] = drowDetail[SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD];
					drowTransaction[MST_TransactionHistoryTable.PRODUCTID_FLD] = intProductID;
					drowTransaction[MST_TransactionHistoryTable.TRANTYPEID_FLD] = intTranTypeID;
					drowTransaction[MST_TransactionHistoryTable.USERNAME_FLD] = SystemProperty.UserName;
					drowTransaction[MST_TransactionHistoryTable.QUANTITY_FLD] = decDeliveryQuantity;
					drowTransaction[MST_TransactionHistoryTable.MASTERLOCATIONID_FLD] = intMasterLocationID;
					decimal decOHQuantity = GetQuantityFromCache(dtbMasLocCache.Tables[0], intMasterLocationID, intProductID, 1);
					drowTransaction[MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD] = decOHQuantity;
					drowTransaction[MST_TransactionHistoryTable.MASLOCCOMMITQUANTITY_FLD] = decMasLocCommit;
					drowTransaction[MST_TransactionHistoryTable.LOCATIONID_FLD] = pintLocationID;
					decOHQuantity = GetQuantityFromCache(dtbLocCache.Tables[0], pintLocationID, intProductID, 2);
					drowTransaction[MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD] = decOHQuantity;
					drowTransaction[MST_TransactionHistoryTable.LOCATIONCOMMITQUANTITY_FLD] = decLocCommit;
					drowTransaction[MST_TransactionHistoryTable.BINID_FLD] = pintBinID;
					decOHQuantity = GetQuantityFromCache(dtbBinCache.Tables[0], pintBinID, intProductID, 3);
					drowTransaction[MST_TransactionHistoryTable.BINOHQUANTITY_FLD] = decOHQuantity;
					drowTransaction[MST_TransactionHistoryTable.BINCOMMITQUANTITY_FLD] = decBinCommit;
					drowTransaction[MST_TransactionHistoryTable.STOCKUMID_FLD] = drowDetail[ITM_ProductTable.STOCKUMID_FLD];
					dstTransaction.Tables[0].Rows.Add(drowTransaction);
					#endregion
				}
			}

			boRelease.UpdateRelease(arrMaster, dstCommitDetail, dtbMasLocCache, dtbLocCache, dtbBinCache, dstTransaction);
		}
		/// <summary>
		/// Get quantity from cache table
		/// </summary>
		/// <param name="pdtbCacheData">Cache Table</param>
		/// <param name="pintID">Cache ID</param>
		/// <param name="pintProductID"></param>
		/// <param name="pintType">1: Master Location | 2: Location | 3: Bin</param>
		/// <returns>Onhand Quantity</returns>
		private decimal GetQuantityFromCache(DataTable pdtbCacheData, int pintID, int pintProductID, int pintType)
		{
			try
			{
				string strFilter = ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID;
				switch(pintType)
				{
					case 1: // master location
						strFilter += " AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintID;
						break;
					case 2: // location
						strFilter += " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintID;
						break;
					default: // bin
						strFilter += " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintID;
						break;
				}
				return Convert.ToDecimal(pdtbCacheData.Compute("SUM(" + IV_BinCacheTable.OHQUANTITY_FLD + ")", strFilter));
			}
			catch
			{
				return 0;
			}
		}
	}
}