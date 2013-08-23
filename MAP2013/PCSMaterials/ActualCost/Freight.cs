using System;
using System.Data;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using PCSComMaterials.ActualCost.BO;
using PCSComMaterials.ActualCost.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.DataAccess;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.PCSExc;

using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSMaterials.ActualCost
{
	/// <summary>
	/// Summary description for TransportationFee.
	/// </summary>
	public class Freight : System.Windows.Forms.Form
	{
		#region Windows Control
		private System.Windows.Forms.TextBox txtCurrency;
		private System.Windows.Forms.TextBox txtOrderNo;
		private C1.Win.C1Input.C1NumericEdit txtExchRate;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnOrderNo;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblExchRate;
		private System.Windows.Forms.Label lblCurrency;
		private System.Windows.Forms.Button btnCurrency;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnAdd;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.TextBox txtTransporterCode;
		private System.Windows.Forms.TextBox txtTransporterName;
		private System.Windows.Forms.Label lblTransporter;
		private System.Windows.Forms.Label lblTransNo;
		private System.Windows.Forms.Label lblVendor;
		private System.Windows.Forms.Label lblPostDate;
		private System.Windows.Forms.TextBox txtVendorCode;
		private System.Windows.Forms.TextBox txtVendorName;
		private System.Windows.Forms.Label lblNetAmount;
		private System.Windows.Forms.TextBox txtNote;
		private System.Windows.Forms.Label lblNote;
		private C1.Win.C1Input.C1NumericEdit txtVAT;
		private System.Windows.Forms.Label lblInvoiceNo;
		private System.Windows.Forms.TextBox txtInvoiceNo;
		private System.Windows.Forms.Button btnInvoiceNo;
		private C1.Win.C1Input.C1NumericEdit txtTotalVAT;
		private System.Windows.Forms.Label lblTotalVAT;
		private System.Windows.Forms.Label lblVAT;
		private C1.Win.C1Input.C1NumericEdit txtGrandTotal;
		private System.Windows.Forms.Label lblGrandToTal;
		private C1.Win.C1Input.C1NumericEdit txtSubTotal;
		private System.Windows.Forms.Label lblSubTotal;
		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		
		private System.ComponentModel.Container components = null;
		#region My Variables
		public const string THIS = "PCSMaterials.ActualCost.Freight";
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private DataTable dtbGridLayOut;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnEdit;
		private EnumAction mFormMode = EnumAction.Default;
		private int intPurpose = 0;// 0 = default; 1 = Freight; 2 = Import Tax; 3 = CreditNote; 4 = DebitNote
		private int intObject = 0; //
		private const string AMOUNT_COLUMN = "Amount";
		private const string TOTAL_AMOUNT_COLUMN = "TotalAmount";
		private const string PONO = "PONo";
		private C1.Win.C1Input.C1DateEdit dtmPostDate;
		private const string VAT_AMOUNT_COLUMN = "VATAmount";
		private System.Windows.Forms.Button btnTransporterName;
		private System.Windows.Forms.Button btnTransporterCode;
		private System.Windows.Forms.Button btnVendorCode;
		private System.Windows.Forms.Button btnVendorName;
		private const string VENDOR = "Vendor";
		private const string V_VENDORCUSTOMER = "V_VendorCustomer";
		private C1.Win.C1Input.C1NumericEdit txtNetAmount;
		DataSet dstGridData = new DataSet();
		cst_FreightMasterVO voFreightMaster = new cst_FreightMasterVO();
		bool blnHasError = false;
		private int intFreightMasterID = 0;
		private const string V_PRODUCTFORFREIGHT = "V_ProductForFreight";
		private const string VENDORCODE = "VendorCode";
		private const string MAKER_CODE = "MakerCode";
		private const string VENDORNAME = "VendorName";
		private const string TRANSPORTERCODE = "TransporterCode";
		private const string TRANSPORTERNAME = "TransporterName";
		private System.Windows.Forms.Button btnAllocate;
		private C1.C1Report.C1Report rptFreightSlip;
		private System.Windows.Forms.Label lblPurpose;
		private System.Windows.Forms.Label lblObject;
		private System.Windows.Forms.TextBox txtMaker;
		private System.Windows.Forms.TextBox txtMakerName;
		private System.Windows.Forms.Button btnMaker;
		private System.Windows.Forms.Button btnMakerName;
		private System.Windows.Forms.Label lblMaker;
		private System.Windows.Forms.TextBox txtPurpose;
		private System.Windows.Forms.Button btnPurpose;
		private System.Windows.Forms.TextBox txtObject;
		private System.Windows.Forms.Button btnObject;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPONo;
		private System.Windows.Forms.TextBox txtInvoice;
		private const string V_RECEIPTBYVENDOR = "V_ReceiptByVendor";
		private const string V_INVOICE = "V_Invoice";
		C1.Win.C1Input.ValueInterval[] objInterval1 = new ValueInterval[]{
																		new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] 
																																		{
																																			0,
																																			0,
																																			0,
																																			0}), new System.Decimal(new int[] 

																																											{
																																												276447231,
																																												23283,
																																												0,
																																												0}), false, true)};
		C1.Win.C1Input.ValueInterval[] objInterval2 = new ValueInterval[]{
																			 new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																1215752191,
																																23,
																																0,
																																-2147483648}), new System.Decimal(new int[] {
																																												276447231,
																																												23283,
																																												0,
																																												0}), false, true)};
		#endregion
		public Freight()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Freight));
			this.txtCurrency = new System.Windows.Forms.TextBox();
			this.txtOrderNo = new System.Windows.Forms.TextBox();
			this.txtTransporterCode = new System.Windows.Forms.TextBox();
			this.txtTransporterName = new System.Windows.Forms.TextBox();
			this.txtExchRate = new C1.Win.C1Input.C1NumericEdit();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnTransporterName = new System.Windows.Forms.Button();
			this.btnOrderNo = new System.Windows.Forms.Button();
			this.btnTransporterCode = new System.Windows.Forms.Button();
			this.lblCCN = new System.Windows.Forms.Label();
			this.lblExchRate = new System.Windows.Forms.Label();
			this.lblCurrency = new System.Windows.Forms.Label();
			this.lblTransporter = new System.Windows.Forms.Label();
			this.lblTransNo = new System.Windows.Forms.Label();
			this.lblVendor = new System.Windows.Forms.Label();
			this.lblPostDate = new System.Windows.Forms.Label();
			this.txtVendorCode = new System.Windows.Forms.TextBox();
			this.txtVendorName = new System.Windows.Forms.TextBox();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnVendorCode = new System.Windows.Forms.Button();
			this.btnVendorName = new System.Windows.Forms.Button();
			this.btnCurrency = new System.Windows.Forms.Button();
			this.dtmPostDate = new C1.Win.C1Input.C1DateEdit();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblNetAmount = new System.Windows.Forms.Label();
			this.txtNote = new System.Windows.Forms.TextBox();
			this.lblNote = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.txtVAT = new C1.Win.C1Input.C1NumericEdit();
			this.lblVAT = new System.Windows.Forms.Label();
			this.txtGrandTotal = new C1.Win.C1Input.C1NumericEdit();
			this.lblGrandToTal = new System.Windows.Forms.Label();
			this.txtSubTotal = new C1.Win.C1Input.C1NumericEdit();
			this.lblSubTotal = new System.Windows.Forms.Label();
			this.txtTotalVAT = new C1.Win.C1Input.C1NumericEdit();
			this.lblTotalVAT = new System.Windows.Forms.Label();
			this.lblInvoiceNo = new System.Windows.Forms.Label();
			this.txtInvoiceNo = new System.Windows.Forms.TextBox();
			this.btnInvoiceNo = new System.Windows.Forms.Button();
			this.txtNetAmount = new C1.Win.C1Input.C1NumericEdit();
			this.btnAllocate = new System.Windows.Forms.Button();
			this.rptFreightSlip = new C1.C1Report.C1Report();
			this.lblPurpose = new System.Windows.Forms.Label();
			this.lblObject = new System.Windows.Forms.Label();
			this.txtMaker = new System.Windows.Forms.TextBox();
			this.txtMakerName = new System.Windows.Forms.TextBox();
			this.btnMaker = new System.Windows.Forms.Button();
			this.btnMakerName = new System.Windows.Forms.Button();
			this.lblMaker = new System.Windows.Forms.Label();
			this.txtPurpose = new System.Windows.Forms.TextBox();
			this.btnPurpose = new System.Windows.Forms.Button();
			this.txtObject = new System.Windows.Forms.TextBox();
			this.btnObject = new System.Windows.Forms.Button();
			this.txtPONo = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtInvoice = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.txtExchRate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmPostDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtVAT)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtGrandTotal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSubTotal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalVAT)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNetAmount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rptFreightSlip)).BeginInit();
			this.SuspendLayout();
			// 
			// txtCurrency
			// 
			this.txtCurrency.AccessibleDescription = "";
			this.txtCurrency.AccessibleName = "";
			this.txtCurrency.Location = new System.Drawing.Point(328, 4);
			this.txtCurrency.Name = "txtCurrency";
			this.txtCurrency.Size = new System.Drawing.Size(72, 20);
			this.txtCurrency.TabIndex = 5;
			this.txtCurrency.Text = "";
			this.txtCurrency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrency_KeyDown);
			this.txtCurrency.Validating += new System.ComponentModel.CancelEventHandler(this.txtCurrency_Validating);
			// 
			// txtOrderNo
			// 
			this.txtOrderNo.AccessibleDescription = "";
			this.txtOrderNo.AccessibleName = "";
			this.txtOrderNo.Location = new System.Drawing.Point(72, 26);
			this.txtOrderNo.MaxLength = 20;
			this.txtOrderNo.Name = "txtOrderNo";
			this.txtOrderNo.Size = new System.Drawing.Size(96, 20);
			this.txtOrderNo.TabIndex = 8;
			this.txtOrderNo.Text = "";
			this.txtOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderNo_KeyDown);
			this.txtOrderNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtOrderNo_Validating);
			// 
			// txtTransporterCode
			// 
			this.txtTransporterCode.AccessibleDescription = "";
			this.txtTransporterCode.AccessibleName = "";
			this.txtTransporterCode.Location = new System.Drawing.Point(72, 96);
			this.txtTransporterCode.MaxLength = 20;
			this.txtTransporterCode.Name = "txtTransporterCode";
			this.txtTransporterCode.Size = new System.Drawing.Size(96, 20);
			this.txtTransporterCode.TabIndex = 19;
			this.txtTransporterCode.Text = "";
			this.txtTransporterCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTransporterCode_KeyDown);
			this.txtTransporterCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtTransporterCode_Validating);
			// 
			// txtTransporterName
			// 
			this.txtTransporterName.AccessibleDescription = "";
			this.txtTransporterName.AccessibleName = "";
			this.txtTransporterName.Location = new System.Drawing.Point(196, 96);
			this.txtTransporterName.MaxLength = 200;
			this.txtTransporterName.Name = "txtTransporterName";
			this.txtTransporterName.Size = new System.Drawing.Size(204, 20);
			this.txtTransporterName.TabIndex = 21;
			this.txtTransporterName.Text = "";
			this.txtTransporterName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTransporterName_KeyDown);
			this.txtTransporterName.Validating += new System.ComponentModel.CancelEventHandler(this.txtTransporterName_Validating);
			// 
			// txtExchRate
			// 
			this.txtExchRate.AccessibleDescription = "";
			this.txtExchRate.AccessibleName = "";
			// 
			// txtExchRate.Calculator
			// 
			this.txtExchRate.Calculator.AccessibleDescription = "";
			this.txtExchRate.Calculator.AccessibleName = "";
			this.txtExchRate.CustomFormat = "###############,0.00";
			this.txtExchRate.EmptyAsNull = true;
			this.txtExchRate.ErrorInfo.ShowErrorMessage = false;
			this.txtExchRate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtExchRate.Location = new System.Drawing.Point(328, 26);
			this.txtExchRate.Name = "txtExchRate";
			this.txtExchRate.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									  new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																										0,
																																										0,
																																										0,
																																										0}), new System.Decimal(new int[] {
																																																			  -1530494977,
																																																			  232830,
																																																			  0,
																																																			  0}), false, false)});
			this.txtExchRate.Size = new System.Drawing.Size(72, 20);
			this.txtExchRate.TabIndex = 11;
			this.txtExchRate.Tag = null;
			this.txtExchRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtExchRate.Value = new System.Decimal(new int[] {
																	  0,
																	  0,
																	  0,
																	  0});
			this.txtExchRate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// btnPrint
			// 
			this.btnPrint.AccessibleDescription = "";
			this.btnPrint.AccessibleName = "";
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPrint.Location = new System.Drawing.Point(128, 481);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(60, 23);
			this.btnPrint.TabIndex = 57;
			this.btnPrint.Text = "&Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnTransporterName
			// 
			this.btnTransporterName.AccessibleDescription = "";
			this.btnTransporterName.AccessibleName = "";
			this.btnTransporterName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnTransporterName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnTransporterName.Location = new System.Drawing.Point(401, 96);
			this.btnTransporterName.Name = "btnTransporterName";
			this.btnTransporterName.Size = new System.Drawing.Size(24, 20);
			this.btnTransporterName.TabIndex = 22;
			this.btnTransporterName.Text = "...";
			this.btnTransporterName.Click += new System.EventHandler(this.btnTransporterName_Click);
			// 
			// btnOrderNo
			// 
			this.btnOrderNo.AccessibleDescription = "";
			this.btnOrderNo.AccessibleName = "";
			this.btnOrderNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOrderNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnOrderNo.Location = new System.Drawing.Point(170, 26);
			this.btnOrderNo.Name = "btnOrderNo";
			this.btnOrderNo.Size = new System.Drawing.Size(24, 20);
			this.btnOrderNo.TabIndex = 9;
			this.btnOrderNo.Text = "...";
			this.btnOrderNo.Click += new System.EventHandler(this.btnOrderNo_Click);
			// 
			// btnTransporterCode
			// 
			this.btnTransporterCode.AccessibleDescription = "";
			this.btnTransporterCode.AccessibleName = "";
			this.btnTransporterCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnTransporterCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnTransporterCode.Location = new System.Drawing.Point(170, 96);
			this.btnTransporterCode.Name = "btnTransporterCode";
			this.btnTransporterCode.Size = new System.Drawing.Size(24, 20);
			this.btnTransporterCode.TabIndex = 20;
			this.btnTransporterCode.Text = "...";
			this.btnTransporterCode.Click += new System.EventHandler(this.btnTransporterCode_Click);
			// 
			// lblCCN
			// 
			this.lblCCN.AccessibleDescription = "";
			this.lblCCN.AccessibleName = "";
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(512, 4);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(30, 20);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblExchRate
			// 
			this.lblExchRate.AccessibleDescription = "";
			this.lblExchRate.AccessibleName = "";
			this.lblExchRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblExchRate.ForeColor = System.Drawing.Color.Maroon;
			this.lblExchRate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblExchRate.Location = new System.Drawing.Point(268, 26);
			this.lblExchRate.Name = "lblExchRate";
			this.lblExchRate.Size = new System.Drawing.Size(60, 20);
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
			this.lblCurrency.Location = new System.Drawing.Point(268, 4);
			this.lblCurrency.Name = "lblCurrency";
			this.lblCurrency.Size = new System.Drawing.Size(60, 18);
			this.lblCurrency.TabIndex = 4;
			this.lblCurrency.Text = "Currency";
			this.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTransporter
			// 
			this.lblTransporter.AccessibleDescription = "";
			this.lblTransporter.AccessibleName = "";
			this.lblTransporter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblTransporter.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTransporter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblTransporter.Location = new System.Drawing.Point(4, 96);
			this.lblTransporter.Name = "lblTransporter";
			this.lblTransporter.Size = new System.Drawing.Size(73, 20);
			this.lblTransporter.TabIndex = 18;
			this.lblTransporter.Text = "Transporter";
			this.lblTransporter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTransNo
			// 
			this.lblTransNo.AccessibleDescription = "";
			this.lblTransNo.AccessibleName = "";
			this.lblTransNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblTransNo.ForeColor = System.Drawing.Color.Maroon;
			this.lblTransNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblTransNo.Location = new System.Drawing.Point(4, 26);
			this.lblTransNo.Name = "lblTransNo";
			this.lblTransNo.Size = new System.Drawing.Size(73, 20);
			this.lblTransNo.TabIndex = 7;
			this.lblTransNo.Text = "Trans. No.";
			this.lblTransNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblVendor
			// 
			this.lblVendor.AccessibleDescription = "";
			this.lblVendor.AccessibleName = "";
			this.lblVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblVendor.ForeColor = System.Drawing.Color.Black;
			this.lblVendor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblVendor.Location = new System.Drawing.Point(4, 118);
			this.lblVendor.Name = "lblVendor";
			this.lblVendor.Size = new System.Drawing.Size(73, 20);
			this.lblVendor.TabIndex = 23;
			this.lblVendor.Text = "Vendor";
			this.lblVendor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPostDate
			// 
			this.lblPostDate.AccessibleDescription = "";
			this.lblPostDate.AccessibleName = "";
			this.lblPostDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblPostDate.ForeColor = System.Drawing.Color.Maroon;
			this.lblPostDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPostDate.Location = new System.Drawing.Point(4, 4);
			this.lblPostDate.Name = "lblPostDate";
			this.lblPostDate.Size = new System.Drawing.Size(73, 20);
			this.lblPostDate.TabIndex = 2;
			this.lblPostDate.Text = "Post Date";
			this.lblPostDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtVendorCode
			// 
			this.txtVendorCode.AccessibleDescription = "";
			this.txtVendorCode.AccessibleName = "";
			this.txtVendorCode.Location = new System.Drawing.Point(72, 118);
			this.txtVendorCode.MaxLength = 20;
			this.txtVendorCode.Name = "txtVendorCode";
			this.txtVendorCode.Size = new System.Drawing.Size(96, 20);
			this.txtVendorCode.TabIndex = 24;
			this.txtVendorCode.Text = "";
			this.txtVendorCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendorCode_KeyDown);
			this.txtVendorCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendorCode_Validating);
			// 
			// txtVendorName
			// 
			this.txtVendorName.AccessibleDescription = "";
			this.txtVendorName.AccessibleName = "";
			this.txtVendorName.Location = new System.Drawing.Point(196, 118);
			this.txtVendorName.Name = "txtVendorName";
			this.txtVendorName.Size = new System.Drawing.Size(204, 20);
			this.txtVendorName.TabIndex = 27;
			this.txtVendorName.Text = "";
			this.txtVendorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendorName_KeyDown);
			this.txtVendorName.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendorName_Validating);
			// 
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = "";
			this.dgrdData.AccessibleName = "";
			this.dgrdData.AllowSort = false;
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(4, 276);
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
			this.dgrdData.Size = new System.Drawing.Size(624, 172);
			this.dgrdData.TabIndex = 48;
			this.dgrdData.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Part Number" +
				"\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"Part Name\" DataField=\"Description\"><ValueIt" +
				"ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Quantity\" Dat" +
				"aField=\"Quantity\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"Buying UM\" DataField=\"MST_UnitOfMeasureCode\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Unit Price (CIF)\" DataField" +
				"=\"UnitPriceCIF\" NumberFormat=\"Edit Mask\"><ValueItems /><GroupInfo /></C1DataColu" +
				"mn><C1DataColumn Level=\"0\" Caption=\"Amount\" DataField=\"Amount\"><ValueItems /><Gr" +
				"oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"VAT Amount\" DataField=" +
				"\"VATAmount\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Total Amount\" DataField=\"TotalAmount\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"%Imp Tax\" DataField=\"ImportTax\"><ValueIt" +
				"ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"INV Adjustmen" +
				"t No.\" DataField=\"TransNo\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColu" +
				"mn Level=\"0\" Caption=\"Invoice No.\" DataField=\"InvoiceNo\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWr" +
				"apper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Inactive{" +
				"ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style78{}Style79{}Style" +
				"70{AlignHorz:Near;ForeColor:ControlText;}Style71{AlignHorz:Near;}Style115{}Style" +
				"114{}Style117{}Style116{}Style72{}Style73{}Style113{AlignHorz:Near;}Style112{Ali" +
				"gnHorz:Center;ForeColor:Maroon;}Style76{AlignHorz:Near;ForeColor:Maroon;}Style77" +
				"{AlignHorz:Near;}Style74{}Style75{}Style81{}Style80{}Footer{}Editor{}FilterBar{}" +
				"RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Rais" +
				"ed,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style18{}Style19{}Style1" +
				"4{}Style15{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;}Style10{AlignHorz:Ne" +
				"ar;}Style11{}Style12{}Style13{}Selected{ForeColor:HighlightText;BackColor:Highli" +
				"ght;}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}Style27{}Style22{AlignHorz:" +
				"Center;ForeColor:ControlText;}Style9{}Style26{}Style25{}Style24{}Style5{}Style4{" +
				"}Style7{}Style6{}Style1{}Style23{AlignHorz:Near;}Style3{}Style2{}Style21{}Style2" +
				"0{}OddRow{}Style38{}Style39{}Style36{}Style37{}Style34{AlignHorz:Center;}Style35" +
				"{AlignHorz:Near;}Style32{}Style33{}Style30{}Style49{}Style48{}Style31{}Normal{Fo" +
				"nt:Tahoma, 11world;}Style41{AlignHorz:Near;}Style40{AlignHorz:Center;}Style43{}S" +
				"tyle42{}Style45{}Style44{}Style47{AlignHorz:Near;}Style46{AlignHorz:Center;ForeC" +
				"olor:Maroon;}EvenRow{BackColor:Aqua;}Style8{}Style58{AlignHorz:Center;ForeColor:" +
				"ControlText;}Style59{AlignHorz:Near;}Style50{}Style51{}Style52{AlignHorz:Center;" +
				"ForeColor:ControlText;}Style53{AlignHorz:Near;}Style54{}Style55{}Style56{}Style5" +
				"7{}Caption{AlignHorz:Center;}Style69{}Style68{}Style63{}Style62{}Style61{}Style6" +
				"0{}Style67{}Style66{}Style65{AlignHorz:Near;}Style64{AlignHorz:Near;ForeColor:Ma" +
				"roon;}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}</Da" +
				"ta></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" Co" +
				"lumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder\" R" +
				"ecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalSc" +
				"rollGroup=\"1\"><ClientRect>0, 0, 620, 168</ClientRect><BorderSide>0</BorderSide><" +
				"CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Sty" +
				"le5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"Filt" +
				"erBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle par" +
				"ent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLig" +
				"htRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" " +
				"me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle pa" +
				"rent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6" +
				"\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><Heading" +
				"Style parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><Foot" +
				"erStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\"" +
				" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"St" +
				"yle1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colu" +
				"mnDivider><Width>132</Width><Height>15</Height><Button>True</Button><DCIdx>0</DC" +
				"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style40" +
				"\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style" +
				"42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Styl" +
				"e1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visible>Tru" +
				"e</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>171</Width><Heig" +
				"ht>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
				"arent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle" +
				" parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><Gro" +
				"upHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" m" +
				"e=\"Style38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivid" +
				"er><Width>64</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1Disp" +
				"layColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me" +
				"=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"St" +
				"yle5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFoot" +
				"erStyle parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnDivider>Dar" +
				"kGray,Single</ColumnDivider><Width>59</Width><Height>15</Height><Button>True</Bu" +
				"tton><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
				"yle2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"" +
				"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderS" +
				"tyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style5" +
				"0\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width" +
				">83</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn" +
				"><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59" +
				"\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=" +
				"\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle p" +
				"arent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>DarkGray,Sin" +
				"gle</ColumnDivider><Width>93</Width><Height>15</Height><DCIdx>5</DCIdx></C1Displ" +
				"ayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style pa" +
				"rent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><Editor" +
				"Style parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style" +
				"21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><C" +
				"olumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>9</DCIdx><" +
				"/C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style112\" />" +
				"<Style parent=\"Style1\" me=\"Style113\" /><FooterStyle parent=\"Style3\" me=\"Style114" +
				"\" /><EditorStyle parent=\"Style5\" me=\"Style115\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style117\" /><GroupFooterStyle parent=\"Style1\" me=\"Style116\" /><Visible>Tr" +
				"ue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>106</Width><Hei" +
				"ght>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyl" +
				"e parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><Gr" +
				"oupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" " +
				"me=\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivi" +
				"der><Width>72</Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1Dis" +
				"playColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" m" +
				"e=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"S" +
				"tyle5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupFoo" +
				"terStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><ColumnDivider>Da" +
				"rkGray,Single</ColumnDivider><Width>78</Width><Height>15</Height><DCIdx>8</DCIdx" +
				"></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style70\" /" +
				"><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=\"Style72\"" +
				" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent=\"Style1\"" +
				" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Visible>True</" +
				"Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>135</Width><Height>" +
				"15</Height><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=" +
				"\"Style80\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider" +
				"><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn></internalCols></C1.Win.C" +
				"1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Styl" +
				"e parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style pa" +
				"rent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style par" +
				"ent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=" +
				"\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent" +
				"=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style par" +
				"ent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles" +
				"><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><D" +
				"efaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 620, 168</ClientArea>" +
				"<PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" m" +
				"e=\"Style15\" /></Blob>";
			// 
			// btnVendorCode
			// 
			this.btnVendorCode.AccessibleDescription = "";
			this.btnVendorCode.AccessibleName = "";
			this.btnVendorCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnVendorCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnVendorCode.Location = new System.Drawing.Point(170, 118);
			this.btnVendorCode.Name = "btnVendorCode";
			this.btnVendorCode.Size = new System.Drawing.Size(24, 20);
			this.btnVendorCode.TabIndex = 25;
			this.btnVendorCode.Text = "...";
			this.btnVendorCode.Click += new System.EventHandler(this.btnVendorCode_Click);
			// 
			// btnVendorName
			// 
			this.btnVendorName.AccessibleDescription = "";
			this.btnVendorName.AccessibleName = "";
			this.btnVendorName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnVendorName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnVendorName.Location = new System.Drawing.Point(401, 118);
			this.btnVendorName.Name = "btnVendorName";
			this.btnVendorName.Size = new System.Drawing.Size(24, 20);
			this.btnVendorName.TabIndex = 28;
			this.btnVendorName.Text = "...";
			this.btnVendorName.Click += new System.EventHandler(this.btnVendorName_Click);
			// 
			// btnCurrency
			// 
			this.btnCurrency.AccessibleDescription = "";
			this.btnCurrency.AccessibleName = "";
			this.btnCurrency.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCurrency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCurrency.Location = new System.Drawing.Point(401, 4);
			this.btnCurrency.Name = "btnCurrency";
			this.btnCurrency.Size = new System.Drawing.Size(24, 20);
			this.btnCurrency.TabIndex = 6;
			this.btnCurrency.Text = "...";
			this.btnCurrency.Click += new System.EventHandler(this.btnCurrency_Click);
			// 
			// dtmPostDate
			// 
			this.dtmPostDate.AccessibleDescription = "";
			this.dtmPostDate.AccessibleName = "";
			// 
			// dtmPostDate.Calendar
			// 
			this.dtmPostDate.Calendar.AccessibleDescription = "";
			this.dtmPostDate.Calendar.AccessibleName = "";
			this.dtmPostDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmPostDate.CustomFormat = "dd-MM-yyyy";
			this.dtmPostDate.ErrorInfo.ShowErrorMessage = false;
			this.dtmPostDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmPostDate.Location = new System.Drawing.Point(72, 4);
			this.dtmPostDate.Name = "dtmPostDate";
			this.dtmPostDate.Size = new System.Drawing.Size(96, 20);
			this.dtmPostDate.TabIndex = 3;
			this.dtmPostDate.Tag = null;
			this.dtmPostDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtmPostDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			// 
			// btnClose
			// 
			this.btnClose.AccessibleDescription = "";
			this.btnClose.AccessibleName = "";
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(568, 481);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 0;
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
			this.btnHelp.Location = new System.Drawing.Point(507, 481);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 60;
			this.btnHelp.Text = "&Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// btnSave
			// 
			this.btnSave.AccessibleDescription = "";
			this.btnSave.AccessibleName = "";
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(67, 481);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 56;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.AccessibleDescription = "";
			this.btnAdd.AccessibleName = "";
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAdd.Location = new System.Drawing.Point(6, 481);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(60, 23);
			this.btnAdd.TabIndex = 55;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(542, 4);
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
			// lblNetAmount
			// 
			this.lblNetAmount.AccessibleDescription = "";
			this.lblNetAmount.AccessibleName = "";
			this.lblNetAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblNetAmount.ForeColor = System.Drawing.Color.Maroon;
			this.lblNetAmount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblNetAmount.Location = new System.Drawing.Point(240, 188);
			this.lblNetAmount.Name = "lblNetAmount";
			this.lblNetAmount.Size = new System.Drawing.Size(66, 20);
			this.lblNetAmount.TabIndex = 39;
			this.lblNetAmount.Text = "Net Amount";
			this.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtNote
			// 
			this.txtNote.AccessibleDescription = "";
			this.txtNote.AccessibleName = "";
			this.txtNote.Location = new System.Drawing.Point(72, 164);
			this.txtNote.MaxLength = 100;
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(328, 22);
			this.txtNote.TabIndex = 35;
			this.txtNote.Text = "";
			// 
			// lblNote
			// 
			this.lblNote.AccessibleDescription = "";
			this.lblNote.AccessibleName = "";
			this.lblNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblNote.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblNote.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblNote.Location = new System.Drawing.Point(6, 164);
			this.lblNote.Name = "lblNote";
			this.lblNote.Size = new System.Drawing.Size(73, 20);
			this.lblNote.TabIndex = 34;
			this.lblNote.Text = "Note";
			this.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = "";
			this.btnDelete.AccessibleName = "";
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(190, 480);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 59;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.AccessibleDescription = "";
			this.btnEdit.AccessibleName = "";
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnEdit.Location = new System.Drawing.Point(336, 482);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(60, 23);
			this.btnEdit.TabIndex = 58;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.Visible = false;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// txtVAT
			// 
			this.txtVAT.AccessibleDescription = "";
			this.txtVAT.AccessibleName = "";
			// 
			// txtVAT.Calculator
			// 
			this.txtVAT.Calculator.AccessibleDescription = "";
			this.txtVAT.Calculator.AccessibleName = "";
			this.txtVAT.CustomFormat = "###############,0.00";
			this.txtVAT.EmptyAsNull = true;
			this.txtVAT.ErrorInfo.ShowErrorMessage = false;
			this.txtVAT.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtVAT.Location = new System.Drawing.Point(472, 188);
			this.txtVAT.Name = "txtVAT";
			this.txtVAT.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																								 new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																								   0,
																																								   0,
																																								   0,
																																								   0}), new System.Decimal(new int[] {
																																																		 -1530494977,
																																																		 232830,
																																																		 0,
																																																		 0}), true, true)});
			this.txtVAT.Size = new System.Drawing.Size(50, 20);
			this.txtVAT.TabIndex = 42;
			this.txtVAT.Tag = null;
			this.txtVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtVAT.Value = new System.Decimal(new int[] {
																 0,
																 0,
																 0,
																 0});
			this.txtVAT.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtVAT.Leave += new System.EventHandler(this.txtVAT_Leave);
			// 
			// lblVAT
			// 
			this.lblVAT.AccessibleDescription = "";
			this.lblVAT.AccessibleName = "";
			this.lblVAT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblVAT.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVAT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblVAT.Location = new System.Drawing.Point(422, 188);
			this.lblVAT.Name = "lblVAT";
			this.lblVAT.Size = new System.Drawing.Size(50, 20);
			this.lblVAT.TabIndex = 41;
			this.lblVAT.Text = "VAT (%)";
			this.lblVAT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtGrandTotal
			// 
			this.txtGrandTotal.AccessibleDescription = "";
			this.txtGrandTotal.AccessibleName = "";
			this.txtGrandTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// txtGrandTotal.Calculator
			// 
			this.txtGrandTotal.Calculator.AccessibleDescription = "";
			this.txtGrandTotal.Calculator.AccessibleName = "";
			this.txtGrandTotal.CustomFormat = "###############,0.00";
			this.txtGrandTotal.ErrorInfo.ShowErrorMessage = false;
			this.txtGrandTotal.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtGrandTotal.Location = new System.Drawing.Point(527, 455);
			this.txtGrandTotal.Name = "txtGrandTotal";
			this.txtGrandTotal.ReadOnly = true;
			this.txtGrandTotal.Size = new System.Drawing.Size(100, 20);
			this.txtGrandTotal.TabIndex = 54;
			this.txtGrandTotal.Tag = null;
			this.txtGrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtGrandTotal.Value = new System.Decimal(new int[] {
																		0,
																		0,
																		0,
																		0});
			this.txtGrandTotal.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// lblGrandToTal
			// 
			this.lblGrandToTal.AccessibleDescription = "";
			this.lblGrandToTal.AccessibleName = "";
			this.lblGrandToTal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblGrandToTal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblGrandToTal.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblGrandToTal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblGrandToTal.Location = new System.Drawing.Point(463, 455);
			this.lblGrandToTal.Name = "lblGrandToTal";
			this.lblGrandToTal.Size = new System.Drawing.Size(64, 20);
			this.lblGrandToTal.TabIndex = 53;
			this.lblGrandToTal.Text = "Grand Total";
			this.lblGrandToTal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtSubTotal
			// 
			this.txtSubTotal.AccessibleDescription = "";
			this.txtSubTotal.AccessibleName = "";
			this.txtSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// txtSubTotal.Calculator
			// 
			this.txtSubTotal.Calculator.AccessibleDescription = "";
			this.txtSubTotal.Calculator.AccessibleName = "";
			this.txtSubTotal.CustomFormat = "###############,0.00";
			this.txtSubTotal.ErrorInfo.ShowErrorMessage = false;
			this.txtSubTotal.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtSubTotal.Location = new System.Drawing.Point(354, 455);
			this.txtSubTotal.Name = "txtSubTotal";
			this.txtSubTotal.ReadOnly = true;
			this.txtSubTotal.Size = new System.Drawing.Size(100, 20);
			this.txtSubTotal.TabIndex = 52;
			this.txtSubTotal.Tag = null;
			this.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSubTotal.Value = new System.Decimal(new int[] {
																	  0,
																	  0,
																	  0,
																	  0});
			this.txtSubTotal.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// lblSubTotal
			// 
			this.lblSubTotal.AccessibleDescription = "";
			this.lblSubTotal.AccessibleName = "";
			this.lblSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSubTotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblSubTotal.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblSubTotal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblSubTotal.Location = new System.Drawing.Point(300, 455);
			this.lblSubTotal.Name = "lblSubTotal";
			this.lblSubTotal.Size = new System.Drawing.Size(56, 20);
			this.lblSubTotal.TabIndex = 51;
			this.lblSubTotal.Text = "Sub Total";
			this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtTotalVAT
			// 
			this.txtTotalVAT.AccessibleDescription = "";
			this.txtTotalVAT.AccessibleName = "";
			this.txtTotalVAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// txtTotalVAT.Calculator
			// 
			this.txtTotalVAT.Calculator.AccessibleDescription = "";
			this.txtTotalVAT.Calculator.AccessibleName = "";
			this.txtTotalVAT.CustomFormat = "###############,0.00";
			this.txtTotalVAT.ErrorInfo.ShowErrorMessage = false;
			this.txtTotalVAT.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtTotalVAT.Location = new System.Drawing.Point(188, 455);
			this.txtTotalVAT.Name = "txtTotalVAT";
			this.txtTotalVAT.ReadOnly = true;
			this.txtTotalVAT.Size = new System.Drawing.Size(100, 20);
			this.txtTotalVAT.TabIndex = 50;
			this.txtTotalVAT.Tag = null;
			this.txtTotalVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtTotalVAT.Value = new System.Decimal(new int[] {
																	  0,
																	  0,
																	  0,
																	  0});
			this.txtTotalVAT.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			// 
			// lblTotalVAT
			// 
			this.lblTotalVAT.AccessibleDescription = "";
			this.lblTotalVAT.AccessibleName = "";
			this.lblTotalVAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTotalVAT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblTotalVAT.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTotalVAT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblTotalVAT.Location = new System.Drawing.Point(134, 455);
			this.lblTotalVAT.Name = "lblTotalVAT";
			this.lblTotalVAT.Size = new System.Drawing.Size(56, 20);
			this.lblTotalVAT.TabIndex = 49;
			this.lblTotalVAT.Text = "Total VAT";
			this.lblTotalVAT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblInvoiceNo
			// 
			this.lblInvoiceNo.AccessibleDescription = "";
			this.lblInvoiceNo.AccessibleName = "";
			this.lblInvoiceNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblInvoiceNo.ForeColor = System.Drawing.Color.Maroon;
			this.lblInvoiceNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblInvoiceNo.Location = new System.Drawing.Point(6, 188);
			this.lblInvoiceNo.Name = "lblInvoiceNo";
			this.lblInvoiceNo.Size = new System.Drawing.Size(73, 20);
			this.lblInvoiceNo.TabIndex = 36;
			this.lblInvoiceNo.Text = "Receipt No.";
			this.lblInvoiceNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtInvoiceNo
			// 
			this.txtInvoiceNo.AccessibleDescription = "";
			this.txtInvoiceNo.AccessibleName = "";
			this.txtInvoiceNo.Location = new System.Drawing.Point(72, 188);
			this.txtInvoiceNo.MaxLength = 20;
			this.txtInvoiceNo.Name = "txtInvoiceNo";
			this.txtInvoiceNo.Size = new System.Drawing.Size(121, 20);
			this.txtInvoiceNo.TabIndex = 37;
			this.txtInvoiceNo.Text = "";
			this.txtInvoiceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvoiceNo_KeyDown);
			this.txtInvoiceNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtInvoiceNo_Validating);
			// 
			// btnInvoiceNo
			// 
			this.btnInvoiceNo.AccessibleDescription = "";
			this.btnInvoiceNo.AccessibleName = "";
			this.btnInvoiceNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnInvoiceNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnInvoiceNo.Location = new System.Drawing.Point(195, 188);
			this.btnInvoiceNo.Name = "btnInvoiceNo";
			this.btnInvoiceNo.Size = new System.Drawing.Size(24, 20);
			this.btnInvoiceNo.TabIndex = 38;
			this.btnInvoiceNo.Text = "...";
			this.btnInvoiceNo.Click += new System.EventHandler(this.btnInvoiceNo_Click);
			// 
			// txtNetAmount
			// 
			this.txtNetAmount.AccessibleDescription = "";
			this.txtNetAmount.AccessibleName = "";
			// 
			// txtNetAmount.Calculator
			// 
			this.txtNetAmount.Calculator.AccessibleDescription = "";
			this.txtNetAmount.Calculator.AccessibleName = "";
			this.txtNetAmount.CustomFormat = "###############,0.00";
			this.txtNetAmount.EmptyAsNull = true;
			this.txtNetAmount.ErrorInfo.ShowErrorMessage = false;
			this.txtNetAmount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtNetAmount.Location = new System.Drawing.Point(308, 188);
			this.txtNetAmount.MaxLength = 13;
			this.txtNetAmount.Name = "txtNetAmount";
			this.txtNetAmount.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									   new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																										 0,
																																										 0,
																																										 0,
																																										 0}), new System.Decimal(new int[] {
																																																			   276447231,
																																																			   23283,
																																																			   0,
																																																			   0}), false, true)});
			this.txtNetAmount.Size = new System.Drawing.Size(92, 20);
			this.txtNetAmount.TabIndex = 40;
			this.txtNetAmount.Tag = null;
			this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtNetAmount.Value = new System.Decimal(new int[] {
																	   0,
																	   0,
																	   0,
																	   0});
			this.txtNetAmount.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtNetAmount.Leave += new System.EventHandler(this.txtNetAmount_Leave);
			// 
			// btnAllocate
			// 
			this.btnAllocate.Location = new System.Drawing.Point(308, 210);
			this.btnAllocate.Name = "btnAllocate";
			this.btnAllocate.Size = new System.Drawing.Size(92, 24);
			this.btnAllocate.TabIndex = 47;
			this.btnAllocate.Text = "A&llocate";
			this.btnAllocate.Click += new System.EventHandler(this.btnAllocate_Click);
			// 
			// rptFreightSlip
			// 
			this.rptFreightSlip.ReportDefinition = "<!--Report *** FreightSlip ***--><Report version=\"2.5.20043.144\"><Name>FreightSli" +
				"p</Name><DataSource><ConnectionString>Provider=SQLOLEDB;Data Source=ngannt;User " +
				"ID=sa;Password=sa;Initial Catalog=MAP</ConnectionString><RecordSource>SELECT \tcs" +
				"t_FreightMaster.PostDate, MST_Currency.Code AS Currency, ExchangeRate, TranNo, V" +
				"ATPercent,\r\n\tTransporter.Code + \' (\' + Transporter.Name + \')\' AS Transporter,\r\n\t" +
				"V.Code + \' (\' + V.Name + \')\' AS Vendor, Note, STD_CostElement.Code AS CostElemen" +
				"t,\r\n\tITM_Product.Code AS PartsNumber, ITM_Product.Description AS PartsName, ITM_" +
				"Product.Revision AS Model,\r\n\tMST_UnitOfMeasure.Code AS BuyingUM, cst_FreightDeta" +
				"il.Quantity, cst_FreightDetail.Amount,\r\n\tcst_FreightMaster.SubTotal, cst_Freight" +
				"Master.TotalVAT, cst_FreightMaster.GrandTotal,\r\n\tPO_PurchaseOrderReceiptMaster.R" +
				"eceiveNo\r\nFROM cst_FreightMaster JOIN cst_FreightDetail\r\nON cst_FreightMaster.Fr" +
				"eightMasterID = cst_FreightDetail.FreightMasterID\r\nJOIN MST_Currency\r\nON cst_Fre" +
				"ightMaster.CurrencyID = MST_Currency.CurrencyID\r\nLEFT JOIN MST_Party AS Transpor" +
				"ter\r\nON cst_FreightMaster.TransporterID = Transporter.PartyID\r\nLEFT JOIN MST_Par" +
				"ty AS V\r\nON cst_FreightMaster.VendorID = V.PartyID\r\nJOIN MST_UnitOfMeasure\r\nON c" +
				"st_FreightDetail.BuyingUMID = MST_UnitOfMeasure.UnitOfMeasureID\r\nJOIN ITM_Produc" +
				"t\r\nON cst_FreightDetail.ProductID = ITM_Product.ProductID\r\nJOIN STD_CostElement\r" +
				"\nON cst_FreightMaster.CostElementID = STD_CostElement.CostElementID\r\nJOIN PO_Pur" +
				"chaseOrderReceiptMaster\r\nON cst_FreightMaster.PurchaseOrderReceiptID = PO_Purcha" +
				"seOrderReceiptMaster.PurchaseOrderReceiptID</RecordSource></DataSource><Layout><" +
				"Width>11250</Width><MarginLeft>500</MarginLeft><MarginTop>500</MarginTop><Margin" +
				"Right>500</MarginRight><MarginBottom>200</MarginBottom><Orientation>1</Orientati" +
				"on><PaperSize>9</PaperSize></Layout><Font><Name>Arial Narrow</Name><Size>8</Size" +
				"></Font><Groups><Group><Name>TranNo</Name><GroupBy>TranNo</GroupBy><Sort>1</Sort" +
				"><KeepTogether>1</KeepTogether></Group></Groups><Sections><Section><Name>Detail<" +
				"/Name><Type>0</Type><Height>300</Height></Section><Section><Name>Header</Name><T" +
				"ype>1</Type><Visible>0</Visible></Section><Section><Name>Footer</Name><Type>2</T" +
				"ype><Visible>0</Visible></Section><Section><Name>PageHeader</Name><Type>3</Type>" +
				"<Visible>0</Visible></Section><Section><Name>PageFooter</Name><Type>4</Type><Hei" +
				"ght>300</Height></Section><Section><Name>TranNo Header</Name><Type>5</Type><Heig" +
				"ht>4545</Height><Repeat>-1</Repeat></Section><Section><Name>TranNo Footer</Name>" +
				"<Type>6</Type><Height>1320</Height></Section></Sections><Fields><Field><Name>fld" +
				"No</Name><Section>0</Section><Text>1</Text><Width>450</Width><Height>300</Height" +
				"><ZOrder>-72</ZOrder><Align>7</Align><RunningSum>1</RunningSum><WordWrap>0</Word" +
				"Wrap><CanGrow>-1</CanGrow><CanShrink>-1</CanShrink><Anchor>2</Anchor><Font><Name" +
				">Arial Narrow</Name><Size>6.75</Size></Font></Field><Field><Name>fldPartNumber</" +
				"Name><Section>0</Section><Text>PartsNumber</Text><Calculated>-1</Calculated><Lef" +
				"t>450</Left><Width>1710</Width><Height>300</Height><ZOrder>-71</ZOrder><Align>6<" +
				"/Align><MarginLeft>50</MarginLeft><WordWrap>0</WordWrap><CanGrow>-1</CanGrow><Ca" +
				"nShrink>-1</CanShrink><Anchor>2</Anchor><Font><Name>Arial Narrow</Name><Size>6.7" +
				"5</Size></Font></Field><Field><Name>fldPartName</Name><Section>0</Section><Text>" +
				"PartsName</Text><Calculated>-1</Calculated><Left>2160</Left><Width>3015</Width><" +
				"Height>300</Height><ZOrder>-70</ZOrder><Align>6</Align><MarginLeft>50</MarginLef" +
				"t><WordWrap>0</WordWrap><CanGrow>-1</CanGrow><CanShrink>-1</CanShrink><Anchor>2<" +
				"/Anchor><Font><Name>Arial Narrow</Name><Size>6.75</Size></Font></Field><Field><N" +
				"ame>fldUnit</Name><Section>0</Section><Text>BuyingUM</Text><Calculated>-1</Calcu" +
				"lated><Left>6210</Left><Width>945</Width><Height>300</Height><ZOrder>-69</ZOrder" +
				"><Align>7</Align><WordWrap>0</WordWrap><CanGrow>-1</CanGrow><CanShrink>-1</CanSh" +
				"rink><Anchor>2</Anchor><Font><Name>Arial Narrow</Name><Size>6.75</Size></Font></" +
				"Field><Field><Name>fldDeliveryQuantity</Name><Section>0</Section><Text>Quantity<" +
				"/Text><Calculated>-1</Calculated><Format>#,##0</Format><Left>7155</Left><Width>1" +
				"575</Width><Height>300</Height><ZOrder>-68</ZOrder><Align>8</Align><MarginRight>" +
				"50</MarginRight><WordWrap>0</WordWrap><CanGrow>-1</CanGrow><CanShrink>-1</CanShr" +
				"ink><Anchor>2</Anchor><Font><Name>Arial Narrow</Name><Size>6.75</Size></Font></F" +
				"ield><Field><Name>fldModel</Name><Section>0</Section><Text>Model</Text><Calculat" +
				"ed>-1</Calculated><Left>5175</Left><Width>1035</Width><Height>300</Height><ZOrde" +
				"r>-65</ZOrder><Align>7</Align><MarginLeft>50</MarginLeft><WordWrap>0</WordWrap><" +
				"CanGrow>-1</CanGrow><CanShrink>-1</CanShrink><Anchor>2</Anchor><Font><Name>Arial" +
				" Narrow</Name><Size>6.75</Size></Font></Field><Field><Name>fldFreight</Name><Sec" +
				"tion>0</Section><Text>Amount</Text><Calculated>-1</Calculated><Format>#,##0.00</" +
				"Format><Left>8730</Left><Width>2160</Width><Height>300</Height><ZOrder>-61</ZOrd" +
				"er><Align>8</Align><MarginRight>50</MarginRight><WordWrap>0</WordWrap><CanGrow>-" +
				"1</CanGrow><CanShrink>-1</CanShrink><Anchor>2</Anchor><Font><Name>Arial Narrow</" +
				"Name><Size>6.75</Size></Font></Field><Field><Name>Field4</Name><Section>0</Secti" +
				"on><Height>300</Height><BorderStyle>1</BorderStyle><LineSlant>1</LineSlant><Font" +
				"><Name>Arial Narrow</Name><Size>6.75</Size></Font></Field><Field><Name>Field27</" +
				"Name><Section>0</Section><Left>450</Left><Height>300</Height><BorderStyle>3</Bor" +
				"derStyle><LineSlant>1</LineSlant></Field><Field><Name>Field28</Name><Section>0</" +
				"Section><Left>2160</Left><Height>300</Height><BorderStyle>3</BorderStyle><LineSl" +
				"ant>1</LineSlant></Field><Field><Name>Field29</Name><Section>0</Section><Left>51" +
				"75</Left><Height>300</Height><BorderStyle>3</BorderStyle><LineSlant>1</LineSlant" +
				"></Field><Field><Name>Field30</Name><Section>0</Section><Left>7155</Left><Height" +
				">300</Height><BorderStyle>3</BorderStyle><LineSlant>1</LineSlant></Field><Field>" +
				"<Name>Field31</Name><Section>0</Section><Left>8730</Left><Height>300</Height><Bo" +
				"rderStyle>3</BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>Field33</N" +
				"ame><Section>0</Section><Left>6210</Left><Height>300</Height><BorderStyle>3</Bor" +
				"derStyle><LineSlant>1</LineSlant></Field><Field><Name>Field3</Name><Section>0</S" +
				"ection><Left>10890</Left><Height>300</Height><BorderStyle>1</BorderStyle><LineSl" +
				"ant>1</LineSlant><Font><Name>Arial Narrow</Name><Size>6.75</Size></Font></Field>" +
				"<Field><Name>Field35</Name><Section>0</Section><Top>300</Top><Width>10890</Width" +
				"><Align>3</Align><BorderStyle>3</BorderStyle><LineSlant>3</LineSlant></Field><Fi" +
				"eld><Name>ftrRight</Name><Section>4</Section><Text>\"Page \" &amp; [Page] &amp; \"/" +
				"\" &amp; [Pages]</Text><Calculated>-1</Calculated><Left>6210</Left><Width>4680</W" +
				"idth><Height>300</Height><Align>5</Align><Font><Name>Arial Narrow</Name><Size>8." +
				"25</Size></Font></Field><Field><Name>Field1</Name><Section>5</Section><Top>570</" +
				"Top><Width>10890</Width><BorderStyle>1</BorderStyle><LineSlant>1</LineSlant></Fi" +
				"eld><Field><Name>picLogo</Name><Section>5</Section><Left>45</Left><Top>675</Top>" +
				"<Width>1170</Width><Height>540</Height><ZOrder>-60</ZOrder><Picture encoding=\"ba" +
				"se64\">iVBORw0KGgoAAAANSUhEUgAAAGUAAAA3CAIAAACaWBNkAAAAAXNSR0IArs4c6QAAAARnQU1BAA" +
				"Cxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAAlwSFlzAA" +
				"ALEAAACxABrSO9dQAAIAZJREFUeF7lWweYlNW5PtNn2cWIijUXNQYbLNNndpeiErHn2oiiuUYlooktXl" +
				"swtkAiKoGAwrK90HYBQVGwRIJSlu29950tbG+z0+u573f+1STmkgtXvY+Pd55/Z2dm//n/c97zfe/3ft" +
				"93VsY5Z/9vHpFIRMZCXC4LMxYRs1YypsAvYMBVLESfBNT4U0SL15wHVRE5Uyl4xCHz/CCowFt8+L19AJ" +
				"1wOIznv5+hi3M/3uOzIOcB8RzhYbzFp176JMS5zx+mXxFxij+CF+M8HAz7fDz4fcYLYOEhgRUKhaTXob" +
				"AvHA5GIsCDc3wAyIBUANAEAYebB7087Ar78Gd8PAHYACVwxLMHQPLvM15/b1nAKxgETLAlH+AJcr+HB5" +
				"wEUIjMDz9AMxSGaU2aHtBxBYEaLCuEU/Al4Pf9xusrRDMJH3wvEOIB4WzCyNw87ABDkbERVg4eCuI3uW" +
				"Soi8N9RwjYgB9G1svD32f7ktwQj78HDtOG6XgiEkERR4U5KCrg58M83MdHWr2HPxxctarrjv+w33Rn3W" +
				"23Fy9caF+ydGD9Ju4YG+TO7zle4KxJNxSY+f1+DicL+HjYw4MOPmwP5H1yPHl946u/Hb3i2vwZsfujzi" +
				"hh6lHZtGbGihmrZKyFMbwuO/tyx9MvR4Kt31u8vmT6L1kMYA0NDUVy9xz//arSW+8+ern+2NRzSpiqnr" +
				"EOJit7/XUe7OXdFZXxC/KZMl+prIxS9jBWF826CLuoggtnc17/reMFoiR/QDyKBHjExwMuHvHzkB/0Cb" +
				"+YEAGdh4lOsPQBBKpAJOjHn7mT89FImGI/TqETRKiHO/nJPvAbVwZ9D3A68OEY+MdDZDTEfcEwGD2M19" +
				"wX5p6we2ws/+YHPjvvohYmb2SsnskqZRqYT7WW1SpYNVMdZVPcNZ3iXo62WF2dVlHGlL1MUw7LYqe3Aa" +
				"9oxRF2TsBV+a3jJZEE6NIX9nq4302xieTOJH/gnTdMRzDkj4hJBjwEiy+Ek0gVwYOEDEJg8/DIqI/eA0" +
				"rwsGNihEMShVw8OMY7+wl3fIoYB2RJCyACYn0ErrjwoJ2XH7CfNb2GrEldoFBVyuXVMvK4KqbouPEmfA" +
				"VrwHta806bWsJYOVO1ME0VY60sKl/Napmi5N+soXDLt49XECCEJe0nGQXBEECc9gG7Ca8DfCyJR7cftB" +
				"LxcvGFYIQ4GZ+Cjf1eUI4wK0gkPsrx1sOHO/hYuz//fd/KlS2/Wla++EE+4SCYQuFgyBsBaAI3h8AQas" +
				"vF+3s2/T5PqahgrI2p8jSqaiZrgLsxVshUoT3ZGJuX+wLr1x+WwQBZlUZTKlM2KliZjNVo2RHGuu54LB" +
				"To/tbxggfSvBHCYQrCEQkzTAJKRwhFIQvhR2KGZCQBv88VkHRQGLKafgX8bnIWUgBB7nFWPvafTZfZam" +
				"PO/lDJ6qZOzWeyD9Vncu+4kKBh+C0uSG9wR5IDAAx6q+fIpVc0y6JgU4CsVK0pA5cr5cDu2NkzuaO1n2" +
				"S+o2mOtUxJBF8iY4eV8iaZukzLmlUww7P4B4d9/wfxcYL7QiAsyGgcMBbxWxCOmL+HXGZMOCnkdT+A9H" +
				"mACMYOBDFtnBb24uvhMaGMeMjhSU87rJk+wmJgGh1qeBODFZSccxEuMwGKhD+GifWgM3EnEByWhk+Mhz" +
				"7b18B+YCfyYvkq+Je8TM7q5TKQl/2Jp/3cOYHz8z6FbzZoWRNoS86ORKlrmBrn1zN5+x33c5fXwQPfun" +
				"0RBcM9AFKIjxM9B8a4O4hZEK+B/MccfCTstvOyUl9aTvvaTaWLlu2/YSkv64DPjTldmC1OHfKMc2eQhz" +
				"y8/NiR8y7qZIoeJq9Ws0phC5he1UVXgtb6g16RCwYneMSFWxIzCryCjqo7F7czMhbMv3gKxcTaKBVM7J" +
				"jmTF5eQMwV8tUtWYyrtWhljUxRI5MXqeXVLAp8V37uTD7UQ4zBQ986XiKFxZpzhz8EHyG3nBjkLfXeA/" +
				"smEtd3LHugIsFcc9o5HezMQ6rzB55/0VVewkcHyQVhWHApWAi+HqTAxwc682/792K1BiRdyIhT6hVRHU" +
				"IldRl+ggnDJGGnfu5DDuiJwGrD3B1xYmXa66umXVTL2EFiblalUuK5TiGHY9bfdCMMl5y+oaF82jktMj" +
				"qhnClwTaBZrYk+fOYPu3OzQLbk4KH/g/wR3BEKY+RYZ2KnwcGu5SvfP+PHRez0SqauU7NSBWIQa2fsg/" +
				"On886jYT4eQuYGTSBEhMRi8Gnkw87XNxWyGJB0dTTxS5NcU8rUlVOiDyhUHVcvBllTEg257nMSOQYd+K" +
				"FJhoMT6zbWs5iiKXQj+C9Mpk5DoDcylXv9Hz3w+yAvemVlM5kV5AU4PqaFqaA8yjRavmsrLJdIFnNwnD" +
				"peHrJ5MA6IFLREGkpwDJmRG4qZSGYg3FnKc7KbXk8kknIPD/CwJJG4d8h5aLudqctVrFyL4bJqpbpaQQ" +
				"RcKVe2373MAf+hS4WDATe9EpSHVA7BlH/6YecZ52MypRDcCm25ggyhTkGW0sKia5cuBVtDmmJs5OqIsR" +
				"LQhHh/449ndinZYQWcl3wwD/qLsV6mLGYzwJbkjHXNFTIV7A4jgcH2MPUBxg4nXM0/2i/I9m+PU/ZHhD" +
				"qs2xDVPRCuBQtP2IcLPh59fXXj0gfarAuqNedXsKgmFl2asIi7QaMoBIhoBdkw0FJ6wcyqqOhSGSuXM7" +
				"IUiCDG7NGqAqbie98NoRhAyOKJ8hjhvRRceU/vviV3HoYUUNNUwdbAC3wPYwFejSym6VePCrwIIAkvqe" +
				"qAUOM/uC9fO6VKxorJ1+SlalWLkoyUtOgT93kh5kb7uu55sEQlb2GyIaZuYPJD0y/oXvEK77eLQP718O" +
				"IO6EOff7jHvmtH2Y13l8VcXCL7QTfTFKmijsmJfWHzyLmgiZvmX829w5gsYTAGTe7p+/lDyM4ww9JoJW" +
				"ACWFUyGai3Ta4u/vHlPABDpHAp8gGyx0HKCsIuPup8aV3N8mfKmLpdQ6SD7wIv3KteqYJzNbCYzuee45" +
				"EJCS9B3iSJPXQlZ899Dx2FPkBMJIjln2uVdYy+VaWV8bKdvLGgfdVL+UxRpNEeUmgOXPBvLcuf5r0Nfu" +
				"5yhKAHvwLXqfsjVlwoAFewpZI3HBq+wnDkNHZYi0HDnlmbQtYgk5VrWJFMVnnb7RgziFfo01Dg7YzKa+" +
				"ZjDTujplQygqlCCchYk0IFF+tf+YqPO8L+ABIYkmhuKtRh5kGfw7d3f8UjT/IP9lQromEUsBTcC/EeYR" +
				"FRDDOvYdFDr/0hTPKDfFDCC0+Iqby3sSbmwkq5ulojq1WyRrmym8mLoT+06s/U2or4hbXa2GbLov777x" +
				"7JfIvXF3LvIEKrqB1O6uWva18YDZyLzHS4mxf+BdwBa8K0q+AjMrKsVqasVajBxI33LJUSPtL07V0V82" +
				"7pXnorTihW4TREdBnhhYxEO6VCdi7vqhPKlgwE5/u8qEnhNhPc76yNv4f3NPBtWbAvYmsFsFYiZSHEBX" +
				"lXsBhv4saApFFCAi9kFJg0d46ue7OGyetVWjgvYMJXEFibpqrbmGzwt7/jPe3c7+DBEVQqYP7jPICpEe" +
				"dOUAaBq4yLX1+Lv0R9W0p2XfX3Pdw0/ZJWmQbRCuEZNg82BTcdVbM8uaJv2ROQ81ChyGZa5t7n/3xnwe" +
				"kx9VNVx0gHQG0qKsH6oFWFyn7LvVBksEMKoL4I7IQSKAh17mxaujyY9S5ejG1cV6c+rUlOGDXK1FVqOT" +
				"gI9gXEEc749i2ktERiJfASHu0bLjOY7UwpIGblSjJGFBvwlfLTz+c9XaiMUV4B2Q73xYxQIiTKE98F5G" +
				"HkqbRqXxcvkdm6+PHWTy409N+7BIKoSANRo0YmgZDXwRRwmTKVxrn8RQ9G4RmqfnP9+Mq1nux1IGw4UV" +
				"3UlFIWXcMUOB8TOBoV49y/B0yPOCKSZaorAGU+MdH1/u6ue59GJukL+O2v/b6caXA+nLdVNQVqExAgB8" +
				"S9ioDX3nc9X+Alpag0yP37i5TRrSp5HakEFZKbSgXMk+ivdcli3KyH807gM4GkQmAixXvqelDdGsssOf" +
				"fXwyvo76erOHs3pbQtvC2ycjnyUgy6iU2tZcp2JrezqHKEIUV04JVVKMrxomMV19/HeXf93KsamcbOZG" +
				"1MnS+PgZvkw3/VrP6cGVSSgRqX1AOtMnSTj7fV9s2/lw92oHgDK6h/8flipoB9IT7WMBmqMXCxWjmtTY" +
				"E8hn+83425CqaU8MIQu5b+poFpyQGJ79WdYC4VgyPDqfln7wCdYfgK2iAUwgIO7nHREFBrJeOmkeD4h9" +
				"Is4XbKekKoRzx1ds2a79v5XtvKFVUUbuTQBwhApPHkgpViTucr14T4wJF5i12uZtexfY3snEptNKooCF" +
				"INjOSiXc0+Zyz4RiIFUVw0SF0ZEpxIA3ngwJ2P808K/NwNsBA1vYtvPaaNgXE1MRVcskxLkkIqMJQwGb" +
				"c3kGQRNRkiIPjyYMuu8y+pZdHtTIVzOpkMa1kj1yCqllst4HWh73A6hfvJnPYrtvTfvT1lvGhKrkCgIa" +
				"9k2uXc3df28OPgLCoPKclTqlW0ek1RKiQrPCctf+mj3nSwT3/rs48VMC2QhekhMjQxWSHqJFCeU8/i1a" +
				"VDuCbSQy8EBYKc3+3pHcvKrHl2Oa6PnFGkgI6+q+blqUHbartqKvAqktPtgFejFpBF8c4WMit4kxC5UI" +
				"fh5D8fOO2sFqbFQiJ8F6mI+IqUrFAV3bfmTZwh1gaSAZUTya5P6nHKeBEbRjx9v36u+/4nHNzRe8/Seh" +
				"moRFmuUTYqFVASsC+sYeEZ07offLBt2W84ag1j9tIfzsgTqUYFzkSdBEipqKLSfN21iLdEr6J1Okrs4e" +
				"Kt5XWGBOdHmwMf7Gt7aV3//AdbZi6o+NGMg0RYVGOBTxWJ/K5RFPzKWUzAN4QoQb3VgKgRhjyVsXrUeT" +
				"qYCrEoL4pUCIrLOA5Pm8mbIa+I14V1URnyW7QvpBy+sfamaANvaxoLD/f87OdNminNTA4Tq4sisqhCnY" +
				"SBU6LbjAt5ZzncxJOeTkJcKXJdNqVSrQReMI1K5dTxj3OoLj1KqTjcYgCa3mvvu+bu5isXtP3swcCKjd" +
				"6PPgu2QWoMtl36IxSt6pgSSR+iaqVKBhEDR2tWqouip48DdKn56vXCsR2FeRCxXUw1wLQAFJkj8K3HCE" +
				"Gvi+/n7jH3uAtujvgn6uBIy0/KuP43/IUq38Cm9XbdzSiERgKOpptvgp3DoJqZslAghYDdEHN6KftBeP" +
				"tWHx8+HnA2JtwIgDpJOgHNKTUKjBs0pC2Z/mMkm2gpY6q01E6qHLevWdG7fAUccBxJF2YUcQVCXkDaPu" +
				"O8ai1yAxm8mChfrYCCEeFSXjPjMsRQCv1+eBmc19n2yDOFUTIQHMZWAVi1CiwYfBNCt/PTDPx9oKMe96" +
				"Wigxf+LpKnk3ucsj+GgZEuYXz3VpJHIW/HokVU9wBGsigMrvs02BpGObVr+e+QBUS421V+rJKdXa+RQa" +
				"kiIFQpNDi5llI5TdcLLxN9RETOGAJPB/nnh5p/spgf78G1YXHjYWfI6wAOyIvKFJMNLswceCGXgrEAd9" +
				"zdPtvijYy6HKhLjA+gjdjb0HjmnDaIYREKEYKAVBkxpvaIYkrJDGP3vCXDryVTL4G6alTKCIohnMzjlP" +
				"HyHC1suOxKdOJgEv6A+7h1bp1KAY4ok6sxMsR7hLwm20K/r8crMBh76tF6FgXhg4nByqRMoFqmPgKS7u" +
				"+gihVVIlAJDQe6W4pvvYMXl1GkBIDHvZTg+IPDKGu4Xfgi4K6HCSsVtUq5UBWC75liwDpvzNlFYdE5wg" +
				"fqR5PWH5RP7WCo9kwpPOvC8tm25tvv6V250v3BTl5X4EMuN4EWEpJLFI18Yb8HzaSvqtITI3fKeLUvX3" +
				"v8l3dCT1GjJhSuO//iKqWsWCOrkWsbppDt5E0/nTceBuNSX9nJa884A64HxZR/mhylElRvqmVItqOP33" +
				"Ub9zlwEcoUSEy47X9YG1r+DMxN2jmDz71uRMwQd7p8XbW4MpU6JWdUyGA4dUzWhFyHybtm6Ws/2u39a3" +
				"mn7s6KS+JLr1o08fJjw3u3j1R+xr29PIRymA/3cIZDRFOi0kMvRCWHVAgKVCftkCfECxPBiGGyE1QK99" +
				"F1nVBG/Y3ai3hj6TC1ELg3ONh8zoVwwEqVtllGS31EPtWetpEqMCTTfG3vvV+hkJcpFIj9dQo1okEp1J" +
				"mcfKTzw48ACRwNInqc9/FP9ubNvIaEForOkQDmQ4kvNfQhMIb6juwFWKjTI2JA3DVo0B+kTLBarcozxV" +
				"XdtaTv7XW8qYYH0VMSQEsSgcpCk7kRbiT6UFLTZPLzCPIJUWMk1Rfx4xB7KyBG0Iuj6gQ4rrnLfejIRF" +
				"Jiwf79laCGE+IVCrmkugqWIQJd50fV1jmWmVZ99c08NEKeAo6JjDROvwAkUiCTo1KKObTeu4SP9SG1p3" +
				"TEM2aPv7uJKdsYVKISoaBFoW2PnpYHAzn/XFEJoJEOYU4F+eWXWHwHPhoLOkD9yJSp6Sg0LG/r4+9/Wv" +
				"/YM9XIn9RTj10wo+S6G5tffcW+M2ektoLA9aDIOMHRi5BqZYBA+qLoNhEaIhkkoUF7SIDkKASOKGbQ+V" +
				"KyiDAhld56B3zFZeO7dvX+YUXtL+49MC8+O8H2Z4txxYO/2NDVFYDHnNgfPSK9EE0v4I2w4+RDDcaFfZ" +
				"uzfKgbCJUPvEo00QAC9oUEpeLiH4VrjuHGwPg4Eov2iiZ2VpVChQpcs0JGcokpGpn2U1lU+Jmn0EHlXu" +
				"eQe5BP9LZfEZenPmN0ZyrIBb0g0YIUVnB8LG/VhuInXx56ac1I0UHe08y9Y9yH7AXZkz/kISuG4QMq6V" +
				"nshZNwwgXQSBvDZwIpqXKBkjWeSamSRUU46hLllZ7cHfV/WvvZQ/cfuPXmLfGW1RbjWpNhk8WQHW/NNe" +
				"ozbbP23XJdensbLQQucmK8ArRRSupCC293RGqOVp93BR/tx9YfcDTh5ekuQj2LpI22RTbNl5UECiXRSQ" +
				"N39j316EE5y1OQ9qmKYp9r2VE0qZj848su5cWfECwTrpr0zQUXWyDQW2TyRllU79PLggH4lGiI+X1kGd" +
				"hH5Hfh7tTzBc/A3tEHF7trMGdfJBQiM0c/LuwNB90RxDycjX63Gx06yg7FQWcG+ZCD23s9H3/iSEtreu" +
				"o/9//0luT4+HUW89s2S2p8XHa86b05szKN+jSzMcMwJ8ti3G41Z+mMG662bamthpeK3h93nxgvtL8kXh" +
				"T5iJ+P9D/65PDDj2DtJ3H0cm9/VS1ToyBRxqI7HnwchaTjmFGYt+BbLnvF6ecOaNWIngiLOND7QrHwOJ" +
				"t2WH1exZL/yDfNHd+T0Xj9Yv7oC54PcnleHu+dEGb1t2Y47jscdroIM4/Y5ja59eJLVhJm46HwGp6EB4" +
				"OjjlKEcujO7sChoz0pqcXPP//Rz5dsXzg/ST97tUmXYdJlAhGjbotet02vy5kTu3327G2zYjfqTRstcY" +
				"k6w6YE2444y44rr1h31TXJBw92C60BUw0E/K4T4gUuFFm7ID0M0Nmad9blvOxzKq5Lxg0z6K8BXgXQkI" +
				"YF4ePdyC2I18juRye2pNXIp3cxZSeLbmNTyuVTCy6+uG7Rtf4XX3HnbBmqOkJSg+aFbaGQ2r4RUYQQe0" +
				"Wp8kQjlLzob9v+JOqGG8Gh4LAo/6DGLQIL3qOaM8obqty5W0pWvPrur3+dNtf69lzrxnjLpjhTSoI5M9" +
				"662WbaYtJlAyazYZvNvN1i2Wq2ZBkt6ea4dEt8htWaY7Flxuo26GKT58XtvOLStddc86fcd1rE1jpXMO" +
				"iUFunEfE/5BTyC2g5QKxNvrC6dacN602xAqVhqjL2juIppCmde7svdhvmjSsXH3JUvvGm/7heHplxSdf" +
				"VPu599sTIndbT4r3ygiYdHqGCJ6E7lhyDJER/IXjwC5MRB6mqT75Ab4geJDdxeaowL86Itkkhd0DADab" +
				"t5aXX/rr2F698s/c2vdl+z4DWj4WW96Y96858sCYmmhESbIc2kT4E1mfVZAMiky9Xrcg26XVZTOg6LOd" +
				"VkTNYbN8GmjNYkc1wKwLJYkszmTXHmLcZZGXMtiW+tL8L6oeVC40FAQM3X7/uX+gvtZ5+HvM/lrDpL73" +
				"z0WQcfA51LrowX/Uffb2LT+n/9Sx4aRjQGr3nraof/upf3FPII6laD1OcnOUqdxC8L2RT8qMxLJSfuRo" +
				"89AH0NUIIhqrBOCm2pwklBnbI8OENT68hnh5pSkg89+9Q7t9+SPs+cYovNjp+zw2LbYY3bqTflmG27LZ" +
				"Y9xjk7Lbo91lnvJFjesRq3mo1ZVkua2bbJYFlviltjnf9nCS+rKdNq3mw1bzGbt5lM243GbVbjLpMhy2" +
				"bOsumzLbGpq1fVYBsCSmQYiAcjoGEhgAyfGC8nkRcV4dorx2+/q4Bp/nru2f1/fCJ/21YUh0XzxeOoq2" +
				"385AAfGf6iRE2FaohP6aACjejR0B6TL3SQ2KojyUNpVxIOgI8uB5r3fmwLwSWGPbzJHjqU50hKa37u+f" +
				"yf3fWB1ZpqMWV+I4fJkG4ypuIZVwOaBn2y2ZxssyXpzYlW6w6b4R3j7DW/fS4HDkRL+tXy6on9EUVtNM" +
				"C6Xvt93oWXomM2Mf/a4ecfb01dGeqhbR+TIZpavkh7gugCARQEcXJicNAXoAhcJBC9QcR/RDBBQCAtUR" +
				"eiMyFqxkZ4RVkkZ2vrm68dueeerTdcv+HqBW/Fx28w6jcgWsVZcvSx2d8IWJMY6eCMFBPjrFthZTZLNt" +
				"wTXKafk2bUJy775e7hYdTbsXd/cmElzpAeJ7QvsLxneLD5wF/CLU20KRABFVoSNgFcsH8ffjbJv8T7Ex" +
				"S6vmBocV3pHcK82Oo2KQtB73DQoRHe2Ox4773j69cVPfJQ7g2LNs6zbYg3p8abNtv02xLgHbMyTLPTQT" +
				"362GRMDA6l1yV+U3hZTdsMulTYFPmpEbphKz4xGTJtxh1Wc+Kdd2y0d1MYosFD6v9TnnRif6Rtaz6fbx" +
				"QdC9AYYh8ltKRgkW4hauOtAIzYH5vccHVQNjyYRDNyQJI/4p8kEE5b2ob/8mlj8qaCZ57ad9dtOQsTsk" +
				"yzkudZt9hMm836TER3k36z2bDFYNiq021OMOWa9Yhlm+OtWRZLii0uMW4e+PitbwovizEXzmizpRiNm3" +
				"SxaVbTDiu4z5gVr996/bUbWtBgo+rhZIVncj/oydgXbfCQauG0OQZbAWlz0BhYG7RO+y8cviCEoYhnMD" +
				"t8LJwLxZHjx6Gl+tPTCpY/n/vAAxvjdOnGKzaaZm+Kh6UYksy6DVZDosnwtiF2u1m/w2bOtVi2m8zZBn" +
				"OG0ZpqsqVZDBT1Ia/Nxmz9nHS9IQN/xfFN4YWbwsfj49MQH7FI8dad0KgGw1vXL0jJO+wB6aKCBlEsZI" +
				"GY+z8+/pVeBcjwPxxgYynNkLZxEUNJEszPJ/qxfWo8d3vZileOPvLLPVgiq/FNi2H9XGu6sKAtcy1ZOO" +
				"bbtiNaGWZvsRh2zbXujzO9bzIlmkxJOIzmFJMlHToIkOlN6UbdJszEZs0EuSB4IXLpDdt1+pxvCi+LYS" +
				"dxYlyq2QTjgidmzY5dfdVP1n74bif2Q5EbRsbDERG/ifO/Wtg/IV5e5NcUvAQxwYxEboznkgL/7t321X" +
				"86+vDDOTdd/+erElbHmd/Q61fFWTItyCRgTbpskzHXYt5tMr1nNL5riE0DE4FWLabsObo0gzHTaMmO1a" +
				"fZLMlmY5JevxHcZDCmmy2bycpM2SbTBrN5I3SQwZSkNybpjCkA0WDO+qbwggMKf6S8B7pMPyflhpuT30" +
				"opJhoW4NB+WnQokD5P7jb+BwP7l/zl5e1NI4cPdaWllT355Lu3L04zWf+QMDdptm6NyZZstmUaTVkmSw" +
				"4W32TZbTClGs04IQuHwZqpt2TgmGPJIIPXZRsMWRCERnOyzvy20fa2wbZurmlPnOkdqyEXdmcxQQrhUu" +
				"kmSypxsJAOJjMdRguOdNzrm8IL/AUHjLPREoK54uMyX3jlEDzQH+wmZfNlCWjSvr7ijpzB1ySewi6L7v" +
				"7Q4WNDSclVy397cPHijxISMkGK0D5GQxJ8xAoIdEkIJd+pA8EOYcFoWm8wrrFYwIwIrLnzbB/rdOvwOY" +
				"zIOCcDbAghCnM2GzfMMb8BotTN2Rpn2WWcs/axx7eB4KkAc3IPhtg5NOIvLG7I3XUgJW1/asrhjIyqrM" +
				"zGjKyKDYnHEpMK0zLKk1PLcGRkVadn1CSnVHynjpTk5pSUhrT02uwtdekZdS+9ePDG6xIv/dELV8V/aJ" +
				"qDPDFrwbwMm3Wj1ZyEFNIQC9feMDdupzF2e5wp6aFlmZDUqEnTfvWTe7AvJQbKZVLNRFJJuATUNp7Fge" +
				"IdxDdiJtQlpbvfncOPkpYYM2ZOL8K8tGTwjVXvXH5R6nzLu3H63NjL062G7RZ9jv7KbQnW9+KMew2X7Z" +
				"pn3P3TG5I77CE/1agFR5/cA3iReJrsl9IOS6pOipKX4Hf8WykqTJOlAuz7l8Lkd+qgpuMX2RVImurzHm" +
				"fgSEHfLx7YMOuyFxPM2QmWPYbZiCRJZss6y5W74vTZ1y/c2NkxmZYhPTmV/rYkESTFMJkBSEL07/ZcSL" +
				"Vw6fjy8+/MC+yyk1YUOwm8E2JTIOyfe/A/Djm5DTdfn2aYlYRQCLVli9uWYNx803UbjhzpkRJ7/M/IZK" +
				"X/ZO1LwuiLJFh6QZWBEMrV2NdDlxX/FUf/ZipC7XfrITZeT6aoQjeRr/gDiHg+fwRg8K6B4Op1f7lm0Z" +
				"pY/VuzYjPnznv1vX31opiE+pFocFN3ZbKw9D+CBj1BMjQUduMe6MRJ/EU9dTQzQz4f/Q/Ud8v9/nk8CH" +
				"DY2SN9DpvyiMyEtr5SARIZB1UZCqpHf7P8fcv8N9ZtykNCJ3b2k5z0UgUCjYD2/xEp6YT/ArZgJhyoVw" +
				"yEAAAAAElFTkSuQmCC</Picture><PictureAlign>10</PictureAlign><Font><Name>Times New" +
				" Roman</Name><Size>9</Size></Font></Field><Field><Name>fldCompany</Name><Section" +
				">5</Section><Text>Machino Auto - Parts Co.,Ltd.</Text><Left>3285</Left><Top>675<" +
				"/Top><Width>3330</Width><Height>300</Height><ZOrder>-59</ZOrder><Align>7</Align>" +
				"<MarginLeft>50</MarginLeft><Font><Name>Arial Narrow</Name><Size>12</Size></Font>" +
				"</Field><Field><Name>fldTitle</Name><Section>5</Section><Text>FREIGHT SLIP</Text" +
				"><Left>3285</Left><Top>975</Top><Width>3330</Width><Height>540</Height><Align>7<" +
				"/Align><ForeColor>-2147483630</ForeColor><Font><Bold>-1</Bold><Name>Arial Narrow" +
				"</Name><Size>18</Size></Font></Field><Field><Name>fldCopied</Name><Section>5</Se" +
				"ction><Text>1st copy: Vendor keeps</Text><Left>7545</Left><Top>900</Top><Width>3" +
				"330</Width><Height>300</Height><ZOrder>19</ZOrder><Align>8</Align><MarginRight>1" +
				"00</MarginRight><Visible>0</Visible><Font><Name>Arial Narrow</Name><Size>8.25</S" +
				"ize></Font></Field><Field><Name>fldTitleVN</Name><Section>5</Section><Text>CHI P" +
				"HÍ VẬN CHUYỂN</Text><Left>3570</Left><Top>1395</Top><Width>2760</Width><Height>5" +
				"40</Height><Align>7</Align><ForeColor>-2147483630</ForeColor><Font><Bold>-1</Bol" +
				"d><Name>Arial</Name><Size>9.75</Size></Font></Field><Field><Name>fldSlipNo</Name" +
				"><Section>5</Section><Text>TranNo</Text><Calculated>-1</Calculated><Left>990</Le" +
				"ft><Top>2010</Top><Width>1890</Width><Height>300</Height><ZOrder>-125</ZOrder><A" +
				"lign>6</Align><MarginLeft>50</MarginLeft><WordWrap>0</WordWrap><Font><Name>Arial" +
				" Narrow</Name><Size>9</Size></Font></Field><Field><Name>fldSlipNo1</Name><Sectio" +
				"n>5</Section><Text>PostDate</Text><Calculated>-1</Calculated><Format>dd-MM-yy</F" +
				"ormat><Left>3690</Left><Top>2010</Top><Width>1260</Width><Height>300</Height><ZO" +
				"rder>-124</ZOrder><Align>7</Align><MarginLeft>50</MarginLeft><WordWrap>0</WordWr" +
				"ap><Font><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Field><Name>lblS" +
				"lipNo1</Name><Section>5</Section><Text>Date</Text><Left>2880</Left><Top>2010</To" +
				"p><Width>810</Width><Height>300</Height><ZOrder>-123</ZOrder><Align>6</Align><Ma" +
				"rginLeft>50</MarginLeft><WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name><Si" +
				"ze>9</Size></Font></Field><Field><Name>lblSlipNo2</Name><Section>5</Section><Tex" +
				"t>Exc. Rate</Text><Left>2880</Left><Top>2310</Top><Width>810</Width><Height>300<" +
				"/Height><ZOrder>-123</ZOrder><Align>6</Align><MarginLeft>50</MarginLeft><WordWra" +
				"p>0</WordWrap><Font><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Field" +
				"><Name>lblSlipNo</Name><Section>5</Section><Text>Slip No.</Text><Top>2010</Top><" +
				"Width>990</Width><Height>300</Height><ZOrder>-122</ZOrder><Align>6</Align><Margi" +
				"nLeft>50</MarginLeft><WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name><Size>" +
				"9</Size></Font></Field><Field><Name>lblDeliverSign</Name><Section>5</Section><Le" +
				"ft>4950</Left><Top>2310</Top><Width>1980</Width><Height>1800</Height><ZOrder>-11" +
				"8</ZOrder><Align>3</Align><MarginLeft>50</MarginLeft><MarginBottom>50</MarginBot" +
				"tom><WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name><Size>9</Size></Font></" +
				"Field><Field><Name>lblReceived1</Name><Section>5</Section><Text>Chief Accountant" +
				"</Text><Left>8910</Left><Top>2010</Top><Width>1980</Width><Height>300</Height><Z" +
				"Order>-117</ZOrder><Align>7</Align><Font><Name>Arial Narrow</Name><Size>9</Size>" +
				"</Font></Field><Field><Name>lblReceived</Name><Section>5</Section><Text>Checked " +
				"by</Text><Left>6930</Left><Top>2010</Top><Width>1980</Width><Height>300</Height>" +
				"<ZOrder>-116</ZOrder><Align>7</Align><Font><Name>Arial Narrow</Name><Size>9</Siz" +
				"e></Font></Field><Field><Name>lblDelivered</Name><Section>5</Section><Text>Recor" +
				"ded by</Text><Left>4950</Left><Top>2010</Top><Width>1980</Width><Height>300</Hei" +
				"ght><ZOrder>-115</ZOrder><Align>7</Align><Font><Name>Arial Narrow</Name><Size>9<" +
				"/Size></Font></Field><Field><Name>lblSlipNo3</Name><Section>5</Section><Text>Vat" +
				" (%)</Text><Left>2880</Left><Top>2610</Top><Width>810</Width><Height>300</Height" +
				"><ZOrder>-123</ZOrder><Align>6</Align><MarginLeft>50</MarginLeft><WordWrap>0</Wo" +
				"rdWrap><Font><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Field><Name>" +
				"fldCurrency</Name><Section>5</Section><Text>Currency</Text><Calculated>-1</Calcu" +
				"lated><Left>990</Left><Top>2310</Top><Width>1890</Width><Height>300</Height><ZOr" +
				"der>-114</ZOrder><Align>6</Align><MarginLeft>50</MarginLeft><WordWrap>0</WordWra" +
				"p><Font><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Field><Name>fldTr" +
				"ansNo</Name><Section>5</Section><Text>ReceiveNo</Text><Calculated>-1</Calculated" +
				"><Left>990</Left><Top>2610</Top><Width>1890</Width><Height>300</Height><ZOrder>-" +
				"112</ZOrder><Align>6</Align><MarginLeft>50</MarginLeft><WordWrap>0</WordWrap><Fo" +
				"nt><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Field><Name>fldSlipNo2" +
				"</Name><Section>5</Section><Text>ExchangeRate</Text><Calculated>-1</Calculated><" +
				"Format>#,##0.00</Format><Left>3690</Left><Top>2310</Top><Width>1260</Width><Heig" +
				"ht>300</Height><ZOrder>-109</ZOrder><Align>8</Align><MarginRight>50</MarginRight" +
				"><WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name><Size>9</Size></Font></Fie" +
				"ld><Field><Name>fldSlipNo3</Name><Section>5</Section><Text>VATPercent</Text><Cal" +
				"culated>-1</Calculated><Left>3690</Left><Top>2610</Top><Width>1260</Width><Heigh" +
				"t>300</Height><ZOrder>-109</ZOrder><Align>8</Align><MarginRight>50</MarginRight>" +
				"<WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name><Size>9</Size></Font></Fiel" +
				"d><Field><Name>lblCurrency</Name><Section>5</Section><Text>Currency</Text><Top>2" +
				"310</Top><Width>990</Width><Height>300</Height><ZOrder>-107</ZOrder><Align>6</Al" +
				"ign><MarginLeft>50</MarginLeft><WordWrap>0</WordWrap><Font><Name>Arial Narrow</N" +
				"ame><Size>9</Size></Font></Field><Field><Name>fldVendor</Name><Section>5</Sectio" +
				"n><Text>Vendor</Text><Calculated>-1</Calculated><Left>990</Left><Top>3210</Top><" +
				"Width>3960</Width><Height>300</Height><ZOrder>-127</ZOrder><Align>6</Align><Marg" +
				"inLeft>50</MarginLeft><WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name><Size" +
				">9</Size></Font></Field><Field><Name>fldTransporter</Name><Section>5</Section><T" +
				"ext>Transporter</Text><Calculated>-1</Calculated><Left>990</Left><Top>2910</Top>" +
				"<Width>3960</Width><Height>300</Height><ZOrder>-126</ZOrder><Align>6</Align><Mar" +
				"ginLeft>50</MarginLeft><WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name><Siz" +
				"e>9</Size></Font></Field><Field><Name>lblPlanDate</Name><Section>5</Section><Tex" +
				"t>Transporter</Text><Top>2910</Top><Width>990</Width><Height>300</Height><ZOrder" +
				">-108</ZOrder><Align>6</Align><MarginLeft>50</MarginLeft><WordWrap>0</WordWrap><" +
				"Font><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Field><Name>lblRecei" +
				"vedSign</Name><Section>5</Section><Left>6930</Left><Top>2310</Top><Width>1980</W" +
				"idth><Height>1800</Height><ZOrder>-86</ZOrder><Align>3</Align><MarginLeft>50</Ma" +
				"rginLeft><MarginBottom>50</MarginBottom><WordWrap>0</WordWrap><Font><Name>Arial " +
				"Narrow</Name><Size>9</Size></Font></Field><Field><Name>lblReceivedSign1</Name><S" +
				"ection>5</Section><Left>8910</Left><Top>2310</Top><Width>1980</Width><Height>180" +
				"0</Height><ZOrder>-83</ZOrder><Align>3</Align><MarginLeft>50</MarginLeft><Margin" +
				"Bottom>50</MarginBottom><WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name><Si" +
				"ze>9</Size></Font></Field><Field><Name>lblPurpose</Name><Section>5</Section><Tex" +
				"t>Receive No.</Text><Top>2610</Top><Width>990</Width><Height>300</Height><ZOrder" +
				">-12</ZOrder><Align>6</Align><MarginLeft>50</MarginLeft><WordWrap>0</WordWrap><F" +
				"ont><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Field><Name>fldCostEl" +
				"ement</Name><Section>5</Section><Text>CostElement</Text><Calculated>-1</Calculat" +
				"ed><Left>990</Left><Top>3810</Top><Width>3960</Width><Height>300</Height><ZOrder" +
				">-129</ZOrder><Align>6</Align><MarginLeft>50</MarginLeft><WordWrap>0</WordWrap><" +
				"Font><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Field><Name>fldNote<" +
				"/Name><Section>5</Section><Text>Note</Text><Calculated>-1</Calculated><Left>990<" +
				"/Left><Top>3510</Top><Width>3960</Width><Height>300</Height><ZOrder>-128</ZOrder" +
				"><Align>6</Align><MarginLeft>50</MarginLeft><WordWrap>0</WordWrap><Font><Name>Ar" +
				"ial Narrow</Name><Size>9</Size></Font></Field><Field><Name>fldScheduleDate</Name" +
				"><Section>5</Section><Text>Note</Text><Top>3510</Top><Width>990</Width><Height>3" +
				"00</Height><ZOrder>-104</ZOrder><Align>6</Align><MarginLeft>50</MarginLeft><Word" +
				"Wrap>0</WordWrap><Font><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Fi" +
				"eld><Name>lblActDate</Name><Section>5</Section><Text>Vendor</Text><Top>3210</Top" +
				"><Width>990</Width><Height>300</Height><ZOrder>-81</ZOrder><Align>6</Align><Marg" +
				"inLeft>50</MarginLeft><WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name><Size" +
				">9</Size></Font></Field><Field><Name>fldCustName</Name><Section>5</Section><Text" +
				">Cost Element</Text><Top>3810</Top><Width>990</Width><Height>300</Height><ZOrder" +
				">-93</ZOrder><Align>6</Align><MarginLeft>50</MarginLeft><WordWrap>0</WordWrap><F" +
				"ont><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Field><Name>Field18</" +
				"Name><Section>5</Section><Left>6930</Left><Top>2010</Top><Height>2100</Height><B" +
				"orderStyle>3</BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>Field16</" +
				"Name><Section>5</Section><Left>8910</Left><Top>2010</Top><Height>2100</Height><B" +
				"orderStyle>3</BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>div4</Nam" +
				"e><Section>5</Section><Top>4110</Top><Width>10890</Width><BorderStyle>1</BorderS" +
				"tyle><LineSlant>1</LineSlant></Field><Field><Name>lblNo</Name><Section>5</Sectio" +
				"n><Text>No</Text><Top>4245</Top><Width>450</Width><Height>300</Height><ZOrder>-1" +
				"11</ZOrder><Align>7</Align><WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name>" +
				"<Size>9</Size></Font></Field><Field><Name>lblRemark</Name><Section>5</Section><T" +
				"ext>Freight</Text><Left>8730</Left><Top>4245</Top><Width>2160</Width><Height>300" +
				"</Height><ZOrder>-25</ZOrder><Align>7</Align><WordWrap>0</WordWrap><Font><Name>A" +
				"rial Narrow</Name><Size>9</Size></Font></Field><Field><Name>lblModel</Name><Sect" +
				"ion>5</Section><Text>Model</Text><Left>5175</Left><Top>4245</Top><Width>1035</Wi" +
				"dth><Height>300</Height><ZOrder>-24</ZOrder><Align>7</Align><WordWrap>0</WordWra" +
				"p><Font><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Field><Name>lblQt" +
				"yPlan</Name><Section>5</Section><Text>Quantity</Text><Left>7155</Left><Top>4245<" +
				"/Top><Width>1575</Width><Height>300</Height><ZOrder>-21</ZOrder><Align>7</Align>" +
				"<WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name><Size>9</Size></Font></Fiel" +
				"d><Field><Name>lblPartName</Name><Section>5</Section><Text>Parts Name</Text><Lef" +
				"t>2160</Left><Top>4245</Top><Width>3015</Width><Height>300</Height><ZOrder>-20</" +
				"ZOrder><Align>7</Align><WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name><Siz" +
				"e>9</Size></Font></Field><Field><Name>lblPartNumber</Name><Section>5</Section><T" +
				"ext>Parts Number</Text><Left>450</Left><Top>4245</Top><Width>1710</Width><Height" +
				">300</Height><ZOrder>-19</ZOrder><Align>7</Align><WordWrap>0</WordWrap><Font><Na" +
				"me>Arial Narrow</Name><Size>9</Size></Font></Field><Field><Name>lblUnit</Name><S" +
				"ection>5</Section><Text>Buying UM</Text><Left>6210</Left><Top>4245</Top><Width>9" +
				"45</Width><Height>300</Height><ZOrder>-18</ZOrder><Align>7</Align><WordWrap>0</W" +
				"ordWrap><Font><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Field><Name" +
				">div5</Name><Section>5</Section><Top>4245</Top><Width>10890</Width><BorderStyle>" +
				"1</BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>Field46</Name><Secti" +
				"on>5</Section><Left>450</Left><Top>4245</Top><Height>300</Height><BorderStyle>3<" +
				"/BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>Field20</Name><Section" +
				">5</Section><Left>2160</Left><Top>4245</Top><Height>300</Height><BorderStyle>3</" +
				"BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>Field21</Name><Section>" +
				"5</Section><Left>5175</Left><Top>4245</Top><Height>300</Height><BorderStyle>3</B" +
				"orderStyle><LineSlant>1</LineSlant></Field><Field><Name>Field22</Name><Section>5" +
				"</Section><Left>7155</Left><Top>4245</Top><Height>300</Height><BorderStyle>3</Bo" +
				"rderStyle><LineSlant>1</LineSlant></Field><Field><Name>Field23</Name><Section>5<" +
				"/Section><Left>8730</Left><Top>4245</Top><Height>300</Height><BorderStyle>3</Bor" +
				"derStyle><LineSlant>1</LineSlant></Field><Field><Name>Field25</Name><Section>5</" +
				"Section><Left>6210</Left><Top>4245</Top><Height>300</Height><BorderStyle>3</Bord" +
				"erStyle><LineSlant>1</LineSlant></Field><Field><Name>Field43</Name><Section>5</S" +
				"ection><Top>4545</Top><Width>10890</Width><Align>3</Align><BorderStyle>3</Border" +
				"Style><LineSlant>3</LineSlant></Field><Field><Name>Field</Name><Section>5</Secti" +
				"on><Left>10890</Left><Top>570</Top><Height>3975</Height><ZOrder>30</ZOrder><Bord" +
				"erStyle>1</BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>Field2</Name" +
				"><Section>5</Section><Top>570</Top><Height>3975</Height><ZOrder>33</ZOrder><Bord" +
				"erStyle>1</BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>div3</Name><" +
				"Section>5</Section><Top>2010</Top><Width>10890</Width><ZOrder>36</ZOrder><Border" +
				"Style>1</BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>Field42</Name>" +
				"<Section>5</Section><Left>990</Left><Top>2010</Top><Height>2100</Height><ZOrder>" +
				"37</ZOrder><BorderStyle>3</BorderStyle><LineSlant>1</LineSlant></Field><Field><N" +
				"ame>Field5</Name><Section>5</Section><Left>2880</Left><Top>2010</Top><Height>900" +
				"</Height><ZOrder>38</ZOrder><BorderStyle>3</BorderStyle><LineSlant>1</LineSlant>" +
				"</Field><Field><Name>Field9</Name><Section>5</Section><Left>3690</Left><Top>2010" +
				"</Top><Height>900</Height><ZOrder>38</ZOrder><BorderStyle>3</BorderStyle><LineSl" +
				"ant>1</LineSlant></Field><Field><Name>Field17</Name><Section>5</Section><Left>49" +
				"50</Left><Top>2010</Top><Height>2100</Height><ZOrder>38</ZOrder><BorderStyle>3</" +
				"BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>Field10</Name><Section>" +
				"5</Section><Top>2310</Top><Width>10890</Width><ZOrder>39</ZOrder><Align>3</Align" +
				"><BorderStyle>3</BorderStyle><LineSlant>3</LineSlant></Field><Field><Name>Field6" +
				"</Name><Section>5</Section><Top>2610</Top><Width>4950</Width><ZOrder>42</ZOrder>" +
				"<BorderStyle>3</BorderStyle><LineSlant>1</LineSlant><Font><Name>Arial Narrow</Na" +
				"me><Size>9</Size></Font></Field><Field><Name>Field7</Name><Section>5</Section><T" +
				"op>2910</Top><Width>4950</Width><ZOrder>41</ZOrder><Align>3</Align><BorderStyle>" +
				"3</BorderStyle><LineSlant>3</LineSlant></Field><Field><Name>Field8</Name><Sectio" +
				"n>5</Section><Top>3210</Top><Width>4950</Width><ZOrder>40</ZOrder><Align>3</Alig" +
				"n><BorderStyle>3</BorderStyle><LineSlant>3</LineSlant></Field><Field><Name>Field" +
				"11</Name><Section>5</Section><Top>3510</Top><Width>4950</Width><ZOrder>43</ZOrde" +
				"r><Align>3</Align><BorderStyle>3</BorderStyle><LineSlant>3</LineSlant></Field><F" +
				"ield><Name>Field47</Name><Section>5</Section><Top>3810</Top><Width>4950</Width><" +
				"ZOrder>44</ZOrder><Align>3</Align><BorderStyle>3</BorderStyle><LineSlant>3</Line" +
				"Slant></Field><Field><Name>div</Name><Section>6</Section><Width>10890</Width><ZO" +
				"rder>35</ZOrder><BorderStyle>1</BorderStyle><LineSlant>1</LineSlant></Field><Fie" +
				"ld><Name>divend</Name><Section>6</Section><Top>1320</Top><Width>10890</Width><Bo" +
				"rderStyle>4</BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>lblQtyPlan" +
				"1</Name><Section>6</Section><Text>Sub-Total</Text><Left>7155</Left><Top>120</Top" +
				"><Width>1575</Width><Height>300</Height><ZOrder>-21</ZOrder><Align>6</Align><Mar" +
				"ginLeft>50</MarginLeft><WordWrap>0</WordWrap><Font><Name>Arial Narrow</Name><Siz" +
				"e>9</Size></Font></Field><Field><Name>lblQtyPlan2</Name><Section>6</Section><Tex" +
				"t>Vat Amount</Text><Left>7155</Left><Top>420</Top><Width>1575</Width><Height>300" +
				"</Height><ZOrder>-21</ZOrder><Align>6</Align><MarginLeft>50</MarginLeft><WordWra" +
				"p>0</WordWrap><Font><Name>Arial Narrow</Name><Size>9</Size></Font></Field><Field" +
				"><Name>lblQtyPlan3</Name><Section>6</Section><Text>Total of Payment</Text><Left>" +
				"7155</Left><Top>720</Top><Width>1575</Width><Height>300</Height><ZOrder>-21</ZOr" +
				"der><Align>6</Align><MarginLeft>50</MarginLeft><WordWrap>0</WordWrap><Font><Name" +
				">Arial Narrow</Name><Size>9</Size></Font></Field><Field><Name>fldSubTotal</Name>" +
				"<Section>6</Section><Text>SubTotal</Text><Calculated>-1</Calculated><Format>#,##" +
				"0.00</Format><Left>8730</Left><Top>120</Top><Width>2160</Width><Height>300</Heig" +
				"ht><ZOrder>-61</ZOrder><Align>8</Align><MarginRight>50</MarginRight><WordWrap>0<" +
				"/WordWrap><CanGrow>-1</CanGrow><CanShrink>-1</CanShrink><Anchor>2</Anchor><Font>" +
				"<Name>Arial Narrow</Name><Size>6.75</Size></Font></Field><Field><Name>fldVATAmou" +
				"nt</Name><Section>6</Section><Text>TotalVAT</Text><Calculated>-1</Calculated><Fo" +
				"rmat>#,##0.00</Format><Left>8730</Left><Top>420</Top><Width>2160</Width><Height>" +
				"300</Height><ZOrder>-61</ZOrder><Align>8</Align><MarginRight>50</MarginRight><Wo" +
				"rdWrap>0</WordWrap><CanGrow>-1</CanGrow><CanShrink>-1</CanShrink><Anchor>2</Anch" +
				"or><Font><Name>Arial Narrow</Name><Size>6.75</Size></Font></Field><Field><Name>f" +
				"ldGrandTotal</Name><Section>6</Section><Text>GrandTotal</Text><Calculated>-1</Ca" +
				"lculated><Format>#,##0.00</Format><Left>8730</Left><Top>720</Top><Width>2160</Wi" +
				"dth><Height>300</Height><ZOrder>-61</ZOrder><Align>8</Align><MarginRight>50</Mar" +
				"ginRight><WordWrap>0</WordWrap><CanGrow>-1</CanGrow><CanShrink>-1</CanShrink><An" +
				"chor>2</Anchor><Font><Name>Arial Narrow</Name><Size>6.75</Size></Font></Field><F" +
				"ield><Name>Field12</Name><Section>6</Section><Left>7155</Left><Top>420</Top><Wid" +
				"th>3735</Width><BorderStyle>1</BorderStyle><LineSlant>1</LineSlant></Field><Fiel" +
				"d><Name>Field13</Name><Section>6</Section><Left>7155</Left><Top>720</Top><Width>" +
				"3735</Width><BorderStyle>1</BorderStyle><LineSlant>1</LineSlant></Field><Field><" +
				"Name>Field14</Name><Section>6</Section><Left>7155</Left><Top>1020</Top><Width>37" +
				"35</Width><BorderStyle>1</BorderStyle><LineSlant>1</LineSlant></Field><Field><Na" +
				"me>Field15</Name><Section>6</Section><Left>10890</Left><Height>1215</Height><ZOr" +
				"der>30</ZOrder><BorderStyle>1</BorderStyle><LineSlant>1</LineSlant></Field><Fiel" +
				"d><Name>Field19</Name><Section>6</Section><Height>1215</Height><ZOrder>30</ZOrde" +
				"r><BorderStyle>1</BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>diven" +
				"d1</Name><Section>6</Section><Top>1215</Top><Width>10890</Width><BorderStyle>1</" +
				"BorderStyle><LineSlant>1</LineSlant></Field><Field><Name>Field24</Name><Section>" +
				"4</Section><Width>10890</Width><BorderStyle>1</BorderStyle><LineSlant>1</LineSla" +
				"nt></Field><Field><Name>lblPrinted</Name><Section>4</Section><Text>fldTitle + \" " +
				" printed on: \" + Format(Now(),\"dd-MM-yyyy\") + \"  \" + Format(Now(),\"Long Time\")</" +
				"Text><Calculated>-1</Calculated><Width>11250</Width><Height>300</Height><ZOrder>" +
				"-250</ZOrder><Align>3</Align><Font><Name>Arial Narrow</Name><Size>8.25</Size></F" +
				"ont></Field></Fields></Report>";
			this.rptFreightSlip.ReportName = "FreightSlip";
			// 
			// lblPurpose
			// 
			this.lblPurpose.ForeColor = System.Drawing.Color.Maroon;
			this.lblPurpose.Location = new System.Drawing.Point(4, 48);
			this.lblPurpose.Name = "lblPurpose";
			this.lblPurpose.Size = new System.Drawing.Size(60, 23);
			this.lblPurpose.TabIndex = 12;
			this.lblPurpose.Text = "Purpose";
			this.lblPurpose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblObject
			// 
			this.lblObject.ForeColor = System.Drawing.Color.Maroon;
			this.lblObject.Location = new System.Drawing.Point(4, 70);
			this.lblObject.Name = "lblObject";
			this.lblObject.Size = new System.Drawing.Size(38, 23);
			this.lblObject.TabIndex = 15;
			this.lblObject.Text = "Object";
			this.lblObject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtMaker
			// 
			this.txtMaker.AccessibleDescription = "";
			this.txtMaker.AccessibleName = "";
			this.txtMaker.Location = new System.Drawing.Point(72, 141);
			this.txtMaker.MaxLength = 20;
			this.txtMaker.Name = "txtMaker";
			this.txtMaker.Size = new System.Drawing.Size(96, 20);
			this.txtMaker.TabIndex = 30;
			this.txtMaker.Text = "";
			this.txtMaker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaker_KeyDown);
			this.txtMaker.Validating += new System.ComponentModel.CancelEventHandler(this.txtMaker_Validating);
			// 
			// txtMakerName
			// 
			this.txtMakerName.AccessibleDescription = "";
			this.txtMakerName.AccessibleName = "";
			this.txtMakerName.Location = new System.Drawing.Point(196, 141);
			this.txtMakerName.Name = "txtMakerName";
			this.txtMakerName.Size = new System.Drawing.Size(204, 20);
			this.txtMakerName.TabIndex = 32;
			this.txtMakerName.Text = "";
			this.txtMakerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMakerName_KeyDown);
			this.txtMakerName.Validating += new System.ComponentModel.CancelEventHandler(this.txtMakerName_Validating);
			// 
			// btnMaker
			// 
			this.btnMaker.AccessibleDescription = "";
			this.btnMaker.AccessibleName = "";
			this.btnMaker.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnMaker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnMaker.Location = new System.Drawing.Point(170, 141);
			this.btnMaker.Name = "btnMaker";
			this.btnMaker.Size = new System.Drawing.Size(24, 20);
			this.btnMaker.TabIndex = 31;
			this.btnMaker.Text = "...";
			this.btnMaker.Click += new System.EventHandler(this.btnMaker_Click);
			// 
			// btnMakerName
			// 
			this.btnMakerName.AccessibleDescription = "";
			this.btnMakerName.AccessibleName = "";
			this.btnMakerName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnMakerName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnMakerName.Location = new System.Drawing.Point(402, 141);
			this.btnMakerName.Name = "btnMakerName";
			this.btnMakerName.Size = new System.Drawing.Size(24, 20);
			this.btnMakerName.TabIndex = 33;
			this.btnMakerName.Text = "...";
			this.btnMakerName.Click += new System.EventHandler(this.btnMakerName_Click);
			// 
			// lblMaker
			// 
			this.lblMaker.AccessibleDescription = "";
			this.lblMaker.AccessibleName = "";
			this.lblMaker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblMaker.ForeColor = System.Drawing.Color.Black;
			this.lblMaker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblMaker.Location = new System.Drawing.Point(4, 141);
			this.lblMaker.Name = "lblMaker";
			this.lblMaker.Size = new System.Drawing.Size(73, 20);
			this.lblMaker.TabIndex = 29;
			this.lblMaker.Text = "Maker";
			this.lblMaker.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtPurpose
			// 
			this.txtPurpose.AccessibleDescription = "";
			this.txtPurpose.AccessibleName = "";
			this.txtPurpose.Location = new System.Drawing.Point(72, 50);
			this.txtPurpose.MaxLength = 20;
			this.txtPurpose.Name = "txtPurpose";
			this.txtPurpose.Size = new System.Drawing.Size(154, 20);
			this.txtPurpose.TabIndex = 13;
			this.txtPurpose.Text = "";
			this.txtPurpose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPurpose_KeyDown);
			this.txtPurpose.Validating += new System.ComponentModel.CancelEventHandler(this.txtPurpose_Validating);
			// 
			// btnPurpose
			// 
			this.btnPurpose.AccessibleDescription = "";
			this.btnPurpose.AccessibleName = "";
			this.btnPurpose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPurpose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPurpose.Location = new System.Drawing.Point(226, 50);
			this.btnPurpose.Name = "btnPurpose";
			this.btnPurpose.Size = new System.Drawing.Size(24, 20);
			this.btnPurpose.TabIndex = 14;
			this.btnPurpose.Text = "...";
			this.btnPurpose.Click += new System.EventHandler(this.btnPurpose_Click);
			// 
			// txtObject
			// 
			this.txtObject.AccessibleDescription = "";
			this.txtObject.AccessibleName = "";
			this.txtObject.Location = new System.Drawing.Point(72, 73);
			this.txtObject.MaxLength = 20;
			this.txtObject.Name = "txtObject";
			this.txtObject.Size = new System.Drawing.Size(154, 20);
			this.txtObject.TabIndex = 16;
			this.txtObject.Text = "";
			this.txtObject.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtObject_KeyDown);
			this.txtObject.Validating += new System.ComponentModel.CancelEventHandler(this.txtObject_Validating);
			// 
			// btnObject
			// 
			this.btnObject.AccessibleDescription = "";
			this.btnObject.AccessibleName = "";
			this.btnObject.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnObject.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnObject.Location = new System.Drawing.Point(226, 73);
			this.btnObject.Name = "btnObject";
			this.btnObject.Size = new System.Drawing.Size(24, 20);
			this.btnObject.TabIndex = 17;
			this.btnObject.Text = "...";
			this.btnObject.Click += new System.EventHandler(this.btnObject_Click);
			// 
			// txtPONo
			// 
			this.txtPONo.AccessibleDescription = "";
			this.txtPONo.AccessibleName = "";
			this.txtPONo.Enabled = false;
			this.txtPONo.Location = new System.Drawing.Point(72, 232);
			this.txtPONo.MaxLength = 20;
			this.txtPONo.Name = "txtPONo";
			this.txtPONo.ReadOnly = true;
			this.txtPONo.Size = new System.Drawing.Size(121, 20);
			this.txtPONo.TabIndex = 46;
			this.txtPONo.Text = "";
			// 
			// label2
			// 
			this.label2.AccessibleDescription = "";
			this.label2.AccessibleName = "";
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(4, 234);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 20);
			this.label2.TabIndex = 45;
			this.label2.Text = "PO No.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtInvoice
			// 
			this.txtInvoice.AccessibleDescription = "";
			this.txtInvoice.AccessibleName = "";
			this.txtInvoice.Enabled = false;
			this.txtInvoice.Location = new System.Drawing.Point(72, 210);
			this.txtInvoice.MaxLength = 20;
			this.txtInvoice.Name = "txtInvoice";
			this.txtInvoice.ReadOnly = true;
			this.txtInvoice.Size = new System.Drawing.Size(121, 20);
			this.txtInvoice.TabIndex = 44;
			this.txtInvoice.Text = "";
			// 
			// label1
			// 
			this.label1.AccessibleDescription = "";
			this.label1.AccessibleName = "";
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(4, 212);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 20);
			this.label1.TabIndex = 43;
			this.label1.Text = "Invoice No.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Freight
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(632, 510);
			this.Controls.Add(this.txtPONo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtInvoice);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtObject);
			this.Controls.Add(this.btnObject);
			this.Controls.Add(this.txtPurpose);
			this.Controls.Add(this.btnPurpose);
			this.Controls.Add(this.txtMaker);
			this.Controls.Add(this.txtMakerName);
			this.Controls.Add(this.btnMaker);
			this.Controls.Add(this.btnMakerName);
			this.Controls.Add(this.lblMaker);
			this.Controls.Add(this.lblObject);
			this.Controls.Add(this.lblPurpose);
			this.Controls.Add(this.btnAllocate);
			this.Controls.Add(this.txtNetAmount);
			this.Controls.Add(this.txtInvoiceNo);
			this.Controls.Add(this.txtVendorCode);
			this.Controls.Add(this.txtOrderNo);
			this.Controls.Add(this.txtTransporterCode);
			this.Controls.Add(this.txtNote);
			this.Controls.Add(this.txtVendorName);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.txtCurrency);
			this.Controls.Add(this.txtTransporterName);
			this.Controls.Add(this.txtGrandTotal);
			this.Controls.Add(this.lblGrandToTal);
			this.Controls.Add(this.txtSubTotal);
			this.Controls.Add(this.lblSubTotal);
			this.Controls.Add(this.txtTotalVAT);
			this.Controls.Add(this.lblTotalVAT);
			this.Controls.Add(this.txtVAT);
			this.Controls.Add(this.dtmPostDate);
			this.Controls.Add(this.lblVAT);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.txtExchRate);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnTransporterName);
			this.Controls.Add(this.btnOrderNo);
			this.Controls.Add(this.btnTransporterCode);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblExchRate);
			this.Controls.Add(this.btnVendorCode);
			this.Controls.Add(this.btnVendorName);
			this.Controls.Add(this.btnCurrency);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCurrency);
			this.Controls.Add(this.lblNetAmount);
			this.Controls.Add(this.btnInvoiceNo);
			this.Controls.Add(this.lblNote);
			this.Controls.Add(this.lblTransporter);
			this.Controls.Add(this.lblTransNo);
			this.Controls.Add(this.lblVendor);
			this.Controls.Add(this.lblPostDate);
			this.Controls.Add(this.lblInvoiceNo);
			this.KeyPreview = true;
			this.Name = "Freight";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Addition Charge";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Freight_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Freight_Closing);
			this.Load += new System.EventHandler(this.Freight_Load);
			((System.ComponentModel.ISupportInitialize)(this.txtExchRate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmPostDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtVAT)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtGrandTotal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSubTotal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalVAT)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNetAmount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rptFreightSlip)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// Freight_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 23 2006</date>
		private void Freight_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".Freight_Load()";
			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					return;
				}
				
				//Load CCN and set default CCN
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
				
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				
				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
				mFormMode = EnumAction.Default;
				ClearForm();
				SwitchFormMode();
				
				btnEdit.Enabled = false;
				btnDelete.Enabled = false;
				

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
		/// CreateDataSet
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void CreateDataSet()
		{
			const string METHOD_NAME = THIS + ".CreateDataSet()";
			try
			{
				dstGridData = new DataSet();
				dstGridData.Tables.Add(cst_FreightDetailTable.TABLE_NAME);

				//insert columns which is invisible but use to update
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.PRODUCTID_FLD);
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(cst_FreightDetailTable.BUYINGUMID_FLD);
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(cst_FreightDetailTable.FREIGHTMASTERID_FLD);
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.VAT_FLD);
//				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(MST_BINTable.BINID_FLD);
				//insert display columns
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.CODE_FLD);
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.DESCRIPTION_FLD);
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(ITM_ProductTable.REVISION_FLD);
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD);
				
