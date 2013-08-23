using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComProduct.Items.BO;
using PCSComProduct.Items.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSComUtils.Common;
using PCSComSale.Order.BO;
using PCSComSale.Order.DS;
using PCSComUtils.Common.BO;

namespace PCSSale.Order
{
	/// <summary>
	/// Summary description for SaleOrder.
	/// </summary>
	public class SaleOrder : System.Windows.Forms.Form
	{
		#region My variable
		const string THIS = "PCSSale.Order.SaleOrder";
		const string DECIMAL_NUMBERFORMAT = "####################,0.00";
		const string FORMATED_ORDERNO = "YYYYMMDD0000";
		const decimal ONE_HUNDRED = 100;
		const int ONE = 1;

		SaleOrderBO boSaleOrder = new SaleOrderBO();

		//private DataRow drowMaster;		
		private DataSet dstSaleOrderDetail = new DataSet();
		private decimal decDiscountRate;
		const string TOTAL_DELIVERY_QTY = "TotalDelivery";
		const string TEMP_QTY_COL_NAME = "TempQty";
		const string SUMCOMMITQUANTITY_FLD = "SUMCommitQuantity";
		const string REMAIN_QTY = "RemainQty";
		String[] strParam = new string[2];
		DataSet dstSODetailDeliverySchedule = new DataSet();
		string strNewFile = string.Empty;
		private DataTable dtbGridDesign;
		private Hashtable htbCriteria;
		private DataRowView drwResult = null;
		//store query string of Sale Order report
		string strReportQuery = string.Empty;
		private string V_VENDORCUSTOMER = "V_VendorCustomer";
		private string CUSTOMER = "Customer";
		private PartyTypeEnum mPartyType;
		private SO_SaleOrderMasterVO voSaleOrderMaster = new SO_SaleOrderMasterVO();
		private bool blnIsChangedGrid = false;
		private bool blnHasError = false;
		private bool blnHasCommited = false;
		private EnumAction mFormAction = EnumAction.Default;
		public EnumAction FormAction
		{
			set{mFormAction = value;}
			get{return mFormAction;}
		}
		private int mSaleOrderMasterID = 0;
		public int SaleOrderMasterID
		{
			set{mSaleOrderMasterID = value;}
			get{return mSaleOrderMasterID;}
		}
		// DataTable dtbStoreGridLayout;
		#endregion My variable
		
		#region Generated variable controls

		private System.Windows.Forms.Button btnOrderNo;
		private System.Windows.Forms.TextBox txtCustomer;
		private System.Windows.Forms.TextBox txtOrderNo;
		private System.Windows.Forms.TextBox txtCustomerName;
		private System.Windows.Forms.Button btnCustomer;
		private System.Windows.Forms.Button btnCustomerName;
		private System.Windows.Forms.CheckBox chkSpecialTax;
		private System.Windows.Forms.CheckBox chkExportTax;
		private System.Windows.Forms.CheckBox chkVAT;
		private System.Windows.Forms.CheckBox chkShipCompleted;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid gridData;
		private System.Windows.Forms.Button btnDeliverySchedule;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnAdvance;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.ComboBox cboPriority;
		private System.Windows.Forms.TextBox txtCustomerPO;
		private C1.Win.C1Input.C1DateEdit cboTransDate;
		private System.Windows.Forms.Label lblPause;
		private System.Windows.Forms.Label lblCarrier;
		private System.Windows.Forms.Label lblDiscTerms;
		private System.Windows.Forms.Label lblDeliveryTerms;
		private System.Windows.Forms.Label lblPayTerms;
		private System.Windows.Forms.Label lblSalesType;
		private System.Windows.Forms.Label lblSalesRepres;
		private System.Windows.Forms.Label lblBillToLoc;
		private System.Windows.Forms.Label lblShipFromLoc;
		private System.Windows.Forms.Label lblShipToLoc;
		private System.Windows.Forms.Label lblExchRate;
		private System.Windows.Forms.Label lblCurrency;
		private System.Windows.Forms.Label lblCustPO;
		private System.Windows.Forms.Label lblContact;
		private System.Windows.Forms.Label lblBuyLoc;
		private System.Windows.Forms.Label lblCustomer;
		private System.Windows.Forms.Label lblOrderNo;
		private System.Windows.Forms.Label lblTransDate;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblSOHeader;
		private System.Windows.Forms.Label lblTotalSpecTax;
		private System.Windows.Forms.Label lblTotalVAT;
		private System.Windows.Forms.Label lblTotalDiscount;
		private System.Windows.Forms.Label lblTotalExpTax;
		private System.Windows.Forms.Label lblTotalAmount;
		private System.Windows.Forms.Label lblTotalNetAmount;
		private System.Windows.Forms.Label lblPriority;
		private System.Windows.Forms.GroupBox grpHeader;
		private C1.Win.C1Input.C1NumericEdit txtExchRate;
		private C1.Win.C1Input.C1NumericEdit txtTotalNetAmount;
		private C1.Win.C1Input.C1NumericEdit txtTotalAmount;
		private C1.Win.C1Input.C1NumericEdit txtTotalDiscount;
		private C1.Win.C1Input.C1NumericEdit txtTotalSpecTax;
		private C1.Win.C1Input.C1NumericEdit txtTotalVAT;
		private C1.Win.C1Input.C1NumericEdit txtTotalExpTax;
		private System.Windows.Forms.TextBox txtCurrency;
		private System.Windows.Forms.Button btnCurrency;
		private System.Windows.Forms.TextBox txtContact;
		private System.Windows.Forms.Button btnContact;
		private System.Windows.Forms.TextBox txtPayTerms;
		private System.Windows.Forms.TextBox txtDeliveryTerms;
		private System.Windows.Forms.TextBox txtDiscTerms;
		private System.Windows.Forms.Button btnPayTerms;
		private System.Windows.Forms.Button btnDeliveryTerms;
		private System.Windows.Forms.Button btnDiscTerms;
		private System.Windows.Forms.TextBox txtPause;
		private System.Windows.Forms.Button btnPause;
		private System.Windows.Forms.TextBox txtShipToLoc;
		private System.Windows.Forms.Button btnShipToLoc;
		private System.Windows.Forms.TextBox txtBillToLoc;
		private System.Windows.Forms.Button btnBillToLoc;
		private System.Windows.Forms.TextBox txtShipFromLoc;
		private System.Windows.Forms.Button btnShipFromLoc;
		private System.Windows.Forms.TextBox txtSalesRepres;
		private System.Windows.Forms.Button btnSalesRepres;
		private System.Windows.Forms.TextBox txtCarrier;
		private System.Windows.Forms.Button btnCarrier;
		private System.Windows.Forms.TextBox txtBuyLoc;
		private System.Windows.Forms.Button btnBuyLoc;
		private System.Windows.Forms.TextBox txtSalesType;
		private System.Windows.Forms.Button btnSalesType;
		private C1.C1Report.C1Report rptSOReport;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.ContextMenu ctxmnuImport;
		private System.Windows.Forms.MenuItem mnuImportNew;
		private System.Windows.Forms.MenuItem mnuImportUpdate;
		private System.Data.OleDb.OleDbConnection oconExcelFile;
		private System.Data.OleDb.OleDbCommand ocmdExcelSelect;
		private System.Data.OleDb.OleDbDataAdapter odadExcelFile;
		private System.Windows.Forms.OpenFileDialog dlgOpenImpFile;
		private System.Windows.Forms.Button btnAutoCancelSO;
		private System.Windows.Forms.TextBox txtGateType;
		private System.Windows.Forms.Button btnGateType;
		private System.Windows.Forms.Label lblGate;
		private System.Windows.Forms.Label lblGateType;
		private System.Windows.Forms.Button btnUpdateUnitPrice;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SaleOrder()
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

