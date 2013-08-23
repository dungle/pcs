using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComMaterials.Inventory.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Utils;
using PCSUtils.Log;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComSale.Order.BO;
using PCSComSale.Order.DS;

using PCSComUtils.PCSExc;
namespace PCSSale.Order
{
	/// <summary>
	/// Summary description for SOReturnGoodsReceive.
	/// </summary>
	public class SOReturnGoodsReceive : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnPrint;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid tgridViewData;
		private System.Windows.Forms.TextBox txtReturnedGoodsNumber;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.TextBox txtSaleOrderNo;
		private System.Windows.Forms.TextBox txtCustomerCode;
		private System.Windows.Forms.TextBox txtCustomerName;

		private EnumAction enumAction ;	
		private const string THIS = "PCSSale.Order.SOReturnGoodsReceive";
		private int intReturnedGoodsMasterID;
		private System.Windows.Forms.Button btnSearchSaleOrder;
		private System.Windows.Forms.Button btnSearchReturnedGoods;
		private System.Windows.Forms.Button btnSearchCustomerByCode;
		private System.Windows.Forms.Button btnSearchCustomerByName;
		private C1.Win.C1Input.C1DateEdit dtmEntryDate;
		private const string VIEW_SO_RETURNED_NAME = "V_SOTORETURNED";
		private const string VIEW_PRODUCT_FOR_CUSTOMER_NAME = "V_ProductForCustomer";
		private const string VIEW_ITEM_OF_SO = "v_GetSaleOrderTotalInvCommit";
		private const string VIEW_CONFIRMSHIPBYCUSTOMER = "v_ConfirmShipByCustomer";


		private DataSet dsReturnedGoodsDetail = null;
		private System.Windows.Forms.Label lblCustomerID;
		private System.Windows.Forms.Label lblSaleOrderMasterID;
		private DataTable dtbGridLayout;
		private bool blnHasError = false;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		const string GRID_COL_NAME ="NAME";
		const string GRID_COL_CAPTION = "CAPTION";
		const string GRID_COL_WIDTH = "WIDTH";
		private DataTable dtGridDesign;
		private DataTable dtSaleOrderTotalCommit; //this table will store all the 

		//private string strTotalCommitColumnName = "Balance";
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox txtMasLoc;
		private System.Windows.Forms.Button btnSearchMasLoc;
		private string V_VENDORCUSTOMER = "V_VendorCustomer";
		private System.Windows.Forms.TextBox txtCustomerLoc;
		private System.Windows.Forms.Button btnCustomerLoc;
		private System.Windows.Forms.Label lblCustomerLoc;
		private System.Windows.Forms.Label lblCustomer;
		private System.Windows.Forms.TextBox txtContact;
		private System.Windows.Forms.Button btnContact;
		private System.Windows.Forms.Label lblContact;
		private System.Windows.Forms.Label lblRGR;
		private System.Windows.Forms.Button btnHelp;
		private string CUSTOMER = "Customer";
		private UtilsBO boUtils = new UtilsBO();
		int intOldUMID = 0, intStockUMID = 0;
		private System.Windows.Forms.TextBox txtCurrency;
		private System.Windows.Forms.Button btnCurrency;
		private C1.Win.C1Input.C1NumericEdit txtExchRate;
		private System.Windows.Forms.Label lblExchRate;
		private System.Windows.Forms.Label lblCurrency;
	
