using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1TrueDBGrid;
using PCSComProcurement.Purchase.BO;
using PCSComProcurement.Purchase.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using BeforeColEditEventArgs = C1.Win.C1TrueDBGrid.BeforeColEditEventArgs;
using C1DisplayColumn = C1.Win.C1TrueDBGrid.C1DisplayColumn;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using ColEventArgs = C1.Win.C1TrueDBGrid.ColEventArgs;
using RowColChangeEventArgs = C1.Win.C1TrueDBGrid.RowColChangeEventArgs;

namespace PCSProcurement.Purchase
{
	/// <summary>
	/// Summary description for POAdditionCharges.
	/// </summary>
	public class POAdditionCharges : Form
	{
		#region Form controls

		private Label lblTotalAmount;
		private TextBox txtTotalAmount;
		private TextBox txtTotalVATAmount;
		private Label lblTotalVATAmount;
		private GroupBox grbDistribution;
		private RadioButton radByManual;
		private RadioButton radByPrice;
		private RadioButton radByQuantity;
		private Button btnToDistribute;
		private Label lblDistributionAmount;
		private TextBox txtVendorCode;
		private Button btnDelete;
		private Button btnClose;
		private Button btnHelp;
		private Button btnSave;
		private TextBox txtTransDate;
		private TextBox txtCCN;
		private TextBox txtVendorName;
		private Label lblTransDate;
		private Label lblCCN;
		private Label lblVendorName;
		private Label lblVendorCode;
		private Label lblOrderNo;
		private TextBox txtPOOrderNo;
		private C1TrueDBGrid dgrdData;
		private TextBox txtTotalTaxAmount;
		private Label lblTotalTaxAmount;
		private C1NumericEdit txtDistributionAmount;

		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		#region Constants

		private const string THIS = "PCSProcurement.Purchase.POAdditionCharges";
		private const int DECIMAL_ROUND = 2;
		private const int VAT_NUMBER = 100;
		private const string OPEN_QUOTE = "(";
		private const string CLOSE_QUOTE = ")";
		private const string OR = " OR ";
		private const string IS_NULL = " IS NULL ";
		private const string SMALLER_THAN = "<";
		private const string BY_PRICE = "BYPRICE";
		private const string BY_MANUAL = "BYMANUAL";
		private const string BY_QUANTITY = "BYQUANTITY";
		private const string LINE_COL = "Line";
		
		#endregion

		#region Variables - Properties

		private int intPOMasterID;
		private EnumAction mFormMode;
		private POAdditionChargesBO boPOAddCharge;
		private UtilsBO boUtils;
		private PO_PurchaseOrderMasterVO voPOMaster;
		private PO_PurchaseOrderDetailVO voPODetail;
		private MST_AddChargeVO voAddCharge;
		private MST_ReasonVO voReason;
		private DataSet dstData = new DataSet();
		private DataTable tblPODetails = new DataTable(PO_PurchaseOrderDetailTable.TABLE_NAME);
		private string strSelectedValue;
		private bool blnChargeByQuantity;
		private System.Windows.Forms.TextBox txtCellValue;
		
		public EnumAction FormMode
		{
			get { return mFormMode; }
			set { mFormMode = value; }
		}

		#endregion

