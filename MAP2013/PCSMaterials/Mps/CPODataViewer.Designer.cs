using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1.Win.C1Input;

namespace PCSMaterials.Mps
{
    partial class CPODataViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPODataViewer));
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
            this.btnUpdateWorkOrder = new System.Windows.Forms.Button();
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
            resources.ApplyResources(this.GroupBox, "GroupBox");
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
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.TabStop = false;
            this.GroupBox.Enter += new System.EventHandler(this.GroupBox_Enter);
            // 
            // txtVendor
            // 
            resources.ApplyResources(this.txtVendor, "txtVendor");
            this.txtVendor.Name = "txtVendor";
            this.txtVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendor_KeyDown);
            this.txtVendor.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendor_Validating);
            // 
            // btnVendorSearch
            // 
            resources.ApplyResources(this.btnVendorSearch, "btnVendorSearch");
            this.btnVendorSearch.Name = "btnVendorSearch";
            this.btnVendorSearch.Click += new System.EventHandler(this.btnVendorSearch_Click);
            // 
            // lblVendor
            // 
            resources.ApplyResources(this.lblVendor, "lblVendor");
            this.lblVendor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVendor.Name = "lblVendor";
            // 
            // txtPartName
            // 
            resources.ApplyResources(this.txtPartName, "txtPartName");
            this.txtPartName.Name = "txtPartName";
            this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
            this.txtPartName.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartName_Validating);
            // 
            // txtPartNumber
            // 
            resources.ApplyResources(this.txtPartNumber, "txtPartNumber");
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNumber_KeyDown);
            this.txtPartNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartNumber_Validating);
            // 
            // txtCategory
            // 
            resources.ApplyResources(this.txtCategory, "txtCategory");
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategory_KeyDown);
            this.txtCategory.Validating += new System.ComponentModel.CancelEventHandler(this.txtCategory_Validating);
            // 
            // txtRevision
            // 
            resources.ApplyResources(this.txtRevision, "txtRevision");
            this.txtRevision.Name = "txtRevision";
            // 
            // lblDCP
            // 
            resources.ApplyResources(this.lblDCP, "lblDCP");
            this.lblDCP.Name = "lblDCP";
            // 
            // txtProductionLine
            // 
            resources.ApplyResources(this.txtProductionLine, "txtProductionLine");
            this.txtProductionLine.Name = "txtProductionLine";
            this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
            this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
            // 
            // lblProductionLine
            // 
            this.lblProductionLine.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblProductionLine, "lblProductionLine");
            this.lblProductionLine.Name = "lblProductionLine";
            // 
            // btnProductionLine
            // 
            resources.ApplyResources(this.btnProductionLine, "btnProductionLine");
            this.btnProductionLine.Name = "btnProductionLine";
            this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
            // 
            // cboCCN
            // 
            this.cboCCN.AddItemSeparator = ';';
            resources.ApplyResources(this.cboCCN, "cboCCN");
            this.cboCCN.CaptionHeight = 17;
            this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCCN.ColumnCaptionHeight = 17;
            this.cboCCN.ColumnFooterHeight = 17;
            this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCCN.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCCN.Images"))));
            this.cboCCN.ItemHeight = 15;
            this.cboCCN.MatchEntryTimeout = ((long)(2000));
            this.cboCCN.MaxDropDownItems = ((short)(5));
            this.cboCCN.MaxLength = 32767;
            this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCCN.Name = "cboCCN";
            this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboCCN.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboCCN.PropBag = resources.GetString("cboCCN.PropBag");
            // 
            // btnCategorySearch
            // 
            resources.ApplyResources(this.btnCategorySearch, "btnCategorySearch");
            this.btnCategorySearch.Name = "btnCategorySearch";
            this.btnCategorySearch.Click += new System.EventHandler(this.btnCategorySearch_Click);
            // 
            // btnMasLocSearch
            // 
            resources.ApplyResources(this.btnMasLocSearch, "btnMasLocSearch");
            this.btnMasLocSearch.Name = "btnMasLocSearch";
            this.btnMasLocSearch.Click += new System.EventHandler(this.btnMasLocSearch_Click);
            // 
            // txtMasLoc
            // 
            resources.ApplyResources(this.txtMasLoc, "txtMasLoc");
            this.txtMasLoc.Name = "txtMasLoc";
            this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
            this.txtMasLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasLoc_Validating);
            // 
            // btnCycleSearch
            // 
            resources.ApplyResources(this.btnCycleSearch, "btnCycleSearch");
            this.btnCycleSearch.Name = "btnCycleSearch";
            this.btnCycleSearch.Click += new System.EventHandler(this.btnCycleSearch_Click);
            // 
            // txtCycle
            // 
            resources.ApplyResources(this.txtCycle, "txtCycle");
            this.txtCycle.Name = "txtCycle";
            this.txtCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCycle_KeyDown);
            this.txtCycle.Validating += new System.ComponentModel.CancelEventHandler(this.txtCycle_Validating);
            // 
            // cboViewType
            // 
            resources.ApplyResources(this.cboViewType, "cboViewType");
            this.cboViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboViewType.Items.AddRange(new object[] {
            resources.GetString("cboViewType.Items"),
            resources.GetString("cboViewType.Items1")});
            this.cboViewType.Name = "cboViewType";
            this.cboViewType.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboViewType.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblViewType
            // 
            resources.ApplyResources(this.lblViewType, "lblViewType");
            this.lblViewType.ForeColor = System.Drawing.Color.Maroon;
            this.lblViewType.Name = "lblViewType";
            // 
            // dtmToDueDate
            // 
            resources.ApplyResources(this.dtmToDueDate, "dtmToDueDate");
            // 
            // 
            // 
            this.dtmToDueDate.Calendar.AccessibleDescription = resources.GetString("dtmToDueDate.Calendar.AccessibleDescription");
            this.dtmToDueDate.Calendar.AccessibleName = resources.GetString("dtmToDueDate.Calendar.AccessibleName");
            this.dtmToDueDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmToDueDate.Calendar.ImeMode")));
            this.dtmToDueDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmToDueDate.Calendar.RightToLeft")));
            this.dtmToDueDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDueDate.DisplayFormat.Inherit")));
            this.dtmToDueDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmToDueDate.EditFormat.Inherit")));
            this.dtmToDueDate.Name = "dtmToDueDate";
            this.dtmToDueDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmToDueDate.ParseInfo.Inherit")));
            this.dtmToDueDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmToDueDate.PreValidation.Inherit")));
            this.dtmToDueDate.Enter += new System.EventHandler(this.OnEnterControl);
            this.dtmToDueDate.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // dtmFromDueDate
            // 
            resources.ApplyResources(this.dtmFromDueDate, "dtmFromDueDate");
            // 
            // 
            // 
            this.dtmFromDueDate.Calendar.AccessibleDescription = resources.GetString("dtmFromDueDate.Calendar.AccessibleDescription");
            this.dtmFromDueDate.Calendar.AccessibleName = resources.GetString("dtmFromDueDate.Calendar.AccessibleName");
            this.dtmFromDueDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmFromDueDate.Calendar.ImeMode")));
            this.dtmFromDueDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmFromDueDate.Calendar.RightToLeft")));
            this.dtmFromDueDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDueDate.DisplayFormat.Inherit")));
            this.dtmFromDueDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmFromDueDate.EditFormat.Inherit")));
            this.dtmFromDueDate.Name = "dtmFromDueDate";
            this.dtmFromDueDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmFromDueDate.ParseInfo.Inherit")));
            this.dtmFromDueDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmFromDueDate.PreValidation.Inherit")));
            this.dtmFromDueDate.Enter += new System.EventHandler(this.OnEnterControl);
            this.dtmFromDueDate.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblMasLoc
            // 
            resources.ApplyResources(this.lblMasLoc, "lblMasLoc");
            this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblMasLoc.Name = "lblMasLoc";
            // 
            // cboPlanType
            // 
            resources.ApplyResources(this.cboPlanType, "cboPlanType");
            this.cboPlanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlanType.Items.AddRange(new object[] {
            resources.GetString("cboPlanType.Items"),
            resources.GetString("cboPlanType.Items1"),
            resources.GetString("cboPlanType.Items2")});
            this.cboPlanType.Name = "cboPlanType";
            this.cboPlanType.SelectedIndexChanged += new System.EventHandler(this.cboPlanType_SelectedIndexChanged);
            // 
            // lblPlanType
            // 
            resources.ApplyResources(this.lblPlanType, "lblPlanType");
            this.lblPlanType.ForeColor = System.Drawing.Color.Maroon;
            this.lblPlanType.Name = "lblPlanType";
            // 
            // lblCycle
            // 
            resources.ApplyResources(this.lblCycle, "lblCycle");
            this.lblCycle.ForeColor = System.Drawing.Color.Maroon;
            this.lblCycle.Name = "lblCycle";
            // 
            // lblCCN
            // 
            resources.ApplyResources(this.lblCCN, "lblCCN");
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.Name = "lblCCN";
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPartNumberSearch
            // 
            resources.ApplyResources(this.btnPartNumberSearch, "btnPartNumberSearch");
            this.btnPartNumberSearch.Name = "btnPartNumberSearch";
            this.btnPartNumberSearch.Click += new System.EventHandler(this.btnPartNumberSearch_Click);
            // 
            // btnPartNameSearch
            // 
            resources.ApplyResources(this.btnPartNameSearch, "btnPartNameSearch");
            this.btnPartNameSearch.Name = "btnPartNameSearch";
            this.btnPartNameSearch.Click += new System.EventHandler(this.btnPartNameSearch_Click);
            // 
            // lblRevision
            // 
            this.lblRevision.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblRevision, "lblRevision");
            this.lblRevision.Name = "lblRevision";
            // 
            // lblToDueDate
            // 
            resources.ApplyResources(this.lblToDueDate, "lblToDueDate");
            this.lblToDueDate.Name = "lblToDueDate";
            // 
            // lblPartNumer
            // 
            this.lblPartNumer.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblPartNumer, "lblPartNumer");
            this.lblPartNumer.Name = "lblPartNumer";
            // 
            // lblCategory
            // 
            resources.ApplyResources(this.lblCategory, "lblCategory");
            this.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCategory.Name = "lblCategory";
            // 
            // lblPartName
            // 
            this.lblPartName.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblPartName, "lblPartName");
            this.lblPartName.Name = "lblPartName";
            // 
            // lblFromDueDate
            // 
            resources.ApplyResources(this.lblFromDueDate, "lblFromDueDate");
            this.lblFromDueDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFromDueDate.Name = "lblFromDueDate";
            // 
            // dgrdData
            // 
            resources.ApplyResources(this.dgrdData, "dgrdData");
            this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("dgrdData.Images"))));
            this.dgrdData.Name = "dgrdData";
            this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.dgrdData.PreviewInfo.ZoomFactor = 75D;
            this.dgrdData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("dgrdData.PrintInfo.PageSettings")));
            this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
            this.dgrdData.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
            this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
            this.dgrdData.BeforeRowColChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdData_BeforeRowColChange);
            this.dgrdData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
            this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
            this.dgrdData.OnAddNew += new System.EventHandler(this.dgrdData_OnAddNew);
            this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
            this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnNewPOConvert
            // 
            resources.ApplyResources(this.btnNewPOConvert, "btnNewPOConvert");
            this.btnNewPOConvert.Name = "btnNewPOConvert";
            this.btnNewPOConvert.Click += new System.EventHandler(this.btnNewPOConvert_Click);
            // 
            // chkSelectAll
            // 
            resources.ApplyResources(this.chkSelectAll, "chkSelectAll");
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // btnNewWOConvert
            // 
            resources.ApplyResources(this.btnNewWOConvert, "btnNewWOConvert");
            this.btnNewWOConvert.Name = "btnNewWOConvert";
            this.btnNewWOConvert.Click += new System.EventHandler(this.btnNewWOConvert_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExistingPOConvert
            // 
            resources.ApplyResources(this.btnExistingPOConvert, "btnExistingPOConvert");
            this.btnExistingPOConvert.Name = "btnExistingPOConvert";
            this.btnExistingPOConvert.Click += new System.EventHandler(this.btnExistingPOConvert_Click);
            // 
            // btnUpdateWorkOrder
            // 
            resources.ApplyResources(this.btnUpdateWorkOrder, "btnUpdateWorkOrder");
            this.btnUpdateWorkOrder.Name = "btnUpdateWorkOrder";
            this.btnUpdateWorkOrder.Click += new System.EventHandler(this.btnUpdateWorkOrder_Click);
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            // 
            // 
            // 
            this.dtmDate.Calendar.AccessibleDescription = resources.GetString("dtmDate.Calendar.AccessibleDescription");
            this.dtmDate.Calendar.AccessibleName = resources.GetString("dtmDate.Calendar.AccessibleName");
            this.dtmDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmDate.Calendar.ImeMode")));
            this.dtmDate.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dtmDate.Calendar.RightToLeft")));
            this.dtmDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmDate.DisplayFormat.Inherit")));
            this.dtmDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("dtmDate.EditFormat.Inherit")));
            this.dtmDate.Name = "dtmDate";
            this.dtmDate.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("dtmDate.ParseInfo.Inherit")));
            this.dtmDate.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("dtmDate.PreValidation.Inherit")));
            this.dtmDate.DropDownOpened += new System.EventHandler(this.dtmDate_DropDownOpened);
            this.dtmDate.DropDownClosed += new C1.Win.C1Input.DropDownClosedEventHandler(this.dtmDate_DropDownClosed);
            this.dtmDate.VisibleChanged += new System.EventHandler(this.dtmDate_VisibleChanged);
            // 
            // numQuantity
            // 
            resources.ApplyResources(this.numQuantity, "numQuantity");
            this.numQuantity.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("numQuantity.DisplayFormat.Inherit")));
            this.numQuantity.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("numQuantity.EditFormat.Inherit")));
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
            | C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.numQuantity.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("numQuantity.ParseInfo.Inherit")));
            this.numQuantity.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("numQuantity.PreValidation.Inherit")));
            this.numQuantity.VisibleChanged += new System.EventHandler(this.numQuantity_VisibleChanged);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblNumberOfRows
            // 
            resources.ApplyResources(this.lblNumberOfRows, "lblNumberOfRows");
            this.lblNumberOfRows.Name = "lblNumberOfRows";
            // 
            // txtNumRows
            // 
            resources.ApplyResources(this.txtNumRows, "txtNumRows");
            this.txtNumRows.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtNumRows.DisplayFormat.Inherit")));
            this.txtNumRows.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(resources.GetObject("txtNumRows.EditFormat.Inherit")));
            this.txtNumRows.Name = "txtNumRows";
            this.txtNumRows.ParseInfo.Inherit = ((C1.Win.C1Input.ParseInfoInheritFlags)(resources.GetObject("txtNumRows.ParseInfo.Inherit")));
            this.txtNumRows.PreValidation.Inherit = ((C1.Win.C1Input.PreValidationInheritFlags)(resources.GetObject("txtNumRows.PreValidation.Inherit")));
            // 
            // CPODataViewer
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.txtNumRows);
            this.Controls.Add(this.lblNumberOfRows);
            this.Controls.Add(this.btnUpdateWorkOrder);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "CPODataViewer";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CPODataViewer_Closing);
            this.Load += new System.EventHandler(this.CPODataViewer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CPODataViewer_KeyDown);
            this.GroupBox.ResumeLayout(false);
            this.GroupBox.PerformLayout();
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
        private System.Windows.Forms.Button btnUpdateWorkOrder;
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
        private System.Windows.Forms.Label lblNumberOfRows;
        private C1.Win.C1Input.C1NumericEdit txtNumRows;
    }
}