//				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD);
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(cst_FreightDetailTable.QUANTITY_FLD, typeof(Decimal));
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(cst_FreightDetailTable.UNITPRICECIF_FLD, typeof(Decimal));
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(PO_PurchaseOrderDetailTable.IMPORTTAX_FLD, typeof(decimal));
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(AMOUNT_COLUMN, typeof(Decimal));
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(VAT_AMOUNT_COLUMN, typeof(Decimal));
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(TOTAL_AMOUNT_COLUMN, typeof(Decimal));
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(IV_AdjustmentTable.TRANSNO_FLD, typeof(string));
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(IV_AdjustmentTable.ADJUSTMENTID_FLD, typeof(int));
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(PO_InvoiceMasterTable.INVOICENO_FLD, typeof(string));
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(PO_InvoiceMasterTable.INVOICEMASTERID_FLD, typeof(int));
				dstGridData.Tables[cst_FreightDetailTable.TABLE_NAME].Columns.Add(cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD, typeof(int));
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
		/// clear all control values
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void ClearForm()
		{
			const string METHOD_NAME = THIS + ".ClearForm()";
			try
			{
				dtmPostDate.Value = null;
				txtCurrency.Text = string.Empty;
				txtCurrency.Tag = null;
				txtOrderNo.Tag = null;
				txtOrderNo.Text = string.Empty;
				txtExchRate.Value = null;
				txtTransporterCode.Text = string.Empty;
				txtTransporterCode.Tag = null;
				txtTransporterName.Text = string.Empty;
				txtVendorCode.Tag = null;
				txtVendorCode.Text = string.Empty;
				txtVendorName.Text = string.Empty;
				txtNote.Text = string.Empty;
				txtNetAmount.Value = null;
				txtVAT.Value = null;
				txtInvoiceNo.Text = string.Empty;
				txtInvoiceNo.Tag = null;
				txtTotalVAT.Value = null;
				txtPONo.Text = string.Empty;
				txtInvoice.Text = string.Empty;
				txtSubTotal.Value = null;
				txtGrandTotal.Value = null;
				txtMaker.Text = string.Empty;
				txtMakerName.Text = string.Empty;
				txtObject.Text = string.Empty;
				txtPurpose.Text = String.Empty;
				//clear grid
				CreateDataSet();
				dgrdData.DataSource = dstGridData.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
				ClearAdjustmentID();
				dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
				ClearInvoiceMasterID();
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
		/// <date>Thursday, Feb 23 2006</date>
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
				dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.QUANTITY_FLD].Locked = pblnLock;
                dgrdData.Splits[0].DisplayColumns[AMOUNT_COLUMN].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[VAT_AMOUNT_COLUMN].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Locked = pblnLock;
				dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.QUANTITY_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[AMOUNT_COLUMN].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[TOTAL_AMOUNT_COLUMN].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.QUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[AMOUNT_COLUMN].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[TOTAL_AMOUNT_COLUMN].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[VAT_AMOUNT_COLUMN].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[VAT_AMOUNT_COLUMN].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				
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
		/// <date>Thursday, Feb 23 2006</date>
		private void SwitchFormMode()
		{
			const string METHOD_NAME = THIS + ".SwitchFormMode()";
			try
			{
				switch (mFormMode)
				{
					case EnumAction.Default:
						btnAdd.Enabled = true;
						btnSave.Enabled = false;
						btnEdit.Enabled = true;
						btnDelete.Enabled = true;
						dtmPostDate.Enabled = false;
						btnTransporterCode.Enabled = false;
						btnTransporterName.Enabled = false;
						btnVendorCode.Enabled = false;
						btnInvoiceNo.Enabled = false;
						btnCurrency.Enabled = false;
						
						btnOrderNo.Enabled = true;
						btnVendorName.Enabled = false;
						btnAllocate.Enabled = false;
						txtCurrency.Enabled = false;
						txtExchRate.Enabled = false;
						txtGrandTotal.Enabled = false;
						txtInvoiceNo.Enabled = false;
						txtNetAmount.Enabled = false;
						
						txtNote.Enabled = false;
						txtOrderNo.Enabled = true;
						txtSubTotal.Enabled = false;
						txtTotalVAT.Enabled = false;
						txtTransporterCode.Enabled = false;
						txtTransporterName.Enabled = false;
						txtVAT.Enabled = false;
						txtVendorCode.Enabled = false;
						txtVendorName.Enabled = false;
						dgrdData.AllowDelete = false;
						btnPrint.Enabled = true;
						btnPurpose.Enabled =false;
						btnObject.Enabled =false;
						btnMaker.Enabled =false;
						btnMakerName.Enabled =false;
						txtMaker.Enabled = false;
						txtMakerName.Enabled = false;
						txtObject.Enabled = false;
						txtPurpose.Enabled = false;
						ConfigGrid(true);
						if (voFreightMaster != null && voFreightMaster.FreightMasterID > 0)
							btnPrint.Enabled = true;
						break;
					case EnumAction.Add:
						btnAdd.Enabled = false;
						btnSave.Enabled = true;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						dtmPostDate.Enabled = true;
						btnTransporterCode.Enabled = true;
						btnTransporterName.Enabled = true;
						btnVendorCode.Enabled = true;
						btnInvoiceNo.Enabled = true;
						btnOrderNo.Enabled = false;
						btnCurrency.Enabled = true;
						btnAllocate.Enabled = true;
						btnVendorName.Enabled = true;
						txtCurrency.Enabled = true;
						txtExchRate.Enabled = true;
						txtGrandTotal.Enabled = true;
						txtInvoiceNo.Enabled = true;
						txtNetAmount.Enabled = true;
						txtNote.Enabled = true;
						txtOrderNo.Enabled = true;
						txtSubTotal.Enabled = true;
						txtTotalVAT.Enabled = true;
						txtTransporterCode.Enabled = true;
						txtTransporterName.Enabled = true;
						txtVAT.Enabled = true;
						txtVendorCode.Enabled = true;
						txtVendorName.Enabled = true;
						dgrdData.AllowDelete = true;

						btnPurpose.Enabled =true;
						btnObject.Enabled =true;
						btnMaker.Enabled =true;
						btnMakerName.Enabled =true;
						txtMaker.Enabled = true;
						txtMakerName.Enabled = true;
						txtObject.Enabled = true;
						txtPurpose.Enabled = true;

						btnPrint.Enabled = false; // edited dungla: unable to view slip while in add mode
						ConfigGrid(false);
						dgrdData.AllowDelete = true;
						break;
					case EnumAction.Edit:
						
						btnAdd.Enabled = false;
						btnSave.Enabled = true;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						dtmPostDate.Enabled = true;
						btnTransporterCode.Enabled = true;
						btnTransporterName.Enabled = true;
						btnVendorCode.Enabled = true;
						btnInvoiceNo.Enabled = true;
						btnOrderNo.Enabled = false;
						btnVendorName.Enabled = true;
						btnCurrency.Enabled = true;
						btnAllocate.Enabled = true;
						txtCurrency.Enabled = true;
						txtExchRate.Enabled = true;
						txtGrandTotal.Enabled = true;
						txtInvoiceNo.Enabled = true;
						txtNetAmount.Enabled = true;
						txtNote.Enabled = true;
						txtOrderNo.Enabled = true;
						txtSubTotal.Enabled = true;
						txtTotalVAT.Enabled = true;
						txtTransporterCode.Enabled = true;
						txtTransporterName.Enabled = true;
						txtVAT.Enabled = true;
						txtVendorCode.Enabled = true;
						txtVendorName.Enabled = true;

						btnPurpose.Enabled =true;
						btnObject.Enabled =true;
						btnMaker.Enabled =true;
						btnMakerName.Enabled =true;
						txtMaker.Enabled = true;
						txtMakerName.Enabled = true;
						txtObject.Enabled = true;
						txtPurpose.Enabled = true;

						btnPrint.Enabled = false; // edited by dungla: unable to view slip while in edit mode
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
		/// <date>Friday, Feb 24 2006</date>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				mFormMode = EnumAction.Add;
				SwitchFormMode();
				ClearForm();
				//InitForm
				dtmPostDate.Value = new UtilsBO().GetDBDate();
				txtOrderNo.Text = FormControlComponents.GetNoByMask(this);
//				txtCurrency.Text = SystemProperty.HomeCurrency;
//				txtCurrency.Tag = SystemProperty.HomeCurrencyID;
//				txtExchRate.Value = 1;
				txtExchRate.Enabled = false;
				//Reset FreightMasterID 
				intFreightMasterID = 0;
				dtmPostDate.Focus();
				dgrdData.AllowAddNew = false;
				dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Button = true;
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
		/// btnCurrency_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
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
					if (drwResult[MST_CurrencyTable.CURRENCYID_FLD].ToString() != SystemProperty.HomeCurrencyID.ToString())
					{
						txtExchRate.Value = null;
						txtExchRate.Enabled = true;
					}
					else
					{
						txtExchRate.Value = 1;
						txtExchRate.Enabled = false;
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
		/// Fill Data To Control by MasterID
		/// </summary>
		/// <param name="pintFreightMasterID"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void FillDataByMasterID(int pintFreightMasterID)
		{
			const string METHOD_NAME = THIS + ".FillDataByMasterID()";
			try
			{	
				FreightBO boFreight = new FreightBO();
				//Get Master
				DataTable dtbFreightMaster = new DataTable();
				dtbFreightMaster = boFreight.GetFreightMaster(pintFreightMasterID);
				if (dtbFreightMaster.Rows.Count > 0)
				{
					txtInvoiceNo.Text = string.Empty;
					txtInvoice.Text = string.Empty;
					txtPONo.Text = string.Empty;
					dtmPostDate.Value = (DateTime) dtbFreightMaster.Rows[0][cst_FreightMasterTable.POSTDATE_FLD];
					txtCurrency.Text = dtbFreightMaster.Rows[0][MST_CurrencyTable.TABLE_NAME + MST_CurrencyTable.CODE_FLD].ToString();
					txtCurrency.Tag = dtbFreightMaster.Rows[0][cst_FreightMasterTable.CURRENCYID_FLD];
					txtTransporterCode.Tag = dtbFreightMaster.Rows[0][cst_FreightMasterTable.TRANSPORTERID_FLD];
					txtTransporterCode.Text = dtbFreightMaster.Rows[0][TRANSPORTERCODE].ToString();
					txtTransporterName.Text = dtbFreightMaster.Rows[0][TRANSPORTERNAME].ToString();
					txtVendorCode.Tag = dtbFreightMaster.Rows[0][cst_FreightMasterTable.VENDORID_FLD];
					txtVendorCode.Text = dtbFreightMaster.Rows[0][VENDORCODE].ToString();
					txtVendorName.Text = dtbFreightMaster.Rows[0][VENDORNAME].ToString();
					txtMaker.Text = dtbFreightMaster.Rows[0]["MakerCode"].ToString();
					txtMaker.Tag = dtbFreightMaster.Rows[0][cst_FreightMasterTable.MAKERID_FLD];
                    txtMakerName.Text = dtbFreightMaster.Rows[0]["MakerName"].ToString();
					txtNote.Text = dtbFreightMaster.Rows[0][cst_FreightMasterTable.NOTE_FLD].ToString();
//					txtCostElement.Tag = dtbFreightMaster.Rows[0][cst_FreightMasterTable.COSTELEMENTID_FLD];
//					txtCostElement.Text = dtbFreightMaster.Rows[0][STD_CostElementTable.TABLE_NAME + STD_CostElementTable.CODE_FLD].ToString();
					txtObject.Text = dtbFreightMaster.Rows[0]["OBDes"].ToString();
					txtObject.Tag = dtbFreightMaster.Rows[0]["ACObjectID"];
					intObject = int.Parse(txtObject.Tag.ToString());
					txtPurpose.Text = dtbFreightMaster.Rows[0]["PUDes"].ToString();
					txtPurpose.Tag = dtbFreightMaster.Rows[0]["ACPurposeID"];
					intPurpose = int.Parse(dtbFreightMaster.Rows[0]["ACPurposeID"].ToString());
					if (dtbFreightMaster.Rows[0][cst_FreightMasterTable.RECEIPTMASTERID_FLD] != DBNull.Value)
					{
						txtInvoiceNo.Tag = dtbFreightMaster.Rows[0][cst_FreightMasterTable.RECEIPTMASTERID_FLD];
						txtInvoiceNo.Text = dtbFreightMaster.Rows[0][PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD].ToString();
						if ((dtbFreightMaster.Rows[0]["PONo"] != null)
							&& (dtbFreightMaster.Rows[0]["PONo"] != DBNull.Value))
						{
							txtPONo.Text = dtbFreightMaster.Rows[0]["PONo"].ToString();
						}
						else
							txtPONo.Text = string.Empty;
						if ((dtbFreightMaster.Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD] != null)
							&& (dtbFreightMaster.Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD] != DBNull.Value))
						{
							txtInvoice.Text = dtbFreightMaster.Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD].ToString();
						}
						else
							txtInvoice.Text = string.Empty;
						lblInvoiceNo.Text = "Receipt No.";
					}
					if (dtbFreightMaster.Rows[0][cst_FreightMasterTable.RETURNTOVENDORMASTERID_FLD] != DBNull.Value)
					{
						txtInvoiceNo.Tag = dtbFreightMaster.Rows[0][cst_FreightMasterTable.RETURNTOVENDORMASTERID_FLD];
						txtInvoiceNo.Text = dtbFreightMaster.Rows[0][PO_ReturnToVendorMasterTable.RTVNO_FLD].ToString();
						//get invoice number/PO Number
						DataSet dstInvoice_PONo = boFreight.GetInvoice_PONumber(int.Parse(txtInvoiceNo.Tag.ToString()));
						if (dstInvoice_PONo.Tables.Count > 0)
						{
							if (dstInvoice_PONo.Tables[0].Rows.Count > 0)
							{
								txtInvoice.Text = dstInvoice_PONo.Tables[0].Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD].ToString();
								txtPONo.Text = dstInvoice_PONo.Tables[0].Rows[0][PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
							}
						}
						lblInvoiceNo.Text = "Return No.";
					}
					txtExchRate.Value = (decimal) dtbFreightMaster.Rows[0][cst_FreightMasterTable.EXCHANGERATE_FLD];
					if ((dtbFreightMaster.Rows[0][cst_FreightMasterTable.TOTALAMOUNT_FLD] != null)
						&& (dtbFreightMaster.Rows[0][cst_FreightMasterTable.TOTALAMOUNT_FLD] != DBNull.Value))
					{
						txtNetAmount.Value = (decimal) dtbFreightMaster.Rows[0][cst_FreightMasterTable.TOTALAMOUNT_FLD];
					}
					else 
						txtNetAmount.Value = null;
					if ((dtbFreightMaster.Rows[0][cst_FreightMasterTable.VATPERCENT_FLD] != null)
						&& (dtbFreightMaster.Rows[0][cst_FreightMasterTable.VATPERCENT_FLD] != DBNull.Value))
					{
						txtVAT.Value = (decimal) dtbFreightMaster.Rows[0][cst_FreightMasterTable.VATPERCENT_FLD];
					}
					else
						txtVAT.Value = null;
					if ((dtbFreightMaster.Rows[0][cst_FreightMasterTable.SUBTOTAL_FLD] != null)
						&& (dtbFreightMaster.Rows[0][cst_FreightMasterTable.SUBTOTAL_FLD] != DBNull.Value))
					{
						txtSubTotal.Value = (decimal)dtbFreightMaster.Rows[0][cst_FreightMasterTable.SUBTOTAL_FLD];
					}
					else
						txtSubTotal.Value = null;
					if ((dtbFreightMaster.Rows[0][cst_FreightMasterTable.GRANDTOTAL_FLD] != null)
						&& (dtbFreightMaster.Rows[0][cst_FreightMasterTable.GRANDTOTAL_FLD] != DBNull.Value))
					{
						txtGrandTotal.Value = (decimal)dtbFreightMaster.Rows[0][cst_FreightMasterTable.GRANDTOTAL_FLD];
					}
					else
						txtGrandTotal.Value = null;
					if ((dtbFreightMaster.Rows[0][cst_FreightMasterTable.TOTALVAT_FLD] != null)
						&& (dtbFreightMaster.Rows[0][cst_FreightMasterTable.TOTALVAT_FLD] != DBNull.Value))
					{
						txtTotalVAT.Value = (decimal) dtbFreightMaster.Rows[0][cst_FreightMasterTable.TOTALVAT_FLD];
					}
					else
						txtTotalVAT.Value = null;
					if (txtInvoiceNo.Text != string.Empty)
					{
						lblInvoiceNo.ForeColor = Color.Maroon;
					}
					else
					{
						lblInvoiceNo.ForeColor = Color.Black;
					}
					if (txtNetAmount.Text != string.Empty)
					{
						lblNetAmount.ForeColor = Color.Maroon;
					}
					else
					{
						lblNetAmount.ForeColor = Color.Black;
					}
				}
				//Get Detail 
				dstGridData = boFreight.GetFreightDetailByMasterID(pintFreightMasterID);
				//Calculate Total Amount
				foreach (DataRow drow in dstGridData.Tables[0].Rows)
				{
					if (drow[VAT_AMOUNT_COLUMN] != DBNull.Value)
					{
						drow[TOTAL_AMOUNT_COLUMN] = Convert.ToDecimal(drow[AMOUNT_COLUMN]) + Convert.ToDecimal(drow[VAT_AMOUNT_COLUMN]);
					}
					else
						drow[TOTAL_AMOUNT_COLUMN] = drow[AMOUNT_COLUMN];
				}
				dgrdData.DataSource = dstGridData.Tables[0];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				if (intPurpose != (int)ACPurpose.ImportTax)
				{
					dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].Visible = false;
				}
				else
					dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].Visible = true;
				//CalculateInformationInTheGrid();
				dgrdData.Refresh();
				ConfigGrid(true);
				//visible/invisible Adjustment columns
				if ((intObject == 2) && (intPurpose == 3 || intPurpose == 4))
				{
					dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible =  true;
				}
				else
				{
					dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible =  false;						
					ClearAdjustmentID();
				}
				if (intPurpose == (int)ACPurpose.DebitNote)
				{
					dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible =  true;
				}
				else
				{
					dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible =  false;
					ClearInvoiceMasterID();
				}
				if (txtInvoice.Text != string.Empty)
				{
					dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor =  Color.Maroon;	
				}
				else
				{
					dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor =  Color.Black;	
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
		/// btnOrderNo_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void btnOrderNo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnOrderNo_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				
				if (cboCCN.SelectedValue != null)
				{
					htbCondition.Add(cst_FreightMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(cst_FreightMasterTable.TABLE_NAME, cst_FreightMasterTable.TRANNO_FLD, txtOrderNo.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					txtOrderNo.Text = drwResult[cst_FreightMasterTable.TRANNO_FLD].ToString();
					intFreightMasterID = int.Parse(drwResult[cst_FreightMasterTable.FREIGHTMASTERID_FLD].ToString());
					txtOrderNo.Tag = intFreightMasterID;
					FillDataByMasterID(intFreightMasterID);
					btnEdit.Enabled = true;
					btnDelete.Enabled = true;
					// added: enable Print button to view Freight Slip - DungLA 13-03-2006
					btnPrint.Enabled = true;
					// end added: enable Print button to view Freight Slip - DungLA 13-03-2006
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
		/// btnTransporterCode_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void btnTransporterCode_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnTransporterCode_Click()";
			try
			{
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				htbCondition.Add(VENDOR, (int) PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtTransporterCode.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					txtTransporterCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtTransporterCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtTransporterName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
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
		/// btnTransporterName_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void btnTransporterName_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnTransporterName_Click()";
			try
			{
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				htbCondition.Add(VENDOR, (int) PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtTransporterName.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					txtTransporterCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtTransporterCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtTransporterName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
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
		/// btnVendorName_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void btnVendorName_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendorName_Click()";
			try
			{
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				htbCondition.Add(VENDOR, (int) PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtVendorName.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					if ((txtVendorCode.Tag != null) && (int.Parse(txtVendorCode.Tag.ToString()) != int.Parse(drwResult[MST_PartyTable.PARTYID_FLD ].ToString())))
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtPONo.Text = string.Empty;
						txtInvoice.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						if (intObject == (int)ACObject.ReceiptTransaction)
						{
							//clear dataset
							if (dstGridData.Tables[0].Rows.Count > 0)
							{
								for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
								{
									if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
									{
										dstGridData.Tables[0].Rows[i].Delete();
									}
								}
							}
						}
					}
					txtVendorCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtVendorCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
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
		/// btnVendorCode_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void btnVendorCode_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendorCode_Click()";
			try
			{
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				htbCondition.Add(VENDOR, (int) PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtVendorCode.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					if ((txtVendorCode.Tag != null) && (int.Parse(txtVendorCode.Tag.ToString()) != int.Parse(drwResult[MST_PartyTable.PARTYID_FLD ].ToString())))
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtPONo.Text = string.Empty;
						txtInvoice.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						if (intObject == (int)ACObject.ReceiptTransaction)
						{
							//clear dataset
							if (dstGridData.Tables[0].Rows.Count > 0)
							{
								for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
								{
									if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
									{
										dstGridData.Tables[0].Rows[i].Delete();
									}
								}
							}
						}
						
					}
					txtVendorCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtVendorCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
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
		/// CheckBeforeSelectInvoice
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private bool CheckBeforeSelectInvoice()
		{
			const string METHOD_NAME = THIS + ".CheckBeforeSelectInvoice()";
			try
			{
				string[] strParam = new string[2];
				if (txtVendorCode.Text.Trim() == string.Empty)
				{
					strParam =  new string[2];
					strParam[0] = lblVendor.Text;
					strParam[1] = lblInvoiceNo.Text;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtVendorCode.Focus();
					return false;
				}
				
//				if (txtNetAmount.Text.Trim() == string.Empty)
//				{
//					strParam =  new string[2];
//					strParam[0] = lblNetAmount.Text;
//					strParam[1] = lblInvoiceNo.Text;
//					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
//					txtNetAmount.Focus();
//					return false;
//				}
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
		/// Calculate information in the grid
		/// </summary>
		/// <param name="pdstGridData"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, Feb 27 2006</date>
		private DataSet CalculateGrid(DataSet pdstGridData)
		{
			const string METHOD_NAME = THIS + ".CalculateGrid()";
			try
			{
				if (intPurpose == (int)ACPurpose.Freight)
				{
					decimal decSum = 0;
					foreach(DataRow drow in dstGridData.Tables[0].Rows)
					{
						if (drow.RowState == DataRowState.Deleted) continue;
						if ((drow[cst_FreightDetailTable.UNITPRICECIF_FLD] != null) 
							&& (drow[cst_FreightDetailTable.UNITPRICECIF_FLD] != DBNull.Value)
							&& (drow[cst_FreightDetailTable.QUANTITY_FLD] != null)
							&& (drow[cst_FreightDetailTable.QUANTITY_FLD] != DBNull.Value))

						{
							decSum += decimal.Parse(drow[cst_FreightDetailTable.QUANTITY_FLD].ToString())
								* decimal.Parse(drow[cst_FreightDetailTable.UNITPRICECIF_FLD].ToString());
						}
					}
					foreach(DataRow drow in dstGridData.Tables[0].Rows)
					{
						if (drow.RowState == DataRowState.Deleted) continue;
						//Calculate Amount column
						if (decSum > 0)
						{
							if ((drow[cst_FreightDetailTable.UNITPRICECIF_FLD] != null) 
								&& (drow[cst_FreightDetailTable.UNITPRICECIF_FLD] != DBNull.Value)
								&& (drow[cst_FreightDetailTable.QUANTITY_FLD] != null)
								&& (drow[cst_FreightDetailTable.QUANTITY_FLD] != DBNull.Value))
							{
								try 
								{
									if ((txtNetAmount.Value != null) &&(txtNetAmount.Value != DBNull.Value))
									{
										drow[AMOUNT_COLUMN] = (decimal.Parse(drow[cst_FreightDetailTable.QUANTITY_FLD].ToString())
											* decimal.Parse(drow[cst_FreightDetailTable.UNITPRICECIF_FLD].ToString())/decSum)* (decimal) txtNetAmount.Value;	
									}
								}
								catch{}
								
							}
						}
						//Calculate Vat Amount column
						decimal decVat = 0;
						try
						{
							decVat = (decimal)txtVAT.Value;
						}
						catch
						{
							decVat = 0;
							drow[VAT_AMOUNT_COLUMN] = DBNull.Value;
						}
					
						if ((drow[AMOUNT_COLUMN] != null) && (drow[AMOUNT_COLUMN] != DBNull.Value))
						{
							drow[VAT_AMOUNT_COLUMN] = Decimal.Parse(drow[AMOUNT_COLUMN].ToString()) * decVat/100;
						}
					
						//Calculate Total Amount column
						if ((drow[VAT_AMOUNT_COLUMN] != null) && (drow[VAT_AMOUNT_COLUMN] != DBNull.Value) && drow[VAT_AMOUNT_COLUMN].ToString() != string.Empty)
						{
							if ((drow[AMOUNT_COLUMN] != null) && (drow[AMOUNT_COLUMN] != DBNull.Value))
							{
								drow[TOTAL_AMOUNT_COLUMN] = decimal.Parse(drow[AMOUNT_COLUMN].ToString()) + Decimal.Parse(drow[VAT_AMOUNT_COLUMN].ToString());
							}
						}
						else
							drow[TOTAL_AMOUNT_COLUMN] = drow[AMOUNT_COLUMN];
					
					}
				}
				else if (intPurpose == (int)ACPurpose.ImportTax)
				{
					foreach(DataRow drow in dstGridData.Tables[0].Rows)
					{
						//Amount Column
						if ((drow[cst_FreightDetailTable.UNITPRICECIF_FLD] != null) 
							&& (drow[cst_FreightDetailTable.UNITPRICECIF_FLD] != DBNull.Value)
							&& (drow[cst_FreightDetailTable.QUANTITY_FLD] != null)
							&& (drow[cst_FreightDetailTable.QUANTITY_FLD] != DBNull.Value))
						{
							if ((drow[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD] != null) 
							&& (drow[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD] != DBNull.Value))
							{
								drow[AMOUNT_COLUMN] = decimal.Parse(drow[cst_FreightDetailTable.QUANTITY_FLD].ToString())
											* decimal.Parse(drow[cst_FreightDetailTable.UNITPRICECIF_FLD].ToString())/100
											* decimal.Parse(drow[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].ToString());
							}
							else
								drow[AMOUNT_COLUMN] = 0;
						}
						//Vat Column
						if ((drow[AMOUNT_COLUMN] != null) && (drow[AMOUNT_COLUMN] != DBNull.Value))
						{
							if ((drow[PO_PurchaseOrderDetailTable.VAT_FLD] != null) && (drow[PO_PurchaseOrderDetailTable.VAT_FLD] != DBNull.Value))
							{
								drow[VAT_AMOUNT_COLUMN] = decimal.Parse(drow[AMOUNT_COLUMN].ToString()) * decimal.Parse(drow[PO_PurchaseOrderDetailTable.VAT_FLD].ToString())	/100;
							}
							else
							{
								drow[VAT_AMOUNT_COLUMN]	= 0;
							}
						}
						//Calculate Total Amount column
						if ((drow[VAT_AMOUNT_COLUMN] != null) && (drow[VAT_AMOUNT_COLUMN] != DBNull.Value) && drow[VAT_AMOUNT_COLUMN].ToString() != string.Empty)
						{
							if ((drow[AMOUNT_COLUMN] != null) && (drow[AMOUNT_COLUMN] != DBNull.Value))
							{
								drow[TOTAL_AMOUNT_COLUMN] = decimal.Parse(drow[AMOUNT_COLUMN].ToString()) + Decimal.Parse(drow[VAT_AMOUNT_COLUMN].ToString());
							}
						}
						else
							drow[TOTAL_AMOUNT_COLUMN] = drow[AMOUNT_COLUMN];
					}
					
				}
				else
					if (intPurpose == (int)ACPurpose.DebitNote || intPurpose == (int)ACPurpose.CreditNote)
					{
						if (intObject == (int)ACObject.ReceiptTransaction)
						{
							decimal decSum = 0;
							foreach(DataRow drow in dstGridData.Tables[0].Rows)
							{
								if (drow.RowState == DataRowState.Deleted) continue;
								if ((drow[cst_FreightDetailTable.UNITPRICECIF_FLD] != null) 
									&& (drow[cst_FreightDetailTable.UNITPRICECIF_FLD] != DBNull.Value)
									&& (drow[cst_FreightDetailTable.QUANTITY_FLD] != null)
									&& (drow[cst_FreightDetailTable.QUANTITY_FLD] != DBNull.Value))

								{
									decSum += decimal.Parse(drow[cst_FreightDetailTable.QUANTITY_FLD].ToString())
										* decimal.Parse(drow[cst_FreightDetailTable.UNITPRICECIF_FLD].ToString());
								}
							}
							foreach(DataRow drow in dstGridData.Tables[0].Rows)
							{
								if (drow.RowState == DataRowState.Deleted) continue;
								//Calculate Amount column
								if (decSum > 0)
								{
									if ((drow[cst_FreightDetailTable.UNITPRICECIF_FLD] != null) 
										&& (drow[cst_FreightDetailTable.UNITPRICECIF_FLD] != DBNull.Value)
										&& (drow[cst_FreightDetailTable.QUANTITY_FLD] != null)
										&& (drow[cst_FreightDetailTable.QUANTITY_FLD] != DBNull.Value))
									{
										try 
										{
											if ((txtNetAmount.Value != null) &&(txtNetAmount.Value != DBNull.Value))
											{
												drow[AMOUNT_COLUMN] = (decimal.Parse(drow[cst_FreightDetailTable.QUANTITY_FLD].ToString())
													* decimal.Parse(drow[cst_FreightDetailTable.UNITPRICECIF_FLD].ToString())/decSum)* (decimal) txtNetAmount.Value;	
											}
										}
										catch{}
								
									}
								}
								//Calculate Vat Amount column
								if ((drow[AMOUNT_COLUMN] != null) && (drow[AMOUNT_COLUMN] != DBNull.Value))
								{
									if ((drow[VAT_AMOUNT_COLUMN] != null) && (drow[VAT_AMOUNT_COLUMN] != DBNull.Value))
									{
										drow[VAT_AMOUNT_COLUMN] = decimal.Parse(drow[AMOUNT_COLUMN].ToString()) * decimal.Parse(drow[VAT_AMOUNT_COLUMN].ToString())	/100;
									}
									else
									{
										drow[VAT_AMOUNT_COLUMN]	= DBNull.Value;					 
									}
								}
					
								//Calculate Total Amount column
								if ((drow[VAT_AMOUNT_COLUMN] != null) && (drow[VAT_AMOUNT_COLUMN] != DBNull.Value) && drow[VAT_AMOUNT_COLUMN].ToString() != string.Empty)
								{
									if ((drow[AMOUNT_COLUMN] != null) && (drow[AMOUNT_COLUMN] != DBNull.Value))
									{
										drow[TOTAL_AMOUNT_COLUMN] = decimal.Parse(drow[AMOUNT_COLUMN].ToString()) + Decimal.Parse(drow[VAT_AMOUNT_COLUMN].ToString());
									}
								}
								else
									drow[TOTAL_AMOUNT_COLUMN] = drow[AMOUNT_COLUMN];
					
							}
						}
					}
				//Calculate Total Vat, Sub Total, Grand Total
				decimal decTotalVat = 0;
				decimal decSubTotal = 0;
				decimal decGrandTotal = 0;
				foreach(DataRow drow in dstGridData.Tables[0].Rows)
				{
					if (drow.RowState == DataRowState.Deleted) continue;
					if ((drow[AMOUNT_COLUMN] != null) && (drow[AMOUNT_COLUMN] != DBNull.Value))
					{
						decSubTotal += decimal.Parse(drow[AMOUNT_COLUMN].ToString());
					}
					if ((drow[VAT_AMOUNT_COLUMN] != null) && (drow[VAT_AMOUNT_COLUMN] != DBNull.Value)
						&& (drow[VAT_AMOUNT_COLUMN].ToString() != string.Empty))
					{
						decTotalVat += decimal.Parse(drow[VAT_AMOUNT_COLUMN].ToString());
					}
					if ((drow[TOTAL_AMOUNT_COLUMN] != null) && (drow[TOTAL_AMOUNT_COLUMN] != DBNull.Value))
					{
						decGrandTotal += decimal.Parse(drow[TOTAL_AMOUNT_COLUMN].ToString());
					}
				}
				txtTotalVAT.Value = decTotalVat;
				txtSubTotal.Value = decSubTotal;
				txtGrandTotal.Value = decGrandTotal;
				//DataSet dstGrid = pdstGridData.Clone();
				//Update Line column
				int intIncrement = 0;
				for (int i = 0; i < dstGridData.Tables[0].Rows.Count; i++)
				{
					if (dstGridData.Tables[0].Rows[i].RowState == DataRowState.Deleted) continue;
					intIncrement++;
					//dstGridData.Tables[0].Rows[i][cst_FreightDetailTable.LINE_FLD] = intIncrement;
				}
			
				return pdstGridData;
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
		/// FillDataByReturnNo
		/// </summary>
		/// <param name="pintReturnToVendorMasterID"></param>
		/// <author>Trada</author>
		/// <date>Thursday, July 6 2006</date>
		private void FillDataByReturnNo(int pintReturnToVendorMasterID)
		{
			//delete all old rows if they are exist
			for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
			{
				if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
				{
					dstGridData.Tables[0].Rows[i].Delete();
				}
			}
			DataSet dstTemp = new DataSet();
			FreightBO boFreight = new FreightBO();
			dstTemp = boFreight.GetReturnToVendorDetail(pintReturnToVendorMasterID);
			if (dstTemp.Tables.Count > 1)
			{
				if (dstTemp.Tables[1].Rows.Count > 0)
				{
					if ((dstTemp.Tables[1].Rows[0][PO_PurchaseOrderMasterTable.CODE_FLD] != null)
						&& (dstTemp.Tables[1].Rows[0][PO_PurchaseOrderMasterTable.CODE_FLD] != DBNull.Value))
					{
						txtPONo.Text = dstTemp.Tables[1].Rows[0][PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
					}
					else
						txtPONo.Text = string.Empty;
					if ((dstTemp.Tables[1].Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD] != null)
						&& (dstTemp.Tables[1].Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD] != DBNull.Value))
					{
						txtInvoice.Text = dstTemp.Tables[1].Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD].ToString();
					}
					else
						txtInvoice.Text = string.Empty;
				}
			}

			foreach (DataRow drow in dstTemp.Tables[0].Rows)
			{
				if (drow.RowState == DataRowState.Deleted) continue;
				DataRow drowData = dstGridData.Tables[0].NewRow();
				foreach (DataColumn dcol in dstGridData.Tables[0].Columns)
				{
					drowData[dcol.Caption] = drow[dcol.Caption];
				}
				dstGridData.Tables[0].Rows.Add(drowData);
				
			}
			//Set FreightMasterID
			if (intFreightMasterID > 0) 
			{
				foreach (DataRow drow in dstGridData.Tables[0].Rows)
				{
					if (drow.RowState == DataRowState.Deleted) continue;
					if ((drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] == null)
						|| (drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] == DBNull.Value))
					{
						drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] = intFreightMasterID;
					}
					else
					{
						if (int.Parse(drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD].ToString()) == 0)
						{
							drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] = intFreightMasterID;
						}
					}
				}
			}
			//Calculate Total Vat, Sub Total, Grand Total
			decimal decTotalVat = 0;
			decimal decSubTotal = 0;
			decimal decGrandTotal = 0;
			foreach(DataRow drow in dstGridData.Tables[0].Rows)
			{
				if (drow.RowState == DataRowState.Deleted) continue;
				if ((drow[AMOUNT_COLUMN] != null) && (drow[AMOUNT_COLUMN] != DBNull.Value))
				{
					decSubTotal += decimal.Parse(drow[AMOUNT_COLUMN].ToString());
				}
				if ((drow[VAT_AMOUNT_COLUMN] != null) && (drow[VAT_AMOUNT_COLUMN] != DBNull.Value)
					&& (drow[VAT_AMOUNT_COLUMN].ToString() != string.Empty))
				{
					decTotalVat += decimal.Parse(drow[VAT_AMOUNT_COLUMN].ToString());
				}
				if ((drow[TOTAL_AMOUNT_COLUMN] != null) && (drow[TOTAL_AMOUNT_COLUMN] != DBNull.Value))
				{
					decGrandTotal += decimal.Parse(drow[TOTAL_AMOUNT_COLUMN].ToString());
				}
			}
			txtTotalVAT.Value = decTotalVat;
			txtSubTotal.Value = decSubTotal;
			txtGrandTotal.Value = decGrandTotal;
			dgrdData.DataSource = dstGridData.Tables[0];
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
			dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].Visible = false;
			dgrdData.Refresh();
			ConfigGrid(false);
			
			//unlock some column
			dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].Locked = false;
			dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor = Color.Maroon;
			if ((intPurpose == (int)ACPurpose.CreditNote || intPurpose == (int)ACPurpose.DebitNote)
				&& intObject == (int)ACObject.ItemInventory)
			{
				//show Inventory adjustment
				dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = true;
			}
			else
				dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;

			//check invoiceno
			if (txtInvoice.Text.Trim() != string.Empty)
			{
				dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor = Color.Maroon;
			}
			else
				dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor = Color.Black;	
		}
		/// <summary>
		/// FillDataByInvoiceNo
		/// </summary>
		/// <param name="pintPOReceiveMasterID"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void FillDataByReceiveNo(int pintPOReceiveMasterID)
		{
			const string METHOD_NAME = THIS + ".FillDataByInvoiceNo()";
			try
			{
				#region Debit or CreditNote
				if (intPurpose == (int)ACPurpose.DebitNote || intPurpose == (int)ACPurpose.CreditNote)
				{
					if (intObject == (int)ACObject.ReceiptTransaction)
					{
						//delete all old rows if they are exist
						for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
						{
							if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
							{
								dstGridData.Tables[0].Rows[i].Delete();
							}
						}
						DataSet dstTemp = new DataSet();
						FreightBO boFreight = new FreightBO();
						dstTemp = boFreight.GetPOReceiveDetail(pintPOReceiveMasterID);
						if (dstTemp.Tables.Count > 1)
						{
							if (dstTemp.Tables[1].Rows.Count > 0)
							{
								if ((dstTemp.Tables[1].Rows[0][PO_PurchaseOrderMasterTable.CODE_FLD] != null)
									&& (dstTemp.Tables[1].Rows[0][PO_PurchaseOrderMasterTable.CODE_FLD] != DBNull.Value))
								{
									txtPONo.Text = dstTemp.Tables[1].Rows[0][PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
								}
								else
									txtPONo.Text = string.Empty;
								if ((dstTemp.Tables[1].Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD] != null)
									&& (dstTemp.Tables[1].Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD] != DBNull.Value))
								{
									txtInvoice.Text = dstTemp.Tables[1].Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD].ToString();
								}
								else
									txtInvoice.Text = string.Empty;
							}
						}
						foreach (DataRow drow in dstTemp.Tables[0].Rows)
						{
							if (drow.RowState == DataRowState.Deleted) continue;
							DataRow drowData = dstGridData.Tables[0].NewRow();
							//drowData.ItemArray = drow.ItemArray;
							foreach (DataColumn dcol in dstGridData.Tables[0].Columns)
							{
								if (dcol.Caption.Trim().ToLower() != cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD.Trim().ToLower())
								{
									drowData[dcol.Caption] = drow[dcol.Caption];
								}
							}
							dstGridData.Tables[0].Rows.Add(drowData);
						}
						//Set FreightMasterID
						if (intFreightMasterID > 0) 
						{
							foreach (DataRow drow in dstGridData.Tables[0].Rows)
							{
								if (drow.RowState == DataRowState.Deleted) continue;
								if ((drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] == null)
									|| (drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] == DBNull.Value))
								{
									drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] = intFreightMasterID;
								}
								else
								{
									if (int.Parse(drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD].ToString()) == 0)
									{
										drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] = intFreightMasterID;
									}
								}
							}
						}
						//Calculate Amount Column
						foreach (DataRow drow in dstGridData.Tables[0].Rows)
						{
							if ((drow[cst_FreightDetailTable.UNITPRICECIF_FLD] != null) 
							&& (drow[cst_FreightDetailTable.UNITPRICECIF_FLD] != DBNull.Value)
							&& (drow[cst_FreightDetailTable.QUANTITY_FLD] != null)
							&& (drow[cst_FreightDetailTable.QUANTITY_FLD] != DBNull.Value))
							{
								drow[cst_FreightDetailTable.AMOUNT_FLD] = Convert.ToDecimal(drow[cst_FreightDetailTable.QUANTITY_FLD])
									* Convert.ToDecimal(drow[cst_FreightDetailTable.UNITPRICECIF_FLD]);
							}
							//Vat Amount
							if ((drow[cst_FreightDetailTable.AMOUNT_FLD] != null) 
								&& (drow[cst_FreightDetailTable.AMOUNT_FLD] != DBNull.Value)
								&& (drow[ITM_ProductTable.VAT_FLD] != null)
								&& (drow[ITM_ProductTable.VAT_FLD] != DBNull.Value))
							{
								drow[cst_FreightDetailTable.VATAMOUNT_FLD] = Convert.ToDecimal(drow[cst_FreightDetailTable.AMOUNT_FLD])
									* Convert.ToDecimal(drow[ITM_ProductTable.VAT_FLD])/100;
							}

						}
						dgrdData.DataSource = dstGridData.Tables[0];
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
						dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].Visible = false;
						//unlock some column
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor = Color.Maroon;
						CalculateInformationInTheGrid();
						dgrdData.Refresh();
						ConfigGrid(false);
						
					}
				}
				#endregion
				#region Freight or Importax Mode
				if (intPurpose == (int)ACPurpose.Freight || intPurpose == (int)ACPurpose.ImportTax)
				{
					//delete all old rows if they are exist
					for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
					{
						if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
						{
							dstGridData.Tables[0].Rows[i].Delete();
						}
					}
					DataSet dstTemp = new DataSet();
					FreightBO boFreight = new FreightBO();
					dstTemp = boFreight.GetPOReceiveDetail(pintPOReceiveMasterID);
					if (dstTemp.Tables.Count > 1)
					{
						if (dstTemp.Tables[1].Rows.Count > 0)
						{
							if ((dstTemp.Tables[1].Rows[0][PO_PurchaseOrderMasterTable.CODE_FLD] != null)
								&& (dstTemp.Tables[1].Rows[0][PO_PurchaseOrderMasterTable.CODE_FLD] != DBNull.Value))
							{
								txtPONo.Text = dstTemp.Tables[1].Rows[0][PO_PurchaseOrderMasterTable.CODE_FLD].ToString();
							}
							else
								txtPONo.Text = string.Empty;
							if ((dstTemp.Tables[1].Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD] != null)
								&& (dstTemp.Tables[1].Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD] != DBNull.Value))
							{
								txtInvoice.Text = dstTemp.Tables[1].Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD].ToString();
							}
							else
								txtInvoice.Text = string.Empty;
						}
					}
					foreach (DataRow drow in dstTemp.Tables[0].Rows)
					{
						if (drow.RowState == DataRowState.Deleted) continue;
						DataRow drowData = dstGridData.Tables[0].NewRow();
						//drowData.ItemArray = drow.ItemArray;
						foreach (DataColumn dcol in dstGridData.Tables[0].Columns)
						{
							if (dcol.Caption.Trim().ToLower() != cst_FreightDetailTable.RETURNTOVENDORDETAILID_FLD.Trim().ToLower())
							{
								drowData[dcol.Caption] = drow[dcol.Caption];
							}
						}
						dstGridData.Tables[0].Rows.Add(drowData);
					}
					if (dstGridData.Tables[0].Rows.Count > 0)
					{
						//Calculate grid
						dstGridData = CalculateGrid(dstGridData);
					}
					//reset invoicemasterid 
					foreach (DataRow drow in dstGridData.Tables[0].Rows)
					{
						if (drow.RowState == DataRowState.Deleted) continue;
						drow[PO_InvoiceMasterTable.INVOICEMASTERID_FLD] = DBNull.Value;
					}
					//Set FreightMasterID
					if (intFreightMasterID > 0) 
					{
						foreach (DataRow drow in dstGridData.Tables[0].Rows)
						{
							if (drow.RowState == DataRowState.Deleted) continue;
							if ((drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] == null)
								|| (drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] == DBNull.Value))
							{
								drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] = intFreightMasterID;
							}
							else
							{
								if (int.Parse(drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD].ToString()) == 0)
								{
									drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] = intFreightMasterID;
								}
							}
						}
					}
					dgrdData.DataSource = dstGridData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					if (intPurpose == (int) ACPurpose.Freight)
					{
						dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].Visible = false;
					}
					else
						dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].Visible = true;	
					dstGridData = CalculateGrid(dstGridData);
					CalculateInformationInTheGrid();
					dgrdData.Refresh();
					ConfigGrid(false);
				}
				#endregion
				if ((intPurpose == (int)ACPurpose.CreditNote || intPurpose == (int)ACPurpose.DebitNote)
					&& intObject == (int)ACObject.ItemInventory)
				{
					//show Inventory adjustment
					dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = true;
				}
				else
					dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
				if (intPurpose == (int)ACPurpose.DebitNote && intObject == (int) ACObject.ItemInventory)
				{
					dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = true;
				}
				else
					dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
				dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = true;
				//check invoiceno
				if (txtInvoice.Text.Trim() != string.Empty)
				{
					dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor = Color.Maroon;
				}
				else
					dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor = Color.Black;	

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
		/// btnInvoiceNo_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void btnInvoiceNo_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnInvoiceNo_Click()";
			try
			{
				//update use case
				if (intPurpose == (int)ACPurpose.DebitNote && intObject == (int)ACObject.ReceiptTransaction)
				{
					//check vendor
					string[] strParam = new string[2];
					if (txtVendorCode.Text.Trim() == string.Empty)
					{
						strParam =  new string[2];
						strParam[0] = lblVendor.Text;
						strParam[1] = lblInvoiceNo.Text;
						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
						txtVendorCode.Focus();
						return;
					}
					else
					{
						Hashtable htbCondition = new Hashtable();
						DataRowView drwResult = null;
						//add condition
						htbCondition.Add(PO_ReturnToVendorMasterTable.PARTYID_FLD, txtVendorCode.Tag);
						drwResult = FormControlComponents.OpenSearchForm(PO_ReturnToVendorMasterTable.TABLE_NAME, PO_ReturnToVendorMasterTable.RTVNO_FLD, txtInvoiceNo.Text.Trim(), htbCondition, true);
						if (drwResult != null)
						{
							if ((txtInvoiceNo.Tag != null) && (txtInvoiceNo.Tag != DBNull.Value))
							{
								if (int.Parse(drwResult[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD].ToString()) != int.Parse(txtInvoiceNo.Tag.ToString()))
								{
									if (PCSMessageBox.Show(ErrorCode.MESSAGE_ADDITION_CHARGE_CHANGE_RETURN_NO, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
									{
										txtInvoiceNo.Tag = drwResult[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD];
										txtInvoiceNo.Text = drwResult[PO_ReturnToVendorMasterTable.RTVNO_FLD].ToString();
										FillDataByReturnNo(int.Parse(drwResult[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD].ToString()));			
										return;
									}
									else
										return;
								}
							}
							txtInvoiceNo.Tag = drwResult[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD];
							txtInvoiceNo.Text = drwResult[PO_ReturnToVendorMasterTable.RTVNO_FLD].ToString();
							FillDataByReturnNo(int.Parse(drwResult[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD].ToString()));			
						}
					}

				}
				else
				{
					//old code
					if (CheckBeforeSelectInvoice())
					{
						Hashtable htbCondition = new Hashtable();
						DataRowView drwResult = null;
						htbCondition.Add(PO_InvoiceMasterTable.PARTYID_FLD, int.Parse(txtVendorCode.Tag.ToString()));
						if (txtMaker.Text != string.Empty)
						{
							//Se phai add them dieu kien ve Maker.
							htbCondition.Add(PO_PurchaseOrderMasterTable.MAKERID_FLD, txtMaker.Tag);
						}
						drwResult = FormControlComponents.OpenSearchForm(V_RECEIPTBYVENDOR, PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD, txtInvoiceNo.Text.Trim(), htbCondition, true);
						if (drwResult != null)
						{
							if ((txtInvoiceNo.Tag != null) && (txtInvoiceNo.Tag != DBNull.Value))
							{
								if (int.Parse(drwResult[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD].ToString()) != int.Parse(txtInvoiceNo.Tag.ToString()))
								{
									if (PCSMessageBox.Show(ErrorCode.MESSAGE_FREIGHT_CHANGE_RECEIVE_NO, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
									{
										txtInvoiceNo.Tag = drwResult[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD];
										txtInvoiceNo.Text = drwResult[PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD].ToString();
										FillDataByReceiveNo(int.Parse(drwResult[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD].ToString()));			
										return;
									}
									else
										return;
								}
							}
							txtInvoiceNo.Tag = drwResult[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD];
							txtInvoiceNo.Text = drwResult[PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD].ToString();
							FillDataByReceiveNo(int.Parse(drwResult[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD].ToString()));			

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

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
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
		/// btnClose_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnClose_Click()";
			try
			{
				this.Close();
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
		/// txtCurrency_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void txtCurrency_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCurrency_Validating()";
			try
			{
				if (txtCurrency.Text.Trim() ==  string.Empty)
				{
					txtExchRate.Value = null;
					txtCurrency.Tag = null;
					return;
				}
				if (!txtCurrency.Modified) return;
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME, MST_CurrencyTable.CODE_FLD, txtCurrency.Text.Trim(), null, false);
				if(drwResult != null)
				{
					txtCurrency.Tag = drwResult[MST_CurrencyTable.CURRENCYID_FLD];
					txtCurrency.Text = drwResult[MST_CurrencyTable.CODE_FLD].ToString();
					if (drwResult[MST_CurrencyTable.CURRENCYID_FLD].ToString() != SystemProperty.HomeCurrencyID.ToString())
					{
						txtExchRate.Value = null;
						txtExchRate.Enabled = true;
					}
					else
					{
						txtExchRate.Value = 1;
						txtExchRate.Enabled = false;
					}
				}	
				else
					e.Cancel = true;
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
		/// txtTransporterCode_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void txtTransporterCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtTransporterCode_Validating()";
			try
			{
				if (txtTransporterCode.Text.Trim() == string.Empty)
				{
					txtTransporterCode.Tag = null;
					txtTransporterName.Text = string.Empty;
					return;
				}
				if (!txtTransporterCode.Modified) return;
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				htbCondition.Add(VENDOR, (int) PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtTransporterCode.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					txtTransporterCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtTransporterCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtTransporterName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
				}
				else
					e.Cancel = true;
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
		/// txtTransporterName_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void txtTransporterName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtTransporterCode_Validating()";
			try
			{
				if (txtTransporterName.Text.Trim() == string.Empty)
				{
					txtTransporterCode.Tag = null;
					txtTransporterCode.Text = string.Empty;
					return;
				}
				if (!txtTransporterName.Modified) return;
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				htbCondition.Add(VENDOR, (int) PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtTransporterName.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					txtTransporterCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtTransporterCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtTransporterName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
				}
				else
					e.Cancel = true;
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
		/// txtVendorCode_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
		private void txtVendorCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendorCode_Validating()";
			try
			{
				if (txtVendorCode.Text.Trim() == string.Empty)
				{
					txtVendorCode.Tag = null;
					txtVendorName.Text = string.Empty;
					//clear information relate
					txtInvoiceNo.Text = string.Empty;
					txtPONo.Text = string.Empty;
					txtInvoice.Text = string.Empty;
					txtInvoiceNo.Tag = null;
					if (intObject == (int)ACObject.ReceiptTransaction)
					{
						//clear dataset
						if (dstGridData.Tables[0].Rows.Count > 0)
						{
							for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
							{
								if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
								{
									dstGridData.Tables[0].Rows[i].Delete();
								}
							}
						}
					}
					return;
				}
				if (!txtVendorCode.Modified) return;
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				htbCondition.Add(VENDOR, (int) PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtVendorCode.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					if ((txtVendorCode.Tag != null) && (int.Parse(txtVendorCode.Tag.ToString()) != int.Parse(drwResult[MST_PartyTable.PARTYID_FLD ].ToString())))
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtPONo.Text = string.Empty;
						txtInvoice.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						//clear dataset
						if (dstGridData.Tables[0].Rows.Count > 0)
						{
							for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
							{
								if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
								{
									dstGridData.Tables[0].Rows[i].Delete();
								}
							}
						}
						
					}
					txtVendorCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtVendorCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
				}
				else
					e.Cancel = true;
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
		/// txtVendorName_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 17 2006</date>
		private void txtVendorName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendorName_Validating()";
			try
			{
				if (txtVendorName.Text.Trim() == string.Empty)
				{
					txtVendorCode.Tag = null;
					txtVendorCode.Text = string.Empty;
					//clear information relate
					txtInvoiceNo.Text = string.Empty;
					txtPONo.Text = string.Empty;
					txtInvoice.Text = string.Empty;
					txtInvoiceNo.Tag = null;
					if (intObject == (int)ACObject.ReceiptTransaction)
					{
						//clear dataset
						if (dstGridData.Tables[0].Rows.Count > 0)
						{
							for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
							{
								if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
								{
									dstGridData.Tables[0].Rows[i].Delete();
								}
							}
						}
					}
					return;
				}
				if (!txtVendorName.Modified) return;
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				htbCondition.Add(VENDOR, (int) PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtVendorName.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					if ((txtVendorCode.Tag != null) && (int.Parse(txtVendorCode.Tag.ToString()) != int.Parse(drwResult[MST_PartyTable.PARTYID_FLD ].ToString())))
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtPONo.Text = string.Empty;
						txtInvoice.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						//clear dataset
						if (dstGridData.Tables[0].Rows.Count > 0)
						{
							for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
							{
								if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
								{
									dstGridData.Tables[0].Rows[i].Delete();
								}
							}
						}
						
					}
					txtVendorCode.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtVendorCode.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtVendorName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
				}
				else
					e.Cancel = true;
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
		/// txtInvoiceNo_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, Feb 27 2006</date>
		private void txtInvoiceNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtInvoiceNo_Validating()";
			try
			{
				if (txtInvoiceNo.Text.Trim() == string.Empty)
				{
					txtInvoice.Text = string.Empty;
					txtPONo.Text = string.Empty;
					txtInvoiceNo.Tag = null;
					txtSubTotal.Value = DBNull.Value;
					txtGrandTotal.Value = DBNull.Value;
					txtTotalVAT.Value = DBNull.Value;
					//clear dataset
					if (dstGridData.Tables[0].Rows.Count > 0)
					{
						for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
						{
							if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
							{
								dstGridData.Tables[0].Rows[i].Delete();
							}
						}
					}
//					CreateDataSet();
//					dgrdData.DataSource = dstGridData.Tables[0];
//					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
//					ConfigGrid(false);
					return;
				}
				if (!txtInvoiceNo.Modified) return;
				//update use case
				if (intPurpose == (int)ACPurpose.DebitNote && intObject == (int)ACObject.ReceiptTransaction)
				{
					//check vendor
					string[] strParam = new string[2];
					if (txtVendorCode.Text.Trim() == string.Empty)
					{
						strParam =  new string[2];
						strParam[0] = lblVendor.Text;
						strParam[1] = lblInvoiceNo.Text;
						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
						txtVendorCode.Focus();
						return;
					}
					else
					{
						Hashtable htbCondition = new Hashtable();
						DataRowView drwResult = null;
						//add condition
						htbCondition.Add(PO_ReturnToVendorMasterTable.PARTYID_FLD, txtVendorCode.Tag);
						drwResult = FormControlComponents.OpenSearchForm(PO_ReturnToVendorMasterTable.TABLE_NAME, PO_ReturnToVendorMasterTable.RTVNO_FLD, txtInvoiceNo.Text.Trim(), htbCondition, true);
						if (drwResult != null)
						{
							if ((txtInvoiceNo.Tag != null) && (txtInvoiceNo.Tag != DBNull.Value))
							{
								if (int.Parse(drwResult[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD].ToString()) != int.Parse(txtInvoiceNo.Tag.ToString()))
								{
									if (PCSMessageBox.Show(ErrorCode.MESSAGE_ADDITION_CHARGE_CHANGE_RETURN_NO, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
									{
										txtInvoiceNo.Tag = drwResult[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD];
										txtInvoiceNo.Text = drwResult[PO_ReturnToVendorMasterTable.RTVNO_FLD].ToString();
										FillDataByReturnNo(int.Parse(drwResult[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD].ToString()));			
										return;
									}
									else
										return;
								}
							}
							txtInvoiceNo.Tag = drwResult[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD];
							txtInvoiceNo.Text = drwResult[PO_ReturnToVendorMasterTable.RTVNO_FLD].ToString();
							FillDataByReturnNo(int.Parse(drwResult[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD].ToString()));			
						}
					}

				}
				else
				{
					if (CheckBeforeSelectInvoice())
					{
						Hashtable htbCondition = new Hashtable();
						DataRowView drwResult = null;
						htbCondition.Add(PO_InvoiceMasterTable.PARTYID_FLD, int.Parse(txtVendorCode.Tag.ToString()));
						if (txtMaker.Text != string.Empty)
						{
							//se phai add them dieu kien ve maker
							htbCondition.Add(PO_PurchaseOrderMasterTable.MAKERID_FLD, txtMaker.Tag);
						}
						drwResult = FormControlComponents.OpenSearchForm(V_RECEIPTBYVENDOR, PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD, txtInvoiceNo.Text.Trim(), htbCondition, false);
						if (drwResult != null)
						{
							if ((txtInvoiceNo.Tag != null) && (txtInvoiceNo.Tag != DBNull.Value))
							{
								if (int.Parse(drwResult[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD].ToString()) != int.Parse(txtInvoiceNo.Tag.ToString()))
								{
									if (PCSMessageBox.Show(ErrorCode.MESSAGE_FREIGHT_CHANGE_RECEIVE_NO, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
									{
										txtInvoiceNo.Tag = drwResult[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD];
										txtInvoiceNo.Text = drwResult[PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD].ToString();
										FillDataByReceiveNo(int.Parse(drwResult[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD].ToString()));			
									}
									else 
										e.Cancel = true;
								}
							}
							txtInvoiceNo.Tag = drwResult[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD];
							txtInvoiceNo.Text = drwResult[PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD].ToString();
							FillDataByReceiveNo(int.Parse(drwResult[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD].ToString()));
						
						}
						else
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
		/// txtVAT_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, Feb 27 2006</date>
		private void txtVAT_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVAT_Leave()";
			try
			{
				if (dgrdData.RowCount == 0) return;
				dstGridData = CalculateGrid(dstGridData); 
				dgrdData.Refresh();
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
		/// ValidateData
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, Feb 27 2006</date>
		private bool ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			try
			{
				if (FormControlComponents.CheckMandatory(dtmPostDate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return false;
				}
				if (!FormControlComponents.CheckDateInCurrentPeriod((DateTime)dtmPostDate.Value))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD);
					dtmPostDate.Focus();
					dtmPostDate.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(cboCCN))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboCCN.Focus();
					cboCCN.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtCurrency))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtCurrency.Focus();
					txtCurrency.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtCurrency))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtCurrency.Focus();
					txtCurrency.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtOrderNo))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtOrderNo.Focus();
					txtOrderNo.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtExchRate))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtExchRate.Focus();
					txtExchRate.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtPurpose))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtPurpose.Focus();
					txtPurpose.Select();
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtObject))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtObject.Focus();
					txtObject.Select();
					return false;
				}
				if ((intPurpose == (int)ACPurpose.Freight) && FormControlComponents.CheckMandatory(txtTransporterCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtTransporterCode.Focus();
					txtTransporterCode.Select();
					return false;
				}
				if ((intPurpose != (int)ACPurpose.CreditNote && intPurpose != (int)ACPurpose.DebitNote) && FormControlComponents.CheckMandatory(txtVendorCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtVendorCode.Focus();
					txtVendorCode.Select();
					return false;
				}
				if ((txtNetAmount.Enabled == true)&& FormControlComponents.CheckMandatory(txtNetAmount))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtNetAmount.Focus();
					txtNetAmount.Select();
					return false;
				}
				if ((intPurpose == (int)ACPurpose.Freight) && FormControlComponents.CheckMandatory(txtNetAmount))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtNetAmount.Focus();
					txtNetAmount.Select();
					return false;
				}
				if ((intPurpose != (int)ACPurpose.CreditNote && intPurpose != (int)ACPurpose.DebitNote) && FormControlComponents.CheckMandatory(txtInvoiceNo))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtInvoiceNo.Focus();
					txtInvoiceNo.Select();
					return false;
				}
				//check data in the grid
				if (dgrdData.RowCount == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_PLEASE_ENTER_DETAIL_INFOR, MessageBoxIcon.Warning);
					dgrdData.Focus();
					return false;
				}

				if (intPurpose == (int)ACPurpose.DebitNote && intObject == (int) ACObject.ItemInventory && txtInvoice.Text.Trim() != string.Empty)
				{
					for (int i = 0; i < dgrdData.RowCount; i++)
					{
						if (dgrdData[i, PO_InvoiceMasterTable.INVOICENO_FLD].ToString() == string.Empty)
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD]);
							dgrdData.Focus();
							return false;
						}
					}

				}
				if (txtInvoice.Text.Trim() != string.Empty)
				{
					for (int i = 0; i < dgrdData.RowCount; i++)
					{
						if (dgrdData[i, PO_InvoiceMasterTable.INVOICENO_FLD].ToString() == string.Empty)
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD]);
							dgrdData.Focus();
							return false;
						}
					}
				}
				decimal decSumOfAmount = 0;
				int intRowsCount = 0;
				if (intObject == (int)ACObject.ItemInventory)
				{
					if (dgrdData.RowCount == 0)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_PLEASE_ENTER_DETAIL_INFOR, MessageBoxIcon.Warning);
						dgrdData.Focus();
						return false;
					}
					intRowsCount = dgrdData.RowCount - 1;
                }
				else
					intRowsCount = dgrdData.RowCount;
				for (int i = 0; i < intRowsCount; i++)
				{
					if (intObject == (int)ACObject.ItemInventory)
					{
						if (dgrdData[i, ITM_ProductTable.CODE_FLD].ToString() == string.Empty)
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
							dgrdData.Focus();
							return false;
						}	
						if (dgrdData[i, ITM_ProductTable.DESCRIPTION_FLD].ToString() == string.Empty)
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD]);
							dgrdData.Focus();
							return false;
						}	
					}
					if (dgrdData[i, cst_FreightDetailTable.QUANTITY_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.QUANTITY_FLD]);
						dgrdData.Focus();
						return false;
					}
					else
					{
						if (decimal.Parse(dgrdData[i, cst_FreightDetailTable.QUANTITY_FLD].ToString()) <= 0)
						{
							string[] strParam = new string[2];
							strParam[0] = "Quantity";
							strParam[1] = "0";
							PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.QUANTITY_FLD]);
							dgrdData.Focus();
							return false;
						}
					}
					if (dgrdData[i, cst_FreightDetailTable.UNITPRICECIF_FLD].ToString() == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD]);
						dgrdData.Focus();
						return false;
					}
					else
					{
						if (intPurpose == (int)ACPurpose.ImportTax)
						{
//							if (decimal.Parse(dgrdData[i, AMOUNT_COLUMN].ToString()) < 0)
//							{
//								string[] strParam = new string[2];
//								strParam[0] = "Quantity";
//								strParam[1] = "0";
//								PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
//								dgrdData.Row = i;
//								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[AMOUNT_COLUMN]);
//								dgrdData.Focus();
//								return false;
//							}	
						}
						else
						{
//							if (decimal.Parse(dgrdData[i, AMOUNT_COLUMN].ToString()) <= 0)
//							{
//								string[] strParam = new string[2];
//								strParam[0] = "Quantity";
//								strParam[1] = "0";
//								PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
//								dgrdData.Row = i;
//								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[AMOUNT_COLUMN]);
//								dgrdData.Focus();
//								return false;
//							}
						}
					}
					if (intPurpose != (int)ACPurpose.ImportTax)
					{
						if (dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString() == string.Empty )
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[TOTAL_AMOUNT_COLUMN]);
							dgrdData.Focus();
							return false;
						}
						else
						{
//							if (decimal.Parse(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString()) <= 0)
//							{
//								string[] strParam = new string[2];
//								strParam[0] = "Quantity";
//								strParam[1] = "0";
//								PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
//								dgrdData.Row = i;
//								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[TOTAL_AMOUNT_COLUMN]);
//								dgrdData.Focus();
//								return false;
//							}
						}
					}
					//Check if Net Amount = Sum(Amount column)
					decSumOfAmount += decimal.Round(decimal.Parse(dgrdData[i, AMOUNT_COLUMN].ToString()),5);
				
				}

                //Check if Net Amount = Sum(Amount column)
				if ((intPurpose == (int)ACPurpose.Freight) && (decimal)txtNetAmount.Value != decimal.Round(decSumOfAmount, 2))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_FREIGHT_SUMOFAMOUNT_MUST_BE_EQUAL_NETAMOUNT, MessageBoxIcon.Warning);
					dgrdData.Row = dgrdData.RowCount;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[AMOUNT_COLUMN]);
					dgrdData.Focus();
					return false;
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
		/// btnSave_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, Feb 27 2006</date>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnHasError = true;
				if (!dgrdData.EditActive && ValidateData())
				{
//					if(Security.IsDifferencePrefix(this, lblWO_No,txtWONo))
//					{
//						return;
//					} 
					//Assign value to MasterVO
					voFreightMaster =  new cst_FreightMasterVO();
					voFreightMaster.CCNID = SystemProperty.CCNID;
					if ((txtOrderNo.Tag != null) && (txtOrderNo.Tag != DBNull.Value))
					{
						if (intFreightMasterID != 0)
						{
							voFreightMaster.FreightMasterID = int.Parse(txtOrderNo.Tag.ToString());
						}
					}
					voFreightMaster.CurrencyID = int.Parse(txtCurrency.Tag.ToString());
					voFreightMaster.ExchangeRate = (decimal) txtExchRate.Value;
					voFreightMaster.ACObjectID = int.Parse(txtObject.Tag.ToString());
					voFreightMaster.ACPurposeID = int.Parse(txtPurpose.Tag.ToString());
					if (txtMaker.Text != string.Empty)
					{
						voFreightMaster.MakerID = int.Parse(txtMaker.Tag.ToString());
					}
					else
						voFreightMaster.MakerID = 0;
					if (txtInvoiceNo.Text != string.Empty)
					{
						if (intPurpose == (int) ACPurpose.DebitNote && intObject == (int) ACObject.ReceiptTransaction)
						{
							voFreightMaster.ReceiveMasterID = 0;
							voFreightMaster.ReturnToVendorMasterID = int.Parse(txtInvoiceNo.Tag.ToString());
						}
						else
						{
							voFreightMaster.ReceiveMasterID = int.Parse(txtInvoiceNo.Tag.ToString());
							voFreightMaster.ReturnToVendorMasterID = 0;
						}
					}
					else
						voFreightMaster.ReceiveMasterID = 0;
					voFreightMaster.PostDate = (new DateTime(((DateTime)dtmPostDate.Value).Year, ((DateTime)dtmPostDate.Value).Month,((DateTime)dtmPostDate.Value).Day));
					if ((txtGrandTotal.Value != null) && (txtGrandTotal.Value != DBNull.Value))
					{
						voFreightMaster.GrandTotal = decimal.Round((decimal)txtGrandTotal.Value, 5);
					}
					else
						voFreightMaster.GrandTotal = 0;
					if ((txtSubTotal.Value != null) && (txtSubTotal.Value != DBNull.Value))
					{
						voFreightMaster.SubTotal = decimal.Round((decimal)txtSubTotal.Value,5);
					}
					else
						voFreightMaster.SubTotal = 0;
					if ((txtTotalVAT.Value != null) && (txtTotalVAT.Value != DBNull.Value))
					{
						voFreightMaster.TotalVAT = decimal.Round((decimal)txtTotalVAT.Value,5);
					}
					else
						voFreightMaster.TotalVAT = 0;
					voFreightMaster.TranNo = txtOrderNo.Text.Trim();
					voFreightMaster.Note = txtNote.Text.Trim();
					if (txtTransporterCode.Text != string.Empty)
					{
						voFreightMaster.TransporterID =  int.Parse(txtTransporterCode.Tag.ToString());
					}
					else
						voFreightMaster.TransporterID = 0;
					if (txtVendorCode.Text != string.Empty)
					{
						voFreightMaster.VendorID = int.Parse(txtVendorCode.Tag.ToString());
					}
					else
						voFreightMaster.VendorID = 0;
					if ((txtVAT.Value != null) && (txtVAT.Value != DBNull.Value))
					{
						voFreightMaster.VATPercent = (decimal)txtVAT.Value;
					}
					else
						voFreightMaster.VATPercent = 0;
					if (txtNetAmount.Value != DBNull.Value)
					{
						voFreightMaster.TotalAmount = (decimal)txtNetAmount.Value;
					}
					else
						voFreightMaster.TotalAmount = 0;
					//Save to database
					FreightBO boFreight = new FreightBO();
					switch (mFormMode)
					{
						case EnumAction.Add:
							//New a dataset to update
							DataSet dstToUpdate = dstGridData.Clone();
							foreach (DataRow drow in dstGridData.Tables[0].Rows)
							{
								if (drow.RowState == DataRowState.Deleted) continue;
								DataRow drowData = dstToUpdate.Tables[0].NewRow();
								drowData.ItemArray = drow.ItemArray;
								dstToUpdate.Tables[0].Rows.Add(drowData);
							}
							//syncronize data
							foreach (DataRow drow in dstToUpdate.Tables[0].Rows)
							{
								if (drow.RowState == DataRowState.Deleted) continue;
								if (drow[PO_InvoiceMasterTable.INVOICEMASTERID_FLD].ToString() == 0.ToString())
								{
									drow[PO_InvoiceMasterTable.INVOICEMASTERID_FLD] = DBNull.Value;
								}
							}
							intFreightMasterID = boFreight.AddAndReturnID(voFreightMaster, dstToUpdate);
							voFreightMaster.FreightMasterID = intFreightMasterID;
							break;
						case EnumAction.Edit:
							voFreightMaster.FreightMasterID = intFreightMasterID;
							boFreight.UpdateMasterAndDetail(voFreightMaster, dstGridData);
							break;
					}
					
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					mFormMode = EnumAction.Default;
					SwitchFormMode();
					//reload grid form database
					dstGridData = boFreight.GetFreightDetailByMasterID(intFreightMasterID);
					dgrdData.DataSource = dstGridData.Tables[0];
					//restore the layout of grid
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					if (intPurpose != (int)ACPurpose.Freight)
					{
						CalculateInformationInTheGrid();
					}
					else
					{
						//Calculate Total Amount
						for (int i = 0; i < dgrdData.RowCount; i++)
						{
							if (dgrdData[i, cst_FreightDetailTable.VATAMOUNT_FLD].ToString() != string.Empty)
							{
								dgrdData[i, TOTAL_AMOUNT_COLUMN] = Decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString()) 
									+ Decimal.Parse(dgrdData[i, cst_FreightDetailTable.VATAMOUNT_FLD].ToString());
							}
							else
								dgrdData[i, TOTAL_AMOUNT_COLUMN] = Decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString());
						}
					}
					dgrdData.Refresh();
					ConfigGrid(true);
					if (intPurpose == (int)ACPurpose.ImportTax)
					{
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].Visible = true;
					}
					else
					{
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].Visible = false;
					}
					if (intObject == (int)ACObject.ItemInventory)
					{
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].HeadingStyle.ForeColor  = Color.Maroon;
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].HeadingStyle.ForeColor  = Color.Maroon;
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor  = Color.Maroon;
					}
					else
					{
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].HeadingStyle.ForeColor  = Color.Black;
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].HeadingStyle.ForeColor  = Color.Black;
					}
					
					if ((intPurpose == (int)ACPurpose.CreditNote || intPurpose == (int)ACPurpose.DebitNote))
					{
						if (intObject == (int)ACObject.ItemInventory)
						{
							//show Inventory adjustment
							dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = true;
						}
						else
							dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
					}
					else
						dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
					
					if (txtInvoice.Text != string.Empty)
					{
						dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = true;
						dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor = Color.Maroon;
					}
					else
					{
						dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor = Color.Black;
						if (intPurpose == (int)ACPurpose.DebitNote && intObject == (int) ACObject.ItemInventory)
						{
							dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = true;
						}
						else
							dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
					}
					
					blnHasError = false;
					intPurpose = 0;
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					txtOrderNo.Focus();
				}
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
		/// dgrdData_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4 && btnSave.Enabled == true && intObject == (int)ACObject.ItemInventory)
				{
					dgrdData_ButtonClick(null, null);
				}
				if (e.KeyCode == Keys.Delete && dgrdData.RowCount > 0)
				{
					if (btnSave.Enabled)
					{
                        FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
                        int intCount = dstGridData.Tables[0].Rows.Cast<DataRow>().Count(objRow => objRow.RowState != DataRowState.Deleted);
					    CalculateInformationInTheGrid();
                        dgrdData.Refresh();
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
		/// Calculate information in the grid
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, Feb 27 2006</date>
		private void CalculateInformationInTheGrid()
		{
			const string METHOD_NAME = THIS + ".CalculateInformationInTheGrid()";
			try
			{
				decimal decTotalVat = 0;
				decimal decSubTotal = 0;
				decimal decGrandTotal = 0;
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					//Calculate Vat Column
					if(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString() != string.Empty)
					{
						if (txtVAT.Text.Trim() != string.Empty)
						{
							dgrdData[i, VAT_AMOUNT_COLUMN] = decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString())
								* ((decimal)txtVAT.Value)/100 ;
						}
						
					}
					//Calculate Total Amount Column
					if(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString() != string.Empty)
					{
						if ((dgrdData[i, VAT_AMOUNT_COLUMN].ToString() != string.Empty))
						{
							dgrdData[i, TOTAL_AMOUNT_COLUMN] = Decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString())
								+ decimal.Parse(dgrdData[i, VAT_AMOUNT_COLUMN].ToString());	
						}
						else
							dgrdData[i, TOTAL_AMOUNT_COLUMN] = Decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString());
					}
					if(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString() != string.Empty)
					{
						decSubTotal += decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString());
					}
					if(dgrdData[i, VAT_AMOUNT_COLUMN].ToString() != string.Empty)
					{
						decTotalVat += decimal.Parse(dgrdData[i, VAT_AMOUNT_COLUMN].ToString());
					}
					if(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString() != string.Empty)
					{
						decGrandTotal += decimal.Parse(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString());
					}
				}
				txtTotalVAT.Value = decTotalVat;
				txtSubTotal.Value = decSubTotal;
				txtGrandTotal.Value = decGrandTotal;
				
				
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
		/// Calculate information in the grid
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, Feb 27 2006</date>
		private void CalculateWhenVATColumnsChange()
		{
			const string METHOD_NAME = THIS + ".CalculateWhenVATColumnsChange()";
			try
			{
				
				decimal decTotalVat = 0;
				decimal decSubTotal = 0;
				decimal decGrandTotal = 0;
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					//Calculate Total Amount Column
					if(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString() != string.Empty)
					{
						if ((dgrdData[i, VAT_AMOUNT_COLUMN].ToString() != string.Empty))
						{
							dgrdData[i, TOTAL_AMOUNT_COLUMN] = Decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString())
								+ decimal.Parse(dgrdData[i, VAT_AMOUNT_COLUMN].ToString());	
						}
						else
							dgrdData[i, TOTAL_AMOUNT_COLUMN] = Decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString());
					}
					if(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString() != string.Empty)
					{
						decSubTotal += decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString());
					}
					if(dgrdData[i, VAT_AMOUNT_COLUMN].ToString() != string.Empty)
					{
						decTotalVat += decimal.Parse(dgrdData[i, VAT_AMOUNT_COLUMN].ToString());
					}
					if(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString() != string.Empty)
					{
						decGrandTotal += decimal.Parse(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString());
					}
				}
				txtTotalVAT.Value = decTotalVat;
				txtSubTotal.Value = decSubTotal;
				txtGrandTotal.Value = decGrandTotal;
				
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
		/// dgrdData_BeforeColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				string strColumnName = e.Column.DataColumn.DataField;
				DataRowView drwResult = null;
				Hashtable htbCondition = new Hashtable();
				string[] strParam = new string[2];
				decimal decUnitPrice = 0;
				switch (strColumnName)
				{
					case ITM_ProductTable.CODE_FLD:
						# region Open ComNumber search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTFORFREIGHT, ITM_ProductTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString().Trim(), null, false);
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
					case ITM_ProductTable.DESCRIPTION_FLD:
						# region Open ComName search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTFORFREIGHT, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString().Trim(), null, false);
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
					case IV_AdjustmentTable.TRANSNO_FLD:
						# region Open Adjustment No search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							drwResult = FormControlComponents.OpenSearchForm(IV_AdjustmentTable.TABLE_NAME, IV_AdjustmentTable.TRANSNO_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString().Trim(), null, false);
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
					case PO_InvoiceMasterTable.INVOICENO_FLD:
						# region Open Invoice No search form
						if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
						{
							if (txtVendorCode.Text.Trim() == string.Empty)
							{
								strParam =  new string[2];
								strParam[0] = lblVendor.Text;
								strParam[1] = label1.Text;
								PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
								txtVendorCode.Focus();
								e.Cancel = true;
							}
							else
							{
								htbCondition.Add(PO_InvoiceMasterTable.PARTYID_FLD, txtVendorCode.Tag);
							}
							if (dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value.ToString() == string.Empty)
							{
								strParam =  new string[2];
								strParam[0] = dgrdData.Columns[ITM_ProductTable.CODE_FLD].Caption;
								strParam[1] = label1.Text;
								PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
								dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
								dgrdData.Focus();
								e.Cancel = true;
							}
							else
							{
								htbCondition.Add(PO_InvoiceDetailTable.PRODUCTID_FLD, dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value);
							}
							drwResult = FormControlComponents.OpenSearchForm(V_INVOICE, PO_InvoiceMasterTable.INVOICENO_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Value.ToString().Trim(), null, false);
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
					case cst_FreightDetailTable.QUANTITY_FLD:
						if (e.Column.DataColumn.Text == string.Empty)
						{
							return;
						}
						try
						{
							if (decimal.Parse(e.Column.DataColumn.Text) <= 0)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_WO_ORDERQUANTITY, MessageBoxIcon.Warning);
								e.Cancel = true;
							}
						}
						catch
						{
							e.Cancel = true;
						}
						break;
					case cst_FreightDetailTable.UNITPRICECIF_FLD:
						if (e.Column.DataColumn.Text == string.Empty)
						{
							return;
						}
						try
						{
							decUnitPrice = decimal.Parse(e.Column.DataColumn.Text);
						}
						catch
						{
							e.Cancel = true;
						}
						if (intPurpose == (int)ACPurpose.Freight || intPurpose == (int) ACPurpose.ImportTax)
						{
							if (decimal.Parse(e.Column.DataColumn.Text) <= 0)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_WO_ORDERQUANTITY, MessageBoxIcon.Warning);
								e.Cancel = true;
							}
						}
						break;
					case cst_FreightDetailTable.AMOUNT_FLD:
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
						}
						break;
					case cst_FreightDetailTable.VATAMOUNT_FLD:
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
		/// dgrdData_AfterColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				DataRow drwResult = (DataRow) e.Column.DataColumn.Tag;
				//Fill Data to ComNumber
				if (e.Column.DataColumn.DataField == ITM_ProductTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.REVISION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = null;
						dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						FillItemDataToGrid(drwResult);
						
					}
				}
				//Fill Data to ComName
				if(e.Column.DataColumn.DataField == ITM_ProductTable.DESCRIPTION_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.REVISION_FLD].Value	 = string.Empty;
						dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = null;
						dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;

						FillItemDataToGrid(drwResult);
					}
				}
				//Fill Data to Adjustment No.
				if(e.Column.DataColumn.DataField == IV_AdjustmentTable.TRANSNO_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, IV_AdjustmentTable.TRANSNO_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[IV_AdjustmentTable.TRANSNO_FLD].Value = string.Empty;
						dgrdData.Columns[IV_AdjustmentTable.ADJUSTMENTID_FLD].Value = null;
					}
					else
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row, IV_AdjustmentTable.ADJUSTMENTID_FLD] = int.Parse(drwResult[IV_AdjustmentTable.ADJUSTMENTID_FLD].ToString());
						dgrdData[dgrdData.Row, IV_AdjustmentTable.TRANSNO_FLD] = drwResult[IV_AdjustmentTable.TRANSNO_FLD];
						
					}
				}
				//Fill Data to Invoice No.
				if(e.Column.DataColumn.DataField == PO_InvoiceMasterTable.INVOICENO_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, PO_InvoiceMasterTable.INVOICENO_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[PO_InvoiceMasterTable.INVOICENO_FLD].Value = string.Empty;
						dgrdData.Columns[PO_InvoiceMasterTable.INVOICEMASTERID_FLD].Value = null;
					}
					else
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row, PO_InvoiceMasterTable.INVOICEMASTERID_FLD] = int.Parse(drwResult[PO_InvoiceMasterTable.INVOICEMASTERID_FLD].ToString());
						dgrdData[dgrdData.Row, PO_InvoiceMasterTable.INVOICENO_FLD] = drwResult[PO_InvoiceMasterTable.INVOICENO_FLD];
						
					}
				}
				//Calculate Amount Column
				if (e.Column.DataColumn.DataField == cst_FreightDetailTable.QUANTITY_FLD 
					|| e.Column.DataColumn.DataField == cst_FreightDetailTable.UNITPRICECIF_FLD)
				{
					if (dgrdData.Columns[cst_FreightDetailTable.QUANTITY_FLD].Value  != DBNull.Value
						&& dgrdData.Columns[cst_FreightDetailTable.UNITPRICECIF_FLD].Value != DBNull.Value)
					{
						if (intPurpose == (int)ACPurpose.ImportTax)
						{
							if (dgrdData.Columns[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].Value != DBNull.Value)
							{
								dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value = Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.QUANTITY_FLD].Value)
									* Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.UNITPRICECIF_FLD].Value)* Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].Value)/100;
							}
							else
								dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value = 0;
						}
						else
						{
							dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value = Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.QUANTITY_FLD].Value)
								* Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.UNITPRICECIF_FLD].Value);
						}
						if (dgrdData.Columns[ITM_ProductTable.VAT_FLD].Value != DBNull.Value)
						{
							dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value = Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value)
							* Convert.ToDecimal(dgrdData.Columns[ITM_ProductTable.VAT_FLD].Value)/100;
						}
						else
							dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value = DBNull.Value;
						//Update information in the grid
						if (dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value != DBNull.Value)
						{
							dgrdData.Columns[TOTAL_AMOUNT_COLUMN].Value = Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value)
								+ Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value);
						}
						else
							dgrdData.Columns[TOTAL_AMOUNT_COLUMN].Value = Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value);
						decimal decTotalVat = 0;
						decimal decSubTotal = 0;
						decimal decGrandTotal = 0;
						for (int i = 0; i < dgrdData.RowCount; i++)
						{
							if(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString() != string.Empty)
							{
								decSubTotal += decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString());
							}
							if(dgrdData[i, VAT_AMOUNT_COLUMN].ToString() != string.Empty)
							{
								decTotalVat += decimal.Parse(dgrdData[i, VAT_AMOUNT_COLUMN].ToString());
							}
							if(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString() != string.Empty)
							{
								decGrandTotal += decimal.Parse(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString());
							}
						}
						txtTotalVAT.Value = decTotalVat;
						txtSubTotal.Value = decSubTotal;
						txtGrandTotal.Value = decGrandTotal;
					}
					
				}
				//Amount
				if (e.Column.DataColumn.DataField == cst_FreightDetailTable.AMOUNT_FLD)
				{
					if((dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value == DBNull.Value))
					{
						return;
					}
					else
					{
						if (intObject == (int)ACObject.ItemInventory)
						{
							CalculateInforWhenObjectIsItems();
						}
						else
						{
							//update Vat Amount
							if (intPurpose == (int)ACPurpose.ImportTax)
							{
								if (dgrdData.Columns[ITM_ProductTable.VAT_FLD].Value != DBNull.Value)
								{
									dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value = Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value)
										* Convert.ToDecimal(dgrdData.Columns[ITM_ProductTable.VAT_FLD].Value)/100;
								}
								else
									dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value = DBNull.Value;
							}
							//Update information in the grid
							if (dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value != DBNull.Value)
							{
								dgrdData.Columns[TOTAL_AMOUNT_COLUMN].Value = Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value)
									+ Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value);
							}
							else
								dgrdData.Columns[TOTAL_AMOUNT_COLUMN].Value = Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value);
							decimal decTotalVat = 0;
							decimal decSubTotal = 0;
							decimal decGrandTotal = 0;
							for (int i = 0; i < dgrdData.RowCount; i++)
							{
								if(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString() != string.Empty)
								{
									decSubTotal += decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString());
								}
								if(dgrdData[i, VAT_AMOUNT_COLUMN].ToString() != string.Empty)
								{
									decTotalVat += decimal.Parse(dgrdData[i, VAT_AMOUNT_COLUMN].ToString());
								}
								if(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString() != string.Empty)
								{
									decGrandTotal += decimal.Parse(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString());
								}
							}
							txtTotalVAT.Value = decTotalVat;
							txtSubTotal.Value = decSubTotal;
							txtGrandTotal.Value = decGrandTotal;
						}
						dgrdData.Refresh();
					}
				}
				if (e.Column.DataColumn.DataField == cst_FreightDetailTable.VATAMOUNT_FLD)
				{
					if((dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value == DBNull.Value))
					{
						return;
					}
					else
					{
						//Update information in the grid
						if (dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value != DBNull.Value)
						{
							dgrdData.Columns[TOTAL_AMOUNT_COLUMN].Value = Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value)
								+ Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value);
						}
						else
							dgrdData.Columns[TOTAL_AMOUNT_COLUMN].Value = DBNull.Value;
						decimal decTotalVat = 0;
						decimal decSubTotal = 0;
						decimal decGrandTotal = 0;
						for (int i = 0; i < dgrdData.RowCount; i++)
						{
							if(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString() != string.Empty)
							{
								decSubTotal += decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString());
							}
							if(dgrdData[i, VAT_AMOUNT_COLUMN].ToString() != string.Empty)
							{
								decTotalVat += decimal.Parse(dgrdData[i, VAT_AMOUNT_COLUMN].ToString());
							}
							if(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString() != string.Empty)
							{
								decGrandTotal += decimal.Parse(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString());
							}
						}
						txtTotalVAT.Value = decTotalVat;
						txtSubTotal.Value = decSubTotal;
						txtGrandTotal.Value = decGrandTotal;