		public POAdditionCharges()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		public POAdditionCharges(int pintPOMasterID)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			intPOMasterID = pintPOMasterID;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(POAdditionCharges));
			this.lblTotalAmount = new System.Windows.Forms.Label();
			this.txtTotalAmount = new System.Windows.Forms.TextBox();
			this.txtTotalTaxAmount = new System.Windows.Forms.TextBox();
			this.lblTotalTaxAmount = new System.Windows.Forms.Label();
			this.txtTotalVATAmount = new System.Windows.Forms.TextBox();
			this.lblTotalVATAmount = new System.Windows.Forms.Label();
			this.grbDistribution = new System.Windows.Forms.GroupBox();
			this.txtDistributionAmount = new C1.Win.C1Input.C1NumericEdit();
			this.radByManual = new System.Windows.Forms.RadioButton();
			this.radByPrice = new System.Windows.Forms.RadioButton();
			this.radByQuantity = new System.Windows.Forms.RadioButton();
			this.btnToDistribute = new System.Windows.Forms.Button();
			this.lblDistributionAmount = new System.Windows.Forms.Label();
			this.lblTransDate = new System.Windows.Forms.Label();
			this.lblCCN = new System.Windows.Forms.Label();
			this.lblVendorName = new System.Windows.Forms.Label();
			this.txtVendorCode = new System.Windows.Forms.TextBox();
			this.lblVendorCode = new System.Windows.Forms.Label();
			this.txtPOOrderNo = new System.Windows.Forms.TextBox();
			this.lblOrderNo = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.txtTransDate = new System.Windows.Forms.TextBox();
			this.txtCCN = new System.Windows.Forms.TextBox();
			this.txtVendorName = new System.Windows.Forms.TextBox();
			this.txtCellValue = new System.Windows.Forms.TextBox();
			this.grbDistribution.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtDistributionAmount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTotalAmount
			// 
			this.lblTotalAmount.AccessibleDescription = resources.GetString("lblTotalAmount.AccessibleDescription");
			this.lblTotalAmount.AccessibleName = resources.GetString("lblTotalAmount.AccessibleName");
			this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTotalAmount.Anchor")));
			this.lblTotalAmount.AutoSize = ((bool)(resources.GetObject("lblTotalAmount.AutoSize")));
			this.lblTotalAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTotalAmount.Dock")));
			this.lblTotalAmount.Enabled = ((bool)(resources.GetObject("lblTotalAmount.Enabled")));
			this.lblTotalAmount.Font = ((System.Drawing.Font)(resources.GetObject("lblTotalAmount.Font")));
			this.lblTotalAmount.Image = ((System.Drawing.Image)(resources.GetObject("lblTotalAmount.Image")));
			this.lblTotalAmount.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTotalAmount.ImageAlign")));
			this.lblTotalAmount.ImageIndex = ((int)(resources.GetObject("lblTotalAmount.ImageIndex")));
			this.lblTotalAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTotalAmount.ImeMode")));
			this.lblTotalAmount.Location = ((System.Drawing.Point)(resources.GetObject("lblTotalAmount.Location")));
			this.lblTotalAmount.Name = "lblTotalAmount";
			this.lblTotalAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTotalAmount.RightToLeft")));
			this.lblTotalAmount.Size = ((System.Drawing.Size)(resources.GetObject("lblTotalAmount.Size")));
			this.lblTotalAmount.TabIndex = ((int)(resources.GetObject("lblTotalAmount.TabIndex")));
			this.lblTotalAmount.Text = resources.GetString("lblTotalAmount.Text");
			this.lblTotalAmount.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTotalAmount.TextAlign")));
			this.lblTotalAmount.Visible = ((bool)(resources.GetObject("lblTotalAmount.Visible")));
			// 
			// txtTotalAmount
			// 
			this.txtTotalAmount.AccessibleDescription = resources.GetString("txtTotalAmount.AccessibleDescription");
			this.txtTotalAmount.AccessibleName = resources.GetString("txtTotalAmount.AccessibleName");
			this.txtTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtTotalAmount.Anchor")));
			this.txtTotalAmount.AutoSize = ((bool)(resources.GetObject("txtTotalAmount.AutoSize")));
			this.txtTotalAmount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtTotalAmount.BackgroundImage")));
			this.txtTotalAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtTotalAmount.Dock")));
			this.txtTotalAmount.Enabled = ((bool)(resources.GetObject("txtTotalAmount.Enabled")));
			this.txtTotalAmount.Font = ((System.Drawing.Font)(resources.GetObject("txtTotalAmount.Font")));
			this.txtTotalAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtTotalAmount.ImeMode")));
			this.txtTotalAmount.Location = ((System.Drawing.Point)(resources.GetObject("txtTotalAmount.Location")));
			this.txtTotalAmount.MaxLength = ((int)(resources.GetObject("txtTotalAmount.MaxLength")));
			this.txtTotalAmount.Multiline = ((bool)(resources.GetObject("txtTotalAmount.Multiline")));
			this.txtTotalAmount.Name = "txtTotalAmount";
			this.txtTotalAmount.PasswordChar = ((char)(resources.GetObject("txtTotalAmount.PasswordChar")));
			this.txtTotalAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtTotalAmount.RightToLeft")));
			this.txtTotalAmount.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtTotalAmount.ScrollBars")));
			this.txtTotalAmount.Size = ((System.Drawing.Size)(resources.GetObject("txtTotalAmount.Size")));
			this.txtTotalAmount.TabIndex = ((int)(resources.GetObject("txtTotalAmount.TabIndex")));
			this.txtTotalAmount.Text = resources.GetString("txtTotalAmount.Text");
			this.txtTotalAmount.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtTotalAmount.TextAlign")));
			this.txtTotalAmount.Visible = ((bool)(resources.GetObject("txtTotalAmount.Visible")));
			this.txtTotalAmount.WordWrap = ((bool)(resources.GetObject("txtTotalAmount.WordWrap")));
			// 
			// txtTotalTaxAmount
			// 
			this.txtTotalTaxAmount.AccessibleDescription = resources.GetString("txtTotalTaxAmount.AccessibleDescription");
			this.txtTotalTaxAmount.AccessibleName = resources.GetString("txtTotalTaxAmount.AccessibleName");
			this.txtTotalTaxAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtTotalTaxAmount.Anchor")));
			this.txtTotalTaxAmount.AutoSize = ((bool)(resources.GetObject("txtTotalTaxAmount.AutoSize")));
			this.txtTotalTaxAmount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtTotalTaxAmount.BackgroundImage")));
			this.txtTotalTaxAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtTotalTaxAmount.Dock")));
			this.txtTotalTaxAmount.Enabled = ((bool)(resources.GetObject("txtTotalTaxAmount.Enabled")));
			this.txtTotalTaxAmount.Font = ((System.Drawing.Font)(resources.GetObject("txtTotalTaxAmount.Font")));
			this.txtTotalTaxAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtTotalTaxAmount.ImeMode")));
			this.txtTotalTaxAmount.Location = ((System.Drawing.Point)(resources.GetObject("txtTotalTaxAmount.Location")));
			this.txtTotalTaxAmount.MaxLength = ((int)(resources.GetObject("txtTotalTaxAmount.MaxLength")));
			this.txtTotalTaxAmount.Multiline = ((bool)(resources.GetObject("txtTotalTaxAmount.Multiline")));
			this.txtTotalTaxAmount.Name = "txtTotalTaxAmount";
			this.txtTotalTaxAmount.PasswordChar = ((char)(resources.GetObject("txtTotalTaxAmount.PasswordChar")));
			this.txtTotalTaxAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtTotalTaxAmount.RightToLeft")));
			this.txtTotalTaxAmount.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtTotalTaxAmount.ScrollBars")));
			this.txtTotalTaxAmount.Size = ((System.Drawing.Size)(resources.GetObject("txtTotalTaxAmount.Size")));
			this.txtTotalTaxAmount.TabIndex = ((int)(resources.GetObject("txtTotalTaxAmount.TabIndex")));
			this.txtTotalTaxAmount.Text = resources.GetString("txtTotalTaxAmount.Text");
			this.txtTotalTaxAmount.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtTotalTaxAmount.TextAlign")));
			this.txtTotalTaxAmount.Visible = ((bool)(resources.GetObject("txtTotalTaxAmount.Visible")));
			this.txtTotalTaxAmount.WordWrap = ((bool)(resources.GetObject("txtTotalTaxAmount.WordWrap")));
			// 
			// lblTotalTaxAmount
			// 
			this.lblTotalTaxAmount.AccessibleDescription = resources.GetString("lblTotalTaxAmount.AccessibleDescription");
			this.lblTotalTaxAmount.AccessibleName = resources.GetString("lblTotalTaxAmount.AccessibleName");
			this.lblTotalTaxAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTotalTaxAmount.Anchor")));
			this.lblTotalTaxAmount.AutoSize = ((bool)(resources.GetObject("lblTotalTaxAmount.AutoSize")));
			this.lblTotalTaxAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTotalTaxAmount.Dock")));
			this.lblTotalTaxAmount.Enabled = ((bool)(resources.GetObject("lblTotalTaxAmount.Enabled")));
			this.lblTotalTaxAmount.Font = ((System.Drawing.Font)(resources.GetObject("lblTotalTaxAmount.Font")));
			this.lblTotalTaxAmount.Image = ((System.Drawing.Image)(resources.GetObject("lblTotalTaxAmount.Image")));
			this.lblTotalTaxAmount.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTotalTaxAmount.ImageAlign")));
			this.lblTotalTaxAmount.ImageIndex = ((int)(resources.GetObject("lblTotalTaxAmount.ImageIndex")));
			this.lblTotalTaxAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTotalTaxAmount.ImeMode")));
			this.lblTotalTaxAmount.Location = ((System.Drawing.Point)(resources.GetObject("lblTotalTaxAmount.Location")));
			this.lblTotalTaxAmount.Name = "lblTotalTaxAmount";
			this.lblTotalTaxAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTotalTaxAmount.RightToLeft")));
			this.lblTotalTaxAmount.Size = ((System.Drawing.Size)(resources.GetObject("lblTotalTaxAmount.Size")));
			this.lblTotalTaxAmount.TabIndex = ((int)(resources.GetObject("lblTotalTaxAmount.TabIndex")));
			this.lblTotalTaxAmount.Text = resources.GetString("lblTotalTaxAmount.Text");
			this.lblTotalTaxAmount.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTotalTaxAmount.TextAlign")));
			this.lblTotalTaxAmount.Visible = ((bool)(resources.GetObject("lblTotalTaxAmount.Visible")));
			// 
			// txtTotalVATAmount
			// 
			this.txtTotalVATAmount.AccessibleDescription = resources.GetString("txtTotalVATAmount.AccessibleDescription");
			this.txtTotalVATAmount.AccessibleName = resources.GetString("txtTotalVATAmount.AccessibleName");
			this.txtTotalVATAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtTotalVATAmount.Anchor")));
			this.txtTotalVATAmount.AutoSize = ((bool)(resources.GetObject("txtTotalVATAmount.AutoSize")));
			this.txtTotalVATAmount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtTotalVATAmount.BackgroundImage")));
			this.txtTotalVATAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtTotalVATAmount.Dock")));
			this.txtTotalVATAmount.Enabled = ((bool)(resources.GetObject("txtTotalVATAmount.Enabled")));
			this.txtTotalVATAmount.Font = ((System.Drawing.Font)(resources.GetObject("txtTotalVATAmount.Font")));
			this.txtTotalVATAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtTotalVATAmount.ImeMode")));
			this.txtTotalVATAmount.Location = ((System.Drawing.Point)(resources.GetObject("txtTotalVATAmount.Location")));
			this.txtTotalVATAmount.MaxLength = ((int)(resources.GetObject("txtTotalVATAmount.MaxLength")));
			this.txtTotalVATAmount.Multiline = ((bool)(resources.GetObject("txtTotalVATAmount.Multiline")));
			this.txtTotalVATAmount.Name = "txtTotalVATAmount";
			this.txtTotalVATAmount.PasswordChar = ((char)(resources.GetObject("txtTotalVATAmount.PasswordChar")));
			this.txtTotalVATAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtTotalVATAmount.RightToLeft")));
			this.txtTotalVATAmount.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtTotalVATAmount.ScrollBars")));
			this.txtTotalVATAmount.Size = ((System.Drawing.Size)(resources.GetObject("txtTotalVATAmount.Size")));
			this.txtTotalVATAmount.TabIndex = ((int)(resources.GetObject("txtTotalVATAmount.TabIndex")));
			this.txtTotalVATAmount.Text = resources.GetString("txtTotalVATAmount.Text");
			this.txtTotalVATAmount.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtTotalVATAmount.TextAlign")));
			this.txtTotalVATAmount.Visible = ((bool)(resources.GetObject("txtTotalVATAmount.Visible")));
			this.txtTotalVATAmount.WordWrap = ((bool)(resources.GetObject("txtTotalVATAmount.WordWrap")));
			// 
			// lblTotalVATAmount
			// 
			this.lblTotalVATAmount.AccessibleDescription = resources.GetString("lblTotalVATAmount.AccessibleDescription");
			this.lblTotalVATAmount.AccessibleName = resources.GetString("lblTotalVATAmount.AccessibleName");
			this.lblTotalVATAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTotalVATAmount.Anchor")));
			this.lblTotalVATAmount.AutoSize = ((bool)(resources.GetObject("lblTotalVATAmount.AutoSize")));
			this.lblTotalVATAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTotalVATAmount.Dock")));
			this.lblTotalVATAmount.Enabled = ((bool)(resources.GetObject("lblTotalVATAmount.Enabled")));
			this.lblTotalVATAmount.Font = ((System.Drawing.Font)(resources.GetObject("lblTotalVATAmount.Font")));
			this.lblTotalVATAmount.Image = ((System.Drawing.Image)(resources.GetObject("lblTotalVATAmount.Image")));
			this.lblTotalVATAmount.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTotalVATAmount.ImageAlign")));
			this.lblTotalVATAmount.ImageIndex = ((int)(resources.GetObject("lblTotalVATAmount.ImageIndex")));
			this.lblTotalVATAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTotalVATAmount.ImeMode")));
			this.lblTotalVATAmount.Location = ((System.Drawing.Point)(resources.GetObject("lblTotalVATAmount.Location")));
			this.lblTotalVATAmount.Name = "lblTotalVATAmount";
			this.lblTotalVATAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTotalVATAmount.RightToLeft")));
			this.lblTotalVATAmount.Size = ((System.Drawing.Size)(resources.GetObject("lblTotalVATAmount.Size")));
			this.lblTotalVATAmount.TabIndex = ((int)(resources.GetObject("lblTotalVATAmount.TabIndex")));
			this.lblTotalVATAmount.Text = resources.GetString("lblTotalVATAmount.Text");
			this.lblTotalVATAmount.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTotalVATAmount.TextAlign")));
			this.lblTotalVATAmount.Visible = ((bool)(resources.GetObject("lblTotalVATAmount.Visible")));
			// 
			// grbDistribution
			// 
			this.grbDistribution.AccessibleDescription = resources.GetString("grbDistribution.AccessibleDescription");
			this.grbDistribution.AccessibleName = resources.GetString("grbDistribution.AccessibleName");
			this.grbDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grbDistribution.Anchor")));
			this.grbDistribution.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grbDistribution.BackgroundImage")));
			this.grbDistribution.Controls.Add(this.txtDistributionAmount);
			this.grbDistribution.Controls.Add(this.radByManual);
			this.grbDistribution.Controls.Add(this.radByPrice);
			this.grbDistribution.Controls.Add(this.radByQuantity);
			this.grbDistribution.Controls.Add(this.btnToDistribute);
			this.grbDistribution.Controls.Add(this.lblDistributionAmount);
			this.grbDistribution.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grbDistribution.Dock")));
			this.grbDistribution.Enabled = ((bool)(resources.GetObject("grbDistribution.Enabled")));
			this.grbDistribution.Font = ((System.Drawing.Font)(resources.GetObject("grbDistribution.Font")));
			this.grbDistribution.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grbDistribution.ImeMode")));
			this.grbDistribution.Location = ((System.Drawing.Point)(resources.GetObject("grbDistribution.Location")));
			this.grbDistribution.Name = "grbDistribution";
			this.grbDistribution.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grbDistribution.RightToLeft")));
			this.grbDistribution.Size = ((System.Drawing.Size)(resources.GetObject("grbDistribution.Size")));
			this.grbDistribution.TabIndex = ((int)(resources.GetObject("grbDistribution.TabIndex")));
			this.grbDistribution.TabStop = false;
			this.grbDistribution.Text = resources.GetString("grbDistribution.Text");
			this.grbDistribution.Visible = ((bool)(resources.GetObject("grbDistribution.Visible")));
			// 
			// txtDistributionAmount
			// 
			this.txtDistributionAmount.AcceptsEscape = ((bool)(resources.GetObject("txtDistributionAmount.AcceptsEscape")));
			this.txtDistributionAmount.AccessibleDescription = resources.GetString("txtDistributionAmount.AccessibleDescription");
			this.txtDistributionAmount.AccessibleName = resources.GetString("txtDistributionAmount.AccessibleName");
			this.txtDistributionAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtDistributionAmount.Anchor")));
			this.txtDistributionAmount.AutoSize = ((bool)(resources.GetObject("txtDistributionAmount.AutoSize")));
			this.txtDistributionAmount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtDistributionAmount.BackgroundImage")));
			this.txtDistributionAmount.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("txtDistributionAmount.BorderStyle")));
			// 
			// txtDistributionAmount.Calculator
			// 
			this.txtDistributionAmount.Calculator.AccessibleDescription = resources.GetString("txtDistributionAmount.Calculator.AccessibleDescription");
			this.txtDistributionAmount.Calculator.AccessibleName = resources.GetString("txtDistributionAmount.Calculator.AccessibleName");
			this.txtDistributionAmount.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtDistributionAmount.Calculator.BackgroundImage")));
			this.txtDistributionAmount.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("txtDistributionAmount.Calculator.ButtonFlatStyle")));
			this.txtDistributionAmount.Calculator.DisplayFormat = resources.GetString("txtDistributionAmount.Calculator.DisplayFormat");
			this.txtDistributionAmount.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("txtDistributionAmount.Calculator.Font")));
			this.txtDistributionAmount.Calculator.FormatOnClose = ((bool)(resources.GetObject("txtDistributionAmount.Calculator.FormatOnClose")));
			this.txtDistributionAmount.Calculator.StoredFormat = resources.GetString("txtDistributionAmount.Calculator.StoredFormat");
			this.txtDistributionAmount.Calculator.UIStrings.Content = ((string[])(resources.GetObject("txtDistributionAmount.Calculator.UIStrings.Content")));
			this.txtDistributionAmount.CaseSensitive = ((bool)(resources.GetObject("txtDistributionAmount.CaseSensitive")));
			this.txtDistributionAmount.Culture = ((int)(resources.GetObject("txtDistributionAmount.Culture")));
			this.txtDistributionAmount.CustomFormat = resources.GetString("txtDistributionAmount.CustomFormat");
			this.txtDistributionAmount.DataType = ((System.Type)(resources.GetObject("txtDistributionAmount.DataType")));
			this.txtDistributionAmount.DisableOnNoData = false;
			this.txtDistributionAmount.DisplayFormat.CustomFormat = resources.GetString("txtDistributionAmount.DisplayFormat.CustomFormat");
			this.txtDistributionAmount.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtDistributionAmount.DisplayFormat.FormatType")));
			this.txtDistributionAmount.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtDistributionAmount.DisplayFormat.Inherit")));
			this.txtDistributionAmount.DisplayFormat.NullText = resources.GetString("txtDistributionAmount.DisplayFormat.NullText");
			this.txtDistributionAmount.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("txtDistributionAmount.DisplayFormat.TrimEnd")));
			this.txtDistributionAmount.DisplayFormat.TrimStart = ((bool)(resources.GetObject("txtDistributionAmount.DisplayFormat.TrimStart")));
			this.txtDistributionAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtDistributionAmount.Dock")));
			this.txtDistributionAmount.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("txtDistributionAmount.DropDownFormAlign")));
			this.txtDistributionAmount.EditFormat.CustomFormat = resources.GetString("txtDistributionAmount.EditFormat.CustomFormat");
			this.txtDistributionAmount.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtDistributionAmount.EditFormat.FormatType")));
			this.txtDistributionAmount.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtDistributionAmount.EditFormat.Inherit")));
			this.txtDistributionAmount.EditFormat.NullText = resources.GetString("txtDistributionAmount.EditFormat.NullText");
			this.txtDistributionAmount.EditFormat.TrimEnd = ((bool)(resources.GetObject("txtDistributionAmount.EditFormat.TrimEnd")));
			this.txtDistributionAmount.EditFormat.TrimStart = ((bool)(resources.GetObject("txtDistributionAmount.EditFormat.TrimStart")));
			this.txtDistributionAmount.EditMask = resources.GetString("txtDistributionAmount.EditMask");
			this.txtDistributionAmount.EmptyAsNull = ((bool)(resources.GetObject("txtDistributionAmount.EmptyAsNull")));
			this.txtDistributionAmount.Enabled = ((bool)(resources.GetObject("txtDistributionAmount.Enabled")));
			this.txtDistributionAmount.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtDistributionAmount.ErrorInfo.BeepOnError")));
			this.txtDistributionAmount.ErrorInfo.ErrorMessage = resources.GetString("txtDistributionAmount.ErrorInfo.ErrorMessage");
			this.txtDistributionAmount.ErrorInfo.ErrorMessageCaption = resources.GetString("txtDistributionAmount.ErrorInfo.ErrorMessageCaption");
			this.txtDistributionAmount.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("txtDistributionAmount.ErrorInfo.ShowErrorMessage")));
			this.txtDistributionAmount.ErrorInfo.ValueOnError = ((object)(resources.GetObject("txtDistributionAmount.ErrorInfo.ValueOnError")));
			this.txtDistributionAmount.Font = ((System.Drawing.Font)(resources.GetObject("txtDistributionAmount.Font")));
			this.txtDistributionAmount.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtDistributionAmount.FormatType")));
			this.txtDistributionAmount.GapHeight = ((int)(resources.GetObject("txtDistributionAmount.GapHeight")));
			this.txtDistributionAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtDistributionAmount.ImeMode")));
			this.txtDistributionAmount.Increment = ((object)(resources.GetObject("txtDistributionAmount.Increment")));
			this.txtDistributionAmount.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("txtDistributionAmount.InitialSelection")));
			this.txtDistributionAmount.Location = ((System.Drawing.Point)(resources.GetObject("txtDistributionAmount.Location")));
			this.txtDistributionAmount.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("txtDistributionAmount.MaskInfo.AutoTabWhenFilled")));
			this.txtDistributionAmount.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("txtDistributionAmount.MaskInfo.CaseSensitive")));
			this.txtDistributionAmount.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("txtDistributionAmount.MaskInfo.CopyWithLiterals")));
			this.txtDistributionAmount.MaskInfo.EditMask = resources.GetString("txtDistributionAmount.MaskInfo.EditMask");
			this.txtDistributionAmount.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("txtDistributionAmount.MaskInfo.EmptyAsNull")));
			this.txtDistributionAmount.MaskInfo.ErrorMessage = resources.GetString("txtDistributionAmount.MaskInfo.ErrorMessage");
			this.txtDistributionAmount.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtDistributionAmount.MaskInfo.Inherit")));
			this.txtDistributionAmount.MaskInfo.PromptChar = ((char)(resources.GetObject("txtDistributionAmount.MaskInfo.PromptChar")));
			this.txtDistributionAmount.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtDistributionAmount.MaskInfo.ShowLiterals")));
			this.txtDistributionAmount.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("txtDistributionAmount.MaskInfo.StoredEmptyChar")));
			this.txtDistributionAmount.MaxLength = ((int)(resources.GetObject("txtDistributionAmount.MaxLength")));
			this.txtDistributionAmount.Name = "txtDistributionAmount";
			this.txtDistributionAmount.NullText = resources.GetString("txtDistributionAmount.NullText");
			this.txtDistributionAmount.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("txtDistributionAmount.ParseInfo.CaseSensitive")));
			this.txtDistributionAmount.ParseInfo.CustomFormat = resources.GetString("txtDistributionAmount.ParseInfo.CustomFormat");
			this.txtDistributionAmount.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("txtDistributionAmount.ParseInfo.EmptyAsNull")));
			this.txtDistributionAmount.ParseInfo.ErrorMessage = resources.GetString("txtDistributionAmount.ParseInfo.ErrorMessage");
			this.txtDistributionAmount.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtDistributionAmount.ParseInfo.FormatType")));
			this.txtDistributionAmount.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtDistributionAmount.ParseInfo.Inherit")));
			this.txtDistributionAmount.ParseInfo.NullText = resources.GetString("txtDistributionAmount.ParseInfo.NullText");
			this.txtDistributionAmount.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("txtDistributionAmount.ParseInfo.NumberStyle")));
			this.txtDistributionAmount.ParseInfo.TrimEnd = ((bool)(resources.GetObject("txtDistributionAmount.ParseInfo.TrimEnd")));
			this.txtDistributionAmount.ParseInfo.TrimStart = ((bool)(resources.GetObject("txtDistributionAmount.ParseInfo.TrimStart")));
			this.txtDistributionAmount.PasswordChar = ((char)(resources.GetObject("txtDistributionAmount.PasswordChar")));
			this.txtDistributionAmount.PostValidation.CaseSensitive = ((bool)(resources.GetObject("txtDistributionAmount.PostValidation.CaseSensitive")));
			this.txtDistributionAmount.PostValidation.ErrorMessage = resources.GetString("txtDistributionAmount.PostValidation.ErrorMessage");
			this.txtDistributionAmount.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtDistributionAmount.PostValidation.Inherit")));
			this.txtDistributionAmount.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("txtDistributionAmount.PostValidation.Validation")));
			this.txtDistributionAmount.PostValidation.Values = ((System.Array)(resources.GetObject("txtDistributionAmount.PostValidation.Values")));
			this.txtDistributionAmount.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("txtDistributionAmount.PostValidation.ValuesExcluded")));
			this.txtDistributionAmount.PreValidation.CaseSensitive = ((bool)(resources.GetObject("txtDistributionAmount.PreValidation.CaseSensitive")));
			this.txtDistributionAmount.PreValidation.ErrorMessage = resources.GetString("txtDistributionAmount.PreValidation.ErrorMessage");
			this.txtDistributionAmount.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtDistributionAmount.PreValidation.Inherit")));
			this.txtDistributionAmount.PreValidation.ItemSeparator = resources.GetString("txtDistributionAmount.PreValidation.ItemSeparator");
			this.txtDistributionAmount.PreValidation.PatternString = resources.GetString("txtDistributionAmount.PreValidation.PatternString");
			this.txtDistributionAmount.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("txtDistributionAmount.PreValidation.RegexOptions")));
			this.txtDistributionAmount.PreValidation.TrimEnd = ((bool)(resources.GetObject("txtDistributionAmount.PreValidation.TrimEnd")));
			this.txtDistributionAmount.PreValidation.TrimStart = ((bool)(resources.GetObject("txtDistributionAmount.PreValidation.TrimStart")));
			this.txtDistributionAmount.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("txtDistributionAmount.PreValidation.Validation")));
			this.txtDistributionAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtDistributionAmount.RightToLeft")));
			this.txtDistributionAmount.ShowFocusRectangle = ((bool)(resources.GetObject("txtDistributionAmount.ShowFocusRectangle")));
			this.txtDistributionAmount.Size = ((System.Drawing.Size)(resources.GetObject("txtDistributionAmount.Size")));
			this.txtDistributionAmount.TabIndex = ((int)(resources.GetObject("txtDistributionAmount.TabIndex")));
			this.txtDistributionAmount.Tag = ((object)(resources.GetObject("txtDistributionAmount.Tag")));
			this.txtDistributionAmount.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtDistributionAmount.TextAlign")));
			this.txtDistributionAmount.TrimEnd = ((bool)(resources.GetObject("txtDistributionAmount.TrimEnd")));
			this.txtDistributionAmount.TrimStart = ((bool)(resources.GetObject("txtDistributionAmount.TrimStart")));
			this.txtDistributionAmount.UserCultureOverride = ((bool)(resources.GetObject("txtDistributionAmount.UserCultureOverride")));
			this.txtDistributionAmount.Value = ((object)(resources.GetObject("txtDistributionAmount.Value")));
			this.txtDistributionAmount.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("txtDistributionAmount.VerticalAlign")));
			this.txtDistributionAmount.Visible = ((bool)(resources.GetObject("txtDistributionAmount.Visible")));
			this.txtDistributionAmount.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("txtDistributionAmount.VisibleButtons")));
			// 
			// radByManual
			// 
			this.radByManual.AccessibleDescription = resources.GetString("radByManual.AccessibleDescription");
			this.radByManual.AccessibleName = resources.GetString("radByManual.AccessibleName");
			this.radByManual.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("radByManual.Anchor")));
			this.radByManual.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("radByManual.Appearance")));
			this.radByManual.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radByManual.BackgroundImage")));
			this.radByManual.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("radByManual.CheckAlign")));
			this.radByManual.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("radByManual.Dock")));
			this.radByManual.Enabled = ((bool)(resources.GetObject("radByManual.Enabled")));
			this.radByManual.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("radByManual.FlatStyle")));
			this.radByManual.Font = ((System.Drawing.Font)(resources.GetObject("radByManual.Font")));
			this.radByManual.Image = ((System.Drawing.Image)(resources.GetObject("radByManual.Image")));
			this.radByManual.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("radByManual.ImageAlign")));
			this.radByManual.ImageIndex = ((int)(resources.GetObject("radByManual.ImageIndex")));
			this.radByManual.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("radByManual.ImeMode")));
			this.radByManual.Location = ((System.Drawing.Point)(resources.GetObject("radByManual.Location")));
			this.radByManual.Name = "radByManual";
			this.radByManual.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("radByManual.RightToLeft")));
			this.radByManual.Size = ((System.Drawing.Size)(resources.GetObject("radByManual.Size")));
			this.radByManual.TabIndex = ((int)(resources.GetObject("radByManual.TabIndex")));
			this.radByManual.Text = resources.GetString("radByManual.Text");
			this.radByManual.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("radByManual.TextAlign")));
			this.radByManual.Visible = ((bool)(resources.GetObject("radByManual.Visible")));
			this.radByManual.CheckedChanged += new System.EventHandler(this.radManual_CheckedChanged);
			// 
			// radByPrice
			// 
			this.radByPrice.AccessibleDescription = resources.GetString("radByPrice.AccessibleDescription");
			this.radByPrice.AccessibleName = resources.GetString("radByPrice.AccessibleName");
			this.radByPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("radByPrice.Anchor")));
			this.radByPrice.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("radByPrice.Appearance")));
			this.radByPrice.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radByPrice.BackgroundImage")));
			this.radByPrice.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("radByPrice.CheckAlign")));
			this.radByPrice.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("radByPrice.Dock")));
			this.radByPrice.Enabled = ((bool)(resources.GetObject("radByPrice.Enabled")));
			this.radByPrice.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("radByPrice.FlatStyle")));
			this.radByPrice.Font = ((System.Drawing.Font)(resources.GetObject("radByPrice.Font")));
			this.radByPrice.Image = ((System.Drawing.Image)(resources.GetObject("radByPrice.Image")));
			this.radByPrice.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("radByPrice.ImageAlign")));
			this.radByPrice.ImageIndex = ((int)(resources.GetObject("radByPrice.ImageIndex")));
			this.radByPrice.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("radByPrice.ImeMode")));
			this.radByPrice.Location = ((System.Drawing.Point)(resources.GetObject("radByPrice.Location")));
			this.radByPrice.Name = "radByPrice";
			this.radByPrice.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("radByPrice.RightToLeft")));
			this.radByPrice.Size = ((System.Drawing.Size)(resources.GetObject("radByPrice.Size")));
			this.radByPrice.TabIndex = ((int)(resources.GetObject("radByPrice.TabIndex")));
			this.radByPrice.Text = resources.GetString("radByPrice.Text");
			this.radByPrice.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("radByPrice.TextAlign")));
			this.radByPrice.Visible = ((bool)(resources.GetObject("radByPrice.Visible")));
			this.radByPrice.CheckedChanged += new System.EventHandler(this.radByPrice_CheckedChanged);
			// 
			// radByQuantity
			// 
			this.radByQuantity.AccessibleDescription = resources.GetString("radByQuantity.AccessibleDescription");
			this.radByQuantity.AccessibleName = resources.GetString("radByQuantity.AccessibleName");
			this.radByQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("radByQuantity.Anchor")));
			this.radByQuantity.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("radByQuantity.Appearance")));
			this.radByQuantity.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radByQuantity.BackgroundImage")));
			this.radByQuantity.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("radByQuantity.CheckAlign")));
			this.radByQuantity.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("radByQuantity.Dock")));
			this.radByQuantity.Enabled = ((bool)(resources.GetObject("radByQuantity.Enabled")));
			this.radByQuantity.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("radByQuantity.FlatStyle")));
			this.radByQuantity.Font = ((System.Drawing.Font)(resources.GetObject("radByQuantity.Font")));
			this.radByQuantity.Image = ((System.Drawing.Image)(resources.GetObject("radByQuantity.Image")));
			this.radByQuantity.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("radByQuantity.ImageAlign")));
			this.radByQuantity.ImageIndex = ((int)(resources.GetObject("radByQuantity.ImageIndex")));
			this.radByQuantity.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("radByQuantity.ImeMode")));
			this.radByQuantity.Location = ((System.Drawing.Point)(resources.GetObject("radByQuantity.Location")));
			this.radByQuantity.Name = "radByQuantity";
			this.radByQuantity.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("radByQuantity.RightToLeft")));
			this.radByQuantity.Size = ((System.Drawing.Size)(resources.GetObject("radByQuantity.Size")));
			this.radByQuantity.TabIndex = ((int)(resources.GetObject("radByQuantity.TabIndex")));
			this.radByQuantity.Text = resources.GetString("radByQuantity.Text");
			this.radByQuantity.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("radByQuantity.TextAlign")));
			this.radByQuantity.Visible = ((bool)(resources.GetObject("radByQuantity.Visible")));
			this.radByQuantity.CheckedChanged += new System.EventHandler(this.radByQuantity_CheckedChanged);
			// 
			// btnToDistribute
			// 
			this.btnToDistribute.AccessibleDescription = resources.GetString("btnToDistribute.AccessibleDescription");
			this.btnToDistribute.AccessibleName = resources.GetString("btnToDistribute.AccessibleName");
			this.btnToDistribute.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnToDistribute.Anchor")));
			this.btnToDistribute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnToDistribute.BackgroundImage")));
			this.btnToDistribute.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnToDistribute.Dock")));
			this.btnToDistribute.Enabled = ((bool)(resources.GetObject("btnToDistribute.Enabled")));
			this.btnToDistribute.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnToDistribute.FlatStyle")));
			this.btnToDistribute.Font = ((System.Drawing.Font)(resources.GetObject("btnToDistribute.Font")));
			this.btnToDistribute.Image = ((System.Drawing.Image)(resources.GetObject("btnToDistribute.Image")));
			this.btnToDistribute.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnToDistribute.ImageAlign")));
			this.btnToDistribute.ImageIndex = ((int)(resources.GetObject("btnToDistribute.ImageIndex")));
			this.btnToDistribute.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnToDistribute.ImeMode")));
			this.btnToDistribute.Location = ((System.Drawing.Point)(resources.GetObject("btnToDistribute.Location")));
			this.btnToDistribute.Name = "btnToDistribute";
			this.btnToDistribute.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnToDistribute.RightToLeft")));
			this.btnToDistribute.Size = ((System.Drawing.Size)(resources.GetObject("btnToDistribute.Size")));
			this.btnToDistribute.TabIndex = ((int)(resources.GetObject("btnToDistribute.TabIndex")));
			this.btnToDistribute.Text = resources.GetString("btnToDistribute.Text");
			this.btnToDistribute.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnToDistribute.TextAlign")));
			this.btnToDistribute.Visible = ((bool)(resources.GetObject("btnToDistribute.Visible")));
			this.btnToDistribute.Click += new System.EventHandler(this.btnToDistribute_Click);
			// 
			// lblDistributionAmount
			// 
			this.lblDistributionAmount.AccessibleDescription = resources.GetString("lblDistributionAmount.AccessibleDescription");
			this.lblDistributionAmount.AccessibleName = resources.GetString("lblDistributionAmount.AccessibleName");
			this.lblDistributionAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDistributionAmount.Anchor")));
			this.lblDistributionAmount.AutoSize = ((bool)(resources.GetObject("lblDistributionAmount.AutoSize")));
			this.lblDistributionAmount.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDistributionAmount.Dock")));
			this.lblDistributionAmount.Enabled = ((bool)(resources.GetObject("lblDistributionAmount.Enabled")));
			this.lblDistributionAmount.Font = ((System.Drawing.Font)(resources.GetObject("lblDistributionAmount.Font")));
			this.lblDistributionAmount.Image = ((System.Drawing.Image)(resources.GetObject("lblDistributionAmount.Image")));
			this.lblDistributionAmount.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDistributionAmount.ImageAlign")));
			this.lblDistributionAmount.ImageIndex = ((int)(resources.GetObject("lblDistributionAmount.ImageIndex")));
			this.lblDistributionAmount.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDistributionAmount.ImeMode")));
			this.lblDistributionAmount.Location = ((System.Drawing.Point)(resources.GetObject("lblDistributionAmount.Location")));
			this.lblDistributionAmount.Name = "lblDistributionAmount";
			this.lblDistributionAmount.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDistributionAmount.RightToLeft")));
			this.lblDistributionAmount.Size = ((System.Drawing.Size)(resources.GetObject("lblDistributionAmount.Size")));
			this.lblDistributionAmount.TabIndex = ((int)(resources.GetObject("lblDistributionAmount.TabIndex")));
			this.lblDistributionAmount.Text = resources.GetString("lblDistributionAmount.Text");
			this.lblDistributionAmount.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDistributionAmount.TextAlign")));
			this.lblDistributionAmount.Visible = ((bool)(resources.GetObject("lblDistributionAmount.Visible")));
			// 
			// lblTransDate
			// 
			this.lblTransDate.AccessibleDescription = resources.GetString("lblTransDate.AccessibleDescription");
			this.lblTransDate.AccessibleName = resources.GetString("lblTransDate.AccessibleName");
			this.lblTransDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblTransDate.Anchor")));
			this.lblTransDate.AutoSize = ((bool)(resources.GetObject("lblTransDate.AutoSize")));
			this.lblTransDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblTransDate.Dock")));
			this.lblTransDate.Enabled = ((bool)(resources.GetObject("lblTransDate.Enabled")));
			this.lblTransDate.Font = ((System.Drawing.Font)(resources.GetObject("lblTransDate.Font")));
			this.lblTransDate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTransDate.Image = ((System.Drawing.Image)(resources.GetObject("lblTransDate.Image")));
			this.lblTransDate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTransDate.ImageAlign")));
			this.lblTransDate.ImageIndex = ((int)(resources.GetObject("lblTransDate.ImageIndex")));
			this.lblTransDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblTransDate.ImeMode")));
			this.lblTransDate.Location = ((System.Drawing.Point)(resources.GetObject("lblTransDate.Location")));
			this.lblTransDate.Name = "lblTransDate";
			this.lblTransDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblTransDate.RightToLeft")));
			this.lblTransDate.Size = ((System.Drawing.Size)(resources.GetObject("lblTransDate.Size")));
			this.lblTransDate.TabIndex = ((int)(resources.GetObject("lblTransDate.TabIndex")));
			this.lblTransDate.Text = resources.GetString("lblTransDate.Text");
			this.lblTransDate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblTransDate.TextAlign")));
			this.lblTransDate.Visible = ((bool)(resources.GetObject("lblTransDate.Visible")));
			// 
			// lblCCN
			// 
			this.lblCCN.AccessibleDescription = resources.GetString("lblCCN.AccessibleDescription");
			this.lblCCN.AccessibleName = resources.GetString("lblCCN.AccessibleName");
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCCN.Anchor")));
			this.lblCCN.AutoSize = ((bool)(resources.GetObject("lblCCN.AutoSize")));
			this.lblCCN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCCN.Dock")));
			this.lblCCN.Enabled = ((bool)(resources.GetObject("lblCCN.Enabled")));
			this.lblCCN.Font = ((System.Drawing.Font)(resources.GetObject("lblCCN.Font")));
			this.lblCCN.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCCN.Image = ((System.Drawing.Image)(resources.GetObject("lblCCN.Image")));
			this.lblCCN.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCCN.ImageAlign")));
			this.lblCCN.ImageIndex = ((int)(resources.GetObject("lblCCN.ImageIndex")));
			this.lblCCN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCCN.ImeMode")));
			this.lblCCN.Location = ((System.Drawing.Point)(resources.GetObject("lblCCN.Location")));
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCCN.RightToLeft")));
			this.lblCCN.Size = ((System.Drawing.Size)(resources.GetObject("lblCCN.Size")));
			this.lblCCN.TabIndex = ((int)(resources.GetObject("lblCCN.TabIndex")));
			this.lblCCN.Text = resources.GetString("lblCCN.Text");
			this.lblCCN.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCCN.TextAlign")));
			this.lblCCN.Visible = ((bool)(resources.GetObject("lblCCN.Visible")));
			// 
			// lblVendorName
			// 
			this.lblVendorName.AccessibleDescription = resources.GetString("lblVendorName.AccessibleDescription");
			this.lblVendorName.AccessibleName = resources.GetString("lblVendorName.AccessibleName");
			this.lblVendorName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblVendorName.Anchor")));
			this.lblVendorName.AutoSize = ((bool)(resources.GetObject("lblVendorName.AutoSize")));
			this.lblVendorName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblVendorName.Dock")));
			this.lblVendorName.Enabled = ((bool)(resources.GetObject("lblVendorName.Enabled")));
			this.lblVendorName.Font = ((System.Drawing.Font)(resources.GetObject("lblVendorName.Font")));
			this.lblVendorName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVendorName.Image = ((System.Drawing.Image)(resources.GetObject("lblVendorName.Image")));
			this.lblVendorName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVendorName.ImageAlign")));
			this.lblVendorName.ImageIndex = ((int)(resources.GetObject("lblVendorName.ImageIndex")));
			this.lblVendorName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblVendorName.ImeMode")));
			this.lblVendorName.Location = ((System.Drawing.Point)(resources.GetObject("lblVendorName.Location")));
			this.lblVendorName.Name = "lblVendorName";
			this.lblVendorName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblVendorName.RightToLeft")));
			this.lblVendorName.Size = ((System.Drawing.Size)(resources.GetObject("lblVendorName.Size")));
			this.lblVendorName.TabIndex = ((int)(resources.GetObject("lblVendorName.TabIndex")));
			this.lblVendorName.Text = resources.GetString("lblVendorName.Text");
			this.lblVendorName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVendorName.TextAlign")));
			this.lblVendorName.Visible = ((bool)(resources.GetObject("lblVendorName.Visible")));
			// 
			// txtVendorCode
			// 
			this.txtVendorCode.AccessibleDescription = resources.GetString("txtVendorCode.AccessibleDescription");
			this.txtVendorCode.AccessibleName = resources.GetString("txtVendorCode.AccessibleName");
			this.txtVendorCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtVendorCode.Anchor")));
			this.txtVendorCode.AutoSize = ((bool)(resources.GetObject("txtVendorCode.AutoSize")));
			this.txtVendorCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtVendorCode.BackgroundImage")));
			this.txtVendorCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtVendorCode.Dock")));
			this.txtVendorCode.Enabled = ((bool)(resources.GetObject("txtVendorCode.Enabled")));
			this.txtVendorCode.Font = ((System.Drawing.Font)(resources.GetObject("txtVendorCode.Font")));
			this.txtVendorCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtVendorCode.ImeMode")));
			this.txtVendorCode.Location = ((System.Drawing.Point)(resources.GetObject("txtVendorCode.Location")));
			this.txtVendorCode.MaxLength = ((int)(resources.GetObject("txtVendorCode.MaxLength")));
			this.txtVendorCode.Multiline = ((bool)(resources.GetObject("txtVendorCode.Multiline")));
			this.txtVendorCode.Name = "txtVendorCode";
			this.txtVendorCode.PasswordChar = ((char)(resources.GetObject("txtVendorCode.PasswordChar")));
			this.txtVendorCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtVendorCode.RightToLeft")));
			this.txtVendorCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtVendorCode.ScrollBars")));
			this.txtVendorCode.Size = ((System.Drawing.Size)(resources.GetObject("txtVendorCode.Size")));
			this.txtVendorCode.TabIndex = ((int)(resources.GetObject("txtVendorCode.TabIndex")));
			this.txtVendorCode.Text = resources.GetString("txtVendorCode.Text");
			this.txtVendorCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtVendorCode.TextAlign")));
			this.txtVendorCode.Visible = ((bool)(resources.GetObject("txtVendorCode.Visible")));
			this.txtVendorCode.WordWrap = ((bool)(resources.GetObject("txtVendorCode.WordWrap")));
			// 
			// lblVendorCode
			// 
			this.lblVendorCode.AccessibleDescription = resources.GetString("lblVendorCode.AccessibleDescription");
			this.lblVendorCode.AccessibleName = resources.GetString("lblVendorCode.AccessibleName");
			this.lblVendorCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblVendorCode.Anchor")));
			this.lblVendorCode.AutoSize = ((bool)(resources.GetObject("lblVendorCode.AutoSize")));
			this.lblVendorCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblVendorCode.Dock")));
			this.lblVendorCode.Enabled = ((bool)(resources.GetObject("lblVendorCode.Enabled")));
			this.lblVendorCode.Font = ((System.Drawing.Font)(resources.GetObject("lblVendorCode.Font")));
			this.lblVendorCode.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVendorCode.Image = ((System.Drawing.Image)(resources.GetObject("lblVendorCode.Image")));
			this.lblVendorCode.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVendorCode.ImageAlign")));
			this.lblVendorCode.ImageIndex = ((int)(resources.GetObject("lblVendorCode.ImageIndex")));
			this.lblVendorCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblVendorCode.ImeMode")));
			this.lblVendorCode.Location = ((System.Drawing.Point)(resources.GetObject("lblVendorCode.Location")));
			this.lblVendorCode.Name = "lblVendorCode";
			this.lblVendorCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblVendorCode.RightToLeft")));
			this.lblVendorCode.Size = ((System.Drawing.Size)(resources.GetObject("lblVendorCode.Size")));
			this.lblVendorCode.TabIndex = ((int)(resources.GetObject("lblVendorCode.TabIndex")));
			this.lblVendorCode.Text = resources.GetString("lblVendorCode.Text");
			this.lblVendorCode.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVendorCode.TextAlign")));
			this.lblVendorCode.Visible = ((bool)(resources.GetObject("lblVendorCode.Visible")));
			// 
			// txtPOOrderNo
			// 
			this.txtPOOrderNo.AccessibleDescription = resources.GetString("txtPOOrderNo.AccessibleDescription");
			this.txtPOOrderNo.AccessibleName = resources.GetString("txtPOOrderNo.AccessibleName");
			this.txtPOOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPOOrderNo.Anchor")));
			this.txtPOOrderNo.AutoSize = ((bool)(resources.GetObject("txtPOOrderNo.AutoSize")));
			this.txtPOOrderNo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPOOrderNo.BackgroundImage")));
			this.txtPOOrderNo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPOOrderNo.Dock")));
			this.txtPOOrderNo.Enabled = ((bool)(resources.GetObject("txtPOOrderNo.Enabled")));
			this.txtPOOrderNo.Font = ((System.Drawing.Font)(resources.GetObject("txtPOOrderNo.Font")));
			this.txtPOOrderNo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPOOrderNo.ImeMode")));
			this.txtPOOrderNo.Location = ((System.Drawing.Point)(resources.GetObject("txtPOOrderNo.Location")));
			this.txtPOOrderNo.MaxLength = ((int)(resources.GetObject("txtPOOrderNo.MaxLength")));
			this.txtPOOrderNo.Multiline = ((bool)(resources.GetObject("txtPOOrderNo.Multiline")));
			this.txtPOOrderNo.Name = "txtPOOrderNo";
			this.txtPOOrderNo.PasswordChar = ((char)(resources.GetObject("txtPOOrderNo.PasswordChar")));
			this.txtPOOrderNo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPOOrderNo.RightToLeft")));
			this.txtPOOrderNo.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPOOrderNo.ScrollBars")));
			this.txtPOOrderNo.Size = ((System.Drawing.Size)(resources.GetObject("txtPOOrderNo.Size")));
			this.txtPOOrderNo.TabIndex = ((int)(resources.GetObject("txtPOOrderNo.TabIndex")));
			this.txtPOOrderNo.Text = resources.GetString("txtPOOrderNo.Text");
			this.txtPOOrderNo.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPOOrderNo.TextAlign")));
			this.txtPOOrderNo.Visible = ((bool)(resources.GetObject("txtPOOrderNo.Visible")));
			this.txtPOOrderNo.WordWrap = ((bool)(resources.GetObject("txtPOOrderNo.WordWrap")));
			// 
			// lblOrderNo
			// 
			this.lblOrderNo.AccessibleDescription = resources.GetString("lblOrderNo.AccessibleDescription");
			this.lblOrderNo.AccessibleName = resources.GetString("lblOrderNo.AccessibleName");
			this.lblOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblOrderNo.Anchor")));
			this.lblOrderNo.AutoSize = ((bool)(resources.GetObject("lblOrderNo.AutoSize")));
			this.lblOrderNo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblOrderNo.Dock")));
			this.lblOrderNo.Enabled = ((bool)(resources.GetObject("lblOrderNo.Enabled")));
			this.lblOrderNo.Font = ((System.Drawing.Font)(resources.GetObject("lblOrderNo.Font")));
			this.lblOrderNo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblOrderNo.Image = ((System.Drawing.Image)(resources.GetObject("lblOrderNo.Image")));
			this.lblOrderNo.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblOrderNo.ImageAlign")));
			this.lblOrderNo.ImageIndex = ((int)(resources.GetObject("lblOrderNo.ImageIndex")));
			this.lblOrderNo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblOrderNo.ImeMode")));
			this.lblOrderNo.Location = ((System.Drawing.Point)(resources.GetObject("lblOrderNo.Location")));
			this.lblOrderNo.Name = "lblOrderNo";
			this.lblOrderNo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblOrderNo.RightToLeft")));
			this.lblOrderNo.Size = ((System.Drawing.Size)(resources.GetObject("lblOrderNo.Size")));
			this.lblOrderNo.TabIndex = ((int)(resources.GetObject("lblOrderNo.TabIndex")));
			this.lblOrderNo.Text = resources.GetString("lblOrderNo.Text");
			this.lblOrderNo.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblOrderNo.TextAlign")));
			this.lblOrderNo.Visible = ((bool)(resources.GetObject("lblOrderNo.Visible")));
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
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
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
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = resources.GetString("dgrdData.AccessibleDescription");
			this.dgrdData.AccessibleName = resources.GetString("dgrdData.AccessibleName");
			this.dgrdData.AllowAddNew = ((bool)(resources.GetObject("dgrdData.AllowAddNew")));
			this.dgrdData.AllowArrows = ((bool)(resources.GetObject("dgrdData.AllowArrows")));
			this.dgrdData.AllowColMove = ((bool)(resources.GetObject("dgrdData.AllowColMove")));
			this.dgrdData.AllowColSelect = ((bool)(resources.GetObject("dgrdData.AllowColSelect")));
			this.dgrdData.AllowDelete = ((bool)(resources.GetObject("dgrdData.AllowDelete")));
			this.dgrdData.AllowDrag = ((bool)(resources.GetObject("dgrdData.AllowDrag")));
			this.dgrdData.AllowFilter = ((bool)(resources.GetObject("dgrdData.AllowFilter")));
			this.dgrdData.AllowHorizontalSplit = ((bool)(resources.GetObject("dgrdData.AllowHorizontalSplit")));
			this.dgrdData.AllowRowSelect = ((bool)(resources.GetObject("dgrdData.AllowRowSelect")));
			this.dgrdData.AllowSort = ((bool)(resources.GetObject("dgrdData.AllowSort")));
			this.dgrdData.AllowUpdate = ((bool)(resources.GetObject("dgrdData.AllowUpdate")));
			this.dgrdData.AllowUpdateOnBlur = ((bool)(resources.GetObject("dgrdData.AllowUpdateOnBlur")));
			this.dgrdData.AllowVerticalSplit = ((bool)(resources.GetObject("dgrdData.AllowVerticalSplit")));
			this.dgrdData.AlternatingRows = ((bool)(resources.GetObject("dgrdData.AlternatingRows")));
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dgrdData.Anchor")));
			this.dgrdData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dgrdData.BackgroundImage")));
			this.dgrdData.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dgrdData.BorderStyle")));
			this.dgrdData.Caption = resources.GetString("dgrdData.Caption");
			this.dgrdData.CaptionHeight = ((int)(resources.GetObject("dgrdData.CaptionHeight")));
			this.dgrdData.CellTipsDelay = ((int)(resources.GetObject("dgrdData.CellTipsDelay")));
			this.dgrdData.CellTipsWidth = ((int)(resources.GetObject("dgrdData.CellTipsWidth")));
			this.dgrdData.ChildGrid = ((C1.Win.C1TrueDBGrid.C1TrueDBGrid)(resources.GetObject("dgrdData.ChildGrid")));
			this.dgrdData.CollapseColor = ((System.Drawing.Color)(resources.GetObject("dgrdData.CollapseColor")));
			this.dgrdData.ColumnFooters = ((bool)(resources.GetObject("dgrdData.ColumnFooters")));
			this.dgrdData.ColumnHeaders = ((bool)(resources.GetObject("dgrdData.ColumnHeaders")));
			this.dgrdData.DefColWidth = ((int)(resources.GetObject("dgrdData.DefColWidth")));
			this.dgrdData.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dgrdData.Dock")));
			this.dgrdData.EditDropDown = ((bool)(resources.GetObject("dgrdData.EditDropDown")));
			this.dgrdData.EmptyRows = ((bool)(resources.GetObject("dgrdData.EmptyRows")));
			this.dgrdData.Enabled = ((bool)(resources.GetObject("dgrdData.Enabled")));
			this.dgrdData.ExpandColor = ((System.Drawing.Color)(resources.GetObject("dgrdData.ExpandColor")));
			this.dgrdData.ExposeCellMode = ((C1.Win.C1TrueDBGrid.ExposeCellModeEnum)(resources.GetObject("dgrdData.ExposeCellMode")));
			this.dgrdData.ExtendRightColumn = ((bool)(resources.GetObject("dgrdData.ExtendRightColumn")));
			this.dgrdData.FetchRowStyles = ((bool)(resources.GetObject("dgrdData.FetchRowStyles")));
			this.dgrdData.FilterBar = ((bool)(resources.GetObject("dgrdData.FilterBar")));
			this.dgrdData.FlatStyle = ((C1.Win.C1TrueDBGrid.FlatModeEnum)(resources.GetObject("dgrdData.FlatStyle")));
			this.dgrdData.Font = ((System.Drawing.Font)(resources.GetObject("dgrdData.Font")));
			this.dgrdData.GroupByAreaVisible = ((bool)(resources.GetObject("dgrdData.GroupByAreaVisible")));
			this.dgrdData.GroupByCaption = resources.GetString("dgrdData.GroupByCaption");
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dgrdData.ImeMode")));
			this.dgrdData.LinesPerRow = ((int)(resources.GetObject("dgrdData.LinesPerRow")));
			this.dgrdData.Location = ((System.Drawing.Point)(resources.GetObject("dgrdData.Location")));
			this.dgrdData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdData.Name = "dgrdData";
			this.dgrdData.PictureAddnewRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureAddnewRow")));
			this.dgrdData.PictureCurrentRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureCurrentRow")));
			this.dgrdData.PictureFilterBar = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureFilterBar")));
			this.dgrdData.PictureFooterRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureFooterRow")));
			this.dgrdData.PictureHeaderRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureHeaderRow")));
			this.dgrdData.PictureModifiedRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureModifiedRow")));
			this.dgrdData.PictureStandardRow = ((System.Drawing.Image)(resources.GetObject("dgrdData.PictureStandardRow")));
			this.dgrdData.PreviewInfo.AllowSizing = ((bool)(resources.GetObject("dgrdData.PreviewInfo.AllowSizing")));
			this.dgrdData.PreviewInfo.Caption = resources.GetString("dgrdData.PreviewInfo.Caption");
			this.dgrdData.PreviewInfo.Location = ((System.Drawing.Point)(resources.GetObject("dgrdData.PreviewInfo.Location")));
			this.dgrdData.PreviewInfo.Size = ((System.Drawing.Size)(resources.GetObject("dgrdData.PreviewInfo.Size")));
			this.dgrdData.PreviewInfo.ToolBars = ((bool)(resources.GetObject("dgrdData.PreviewInfo.ToolBars")));
			this.dgrdData.PreviewInfo.UIStrings.Content = ((string[])(resources.GetObject("dgrdData.PreviewInfo.UIStrings.Content")));
			this.dgrdData.PreviewInfo.ZoomFactor = ((System.Double)(resources.GetObject("dgrdData.PreviewInfo.ZoomFactor")));
			this.dgrdData.PrintInfo.MaxRowHeight = ((int)(resources.GetObject("dgrdData.PrintInfo.MaxRowHeight")));
			this.dgrdData.PrintInfo.OwnerDrawPageFooter = ((bool)(resources.GetObject("dgrdData.PrintInfo.OwnerDrawPageFooter")));
			this.dgrdData.PrintInfo.OwnerDrawPageHeader = ((bool)(resources.GetObject("dgrdData.PrintInfo.OwnerDrawPageHeader")));
			this.dgrdData.PrintInfo.PageFooter = resources.GetString("dgrdData.PrintInfo.PageFooter");
			this.dgrdData.PrintInfo.PageFooterHeight = ((int)(resources.GetObject("dgrdData.PrintInfo.PageFooterHeight")));
			this.dgrdData.PrintInfo.PageHeader = resources.GetString("dgrdData.PrintInfo.PageHeader");
			this.dgrdData.PrintInfo.PageHeaderHeight = ((int)(resources.GetObject("dgrdData.PrintInfo.PageHeaderHeight")));
			this.dgrdData.PrintInfo.PrintHorizontalSplits = ((bool)(resources.GetObject("dgrdData.PrintInfo.PrintHorizontalSplits")));
			this.dgrdData.PrintInfo.ProgressCaption = resources.GetString("dgrdData.PrintInfo.ProgressCaption");
			this.dgrdData.PrintInfo.RepeatColumnFooters = ((bool)(resources.GetObject("dgrdData.PrintInfo.RepeatColumnFooters")));
			this.dgrdData.PrintInfo.RepeatColumnHeaders = ((bool)(resources.GetObject("dgrdData.PrintInfo.RepeatColumnHeaders")));
			this.dgrdData.PrintInfo.RepeatGridHeader = ((bool)(resources.GetObject("dgrdData.PrintInfo.RepeatGridHeader")));
			this.dgrdData.PrintInfo.RepeatSplitHeaders = ((bool)(resources.GetObject("dgrdData.PrintInfo.RepeatSplitHeaders")));
			this.dgrdData.PrintInfo.ShowOptionsDialog = ((bool)(resources.GetObject("dgrdData.PrintInfo.ShowOptionsDialog")));
			this.dgrdData.PrintInfo.ShowProgressForm = ((bool)(resources.GetObject("dgrdData.PrintInfo.ShowProgressForm")));
			this.dgrdData.PrintInfo.ShowSelection = ((bool)(resources.GetObject("dgrdData.PrintInfo.ShowSelection")));
			this.dgrdData.PrintInfo.UseGridColors = ((bool)(resources.GetObject("dgrdData.PrintInfo.UseGridColors")));
			this.dgrdData.RecordSelectors = ((bool)(resources.GetObject("dgrdData.RecordSelectors")));
			this.dgrdData.RecordSelectorWidth = ((int)(resources.GetObject("dgrdData.RecordSelectorWidth")));
			this.dgrdData.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dgrdData.RightToLeft")));
			this.dgrdData.RowDivider.Color = ((System.Drawing.Color)(resources.GetObject("resource.Color")));
			this.dgrdData.RowDivider.Style = ((C1.Win.C1TrueDBGrid.LineStyleEnum)(resources.GetObject("resource.Style")));
			this.dgrdData.RowHeight = ((int)(resources.GetObject("dgrdData.RowHeight")));
			this.dgrdData.RowSubDividerColor = ((System.Drawing.Color)(resources.GetObject("dgrdData.RowSubDividerColor")));
			this.dgrdData.ScrollTips = ((bool)(resources.GetObject("dgrdData.ScrollTips")));
			this.dgrdData.ScrollTrack = ((bool)(resources.GetObject("dgrdData.ScrollTrack")));
			this.dgrdData.Size = ((System.Drawing.Size)(resources.GetObject("dgrdData.Size")));
			this.dgrdData.SpringMode = ((bool)(resources.GetObject("dgrdData.SpringMode")));
			this.dgrdData.TabAcrossSplits = ((bool)(resources.GetObject("dgrdData.TabAcrossSplits")));
			this.dgrdData.TabIndex = ((int)(resources.GetObject("dgrdData.TabIndex")));
			this.dgrdData.Text = resources.GetString("dgrdData.Text");
			this.dgrdData.ViewCaptionWidth = ((int)(resources.GetObject("dgrdData.ViewCaptionWidth")));
			this.dgrdData.ViewColumnWidth = ((int)(resources.GetObject("dgrdData.ViewColumnWidth")));
			this.dgrdData.Visible = ((bool)(resources.GetObject("dgrdData.Visible")));
			this.dgrdData.WrapCellPointer = ((bool)(resources.GetObject("dgrdData.WrapCellPointer")));
			this.dgrdData.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
			this.dgrdData.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.dgrdData_RowColChange);
			this.dgrdData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdData_BeforeDelete);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
			// 
			// txtTransDate
			// 
			this.txtTransDate.AccessibleDescription = resources.GetString("txtTransDate.AccessibleDescription");
			this.txtTransDate.AccessibleName = resources.GetString("txtTransDate.AccessibleName");
			this.txtTransDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtTransDate.Anchor")));
			this.txtTransDate.AutoSize = ((bool)(resources.GetObject("txtTransDate.AutoSize")));
			this.txtTransDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtTransDate.BackgroundImage")));
			this.txtTransDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtTransDate.Dock")));
			this.txtTransDate.Enabled = ((bool)(resources.GetObject("txtTransDate.Enabled")));
			this.txtTransDate.Font = ((System.Drawing.Font)(resources.GetObject("txtTransDate.Font")));
			this.txtTransDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtTransDate.ImeMode")));
			this.txtTransDate.Location = ((System.Drawing.Point)(resources.GetObject("txtTransDate.Location")));
			this.txtTransDate.MaxLength = ((int)(resources.GetObject("txtTransDate.MaxLength")));
			this.txtTransDate.Multiline = ((bool)(resources.GetObject("txtTransDate.Multiline")));
			this.txtTransDate.Name = "txtTransDate";
			this.txtTransDate.PasswordChar = ((char)(resources.GetObject("txtTransDate.PasswordChar")));
			this.txtTransDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtTransDate.RightToLeft")));
			this.txtTransDate.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtTransDate.ScrollBars")));
			this.txtTransDate.Size = ((System.Drawing.Size)(resources.GetObject("txtTransDate.Size")));
			this.txtTransDate.TabIndex = ((int)(resources.GetObject("txtTransDate.TabIndex")));
			this.txtTransDate.Text = resources.GetString("txtTransDate.Text");
			this.txtTransDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtTransDate.TextAlign")));
			this.txtTransDate.Visible = ((bool)(resources.GetObject("txtTransDate.Visible")));
			this.txtTransDate.WordWrap = ((bool)(resources.GetObject("txtTransDate.WordWrap")));
			// 
			// txtCCN
			// 
			this.txtCCN.AccessibleDescription = resources.GetString("txtCCN.AccessibleDescription");
			this.txtCCN.AccessibleName = resources.GetString("txtCCN.AccessibleName");
			this.txtCCN.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCCN.Anchor")));
			this.txtCCN.AutoSize = ((bool)(resources.GetObject("txtCCN.AutoSize")));
			this.txtCCN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCCN.BackgroundImage")));
			this.txtCCN.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCCN.Dock")));
			this.txtCCN.Enabled = ((bool)(resources.GetObject("txtCCN.Enabled")));
			this.txtCCN.Font = ((System.Drawing.Font)(resources.GetObject("txtCCN.Font")));
			this.txtCCN.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCCN.ImeMode")));
			this.txtCCN.Location = ((System.Drawing.Point)(resources.GetObject("txtCCN.Location")));
			this.txtCCN.MaxLength = ((int)(resources.GetObject("txtCCN.MaxLength")));
			this.txtCCN.Multiline = ((bool)(resources.GetObject("txtCCN.Multiline")));
			this.txtCCN.Name = "txtCCN";
			this.txtCCN.PasswordChar = ((char)(resources.GetObject("txtCCN.PasswordChar")));
			this.txtCCN.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCCN.RightToLeft")));
			this.txtCCN.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCCN.ScrollBars")));
			this.txtCCN.Size = ((System.Drawing.Size)(resources.GetObject("txtCCN.Size")));
			this.txtCCN.TabIndex = ((int)(resources.GetObject("txtCCN.TabIndex")));
			this.txtCCN.Text = resources.GetString("txtCCN.Text");
			this.txtCCN.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCCN.TextAlign")));
			this.txtCCN.Visible = ((bool)(resources.GetObject("txtCCN.Visible")));
			this.txtCCN.WordWrap = ((bool)(resources.GetObject("txtCCN.WordWrap")));
			// 
			// txtVendorName
			// 
			this.txtVendorName.AccessibleDescription = resources.GetString("txtVendorName.AccessibleDescription");
			this.txtVendorName.AccessibleName = resources.GetString("txtVendorName.AccessibleName");
			this.txtVendorName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtVendorName.Anchor")));
			this.txtVendorName.AutoSize = ((bool)(resources.GetObject("txtVendorName.AutoSize")));
			this.txtVendorName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtVendorName.BackgroundImage")));
			this.txtVendorName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtVendorName.Dock")));
			this.txtVendorName.Enabled = ((bool)(resources.GetObject("txtVendorName.Enabled")));
			this.txtVendorName.Font = ((System.Drawing.Font)(resources.GetObject("txtVendorName.Font")));
			this.txtVendorName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtVendorName.ImeMode")));
			this.txtVendorName.Location = ((System.Drawing.Point)(resources.GetObject("txtVendorName.Location")));
			this.txtVendorName.MaxLength = ((int)(resources.GetObject("txtVendorName.MaxLength")));
			this.txtVendorName.Multiline = ((bool)(resources.GetObject("txtVendorName.Multiline")));
			this.txtVendorName.Name = "txtVendorName";
			this.txtVendorName.PasswordChar = ((char)(resources.GetObject("txtVendorName.PasswordChar")));
			this.txtVendorName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtVendorName.RightToLeft")));
			this.txtVendorName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtVendorName.ScrollBars")));
			this.txtVendorName.Size = ((System.Drawing.Size)(resources.GetObject("txtVendorName.Size")));
			this.txtVendorName.TabIndex = ((int)(resources.GetObject("txtVendorName.TabIndex")));
			this.txtVendorName.Text = resources.GetString("txtVendorName.Text");
			this.txtVendorName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtVendorName.TextAlign")));
			this.txtVendorName.Visible = ((bool)(resources.GetObject("txtVendorName.Visible")));
			this.txtVendorName.WordWrap = ((bool)(resources.GetObject("txtVendorName.WordWrap")));
			// 
			// txtCellValue
			// 
			this.txtCellValue.AccessibleDescription = resources.GetString("txtCellValue.AccessibleDescription");
			this.txtCellValue.AccessibleName = resources.GetString("txtCellValue.AccessibleName");
			this.txtCellValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCellValue.Anchor")));
			this.txtCellValue.AutoSize = ((bool)(resources.GetObject("txtCellValue.AutoSize")));
			this.txtCellValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCellValue.BackgroundImage")));
			this.txtCellValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtCellValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCellValue.Dock")));
			this.txtCellValue.Enabled = ((bool)(resources.GetObject("txtCellValue.Enabled")));
			this.txtCellValue.Font = ((System.Drawing.Font)(resources.GetObject("txtCellValue.Font")));
			this.txtCellValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCellValue.ImeMode")));
			this.txtCellValue.Location = ((System.Drawing.Point)(resources.GetObject("txtCellValue.Location")));
			this.txtCellValue.MaxLength = ((int)(resources.GetObject("txtCellValue.MaxLength")));
			this.txtCellValue.Multiline = ((bool)(resources.GetObject("txtCellValue.Multiline")));
			this.txtCellValue.Name = "txtCellValue";
			this.txtCellValue.PasswordChar = ((char)(resources.GetObject("txtCellValue.PasswordChar")));
			this.txtCellValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCellValue.RightToLeft")));
			this.txtCellValue.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCellValue.ScrollBars")));
			this.txtCellValue.Size = ((System.Drawing.Size)(resources.GetObject("txtCellValue.Size")));
			this.txtCellValue.TabIndex = ((int)(resources.GetObject("txtCellValue.TabIndex")));
			this.txtCellValue.Text = resources.GetString("txtCellValue.Text");
			this.txtCellValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCellValue.TextAlign")));
			this.txtCellValue.Visible = ((bool)(resources.GetObject("txtCellValue.Visible")));
			this.txtCellValue.WordWrap = ((bool)(resources.GetObject("txtCellValue.WordWrap")));
			// 
			// POAdditionCharges
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
			this.Controls.Add(this.lblTotalVATAmount);
			this.Controls.Add(this.txtVendorName);
			this.Controls.Add(this.txtCCN);
			this.Controls.Add(this.txtTransDate);
			this.Controls.Add(this.txtTotalAmount);
			this.Controls.Add(this.txtTotalTaxAmount);
			this.Controls.Add(this.txtTotalVATAmount);
			this.Controls.Add(this.txtVendorCode);
			this.Controls.Add(this.txtPOOrderNo);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.txtCellValue);
			this.Controls.Add(this.grbDistribution);
			this.Controls.Add(this.lblTransDate);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblVendorName);
			this.Controls.Add(this.lblVendorCode);
			this.Controls.Add(this.lblOrderNo);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.lblTotalAmount);
			this.Controls.Add(this.lblTotalTaxAmount);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "POAdditionCharges";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.POAdditionCharges_Closing);
			this.Load += new System.EventHandler(this.POAdditionCharges_Load);
			this.grbDistribution.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtDistributionAmount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Form events
		private void POAdditionCharges_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".POAdditionCharges_Load()";
			try
			{
				#region Apply security
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					Close();
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					return;
				}
				#endregion

				// initial value
				voPOMaster = new PO_PurchaseOrderMasterVO();
				voPODetail = new PO_PurchaseOrderDetailVO();
				voAddCharge = new MST_AddChargeVO();
				voReason = new MST_ReasonVO();
				boPOAddCharge = new POAdditionChargesBO();
				boUtils = new UtilsBO();

				// format distribution amount text box
				txtDistributionAmount.FormatType = FormatTypeEnum.CustomFormat;
				txtDistributionAmount.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;
				
				// get POMasterVO object
				voPOMaster = (PO_PurchaseOrderMasterVO)boPOAddCharge.GetPOMasterVO(intPOMasterID);
				bool blnIsCharged = boPOAddCharge.AlreadyCharged(intPOMasterID);
				// if already charged, get old value
				if (blnIsCharged)
				{
					dstData = boPOAddCharge.GetAdditionalChargeByPOMasterID(intPOMasterID);
					decimal decDistributionAmount = 0;
					// re-calculate lastest distribution amount
					foreach (DataRow drowData in dstData.Tables[0].Rows)
					{
						decDistributionAmount += decimal.Parse(drowData[PO_AdditionChargesTable.AMOUNT_FLD].ToString());
					}
					txtDistributionAmount.Value = decDistributionAmount.ToString(Constants.CELL_NUMBER_FORMAT);
				}
				else // else get new value
					dstData = boPOAddCharge.GetDataByPOMasterID(intPOMasterID);
				// assign default value to PurchaseOrderMasterID column
				dstData.Tables[0].Columns[PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD].DefaultValue = intPOMasterID;
				// get all PODetail by Master
				tblPODetails = boPOAddCharge.ListPODetailByPOMaster(intPOMasterID);
				
				// get CCN code fill to form
				txtCCN.Text = boUtils.GetCCNCodeFromID(voPOMaster.CCNID);
				txtPOOrderNo.Text = voPOMaster.Code;
				txtTransDate.Text = voPOMaster.OrderDate.ToString(Constants.DATETIME_FORMAT);
				// get customer information
				MST_PartyVO voParty = new MST_PartyVO();
				voParty = (MST_PartyVO)boPOAddCharge.GetCustomerInfo(voPOMaster.PartyID);
				// fill to label
				txtVendorCode.Text = voParty.Code;
				txtVendorName.Text = voParty.Name;
				// bind data source for the true db grid
				BindDataToGrid();
				// update Total Amount, Total NET Amount, Total VAT Amount
				UpdateTotalAmount();
				// if all of product have the same UM then set radByQuantity = true
				blnChargeByQuantity = boPOAddCharge.IsChargeByQuantity(intPOMasterID);
				if (blnChargeByQuantity)
				{
					// charge by quantity
					radByQuantity.Checked = true;
					//dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.UNITPRICE_FLD].Locked = false;
					//dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.QUANTITY_FLD].Locked = false;
				}
				else
				{
					radByPrice.Checked = true;
				}
				txtDistributionAmount.Focus();
				#region Set Textbox to read-only
				txtPOOrderNo.ReadOnly = true;
				txtTransDate.ReadOnly = true;
				txtVendorCode.ReadOnly = true;
				txtVendorName.ReadOnly = true;
				txtCCN.ReadOnly = true;
				txtTotalAmount.ReadOnly = true;
				txtTotalTaxAmount.ReadOnly = true;
				txtTotalVATAmount.ReadOnly = true;
				#endregion
				AssignEvent(this);
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
		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				// check mandatory field
				int intTotalRows = dgrdData.RowCount;
				if (mFormMode != EnumAction.Edit)
				{
					if (intTotalRows <= 0)
						return;
					for (int i = 0; i < intTotalRows; i++)
					{
						// po line
						try
						{
							int.Parse(dgrdData[i, PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].ToString());
						}
						catch
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD]);
							return;
						}
						// addition charge
						try
						{
							int.Parse(dgrdData[i, PO_AdditionChargesTable.ADDCHARGEID_FLD].ToString());
						}
						catch
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD]);
							return;
						}
						// charge quantity
						try
						{
							decimal decQuantity = decimal.Parse(dgrdData[i, PO_AdditionChargesTable.QUANTITY_FLD].ToString());
							decimal decUnitPrice = decimal.Parse(dgrdData[i, PO_AdditionChargesTable.UNITPRICE_FLD].ToString());
							decimal decAmount = decimal.Parse(dgrdData[i, PO_AdditionChargesTable.AMOUNT_FLD].ToString());
							if ((decQuantity < 0) || (decUnitPrice < 0) || (decAmount < 0))
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_INPUT_NEGATIVE_NUMBER, MessageBoxButtons.OK, MessageBoxIcon.Error);
								dgrdData.Row = i;
								dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_AdditionChargesTable.QUANTITY_FLD]);
								return;
							}
						}
						catch
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_AdditionChargesTable.QUANTITY_FLD]);
							return;
						}
					}
					// if Base Amount is greater than Distribution Amount
					// display error message
					try
					{
						if (decimal.Parse(txtTotalTaxAmount.Text.Trim()) > decimal.Parse(txtDistributionAmount.Value.ToString()))
						{
							//PCSMessageBox.Show(ErrorCode.MESSAGE_BASE_AMOUNT_GREATER_THAN_DISTRIBUTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
							MessageBox.Show("Base Amount cannot greater than Distribution Amount");
							return;
						}
					}
					catch{}
				}
				if (SaveData())
				{
					// display success message
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
					// get new data from database
					dstData = boPOAddCharge.GetAdditionalChargeByPOMasterID(intPOMasterID);
					// rebind to grid
					BindDataToGrid();
				}
				else
				{
					// display error message
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		private void btnDelete_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			if (dgrdData.RowCount == 0)
			{
				return;
			}
			// ask user to confirm delete
			DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dlgResult == DialogResult.Yes)
			{
				try
				{
					int intRowCount = dgrdData.RowCount;
					
					for (int i = intRowCount - 1; i >= 0; i--)
					{
						dstData.Tables[0].Rows[i].Delete();
					}
					dgrdData.UpdateData();
					mFormMode = EnumAction.Edit;
					// turn allow addnew
					dgrdData.AllowAddNew = true;
					txtTotalAmount.Text = decimal.Zero.ToString();
					txtTotalTaxAmount.Text = decimal.Zero.ToString();
					txtTotalVATAmount.Text = decimal.Zero.ToString();
				}
				catch (Exception ex)
				{
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

		private void btnHelp_Click(object sender, EventArgs e)
		{
		
		}

		private void btnToDistribute_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnToDistribute_Click()";
			try
			{
				if (radByPrice.Checked)
					UpdateAmount(BY_PRICE, false);
				else if (radByQuantity.Checked)
					UpdateAmount(BY_QUANTITY, false);
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
		private void radByPrice_CheckedChanged(object sender, EventArgs e)
		{
//			if (blnChargeByQuantity)
//			{
//				PCSMessageBox.Show(ErrorCode.MESSAGE_COULDNOT_CHARGE_BY_PRICE, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
//				if (blnChargeManual)
//					radByManual.Checked = true;
//				else
//					radByQuantity.Checked = true;
//				return;
//			}
//			blnChargeManual = false;
			// enable btnToDistribute
			btnToDistribute.Enabled = true;
		}
		private void radByQuantity_CheckedChanged(object sender, EventArgs e)
		{
//			if (!blnChargeByQuantity)
//			{
//				PCSMessageBox.Show(ErrorCode.MESSAGE_COULDNOT_CHARGE_BY_QUANTITY, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
//				if (blnChargeManual)
//					radByManual.Checked = true;
//				else
//					radByPrice.Checked = true;
//				return;
//			}
//			blnChargeManual = false;
			// enable btnToDistribute
			btnToDistribute.Enabled = true;
		}
		private void radManual_CheckedChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".radManual_CheckedChanged()";
			try
			{
				// clear all rows in grid data
				//dstData.Tables[0].Clear();
				// allow grid to add new row
				//dgrdData.AllowAddNew = true;
				//txtTotalAmount.Text = decimal.Zero.ToString();
				//txtTotalTaxAmount.Text = decimal.Zero.ToString();
				//txtTotalVATAmount.Text = decimal.Zero.ToString();
				// disable btnToDistribute
				btnToDistribute.Enabled = false;
				//blnChargeManual = true;
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
		private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				string strSelectedColumn = dgrdData.Columns[dgrdData.Col].DataField;
				decimal decSelectedValue = 0;
				if (!strSelectedColumn.Equals(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD) && !strSelectedColumn.Equals(MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD)
					&& !strSelectedColumn.Equals(MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD))
				{
					try
					{
						decSelectedValue = decimal.Parse(dgrdData[dgrdData.Row, dgrdData.Col].ToString().Trim());
						if (decSelectedValue < 0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_INPUT_NEGATIVE_NUMBER, MessageBoxButtons.OK, MessageBoxIcon.Error);
							dgrdData.Select();
							dgrdData[dgrdData.Row, dgrdData.Col] = strSelectedValue;
							return;
						}
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxButtons.OK, MessageBoxIcon.Error);
						dgrdData.Focus();
						dgrdData.Select();
						dgrdData[dgrdData.Row, dgrdData.Col] = strSelectedValue;
						return;
					}
				}
				switch (strSelectedColumn)
				{
					case PO_AdditionChargesTable.QUANTITY_FLD:
					case PO_AdditionChargesTable.UNITPRICE_FLD:
						// update amount on grid
						if (radByPrice.Checked)
							UpdateAmount(BY_PRICE, true);
						else if (radByQuantity.Checked)
							UpdateAmount(BY_QUANTITY, true);
						else if (radByManual.Checked)
							UpdateAmount(BY_MANUAL, true);
						break;
					// amount
					case PO_AdditionChargesTable.AMOUNT_FLD:
						// get AddChargeID
						if (dgrdData[dgrdData.Row, PO_AdditionChargesTable.ADDCHARGEID_FLD].ToString() != string.Empty &&
							(dgrdData[dgrdData.Row, PO_AdditionChargesTable.ADDCHARGEID_FLD] != null))
						{
							// get addition charge vo
							voAddCharge = (MST_AddChargeVO) boPOAddCharge.GetAddCharge(int.Parse(dgrdData[dgrdData.Row, PO_AdditionChargesTable.ADDCHARGEID_FLD].ToString()));
						}
						else
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
							dgrdData.Focus();
							dgrdData.Select();
							dgrdData[dgrdData.Row, dgrdData.Col] = strSelectedValue;
							return;
						}
						//decAmount
						dgrdData[dgrdData.Row, PO_AdditionChargesTable.VATAMOUNT_FLD] = Math.Round(decSelectedValue * voAddCharge.VAT / VAT_NUMBER, DECIMAL_ROUND);
						dgrdData[dgrdData.Row, PO_AdditionChargesTable.TOTALAMOUNT_FLD] = Math.Round(decSelectedValue + decimal.Parse(dgrdData.Columns[PO_AdditionChargesTable.VATAMOUNT_FLD].ToString()), DECIMAL_ROUND);
						// update TotalAmount, TotalVATAmount, TotalNETAmount;
						UpdateTotalAmount();
						break;
					// VAT amount
					case PO_AdditionChargesTable.VATAMOUNT_FLD:
						dgrdData[dgrdData.Row, PO_AdditionChargesTable.TOTALAMOUNT_FLD] = Math.Round(decSelectedValue + decimal.Parse(dgrdData.Columns[PO_AdditionChargesTable.AMOUNT_FLD].ToString()), DECIMAL_ROUND);
						// update TotalAmount, TotalVATAmount, TotalNETAmount;
						UpdateTotalAmount();
						break;
					// total amount
					case PO_AdditionChargesTable.TOTALAMOUNT_FLD:
						// update TotalAmount, TotalVATAmount, TotalNETAmount;
						UpdateTotalAmount();
						break;
				}
			}
			catch (PCSException ex)
			{
				dgrdData[dgrdData.Row, dgrdData.Col] = strSelectedValue;
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
				dgrdData[dgrdData.Row, dgrdData.Col] = strSelectedValue;
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
		private void dgrdData_ButtonClick(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				// if current column is locked then return
				if (dgrdData.Splits[0].DisplayColumns[dgrdData.Col].Locked)
					return;
				FillDataToGrid();
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
		private void dgrdData_BeforeColEdit(object sender, BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit()";
			try
			{
				// get the selected value and store to a variable in order to compare later
				strSelectedValue = dgrdData[dgrdData.Row, e.ColIndex].ToString();
				// now we need to check if current PO Line is approved or not?
				// get PO Detail of current row
				int intPODetailID = 0;
				try
				{
					intPODetailID = int.Parse(dgrdData[dgrdData.Row, PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD].ToString());
				}
				catch{}
				if (intPODetailID <= 0)
					return;
				voPODetail = (PO_PurchaseOrderDetailVO)boPOAddCharge.GetPurchaseOrderDetailVO(intPODetailID);
				// if the PODetail of current row has been approved, then unable to change anything
				if (voPODetail.ApproverID > 0)
				{
					e.Cancel = true;
					// lock all editable column
					dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.UNITPRICE_FLD].Locked = true;
					dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.QUANTITY_FLD].Locked = true;
					dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.AMOUNT_FLD].Locked = true;
					dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.VATAMOUNT_FLD].Locked = true;
					// unable to change PO, Addition Charge, Reason
					dgrdData.Splits[0].DisplayColumns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Button = false;
					dgrdData.Splits[0].DisplayColumns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Locked = true;
					dgrdData.Splits[0].DisplayColumns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Button = false;
					dgrdData.Splits[0].DisplayColumns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Locked = true;
					dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Button = false;
					dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Locked = true;
				}
				else
				{
					// unlock Unit price and quantity
					dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.UNITPRICE_FLD].Locked = false;
					dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.QUANTITY_FLD].Locked = false;
					dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.AMOUNT_FLD].Locked = false;
					dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.VATAMOUNT_FLD].Locked = false;
					// allow to change PO, Addition Charge, Reason
					dgrdData.Splits[0].DisplayColumns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Button = true;
					dgrdData.Splits[0].DisplayColumns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Locked = false;
					dgrdData.Splits[0].DisplayColumns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Button = true;
					dgrdData.Splits[0].DisplayColumns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Locked = false;
					dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Button = true;
					dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Locked = false;
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
		private void dgrdData_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeDelete()";
			try
			{
				// turn of auto increment mode
				dstData.Tables[0].Columns[LINE_COL].AutoIncrement = false;
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
		private void dgrdData_AfterDelete(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterDelete()";
			try
			{
				// allow user to add new
				dgrdData.AllowAddNew = true;
				if (dgrdData.RowCount == 0)
					return;
				// re-assign line value
				int intCount = 0;
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					dgrdData[i, LINE_COL] = ++intCount;
				}
				// update total amount when user delete one line on grid
				UpdateTotalAmount();
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
		private void dgrdData_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.F12:
						if (dgrdData.AllowAddNew)
						{
							// move to last row of grid
							dgrdData.Row = dgrdData.RowCount;
							// set grid to EditActive mode
							dgrdData.EditActive = true;
							// line number
							int intMaxLine = 0;
							if (dgrdData[dgrdData.Row, LINE_COL].ToString() == string.Empty)
							{
								// get max line number
								if (dgrdData.Row > 0)
								{
									intMaxLine = int.Parse(dgrdData[dgrdData.Row - 1, LINE_COL].ToString());
								}
								dgrdData[dgrdData.Row, LINE_COL] = ++intMaxLine;
							}
							dgrdData[dgrdData.Row, PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD] = intPOMasterID;
							decimal decDistributionAmount = 0;
							decimal decSumAmount = CalculateTotalAmount();
							try
							{
								decDistributionAmount = decimal.Parse(txtDistributionAmount.Text.Trim());
								if (decDistributionAmount <= 0)
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_DISTRIBUTE_AMOUNT_MUST_GREATER_THAN_ZERO, MessageBoxIcon.Error);
									txtDistributionAmount.Focus();
									txtDistributionAmount.SelectAll();
									return;
								}
							}
							catch
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxIcon.Error);
								txtDistributionAmount.Focus();
								txtDistributionAmount.SelectAll();
								return;
							}
							if (decDistributionAmount <= decSumAmount)
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_DISTRIBUTE_AMOUNT_MUST_GREATER_THAN_SUM_AMOUNT, MessageBoxIcon.Error);
								txtDistributionAmount.Focus();
								txtDistributionAmount.SelectAll();
								return;
							}
							// amount = distribution amount - sum (amount)
							dgrdData[dgrdData.Row, PO_AdditionChargesTable.AMOUNT_FLD] = decDistributionAmount - decSumAmount;
							// set focus to SO Line
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD]);

							// if total row have data of grid equal to number of sale order detail
							// then disable add new record to grid
							int intTotalRowHaveData = dgrdData.RowCount;
							int intTotalSODetail = tblPODetails.Rows.Count;
							if (intTotalRowHaveData == intTotalSODetail)
							{
								dgrdData.AllowAddNew = false;
							}
						}
						break;
					case Keys.F4:
						dgrdData_ButtonClick(null, null);
						break;
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
		private void dgrdData_RowColChange(object sender, RowColChangeEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_RowColChange()";
			try
			{
				// change row
				if (e.LastRow != dgrdData.Row)
				{
					// get PO Detail of current row
					int intPODetailID = 0;
					try
					{
						intPODetailID = int.Parse(dgrdData[dgrdData.Row, PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD].ToString());
					}
					catch{}
					if (intPODetailID <= 0)
						return;
					voPODetail = (PO_PurchaseOrderDetailVO)boPOAddCharge.GetPurchaseOrderDetailVO(intPODetailID);
					// if the PODetail of current row has been approved, then unable to change anything
					if (voPODetail.ApproverID > 0)
					{
						// lock all editable column
						dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.UNITPRICE_FLD].Locked = true;
						dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.QUANTITY_FLD].Locked = true;
						dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.AMOUNT_FLD].Locked = true;
						dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.VATAMOUNT_FLD].Locked = true;
						// unable to change PO, Addition Charge, Reason
						dgrdData.Splits[0].DisplayColumns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Button = false;
						dgrdData.Splits[0].DisplayColumns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Locked = true;
						dgrdData.Splits[0].DisplayColumns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Button = false;
						dgrdData.Splits[0].DisplayColumns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Locked = true;
						dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Button = false;
						dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Locked = true;
					}
					else
					{
						// unlock Unit price and quantity
						dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.UNITPRICE_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.QUANTITY_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.AMOUNT_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.VATAMOUNT_FLD].Locked = false;
						// allow to change PO, Addition Charge, Reason
						dgrdData.Splits[0].DisplayColumns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Button = true;
						dgrdData.Splits[0].DisplayColumns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Button = true;
						dgrdData.Splits[0].DisplayColumns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Locked = false;
						dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Button = true;
						dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Locked = false;
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
		private void POAdditionCharges_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".POAdditionCharges_Closing()";
			try
			{
				if (this.mFormMode != EnumAction.Default)
				{
					DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
					switch (dlgResult)
					{
						case DialogResult.Yes:
							if (!SaveData())
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
		#endregion

		#region private methods
		private void BindDataToGrid()
		{
			try
			{
				#region store all columns header for localized purpose

				string strLineHeader = dgrdData.Columns[LINE_COL].Caption;
				int intLineWidth = dgrdData.Splits[0].DisplayColumns[LINE_COL].Width;
				string strPOLineHeader = dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Caption;
				int intPOLineWidth = dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Width;
				string strAddChargeHeader = dgrdData.Columns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Caption;
				string strReasonHeader = dgrdData.Columns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Caption;
				string strQuantityHeader = dgrdData.Columns[PO_AdditionChargesTable.QUANTITY_FLD].Caption;
				string strUnitPriceHeader = dgrdData.Columns[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].Caption;
				string strTaxAmountHeader = dgrdData.Columns[PO_AdditionChargesTable.AMOUNT_FLD].Caption;
				string strVATAmountHeader = dgrdData.Columns[PO_AdditionChargesTable.VATAMOUNT_FLD].Caption;
				string strTotalAmountHeader = dgrdData.Columns[PO_AdditionChargesTable.TOTALAMOUNT_FLD].Caption;

				#endregion
				
				// bind data to dgrdData
				dgrdData.DataSource = dstData.Tables[0];

				#region display columns
				foreach (C1DisplayColumn objCol in dgrdData.Splits[0].DisplayColumns)
				{
					objCol.Visible = false;
					objCol.Locked = true;
					objCol.AutoSize();
					// align center heading
					objCol.HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;
				}
				//
				// Line
				//
				dgrdData.Splits[0].DisplayColumns[LINE_COL].Visible = true;
				dgrdData.Splits[0].DisplayColumns[LINE_COL].Width = intLineWidth;
				dgrdData.Columns[LINE_COL].Caption = strLineHeader;
				//dgrdData.Splits[0].DisplayColumns[LINE_COL].HeadingStyle.ForeColor = Color.Maroon;
				//dgrdData.Columns[LINE_COL].Aggregate = AggregateEnum.Count;
				//
				// PO Line
				//
				dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Visible = true;
				dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Width = intPOLineWidth;
				dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Caption = strPOLineHeader;
				dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Editor = txtCellValue;
				dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].HeadingStyle.ForeColor = Color.Maroon;
				dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].Locked = false;
				//
				// Addition Charge
				//
				dgrdData.Splits[0].DisplayColumns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Visible = true;
				dgrdData.Columns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Caption = strAddChargeHeader;
				dgrdData.Columns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Editor = txtCellValue;
				dgrdData.Splits[0].DisplayColumns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].HeadingStyle.ForeColor = Color.Maroon;
				dgrdData.Splits[0].DisplayColumns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD].Locked = false;
				//
				// Reason
				//
				dgrdData.Splits[0].DisplayColumns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Visible = true;
				dgrdData.Columns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Caption = strReasonHeader;
				dgrdData.Columns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Editor = txtCellValue;
				//dgrdData.Splits[0].DisplayColumns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].HeadingStyle.ForeColor = Color.Maroon;
				dgrdData.Splits[0].DisplayColumns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD].Locked = false;
				//
				// Quantity
				//
				dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.QUANTITY_FLD].Visible = true;
				dgrdData.Columns[PO_AdditionChargesTable.QUANTITY_FLD].Caption = strQuantityHeader;
				dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.QUANTITY_FLD].Locked = false;
				dgrdData.Columns[PO_AdditionChargesTable.QUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				//
				// Unit Price
				//
				dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.UNITPRICE_FLD].Visible = true;
				dgrdData.Columns[PO_AdditionChargesTable.UNITPRICE_FLD].Caption = strUnitPriceHeader;
				dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.UNITPRICE_FLD].Locked = false;
				dgrdData.Columns[PO_AdditionChargesTable.UNITPRICE_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				//
				// Tax Amount
				//
				dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.AMOUNT_FLD].Visible = true;
				dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.AMOUNT_FLD].Locked = true;
				dgrdData.Columns[PO_AdditionChargesTable.AMOUNT_FLD].Caption = strTaxAmountHeader;
				dgrdData.Columns[PO_AdditionChargesTable.AMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				//
				// VAT Amount
				//
				dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.VATAMOUNT_FLD].Visible = true;
				dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.VATAMOUNT_FLD].Locked = true;
				dgrdData.Columns[PO_AdditionChargesTable.VATAMOUNT_FLD].Caption = strVATAmountHeader;
				dgrdData.Columns[PO_AdditionChargesTable.VATAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				//
				// Total Amount
				//
				dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.TOTALAMOUNT_FLD].Visible = true;
				dgrdData.Splits[0].DisplayColumns[PO_AdditionChargesTable.TOTALAMOUNT_FLD].Locked = true;
				dgrdData.Columns[PO_AdditionChargesTable.TOTALAMOUNT_FLD].Caption = strTotalAmountHeader;
				dgrdData.Columns[PO_AdditionChargesTable.TOTALAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				//Align center all caption of columns

				#endregion

				// if total row have data of grid equal to number of sale order detail
				// then disable add new record to grid
				int intTotalRowHaveData = dgrdData.RowCount;
				if (intTotalRowHaveData == tblPODetails.Rows.Count)
				{
					dgrdData.AllowAddNew = false;
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
		private decimal CalculateTotalAmount()
		{
            // get rows count of grid
            int intRowsCount = this.dgrdData.RowCount;
            decimal decTotalValue = 0;

            // calculate value
            for (int i = 0; i < intRowsCount; i++)
            {
                try
                {
                    decTotalValue += decimal.Parse(this.dgrdData[i, PO_AdditionChargesTable.TOTALAMOUNT_FLD].ToString());
                }
                catch { }
            }
            return decTotalValue;
		}
		private decimal CalculateTotalVATAmount()
		{
            // get rows count of grid
            int intRowsCount = this.dgrdData.RowCount;
            decimal decTotalValue = 0;

            // calculate value
            for (int i = 0; i < intRowsCount; i++)
            {
                try
                {
                    decTotalValue += decimal.Parse(this.dgrdData[i, PO_AdditionChargesTable.VATAMOUNT_FLD].ToString());
                }
                catch { }
            }
            return decTotalValue;
		}
		private decimal CalculateTotalTaxAmount()
		{
            // get rows count of grid
            int intRowsCount = this.dgrdData.RowCount;
            decimal decTotalValue = 0;

            // calculate value
            for (int i = 0; i < intRowsCount; i++)
            {
                try
                {
                    decTotalValue += decimal.Parse(this.dgrdData[i, PO_AdditionChargesTable.AMOUNT_FLD].ToString());
                }
                catch { }
            }
            return decTotalValue;
		}
		private void UpdateTotalAmount()
		{
			try
			{
				// calculate Total VAT Amount
				decimal decTotalVATAmount = CalculateTotalVATAmount();
				txtTotalVATAmount.Text = decTotalVATAmount.ToString(Constants.DECIMAL_NUMBERFORMAT);
				// calculate Total Amount
				decimal decTotalAmount = CalculateTotalAmount();
				txtTotalAmount.Text = decTotalAmount.ToString(Constants.DECIMAL_NUMBERFORMAT);
				// calculate Total Tax Amount
				decimal decTotalTaxAmount = CalculateTotalTaxAmount();
				txtTotalTaxAmount.Text = decTotalTaxAmount.ToString(Constants.DECIMAL_NUMBERFORMAT);
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
		private void UpdateAmount(string pstrBy, bool pblnForOneRow)
		{
			try
			{
				decimal decAmount = 0;
				decimal decQuantity = 0;
				decimal decUnitPrice = 0;
				decimal decAddChargeVAT = 0;
				decimal decVATAmount = 0;
				decimal decTotalAmount = 0;
				decimal decTotalQuantity = 0;
				decimal decTotalUniPrice = 0;
				decimal decDistributionAmount = 0;
				int intAddChargeID = 0;
				// get distribution amount
				try
				{
					decDistributionAmount = decimal.Parse(txtDistributionAmount.Value.ToString().Trim());
					if (decDistributionAmount <= 0)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_DISTRIBUTE_AMOUNT_MUST_GREATER_THAN_ZERO, MessageBoxIcon.Error);
						txtDistributionAmount.Focus();
						txtDistributionAmount.SelectAll();
						throw new Exception();
					}
				}
				catch
				{
					dgrdData[dgrdData.Row, dgrdData.Col] = strSelectedValue;
					return;
				}
				int intRowsCount = dgrdData.RowCount;
				// we get total quantity, total unit price
				for (int i = 0; i < intRowsCount; i++)
				{
					// get total quantity of all rows
					try
					{
						if (!pblnForOneRow)
							decTotalQuantity += decimal.Parse(dgrdData[i, PO_PurchaseOrderDetailTable.TABLE_NAME + PO_AdditionChargesTable.QUANTITY_FLD].ToString());
						else
							decTotalQuantity += decimal.Parse(dgrdData[i,  PO_AdditionChargesTable.QUANTITY_FLD].ToString());
					}
					catch
					{
					}
					// get total unit price of all rows
					try
					{
						if (!pblnForOneRow)
							decTotalUniPrice += decimal.Parse(dgrdData[i, PO_PurchaseOrderDetailTable.TABLE_NAME + PO_AdditionChargesTable.UNITPRICE_FLD].ToString());
						else
							decTotalUniPrice += decimal.Parse(dgrdData[i, PO_AdditionChargesTable.UNITPRICE_FLD].ToString());
						
					}
					catch
					{
					}
				}
//				if (radByQuantity.Checked && (decTotalQuantity <= 0))
//					return;
//				if (radByPrice.Checked && (decTotalUniPrice <= 0))
//					return;
				// we have total quantity and total unit price
				// now fill value to Amount, VAT Amount, Total Amount cell of grid
				if (pblnForOneRow)
				{
					try
					{
						decQuantity = decimal.Parse(dgrdData[dgrdData.Row, PO_AdditionChargesTable.QUANTITY_FLD].ToString());
						//decQuantity = decimal.Parse(dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.TABLE_NAME + PO_AdditionChargesTable.QUANTITY_FLD].ToString());
					}
					catch
					{
						decQuantity = 0;
					}
					try
					{
						decUnitPrice = decimal.Parse(dgrdData[dgrdData.Row, PO_AdditionChargesTable.UNITPRICE_FLD].ToString());
						//decUnitPrice = decimal.Parse(dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.TABLE_NAME + PO_AdditionChargesTable.UNITPRICE_FLD].ToString());
					}
					catch
					{
						decUnitPrice = 0;
					}
					try
					{
						decAmount = decimal.Parse(dgrdData[dgrdData.Row, PO_AdditionChargesTable.AMOUNT_FLD].ToString());
					}
					catch
					{
						decAmount = 0;
					}
					if(decUnitPrice != 0 && decQuantity != 0)
						decAmount = decUnitPrice * decQuantity;
//					switch (pstrBy)
//					{
//						case BY_PRICE:
//							// Amount = (Unit Price/Total Unit Price) * Distribution Amount)
//							//decAmount = Math.Round((decUnitPrice / decTotalUniPrice) * decDistributionAmount, DECIMAL_ROUND);
//							decAmount = decUnitPrice * decQuantity;
//							break;
//						case BY_QUANTITY:
//							// Amount = (Quantity/Total Quantity) * Distribution Amount)
//							decAmount = Math.Round((decQuantity / decTotalQuantity) * decDistributionAmount, DECIMAL_ROUND);
//							break;
//						case BY_MANUAL:
//							// Amount = Quantity * Unit Price
//							decAmount = decQuantity * decUnitPrice;
//							break;
//					}
					// get AddChargeID
					try
					{
						intAddChargeID = int.Parse(dgrdData[dgrdData.Row, SO_AdditionChargeTable.ADDCHARGEID_FLD].ToString());
					}
					catch{}
					if (intAddChargeID > 0)
					{
						// get addition charge vo
						voAddCharge = (MST_AddChargeVO) boPOAddCharge.GetAddCharge(intAddChargeID);
						decAddChargeVAT = voAddCharge.VAT;
					}
					// get Addition Charge VAT
					decAddChargeVAT = decAddChargeVAT;
					// VAT Amount =  Amount * Addition Charge VAT / 100
					decVATAmount = Math.Round(decAmount * decAddChargeVAT / VAT_NUMBER, DECIMAL_ROUND);
					// Total Amount = Amount + VAT Amount
					decTotalAmount = Math.Round(decAmount + decVATAmount, DECIMAL_ROUND);
					// fill data to cell
					dgrdData[dgrdData.Row, PO_AdditionChargesTable.AMOUNT_FLD] = decAmount;
					dgrdData[dgrdData.Row, PO_AdditionChargesTable.VATAMOUNT_FLD] = decVATAmount;
					dgrdData[dgrdData.Row, PO_AdditionChargesTable.TOTALAMOUNT_FLD] = decTotalAmount;
				}
				else
				{
					if (radByQuantity.Checked && (decTotalQuantity <= 0))
						return;
					if (radByPrice.Checked && (decTotalUniPrice <= 0))
						return;
					for (int i = 0; i < intRowsCount; i++)
					{
						// get PO Detail of current row
						int intPODetailID = 0;
						try
						{
							intPODetailID = int.Parse(dgrdData[i, PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD].ToString());
						}
						catch{}
						if (intPODetailID <= 0)
							continue;
						voPODetail = (PO_PurchaseOrderDetailVO)boPOAddCharge.GetPurchaseOrderDetailVO(intPODetailID);
						// if current line was approved then continue to next line
						if (voPODetail.ApproverID > 0)
							continue;
						try
						{
							//decQuantity = decimal.Parse(dgrdData[i, PO_AdditionChargesTable.QUANTITY_FLD].ToString());
							decQuantity = decimal.Parse(dgrdData[i, PO_PurchaseOrderDetailTable.TABLE_NAME + PO_AdditionChargesTable.QUANTITY_FLD].ToString());
						}
						catch
						{
							decQuantity = 0;
						}
						try
						{
							//decUnitPrice = decimal.Parse(dgrdData[i, PO_AdditionChargesTable.UNITPRICE_FLD].ToString());
							decUnitPrice = decimal.Parse(dgrdData[i, PO_PurchaseOrderDetailTable.TABLE_NAME + PO_AdditionChargesTable.UNITPRICE_FLD].ToString());
						}
						catch
						{
							decUnitPrice = 0;
						}
						switch (pstrBy)
						{
							case BY_PRICE:
								// Amount = (Unit Price/Total Unit Price) * Distribution Amount)
								decAmount = Math.Round((decUnitPrice / decTotalUniPrice) * decDistributionAmount, DECIMAL_ROUND);
								break;
							case BY_QUANTITY:
								// Amount = (Quantity/Total Quantity) * Distribution Amount)
								decAmount = Math.Round((decQuantity / decTotalQuantity) * decDistributionAmount, DECIMAL_ROUND);
								break;
							case BY_MANUAL:
								// Amount = Quantity * Unit Price
								decAmount = decQuantity * decUnitPrice;
								break;
						}
						// get AddChargeID
						try
						{
							intAddChargeID = int.Parse(dgrdData[dgrdData.Row, SO_AdditionChargeTable.ADDCHARGEID_FLD].ToString());
						}
						catch{}
						if (intAddChargeID > 0)
						{
							// get addition charge vo
							voAddCharge = (MST_AddChargeVO) boPOAddCharge.GetAddCharge(intAddChargeID);
							decAddChargeVAT = voAddCharge.VAT;
						}
						// get Addition Charge VAT
						decAddChargeVAT = decAddChargeVAT;
						// VAT Amount =  Amount * Addition Charge VAT / 100
						decVATAmount = Math.Round(decAmount * decAddChargeVAT / VAT_NUMBER, DECIMAL_ROUND);
						// Total Amount = Amount + VAT Amount
						decTotalAmount = Math.Round(decAmount + decVATAmount, DECIMAL_ROUND);
						// fill data to cell
						dgrdData[i, PO_AdditionChargesTable.AMOUNT_FLD] = decAmount;
						dgrdData[i, PO_AdditionChargesTable.VATAMOUNT_FLD] = decVATAmount;
						dgrdData[i, PO_AdditionChargesTable.TOTALAMOUNT_FLD] = decTotalAmount;
					}
				}
				// update TotalAmount, TotalVATAmount, TotalNETAmount;
				UpdateTotalAmount();
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
		private bool SaveData()
		{
			try
			{
				// update data source
				dgrdData.UpdateData();
				// update data base
				boPOAddCharge.UpdateDataSet(dstData);
				// refresh the grid
				dgrdData.Refresh();
				// turn off edit mode
				mFormMode = EnumAction.Default;
				return true;
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
		private void AssignEvent(Control pobjControl)
		{
			foreach (Control objControl in pobjControl.Controls)
			{
				if (objControl.HasChildren)
				{
					AssignEvent(objControl);
				}
				if (((objControl is TextBox) || (objControl is ComboBox) ||
					(objControl is DateTimePicker) || (objControl is C1Combo) ||
					(objControl is C1TextBox) || (objControl is C1DateEdit) ||
					(objControl is C1NumericEdit) || (objControl is C1DropDownControl)) && (!objControl.Equals(txtCellValue)))
				{
					objControl.Enter += new EventHandler(this.OnEnterControl);
					objControl.Leave += new EventHandler(this.OnLeaveControl);
				}
			}
		}
		private void OnEnterControl(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnEnterControl()";
			try
			{
				#region // HACK: DEL dungla 10-14-2005

//				if (sender.GetType().Equals(typeof(TextBox)))
//				{
//					objBackColor = ((TextBox)sender).BackColor;
//					objForeColor = ((TextBox)sender).ForeColor;
//				}
//				else if (sender.GetType().Equals(typeof(C1NumericEdit)))
//				{
//					objBackColor = ((C1NumericEdit)sender).BackColor;
//					objForeColor = ((C1NumericEdit)sender).ForeColor;
//				}

				#endregion // END: DEL dungla 10-14-2005

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
		private void OnLeaveControl(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnLeaveControl()";
			try
			{
				// validate data if has changed
				if (sender.GetType().Equals(typeof(C1NumericEdit)))
				{
					C1NumericEdit txtSender = (C1NumericEdit)sender;
					if (txtSender.Modified)
					{
						if (txtSender.Text.Trim() != string.Empty)
						{
							if (txtSender.Equals(txtDistributionAmount))
							{
								try
								{
									decimal decDistributionAmount = decimal.Parse(txtDistributionAmount.Value.ToString().Trim());
									if (decDistributionAmount <= 0)
									{
										PCSMessageBox.Show(ErrorCode.MESSAGE_DISTRIBUTE_AMOUNT_MUST_GREATER_THAN_ZERO, MessageBoxIcon.Error);
										txtDistributionAmount.Focus();
										txtDistributionAmount.SelectAll();
										return;
									}
								}
								catch
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxButtons.OK, MessageBoxIcon.Error);
									this.txtDistributionAmount.Focus();
									this.txtDistributionAmount.Select();
									return;
								}
							}
						}
					}
				}
			// HACK: SonHT 2005-10-13
			//				FormControlComponents.OnLeaveControl(sender, e, objForeColor, objBackColor);
			// END: SonHT 2005-10-13
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
		private void FillDataToGrid()
		{
			try
			{
				string strColumn = dgrdData.Columns[dgrdData.Col].DataField;
				if (!strColumn.Equals(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD)
					&& !strColumn.Equals(MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD) 
					&& !strColumn.Equals(MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD))
				{
					return;
				}
				string strValue = dgrdData[dgrdData.Row, dgrdData.Col].ToString().Trim();
				DataRowView drvData = null;
				string strExpression = string.Empty;
				int intRowCount = dgrdData.RowCount;
				switch (strColumn)
				{
					// PO line
					case PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD:
						// ignore all po line has been approved
						strExpression = Constants.WHERE_KEYWORD + Constants.WHITE_SPACE
							+ PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + Constants.EQUAL + voPOMaster.PurchaseOrderMasterID.ToString()
							+ Constants.AND + OPEN_QUOTE + PO_PurchaseOrderDetailTable.APPROVERID_FLD + SMALLER_THAN + 1 + OR + PO_PurchaseOrderDetailTable.APPROVERID_FLD + IS_NULL + CLOSE_QUOTE; 
						drvData = FormControlComponents.OpenSearchForm(PO_PurchaseOrderDetailTable.TABLE_NAME, PO_PurchaseOrderDetailTable.LINE_FLD, strValue, strExpression);
						if (drvData != null)
							FillPOData(drvData.Row, intRowCount);
						break;
					// Addition Charge
					case (MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD):
						drvData = FormControlComponents.OpenSearchForm(MST_AddChargeTable.TABLE_NAME, MST_AddChargeTable.CODE_FLD, strValue, null, true);
						if (drvData != null)
							FillAdditionChargeData(drvData.Row);
						break;
					// Reason
					case (MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD):
						drvData = FormControlComponents.OpenSearchForm(MST_ReasonTable.TABLE_NAME, MST_ReasonTable.CODE_FLD, strValue, null, true);
						if (drvData != null)
							FillReasonData(drvData.Row);
						break;
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
		private void FillPOData(DataRow pdrowData, int pintRowCount)
		{
            // get PurchaseOrderDetailVO
            if ((pdrowData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD] != null) && (pdrowData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString() != string.Empty))
                voPODetail = (PO_PurchaseOrderDetailVO)boPOAddCharge.GetPurchaseOrderDetailVO(int.Parse(pdrowData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString()));
            else
                return;
            // set grid to EditActive mode
            dgrdData.EditActive = true;
            // line number
            int intMaxLine = 0;
            if (dgrdData[dgrdData.Row, LINE_COL].ToString() == string.Empty)
            {
                // get max line number
                if (dgrdData.Row > 0)
                {
                    intMaxLine = int.Parse(dgrdData[dgrdData.Row - 1, LINE_COL].ToString());
                }
                dgrdData[dgrdData.Row, LINE_COL] = ++intMaxLine;
            }
            // po line
            dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD] = voPODetail.Line;
            // po detail id
            dgrdData[dgrdData.Row, PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD] = voPODetail.PurchaseOrderDetailID;
            dgrdData.AllowAddNew = pintRowCount != tblPODetails.Rows.Count;
            if (radByPrice.Checked)
                UpdateAmount(BY_PRICE, true);
            else if (radByQuantity.Checked)
                UpdateAmount(BY_QUANTITY, true);
            else if (radByManual.Checked)
                UpdateAmount(BY_MANUAL, true);
		}
		private void FillAdditionChargeData(DataRow pdrowData)
		{
			try
			{
				if ((pdrowData[MST_AddChargeTable.ADDCHARGEID_FLD] != null) && (pdrowData[MST_AddChargeTable.ADDCHARGEID_FLD].ToString() != string.Empty))
					voAddCharge = (MST_AddChargeVO) boPOAddCharge.GetAddCharge(int.Parse(pdrowData[MST_AddChargeTable.ADDCHARGEID_FLD].ToString()));
				else
					return;
				// set grid to EditActive mode
				dgrdData.EditActive = true;
				dgrdData[dgrdData.Row, MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD] = voAddCharge.Code;
				dgrdData[dgrdData.Row, PO_AdditionChargesTable.ADDCHARGEID_FLD] = voAddCharge.AddChargeID;
				// update amount on grid
				if (radByPrice.Checked)
					UpdateAmount(BY_PRICE, true);
				else if (radByQuantity.Checked)
					UpdateAmount(BY_QUANTITY, true);
				else if (radByManual.Checked)
					UpdateAmount(BY_MANUAL, true);
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
		private void FillReasonData(DataRow pdrowData)
		{
			try
			{
				if ((pdrowData[MST_ReasonTable.REASONID_FLD] != null) && (pdrowData[MST_ReasonTable.REASONID_FLD].ToString() != string.Empty))
					voReason = (MST_ReasonVO) boPOAddCharge.GetReasonVO(int.Parse(pdrowData[MST_ReasonTable.REASONID_FLD].ToString()));
				else
					return;
				// set grid to EditActive mode
				dgrdData.EditActive = true;
				dgrdData[dgrdData.Row, MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD] = voReason.Code;
				dgrdData[dgrdData.Row, PO_AdditionChargesTable.REASONID_FLD] = voReason.ReasonID;
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
		#endregion

		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				string strExpression = string.Empty;
				DataRowView drvData = null;
				int intRowCount = dgrdData.RowCount;
				string strValue = dgrdData[dgrdData.Row, dgrdData.Col].ToString().Trim();
				switch (e.Column.DataColumn.DataField)
				{
						// PO line
					case PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD:
						// ignore all po line has been approved
						strExpression = Constants.WHERE_KEYWORD + Constants.WHITE_SPACE
							+ PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + Constants.EQUAL + voPOMaster.PurchaseOrderMasterID.ToString()
							+ Constants.AND + OPEN_QUOTE + PO_PurchaseOrderDetailTable.APPROVERID_FLD + SMALLER_THAN + 1 + OR + PO_PurchaseOrderDetailTable.APPROVERID_FLD + IS_NULL + CLOSE_QUOTE; 
						drvData = FormControlComponents.OpenSearchForm(PO_PurchaseOrderDetailTable.TABLE_NAME, PO_PurchaseOrderDetailTable.LINE_FLD, strValue, strExpression);
						if (drvData != null)
							FillPOData(drvData.Row, intRowCount);
						else
							e.Cancel = true;
						break;
						// Addition Charge
					case (MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD):
						drvData = FormControlComponents.OpenSearchForm(MST_AddChargeTable.TABLE_NAME, MST_AddChargeTable.CODE_FLD, strValue, null, true);
						if (drvData != null)
							FillAdditionChargeData(drvData.Row);
						else
							e.Cancel = true;
						break;
						// Reason
					case (MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD):
						drvData = FormControlComponents.OpenSearchForm(MST_ReasonTable.TABLE_NAME, MST_ReasonTable.CODE_FLD, strValue, null, true);
						if (drvData != null)
							FillReasonData(drvData.Row);
						else
							e.Cancel = true;
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
	}
}
