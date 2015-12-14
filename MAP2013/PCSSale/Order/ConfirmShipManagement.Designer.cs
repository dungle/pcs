using System.Data;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1TrueDBGrid;

namespace PCSSale.Order
{
    partial class ConfirmShipManagement
    {
        private const string This = "PCSSale.Order.ConfirmShipManagement";
        private const string ViewForInvoice = "v_SaleInvoice";
        private const string CommittedqtyCol = "CommittedQuantity";
        private const string NetAmountRateCol = "NetAmountRate";
        private const string OldInvoiceqtyCol = "OldInvoiceQty";
        private const string APPLICATION_PATH = @"PCSMain\bin\Debug";
        private const string SO_INVOICE_STANDARD_REPORT = "Invoice4SaleOrder.xml";
        private const string SO_INVOICE_APPENDIX_REPORT = "Invoice4SaleOrder_Appendix.xml";
        private const string REPORT_NAME = "Sale Order Invoice";
        private const string REPORTFLD_COMPANY = "fldCompany";
        private const string REPORTFLD_ADDRESS = "fldAddress";
        private const string REPORTFLD_TEL = "fldTel";
        private const string REPORTFLD_FAX = "fldFax";
        private const string REPORTFLD_AMOUNT_IN_WORD = "fldAmountInWord";
        private const string REPORTFLD_AMOUNT_IN_WORD1 = "fldAmountInWord1";
        private const string REPORTFLD_TOTAL_AMOUNT = "fldSumTotalNetAmount";

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmShipManagement));
            this.dtmShipmentDate = new C1.Win.C1Input.C1DateEdit();
            this.btnSearchMasLoc = new System.Windows.Forms.Button();
            this.txtMasLoc = new System.Windows.Forms.TextBox();
            this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnConfirmShippment = new System.Windows.Forms.Button();
            this.lblShippedDate = new System.Windows.Forms.Label();
            this.lblCCN = new System.Windows.Forms.Label();
            this.cboCCN = new C1.Win.C1List.C1Combo();
            this.lblMasLoc = new System.Windows.Forms.Label();
            this.txtConfirmShipNo = new System.Windows.Forms.TextBox();
            this.lblShipNo = new System.Windows.Forms.Label();
            this.btnShipNo = new System.Windows.Forms.Button();
            this.btnSO = new System.Windows.Forms.Button();
            this.txtSalesOrder = new System.Windows.Forms.TextBox();
            this.lblSO = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.lblCustomerCode = new System.Windows.Forms.Label();
            this.txtSaleType = new System.Windows.Forms.TextBox();
            this.lblSaleType = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.txtCurrency = new System.Windows.Forms.TextBox();
            this.btnCurrency = new System.Windows.Forms.Button();
            this.txtExchRate = new C1.Win.C1Input.C1NumericEdit();
            this.lblExchRate = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.btnSaleType = new System.Windows.Forms.Button();
            this.chkHaveGate = new System.Windows.Forms.CheckBox();
            this.txtGate = new System.Windows.Forms.TextBox();
            this.btnGate = new System.Windows.Forms.Button();
            this.lblGate = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.grpMoreDetail = new System.Windows.Forms.GroupBox();
            this.btnSearchBin = new System.Windows.Forms.Button();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.txtBin = new System.Windows.Forms.TextBox();
            this.txtNetWeight = new C1.Win.C1Input.C1NumericEdit();
            this.txtGrossWeight = new C1.Win.C1Input.C1NumericEdit();
            this.lblBin = new System.Windows.Forms.Label();
            this.dtmLCDate = new C1.Win.C1Input.C1DateEdit();
            this.dtmOnBoardDate = new C1.Win.C1Input.C1DateEdit();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblOnBoardDate = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.btnSearchLocation = new System.Windows.Forms.Button();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.txtPaymentTerm = new System.Windows.Forms.TextBox();
            this.lblPaymentTerm = new System.Windows.Forms.Label();
            this.txtVessel = new System.Windows.Forms.TextBox();
            this.lblVessel = new System.Windows.Forms.Label();
            this.txtLCNo = new System.Windows.Forms.TextBox();
            this.lblLCNo = new System.Windows.Forms.Label();
            this.lblLCDate = new System.Windows.Forms.Label();
            this.txtIssuingBank = new System.Windows.Forms.TextBox();
            this.lblIssuingBank = new System.Windows.Forms.Label();
            this.lblNetWeight = new System.Windows.Forms.Label();
            this.lblGrossWeight = new System.Windows.Forms.Label();
            this.lblMeasurement = new System.Windows.Forms.Label();
            this.txtCNo = new System.Windows.Forms.TextBox();
            this.lblCNO = new System.Windows.Forms.Label();
            this.txtFromPort = new System.Windows.Forms.TextBox();
            this.lblFromPort = new System.Windows.Forms.Label();
            this.txtShippingCode = new System.Windows.Forms.TextBox();
            this.lblShippingCode = new System.Windows.Forms.Label();
            this.txtMeasurement = new C1.Win.C1Input.C1NumericEdit();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnPrintConfiguration = new System.Windows.Forms.Button();
            this.btnAttachedSheet = new System.Windows.Forms.Button();
            this.lblVATInInvoice = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.dtmInvoiceDate = new C1.Win.C1Input.C1DateEdit();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
            this.lblPurpose = new System.Windows.Forms.Label();
            this.cboPurpose = new System.Windows.Forms.ComboBox();
            this.lblPONo = new System.Windows.Forms.Label();
            this.txtPONo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtmShipmentDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchRate)).BeginInit();
            this.grpMoreDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGrossWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmLCDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmOnBoardDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeasurement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmInvoiceDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).BeginInit();
            this.SuspendLayout();
            // 
            // dtmShipmentDate
            // 
            this.dtmShipmentDate.AccessibleDescription = "";
            this.dtmShipmentDate.AccessibleName = "";
            this.dtmShipmentDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.dtmShipmentDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dtmShipmentDate.Calendar.AccessibleDescription = "";
            this.dtmShipmentDate.Calendar.AccessibleName = "";
            this.dtmShipmentDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dtmShipmentDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmShipmentDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmShipmentDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmShipmentDate.CustomFormat = "dd-MM-yyyy HH:mm:ss";
            this.dtmShipmentDate.EmptyAsNull = true;
            this.dtmShipmentDate.ErrorInfo.ShowErrorMessage = false;
            this.dtmShipmentDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmShipmentDate.Location = new System.Drawing.Point(108, 98);
            this.dtmShipmentDate.Name = "dtmShipmentDate";
            this.dtmShipmentDate.Size = new System.Drawing.Size(118, 18);
            this.dtmShipmentDate.TabIndex = 26;
            this.dtmShipmentDate.Tag = null;
            this.dtmShipmentDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmShipmentDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmShipmentDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmShipmentDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmShipmentDate.ValueChanged += new System.EventHandler(this.dtmShipmentDate_ValueChanged);
            // 
            // btnSearchMasLoc
            // 
            this.btnSearchMasLoc.AccessibleDescription = "";
            this.btnSearchMasLoc.AccessibleName = "";
            this.btnSearchMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchMasLoc.Location = new System.Drawing.Point(228, 28);
            this.btnSearchMasLoc.Name = "btnSearchMasLoc";
            this.btnSearchMasLoc.Size = new System.Drawing.Size(22, 20);
            this.btnSearchMasLoc.TabIndex = 9;
            this.btnSearchMasLoc.Text = "...";
            this.btnSearchMasLoc.Click += new System.EventHandler(this.btnSearchMasLoc_Click);
            // 
            // txtMasLoc
            // 
            this.txtMasLoc.AccessibleDescription = "";
            this.txtMasLoc.AccessibleName = "";
            this.txtMasLoc.Location = new System.Drawing.Point(108, 28);
            this.txtMasLoc.Name = "txtMasLoc";
            this.txtMasLoc.Size = new System.Drawing.Size(118, 20);
            this.txtMasLoc.TabIndex = 8;
            this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
            this.txtMasLoc.Leave += new System.EventHandler(this.txtMasLoc_Leave);
            this.txtMasLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasLoc_Validating);
            // 
            // dgrdData
            // 
            this.dgrdData.AccessibleDescription = "";
            this.dgrdData.AccessibleName = "";
            this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdData.CaptionHeight = 19;
            this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("dgrdData.Images"))));
            this.dgrdData.Location = new System.Drawing.Point(7, 297);
            this.dgrdData.Name = "dgrdData";
            this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.dgrdData.PreviewInfo.ZoomFactor = 75D;
            this.dgrdData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("dgrdData.PrintInfo.PageSettings")));
            this.dgrdData.RecordSelectorWidth = 16;
            this.dgrdData.RowHeight = 17;
            this.dgrdData.Size = new System.Drawing.Size(1018, 206);
            this.dgrdData.TabIndex = 41;
            this.dgrdData.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;
            this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
            this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
            this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
            this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = "";
            this.btnClose.AccessibleName = "";
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(958, 509);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 23);
            this.btnClose.TabIndex = 49;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.AccessibleDescription = "";
            this.btnHelp.AccessibleName = "";
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(891, 509);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(65, 23);
            this.btnHelp.TabIndex = 48;
            this.btnHelp.Text = "&Help";
            // 
            // btnConfirmShippment
            // 
            this.btnConfirmShippment.AccessibleDescription = "";
            this.btnConfirmShippment.AccessibleName = "";
            this.btnConfirmShippment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfirmShippment.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnConfirmShippment.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnConfirmShippment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConfirmShippment.Location = new System.Drawing.Point(71, 509);
            this.btnConfirmShippment.Name = "btnConfirmShippment";
            this.btnConfirmShippment.Size = new System.Drawing.Size(65, 24);
            this.btnConfirmShippment.TabIndex = 43;
            this.btnConfirmShippment.Text = "Con&firm";
            this.btnConfirmShippment.Click += new System.EventHandler(this.btnConfirmShippment_Click);
            // 
            // lblShippedDate
            // 
            this.lblShippedDate.AccessibleDescription = "";
            this.lblShippedDate.AccessibleName = "";
            this.lblShippedDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblShippedDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblShippedDate.Location = new System.Drawing.Point(4, 98);
            this.lblShippedDate.Name = "lblShippedDate";
            this.lblShippedDate.Size = new System.Drawing.Size(104, 20);
            this.lblShippedDate.TabIndex = 25;
            this.lblShippedDate.Text = "Shipped Date, Time";
            this.lblShippedDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCCN
            // 
            this.lblCCN.AccessibleDescription = "";
            this.lblCCN.AccessibleName = "";
            this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCCN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCCN.Location = new System.Drawing.Point(903, 5);
            this.lblCCN.Name = "lblCCN";
            this.lblCCN.Size = new System.Drawing.Size(31, 20);
            this.lblCCN.TabIndex = 2;
            this.lblCCN.Text = "CCN";
            this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboCCN
            // 
            this.cboCCN.AccessibleDescription = "";
            this.cboCCN.AccessibleName = "";
            this.cboCCN.AddItemSeparator = ';';
            this.cboCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCCN.Caption = "";
            this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCCN.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCCN.Images"))));
            this.cboCCN.Location = new System.Drawing.Point(933, 5);
            this.cboCCN.MatchEntryTimeout = ((long)(2000));
            this.cboCCN.MaxDropDownItems = ((short)(5));
            this.cboCCN.MaxLength = 32767;
            this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCCN.Name = "cboCCN";
            this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCCN.Size = new System.Drawing.Size(90, 21);
            this.cboCCN.TabIndex = 3;
            this.cboCCN.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue;
            this.cboCCN.PropBag = resources.GetString("cboCCN.PropBag");
            // 
            // lblMasLoc
            // 
            this.lblMasLoc.AccessibleDescription = "";
            this.lblMasLoc.AccessibleName = "";
            this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMasLoc.Location = new System.Drawing.Point(4, 28);
            this.lblMasLoc.Name = "lblMasLoc";
            this.lblMasLoc.Size = new System.Drawing.Size(104, 20);
            this.lblMasLoc.TabIndex = 7;
            this.lblMasLoc.Text = "Master Location";
            this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtConfirmShipNo
            // 
            this.txtConfirmShipNo.AccessibleDescription = "";
            this.txtConfirmShipNo.AccessibleName = "";
            this.txtConfirmShipNo.Location = new System.Drawing.Point(108, 5);
            this.txtConfirmShipNo.Name = "txtConfirmShipNo";
            this.txtConfirmShipNo.Size = new System.Drawing.Size(118, 20);
            this.txtConfirmShipNo.TabIndex = 5;
            this.txtConfirmShipNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConfirmShipNo_KeyDown);
            this.txtConfirmShipNo.Leave += new System.EventHandler(this.txtConfirmShipNo_Leave);
            this.txtConfirmShipNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtConfirmShipNo_Validating);
            // 
            // lblShipNo
            // 
            this.lblShipNo.AccessibleDescription = "";
            this.lblShipNo.AccessibleName = "";
            this.lblShipNo.ForeColor = System.Drawing.Color.Maroon;
            this.lblShipNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblShipNo.Location = new System.Drawing.Point(4, 5);
            this.lblShipNo.Name = "lblShipNo";
            this.lblShipNo.Size = new System.Drawing.Size(104, 20);
            this.lblShipNo.TabIndex = 4;
            this.lblShipNo.Text = "Confirm Ship No.";
            this.lblShipNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnShipNo
            // 
            this.btnShipNo.AccessibleDescription = "";
            this.btnShipNo.AccessibleName = "";
            this.btnShipNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnShipNo.Location = new System.Drawing.Point(228, 5);
            this.btnShipNo.Name = "btnShipNo";
            this.btnShipNo.Size = new System.Drawing.Size(22, 20);
            this.btnShipNo.TabIndex = 6;
            this.btnShipNo.Text = "...";
            this.btnShipNo.Click += new System.EventHandler(this.btnShipNo_Click);
            // 
            // btnSO
            // 
            this.btnSO.AccessibleDescription = "";
            this.btnSO.AccessibleName = "";
            this.btnSO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSO.Location = new System.Drawing.Point(228, 74);
            this.btnSO.Name = "btnSO";
            this.btnSO.Size = new System.Drawing.Size(22, 20);
            this.btnSO.TabIndex = 15;
            this.btnSO.Text = "...";
            this.btnSO.Click += new System.EventHandler(this.btnSO_Click);
            // 
            // txtSalesOrder
            // 
            this.txtSalesOrder.AccessibleDescription = "";
            this.txtSalesOrder.AccessibleName = "";
            this.txtSalesOrder.Location = new System.Drawing.Point(108, 74);
            this.txtSalesOrder.Name = "txtSalesOrder";
            this.txtSalesOrder.Size = new System.Drawing.Size(118, 20);
            this.txtSalesOrder.TabIndex = 14;
            this.txtSalesOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesOrder_KeyDown);
            this.txtSalesOrder.Leave += new System.EventHandler(this.txtSalesOrder_Leave);
            this.txtSalesOrder.Validating += new System.ComponentModel.CancelEventHandler(this.txtSalesOrder_Validating);
            // 
            // lblSO
            // 
            this.lblSO.AccessibleDescription = "";
            this.lblSO.AccessibleName = "";
            this.lblSO.ForeColor = System.Drawing.Color.Maroon;
            this.lblSO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSO.Location = new System.Drawing.Point(4, 74);
            this.lblSO.Name = "lblSO";
            this.lblSO.Size = new System.Drawing.Size(104, 20);
            this.lblSO.TabIndex = 13;
            this.lblSO.Text = "Sales Order";
            this.lblSO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleDescription = "";
            this.btnAdd.AccessibleName = "";
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(5, 509);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 24);
            this.btnAdd.TabIndex = 42;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleDescription = "";
            this.btnPrint.AccessibleName = "";
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrint.Location = new System.Drawing.Point(312, 509);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(65, 24);
            this.btnPrint.TabIndex = 46;
            this.btnPrint.Text = "&Print";
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.AccessibleDescription = "";
            this.txtCustomerCode.AccessibleName = "";
            this.txtCustomerCode.Location = new System.Drawing.Point(340, 51);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.ReadOnly = true;
            this.txtCustomerCode.Size = new System.Drawing.Size(100, 20);
            this.txtCustomerCode.TabIndex = 22;
            // 
            // lblCustomerCode
            // 
            this.lblCustomerCode.AccessibleDescription = "";
            this.lblCustomerCode.AccessibleName = "";
            this.lblCustomerCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCustomerCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCustomerCode.Location = new System.Drawing.Point(258, 50);
            this.lblCustomerCode.Name = "lblCustomerCode";
            this.lblCustomerCode.Size = new System.Drawing.Size(84, 20);
            this.lblCustomerCode.TabIndex = 21;
            this.lblCustomerCode.Text = "Customer Code";
            this.lblCustomerCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSaleType
            // 
            this.txtSaleType.AccessibleDescription = "";
            this.txtSaleType.AccessibleName = "";
            this.txtSaleType.Location = new System.Drawing.Point(108, 51);
            this.txtSaleType.Name = "txtSaleType";
            this.txtSaleType.Size = new System.Drawing.Size(118, 20);
            this.txtSaleType.TabIndex = 11;
            this.txtSaleType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSaleType_KeyDown);
            this.txtSaleType.Validating += new System.ComponentModel.CancelEventHandler(this.txtSaleType_Validating);
            // 
            // lblSaleType
            // 
            this.lblSaleType.AccessibleDescription = "";
            this.lblSaleType.AccessibleName = "";
            this.lblSaleType.ForeColor = System.Drawing.Color.Maroon;
            this.lblSaleType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSaleType.Location = new System.Drawing.Point(4, 52);
            this.lblSaleType.Name = "lblSaleType";
            this.lblSaleType.Size = new System.Drawing.Size(84, 20);
            this.lblSaleType.TabIndex = 10;
            this.lblSaleType.Text = "Type";
            this.lblSaleType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.AccessibleDescription = "";
            this.txtCustomerName.AccessibleName = "";
            this.txtCustomerName.Location = new System.Drawing.Point(340, 74);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(286, 20);
            this.txtCustomerName.TabIndex = 24;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AccessibleDescription = "";
            this.lblCustomerName.AccessibleName = "";
            this.lblCustomerName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCustomerName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCustomerName.Location = new System.Drawing.Point(258, 74);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(88, 20);
            this.lblCustomerName.TabIndex = 23;
            this.lblCustomerName.Text = "Customer Name";
            this.lblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCurrency
            // 
            this.txtCurrency.AccessibleDescription = "";
            this.txtCurrency.AccessibleName = "";
            this.txtCurrency.Location = new System.Drawing.Point(340, 5);
            this.txtCurrency.Name = "txtCurrency";
            this.txtCurrency.Size = new System.Drawing.Size(100, 20);
            this.txtCurrency.TabIndex = 17;
            this.txtCurrency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrency_KeyDown);
            this.txtCurrency.Validating += new System.ComponentModel.CancelEventHandler(this.txtCurrency_Validating);
            // 
            // btnCurrency
            // 
            this.btnCurrency.AccessibleDescription = "";
            this.btnCurrency.AccessibleName = "";
            this.btnCurrency.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCurrency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCurrency.Location = new System.Drawing.Point(442, 5);
            this.btnCurrency.Name = "btnCurrency";
            this.btnCurrency.Size = new System.Drawing.Size(24, 20);
            this.btnCurrency.TabIndex = 18;
            this.btnCurrency.Text = "...";
            this.btnCurrency.Click += new System.EventHandler(this.btnCurrency_Click);
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
            this.txtExchRate.Location = new System.Drawing.Point(340, 28);
            this.txtExchRate.Name = "txtExchRate";
            this.txtExchRate.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            new C1.Win.C1Input.ValueInterval(new decimal(new int[] {
                            0,
                            0,
                            0,
                            0}), new decimal(new int[] {
                            1410065407,
                            2,
                            0,
                            0}), false, true)});
            this.txtExchRate.Size = new System.Drawing.Size(100, 20);
            this.txtExchRate.TabIndex = 20;
            this.txtExchRate.Tag = null;
            this.txtExchRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtExchRate.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtExchRate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            this.txtExchRate.Validated += new System.EventHandler(this.txtExchRate_Validated);
            // 
            // lblExchRate
            // 
            this.lblExchRate.AccessibleDescription = "";
            this.lblExchRate.AccessibleName = "";
            this.lblExchRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblExchRate.ForeColor = System.Drawing.Color.Maroon;
            this.lblExchRate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblExchRate.Location = new System.Drawing.Point(260, 28);
            this.lblExchRate.Name = "lblExchRate";
            this.lblExchRate.Size = new System.Drawing.Size(65, 20);
            this.lblExchRate.TabIndex = 19;
            this.lblExchRate.Text = "Exch. Rate";
            this.lblExchRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrency
            // 
            this.lblCurrency.AccessibleDescription = "";
            this.lblCurrency.AccessibleName = "";
            this.lblCurrency.ForeColor = System.Drawing.Color.Maroon;
            this.lblCurrency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCurrency.Location = new System.Drawing.Point(260, 7);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(65, 18);
            this.lblCurrency.TabIndex = 16;
            this.lblCurrency.Text = "Currency";
            this.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSaleType
            // 
            this.btnSaleType.AccessibleDescription = "";
            this.btnSaleType.AccessibleName = "";
            this.btnSaleType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSaleType.Location = new System.Drawing.Point(228, 51);
            this.btnSaleType.Name = "btnSaleType";
            this.btnSaleType.Size = new System.Drawing.Size(22, 20);
            this.btnSaleType.TabIndex = 12;
            this.btnSaleType.Text = "...";
            this.btnSaleType.Click += new System.EventHandler(this.btnSaleType_Click);
            // 
            // chkHaveGate
            // 
            this.chkHaveGate.Location = new System.Drawing.Point(260, 98);
            this.chkHaveGate.Name = "chkHaveGate";
            this.chkHaveGate.Size = new System.Drawing.Size(104, 20);
            this.chkHaveGate.TabIndex = 29;
            this.chkHaveGate.Text = "Have Gate";
            this.chkHaveGate.CheckedChanged += new System.EventHandler(this.chkHaveGate_CheckedChanged);
            // 
            // txtGate
            // 
            this.txtGate.AccessibleDescription = "";
            this.txtGate.AccessibleName = "";
            this.txtGate.Location = new System.Drawing.Point(392, 98);
            this.txtGate.Name = "txtGate";
            this.txtGate.Size = new System.Drawing.Size(208, 20);
            this.txtGate.TabIndex = 30;
            this.txtGate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGate_KeyDown);
            this.txtGate.Validating += new System.ComponentModel.CancelEventHandler(this.txtGate_Validating);
            // 
            // btnGate
            // 
            this.btnGate.AccessibleDescription = "";
            this.btnGate.AccessibleName = "";
            this.btnGate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGate.Location = new System.Drawing.Point(602, 98);
            this.btnGate.Name = "btnGate";
            this.btnGate.Size = new System.Drawing.Size(24, 20);
            this.btnGate.TabIndex = 31;
            this.btnGate.Text = "...";
            this.btnGate.Click += new System.EventHandler(this.btnGate_Click);
            // 
            // lblGate
            // 
            this.lblGate.AccessibleDescription = "";
            this.lblGate.AccessibleName = "";
            this.lblGate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblGate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblGate.Location = new System.Drawing.Point(352, 98);
            this.lblGate.Name = "lblGate";
            this.lblGate.Size = new System.Drawing.Size(36, 20);
            this.lblGate.TabIndex = 33;
            this.lblGate.Text = "Gate";
            this.lblGate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(550, 146);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 39;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // grpMoreDetail
            // 
            this.grpMoreDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMoreDetail.Controls.Add(this.btnSearchBin);
            this.grpMoreDetail.Controls.Add(this.txtInvoiceNo);
            this.grpMoreDetail.Controls.Add(this.lblInvoiceNo);
            this.grpMoreDetail.Controls.Add(this.txtBin);
            this.grpMoreDetail.Controls.Add(this.txtNetWeight);
            this.grpMoreDetail.Controls.Add(this.txtGrossWeight);
            this.grpMoreDetail.Controls.Add(this.lblBin);
            this.grpMoreDetail.Controls.Add(this.dtmLCDate);
            this.grpMoreDetail.Controls.Add(this.dtmOnBoardDate);
            this.grpMoreDetail.Controls.Add(this.lblLocation);
            this.grpMoreDetail.Controls.Add(this.lblOnBoardDate);
            this.grpMoreDetail.Controls.Add(this.txtLocation);
            this.grpMoreDetail.Controls.Add(this.txtReferenceNo);
            this.grpMoreDetail.Controls.Add(this.btnSearchLocation);
            this.grpMoreDetail.Controls.Add(this.lblReferenceNo);
            this.grpMoreDetail.Controls.Add(this.txtComment);
            this.grpMoreDetail.Controls.Add(this.lblComment);
            this.grpMoreDetail.Controls.Add(this.txtPaymentTerm);
            this.grpMoreDetail.Controls.Add(this.lblPaymentTerm);
            this.grpMoreDetail.Controls.Add(this.txtVessel);
            this.grpMoreDetail.Controls.Add(this.lblVessel);
            this.grpMoreDetail.Controls.Add(this.txtPONo);
            this.grpMoreDetail.Controls.Add(this.lblPONo);
            this.grpMoreDetail.Controls.Add(this.txtLCNo);
            this.grpMoreDetail.Controls.Add(this.lblLCNo);
            this.grpMoreDetail.Controls.Add(this.lblLCDate);
            this.grpMoreDetail.Controls.Add(this.txtIssuingBank);
            this.grpMoreDetail.Controls.Add(this.lblIssuingBank);
            this.grpMoreDetail.Controls.Add(this.lblNetWeight);
            this.grpMoreDetail.Controls.Add(this.lblGrossWeight);
            this.grpMoreDetail.Controls.Add(this.lblMeasurement);
            this.grpMoreDetail.Controls.Add(this.txtCNo);
            this.grpMoreDetail.Controls.Add(this.lblCNO);
            this.grpMoreDetail.Controls.Add(this.txtFromPort);
            this.grpMoreDetail.Controls.Add(this.lblFromPort);
            this.grpMoreDetail.Controls.Add(this.txtShippingCode);
            this.grpMoreDetail.Controls.Add(this.lblShippingCode);
            this.grpMoreDetail.Controls.Add(this.txtMeasurement);
            this.grpMoreDetail.Location = new System.Drawing.Point(4, 168);
            this.grpMoreDetail.Name = "grpMoreDetail";
            this.grpMoreDetail.Size = new System.Drawing.Size(1017, 126);
            this.grpMoreDetail.TabIndex = 40;
            this.grpMoreDetail.TabStop = false;
            // 
            // btnSearchBin
            // 
            this.btnSearchBin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearchBin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchBin.Location = new System.Drawing.Point(980, 98);
            this.btnSearchBin.Name = "btnSearchBin";
            this.btnSearchBin.Size = new System.Drawing.Size(24, 20);
            this.btnSearchBin.TabIndex = 35;
            this.btnSearchBin.Text = "...";
            this.btnSearchBin.Click += new System.EventHandler(this.btnSearchBin_Click);
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.AccessibleDescription = "";
            this.txtInvoiceNo.AccessibleName = "";
            this.txtInvoiceNo.Location = new System.Drawing.Point(274, 100);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(100, 20);
            this.txtInvoiceNo.TabIndex = 27;
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AccessibleDescription = "";
            this.lblInvoiceNo.AccessibleName = "";
            this.lblInvoiceNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInvoiceNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblInvoiceNo.Location = new System.Drawing.Point(194, 100);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(84, 20);
            this.lblInvoiceNo.TabIndex = 26;
            this.lblInvoiceNo.Text = "Invoice No";
            this.lblInvoiceNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBin
            // 
            this.txtBin.Location = new System.Drawing.Point(880, 98);
            this.txtBin.Name = "txtBin";
            this.txtBin.Size = new System.Drawing.Size(98, 20);
            this.txtBin.TabIndex = 34;
            this.txtBin.Validating += new System.ComponentModel.CancelEventHandler(this.txtBin_Validating);
            // 
            // txtNetWeight
            // 
            this.txtNetWeight.EmptyAsNull = true;
            this.txtNetWeight.Location = new System.Drawing.Point(494, 34);
            this.txtNetWeight.MaxLength = 15;
            this.txtNetWeight.Name = "txtNetWeight";
            this.txtNetWeight.Size = new System.Drawing.Size(118, 20);
            this.txtNetWeight.TabIndex = 11;
            this.txtNetWeight.Tag = null;
            this.txtNetWeight.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtGrossWeight
            // 
            this.txtGrossWeight.EmptyAsNull = true;
            this.txtGrossWeight.Location = new System.Drawing.Point(274, 34);
            this.txtGrossWeight.MaxLength = 15;
            this.txtGrossWeight.Name = "txtGrossWeight";
            this.txtGrossWeight.Size = new System.Drawing.Size(100, 20);
            this.txtGrossWeight.TabIndex = 9;
            this.txtGrossWeight.Tag = null;
            this.txtGrossWeight.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // lblBin
            // 
            this.lblBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblBin.ForeColor = System.Drawing.Color.Maroon;
            this.lblBin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBin.Location = new System.Drawing.Point(852, 98);
            this.lblBin.Name = "lblBin";
            this.lblBin.Size = new System.Drawing.Size(26, 20);
            this.lblBin.TabIndex = 33;
            this.lblBin.Text = "Bin";
            this.lblBin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtmLCDate
            // 
            this.dtmLCDate.AccessibleDescription = "";
            this.dtmLCDate.AccessibleName = "";
            this.dtmLCDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.dtmLCDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dtmLCDate.Calendar.AccessibleDescription = "";
            this.dtmLCDate.Calendar.AccessibleName = "";
            this.dtmLCDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dtmLCDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmLCDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmLCDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmLCDate.CustomFormat = "dd-MM-yyyy HH:mm:ss";
            this.dtmLCDate.EmptyAsNull = true;
            this.dtmLCDate.ErrorInfo.ShowErrorMessage = false;
            this.dtmLCDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmLCDate.Location = new System.Drawing.Point(494, 78);
            this.dtmLCDate.Name = "dtmLCDate";
            this.dtmLCDate.Size = new System.Drawing.Size(118, 18);
            this.dtmLCDate.TabIndex = 23;
            this.dtmLCDate.Tag = null;
            this.dtmLCDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmLCDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmLCDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmLCDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // dtmOnBoardDate
            // 
            this.dtmOnBoardDate.AccessibleDescription = "";
            this.dtmOnBoardDate.AccessibleName = "";
            this.dtmOnBoardDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.dtmOnBoardDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dtmOnBoardDate.Calendar.AccessibleDescription = "";
            this.dtmOnBoardDate.Calendar.AccessibleName = "";
            this.dtmOnBoardDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dtmOnBoardDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmOnBoardDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmOnBoardDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmOnBoardDate.CustomFormat = "dd-MM-yyyy HH:mm:ss";
            this.dtmOnBoardDate.EmptyAsNull = true;
            this.dtmOnBoardDate.ErrorInfo.ShowErrorMessage = false;
            this.dtmOnBoardDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmOnBoardDate.Location = new System.Drawing.Point(494, 100);
            this.dtmOnBoardDate.Name = "dtmOnBoardDate";
            this.dtmOnBoardDate.Size = new System.Drawing.Size(118, 18);
            this.dtmOnBoardDate.TabIndex = 29;
            this.dtmOnBoardDate.Tag = null;
            this.dtmOnBoardDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmOnBoardDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmOnBoardDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmOnBoardDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblLocation
            // 
            this.lblLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblLocation.ForeColor = System.Drawing.Color.Maroon;
            this.lblLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLocation.Location = new System.Drawing.Point(622, 98);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 20);
            this.lblLocation.TabIndex = 30;
            this.lblLocation.Text = "Location";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOnBoardDate
            // 
            this.lblOnBoardDate.AccessibleDescription = "";
            this.lblOnBoardDate.AccessibleName = "";
            this.lblOnBoardDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOnBoardDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOnBoardDate.Location = new System.Drawing.Point(380, 100);
            this.lblOnBoardDate.Name = "lblOnBoardDate";
            this.lblOnBoardDate.Size = new System.Drawing.Size(112, 20);
            this.lblOnBoardDate.TabIndex = 28;
            this.lblOnBoardDate.Text = "On Board Date, Time";
            this.lblOnBoardDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(683, 98);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(120, 20);
            this.txtLocation.TabIndex = 31;
            this.txtLocation.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocation_Validating);
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.AccessibleDescription = "";
            this.txtReferenceNo.AccessibleName = "";
            this.txtReferenceNo.Location = new System.Drawing.Point(88, 100);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(100, 20);
            this.txtReferenceNo.TabIndex = 25;
            // 
            // btnSearchLocation
            // 
            this.btnSearchLocation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearchLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchLocation.Location = new System.Drawing.Point(805, 98);
            this.btnSearchLocation.Name = "btnSearchLocation";
            this.btnSearchLocation.Size = new System.Drawing.Size(24, 20);
            this.btnSearchLocation.TabIndex = 32;
            this.btnSearchLocation.Text = "...";
            this.btnSearchLocation.Click += new System.EventHandler(this.btnSearchLocation_Click);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AccessibleDescription = "";
            this.lblReferenceNo.AccessibleName = "";
            this.lblReferenceNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblReferenceNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblReferenceNo.Location = new System.Drawing.Point(6, 98);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 20);
            this.lblReferenceNo.TabIndex = 24;
            this.lblReferenceNo.Text = "Reference No";
            this.lblReferenceNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtComment
            // 
            this.txtComment.AccessibleDescription = "";
            this.txtComment.AccessibleName = "";
            this.txtComment.Location = new System.Drawing.Point(274, 78);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(100, 20);
            this.txtComment.TabIndex = 21;
            // 
            // lblComment
            // 
            this.lblComment.AccessibleDescription = "";
            this.lblComment.AccessibleName = "";
            this.lblComment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblComment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblComment.Location = new System.Drawing.Point(194, 78);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(84, 20);
            this.lblComment.TabIndex = 20;
            this.lblComment.Text = "Comment";
            this.lblComment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPaymentTerm
            // 
            this.txtPaymentTerm.AccessibleDescription = "";
            this.txtPaymentTerm.AccessibleName = "";
            this.txtPaymentTerm.Location = new System.Drawing.Point(274, 56);
            this.txtPaymentTerm.Name = "txtPaymentTerm";
            this.txtPaymentTerm.ReadOnly = true;
            this.txtPaymentTerm.Size = new System.Drawing.Size(100, 20);
            this.txtPaymentTerm.TabIndex = 15;
            // 
            // lblPaymentTerm
            // 
            this.lblPaymentTerm.AccessibleDescription = "";
            this.lblPaymentTerm.AccessibleName = "";
            this.lblPaymentTerm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPaymentTerm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPaymentTerm.Location = new System.Drawing.Point(194, 56);
            this.lblPaymentTerm.Name = "lblPaymentTerm";
            this.lblPaymentTerm.Size = new System.Drawing.Size(84, 20);
            this.lblPaymentTerm.TabIndex = 14;
            this.lblPaymentTerm.Text = "Payment Term";
            this.lblPaymentTerm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVessel
            // 
            this.txtVessel.AccessibleDescription = "";
            this.txtVessel.AccessibleName = "";
            this.txtVessel.Location = new System.Drawing.Point(88, 78);
            this.txtVessel.Name = "txtVessel";
            this.txtVessel.Size = new System.Drawing.Size(100, 20);
            this.txtVessel.TabIndex = 19;
            // 
            // lblVessel
            // 
            this.lblVessel.AccessibleDescription = "";
            this.lblVessel.AccessibleName = "";
            this.lblVessel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVessel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVessel.Location = new System.Drawing.Point(6, 78);
            this.lblVessel.Name = "lblVessel";
            this.lblVessel.Size = new System.Drawing.Size(84, 20);
            this.lblVessel.TabIndex = 18;
            this.lblVessel.Text = "Vessel";
            this.lblVessel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLCNo
            // 
            this.txtLCNo.AccessibleDescription = "";
            this.txtLCNo.AccessibleName = "";
            this.txtLCNo.Location = new System.Drawing.Point(494, 56);
            this.txtLCNo.Name = "txtLCNo";
            this.txtLCNo.Size = new System.Drawing.Size(118, 20);
            this.txtLCNo.TabIndex = 17;
            // 
            // lblLCNo
            // 
            this.lblLCNo.AccessibleDescription = "";
            this.lblLCNo.AccessibleName = "";
            this.lblLCNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLCNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLCNo.Location = new System.Drawing.Point(380, 56);
            this.lblLCNo.Name = "lblLCNo";
            this.lblLCNo.Size = new System.Drawing.Size(84, 20);
            this.lblLCNo.TabIndex = 16;
            this.lblLCNo.Text = "L/C No";
            this.lblLCNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLCDate
            // 
            this.lblLCDate.AccessibleDescription = "";
            this.lblLCDate.AccessibleName = "";
            this.lblLCDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLCDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLCDate.Location = new System.Drawing.Point(380, 78);
            this.lblLCDate.Name = "lblLCDate";
            this.lblLCDate.Size = new System.Drawing.Size(84, 20);
            this.lblLCDate.TabIndex = 22;
            this.lblLCDate.Text = "L/C Date";
            this.lblLCDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIssuingBank
            // 
            this.txtIssuingBank.AccessibleDescription = "";
            this.txtIssuingBank.AccessibleName = "";
            this.txtIssuingBank.Location = new System.Drawing.Point(88, 56);
            this.txtIssuingBank.Name = "txtIssuingBank";
            this.txtIssuingBank.Size = new System.Drawing.Size(100, 20);
            this.txtIssuingBank.TabIndex = 13;
            // 
            // lblIssuingBank
            // 
            this.lblIssuingBank.AccessibleDescription = "";
            this.lblIssuingBank.AccessibleName = "";
            this.lblIssuingBank.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIssuingBank.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblIssuingBank.Location = new System.Drawing.Point(6, 56);
            this.lblIssuingBank.Name = "lblIssuingBank";
            this.lblIssuingBank.Size = new System.Drawing.Size(84, 20);
            this.lblIssuingBank.TabIndex = 12;
            this.lblIssuingBank.Text = "Issuing Bank";
            this.lblIssuingBank.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNetWeight
            // 
            this.lblNetWeight.AccessibleDescription = "";
            this.lblNetWeight.AccessibleName = "";
            this.lblNetWeight.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNetWeight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNetWeight.Location = new System.Drawing.Point(380, 34);
            this.lblNetWeight.Name = "lblNetWeight";
            this.lblNetWeight.Size = new System.Drawing.Size(84, 20);
            this.lblNetWeight.TabIndex = 10;
            this.lblNetWeight.Text = "Net Weight";
            this.lblNetWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGrossWeight
            // 
            this.lblGrossWeight.AccessibleDescription = "";
            this.lblGrossWeight.AccessibleName = "";
            this.lblGrossWeight.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblGrossWeight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblGrossWeight.Location = new System.Drawing.Point(194, 34);
            this.lblGrossWeight.Name = "lblGrossWeight";
            this.lblGrossWeight.Size = new System.Drawing.Size(84, 20);
            this.lblGrossWeight.TabIndex = 8;
            this.lblGrossWeight.Text = "Gross Weight";
            this.lblGrossWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMeasurement
            // 
            this.lblMeasurement.AccessibleDescription = "";
            this.lblMeasurement.AccessibleName = "";
            this.lblMeasurement.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMeasurement.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMeasurement.Location = new System.Drawing.Point(6, 34);
            this.lblMeasurement.Name = "lblMeasurement";
            this.lblMeasurement.Size = new System.Drawing.Size(76, 20);
            this.lblMeasurement.TabIndex = 6;
            this.lblMeasurement.Text = "Measurement";
            this.lblMeasurement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCNo
            // 
            this.txtCNo.AccessibleDescription = "";
            this.txtCNo.AccessibleName = "";
            this.txtCNo.Location = new System.Drawing.Point(494, 12);
            this.txtCNo.Name = "txtCNo";
            this.txtCNo.Size = new System.Drawing.Size(118, 20);
            this.txtCNo.TabIndex = 5;
            // 
            // lblCNO
            // 
            this.lblCNO.AccessibleDescription = "";
            this.lblCNO.AccessibleName = "";
            this.lblCNO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCNO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCNO.Location = new System.Drawing.Point(380, 12);
            this.lblCNO.Name = "lblCNO";
            this.lblCNO.Size = new System.Drawing.Size(84, 20);
            this.lblCNO.TabIndex = 4;
            this.lblCNO.Text = "C/No";
            this.lblCNO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFromPort
            // 
            this.txtFromPort.AccessibleDescription = "";
            this.txtFromPort.AccessibleName = "";
            this.txtFromPort.Location = new System.Drawing.Point(274, 12);
            this.txtFromPort.Name = "txtFromPort";
            this.txtFromPort.Size = new System.Drawing.Size(100, 20);
            this.txtFromPort.TabIndex = 3;
            // 
            // lblFromPort
            // 
            this.lblFromPort.AccessibleDescription = "";
            this.lblFromPort.AccessibleName = "";
            this.lblFromPort.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFromPort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFromPort.Location = new System.Drawing.Point(194, 12);
            this.lblFromPort.Name = "lblFromPort";
            this.lblFromPort.Size = new System.Drawing.Size(84, 20);
            this.lblFromPort.TabIndex = 2;
            this.lblFromPort.Text = "From Port";
            this.lblFromPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtShippingCode
            // 
            this.txtShippingCode.AccessibleDescription = "";
            this.txtShippingCode.AccessibleName = "";
            this.txtShippingCode.Location = new System.Drawing.Point(88, 12);
            this.txtShippingCode.Name = "txtShippingCode";
            this.txtShippingCode.Size = new System.Drawing.Size(100, 20);
            this.txtShippingCode.TabIndex = 1;
            // 
            // lblShippingCode
            // 
            this.lblShippingCode.AccessibleDescription = "";
            this.lblShippingCode.AccessibleName = "";
            this.lblShippingCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblShippingCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblShippingCode.Location = new System.Drawing.Point(6, 12);
            this.lblShippingCode.Name = "lblShippingCode";
            this.lblShippingCode.Size = new System.Drawing.Size(84, 20);
            this.lblShippingCode.TabIndex = 0;
            this.lblShippingCode.Text = "Shipping Code";
            this.lblShippingCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMeasurement
            // 
            this.txtMeasurement.EmptyAsNull = true;
            this.txtMeasurement.Location = new System.Drawing.Point(88, 34);
            this.txtMeasurement.MaxLength = 15;
            this.txtMeasurement.Name = "txtMeasurement";
            this.txtMeasurement.Size = new System.Drawing.Size(100, 20);
            this.txtMeasurement.TabIndex = 7;
            this.txtMeasurement.Tag = null;
            this.txtMeasurement.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // btnModify
            // 
            this.btnModify.AccessibleDescription = "";
            this.btnModify.AccessibleName = "";
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnModify.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnModify.Location = new System.Drawing.Point(136, 509);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(65, 24);
            this.btnModify.TabIndex = 44;
            this.btnModify.Text = "&Modify";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnPrintConfiguration
            // 
            this.btnPrintConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrintConfiguration.Font = new System.Drawing.Font("Wingdings 3", 6F);
            this.btnPrintConfiguration.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrintConfiguration.Location = new System.Drawing.Point(378, 509);
            this.btnPrintConfiguration.Name = "btnPrintConfiguration";
            this.btnPrintConfiguration.Size = new System.Drawing.Size(18, 24);
            this.btnPrintConfiguration.TabIndex = 47;
            this.btnPrintConfiguration.Text = "q";
            // 
            // btnAttachedSheet
            // 
            this.btnAttachedSheet.AccessibleDescription = "";
            this.btnAttachedSheet.AccessibleName = "";
            this.btnAttachedSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAttachedSheet.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAttachedSheet.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAttachedSheet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAttachedSheet.Location = new System.Drawing.Point(211, 509);
            this.btnAttachedSheet.Name = "btnAttachedSheet";
            this.btnAttachedSheet.Size = new System.Drawing.Size(100, 24);
            this.btnAttachedSheet.TabIndex = 45;
            this.btnAttachedSheet.Text = "A&ttached Sheet";
            this.btnAttachedSheet.Click += new System.EventHandler(this.btnAttachedSheet_Click);
            // 
            // lblVATInInvoice
            // 
            this.lblVATInInvoice.Location = new System.Drawing.Point(386, 101);
            this.lblVATInInvoice.Name = "lblVATInInvoice";
            this.lblVATInInvoice.Size = new System.Drawing.Size(158, 14);
            this.lblVATInInvoice.TabIndex = 34;
            this.lblVATInInvoice.Text = "VAT values in the Invoice";
            this.lblVATInInvoice.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(340, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(4, 22);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AccessibleDescription = "";
            this.lblInvoiceDate.AccessibleName = "";
            this.lblInvoiceDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblInvoiceDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblInvoiceDate.Location = new System.Drawing.Point(4, 120);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(104, 20);
            this.lblInvoiceDate.TabIndex = 27;
            this.lblInvoiceDate.Text = "Invoice Date";
            this.lblInvoiceDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtmInvoiceDate
            // 
            this.dtmInvoiceDate.AccessibleDescription = "";
            this.dtmInvoiceDate.AccessibleName = "";
            this.dtmInvoiceDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.dtmInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dtmInvoiceDate.Calendar.AccessibleDescription = "";
            this.dtmInvoiceDate.Calendar.AccessibleName = "";
            this.dtmInvoiceDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dtmInvoiceDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmInvoiceDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmInvoiceDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmInvoiceDate.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dtmInvoiceDate.EmptyAsNull = true;
            this.dtmInvoiceDate.ErrorInfo.ShowErrorMessage = false;
            this.dtmInvoiceDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmInvoiceDate.Location = new System.Drawing.Point(108, 120);
            this.dtmInvoiceDate.Name = "dtmInvoiceDate";
            this.dtmInvoiceDate.Size = new System.Drawing.Size(118, 18);
            this.dtmInvoiceDate.TabIndex = 28;
            this.dtmInvoiceDate.Tag = null;
            this.dtmInvoiceDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmInvoiceDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmInvoiceDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmInvoiceDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AccessibleDescription = "";
            this.lblFromDate.AccessibleName = "";
            this.lblFromDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblFromDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFromDate.Location = new System.Drawing.Point(4, 146);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(90, 20);
            this.lblFromDate.TabIndex = 35;
            this.lblFromDate.Text = "From Date";
            this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtmFromDate
            // 
            this.dtmFromDate.AccessibleDescription = "";
            this.dtmFromDate.AccessibleName = "";
            this.dtmFromDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.dtmFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dtmFromDate.Calendar.AccessibleDescription = "";
            this.dtmFromDate.Calendar.AccessibleName = "";
            this.dtmFromDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dtmFromDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmFromDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmFromDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmFromDate.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dtmFromDate.EmptyAsNull = true;
            this.dtmFromDate.ErrorInfo.ShowErrorMessage = false;
            this.dtmFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmFromDate.Location = new System.Drawing.Point(108, 146);
            this.dtmFromDate.Name = "dtmFromDate";
            this.dtmFromDate.Size = new System.Drawing.Size(118, 18);
            this.dtmFromDate.TabIndex = 36;
            this.dtmFromDate.Tag = null;
            this.dtmFromDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmFromDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmFromDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblToDate
            // 
            this.lblToDate.AccessibleDescription = "";
            this.lblToDate.AccessibleName = "";
            this.lblToDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblToDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblToDate.Location = new System.Drawing.Point(228, 146);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(50, 20);
            this.lblToDate.TabIndex = 37;
            this.lblToDate.Text = "To Date";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtmToDate
            // 
            this.dtmToDate.AccessibleDescription = "";
            this.dtmToDate.AccessibleName = "";
            this.dtmToDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.dtmToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.dtmToDate.Calendar.AccessibleDescription = "";
            this.dtmToDate.Calendar.AccessibleName = "";
            this.dtmToDate.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dtmToDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmToDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmToDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmToDate.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dtmToDate.EmptyAsNull = true;
            this.dtmToDate.ErrorInfo.ShowErrorMessage = false;
            this.dtmToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmToDate.Location = new System.Drawing.Point(280, 146);
            this.dtmToDate.Name = "dtmToDate";
            this.dtmToDate.Size = new System.Drawing.Size(118, 18);
            this.dtmToDate.TabIndex = 38;
            this.dtmToDate.Tag = null;
            this.dtmToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmToDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmToDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblPurpose
            // 
            this.lblPurpose.AccessibleDescription = "";
            this.lblPurpose.AccessibleName = "";
            this.lblPurpose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPurpose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPurpose.ForeColor = System.Drawing.Color.Maroon;
            this.lblPurpose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPurpose.Location = new System.Drawing.Point(888, 27);
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.Size = new System.Drawing.Size(45, 20);
            this.lblPurpose.TabIndex = 0;
            this.lblPurpose.Text = "Purpose";
            this.lblPurpose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboPurpose
            // 
            this.cboPurpose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPurpose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPurpose.Items.AddRange(new object[] {
            "Print Invoice",
            "Shipping"});
            this.cboPurpose.Location = new System.Drawing.Point(933, 28);
            this.cboPurpose.Name = "cboPurpose";
            this.cboPurpose.Size = new System.Drawing.Size(90, 21);
            this.cboPurpose.TabIndex = 1;
            this.cboPurpose.SelectedIndexChanged += new System.EventHandler(this.cboPurpose_SelectedIndexChanged);
            // 
            // lblPONo
            // 
            this.lblPONo.AccessibleDescription = "";
            this.lblPONo.AccessibleName = "";
            this.lblPONo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPONo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPONo.Location = new System.Drawing.Point(627, 56);
            this.lblPONo.Name = "lblPONo";
            this.lblPONo.Size = new System.Drawing.Size(49, 20);
            this.lblPONo.TabIndex = 16;
            this.lblPONo.Text = "PO No.";
            this.lblPONo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPONo
            // 
            this.txtPONo.AccessibleDescription = "";
            this.txtPONo.AccessibleName = "";
            this.txtPONo.Location = new System.Drawing.Point(683, 56);
            this.txtPONo.Name = "txtPONo";
            this.txtPONo.Size = new System.Drawing.Size(120, 20);
            this.txtPONo.TabIndex = 17;
            // 
            // ConfirmShipManagement
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1028, 539);
            this.Controls.Add(this.cboPurpose);
            this.Controls.Add(this.lblPurpose);
            this.Controls.Add(this.lblFromDate);
            this.Controls.Add(this.dtmFromDate);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.dtmToDate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAttachedSheet);
            this.Controls.Add(this.btnPrintConfiguration);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.grpMoreDetail);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtGate);
            this.Controls.Add(this.txtCurrency);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.txtSaleType);
            this.Controls.Add(this.txtCustomerCode);
            this.Controls.Add(this.txtSalesOrder);
            this.Controls.Add(this.txtConfirmShipNo);
            this.Controls.Add(this.txtMasLoc);
            this.Controls.Add(this.dgrdData);
            this.Controls.Add(this.btnGate);
            this.Controls.Add(this.lblGate);
            this.Controls.Add(this.chkHaveGate);
            this.Controls.Add(this.btnSaleType);
            this.Controls.Add(this.btnCurrency);
            this.Controls.Add(this.txtExchRate);
            this.Controls.Add(this.lblExchRate);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.lblCustomerName);
            this.Controls.Add(this.lblSaleType);
            this.Controls.Add(this.lblCustomerCode);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSO);
            this.Controls.Add(this.lblSO);
            this.Controls.Add(this.btnShipNo);
            this.Controls.Add(this.lblShipNo);
            this.Controls.Add(this.dtmShipmentDate);
            this.Controls.Add(this.btnSearchMasLoc);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnConfirmShippment);
            this.Controls.Add(this.lblShippedDate);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.lblMasLoc);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.lblVATInInvoice);
            this.Controls.Add(this.lblInvoiceDate);
            this.Controls.Add(this.dtmInvoiceDate);
            this.Name = "ConfirmShipManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shipping Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ConfirmShipManagement_Closing);
            this.Load += new System.EventHandler(this.ConfirmShipManagerment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtmShipmentDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchRate)).EndInit();
            this.grpMoreDetail.ResumeLayout(false);
            this.grpMoreDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGrossWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmLCDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmOnBoardDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeasurement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmInvoiceDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtBin;
        private TextBox txtCNo;
        private TextBox txtComment;
        private TextBox txtConfirmShipNo;
        private TextBox txtCurrency;
        private TextBox txtCustomerCode;
        private TextBox txtCustomerName;
        private C1NumericEdit txtExchRate;
        private TextBox txtFromPort;
        private TextBox txtGate;
        private C1NumericEdit txtGrossWeight;
        private TextBox txtInvoiceNo;
        private TextBox txtIssuingBank;
        private TextBox txtLCNo;
        private TextBox txtLocation;
        private TextBox txtMasLoc;
        private C1NumericEdit txtMeasurement;
        private C1NumericEdit txtNetWeight;
        private TextBox txtPaymentTerm;
        private TextBox txtReferenceNo;
        private TextBox txtSaleType;
        private TextBox txtSalesOrder;
        private TextBox txtShippingCode;
        private TextBox txtVessel;
        private Label lblBin;
        private Label lblCCN;
        private Label lblCNO;
        private Label lblComment;
        private Label lblCurrency;
        private Label lblCustomerCode;
        private Label lblCustomerName;
        private Label lblExchRate;
        private Label lblFromDate;
        private Label lblFromPort;
        private Label lblGate;
        private Label lblGrossWeight;
        private Label lblInvoiceDate;
        private Label lblInvoiceNo;
        private Label lblIssuingBank;
        private Label lblLCDate;
        private Label lblLCNo;
        private Label lblLocation;
        private Label lblMasLoc;
        private Label lblMeasurement;
        private Label lblNetWeight;
        private Label lblOnBoardDate;
        private Label lblPaymentTerm;
        private Label lblPurpose;
        private Label lblReferenceNo;
        private Label lblSO;
        private Label lblSaleType;
        private Label lblShipNo;
        private Label lblShippedDate;
        private Label lblShippingCode;
        private Label lblToDate;
        private Label lblVATInInvoice;
        private Label lblVessel;
        private Button btnAdd;
        private Button btnAttachedSheet;

        private Button btnClose;
        private Button btnConfirmShippment;
        private Button btnCurrency;
        private Button btnGate;
        private Button btnHelp;
        private Button btnModify;
        private Button btnPrint;
        private Button btnPrintConfiguration;
        private Button btnSO;
        private Button btnSaleType;
        private Button btnSearch;
        private Button btnSearchBin;
        private Button btnSearchLocation;
        private Button btnSearchMasLoc;
        private Button btnShipNo;
        private C1Combo cboCCN;
        private ComboBox cboPurpose;
        private CheckBox chkHaveGate;
        private C1TrueDBGrid dgrdData;
        private DataSet dstData;
        private DataTable dtbGridDesign;
        private C1DateEdit dtmFromDate;
        private C1DateEdit dtmInvoiceDate;
        private C1DateEdit dtmLCDate;
        private C1DateEdit dtmOnBoardDate;
        private C1DateEdit dtmShipmentDate;
        private C1DateEdit dtmToDate;
        private GroupBox groupBox1;
        private GroupBox grpMoreDetail;
        private TextBox txtPONo;
        private Label lblPONo;
    }
}