//						CalculateWhenVATColumnsChange();
						dgrdData.Refresh();
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
		/// CalculateInforWhenObjectIsItems
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, May 19 2006</date>
		private void CalculateInforWhenObjectIsItems()
		{
			const string METHOD_NAME = THIS + ".CalculateInforWhenObjectIsItems()";
			try
			{
				if (dgrdData.Columns[ITM_ProductTable.VAT_FLD].Value != DBNull.Value)
				{
					dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value = Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value)
						* Convert.ToDecimal(dgrdData.Columns[ITM_ProductTable.VAT_FLD].Value)/100;
				}
				if (dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value != DBNull.Value)
				{
					dgrdData.Columns[TOTAL_AMOUNT_COLUMN].Value = Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value)
						+ Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.VATAMOUNT_FLD].Value);
				}
				else
					dgrdData.Columns[TOTAL_AMOUNT_COLUMN].Value = Convert.ToDecimal(dgrdData.Columns[cst_FreightDetailTable.AMOUNT_FLD].Value);
				decimal decTotalVat = 0;
				decimal decSubTotal = 0;
				decimal decGrandTotal = 0;
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					if(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString() != string.Empty)
					{
						decSubTotal += decimal.Parse(dgrdData[i, cst_FreightDetailTable.AMOUNT_FLD].ToString());
					}
					if(dgrdData[i, VAT_AMOUNT_COLUMN].ToString() != string.Empty)
					{
						decTotalVat += decimal.Parse(dgrdData[i, VAT_AMOUNT_COLUMN].ToString());
					}
					if(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString() != string.Empty)
					{
						decGrandTotal += decimal.Parse(dgrdData[i, TOTAL_AMOUNT_COLUMN].ToString());
					}
				}
				txtTotalVAT.Value = decTotalVat;
				txtSubTotal.Value = decSubTotal;
				txtGrandTotal.Value = decGrandTotal;

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
		/// txtCurrency_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void txtCurrency_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4) 
			{
				btnCurrency_Click(sender, e);
			}
		}
		/// <summary>
		/// txtOrderNo_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void txtOrderNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if((e.KeyCode == Keys.F4) && (btnOrderNo.Enabled))
			{
				btnOrderNo_Click(sender, e);
			}
		}
		/// <summary>
		/// txtTransporterCode_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void txtTransporterCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4) 
			{
				btnTransporterCode_Click(sender, e);
			}
		}
		/// <summary>
		/// txtTransporterName_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void txtTransporterName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4) 
			{
				btnTransporterName_Click(sender, e);
			}
		}
		/// <summary>
		/// txtVendorCode_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void txtVendorCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4) 
			{
				btnVendorCode_Click(sender, e);
			}
		}
		/// <summary>
		/// txtVendorName_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void txtVendorName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4) 
			{
				btnVendorName_Click(sender, e);
			}
		}
		
		/// <summary>
		/// txtInvoiceNo_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void txtInvoiceNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4) 
			{
				btnInvoiceNo_Click(sender, e);
			}
		}
		/// <summary>
		/// btnEdit_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				mFormMode = EnumAction.Edit;
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
		/// btnDelete_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