		#endregion Generated variable controls

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaleOrder));
            this.cboCCN = new C1.Win.C1List.C1Combo();
            this.btnOrderNo = new System.Windows.Forms.Button();
            this.lblPause = new System.Windows.Forms.Label();
            this.chkShipCompleted = new System.Windows.Forms.CheckBox();
            this.lblCarrier = new System.Windows.Forms.Label();
            this.lblDiscTerms = new System.Windows.Forms.Label();
            this.lblDeliveryTerms = new System.Windows.Forms.Label();
            this.lblPayTerms = new System.Windows.Forms.Label();
            this.lblSalesType = new System.Windows.Forms.Label();
            this.chkSpecialTax = new System.Windows.Forms.CheckBox();
            this.chkExportTax = new System.Windows.Forms.CheckBox();
            this.chkVAT = new System.Windows.Forms.CheckBox();
            this.lblSalesRepres = new System.Windows.Forms.Label();
            this.lblBillToLoc = new System.Windows.Forms.Label();
            this.lblShipFromLoc = new System.Windows.Forms.Label();
            this.lblShipToLoc = new System.Windows.Forms.Label();
            this.lblExchRate = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.txtCustomerPO = new System.Windows.Forms.TextBox();
            this.lblCustPO = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblBuyLoc = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.lblTransDate = new System.Windows.Forms.Label();
            this.lblCCN = new System.Windows.Forms.Label();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.lblSOHeader = new System.Windows.Forms.Label();
            this.btnCustomerName = new System.Windows.Forms.Button();
            this.gridData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.btnDeliverySchedule = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblTotalSpecTax = new System.Windows.Forms.Label();
            this.lblTotalVAT = new System.Windows.Forms.Label();
            this.lblTotalDiscount = new System.Windows.Forms.Label();
            this.lblTotalExpTax = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalNetAmount = new System.Windows.Forms.Label();
            this.btnAdvance = new System.Windows.Forms.Button();
            this.lblPriority = new System.Windows.Forms.Label();
            this.cboPriority = new System.Windows.Forms.ComboBox();
            this.cboTransDate = new C1.Win.C1Input.C1DateEdit();
            this.txtExchRate = new C1.Win.C1Input.C1NumericEdit();
            this.txtTotalNetAmount = new C1.Win.C1Input.C1NumericEdit();
            this.txtTotalAmount = new C1.Win.C1Input.C1NumericEdit();
            this.txtTotalExpTax = new C1.Win.C1Input.C1NumericEdit();
            this.txtTotalDiscount = new C1.Win.C1Input.C1NumericEdit();
            this.txtTotalSpecTax = new C1.Win.C1Input.C1NumericEdit();
            this.txtTotalVAT = new C1.Win.C1Input.C1NumericEdit();
            this.txtCurrency = new System.Windows.Forms.TextBox();
            this.btnCurrency = new System.Windows.Forms.Button();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.btnContact = new System.Windows.Forms.Button();
            this.txtPayTerms = new System.Windows.Forms.TextBox();
            this.txtDeliveryTerms = new System.Windows.Forms.TextBox();
            this.txtDiscTerms = new System.Windows.Forms.TextBox();
            this.txtSalesType = new System.Windows.Forms.TextBox();
            this.btnPayTerms = new System.Windows.Forms.Button();
            this.btnDeliveryTerms = new System.Windows.Forms.Button();
            this.btnDiscTerms = new System.Windows.Forms.Button();
            this.btnSalesType = new System.Windows.Forms.Button();
            this.txtPause = new System.Windows.Forms.TextBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.txtShipToLoc = new System.Windows.Forms.TextBox();
            this.btnShipToLoc = new System.Windows.Forms.Button();
            this.txtBillToLoc = new System.Windows.Forms.TextBox();
            this.btnBillToLoc = new System.Windows.Forms.Button();
            this.txtShipFromLoc = new System.Windows.Forms.TextBox();
            this.btnShipFromLoc = new System.Windows.Forms.Button();
            this.txtSalesRepres = new System.Windows.Forms.TextBox();
            this.btnSalesRepres = new System.Windows.Forms.Button();
            this.txtCarrier = new System.Windows.Forms.TextBox();
            this.btnCarrier = new System.Windows.Forms.Button();
            this.txtBuyLoc = new System.Windows.Forms.TextBox();
            this.btnBuyLoc = new System.Windows.Forms.Button();
            this.rptSOReport = new C1.C1Report.C1Report();
            this.btnImport = new System.Windows.Forms.Button();
            this.ctxmnuImport = new System.Windows.Forms.ContextMenu();
            this.mnuImportNew = new System.Windows.Forms.MenuItem();
            this.mnuImportUpdate = new System.Windows.Forms.MenuItem();
            this.oconExcelFile = new System.Data.OleDb.OleDbConnection();
            this.ocmdExcelSelect = new System.Data.OleDb.OleDbCommand();
            this.odadExcelFile = new System.Data.OleDb.OleDbDataAdapter();
            this.dlgOpenImpFile = new System.Windows.Forms.OpenFileDialog();
            this.btnAutoCancelSO = new System.Windows.Forms.Button();
            this.txtGateType = new System.Windows.Forms.TextBox();
            this.btnGateType = new System.Windows.Forms.Button();
            this.lblGate = new System.Windows.Forms.Label();
            this.lblGateType = new System.Windows.Forms.Label();
            this.btnUpdateUnitPrice = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalNetAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalExpTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalSpecTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalVAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptSOReport)).BeginInit();
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
            this.cboCCN.DropDownWidth = 200;
            this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCCN.EditorHeight = 15;
            this.cboCCN.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCCN.Images"))));
            this.cboCCN.ItemHeight = 15;
            this.cboCCN.Location = new System.Drawing.Point(898, 6);
            this.cboCCN.MatchEntryTimeout = ((long)(2000));
            this.cboCCN.MaxDropDownItems = ((short)(5));
            this.cboCCN.MaxLength = 32767;
            this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCCN.Name = "cboCCN";
            this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCCN.Size = new System.Drawing.Size(84, 21);
            this.cboCCN.TabIndex = 1;
            this.cboCCN.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboCCN.PropBag = resources.GetString("cboCCN.PropBag");
            // 
            // btnOrderNo
            // 
            this.btnOrderNo.AccessibleDescription = "";
            this.btnOrderNo.AccessibleName = "";
            this.btnOrderNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOrderNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOrderNo.Location = new System.Drawing.Point(205, 28);
            this.btnOrderNo.Name = "btnOrderNo";
            this.btnOrderNo.Size = new System.Drawing.Size(24, 20);
            this.btnOrderNo.TabIndex = 9;
            this.btnOrderNo.Text = "...";
            this.btnOrderNo.Click += new System.EventHandler(this.btnOrderNo_Click);
            // 
            // lblPause
            // 
            this.lblPause.AccessibleDescription = "";
            this.lblPause.AccessibleName = "";
            this.lblPause.ForeColor = System.Drawing.Color.Black;
            this.lblPause.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPause.Location = new System.Drawing.Point(432, 138);
            this.lblPause.Name = "lblPause";
            this.lblPause.Size = new System.Drawing.Size(80, 19);
            this.lblPause.TabIndex = 54;
            this.lblPause.Text = "Pause";
            this.lblPause.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkShipCompleted
            // 
            this.chkShipCompleted.AccessibleDescription = "";
            this.chkShipCompleted.AccessibleName = "";
            this.chkShipCompleted.Enabled = false;
            this.chkShipCompleted.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkShipCompleted.ForeColor = System.Drawing.Color.Black;
            this.chkShipCompleted.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkShipCompleted.Location = new System.Drawing.Point(440, 72);
            this.chkShipCompleted.Name = "chkShipCompleted";
            this.chkShipCompleted.Size = new System.Drawing.Size(98, 22);
            this.chkShipCompleted.TabIndex = 23;
            this.chkShipCompleted.Text = "Ship Completed";
            this.chkShipCompleted.Leave += new System.EventHandler(this.OnLeaveControl);
            this.chkShipCompleted.Enter += new System.EventHandler(this.OnEnterControl);
            // 
            // lblCarrier
            // 
            this.lblCarrier.AccessibleDescription = "";
            this.lblCarrier.AccessibleName = "";
            this.lblCarrier.ForeColor = System.Drawing.Color.Black;
            this.lblCarrier.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCarrier.Location = new System.Drawing.Point(204, 158);
            this.lblCarrier.Name = "lblCarrier";
            this.lblCarrier.Size = new System.Drawing.Size(85, 19);
            this.lblCarrier.TabIndex = 47;
            this.lblCarrier.Text = "Carrier";
            this.lblCarrier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDiscTerms
            // 
            this.lblDiscTerms.AccessibleDescription = "";
            this.lblDiscTerms.AccessibleName = "";
            this.lblDiscTerms.ForeColor = System.Drawing.Color.Black;
            this.lblDiscTerms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDiscTerms.Location = new System.Drawing.Point(432, 160);
            this.lblDiscTerms.Name = "lblDiscTerms";
            this.lblDiscTerms.Size = new System.Drawing.Size(80, 19);
            this.lblDiscTerms.TabIndex = 57;
            this.lblDiscTerms.Text = "Disc. Terms";
            this.lblDiscTerms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDeliveryTerms
            // 
            this.lblDeliveryTerms.AccessibleDescription = "";
            this.lblDeliveryTerms.AccessibleName = "";
            this.lblDeliveryTerms.ForeColor = System.Drawing.Color.Black;
            this.lblDeliveryTerms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDeliveryTerms.Location = new System.Drawing.Point(432, 182);
            this.lblDeliveryTerms.Name = "lblDeliveryTerms";
            this.lblDeliveryTerms.Size = new System.Drawing.Size(80, 19);
            this.lblDeliveryTerms.TabIndex = 60;
            this.lblDeliveryTerms.Text = "Delivery Terms";
            this.lblDeliveryTerms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDeliveryTerms.Visible = false;
            // 
            // lblPayTerms
            // 
            this.lblPayTerms.AccessibleDescription = "";
            this.lblPayTerms.AccessibleName = "";
            this.lblPayTerms.ForeColor = System.Drawing.Color.Black;
            this.lblPayTerms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPayTerms.Location = new System.Drawing.Point(204, 136);
            this.lblPayTerms.Name = "lblPayTerms";
            this.lblPayTerms.Size = new System.Drawing.Size(85, 19);
            this.lblPayTerms.TabIndex = 44;
            this.lblPayTerms.Text = "Payment Terms";
            this.lblPayTerms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSalesType
            // 
            this.lblSalesType.AccessibleDescription = "";
            this.lblSalesType.AccessibleName = "";
            this.lblSalesType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSalesType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSalesType.Location = new System.Drawing.Point(204, 114);
            this.lblSalesType.Name = "lblSalesType";
            this.lblSalesType.Size = new System.Drawing.Size(85, 19);
            this.lblSalesType.TabIndex = 41;
            this.lblSalesType.Text = "Sales Type";
            this.lblSalesType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkSpecialTax
            // 
            this.chkSpecialTax.AccessibleDescription = "";
            this.chkSpecialTax.AccessibleName = "";
            this.chkSpecialTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSpecialTax.ForeColor = System.Drawing.Color.Black;
            this.chkSpecialTax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkSpecialTax.Location = new System.Drawing.Point(898, 74);
            this.chkSpecialTax.Name = "chkSpecialTax";
            this.chkSpecialTax.Size = new System.Drawing.Size(82, 20);
            this.chkSpecialTax.TabIndex = 26;
            this.chkSpecialTax.Text = "Special Tax";
            this.chkSpecialTax.Leave += new System.EventHandler(this.OnLeaveControl);
            this.chkSpecialTax.Enter += new System.EventHandler(this.OnEnterControl);
            this.chkSpecialTax.CheckedChanged += new System.EventHandler(this.chkSpecialTax_CheckedChanged);
            // 
            // chkExportTax
            // 
            this.chkExportTax.AccessibleDescription = "";
            this.chkExportTax.AccessibleName = "";
            this.chkExportTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkExportTax.ForeColor = System.Drawing.Color.Black;
            this.chkExportTax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkExportTax.Location = new System.Drawing.Point(898, 52);
            this.chkExportTax.Name = "chkExportTax";
            this.chkExportTax.Size = new System.Drawing.Size(82, 20);
            this.chkExportTax.TabIndex = 25;
            this.chkExportTax.Text = "Export Tax";
            this.chkExportTax.Leave += new System.EventHandler(this.OnLeaveControl);
            this.chkExportTax.Enter += new System.EventHandler(this.OnEnterControl);
            this.chkExportTax.CheckedChanged += new System.EventHandler(this.chkExportTax_CheckedChanged);
            // 
            // chkVAT
            // 
            this.chkVAT.AccessibleDescription = "";
            this.chkVAT.AccessibleName = "";
            this.chkVAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkVAT.ForeColor = System.Drawing.Color.Black;
            this.chkVAT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkVAT.Location = new System.Drawing.Point(898, 31);
            this.chkVAT.Name = "chkVAT";
            this.chkVAT.Size = new System.Drawing.Size(82, 19);
            this.chkVAT.TabIndex = 24;
            this.chkVAT.Text = "VAT";
            this.chkVAT.Leave += new System.EventHandler(this.OnLeaveControl);
            this.chkVAT.Enter += new System.EventHandler(this.OnEnterControl);
            this.chkVAT.CheckedChanged += new System.EventHandler(this.chkVAT_CheckedChanged);
            // 
            // lblSalesRepres
            // 
            this.lblSalesRepres.AccessibleDescription = "";
            this.lblSalesRepres.AccessibleName = "";
            this.lblSalesRepres.ForeColor = System.Drawing.Color.Black;
            this.lblSalesRepres.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSalesRepres.Location = new System.Drawing.Point(5, 180);
            this.lblSalesRepres.Name = "lblSalesRepres";
            this.lblSalesRepres.Size = new System.Drawing.Size(79, 19);
            this.lblSalesRepres.TabIndex = 38;
            this.lblSalesRepres.Text = "Sales Repres";
            this.lblSalesRepres.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBillToLoc
            // 
            this.lblBillToLoc.AccessibleDescription = "";
            this.lblBillToLoc.AccessibleName = "";
            this.lblBillToLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblBillToLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBillToLoc.Location = new System.Drawing.Point(5, 136);
            this.lblBillToLoc.Name = "lblBillToLoc";
            this.lblBillToLoc.Size = new System.Drawing.Size(79, 19);
            this.lblBillToLoc.TabIndex = 32;
            this.lblBillToLoc.Text = "Bill To Loc";
            this.lblBillToLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShipFromLoc
            // 
            this.lblShipFromLoc.AccessibleDescription = "";
            this.lblShipFromLoc.AccessibleName = "";
            this.lblShipFromLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblShipFromLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblShipFromLoc.Location = new System.Drawing.Point(5, 158);
            this.lblShipFromLoc.Name = "lblShipFromLoc";
            this.lblShipFromLoc.Size = new System.Drawing.Size(79, 19);
            this.lblShipFromLoc.TabIndex = 35;
            this.lblShipFromLoc.Text = "Ship From";
            this.lblShipFromLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShipToLoc
            // 
            this.lblShipToLoc.AccessibleDescription = "";
            this.lblShipToLoc.AccessibleName = "";
            this.lblShipToLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblShipToLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblShipToLoc.Location = new System.Drawing.Point(5, 114);
            this.lblShipToLoc.Name = "lblShipToLoc";
            this.lblShipToLoc.Size = new System.Drawing.Size(79, 19);
            this.lblShipToLoc.TabIndex = 29;
            this.lblShipToLoc.Text = "Ship To Loc";
            this.lblShipToLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblExchRate
            // 
            this.lblExchRate.AccessibleDescription = "";
            this.lblExchRate.AccessibleName = "";
            this.lblExchRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblExchRate.ForeColor = System.Drawing.Color.Maroon;
            this.lblExchRate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblExchRate.Location = new System.Drawing.Point(283, 28);
            this.lblExchRate.Name = "lblExchRate";
            this.lblExchRate.Size = new System.Drawing.Size(65, 20);
            this.lblExchRate.TabIndex = 10;
            this.lblExchRate.Text = "Exch. Rate";
            this.lblExchRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrency
            // 
            this.lblCurrency.AccessibleDescription = "";
            this.lblCurrency.AccessibleName = "";
            this.lblCurrency.ForeColor = System.Drawing.Color.Maroon;
            this.lblCurrency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCurrency.Location = new System.Drawing.Point(283, 8);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(65, 18);
            this.lblCurrency.TabIndex = 4;
            this.lblCurrency.Text = "Currency";
            this.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCustomerPO
            // 
            this.txtCustomerPO.AccessibleDescription = "";
            this.txtCustomerPO.AccessibleName = "";
            this.txtCustomerPO.Location = new System.Drawing.Point(283, 180);
            this.txtCustomerPO.MaxLength = 20;
            this.txtCustomerPO.Name = "txtCustomerPO";
            this.txtCustomerPO.Size = new System.Drawing.Size(103, 20);
            this.txtCustomerPO.TabIndex = 51;
            this.txtCustomerPO.Leave += new System.EventHandler(this.OnLeaveControl);
            this.txtCustomerPO.Enter += new System.EventHandler(this.OnEnterControl);
            // 
            // lblCustPO
            // 
            this.lblCustPO.AccessibleDescription = "";
            this.lblCustPO.AccessibleName = "";
            this.lblCustPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCustPO.ForeColor = System.Drawing.Color.Black;
            this.lblCustPO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCustPO.Location = new System.Drawing.Point(204, 180);
            this.lblCustPO.Name = "lblCustPO";
            this.lblCustPO.Size = new System.Drawing.Size(85, 20);
            this.lblCustPO.TabIndex = 50;
            this.lblCustPO.Text = "Customer PO";
            this.lblCustPO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblContact
            // 
            this.lblContact.AccessibleDescription = "";
            this.lblContact.AccessibleName = "";
            this.lblContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblContact.ForeColor = System.Drawing.Color.Black;
            this.lblContact.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblContact.Location = new System.Drawing.Point(204, 72);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(85, 20);
            this.lblContact.TabIndex = 20;
            this.lblContact.Text = "Contact";
            this.lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.AccessibleDescription = "";
            this.txtCustomerName.AccessibleName = "";
            this.txtCustomerName.Location = new System.Drawing.Point(204, 50);
            this.txtCustomerName.MaxLength = 200;
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(204, 20);
            this.txtCustomerName.TabIndex = 15;
            this.txtCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerName_KeyDown);
            this.txtCustomerName.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtCustomerName.Validating += new System.ComponentModel.CancelEventHandler(this.txtCustomerName_Validating);
            // 
            // lblBuyLoc
            // 
            this.lblBuyLoc.AccessibleDescription = "";
            this.lblBuyLoc.AccessibleName = "";
            this.lblBuyLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBuyLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblBuyLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBuyLoc.Location = new System.Drawing.Point(5, 72);
            this.lblBuyLoc.Name = "lblBuyLoc";
            this.lblBuyLoc.Size = new System.Drawing.Size(73, 20);
            this.lblBuyLoc.TabIndex = 17;
            this.lblBuyLoc.Text = "Buy Loc.";
            this.lblBuyLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCustomer
            // 
            this.txtCustomer.AccessibleDescription = "";
            this.txtCustomer.AccessibleName = "";
            this.txtCustomer.Location = new System.Drawing.Point(80, 50);
            this.txtCustomer.MaxLength = 20;
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(96, 20);
            this.txtCustomer.TabIndex = 13;
            this.txtCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomer_KeyDown);
            this.txtCustomer.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtCustomer.Validating += new System.ComponentModel.CancelEventHandler(this.txtCustomer_Validating);
            // 
            // lblCustomer
            // 
            this.lblCustomer.AccessibleDescription = "";
            this.lblCustomer.AccessibleName = "";
            this.lblCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCustomer.ForeColor = System.Drawing.Color.Maroon;
            this.lblCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCustomer.Location = new System.Drawing.Point(5, 50);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(73, 20);
            this.lblCustomer.TabIndex = 12;
            this.lblCustomer.Text = "Customer";
            this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCustomer
            // 
            this.btnCustomer.AccessibleDescription = "";
            this.btnCustomer.AccessibleName = "";
            this.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCustomer.Location = new System.Drawing.Point(177, 50);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(24, 20);
            this.btnCustomer.TabIndex = 14;
            this.btnCustomer.Text = "...";
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.AccessibleDescription = "";
            this.txtOrderNo.AccessibleName = "";
            this.txtOrderNo.Location = new System.Drawing.Point(80, 28);
            this.txtOrderNo.MaxLength = 20;
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(124, 20);
            this.txtOrderNo.TabIndex = 8;
            this.txtOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderNo_KeyDown);
            this.txtOrderNo.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtOrderNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtOrderNo_Validating);
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.AccessibleDescription = "";
            this.lblOrderNo.AccessibleName = "";
            this.lblOrderNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOrderNo.ForeColor = System.Drawing.Color.Maroon;
            this.lblOrderNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOrderNo.Location = new System.Drawing.Point(5, 28);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(73, 20);
            this.lblOrderNo.TabIndex = 7;
            this.lblOrderNo.Text = "Order No.";
            this.lblOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTransDate
            // 
            this.lblTransDate.AccessibleDescription = "";
            this.lblTransDate.AccessibleName = "";
            this.lblTransDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTransDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblTransDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTransDate.Location = new System.Drawing.Point(5, 6);
            this.lblTransDate.Name = "lblTransDate";
            this.lblTransDate.Size = new System.Drawing.Size(73, 20);
            this.lblTransDate.TabIndex = 2;
            this.lblTransDate.Text = "Trans. Date";
            this.lblTransDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCCN
            // 
            this.lblCCN.AccessibleDescription = "";
            this.lblCCN.AccessibleName = "";
            this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCCN.Location = new System.Drawing.Point(866, 6);
            this.lblCCN.Name = "lblCCN";
            this.lblCCN.Size = new System.Drawing.Size(30, 20);
            this.lblCCN.TabIndex = 0;
            this.lblCCN.Text = "CCN";
            this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpHeader
            // 
            this.grpHeader.AccessibleDescription = "";
            this.grpHeader.AccessibleName = "";
            this.grpHeader.Location = new System.Drawing.Point(166, 100);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(462, 7);
            this.grpHeader.TabIndex = 28;
            this.grpHeader.TabStop = false;
            // 
            // lblSOHeader
            // 
            this.lblSOHeader.AccessibleDescription = "";
            this.lblSOHeader.AccessibleName = "";
            this.lblSOHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSOHeader.ForeColor = System.Drawing.Color.Black;
            this.lblSOHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSOHeader.Location = new System.Drawing.Point(5, 94);
            this.lblSOHeader.Name = "lblSOHeader";
            this.lblSOHeader.Size = new System.Drawing.Size(209, 20);
            this.lblSOHeader.TabIndex = 27;
            this.lblSOHeader.Text = "Sales Order Header Addition Information";
            this.lblSOHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCustomerName
            // 
            this.btnCustomerName.AccessibleDescription = "";
            this.btnCustomerName.AccessibleName = "";
            this.btnCustomerName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCustomerName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCustomerName.Location = new System.Drawing.Point(409, 50);
            this.btnCustomerName.Name = "btnCustomerName";
            this.btnCustomerName.Size = new System.Drawing.Size(24, 20);
            this.btnCustomerName.TabIndex = 16;
            this.btnCustomerName.Text = "...";
            this.btnCustomerName.Click += new System.EventHandler(this.btnCustomerName_Click);
            // 
            // gridData
            // 
            this.gridData.AccessibleDescription = "";
            this.gridData.AccessibleName = "";
            this.gridData.AllowSort = false;
            this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData.CaptionHeight = 17;
            this.gridData.GroupByCaption = "Drag a column header here to group by that column";
            this.gridData.Images.Add(((System.Drawing.Image)(resources.GetObject("gridData.Images"))));
            this.gridData.Location = new System.Drawing.Point(5, 206);
            this.gridData.Name = "gridData";
            this.gridData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.gridData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.gridData.PreviewInfo.ZoomFactor = 75;
            this.gridData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("gridData.PrintInfo.PageSettings")));
            this.gridData.RowHeight = 15;
            this.gridData.Size = new System.Drawing.Size(975, 182);
            this.gridData.TabIndex = 66;
            this.gridData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.gridData_BeforeColEdit);
            this.gridData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridData_ButtonClick);
            this.gridData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.gridData_BeforeColUpdate);
            this.gridData.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.gridData_BeforeDelete);
            this.gridData.OnAddNew += new System.EventHandler(this.gridData_OnAddNew);
            this.gridData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridData_KeyDown);
            this.gridData.AfterDelete += new System.EventHandler(this.gridData_AfterDelete);
            this.gridData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridData_AfterColUpdate);
            this.gridData.PropBag = resources.GetString("gridData.PropBag");
            // 
            // btnDeliverySchedule
            // 
            this.btnDeliverySchedule.AccessibleDescription = "";
            this.btnDeliverySchedule.AccessibleName = "";
            this.btnDeliverySchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeliverySchedule.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDeliverySchedule.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDeliverySchedule.Location = new System.Drawing.Point(795, 436);
            this.btnDeliverySchedule.Name = "btnDeliverySchedule";
            this.btnDeliverySchedule.Size = new System.Drawing.Size(63, 23);
            this.btnDeliverySchedule.TabIndex = 87;
            this.btnDeliverySchedule.Text = "De&l. Sch.";
            this.btnDeliverySchedule.Click += new System.EventHandler(this.btnDeliverySchedule_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleDescription = "";
            this.btnEdit.AccessibleName = "";
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEdit.Location = new System.Drawing.Point(128, 436);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(60, 23);
            this.btnEdit.TabIndex = 83;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = "";
            this.btnClose.AccessibleName = "";
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(920, 436);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 89;
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
            this.btnHelp.Location = new System.Drawing.Point(859, 436);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(60, 23);
            this.btnHelp.TabIndex = 88;
            this.btnHelp.Text = "&Help";
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleDescription = "";
            this.btnDelete.AccessibleName = "";
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(189, 436);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 23);
            this.btnDelete.TabIndex = 84;
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
            this.btnSave.Location = new System.Drawing.Point(67, 436);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 82;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleDescription = "";
            this.btnAdd.AccessibleName = "";
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(6, 436);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 23);
            this.btnAdd.TabIndex = 81;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleDescription = "";
            this.btnPrint.AccessibleName = "";
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrint.Location = new System.Drawing.Point(250, 436);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(60, 23);
            this.btnPrint.TabIndex = 85;
            this.btnPrint.Text = "&Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblTotalSpecTax
            // 
            this.lblTotalSpecTax.AccessibleDescription = "";
            this.lblTotalSpecTax.AccessibleName = "";
            this.lblTotalSpecTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalSpecTax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalSpecTax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalSpecTax.Location = new System.Drawing.Point(6, 413);
            this.lblTotalSpecTax.Name = "lblTotalSpecTax";
            this.lblTotalSpecTax.Size = new System.Drawing.Size(80, 20);
            this.lblTotalSpecTax.TabIndex = 69;
            this.lblTotalSpecTax.Text = "Total Spec Tax";
            this.lblTotalSpecTax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalVAT
            // 
            this.lblTotalVAT.AccessibleDescription = "";
            this.lblTotalVAT.AccessibleName = "";
            this.lblTotalVAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalVAT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalVAT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalVAT.Location = new System.Drawing.Point(6, 391);
            this.lblTotalVAT.Name = "lblTotalVAT";
            this.lblTotalVAT.Size = new System.Drawing.Size(80, 20);
            this.lblTotalVAT.TabIndex = 67;
            this.lblTotalVAT.Text = "Total VAT";
            this.lblTotalVAT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalDiscount
            // 
            this.lblTotalDiscount.AccessibleDescription = "";
            this.lblTotalDiscount.AccessibleName = "";
            this.lblTotalDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalDiscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalDiscount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalDiscount.Location = new System.Drawing.Point(184, 392);
            this.lblTotalDiscount.Name = "lblTotalDiscount";
            this.lblTotalDiscount.Size = new System.Drawing.Size(78, 19);
            this.lblTotalDiscount.TabIndex = 71;
            this.lblTotalDiscount.Text = "Total Discount";
            this.lblTotalDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalExpTax
            // 
            this.lblTotalExpTax.AccessibleDescription = "";
            this.lblTotalExpTax.AccessibleName = "";
            this.lblTotalExpTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalExpTax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalExpTax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalExpTax.Location = new System.Drawing.Point(184, 413);
            this.lblTotalExpTax.Name = "lblTotalExpTax";
            this.lblTotalExpTax.Size = new System.Drawing.Size(78, 20);
            this.lblTotalExpTax.TabIndex = 73;
            this.lblTotalExpTax.Text = "Total Exp. Tax";
            this.lblTotalExpTax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AccessibleDescription = "";
            this.lblTotalAmount.AccessibleName = "";
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalAmount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalAmount.Location = new System.Drawing.Point(360, 392);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(92, 19);
            this.lblTotalAmount.TabIndex = 75;
            this.lblTotalAmount.Text = "Total Amount";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalNetAmount
            // 
            this.lblTotalNetAmount.AccessibleDescription = "";
            this.lblTotalNetAmount.AccessibleName = "";
            this.lblTotalNetAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalNetAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalNetAmount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalNetAmount.Location = new System.Drawing.Point(360, 413);
            this.lblTotalNetAmount.Name = "lblTotalNetAmount";
            this.lblTotalNetAmount.Size = new System.Drawing.Size(92, 20);
            this.lblTotalNetAmount.TabIndex = 77;
            this.lblTotalNetAmount.Text = "Total Net Amount";
            this.lblTotalNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAdvance
            // 
            this.btnAdvance.AccessibleDescription = "";
            this.btnAdvance.AccessibleName = "";
            this.btnAdvance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdvance.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdvance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdvance.Location = new System.Drawing.Point(558, 391);
            this.btnAdvance.Name = "btnAdvance";
            this.btnAdvance.Size = new System.Drawing.Size(34, 20);
            this.btnAdvance.TabIndex = 79;
            this.btnAdvance.Text = "&>>";
            this.btnAdvance.Visible = false;
            this.btnAdvance.Click += new System.EventHandler(this.btnAdvance_Click);
            // 
            // lblPriority
            // 
            this.lblPriority.AccessibleDescription = "";
            this.lblPriority.AccessibleName = "";
            this.lblPriority.ForeColor = System.Drawing.Color.Black;
            this.lblPriority.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPriority.Location = new System.Drawing.Point(432, 116);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(80, 21);
            this.lblPriority.TabIndex = 52;
            this.lblPriority.Text = "Priority";
            this.lblPriority.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboPriority
            // 
            this.cboPriority.AccessibleDescription = "";
            this.cboPriority.AccessibleName = "";
            this.cboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPriority.ItemHeight = 13;
            this.cboPriority.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cboPriority.Location = new System.Drawing.Point(510, 114);
            this.cboPriority.Name = "cboPriority";
            this.cboPriority.Size = new System.Drawing.Size(46, 21);
            this.cboPriority.TabIndex = 53;
            this.cboPriority.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboPriority.Enter += new System.EventHandler(this.OnEnterControl);
            // 
            // cboTransDate
            // 
            this.cboTransDate.AccessibleDescription = "";
            this.cboTransDate.AccessibleName = "";
            // 
            // 
            // 
            this.cboTransDate.Calendar.AccessibleDescription = "";
            this.cboTransDate.Calendar.AccessibleName = "";
            this.cboTransDate.Calendar.DayNameLength = 1;
            this.cboTransDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cboTransDate.CustomFormat = "dd-MM-yyyy";
            this.cboTransDate.ErrorInfo.ShowErrorMessage = false;
            this.cboTransDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.cboTransDate.Location = new System.Drawing.Point(80, 6);
            this.cboTransDate.Name = "cboTransDate";
            this.cboTransDate.Size = new System.Drawing.Size(96, 20);
            this.cboTransDate.TabIndex = 3;
            this.cboTransDate.Tag = null;
            this.cboTransDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cboTransDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.cboTransDate.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboTransDate.Enter += new System.EventHandler(this.OnEnterControl);
            // 
            // txtExchRate
            // 
            this.txtExchRate.AccessibleDescription = "";
            this.txtExchRate.AccessibleName = "";
            // 
            // 
            // 
            this.txtExchRate.Calculator.AccessibleDescription = "";
            this.txtExchRate.Calculator.AccessibleName = "";
            this.txtExchRate.CustomFormat = "###############,0.00";
            this.txtExchRate.EmptyAsNull = true;
            this.txtExchRate.ErrorInfo.ShowErrorMessage = false;
            this.txtExchRate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtExchRate.Location = new System.Drawing.Point(344, 28);
            this.txtExchRate.Name = "txtExchRate";
            this.txtExchRate.Size = new System.Drawing.Size(64, 20);
            this.txtExchRate.TabIndex = 11;
            this.txtExchRate.Tag = null;
            this.txtExchRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtExchRate.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtExchRate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            this.txtExchRate.Leave += new System.EventHandler(this.txtExchRate_Leave);
            this.txtExchRate.Enter += new System.EventHandler(this.OnEnterControl);
            // 
            // txtTotalNetAmount
            // 
            this.txtTotalNetAmount.AccessibleDescription = "";
            this.txtTotalNetAmount.AccessibleName = "";
            this.txtTotalNetAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.txtTotalNetAmount.Calculator.AccessibleDescription = "";
            this.txtTotalNetAmount.Calculator.AccessibleName = "";
            this.txtTotalNetAmount.CustomFormat = "###############,0.00";
            this.txtTotalNetAmount.ErrorInfo.ShowErrorMessage = false;
            this.txtTotalNetAmount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtTotalNetAmount.Location = new System.Drawing.Point(452, 413);
            this.txtTotalNetAmount.Name = "txtTotalNetAmount";
            this.txtTotalNetAmount.ReadOnly = true;
            this.txtTotalNetAmount.Size = new System.Drawing.Size(98, 20);
            this.txtTotalNetAmount.TabIndex = 78;
            this.txtTotalNetAmount.TabStop = false;
            this.txtTotalNetAmount.Tag = null;
            this.txtTotalNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalNetAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalNetAmount.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.AccessibleDescription = "";
            this.txtTotalAmount.AccessibleName = "";
            this.txtTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.txtTotalAmount.Calculator.AccessibleDescription = "";
            this.txtTotalAmount.Calculator.AccessibleName = "";
            this.txtTotalAmount.CustomFormat = "###############,0.00";
            this.txtTotalAmount.ErrorInfo.ShowErrorMessage = false;
            this.txtTotalAmount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtTotalAmount.Location = new System.Drawing.Point(452, 391);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(98, 20);
            this.txtTotalAmount.TabIndex = 76;
            this.txtTotalAmount.TabStop = false;
            this.txtTotalAmount.Tag = null;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalAmount.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtTotalExpTax
            // 
            this.txtTotalExpTax.AccessibleDescription = "";
            this.txtTotalExpTax.AccessibleName = "";
            this.txtTotalExpTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.txtTotalExpTax.Calculator.AccessibleDescription = "";
            this.txtTotalExpTax.Calculator.AccessibleName = "";
            this.txtTotalExpTax.CustomFormat = "###############,0.00";
            this.txtTotalExpTax.ErrorInfo.ShowErrorMessage = false;
            this.txtTotalExpTax.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtTotalExpTax.Location = new System.Drawing.Point(262, 413);
            this.txtTotalExpTax.Name = "txtTotalExpTax";
            this.txtTotalExpTax.ReadOnly = true;
            this.txtTotalExpTax.Size = new System.Drawing.Size(98, 20);
            this.txtTotalExpTax.TabIndex = 74;
            this.txtTotalExpTax.TabStop = false;
            this.txtTotalExpTax.Tag = null;
            this.txtTotalExpTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalExpTax.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalExpTax.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtTotalDiscount
            // 
            this.txtTotalDiscount.AccessibleDescription = "";
            this.txtTotalDiscount.AccessibleName = "";
            this.txtTotalDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.txtTotalDiscount.Calculator.AccessibleDescription = "";
            this.txtTotalDiscount.Calculator.AccessibleName = "";
            this.txtTotalDiscount.CustomFormat = "###############,0.00";
            this.txtTotalDiscount.ErrorInfo.ShowErrorMessage = false;
            this.txtTotalDiscount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtTotalDiscount.Location = new System.Drawing.Point(262, 391);
            this.txtTotalDiscount.Name = "txtTotalDiscount";
            this.txtTotalDiscount.ReadOnly = true;
            this.txtTotalDiscount.Size = new System.Drawing.Size(98, 20);
            this.txtTotalDiscount.TabIndex = 72;
            this.txtTotalDiscount.TabStop = false;
            this.txtTotalDiscount.Tag = null;
            this.txtTotalDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalDiscount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalDiscount.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtTotalSpecTax
            // 
            this.txtTotalSpecTax.AccessibleDescription = "";
            this.txtTotalSpecTax.AccessibleName = "";
            this.txtTotalSpecTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.txtTotalSpecTax.Calculator.AccessibleDescription = "";
            this.txtTotalSpecTax.Calculator.AccessibleName = "";
            this.txtTotalSpecTax.CustomFormat = "###############,0.00";
            this.txtTotalSpecTax.ErrorInfo.ShowErrorMessage = false;
            this.txtTotalSpecTax.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtTotalSpecTax.Location = new System.Drawing.Point(86, 413);
            this.txtTotalSpecTax.Name = "txtTotalSpecTax";
            this.txtTotalSpecTax.ReadOnly = true;
            this.txtTotalSpecTax.Size = new System.Drawing.Size(98, 20);
            this.txtTotalSpecTax.TabIndex = 70;
            this.txtTotalSpecTax.TabStop = false;
            this.txtTotalSpecTax.Tag = null;
            this.txtTotalSpecTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalSpecTax.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalSpecTax.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtTotalVAT
            // 
            this.txtTotalVAT.AccessibleDescription = "";
            this.txtTotalVAT.AccessibleName = "";
            this.txtTotalVAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.txtTotalVAT.Calculator.AccessibleDescription = "";
            this.txtTotalVAT.Calculator.AccessibleName = "";
            this.txtTotalVAT.CustomFormat = "###############,0.00";
            this.txtTotalVAT.ErrorInfo.ShowErrorMessage = false;
            this.txtTotalVAT.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtTotalVAT.Location = new System.Drawing.Point(86, 391);
            this.txtTotalVAT.Name = "txtTotalVAT";
            this.txtTotalVAT.ReadOnly = true;
            this.txtTotalVAT.Size = new System.Drawing.Size(98, 20);
            this.txtTotalVAT.TabIndex = 68;
            this.txtTotalVAT.TabStop = false;
            this.txtTotalVAT.Tag = null;
            this.txtTotalVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalVAT.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalVAT.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtCurrency
            // 
            this.txtCurrency.AccessibleDescription = "";
            this.txtCurrency.AccessibleName = "";
            this.txtCurrency.Location = new System.Drawing.Point(344, 6);
            this.txtCurrency.Name = "txtCurrency";
            this.txtCurrency.Size = new System.Drawing.Size(64, 20);
            this.txtCurrency.TabIndex = 5;
            this.txtCurrency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrency_KeyDown);
            this.txtCurrency.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtCurrency.Validating += new System.ComponentModel.CancelEventHandler(this.txtCurrency_Validating);
            // 
            // btnCurrency
            // 
            this.btnCurrency.AccessibleDescription = "";
            this.btnCurrency.AccessibleName = "";
            this.btnCurrency.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCurrency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCurrency.Location = new System.Drawing.Point(409, 6);
            this.btnCurrency.Name = "btnCurrency";
            this.btnCurrency.Size = new System.Drawing.Size(24, 20);
            this.btnCurrency.TabIndex = 6;
            this.btnCurrency.Text = "...";
            this.btnCurrency.Click += new System.EventHandler(this.btnCurrency_Click);
            // 
            // txtContact
            // 
            this.txtContact.AccessibleDescription = "";
            this.txtContact.AccessibleName = "";
            this.txtContact.Location = new System.Drawing.Point(284, 72);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(124, 20);
            this.txtContact.TabIndex = 21;
            this.txtContact.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContact_KeyDown);
            this.txtContact.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtContact.Validating += new System.ComponentModel.CancelEventHandler(this.txtContact_Validating);
            // 
            // btnContact
            // 
            this.btnContact.AccessibleDescription = "";
            this.btnContact.AccessibleName = "";
            this.btnContact.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnContact.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnContact.Location = new System.Drawing.Point(409, 72);
            this.btnContact.Name = "btnContact";
            this.btnContact.Size = new System.Drawing.Size(24, 20);
            this.btnContact.TabIndex = 22;
            this.btnContact.Text = "...";
            this.btnContact.Click += new System.EventHandler(this.btnContact_Click);
            // 
            // txtPayTerms
            // 
            this.txtPayTerms.AccessibleDescription = "";
            this.txtPayTerms.AccessibleName = "";
            this.txtPayTerms.Location = new System.Drawing.Point(283, 136);
            this.txtPayTerms.Name = "txtPayTerms";
            this.txtPayTerms.Size = new System.Drawing.Size(103, 20);
            this.txtPayTerms.TabIndex = 45;
            this.txtPayTerms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayTerms_KeyDown);
            this.txtPayTerms.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtPayTerms.Validating += new System.ComponentModel.CancelEventHandler(this.txtPayTerms_Validating);
            // 
            // txtDeliveryTerms
            // 
            this.txtDeliveryTerms.AccessibleDescription = "";
            this.txtDeliveryTerms.AccessibleName = "";
            this.txtDeliveryTerms.Location = new System.Drawing.Point(510, 182);
            this.txtDeliveryTerms.Name = "txtDeliveryTerms";
            this.txtDeliveryTerms.Size = new System.Drawing.Size(90, 20);
            this.txtDeliveryTerms.TabIndex = 61;
            this.txtDeliveryTerms.Visible = false;
            this.txtDeliveryTerms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeliveryTerms_KeyDown);
            this.txtDeliveryTerms.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtDeliveryTerms.Validating += new System.ComponentModel.CancelEventHandler(this.txtDeliveryTerms_Validating);
            // 
            // txtDiscTerms
            // 
            this.txtDiscTerms.AccessibleDescription = "";
            this.txtDiscTerms.AccessibleName = "";
            this.txtDiscTerms.Location = new System.Drawing.Point(510, 160);
            this.txtDiscTerms.Name = "txtDiscTerms";
            this.txtDiscTerms.Size = new System.Drawing.Size(90, 20);
            this.txtDiscTerms.TabIndex = 58;
            this.txtDiscTerms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscTerms_KeyDown);
            this.txtDiscTerms.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtDiscTerms.Validating += new System.ComponentModel.CancelEventHandler(this.txtDiscTerms_Validating);
            // 
            // txtSalesType
            // 
            this.txtSalesType.AccessibleDescription = "";
            this.txtSalesType.AccessibleName = "";
            this.txtSalesType.Location = new System.Drawing.Point(283, 114);
            this.txtSalesType.Name = "txtSalesType";
            this.txtSalesType.Size = new System.Drawing.Size(103, 20);
            this.txtSalesType.TabIndex = 42;
            this.txtSalesType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesType_KeyDown);
            this.txtSalesType.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtSalesType.Validating += new System.ComponentModel.CancelEventHandler(this.txtSalesType_Validating);
            // 
            // btnPayTerms
            // 
            this.btnPayTerms.AccessibleDescription = "";
            this.btnPayTerms.AccessibleName = "";
            this.btnPayTerms.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPayTerms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPayTerms.Location = new System.Drawing.Point(387, 136);
            this.btnPayTerms.Name = "btnPayTerms";
            this.btnPayTerms.Size = new System.Drawing.Size(24, 20);
            this.btnPayTerms.TabIndex = 46;
            this.btnPayTerms.Text = "...";
            this.btnPayTerms.Click += new System.EventHandler(this.btnPayTerms_Click);
            // 
            // btnDeliveryTerms
            // 
            this.btnDeliveryTerms.AccessibleDescription = "";
            this.btnDeliveryTerms.AccessibleName = "";
            this.btnDeliveryTerms.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDeliveryTerms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDeliveryTerms.Location = new System.Drawing.Point(601, 182);
            this.btnDeliveryTerms.Name = "btnDeliveryTerms";
            this.btnDeliveryTerms.Size = new System.Drawing.Size(24, 20);
            this.btnDeliveryTerms.TabIndex = 62;
            this.btnDeliveryTerms.Text = "...";
            this.btnDeliveryTerms.Visible = false;
            this.btnDeliveryTerms.Click += new System.EventHandler(this.btnDeliveryTerms_Click);
            // 
            // btnDiscTerms
            // 
            this.btnDiscTerms.AccessibleDescription = "";
            this.btnDiscTerms.AccessibleName = "";
            this.btnDiscTerms.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDiscTerms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDiscTerms.Location = new System.Drawing.Point(601, 160);
            this.btnDiscTerms.Name = "btnDiscTerms";
            this.btnDiscTerms.Size = new System.Drawing.Size(24, 20);
            this.btnDiscTerms.TabIndex = 59;
            this.btnDiscTerms.Text = "...";
            this.btnDiscTerms.Click += new System.EventHandler(this.btnDiscTerms_Click);
            // 
            // btnSalesType
            // 
            this.btnSalesType.AccessibleDescription = "";
            this.btnSalesType.AccessibleName = "";
            this.btnSalesType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSalesType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSalesType.Location = new System.Drawing.Point(387, 114);
            this.btnSalesType.Name = "btnSalesType";
            this.btnSalesType.Size = new System.Drawing.Size(24, 20);
            this.btnSalesType.TabIndex = 43;
            this.btnSalesType.Text = "...";
            this.btnSalesType.Click += new System.EventHandler(this.btnSalesType_Click);
            // 
            // txtPause
            // 
            this.txtPause.AccessibleDescription = "";
            this.txtPause.AccessibleName = "";
            this.txtPause.Location = new System.Drawing.Point(510, 138);
            this.txtPause.Name = "txtPause";
            this.txtPause.Size = new System.Drawing.Size(90, 20);
            this.txtPause.TabIndex = 55;
            this.txtPause.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPause_KeyDown);
            this.txtPause.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtPause.Validating += new System.ComponentModel.CancelEventHandler(this.txtPause_Validating);
            // 
            // btnPause
            // 
            this.btnPause.AccessibleDescription = "";
            this.btnPause.AccessibleName = "";
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPause.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPause.Location = new System.Drawing.Point(601, 138);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(24, 20);
            this.btnPause.TabIndex = 56;
            this.btnPause.Text = "...";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // txtShipToLoc
            // 
            this.txtShipToLoc.AccessibleDescription = "";
            this.txtShipToLoc.AccessibleName = "";
            this.txtShipToLoc.Location = new System.Drawing.Point(80, 114);
            this.txtShipToLoc.Name = "txtShipToLoc";
            this.txtShipToLoc.Size = new System.Drawing.Size(78, 20);
            this.txtShipToLoc.TabIndex = 30;
            this.txtShipToLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShipToLoc_KeyDown);
            this.txtShipToLoc.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtShipToLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtShipToLoc_Validating);
            // 
            // btnShipToLoc
            // 
            this.btnShipToLoc.AccessibleDescription = "";
            this.btnShipToLoc.AccessibleName = "";
            this.btnShipToLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShipToLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnShipToLoc.Location = new System.Drawing.Point(159, 114);
            this.btnShipToLoc.Name = "btnShipToLoc";
            this.btnShipToLoc.Size = new System.Drawing.Size(24, 20);
            this.btnShipToLoc.TabIndex = 31;
            this.btnShipToLoc.Text = "...";
            this.btnShipToLoc.Click += new System.EventHandler(this.btnShipToLoc_Click);
            // 
            // txtBillToLoc
            // 
            this.txtBillToLoc.AccessibleDescription = "";
            this.txtBillToLoc.AccessibleName = "";
            this.txtBillToLoc.Location = new System.Drawing.Point(80, 136);
            this.txtBillToLoc.Name = "txtBillToLoc";
            this.txtBillToLoc.Size = new System.Drawing.Size(78, 20);
            this.txtBillToLoc.TabIndex = 33;
            this.txtBillToLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillToLoc_KeyDown);
            this.txtBillToLoc.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtBillToLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtBillToLoc_Validating);
            // 
            // btnBillToLoc
            // 
            this.btnBillToLoc.AccessibleDescription = "";
            this.btnBillToLoc.AccessibleName = "";
            this.btnBillToLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBillToLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBillToLoc.Location = new System.Drawing.Point(159, 136);
            this.btnBillToLoc.Name = "btnBillToLoc";
            this.btnBillToLoc.Size = new System.Drawing.Size(24, 20);
            this.btnBillToLoc.TabIndex = 34;
            this.btnBillToLoc.Text = "...";
            this.btnBillToLoc.Click += new System.EventHandler(this.btnBillToLoc_Click);
            // 
            // txtShipFromLoc
            // 
            this.txtShipFromLoc.AccessibleDescription = "";
            this.txtShipFromLoc.AccessibleName = "";
            this.txtShipFromLoc.Location = new System.Drawing.Point(80, 158);
            this.txtShipFromLoc.Name = "txtShipFromLoc";
            this.txtShipFromLoc.Size = new System.Drawing.Size(78, 20);
            this.txtShipFromLoc.TabIndex = 36;
            this.txtShipFromLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShipFromLoc_KeyDown);
            this.txtShipFromLoc.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtShipFromLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtShipFromLoc_Validating);
            // 
            // btnShipFromLoc
            // 
            this.btnShipFromLoc.AccessibleDescription = "";
            this.btnShipFromLoc.AccessibleName = "";
            this.btnShipFromLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShipFromLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnShipFromLoc.Location = new System.Drawing.Point(159, 158);
            this.btnShipFromLoc.Name = "btnShipFromLoc";
            this.btnShipFromLoc.Size = new System.Drawing.Size(24, 20);
            this.btnShipFromLoc.TabIndex = 37;
            this.btnShipFromLoc.Text = "...";
            this.btnShipFromLoc.Click += new System.EventHandler(this.btnShipFromLoc_Click);
            // 
            // txtSalesRepres
            // 
            this.txtSalesRepres.AccessibleDescription = "";
            this.txtSalesRepres.AccessibleName = "";
            this.txtSalesRepres.Location = new System.Drawing.Point(80, 180);
            this.txtSalesRepres.Name = "txtSalesRepres";
            this.txtSalesRepres.Size = new System.Drawing.Size(78, 20);
            this.txtSalesRepres.TabIndex = 39;
            this.txtSalesRepres.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesRepres_KeyDown);
            this.txtSalesRepres.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtSalesRepres.Validating += new System.ComponentModel.CancelEventHandler(this.txtSalesRepres_Validating);
            // 
            // btnSalesRepres
            // 
            this.btnSalesRepres.AccessibleDescription = "";
            this.btnSalesRepres.AccessibleName = "";
            this.btnSalesRepres.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSalesRepres.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSalesRepres.Location = new System.Drawing.Point(159, 180);
            this.btnSalesRepres.Name = "btnSalesRepres";
            this.btnSalesRepres.Size = new System.Drawing.Size(24, 20);
            this.btnSalesRepres.TabIndex = 40;
            this.btnSalesRepres.Text = "...";
            this.btnSalesRepres.Click += new System.EventHandler(this.btnSalesRepres_Click);
            // 
            // txtCarrier
            // 
            this.txtCarrier.AccessibleDescription = "";
            this.txtCarrier.AccessibleName = "";
            this.txtCarrier.Location = new System.Drawing.Point(283, 158);
            this.txtCarrier.Name = "txtCarrier";
            this.txtCarrier.Size = new System.Drawing.Size(103, 20);
            this.txtCarrier.TabIndex = 48;
            this.txtCarrier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarrier_KeyDown);
            this.txtCarrier.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtCarrier.Validating += new System.ComponentModel.CancelEventHandler(this.txtCarrier_Validating);
            // 
            // btnCarrier
            // 
            this.btnCarrier.AccessibleDescription = "";
            this.btnCarrier.AccessibleName = "";
            this.btnCarrier.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCarrier.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCarrier.Location = new System.Drawing.Point(387, 158);
            this.btnCarrier.Name = "btnCarrier";
            this.btnCarrier.Size = new System.Drawing.Size(24, 20);
            this.btnCarrier.TabIndex = 49;
            this.btnCarrier.Text = "...";
            this.btnCarrier.Click += new System.EventHandler(this.btnCarrier_Click);
            // 
            // txtBuyLoc
            // 
            this.txtBuyLoc.AccessibleDescription = "";
            this.txtBuyLoc.AccessibleName = "";
            this.txtBuyLoc.Location = new System.Drawing.Point(80, 72);
            this.txtBuyLoc.MaxLength = 20;
            this.txtBuyLoc.Name = "txtBuyLoc";
            this.txtBuyLoc.Size = new System.Drawing.Size(96, 20);
            this.txtBuyLoc.TabIndex = 18;
            this.txtBuyLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuyLoc_KeyDown);
            this.txtBuyLoc.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtBuyLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtBuyLoc_Validating);
            // 
            // btnBuyLoc
            // 
            this.btnBuyLoc.AccessibleDescription = "";
            this.btnBuyLoc.AccessibleName = "";
            this.btnBuyLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBuyLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBuyLoc.Location = new System.Drawing.Point(177, 72);
            this.btnBuyLoc.Name = "btnBuyLoc";
            this.btnBuyLoc.Size = new System.Drawing.Size(24, 20);
            this.btnBuyLoc.TabIndex = 19;
            this.btnBuyLoc.Text = "...";
            this.btnBuyLoc.Click += new System.EventHandler(this.btnBuyLoc_Click);
            // 
            // rptSOReport
            // 
            this.rptSOReport.ReportDefinition = resources.GetString("rptSOReport.ReportDefinition");
            this.rptSOReport.ReportName = "SaleOrderReport";
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(946, 391);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(34, 20);
            this.btnImport.TabIndex = 80;
            this.btnImport.Text = "Imp.";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // ctxmnuImport
            // 
            this.ctxmnuImport.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuImportNew,
            this.mnuImportUpdate});
            // 
            // mnuImportNew
            // 
            this.mnuImportNew.Index = 0;
            this.mnuImportNew.Text = "Import for new Sales Order";
            this.mnuImportNew.Click += new System.EventHandler(this.mnuImportNew_Click);
            // 
            // mnuImportUpdate
            // 
            this.mnuImportUpdate.Index = 1;
            this.mnuImportUpdate.Text = "Import update Sales Order";
            this.mnuImportUpdate.Click += new System.EventHandler(this.mnuImportUpdate_Click);
            // 
            // ocmdExcelSelect
            // 
            this.ocmdExcelSelect.Connection = this.oconExcelFile;
            // 
            // odadExcelFile
            // 
            this.odadExcelFile.MissingSchemaAction = System.Data.MissingSchemaAction.Ignore;
            this.odadExcelFile.SelectCommand = this.ocmdExcelSelect;
            // 
            // dlgOpenImpFile
            // 
            this.dlgOpenImpFile.DefaultExt = "xls";
            this.dlgOpenImpFile.Filter = "Excel File|*.xls";
            this.dlgOpenImpFile.Title = "Select File To Import";
            this.dlgOpenImpFile.FileOk += new System.ComponentModel.CancelEventHandler(this.dlgOpenImpFile_FileOk);
            // 
            // btnAutoCancelSO
            // 
            this.btnAutoCancelSO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoCancelSO.Location = new System.Drawing.Point(688, 436);
            this.btnAutoCancelSO.Name = "btnAutoCancelSO";
            this.btnAutoCancelSO.Size = new System.Drawing.Size(106, 23);
            this.btnAutoCancelSO.TabIndex = 86;
            this.btnAutoCancelSO.Text = "A&uto Cancel";
            this.btnAutoCancelSO.Click += new System.EventHandler(this.btnAutoCancelSO_Click);
            // 
            // txtGateType
            // 
            this.txtGateType.AccessibleDescription = "";
            this.txtGateType.AccessibleName = "";
            this.txtGateType.Location = new System.Drawing.Point(510, 182);
            this.txtGateType.Name = "txtGateType";
            this.txtGateType.Size = new System.Drawing.Size(90, 20);
            this.txtGateType.TabIndex = 64;
            this.txtGateType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGateType_KeyDown);
            this.txtGateType.Validating += new System.ComponentModel.CancelEventHandler(this.txtGateType_Validating);
            // 
            // btnGateType
            // 
            this.btnGateType.AccessibleDescription = "";
            this.btnGateType.AccessibleName = "";
            this.btnGateType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGateType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGateType.Location = new System.Drawing.Point(601, 182);
            this.btnGateType.Name = "btnGateType";
            this.btnGateType.Size = new System.Drawing.Size(24, 20);
            this.btnGateType.TabIndex = 65;
            this.btnGateType.Text = "...";
            this.btnGateType.Click += new System.EventHandler(this.btnGateType_Click);
            // 
            // lblGate
            // 
            this.lblGate.Location = new System.Drawing.Point(458, 34);
            this.lblGate.Name = "lblGate";
            this.lblGate.Size = new System.Drawing.Size(64, 23);
            this.lblGate.TabIndex = 81;
            this.lblGate.Text = "Gate";
            this.lblGate.Visible = false;
            // 
            // lblGateType
            // 
            this.lblGateType.AccessibleDescription = "";
            this.lblGateType.AccessibleName = "";
            this.lblGateType.ForeColor = System.Drawing.Color.Maroon;
            this.lblGateType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblGateType.Location = new System.Drawing.Point(432, 182);
            this.lblGateType.Name = "lblGateType";
            this.lblGateType.Size = new System.Drawing.Size(80, 19);
            this.lblGateType.TabIndex = 63;
            this.lblGateType.Text = "Type";
            this.lblGateType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnUpdateUnitPrice
            // 
            this.btnUpdateUnitPrice.AccessibleDescription = "";
            this.btnUpdateUnitPrice.AccessibleName = "";
            this.btnUpdateUnitPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateUnitPrice.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUpdateUnitPrice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnUpdateUnitPrice.Location = new System.Drawing.Point(551, 390);
            this.btnUpdateUnitPrice.Name = "btnUpdateUnitPrice";
            this.btnUpdateUnitPrice.Size = new System.Drawing.Size(92, 23);
            this.btnUpdateUnitPrice.TabIndex = 90;
            this.btnUpdateUnitPrice.Text = "Update UnitPrice";
            this.btnUpdateUnitPrice.Click += new System.EventHandler(this.btnUpdateUnitPrice_Click);
            // 
            // SaleOrder
            // 
            this.AccessibleDescription = "";
            this.AccessibleName = "";
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(988, 462);
            this.Controls.Add(this.gridData);
            this.Controls.Add(this.btnUpdateUnitPrice);
            this.Controls.Add(this.txtGateType);
            this.Controls.Add(this.txtBuyLoc);
            this.Controls.Add(this.txtCarrier);
            this.Controls.Add(this.txtSalesRepres);
            this.Controls.Add(this.txtShipFromLoc);
            this.Controls.Add(this.txtBillToLoc);
            this.Controls.Add(this.txtShipToLoc);
            this.Controls.Add(this.txtPause);
            this.Controls.Add(this.txtPayTerms);
            this.Controls.Add(this.txtDeliveryTerms);
            this.Controls.Add(this.txtDiscTerms);
            this.Controls.Add(this.txtSalesType);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.txtCurrency);
            this.Controls.Add(this.txtOrderNo);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.txtCustomerPO);
            this.Controls.Add(this.lblGateType);
            this.Controls.Add(this.btnGateType);
            this.Controls.Add(this.lblGate);
            this.Controls.Add(this.btnAutoCancelSO);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnBuyLoc);
            this.Controls.Add(this.btnCarrier);
            this.Controls.Add(this.btnSalesRepres);
            this.Controls.Add(this.btnShipFromLoc);
            this.Controls.Add(this.btnBillToLoc);
            this.Controls.Add(this.btnShipToLoc);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPayTerms);
            this.Controls.Add(this.btnDeliveryTerms);
            this.Controls.Add(this.btnDiscTerms);
            this.Controls.Add(this.btnSalesType);
            this.Controls.Add(this.btnContact);
            this.Controls.Add(this.btnCurrency);
            this.Controls.Add(this.txtTotalNetAmount);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.txtTotalExpTax);
            this.Controls.Add(this.txtTotalDiscount);
            this.Controls.Add(this.txtTotalSpecTax);
            this.Controls.Add(this.txtTotalVAT);
            this.Controls.Add(this.txtExchRate);
            this.Controls.Add(this.cboTransDate);
            this.Controls.Add(this.cboPriority);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.btnAdvance);
            this.Controls.Add(this.lblSOHeader);
            this.Controls.Add(this.lblTotalExpTax);
            this.Controls.Add(this.lblTotalDiscount);
            this.Controls.Add(this.lblTotalSpecTax);
            this.Controls.Add(this.lblTotalVAT);
            this.Controls.Add(this.btnDeliverySchedule);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnCustomerName);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.btnOrderNo);
            this.Controls.Add(this.btnCustomer);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.lblExchRate);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.lblOrderNo);
            this.Controls.Add(this.lblBuyLoc);
            this.Controls.Add(this.lblTransDate);
            this.Controls.Add(this.chkVAT);
            this.Controls.Add(this.chkExportTax);
            this.Controls.Add(this.chkSpecialTax);
            this.Controls.Add(this.lblCustPO);
            this.Controls.Add(this.lblPause);
            this.Controls.Add(this.chkShipCompleted);
            this.Controls.Add(this.lblCarrier);
            this.Controls.Add(this.lblDiscTerms);
            this.Controls.Add(this.lblPayTerms);
            this.Controls.Add(this.lblSalesType);
            this.Controls.Add(this.lblDeliveryTerms);
            this.Controls.Add(this.lblBillToLoc);
            this.Controls.Add(this.lblShipFromLoc);
            this.Controls.Add(this.lblSalesRepres);
            this.Controls.Add(this.lblShipToLoc);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblTotalNetAmount);
            this.KeyPreview = true;
            this.Name = "SaleOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Order Maintenance";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SaleOrder_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SaleOrder_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SaleOrder_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalNetAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalExpTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalSpecTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalVAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptSOReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Load,Add,Save,Edit,Delete,Close
		private void SaleOrder_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".SaleOrder_Load()";
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

				// load form
				InitVariable();
				// Store grid layout
				dtbGridDesign = FormControlComponents.StoreGridLayout(gridData);
				// KeepTheGridDesign();
				//Set type of Party
				
				// Load initial value for controls
				mFormAction = EnumAction.Default;
				SetEnableButtons();
				SetEditableControl(false);
				txtOrderNo.Enabled = true;
				//txtExchRate.ReadOnly = true;

				if(mSaleOrderMasterID > 0)
				{
					//SaleOrderBO boSO = new SaleOrderBO();
					voSaleOrderMaster = (SO_SaleOrderMasterVO)boSaleOrder.GetMasterVO(mSaleOrderMasterID);
					//VOToControls(voSaleOrderMaster);
					LoadMaster(mSaleOrderMasterID);
					SetEnableButtons();
					txtOrderNo.Enabled = false;
					btnClose.Focus();
				}
				else
					txtExchRate.Value = null;
				strParam[0] = lblCustomer.Text;
				//Keep query string
				strReportQuery = rptSOReport.DataSource.RecordSource;				
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

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				foreach(Control objControl in this.Controls)
				{
					if(objControl.GetType().Equals(typeof(TextBox)))
					{
						((TextBox)objControl).Text = string.Empty;
						objControl.Tag = null;
					}
					if(objControl.GetType().Equals(typeof(ComboBox)))
					{
						((ComboBox) objControl).SelectedIndex = -1;
					}
				}
				// add master record
				mFormAction = EnumAction.Add;
				SetEnableButtons();
				SetEditableControl(true);
				SO_SaleOrderMasterVO voMasterBlank = new SO_SaleOrderMasterVO();
				UtilsBO boUtil = new UtilsBO();
				cboTransDate.Value = voMasterBlank.TransDate = boUtil.GetDBDate();
				voMasterBlank.CCNID = SystemProperty.CCNID;
				voMasterBlank.CurrencyID = SystemProperty.DefaultCurrencyID;

				txtExchRate.Value = null;
				voMasterBlank.Code = FormControlComponents.GetNoByMask(this);
				voSaleOrderMaster = voMasterBlank;
				VOToControls(voMasterBlank);
				gridData.AllowAddNew = true;
				cboTransDate.Focus();
				txtExchRate.Enabled = false;
				txtOrderNo.Enabled = true;
				//Fill MasterLocation Default
				FormControlComponents.SetDefaultMasterLocation(txtShipFromLoc);
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

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			blnHasError = true;
			if(Security.IsDifferencePrefix(this,lblOrderNo,txtOrderNo))
			{
				return;
			}
			if(CheckMandatory(cboCCN))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				cboCCN.Focus();
				return;
			}
			if(CheckMandatory(cboTransDate))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				cboTransDate.Focus();
				return;
			}

			#region no need to check period

