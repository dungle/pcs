using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1List;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.MasterSetup.SystemTable
{
	/// <summary>
	/// Summary description for Party.
	/// </summary>
	public class Party : Form
	{
		#region Declaration

		#region Form controls
		private System.Windows.Forms.TextBox txtVATCode;
		private System.Windows.Forms.Label lblVATCode;
		private System.Windows.Forms.TextBox txtPhone;
		private System.Windows.Forms.Label lblPhone;
		private System.Windows.Forms.TextBox txtBankAccount;
		private System.Windows.Forms.Label lblBankAccount;
		private System.Windows.Forms.TextBox txtFax;
		private System.Windows.Forms.Label lblFax;
		private C1.Win.C1List.C1Combo cboType;
		private C1.Win.C1List.C1Combo cboCountry;
		private C1.Win.C1List.C1Combo cboCity;		
		private Button btnSearchCode;
		private TextBox txtCode;
		private Label lblCode;
		private Button btnSearchName;
		private TextBox txtName;
		private Label lblName;
		private Button btnContacts;
		private Button btnEdit;
		private Button btnClose;
		private Button btnHelp;
		private Button btnDelete;
		private Button btnSave;
		private Button btnAdd;
		private CheckBox chkDelete;
		private Label lblType;
		private TextBox txtWebSite;
		private Label lblWebSite;
		private Button btnLocation;
		private Label lblCountry;
		private TextBox txtZIP;
		private Label lblZIP;
		private TextBox txtState;
		private Label lblState;
		private Label lblCity;
		private TextBox txtAddress;
		private Label lblAddress;
		private Label lblVendor;
		private Label lblCustomer;
		private Label lblBoth;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		
		#endregion

		private const string THIS = "PCSUtils.MasterSetup.SystemTable.Party";		
		private const string DOT = ".";
		
		UtilsBO boUtils = new UtilsBO();
		PartyBO boParty = new PartyBO();		
		private MST_PartyVO voParty = new MST_PartyVO();
		private DataTable tblCityData = new DataTable(MST_CityTable.TABLE_NAME);
		private bool blnDataIsValid = false;		

		#endregion Declaration

		private System.Windows.Forms.Label lblPaymentTerm;
		private C1.Win.C1List.C1Combo cboPaymentTerm;
		private System.Windows.Forms.Label lblCurrency;
		private System.Windows.Forms.TextBox txtCurrency;
		private System.Windows.Forms.Button btnCurrency;
		string strLastValidCurrency = string.Empty;
		private System.Windows.Forms.Label lblMAPBankAccountNo;
		private System.Windows.Forms.Label lblMAPBankAccountName;
		private System.Windows.Forms.TextBox txtMAPBankAccNo;
		private System.Windows.Forms.TextBox txtMAPBankAccName;
        private System.Windows.Forms.Label lblVendorOutside;

		#region Properties
		
		private EnumAction mFormAction;
		public EnumAction FormAction
		{
			get { return mFormAction; }
			set { mFormAction = value; }
		}
		
		#endregion Properties

		#region Constructor, Destructor
		public Party()
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
		
		#endregion Constructor, Destructor

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Party));
            this.btnSearchCode = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.btnSearchName = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnContacts = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtWebSite = new System.Windows.Forms.TextBox();
            this.lblWebSite = new System.Windows.Forms.Label();
            this.btnLocation = new System.Windows.Forms.Button();
            this.lblCountry = new System.Windows.Forms.Label();
            this.txtZIP = new System.Windows.Forms.TextBox();
            this.lblZIP = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblVendor = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblBoth = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.lblFax = new System.Windows.Forms.Label();
            this.txtVATCode = new System.Windows.Forms.TextBox();
            this.lblVATCode = new System.Windows.Forms.Label();
            this.txtBankAccount = new System.Windows.Forms.TextBox();
            this.lblBankAccount = new System.Windows.Forms.Label();
            this.cboType = new C1.Win.C1List.C1Combo();
            this.cboCountry = new C1.Win.C1List.C1Combo();
            this.cboCity = new C1.Win.C1List.C1Combo();
            this.lblPaymentTerm = new System.Windows.Forms.Label();
            this.cboPaymentTerm = new C1.Win.C1List.C1Combo();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.txtCurrency = new System.Windows.Forms.TextBox();
            this.btnCurrency = new System.Windows.Forms.Button();
            this.txtMAPBankAccNo = new System.Windows.Forms.TextBox();
            this.lblMAPBankAccountNo = new System.Windows.Forms.Label();
            this.txtMAPBankAccName = new System.Windows.Forms.TextBox();
            this.lblMAPBankAccountName = new System.Windows.Forms.Label();
            this.lblVendorOutside = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cboType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPaymentTerm)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearchCode
            // 
            resources.ApplyResources(this.btnSearchCode, "btnSearchCode");
            this.btnSearchCode.Name = "btnSearchCode";
            this.btnSearchCode.Click += new System.EventHandler(this.btnSearchCode_Click);
            this.btnSearchCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Party_KeyDown);
            // 
            // txtCode
            // 
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            this.txtCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtCode_Validating);
            // 
            // lblCode
            // 
            this.lblCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCode.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblCode, "lblCode");
            this.lblCode.Name = "lblCode";
            // 
            // btnSearchName
            // 
            resources.ApplyResources(this.btnSearchName, "btnSearchName");
            this.btnSearchName.Name = "btnSearchName";
            this.btnSearchName.Click += new System.EventHandler(this.btnSearchName_Click);
            this.btnSearchName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Party_KeyDown);
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // lblName
            // 
            this.lblName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblName.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // btnContacts
            // 
            resources.ApplyResources(this.btnContacts, "btnContacts");
            this.btnContacts.Name = "btnContacts";
            this.btnContacts.Click += new System.EventHandler(this.btnContacts_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chkDelete
            // 
            resources.ApplyResources(this.chkDelete, "chkDelete");
            this.chkDelete.Name = "chkDelete";
            // 
            // lblType
            // 
            this.lblType.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblType, "lblType");
            this.lblType.Name = "lblType";
            // 
            // txtWebSite
            // 
            resources.ApplyResources(this.txtWebSite, "txtWebSite");
            this.txtWebSite.Name = "txtWebSite";
            this.txtWebSite.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtWebSite.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblWebSite
            // 
            this.lblWebSite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblWebSite.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblWebSite, "lblWebSite");
            this.lblWebSite.Name = "lblWebSite";
            // 
            // btnLocation
            // 
            resources.ApplyResources(this.btnLocation, "btnLocation");
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // lblCountry
            // 
            this.lblCountry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCountry.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblCountry, "lblCountry");
            this.lblCountry.Name = "lblCountry";
            // 
            // txtZIP
            // 
            resources.ApplyResources(this.txtZIP, "txtZIP");
            this.txtZIP.Name = "txtZIP";
            this.txtZIP.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtZIP.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblZIP
            // 
            this.lblZIP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblZIP.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblZIP, "lblZIP");
            this.lblZIP.Name = "lblZIP";
            // 
            // txtState
            // 
            resources.ApplyResources(this.txtState, "txtState");
            this.txtState.Name = "txtState";
            this.txtState.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtState.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblState
            // 
            this.lblState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblState.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblState, "lblState");
            this.lblState.Name = "lblState";
            // 
            // lblCity
            // 
            this.lblCity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCity.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblCity, "lblCity");
            this.lblCity.Name = "lblCity";
            // 
            // txtAddress
            // 
            resources.ApplyResources(this.txtAddress, "txtAddress");
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtAddress.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblAddress
            // 
            this.lblAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAddress.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblAddress, "lblAddress");
            this.lblAddress.Name = "lblAddress";
            // 
            // lblVendor
            // 
            resources.ApplyResources(this.lblVendor, "lblVendor");
            this.lblVendor.Name = "lblVendor";
            // 
            // lblCustomer
            // 
            resources.ApplyResources(this.lblCustomer, "lblCustomer");
            this.lblCustomer.Name = "lblCustomer";
            // 
            // lblBoth
            // 
            resources.ApplyResources(this.lblBoth, "lblBoth");
            this.lblBoth.Name = "lblBoth";
            // 
            // txtPhone
            // 
            resources.ApplyResources(this.txtPhone, "txtPhone");
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtPhone.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblPhone
            // 
            this.lblPhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPhone.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblPhone, "lblPhone");
            this.lblPhone.Name = "lblPhone";
            // 
            // txtFax
            // 
            resources.ApplyResources(this.txtFax, "txtFax");
            this.txtFax.Name = "txtFax";
            this.txtFax.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtFax.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblFax
            // 
            this.lblFax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFax.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblFax, "lblFax");
            this.lblFax.Name = "lblFax";
            // 
            // txtVATCode
            // 
            resources.ApplyResources(this.txtVATCode, "txtVATCode");
            this.txtVATCode.Name = "txtVATCode";
            this.txtVATCode.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtVATCode.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblVATCode
            // 
            this.lblVATCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblVATCode.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblVATCode, "lblVATCode");
            this.lblVATCode.Name = "lblVATCode";
            // 
            // txtBankAccount
            // 
            resources.ApplyResources(this.txtBankAccount, "txtBankAccount");
            this.txtBankAccount.Name = "txtBankAccount";
            this.txtBankAccount.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtBankAccount.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblBankAccount
            // 
            this.lblBankAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBankAccount.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblBankAccount, "lblBankAccount");
            this.lblBankAccount.Name = "lblBankAccount";
            // 
            // cboType
            // 
            this.cboType.AddItemSeparator = ';';
            resources.ApplyResources(this.cboType, "cboType");
            this.cboType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboType.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboType.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboType.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboType.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboType.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboType.Images.Add(((System.Drawing.Image)(resources.GetObject("cboType.Images"))));
            this.cboType.MatchEntryTimeout = ((long)(2000));
            this.cboType.MaxDropDownItems = ((short)(5));
            this.cboType.MaxLength = 32767;
            this.cboType.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboType.Name = "cboType";
            this.cboType.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboType.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboType.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboType.PropBag = resources.GetString("cboType.PropBag");
            // 
            // cboCountry
            // 
            this.cboCountry.AddItemSeparator = ';';
            resources.ApplyResources(this.cboCountry, "cboCountry");
            this.cboCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCountry.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCountry.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCountry.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCountry.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCountry.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCountry.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCountry.Images"))));
            this.cboCountry.MatchEntryTimeout = ((long)(2000));
            this.cboCountry.MaxDropDownItems = ((short)(5));
            this.cboCountry.MaxLength = 32767;
            this.cboCountry.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCountry.Name = "cboCountry";
            this.cboCountry.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCountry.SelectedValueChanged += new System.EventHandler(this.cboCountry_SelectedValueChanged);
            this.cboCountry.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboCountry.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboCountry.PropBag = resources.GetString("cboCountry.PropBag");
            // 
            // cboCity
            // 
            this.cboCity.AddItemSeparator = ';';
            resources.ApplyResources(this.cboCity, "cboCity");
            this.cboCity.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCity.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCity.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCity.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCity.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCity.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCity.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCity.Images"))));
            this.cboCity.MatchEntryTimeout = ((long)(2000));
            this.cboCity.MaxDropDownItems = ((short)(5));
            this.cboCity.MaxLength = 32767;
            this.cboCity.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCity.Name = "cboCity";
            this.cboCity.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCity.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboCity.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboCity.PropBag = resources.GetString("cboCity.PropBag");
            // 
            // lblPaymentTerm
            // 
            this.lblPaymentTerm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPaymentTerm.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblPaymentTerm, "lblPaymentTerm");
            this.lblPaymentTerm.Name = "lblPaymentTerm";
            // 
            // cboPaymentTerm
            // 
            this.cboPaymentTerm.AddItemSeparator = ';';
            resources.ApplyResources(this.cboPaymentTerm, "cboPaymentTerm");
            this.cboPaymentTerm.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboPaymentTerm.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboPaymentTerm.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboPaymentTerm.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboPaymentTerm.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPaymentTerm.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPaymentTerm.Images.Add(((System.Drawing.Image)(resources.GetObject("cboPaymentTerm.Images"))));
            this.cboPaymentTerm.MatchEntryTimeout = ((long)(2000));
            this.cboPaymentTerm.MaxDropDownItems = ((short)(5));
            this.cboPaymentTerm.MaxLength = 32767;
            this.cboPaymentTerm.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboPaymentTerm.Name = "cboPaymentTerm";
            this.cboPaymentTerm.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboPaymentTerm.PropBag = resources.GetString("cboPaymentTerm.PropBag");
            // 
            // lblCurrency
            // 
            this.lblCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCurrency.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblCurrency, "lblCurrency");
            this.lblCurrency.Name = "lblCurrency";
            // 
            // txtCurrency
            // 
            resources.ApplyResources(this.txtCurrency, "txtCurrency");
            this.txtCurrency.Name = "txtCurrency";
            this.txtCurrency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrency_KeyDown);
            this.txtCurrency.Validating += new System.ComponentModel.CancelEventHandler(this.txtCurrency_Validating);
            // 
            // btnCurrency
            // 
            resources.ApplyResources(this.btnCurrency, "btnCurrency");
            this.btnCurrency.Name = "btnCurrency";
            this.btnCurrency.Click += new System.EventHandler(this.btnCurrency_Click);
            // 
            // txtMAPBankAccNo
            // 
            resources.ApplyResources(this.txtMAPBankAccNo, "txtMAPBankAccNo");
            this.txtMAPBankAccNo.Name = "txtMAPBankAccNo";
            // 
            // lblMAPBankAccountNo
            // 
            this.lblMAPBankAccountNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblMAPBankAccountNo.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblMAPBankAccountNo, "lblMAPBankAccountNo");
            this.lblMAPBankAccountNo.Name = "lblMAPBankAccountNo";
            // 
            // txtMAPBankAccName
            // 
            resources.ApplyResources(this.txtMAPBankAccName, "txtMAPBankAccName");
            this.txtMAPBankAccName.Name = "txtMAPBankAccName";
            // 
            // lblMAPBankAccountName
            // 
            this.lblMAPBankAccountName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblMAPBankAccountName.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblMAPBankAccountName, "lblMAPBankAccountName");
            this.lblMAPBankAccountName.Name = "lblMAPBankAccountName";
            // 
            // lblVendorOutside
            // 
            resources.ApplyResources(this.lblVendorOutside, "lblVendorOutside");
            this.lblVendorOutside.Name = "lblVendorOutside";
            // 
            // Party
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.txtMAPBankAccName);
            this.Controls.Add(this.lblMAPBankAccountName);
            this.Controls.Add(this.txtMAPBankAccNo);
            this.Controls.Add(this.lblMAPBankAccountNo);
            this.Controls.Add(this.btnCurrency);
            this.Controls.Add(this.txtCurrency);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.cboPaymentTerm);
            this.Controls.Add(this.lblPaymentTerm);
            this.Controls.Add(this.cboCity);
            this.Controls.Add(this.cboCountry);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.txtBankAccount);
            this.Controls.Add(this.txtVATCode);
            this.Controls.Add(this.txtFax);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtWebSite);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtZIP);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.lblBankAccount);
            this.Controls.Add(this.lblVATCode);
            this.Controls.Add(this.lblFax);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.btnLocation);
            this.Controls.Add(this.lblWebSite);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.chkDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSearchName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnSearchCode);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.btnContacts);
            this.Controls.Add(this.lblCountry);
            this.Controls.Add(this.lblZIP);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblBoth);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.lblVendor);
            this.Controls.Add(this.lblVendorOutside);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Party";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Party_Closing);
            this.Load += new System.EventHandler(this.Party_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Party_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.cboType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPaymentTerm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Private Methods
		//**************************************************************************              
		///    <Description>
		///       This method uses to intial data for combo boxs in form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void InitComboBoxs()
		{
//			try
//			{
				// get all city first
				tblCityData = boUtils.ListAllCity();
				// put data to Country Combo
				DataTable tblCountry = boUtils.ListCountry();
				
				FormControlComponents.PutDataIntoC1ComboBox(cboCountry, tblCountry, MST_CountryTable.CODE_FLD, MST_CountryTable.COUNTRYID_FLD, MST_CountryTable.TABLE_NAME, true);
				cboCountry.SelectedIndex = -1;

				// build a data table for Type combo
				DataTable dtbType = boParty.GetPartyType();
				
				// bind to combo
				FormControlComponents.PutDataIntoC1ComboBox(cboType, dtbType, enm_PartyTypeEnumTable.NAME_FLD, enm_PartyTypeEnumTable.VALUE_FLD, enm_PartyTypeEnumTable.TABLE_NAME, true);
				cboType.SelectedIndex = -1;
				// Paymentterm cbo
				DataTable dtbPaymentTerm = (new MST_PaymentTermDS()).List().Tables[0];
				FormControlComponents.PutDataIntoC1ComboBox(cboPaymentTerm, dtbPaymentTerm, MST_PaymentTermTable.CODE_FLD, MST_PaymentTermTable.PAYMENTTERMID_FLD, MST_PaymentTermTable.TABLE_NAME, true);
				cboPaymentTerm.SelectedIndex = -1;
//			}
//			catch (PCSException ex)
//			{
//				throw ex;
//			}
//			catch (Exception ex)
//			{
//				throw ex;
//			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to switching form mode based on form action
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void SwitchFormMode()
		{
			switch (mFormAction)
			{
				case EnumAction.Default:
					foreach (Control objControl in this.Controls)
					{
						if ((objControl is TextBox) || (objControl is ComboBox) 
							|| (objControl is C1Combo) || (objControl is CheckBox))
						{
							if (objControl != txtCode)
								objControl.Enabled = false;
						}
					}

					txtCode.Enabled = true;
					txtName.Enabled = true;
					txtMAPBankAccName.Enabled = false;
					txtMAPBankAccNo.Enabled = false;
					btnAdd.Enabled = true;
					btnSave.Enabled = false;
					btnCurrency.Enabled = false;
					if (voParty.PartyID > 0)
					{
						btnEdit.Enabled = true;
						btnLocation.Enabled = true;
						btnContacts.Enabled = true;
						btnDelete.Enabled = true;
					}
					else
					{
						btnLocation.Enabled = false;
						btnContacts.Enabled = false;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
					}

					btnSearchCode.Enabled = true;
					btnSearchName.Enabled  = true;
						
					break;

				default:
					foreach (Control objControl in this.Controls)
					{
						if ((objControl is TextBox) || (objControl is ComboBox) 
							|| (objControl is C1Combo) || (objControl is CheckBox))
							objControl.Enabled = true;
					}
						
					txtCode.Enabled = true && (mFormAction != EnumAction.Default);
					btnSave.Enabled = true;
					txtName.Enabled = true;
					btnCurrency.Enabled = true;
					btnSearchCode.Enabled = false;
					btnSearchName.Enabled  = false;
					btnLocation.Enabled = false;
					btnContacts.Enabled = false;
					btnAdd.Enabled = false;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					
					break;				
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to load all data from VO object to controls
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void LoadDataToControl(MST_PartyVO pvoParty)
		{
			if ((pvoParty != null) && (pvoParty.PartyID > 0))
			{ 
				// load all data from VO to controls
				txtCode.Text = pvoParty.Code;
				txtName.Text = pvoParty.Name;
				txtAddress.Text = pvoParty.Address;					
				cboType.SelectedValue = pvoParty.Type;
				cboCountry.SelectedValue = pvoParty.CountryID;
				cboCity.SelectedValue = pvoParty.CityID;
				txtState.Text = pvoParty.State;
				txtZIP.Text = pvoParty.ZipPost;
				txtWebSite.Text = pvoParty.WebSite;
				chkDelete.Checked = pvoParty.DeleteReason;
				txtPhone.Text = pvoParty.Phone;
				txtFax.Text = pvoParty.Fax;
				txtMAPBankAccName.Text = pvoParty.MAPBankAccountName;
				txtMAPBankAccNo.Text = pvoParty.MAPBankAccountNo;
				txtBankAccount.Text = pvoParty.BankAccount;

				//HACKED - Added by Tuan TQ. 08 Nov, 2005. Fix Error No. 2636.
				txtVATCode.Text = voParty.VATCode;
				//End
				cboPaymentTerm.SelectedValue = pvoParty.PaymentTermID;
					
				// currency
				if (voParty.CurrencyID > 0)
				{
					DataRowView drowView = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME, MST_CurrencyTable.CURRENCYID_FLD, voParty.CurrencyID.ToString(), null, false);
					FillCurrencyData(drowView.Row);
				}
				else
				{
					txtCurrency.Text = string.Empty;
					txtCurrency.Tag = null;
					strLastValidCurrency = string.Empty;
				}
			}
			else
			{
				// clear data in form
				foreach (Control objControl in this.Controls)
				{
					if (objControl is TextBox)
					{
						objControl.Text = string.Empty;
						objControl.Tag = null;
					}
					else if (objControl is CheckBox)
					{
						((CheckBox)objControl).Checked = false;
					}
					else if (objControl is C1Combo)
					{
						((C1Combo)objControl).SelectedIndex = -1;							
					}
				}
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to save data to database
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool SaveData()
		{
			voParty.Phone = txtPhone.Text.Trim();
			voParty.Fax = txtFax.Text.Trim();
			voParty.BankAccount = txtBankAccount.Text.Trim();
			voParty.Code = txtCode.Text.Trim();
			voParty.Name = txtName.Text.Trim();
			voParty.MAPBankAccountName = txtMAPBankAccName.Text;
			voParty.MAPBankAccountNo = txtMAPBankAccNo.Text;
			voParty.Address = txtAddress.Text.Trim();
			try
			{
				voParty.CityID = int.Parse(cboCity.SelectedValue.ToString().Trim());
			}
			catch
			{}
			try
			{
				voParty.PaymentTermID = Convert.ToInt32(cboPaymentTerm.SelectedValue);
			}
			catch
			{
				// edited dungla 08-06-2006: fix bug 4049 for Ngannt
				voParty.PaymentTermID = 0;
			}
			try
			{
				voParty.CurrencyID = Convert.ToInt32(txtCurrency.Tag);
			}
			catch
			{
				voParty.CurrencyID = 0;
			}
			
			voParty.State = txtState.Text.Trim();
			voParty.ZipPost = txtZIP.Text.Trim();
			try
			{
				voParty.CountryID = int.Parse(cboCountry.SelectedValue.ToString().Trim());
			}
			catch
			{}

			voParty.Type = int.Parse(cboType.SelectedValue.ToString());
			voParty.WebSite = txtWebSite.Text.Trim();
			voParty.DeleteReason = chkDelete.Checked;
				
			//HACKED - Added by Tuan TQ. 08 Nov, 2005. Fix Error No. 2636.
			voParty.VATCode = txtVATCode.Text.Trim();
			//END

			switch (mFormAction)
			{
				case EnumAction.Add:
					voParty.PartyID = boParty.AddReturnID(voParty);
					break;
				case EnumAction.Edit:
					boParty.Update(voParty);
					break;
			}
			return true;
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to save data to database
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void PutDataToCityCombo(DataRow[] pdrowData)
		{
			try
			{
				// clear data source and related info first
				cboCity.DataSource = null;
				DataTable tblData = new DataTable(MST_CityTable.TABLE_NAME);
				tblData.Columns.Add(MST_CityTable.CITYID_FLD);
				tblData.Columns.Add(MST_CityTable.CODE_FLD);
				tblData.Columns.Add(MST_CityTable.NAME_FLD);
				foreach (DataRow drowTemp in pdrowData)
				{
					DataRow drowNew = tblData.NewRow();
					drowNew[MST_CityTable.CITYID_FLD] = drowTemp[MST_CityTable.CITYID_FLD];
					drowNew[MST_CityTable.CODE_FLD] = drowTemp[MST_CityTable.CODE_FLD];
					drowNew[MST_CityTable.NAME_FLD] = drowTemp[MST_CityTable.NAME_FLD];
					tblData.Rows.Add(drowNew);
				}
				FormControlComponents.PutDataIntoC1ComboBox(cboCity, tblData, MST_CityTable.CODE_FLD, MST_CityTable.CITYID_FLD, MST_CityTable.TABLE_NAME, true);
				//cboCity.Items.Insert(0, string.Empty);
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
		///       This method use to assign Enter and Leave event for all editable control on form
		///    </Description>
		///    <Inputs>
		///       Control to assign
		///    </Inputs>
		///    <Outputs>
		///
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       29-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		
		
		//**************************************************************************              
		///    <Description>
		///       This method use to format control when Enter event raise in the form
		///    </Description>
		///    <Inputs>
		///       Control to format
		///    </Inputs>
		///    <Outputs>
		///
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       04-Jan-2005
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
		//**************************************************************************              
		///    <Description>
		///       This method use to format control when Leave event raise in the form
		///    </Description>
		///    <Inputs>
		///       Control to format
		///    </Inputs>
		///    <Outputs>
		///
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       04-Jan-2005
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
		}
		//**************************************************************************              
		///    <Description>
		///       This method use to fill data to form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       29-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void FillData(DataRow pdrowData)
		{
			try
			{
				// Get MST_PartyVO object
				voParty = (MST_PartyVO)boParty.GetObjectVO(int.Parse(pdrowData[MST_PartyTable.PARTYID_FLD].ToString()), string.Empty);				
				// Load data to form
				LoadDataToControl(voParty);
				// Switching form mode
				SwitchFormMode();
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

		/// <summary>
		/// Call this method to PartyLocation open search form 
		/// </summary>
		/// <param name="pstrMethodName"></param>
		/// Method that called this method
		/// <param name="pblnAlwaysShowDialog"></param>
		/// 1: always show open search form
		/// 0: other else
		private bool SelectPartyCode(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
//			try
//			{				
				//Call OpenSearchForm for selecting	
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_PartyTable.TABLE_NAME, MST_PartyTable.CODE_FLD , txtCode.Text, null, pblnAlwaysShowDialog);

				// If matching condition, fill values to form's controls
				if(drwResult != null)
				{
					FillData(drwResult.Row);
					//Reset modify status
					txtCode.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtCode.Focus();
					return false;
				}

				return true;
//			}
//			catch (PCSException ex)
//			{
//				throw new PCSException(ex.mCode, pstrMethodName, ex);
//			}
//			catch (Exception ex)
//			{
//				throw new Exception(ex.Message, ex);
//			}
		}
		
		/// <summary>
		/// Call this method to Party open search form 
		/// </summary>
		/// <param name="pstrMethodName"></param>
		/// Method that called SelectPartyName
		/// <param name="pblnAlwaysShowDialog"></param>
		/// 1: always show open search form
		/// 0: other else
		private bool SelectPartyName(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{					
				//Call OpenSearchForm for selecting TransNo	
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_PartyTable.TABLE_NAME, MST_PartyTable.NAME_FLD , txtName.Text, null, pblnAlwaysShowDialog);

				// If has TransNo matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					FillData(drwResult.Row);
					//Reset modify status
					txtName.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtName.Focus();
					return false;
				}

				return true;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		#endregion

		#region Event Processing

		//**************************************************************************              
		///    <Description>
		///       Init data, switching form mode and fill data to form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void Party_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".Party_Load()";
			try
			{
				this.Name = THIS;
				//Set form security
				Security objSecurity = new Security();
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
									{
										// You don't have the right to view this item
										PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				mFormAction = EnumAction.Default;
				// initial combo box
				InitComboBoxs();
				// switch form mode based on FormAction
				SwitchFormMode();
				if (mFormAction == EnumAction.Edit)
				{
					LoadDataToControl(voParty);
				}
				else
				{
					LoadDataToControl(null);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       User enter code and click search button. Display search form, allows user to select Party
		///       turn form to Edit mode if user select a Party
		///       else turn form to Add mode
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSearchCode_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSearchCode_Click()";

			try
			{
				SelectPartyCode(METHOD_NAME, true);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       User enter Name and click search button. Display search form, allows user to select Party
		///       turn form to Edit mode if user select a Party
		///       else turn form to Add mode
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSearchName_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSearchName_Click()";

			try
			{
				SelectPartyName(METHOD_NAME, true);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnLocation_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnLocation_Click()";
			try
			{
				//exist immediate if no item's selected
				if(voParty.PartyID <= 0) return;

				Location frmLocation = new Location(voParty);
				frmLocation.FormMode = EnumAction.Default;
				if ((cboCountry.SelectedValue != DBNull.Value) && (cboCountry.SelectedValue != null))
				{
					frmLocation.DefaultCountryID = int.Parse(cboCountry.SelectedValue.ToString());
					if ((cboCity.SelectedValue != DBNull.Value) && (cboCity.SelectedValue != null))
					{
						frmLocation.DefaultCityID = int.Parse(cboCity.SelectedValue.ToString());
					}
				}
				frmLocation.ShowDialog();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnContacts_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnContacts_Click()";
			try
			{
				//exist immediate if no item's selected
				if(voParty.PartyID <= 0) return;

				MasterContacts frmMasterContacts = new MasterContacts(voParty);
				frmMasterContacts.FormMode = EnumAction.Default;
				frmMasterContacts.ShowDialog();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Turn formto ADD mode and clear data in form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnAdd_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				strLastValidCurrency = string.Empty;
				// turn form to ADD mode
				mFormAction = EnumAction.Add;
				// switching form mode
				SwitchFormMode();
				// reset form
				LoadDataToControl(null);
				txtCode.Text = boUtils.GetNoByMask(MST_PartyTable.TABLE_NAME, MST_PartyTable.CODE_FLD, DateTime.Now, string.Empty);
				txtCode.Focus();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Validate data and save to database
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSave_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				// validate data
				if (FormControlComponents.CheckMandatory(txtCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
										txtCode.Focus();
										txtCode.SelectAll();					
					blnDataIsValid = false;
					// Code Inserted Automatically
					#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				if (FormControlComponents.CheckMandatory(txtName))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
										txtName.Focus();
										txtName.SelectAll();
					blnDataIsValid = false;
					// Code Inserted Automatically
					#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				if (FormControlComponents.CheckMandatory(txtAddress))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
										txtAddress.Focus();
										txtAddress.SelectAll();
					blnDataIsValid = false;
					// Code Inserted Automatically
					#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				// 21-04-2006 dungla: fix bug 3833 for thuypt: User must enter Country
				if (cboCountry.SelectedIndex < 0 || cboCountry.Text.Trim().Length == 0)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
					cboCountry.Focus();
					blnDataIsValid = false;
					// Code Inserted Automatically
					#region Code Inserted Automatically
					this.Cursor = Cursors.Default;
					#endregion Code Inserted Automatically

					return;
				}
				// 21-04-2006 dungla: fix bug 3833 for thuypt: User must enter Country
				
				if (cboType.SelectedIndex <0 || cboType.Text.Trim().Length == 0)
				{
										PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
										cboType.Focus();
					blnDataIsValid = false;
					// Code Inserted Automatically
					#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				
				if (txtPhone.Text.Trim() != string.Empty)
				{
					if (!FormControlComponents.IsPhoneFaxNumber(txtPhone.Text.Trim()))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_PHONE_NUMBER, MessageBoxButtons.OK, MessageBoxIcon.Error);
												txtPhone.Focus();
												txtPhone.SelectAll();
						blnDataIsValid = false;
						// Code Inserted Automatically
						#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

						return;
					}
				}
				
				if (txtFax.Text.Trim() != string.Empty)
				{
					if (!FormControlComponents.IsPhoneFaxNumber(txtFax.Text.Trim()))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_FAX_NUMBER, MessageBoxButtons.OK, MessageBoxIcon.Error);
												txtFax.Focus();
												txtFax.SelectAll();
						blnDataIsValid = false;
						// Code Inserted Automatically
						#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

						return;
					}
				}

				if (SaveData())
				{
					// display successful message
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
					// turn to DEFAULT mode
					mFormAction = EnumAction.Default;
					// switching form mode
					SwitchFormMode();
					blnDataIsValid = true;
				}
				else
				{
					// display error message
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtCode.Focus();
					txtCode.SelectAll();
					blnDataIsValid = false;
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
				txtCode.Focus();
				txtCode.SelectAll();
				blnDataIsValid = false;
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
				txtCode.Focus();
				txtCode.SelectAll();
				blnDataIsValid = false;
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Turn form to edit mode
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		private void btnEdit_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				//exist immediate if no item's selected
				if(voParty.PartyID <= 0) return;

				// turn form to EDIT mode
				mFormAction = EnumAction.Edit;
				// switching form mode
				SwitchFormMode();
				txtCode.Focus();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
		///       Display confirm message and do delete action if user select Yes option.
		///       After delete successful, turn form to DEFAULT mode and clear data in form.
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnDelete_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				//exist immediate if no item's selected
				if(voParty.PartyID <= 0) return;

				// display confirm message
				DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
				if (dlgResult == DialogResult.Yes)
				{
					// do delete action
					boParty.Delete(voParty);

					// clear data of vo object then bind to controls
					voParty = new MST_PartyVO();					
					LoadDataToControl(voParty);
					// set form mode to DEFAULT
					mFormAction = EnumAction.Default;
					// switching form mode
					SwitchFormMode();					
				}
				txtCode.Focus();
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		//**************************************************************************              
		///    <Description>
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
			///    <Notes>
		///    </Notes>
			//**************************************************************************
		private void btnHelp_Click(object sender, EventArgs e)
			{
		// Code Inserted Automatically
					#region Code Inserted Automatically

		this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
		
					// Code Inserted Automatically
					#region Code Inserted Automatically

		this.Cursor = Cursors.Default;

		#endregion Code Inserted Automatically

		
		}

		//**************************************************************************              
		///    <Description>
		///       Close the form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnClose_Click(object sender, EventArgs e)
		{
			
			// Code Inserted Automatically

			
			#region Code Inserted Automatically

			
			this.Cursor = Cursors.WaitCursor;

			
			#endregion Code Inserted Automatically

			
			
			
			Close();

			
			// Code Inserted Automatically

			
			#region Code Inserted Automatically

			
			this.Cursor = Cursors.Default;

			
			#endregion Code Inserted Automatically

			
			
		}

		//**************************************************************************              
		///    <Description>
		///       If curretn form mode is not equal to DEFAULT 
		///       and ask user to save data to database before close the form
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    03-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void Party_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".Party_Closing()";
			try
			{				
				// if form mode is not DEFAULT then display confirm message
				if (mFormAction != EnumAction.Default)
				{
					DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					switch (dlgResult)
					{
						case DialogResult.Yes:

							btnSave_Click(sender, e);
							e.Cancel = !blnDataIsValid;
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

		//**************************************************************************              
		///    <Description>
		///       When user select another country, then get all city of selected country
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    04-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void cboCountry_SelectedValueChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboCountry_SelectedValueChanged()";
			try
			{
				if(cboCountry.SelectedIndex <=0 ) return;

				int intCountryID = 0;
				try
				{
					intCountryID = int.Parse(cboCountry.SelectedValue.ToString());
				}
				catch{}
				if (intCountryID > 0 && tblCityData.Rows.Count > 0)
				{ 
					// select data for city combo
					DataRow[] drowData = tblCityData.Select(MST_CityTable.COUNTRYID_FLD + Constants.EQUAL + intCountryID.ToString());
					// put data into City combo
					PutDataToCityCombo(drowData);
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
		//**************************************************************************              
		///    <Description>
		///       F4 - Open search Party form
		///       F11 - Open MasterContact form with mode is ADD
		///       F12 - Open Location form with mode is ADD
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    04-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void Party_KeyDown(object sender, KeyEventArgs e)
		{
			
		}

		private void txtName_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtName_Validating()";
			
			try
			{				
				if(mFormAction != EnumAction.Default) return;
				
				//Exit immediately if empty
				if(txtName.Text.Length == 0)
				{
					voParty = new MST_PartyVO();
					voParty.PartyID = 0;
					LoadDataToControl(voParty);
					SwitchFormMode();
					return;
				}
				else if(!txtName.Modified)
				{
					return;
				}

				e.Cancel = !SelectPartyName(METHOD_NAME, false);
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

		
		private void txtName_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtName_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnSearchName.Enabled))
				{
					SelectPartyName(METHOD_NAME, true);
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

		private void txtCode_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCode_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnSearchCode.Enabled))
				{
					SelectPartyCode(METHOD_NAME, true);
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
		
		private void txtCode_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCode_Validating()";
			
			try
			{
				//Exit immediately if empty
				if(mFormAction != EnumAction.Default) return;
				
				if(txtCode.Text.Length == 0)
				{	
					voParty = new MST_PartyVO();
					voParty.PartyID = 0;
					LoadDataToControl(voParty);
					SwitchFormMode();
					return;
				}
				else if(!txtCode.Modified)
				{
					return;
				}

				e.Cancel = !SelectPartyCode(METHOD_NAME, false);
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

		

		#endregion		

		private void txtCurrency_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCurrency_Validating()";
			try
			{
				if (txtCurrency.Text.Trim() == string.Empty)
				{
					txtCurrency.Tag = null;
					strLastValidCurrency = string.Empty;
					return;
				}
				if (strLastValidCurrency == txtCurrency.Text.Trim()) return;
				DataRowView drowView = SearchCurrency(false);
				if (drowView != null)
					FillCurrencyData(drowView.Row);
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

		private void btnCurrency_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCurrency_Click()";
			try
			{
				DataRowView drowView = SearchCurrency(true);
				if (drowView != null)
					FillCurrencyData(drowView.Row);
				else
					txtCurrency.Focus();
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

		private void txtCurrency_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCurrency_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4 && btnCurrency.Enabled)
					btnCurrency_Click(btnCurrency, new EventArgs());
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

		private DataRowView SearchCurrency(bool pblnOpenForm)
		{
			return FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME, MST_CurrencyTable.CODE_FLD, txtCurrency.Text.Trim(), null, pblnOpenForm);
		}
		private void FillCurrencyData(DataRow pdrowData)
		{
			txtCurrency.Text = pdrowData[MST_CurrencyTable.CODE_FLD].ToString();
			txtCurrency.Tag = pdrowData[MST_CurrencyTable.CURRENCYID_FLD];
			// user has changed purpose
			if (strLastValidCurrency != txtCurrency.Text)
				strLastValidCurrency = txtCurrency.Text;
		}
	}
}