//				if(Security.NoRightToDeleteTransaction(this, PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD, voWorkOrderMaster.WorkOrderMasterID))
//				{
//					return;
//				}
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					FreightBO boFreight = new FreightBO();
					boFreight.DeleteMasterAndDetail(intFreightMasterID, dstGridData);
					intFreightMasterID = 0;
					mFormMode = EnumAction.Default;
					ClearForm();
					SwitchFormMode();
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					intFreightMasterID = 0;
					dstGridData = boFreight.GetFreightDetailByMasterID(0);
					dgrdData.DataSource = dstGridData.Tables[0];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
					ConfigGrid(true);
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
		/// <date>Tuesday, Feb 28 2006</date>
		private void txtOrderNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtOrderNo_Validating()";
			try
			{
				if (btnOrderNo.Enabled)
				{
					if (txtOrderNo.Text.Trim() == string.Empty)
					{
						
						ClearForm();
						CreateDataSet();
						txtOrderNo.Tag = null;
						dgrdData.DataSource = dstGridData.Tables[0];
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
						return;
					}
					if (!txtOrderNo.Modified) return;
					DataRowView drwResult = null;
					Hashtable htbCondition = new Hashtable();
				
					if (cboCCN.SelectedValue != null)
					{
						htbCondition.Add(cst_FreightMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
					}
					else
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
						cboCCN.Focus();
						return;
					}
					drwResult = FormControlComponents.OpenSearchForm(cst_FreightMasterTable.TABLE_NAME, cst_FreightMasterTable.TRANNO_FLD, txtOrderNo.Text.Trim(), htbCondition, false);
					if (drwResult != null)
					{
						txtOrderNo.Text = drwResult[cst_FreightMasterTable.TRANNO_FLD].ToString();
						intFreightMasterID = int.Parse(drwResult[cst_FreightMasterTable.FREIGHTMASTERID_FLD].ToString());
						txtOrderNo.Tag = intFreightMasterID;
						FillDataByMasterID(intFreightMasterID);
						btnEdit.Enabled = true;
						btnDelete.Enabled = true;
					}
					else
						e.Cancel = true;
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
		/// Freight_Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void Freight_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".Freight_Closing()";
			try
			{
				if (mFormMode != EnumAction.Default)
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
		/// dgrdData_AfterDelete
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
		private void dgrdData_AfterDelete(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterDelete()";
			try
			{
				CalculateInformationInTheGrid();
				dgrdData.Refresh();
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
		/// txtNetAmount_Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Mar 1 2006</date>
		private void txtNetAmount_Leave(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtNetAmount_Leave()";
			try
			{
				if (dgrdData.RowCount == 0) return;
				dstGridData = CalculateGrid(dstGridData); 
				dgrdData.Refresh();
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
		/// btnAllocate_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 9 2006</date>
		private void btnAllocate_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAllocate_Click()";
			try
			{
				if (txtNetAmount.Value == DBNull.Value)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					txtNetAmount.Focus();
					return;
				}
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_ASK_FOR_ALLOCATE_COST, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if (dgrdData.RowCount == 0) return;
					dstGridData = CalculateGrid(dstGridData); 
					dgrdData.Refresh();
					string[] pstrParam = new string[1];
					pstrParam[0] = "Allocating data";
					PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED, MessageBoxIcon.Information, pstrParam);
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
		/// Show Freight Slip Report (Original author: DungLA)
		/// </summary>
		/// <author> Tuan TQ. 13 July, 2006</author>
		/// <remarks>Original author is Dung LA. Tuan TQ only copy code from Print_Click method for printing multi-report purpose </remarks>
		private void ShowFreightSlipReport(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ShowFreightSlipReport()";
			string REPORT_FILE = Application.StartupPath + @"\ReportDefinition\FreightSlip.xml";

			try
			{
				while (rptFreightSlip.IsBusy)
					Application.DoEvents();
				if (!File.Exists(REPORT_FILE))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_FILE_NOT_FOUND, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				this.Cursor = Cursors.WaitCursor;
				Utils objUtils = new Utils();
				string[] arrstrReportInDefinitionFile = rptFreightSlip.GetReportInfo(REPORT_FILE);
				rptFreightSlip.Load(REPORT_FILE, arrstrReportInDefinitionFile[0]);
				arrstrReportInDefinitionFile = null;
				rptFreightSlip.DataSource.ConnectionString = Utils.Instance.OleDbConnectionString;
				rptFreightSlip.DataSource.RecordSource += " WHERE cst_FreightMaster.FreightMasterID = " + intFreightMasterID.ToString()
					+ " ORDER BY ITM_Product.Revision, ITM_Product.Code, ITM_Product.Description";
				try
				{
					rptFreightSlip.Fields["fldCompany"].Text = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
				}
				catch{}
				rptFreightSlip.Render();
				C1PrintPreviewDialog dlgPreview = new C1PrintPreviewDialog();
				dlgPreview.ReportViewer.PreviewNavigationPanel.Visible = false;
				dlgPreview.ReportViewer.Document = rptFreightSlip.Document;
				dlgPreview.FormTitle = this.Text;
				dlgPreview.Show();
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
				//Set cursor to default status
				this.Cursor = Cursors.Default;
			}
		}
		
		/// <summary>
		/// Build and show Debit Note Report
		/// </summary>
		/// <Author> Tuan TQ, 13 July, 2006</Author>
		private void ShowDebitNoteReport(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ShowDebitNoteReport()";
			try
			{	
				const string REPORT_NAME = "DebitNoteReport";
				const string REPORT_LAYOUT_FILE = "DebitNoteReport.xml";
				const string REPORTFLD_TITLE = "fldTitle";				
				
				const string APPLICATION_PATH  = @"PCSMain\bin\Debug";
			
				const string REPORTFLD_COMPANY = "fldCompany";
				const string REPORTFLD_ADDRESS			= "fldAddress";
				const string REPORTFLD_TEL				= "fldTel";
				const string REPORTFLD_FAX				= "fldFax";			
			
				if(intFreightMasterID <= 0)
				{
					return;
				}				
				
				//Change cursor to wait status
				this.Cursor = Cursors.WaitCursor;

				C1PrintPreviewDialog   printPreview = new C1PrintPreviewDialog();
				C1PrintPreviewDialogBO boDataReport = new C1PrintPreviewDialogBO();
			
				DataTable dtbResult;
				dtbResult = boDataReport.GetDebitNoteData(intFreightMasterID);
				
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
					strReportPath += "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
			
				//Set datasource and lay-out path for reports
				reportBuilder.ReportName = REPORT_NAME;
				reportBuilder.SourceDataTable = dtbResult;				
				reportBuilder.ReportDefinitionFolder = strReportPath;
			
				reportBuilder.ReportLayoutFile = REPORT_LAYOUT_FILE;

				//check if layout is valid
				if(reportBuilder.AnalyseLayoutFile())
				{					
					reportBuilder.UseLayoutFile = true;
				}
				else
				{
					//Set cursor to default
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
				reportBuilder.DrawPredefinedField(REPORTFLD_ADDRESS, SystemProperty.SytemParams.Get(SystemParam.ADDRESS));
				reportBuilder.DrawPredefinedField(REPORTFLD_TEL, SystemProperty.SytemParams.Get(SystemParam.TEL));
				reportBuilder.DrawPredefinedField(REPORTFLD_FAX, SystemProperty.SytemParams.Get(SystemParam.FAX));

				reportBuilder.RefreshReport();
			
				//Print report
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(REPORTFLD_TITLE).Text;
				}
				catch
				{}

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
				//Set cursor to default status
				this.Cursor = Cursors.Default;
			}
		}
		/// <summary>
		/// Build and show Credit Note Report
		/// </summary>
		/// <Author> Trada, 2 August, 2006</Author>
		private void ShowCreditNoteReport(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ShowCreditNoteReport()";
			try
			{	
				const string REPORT_NAME = "CreditNoteReport";
				const string REPORT_LAYOUT_FILE = "CreditNoteReport.xml";
				const string REPORTFLD_TITLE = "fldTitle";				
				
				const string APPLICATION_PATH  = @"PCSMain\bin\Debug";
			
				const string REPORTFLD_COMPANY = "fldCompany";
				const string REPORTFLD_ADDRESS			= "fldAddress";
				const string REPORTFLD_TEL				= "fldTel";
				const string REPORTFLD_FAX				= "fldFax";			
			
				if(intFreightMasterID <= 0)
				{
					return;
				}				
				
				//Change cursor to wait status
				this.Cursor = Cursors.WaitCursor;

				C1PrintPreviewDialog   printPreview = new C1PrintPreviewDialog();
				C1PrintPreviewDialogBO boDataReport = new C1PrintPreviewDialogBO();
			
				DataTable dtbResult;
				dtbResult = boDataReport.GetCreditNoteData(intFreightMasterID);
				
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
					strReportPath += "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
			
				//Set datasource and lay-out path for reports
				reportBuilder.ReportName = REPORT_NAME;
				reportBuilder.SourceDataTable = dtbResult;				
				reportBuilder.ReportDefinitionFolder = strReportPath;
			
				reportBuilder.ReportLayoutFile = REPORT_LAYOUT_FILE;

				//check if layout is valid
				if(reportBuilder.AnalyseLayoutFile())
				{					
					reportBuilder.UseLayoutFile = true;
				}
				else
				{
					//Set cursor to default
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
				reportBuilder.DrawPredefinedField(REPORTFLD_ADDRESS, SystemProperty.SytemParams.Get(SystemParam.ADDRESS));
				reportBuilder.DrawPredefinedField(REPORTFLD_TEL, SystemProperty.SytemParams.Get(SystemParam.TEL));
				reportBuilder.DrawPredefinedField(REPORTFLD_FAX, SystemProperty.SytemParams.Get(SystemParam.FAX));

				reportBuilder.RefreshReport();
			
				//Print report
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(REPORTFLD_TITLE).Text;
				}
				catch
				{}

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
				//Set cursor to default status
				this.Cursor = Cursors.Default;
			}
		}
		/// <summary>
		/// Build and show Debit Note Report
		/// </summary>
		/// <Author> Trada, 2 August, 2006</Author>
		private void ShowDebitNoteWithItemInventoryObjectReport(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ShowDebitNoteWithItemInventoryObjectReport()";
			try
			{	
				const string REPORT_NAME = "DebitNoteWithItemInventoryObject";
				const string REPORT_LAYOUT_FILE = "DebitNoteWithItemInventoryObject.xml";
				const string REPORTFLD_TITLE = "fldTitle";				
				
				const string APPLICATION_PATH  = @"PCSMain\bin\Debug";
			
				const string REPORTFLD_COMPANY = "fldCompany";
				const string REPORTFLD_ADDRESS			= "fldAddress";
				const string REPORTFLD_TEL				= "fldTel";
				const string REPORTFLD_FAX				= "fldFax";			
			
				if(intFreightMasterID <= 0)
				{
					return;
				}				
				
				//Change cursor to wait status
				this.Cursor = Cursors.WaitCursor;

				C1PrintPreviewDialog   printPreview = new C1PrintPreviewDialog();
				C1PrintPreviewDialogBO boDataReport = new C1PrintPreviewDialogBO();
			
				DataTable dtbResult;
				dtbResult = boDataReport.GetCreditNoteData(intFreightMasterID);
				
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
					strReportPath += "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
				}				
			
				//Set datasource and lay-out path for reports
				reportBuilder.ReportName = REPORT_NAME;
				reportBuilder.SourceDataTable = dtbResult;				
				reportBuilder.ReportDefinitionFolder = strReportPath;
			
				reportBuilder.ReportLayoutFile = REPORT_LAYOUT_FILE;

				//check if layout is valid
				if(reportBuilder.AnalyseLayoutFile())
				{					
					reportBuilder.UseLayoutFile = true;
				}
				else
				{
					//Set cursor to default
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
				reportBuilder.DrawPredefinedField(REPORTFLD_ADDRESS, SystemProperty.SytemParams.Get(SystemParam.ADDRESS));
				reportBuilder.DrawPredefinedField(REPORTFLD_TEL, SystemProperty.SytemParams.Get(SystemParam.TEL));
				reportBuilder.DrawPredefinedField(REPORTFLD_FAX, SystemProperty.SytemParams.Get(SystemParam.FAX));

				reportBuilder.RefreshReport();
			
				//Print report
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(REPORTFLD_TITLE).Text;
				}
				catch
				{}

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
				//Set cursor to default status
				this.Cursor = Cursors.Default;
			}
		}

		/// <summary>
		/// Displays Freight Slip report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>DungLA</author>
		/// <date>14-Mar-2006</date>
		/// <remarks>Update by Tuan TQ: 12 Jul, 2006. Multi-report printing</remarks>
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPrint_Click()";
			
			try
			{
				//Print nothing if not select item
				if(intFreightMasterID <= 0) return;

				//Only print if has purpose and object info
				if(txtPurpose.Tag != null && txtObject.Tag != null)
				{
					if(int.Parse(txtPurpose.Tag.ToString()) == (int)ACPurpose.DebitNote 
					&& int.Parse(txtObject.Tag.ToString()) == (int)ACObject.ReceiptTransaction)
					{
						//Show debit note report
						ShowDebitNoteReport(sender, e);
					}
					else
					{
						//Other else, show default report which was created by DungLA
						//ShowFreightSlipReport(sender, e);
					}
					if (int.Parse(txtPurpose.Tag.ToString()) == (int)ACPurpose.CreditNote
						&& int.Parse(txtObject.Tag.ToString()) == (int)ACObject.ItemInventory)
					{
						//Show credit note report
						ShowCreditNoteReport(sender, e);
					}
					if (int.Parse(txtPurpose.Tag.ToString()) == (int)ACPurpose.DebitNote
						&& int.Parse(txtObject.Tag.ToString()) == (int)ACObject.ItemInventory)
					{
						//Show debit note report
						ShowDebitNoteWithItemInventoryObjectReport(sender, e);
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
		/// ClearAdjustmentID
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, July 14 2006</date>
		private void ClearAdjustmentID()
		{
			for(int i = 0; i < dgrdData.RowCount; i++)
			{
				dgrdData[dgrdData.Row, IV_AdjustmentTable.ADJUSTMENTID_FLD] = null;
			}
		}
		/// <summary>
		/// ClearAdjustmentID
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, July 14 2006</date>
		private void ClearInvoiceMasterID()
		{
			for(int i = 0; i < dgrdData.RowCount; i++)
			{
				dgrdData[dgrdData.Row, PO_InvoiceMasterTable.INVOICEMASTERID_FLD] = null;
			}
		}
		/// <summary>
		/// btnPurpose_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 17 2006</date>
		private void btnPurpose_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPurpose_Click()";
			try
			{
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm("enm_ACPurpose", "Description", txtPurpose.Text.Trim(), null, true);
				if (drwResult != null)
				{
					if ((txtPurpose.Tag != null) && (txtPurpose.Tag != DBNull.Value)
						&& (int.Parse(txtPurpose.Tag.ToString()) != int.Parse(drwResult["ACPurposeID" ].ToString())))
					{
						//clear all data
						txtTransporterCode.Tag = null;
						txtTransporterCode.Text = string.Empty;
						txtTransporterName.Text = string.Empty;
						txtVendorCode.Text = string.Empty;
						txtMaker.Text = string.Empty;
						txtMaker.Tag = null;
						txtMakerName.Text = string.Empty;
						txtVendorCode.Tag = null;
						txtVendorName.Text = string.Empty;
						txtInvoiceNo.Text = string.Empty;
						txtPONo.Text = string.Empty;
						txtInvoice.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						//clear dataset
						if (dstGridData.Tables[0].Rows.Count > 0)
						{
							for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
							{
								if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
								{
									dstGridData.Tables[0].Rows[i].Delete();
								}
							}
						}
					}
					txtPurpose.Tag = drwResult["ACPurposeID"];
					txtPurpose.Text = drwResult["Description"].ToString();
					switch (int.Parse(drwResult["ACPurposeID"].ToString()))
					{
						case (int)ACPurpose.Freight: //If Freight then
							intPurpose = 1; //Freight Mode
							txtNetAmount.PostValidation.Intervals.AddRange(objInterval2);
							txtObject.Text = "ReceiptTransaction";
							txtObject.Tag = 1;
							intObject = 1;
							txtObject.Enabled = false;
							btnObject.Enabled = false;
							dgrdData.AllowAddNew = false;
							txtTransporterCode.Enabled = true;
							txtTransporterName.Enabled = true;
							btnTransporterCode.Enabled = true;
							btnTransporterName.Enabled = true;
							lblTransporter.ForeColor = Color.Maroon;
							lblVendor.ForeColor = Color.Maroon;
							txtNetAmount.Enabled = true;
							lblNetAmount.ForeColor = Color.Maroon;
							txtInvoiceNo.Text = string.Empty;
							txtInvoiceNo.Enabled = true;
							txtInvoice.Text = string.Empty;
							txtInvoiceNo.Tag = null;
							btnInvoiceNo.Enabled = true;
							txtVAT.Enabled = true;
							lblInvoiceNo.Text = "Receipt No.";
							btnAllocate.Enabled = true;
							//Config some columns to select items
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = false;
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = false;
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor = Color.Black;
							dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
							dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
							ClearAdjustmentID();
							ClearInvoiceMasterID();
							break;
						case (int) ACPurpose.ImportTax:
							intPurpose = 2; //Import Tax Mode
							txtNetAmount.PostValidation.Intervals.AddRange(objInterval1);
							txtObject.Text = "ReceiptTransaction";
							txtObject.Tag = 1;
							intObject = 1;
							txtObject.Enabled = false;
							btnObject.Enabled = false;	
							txtTransporterCode.Text = string.Empty;
							txtTransporterCode.Tag = null;
							txtTransporterName.Text = string.Empty;
							txtTransporterCode.Enabled = false;
							txtTransporterName.Enabled = false;
							btnTransporterCode.Enabled = false;
							lblTransporter.ForeColor = Color.Black;
							lblVendor.ForeColor = Color.Maroon;
							btnTransporterName.Enabled = false;
							txtNetAmount.Value = null;
							txtNetAmount.Enabled = false;
							lblNetAmount.ForeColor = Color.Black;
							txtVAT.Value = null;
							txtInvoiceNo.Text = string.Empty;
							txtInvoiceNo.Enabled = true;
							lblInvoiceNo.Text = "Receipt No.";
							txtInvoice.Text = string.Empty;
							txtInvoiceNo.Tag = null;
							btnInvoiceNo.Enabled = true;
							txtVAT.Enabled = false;
							btnAllocate.Enabled = false;
							dgrdData.AllowAddNew = false;
							//Config some columns to select items
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = false;
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = false;
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor = Color.Black;
							dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
							dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
							ClearAdjustmentID();
							ClearInvoiceMasterID();
							break;
						case (int)ACPurpose.CreditNote: 
							intPurpose = 3; //CreditNote Mode
							txtNetAmount.PostValidation.Intervals.AddRange(objInterval1);
							//invisible Import Tax column
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].Visible = false;
							txtTransporterCode.Enabled = true;
							txtTransporterName.Enabled = true;
							txtObject.Enabled = true;
							txtObject.Text = string.Empty;
							btnObject.Enabled = true;	
							btnTransporterCode.Enabled = true;
							btnTransporterName.Enabled = true;
							lblTransporter.ForeColor = Color.Black;
							lblVendor.ForeColor = Color.Maroon;
							txtInvoiceNo.Text = string.Empty;
							txtInvoiceNo.Enabled = true;
							txtInvoice.Text = string.Empty;
							txtInvoiceNo.Tag = null;
							btnInvoiceNo.Enabled = true;
							lblInvoiceNo.Text = "Receipt No.";
							lblNetAmount.ForeColor = Color.Black;
							txtNetAmount.Enabled = false;
							txtVAT.Enabled = false;
							btnAllocate.Enabled = false;
							dgrdData.AllowAddNew = false;
							if (intObject == 2)
							{
								//visible Adjustment column
								dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = true;
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = true;
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor = Color.Maroon;
							}
							else
							{
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
								ClearInvoiceMasterID();
								dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
								//Clear IDs
								ClearAdjustmentID();
							}
//							dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = true;
//							//ClearInvoiceMasterID();
							break;
						case (int)ACPurpose.DebitNote: 
							intPurpose = 4; //CreditNote Mode
							txtNetAmount.PostValidation.Intervals.AddRange(objInterval1);
							//invisible Import Tax column
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].Visible = false;
							txtTransporterCode.Enabled = true;
							txtTransporterName.Enabled = true;
							txtObject.Text = string.Empty;
							txtObject.Enabled = true;
							btnObject.Enabled = true;	
							lblTransporter.ForeColor = Color.Black;
							lblVendor.ForeColor = Color.Maroon;
							btnTransporterCode.Enabled = true;
							txtInvoiceNo.Text = string.Empty;
							txtInvoiceNo.Enabled = true;
							txtInvoice.Text = string.Empty;
							txtInvoiceNo.Tag = null;
							btnInvoiceNo.Enabled = true;
							btnTransporterName.Enabled = true;
							txtNetAmount.Enabled = false;
							lblNetAmount.ForeColor = Color.Black;
							txtVAT.Enabled = false;
							btnAllocate.Enabled = false;
							dgrdData.AllowAddNew = false;
							if (intObject == 2)
							{
								//visible Adjustment column
								dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = true;
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = true;
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor = Color.Black;
							}
							else
							{
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
								dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
								ClearAdjustmentID();
								ClearInvoiceMasterID();
							}
							break;
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
		/// btnObject_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 17 2006</date>
        private void btnObject_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnObject_Click()";
			try
			{
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm("enm_ACObject", "Description", txtObject.Text.Trim(), null, true);
				if (drwResult != null)
				{
					if ((txtObject.Tag != null) && (txtObject.Tag != DBNull.Value)
						&& (int.Parse(txtObject.Tag.ToString()) != int.Parse(drwResult["ACObjectID" ].ToString())))
					{
						//clear all data
						txtTransporterCode.Tag = null;
						txtTransporterCode.Text = string.Empty;
						txtTransporterName.Text = string.Empty;
						txtVendorCode.Text = string.Empty;
						txtMaker.Text = string.Empty;
						txtMaker.Tag = null;
						txtMakerName.Text = string.Empty;
						txtVendorCode.Tag = null;
						txtVendorName.Text = string.Empty;
						txtInvoiceNo.Text = string.Empty;
						txtPONo.Text = string.Empty;
						txtInvoice.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						//clear dataset
						if (dstGridData.Tables[0].Rows.Count > 0)
						{
							for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
							{
								if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
								{
									dstGridData.Tables[0].Rows[i].Delete();
								}
							}
						}
					}
					txtObject.Tag = drwResult["ACObjectID"];
					txtObject.Text = drwResult["Description"].ToString();
					if (int.Parse(drwResult["ACObjectID"].ToString()) == (int)ACObject.ReceiptTransaction)
					{
						intObject = 1; //Receipt Transaction
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].HeadingStyle.ForeColor = Color.Black;
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].HeadingStyle.ForeColor = Color.Black;
						
						txtTransporterCode.Text = string.Empty;
						txtTransporterName.Text = string.Empty;
						txtTransporterCode.Tag = null;
						txtTransporterCode.Enabled = false;
						txtTransporterName.Enabled = false;
						btnTransporterCode.Enabled = false;
						btnTransporterName.Enabled = false;
						btnInvoiceNo.Enabled = true;
						lblInvoiceNo.ForeColor = Color.Maroon;
						txtInvoiceNo.Enabled = true;
						//Disable Net Amount
						txtNetAmount.Enabled = false;
						txtNetAmount.Value = DBNull.Value;
						lblNetAmount.ForeColor = Color.Black;
						if (intPurpose == (int)ACPurpose.CreditNote || intPurpose == (int)ACPurpose.DebitNote)
						{
							btnAllocate.Enabled = false;
						}
						else
							btnAllocate.Enabled = true;
						if (intPurpose == (int)ACPurpose.DebitNote)
						{
							lblInvoiceNo.Text = "Return No.";
						}
						else
							lblInvoiceNo.Text = "Receipt No.";
						txtMaker.Enabled = false;
						txtMakerName.Enabled = false;
						btnMaker.Enabled = false;
						btnMakerName.Enabled = false;
						txtVAT.Value = DBNull.Value;	
						txtVAT.Enabled = false;
						dgrdData.AllowAddNew = false;
						//Config some columns to select items
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = false;
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = false;
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor = Color.Black;
						dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
						dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
						ClearAdjustmentID();
						ClearInvoiceMasterID();
					}
					else
						if (int.Parse(drwResult["ACObjectID"].ToString()) == (int)ACObject.ItemInventory)
						{
							intObject = 2; //Item Inventory
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].HeadingStyle.ForeColor = Color.Maroon;
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].HeadingStyle.ForeColor = Color.Maroon;
							txtInvoiceNo.Text = string.Empty;
							txtInvoiceNo.Tag = null;
							txtInvoice.Text = string.Empty;
							txtPONo.Text = string.Empty;
							btnInvoiceNo.Enabled = false;
							txtInvoiceNo.Enabled = false;
							txtMaker.Enabled = true;
							lblInvoiceNo.Text = "Receipt No.";
							txtMakerName.Enabled = true;
							btnMaker.Enabled = true;
							btnMakerName.Enabled = true;
							txtNetAmount.Value = DBNull.Value;
							lblNetAmount.ForeColor = Color.Black;
							lblVendor.ForeColor = Color.Black;
							lblInvoiceNo.ForeColor = Color.Black;
							txtNetAmount.Enabled = false;
							txtVAT.Value = DBNull.Value;
							txtTransporterCode.Enabled = true;
							txtTransporterName.Enabled = true;
							btnTransporterCode.Enabled = true;
							btnTransporterName.Enabled = true;
							txtVAT.Enabled = false;
							btnAllocate.Enabled = false;
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor = Color.Black;
							dgrdData.AllowAddNew = true;
							//Unlock some columns
							for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
							{
								dgrdData.Splits[0].DisplayColumns[i].Locked = true;
							}
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Locked = false;
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Locked = false;
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.QUANTITY_FLD].Locked = false;
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].Locked = false;
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.AMOUNT_FLD].Locked = false;
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.VATAMOUNT_FLD].Locked = false;
							dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Locked = false;
							dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Locked = false;
							//Config some columns to select items
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor = Color.Maroon;
							if (intPurpose == 3 || intPurpose == 4)
							{
								//visible Adjustment column
								dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = true;
							}
							else
							{
								dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
								ClearAdjustmentID();
							}
							if (intPurpose == (int)ACPurpose.DebitNote)
							{
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = true;
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor = Color.Black;
							}
							else
							{
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
								ClearInvoiceMasterID();
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
		/// txtPurpose_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 17 2006</date>
		private void txtPurpose_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPurpose_Validating()";
			try
			{
				if (txtPurpose.Text == string.Empty)
				{
					//clear all data
					txtTransporterCode.Tag = null;
					txtTransporterCode.Text = string.Empty;
					txtTransporterName.Text = string.Empty;
					txtVendorCode.Text = string.Empty;
					txtMaker.Text = string.Empty;
					txtMaker.Tag = null;
					txtMakerName.Text = string.Empty;
					txtVendorCode.Tag = null;
					txtVendorName.Text = string.Empty;
					txtInvoiceNo.Text = string.Empty;
					txtPONo.Text = string.Empty;
					txtInvoice.Text = string.Empty;
					txtInvoiceNo.Tag = null;
					txtObject.Text = string.Empty;
					txtObject.Tag = null;
					txtObject.Enabled = true;
					btnObject.Enabled = true;
					txtSubTotal.Value = DBNull.Value;
					txtGrandTotal.Value = DBNull.Value;
					txtTotalVAT.Value = DBNull.Value;
					//clear dataset
					if (dstGridData.Tables[0].Rows.Count > 0)
					{
						for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
						{
							if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
							{
								dstGridData.Tables[0].Rows[i].Delete();
							}
						}
					}
					return;
				}
				if (!txtPurpose.Modified) return;
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm("enm_ACPurpose", "Description", txtPurpose.Text.Trim(), null, false);
				if (drwResult != null)
				{
					//clear all data
					txtTransporterCode.Tag = null;
					txtTransporterCode.Text = string.Empty;
					txtTransporterName.Text = string.Empty;
					txtVendorCode.Text = string.Empty;
					txtMaker.Text = string.Empty;
					txtMaker.Tag = null;
					txtMakerName.Text = string.Empty;
					txtVendorCode.Tag = null;
					txtVendorName.Text = string.Empty;
					txtInvoiceNo.Text = string.Empty;
					txtPONo.Text = string.Empty;
					txtInvoice.Text = string.Empty;
					txtInvoiceNo.Tag = null;
					//clear dataset
					if (dstGridData.Tables[0].Rows.Count > 0)
					{
						for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
						{
							if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
							{
								dstGridData.Tables[0].Rows[i].Delete();
							}
						}
					}
					txtPurpose.Tag = drwResult["ACPurposeID"];
					txtPurpose.Text = drwResult["Description"].ToString();
					switch (int.Parse(drwResult["ACPurposeID"].ToString()))
					{
						case (int)ACPurpose.Freight: //If Freight then
							intPurpose = 1; //Freight Mode
							txtNetAmount.PostValidation.Intervals.AddRange(objInterval2);
							txtObject.Text = "ReceiptTransaction";
							txtObject.Tag = 1;
							intObject = 1;
							txtObject.Enabled = false;
							btnObject.Enabled = false;
							dgrdData.AllowAddNew = false;
							txtTransporterCode.Enabled = true;
							txtTransporterName.Enabled = true;
							btnTransporterCode.Enabled = true;
							btnTransporterName.Enabled = true;
							lblTransporter.ForeColor = Color.Maroon;
							lblVendor.ForeColor = Color.Maroon;
							txtNetAmount.Enabled = true;
							txtVAT.Enabled = true;
							btnAllocate.Enabled = true;
							//Config some columns to select items
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = false;
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = false;
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor = Color.Black;
							dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
							dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
							ClearAdjustmentID();
							ClearInvoiceMasterID();
							break;
						case (int) ACPurpose.ImportTax:
							intPurpose = 2; //Import Tax Mode
							txtNetAmount.PostValidation.Intervals.AddRange(objInterval1);
							txtObject.Text = "ReceiptTransaction";
							txtObject.Tag = 1;
							intObject = 1;
							txtObject.Enabled = false;
							btnObject.Enabled = false;	
							txtTransporterCode.Text = string.Empty;
							txtTransporterCode.Tag = null;
							txtTransporterName.Text = string.Empty;
							txtTransporterCode.Enabled = false;
							txtTransporterName.Enabled = false;
							lblTransporter.ForeColor = Color.Black;
							lblVendor.ForeColor = Color.Maroon;
							btnTransporterCode.Enabled = false;
							btnTransporterName.Enabled = false;
							txtNetAmount.Value = null;
							txtNetAmount.Enabled = false;
							txtVAT.Value = null;
							txtVAT.Enabled = false;
							btnAllocate.Enabled = false;
							dgrdData.AllowAddNew = false;
							//Config some columns to select items
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = false;
							dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = false;
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor = Color.Black;
							dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
							dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
							ClearAdjustmentID();
							ClearInvoiceMasterID();
							break;
						case (int)ACPurpose.CreditNote: 
							intPurpose = 3; //CreditNote Mode
							txtNetAmount.PostValidation.Intervals.AddRange(objInterval1);
							txtObject.Enabled = true;
							btnObject.Enabled = true;
							//invisible Import Tax column
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].Visible = false;
							txtTransporterCode.Enabled = true;
							txtTransporterName.Enabled = true;
							btnTransporterCode.Enabled = true;
							lblTransporter.ForeColor = Color.Black;
							lblVendor.ForeColor = Color.Maroon;
							btnTransporterName.Enabled = true;
							txtNetAmount.Enabled = false;
							txtVAT.Enabled = false;
							btnAllocate.Enabled = false;
							dgrdData.AllowAddNew = false;
							if (intObject == 2)
							{
								//visible Adjustment column
								dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = true;
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = true;
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor = Color.Maroon;
							}
							else
							{
								dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
								ClearAdjustmentID();
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
								ClearInvoiceMasterID();
							}
							
							break;
						case (int)ACPurpose.DebitNote: 
							intPurpose = 4; //CreditNote Mode
							txtNetAmount.PostValidation.Intervals.AddRange(objInterval1);
							txtObject.Enabled = true;
							btnObject.Enabled = true;
							//invisible Import Tax column
							dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.IMPORTTAXPERCENT_FLD].Visible = false;
							txtTransporterCode.Enabled = true;
							txtTransporterName.Enabled = true;
							btnTransporterCode.Enabled = true;
							lblTransporter.ForeColor = Color.Black;
							lblVendor.ForeColor = Color.Maroon;
							btnTransporterName.Enabled = true;
							txtNetAmount.Enabled = false;
							txtVAT.Enabled = false;
							btnAllocate.Enabled = false;
							dgrdData.AllowAddNew = false;
							if (intObject == 2)
							{
								//visible Adjustment column
								dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = true;
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = true;
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor = Color.Black;
							}
							else
							{
								dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
								dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
								ClearAdjustmentID();
								ClearInvoiceMasterID();
							}

							break;
					}
				}
				else
					e.Cancel = true;
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
		/// txtObject_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 17 2006</date>
		private void txtObject_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtObject_Validating()";
			try
			{
				if (txtObject.Text == string.Empty)
				{
					//clear all data
					txtTransporterCode.Tag = null;
					txtTransporterCode.Text = string.Empty;
					txtTransporterName.Text = string.Empty;
					txtVendorCode.Text = string.Empty;
					txtMaker.Text = string.Empty;
					txtMaker.Tag = null;
					txtMakerName.Text = string.Empty;
					txtVendorCode.Tag = null;
					txtVendorName.Text = string.Empty;
					txtInvoiceNo.Text = string.Empty;
					txtPONo.Text = string.Empty;
					txtInvoice.Text = string.Empty;
					txtInvoiceNo.Tag = null;
					//clear dataset
					if (dstGridData.Tables[0].Rows.Count > 0)
					{
						for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
						{
							if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
							{
								dstGridData.Tables[0].Rows[i].Delete();
							}
						}
					}
					return;
				}
				if (!txtObject.Modified) return;
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm("enm_ACObject", "Description", txtObject.Text.Trim(), null, false);
				if (drwResult != null)
				{
					if ((txtObject.Tag != null) && (txtObject.Tag != DBNull.Value)
						&& (int.Parse(txtObject.Tag.ToString()) != int.Parse(drwResult["ACObjectID" ].ToString())))
					{
						//clear all data
						txtTransporterCode.Tag = null;
						txtTransporterCode.Text = string.Empty;
						txtTransporterName.Text = string.Empty;
						txtVendorCode.Text = string.Empty;
						txtMaker.Text = string.Empty;
						txtMaker.Tag = null;
						txtMakerName.Text = string.Empty;
						txtVendorCode.Tag = null;
						txtVendorName.Text = string.Empty;
						txtInvoiceNo.Text = string.Empty;
						txtPONo.Text = string.Empty;
						txtInvoice.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						//clear dataset
						if (dstGridData.Tables[0].Rows.Count > 0)
						{
							for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
							{
								if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
								{
									dstGridData.Tables[0].Rows[i].Delete();
								}
							}
						}
					}
					txtObject.Tag = drwResult["ACObjectID"];
					txtObject.Text = drwResult["Description"].ToString();
					if (int.Parse(drwResult["ACObjectID"].ToString()) == (int)ACObject.ReceiptTransaction)
					{
						intObject = 1; //Receipt Transaction
						txtTransporterCode.Text = string.Empty;
						txtTransporterName.Text = string.Empty;
						txtTransporterCode.Tag = null;
						txtTransporterCode.Enabled = false;
						txtTransporterName.Enabled = false;
						btnTransporterCode.Enabled = false;
						btnTransporterName.Enabled = false;
						btnInvoiceNo.Enabled = true;
						txtInvoiceNo.Enabled = true;
						txtNetAmount.Enabled = false;
						txtNetAmount.Value = DBNull.Value;
						lblNetAmount.ForeColor = Color.Black;
						if (intPurpose == (int)ACPurpose.CreditNote || intPurpose == (int)ACPurpose.DebitNote)
						{
							btnAllocate.Enabled = false;
						}
						else
							btnAllocate.Enabled = true;
						txtVAT.Value = DBNull.Value;
						txtVAT.Enabled = false;
						dgrdData.AllowAddNew = false;
						//Config some columns to select items
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = false;
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = false;
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor = Color.Black;
						dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
						dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
						ClearInvoiceMasterID();
						ClearAdjustmentID();
					}
					else
						if (int.Parse(drwResult["ACObjectID"].ToString()) == (int)ACObject.ItemInventory)
					{
						intObject = 2; //Item Inventory
						txtInvoiceNo.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						txtInvoice.Text = string.Empty;
						txtPONo.Text = string.Empty;
						btnInvoiceNo.Enabled = false;
						txtInvoiceNo.Enabled = false;
						txtNetAmount.Value = DBNull.Value;
						lblNetAmount.ForeColor = Color.Black;
						lblVendor.ForeColor = Color.Black;
						txtNetAmount.Enabled = false;
						txtVAT.Value = DBNull.Value;
						txtTransporterCode.Enabled = true;
						txtTransporterName.Enabled = true;
						btnTransporterCode.Enabled = true;
						btnTransporterName.Enabled = true;
						txtVAT.Enabled = false;
						btnAllocate.Enabled = false;
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor = Color.Black;
						dgrdData.AllowAddNew = true;
						//Unlock some columns
						for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
						{
							dgrdData.Splits[0].DisplayColumns[i].Locked = true;
						}
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.QUANTITY_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.AMOUNT_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.VATAMOUNT_FLD].Locked = false;
						//Config some columns to select items
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
						dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
						dgrdData.Splits[0].DisplayColumns[cst_FreightDetailTable.UNITPRICECIF_FLD].HeadingStyle.ForeColor = Color.Maroon;
						if (intPurpose == 3 || intPurpose == 4)
						{
							//visible Adjustment column
							dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = true;
						}
						else
						{
							dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.TRANSNO_FLD].Visible = false;
							ClearAdjustmentID();
						}
						if (intPurpose == (int)ACPurpose.DebitNote)
						{
							dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = true;
							dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].HeadingStyle.ForeColor = Color.Black;
						}
						else
						{
							dgrdData.Splits[0].DisplayColumns[PO_InvoiceMasterTable.INVOICENO_FLD].Visible = false;
							ClearInvoiceMasterID();
						}
					}
				}
				else
					e.Cancel = true;
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
		/// btnMaker_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 17 2006</date>
		private void btnMaker_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMaker_Click()";
			try
			{
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				htbCondition.Add(VENDOR, (int) PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtMaker.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					if (txtMaker.Tag == null)
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						txtInvoice.Text = string.Empty;
						txtPONo.Text = string.Empty;
						if (intObject == (int)ACObject.ReceiptTransaction)
						{
							//clear dataset
							if (dstGridData.Tables[0].Rows.Count > 0)
							{
								for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
								{
									if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
									{
										dstGridData.Tables[0].Rows[i].Delete();
									}
								}
							}
						}
					}
					if ((txtMaker.Tag != null) && (txtMaker.Tag != DBNull.Value)
						&& (int.Parse(txtMaker.Tag.ToString()) != int.Parse(drwResult[MST_PartyTable.PARTYID_FLD ].ToString())))
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						txtInvoice.Text = string.Empty;
						txtPONo.Text = string.Empty;
						if (intObject == (int)ACObject.ReceiptTransaction)
						{
							//clear dataset
							if (dstGridData.Tables[0].Rows.Count > 0)
							{
								for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
								{
									if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
									{
										dstGridData.Tables[0].Rows[i].Delete();
									}
								}
							}
						}
					}
					txtMaker.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtMaker.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtMakerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
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
		/// txtMaker_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 17 2006</date>
		private void txtMaker_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMaker_Validating()";
			try
			{
				if (txtMaker.Text.Trim() == string.Empty)
				{
					
					
					if (intObject == (int)ACObject.ReceiptTransaction && txtMaker.Tag != null)
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtPONo.Text = string.Empty;
						txtInvoice.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						//clear dataset
						if (dstGridData.Tables[0].Rows.Count > 0)
						{
							for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
							{
								if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
								{
									dstGridData.Tables[0].Rows[i].Delete();
								}
							}
						}
					}
					txtMaker.Tag = null;
					txtMakerName.Text = string.Empty;
					return;
					
				}
				if (!txtMaker.Modified) return;
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				htbCondition.Add(VENDOR, (int) PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.CODE_FLD, txtMaker.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					if (txtMaker.Tag == null)
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						txtInvoice.Text = string.Empty;
						txtPONo.Text = string.Empty;
						if (intObject == (int)ACObject.ReceiptTransaction)
						{
							//clear dataset
							if (dstGridData.Tables[0].Rows.Count > 0)
							{
								for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
								{
									if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
									{
										dstGridData.Tables[0].Rows[i].Delete();
									}
								}
							}
						}
					}
					if ((txtMaker.Tag != null) && (txtMaker.Tag != DBNull.Value)
						&& (int.Parse(txtMaker.Tag.ToString()) != int.Parse(drwResult[MST_PartyTable.PARTYID_FLD ].ToString())))
					{
						//clear information relate
						txtInvoice.Text = string.Empty;
						txtPONo.Text = string.Empty;
						txtInvoiceNo.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						if (intObject == (int)ACObject.ReceiptTransaction)
						{
							//clear dataset
							if (dstGridData.Tables[0].Rows.Count > 0)
							{
								for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
								{
									if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
									{
										dstGridData.Tables[0].Rows[i].Delete();
									}
								}
							}
						}
					}
					txtMaker.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtMaker.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtMakerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
				}
				else
					e.Cancel = true;
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
		/// btnMakerName_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 17 2006</date>
		private void btnMakerName_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMakerName_Click()";
			try
			{
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				htbCondition.Add(VENDOR, (int) PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtMakerName.Text.Trim(), htbCondition, true);
				if (drwResult != null)
				{
					if (txtMaker.Tag == null)
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						txtInvoice.Text = string.Empty;
						txtPONo.Text = string.Empty;
						if (intObject == (int)ACObject.ReceiptTransaction)
						{
							//clear dataset
							if (dstGridData.Tables[0].Rows.Count > 0)
							{
								for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
								{
									if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
									{
										dstGridData.Tables[0].Rows[i].Delete();
									}
								}
							}
						}
					}
					if ((txtMaker.Tag != null) && (int.Parse(txtMaker.Tag.ToString()) != int.Parse(drwResult[MST_PartyTable.PARTYID_FLD ].ToString())))
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						txtInvoice.Text = string.Empty;
						txtPONo.Text = string.Empty;
						if (intObject == (int)ACObject.ReceiptTransaction)
						{
							//clear dataset
							if (dstGridData.Tables[0].Rows.Count > 0)
							{
								for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
								{
									if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
									{
										dstGridData.Tables[0].Rows[i].Delete();
									}
								}
							}
						}
					}
					txtMaker.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtMaker.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtMakerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
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
		/// txtMakerName_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 17 2006</date>
		private void txtMakerName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMakerName_Validating()";
			try
			{
				if (txtMakerName.Text.Trim() == string.Empty)
				{
					if (intObject == (int)ACObject.ReceiptTransaction && txtMaker.Tag != null) 
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtPONo.Text = string.Empty;
						txtInvoice.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						//clear dataset
						if (dstGridData.Tables[0].Rows.Count > 0)
						{
							for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
							{
								if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
								{
									dstGridData.Tables[0].Rows[i].Delete();
								}
							}
						}
					}
					txtMaker.Tag = null;
					txtMaker.Text = string.Empty;
					return;
				}
				if (!txtVendorName.Modified) return;
				Hashtable htbCondition = new Hashtable();
				DataRowView drwResult = null;
				htbCondition.Add(VENDOR, (int) PartyTypeEnum.VENDOR);
				drwResult = FormControlComponents.OpenSearchForm(V_VENDORCUSTOMER, MST_PartyTable.NAME_FLD, txtMakerName.Text.Trim(), htbCondition, false);
				if (drwResult != null)
				{
					if (txtMaker.Tag == null)
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						txtInvoice.Text = string.Empty;
						txtPONo.Text = string.Empty;
						if (intObject == (int)ACObject.ReceiptTransaction)
						{
							//clear dataset
							if (dstGridData.Tables[0].Rows.Count > 0)
							{
								for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
								{
									if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
									{
										dstGridData.Tables[0].Rows[i].Delete();
									}
								}
							}
						}
					}
					if ((txtMaker.Tag != null) && (int.Parse(txtMaker.Tag.ToString()) != int.Parse(drwResult[MST_PartyTable.PARTYID_FLD ].ToString())))
					{
						//clear information relate
						txtInvoiceNo.Text = string.Empty;
						txtInvoiceNo.Tag = null;
						txtInvoice.Text = string.Empty;
						txtPONo.Text = string.Empty;
						if (intObject == (int)ACObject.ReceiptTransaction)
						{
							//clear dataset
							if (dstGridData.Tables[0].Rows.Count > 0)
							{
								for (int i = dstGridData.Tables[0].Rows.Count - 1; i >= 0; i--)
								{
									if (dstGridData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
									{
										dstGridData.Tables[0].Rows[i].Delete();
									}
								}
							}
						}
					}
					txtMaker.Tag = drwResult[MST_PartyTable.PARTYID_FLD];
					txtMaker.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtMakerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
				}
				else
					e.Cancel = true;
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
		/// txtMakerName_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 17 2006</date>
		private void txtMakerName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4) 
			{
				btnMakerName_Click(sender, e);
			}
		}
		/// <summary>
		/// txtMaker_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, May 17 2006</date>
		private void txtMaker_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F4) 
			{
				btnMaker_Click(sender, e);
			}
		}
		/// <summary>
		/// Freight_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, May 18 2006</date>
		private void Freight_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".FillItemDataToGrid()";
			try
			{
				if (e.KeyCode == Keys.F12 && intObject == (int)ACObject.ItemInventory)
				{
					dgrdData.Row = dgrdData.RowCount;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
					dgrdData.Focus();
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
		/// FillItemDataToGrid
		/// </summary>
		/// <param name="pdrowData"></param>
		/// <author>Trada</author>
		/// <date>Thursday, May 18 2006</date>
		private void FillItemDataToGrid(DataRow pdrowData)
		{
			const string METHOD_NAME = THIS + ".FillItemDataToGrid()";
			try
			{
				dgrdData.EditActive = true;
				dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = int.Parse(pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString());
				dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD] = pdrowData[ITM_ProductTable.CODE_FLD];
				dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD]	= pdrowData[ITM_ProductTable.DESCRIPTION_FLD];
				dgrdData.Columns[ITM_ProductTable.REVISION_FLD].Value = pdrowData[ITM_ProductTable.REVISION_FLD];
				dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value  = pdrowData[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
				dgrdData.Columns[cst_FreightDetailTable.BUYINGUMID_FLD].Value  = pdrowData[ITM_ProductTable.STOCKUMID_FLD];
				dgrdData.Columns[ITM_ProductTable.VAT_FLD].Value  = pdrowData[ITM_ProductTable.VAT_FLD];
				if (((int)ACPurpose.CreditNote == intPurpose || (int)ACPurpose.DebitNote == intPurpose) && 
						(int)ACObject.ItemInventory == intObject)
				{
					if (pdrowData[ITM_ProductTable.LISTPRICE_FLD].ToString() != string.Empty && 
						pdrowData[ITM_ProductTable.LISTPRICE_FLD] != DBNull.Value &&
						pdrowData[ITM_ProductTable.LISTPRICE_FLD].ToString() != null)
					{
						dgrdData.Columns[cst_FreightDetailTable.UNITPRICECIF_FLD].Value  = pdrowData[ITM_ProductTable.LISTPRICE_FLD];
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
		}
		/// <summary>
		/// dgrdData_ButtonClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, May 18 2006</date>
		private void dgrdData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition= new Hashtable();
				string[] strParam = new string[2];
				if (!btnSave.Enabled) return;
				//Select Item
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD])
					|| dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD]))
				{
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]))
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTFORFREIGHT, ITM_ProductTable.CODE_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD].ToString(), htbCondition, true);
						else
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTFORFREIGHT, ITM_ProductTable.DESCRIPTION_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD].ToString(), htbCondition, true);
					}
					else
					{
						if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]))
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTFORFREIGHT, ITM_ProductTable.CODE_FLD, dgrdData.Columns[ITM_ProductTable.CODE_FLD].Value.ToString().Trim(), htbCondition, true);
						else
							drwResult = FormControlComponents.OpenSearchForm(V_PRODUCTFORFREIGHT, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[ITM_ProductTable.DESCRIPTION_FLD].Value.ToString().Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						FillItemDataToGrid(drwResult.Row);
					}
				}
				//Select Adjustment No.
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[IV_AdjustmentTable.TRANSNO_FLD]))
				{
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(IV_AdjustmentTable.TABLE_NAME, IV_AdjustmentTable.TRANSNO_FLD, dgrdData[dgrdData.Row, IV_AdjustmentTable.TRANSNO_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(IV_AdjustmentTable.TABLE_NAME, IV_AdjustmentTable.TRANSNO_FLD, dgrdData.Columns[IV_AdjustmentTable.TRANSNO_FLD].Value.ToString().Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row, IV_AdjustmentTable.TRANSNO_FLD] = drwResult[IV_AdjustmentTable.TRANSNO_FLD].ToString();
						dgrdData[dgrdData.Row, IV_AdjustmentTable.ADJUSTMENTID_FLD] = int.Parse(drwResult[IV_AdjustmentTable.ADJUSTMENTID_FLD].ToString());
					}
				}
				//Select Invoice No.
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[PO_InvoiceMasterTable.INVOICENO_FLD]))
				{
					if (txtVendorCode.Text.Trim() == string.Empty)
					{
						strParam =  new string[2];
						strParam[0] = lblVendor.Text;
						strParam[1] = label1.Text;
						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
						txtVendorCode.Focus();
						return;
					}
					else
					{
						htbCondition.Add(PO_InvoiceMasterTable.PARTYID_FLD, txtVendorCode.Tag);
					}
					//if (dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value.ToString() == string.Empty)
					if (dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD].ToString() == string.Empty)
					{
						strParam =  new string[2];
						strParam[0] = dgrdData.Columns[ITM_ProductTable.CODE_FLD].Caption;
						strParam[1] = label1.Text;
						PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
						dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
						dgrdData.Focus();
						return;
					}
					else
					{
						htbCondition.Add(PO_InvoiceDetailTable.PRODUCTID_FLD, dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value);
					}
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(V_INVOICE, PO_InvoiceMasterTable.INVOICENO_FLD, dgrdData[dgrdData.Row, PO_InvoiceMasterTable.INVOICENO_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(V_INVOICE, PO_InvoiceMasterTable.INVOICENO_FLD, dgrdData.Columns[PO_InvoiceMasterTable.INVOICENO_FLD].Value.ToString().Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						dgrdData[dgrdData.Row, PO_InvoiceMasterTable.INVOICENO_FLD] = drwResult[PO_InvoiceMasterTable.INVOICENO_FLD].ToString();
						dgrdData[dgrdData.Row, PO_InvoiceMasterTable.INVOICEMASTERID_FLD] = int.Parse(drwResult[PO_InvoiceMasterTable.INVOICEMASTERID_FLD].ToString());
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

		private void txtObject_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4 && txtObject.Enabled ==  true)
			{
				btnObject_Click(null, null);
			}
		}

		private void txtPurpose_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4 && txtPurpose.Enabled ==  true)
			{
				btnPurpose_Click(null, null);
			}
		}

	}
}
