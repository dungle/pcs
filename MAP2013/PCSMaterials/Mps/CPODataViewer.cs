using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using PCSComMaterials.Plan.BO;
using PCSComMaterials.Plan.DS;
using PCSComProcurement.Purchase.BO;
using PCSComProcurement.Purchase.DS;
using PCSComProduction.DCP.BO;
using PCSComProduction.WorkOrder.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.MasterSetup.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSProcurement.Purchase;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSProduction.WorkOrder;
using PCSComProduction.WorkOrder.BO;
using DCOptionsBO = PCSComProduction.DCP.BO.DCOptionsBO;
using PCSComUtils.DataContext;
using System.Linq;

namespace PCSMaterials.Mps
{
	/// <summary>
	/// Summary description for CPODataViewer.
	/// </summary>
	public class CPODataViewer : System.Windows.Forms.Form
	{
		#region Declaration
		
		#region Auto Generated

		private System.Windows.Forms.TextBox txtRevision;
		private System.Windows.Forms.Label lblRevision;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.CheckBox chkSelectAll;
		private System.Windows.Forms.TextBox txtCycle;
		private System.Windows.Forms.Button btnCycleSearch;
		private System.Windows.Forms.Label lblPlanType;
		private System.Windows.Forms.TextBox txtPartNumber;
		private System.Windows.Forms.Label lblViewType;
		private System.Windows.Forms.Label lblCycle;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.Label lblPartNumer;
		private System.Windows.Forms.Button btnPartNumberSearch;
		private C1.Win.C1Input.C1DateEdit dtmToDueDate;
		private C1.Win.C1Input.C1DateEdit dtmFromDueDate;
		private System.Windows.Forms.Label lblToDueDate;
		private System.Windows.Forms.Label lblFromDueDate;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label lblMasLoc;
		private System.Windows.Forms.TextBox txtMasLoc;
		private System.Windows.Forms.Button btnMasLocSearch;
		private System.Windows.Forms.GroupBox GroupBox;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.TextBox txtPartName;
		private System.Windows.Forms.Label lblPartName;
		private System.Windows.Forms.Button btnPartNameSearch;
		private System.Windows.Forms.ComboBox cboViewType;
		private System.Windows.Forms.ComboBox cboPlanType;
		private System.Windows.Forms.Button btnCategorySearch;
		private System.Windows.Forms.TextBox txtCategory;
		private System.Windows.Forms.Button btnNewPOConvert;
		private System.Windows.Forms.Button btnExistingPOConvert;
		private System.Windows.Forms.Button btnExistingWOConvert;
		private System.Windows.Forms.Button btnNewWOConvert;
		private C1.Win.C1List.C1Combo cboCCN;		
		private C1.Win.C1Input.C1DateEdit dtmDate;
		private C1.Win.C1Input.C1NumericEdit numQuantity;
		private System.Windows.Forms.Label lblProductionLine;
		private System.Windows.Forms.TextBox txtProductionLine;
		private System.Windows.Forms.Button btnProductionLine;
		private System.Windows.Forms.Label lblDCP;
		private System.Windows.Forms.Button btnPrint;	
		private System.Windows.Forms.TextBox txtVendor;
		private System.Windows.Forms.Label lblVendor;
		private System.Windows.Forms.Button btnVendorSearch;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		#endregion Auto Generated

		#region Constants
		
		private const string THIS = "PCSMaterials.Mps.CPODataViewer";
		private const string ZERO_STRING = "0";		
		private const string VIEW_PRODUCTINFOR = "V_ProductInWorkCenter";
		#endregion Constants

		#region Private Variables

		private UtilsBO boUtils = new UtilsBO();
		private PurchaseOrderBO boPurchaseOrder = new PurchaseOrderBO();
		private WorkOrderBO boWorkOrder = new WorkOrderBO();
		private int intToPOorToWO = 0;
		
		CPODataViewerBO boCPODataViewer = new CPODataViewerBO();
		private DataTable dtbGridLayOut;
		private DataTable dtbCPODetail;
		private bool blnDataIsValid = false;

			

		MessageBoxFormForItems frmMessageBoxForm;
		DataTable dtbListErrorItem;
		const string REASON_FLD = "Reason";
		bool blnConvertPOSuccess = false;
		private System.Windows.Forms.Label lblNumberOfRows;
		private C1.Win.C1Input.C1NumericEdit txtNumRows;		
		bool blnConvertWOSuccess = false;
		string strMasterIDToUpdate = "0";
		string strCPOIDToDelete = "0";
		//string strMasterIDToUpdate = string.Empty;
		ArrayList arrMasterIDToUpdate = new ArrayList();
		DataTable dtbVendor = new DataTable(MST_PartyTable.TABLE_NAME);
		#endregion	

		#endregion Declaration

		#region Constructor, Destructor

