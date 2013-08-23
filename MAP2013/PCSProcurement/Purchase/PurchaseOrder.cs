using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.C1Report;
using C1.Win.C1TrueDBGrid;
using PCSComMaterials.Plan.DS;
using PCSComProcurement.Purchase.BO;
using PCSComProcurement.Purchase.DS;
using PCSComProduct.Items.BO;
using PCSComProduct.Items.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;
using AddNewModeEnum = C1.Win.C1TrueDBGrid.AddNewModeEnum;
using AlignHorzEnum = C1.Win.C1TrueDBGrid.AlignHorzEnum;
using BeforeColEditEventArgs = C1.Win.C1TrueDBGrid.BeforeColEditEventArgs;
using BeforeColUpdateEventArgs = C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs;
using C1DataColumn = C1.Win.C1TrueDBGrid.C1DataColumn;
using C1DisplayColumn = C1.Win.C1TrueDBGrid.C1DisplayColumn;
using CancelEventArgs = C1.Win.C1TrueDBGrid.CancelEventArgs;
using ColEventArgs = C1.Win.C1TrueDBGrid.ColEventArgs;
using PresentationEnum = C1.Win.C1TrueDBGrid.PresentationEnum;
using RowColChangeEventArgs = C1.Win.C1TrueDBGrid.RowColChangeEventArgs;

namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for PurchaseOrder.
	/// </summary>
	public class PurchaseOrder : Form
	{
		#region My variable
		public const string THIS = "PCSProcurement.Purchase.PurchaseOrder";
		const int DECIMALS = 2;
		const int ONE = 1;
		const int FORTY = 40;
		//		const string FORMATED_DECIMAL = "999999999990.99";
		
		//		const string FORMATED_ORDERNO = "YYYYMMDD0000";
		const decimal ONE_HUNDRED = 100;
		private DataSet dstPODetail = new DataSet();
		private decimal decDiscountRate;
		private Hashtable htbCriteria;
		private DataRowView drwResult = null;
		private const string V_VENDORCUSTOMER = "V_VendorCustomer";
		private string strOldTextBoxValue;
		private const string VENDOR = "Vendor";
		const string GRID_COL_NAME ="NAME";
		const string GRID_COL_CAPTION = "CAPTION";
		const string GRID_COL_WIDTH = "WIDTH";
		const string GRID_COL_VISIBLE = "VISIBLE";
		private DataTable dtbGridDesign;
		private decimal decAmount = 0;
		
		private PO_PurchaseOrderMasterVO voPOMaster = new PO_PurchaseOrderMasterVO();
		private bool blnIsChangedGrid = false;
		private EnumAction mFormAction = EnumAction.Default;
		public EnumAction FormAction
		{
			set{mFormAction = value;}
			get{return mFormAction;}
		}

		#endregion My variable

		private const string ADDITION_CAPTION_FLD = " - Converted from CPO";
		const string TOTAL_DELIVERY_QTY = "TotalDelivery";

		//variables for CPO
		private POFormState mPOFormState = POFormState.Normal;
		private DataView dtwCPOs;
		private DataSet  dstDelivery;
		private bool blnHasError = true;
		private string strCheckTextBoxModified = string.Empty;

		#region Generate code automatic

		private Button btnOrderNo;
		private TextBox txtOrderNo;
		private TextBox txtVendor;
		private TextBox txtVendorName;
		private Button btnVendor;
		private Label lblContact;
		private Label lblCCN;
		private Label lblCurrency;
		private Label lblVendor;
		private Label lblOrderNo;
		private Label lblVendorLoc;
		private Label lblOrderDate;
		private TextBox txtVendorSO;
		private Label lblBuyer;
		private Button btnVendorLoc;
		private TextBox txtContact;
		private Button btnContact;
		private TextBox txtBuyer;
		private Button btnBuyer;
		private TextBox txtShipToLoc;
		private TextBox txtInvToLoc;
		private TextBox txtPOType;
		private TextBox txtPayTerms;
		private TextBox txtCarrier;
		private TextBox txtPause;
		private TextBox txtCurrency;
		private Button btnCurrency;
		private C1Combo cboCCN;
		private CheckBox chkVAT;
		private CheckBox chkSpecialTax;
		private Label lblVendorSQ;
		private Label lblPause;
		private Label lblCarrier;
		private Label lblDistTerms;
		private Label lblPayTerms;
		private Label lblPOType;
		private Label lblDeliveryTerms;
		private Label lblInvToLoc;
		private Label lblShipToLoc;
		private C1TrueDBGrid gridData;
		private Button btnPOType;
		private Button btnDeliveryTerms;
		private Button btnPayTerms;
		private Button btnCarrier;
		private Button btnPause;
		private Label lblTotalImpTax;
		private Label lblTotalDiscount;
		private Label lblSpecTax;
		private Label lblTotalVAT;
		private Label lblTotalAmount;
		private Label lblTotalNetAmount;
		private ComboBox cboPriority;
		private Label lblPriority;
		private Label lblHeader;
		private Button btnEdit;
		private Button btnClose;
		private Button btnHelp;
		private Button btnDelete;
		private Button btnSave;
		private Button btnAdd;
		private Button btnPrint;
		private C1DateEdit cboOrderDate;
		private Button btnAdvance;
		private Label lblExchRate;
		private Button btnShipToLoc;
		private Button btnInvToLoc;
		private Button btnDeliverySchedule;
		private Button btnAdditionCharges;
		private CheckBox chkImportTax;
		private TextBox txtDiscTerms;
		private Button btnDiscTerms;
		private TextBox txtDeliveryTerms;
		private TextBox txtVendorLoc;
		private CheckBox chkRecCompleted;
		private C1NumericEdit txtExchRate;
		private C1DateEdit cboRequiredDate;
		private Button btnVendorName;
		private C1NumericEdit txtTotalVAT;
		private C1NumericEdit txtTotalSpecTax;
		private C1NumericEdit txtTotalDiscount;
		private C1NumericEdit txtTotalImpTax;
		private C1NumericEdit txtTotalAmount;
		private C1NumericEdit txtTotalNetAmount;
		private Label lblRevision;
		private C1Report rptPOSheet;
		private GroupBox grpHeader;
		private Label lblReportTitle;
		private Button btnPrintConfiguration;
		private CheckBox chkApproved;
		private C1NumericEdit txtRevision;
		private Label lblTotalDeliveryScheduleQuantity;
		private TextBox txtMakerLoc;
		private TextBox txtMakerCode;
		private TextBox txtMakerName;
		private Button btnMakerLoc;
		private Button btnMakerCode;
		private Label lblMakerCode;
		private Label lblMakerLoc;
		private Label lblDeliveryTime;
		private C1NumericEdit txtDeliveryTime;
		private TextBox txtPricingType;
		private Button btnPricingType;
		private Label lblPricingType;
		private Label label1;
		private TextBox txtReferenceNo;
		private Label lblReferenceNo;
		private Button btnImport;
		private OleDbCommand ocmdExcelSelect;
		private OleDbConnection oconExcelFile;
		private OpenFileDialog dlgOpenImpFile;
		private ContextMenu ctxmnuImport;
		private MenuItem mnuImportNew;
		private MenuItem mnuImportUpdate;
		private OleDbDataAdapter odadExcelFile;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public PurchaseOrder()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();			
			//this.TextBoxTagChanged += new TextBoxTagChanged(PurchaseOrder_TextBoxTagChanged);
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
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseOrder));
            this.btnAdvance = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblTotalImpTax = new System.Windows.Forms.Label();
            this.lblTotalDiscount = new System.Windows.Forms.Label();
            this.lblSpecTax = new System.Windows.Forms.Label();
            this.lblTotalVAT = new System.Windows.Forms.Label();
            this.btnDeliverySchedule = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnAdditionCharges = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnOrderNo = new System.Windows.Forms.Button();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.txtVendor = new System.Windows.Forms.TextBox();
            this.txtVendorName = new System.Windows.Forms.TextBox();
            this.btnVendor = new System.Windows.Forms.Button();
            this.lblContact = new System.Windows.Forms.Label();
            this.cboCCN = new C1.Win.C1List.C1Combo();
            this.lblCCN = new System.Windows.Forms.Label();
            this.lblExchRate = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblVendor = new System.Windows.Forms.Label();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.lblVendorLoc = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.chkVAT = new System.Windows.Forms.CheckBox();
            this.chkImportTax = new System.Windows.Forms.CheckBox();
            this.chkSpecialTax = new System.Windows.Forms.CheckBox();
            this.txtVendorSO = new System.Windows.Forms.TextBox();
            this.lblVendorSQ = new System.Windows.Forms.Label();
            this.lblPause = new System.Windows.Forms.Label();
            this.chkRecCompleted = new System.Windows.Forms.CheckBox();
            this.lblCarrier = new System.Windows.Forms.Label();
            this.lblDistTerms = new System.Windows.Forms.Label();
            this.lblPayTerms = new System.Windows.Forms.Label();
            this.lblPOType = new System.Windows.Forms.Label();
            this.lblDeliveryTerms = new System.Windows.Forms.Label();
            this.lblInvToLoc = new System.Windows.Forms.Label();
            this.lblShipToLoc = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalNetAmount = new System.Windows.Forms.Label();
            this.lblBuyer = new System.Windows.Forms.Label();
            this.gridData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.txtVendorLoc = new System.Windows.Forms.TextBox();
            this.btnVendorLoc = new System.Windows.Forms.Button();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.btnContact = new System.Windows.Forms.Button();
            this.txtBuyer = new System.Windows.Forms.TextBox();
            this.btnBuyer = new System.Windows.Forms.Button();
            this.txtShipToLoc = new System.Windows.Forms.TextBox();
            this.btnShipToLoc = new System.Windows.Forms.Button();
            this.txtInvToLoc = new System.Windows.Forms.TextBox();
            this.btnInvToLoc = new System.Windows.Forms.Button();
            this.txtPOType = new System.Windows.Forms.TextBox();
            this.btnPOType = new System.Windows.Forms.Button();
            this.txtDiscTerms = new System.Windows.Forms.TextBox();
            this.btnDiscTerms = new System.Windows.Forms.Button();
            this.txtDeliveryTerms = new System.Windows.Forms.TextBox();
            this.btnDeliveryTerms = new System.Windows.Forms.Button();
            this.txtPayTerms = new System.Windows.Forms.TextBox();
            this.btnPayTerms = new System.Windows.Forms.Button();
            this.txtCarrier = new System.Windows.Forms.TextBox();
            this.btnCarrier = new System.Windows.Forms.Button();
            this.txtPause = new System.Windows.Forms.TextBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.txtCurrency = new System.Windows.Forms.TextBox();
            this.btnCurrency = new System.Windows.Forms.Button();
            this.cboPriority = new System.Windows.Forms.ComboBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.cboOrderDate = new C1.Win.C1Input.C1DateEdit();
            this.txtExchRate = new C1.Win.C1Input.C1NumericEdit();
            this.btnVendorName = new System.Windows.Forms.Button();
            this.cboRequiredDate = new C1.Win.C1Input.C1DateEdit();
            this.txtTotalVAT = new C1.Win.C1Input.C1NumericEdit();
            this.txtTotalSpecTax = new C1.Win.C1Input.C1NumericEdit();
            this.txtTotalDiscount = new C1.Win.C1Input.C1NumericEdit();
            this.txtTotalImpTax = new C1.Win.C1Input.C1NumericEdit();
            this.txtTotalAmount = new C1.Win.C1Input.C1NumericEdit();
            this.txtTotalNetAmount = new C1.Win.C1Input.C1NumericEdit();
            this.lblRevision = new System.Windows.Forms.Label();
            this.rptPOSheet = new C1.C1Report.C1Report();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.btnPrintConfiguration = new System.Windows.Forms.Button();
            this.chkApproved = new System.Windows.Forms.CheckBox();
            this.txtRevision = new C1.Win.C1Input.C1NumericEdit();
            this.lblTotalDeliveryScheduleQuantity = new System.Windows.Forms.Label();
            this.txtMakerLoc = new System.Windows.Forms.TextBox();
            this.txtMakerCode = new System.Windows.Forms.TextBox();
            this.txtMakerName = new System.Windows.Forms.TextBox();
            this.btnMakerLoc = new System.Windows.Forms.Button();
            this.btnMakerCode = new System.Windows.Forms.Button();
            this.lblMakerCode = new System.Windows.Forms.Label();
            this.lblMakerLoc = new System.Windows.Forms.Label();
            this.txtDeliveryTime = new C1.Win.C1Input.C1NumericEdit();
            this.lblDeliveryTime = new System.Windows.Forms.Label();
            this.txtPricingType = new System.Windows.Forms.TextBox();
            this.btnPricingType = new System.Windows.Forms.Button();
            this.lblPricingType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.ocmdExcelSelect = new System.Data.OleDb.OleDbCommand();
            this.oconExcelFile = new System.Data.OleDb.OleDbConnection();
            this.dlgOpenImpFile = new System.Windows.Forms.OpenFileDialog();
            this.ctxmnuImport = new System.Windows.Forms.ContextMenu();
            this.mnuImportNew = new System.Windows.Forms.MenuItem();
            this.mnuImportUpdate = new System.Windows.Forms.MenuItem();
            this.odadExcelFile = new System.Data.OleDb.OleDbDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrderDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRequiredDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalVAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalSpecTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalImpTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalNetAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptPOSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRevision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryTime)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdvance
            // 
            this.btnAdvance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdvance.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdvance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdvance.Location = new System.Drawing.Point(557, 429);
            this.btnAdvance.Name = "btnAdvance";
            this.btnAdvance.Size = new System.Drawing.Size(34, 20);
            this.btnAdvance.TabIndex = 88;
            this.btnAdvance.Text = "&>>";
            this.btnAdvance.Visible = false;
            this.btnAdvance.Click += new System.EventHandler(this.btnAdvance_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHeader.Location = new System.Drawing.Point(5, 182);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(167, 20);
            this.lblHeader.TabIndex = 49;
            this.lblHeader.Text = "PO Header Addition Information";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalImpTax
            // 
            this.lblTotalImpTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalImpTax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalImpTax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalImpTax.Location = new System.Drawing.Point(184, 451);
            this.lblTotalImpTax.Name = "lblTotalImpTax";
            this.lblTotalImpTax.Size = new System.Drawing.Size(78, 20);
            this.lblTotalImpTax.TabIndex = 82;
            this.lblTotalImpTax.Text = "Total Imp. Tax";
            this.lblTotalImpTax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalDiscount
            // 
            this.lblTotalDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalDiscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalDiscount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalDiscount.Location = new System.Drawing.Point(6, 429);
            this.lblTotalDiscount.Name = "lblTotalDiscount";
            this.lblTotalDiscount.Size = new System.Drawing.Size(80, 23);
            this.lblTotalDiscount.TabIndex = 76;
            this.lblTotalDiscount.Text = "Total Discount";
            this.lblTotalDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSpecTax
            // 
            this.lblSpecTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSpecTax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSpecTax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSpecTax.Location = new System.Drawing.Point(6, 451);
            this.lblSpecTax.Name = "lblSpecTax";
            this.lblSpecTax.Size = new System.Drawing.Size(80, 20);
            this.lblSpecTax.TabIndex = 78;
            this.lblSpecTax.Text = "Total Spec Tax";
            this.lblSpecTax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalVAT
            // 
            this.lblTotalVAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalVAT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalVAT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalVAT.Location = new System.Drawing.Point(184, 429);
            this.lblTotalVAT.Name = "lblTotalVAT";
            this.lblTotalVAT.Size = new System.Drawing.Size(80, 23);
            this.lblTotalVAT.TabIndex = 80;
            this.lblTotalVAT.Text = "Total VAT";
            this.lblTotalVAT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDeliverySchedule
            // 
            this.btnDeliverySchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeliverySchedule.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDeliverySchedule.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDeliverySchedule.Location = new System.Drawing.Point(364, 475);
            this.btnDeliverySchedule.Name = "btnDeliverySchedule";
            this.btnDeliverySchedule.Size = new System.Drawing.Size(63, 23);
            this.btnDeliverySchedule.TabIndex = 95;
            this.btnDeliverySchedule.Text = "De&l. Sch.";
            this.btnDeliverySchedule.Click += new System.EventHandler(this.btnDeliverySchedule_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEdit.Location = new System.Drawing.Point(128, 475);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(60, 23);
            this.btnEdit.TabIndex = 91;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(570, 475);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 98;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(509, 475);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(60, 23);
            this.btnHelp.TabIndex = 97;
            this.btnHelp.Text = "&Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(189, 475);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 23);
            this.btnDelete.TabIndex = 92;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(67, 475);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 90;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(6, 475);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 23);
            this.btnAdd.TabIndex = 89;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAdditionCharges
            // 
            this.btnAdditionCharges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdditionCharges.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdditionCharges.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdditionCharges.Location = new System.Drawing.Point(428, 475);
            this.btnAdditionCharges.Name = "btnAdditionCharges";
            this.btnAdditionCharges.Size = new System.Drawing.Size(80, 23);
            this.btnAdditionCharges.TabIndex = 96;
            this.btnAdditionCharges.Text = "Add. Char&ges";
            this.btnAdditionCharges.Click += new System.EventHandler(this.btnAdditionCharges_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrint.Location = new System.Drawing.Point(250, 475);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(60, 23);
            this.btnPrint.TabIndex = 93;
            this.btnPrint.Text = "&Print";
            // 
            // btnOrderNo
            // 
            this.btnOrderNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOrderNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOrderNo.Location = new System.Drawing.Point(207, 26);
            this.btnOrderNo.Name = "btnOrderNo";
            this.btnOrderNo.Size = new System.Drawing.Size(24, 20);
            this.btnOrderNo.TabIndex = 6;
            this.btnOrderNo.Text = "...";
            this.btnOrderNo.Click += new System.EventHandler(this.btnOrderNo_Click);
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Location = new System.Drawing.Point(80, 26);
            this.txtOrderNo.MaxLength = 200;
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(126, 20);
            this.txtOrderNo.TabIndex = 5;
            this.txtOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderNo_KeyDown);
            this.txtOrderNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtOrderNo_Validating);
            // 
            // txtVendor
            // 
            this.txtVendor.Location = new System.Drawing.Point(80, 72);
            this.txtVendor.Name = "txtVendor";
            this.txtVendor.Size = new System.Drawing.Size(96, 20);
            this.txtVendor.TabIndex = 15;
            this.txtVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendor_KeyDown);
            this.txtVendor.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtVendor.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendor_Validating);
            // 
            // txtVendorName
            // 
            this.txtVendorName.Location = new System.Drawing.Point(205, 72);
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.Size = new System.Drawing.Size(230, 20);
            this.txtVendorName.TabIndex = 17;
            this.txtVendorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendorName_KeyDown);
            this.txtVendorName.Leave += new System.EventHandler(this.txtVendorName_Leave);
            this.txtVendorName.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtVendorName.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendorName_Validating);
            // 
            // btnVendor
            // 
            this.btnVendor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnVendor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnVendor.Location = new System.Drawing.Point(178, 72);
            this.btnVendor.Name = "btnVendor";
            this.btnVendor.Size = new System.Drawing.Size(24, 20);
            this.btnVendor.TabIndex = 16;
            this.btnVendor.Text = "...";
            this.btnVendor.Click += new System.EventHandler(this.btnVendor_Click);
            // 
            // lblContact
            // 
            this.lblContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblContact.ForeColor = System.Drawing.Color.Black;
            this.lblContact.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblContact.Location = new System.Drawing.Point(236, 94);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(52, 20);
            this.lblContact.TabIndex = 22;
            this.lblContact.Text = "Contact";
            this.lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboCCN
            // 
            this.cboCCN.AddItemSeparator = ';';
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
            this.cboCCN.Location = new System.Drawing.Point(554, 4);
            this.cboCCN.MatchEntryTimeout = ((long)(2000));
            this.cboCCN.MaxDropDownItems = ((short)(5));
            this.cboCCN.MaxLength = 32767;
            this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCCN.Name = "cboCCN";
            this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCCN.Size = new System.Drawing.Size(80, 21);
            this.cboCCN.TabIndex = 1;
            this.cboCCN.PropBag = resources.GetString("cboCCN.PropBag");
            // 
            // lblCCN
            // 
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCCN.Location = new System.Drawing.Point(522, 4);
            this.lblCCN.Name = "lblCCN";
            this.lblCCN.Size = new System.Drawing.Size(30, 20);
            this.lblCCN.TabIndex = 0;
            this.lblCCN.Text = "CCN";
            this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblExchRate
            // 
            this.lblExchRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblExchRate.ForeColor = System.Drawing.Color.Maroon;
            this.lblExchRate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblExchRate.Location = new System.Drawing.Point(313, 26);
            this.lblExchRate.Name = "lblExchRate";
            this.lblExchRate.Size = new System.Drawing.Size(62, 20);
            this.lblExchRate.TabIndex = 12;
            this.lblExchRate.Text = "Exch. Rate";
            this.lblExchRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrency
            // 
            this.lblCurrency.ForeColor = System.Drawing.Color.Maroon;
            this.lblCurrency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCurrency.Location = new System.Drawing.Point(313, 6);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(62, 18);
            this.lblCurrency.TabIndex = 9;
            this.lblCurrency.Text = "Currency";
            this.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVendor
            // 
            this.lblVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblVendor.ForeColor = System.Drawing.Color.Maroon;
            this.lblVendor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVendor.Location = new System.Drawing.Point(5, 72);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(79, 18);
            this.lblVendor.TabIndex = 14;
            this.lblVendor.Text = "Vendor";
            this.lblVendor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOrderNo.ForeColor = System.Drawing.Color.Maroon;
            this.lblOrderNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOrderNo.Location = new System.Drawing.Point(5, 26);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(79, 20);
            this.lblOrderNo.TabIndex = 4;
            this.lblOrderNo.Text = "Order No.";
            this.lblOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVendorLoc
            // 
            this.lblVendorLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblVendorLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblVendorLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVendorLoc.Location = new System.Drawing.Point(5, 92);
            this.lblVendorLoc.Name = "lblVendorLoc";
            this.lblVendorLoc.Size = new System.Drawing.Size(79, 20);
            this.lblVendorLoc.TabIndex = 19;
            this.lblVendorLoc.Text = "Vendor Loc.";
            this.lblVendorLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOrderDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblOrderDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOrderDate.Location = new System.Drawing.Point(5, 4);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(79, 20);
            this.lblOrderDate.TabIndex = 2;
            this.lblOrderDate.Text = "Trans. Date";
            this.lblOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkVAT
            // 
            this.chkVAT.ForeColor = System.Drawing.Color.Black;
            this.chkVAT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkVAT.Location = new System.Drawing.Point(522, 29);
            this.chkVAT.Name = "chkVAT";
            this.chkVAT.Size = new System.Drawing.Size(82, 19);
            this.chkVAT.TabIndex = 40;
            this.chkVAT.Text = "VAT";
            this.chkVAT.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.chkVAT.CheckedChanged += new System.EventHandler(this.chkVAT_CheckedChanged);
            // 
            // chkImportTax
            // 
            this.chkImportTax.ForeColor = System.Drawing.Color.Black;
            this.chkImportTax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkImportTax.Location = new System.Drawing.Point(522, 51);
            this.chkImportTax.Name = "chkImportTax";
            this.chkImportTax.Size = new System.Drawing.Size(82, 19);
            this.chkImportTax.TabIndex = 41;
            this.chkImportTax.Text = "Import Tax";
            this.chkImportTax.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.chkImportTax.CheckedChanged += new System.EventHandler(this.chkImportTax_CheckedChanged);
            // 
            // chkSpecialTax
            // 
            this.chkSpecialTax.ForeColor = System.Drawing.Color.Black;
            this.chkSpecialTax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkSpecialTax.Location = new System.Drawing.Point(522, 96);
            this.chkSpecialTax.Name = "chkSpecialTax";
            this.chkSpecialTax.Size = new System.Drawing.Size(82, 19);
            this.chkSpecialTax.TabIndex = 42;
            this.chkSpecialTax.Text = "Special Tax";
            this.chkSpecialTax.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.chkSpecialTax.CheckedChanged += new System.EventHandler(this.chkSpecialTax_CheckedChanged);
            // 
            // txtVendorSO
            // 
            this.txtVendorSO.Location = new System.Drawing.Point(522, 246);
            this.txtVendorSO.MaxLength = 20;
            this.txtVendorSO.Name = "txtVendorSO";
            this.txtVendorSO.Size = new System.Drawing.Size(108, 20);
            this.txtVendorSO.TabIndex = 74;
            this.txtVendorSO.Leave += new System.EventHandler(this.OnLeaveControl);
            this.txtVendorSO.Enter += new System.EventHandler(this.txtVendorName_Enter);
            // 
            // lblVendorSQ
            // 
            this.lblVendorSQ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblVendorSQ.ForeColor = System.Drawing.Color.Black;
            this.lblVendorSQ.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVendorSQ.Location = new System.Drawing.Point(460, 246);
            this.lblVendorSQ.Name = "lblVendorSQ";
            this.lblVendorSQ.Size = new System.Drawing.Size(61, 20);
            this.lblVendorSQ.TabIndex = 73;
            this.lblVendorSQ.Text = "Vendor SO";
            this.lblVendorSQ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPause
            // 
            this.lblPause.ForeColor = System.Drawing.Color.Black;
            this.lblPause.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPause.Location = new System.Drawing.Point(460, 160);
            this.lblPause.Name = "lblPause";
            this.lblPause.Size = new System.Drawing.Size(61, 19);
            this.lblPause.TabIndex = 46;
            this.lblPause.Text = "Pause";
            this.lblPause.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkRecCompleted
            // 
            this.chkRecCompleted.Enabled = false;
            this.chkRecCompleted.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkRecCompleted.ForeColor = System.Drawing.Color.Black;
            this.chkRecCompleted.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkRecCompleted.Location = new System.Drawing.Point(522, 224);
            this.chkRecCompleted.Name = "chkRecCompleted";
            this.chkRecCompleted.Size = new System.Drawing.Size(102, 20);
            this.chkRecCompleted.TabIndex = 72;
            this.chkRecCompleted.Text = "Rec. Completed";
            // 
            // lblCarrier
            // 
            this.lblCarrier.ForeColor = System.Drawing.Color.Black;
            this.lblCarrier.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCarrier.Location = new System.Drawing.Point(208, 224);
            this.lblCarrier.Name = "lblCarrier";
            this.lblCarrier.Size = new System.Drawing.Size(84, 20);
            this.lblCarrier.TabIndex = 63;
            this.lblCarrier.Text = "Carrier";
            this.lblCarrier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDistTerms
            // 
            this.lblDistTerms.ForeColor = System.Drawing.Color.Black;
            this.lblDistTerms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDistTerms.Location = new System.Drawing.Point(406, 106);
            this.lblDistTerms.Name = "lblDistTerms";
            this.lblDistTerms.Size = new System.Drawing.Size(84, 19);
            this.lblDistTerms.TabIndex = 37;
            this.lblDistTerms.Text = "Disc. Terms";
            this.lblDistTerms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPayTerms
            // 
            this.lblPayTerms.ForeColor = System.Drawing.Color.Black;
            this.lblPayTerms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPayTerms.Location = new System.Drawing.Point(208, 202);
            this.lblPayTerms.Name = "lblPayTerms";
            this.lblPayTerms.Size = new System.Drawing.Size(84, 20);
            this.lblPayTerms.TabIndex = 60;
            this.lblPayTerms.Text = "Payment Terms";
            this.lblPayTerms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPOType
            // 
            this.lblPOType.ForeColor = System.Drawing.Color.Maroon;
            this.lblPOType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPOType.Location = new System.Drawing.Point(5, 246);
            this.lblPOType.Name = "lblPOType";
            this.lblPOType.Size = new System.Drawing.Size(76, 19);
            this.lblPOType.TabIndex = 57;
            this.lblPOType.Text = "PO Type";
            this.lblPOType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDeliveryTerms
            // 
            this.lblDeliveryTerms.ForeColor = System.Drawing.Color.Black;
            this.lblDeliveryTerms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDeliveryTerms.Location = new System.Drawing.Point(208, 246);
            this.lblDeliveryTerms.Name = "lblDeliveryTerms";
            this.lblDeliveryTerms.Size = new System.Drawing.Size(84, 19);
            this.lblDeliveryTerms.TabIndex = 66;
            this.lblDeliveryTerms.Text = "Delivery Terms";
            this.lblDeliveryTerms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInvToLoc
            // 
            this.lblInvToLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblInvToLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblInvToLoc.Location = new System.Drawing.Point(5, 224);
            this.lblInvToLoc.Name = "lblInvToLoc";
            this.lblInvToLoc.Size = new System.Drawing.Size(76, 20);
            this.lblInvToLoc.TabIndex = 54;
            this.lblInvToLoc.Text = "Inv. To Loc";
            this.lblInvToLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShipToLoc
            // 
            this.lblShipToLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblShipToLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblShipToLoc.Location = new System.Drawing.Point(5, 202);
            this.lblShipToLoc.Name = "lblShipToLoc";
            this.lblShipToLoc.Size = new System.Drawing.Size(76, 20);
            this.lblShipToLoc.TabIndex = 51;
            this.lblShipToLoc.Text = "Ship To Loc";
            this.lblShipToLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalAmount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalAmount.Location = new System.Drawing.Point(360, 429);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(92, 23);
            this.lblTotalAmount.TabIndex = 84;
            this.lblTotalAmount.Text = "Total Amount";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalNetAmount
            // 
            this.lblTotalNetAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalNetAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalNetAmount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalNetAmount.Location = new System.Drawing.Point(360, 451);
            this.lblTotalNetAmount.Name = "lblTotalNetAmount";
            this.lblTotalNetAmount.Size = new System.Drawing.Size(92, 20);
            this.lblTotalNetAmount.TabIndex = 86;
            this.lblTotalNetAmount.Text = "Total Net Amount";
            this.lblTotalNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBuyer
            // 
            this.lblBuyer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBuyer.ForeColor = System.Drawing.Color.Black;
            this.lblBuyer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBuyer.Location = new System.Drawing.Point(6, 158);
            this.lblBuyer.Name = "lblBuyer";
            this.lblBuyer.Size = new System.Drawing.Size(76, 20);
            this.lblBuyer.TabIndex = 34;
            this.lblBuyer.Text = "Buyer";
            this.lblBuyer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridData
            // 
            this.gridData.AllowDelete = true;
            this.gridData.AllowSort = false;
            this.gridData.AllowUpdate = false;
            this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData.GroupByCaption = "Drag a column header here to group by that column";
            this.gridData.Images.Add(((System.Drawing.Image)(resources.GetObject("gridData.Images"))));
            this.gridData.Location = new System.Drawing.Point(6, 270);
            this.gridData.Name = "gridData";
            this.gridData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.gridData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.gridData.PreviewInfo.ZoomFactor = 75;
            this.gridData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("gridData.PrintInfo.PageSettings")));
            this.gridData.Size = new System.Drawing.Size(624, 154);
            this.gridData.TabIndex = 75;
            this.gridData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.gridData_BeforeColEdit);
            this.gridData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridData_ButtonClick);
            this.gridData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.gridData_BeforeColUpdate);
            this.gridData.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.gridData_BeforeDelete);
            this.gridData.OnAddNew += new System.EventHandler(this.gridData_OnAddNew);
            this.gridData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridData_KeyDown);
            this.gridData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridData_AfterColUpdate);
            this.gridData.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.gridData_RowColChange);
            this.gridData.Click += new System.EventHandler(this.gridData_Click);
            this.gridData.PropBag = resources.GetString("gridData.PropBag");
            // 
            // txtVendorLoc
            // 
            this.txtVendorLoc.Location = new System.Drawing.Point(80, 94);
            this.txtVendorLoc.Name = "txtVendorLoc";
            this.txtVendorLoc.Size = new System.Drawing.Size(96, 20);
            this.txtVendorLoc.TabIndex = 20;
            this.txtVendorLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendorLoc_KeyDown);
            this.txtVendorLoc.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtVendorLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendorLoc_Validating);
            // 
            // btnVendorLoc
            // 
            this.btnVendorLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnVendorLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnVendorLoc.Location = new System.Drawing.Point(178, 94);
            this.btnVendorLoc.Name = "btnVendorLoc";
            this.btnVendorLoc.Size = new System.Drawing.Size(24, 20);
            this.btnVendorLoc.TabIndex = 21;
            this.btnVendorLoc.Text = "...";
            this.btnVendorLoc.Click += new System.EventHandler(this.btnVendorLoc_Click);
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(283, 94);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(152, 20);
            this.txtContact.TabIndex = 23;
            this.txtContact.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContact_KeyDown);
            this.txtContact.Leave += new System.EventHandler(this.txtContact_Leave);
            this.txtContact.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtContact.Validating += new System.ComponentModel.CancelEventHandler(this.txtContact_Validating);
            // 
            // btnContact
            // 
            this.btnContact.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnContact.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnContact.Location = new System.Drawing.Point(437, 94);
            this.btnContact.Name = "btnContact";
            this.btnContact.Size = new System.Drawing.Size(24, 20);
            this.btnContact.TabIndex = 24;
            this.btnContact.Text = "...";
            this.btnContact.Click += new System.EventHandler(this.btnContact_Click);
            // 
            // txtBuyer
            // 
            this.txtBuyer.Location = new System.Drawing.Point(80, 160);
            this.txtBuyer.Name = "txtBuyer";
            this.txtBuyer.Size = new System.Drawing.Size(96, 20);
            this.txtBuyer.TabIndex = 35;
            this.txtBuyer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuyer_KeyDown);
            this.txtBuyer.Leave += new System.EventHandler(this.txtBuyer_Leave);
            this.txtBuyer.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtBuyer.Validating += new System.ComponentModel.CancelEventHandler(this.txtBuyer_Validating);
            // 
            // btnBuyer
            // 
            this.btnBuyer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBuyer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBuyer.Location = new System.Drawing.Point(178, 160);
            this.btnBuyer.Name = "btnBuyer";
            this.btnBuyer.Size = new System.Drawing.Size(24, 20);
            this.btnBuyer.TabIndex = 36;
            this.btnBuyer.Text = "...";
            this.btnBuyer.Click += new System.EventHandler(this.btnBuyer_Click);
            // 
            // txtShipToLoc
            // 
            this.txtShipToLoc.Location = new System.Drawing.Point(81, 202);
            this.txtShipToLoc.Name = "txtShipToLoc";
            this.txtShipToLoc.Size = new System.Drawing.Size(95, 20);
            this.txtShipToLoc.TabIndex = 52;
            this.txtShipToLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShipToLoc_KeyDown);
            this.txtShipToLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtShipToLoc_Validating);
            // 
            // btnShipToLoc
            // 
            this.btnShipToLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShipToLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnShipToLoc.Location = new System.Drawing.Point(178, 202);
            this.btnShipToLoc.Name = "btnShipToLoc";
            this.btnShipToLoc.Size = new System.Drawing.Size(24, 20);
            this.btnShipToLoc.TabIndex = 53;
            this.btnShipToLoc.Text = "...";
            this.btnShipToLoc.Click += new System.EventHandler(this.btnShipToLoc_Click);
            // 
            // txtInvToLoc
            // 
            this.txtInvToLoc.Location = new System.Drawing.Point(81, 224);
            this.txtInvToLoc.Name = "txtInvToLoc";
            this.txtInvToLoc.Size = new System.Drawing.Size(95, 20);
            this.txtInvToLoc.TabIndex = 55;
            this.txtInvToLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvToLoc_KeyDown);
            this.txtInvToLoc.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtInvToLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtInvToLoc_Validating);
            // 
            // btnInvToLoc
            // 
            this.btnInvToLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnInvToLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInvToLoc.Location = new System.Drawing.Point(178, 224);
            this.btnInvToLoc.Name = "btnInvToLoc";
            this.btnInvToLoc.Size = new System.Drawing.Size(24, 20);
            this.btnInvToLoc.TabIndex = 56;
            this.btnInvToLoc.Text = "...";
            this.btnInvToLoc.Click += new System.EventHandler(this.btnInvToLoc_Click);
            // 
            // txtPOType
            // 
            this.txtPOType.Location = new System.Drawing.Point(81, 246);
            this.txtPOType.Name = "txtPOType";
            this.txtPOType.Size = new System.Drawing.Size(95, 20);
            this.txtPOType.TabIndex = 58;
            this.txtPOType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPOType_KeyDown);
            this.txtPOType.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtPOType.Validating += new System.ComponentModel.CancelEventHandler(this.txtPOType_Validating);
            // 
            // btnPOType
            // 
            this.btnPOType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPOType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPOType.Location = new System.Drawing.Point(178, 246);
            this.btnPOType.Name = "btnPOType";
            this.btnPOType.Size = new System.Drawing.Size(24, 20);
            this.btnPOType.TabIndex = 59;
            this.btnPOType.Text = "...";
            this.btnPOType.Click += new System.EventHandler(this.btnPOType_Click);
            // 
            // txtDiscTerms
            // 
            this.txtDiscTerms.Location = new System.Drawing.Point(494, 106);
            this.txtDiscTerms.Name = "txtDiscTerms";
            this.txtDiscTerms.Size = new System.Drawing.Size(112, 20);
            this.txtDiscTerms.TabIndex = 38;
            this.txtDiscTerms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscTerms_KeyDown);
            this.txtDiscTerms.Leave += new System.EventHandler(this.txtDiscTerms_Leave);
            this.txtDiscTerms.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtDiscTerms.Validating += new System.ComponentModel.CancelEventHandler(this.txtDiscTerms_Validating);
            // 
            // btnDiscTerms
            // 
            this.btnDiscTerms.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDiscTerms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDiscTerms.Location = new System.Drawing.Point(608, 106);
            this.btnDiscTerms.Name = "btnDiscTerms";
            this.btnDiscTerms.Size = new System.Drawing.Size(24, 20);
            this.btnDiscTerms.TabIndex = 39;
            this.btnDiscTerms.Text = "...";
            this.btnDiscTerms.Click += new System.EventHandler(this.btnDiscTerms_Click);
            // 
            // txtDeliveryTerms
            // 
            this.txtDeliveryTerms.Location = new System.Drawing.Point(293, 246);
            this.txtDeliveryTerms.Name = "txtDeliveryTerms";
            this.txtDeliveryTerms.Size = new System.Drawing.Size(90, 20);
            this.txtDeliveryTerms.TabIndex = 67;
            this.txtDeliveryTerms.Text = "btnDeliveryTerms";
            this.txtDeliveryTerms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeliveryTerms_KeyDown);
            this.txtDeliveryTerms.Leave += new System.EventHandler(this.txtDeliveryTerms_Leave);
            this.txtDeliveryTerms.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtDeliveryTerms.Validating += new System.ComponentModel.CancelEventHandler(this.txtDeliveryTerms_Validating);
            // 
            // btnDeliveryTerms
            // 
            this.btnDeliveryTerms.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDeliveryTerms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDeliveryTerms.Location = new System.Drawing.Point(385, 246);
            this.btnDeliveryTerms.Name = "btnDeliveryTerms";
            this.btnDeliveryTerms.Size = new System.Drawing.Size(24, 20);
            this.btnDeliveryTerms.TabIndex = 68;
            this.btnDeliveryTerms.Text = "...";
            this.btnDeliveryTerms.Click += new System.EventHandler(this.btnDeliveryTerms_Click);
            // 
            // txtPayTerms
            // 
            this.txtPayTerms.Location = new System.Drawing.Point(293, 202);
            this.txtPayTerms.Name = "txtPayTerms";
            this.txtPayTerms.Size = new System.Drawing.Size(90, 20);
            this.txtPayTerms.TabIndex = 61;
            this.txtPayTerms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayTerms_KeyDown);
            this.txtPayTerms.Leave += new System.EventHandler(this.txtPayTerms_Leave);
            this.txtPayTerms.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtPayTerms.Validating += new System.ComponentModel.CancelEventHandler(this.txtPayTerms_Validating);
            // 
            // btnPayTerms
            // 
            this.btnPayTerms.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPayTerms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPayTerms.Location = new System.Drawing.Point(385, 202);
            this.btnPayTerms.Name = "btnPayTerms";
            this.btnPayTerms.Size = new System.Drawing.Size(24, 20);
            this.btnPayTerms.TabIndex = 62;
            this.btnPayTerms.Text = "...";
            this.btnPayTerms.Click += new System.EventHandler(this.btnPayTerms_Click);
            // 
            // txtCarrier
            // 
            this.txtCarrier.Location = new System.Drawing.Point(293, 224);
            this.txtCarrier.Name = "txtCarrier";
            this.txtCarrier.Size = new System.Drawing.Size(90, 20);
            this.txtCarrier.TabIndex = 64;
            this.txtCarrier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarrier_KeyDown);
            this.txtCarrier.Leave += new System.EventHandler(this.txtCarrier_Leave);
            this.txtCarrier.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtCarrier.Validating += new System.ComponentModel.CancelEventHandler(this.txtCarrier_Validating);
            // 
            // btnCarrier
            // 
            this.btnCarrier.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCarrier.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCarrier.Location = new System.Drawing.Point(385, 224);
            this.btnCarrier.Name = "btnCarrier";
            this.btnCarrier.Size = new System.Drawing.Size(24, 20);
            this.btnCarrier.TabIndex = 65;
            this.btnCarrier.Text = "...";
            this.btnCarrier.Click += new System.EventHandler(this.btnCarrier_Click);
            // 
            // txtPause
            // 
            this.txtPause.Location = new System.Drawing.Point(522, 160);
            this.txtPause.Name = "txtPause";
            this.txtPause.Size = new System.Drawing.Size(82, 20);
            this.txtPause.TabIndex = 47;
            this.txtPause.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPause_KeyDown);
            this.txtPause.Leave += new System.EventHandler(this.txtPause_Leave);
            this.txtPause.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtPause.Validating += new System.ComponentModel.CancelEventHandler(this.txtPause_Validating);
            // 
            // btnPause
            // 
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPause.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPause.Location = new System.Drawing.Point(606, 160);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(24, 20);
            this.btnPause.TabIndex = 48;
            this.btnPause.Text = "...";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            this.btnPause.Leave += new System.EventHandler(this.btnPause_Leave);
            // 
            // txtCurrency
            // 
            this.txtCurrency.Location = new System.Drawing.Point(375, 4);
            this.txtCurrency.Name = "txtCurrency";
            this.txtCurrency.Size = new System.Drawing.Size(60, 20);
            this.txtCurrency.TabIndex = 10;
            this.txtCurrency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrency_KeyDown);
            this.txtCurrency.Leave += new System.EventHandler(this.txtCurrency_Leave);
            this.txtCurrency.Enter += new System.EventHandler(this.txtVendorName_Enter);
            this.txtCurrency.Validating += new System.ComponentModel.CancelEventHandler(this.txtCurrency_Validating);
            // 
            // btnCurrency
            // 
            this.btnCurrency.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCurrency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCurrency.Location = new System.Drawing.Point(437, 4);
            this.btnCurrency.Name = "btnCurrency";
            this.btnCurrency.Size = new System.Drawing.Size(24, 20);
            this.btnCurrency.TabIndex = 11;
            this.btnCurrency.Text = "...";
            this.btnCurrency.Click += new System.EventHandler(this.btnCurrency_Click);
            // 
            // cboPriority
            // 
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
            this.cboPriority.Location = new System.Drawing.Point(476, 202);
            this.cboPriority.Name = "cboPriority";
            this.cboPriority.Size = new System.Drawing.Size(42, 21);
            this.cboPriority.TabIndex = 70;
            // 
            // lblPriority
            // 
            this.lblPriority.ForeColor = System.Drawing.Color.Black;
            this.lblPriority.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPriority.Location = new System.Drawing.Point(414, 202);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(61, 20);
            this.lblPriority.TabIndex = 69;
            this.lblPriority.Text = "Priority";
            this.lblPriority.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboOrderDate
            // 
            // 
            // 
            // 
            this.cboOrderDate.Calendar.DayNameLength = 1;
            this.cboOrderDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cboOrderDate.CustomFormat = "dd-MM-yyyy";
            this.cboOrderDate.EmptyAsNull = true;
            this.cboOrderDate.ErrorInfo.ShowErrorMessage = false;
            this.cboOrderDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.cboOrderDate.Location = new System.Drawing.Point(80, 4);
            this.cboOrderDate.Name = "cboOrderDate";
            this.cboOrderDate.Size = new System.Drawing.Size(96, 20);
            this.cboOrderDate.TabIndex = 3;
            this.cboOrderDate.Tag = null;
            this.cboOrderDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cboOrderDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.cboOrderDate.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboOrderDate.Enter += new System.EventHandler(this.txtVendorName_Enter);
            // 
            // txtExchRate
            // 
            this.txtExchRate.CustomFormat = "###############,0.00";
            this.txtExchRate.EmptyAsNull = true;
            this.txtExchRate.ErrorInfo.ShowErrorMessage = false;
            this.txtExchRate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtExchRate.Location = new System.Drawing.Point(375, 26);
            this.txtExchRate.Name = "txtExchRate";
            this.txtExchRate.Size = new System.Drawing.Size(60, 20);
            this.txtExchRate.TabIndex = 13;
            this.txtExchRate.Tag = null;
            this.txtExchRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtExchRate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            this.txtExchRate.Enter += new System.EventHandler(this.txtVendorName_Enter);
            // 
            // btnVendorName
            // 
            this.btnVendorName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnVendorName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnVendorName.Location = new System.Drawing.Point(437, 72);
            this.btnVendorName.Name = "btnVendorName";
            this.btnVendorName.Size = new System.Drawing.Size(24, 20);
            this.btnVendorName.TabIndex = 18;
            this.btnVendorName.Text = "...";
            this.btnVendorName.Click += new System.EventHandler(this.btnVendorName_Click);
            // 
            // cboRequiredDate
            // 
            // 
            // 
            // 
            this.cboRequiredDate.Calendar.DayNameLength = 1;
            this.cboRequiredDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cboRequiredDate.CustomFormat = "dd-MM-yyyy";
            this.cboRequiredDate.ErrorInfo.ShowErrorMessage = false;
            this.cboRequiredDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.cboRequiredDate.Location = new System.Drawing.Point(250, 286);
            this.cboRequiredDate.Name = "cboRequiredDate";
            this.cboRequiredDate.Size = new System.Drawing.Size(96, 20);
            this.cboRequiredDate.TabIndex = 219;
            this.cboRequiredDate.TabStop = false;
            this.cboRequiredDate.Tag = null;
            this.cboRequiredDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cboRequiredDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // txtTotalVAT
            // 
            this.txtTotalVAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalVAT.CustomFormat = "###############,0.00";
            this.txtTotalVAT.ErrorInfo.ShowErrorMessage = false;
            this.txtTotalVAT.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtTotalVAT.Location = new System.Drawing.Point(262, 429);
            this.txtTotalVAT.Name = "txtTotalVAT";
            this.txtTotalVAT.ReadOnly = true;
            this.txtTotalVAT.Size = new System.Drawing.Size(98, 20);
            this.txtTotalVAT.TabIndex = 81;
            this.txtTotalVAT.TabStop = false;
            this.txtTotalVAT.Tag = null;
            this.txtTotalVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalVAT.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtTotalSpecTax
            // 
            this.txtTotalSpecTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalSpecTax.CustomFormat = "###############,0.00";
            this.txtTotalSpecTax.ErrorInfo.ShowErrorMessage = false;
            this.txtTotalSpecTax.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtTotalSpecTax.Location = new System.Drawing.Point(86, 451);
            this.txtTotalSpecTax.Name = "txtTotalSpecTax";
            this.txtTotalSpecTax.ReadOnly = true;
            this.txtTotalSpecTax.Size = new System.Drawing.Size(98, 20);
            this.txtTotalSpecTax.TabIndex = 79;
            this.txtTotalSpecTax.TabStop = false;
            this.txtTotalSpecTax.Tag = null;
            this.txtTotalSpecTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalSpecTax.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtTotalDiscount
            // 
            this.txtTotalDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalDiscount.CustomFormat = "###############,0.00";
            this.txtTotalDiscount.ErrorInfo.ShowErrorMessage = false;
            this.txtTotalDiscount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtTotalDiscount.Location = new System.Drawing.Point(86, 429);
            this.txtTotalDiscount.Name = "txtTotalDiscount";
            this.txtTotalDiscount.ReadOnly = true;
            this.txtTotalDiscount.Size = new System.Drawing.Size(98, 20);
            this.txtTotalDiscount.TabIndex = 77;
            this.txtTotalDiscount.TabStop = false;
            this.txtTotalDiscount.Tag = null;
            this.txtTotalDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalDiscount.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtTotalImpTax
            // 
            this.txtTotalImpTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalImpTax.CustomFormat = "###############,0.00";
            this.txtTotalImpTax.ErrorInfo.ShowErrorMessage = false;
            this.txtTotalImpTax.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtTotalImpTax.Location = new System.Drawing.Point(262, 451);
            this.txtTotalImpTax.Name = "txtTotalImpTax";
            this.txtTotalImpTax.ReadOnly = true;
            this.txtTotalImpTax.Size = new System.Drawing.Size(98, 20);
            this.txtTotalImpTax.TabIndex = 83;
            this.txtTotalImpTax.TabStop = false;
            this.txtTotalImpTax.Tag = null;
            this.txtTotalImpTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalImpTax.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalAmount.CustomFormat = "###############,0.00";
            this.txtTotalAmount.ErrorInfo.ShowErrorMessage = false;
            this.txtTotalAmount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtTotalAmount.Location = new System.Drawing.Point(452, 429);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(98, 20);
            this.txtTotalAmount.TabIndex = 85;
            this.txtTotalAmount.TabStop = false;
            this.txtTotalAmount.Tag = null;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalAmount.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtTotalNetAmount
            // 
            this.txtTotalNetAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalNetAmount.CustomFormat = "###############,0.00";
            this.txtTotalNetAmount.ErrorInfo.ShowErrorMessage = false;
            this.txtTotalNetAmount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtTotalNetAmount.Location = new System.Drawing.Point(452, 451);
            this.txtTotalNetAmount.Name = "txtTotalNetAmount";
            this.txtTotalNetAmount.ReadOnly = true;
            this.txtTotalNetAmount.Size = new System.Drawing.Size(98, 20);
            this.txtTotalNetAmount.TabIndex = 87;
            this.txtTotalNetAmount.TabStop = false;
            this.txtTotalNetAmount.Tag = null;
            this.txtTotalNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalNetAmount.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // lblRevision
            // 
            this.lblRevision.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRevision.Location = new System.Drawing.Point(236, 50);
            this.lblRevision.Name = "lblRevision";
            this.lblRevision.Size = new System.Drawing.Size(48, 20);
            this.lblRevision.TabIndex = 7;
            this.lblRevision.Text = "Revision";
            this.lblRevision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rptPOSheet
            // 
            this.rptPOSheet.ReportDefinition = resources.GetString("rptPOSheet.ReportDefinition");
            this.rptPOSheet.ReportName = "Purchase Order Sheet";
            // 
            // grpHeader
            // 
            this.grpHeader.Enabled = false;
            this.grpHeader.Location = new System.Drawing.Point(166, 188);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(463, 7);
            this.grpHeader.TabIndex = 50;
            this.grpHeader.TabStop = false;
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblReportTitle.Location = new System.Drawing.Point(462, 264);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Size = new System.Drawing.Size(140, 14);
            this.lblReportTitle.TabIndex = 235;
            this.lblReportTitle.Text = "PART ORDER SHEET";
            this.lblReportTitle.Visible = false;
            // 
            // btnPrintConfiguration
            // 
            this.btnPrintConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrintConfiguration.Font = new System.Drawing.Font("Wingdings 3", 6F);
            this.btnPrintConfiguration.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrintConfiguration.Location = new System.Drawing.Point(310, 475);
            this.btnPrintConfiguration.Name = "btnPrintConfiguration";
            this.btnPrintConfiguration.Size = new System.Drawing.Size(18, 23);
            this.btnPrintConfiguration.TabIndex = 94;
            this.btnPrintConfiguration.Text = "q";
            // 
            // chkApproved
            // 
            this.chkApproved.Enabled = false;
            this.chkApproved.Location = new System.Drawing.Point(522, 202);
            this.chkApproved.Name = "chkApproved";
            this.chkApproved.Size = new System.Drawing.Size(76, 18);
            this.chkApproved.TabIndex = 71;
            this.chkApproved.TabStop = false;
            this.chkApproved.Text = "Approved";
            // 
            // txtRevision
            // 
            this.txtRevision.DataType = typeof(int);
            this.txtRevision.EmptyAsNull = true;
            this.txtRevision.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtRevision.Location = new System.Drawing.Point(283, 50);
            this.txtRevision.Name = "txtRevision";
            this.txtRevision.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            new C1.Win.C1Input.ValueInterval(((long)(1)), ((long)(255)), true, true)});
            this.txtRevision.Size = new System.Drawing.Size(28, 20);
            this.txtRevision.TabIndex = 8;
            this.txtRevision.Tag = null;
            this.txtRevision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRevision.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // lblTotalDeliveryScheduleQuantity
            // 
            this.lblTotalDeliveryScheduleQuantity.Location = new System.Drawing.Point(532, 400);
            this.lblTotalDeliveryScheduleQuantity.Name = "lblTotalDeliveryScheduleQuantity";
            this.lblTotalDeliveryScheduleQuantity.Size = new System.Drawing.Size(100, 23);
            this.lblTotalDeliveryScheduleQuantity.TabIndex = 237;
            this.lblTotalDeliveryScheduleQuantity.Text = "Total Delivery Schedule Quantity";
            this.lblTotalDeliveryScheduleQuantity.Visible = false;
            // 
            // txtMakerLoc
            // 
            this.txtMakerLoc.Location = new System.Drawing.Point(80, 138);
            this.txtMakerLoc.Name = "txtMakerLoc";
            this.txtMakerLoc.Size = new System.Drawing.Size(96, 20);
            this.txtMakerLoc.TabIndex = 30;
            this.txtMakerLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMakerLoc_KeyDown);
            this.txtMakerLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtMakerLoc_Validating);
            // 
            // txtMakerCode
            // 
            this.txtMakerCode.Location = new System.Drawing.Point(80, 116);
            this.txtMakerCode.Name = "txtMakerCode";
            this.txtMakerCode.Size = new System.Drawing.Size(96, 20);
            this.txtMakerCode.TabIndex = 26;
            this.txtMakerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMakerCode_KeyDown);
            this.txtMakerCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtMakerCode_Validating);
            // 
            // txtMakerName
            // 
            this.txtMakerName.Location = new System.Drawing.Point(206, 116);
            this.txtMakerName.Name = "txtMakerName";
            this.txtMakerName.ReadOnly = true;
            this.txtMakerName.Size = new System.Drawing.Size(230, 20);
            this.txtMakerName.TabIndex = 28;
            // 
            // btnMakerLoc
            // 
            this.btnMakerLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnMakerLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMakerLoc.Location = new System.Drawing.Point(178, 138);
            this.btnMakerLoc.Name = "btnMakerLoc";
            this.btnMakerLoc.Size = new System.Drawing.Size(24, 20);
            this.btnMakerLoc.TabIndex = 31;
            this.btnMakerLoc.Text = "...";
            this.btnMakerLoc.Click += new System.EventHandler(this.btnMakerLoc_Click);
            // 
            // btnMakerCode
            // 
            this.btnMakerCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnMakerCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMakerCode.Location = new System.Drawing.Point(178, 116);
            this.btnMakerCode.Name = "btnMakerCode";
            this.btnMakerCode.Size = new System.Drawing.Size(24, 20);
            this.btnMakerCode.TabIndex = 27;
            this.btnMakerCode.Text = "...";
            this.btnMakerCode.Click += new System.EventHandler(this.btnMakerCode_Click);
            // 
            // lblMakerCode
            // 
            this.lblMakerCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblMakerCode.ForeColor = System.Drawing.Color.Maroon;
            this.lblMakerCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMakerCode.Location = new System.Drawing.Point(6, 116);
            this.lblMakerCode.Name = "lblMakerCode";
            this.lblMakerCode.Size = new System.Drawing.Size(79, 18);
            this.lblMakerCode.TabIndex = 25;
            this.lblMakerCode.Text = "Maker";
            this.lblMakerCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMakerLoc
            // 
            this.lblMakerLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblMakerLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblMakerLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMakerLoc.Location = new System.Drawing.Point(6, 136);
            this.lblMakerLoc.Name = "lblMakerLoc";
            this.lblMakerLoc.Size = new System.Drawing.Size(79, 20);
            this.lblMakerLoc.TabIndex = 29;
            this.lblMakerLoc.Text = "Maker Loc.";
            this.lblMakerLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDeliveryTime
            // 
            this.txtDeliveryTime.CustomFormat = "###############,0.00";
            this.txtDeliveryTime.EmptyAsNull = true;
            this.txtDeliveryTime.ErrorInfo.ShowErrorMessage = false;
            this.txtDeliveryTime.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtDeliveryTime.Location = new System.Drawing.Point(323, 138);
            this.txtDeliveryTime.Name = "txtDeliveryTime";
            this.txtDeliveryTime.Size = new System.Drawing.Size(113, 20);
            this.txtDeliveryTime.TabIndex = 33;
            this.txtDeliveryTime.Tag = null;
            this.txtDeliveryTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDeliveryTime.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // lblDeliveryTime
            // 
            this.lblDeliveryTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDeliveryTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDeliveryTime.Location = new System.Drawing.Point(236, 138);
            this.lblDeliveryTime.Name = "lblDeliveryTime";
            this.lblDeliveryTime.Size = new System.Drawing.Size(84, 20);
            this.lblDeliveryTime.TabIndex = 32;
            this.lblDeliveryTime.Text = "Delivery Days";
            this.lblDeliveryTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPricingType
            // 
            this.txtPricingType.Location = new System.Drawing.Point(323, 160);
            this.txtPricingType.Name = "txtPricingType";
            this.txtPricingType.Size = new System.Drawing.Size(87, 20);
            this.txtPricingType.TabIndex = 35;
            this.txtPricingType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPricingType_KeyDown);
            this.txtPricingType.Validating += new System.ComponentModel.CancelEventHandler(this.txtPricingType_Validating);
            // 
            // btnPricingType
            // 
            this.btnPricingType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPricingType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPricingType.Location = new System.Drawing.Point(412, 160);
            this.btnPricingType.Name = "btnPricingType";
            this.btnPricingType.Size = new System.Drawing.Size(24, 20);
            this.btnPricingType.TabIndex = 36;
            this.btnPricingType.Text = "...";
            this.btnPricingType.Click += new System.EventHandler(this.btnPricingType_Click);
            // 
            // lblPricingType
            // 
            this.lblPricingType.ForeColor = System.Drawing.Color.Black;
            this.lblPricingType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPricingType.Location = new System.Drawing.Point(236, 160);
            this.lblPricingType.Name = "lblPricingType";
            this.lblPricingType.Size = new System.Drawing.Size(78, 19);
            this.lblPricingType.TabIndex = 34;
            this.lblPricingType.Text = "Pricing Type";
            this.lblPricingType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(438, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 238;
            this.label1.Text = "(days)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(80, 48);
            this.txtReferenceNo.MaxLength = 20;
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(126, 20);
            this.txtReferenceNo.TabIndex = 7;
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblReferenceNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblReferenceNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblReferenceNo.Location = new System.Drawing.Point(5, 48);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(79, 20);
            this.lblReferenceNo.TabIndex = 239;
            this.lblReferenceNo.Text = "Reference No.";
            this.lblReferenceNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(596, 429);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(34, 20);
            this.btnImport.TabIndex = 240;
            this.btnImport.Text = "Imp.";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // ocmdExcelSelect
            // 
            this.ocmdExcelSelect.Connection = this.oconExcelFile;
            // 
            // dlgOpenImpFile
            // 
            this.dlgOpenImpFile.DefaultExt = "xls";
            this.dlgOpenImpFile.Filter = "Excel File|*.xls";
            this.dlgOpenImpFile.Title = "Select File To Import";
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
            this.mnuImportNew.Text = "Import for new Purchase Order";
            this.mnuImportNew.Click += new System.EventHandler(this.mnuImportNew_Click);
            // 
            // mnuImportUpdate
            // 
            this.mnuImportUpdate.Index = 1;
            this.mnuImportUpdate.Text = "Import update Purchase Order";
            this.mnuImportUpdate.Click += new System.EventHandler(this.mnuImportUpdate_Click);
            // 
            // odadExcelFile
            // 
            this.odadExcelFile.MissingSchemaAction = System.Data.MissingSchemaAction.Ignore;
            this.odadExcelFile.SelectCommand = this.ocmdExcelSelect;
            // 
            // PurchaseOrder
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(634, 500);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txtReferenceNo);
            this.Controls.Add(this.txtPricingType);
            this.Controls.Add(this.txtMakerLoc);
            this.Controls.Add(this.txtMakerCode);
            this.Controls.Add(this.txtMakerName);
            this.Controls.Add(this.gridData);
            this.Controls.Add(this.txtCurrency);
            this.Controls.Add(this.txtPause);
            this.Controls.Add(this.txtCarrier);
            this.Controls.Add(this.txtPayTerms);
            this.Controls.Add(this.txtDeliveryTerms);
            this.Controls.Add(this.txtDiscTerms);
            this.Controls.Add(this.txtPOType);
            this.Controls.Add(this.txtInvToLoc);
            this.Controls.Add(this.txtShipToLoc);
            this.Controls.Add(this.txtBuyer);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.txtVendorLoc);
            this.Controls.Add(this.txtOrderNo);
            this.Controls.Add(this.txtVendor);
            this.Controls.Add(this.txtVendorName);
            this.Controls.Add(this.txtVendorSO);
            this.Controls.Add(this.lblReferenceNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPricingType);
            this.Controls.Add(this.lblPricingType);
            this.Controls.Add(this.txtDeliveryTime);
            this.Controls.Add(this.lblDeliveryTime);
            this.Controls.Add(this.btnMakerLoc);
            this.Controls.Add(this.btnMakerCode);
            this.Controls.Add(this.lblMakerCode);
            this.Controls.Add(this.lblMakerLoc);
            this.Controls.Add(this.txtRevision);
            this.Controls.Add(this.chkApproved);
            this.Controls.Add(this.lblReportTitle);
            this.Controls.Add(this.txtTotalNetAmount);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.txtTotalImpTax);
            this.Controls.Add(this.txtTotalDiscount);
            this.Controls.Add(this.txtTotalSpecTax);
            this.Controls.Add(this.txtTotalVAT);
            this.Controls.Add(this.btnVendorName);
            this.Controls.Add(this.txtExchRate);
            this.Controls.Add(this.cboOrderDate);
            this.Controls.Add(this.cboPriority);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.btnCurrency);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnCarrier);
            this.Controls.Add(this.btnPayTerms);
            this.Controls.Add(this.btnDeliveryTerms);
            this.Controls.Add(this.btnDiscTerms);
            this.Controls.Add(this.btnPOType);
            this.Controls.Add(this.btnInvToLoc);
            this.Controls.Add(this.btnShipToLoc);
            this.Controls.Add(this.btnBuyer);
            this.Controls.Add(this.btnContact);
            this.Controls.Add(this.btnVendorLoc);
            this.Controls.Add(this.lblBuyer);
            this.Controls.Add(this.btnAdvance);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblTotalImpTax);
            this.Controls.Add(this.lblTotalDiscount);
            this.Controls.Add(this.lblSpecTax);
            this.Controls.Add(this.lblTotalVAT);
            this.Controls.Add(this.btnDeliverySchedule);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnAdditionCharges);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.btnOrderNo);
            this.Controls.Add(this.btnVendor);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.lblExchRate);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.lblVendor);
            this.Controls.Add(this.lblOrderNo);
            this.Controls.Add(this.lblVendorLoc);
            this.Controls.Add(this.lblOrderDate);
            this.Controls.Add(this.chkVAT);
            this.Controls.Add(this.chkImportTax);
            this.Controls.Add(this.chkSpecialTax);
            this.Controls.Add(this.chkRecCompleted);
            this.Controls.Add(this.lblDistTerms);
            this.Controls.Add(this.lblPayTerms);
            this.Controls.Add(this.lblPOType);
            this.Controls.Add(this.lblDeliveryTerms);
            this.Controls.Add(this.lblInvToLoc);
            this.Controls.Add(this.lblShipToLoc);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblTotalNetAmount);
            this.Controls.Add(this.lblVendorSQ);
            this.Controls.Add(this.lblPause);
            this.Controls.Add(this.lblCarrier);
            this.Controls.Add(this.cboRequiredDate);
            this.Controls.Add(this.btnPrintConfiguration);
            this.Controls.Add(this.lblRevision);
            this.Controls.Add(this.lblTotalDeliveryScheduleQuantity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "PurchaseOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Order Maintenance";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PurchaseOrder_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.PurchaseOrder_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PurchaseOrder_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrderDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRequiredDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalVAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalSpecTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalImpTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalNetAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptPOSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRevision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Load,Add,Save,Edit,Delete,Close
		private void PurchaseOrder_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".PurchaseOrder_Load()";
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
				
				// load form
				InitVariable();
				// Store grid layout
				KeepTheGridDesign();

				// Load initial value for controls
				mFormAction = EnumAction.Default;
				SetEnableButtons();
				SetEditableControl(false);
				txtOrderNo.Enabled = true;
				btnDeliverySchedule.Enabled = false;
				btnAdditionCharges.Enabled = false;
				btnPrint.Enabled = false;

				if (mPOFormState == POFormState.ToExistCPO)
				{
					FormLoadForToExistPO();
				}
				
				//HACKED by TUAN TQ, 07 Nov, 2005
				if(voPOMaster.PurchaseOrderMasterID > 0 && mPOFormState == POFormState.Normal)
				{
					LoadPurchaseOrder4ViewOnly(voPOMaster.PurchaseOrderMasterID);
				}
				//End Tuan TQ


				this.btnPrintConfiguration.Click += new EventHandler(FormControlComponents.ShowMenuReportListHandler);
				this.btnPrint.Click += new EventHandler(FormControlComponents.RunDefaultReportEntriesHandler);
				
				// added: dungla: 11-04-2006 format revision number
				txtRevision.FormatType = FormatTypeEnum.CustomFormat;
				txtRevision.CustomFormat = Constants.INTERGER_NUMBERFORMAT;
				txtDeliveryTime.CustomFormat = Constants.INTERGER_NUMBERFORMAT;
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

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				voPOMaster = new PO_PurchaseOrderMasterVO();
				// add master record
				mFormAction = EnumAction.Add;
				mPOFormState = POFormState.Normal;
				SetEnableButtons();
				SetEditableControl(true);
				
				PO_PurchaseOrderMasterVO voMasterBlank = new PO_PurchaseOrderMasterVO();
				UtilsBO boUtil = new UtilsBO();
				voMasterBlank.OrderDate = boUtil.GetDBDate();
				cboOrderDate.Value = voMasterBlank.OrderDate;
				voMasterBlank.CCNID = SystemProperty.CCNID;
				voMasterBlank.CurrencyID = SystemProperty.DefaultCurrencyID;
//				if(voMasterBlank.CurrencyID > 0)
//				{
//					txtCurrency.Tag = SystemProperty.HomeCurrencyID;
//					txtCurrency.Text = SystemProperty.HomeCurrency;
//					FillExchangeRate(SystemProperty.DefaultCurrencyID);
//					voMasterBlank.ExchangeRate = decimal.Parse(txtExchRate.Value.ToString());
//				}
//				voMasterBlank.Code = CreateOrderNo(voMasterBlank.OrderDate);
				//voMasterBlank.Code = FormControlComponents.GetNoByMask(this);
				txtOrderNo.Text = string.Empty;
				
				VOToControls(voMasterBlank);
				txtCurrency.Text = string.Empty;
				txtCurrency.Tag = null;
				txtExchRate.Value = null;
				//Fill Default Master Location 
				FormControlComponents.SetDefaultMasterLocation(txtShipToLoc);
				FormControlComponents.SetDefaultMasterLocation(txtInvToLoc);
				OnGridDataChangeAllowAddNew(true);
				cboOrderDate.Focus();

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

		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			blnHasError = true;
			
			//Check mandatory in grid.
			try
			{
				if (gridData.EditActive) return;
				if(CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					cboCCN.Focus();
					return;
				}
				if(CheckMandatory(cboOrderDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					cboOrderDate.Focus();
					return;
				}

				#region no need to check period

//				if(!FormControlComponents.CheckDateInCurrentPeriod((DateTime)cboOrderDate.Value))
//				{
//					PCSMessageBox.Show(ErrorCode.MESSAGE_PKL_TRANSDATE_PERIOD, MessageBoxIcon.Exclamation);
//					cboOrderDate.Focus();
//					return;
//				}

				#endregion

				if(CheckMandatory(txtCurrency))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtCurrency.Focus();
					return;
				}
				if(CheckMandatory(txtExchRate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtExchRate.Focus();
					return;
				}
				if(decimal.Parse(txtExchRate.Value.ToString()) == 0)
					FillExchangeRate(int.Parse(txtCurrency.Tag.ToString()));
			
				if(CheckMandatory(txtOrderNo))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtOrderNo.Focus();
					return;
				}
//				if(Security.IsDifferencePrefix(this,lblOrderNo,txtOrderNo))
//				{
//					return;
//				}

				if(CheckMandatory(txtExchRate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtExchRate.Focus();
					return;
				}			
				if(decimal.Parse(txtExchRate.Value.ToString()) <= 0)
				{
					PCSMessageBox.Show(ErrorCode.POSITIVE_NUMBER,MessageBoxIcon.Exclamation);
					txtExchRate.Focus();
					return;
				}
				if (txtReferenceNo.Text != string.Empty && txtRevision.Value == DBNull.Value)
				{
					string[] strParams = new string[1];
					strParams[0] = lblRevision.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Warning, strParams);
					txtRevision.Focus();
					return;	
				}
				if (txtRevision.Value != DBNull.Value && txtReferenceNo.Text == string.Empty)
				{
					string[] strParams = new string[1];
					strParams[0] = lblReferenceNo.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Warning, strParams);
					txtReferenceNo.Focus();
					return;	
				}
				if (txtRevision.Value != DBNull.Value)
				{
					if ((int)txtRevision.Value <= 0)
					{    
						string[] strParams = new string[2];
						strParams[0] = lblRevision.Text;
						strParams[1] = "zero";
						PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParams);
						txtRevision.Focus();
						return;	
					}
				}

				//Check revision and reference number
				if (txtRevision.Value != DBNull.Value && txtReferenceNo.Text != string.Empty)
				{
					PurchaseOrderBO boPurchaseOrder = new PurchaseOrderBO();
					int intPOMasterIDToCheck = 0;
					if (mFormAction == EnumAction.Edit && txtOrderNo.Text != string.Empty)
					{
						intPOMasterIDToCheck = voPOMaster.PurchaseOrderMasterID;
					}
					if (!boPurchaseOrder.CheckReferenceAndRevisionNo(txtReferenceNo.Text, txtRevision.Text, intPOMasterIDToCheck))
					{
						PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Warning);
						txtRevision.Focus();
						return;	
					}

				}
				for (int i = 0; i < gridData.RowCount; i++)
				{
					#region checking item

					if(gridData[i, PO_PurchaseOrderDetailTable.PRODUCTID_FLD] == DBNull.Value ||
						gridData[i, PO_PurchaseOrderDetailTable.PRODUCTID_FLD].ToString() == string.Empty)
					{
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ITEM_FIELD,MessageBoxIcon.Exclamation);
						gridData.Row = i;
						gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.PRODUCTID_FLD]);
						gridData.Focus();
						return;
					}

					#endregion

					#region checking order quantity

					if(gridData[i, PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString() == string.Empty)
					{
						// Order quantity field must be higher than 0.
						PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ORDER_QUANTITY_FIELD,MessageBoxIcon.Exclamation);
						gridData.Row = i;
						gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD]);
						gridData.Focus();
						return;
					}
					else
					{
						try
						{
							if(decimal.Parse(gridData[i, PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString()) < 0)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_POSITIVE_ORDER_QUANTITY,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD]);
								gridData.Focus();
								return;
							}
						}
						catch
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC,MessageBoxIcon.Exclamation);
							gridData.Row = i;
							gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD]);
							gridData.Focus();
							return;
						}
					}

					#endregion

					#region checking unit of measure

					if(gridData[i, PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].ToString() == string.Empty)
					{
						// Please input Unit of measure field for each records.
						PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_OF_MEASURE_FIELD,MessageBoxIcon.Exclamation);
						gridData.Row = i;
						gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD]);
						gridData.Focus();
						return;
					}

					#endregion

					#region checking unit price

					if (gridData[i, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] == DBNull.Value ||
						gridData[i, PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString() == string.Empty)
					{
						// Order quantity field must be higher than 0.
						PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_PRICE_FIELD,MessageBoxIcon.Exclamation);
						gridData.Row = i;
						gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.UNITPRICE_FLD]);
						gridData.Focus();
						return;
					}
					else
					{
						#region 19/03/2007: allow UnitPrice = 0{Require : ThuyPt}
//						try
//						{
//							if(decimal.Parse(gridData[i, PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) <= 0)
//							{
//								PCSMessageBox.Show(ErrorCode.MESSAGE_PO_UP_CAN_NOT_BE_ZERO,MessageBoxIcon.Exclamation);
//								gridData.Row = i;
//								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.UNITPRICE_FLD]);
//								gridData.Focus();
//								return;
//							}
//						}
//						catch
//						{
//							PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC,MessageBoxIcon.Exclamation);
//							gridData.Row = i;
//							gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.UNITPRICE_FLD]);
//							gridData.Focus();
//							return;
//						}
						#endregion
					}

					#endregion
				}
			}
			catch{}
			if(CheckMandatory(txtVendor))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtVendor.Focus();
				return;
			}

			if(CheckMandatory(txtVendorLoc))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtVendorLoc.Focus();
				return;
			}
			if(CheckMandatory(txtMakerCode))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtMakerCode.Focus();
				return;
			}
			if(CheckMandatory(txtMakerLoc))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtMakerLoc.Focus();
				return;
			}
			if(CheckMandatory(txtShipToLoc))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtShipToLoc.Focus();
				return;
			}
			if(CheckMandatory(txtInvToLoc))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtInvToLoc.Focus();
				return;
			}
			if(CheckMandatory(txtPOType))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
				txtPOType.Focus();
				return;
			}
			// Checks sale order record
            if (gridData.RowCount <= 0)
            {
                // You have to input at least a record in grid sale order detail
                PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_AT_LEAST_RECORD_IN_GRID, MessageBoxIcon.Exclamation);
                gridData.Focus();
                return;
            }
			// Correcting in SaleOrderDetail
			if(IsInCorrectDetail())
			{
				gridData.Focus();
				return;
			}
			// if The Purchase Order is approved
			try
			{
				StoreDatabase();
				blnHasError = false;
				btnPrint.Enabled = true;
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

		private void btnEdit_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				#region Check Right To Edit Transaction
				if(Security.NoRightToEditTransaction(this, PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD, voPOMaster.PurchaseOrderMasterID))
				{
					return;
				}
				#endregion 
				if (mPOFormState == POFormState.Normal)
				{
//					// if The Purchase Order is approved
//					if(IsApproved())
//					{
//						// The Purchase Order is approved, you can not modify or delete
//						PCSMessageBox.Show(ErrorCode.MESSAGE_PURCHASE_ORDER_APPROVED,MessageBoxIcon.Exclamation);
//						gridData.Focus();
//						return;
//					}
					// edit form
					InitGrid(voPOMaster.PurchaseOrderMasterID);
					mFormAction = EnumAction.Edit;
					SetEnableButtons();
					SetEditableControl(true);
					OnGridDataChangeAllowAddNew(false);
					cboCCN.Enabled = false;
				}
				else
				{
					mFormAction = EnumAction.Edit;
					SetEnableButtons();
					SetEditableControl(true);
					OnGridDataChangeAllowAddNew(false);
					cboCCN.Enabled = false;
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

		private void btnDelete_Click(object sender, EventArgs e)
		{
			// delete master and detail records (include delivery schedule and additional charge),
			// the system to displays message inform to the user that data have been deleted,
			// the system clear all data in form 
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if(Security.NoRightToDeleteTransaction(this, PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD, voPOMaster.PurchaseOrderMasterID))
				{
					return;
				}
				if(IsApproved())
				{
					// The Purchase Order is approved, you can not modify or delete
					PCSMessageBox.Show(ErrorCode.MESSAGE_PURCHASE_ORDER_APPROVED,MessageBoxIcon.Exclamation);
					gridData.Focus();
					return;
				}
				// if exist product then excute deleted, else do nothing
				if (voPOMaster.PurchaseOrderMasterID > 0)	
				{
					DialogResult enumResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
					if(enumResult == DialogResult.Yes)
					{
						PurchaseOrderBO boOrder = new PurchaseOrderBO();
						boOrder.DeletePurchaseOrder(voPOMaster.PurchaseOrderMasterID);
						voPOMaster = new PO_PurchaseOrderMasterVO();
						// Set enable button and clear info on form
						blnIsChangedGrid = false;
						PO_PurchaseOrderMasterVO voSOMaster = new PO_PurchaseOrderMasterVO();
						VOToControls(voSOMaster);
						txtExchRate.Value = null;
						mFormAction = EnumAction.Default;
						SetEnableButtons();
			
						if (SystemProperty.CCNID > 0)
						{
							cboCCN.SelectedValue = SystemProperty.CCNID;
						}
						txtOrderNo.Text = string.Empty;
						txtOrderNo.Tag = null;
						txtOrderNo.Focus();

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
		private void btnDeliverySchedule_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDeliverySchedule_Click()";
			try 
			{
                if (dstPODetail.Tables.Count > 0 && dstPODetail.Tables[0].Rows.Count > 0)
                {
                    if (gridData[gridData.Row, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString() != string.Empty)
                    {
                        PurchaseOrderInformationVO objPurchaseOrderInformationVO = new PurchaseOrderInformationVO();
                        objPurchaseOrderInformationVO.CCNCode = cboCCN.Text;
                        objPurchaseOrderInformationVO.OrderDate = (DateTime)cboOrderDate.Value;
                        objPurchaseOrderInformationVO.OrderQuantity = decimal.Parse(gridData[gridData.Row, PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString());
                        objPurchaseOrderInformationVO.ProductID = int.Parse(gridData[gridData.Row, PO_PurchaseOrderDetailTable.PRODUCTID_FLD].ToString());
                        objPurchaseOrderInformationVO.ProductCode = gridData[gridData.Row, ITM_ProductTable.CODE_FLD].ToString();
                        objPurchaseOrderInformationVO.ProductDescription = gridData[gridData.Row, ITM_ProductTable.DESCRIPTION_FLD].ToString();
                        objPurchaseOrderInformationVO.ProductRevision = gridData[gridData.Row, ITM_ProductTable.REVISION_FLD].ToString();
                        objPurchaseOrderInformationVO.PurchaseOrderDetailID = int.Parse(gridData[gridData.Row, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
                        objPurchaseOrderInformationVO.PurchaseOrderLine = int.Parse(gridData[gridData.Row, PO_PurchaseOrderDetailTable.LINE_FLD].ToString());
                        objPurchaseOrderInformationVO.PurchaseOrderMasterID = int.Parse(gridData[gridData.Row, PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD].ToString());
                        objPurchaseOrderInformationVO.PurchaseOrderNo = gridData[gridData.Row, PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
                        objPurchaseOrderInformationVO.PartyId = (int) txtVendor.Tag;

                        objPurchaseOrderInformationVO.UnitCode = gridData[gridData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].ToString();
                        objPurchaseOrderInformationVO.Category = gridData[gridData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].ToString();
                        PODeliverySchedule frmPODeliverySchedule = new PODeliverySchedule(objPurchaseOrderInformationVO);
                        frmPODeliverySchedule.ShowDialog();
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

		private void btnAdditionCharges_Click(object sender, EventArgs e)
		{
			if(voPOMaster.PurchaseOrderMasterID > 0)
			{
				POAdditionCharges frmPOAdditionCharges = new POAdditionCharges(voPOMaster.PurchaseOrderMasterID);
				frmPOAdditionCharges.FormMode = EnumAction.Default;
				frmPOAdditionCharges.Show();
			}
		}

		private void btnAdvance_Click(object sender, EventArgs e)
		{
			// open advance form
		}

		private void btnOrderNo_Click(object sender, EventArgs e)
		{
			// find order
			const string METHOD_NAME = THIS + ".btnOrderNo_Click()";
			try
			{
				mPOFormState = POFormState.Normal;
				// HACK: TUANDM 10-14-2005
				Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbCondition.Add(PO_PurchaseOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				} 
				// END: TUANDM 10-14-2005

				drwResult = FormControlComponents.OpenSearchForm(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, txtOrderNo.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					LoadMaster(int.Parse(drwResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].ToString()));
					btnPrint.Enabled = true;
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

		private void btnVendor_Click(object sender, EventArgs e)
		{
			// find customer base on code
			const string METHOD_NAME = THIS + ".btnVendor_Click()";
			
			try
			{
				/*
				string strCondition = string.Empty;
				if(txtVendor.Text.Trim().Length > 0)
				{
					strCondition = " WHERE " + MST_PartyTable.CODE_FLD + " LIKE '" + txtVendor.Text.Replace("'","''") + "%'"
								+ " AND (" + MST_PartyTable.DELETEREASON_FLD + "=0 OR " + MST_PartyTable.DELETEREASON_FLD + " IS NULL)" ;
				}
				*/
				htbCriteria = new Hashtable();
				htbCriteria.Add(VENDOR, 1);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtVendor.Text.Trim(), htbCriteria, true);
				if(drwResult != null)
				{
					txtVendor.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();

					
					txtVendorLoc.Text = string.Empty;
					txtVendorLoc.Tag = null;
					txtContact.Text = string.Empty;
					txtContact.Tag = null;
				}
				else 
					txtVendor.Focus();
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

		private void PurchaseOrder_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
						for (int i = 0; i < gridData.RowCount; i++)
						{
							if(gridData[i, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] == null)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_PO_UP_CAN_NOT_BE_ZERO,MessageBoxIcon.Exclamation);
								return;
							}
						}
						btnSave_Click(sender, e);
						if (blnHasError)
						{
							e.Cancel = true;
						}
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
		private bool CheckBeforeDeleteAllRows()
		{
			for (int i =0; i <gridData.SelectedRows.Count; i++)
			{
				if (gridData[int.Parse(gridData.SelectedRows[i].ToString()),  PO_DeliveryScheduleTable.DELIVERYLINE_FLD].ToString() != 0.ToString()
					&& gridData[int.Parse(gridData.SelectedRows[i].ToString()),  PO_DeliveryScheduleTable.DELIVERYLINE_FLD].ToString() != string.Empty)
				{
					return false;
				}
				if (gridData[int.Parse(gridData.SelectedRows[i].ToString()),  PO_PurchaseOrderDetailTable.APPROVERID_FLD].ToString() != 0.ToString()
					&& gridData[int.Parse(gridData.SelectedRows[i].ToString()),  PO_PurchaseOrderDetailTable.APPROVERID_FLD].ToString() != string.Empty)
				{
					return false;
				}
			}
			return true;
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
				case Keys.Delete:
					// 04-05-2006 dungla: fix bug 3946 for NganNT: do not allow user to delete while form mode is default
					if ((e.KeyCode == Keys.Delete)&&(gridData.SelectedRows.Count > 0) && mFormAction == EnumAction.Edit)
					{
						if (btnSave.Enabled)
						{
							if (CheckBeforeDeleteAllRows())
							{
								gridData.AllowDelete = true;
								FormControlComponents.DeleteMultiRowsOnTrueDBGrid(gridData);
								int intCount  =0;
								foreach (DataRow objRow in dstPODetail.Tables[0].Rows)
								{
									if(objRow.RowState != DataRowState.Deleted) 
										intCount++;
								}
								for (int i =0; i <intCount; i++)
									gridData[i, PO_PurchaseOrderDetailTable.LINE_FLD] = i+1;
								Calculate();
								ReCalculate();
								//mFormAction = EnumAction.Edit;
								
							}
							else
							{
								gridData.AllowDelete = false;
								PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_DELDELIVERY,MessageBoxIcon.Warning);
								return;
							}
						}
					}
					break;
			}		
		}

		private void gridData_ButtonClick(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridData_ButtonClick()";
			try
			{
				// voPOMaster = new PO_PurchaseOrderMasterVO();
				if(gridData.AllowUpdate == false) return;
				if (gridData[gridData.Row, PO_PurchaseOrderDetailTable.APPROVERID_FLD].ToString() == string.Empty)
				{
					// Open form to select Product
					if ((gridData.Columns[gridData.Col] == gridData.Columns[ITM_ProductTable.CODE_FLD])
						|| (gridData.Columns[gridData.Col] == gridData.Columns[ITM_ProductTable.DESCRIPTION_FLD])
						|| (gridData.Columns[gridData.Col] == gridData.Columns[ITM_ProductTable.REVISION_FLD]))
					{
						string strText = string.Empty;
						// edited: dungla 28-03-2006, changed the condition
						if (gridData.AddNewMode == AddNewModeEnum.AddNewCurrent)
							strText = gridData[gridData.Row, gridData.Col].ToString();
						else
							strText = gridData.Columns[gridData.Col].Text.Trim();
						
						DataRowView drowView = null;
						if (gridData.Columns[gridData.Col] == gridData.Columns[ITM_ProductTable.CODE_FLD])
						{
							#region Del by Trada 15-11-2005

							//drowView = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.CODE_FLD,strText, string.Empty);

							#endregion 
							drowView = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.CODE_FLD,strText, null, true);
						}
						if (gridData.Columns[gridData.Col] == gridData.Columns[ITM_ProductTable.DESCRIPTION_FLD])
						{
							//drowView = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.DESCRIPTION_FLD,strText,string.Empty);
							drowView = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.DESCRIPTION_FLD,strText, null, true);
						}
						if (gridData.Columns[gridData.Col] == gridData.Columns[ITM_ProductTable.REVISION_FLD])
						{
							//drowView = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.REVISION_FLD,strText,string.Empty);
							drowView = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.REVISION_FLD,strText, null, true);
						}
						// HACK: TUANDM 10-14-2005
						if(drowView != null)
						{
							gridData.EditActive = true;
							gridData[gridData.Row, PO_PurchaseOrderDetailTable.PRODUCTID_FLD] = drowView[ITM_ProductTable.PRODUCTID_FLD];
							PurchaseOrderBO boOrder = new PurchaseOrderBO();
							ITM_ProductVO voProduct = (ITM_ProductVO) boOrder.GetProductVO(int.Parse(drowView[ITM_ProductTable.PRODUCTID_FLD].ToString()));
							if (voProduct.CategoryID > 0)
							{
								// category information
								ProductItemInfoBO boItem = new ProductItemInfoBO();
								gridData[gridData.Row,ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = boItem.GetCategoryCodeByProductID(voProduct.ProductID);
								gridData[gridData.Row,ITM_CategoryTable.CATEGORYID_FLD] = voProduct.CategoryID;
							}
							else
							{
								gridData[gridData.Row,ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = string.Empty;
								gridData[gridData.Row,ITM_CategoryTable.CATEGORYID_FLD] = null;
							}
							gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = voProduct.Code;
							gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = voProduct.Description;
							gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = voProduct.Revision;
//							if (gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] == DBNull.Value ||
//								gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].ToString() == string.Empty)
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] = int.Parse(drowView[ITM_ProductTable.BUYINGUMID_FLD].ToString());
							gridData[gridData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = ((MST_UnitOfMeasureVO)boOrder.GetUnitOfMeasure(voProduct.BuyingUMID)).Code;
							gridData[gridData.Row,PO_PurchaseOrderDetailTable.STOCKUMID_FLD] = int.Parse(drowView[ITM_ProductTable.STOCKUMID_FLD].ToString());

							#region UM Rate
                                
							int intBuyingUMID = 0;
							try
							{
								intBuyingUMID = int.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].ToString());
								if (intBuyingUMID > 0)
								{
									decimal decUMRate = boUtil.GetUMRate(intBuyingUMID, voProduct.StockUMID);
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.UMRATE_FLD] = decUMRate;
								}
							}
							catch{}

							#endregion

							int intMaxLine = 0;
							if(gridData.Row > 0) intMaxLine = int.Parse(gridData[gridData.Row - 1,PO_PurchaseOrderDetailTable.LINE_FLD].ToString());
							gridData[gridData.Row,PO_PurchaseOrderDetailTable.LINE_FLD] = ++intMaxLine;
							gridData[gridData.Row,PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = 0;
						
							//HACK: added by Tuan TQ: 19 Jul, 2006: Update UnitPrice field							
							//gridData[gridData.Row,PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = 0;
							if(drowView[ITM_ProductTable.LISTPRICE_FLD].Equals(DBNull.Value) || drowView[ITM_ProductTable.LISTPRICE_FLD].ToString().Equals(string.Empty))
							{
								gridData[gridData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = Decimal.Zero;
							}
							else
							{
								gridData[gridData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = drowView[ITM_ProductTable.LISTPRICE_FLD];
							}
							//End hack							

							//HACK: rem TuanDM's code by Tuan TQ (19 Jul, 2006):
							//UnitPrice from Item is new business (as above line code)

							// HACK: TUANDM 10-14-2005
							/*
							if (txtVendor.Text != string.Empty)
							{
								AutoFillItemReference(voProduct.ProductID, int.Parse(txtVendor.Tag.ToString()));
							}
							*/
							// END: TUANDM 10-14-2005
							//End Hack

							// input vat,imp,spec
							if(chkVAT.Checked)
							{
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.VAT_FLD] = voProduct.VAT;
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] = 0;
							}
							if(chkImportTax.Checked)
							{
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAX_FLD] = voProduct.ImportTax;
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] = 0;
							}
							if(chkSpecialTax.Checked)
							{
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.SPECIALTAX_FLD] = voProduct.SpecialTax;
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD] = 0;
							}
						} 
						// END: TUANDM 10-14-2005
					}
					// input value Unit of measure
					if (gridData.Columns[gridData.Col] == gridData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD])
					{
						DataRowView drowView = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME, MST_UnitOfMeasureTable.CODE_FLD, gridData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Text.Trim(), null, true);
						if(drowView != null)
						{
							// init value for record //pending
							gridData.Columns[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].Value = drowView[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD];
							gridData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = drowView[MST_UnitOfMeasureTable.CODE_FLD];

							#region UM Rate
                                
							// get stock um id
							int intStockUMID = 0;
							try
							{
								intStockUMID = int.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.STOCKUMID_FLD].ToString());
								if (intStockUMID > 0)
								{
									decimal decUMRate = boUtil.GetUMRate(Convert.ToInt32(drowView[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD]), intStockUMID);
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.UMRATE_FLD] = decUMRate;
								}
							}
							catch{}

							#endregion
						}
					}
				}
				else
				{
					//PCSMessageBox.Show(ErrorCode.MESSAGE_PURCHASE_ORDER_APPROVED,MessageBoxIcon.Exclamation);
					gridData.Focus();
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

		private void chkVAT_CheckedChanged(object sender, EventArgs e)
		{
			int intRow = gridData.RowCount;
			for(int i = 0; i < intRow; i++)
			{ 
				//•	If VAT field checked then the system calculate VAT amount base on unit price and VAT percent of the item
				if(chkVAT.Checked == true)
				{
					try
					{
						PurchaseOrderBO boOrder = new PurchaseOrderBO();
						ITM_ProductVO voProduct =  (ITM_ProductVO)boOrder.GetProductVO(int.Parse(gridData[i, PO_PurchaseOrderDetailTable.PRODUCTID_FLD].ToString()));
						gridData[i, PO_PurchaseOrderDetailTable.VAT_FLD] = voProduct.VAT;
						gridData[i, PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] = (decimal.Parse(gridData[i, PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString())
							*decimal.Parse(gridData[i, PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString()) - decimal.Parse(gridData[i, PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString()))
							*(decimal)voProduct.VAT/ONE_HUNDRED;
					}
					catch
					{
						gridData[i, PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] = 0;
					}
				}
				else
				{
					gridData[i, PO_PurchaseOrderDetailTable.VAT_FLD] = gridData[i, PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] = DBNull.Value;

				}
			}
			ReCalculate();
		}

		private void chkImportTax_CheckedChanged(object sender, EventArgs e)
		{
			if(dstPODetail.Tables.Count == 0) return;
			foreach (DataRow objRow in dstPODetail.Tables[0].Rows)
			{
				if(objRow.RowState == DataRowState.Deleted) continue;
				//•	If Export Tax field checked then the system calculate export tax amount base on unit price and export tax percent of the item 
				if(chkImportTax.Checked == true)
				{
					try
					{
						PurchaseOrderBO boSale = new PurchaseOrderBO();
						ITM_ProductVO voProduct =  (ITM_ProductVO)boSale.GetProductVO(int.Parse(objRow[PO_PurchaseOrderDetailTable.PRODUCTID_FLD].ToString()));
						decimal decDiscountAmount;
						if (objRow[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] != null)
						{
							decDiscountAmount = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());
						}
						else
							decDiscountAmount = 0;
						objRow[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD] = voProduct.ImportTax;
						objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] = (decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString())
							*decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString()) - decDiscountAmount)
							*(decimal)voProduct.ImportTax /ONE_HUNDRED;
					}
					catch
					{
						//objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] = 0;
					}
				}
				else
				{
					objRow[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD] = 
						objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] = DBNull.Value;
					//CalculateSpecAmount(decAmount);
				}
			}
			ReCalculate();
		}

		private void chkSpecialTax_CheckedChanged(object sender, EventArgs e)
		{
			if(dstPODetail.Tables.Count == 0) return;
			foreach (DataRow objRow in dstPODetail.Tables[0].Rows)
			{
				if(objRow.RowState == DataRowState.Deleted) continue;
				//•	If Special Tax field checked then the system calculate special tax amount base on unit price and special tax percent of the item 
				if(chkSpecialTax.Checked == true)
				{
					try
					{
						PurchaseOrderBO boSale = new PurchaseOrderBO();
						ITM_ProductVO voProduct =  (ITM_ProductVO)boSale.GetProductVO(int.Parse(objRow[PO_PurchaseOrderDetailTable.PRODUCTID_FLD].ToString()));

						objRow[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD] = voProduct.SpecialTax;
						objRow[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD] = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString())
							*decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
							*(decimal)voProduct.SpecialTax/ONE_HUNDRED;
					}
					catch
					{
						objRow[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD] = 0;
					}
				}
				else
				{
					objRow[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD] = 
						objRow[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD] = DBNull.Value;
				}
			}
			ReCalculate();
		}

		private void btnCurrency_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCurrency_Click()";
			try
			{
				DataRowView drowView = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME, MST_CurrencyTable.CODE_FLD, txtCurrency.Text, null, true);
				if(drowView != null)
				{
					txtCurrency.Tag = drowView[MST_CurrencyTable.CURRENCYID_FLD];
					txtCurrency.Text = drowView[MST_CurrencyTable.CODE_FLD].ToString();
					txtExchRate.Tag = FillExchangeRate(int.Parse(drowView[MST_CurrencyTable.CURRENCYID_FLD].ToString()));
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
		private void gridData_RowColChange(object sender, RowColChangeEventArgs e)
		{
		}

		private void gridData_OnAddNew(object sender, EventArgs e)
		{
		}
		
		private void gridData_BeforeDelete(object sender, CancelEventArgs e)
		{
			if(IsNotDeleted())
			{
				e.Cancel = true;
				return;
			}
		}

		private void btnVendorLoc_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendorLoc_Click()";
			try
			{
				htbCriteria = new Hashtable();
				if(txtVendor.Text != string.Empty)
				{
					if(txtVendor.Tag != null)
					{
						//strWhere = " WHERE " + MST_PartyContactTable.PARTYID_FLD + " = " + txtVendor.Tag.ToString();
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtVendor.Tag.ToString());
					}
				}
				else
				{
					// You have to select Vendor before select Vendor Location, please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_VENDOR_BEFORE_VENDORLOC);
					txtVendor.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtVendorLoc.Text.Trim(), htbCriteria, true);
				if(drwResult != null)
				{
					txtVendorLoc.Tag = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtVendorLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
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

		private void btnContact_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnContact_Click()";
			try
			{
				htbCriteria = new Hashtable();
				if(txtVendor.Text != string.Empty)
				{
					if(txtVendor.Tag != null)
					{
						//strWhere = " WHERE " + MST_PartyContactTable.PARTYID_FLD + " = " + txtVendor.Tag.ToString();
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtVendor.Tag.ToString());
					}
				}
				else
				{
					// You have to select Vendor before select Vendor contact, please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_VENDOR_BEFORE_VENDORLOC);
					txtVendor.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyContactTable.TABLE_NAME, MST_PartyContactTable.CODE_FLD, txtContact.Text.Trim(), htbCriteria, true);
				if(drwResult != null)
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

		private void btnBuyer_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnBuyer_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(MST_EmployeeTable.TABLE_NAME, MST_EmployeeTable.CODE_FLD, txtBuyer.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtBuyer.Tag = drwResult[MST_EmployeeTable.EMPLOYEEID_FLD];
					txtBuyer.Text = drwResult[MST_EmployeeTable.CODE_FLD].ToString();
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

		private void btnShipToLoc_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnShipToLoc_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtShipToLoc.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtShipToLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
					txtShipToLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
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
		private void btnInvToLoc_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnInvToLoc_Click()";
			try
			{
				
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtInvToLoc.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtInvToLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
					txtInvToLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
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

		private void btnVendorName_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendorName_Click()";
			try
			{
				/*
				string strCondition = string.Empty;
				if(txtVendorName.Text.Trim().Length > 0)
				{
					 strCondition = " WHERE " + MST_PartyTable.NAME_FLD + " LIKE '" + txtVendorName.Text.Replace("'","''") + "%'"
						 + " AND (" + MST_PartyTable.DELETEREASON_FLD + "=0 OR " + MST_PartyTable.DELETEREASON_FLD + " IS NULL)";
				}
				*/
				htbCriteria = new Hashtable();
				htbCriteria.Add(VENDOR, 1);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtVendorName.Text.Trim(), htbCriteria, true);
				if(drwResult != null)
				{
					txtVendor.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					
					txtVendorLoc.Text = string.Empty;
					txtVendorLoc.Tag = null;
					txtContact.Text = string.Empty;
					txtContact.Tag = null;
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

		private void btnPOType_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPOType_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(PO_PurchaseTypeTable.TABLE_NAME, PO_PurchaseTypeTable.CODE_FLD, txtPOType.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtPOType.Tag = drwResult[PO_PurchaseTypeTable.PURCHASETYPEID_FLD];
					txtPOType.Text = drwResult[PO_PurchaseTypeTable.CODE_FLD].ToString();
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

		private void btnDiscTerms_Click(object sender, EventArgs e)
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
							gridData[i,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] = decDiscountRate
								*decimal.Parse(gridData[i,PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString())
								*decimal.Parse(gridData[i,PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())/ONE_HUNDRED;
						}
						catch
						{
							gridData[i,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] = 0;
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

		private void btnDeliveryTerms_Click(object sender, EventArgs e)
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

		private void btnPayTerms_Click(object sender, EventArgs e)
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

		private void btnCarrier_Click(object sender, EventArgs e)
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

		private void btnPause_Click(object sender, EventArgs e)
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

		private void btnHelp_Click(object sender, EventArgs e)
		{
			//ShowPOSummaryReport(sender, e);
		}

		private void txtOrderNo_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnOrderNo.Enabled)
					btnOrderNo_Click(sender,e);
		}
		private void txtVendor_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnVendor.Enabled)
					btnVendor_Click(sender,e);
		}

		private void txtVendorName_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnVendorName.Enabled)
					btnVendorName_Click(sender,e);
		}

		private void txtVendorLoc_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnVendorLoc.Enabled)
					btnVendorLoc_Click(sender,e);
		}

		private void txtContact_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnContact.Enabled)
					btnContact_Click(sender,e);
		}

		private void txtBuyer_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnBuyer.Enabled)
					btnBuyer_Click(sender,e);
		}

		private void txtShipToLoc_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnShipToLoc.Enabled)
					btnShipToLoc_Click(sender,e);
		}

		private void txtInvToLoc_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnInvToLoc.Enabled)
					btnInvToLoc_Click(sender,e);
		}

		private void txtPOType_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnPOType.Enabled)
					btnPOType_Click(sender,e);
		}

		private void txtDiscTerms_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnDiscTerms.Enabled)
					btnDiscTerms_Click(sender,e);
		}

		private void txtDeliveryTerms_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnDeliveryTerms.Enabled)
					btnDeliveryTerms_Click(sender,e);
		}

		private void txtPayTerms_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnPayTerms.Enabled)
					btnPayTerms_Click(sender,e);
		}

		private void txtCarrier_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnCarrier.Enabled)
					btnCarrier_Click(sender,e);
		}

		private void txtPause_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnPause.Enabled)
					btnPause_Click(sender,e);
		}
		private void txtCurrency_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnCurrency.Enabled)
					btnCurrency_Click(sender,e);
		}
		
		private void btnPause_Leave(object sender, EventArgs e)
		{
			//HACK TUANDM : 10 - 24- 2005
			//			gridData.Row = 0;
			//			gridData.Col = gridData.Columns.IndexOf(gridData.Columns[ITM_ProductTable.CODE_FLD]);
			//			gridData.Focus();
			//END : 10 - 24- 2005
		}


		private void gridData_Click(object sender, EventArgs e)
		{
		
		}
		/// <summary>
		/// gridData_BeforeColEdit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, October 3 2005</date>
		private void gridData_BeforeColEdit(object sender, BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridData_BeforeColEdit()";
			try
			{
				if (e.Column.DataColumn.DataField == ITM_ProductTable.CODE_FLD || e.Column.DataColumn.DataField == ITM_ProductTable.DESCRIPTION_FLD
					|| e.Column.DataColumn.DataField == ITM_ProductTable.REVISION_FLD || e.Column.DataColumn.DataField == MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD)
				{
					if (gridData[gridData.Row, PO_PurchaseOrderDetailTable.APPROVERID_FLD].ToString() != string.Empty)
					{
						//PCSMessageBox.Show(ErrorCode.MESSAGE_PURCHASE_ORDER_APPROVED,MessageBoxIcon.Exclamation);
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
		/// txtOrderNo_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
		private void txtOrderNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtOrderNo_Validating()";
			try 
			{	
				if (!btnOrderNo.Enabled) return;
				if (!txtOrderNo.Modified) return;
				// HACK: TUANDM 10-14-2005
				if ((mFormAction == EnumAction.Add) ||(mFormAction == EnumAction.Edit) || txtOrderNo.Text.Trim() == String.Empty)
				{
					if (txtOrderNo.Text.Trim() == string.Empty)
					{
						FormControlComponents.ClearForm(this);
						InitGrid(0);
						btnPrint.Enabled = false;
						if (SystemProperty.CCNID != 0)
						{
							cboCCN.SelectedValue = SystemProperty.CCNID;
						}
					}
					return;
				}
				
				Hashtable htbCondition = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbCondition.Add(PO_PurchaseOrderMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				} 
				// END: TUANDM 10-14-2005
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, txtOrderNo.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					LoadMaster(int.Parse(drwResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].ToString()));
					btnPrint.Enabled = true;
				}
				else
				{
					e.Cancel = true;
					btnPrint.Enabled = true;
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
		/// <summary>
		/// txtVendor_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
		private void txtVendor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_Validating()";
			try 
			{	
				OnLeaveControl(sender,e);
				if (!txtVendor.Modified) return;
				if(txtVendor.Text.Trim() == string.Empty)
				{
					txtVendor.Tag = null;
					txtVendorName.Text = string.Empty;
					txtVendorLoc.Text = string.Empty;
					txtVendorLoc.Tag = null;
					txtContact.Text = string.Empty;
					txtContact.Tag = null;
					return;
				}
				htbCriteria = new Hashtable();
				htbCriteria.Add(VENDOR, 1);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtVendor.Text.Trim(), htbCriteria, false);
				if(drwResult != null)
				{
					txtVendor.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();

					
					txtVendorLoc.Text = string.Empty;
					txtVendorLoc.Tag = null;
					txtContact.Text = string.Empty;
					txtContact.Tag = null;
				}
				else 
					e.Cancel = true;
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
		/// <summary>
		/// txtVendorLoc_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
		private void txtVendorLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_Validating()";
			try 
			{	
				OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified)return;
				if(txtText.Text.Trim() == string.Empty)
				{
					txtText.Tag = null;
					return;
				}
			
				if(txtVendor.Tag == null)
				{
					// You have to select Vendor before select Vendor Location, please
					txtText.Text = string.Empty;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_VENDOR_BEFORE_VENDORLOC,MessageBoxIcon.Warning);
					txtVendor.Focus();
					return;
				}
				htbCriteria = new Hashtable();
				if(txtVendor.Text != string.Empty)
				{
					if(txtVendor.Tag != null)
					{
					
						//strWhere = " WHERE " + MST_PartyContactTable.PARTYID_FLD + " = " + txtVendor.Tag.ToString();
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtVendor.Tag.ToString());
					}
				}
				else
				{
					// You have to select Vendor before select Vendor Location, please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_VENDOR_BEFORE_VENDORLOC);
					return;
				}
			
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtVendorLoc.Text.Trim(), htbCriteria, false);
				if(drwResult != null)
				{
					txtVendorLoc.Tag = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtVendorLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
				}
				else 
					e.Cancel = true;
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
		/// <summary>
		/// txtBuyer_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
		private void txtBuyer_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtBuyer_Validating()";
			try 
			{	
				OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified)return;
				if(txtText.Text.Trim() == string.Empty) 
				{
					txtText.Tag = null;
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_EmployeeTable.TABLE_NAME, MST_EmployeeTable.CODE_FLD, txtBuyer.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtBuyer.Tag = drwResult[MST_EmployeeTable.EMPLOYEEID_FLD];
					txtBuyer.Text = drwResult[MST_EmployeeTable.CODE_FLD].ToString();
				}
				else
					txtBuyer.Focus();
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
				OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified)return;
				if(txtText.Text.Trim() == string.Empty)
				{
					txtText.Tag = null;
					return;
				}
				
				DataRowView drowView = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME, MST_CurrencyTable.CODE_FLD, txtCurrency.Text, null, false);
				if(drowView != null)
				{
					txtCurrency.Tag = drowView[MST_CurrencyTable.CURRENCYID_FLD];
					txtCurrency.Text = drowView[MST_CurrencyTable.CODE_FLD].ToString();
					txtExchRate.Tag = FillExchangeRate(int.Parse(drowView[MST_CurrencyTable.CURRENCYID_FLD].ToString()));
				}
				else 
					e.Cancel = true;
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
		/// <summary>
		/// txtVendorName_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
		private void txtVendorName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendorName_Validating()";
			try 
			{
				OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified)return;
				if(txtText.Text.Trim() == string.Empty)
				{
					txtVendor.Text = string.Empty;
					txtVendor.Tag = null;
					txtVendorLoc.Text = string.Empty;
					txtVendorLoc.Tag = null;
					txtContact.Text = string.Empty;
					txtContact.Tag = null;
					return;
				}
				htbCriteria = new Hashtable();
				htbCriteria.Add(VENDOR, 1);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtVendorName.Text.Trim(), htbCriteria, false);
				if(drwResult != null)
				{
					txtVendor.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					
					txtVendorLoc.Text = string.Empty;
					txtVendorLoc.Tag = null;
					txtContact.Text = string.Empty;
					txtContact.Tag = null;
				}
				e.Cancel = true;
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
		/// <summary>
		/// txtContact_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
		private void txtContact_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtContact_Validating()";
			try 
			{
				OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified)return;
				if(txtText.Text.Trim() == string.Empty) 
				{
					txtText.Tag = null;
					return;
				}
			
				if(txtVendor.Tag == null)
				{
					// You have to select Vendor before select Vendor Location, please
					txtText.Text = string.Empty;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_VENDOR_BEFORE_VENDORLOC,MessageBoxIcon.Warning);
					return;
				}
				if(txtVendor.Text != string.Empty)
				{
					if(txtVendor.Tag != null)
					{
						htbCriteria = new Hashtable();
						//strWhere = " WHERE " + MST_PartyContactTable.PARTYID_FLD + " = " + txtVendor.Tag.ToString();
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtVendor.Tag.ToString());
					}
				}
				else
				{
					// You have to select Vendor before select Vendor contact, please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_VENDOR_BEFORE_VENDORLOC);
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyContactTable.TABLE_NAME, MST_PartyContactTable.CODE_FLD, txtContact.Text.Trim(), htbCriteria, false);
				if(drwResult != null)
				{
					txtContact.Tag = drwResult[MST_PartyContactTable.PARTYCONTACTID_FLD];
					txtContact.Text = drwResult[MST_PartyContactTable.CODE_FLD].ToString();
				}
				else 
					e.Cancel = true;
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
		/// <summary>
		///	txtDiscTerms_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
		private void txtDiscTerms_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDiscTerms_Validating()";
			try 
			{
				OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified)return;
                int intRowCount = gridData.RowCount;
				
				if(txtText.Text.Trim() == string.Empty)
				{
					txtText.Tag = null;
					decDiscountRate = 0;
					// update grid
					for(int i = 0; i < intRowCount; i++)
					{ 
						gridData[i,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] = DBNull.Value;
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
                    intRowCount = gridData.RowCount;
					
					// update grid
					for(int i = 0; i < intRowCount; i++)
					{
						try
						{
							gridData[i,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] = decDiscountRate
								*decimal.Parse(gridData[i,PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString())
								*decimal.Parse(gridData[i,PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())/ONE_HUNDRED;
						}
						catch
						{
							gridData[i,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] = 0;
						}
					}
					ReCalculate();
				}
				else
					e.Cancel = true;
				txtText.Modified = false;
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
		/// <summary>
		/// txtPause_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
		private void txtPause_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPause_Validating()";
			try 
			{
				OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified)return;
				if(txtText.Text.Trim() == string.Empty)			
				{
					txtText.Tag = null;
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
//				OnLeaveControl(sender,e);
//				TextBox txtText = (TextBox)sender;
//				if(!txtText.Modified)return;
				if (!txtShipToLoc.Modified) return;
				if (txtShipToLoc.Text.Trim() == strCheckTextBoxModified) return;
				if(txtShipToLoc.Text.Trim() == string.Empty) 
				{
					txtShipToLoc.Tag = null;
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtShipToLoc.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtShipToLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
					txtShipToLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
				}
				else 
					e.Cancel = true;
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

		private void txtInvToLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtShipToLoc_Validating()";
			try 
			{
				OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified || txtText.Text.Trim() == string.Empty)
					return;
				txtText.Text = txtText.Text.Trim();
				if(txtText.Text == strOldTextBoxValue)
				{
					txtText.Tag = null;
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtInvToLoc.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtInvToLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
					txtInvToLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
				}	
				else
					e.Cancel = true;
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
		/// <summary>
		/// txtPOType_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtPOType_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPOType_Validating()";
			try 
			{
				OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified)return;
				if(txtText.Text.Trim() == string.Empty) 
				{
					txtText.Tag = null;
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(PO_PurchaseTypeTable.TABLE_NAME, PO_PurchaseTypeTable.CODE_FLD, txtPOType.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtPOType.Tag = drwResult[PO_PurchaseTypeTable.PURCHASETYPEID_FLD];
					txtPOType.Text = drwResult[PO_PurchaseTypeTable.CODE_FLD].ToString();
				}
				else
					e.Cancel = true;
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
		/// <summary>
		/// txtDeliveryTerms_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Nov 15 2005</date>
		private void txtDeliveryTerms_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtDeliveryTerms_Validating()";
			try 
			{
				OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified)return;
				if(txtText.Text.Trim() == string.Empty) 
				{
					txtText.Tag = null;
					return;
				}

				drwResult = FormControlComponents.OpenSearchForm(MST_DeliveryTermTable.TABLE_NAME, MST_DeliveryTermTable.CODE_FLD, txtDeliveryTerms.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtDeliveryTerms.Tag = drwResult[MST_DeliveryTermTable.DELIVERYTERMID_FLD];
					txtDeliveryTerms.Text = drwResult[MST_DeliveryTermTable.CODE_FLD].ToString();
				}
				else
					e.Cancel = true;	
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

		private void txtPayTerms_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPayTerms_Validating()";
			try 
			{
				OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified)return;
				if(txtText.Text.Trim() == string.Empty)
				{
					txtText.Tag = null;
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

		private void txtCarrier_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCarrier_Validating()";
			try 
			{
				OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified)return;
				if(txtText.Text.Trim() == string.Empty)
				{
					txtText.Tag = null;
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
		#endregion other events

		#region other functions
		private void InitVariable()
		{
			// Load combo box
			DataSet dstCCN = new DataSet();
			UtilsBO boUtil = new UtilsBO();
			dstCCN = boUtil.ListCCN();
			if(dstCCN.Tables.Count > 0)
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[0],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);


			// HACK: TUANDM 10-14-2005
			if (SystemProperty.CCNID != 0)
			{
				cboCCN.SelectedValue = SystemProperty.CCNID;
			} 
			// END: TUANDM 10-14-2005
		}

		private void InitGrid(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".InitGrid()";
			const int FORMATED_STRING_24 = 24; //24 chars
			PurchaseOrderBO boOrder = new PurchaseOrderBO();
			dstPODetail = boOrder.ListDetailByMaster(pintMasterID); 
			DataColumn[] objColumns = {dstPODetail.Tables[0].Columns[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD]};

			// fill data into grid
			dstPODetail.Tables[0].PrimaryKey = objColumns;
			gridData.DataSource = dstPODetail.Tables[0];
			dstPODetail.Tables[0].Columns[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].AutoIncrement = true;
			dstPODetail.Tables[0].Columns[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].AutoIncrementSeed = 1;
			dstPODetail.Tables[0].Columns[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].AutoIncrementStep = 1;
			dstPODetail.Tables[0].Columns[PO_PurchaseOrderDetailTable.CLOSED_FLD].DefaultValue = false;
			dstPODetail.Tables[0].Columns[PO_PurchaseOrderDetailTable.REOPEN_FLD].DefaultValue = false;

			// show columns
			foreach(C1DisplayColumn objCol in gridData.Splits[0].DisplayColumns)
			{
				objCol.Visible = false;					
				objCol.HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
			}

			// Restore grid layout 
			foreach (C1DataColumn dcolC1 in gridData.Columns)
			{
				foreach(DataRow drowData in dtbGridDesign.Rows)
				{
					if (dcolC1.DataField.Trim().ToLower() == drowData[GRID_COL_NAME].ToString().ToLower())
					{
						// gridData.Columns[i].DataField = drowData[GRID_COL_NAME];
						dcolC1.Caption = drowData[GRID_COL_CAPTION].ToString();
						gridData.Splits[0].DisplayColumns[dcolC1.DataField].Width = int.Parse(drowData[GRID_COL_WIDTH].ToString());
						gridData.Splits[0].DisplayColumns[dcolC1.DataField].Visible = bool.Parse(drowData[GRID_COL_VISIBLE].ToString());
						if(!gridData.Splits[0].DisplayColumns[dcolC1.DataField].Visible)
							gridData.Splits[0].DisplayColumns[dcolC1.DataField].AllowSizing = false;

						break;
					}
				}
			}
			// Line column
			gridData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.LINE_FLD].Locked = true;
			gridData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.LINE_FLD].HeadingStyle.ForeColor = Color.Maroon;
			// Category column
			gridData.Splits[0].DisplayColumns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Locked = true;
			// Item column
			gridData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].HeadingStyle.ForeColor = Color.Maroon;
			gridData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
			gridData.Columns[ITM_ProductTable.CODE_FLD].DataWidth = FORMATED_STRING_24;
			// Description column
			gridData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
			// Revision column
			gridData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Button = true;
			gridData.Columns[ITM_ProductTable.REVISION_FLD].DataWidth = FORMATED_STRING_24;
			// OrderQuantity column
			gridData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].HeadingStyle.ForeColor = Color.Maroon;
			gridData.Columns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			// Adjustment1 column
			gridData.Columns[PO_PurchaseOrderDetailTable.ADJUSTMENT1_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			// Adjustment2 column
			gridData.Columns[PO_PurchaseOrderDetailTable.ADJUSTMENT2_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			// RequiredDate column
			gridData.Columns[PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD].Editor = cboRequiredDate;
			gridData.Columns[PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT;
			gridData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
			// UM column
			gridData.Splits[0].DisplayColumns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].HeadingStyle.ForeColor = Color.Maroon;
			gridData.Splits[0].DisplayColumns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Button = true;
			// UnitPrice column
			//gridData.Columns[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			gridData.Columns[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].NumberFormat = "##############,0.0000";
			gridData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].HeadingStyle.ForeColor = Color.Maroon;
			// %VAT column
			gridData.Columns[PO_PurchaseOrderDetailTable.VAT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			// VATAmount column
			gridData.Columns[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			// %ImportTax column
			gridData.Columns[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			// ImportTaxAmount column
			gridData.Columns[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			// %SpecialTax column
			gridData.Columns[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			// SpecialTaxAmount column
			gridData.Columns[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			// TotalAmount column
			gridData.Columns[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			gridData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].Locked = true;
			dstPODetail.Tables[0].Columns[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].DefaultValue = 0;
			// DiscountAmount column
			gridData.Columns[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			// NetAmount column
			gridData.Columns[PO_PurchaseOrderDetailTable.NETAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
			gridData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.NETAMOUNT_FLD].Locked = true;
			// strHeaderClosed column
			gridData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.CLOSED_FLD].Locked = true;
			gridData.Columns[PO_PurchaseOrderDetailTable.CLOSED_FLD].ValueItems.Presentation = PresentationEnum.CheckBox;
			// REOPEN_FLD column
			gridData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.REOPEN_FLD].Locked = true;
			gridData.Columns[PO_PurchaseOrderDetailTable.REOPEN_FLD].ValueItems.Presentation = PresentationEnum.CheckBox;
			// ItemCustomerCode column
			gridData.Columns[PO_PurchaseOrderDetailTable.VENDORITEM_FLD].DataWidth = FORTY;
			// ItemCustomerRevision column
			gridData.Columns[PO_PurchaseOrderDetailTable.VENDORREVISION_FLD].DataWidth = FORTY;
		}

		private void ControlsToVO()
		{
			voPOMaster.PartyID = int.Parse(txtVendor.Tag.ToString());
			voPOMaster.Code = txtOrderNo.Text.Trim();
			voPOMaster.ExchangeRate = decimal.Parse(txtExchRate.Value.ToString());
			voPOMaster.VendorSO = txtVendorSO.Text.Trim();
			if (!txtRevision.ValueIsDbNull)
				voPOMaster.PORevision = Convert.ToInt32(txtRevision.Value);
			voPOMaster.SpecialTax = chkSpecialTax.Checked;
			voPOMaster.ImportTax = chkImportTax.Checked;
			voPOMaster.VAT = chkVAT.Checked;
			voPOMaster.RecCompleted = chkRecCompleted.Checked;
			voPOMaster.VendorLocID = int.Parse(txtVendorLoc.Tag.ToString());
			if(txtShipToLoc.Text != string.Empty)
				voPOMaster.ShipToLocID = int.Parse(txtShipToLoc.Tag.ToString());
			if(txtCurrency.Text != string.Empty)
				voPOMaster.CurrencyID = int.Parse(txtCurrency.Tag.ToString());
			if(txtContact.Text != string.Empty)
				voPOMaster.PartyContactID = int.Parse(txtContact.Tag.ToString());
			if(txtDeliveryTerms.Text != string.Empty)
				voPOMaster.DeliveryTermsID = int.Parse(txtDeliveryTerms.Tag.ToString());
			if(txtPayTerms.Text != string.Empty)
				voPOMaster.PaymentTermsID = int.Parse(txtPayTerms.Tag.ToString());
			else
				voPOMaster.PaymentTermsID = 0;
			if(txtCarrier.Text != string.Empty)
				voPOMaster.CarrierID = int.Parse(txtCarrier.Tag.ToString());
			else
				voPOMaster.CarrierID = 0;
			if(txtPause.Text != string.Empty)
				voPOMaster.PauseID = int.Parse(txtPause.Tag.ToString());
			if(txtDiscTerms.Text != string.Empty)
				voPOMaster.DiscountTermID = int.Parse(txtDiscTerms.Tag.ToString());
			if(txtPOType.Text != string.Empty)
				voPOMaster.PurchaseTypeID = int.Parse(txtPOType.Tag.ToString());
			if(txtInvToLoc.Text != string.Empty)
				voPOMaster.InvToLocID = int.Parse(txtInvToLoc.Tag.ToString());
			if(txtShipToLoc.Text != string.Empty)
				voPOMaster.ShipToLocID = int.Parse(txtShipToLoc.Tag.ToString());
			if(txtBuyer.Text != string.Empty)
				voPOMaster.BuyerID = int.Parse(txtBuyer.Tag.ToString());
			if(cboCCN.Text != string.Empty)
				voPOMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
			try
			{
				voPOMaster.Priority = int.Parse(cboPriority.Text);
			}
			catch
			{
				voPOMaster.Priority = ONE;
			}
			voPOMaster.OrderDate = (DateTime)cboOrderDate.Value;
			if(txtMakerCode.Tag != null)
				voPOMaster.MakerID = int.Parse(txtMakerCode.Tag.ToString());
			if(txtPricingType.Tag != null)
			{
				voPOMaster.PricingTypeID = int.Parse(txtPricingType.Tag.ToString());
			}
			if(txtMakerLoc.Tag != null)
				voPOMaster.MakerLocationID = int.Parse(txtMakerLoc.Tag.ToString());
			if (txtDeliveryTime.Value != DBNull.Value)
			{
				voPOMaster.RequestDeliveryTime = Convert.ToInt32(txtDeliveryTime.Value);
			}
			else
				voPOMaster.RequestDeliveryTime = 0;
			voPOMaster.ReferenceNo = txtReferenceNo.Text;
		}

		private void VOToControls(PO_PurchaseOrderMasterVO pvoMaster)
		{
			//PurchaseOrderBO boOrder = new PurchaseOrderBO();
			if (pvoMaster.PartyID > 0)
			{
				txtVendor.Tag = pvoMaster.PartyID;
			}
			else
			{
				txtVendor.Tag = null;
				txtVendor.Text = string.Empty;
				txtVendorName.Text = string.Empty;
			}
			if (pvoMaster.VendorLocID > 0)
				txtVendorLoc.Tag = pvoMaster.VendorLocID;
			else
			{
				txtVendorLoc.Text = string.Empty;
				txtVendorLoc.Tag = null;
			}
			if (pvoMaster.MakerID > 0)
			{
				txtMakerCode.Tag = pvoMaster.MakerID;
			}
			else
			{
				txtMakerCode.Tag = null;
				txtMakerCode.Text = string.Empty;
				txtMakerName.Text = string.Empty;
			}
			if (pvoMaster.PricingTypeID > 0)
			{
				txtPricingType.Tag = pvoMaster.PricingTypeID;
			}
			else
			{
				txtPricingType.Tag = null;
				txtPricingType.Text = string.Empty;
			}
			if (pvoMaster.MakerLocationID > 0)
				txtMakerLoc.Tag = pvoMaster.MakerLocationID;
			else
			{
				txtMakerLoc.Text = string.Empty;
				txtMakerLoc.Tag = null;
			}
			if (voPOMaster.RequestDeliveryTime != 0)
			{
				txtDeliveryTime.Value = voPOMaster.RequestDeliveryTime;
			}
			else
				txtDeliveryTime.Value = null;	
			//txtOrderNo.Text = pvoMaster.Code;
			if((DateTime.MinValue < pvoMaster.OrderDate) && (pvoMaster.OrderDate < DateTime.MaxValue))
				cboOrderDate.Value = pvoMaster.OrderDate;
			else
				cboOrderDate.Value = DBNull.Value;
			txtExchRate.Value = pvoMaster.ExchangeRate;
			txtVendorSO.Text = pvoMaster.VendorSO; 
			if (pvoMaster.PORevision > 0)
				txtRevision.Value = pvoMaster.PORevision;
			else
				txtRevision.Value = DBNull.Value;
			chkSpecialTax.Checked = pvoMaster.SpecialTax;
			chkImportTax.Checked = pvoMaster.ImportTax;
			chkVAT.Checked = pvoMaster.VAT;
			chkRecCompleted.Checked = pvoMaster.RecCompleted; 
			if (pvoMaster.ShipToLocID > 0)
				txtShipToLoc.Tag = pvoMaster.ShipToLocID;
			else
			{
				txtShipToLoc.Text = string.Empty;
				txtShipToLoc.Tag = null;
			}
			if(pvoMaster.CurrencyID > 0)
				txtCurrency.Tag = pvoMaster.CurrencyID;
			else
			{
				txtCurrency.Text = string.Empty;
				txtCurrency.Tag = null;
			}
			if(pvoMaster.PartyContactID > 0)
				txtContact.Tag = pvoMaster.PartyContactID;
			else
			{
				txtContact.Text = string.Empty;
				txtContact.Tag = null;
			}
			if(pvoMaster.DeliveryTermsID > 0)
				txtDeliveryTerms.Tag = pvoMaster.DeliveryTermsID;
			else
			{
				txtDeliveryTerms.Text = string.Empty;
				txtDeliveryTerms.Tag = null;
			}
			if(pvoMaster.PaymentTermsID > 0)
				txtPayTerms.Tag = pvoMaster.PaymentTermsID;
			else
			{
				txtPayTerms.Text = string.Empty;
				txtPayTerms.Tag = null;
			}
			if(pvoMaster.CarrierID > 0)
				txtCarrier.Tag = pvoMaster.CarrierID;
			else
			{
				txtCarrier.Text = string.Empty;
				txtCarrier.Tag = null;
			}
			if(pvoMaster.PauseID > 0)
				txtPause.Tag = pvoMaster.PauseID;
			else
			{
				txtPause.Text = string.Empty;
				txtPause.Tag = null;
			}
			if(pvoMaster.DiscountTermID > 0)
				txtDiscTerms.Tag = pvoMaster.DiscountTermID;
			else
			{
				txtDiscTerms.Text = string.Empty;
				txtDiscTerms.Tag = null;
			}
			if(pvoMaster.PurchaseTypeID > 0)
				txtPOType.Tag = pvoMaster.PurchaseTypeID;
			else
			{
				txtPOType.Text = string.Empty;
				txtPOType.Tag = null;
			}
			if(pvoMaster.InvToLocID > 0)
				txtInvToLoc.Tag = pvoMaster.InvToLocID;
			else
			{
				txtInvToLoc.Text = string.Empty;
				txtInvToLoc.Tag = null;
			}
			txtReferenceNo.Text = pvoMaster.ReferenceNo;
			if(pvoMaster.BuyerID > 0)
				txtBuyer.Tag = pvoMaster.BuyerID;
			else
			{
				txtBuyer.Text = string.Empty;
				txtBuyer.Tag = null;
			}
			if(pvoMaster.CCNID > 0)
				cboCCN.SelectedValue = pvoMaster.CCNID;
			else
				cboCCN.Text = string.Empty;
			cboPriority.Text = pvoMaster.Priority.ToString();
			InitGrid(pvoMaster.PurchaseOrderMasterID);
			Calculate();
			chkApproved.Checked = IsApproved();
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
			//txtRevision.Enabled = pblnEnable;
			txtExchRate.Enabled = pblnEnable;
			btnVendor.Enabled = pblnEnable;
			txtVendor.Enabled = pblnEnable;
			btnOrderNo.Enabled = !pblnEnable;
			txtVendorSO.Enabled = pblnEnable;
			chkSpecialTax.Enabled = pblnEnable;
			chkImportTax.Enabled = pblnEnable;
			chkVAT.Enabled = pblnEnable;
			//chkRecCompleted.Enabled = pblnEnable;
			btnShipToLoc.Enabled = pblnEnable;
			txtShipToLoc.Enabled = pblnEnable;
			btnCurrency.Enabled = pblnEnable;
			txtCurrency.Enabled = pblnEnable;
			btnContact.Enabled = pblnEnable;
			txtContact.Enabled = pblnEnable;
			btnDeliveryTerms.Enabled = pblnEnable;
			txtDeliveryTerms.Enabled = pblnEnable;
			btnPayTerms.Enabled = pblnEnable;
			txtPayTerms.Enabled = pblnEnable;
			btnCarrier.Enabled = pblnEnable;
			txtCarrier.Enabled = pblnEnable;
			btnPause.Enabled = pblnEnable;
			txtPause.Enabled = pblnEnable;
			btnDiscTerms.Enabled = pblnEnable;
			txtDiscTerms.Enabled = pblnEnable;
			btnPOType.Enabled = pblnEnable;
			txtPOType.Enabled = pblnEnable;
			btnInvToLoc.Enabled = pblnEnable;
			txtInvToLoc.Enabled = pblnEnable;
			btnVendorName.Enabled = pblnEnable;
			txtVendorName.Enabled = pblnEnable;
			txtRevision.Enabled = pblnEnable;
			txtReferenceNo.Enabled = pblnEnable;
			btnVendorLoc.Enabled = pblnEnable;
			txtVendorLoc.Enabled = pblnEnable;
			btnPayTerms.Enabled = pblnEnable;
			txtPayTerms.Enabled = pblnEnable;
			btnBuyer.Enabled = pblnEnable;
			
			btnMakerCode.Enabled = pblnEnable;
			btnMakerLoc.Enabled = pblnEnable;
			txtMakerCode.Enabled = pblnEnable;
			txtMakerName.Enabled = pblnEnable;
			txtMakerLoc.Enabled = pblnEnable;
			txtDeliveryTime.Enabled = pblnEnable;
			txtBuyer.Enabled = pblnEnable;
			//			cboCCN.Enabled = pblnEnable;
			cboPriority.Enabled = pblnEnable;
			cboOrderDate.Enabled = pblnEnable;
			btnPricingType.Enabled = txtPricingType.Enabled = pblnEnable;

			btnAdditionCharges.Enabled = !pblnEnable;
			btnAdvance.Enabled = !pblnEnable;
			btnDeliverySchedule.Enabled = !pblnEnable;
			// Set editable for gridData
			// 04-05-2006 dungla: fix bug 3946 for NganNT: do not allow user to delete while form mode is default
			gridData.AllowDelete = pblnEnable;
			// 04-05-2006 dungla: fix bug 3946 for NganNT: do not allow user to delete while form mode is default
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
					btnPrint.Enabled = false;
					//Enable Buttons
					btnSave.Enabled = true;

					mnuImportNew.Enabled = true;
					mnuImportUpdate.Enabled = false;
					break;
				case EnumAction.Edit:
					//Disable Buttons
					btnAdd.Enabled = false;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					btnPrint.Enabled = false;
					//Enable Buttons
					btnSave.Enabled = true;
					mnuImportNew.Enabled = false;
					mnuImportUpdate.Enabled = true;
					break;
				case EnumAction.Default:
					//Disable Buttons
					btnSave.Enabled = false;
					btnPrint.Enabled = false;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					btnAdd.Enabled = true;
					btnHelp.Enabled = true;

					mnuImportNew.Enabled = false;
					mnuImportUpdate.Enabled = false;
					break;
			}
		}

		private void Calculate()
		{
			//•	The system calculate sum of Total Amount = sum(Total Amount column)
			txtTotalAmount.Value = TotalColumn(PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD).ToString();
			//•	The system calculate sum of Total Discount = sum(Discount Amount column)
			txtTotalDiscount.Value = TotalColumn(PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD).ToString();
			//•	The system calculate sum of Total Export Tax = sum(Export Tax Amount column)
			txtTotalImpTax.Value = TotalColumn(PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD).ToString();
			//•	The system calculate sum of Total Net Amount = sum(Net Amount column)
			txtTotalNetAmount.Value = TotalColumn(PO_PurchaseOrderDetailTable.NETAMOUNT_FLD).ToString();
			//•	The system calculate sum of Total Special Tax = sum(Special Tax Amount column)
			txtTotalSpecTax.Value = TotalColumn(PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD).ToString();
			//•	The system calculate sum of Total VAT = sum(VAT Amount column)
			txtTotalVAT.Value = TotalColumn(PO_PurchaseOrderDetailTable.VATAMOUNT_FLD).ToString();
		}

		private void ReCalculate()
		{
			if(dstPODetail.Tables.Count > 0)
				foreach (DataRow objRow in dstPODetail.Tables[0].Rows)
				{
					if(objRow.RowState == DataRowState.Deleted) continue;
					//•	If VAT field checked then the system calculate VAT amount base on unit price and VAT percent of the item
					if(objRow[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] == DBNull.Value) continue;
					decimal decDiscountAmount = 0;
					if(objRow[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
						decDiscountAmount = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());

					if(chkVAT.Checked)
					{
						try
						{
							objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] =  (decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
								* decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount)
								*decimal.Parse(objRow[PO_PurchaseOrderDetailTable.VAT_FLD].ToString())/ONE_HUNDRED;
						}
						catch
						{
							objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] = 0;
						}
					}
					//•	If Export Tax field checked then the system calculate export tax amount base on unit price and export tax percent of the item 
					if(chkImportTax.Checked == true)
					{
						try
						{
							objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] = (decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
								* decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount)
								* decimal.Parse(objRow[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].ToString())/ONE_HUNDRED;
						}
						catch
						{
							objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] = 0;
						}
					}
					//•	If Special Tax field checked then the system calculate special tax amount base on unit price and special tax percent of the item 
					if(chkSpecialTax.Checked)
					{
						try
						{
							decimal decImpTax, decVatAmount;
							if ((objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] != null) && (objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value))
							{
								decVatAmount = 	decimal.Parse(objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD].ToString());	
							}
							else
								decVatAmount = 0;
							if ((objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] != null) && (objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] != DBNull.Value))
							{
								decImpTax = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD].ToString());	
							}
							else
								decImpTax = 0;	
							objRow[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD] = ((decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
								* decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount) + decImpTax + decVatAmount)
								* decimal.Parse(objRow[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD].ToString())/ONE_HUNDRED;
						}
						catch
						{
						}
					}
					// Get VAT, Export Tax, Special Tax
					decimal decVAT;
					try
					{
						decVAT = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD].ToString());
					}
					catch
					{
						decVAT = 0;
					}
					decimal decImportTax;
					try
					{
						decImportTax = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD].ToString());
					}
					catch
					{
						decImportTax = 0;
					}
					decimal decSpecialTax;
					try
					{
						decSpecialTax = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD].ToString());
					}
					catch
					{
						decSpecialTax = 0;
					}
					//•	The system calculate Total Amount = (quantity * unit price)+ VAT + Export Tax + Special Tax
					try
					{
						objRow[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD] = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
							* decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount + decVAT + decImportTax + decSpecialTax;
					}
					catch
					{
						// do nothing
					}
					//•	The system calculate Net Amount = Total Amount – Discount Amount
					try
					{
						objRow[PO_PurchaseOrderDetailTable.NETAMOUNT_FLD] = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
							* decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount;
						
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
				// synchronyze data
				FormControlComponents.SynchronyGridData(gridData);
				PurchaseOrderBO boOrder = new PurchaseOrderBO();
				voPOMaster.MasterLocationID = voPOMaster.ShipToLocID; // ThangLQ required
				voPOMaster.TotalAmount = Decimal.Parse(txtTotalAmount.Value.ToString());
				voPOMaster.TotalDiscount = Decimal.Parse(txtTotalDiscount.Value.ToString());
				voPOMaster.TotalNetAmount = Decimal.Parse(txtTotalNetAmount.Value.ToString());
				voPOMaster.TotalImportTax = Decimal.Parse(txtTotalImpTax.Value.ToString());
				voPOMaster.TotalSpecialTax = Decimal.Parse(txtTotalSpecTax.Value.ToString());
				voPOMaster.TotalVAT = Decimal.Parse(txtTotalVAT.Value.ToString());
				if (!txtRevision.ValueIsDbNull)
					voPOMaster.PORevision = Convert.ToInt32(txtRevision.Value);
				else
					voPOMaster.PORevision = 0;
				if (mPOFormState == POFormState.Normal)
				{
					if(mFormAction == EnumAction.Add)
					{
						voPOMaster.PurchaseOrderMasterID = boOrder.AddNewPurchaseOrder(voPOMaster,dstPODetail);
						LoadMaster(voPOMaster.PurchaseOrderMasterID);
						//InitGrid(voPOMaster.PurchaseOrderMasterID);
					}
					else if(mFormAction == EnumAction.Edit)
					{
						boOrder.UpdatePurchaseOrder(voPOMaster,dstPODetail);
					}
				}
				else
				{
					if (mPOFormState == POFormState.ToExistCPO)
					{
						ArrayList arlCPOIDs = new ArrayList();
						foreach (DataRowView drvCPO in dtwCPOs)
						{
							arlCPOIDs.Add(drvCPO[MTR_CPOTable.CPOID_FLD].ToString());
						}
						boOrder.UpdatePOAndDelScheduleImmediate(voPOMaster, dstPODetail, dstDelivery, arlCPOIDs);
					}
					mPOFormState = POFormState.Normal;
				}
				InitGrid(voPOMaster.PurchaseOrderMasterID);
				mFormAction = EnumAction.Default;
				SetEnableButtons();
				SetEditableControl(false);
				OnGridDataChangeAllowAddNew(false);
				Security.UpdateUserNameModifyTransaction(this, PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD, voPOMaster.PurchaseOrderMasterID);
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
				btnDelete.Enabled = true;
				btnEdit.Enabled = true;
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

		private bool IsNotDeleted()
		{
			if (gridData.Columns[PO_DeliveryScheduleTable.DELIVERYLINE_FLD] != null)
			{
				if (gridData[gridData.Row,  PO_DeliveryScheduleTable.DELIVERYLINE_FLD].ToString() != 0.ToString()
					&& gridData[gridData.Row,  PO_DeliveryScheduleTable.DELIVERYLINE_FLD].ToString() != string.Empty)
				{
					// 04-04-2006 dungla: removed message fix bug 3945 for NganNT: display error message twice
					//PCSMessageBox.Show(ErrorCode.MESSAGE_PO_POLINE_HAS_DELIVERY, MessageBoxIcon.Error);
					return true;
				}
			}
			return false;
		}

		private int FillExchangeRate(int pintCurrencyID)
		{
			// Fill Exch. Rate if the system configured the exchange rate get form Exchange Rate Table
			// based on Currency and transaction date (begin date<= transaction date <= end date and approved)
			const decimal DEFAULT_RATE = 1;
			const string METHOD_NAME = THIS + ".FillExchangeRate()";
			int intExchangeRateID = 0;
			if (pintCurrencyID == 0) return intExchangeRateID;
			//•	If the Currency is same as base(Home - CuongNT fixed) Currency then the system automatically fill the number 1 to exchange rate field
			if(pintCurrencyID == SystemProperty.HomeCurrencyID)
			{
				txtExchRate.Value = DEFAULT_RATE;
				txtExchRate.ReadOnly = true;
				return intExchangeRateID;
			}
			try
			{
				if(cboOrderDate.Value == null)
				{
					// Input Transaction date before execute this function
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_TRANSACTION_BEFORE,MessageBoxIcon.Exclamation);
					txtExchRate.Value = DEFAULT_RATE;
					txtExchRate.ReadOnly = false;
					cboOrderDate.Focus();
					return intExchangeRateID;
				}
				DateTime dtmOrderDate = (DateTime)cboOrderDate.Value;
				PurchaseOrderBO boOrder = new PurchaseOrderBO();
				MST_ExchangeRateVO voExchange = (MST_ExchangeRateVO) boOrder.GetExchangeRate(pintCurrencyID,dtmOrderDate);
				if(voExchange.ExchangeRateID == 0)
				{
					// Do not found any Exchange Rate records which (begin effective date <= the current date <= end of effective date)
					PCSMessageBox.Show(ErrorCode.MESSAGE_NOT_FOUND_EXCHANGE_RATE,MessageBoxIcon.Exclamation);
					txtExchRate.Value = DEFAULT_RATE;
					txtExchRate.ReadOnly = false;
					txtExchRate.Focus();
					return intExchangeRateID;
				}
				// fill value and return
				intExchangeRateID = voExchange.ExchangeRateID;
				txtExchRate.Value = voExchange.Rate;
				txtExchRate.ReadOnly = false;
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
			if(dstPODetail.Tables.Count > 0)
				foreach (DataRow objRow in dstPODetail.Tables[0].Rows)
				{
					if(objRow.RowState == DataRowState.Deleted) continue;
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
			return decimal.Round(decTotal,DECIMALS);
		}

		private bool IsInCorrectDetail()
		{
			foreach (DataRow objRow in dstPODetail.Tables[0].Rows)
			{
				if(objRow.RowState == DataRowState.Deleted) continue;
				if(objRow[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] == DBNull.Value)
				{
					// Please input Item field for each records.
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ITEM_FIELD,MessageBoxIcon.Exclamation);
					return true;
				}
				if(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] == DBNull.Value)
				{
					// Please input Order quantity field for each records.
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ORDER_QUANTITY_FIELD,MessageBoxIcon.Exclamation);
					return true;
				}
				if(decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString()) < 0)
				{
					// Order quantity field must be higher than 0.
					PCSMessageBox.Show(ErrorCode.MESSAGE_POSITIVE_ORDER_QUANTITY,MessageBoxIcon.Exclamation);
					return true;
				}
				if(objRow[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] == DBNull.Value)
				{
					// Please input Unit of measure field for each records.
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_OF_MEASURE_FIELD,MessageBoxIcon.Exclamation);
					return true;
				}
				if(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD] == DBNull.Value)
				{
					// Please input Unit price field for each records.
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_PRICE_FIELD,MessageBoxIcon.Exclamation);
					return true;
				}
			}
			return false;
		}

		public void LoadMaster(int pintID)
		{
			const string CURRENCY_CODE = "CURRENCY_CODE";
			const string DELIVERYTERM_CODE = "DELIVERYTERM_CODE";
			const string PAYMENTTERM_CODE = "PAYMENTTERM_CODE";
			const string CARRIER_CODE = "CARRIER_CODE";
			const string EMPLOYEE_CODE = "EMPLOYEE_CODE";
			const string PARTY_CODE = "PARTY_CODE";
			const string PARTY_NAME = "PARTY_NAME";
			const string VENDORLOC_CODE = "VENDORLOC_CODE";
			const string SHIPTOLOC_CODE = "SHIPTOLOC_CODE";
			const string INVTOLOC_CODE = "INVTOLOC_CODE";
			const string PARTYCONTACT_CODE = "PARTYCONTACT_CODE";
			const string PURCHASETYPE_CODE = "PURCHASETYPE_CODE";
			const string DISCOUNTTERM_CODE = "DISCOUNTTERM_CODE";
			const string PAUSE_CODE = "PAUSE_CODE";
			const string MAKER_CODE = "MakerCode";
			const string MAKER_NAME = "MakerName";
			const string MAKERLOCATION_CODE = "MakerLocationCode";

			// const string METHOD_NAME = THIS + ".LoadMaster()";
			PurchaseOrderBO boSO = new PurchaseOrderBO();
			DataRow drowMaster = boSO.LoadObjectVO(pintID);

			txtOrderNo.Text = drowMaster[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();	
			txtCurrency.Text = drowMaster[CURRENCY_CODE].ToString();
			txtVendor.Text = drowMaster[PARTY_CODE].ToString();
			txtVendorName.Text = drowMaster[PARTY_NAME].ToString();
			txtVendorLoc.Text = drowMaster[VENDORLOC_CODE].ToString();
			txtMakerCode.Text = drowMaster[MAKER_CODE].ToString();
			txtMakerCode.Tag = drowMaster[PO_PurchaseOrderMasterTable.MAKERID_FLD];
			txtMakerName.Text = drowMaster[MAKER_NAME].ToString();
			txtMakerLoc.Text = drowMaster[MAKERLOCATION_CODE].ToString();
			txtMakerLoc.Tag = drowMaster[PO_PurchaseOrderMasterTable.MAKERLOCATIONID_FLD];
			txtPricingType.Text = drowMaster["PricingTypeCode"].ToString();
			txtPricingType.Tag = drowMaster[PO_PurchaseOrderMasterTable.PRICINGTYPEID_FLD];
			if (drowMaster[PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD] != DBNull.Value)
			{
				txtDeliveryTime.Value = Convert.ToInt32(drowMaster[PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD]);
			}
			else
				txtDeliveryTime.Value = null;	
			txtContact.Text = drowMaster[PARTYCONTACT_CODE].ToString();
			txtBuyer.Text = drowMaster[EMPLOYEE_CODE].ToString();
			txtShipToLoc.Text = drowMaster[SHIPTOLOC_CODE].ToString();
			txtInvToLoc.Text = drowMaster[INVTOLOC_CODE].ToString();
			txtPOType.Text = drowMaster[PURCHASETYPE_CODE].ToString();
			txtDiscTerms.Text = drowMaster[DISCOUNTTERM_CODE].ToString();
			txtDeliveryTerms.Text = drowMaster[DELIVERYTERM_CODE].ToString();
			txtPayTerms.Text = drowMaster[PAYMENTTERM_CODE].ToString();
			txtCarrier.Text = drowMaster[CARRIER_CODE].ToString();
			txtPause.Text = drowMaster[PAUSE_CODE].ToString();
			txtExchRate.Value = drowMaster[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD];
			if (drowMaster[PO_PurchaseOrderMasterTable.POREVISION_FLD] != DBNull.Value)
				txtRevision.Value = Convert.ToInt32(drowMaster[PO_PurchaseOrderMasterTable.POREVISION_FLD]);
			else
				txtRevision.Value = DBNull.Value;
			if (drowMaster[PO_PurchaseOrderMasterTable.REFERENCENO_FLD] != DBNull.Value)
			{
				txtReferenceNo.Text = drowMaster[PO_PurchaseOrderMasterTable.REFERENCENO_FLD].ToString();
			}
			else
			{
				txtReferenceNo.Text = string.Empty;
			}
			voPOMaster = new PO_PurchaseOrderMasterVO();

			if(drowMaster[PO_PurchaseOrderMasterTable.PRICINGTYPEID_FLD] != DBNull.Value)
			{
				voPOMaster.PricingTypeID = Convert.ToInt32(drowMaster[PO_PurchaseOrderMasterTable.PRICINGTYPEID_FLD]);
			}

			voPOMaster.PurchaseOrderMasterID = pintID;
			if(drowMaster[PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD] != DBNull.Value)
				voPOMaster.MasterLocationID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.POREVISION_FLD] != DBNull.Value)
				voPOMaster.PORevision = Convert.ToInt32(drowMaster[PO_PurchaseOrderMasterTable.POREVISION_FLD]);
			else
				voPOMaster.PORevision = 0;
				
			voPOMaster.PartyID = (drowMaster[PO_PurchaseOrderMasterTable.PARTYID_FLD] == DBNull.Value) ? 0 : int.Parse(drowMaster[PO_PurchaseOrderMasterTable.PARTYID_FLD].ToString());
			voPOMaster.Code = drowMaster[PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
			if(drowMaster[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD] != DBNull.Value)
				voPOMaster.ExchangeRate = decimal.Parse(drowMaster[PO_PurchaseOrderMasterTable.EXCHANGERATE_FLD].ToString());
			voPOMaster.VendorSO = drowMaster[PO_PurchaseOrderMasterTable.VENDORSO_FLD].ToString();
			if (drowMaster[PO_PurchaseOrderMasterTable.SPECIALTAX_FLD] != DBNull.Value)
				voPOMaster.SpecialTax = bool.Parse(drowMaster[PO_PurchaseOrderMasterTable.SPECIALTAX_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.IMPORTTAX_FLD] != DBNull.Value)
				voPOMaster.ImportTax = bool.Parse(drowMaster[PO_PurchaseOrderMasterTable.IMPORTTAX_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.IMPORTTAX_FLD] != DBNull.Value)
				voPOMaster.VAT = bool.Parse(drowMaster[PO_PurchaseOrderMasterTable.IMPORTTAX_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD] != DBNull.Value)
				voPOMaster.RecCompleted = bool.Parse(drowMaster[PO_PurchaseOrderMasterTable.RECCOMPLETED_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD] != DBNull.Value)
				voPOMaster.VendorLocID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.VENDORLOCID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD] != DBNull.Value)
				voPOMaster.ShipToLocID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.CURRENCYID_FLD] != DBNull.Value)
				voPOMaster.CurrencyID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.CURRENCYID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD] != DBNull.Value)
				voPOMaster.PartyContactID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.PARTYCONTACTID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD] != DBNull.Value)
				voPOMaster.DeliveryTermsID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.DELIVERYTERMSID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD] != DBNull.Value)
				voPOMaster.PaymentTermsID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.PAYMENTTERMSID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.CARRIERID_FLD] != DBNull.Value)
				voPOMaster.CarrierID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.CARRIERID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.PAUSEID_FLD] != DBNull.Value)
				voPOMaster.PauseID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.PAUSEID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD] != DBNull.Value)
				voPOMaster.DiscountTermID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD] != DBNull.Value)
				voPOMaster.PurchaseTypeID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.PURCHASETYPEID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.INVTOLOCID_FLD] != DBNull.Value)
				voPOMaster.InvToLocID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.INVTOLOCID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD] != DBNull.Value)
				voPOMaster.ShipToLocID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.SHIPTOLOCID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.BUYERID_FLD] != DBNull.Value)
				voPOMaster.BuyerID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.BUYERID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.CCNID_FLD] != DBNull.Value)
				voPOMaster.CCNID = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.CCNID_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.PRIORITY_FLD] != DBNull.Value)
				voPOMaster.Priority = int.Parse(drowMaster[PO_PurchaseOrderMasterTable.PRIORITY_FLD].ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.ORDERDATE_FLD] != DBNull.Value)
				voPOMaster.OrderDate = (DateTime)drowMaster[PO_PurchaseOrderMasterTable.ORDERDATE_FLD];
			if(txtMakerCode.Tag != DBNull.Value)
				voPOMaster.MakerID = int.Parse(txtMakerCode.Tag.ToString());
			if(txtMakerLoc.Tag != DBNull.Value)
			voPOMaster.MakerLocationID = int.Parse(txtMakerLoc.Tag.ToString());
			if (drowMaster[PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD] != DBNull.Value)
			{
				voPOMaster.RequestDeliveryTime = Convert.ToInt32(drowMaster[PO_PurchaseOrderMasterTable.REQUESTDELIVERYTIME_FLD]);
			}
			voPOMaster.ReferenceNo = drowMaster[PO_PurchaseOrderMasterTable.REFERENCENO_FLD].ToString();
			// If selected discount terms
			if (drowMaster[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD] != DBNull.Value)
			{
				DataRow drolResult = GetDataRow(MST_DiscountTermTable.DISCOUNTTERMID_FLD + "," + MST_DiscountTermTable.CODE_FLD + "," + MST_DiscountTermTable.RATE_FLD,
					drowMaster[PO_PurchaseOrderMasterTable.DISCOUNTTERMID_FLD].ToString(), 
					MST_DiscountTermTable.TABLE_NAME,MST_DiscountTermTable.DISCOUNTTERMID_FLD,string.Empty);
				decDiscountRate = decimal.Parse(drolResult[MST_DiscountTermTable.RATE_FLD].ToString());
			}
			else decDiscountRate = 0;
				
			VOToControls(voPOMaster);
			//SetEnableButtons();
			btnDeliverySchedule.Enabled = true;
			btnAdditionCharges.Enabled = true;
			btnEdit.Enabled = true;
			btnDelete.Enabled = true;
			dstPODetail.EnforceConstraints = false;
		}
		private bool IsApproved()
		{
			if(dstPODetail.Tables.Count == 0) return false;
			int intCount = 0;
			for (int i = 0; i < gridData.RowCount; i++)
			{
				if(gridData[i, PO_PurchaseOrderDetailTable.APPROVERID_FLD] != DBNull.Value)
				{
					intCount++;
				}
			}
			if (intCount == gridData.RowCount && gridData.RowCount > 0) return true;
			return false;
		}
		
		private DataRow GetDataRow(string pstrListFields,string pstrValue,string pstrTable,string pstrField,string pstrCodition)
		{
			PurchaseOrderBO boOrder = new PurchaseOrderBO();
			return boOrder.GetDataRow(pstrListFields,pstrValue,pstrTable,pstrField,pstrCodition);
		}

		private void PurchaseOrder_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F12)
			{
				if(mFormAction == EnumAction.Edit)
				{
					if(!gridData.AllowAddNew)
					{
						OnGridDataChangeAllowAddNew(true);
					}
				}
				gridData.Focus();
				gridData.Row = dstPODetail.Tables[0].Rows.Count;
				gridData.Col = dstPODetail.Tables[0].Columns.IndexOf(ITM_ProductTable.CODE_FLD);
			}
		}

		private void gridData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".gridData_AfterColUpdate()";
			try
			{
				gridData.UpdateData();
				// no process for not visible field and readonly
				if(!gridData.Splits[0].DisplayColumns[gridData.Col].Visible) return;
				if(gridData.Splits[0].DisplayColumns[gridData.Col].Locked) return;
				// check condition to calculate
				if(gridData.AllowUpdate == false) return;
				// no table
				if(dstPODetail.Tables.Count == 0) return;
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
							gridData[gridData.Row,PO_PurchaseOrderDetailTable.STOCKUMID_FLD] = String.Empty;
							gridData[gridData.Row,PO_PurchaseOrderDetailTable.UMRATE_FLD] = String.Empty;
							gridData[gridData.Row,ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = string.Empty;
							gridData[gridData.Row,ITM_CategoryTable.CATEGORYID_FLD] = string.Empty;

							//HACK: added by Tuan TQ: 19 Jul, 2006: Update UnitPrice field
							gridData[gridData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = DBNull.Value;
							//End hack
						}
						else
						{
							if (e.Column.DataColumn.Tag != null)
							{
								DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
								gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD] = drvResult[ITM_ProductTable.PRODUCTID_FLD];
								
								

								PurchaseOrderBO boOrder = new PurchaseOrderBO();
								ITM_ProductVO voProduct = (ITM_ProductVO)boOrder.GetProductVO(int.Parse(gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD].ToString()));
								if (voProduct.CategoryID > 0)
								{
									// category information
									ProductItemInfoBO boItem = new ProductItemInfoBO();
									gridData[gridData.Row,ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = boItem.GetCategoryCodeByProductID(voProduct.ProductID);
									gridData[gridData.Row,ITM_CategoryTable.CATEGORYID_FLD] = voProduct.CategoryID;
								}
								else
								{
									gridData[gridData.Row,ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = string.Empty;
									gridData[gridData.Row,ITM_CategoryTable.CATEGORYID_FLD] = null;
								}
								gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = voProduct.Code;
								gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = voProduct.Description;
								gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = voProduct.Revision;
//								if (gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] == DBNull.Value ||
//									gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].ToString() == string.Empty)
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] = int.Parse(drvResult[ITM_ProductTable.BUYINGUMID_FLD].ToString());
								gridData[gridData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = ((MST_UnitOfMeasureVO)boOrder.GetUnitOfMeasure(voProduct.BuyingUMID)).Code;
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.STOCKUMID_FLD] = int.Parse(drvResult[ITM_ProductTable.STOCKUMID_FLD].ToString());

								#region UM Rate
                                
								int intBuyingUMID = 0;
								try
								{
									intBuyingUMID = int.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].ToString());
									if (intBuyingUMID > 0)
									{
										decimal decUMRate = boUtil.GetUMRate(intBuyingUMID, voProduct.StockUMID);
										gridData[gridData.Row,PO_PurchaseOrderDetailTable.UMRATE_FLD] = decUMRate;
									}
								}
								catch{}

								#endregion

								int intMaxLine = 0;
								if(gridData.Row > 0) intMaxLine = int.Parse(gridData[gridData.Row - 1,PO_PurchaseOrderDetailTable.LINE_FLD].ToString());
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.LINE_FLD] = ++intMaxLine;
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = 0;

								//HACK: added by Tuan TQ: 19 Jul, 2006: Update UnitPrice field
								//Rem by Tuan TQ
								//gridData[gridData.Row,PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = 0;
								if(drvResult[ITM_ProductTable.LISTPRICE_FLD].Equals(DBNull.Value) || drvResult[ITM_ProductTable.LISTPRICE_FLD].ToString().Equals(string.Empty))
								{
									gridData[gridData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = Decimal.Zero;
								}
								else
								{
									gridData[gridData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = drvResult[ITM_ProductTable.LISTPRICE_FLD];
								}
								//End hack

								
								if(chkVAT.Checked)
								{
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.VAT_FLD] = voProduct.VAT;
								}
								if(chkImportTax.Checked)
								{
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAX_FLD] = voProduct.ImportTax;
								}
								if(chkSpecialTax.Checked)
								{
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.SPECIALTAX_FLD] = voProduct.SpecialTax;
								}
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
							gridData[gridData.Row,PO_PurchaseOrderDetailTable.STOCKUMID_FLD] = String.Empty;
							gridData[gridData.Row,PO_PurchaseOrderDetailTable.UMRATE_FLD] = String.Empty;
							gridData[gridData.Row,ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = string.Empty;
							gridData[gridData.Row,ITM_CategoryTable.CATEGORYID_FLD] = string.Empty;

							//HACK: added by Tuan TQ: 19 Jul, 2006: Update UnitPrice field
							gridData[gridData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = DBNull.Value;
							//End hack
						}
						else
						{
							if (e.Column.DataColumn.Tag != null)
							{
								DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
								gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD] = drvResult[ITM_ProductTable.PRODUCTID_FLD];
								
								

								PurchaseOrderBO boOrder = new PurchaseOrderBO();
								ITM_ProductVO voProduct = (ITM_ProductVO)boOrder.GetProductVO(int.Parse(gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD].ToString()));
								if (voProduct.CategoryID > 0)
								{
									// category information
									ProductItemInfoBO boItem = new ProductItemInfoBO();
									gridData[gridData.Row,ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = boItem.GetCategoryCodeByProductID(voProduct.ProductID);
									gridData[gridData.Row,ITM_CategoryTable.CATEGORYID_FLD] = voProduct.CategoryID;
								}
								else
								{
									gridData[gridData.Row,ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = string.Empty;
									gridData[gridData.Row,ITM_CategoryTable.CATEGORYID_FLD] = null;
								}
								gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = voProduct.Code;
								gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = voProduct.Description;
								gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = voProduct.Revision;
//								if (gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] == DBNull.Value ||
//									gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].ToString() == string.Empty)
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] = int.Parse(drvResult[ITM_ProductTable.BUYINGUMID_FLD].ToString());
								gridData[gridData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = ((MST_UnitOfMeasureVO)boOrder.GetUnitOfMeasure(voProduct.BuyingUMID)).Code;
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.STOCKUMID_FLD] = int.Parse(drvResult[ITM_ProductTable.STOCKUMID_FLD].ToString());

								#region UM Rate
                                
								int intBuyingUMID = 0;
								try
								{
									intBuyingUMID = int.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].ToString());
									if (intBuyingUMID > 0)
									{
										decimal decUMRate = boUtil.GetUMRate(intBuyingUMID, voProduct.StockUMID);
										gridData[gridData.Row,PO_PurchaseOrderDetailTable.UMRATE_FLD] = decUMRate;
									}
								}
								catch{}

								#endregion

								int intMaxLine = 0;
								if(gridData.Row > 0) intMaxLine = int.Parse(gridData[gridData.Row - 1,PO_PurchaseOrderDetailTable.LINE_FLD].ToString());
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.LINE_FLD] = ++intMaxLine;
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = 0;

								//HACK: added by Tuan TQ: 19 Jul, 2006: Update UnitPrice field
								//gridData[gridData.Row,PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = 0;
								if(drvResult[ITM_ProductTable.LISTPRICE_FLD].Equals(DBNull.Value) || drvResult[ITM_ProductTable.LISTPRICE_FLD].ToString().Equals(string.Empty))
								{
									gridData[gridData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = Decimal.Zero;
								}
								else
								{
									gridData[gridData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = drvResult[ITM_ProductTable.LISTPRICE_FLD];
								}
								//End hack
								
								if(chkVAT.Checked)
								{
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.VAT_FLD] = voProduct.VAT;
								}
								if(chkImportTax.Checked)
								{
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAX_FLD] = voProduct.ImportTax;
								}
								if(chkSpecialTax.Checked)
								{
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.SPECIALTAX_FLD] = voProduct.SpecialTax;
								}
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
							gridData[gridData.Row,PO_PurchaseOrderDetailTable.STOCKUMID_FLD] = String.Empty;
							gridData[gridData.Row,PO_PurchaseOrderDetailTable.UMRATE_FLD] = String.Empty;
							gridData[gridData.Row,ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = string.Empty;
							gridData[gridData.Row,ITM_CategoryTable.CATEGORYID_FLD] = string.Empty;

							//HACK: added by Tuan TQ: 19 Jul, 2006: Update UnitPrice field
							gridData[gridData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = DBNull.Value;
							//End hack
						}
						else
						{
							if (e.Column.DataColumn.Tag != null)
							{
								DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
								gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD] = drvResult[ITM_ProductTable.PRODUCTID_FLD];
								
								

								PurchaseOrderBO boOrder = new PurchaseOrderBO();
								ITM_ProductVO voProduct = (ITM_ProductVO)boOrder.GetProductVO(int.Parse(gridData[gridData.Row,ITM_ProductTable.PRODUCTID_FLD].ToString()));
								if (voProduct.CategoryID > 0)
								{
									// category information
									ProductItemInfoBO boItem = new ProductItemInfoBO();
									gridData[gridData.Row,ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = boItem.GetCategoryCodeByProductID(voProduct.ProductID);
									gridData[gridData.Row,ITM_CategoryTable.CATEGORYID_FLD] = voProduct.CategoryID;
								}
								else
								{
									gridData[gridData.Row,ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = string.Empty;
									gridData[gridData.Row,ITM_CategoryTable.CATEGORYID_FLD] = null;
								}
								gridData[gridData.Row,ITM_ProductTable.CODE_FLD] = voProduct.Code;
								gridData[gridData.Row,ITM_ProductTable.DESCRIPTION_FLD] = voProduct.Description;
								gridData[gridData.Row,ITM_ProductTable.REVISION_FLD] = voProduct.Revision;
//								if (gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] == DBNull.Value ||
//									gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].ToString() == string.Empty)
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] = int.Parse(drvResult[ITM_ProductTable.BUYINGUMID_FLD].ToString());
								gridData[gridData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = ((MST_UnitOfMeasureVO)boOrder.GetUnitOfMeasure(voProduct.BuyingUMID)).Code;
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.STOCKUMID_FLD] = int.Parse(drvResult[ITM_ProductTable.STOCKUMID_FLD].ToString());

								#region UM Rate
                                
								int intBuyingUMID = 0;
								try
								{
									intBuyingUMID = int.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].ToString());
									if (intBuyingUMID > 0)
									{
										decimal decUMRate = boUtil.GetUMRate(intBuyingUMID, voProduct.StockUMID);
										gridData[gridData.Row,PO_PurchaseOrderDetailTable.UMRATE_FLD] = decUMRate;
									}
								}
								catch{}

								#endregion

								int intMaxLine = 0;
								if(gridData.Row > 0) intMaxLine = int.Parse(gridData[gridData.Row - 1,PO_PurchaseOrderDetailTable.LINE_FLD].ToString());
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.LINE_FLD] = ++intMaxLine;
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = 0;

								//HACK: added by Tuan TQ: 19 Jul, 2006: Update UnitPrice field
								//gridData[gridData.Row,PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = 0;
								if(drvResult[ITM_ProductTable.LISTPRICE_FLD].Equals(DBNull.Value) || drvResult[ITM_ProductTable.LISTPRICE_FLD].ToString().Equals(string.Empty))
								{
									gridData[gridData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = Decimal.Zero;
								}
								else
								{
									gridData[gridData.Row, PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = drvResult[ITM_ProductTable.LISTPRICE_FLD];
								}
								//End hack

								
								if(chkVAT.Checked)
								{
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.VAT_FLD] = voProduct.VAT;
								}
								if(chkImportTax.Checked)
								{
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAX_FLD] = voProduct.ImportTax;
								}
								if(chkSpecialTax.Checked)
								{
									gridData[gridData.Row,PO_PurchaseOrderDetailTable.SPECIALTAX_FLD] = voProduct.SpecialTax;
								}
							}
						}
						break;
					case MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD:
						if (strColumnValue == String.Empty)
						{
							gridData[gridData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = String.Empty;
							gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] = String.Empty;
							gridData[gridData.Row,PO_PurchaseOrderDetailTable.UMRATE_FLD] = DBNull.Value;
						}
						else
						{
							if (e.Column.DataColumn.Tag != null)
							{
								DataRowView drvResult = (DataRowView)e.Column.DataColumn.Tag;
								gridData[gridData.Row,MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drvResult[MST_UnitOfMeasureTable.CODE_FLD];
								gridData[gridData.Row,PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] = drvResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD];

								#region UM Rate
                                
								// get stock um id
								int intStockUMID = 0;
								try
								{
									intStockUMID = int.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.STOCKUMID_FLD].ToString());
									if (intStockUMID > 0)
									{
										decimal decUMRate = boUtil.GetUMRate(Convert.ToInt32(drvResult[MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD]), intStockUMID);
										gridData[gridData.Row,PO_PurchaseOrderDetailTable.UMRATE_FLD] = decUMRate;
									}
								}
								catch{}

								#endregion
							}
						}
						break;
				}


				// no product
				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.PRODUCTID_FLD] == DBNull.Value) return;
				// init value
				decimal decOrderQuantity = 0,decUnitPrice;
				try
				{
					//if(gridData.Row == 0) return;
					decOrderQuantity = decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString());
					// Check Order quantity
					if ((gridData.Col == dstPODetail.Tables[0].Columns.IndexOf(PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD)))
						gridData[gridData.Row,PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = decOrderQuantity;
					decUnitPrice = decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString());
					// Check unit price
					if ((gridData.Col == dstPODetail.Tables[0].Columns.IndexOf(PO_PurchaseOrderDetailTable.UNITPRICE_FLD)))
						gridData[gridData.Row,PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = decUnitPrice;
					decAmount = decOrderQuantity * decUnitPrice;
				}
				catch
				{
				}
				// col order quantity
			

				// col orderquantity, or unit price was change
				if((gridData.Col == dstPODetail.Tables[0].Columns.IndexOf(PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD))
					||(gridData.Col == dstPODetail.Tables[0].Columns.IndexOf(PO_PurchaseOrderDetailTable.UNITPRICE_FLD)))
				{
					try
					{
						gridData[gridData.Row,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] = (decDiscountRate * decAmount)/ONE_HUNDRED;
					}
					catch {}
					// sum VAT amount,Imp amount, Spe amount, total amount, net amount column
					CalculateNetAmount(decAmount);
					CalculateVATAmount(decAmount);
					CalculateImpAmount(decAmount);
					CalculateSpecAmount(decAmount);
					CalculateTotalAmount(decAmount);
					// Sum all for lable
					Calculate();
					return;
				
				}
				// col VAT was change
				if(gridData.Col == dstPODetail.Tables[0].Columns.IndexOf(PO_PurchaseOrderDetailTable.VAT_FLD))
				{
					// recalculate VATAMount, TotalAmount, Special Tax amount column
					CalculateVATAmount(decAmount);
					CalculateSpecAmount(decAmount);
					CalculateTotalAmount(decAmount);
					//CalculateNetAmount(decAmount);
					// Sum all for lable
					Calculate();
					return;
				
				}
				// VATAMOUNT was change
				if(gridData.Col == dstPODetail.Tables[0].Columns.IndexOf(PO_PurchaseOrderDetailTable.VATAMOUNT_FLD))
				{
					// recalculate TotalAmount, Special Tax amount column
					//					CalculateVAT(decAmount);
					CalculateSpecAmount(decAmount);
					CalculateTotalAmount(decAmount);
					
					//CalculateNetAmount(decAmount);
					// Sum all for lable
					Calculate();
					return;
				
				}
				// %Import was change
				if(gridData.Col == dstPODetail.Tables[0].Columns.IndexOf(PO_PurchaseOrderDetailTable.IMPORTTAX_FLD))
				{
					// recalculate ImportAmount,TotalAmount,Special Tax amount column
					CalculateImpAmount(decAmount);
					CalculateSpecAmount(decAmount);
					CalculateTotalAmount(decAmount);
					
					// Sum all for lable
					Calculate();
					return;
				
				}
				// Import Amount was change
				if(gridData.Col == dstPODetail.Tables[0].Columns.IndexOf(PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD))
				{
					// recalculate TotalAmount, Special Tax amount column
					//					CalculateImp(decAmount);
					CalculateSpecAmount(decAmount);
					CalculateTotalAmount(decAmount);
					
					// Sum all for lable
					Calculate();
					return;
				
				}
				// %Special was change
				if(gridData.Col == dstPODetail.Tables[0].Columns.IndexOf(PO_PurchaseOrderDetailTable.SPECIALTAX_FLD))
				{
					// recalculate Special Amount,TotalAmount column
					CalculateSpecAmount(decAmount);
					CalculateTotalAmount(decAmount);
					//CalculateNetAmount(decAmount);
					// Sum all for lable
					Calculate();
					return;
				}
				// Special Amount was change
				if(gridData.Col == dstPODetail.Tables[0].Columns.IndexOf(PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD))
				{
					// recalculate %Special,TotalAmount column
					//					CalculateSpec(decAmount);
					CalculateTotalAmount(decAmount);
					//CalculateNetAmount(decAmount);
					// Sum all for lable
					Calculate();
					return;
				}
				// Discount Amount was change
				if(gridData.Col == dstPODetail.Tables[0].Columns.IndexOf(PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD))
				{
					// sum VAT amount,Imp amount, Spe amount, total amount, net amount column
					CalculateNetAmount(decAmount);
					CalculateVATAmount(decAmount);
					CalculateImpAmount(decAmount);
					CalculateSpecAmount(decAmount);
					CalculateTotalAmount(decAmount);

					// recalculate net amount column
					
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
				decimal decDiscountAmount = 0;
				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
					decDiscountAmount = decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());
				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.VAT_FLD] != DBNull.Value) 
				{
					gridData[gridData.Row,PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] =  ((pdecAmount - decDiscountAmount)
						* decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.VAT_FLD].ToString()))/ONE_HUNDRED;
				}
				else
				{
					gridData[gridData.Row,PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] = 0;
				}
			}
			catch
			{
				gridData[gridData.Row,PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] = 0;
			}
		}
		private void CalculateImpAmount(decimal pdecAmount)
		{
			try
			{
				if(!chkImportTax.Checked) return;
				decimal decDiscountAmount = 0;
				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
					decDiscountAmount = decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());

				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAX_FLD] != DBNull.Value)
				{
					gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] =  ((pdecAmount - decDiscountAmount)
						* decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].ToString()))/ONE_HUNDRED;
				}
				else
				{
					gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] = 0;
				}
			}
			catch
			{
				//dstPODetail.Tables[0].Rows[gridData.Row][PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] = 0;
				
			}
		}
		private void CalculateSpecAmount(decimal pdecAmount)
		{
			try
			{
				if(!chkSpecialTax.Checked) return;
				decimal decDiscountAmount, decVatAmount, decImpAmount;
				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
					decDiscountAmount = decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());
				else
					decDiscountAmount = 0;
				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value)
					decVatAmount = decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.VATAMOUNT_FLD].ToString());
				else 
					decVatAmount = 0;
				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] != DBNull.Value)
					decImpAmount = decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD].ToString());
				else 
					decImpAmount = 0;

				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.SPECIALTAX_FLD] != DBNull.Value)
				{
					gridData[gridData.Row,PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD] =  (((pdecAmount - decDiscountAmount) + decVatAmount + decImpAmount)
						* decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.SPECIALTAX_FLD].ToString()))/ONE_HUNDRED;
				}
				else
				{
					gridData[gridData.Row,PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD] = 0;
				}
			}
			catch
			{
				//dstPODetail.Tables[0].Rows[gridData.Row][PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD] = 0;
				
			}
		}
		private void CalculateTotalAmount(decimal pdecAmount)
		{
			try
			{
				decimal decVatAmount,decImpAmount,decSpecAmount, decNetAmount;
				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value) 
					decVatAmount = decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.VATAMOUNT_FLD].ToString());
				else decVatAmount = 0;
				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] != DBNull.Value)
					decImpAmount = decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD].ToString());
				else decImpAmount = 0;
				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD] != DBNull.Value)
					decSpecAmount = decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD].ToString());
				else decSpecAmount = 0;
				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.NETAMOUNT_FLD] != DBNull.Value)
					decNetAmount = decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.NETAMOUNT_FLD].ToString());
				else decNetAmount = 0;
				gridData[gridData.Row,PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD] =  
					decNetAmount + decVatAmount + decImpAmount + decSpecAmount;
			}
			catch
			{
				//dstPODetail.Tables[0].Rows[gridData.Row][PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD] = 0;
			}
		}

		private void CalculateNetAmount(decimal pdecAmount)
		{
			try
			{
				decimal decDiscountAmount = 0;
				if(gridData[gridData.Row,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
					decDiscountAmount = decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());
				gridData[gridData.Row,PO_PurchaseOrderDetailTable.NETAMOUNT_FLD] =
					//decimal.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].ToString()) - decDiscountAmount;
					pdecAmount - decDiscountAmount;
			}
			catch
			{
				//				dstPODetail.Tables[0].Rows[gridData.Row][PO_PurchaseOrderDetailTable.NETAMOUNT_FLD] = 
				//					gridData[gridData.Row,PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD];
			}
		}

		private void OnGridDataChangeAllowAddNew(bool pblnAllowAddNew)
		{
			bool blnOldValue = gridData.AllowAddNew;
			if(blnOldValue != pblnAllowAddNew)
			{
				gridData.AllowAddNew = pblnAllowAddNew;
				if(pblnAllowAddNew)
				{
					// do some thing
				}
			}
		}

		private void txtCurrency_Leave(object sender, EventArgs e)
		{
			OnLeaveControl(sender,e);
			TextBox txtText = (TextBox)sender;
			if(!txtText.Modified)return;
			if(txtText.Text.Trim() == string.Empty)
			{
				txtText.Tag = null;
				return;
			}
			/*
			if(IsUnValidateData(txtText,MST_CurrencyTable.TABLE_NAME,MST_CurrencyTable.CODE_FLD,string.Empty))
			{				
				btnCurrency_Click(sender,e);
			}
			else
			{
				DataTable dtResult = (new UtilsBO()).GetRows(MST_CurrencyTable.TABLE_NAME,MST_CurrencyTable.CODE_FLD,txtText.Text,null);
				txtText.Text = dtResult.Rows[0][MST_CurrencyTable.CODE_FLD].ToString();
				txtText.Tag = dtResult.Rows[0][MST_CurrencyTable.CURRENCYID_FLD];
				FillExchangeRate(int.Parse(txtText.Tag.ToString()));
			}
			*/
			DataRowView drowView = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME, MST_CurrencyTable.CODE_FLD, txtCurrency.Text, null, false);
			if(drowView != null)
			{
				txtCurrency.Tag = drowView[MST_CurrencyTable.CURRENCYID_FLD];
				txtCurrency.Text = drowView[MST_CurrencyTable.CODE_FLD].ToString();
				txtExchRate.Tag = FillExchangeRate(int.Parse(drowView[MST_CurrencyTable.CURRENCYID_FLD].ToString()));
			}
			else 
				txtCurrency.Focus();
		}

		private void txtVendorName_Leave(object sender, EventArgs e)
		{
			OnLeaveControl(sender,e);
			TextBox txtText = (TextBox)sender;
			if(!txtText.Modified)return;
			if(txtText.Text.Trim() == string.Empty)
			{
				txtVendor.Text = string.Empty;
				txtVendor.Tag = null;
				txtVendorLoc.Text = string.Empty;
				txtVendorLoc.Tag = null;
				txtContact.Text = string.Empty;
				txtContact.Tag = null;
				return;
			}
			htbCriteria = new Hashtable();
			htbCriteria.Add(VENDOR, 1);
			drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtVendorName.Text.Trim(), htbCriteria, false);
			if(drwResult != null)
			{
				txtVendor.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
				txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
				txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					
				txtVendorLoc.Text = string.Empty;
				txtVendorLoc.Tag = null;
				txtContact.Text = string.Empty;
				txtContact.Tag = null;
			}
			txtVendorName.Focus();
			
		}

		private void txtContact_Leave(object sender, EventArgs e)
		{
			OnLeaveControl(sender,e);
			TextBox txtText = (TextBox)sender;
			if(!txtText.Modified)return;
			if(txtText.Text.Trim() == string.Empty) 
			{
				txtText.Tag = null;
				return;
			}
			
			if(txtVendor.Tag == null)
			{
				// You have to select Vendor before select Vendor Location, please
				txtText.Text = string.Empty;
				PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_VENDOR_BEFORE_VENDORLOC,MessageBoxIcon.Warning);
				return;
			}
			if(txtVendor.Text != string.Empty)
			{
				if(txtVendor.Tag != null)
				{
					htbCriteria = new Hashtable();
					//strWhere = " WHERE " + MST_PartyContactTable.PARTYID_FLD + " = " + txtVendor.Tag.ToString();
					htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtVendor.Tag.ToString());
				}
			}
			else
			{
				// You have to select Vendor before select Vendor contact, please
				PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_VENDOR_BEFORE_VENDORLOC);
				return;
			}
			drwResult = FormControlComponents.OpenSearchForm(MST_PartyContactTable.TABLE_NAME, MST_PartyContactTable.CODE_FLD, txtContact.Text.Trim(), htbCriteria, false);
			if(drwResult != null)
			{
				txtContact.Tag = drwResult[MST_PartyContactTable.PARTYCONTACTID_FLD];
				txtContact.Text = drwResult[MST_PartyContactTable.CODE_FLD].ToString();
			}
			else 
				txtContact.Focus();
		}

		private void txtBuyer_Leave(object sender, EventArgs e)
		{
			OnLeaveControl(sender,e);
			TextBox txtText = (TextBox)sender;
			if(!txtText.Modified)return;
			if(txtText.Text.Trim() == string.Empty) 
			{
				txtText.Tag = null;
				return;
			}
			drwResult = FormControlComponents.OpenSearchForm(MST_EmployeeTable.TABLE_NAME, MST_EmployeeTable.CODE_FLD, txtBuyer.Text.Trim(), null, false);
			if(drwResult != null)
			{
				txtBuyer.Tag = drwResult[MST_EmployeeTable.EMPLOYEEID_FLD];
				txtBuyer.Text = drwResult[MST_EmployeeTable.CODE_FLD].ToString();
			}
			else
				txtBuyer.Focus();
			
		}

		private void txtDiscTerms_Leave(object sender, EventArgs e)
		{
			OnLeaveControl(sender,e);
			TextBox txtText = (TextBox)sender;
			if(!txtText.Modified)return;
            int intRowCount = gridData.RowCount;
			
			if(txtText.Text.Trim() == string.Empty)
			{
				txtText.Tag = null;
				decDiscountRate = 0;
				// update grid
				for(int i = 0; i < intRowCount; i++)
				{ 
					gridData[i,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] = DBNull.Value;
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
                intRowCount = gridData.RowCount;
				// update grid
				for(int i = 0; i < intRowCount; i++)
				{
					try
					{
						gridData[i,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] = decDiscountRate
							*decimal.Parse(gridData[i,PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString())
							*decimal.Parse(gridData[i,PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())/ONE_HUNDRED;
					}
					catch
					{
						gridData[i,PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] = 0;
					}
				}
				ReCalculate();
			}
			else
				txtDiscTerms.Focus();
			txtText.Modified = false;
		}

		private void txtDeliveryTerms_Leave(object sender, EventArgs e)
		{
			OnLeaveControl(sender,e);
			TextBox txtText = (TextBox)sender;
			if(!txtText.Modified)return;
			if(txtText.Text.Trim() == string.Empty) 
			{
				txtText.Tag = null;
				return;
			}

			drwResult = FormControlComponents.OpenSearchForm(MST_DeliveryTermTable.TABLE_NAME, MST_DeliveryTermTable.CODE_FLD, txtDeliveryTerms.Text.Trim(), null, false);
			if(drwResult != null)
			{
				txtDeliveryTerms.Tag = drwResult[MST_DeliveryTermTable.DELIVERYTERMID_FLD];
				txtDeliveryTerms.Text = drwResult[MST_DeliveryTermTable.CODE_FLD].ToString();
			}
			else
				txtDeliveryTerms.Focus();
		}

		private void txtPayTerms_Leave(object sender, EventArgs e)
		{
			OnLeaveControl(sender,e);
			TextBox txtText = (TextBox)sender;
			if(!txtText.Modified)return;
			if(txtText.Text.Trim() == string.Empty)
			{
				txtText.Tag = null;
				return;
			}

			drwResult = FormControlComponents.OpenSearchForm(MST_PaymentTermTable.TABLE_NAME, MST_PaymentTermTable.CODE_FLD, txtPayTerms.Text.Trim(), null, false);
			if(drwResult != null)
			{
				txtPayTerms.Tag = drwResult[MST_PaymentTermTable.PAYMENTTERMID_FLD];
				txtPayTerms.Text = drwResult[MST_PaymentTermTable.CODE_FLD].ToString();
			}
			else
				txtPayTerms.Focus();
		}

		private void txtCarrier_Leave(object sender, EventArgs e)
		{
			OnLeaveControl(sender,e);
			TextBox txtText = (TextBox)sender;
			if(!txtText.Modified)return;
			if(txtText.Text.Trim() == string.Empty)
			{
				txtText.Tag = null;
				return;
			}

			drwResult = FormControlComponents.OpenSearchForm(MST_CarrierTable.TABLE_NAME, MST_CarrierTable.CODE_FLD, txtCarrier.Text.Trim(), null, false);
			if(drwResult != null)
			{
				txtCarrier.Tag = drwResult[MST_CarrierTable.CARRIERID_FLD];
				txtCarrier.Text = drwResult[MST_CarrierTable.CODE_FLD].ToString();
			}
			else
				txtCarrier.Focus();
		}

		private void txtPause_Leave(object sender, EventArgs e)
		{
			OnLeaveControl(sender,e);
			TextBox txtText = (TextBox)sender;
			if(!txtText.Modified)return;
			if(txtText.Text.Trim() == string.Empty)			
			{
				txtText.Tag = null;
				return;
			}

			drwResult = FormControlComponents.OpenSearchForm(MST_PauseTable.TABLE_NAME, MST_PauseTable.CODE_FLD, txtPause.Text.Trim(), null, false);
			if(drwResult != null)
			{
				txtPause.Tag = drwResult[MST_PauseTable.PAUSEID_FLD];
				txtPause.Text = drwResult[MST_PauseTable.CODE_FLD].ToString();
			}
			else 
				txtPause.Focus();
		}

		private void txtVendorName_Enter(object sender, EventArgs e)
		{
//						if(sender.GetType().Equals(typeof(TextBox)))
//						{
//							TextBox txtText = (TextBox)sender;
//							strOldTextBoxValue = txtText.Text;
			//			}
			FormControlComponents.OnEnterControl(sender, e);
			//			Control obj = (Control)sender;
			//			obj.BackColor = Color.FromArgb((byte)Constants.BACKGROUND_COLOUR_R, (byte)Constants.BACKGROUND_COLOUR_G, (byte)Constants.BACKGROUND_COLOUR_B);
		}

		private void OnLeaveControl(object sender,EventArgs e)
		{
			//if(sender.GetType().Equals(typeof(TextBox)))
			//{
			//TextBox txtText = (TextBox)sender;
			//if(txtText.ReadOnly)
			//{
			//	txtText.BackColor = BackColor;
			//txtText.ForeColor = ForeColor;
			//}
			//else
			//{
			//	txtText.BackColor = Color.White;
			//txtText.ForeColor = Color.Black;
			FormControlComponents.OnLeaveControl(sender,e);
			//}
			return;
			//}
			//			if(sender.GetType().Equals(typeof(C1NumericEdit)))
			//			{
			//				C1NumericEdit txtText = (C1NumericEdit)sender;
			//				if(txtText.ReadOnly)
			//				{
			//					txtText.BackColor = BackColor;
			//					txtText.ForeColor = ForeColor;
			//				}
			//				else
			//				{
			//					FormControlComponents.OnLeaveControl(sender,e);
			//				}
			//				return;
			//			}
			//			if(sender.GetType().Equals(typeof(C1DateEdit)))
			//			{
			//				C1DateEdit txtText = (C1DateEdit)sender;
			//				if(txtText.ReadOnly)
			//				{
			//					txtText.BackColor = BackColor;
			//					txtText.ForeColor = ForeColor;
			//				}
			//				else
			//				{
			//					FormControlComponents.OnLeaveControl(sender,e);
			//				}
			//				return;
			//			}
			//			if(sender.GetType().Equals(typeof(CheckBox)))
			//			{
			//				//CheckBox chkCheck = (CheckBox)sender;
			//				//if(txtText.ReadOnly)
			//				//{
			//				//	txtText.BackColor = BackColor;
			//				//	txtText.ForeColor = ForeColor;
			//				//}
			//				//else
			//				//{
			//				//	FormControlComponents.OnLeaveControl(sender,e);
			//				//}
			//
			//				return;
			//			}
			
		}

		public void PODetailForApprover(int pintPOMasterID)
		{
			mFormAction = EnumAction.Default;
			LoadMaster(pintPOMasterID);
			txtOrderNo.Enabled = false;
			btnOrderNo.Enabled = false;
			SetEnableButtons();
			btnDeliverySchedule.Enabled = true;
			btnAdditionCharges.Enabled = true;
		}
		private void gridData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
		{
			string strColumnName = e.Column.DataColumn.DataField;
			string strColumValue = e.Column.DataColumn.Text.Trim(); 
			DataTable dtResult;
			DataRowView drvResult = null;
			UtilsBO objUtilsBO = new UtilsBO();
			switch (strColumnName)
			{
				case ITM_ProductTable.CODE_FLD:
					if (strColumValue == String.Empty)
						break;
					drvResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.CODE_FLD,strColumValue, null, false);
					if (drvResult != null)
						e.Column.DataColumn.Tag = drvResult;
					else
						e.Cancel = true;
					break;
				case ITM_ProductTable.DESCRIPTION_FLD:
					if (strColumValue == String.Empty)
						break;
					drvResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.DESCRIPTION_FLD,strColumValue,null, false);
					if (drvResult != null)
						e.Column.DataColumn.Tag = drvResult;
					else
						e.Cancel = true;
					break;
				case ITM_ProductTable.REVISION_FLD:
					if (strColumValue == String.Empty)
						break;
					drvResult = FormControlComponents.OpenSearchForm(ITM_ProductTable.TABLE_NAME,ITM_ProductTable.REVISION_FLD,strColumValue,null, false);
					if (drvResult != null)
						e.Column.DataColumn.Tag = drvResult;
					else
						e.Cancel = true;
					break;
				case MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD:
					if (strColumValue == String.Empty)
						break;
					drvResult = FormControlComponents.OpenSearchForm(MST_UnitOfMeasureTable.TABLE_NAME,MST_UnitOfMeasureTable.CODE_FLD,strColumValue,null, false);
					if (drvResult != null)
						e.Column.DataColumn.Tag = drvResult;
					else
						e.Cancel = true;
					break;
				case PO_PurchaseOrderDetailTable.ADJUSTMENT1_FLD:
					try
					{
						if(e.Column.DataColumn.Text.Length > 0)
							decimal.Parse(e.Column.DataColumn.Text);
					}
					catch
					{
						e.Cancel = true;
					}
					break;
				case PO_PurchaseOrderDetailTable.ADJUSTMENT2_FLD:
					try
					{
						if(e.Column.DataColumn.Text.Length > 0)
							decimal.Parse(e.Column.DataColumn.Text);
					}
					catch
					{
						e.Cancel = true;
					}
					break;

				case PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD:
					try
					{
						if (e.Column.DataColumn.Text == string.Empty)
							return;
						
						#region 04-05-2006 dungla: fix bug 3942 for NganNT
                    
						// 04-05-2006 dungla: fix bug 3942 for NganNT
						// don't allow user modify Order Quantity < Sum of Delivery Quantity
						int intPODetailID = 0;
						try
						{
							intPODetailID = Convert.ToInt32(gridData[gridData.Row, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD]);
						}
						catch{}
						// get sum of delivery quantity of current line
						if (mFormAction == EnumAction.Edit && gridData.AddNewMode == AddNewModeEnum.NoAddNew && intPODetailID > 0)
						{
							PODeliveryScheduleBO boPOSchedule = new PODeliveryScheduleBO();
							decimal decTotalDeliveryQuantity = boPOSchedule.GetTotalDeliveryQuantityOfLine(intPODetailID);
							decimal decOrderQuantity = Convert.ToDecimal(e.Column.DataColumn.Text);
							if (decOrderQuantity < decTotalDeliveryQuantity)
							{
								string[] msg = {gridData.Columns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].Caption,
												lblTotalDeliveryScheduleQuantity.Text};
								PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, msg);
								e.Cancel = true;
								return;
							}
						}
						// 04-05-2006 dungla: fix bug 3942 for NganNT

						#endregion
						
						// don't allow user modify Order Quantity < Sum of Delivery Quantity
						// get sum of receipt quantity of current line
						if (mFormAction == EnumAction.Edit && gridData.AddNewMode == AddNewModeEnum.NoAddNew && intPODetailID > 0)
						{
							POPurchaseOrderReceiptsBO boPurchaseOrderReceipts = new POPurchaseOrderReceiptsBO();
							decimal decTotalReceiptQuantity = boPurchaseOrderReceipts.GetTotalReceiveQuantity(intPODetailID);
							decimal decOrderQuantity = Convert.ToDecimal(e.Column.DataColumn.Text);
							if (decOrderQuantity < decTotalReceiptQuantity)
							{
								string[] msg = {gridData.Columns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].Caption,
												   "Receipt Quantity"};
								PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, msg);
								e.Cancel = true;
								return;
							}
						}
						
					}
					catch
					{
						e.Cancel = true;
					}
					break;
				case PO_PurchaseOrderDetailTable.UNITPRICE_FLD:
					try
					{
						if (e.Column.DataColumn.Text == string.Empty)
							return;
						decimal.Parse(e.Column.DataColumn.Text);
					}
					catch
					{
						e.Cancel = true;
					}
					break;
				case PO_PurchaseOrderDetailTable.VAT_FLD:
					try
					{
						if (e.Column.DataColumn.Text == string.Empty)
							return;
						decimal.Parse(e.Column.DataColumn.Text);
					}
					catch
					{
						e.Cancel = true;
					}
					break;
				case PO_PurchaseOrderDetailTable.VATAMOUNT_FLD:
					try
					{
						if (e.Column.DataColumn.Text == string.Empty)
							return;
						decimal.Parse(e.Column.DataColumn.Text);
					}
					catch
					{
						e.Cancel = true;
					}
					break;
				case PO_PurchaseOrderDetailTable.IMPORTTAX_FLD:
					try
					{
						if (e.Column.DataColumn.Text == string.Empty)
							return;
						decimal.Parse(e.Column.DataColumn.Text);
					}
					catch
					{
						e.Cancel = true;
					}
					break;
				case PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD:
					try
					{
						if (e.Column.DataColumn.Text == string.Empty)
							return;
						decimal.Parse(e.Column.DataColumn.Text);
					}
					catch
					{
						e.Cancel = true;
					}
					break;
				case PO_PurchaseOrderDetailTable.SPECIALTAX_FLD:
					try
					{
						if (e.Column.DataColumn.Text == string.Empty)
							return;
						decimal.Parse(e.Column.DataColumn.Text);
					}
					catch
					{
						e.Cancel = true;
					}
					break;
				case PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD:
					try
					{
						if (e.Column.DataColumn.Text == string.Empty)
							return;
						decimal.Parse(e.Column.DataColumn.Text);
					}
					catch
					{
						e.Cancel = true;
					}
					break;
				case PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD:
					try
					{
						if (e.Column.DataColumn.Text == string.Empty)
							return;
						decimal.Parse(e.Column.DataColumn.Text);
					}
					catch
					{
						e.Cancel = true;
					}
					break;
				case PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD:
					try
					{
						if(e.Column.DataColumn.Text != string.Empty)
						{
							DateTime dtmDate = ((DateTime)cboRequiredDate.Value).Date;
							if(dtmDate < ((DateTime)cboOrderDate.Value).Date)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_REQUIREDATE_GREATER_THAN_TRANSDATE);
								e.Cancel = true;
							}
						}
					}
					catch{}
					break;
			}
				
		}
		private void KeepTheGridDesign()
		{
			//const string COL_ALIGN = "ALIGN";
			DataColumn dcolMyColumn ; 
			dtbGridDesign = new DataTable();

			DataColumn[] dcolKey = new DataColumn[ONE];

			dcolMyColumn = new DataColumn();
			dcolMyColumn.DataType = typeof(string);
			dcolMyColumn.ColumnName= GRID_COL_NAME;
			dtbGridDesign.Columns.Add(dcolMyColumn);
			dcolKey[0] = dcolMyColumn; //set this column as the primary key

			dcolMyColumn = new DataColumn();
			dcolMyColumn.DataType = typeof(string);
			dcolMyColumn.ColumnName= GRID_COL_CAPTION;
			dtbGridDesign.Columns.Add(dcolMyColumn);

			dcolMyColumn = new DataColumn();
			dcolMyColumn.DataType = typeof(string);
			dcolMyColumn.ColumnName= GRID_COL_WIDTH;
			dtbGridDesign.Columns.Add(dcolMyColumn);

			dcolMyColumn = new DataColumn();
			dcolMyColumn.DataType = typeof(bool);
			dcolMyColumn.ColumnName = GRID_COL_VISIBLE;
			dtbGridDesign.Columns.Add(dcolMyColumn);

			dtbGridDesign.PrimaryKey = dcolKey;

			DataRow drowNew;
			foreach (C1DisplayColumn dcolC1 in gridData.Splits[0].DisplayColumns)
			{
				if (dcolC1.DataColumn.DataField.Trim() != String.Empty)
				{
					drowNew = dtbGridDesign.NewRow();
					drowNew[GRID_COL_NAME] = dcolC1.DataColumn.DataField;
					drowNew[GRID_COL_CAPTION] = dcolC1.DataColumn.Caption;
					drowNew[GRID_COL_WIDTH] = dcolC1.Width;
					drowNew[GRID_COL_VISIBLE] = dcolC1.Visible;
					//drowNew[GRID_COL_VISIBLE] = gridData.Splits[0].DisplayColumns[i].HeadingStyle.HorizontalAlignment;
					
					dtbGridDesign.Rows.Add(drowNew);
				}
			}
		}
		#endregion other functions

		#region HACKED by Tuan TQ, 07 Nov, 2005-Change request
		/// <summary>
		/// Constructor for open existing PO
		/// </summary>
		/// <param name="pintPOMasterId"></param>
		/// <author> Tuan TQ, 07 Nov, 2005</author>
		public PurchaseOrder(int pintPOMasterId)
		{
			InitializeComponent();
			voPOMaster = new PO_PurchaseOrderMasterVO();
			voPOMaster.PurchaseOrderMasterID = pintPOMasterId;			
		}
		
		/// <summary>
		/// Load PO Information for viewing
		/// </summary>
		public void LoadPurchaseOrder4ViewOnly(int pintPOMasterID)
		{
			const string METHOD_NAME = THIS + ".LoadReadOnlyPurchaseOrder()";
			LoadMaster(pintPOMasterID);
				
			//Disble buttons
			btnAdd.Enabled = false;
			btnAdditionCharges.Enabled = false;
			btnAdvance.Enabled = false;
			btnBuyer.Enabled = false;
			btnCarrier.Enabled = false;
			btnContact.Enabled = false;
			btnCurrency.Enabled = false;
			btnDelete.Enabled = false;
			btnDeliverySchedule.Enabled = false;
			btnDeliverySchedule.Enabled = true;
			btnDeliveryTerms.Enabled = false;
			btnDiscTerms.Enabled = false;
			// fix bug 3621 from thuypt
			btnEdit.Enabled = true;
			btnHelp.Enabled = false;
			btnInvToLoc.Enabled = false;
			btnPause.Enabled = false;
			btnPayTerms.Enabled = false;
			btnPOType.Enabled = false;				
			btnSave.Enabled = false;
			btnShipToLoc.Enabled = false;
			btnVendor.Enabled = false;
			btnVendorLoc.Enabled = false;
			btnVendorName.Enabled = false;

			// 25-04-2006 dungla: fix bug 3817 for NgaHT, enable PO No
			btnOrderNo.Enabled = true;
			txtOrderNo.Enabled = true;
				
			cboCCN.Enabled = false;

			//Enable buttons
			btnClose.Enabled = true;
			btnPrint.Enabled = true;
		}


		#endregion Tuan TQ

		/// <summary>
		/// Constructor for convert CPO to new PO
		/// </summary>
		/// <param name="pdstCPODetail"></param>
		/// <param name="pobjCPO"></param>
		/// <author>Do Manh Tuan</author>
		public PurchaseOrder(DataSet pdstCPODetail, object pobjCPO)
		{
			InitializeComponent();
			mPOFormState = POFormState.ToNewCPO;	
		}
		
		public PurchaseOrder(DataView pdtwCPODetail, object pobjCPO, object pobjPOMaster, DataSet pdstDelivery)
		{
			InitializeComponent();
			mPOFormState = POFormState.ToExistCPO;
			if (pdtwCPODetail != null)
			{
				dtwCPOs = pdtwCPODetail;
				dstDelivery = pdstDelivery;
			}
			else
				dtwCPOs = null;
			voPOMaster = (PO_PurchaseOrderMasterVO) pobjPOMaster;
		}
		
		/// <summary>
		/// Form Load in case : CPO To Exist PO
		/// </summary>
		public void FormLoadForToExistPO()
		{
			this.Text = this.Text + ADDITION_CAPTION_FLD;
			LoadMaster(voPOMaster.PurchaseOrderMasterID);
			chkImportTax.Checked = false;
			chkVAT.Checked = false;
			chkSpecialTax.Checked = false;
			//fill to grid
			int intLine = 0, i =0;
			dstPODetail.EnforceConstraints = false;
			dstPODetail.Tables[0].Columns.Add(MTR_CPODS.SELECT_COLUMN, typeof(bool));
			dtwCPOs.Sort = ITM_ProductTable.PRODUCTID_FLD;
			while (i < dtwCPOs.Count)
			{
				DataRowView drowData = dtwCPOs[i];
				int j = GetIndexOfPOLine((int)dtwCPOs[i][ITM_ProductTable.PRODUCTID_FLD]);
				if (j > -1)
				{
					DataRow drowPODetail = dstPODetail.Tables[0].Rows[j];
					drowPODetail[MTR_CPODS.SELECT_COLUMN] = true;
					drowPODetail[ITM_ProductTable.PRODUCTID_FLD] = drowData[ITM_ProductTable.PRODUCTID_FLD];
					drowPODetail[ITM_ProductTable.BUYINGUMID_FLD] = drowData[ITM_ProductTable.STOCKUMID_FLD];
					drowPODetail[ITM_ProductTable.STOCKUMID_FLD] = drowData[ITM_ProductTable.STOCKUMID_FLD];
					drowPODetail[ITM_ProductTable.CODE_FLD] = drowData[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD];
					drowPODetail[ITM_ProductTable.DESCRIPTION_FLD] = drowData[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD];
					drowPODetail[ITM_ProductTable.REVISION_FLD] = drowData[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD];
					drowPODetail[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drowData[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = drowData[ITM_ProductTable.LISTPRICE_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.VAT_FLD] = drowData[ITM_ProductTable.VAT_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD] = drowData[ITM_ProductTable.IMPORTTAX_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD] = drowData[ITM_ProductTable.SPECIALTAX_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD] = DBNull.Value;
					DataRowView[] drvSameItems = dtwCPOs.FindRows(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString());
					foreach (DataRowView drvNewItem in drvSameItems)
					{
						if (drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString() == string.Empty)
						{
							drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = drvNewItem[MTR_CPOTable.QUANTITY_FLD];
						}
						else
						{
							drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = (decimal)drvNewItem[MTR_CPOTable.QUANTITY_FLD] + (decimal) drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD];
						}
					}
					i = i + drvSameItems.Length;
				}
				else
				{
					DataRow drowPODetail = dstPODetail.Tables[0].NewRow();
					drowPODetail[MTR_CPODS.SELECT_COLUMN] = true;
					drowPODetail[PRO_WorkOrderDetailTable.LINE_FLD] = int.Parse(dstPODetail.Tables[0].Rows[dstPODetail.Tables[0].Rows.Count - 1 + intLine][PRO_WorkOrderDetailTable.LINE_FLD].ToString()) + 1;
					drowPODetail[ITM_ProductTable.PRODUCTID_FLD] = drowData[ITM_ProductTable.PRODUCTID_FLD];
					drowPODetail[ITM_ProductTable.BUYINGUMID_FLD] = drowData[ITM_ProductTable.STOCKUMID_FLD];
					drowPODetail[ITM_ProductTable.STOCKUMID_FLD] = drowData[ITM_ProductTable.STOCKUMID_FLD];
					drowPODetail[ITM_ProductTable.CODE_FLD] = drowData[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD];
					drowPODetail[ITM_ProductTable.DESCRIPTION_FLD] = drowData[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD];
					drowPODetail[ITM_ProductTable.REVISION_FLD] = drowData[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD];
					drowPODetail[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = drowData[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = drowData[ITM_ProductTable.LISTPRICE_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.VAT_FLD] = drowData[ITM_ProductTable.VAT_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD] = drowData[ITM_ProductTable.IMPORTTAX_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD] = drowData[ITM_ProductTable.SPECIALTAX_FLD];
					DataRowView[] drvSameItems = dtwCPOs.FindRows(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString());
					foreach (DataRowView drvNewItem in drvSameItems)
					{
						if (drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString() == string.Empty)
						{
							drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = drvNewItem[MTR_CPOTable.QUANTITY_FLD];
						}
						else
						{
							drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = (decimal)drvNewItem[MTR_CPOTable.QUANTITY_FLD] + (decimal) drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD];
						}
					}
					dstPODetail.Tables[0].Rows.Add(drowPODetail);
					i = i + drvSameItems.Length;
				}
			}
			btnEdit_Click(btnEdit, new EventArgs());
			mPOFormState = POFormState.ToExistCPO;
			ReCalculate();
		}

		private int GetIndexOfPOLine(int pintProductID)
		{
			int i =0;
			foreach (DataRow drowData in dstPODetail.Tables[0].Rows)
			{
				if ( (int)drowData[ITM_ProductTable.PRODUCTID_FLD] == pintProductID)
					break;
				i += 1;
			}
			if (i == dstPODetail.Tables[0].Rows.Count) i = -1;
			return i;
		}

		
		/// <summary>
		/// Make btnPrintConfiguration always enable/disable like the btnPrint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_EnabledChanged(object sender, EventArgs e)
		{
			btnPrintConfiguration.Enabled = ((Control)sender).Enabled;
		}

		
		#region PART ORDER SHEET REPORT ThachNN
				
		#region PART ORDER SHEET REPORT variables			
		const string ISSUE_DATE_FORMAT = "dd-MMM-yyyy";
		/// Result Data Table Column name
		//const string VENDOR = "Vendor";
		const string PRODUCTID = "ProductID";
		const string PARTNO = "PartNo";
		const string PARTNAME = "PartName";		
		const string MONTH = "Month";		
		const string YEAR = "Year";
		
		const string SCHEDULE_DATE = "ScheduleDate";
		const string QUANTITY = "Quantity";
		const string ADJUSTMENT = "Adjustment";

		const string SUMROWNEXT_O = "SumRowNextO";
		const string SUMROWNEXTNEXT_O = "SumRowNextNextO";
		const string SUMROWNEXT_A = "SumRowNextA";
		const string SUMROWNEXTNEXT_A = "SumRowNextNextA";
						
		const string MONTHHEADING = "MonthHeading";
		const string NEXTMONTHHEADING = "NextMonthHeading";
		const string NEXTNEXTMONTHHEADING = "NextNextMonthHeading";
		
		const string REPORT_LAYOUT_FILE = "PartOrderSheetReport.xml";
		const string REPORT_NAME = "PartOrderSheetReport";

		const string REPORTFLD_TITLE			= "fldTitle";
		const string REPORTFLD_COMPANY			= "fldCompany";
		const string REPORTFLD_ADDRESS			= "fldAddress";
		const string REPORTFLD_TEL				= "fldTel";
		const string REPORTFLD_FAX				= "fldFax";

		const string REPORTFLD_ISSUE_DATE					= "fldIssueDate";
		const string REPORTFLD_ATTENTION				= "fldAttention";
		const string REPORTFLD_REVISION					= "fldRevision";	
		const string REPORTFLD_PONO = "fldPONo";

		const string COL_ORDER_PREFIX			= "D";
		const string COL_ADJUSTMENT_PREFIX		= "A";
	
		UtilsBO boUtil = new UtilsBO();
		private string strNewFile = string.Empty;

		#endregion

		/// <summary>
		/// thachnn: 17/11/2005
		/// Print the Part Order Sheet Report
		/// Using the "PartOrderSheetReport.xml" layout
		/// </summary>		
		private void ShowPartOrderSheetReport(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + "ShowPartOrderSheetReport()";			

			try
			{						
				Cursor = Cursors.WaitCursor;		

				#region PREPARE			

				string mstrReportDefFolder = Application.StartupPath + "\\" +Constants.REPORT_DEFINITION_STORE_LOCATION;
				DataTable dtbResult;
				DataTable dtbSummaryResult;				
				C1PrintPreviewDialogBO objBO = new C1PrintPreviewDialogBO();
                
				string strAttention = txtVendorName.Text;
				string strRevision = string.Empty;
				if (!txtRevision.ValueIsDbNull)
					strRevision = txtRevision.Value.ToString();
				
				int nCCNID;
				int nMonth = DateTime.MinValue.Month; //minimum month
				int nYear = DateTime.MinValue.Year; // Minimum year
				int nVendorID = int.MinValue;
				try
				{
					nVendorID = (int)txtVendor.Tag;
				}
				catch{}

				string strCCN = string.Empty;				
				string strPurchaseOrderMasterCode = string.Empty;
				string strVendor = string.Empty;
				
				/// contain array of string: 01 02 03 .. of day of month in the dtbResult, except the missing day			
				ArrayList arrDueDateHeading = new ArrayList();	

				/// Build to keep value of pair: 01 --> 01-Mon, ... depend on the real data of dtbResule
				NameValueCollection arrDayNumberMapToDayWithDayOfWeek = new NameValueCollection();

				/// Build to keep value of pair: 01 --> 01-Mon, ... NOT DEPEND on the real data
				NameValueCollection arrFullDayNumberMapToDayWithDayOfWeek = new NameValueCollection();
	

				// check report layout file is exist or not
				if (!File.Exists(mstrReportDefFolder + @"\" + REPORT_LAYOUT_FILE))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
					Cursor = Cursors.Default;
					return;
				}

				//return if no record was selected
				if(voPOMaster.PurchaseOrderMasterID <= 0)
				{
					return;
				}				
				string pstrPurchaseOrderMasterID = voPOMaster.PurchaseOrderMasterID.ToString();

				#endregion

				#region	GETTING THE PARAMETER
		
				#region Validate data
		
				// user must select CCN first
				if (cboCCN.SelectedValue == null || cboCCN.SelectedValue == DBNull.Value)
				{
					string[] arrParams = {lblCCN.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error,arrParams);
					cboCCN.Focus();
					Cursor = Cursors.Default;
					return;
				}				
				// Check if user does not select PO Number
				if(txtOrderNo.Text.Trim() == string.Empty)
				{
					string[] arrParams = {lblOrderNo.Text};
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error,arrParams);
					txtOrderNo.Focus();
					Cursor = Cursors.Default;
					return;
				}				

				#endregion
			
				nCCNID = int.Parse(cboCCN.SelectedValue.ToString());
				// strCCN = boUtil.GetCCNCodeFromID(nCCNID);
				strPurchaseOrderMasterCode = txtOrderNo.Text.Trim();
				
				DateTime dtmOrderMonth = DateTime.MinValue;		
				///	Order Date will getting like the grid of PO_Delivery Schedule Form
				///	Build the data table like PO_Delivery Schedule Form. getting the first record, schedule date
				try
				{
					PODeliveryScheduleBO objPODeliveryScheduleBO = new PODeliveryScheduleBO();
					int nCurrentPurchaseOrderDetail = int.Parse(gridData[gridData.Row,PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
					DataSet dstTempDeliverySchedule = objPODeliveryScheduleBO.GetDeliverySchedule(nCurrentPurchaseOrderDetail);
					if(dstTempDeliverySchedule != null)
					{
						if(dstTempDeliverySchedule.Tables.Count > 0)
						{
							if(dstTempDeliverySchedule.Tables[0].Rows.Count > 0)
							{
								try
								{
									dtmOrderMonth = (DateTime)dstTempDeliverySchedule.Tables[0].Rows[0][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD];									
								}
								catch{}
							}
						}
					}
				}
				catch
				{
					///if can't get dtmOrderMonth, may be there is error in the other module
					///we still generate the report with month= 1 and year = 1900, it will be a empty report
				}
				nYear = dtmOrderMonth.Year;
				nMonth = dtmOrderMonth.Month;				

				// if input null, then we send the int.MinValue to the BO function
				// Not mandatory id field will have int.MinValue if it is not selected
				try
				{
					nVendorID = (int)(txtVendor.Tag);
					strVendor = objBO.GetVendorCodeFromID(nVendorID) + "-" + objBO.GetVendorNameFromID(nVendorID);
				}
				catch
				{
					strVendor = string.Empty;
				}
						
				#endregion
			
				#region BUILDING THE TABLE (getting from database by BO)
				DataSet dstResult = objBO.GetPartOrderSheetReportData(nCCNID,   nVendorID,  pstrPurchaseOrderMasterID);
				dtbResult = dstResult.Tables[0];
				dtbSummaryResult = dstResult.Tables[1];
				#endregion

				#region TRANSFORM ORIGINAL TABLE FOR REPORT
				

				#region BUILD THE FULL DayWithDayOfWeek Pair	// full from 1 to 31
				DateTime dtmTemp = new DateTime(nYear,nMonth,1);
				for(int i = 0 ; i <31 ; i++)
				{
					DateTime dtm = dtmTemp.AddDays(i);
					arrFullDayNumberMapToDayWithDayOfWeek.Add(dtm.Day.ToString("00"), dtm.Day.ToString("00")+"-"+dtm.DayOfWeek.ToString().Substring(0,3)  );
				}

				#endregion
	
				#region GETTING THE DATE HEADING
				ArrayList arrDueDate = GetColumnValuesFromTable(dtbResult,SCHEDULE_DATE);			

				foreach(DateTime dtm in arrDueDate)
				{
					string strColumnName = COL_ORDER_PREFIX + dtm.Day.ToString("00");
					string strColumnNameA = COL_ADJUSTMENT_PREFIX + dtm.Day.ToString("00");
					arrDayNumberMapToDayWithDayOfWeek.Add(dtm.Day.ToString("00"), dtm.Day.ToString("00")+"-"+dtm.DayOfWeek.ToString().Substring(0,3)  );
					arrDueDateHeading.Add(strColumnName);
					arrDueDateHeading.Add(strColumnNameA);
				}		

				#endregion		

				ArrayList arrItems = GetProductID_Month_YearGROUPFromTable(dtbResult,PRODUCTID,MONTH,YEAR);
			
				DataTable dtbTransform = BuildPartOrderSheetTable(arrDueDateHeading);
				foreach(string strItem in arrItems)
				{
					// Create DUMMYROW FIRST
					DataRow dtrNew = dtbTransform.NewRow();

					double dblSumRowNext = 0;
					double dblSumRowNextNext = 0;

					double dblSumRowNextA = 0;
					double dblSumRowNextNextA = 0;				
										
					DateTime dtmTemp1 = new DateTime( int.Parse(strItem.Split('#')[2]) , int.Parse(strItem.Split('#')[1])  , 1) ;

					string strFilter =  
							string.Format("[{0}]='{1}' AND [{2}]='{3}' AND [{4}]='{5}'",
							PRODUCTID,
							strItem.Split('#')[0],
							MONTH,
							dtmTemp1.Month,
							YEAR,
							dtmTemp1.Year
							);
					
					string strSort = string.Empty; // string.Format("[{0}] ASC", PARTNO);

					/// Getting next total for the next, and next next month of order quantity
					/// Getting next total for the next, and next next month of adjustment
					string strFilterNext =  
					string.Format("[{0}]='{1}' AND [{2}]='{3}' AND [{4}]='{5}'",
						PRODUCTID,
						strItem.Split('#')[0],
						MONTH,
						dtmTemp1.AddMonths(1).Month,
						YEAR,
						dtmTemp1.AddMonths(1).Year
						);
					DataRow[] dtrowsNext = dtbSummaryResult.Select(strFilterNext);
					foreach(DataRow dtr in dtrowsNext)
					{						
						dblSumRowNext += ReportBuilder.ToDouble(dtr[QUANTITY]);						
						dblSumRowNextA += ReportBuilder.ToDouble(dtr[ADJUSTMENT]);						
					}
					
					string strFilterNextNext =  
						string.Format("[{0}]='{1}' AND [{2}]='{3}' AND [{4}]='{5}'",
						PRODUCTID,
						strItem.Split('#')[0],
						MONTH,
						dtmTemp1.AddMonths(2).Month,
						YEAR,
						dtmTemp1.AddMonths(2).Year
						);
					DataRow[] dtrowsNextNext = dtbSummaryResult.Select(strFilterNextNext);
					foreach(DataRow dtr in dtrowsNextNext)
					{						
						dblSumRowNextNext += ReportBuilder.ToDouble(dtr[QUANTITY]);
						dblSumRowNextNextA += ReportBuilder.ToDouble(dtr[ADJUSTMENT]);
					}
					

					DataRow[] dtrows = dtbResult.Select(strFilter,strSort);	// select productid in a specific month, year
					foreach(DataRow dtr in dtrows)
					{
						/// fill data to the dummy row
						/// these column is persistance, we always set to the first rows						
						dtrNew[PRODUCTID] = dtrows[0][PRODUCTID];
						dtrNew[PARTNO] = dtrows[0][PARTNO];
						dtrNew[PARTNAME] = dtrows[0][PARTNAME];						

						/// fill the Quantity of the day to the cell (indicate by column Dxx in this dummy rows)
						string strDateColumnToFill = COL_ORDER_PREFIX + ((DateTime)dtr[SCHEDULE_DATE]).Day.ToString("00");
						dtrNew[strDateColumnToFill] = dtr[QUANTITY];

						/// fill the Quantity of the day to the cell (indicate by column Dxx in this dummy rows)
						strDateColumnToFill = COL_ADJUSTMENT_PREFIX + ((DateTime)dtr[SCHEDULE_DATE]).Day.ToString("00");
						dtrNew[strDateColumnToFill] = dtr[ADJUSTMENT];
						
						/// fill the SumRow of next month and next next month to this dummy rows
						/// we calculated these values in report layout script
						
						/// Order Delivery quantity
						dtrNew[SUMROWNEXT_O] = dblSumRowNext;
						dtrNew[SUMROWNEXTNEXT_O] = dblSumRowNextNext;

						/// Adjustment Quantity
						dtrNew[SUMROWNEXT_A] = dblSumRowNextA;
						dtrNew[SUMROWNEXTNEXT_A] = dblSumRowNextNextA;

						DateTime dtmForHeading = new DateTime((int)dtr[YEAR], (int)dtr[MONTH], 1) ;
						dtrNew[MONTHHEADING] = (dtmForHeading).ToString("MMM-yyyy").ToUpper();
						dtrNew[NEXTMONTHHEADING] = (dtmForHeading.AddMonths(1)).ToString("MMM-yyyy").ToUpper();
						dtrNew[NEXTNEXTMONTHHEADING] = (dtmForHeading.AddMonths(2)).ToString("MMM-yyyy").ToUpper();						
					}
				
					// add to the transform data table
					dtbTransform.Rows.Add(dtrNew);				
				}	    
			
				#endregion

				#region RENDER REPORT
				
				ReportBuilder objRB;	
				objRB = new ReportBuilder();				
				try
				{
					objRB.ReportName = REPORT_NAME;
					//					string strSort = string.Format("[{0}] ASC, [{1}] ASC, [{2}] ASC,[{3}] ASC,[{4}] ASC,[{5}] ASC,[{6}] ASC "   ,     VENDOR,CATEGORY,PARTNO,PARTNAME,MODEL,UM,QUANTITYSET );
					//					DataTable dtbToDisplayInReport = new DataTable();
					//					foreach(DataRow dtr in dtbTransform.Select(string.Empty,strSort))
					//					{
					//						dtbToDisplayInReport.ImportRow(dtr);
					//					}
					//					objRB.SourceDataTable = dtbToDisplayInReport;
					objRB.SourceDataTable = dtbTransform;
				}
				catch// (Exception ex)
				{
					/// we can't preview while we don't have any data
					return;
				}

				#region INIT REPORT BUIDER OBJECT
				try
				{
					objRB.ReportDefinitionFolder = mstrReportDefFolder;
					objRB.ReportLayoutFile = REPORT_LAYOUT_FILE;					
					if(objRB.AnalyseLayoutFile() == false)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
						return;
					}
					//objRB.UseLayoutFile = objRB.AnalyseLayoutFile();	// use layout file if any , auto drawing if not found layout file
					objRB.UseLayoutFile = true;	// always use layout file
				}
				catch
				{
					objRB.UseLayoutFile = false;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND,MessageBoxIcon.Error);
				}
				#endregion				

				objRB.MakeDataTableForRender();
				//grid.DataSource = objRB.RenderDataTable;

			
				// and show it in preview dialog				
				C1PrintPreviewDialog	printPreview = new C1PrintPreviewDialog();
				
						

				objRB.ReportViewer = printPreview.ReportViewer;				
				objRB.RenderReport();			

						

				#region MODIFY THE REPORT LAYOUT
				
				objRB.DrawPredefinedField(REPORTFLD_TITLE, lblReportTitle.Text.Trim());
				objRB.DrawPredefinedField(REPORTFLD_ATTENTION, strAttention.ToUpper());								

//				if(dtmOrderMonth.Equals(DateTime.MinValue) == false)	// we process here when we have valid dtmOrderMonth in the start point of this function
//				{
//					objRB.DrawPredefinedField(REPORTFLD_ORDER_MONTH, dtmOrderMonth.ToString(NEXTMONTH_DATE_FORMAT).ToUpper());
//					objRB.DrawPredefinedField(REPORTFLD_SUMROW_NEXT,		dtmOrderMonth.AddMonths(1).ToString(NEXTMONTH_DATE_FORMAT));
//					objRB.DrawPredefinedField(REPORTFLD_SUMROW_NEXTNEXT,	dtmOrderMonth.AddMonths(2).ToString(NEXTMONTH_DATE_FORMAT));			
//				}

				
				//get this date time from the Form (TransDate on form)
				DateTime dtmTransDate = (DateTime)cboOrderDate.Value;					
				objRB.DrawPredefinedField(REPORTFLD_ISSUE_DATE, dtmTransDate.ToString(ISSUE_DATE_FORMAT));
				objRB.DrawPredefinedField(REPORTFLD_REVISION, strRevision);			
				objRB.DrawPredefinedField(REPORTFLD_PONO, strPurchaseOrderMasterCode);


				#region COMPANY INFO // header information get from system params
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_COMPANY,SystemProperty.SytemParams.Get(SystemParam.COMPANY_NAME));
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_ADDRESS,SystemProperty.SytemParams.Get(SystemParam.ADDRESS));					
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_TEL,SystemProperty.SytemParams.Get(SystemParam.TEL));					
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_FAX,SystemProperty.SytemParams.Get(SystemParam.FAX));					
				}
				catch{}
				#endregion

				#region PUSH PARAMETER VALUE
				//There is no parameter for this report				
				#endregion		
			
				#region RENAME THE COLUMN HEADING TEXT
				ArrayList arrColumnHeadings = new ArrayList();				
				for(int i = 0; i <= 31; i++) /// clear the heading text
				{
					objRB.DrawPredefinedField(COL_ORDER_PREFIX+i.ToString("00")+"Lbl","");
				}

				for(int i = 0; i <= 31; i++)
				{
					/// Paint the EMPTY Colummn to
					try
					{
						if(arrDueDateHeading.Contains(COL_ORDER_PREFIX+i.ToString("00"))   )
						{
							string strHeading = arrDayNumberMapToDayWithDayOfWeek[i.ToString("00")].Substring(0,6);
							objRB.DrawPredefinedField(COL_ORDER_PREFIX+i.ToString("00")+"Lbl",strHeading);
						}
						else
						{
							string strHeading = arrFullDayNumberMapToDayWithDayOfWeek[i.ToString("00")].Substring(0,6);
							objRB.DrawPredefinedField(COL_ORDER_PREFIX+i.ToString("00")+"Lbl",strHeading);
							/// HACKED: Thachnn: from now we don't paint the empty Column to WhiteSmoke.
							/// objRB.Report.Fields[COL_ORDER_PREFIX+i.ToString("00")+"Lbl"].BackColor = Color.WhiteSmoke;
						}
					}
					catch	// draw continue, don't care about error value in the parrValuesToFill
					{
						//break;
					}

					/// Paint the WEEKEND Colummn to Red on Yealow
					try
					{
						if(objRB.Report.Fields[COL_ORDER_PREFIX+i.ToString("00")+"Lbl"] != null)
						{
							/// this variable contain sat, sun, mon tue, ...
							string strDateName = objRB.GetFieldByName(COL_ORDER_PREFIX+i.ToString("00")+"Lbl").Text.Substring(3,3);
							//if(strDateName == "Sat" || strDateName == "Sun")
							if(strDateName == "Sun")
							{
								objRB.Report.Fields[COL_ORDER_PREFIX+i.ToString("00")+"Lbl"].BackColor = Color.Yellow;
								objRB.Report.Fields[COL_ORDER_PREFIX+i.ToString("00")+"Lbl"].ForeColor = Color.Red;								
							}
						}
					}
					catch	// draw continue, don't care about error value in the parrValuesToFill
					{
						//break;
					}

					/// REMOVE THE DATE STRING (Mon, Tue, ..) as Mr.CuongNT's request, hic hic hic. I add, then I have to remove
					/// Rename it to 1 2 3 4 5 6
					try
					{
						objRB.Report.Fields[COL_ORDER_PREFIX+i.ToString("00")+"Lbl"].Text = i.ToString();
						objRB.Report.Fields[COL_ORDER_PREFIX+i.ToString("00")+"Lbl"].Font.Bold = false;
					}
					catch{}
				}		

				#endregion
			
				#endregion	
					
				objRB.RefreshReport();
				
				/// force the copies number
                printPreview.FormTitle = lblReportTitle.Text + " "  + nMonth.ToString("00") + "-"+ nYear.ToString("0000") ;	
				printPreview.Show();
				#endregion

			}
			catch(Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
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
		
		/// <summary>
		/// Thachnn : 15/Oct/2005
		/// Browse the DataTable, get all value of column with provided named.
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>
		/// <param name="pstrColumnName">COlumn Name in pdtb DataTable to collect values from</param>
		/// <returns>ArrayList of object, collect from pdtb's column named pstrColumnName. Empty ArrayList if error or not found any row in pdtb.</returns>
		private static ArrayList GetColumnValuesFromTable(DataTable pdtb, string pstrColumnName)
		{
			ArrayList arrRet = new ArrayList();
			try
			{
				foreach (DataRow drow in pdtb.Rows)
				{
					object objGet = drow[pstrColumnName];
					if( !arrRet.Contains(objGet)  )
					{
						arrRet.Add(objGet);
					}
				}
			}
			catch
			{
				arrRet.Clear();
			}
			return arrRet;
		}


		/// <summary>
		/// /// thachnn: 14/Nov/2005
		/// Build the crosstab table to render on report
		/// contain D01 ---> D31 , A01 ---> A31, and NextMonth SumRow O A, NextNextMonth SumRow O A
		/// see Schedule of local parts in month UseCase under PCS/06-Project Design/DesignReport folder.
		/// </summary>
		/// <remarks>		
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildPartOrderSheetTable(ArrayList parrScheduleDateHeading)
		{
			const string strPartOrderSheetTableName = "PartOrderSheetTable";
			try
			{
				//Create table
				DataTable dtbRet = new DataTable(strPartOrderSheetTableName);
				
				//Add columns				
				dtbRet.Columns.Add(PRODUCTID, typeof(Int32));
				dtbRet.Columns.Add(PARTNO, typeof(String));
				dtbRet.Columns.Add(PARTNAME, typeof(String));				

				dtbRet.Columns.Add(SUMROWNEXT_O, typeof(Double));
				dtbRet.Columns.Add(SUMROWNEXTNEXT_O, typeof(Double));
				dtbRet.Columns.Add(SUMROWNEXT_A, typeof(Double));
				dtbRet.Columns.Add(SUMROWNEXTNEXT_A, typeof(Double));

				dtbRet.Columns.Add(MONTHHEADING, typeof(String));
				dtbRet.Columns.Add(NEXTMONTHHEADING, typeof(String));
				dtbRet.Columns.Add(NEXTNEXTMONTHHEADING, typeof(String));

				foreach(string strColumnName in parrScheduleDateHeading)
				{					
					try
					{
						dtbRet.Columns.Add(strColumnName,typeof(Double));
					}
					catch{}
				}
				// FILL the null column, if not exist null column (not existed date.) report will gen ###,#0 to the cell	
				for(int i = 1; i <=31; i++)												  
				{
					if(parrScheduleDateHeading.Contains(COL_ORDER_PREFIX + i.ToString("00")) == false )
					{		
						try
						{
							dtbRet.Columns.Add(COL_ORDER_PREFIX + i.ToString("00"),typeof(String));							
						}
						catch{}						
					}
					if(parrScheduleDateHeading.Contains(COL_ADJUSTMENT_PREFIX + i.ToString("00")) == false )
					{		
						try
						{
							dtbRet.Columns.Add(COL_ADJUSTMENT_PREFIX + i.ToString("00"),typeof(String));							
						}
						catch{}						
					}
				}
				//				dtbRet.Columns.Add("Boolean", typeof(System.Boolean));
				//				dtbRet.Columns.Add("Int32", typeof(System.Int32));											
				//				dtbRet.Columns.Add("String", typeof(System.String));
				//				dtbRet.Columns.Add("Double", typeof(System.Double));
				//				dtbRet.Columns.Add("DateTime", typeof(System.DateTime));		
				
				return dtbRet;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		
		/// <summary>
		/// Thachnn : 08/Nov/2005
		/// Browse the DataTable, get all value of PartNo column, insert into ArraysList as PartNoValue
		/// Because Item differ from each other by partNo
		/// So this group will be unique between Items
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>			
		/// <param name="pstrCategoryColName"></param>
		/// <param name="pstrPartNoColName"></param>
		/// <returns>ArrayList of object, collect PartNoValue pairs from pdtb. Empty ArrayList if error or not found any row in pdtb.</returns>
		private static ArrayList GetProductID_Month_YearGROUPFromTable(DataTable pdtb, string pstrProductID, string pstrMonth, string pstrYear)
		{
			ArrayList arrRet = new ArrayList();
			try
			{	
				foreach (DataRow drow in pdtb.Rows)
				{
					object objProductIDGet = drow[pstrProductID];
					object objMonthGet = drow[pstrMonth];
					object objYearGet = drow[pstrYear];
					
					string str = objProductIDGet.ToString().Trim() +"#"+  objMonthGet.ToString().Trim() + "#" + objYearGet.ToString().Trim();
					//string str = objPartNoGet.ToString();
					if( !arrRet.Contains(str)  )
					{
						arrRet.Add(str);
					}
				}
			}
			catch
			{
				arrRet.Clear();
			}
			return arrRet;
		}

	
		#endregion
		
		#region PART ORDER SHEET Multi Vendor REPORT ThachNN

		/// <summary>
		/// thachnn: 10/01/2006
		/// Show the Part Order Sheet Multi Vendor Report
		/// Using the "PartOrderSheetMultiVendorReport.xml" layout
		/// </summary>		
		private void ShowPartOrderSheetMultiVendorReport(object sender, EventArgs e)
		{
			(new PartOrderSheetMultiVendorReport()).Show();
		}

		#endregion	PART ORDER SHEET REPORT ThachNN

		#region PO Summary Report: TuanTQ

		/// <summary>
		/// Build and show Delivery To Next Slip Report
		/// </summary>
		/// <Author> Tuan TQ, 01 Dec, 2005</Author>
		private void ShowPOSummaryReport(object sender, EventArgs e)
		{	
			const string METHOD_NAME = THIS + ".ShowPOSummaryReport()";
			try
			{				
				const string APPLICATION_PATH  = @"PCSMain\bin\Debug";

				const string REPORTFLD_TITLE = "fldTitle";

				const string REPORTFLD_COMPANY = "fldCompany";
				const string REPORTFLD_TEL = "fldCompanyTel";
				const string REPORTFLD_FAX = "fldCompanyFax";
				const string REPORTFLD_ADDRESS = "fldCompanyAddress";

				const string PO_SUMMARY_REPORT_LAYOUT = "POSummaryReport.xml";
				
				//return if no record was selected
				if(voPOMaster.PurchaseOrderMasterID <= 0)
				{
					return;
				}				
				
				this.Cursor = Cursors.WaitCursor;

				C1PrintPreviewDialog   printPreview = new C1PrintPreviewDialog();
				C1PrintPreviewDialogBO boDataReport = new C1PrintPreviewDialogBO();
			
				DataTable dtbResult;
				dtbResult = boDataReport.GetPOSummaryData(voPOMaster.PurchaseOrderMasterID, voPOMaster.OrderDate);

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
			
				reportBuilder.ReportLayoutFile = PO_SUMMARY_REPORT_LAYOUT;

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
				reportBuilder.DrawPredefinedField(REPORTFLD_COMPANY, SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
				reportBuilder.DrawPredefinedField(REPORTFLD_TEL, SystemProperty.SytemParams.Get(SystemParam.TEL));
				reportBuilder.DrawPredefinedField(REPORTFLD_FAX, SystemProperty.SytemParams.Get(SystemParam.FAX));
				reportBuilder.DrawPredefinedField(REPORTFLD_ADDRESS, SystemProperty.SytemParams.Get(SystemParam.ADDRESS));
			
				reportBuilder.RefreshReport();
			
				//Print report
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(REPORTFLD_TITLE).Text;
				}catch{}
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
		
		#endregion
		
		#region DELIVERY SLIP REPORT - DungLA	

		/// <summary>
		/// Print current Purchase Order
		/// Delegate: this event handler is reponsible to execute the MenuItem Report
		/// Open the Delivery Slip Report Parameter form of Mr.DungLA
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>DungLA</author>
		private void ShowDeliverySlipReport(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPrint_Click()";
			// store original report definition
			//string strOriginalDefinition = rptPOSheet.ReportDefinition;
			try
			{
				// open PO delivery slip form
				if (voPOMaster != null && voPOMaster.PurchaseOrderMasterID > 0)
				{
					PODeliverySlip frmPODeliverySlip = new PODeliverySlip(voPOMaster);
					frmPODeliverySlip.Show();
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
			finally
			{
				this.Cursor = Cursors.Default;
				// after render, restore original report format from definition
				//rptPOSheet.ReportDefinition = strOriginalDefinition;
			}
		}


		#endregion

		#region Firm Order REPORT ThachNN: 13/06/2006

		/// <summary>
		/// thachnn: 06/13/2006
		/// Show the PO Firm Order Report
		/// Using the "POFirmOrder.xml" layout
		/// ReportID = ""
		/// </summary>		
		private void ShowPOFirmOrderReport(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ShowPOFirmOrderReport()";
			const string FIRM_ORDER_REPORT_ID = "20060611164200077";
			try
			{				
				C1PrintPreviewDialogBO boDataReport = new C1PrintPreviewDialogBO();

				ViewReportBO boViewReportBO = new ViewReportBO();
				sys_ReportVO voReportVO = (sys_ReportVO)boViewReportBO.GetReportByReportID(FIRM_ORDER_REPORT_ID);

				if(voReportVO == null)
				{
					return;
				}				

				ViewReport frmViewReport = new ViewReport();
				frmViewReport.VoReport = voReportVO;
				frmViewReport.ViewMode = ViewReportMode.Normal;
				frmViewReport.Show();
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
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		#endregion Firm Order REPORT ThachNN 13/06/2006

		/// <summary>
		/// FillPONumber
		/// </summary>
		/// <author>Trada</author>
		/// <date>Thursday, June 8 2006</date>
		private void FillPONumber(string pstrPrefix)
		{
			string strFormat = "-yy-mm-dd-##";
			if (txtMakerCode.Text != string.Empty)
			{
				string strPONumber = boUtil.GetNoByMask(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, pstrPrefix, strFormat);
				//strPONumber = txtMakerCode.Text + "-" + strPONumber;
				txtOrderNo.Text = strPONumber;
			}
		}
		/// <summary>
		/// btnMakerCode_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, June 8 2006</date>
		private void btnMakerCode_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMakerCode_Click()";
			try 
			{
				htbCriteria = new Hashtable();
				htbCriteria.Add(VENDOR, (int)PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtMakerCode.Text.Trim(), htbCriteria, true);
				if(drwResult != null)
				{
					if (txtMakerCode.Tag != null && int.Parse(txtMakerCode.Tag.ToString()) != int.Parse(drwResult[MST_PartyTable.PARTYID_FLD].ToString()))
					{
						txtMakerLoc.Text = string.Empty;
						txtMakerLoc.Tag = null;
					}
					txtMakerCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtMakerCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtMakerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					//Fill PO number
					FillPONumber(txtMakerCode.Text);
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
		/// <summary>
		/// txtMakerCode_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, June 8 2006</date>
		private void txtMakerCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMakerCode_Validating()";
			try 
			{
				if (txtMakerCode.Text == string.Empty)
				{
					txtMakerCode.Tag = null;
					txtMakerName.Text = string.Empty;
					txtMakerLoc.Text = string.Empty;
					txtMakerLoc.Tag = null;
					return;
                }
				if (!txtMakerCode.Modified) return;
				htbCriteria = new Hashtable();
				htbCriteria.Add(VENDOR, (int)PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtMakerCode.Text.Trim(), htbCriteria, false);
				if(drwResult != null)
				{
					if (txtMakerCode.Tag != null && int.Parse(txtMakerCode.Tag.ToString()) != int.Parse(drwResult[MST_PartyTable.PARTYID_FLD].ToString()))
					{
						txtMakerLoc.Text = string.Empty;
						txtMakerLoc.Tag = null;
					}
					txtMakerCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtMakerCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtMakerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					//Fill PO number
					FillPONumber(txtMakerCode.Text);
				}
				else 
					e.Cancel = true;
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
		/// <summary>
		/// txtMakerCode_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, June 8 2006</date>
		private void txtMakerCode_KeyDown(object sender, KeyEventArgs e)
		{	
			if (e.KeyCode == Keys.F4)
			{
				btnMakerCode_Click(null, null);
			}
		}
		/// <summary>
		/// btnMakerLoc_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, June 8 2006</date>
		private void btnMakerLoc_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMakerLoc_Click()";
			try
			{
				htbCriteria = new Hashtable();
				if(txtMakerCode.Text != string.Empty)
				{
					if(txtMakerCode.Tag != null)
					{
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtMakerCode.Tag.ToString());
					}
				}
				else
				{
					string[] strParam = new string[2];
					strParam[0] = lblMakerCode.Text;
					strParam[1] = lblMakerLoc.Text;
					// You have to select Maker before select Maker Location, please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtMakerCode.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtMakerLoc.Text.Trim(), htbCriteria, true);
				if(drwResult != null)
				{
					txtMakerLoc.Tag = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtMakerLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
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
		/// txtMakerLoc_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, June 8 2006</date>
		private void txtMakerLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMakerLoc_Validating()";
			try
			{
				if (txtMakerLoc.Text == string.Empty)
				{
					txtMakerLoc.Tag = null;
					return;
				}
				if (!txtMakerLoc.Modified) return;
				htbCriteria = new Hashtable();
				if(txtMakerCode.Text != string.Empty)
				{
					if(txtMakerCode.Tag != null)
					{
						htbCriteria.Add(MST_PartyContactTable.PARTYID_FLD, txtMakerCode.Tag.ToString());
					}
				}
				else
				{
					string[] strParam = new string[2];
					strParam[0] = lblMakerCode.Text;
					strParam[1] = lblMakerLoc.Text;
					// You have to select Maker before select Maker Location, please
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					e.Cancel = true;
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, txtMakerLoc.Text.Trim(), htbCriteria, false);
				if(drwResult != null)
				{
					txtMakerLoc.Tag = drwResult[MST_PartyLocationTable.PARTYLOCATIONID_FLD];
					txtMakerLoc.Text = drwResult[MST_PartyLocationTable.CODE_FLD].ToString();
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
		/// txtMakerLoc_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, June 8 2006</date>
		private void txtMakerLoc_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				btnMakerLoc_Click(null, null);
			}
		}

		private void btnPricingType_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPause_Click()";
			try
			{
				drwResult = FormControlComponents.OpenSearchForm(enm_PricingTypeTable.TABLE_NAME, enm_PricingTypeTable.DESCRIPTION_FLD, txtPricingType.Text.Trim(), null, true);
				if(drwResult != null)
				{
					txtPricingType.Tag = drwResult[enm_PricingTypeTable.PRICINGTYPEID_FLD];
					txtPricingType.Text = drwResult[enm_PricingTypeTable.DESCRIPTION_FLD].ToString();
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

		private void txtPricingType_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4)
				if(btnPricingType.Enabled)
					btnPricingType_Click(sender,e);
		}

		private void txtPricingType_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPricingType_Validating()";
			try 
			{
				//OnLeaveControl(sender,e);
				TextBox txtText = (TextBox)sender;
				if(!txtText.Modified)return;
				if(txtText.Text.Trim() == string.Empty)			
				{
					txtText.Tag = null;
					return;
				}

				drwResult = FormControlComponents.OpenSearchForm(enm_PricingTypeTable.TABLE_NAME, enm_PricingTypeTable.DESCRIPTION_FLD, txtPricingType.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtPricingType.Tag = drwResult[enm_PricingTypeTable.PRICINGTYPEID_FLD];
					txtPricingType.Text = drwResult[enm_PricingTypeTable.DESCRIPTION_FLD].ToString();
				}
				else 
					e.Cancel = true;
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

		
		#region Import New functions

		const int INT_MINIMUM_ROW = 4;
		const int INT_MINIMUM_COL = 8;
		const int INT_TITLE_ROW = 0;
		const int INT_HEADER_ROW = 1;
		const int INT_BEGIN_DATA_ROW = 3;
		const int INT_TITLE_COL = 0;
		const int INT_TIME_COL = 5;
		const int INT_BEGIN_DATA_COL = 6;
		const string TEMP_QTY_COL_NAME = "TempQty";
		const string STR_IMPORT_TASK = "Import";
		const int INT_PARTS_NO_COL = 2;

		private void btnImport_Click(object sender, EventArgs e)
		{
			Point ptShow = btnImport.Location;
			ptShow.X = btnImport.Right;
			ptShow.Y = btnImport.Bottom;
			ctxmnuImport.Show(this,ptShow);
		}

		private void mnuImportNew_Click(object sender, EventArgs e)
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
				if(CheckMandatory(cboOrderDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					cboOrderDate.Focus();
					return;
				}

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
				if(CheckMandatory(txtVendor))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtVendor.Focus();
					return;
				}
				if(IsNotExistCustomer())
				{
					// This customer does not exist in database
					PCSMessageBox.Show(ErrorCode.MESSAGE_CUSTOMER_DOES_NOT_EXIST,MessageBoxIcon.Exclamation);
					btnVendor.Focus();
					return;
				}
				if(CheckMandatory(txtVendorLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtVendorLoc.Focus();
					return;
				}
				if(CheckMandatory(txtShipToLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtShipToLoc.Focus();
					return;
				}
				if(CheckMandatory(txtInvToLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtInvToLoc.Focus();
					return;
				}
				if(CheckMandatory(txtPOType))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtPOType.Focus();
					return;
				}
				
				#endregion

				//Clear all manual data before import
				dstPODetail.Tables[0].Clear();

				//Do Import
				if (dlgOpenImpFile.ShowDialog() == DialogResult.OK)
				{
					DataTable dt = ReadImportData(dlgOpenImpFile.FileName);
					if (dt == null)
					{
						//Invalid file
						PCSMessageBox.Show(ErrorCode.MESSAGE_IMPORT_ERROR_READ_FILE, MessageBoxIcon.Error);
						return;
					}
					else 
					{
						int intCheck = CheckImportData(dt);
						if (intCheck != -1)
						{
							dstPODetail.Clear();
							PCSMessageBox.Show(ErrorCode.MESSAGE_IMPORT_ERROR_FILE_FORMAT, MessageBoxIcon.Error);
							HighlightExcelFile(intCheck, dt.Columns.Count, dlgOpenImpFile.FileName);
							return;					
						}
					}

					int intPartyID = int.Parse(txtVendor.Tag.ToString());
					int intCCNID = int.Parse(cboCCN.SelectedValue.ToString());

					//Get data
					int intMaxLine = 0;
					if(gridData.RowCount > 0)
					{
						try
						{
							intMaxLine = int.Parse(gridData[gridData.RowCount-1, PO_PurchaseOrderDetailTable.LINE_FLD].ToString());
						}
						catch
						{
							intMaxLine = 0;
						}
						dstPODetail.Tables[0].DefaultView.Sort = PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD;
						dstPODetail.Tables[0].DefaultView.Sort = string.Empty;
					}
					else
						intMaxLine = 0;

					DataSet dstMappingData = dstPODetail;
					blnIsChangedGrid = false;
					
					PurchaseOrderBO boPurchaseOrder = new PurchaseOrderBO();
					int intErrorLine = boPurchaseOrder.ImportNewMappingData(dt, intPartyID, intCCNID, intMaxLine, dstMappingData);
					if (intErrorLine == 0)
					{
						ReCalculate();
						StoreDatabaseAfterImport(dt);

						mFormAction = EnumAction.Default;
						btnSave.Enabled = false;
						btnEdit.Enabled = true;
						btnDelete.Enabled = true;
						btnPrint.Enabled = true;
						SetEnableButtons();
						SetEditableControl(false);
						string[] arrParam = {STR_IMPORT_TASK};
						PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED,MessageBoxIcon.Information,arrParam);
						blnHasError = false;
					}
					else
					{
						//Cannot map data
						dstPODetail.Clear();
						if (intErrorLine > 0)
							PCSMessageBox.Show(ErrorCode.MESSAGE_IMPORT_ERROR_MAP_ITEM, MessageBoxIcon.Error);
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

		private bool IsNotExistCustomer()
		{
			try
			{
				string strVendor = txtVendor.Text.Trim();
				if(strVendor.Length > 0)
				{
					DataRowView drvResult = FormControlComponents.OpenSearchForm(MST_PartyTable.TABLE_NAME, MST_PartyTable.CODE_FLD, strVendor, null, false);
					if(drvResult == null) 
						return true;
				}
				return false;
			}
			catch
			{
				return true;
			}	
		}
		
		/// <summary>
		/// Read import data from excel file
		/// </summary>
		/// <param name="strFilename">File name</param>
		/// <returns></returns>
		private DataTable ReadImportData(string strFilename)
		{
			const string CONN_STR_TEMPL = "Provider=Microsoft.Jet.OLEDB.4.0;"
					  + "Data Source = {0};"
					  + "Extended Properties=\"Excel 8.0;Imex=2;HDR=No\"";
			const string CMD_STR_TEMPL  = "Select * FROM [{0}$]"; //
			const int MAIN_SHEET_IDX = 0;
			const string SHEET_NAME_IDX = "TABLE_NAME";
			const string DOLLAR = "$";
			
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
				string strConnStr = string.Format(CONN_STR_TEMPL,strNewFile);
				DataTable dtbPO = new DataTable();

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
					return null;
				string strSheetName = dtSChema.Rows[MAIN_SHEET_IDX][SHEET_NAME_IDX].ToString();
				if (strSheetName.EndsWith(DOLLAR))
					strSheetName = strSheetName.Substring(0,strSheetName.Length - 1);
				string strCmdSelect = string.Format(CMD_STR_TEMPL,strSheetName);
				ocmdExcelSelect.CommandText = strCmdSelect;

				//Fill table
				OleDbDataAdapter oleAdapter = new OleDbDataAdapter();   
				oleAdapter.SelectCommand = ocmdExcelSelect;
				oleAdapter.FillSchema(dtbPO,SchemaType.Source);  
				oleAdapter.Fill(dtbPO);

				return dtbPO;
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
						File.Delete(strNewFile);
					}
				}
			}
		}

		private int CheckImportData(DataTable dtImpData)
		{			
			const string METHOD_NAME = THIS + ".CheckImportData()";

			const int INT_MAX_MONTH = 12;
			const string DATE_SLASH = "/";
			try
			{
				//First, check minumum table size
				if ((dtImpData.Rows.Count < INT_MINIMUM_ROW) || (dtImpData.Columns.Count < INT_MINIMUM_COL))
					return INT_TITLE_ROW;

				//Second, check title to get month
				string strMMYYYY = dtImpData.Rows[INT_TITLE_ROW][INT_TITLE_COL].ToString();
				int intMonth = 0, intYear = 0;
				if (strMMYYYY.Equals(string.Empty))
					return INT_TITLE_ROW;
				else
				{
					try
					{
						intMonth = int.Parse(strMMYYYY.Substring(0,strMMYYYY.IndexOf(DATE_SLASH)));
						if (intMonth > INT_MAX_MONTH)
							return INT_TITLE_ROW;
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
							return INT_HEADER_ROW;
						else
							intLastDay = intCurrentDay;
					}
					catch
					{
						return INT_HEADER_ROW;
					}
				}
				
				//Forth, check if day in month is valid
				if (DateTime.DaysInMonth(intYear,intMonth) < intLastDay)
					return INT_HEADER_ROW;

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

		/// <summary>
		/// Store to database after inport
		/// </summary>
		/// <param name="dtImportData"></param>
		/// <returns></returns>
		private int StoreDatabaseAfterImport(DataTable dtImportData)
		{
			int intRet = 0;
			int intErrorLine = -1;
			ControlsToVO();
			PurchaseOrderBO boPurchaseOrder = new PurchaseOrderBO();
			if(mFormAction == EnumAction.Add)
			{
				intRet = voPOMaster.PurchaseOrderMasterID = boPurchaseOrder.ImportNewPurchaseOrder(voPOMaster, dstPODetail);
				UpdateSchedule(intRet, dstPODetail, dtImportData);
				InitGrid(voPOMaster.PurchaseOrderMasterID);
			}
			else if(mFormAction == EnumAction.Edit)
			{
				try
				{
					boPurchaseOrder.ImportUpdatePurchaseOrder(voPOMaster.PurchaseOrderMasterID, dstPODetail, ref intErrorLine);
					UpdateScheduleForImportUpdate(voPOMaster.PurchaseOrderMasterID, dstPODetail, dtImportData, ref intErrorLine);
				}
				catch (Exception ex)
				{
					HighlightExcelFile(intErrorLine, dtImportData.Columns.Count,dlgOpenImpFile.FileName);
					throw ex;
				}
				InitGrid(voPOMaster.PurchaseOrderMasterID);
			}
			Security.UpdateUserNameModifyTransaction(this,PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD, voPOMaster.PurchaseOrderMasterID);
			return intRet;
		}

		private void UpdateSchedule(int pintMasterID,DataSet pdstDetail, DataTable dtImportData)
		{
			//get delivery schedules schema
			PurchaseOrderBO boPurchaseOrder = new PurchaseOrderBO();
			int intMonth = 0,intYear = 0;
			DataSet dstDeli = boPurchaseOrder.ListScheduleForImport(pintMasterID);
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
			dstDeli.Tables[PO_DeliveryScheduleTable.TABLE_NAME].Columns.Add(objCol);

			#region //insert delivery schedules

			int intEndDataCol = dtImportData.Columns.Count - 1;
			int intEndDataRow = dtImportData.Rows.Count;
			int intIdx = 0;
			foreach (DataRow objRow in pdstDetail.Tables[0].Rows)
			{
				int intLine = 0;
				for (int i = INT_BEGIN_DATA_COL; i < intEndDataCol; i++)
				{
					for (int j = INT_BEGIN_DATA_ROW; j < intEndDataRow; j++)
					{
						if (dtImportData.Rows[j][i] == DBNull.Value)
							continue;
						string strPartNoExcel = dtImportData.Rows[j][INT_PARTS_NO_COL].ToString();
						string strPartNo = objRow[ITM_ProductTable.CODE_FLD].ToString();
						if (strPartNoExcel == strPartNo)
						{
							int intDay = int.Parse(dtImportData.Rows[INT_HEADER_ROW][i].ToString());
							int intHour = int.Parse(dtImportData.Rows[j][INT_TIME_COL].ToString());
							DateTime dateDeli = new DateTime(intYear, intMonth, intDay, intHour, 0, 0);
							decimal dcmQty;
							if (dtImportData.Rows[j][i] == DBNull.Value)
								continue;
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

							//Check if this schedule exist, only update quantity
							string strFilter = PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + "=" + objRow[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString() 
								+ " AND " + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "='" + dateDeli.ToString() + "'";
							DataRow[] arrSchRows = dstDeli.Tables[PO_DeliveryScheduleTable.TABLE_NAME].Select(strFilter);
							if (arrSchRows.Length > 0)
							{
								arrSchRows[0][TEMP_QTY_COL_NAME] = decimal.Parse(arrSchRows[0][TEMP_QTY_COL_NAME].ToString()) + dcmQty;
								continue;
							}

							DataRow drDeli = dstDeli.Tables[PO_DeliveryScheduleTable.TABLE_NAME].NewRow();
							drDeli[PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD] = objRow[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD];
							drDeli[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = dcmQty;
							drDeli[TEMP_QTY_COL_NAME] = dcmQty;
							drDeli[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dateDeli;
							drDeli[PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = intLine;
							dstDeli.Tables[PO_DeliveryScheduleTable.TABLE_NAME].Rows.Add(drDeli);
						}
					}
				}
				intIdx++;
			}

			#endregion

			#region //refine DelSch data

			int intNewLine = 0;
			DataRow[] arrRows = dstDeli.Tables[0].Select(string.Empty, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD);
			int intLastDetailID = -1;
			for (int i = 0; i < arrRows.Length; i++)
			{
				int intDetailID = int.Parse(arrRows[i][PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());

				if (intDetailID != intLastDetailID)
				{
					//reset line
					intNewLine = 0;
					intLastDetailID = intDetailID;
				}
				if (decimal.Parse(arrRows[i][TEMP_QTY_COL_NAME].ToString()) == 0)
					arrRows[i].Delete();
				else
				{
					//update line
					arrRows[i][PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = ++intNewLine;
					//only update qty
					decimal dcmNewQTy = decimal.Parse(arrRows[i][TEMP_QTY_COL_NAME].ToString());
					arrRows[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = dcmNewQTy;
				}
			}

			#endregion

			boPurchaseOrder.UpdateScheduleForImport(dstDeli);
			dstDeli.Tables[PO_DeliveryScheduleTable.TABLE_NAME].Columns.Remove(objCol);
		}
		#endregion

		#region Import update functions

		private void mnuImportUpdate_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".mnuImportNew_Click()";
			this.Cursor = Cursors.WaitCursor;
			try
			{
				PurchaseOrderBO boPurchaseOrder = new PurchaseOrderBO();
				if ((dstPODetail.Tables[0].GetChanges() != null) && (dstPODetail.Tables[0].GetChanges().Rows.Count != 0))
				{
					if (PCSMessageBox.Show(ErrorCode.MESSAGE_SAVE_BEFORE_IMPORT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
					{
						//save data
						#region check mandatory
						if(CheckMandatory(cboCCN))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							cboCCN.Focus();
							return;
						}
						if(CheckMandatory(cboOrderDate))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							cboOrderDate.Focus();
							return;
						}

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
						if(CheckMandatory(txtVendor))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtVendor.Focus();
							return;
						}
						if(IsNotExistCustomer())
						{
							// This customer does not exist in database
							PCSMessageBox.Show(ErrorCode.MESSAGE_CUSTOMER_DOES_NOT_EXIST,MessageBoxIcon.Exclamation);
							btnVendor.Focus();
							return;
						}
						if(CheckMandatory(txtVendorLoc))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtVendorLoc.Focus();
							return;
						}
						if(CheckMandatory(txtShipToLoc))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtShipToLoc.Focus();
							return;
						}
						if(CheckMandatory(txtInvToLoc))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtInvToLoc.Focus();
							return;
						}
						if(CheckMandatory(txtPOType))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
							txtPOType.Focus();
							return;
						}
						
						// Checks purchase order record
						if(gridData.RowCount <= 0)
						{
							// You have to input at least a record in grid purchase order detail
							PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_AT_LEAST_RECORD_IN_GRID,MessageBoxIcon.Exclamation);
							gridData.Focus();
							return;
						}
						//Check mandatory in grid
                        int intMaxRow = gridData.RowCount;
						
						//Check mandatory in grid.
						for (int i = 0; i < intMaxRow; i++)
						{
							if((gridData[i, PO_PurchaseOrderDetailTable.PRODUCTID_FLD]) == DBNull.Value)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ITEM_FIELD,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[ITM_ProductTable.CODE_FLD]);
								gridData.Focus();
								return;
							}
							if((gridData[i, PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD]) == DBNull.Value)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ORDER_QUANTITY_FIELD,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD]);
								gridData.Focus();
								return;
							}
							if(decimal.Parse(gridData[i, PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString()) <= 0)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_ORDER_QUANTITY_FIELD,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD]);
								gridData.Focus();
								return;
							}
							if((gridData[i, PO_PurchaseOrderDetailTable.BUYINGUMID_FLD]) == DBNull.Value)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_OF_MEASURE_FIELD,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD]);
								gridData.Focus();
								return;
							}
							if((gridData[i, PO_PurchaseOrderDetailTable.UNITPRICE_FLD]) == DBNull.Value)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_PRICE_FIELD,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.UNITPRICE_FLD]);
								gridData.Focus();
								return;
							}
							if(decimal.Parse(gridData[i, PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) < 0)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_UNIT_PRICE_FIELD,MessageBoxIcon.Exclamation);
								gridData.Row = i;
								gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.UNITPRICE_FLD]);
								gridData.Focus();
								return;
							}

							#region HACK: Trada 28-04-2006: 
							//Check Total Amount and Discount Amount
							if((gridData[i, PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD] != DBNull.Value)
								&&(gridData[i, PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
								&&(gridData[i, PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] != null)
								&&(gridData[i, PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD] != null))
							{
								if (decimal.Parse(gridData[i, PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString()) > decimal.Parse(gridData[i, PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD].ToString()))
								{
									string[] strParam = new string[2];
									strParam[0] = "Discount Amount";
									strParam[1] = "Total Amount";
									PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
									gridData.Row = i;
									gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD]);
									gridData.Focus();
									return;
								}
							}
							//Check Order Quantity must be greater than Total Del Quantity
							//HACKED : DuongNA - fix error when form in add new mode
							if ((dstPODetail.Tables.Count > 0)
								&& (gridData[i, PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] != DBNull.Value)
								&& (gridData[i, PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] != null))
							{
								if ((gridData[i, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD] != DBNull.Value)
									&& (gridData[i, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD] != null))
								{
									DataRow[] adrowSODeliverySchedule = dstPODetail.Tables[0].Select(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + " = " + gridData[i, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
									if (adrowSODeliverySchedule.Length > 0)
									{
										if (decimal.Parse(gridData[i, PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString()) < decimal.Parse(adrowSODeliverySchedule[0][TOTAL_DELIVERY_QTY].ToString()))
										{
											string[] strParam = new string[2];
											strParam[0] = "Order Quantity";
											strParam[1] = "Total Delivery";//TOTAL_DELIVERY_QTY;
											PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
											gridData.Row = i;
											gridData.Col = gridData.Columns.IndexOf(gridData.Columns[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD]);
											gridData.Focus();
											return;
										}
									}
								}
							}
							#endregion END: Trada 28-04-2006
						}

						#endregion

						// store to database
						boPurchaseOrder.UpdatePurchaseOrder(voPOMaster, dstPODetail);
					}
					else
						return;
				}

				#region //Check madatory
				if(CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					cboCCN.Focus();
					return;
				}
				if(CheckMandatory(cboOrderDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					cboOrderDate.Focus();
					return;
				}

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
				if(CheckMandatory(txtVendor))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtVendor.Focus();
					return;
				}
				if(IsNotExistCustomer())
				{
					// This customer does not exist in database
					PCSMessageBox.Show(ErrorCode.MESSAGE_CUSTOMER_DOES_NOT_EXIST,MessageBoxIcon.Exclamation);
					btnVendor.Focus();
					return;
				}
				if(CheckMandatory(txtVendorLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtVendorLoc.Focus();
					return;
				}
				if(CheckMandatory(txtShipToLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtShipToLoc.Focus();
					return;
				}
				if(CheckMandatory(txtInvToLoc))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtInvToLoc.Focus();
					return;
				}
				if(CheckMandatory(txtPOType))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID,MessageBoxIcon.Exclamation);
					txtPOType.Focus();
					return;
				}
				#endregion

				//Do Import
				if (dlgOpenImpFile.ShowDialog() == DialogResult.OK)
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

					int intPartyID = int.Parse(txtVendor.Tag.ToString());
					int intCCNID = int.Parse(cboCCN.SelectedValue.ToString());

					//Get data
					int intMaxLine = 0;
					if(gridData.RowCount > 0)
					{
						try
						{
							intMaxLine = int.Parse(gridData[gridData.RowCount-1, PO_PurchaseOrderDetailTable.LINE_FLD].ToString());
						}
						catch
						{
							intMaxLine = 0;
						}
						dstPODetail.Tables[0].DefaultView.Sort = string.Empty;
					}
					else
					{
						intMaxLine = 0;
					}

					DataSet dstMappingData = dstPODetail;
					blnIsChangedGrid = false;
					int intErrorLine = boPurchaseOrder.ImportUpdateMappingData(dt,intPartyID,intCCNID, intMaxLine, dstMappingData);
					if (intErrorLine == 0)
					{
						ReCalculate();
						StoreDatabaseAfterImport(dt);
						mFormAction = EnumAction.Default;
						btnSave.Enabled = false;
						SetEnableButtons();
						SetEditableControl(false);
						string[] arrParam = {STR_IMPORT_TASK};
						PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED,MessageBoxIcon.Information,arrParam);
						blnHasError = false;
					}
					else
					{
						if (intErrorLine > 0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_IMPORT_ERROR_MAP_ITEM, MessageBoxIcon.Error);
						}
						//reload data
						InitGrid(voPOMaster.PurchaseOrderMasterID);
						HighlightExcelFile(Math.Abs(intErrorLine),dt.Columns.Count,dlgOpenImpFile.FileName);
					}
				}
			}
			catch (PCSException ex)
			{
				//reload data
				InitGrid(voPOMaster.PurchaseOrderMasterID);
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
		private void UpdateScheduleForImportUpdate(int pintMasterID, DataSet pdstDetail, DataTable dtImportData, ref int pintErrorLine)
		{
			const string METHOD_NAME = THIS + ".UpdateScheduleForImportUpdate()";
			const string TEMP_QTY_COL_NAME = "TempQty";
			const string SUMCOMMITQUANTITY_FLD = "SUMCommitQuantity";

			int intMonth = 0,intYear = 0;
			string strMMYYYY = dtImportData.Rows[0][0].ToString();
			const string DATE_SLASH = "/";
			try
			{
				intMonth = int.Parse(strMMYYYY.Substring(0,strMMYYYY.IndexOf(DATE_SLASH)));
				intYear = int.Parse(strMMYYYY.Substring(strMMYYYY.IndexOf(DATE_SLASH)+DATE_SLASH.Length));
			}
			catch{}

			if(pdstDetail.Tables.Count > 0)
			{
				PurchaseOrderBO boPurchaseOrder = new PurchaseOrderBO();
				// get all current delivery schedule to delete first
				DataSet dstDelSchData = boPurchaseOrder.ListScheduleForImport(voPOMaster.PurchaseOrderMasterID);
				int intRowIdx = 0;

				#region delete old schedule

				foreach (DataRow drowDetail in pdstDetail.Tables[0].Rows)
				{
					if(drowDetail.RowState == DataRowState.Deleted)
						continue;
					if (int.Parse(drowDetail[PO_PurchaseOrderDetailTable.LINE_FLD].ToString()) == -1)
					{
						//delete all related 
						int intDetailID = int.Parse(drowDetail[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
						try
						{
							//offline
							DataRow[] arrDeliverySchedule =	dstDelSchData.Tables[0].Select(PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + "=" + intDetailID);
							foreach (DataRow drowDeliverySchedule in arrDeliverySchedule) 
							{
								if (Convert.ToDecimal(drowDeliverySchedule[SUMCOMMITQUANTITY_FLD]) == 0)
									drowDeliverySchedule.Delete();
								else
								{
									pintErrorLine = intRowIdx + INT_BEGIN_DATA_ROW;
									throw new PCSDBException(ErrorCode.MESSAGE_IMPORT_ERROR_UPDATE_COMMITTED,METHOD_NAME,null);
								}
							}
						}
						catch (Exception ex)
						{
							//cannot delete commited schedule
							pintErrorLine = intRowIdx + INT_BEGIN_DATA_ROW;
							throw new PCSDBException(ErrorCode.MESSAGE_IMPORT_ERROR_UPDATE_COMMITTED,METHOD_NAME,ex);
						}
						drowDetail.Delete();
						continue;
					}
					drowDetail[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD] = voPOMaster.PurchaseOrderMasterID;
					intRowIdx++;
				}

				//update deleted delivery schedule in dataset
				boPurchaseOrder.UpdateDeletedRowInDataSet(dstDelSchData, voPOMaster.PurchaseOrderMasterID);

				#endregion

				#region insert delivery schedules

				//Add temp column for new quantity
				DataColumn objCol = new DataColumn(TEMP_QTY_COL_NAME);
				objCol.DataType = typeof(decimal);
				objCol.DefaultValue = 0;
				dstDelSchData.Tables[PO_DeliveryScheduleTable.TABLE_NAME].Columns.Add(objCol);

				int intEndDataCol = dtImportData.Columns.Count - 1;
				int intEndDataRow = dtImportData.Rows.Count;
				intRowIdx = -1;
				foreach (DataRow objRow in pdstDetail.Tables[0].Rows)
				{
					intRowIdx++;
					int intLine = 0;
					for (int i = INT_BEGIN_DATA_COL; i < intEndDataCol; i++)
					{
						for (int j = INT_BEGIN_DATA_ROW; j < intEndDataRow; j++)
						{
							if (dtImportData.Rows[j][INT_PARTS_NO_COL].ToString().Equals(objRow[ITM_ProductTable.CODE_FLD].ToString()))
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

								//Check if this schedule exist, only update quantity
								string strFilter = PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + "=" + objRow[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString() 
									+ " AND " + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "='" + dateDeli.ToString() + "'";

								DataRow[] arrSchRows = dstDelSchData.Tables[PO_DeliveryScheduleTable.TABLE_NAME].Select(strFilter);
								if (arrSchRows.Length > 0)
								{
									arrSchRows[0][TEMP_QTY_COL_NAME] = decimal.Parse(arrSchRows[0][TEMP_QTY_COL_NAME].ToString()) + dcmQty;
									continue;
								}

								DataRow drDeli = dstDelSchData.Tables[PO_DeliveryScheduleTable.TABLE_NAME].NewRow();
								drDeli[TEMP_QTY_COL_NAME] = dcmQty;
								drDeli[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = dcmQty;
								drDeli[PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD] = objRow[PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD];
								drDeli[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dateDeli;
								drDeli[PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = intLine;
								dstDelSchData.Tables[PO_DeliveryScheduleTable.TABLE_NAME].Rows.Add(drDeli);
							}
						}
					}
				}

				#endregion

				#region refine DelSch data

				int intNewLine = 0;
				DataRow[] arrRows = dstDelSchData.Tables[0].Select(string.Empty, PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD);
				int intLastDetailID = -1;
				int intCurrentProductID = -1;
				
				intRowIdx = -1;
				for (int i = 0; i < arrRows.Length; i++)
				{
					int intDetailID = int.Parse(arrRows[i][PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD].ToString());

					if (intDetailID != intLastDetailID)
					{
						//reset line
						intNewLine = 0;
						intLastDetailID = intDetailID;
						intRowIdx++;

						//get new product id
						DataRow[] arrDetails = pdstDetail.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME].Select(PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD+ "=" + intLastDetailID.ToString());
						//check if not found? not needed
						intCurrentProductID = int.Parse(arrDetails[0][PO_PurchaseOrderDetailTable.PRODUCTID_FLD].ToString());
					}
					if (decimal.Parse(arrRows[i][TEMP_QTY_COL_NAME].ToString()) == 0)
						arrRows[i].Delete();
					else
					{
						if (arrRows[i].RowState != DataRowState.Added)
						{
							//update line
							arrRows[i][PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = ++intNewLine;
							//only update qty
							decimal dcmNewQTy = decimal.Parse(arrRows[i][TEMP_QTY_COL_NAME].ToString());
							arrRows[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = dcmNewQTy;
						}
						else
						{
							//update line
							arrRows[i][PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = ++intNewLine;
							arrRows[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = arrRows[i][TEMP_QTY_COL_NAME];
						}
					}
				}

				#endregion

				dstDelSchData.Tables[PO_DeliveryScheduleTable.TABLE_NAME].Columns.Remove(objCol);
				boPurchaseOrder.UpdateInsertedRowInDataSet(dstDelSchData, pintMasterID);
			}
		}
		#endregion
	}


	public enum POFormState
	{
		Normal = 1,
		ToNewCPO = 2,
		ToExistCPO = 3
	}
}