//			if(!FormControlComponents.CheckDateInCurrentPeriod((DateTime)cboTransDate.Value))
//			{
//				//PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE,MessageBoxIcon.Exclamation);
//				PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD,MessageBoxIcon.Exclamation);
//				
//				cboTransDate.Focus();
//				return;
//			}

			#endregion

			if(CheckMandatory(txtCurrency))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtCurrency.Focus();
				return;
			}			
			if(CheckMandatory(txtOrderNo))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtOrderNo.Focus();
				return;
			}
			if(CheckMandatory(txtExchRate))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtExchRate.Focus();
				return;
			}
			if (Decimal.Parse(txtExchRate.Value.ToString()) <= 0)
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtExchRate.Focus();
				return;
			}
			if(CheckMandatory(txtCustomer))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtCustomer.Focus();
				return;
			}
			if(IsNotExistCustomer())
			{
				// This customer does not exist in database
				PCSMessageBox.Show(ErrorCode.MESSAGE_CUSTOMER_DOES_NOT_EXIST,MessageBoxIcon.Exclamation);
				btnCustomer.Focus();
				return;
			}
			if(CheckMandatory(txtBuyLoc))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtBuyLoc.Focus();
				return;
			}
			if(CheckMandatory(txtShipToLoc))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtShipToLoc.Focus();
				return;
			}
			if(CheckMandatory(txtBillToLoc))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtBillToLoc.Focus();
				return;
			}
			if(CheckMandatory(txtShipFromLoc))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtShipFromLoc.Focus();
				return;
			}
			if(CheckMandatory(txtGateType))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtGateType.Focus();
				return;
			}

			// Checks sale order record
			if(gridData.RowCount <= 0)
			{
				// You have to input at least a record in grid sale order detail
				PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_AT_LEAST_RECORD_IN_GRID,MessageBoxIcon.Warning);
				gridData.Focus();
				return;
			}
			//Check mandatory in grid
			int intMaxRow = gridData.RowCount;
			//Check mandatory in grid.
			for (int i = 0; i < intMaxRow; i++)
			{
				if((gridData[i, SO_SaleOrderDetailTable.PRODUCTID_FLD]) == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ITEM_FIELD,MessageBoxIcon.Exclamation);
					gridData.Row = i;
					gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.PRODUCTID_FLD]);
					gridData.Focus();
					return;
				}
				if((gridData[i, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD]) == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ORDER_QUANTITY_FIELD,MessageBoxIcon.Exclamation);
					gridData.Row = i;
					gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD]);
					gridData.Focus();
					return;
				}
				if((gridData[i, SO_SaleOrderDetailTable.SELLINGUMID_FLD]) == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_OF_MEASURE_FIELD,MessageBoxIcon.Exclamation);
					gridData.Row = i;
					gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.SELLINGUMID_FLD]);
					gridData.Focus();
					return;
				}
				if(decimal.Parse(gridData[i, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString()) <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ORDER_QUANTITY_FIELD,MessageBoxIcon.Exclamation);
					gridData.Row = i;
					gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD]);
					gridData.Focus();
					return;
				}
				if((gridData[i, SO_SaleOrderDetailTable.UNITPRICE_FLD]) == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_PRICE_FIELD,MessageBoxIcon.Exclamation);
					gridData.Row = i;
					gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.UNITPRICE_FLD]);
					gridData.Focus();
					return;
				}
				if(decimal.Parse(gridData[i, SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString()) < 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_PRICE_FIELD,MessageBoxIcon.Exclamation);
					gridData.Row = i;
					gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.UNITPRICE_FLD]);
					gridData.Focus();
					return;
				}

				#region HACK: Trada 28-04-2006: 
				//Check Total Amount and Discount Amount

				if((gridData[i, SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] != DBNull.Value)
					&&(gridData[i, SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
					&&(gridData[i, SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != null)
					&&(gridData[i, SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] != null))
				{
					if (decimal.Parse(gridData[i, SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString()) > decimal.Parse(gridData[i, SO_SaleOrderDetailTable.TOTALAMOUNT_FLD].ToString()))
					{
						string[] strParam = new string[2];
						strParam[0] = "Discount Amount";
						strParam[1] = "Total Amount";
						PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
						gridData.Row = i;
						gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD]);
						gridData.Focus();
						return;
					}
				}
				//Check Order Quantity must be greater than Total Del Quantity
				//HACKED : DuongNA - fix error when form in add new mode
				if ((dstSODetailDeliverySchedule.Tables.Count > 0)
					&& (gridData[i, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] != DBNull.Value)
					&& (gridData[i, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] != null))
				{
					if ((gridData[i, SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD] != DBNull.Value)
					&& (gridData[i, SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD] != null))
					{
						DataRow[] adrowSODeliverySchedule = dstSODetailDeliverySchedule.Tables[0].Select(SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + " = " + gridData[i, SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString());
						if (adrowSODeliverySchedule.Length > 0)
						{
							if (decimal.Parse(gridData[i, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString()) < decimal.Parse(adrowSODeliverySchedule[0][TOTAL_DELIVERY_QTY].ToString()))
							{
								string[] strParam = new string[2];
								strParam[0] = "Order Quantity";
								strParam[1] = "Total Delivery";//TOTAL_DELIVERY_QTY;
								PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD]);
								gridData.Focus();
								return;
							}
						}
					}
				}
				#endregion END: Trada 28-04-2006
			}

			try
			{
				// save to database
				ReCalculate();
				FormControlComponents.SynchronyGridData(gridData);
				StoreDatabase();
				blnHasCommited = false;
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

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				if (voSaleOrderMaster.SaleOrderMasterID > 0)	
				{
					#region Check Right To Edit Transaction
					if(Security.NoRightToEditTransaction(this, SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD, voSaleOrderMaster.SaleOrderMasterID))
					{
						return;
					}
					//Check if SO has been committed and return
					if (boSaleOrder.CheckSOReleased(voSaleOrderMaster.SaleOrderMasterID))
					{
						blnHasCommited = true;
						gridData.AllowAddNew = false;
					}
					else
					{
						blnHasCommited = false;
						gridData.AllowAddNew = true;
					}
						
					#endregion Check Right To Edit Transaction

                    mFormAction = EnumAction.Edit;
                    // HACK: Trada 27-10-2005
                    InitGrid(voSaleOrderMaster.SaleOrderMasterID);
                    // END: Trada 27-10-2005

                    #region HACK: Trada 28-04-2006: Check editable items
                    //Get All SODetail has been set up delivery schedule

                    dstSODetailDeliverySchedule = boSaleOrder.GetSODetailDeliverySchedule(voSaleOrderMaster.SaleOrderMasterID);
                    #endregion END: Trada 28-04-2006

                    SetEnableButtons();
                    SetEditableControl(true);
                    cboCCN.Enabled = false;
                    txtOrderNo.Enabled = true;
                    if (blnHasCommited)
                    {
                        cboTransDate.Enabled = false;
                        cboCCN.Enabled = false;
                        txtCurrency.Enabled = false;
                        txtExchRate.Enabled = false;
                        txtOrderNo.Enabled = false;
                        txtCustomerName.Enabled = false;
                        txtCustomer.Enabled = false;
                        txtBuyLoc.Enabled = false;
                        txtShipFromLoc.Enabled = false;
                        txtShipToLoc.Enabled = false;
                        txtBillToLoc.Enabled = false;
                        btnCurrency.Enabled = false;
                        btnOrderNo.Enabled = false;
                        btnCustomer.Enabled = false;
                        btnCustomerName.Enabled = false;
                        btnBuyLoc.Enabled = false;
                        btnShipFromLoc.Enabled = false;
                        btnShipToLoc.Enabled = false;
                        btnBillToLoc.Enabled = false;
                        btnGateType.Enabled = txtGateType.Enabled = false;
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

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				// if exist product then excute deleted, else do nothing
				if (voSaleOrderMaster.SaleOrderMasterID > 0)	
				{
					if(Security.NoRightToDeleteTransaction(this,SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD, voSaleOrderMaster.SaleOrderMasterID))
					{
						return;
					}

					DialogResult enumResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
					if(enumResult == DialogResult.Yes)
					{
						boSaleOrder.DeleteSaleOrder(voSaleOrderMaster.SaleOrderMasterID);
						voSaleOrderMaster = new SO_SaleOrderMasterVO();
						// Set enable button and clear info on form
						blnIsChangedGrid = false;
						SO_SaleOrderMasterVO voSOMaster = new SO_SaleOrderMasterVO();
						VOToControls(voSOMaster);
						ClearForm();
						mFormAction = EnumAction.Default;
						SetEnableButtons();
						btnAdd.Focus();
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


		#endregion Load,Add,Save,Edit,Delete,Close

		#region other events
		private void btnDeliverySchedule_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDeliverySchedule_Click()";
			try 
			{
				SaleOrderInformationVO objSaleOrderInformationVO = new SaleOrderInformationVO();
				if(dstSaleOrderDetail.Tables.Count > 0)
					if(dstSaleOrderDetail.Tables[0].Rows.Count > 0)
					{
						objSaleOrderInformationVO.CCNCode = cboCCN.Text.Trim();
						objSaleOrderInformationVO.OrderDate = (DateTime)cboTransDate.Value;
						objSaleOrderInformationVO.OrderQuantity = decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString());
						objSaleOrderInformationVO.ProductID = int.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.PRODUCTID_FLD].ToString());
						//SaleOrderBO boSale = new SaleOrderBO();
						ITM_ProductVO voProduct =  (ITM_ProductVO)boSaleOrder.GetProduct(int.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.PRODUCTID_FLD].ToString()));
						objSaleOrderInformationVO.ProductCode = voProduct.Code;
						objSaleOrderInformationVO.ProductDescription = gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD].ToString();
						objSaleOrderInformationVO.ProductRevision = gridData[gridData.Row,ITM_ProductTable.REVISION_FLD].ToString();
						objSaleOrderInformationVO.SaleOrderDetailID = int.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString());
						objSaleOrderInformationVO.SaleOrderLine = int.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString());
						objSaleOrderInformationVO.SaleOrderMasterID = voSaleOrderMaster.SaleOrderMasterID;
						objSaleOrderInformationVO.SaleOrderNo = voSaleOrderMaster.Code;
						
						DataSet dstUnitOfMeasure = boSaleOrder.ListUnitOfMeasure();
						// set primary key
						if(dstUnitOfMeasure.Tables.Count > 0)
						{
							DataColumn[] objColumns = new DataColumn[1];
							objColumns[0] = dstUnitOfMeasure.Tables[0].Columns[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD];
							dstUnitOfMeasure.Tables[0].PrimaryKey = objColumns;
						}
						objSaleOrderInformationVO.UnitCode = dstUnitOfMeasure.Tables[0].Rows.Find(gridData[gridData.Row,SO_SaleOrderDetailTable.SELLINGUMID_FLD].ToString())[MST_UnitOfMeasureTable.CODE_FLD].ToString();
						SODeliverySchedule frmSODeliverySchedule = new SODeliverySchedule(objSaleOrderInformationVO);
						try
						{
							frmSODeliverySchedule.ShowDialog();
						}
						catch
						{
							// Don't show system error
						}
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
		
		private void btnAdvance_Click(object sender, System.EventArgs e)
		{
			// open advance form
		}

		private void btnOrderNo_Click(object sender, System.EventArgs e)
		{
			// find order
			const string METHOD_NAME = THIS + ".btnOrderNo_Click()";
			try
			{
				// HACK: Trada 28-10-2005
				if (mFormAction == EnumAction.Edit) return;	
				// END: Trada 28-10-2005
					drwResult = FormControlComponents.OpenSearchForm(SO_SaleOrderMasterTable.TABLE_NAME, SO_SaleOrderMasterTable.CODE_FLD, txtOrderNo.Text.Trim(), null, true);
				if (drwResult != null)
				{	
					voSaleOrderMaster.SaleOrderMasterID = int.Parse(drwResult[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString());
					//SaleOrderBO boSO = new SaleOrderBO();
					LoadMaster(voSaleOrderMaster.SaleOrderMasterID);
					voSaleOrderMaster = (SO_SaleOrderMasterVO)boSaleOrder.GetMasterVO(voSaleOrderMaster.SaleOrderMasterID);
					//VOToControls(voSaleOrderMaster);
					btnEdit.Enabled = btnDelete.Enabled = btnPrint.Enabled = btnDeliverySchedule.Enabled = true;
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

		private void txtOrderNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnOrderNo.Enabled)
					btnOrderNo_Click(sender,e);
		}
		private void btnCustomer_Click(object sender, System.EventArgs e)
		{
			// find customer base on code
			const string METHOD_NAME = THIS + ".btnCustomer_Click()";
			try
			{
				htbCriteria = new Hashtable();
				htbCriteria.Add(CUSTOMER, (int)PartyTypeEnum.CUSTOMER);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtCustomer.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					// HACK: Trada 28-10-2005
					if ((txtCustomer.Tag != null) && (int.Parse(txtCustomer.Tag.ToString()) != int.Parse(drwResult[MST_PartyTable.PARTYID_FLD].ToString())))
					{
						txtCustomerName.Text = string.Empty;
						txtBuyLoc.Text = string.Empty;
						txtBuyLoc.Tag = null;
						txtContact.Text = string.Empty;
						txtContact.Tag = null;
						txtShipToLoc.Text = string.Empty;
						txtShipToLoc.Tag = null;
						txtBillToLoc.Text = string.Empty;
						txtBillToLoc.Tag = null;
					} // END: Trada 28-10-2005

					txtCustomer.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtCustomer.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					
					ResetCustomerItemAfterChangingOfCustomer();
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

		private void btnCustomerName_Click(object sender, System.EventArgs e)
		{
			// find customer base on name
			const string METHOD_NAME = THIS + ".btnCustomerName_Click()";
			try
			{
				/*
				ViewTable frmViewTable = new ViewTable(MST_PartyTable.TABLE_NAME);
				frmViewTable.ViewOnly = true;
				frmViewTable.GetData = true;
				frmViewTable.ReturnField = MST_PartyTable.PARTYID_FLD;
				frmViewTable.FilterField1 = MST_PartyTable.NAME_FLD;
				frmViewTable.FilterField2 = MST_PartyTable.CODE_FLD;
				frmViewTable.FilterFieldValue1 = txtCustomerName.Text.Trim();

				if (frmViewTable.ShowDialog() == DialogResult.OK)
				{
					txtCustomer.Tag = frmViewTable.ReturnField;
					txtCustomerName.Text = frmViewTable.FilterFieldValue1;
					txtCustomer.Text = frmViewTable.FilterFieldValue2;
				}
				frmViewTable.Close();
				*/
				htbCriteria = new Hashtable();
				htbCriteria.Add(CUSTOMER, 0);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtCustomerName.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtCustomer.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					txtCustomer.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
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

		private void SaleOrder_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Orcurs whenever users closed form
			const string METHOD_NAME = THIS + ".SaleOrder_Closing()";
			try
			{
				// if the form has been change then ask to store database
				if(blnIsChangedGrid || (mFormAction != EnumAction.Default) ) 
				{
					DialogResult enumDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
					if( enumDialog == DialogResult.Yes)
					{
						// store database
						btnSave_Click(sender, e);
						if (blnHasError)
						{
							e.Cancel = true;
						}
						//e.Cancel = false;
					} 
					else if( enumDialog == DialogResult.No) // click No button
					{
						e.Cancel = false;
					}
					else if( enumDialog == DialogResult.Cancel) // click Cancel button
					{
						e.Cancel = true;
					}
				}
				else // if has no change
				{
					e.Cancel = false;
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

		private void gridData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode) 
			{
				case Keys.F4:
					// Open search form
					if(sender != null)
					if(sender.GetType().Equals(typeof(C1TrueDBGrid)))
						gridData_ButtonClick(null,null);
					break;
				case Keys.Delete:
					if (gridData.SelectedRows.Count == 0) return;
					if (btnSave.Enabled)
					{
						if (CheckBeforeDeleteAllRows())
						{
							FormControlComponents.DeleteMultiRowsOnTrueDBGrid(gridData);
							int intCount  =0;
							foreach (DataRow objRow in dstSaleOrderDetail.Tables[0].Rows)
							{
								if(objRow.RowState != DataRowState.Deleted) 
									intCount++;
							}
							for (int i =0; i <intCount; i++)
								gridData[i, SO_DeliveryScheduleTable.LINE_FLD] = i+1;
							Calculate();
							ReCalculate();
							//return;
						}
						else
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_DELDELIVERY,MessageBoxIcon.Error);
						}
					}
					break;
			}		
		}
		/// <summary>
		/// CheckEditableItems
		/// </summary>
		/// <param name="pintSaleOrderDetailID"></param>
		/// <returns>true if allow, and false if not</returns>
		/// <Author>Trada</Author>
		/// <date>Friday, April 28 2006</date>
		private bool CheckEditableItems(int pintSaleOrderDetailID)
		{
			const string METHOD_NAME = THIS + ".CheckEditableItems()";
			try
			{
				DataRow[] adrowSODeliverySchedule = dstSODetailDeliverySchedule.Tables[0].Select(SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + " = " + pintSaleOrderDetailID.ToString());
				if (adrowSODeliverySchedule.Length > 0)
					return false;
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
		private void gridData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridData_ButtonClick()";
			try
			{
				if(gridData.AllowUpdate == false) return;
				DataRowView drowView = null;
				if ((gridData.Columns[gridData.Col] != gridData.Columns[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD])
					&& (gridData.Columns[gridData.Col] != gridData.Columns[SO_SaleOrderDetailTable.UNITPRICE_FLD]))
				{
					if (blnHasCommited)
					{
						return;
					}
				}
				// Open form to select Product
				if ((gridData.Columns[gridData.Col] == gridData.Columns[ITM_ProductTable.CODE_FLD])
					|| (gridData.Columns[gridData.Col] == gridData.Columns[ITM_ProductTable.DESCRIPTION_FLD])
					|| (gridData.Columns[gridData.Col] == gridData.Columns[ITM_ProductTable.REVISION_FLD]))
				{
					#region HACK: Trada 28-04-2006: Check editable items
					if ((mFormAction == EnumAction.Edit) && (gridData.Columns[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].Value != DBNull.Value))
					{
						gridData.EditActive = true;
                        if (!CheckEditableItems(Convert.ToInt32(gridData.Columns[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].Value)))
						{
							string[] strParam = new string[1];
							strParam[0] = "Sale Order Detail";
							PCSMessageBox.Show(ErrorCode.MESSAGE_SO_CAN_NOT_CHANGE_ITEM, MessageBoxIcon.Warning, strParam);
							return;
						}
					}
					

					#endregion END: Trada 28-04-2006

					//string strText = gridData[gridData.Row,gridData.Col].ToString();
					// HACK: Trada 10-04-2006
					string strText = string.Empty;
					if (gridData.AddNewMode == AddNewModeEnum.AddNewCurrent)
						strText = gridData[gridData.Row, gridData.Col].ToString();
					else
						strText = gridData.Columns[gridData.Col].Value.ToString().Trim();
					// END: Trada 10-04-2006

					//HACK: Trada 2005-10-13
					string strFieldName = string.Empty;
					string strTextFilter = string.Empty;
					if (gridData.Columns[gridData.Col] == gridData.Columns[ITM_ProductTable.CODE_FLD])
					{
						strFieldName = ITM_ProductTable.CODE_FLD;
						strTextFilter = gridData.Columns[ITM_ProductTable.CODE_FLD].Text.Trim(); 
					}
					if (gridData.Columns[gridData.Col] == gridData.Columns[ITM_ProductTable.DESCRIPTION_FLD])
					{
						strFieldName = ITM_ProductTable.DESCRIPTION_FLD;
						strTextFilter = gridData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Text.Trim(); 
					}
					if (gridData.Columns[gridData.Col] == gridData.Columns[ITM_ProductTable.REVISION_FLD])
					{
						strFieldName = ITM_ProductTable.REVISION_FLD;
						strTextFilter = gridData.Columns[ITM_ProductTable.REVISION_FLD].Text.Trim(); 
					}
					//END Trada 2005-10-13
					// HACK: Trada 10-18-2005

				    drowView = gridData.AddNewMode != AddNewModeEnum.AddNewPending
				                   ? FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, strFieldName, strText, null,
				                                                          true)
				                   : FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, strFieldName, strTextFilter,
				                                                          null, true);
				    // END: Trada 10-18-2005

					if(drowView != null)
					{
						DataRow[] objRows = dstSaleOrderDetail.Tables[0].Select(SO_SaleOrderDetailTable.PRODUCTID_FLD + " = " + drowView[ITM_ProductTable.PRODUCTID_FLD].ToString());
						if(objRows.Length == 0)
						{
							// init value for record
							gridData.EditActive = true;
							gridData[gridData.Row,SO_SaleOrderDetailTable.PRODUCTID_FLD] = drowView[ITM_ProductTable.PRODUCTID_FLD];
							ITM_ProductVO voProduct = (ITM_ProductVO)boSaleOrder.GetProductVO(int.Parse(drowView[ITM_ProductTable.PRODUCTID_FLD].ToString()));
							gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD] = voProduct.ProductID;
							gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = voProduct.Code;
							gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = voProduct.Description;
							gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = voProduct.Revision;
							gridData[gridData.Row,SO_SaleOrderDetailTable.SELLINGUMID_FLD] = voProduct.SellingUMID;
							gridData[gridData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = ((MST_UnitOfMeasureVO)boSaleOrder.GetUnitOfMeasure(voProduct.BuyingUMID)).Code;
							gridData[gridData.Row,SO_SaleOrderDetailTable.STOCKUMID_FLD] = voProduct.StockUMID;
							//Get category
							ProductItemInfoBO boProductItemInfo = new ProductItemInfoBO(); 
							gridData[gridData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = boProductItemInfo.GetCategoryCodeByProductID(int.Parse(drowView[ITM_ProductTable.PRODUCTID_FLD].ToString()));
							int intMaxLine = 0;
							if(gridData.Row > 0) intMaxLine = int.Parse(gridData[gridData.Row - 1,SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString());
							gridData[gridData.Row,SO_SaleOrderDetailTable.SALEORDERLINE_FLD] = ++intMaxLine;
							gridData[gridData.Row,SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] = 0;
							int intCustomerID = 0;
							try
							{
								intCustomerID = Convert.ToInt32(txtCustomer.Tag);
							}
							catch{}
							
							gridData[gridData.Row,SO_SaleOrderDetailTable.UNITPRICE_FLD] = boSaleOrder.GetUnitPriceDefualt(voProduct.ProductID, intCustomerID);
							// input vat,imp,spec
							if(chkVAT.Checked)
							{
								gridData[gridData.Row,SO_SaleOrderDetailTable.VATPERCENT_FLD] = voProduct.VAT;
								gridData[gridData.Row,SO_SaleOrderDetailTable.VATAMOUNT_FLD] = 0;
							}
							if(chkExportTax.Checked)
							{
								gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD] = voProduct.ExportTax;
								gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] = 0;
							}
							if(chkSpecialTax.Checked)
							{
								gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD] = voProduct.SpecialTax;
								gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] = 0;
							}

							//fill customer item ref
							if (txtCustomer.Text.Trim() != string.Empty)
							{
								SO_CustomerItemRefDetailVO voCusItemRef = (SO_CustomerItemRefDetailVO) boSaleOrder.GetItemCustomerRef(voProduct.ProductID, (int) txtCustomer.Tag);
								if (voCusItemRef != null)
								{
									gridData[gridData.Row,SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD] = voCusItemRef.CustomerItemCode;
									gridData[gridData.Row,SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD] = voCusItemRef.CustomerItemModel;
								}
							}
						}
					}
				}
				// input value Unit of measure
				if (gridData.Columns[gridData.Col] == gridData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD])
				{
					// HACK: Trada 10-18-2005
					if (gridData.AddNewMode != AddNewModeEnum.AddNewPending)
					{
						drowView = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME, MST_UnitOfMeasureTable.CODE_FLD, gridData[gridData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].ToString(), null, true);
					}
					else
					{
						drowView = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME, MST_UnitOfMeasureTable.CODE_FLD, gridData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Text.Trim(), null, true);
					} // END: Trada 10-18-2005


					//drowView = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME,MST_UnitOfMeasureTable.CODE_FLD,gridData[gridData.Row,gridData.Col].ToString(), null, true);
					if(drowView != null)
					{
						gridData.Columns[SO_SaleOrderDetailTable.SELLINGUMID_FLD].Value = drowView[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD];
						gridData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = drowView[MST_UnitOfMeasureTable.CODE_FLD];
					}
				}

				if (gridData.Columns[gridData.Col] == gridData.Columns[SO_CancelReasonTable.TABLE_NAME + SO_CancelReasonTable.CODE_FLD])
				{
					// HACK: Trada 10-18-2005
					if (gridData.AddNewMode != AddNewModeEnum.AddNewPending)
					{
						drowView = FormControlComponents.OpenSearchForm(SO_CancelReasonTable.TABLE_NAME, SO_CancelReasonTable.CODE_FLD, gridData[gridData.Row, SO_CancelReasonTable.TABLE_NAME + SO_CancelReasonTable.CODE_FLD].ToString(), null, true);
					}
					else
					{
						drowView = FormControlComponents.OpenSearchForm(SO_CancelReasonTable.TABLE_NAME, SO_CancelReasonTable.CODE_FLD, gridData.Columns[SO_CancelReasonTable.TABLE_NAME + SO_CancelReasonTable.CODE_FLD].Text.Trim(), null, true);
					}
					// END: Trada 10-18-2005
					//DataRowView drowView = FormControlComponents.OpenSearchForm(SO_CancelReasonTable.TABLE_NAME,SO_CancelReasonTable.CODE_FLD,gridData[gridData.Row,gridData.Col].ToString(), null, true);
					if(drowView != null)
					{
						gridData.Columns[SO_SaleOrderDetailTable.CANCELREASONID_FLD].Value = drowView[SO_CancelReasonTable.CANCELREASONID_FLD];
						gridData.Columns[SO_CancelReasonTable.TABLE_NAME + SO_CancelReasonTable.CODE_FLD].Value = drowView[SO_CancelReasonTable.CODE_FLD];
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

		private void chkVAT_CheckedChanged(object sender, System.EventArgs e)
		{
			if(dstSaleOrderDetail.Tables.Count == 0) return;
			foreach (DataRow objRow in dstSaleOrderDetail.Tables[0].Rows)
			{ 
				if(objRow.RowState == DataRowState.Deleted) continue;
				//•	If VAT field checked then the system calculate VAT amount base on unit price and VAT percent of the item
				if(chkVAT.Checked == true)
				{
					try
					{
						//DataRow objProduct = dstProduct.Tables[0].Rows.Find(objRow[SO_SaleOrderDetailTable.PRODUCTID_FLD]);
						//SaleOrderBO boSale = new SaleOrderBO();
						ITM_ProductVO voProduct =  (ITM_ProductVO)boSaleOrder.GetProduct(int.Parse(objRow[SO_SaleOrderDetailTable.PRODUCTID_FLD].ToString()));
						objRow[SO_SaleOrderDetailTable.VATPERCENT_FLD] = voProduct.VAT;
						decimal decDiscountAmount;
						if ((objRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != null) && (objRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value))
						{
							decDiscountAmount = decimal.Parse(objRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());
						}
						else
							decDiscountAmount = 0;
						objRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] = (decimal.Parse(objRow[SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString())
																		*decimal.Parse(objRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString()) - decDiscountAmount)
																		*(decimal)voProduct.VAT/ONE_HUNDRED;
					}
					catch
					{
						objRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] = 0;
					}
				}
				else
				{
					objRow[SO_SaleOrderDetailTable.VATPERCENT_FLD] = 
					objRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] = DBNull.Value;
				}
			}
			ReCalculate();
		}

		private void chkExportTax_CheckedChanged(object sender, System.EventArgs e)
		{
			if(dstSaleOrderDetail.Tables.Count == 0) return;
			foreach (DataRow objRow in dstSaleOrderDetail.Tables[0].Rows)
			{
				if(objRow.RowState == DataRowState.Deleted) continue;
				//•	If Export Tax field checked then the system calculate export tax amount base on unit price and export tax percent of the item 
				if(chkExportTax.Checked == true)
				{
					try
					{
						//DataRow objProduct = dstProduct.Tables[0].Rows.Find(objRow[SO_SaleOrderDetailTable.PRODUCTID_FLD]);
						//SaleOrderBO boSale = new SaleOrderBO();
						ITM_ProductVO voProduct =  (ITM_ProductVO)boSaleOrder.GetProduct(int.Parse(objRow[SO_SaleOrderDetailTable.PRODUCTID_FLD].ToString()));
						objRow[SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD] = voProduct.ExportTax;
						decimal decDiscountAmount;
						if ((objRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != null) && (objRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value))
						{
							decDiscountAmount = decimal.Parse(objRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());
						}
						else
							decDiscountAmount = 0;
						objRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] = (decimal.Parse(objRow[SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString())
							*decimal.Parse(objRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString()) - decDiscountAmount)
							*(decimal)voProduct.ExportTax /ONE_HUNDRED;
					}
					catch
					{
						objRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] = 0;
					}
				}
				else
				{
					objRow[SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD] = 
					objRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] = DBNull.Value;
				}
			}
			ReCalculate();
		}

		private void chkSpecialTax_CheckedChanged(object sender, System.EventArgs e)
		{
			if(dstSaleOrderDetail.Tables.Count == 0) return;
			foreach (DataRow objRow in dstSaleOrderDetail.Tables[0].Rows)
			{
				if(objRow.RowState == DataRowState.Deleted) continue;
				//•	If Special Tax field checked then the system calculate special tax amount base on unit price and special tax percent of the item 
				if(chkSpecialTax.Checked == true)
				{
					try
					{
						//DataRow objProduct = dstProduct.Tables[0].Rows.Find(objRow[SO_SaleOrderDetailTable.PRODUCTID_FLD]);
						//SaleOrderBO boSale = new SaleOrderBO();
						ITM_ProductVO voProduct =  (ITM_ProductVO)boSaleOrder.GetProduct(int.Parse(objRow[SO_SaleOrderDetailTable.PRODUCTID_FLD].ToString()));
						objRow[SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD] = voProduct.SpecialTax;
						decimal decDiscountAmount, decVatAmount, decExpTax;
						if ((objRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != null) && (objRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value))
						{
							decDiscountAmount = decimal.Parse(objRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());
						}
						else
							decDiscountAmount = 0;
						if ((objRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] != null) && (objRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value))
						{
							decVatAmount = decimal.Parse(objRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD].ToString());
						}
						else
							decVatAmount = 0;
						if ((objRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] != null) && (objRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] != DBNull.Value))
						{
							decExpTax = decimal.Parse(objRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].ToString());
						}
						else
							decExpTax = 0;
						objRow[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] = (decimal.Parse(objRow[SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString())
							*decimal.Parse(objRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString()) - decDiscountAmount + decVatAmount + decExpTax)
							*(decimal)voProduct.SpecialTax/ONE_HUNDRED;
					}
					catch
					{
						objRow[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] = 0;
					}
				}
				else
				{
					objRow[SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD] = 
					objRow[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] = DBNull.Value;
				}
			}
			ReCalculate();
		}

		private void gridData_OnAddNew(object sender, System.EventArgs e)
		{
			const decimal DEFAUFT_QUANTITY = 0;
			const decimal DEFAUFT_UMRATE = 0;
			int intMaxLine = 0;
			int intMaxID = 0;

			// if detail has no row
			if(gridData.Row > 0)
			{
				intMaxLine = int.Parse(gridData[gridData.Row-1,SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString());
				dstSaleOrderDetail.Tables[0].DefaultView.Sort = SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD;
				intMaxID = int.Parse(gridData[gridData.Row-1,SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString());
				dstSaleOrderDetail.Tables[0].DefaultView.Sort = string.Empty;
			}
			gridData.Columns[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].Value = ++intMaxLine;
			gridData.Columns[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].Value = ++intMaxID;
			gridData.Columns[SO_SaleOrderDetailTable.UMRATE_FLD].Value = DEFAUFT_UMRATE;
			gridData.Columns[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].Value = DEFAUFT_QUANTITY;
		}
		private void gridData_AfterDelete(object sender, System.EventArgs e)
		{
		}
		
		private void gridData_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridData_BeforeDelete()";
			try
			{
				if(!CheckBeforeDeleteAllRows())
				{
					e.Cancel = true;
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
			if(e.KeyCode == Keys.F4)
				if(btnCustomer.Enabled)
					btnCustomer_Click(sender,e);
		}
		private void txtCustomerName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnCustomerName.Enabled)
					btnCustomerName_Click(sender,e);
		}

		#endregion other events
				
		#region other functions
		private void InitVariable()
		{
			const string METHOD_NAME = THIS + ".InitVariable()";
			try
			{
				//SaleOrderBO boSaleOrder = new SaleOrderBO();
				DataSet dstCCN = new DataSet();
				dstCCN = boSaleOrder.ListCCN();
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[0],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);

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

		private void InitGrid(int pintSOMasterID)
		{
			const int FORMATED_STRING_40 = 40; //40 chars
			const int FORMATED_STRING_24 = 24; //24 chars
            dstSaleOrderDetail = boSaleOrder.ListDetailByMaster(pintSOMasterID);
            if (dstSaleOrderDetail.Tables.Count == 0) return;
            DataColumn[] objColumns = new DataColumn[1];
            objColumns[0] = dstSaleOrderDetail.Tables[0].Columns[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD];

            // fill data into grid
            dstSaleOrderDetail.Tables[0].PrimaryKey = objColumns;
            objColumns[0].AutoIncrement = true;
            gridData.DataSource = dstSaleOrderDetail.Tables[0];

            FormControlComponents.RestoreGridLayout(gridData, dtbGridDesign);

            gridData.Splits[0].DisplayColumns[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].Locked = true;
            gridData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Locked = true;
            gridData.Splits[0].DisplayColumns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Locked = true;
            gridData.Splits[0].DisplayColumns[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].HeadingStyle.ForeColor = Color.Maroon;
            gridData.Columns[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].Aggregate = AggregateEnum.Count;
            gridData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].HeadingStyle.ForeColor = Color.Maroon;
            gridData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
            gridData.Columns[ITM_ProductTable.CODE_FLD].DataWidth = FORMATED_STRING_24;
            gridData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
            gridData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Button = true;
            gridData.Columns[ITM_ProductTable.REVISION_FLD].DataWidth = FORMATED_STRING_24;
            gridData.Columns[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            gridData.Splits[0].DisplayColumns[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].HeadingStyle.ForeColor = Color.Maroon;
            gridData.Splits[0].DisplayColumns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].HeadingStyle.ForeColor = Color.Maroon;
            gridData.Splits[0].DisplayColumns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Button = true;
            gridData.Columns[SO_SaleOrderDetailTable.UNITPRICE_FLD].NumberFormat = "##############,0.0000";
            gridData.Splits[0].DisplayColumns[SO_SaleOrderDetailTable.UNITPRICE_FLD].HeadingStyle.ForeColor = Color.Maroon;
            gridData.Columns[SO_SaleOrderDetailTable.VATPERCENT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            gridData.Columns[SO_SaleOrderDetailTable.VATAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            gridData.Columns[SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            gridData.Columns[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            // SpecialTaxAmount column
            gridData.Columns[SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            gridData.Columns[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            gridData.Splits[0].DisplayColumns[SO_SaleOrderDetailTable.TOTALAMOUNT_FLD].Locked = true;
            gridData.Columns[SO_SaleOrderDetailTable.TOTALAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            gridData.Columns[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            gridData.Columns[SO_SaleOrderDetailTable.NETAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            gridData.Splits[0].DisplayColumns[SO_SaleOrderDetailTable.NETAMOUNT_FLD].Locked = true;
            gridData.Splits[0].DisplayColumns[SO_CancelReasonTable.TABLE_NAME + SO_CancelReasonTable.CODE_FLD].Button = true;
            gridData.Columns[SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD].DataWidth = FORMATED_STRING_40;
            gridData.Columns[SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD].DataWidth = FORMATED_STRING_40;
            gridData.Columns[SO_SaleOrderDetailTable.AUTOCOMMIT_FLD].ValueItems.Presentation = PresentationEnum.CheckBox;

            //lock two column about Customer Item Ref
            gridData.Splits[0].DisplayColumns[SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD].Locked = true;
            gridData.Splits[0].DisplayColumns[SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD].Locked = true;
		}

		private void ControlsToVO()
		{
			const string METHOD_NAME = THIS + ".ControlsToVO()";
			try
			{
				voSaleOrderMaster.PartyID = int.Parse(txtCustomer.Tag.ToString());
				voSaleOrderMaster.Code = txtOrderNo.Text.Trim();
				//if(txtExchRate.Tag != null)
				voSaleOrderMaster.ExchangeRate = decimal.Parse(txtExchRate.Value.ToString());
				voSaleOrderMaster.CustomerPurchaseOrderNo = txtCustomerPO.Text.Trim();
				voSaleOrderMaster.SpecialTax = chkSpecialTax.Checked;
				voSaleOrderMaster.ExportTax = chkExportTax.Checked;
				voSaleOrderMaster.VAT = chkVAT.Checked;
				voSaleOrderMaster.ShipCompleted = chkShipCompleted.Checked;
				voSaleOrderMaster.TotalAmount = decimal.Parse(txtTotalAmount.Text.Trim());
				voSaleOrderMaster.TotalDiscountAmount = decimal.Parse(txtTotalDiscount.Text.Trim());
				voSaleOrderMaster.TotalExportAmount = decimal.Parse(txtTotalExpTax.Text.Trim());
				voSaleOrderMaster.TotalNetAmount = decimal.Parse(txtTotalNetAmount.Text.Trim());
				voSaleOrderMaster.TotalSpecialTaxAmount = decimal.Parse(txtTotalSpecTax.Text.Trim());
				voSaleOrderMaster.TotalVATAmount = decimal.Parse(txtTotalVAT.Text.Trim());

				if(txtSalesType.Tag != null)
					voSaleOrderMaster.SaleTypeID = int.Parse(txtSalesType.Tag.ToString());
				if(txtBillToLoc.Tag != null)
					voSaleOrderMaster.BillToLocID = int.Parse(txtBillToLoc.Tag.ToString());
				if(txtBuyLoc.Tag != null)
					voSaleOrderMaster.BuyingLocID = int.Parse(txtBuyLoc.Tag.ToString());
				if(txtCarrier.Tag != null)
					voSaleOrderMaster.CarrierID = int.Parse(txtCarrier.Tag.ToString());
				if(txtCurrency.Tag != null)
					voSaleOrderMaster.CurrencyID = int.Parse(txtCurrency.Tag.ToString());
				if(txtDeliveryTerms.Tag != null)
					voSaleOrderMaster.DeliveryTermsID = int.Parse(txtDeliveryTerms.Tag.ToString());
				if(txtDiscTerms.Tag != null)
					voSaleOrderMaster.DiscountTermsID = int.Parse(txtDiscTerms.Tag.ToString());
				if(txtContact.Tag != null)
					voSaleOrderMaster.PartyContactID = int.Parse(txtContact.Tag.ToString());
				if(txtCustomer.Tag != null)
					voSaleOrderMaster.PartyID = int.Parse(txtCustomer.Tag.ToString());
				if(txtPause.Tag != null)
					voSaleOrderMaster.PauseID = int.Parse(txtPause.Tag.ToString());
				if(txtPayTerms.Tag != null)
					voSaleOrderMaster.PaymentTermsID = int.Parse(txtPayTerms.Tag.ToString());
				if(txtSalesType.Tag != null)
					voSaleOrderMaster.SaleTypeID = int.Parse(txtSalesType.Tag.ToString());
				if(txtShipFromLoc.Tag != null)
					voSaleOrderMaster.ShipFromLocID = int.Parse(txtShipFromLoc.Tag.ToString());
				if(txtShipToLoc.Tag != null)
					voSaleOrderMaster.ShipToLocID = int.Parse(txtShipToLoc.Tag.ToString());
				if(txtSalesRepres.Tag != null)
					voSaleOrderMaster.SalesRepresentativeID = int.Parse(txtSalesRepres.Tag.ToString());
				if(txtGateType.Tag != null)
					voSaleOrderMaster.TypeID = int.Parse(txtGateType.Tag.ToString());

				if(cboCCN.Text != string.Empty)
					voSaleOrderMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
				voSaleOrderMaster.TypeID = int.Parse(txtGateType.Tag.ToString());
				if(cboPriority.Text != string.Empty)
				{
					voSaleOrderMaster.Priority = int.Parse(cboPriority.Text);
				} 
				else voSaleOrderMaster.Priority = ONE;
				voSaleOrderMaster.TransDate = (DateTime)cboTransDate.Value;
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

		private void VOToControls(SO_SaleOrderMasterVO pvoSOMaster)
		{
			const string METHOD_NAME = THIS + ".VOToControls()";
			try
			{
				cboPriority.SelectedIndex = (int)pvoSOMaster.Priority - 1;
				if (pvoSOMaster.PartyID > 0)
				{
					//SaleOrderBO boSale = new SaleOrderBO();
					DataSet dstParty = boSaleOrder.ListParty();
					if (dstParty.Tables.Count > 0)
					{
						string strFilter = MST_PartyTable.PARTYID_FLD + "=" + pvoSOMaster.PartyID.ToString();
						DataRow[] objRows = dstParty.Tables[0].Select(strFilter);
						if (objRows.Length > 0)
						{
							txtCustomer.Tag = pvoSOMaster.PartyID;
							txtCustomer.Text = objRows[0][MST_PartyTable.CODE_FLD].ToString();
							txtCustomerName.Text = objRows[0][MST_PartyTable.NAME_FLD].ToString();
						}
					}
//					UtilsBO boUtils = new UtilsBO();
//					MST_PartyVO voParty = boUtils.
				}
				else
				{
					txtCustomer.Tag = null;
					txtCustomer.Text = string.Empty;
					txtCustomerName.Text = string.Empty;
					//ClearForm();
				}
				txtOrderNo.Text = pvoSOMaster.Code;
				if((DateTime.MinValue < pvoSOMaster.TransDate) && (pvoSOMaster.TransDate < DateTime.MaxValue))
					cboTransDate.Value = pvoSOMaster.TransDate;
				else
					cboTransDate.Value = DBNull.Value;
				// txtExchRate.Text = pvoSOMaster.ExchangeRateID;
//				FillExchangeRate(pvoSOMaster.CurrencyID);
				txtCustomerPO.Text = pvoSOMaster.CustomerPurchaseOrderNo;
				chkSpecialTax.Checked = pvoSOMaster.SpecialTax;
				chkExportTax.Checked = pvoSOMaster.ExportTax;
				chkVAT.Checked = pvoSOMaster.VAT;
				chkShipCompleted.Checked = pvoSOMaster.ShipCompleted;
				txtShipFromLoc.Tag = pvoSOMaster.ShipFromLocID;
				
				txtGateType.Tag = pvoSOMaster.TypeID;
				if(pvoSOMaster.CCNID > 0)
					cboCCN.SelectedValue = pvoSOMaster.CCNID;

				#region HACK: DEL Trada 28-10-2005

//				else
//					cboCCN.Text = string.Empty;

				#endregion 

				//cboPriority.SelectedIndex = (int)pvoSOMaster.Priority - 1;
				InitGrid(pvoSOMaster.SaleOrderMasterID);
				Calculate();
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
		private void ClearForm()
		{
			try
			{
				cboTransDate.Value = DBNull.Value;
				txtBillToLoc.Text = string.Empty;
				txtBuyLoc.Text = string.Empty;
				txtCarrier.Text = string.Empty;
				txtContact.Text = string.Empty;
				txtCurrency.Text = string.Empty;
				txtCustomer.Text = string.Empty;
				txtCustomerName.Text = string.Empty;
				txtCustomerPO.Text = string.Empty;
				txtDiscTerms.Text = string.Empty;
				txtDeliveryTerms.Text = string.Empty;
				txtExchRate.Value = null;
				txtOrderNo.Text = string.Empty;
				txtPause.Text = string.Empty;
				txtPayTerms.Text = string.Empty;
				txtSalesRepres.Text = string.Empty;
				txtSalesType.Text = string.Empty;
				txtShipFromLoc.Text = string.Empty;
				txtShipToLoc.Text = string.Empty;
				txtTotalAmount.Value = null;
				txtTotalDiscount.Value = null;
				txtTotalExpTax.Value = null;
				txtTotalNetAmount.Value = null;
				txtTotalSpecTax.Value = null;
				txtTotalVAT.Value = null;
				txtGateType.Text = string.Empty;
				txtGateType.Tag = null;
				cboPriority.Text = string.Empty;
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
		private bool CheckMandatory(Control pobjControl)
		{
			if (pobjControl.Text.Trim() == string.Empty)
			{
				return true;
			}
			return false;
		}

		private void SetEditableControl(bool pblnEnable)
		{
			btnCustomer.Enabled = 
			txtCustomer.Enabled = pblnEnable;
			btnOrderNo.Enabled = !pblnEnable;
			txtOrderNo.Enabled = !pblnEnable;
			btnCustomerName.Enabled = 
			txtCustomerName.Enabled = pblnEnable;

			txtExchRate.Enabled = pblnEnable;
			txtCustomerPO.Enabled = pblnEnable;
			chkSpecialTax.Enabled = pblnEnable;
			chkExportTax.Enabled = pblnEnable;
			chkVAT.Enabled = pblnEnable;
			// chkShipCompleted.Enabled = pblnEnable;
			btnBuyLoc.Enabled = 
			txtBuyLoc.Enabled = pblnEnable;
			btnCurrency.Enabled = 
			txtCurrency.Enabled = pblnEnable;
			btnContact.Enabled = 
			txtContact.Enabled = pblnEnable;
			btnDeliveryTerms.Enabled = 
			txtDeliveryTerms.Enabled = pblnEnable;
			btnPayTerms.Enabled = 
			txtPayTerms.Enabled = pblnEnable;
			btnCarrier.Enabled = 
			txtCarrier.Enabled = pblnEnable;
			btnPause.Enabled = 
			txtPause.Enabled = pblnEnable;
			btnDiscTerms.Enabled = 
			txtDiscTerms.Enabled = pblnEnable;
			btnSalesType.Enabled = 
			txtSalesType.Enabled = pblnEnable;
			btnBillToLoc.Enabled = 
			txtBillToLoc.Enabled = pblnEnable;
			btnShipToLoc.Enabled = 
			txtShipToLoc.Enabled = pblnEnable;
			btnShipFromLoc.Enabled = 
			txtShipFromLoc.Enabled = pblnEnable;
			btnSalesRepres.Enabled = 
			txtSalesRepres.Enabled = pblnEnable;
			btnGateType.Enabled = 
				txtGateType.Enabled = pblnEnable;
			cboPriority.Enabled = pblnEnable;
			cboTransDate.Enabled = pblnEnable;

			btnAdvance.Enabled = !pblnEnable;
			btnDeliverySchedule.Enabled = !pblnEnable;
			btnPrint.Enabled = !pblnEnable;
			// Set editable for gridData
            gridData.AllowAddNew = pblnEnable;
			gridData.AllowDelete = pblnEnable;
			gridData.AllowUpdate = pblnEnable;			
		}

		private void SetEnableButtons() 
		{
			switch (mFormAction) 
			{
				case EnumAction.Add:
					//Disable Buttons
					btnAdd.Enabled = false;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
//					btnCopy.Enabled = false;
//					btnBOM.Enabled = false;
//					btnRouting.Enabled = false;
//					btnCost.Enabled = false;
//					btnSearchProductCode.Enabled = false;
//					btnSearchProductDescription.Enabled = false;
					

					//Enable Buttons
					btnSave.Enabled = true;
//					btnSearchVendor.Enabled = true;

					#region // HACK: DuongNA 2005-10-21
					mnuImportNew.Enabled = true;
					mnuImportUpdate.Enabled = false;
					#endregion // END: DuongNA 2005-10-21

					break;
				case EnumAction.Delete:
					btnAdd.Enabled = true;
					btnSave.Enabled = false;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					btnDeliverySchedule.Enabled = false;
					btnPrint.Enabled = false;
					btnHelp.Enabled	= false;

					#region // HACK: DuongNA 2005-10-21
					mnuImportNew.Enabled = false;
					mnuImportUpdate.Enabled = false;
					#endregion // END: DuongNA 2005-10-21

					break;
				case EnumAction.Edit:
					//Disable Buttons
					btnAdd.Enabled = false;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
//					btnCopy.Enabled = false;
//					btnBOM.Enabled = false;
//					btnRouting.Enabled = false;
//					btnCost.Enabled = false;
//					btnSearchProductCode.Enabled = false;
//					btnSearchProductDescription.Enabled = false;

					//Enable Buttons
					btnSave.Enabled = true;
//					btnSearchVendor.Enabled = true;

					#region // HACK: DuongNA 2005-10-21
					mnuImportNew.Enabled = false;
					mnuImportUpdate.Enabled = true;
					#endregion // END: DuongNA 2005-10-21

					break;
				case EnumAction.Default:
					//Disable Buttons
					btnSave.Enabled = false;
//					btnSearchVendor.Enabled = false;

//					ExtendedProButton extendPro ;
//
//					//Enable Buttons
//					extendPro = (ExtendedProButton) btnAdd.Tag;
//					if (!extendPro.IsDisable)
//					{
						btnAdd.Enabled = true;
//
//					}
//					else
//					{
//						btnAdd.Enabled = false;
//
//					}
//					btnSearchProductCode.Enabled = true;
//					btnSearchProductDescription.Enabled = true;
					

					if (voSaleOrderMaster.SaleOrderMasterID >0) 
					{
						//Edit 
//						extendPro = (ExtendedProButton) btnEdit.Tag;
//						if (!extendPro.IsDisable) 
//						{
							btnEdit.Enabled = true;
							btnUpdateUnitPrice.Enabled = true;
//						}
//						else
//						{
//							btnEdit.Enabled = false;
//						}
//						//Delete
//						extendPro = (ExtendedProButton) btnDelete.Tag;
//						if (!extendPro.IsDisable)
//						{
							btnDelete.Enabled = true;
//						}
//						else
//						{
//							btnDelete.Enabled = false;
//						}

					}
					else 
					{
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						btnUpdateUnitPrice.Enabled = false;
//						btnCopy.Enabled = false;
//						btnBOM.Enabled = false;
//						btnRouting.Enabled = false;
//						btnCost.Enabled = false;
					}

					#region // HACK: DuongNA 2005-10-21
					mnuImportNew.Enabled = false;
					mnuImportUpdate.Enabled = false;
					#endregion // END: DuongNA 2005-10-21

					break;
			}
		}
		private void Calculate()
		{
			//•	The system calculate sum of Total Amount = sum(Total Amount column)
			txtTotalAmount.Value = TotalColumn(SO_SaleOrderDetailTable.TOTALAMOUNT_FLD).ToString();
			//•	The system calculate sum of Total Discount = sum(Discount Amount column)
			txtTotalDiscount.Value = TotalColumn(SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD).ToString();
			//•	The system calculate sum of Total Export Tax = sum(Export Tax Amount column)
			txtTotalExpTax.Value = TotalColumn(SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD).ToString();
			//•	The system calculate sum of Total Net Amount = sum(Net Amount column)
			txtTotalNetAmount.Value = TotalColumn(SO_SaleOrderDetailTable.NETAMOUNT_FLD).ToString();
			//•	The system calculate sum of Total Special Tax = sum(Special Tax Amount column)
			txtTotalSpecTax.Value = TotalColumn(SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD).ToString();
			//•	The system calculate sum of Total VAT = sum(VAT Amount column)
			txtTotalVAT.Value = TotalColumn(SO_SaleOrderDetailTable.VATAMOUNT_FLD).ToString();
		}

		private void ReCalculate()
		{
			if(dstSaleOrderDetail.Tables.Count > 0)
				foreach (DataRow objRow in dstSaleOrderDetail.Tables[0].Rows)
				{
					if(objRow.RowState == DataRowState.Deleted) continue;
					#region // HACK: DuongNA 2005-10-26
					if(int.Parse(objRow[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString()) < 0)
					{
						continue;
					}
					#endregion // END: DuongNA 2005-10-26	
					//•	If VAT field checked then the system calculate VAT amount base on unit price and VAT percent of the item
					// SaleOrderBO boSale = new SaleOrderBO();
					if(objRow[SO_SaleOrderDetailTable.PRODUCTID_FLD] == DBNull.Value) continue;
					decimal decDiscountAmount;
					if(objRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
					{
						decDiscountAmount = decimal.Parse(objRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());
					}
					else
					{
						decDiscountAmount = 0;
					}
					//ITM_ProductVO voProduct = (ITM_ProductVO)boSale.GetProductVO(int.Parse(objRow[SO_SaleOrderDetailTable.PRODUCTID_FLD].ToString()));
					if(chkVAT.Checked == true)
					{
						try
						{
							// objRow[SO_SaleOrderDetailTable.VATPERCENT_FLD] = voProduct.VAT;
							objRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] =  (decimal.Parse(objRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString())
								* decimal.Parse(objRow[SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount)
								* decimal.Parse(objRow[SO_SaleOrderDetailTable.VATPERCENT_FLD].ToString())/ONE_HUNDRED;
						}
						catch
						{
							objRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] = 0;
						}
					}
					//•	If Export Tax field checked then the system calculate export tax amount base on unit price and export tax percent of the item 
					if(chkExportTax.Checked == true)
					{
						try
						{
							// objRow[SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD] = voProduct.ImportTax;
							objRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] = (decimal.Parse(objRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString())
								* decimal.Parse(objRow[SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount)
								*decimal.Parse(objRow[SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD].ToString())/ONE_HUNDRED;
						}
						catch
						{
							objRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] = 0;
						}
					}
					//•	If Special Tax field checked then the system calculate special tax amount base on unit price and special tax percent of the item 
					if(chkSpecialTax.Checked == true)
					{
						try
						{
							decimal decVatAmount, decExpTax;
							if ((objRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] != null) && (objRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value))
							{
								decVatAmount = decimal.Parse(objRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD].ToString());
							}
							else
								decVatAmount = 0;
							if ((objRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] != null) && (objRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] != DBNull.Value))
							{
								decExpTax = decimal.Parse(objRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].ToString());
							}
							else
								decExpTax = 0;
							// objRow[SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD] = voProduct.SpecialTax;
							objRow[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] = ((decimal.Parse(objRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString())
								* decimal.Parse(objRow[SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount) + decVatAmount + decExpTax)
								*decimal.Parse(objRow[SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD].ToString())/ONE_HUNDRED;
						}
						catch
						{
							objRow[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] = 0;
						}
					}
					//•	If the discount term is selected then the system calculate the discount amount base on discount term conditions
					decimal decDiscountTerm = 0;
					try
					{
						decDiscountTerm = decimal.Parse(objRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());
					}
					catch {}
					// Get VAT, Export Tax, Special Tax
					decimal decVAT;
					try
					{
						decVAT = decimal.Parse(objRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD].ToString());
					}
					catch
					{
						decVAT = 0;
					}
					decimal decImportTax;
					try
					{
						decImportTax = decimal.Parse(objRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].ToString());
					}
					catch
					{
						decImportTax = 0;
					}
					decimal decSpecialTax;
					try
					{
						decSpecialTax = decimal.Parse(objRow[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD].ToString());
					}
					catch
					{
						decSpecialTax = 0;
					}
					//•	The system calculate Total Amount = (quantity * unit price)+ VAT + Export Tax + Special Tax
					try
					{
						objRow[SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] = decimal.Parse(objRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString())
							* decimal.Parse(objRow[SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount + decVAT + decImportTax + decSpecialTax;
					}
					catch
					{
						// do nothing
					}
					//•	The system calculate Net Amount = Total Amount – Discount Amount
					try
					{
						//objRow[SO_SaleOrderDetailTable.NETAMOUNT_FLD] = decimal.Parse(objRow[SO_SaleOrderDetailTable.TOTALAMOUNT_FLD].ToString()) - decDiscountTerm;
						objRow[SO_SaleOrderDetailTable.NETAMOUNT_FLD] = decimal.Parse(objRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString())
							* decimal.Parse(objRow[SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount;
					}
					catch
					{
						// do nothing
					}
				}
			gridData.Refresh();
			// Sum all for lable
			Calculate();				
			
		}
		private void StoreDatabase()
		{
			const string METHOD_NAME = THIS + ".StoreDatabase()";
			ControlsToVO();
			try
			{
				//SaleOrderBO boSaleOrder = new SaleOrderBO();
				if(mFormAction == EnumAction.Add)
				{
					voSaleOrderMaster.SaleOrderMasterID = boSaleOrder.AddNewSaleOrder(voSaleOrderMaster,dstSaleOrderDetail);
					InitGrid(voSaleOrderMaster.SaleOrderMasterID);
				}
				else if(mFormAction == EnumAction.Edit)
				{
					boSaleOrder.UpdateSaleOrder(voSaleOrderMaster,dstSaleOrderDetail);
					InitGrid(voSaleOrderMaster.SaleOrderMasterID);
				}

				Security.UpdateUserNameModifyTransaction(this,SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD,voSaleOrderMaster.SaleOrderMasterID);

				mFormAction = EnumAction.Default;
				SetEnableButtons();
				SetEditableControl(false);
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
				blnHasError = false;
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
					txtOrderNo.Focus();
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
		private bool IsNotExistCustomer()
		{
			try
			{
				string strCustomer = txtCustomer.Text.Trim();
				string strCustomerName = txtCustomerName.Text.Trim();
				if((strCustomer.Length > 0) && (strCustomerName.Length > 0))
				{
					int intCount = 0;
					//SaleOrderBO boSale = new SaleOrderBO();
					DataSet dstParty = boSaleOrder.ListParty();
					if(dstParty.Tables[0].Rows.Count > 0)
					{
						string strCondition = "(" + MST_PartyTable.CODE_FLD + "='" + strCustomer + "'" + ") AND ("
							 + MST_PartyTable.NAME_FLD + "='" + strCustomerName + "'" + ")";
						intCount = dstParty.Tables[0].Select(strCondition).Length;
					}
					if(intCount == 0) return true;
				}
				return false;
			}
			catch
			{
				return true;
			}	
		}
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
				if(cboTransDate.Value == DBNull.Value)
				{
					// Input Transaction date before execute this function
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_TRANSACTION_BEFORE,MessageBoxIcon.Exclamation);
					txtExchRate.Value = DEFAULT_RATE;
					cboTransDate.Focus();
					return intExchangeRateID;
				}
				DateTime dtmOrderDate = (DateTime)cboTransDate.Value;
				//SaleOrderBO boOrder = new SaleOrderBO();
				MST_ExchangeRateVO voExchange = (MST_ExchangeRateVO) boSaleOrder.GetExchangeRate(pintCurrencyID,dtmOrderDate);
				if(voExchange.ExchangeRateID == 0)
				{
					// Do not found any Exchange Rate records which (begin effective date <= the current date <= end of effective date)
					PCSMessageBox.Show(ErrorCode.MESSAGE_NOT_FOUND_EXCHANGE_RATE,MessageBoxIcon.Exclamation);
//					txtExchRate.Value = DEFAULT_RATE;
//					txtExchRate.Focus();
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
		private decimal TotalColumn(string pstrColumnName)
		{
			decimal decTotal = 0;
			if(dstSaleOrderDetail.Tables.Count > 0)
				foreach (DataRow objRow in dstSaleOrderDetail.Tables[0].Rows)
				{
					if(objRow.RowState == DataRowState.Deleted) continue;
					
					#region // HACK: DuongNA 2005-10-26
					if (int.Parse(objRow[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString()) < 0)
					{
						continue;
					}
					#endregion // END: DuongNA 2005-10-26

					decimal decRow;
					try
					{
						decRow = decimal.Parse(objRow[pstrColumnName].ToString());
					}
					catch
					{
						decRow = 0;
					}
					decTotal = decTotal + decRow;
				}
			return decimal.Round(decTotal,2);
		}

		private void OnEnterControl(object sender,EventArgs e)
		{
			FormControlComponents.OnEnterControl(sender,e);
		}

		private void OnLeaveControl(object sender,EventArgs e)
		{
			FormControlComponents.OnLeaveControl(sender,e);
		}

		#endregion other functions

		#region Delivery To Customer Schedule Report: Tuan TQ
		//HACKED: Added by Tuan TQ, 06 Dec, 2005: add new Delivery To Customer Schedule report
		
		private const int MAX_DAYS_IN_MONTH = 31;
		private const string APPLICATION_PATH     = @"PCSMain\bin\Debug";
				
		private const string DELIVERY_FLD_PREFIX = "DeliveryQuantity_";

		private const string DELIVERY_TIME_FLD = "DeliveryTime";
		private const string DELIVERY_DAY_FLD = "DeliveryDay";
		private const string DELIVERY_MONTH_FLD = "DeliveryMonth";
		private const string DELIVERY_YEAR_FLD = "DeliveryYear";
		
		private const string PARTS_NUMBER_FLD = "ITM_ProductCode";
		private const string PARTS_MODEL_FLD = "ITM_ProductRevision";
		private const string PARTS_UM_FLD = "MST_UnitOfMeasureCode";
		private const string PARTS_NAME_FLD = "ITM_ProductDescription";
		private const string CATEGORY_FLD = "ITM_CategoryCode";
		

		private const string ORDER_NO_FLD = "SO_SaleOrderMasterCode";
		private const string PARTY_CODE_NAME_FLD = "MST_PartyCodeName";
		private const string TOTAL_DELIVERY_FLD = "TotalDeliveryQuantity";
		
		/// <summary>
		/// Build and show Delivery To Customer Report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Tuan TQ, 06 Dec, 2005</author>
		private void ShowDelivery2CustomerReport(object sender, System.EventArgs e)
		{
			const string DELIVERY_TO_CUSTOMER_REPORT = "Delivery2CustomerReport.xml";
			const string REPORT_NAME = "Delivery2CustomerScheduleReport";

			//Report field
			const string RPT_TITLE_FIELD = "fldTitle";
			const string RPT_PAGE_HEADER = "PageHeader";

			string strReportPath = Application.StartupPath;		

			DataTable dtbResult = null;
			DataTable dtbTransform = null;

			int iReportMonth = DateTime.MinValue.Month;
			int iReportYear = DateTime.MinValue.Year;
			
			if(voSaleOrderMaster.SaleOrderMasterID <= 0) return;
			
			int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
				
			//Validate report layout path
			if(intIndex > -1)
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
			
			// check report layout file is exist or not
			if(!File.Exists(strReportPath + @"\" + DELIVERY_TO_CUSTOMER_REPORT))
			{				
				PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);					
				return;
			}	

			//create print preview object
			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO boPrintPreview = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();

			//Get delivery to customer data
			dtbResult = boPrintPreview.GetDelivery2CustomerData(voSaleOrderMaster.SaleOrderMasterID);
			//Return if data is null or no data
			if(dtbResult == null) return;
			
			//get report month & year
			if(dtbResult.Rows.Count != 0)
			{
				iReportMonth = int.Parse(dtbResult.Rows[0][DELIVERY_MONTH_FLD].ToString());
				iReportYear  = int.Parse(dtbResult.Rows[0][DELIVERY_YEAR_FLD].ToString());
			}

			//Get total days in report month
			int iDaysInMonth = DateTime.DaysInMonth(iReportYear, iReportMonth);

			//Transform Data
			dtbTransform = BuildReportTable(dtbResult, iDaysInMonth);
			
			//Return if data is null or no data
			if(dtbTransform == null) return;

			//Create builder object
			ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();			
			
			//Set report name
			reportBuilder.ReportName = REPORT_NAME;
			//Set Datasource
			reportBuilder.SourceDataTable = dtbTransform;
			//Set report layout location
			reportBuilder.ReportDefinitionFolder = strReportPath;
			reportBuilder.ReportLayoutFile = DELIVERY_TO_CUSTOMER_REPORT;				
				
			reportBuilder.UseLayoutFile = true;
			reportBuilder.MakeDataTableForRender();			

			// and show it in preview dialog				
			PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
				
			printPreview.FormTitle = this.Text;			
			//Attach report viewer
			reportBuilder.ReportViewer = printPreview.ReportViewer;				
			reportBuilder.RenderReport();
				
			//Change report header & column header
			ChangeReportDisplayInfo(reportBuilder, iReportYear, iReportMonth, iDaysInMonth);

			try
			{
				printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text;
			}
			catch{}

			reportBuilder.RefreshReport();
			printPreview.Show();
		}
		
		/// <summary>
		/// Change header & column header of report
		/// </summary>
		/// <param name="pobjReportBuilder"></param>
		/// <param name="pintReportYear.ToString()"></param>
		/// <author> Tuan TQ, 07 Dec, 2005</author>
		private void ChangeReportDisplayInfo(ReportBuilder pobjReportBuilder, int pintReportYear, int pintReportMonth, int pintDaysInMonth)
		{
			#region Constants
			
			const string SHORT_DATE_FORMAT = "d-MMM";
			const string DAY_OF_WEEK_FORMAT = "ddd";
			
			//Report Field's Name
			const string RPT_COMPANY_FLD  = "fldCompany";		
			
			const string RPT_DAY_PREFIX  = "fldDay_";
			const string RPT_DAY_OF_WEEK_PREFIX  = "fldDayOfWeek_";			
			const string RPT_QUANTITY_PREFIX  = "fldQuantity_";

			#endregion Constants
			
			//set start day in month
			DateTime dtmReportDay = new DateTime(pintReportYear, pintReportMonth, 1);

			//loop and change caption
			for(int i = 1; i <= pintDaysInMonth; i++)
			{
				pobjReportBuilder.DrawPredefinedField(RPT_DAY_PREFIX + i, dtmReportDay.ToString(SHORT_DATE_FORMAT));
				pobjReportBuilder.DrawPredefinedField(RPT_DAY_OF_WEEK_PREFIX + i, dtmReportDay.DayOfWeek.ToString().Substring(0, 3));
				//pobjReportBuilder.DrawPredefinedField(RPT_DAY_OF_WEEK_PREFIX + i, dtmReportDay.ToString(DAY_OF_WEEK_FORMAT).Substring(0, 3));

				if(dtmReportDay.DayOfWeek == DayOfWeek.Sunday)
				{
					pobjReportBuilder.Report.Fields[RPT_DAY_PREFIX + i.ToString()].BackColor = Color.Yellow;
					pobjReportBuilder.Report.Fields[RPT_DAY_PREFIX + i.ToString()].ForeColor = Color.Red;

					pobjReportBuilder.Report.Fields[RPT_DAY_OF_WEEK_PREFIX + i.ToString()].BackColor = Color.Yellow;
					pobjReportBuilder.Report.Fields[RPT_DAY_OF_WEEK_PREFIX + i.ToString()].ForeColor = Color.Red;
					
					//pobjReportBuilder.Report.Fields[RPT_QUANTITY_PREFIX + i.ToString()].Visible = false;
				}

				dtmReportDay = dtmReportDay.AddDays(1);
			}

			//Company Info
			pobjReportBuilder.DrawPredefinedField(RPT_COMPANY_FLD, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
			
			//Hide fields those are not displayed
			for(int i = pintDaysInMonth + 1; i<= MAX_DAYS_IN_MONTH; i++)
			{
				pobjReportBuilder.Report.Fields[RPT_DAY_PREFIX + i.ToString()].Visible = false;
				pobjReportBuilder.Report.Fields[RPT_DAY_OF_WEEK_PREFIX + i.ToString()].Visible = false;
				pobjReportBuilder.Report.Fields[RPT_QUANTITY_PREFIX + i.ToString()].Visible = false;
			}
		}

		/// <summary>
		/// Create Delivery To Customer Schedule data template
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ, 06 Dec, 2005</author>
		private DataTable BuildDataTemplateTable()
		{
			DataTable dtbReport = new DataTable();

			//Add column			
			dtbReport.Columns.Add(DELIVERY_TIME_FLD, typeof(System.Int32));
			dtbReport.Columns.Add(DELIVERY_DAY_FLD, typeof(System.Int32));
			dtbReport.Columns.Add(DELIVERY_MONTH_FLD, typeof(System.Int32));
			dtbReport.Columns.Add(DELIVERY_YEAR_FLD, typeof(System.Int32));

			dtbReport.Columns.Add(PARTS_NUMBER_FLD, typeof(System.String));
			dtbReport.Columns.Add(PARTS_NAME_FLD, typeof(System.String));			
			dtbReport.Columns.Add(CATEGORY_FLD, typeof(System.String));
			dtbReport.Columns.Add(PARTS_MODEL_FLD, typeof(System.String));
			dtbReport.Columns.Add(PARTS_UM_FLD, typeof(System.String));

			dtbReport.Columns.Add(ORDER_NO_FLD, typeof(System.String));
			dtbReport.Columns.Add(PARTY_CODE_NAME_FLD, typeof(System.String));			
			
			for(int i = 1; i <= MAX_DAYS_IN_MONTH; i++)
			{
				dtbReport.Columns.Add(DELIVERY_FLD_PREFIX + i.ToString(), typeof(System.Decimal));				
			}

			dtbReport.Columns.Add(TOTAL_DELIVERY_FLD, typeof(System.Decimal));			

			return dtbReport;
		}		

		/// <summary>
		/// Build Data table for Delivery To Customer Schedule Report
		/// </summary>
		/// <param name="pdtbData">Source Data</param>
		/// <returns>Data with template as data template of report</returns>
		/// <author> Tuan TQ, 06 Dec, 2005</author>
		private DataTable BuildReportTable(DataTable pdtbSourceData, int pintDaysInMonth)
		{

			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO boPrintPreview = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();

			DataTable dtbTransform = BuildDataTemplateTable();
			if(pdtbSourceData == null)
			{
				return dtbTransform;
			}
			
			//Collection of processed item
			ArrayList arlProcessedItems = new ArrayList();			
			DataRow[] arrItem = null;
			
			//select condition string
			string strSelectCondition = string.Empty;

			//keeps Product, Category and Time
			string strProductCategoryTime = string.Empty;

			//indicate data will be added or modified
			bool blnFirstTime = false;

			foreach(DataRow dtRow in pdtbSourceData.Rows)
			{
				//Mark processed items
				strProductCategoryTime = dtRow[PARTS_NUMBER_FLD].ToString() + dtRow[CATEGORY_FLD].ToString() + dtRow[DELIVERY_TIME_FLD].ToString();

				if(!arlProcessedItems.Contains(strProductCategoryTime))
				{
					strSelectCondition = PARTS_NUMBER_FLD + "='" + dtRow[PARTS_NUMBER_FLD].ToString().Replace("'", "''") + "'";
					
					if(!dtRow[CATEGORY_FLD].Equals(DBNull.Value))
					{
						strSelectCondition += " AND " + CATEGORY_FLD + "='" + dtRow[CATEGORY_FLD].ToString().Replace("'", "''") + "'";
					}
					else
					{
						strSelectCondition += " AND " + CATEGORY_FLD + " IS NULL";
					}
					
					strSelectCondition += " AND " + DELIVERY_TIME_FLD + "=" + dtRow[DELIVERY_TIME_FLD].ToString();

					//Select data related by select condition
					arrItem = pdtbSourceData.Select(strSelectCondition);
					
					//reset firt time flag
					blnFirstTime = true;

					//loop in result table and process data
					for(int i = 0; i < arrItem.Length; i++)
					{						
						InsertRow2ReportTable(arrItem[i], dtbTransform, blnFirstTime, pintDaysInMonth);
						blnFirstTime = false;
					}

					//Add item to collection as processed
					arlProcessedItems.Add(strProductCategoryTime);
				}
			}		

			return dtbTransform;
		}		
		
		/// <summary>
		/// Insert or update row into report data table
		/// </summary>
		/// <param name="ptdrSourceRow"></param>
		/// <param name="pdtbReporTable"></param>
		/// <param name="pblnFirstTime"></param>
		/// <author> Tuan TQ, 05 Dec, 2005</author>
		private void InsertRow2ReportTable(DataRow ptdrSourceRow, DataTable pdtbReporTable, bool pblnFirstTime, int pintDaysInMonth)
		{
			DataRow dtrNewRow;

			//First time means insert new row
			if(pblnFirstTime)
			{
				dtrNewRow = pdtbReporTable.NewRow();

				dtrNewRow[DELIVERY_DAY_FLD]  = ptdrSourceRow[DELIVERY_DAY_FLD];				
				dtrNewRow[DELIVERY_TIME_FLD] = ptdrSourceRow[DELIVERY_TIME_FLD];
				dtrNewRow[DELIVERY_YEAR_FLD] = ptdrSourceRow[DELIVERY_YEAR_FLD];
				dtrNewRow[DELIVERY_MONTH_FLD] = ptdrSourceRow[DELIVERY_MONTH_FLD];

				dtrNewRow[PARTS_MODEL_FLD] = ptdrSourceRow[PARTS_MODEL_FLD];
				dtrNewRow[PARTS_NAME_FLD] = ptdrSourceRow[PARTS_NAME_FLD];
				dtrNewRow[PARTS_NUMBER_FLD] = ptdrSourceRow[PARTS_NUMBER_FLD];
				dtrNewRow[PARTS_UM_FLD] = ptdrSourceRow[PARTS_UM_FLD];

				dtrNewRow[CATEGORY_FLD] = ptdrSourceRow[CATEGORY_FLD];		

				dtrNewRow[ORDER_NO_FLD] = ptdrSourceRow[ORDER_NO_FLD];
				dtrNewRow[PARTY_CODE_NAME_FLD] = ptdrSourceRow[PARTY_CODE_NAME_FLD];
				
				//Set 0 to other quantity columns
				for(int i =1; i <= pintDaysInMonth; i++)
				{
					dtrNewRow[DELIVERY_FLD_PREFIX + i] = DBNull.Value;
				}

				//dtrNewRow[TOTAL_CAPACITY_FLD] = DBNull.Value;
				dtrNewRow[TOTAL_DELIVERY_FLD] = decimal.Zero;

				//Add to colection
				pdtbReporTable.Rows.Add(dtrNewRow);
			}
			else
			{
				//Update data of last row
				dtrNewRow = pdtbReporTable.Rows[pdtbReporTable.Rows.Count - 1];
			}

			//Get deleivery day from data source
			int iDeliveryDay = int.Parse(ptdrSourceRow[DELIVERY_DAY_FLD].ToString());
			
			//Assign delivery quantity
			dtrNewRow[DELIVERY_FLD_PREFIX + iDeliveryDay.ToString()] = ptdrSourceRow[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];
			
			//Sum total delivery of item
			if(!ptdrSourceRow[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].Equals(DBNull.Value)
				&& !ptdrSourceRow[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString().Equals(string.Empty))
			{
				dtrNewRow[TOTAL_DELIVERY_FLD] = decimal.Parse(dtrNewRow[TOTAL_DELIVERY_FLD].ToString()) + decimal.Parse(ptdrSourceRow[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
			}
		}

		/// <summary>
		/// Show Sale Order Report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Tuan TQ, 06 Dec, 2005</author>
		private void ShowSaleOrderReport(object sender, System.EventArgs e)
		{
			rptSOReport.DataSource.ConnectionString = PCSComUtils.DataAccess.Utils.Instance.OleDbConnectionString;
			rptSOReport.DataSource.RecordSource = strReportQuery + " WHERE so." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + "=" + voSaleOrderMaster.SaleOrderMasterID;
			rptSOReport.Render();
			this.Cursor = Cursors.Default;
			// and show it in preview dialog
			PrintPreviewDialog printPreview = new PrintPreviewDialog();				
			printPreview.Document = rptSOReport.Document;
			printPreview.ResumeLayout();
			printPreview.ShowDialog();
			printPreview.Show();
		}	

		/// <summary>
		/// Processing Print event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPrint_Click()";
			//return immediatly if none SO selected
			if(voSaleOrderMaster.SaleOrderMasterID <= 0) return; 

			try
			{
				this.Cursor = Cursors.WaitCursor;
				ShowDelivery2CustomerReport(sender, e);
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		#endregion Delivery To Customer Schedule Report: Tuan TQ

		private void txtExchRate_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				OnLeaveControl(sender,e);
				if(txtExchRate.Value.ToString() == string.Empty)
				{
					return;
				}
				else
					if (Decimal.Parse(txtExchRate.Value.ToString()) < 0)
					{
						txtExchRate.Focus();
						txtExchRate.Select();
					}
				//if((decimal)txtExchRate.Value < 0) txtExchRate.Value = null;
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

		private void gridData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			string strColumnName = e.Column.DataColumn.DataField;
			string strColumValue = e.Column.DataColumn.Text.Trim(); //tgridViewData[tgridViewData.Row,e.ColIndex].ToString().Trim();
			DataRowView drwResult;
			UtilsBO objUtilsBO = new UtilsBO();
			const string METHOD_NAME = THIS + ".gridData_BeforeColUpdate()";
			try
			{
			switch (strColumnName)
			{
				case ITM_ProductTable.CODE_FLD:
					
					#region Rem by Trada 2005-10-13
					//					if (strColumValue == String.Empty)
					//					{
					//						break;
					//					}
					//					//search for this column
					//					dtResult = objUtilsBO.GetRows(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.CODE_FLD,strColumValue,null);
					//					if (dtResult.Rows.Count == 0)
					//					{
					//						DataRowView drvResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.CODE_FLD,String.Empty,String.Empty);
					//						if (drvResult != null)
					//						{
					//							e.Column.DataColumn.Tag = drvResult;
					//						}
					//						else
					//						{
					//							e.Cancel = true;
					//						}
					//					}
					//					else
					//					{
					//						if (dtResult.Rows.Count > 1) 
					//						{
					//							DataRowView drvResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.CODE_FLD,strColumValue,String.Empty);
					//							if (drvResult != null)
					//							{
					//								e.Column.DataColumn.Tag = drvResult;
					//							}
					//							else
					//							{
					//								e.Cancel = true;
					//							}
					//						}
					//						else
					//						{
					//							e.Column.DataColumn.Tag = dtResult.DefaultView[0];
					//						}
					//					}
					#endregion
					//HACK: Trada 2005-10-13
					if (strColumValue == String.Empty)
					{
						break;
					}

					if (gridData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, gridData.Columns[e.Column.DataColumn.DataField].Text.Trim(), null, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult;	
						}
						else
						{
							e.Cancel = true;
						}
					}
					//END Trada 2005-10-13
					break;
				case ITM_ProductTable.DESCRIPTION_FLD:
					
					#region Rem by Trada 2005-10-13
//					if (strColumValue == String.Empty)
//					{
//						break;
//					}
//					//search for this column
//					dtResult = objUtilsBO.GetRows(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.DESCRIPTION_FLD,strColumValue,null);
//					if (dtResult.Rows.Count == 0)
//					{
//						DataRowView drvResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.DESCRIPTION_FLD,String.Empty,String.Empty);
//						if (drvResult != null)
//						{
//							e.Column.DataColumn.Tag = drvResult;
//						}
//						else
//						{
//							e.Cancel = true;
//						}
//					}
//					else
//					{
//						if (dtResult.Rows.Count > 1) 
//						{
//							DataRowView drvResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.DESCRIPTION_FLD,strColumValue,String.Empty);
//							if (drvResult != null)
//							{
//								e.Column.DataColumn.Tag = drvResult;
//							}
//							else
//							{
//								e.Cancel = true;
//							}
//						}
//						else
//						{
//							e.Column.DataColumn.Tag = dtResult.DefaultView[0];
//						}
//					}
					#endregion
					//HACK: Trada 2005-10-13
					if (strColumValue == String.Empty)
					{
						break;
					}
					if (gridData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, gridData.Columns[e.Column.DataColumn.DataField].Text.Trim(), null, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult;	
						}
						else
						{
							e.Cancel = true;
						}
					}
					//END Trada 2005-10-13
					break;
				case ITM_ProductTable.REVISION_FLD:
					
					#region Rem by Trada 2005-10-13
//					if (strColumValue == String.Empty)
//					{
//						break;
//					}
//					//search for this column
//					dtResult = objUtilsBO.GetRows(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.REVISION_FLD,strColumValue,null);
//					if (dtResult.Rows.Count == 0)
//					{
//						DataRowView drvResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.REVISION_FLD,String.Empty,String.Empty);
//						if (drvResult != null)
//						{
//							e.Column.DataColumn.Tag = drvResult;
//						}
//						else
//						{
//							e.Cancel = true;
//						}
//					}
//					else
//					{
//						if (dtResult.Rows.Count > 1) 
//						{
//							DataRowView drvResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.REVISION_FLD,strColumValue,String.Empty);
//							if (drvResult != null)
//							{
//								e.Column.DataColumn.Tag = drvResult;
//							}
//							else
//							{
//								e.Cancel = true;
//							}
//						}
//						else
//						{
//							e.Column.DataColumn.Tag = dtResult.DefaultView[0];
//						}
//					}
					#endregion
					//HACK: Trada 2005-10-13
					if (strColumValue == String.Empty)
					{
						break;
					}
					if (gridData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.REVISION_FLD, gridData.Columns[e.Column.DataColumn.DataField].Text.Trim(), null, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult;	
						}
						else
						{
							e.Cancel = true;
						}
					}
					//END: Trada 2005-10-13
					break;
				case MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD:
					if (strColumValue == String.Empty)
					{
						break;
					}
					//search for this column
					#region Rem by Trada 2005-10-13
//					dtResult = objUtilsBO.GetRows(MST_UnitOfMeasureTable.TABLE_NAME,MST_UnitOfMeasureTable.CODE_FLD,strColumValue,null);
//					if (dtResult.Rows.Count == 0)
//					{
//						DataRowView drvResult = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME,MST_UnitOfMeasureTable.CODE_FLD,String.Empty,String.Empty);
//						if (drvResult != null)
//						{
//							e.Column.DataColumn.Tag = drvResult;
//						}
//						else
//						{
//							e.Cancel = true;
//						}
//					}
//					else
//					{
//						if (dtResult.Rows.Count > 1) 
//						{
//							DataRowView drvResult = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME,MST_UnitOfMeasureTable.CODE_FLD,strColumValue,String.Empty);
//							if (drvResult != null)
//							{
//								e.Column.DataColumn.Tag = drvResult;
//							}
//							else
//							{
//								e.Cancel = true;
//							}
//						}
//						else
//						{
//							e.Column.DataColumn.Tag = dtResult.DefaultView[0];
//						}
//					}
					#endregion
					//HACK: Trada 2005-10-13
					if (gridData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME, MST_UnitOfMeasureTable.CODE_FLD, gridData.Columns[e.Column.DataColumn.DataField].Text.Trim(), null, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult;	
						}
						else
						{
							e.Cancel = true;
						}
					}
					//END Trada 2005-10-13
					break;
				case SO_CancelReasonTable.TABLE_NAME + SO_CancelReasonTable.CODE_FLD:
					if (strColumValue == String.Empty)
					{
						break;
					}
					//search for this column
					#region Rem by Trada 2005-10-13
//					dtResult = objUtilsBO.GetRows(SO_CancelReasonTable.TABLE_NAME,SO_CancelReasonTable.CODE_FLD,strColumValue,null);
//					if (dtResult.Rows.Count == 0)
//					{
//						DataRowView drvResult = FormControlComponents.OpenSearchForm(SO_CancelReasonTable.TABLE_NAME,SO_CancelReasonTable.CODE_FLD,String.Empty,String.Empty);
//						if (drvResult != null)
//						{
//							e.Column.DataColumn.Tag = drvResult;
//						}
//						else
//						{
//							e.Cancel = true;
//						}
//					}
//					else
//					{
//						if (dtResult.Rows.Count > 1) 
//						{
//							DataRowView drvResult = FormControlComponents.OpenSearchForm(SO_CancelReasonTable.TABLE_NAME,SO_CancelReasonTable.CODE_FLD,strColumValue,String.Empty);
//							if (drvResult != null)
//							{
//								e.Column.DataColumn.Tag = drvResult;
//							}
//							else
//							{
//								e.Cancel = true;
//							}
//						}
//						else
//						{
//							e.Column.DataColumn.Tag = dtResult.DefaultView[0];
//						}
//					}
					#endregion
					//HACK: Trada 2005-10-13
					if (gridData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(SO_CancelReasonTable.TABLE_NAME, SO_CancelReasonTable.CODE_FLD, gridData.Columns[e.Column.DataColumn.DataField].Text.Trim(), null, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult;	
						}
						else
						{
							e.Cancel = true;
						}
					}
					//END Trada 2005-10-13
					break;
				case SO_SaleOrderDetailTable.ORDERQUANTITY_FLD:
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
						//PCSMessageBox.Show(ErrorCode.Mes)
						e.Cancel = true;
						return;
					}
					if(decimal.Parse(e.Column.DataColumn.Text) < 0) 
					{
						//e.Column.DataColumn.Value = -decimal.Parse(e.Column.DataColumn.Text);
						e.Cancel = true;
					}

					break;
				case SO_SaleOrderDetailTable.UNITPRICE_FLD:
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
						e.Column.DataColumn.Value = 0;
					}
					if(decimal.Parse(e.Column.DataColumn.Text) < 0) 
					{
						//e.Column.DataColumn.Value = -decimal.Parse(e.Column.DataColumn.Text);
						e.Cancel = true;
					}
					break;
				case SO_SaleOrderDetailTable.VATPERCENT_FLD:
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
						//e.Column.DataColumn.Value = 0;
						e.Cancel = true;
						return;
					}
					if(decimal.Parse(e.Column.DataColumn.Text) < 0) 
//						//e.Column.DataColumn.Value = -decimal.Parse(e.Column.DataColumn.Text);
						e.Cancel = true;
					break;
				case SO_SaleOrderDetailTable.VATAMOUNT_FLD:
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
//						e.Column.DataColumn.Value = 0;
						e.Cancel = true;
						return;
					}
					if(decimal.Parse(e.Column.DataColumn.Text) < 0) 
//						//e.Column.DataColumn.Value = -decimal.Parse(e.Column.DataColumn.Text);
						e.Cancel = true;
					break;
				case SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD:
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
//						e.Column.DataColumn.Value = 0;
						e.Cancel = true;
						return;
					}
					if(decimal.Parse(e.Column.DataColumn.Text) < 0) 
//						//e.Column.DataColumn.Value = -decimal.Parse(e.Column.DataColumn.Text);
						e.Cancel = true;
					break;
				case SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD:
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
//						e.Column.DataColumn.Value = 0;
						e.Cancel = true;
						return;
					}
					if(decimal.Parse(e.Column.DataColumn.Text) < 0)
//						//e.Column.DataColumn.Value = -decimal.Parse(e.Column.DataColumn.Text);
						e.Cancel = true;
					break;
				case SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD:
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
//						e.Column.DataColumn.Value = 0;
						e.Cancel = true;
						return;
					}
					if(decimal.Parse(e.Column.DataColumn.Text) < 0) 
//						//e.Column.DataColumn.Value = -decimal.Parse(e.Column.DataColumn.Text);
						e.Cancel = true;
					break;
				case SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD:
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
//						e.Column.DataColumn.Value = 0;
						e.Cancel = true;
						return;
					}
					if(decimal.Parse(e.Column.DataColumn.Text) < 0) 
//					e.Column.DataColumn.Value = -decimal.Parse(e.Column.DataColumn.Text);
						e.Cancel = true;

					break;
				case SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD:
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
					//	e.Column.DataColumn.Value = 0;
						e.Cancel = true;
						return;
					}
					if(decimal.Parse(e.Column.DataColumn.Text) < 0) 
						//e.Column.DataColumn.Value = -decimal.Parse(e.Column.DataColumn.Text);
						e.Cancel = true;
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

		private void gridData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridData_AfterColUpdate()";
			try
			{
				// no process for not visible field and readonly
				if(!gridData.Splits[0].DisplayColumns[gridData.Col].Visible) return;
				if(gridData.Splits[0].DisplayColumns[gridData.Col].Locked) return;
				// check condition to calculate
				if(gridData.AllowUpdate == false) return;
				// no table
				if(dstSaleOrderDetail.Tables.Count == 0) return;
				gridData.EditActive = false;
				// input code product
				string strColumnName = e.Column.DataColumn.DataField;
				string strColumnValue = e.Column.DataColumn.Text.Trim();
				switch (strColumnName)
				{
					case ITM_ProductTable.CODE_FLD:
						if (strColumnValue == String.Empty)
						{
							//gridData[gridData.Row,PRODUCTCODE_FLD] = String.Empty;
							gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD] = String.Empty;
							gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = String.Empty;
							gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = String.Empty;
							gridData[gridData.Row,PO_ReturnToVendorDetailTable.STOCKUMID_FLD] = String.Empty;
							gridData[gridData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.UNITPRICE_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.VATAMOUNT_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.VATPERCENT_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] = String.Empty;
							gridData[gridData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = String.Empty;
							
						}
						else
						{
							if (e.Column.DataColumn.Tag != null)
							{
								DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
								DataRow[] objRows = dstSaleOrderDetail.Tables[0].Select(SO_SaleOrderDetailTable.PRODUCTID_FLD + " = " + drvResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
								if(objRows.Length == 0)
								{
									
									gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = drvResult[ITM_ProductTable.CODE_FLD];
									gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD] = drvResult[ITM_ProductTable.PRODUCTID_FLD];
									gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
									gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = drvResult[ITM_ProductTable.REVISION_FLD];
									gridData[gridData.Row,PO_ReturnToVendorDetailTable.STOCKUMID_FLD] = drvResult[ITM_ProductTable.STOCKUMID_FLD];
									ProductItemInfoBO boProductItemInfo = new ProductItemInfoBO();
									gridData[gridData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = boProductItemInfo.GetCategoryCodeByProductID(int.Parse(drvResult[ITM_ProductTable.PRODUCTID_FLD].ToString()));
									//SaleOrderBO boOrder = new SaleOrderBO();
									ITM_ProductVO voProduct = (ITM_ProductVO)boSaleOrder.GetProduct(int.Parse(gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD].ToString()));
									gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = voProduct.Code;
									gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = voProduct.Description;
									gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = voProduct.Revision;
									gridData[gridData.Row,SO_SaleOrderDetailTable.SELLINGUMID_FLD] = voProduct.SellingUMID;
									gridData[gridData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = ((MST_UnitOfMeasureVO)boSaleOrder.GetUnitOfMeasure(voProduct.BuyingUMID)).Code;
									gridData[gridData.Row,SO_SaleOrderDetailTable.STOCKUMID_FLD] = voProduct.StockUMID;
									int intMaxLine = 0;
									if(gridData.Row > 0) intMaxLine = int.Parse(gridData[gridData.Row - 1,SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString());
									gridData[gridData.Row,SO_SaleOrderDetailTable.SALEORDERLINE_FLD] = ++intMaxLine;
									gridData[gridData.Row,SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] = 0;
									// gridData[gridData.Row,SO_SaleOrderDetailTable.UNITPRICE_FLD] = 0;
									int intCustomerID = 0;
									try
									{
										intCustomerID = Convert.ToInt32(txtCustomer.Tag);
									}
									catch{}
				
									gridData[gridData.Row,SO_SaleOrderDetailTable.UNITPRICE_FLD] = boSaleOrder.GetUnitPriceDefualt(Convert.ToInt32(drvResult[ITM_ProductTable.PRODUCTID_FLD]), intCustomerID);

									if(chkVAT.Checked)
									{
										gridData[gridData.Row,SO_SaleOrderDetailTable.VATPERCENT_FLD] = voProduct.VAT;
									}
									if(chkExportTax.Checked)
									{
										gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD] = voProduct.ImportTax;
									}
									if(chkSpecialTax.Checked)
									{
										gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD] = voProduct.SpecialTax;
									}

									//fill customer item ref
									if (txtCustomer.Text.Trim() != string.Empty)
									{
										SO_CustomerItemRefDetailVO voCusItemRef = (SO_CustomerItemRefDetailVO) boSaleOrder.GetItemCustomerRef((int)gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD], (int) txtCustomer.Tag);
										if (voCusItemRef != null)
										{
											gridData[gridData.Row,SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD] = voCusItemRef.CustomerItemCode;
											gridData[gridData.Row,SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD] = voCusItemRef.CustomerItemModel;
										}
									}
								}
								else
									gridData.Columns[ITM_ProductTable.CODE_FLD].Value = string.Empty;
							}
						}
						break;
					case ITM_ProductTable.DESCRIPTION_FLD:
						if (strColumnValue == String.Empty)
						{
							gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = String.Empty;
							gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD] = String.Empty;
							//gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = String.Empty;
							gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = String.Empty;
							gridData[gridData.Row,PO_ReturnToVendorDetailTable.STOCKUMID_FLD] = String.Empty;
							gridData[gridData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.UNITPRICE_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.VATAMOUNT_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.VATPERCENT_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] = String.Empty;
							gridData[gridData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = String.Empty;
						}
						else
						{
							if (e.Column.DataColumn.Tag != null)
							{
								DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
								DataRow[] objRows = dstSaleOrderDetail.Tables[0].Select(SO_SaleOrderDetailTable.PRODUCTID_FLD + " = " + drvResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
								if(objRows.Length == 0)
								{
									
									gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = drvResult[ITM_ProductTable.CODE_FLD];
									gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD] = drvResult[ITM_ProductTable.PRODUCTID_FLD];
									gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
									gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = drvResult[ITM_ProductTable.REVISION_FLD];
									gridData[gridData.Row,PO_ReturnToVendorDetailTable.STOCKUMID_FLD] = drvResult[ITM_ProductTable.STOCKUMID_FLD];
									ProductItemInfoBO boProductItemInfo = new ProductItemInfoBO();
									gridData[gridData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = boProductItemInfo.GetCategoryCodeByProductID(int.Parse(drvResult[ITM_ProductTable.PRODUCTID_FLD].ToString()));	
									//SaleOrderBO boOrder = new SaleOrderBO();
									ITM_ProductVO voProduct = (ITM_ProductVO)boSaleOrder.GetProduct(int.Parse(gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD].ToString()));
									gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = voProduct.Code;
									gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = voProduct.Description;
									gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = voProduct.Revision;
									gridData[gridData.Row,SO_SaleOrderDetailTable.SELLINGUMID_FLD] = voProduct.BuyingUMID;
									gridData[gridData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = ((MST_UnitOfMeasureVO)boSaleOrder.GetUnitOfMeasure(voProduct.BuyingUMID)).Code;
									gridData[gridData.Row,SO_SaleOrderDetailTable.STOCKUMID_FLD] = voProduct.StockUMID;
									int intMaxLine = 0;
									if(gridData.Row > 0) intMaxLine = int.Parse(gridData[gridData.Row - 1,SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString());
									gridData[gridData.Row,SO_SaleOrderDetailTable.SALEORDERLINE_FLD] = ++intMaxLine;
									gridData[gridData.Row,SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] = 0;
									//gridData[gridData.Row,SO_SaleOrderDetailTable.UNITPRICE_FLD] = 0;
									int intCustomerID = 0;
									try
									{
										intCustomerID = Convert.ToInt32(txtCustomer.Tag);
									}
									catch{}
				
									gridData[gridData.Row,SO_SaleOrderDetailTable.UNITPRICE_FLD] = boSaleOrder.GetUnitPriceDefualt(Convert.ToInt32(drvResult[ITM_ProductTable.PRODUCTID_FLD]), intCustomerID);

									if(chkVAT.Checked)
									{
										gridData[gridData.Row,SO_SaleOrderDetailTable.VATPERCENT_FLD] = voProduct.VAT;
									}
									if(chkExportTax.Checked)
									{
										gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD] = voProduct.ImportTax;
									}
									if(chkSpecialTax.Checked)
									{
										gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD] = voProduct.SpecialTax;
									}	
								}
								else
									gridData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
							}
						}
						break;
					case ITM_ProductTable.REVISION_FLD:
						if (strColumnValue == String.Empty)
						{
							gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = String.Empty;
							gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD] = String.Empty;
							gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = String.Empty;
							//gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = String.Empty;
							gridData[gridData.Row,PO_ReturnToVendorDetailTable.STOCKUMID_FLD] = String.Empty;
							gridData[gridData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.UNITPRICE_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.VATAMOUNT_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.VATPERCENT_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] = String.Empty;
							gridData[gridData.Row, SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] = String.Empty;
							gridData[gridData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = String.Empty;
						}
						else
						{
							if (e.Column.DataColumn.Tag != null)
							{
								DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
								DataRow[] objRows = dstSaleOrderDetail.Tables[0].Select(SO_SaleOrderDetailTable.PRODUCTID_FLD + " = " + drvResult[ITM_ProductTable.PRODUCTID_FLD].ToString());
								if(objRows.Length == 0)
								{
									
									gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = drvResult[ITM_ProductTable.CODE_FLD];
									gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD] = drvResult[ITM_ProductTable.PRODUCTID_FLD];
									gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = drvResult[ITM_ProductTable.DESCRIPTION_FLD];
									gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = drvResult[ITM_ProductTable.REVISION_FLD];
									gridData[gridData.Row,PO_ReturnToVendorDetailTable.STOCKUMID_FLD] = drvResult[ITM_ProductTable.STOCKUMID_FLD];
									ProductItemInfoBO boProductItemInfo = new ProductItemInfoBO();
									gridData[gridData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = boProductItemInfo.GetCategoryCodeByProductID(int.Parse(drvResult[ITM_ProductTable.PRODUCTID_FLD].ToString()));
									//SaleOrderBO boOrder = new SaleOrderBO();
									ITM_ProductVO voProduct = (ITM_ProductVO)boSaleOrder.GetProduct(int.Parse(gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD].ToString()));
									gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = voProduct.Code;
									gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = voProduct.Description;
									gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = voProduct.Revision;
									gridData[gridData.Row,SO_SaleOrderDetailTable.SELLINGUMID_FLD] = voProduct.SellingUMID;
									gridData[gridData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = ((MST_UnitOfMeasureVO)boSaleOrder.GetUnitOfMeasure(voProduct.BuyingUMID)).Code;
									gridData[gridData.Row,SO_SaleOrderDetailTable.STOCKUMID_FLD] = voProduct.StockUMID;
									int intMaxLine = 0;
									if(gridData.Row > 0) intMaxLine = int.Parse(gridData[gridData.Row - 1,SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString());
									gridData[gridData.Row,SO_SaleOrderDetailTable.SALEORDERLINE_FLD] = ++intMaxLine;
									gridData[gridData.Row,SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] = 0;
									int intCustomerID = 0;
									try
									{
										intCustomerID = Convert.ToInt32(txtCustomer.Tag);
									}
									catch{}
				
									gridData[gridData.Row,SO_SaleOrderDetailTable.UNITPRICE_FLD] = boSaleOrder.GetUnitPriceDefualt(Convert.ToInt32(drvResult[ITM_ProductTable.PRODUCTID_FLD]), intCustomerID);
									if(chkVAT.Checked)
									{
										gridData[gridData.Row,SO_SaleOrderDetailTable.VATPERCENT_FLD] = voProduct.VAT;
									}
									if(chkExportTax.Checked)
									{
										gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD] = voProduct.ImportTax;
									}
									if(chkSpecialTax.Checked)
									{
										gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD] = voProduct.SpecialTax;
									}
								}
								else
									gridData.Columns[ITM_ProductTable.REVISION_FLD].Value = string.Empty;
							}
						}
						break;
					case MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD:
						if (strColumnValue == String.Empty)
						{
							gridData[gridData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = String.Empty;
							gridData[gridData.Row,SO_SaleOrderDetailTable.SELLINGUMID_FLD] = DBNull.Value;
						}
						else
						{
							if (e.Column.DataColumn.Tag != null)
							{
								DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
								gridData[gridData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drvResult[MST_UnitOfMeasureTable.CODE_FLD];
								gridData[gridData.Row,SO_SaleOrderDetailTable.SELLINGUMID_FLD] = drvResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD];
							}
						}
						break;
					case SO_CancelReasonTable.TABLE_NAME + SO_CancelReasonTable.CODE_FLD:
						if (strColumnValue == String.Empty)
						{
							gridData[gridData.Row,SO_CancelReasonTable.TABLE_NAME + SO_CancelReasonTable.CODE_FLD] = String.Empty;
							gridData[gridData.Row,SO_SaleOrderDetailTable.CANCELREASONID_FLD] = DBNull.Value;
						}
						else
						{
							if (e.Column.DataColumn.Tag != null)
							{
								DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
								gridData[gridData.Row,SO_CancelReasonTable.TABLE_NAME + SO_CancelReasonTable.CODE_FLD] = drvResult[SO_CancelReasonTable.CODE_FLD];
								gridData[gridData.Row,SO_SaleOrderDetailTable.CANCELREASONID_FLD] = drvResult[SO_CancelReasonTable.CANCELREASONID_FLD];
							}
						}
						break;
				}


				// no product
				if(gridData[gridData.Row,SO_SaleOrderDetailTable.PRODUCTID_FLD] == DBNull.Value) return;
				// init value
				decimal decBaseAmount, decOrderQuantity, decUnitPrice;
				try
				{
					//if(gridData.Row == 0) return;
					decOrderQuantity = decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString());
					// Check Order quantity
					if ((gridData.Col == dstSaleOrderDetail.Tables[0].Columns.IndexOf(SO_SaleOrderDetailTable.ORDERQUANTITY_FLD)))
					{
//						if(decOrderQuantity <= 0)
//						{
							//decOrderQuantity = -decOrderQuantity;
							//if(decOrderQuantity == 0) decOrderQuantity = ONE;
							gridData[gridData.Row,SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] = decOrderQuantity;
	//					}
					}
					decUnitPrice = decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString());
					// Check unit price
					if ((gridData.Col == dstSaleOrderDetail.Tables[0].Columns.IndexOf(SO_SaleOrderDetailTable.UNITPRICE_FLD)))
					{
//						if(decUnitPrice < 0)
//						{
//							decUnitPrice = -decUnitPrice;
							gridData[gridData.Row,SO_SaleOrderDetailTable.UNITPRICE_FLD] = decUnitPrice;
//						}
					}
					decBaseAmount = decOrderQuantity * decUnitPrice;
				}
				catch
				{
					//decOrderQuantity = ONE;
					decUnitPrice = decBaseAmount = 0;
					//return;
				}
				// col order quantity
			

				// col orderquantity, or unit price was change
				if((gridData.Col == dstSaleOrderDetail.Tables[0].Columns.IndexOf(SO_SaleOrderDetailTable.ORDERQUANTITY_FLD))
					||(gridData.Col == dstSaleOrderDetail.Tables[0].Columns.IndexOf(SO_SaleOrderDetailTable.UNITPRICE_FLD)))
				{
					
					
					try
					{
						gridData[gridData.Row,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] = (decDiscountRate * decBaseAmount)/ONE_HUNDRED;
					}
					catch {}
					CalculateNetAmount(decBaseAmount);
					// sum VAT amount,Imp amount, Spe amount, total amount, net amount column
					CalculateVATAmount(decBaseAmount);
					CalculateExpAmount(decBaseAmount);
					CalculateSpecAmount(decBaseAmount);
					CalculateTotalAmount(decBaseAmount);
					// Sum all for lable
					Calculate();
					return;
				
				}
				// col VAT was change
				if(gridData.Col == dstSaleOrderDetail.Tables[0].Columns.IndexOf(SO_SaleOrderDetailTable.VATPERCENT_FLD))
				{
					// recalculate VATAMount,TotalAmount,Special tax amount column
					CalculateVATAmount(decBaseAmount);
					CalculateSpecAmount(decBaseAmount);
					CalculateTotalAmount(decBaseAmount);
					//CalculateNetAmount();
					// Sum all for lable
					Calculate();
					return;
				
				}
				// VATAMOUNT was change
				if(gridData.Col == dstSaleOrderDetail.Tables[0].Columns.IndexOf(SO_SaleOrderDetailTable.VATAMOUNT_FLD))
				{
					// recalculate TotalAmount,Special Tax amount column
//					CalculateVAT(decAmount);
					CalculateSpecAmount(decBaseAmount);
					CalculateTotalAmount(decBaseAmount);
					
					//CalculateNetAmount();
					// Sum all for lable
					Calculate();
					return;
				
				}
				// %Export was change
				if(gridData.Col == dstSaleOrderDetail.Tables[0].Columns.IndexOf(SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD))
				{
					// recalculate ExportAmount, TotalAmount, Special amount column
					CalculateExpAmount(decBaseAmount);
					CalculateSpecAmount(decBaseAmount);
					CalculateTotalAmount(decBaseAmount);
					//CalculateNetAmount();
					
					// Sum all for lable
					Calculate();
					return;
				
				}
				// Export Amount was change
				if(gridData.Col == dstSaleOrderDetail.Tables[0].Columns.IndexOf(SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD))
				{
					// recalculate TotalAmount, Special Tax amount column
//					CalculateImp(decAmount);
					CalculateSpecAmount(decBaseAmount);
					CalculateTotalAmount(decBaseAmount);
					
					//CalculateNetAmount();
					// Sum all for lable
					Calculate();
					return;
				
				}
				// %Special was change
				if(gridData.Col == dstSaleOrderDetail.Tables[0].Columns.IndexOf(SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD))
				{
					// recalculate Special Amount,TotalAmount column
					CalculateSpecAmount(decBaseAmount);
					CalculateTotalAmount(decBaseAmount);
					//CalculateNetAmount();
					// Sum all for lable
					Calculate();
					return;
				}
				// Special Amount was change
				if(gridData.Col == dstSaleOrderDetail.Tables[0].Columns.IndexOf(SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD))
				{
					// recalculate TotalAmount column
//					CalculateSpec(decAmount);
					CalculateTotalAmount(decBaseAmount);
					//CalculateNetAmount();
					// Sum all for lable
					Calculate();
					return;
				}
				// Discount Amount was change
				if(gridData.Col == dstSaleOrderDetail.Tables[0].Columns.IndexOf(SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD))
				{
					// recalculate net amount column
					CalculateNetAmount(decBaseAmount);
					// sum VAT amount,Imp amount, Spe amount, total amount, net amount column
					CalculateVATAmount(decBaseAmount);
					CalculateExpAmount(decBaseAmount);
					CalculateSpecAmount(decBaseAmount);
					CalculateTotalAmount(decBaseAmount);

					
					// Sum all for lable
					Calculate();
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

		private void CalculateVATAmount(decimal pdecAmount)
		{
			try
			{
				if(!chkVAT.Checked) return;
				if(gridData[gridData.Row,SO_SaleOrderDetailTable.VATPERCENT_FLD] != DBNull.Value) 
				{
					if(gridData[gridData.Row,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] == DBNull.Value)
					{
						gridData[gridData.Row,SO_SaleOrderDetailTable.VATAMOUNT_FLD] =  (pdecAmount
						* decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.VATPERCENT_FLD].ToString()))/ONE_HUNDRED;
					}
					else
					{
						gridData[gridData.Row,SO_SaleOrderDetailTable.VATAMOUNT_FLD] =  ((pdecAmount - decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString()))
							* decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.VATPERCENT_FLD].ToString()))/ONE_HUNDRED;
					}
				}
				else
				{
					gridData[gridData.Row,SO_SaleOrderDetailTable.VATAMOUNT_FLD] = 0;
				}
			}
			catch
			{
				gridData[gridData.Row,SO_SaleOrderDetailTable.VATAMOUNT_FLD] = 0;
			}
		}
		private void CalculateExpAmount(decimal pdecAmount)
		{
			try
			{
				if(!chkExportTax.Checked) return;
				if(gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD] != DBNull.Value)
				{
					if(gridData[gridData.Row,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] == DBNull.Value)
					{
						gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] =  (pdecAmount
							* decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD].ToString()))/ONE_HUNDRED;
					}
					else
					{
						gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] =  ((pdecAmount - decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString()))
							* decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD].ToString()))/ONE_HUNDRED;
					}				
				}
				else
				{
					gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] = 0;
				}
			}
			catch
			{
				//dstPODetail.Tables[0].Rows[gridData.Row][SO_SaleOrderDetailTable.IMPORTTAXAMOUNT_FLD] = 0;
				
			}
		}
		private void CalculateSpecAmount(decimal pdecAmount)
		{
			try
			{
				if(!chkSpecialTax.Checked) return;
				decimal decVatAmout, decExpAmount;
				if(gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD] != DBNull.Value)
				{
					if(gridData[gridData.Row,SO_SaleOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value)
					{
						decVatAmout = decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.VATAMOUNT_FLD].ToString());
					}
					else 
						decVatAmout = 0;
					if(gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] != DBNull.Value)
					{
						decExpAmount = decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].ToString());
					}
					else 
						decExpAmount = 0;
                    if(gridData[gridData.Row,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] == DBNull.Value)
					{
						gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] =  (pdecAmount + decVatAmout + decExpAmount)
							* decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD].ToString())/ONE_HUNDRED;
					}
					else
					{
						gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] =  ((pdecAmount - decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString())) + decVatAmout + decExpAmount)
							* decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD].ToString())/ONE_HUNDRED;
					}				
				}
				else
				{
					gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] = 0;
				}
			}
			catch
			{
				//dstPODetail.Tables[0].Rows[gridData.Row][SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] = 0;
				
			}
		}
		private void CalculateTotalAmount(decimal pdecAmount)
		{
			try
			{
				decimal decVatAmount,decExpAmount,decSpecAmount;
				if(gridData[gridData.Row,SO_SaleOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value) 
					decVatAmount = decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.VATAMOUNT_FLD].ToString());
				else decVatAmount = 0;
				if(gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] != DBNull.Value)
					decExpAmount = decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].ToString());
				else decExpAmount = 0;
				if(gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] != DBNull.Value)
					decSpecAmount = decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD].ToString());
				else decSpecAmount = 0;
				if(gridData[gridData.Row,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] == DBNull.Value)
				{
//					gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] =  (pdecAmount + decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.VATAMOUNT_FLD].ToString()) + decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].ToString()))
//						* decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD].ToString())/ONE_HUNDRED;
					gridData[gridData.Row,SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] =  
						pdecAmount + decVatAmount + decExpAmount + decSpecAmount;
				}
				else
				{
//					gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] =  ((pdecAmount - decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString())) + decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.VATAMOUNT_FLD].ToString()) + decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].ToString()))
//						* decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD].ToString())/ONE_HUNDRED;\
					gridData[gridData.Row,SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] =  
						(pdecAmount - decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString())) + decVatAmount + decExpAmount + decSpecAmount;
				}		
				
			}
			catch
			{
				//dstPODetail.Tables[0].Rows[gridData.Row][SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] = 0;
			}
		}

		private void CalculateNetAmount(decimal pdecAmount)
		{
			try
			{
				decimal decDiscountAmount = 0;
				if(gridData[gridData.Row,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
					decDiscountAmount = decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());
				gridData[gridData.Row,SO_SaleOrderDetailTable.NETAMOUNT_FLD] = pdecAmount - decDiscountAmount;
					//decimal.Parse(gridData[gridData.Row,SO_SaleOrderDetailTable.TOTALAMOUNT_FLD].ToString()) - decDiscountAmount;
					
			}
			catch
			{
				//				dstPODetail.Tables[0].Rows[gridData.Row][SO_SaleOrderDetailTable.NETAMOUNT_FLD] = 
				//					gridData[gridData.Row,SO_SaleOrderDetailTable.TOTALAMOUNT_FLD];
			}
		}

		private void btnCurrency_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCurrency_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME, MST_CurrencyTable.CODE_FLD, txtCurrency.Text.Trim(), null, true);
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
		private DataRow GetDataRow(string pstrListFields,string pstrValue,string pstrTable,string pstrField,string pstrCodition)
		{
			//SaleOrderBO boOrder = new SaleOrderBO();
			return boSaleOrder.GetDataRow(pstrListFields,pstrValue,pstrTable,pstrField,pstrCodition);
		}

		private void btnBuyLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnBuyLoc_Click()";
			try
			{
				
				htbCriteria = new Hashtable();
				if(txtCustomer.Text != string.Empty)
				{
					if(txtCustomer.Tag != null)
					{
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtCustomer.Tag.ToString());
					}
				}
				else
				{
					strParam[0]	= lblCustomer.Text;
					strParam[1] = lblBuyLoc.Text;
					// You have to select Customer before select Buy Location, please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtBuyLoc.Text = string.Empty;
					txtCustomer.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtBuyLoc.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtBuyLoc.Tag = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtBuyLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
				}
				/*
				
				htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtCustomer.Tag.ToString());
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtBuyLoc.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtBuyLoc.Tag = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtBuyLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
				}
				*/
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
				htbCriteria = new Hashtable();
				if(txtCustomer.Text != string.Empty)
				{
					if(txtCustomer.Tag != null)
					{
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtCustomer.Tag.ToString());
					}
				}
				else
				{
					strParam[1] = lblContact.Text;
					// You have to select Vendor before , please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtContact.Text = string.Empty;
					txtCustomer.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyContactTable.TABLE_NAME, MST_PartyContactTable.CODE_FLD, txtContact.Text.Trim(), htbCriteria, true);
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

		private void btnShipToLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnShipToLoc_Click()";
			try
			{
				htbCriteria = new Hashtable();
				if(txtCustomer.Text != string.Empty)
				{
					if(txtCustomer.Tag != null)
					{
						//strWhere = " WHERE " + MST_PartyContactTable.PARTYID_FLD + " = " + txtCustomer.Tag.ToString();
						htbCriteria.Add(MST_PartyLocationTable.PARTYID_FLD, txtCustomer.Tag.ToString());
					}
				}
				else
				{
					strParam[0] = lblCustomer.Text;
					strParam[1] = lblShipToLoc.Text;
					// You have to select Customer before select Buy Location, please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtShipToLoc.Text = string.Empty;
					txtCustomer.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtShipToLoc.Text.Trim(), htbCriteria, true);
				if(drwResult != null)
				{
					txtShipToLoc.Tag = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtShipToLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
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

		private void btnBillToLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnShipToLoc_Click()";
			try
			{
				//string strWhere = string.Empty;
				htbCriteria = new Hashtable();
				if(txtCustomer.Text != string.Empty)
				{
					if(txtCustomer.Tag != null)
					{
						//strWhere = " WHERE " + MST_PartyContactTable.PARTYID_FLD + " = " + txtCustomer.Tag.ToString();
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtCustomer.Tag.ToString());
					}
				}
				else
				{
					strParam[1] = lblBillToLoc.Text;
					// You have to select Customer before select Buy Location, please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtBillToLoc.Text = string.Empty;
					txtCustomer.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtBillToLoc.Text.Trim(), htbCriteria, true);
				if(drwResult != null)
				{
					txtBillToLoc.Tag = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtBillToLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
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

		private void btnShipFromLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnShipFromLoc_Click()";
			try
			{
				//string strCondition = string.Empty;
				//htbCriteria = new Hashtable();
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtShipFromLoc.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtShipFromLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
					txtShipFromLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
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

		private void btnSalesRepres_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSalesRepres_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(MST_EmployeeTable.TABLE_NAME, MST_EmployeeTable.CODE_FLD, txtSalesRepres.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtSalesRepres.Tag = drwResult[MST_EmployeeTable.EMPLOYEEID_FLD];
					txtSalesRepres.Text = drwResult[MST_EmployeeTable.CODE_FLD].ToString();
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

		private void btnSalesType_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSalesType_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(SO_SaleTypeTable.TABLE_NAME,SO_SaleTypeTable.CODE_FLD, txtSalesType.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtSalesType.Tag = drwResult[SO_SaleTypeTable.SALETYPEID_FLD];
					txtSalesType.Text = drwResult[SO_SaleTypeTable.CODE_FLD].ToString();
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

		private void btnDiscTerms_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDiscTerms_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(MST_DiscountTermTable.TABLE_NAME, MST_DiscountTermTable.CODE_FLD, txtDiscTerms.Text.Trim(), null, true);
				if(drwResult != null)
				{
					decDiscountRate = decimal.Parse(drwResult[MST_DiscountTermTable.RATE_FLD].ToString());
					txtDiscTerms.Text = drwResult[MST_DiscountTermTable.CODE_FLD].ToString();
					txtDiscTerms.Tag = drwResult[MST_DiscountTermTable.DISCOUNTTERMID_FLD];
                    int intRowCount = gridData.RowCount;
					
					// update grid
					for(int i = 0; i < intRowCount; i++)
					{
						try
						{
							gridData[i,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] = decDiscountRate
								*decimal.Parse(gridData[i,SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString())
								*decimal.Parse(gridData[i,SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString())/ONE_HUNDRED;
						}
						catch
						{
							gridData[i,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] = 0;
						}
					}
					ReCalculate();
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

		private void btnDeliveryTerms_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDeliveryTerms_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(MST_DeliveryTermTable.TABLE_NAME, MST_DeliveryTermTable.CODE_FLD, txtDeliveryTerms.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtDeliveryTerms.Tag = drwResult[MST_DeliveryTermTable.DELIVERYTERMID_FLD];
					txtDeliveryTerms.Text = drwResult[MST_DeliveryTermTable.CODE_FLD].ToString();
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

		private void btnPayTerms_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPayTerms_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(MST_PaymentTermTable.TABLE_NAME, MST_PaymentTermTable.CODE_FLD, txtPayTerms.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtPayTerms.Tag = drwResult[MST_PaymentTermTable.PAYMENTTERMID_FLD];
					txtPayTerms.Text = drwResult[MST_PaymentTermTable.CODE_FLD].ToString();
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

		private void btnCarrier_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCarrier_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(MST_CarrierTable.TABLE_NAME, MST_CarrierTable.CODE_FLD, txtCarrier.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtCarrier.Tag = drwResult[MST_CarrierTable.CARRIERID_FLD];
					txtCarrier.Text = drwResult[MST_CarrierTable.CODE_FLD].ToString();
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

		private void btnPause_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPause_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(MST_PauseTable.TABLE_NAME, MST_PauseTable.CODE_FLD, txtPause.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtPause.Tag = drwResult[MST_PauseTable.PAUSEID_FLD];
					txtPause.Text = drwResult[MST_PauseTable.CODE_FLD].ToString();
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

		private void LoadMaster(int pintID)
		{
			const string CURRENCY_CODE = "CURRENCY_CODE";
			const string DELIVERYTERM_CODE = "DELIVERYTERM_CODE";
			const string PAYMENTTERM_CODE = "PAYMENTTERM_CODE";
			const string CARRIER_CODE = "CARRIER_CODE";
			const string EMPLOYEE_CODE = "EMPLOYEE_CODE";
			const string PARTY_CODE = "PARTY_CODE";
			const string PARTY_NAME = "PARTY_NAME";
			const string SHIPTOLOC_CODE = "SHIPTOLOC_CODE";
			const string BUYINGLOC_CODE = "BuyingLoc_Code";
			const string PARTYCONTACT_CODE = "PARTYCONTACT_CODE";
			const string SALETYPE_CODE = "SALETYPE_CODE";
			const string DISCOUNTTERM_CODE = "DISCOUNTTERM_CODE";
			const string PAUSE_CODE = "PAUSE_CODE";
			const string SHIPFROMLOC_CODE = "SHIPFROMLOC_CODE";
			const string BILLTOLOC_CODE = "BILLTOLOC_CODE";

			const string METHOD_NAME = THIS + ".LoadMaster()";
			try
			{
				//SaleOrderBO boSO = new SaleOrderBO();
				DataRow drowMaster = boSaleOrder.LoadObjectVO(pintID);
				txtCurrency.Text = drowMaster[CURRENCY_CODE].ToString();
				txtCustomer.Text = drowMaster[PARTY_CODE].ToString();
				txtCustomerName.Text = drowMaster[PARTY_NAME].ToString();
				// txtContact.Text = drowMaster[VENDORLOC_CODE].ToString();
				txtContact.Text = drowMaster[PARTYCONTACT_CODE].ToString();
				txtBuyLoc.Text = drowMaster[BUYINGLOC_CODE].ToString();
				txtSalesRepres.Text = drowMaster[EMPLOYEE_CODE].ToString();
				txtShipToLoc.Text = drowMaster[SHIPTOLOC_CODE].ToString();
				txtShipFromLoc.Text = drowMaster[SHIPFROMLOC_CODE].ToString();
				txtShipFromLoc.Tag = drowMaster[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD];
				txtBillToLoc.Text = drowMaster[BILLTOLOC_CODE].ToString();
				txtSalesType.Text = drowMaster[SALETYPE_CODE].ToString();
				txtDiscTerms.Text = drowMaster[DISCOUNTTERM_CODE].ToString();
				txtDeliveryTerms.Text = drowMaster[DELIVERYTERM_CODE].ToString();
				txtPayTerms.Text = drowMaster[PAYMENTTERM_CODE].ToString();
				txtCarrier.Text = drowMaster[CARRIER_CODE].ToString();
				txtPause.Text = drowMaster[PAUSE_CODE].ToString();
				txtExchRate.Value = drowMaster[SO_SaleOrderMasterTable.EXCHANGERATE_FLD];
				txtGateType.Text = drowMaster["GateType"].ToString();

				voSaleOrderMaster = new SO_SaleOrderMasterVO();
				voSaleOrderMaster.SaleOrderMasterID = pintID;
	
				voSaleOrderMaster.PartyID = (drowMaster[SO_SaleOrderMasterTable.PARTYID_FLD] == DBNull.Value) ? 0 : int.Parse(drowMaster[SO_SaleOrderMasterTable.PARTYID_FLD].ToString());
				voSaleOrderMaster.Code = drowMaster[SO_SaleOrderMasterTable.CODE_FLD].ToString();
				if(drowMaster[SO_SaleOrderMasterTable.EXCHANGERATE_FLD] != DBNull.Value)
					voSaleOrderMaster.ExchangeRate = decimal.Parse(drowMaster[SO_SaleOrderMasterTable.EXCHANGERATE_FLD].ToString());
				voSaleOrderMaster.CustomerPurchaseOrderNo = drowMaster[SO_SaleOrderMasterTable.CUSTOMERPURCHASEORDERNO_FLD].ToString();
				if (drowMaster[SO_SaleOrderMasterTable.SPECIALTAX_FLD] != DBNull.Value)
					voSaleOrderMaster.SpecialTax = bool.Parse(drowMaster[SO_SaleOrderMasterTable.SPECIALTAX_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.EXPORTTAX_FLD] != DBNull.Value)
					voSaleOrderMaster.ExportTax = bool.Parse(drowMaster[SO_SaleOrderMasterTable.EXPORTTAX_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.EXPORTTAX_FLD] != DBNull.Value)
					voSaleOrderMaster.VAT = bool.Parse(drowMaster[SO_SaleOrderMasterTable.EXPORTTAX_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD] != DBNull.Value)
					voSaleOrderMaster.ShipCompleted = bool.Parse(drowMaster[SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.BILLTOLOCID_FLD] != DBNull.Value)
					voSaleOrderMaster.BillToLocID = int.Parse(drowMaster[SO_SaleOrderMasterTable.BILLTOLOCID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD] != DBNull.Value)
					voSaleOrderMaster.ShipToLocID = int.Parse(drowMaster[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD] != DBNull.Value)
					voSaleOrderMaster.ShipFromLocID = int.Parse(drowMaster[SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.CURRENCYID_FLD] != DBNull.Value)
					voSaleOrderMaster.CurrencyID = int.Parse(drowMaster[SO_SaleOrderMasterTable.CURRENCYID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD] != DBNull.Value)
					voSaleOrderMaster.PartyContactID = int.Parse(drowMaster[SO_SaleOrderMasterTable.PARTYCONTACTID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD] != DBNull.Value)
					voSaleOrderMaster.DeliveryTermsID = int.Parse(drowMaster[SO_SaleOrderMasterTable.DELIVERYTERMSID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD] != DBNull.Value)
					voSaleOrderMaster.PaymentTermsID = int.Parse(drowMaster[SO_SaleOrderMasterTable.PAYMENTTERMSID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.CARRIERID_FLD] != DBNull.Value)
					voSaleOrderMaster.CarrierID = int.Parse(drowMaster[SO_SaleOrderMasterTable.CARRIERID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.PAUSEID_FLD] != DBNull.Value)
					voSaleOrderMaster.PauseID = int.Parse(drowMaster[SO_SaleOrderMasterTable.PAUSEID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD] != DBNull.Value)
					voSaleOrderMaster.DiscountTermsID = int.Parse(drowMaster[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.SALETYPEID_FLD] != DBNull.Value)
					voSaleOrderMaster.SaleTypeID = int.Parse(drowMaster[SO_SaleOrderMasterTable.SALETYPEID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.BUYINGLOCID_FLD] != DBNull.Value)
					voSaleOrderMaster.BuyingLocID = int.Parse(drowMaster[SO_SaleOrderMasterTable.BUYINGLOCID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD] != DBNull.Value)
					voSaleOrderMaster.ShipToLocID = int.Parse(drowMaster[SO_SaleOrderMasterTable.SHIPTOLOCID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD] != DBNull.Value)
					voSaleOrderMaster.SalesRepresentativeID = int.Parse(drowMaster[SO_SaleOrderMasterTable.SALESREPRESENTATIVEID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.CCNID_FLD] != DBNull.Value)
					voSaleOrderMaster.CCNID = int.Parse(drowMaster[SO_SaleOrderMasterTable.CCNID_FLD].ToString());
				if (drowMaster[SO_SaleOrderMasterTable.PRIORITY_FLD] != DBNull.Value)
					voSaleOrderMaster.Priority = decimal.ToInt32((decimal)(drowMaster[SO_SaleOrderMasterTable.PRIORITY_FLD]));
				if (drowMaster[SO_SaleOrderMasterTable.TRANSDATE_FLD] != DBNull.Value)
					voSaleOrderMaster.TransDate = (DateTime)drowMaster[SO_SaleOrderMasterTable.TRANSDATE_FLD];
				if (drowMaster[SO_SaleOrderMasterTable.TYPEID_FLD] != DBNull.Value)
					voSaleOrderMaster.TypeID = Convert.ToInt32(drowMaster[SO_SaleOrderMasterTable.TYPEID_FLD]);
				// If selected discount terms
				if (drowMaster[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD] != DBNull.Value)
				{
					DataRow drolResult = GetDataRow(MST_DiscountTermTable.DISCOUNTTERMID_FLD + "," + MST_DiscountTermTable.CODE_FLD + "," + MST_DiscountTermTable.RATE_FLD,
						drowMaster[SO_SaleOrderMasterTable.DISCOUNTTERMSID_FLD].ToString(), 
						MST_DiscountTermTable.TABLE_NAME,MST_DiscountTermTable.DISCOUNTTERMID_FLD,string.Empty);
					decDiscountRate = decimal.Parse(drolResult[MST_DiscountTermTable.RATE_FLD].ToString());
				}
				else decDiscountRate = 0;
				VOToControls(voSaleOrderMaster);

				SetEnableButtons();
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

		private void txtBuyLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnBuyLoc.Enabled)
					btnBuyLoc_Click(sender,e);
	
		}

		private void txtCurrency_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnCurrency.Enabled)
					btnCurrency_Click(sender,e);

		}

		private void txtContact_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnContact.Enabled)
					btnContact_Click(sender,e);

		}

		private void txtShipToLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnShipToLoc.Enabled)
					btnShipToLoc_Click(sender,e);

		}

		private void txtBillToLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnBillToLoc.Enabled)
					btnBillToLoc_Click(sender,e);

		}

		private void txtShipFromLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnShipFromLoc.Enabled)
					btnShipFromLoc_Click(sender,e);

		}

		private void txtSalesRepres_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnSalesRepres.Enabled)
					btnSalesRepres_Click(sender,e);

		}

		private void txtSalesType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnSalesType.Enabled)
					btnSalesType_Click(sender,e);

		}

		private void txtDiscTerms_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnDiscTerms.Enabled)
					btnDiscTerms_Click(sender,e);

		}
		private void txtDeliveryTerms_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnDeliveryTerms.Enabled)
					btnDeliveryTerms_Click(sender,e);

		}

		private void txtPayTerms_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnPayTerms.Enabled)
					btnPayTerms_Click(sender,e);

		}

		private void txtCarrier_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnCarrier.Enabled)
					btnCarrier_Click(sender,e);

		}

		private void txtPause_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnPause.Enabled)
					btnPause_Click(sender,e);

		}


		private void cboTransDate_DropDownClosed(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboTransDate_DropDownClosed()";
			try
			{
				if (cboTransDate.Text.Trim() == string.Empty)
				{
					cboTransDate.Value = null;
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

		private void btnImport_Click(object sender, System.EventArgs e)
		{
			Point ptShow = btnImport.Location;
			ptShow.X = btnImport.Right;
			ptShow.Y = btnImport.Bottom;
			ctxmnuImport.Show(this,ptShow);
		}
		
		#region Import functions

		const int INT_MINIMUM_ROW = 4;
		const int INT_MINIMUM_COL = 8;

		const int INT_TITLE_ROW = 0;
		const int INT_HEADER_ROW = 1;
		const int INT_BEGIN_DATA_ROW = 3;

		const int INT_TITLE_COL = 0;
		const int INT_PARTS_NO_COL = 1;
		const int INT_TIME_COL = 4;
		const int INT_CODE_COL = 5;
		const int INT_BEGIN_DATA_COL = 6;

		const string STR_IMPORT_TASK = "Import";

		//**************************************************************************              
		///    <Description>
		///       This method uses to 
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
		///       DuongNA
		///    </Authors>
		///    <History>
		///       20-10-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private DataTable ReadImportData(string strFilename)
		{
			const string CONN_STR_TEMPL = "Provider=Microsoft.Jet.OLEDB.4.0;"
										+ "Data Source = {0};"
										+ "Extended Properties=\"Excel 8.0;Imex=2;HDR=No\"";
			const string CMD_STR_TEMPL  = "Select * FROM [{0}$]"; //
			const int MAIN_SHEET_IDX = 0;
			const string SHEET_NAME_IDX = "TABLE_NAME";
			const string DOLLAR = "$";
			
			//const string METHOD_NAME = THIS + ".cboTransDate_DropDownClosed()";
			try
			{
				try
				{
					strNewFile = CorrectExcelFile(strFilename);
				}
				catch
				{
					strNewFile = strFilename;
				}
				//string strConnStr = string.Format(CONN_STR_TEMPL,strFilename);
				string strConnStr = string.Format(CONN_STR_TEMPL,strNewFile);
				DataTable dtSales = new DataTable();

				oconExcelFile.ConnectionString = strConnStr;
				try
				{
					oconExcelFile.Open();
				}
				catch
				{
					return null;
				}
				
				//Get sheetnames
				DataTable dtSChema = oconExcelFile.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				
				if (dtSChema.Rows.Count <= MAIN_SHEET_IDX)
				{
					return null;
				}
				string strSheetName = dtSChema.Rows[MAIN_SHEET_IDX][SHEET_NAME_IDX].ToString();
				if (strSheetName.EndsWith(DOLLAR))
				{
					strSheetName = strSheetName.Substring(0,strSheetName.Length - 1);
				}
				string strCmdSelect = string.Format(CMD_STR_TEMPL,strSheetName);
				ocmdExcelSelect.CommandText = strCmdSelect;

				//Fill table
				OleDbDataAdapter oleAdapter = new OleDbDataAdapter();   
				oleAdapter.SelectCommand = ocmdExcelSelect;
				oleAdapter.FillSchema(dtSales,SchemaType.Source);  
				oleAdapter.Fill(dtSales);

				return dtSales;
			}
			catch
			{
				//throw ex;
				return null;
			}
			finally
			{
				if (oconExcelFile.State != ConnectionState.Closed)
				{
					oconExcelFile.Close();
					if(strNewFile != strFilename)
					{
						System.IO.File.Delete(strNewFile);
					}
				}
			}
		}

		private int CheckImportData(DataTable dtImpData)
		{			
			const string METHOD_NAME = THIS + ".CheckImportData()";

			const int INT_MAX_MONTH = 12;
			try
			{
				//First, check minumum table size
				if ((dtImpData.Rows.Count < INT_MINIMUM_ROW) || (dtImpData.Columns.Count < INT_MINIMUM_COL))
				{
					return INT_TITLE_ROW;
				}

				//Second, check title to get month
				string strMMYYYY = dtImpData.Rows[INT_TITLE_ROW][INT_TITLE_COL].ToString();
				int intMonth = 0, intYear = 0;
				if (strMMYYYY.Equals(string.Empty))
				{
					return INT_TITLE_ROW;
				}
				else
				{
					const string DATE_SLASH = "/";
					try
					{
						intMonth = int.Parse(strMMYYYY.Substring(0,strMMYYYY.IndexOf(DATE_SLASH)));
						if (intMonth > INT_MAX_MONTH)
						{							
							return INT_TITLE_ROW;
						}
						intYear = int.Parse(strMMYYYY.Substring(strMMYYYY.IndexOf(DATE_SLASH)+DATE_SLASH.Length));
					}
					catch
					{	
						return INT_TITLE_ROW;
					}
				}

				//Third, check if the days sequence is in a month and consequently
				int intLastDay = 0;
				int intCurrentDay;
				for (int i = INT_BEGIN_DATA_COL; i < dtImpData.Columns.Count - 1; i++)
				{
					try
					{
						intCurrentDay = int.Parse(dtImpData.Rows[INT_HEADER_ROW][i].ToString());
						if (intCurrentDay <= intLastDay)
						{
							return INT_HEADER_ROW;
						}
						else
						{
							intLastDay = intCurrentDay;
						}
					}
					catch
					{
						return INT_HEADER_ROW;
					}
				}
				
				//Forth, check if day in month is valid
				if (DateTime.DaysInMonth(intYear,intMonth) < intLastDay)
				{
					return (INT_HEADER_ROW);
				}

				int intCount = dtImpData.Rows.Count;
				int intTotalCol = dtImpData.Columns.Count - 1;
				//Fifth, check time column & Total column
                for (int i = INT_BEGIN_DATA_ROW; i < intCount; i++)
                {
                	try
                	{
                		int.Parse(dtImpData.Rows[i][INT_TIME_COL].ToString());
						int.Parse(dtImpData.Rows[i][intTotalCol].ToString());
                	}
					catch
					{
						return i;
					}
                }

				return -1;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconExcelFile.State != ConnectionState.Closed)
				{
					oconExcelFile.Close();
				}
			}
		}


		#endregion

		private void mnuImportNew_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".mnuImportNew_Click()";
			this.Cursor = Cursors.WaitCursor;
			try
			{
				#region //Check madatory
				if(CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					cboCCN.Focus();
					return;
				}
				if(CheckMandatory(cboTransDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					cboTransDate.Focus();
					return;
				}

				#region no need to check period

//				if(!FormControlComponents.CheckDateInCurrentPeriod((DateTime)cboTransDate.Value))
//				{
//					//PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE,MessageBoxIcon.Exclamation);
//					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD,MessageBoxIcon.Exclamation);
//				
//					cboTransDate.Focus();
//					return;
//				}

				#endregion

				if(CheckMandatory(txtCurrency))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtCurrency.Focus();
					return;
				}			
				if(CheckMandatory(txtOrderNo))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtOrderNo.Focus();
					return;
				}
				if(CheckMandatory(txtExchRate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtExchRate.Focus();
					return;
				}
				if (Decimal.Parse(txtExchRate.Value.ToString()) <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtExchRate.Focus();
					return;
				}
				if(CheckMandatory(txtCustomer))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtCustomer.Focus();
					return;
				}
				if(IsNotExistCustomer())
				{
					// This customer does not exist in database
					PCSMessageBox.Show(ErrorCode.MESSAGE_CUSTOMER_DOES_NOT_EXIST,MessageBoxIcon.Exclamation);
					btnCustomer.Focus();
					return;
				}
				if(CheckMandatory(txtBuyLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtBuyLoc.Focus();
					return;
				}
				if(CheckMandatory(txtShipToLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtShipToLoc.Focus();
					return;
				}
				if(CheckMandatory(txtBillToLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtBillToLoc.Focus();
					return;
				}
				if(CheckMandatory(txtShipFromLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtShipFromLoc.Focus();
					return;
				}
				if(CheckMandatory(txtGateType))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtGateType.Focus();
					return;
				}
				#endregion

				//Clear all manual data before import
				dstSaleOrderDetail.Tables[0].Clear();

				//Do Import
				if (dlgOpenImpFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					DataTable dt = ReadImportData(dlgOpenImpFile.FileName);
					if (dt == null)
					{
						//Invalid file
						PCSMessageBox.Show(ErrorCode.MESSAGE_IMPORT_ERROR_READ_FILE,MessageBoxIcon.Error);
						return;
					}
					else {
						int intCheck = CheckImportData(dt);
						if (intCheck != -1)
						{
							//gridData.Splits[0].Rows.Clear();
							dstSaleOrderDetail.Clear();
							PCSMessageBox.Show(ErrorCode.MESSAGE_IMPORT_ERROR_FILE_FORMAT,MessageBoxIcon.Error);
							HighlightExcelFile(intCheck,dt.Columns.Count,dlgOpenImpFile.FileName);
							return;					
						}
					}

					int intPartyID = int.Parse(txtCustomer.Tag.ToString());
					int intCCNID = int.Parse(cboCCN.SelectedValue.ToString());

					//Get data
					int intMaxLine = 0;
					if(gridData.RowCount > 0)
					{
						try
						{
							intMaxLine = int.Parse(gridData[gridData.RowCount-1,SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString());
						}
						catch
						{
							intMaxLine = 0;
						}
						dstSaleOrderDetail.Tables[0].DefaultView.Sort = SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD;
						dstSaleOrderDetail.Tables[0].DefaultView.Sort = string.Empty;
					}
					else
					{
						intMaxLine = 0;
					}

					DataSet dstMappingData = dstSaleOrderDetail;
					blnIsChangedGrid = false;
					
					//get gate data
					DataSet dstGateData = boSaleOrder.ListGate();

					int intErrorLine = boSaleOrder.ImportNewMappingData(dt,intPartyID,intCCNID, intMaxLine, dstMappingData, dstGateData);
					if (intErrorLine == 0)
					{
						ReCalculate();
						StoreDatabaseAfterImport(dt,dlgOpenImpFile.FileName,dstGateData);

						mFormAction = EnumAction.Default;
						btnSave.Enabled = false;
						SetEnableButtons();
						SetEditableControl(false);
						string[] arrParam = {STR_IMPORT_TASK};
						PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED,MessageBoxIcon.Information,arrParam);
						blnHasError = false;
						//blnIsChangedGrid = true;
					}
					else
					{
						//Cannot map data or gate not found
						//gridData.Splits[0].Rows.Clear();
						dstSaleOrderDetail.Clear();
						if (intErrorLine > 0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_IMPORT_ERROR_MAP_ITEM, MessageBoxIcon.Error);
						}
						else
						{
							string[] arrParams = {lblGate.Text};
							PCSMessageBox.Show(ErrorCode.NOT_FOUND, MessageBoxIcon.Error, arrParams);
						}
						HighlightExcelFile(Math.Abs(intErrorLine),dt.Columns.Count,dlgOpenImpFile.FileName);
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private int StoreDatabaseAfterImport(DataTable dtImportData, string strFilename, DataSet pdstGateData)
		{
			//const string METHOD_NAME = THIS + ".StoreDatabaseAfterImport()";
			int intRet = 0;
			int intErrorLine = -1;
			ControlsToVO();
			//SaleOrderBO boSaleOrder = new SaleOrderBO();
			if(mFormAction == EnumAction.Add)
			{
				intRet = voSaleOrderMaster.SaleOrderMasterID = boSaleOrder.ImportNewSaleOrder(voSaleOrderMaster,dstSaleOrderDetail,dtImportData);
				UpdateSchedule(intRet,dstSaleOrderDetail,dtImportData,pdstGateData);
				InitGrid(voSaleOrderMaster.SaleOrderMasterID);
			}
			else if(mFormAction == EnumAction.Edit)
			{
				try
				{
					//cache delivery schedule data
					DataSet dstDelSchData = boSaleOrder.GetAllDeliveryLine(voSaleOrderMaster.SaleOrderMasterID);
					boSaleOrder.ImportUpdateSaleOrder(voSaleOrderMaster,dstSaleOrderDetail,dtImportData, dstDelSchData, ref intErrorLine);
					UpdateScheduleForImportUpdate(voSaleOrderMaster.SaleOrderMasterID, dstSaleOrderDetail, dtImportData, dstDelSchData, pdstGateData, ref intErrorLine);
				}
				catch (Exception ex)
				{
					HighlightExcelFile(intErrorLine,dtImportData.Columns.Count,dlgOpenImpFile.FileName);
					throw ex;
				}
				InitGrid(voSaleOrderMaster.SaleOrderMasterID);
			}
			Security.UpdateUserNameModifyTransaction(this,SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD,voSaleOrderMaster.SaleOrderMasterID);
			return intRet;
		}

		public void HighlightExcelFile(int intRow, int intMaxCol, string strFilename)
		{
			const int RED_COLOR = 3;

			Microsoft.Office.Interop.Excel.Application objExcelApp = new Microsoft.Office.Interop.Excel.Application();
			Microsoft.Office.Interop.Excel.Workbook objWorkBook = objExcelApp.Workbooks.Open(strFilename,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
			try
			{
				#region HACK: DEL Trada 27-10-2005

				Microsoft.Office.Interop.Excel.Worksheet objSheet = (Microsoft.Office.Interop.Excel.Worksheet)objWorkBook.Worksheets.get_Item(1);
				Microsoft.Office.Interop.Excel.Range objRange = objSheet.get_Range(objSheet.Cells[intRow+1,1],objSheet.Cells[intRow+1,intMaxCol]);
				objRange.Interior.ColorIndex = RED_COLOR;
				
				objExcelApp.Visible = true;
				//objExcelApp.AlertBeforeOverwriting = false;
				//objExcelApp.DisplayAlerts = false;
				//objExcelApp.ScreenUpdating = false;

				//objWorkBook.Save();

				#endregion 
				
			}
			catch 
			{
				
			}
			finally
			{
				//objWorkBook.Close(Type.Missing,Type.Missing,Type.Missing);
				//objExcelApp.Quit();
			}
		}
		
		private string CorrectExcelFile(string pstrFileName)
		{
			const int START_ROW = 2;
			const int MAX_COL = 50;
			const int PARTNO_COL = 2;
			const int PARTNAME_COL = 3;
			const int START_COL_DATE = 7;
			const string DATE_TIME = "yyyyMMdd HHmmss";
			const string XLS = ".xls";
			const int INT_MAX_MONTH = 12;
			const string BLANK = " ";

			Microsoft.Office.Interop.Excel.Application objExcelApp = new Microsoft.Office.Interop.Excel.Application();
			Microsoft.Office.Interop.Excel.Workbook objWorkBook = objExcelApp.Workbooks.Open(pstrFileName,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
			Microsoft.Office.Interop.Excel.Worksheet objSheet = (Microsoft.Office.Interop.Excel.Worksheet)objWorkBook.Worksheets.get_Item(1);
			//Microsoft.Office.Interop.Excel.Range objRange = objSheet.get_Range(objSheet.Cells[1,1],objSheet.Cells[50,50]);
			objExcelApp.Visible = false;
			objExcelApp.AlertBeforeOverwriting = false;
			objExcelApp.DisplayAlerts = false;
			objExcelApp.ScreenUpdating = false;
			Microsoft.Office.Interop.Excel.Range objRangeMonth = objSheet.get_Range(objSheet.Cells[1,1],objSheet.Cells[1,1]);
			string strValue = string.Empty;
			string strStoreMonthYear = string.Empty;
			int intMonth = 0, intYear = 0;
			if(objRangeMonth.Value2 != null)
			{
				objRangeMonth.ClearFormats();
				//Second, check title to get month
				string strMMYYYY = objRangeMonth.Value2.ToString();
				if (!strMMYYYY.Equals(string.Empty))
				{
					const string DATE_SLASH = "/";
					try
					{
						intMonth = int.Parse(strMMYYYY.Substring(0,strMMYYYY.IndexOf(DATE_SLASH)));
						intYear = int.Parse(strMMYYYY.Substring(strMMYYYY.IndexOf(DATE_SLASH)+DATE_SLASH.Length));
						strStoreMonthYear = BLANK + strMMYYYY;
					}
					catch
					{	
						//Do nothing
					}
				}
			}
			int[] delCols = new int[MAX_COL];
			// Modify date
			for(int intCol = START_COL_DATE; intCol < MAX_COL; intCol++)
			{
				Microsoft.Office.Interop.Excel.Range objRange = objSheet.get_Range(objSheet.Cells[START_ROW,intCol],objSheet.Cells[START_ROW,intCol]);
				
				if(objRange.Value2 != null)
				{
					// remove hidden column
//					if(objRange. != null)
//						if(Convert.ToBoolean(objRange.Hidden))
//						{
//							delCols[intCol] = 1;
//							continue;
//						}
					objRange.ClearFormats();
					strValue = objRange.Value2.ToString();
					try
					{
						int intDay = Convert.ToInt16(strValue);
						objRange.Value2 = intDay;
						if(DateTime.DaysInMonth(intYear,intMonth) < intDay)
						{
							delCols[intCol] = intDay;
						}
					}
					catch 
					{
						
					}
				}
				else
				{
					delCols[intCol] = 1;
					continue;
				}
			}
			// del all redundal column
			for(int col = delCols.Length-1;col > 0; col--)
			{
				if(delCols[col] > 0)
				{
					Microsoft.Office.Interop.Excel.Range objRange = objSheet.get_Range(objSheet.Cells[START_ROW,col],objSheet.Cells[START_ROW,col]);
					objRange.EntireColumn.Delete(false);
				}
			}
		
			Microsoft.Office.Interop.Excel.Range objRangeNo = objSheet.get_Range(objSheet.Cells[START_ROW,PARTNO_COL],objSheet.Cells[START_ROW,PARTNO_COL]).EntireColumn;
			objRangeNo.ClearFormats();
			// First column
			objRangeNo = objSheet.get_Range(objSheet.Cells[START_ROW,PARTNO_COL-1],objSheet.Cells[START_ROW,PARTNO_COL-1]).EntireColumn;
			objRangeNo.Value2 = string.Empty;
			objRangeMonth.Value2 = strStoreMonthYear;
			Microsoft.Office.Interop.Excel.Range objRangeName = objSheet.get_Range(objSheet.Cells[START_ROW,PARTNAME_COL],objSheet.Cells[START_ROW,PARTNAME_COL]).EntireColumn;
			objRangeName.ClearFormats();

			string strNewFile = pstrFileName + DateTime.Now.ToString(DATE_TIME) + XLS;
			objWorkBook.SaveCopyAs(strNewFile);
			objWorkBook.Close(Type.Missing,Type.Missing,Type.Missing);
			objExcelApp.Quit();
			return strNewFile;
		}

		private void mnuImportUpdate_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".mnuImportNew_Click()";
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if ((dstSaleOrderDetail.Tables[0].GetChanges() != null) && (dstSaleOrderDetail.Tables[0].GetChanges().Rows.Count != 0))
				{
					if (PCSMessageBox.Show(ErrorCode.MESSAGE_SAVE_BEFORE_IMPORT,MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
					{
						//save data
						#region check mandatory
						if(CheckMandatory(cboCCN))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							cboCCN.Focus();
							return;
						}
						if(CheckMandatory(cboTransDate))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							cboTransDate.Focus();
							return;
						}

						#region no need to check period

//						if(!FormControlComponents.CheckDateInCurrentPeriod((DateTime)cboTransDate.Value))
//						{
//							//PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE,MessageBoxIcon.Exclamation);
//							PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD,MessageBoxIcon.Exclamation);
//				
//							cboTransDate.Focus();
//							return;
//						}

						#endregion

						if(CheckMandatory(txtCurrency))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtCurrency.Focus();
							return;
						}			
						if(CheckMandatory(txtOrderNo))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtOrderNo.Focus();
							return;
						}
						if(CheckMandatory(txtExchRate))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtExchRate.Focus();
							return;
						}
						if (Decimal.Parse(txtExchRate.Value.ToString()) <= 0)
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtExchRate.Focus();
							return;
						}
						if(CheckMandatory(txtCustomer))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtCustomer.Focus();
							return;
						}
						if(IsNotExistCustomer())
						{
							// This customer does not exist in database
							PCSMessageBox.Show(ErrorCode.MESSAGE_CUSTOMER_DOES_NOT_EXIST,MessageBoxIcon.Exclamation);
							btnCustomer.Focus();
							return;
						}
						if(CheckMandatory(txtBuyLoc))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtBuyLoc.Focus();
							return;
						}
						if(CheckMandatory(txtShipToLoc))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtShipToLoc.Focus();
							return;
						}
						if(CheckMandatory(txtBillToLoc))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtBillToLoc.Focus();
							return;
						}
						if(CheckMandatory(txtShipFromLoc))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtShipFromLoc.Focus();
							return;
						}
						if(CheckMandatory(txtGateType))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtGateType.Focus();
							return;
						}
						
						// Checks sale order record
						if(gridData.RowCount <= 0)
						{
							// You have to input at least a record in grid sale order detail
							PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_AT_LEAST_RECORD_IN_GRID,MessageBoxIcon.Exclamation);
							gridData.Focus();
							return;
						}
						//Check mandatory in grid
						int intMaxRow = gridData.RowCount;
						
						//Check mandatory in grid.
						for (int i = 0; i < intMaxRow; i++)
						{
							if((gridData[i, SO_SaleOrderDetailTable.PRODUCTID_FLD]) == DBNull.Value)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ITEM_FIELD,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.PRODUCTID_FLD]);
								gridData.Focus();
								return;
							}
							if((gridData[i, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD]) == DBNull.Value)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ORDER_QUANTITY_FIELD,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD]);
								gridData.Focus();
								return;
							}
							if((gridData[i, SO_SaleOrderDetailTable.SELLINGUMID_FLD]) == DBNull.Value)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_OF_MEASURE_FIELD,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.SELLINGUMID_FLD]);
								gridData.Focus();
								return;
							}
							if(decimal.Parse(gridData[i, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString()) <= 0)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ORDER_QUANTITY_FIELD,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD]);
								gridData.Focus();
								return;
							}
							if((gridData[i, SO_SaleOrderDetailTable.UNITPRICE_FLD]) == DBNull.Value)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_PRICE_FIELD,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.UNITPRICE_FLD]);
								gridData.Focus();
								return;
							}
							if(decimal.Parse(gridData[i, SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString()) < 0)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_PRICE_FIELD,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.UNITPRICE_FLD]);
								gridData.Focus();
								return;
							}

							#region HACK: Trada 28-04-2006: 
							//Check Total Amount and Discount Amount

							if((gridData[i, SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] != DBNull.Value)
								&&(gridData[i, SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
								&&(gridData[i, SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != null)
								&&(gridData[i, SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] != null))
							{
								if (decimal.Parse(gridData[i, SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString()) > decimal.Parse(gridData[i, SO_SaleOrderDetailTable.TOTALAMOUNT_FLD].ToString()))
								{
									string[] strParam = new string[2];
									strParam[0] = "Discount Amount";
									strParam[1] = "Total Amount";
									PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
									gridData.Row = i;
									gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD]);
									gridData.Focus();
									return;
								}
							}
							//Check Order Quantity must be greater than Total Del Quantity
							//HACKED : DuongNA - fix error when form in add new mode
							if ((dstSODetailDeliverySchedule.Tables.Count > 0)
								&& (gridData[i, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] != DBNull.Value)
								&& (gridData[i, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] != null))
							{
								if ((gridData[i, SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD] != DBNull.Value)
									&& (gridData[i, SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD] != null))
								{
									DataRow[] adrowSODeliverySchedule = dstSODetailDeliverySchedule.Tables[0].Select(SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + " = " + gridData[i, SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString());
									if (adrowSODeliverySchedule.Length > 0)
									{
										if (decimal.Parse(gridData[i, SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString()) < decimal.Parse(adrowSODeliverySchedule[0][TOTAL_DELIVERY_QTY].ToString()))
										{
											string[] strParam = new string[2];
											strParam[0] = "Order Quantity";
											strParam[1] = "Total Delivery";//TOTAL_DELIVERY_QTY;
											PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
											gridData.Row = i;
											gridData.Col = gridData.Columns.IndexOf(gridData.Columns[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD]);
											gridData.Focus();
											return;
										}
									}
								}
							}
							#endregion END: Trada 28-04-2006
						}

						#endregion
						if (!StoreDatabaseBeforImport())
						{
							return;
						}
					}
					else
					{
						return;
					}
				}
				#region //Check madatory
				if(CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					cboCCN.Focus();
					return;
				}
				if(CheckMandatory(cboTransDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					cboTransDate.Focus();
					return;
				}
//				if(!FormControlComponents.CheckDateInCurrentPeriod((DateTime)cboTransDate.Value))
//				{
//					//PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE,MessageBoxIcon.Exclamation);
//					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD,MessageBoxIcon.Exclamation);
//				
//					cboTransDate.Focus();
//					return;
//				}
				if(CheckMandatory(txtCurrency))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtCurrency.Focus();
					return;
				}			
				if(CheckMandatory(txtOrderNo))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtOrderNo.Focus();
					return;
				}
				if(CheckMandatory(txtExchRate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtExchRate.Focus();
					return;
				}
				if (Decimal.Parse(txtExchRate.Value.ToString()) <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtExchRate.Focus();
					return;
				}
				if(CheckMandatory(txtCustomer))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtCustomer.Focus();
					return;
				}
				if(IsNotExistCustomer())
				{
					// This customer does not exist in database
					PCSMessageBox.Show(ErrorCode.MESSAGE_CUSTOMER_DOES_NOT_EXIST,MessageBoxIcon.Exclamation);
					btnCustomer.Focus();
					return;
				}
				if(CheckMandatory(txtBuyLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtBuyLoc.Focus();
					return;
				}
				if(CheckMandatory(txtShipToLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtShipToLoc.Focus();
					return;
				}
				if(CheckMandatory(txtBillToLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtBillToLoc.Focus();
					return;
				}
				if(CheckMandatory(txtShipFromLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtShipFromLoc.Focus();
					return;
				}
				#endregion

				//Do Import
				if (dlgOpenImpFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					DataTable dt = ReadImportData(dlgOpenImpFile.FileName);
					if (dt == null)
					{
						//Invalid file format
						PCSMessageBox.Show(ErrorCode.MESSAGE_IMPORT_ERROR_READ_FILE, MessageBoxIcon.Error);
						return;
					}
					else 
					{
						int intCheck = CheckImportData(dt);
						if (intCheck != -1)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_IMPORT_ERROR_FILE_FORMAT,MessageBoxIcon.Error);
							HighlightExcelFile(intCheck,dt.Columns.Count,dlgOpenImpFile.FileName);
							return;					
						}
					}

					int intPartyID = int.Parse(txtCustomer.Tag.ToString());
					int intCCNID = int.Parse(cboCCN.SelectedValue.ToString());

					//Get data
					int intMaxLine = 0;
					if(gridData.RowCount > 0)
					{
						try
						{
							intMaxLine = int.Parse(gridData[gridData.RowCount-1,SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString());
						}
						catch
						{
							intMaxLine = 0;
						}
						dstSaleOrderDetail.Tables[0].DefaultView.Sort = string.Empty;
					}
					else
					{
						intMaxLine = 0;
					}

					//get gate data
					DataSet dstGateData = boSaleOrder.ListGate();
					DataSet dstMappingData = dstSaleOrderDetail;
					blnIsChangedGrid = false;
					int intErrorLine = boSaleOrder.ImportUpdateMappingData(dt,intPartyID,intCCNID, intMaxLine, dstMappingData, dstGateData);
					if (intErrorLine == 0)
					{
						ReCalculate();
						StoreDatabaseAfterImport(dt,dlgOpenImpFile.FileName,dstGateData);
						mFormAction = EnumAction.Default;
						btnSave.Enabled = false;
						SetEnableButtons();
						SetEditableControl(false);
						////PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
						string[] arrParam = {STR_IMPORT_TASK};
						PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED,MessageBoxIcon.Information,arrParam);
						////TODO Request message
						blnHasError = false;
						//blnIsChangedGrid = true;
					}
					else
					{
						if (intErrorLine > 0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_IMPORT_ERROR_MAP_ITEM, MessageBoxIcon.Error);
						}
						else
						{
							string[] arrParams = {lblGate.Text};
							PCSMessageBox.Show(ErrorCode.NOT_FOUND, MessageBoxIcon.Error, arrParams);
						}
						//Cannot map data or gate not found
						//reload data
						InitGrid(voSaleOrderMaster.SaleOrderMasterID);
						HighlightExcelFile(Math.Abs(intErrorLine),dt.Columns.Count,dlgOpenImpFile.FileName);
					}
				}
			}
			catch (PCSException ex)
			{
				//reload data
				InitGrid(voSaleOrderMaster.SaleOrderMasterID);
				//show message
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
				this.Cursor = Cursors.Default;
			}		
		}

		private bool StoreDatabaseBeforImport()
		{
			const string METHOD_NAME = THIS + ".StoreDatabaseBeforeImport()";
			ControlsToVO();
			try
			{
				//SaleOrderBO boSaleOrder = new SaleOrderBO();
				boSaleOrder.UpdateSaleOrder(voSaleOrderMaster,dstSaleOrderDetail);
				return true;
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
					txtOrderNo.Focus();
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
			return false;
		}
		/// <summary>
		/// txtOrderNo_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void txtOrderNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtOrderNo_Validating()";
			if (mFormAction == EnumAction.Edit) return;
			if (!txtOrderNo.Modified) return;
			if (txtOrderNo.Text == string.Empty) //Clear form
			{
				ClearForm();
				InitGrid(0);
				SetEnableButtons();
				return;
			}
			//END Trada 2005-10-13
			if ((mFormAction == EnumAction.Add) || (mFormAction == EnumAction.Edit) || txtOrderNo.Text.Trim() == String.Empty)
			{
				return;
			}
			//Only use when users use this to search for existing product
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(SO_SaleOrderMasterTable.TABLE_NAME, SO_SaleOrderMasterTable.CODE_FLD, txtOrderNo.Text.Trim(), null, false);
				if (drwResult != null)
				{	
					voSaleOrderMaster.SaleOrderMasterID = int.Parse(drwResult[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString());
					//SaleOrderBO boSO = new SaleOrderBO();
					LoadMaster(voSaleOrderMaster.SaleOrderMasterID);
					voSaleOrderMaster = (SO_SaleOrderMasterVO)boSaleOrder.GetMasterVO(voSaleOrderMaster.SaleOrderMasterID);
					//VOToControls(voSaleOrderMaster);
					SetEnableButtons();
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtCustomer_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void txtCustomer_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCustomer_Validating()";
			try
			{
				if (!txtCustomer.Modified) return;
				if(txtCustomer.Text.Trim() == string.Empty)
				{
					txtCustomer.Tag = null;
					txtCustomerName.Text = string.Empty;
					txtBuyLoc.Text = string.Empty;
					txtBuyLoc.Tag = null;
					txtContact.Text = string.Empty;
					txtContact.Tag = null;
					txtShipToLoc.Text = string.Empty;
					txtShipToLoc.Tag = null;
					txtBillToLoc.Text = string.Empty;
					txtBillToLoc.Tag = null;
					ResetCustomerItemAfterChangingOfCustomer();
					return;
				}
				htbCriteria = new Hashtable();
				htbCriteria.Add(CUSTOMER, 0);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtCustomer.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtCustomer.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtCustomer.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					ResetCustomerItemAfterChangingOfCustomer();
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtCustomerName_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
		private void txtCustomerName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCustomerName_Validating()";
			try
			{
				if (!txtCustomerName.Modified) return;
				if(txtCustomerName.Text.Trim() == string.Empty)
				{
					txtCustomer.Text = string.Empty;
					txtCustomer.Tag = null;
					txtBuyLoc.Text = string.Empty;
					txtBuyLoc.Tag = null;
					txtContact.Text = string.Empty;
					txtContact.Tag = null;
					return;
				}
				
				htbCriteria = new Hashtable();
				htbCriteria.Add(CUSTOMER, 0);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtCustomerName.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtCustomer.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					txtCustomer.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtCurrency_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
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
			
				drwResult = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME, MST_CurrencyTable.CODE_FLD, txtCurrency.Text.Trim(), null, false);
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
				if (ex.mCode != ErrorCode.DUPLICATE_KEY && ex.mCode != ErrorCode.DUPLICATEKEY)
				{
					PCSMessageBox.Show(ex.mCode,MessageBoxIcon.Error);
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY,MessageBoxIcon.Error);
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtBuyLoc_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
		private void txtBuyLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBuyLoc_Validating()";
			try
			{
				if (!txtBuyLoc.Modified) return;
				if(txtBuyLoc.Text.Trim() == string.Empty)
				{
					txtBuyLoc.Tag = null;
					return;
				}
			
				if(txtCustomer.Tag == null)
				{
					strParam[1] = lblBuyLoc.Text;
					// You have to select Customer before select Buy Location, please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);				
					txtBuyLoc.Text = string.Empty;
					txtCustomer.Focus();
					return;
				}
				
				htbCriteria = new Hashtable();
				htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtCustomer.Tag.ToString());
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtBuyLoc.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtBuyLoc.Tag = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtBuyLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtContact_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void txtContact_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtContact_Validating()";
			try
			{
				if (!txtContact.Modified) return;
				if (txtContact.Text == string.Empty)
				{
					txtContact.Tag = null;
					return;
				}
				htbCriteria = new Hashtable();
				if(txtCustomer.Text != string.Empty)
				{
					if(txtCustomer.Tag != null)
					{
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtCustomer.Tag.ToString());
					}
				}
				else
				{
					strParam[1] = lblContact.Text;
					// You have to select Vendor before , please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtContact.Text = string.Empty;
					txtCustomer.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyContactTable.TABLE_NAME, MST_PartyContactTable.CODE_FLD, txtContact.Text.Trim(), htbCriteria, false);
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtShipToLoc_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
		private void txtShipToLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtShipToLoc_Validating()";
			try
			{
				if(txtShipToLoc.Text.Trim() == string.Empty)
				{
					txtShipToLoc.Tag = null;
					return;
				}
				if (!txtShipToLoc.Modified) return;
				if(txtCustomer.Tag == null)
				{
					strParam[1] = lblShipToLoc.Text;
					// You have to select Customer before select Buy Location, please
					txtShipToLoc.Text = string.Empty;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE,MessageBoxIcon.Warning, strParam);				
					txtCustomer.Focus();
					return;
				}
				htbCriteria = new Hashtable();
				//string strVendorLoc = " AND " + MST_PartyLocationTable.PARTYID_FLD + "=" + txtCustomer.Tag.ToString();
				htbCriteria.Add(MST_PartyLocationTable.PARTYID_FLD, txtCustomer.Tag.ToString());
				
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtShipToLoc.Text.Trim(), htbCriteria, false);
				if(drwResult != null)
				{
					txtShipToLoc.Tag = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtShipToLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtBillToLoc_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void txtBillToLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBillToLoc_Validating()";
			try
			{
				if(txtBillToLoc.Text.Trim() == string.Empty)
				{
					txtBillToLoc.Tag = null;
					return;
				}
				if (!txtBillToLoc.Modified) return;
				if(txtCustomer.Tag == null)
				{
					strParam[1] = lblBillToLoc.Text;
					// You have to select Customer before select Buy Location, please
					txtBillToLoc.Text = string.Empty;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE,MessageBoxIcon.Warning, strParam);				
					txtCustomer.Focus();
					return;
				}
				htbCriteria = new Hashtable();
				htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtCustomer.Tag.ToString());
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtBillToLoc.Text.Trim(), htbCriteria, false);
				if(drwResult != null)
				{
					txtBillToLoc.Tag = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtBillToLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtShipFromLoc_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void txtShipFromLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtShipFromLoc_Validating()";
			try
			{
				if (!txtShipFromLoc.Modified) return;
				if(txtShipFromLoc.Text.Trim() == string.Empty) 
				{
					txtShipFromLoc.Tag = null;
					return;
				}
			
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtShipFromLoc.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtShipFromLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
					txtShipFromLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtSalesRepres_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void txtSalesRepres_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtSalesRepres_Validating()";
			try
			{
				if (!txtSalesRepres.Modified) return;
				if(txtSalesRepres.Text.Trim() == string.Empty) 
				{
					txtSalesRepres.Tag = null;
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_EmployeeTable.TABLE_NAME, MST_EmployeeTable.CODE_FLD, txtSalesRepres.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtSalesRepres.Tag = drwResult[MST_EmployeeTable.EMPLOYEEID_FLD];
					txtSalesRepres.Text = drwResult[MST_EmployeeTable.CODE_FLD].ToString();
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtSalesType_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void txtSalesType_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtSalesType_Validating()";
			try
			{
				if (!txtSalesType.Modified) return;
				if(txtSalesType.Text.Trim() == string.Empty) 
				{
					txtSalesType.Tag = null;
					return;
				}

				drwResult = FormControlComponents.OpenSearchForm(SO_SaleTypeTable.TABLE_NAME,SO_SaleTypeTable.CODE_FLD, txtSalesType.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtSalesType.Tag = drwResult[SO_SaleTypeTable.SALETYPEID_FLD];
					txtSalesType.Text = drwResult[SO_SaleTypeTable.CODE_FLD].ToString();
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtPayTerms_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void txtPayTerms_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPayTerms_Validating()";
			try
			{
				if (!txtPayTerms.Modified) return;
				if(txtPayTerms.Text.Trim() == string.Empty)
				{
					txtPayTerms.Tag = null;
					return;
				}

				drwResult = FormControlComponents.OpenSearchForm(MST_PaymentTermTable.TABLE_NAME, MST_PaymentTermTable.CODE_FLD, txtPayTerms.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtPayTerms.Tag = drwResult[MST_PaymentTermTable.PAYMENTTERMID_FLD];
					txtPayTerms.Text = drwResult[MST_PaymentTermTable.CODE_FLD].ToString();
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtCarrier_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void txtCarrier_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCarrier_Validating()";
			try
			{
				if (!txtCarrier.Modified) return;
				if(txtCarrier.Text.Trim() == string.Empty)
				{
					txtCarrier.Tag = null;
					return;
				}

				drwResult = FormControlComponents.OpenSearchForm(MST_CarrierTable.TABLE_NAME, MST_CarrierTable.CODE_FLD, txtCarrier.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtCarrier.Tag = drwResult[MST_CarrierTable.CARRIERID_FLD];
					txtCarrier.Text = drwResult[MST_CarrierTable.CODE_FLD].ToString();
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
					txtOrderNo.Focus();
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

		/// <summary>
		/// txtPause_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void txtPause_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPause_Validating()";
			try
			{
				if (!txtPause.Modified) return;
				if(txtPause.Text.Trim() == string.Empty)			
				{
					txtPause.Tag = null;
					return;
				}

				drwResult = FormControlComponents.OpenSearchForm(MST_PauseTable.TABLE_NAME, MST_PauseTable.CODE_FLD, txtPause.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtPause.Tag = drwResult[MST_PauseTable.PAUSEID_FLD];
					txtPause.Text = drwResult[MST_PauseTable.CODE_FLD].ToString();
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtDiscTerms_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void txtDiscTerms_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDiscTerms_Validating()";
			try
			{
				if(!txtDiscTerms.Modified) return;
				if(txtDiscTerms.Text.Trim() == string.Empty)
				{
					txtDiscTerms.Tag = null;
					decDiscountRate = 0;
                    int intRowCount = gridData.RowCount;
					// update grid
					for(int i = 0; i < intRowCount; i++)
					{ 
						gridData[i,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] = DBNull.Value;
					}
					ReCalculate();
					return;
				}

				drwResult = FormControlComponents.OpenSearchForm(MST_DiscountTermTable.TABLE_NAME, MST_DiscountTermTable.CODE_FLD, txtDiscTerms.Text.Trim(), null, false);
				if(drwResult != null)
				{
					decDiscountRate = decimal.Parse(drwResult[MST_DiscountTermTable.RATE_FLD].ToString());
					txtDiscTerms.Text = drwResult[MST_DiscountTermTable.CODE_FLD].ToString();
					txtDiscTerms.Tag = drwResult[MST_DiscountTermTable.DISCOUNTTERMID_FLD];
                    int intRowCount = gridData.RowCount;
					
					// update grid
					for(int i = 0; i < intRowCount; i++)
					{
						try
						{
							gridData[i,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] = decDiscountRate
								*decimal.Parse(gridData[i,SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString())
								*decimal.Parse(gridData[i,SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString())/ONE_HUNDRED;
						}
						catch
						{
							gridData[i,SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] = 0;
						}
					}
					ReCalculate();
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
					txtOrderNo.Focus();
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
		/// <summary>
		/// txtDeliveryTerms_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void txtDeliveryTerms_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDeliveryTerms_Validating()";
			try
			{
				if (!txtDeliveryTerms.Modified) return;
				if(txtDeliveryTerms.Text.Trim() == string.Empty) 
				{
					txtDeliveryTerms.Tag = null;
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_DeliveryTermTable.TABLE_NAME, MST_DeliveryTermTable.CODE_FLD, txtDeliveryTerms.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtDeliveryTerms.Tag = drwResult[MST_DeliveryTermTable.DELIVERYTERMID_FLD];
					txtDeliveryTerms.Text = drwResult[MST_DeliveryTermTable.CODE_FLD].ToString();
				}
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
					txtOrderNo.Focus();
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

		private void ResetCustomerItemAfterChangingOfCustomer()
		{
			if (txtCustomer.Text.Trim() == string.Empty)
			{
				//reset all
                for (int i = 0; i < gridData.RowCount; i++)
                {
                    gridData[i, SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD] = string.Empty;
                    gridData[i, SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD] = string.Empty;
                }
			}
			else
			{
				//set again
				SO_CustomerItemRefDetailVO voCusItem;
                for (int i = 0; i < gridData.RowCount; i++)
                {
                    if (gridData[i, ITM_ProductTable.CODE_FLD].ToString() != string.Empty)
                    {
                        voCusItem = (SO_CustomerItemRefDetailVO)boSaleOrder.GetItemCustomerRef((int)gridData[i, SO_SaleOrderDetailTable.PRODUCTID_FLD], (int)txtCustomer.Tag);
                        if (voCusItem != null)
                        {
                            gridData[i, SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD] = voCusItem.CustomerItemCode;
                            gridData[i, SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD] = voCusItem.CustomerItemModel;
                        }
                        else
                        {
                            gridData[i, SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD] = string.Empty;
                            gridData[i, SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD] = string.Empty;
                        }
                    }
                }
			}
		}

		private void dlgOpenImpFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
		
		}

		/// <summary>
		/// Check deleting in case selecting multiplies rows
		/// </summary>
		/// <returns></returns>
		private bool CheckBeforeDeleteAllRows()
		{
			try 
			{
				for (int i = 0; i <gridData.SelectedRows.Count; i++)
				{
					if (gridData[int.Parse(gridData.SelectedRows[i].ToString()),  SO_DeliveryScheduleTable.DELIVERYNO_FLD].ToString() != 0.ToString()
						&& gridData[int.Parse(gridData.SelectedRows[i].ToString()),  SO_DeliveryScheduleTable.DELIVERYNO_FLD].ToString() != string.Empty)
					{
						return false;
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void SaleOrder_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode) 
			{
				case Keys.F12:
					// New record
					if((mFormAction == EnumAction.Edit) || (mFormAction == EnumAction.Add)) 
					{
						if (!blnHasCommited)
						{
							gridData.Focus();
							gridData.AllowAddNew = true;
							gridData.AllowUpdate = true;
							gridData.Col = gridData.Splits[0].DisplayColumns.IndexOf(gridData.Columns[ITM_ProductTable.CODE_FLD]);
							gridData.Row = gridData.RowCount;
						}
					}
					break;
			}		
		}

		private void btnAutoCancelSO_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDeliveryTerms_Validating()";
			try
			{
				SOAvoidDupleSaleOrdersDelivery frmAvoidDupleSaleOrdersDelivery = new SOAvoidDupleSaleOrdersDelivery();
				frmAvoidDupleSaleOrdersDelivery.Show();
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
		/// gridData_BeforeColEdit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author>Trada</Author>
		/// <date>Friday, April 28 2006</date>
		private void gridData_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridData_BeforeColEdit()";
			try
			{
				#region HACK: Trada 28-04-2006: Check editable items
				if ((e.Column.DataColumn.DataField == ITM_ProductTable.CODE_FLD)
					||(e.Column.DataColumn.DataField == ITM_ProductTable.DESCRIPTION_FLD)
					||(e.Column.DataColumn.DataField == ITM_ProductTable.REVISION_FLD))
				{
					if (gridData.Columns[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].Value != DBNull.Value)
					{
						if (mFormAction == EnumAction.Edit) 
						{
							if  (!CheckEditableItems(Convert.ToInt32(gridData.Columns[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].Value)))
							{
								string[] strParam = new string[1];
								strParam[0] = "Sale Order Detail";
								PCSMessageBox.Show(ErrorCode.MESSAGE_SO_CAN_NOT_CHANGE_ITEM, MessageBoxIcon.Warning, strParam);
								e.Cancel = true;
								return;
							}
							if (blnHasCommited)
							{
								e.Cancel = true;
							}
						}
					}
				}		
				
				#endregion END: Trada 28-04-2006
				if ((e.Column.DataColumn.DataField != SO_SaleOrderDetailTable.ORDERQUANTITY_FLD)
					&& (e.Column.DataColumn.DataField != SO_SaleOrderDetailTable.UNITPRICE_FLD))
				{
					if (blnHasCommited)
					{
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
		private void btnGateType_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnGateType_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(SO_TypeTable.TABLE_NAME, SO_TypeTable.DESCRIPTION_FLD, txtGateType.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtGateType.Tag = drwResult[SO_TypeTable.TYPEID_FLD];
					txtGateType.Text = drwResult[SO_TypeTable.DESCRIPTION_FLD].ToString();
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

		private void txtGateType_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtGateType_Validating()";
			try
			{
				if(!txtGateType.Modified) return;
				drwResult = FormControlComponents.OpenSearchForm(SO_TypeTable.TABLE_NAME, SO_TypeTable.DESCRIPTION_FLD, txtGateType.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtGateType.Text = drwResult[SO_TypeTable.DESCRIPTION_FLD].ToString();
					txtGateType.Tag = drwResult[SO_TypeTable.TYPEID_FLD];
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

		private void txtGateType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnGateType.Enabled)
					btnGateType_Click(sender,e);
		}
		private void UpdateSchedule(int pintMasterID,DataSet pdstSODetail, DataTable dtImportData, DataSet pdstGateData)
		{
			//get delivery schedules schema
			//SaleOrderBO boSO = new SaleOrderBO();
			int intMonth = 0,intYear = 0;
			DataSet dstDeli = boSaleOrder.ListScheduleForImport(pintMasterID);
			string strMMYYYY = dtImportData.Rows[0][0].ToString();
			const string DATE_SLASH = "/";
			try
			{
				intMonth = int.Parse(strMMYYYY.Substring(0,strMMYYYY.IndexOf(DATE_SLASH)));
				intYear = int.Parse(strMMYYYY.Substring(strMMYYYY.IndexOf(DATE_SLASH)+DATE_SLASH.Length));
			}
			catch{}

			//Add temp column for new quantity
			DataColumn objCol = new DataColumn(TEMP_QTY_COL_NAME);
			objCol.DataType = typeof(decimal);
			objCol.DefaultValue = 0;
			dstDeli.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Columns.Add(objCol);

			//insert delivery schedules
			int intEndDataCol = dtImportData.Columns.Count - 1;
			int intEndDataRow = dtImportData.Rows.Count;
			int intIdx = 0;
			foreach (DataRow objRow in pdstSODetail.Tables[0].Rows)
			{
				int intLine = 0;
				for (int i = INT_BEGIN_DATA_COL; i < intEndDataCol; i++)
				{
					for (int j = INT_BEGIN_DATA_ROW; j < intEndDataRow; j++)
					{
						if (dtImportData.Rows[j][INT_PARTS_NO_COL].ToString().Equals(objRow[SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD].ToString()))
						{
							int intDay = int.Parse(dtImportData.Rows[INT_HEADER_ROW][i].ToString());
							int intHour = int.Parse(dtImportData.Rows[j][INT_TIME_COL].ToString());
							DateTime dateDeli = new DateTime(intYear,intMonth,intDay,intHour,0,0);
							decimal dcmQty;
							if (dtImportData.Rows[j][i] == DBNull.Value)
							{
								continue;
							}
							else
							{
								intLine++;
								try 
								{
									dcmQty = decimal.Parse(dtImportData.Rows[j][i].ToString());
								}
								catch
								{
									continue;
								}
							}

							//findout gate code
							string strGateCode = dtImportData.Rows[j][INT_CODE_COL].ToString().Trim();
							int intGateID = 0;
							if (strGateCode != string.Empty)
							{
								//lookup gate id
								DataRow[] arrGate = pdstGateData.Tables[0].Select(SO_GateTable.CODE_FLD + "='" + strGateCode + "'");
								if (arrGate.Length == 0)
								{
									//never occurs
								}
								else
								{
									intGateID = Convert.ToInt32(arrGate[0][SO_GateTable.GATEID_FLD]);
								}
							}

							//Check if this schedule exist, only update quantity
							string strFilter = SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=" + objRow[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString() 
								+ " AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "='" + dateDeli.ToString() + "'";
							if (intGateID != 0)
							{
								strFilter += " AND " + SO_DeliveryScheduleTable.GATEID_FLD + "=" + intGateID;
							}
							else
							{
								strFilter += " AND " + SO_DeliveryScheduleTable.GATEID_FLD + " IS NULL";
							}

							DataRow[] arrSchRows = dstDeli.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Select(strFilter);
							if (arrSchRows.Length > 0)
							{
								arrSchRows[0][TEMP_QTY_COL_NAME] = decimal.Parse(arrSchRows[0][TEMP_QTY_COL_NAME].ToString()) + dcmQty;
								continue;
							}

							DataRow drDeli = dstDeli.Tables[SO_DeliveryScheduleTable.TABLE_NAME].NewRow();
							drDeli[SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD] = objRow[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD];
							if (dtImportData.Rows[j][i] == DBNull.Value)
							{
								continue;
							}
							drDeli[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = dcmQty;
							drDeli[TEMP_QTY_COL_NAME] = dcmQty;
							drDeli[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dateDeli;
							drDeli[SO_DeliveryScheduleTable.LINE_FLD] = intLine;
							if (intGateID > 0)
							{
								drDeli[SO_DeliveryScheduleTable.GATEID_FLD] = intGateID; //objRow[SO_GateTable.GATEID_FLD];
							}
							dstDeli.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Rows.Add(drDeli);
						}
					}
				}
				intIdx++;
			}

			//refine DelSch data
			int intNewLine = 0;
			DataRow[] arrRows = dstDeli.Tables[0].Select(string.Empty,SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD);
			int intLastDetailID = -1;
			int intCurrentProductID = -1;
			for (int i = 0; i < arrRows.Length; i++)
			{
				int intDetailID = int.Parse(arrRows[i][SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString());

				if (intDetailID != intLastDetailID)
				{
					//reset line
					intNewLine = 0;
					intLastDetailID = intDetailID;
				}
				if (decimal.Parse(arrRows[i][TEMP_QTY_COL_NAME].ToString()) == 0)
				{
					arrRows[i].Delete();
				}
				else
				{
					//update line
					arrRows[i][SO_DeliveryScheduleTable.LINE_FLD] = ++intNewLine;
					//only update qty
					decimal dcmNewQTy = decimal.Parse(arrRows[i][TEMP_QTY_COL_NAME].ToString());
					arrRows[i][SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = dcmNewQTy;
				}
			}

			boSaleOrder.UpdateScheduleForImport(dstDeli);
			dstDeli.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Columns.Remove(objCol);
		}
		private void UpdateScheduleForImportUpdate(int pintMasterID, DataSet pdstSODetail, DataTable dtImportData, DataSet pdstDelSchData, DataSet pdstGateData, ref int pintErrorLine)
		{
			const string METHOD_NAME = THIS + ".UpdateScheduleForImportUpdate()";
			int intMonth = 0,intYear = 0;
			string strMMYYYY = dtImportData.Rows[0][0].ToString();
			const string DATE_SLASH = "/";
			try
			{
				intMonth = int.Parse(strMMYYYY.Substring(0,strMMYYYY.IndexOf(DATE_SLASH)));
				intYear = int.Parse(strMMYYYY.Substring(strMMYYYY.IndexOf(DATE_SLASH)+DATE_SLASH.Length));
			}
			catch{}

			//Add temp column for new quantity
			DataColumn objCol = new DataColumn(TEMP_QTY_COL_NAME);
			objCol.DataType = typeof(decimal);
			objCol.DefaultValue = 0;
			pdstDelSchData.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Columns.Add(objCol);

			//insert delivery schedules
			int intEndDataCol = dtImportData.Columns.Count - 1;
			int intEndDataRow = dtImportData.Rows.Count;
			int intRowIdx = -1;
			foreach (DataRow objRow in pdstSODetail.Tables[0].Rows)
			{
				intRowIdx++;
				int intLine = 0;
				for (int i = INT_BEGIN_DATA_COL; i < intEndDataCol; i++)
				{
					for (int j = INT_BEGIN_DATA_ROW; j < intEndDataRow; j++)
					{
						if (dtImportData.Rows[j][INT_PARTS_NO_COL].ToString().Equals(objRow[SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD].ToString()))
						{
							int intDay = int.Parse(dtImportData.Rows[INT_HEADER_ROW][i].ToString());
							int intHour = int.Parse(dtImportData.Rows[j][INT_TIME_COL].ToString());
							DateTime dateDeli = new DateTime(intYear,intMonth,intDay,intHour,0,0);
							decimal dcmQty;
							if (dtImportData.Rows[j][i] == DBNull.Value)
								continue;
							else
							{
								intLine++;
								try 
								{
									dcmQty = decimal.Parse(dtImportData.Rows[j][i].ToString());
									if (dcmQty <= 0) 
										continue;
								}
								catch
								{
									continue;
								}
							}

							//findout gate code
							string strGateCode = dtImportData.Rows[j][INT_CODE_COL].ToString().Trim();
							int intGateID = 0;
							if (strGateCode != string.Empty)
							{
								//lookup gate id
								DataRow[] arrGate = pdstGateData.Tables[0].Select(SO_GateTable.CODE_FLD + "='" + strGateCode + "'");
								if (arrGate.Length != 0)
								{
									intGateID = Convert.ToInt32(arrGate[0][SO_GateTable.GATEID_FLD]);
								}
							}

							//Check if this schedule exist, only update quantity
							string strFilter = SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=" + objRow[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString() 
								+ " AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "='" + dateDeli.ToString() + "'";

							if (intGateID > 0)
								strFilter += " AND " + SO_DeliveryScheduleTable.GATEID_FLD + "=" + intGateID;
							else
								strFilter += " AND " + SO_DeliveryScheduleTable.GATEID_FLD + " IS NULL";

							DataRow[] arrSchRows = pdstDelSchData.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Select(strFilter);
							if (arrSchRows.Length > 0)
							{
								arrSchRows[0][TEMP_QTY_COL_NAME] = decimal.Parse(arrSchRows[0][TEMP_QTY_COL_NAME].ToString()) + dcmQty;
								if (intGateID > 0)
									arrSchRows[0][SO_DeliveryScheduleTable.GATEID_FLD] = intGateID;
								else
									arrSchRows[0][SO_DeliveryScheduleTable.GATEID_FLD] = DBNull.Value;
								continue;
							}

							DataRow drDeli = pdstDelSchData.Tables[SO_DeliveryScheduleTable.TABLE_NAME].NewRow();
							drDeli[TEMP_QTY_COL_NAME] = dcmQty;
							drDeli[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = dcmQty;
							drDeli[SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD] = objRow[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD];
							drDeli[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dateDeli;
							drDeli[SO_DeliveryScheduleTable.LINE_FLD] = intLine;
							if (intGateID > 0)
								drDeli[SO_DeliveryScheduleTable.GATEID_FLD] = intGateID;
							else
								drDeli[SO_DeliveryScheduleTable.GATEID_FLD] = DBNull.Value;
							pdstDelSchData.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Rows.Add(drDeli);
						}
					}
				}
			}

			//refine DelSch data
			int intNewLine = 0;
			DataRow[] arrRows = pdstDelSchData.Tables[0].Select(string.Empty,SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD);
			int intLastDetailID = -1;
			int intCurrentProductID = -1;
			//SaleOrderBO boSO = new SaleOrderBO();
			DataTable dtbRemain = boSaleOrder.GetRemainQuantity(pintMasterID);

			intRowIdx = -1;
			for (int i = 0; i < arrRows.Length; i++)
			{
				int intDetailID = int.Parse(arrRows[i][SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString());

				if (intDetailID != intLastDetailID)
				{
					//reset line
					intNewLine = 0;
					intLastDetailID = intDetailID;
					intRowIdx++;

					//get new product id
					DataRow[] arrDetails = pdstSODetail.Tables[SO_SaleOrderDetailTable.TABLE_NAME].Select(SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + "=" + intLastDetailID.ToString());
					//check if not found? not needed
					intCurrentProductID = int.Parse(arrDetails[0][SO_SaleOrderDetailTable.PRODUCTID_FLD].ToString());
				}
				if (decimal.Parse(arrRows[i][TEMP_QTY_COL_NAME].ToString()) == 0)
				{
					//Not exist in excel file
					int intDeliID = int.Parse(arrRows[i][SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString());
					decimal dcmRemainQty = decimal.MinValue;
					try
					{
						dcmRemainQty = GetRemainQuantity(pintMasterID, intDeliID,intCurrentProductID, dtbRemain);
					}
					catch{}
					decimal dcmOldQty = decimal.Parse(arrRows[i][SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
					if (dcmRemainQty != dcmOldQty && dcmRemainQty != decimal.MinValue)
					{
						pintErrorLine = intRowIdx + INT_BEGIN_DATA_ROW;
						throw new PCSDBException(ErrorCode.MESSAGE_IMPORT_ERROR_UPDATE_COMMITTED,METHOD_NAME,null);
						//cannot delete because this schedule is commited, what we will to do here ???
					}
					else
						arrRows[i].Delete();
				}
				else
				{
					if (arrRows[i].RowState != DataRowState.Added)
					{
						//update line
						arrRows[i][SO_DeliveryScheduleTable.LINE_FLD] = ++intNewLine;
						//only update qty
						//check for total comitted
						//int intDeliID = int.Parse(arrRows[i][SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString());
						//decimal dcmRemainQty = dsCommit.GetRemainQuantity(intMasterSOID, intDeliID,intCurrentProductID);
						decimal dcmCommittedQty = Convert.ToDecimal(arrRows[i][SUMCOMMITQUANTITY_FLD]);
						//decimal dcmOldQty = decimal.Parse(arrRows[i][SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
						decimal dcmNewQTy = decimal.Parse(arrRows[i][TEMP_QTY_COL_NAME].ToString());
						if (dcmCommittedQty > dcmNewQTy)
						{
							//Cannot update quantity less than commited
							pintErrorLine = intRowIdx + INT_BEGIN_DATA_ROW;
							throw new PCSDBException(ErrorCode.MESSAGE_IMPORT_ERROR_UPDATE_COMMITTED,METHOD_NAME,null);
						}
						else
							arrRows[i][SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = dcmNewQTy;
					}
					else
					{
						//update line
						arrRows[i][SO_DeliveryScheduleTable.LINE_FLD] = ++intNewLine;
						arrRows[i][SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = arrRows[i][TEMP_QTY_COL_NAME];
					}
				}
			}

			pdstDelSchData.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Columns.Remove(objCol);
			boSaleOrder.UpdateInsertedRowInDataSet(pdstDelSchData,pintMasterID);
		}
		private decimal GetRemainQuantity(int pintMasterID, int pintDeliID, int pintCurrentProductID, DataTable pdtbRemain)
		{
			decimal decRemain = 0;
			try
			{
				string strFilter = SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + "=" + pintMasterID + " AND "
					+ SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "=" + pintDeliID + " AND "
					+ ITM_ProductTable.PRODUCTID_FLD + "=" + pintCurrentProductID;
				decRemain = Convert.ToDecimal(pdtbRemain.Compute("SUM(" + REMAIN_QTY + ")", strFilter));
				return decRemain;
			}
			catch
			{
				throw new ArgumentException();
			}
		}

		/// <summary>
		/// btnUpdateUnitPrice_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author>CanhNv</Author>
		/// <date>18/04/2007</date>
		private void btnUpdateUnitPrice_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnUpdateUnitPrice_Click()";
			try
			{
				if (voSaleOrderMaster.SaleOrderMasterID > 0)	
				{
					DataSet dsListDeatail;
					SO_CustomerItemRefMasterDS objCustomerDS = new SO_CustomerItemRefMasterDS();

					//1. Get CustomerItemRefMasterid
					SO_CustomerItemRefMasterVO voCustomerItemRefMaster;
					voCustomerItemRefMaster = (SO_CustomerItemRefMasterVO) objCustomerDS.GetObjectVO(voSaleOrderMaster.PartyID,voSaleOrderMaster.CCNID);
					
					//2. Get UnitPrice change in detail table
					dsListDeatail = boSaleOrder.GetListDetail(voSaleOrderMaster.SaleOrderMasterID,voCustomerItemRefMaster.CustomerItemRefMasterID);
					decimal decBaseAmount = 0, decOrderQuantity = 0, decUnitPrice = 0;
					
					//3. Data process
					foreach (DataRow dRow in dsListDeatail.Tables[0].Rows)
					{
						#region Calculate UnitPrice change
						if (dRow[SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString().Trim() != dRow["UnitPriceCustomerIRD"].ToString().Trim())
							dRow[SO_SaleOrderDetailTable.UNITPRICE_FLD] = dRow["UnitPriceCustomerIRD"];
						else
							continue;
						try
						{
							decOrderQuantity = decimal.Parse(dRow[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString());
							decUnitPrice = decimal.Parse(dRow[SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString());
							decBaseAmount = decOrderQuantity * decUnitPrice;
						}
						catch{}
						
						try
						{
							dRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] = (decDiscountRate * decBaseAmount)/ONE_HUNDRED;
						}
						catch {}
						CalculateNetAmountUnitPrice(decBaseAmount,dRow);
						CalculateVATAmountUnitPrice(decBaseAmount,dRow);
						CalculateExpAmountUnitPrice(decBaseAmount,dRow);
						CalculateSpecAmountUnitPrice(decBaseAmount,dRow);
						CalculateTotalAmountUnitPrice(decBaseAmount,dRow);	
						#endregion
					}
					//4. Update Change 
                    boSaleOrder.UpdateUnitPrice(dsListDeatail);

					//5. Load again
                    LoadMaster(voSaleOrderMaster.SaleOrderMasterID);
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
		/// CalculateNetAmountUnitPrice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author>CanhNv</Author>
		/// <date>18/04/2007</date>
		private void CalculateNetAmountUnitPrice(decimal pdecAmount,DataRow dRow)
		{
			try
			{
				decimal decDiscountAmount = 0;
				if(dRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
					decDiscountAmount = decimal.Parse(dRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());
				dRow[SO_SaleOrderDetailTable.NETAMOUNT_FLD] = pdecAmount - decDiscountAmount;				
			}
			catch{}
		}
		/// <summary>
		/// CalculateVATAmountUnitPrice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author>CanhNv</Author>
		/// <date>18/04/2007</date>
		private void CalculateVATAmountUnitPrice(decimal pdecAmount,DataRow dRow)
		{
			try
			{
				if(!chkVAT.Checked) return;
				if(dRow[SO_SaleOrderDetailTable.VATPERCENT_FLD] != DBNull.Value) 
				{
					if(dRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] == DBNull.Value)
					{
						dRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] =  (pdecAmount
							* decimal.Parse(dRow[SO_SaleOrderDetailTable.VATPERCENT_FLD].ToString()))/ONE_HUNDRED;
					}
					else
					{
						dRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] =  ((pdecAmount - decimal.Parse(dRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString()))
							* decimal.Parse(dRow[SO_SaleOrderDetailTable.VATPERCENT_FLD].ToString()))/ONE_HUNDRED;
					}
				}
				else
				{
					dRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] = 0;
				}
			}
			catch
			{
				dRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] = 0;
			}
		}

		/// <summary>
		/// CalculateExpAmountUnitPrice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author>CanhNv</Author>
		/// <date>18/04/2007</date>
		private void CalculateExpAmountUnitPrice(decimal pdecAmount,DataRow dRow)
		{
			try
			{
				if(!chkExportTax.Checked) return;
				if(dRow[SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD] != DBNull.Value)
				{
					if(dRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] == DBNull.Value)
					{
						dRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] =  (pdecAmount
							* decimal.Parse(dRow[SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD].ToString()))/ONE_HUNDRED;
					}
					else
					{
						dRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] =  ((pdecAmount - decimal.Parse(dRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString()))
							* decimal.Parse(dRow[SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD].ToString()))/ONE_HUNDRED;
					}				
				}
				else
				{
					dRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] = 0;
				}
			}
			catch{}
		}
		/// <summary>
		/// CalculateSpecAmountUnitPrice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author>CanhNv</Author>
		/// <date>18/04/2007</date>
		private void CalculateSpecAmountUnitPrice(decimal pdecAmount,DataRow dRow)
		{
			try
			{
				if(!chkSpecialTax.Checked) return;
				decimal decVatAmout, decExpAmount;
				if(dRow[SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD] != DBNull.Value)
				{
					if(dRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value)
					{
						decVatAmout = decimal.Parse(dRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD].ToString());
					}
					else 
						decVatAmout = 0;
					if(dRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] != DBNull.Value)
					{
						decExpAmount = decimal.Parse(dRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].ToString());
					}
					else 
						decExpAmount = 0;
					if(dRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] == DBNull.Value)
					{
						dRow[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] =  (pdecAmount + decVatAmout + decExpAmount)
							* decimal.Parse(dRow[SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD].ToString())/ONE_HUNDRED;
					}
					else
					{
						dRow[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] =  ((pdecAmount - decimal.Parse(dRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString())) + decVatAmout + decExpAmount)
							* decimal.Parse(dRow[SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD].ToString())/ONE_HUNDRED;
					}				
				}
				else
				{
					dRow[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] = 0;
				}
			}
			catch{}
		}
		/// <summary>
		/// CalculateTotalAmountUnitPrice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author>CanhNv</Author>
		/// <date>18/04/2007</date>
		private void CalculateTotalAmountUnitPrice(decimal pdecAmount,DataRow dRow)
		{
			try
			{
				decimal decVatAmount,decExpAmount,decSpecAmount;
				if(dRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value) 
					decVatAmount = decimal.Parse(dRow[SO_SaleOrderDetailTable.VATAMOUNT_FLD].ToString());
				else decVatAmount = 0;

				if(dRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] != DBNull.Value)
					decExpAmount = decimal.Parse(dRow[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].ToString());
				else decExpAmount = 0;
				
				if(dRow[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] != DBNull.Value)
					decSpecAmount = decimal.Parse(dRow[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD].ToString());
				else decSpecAmount = 0;

				if(dRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] == DBNull.Value)
				{
					dRow[SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] =  
						pdecAmount + decVatAmount + decExpAmount + decSpecAmount;
				}
				else
				{
					dRow[SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] =  
						(pdecAmount - decimal.Parse(dRow[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString())) 
						+ decVatAmount + decExpAmount + decSpecAmount;
				}		
				
			}
			catch{}
		}

	} 
}