		public CPODataViewer()
		{
			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CPODataViewer));
			this.GroupBox = new System.Windows.Forms.GroupBox();
			this.txtVendor = new System.Windows.Forms.TextBox();
			this.btnVendorSearch = new System.Windows.Forms.Button();
			this.lblVendor = new System.Windows.Forms.Label();
			this.txtPartName = new System.Windows.Forms.TextBox();
			this.txtPartNumber = new System.Windows.Forms.TextBox();
			this.txtCategory = new System.Windows.Forms.TextBox();
			this.txtRevision = new System.Windows.Forms.TextBox();
			this.lblDCP = new System.Windows.Forms.Label();
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.btnProductionLine = new System.Windows.Forms.Button();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.btnCategorySearch = new System.Windows.Forms.Button();
			this.btnMasLocSearch = new System.Windows.Forms.Button();
			this.txtMasLoc = new System.Windows.Forms.TextBox();
			this.btnCycleSearch = new System.Windows.Forms.Button();
			this.txtCycle = new System.Windows.Forms.TextBox();
			this.cboViewType = new System.Windows.Forms.ComboBox();
			this.lblViewType = new System.Windows.Forms.Label();
			this.dtmToDueDate = new C1.Win.C1Input.C1DateEdit();
			this.dtmFromDueDate = new C1.Win.C1Input.C1DateEdit();
			this.lblMasLoc = new System.Windows.Forms.Label();
			this.cboPlanType = new System.Windows.Forms.ComboBox();
			this.lblPlanType = new System.Windows.Forms.Label();
			this.lblCycle = new System.Windows.Forms.Label();
			this.lblCCN = new System.Windows.Forms.Label();
			this.btnSearch = new System.Windows.Forms.Button();
			this.btnPartNumberSearch = new System.Windows.Forms.Button();
			this.btnPartNameSearch = new System.Windows.Forms.Button();
			this.lblRevision = new System.Windows.Forms.Label();
			this.lblToDueDate = new System.Windows.Forms.Label();
			this.lblPartNumer = new System.Windows.Forms.Label();
			this.lblCategory = new System.Windows.Forms.Label();
			this.lblPartName = new System.Windows.Forms.Label();
			this.lblFromDueDate = new System.Windows.Forms.Label();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnNewPOConvert = new System.Windows.Forms.Button();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.btnNewWOConvert = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnExistingPOConvert = new System.Windows.Forms.Button();
			this.btnExistingWOConvert = new System.Windows.Forms.Button();
			this.dtmDate = new C1.Win.C1Input.C1DateEdit();
			this.numQuantity = new C1.Win.C1Input.C1NumericEdit();
			this.btnPrint = new System.Windows.Forms.Button();
			this.lblNumberOfRows = new System.Windows.Forms.Label();
			this.txtNumRows = new C1.Win.C1Input.C1NumericEdit();
			this.GroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDueDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDueDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNumRows)).BeginInit();
			this.SuspendLayout();
			// 
			// GroupBox
			// 
			this.GroupBox.AccessibleDescription = resources.GetString("GroupBox.AccessibleDescription");
			this.GroupBox.AccessibleName = resources.GetString("GroupBox.AccessibleName");
			this.GroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("GroupBox.Anchor")));
			this.GroupBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GroupBox.BackgroundImage")));
			this.GroupBox.Controls.Add(this.txtVendor);
			this.GroupBox.Controls.Add(this.btnVendorSearch);
			this.GroupBox.Controls.Add(this.lblVendor);
			this.GroupBox.Controls.Add(this.txtPartName);
			this.GroupBox.Controls.Add(this.txtPartNumber);
			this.GroupBox.Controls.Add(this.txtCategory);
			this.GroupBox.Controls.Add(this.txtRevision);
			this.GroupBox.Controls.Add(this.lblDCP);
			this.GroupBox.Controls.Add(this.txtProductionLine);
			this.GroupBox.Controls.Add(this.lblProductionLine);
			this.GroupBox.Controls.Add(this.btnProductionLine);
			this.GroupBox.Controls.Add(this.cboCCN);
			this.GroupBox.Controls.Add(this.btnCategorySearch);
			this.GroupBox.Controls.Add(this.btnMasLocSearch);
			this.GroupBox.Controls.Add(this.txtMasLoc);
			this.GroupBox.Controls.Add(this.btnCycleSearch);
			this.GroupBox.Controls.Add(this.txtCycle);
			this.GroupBox.Controls.Add(this.cboViewType);
			this.GroupBox.Controls.Add(this.lblViewType);
			this.GroupBox.Controls.Add(this.dtmToDueDate);
			this.GroupBox.Controls.Add(this.dtmFromDueDate);
			this.GroupBox.Controls.Add(this.lblMasLoc);
			this.GroupBox.Controls.Add(this.cboPlanType);
			this.GroupBox.Controls.Add(this.lblPlanType);
			this.GroupBox.Controls.Add(this.lblCycle);
			this.GroupBox.Controls.Add(this.lblCCN);
			this.GroupBox.Controls.Add(this.btnSearch);
			this.GroupBox.Controls.Add(this.btnPartNumberSearch);
			this.GroupBox.Controls.Add(this.btnPartNameSearch);
			this.GroupBox.Controls.Add(this.lblRevision);
			this.GroupBox.Controls.Add(this.lblToDueDate);
			this.GroupBox.Controls.Add(this.lblPartNumer);
			this.GroupBox.Controls.Add(this.lblCategory);
			this.GroupBox.Controls.Add(this.lblPartName);
			this.GroupBox.Controls.Add(this.lblFromDueDate);
			this.GroupBox.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("GroupBox.Dock")));
			this.GroupBox.Enabled = ((bool)(resources.GetObject("GroupBox.Enabled")));
			this.GroupBox.Font = ((System.Drawing.Font)(resources.GetObject("GroupBox.Font")));
			this.GroupBox.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("GroupBox.ImeMode")));
			this.GroupBox.Location = ((System.Drawing.Point)(resources.GetObject("GroupBox.Location")));
			this.GroupBox.Name = "GroupBox";
			this.GroupBox.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("GroupBox.RightToLeft")));
			this.GroupBox.Size = ((System.Drawing.Size)(resources.GetObject("GroupBox.Size")));
			this.GroupBox.TabIndex = ((int)(resources.GetObject("GroupBox.TabIndex")));
			this.GroupBox.TabStop = false;
			this.GroupBox.Text = resources.GetString("GroupBox.Text");
			this.GroupBox.Visible = ((bool)(resources.GetObject("GroupBox.Visible")));
			this.GroupBox.Enter += new System.EventHandler(this.GroupBox_Enter);
			// 
			// txtVendor
			// 
			this.txtVendor.AccessibleDescription = resources.GetString("txtVendor.AccessibleDescription");
			this.txtVendor.AccessibleName = resources.GetString("txtVendor.AccessibleName");
			this.txtVendor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtVendor.Anchor")));
			this.txtVendor.AutoSize = ((bool)(resources.GetObject("txtVendor.AutoSize")));
			this.txtVendor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtVendor.BackgroundImage")));
			this.txtVendor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtVendor.Dock")));
			this.txtVendor.Enabled = ((bool)(resources.GetObject("txtVendor.Enabled")));
			this.txtVendor.Font = ((System.Drawing.Font)(resources.GetObject("txtVendor.Font")));
			this.txtVendor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtVendor.ImeMode")));
			this.txtVendor.Location = ((System.Drawing.Point)(resources.GetObject("txtVendor.Location")));
			this.txtVendor.MaxLength = ((int)(resources.GetObject("txtVendor.MaxLength")));
			this.txtVendor.Multiline = ((bool)(resources.GetObject("txtVendor.Multiline")));
			this.txtVendor.Name = "txtVendor";
			this.txtVendor.PasswordChar = ((char)(resources.GetObject("txtVendor.PasswordChar")));
			this.txtVendor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtVendor.RightToLeft")));
			this.txtVendor.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtVendor.ScrollBars")));
			this.txtVendor.Size = ((System.Drawing.Size)(resources.GetObject("txtVendor.Size")));
			this.txtVendor.TabIndex = ((int)(resources.GetObject("txtVendor.TabIndex")));
			this.txtVendor.Text = resources.GetString("txtVendor.Text");
			this.txtVendor.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtVendor.TextAlign")));
			this.txtVendor.Visible = ((bool)(resources.GetObject("txtVendor.Visible")));
			this.txtVendor.WordWrap = ((bool)(resources.GetObject("txtVendor.WordWrap")));
			this.txtVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendor_KeyDown);
			this.txtVendor.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendor_Validating);
			// 
			// btnVendorSearch
			// 
			this.btnVendorSearch.AccessibleDescription = resources.GetString("btnVendorSearch.AccessibleDescription");
			this.btnVendorSearch.AccessibleName = resources.GetString("btnVendorSearch.AccessibleName");
			this.btnVendorSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnVendorSearch.Anchor")));
			this.btnVendorSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVendorSearch.BackgroundImage")));
			this.btnVendorSearch.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnVendorSearch.Dock")));
			this.btnVendorSearch.Enabled = ((bool)(resources.GetObject("btnVendorSearch.Enabled")));
			this.btnVendorSearch.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnVendorSearch.FlatStyle")));
			this.btnVendorSearch.Font = ((System.Drawing.Font)(resources.GetObject("btnVendorSearch.Font")));
			this.btnVendorSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnVendorSearch.Image")));
			this.btnVendorSearch.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnVendorSearch.ImageAlign")));
			this.btnVendorSearch.ImageIndex = ((int)(resources.GetObject("btnVendorSearch.ImageIndex")));
			this.btnVendorSearch.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnVendorSearch.ImeMode")));
			this.btnVendorSearch.Location = ((System.Drawing.Point)(resources.GetObject("btnVendorSearch.Location")));
			this.btnVendorSearch.Name = "btnVendorSearch";
			this.btnVendorSearch.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnVendorSearch.RightToLeft")));
			this.btnVendorSearch.Size = ((System.Drawing.Size)(resources.GetObject("btnVendorSearch.Size")));
			this.btnVendorSearch.TabIndex = ((int)(resources.GetObject("btnVendorSearch.TabIndex")));
			this.btnVendorSearch.Text = resources.GetString("btnVendorSearch.Text");
			this.btnVendorSearch.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnVendorSearch.TextAlign")));
			this.btnVendorSearch.Visible = ((bool)(resources.GetObject("btnVendorSearch.Visible")));
			this.btnVendorSearch.Click += new System.EventHandler(this.btnVendorSearch_Click);
			// 
			// lblVendor
			// 
			this.lblVendor.AccessibleDescription = resources.GetString("lblVendor.AccessibleDescription");
			this.lblVendor.AccessibleName = resources.GetString("lblVendor.AccessibleName");
			this.lblVendor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblVendor.Anchor")));
			this.lblVendor.AutoSize = ((bool)(resources.GetObject("lblVendor.AutoSize")));
			this.lblVendor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblVendor.Dock")));
			this.lblVendor.Enabled = ((bool)(resources.GetObject("lblVendor.Enabled")));
			this.lblVendor.Font = ((System.Drawing.Font)(resources.GetObject("lblVendor.Font")));
			this.lblVendor.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVendor.Image = ((System.Drawing.Image)(resources.GetObject("lblVendor.Image")));
			this.lblVendor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVendor.ImageAlign")));
			this.lblVendor.ImageIndex = ((int)(resources.GetObject("lblVendor.ImageIndex")));
			this.lblVendor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblVendor.ImeMode")));
			this.lblVendor.Location = ((System.Drawing.Point)(resources.GetObject("lblVendor.Location")));
			this.lblVendor.Name = "lblVendor";
			this.lblVendor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblVendor.RightToLeft")));
			this.lblVendor.Size = ((System.Drawing.Size)(resources.GetObject("lblVendor.Size")));
			this.lblVendor.TabIndex = ((int)(resources.GetObject("lblVendor.TabIndex")));
			this.lblVendor.Text = resources.GetString("lblVendor.Text");
			this.lblVendor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblVendor.TextAlign")));
			this.lblVendor.Visible = ((bool)(resources.GetObject("lblVendor.Visible")));
			// 
			// txtPartName
			// 
			this.txtPartName.AccessibleDescription = resources.GetString("txtPartName.AccessibleDescription");
			this.txtPartName.AccessibleName = resources.GetString("txtPartName.AccessibleName");
			this.txtPartName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPartName.Anchor")));
			this.txtPartName.AutoSize = ((bool)(resources.GetObject("txtPartName.AutoSize")));
			this.txtPartName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPartName.BackgroundImage")));
			this.txtPartName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPartName.Dock")));
			this.txtPartName.Enabled = ((bool)(resources.GetObject("txtPartName.Enabled")));
			this.txtPartName.Font = ((System.Drawing.Font)(resources.GetObject("txtPartName.Font")));
			this.txtPartName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPartName.ImeMode")));
			this.txtPartName.Location = ((System.Drawing.Point)(resources.GetObject("txtPartName.Location")));
			this.txtPartName.MaxLength = ((int)(resources.GetObject("txtPartName.MaxLength")));
			this.txtPartName.Multiline = ((bool)(resources.GetObject("txtPartName.Multiline")));
			this.txtPartName.Name = "txtPartName";
			this.txtPartName.PasswordChar = ((char)(resources.GetObject("txtPartName.PasswordChar")));
			this.txtPartName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPartName.RightToLeft")));
			this.txtPartName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPartName.ScrollBars")));
			this.txtPartName.Size = ((System.Drawing.Size)(resources.GetObject("txtPartName.Size")));
			this.txtPartName.TabIndex = ((int)(resources.GetObject("txtPartName.TabIndex")));
			this.txtPartName.Text = resources.GetString("txtPartName.Text");
			this.txtPartName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPartName.TextAlign")));
			this.txtPartName.Visible = ((bool)(resources.GetObject("txtPartName.Visible")));
			this.txtPartName.WordWrap = ((bool)(resources.GetObject("txtPartName.WordWrap")));
			this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
			this.txtPartName.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartName_Validating);
			// 
			// txtPartNumber
			// 
			this.txtPartNumber.AccessibleDescription = resources.GetString("txtPartNumber.AccessibleDescription");
			this.txtPartNumber.AccessibleName = resources.GetString("txtPartNumber.AccessibleName");
			this.txtPartNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPartNumber.Anchor")));
			this.txtPartNumber.AutoSize = ((bool)(resources.GetObject("txtPartNumber.AutoSize")));
			this.txtPartNumber.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPartNumber.BackgroundImage")));
			this.txtPartNumber.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPartNumber.Dock")));
			this.txtPartNumber.Enabled = ((bool)(resources.GetObject("txtPartNumber.Enabled")));
			this.txtPartNumber.Font = ((System.Drawing.Font)(resources.GetObject("txtPartNumber.Font")));
			this.txtPartNumber.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPartNumber.ImeMode")));
			this.txtPartNumber.Location = ((System.Drawing.Point)(resources.GetObject("txtPartNumber.Location")));
			this.txtPartNumber.MaxLength = ((int)(resources.GetObject("txtPartNumber.MaxLength")));
			this.txtPartNumber.Multiline = ((bool)(resources.GetObject("txtPartNumber.Multiline")));
			this.txtPartNumber.Name = "txtPartNumber";
			this.txtPartNumber.PasswordChar = ((char)(resources.GetObject("txtPartNumber.PasswordChar")));
			this.txtPartNumber.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPartNumber.RightToLeft")));
			this.txtPartNumber.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPartNumber.ScrollBars")));
			this.txtPartNumber.Size = ((System.Drawing.Size)(resources.GetObject("txtPartNumber.Size")));
			this.txtPartNumber.TabIndex = ((int)(resources.GetObject("txtPartNumber.TabIndex")));
			this.txtPartNumber.Text = resources.GetString("txtPartNumber.Text");
			this.txtPartNumber.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPartNumber.TextAlign")));
			this.txtPartNumber.Visible = ((bool)(resources.GetObject("txtPartNumber.Visible")));
			this.txtPartNumber.WordWrap = ((bool)(resources.GetObject("txtPartNumber.WordWrap")));
			this.txtPartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNumber_KeyDown);
			this.txtPartNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartNumber_Validating);
			// 
			// txtCategory
			// 
			this.txtCategory.AccessibleDescription = resources.GetString("txtCategory.AccessibleDescription");
			this.txtCategory.AccessibleName = resources.GetString("txtCategory.AccessibleName");
			this.txtCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCategory.Anchor")));
			this.txtCategory.AutoSize = ((bool)(resources.GetObject("txtCategory.AutoSize")));
			this.txtCategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCategory.BackgroundImage")));
			this.txtCategory.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCategory.Dock")));
			this.txtCategory.Enabled = ((bool)(resources.GetObject("txtCategory.Enabled")));
			this.txtCategory.Font = ((System.Drawing.Font)(resources.GetObject("txtCategory.Font")));
			this.txtCategory.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCategory.ImeMode")));
			this.txtCategory.Location = ((System.Drawing.Point)(resources.GetObject("txtCategory.Location")));
			this.txtCategory.MaxLength = ((int)(resources.GetObject("txtCategory.MaxLength")));
			this.txtCategory.Multiline = ((bool)(resources.GetObject("txtCategory.Multiline")));
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.PasswordChar = ((char)(resources.GetObject("txtCategory.PasswordChar")));
			this.txtCategory.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCategory.RightToLeft")));
			this.txtCategory.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCategory.ScrollBars")));
			this.txtCategory.Size = ((System.Drawing.Size)(resources.GetObject("txtCategory.Size")));
			this.txtCategory.TabIndex = ((int)(resources.GetObject("txtCategory.TabIndex")));
			this.txtCategory.Text = resources.GetString("txtCategory.Text");
			this.txtCategory.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCategory.TextAlign")));
			this.txtCategory.Visible = ((bool)(resources.GetObject("txtCategory.Visible")));
			this.txtCategory.WordWrap = ((bool)(resources.GetObject("txtCategory.WordWrap")));
			this.txtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategory_KeyDown);
			this.txtCategory.Validating += new System.ComponentModel.CancelEventHandler(this.txtCategory_Validating);
			// 
			// txtRevision
			// 
			this.txtRevision.AccessibleDescription = resources.GetString("txtRevision.AccessibleDescription");
			this.txtRevision.AccessibleName = resources.GetString("txtRevision.AccessibleName");
			this.txtRevision.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtRevision.Anchor")));
			this.txtRevision.AutoSize = ((bool)(resources.GetObject("txtRevision.AutoSize")));
			this.txtRevision.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtRevision.BackgroundImage")));
			this.txtRevision.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtRevision.Dock")));
			this.txtRevision.Enabled = ((bool)(resources.GetObject("txtRevision.Enabled")));
			this.txtRevision.Font = ((System.Drawing.Font)(resources.GetObject("txtRevision.Font")));
			this.txtRevision.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtRevision.ImeMode")));
			this.txtRevision.Location = ((System.Drawing.Point)(resources.GetObject("txtRevision.Location")));
			this.txtRevision.MaxLength = ((int)(resources.GetObject("txtRevision.MaxLength")));
			this.txtRevision.Multiline = ((bool)(resources.GetObject("txtRevision.Multiline")));
			this.txtRevision.Name = "txtRevision";
			this.txtRevision.PasswordChar = ((char)(resources.GetObject("txtRevision.PasswordChar")));
			this.txtRevision.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtRevision.RightToLeft")));
			this.txtRevision.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtRevision.ScrollBars")));
			this.txtRevision.Size = ((System.Drawing.Size)(resources.GetObject("txtRevision.Size")));
			this.txtRevision.TabIndex = ((int)(resources.GetObject("txtRevision.TabIndex")));
			this.txtRevision.Text = resources.GetString("txtRevision.Text");
			this.txtRevision.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtRevision.TextAlign")));
			this.txtRevision.Visible = ((bool)(resources.GetObject("txtRevision.Visible")));
			this.txtRevision.WordWrap = ((bool)(resources.GetObject("txtRevision.WordWrap")));
			// 
			// lblDCP
			// 
			this.lblDCP.AccessibleDescription = resources.GetString("lblDCP.AccessibleDescription");
			this.lblDCP.AccessibleName = resources.GetString("lblDCP.AccessibleName");
			this.lblDCP.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblDCP.Anchor")));
			this.lblDCP.AutoSize = ((bool)(resources.GetObject("lblDCP.AutoSize")));
			this.lblDCP.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblDCP.Dock")));
			this.lblDCP.Enabled = ((bool)(resources.GetObject("lblDCP.Enabled")));
			this.lblDCP.Font = ((System.Drawing.Font)(resources.GetObject("lblDCP.Font")));
			this.lblDCP.Image = ((System.Drawing.Image)(resources.GetObject("lblDCP.Image")));
			this.lblDCP.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDCP.ImageAlign")));
			this.lblDCP.ImageIndex = ((int)(resources.GetObject("lblDCP.ImageIndex")));
			this.lblDCP.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblDCP.ImeMode")));
			this.lblDCP.Location = ((System.Drawing.Point)(resources.GetObject("lblDCP.Location")));
			this.lblDCP.Name = "lblDCP";
			this.lblDCP.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblDCP.RightToLeft")));
			this.lblDCP.Size = ((System.Drawing.Size)(resources.GetObject("lblDCP.Size")));
			this.lblDCP.TabIndex = ((int)(resources.GetObject("lblDCP.TabIndex")));
			this.lblDCP.Text = resources.GetString("lblDCP.Text");
			this.lblDCP.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblDCP.TextAlign")));
			this.lblDCP.Visible = ((bool)(resources.GetObject("lblDCP.Visible")));
			// 
			// txtProductionLine
			// 
			this.txtProductionLine.AccessibleDescription = resources.GetString("txtProductionLine.AccessibleDescription");
			this.txtProductionLine.AccessibleName = resources.GetString("txtProductionLine.AccessibleName");
			this.txtProductionLine.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtProductionLine.Anchor")));
			this.txtProductionLine.AutoSize = ((bool)(resources.GetObject("txtProductionLine.AutoSize")));
			this.txtProductionLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtProductionLine.BackgroundImage")));
			this.txtProductionLine.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtProductionLine.Dock")));
			this.txtProductionLine.Enabled = ((bool)(resources.GetObject("txtProductionLine.Enabled")));
			this.txtProductionLine.Font = ((System.Drawing.Font)(resources.GetObject("txtProductionLine.Font")));
			this.txtProductionLine.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtProductionLine.ImeMode")));
			this.txtProductionLine.Location = ((System.Drawing.Point)(resources.GetObject("txtProductionLine.Location")));
			this.txtProductionLine.MaxLength = ((int)(resources.GetObject("txtProductionLine.MaxLength")));
			this.txtProductionLine.Multiline = ((bool)(resources.GetObject("txtProductionLine.Multiline")));
			this.txtProductionLine.Name = "txtProductionLine";
			this.txtProductionLine.PasswordChar = ((char)(resources.GetObject("txtProductionLine.PasswordChar")));
			this.txtProductionLine.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtProductionLine.RightToLeft")));
			this.txtProductionLine.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtProductionLine.ScrollBars")));
			this.txtProductionLine.Size = ((System.Drawing.Size)(resources.GetObject("txtProductionLine.Size")));
			this.txtProductionLine.TabIndex = ((int)(resources.GetObject("txtProductionLine.TabIndex")));
			this.txtProductionLine.Text = resources.GetString("txtProductionLine.Text");
			this.txtProductionLine.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtProductionLine.TextAlign")));
			this.txtProductionLine.Visible = ((bool)(resources.GetObject("txtProductionLine.Visible")));
			this.txtProductionLine.WordWrap = ((bool)(resources.GetObject("txtProductionLine.WordWrap")));
			this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
			// 
			// lblProductionLine
			// 
			this.lblProductionLine.AccessibleDescription = resources.GetString("lblProductionLine.AccessibleDescription");
			this.lblProductionLine.AccessibleName = resources.GetString("lblProductionLine.AccessibleName");
			this.lblProductionLine.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblProductionLine.Anchor")));
			this.lblProductionLine.AutoSize = ((bool)(resources.GetObject("lblProductionLine.AutoSize")));
			this.lblProductionLine.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblProductionLine.Dock")));
			this.lblProductionLine.Enabled = ((bool)(resources.GetObject("lblProductionLine.Enabled")));
			this.lblProductionLine.Font = ((System.Drawing.Font)(resources.GetObject("lblProductionLine.Font")));
			this.lblProductionLine.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblProductionLine.Image = ((System.Drawing.Image)(resources.GetObject("lblProductionLine.Image")));
			this.lblProductionLine.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblProductionLine.ImageAlign")));
			this.lblProductionLine.ImageIndex = ((int)(resources.GetObject("lblProductionLine.ImageIndex")));
			this.lblProductionLine.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblProductionLine.ImeMode")));
			this.lblProductionLine.Location = ((System.Drawing.Point)(resources.GetObject("lblProductionLine.Location")));
			this.lblProductionLine.Name = "lblProductionLine";
			this.lblProductionLine.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblProductionLine.RightToLeft")));
			this.lblProductionLine.Size = ((System.Drawing.Size)(resources.GetObject("lblProductionLine.Size")));
			this.lblProductionLine.TabIndex = ((int)(resources.GetObject("lblProductionLine.TabIndex")));
			this.lblProductionLine.Text = resources.GetString("lblProductionLine.Text");
			this.lblProductionLine.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblProductionLine.TextAlign")));
			this.lblProductionLine.Visible = ((bool)(resources.GetObject("lblProductionLine.Visible")));
			// 
			// btnProductionLine
			// 
			this.btnProductionLine.AccessibleDescription = resources.GetString("btnProductionLine.AccessibleDescription");
			this.btnProductionLine.AccessibleName = resources.GetString("btnProductionLine.AccessibleName");
			this.btnProductionLine.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnProductionLine.Anchor")));
			this.btnProductionLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProductionLine.BackgroundImage")));
			this.btnProductionLine.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnProductionLine.Dock")));
			this.btnProductionLine.Enabled = ((bool)(resources.GetObject("btnProductionLine.Enabled")));
			this.btnProductionLine.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnProductionLine.FlatStyle")));
			this.btnProductionLine.Font = ((System.Drawing.Font)(resources.GetObject("btnProductionLine.Font")));
			this.btnProductionLine.Image = ((System.Drawing.Image)(resources.GetObject("btnProductionLine.Image")));
			this.btnProductionLine.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProductionLine.ImageAlign")));
			this.btnProductionLine.ImageIndex = ((int)(resources.GetObject("btnProductionLine.ImageIndex")));
			this.btnProductionLine.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnProductionLine.ImeMode")));
			this.btnProductionLine.Location = ((System.Drawing.Point)(resources.GetObject("btnProductionLine.Location")));
			this.btnProductionLine.Name = "btnProductionLine";
			this.btnProductionLine.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnProductionLine.RightToLeft")));
			this.btnProductionLine.Size = ((System.Drawing.Size)(resources.GetObject("btnProductionLine.Size")));
			this.btnProductionLine.TabIndex = ((int)(resources.GetObject("btnProductionLine.TabIndex")));
			this.btnProductionLine.Text = resources.GetString("btnProductionLine.Text");
			this.btnProductionLine.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnProductionLine.TextAlign")));
			this.btnProductionLine.Visible = ((bool)(resources.GetObject("btnProductionLine.Visible")));
			this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
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
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.Enabled = ((bool)(resources.GetObject("cboCCN.Enabled")));
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
			this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
			this.cboCCN.Leave += new System.EventHandler(this.OnLeaveControl);
			this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
				"ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:N" +
				"ear;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Cente" +
				"r;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Sty" +
				"le10{}Style11{}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
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
				"th>17</DefaultRecSelWidth></Blob>";
			// 
			// btnCategorySearch
			// 
			this.btnCategorySearch.AccessibleDescription = resources.GetString("btnCategorySearch.AccessibleDescription");
			this.btnCategorySearch.AccessibleName = resources.GetString("btnCategorySearch.AccessibleName");
			this.btnCategorySearch.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCategorySearch.Anchor")));
			this.btnCategorySearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCategorySearch.BackgroundImage")));
			this.btnCategorySearch.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCategorySearch.Dock")));
			this.btnCategorySearch.Enabled = ((bool)(resources.GetObject("btnCategorySearch.Enabled")));
			this.btnCategorySearch.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCategorySearch.FlatStyle")));
			this.btnCategorySearch.Font = ((System.Drawing.Font)(resources.GetObject("btnCategorySearch.Font")));
			this.btnCategorySearch.Image = ((System.Drawing.Image)(resources.GetObject("btnCategorySearch.Image")));
			this.btnCategorySearch.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCategorySearch.ImageAlign")));
			this.btnCategorySearch.ImageIndex = ((int)(resources.GetObject("btnCategorySearch.ImageIndex")));
			this.btnCategorySearch.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCategorySearch.ImeMode")));
			this.btnCategorySearch.Location = ((System.Drawing.Point)(resources.GetObject("btnCategorySearch.Location")));
			this.btnCategorySearch.Name = "btnCategorySearch";
			this.btnCategorySearch.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCategorySearch.RightToLeft")));
			this.btnCategorySearch.Size = ((System.Drawing.Size)(resources.GetObject("btnCategorySearch.Size")));
			this.btnCategorySearch.TabIndex = ((int)(resources.GetObject("btnCategorySearch.TabIndex")));
			this.btnCategorySearch.Text = resources.GetString("btnCategorySearch.Text");
			this.btnCategorySearch.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCategorySearch.TextAlign")));
			this.btnCategorySearch.Visible = ((bool)(resources.GetObject("btnCategorySearch.Visible")));
			this.btnCategorySearch.Click += new System.EventHandler(this.btnCategorySearch_Click);
			// 
			// btnMasLocSearch
			// 
			this.btnMasLocSearch.AccessibleDescription = resources.GetString("btnMasLocSearch.AccessibleDescription");
			this.btnMasLocSearch.AccessibleName = resources.GetString("btnMasLocSearch.AccessibleName");
			this.btnMasLocSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnMasLocSearch.Anchor")));
			this.btnMasLocSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMasLocSearch.BackgroundImage")));
			this.btnMasLocSearch.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnMasLocSearch.Dock")));
			this.btnMasLocSearch.Enabled = ((bool)(resources.GetObject("btnMasLocSearch.Enabled")));
			this.btnMasLocSearch.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnMasLocSearch.FlatStyle")));
			this.btnMasLocSearch.Font = ((System.Drawing.Font)(resources.GetObject("btnMasLocSearch.Font")));
			this.btnMasLocSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnMasLocSearch.Image")));
			this.btnMasLocSearch.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMasLocSearch.ImageAlign")));
			this.btnMasLocSearch.ImageIndex = ((int)(resources.GetObject("btnMasLocSearch.ImageIndex")));
			this.btnMasLocSearch.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnMasLocSearch.ImeMode")));
			this.btnMasLocSearch.Location = ((System.Drawing.Point)(resources.GetObject("btnMasLocSearch.Location")));
			this.btnMasLocSearch.Name = "btnMasLocSearch";
			this.btnMasLocSearch.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnMasLocSearch.RightToLeft")));
			this.btnMasLocSearch.Size = ((System.Drawing.Size)(resources.GetObject("btnMasLocSearch.Size")));
			this.btnMasLocSearch.TabIndex = ((int)(resources.GetObject("btnMasLocSearch.TabIndex")));
			this.btnMasLocSearch.Text = resources.GetString("btnMasLocSearch.Text");
			this.btnMasLocSearch.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnMasLocSearch.TextAlign")));
			this.btnMasLocSearch.Visible = ((bool)(resources.GetObject("btnMasLocSearch.Visible")));
			this.btnMasLocSearch.Click += new System.EventHandler(this.btnMasLocSearch_Click);
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
			// 
			// btnCycleSearch
			// 
			this.btnCycleSearch.AccessibleDescription = resources.GetString("btnCycleSearch.AccessibleDescription");
			this.btnCycleSearch.AccessibleName = resources.GetString("btnCycleSearch.AccessibleName");
			this.btnCycleSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCycleSearch.Anchor")));
			this.btnCycleSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCycleSearch.BackgroundImage")));
			this.btnCycleSearch.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCycleSearch.Dock")));
			this.btnCycleSearch.Enabled = ((bool)(resources.GetObject("btnCycleSearch.Enabled")));
			this.btnCycleSearch.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCycleSearch.FlatStyle")));
			this.btnCycleSearch.Font = ((System.Drawing.Font)(resources.GetObject("btnCycleSearch.Font")));
			this.btnCycleSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnCycleSearch.Image")));
			this.btnCycleSearch.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCycleSearch.ImageAlign")));
			this.btnCycleSearch.ImageIndex = ((int)(resources.GetObject("btnCycleSearch.ImageIndex")));
			this.btnCycleSearch.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCycleSearch.ImeMode")));
			this.btnCycleSearch.Location = ((System.Drawing.Point)(resources.GetObject("btnCycleSearch.Location")));
			this.btnCycleSearch.Name = "btnCycleSearch";
			this.btnCycleSearch.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCycleSearch.RightToLeft")));
			this.btnCycleSearch.Size = ((System.Drawing.Size)(resources.GetObject("btnCycleSearch.Size")));
			this.btnCycleSearch.TabIndex = ((int)(resources.GetObject("btnCycleSearch.TabIndex")));
			this.btnCycleSearch.Text = resources.GetString("btnCycleSearch.Text");
			this.btnCycleSearch.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCycleSearch.TextAlign")));
			this.btnCycleSearch.Visible = ((bool)(resources.GetObject("btnCycleSearch.Visible")));
			this.btnCycleSearch.Click += new System.EventHandler(this.btnCycleSearch_Click);
			// 
			// txtCycle
			// 
			this.txtCycle.AccessibleDescription = resources.GetString("txtCycle.AccessibleDescription");
			this.txtCycle.AccessibleName = resources.GetString("txtCycle.AccessibleName");
			this.txtCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtCycle.Anchor")));
			this.txtCycle.AutoSize = ((bool)(resources.GetObject("txtCycle.AutoSize")));
			this.txtCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtCycle.BackgroundImage")));
			this.txtCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtCycle.Dock")));
			this.txtCycle.Enabled = ((bool)(resources.GetObject("txtCycle.Enabled")));
			this.txtCycle.Font = ((System.Drawing.Font)(resources.GetObject("txtCycle.Font")));
			this.txtCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtCycle.ImeMode")));
			this.txtCycle.Location = ((System.Drawing.Point)(resources.GetObject("txtCycle.Location")));
			this.txtCycle.MaxLength = ((int)(resources.GetObject("txtCycle.MaxLength")));
			this.txtCycle.Multiline = ((bool)(resources.GetObject("txtCycle.Multiline")));
			this.txtCycle.Name = "txtCycle";
			this.txtCycle.PasswordChar = ((char)(resources.GetObject("txtCycle.PasswordChar")));
			this.txtCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtCycle.RightToLeft")));
			this.txtCycle.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtCycle.ScrollBars")));
			this.txtCycle.Size = ((System.Drawing.Size)(resources.GetObject("txtCycle.Size")));
			this.txtCycle.TabIndex = ((int)(resources.GetObject("txtCycle.TabIndex")));
			this.txtCycle.Text = resources.GetString("txtCycle.Text");
			this.txtCycle.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtCycle.TextAlign")));
			this.txtCycle.Visible = ((bool)(resources.GetObject("txtCycle.Visible")));
			this.txtCycle.WordWrap = ((bool)(resources.GetObject("txtCycle.WordWrap")));
			this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCycle_KeyDown);
			this.txtCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtCycle_Validating);
			// 
			// cboViewType
			// 
			this.cboViewType.AccessibleDescription = resources.GetString("cboViewType.AccessibleDescription");
			this.cboViewType.AccessibleName = resources.GetString("cboViewType.AccessibleName");
			this.cboViewType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboViewType.Anchor")));
			this.cboViewType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboViewType.BackgroundImage")));
			this.cboViewType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboViewType.Dock")));
			this.cboViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboViewType.Enabled = ((bool)(resources.GetObject("cboViewType.Enabled")));
			this.cboViewType.Font = ((System.Drawing.Font)(resources.GetObject("cboViewType.Font")));
			this.cboViewType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboViewType.ImeMode")));
			this.cboViewType.IntegralHeight = ((bool)(resources.GetObject("cboViewType.IntegralHeight")));
			this.cboViewType.ItemHeight = ((int)(resources.GetObject("cboViewType.ItemHeight")));
			this.cboViewType.Items.AddRange(new object[] {
															 resources.GetString("cboViewType.Items"),
															 resources.GetString("cboViewType.Items1")});
			this.cboViewType.Location = ((System.Drawing.Point)(resources.GetObject("cboViewType.Location")));
			this.cboViewType.MaxDropDownItems = ((int)(resources.GetObject("cboViewType.MaxDropDownItems")));
			this.cboViewType.MaxLength = ((int)(resources.GetObject("cboViewType.MaxLength")));
			this.cboViewType.Name = "cboViewType";
			this.cboViewType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboViewType.RightToLeft")));
			this.cboViewType.Size = ((System.Drawing.Size)(resources.GetObject("cboViewType.Size")));
			this.cboViewType.TabIndex = ((int)(resources.GetObject("cboViewType.TabIndex")));
			this.cboViewType.Text = resources.GetString("cboViewType.Text");
			this.cboViewType.Visible = ((bool)(resources.GetObject("cboViewType.Visible")));
			this.cboViewType.Leave += new System.EventHandler(this.OnLeaveControl);
			this.cboViewType.Enter += new System.EventHandler(this.OnEnterControl);
			// 
			// lblViewType
			// 
			this.lblViewType.AccessibleDescription = resources.GetString("lblViewType.AccessibleDescription");
			this.lblViewType.AccessibleName = resources.GetString("lblViewType.AccessibleName");
			this.lblViewType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblViewType.Anchor")));
			this.lblViewType.AutoSize = ((bool)(resources.GetObject("lblViewType.AutoSize")));
			this.lblViewType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblViewType.Dock")));
			this.lblViewType.Enabled = ((bool)(resources.GetObject("lblViewType.Enabled")));
			this.lblViewType.Font = ((System.Drawing.Font)(resources.GetObject("lblViewType.Font")));
			this.lblViewType.ForeColor = System.Drawing.Color.Maroon;
			this.lblViewType.Image = ((System.Drawing.Image)(resources.GetObject("lblViewType.Image")));
			this.lblViewType.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblViewType.ImageAlign")));
			this.lblViewType.ImageIndex = ((int)(resources.GetObject("lblViewType.ImageIndex")));
			this.lblViewType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblViewType.ImeMode")));
			this.lblViewType.Location = ((System.Drawing.Point)(resources.GetObject("lblViewType.Location")));
			this.lblViewType.Name = "lblViewType";
			this.lblViewType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblViewType.RightToLeft")));
			this.lblViewType.Size = ((System.Drawing.Size)(resources.GetObject("lblViewType.Size")));
			this.lblViewType.TabIndex = ((int)(resources.GetObject("lblViewType.TabIndex")));
			this.lblViewType.Text = resources.GetString("lblViewType.Text");
			this.lblViewType.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblViewType.TextAlign")));
			this.lblViewType.Visible = ((bool)(resources.GetObject("lblViewType.Visible")));
			// 
			// dtmToDueDate
			// 
			this.dtmToDueDate.AcceptsEscape = ((bool)(resources.GetObject("dtmToDueDate.AcceptsEscape")));
			this.dtmToDueDate.AccessibleDescription = resources.GetString("dtmToDueDate.AccessibleDescription");
			this.dtmToDueDate.AccessibleName = resources.GetString("dtmToDueDate.AccessibleName");
			this.dtmToDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmToDueDate.Anchor")));
			this.dtmToDueDate.AutoSize = ((bool)(resources.GetObject("dtmToDueDate.AutoSize")));
			this.dtmToDueDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmToDueDate.BackgroundImage")));
			this.dtmToDueDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmToDueDate.BorderStyle")));
			// 
			// dtmToDueDate.Calendar
			// 
			this.dtmToDueDate.Calendar.AccessibleDescription = resources.GetString("dtmToDueDate.Calendar.AccessibleDescription");
			this.dtmToDueDate.Calendar.AccessibleName = resources.GetString("dtmToDueDate.Calendar.AccessibleName");
			this.dtmToDueDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmToDueDate.Calendar.AnnuallyBoldedDates")));
			this.dtmToDueDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmToDueDate.Calendar.BackgroundImage")));
			this.dtmToDueDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmToDueDate.Calendar.BoldedDates")));
			this.dtmToDueDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmToDueDate.Calendar.CalendarDimensions")));
			this.dtmToDueDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmToDueDate.Calendar.Enabled")));
			this.dtmToDueDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmToDueDate.Calendar.FirstDayOfWeek")));
			this.dtmToDueDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmToDueDate.Calendar.Font")));
			this.dtmToDueDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmToDueDate.Calendar.ImeMode")));
			this.dtmToDueDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmToDueDate.Calendar.MonthlyBoldedDates")));
			this.dtmToDueDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmToDueDate.Calendar.RightToLeft")));
			this.dtmToDueDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmToDueDate.Calendar.ShowClearButton")));
			this.dtmToDueDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmToDueDate.Calendar.ShowTodayButton")));
			this.dtmToDueDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmToDueDate.Calendar.ShowWeekNumbers")));
			this.dtmToDueDate.CaseSensitive = ((bool)(resources.GetObject("dtmToDueDate.CaseSensitive")));
			this.dtmToDueDate.Culture = ((int)(resources.GetObject("dtmToDueDate.Culture")));
			this.dtmToDueDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmToDueDate.CurrentTimeZone")));
			this.dtmToDueDate.CustomFormat = resources.GetString("dtmToDueDate.CustomFormat");
			this.dtmToDueDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmToDueDate.DaylightTimeAdjustment")));
			this.dtmToDueDate.DisplayFormat.CustomFormat = resources.GetString("dtmToDueDate.DisplayFormat.CustomFormat");
			this.dtmToDueDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDueDate.DisplayFormat.FormatType")));
			this.dtmToDueDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDueDate.DisplayFormat.Inherit")));
			this.dtmToDueDate.DisplayFormat.NullText = resources.GetString("dtmToDueDate.DisplayFormat.NullText");
			this.dtmToDueDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmToDueDate.DisplayFormat.TrimEnd")));
			this.dtmToDueDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmToDueDate.DisplayFormat.TrimStart")));
			this.dtmToDueDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmToDueDate.Dock")));
			this.dtmToDueDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmToDueDate.DropDownFormAlign")));
			this.dtmToDueDate.EditFormat.CustomFormat = resources.GetString("dtmToDueDate.EditFormat.CustomFormat");
			this.dtmToDueDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDueDate.EditFormat.FormatType")));
			this.dtmToDueDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDueDate.EditFormat.Inherit")));
			this.dtmToDueDate.EditFormat.NullText = resources.GetString("dtmToDueDate.EditFormat.NullText");
			this.dtmToDueDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmToDueDate.EditFormat.TrimEnd")));
			this.dtmToDueDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmToDueDate.EditFormat.TrimStart")));
			this.dtmToDueDate.EditMask = resources.GetString("dtmToDueDate.EditMask");
			this.dtmToDueDate.EmptyAsNull = ((bool)(resources.GetObject("dtmToDueDate.EmptyAsNull")));
			this.dtmToDueDate.Enabled = ((bool)(resources.GetObject("dtmToDueDate.Enabled")));
			this.dtmToDueDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmToDueDate.ErrorInfo.BeepOnError")));
			this.dtmToDueDate.ErrorInfo.ErrorMessage = resources.GetString("dtmToDueDate.ErrorInfo.ErrorMessage");
			this.dtmToDueDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmToDueDate.ErrorInfo.ErrorMessageCaption");
			this.dtmToDueDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmToDueDate.ErrorInfo.ShowErrorMessage")));
			this.dtmToDueDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmToDueDate.ErrorInfo.ValueOnError")));
			this.dtmToDueDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmToDueDate.Font")));
			this.dtmToDueDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDueDate.FormatType")));
			this.dtmToDueDate.GapHeight = ((int)(resources.GetObject("dtmToDueDate.GapHeight")));
			this.dtmToDueDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmToDueDate.GMTOffset")));
			this.dtmToDueDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmToDueDate.ImeMode")));
			this.dtmToDueDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmToDueDate.InitialSelection")));
			this.dtmToDueDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmToDueDate.Location")));
			this.dtmToDueDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmToDueDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmToDueDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmToDueDate.MaskInfo.CaseSensitive")));
			this.dtmToDueDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmToDueDate.MaskInfo.CopyWithLiterals")));
			this.dtmToDueDate.MaskInfo.EditMask = resources.GetString("dtmToDueDate.MaskInfo.EditMask");
			this.dtmToDueDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmToDueDate.MaskInfo.EmptyAsNull")));
			this.dtmToDueDate.MaskInfo.ErrorMessage = resources.GetString("dtmToDueDate.MaskInfo.ErrorMessage");
			this.dtmToDueDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmToDueDate.MaskInfo.Inherit")));
			this.dtmToDueDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmToDueDate.MaskInfo.PromptChar")));
			this.dtmToDueDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmToDueDate.MaskInfo.ShowLiterals")));
			this.dtmToDueDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmToDueDate.MaskInfo.StoredEmptyChar")));
			this.dtmToDueDate.MaxLength = ((int)(resources.GetObject("dtmToDueDate.MaxLength")));
			this.dtmToDueDate.Name = "dtmToDueDate";
			this.dtmToDueDate.NullText = resources.GetString("dtmToDueDate.NullText");
			this.dtmToDueDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmToDueDate.ParseInfo.CaseSensitive")));
			this.dtmToDueDate.ParseInfo.CustomFormat = resources.GetString("dtmToDueDate.ParseInfo.CustomFormat");
			this.dtmToDueDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmToDueDate.ParseInfo.DateTimeStyle")));
			this.dtmToDueDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmToDueDate.ParseInfo.EmptyAsNull")));
			this.dtmToDueDate.ParseInfo.ErrorMessage = resources.GetString("dtmToDueDate.ParseInfo.ErrorMessage");
			this.dtmToDueDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmToDueDate.ParseInfo.FormatType")));
			this.dtmToDueDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmToDueDate.ParseInfo.Inherit")));
			this.dtmToDueDate.ParseInfo.NullText = resources.GetString("dtmToDueDate.ParseInfo.NullText");
			this.dtmToDueDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmToDueDate.ParseInfo.TrimEnd")));
			this.dtmToDueDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmToDueDate.ParseInfo.TrimStart")));
			this.dtmToDueDate.PasswordChar = ((char)(resources.GetObject("dtmToDueDate.PasswordChar")));
			this.dtmToDueDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmToDueDate.PostValidation.CaseSensitive")));
			this.dtmToDueDate.PostValidation.ErrorMessage = resources.GetString("dtmToDueDate.PostValidation.ErrorMessage");
			this.dtmToDueDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmToDueDate.PostValidation.Inherit")));
			this.dtmToDueDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmToDueDate.PostValidation.Validation")));
			this.dtmToDueDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmToDueDate.PostValidation.Values")));
			this.dtmToDueDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmToDueDate.PostValidation.ValuesExcluded")));
			this.dtmToDueDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmToDueDate.PreValidation.CaseSensitive")));
			this.dtmToDueDate.PreValidation.ErrorMessage = resources.GetString("dtmToDueDate.PreValidation.ErrorMessage");
			this.dtmToDueDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmToDueDate.PreValidation.Inherit")));
			this.dtmToDueDate.PreValidation.ItemSeparator = resources.GetString("dtmToDueDate.PreValidation.ItemSeparator");
			this.dtmToDueDate.PreValidation.PatternString = resources.GetString("dtmToDueDate.PreValidation.PatternString");
			this.dtmToDueDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmToDueDate.PreValidation.RegexOptions")));
			this.dtmToDueDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmToDueDate.PreValidation.TrimEnd")));
			this.dtmToDueDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmToDueDate.PreValidation.TrimStart")));
			this.dtmToDueDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmToDueDate.PreValidation.Validation")));
			this.dtmToDueDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmToDueDate.RightToLeft")));
			this.dtmToDueDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmToDueDate.ShowFocusRectangle")));
			this.dtmToDueDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmToDueDate.Size")));
			this.dtmToDueDate.TabIndex = ((int)(resources.GetObject("dtmToDueDate.TabIndex")));
			this.dtmToDueDate.Tag = ((object)(resources.GetObject("dtmToDueDate.Tag")));
			this.dtmToDueDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmToDueDate.TextAlign")));
			this.dtmToDueDate.TrimEnd = ((bool)(resources.GetObject("dtmToDueDate.TrimEnd")));
			this.dtmToDueDate.TrimStart = ((bool)(resources.GetObject("dtmToDueDate.TrimStart")));
			this.dtmToDueDate.UserCultureOverride = ((bool)(resources.GetObject("dtmToDueDate.UserCultureOverride")));
			this.dtmToDueDate.Value = ((object)(resources.GetObject("dtmToDueDate.Value")));
			this.dtmToDueDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmToDueDate.VerticalAlign")));
			this.dtmToDueDate.Visible = ((bool)(resources.GetObject("dtmToDueDate.Visible")));
			this.dtmToDueDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmToDueDate.VisibleButtons")));
			this.dtmToDueDate.Enter += new System.EventHandler(this.OnEnterControl);
			this.dtmToDueDate.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// dtmFromDueDate
			// 
			this.dtmFromDueDate.AcceptsEscape = ((bool)(resources.GetObject("dtmFromDueDate.AcceptsEscape")));
			this.dtmFromDueDate.AccessibleDescription = resources.GetString("dtmFromDueDate.AccessibleDescription");
			this.dtmFromDueDate.AccessibleName = resources.GetString("dtmFromDueDate.AccessibleName");
			this.dtmFromDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmFromDueDate.Anchor")));
			this.dtmFromDueDate.AutoSize = ((bool)(resources.GetObject("dtmFromDueDate.AutoSize")));
			this.dtmFromDueDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmFromDueDate.BackgroundImage")));
			this.dtmFromDueDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmFromDueDate.BorderStyle")));
			// 
			// dtmFromDueDate.Calendar
			// 
			this.dtmFromDueDate.Calendar.AccessibleDescription = resources.GetString("dtmFromDueDate.Calendar.AccessibleDescription");
			this.dtmFromDueDate.Calendar.AccessibleName = resources.GetString("dtmFromDueDate.Calendar.AccessibleName");
			this.dtmFromDueDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmFromDueDate.Calendar.AnnuallyBoldedDates")));
			this.dtmFromDueDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmFromDueDate.Calendar.BackgroundImage")));
			this.dtmFromDueDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmFromDueDate.Calendar.BoldedDates")));
			this.dtmFromDueDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmFromDueDate.Calendar.CalendarDimensions")));
			this.dtmFromDueDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmFromDueDate.Calendar.Enabled")));
			this.dtmFromDueDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmFromDueDate.Calendar.FirstDayOfWeek")));
			this.dtmFromDueDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmFromDueDate.Calendar.Font")));
			this.dtmFromDueDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmFromDueDate.Calendar.ImeMode")));
			this.dtmFromDueDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmFromDueDate.Calendar.MonthlyBoldedDates")));
			this.dtmFromDueDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmFromDueDate.Calendar.RightToLeft")));
			this.dtmFromDueDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmFromDueDate.Calendar.ShowClearButton")));
			this.dtmFromDueDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmFromDueDate.Calendar.ShowTodayButton")));
			this.dtmFromDueDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmFromDueDate.Calendar.ShowWeekNumbers")));
			this.dtmFromDueDate.CaseSensitive = ((bool)(resources.GetObject("dtmFromDueDate.CaseSensitive")));
			this.dtmFromDueDate.Culture = ((int)(resources.GetObject("dtmFromDueDate.Culture")));
			this.dtmFromDueDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmFromDueDate.CurrentTimeZone")));
			this.dtmFromDueDate.CustomFormat = resources.GetString("dtmFromDueDate.CustomFormat");
			this.dtmFromDueDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmFromDueDate.DaylightTimeAdjustment")));
			this.dtmFromDueDate.DisplayFormat.CustomFormat = resources.GetString("dtmFromDueDate.DisplayFormat.CustomFormat");
			this.dtmFromDueDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDueDate.DisplayFormat.FormatType")));
			this.dtmFromDueDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDueDate.DisplayFormat.Inherit")));
			this.dtmFromDueDate.DisplayFormat.NullText = resources.GetString("dtmFromDueDate.DisplayFormat.NullText");
			this.dtmFromDueDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmFromDueDate.DisplayFormat.TrimEnd")));
			this.dtmFromDueDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmFromDueDate.DisplayFormat.TrimStart")));
			this.dtmFromDueDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmFromDueDate.Dock")));
			this.dtmFromDueDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmFromDueDate.DropDownFormAlign")));
			this.dtmFromDueDate.EditFormat.CustomFormat = resources.GetString("dtmFromDueDate.EditFormat.CustomFormat");
			this.dtmFromDueDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDueDate.EditFormat.FormatType")));
			this.dtmFromDueDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDueDate.EditFormat.Inherit")));
			this.dtmFromDueDate.EditFormat.NullText = resources.GetString("dtmFromDueDate.EditFormat.NullText");
			this.dtmFromDueDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmFromDueDate.EditFormat.TrimEnd")));
			this.dtmFromDueDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmFromDueDate.EditFormat.TrimStart")));
			this.dtmFromDueDate.EditMask = resources.GetString("dtmFromDueDate.EditMask");
			this.dtmFromDueDate.EmptyAsNull = ((bool)(resources.GetObject("dtmFromDueDate.EmptyAsNull")));
			this.dtmFromDueDate.Enabled = ((bool)(resources.GetObject("dtmFromDueDate.Enabled")));
			this.dtmFromDueDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmFromDueDate.ErrorInfo.BeepOnError")));
			this.dtmFromDueDate.ErrorInfo.ErrorMessage = resources.GetString("dtmFromDueDate.ErrorInfo.ErrorMessage");
			this.dtmFromDueDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmFromDueDate.ErrorInfo.ErrorMessageCaption");
			this.dtmFromDueDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmFromDueDate.ErrorInfo.ShowErrorMessage")));
			this.dtmFromDueDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmFromDueDate.ErrorInfo.ValueOnError")));
			this.dtmFromDueDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmFromDueDate.Font")));
			this.dtmFromDueDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDueDate.FormatType")));
			this.dtmFromDueDate.GapHeight = ((int)(resources.GetObject("dtmFromDueDate.GapHeight")));
			this.dtmFromDueDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmFromDueDate.GMTOffset")));
			this.dtmFromDueDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmFromDueDate.ImeMode")));
			this.dtmFromDueDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmFromDueDate.InitialSelection")));
			this.dtmFromDueDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmFromDueDate.Location")));
			this.dtmFromDueDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmFromDueDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmFromDueDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmFromDueDate.MaskInfo.CaseSensitive")));
			this.dtmFromDueDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmFromDueDate.MaskInfo.CopyWithLiterals")));
			this.dtmFromDueDate.MaskInfo.EditMask = resources.GetString("dtmFromDueDate.MaskInfo.EditMask");
			this.dtmFromDueDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmFromDueDate.MaskInfo.EmptyAsNull")));
			this.dtmFromDueDate.MaskInfo.ErrorMessage = resources.GetString("dtmFromDueDate.MaskInfo.ErrorMessage");
			this.dtmFromDueDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmFromDueDate.MaskInfo.Inherit")));
			this.dtmFromDueDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmFromDueDate.MaskInfo.PromptChar")));
			this.dtmFromDueDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmFromDueDate.MaskInfo.ShowLiterals")));
			this.dtmFromDueDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmFromDueDate.MaskInfo.StoredEmptyChar")));
			this.dtmFromDueDate.MaxLength = ((int)(resources.GetObject("dtmFromDueDate.MaxLength")));
			this.dtmFromDueDate.Name = "dtmFromDueDate";
			this.dtmFromDueDate.NullText = resources.GetString("dtmFromDueDate.NullText");
			this.dtmFromDueDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmFromDueDate.ParseInfo.CaseSensitive")));
			this.dtmFromDueDate.ParseInfo.CustomFormat = resources.GetString("dtmFromDueDate.ParseInfo.CustomFormat");
			this.dtmFromDueDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmFromDueDate.ParseInfo.DateTimeStyle")));
			this.dtmFromDueDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmFromDueDate.ParseInfo.EmptyAsNull")));
			this.dtmFromDueDate.ParseInfo.ErrorMessage = resources.GetString("dtmFromDueDate.ParseInfo.ErrorMessage");
			this.dtmFromDueDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmFromDueDate.ParseInfo.FormatType")));
			this.dtmFromDueDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmFromDueDate.ParseInfo.Inherit")));
			this.dtmFromDueDate.ParseInfo.NullText = resources.GetString("dtmFromDueDate.ParseInfo.NullText");
			this.dtmFromDueDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmFromDueDate.ParseInfo.TrimEnd")));
			this.dtmFromDueDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmFromDueDate.ParseInfo.TrimStart")));
			this.dtmFromDueDate.PasswordChar = ((char)(resources.GetObject("dtmFromDueDate.PasswordChar")));
			this.dtmFromDueDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmFromDueDate.PostValidation.CaseSensitive")));
			this.dtmFromDueDate.PostValidation.ErrorMessage = resources.GetString("dtmFromDueDate.PostValidation.ErrorMessage");
			this.dtmFromDueDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmFromDueDate.PostValidation.Inherit")));
			this.dtmFromDueDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmFromDueDate.PostValidation.Validation")));
			this.dtmFromDueDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmFromDueDate.PostValidation.Values")));
			this.dtmFromDueDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmFromDueDate.PostValidation.ValuesExcluded")));
			this.dtmFromDueDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmFromDueDate.PreValidation.CaseSensitive")));
			this.dtmFromDueDate.PreValidation.ErrorMessage = resources.GetString("dtmFromDueDate.PreValidation.ErrorMessage");
			this.dtmFromDueDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmFromDueDate.PreValidation.Inherit")));
			this.dtmFromDueDate.PreValidation.ItemSeparator = resources.GetString("dtmFromDueDate.PreValidation.ItemSeparator");
			this.dtmFromDueDate.PreValidation.PatternString = resources.GetString("dtmFromDueDate.PreValidation.PatternString");
			this.dtmFromDueDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmFromDueDate.PreValidation.RegexOptions")));
			this.dtmFromDueDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmFromDueDate.PreValidation.TrimEnd")));
			this.dtmFromDueDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmFromDueDate.PreValidation.TrimStart")));
			this.dtmFromDueDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmFromDueDate.PreValidation.Validation")));
			this.dtmFromDueDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmFromDueDate.RightToLeft")));
			this.dtmFromDueDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmFromDueDate.ShowFocusRectangle")));
			this.dtmFromDueDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmFromDueDate.Size")));
			this.dtmFromDueDate.TabIndex = ((int)(resources.GetObject("dtmFromDueDate.TabIndex")));
			this.dtmFromDueDate.Tag = ((object)(resources.GetObject("dtmFromDueDate.Tag")));
			this.dtmFromDueDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmFromDueDate.TextAlign")));
			this.dtmFromDueDate.TrimEnd = ((bool)(resources.GetObject("dtmFromDueDate.TrimEnd")));
			this.dtmFromDueDate.TrimStart = ((bool)(resources.GetObject("dtmFromDueDate.TrimStart")));
			this.dtmFromDueDate.UserCultureOverride = ((bool)(resources.GetObject("dtmFromDueDate.UserCultureOverride")));
			this.dtmFromDueDate.Value = ((object)(resources.GetObject("dtmFromDueDate.Value")));
			this.dtmFromDueDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmFromDueDate.VerticalAlign")));
			this.dtmFromDueDate.Visible = ((bool)(resources.GetObject("dtmFromDueDate.Visible")));
			this.dtmFromDueDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmFromDueDate.VisibleButtons")));
			this.dtmFromDueDate.Enter += new System.EventHandler(this.OnEnterControl);
			this.dtmFromDueDate.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// lblMasLoc
			// 
			this.lblMasLoc.AccessibleDescription = resources.GetString("lblMasLoc.AccessibleDescription");
			this.lblMasLoc.AccessibleName = resources.GetString("lblMasLoc.AccessibleName");
			this.lblMasLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMasLoc.Anchor")));
			this.lblMasLoc.AutoSize = ((bool)(resources.GetObject("lblMasLoc.AutoSize")));
			this.lblMasLoc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMasLoc.Dock")));
			this.lblMasLoc.Enabled = ((bool)(resources.GetObject("lblMasLoc.Enabled")));
			this.lblMasLoc.Font = ((System.Drawing.Font)(resources.GetObject("lblMasLoc.Font")));
			this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
			this.lblMasLoc.Image = ((System.Drawing.Image)(resources.GetObject("lblMasLoc.Image")));
			this.lblMasLoc.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMasLoc.ImageAlign")));
			this.lblMasLoc.ImageIndex = ((int)(resources.GetObject("lblMasLoc.ImageIndex")));
			this.lblMasLoc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMasLoc.ImeMode")));
			this.lblMasLoc.Location = ((System.Drawing.Point)(resources.GetObject("lblMasLoc.Location")));
			this.lblMasLoc.Name = "lblMasLoc";
			this.lblMasLoc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMasLoc.RightToLeft")));
			this.lblMasLoc.Size = ((System.Drawing.Size)(resources.GetObject("lblMasLoc.Size")));
			this.lblMasLoc.TabIndex = ((int)(resources.GetObject("lblMasLoc.TabIndex")));
			this.lblMasLoc.Text = resources.GetString("lblMasLoc.Text");
			this.lblMasLoc.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMasLoc.TextAlign")));
			this.lblMasLoc.Visible = ((bool)(resources.GetObject("lblMasLoc.Visible")));
			// 
			// cboPlanType
			// 
			this.cboPlanType.AccessibleDescription = resources.GetString("cboPlanType.AccessibleDescription");
			this.cboPlanType.AccessibleName = resources.GetString("cboPlanType.AccessibleName");
			this.cboPlanType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cboPlanType.Anchor")));
			this.cboPlanType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cboPlanType.BackgroundImage")));
			this.cboPlanType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cboPlanType.Dock")));
			this.cboPlanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPlanType.Enabled = ((bool)(resources.GetObject("cboPlanType.Enabled")));
			this.cboPlanType.Font = ((System.Drawing.Font)(resources.GetObject("cboPlanType.Font")));
			this.cboPlanType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cboPlanType.ImeMode")));
			this.cboPlanType.IntegralHeight = ((bool)(resources.GetObject("cboPlanType.IntegralHeight")));
			this.cboPlanType.ItemHeight = ((int)(resources.GetObject("cboPlanType.ItemHeight")));
			this.cboPlanType.Items.AddRange(new object[] {
															 resources.GetString("cboPlanType.Items"),
															 resources.GetString("cboPlanType.Items1"),
															 resources.GetString("cboPlanType.Items2")});
			this.cboPlanType.Location = ((System.Drawing.Point)(resources.GetObject("cboPlanType.Location")));
			this.cboPlanType.MaxDropDownItems = ((int)(resources.GetObject("cboPlanType.MaxDropDownItems")));
			this.cboPlanType.MaxLength = ((int)(resources.GetObject("cboPlanType.MaxLength")));
			this.cboPlanType.Name = "cboPlanType";
			this.cboPlanType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cboPlanType.RightToLeft")));
			this.cboPlanType.Size = ((System.Drawing.Size)(resources.GetObject("cboPlanType.Size")));
			this.cboPlanType.TabIndex = ((int)(resources.GetObject("cboPlanType.TabIndex")));
			this.cboPlanType.Text = resources.GetString("cboPlanType.Text");
			this.cboPlanType.Visible = ((bool)(resources.GetObject("cboPlanType.Visible")));
			this.cboPlanType.SelectedIndexChanged += new System.EventHandler(this.cboPlanType_SelectedIndexChanged);
			// 
			// lblPlanType
			// 
			this.lblPlanType.AccessibleDescription = resources.GetString("lblPlanType.AccessibleDescription");
			this.lblPlanType.AccessibleName = resources.GetString("lblPlanType.AccessibleName");
			this.lblPlanType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPlanType.Anchor")));
			this.lblPlanType.AutoSize = ((bool)(resources.GetObject("lblPlanType.AutoSize")));
			this.lblPlanType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPlanType.Dock")));
			this.lblPlanType.Enabled = ((bool)(resources.GetObject("lblPlanType.Enabled")));
			this.lblPlanType.Font = ((System.Drawing.Font)(resources.GetObject("lblPlanType.Font")));
			this.lblPlanType.ForeColor = System.Drawing.Color.Maroon;
			this.lblPlanType.Image = ((System.Drawing.Image)(resources.GetObject("lblPlanType.Image")));
			this.lblPlanType.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPlanType.ImageAlign")));
			this.lblPlanType.ImageIndex = ((int)(resources.GetObject("lblPlanType.ImageIndex")));
			this.lblPlanType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPlanType.ImeMode")));
			this.lblPlanType.Location = ((System.Drawing.Point)(resources.GetObject("lblPlanType.Location")));
			this.lblPlanType.Name = "lblPlanType";
			this.lblPlanType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPlanType.RightToLeft")));
			this.lblPlanType.Size = ((System.Drawing.Size)(resources.GetObject("lblPlanType.Size")));
			this.lblPlanType.TabIndex = ((int)(resources.GetObject("lblPlanType.TabIndex")));
			this.lblPlanType.Text = resources.GetString("lblPlanType.Text");
			this.lblPlanType.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPlanType.TextAlign")));
			this.lblPlanType.Visible = ((bool)(resources.GetObject("lblPlanType.Visible")));
			// 
			// lblCycle
			// 
			this.lblCycle.AccessibleDescription = resources.GetString("lblCycle.AccessibleDescription");
			this.lblCycle.AccessibleName = resources.GetString("lblCycle.AccessibleName");
			this.lblCycle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCycle.Anchor")));
			this.lblCycle.AutoSize = ((bool)(resources.GetObject("lblCycle.AutoSize")));
			this.lblCycle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCycle.Dock")));
			this.lblCycle.Enabled = ((bool)(resources.GetObject("lblCycle.Enabled")));
			this.lblCycle.Font = ((System.Drawing.Font)(resources.GetObject("lblCycle.Font")));
			this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
			this.lblCycle.Image = ((System.Drawing.Image)(resources.GetObject("lblCycle.Image")));
			this.lblCycle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCycle.ImageAlign")));
			this.lblCycle.ImageIndex = ((int)(resources.GetObject("lblCycle.ImageIndex")));
			this.lblCycle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCycle.ImeMode")));
			this.lblCycle.Location = ((System.Drawing.Point)(resources.GetObject("lblCycle.Location")));
			this.lblCycle.Name = "lblCycle";
			this.lblCycle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCycle.RightToLeft")));
			this.lblCycle.Size = ((System.Drawing.Size)(resources.GetObject("lblCycle.Size")));
			this.lblCycle.TabIndex = ((int)(resources.GetObject("lblCycle.TabIndex")));
			this.lblCycle.Text = resources.GetString("lblCycle.Text");
			this.lblCycle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCycle.TextAlign")));
			this.lblCycle.Visible = ((bool)(resources.GetObject("lblCycle.Visible")));
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
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
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
			// btnSearch
			// 
			this.btnSearch.AccessibleDescription = resources.GetString("btnSearch.AccessibleDescription");
			this.btnSearch.AccessibleName = resources.GetString("btnSearch.AccessibleName");
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSearch.Anchor")));
			this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
			this.btnSearch.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSearch.Dock")));
			this.btnSearch.Enabled = ((bool)(resources.GetObject("btnSearch.Enabled")));
			this.btnSearch.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSearch.FlatStyle")));
			this.btnSearch.Font = ((System.Drawing.Font)(resources.GetObject("btnSearch.Font")));
			this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
			this.btnSearch.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearch.ImageAlign")));
			this.btnSearch.ImageIndex = ((int)(resources.GetObject("btnSearch.ImageIndex")));
			this.btnSearch.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSearch.ImeMode")));
			this.btnSearch.Location = ((System.Drawing.Point)(resources.GetObject("btnSearch.Location")));
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSearch.RightToLeft")));
			this.btnSearch.Size = ((System.Drawing.Size)(resources.GetObject("btnSearch.Size")));
			this.btnSearch.TabIndex = ((int)(resources.GetObject("btnSearch.TabIndex")));
			this.btnSearch.Text = resources.GetString("btnSearch.Text");
			this.btnSearch.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSearch.TextAlign")));
			this.btnSearch.Visible = ((bool)(resources.GetObject("btnSearch.Visible")));
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// btnPartNumberSearch
			// 
			this.btnPartNumberSearch.AccessibleDescription = resources.GetString("btnPartNumberSearch.AccessibleDescription");
			this.btnPartNumberSearch.AccessibleName = resources.GetString("btnPartNumberSearch.AccessibleName");
			this.btnPartNumberSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPartNumberSearch.Anchor")));
			this.btnPartNumberSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPartNumberSearch.BackgroundImage")));
			this.btnPartNumberSearch.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPartNumberSearch.Dock")));
			this.btnPartNumberSearch.Enabled = ((bool)(resources.GetObject("btnPartNumberSearch.Enabled")));
			this.btnPartNumberSearch.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPartNumberSearch.FlatStyle")));
			this.btnPartNumberSearch.Font = ((System.Drawing.Font)(resources.GetObject("btnPartNumberSearch.Font")));
			this.btnPartNumberSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnPartNumberSearch.Image")));
			this.btnPartNumberSearch.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPartNumberSearch.ImageAlign")));
			this.btnPartNumberSearch.ImageIndex = ((int)(resources.GetObject("btnPartNumberSearch.ImageIndex")));
			this.btnPartNumberSearch.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPartNumberSearch.ImeMode")));
			this.btnPartNumberSearch.Location = ((System.Drawing.Point)(resources.GetObject("btnPartNumberSearch.Location")));
			this.btnPartNumberSearch.Name = "btnPartNumberSearch";
			this.btnPartNumberSearch.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPartNumberSearch.RightToLeft")));
			this.btnPartNumberSearch.Size = ((System.Drawing.Size)(resources.GetObject("btnPartNumberSearch.Size")));
			this.btnPartNumberSearch.TabIndex = ((int)(resources.GetObject("btnPartNumberSearch.TabIndex")));
			this.btnPartNumberSearch.Text = resources.GetString("btnPartNumberSearch.Text");
			this.btnPartNumberSearch.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPartNumberSearch.TextAlign")));
			this.btnPartNumberSearch.Visible = ((bool)(resources.GetObject("btnPartNumberSearch.Visible")));
			this.btnPartNumberSearch.Click += new System.EventHandler(this.btnPartNumberSearch_Click);
			// 
			// btnPartNameSearch
			// 
			this.btnPartNameSearch.AccessibleDescription = resources.GetString("btnPartNameSearch.AccessibleDescription");
			this.btnPartNameSearch.AccessibleName = resources.GetString("btnPartNameSearch.AccessibleName");
			this.btnPartNameSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPartNameSearch.Anchor")));
			this.btnPartNameSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPartNameSearch.BackgroundImage")));
			this.btnPartNameSearch.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPartNameSearch.Dock")));
			this.btnPartNameSearch.Enabled = ((bool)(resources.GetObject("btnPartNameSearch.Enabled")));
			this.btnPartNameSearch.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnPartNameSearch.FlatStyle")));
			this.btnPartNameSearch.Font = ((System.Drawing.Font)(resources.GetObject("btnPartNameSearch.Font")));
			this.btnPartNameSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnPartNameSearch.Image")));
			this.btnPartNameSearch.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPartNameSearch.ImageAlign")));
			this.btnPartNameSearch.ImageIndex = ((int)(resources.GetObject("btnPartNameSearch.ImageIndex")));
			this.btnPartNameSearch.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPartNameSearch.ImeMode")));
			this.btnPartNameSearch.Location = ((System.Drawing.Point)(resources.GetObject("btnPartNameSearch.Location")));
			this.btnPartNameSearch.Name = "btnPartNameSearch";
			this.btnPartNameSearch.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPartNameSearch.RightToLeft")));
			this.btnPartNameSearch.Size = ((System.Drawing.Size)(resources.GetObject("btnPartNameSearch.Size")));
			this.btnPartNameSearch.TabIndex = ((int)(resources.GetObject("btnPartNameSearch.TabIndex")));
			this.btnPartNameSearch.Text = resources.GetString("btnPartNameSearch.Text");
			this.btnPartNameSearch.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnPartNameSearch.TextAlign")));
			this.btnPartNameSearch.Visible = ((bool)(resources.GetObject("btnPartNameSearch.Visible")));
			this.btnPartNameSearch.Click += new System.EventHandler(this.btnPartNameSearch_Click);
			// 
			// lblRevision
			// 
			this.lblRevision.AccessibleDescription = resources.GetString("lblRevision.AccessibleDescription");
			this.lblRevision.AccessibleName = resources.GetString("lblRevision.AccessibleName");
			this.lblRevision.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblRevision.Anchor")));
			this.lblRevision.AutoSize = ((bool)(resources.GetObject("lblRevision.AutoSize")));
			this.lblRevision.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblRevision.Dock")));
			this.lblRevision.Enabled = ((bool)(resources.GetObject("lblRevision.Enabled")));
			this.lblRevision.Font = ((System.Drawing.Font)(resources.GetObject("lblRevision.Font")));
			this.lblRevision.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRevision.Image = ((System.Drawing.Image)(resources.GetObject("lblRevision.Image")));
			this.lblRevision.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblRevision.ImageAlign")));
			this.lblRevision.ImageIndex = ((int)(resources.GetObject("lblRevision.ImageIndex")));
			this.lblRevision.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblRevision.ImeMode")));
			this.lblRevision.Location = ((System.Drawing.Point)(resources.GetObject("lblRevision.Location")));
			this.lblRevision.Name = "lblRevision";
			this.lblRevision.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblRevision.RightToLeft")));
			this.lblRevision.Size = ((System.Drawing.Size)(resources.GetObject("lblRevision.Size")));
			this.lblRevision.TabIndex = ((int)(resources.GetObject("lblRevision.TabIndex")));
			this.lblRevision.Text = resources.GetString("lblRevision.Text");
			this.lblRevision.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblRevision.TextAlign")));
			this.lblRevision.Visible = ((bool)(resources.GetObject("lblRevision.Visible")));
			// 
			// lblToDueDate
			// 
			this.lblToDueDate.AccessibleDescription = resources.GetString("lblToDueDate.AccessibleDescription");
			this.lblToDueDate.AccessibleName = resources.GetString("lblToDueDate.AccessibleName");
			this.lblToDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblToDueDate.Anchor")));
			this.lblToDueDate.AutoSize = ((bool)(resources.GetObject("lblToDueDate.AutoSize")));
			this.lblToDueDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblToDueDate.Dock")));
			this.lblToDueDate.Enabled = ((bool)(resources.GetObject("lblToDueDate.Enabled")));
			this.lblToDueDate.Font = ((System.Drawing.Font)(resources.GetObject("lblToDueDate.Font")));
			this.lblToDueDate.Image = ((System.Drawing.Image)(resources.GetObject("lblToDueDate.Image")));
			this.lblToDueDate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblToDueDate.ImageAlign")));
			this.lblToDueDate.ImageIndex = ((int)(resources.GetObject("lblToDueDate.ImageIndex")));
			this.lblToDueDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblToDueDate.ImeMode")));
			this.lblToDueDate.Location = ((System.Drawing.Point)(resources.GetObject("lblToDueDate.Location")));
			this.lblToDueDate.Name = "lblToDueDate";
			this.lblToDueDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblToDueDate.RightToLeft")));
			this.lblToDueDate.Size = ((System.Drawing.Size)(resources.GetObject("lblToDueDate.Size")));
			this.lblToDueDate.TabIndex = ((int)(resources.GetObject("lblToDueDate.TabIndex")));
			this.lblToDueDate.Text = resources.GetString("lblToDueDate.Text");
			this.lblToDueDate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblToDueDate.TextAlign")));
			this.lblToDueDate.Visible = ((bool)(resources.GetObject("lblToDueDate.Visible")));
			// 
			// lblPartNumer
			// 
			this.lblPartNumer.AccessibleDescription = resources.GetString("lblPartNumer.AccessibleDescription");
			this.lblPartNumer.AccessibleName = resources.GetString("lblPartNumer.AccessibleName");
			this.lblPartNumer.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPartNumer.Anchor")));
			this.lblPartNumer.AutoSize = ((bool)(resources.GetObject("lblPartNumer.AutoSize")));
			this.lblPartNumer.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPartNumer.Dock")));
			this.lblPartNumer.Enabled = ((bool)(resources.GetObject("lblPartNumer.Enabled")));
			this.lblPartNumer.Font = ((System.Drawing.Font)(resources.GetObject("lblPartNumer.Font")));
			this.lblPartNumer.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPartNumer.Image = ((System.Drawing.Image)(resources.GetObject("lblPartNumer.Image")));
			this.lblPartNumer.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPartNumer.ImageAlign")));
			this.lblPartNumer.ImageIndex = ((int)(resources.GetObject("lblPartNumer.ImageIndex")));
			this.lblPartNumer.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPartNumer.ImeMode")));
			this.lblPartNumer.Location = ((System.Drawing.Point)(resources.GetObject("lblPartNumer.Location")));
			this.lblPartNumer.Name = "lblPartNumer";
			this.lblPartNumer.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPartNumer.RightToLeft")));
			this.lblPartNumer.Size = ((System.Drawing.Size)(resources.GetObject("lblPartNumer.Size")));
			this.lblPartNumer.TabIndex = ((int)(resources.GetObject("lblPartNumer.TabIndex")));
			this.lblPartNumer.Text = resources.GetString("lblPartNumer.Text");
			this.lblPartNumer.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPartNumer.TextAlign")));
			this.lblPartNumer.Visible = ((bool)(resources.GetObject("lblPartNumer.Visible")));
			// 
			// lblCategory
			// 
			this.lblCategory.AccessibleDescription = resources.GetString("lblCategory.AccessibleDescription");
			this.lblCategory.AccessibleName = resources.GetString("lblCategory.AccessibleName");
			this.lblCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblCategory.Anchor")));
			this.lblCategory.AutoSize = ((bool)(resources.GetObject("lblCategory.AutoSize")));
			this.lblCategory.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblCategory.Dock")));
			this.lblCategory.Enabled = ((bool)(resources.GetObject("lblCategory.Enabled")));
			this.lblCategory.Font = ((System.Drawing.Font)(resources.GetObject("lblCategory.Font")));
			this.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCategory.Image = ((System.Drawing.Image)(resources.GetObject("lblCategory.Image")));
			this.lblCategory.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCategory.ImageAlign")));
			this.lblCategory.ImageIndex = ((int)(resources.GetObject("lblCategory.ImageIndex")));
			this.lblCategory.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblCategory.ImeMode")));
			this.lblCategory.Location = ((System.Drawing.Point)(resources.GetObject("lblCategory.Location")));
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblCategory.RightToLeft")));
			this.lblCategory.Size = ((System.Drawing.Size)(resources.GetObject("lblCategory.Size")));
			this.lblCategory.TabIndex = ((int)(resources.GetObject("lblCategory.TabIndex")));
			this.lblCategory.Text = resources.GetString("lblCategory.Text");
			this.lblCategory.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblCategory.TextAlign")));
			this.lblCategory.Visible = ((bool)(resources.GetObject("lblCategory.Visible")));
			// 
			// lblPartName
			// 
			this.lblPartName.AccessibleDescription = resources.GetString("lblPartName.AccessibleDescription");
			this.lblPartName.AccessibleName = resources.GetString("lblPartName.AccessibleName");
			this.lblPartName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblPartName.Anchor")));
			this.lblPartName.AutoSize = ((bool)(resources.GetObject("lblPartName.AutoSize")));
			this.lblPartName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblPartName.Dock")));
			this.lblPartName.Enabled = ((bool)(resources.GetObject("lblPartName.Enabled")));
			this.lblPartName.Font = ((System.Drawing.Font)(resources.GetObject("lblPartName.Font")));
			this.lblPartName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPartName.Image = ((System.Drawing.Image)(resources.GetObject("lblPartName.Image")));
			this.lblPartName.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPartName.ImageAlign")));
			this.lblPartName.ImageIndex = ((int)(resources.GetObject("lblPartName.ImageIndex")));
			this.lblPartName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblPartName.ImeMode")));
			this.lblPartName.Location = ((System.Drawing.Point)(resources.GetObject("lblPartName.Location")));
			this.lblPartName.Name = "lblPartName";
			this.lblPartName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblPartName.RightToLeft")));
			this.lblPartName.Size = ((System.Drawing.Size)(resources.GetObject("lblPartName.Size")));
			this.lblPartName.TabIndex = ((int)(resources.GetObject("lblPartName.TabIndex")));
			this.lblPartName.Text = resources.GetString("lblPartName.Text");
			this.lblPartName.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblPartName.TextAlign")));
			this.lblPartName.Visible = ((bool)(resources.GetObject("lblPartName.Visible")));
			// 
			// lblFromDueDate
			// 
			this.lblFromDueDate.AccessibleDescription = resources.GetString("lblFromDueDate.AccessibleDescription");
			this.lblFromDueDate.AccessibleName = resources.GetString("lblFromDueDate.AccessibleName");
			this.lblFromDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblFromDueDate.Anchor")));
			this.lblFromDueDate.AutoSize = ((bool)(resources.GetObject("lblFromDueDate.AutoSize")));
			this.lblFromDueDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblFromDueDate.Dock")));
			this.lblFromDueDate.Enabled = ((bool)(resources.GetObject("lblFromDueDate.Enabled")));
			this.lblFromDueDate.Font = ((System.Drawing.Font)(resources.GetObject("lblFromDueDate.Font")));
			this.lblFromDueDate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFromDueDate.Image = ((System.Drawing.Image)(resources.GetObject("lblFromDueDate.Image")));
			this.lblFromDueDate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFromDueDate.ImageAlign")));
			this.lblFromDueDate.ImageIndex = ((int)(resources.GetObject("lblFromDueDate.ImageIndex")));
			this.lblFromDueDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblFromDueDate.ImeMode")));
			this.lblFromDueDate.Location = ((System.Drawing.Point)(resources.GetObject("lblFromDueDate.Location")));
			this.lblFromDueDate.Name = "lblFromDueDate";
			this.lblFromDueDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblFromDueDate.RightToLeft")));
			this.lblFromDueDate.Size = ((System.Drawing.Size)(resources.GetObject("lblFromDueDate.Size")));
			this.lblFromDueDate.TabIndex = ((int)(resources.GetObject("lblFromDueDate.TabIndex")));
			this.lblFromDueDate.Text = resources.GetString("lblFromDueDate.Text");
			this.lblFromDueDate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblFromDueDate.TextAlign")));
			this.lblFromDueDate.Visible = ((bool)(resources.GetObject("lblFromDueDate.Visible")));
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
			this.dgrdData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.BeforeRowColChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdData_BeforeRowColChange);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.OnAddNew += new System.EventHandler(this.dgrdData_OnAddNew);
			this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
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
			// btnNewPOConvert
			// 
			this.btnNewPOConvert.AccessibleDescription = resources.GetString("btnNewPOConvert.AccessibleDescription");
			this.btnNewPOConvert.AccessibleName = resources.GetString("btnNewPOConvert.AccessibleName");
			this.btnNewPOConvert.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnNewPOConvert.Anchor")));
			this.btnNewPOConvert.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNewPOConvert.BackgroundImage")));
			this.btnNewPOConvert.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnNewPOConvert.Dock")));
			this.btnNewPOConvert.Enabled = ((bool)(resources.GetObject("btnNewPOConvert.Enabled")));
			this.btnNewPOConvert.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnNewPOConvert.FlatStyle")));
			this.btnNewPOConvert.Font = ((System.Drawing.Font)(resources.GetObject("btnNewPOConvert.Font")));
			this.btnNewPOConvert.Image = ((System.Drawing.Image)(resources.GetObject("btnNewPOConvert.Image")));
			this.btnNewPOConvert.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnNewPOConvert.ImageAlign")));
			this.btnNewPOConvert.ImageIndex = ((int)(resources.GetObject("btnNewPOConvert.ImageIndex")));
			this.btnNewPOConvert.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnNewPOConvert.ImeMode")));
			this.btnNewPOConvert.Location = ((System.Drawing.Point)(resources.GetObject("btnNewPOConvert.Location")));
			this.btnNewPOConvert.Name = "btnNewPOConvert";
			this.btnNewPOConvert.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnNewPOConvert.RightToLeft")));
			this.btnNewPOConvert.Size = ((System.Drawing.Size)(resources.GetObject("btnNewPOConvert.Size")));
			this.btnNewPOConvert.TabIndex = ((int)(resources.GetObject("btnNewPOConvert.TabIndex")));
			this.btnNewPOConvert.Text = resources.GetString("btnNewPOConvert.Text");
			this.btnNewPOConvert.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnNewPOConvert.TextAlign")));
			this.btnNewPOConvert.Visible = ((bool)(resources.GetObject("btnNewPOConvert.Visible")));
			this.btnNewPOConvert.Click += new System.EventHandler(this.btnNewPOConvert_Click);
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.AccessibleDescription = resources.GetString("chkSelectAll.AccessibleDescription");
			this.chkSelectAll.AccessibleName = resources.GetString("chkSelectAll.AccessibleName");
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkSelectAll.Anchor")));
			this.chkSelectAll.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkSelectAll.Appearance")));
			this.chkSelectAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkSelectAll.BackgroundImage")));
			this.chkSelectAll.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSelectAll.CheckAlign")));
			this.chkSelectAll.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkSelectAll.Dock")));
			this.chkSelectAll.Enabled = ((bool)(resources.GetObject("chkSelectAll.Enabled")));
			this.chkSelectAll.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkSelectAll.FlatStyle")));
			this.chkSelectAll.Font = ((System.Drawing.Font)(resources.GetObject("chkSelectAll.Font")));
			this.chkSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("chkSelectAll.Image")));
			this.chkSelectAll.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSelectAll.ImageAlign")));
			this.chkSelectAll.ImageIndex = ((int)(resources.GetObject("chkSelectAll.ImageIndex")));
			this.chkSelectAll.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkSelectAll.ImeMode")));
			this.chkSelectAll.Location = ((System.Drawing.Point)(resources.GetObject("chkSelectAll.Location")));
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkSelectAll.RightToLeft")));
			this.chkSelectAll.Size = ((System.Drawing.Size)(resources.GetObject("chkSelectAll.Size")));
			this.chkSelectAll.TabIndex = ((int)(resources.GetObject("chkSelectAll.TabIndex")));
			this.chkSelectAll.Text = resources.GetString("chkSelectAll.Text");
			this.chkSelectAll.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkSelectAll.TextAlign")));
			this.chkSelectAll.Visible = ((bool)(resources.GetObject("chkSelectAll.Visible")));
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// btnNewWOConvert
			// 
			this.btnNewWOConvert.AccessibleDescription = resources.GetString("btnNewWOConvert.AccessibleDescription");
			this.btnNewWOConvert.AccessibleName = resources.GetString("btnNewWOConvert.AccessibleName");
			this.btnNewWOConvert.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnNewWOConvert.Anchor")));
			this.btnNewWOConvert.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNewWOConvert.BackgroundImage")));
			this.btnNewWOConvert.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnNewWOConvert.Dock")));
			this.btnNewWOConvert.Enabled = ((bool)(resources.GetObject("btnNewWOConvert.Enabled")));
			this.btnNewWOConvert.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnNewWOConvert.FlatStyle")));
			this.btnNewWOConvert.Font = ((System.Drawing.Font)(resources.GetObject("btnNewWOConvert.Font")));
			this.btnNewWOConvert.Image = ((System.Drawing.Image)(resources.GetObject("btnNewWOConvert.Image")));
			this.btnNewWOConvert.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnNewWOConvert.ImageAlign")));
			this.btnNewWOConvert.ImageIndex = ((int)(resources.GetObject("btnNewWOConvert.ImageIndex")));
			this.btnNewWOConvert.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnNewWOConvert.ImeMode")));
			this.btnNewWOConvert.Location = ((System.Drawing.Point)(resources.GetObject("btnNewWOConvert.Location")));
			this.btnNewWOConvert.Name = "btnNewWOConvert";
			this.btnNewWOConvert.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnNewWOConvert.RightToLeft")));
			this.btnNewWOConvert.Size = ((System.Drawing.Size)(resources.GetObject("btnNewWOConvert.Size")));
			this.btnNewWOConvert.TabIndex = ((int)(resources.GetObject("btnNewWOConvert.TabIndex")));
			this.btnNewWOConvert.Text = resources.GetString("btnNewWOConvert.Text");
			this.btnNewWOConvert.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnNewWOConvert.TextAlign")));
			this.btnNewWOConvert.Visible = ((bool)(resources.GetObject("btnNewWOConvert.Visible")));
			this.btnNewWOConvert.Click += new System.EventHandler(this.btnNewWOConvert_Click);
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
			// btnExistingPOConvert
			// 
			this.btnExistingPOConvert.AccessibleDescription = resources.GetString("btnExistingPOConvert.AccessibleDescription");
			this.btnExistingPOConvert.AccessibleName = resources.GetString("btnExistingPOConvert.AccessibleName");
			this.btnExistingPOConvert.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnExistingPOConvert.Anchor")));
			this.btnExistingPOConvert.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExistingPOConvert.BackgroundImage")));
			this.btnExistingPOConvert.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnExistingPOConvert.Dock")));
			this.btnExistingPOConvert.Enabled = ((bool)(resources.GetObject("btnExistingPOConvert.Enabled")));
			this.btnExistingPOConvert.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnExistingPOConvert.FlatStyle")));
			this.btnExistingPOConvert.Font = ((System.Drawing.Font)(resources.GetObject("btnExistingPOConvert.Font")));
			this.btnExistingPOConvert.Image = ((System.Drawing.Image)(resources.GetObject("btnExistingPOConvert.Image")));
			this.btnExistingPOConvert.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnExistingPOConvert.ImageAlign")));
			this.btnExistingPOConvert.ImageIndex = ((int)(resources.GetObject("btnExistingPOConvert.ImageIndex")));
			this.btnExistingPOConvert.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnExistingPOConvert.ImeMode")));
			this.btnExistingPOConvert.Location = ((System.Drawing.Point)(resources.GetObject("btnExistingPOConvert.Location")));
			this.btnExistingPOConvert.Name = "btnExistingPOConvert";
			this.btnExistingPOConvert.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnExistingPOConvert.RightToLeft")));
			this.btnExistingPOConvert.Size = ((System.Drawing.Size)(resources.GetObject("btnExistingPOConvert.Size")));
			this.btnExistingPOConvert.TabIndex = ((int)(resources.GetObject("btnExistingPOConvert.TabIndex")));
			this.btnExistingPOConvert.Text = resources.GetString("btnExistingPOConvert.Text");
			this.btnExistingPOConvert.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnExistingPOConvert.TextAlign")));
			this.btnExistingPOConvert.Visible = ((bool)(resources.GetObject("btnExistingPOConvert.Visible")));
			this.btnExistingPOConvert.Click += new System.EventHandler(this.btnExistingPOConvert_Click);
			// 
			// btnExistingWOConvert
			// 
			this.btnExistingWOConvert.AccessibleDescription = resources.GetString("btnExistingWOConvert.AccessibleDescription");
			this.btnExistingWOConvert.AccessibleName = resources.GetString("btnExistingWOConvert.AccessibleName");
			this.btnExistingWOConvert.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnExistingWOConvert.Anchor")));
			this.btnExistingWOConvert.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExistingWOConvert.BackgroundImage")));
			this.btnExistingWOConvert.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnExistingWOConvert.Dock")));
			this.btnExistingWOConvert.Enabled = ((bool)(resources.GetObject("btnExistingWOConvert.Enabled")));
			this.btnExistingWOConvert.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnExistingWOConvert.FlatStyle")));
			this.btnExistingWOConvert.Font = ((System.Drawing.Font)(resources.GetObject("btnExistingWOConvert.Font")));
			this.btnExistingWOConvert.Image = ((System.Drawing.Image)(resources.GetObject("btnExistingWOConvert.Image")));
			this.btnExistingWOConvert.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnExistingWOConvert.ImageAlign")));
			this.btnExistingWOConvert.ImageIndex = ((int)(resources.GetObject("btnExistingWOConvert.ImageIndex")));
			this.btnExistingWOConvert.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnExistingWOConvert.ImeMode")));
			this.btnExistingWOConvert.Location = ((System.Drawing.Point)(resources.GetObject("btnExistingWOConvert.Location")));
			this.btnExistingWOConvert.Name = "btnExistingWOConvert";
			this.btnExistingWOConvert.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnExistingWOConvert.RightToLeft")));
			this.btnExistingWOConvert.Size = ((System.Drawing.Size)(resources.GetObject("btnExistingWOConvert.Size")));
			this.btnExistingWOConvert.TabIndex = ((int)(resources.GetObject("btnExistingWOConvert.TabIndex")));
			this.btnExistingWOConvert.Text = resources.GetString("btnExistingWOConvert.Text");
			this.btnExistingWOConvert.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnExistingWOConvert.TextAlign")));
			this.btnExistingWOConvert.Visible = ((bool)(resources.GetObject("btnExistingWOConvert.Visible")));
			this.btnExistingWOConvert.Click += new System.EventHandler(this.btnExistingWOConvert_Click);
			// 
			// dtmDate
			// 
			this.dtmDate.AcceptsEscape = ((bool)(resources.GetObject("dtmDate.AcceptsEscape")));
			this.dtmDate.AccessibleDescription = resources.GetString("dtmDate.AccessibleDescription");
			this.dtmDate.AccessibleName = resources.GetString("dtmDate.AccessibleName");
			this.dtmDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dtmDate.Anchor")));
			this.dtmDate.AutoSize = ((bool)(resources.GetObject("dtmDate.AutoSize")));
			this.dtmDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmDate.BackgroundImage")));
			this.dtmDate.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("dtmDate.BorderStyle")));
			// 
			// dtmDate.Calendar
			// 
			this.dtmDate.Calendar.AccessibleDescription = resources.GetString("dtmDate.Calendar.AccessibleDescription");
			this.dtmDate.Calendar.AccessibleName = resources.GetString("dtmDate.Calendar.AccessibleName");
			this.dtmDate.Calendar.AnnuallyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmDate.Calendar.AnnuallyBoldedDates")));
			this.dtmDate.Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dtmDate.Calendar.BackgroundImage")));
			this.dtmDate.Calendar.BoldedDates = ((System.DateTime[])(resources.GetObject("dtmDate.Calendar.BoldedDates")));
			this.dtmDate.Calendar.CalendarDimensions = ((System.Drawing.Size)(resources.GetObject("dtmDate.Calendar.CalendarDimensions")));
			this.dtmDate.Calendar.Enabled = ((bool)(resources.GetObject("dtmDate.Calendar.Enabled")));
			this.dtmDate.Calendar.FirstDayOfWeek = ((System.Windows.Forms.Day)(resources.GetObject("dtmDate.Calendar.FirstDayOfWeek")));
			this.dtmDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmDate.Calendar.Font")));
			this.dtmDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmDate.Calendar.ImeMode")));
			this.dtmDate.Calendar.MonthlyBoldedDates = ((System.DateTime[])(resources.GetObject("dtmDate.Calendar.MonthlyBoldedDates")));
			this.dtmDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmDate.Calendar.RightToLeft")));
			this.dtmDate.Calendar.ShowClearButton = ((bool)(resources.GetObject("dtmDate.Calendar.ShowClearButton")));
			this.dtmDate.Calendar.ShowTodayButton = ((bool)(resources.GetObject("dtmDate.Calendar.ShowTodayButton")));
			this.dtmDate.Calendar.ShowWeekNumbers = ((bool)(resources.GetObject("dtmDate.Calendar.ShowWeekNumbers")));
			this.dtmDate.CaseSensitive = ((bool)(resources.GetObject("dtmDate.CaseSensitive")));
			this.dtmDate.Culture = ((int)(resources.GetObject("dtmDate.Culture")));
			this.dtmDate.CurrentTimeZone = ((bool)(resources.GetObject("dtmDate.CurrentTimeZone")));
			this.dtmDate.CustomFormat = resources.GetString("dtmDate.CustomFormat");
			this.dtmDate.DaylightTimeAdjustment = ((C1.Win.C1Input.DaylightTimeAdjustmentEnum)(resources.GetObject("dtmDate.DaylightTimeAdjustment")));
			this.dtmDate.DisplayFormat.CustomFormat = resources.GetString("dtmDate.DisplayFormat.CustomFormat");
			this.dtmDate.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmDate.DisplayFormat.FormatType")));
			this.dtmDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmDate.DisplayFormat.Inherit")));
			this.dtmDate.DisplayFormat.NullText = resources.GetString("dtmDate.DisplayFormat.NullText");
			this.dtmDate.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("dtmDate.DisplayFormat.TrimEnd")));
			this.dtmDate.DisplayFormat.TrimStart = ((bool)(resources.GetObject("dtmDate.DisplayFormat.TrimStart")));
			this.dtmDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dtmDate.Dock")));
			this.dtmDate.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("dtmDate.DropDownFormAlign")));
			this.dtmDate.EditFormat.CustomFormat = resources.GetString("dtmDate.EditFormat.CustomFormat");
			this.dtmDate.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmDate.EditFormat.FormatType")));
			this.dtmDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmDate.EditFormat.Inherit")));
			this.dtmDate.EditFormat.NullText = resources.GetString("dtmDate.EditFormat.NullText");
			this.dtmDate.EditFormat.TrimEnd = ((bool)(resources.GetObject("dtmDate.EditFormat.TrimEnd")));
			this.dtmDate.EditFormat.TrimStart = ((bool)(resources.GetObject("dtmDate.EditFormat.TrimStart")));
			this.dtmDate.EditMask = resources.GetString("dtmDate.EditMask");
			this.dtmDate.EmptyAsNull = ((bool)(resources.GetObject("dtmDate.EmptyAsNull")));
			this.dtmDate.Enabled = ((bool)(resources.GetObject("dtmDate.Enabled")));
			this.dtmDate.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("dtmDate.ErrorInfo.BeepOnError")));
			this.dtmDate.ErrorInfo.ErrorMessage = resources.GetString("dtmDate.ErrorInfo.ErrorMessage");
			this.dtmDate.ErrorInfo.ErrorMessageCaption = resources.GetString("dtmDate.ErrorInfo.ErrorMessageCaption");
			this.dtmDate.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("dtmDate.ErrorInfo.ShowErrorMessage")));
			this.dtmDate.ErrorInfo.ValueOnError = ((object)(resources.GetObject("dtmDate.ErrorInfo.ValueOnError")));
			this.dtmDate.Font = ((System.Drawing.Font)(resources.GetObject("dtmDate.Font")));
			this.dtmDate.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmDate.FormatType")));
			this.dtmDate.GapHeight = ((int)(resources.GetObject("dtmDate.GapHeight")));
			this.dtmDate.GMTOffset = ((System.TimeSpan)(resources.GetObject("dtmDate.GMTOffset")));
			this.dtmDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmDate.ImeMode")));
			this.dtmDate.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("dtmDate.InitialSelection")));
			this.dtmDate.Location = ((System.Drawing.Point)(resources.GetObject("dtmDate.Location")));
			this.dtmDate.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("dtmDate.MaskInfo.AutoTabWhenFilled")));
			this.dtmDate.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("dtmDate.MaskInfo.CaseSensitive")));
			this.dtmDate.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("dtmDate.MaskInfo.CopyWithLiterals")));
			this.dtmDate.MaskInfo.EditMask = resources.GetString("dtmDate.MaskInfo.EditMask");
			this.dtmDate.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmDate.MaskInfo.EmptyAsNull")));
			this.dtmDate.MaskInfo.ErrorMessage = resources.GetString("dtmDate.MaskInfo.ErrorMessage");
			this.dtmDate.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("dtmDate.MaskInfo.Inherit")));
			this.dtmDate.MaskInfo.PromptChar = ((char)(resources.GetObject("dtmDate.MaskInfo.PromptChar")));
			this.dtmDate.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("dtmDate.MaskInfo.ShowLiterals")));
			this.dtmDate.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("dtmDate.MaskInfo.StoredEmptyChar")));
			this.dtmDate.MaxLength = ((int)(resources.GetObject("dtmDate.MaxLength")));
			this.dtmDate.Name = "dtmDate";
			this.dtmDate.NullText = resources.GetString("dtmDate.NullText");
			this.dtmDate.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("dtmDate.ParseInfo.CaseSensitive")));
			this.dtmDate.ParseInfo.CustomFormat = resources.GetString("dtmDate.ParseInfo.CustomFormat");
			this.dtmDate.ParseInfo.DateTimeStyle = ((C1.Win.C1Input.DateTimeStyleFlags)(resources.GetObject("dtmDate.ParseInfo.DateTimeStyle")));
			this.dtmDate.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("dtmDate.ParseInfo.EmptyAsNull")));
			this.dtmDate.ParseInfo.ErrorMessage = resources.GetString("dtmDate.ParseInfo.ErrorMessage");
			this.dtmDate.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("dtmDate.ParseInfo.FormatType")));
			this.dtmDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmDate.ParseInfo.Inherit")));
			this.dtmDate.ParseInfo.NullText = resources.GetString("dtmDate.ParseInfo.NullText");
			this.dtmDate.ParseInfo.TrimEnd = ((bool)(resources.GetObject("dtmDate.ParseInfo.TrimEnd")));
			this.dtmDate.ParseInfo.TrimStart = ((bool)(resources.GetObject("dtmDate.ParseInfo.TrimStart")));
			this.dtmDate.PasswordChar = ((char)(resources.GetObject("dtmDate.PasswordChar")));
			this.dtmDate.PostValidation.CaseSensitive = ((bool)(resources.GetObject("dtmDate.PostValidation.CaseSensitive")));
			this.dtmDate.PostValidation.ErrorMessage = resources.GetString("dtmDate.PostValidation.ErrorMessage");
			this.dtmDate.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("dtmDate.PostValidation.Inherit")));
			this.dtmDate.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("dtmDate.PostValidation.Validation")));
			this.dtmDate.PostValidation.Values = ((System.Array)(resources.GetObject("dtmDate.PostValidation.Values")));
			this.dtmDate.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("dtmDate.PostValidation.ValuesExcluded")));
			this.dtmDate.PreValidation.CaseSensitive = ((bool)(resources.GetObject("dtmDate.PreValidation.CaseSensitive")));
			this.dtmDate.PreValidation.ErrorMessage = resources.GetString("dtmDate.PreValidation.ErrorMessage");
			this.dtmDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmDate.PreValidation.Inherit")));
			this.dtmDate.PreValidation.ItemSeparator = resources.GetString("dtmDate.PreValidation.ItemSeparator");
			this.dtmDate.PreValidation.PatternString = resources.GetString("dtmDate.PreValidation.PatternString");
			this.dtmDate.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("dtmDate.PreValidation.RegexOptions")));
			this.dtmDate.PreValidation.TrimEnd = ((bool)(resources.GetObject("dtmDate.PreValidation.TrimEnd")));
			this.dtmDate.PreValidation.TrimStart = ((bool)(resources.GetObject("dtmDate.PreValidation.TrimStart")));
			this.dtmDate.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("dtmDate.PreValidation.Validation")));
			this.dtmDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmDate.RightToLeft")));
			this.dtmDate.ShowFocusRectangle = ((bool)(resources.GetObject("dtmDate.ShowFocusRectangle")));
			this.dtmDate.Size = ((System.Drawing.Size)(resources.GetObject("dtmDate.Size")));
			this.dtmDate.TabIndex = ((int)(resources.GetObject("dtmDate.TabIndex")));
			this.dtmDate.Tag = ((object)(resources.GetObject("dtmDate.Tag")));
			this.dtmDate.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("dtmDate.TextAlign")));
			this.dtmDate.TrimEnd = ((bool)(resources.GetObject("dtmDate.TrimEnd")));
			this.dtmDate.TrimStart = ((bool)(resources.GetObject("dtmDate.TrimStart")));
			this.dtmDate.UserCultureOverride = ((bool)(resources.GetObject("dtmDate.UserCultureOverride")));
			this.dtmDate.Value = ((object)(resources.GetObject("dtmDate.Value")));
			this.dtmDate.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("dtmDate.VerticalAlign")));
			this.dtmDate.Visible = ((bool)(resources.GetObject("dtmDate.Visible")));
			this.dtmDate.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("dtmDate.VisibleButtons")));
			this.dtmDate.VisibleChanged += new System.EventHandler(this.dtmDate_VisibleChanged);
			this.dtmDate.DropDownOpened += new System.EventHandler(this.dtmDate_DropDownOpened);
			this.dtmDate.DropDownClosed += new DropDownClosedEventHandler(this.dtmDate_DropDownClosed);
			// 
			// numQuantity
			// 
			this.numQuantity.AcceptsEscape = ((bool)(resources.GetObject("numQuantity.AcceptsEscape")));
			this.numQuantity.AccessibleDescription = resources.GetString("numQuantity.AccessibleDescription");
			this.numQuantity.AccessibleName = resources.GetString("numQuantity.AccessibleName");
			this.numQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numQuantity.Anchor")));
			this.numQuantity.AutoSize = ((bool)(resources.GetObject("numQuantity.AutoSize")));
			this.numQuantity.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("numQuantity.BackgroundImage")));
			this.numQuantity.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("numQuantity.BorderStyle")));
			// 
			// numQuantity.Calculator
			// 
			this.numQuantity.Calculator.AccessibleDescription = resources.GetString("numQuantity.Calculator.AccessibleDescription");
			this.numQuantity.Calculator.AccessibleName = resources.GetString("numQuantity.Calculator.AccessibleName");
			this.numQuantity.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("numQuantity.Calculator.BackgroundImage")));
			this.numQuantity.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("numQuantity.Calculator.ButtonFlatStyle")));
			this.numQuantity.Calculator.DisplayFormat = resources.GetString("numQuantity.Calculator.DisplayFormat");
			this.numQuantity.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("numQuantity.Calculator.Font")));
			this.numQuantity.Calculator.FormatOnClose = ((bool)(resources.GetObject("numQuantity.Calculator.FormatOnClose")));
			this.numQuantity.Calculator.StoredFormat = resources.GetString("numQuantity.Calculator.StoredFormat");
			this.numQuantity.Calculator.UIStrings.Content = ((string[])(resources.GetObject("numQuantity.Calculator.UIStrings.Content")));
			this.numQuantity.CaseSensitive = ((bool)(resources.GetObject("numQuantity.CaseSensitive")));
			this.numQuantity.Culture = ((int)(resources.GetObject("numQuantity.Culture")));
			this.numQuantity.CustomFormat = resources.GetString("numQuantity.CustomFormat");
			this.numQuantity.DataType = ((System.Type)(resources.GetObject("numQuantity.DataType")));
			this.numQuantity.DisplayFormat.CustomFormat = resources.GetString("numQuantity.DisplayFormat.CustomFormat");
			this.numQuantity.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numQuantity.DisplayFormat.FormatType")));
			this.numQuantity.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("numQuantity.DisplayFormat.Inherit")));
			this.numQuantity.DisplayFormat.NullText = resources.GetString("numQuantity.DisplayFormat.NullText");
			this.numQuantity.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("numQuantity.DisplayFormat.TrimEnd")));
			this.numQuantity.DisplayFormat.TrimStart = ((bool)(resources.GetObject("numQuantity.DisplayFormat.TrimStart")));
			this.numQuantity.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numQuantity.Dock")));
			this.numQuantity.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("numQuantity.DropDownFormAlign")));
			this.numQuantity.EditFormat.CustomFormat = resources.GetString("numQuantity.EditFormat.CustomFormat");
			this.numQuantity.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numQuantity.EditFormat.FormatType")));
			this.numQuantity.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("numQuantity.EditFormat.Inherit")));
			this.numQuantity.EditFormat.NullText = resources.GetString("numQuantity.EditFormat.NullText");
			this.numQuantity.EditFormat.TrimEnd = ((bool)(resources.GetObject("numQuantity.EditFormat.TrimEnd")));
			this.numQuantity.EditFormat.TrimStart = ((bool)(resources.GetObject("numQuantity.EditFormat.TrimStart")));
			this.numQuantity.EditMask = resources.GetString("numQuantity.EditMask");
			this.numQuantity.EmptyAsNull = ((bool)(resources.GetObject("numQuantity.EmptyAsNull")));
			this.numQuantity.Enabled = ((bool)(resources.GetObject("numQuantity.Enabled")));
			this.numQuantity.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("numQuantity.ErrorInfo.BeepOnError")));
			this.numQuantity.ErrorInfo.ErrorMessage = resources.GetString("numQuantity.ErrorInfo.ErrorMessage");
			this.numQuantity.ErrorInfo.ErrorMessageCaption = resources.GetString("numQuantity.ErrorInfo.ErrorMessageCaption");
			this.numQuantity.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("numQuantity.ErrorInfo.ShowErrorMessage")));
			this.numQuantity.ErrorInfo.ValueOnError = ((object)(resources.GetObject("numQuantity.ErrorInfo.ValueOnError")));
			this.numQuantity.Font = ((System.Drawing.Font)(resources.GetObject("numQuantity.Font")));
			this.numQuantity.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numQuantity.FormatType")));
			this.numQuantity.GapHeight = ((int)(resources.GetObject("numQuantity.GapHeight")));
			this.numQuantity.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numQuantity.ImeMode")));
			this.numQuantity.Increment = ((object)(resources.GetObject("numQuantity.Increment")));
			this.numQuantity.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("numQuantity.InitialSelection")));
			this.numQuantity.Location = ((System.Drawing.Point)(resources.GetObject("numQuantity.Location")));
			this.numQuantity.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("numQuantity.MaskInfo.AutoTabWhenFilled")));
			this.numQuantity.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("numQuantity.MaskInfo.CaseSensitive")));
			this.numQuantity.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("numQuantity.MaskInfo.CopyWithLiterals")));
			this.numQuantity.MaskInfo.EditMask = resources.GetString("numQuantity.MaskInfo.EditMask");
			this.numQuantity.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("numQuantity.MaskInfo.EmptyAsNull")));
			this.numQuantity.MaskInfo.ErrorMessage = resources.GetString("numQuantity.MaskInfo.ErrorMessage");
			this.numQuantity.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("numQuantity.MaskInfo.Inherit")));
			this.numQuantity.MaskInfo.PromptChar = ((char)(resources.GetObject("numQuantity.MaskInfo.PromptChar")));
			this.numQuantity.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("numQuantity.MaskInfo.ShowLiterals")));
			this.numQuantity.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("numQuantity.MaskInfo.StoredEmptyChar")));
			this.numQuantity.MaxLength = ((int)(resources.GetObject("numQuantity.MaxLength")));
			this.numQuantity.Name = "numQuantity";
			this.numQuantity.NullText = resources.GetString("numQuantity.NullText");
			this.numQuantity.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.numQuantity.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("numQuantity.ParseInfo.CaseSensitive")));
			this.numQuantity.ParseInfo.CustomFormat = resources.GetString("numQuantity.ParseInfo.CustomFormat");
			this.numQuantity.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("numQuantity.ParseInfo.EmptyAsNull")));
			this.numQuantity.ParseInfo.ErrorMessage = resources.GetString("numQuantity.ParseInfo.ErrorMessage");
			this.numQuantity.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("numQuantity.ParseInfo.FormatType")));
			this.numQuantity.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("numQuantity.ParseInfo.Inherit")));
			this.numQuantity.ParseInfo.NullText = resources.GetString("numQuantity.ParseInfo.NullText");
			this.numQuantity.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("numQuantity.ParseInfo.NumberStyle")));
			this.numQuantity.ParseInfo.TrimEnd = ((bool)(resources.GetObject("numQuantity.ParseInfo.TrimEnd")));
			this.numQuantity.ParseInfo.TrimStart = ((bool)(resources.GetObject("numQuantity.ParseInfo.TrimStart")));
			this.numQuantity.PasswordChar = ((char)(resources.GetObject("numQuantity.PasswordChar")));
			this.numQuantity.PostValidation.CaseSensitive = ((bool)(resources.GetObject("numQuantity.PostValidation.CaseSensitive")));
			this.numQuantity.PostValidation.ErrorMessage = resources.GetString("numQuantity.PostValidation.ErrorMessage");
			this.numQuantity.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("numQuantity.PostValidation.Inherit")));
			this.numQuantity.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("numQuantity.PostValidation.Validation")));
			this.numQuantity.PostValidation.Values = ((System.Array)(resources.GetObject("numQuantity.PostValidation.Values")));
			this.numQuantity.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("numQuantity.PostValidation.ValuesExcluded")));
			this.numQuantity.PreValidation.CaseSensitive = ((bool)(resources.GetObject("numQuantity.PreValidation.CaseSensitive")));
			this.numQuantity.PreValidation.ErrorMessage = resources.GetString("numQuantity.PreValidation.ErrorMessage");
			this.numQuantity.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("numQuantity.PreValidation.Inherit")));
			this.numQuantity.PreValidation.ItemSeparator = resources.GetString("numQuantity.PreValidation.ItemSeparator");
			this.numQuantity.PreValidation.PatternString = resources.GetString("numQuantity.PreValidation.PatternString");
			this.numQuantity.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("numQuantity.PreValidation.RegexOptions")));
			this.numQuantity.PreValidation.TrimEnd = ((bool)(resources.GetObject("numQuantity.PreValidation.TrimEnd")));
			this.numQuantity.PreValidation.TrimStart = ((bool)(resources.GetObject("numQuantity.PreValidation.TrimStart")));
			this.numQuantity.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("numQuantity.PreValidation.Validation")));
			this.numQuantity.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numQuantity.RightToLeft")));
			this.numQuantity.ShowFocusRectangle = ((bool)(resources.GetObject("numQuantity.ShowFocusRectangle")));
			this.numQuantity.Size = ((System.Drawing.Size)(resources.GetObject("numQuantity.Size")));
			this.numQuantity.TabIndex = ((int)(resources.GetObject("numQuantity.TabIndex")));
			this.numQuantity.Tag = ((object)(resources.GetObject("numQuantity.Tag")));
			this.numQuantity.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numQuantity.TextAlign")));
			this.numQuantity.TrimEnd = ((bool)(resources.GetObject("numQuantity.TrimEnd")));
			this.numQuantity.TrimStart = ((bool)(resources.GetObject("numQuantity.TrimStart")));
			this.numQuantity.UserCultureOverride = ((bool)(resources.GetObject("numQuantity.UserCultureOverride")));
			this.numQuantity.Value = ((object)(resources.GetObject("numQuantity.Value")));
			this.numQuantity.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("numQuantity.VerticalAlign")));
			this.numQuantity.Visible = ((bool)(resources.GetObject("numQuantity.Visible")));
			this.numQuantity.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("numQuantity.VisibleButtons")));
			this.numQuantity.VisibleChanged += new System.EventHandler(this.numQuantity_VisibleChanged);
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
			// lblNumberOfRows
			// 
			this.lblNumberOfRows.AccessibleDescription = resources.GetString("lblNumberOfRows.AccessibleDescription");
			this.lblNumberOfRows.AccessibleName = resources.GetString("lblNumberOfRows.AccessibleName");
			this.lblNumberOfRows.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblNumberOfRows.Anchor")));
			this.lblNumberOfRows.AutoSize = ((bool)(resources.GetObject("lblNumberOfRows.AutoSize")));
			this.lblNumberOfRows.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblNumberOfRows.Dock")));
			this.lblNumberOfRows.Enabled = ((bool)(resources.GetObject("lblNumberOfRows.Enabled")));
			this.lblNumberOfRows.Font = ((System.Drawing.Font)(resources.GetObject("lblNumberOfRows.Font")));
			this.lblNumberOfRows.Image = ((System.Drawing.Image)(resources.GetObject("lblNumberOfRows.Image")));
			this.lblNumberOfRows.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblNumberOfRows.ImageAlign")));
			this.lblNumberOfRows.ImageIndex = ((int)(resources.GetObject("lblNumberOfRows.ImageIndex")));
			this.lblNumberOfRows.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblNumberOfRows.ImeMode")));
			this.lblNumberOfRows.Location = ((System.Drawing.Point)(resources.GetObject("lblNumberOfRows.Location")));
			this.lblNumberOfRows.Name = "lblNumberOfRows";
			this.lblNumberOfRows.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblNumberOfRows.RightToLeft")));
			this.lblNumberOfRows.Size = ((System.Drawing.Size)(resources.GetObject("lblNumberOfRows.Size")));
			this.lblNumberOfRows.TabIndex = ((int)(resources.GetObject("lblNumberOfRows.TabIndex")));
			this.lblNumberOfRows.Text = resources.GetString("lblNumberOfRows.Text");
			this.lblNumberOfRows.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblNumberOfRows.TextAlign")));
			this.lblNumberOfRows.Visible = ((bool)(resources.GetObject("lblNumberOfRows.Visible")));
			// 
			// txtNumRows
			// 
			this.txtNumRows.AcceptsEscape = ((bool)(resources.GetObject("txtNumRows.AcceptsEscape")));
			this.txtNumRows.AccessibleDescription = resources.GetString("txtNumRows.AccessibleDescription");
			this.txtNumRows.AccessibleName = resources.GetString("txtNumRows.AccessibleName");
			this.txtNumRows.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtNumRows.Anchor")));
			this.txtNumRows.AutoSize = ((bool)(resources.GetObject("txtNumRows.AutoSize")));
			this.txtNumRows.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtNumRows.BackgroundImage")));
			this.txtNumRows.BorderStyle = ((System.Windows.Forms.BorderStyle)(resources.GetObject("txtNumRows.BorderStyle")));
			// 
			// txtNumRows.Calculator
			// 
			this.txtNumRows.Calculator.AccessibleDescription = resources.GetString("txtNumRows.Calculator.AccessibleDescription");
			this.txtNumRows.Calculator.AccessibleName = resources.GetString("txtNumRows.Calculator.AccessibleName");
			this.txtNumRows.Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtNumRows.Calculator.BackgroundImage")));
			this.txtNumRows.Calculator.ButtonFlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("txtNumRows.Calculator.ButtonFlatStyle")));
			this.txtNumRows.Calculator.DisplayFormat = resources.GetString("txtNumRows.Calculator.DisplayFormat");
			this.txtNumRows.Calculator.Font = ((System.Drawing.Font)(resources.GetObject("txtNumRows.Calculator.Font")));
			this.txtNumRows.Calculator.FormatOnClose = ((bool)(resources.GetObject("txtNumRows.Calculator.FormatOnClose")));
			this.txtNumRows.Calculator.StoredFormat = resources.GetString("txtNumRows.Calculator.StoredFormat");
			this.txtNumRows.Calculator.UIStrings.Content = ((string[])(resources.GetObject("txtNumRows.Calculator.UIStrings.Content")));
			this.txtNumRows.CaseSensitive = ((bool)(resources.GetObject("txtNumRows.CaseSensitive")));
			this.txtNumRows.Culture = ((int)(resources.GetObject("txtNumRows.Culture")));
			this.txtNumRows.CustomFormat = resources.GetString("txtNumRows.CustomFormat");
			this.txtNumRows.DataType = ((System.Type)(resources.GetObject("txtNumRows.DataType")));
			this.txtNumRows.DisplayFormat.CustomFormat = resources.GetString("txtNumRows.DisplayFormat.CustomFormat");
			this.txtNumRows.DisplayFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtNumRows.DisplayFormat.FormatType")));
			this.txtNumRows.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtNumRows.DisplayFormat.Inherit")));
			this.txtNumRows.DisplayFormat.NullText = resources.GetString("txtNumRows.DisplayFormat.NullText");
			this.txtNumRows.DisplayFormat.TrimEnd = ((bool)(resources.GetObject("txtNumRows.DisplayFormat.TrimEnd")));
			this.txtNumRows.DisplayFormat.TrimStart = ((bool)(resources.GetObject("txtNumRows.DisplayFormat.TrimStart")));
			this.txtNumRows.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtNumRows.Dock")));
			this.txtNumRows.DropDownFormAlign = ((C1.Win.C1Input.DropDownFormAlignmentEnum)(resources.GetObject("txtNumRows.DropDownFormAlign")));
			this.txtNumRows.EditFormat.CustomFormat = resources.GetString("txtNumRows.EditFormat.CustomFormat");
			this.txtNumRows.EditFormat.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtNumRows.EditFormat.FormatType")));
			this.txtNumRows.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtNumRows.EditFormat.Inherit")));
			this.txtNumRows.EditFormat.NullText = resources.GetString("txtNumRows.EditFormat.NullText");
			this.txtNumRows.EditFormat.TrimEnd = ((bool)(resources.GetObject("txtNumRows.EditFormat.TrimEnd")));
			this.txtNumRows.EditFormat.TrimStart = ((bool)(resources.GetObject("txtNumRows.EditFormat.TrimStart")));
			this.txtNumRows.EditMask = resources.GetString("txtNumRows.EditMask");
			this.txtNumRows.EmptyAsNull = ((bool)(resources.GetObject("txtNumRows.EmptyAsNull")));
			this.txtNumRows.Enabled = ((bool)(resources.GetObject("txtNumRows.Enabled")));
			this.txtNumRows.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtNumRows.ErrorInfo.BeepOnError")));
			this.txtNumRows.ErrorInfo.ErrorMessage = resources.GetString("txtNumRows.ErrorInfo.ErrorMessage");
			this.txtNumRows.ErrorInfo.ErrorMessageCaption = resources.GetString("txtNumRows.ErrorInfo.ErrorMessageCaption");
			this.txtNumRows.ErrorInfo.ShowErrorMessage = ((bool)(resources.GetObject("txtNumRows.ErrorInfo.ShowErrorMessage")));
			this.txtNumRows.ErrorInfo.ValueOnError = ((object)(resources.GetObject("txtNumRows.ErrorInfo.ValueOnError")));
			this.txtNumRows.Font = ((System.Drawing.Font)(resources.GetObject("txtNumRows.Font")));
			this.txtNumRows.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtNumRows.FormatType")));
			this.txtNumRows.GapHeight = ((int)(resources.GetObject("txtNumRows.GapHeight")));
			this.txtNumRows.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtNumRows.ImeMode")));
			this.txtNumRows.Increment = ((object)(resources.GetObject("txtNumRows.Increment")));
			this.txtNumRows.InitialSelection = ((C1.Win.C1Input.InitialSelectionEnum)(resources.GetObject("txtNumRows.InitialSelection")));
			this.txtNumRows.Location = ((System.Drawing.Point)(resources.GetObject("txtNumRows.Location")));
			this.txtNumRows.MaskInfo.AutoTabWhenFilled = ((bool)(resources.GetObject("txtNumRows.MaskInfo.AutoTabWhenFilled")));
			this.txtNumRows.MaskInfo.CaseSensitive = ((bool)(resources.GetObject("txtNumRows.MaskInfo.CaseSensitive")));
			this.txtNumRows.MaskInfo.CopyWithLiterals = ((bool)(resources.GetObject("txtNumRows.MaskInfo.CopyWithLiterals")));
			this.txtNumRows.MaskInfo.EditMask = resources.GetString("txtNumRows.MaskInfo.EditMask");
			this.txtNumRows.MaskInfo.EmptyAsNull = ((bool)(resources.GetObject("txtNumRows.MaskInfo.EmptyAsNull")));
			this.txtNumRows.MaskInfo.ErrorMessage = resources.GetString("txtNumRows.MaskInfo.ErrorMessage");
			this.txtNumRows.MaskInfo.Inherit = ((C1.Win.C1Input.MaskInfoInheritFlags)(resources.GetObject("txtNumRows.MaskInfo.Inherit")));
			this.txtNumRows.MaskInfo.PromptChar = ((char)(resources.GetObject("txtNumRows.MaskInfo.PromptChar")));
			this.txtNumRows.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtNumRows.MaskInfo.ShowLiterals")));
			this.txtNumRows.MaskInfo.StoredEmptyChar = ((char)(resources.GetObject("txtNumRows.MaskInfo.StoredEmptyChar")));
			this.txtNumRows.MaxLength = ((int)(resources.GetObject("txtNumRows.MaxLength")));
			this.txtNumRows.Name = "txtNumRows";
			this.txtNumRows.NullText = resources.GetString("txtNumRows.NullText");
			this.txtNumRows.ParseInfo.CaseSensitive = ((bool)(resources.GetObject("txtNumRows.ParseInfo.CaseSensitive")));
			this.txtNumRows.ParseInfo.CustomFormat = resources.GetString("txtNumRows.ParseInfo.CustomFormat");
			this.txtNumRows.ParseInfo.EmptyAsNull = ((bool)(resources.GetObject("txtNumRows.ParseInfo.EmptyAsNull")));
			this.txtNumRows.ParseInfo.ErrorMessage = resources.GetString("txtNumRows.ParseInfo.ErrorMessage");
			this.txtNumRows.ParseInfo.FormatType = ((C1.Win.C1Input.FormatTypeEnum)(resources.GetObject("txtNumRows.ParseInfo.FormatType")));
			this.txtNumRows.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtNumRows.ParseInfo.Inherit")));
			this.txtNumRows.ParseInfo.NullText = resources.GetString("txtNumRows.ParseInfo.NullText");
			this.txtNumRows.ParseInfo.NumberStyle = ((C1.Win.C1Input.NumberStyleFlags)(resources.GetObject("txtNumRows.ParseInfo.NumberStyle")));
			this.txtNumRows.ParseInfo.TrimEnd = ((bool)(resources.GetObject("txtNumRows.ParseInfo.TrimEnd")));
			this.txtNumRows.ParseInfo.TrimStart = ((bool)(resources.GetObject("txtNumRows.ParseInfo.TrimStart")));
			this.txtNumRows.PasswordChar = ((char)(resources.GetObject("txtNumRows.PasswordChar")));
			this.txtNumRows.PostValidation.CaseSensitive = ((bool)(resources.GetObject("txtNumRows.PostValidation.CaseSensitive")));
			this.txtNumRows.PostValidation.ErrorMessage = resources.GetString("txtNumRows.PostValidation.ErrorMessage");
			this.txtNumRows.PostValidation.Inherit = ((C1.Win.C1Input.PostValidationInheritFlags)(resources.GetObject("txtNumRows.PostValidation.Inherit")));
			this.txtNumRows.PostValidation.Validation = ((C1.Win.C1Input.PostValidationTypeEnum)(resources.GetObject("txtNumRows.PostValidation.Validation")));
			this.txtNumRows.PostValidation.Values = ((System.Array)(resources.GetObject("txtNumRows.PostValidation.Values")));
			this.txtNumRows.PostValidation.ValuesExcluded = ((System.Array)(resources.GetObject("txtNumRows.PostValidation.ValuesExcluded")));
			this.txtNumRows.PreValidation.CaseSensitive = ((bool)(resources.GetObject("txtNumRows.PreValidation.CaseSensitive")));
			this.txtNumRows.PreValidation.ErrorMessage = resources.GetString("txtNumRows.PreValidation.ErrorMessage");
			this.txtNumRows.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtNumRows.PreValidation.Inherit")));
			this.txtNumRows.PreValidation.ItemSeparator = resources.GetString("txtNumRows.PreValidation.ItemSeparator");
			this.txtNumRows.PreValidation.PatternString = resources.GetString("txtNumRows.PreValidation.PatternString");
			this.txtNumRows.PreValidation.RegexOptions = ((C1.Win.C1Input.RegexOptionFlags)(resources.GetObject("txtNumRows.PreValidation.RegexOptions")));
			this.txtNumRows.PreValidation.TrimEnd = ((bool)(resources.GetObject("txtNumRows.PreValidation.TrimEnd")));
			this.txtNumRows.PreValidation.TrimStart = ((bool)(resources.GetObject("txtNumRows.PreValidation.TrimStart")));
			this.txtNumRows.PreValidation.Validation = ((C1.Win.C1Input.PreValidationTypeEnum)(resources.GetObject("txtNumRows.PreValidation.Validation")));
			this.txtNumRows.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtNumRows.RightToLeft")));
			this.txtNumRows.ShowFocusRectangle = ((bool)(resources.GetObject("txtNumRows.ShowFocusRectangle")));
			this.txtNumRows.Size = ((System.Drawing.Size)(resources.GetObject("txtNumRows.Size")));
			this.txtNumRows.TabIndex = ((int)(resources.GetObject("txtNumRows.TabIndex")));
			this.txtNumRows.Tag = ((object)(resources.GetObject("txtNumRows.Tag")));
			this.txtNumRows.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtNumRows.TextAlign")));
			this.txtNumRows.TrimEnd = ((bool)(resources.GetObject("txtNumRows.TrimEnd")));
			this.txtNumRows.TrimStart = ((bool)(resources.GetObject("txtNumRows.TrimStart")));
			this.txtNumRows.UserCultureOverride = ((bool)(resources.GetObject("txtNumRows.UserCultureOverride")));
			this.txtNumRows.Value = ((object)(resources.GetObject("txtNumRows.Value")));
			this.txtNumRows.VerticalAlign = ((C1.Win.C1Input.VerticalAlignEnum)(resources.GetObject("txtNumRows.VerticalAlign")));
			this.txtNumRows.Visible = ((bool)(resources.GetObject("txtNumRows.Visible")));
			this.txtNumRows.VisibleButtons = ((C1.Win.C1Input.DropDownControlButtonFlags)(resources.GetObject("txtNumRows.VisibleButtons")));
			// 
			// CPODataViewer
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
			this.Controls.Add(this.txtNumRows);
			this.Controls.Add(this.lblNumberOfRows);
			this.Controls.Add(this.btnExistingWOConvert);
			this.Controls.Add(this.btnExistingPOConvert);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnNewWOConvert);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnNewPOConvert);
			this.Controls.Add(this.chkSelectAll);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.GroupBox);
			this.Controls.Add(this.dtmDate);
			this.Controls.Add(this.numQuantity);
			this.Controls.Add(this.btnPrint);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "CPODataViewer";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CPODataViewer_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.CPODataViewer_Closing);
			this.Load += new System.EventHandler(this.CPODataViewer_Load);
			this.GroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmToDueDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmFromDueDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNumRows)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Methods
		
		/// <summary>
		/// Fill related data on controls when select Vendor
		/// </summary>
		private bool SelectVendor(bool pblnAlwaysShowDialog)
		{	
			const string VENDOR_VIEW = "v_Vendor";		

			//Call OpenSearchForm for selecting Vendor
			DataRowView drwResult = FormControlComponents.OpenSearchForm(VENDOR_VIEW, MST_PartyTable.CODE_FLD, txtVendor.Text, null, pblnAlwaysShowDialog);

			//If has Vendor matched searching condition, fill values to form's controls
			if(drwResult != null)
			{			
				txtVendor.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
				txtVendor.Tag  = drwResult[MST_PartyTable.PARTYID_FLD];
				
				//reset modify status
				txtVendor.Modified = false;				
			}				
			else if(!pblnAlwaysShowDialog)
			{
				txtVendor.Focus();
				return false;
			}

			//Return true = ok
			return true;			
		}
		
		/// <summary>
		/// Processing control get focus event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private void OnEnterControl(object sender, EventArgs e)
		{
			try
			{
				FormControlComponents.OnEnterControl(sender, e);
			}
			catch
			{}
		}
		
		/// <summary>
		/// Processing control get focus event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 15 June 2005
		/// </created>
		private void OnLeaveControl(object sender, EventArgs e)
		{
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
			}
			catch
			{}
		}

		private bool ValidateData()
		{
			try
			{
				//Check Cycle no
				if (FormControlComponents.CheckMandatory(txtCycle))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					txtCycle.Focus();				
					return false;
				}
				if (dgrdData.RowCount <= 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_AT_LEAST_RECORD_IN_GRID,MessageBoxIcon.Exclamation);
					dgrdData.Focus();
					return false;
				}
				//variable to indicate grid's row index
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					if(dgrdData[i, MTR_CPOTable.QUANTITY_FLD].Equals(DBNull.Value)
						|| dgrdData[i, MTR_CPOTable.QUANTITY_FLD].ToString().Equals(string.Empty)
						|| dgrdData[i, MTR_CPOTable.QUANTITY_FLD].ToString().Equals(ZERO_STRING)
						|| !FormControlComponents.IsPositiveNumeric(dgrdData[i, MTR_CPOTable.QUANTITY_FLD].ToString())
						)
					{
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MTR_CPOTable.QUANTITY_FLD]);
						dgrdData.Focus();
						return false;
					}
					if(dgrdData[i, PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].ToString() != string.Empty)
					{
						decimal decSafetyStockQty = 0;
						try
						{
							decSafetyStockQty = decimal.Parse(dgrdData[i, PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].ToString());
						}
						catch
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_OUTPROCESSING_INVALID_NUMBER, MessageBoxIcon.Exclamation);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT]);
							dgrdData.Focus();
							return false;

						}
						if (decSafetyStockQty > decimal.Parse(dgrdData[i, MTR_CPOTable.QUANTITY_FLD].ToString()))
						{
							// Please input Order quantity field for each records.
							string[] strParam = new string[2];
							strParam[0] = "Quantity";
							strParam[1] = "Safety Stock Quantity";
							PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Exclamation, strParam);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT]);
							dgrdData.Focus();
							return false;
						}
					}
					if(dgrdData[i, MTR_CPOTable.STARTDATE_FLD].ToString().Trim().Equals(string.Empty)
						|| dgrdData[i, MTR_CPOTable.STARTDATE_FLD].Equals(DBNull.Value)
						)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MTR_CPOTable.STARTDATE_FLD]);
						dgrdData.Focus();
						return false;
					}

					if(dgrdData[i, MTR_CPOTable.DUEDATE_FLD].ToString().Trim().Equals(string.Empty)
						|| dgrdData[i, MTR_CPOTable.DUEDATE_FLD].Equals(DBNull.Value)
						)
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MTR_CPOTable.DUEDATE_FLD]);
						dgrdData.Focus();
						return false;
					}

					if(DateTime.Parse(dgrdData[i, MTR_CPOTable.STARTDATE_FLD].ToString()) > DateTime.Parse(dgrdData[i, MTR_CPOTable.DUEDATE_FLD].ToString()))
					{
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MESSAGE_ENDDATE_GREATER_THAN_BEGINDATE, MessageBoxIcon.Exclamation);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MTR_CPOTable.DUEDATE_FLD]);
						dgrdData.Focus();
						return false;
					}
					if(cboPlanType.Text == lblDCP.Text)
					if(dgrdData[i, PRO_ShiftTable.SHIFTDESC_FLD].ToString() ==  string.Empty)
					{
						// Please input Item field for each records.
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD]);
						dgrdData.Focus();
						return false;
					}
					
				}
				int intRowIndex = -1;
				if(cboPlanType.Text == lblDCP.Text)
				foreach (DataRow drowRow in dtbCPODetail.Rows)
				{
					intRowIndex++;
					//Check data has just entered
					if(drowRow.RowState == DataRowState.Added) 
					{
						//Items
						if(drowRow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].ToString().Trim().Equals(string.Empty)
							|| drowRow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Equals(DBNull.Value))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
							dgrdData.Row = intRowIndex;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
							dgrdData.Focus();
							return false;
						}

						if(drowRow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].ToString().Trim().Equals(string.Empty)
							|| drowRow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Equals(DBNull.Value))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
							dgrdData.Row = intRowIndex;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD]);
							dgrdData.Focus();
							return false;
						}
						//Shift
						if(drowRow[PRO_ShiftTable.SHIFTDESC_FLD].ToString().Trim().Equals(string.Empty)
							|| drowRow[PRO_ShiftTable.SHIFTDESC_FLD].Equals(DBNull.Value))
						{
							PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
							dgrdData.Row = intRowIndex;
							dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD]);
							dgrdData.Focus();
							return false;
						}	
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Buid condition
		/// </summary>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 10 Aug 2005
		/// </created>
		private bool BuildConditionHashTable(out Hashtable phtbCondition, out string pstrExtraCondition)
		{
			const string SQL_DATETIME_FORMAT = "yyyy-MM-dd";
			const string ENDDAY_TIME_VALUE = " 23:59:59";

			phtbCondition = new Hashtable();
			pstrExtraCondition = "1=1";
				
			//Check if user does not select plan type
			if(cboCCN.SelectedIndex <0)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_CCN);
				cboCCN.Focus();
				return false;
			}
				
			//Check if user does not select plan type
			if((cboPlanType.SelectedIndex == 0) || (cboPlanType.Text.Trim() == string.Empty))
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_PLAN_TYPE);
				cboPlanType.Focus();
				return false;
			}

			//Check if user does not select Cycle
			if(txtCycle.Text.Trim() == string.Empty || txtCycle.Tag.ToString().Equals(ZERO_STRING))
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_CYCLE);
				txtCycle.Focus();
				return false;
			}

			//Check if user does not select Master Location
			if((txtMasLoc.Text.Trim() == string.Empty|| txtMasLoc.Tag.ToString().Equals(ZERO_STRING)) && (cboPlanType.Text.Trim() != lblDCP.Text.Trim()))
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_MASLOC);
				txtMasLoc.Focus();
				return false;
			}

			if(!dtmFromDueDate.ValueIsDbNull || !dtmFromDueDate.Text.Equals(string.Empty))
			{
				if(dtmToDueDate.ValueIsDbNull || dtmToDueDate.Text.Equals(string.Empty))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Exclamation, new string[]{lblToDueDate.Text});
					dtmToDueDate.Focus();
					return false;
				}
				else
				{
					DateTime dtmFrom = DateTime.Parse(dtmFromDueDate.Value.ToString());
					DateTime dtmTo = DateTime.Parse(dtmToDueDate.Value.ToString());
							
					pstrExtraCondition += " AND " + MTR_CPOTable.DUEDATE_FLD + " >='" + dtmFrom.ToString(SQL_DATETIME_FORMAT);
					pstrExtraCondition += "' AND " + MTR_CPOTable.DUEDATE_FLD + "<= '" + dtmTo.ToString(SQL_DATETIME_FORMAT) + ENDDAY_TIME_VALUE + "'";
				}
			}				

			//Add CCN, Mas Loc, IsMPS condition to filter
			if (cboPlanType.Text.Trim() != lblDCP.Text.Trim())
			{
				phtbCondition.Add(MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.CCNID_FLD, cboCCN.SelectedValue);
				phtbCondition.Add(MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.MASTERLOCATIONID_FLD, txtMasLoc.Tag);
			}
			else
				phtbCondition.Add(PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.CCNID_FLD, cboCCN.SelectedValue);

			phtbCondition.Add(PRO_ProductionLineTable.TABLE_NAME + "." + PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, txtProductionLine.Tag);

			if (cboPlanType.Text.Trim() != lblDCP.Text.Trim())
				phtbCondition.Add(MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.ISMPS_FLD, cboPlanType.SelectedIndex - 1);
				
			if(cboPlanType.Text == PlanTypeEnum.MRP.ToString()) //MRP
				phtbCondition.Add(MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD, txtCycle.Tag);

			if (cboPlanType.Text == PlanTypeEnum.MPS.ToString()) //MPS
				phtbCondition.Add(MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD, txtCycle.Tag);

			if (cboPlanType.Text.Trim() == lblDCP.Text.ToString())
				phtbCondition.Add(PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD, txtCycle.Tag);
				
			if(!txtCategory.Tag.ToString().Equals(ZERO_STRING))
				phtbCondition.Add(ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CATEGORYID_FLD, txtCategory.Tag);			
				
			if(txtVendor.Text.Trim() != string.Empty && txtVendor.Tag != null)
				phtbCondition.Add(MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.PARTYID_FLD, txtVendor.Tag);
				
			if (txtPartNumber.Text != string.Empty)
				phtbCondition.Add(ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD, txtPartNumber.Tag);

			return true;
		}

		/// <summary>
		/// Reformat data grid
		/// </summary>
		/// <author> Tran Quoc Tuan </author>
		/// <created date> 
		/// 10 Aug 2005
		/// </created>
		private void FormatDataGrid()
		{
            //Restore layout
            FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);

            dgrdData.AllowAddNew = false;
            dgrdData.AllowDelete = false;
            dgrdData.AllowUpdate = true;

            //Change presentation
            dgrdData.Columns[MTR_CPOTable.CONVERTED_FLD].ValueItems.Presentation = PresentationEnum.CheckBox;
            dgrdData.Columns[MTR_CPODS.SELECT_COLUMN].ValueItems.Presentation = PresentationEnum.CheckBox;
            dgrdData.Columns[PRO_DCPResultDetailTable.ISMANUAL_FLD].ValueItems.Presentation = PresentationEnum.CheckBox;

            //Lock all columns
            foreach (C1DisplayColumn col in dgrdData.Splits[0].DisplayColumns)
            {
                col.Locked = true;
            }

            //unlock for editing
            dgrdData.Splits[0].DisplayColumns[MTR_CPODS.SELECT_COLUMN].Locked = false;
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.QUANTITY_FLD].Locked = false;
            dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].Locked = false;
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.STARTDATE_FLD].Locked = false;
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.DUEDATE_FLD].Locked = false;
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.DUEDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.STARTDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
            //For selecting Items
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Locked = false;
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Locked = false;
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Button = true;
            dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Button = true;
            dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Button = true;
            dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Locked = false;

            //Column button
            dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Button = true;
            dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ButtonText = true;
            //dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ButtonAlways = true;

            dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].Button = true;
            dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].ButtonText = true;
            //dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].ButtonAlways = true;

            //Change display format
            dgrdData.Columns[MTR_CPOTable.QUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;

            dgrdData.Columns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Far;
            dgrdData.Columns[MTR_CPOTable.STARTDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
            dgrdData.Columns[MTR_CPOTable.DUEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;

            //Assign editor to Date Columns
            dgrdData.Columns[MTR_CPOTable.STARTDATE_FLD].Editor = dtmDate;
            dgrdData.Columns[MTR_CPOTable.DUEDATE_FLD].Editor = dtmDate;
            dgrdData.Columns[MTR_CPOTable.QUANTITY_FLD].Editor = numQuantity;
            if (cboPlanType.SelectedItem.ToString() == lblDCP.Text.Trim()) //Change to DCP mode
            {
                txtMasLoc.Text = string.Empty;

                #region HACK: Trada 21-04-2006
                //add some new columns
                //Shift column
                dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Visible = true;
                //Safety Stock Qty Column
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].Visible = true;
                //IsManual Column
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Visible = true;
                //config new columns
                dgrdData.Columns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Far;
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.ISMANUAL_FLD].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;

                dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Button = true;
                dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Button = true;
                dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Button = true;

                dgrdData.Columns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Visible = true;
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Locked = false;

                #endregion END: Trada 21-04-2006
            }
            else //Change to default mode
            {
                #region HACK: Trada 21-04-2006
                dgrdData.AllowAddNew = false;
                //invisible some columns
                //Shift column
                dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Visible = false;
                //Safety Stock Qty Column
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].Visible = false;
                //IsManual Column
                dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Visible = false;
                try
                {
                    dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Visible = false;
                    dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Locked = true;
                }
                catch { }
                #endregion END: Trada 21-04-2006
            }
            //HACKED by Tuan TQ. 22 Dec, 2005. Apply proposal no. 3159
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.CPOID_FLD].Visible = false;
            dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.CPOID_FLD].AllowSizing = false;
		}

		/// <summary>
		/// Build structure of MTR_CPO table for binding to grid
		/// </summary>
		/// <remarks>
		/// Structure of this table based on struct which be returned by calling
		/// MTR_CPODSDS.Search() method.
		/// So we should keep them always are identical.
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildCPODetailTable()
		{
			try
			{
				//Create table
				DataTable dtbDetail = new DataTable(MTR_CPOTable.TABLE_NAME);
				//Add columns

				//HACKED by Tuan TQ. 22 Dec, 2005. Apply proposal no. 3159
				dtbDetail.Columns.Add(MTR_CPODS.LINE_NUMBER_COLUMN, typeof(System.Int32));
				//End
				dtbDetail.Columns.Add(MTR_CPODS.SELECT_COLUMN, typeof(System.Boolean));
				dtbDetail.Columns.Add(MTR_CPOTable.CONVERTED_FLD, typeof(System.Boolean));
				dtbDetail.Columns.Add(MTR_CPOTable.CPOID_FLD, typeof(System.Int32));				
				
				dtbDetail.Columns.Add(ITM_CategoryTable.CATEGORYID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD, typeof(System.String));			
				
				dtbDetail.Columns.Add(ITM_ProductTable.PRODUCTID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD, typeof(System.String));
				dtbDetail.Columns.Add(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD, typeof(System.String));
				dtbDetail.Columns.Add(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD, typeof(System.String));
				
				dtbDetail.Columns.Add(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD, typeof(System.String));

				//HACK: added by Tuan TQ. 17 Feb, 2006
				dtbDetail.Columns.Add(MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD, typeof(System.String));
				//End hack

				dtbDetail.Columns.Add(MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD, typeof(System.String));
				
				dtbDetail.Columns.Add(MTR_CPOTable.QUANTITY_FLD, typeof(System.Double));
				dtbDetail.Columns.Add(MTR_CPOTable.STARTDATE_FLD, typeof(System.DateTime));
				dtbDetail.Columns.Add(MTR_CPOTable.DUEDATE_FLD, typeof(System.DateTime));
				dtbDetail.Columns.Add(PRO_ShiftTable.SHIFTDESC_FLD, typeof(System.String));
				dtbDetail.Columns.Add(PRO_ShiftTable.SHIFTID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(PRO_DCPResultDetailTable.TOTALSECOND_FLD, typeof(decimal));
				dtbDetail.Columns.Add(PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD, typeof(System.String));
				dtbDetail.Columns.Add(PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD, typeof(System.String));
				dtbDetail.Columns.Add(PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD, typeof(System.Int32));
				
				dtbDetail.Columns.Add(MTR_CPOTable.CCNID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(MTR_CPOTable.MASTERLOCATIONID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(MTR_CPOTable.ISMPS_FLD, typeof(System.Boolean));
				
				dtbDetail.Columns.Add(MTR_CPOTable.REFMASTERID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(MTR_CPOTable.REFTYPE_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(MTR_CPOTable.NETAVAILABLEQUANTITY_FLD, typeof(System.Double));
				dtbDetail.Columns.Add(MTR_CPOTable.STOCKUMID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(MTR_CPOTable.POGENERATEDID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(MTR_CPOTable.WOGENERATEDID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD, typeof(System.Int32));
				dtbDetail.Columns.Add(MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD, typeof(System.Int32));
				
				dtbDetail.Columns.Add(PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT, typeof(System.Double));
				dtbDetail.Columns.Add(PRO_DCPResultDetailTable.ISMANUAL_FLD, typeof(System.Double));
				dtbDetail.Columns.Add(ITM_ProductTable.LTVARIABLETIME_FLD, typeof(decimal));

				dtbDetail.Columns.Add("FixLT", typeof(System.Decimal));

				return dtbDetail;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Clear all condition information on the form
		/// </summary>
		private void ClearSearchingCondition()
		{
			try
			{
				txtCategory.Text = string.Empty;
				txtCategory.Tag = ZERO_STRING;

				txtCycle.Text = string.Empty;
				txtCycle.Tag = ZERO_STRING;

				#region  DEL Trada 22-12-2005

				//				txtMasLoc.Text = string.Empty;
				//				txtMasLoc.Tag = ZERO_STRING;

				#endregion 

				txtProductionLine.Text = string.Empty;
				txtProductionLine.Tag = ZERO_STRING;
								
				txtPartNumber.Text = string.Empty;
				txtPartNumber.Tag = ZERO_STRING;

				txtPartName.Text = string.Empty;
				txtRevision.Text = string.Empty;

				txtVendor.Text = string.Empty;

				dtmFromDueDate.Value = DBNull.Value;
				dtmToDueDate.Value = DBNull.Value;

				cboPlanType.Focus();				
				btnSave.Enabled = false;				
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		///<summary>	
		/// Clear item information on the form
		/// </summary>
		private void ClearItemInfo()
		{
			try
			{
				txtPartNumber.Text = string.Empty;
				txtPartNumber.Tag = ZERO_STRING;

				txtPartName.Text = string.Empty;
				txtRevision.Text = string.Empty;
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}	
		}

		/// <summary>
		/// Fill related data on controls when select PartName
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectPartName(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try
			{
				string strWhere = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CCNID_FLD + "=" + cboCCN.SelectedValue;
				if (txtCategory.Tag.ToString() != "0")
					strWhere += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + "=" + txtCategory.Tag.ToString();

				DataTable dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.DESCRIPTION_FLD, txtPartName.Text, strWhere, pblnAlwaysShowDialog);
				
				if(dtbResult != null && dtbResult.Rows.Count > 0)
				{
					string strProductID = string.Empty;
					if (dtbResult.Rows.Count > 1)
					{
						foreach (DataRow drowData in dtbResult.Rows)
							strProductID += drowData[ITM_ProductTable.PRODUCTID_FLD] + ",";
						strProductID += "0";
						txtPartNumber.Text = "Multi Selection";
						txtPartNumber.Tag = strProductID;
						txtPartName.Text = "Multi Selection";
						txtRevision.Text = "Multi Selection";
					}
					else
					{
						txtPartNumber.Text = dtbResult.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
						txtPartNumber.Tag = dtbResult.Rows[0][ITM_ProductTable.PRODUCTID_FLD];
						txtPartName.Text = dtbResult.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
						txtRevision.Text = dtbResult.Rows[0][ITM_ProductTable.REVISION_FLD].ToString();		
					}

					//Reset modify status
					txtPartName.Modified = false;					
					txtPartNumber.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtPartNumber.Focus();
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

		/// <summary>
		/// Fill related data on controls when select Part Name
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectPartNumber(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try
			{
				string strWhere = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CCNID_FLD + "=" + cboCCN.SelectedValue;
				if (txtCategory.Tag.ToString() != "0")
					strWhere += " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + "=" + txtCategory.Tag.ToString();

				DataTable dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strWhere, pblnAlwaysShowDialog);
				
				if(dtbResult != null && dtbResult.Rows.Count > 0)
				{
					string strProductID = string.Empty;
					if (dtbResult.Rows.Count > 1)
					{
						foreach (DataRow drowData in dtbResult.Rows)
							strProductID += drowData[ITM_ProductTable.PRODUCTID_FLD] + ",";
						strProductID += "0";
						txtPartNumber.Text = "Multi Selection";
						txtPartNumber.Tag = strProductID;
						txtPartName.Text = "Multi Selection";
						txtRevision.Text = "Multi Selection";
					}
					else
					{
						txtPartNumber.Text = dtbResult.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
						txtPartNumber.Tag = dtbResult.Rows[0][ITM_ProductTable.PRODUCTID_FLD];
						txtPartName.Text = dtbResult.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
						txtRevision.Text = dtbResult.Rows[0][ITM_ProductTable.REVISION_FLD].ToString();		
					}

					//Reset modify status
					txtPartName.Modified = false;					
					txtPartNumber.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtPartNumber.Focus();
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

		/// <summary>
		/// Fill related data on controls when select Master Location
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectMasterLocation(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();

				if(cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
				}

				//Call OpenSearchForm for selecting Master Location
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					//Check if master location was changed then clear grid content
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					txtMasLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
					ClearItemInfo();					
					
					//Reset modify status
					txtMasLoc.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtMasLoc.Focus();
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
		

		/// <summary>
		/// Fill related data on controls when select Master Location
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectCategory(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();		

				//Call OpenSearchForm for selecting Master Location
				DataRowView drwResult = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					//Check if master location was changed then clear grid content
					txtCategory.Text = drwResult[ITM_CategoryTable.CODE_FLD].ToString();
					txtCategory.Tag = drwResult[ITM_CategoryTable.CATEGORYID_FLD];

					//Clear item infor
					ClearItemInfo();
					
					//Reset modify status
					txtCategory.Modified = false;
				
				}
				else if(!pblnAlwaysShowDialog)
				{					
					txtCategory.Focus();
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

		/// <summary>
		/// Fill related data on controls when select BIN
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectCycle(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				
				if(cboCCN.SelectedValue != null)
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);
				}
				else
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
				}
				
				if(cboPlanType.SelectedIndex == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_PLAN_TYPE);
					cboPlanType.Focus();
					return false;
				}

				if(cboPlanType.Text.ToString() == PlanTypeEnum.MPS.ToString())
				{
					//Call OpenSearchForm for selecting MPS planning cycle
					drwResult = FormControlComponents.OpenSearchForm(MTR_MPSCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text, htbCriteria, pblnAlwaysShowDialog);
				}
				if (cboPlanType.Text.Trim() == PlanTypeEnum.MRP.ToString())
				{
					//Call OpenSearchForm for selecting MRP planning cycle
					drwResult = FormControlComponents.OpenSearchForm(MTR_MRPCycleOptionMasterTable.TABLE_NAME, MTR_MPSCycleOptionMasterTable.CYCLE_FLD, txtCycle.Text, htbCriteria, pblnAlwaysShowDialog);
				}
				if (cboPlanType.Text.Trim() == lblDCP.Text.Trim())
				{
					//Call OpenSearchForm for selecting MRP planning cycle
					drwResult = FormControlComponents.OpenSearchForm(PRO_DCOptionMasterTable.TABLE_NAME, PRO_DCOptionMasterTable.CYCLE_FLD, txtCycle.Text, htbCriteria, pblnAlwaysShowDialog);
				}
				
				// If has Master location matched searching condition, fill values to form's controls
				if (drwResult != null)
				{
					if(cboPlanType.Text.ToString() == PlanTypeEnum.MPS.ToString())
					{
						txtCycle.Text = drwResult[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].ToString();
						txtCycle.Tag = drwResult[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD];	
					}

					if(cboPlanType.Text.ToString() == PlanTypeEnum.MRP.ToString())
					{
						txtCycle.Text = drwResult[MTR_MRPCycleOptionMasterTable.CYCLE_FLD].ToString();
						txtCycle.Tag = drwResult[MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD];
					}

					if(cboPlanType.Text.ToString() == lblDCP.Text.Trim())
					{
						txtCycle.Text = drwResult[PRO_DCOptionMasterTable.CYCLE_FLD].ToString();
						txtCycle.Tag = drwResult[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD];
					}

					//Reset modify status
					txtCycle.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtCycle.Focus();
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
		/// <summary>
		/// Calculate information in the Detail table after saving data
		/// </summary>
		/// <author>Trada</author>
		/// <date>Saturday, June 10 2006</date>
		private void RecalculateDataAfterSaving()
		{
			//Get Data from database
			//Condition HashTable
			Hashtable htbCriteria = new Hashtable();
			string strExtraCondition = string.Empty;
				
			//Call BO's method to retrieve data				
			DataSet dstResult = new DataSet();
			if(BuildConditionHashTable(out htbCriteria, out strExtraCondition))
			{
				dstResult = boCPODataViewer.SearchForDCP(htbCriteria);	
				//Calculate data
				DataRow[] adrowResultDetail = null;
				foreach (DataRow drow in dstResult.Tables[0].Rows)
				{
					if (drow[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD] != DBNull.Value)
					{
						adrowResultDetail = dstResult.Tables[0].Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD 
							+ " = " + drow[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].ToString());
						if (adrowResultDetail.Length > 0)
						{
							decimal decTotalQuantity = 0;
							for (int i = 0; i < adrowResultDetail.Length; i++)
								decTotalQuantity += decimal.Parse(adrowResultDetail[i][PRO_DCPResultDetailTable.QUANTITY_FLD].ToString());
							foreach (DataRow drowNewResult in adrowResultDetail)
							{
								drowNewResult[PRO_DCPResultDetailTable.PERCENTAGE_FLD] = Convert.ToDecimal(drowNewResult[PRO_DCPResultDetailTable.QUANTITY_FLD])
									/decTotalQuantity * 100;
							}
						}
					}
				}
				//Update Database
				DCOptionsBO boDCOptions = new DCOptionsBO();
				boDCOptions.UpdateDataSetAfterSaving(dstResult);
			}
		}
		#endregion Methods
		
		#region Event Processing

		private void CPODataViewer_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".CPODataViewer_Load()";
			try
			{
				dtmDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				dtmDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				//dtmDate.TextDetached = true;

				numQuantity.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				numQuantity.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

				txtNumRows.FormatType = FormatTypeEnum.CustomFormat;
				txtNumRows.CustomFormat = Constants.INTERGER_NUMBERFORMAT;
				//numQuantity.TextDetached = true;				
				
				//Set form security
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();
					return;
				}

				//Load CCN and set default CCN
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME], MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
				
				//Set default CCN for CNN combo box
				if (SystemProperty.CCNID != 0)
					cboCCN.SelectedValue = SystemProperty.CCNID;

				//initiate combo box of Plan type
				cboPlanType.Items.Clear();
				cboPlanType.Items.Add(string.Empty);
				cboPlanType.Items.Add(PlanTypeEnum.MRP.ToString());
				cboPlanType.Items.Add(PlanTypeEnum.MPS.ToString());
				cboPlanType.Items.Add(lblDCP.Text);
				cboPlanType.SelectedIndex = 0;
				
				ClearSearchingCondition();

				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
				
				//Bind data to grid then reformat it
				dtbCPODetail = BuildCPODetailTable();
				dgrdData.DataSource = dtbCPODetail;
				FormatDataGrid();
				dgrdData.FilterBar = true;

				btnNewWOConvert.Enabled = false;
				btnExistingWOConvert.Enabled = false;

				btnNewPOConvert.Enabled = false;
				btnExistingPOConvert.Enabled = false;
				btnPrint.Enabled = false;
				chkSelectAll.Enabled = false;

				//Fill Default Master Location 
				FormControlComponents.SetDefaultMasterLocation(txtMasLoc);

				// cache vendor list
				PartyBO boParty = new PartyBO();
				dtbVendor = boParty.ListVendor();
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

		private void chkSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAll_CheckedChanged()";
			try
			{
				int intRowCount = dgrdData.RowCount;
				if(chkSelectAll.Checked)
				{
					for (int i=0 ; i < intRowCount; i++) 
						dtbCPODetail.DefaultView[i].Row[MTR_CPODS.SELECT_COLUMN] = true;
				}
				else
				{
					for (int i=0 ; i < intRowCount; i++) 
						dtbCPODetail.DefaultView[i].Row[MTR_CPODS.SELECT_COLUMN] = false;
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

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				if(dgrdData.RowCount == 0)
					return;

				//Validate data
				blnDataIsValid = ValidateData() && !dgrdData.EditActive;;
				if(!blnDataIsValid) return;
				DCOptionsBO boDCOptions = new DCOptionsBO();
				if (cboPlanType.Text.Trim() != lblDCP.Text.Trim())  // MRP Mode
					boCPODataViewer.DeleteMRP(strCPOIDToDelete);
				else
				{
					//Check dataset of table
					if(dtbCPODetail.DataSet != null)
					{
						DataSet dstTest = new DataSet();
						dstTest.Tables.Add(dtbCPODetail.Copy());
						boDCOptions.UpdateDataSetForDCP(dstTest, strMasterIDToUpdate, arrMasterIDToUpdate, int.Parse(txtCycle.Tag.ToString()));
					}
					else
					{
						DataSet dtsData = new DataSet();
						dtsData.Tables.Add(dtbCPODetail);
						boDCOptions.UpdateDataSetForDCP(dtsData, strMasterIDToUpdate, arrMasterIDToUpdate, int.Parse(txtCycle.Tag.ToString()));
					}
				}
				arrMasterIDToUpdate = new ArrayList();
				strMasterIDToUpdate = "0";
				strCPOIDToDelete = "0";
				btnSave.Enabled = false;
				btnSearch.Enabled = true;
				if (cboPlanType.Text.Trim() == lblDCP.Text.Trim())
					RecalculateDataAfterSaving();
				btnSearch_Click(null, null);
				//Show successful message
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);				
			}
			catch (PCSException ex)
			{
				blnDataIsValid = false;
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
				blnDataIsValid = false;
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

		private void btnCycleSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCycleSearch_Click()";

			try
			{
				SelectCycle(METHOD_NAME, true);
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

		private void btnMasLocSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMasLocSearch_Click()";
			
			try
			{
				SelectMasterLocation(METHOD_NAME, true);
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

		private void btnPartNumberSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNumberSearch_Click()";
			
			try
			{
				SelectPartNumber(METHOD_NAME, true);
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

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			try
			{
				txtNumRows.Value = 0;
				chkSelectAll.Checked = false;
				dgrdData.FetchRowStyles = false;
				this.Cursor = Cursors.WaitCursor;
				//Condition HashTable
				Hashtable htbCriteria = new Hashtable();
				string strExtraCondition = string.Empty;
				
				//Call BO's method to retrieve data				
				DataSet dstResult = null;
				if(!BuildConditionHashTable(out htbCriteria, out strExtraCondition))
				{
					//HACKED-Added by Tuan TQ. 27 Dec, 2005. - Fix error no. 3195
					dtbCPODetail = BuildCPODetailTable();
					dgrdData.DataSource = dtbCPODetail;

					//Reformat grid
					FormatDataGrid();
					this.Cursor = Cursors.Default;
					//End hacked

					return;
				}

				if (cboPlanType.Text.Trim() != lblDCP.Text.Trim())
				{
					#region Search for MRP/MPS

					dstResult = boCPODataViewer.Search(htbCriteria);
					if(dstResult != null)
					{					
						if(dstResult.Tables.Count != 0)
						{
							dtbCPODetail = dstResult.Tables[0];						
							dtbCPODetail.DefaultView.RowFilter = strExtraCondition;
							dtbCPODetail.DefaultView.Sort = ITM_ProductTable.PRIMARYVENDORID_FLD + ", "
								+ ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
								+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD + ","
								+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD;
						}
					}
					else
						dtbCPODetail = BuildCPODetailTable();

					dgrdData.DataSource = dtbCPODetail;

					//Reformat grid
					FormatDataGrid();
					dgrdData.Splits[0].DisplayColumns["Quantity"].Locked = true;
					dgrdData.AllowAddNew = false;
					#endregion
				}
				else
				{
					#region Search for DCP

					dstResult = boCPODataViewer.SearchForDCP(htbCriteria);
					if(dstResult != null)
					{					

						if(dstResult.Tables.Count != 0)
						{
							dtbCPODetail = dstResult.Tables[0];						
							dtbCPODetail.DefaultView.RowFilter = strExtraCondition;
							dtbCPODetail.DefaultView.Sort = ITM_ProductTable.PRIMARYVENDORID_FLD + ", "
								+ ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
								+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD + ","
								+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD;
						}					
					}
					else
						dtbCPODetail = BuildCPODetailTable();
					
					dgrdData.DataSource = dtbCPODetail;
					//Reformat grid
					FormatDataGrid();
					dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.CPOID_FLD].Visible = true;
					dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].Visible = true;
					dgrdData.AllowAddNew = true;
					#endregion
				}
				dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.CONVERTED_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[MTR_CPOTable.CPOID_FLD].Visible = (cboPlanType.Text.Trim() != lblDCP.Text.Trim());
				dgrdData.Splits[0].DisplayColumns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Visible = (cboPlanType.Text.Trim() != lblDCP.Text.Trim());
				dgrdData.FilterBar = true;
				//Enable button
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Button = true;
				//Disable convert to WO if plan type not is DCP
				btnNewWOConvert.Enabled = (cboPlanType.Text != PlanTypeEnum.MRP.ToString().Trim() && (dgrdData.RowCount > 0));
				btnExistingWOConvert.Enabled = (cboPlanType.Text != PlanTypeEnum.MRP.ToString().Trim() && (dgrdData.RowCount > 0));
			    txtNumRows.Value = dgrdData.RowCount != 0 ? dgrdData.RowCount : 0;
			    btnNewPOConvert.Enabled = (dgrdData.RowCount > 0);
				btnPrint.Enabled = (dgrdData.RowCount > 0);
				btnExistingPOConvert.Enabled = btnNewPOConvert.Enabled;
				chkSelectAll.Enabled = (dgrdData.RowCount > 0);
				btnSave.Enabled = true;
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

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void GroupBox_Enter(object sender, System.EventArgs e)
		{
		
		}


		private void btnCategorySearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCategorySearch_Click()";
			
			try
			{
				SelectCategory(METHOD_NAME, true);
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

		private void btnExistingPOConvert_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnExistingPOConvert_Click()";
			try
			{
				Boolean blnSelected = false;
				for (int i =0; i <dgrdData.RowCount; i++)
				{
					if (dgrdData[i, MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString())
					{
						blnSelected = true;
						break;
					}
				}
				if (!blnSelected)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ROW_TO_CONVERT, MessageBoxIcon.Warning);
					return;
				}
				if (dgrdData.RowCount == 0) return;
				
				//Open search form to select PO which was not closed and approved
				string strCondition = Constants.WHERE_KEYWORD + " (SELECT Count(*) FROM "  + PO_PurchaseOrderDetailTable.TABLE_NAME 
					+ " " + Constants.WHERE_KEYWORD + Constants.WHITE_SPACE + "(" + PO_PurchaseOrderDetailTable.APPROVERID_FLD + " Is Not Null "
					+ " OR " + PO_PurchaseOrderDetailTable.CLOSED_FLD + " = 1 ) " + Constants.AND + " " 
					+ PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + ") = 0 "
					+ " AND " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + "=" + txtMasLoc.Tag.ToString();
				DataRowView drvResult = FormControlComponents.OpenSearchForm(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, string.Empty, strCondition);
				if (drvResult != null)	
				{
					MTR_CPOVO voCPO = new MTR_CPOVO();
					voCPO.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
					voCPO.MasterLocationID = int.Parse(txtMasLoc.Tag.ToString());
					PO_PurchaseOrderMasterVO voPOMaster = new PO_PurchaseOrderMasterVO();
					voPOMaster.PurchaseOrderMasterID = int.Parse(drvResult[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].ToString());
					DataView dtwAllCPOAffterSort = (dtbCPODetail.Copy()).DefaultView;
					dtwAllCPOAffterSort.RowFilter = MTR_CPODS.SELECT_COLUMN + Constants.EQUAL + 1.ToString()
						+ " AND " + ITM_ProductTable.LISTPRICE_FLD+ " > 0";
					//Create Detail & PODelivery
					DataSet dstPODelivery = new DataSet();
					dstPODelivery.Tables.Add(PO_DeliveryScheduleTable.TABLE_NAME);
					dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD, typeof(int));
					dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.DELIVERYLINE_FLD, typeof(string));
					dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.SCHEDULEDATE_FLD, typeof(DateTime));
					dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, typeof(decimal));
					dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, typeof(decimal));
					dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD, typeof(int));
                    dstPODelivery.Tables[0].Columns.Add(ITM_ProductTable.PRODUCTID_FLD, typeof(int));
					dtwAllCPOAffterSort.Sort = ITM_ProductTable.PRODUCTID_FLD;
					int intStep = 0;
					int i =0;
					while (i <dtwAllCPOAffterSort.Count)
					{
						DataRowView[] drowSameItems = dtwAllCPOAffterSort.FindRows(dtwAllCPOAffterSort[i][ITM_ProductTable.PRODUCTID_FLD].ToString());

						intStep = drowSameItems.Length;
						//create the new row for POLine
						int intDeliveryLine = 0;
						foreach (DataRowView drowData in drowSameItems)
						{
							//create the new PODeliveryLine
							DataRow drowPOSche = dstPODelivery.Tables[0].NewRow();
							drowPOSche[ITM_ProductTable.PRODUCTID_FLD] = drowData[ITM_ProductTable.PRODUCTID_FLD];
							drowPOSche[PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = ++intDeliveryLine;
							drowPOSche[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = drowData[MTR_CPOTable.DUEDATE_FLD];
							drowPOSche[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = drowData[MTR_CPOTable.QUANTITY_FLD];
							dstPODelivery.Tables[0].Rows.Add(drowPOSche);
						}
						i = i + intStep;
					}
					PCSProcurement.Purchase.PurchaseOrder frmPO = new PurchaseOrder(dtwAllCPOAffterSort, voCPO, voPOMaster, dstPODelivery);
					frmPO.ShowDialog();
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

		private void btnNewPOConvert_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnNewPOConvert_Click()";
			Cursor = Cursors.WaitCursor;
			try
			{
				#region Check condition before convert
				if(FormControlComponents.CheckMandatory(cboPlanType))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboPlanType.Focus();
					Cursor = Cursors.Default; return;
				}
				if(FormControlComponents.CheckMandatory(txtCycle))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtCycle.Focus();
					txtCycle.Select();
					Cursor = Cursors.Default; return;
				}
				if (cboPlanType.Text.Trim() == lblDCP.Text.Trim())
				{
					//Check if this is new row
					for (int i = 0; i < dgrdData.RowCount; i++)
					{
						if (dgrdData[i, MTR_CPODS.SELECT_COLUMN].ToString() != string.Empty) 
						{
							if ((bool)dgrdData[i, MTR_CPODS.SELECT_COLUMN] == true)
							{
								if (dgrdData[i, PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD] == DBNull.Value)
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_CPO_CAN_NOT_CONVERT, MessageBoxIcon.Exclamation);
									dgrdData.Row = i;
									dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
									dgrdData.Focus();
									Cursor = Cursors.Default; return;
								}
							}
						}
					}
				}
				blnConvertPOSuccess = false;
				//create error items table
				dtbListErrorItem = new DataTable();
				dtbListErrorItem.Columns.Add(ITM_ProductTable.PRODUCTID_FLD, typeof(int));
				dtbListErrorItem.Columns.Add(ITM_ProductTable.CODE_FLD);
				dtbListErrorItem.Columns.Add(ITM_ProductTable.DESCRIPTION_FLD);
				dtbListErrorItem.Columns.Add(ITM_ProductTable.REVISION_FLD);
				dtbListErrorItem.Columns.Add(REASON_FLD);

				frmMessageBoxForm = new MessageBoxFormForItems();

				intToPOorToWO = 1;
				string strReason = string.Empty;
				DataRow[] drowSameItem = dtbCPODetail.Select(MTR_CPODS.SELECT_COLUMN + "=" + 1);

				#region Get list error item

				foreach (DataRow drow in drowSameItem)
				{
					strReason = string.Empty;
					if (drow[MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString() && dtbListErrorItem.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + (int) drow[ITM_ProductTable.PRODUCTID_FLD]).Length == 0)
					{
						
						if (intToPOorToWO == 1)
						{
							if (drow[ITM_ProductTable.PRIMARYVENDORID_FLD].ToString() == string.Empty)
							{
								strReason += frmMessageBoxForm.lblPrimaryVendor.Text.Trim() + ";";
							}
							else
							{
								//Create fake data for Vendor
								DataTable dtbDeliverySchedule = boCPODataViewer.GetVendorDeliveryPolicyByParty((int) drow[ITM_ProductTable.PRIMARYVENDORID_FLD]);

								DataRow[] drowDelForProduct = dtbDeliverySchedule.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + (int) drow[ITM_ProductTable.PRODUCTID_FLD]);
								if (drowDelForProduct.Length == 0)
								{
									strReason += frmMessageBoxForm.lblVendorDeliverySchedule.Text.Trim() + ";";;
								}
							}
							if (drow[ITM_ProductTable.VENDORLOCATIONID_FLD].ToString() == string.Empty)
							{
								strReason += frmMessageBoxForm.lblVendorLoc.Text.Trim() + ";";
							}
							if (drow[ITM_ProductTable.VENDORCURRENCYID_FLD].ToString() == string.Empty)
							{
								strReason += frmMessageBoxForm.lblVendorCurrency.Text.Trim() + ";";
							}
							if (drow[ITM_ProductTable.LISTPRICE_FLD].ToString() == string.Empty)
							{
								strReason += frmMessageBoxForm.lblListPrice.Text.Trim() + ";";
							}
							else
							{
								if (Convert.ToDecimal(drow[ITM_ProductTable.LISTPRICE_FLD]) <= 0)
								{
									strReason += frmMessageBoxForm.lblListPrice.Text.Trim() + ";";
								}
							}
							if (drow[ITM_ProductTable.VENDORCURRENCYID_FLD].ToString() != string.Empty && drow[MST_ExchangeRateTable.RATE_FLD].ToString() == string.Empty && (int) drow[ITM_ProductTable.VENDORCURRENCYID_FLD] != SystemProperty.HomeCurrencyID)
							{
								strReason += frmMessageBoxForm.lblExchangeRate.Text.Trim() + ";";
							}
							
							if (strReason != string.Empty)
							{
								DataRow drowItem = dtbListErrorItem.NewRow();
								drowItem[ITM_ProductTable.PRODUCTID_FLD] = drow[ITM_ProductTable.PRODUCTID_FLD];
								drowItem[ITM_ProductTable.CODE_FLD] = drow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD];
								drowItem[ITM_ProductTable.DESCRIPTION_FLD] = drow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD];
								drowItem[ITM_ProductTable.REVISION_FLD] = drow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD];
								drowItem[REASON_FLD] = strReason.Substring(0, strReason.Length-1);

								dtbListErrorItem.Rows.Add(drowItem);
							}
						}
					}
				}

				#endregion

				Boolean blnSelected = false;
				for (int i =0; i <dtbCPODetail.Rows.Count; i++)
				{
					if (dtbCPODetail.Rows[i][MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString())
					{
						blnSelected = true;
						break;
					}
				}
				if (!blnSelected)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ROW_TO_CONVERT, MessageBoxIcon.Warning);
					Cursor = Cursors.Default; return;
				}

				#endregion

				if (frmMessageBoxForm != null && dtbListErrorItem.Rows.Count > 0)
				{
					frmMessageBoxForm.BugReason = dtbListErrorItem;
					frmMessageBoxForm.ShowDialog();
				}
				else
				{
					ThreadForGenNewPO();
					if (blnConvertPOSuccess)
					{
						PCSMessageBox.Show(ErrorCode.CONVERTED_SUCCESSFULLY, MessageBoxIcon.Information);
						Cursor = Cursors.Default; return;
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
			Cursor = Cursors.Default;
		}

		private void btnNewWOConvert_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnNewWOConvert_Click()";
			Cursor = Cursors.WaitCursor;
			try
			{
				#region Check data before convert
				if(FormControlComponents.CheckMandatory(cboPlanType))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboPlanType.Focus();
					Cursor = Cursors.Default; return;
				}

				if(FormControlComponents.CheckMandatory(txtCycle))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					txtCycle.Focus();
					txtCycle.Select();
					Cursor = Cursors.Default; return;
				}
				if(cboPlanType.Text == PlanTypeEnum.MPS.ToString())
				{
					if(FormControlComponents.CheckMandatory(txtMasLoc))
					{
						PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
						txtMasLoc.Focus();
						txtMasLoc.Select();
						Cursor = Cursors.Default; return;
					}
				}
				else if(cboPlanType.Text == PlanTypeEnum.MRP.ToString())
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_DCP_OR_MPS_BEFORE_CONVERT_WO);
					cboPlanType.Focus();
					Cursor = Cursors.Default; return;
				}
				if (cboPlanType.Text.Trim() == lblDCP.Text.Trim())
				{
					//Check if this is new row
					for (int i = 0; i < dgrdData.RowCount; i++)
					{
						if (dgrdData[i, MTR_CPODS.SELECT_COLUMN].ToString() != string.Empty) 
						{
							if ((bool)dgrdData[i, MTR_CPODS.SELECT_COLUMN] == true)
							{
								if (dgrdData[i, PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD] == DBNull.Value)
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_CPO_CAN_NOT_CONVERT, MessageBoxIcon.Exclamation);
									dgrdData.Row = i;
									dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
									dgrdData.Focus();
									Cursor = Cursors.Default; return;
								}
							}
						}
					}
				}
				blnConvertWOSuccess = false;

				#region //create error items table
				dtbListErrorItem = new DataTable();
				dtbListErrorItem.Columns.Add(ITM_ProductTable.PRODUCTID_FLD, typeof(int));
				dtbListErrorItem.Columns.Add(ITM_ProductTable.CODE_FLD);
				dtbListErrorItem.Columns.Add(ITM_ProductTable.DESCRIPTION_FLD);
				dtbListErrorItem.Columns.Add(ITM_ProductTable.REVISION_FLD);
				dtbListErrorItem.Columns.Add(REASON_FLD);

				frmMessageBoxForm = new MessageBoxFormForItems();

				Boolean blnSelected = false;
				intToPOorToWO = 2;
				DataRow[] drowSameItem = dtbCPODetail.Select(MTR_CPODS.SELECT_COLUMN + "=" + 1);
				foreach (DataRow drow in drowSameItem)
				{
					if (drow[MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString())
					{
						if (intToPOorToWO == 2) 
						{
							if (drow[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString() == string.Empty &&
								dtbListErrorItem.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + (int) drow[ITM_ProductTable.PRODUCTID_FLD]).Length == 0)
							{
								DataRow drowItem = dtbListErrorItem.NewRow();
								drowItem[ITM_ProductTable.PRODUCTID_FLD] = drow[ITM_ProductTable.PRODUCTID_FLD];
								drowItem[ITM_ProductTable.CODE_FLD] = drow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD];
								drowItem[ITM_ProductTable.DESCRIPTION_FLD] = drow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD];
								drowItem[ITM_ProductTable.REVISION_FLD] = drow[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD];
								drowItem[REASON_FLD] = frmMessageBoxForm.lblProductionLine.Text.Trim();

								dtbListErrorItem.Rows.Add(drowItem);
							}
						}
					}
				}

				#endregion

				for (int i =0; i <dgrdData.RowCount; i++)
				{
					if (dgrdData[i, MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString())
					{
						blnSelected = true;
						break;
					}
				}
				if (!blnSelected)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ROW_TO_CONVERT, MessageBoxIcon.Warning);
					Cursor = Cursors.Default; return;
				}

				#endregion

				ThreadForGenNewWO();
				if (frmMessageBoxForm != null && dtbListErrorItem.Rows.Count > 0)
				{
					frmMessageBoxForm.BugReason = dtbListErrorItem;
					frmMessageBoxForm.ShowDialog();
				}
				else
				{
					if (blnConvertWOSuccess == true)
					{
						btnSearch_Click(null, null);
						PCSMessageBox.Show(ErrorCode.CONVERTED_SUCCESSFULLY, MessageBoxIcon.Information);
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
			Cursor = Cursors.Default;
		}		

		private void btnExistingWOConvert_Click(object sender, System.EventArgs e)
		{
			Boolean blnSelected = false;
			for (int i =0; i <dgrdData.RowCount; i++)
			{
				if (dgrdData[i, MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString())
				{
					blnSelected = true;
					break;
				}
			}
			if (!blnSelected)
			{
				PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ROW_TO_CONVERT, MessageBoxIcon.Warning);
				return;
			}
			if (dgrdData.RowCount == 0) return;
			string strCondition = Constants.WHERE_KEYWORD + " (Select count(*) From " + PRO_WorkOrderDetailTable.TABLE_NAME + " " + Constants.WHERE_KEYWORD  + " "
				+ PRO_WorkOrderDetailTable.STATUS_FLD + "=" + (int) WOLineStatus.Released + " OR " + PRO_WorkOrderDetailTable.STATUS_FLD + " = " + (int) WOLineStatus.Unreleased + Constants.AND + " "
				+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + "=" + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + ") > 0 "
				+ " AND " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + "=" + txtMasLoc.Tag.ToString();

			DataRowView drvResult = FormControlComponents.OpenSearchForm(PRO_WorkOrderMasterTable.TABLE_NAME, PRO_WorkOrderMasterTable.WORKORDERNO_FLD, string.Empty, strCondition);

			if (dgrdData.RowCount == 0) return;
			DataView dtwAllCPOAffterSort = (dtbCPODetail.Copy()).DefaultView;
			dtwAllCPOAffterSort.RowFilter = MTR_CPODS.SELECT_COLUMN + Constants.EQUAL + 1.ToString();
			dtwAllCPOAffterSort.Sort = MTR_CPOTable.DUEDATE_FLD;
			if (drvResult != null)
			{
				MTR_CPOVO voCPO = new MTR_CPOVO();
				voCPO.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
				voCPO.MasterLocationID = int.Parse(txtMasLoc.Tag.ToString());
				DataSet dstCPODetail = new DataSet();
				dstCPODetail.Tables.Add(dtbCPODetail.Copy());
				PCSProduction.WorkOrder.WorkOrder frmWO = new PCSProduction.WorkOrder.WorkOrder(dtwAllCPOAffterSort, int.Parse(drvResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].ToString()), voCPO);
				frmWO.ShowDialog();
				btnSearch_Click(sender, new EventArgs());
			}
		}
		
		private void btnPartNameSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPartNameSearch_Click()";
			
			try
			{
				SelectPartName(METHOD_NAME, true);
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
		
		private void txtCategory_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_Validating()";
			try
			{				
				if(txtCategory.Text.Length == 0)
				{
					txtCategory.Tag = ZERO_STRING;
					return;
				}
				else if(!txtCategory.Modified)
				{
					return;
				}

				e.Cancel = !SelectCategory(METHOD_NAME, false);
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

		

		private void txtCategory_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnCategorySearch.Enabled))
				{
					SelectCategory(METHOD_NAME, true);
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
		
		private void txtCycle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_Validating()";

			try
			{				
				if(txtCycle.Text.Length == 0)
				{
					txtCycle.Tag =ZERO_STRING;
					return;
				}
				else if(!txtCycle.Modified)
				{
					return;
				}

				e.Cancel = !SelectCycle(METHOD_NAME, false);
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

		private void txtCycle_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCycle_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnCycleSearch.Enabled))
				{
					SelectCycle(METHOD_NAME, true);
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
		
		private void txtMasLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_Validating()";

			try
			{				
				if(txtMasLoc.Text.Length == 0)
				{
					txtMasLoc.Tag =ZERO_STRING;
					return;
				}
				else if(!txtMasLoc.Modified)
				{
					return;
				}

				e.Cancel = !SelectMasterLocation(METHOD_NAME, false);
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

		
		private void txtMasLoc_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnMasLocSearch.Enabled))
				{
					SelectMasterLocation(METHOD_NAME, true);
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
		
		private void txtPartName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_Validating()";

			try
			{
				if(txtPartName.Text.Length == 0)
				{
					txtPartNumber.Text = string.Empty;
					txtRevision.Text = string.Empty;
					txtPartNumber.Tag = ZERO_STRING;
					return;
				}
				else if(!txtPartName.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectPartName(METHOD_NAME, false);				
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

		private void txtPartName_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartName_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnPartNameSearch.Enabled))
				{
					SelectPartName(METHOD_NAME, true);
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
		
		private void txtPartNumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNumber_Validating()";

			try
			{
				if(txtPartNumber.Text.Length == 0)
				{
					txtPartName.Text = string.Empty;
					txtRevision.Text = string.Empty;
					txtPartNumber.Tag = ZERO_STRING;
					return;
				}
				else if(!txtPartNumber.Modified)
				{
					return;
				}
				
				e.Cancel = !SelectPartNumber(METHOD_NAME, false);				
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

		
		private void txtPartNumber_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtPartNumber_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnPartNumberSearch.Enabled))
				{
					SelectPartNumber(METHOD_NAME, true);
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

		private void cboPlanType_SelectedIndexChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboPlanType_SelectedIndexChanged()";

			try
			{
				if(cboPlanType.SelectedItem == null) return;

				ClearSearchingCondition();

				btnProductionLine.Enabled = !(cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MRP.ToString());
				txtProductionLine.Enabled = !(cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MRP.ToString());

				btnMasLocSearch.Enabled = !(cboPlanType.SelectedItem.ToString() == lblDCP.Text.Trim());
				txtMasLoc.Enabled = !(cboPlanType.SelectedItem.ToString() == lblDCP.Text.Trim());
				lblMasLoc.Enabled = !(cboPlanType.SelectedItem.ToString() == lblDCP.Text.Trim());
				
				if (cboPlanType.SelectedItem.ToString() == lblDCP.Text.Trim()) //Change to DCP mode
				{
					txtMasLoc.Text = string.Empty;

					#region HACK: Trada 21-04-2006
					//add some new columns
					//Shift column
					dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Visible = true;
					//Safety Stock Qty Column
					dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].Visible = true;
					//IsManual Column
					dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Visible = true;
					//config new columns
					dgrdData.Columns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
					dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Far;
					dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.ISMANUAL_FLD].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
					for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
					{
						dgrdData.Splits[0].DisplayColumns[i].Locked = true;
					}
					dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Locked = false;
					dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Locked = false;
					dgrdData.Splits[0].DisplayColumns["Quantity"].Locked = false;
					#endregion END: Trada 21-04-2006
				}
				else //Change to default mode
				{
					#region HACK: Trada 21-04-2006
					dgrdData.AllowAddNew = false;
					//invisible some columns
					//Shift column
					dgrdData.Splits[0].DisplayColumns[PRO_ShiftTable.SHIFTDESC_FLD].Visible = false;
					//Safety Stock Qty Column
					dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT].Visible = false;
					//IsManual Column
					dgrdData.Splits[0].DisplayColumns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Visible = false;
					dgrdData.Splits[0].DisplayColumns["Quantity"].Locked = true;
					#endregion END: Trada 21-04-2006
				}
				//HACK: added by Tuan TQ. 17 Feb, 2006
				txtVendor.Enabled = (cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MRP.ToString());				
				btnVendorSearch.Enabled = txtVendor.Enabled;
				//End hack
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
		
		private void CPODataViewer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".CPODataViewer_Closing()";
			try
			{	
				// if the form has been changed then ask to store database
				if(btnSave.Enabled) 
				{
					DialogResult enumDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
					if( enumDialog == DialogResult.Yes)
					{
						btnSave_Click(sender, e);	
						e.Cancel = !blnDataIsValid;
					}
					else if( enumDialog == DialogResult.Cancel)//click Cancel button
					{
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
		
		private void CPODataViewer_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".CPODataViewer_KeyDown()";

			try
			{				
				if (e.KeyCode == Keys.F12)
				{
					dgrdData.Row = dgrdData.RowCount;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
					dgrdData.Focus();
				}
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
			catch(Exception ex)
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

		private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";

			try
			{
					if(e.Column.DataColumn == null) return;

				switch (e.Column.DataColumn.DataField)
				{
					case MTR_CPOTable.QUANTITY_FLD:
						if (cboPlanType.Text.Trim() == lblDCP.Text.Trim())
						{
							// calculate total time
							decimal decQuantity = 0, decLeadTime = 0, decTotalSecond = 0;
							try
							{
								decQuantity = Convert.ToDecimal(dgrdData.Columns[e.Column.DataColumn.DataField].Value);
							}
							catch{}
							try
							{
								decLeadTime = Convert.ToDecimal(dgrdData.Columns[ITM_ProductTable.LTVARIABLETIME_FLD].Value);
							}
							catch{}
							decTotalSecond = decQuantity * decLeadTime;
							// update to grid
							dgrdData.Columns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Value = decTotalSecond;
						}
						btnSave.Enabled = true;
						break;
					case MTR_CPOTable.STARTDATE_FLD:
					case MTR_CPOTable.DUEDATE_FLD:					
						btnSave.Enabled = true;
						break;
				}

				#region HACK: Trada 21-04-2006

				DataRow drwResult = (DataRow) e.Column.DataColumn.Tag;
				//Fill Data to ComNumber
				if (e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME +ITM_ProductTable.CODE_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.REVISION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = null;
						dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value  = string.Empty;
						dgrdData.Columns[MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD].Value  = string.Empty;
						dgrdData.Columns[ITM_ProductTable.LTVARIABLETIME_FLD].Value = string.Empty;
						dgrdData.Columns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Value = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;
						FillItemDataToGrid(drwResult);
						
					}
				}
				//Fill Data to ComName
				if(e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.DESCRIPTION_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.REVISION_FLD].Value	 = string.Empty;
						dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = null;
						dgrdData.Columns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Value = string.Empty;
						dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value  = string.Empty;
						dgrdData.Columns[MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD].Value  = string.Empty;
						dgrdData.Columns[ITM_ProductTable.LTVARIABLETIME_FLD].Value = string.Empty;
						dgrdData.Columns[PRO_DCPResultDetailTable.TOTALSECOND_FLD].Value = string.Empty;
					}
					else
					{
						dgrdData.EditActive = true;

						FillItemDataToGrid(drwResult);
					}
				}
				//Fill data to shitf column
				if(e.Column.DataColumn.DataField == PRO_ShiftTable.SHIFTDESC_FLD)
				{
					if ((e.Column.DataColumn.Tag == null) ||(dgrdData[dgrdData.Row, PRO_ShiftTable.SHIFTDESC_FLD].ToString() == string.Empty))
					{
						dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD].Value = string.Empty;
						dgrdData.Columns[PRO_ShiftTable.SHIFTID_FLD].Value = null;
						
					}
					else
					{
						dgrdData.EditActive = true;
						//Fill data to grid
						dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD].Value = drwResult[PRO_ShiftTable.SHIFTDESC_FLD];
						dgrdData.Columns[PRO_ShiftTable.SHIFTID_FLD].Value = int.Parse(drwResult[PRO_ShiftTable.SHIFTID_FLD].ToString());		
					}
				}
				#endregion END: Trada 21-04-2006
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
			catch(Exception ex)
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
		
		private void dgrdData_AfterDelete(object sender, EventArgs e)
		{
			btnSave.Enabled = true;
		}
		
		private void dgrdData_BeforeRowColChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnPrint_Click()";
			const string REPORTID = "20060126093222153";
			try
			{
				/// HACKED: Thachnn: 24/01/2006: add call to DCPReport form
				if(cboPlanType.SelectedItem != null && cboPlanType.SelectedIndex != 0)
				{
					if(cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MRP.ToString() ||
						cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MPS.ToString() 
						) /// show CPO Report with specified Plan type
					{
						/// OLD CODE GO HERE
						CPOReport rptCPO = new CPOReport();
						rptCPO.Show();
						if (cboCCN.SelectedValue != null)
						{
							rptCPO.cboCCN.SelectedValue = cboCCN.SelectedValue;
						}
						if (cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MRP.ToString()
							|| cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MPS.ToString())
						{
							rptCPO.cboPlanType.SelectedItem = cboPlanType.SelectedItem;
						}
						if (txtCycle.Text.Trim() != string.Empty)
						{
							rptCPO.txtCycle.Text = txtCycle.Text.Trim();
							rptCPO.txtCycle.Tag = txtCycle.Tag;
						}
						if (txtMasLoc.Text.Trim() != string.Empty)
						{
							rptCPO.txtMasLoc.Text = txtMasLoc.Text.Trim();
							rptCPO.txtMasLoc.Tag = txtMasLoc.Tag;
						}
						if (txtProductionLine.Text.Trim() != string.Empty)
						{
							rptCPO.txtProductionLine.Text = txtProductionLine.Text.Trim();
							rptCPO.txtProductionLine.Tag = txtProductionLine.Tag;
						}
						/// END OLD CODE
					}

					else // show DCP Report
					{
						#region SonHT DELETED

//						PCSProduction.DCP.DCPReport objDCPReport = new PCSProduction.DCP.DCPReport();
//						objDCPReport.Show();
//						if (cboCCN.SelectedValue != null)
//						{
//							objDCPReport.CboCCN.SelectedValue = cboCCN.SelectedValue;
//						} 
//						if (txtCycle.Text.Trim() != string.Empty)
//						{
//							objDCPReport.TxtCycle.Text = txtCycle.Text.Trim();
//							objDCPReport.TxtCycle.Tag = txtCycle.Tag;
//						} 
//
//						if (txtProductionLine.Text.Trim() != string.Empty)
//						{
//							objDCPReport.TxtProductionLine.Text = txtProductionLine.Text.Trim();
//							objDCPReport.TxtProductionLine.Tag = txtProductionLine.Tag;
//						} 
						#endregion
						ViewReport frmReport = new ViewReport();
						sys_ReportVO voReport = (sys_ReportVO)(new ViewReportBO()).GetReportByReportID(REPORTID);
						frmReport.VoReport = voReport;
						frmReport.Show();

					}
				}

				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_CPODATAVIEWER_PLEASE_SELECT_PLAN_TYPE);
					cboPlanType.Focus();
					return; 
				}
				/// ENDHACKED: Thachnn: 24/01/2006: add call to DCPReport form				
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

		private void dtmDate_DropDownOpened(object sender, System.EventArgs e)
		{
			try
			{
				dtmDate.Value = DateTime.Now;
			}
			catch
			{
			}
		}

		private void dtmDate_DropDownClosed(object sender, System.EventArgs e)
		{
			try
			{
				if (dtmDate.Text.ToString() == string.Empty)
				{
					dgrdData[dgrdData.Row, dgrdData.Col] = DBNull.Value;
				}
			}
			catch
			{
			}
		}

		private void btnVendorSearch_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnVendorSearch_Click()";

			try
			{
				SelectVendor(true);
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

		private void txtVendor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_Validating()";

			try
			{
				//exit if empty				
				if(txtVendor.Text.Length == 0)
				{					
					return;
				}
				else if(!txtVendor.Modified)
				{
					return;
				}				

				e.Cancel = !SelectVendor(false);				
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

		private void txtVendor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtVendor_KeyDown()";

			try
			{
				if ((e.KeyCode == Keys.F4) && (btnVendorSearch.Enabled))
				{
					SelectVendor(true);
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

		#endregion Event Processing

		#region All prrocedures for convert to PO, by TUANDM
		
		/// <summary>
		/// Convert to PO main function
		/// </summary>
		private void ThreadForGenNewPO()
		{
			const string METHOD_NAME = THIS + ".ThreadForGenNewPO()";
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (dgrdData.RowCount == 0)
					return;
				DataTable dtbTableOfVendor = dtbCPODetail.Clone();
				// Get shema of PO detail
				DataSet dstPODetailSchema = boPurchaseOrder.ListDetailByMaster(0);
				DataView dtwAllCPOAffterSort = (dtbCPODetail.Copy()).DefaultView;
				dtwAllCPOAffterSort.RowFilter = MTR_CPODS.SELECT_COLUMN + Constants.EQUAL + 1.ToString()
					+ " AND " + ITM_ProductTable.PRIMARYVENDORID_FLD + " > 0"
					+ " AND " + ITM_ProductTable.VENDORLOCATIONID_FLD+ " > 0"
					+ " AND " + ITM_ProductTable.VENDORCURRENCYID_FLD+ " > 0"
					+ " AND " + ITM_ProductTable.LISTPRICE_FLD+ " > 0";
				// sort by vendor, category, model then product
				dtwAllCPOAffterSort.Sort = ITM_ProductTable.PRIMARYVENDORID_FLD + ", "
					+ ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD;
				if (dtwAllCPOAffterSort.Count == 0)
				{
					this.Cursor = Cursors.Default;
					return;
				}
				btnNewPOConvert.Enabled = false;
				btnExistingPOConvert.Enabled = false;
				int intWONew = 0, intWOExist = 0;
				if (btnNewWOConvert.Enabled)
				{
					btnNewWOConvert.Enabled = false;
					intWONew = 1;
				}
				if (btnExistingWOConvert.Enabled)
				{
					btnExistingWOConvert.Enabled = false;
					intWOExist = 1;
				}
				int i =0;
				bool blnIsMRP = true;
				if (cboPlanType.Text == lblDCP.Text)
					blnIsMRP = false;
				DateTime dtmAsOfDate = boCPODataViewer.GetAsOfDate(Convert.ToInt32(txtCycle.Tag), blnIsMRP);
				DataSet dstCalendar = boCPODataViewer.GetWorkDayCalendar();
				DateTime dtmFirstValidWorkDay = GetFirstValidWorkDay(dtmAsOfDate, dstCalendar);
				while (i < dtwAllCPOAffterSort.Count)
				{
					dtbTableOfVendor.Rows.Clear();
					dtbTableOfVendor.AcceptChanges();
					DataSet dstPODetail = dstPODetailSchema.Clone();
					for (int j =i; j < dtwAllCPOAffterSort.Count; j++)
					{
						if ((int)dtwAllCPOAffterSort[i][ITM_ProductTable.PRIMARYVENDORID_FLD]
							==(int)dtwAllCPOAffterSort[j][ITM_ProductTable.PRIMARYVENDORID_FLD])
							dtbTableOfVendor.ImportRow(dtwAllCPOAffterSort[j].Row);
						else
						{
							ConvertCPOToNewPO(dtbTableOfVendor, dstPODetail, dtmFirstValidWorkDay, dstCalendar, blnIsMRP);
							i = j;
							break;
						}
						if (j == dtwAllCPOAffterSort.Count-1) 
						{
							ConvertCPOToNewPO(dtbTableOfVendor, dstPODetail, dtmFirstValidWorkDay, dstCalendar, blnIsMRP);
							i = j+1;
						}
					}
				}
				if (intWONew == 1)
					btnNewWOConvert.Enabled = true;
				if (intWOExist == 1)
					btnExistingWOConvert.Enabled = true;
				btnNewPOConvert.Enabled = true;
				btnExistingPOConvert.Enabled = true;
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdstCPOOfVendor">List of vendors</param>
		/// <param name="pdstPODetail"></param>
		private void ConvertCPOToNewPO(DataTable pdstCPOOfVendor, DataSet pdstPODetail, DateTime pdtmFirstValidWorkDay, DataSet pdstCalendar, bool blnIsMRP)
		{
			pdstCPOOfVendor.DefaultView.Sort = ITM_ProductTable.PRIMARYVENDORID_FLD + ", "
				+ ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
				+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD + ","
				+ ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD;

			int intPrimaryVendor = (int) pdstCPOOfVendor.Rows[0][ITM_ProductTable.PRIMARYVENDORID_FLD];
			bool blnIsLocal = IsLocal(intPrimaryVendor);
		
			//Create data for Vendor
			DataTable dtbDeliverySchedule = boCPODataViewer.GetVendorDeliveryPolicyByParty(intPrimaryVendor);

			#region //Create Master
			PO_PurchaseOrderMasterVO voMaster = new PO_PurchaseOrderMasterVO();
			voMaster.OrderDate = DateTime.Parse(boUtils.GetDBDate().ToShortDateString());
			voMaster.CCNID = (int) cboCCN.SelectedValue;
			voMaster.MasterLocationID = Convert.ToInt32(txtMasLoc.Tag);
			// Get prefix menu for TransNo field
            var list = (from obj in SystemProperty.TableMenuEntry
                        where obj.FormLoad == PurchaseOrder.THIS
                        orderby obj.Menu_EntryID ascending
                        select obj).ToList();
			UtilsBO boUtil = new UtilsBO();
			string strFormat = "-yy-mm-dd-##";
			string strMakerCode =
				(boUtil.GetRows(MST_PartyTable.TABLE_NAME,
				"where PartyID=" + pdstCPOOfVendor.DefaultView[0][ITM_ProductTable.PRIMARYVENDORID_FLD])).Rows[0][MST_PartyTable.CODE_FLD].ToString().Trim();
			if (strMakerCode != string.Empty)
			{
				string strPONumber = boUtil.GetNoByMask(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, strMakerCode, strFormat);
				voMaster.Code = strPONumber;
			}
			voMaster.ShipToLocID = int.Parse(txtMasLoc.Tag.ToString());
			voMaster.InvToLocID  = voMaster.ShipToLocID;
			voMaster.CurrencyID = (int) pdstCPOOfVendor.DefaultView[0][ITM_ProductTable.VENDORCURRENCYID_FLD];
			// Update maker
			voMaster.MakerLocationID = voMaster.VendorLocID= (int) pdstCPOOfVendor.DefaultView[0][ITM_ProductTable.VENDORLOCATIONID_FLD];
			voMaster.MakerID = voMaster.PartyID = (int) pdstCPOOfVendor.DefaultView[0][ITM_ProductTable.PRIMARYVENDORID_FLD];
			voMaster.ExchangeRate = FillExchangeRate(voMaster.CurrencyID, voMaster.OrderDate);

			#endregion

			#region // Create dataset structure
			DataSet dstPODelivery = new DataSet();
			dstPODelivery.Tables.Add(PO_DeliveryScheduleTable.TABLE_NAME);
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD, typeof(int));
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.DELIVERYLINE_FLD, typeof(string));
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.SCHEDULEDATE_FLD, typeof(DateTime));
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD, typeof(decimal));
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD, typeof(decimal));
			dstPODelivery.Tables[0].Columns.Add(ITM_ProductTable.PRODUCTID_FLD, typeof(int));
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD, typeof(int));
			dstPODelivery.Tables[0].Columns.Add(PO_DeliveryScheduleTable.ADJUSTMENT_FLD, typeof(decimal));
			dstPODelivery.Tables[0].Columns.Add("OriginalDeliveryQuantity", typeof(decimal));
			dstPODelivery.Tables[0].Columns.Add("StartOfSCHEDULEDATE", typeof(DateTime));

			dstPODelivery.Tables[0].Columns.Add("FixLT", typeof(decimal));
			dstPODelivery.Tables[0].Columns.Add("CPOID", typeof(int));

			DataSet dstDelTemp = dstPODelivery.Clone();
			dstDelTemp.Tables[0].Columns.Add(MTR_CPOTable.CONVERTED_FLD, typeof(bool));

			#endregion

			int intLine = 0, intStep = 0;
			int i =0;
			pdstPODetail.Tables[0].Columns.Add(PO_DeliveryScheduleTable.SCHEDULEDATE_FLD, typeof(DateTime));
			// scan all vendor
			while (i < pdstCPOOfVendor.DefaultView.Count)
			{
				// Get same items
				DataRowView[] drowSameItems = pdstCPOOfVendor.DefaultView.FindRows(new object[]{
						pdstCPOOfVendor.DefaultView[i][ITM_ProductTable.PRIMARYVENDORID_FLD].ToString(),
						pdstCPOOfVendor.DefaultView[i][ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].ToString(),
						pdstCPOOfVendor.DefaultView[i][ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].ToString(),
						pdstCPOOfVendor.DefaultView[i][ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].ToString()
						});
				// get all delivery by Item
			    var productId = (int) drowSameItems[0][ITM_ProductTable.PRODUCTID_FLD];
                DataRow[] drowDelForProduct = dtbDeliverySchedule.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + productId);
				intStep = drowSameItems.Length;
				// if existed delivery schedule
				if (drowDelForProduct.Length > 0)
				{
					#region //create the new row for POLine
					DataRow drowPODetail = pdstPODetail.Tables[0].NewRow();
					drowPODetail[PRO_WorkOrderDetailTable.LINE_FLD] = ++intLine;
                    drowPODetail[ITM_ProductTable.PRODUCTID_FLD] = productId;
					drowPODetail[ITM_ProductTable.BUYINGUMID_FLD] = drowSameItems[0][ITM_ProductTable.BUYINGUMID_FLD];
					drowPODetail[ITM_ProductTable.STOCKUMID_FLD]  = drowSameItems[0][ITM_ProductTable.STOCKUMID_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = drowSameItems[0][ITM_ProductTable.LISTPRICE_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.VAT_FLD]		= drowSameItems[0][ITM_ProductTable.VAT_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD] = drowSameItems[0][ITM_ProductTable.IMPORTTAX_FLD];
					drowPODetail[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD] = drowSameItems[0][ITM_ProductTable.SPECIALTAX_FLD];

					#endregion

					int intDeliveryLine = 0;

					#region // scan all CPO that is similar ProductID to sum quantity
					foreach (DataRowView drowData in drowSameItems)
					{
						if (drowData[MTR_CPODS.SELECT_COLUMN].ToString() == true.ToString())
						{
							if (drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString() == string.Empty)
								drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = 0;

							//create the new PODeliveryLine
							DataRow drowPOSche = dstDelTemp.Tables[0].NewRow();
							drowPOSche[ITM_ProductTable.PRODUCTID_FLD] = drowData[ITM_ProductTable.PRODUCTID_FLD];
							drowPOSche[PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = ++intDeliveryLine;
							drowPOSche[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = drowData[MTR_CPOTable.DUEDATE_FLD];

							drowPOSche["OriginalDeliveryQuantity"] = Convert.ToDecimal(drowData[MTR_CPOTable.QUANTITY_FLD]);
							drowPOSche["FixLT"] = drowData["FixLT"];
							drowPOSche["CPOID"] = drowData["CPOID"];

                            drowPOSche[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = drowData[MTR_CPOTable.QUANTITY_FLD] =
                                GetQuantityForCPO(drowData, Convert.ToDecimal(drowData[MTR_CPOTable.QUANTITY_FLD]));                            

							dstDelTemp.Tables[0].Rows.Add(drowPOSche);

							drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = Convert.ToDecimal(drowPODetail[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD]) 
								+ Convert.ToDecimal(drowData[MTR_CPOTable.QUANTITY_FLD]);
						}
					}
					pdstPODetail.Tables[0].Rows.Add(drowPODetail);

					#endregion

					DataTable dtbWeekMonthDelivery = new DataTable();
					if (drowDelForProduct[0][PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD].ToString() == ((int) PODeliveryTypeEnum.Daily).ToString())
					{
						#region Nhom theo lich giao hang Daily

						DataRow[] drvRealDel = dtbDeliverySchedule.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + (int) drowSameItems[0][ITM_ProductTable.PRODUCTID_FLD],
						                                                  PO_VendorDeliveryScheduleTable.DELHOUR_FLD + " ASC," + PO_VendorDeliveryScheduleTable.DELMIN_FLD + " ASC");
						dtbWeekMonthDelivery = GroupPODeliverys(dstDelTemp.Tables[0], drvRealDel, PODeliveryTypeEnum.Daily, pdtmFirstValidWorkDay, pdstCalendar, blnIsLocal).Copy();

						#endregion
					} 
					else if (drowDelForProduct[0][PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD].ToString() == ((int) PODeliveryTypeEnum.Weekly).ToString())
					{
						#region Nhom theo lich giao hang Weekly

						DataRow[] drvRealDel = dtbDeliverySchedule.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + (int) drowSameItems[0][ITM_ProductTable.PRODUCTID_FLD],
						                                                  "WeeklyDay ASC," + PO_VendorDeliveryScheduleTable.DELHOUR_FLD + " ASC," + PO_VendorDeliveryScheduleTable.DELMIN_FLD + " ASC");
						dtbWeekMonthDelivery = GroupPODeliverys(dstDelTemp.Tables[0], drvRealDel, PODeliveryTypeEnum.Weekly, pdtmFirstValidWorkDay, pdstCalendar, blnIsLocal).Copy();

						#endregion
					}
					else if (drowDelForProduct[0][PO_VendorDeliveryScheduleTable.DELIVERYTYPE_FLD].ToString() == ((int) PODeliveryTypeEnum.Monthly).ToString())
					{
						#region Nhom theo lich giao hang Monthly

						DataRow[] drvRealDel = dtbDeliverySchedule.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + (int) drowSameItems[0][ITM_ProductTable.PRODUCTID_FLD],
						                                                  PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD + " ASC," + PO_VendorDeliveryScheduleTable.DELHOUR_FLD + " ASC ," + PO_VendorDeliveryScheduleTable.DELMIN_FLD +" ASC");
						dtbWeekMonthDelivery = GroupPODeliverys(dstDelTemp.Tables[0], drvRealDel, PODeliveryTypeEnum.Monthly, pdtmFirstValidWorkDay, pdstCalendar, blnIsLocal).Copy();

						#endregion
					}

					foreach (DataRow drowDel in dtbWeekMonthDelivery.Rows)
					{
					    dstPODelivery.Tables[0].ImportRow(drowDel);
					}
					dstDelTemp.Tables[0].Rows.Clear();
					dstDelTemp.Tables[0].AcceptChanges();
				}
				i = i + intStep;
			}
			
            #region update new convert

			UpdateNewConvertToPO(voMaster, pdstPODetail, dstPODelivery, pdstCPOOfVendor);

			#endregion
			
			// Tinh toan lai cac truong trong PO field
			ReCalculate(pdstPODetail, voMaster);

			while(dstPODelivery.Tables[0].Rows.Count > 0)
			{
                PO_PurchaseOrderMasterVO voNewMaster = new PO_PurchaseOrderMasterVO();
				voNewMaster.BuyerID = voMaster.BuyerID;
				voNewMaster.CCNID = voMaster.CCNID;
				voNewMaster.CurrencyID = voMaster.CurrencyID;
				voNewMaster.DeliveryTermsID = voMaster.DeliveryTermsID;
				voNewMaster.DiscountTermID = voMaster.DiscountTermID;
				voNewMaster.ExchangeRate = voMaster.ExchangeRate;
				voNewMaster.InvToLocID = voMaster.InvToLocID;
				voNewMaster.MakerID = voMaster.MakerID;
				voNewMaster.MakerLocationID = voMaster.MakerLocationID;
				voNewMaster.MasterLocationID = voMaster.MasterLocationID;
				voNewMaster.PartyContactID = voMaster.PartyContactID;
				voNewMaster.PartyID = voMaster.PartyID;
				voNewMaster.ShipToLocID = voMaster.ShipToLocID;
				voNewMaster.VendorLocID = voMaster.VendorLocID;
				voNewMaster.VendorSO = voMaster.VendorSO;
                voNewMaster.OrderDate = voMaster.OrderDate;

				if (blnIsMRP)
				{
					// new biz: insert po type based on vendor type
					DataRow[] drowVendor = dtbVendor.Select("PartyID = " + voMaster.PartyID);
					switch (Convert.ToInt32(drowVendor[0][MST_PartyTable.TYPE_FLD]))
					{
						case (int)PartyTypeEnum.OUTSIDE:
							voNewMaster.PurchaseTypeID = (int)POType.Outside;
							break;
						default:
							if (blnIsLocal)
								voNewMaster.PurchaseTypeID = (int)POType.Domestic;
							else
								voNewMaster.PurchaseTypeID = (int)POType.Import;
							break;
					}
				}
				else // convert from DCP Result, po type always is Outside
				{
				    voNewMaster.PurchaseTypeID = (int)POType.Outside;
				}
				
				DataRow[] rowDeliverys = dstPODelivery.Tables[0].Select("","StartOfSCHEDULEDATE DESC");
				if (rowDeliverys.Length == 0)
				{
				    break;
				}
				DataSet dstNewPODelivery = dstPODelivery.Clone();
				for(int j = rowDeliverys.Length - 1; j >= 0 ; j--)
				{
					DataRow r = dstNewPODelivery.Tables[0].NewRow();
					r.ItemArray = rowDeliverys[j].ItemArray;
					rowDeliverys[j].Delete();
					dstNewPODelivery.Tables[0].Rows.Add(r);
				}

				if(dstNewPODelivery.Tables[0].Rows.Count == 0)
				{
				    break;
				}


				ArrayList arlCPOIDs = new ArrayList();
				if (cboPlanType.Text.Trim() != lblDCP.Text.Trim())
				{
					#region Get CPOID

					foreach (DataRow drvCPO in dstNewPODelivery.Tables[0].Rows)
						arlCPOIDs.Add(drvCPO[MTR_CPOTable.CPOID_FLD].ToString());

					#endregion
				}
				else
				{
					#region Get DCPRESULTDETAILID

					arlCPOIDs.Add(-1);
					foreach (DataRow drvCPO in pdstCPOOfVendor.Rows)
						arlCPOIDs.Add(drvCPO[PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD].ToString());

					#endregion
				}

				DataSet dstNewPODetail = pdstPODetail.Copy();

				if (dstNewPODetail.Tables[0].Rows.Count == 0)
				{
				    break;
				}

				for(int j = dstNewPODetail.Tables[0].Rows.Count - 1; j >= 0; j--)
				{
					DataRow rowDetail = dstNewPODetail.Tables[0].Rows[j];
					DataRow[] rowDels = dstNewPODelivery.Tables[0].Select("ProductID=" + rowDetail["ProductID"]);
					rowDetail["OrderQuantity"] = Convert.ToDecimal(dstNewPODelivery.Tables[0].Compute("SUM(DeliveryQuantity)","ProductID=" + rowDetail["ProductID"]));
					if(rowDels.Length == 0)
						rowDetail.Delete();
				}

				if(dstNewPODetail.Tables[0].Rows.Count == 0)
				{
				    break;
				}
				if(dstNewPODelivery.Tables[0].Rows.Count == 0)
				{
				    break;
				}

				ReCalculate(dstNewPODetail,voNewMaster);
				dstNewPODelivery.Tables[0].Columns["StartOfSCHEDULEDATE"].ColumnName = "StartDate";
			    DateTime deliveryDate = Convert.ToDateTime(dstNewPODelivery.Tables[0].Rows[0][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);

                strMakerCode = "P-" + (boUtil.GetRows(MST_PartyTable.TABLE_NAME, "WHERE PartyID=" + voMaster.PartyID)).Rows[0][MST_PartyTable.CODE_FLD].ToString().Trim();
                if (strMakerCode != string.Empty)
                {
                    string format = "-YYMM-##";
                    if (voNewMaster.PurchaseTypeID == (int)POType.Outside)
                    {
                        format = "-YYMMA-##";
                    }
                    int revision;
                    string poNumber = boUtil.GetNoByMask(PO_PurchaseOrderMasterTable.TABLE_NAME, PO_PurchaseOrderMasterTable.CODE_FLD, strMakerCode, format, deliveryDate, out revision);
                    voNewMaster.Code = poNumber;
                    voNewMaster.ReferenceNo = poNumber;
                    voNewMaster.PORevision = revision;
                }

			    voNewMaster.UserName = SystemProperty.UserName;
			    voNewMaster.LastChange = voMaster.OrderDate;
				voNewMaster.PurchaseOrderMasterID = boPurchaseOrder.AddPOAndDelScheduleImmediate(voNewMaster, dstNewPODetail, dstNewPODelivery, arlCPOIDs);
				// alway break
				break;
			}
			blnConvertPOSuccess = true;
		}

		internal void UpdateNewConvertToPO(PO_PurchaseOrderMasterVO pvoMaster, DataSet pdstPODetail, DataSet pdstPODelivery, DataTable pdtbCPOOfVendor)
		{
			pdtbCPOOfVendor.DefaultView.Sort = ITM_ProductTable.PRODUCTID_FLD;
			foreach(DataRow rowPOLine in pdstPODetail.Tables[0].Rows)
			{
				DataRowView[] drowSameItems = pdtbCPOOfVendor.DefaultView.FindRows(rowPOLine[ITM_ProductTable.PRODUCTID_FLD].ToString());
				DataRow[] rowDeliverys = pdstPODelivery.Tables[0].Select("ProductID = " + rowPOLine["ProductID"]);
				if(rowDeliverys.Length > 0) // re-calculate DeliveryQuanity
				{
					decimal decQuantity = GetQuantityForCPO(drowSameItems[0],
						Convert.ToDecimal(rowDeliverys[0]["OriginalDeliveryQuantity"]));
					rowDeliverys[0]["DeliveryQuantity"] = decQuantity;
					decimal decPrevDeliveryQuantity = Convert.ToDecimal(rowDeliverys[0]["DeliveryQuantity"]);
					decimal decPrevOriginalDeliveryQuantity = Convert.ToDecimal(rowDeliverys[0]["OriginalDeliveryQuantity"]);
					decimal decPrevRedundalQty = decPrevDeliveryQuantity - decPrevOriginalDeliveryQuantity;
					for(int i = 1; i < rowDeliverys.Length; i++)
					{						
						DataRow rowDelivery = rowDeliverys[i];
						decimal decDeliveryQty = Convert.ToDecimal(rowDelivery["OriginalDeliveryQuantity"])	- decPrevRedundalQty;
						decimal decNewQuantity = GetQuantityForCPO(drowSameItems[0],decDeliveryQty);
						rowDelivery["DeliveryQuantity"] = decNewQuantity;
						decPrevRedundalQty = Convert.ToDecimal(rowDelivery["DeliveryQuantity"]) - decDeliveryQty;
					}
				}
			}

			for(int i = pdstPODelivery.Tables[0].Rows.Count - 1; i >= 0; i--)
			{
				DataRow rowDelivery = pdstPODelivery.Tables[0].Rows[i];
				if(Convert.ToDecimal(rowDelivery["DeliveryQuantity"]) == 0)
					rowDelivery.Delete();
			}
			pdstPODelivery.Tables[0].AcceptChanges();
		}

		/// <summary>
		/// Get Start time base on delivery and working day
		/// </summary>
		/// <param name="pdtmDate"></param>
		/// <param name="oStartDate"></param>
		/// <param name="oEndDate"></param>
		/// <param name="pType"></param>
		/// <param name="pdrowDeliverys"></param>
		private void GetStartEndDate(DateTime pdtmDate, ref DateTime oStartDate, ref DateTime oEndDate, PODeliveryTypeEnum pType, DataRow[] pdrowDeliverys, DataSet pdstCalendar)
		{
			oStartDate = new DateTime();
			oEndDate = new DateTime();

			DateTime dtmStartOfSpace = pdtmDate;
			
			DateTime dtmEndOfSpace = new DateTime();

			#region Xac dinh vi tri oStartDate, oEndDate

			if (pType == PODeliveryTypeEnum.Weekly)
			{
				#region By weekly
				int intDayOfWeek = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length -1][PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD]);
				int intHour = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length -1][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
				int intMin = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length -1][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
				int intDayOfWeekDate = (int)pdtmDate.DayOfWeek;
				
				if(pdrowDeliverys.Length == 1)
				{
					#region // if a week has one time delivery

					if(intDayOfWeekDate > intDayOfWeek)
					{
						dtmStartOfSpace = pdtmDate.AddDays(-(double)((int)pdtmDate.DayOfWeek - intDayOfWeek));
						dtmStartOfSpace = new DateTime(dtmStartOfSpace.Year, dtmStartOfSpace.Month, dtmStartOfSpace.Day,intHour,intMin,0);
					}
					else if(intDayOfWeekDate == intDayOfWeek)
					{
						dtmStartOfSpace = pdtmDate;	
						dtmStartOfSpace = new DateTime(dtmStartOfSpace.Year, dtmStartOfSpace.Month, dtmStartOfSpace.Day,intHour,intMin,0);
					}
					else if(intDayOfWeekDate < intDayOfWeek)
					{
						dtmStartOfSpace = pdtmDate.AddDays(-(double)((int)pdtmDate.DayOfWeek - intDayOfWeek));
						dtmStartOfSpace = new DateTime(dtmStartOfSpace.Year, dtmStartOfSpace.Month, dtmStartOfSpace.Day,intHour,intMin,0);
						// alway -7
						dtmStartOfSpace = dtmStartOfSpace.AddDays(-7);
					}
					// correct start 
					if(dtmStartOfSpace > pdtmDate)
					{
						dtmStartOfSpace = dtmStartOfSpace.AddDays(-7);
					}
					// Get EndDate
					dtmEndOfSpace = dtmStartOfSpace.AddDays(7);
					dtmStartOfSpace = GetNearestWorkingDay(dtmStartOfSpace, pdstCalendar);

					#endregion
				}
				else 
				{
					#region // if 1 week has more than one time delivery
					DateTime[] dtmDeliTimes = new DateTime[pdrowDeliverys.Length];
					bool blnOK = false;
					// fill data for each element of delivery date time
					for (int i = 0; i < pdrowDeliverys.Length; i ++)
					{
						intDayOfWeek = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD]);
						intHour = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
						intMin = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
						dtmDeliTimes[i] = new DateTime(pdtmDate.Year, pdtmDate.Month,pdtmDate.Day, intHour, intMin, 0).AddDays((double)intDayOfWeek - (double)pdtmDate.DayOfWeek);
					}
					// check the rule
					for(int i = pdrowDeliverys.Length-1; i >= 0; i--)
					{
						intDayOfWeek = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.WEEKLYDAY_FLD]);
						intHour = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
						intMin = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
						dtmDeliTimes[i] = new DateTime(pdtmDate.Year, pdtmDate.Month,pdtmDate.Day, intHour, intMin, 0).AddDays((double)intDayOfWeek - (double)pdtmDate.DayOfWeek);
						if(dtmDeliTimes[i] <= pdtmDate)
						{
							dtmStartOfSpace = dtmDeliTimes[i];
							// Get EndDate
							if(i + 1 < pdrowDeliverys.Length)
							{
								dtmEndOfSpace = dtmDeliTimes[i+1];
							}
							else // tien len 1 tuan lay ngay dau tien
							{
								dtmEndOfSpace = (dtmDeliTimes[0]).AddDays(7);
							}
							blnOK = true;
							break;
						}
					}
					// If it's not belong to this week then change to prev week
					if(!blnOK)
					{
						dtmStartOfSpace = dtmDeliTimes[pdrowDeliverys.Length-1].AddDays(-7);
						// Get EndDate
						dtmEndOfSpace = dtmDeliTimes[0];
					}
					dtmStartOfSpace = GetNearestWorkingDay(dtmStartOfSpace, pdstCalendar);
					#endregion 
				}
				#endregion
			}
			else if (pType == PODeliveryTypeEnum.Monthly)
			{
				#region By monthly
				int intMonthDate = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length -1][PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
				int intHour = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length -1][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
				int intMin = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length -1][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
				// if a month has one time delivery
				if(pdrowDeliverys.Length == 1)
				{
					#region // Moi thang co 1 lan giao hang
					if(intMonthDate > GetMaxDayOfMonth(pdtmDate))
					{
						intMonthDate = GetMaxDayOfMonth(pdtmDate);
					}
					DateTime dtmDateOfMonth = new DateTime(pdtmDate.Year,pdtmDate.Month,intMonthDate,intHour,intMin,0);
					// Lui 1 thang
					if(dtmDateOfMonth > pdtmDate)
					{
						dtmDateOfMonth = dtmDateOfMonth.AddMonths(-1);
						// Get max day of month
						intMonthDate = GetMaxDayOfMonth(dtmDateOfMonth);
						if(intMonthDate > Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length -1][PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]))
							intMonthDate = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length -1][PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
						dtmDateOfMonth = dtmDateOfMonth.AddDays(intMonthDate - dtmDateOfMonth.Day);
					}
					dtmEndOfSpace = dtmDateOfMonth.AddMonths(1);
					dtmStartOfSpace = GetNearestWorkingDay(dtmDateOfMonth, pdstCalendar);
					#endregion
				}
				else 
				{
					#region // if a month has more than one time delivery
					DateTime[] dtmDeliTimes = new DateTime[pdrowDeliverys.Length];
					bool blnOK = false;
					// fill data for each element of delivery date time
					for (int i = 0; i < pdrowDeliverys.Length; i ++)
					{
						intMonthDate = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
						intHour = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
						intMin = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
						if (intMonthDate > DateTime.DaysInMonth(pdtmDate.Year,  pdtmDate.Month))
							intMonthDate = DateTime.DaysInMonth(pdtmDate.Year,  pdtmDate.Month);
						DateTime dtmDate = new DateTime(pdtmDate.Year, pdtmDate.Month,pdtmDate.Day, intHour, intMin, 0).AddDays((double)intMonthDate - (double)pdtmDate.Day);
						dtmDeliTimes[i] = dtmDate;
					}
					// check the rule
					for(int i = pdrowDeliverys.Length-1; i >= 0; i--)
					{
						intMonthDate = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.MONTHLYDATE_FLD]);
						intHour = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
						intMin = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
						// Day of current month out of range
						if(intMonthDate > GetMaxDayOfMonth(pdtmDate))
						{
							intMonthDate = GetMaxDayOfMonth(pdtmDate);
						}
						DateTime dtmDate = new DateTime(pdtmDate.Year, pdtmDate.Month,pdtmDate.Day, intHour, intMin, 0).AddDays((double)intMonthDate - (double)pdtmDate.Day);
						//dtmDeliTimes[i] = dtmDate;
						if(dtmDate <= pdtmDate)
						{
							dtmStartOfSpace = dtmDate;
							// Get EndDate
							if(i + 1 < pdrowDeliverys.Length)
							{
								dtmEndOfSpace = dtmDeliTimes[i+1]; // lay ngay ke tiep
							}
							else // tien len 1 thang lay ngay dau tien
							{
								dtmEndOfSpace = (dtmDeliTimes[0]).AddMonths(1);
							}

							blnOK = true;
							break;
						}
					}
					// If it's not belong to this week then change to prev month
					if(!blnOK)
					{
						dtmStartOfSpace = dtmDeliTimes[pdrowDeliverys.Length-1].AddMonths(-1);
						// Get EndDate
						dtmEndOfSpace = dtmDeliTimes[0]; // lay ngay dau tien cua thang nay
					}

					dtmStartOfSpace = GetNearestWorkingDay(dtmStartOfSpace, pdstCalendar);
					#endregion
				}
				#endregion
			}
			else if (pType == PODeliveryTypeEnum.Daily)
			{
				#region By daily
				int intHour = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length -1][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
				int intMin = Convert.ToInt32(pdrowDeliverys[pdrowDeliverys.Length -1][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
				if(pdrowDeliverys.Length == 1)
				{
					#region // Moi ngay co 1 gio giao hang
					DateTime dtmDate = new DateTime(pdtmDate.Year,pdtmDate.Month,pdtmDate.Day,intHour,intMin,0);
					// Lui 1 ngay
					if(dtmDate > pdtmDate)
					{
						dtmDate = dtmDate.AddDays(-1);
					}
					dtmEndOfSpace = dtmDate.AddDays(1);
					dtmStartOfSpace = GetNearestWorkingDay(dtmDate, pdstCalendar);
					#endregion
				}
				else 
				{
					#region // if a day has more than one time delivery
					DateTime[] dtmDeliTimes = new DateTime[pdrowDeliverys.Length];
					bool blnOK = false;
					// fill data for each element of delivery date time
					for (int i = 0; i < pdrowDeliverys.Length; i ++)
					{
						intHour = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
						intMin = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
						dtmDeliTimes[i] = new DateTime(pdtmDate.Year, pdtmDate.Month,pdtmDate.Day, intHour, intMin, 0);
					}
					// check the rule
					for(int i = pdrowDeliverys.Length-1; i >= 0; i--)
					{
						intHour = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELHOUR_FLD]);
						intMin = Convert.ToInt32(pdrowDeliverys[i][PO_VendorDeliveryScheduleTable.DELMIN_FLD]);
						dtmDeliTimes[i] = new DateTime(pdtmDate.Year, pdtmDate.Month,pdtmDate.Day, intHour, intMin, 0);
						if(dtmDeliTimes[i] <= pdtmDate)
						{
							dtmStartOfSpace = dtmDeliTimes[i];
							blnOK = true;
							// Get EndDate
							if(i + 1 < pdrowDeliverys.Length)
							{
								dtmEndOfSpace = dtmDeliTimes[i+1]; // lay ngay ke tiep
							}
							else // tien len 1 ngay lay lan dau tien
							{
								dtmEndOfSpace = (dtmDeliTimes[0]).AddDays(1);
							}
							break;
						}
					}
					// If it's not belong to this day then change to prev day
					if(!blnOK)
					{
						dtmStartOfSpace = dtmDeliTimes[pdrowDeliverys.Length-1].AddDays(-1);
						dtmEndOfSpace = dtmDeliTimes[0];
					}

					dtmStartOfSpace = GetNearestWorkingDay(dtmStartOfSpace, pdstCalendar);
					#endregion
				}
				#endregion
			}

			#endregion

			oStartDate = dtmStartOfSpace;
			oEndDate = dtmEndOfSpace;
		
		}

		/// <summary>
		/// Group all PODelivery by Delivery Policy of Vendor
		/// </summary>
		/// <param name="pdtbPODelivery"></param>
		/// <param name="pdrowDeliverys"></param>
		/// <param name="pType"></param>
		/// <returns></returns>
		private DataTable GroupPODeliverys(DataTable pdtbPODelivery, DataRow[] pdrowDeliverys, PODeliveryTypeEnum pType, DateTime pdtmFirstValidWorkDay, DataSet dstCalendar, bool blnIsLocal)
		{
			DataTable dtbResultAfterGroup = pdtbPODelivery.Clone();
			
			pdtbPODelivery.DefaultView.Sort = PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;
			DateTime dtmStart = new DateTime(), dtmEnd = new DateTime();
			DateTime dtmOriginSchedule = DateTime.MinValue;
			if (blnIsLocal)
				dtmOriginSchedule = (DateTime) pdtbPODelivery.DefaultView[0][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD];
			else
				dtmOriginSchedule = (DateTime) pdtbPODelivery.DefaultView[pdtbPODelivery.DefaultView.Count - 1][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD];
			GetStartEndDate(dtmOriginSchedule, ref dtmStart, ref dtmEnd, pType, pdrowDeliverys, dstCalendar);

			bool okNewSpace = true;
			if (blnIsLocal)
			{
				for (int i =0; i <pdtbPODelivery.DefaultView.Count; i++)
				{
					if ( (DateTime) pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] >= dtmStart
						&& (DateTime) pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] < dtmEnd)
					{
						#region if SCHEDULEDATE_FLD thuoc [StartDate,EndDate)

						//add to Deleviry line
						if (okNewSpace)
						{
							#region Tao moi delivery line

							DataRow drowData = dtbResultAfterGroup.NewRow();

							#region verify schedule date
							DateTime dtmScheduleDate = dtmStart;
							DateTime dtmMyStart = dtmStart, dtmMyEnd = dtmEnd;
							while (dtmMyEnd < pdtmFirstValidWorkDay)
							{
								switch (pType)
								{
									case PODeliveryTypeEnum.Daily:
										dtmMyStart = dtmMyStart.AddDays(1);
										dtmMyEnd = dtmMyEnd.AddDays(1);
										break;
									case PODeliveryTypeEnum.Weekly:
										dtmMyStart = dtmMyStart.AddDays(7);
										dtmMyEnd = dtmMyEnd.AddDays(7);
										break;
									default:
										dtmMyStart = dtmMyStart.AddMonths(1);
										dtmMyEnd = dtmMyEnd.AddMonths(1);
										break;
								}
							}
							dtmScheduleDate = (dtmMyStart < pdtmFirstValidWorkDay) ? dtmMyEnd : dtmMyStart;
							#endregion

							dtmScheduleDate = GetNearestWorkingDay(dtmScheduleDate, dstCalendar);
							drowData[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmScheduleDate;
							drowData[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] = pdtbPODelivery.DefaultView[i][PO_PurchaseOrderDetailTable.PRODUCTID_FLD];
							drowData[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];
						
							drowData["OriginalDELIVERYQUANTITY"] = pdtbPODelivery.DefaultView[i]["OriginalDELIVERYQUANTITY"];
							drowData["CPOID"] = pdtbPODelivery.DefaultView[i]["CPOID"];

							double decFixLT = Convert.ToDouble(pdtbPODelivery.DefaultView[i]["FixLT"]);
							drowData["StartOfSCHEDULEDATE"] = dtmScheduleDate.AddSeconds(-decFixLT);
							dtbResultAfterGroup.Rows.Add(drowData);
							okNewSpace = false;

							#endregion
						}
						else
						{
							#region Tang quantity cho delivery line

							dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]
								= (decimal) dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]
								+ (decimal)	pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];

							dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1]["OriginalDELIVERYQUANTITY"]
								= (decimal) dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1]["OriginalDELIVERYQUANTITY"]
								+ (decimal)	pdtbPODelivery.DefaultView[i]["OriginalDELIVERYQUANTITY"];

							#endregion
						}

						#endregion
					}
					else
					{
						#region SCHEDULEDATE_FLD khong thuoc [StartDate,EndDate)

						GetStartEndDate( (DateTime) pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD],
							ref dtmStart, ref dtmEnd, pType, pdrowDeliverys, dstCalendar);
						//add to Delivery line
						okNewSpace = false;
					
						#region verify schedule date
						DateTime dtmScheduleDate = dtmStart;
						DateTime dtmMyStart = dtmStart, dtmMyEnd = dtmEnd;
						while (dtmMyEnd < pdtmFirstValidWorkDay)
						{
							switch (pType)
							{
								case PODeliveryTypeEnum.Daily:
									dtmMyStart = dtmMyStart.AddDays(1);
									dtmMyEnd = dtmMyEnd.AddDays(1);
									break;
								case PODeliveryTypeEnum.Weekly:
									dtmMyStart = dtmMyStart.AddDays(7);
									dtmMyEnd = dtmMyEnd.AddDays(7);
									break;
								default:
									dtmMyStart = dtmMyStart.AddMonths(1);
									dtmMyEnd = dtmMyEnd.AddMonths(1);
									break;
							}
						}
						dtmScheduleDate = (dtmMyStart < pdtmFirstValidWorkDay) ? dtmMyEnd : dtmMyStart;
						#endregion

						dtmScheduleDate = GetNearestWorkingDay(dtmScheduleDate, dstCalendar);

						// try to check the schedule date
						string strFilter = PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "='" + dtmScheduleDate.ToString("G") + "'";
						if (dtbResultAfterGroup.Select(strFilter).Length > 0)
						{
							#region Tang quantity cho delivery line

							dtbResultAfterGroup.Select(strFilter)[0][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]
								= (decimal) dtbResultAfterGroup.Select(strFilter)[0][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]
								+ (decimal)	pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];

							dtbResultAfterGroup.Select(strFilter)[0]["OriginalDELIVERYQUANTITY"]
								= (decimal) dtbResultAfterGroup.Select(strFilter)[0]["OriginalDELIVERYQUANTITY"]
								+ (decimal)	pdtbPODelivery.DefaultView[i]["OriginalDELIVERYQUANTITY"];

							#endregion
						}
						else
						{
							#region add new delivery line

							DataRow drowData = dtbResultAfterGroup.NewRow();
							drowData[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmScheduleDate;
							drowData[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] = pdtbPODelivery.DefaultView[i][PO_PurchaseOrderDetailTable.PRODUCTID_FLD];
							drowData[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];

							drowData["OriginalDELIVERYQUANTITY"] = pdtbPODelivery.DefaultView[i]["OriginalDELIVERYQUANTITY"];
							drowData["CPOID"] = pdtbPODelivery.DefaultView[i]["CPOID"];

							double decFixLT = Convert.ToDouble(pdtbPODelivery.DefaultView[i]["FixLT"]);
							drowData["StartOfSCHEDULEDATE"] = dtmScheduleDate.AddSeconds(-decFixLT);

							dtbResultAfterGroup.Rows.Add(drowData);

							#endregion
						}

						#endregion
					}
				}
			}
			else
			{
				for (int i =0; i <pdtbPODelivery.DefaultView.Count; i++)
				{
					//add to Deleviry line
					if (okNewSpace == true)
					{
						#region Tao moi delivery line

						DataRow drowData = dtbResultAfterGroup.NewRow();
						DateTime dtmScheduleDate = dtmStart.AddMonths(-1);
						
						dtmScheduleDate = GetNearestWorkingDay(dtmScheduleDate, dstCalendar);
						drowData[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dtmScheduleDate;
						drowData[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] = pdtbPODelivery.DefaultView[i][PO_PurchaseOrderDetailTable.PRODUCTID_FLD];
						drowData[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];
						
						drowData["OriginalDELIVERYQUANTITY"] = pdtbPODelivery.DefaultView[i]["OriginalDELIVERYQUANTITY"];
						drowData["CPOID"] = pdtbPODelivery.DefaultView[i]["CPOID"];

						double decFixLT = Convert.ToDouble(pdtbPODelivery.DefaultView[i]["FixLT"]);
						drowData["StartOfSCHEDULEDATE"] = dtmScheduleDate.AddSeconds(-decFixLT);
						dtbResultAfterGroup.Rows.Add(drowData);
						okNewSpace = false;

						#endregion
					}
					else
					{
						#region Tang quantity cho delivery line

						dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]
							= (decimal) dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD]
							+ (decimal)	pdtbPODelivery.DefaultView[i][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];

						dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1]["OriginalDELIVERYQUANTITY"]
							= (decimal) dtbResultAfterGroup.Rows[dtbResultAfterGroup.Rows.Count -1]["OriginalDELIVERYQUANTITY"]
							+ (decimal)	pdtbPODelivery.DefaultView[i]["OriginalDELIVERYQUANTITY"];

						#endregion
					}
				}
			}
			
			return dtbResultAfterGroup;
		}
		
		/// <summary>
		/// Tinh toan cac thong so tren form PO
		/// </summary>
		/// <param name="pdstPODetail"></param>
		/// <param name="pvoMaster"></param>
		private void ReCalculate(DataSet pdstPODetail, PO_PurchaseOrderMasterVO pvoMaster)
		{
			if(pdstPODetail.Tables.Count > 0)
			{
                foreach (DataRow objRow in pdstPODetail.Tables[0].Rows)
                {
                    if (objRow.RowState == DataRowState.Deleted) continue;
                    //•	If VAT field checked then the system calculate VAT amount base on unit price and VAT percent of the item
                    if (objRow[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] == DBNull.Value)
                    {
                        continue;
                    }
                    decimal decDiscountAmount = 0;
                    if (objRow[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
                        decDiscountAmount = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString());

                    try
                    {
                        objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] = (decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount)
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.VAT_FLD].ToString()) / 100;
                        pvoMaster.TotalVAT += (decimal)objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD];
                    }
                    catch
                    {
                        objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] = 0;
                    }
                    //•	If Export Tax field checked then the system calculate export tax amount base on unit price and export tax percent of the item 
                    try
                    {
                        objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] = (decimal.Parse(objRow[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString())
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.UNITPRICE_FLD].ToString()) - decDiscountAmount)
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.IMPORTTAX_FLD].ToString()) / 100;
                        pvoMaster.TotalImportTax += (decimal)objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD];
                    }
                    catch
                    {
                        objRow[PO_PurchaseOrderDetailTable.IMPORTTAXAMOUNT_FLD] = 0;
                    }
                    //•	If Special Tax field checked then the system calculate special tax amount base on unit price and special tax percent of the item 
                    try
                    {
                        decimal decImpTax, decVatAmount;
                        if ((objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] != null) && (objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value))
                        {
                            decVatAmount = decimal.Parse(objRow[PO_PurchaseOrderDetailTable.VATAMOUNT_FLD].ToString());
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
                            * decimal.Parse(objRow[PO_PurchaseOrderDetailTable.SPECIALTAX_FLD].ToString()) / 100;
                    }
                    catch
                    {
                    }
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
                        pvoMaster.TotalSpecialTax += (decimal)objRow[PO_PurchaseOrderDetailTable.SPECIALTAXAMOUNT_FLD];
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
                        pvoMaster.TotalAmount += (decimal)objRow[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD];
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
                        pvoMaster.TotalNetAmount += (decimal)objRow[PO_PurchaseOrderDetailTable.NETAMOUNT_FLD];
                    }
                    catch
                    {
                        // do nothing
                    }
                }
			}
		}

		/// <summary>
		/// Gan gia tri exchange rate
		/// </summary>
		/// <param name="pintCurrencyID"></param>
		/// <param name="pdtmDate"></param>
		/// <returns></returns>
		private decimal FillExchangeRate(int pintCurrencyID, DateTime pdtmDate)
		{
			// Fill Exch. Rate if the system configured the exchange rate get form Exchange Rate Table
			// based on currency and transaction date (begin date<= transaction date <= end date and approved)
			const decimal DEFAULT_RATE = 1;
			const string  METHOD_NAME = THIS + ".FillExchangeRate()";
			int intExchangeRateID = 0;
			if (pintCurrencyID == 0) return intExchangeRateID;
			//•	If the currency is same as base(Home - CuongNT fixed) currency then the system automatically fill the number 1 to exchange rate field
			if(pintCurrencyID == SystemProperty.HomeCurrencyID)
			{
				return DEFAULT_RATE;
			}
			try
			{
				// Input Transaction date before execute this function
				PurchaseOrderBO boOrder = new PurchaseOrderBO();
				MST_ExchangeRateVO voExchange = (MST_ExchangeRateVO) boOrder.GetExchangeRate(pintCurrencyID, pdtmDate);
				if(voExchange.ExchangeRateID == 0)
				{
					// Do not found any Exchange Rate records which (begin effective date <= the current date <= end of effective date)
					return DEFAULT_RATE;
				}
				// fill value and return
				return voExchange.Rate;
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
		/// Tim ngay lam viec gan nhat
		/// </summary>
		/// <param name="pdtmDate"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		DateTime GetNearestWorkingDay(DateTime pdtmDate,DataSet pdstCalendar)
		{
			while(IsOffDay(pdtmDate,pdstCalendar))
			{
				pdtmDate = pdtmDate.AddDays(-1);
			}
			return pdtmDate;
		}

		/// <summary>
		/// Check the day is off day
		/// Return true if isoffday else return false
		/// </summary>
		/// <param name="pdtmDate"></param>
		/// <param name="pdstCalendar"></param>
		/// <returns></returns>
		private bool IsOffDay(DateTime pdtmDate, DataSet pdstCalendar)
		{
			const string METHOD_NAME = THIS + ".IsOffDay()";
			DayOfWeek objWeekDay = pdtmDate.DayOfWeek;
			DataRow[] drows = pdstCalendar.Tables[0].Select(MST_WorkingDayMasterTable.YEAR_FLD + " = " + pdtmDate.Year);
			DataRow[] drowDetails = pdstCalendar.Tables[1].Select(MST_WorkingDayDetailTable.OFFDAY_FLD + " = '" + pdtmDate.Date + "'");
			if(drowDetails.Length > 0)
			{
				return true;
			}
			if(drows.Length == 0) // return true;
			{
				throw new PCSBOException(ErrorCode.MESSAGE_DCP_SETTING_WORKING_CALENDAR,METHOD_NAME,null);
				// return true;
			}
			if(objWeekDay == DayOfWeek.Sunday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.SUN_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Saturday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.SAT_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Friday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.FRI_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Thursday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.THU_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Wednesday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.WED_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Tuesday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.TUE_FLD].ToString()) == true) return false;
			}
			else if(objWeekDay == DayOfWeek.Monday)
			{
				if(bool.Parse(drows[0][MST_WorkingDayMasterTable.MON_FLD].ToString()) == true) return false;
			}
			return true;
		}
		
		/// <summary>
		/// So ngay lon nhat trong 1 thang bat ky
		/// </summary>
		/// <param name="pdtmTemp"></param>
		/// <returns></returns>
		int GetMaxDayOfMonth(DateTime pdtmTemp)
		{
			if(pdtmTemp.Month == 12) return 31;
			else
			{
				pdtmTemp = new DateTime(pdtmTemp.Year,pdtmTemp.Month+1,1,0,0,0);
				return (pdtmTemp.AddDays(-1)).Day;
			}
		}
		/// <summary>
		/// Check if the vendor is Local or Domestic
		/// </summary>
		/// <param name="pintVendorID">Vendor</param>
		/// <returns></returns>
		private bool IsLocal(int pintVendorID)
		{
			DataRow[] drowVendor = dtbVendor.Select(MST_PartyTable.PARTYID_FLD + "=" + pintVendorID);
			int intCountryID = 0;
			try
			{
				intCountryID = Convert.ToInt32(drowVendor[0][MST_PartyTable.COUNTRYID_FLD]);
			}
			catch{}
			return intCountryID == SystemProperty.CountryID;
		}
		#endregion		

		#region Convert 2 WO - Tuan DM
		
		private void ThreadForGenNewWO()
		{
			const string METHOD_NAME = THIS + ".ThreadForGenNewWO()";
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (dgrdData.RowCount == 0) return;
				DataTable dtbTableOfWorkOrder = dtbCPODetail.Clone();
				DataSet dstWODetailSchema = boWorkOrder.GetWODetailByMaster(0);
				DataView dtwAllCPOAffterSort = (dtbCPODetail.Copy()).DefaultView;
				dtwAllCPOAffterSort.RowFilter = MTR_CPODS.SELECT_COLUMN + Constants.EQUAL + 1.ToString()
					+ " AND " + MST_WorkCenterTable.PRODUCTIONLINEID_FLD + " > 0";
				dtwAllCPOAffterSort.Sort = MST_WorkCenterTable.PRODUCTIONLINEID_FLD + ", " + ITM_ProductTable.PRODUCTID_FLD + "," + MTR_CPOTable.STARTDATE_FLD;
				int i =0;
				if (dtwAllCPOAffterSort.Count == 0)
				{
					this.Cursor = Cursors.Default;
					return;
				}
				btnNewPOConvert.Enabled = false;
				btnExistingPOConvert.Enabled = false;
				btnNewWOConvert.Enabled = false;
				btnExistingWOConvert.Enabled = false;
				while (i < dtwAllCPOAffterSort.Count)
				{
					dtbTableOfWorkOrder.Rows.Clear();
					dtbTableOfWorkOrder.AcceptChanges();
					DataSet dstWODetail = dstWODetailSchema.Clone();
					for (int j =i; j < dtwAllCPOAffterSort.Count; j++)
					{
						if ((int)dtwAllCPOAffterSort[i][MST_WorkCenterTable.PRODUCTIONLINEID_FLD]
							==(int)dtwAllCPOAffterSort[j][MST_WorkCenterTable.PRODUCTIONLINEID_FLD])
						{
							dtbTableOfWorkOrder.ImportRow(dtwAllCPOAffterSort[j].Row);
						}
						else
						{
							DataRow[] drowsSort = dtbTableOfWorkOrder.Select(string.Empty, MTR_CPOTable.STARTDATE_FLD + " ASC, " + ITM_ProductTable.PRODUCTID_FLD + " ASC");
							DataTable dtbRealData = dtbTableOfWorkOrder.Clone();
							foreach (DataRow drow in drowsSort)
								dtbRealData.ImportRow(drow);
							ConvertToNewWorkOrder(dtbRealData, dstWODetail);
							i = j;
							break;
						}
						if (j == dtwAllCPOAffterSort.Count-1) 
						{
							DataRow[] drowsSort = dtbTableOfWorkOrder.Select(string.Empty, MTR_CPOTable.STARTDATE_FLD + " ASC, " + ITM_ProductTable.PRODUCTID_FLD + " ASC");
							DataTable dtbRealData = dtbTableOfWorkOrder.Clone();
							foreach (DataRow drow in drowsSort)
								dtbRealData.ImportRow(drow);
							ConvertToNewWorkOrder(dtbRealData, dstWODetail);
							i = j+1;
						}
					}
				}
				if (cboPlanType.Text.Trim() != lblDCP.Text.Trim())
				{
					btnNewPOConvert.Enabled = true;
					btnExistingPOConvert.Enabled = true;
				}
				btnNewWOConvert.Enabled = true;
				btnExistingWOConvert.Enabled = true;
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
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
				this.Cursor = Cursors.Default;
			}
		}

		/// <summary>
		/// Convert to new wo
		/// </summary>
		/// <param name="pdstCPOOfWorkCenter"></param>
		/// <param name="pdstWODetail"></param>
		private void ConvertToNewWorkOrder(DataTable pdstCPOOfWorkCenter, DataSet pdstWODetail)
		{
			PRO_WorkOrderMasterVO voWOMaster = new PRO_WorkOrderMasterVO();
			//create wo master automatically
			voWOMaster.CCNID = (int) cboCCN.SelectedValue;
			voWOMaster.MasterLocationID = Convert.ToInt32(txtMasLoc.Tag);
			voWOMaster.TransDate = DateTime.Parse(boUtils.GetDBDate().ToShortDateString());
			voWOMaster.ProductionLineID = Convert.ToInt32(pdstCPOOfWorkCenter.Rows[0][PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD]);
			if(cboPlanType.Text == lblDCP.Text)
			{
				voWOMaster.DCOptionMasterID = Convert.ToInt32(txtCycle.Tag);
			}
			// HACK: Trada 21-03-2006
			//Make prefix for work order 
			//Get Production Line Code
			ProductionLineBO boProductionLine = new ProductionLineBO();
			DataTable dtbProductionLine = boProductionLine.GetProductionLineCode(voWOMaster.ProductionLineID);
			string strWorkOrderNo = string.Empty;
			string strFormat_Number = "####";
			const string WONOMAX = "WorkOrderNoMax";

			if (dtbProductionLine.Rows.Count > 0)
			{
				strWorkOrderNo = dtbProductionLine.Rows[0][PRO_ProductionLineTable.CODE_FLD].ToString();
			}
			//Get Year, Month and Version of DCOption master
			PCSComProduction.DCP.BO.DCOptionsBO boDCOptions = new DCOptionsBO();
			DataRow drowDCOptionMaster = boDCOptions.GetDCOptionMaster(int.Parse(txtCycle.Tag.ToString()));
			DateTime dtmPlanningPeriod = (DateTime) drowDCOptionMaster[PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD];
			strWorkOrderNo += dtmPlanningPeriod.Year.ToString();
			if (dtmPlanningPeriod.Month >= 10)
			{
				strWorkOrderNo += "-" + dtmPlanningPeriod.Month.ToString();
			}
			else
				strWorkOrderNo += "-0" + dtmPlanningPeriod.Month.ToString();
			
			strWorkOrderNo += "-V" + drowDCOptionMaster[PRO_DCOptionMasterTable.VERSION_FLD].ToString() + "-";
			//Build query
			string strSql = String.Empty;
			strSql =  " SELECT max(" + PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ") WorkOrderNoMax " ;
			strSql += " FROM " + PRO_WorkOrderMasterTable.TABLE_NAME ;
			strSql += " WHERE " + PRO_WorkOrderMasterTable.WORKORDERNO_FLD + " like '"+ strWorkOrderNo + "%'" ;

			WorkOrderBO boWorkOrder = new WorkOrderBO();
			DataSet dstWorkOrderNo = new DataSet();
			dstWorkOrderNo = boWorkOrder.GetWorkOrderNo(strSql);
			if (dstWorkOrderNo.Tables[0].Rows.Count == 0)
			{
				strFormat_Number = "1".PadLeft(strFormat_Number.Length,'0');
				strWorkOrderNo += strFormat_Number;
			}
			else
			{
				int intNumberLength = strFormat_Number.Length;
				string strMaxValue = dstWorkOrderNo.Tables[0].Rows[0][WONOMAX].ToString();
				int intNextValue = 0;
				try 
				{
					intNextValue = int.Parse(strMaxValue.Substring(strWorkOrderNo.Length)) + 1;
				}
				catch 
				{
					//Find to the second max value to parse
					strSql = "select WorkOrderNo from pro_workordermaster where workorderno like '"
						+ strWorkOrderNo + "%' order by WorkOrderNo desc";
					dstWorkOrderNo = boWorkOrder.GetWorkOrderNo(strSql);
					int i = 0;
					while (i < dstWorkOrderNo.Tables[0].Rows.Count)
					{
						try
						{
							intNextValue = int.Parse(dstWorkOrderNo.Tables[0].Rows[i][PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString().Substring(strWorkOrderNo.Length));
							break;
						}
						catch
						{
							i++;
						}
					}
					intNextValue += 1;
				}
				strWorkOrderNo += intNextValue.ToString().PadLeft(intNumberLength,'0');
			}
			voWOMaster.WorkOrderNo = strWorkOrderNo;
			

			// END: Trada 21-03-2006



            var list = (from obj in SystemProperty.TableMenuEntry
                        where obj.FormLoad == WorkOrder.THIS
                        orderby obj.Menu_EntryID ascending
                        select obj).ToList<Sys_Menu_Entry>();
			#region  DEL Trada 22-03-2006
			//			if(drowMenus.Length > 0)
//			{
//				voWOMaster.WorkOrderNo = new UtilsBO().GetNoByMask(drowMenus[0][Sys_Menu_EntryTable.TABLENAME_FLD].ToString(), 
//					drowMenus[0][Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD].ToString(), 
//					drowMenus[0][Sys_Menu_EntryTable.PREFIX_FLD].ToString(),
//					drowMenus[0][Sys_Menu_EntryTable.TRANSFORMAT_FLD].ToString());
//			}
//			else
//				voWOMaster.WorkOrderNo = new UtilsBO().GetNoByMask(string.Empty, string.Empty, string.Empty, string.Empty);
			
			#endregion	

			//create wo detail
			int intLine = 0;
			
			if (pdstCPOOfWorkCenter.Columns.IndexOf(MTR_CPOTable.TABLE_NAME + MTR_CPOTable.STARTDATE_FLD) == -1)
			{
				pdstCPOOfWorkCenter.Columns.Add(MTR_CPOTable.TABLE_NAME + MTR_CPOTable.STARTDATE_FLD, typeof(DateTime));
				pdstCPOOfWorkCenter.Columns.Add(MTR_CPOTable.TABLE_NAME + MTR_CPOTable.DUEDATE_FLD, typeof(DateTime));
			}
			for (int i =0; i < pdstCPOOfWorkCenter.Rows.Count; i++)
			{
				DateTime dtmStartDate = (DateTime) pdstCPOOfWorkCenter.Rows[i][MTR_CPOTable.STARTDATE_FLD];
				pdstCPOOfWorkCenter.Rows[i][MTR_CPOTable.TABLE_NAME + MTR_CPOTable.STARTDATE_FLD] = new DateTime(dtmStartDate.Year, dtmStartDate.Month, dtmStartDate.Day, dtmStartDate.Hour, 0, 0);//.ToString(Constants.DATETIME_FORMAT_HOUR);

				DateTime dtmDueDate = (DateTime) pdstCPOOfWorkCenter.Rows[i][MTR_CPOTable.DUEDATE_FLD];
				pdstCPOOfWorkCenter.Rows[i][MTR_CPOTable.TABLE_NAME + MTR_CPOTable.DUEDATE_FLD] = new DateTime(dtmDueDate.Year, dtmDueDate.Month, dtmDueDate.Day, dtmDueDate.Hour, 0, 0);//.ToString(Constants.DATETIME_FORMAT_HOUR);
			}
			
			int j =0;
			string strLastProductID = string.Empty;
			DateTime dtmLastStart = DateTime.MinValue;
			DateTime dtmLastDue = DateTime.MinValue;
			while (j < pdstCPOOfWorkCenter.Rows.Count)
			{
				#region edited by dungla, fix bug overcome item in the list

				DateTime dtmStartDate = (DateTime)pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.TABLE_NAME + MTR_CPOTable.STARTDATE_FLD];
				DateTime dtmDueDate = (DateTime)pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.TABLE_NAME + MTR_CPOTable.DUEDATE_FLD];
				string strProductID = pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.PRODUCTID_FLD].ToString();
				if (dtmLastStart.Equals(dtmStartDate) && dtmLastDue.Equals(dtmDueDate) && strLastProductID.Equals(strProductID))
				{
					j++;
					continue;
				}
				dtmLastStart = dtmStartDate;
				dtmLastDue = dtmDueDate;
				strLastProductID = strProductID;

				#endregion

				string strSelect = MTR_CPOTable.TABLE_NAME + MTR_CPOTable.STARTDATE_FLD + "='" + pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.TABLE_NAME + MTR_CPOTable.STARTDATE_FLD].ToString() + "'"
					+ " and " + MTR_CPOTable.TABLE_NAME + MTR_CPOTable.DUEDATE_FLD + "='" + pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.TABLE_NAME + MTR_CPOTable.DUEDATE_FLD].ToString() + "'"
					+ " and " + MTR_CPOTable.PRODUCTID_FLD + " = " + strProductID;
				DataRow[] drowSames = pdstCPOOfWorkCenter.Select(strSelect);
				DataRow drowWODetail = pdstWODetail.Tables[0].NewRow();
				drowWODetail[PRO_WorkOrderDetailTable.LINE_FLD] = ++intLine;
				drowWODetail[ITM_ProductTable.PRODUCTID_FLD] = pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.PRODUCTID_FLD];
				drowWODetail[ITM_ProductTable.STOCKUMID_FLD] = pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.STOCKUMID_FLD];
				drowWODetail[PRO_WorkOrderDetailTable.STARTDATE_FLD] = pdstCPOOfWorkCenter.Rows[j][MTR_CPOTable.STARTDATE_FLD];
				drowWODetail[PRO_WorkOrderDetailTable.DUEDATE_FLD] = drowSames[drowSames.Length-1][MTR_CPOTable.DUEDATE_FLD];
				drowWODetail[PRO_WorkOrderDetailTable.STATUS_FLD] = WOLineStatus.Unreleased;
				try
				{
					drowWODetail[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD] = 
						Convert.ToDecimal(pdstCPOOfWorkCenter.Compute("SUM(" + MTR_CPOTable.QUANTITY_FLD + ")", strSelect));
				}
				catch{}
				j++;
				pdstWODetail.Tables[0].Rows.Add(drowWODetail);
			}

			//update master and detail
			ArrayList arlCPOIDs = new ArrayList();
			if (cboPlanType.Text.Trim() == PlanTypeEnum.MPS.ToString())
			{
				foreach (DataRow drvCPO in pdstCPOOfWorkCenter.Rows)
				{
					arlCPOIDs.Add(drvCPO[MTR_CPOTable.CPOID_FLD].ToString());
				}
			}
			else
			{
				foreach (DataRow drvCPO in pdstCPOOfWorkCenter.Rows)
				{
					arlCPOIDs.Add(drvCPO[PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD].ToString());
				}
			}
			voWOMaster.WorkOrderMasterID = boWorkOrder.AddNewWOImmediately(voWOMaster, pdstWODetail, arlCPOIDs, cboPlanType.Text.Trim());
			if (voWOMaster.WorkOrderMasterID != 0)
                new UtilsBO().UpdateUserNameModifyTransaction(SystemProperty.UserName, list[0].TableName, PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD, voWOMaster.WorkOrderMasterID);
			blnConvertWOSuccess = true;
		}		

		#endregion

		#region HACKED by Tuan TQ, Nov 04, 2005: Change request
		
		private void dtmDate_VisibleChanged(object sender, EventArgs e)
		{
			dtmDate.TextDetached = !dtmDate.Visible;
		}

		private void numQuantity_VisibleChanged(object sender, EventArgs e)
		{
			numQuantity.TextDetached = !numQuantity.Visible;
		}

		//Add: production line in searching condition area.
		//Double click on grid: open Work Order or Purchase Order

		/// <summary>
		/// Fill related data on controls when select Production Line
		/// </summary>
		/// <param name="pblnAlwaysShowDialog"></param>
		private bool SelectProductionLine(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{				
				Hashtable htbCriteria = new Hashtable();				

				//Call OpenSearchForm for selecting Production Line
				DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), htbCriteria, pblnAlwaysShowDialog);
				
				//If has Production Line matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
					txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];

					//Reset Modified status
					txtProductionLine.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtProductionLine.Focus();
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

		private void btnProductionLine_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
			
			try
			{
				SelectProductionLine(METHOD_NAME, true);
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
		
		private void txtProductionLine_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";

			try
			{
				if(txtProductionLine.Text.Length == 0)
				{
					txtProductionLine.Tag =ZERO_STRING;
					return;
				}
				else if(!txtProductionLine.Modified)
				{
					return;
				}

				e.Cancel = !SelectProductionLine(METHOD_NAME, false);
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
		
		private void txtProductionLine_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_KeyDown()";
			
			try
			{
				if ((e.KeyCode == Keys.F4) && (btnProductionLine.Enabled))
				{
					SelectProductionLine(METHOD_NAME, true);
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
		/// <summary>
		/// FillItemDataToGrid
		/// </summary>
		/// <param name="pdrowData"></param>
		/// <author>Trada</author>
		/// <date>Friday, April 21 2006</date>
		private void FillItemDataToGrid(DataRow pdrowData)
		{
			try
			{
				dgrdData.EditActive = true;
				dgrdData.Columns[ITM_ProductTable.PRODUCTID_FLD].Value = int.Parse(pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString());
				dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value = pdrowData[ITM_ProductTable.CODE_FLD];
				dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value	= pdrowData[ITM_ProductTable.DESCRIPTION_FLD];
				dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD].Value = pdrowData[ITM_ProductTable.REVISION_FLD];
				dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD].Value  = pdrowData[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
				dgrdData.Columns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Value  = pdrowData[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD];
				dgrdData.Columns[MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD].Value  = pdrowData[MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD];
				dgrdData.Columns[MST_WorkCenterTable.WORKCENTERID_FLD].Value  = int.Parse(pdrowData[MST_WorkCenterTable.WORKCENTERID_FLD].ToString());
				dgrdData.Columns[ITM_RoutingTable.ROUTINGID_FLD].Value  = int.Parse(pdrowData[ITM_RoutingTable.ROUTINGID_FLD].ToString());
				try
				{
					dgrdData.Columns[ITM_ProductTable.LTVARIABLETIME_FLD].Value  = Convert.ToDecimal(pdrowData[ITM_ProductTable.LTVARIABLETIME_FLD]);
				}
				catch{}
				//Change status for IsManuale column
				dgrdData.Columns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Value  = true;
				
			}
			catch (PCSDBException ex)
			{
				throw ex;
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
		/// <date>Friday, April 21 2006</date>
		private void dgrdData_ButtonClick(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			
			try
			{
				#region Old code
				//exit if grid is empty
				if(dgrdData.RowCount <= 0)
				{
					return;
				}

				//Click on WO No column
				//if(e.ColIndex == dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD]))
				if(dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD]))
				{
					if(dgrdData.Columns[PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD].Value.ToString().Trim().Length == 0)
					{
						return;
					}

					int intWOMasterID = int.Parse(dgrdData.Columns[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].Value.ToString());
					int intCCNID = int.Parse(dgrdData.Columns[MTR_CPOTable.CCNID_FLD].Value.ToString());
					int intMasLocID = 0;
					string strMasLoc = string.Empty;

					PRO_WorkOrderMasterVO voWOMaster = (PRO_WorkOrderMasterVO) boWorkOrder.GetObjectWOMasterVO(intWOMasterID);
					if (voWOMaster != null && voWOMaster.MasterLocationID > 0)
					{
						intMasLocID = voWOMaster.MasterLocationID;
						DataRowView drvResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.MASTERLOCATIONID_FLD, intMasLocID.ToString(), null, false);
						if (drvResult != null)
							strMasLoc = drvResult[MST_MasterLocationTable.CODE_FLD].ToString();
					}
				
					WorkOrder frmWorkOrder = new WorkOrder(intWOMasterID, intCCNID, intMasLocID, strMasLoc);
					frmWorkOrder.ShowDialog();					
				}
				
				//Click on PO No column
				//if(e.ColIndex == dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD]))
				if(dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD]))
				{
					if(dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].Value.ToString().Trim().Length == 0)
						return;
										
					int intPOMasterId =  int.Parse(dgrdData.Columns[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].Value.ToString());
					
					PurchaseOrder frmPurchaseOrder = new PurchaseOrder(intPOMasterId);
					frmPurchaseOrder.ShowDialog();
				}
				#endregion

				#region HACK: Trada 21-04-2006

				DataRowView drwResult = null;
				Hashtable htbCondition= new Hashtable();
				
				//Select Item
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD])
					|| dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD]))
				{
					//If this row is old row then return
					if (dgrdData.Row != dgrdData.RowCount)
					{
						if (dtbCPODetail.Rows.Count == 0) return;
						if (dgrdData.AddNewMode != AddNewModeEnum.AddNewPending)
						{
							if (dtbCPODetail.Rows[dgrdData.Row].RowState != DataRowState.Added) return;
						}
					}
					if (txtProductionLine.Text.Trim() != string.Empty)
					{
						htbCondition.Add(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD, int.Parse(txtProductionLine.Tag.ToString()));
					}
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]))
							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, ITM_ProductTable.CODE_FLD, dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].ToString(), htbCondition, true);
						else
							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, ITM_ProductTable.DESCRIPTION_FLD, dgrdData[dgrdData.Row,ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].ToString(), htbCondition, true);
					}
					else
					{
						if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME +ITM_ProductTable.CODE_FLD]))
							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, ITM_ProductTable.CODE_FLD, dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD].Value.ToString().Trim(), htbCondition, true);
						else
							drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD].Value.ToString().Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						FillItemDataToGrid(drwResult.Row);
					}
				}
				//Select Shift 
				if (dgrdData.Col == dgrdData.Columns.IndexOf(dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD]))
				{
//					//If this row is old row then return
//					if (dgrdData.Row != dtbCPODetail.Rows.Count)
//					{
//						if (dtbCPODetail.Rows.Count == 0) return;
//						if (dgrdData.AddNewMode != AddNewModeEnum.AddNewPending)
//						{
//							if (dtbCPODetail.Rows[dgrdData.Row].RowState != DataRowState.Added) return;
//						}
//					}
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, dgrdData[dgrdData.Row, PRO_ShiftTable.SHIFTDESC_FLD].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, dgrdData.Columns[PRO_ShiftTable.SHIFTDESC_FLD].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						dgrdData.EditActive = true;
						//Fill data to grid
						dgrdData[dgrdData.Row, PRO_ShiftTable.SHIFTDESC_FLD] = drwResult[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
						dgrdData.Columns[PRO_ShiftTable.SHIFTID_FLD].Value = int.Parse(drwResult[PRO_ShiftTable.SHIFTID_FLD].ToString());
					}
				}
				#endregion END: Trada 21-04-2006
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
		
		#endregion HACKED by Tuan TQ, Nov 04, 2005: Change request	
		
		/// <summary>
		/// dgrdData_BeforeColUpdate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, April 21 2006</date>
		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				//Code
				if (e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD)
				{
					//If this row is old row then return
					if ((dgrdData.Row != dgrdData.RowCount - 1) && (dgrdData.Row != dgrdData.RowCount))
					{
						if ((dtbCPODetail.Rows[dgrdData.Row].RowState != DataRowState.Added) 
							&& ((bool)dgrdData.Columns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Value != true))
							e.Cancel = true;
					}
					if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, ITM_ProductTable.CODE_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult.Row;	
						}
						else
						{
							e.Cancel = true;
						}
					}
				}
				//Description
				if (e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD)
				{
					//If this row is old row then return
					if ((dgrdData.Row != dgrdData.RowCount - 1) && (dgrdData.Row != dgrdData.RowCount))
					{
						if ((dtbCPODetail.Rows[dgrdData.Row].RowState != DataRowState.Added) 
							&& ((bool)dgrdData.Columns[PRO_DCPResultDetailTable.ISMANUAL_FLD].Value != true))
							e.Cancel = true;
					}
					if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(VIEW_PRODUCTINFOR, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult.Row;	
						}
						else
						{
							e.Cancel = true;
						}
					}
				}
				//Shift
				if (e.Column.DataColumn.DataField == PRO_ShiftTable.SHIFTDESC_FLD)
				{
					if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
						{
							e.Column.DataColumn.Tag = drwResult.Row;	
						}
						else
						{
							e.Cancel = true;
						}
					}
				}
				//SafetyStock Amount Column
				if (e.Column.DataColumn.DataField == PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT)
				{
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
						return;
					}
					if(decimal.Parse(e.Column.DataColumn.Text) < 0) 
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
		/// dgrdData_BeforeColEdit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, April 21 2006</date>
		private void dgrdData_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit()";
			try
			{
				//If this row is old row then return
				if ((e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD)
					||(e.Column.DataColumn.DataField == ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD))
				{
					if (cboPlanType.SelectedItem.ToString() == PlanTypeEnum.MRP.ToString())
					{
						e.Cancel = true;
						return;
					}
					//if (dgrdData.Row != dtbCPODetail.Rows.Count && dgrdData.Row != 0)
					//if (dgrdData.Row != dtbCPODetail.Rows.Count)
					try 
					{
						if ((dtbCPODetail.Rows[dgrdData.Row].RowState != DataRowState.Added))
							e.Cancel = true;
					}
					catch 
					{}
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
		/// <summary>
		/// dgrdData_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, April 24 2006</date>
		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				if ((e.KeyCode == Keys.Delete)&&(dgrdData.SelectedRows.Count > 0))
				{
					if (btnSave.Enabled)
					{
						dgrdData.AllowDelete = true;
						
						#region save ID for delete multirows purpose

						//Save selected rows
						int intSelectRows = dgrdData.SelectedRows.Count;
						ArrayList intIndexOfSelectedRows = new ArrayList();
						for (int i = 0; i < intSelectRows; i++)
							intIndexOfSelectedRows.Add(int.Parse(dgrdData.SelectedRows[i].ToString()));
						intIndexOfSelectedRows.Sort();
						//Save MasterID to Delete
						for (int i = intSelectRows - 1; i >= 0;  i--)
						{
							dgrdData.Row = (int) intIndexOfSelectedRows[i];
							if (cboPlanType.SelectedItem.ToString() == lblDCP.Text.Trim()) //DCP mode
							{
								#region DCP Mode

								if ((dgrdData.Columns[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].Value	 != null)
									&& (dgrdData.Columns[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].Value != DBNull.Value))
								{
									//check if arrMasterIDToUpdate has this DCPResultMasterID
									Int64 intDCPResultMasterID = Convert.ToInt64(dgrdData.Columns[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].Value);
									if (arrMasterIDToUpdate.Count > 0)
									{
										int intCounter = 0;
										for (int j = 0; j < arrMasterIDToUpdate.Count; j++)
											if ((Int64)arrMasterIDToUpdate[j] != intDCPResultMasterID)
												intCounter++;
										if (intCounter == arrMasterIDToUpdate.Count)
										{
											//add new value to array
											strMasterIDToUpdate += "," + dgrdData.Columns[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].Value.ToString();
											arrMasterIDToUpdate.Add(Convert.ToInt64(dgrdData.Columns[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].Value));
										}
									}
									else
									{
										strMasterIDToUpdate += "," + dgrdData.Columns[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].Value.ToString();
										arrMasterIDToUpdate.Add(Convert.ToInt64(dgrdData.Columns[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD].Value));
									}
								}

								#endregion
							}
							else // MRP Mode
							{
								#region MRP Mode
								strCPOIDToDelete += "," + dgrdData.Columns[MTR_CPOTable.CPOID_FLD].Value.ToString();
								#endregion
							}
						}
						
						#endregion

						//delete detail row
						FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
						
						txtNumRows.Value = dgrdData.RowCount;
					}

				}
				if (e.KeyCode == Keys.F4)
				{
					if (btnSave.Enabled)
					{
						dgrdData_ButtonClick(sender, null);
					}
				}
				if (e.KeyCode == Keys.F12)
				{
					dgrdData.Row = dgrdData.RowCount;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
					dgrdData.Focus();
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

		private void dgrdData_OnAddNew(object sender, System.EventArgs e)
		{
		
		}
		
		private decimal GetQuantityForCPO(DataRowView drowItem, decimal pdecQuantity)
		{
			#region remove minorder and multiple order
			if(pdecQuantity < 0) return 0;

			if (drowItem[ITM_ProductTable.ORDERQUANTITY_FLD].ToString().Trim() != string.Empty && pdecQuantity < decimal.Parse(drowItem[ITM_ProductTable.ORDERQUANTITY_FLD].ToString()))
			{
				pdecQuantity = decimal.Parse(drowItem[ITM_ProductTable.ORDERQUANTITY_FLD].ToString());
			}
			// drowItem[ITM_ProductTable.ORDERQUANTITY_FLD].ToString().Trim() != string.Empty && 
			if (drowItem[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString() != string.Empty 
				&& decimal.Parse(drowItem[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString()) != 0 
				&& (pdecQuantity % decimal.Parse(drowItem[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString().Trim()) != 0)
				)
			{
				pdecQuantity =  (decimal.Floor(pdecQuantity/decimal.Parse(drowItem[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString().Trim())) + 1)
					*decimal.Parse(drowItem[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString().Trim());
			}

			#endregion

			return pdecQuantity;
		}
		
		/// <summary>
		/// Gets the first valid work day of cycle
		/// </summary>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <param name="pdstCalendar">Workday Calendar</param>
		/// <returns>First Valid work day</returns>
		private DateTime GetFirstValidWorkDay(DateTime pdtmAsOfDate, DataSet pdstCalendar)
		{
			while (IsOffDay(pdtmAsOfDate, pdstCalendar))
				pdtmAsOfDate = pdtmAsOfDate.AddDays(1);
			return pdtmAsOfDate;
		}
	}
}				