		public SOReturnGoodsReceive()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			tgridViewData.Focus();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SOReturnGoodsReceive));
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.tgridViewData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.lblContact = new System.Windows.Forms.Label();
			this.lblCustomerLoc = new System.Windows.Forms.Label();
			this.lblRGR = new System.Windows.Forms.Label();
			this.btnSearchSaleOrder = new System.Windows.Forms.Button();
			this.txtSaleOrderNo = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtCustomerCode = new System.Windows.Forms.TextBox();
			this.lblCustomer = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSearchReturnedGoods = new System.Windows.Forms.Button();
			this.txtReturnedGoodsNumber = new System.Windows.Forms.TextBox();
			this.txtCustomerName = new System.Windows.Forms.TextBox();
			this.btnSearchCustomerByCode = new System.Windows.Forms.Button();
			this.btnSearchCustomerByName = new System.Windows.Forms.Button();
			this.dtmEntryDate = new C1.Win.C1Input.C1DateEdit();
			this.lblCustomerID = new System.Windows.Forms.Label();
			this.lblSaleOrderMasterID = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.txtMasLoc = new System.Windows.Forms.TextBox();
			this.btnSearchMasLoc = new System.Windows.Forms.Button();
			this.txtCustomerLoc = new System.Windows.Forms.TextBox();
			this.btnCustomerLoc = new System.Windows.Forms.Button();
			this.txtContact = new System.Windows.Forms.TextBox();
			this.btnContact = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.txtCurrency = new System.Windows.Forms.TextBox();
			this.btnCurrency = new System.Windows.Forms.Button();
			this.txtExchRate = new C1.Win.C1Input.C1NumericEdit();
			this.lblExchRate = new System.Windows.Forms.Label();
			this.lblCurrency = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tgridViewData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmEntryDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtExchRate)).BeginInit();
			this.SuspendLayout();
			// 
			// cboCCN
			// 
			this.cboCCN.AccessibleDescription = resources.GetString("cboCCN.AccessibleDescription");
			this.cboCCN.AccessibleName = resources.GetString("cboCCN.AccessibleName");
			this.cboCCN.AddItemSeparator = ';';
			this.cboCCN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboCCN.Anchor")));
			this.cboCCN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboCCN.BackgroundImage")));
			this.cboCCN.Caption = "";
			this.cboCCN.CaptionHeight = 17;
			this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboCCN.ColumnCaptionHeight = 17;
			this.cboCCN.ColumnFooterHeight = 17;
			this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboCCN.ContentHeight = 15;
			this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboCCN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboCCN.Dock")));
			this.cboCCN.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown;
			this.cboCCN.DropDownWidth = 200;
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.Enabled = ((bool)(resources.GetObject("cboCCN.Enabled")));
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.Font = ((System.Drawing.Font)(resources.GetObject("cboCCN.Font")));
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboCCN.ImeMode")));
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = ((System.Drawing.Point)(resources.GetObject("cboCCN.Location")));
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboCCN.RightToLeft")));
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = ((System.Drawing.Size)(resources.GetObject("cboCCN.Size")));
			this.cboCCN.TabIndex = ((int)(resources.GetObject("cboCCN.TabIndex")));
			this.cboCCN.Text = resources.GetString("cboCCN.Text");
			this.cboCCN.Visible = ((bool)(resources.GetObject("cboCCN.Visible")));
			this.cboCCN.SelectedValueChanged += new System.EventHandler(this.cboCCN_SelectedValueChanged);
			this.cboCCN.Enter += new System.EventHandler(this.cboCCN_Enter);
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
			// btnEdit
			// 
			this.btnEdit.AccessibleDescription = resources.GetString("btnEdit.AccessibleDescription");
			this.btnEdit.AccessibleName = resources.GetString("btnEdit.AccessibleName");
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnEdit.Anchor")));
			this.btnEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.BackgroundImage")));
			this.btnEdit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnEdit.Dock")));
			this.btnEdit.Enabled = ((bool)(resources.GetObject("btnEdit.Enabled")));
			this.btnEdit.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnEdit.FlatStyle")));
			this.btnEdit.Font = ((System.Drawing.Font)(resources.GetObject("btnEdit.Font")));
			this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
			this.btnEdit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEdit.ImageAlign")));
			this.btnEdit.ImageIndex = ((int)(resources.GetObject("btnEdit.ImageIndex")));
			this.btnEdit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnEdit.ImeMode")));
			this.btnEdit.Location = ((System.Drawing.Point)(resources.GetObject("btnEdit.Location")));
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnEdit.RightToLeft")));
			this.btnEdit.Size = ((System.Drawing.Size)(resources.GetObject("btnEdit.Size")));
			this.btnEdit.TabIndex = ((int)(resources.GetObject("btnEdit.TabIndex")));
			this.btnEdit.Text = resources.GetString("btnEdit.Text");
			this.btnEdit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEdit.TextAlign")));
			this.btnEdit.Visible = ((bool)(resources.GetObject("btnEdit.Visible")));
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = resources.GetString("btnDelete.AccessibleDescription");
			this.btnDelete.AccessibleName = resources.GetString("btnDelete.AccessibleName");
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDelete.Anchor")));
			this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
			this.btnDelete.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDelete.Dock")));
			this.btnDelete.Enabled = ((bool)(resources.GetObject("btnDelete.Enabled")));
			this.btnDelete.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDelete.FlatStyle")));
			this.btnDelete.Font = ((System.Drawing.Font)(resources.GetObject("btnDelete.Font")));
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.ImageAlign")));
			this.btnDelete.ImageIndex = ((int)(resources.GetObject("btnDelete.ImageIndex")));
			this.btnDelete.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDelete.ImeMode")));
			this.btnDelete.Location = ((System.Drawing.Point)(resources.GetObject("btnDelete.Location")));
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDelete.RightToLeft")));
			this.btnDelete.Size = ((System.Drawing.Size)(resources.GetObject("btnDelete.Size")));
			this.btnDelete.TabIndex = ((int)(resources.GetObject("btnDelete.TabIndex")));
			this.btnDelete.Text = resources.GetString("btnDelete.Text");
			this.btnDelete.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.TextAlign")));
			this.btnDelete.Visible = ((bool)(resources.GetObject("btnDelete.Visible")));
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.AccessibleDescription = resources.GetString("btnSave.AccessibleDescription");
			this.btnSave.AccessibleName = resources.GetString("btnSave.AccessibleName");
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSave.Anchor")));
			this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
			this.btnSave.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSave.Dock")));
			this.btnSave.Enabled = ((bool)(resources.GetObject("btnSave.Enabled")));
			this.btnSave.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSave.FlatStyle")));
			this.btnSave.Font = ((System.Drawing.Font)(resources.GetObject("btnSave.Font")));
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSave.ImageAlign")));
			this.btnSave.ImageIndex = ((int)(resources.GetObject("btnSave.ImageIndex")));
			this.btnSave.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSave.ImeMode")));
			this.btnSave.Location = ((System.Drawing.Point)(resources.GetObject("btnSave.Location")));
			this.btnSave.Name = "btnSave";
			this.btnSave.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSave.RightToLeft")));
			this.btnSave.Size = ((System.Drawing.Size)(resources.GetObject("btnSave.Size")));
			this.btnSave.TabIndex = ((int)(resources.GetObject("btnSave.TabIndex")));
			this.btnSave.Text = resources.GetString("btnSave.Text");
			this.btnSave.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSave.TextAlign")));
			this.btnSave.Visible = ((bool)(resources.GetObject("btnSave.Visible")));
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.AccessibleDescription = resources.GetString("btnAdd.AccessibleDescription");
			this.btnAdd.AccessibleName = resources.GetString("btnAdd.AccessibleName");
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAdd.Anchor")));
			this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
			this.btnAdd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAdd.Dock")));
			this.btnAdd.Enabled = ((bool)(resources.GetObject("btnAdd.Enabled")));
			this.btnAdd.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAdd.FlatStyle")));
			this.btnAdd.Font = ((System.Drawing.Font)(resources.GetObject("btnAdd.Font")));
			this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.ImageAlign")));
			this.btnAdd.ImageIndex = ((int)(resources.GetObject("btnAdd.ImageIndex")));
			this.btnAdd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAdd.ImeMode")));
			this.btnAdd.Location = ((System.Drawing.Point)(resources.GetObject("btnAdd.Location")));
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAdd.RightToLeft")));
			this.btnAdd.Size = ((System.Drawing.Size)(resources.GetObject("btnAdd.Size")));
			this.btnAdd.TabIndex = ((int)(resources.GetObject("btnAdd.TabIndex")));
			this.btnAdd.Text = resources.GetString("btnAdd.Text");
			this.btnAdd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.TextAlign")));
			this.btnAdd.Visible = ((bool)(resources.GetObject("btnAdd.Visible")));
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.AccessibleDescription = resources.GetString("btnPrint.AccessibleDescription");
			this.btnPrint.AccessibleName = resources.GetString("btnPrint.AccessibleName");
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPrint.Anchor")));
			this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
			this.btnPrint.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPrint.Dock")));
			this.btnPrint.Enabled = ((bool)(resources.GetObject("btnPrint.Enabled")));
			this.btnPrint.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPrint.FlatStyle")));
			this.btnPrint.Font = ((System.Drawing.Font)(resources.GetObject("btnPrint.Font")));
			this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
			this.btnPrint.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPrint.ImageAlign")));
			this.btnPrint.ImageIndex = ((int)(resources.GetObject("btnPrint.ImageIndex")));
			this.btnPrint.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPrint.ImeMode")));
			this.btnPrint.Location = ((System.Drawing.Point)(resources.GetObject("btnPrint.Location")));
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPrint.RightToLeft")));
			this.btnPrint.Size = ((System.Drawing.Size)(resources.GetObject("btnPrint.Size")));
			this.btnPrint.TabIndex = ((int)(resources.GetObject("btnPrint.TabIndex")));
			this.btnPrint.Text = resources.GetString("btnPrint.Text");
			this.btnPrint.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPrint.TextAlign")));
			this.btnPrint.Visible = ((bool)(resources.GetObject("btnPrint.Visible")));
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// tgridViewData
			// 
			this.tgridViewData.AccessibleDescription = resources.GetString("tgridViewData.AccessibleDescription");
			this.tgridViewData.AccessibleName = resources.GetString("tgridViewData.AccessibleName");
			this.tgridViewData.AllowAddNew = ((bool)(resources.GetObject("tgridViewData.AllowAddNew")));
			this.tgridViewData.AllowArrows = ((bool)(resources.GetObject("tgridViewData.AllowArrows")));
			this.tgridViewData.AllowColMove = ((bool)(resources.GetObject("tgridViewData.AllowColMove")));
			this.tgridViewData.AllowColSelect = ((bool)(resources.GetObject("tgridViewData.AllowColSelect")));
			this.tgridViewData.AllowDelete = ((bool)(resources.GetObject("tgridViewData.AllowDelete")));
			this.tgridViewData.AllowDrag = ((bool)(resources.GetObject("tgridViewData.AllowDrag")));
			this.tgridViewData.AllowFilter = ((bool)(resources.GetObject("tgridViewData.AllowFilter")));
			this.tgridViewData.AllowHorizontalSplit = ((bool)(resources.GetObject("tgridViewData.AllowHorizontalSplit")));
			this.tgridViewData.AllowRowSelect = ((bool)(resources.GetObject("tgridViewData.AllowRowSelect")));
			this.tgridViewData.AllowSort = ((bool)(resources.GetObject("tgridViewData.AllowSort")));
			this.tgridViewData.AllowUpdate = ((bool)(resources.GetObject("tgridViewData.AllowUpdate")));
			this.tgridViewData.AllowUpdateOnBlur = ((bool)(resources.GetObject("tgridViewData.AllowUpdateOnBlur")));
			this.tgridViewData.AllowVerticalSplit = ((bool)(resources.GetObject("tgridViewData.AllowVerticalSplit")));
			this.tgridViewData.AlternatingRows = ((bool)(resources.GetObject("tgridViewData.AlternatingRows")));
			this.tgridViewData.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tgridViewData.Anchor")));
			this.tgridViewData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tgridViewData.BackgroundImage")));
			this.tgridViewData.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("tgridViewData.BorderStyle")));
			this.tgridViewData.Caption = resources.GetString("tgridViewData.Caption");
			this.tgridViewData.CaptionHeight = ((int)(resources.GetObject("tgridViewData.CaptionHeight")));
			this.tgridViewData.CellTipsDelay = ((int)(resources.GetObject("tgridViewData.CellTipsDelay")));
			this.tgridViewData.CellTipsWidth = ((int)(resources.GetObject("tgridViewData.CellTipsWidth")));
			this.tgridViewData.ChildGrid = ((C1.Win.C1TrueDBGrid.C1TrueDBGrid)(resources.GetObject("tgridViewData.ChildGrid")));
			this.tgridViewData.CollapseColor = ((System.Drawing.Color)(resources.GetObject("tgridViewData.CollapseColor")));
			this.tgridViewData.ColumnFooters = ((bool)(resources.GetObject("tgridViewData.ColumnFooters")));
			this.tgridViewData.ColumnHeaders = ((bool)(resources.GetObject("tgridViewData.ColumnHeaders")));
			this.tgridViewData.DefColWidth = ((int)(resources.GetObject("tgridViewData.DefColWidth")));
			this.tgridViewData.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tgridViewData.Dock")));
			this.tgridViewData.EditDropDown = ((bool)(resources.GetObject("tgridViewData.EditDropDown")));
			this.tgridViewData.EmptyRows = ((bool)(resources.GetObject("tgridViewData.EmptyRows")));
			this.tgridViewData.Enabled = ((bool)(resources.GetObject("tgridViewData.Enabled")));
			this.tgridViewData.ExpandColor = ((System.Drawing.Color)(resources.GetObject("tgridViewData.ExpandColor")));
			this.tgridViewData.ExposeCellMode = ((C1.Win.C1TrueDBGrid.ExposeCellModeEnum)(resources.GetObject("tgridViewData.ExposeCellMode")));
			this.tgridViewData.ExtendRightColumn = ((bool)(resources.GetObject("tgridViewData.ExtendRightColumn")));
			this.tgridViewData.FetchRowStyles = ((bool)(resources.GetObject("tgridViewData.FetchRowStyles")));
			this.tgridViewData.FilterBar = ((bool)(resources.GetObject("tgridViewData.FilterBar")));
			this.tgridViewData.FlatStyle = ((C1.Win.C1TrueDBGrid.FlatModeEnum)(resources.GetObject("tgridViewData.FlatStyle")));
			this.tgridViewData.Font = ((System.Drawing.Font)(resources.GetObject("tgridViewData.Font")));
			this.tgridViewData.GroupByAreaVisible = ((bool)(resources.GetObject("tgridViewData.GroupByAreaVisible")));
			this.tgridViewData.GroupByCaption = resources.GetString("tgridViewData.GroupByCaption");
			this.tgridViewData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.tgridViewData.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tgridViewData.ImeMode")));
			this.tgridViewData.LinesPerRow = ((int)(resources.GetObject("tgridViewData.LinesPerRow")));
			this.tgridViewData.Location = ((System.Drawing.Point)(resources.GetObject("tgridViewData.Location")));
			this.tgridViewData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.tgridViewData.Name = "tgridViewData";
			this.tgridViewData.PictureAddnewRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureAddnewRow")));
			this.tgridViewData.PictureCurrentRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureCurrentRow")));
			this.tgridViewData.PictureFilterBar = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureFilterBar")));
			this.tgridViewData.PictureFooterRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureFooterRow")));
			this.tgridViewData.PictureHeaderRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureHeaderRow")));
			this.tgridViewData.PictureModifiedRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureModifiedRow")));
			this.tgridViewData.PictureStandardRow = ((System.Drawing.Image)(resources.GetObject("tgridViewData.PictureStandardRow")));
			this.tgridViewData.PreviewInfo.AllowSizing = ((bool)(resources.GetObject("tgridViewData.PreviewInfo.AllowSizing")));
			this.tgridViewData.PreviewInfo.Caption = resources.GetString("tgridViewData.PreviewInfo.Caption");
			this.tgridViewData.PreviewInfo.Location = ((System.Drawing.Point)(resources.GetObject("tgridViewData.PreviewInfo.Location")));
			this.tgridViewData.PreviewInfo.Size = ((System.Drawing.Size)(resources.GetObject("tgridViewData.PreviewInfo.Size")));
			this.tgridViewData.PreviewInfo.ToolBars = ((bool)(resources.GetObject("tgridViewData.PreviewInfo.ToolBars")));
			this.tgridViewData.PreviewInfo.UIStrings.Content = ((string[])(resources.GetObject("tgridViewData.PreviewInfo.UIStrings.Content")));
			this.tgridViewData.PreviewInfo.ZoomFactor = ((System.Double)(resources.GetObject("tgridViewData.PreviewInfo.ZoomFactor")));
			this.tgridViewData.PrintInfo.MaxRowHeight = ((int)(resources.GetObject("tgridViewData.PrintInfo.MaxRowHeight")));
			this.tgridViewData.PrintInfo.OwnerDrawPageFooter = ((bool)(resources.GetObject("tgridViewData.PrintInfo.OwnerDrawPageFooter")));
			this.tgridViewData.PrintInfo.OwnerDrawPageHeader = ((bool)(resources.GetObject("tgridViewData.PrintInfo.OwnerDrawPageHeader")));
			this.tgridViewData.PrintInfo.PageFooter = resources.GetString("tgridViewData.PrintInfo.PageFooter");
			this.tgridViewData.PrintInfo.PageFooterHeight = ((int)(resources.GetObject("tgridViewData.PrintInfo.PageFooterHeight")));
			this.tgridViewData.PrintInfo.PageHeader = resources.GetString("tgridViewData.PrintInfo.PageHeader");
			this.tgridViewData.PrintInfo.PageHeaderHeight = ((int)(resources.GetObject("tgridViewData.PrintInfo.PageHeaderHeight")));
			this.tgridViewData.PrintInfo.PrintHorizontalSplits = ((bool)(resources.GetObject("tgridViewData.PrintInfo.PrintHorizontalSplits")));
			this.tgridViewData.PrintInfo.ProgressCaption = resources.GetString("tgridViewData.PrintInfo.ProgressCaption");
			this.tgridViewData.PrintInfo.RepeatColumnFooters = ((bool)(resources.GetObject("tgridViewData.PrintInfo.RepeatColumnFooters")));
			this.tgridViewData.PrintInfo.RepeatColumnHeaders = ((bool)(resources.GetObject("tgridViewData.PrintInfo.RepeatColumnHeaders")));
			this.tgridViewData.PrintInfo.RepeatGridHeader = ((bool)(resources.GetObject("tgridViewData.PrintInfo.RepeatGridHeader")));
			this.tgridViewData.PrintInfo.RepeatSplitHeaders = ((bool)(resources.GetObject("tgridViewData.PrintInfo.RepeatSplitHeaders")));
			this.tgridViewData.PrintInfo.ShowOptionsDialog = ((bool)(resources.GetObject("tgridViewData.PrintInfo.ShowOptionsDialog")));
			this.tgridViewData.PrintInfo.ShowProgressForm = ((bool)(resources.GetObject("tgridViewData.PrintInfo.ShowProgressForm")));
			this.tgridViewData.PrintInfo.ShowSelection = ((bool)(resources.GetObject("tgridViewData.PrintInfo.ShowSelection")));
			this.tgridViewData.PrintInfo.UseGridColors = ((bool)(resources.GetObject("tgridViewData.PrintInfo.UseGridColors")));
			this.tgridViewData.RecordSelectors = ((bool)(resources.GetObject("tgridViewData.RecordSelectors")));
			this.tgridViewData.RecordSelectorWidth = ((int)(resources.GetObject("tgridViewData.RecordSelectorWidth")));
			this.tgridViewData.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tgridViewData.RightToLeft")));
			this.tgridViewData.RowDivider.Color = ((System.Drawing.Color)(resources.GetObject("resource.Color")));
			this.tgridViewData.RowDivider.Style = ((C1.Win.C1TrueDBGrid.LineStyleEnum)(resources.GetObject("resource.Style")));
			this.tgridViewData.RowHeight = ((int)(resources.GetObject("tgridViewData.RowHeight")));
			this.tgridViewData.RowSubDividerColor = ((System.Drawing.Color)(resources.GetObject("tgridViewData.RowSubDividerColor")));
			this.tgridViewData.ScrollTips = ((bool)(resources.GetObject("tgridViewData.ScrollTips")));
			this.tgridViewData.ScrollTrack = ((bool)(resources.GetObject("tgridViewData.ScrollTrack")));
			this.tgridViewData.Size = ((System.Drawing.Size)(resources.GetObject("tgridViewData.Size")));
			this.tgridViewData.SpringMode = ((bool)(resources.GetObject("tgridViewData.SpringMode")));
			this.tgridViewData.TabAcrossSplits = ((bool)(resources.GetObject("tgridViewData.TabAcrossSplits")));
			this.tgridViewData.TabIndex = ((int)(resources.GetObject("tgridViewData.TabIndex")));
			this.tgridViewData.Text = resources.GetString("tgridViewData.Text");
			this.tgridViewData.ViewCaptionWidth = ((int)(resources.GetObject("tgridViewData.ViewCaptionWidth")));
			this.tgridViewData.ViewColumnWidth = ((int)(resources.GetObject("tgridViewData.ViewColumnWidth")));
			this.tgridViewData.Visible = ((bool)(resources.GetObject("tgridViewData.Visible")));
			this.tgridViewData.WrapCellPointer = ((bool)(resources.GetObject("tgridViewData.WrapCellPointer")));
			this.tgridViewData.AfterDelete += new System.EventHandler(this.tgridViewData_AfterDelete);
			this.tgridViewData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.tgridViewData_BeforeColEdit);
			this.tgridViewData.Click += new System.EventHandler(this.tgridViewData_Click);
			this.tgridViewData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.tgridViewData_ButtonClick);
			this.tgridViewData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.tgridViewData_AfterColUpdate);
			this.tgridViewData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.tgridViewData_BeforeColUpdate);
			this.tgridViewData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tgridViewData_KeyDown);
			this.tgridViewData.OnAddNew += new System.EventHandler(this.tgridViewData_OnAddNew);
			this.tgridViewData.PropBag = resources.GetString("tgridViewData.PropBag");
			// 
			// lblContact
			// 
			this.lblContact.AccessibleDescription = resources.GetString("lblContact.AccessibleDescription");
			this.lblContact.AccessibleName = resources.GetString("lblContact.AccessibleName");
			this.lblContact.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblContact.Anchor")));
			this.lblContact.AutoSize = ((bool)(resources.GetObject("lblContact.AutoSize")));
			this.lblContact.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblContact.Dock")));
			this.lblContact.Enabled = ((bool)(resources.GetObject("lblContact.Enabled")));
			this.lblContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblContact.Font = ((System.Drawing.Font)(resources.GetObject("lblContact.Font")));
			this.lblContact.Image = ((System.Drawing.Image)(resources.GetObject("lblContact.Image")));
			this.lblContact.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblContact.ImageAlign")));
			this.lblContact.ImageIndex = ((int)(resources.GetObject("lblContact.ImageIndex")));
			this.lblContact.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblContact.ImeMode")));
			this.lblContact.Location = ((System.Drawing.Point)(resources.GetObject("lblContact.Location")));
			this.lblContact.Name = "lblContact";
			this.lblContact.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblContact.RightToLeft")));
			this.lblContact.Size = ((System.Drawing.Size)(resources.GetObject("lblContact.Size")));
			this.lblContact.TabIndex = ((int)(resources.GetObject("lblContact.TabIndex")));
			this.lblContact.Text = resources.GetString("lblContact.Text");
			this.lblContact.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblContact.TextAlign")));
			this.lblContact.Visible = ((bool)(resources.GetObject("lblContact.Visible")));
			// 
			// lblCustomerLoc
			// 
			this.lblCustomerLoc.AccessibleDescription = resources.GetString("lblCustomerLoc.AccessibleDescription");
			this.lblCustomerLoc.AccessibleName = resources.GetString("lblCustomerLoc.AccessibleName");
			this.lblCustomerLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCustomerLoc.Anchor")));
			this.lblCustomerLoc.AutoSize = ((bool)(resources.GetObject("lblCustomerLoc.AutoSize")));
			this.lblCustomerLoc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCustomerLoc.Dock")));
			this.lblCustomerLoc.Enabled = ((bool)(resources.GetObject("lblCustomerLoc.Enabled")));
			this.lblCustomerLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblCustomerLoc.Font = ((System.Drawing.Font)(resources.GetObject("lblCustomerLoc.Font")));
			this.lblCustomerLoc.ForeColor = System.Drawing.Color.Maroon;
			this.lblCustomerLoc.Image = ((System.Drawing.Image)(resources.GetObject("lblCustomerLoc.Image")));
			this.lblCustomerLoc.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCustomerLoc.ImageAlign")));
			this.lblCustomerLoc.ImageIndex = ((int)(resources.GetObject("lblCustomerLoc.ImageIndex")));
			this.lblCustomerLoc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCustomerLoc.ImeMode")));
			this.lblCustomerLoc.Location = ((System.Drawing.Point)(resources.GetObject("lblCustomerLoc.Location")));
			this.lblCustomerLoc.Name = "lblCustomerLoc";
			this.lblCustomerLoc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCustomerLoc.RightToLeft")));
			this.lblCustomerLoc.Size = ((System.Drawing.Size)(resources.GetObject("lblCustomerLoc.Size")));
			this.lblCustomerLoc.TabIndex = ((int)(resources.GetObject("lblCustomerLoc.TabIndex")));
			this.lblCustomerLoc.Text = resources.GetString("lblCustomerLoc.Text");
			this.lblCustomerLoc.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCustomerLoc.TextAlign")));
			this.lblCustomerLoc.Visible = ((bool)(resources.GetObject("lblCustomerLoc.Visible")));
			// 
			// lblRGR
			// 
			this.lblRGR.AccessibleDescription = resources.GetString("lblRGR.AccessibleDescription");
			this.lblRGR.AccessibleName = resources.GetString("lblRGR.AccessibleName");
			this.lblRGR.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblRGR.Anchor")));
			this.lblRGR.AutoSize = ((bool)(resources.GetObject("lblRGR.AutoSize")));
			this.lblRGR.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblRGR.Dock")));
			this.lblRGR.Enabled = ((bool)(resources.GetObject("lblRGR.Enabled")));
			this.lblRGR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblRGR.Font = ((System.Drawing.Font)(resources.GetObject("lblRGR.Font")));
			this.lblRGR.ForeColor = System.Drawing.Color.Maroon;
			this.lblRGR.Image = ((System.Drawing.Image)(resources.GetObject("lblRGR.Image")));
			this.lblRGR.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblRGR.ImageAlign")));
			this.lblRGR.ImageIndex = ((int)(resources.GetObject("lblRGR.ImageIndex")));
			this.lblRGR.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblRGR.ImeMode")));
			this.lblRGR.Location = ((System.Drawing.Point)(resources.GetObject("lblRGR.Location")));
			this.lblRGR.Name = "lblRGR";
			this.lblRGR.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblRGR.RightToLeft")));
			this.lblRGR.Size = ((System.Drawing.Size)(resources.GetObject("lblRGR.Size")));
			this.lblRGR.TabIndex = ((int)(resources.GetObject("lblRGR.TabIndex")));
			this.lblRGR.Text = resources.GetString("lblRGR.Text");
			this.lblRGR.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblRGR.TextAlign")));
			this.lblRGR.Visible = ((bool)(resources.GetObject("lblRGR.Visible")));
			// 
			// btnSearchSaleOrder
			// 
			this.btnSearchSaleOrder.AccessibleDescription = resources.GetString("btnSearchSaleOrder.AccessibleDescription");
			this.btnSearchSaleOrder.AccessibleName = resources.GetString("btnSearchSaleOrder.AccessibleName");
			this.btnSearchSaleOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearchSaleOrder.Anchor")));
			this.btnSearchSaleOrder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchSaleOrder.BackgroundImage")));
			this.btnSearchSaleOrder.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearchSaleOrder.Dock")));
			this.btnSearchSaleOrder.Enabled = ((bool)(resources.GetObject("btnSearchSaleOrder.Enabled")));
			this.btnSearchSaleOrder.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearchSaleOrder.FlatStyle")));
			this.btnSearchSaleOrder.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchSaleOrder.Font")));
			this.btnSearchSaleOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchSaleOrder.Image")));
			this.btnSearchSaleOrder.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchSaleOrder.ImageAlign")));
			this.btnSearchSaleOrder.ImageIndex = ((int)(resources.GetObject("btnSearchSaleOrder.ImageIndex")));
			this.btnSearchSaleOrder.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearchSaleOrder.ImeMode")));
			this.btnSearchSaleOrder.Location = ((System.Drawing.Point)(resources.GetObject("btnSearchSaleOrder.Location")));
			this.btnSearchSaleOrder.Name = "btnSearchSaleOrder";
			this.btnSearchSaleOrder.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearchSaleOrder.RightToLeft")));
			this.btnSearchSaleOrder.Size = ((System.Drawing.Size)(resources.GetObject("btnSearchSaleOrder.Size")));
			this.btnSearchSaleOrder.TabIndex = ((int)(resources.GetObject("btnSearchSaleOrder.TabIndex")));
			this.btnSearchSaleOrder.Text = resources.GetString("btnSearchSaleOrder.Text");
			this.btnSearchSaleOrder.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchSaleOrder.TextAlign")));
			this.btnSearchSaleOrder.Visible = ((bool)(resources.GetObject("btnSearchSaleOrder.Visible")));
			this.btnSearchSaleOrder.Click += new System.EventHandler(this.btnSearchSaleOrder_Click);
			// 
			// txtSaleOrderNo
			// 
			this.txtSaleOrderNo.AccessibleDescription = resources.GetString("txtSaleOrderNo.AccessibleDescription");
			this.txtSaleOrderNo.AccessibleName = resources.GetString("txtSaleOrderNo.AccessibleName");
			this.txtSaleOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtSaleOrderNo.Anchor")));
			this.txtSaleOrderNo.AutoSize = ((bool)(resources.GetObject("txtSaleOrderNo.AutoSize")));
			this.txtSaleOrderNo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtSaleOrderNo.BackgroundImage")));
			this.txtSaleOrderNo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtSaleOrderNo.Dock")));
			this.txtSaleOrderNo.Enabled = ((bool)(resources.GetObject("txtSaleOrderNo.Enabled")));
			this.txtSaleOrderNo.Font = ((System.Drawing.Font)(resources.GetObject("txtSaleOrderNo.Font")));
			this.txtSaleOrderNo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtSaleOrderNo.ImeMode")));
			this.txtSaleOrderNo.Location = ((System.Drawing.Point)(resources.GetObject("txtSaleOrderNo.Location")));
			this.txtSaleOrderNo.MaxLength = ((int)(resources.GetObject("txtSaleOrderNo.MaxLength")));
			this.txtSaleOrderNo.Multiline = ((bool)(resources.GetObject("txtSaleOrderNo.Multiline")));
			this.txtSaleOrderNo.Name = "txtSaleOrderNo";
			this.txtSaleOrderNo.PasswordChar = ((char)(resources.GetObject("txtSaleOrderNo.PasswordChar")));
			this.txtSaleOrderNo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtSaleOrderNo.RightToLeft")));
			this.txtSaleOrderNo.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtSaleOrderNo.ScrollBars")));
			this.txtSaleOrderNo.Size = ((System.Drawing.Size)(resources.GetObject("txtSaleOrderNo.Size")));
			this.txtSaleOrderNo.TabIndex = ((int)(resources.GetObject("txtSaleOrderNo.TabIndex")));
			this.txtSaleOrderNo.Text = resources.GetString("txtSaleOrderNo.Text");
			this.txtSaleOrderNo.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtSaleOrderNo.TextAlign")));
			this.txtSaleOrderNo.Visible = ((bool)(resources.GetObject("txtSaleOrderNo.Visible")));
			this.txtSaleOrderNo.WordWrap = ((bool)(resources.GetObject("txtSaleOrderNo.WordWrap")));
			this.txtSaleOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSaleOrderNo_KeyDown);
			this.txtSaleOrderNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtSaleOrderNo_Validating);
			// 
			// label7
			// 
			this.label7.AccessibleDescription = resources.GetString("label7.AccessibleDescription");
			this.label7.AccessibleName = resources.GetString("label7.AccessibleName");
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label7.Anchor")));
			this.label7.AutoSize = ((bool)(resources.GetObject("label7.AutoSize")));
			this.label7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label7.Dock")));
			this.label7.Enabled = ((bool)(resources.GetObject("label7.Enabled")));
			this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label7.Font = ((System.Drawing.Font)(resources.GetObject("label7.Font")));
			this.label7.ForeColor = System.Drawing.SystemColors.WindowText;
			this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
			this.label7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.ImageAlign")));
			this.label7.ImageIndex = ((int)(resources.GetObject("label7.ImageIndex")));
			this.label7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label7.ImeMode")));
			this.label7.Location = ((System.Drawing.Point)(resources.GetObject("label7.Location")));
			this.label7.Name = "label7";
			this.label7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label7.RightToLeft")));
			this.label7.Size = ((System.Drawing.Size)(resources.GetObject("label7.Size")));
			this.label7.TabIndex = ((int)(resources.GetObject("label7.TabIndex")));
			this.label7.Text = resources.GetString("label7.Text");
			this.label7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.TextAlign")));
			this.label7.Visible = ((bool)(resources.GetObject("label7.Visible")));
			// 
			// txtCustomerCode
			// 
			this.txtCustomerCode.AccessibleDescription = resources.GetString("txtCustomerCode.AccessibleDescription");
			this.txtCustomerCode.AccessibleName = resources.GetString("txtCustomerCode.AccessibleName");
			this.txtCustomerCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCustomerCode.Anchor")));
			this.txtCustomerCode.AutoSize = ((bool)(resources.GetObject("txtCustomerCode.AutoSize")));
			this.txtCustomerCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCustomerCode.BackgroundImage")));
			this.txtCustomerCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCustomerCode.Dock")));
			this.txtCustomerCode.Enabled = ((bool)(resources.GetObject("txtCustomerCode.Enabled")));
			this.txtCustomerCode.Font = ((System.Drawing.Font)(resources.GetObject("txtCustomerCode.Font")));
			this.txtCustomerCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCustomerCode.ImeMode")));
			this.txtCustomerCode.Location = ((System.Drawing.Point)(resources.GetObject("txtCustomerCode.Location")));
			this.txtCustomerCode.MaxLength = ((int)(resources.GetObject("txtCustomerCode.MaxLength")));
			this.txtCustomerCode.Multiline = ((bool)(resources.GetObject("txtCustomerCode.Multiline")));
			this.txtCustomerCode.Name = "txtCustomerCode";
			this.txtCustomerCode.PasswordChar = ((char)(resources.GetObject("txtCustomerCode.PasswordChar")));
			this.txtCustomerCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCustomerCode.RightToLeft")));
			this.txtCustomerCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCustomerCode.ScrollBars")));
			this.txtCustomerCode.Size = ((System.Drawing.Size)(resources.GetObject("txtCustomerCode.Size")));
			this.txtCustomerCode.TabIndex = ((int)(resources.GetObject("txtCustomerCode.TabIndex")));
			this.txtCustomerCode.Text = resources.GetString("txtCustomerCode.Text");
			this.txtCustomerCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCustomerCode.TextAlign")));
			this.txtCustomerCode.Visible = ((bool)(resources.GetObject("txtCustomerCode.Visible")));
			this.txtCustomerCode.WordWrap = ((bool)(resources.GetObject("txtCustomerCode.WordWrap")));
			this.txtCustomerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerCode_KeyDown);
			this.txtCustomerCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtCustomerCode_Validating);
			// 
			// lblCustomer
			// 
			this.lblCustomer.AccessibleDescription = resources.GetString("lblCustomer.AccessibleDescription");
			this.lblCustomer.AccessibleName = resources.GetString("lblCustomer.AccessibleName");
			this.lblCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCustomer.Anchor")));
			this.lblCustomer.AutoSize = ((bool)(resources.GetObject("lblCustomer.AutoSize")));
			this.lblCustomer.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCustomer.Dock")));
			this.lblCustomer.Enabled = ((bool)(resources.GetObject("lblCustomer.Enabled")));
			this.lblCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblCustomer.Font = ((System.Drawing.Font)(resources.GetObject("lblCustomer.Font")));
			this.lblCustomer.ForeColor = System.Drawing.Color.Maroon;
			this.lblCustomer.Image = ((System.Drawing.Image)(resources.GetObject("lblCustomer.Image")));
			this.lblCustomer.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCustomer.ImageAlign")));
			this.lblCustomer.ImageIndex = ((int)(resources.GetObject("lblCustomer.ImageIndex")));
			this.lblCustomer.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCustomer.ImeMode")));
			this.lblCustomer.Location = ((System.Drawing.Point)(resources.GetObject("lblCustomer.Location")));
			this.lblCustomer.Name = "lblCustomer";
			this.lblCustomer.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCustomer.RightToLeft")));
			this.lblCustomer.Size = ((System.Drawing.Size)(resources.GetObject("lblCustomer.Size")));
			this.lblCustomer.TabIndex = ((int)(resources.GetObject("lblCustomer.TabIndex")));
			this.lblCustomer.Text = resources.GetString("lblCustomer.Text");
			this.lblCustomer.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCustomer.TextAlign")));
			this.lblCustomer.Visible = ((bool)(resources.GetObject("lblCustomer.Visible")));
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.ForeColor = System.Drawing.Color.Maroon;
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// label4
			// 
			this.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription");
			this.label4.AccessibleName = resources.GetString("label4.AccessibleName");
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label4.Anchor")));
			this.label4.AutoSize = ((bool)(resources.GetObject("label4.AutoSize")));
			this.label4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label4.Dock")));
			this.label4.Enabled = ((bool)(resources.GetObject("label4.Enabled")));
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label4.Font = ((System.Drawing.Font)(resources.GetObject("label4.Font")));
			this.label4.ForeColor = System.Drawing.Color.Maroon;
			this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
			this.label4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.ImageAlign")));
			this.label4.ImageIndex = ((int)(resources.GetObject("label4.ImageIndex")));
			this.label4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label4.ImeMode")));
			this.label4.Location = ((System.Drawing.Point)(resources.GetObject("label4.Location")));
			this.label4.Name = "label4";
			this.label4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label4.RightToLeft")));
			this.label4.Size = ((System.Drawing.Size)(resources.GetObject("label4.Size")));
			this.label4.TabIndex = ((int)(resources.GetObject("label4.TabIndex")));
			this.label4.Text = resources.GetString("label4.Text");
			this.label4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.TextAlign")));
			this.label4.Visible = ((bool)(resources.GetObject("label4.Visible")));
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.ForeColor = System.Drawing.Color.Maroon;
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// btnSearchReturnedGoods
			// 
			this.btnSearchReturnedGoods.AccessibleDescription = resources.GetString("btnSearchReturnedGoods.AccessibleDescription");
			this.btnSearchReturnedGoods.AccessibleName = resources.GetString("btnSearchReturnedGoods.AccessibleName");
			this.btnSearchReturnedGoods.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearchReturnedGoods.Anchor")));
			this.btnSearchReturnedGoods.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchReturnedGoods.BackgroundImage")));
			this.btnSearchReturnedGoods.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearchReturnedGoods.Dock")));
			this.btnSearchReturnedGoods.Enabled = ((bool)(resources.GetObject("btnSearchReturnedGoods.Enabled")));
			this.btnSearchReturnedGoods.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearchReturnedGoods.FlatStyle")));
			this.btnSearchReturnedGoods.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchReturnedGoods.Font")));
			this.btnSearchReturnedGoods.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchReturnedGoods.Image")));
			this.btnSearchReturnedGoods.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchReturnedGoods.ImageAlign")));
			this.btnSearchReturnedGoods.ImageIndex = ((int)(resources.GetObject("btnSearchReturnedGoods.ImageIndex")));
			this.btnSearchReturnedGoods.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearchReturnedGoods.ImeMode")));
			this.btnSearchReturnedGoods.Location = ((System.Drawing.Point)(resources.GetObject("btnSearchReturnedGoods.Location")));
			this.btnSearchReturnedGoods.Name = "btnSearchReturnedGoods";
			this.btnSearchReturnedGoods.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearchReturnedGoods.RightToLeft")));
			this.btnSearchReturnedGoods.Size = ((System.Drawing.Size)(resources.GetObject("btnSearchReturnedGoods.Size")));
			this.btnSearchReturnedGoods.TabIndex = ((int)(resources.GetObject("btnSearchReturnedGoods.TabIndex")));
			this.btnSearchReturnedGoods.Text = resources.GetString("btnSearchReturnedGoods.Text");
			this.btnSearchReturnedGoods.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchReturnedGoods.TextAlign")));
			this.btnSearchReturnedGoods.Visible = ((bool)(resources.GetObject("btnSearchReturnedGoods.Visible")));
			this.btnSearchReturnedGoods.Click += new System.EventHandler(this.btnSearchReturnedGoods_Click);
			// 
			// txtReturnedGoodsNumber
			// 
			this.txtReturnedGoodsNumber.AccessibleDescription = resources.GetString("txtReturnedGoodsNumber.AccessibleDescription");
			this.txtReturnedGoodsNumber.AccessibleName = resources.GetString("txtReturnedGoodsNumber.AccessibleName");
			this.txtReturnedGoodsNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtReturnedGoodsNumber.Anchor")));
			this.txtReturnedGoodsNumber.AutoSize = ((bool)(resources.GetObject("txtReturnedGoodsNumber.AutoSize")));
			this.txtReturnedGoodsNumber.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtReturnedGoodsNumber.BackgroundImage")));
			this.txtReturnedGoodsNumber.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtReturnedGoodsNumber.Dock")));
			this.txtReturnedGoodsNumber.Enabled = ((bool)(resources.GetObject("txtReturnedGoodsNumber.Enabled")));
			this.txtReturnedGoodsNumber.Font = ((System.Drawing.Font)(resources.GetObject("txtReturnedGoodsNumber.Font")));
			this.txtReturnedGoodsNumber.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtReturnedGoodsNumber.ImeMode")));
			this.txtReturnedGoodsNumber.Location = ((System.Drawing.Point)(resources.GetObject("txtReturnedGoodsNumber.Location")));
			this.txtReturnedGoodsNumber.MaxLength = ((int)(resources.GetObject("txtReturnedGoodsNumber.MaxLength")));
			this.txtReturnedGoodsNumber.Multiline = ((bool)(resources.GetObject("txtReturnedGoodsNumber.Multiline")));
			this.txtReturnedGoodsNumber.Name = "txtReturnedGoodsNumber";
			this.txtReturnedGoodsNumber.PasswordChar = ((char)(resources.GetObject("txtReturnedGoodsNumber.PasswordChar")));
			this.txtReturnedGoodsNumber.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtReturnedGoodsNumber.RightToLeft")));
			this.txtReturnedGoodsNumber.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtReturnedGoodsNumber.ScrollBars")));
			this.txtReturnedGoodsNumber.Size = ((System.Drawing.Size)(resources.GetObject("txtReturnedGoodsNumber.Size")));
			this.txtReturnedGoodsNumber.TabIndex = ((int)(resources.GetObject("txtReturnedGoodsNumber.TabIndex")));
			this.txtReturnedGoodsNumber.Text = resources.GetString("txtReturnedGoodsNumber.Text");
			this.txtReturnedGoodsNumber.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtReturnedGoodsNumber.TextAlign")));
			this.txtReturnedGoodsNumber.Visible = ((bool)(resources.GetObject("txtReturnedGoodsNumber.Visible")));
			this.txtReturnedGoodsNumber.WordWrap = ((bool)(resources.GetObject("txtReturnedGoodsNumber.WordWrap")));
			this.txtReturnedGoodsNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReturnedGoodsNumber_KeyDown);
			this.txtReturnedGoodsNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtReturnedGoodsNumber_Validating);
			// 
			// txtCustomerName
			// 
			this.txtCustomerName.AccessibleDescription = resources.GetString("txtCustomerName.AccessibleDescription");
			this.txtCustomerName.AccessibleName = resources.GetString("txtCustomerName.AccessibleName");
			this.txtCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCustomerName.Anchor")));
			this.txtCustomerName.AutoSize = ((bool)(resources.GetObject("txtCustomerName.AutoSize")));
			this.txtCustomerName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCustomerName.BackgroundImage")));
			this.txtCustomerName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCustomerName.Dock")));
			this.txtCustomerName.Enabled = ((bool)(resources.GetObject("txtCustomerName.Enabled")));
			this.txtCustomerName.Font = ((System.Drawing.Font)(resources.GetObject("txtCustomerName.Font")));
			this.txtCustomerName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCustomerName.ImeMode")));
			this.txtCustomerName.Location = ((System.Drawing.Point)(resources.GetObject("txtCustomerName.Location")));
			this.txtCustomerName.MaxLength = ((int)(resources.GetObject("txtCustomerName.MaxLength")));
			this.txtCustomerName.Multiline = ((bool)(resources.GetObject("txtCustomerName.Multiline")));
			this.txtCustomerName.Name = "txtCustomerName";
			this.txtCustomerName.PasswordChar = ((char)(resources.GetObject("txtCustomerName.PasswordChar")));
			this.txtCustomerName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCustomerName.RightToLeft")));
			this.txtCustomerName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCustomerName.ScrollBars")));
			this.txtCustomerName.Size = ((System.Drawing.Size)(resources.GetObject("txtCustomerName.Size")));
			this.txtCustomerName.TabIndex = ((int)(resources.GetObject("txtCustomerName.TabIndex")));
			this.txtCustomerName.Text = resources.GetString("txtCustomerName.Text");
			this.txtCustomerName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCustomerName.TextAlign")));
			this.txtCustomerName.Visible = ((bool)(resources.GetObject("txtCustomerName.Visible")));
			this.txtCustomerName.WordWrap = ((bool)(resources.GetObject("txtCustomerName.WordWrap")));
			this.txtCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerName_KeyDown);
			this.txtCustomerName.Validating += new System.ComponentModel.CancelEventHandler(this.txtCustomerName_Validating);
			// 
			// btnSearchCustomerByCode
			// 
			this.btnSearchCustomerByCode.AccessibleDescription = resources.GetString("btnSearchCustomerByCode.AccessibleDescription");
			this.btnSearchCustomerByCode.AccessibleName = resources.GetString("btnSearchCustomerByCode.AccessibleName");
			this.btnSearchCustomerByCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearchCustomerByCode.Anchor")));
			this.btnSearchCustomerByCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchCustomerByCode.BackgroundImage")));
			this.btnSearchCustomerByCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearchCustomerByCode.Dock")));
			this.btnSearchCustomerByCode.Enabled = ((bool)(resources.GetObject("btnSearchCustomerByCode.Enabled")));
			this.btnSearchCustomerByCode.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearchCustomerByCode.FlatStyle")));
			this.btnSearchCustomerByCode.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchCustomerByCode.Font")));
			this.btnSearchCustomerByCode.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchCustomerByCode.Image")));
			this.btnSearchCustomerByCode.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchCustomerByCode.ImageAlign")));
			this.btnSearchCustomerByCode.ImageIndex = ((int)(resources.GetObject("btnSearchCustomerByCode.ImageIndex")));
			this.btnSearchCustomerByCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearchCustomerByCode.ImeMode")));
			this.btnSearchCustomerByCode.Location = ((System.Drawing.Point)(resources.GetObject("btnSearchCustomerByCode.Location")));
			this.btnSearchCustomerByCode.Name = "btnSearchCustomerByCode";
			this.btnSearchCustomerByCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearchCustomerByCode.RightToLeft")));
			this.btnSearchCustomerByCode.Size = ((System.Drawing.Size)(resources.GetObject("btnSearchCustomerByCode.Size")));
			this.btnSearchCustomerByCode.TabIndex = ((int)(resources.GetObject("btnSearchCustomerByCode.TabIndex")));
			this.btnSearchCustomerByCode.Text = resources.GetString("btnSearchCustomerByCode.Text");
			this.btnSearchCustomerByCode.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchCustomerByCode.TextAlign")));
			this.btnSearchCustomerByCode.Visible = ((bool)(resources.GetObject("btnSearchCustomerByCode.Visible")));
			this.btnSearchCustomerByCode.Click += new System.EventHandler(this.btnSearchCustomerByCode_Click);
			// 
			// btnSearchCustomerByName
			// 
			this.btnSearchCustomerByName.AccessibleDescription = resources.GetString("btnSearchCustomerByName.AccessibleDescription");
			this.btnSearchCustomerByName.AccessibleName = resources.GetString("btnSearchCustomerByName.AccessibleName");
			this.btnSearchCustomerByName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearchCustomerByName.Anchor")));
			this.btnSearchCustomerByName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchCustomerByName.BackgroundImage")));
			this.btnSearchCustomerByName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearchCustomerByName.Dock")));
			this.btnSearchCustomerByName.Enabled = ((bool)(resources.GetObject("btnSearchCustomerByName.Enabled")));
			this.btnSearchCustomerByName.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearchCustomerByName.FlatStyle")));
			this.btnSearchCustomerByName.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchCustomerByName.Font")));
			this.btnSearchCustomerByName.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchCustomerByName.Image")));
			this.btnSearchCustomerByName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchCustomerByName.ImageAlign")));
			this.btnSearchCustomerByName.ImageIndex = ((int)(resources.GetObject("btnSearchCustomerByName.ImageIndex")));
			this.btnSearchCustomerByName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearchCustomerByName.ImeMode")));
			this.btnSearchCustomerByName.Location = ((System.Drawing.Point)(resources.GetObject("btnSearchCustomerByName.Location")));
			this.btnSearchCustomerByName.Name = "btnSearchCustomerByName";
			this.btnSearchCustomerByName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearchCustomerByName.RightToLeft")));
			this.btnSearchCustomerByName.Size = ((System.Drawing.Size)(resources.GetObject("btnSearchCustomerByName.Size")));
			this.btnSearchCustomerByName.TabIndex = ((int)(resources.GetObject("btnSearchCustomerByName.TabIndex")));
			this.btnSearchCustomerByName.Text = resources.GetString("btnSearchCustomerByName.Text");
			this.btnSearchCustomerByName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchCustomerByName.TextAlign")));
			this.btnSearchCustomerByName.Visible = ((bool)(resources.GetObject("btnSearchCustomerByName.Visible")));
			this.btnSearchCustomerByName.Click += new System.EventHandler(this.btnSearchCustomerByName_Click);
			// 
			// dtmEntryDate
			// 
			this.dtmEntryDate.AcceptsEscape = ((bool)(resources.GetObject("dtmEntryDate.AcceptsEscape")));
			this.dtmEntryDate.AccessibleDescription = resources.GetString("dtmEntryDate.AccessibleDescription");
			this.dtmEntryDate.AccessibleName = resources.GetString("dtmEntryDate.AccessibleName");
			this.dtmEntryDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmEntryDate.Anchor")));
			this.dtmEntryDate.AutoSize = ((bool)(resources.GetObject("dtmEntryDate.AutoSize")));
			this.dtmEntryDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmEntryDate.BackgroundImage")));
			this.dtmEntryDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmEntryDate.BorderStyle")));
			// 
			// dtmEntryDate.Calendar
			// 
			this.dtmEntryDate.Calendar.AccessibleDescription = resources.GetString("dtmEntryDate.Calendar.AccessibleDescription");
			this.dtmEntryDate.Calendar.AccessibleName = resources.GetString("dtmEntryDate.Calendar.AccessibleName");
			this.dtmEntryDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmEntryDate.Calendar.AnnuallyBoldedDates")));
			this.dtmEntryDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmEntryDate.Calendar.BackgroundImage")));
			this.dtmEntryDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmEntryDate.Calendar.BoldedDates")));
			this.dtmEntryDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmEntryDate.Calendar.CalendarDimensions")));
			this.dtmEntryDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmEntryDate.Calendar.Enabled")));
			this.dtmEntryDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmEntryDate.Calendar.FirstDayOfWeek")));
			this.dtmEntryDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmEntryDate.Calendar.Font")));
			this.dtmEntryDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmEntryDate.Calendar.ImeMode")));
			this.dtmEntryDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmEntryDate.Calendar.MonthlyBoldedDates")));
			this.dtmEntryDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmEntryDate.Calendar.RightToLeft")));
			this.dtmEntryDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmEntryDate.Calendar.ShowClearButton")));
			this.dtmEntryDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmEntryDate.Calendar.ShowTodayButton")));
			this.dtmEntryDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmEntryDate.Calendar.ShowWeekNumbers")));
			this.dtmEntryDate.CaseSensitive = ((bool)(resources.GetObject("dtmEntryDate.CaseSensitive")));
			this.dtmEntryDate.Culture = ((int)(resources.GetObject("dtmEntryDate.Culture")));
			this.dtmEntryDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmEntryDate.CurrentTimeZone")));
			this.dtmEntryDate.CustomFormat = resources.GetString("dtmEntryDate.CustomFormat");
			this.dtmEntryDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmEntryDate.DaylightTimeAdjustment")));
			this.dtmEntryDate.DisplayFormat.CustomFormat = resources.GetString("dtmEntryDate.DisplayFormat.CustomFormat");
			this.dtmEntryDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmEntryDate.DisplayFormat.FormatType")));
			this.dtmEntryDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmEntryDate.DisplayFormat.Inherit")));
			this.dtmEntryDate.DisplayFormat.NullText = resources.GetString("dtmEntryDate.DisplayFormat.NullText");
			this.dtmEntryDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmEntryDate.DisplayFormat.TrimEnd")));
			this.dtmEntryDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmEntryDate.DisplayFormat.TrimStart")));
			this.dtmEntryDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmEntryDate.Dock")));
			this.dtmEntryDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmEntryDate.DropDownFormAlign")));
			this.dtmEntryDate.EditFormat.CustomFormat = resources.GetString("dtmEntryDate.EditFormat.CustomFormat");
			this.dtmEntryDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmEntryDate.EditFormat.FormatType")));
			this.dtmEntryDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmEntryDate.EditFormat.Inherit")));
			this.dtmEntryDate.EditFormat.NullText = resources.GetString("dtmEntryDate.EditFormat.NullText");
			this.dtmEntryDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmEntryDate.EditFormat.TrimEnd")));
			this.dtmEntryDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmEntryDate.EditFormat.TrimStart")));
			this.dtmEntryDate.EditMask = resources.GetString("dtmEntryDate.EditMask");
			this.dtmEntryDate.EmptyAsNull = ((bool)(resources.GetObject("dtmEntryDate.EmptyAsNull")));
			this.dtmEntryDate.Enabled = ((bool)(resources.GetObject("dtmEntryDate.Enabled")));
			this.dtmEntryDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmEntryDate.ErrorInfo.BeepOnError")));
			this.dtmEntryDate.ErrorInfo.ErrorMessage = resources.GetString("dtmEntryDate.ErrorInfo.ErrorMessage");
			this.dtmEntryDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmEntryDate.ErrorInfo.ErrorMessageCaption");
			this.dtmEntryDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmEntryDate.ErrorInfo.ShowErrorMessage")));
			this.dtmEntryDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmEntryDate.ErrorInfo.ValueOnError")));
			this.dtmEntryDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmEntryDate.Font")));
			this.dtmEntryDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmEntryDate.FormatType")));
			this.dtmEntryDate.GapHeight = ((int)(resources.GetObject("dtmEntryDate.GapHeight")));
			this.dtmEntryDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmEntryDate.GMTOffset")));
			this.dtmEntryDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmEntryDate.ImeMode")));
			this.dtmEntryDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmEntryDate.InitialSelection")));
			this.dtmEntryDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmEntryDate.Location")));
			this.dtmEntryDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmEntryDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmEntryDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmEntryDate.MaskInfo.CaseSensitive")));
			this.dtmEntryDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmEntryDate.MaskInfo.CopyWithLiterals")));
			this.dtmEntryDate.MaskInfo.EditMask = resources.GetString("dtmEntryDate.MaskInfo.EditMask");
			this.dtmEntryDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmEntryDate.MaskInfo.EmptyAsNull")));
			this.dtmEntryDate.MaskInfo.ErrorMessage = resources.GetString("dtmEntryDate.MaskInfo.ErrorMessage");
			this.dtmEntryDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmEntryDate.MaskInfo.Inherit")));
			this.dtmEntryDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmEntryDate.MaskInfo.PromptChar")));
			this.dtmEntryDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmEntryDate.MaskInfo.ShowLiterals")));
			this.dtmEntryDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmEntryDate.MaskInfo.StoredEmptyChar")));
			this.dtmEntryDate.MaxLength = ((int)(resources.GetObject("dtmEntryDate.MaxLength")));
			this.dtmEntryDate.Name = "dtmEntryDate";
			this.dtmEntryDate.NullText = resources.GetString("dtmEntryDate.NullText");
			this.dtmEntryDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmEntryDate.ParseInfo.CaseSensitive")));
			this.dtmEntryDate.ParseInfo.CustomFormat = resources.GetString("dtmEntryDate.ParseInfo.CustomFormat");
			this.dtmEntryDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmEntryDate.ParseInfo.DateTimeStyle")));
			this.dtmEntryDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmEntryDate.ParseInfo.EmptyAsNull")));
			this.dtmEntryDate.ParseInfo.ErrorMessage = resources.GetString("dtmEntryDate.ParseInfo.ErrorMessage");
			this.dtmEntryDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmEntryDate.ParseInfo.FormatType")));
			this.dtmEntryDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmEntryDate.ParseInfo.Inherit")));
			this.dtmEntryDate.ParseInfo.NullText = resources.GetString("dtmEntryDate.ParseInfo.NullText");
			this.dtmEntryDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmEntryDate.ParseInfo.TrimEnd")));
			this.dtmEntryDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmEntryDate.ParseInfo.TrimStart")));
			this.dtmEntryDate.PasswordChar = ((char)(resources.GetObject("dtmEntryDate.PasswordChar")));
			this.dtmEntryDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmEntryDate.PostValidation.CaseSensitive")));
			this.dtmEntryDate.PostValidation.ErrorMessage = resources.GetString("dtmEntryDate.PostValidation.ErrorMessage");
			this.dtmEntryDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmEntryDate.PostValidation.Inherit")));
			this.dtmEntryDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmEntryDate.PostValidation.Validation")));
			this.dtmEntryDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmEntryDate.PostValidation.Values")));
			this.dtmEntryDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmEntryDate.PostValidation.ValuesExcluded")));
			this.dtmEntryDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmEntryDate.PreValidation.CaseSensitive")));
			this.dtmEntryDate.PreValidation.ErrorMessage = resources.GetString("dtmEntryDate.PreValidation.ErrorMessage");
			this.dtmEntryDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmEntryDate.PreValidation.Inherit")));
			this.dtmEntryDate.PreValidation.ItemSeparator = resources.GetString("dtmEntryDate.PreValidation.ItemSeparator");
			this.dtmEntryDate.PreValidation.PatternString = resources.GetString("dtmEntryDate.PreValidation.PatternString");
			this.dtmEntryDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmEntryDate.PreValidation.RegexOptions")));
			this.dtmEntryDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmEntryDate.PreValidation.TrimEnd")));
			this.dtmEntryDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmEntryDate.PreValidation.TrimStart")));
			this.dtmEntryDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmEntryDate.PreValidation.Validation")));
			this.dtmEntryDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmEntryDate.RightToLeft")));
			this.dtmEntryDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmEntryDate.ShowFocusRectangle")));
			this.dtmEntryDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmEntryDate.Size")));
			this.dtmEntryDate.TabIndex = ((int)(resources.GetObject("dtmEntryDate.TabIndex")));
			this.dtmEntryDate.Tag = ((object)(resources.GetObject("dtmEntryDate.Tag")));
			this.dtmEntryDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmEntryDate.TextAlign")));
			this.dtmEntryDate.TrimEnd = ((bool)(resources.GetObject("dtmEntryDate.TrimEnd")));
			this.dtmEntryDate.TrimStart = ((bool)(resources.GetObject("dtmEntryDate.TrimStart")));
			this.dtmEntryDate.UserCultureOverride = ((bool)(resources.GetObject("dtmEntryDate.UserCultureOverride")));
			this.dtmEntryDate.Value = ((object)(resources.GetObject("dtmEntryDate.Value")));
			this.dtmEntryDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmEntryDate.VerticalAlign")));
			this.dtmEntryDate.Visible = ((bool)(resources.GetObject("dtmEntryDate.Visible")));
			this.dtmEntryDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmEntryDate.VisibleButtons")));
			this.dtmEntryDate.ValueChanged += new System.EventHandler(this.dtmEntryDate_ValueChanged);
			this.dtmEntryDate.Enter += new System.EventHandler(this.dtmEntryDate_Enter);
			// 
			// lblCustomerID
			// 
			this.lblCustomerID.AccessibleDescription = resources.GetString("lblCustomerID.AccessibleDescription");
			this.lblCustomerID.AccessibleName = resources.GetString("lblCustomerID.AccessibleName");
			this.lblCustomerID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCustomerID.Anchor")));
			this.lblCustomerID.AutoSize = ((bool)(resources.GetObject("lblCustomerID.AutoSize")));
			this.lblCustomerID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCustomerID.Dock")));
			this.lblCustomerID.Enabled = ((bool)(resources.GetObject("lblCustomerID.Enabled")));
			this.lblCustomerID.Font = ((System.Drawing.Font)(resources.GetObject("lblCustomerID.Font")));
			this.lblCustomerID.Image = ((System.Drawing.Image)(resources.GetObject("lblCustomerID.Image")));
			this.lblCustomerID.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCustomerID.ImageAlign")));
			this.lblCustomerID.ImageIndex = ((int)(resources.GetObject("lblCustomerID.ImageIndex")));
			this.lblCustomerID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCustomerID.ImeMode")));
			this.lblCustomerID.Location = ((System.Drawing.Point)(resources.GetObject("lblCustomerID.Location")));
			this.lblCustomerID.Name = "lblCustomerID";
			this.lblCustomerID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCustomerID.RightToLeft")));
			this.lblCustomerID.Size = ((System.Drawing.Size)(resources.GetObject("lblCustomerID.Size")));
			this.lblCustomerID.TabIndex = ((int)(resources.GetObject("lblCustomerID.TabIndex")));
			this.lblCustomerID.Text = resources.GetString("lblCustomerID.Text");
			this.lblCustomerID.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCustomerID.TextAlign")));
			this.lblCustomerID.Visible = ((bool)(resources.GetObject("lblCustomerID.Visible")));
			// 
			// lblSaleOrderMasterID
			// 
			this.lblSaleOrderMasterID.AccessibleDescription = resources.GetString("lblSaleOrderMasterID.AccessibleDescription");
			this.lblSaleOrderMasterID.AccessibleName = resources.GetString("lblSaleOrderMasterID.AccessibleName");
			this.lblSaleOrderMasterID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblSaleOrderMasterID.Anchor")));
			this.lblSaleOrderMasterID.AutoSize = ((bool)(resources.GetObject("lblSaleOrderMasterID.AutoSize")));
			this.lblSaleOrderMasterID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblSaleOrderMasterID.Dock")));
			this.lblSaleOrderMasterID.Enabled = ((bool)(resources.GetObject("lblSaleOrderMasterID.Enabled")));
			this.lblSaleOrderMasterID.Font = ((System.Drawing.Font)(resources.GetObject("lblSaleOrderMasterID.Font")));
			this.lblSaleOrderMasterID.Image = ((System.Drawing.Image)(resources.GetObject("lblSaleOrderMasterID.Image")));
			this.lblSaleOrderMasterID.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblSaleOrderMasterID.ImageAlign")));
			this.lblSaleOrderMasterID.ImageIndex = ((int)(resources.GetObject("lblSaleOrderMasterID.ImageIndex")));
			this.lblSaleOrderMasterID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblSaleOrderMasterID.ImeMode")));
			this.lblSaleOrderMasterID.Location = ((System.Drawing.Point)(resources.GetObject("lblSaleOrderMasterID.Location")));
			this.lblSaleOrderMasterID.Name = "lblSaleOrderMasterID";
			this.lblSaleOrderMasterID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblSaleOrderMasterID.RightToLeft")));
			this.lblSaleOrderMasterID.Size = ((System.Drawing.Size)(resources.GetObject("lblSaleOrderMasterID.Size")));
			this.lblSaleOrderMasterID.TabIndex = ((int)(resources.GetObject("lblSaleOrderMasterID.TabIndex")));
			this.lblSaleOrderMasterID.Text = resources.GetString("lblSaleOrderMasterID.Text");
			this.lblSaleOrderMasterID.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblSaleOrderMasterID.TextAlign")));
			this.lblSaleOrderMasterID.Visible = ((bool)(resources.GetObject("lblSaleOrderMasterID.Visible")));
			// 
			// btnClose
			// 
			this.btnClose.AccessibleDescription = resources.GetString("btnClose.AccessibleDescription");
			this.btnClose.AccessibleName = resources.GetString("btnClose.AccessibleName");
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnClose.Anchor")));
			this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnClose.Dock")));
			this.btnClose.Enabled = ((bool)(resources.GetObject("btnClose.Enabled")));
			this.btnClose.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnClose.FlatStyle")));
			this.btnClose.Font = ((System.Drawing.Font)(resources.GetObject("btnClose.Font")));
			this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
			this.btnClose.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClose.ImageAlign")));
			this.btnClose.ImageIndex = ((int)(resources.GetObject("btnClose.ImageIndex")));
			this.btnClose.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnClose.ImeMode")));
			this.btnClose.Location = ((System.Drawing.Point)(resources.GetObject("btnClose.Location")));
			this.btnClose.Name = "btnClose";
			this.btnClose.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnClose.RightToLeft")));
			this.btnClose.Size = ((System.Drawing.Size)(resources.GetObject("btnClose.Size")));
			this.btnClose.TabIndex = ((int)(resources.GetObject("btnClose.TabIndex")));
			this.btnClose.Text = resources.GetString("btnClose.Text");
			this.btnClose.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClose.TextAlign")));
			this.btnClose.Visible = ((bool)(resources.GetObject("btnClose.Visible")));
			this.btnClose.Click += new System.EventHandler(this.btnCloseForm_Click);
			// 
			// txtMasLoc
			// 
			this.txtMasLoc.AccessibleDescription = resources.GetString("txtMasLoc.AccessibleDescription");
			this.txtMasLoc.AccessibleName = resources.GetString("txtMasLoc.AccessibleName");
			this.txtMasLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtMasLoc.Anchor")));
			this.txtMasLoc.AutoSize = ((bool)(resources.GetObject("txtMasLoc.AutoSize")));
			this.txtMasLoc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtMasLoc.BackgroundImage")));
			this.txtMasLoc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtMasLoc.Dock")));
			this.txtMasLoc.Enabled = ((bool)(resources.GetObject("txtMasLoc.Enabled")));
			this.txtMasLoc.Font = ((System.Drawing.Font)(resources.GetObject("txtMasLoc.Font")));
			this.txtMasLoc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtMasLoc.ImeMode")));
			this.txtMasLoc.Location = ((System.Drawing.Point)(resources.GetObject("txtMasLoc.Location")));
			this.txtMasLoc.MaxLength = ((int)(resources.GetObject("txtMasLoc.MaxLength")));
			this.txtMasLoc.Multiline = ((bool)(resources.GetObject("txtMasLoc.Multiline")));
			this.txtMasLoc.Name = "txtMasLoc";
			this.txtMasLoc.PasswordChar = ((char)(resources.GetObject("txtMasLoc.PasswordChar")));
			this.txtMasLoc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtMasLoc.RightToLeft")));
			this.txtMasLoc.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtMasLoc.ScrollBars")));
			this.txtMasLoc.Size = ((System.Drawing.Size)(resources.GetObject("txtMasLoc.Size")));
			this.txtMasLoc.TabIndex = ((int)(resources.GetObject("txtMasLoc.TabIndex")));
			this.txtMasLoc.Text = resources.GetString("txtMasLoc.Text");
			this.txtMasLoc.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtMasLoc.TextAlign")));
			this.txtMasLoc.Visible = ((bool)(resources.GetObject("txtMasLoc.Visible")));
			this.txtMasLoc.WordWrap = ((bool)(resources.GetObject("txtMasLoc.WordWrap")));
			this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
			this.txtMasLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasLoc_Validating);
			this.txtMasLoc.TextChanged += new System.EventHandler(this.txtMasLoc_TextChanged);
			// 
			// btnSearchMasLoc
			// 
			this.btnSearchMasLoc.AccessibleDescription = resources.GetString("btnSearchMasLoc.AccessibleDescription");
			this.btnSearchMasLoc.AccessibleName = resources.GetString("btnSearchMasLoc.AccessibleName");
			this.btnSearchMasLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearchMasLoc.Anchor")));
			this.btnSearchMasLoc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchMasLoc.BackgroundImage")));
			this.btnSearchMasLoc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearchMasLoc.Dock")));
			this.btnSearchMasLoc.Enabled = ((bool)(resources.GetObject("btnSearchMasLoc.Enabled")));
			this.btnSearchMasLoc.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearchMasLoc.FlatStyle")));
			this.btnSearchMasLoc.Font = ((System.Drawing.Font)(resources.GetObject("btnSearchMasLoc.Font")));
			this.btnSearchMasLoc.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchMasLoc.Image")));
			this.btnSearchMasLoc.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchMasLoc.ImageAlign")));
			this.btnSearchMasLoc.ImageIndex = ((int)(resources.GetObject("btnSearchMasLoc.ImageIndex")));
			this.btnSearchMasLoc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearchMasLoc.ImeMode")));
			this.btnSearchMasLoc.Location = ((System.Drawing.Point)(resources.GetObject("btnSearchMasLoc.Location")));
			this.btnSearchMasLoc.Name = "btnSearchMasLoc";
			this.btnSearchMasLoc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearchMasLoc.RightToLeft")));
			this.btnSearchMasLoc.Size = ((System.Drawing.Size)(resources.GetObject("btnSearchMasLoc.Size")));
			this.btnSearchMasLoc.TabIndex = ((int)(resources.GetObject("btnSearchMasLoc.TabIndex")));
			this.btnSearchMasLoc.Text = resources.GetString("btnSearchMasLoc.Text");
			this.btnSearchMasLoc.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearchMasLoc.TextAlign")));
			this.btnSearchMasLoc.Visible = ((bool)(resources.GetObject("btnSearchMasLoc.Visible")));
			this.btnSearchMasLoc.Click += new System.EventHandler(this.btnSearchMasLoc_Click);
			// 
			// txtCustomerLoc
			// 
			this.txtCustomerLoc.AccessibleDescription = resources.GetString("txtCustomerLoc.AccessibleDescription");
			this.txtCustomerLoc.AccessibleName = resources.GetString("txtCustomerLoc.AccessibleName");
			this.txtCustomerLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCustomerLoc.Anchor")));
			this.txtCustomerLoc.AutoSize = ((bool)(resources.GetObject("txtCustomerLoc.AutoSize")));
			this.txtCustomerLoc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCustomerLoc.BackgroundImage")));
			this.txtCustomerLoc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCustomerLoc.Dock")));
			this.txtCustomerLoc.Enabled = ((bool)(resources.GetObject("txtCustomerLoc.Enabled")));
			this.txtCustomerLoc.Font = ((System.Drawing.Font)(resources.GetObject("txtCustomerLoc.Font")));
			this.txtCustomerLoc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCustomerLoc.ImeMode")));
			this.txtCustomerLoc.Location = ((System.Drawing.Point)(resources.GetObject("txtCustomerLoc.Location")));
			this.txtCustomerLoc.MaxLength = ((int)(resources.GetObject("txtCustomerLoc.MaxLength")));
			this.txtCustomerLoc.Multiline = ((bool)(resources.GetObject("txtCustomerLoc.Multiline")));
			this.txtCustomerLoc.Name = "txtCustomerLoc";
			this.txtCustomerLoc.PasswordChar = ((char)(resources.GetObject("txtCustomerLoc.PasswordChar")));
			this.txtCustomerLoc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCustomerLoc.RightToLeft")));
			this.txtCustomerLoc.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCustomerLoc.ScrollBars")));
			this.txtCustomerLoc.Size = ((System.Drawing.Size)(resources.GetObject("txtCustomerLoc.Size")));
			this.txtCustomerLoc.TabIndex = ((int)(resources.GetObject("txtCustomerLoc.TabIndex")));
			this.txtCustomerLoc.Text = resources.GetString("txtCustomerLoc.Text");
			this.txtCustomerLoc.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCustomerLoc.TextAlign")));
			this.txtCustomerLoc.Visible = ((bool)(resources.GetObject("txtCustomerLoc.Visible")));
			this.txtCustomerLoc.WordWrap = ((bool)(resources.GetObject("txtCustomerLoc.WordWrap")));
			this.txtCustomerLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerLoc_KeyDown);
			this.txtCustomerLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtCustomerLoc_Validating);
			// 
			// btnCustomerLoc
			// 
			this.btnCustomerLoc.AccessibleDescription = resources.GetString("btnCustomerLoc.AccessibleDescription");
			this.btnCustomerLoc.AccessibleName = resources.GetString("btnCustomerLoc.AccessibleName");
			this.btnCustomerLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCustomerLoc.Anchor")));
			this.btnCustomerLoc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCustomerLoc.BackgroundImage")));
			this.btnCustomerLoc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCustomerLoc.Dock")));
			this.btnCustomerLoc.Enabled = ((bool)(resources.GetObject("btnCustomerLoc.Enabled")));
			this.btnCustomerLoc.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCustomerLoc.FlatStyle")));
			this.btnCustomerLoc.Font = ((System.Drawing.Font)(resources.GetObject("btnCustomerLoc.Font")));
			this.btnCustomerLoc.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomerLoc.Image")));
			this.btnCustomerLoc.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCustomerLoc.ImageAlign")));
			this.btnCustomerLoc.ImageIndex = ((int)(resources.GetObject("btnCustomerLoc.ImageIndex")));
			this.btnCustomerLoc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCustomerLoc.ImeMode")));
			this.btnCustomerLoc.Location = ((System.Drawing.Point)(resources.GetObject("btnCustomerLoc.Location")));
			this.btnCustomerLoc.Name = "btnCustomerLoc";
			this.btnCustomerLoc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCustomerLoc.RightToLeft")));
			this.btnCustomerLoc.Size = ((System.Drawing.Size)(resources.GetObject("btnCustomerLoc.Size")));
			this.btnCustomerLoc.TabIndex = ((int)(resources.GetObject("btnCustomerLoc.TabIndex")));
			this.btnCustomerLoc.Text = resources.GetString("btnCustomerLoc.Text");
			this.btnCustomerLoc.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCustomerLoc.TextAlign")));
			this.btnCustomerLoc.Visible = ((bool)(resources.GetObject("btnCustomerLoc.Visible")));
			this.btnCustomerLoc.Click += new System.EventHandler(this.btnCustomerLoc_Click);
			// 
			// txtContact
			// 
			this.txtContact.AccessibleDescription = resources.GetString("txtContact.AccessibleDescription");
			this.txtContact.AccessibleName = resources.GetString("txtContact.AccessibleName");
			this.txtContact.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtContact.Anchor")));
			this.txtContact.AutoSize = ((bool)(resources.GetObject("txtContact.AutoSize")));
			this.txtContact.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtContact.BackgroundImage")));
			this.txtContact.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtContact.Dock")));
			this.txtContact.Enabled = ((bool)(resources.GetObject("txtContact.Enabled")));
			this.txtContact.Font = ((System.Drawing.Font)(resources.GetObject("txtContact.Font")));
			this.txtContact.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtContact.ImeMode")));
			this.txtContact.Location = ((System.Drawing.Point)(resources.GetObject("txtContact.Location")));
			this.txtContact.MaxLength = ((int)(resources.GetObject("txtContact.MaxLength")));
			this.txtContact.Multiline = ((bool)(resources.GetObject("txtContact.Multiline")));
			this.txtContact.Name = "txtContact";
			this.txtContact.PasswordChar = ((char)(resources.GetObject("txtContact.PasswordChar")));
			this.txtContact.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtContact.RightToLeft")));
			this.txtContact.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtContact.ScrollBars")));
			this.txtContact.Size = ((System.Drawing.Size)(resources.GetObject("txtContact.Size")));
			this.txtContact.TabIndex = ((int)(resources.GetObject("txtContact.TabIndex")));
			this.txtContact.Text = resources.GetString("txtContact.Text");
			this.txtContact.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtContact.TextAlign")));
			this.txtContact.Visible = ((bool)(resources.GetObject("txtContact.Visible")));
			this.txtContact.WordWrap = ((bool)(resources.GetObject("txtContact.WordWrap")));
			this.txtContact.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContact_KeyDown);
			this.txtContact.Validating += new System.ComponentModel.CancelEventHandler(this.txtContact_Validating);
			// 
			// btnContact
			// 
			this.btnContact.AccessibleDescription = resources.GetString("btnContact.AccessibleDescription");
			this.btnContact.AccessibleName = resources.GetString("btnContact.AccessibleName");
			this.btnContact.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnContact.Anchor")));
			this.btnContact.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnContact.BackgroundImage")));
			this.btnContact.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnContact.Dock")));
			this.btnContact.Enabled = ((bool)(resources.GetObject("btnContact.Enabled")));
			this.btnContact.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnContact.FlatStyle")));
			this.btnContact.Font = ((System.Drawing.Font)(resources.GetObject("btnContact.Font")));
			this.btnContact.Image = ((System.Drawing.Image)(resources.GetObject("btnContact.Image")));
			this.btnContact.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnContact.ImageAlign")));
			this.btnContact.ImageIndex = ((int)(resources.GetObject("btnContact.ImageIndex")));
			this.btnContact.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnContact.ImeMode")));
			this.btnContact.Location = ((System.Drawing.Point)(resources.GetObject("btnContact.Location")));
			this.btnContact.Name = "btnContact";
			this.btnContact.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnContact.RightToLeft")));
			this.btnContact.Size = ((System.Drawing.Size)(resources.GetObject("btnContact.Size")));
			this.btnContact.TabIndex = ((int)(resources.GetObject("btnContact.TabIndex")));
			this.btnContact.Text = resources.GetString("btnContact.Text");
			this.btnContact.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnContact.TextAlign")));
			this.btnContact.Visible = ((bool)(resources.GetObject("btnContact.Visible")));
			this.btnContact.Click += new System.EventHandler(this.btnContact_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.AccessibleDescription = resources.GetString("btnHelp.AccessibleDescription");
			this.btnHelp.AccessibleName = resources.GetString("btnHelp.AccessibleName");
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnHelp.Anchor")));
			this.btnHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHelp.BackgroundImage")));
			this.btnHelp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnHelp.Dock")));
			this.btnHelp.Enabled = ((bool)(resources.GetObject("btnHelp.Enabled")));
			this.btnHelp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnHelp.FlatStyle")));
			this.btnHelp.Font = ((System.Drawing.Font)(resources.GetObject("btnHelp.Font")));
			this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
			this.btnHelp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.ImageAlign")));
			this.btnHelp.ImageIndex = ((int)(resources.GetObject("btnHelp.ImageIndex")));
			this.btnHelp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnHelp.ImeMode")));
			this.btnHelp.Location = ((System.Drawing.Point)(resources.GetObject("btnHelp.Location")));
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnHelp.RightToLeft")));
			this.btnHelp.Size = ((System.Drawing.Size)(resources.GetObject("btnHelp.Size")));
			this.btnHelp.TabIndex = ((int)(resources.GetObject("btnHelp.TabIndex")));
			this.btnHelp.Text = resources.GetString("btnHelp.Text");
			this.btnHelp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.TextAlign")));
			this.btnHelp.Visible = ((bool)(resources.GetObject("btnHelp.Visible")));
			// 
			// txtCurrency
			// 
			this.txtCurrency.AccessibleDescription = resources.GetString("txtCurrency.AccessibleDescription");
			this.txtCurrency.AccessibleName = resources.GetString("txtCurrency.AccessibleName");
			this.txtCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCurrency.Anchor")));
			this.txtCurrency.AutoSize = ((bool)(resources.GetObject("txtCurrency.AutoSize")));
			this.txtCurrency.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCurrency.BackgroundImage")));
			this.txtCurrency.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCurrency.Dock")));
			this.txtCurrency.Enabled = ((bool)(resources.GetObject("txtCurrency.Enabled")));
			this.txtCurrency.Font = ((System.Drawing.Font)(resources.GetObject("txtCurrency.Font")));
			this.txtCurrency.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCurrency.ImeMode")));
			this.txtCurrency.Location = ((System.Drawing.Point)(resources.GetObject("txtCurrency.Location")));
			this.txtCurrency.MaxLength = ((int)(resources.GetObject("txtCurrency.MaxLength")));
			this.txtCurrency.Multiline = ((bool)(resources.GetObject("txtCurrency.Multiline")));
			this.txtCurrency.Name = "txtCurrency";
			this.txtCurrency.PasswordChar = ((char)(resources.GetObject("txtCurrency.PasswordChar")));
			this.txtCurrency.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCurrency.RightToLeft")));
			this.txtCurrency.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCurrency.ScrollBars")));
			this.txtCurrency.Size = ((System.Drawing.Size)(resources.GetObject("txtCurrency.Size")));
			this.txtCurrency.TabIndex = ((int)(resources.GetObject("txtCurrency.TabIndex")));
			this.txtCurrency.Text = resources.GetString("txtCurrency.Text");
			this.txtCurrency.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCurrency.TextAlign")));
			this.txtCurrency.Visible = ((bool)(resources.GetObject("txtCurrency.Visible")));
			this.txtCurrency.WordWrap = ((bool)(resources.GetObject("txtCurrency.WordWrap")));
			this.txtCurrency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrency_KeyDown);
			this.txtCurrency.Validating += new System.ComponentModel.CancelEventHandler(this.txtCurrency_Validating);
			// 
			// btnCurrency
			// 
			this.btnCurrency.AccessibleDescription = resources.GetString("btnCurrency.AccessibleDescription");
			this.btnCurrency.AccessibleName = resources.GetString("btnCurrency.AccessibleName");
			this.btnCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCurrency.Anchor")));
			this.btnCurrency.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCurrency.BackgroundImage")));
			this.btnCurrency.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCurrency.Dock")));
			this.btnCurrency.Enabled = ((bool)(resources.GetObject("btnCurrency.Enabled")));
			this.btnCurrency.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCurrency.FlatStyle")));
			this.btnCurrency.Font = ((System.Drawing.Font)(resources.GetObject("btnCurrency.Font")));
			this.btnCurrency.Image = ((System.Drawing.Image)(resources.GetObject("btnCurrency.Image")));
			this.btnCurrency.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCurrency.ImageAlign")));
			this.btnCurrency.ImageIndex = ((int)(resources.GetObject("btnCurrency.ImageIndex")));
			this.btnCurrency.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCurrency.ImeMode")));
			this.btnCurrency.Location = ((System.Drawing.Point)(resources.GetObject("btnCurrency.Location")));
			this.btnCurrency.Name = "btnCurrency";
			this.btnCurrency.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCurrency.RightToLeft")));
			this.btnCurrency.Size = ((System.Drawing.Size)(resources.GetObject("btnCurrency.Size")));
			this.btnCurrency.TabIndex = ((int)(resources.GetObject("btnCurrency.TabIndex")));
			this.btnCurrency.Text = resources.GetString("btnCurrency.Text");
			this.btnCurrency.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCurrency.TextAlign")));
			this.btnCurrency.Visible = ((bool)(resources.GetObject("btnCurrency.Visible")));
			this.btnCurrency.Click += new System.EventHandler(this.btnCurrency_Click);
			// 
			// txtExchRate
			// 
			this.txtExchRate.AcceptsEscape = ((bool)(resources.GetObject("txtExchRate.AcceptsEscape")));
			this.txtExchRate.AccessibleDescription = resources.GetString("txtExchRate.AccessibleDescription");
			this.txtExchRate.AccessibleName = resources.GetString("txtExchRate.AccessibleName");
			this.txtExchRate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtExchRate.Anchor")));
			this.txtExchRate.AutoSize = ((bool)(resources.GetObject("txtExchRate.AutoSize")));
			this.txtExchRate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtExchRate.BackgroundImage")));
			this.txtExchRate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("txtExchRate.BorderStyle")));
			// 
			// txtExchRate.Calculator
			// 
			this.txtExchRate.Calculator.AccessibleDescription = resources.GetString("txtExchRate.Calculator.AccessibleDescription");
			this.txtExchRate.Calculator.AccessibleName = resources.GetString("txtExchRate.Calculator.AccessibleName");
			this.txtExchRate.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtExchRate.Calculator.BackgroundImage")));
			this.txtExchRate.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("txtExchRate.Calculator.ButtonFlatStyle")));
			this.txtExchRate.Calculator.DisplayFormat = resources.GetString("txtExchRate.Calculator.DisplayFormat");
			this.txtExchRate.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("txtExchRate.Calculator.Font")));
			this.txtExchRate.Calculator.FormatOnClose = ((bool)(resources.GetObject("txtExchRate.Calculator.FormatOnClose")));
			this.txtExchRate.Calculator.StoredFormat = resources.GetString("txtExchRate.Calculator.StoredFormat");
			this.txtExchRate.Calculator.UIStrings.Content = ((string[])(resources.GetObject("txtExchRate.Calculator.UIStrings.Content")));
			this.txtExchRate.CaseSensitive = ((bool)(resources.GetObject("txtExchRate.CaseSensitive")));
			this.txtExchRate.Culture = ((int)(resources.GetObject("txtExchRate.Culture")));
			this.txtExchRate.CustomFormat = resources.GetString("txtExchRate.CustomFormat");
			this.txtExchRate.DataType = ((System.Type)(resources.GetObject("txtExchRate.DataType")));
			this.txtExchRate.DisplayFormat.CustomFormat = resources.GetString("txtExchRate.DisplayFormat.CustomFormat");
			this.txtExchRate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtExchRate.DisplayFormat.FormatType")));
			this.txtExchRate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtExchRate.DisplayFormat.Inherit")));
			this.txtExchRate.DisplayFormat.NullText = resources.GetString("txtExchRate.DisplayFormat.NullText");
			this.txtExchRate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("txtExchRate.DisplayFormat.TrimEnd")));
			this.txtExchRate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("txtExchRate.DisplayFormat.TrimStart")));
			this.txtExchRate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtExchRate.Dock")));
			this.txtExchRate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("txtExchRate.DropDownFormAlign")));
			this.txtExchRate.EditFormat.CustomFormat = resources.GetString("txtExchRate.EditFormat.CustomFormat");
			this.txtExchRate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtExchRate.EditFormat.FormatType")));
			this.txtExchRate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtExchRate.EditFormat.Inherit")));
			this.txtExchRate.EditFormat.NullText = resources.GetString("txtExchRate.EditFormat.NullText");
			this.txtExchRate.EditFormat.TrimEnd = ((bool)(resources.GetObject("txtExchRate.EditFormat.TrimEnd")));
			this.txtExchRate.EditFormat.TrimStart = ((bool)(resources.GetObject("txtExchRate.EditFormat.TrimStart")));
			this.txtExchRate.EditMask = resources.GetString("txtExchRate.EditMask");
			this.txtExchRate.EmptyAsNull = ((bool)(resources.GetObject("txtExchRate.EmptyAsNull")));
			this.txtExchRate.Enabled = ((bool)(resources.GetObject("txtExchRate.Enabled")));
			this.txtExchRate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtExchRate.ErrorInfo.BeepOnError")));
			this.txtExchRate.ErrorInfo.ErrorMessage = resources.GetString("txtExchRate.ErrorInfo.ErrorMessage");
			this.txtExchRate.ErrorInfo.ErrorMessageCaption = resources.GetString("txtExchRate.ErrorInfo.ErrorMessageCaption");
			this.txtExchRate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("txtExchRate.ErrorInfo.ShowErrorMessage")));
			this.txtExchRate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("txtExchRate.ErrorInfo.ValueOnError")));
			this.txtExchRate.Font = ((System.Drawing.Font)(resources.GetObject("txtExchRate.Font")));
			this.txtExchRate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtExchRate.FormatType")));
			this.txtExchRate.GapHeight = ((int)(resources.GetObject("txtExchRate.GapHeight")));
			this.txtExchRate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtExchRate.ImeMode")));
			this.txtExchRate.Increment = ((object)(resources.GetObject("txtExchRate.Increment")));
			this.txtExchRate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("txtExchRate.InitialSelection")));
			this.txtExchRate.Location = ((System.Drawing.Point)(resources.GetObject("txtExchRate.Location")));
			this.txtExchRate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("txtExchRate.MaskInfo.AutoTabWhenFilled")));
			this.txtExchRate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("txtExchRate.MaskInfo.CaseSensitive")));
			this.txtExchRate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("txtExchRate.MaskInfo.CopyWithLiterals")));
			this.txtExchRate.MaskInfo.EditMask = resources.GetString("txtExchRate.MaskInfo.EditMask");
			this.txtExchRate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("txtExchRate.MaskInfo.EmptyAsNull")));
			this.txtExchRate.MaskInfo.ErrorMessage = resources.GetString("txtExchRate.MaskInfo.ErrorMessage");
			this.txtExchRate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtExchRate.MaskInfo.Inherit")));
			this.txtExchRate.MaskInfo.PromptChar = ((char)(resources.GetObject("txtExchRate.MaskInfo.PromptChar")));
			this.txtExchRate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtExchRate.MaskInfo.ShowLiterals")));
			this.txtExchRate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("txtExchRate.MaskInfo.StoredEmptyChar")));
			this.txtExchRate.MaxLength = ((int)(resources.GetObject("txtExchRate.MaxLength")));
			this.txtExchRate.Name = "txtExchRate";
			this.txtExchRate.NullText = resources.GetString("txtExchRate.NullText");
			this.txtExchRate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("txtExchRate.ParseInfo.CaseSensitive")));
			this.txtExchRate.ParseInfo.CustomFormat = resources.GetString("txtExchRate.ParseInfo.CustomFormat");
			this.txtExchRate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("txtExchRate.ParseInfo.EmptyAsNull")));
			this.txtExchRate.ParseInfo.ErrorMessage = resources.GetString("txtExchRate.ParseInfo.ErrorMessage");
			this.txtExchRate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtExchRate.ParseInfo.FormatType")));
			this.txtExchRate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtExchRate.ParseInfo.Inherit")));
			this.txtExchRate.ParseInfo.NullText = resources.GetString("txtExchRate.ParseInfo.NullText");
			this.txtExchRate.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("txtExchRate.ParseInfo.NumberStyle")));
			this.txtExchRate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("txtExchRate.ParseInfo.TrimEnd")));
			this.txtExchRate.ParseInfo.TrimStart = ((bool)(resources.GetObject("txtExchRate.ParseInfo.TrimStart")));
			this.txtExchRate.PasswordChar = ((char)(resources.GetObject("txtExchRate.PasswordChar")));
			this.txtExchRate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("txtExchRate.PostValidation.CaseSensitive")));
			this.txtExchRate.PostValidation.ErrorMessage = resources.GetString("txtExchRate.PostValidation.ErrorMessage");
			this.txtExchRate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtExchRate.PostValidation.Inherit")));
			this.txtExchRate.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									  ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtExchRate.PostValidation.Intervals")))});
			this.txtExchRate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("txtExchRate.PostValidation.Validation")));
			this.txtExchRate.PostValidation.Values = ((System.Array)(resources.GetObject("txtExchRate.PostValidation.Values")));
			this.txtExchRate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("txtExchRate.PostValidation.ValuesExcluded")));
			this.txtExchRate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("txtExchRate.PreValidation.CaseSensitive")));
			this.txtExchRate.PreValidation.ErrorMessage = resources.GetString("txtExchRate.PreValidation.ErrorMessage");
			this.txtExchRate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtExchRate.PreValidation.Inherit")));
			this.txtExchRate.PreValidation.ItemSeparator = resources.GetString("txtExchRate.PreValidation.ItemSeparator");
			this.txtExchRate.PreValidation.PatternString = resources.GetString("txtExchRate.PreValidation.PatternString");
			this.txtExchRate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("txtExchRate.PreValidation.RegexOptions")));
			this.txtExchRate.PreValidation.TrimEnd = ((bool)(resources.GetObject("txtExchRate.PreValidation.TrimEnd")));
			this.txtExchRate.PreValidation.TrimStart = ((bool)(resources.GetObject("txtExchRate.PreValidation.TrimStart")));
			this.txtExchRate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("txtExchRate.PreValidation.Validation")));
			this.txtExchRate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtExchRate.RightToLeft")));
			this.txtExchRate.ShowFocusRectangle = ((bool)(resources.GetObject("txtExchRate.ShowFocusRectangle")));
			this.txtExchRate.Size = ((System.Drawing.Size)(resources.GetObject("txtExchRate.Size")));
			this.txtExchRate.TabIndex = ((int)(resources.GetObject("txtExchRate.TabIndex")));
			this.txtExchRate.Tag = ((object)(resources.GetObject("txtExchRate.Tag")));
			this.txtExchRate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtExchRate.TextAlign")));
			this.txtExchRate.TrimEnd = ((bool)(resources.GetObject("txtExchRate.TrimEnd")));
			this.txtExchRate.TrimStart = ((bool)(resources.GetObject("txtExchRate.TrimStart")));
			this.txtExchRate.UserCultureOverride = ((bool)(resources.GetObject("txtExchRate.UserCultureOverride")));
			this.txtExchRate.Value = ((object)(resources.GetObject("txtExchRate.Value")));
			this.txtExchRate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("txtExchRate.VerticalAlign")));
			this.txtExchRate.Visible = ((bool)(resources.GetObject("txtExchRate.Visible")));
			this.txtExchRate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("txtExchRate.VisibleButtons")));
			// 
			// lblExchRate
			// 
			this.lblExchRate.AccessibleDescription = resources.GetString("lblExchRate.AccessibleDescription");
			this.lblExchRate.AccessibleName = resources.GetString("lblExchRate.AccessibleName");
			this.lblExchRate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblExchRate.Anchor")));
			this.lblExchRate.AutoSize = ((bool)(resources.GetObject("lblExchRate.AutoSize")));
			this.lblExchRate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblExchRate.Dock")));
			this.lblExchRate.Enabled = ((bool)(resources.GetObject("lblExchRate.Enabled")));
			this.lblExchRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblExchRate.Font = ((System.Drawing.Font)(resources.GetObject("lblExchRate.Font")));
			this.lblExchRate.ForeColor = System.Drawing.Color.Maroon;
			this.lblExchRate.Image = ((System.Drawing.Image)(resources.GetObject("lblExchRate.Image")));
			this.lblExchRate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblExchRate.ImageAlign")));
			this.lblExchRate.ImageIndex = ((int)(resources.GetObject("lblExchRate.ImageIndex")));
			this.lblExchRate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblExchRate.ImeMode")));
			this.lblExchRate.Location = ((System.Drawing.Point)(resources.GetObject("lblExchRate.Location")));
			this.lblExchRate.Name = "lblExchRate";
			this.lblExchRate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblExchRate.RightToLeft")));
			this.lblExchRate.Size = ((System.Drawing.Size)(resources.GetObject("lblExchRate.Size")));
			this.lblExchRate.TabIndex = ((int)(resources.GetObject("lblExchRate.TabIndex")));
			this.lblExchRate.Text = resources.GetString("lblExchRate.Text");
			this.lblExchRate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblExchRate.TextAlign")));
			this.lblExchRate.Visible = ((bool)(resources.GetObject("lblExchRate.Visible")));
			// 
			// lblCurrency
			// 
			this.lblCurrency.AccessibleDescription = resources.GetString("lblCurrency.AccessibleDescription");
			this.lblCurrency.AccessibleName = resources.GetString("lblCurrency.AccessibleName");
			this.lblCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCurrency.Anchor")));
			this.lblCurrency.AutoSize = ((bool)(resources.GetObject("lblCurrency.AutoSize")));
			this.lblCurrency.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCurrency.Dock")));
			this.lblCurrency.Enabled = ((bool)(resources.GetObject("lblCurrency.Enabled")));
			this.lblCurrency.Font = ((System.Drawing.Font)(resources.GetObject("lblCurrency.Font")));
			this.lblCurrency.ForeColor = System.Drawing.Color.Maroon;
			this.lblCurrency.Image = ((System.Drawing.Image)(resources.GetObject("lblCurrency.Image")));
			this.lblCurrency.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCurrency.ImageAlign")));
			this.lblCurrency.ImageIndex = ((int)(resources.GetObject("lblCurrency.ImageIndex")));
			this.lblCurrency.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCurrency.ImeMode")));
			this.lblCurrency.Location = ((System.Drawing.Point)(resources.GetObject("lblCurrency.Location")));
			this.lblCurrency.Name = "lblCurrency";
			this.lblCurrency.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCurrency.RightToLeft")));
			this.lblCurrency.Size = ((System.Drawing.Size)(resources.GetObject("lblCurrency.Size")));
			this.lblCurrency.TabIndex = ((int)(resources.GetObject("lblCurrency.TabIndex")));
			this.lblCurrency.Text = resources.GetString("lblCurrency.Text");
			this.lblCurrency.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCurrency.TextAlign")));
			this.lblCurrency.Visible = ((bool)(resources.GetObject("lblCurrency.Visible")));
			// 
			// SOReturnGoodsReceive
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnClose;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.txtCurrency);
			this.Controls.Add(this.btnCurrency);
			this.Controls.Add(this.txtExchRate);
			this.Controls.Add(this.lblExchRate);
			this.Controls.Add(this.lblCurrency);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.txtContact);
			this.Controls.Add(this.txtCustomerLoc);
			this.Controls.Add(this.txtMasLoc);
			this.Controls.Add(this.txtCustomerName);
			this.Controls.Add(this.tgridViewData);
			this.Controls.Add(this.txtReturnedGoodsNumber);
			this.Controls.Add(this.txtCustomerCode);
			this.Controls.Add(this.txtSaleOrderNo);
			this.Controls.Add(this.btnContact);
			this.Controls.Add(this.btnCustomerLoc);
			this.Controls.Add(this.btnSearchMasLoc);
			this.Controls.Add(this.lblSaleOrderMasterID);
			this.Controls.Add(this.lblCustomerID);
			this.Controls.Add(this.dtmEntryDate);
			this.Controls.Add(this.btnSearchCustomerByName);
			this.Controls.Add(this.btnSearchCustomerByCode);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnSearchSaleOrder);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnSearchReturnedGoods);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblRGR);
			this.Controls.Add(this.lblCustomerLoc);
			this.Controls.Add(this.lblContact);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblCustomer);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnClose);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "SOReturnGoodsReceive";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SOReturnGoodsReceive_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.SOReturnGoodsReceive_Closing);
			this.Load += new System.EventHandler(this.SOReturnGoodsReceive_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tgridViewData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmEntryDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtExchRate)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		//**************************************************************************              
		///    <Description>
		///       Load data for all the combo boxes on form
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void LoadDataForCombo() 
		{
			//Load CCN
			UtilsBO objUtilsBO = new UtilsBO();
			FormControlComponents.PutDataIntoC1ComboBox(cboCCN,objUtilsBO.ListCCN().Tables[0],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
		}

		//**************************************************************************              
		///    <Description>
		///       Clear all in text boxes, combo boxes on form
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void ClearForm() 
		{
			#region HACK: DEL Trada 10-18-2005

			//			cboCCN.SelectedIndex = -1;
			//			cboCCN.Text = String.Empty;

			#endregion END: DEL Trada 10-18-2005

			txtMasLoc.Text = string.Empty;

			dtmEntryDate.Value = DBNull.Value;
			//dtmEntryDate.Text = String.Empty;

			txtReturnedGoodsNumber.Text = string.Empty;
			txtSaleOrderNo.Text = string.Empty;
			txtCustomerCode.Text = string.Empty;
			txtCustomerName.Text = string.Empty;
			txtCustomerLoc.Text = string.Empty;
			txtContact.Text = string.Empty;
			txtCurrency.Text = string.Empty;
			txtExchRate.Value = null;
			//			cboPartyLocation.SelectedIndex = -1;
			//			cboPartyLocation.Text = String.Empty;

			//			cboPartyContact.SelectedIndex = -1;
			//			cboPartyContact.Text = String.Empty;
		}
		//**************************************************************************              
		///    <Description>
		///       Lock or unlock all controls on form
		///    </Description>
		///    <Inputs>
		///       Lock Status : True or False
		///    </Inputs>
		///    <Outputs>
		///       The control will be locked or unlocked
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void LockForm(bool blnLock) 
		{
			const string METHOD_NAME = THIS + ".LockForm()";
			try 
			{
				txtMasLoc.ReadOnly = blnLock;
				cboCCN.ReadOnly = blnLock;
				dtmEntryDate.Enabled = !blnLock;
				//txtReturnedGoodsNumber.ReadOnly = blnLock;
				txtSaleOrderNo.ReadOnly = blnLock;
				txtCustomerCode.ReadOnly = blnLock;
				txtCustomerName.ReadOnly = blnLock;
				txtContact.ReadOnly = blnLock;
				txtCustomerLoc.ReadOnly = blnLock;
				txtCurrency.Enabled = blnLock;
				txtExchRate.Enabled = blnLock;
				btnCurrency.Enabled = blnLock;
				tgridViewData.AllowUpdate = !blnLock;
				tgridViewData.AllowDelete = !blnLock;
				tgridViewData.AllowAddNew = !blnLock;
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
		
		//**************************************************************************              
		///    <Description>
		///       Enable and Disable button according to each Form Status
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       Button will be disable or enable
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void EnableDisableButtons() 
		{
			switch (enumAction) 
			{
				case EnumAction.Add:
					//Disable Buttons
					btnAdd.Enabled = false;
					//btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					btnSearchReturnedGoods.Enabled = false;
					txtCurrency.Enabled = true;
					txtExchRate.Enabled = true;
					btnCurrency.Enabled = true;
					//Enable Buttons
					btnSave.Enabled = true;
					tgridViewData.AllowAddNew = true;
					tgridViewData.AllowDelete = true;
					tgridViewData.AllowUpdate = true;
					btnSearchSaleOrder.Enabled = true;
					btnSearchCustomerByCode.Enabled = true;
					btnSearchCustomerByName.Enabled = true;
					break;
				case EnumAction.Edit:
					//Disable Buttons
					btnAdd.Enabled = false;
					//btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					btnSearchReturnedGoods.Enabled = false;

					if (lblSaleOrderMasterID.Text.Trim() != String.Empty)
					{
						tgridViewData.AllowAddNew = false;
						//						cboPartyContact.ReadOnly = true;
						//						cboPartyLocation.ReadOnly = true;
						//txtCustomerCode.ReadOnly = true;
						//txtCustomerName.ReadOnly = true;
						btnSearchCustomerByCode.Enabled = false;
						btnSearchCustomerByName.Enabled = false;
					}
					else
					{
						tgridViewData.AllowAddNew = true;
						//						cboPartyContact.ReadOnly = false;
						//						cboPartyLocation.ReadOnly = false;
						//txtCustomerCode.ReadOnly = false;
						//txtCustomerName.ReadOnly = false;
						btnSearchCustomerByCode.Enabled = true;
						btnSearchCustomerByName.Enabled = true;

					}
					txtCurrency.Enabled = true;
					txtExchRate.Enabled = true;
					btnCurrency.Enabled = true;
					tgridViewData.AllowDelete = true;
					tgridViewData.AllowUpdate = true;
					btnSearchSaleOrder.Enabled = true;
					//btnSearchCustomerByCode.Enabled = true;
					//btnSearchCustomerByName.Enabled = true;
					
					//Enable Buttons
					btnSave.Enabled = true;
					txtMasLoc.ReadOnly = false;
					btnSearchMasLoc.Enabled = true;
					break;
				case EnumAction.Default:
					//Disable Buttons
					btnSave.Enabled = false;
					txtCurrency.Enabled = false;
					txtExchRate.Enabled = false;
					btnCurrency.Enabled = false;
					tgridViewData.AllowAddNew = false;
					tgridViewData.AllowDelete = false;
					tgridViewData.AllowUpdate = false;
					txtCustomerCode.ReadOnly = true;
					txtCustomerName.ReadOnly = true;
					txtCustomerLoc.ReadOnly = true;
					txtContact.ReadOnly = true;

					txtMasLoc.ReadOnly = false;
					btnSearchMasLoc.Enabled = true;

					btnAdd.Enabled = true;
					AllowSearchReturnedGoods(true);
					
					if (intReturnedGoodsMasterID > 0)
					{
						btnEdit.Enabled = true;
						btnDelete.Enabled = true;
					}
					else 
					{
						
					}

					//don't allow to search for sale order
					AllowSearchSaleOrder(false);

					//Don't allow to search for customer
					AllowSearchCustomer(false);
					break;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       Load returned goods detail into the grid
		///       and set the grid apprearance
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void LoadReturnedGoodsDetail(int pintReturnedGoodsMasterID) 
		{
			const string ID_FIELD = "ID";
			const string VALUE_FIELD = "VALUE";
			SOReturnGoodsReceiveBO objSOReturnGoodsReceiveBO = new SOReturnGoodsReceiveBO();
			dsReturnedGoodsDetail = objSOReturnGoodsReceiveBO.GetReturnGoodsDetail(pintReturnedGoodsMasterID);
			tgridViewData.DataSource = dsReturnedGoodsDetail.Tables[0];
			FormControlComponents.RestoreGridLayout(tgridViewData, dtbGridLayout);

			//Set apprearance for this grid
			//Centering heading 
			//Center Heading
			for (int i = 0; i < tgridViewData.Splits[0].DisplayColumns.Count; i++)
				tgridViewData.Splits[0].DisplayColumns[i].HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;
			
			tgridViewData.Splits[0].DisplayColumns[SO_ReturnedGoodsDetailTable.LINE_FLD].Locked = true;
			tgridViewData.Splits[0].DisplayColumns[SO_ReturnedGoodsDetailTable.LINE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
			tgridViewData.Splits[0].DisplayColumns[SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].Locked = true;
			tgridViewData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Locked = true;

			tgridViewData.Splits[0].DisplayColumns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].DropDownList = true;
			tgridViewData.Splits[0].DisplayColumns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].AutoDropDown = true;
			tgridViewData.Splits[0].DisplayColumns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].Button = true;

			tgridViewData.Columns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox;
			tgridViewData.Columns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ValueItems.Translate = true;
			tgridViewData.Columns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ValueItems.Validate = true;

			//Load data into this combo box
			DataTable dtQAStatus = objSOReturnGoodsReceiveBO.GetQAStatus();
			//Init data for this combo box
			foreach (DataRow drowQAStatus in dtQAStatus.Rows)
			{
				C1.Win.C1TrueDBGrid.ValueItem objItem = new C1.Win.C1TrueDBGrid.ValueItem(drowQAStatus[ID_FIELD].ToString(), drowQAStatus[VALUE_FIELD].ToString());
				tgridViewData.Columns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ValueItems.Values.Add(objItem);
			}

			//turn the coulumn into button column
			tgridViewData.Splits[0].DisplayColumns[SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD].Button = true;
			tgridViewData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
			tgridViewData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
			tgridViewData.Splits[0].DisplayColumns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Button = true;
			tgridViewData.Splits[0].DisplayColumns[MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD].Button = true;
			tgridViewData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Button = true;
			tgridViewData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Button = true;

			//format the Receive Quantity and Unit Price
			tgridViewData.Columns[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			tgridViewData.Columns[SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			tgridViewData.Columns[SO_ReturnedGoodsDetailTable.UNITPRICE_FLD].NumberFormat = "##############,0.0000";
			// allow to edit unit price field
			tgridViewData.Splits[0].DisplayColumns[SO_ReturnedGoodsDetailTable.UNITPRICE_FLD].Locked = false;
		}
		//**************************************************************************              
		///    <Description>
		///       Store the designed inteface of the grid 
		///       and later will get this setting back at runtime
		///       - Store Caption
		///       - Store Width
		///       - 
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void KeepTheGridDesign()
		{

			try 
			{
				//const string COL_ALIGN = "ALIGN";
				DataColumn dcolMyColumn ; 
				dtGridDesign = new DataTable("GRID");


				DataColumn[] dcolKey = new DataColumn[1];
			

				dcolMyColumn = new DataColumn();
				dcolMyColumn.DataType = System.Type.GetType("System.String");
				dcolMyColumn.ColumnName= GRID_COL_NAME;
				dtGridDesign.Columns.Add(dcolMyColumn);
				dcolKey[0] = dcolMyColumn; //set this column as the primary key

				dcolMyColumn = new DataColumn();
				dcolMyColumn.DataType = System.Type.GetType("System.String");
				dcolMyColumn.ColumnName= GRID_COL_CAPTION;
				dtGridDesign.Columns.Add(dcolMyColumn);


				dcolMyColumn = new DataColumn();
				dcolMyColumn.DataType = System.Type.GetType("System.String");
				dcolMyColumn.ColumnName= GRID_COL_WIDTH;
				dtGridDesign.Columns.Add(dcolMyColumn);

				dtGridDesign.PrimaryKey = dcolKey;

				DataRow drNewRow;
				for (int i=0; i<tgridViewData.Columns.Count;i++)
				{
					if (tgridViewData.Columns[i].DataField.Trim() != String.Empty)
					{
						drNewRow = dtGridDesign.NewRow();
						drNewRow[GRID_COL_NAME] = tgridViewData.Columns[i].DataField;
						drNewRow[GRID_COL_CAPTION] = tgridViewData.Columns[i].Caption;
						drNewRow[GRID_COL_WIDTH] = tgridViewData.Splits[0].DisplayColumns[i].Width;
						dtGridDesign.Rows.Add(drNewRow);
					}
				}
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       Load data for the first time when this form is runned
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void SOReturnGoodsReceive_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ProductItemInfo_Load()";
			try 
			{
				InventoryUtilsBO bo = new InventoryUtilsBO();

				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW,MessageBoxIcon.Warning);
					return;
				}

				enumAction = new EnumAction();
				enumAction = EnumAction.Default;
				intReturnedGoodsMasterID = -1; //No product at the load time

				//Keep the Grid Design (for multi languages)
				dtbGridLayout = FormControlComponents.StoreGridLayout(tgridViewData);
				KeepTheGridDesign();

				LoadDataForCombo();
				
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

				// END: Trada 10-18-2005
				//clear all controls value on form
				ClearForm();

				//lock all controls
				LockForm(true);

				//Diable or Enable buttons
				EnableDisableButtons();

				//Load ReturnedGoodsDetail
				LoadReturnedGoodsDetail(intReturnedGoodsMasterID);

				FormControlComponents.SetDefaultMasterLocation(txtMasLoc);
				dtmEntryDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				// set edit number format
				tgridViewData.Columns[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			}
			catch (PCSException ex) 
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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

		private void AllowSearchReturnedGoods(bool blnAllowSearch)
		{
			//txtReturnedGoodsNumber.ReadOnly = !blnAllowSearch;
			btnSearchReturnedGoods.Enabled = blnAllowSearch;
		}
		private void AllowSearchCustomer(bool blnAllowSearch)
		{
			//txtCustomerCode.ReadOnly = !blnAllowSearch;
			//txtCustomerName.ReadOnly = !blnAllowSearch;
			btnSearchCustomerByCode.Enabled = blnAllowSearch;
			btnSearchCustomerByName.Enabled = blnAllowSearch;
			btnCustomerLoc.Enabled = blnAllowSearch;
			btnContact.Enabled = blnAllowSearch;
		}
		private void AllowSearchSaleOrder(bool blnAllowSearch)
		{
			//txtSaleOrderNo.ReadOnly = !blnAllowSearch;
			btnSearchSaleOrder.Enabled = blnAllowSearch;
		}

		//**************************************************************************              
		///    <Description>
		///       Add a new Returned Goods transaction
		///       - Unlock Form
		///       - Input some default data
		///       
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try 
			{

				//Turn to Add action
				enumAction = EnumAction.Add;

				//Unlock form
				LockForm(false);

				//clear controls
				ClearForm();

				//Enable Button
				EnableDisableButtons();

				//Allow Search customer
				AllowSearchCustomer(true);
				//allow search Sale Order
				AllowSearchSaleOrder(true);
				//Don't allow search ReturnedGoods
				AllowSearchReturnedGoods(false);

				//grant some default value for this
				if (SystemProperty.CCNID >0)
					cboCCN.SelectedValue = SystemProperty.CCNID;
				else
					cboCCN.SelectedIndex = -1;

				//change the master location following the CCN
				ChangeMasterLocationFollowCCN();

				//get the default Entry Datetime

				UtilsBO objUtilsBO = new UtilsBO();
				DateTime dtDefaultDate = objUtilsBO.GetDBDate();
				dtmEntryDate.Value = dtDefaultDate;
				
				//get the default returned goods number
				txtReturnedGoodsNumber.Text = FormControlComponents.GetNoByMask(this);
				//Set default master location
				FormControlComponents.SetDefaultMasterLocation(txtMasLoc);
				intReturnedGoodsMasterID = -1;

				//Reload the gird 
				LoadReturnedGoodsDetail(intReturnedGoodsMasterID);

				txtMasLoc.Focus();
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
		//**************************************************************************              
		///    <Description>
		///       Before saving data into database 
		///       We have to validate all the mandatory control first
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private bool ValidateMandatoryControl() 
		{
			try 
			{
				if (cboCCN.SelectedIndex < 0) 
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
					cboCCN.Focus();
					return false;
				}
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
					txtMasLoc.Focus();
					return false;
				}
				if (dtmEntryDate.Value == DBNull.Value || dtmEntryDate.Text.Trim() == String.Empty) 
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
					dtmEntryDate.Focus();
					return false;
				}
				//check Date in the current period
				if (!FormControlComponents.CheckDateInCurrentPeriod((DateTime)dtmEntryDate.Value))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RTG_ENTRYDATE,MessageBoxIcon.Warning);
					dtmEntryDate.Focus();
					return false;
				}
				//check the PostDate smaller than the current date
				if ((DateTime) dtmEntryDate.Value >
					new UtilsBO().GetDBDate())
				{
					//MessageBox.Show("The Post Date you input is not in the current period");
					PCSMessageBox.Show(ErrorCode.MESSAGE_INV_TRANSACTION_CANNOT_IN_FUTURE,MessageBoxIcon.Warning);
					dtmEntryDate.Focus();
					return false;
				}
				if (txtCurrency.Text.Trim() == String.Empty) 
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
					txtCurrency.Focus();
					return false;
				}
				if (txtExchRate.Value == DBNull.Value) 
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
					txtExchRate.Focus();
					return false;
				}
				if (txtReturnedGoodsNumber.Text.Trim() == String.Empty) 
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
					txtReturnedGoodsNumber.Focus();
					return false;
				}
				if (txtCustomerCode.Text.Trim() == String.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
					txtCustomerCode.Focus();
					return false;
				}
				if (txtCustomerLoc.Text.Trim() == String.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
					txtCustomerLoc.Focus();
					return false;
				}
				for (int i =0; i <tgridViewData.RowCount; i++)
				{
					for (int j =i+1; j < tgridViewData.RowCount; j++)
					{
						if (tgridViewData[i, ITM_ProductTable.PRODUCTID_FLD].ToString() == tgridViewData[j, ITM_ProductTable.PRODUCTID_FLD].ToString())
						{
							PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Warning);
							tgridViewData.Col = tgridViewData.Columns.IndexOf(tgridViewData.Columns[ITM_ProductTable.CODE_FLD]);
							tgridViewData.Row = i;
							tgridViewData.Focus();
							return false;
						}
					}
				}

                int intDetailRows = tgridViewData.RowCount;
				
				if (intDetailRows <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_DETAIL,MessageBoxIcon.Warning);
					tgridViewData.Focus();
					tgridViewData.Row = 0;
					tgridViewData.Col = dsReturnedGoodsDetail.Tables[0].Columns.IndexOf(ITM_ProductTable.CODE_FLD);
					return false;
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       Assign the data on text boxes, combo boxes, into the VO class of the Returned Goods Master
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private SO_ReturnedGoodsMasterVO AssignValueToMaster()
		{
			try 
			{
				SO_ReturnedGoodsMasterVO objSO_ReturnedGoodsMasterVO = new SO_ReturnedGoodsMasterVO();
				SOReturnGoodsReceiveBO objSOReturnGoodsReceiveBO = new SOReturnGoodsReceiveBO();
			
				objSO_ReturnedGoodsMasterVO.ReturnedGoodsMasterID = intReturnedGoodsMasterID;
				objSO_ReturnedGoodsMasterVO.MasterLocationID = int.Parse(txtMasLoc.Tag.ToString());
				objSO_ReturnedGoodsMasterVO.ReturnedGoodsNumber = txtReturnedGoodsNumber.Text.Trim();
				objSO_ReturnedGoodsMasterVO.CurrencyID = int.Parse(txtCurrency.Tag.ToString());
				objSO_ReturnedGoodsMasterVO.ExchangeRate = Convert.ToDecimal(txtExchRate.Value);
				if (SystemProperty.UserName != String.Empty)
				{
					objSO_ReturnedGoodsMasterVO.ReceiverID = SystemProperty.EmployeeID;
				}
				DateTime dtmTransDate = (DateTime) dtmEntryDate.Value;
				objSO_ReturnedGoodsMasterVO.TransDate = new DateTime(dtmTransDate.Year, dtmTransDate.Month, dtmTransDate.Day, dtmTransDate.Hour, dtmTransDate.Minute, 0);
				// 24-04-2006 dungla: fix bug 3829 for NgaHT: update post date also
				objSO_ReturnedGoodsMasterVO.PostDate = objSO_ReturnedGoodsMasterVO.TransDate;
				// 24-04-2006 dungla: fix bug 3829 for NgaHT: update post date also
				if (txtSaleOrderNo.Text.Trim() != String.Empty)
				{
					objSO_ReturnedGoodsMasterVO.SaleOrderMasterID = int.Parse(lblSaleOrderMasterID.Text.Trim());
				}
				else
					objSO_ReturnedGoodsMasterVO.SaleOrderMasterID = 0;

				objSO_ReturnedGoodsMasterVO.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
				objSO_ReturnedGoodsMasterVO.PartyID = int.Parse(lblCustomerID.Text.Trim());
				objSO_ReturnedGoodsMasterVO.PartyLocationID = (int) txtCustomerLoc.Tag;
				if (txtContact.Text.Trim() != string.Empty)
				{
					objSO_ReturnedGoodsMasterVO.PartyContactID = (int) txtContact.Tag;
				}
				return objSO_ReturnedGoodsMasterVO;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       Save Data into Database
		///       It will call the SaveDatabase method
		///       and after that it will display a successful message 
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void SaveToDatabase()
		{
			DataSet	 dstTempl = dsReturnedGoodsDetail.Copy();
			try 
			{
				// synchronyze data
				FormControlComponents.SynchronyGridData(tgridViewData);
				//Init the BO class
				SOReturnGoodsReceiveBO objSOReturnGoodsReceiveBO = new SOReturnGoodsReceiveBO();
				switch (enumAction)
				{
					case EnumAction.Add:
						intReturnedGoodsMasterID = objSOReturnGoodsReceiveBO.AddNewReturnedGoods(AssignValueToMaster(),dsReturnedGoodsDetail);
						break;
				}
				LoadReturnedGoodsDetail(intReturnedGoodsMasterID);
			}
			catch (PCSException ex)
			{
				dsReturnedGoodsDetail = dstTempl.Copy();
				tgridViewData.Refresh();
				throw ex;
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       Closing form
		///       Before closing, it will check the status of the form
		///       and if the status is Add or edit, it will ask user to confirm if they want to save 
		///       the editted data or not
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void SOReturnGoodsReceive_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".SOReturnGoodsReceive_Closing()";
			try
			{
				if (enumAction == EnumAction.Add || enumAction == EnumAction.Edit)
				{
					System.Windows.Forms.DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					switch (dlgResult)
					{
						case DialogResult.Yes:
							btnSave_Click(btnSave, new EventArgs());
							if (blnHasError)
							{
								e.Cancel = true;
							}
							break;
						case DialogResult.No:
							e.Cancel = false;
							break;
						case DialogResult.Cancel:
							e.Cancel = true;
							break;
					}
				}
			}			
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
				e.Cancel = true;
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
				e.Cancel = true;

			}

		
		}

		//**************************************************************************              
		///    <Description>
		///       Implement the F12 key board to add a new record
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void SOReturnGoodsReceive_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".SOReturnGoodsReceive_KeyDown()";
			if (e.KeyCode == Keys.F12)
			{
				try
				{
					if (e.KeyCode == Keys.F12)
					{
						if (!tgridViewData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Locked)
						{
							tgridViewData.Row = tgridViewData.RowCount - 1;
							tgridViewData.Col = tgridViewData.Columns.IndexOf(tgridViewData.Columns[ITM_ProductTable.CODE_FLD]);
							tgridViewData.Focus();
							tgridViewData.EditActive = false;
						}
					}
				}
				catch (NoNullAllowedException ex) 
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					// log message.
					try
					{
						Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
					}
				}
				catch (ConstraintException ex) 
				{
					PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY);
					// log message.
					try
					{
						Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
					}
				}

				catch (Exception ex)
				{
					PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
					// log message.
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

		//**************************************************************************              
		///    <Description>
		///       This method will search for the existing product in the grid
		///       if it has, it will display an warning message to user
		///       and don't allow to add new
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private bool FindExistingProduct(int pintProductID, int intCurrentRow)
		{
			const string METHOD_NAME = THIS + ".FindExistingProduct()";
			try 
			{
				for (int i=0 ; i<tgridViewData.RowCount; i++)
				{
					if (i != intCurrentRow)
					{
						if (tgridViewData[i,SO_ReturnedGoodsDetailTable.PRODUCTID_FLD] != DBNull.Value 
							&& tgridViewData[i,SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString().Trim() != String.Empty
							&& int.Parse(tgridViewData[i,SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString().Trim()) == pintProductID)
						{
							return true;
						}
					}
				}
				return false;
			}				
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
				return true;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       Before saving into database, we also have to check data on the grid
		///       - Product
		///       - Unit of Measure
		///       - Received Quantity
		///       - Master location
		///       - Location
		///    </Description>
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
		///       Tuesday, February 22, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool CheckManatoryColumnForReturnedGoodsDetails()
		{
			for (int i =0; i <tgridViewData.RowCount; i++)
			{
				if(tgridViewData[i, SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
					tgridViewData.Row = i;
					tgridViewData.Col = dsReturnedGoodsDetail.Tables[0].Columns.IndexOf(SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD);
					return false;
				}
				if(tgridViewData[i, ITM_ProductTable.CODE_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Warning);
					tgridViewData.Row = i;
					tgridViewData.Col = dsReturnedGoodsDetail.Tables[0].Columns.IndexOf(ITM_ProductTable.CODE_FLD);
					return false;
				}
				if(tgridViewData[i, SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					tgridViewData.Row = i;
					tgridViewData.Col = dsReturnedGoodsDetail.Tables[0].Columns.IndexOf(SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD);
					return false;
				}
				if(tgridViewData[i, SO_ReturnedGoodsDetailTable.UNITPRICE_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					tgridViewData.Row = i;
					tgridViewData.Col = dsReturnedGoodsDetail.Tables[0].Columns.IndexOf(SO_ReturnedGoodsDetailTable.UNITPRICE_FLD);
					return false;
				}
				if(tgridViewData[i, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					tgridViewData.Row = i;
					tgridViewData.Col = dsReturnedGoodsDetail.Tables[0].Columns.IndexOf(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD);
					return false;
				}
				if(tgridViewData[i, MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					tgridViewData.Row = i;
					tgridViewData.Col = dsReturnedGoodsDetail.Tables[0].Columns.IndexOf(MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD);
					return false;
				}
				if(tgridViewData[i, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					tgridViewData.Row = i;
					tgridViewData.Col = dsReturnedGoodsDetail.Tables[0].Columns.IndexOf(MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD);
					return false;
				}
				if (tgridViewData[i, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].ToString() == string.Empty
					&& tgridViewData[i, MST_LocationTable.TABLE_NAME + MST_LocationTable.BIN_FLD].ToString() == true.ToString())
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MATERIALISSUE_SELECT_BIN, MessageBoxIcon.Warning);
					tgridViewData.Row = i;
					tgridViewData.Col = dsReturnedGoodsDetail.Tables[0].Columns.IndexOf(MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD);
					return false;
				}
			}
			if (txtSaleOrderNo.Text.Trim() != string.Empty)
			{
				for (int i =0; i <tgridViewData.RowCount; i++)
				{
					if( (decimal) tgridViewData[i, SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD]
						>	(decimal) tgridViewData[i, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD])
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_RECEIVEQTYTOCOMMIT, MessageBoxIcon.Error);
						tgridViewData.Row = i;
						tgridViewData.Col = dsReturnedGoodsDetail.Tables[0].Columns.IndexOf(SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD);
						return false;
					}
				}
			}
			return true;
		}


		#region Button Click Event

		//**************************************************************************              
		///    <Description>
		///       Implement the Sale Order Search
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnSearchSaleOrder_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchSaleOrder_Click()";
			try 
			{
				if(txtMasLoc.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_MASTERLOCATION);
					txtMasLoc.Focus();
					return;
				}

				Hashtable htbCondition = new Hashtable();
				htbCondition.Add(SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD, txtMasLoc.Tag);

				//Only Select the Sale Order that has commited quantity
				//strWhereClause = objSOReturnGoodsReceiveBO.BuildWhereClauseToSearchSaleOrder(int.Parse(txtMasLoc.Tag.ToString()));

				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(VIEW_SO_RETURNED_NAME, SO_SaleOrderMasterTable.CODE_FLD,txtSaleOrderNo.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					LoadSearchedSaleOrder(drwResult);
					txtSaleOrderNo.Tag = drwResult[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD];
					return;
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

		//**************************************************************************              
		///    <Description>
		///       Implement Search customer by name
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnSearchCustomerByName_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchCustomerByName_Click()";
			try 
			{
				Hashtable htbCriteria = new Hashtable();
				htbCriteria.Add(CUSTOMER, (int)PartyTypeEnum.CUSTOMER);
				DataRowView drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtCustomerName.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtCustomerCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtCustomerCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					UpdateSearchedCustomer(int.Parse(drwResult[MST_PartyTable.PARTYID_FLD].ToString()),
						drwResult[MST_PartyTable.CODE_FLD].ToString(),
						drwResult[MST_PartyTable.NAME_FLD].ToString());
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

		//**************************************************************************              
		///    <Description>
		///       Implement the Save button click
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".SaveToDatabase()";
			try 
			{
				if (tgridViewData.EditActive)
					return;
				DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIRM_BEFORE_SAVE_DATA, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dlgResult == DialogResult.No)
					return;
				blnHasError = true;
				if(Security.IsDifferencePrefix(this,lblRGR,txtReturnedGoodsNumber))
				{
					return;
				}
				if (!ValidateMandatoryControl())
				{
					return ;
				}
				if (!CheckManatoryColumnForReturnedGoodsDetails())
				{
					return;
				}
				SaveToDatabase();

				//Turn to Add action
				enumAction = EnumAction.Default;

				//lock form
				LockForm(true);

				//Enable Button
				EnableDisableButtons();

				//Don't allow to search for existing product
				AllowSearchReturnedGoods(true);
				AllowSearchCustomer(false);
				AllowSearchSaleOrder(false);
				Security.UpdateUserNameModifyTransaction(this, SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD, intReturnedGoodsMasterID);
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
				blnHasError = false;
			}			
			catch (PCSException ex)
			{
				//for lacking of UMRate
				if (ex.mCode == ErrorCode.MESSAGE_UMRATE_IS_NOT_CONFIGURATED)
				{
					string[] strUMCode = new string[2];
					int i =0;
					foreach (DictionaryEntry dicUMCode in ex.Hash)
					{
						strUMCode[i++] = dicUMCode.Value.ToString();
					}
					PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error, strUMCode);
					return;
				}

				// displays the error message.
				PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					txtReturnedGoodsNumber.Focus();
					return;
				}
				if (ex.mCode == ErrorCode.MESSAGE_RGA_RECEIVEQTYTOCOMMIT)
				{
					for (int i =0; i <tgridViewData.RowCount; i++)
					{
						if (tgridViewData[i, ITM_ProductTable.PRODUCTID_FLD].ToString() == ex.mMethod.ToString())
						{
							tgridViewData.Row = i;
							tgridViewData.Col = tgridViewData.Columns.IndexOf(tgridViewData.Columns[ITM_ProductTable.CODE_FLD]);
							tgridViewData.Focus();
							return;
						}
					}
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

		//**************************************************************************              
		///    <Description>
		///       Implement the Edit button click
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnEdit_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			if (intReturnedGoodsMasterID <= 0) 
			{
				//MessageBox.Show("You have to open an existing ReturnedGoodsNumber for edditting");
				
				PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTTOEDIT,MessageBoxIcon.Warning);
				return;
			}
			try 
			{
				//Turn to Add action
				enumAction = EnumAction.Edit;

				//Unlock form
				LockForm(false);

				//Enable Button
				EnableDisableButtons();

				//don't allow to search returned goods
				AllowSearchReturnedGoods(false);

				//focus on the master location
				txtMasLoc.Focus();

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

		//**************************************************************************              
		///    <Description>
		///       Implement the delete button click
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnDelete_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			if (intReturnedGoodsMasterID <= 0) 
			{
				//MessageBox.Show("You have to open an existing returned goods number for deletting");
				PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTTOEDIT,MessageBoxIcon.Warning);
				return;
			}
			if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes) 
			{
				try
				{
					//call the BO class to delete
					SOReturnGoodsReceiveBO objSOReturnGoodsReceiveBO = new SOReturnGoodsReceiveBO();
					objSOReturnGoodsReceiveBO.DeleteReturnedGoods(intReturnedGoodsMasterID);

					//After deleting, clean environment
					intReturnedGoodsMasterID = -1; //No returnedGoods after deleting



					//Turn to default action
					enumAction = EnumAction.Default;

					//unlock form
					LockForm(false);

					//clear controls
					ClearForm();

					//lock form
					LockForm(true);

					//Enable Button
					EnableDisableButtons();

					//allow to search for existing returned goods number
					AllowSearchReturnedGoods(true);

					//don't allow to search customer
					AllowSearchCustomer(false);

					//Don't allow to search sale order
					AllowSearchSaleOrder(false);

					
					//Refresh the grid
					dsReturnedGoodsDetail.Clear();
					dsReturnedGoodsDetail.AcceptChanges();
					tgridViewData.Refresh();

					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
				}
				catch (PCSException ex)
				{
					// displays the error message.
					PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
					// log message.
					try
					{
						Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
		
		}
		/// <summary>
		/// btnPrint_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPrint_Click()";
			try
			{				
				const string APPLICATION_PATH  = @"PCSMain\bin\Debug";
				const string REPORTFLD_TITLE = "fldTitle";
				const string REPORT_COMPANY_FLD = "fldCompany";
				const string RETURNGOODSRECEIPT_REPORT_LAYOUT = "ReturnGoodsReceiptSlip.xml";
				
				//return if no record was selected
				if(intReturnedGoodsMasterID <= 0)
				{
					return;
				}				
				
				this.Cursor = Cursors.WaitCursor;

				C1PrintPreviewDialog   printPreview = new C1PrintPreviewDialog();
				C1PrintPreviewDialogBO boDataReport = new C1PrintPreviewDialogBO();
			
				DataTable dtbResult;
				dtbResult = boDataReport.GetReturnGoodsReceiptByMasterID(intReturnedGoodsMasterID);

				// Check data source
				if(dtbResult == null)
				{
					this.Cursor = Cursors.Default;
					return;
				}

				ReportBuilder reportBuilder = new ReportBuilder();
			
				//Get actual application path
				string strReportPath = Application.StartupPath;
				int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
				if ( intIndex > -1 ) 
				{
					strReportPath = strReportPath.Substring(0, intIndex);
				}

				if(strReportPath.Substring(strReportPath.Length -1) == @"\")
				{
					strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
				}
				else
				{
					strReportPath += @"\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
			
				//Set datasource and lay-out path for reports
				reportBuilder.SourceDataTable = dtbResult;
				reportBuilder.ReportDefinitionFolder = strReportPath;
			
				reportBuilder.ReportLayoutFile = RETURNGOODSRECEIPT_REPORT_LAYOUT;

				//check if layout is valid
				if(reportBuilder.AnalyseLayoutFile())
				{					
					reportBuilder.UseLayoutFile = true;
				}
				else
				{
					this.Cursor = Cursors.Default;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
					return;
				}

				reportBuilder.MakeDataTableForRender();

				// and show it in preview dialog				
				
				reportBuilder.ReportViewer = printPreview.ReportViewer;
				reportBuilder.RenderReport();
		
				//Header information get from system params
				reportBuilder.DrawPredefinedField(REPORT_COMPANY_FLD, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
				reportBuilder.RefreshReport();				
			
				//Print report
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(REPORTFLD_TITLE).Text;
				}
				catch{}
				printPreview.Show();		
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch(Exception ex)
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
				this.Cursor = Cursors.Default;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       Implement the Returned Goods search button
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSearchReturnedGoods_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchReturnedGoods_Click()";
			try 
			{
				string strReturnValue = String.Empty;
				Hashtable htbCondition = new Hashtable();
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
					txtMasLoc.Focus();
					return;
				}
				htbCondition.Add(MST_MasterLocationTable.MASTERLOCATIONID_FLD, txtMasLoc.Tag.ToString());

				DataRowView drvResult = FormControlComponents.OpenSearchForm(SO_ReturnedGoodsMasterTable.TABLE_NAME,SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD, txtReturnedGoodsNumber.Text.Trim(), htbCondition, true);
				if (drvResult != null)
					strReturnValue = drvResult[SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD].ToString();
				if (strReturnValue != String.Empty)
				{
					intReturnedGoodsMasterID = int.Parse(strReturnValue);
					DisplayReturnedGoodsMasterInfo(int.Parse(strReturnValue));

					LoadReturnedGoodsDetail(int.Parse(strReturnValue));

					//Change the status of the button
					EnableDisableButtons();
				}
			}				
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
		private void btnCloseForm_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnSearchMasLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchMasLoc_Click()";
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
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text, htbCondition, true);
				if (drwResult != null)
				{
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					txtMasLoc.Tag = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
				}
				else
				{
					txtMasLoc.Focus();
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


		private void btnHelp_Click(object sender, EventArgs e)
		{
			PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT,MessageBoxIcon.Warning);
		}
		//**************************************************************************              
		///    <Description>
		///       Implement the Search customer button 
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void btnSearchCustomerByCode_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearchCustomerByCode_Click()";
			try 
			{
				Hashtable htbCriteria = new Hashtable();
				htbCriteria.Add(CUSTOMER, (int)PartyTypeEnum.CUSTOMER);
				DataRowView drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtCustomerCode.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					if (txtCustomerCode.Tag != null)
					{
						if (txtCustomerCode.Tag.ToString() != drwResult[MST_PartyTable.PARTYID_FLD].ToString())
						{
							txtCustomerLoc.Text = string.Empty;
							txtContact.Text = string.Empty;
							txtCustomerLoc.Tag = null;
							txtContact.Tag = null;
						}
					}
					txtCustomerCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtCustomerCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					UpdateSearchedCustomer(int.Parse(drwResult[MST_PartyTable.PARTYID_FLD].ToString()),
					                       drwResult[MST_PartyTable.CODE_FLD].ToString(),
					                       drwResult[MST_PartyTable.NAME_FLD].ToString());
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

		private void btnCustomerLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCustomerLoc_Click()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				if(txtCustomerCode.Text != string.Empty)
				{
					if(txtCustomerCode.Tag != null)
					{
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtCustomerCode.Tag);
					}
				}
				else
				{
					string[] strParam = new string[2];
					strParam[0]	= lblCustomer.Text;
					strParam[1] = lblCustomerLoc.Text;
					// You have to select Customer before select Buy Location, please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtCustomerCode.Focus();
					return;
				}
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtCustomerLoc.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtCustomerLoc.Tag  = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtCustomerLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
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

		private void btnContact_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnContact_Click()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				if(txtCustomerCode.Text != string.Empty)
				{
					if(txtCustomerCode.Tag != null)
					{
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtCustomerCode.Tag.ToString());
					}
				}
				else
				{
					string[] strParam = new string[2]; 
					strParam[0] = lblCustomer.Text;
					strParam[1] = lblContact.Text;
					// You have to select Vendor before , please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtContact.Text = string.Empty;
					txtCustomerCode.Focus();
					return;
				}
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_PartyContactTable.TABLE_NAME, MST_PartyContactTable.CODE_FLD, txtContact.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtContact.Tag = drwResult[MST_PartyContactTable.PARTYCONTACTID_FLD];
					txtContact.Text = drwResult[MST_PartyContactTable.CODE_FLD].ToString();
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

		#endregion

		#region Grid Events

		private void tgridViewData_OnAddNew(object sender, EventArgs e)
		{
			//assign the master id to the detail
			const string METHOD_NAME = THIS + ".tgridViewData_OnAddNew()";
			try 
			{
				tgridViewData[tgridViewData.Row,SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD] = intReturnedGoodsMasterID;
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

		//**************************************************************************              
		///    <Description>
		///       Implement the button click in the grid
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void tgridViewData_ButtonClick(object sender, ColEventArgs e)
		{
			if (!tgridViewData.AllowAddNew && !tgridViewData.AllowUpdate)
			{
				return;
			}
			const string METHOD_NAME = THIS + ".tgridViewData_ButtonClick()";
			try
			{
				DataRowView drwResult;
				Hashtable htbCondition = new Hashtable();
				if (tgridViewData.Columns[tgridViewData.Col].DataField == ITM_ProductTable.CODE_FLD || tgridViewData.Columns[tgridViewData.Col].DataField == ITM_ProductTable.DESCRIPTION_FLD)
				{
					if (txtSaleOrderNo.Text.Trim() != string.Empty)
					{
						htbCondition.Add(SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD, txtSaleOrderNo.Tag);
						if (tgridViewData.AddNewMode == AddNewModeEnum.AddNewCurrent)
							drwResult = FormControlComponents.OpenSearchForm(VIEW_ITEM_OF_SO, tgridViewData.Columns[tgridViewData.Col].DataField, tgridViewData[tgridViewData.Row, tgridViewData.Columns[tgridViewData.Col].DataField].ToString(), htbCondition, true);
						else
							drwResult = FormControlComponents.OpenSearchForm(VIEW_ITEM_OF_SO, tgridViewData.Columns[tgridViewData.Col].DataField, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), htbCondition, true);
					}
					else
					{
						if (txtCustomerCode.Text.Trim() == string.Empty)
							htbCondition.Add(SO_SaleOrderMasterTable.PARTYID_FLD, 0);
						else
							htbCondition.Add(SO_SaleOrderMasterTable.PARTYID_FLD, txtCustomerCode.Tag);

						if (txtCustomerLoc.Text == string.Empty)
							htbCondition.Add(SO_SaleOrderMasterTable.BUYINGLOCID_FLD, 0);
						else
							htbCondition.Add(SO_SaleOrderMasterTable.BUYINGLOCID_FLD, txtCustomerLoc.Tag);

						if (txtContact.Text.Trim() != string.Empty)
							htbCondition.Add(SO_SaleOrderMasterTable.PARTYCONTACTID_FLD, txtContact.Tag);

						if (tgridViewData.AddNewMode == AddNewModeEnum.AddNewCurrent)
							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCT_FOR_CUSTOMER_NAME, tgridViewData.Columns[tgridViewData.Col].DataField, tgridViewData[tgridViewData.Row, tgridViewData.Columns[tgridViewData.Col].DataField].ToString(), htbCondition, true);
						else
							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCT_FOR_CUSTOMER_NAME, tgridViewData.Columns[tgridViewData.Col].DataField, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						tgridViewData.EditActive = true;
						tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.LINE_FLD] = tgridViewData.Row + 1;
						tgridViewData[tgridViewData.Row, ITM_ProductTable.PRODUCTID_FLD] = drwResult[ITM_ProductTable.PRODUCTID_FLD];
						tgridViewData[tgridViewData.Row, ITM_ProductTable.CODE_FLD] = drwResult[ITM_ProductTable.CODE_FLD];
						try
						{
							tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITPRICE_FLD] = drwResult[SO_ReturnedGoodsDetailTable.UNITPRICE_FLD];
						}
						catch{}
						tgridViewData[tgridViewData.Row, ITM_ProductTable.DESCRIPTION_FLD] = drwResult[ITM_ProductTable.DESCRIPTION_FLD];
						tgridViewData[tgridViewData.Row, ITM_ProductTable.REVISION_FLD] = drwResult[ITM_ProductTable.REVISION_FLD];
						tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD] = drwResult[ITM_ProductTable.SELLINGUMID_FLD];
						tgridViewData[tgridViewData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drwResult[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
						tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD] = drwResult[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD];
						tgridViewData[tgridViewData.Row, MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD] = drwResult[MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD];
						tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.LOCATIONID_FLD] = drwResult[SO_ReturnedGoodsDetailTable.LOCATIONID_FLD];
						tgridViewData[tgridViewData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = drwResult[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD];
						tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BINID_FLD] = drwResult[SO_ReturnedGoodsDetailTable.BINID_FLD];
						tgridViewData[tgridViewData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = drwResult[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD];
						tgridViewData[tgridViewData.Row, ITM_ProductTable.STOCKUMID_FLD] = drwResult[ITM_ProductTable.STOCKUMID_FLD];
						tgridViewData[tgridViewData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.BIN_FLD] = drwResult[MST_LocationTable.BIN_FLD];
						if (txtMasLoc.Text != string.Empty)
						{
							try
							{
								tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD] = drwResult[SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD];
							}
							catch {}
						}
					}
				}

				if (tgridViewData.Columns[tgridViewData.Col].DataField == MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD)
				{
					if (tgridViewData[tgridViewData.Row, ITM_ProductTable.CODE_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_FIRSTSELECTPRO, MessageBoxIcon.Warning);
						tgridViewData.Col = tgridViewData.Columns.IndexOf(tgridViewData.Columns[ITM_ProductTable.CODE_FLD]);
						tgridViewData.Focus();
						return;
					}
					if (tgridViewData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME, MST_UnitOfMeasureTable.CODE_FLD, tgridViewData[tgridViewData.Row, tgridViewData.Columns[tgridViewData.Col].DataField].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME, MST_UnitOfMeasureTable.CODE_FLD, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						tgridViewData.EditActive = true;
						string[] strUMCode = new string[2];
						intStockUMID = int.Parse(tgridViewData[tgridViewData.Row, ITM_ProductTable.STOCKUMID_FLD].ToString());
						if (boUtils.GetUMRate((int) drwResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD], intStockUMID) == 0
							&& txtSaleOrderNo.Text.Trim() == string.Empty)
						{
							strUMCode[0] = ((MST_UnitOfMeasureVO) boUtils.GetUMInfor((int) drwResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD])).Code;
							strUMCode[1] = ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(intStockUMID)).Code;
							PCSMessageBox.Show(ErrorCode.MESSAGE_UMRATE_IS_NOT_CONFIGURATED, MessageBoxIcon.Error, strUMCode);
							return;
						}
						string strOldUMCode = string.Empty;
						if (tgridViewData[tgridViewData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].ToString() != string.Empty)
						{
							intOldUMID = int.Parse(tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString());
							strOldUMCode = tgridViewData[tgridViewData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].ToString();
						}
						tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD] = drwResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD];
						tgridViewData[tgridViewData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drwResult[MST_UnitOfMeasureTable.CODE_FLD];
						if (tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].ToString() != string.Empty
							&& intOldUMID != int.Parse(tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString()))
						{
							decimal decBLQ = decimal.Parse(tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].ToString());
							decimal decRealQuantity = QuantityByRateOfUM(intOldUMID, int.Parse(tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString()), decBLQ);
							if (decRealQuantity >0)
								tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD] = decRealQuantity; 
							else
							{
								strUMCode = new string[2];
								strUMCode[0] = ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(intOldUMID)).Code;
								strUMCode[1] = ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(int.Parse(tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString()))).Code;
								PCSMessageBox.Show(ErrorCode.MESSAGE_UMRATE_IS_NOT_CONFIGURATED, MessageBoxIcon.Error, strUMCode);
								tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD] = intOldUMID;
								tgridViewData[tgridViewData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = strOldUMCode;
								return;
							}
						}
						if (tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].ToString() == string.Empty
							&& intOldUMID != int.Parse(tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString()))
						{
							decimal decRealQuantity = QuantityByRateOfUM(intOldUMID, int.Parse(tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString()), 1);
							if (decRealQuantity <= 0)
							{
								strUMCode[0] = ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(intOldUMID)).Code;
								strUMCode[1] = ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(int.Parse(tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString()))).Code;
								PCSMessageBox.Show(ErrorCode.MESSAGE_UMRATE_IS_NOT_CONFIGURATED, MessageBoxIcon.Error, strUMCode);
								tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD] = intOldUMID;
								tgridViewData[tgridViewData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = strOldUMCode;
								return;
							}
						}
					}
				}
				//Select Confirm Ship No
				if (tgridViewData.Columns[tgridViewData.Col].DataField == SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD)
				{
					if (txtCustomerCode.Text == string.Empty)
					{
						string[] strParam = new string[2];
						strParam[0] = lblCustomer.Text;
						strParam[1] = tgridViewData.Columns[tgridViewData.Col].Caption;
						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
						txtCustomerCode.Focus();
						return;
					}
					else
					{
						htbCondition.Add(MST_PartyTable.PARTYID_FLD, txtCustomerCode.Tag);
						if (tgridViewData.AddNewMode == AddNewModeEnum.AddNewCurrent)
						{
							drwResult = FormControlComponents.OpenSearchForm(VIEW_CONFIRMSHIPBYCUSTOMER, SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD, tgridViewData[tgridViewData.Row, tgridViewData.Columns[tgridViewData.Col].DataField].ToString(), htbCondition, true);
						}
						else
						{
							drwResult = FormControlComponents.OpenSearchForm(VIEW_CONFIRMSHIPBYCUSTOMER,  SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), htbCondition, true);
						}
						if (drwResult != null)
						{
							tgridViewData.EditActive = true;
							tgridViewData[tgridViewData.Row,  SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD] = drwResult[ SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD].ToString();
							tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.CONFIRMSHIPMASTERID_FLD] = int.Parse(drwResult[SO_ReturnedGoodsDetailTable.CONFIRMSHIPMASTERID_FLD].ToString());
						}
					}
				}

				if (tgridViewData.Columns[tgridViewData.Col].DataField == MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD)
				{
					if (cboCCN.SelectedValue == null)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
						cboCCN.Focus();
						return;
					}
					else
					{
						htbCondition.Add(MST_CCNTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
						if (tgridViewData.AddNewMode == AddNewModeEnum.AddNewCurrent)
						{
							drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, tgridViewData[tgridViewData.Row, tgridViewData.Columns[tgridViewData.Col].DataField].ToString(), htbCondition, true);
						}
						else
						{
							drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), htbCondition, true);
						}
						if (drwResult != null)
						{
							tgridViewData.EditActive = true;
							if (tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD].ToString() != drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString())
							{
								tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.LOCATIONID_FLD] = DBNull.Value;
								tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BINID_FLD] = DBNull.Value;
								tgridViewData[tgridViewData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = DBNull.Value;
								tgridViewData[tgridViewData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = DBNull.Value;
							}
							tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD] = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
							tgridViewData[tgridViewData.Row, MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD] = drwResult[MST_MasterLocationTable.CODE_FLD];
						}
					}
				}
				if (tgridViewData.Columns[tgridViewData.Col].DataField == MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD)
				{
					if (tgridViewData[tgridViewData.Row, MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
						tgridViewData.Col = tgridViewData.Columns.IndexOf(tgridViewData.Columns[MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD]);
						tgridViewData.Focus();
						return;
					}
					else
					{
						htbCondition.Add(MST_MasterLocationTable.MASTERLOCATIONID_FLD, tgridViewData[tgridViewData.Row, MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());   
						if (tgridViewData.AddNewMode == AddNewModeEnum.AddNewCurrent)
						{
							drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, tgridViewData[tgridViewData.Row, tgridViewData.Columns[tgridViewData.Col].DataField].ToString(), htbCondition, true);
						}
						else
						{
							drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), htbCondition, true);
						}
						if (drwResult != null)
						{
							tgridViewData.EditActive = true;
							if (tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.LOCATIONID_FLD].ToString() != drwResult[MST_LocationTable.LOCATIONID_FLD].ToString())
							{
								tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BINID_FLD] = DBNull.Value;
								tgridViewData[tgridViewData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = DBNull.Value;
							}
							tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.LOCATIONID_FLD] = drwResult[MST_LocationTable.LOCATIONID_FLD];
							tgridViewData[tgridViewData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = drwResult[MST_LocationTable.CODE_FLD];
							tgridViewData[tgridViewData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.BIN_FLD] = drwResult[MST_LocationTable.BIN_FLD];
						}
					}
				}
				if (tgridViewData.Columns[tgridViewData.Col].DataField.ToUpper() == (MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD).ToUpper())
				{
					if (tgridViewData[tgridViewData.Row,MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, MessageBoxIcon.Warning);
						tgridViewData.Col = tgridViewData.Columns.IndexOf(tgridViewData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
						tgridViewData.Focus();
						return;
					}
					else
					{
						if (tgridViewData[tgridViewData.Row,MST_LocationTable.TABLE_NAME + MST_LocationTable.BIN_FLD].ToString() == true.ToString())
						{
							string strCondition = MST_BINTable.TABLE_NAME + "." + MST_BINTable.LOCATIONID_FLD + " = " 
								+ tgridViewData[tgridViewData.Row, MST_LocationTable.LOCATIONID_FLD].ToString();
							strCondition += " AND (" + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINTYPEID_FLD + " = " + ((int)BinTypeEnum.OK).ToString();
							strCondition += " OR " + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINTYPEID_FLD + " = " + ((int)BinTypeEnum.NG).ToString() + ")";
							//htbCondition.Add(MST_LocationTable.LOCATIONID_FLD, tgridViewData[tgridViewData.Row, MST_LocationTable.LOCATIONID_FLD].ToString());
							if (tgridViewData.AddNewMode == AddNewModeEnum.AddNewCurrent)
								drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, tgridViewData[tgridViewData.Row, tgridViewData.Columns[tgridViewData.Col].DataField].ToString(), strCondition);
							else
								drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), strCondition);
							if (drwResult != null)
							{
								tgridViewData.EditActive = true;
								tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BINID_FLD] = drwResult[MST_BINTable.BINID_FLD];
								tgridViewData[tgridViewData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = drwResult[MST_BINTable.CODE_FLD];
							}
						}
					}
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

		//**************************************************************************              
		///    <Description>
		///       Before update to each column
		///       We have to check it again
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void tgridViewData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
		{
			//check the input check quantity
			const string METHOD_NAME = THIS + ".tgridViewData_BeforeColUpdate()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (e.Column.DataColumn.Value.ToString() == string.Empty) return;
				if (tgridViewData.Columns[tgridViewData.Col].DataField == ITM_ProductTypeTable.CODE_FLD || 
					tgridViewData.Columns[tgridViewData.Col].DataField == ITM_ProductTable.DESCRIPTION_FLD)
				{
					if (txtCustomerCode.Text.Trim() == string.Empty)
						htbCriteria.Add(SO_SaleOrderMasterTable.PARTYID_FLD, 0);
					else
						htbCriteria.Add(SO_SaleOrderMasterTable.PARTYID_FLD, txtCustomerCode.Tag);

					if (txtCustomerLoc.Text == string.Empty)
						htbCriteria.Add(SO_SaleOrderMasterTable.BUYINGLOCID_FLD, 0);
					else
						htbCriteria.Add(SO_SaleOrderMasterTable.BUYINGLOCID_FLD, txtCustomerLoc.Tag);

					if (txtContact.Text.Trim() != string.Empty)
						htbCriteria.Add(SO_SaleOrderMasterTable.PARTYCONTACTID_FLD, txtContact.Tag);
					drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCT_FOR_CUSTOMER_NAME, tgridViewData.Columns[tgridViewData.Col].DataField, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), htbCriteria, false);
				}
				if (tgridViewData.Columns[tgridViewData.Col].DataField == MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD)
				{
					if (tgridViewData[tgridViewData.Row, ITM_ProductTable.CODE_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_FIRSTSELECTPRO, MessageBoxIcon.Warning);
						e.Cancel = true;
						return;
					}
					drwResult = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME, MST_UnitOfMeasureTable.CODE_FLD, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), htbCriteria, false);
					if (drwResult != null)
					{
						string[] strUMCode = new string[2];
						intStockUMID = int.Parse(tgridViewData[tgridViewData.Row, ITM_ProductTable.STOCKUMID_FLD].ToString());
						if (boUtils.GetUMRate((int) drwResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD], intStockUMID) == 0
							&& txtSaleOrderNo.Text.Trim() == string.Empty)
						{
							strUMCode[0] = ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(intStockUMID)).Code;
							strUMCode[1] = ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(int.Parse(drwResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD].ToString()))).Code;
							PCSMessageBox.Show(ErrorCode.MESSAGE_UMRATE_IS_NOT_CONFIGURATED, MessageBoxIcon.Error, strUMCode);
							e.Cancel = true;
							return;
						}
						if (tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString() != string.Empty)
						{
							intOldUMID = int.Parse(tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString());
						}
						if (tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].ToString() != string.Empty
							&& intOldUMID != int.Parse(drwResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD].ToString()))
						{
							decimal decBLQ = decimal.Parse(tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].ToString());
							decimal decRealQuantity = QuantityByRateOfUM(intOldUMID, int.Parse(drwResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD].ToString()), decBLQ);
							if (decRealQuantity >0)
								tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD] = decRealQuantity; 
							else
							{
								strUMCode[0] = ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(intOldUMID)).Code;
								strUMCode[1] = ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(int.Parse(drwResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD].ToString()))).Code;
								PCSMessageBox.Show(ErrorCode.MESSAGE_UMRATE_IS_NOT_CONFIGURATED, MessageBoxIcon.Error, strUMCode);
								e.Cancel = true;
								return;
							}
						}
						if (tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].ToString() == string.Empty
							&& intOldUMID != int.Parse(drwResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD].ToString()))
						{
							decimal decRealQuantity = QuantityByRateOfUM(intOldUMID, int.Parse(drwResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD].ToString()), 1);
							if (decRealQuantity <= 0)
							{
								strUMCode[0] = ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(intOldUMID)).Code;
								strUMCode[1] = ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(int.Parse(drwResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD].ToString()))).Code;
								PCSMessageBox.Show(ErrorCode.MESSAGE_UMRATE_IS_NOT_CONFIGURATED, MessageBoxIcon.Error, strUMCode);
								e.Cancel = true;
								return;
							}
						}
					}
				}
				//Select Confirm Ship No
				if (tgridViewData.Columns[tgridViewData.Col].DataField == SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD)
				{
					if (txtCustomerCode.Text != string.Empty)
					{
						htbCriteria.Add(MST_PartyTable.PARTYID_FLD, txtCustomerCode.Tag);
					}
					else
					{
						string[] strParam = new string[2];
						strParam[0] = lblCustomer.Text;
						strParam[1] = tgridViewData.Columns[tgridViewData.Col].Caption;
						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
						e.Cancel = true;
						return;
					}
					drwResult = FormControlComponents.OpenSearchForm(VIEW_CONFIRMSHIPBYCUSTOMER, SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), htbCriteria, false);
				}
				if (tgridViewData.Columns[tgridViewData.Col].DataField == MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD)
				{
					if (cboCCN.SelectedValue != null)
					{
						htbCriteria.Add(MST_CCNTable.CCNID_FLD, cboCCN.SelectedValue);
					}
					else
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
						e.Cancel = true;
						return;
					}
					drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), htbCriteria, false);
				}
				if (tgridViewData.Columns[tgridViewData.Col].DataField == MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD)
				{
					if (tgridViewData.Columns[MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD].Text != string.Empty)
					{
						htbCriteria.Add(MST_MasterLocationTable.MASTERLOCATIONID_FLD, tgridViewData[tgridViewData.Row,MST_MasterLocationTable.MASTERLOCATIONID_FLD]);
					}
					else
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
						e.Cancel = true;
						return;
					}
					drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), htbCriteria, false);
				}
				if (tgridViewData.Columns[tgridViewData.Col].DataField.ToUpper() == MST_BINTable.TABLE_NAME.ToUpper() + MST_BINTable.CODE_FLD.ToUpper())
				{
					string strCondition = string.Empty;
					if (tgridViewData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Text != string.Empty)
						//htbCriteria.Add(MST_LocationTable.LOCATIONID_FLD, tgridViewData[tgridViewData.Row,MST_LocationTable.LOCATIONID_FLD]);
					{
						strCondition = MST_BINTable.TABLE_NAME + "." + MST_BINTable.LOCATIONID_FLD + " = " + 
							tgridViewData[tgridViewData.Row,MST_LocationTable.LOCATIONID_FLD].ToString();
						strCondition += " AND (" +  MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINTYPEID_FLD + " = " + ((int)BinTypeEnum.OK).ToString();
						strCondition += " OR " +  MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINTYPEID_FLD + " = " + ((int)BinTypeEnum.NG).ToString() +")";
					}
					else
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, MessageBoxIcon.Warning);
						e.Cancel = true;
						return;
					}
					drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, tgridViewData.Columns[tgridViewData.Columns[tgridViewData.Col].DataField].Text.Trim(), strCondition);
				}
				if (tgridViewData.Columns[tgridViewData.Col].DataField == ITM_ProductTypeTable.CODE_FLD 
					|| tgridViewData.Columns[tgridViewData.Col].DataField == ITM_ProductTable.DESCRIPTION_FLD
					|| tgridViewData.Columns[tgridViewData.Col].DataField == MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD
					|| tgridViewData.Columns[tgridViewData.Col].DataField == MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD
					|| tgridViewData.Columns[tgridViewData.Col].DataField == MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD
					|| tgridViewData.Columns[tgridViewData.Col].DataField == SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD
					|| tgridViewData.Columns[tgridViewData.Col].DataField.ToUpper() == MST_BINTable.TABLE_NAME.ToUpper() + MST_BINTable.CODE_FLD.ToUpper()
					)
				{
					if (drwResult != null)
						e.Column.DataColumn.Tag = drwResult;
					else
					{
						e.Cancel = true;
						return;
					}
				}
				string strColumnName = tgridViewData.Columns[tgridViewData.Col].DataField;
				int intProductId = 0;
				switch (strColumnName)
				{
					case SO_ReturnedGoodsDetailTable.UNITPRICE_FLD:
						decimal decSellingPrice = 0;
						try
						{
							decSellingPrice = decimal.Parse(e.Column.DataColumn.Value.ToString());
							if (decSellingPrice <= 0)
							{
								PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_QTY, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
						}
						catch
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC,MessageBoxIcon.Warning);
							e.Cancel = true;
							return;
						}
						
						break;
					case SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD:
						decimal decReturnQuantity = 0;
						try
						{
							decReturnQuantity = decimal.Parse(e.Column.DataColumn.Value.ToString());
							if (decReturnQuantity <= 0)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_RECEIVEQTYTOZERO, MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
						}
						catch
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC,MessageBoxIcon.Warning);
							e.Cancel = true;
							return;
						}
						if (lblSaleOrderMasterID.Text.Trim() != String.Empty)
						{
							decimal intTotalCommit = 0;
							try
							{
								intTotalCommit = decimal.Parse(tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].ToString());
							}
							catch
							{
								if (txtSaleOrderNo.Text.Trim() != string.Empty
									&& tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].ToString() == string.Empty)
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_FIRSTSELECTPRO, MessageBoxIcon.Warning);
									e.Cancel = true;
									return;
								}
							}
				
							if (decReturnQuantity > intTotalCommit && txtSaleOrderNo.Text.Trim() != string.Empty)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_RECEIVEQTYTOCOMMIT,MessageBoxIcon.Warning);
								e.Cancel = true;
								return;
							}
							else
							{
								e.Cancel = false;			
								return;
							}
						}
						break;
				}
				//check for the duplicate Product ID
				if ((intProductId > 0) && (strColumnName == ITM_ProductTable.CODE_FLD
					|| strColumnName == ITM_ProductTable.DESCRIPTION_FLD 
					|| strColumnName == ITM_ProductTable.REVISION_FLD)) 
				{
					if (FindExistingProduct(intProductId,tgridViewData.Row))
					{
						//MessageBox.Show("This product is already existed, please select another product");
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_DUPLICATEPRO,MessageBoxIcon.Warning);
						e.Column.DataColumn.Tag = null;
						e.Cancel = true;
						return;
					}
				}
				e.Cancel = false;
			}			
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
			
			catch (Exception ex) 
			{
				e.Cancel = true;
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

		private void tgridViewData_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tgridViewData_KeyDown()";
			try 
			{
				switch (e.KeyCode)
				{
					case Keys.F4:
						if (btnSave.Enabled)
						{
							tgridViewData_ButtonClick(sender, null);
						}
						break;
					case Keys.Delete:
						if ((btnSave.Enabled == true)&&(tgridViewData.SelectedRows.Count > 0))
						{
							DeleteMultiRowsOnTrueDBGrid(tgridViewData, SO_ReturnedGoodsDetailTable.LINE_FLD);
						}
						break;
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

		private void tgridViewData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tgridViewData_AfterColUpdate()";
			try
			{
				if (tgridViewData.Columns[tgridViewData.Col].DataField == ITM_ProductTable.CODE_FLD
					|| tgridViewData.Columns[tgridViewData.Col].DataField == ITM_ProductTable.DESCRIPTION_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||( e.Column.DataColumn.Value.ToString() == string.Empty))
					{
						tgridViewData.Columns[ITM_ProductTable.CODE_FLD].Value = DBNull.Value;
						tgridViewData[tgridViewData.Row,ITM_ProductTable.PRODUCTID_FLD] = String.Empty;
						tgridViewData[tgridViewData.Row,ITM_ProductTable.DESCRIPTION_FLD] = String.Empty;
						tgridViewData[tgridViewData.Row,ITM_ProductTable.REVISION_FLD] = String.Empty;
						tgridViewData[tgridViewData.Row,MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD] = string.Empty;
						tgridViewData[tgridViewData.Row,MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = string.Empty;
						tgridViewData[tgridViewData.Row,MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = string.Empty;
						tgridViewData[tgridViewData.Row,MST_MasterLocationTable.MASTERLOCATIONID_FLD] = string.Empty;
						tgridViewData[tgridViewData.Row,MST_LocationTable.LOCATIONID_FLD] = string.Empty;
						tgridViewData[tgridViewData.Row,MST_BINTable.BINID_FLD] = string.Empty;
						tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD] = string.Empty;
						tgridViewData[tgridViewData.Row,ITM_ProductTable.STOCKUMID_FLD] = DBNull.Value;
						tgridViewData[tgridViewData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.BIN_FLD] = DBNull.Value;
					}
					else
					{
						if (e.Column.DataColumn.Tag != null)
						{
							DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
							tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.LINE_FLD] = tgridViewData.Row + 1;
							tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.UNITID_FLD] = drvResult[ITM_ProductTable.SELLINGUMID_FLD];
							tgridViewData[tgridViewData.Row,ITM_ProductTable.CODE_FLD] = drvResult[ITM_ProductTable.CODE_FLD];
							tgridViewData[tgridViewData.Row,ITM_ProductTable.PRODUCTID_FLD] = drvResult[ITM_ProductTable.PRODUCTID_FLD];
							tgridViewData[tgridViewData.Row,ITM_ProductTable.DESCRIPTION_FLD] = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
							tgridViewData[tgridViewData.Row,ITM_ProductTable.REVISION_FLD] = drvResult[ITM_ProductTable.REVISION_FLD];
							tgridViewData[tgridViewData.Row,MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD] = drvResult[MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD];
							tgridViewData[tgridViewData.Row,MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = drvResult[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD];
							tgridViewData[tgridViewData.Row,MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = drvResult[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD];
							tgridViewData[tgridViewData.Row,MST_MasterLocationTable.MASTERLOCATIONID_FLD] = drvResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
							tgridViewData[tgridViewData.Row,MST_LocationTable.LOCATIONID_FLD] = drvResult[MST_LocationTable.LOCATIONID_FLD];
							tgridViewData[tgridViewData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drvResult[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
							tgridViewData[tgridViewData.Row,MST_BINTable.BINID_FLD] = drvResult[MST_BINTable.BINID_FLD];
							tgridViewData[tgridViewData.Row,ITM_ProductTable.STOCKUMID_FLD] = drvResult[ITM_ProductTable.STOCKUMID_FLD];
							tgridViewData[tgridViewData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.BIN_FLD] = drvResult[MST_LocationTable.BIN_FLD];
							if (txtMasLoc.Text != string.Empty)
							{
								try
								{
									tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD] = drvResult[SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD];
								}
								catch{}
							}
						}
					}
				}
				//select Confirm Ship No
				if (tgridViewData.Columns[tgridViewData.Col].DataField == SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||( e.Column.DataColumn.Value.ToString() == string.Empty))
					{
						tgridViewData[tgridViewData.Row, SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD] = string.Empty;
						tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.CONFIRMSHIPMASTERID_FLD] = DBNull.Value;
					}
					else
					{
						if (e.Column.DataColumn.Tag != null)
						{
							DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
							tgridViewData[tgridViewData.Row, SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD] = drvResult[SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD].ToString();
							tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.CONFIRMSHIPMASTERID_FLD] = drvResult[SO_ReturnedGoodsDetailTable.CONFIRMSHIPMASTERID_FLD];
						}
					}
				}

				if (tgridViewData.Columns[tgridViewData.Col].DataField == MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||( e.Column.DataColumn.Value.ToString() == string.Empty))
					{
						tgridViewData[tgridViewData.Row,MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD] = DBNull.Value;
						tgridViewData[tgridViewData.Row,SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD] = DBNull.Value;

						tgridViewData[tgridViewData.Row,MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = DBNull.Value;
						tgridViewData[tgridViewData.Row,SO_ReturnedGoodsDetailTable.LOCATIONID_FLD] = DBNull.Value;

						tgridViewData[tgridViewData.Row,MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = DBNull.Value;
						tgridViewData[tgridViewData.Row,SO_ReturnedGoodsDetailTable.BINID_FLD] = DBNull.Value;
					}
					else
					{
						if (e.Column.DataColumn.Tag != null)
						{
							DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
							tgridViewData[tgridViewData.Row,MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD] = drvResult[MST_MasterLocationTable.CODE_FLD];
							tgridViewData[tgridViewData.Row,SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD] = drvResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
							// 25-04-2006 dungla: clear location and bin information
							tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.LOCATIONID_FLD] = DBNull.Value;
							tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BINID_FLD] = DBNull.Value;
							tgridViewData[tgridViewData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = DBNull.Value;
							tgridViewData[tgridViewData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = DBNull.Value;
							// 25-04-2006 dungla: clear location and bin information
						}
					}
				}
				if (tgridViewData.Columns[tgridViewData.Col].DataField == MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||( e.Column.DataColumn.Value.ToString() == string.Empty))
					{
						tgridViewData[tgridViewData.Row,MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = DBNull.Value;
						tgridViewData[tgridViewData.Row,SO_ReturnedGoodsDetailTable.LOCATIONID_FLD] = DBNull.Value;

						tgridViewData[tgridViewData.Row,MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = DBNull.Value;
						tgridViewData[tgridViewData.Row,SO_ReturnedGoodsDetailTable.BINID_FLD] = DBNull.Value;
						tgridViewData[tgridViewData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.BIN_FLD] = DBNull.Value;
					}
					else
					{
						if (e.Column.DataColumn.Tag != null)
						{
							DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
							tgridViewData[tgridViewData.Row,MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = drvResult[MST_LocationTable.CODE_FLD];
							tgridViewData[tgridViewData.Row,SO_ReturnedGoodsDetailTable.LOCATIONID_FLD] = drvResult[MST_LocationTable.LOCATIONID_FLD];
							// 25-04-2006 dungla: clear bin information
							tgridViewData[tgridViewData.Row, SO_ReturnedGoodsDetailTable.BINID_FLD] = DBNull.Value;
							tgridViewData[tgridViewData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = DBNull.Value;
							// 25-04-2006 dungla: clear bin information
						}
					}
				}
				if (tgridViewData.Columns[tgridViewData.Col].DataField.ToUpper() == MST_BINTable.TABLE_NAME.ToUpper() + MST_BINTable.CODE_FLD.ToUpper())
				{
					if ((e.Column.DataColumn.Tag == null) ||( e.Column.DataColumn.Value.ToString() == string.Empty))
					{
						tgridViewData[tgridViewData.Row,MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = DBNull.Value;
						tgridViewData[tgridViewData.Row,SO_ReturnedGoodsDetailTable.BINID_FLD] = DBNull.Value;
					}
					else
					{
						if (e.Column.DataColumn.Tag != null)
						{
							DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
							tgridViewData[tgridViewData.Row,MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = drvResult[MST_BINTable.CODE_FLD];
							tgridViewData[tgridViewData.Row,SO_ReturnedGoodsDetailTable.BINID_FLD] = drvResult[MST_BINTable.BINID_FLD];
						}
					}
				}
				if (tgridViewData.Columns[tgridViewData.Col].DataField == MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||( e.Column.DataColumn.Value.ToString() == string.Empty))
					{
						tgridViewData[tgridViewData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = DBNull.Value;
						tgridViewData[tgridViewData.Row,SO_ReturnedGoodsDetailTable.UNITID_FLD] = DBNull.Value;
					}
					else
					{
						if (e.Column.DataColumn.Tag != null)
						{
							DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
							tgridViewData[tgridViewData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drvResult[MST_UnitOfMeasureTable.CODE_FLD];
							tgridViewData[tgridViewData.Row,SO_ReturnedGoodsDetailTable.UNITID_FLD] = drvResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD];
						}
					}
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

		private void tgridViewData_AfterDelete(object sender, EventArgs e)
		{
			try
			{
				//				DeleteMultiRowsOnTrueDBGrid(tgridViewData, dsReturnedGoodsDetail.Tables[0], SO_ReturnedGoodsDetailTable.LINE_FLD);
			}
			catch{}
		}

		private void tgridViewData_Click(object sender, EventArgs e)
		{
		
		}

		private void tgridViewData_BeforeColEdit(object sender, BeforeColEditEventArgs e)
		{
			if (e.Column.DataColumn.DataField == MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD)
				if (tgridViewData[tgridViewData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.BIN_FLD].ToString() != true.ToString())
					e.Cancel = true;
		}

		#endregion

		private void UpdateSearchedCustomer(int intCustomerID, string strCustomerCode, string strCustomerName)
		{
			if (intCustomerID > 0)
				lblCustomerID.Text = intCustomerID.ToString();
			else
			{
				lblCustomerID.Text = String.Empty;
				txtCustomerCode.Text = string.Empty;
				txtCustomerName.Text = string.Empty;
				txtCustomerLoc.Text = string.Empty;
				txtContact.Text = string.Empty;
			}
			//Get list of commited sale order
			dtSaleOrderTotalCommit = new SOReturnGoodsReceiveBO().GetSaleOrderTotalCommit(0);

			//input this sale order into the receieved Goods detail
			InputNewSaleOrderDetailIntoReceivedGoods(dtSaleOrderTotalCommit);
			txtCustomerCode.Text = strCustomerCode;
			txtCustomerName.Text = strCustomerName;
		}
		//**************************************************************************              
		///    <Description>
		///       When user select an existing sale order
		///       - we have to delete all the current detail in the grid
		///       - get all the product item in this sale order that have commited 
		///       - and then input all of them into the grid
		///       - and then only allow user to edit , delete, don't allow add new
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void InputNewSaleOrderDetailIntoReceivedGoods(DataTable dtData)
		{
			const string ID_FIELD = "ID";
			const string VALUE_FIELD = "VALUE";
			try
			{
				dsReturnedGoodsDetail = new SOReturnGoodsReceiveBO().GetReturnGoodsDetail(0);
				tgridViewData.Refresh();
				int intLine = 0;
				foreach (DataRow drowTmp in dtData.Rows)
				{
					if (drowTmp[SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].ToString() != string.Empty && (decimal) drowTmp[SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD] > 0)
					{
						DataRow drowNewRow = dsReturnedGoodsDetail.Tables[0].NewRow();
						// product
						drowNewRow[SO_ReturnedGoodsDetailTable.LINE_FLD] = ++intLine;
						drowNewRow[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD] = drowTmp[SO_SaleOrderDetailTable.PRODUCTID_FLD];

						//product code
						drowNewRow[ITM_ProductTable.CODE_FLD] = drowTmp[ITM_ProductTable.CODE_FLD];

						//product description
						drowNewRow[ITM_ProductTable.DESCRIPTION_FLD] = drowTmp[ITM_ProductTable.DESCRIPTION_FLD];
						//revision
						drowNewRow[ITM_ProductTable.REVISION_FLD] = drowTmp[ITM_ProductTable.REVISION_FLD];
						//stockumid
						drowNewRow[ITM_ProductTable.STOCKUMID_FLD] = drowTmp[ITM_ProductTable.STOCKUMID_FLD];
						//Unit of measure
						drowNewRow[SO_ReturnedGoodsDetailTable.UNITID_FLD] = drowTmp[SO_SaleOrderDetailTable.SELLINGUMID_FLD];
						drowNewRow[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drowTmp[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
						//MasLoc 
						drowNewRow[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD] = txtMasLoc.Tag;
						drowNewRow[MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD] = txtMasLoc.Text.Trim();
						//Loc 
						drowNewRow[SO_ReturnedGoodsDetailTable.LOCATIONID_FLD] = drowTmp[SO_ReturnedGoodsDetailTable.LOCATIONID_FLD];
						drowNewRow[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = drowTmp[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD];
						//Bin 
						drowNewRow[SO_ReturnedGoodsDetailTable.BINID_FLD] = drowTmp[SO_ReturnedGoodsDetailTable.BINID_FLD];
						drowNewRow[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = drowTmp[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD];
						//lot
						drowNewRow[SO_ReturnedGoodsDetailTable.LOT_FLD] = drowTmp[SO_CommitInventoryDetailTable.LOT_FLD];
						//QAStatus
						//drowNewRow[SO_ReturnedGoodsDetailTable.QASTATUS_FLD] = drowTmp[SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD];
						//serial
						drowNewRow[SO_ReturnedGoodsDetailTable.SERIAL_FLD] = drowTmp[SO_CommitInventoryDetailTable.SERIAL_FLD];
						//Unit Price
						drowNewRow[SO_ReturnedGoodsDetailTable.UNITPRICE_FLD] = drowTmp[SO_ReturnedGoodsDetailTable.UNITPRICE_FLD];
						//Balance
						drowNewRow[SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD] = drowTmp[SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD];
						//Add saleorderdetail
						drowNewRow[SO_ReturnedGoodsDetailTable.SALEORDERDETAILID_FLD] = drowTmp[SO_ReturnedGoodsDetailTable.SALEORDERDETAILID_FLD];

						//
						drowNewRow[MST_LocationTable.TABLE_NAME + MST_LocationTable.BIN_FLD] = drowTmp[MST_LocationTable.BIN_FLD];
					
						//add this row
						dsReturnedGoodsDetail.Tables[0].Rows.Add(drowNewRow);
					}
				}
				tgridViewData.DataSource = dsReturnedGoodsDetail.Tables[0];
				FormControlComponents.RestoreGridLayout(tgridViewData, dtbGridLayout);

				//Set apprearance for this grid

				tgridViewData.Splits[0].DisplayColumns[SO_ReturnedGoodsDetailTable.LINE_FLD].Locked = true;
				tgridViewData.Splits[0].DisplayColumns[SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].Locked = true;
				tgridViewData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Locked = true;

				tgridViewData.Splits[0].DisplayColumns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].DropDownList = true;
				tgridViewData.Splits[0].DisplayColumns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].AutoDropDown = true;
				tgridViewData.Splits[0].DisplayColumns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].Button = true;

				tgridViewData.Columns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox;
				tgridViewData.Columns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ValueItems.Translate = true;
				tgridViewData.Columns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ValueItems.Validate = true;

				//Load data into this combo box
				DataTable dtQAStatus = new SOReturnGoodsReceiveBO().GetQAStatus();
				//Init data for this combo box
				foreach (DataRow drowQAStatus in dtQAStatus.Rows)
				{
					C1.Win.C1TrueDBGrid.ValueItem objItem = new C1.Win.C1TrueDBGrid.ValueItem(drowQAStatus[ID_FIELD].ToString(), drowQAStatus[VALUE_FIELD].ToString());
					tgridViewData.Columns[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ValueItems.Values.Add(objItem);
				}

				//turn the coulumn into button column
				tgridViewData.Splits[0].DisplayColumns[SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD].Button = true;
				tgridViewData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
				tgridViewData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
				tgridViewData.Splits[0].DisplayColumns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Button = true;
				tgridViewData.Splits[0].DisplayColumns[MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD].Button = true;
				tgridViewData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Button = true;
				tgridViewData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Button = true;

				//format the Receive Quantity and Unit Price
				tgridViewData.Columns[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				tgridViewData.Columns[SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				//tgridViewData.Columns[SO_ReturnedGoodsDetailTable.UNITPRICE_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				tgridViewData.Columns[SO_ReturnedGoodsDetailTable.UNITPRICE_FLD].NumberFormat = "##############,0.0000";
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		//**************************************************************************              
		///    <Description>
		///       Based on the ID that user selected
		///       we have to display the customer code, customer name
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void DisplayCustomerInfo(int pintPartyID)
		{
			try
			{
				if (pintPartyID != 0)
				{
					txtCustomerCode.ReadOnly = true;
					txtCustomerName.ReadOnly = true;
					btnSearchCustomerByCode.Enabled = false;
					btnSearchCustomerByName.Enabled = false;
					txtContact.ReadOnly = true;
					txtCustomerLoc.ReadOnly = true;
					btnContact.Enabled = false;
					btnCustomerLoc.Enabled = false;

					//first get the customer information
					SOReturnGoodsReceiveBO objSOReturnGoodsReceiveBO = new SOReturnGoodsReceiveBO();
					DataTable dtTmp = objSOReturnGoodsReceiveBO.GetCustomerInfo(pintPartyID);
					if (dtTmp.Rows.Count > 0) 
					{
						txtCustomerCode.Tag = pintPartyID;
						txtCustomerCode.Text = dtTmp.Rows[0][MST_PartyTable.CODE_FLD].ToString();
						txtCustomerName.Text = dtTmp.Rows[0][MST_PartyTable.NAME_FLD].ToString();
					}
				}
				else
				{
					btnSearchCustomerByCode.Enabled = true;
					btnSearchCustomerByName.Enabled = true;
					txtContact.ReadOnly = false;
					btnContact.Enabled = true;
					btnCustomerLoc.Enabled = true;
					txtCustomerCode.ReadOnly = false;
					txtCustomerLoc.ReadOnly = false;
					txtCustomerName.ReadOnly = false;
				}
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void LoadSearchedSaleOrder(DataRowView drwResult) 
		{
			try 
			{
				SOReturnGoodsReceiveBO objSOReturnGoodsReceiveBO = new SOReturnGoodsReceiveBO();
						
				//assign the Customer id, partyid, shipto location
				lblCustomerID.Text = drwResult[SO_SaleOrderMasterTable.PARTYID_FLD].ToString();
				if (drwResult[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD] != DBNull.Value
					&& drwResult[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD].ToString().Trim() != String.Empty)
				{
					txtCustomerLoc.Tag = drwResult[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD];
					DataRowView drvCusLoc = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.PARTYLOCATIONID_FLD, txtCustomerLoc.Tag.ToString(), null, false);
					if (drvCusLoc != null)
					{
						txtCustomerLoc.Text = drvCusLoc[MST_PartyLocationTable.CODE_FLD].ToString();
					}
				}
				else
				{
					txtCustomerLoc.Tag = null;
					txtCustomerLoc.Text = string.Empty;
				}
				if (drwResult[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD] != DBNull.Value
					&& drwResult[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD].ToString().Trim() != String.Empty)
				{
					txtContact.Tag = drwResult[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD];
					DataRowView drvCusLoc = FormControlComponents.OpenSearchForm(MST_PartyContactTable.TABLE_NAME, MST_PartyContactTable.PARTYCONTACTID_FLD, txtContact.Tag.ToString(), null, false);
					if (drvCusLoc != null)
					{
						txtContact.Text = drvCusLoc[MST_PartyContactTable.CODE_FLD].ToString();
					}
				}
				else
				{
					txtContact.Text = string.Empty;
				}
				//Assign the sale order 
				lblSaleOrderMasterID.Text = drwResult[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString();
				txtSaleOrderNo.Text = drwResult[SO_SaleOrderMasterTable.CODE_FLD].ToString();

				//Get list of commited sale order
				dtSaleOrderTotalCommit = objSOReturnGoodsReceiveBO.GetSaleOrderTotalCommit(int.Parse(lblSaleOrderMasterID.Text));

				//input this sale order into the receieved Goods detail
				InputNewSaleOrderDetailIntoReceivedGoods(dtSaleOrderTotalCommit);

				//Get the customer information for this sale order
				DisplayCustomerInfo(int.Parse(lblCustomerID.Text));
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		//**************************************************************************              
		///    <Description>
		///       When a user select an existing returned goods order
		///       - we have to display its information
		///       
		///    </Description>
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
		///       THIENHD
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		private void DisplayReturnedGoodsMasterInfo(int pintReturnedGoodsMasterID)
		{
			try
			{
				SOReturnGoodsReceiveBO objSOReturnGoodsReceiveBO = new SOReturnGoodsReceiveBO();

				intReturnedGoodsMasterID = pintReturnedGoodsMasterID;
				//get its information from database
				DataTable dtReturnedGoodsMaster = objSOReturnGoodsReceiveBO.GetReturnGoodsMaster(pintReturnedGoodsMasterID);

				if (dtReturnedGoodsMaster.Rows.Count <= 0) 
				{
					intReturnedGoodsMasterID = -1 ;

					dtmEntryDate.Value = DBNull.Value;
					txtSaleOrderNo.Text = string.Empty;
					lblSaleOrderMasterID.Text = string.Empty;

					txtCustomerCode.Text = string.Empty;
					txtCustomerName.Text = string.Empty;
					lblCustomerID.Text = string.Empty;
					txtCustomerLoc.Text = string.Empty;
					txtContact.Text = string.Empty;
					txtCurrency.Text = string.Empty;
					txtCurrency.Tag = null;
					txtExchRate.Value = null;

					txtReturnedGoodsNumber.Text = String.Empty;
					return;
				}
				//display the master location
				if (dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD] != DBNull.Value && dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD].ToString().Trim() != String.Empty) 
				{
					txtMasLoc.Tag = dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD].ToString().Trim();
					DataRowView drvCusLoc = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, SO_ReturnedGoodsMasterTable.MASTERLOCATIONID_FLD, txtMasLoc.Tag.ToString(), null, false);
					if (drvCusLoc != null)
					{
						txtMasLoc.Text = drvCusLoc[MST_MasterLocationTable.CODE_FLD].ToString();
					}
				}
				else
				{
					txtMasLoc.Text = string.Empty;
				}


				//Display the CCN
				cboCCN.SelectedValue = dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.CCNID_FLD].ToString();

				//Entry date
				dtmEntryDate.Value = dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.TRANSDATE_FLD];

				//Returned Goods Number
				txtReturnedGoodsNumber.Text = dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD].ToString();

				//Sale Order 
				txtSaleOrderNo.Text = dtReturnedGoodsMaster.Rows[0][SO_SaleOrderMasterTable.TABLE_NAME + SO_SaleOrderMasterTable.CODE_FLD].ToString().Trim();
				lblSaleOrderMasterID.Text = dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].ToString();

				//Customer
				txtCustomerCode.Text = dtReturnedGoodsMaster.Rows[0][MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].ToString().Trim();
				txtCustomerName.Text = dtReturnedGoodsMaster.Rows[0][MST_PartyTable.TABLE_NAME + MST_PartyTable.NAME_FLD].ToString();
				lblCustomerID.Text = dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.PARTYID_FLD].ToString();
				
				//Currency
				txtCurrency.Text = dtReturnedGoodsMaster.Rows[0][MST_CurrencyTable.TABLE_NAME + MST_CurrencyTable.CODE_FLD].ToString();
				txtCurrency.Tag = dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.CURRENCYID_FLD];
				txtExchRate.Value = dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.EXCHANGERATE_FLD];
				//Customer location
				if (dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD] != DBNull.Value && dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD].ToString().Trim() != String.Empty) 
				{
					txtCustomerLoc.Tag = dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.PARTYLOCATIONID_FLD].ToString().Trim();
					DataRowView drvCusLoc = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.PARTYLOCATIONID_FLD, txtCustomerLoc.Tag.ToString(), null, false);
					if (drvCusLoc != null)
					{
						txtCustomerLoc.Text = drvCusLoc[MST_PartyLocationTable.CODE_FLD].ToString();
					}
				}
				else
				{
					txtCustomerLoc.Tag = null;
				}

				//Customer contact
				if (dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD] != DBNull.Value && dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD].ToString().Trim() != String.Empty) 
				{
					txtContact.Tag = dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.PARTYCONTACTID_FLD].ToString().Trim();
					DataRowView drvCusLoc = FormControlComponents.OpenSearchForm(MST_PartyContactTable.TABLE_NAME, MST_PartyContactTable.PARTYCONTACTID_FLD, txtContact.Tag.ToString(), null, false);
					if (drvCusLoc != null)
					{
						txtContact.Text = drvCusLoc[MST_PartyContactTable.CODE_FLD].ToString();
					}
				}
				else
				{
					txtContact.Tag = null;
				}

				//Get the total commit for this sale order if 
				//Get list of commited sale order
				if (dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD] != DBNull.Value 
					&& int.Parse(dtReturnedGoodsMaster.Rows[0][SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD].ToString().Trim()) > 0)
				{
					dtSaleOrderTotalCommit = objSOReturnGoodsReceiveBO.GetSaleOrderTotalCommit(int.Parse(lblSaleOrderMasterID.Text));
				}
				else
				{
					dtSaleOrderTotalCommit = null;
				}
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private void dtmEntryDate_ValueChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmEntryDate_ValueChanged()";
			//get a new ReturnedGoodsNumber 
			//get the default returned goods number
			return;
			try 
			{
				UtilsBO objUtilsBO = new UtilsBO();
				txtReturnedGoodsNumber.Text = objUtilsBO.GetNoByMask(SO_ReturnedGoodsMasterTable.TABLE_NAME,SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD,(DateTime)dtmEntryDate.Value,"YYYYMMDD0000");
			}			
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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

		
		#region 28-04-2006 dungla: fix bug 3932 for NgaHT: Expected: Do not clear return goods number when change post date
		private void dtmEntryDate_Leave(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmEntryDate_Leave()";
			//get a new ReturnedGoodsNumber 
			//get the default returned goods number
			try 
			{
				
				UtilsBO objUtilsBO = new UtilsBO();
				if (dtmEntryDate.Value == DBNull.Value || dtmEntryDate.Text == String.Empty)
				{
					txtReturnedGoodsNumber.Text = String.Empty;
				}
				else
				{
					txtReturnedGoodsNumber.Text = FormControlComponents.GetNoByMask(this);
				}
			}			
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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

		#endregion

		private void LoadSearchReturnGoodsReceive(int intReturnGoodsMasterID)
		{
			try 
			{
				DisplayReturnedGoodsMasterInfo(intReturnGoodsMasterID);

				LoadReturnedGoodsDetail(intReturnGoodsMasterID);

				//Change the status of the button
				EnableDisableButtons();
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		private void txtReturnedGoodsNumber_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				if (btnSearchReturnedGoods.Enabled)
				{
					btnSearchReturnedGoods_Click(null,null);
				}
			}
		}

		private void txtSaleOrderNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				if (btnSearchSaleOrder.Enabled)
				{
					btnSearchSaleOrder_Click(null,null);
				}
			}
		}

		private void txtCustomerCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				if (btnSearchCustomerByCode.Enabled)
				{
					btnSearchCustomerByCode_Click(null,null);
				}
			}
		
		}

		private void txtCustomerName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				if (btnSearchCustomerByName.Enabled)
				{
					btnSearchCustomerByName_Click(null,null);
				}
			}
		
		}

		private void ChangeMasterLocationFollowCCN ()
		{
			try 
			{
				if (cboCCN.SelectedIndex < 0)
				{
					//if user doesn't select any CCN
					txtMasLoc.Text = String.Empty;
					txtMasLoc.ReadOnly = true;
				}
				else
				{
					txtMasLoc.ReadOnly = false;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void cboCCN_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if (enumAction != EnumAction.Default)
			{
				ChangeMasterLocationFollowCCN();
			}
		}

		private void dtmEntryDate_Enter(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ". OnEnterControl()";

			try

			{

				FormControlComponents.OnEnterControl(sender, e);

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

		private void cboCCN_Enter(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ". OnEnterControl()";
			try
			{
				FormControlComponents.OnEnterControl(sender, e);
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

		private void txtMasLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4 && btnSearchMasLoc.Enabled)
				{
					btnSearchMasLoc_Click(btnSearchMasLoc, new EventArgs());
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

		private void txtSaleOrderNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ". txtSaleOrderNo_Validating()";
			try
			{
				if (!btnSearchSaleOrder.Enabled || !txtSaleOrderNo.Modified)
				{
					return;
				}
				if (txtSaleOrderNo.Text.Trim() == String.Empty)
				{
					//clear all the loading data
					lblSaleOrderMasterID.Text = String.Empty;
					tgridViewData.AllowAddNew = true;
					//Assign the sale order 
					
					//Get list of commited sale order
					dtSaleOrderTotalCommit = new SOReturnGoodsReceiveBO().GetSaleOrderTotalCommit(0);

					//input this sale order into the receieved Goods detail
					InputNewSaleOrderDetailIntoReceivedGoods(dtSaleOrderTotalCommit);

					//Get the customer information for this sale order
					DisplayCustomerInfo(0);
					txtCustomerCode.ReadOnly = false;
					btnSearchCustomerByCode.Enabled = true;
					txtCustomerName.ReadOnly = false;
					btnSearchCustomerByName.Enabled = true;
					btnCustomerLoc.Enabled = true;
					txtContact.ReadOnly = false;
					btnContact.Enabled = true;

					txtCustomerLoc.ReadOnly = false;
					txtCustomerName.Text = string.Empty;
					txtCustomerCode.Text = string.Empty;
					txtContact.Text = string.Empty;
					txtCustomerLoc.Text = string.Empty;
					return;
				}
				lblSaleOrderMasterID.Text = String.Empty;
				if(txtMasLoc.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_MASTERLOCATION);
					e.Cancel = true;
					return;
				}

				//Search for this returned goods 
				Hashtable htbCondition = new Hashtable();
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
					e.Cancel = true;
					return;
				}
				htbCondition.Add(SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD, txtMasLoc.Tag);
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(VIEW_SO_RETURNED_NAME, SO_SaleOrderMasterTable.CODE_FLD,txtSaleOrderNo.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					LoadSearchedSaleOrder(drwResult);
					return;
				}
				else
					e.Cancel = true;
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

		private void txtCustomerCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ". txtCustomerCode_Validating()";
			try
			{
				if (!btnSearchCustomerByCode.Enabled || !txtCustomerCode.Modified)
				{
					return;
				}
				if (txtCustomerCode.Text == String.Empty)
				{
					UpdateSearchedCustomer(0,
						String.Empty,
						String.Empty);
					return;
				}
				Hashtable htbCriteria = new Hashtable();
				htbCriteria.Add(CUSTOMER, (int)PartyTypeEnum.CUSTOMER);
				DataRowView drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtCustomerCode.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					if (txtCustomerCode.Tag != null)
					{
						if (txtCustomerCode.Tag.ToString() != drwResult[MST_PartyTable.PARTYID_FLD].ToString())
						{
							txtCustomerLoc.Text = string.Empty;
							txtContact.Text = string.Empty;
							txtCustomerLoc.Tag = null;
							txtContact.Tag = null;
						}
					}
						
					txtCustomerCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtCustomerCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					UpdateSearchedCustomer(int.Parse(drwResult[MST_PartyTable.PARTYID_FLD].ToString()),
						drwResult[MST_PartyTable.CODE_FLD].ToString(),
						drwResult[MST_PartyTable.NAME_FLD].ToString());
				}
				else 
					e.Cancel = true;
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

		private void txtCustomerLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCustomerLoc_Validating()";
			try
			{
				if (!txtCustomerLoc.Modified) return;
				if (txtCustomerLoc.Text == string.Empty)
				{
					txtContact.Text = string.Empty;
					//Get list of commited sale order
					dtSaleOrderTotalCommit = new SOReturnGoodsReceiveBO().GetSaleOrderTotalCommit(0);

					//input this sale order into the receieved Goods detail
					InputNewSaleOrderDetailIntoReceivedGoods(dtSaleOrderTotalCommit);
					return;
				}
				Hashtable htbCriteria = new Hashtable();
				if(txtCustomerCode.Text != string.Empty)
				{
					if(txtCustomerCode.Tag != null)
					{
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtCustomerCode.Tag);
					}
				}
				else
				{
					string[] strParam = new string[2];
					strParam[0]	= lblCustomer.Text;
					strParam[1] = lblCustomerLoc.Text;
					// You have to select Customer before select Buy Location, please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					e.Cancel = true;
					return;
				}
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtCustomerLoc.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtCustomerLoc.Tag = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtCustomerLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
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

		private void txtMasLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_Validating()";
			try
			{
				if (!txtMasLoc.Modified) return;
				if (txtMasLoc.Text == string.Empty)
				{
					ClearForm();
					LoadReturnedGoodsDetail(0);
					return;
				}
				if (!txtMasLoc.Modified) return;
				Hashtable htbCriteria = new Hashtable();
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(SO_SaleOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					e.Cancel = true;
					return;
				}
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					txtMasLoc.Tag = int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
				}
				else
					e.Cancel = true;
			}
			catch(PCSException ex)
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
			catch(Exception ex)
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

		private void txtContact_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtContact_Validating()";
			try
			{
				if (!txtContact.Modified) return;
				if (txtContact.Text == string.Empty)
				{
					txtContact.Tag = null;
					//Get list of commited sale order
					dtSaleOrderTotalCommit = new SOReturnGoodsReceiveBO().GetSaleOrderTotalCommit(0);

					//input this sale order into the receieved Goods detail
					InputNewSaleOrderDetailIntoReceivedGoods(dtSaleOrderTotalCommit);
					return;
				}
				Hashtable htbCriteria = new Hashtable();
				if(txtCustomerCode.Text != string.Empty)
				{
					htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtCustomerCode.Tag.ToString());
				}
				else
				{
					string[] strParam = new string[2]; 
					strParam[0] = lblCustomer.Text;
					strParam[1] = lblContact.Text;
					// You have to select Vendor before , please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					e.Cancel = true;
					return;
				}
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_PartyContactTable.TABLE_NAME, MST_PartyContactTable.CODE_FLD, txtContact.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtContact.Tag = drwResult[MST_PartyContactTable.PARTYCONTACTID_FLD];
					txtContact.Text = drwResult[MST_PartyContactTable.CODE_FLD].ToString();
				}
				else 
					e.Cancel = true;
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				if (ex.mCode != ErrorCode.DUPLICATE_KEY && ex.mCode != ErrorCode.DUPLICATEKEY)
				{
					PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY,MessageBoxIcon.Error);
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

		private void txtCustomerLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4 && btnCustomerLoc.Enabled)
			{
				btnCustomerLoc_Click(btnCustomerLoc, new EventArgs());
			}
		}

		private void txtContact_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4 && btnContact.Enabled)
			{
				btnContact_Click(btnCustomerLoc, new EventArgs());
			}
		}

		private void txtReturnedGoodsNumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtReturnedGoodsNumber_Validating()";
			try
			{
				string strReturnValue = string.Empty;
				if (!txtReturnedGoodsNumber.Modified || !btnSearchReturnedGoods.Enabled) return;
				if (txtReturnedGoodsNumber.Text == String.Empty)
				{
					//clear all the loading data
					intReturnedGoodsMasterID = -1;
					LoadSearchReturnGoodsReceive(intReturnedGoodsMasterID);
					return;
				}
				Hashtable htbCondition = new Hashtable();
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
					e.Cancel = true;
					return;
				}
				htbCondition.Add(MST_MasterLocationTable.MASTERLOCATIONID_FLD, txtMasLoc.Tag.ToString());
				
				DataRowView drvResult = FormControlComponents.OpenSearchForm(SO_ReturnedGoodsMasterTable.TABLE_NAME,SO_ReturnedGoodsMasterTable.RETURNEDGOODSNUMBER_FLD, txtReturnedGoodsNumber.Text.Trim(), htbCondition, false);
				if (drvResult != null)
					strReturnValue = drvResult[SO_ReturnedGoodsMasterTable.RETURNEDGOODSMASTERID_FLD].ToString();
				if (strReturnValue != String.Empty)
				{
					DisplayReturnedGoodsMasterInfo(int.Parse(strReturnValue));

					LoadReturnedGoodsDetail(int.Parse(strReturnValue));

					//Change the status of the button
					EnableDisableButtons();
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
		/// Get exactly quanty belong to UM
		/// </summary>
		/// <param name="pintInUMID"></param>
		/// <param name="pintOutUMID"></param>
		/// <param name="pdecInQuantity"></param>
		/// <returns></returns>
		private decimal QuantityByRateOfUM(int pintInUMID, int pintOutUMID, decimal pdecInQuantity)
		{
			try
			{
				return pdecInQuantity*(new UtilsBO().GetUMRate(pintInUMID, pintOutUMID));
			}
			catch
			{
				return 0;
			}
		}

		private void txtMasLoc_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtCustomerName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ". txtCustomerName_Validating()";
			try
			{
				if (!btnSearchCustomerByName.Enabled || !txtCustomerName.Modified)
				{
					return;
				}
				if (txtCustomerName.Text.Trim() == String.Empty && lblCustomerID.Text == String.Empty)
				{
					return;
				}
				if (txtCustomerName.Text == String.Empty)
				{
					UpdateSearchedCustomer(0,
						String.Empty,
						String.Empty);
					return;
				}
				Hashtable htbCriteria = new Hashtable();
				htbCriteria.Add(CUSTOMER, (int)PartyTypeEnum.CUSTOMER);
				DataRowView drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtCustomerName.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtCustomerCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtCustomerCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					UpdateSearchedCustomer(int.Parse(drwResult[MST_PartyTable.PARTYID_FLD].ToString()),
						drwResult[MST_PartyTable.CODE_FLD].ToString(),
						drwResult[MST_PartyTable.NAME_FLD].ToString());
				}
				else 
					e.Cancel = true;
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
		
		/// <summary>
		/// Delete multiply Rows
		/// </summary>
		/// <param name="pdgrdData"></param>
		/// <param name="pstrUniqueFieldName"></param>
		public void DeleteMultiRowsOnTrueDBGrid(C1TrueDBGrid pdgrdData, string pstrUniqueFieldName)
		{
			//store the index of selectrows
			int intSelectRows = pdgrdData.SelectedRows.Count;
			ArrayList intIndexOfSelectedRows = new ArrayList();
			for (int i =0; i < intSelectRows; i++)
			{
				intIndexOfSelectedRows.Add(int.Parse(pdgrdData.SelectedRows[i].ToString()));
			}
			intIndexOfSelectedRows.Sort();

			//delete Rows
			for (int i = intSelectRows-1; i >= 0;  i--)
			{
				pdgrdData.Row = (int) intIndexOfSelectedRows[i];
				pdgrdData.Delete();
			}

			//reset value of line again
			for (int i =0; i <pdgrdData.RowCount; i++)
			{
				pdgrdData[i, pstrUniqueFieldName] = i +1 ;
			}

			return;
		}
		/// <summary>
		/// FillExchangeRate
		/// </summary>
		/// <param name="pintCurrencyID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, June 19 2006</date>
		private int FillExchangeRate(int pintCurrencyID)
		{
			// Fill Exch. Rate if the system configured the exchange rate get form Exchange Rate Table
			// based on currency and transaction date (begin date<= transaction date <= end date and approved)
			const decimal DEFAULT_RATE = 1;
			const string METHOD_NAME = THIS + ".FillExchangeRate()";
			int intExchangeRateID = 0;
			if (pintCurrencyID == 0) return intExchangeRateID;
			//•	If the currency is same as base(Home - CuongNT fixed) currency then the system automatically fill the number 1 to exchange rate field
			if(pintCurrencyID == SystemProperty.HomeCurrencyID)
			{
				txtExchRate.Value = DEFAULT_RATE;
				return intExchangeRateID;
			}
			try
			{
				if(dtmEntryDate.Value == DBNull.Value)
				{
					// Input Transaction date before execute this function
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_TRANSACTION_BEFORE,MessageBoxIcon.Exclamation);
					txtExchRate.Value = DEFAULT_RATE;
					dtmEntryDate.Focus();
					return intExchangeRateID;
				}
				DateTime dtmOrderDate = (DateTime)dtmEntryDate.Value;
				SaleOrderBO boOrder = new SaleOrderBO();
				MST_ExchangeRateVO voExchange = (MST_ExchangeRateVO) boOrder.GetExchangeRate(pintCurrencyID,dtmOrderDate);
				if(voExchange.ExchangeRateID == 0)
				{
					// Do not found any Exchange Rate records which (begin effective date <= the current date <= end of effective date)
					PCSMessageBox.Show(ErrorCode.MESSAGE_NOT_FOUND_EXCHANGE_RATE,MessageBoxIcon.Exclamation);
					return intExchangeRateID;
				}
				// fill value and return
				intExchangeRateID = voExchange.ExchangeRateID;
				txtExchRate.Value = voExchange.Rate;
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
			return intExchangeRateID;
		}
		/// <summary>
		/// btnCurrency_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, June 19 2006</date>
		private void btnCurrency_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCurrency_Click()";
			try
			{
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME, MST_CurrencyTable.CODE_FLD, txtCurrency.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtCurrency.Tag = drwResult[MST_CurrencyTable.CURRENCYID_FLD];
					txtCurrency.Text = drwResult[MST_CurrencyTable.CODE_FLD].ToString();
					txtExchRate.Tag = FillExchangeRate(int.Parse(drwResult[MST_CurrencyTable.CURRENCYID_FLD].ToString()));
					if (drwResult[MST_CurrencyTable.CURRENCYID_FLD].ToString() != SystemProperty.HomeCurrencyID.ToString())
					{
						txtExchRate.Enabled = true;
					}
					else
						txtExchRate.Enabled = false;
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
		/// txtCurrency_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, June 19 2006</date>
		private void txtCurrency_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCurrency_Validating()";
			try
			{
				if (!txtCurrency.Modified) return;
				if(txtCurrency.Text.Trim() == string.Empty)
				{
					txtCurrency.Tag = null;
					return;
				}
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME, MST_CurrencyTable.CODE_FLD, txtCurrency.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtCurrency.Tag = drwResult[MST_CurrencyTable.CURRENCYID_FLD];
					txtCurrency.Text = drwResult[MST_CurrencyTable.CODE_FLD].ToString();
					txtExchRate.Tag = FillExchangeRate(int.Parse(drwResult[MST_CurrencyTable.CURRENCYID_FLD].ToString()));
					if(drwResult[MST_CurrencyTable.CURRENCYID_FLD].ToString() != SystemProperty.HomeCurrencyID.ToString())
					{
						txtExchRate.Enabled = true;
					}
					else 
						txtExchRate.Enabled = false;
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
		/// txtCurrency_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, June 19 2006</date>
		private void txtCurrency_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (btnSave.Enabled && e.KeyCode == Keys.F4)
			{
				btnCurrency_Click(null, null);
			}
		}
	}
}
