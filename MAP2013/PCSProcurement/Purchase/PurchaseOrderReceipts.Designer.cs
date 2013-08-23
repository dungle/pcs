namespace PCSProcurement.Purchase
{
    partial class PurchaseOrderReceipts
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseOrderReceipts));
            this.btnDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPurpose = new System.Windows.Forms.ComboBox();
            this.btnBOMShortage = new System.Windows.Forms.Button();
            this.txtProductionLine = new System.Windows.Forms.TextBox();
            this.txtOutside = new System.Windows.Forms.TextBox();
            this.txtVendorNo = new System.Windows.Forms.TextBox();
            this.txtVendorName = new System.Windows.Forms.TextBox();
            this.txtDeliverySlip = new System.Windows.Forms.TextBox();
            this.txtInvoice = new System.Windows.Forms.TextBox();
            this.lblProductionLine = new System.Windows.Forms.Label();
            this.btnOutside = new System.Windows.Forms.Button();
            this.btnDeliverySlip = new System.Windows.Forms.Button();
            this.btnInvoice = new System.Windows.Forms.Button();
            this.btnProductionLine = new System.Windows.Forms.Button();
            this.radOutside = new System.Windows.Forms.RadioButton();
            this.lblVenderNo = new System.Windows.Forms.Label();
            this.radBySlip = new System.Windows.Forms.RadioButton();
            this.CCNCombo = new C1.Win.C1List.C1Combo();
            this.radByInvoice = new System.Windows.Forms.RadioButton();
            this.txtMasLoc = new System.Windows.Forms.TextBox();
            this.ReceiveNoText = new System.Windows.Forms.TextBox();
            this.PostDatePicker = new C1.Win.C1Input.C1DateEdit();
            this.btnSearchMasLoc = new System.Windows.Forms.Button();
            this.SearchReceiveButton = new System.Windows.Forms.Button();
            this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.btnClose = new System.Windows.Forms.Button();
            this.ReceiveNoLabel = new System.Windows.Forms.Label();
            this.MasLocLabel = new System.Windows.Forms.Label();
            this.PostDateLabel = new System.Windows.Forms.Label();
            this.CCNLabel = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDeleteRow = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CCNCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostDatePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cboPurpose
            // 
            this.cboPurpose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboPurpose, "cboPurpose");
            this.cboPurpose.Items.AddRange(new object[] {
            resources.GetString("cboPurpose.Items"),
            resources.GetString("cboPurpose.Items1")});
            this.cboPurpose.Name = "cboPurpose";
            // 
            // btnBOMShortage
            // 
            resources.ApplyResources(this.btnBOMShortage, "btnBOMShortage");
            this.btnBOMShortage.Name = "btnBOMShortage";
            this.btnBOMShortage.Click += new System.EventHandler(this.btnBOMShortage_Click);
            // 
            // txtProductionLine
            // 
            resources.ApplyResources(this.txtProductionLine, "txtProductionLine");
            this.txtProductionLine.Name = "txtProductionLine";
            this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
            this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
            // 
            // txtOutside
            // 
            resources.ApplyResources(this.txtOutside, "txtOutside");
            this.txtOutside.Name = "txtOutside";
            // 
            // txtVendorNo
            // 
            resources.ApplyResources(this.txtVendorNo, "txtVendorNo");
            this.txtVendorNo.Name = "txtVendorNo";
            this.txtVendorNo.ReadOnly = true;
            // 
            // txtVendorName
            // 
            resources.ApplyResources(this.txtVendorName, "txtVendorName");
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.ReadOnly = true;
            // 
            // txtDeliverySlip
            // 
            resources.ApplyResources(this.txtDeliverySlip, "txtDeliverySlip");
            this.txtDeliverySlip.Name = "txtDeliverySlip";
            // 
            // txtInvoice
            // 
            resources.ApplyResources(this.txtInvoice, "txtInvoice");
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvoice_KeyDown);
            this.txtInvoice.Validating += new System.ComponentModel.CancelEventHandler(this.txtInvoice_Validating);
            // 
            // lblProductionLine
            // 
            this.lblProductionLine.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblProductionLine, "lblProductionLine");
            this.lblProductionLine.Name = "lblProductionLine";
            // 
            // btnOutside
            // 
            resources.ApplyResources(this.btnOutside, "btnOutside");
            this.btnOutside.Name = "btnOutside";
            this.btnOutside.Click += new System.EventHandler(this.btnOutside_Click);
            // 
            // btnDeliverySlip
            // 
            resources.ApplyResources(this.btnDeliverySlip, "btnDeliverySlip");
            this.btnDeliverySlip.Name = "btnDeliverySlip";
            this.btnDeliverySlip.Click += new System.EventHandler(this.btnDeliverySlip_Click);
            // 
            // btnInvoice
            // 
            resources.ApplyResources(this.btnInvoice, "btnInvoice");
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Click += new System.EventHandler(this.btnInvoice_Click);
            // 
            // btnProductionLine
            // 
            resources.ApplyResources(this.btnProductionLine, "btnProductionLine");
            this.btnProductionLine.Name = "btnProductionLine";
            this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
            // 
            // radOutside
            // 
            resources.ApplyResources(this.radOutside, "radOutside");
            this.radOutside.Name = "radOutside";
            this.radOutside.CheckedChanged += new System.EventHandler(this.radOutside_CheckedChanged);
            // 
            // lblVenderNo
            // 
            resources.ApplyResources(this.lblVenderNo, "lblVenderNo");
            this.lblVenderNo.Name = "lblVenderNo";
            // 
            // radBySlip
            // 
            resources.ApplyResources(this.radBySlip, "radBySlip");
            this.radBySlip.Name = "radBySlip";
            this.radBySlip.CheckedChanged += new System.EventHandler(this.radBySlip_CheckedChanged);
            // 
            // CCNCombo
            // 
            this.CCNCombo.AddItemSeparator = ';';
            resources.ApplyResources(this.CCNCombo, "CCNCombo");
            this.CCNCombo.CaptionHeight = 17;
            this.CCNCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.CCNCombo.ColumnCaptionHeight = 17;
            this.CCNCombo.ColumnFooterHeight = 17;
            this.CCNCombo.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.CCNCombo.ContentHeight = 15;
            this.CCNCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.CCNCombo.EditorBackColor = System.Drawing.SystemColors.Window;
            this.CCNCombo.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CCNCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.CCNCombo.EditorHeight = 15;
            this.CCNCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("CCNCombo.Images"))));
            this.CCNCombo.ItemHeight = 15;
            this.CCNCombo.MatchEntryTimeout = ((long)(2000));
            this.CCNCombo.MaxDropDownItems = ((short)(5));
            this.CCNCombo.MaxLength = 32767;
            this.CCNCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.CCNCombo.Name = "CCNCombo";
            this.CCNCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.CCNCombo.PropBag = resources.GetString("CCNCombo.PropBag");
            // 
            // radByInvoice
            // 
            resources.ApplyResources(this.radByInvoice, "radByInvoice");
            this.radByInvoice.Name = "radByInvoice";
            this.radByInvoice.CheckedChanged += new System.EventHandler(this.radByInvoice_CheckedChanged);
            // 
            // txtMasLoc
            // 
            resources.ApplyResources(this.txtMasLoc, "txtMasLoc");
            this.txtMasLoc.Name = "txtMasLoc";
            this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
            this.txtMasLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasLoc_Validating);
            // 
            // ReceiveNoText
            // 
            resources.ApplyResources(this.ReceiveNoText, "ReceiveNoText");
            this.ReceiveNoText.Name = "ReceiveNoText";
            this.ReceiveNoText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReceiveNoText_KeyDown);
            this.ReceiveNoText.Validating += new System.ComponentModel.CancelEventHandler(this.ReceiveNoText_Validating);
            // 
            // PostDatePicker
            // 
            // 
            // 
            // 
            this.PostDatePicker.Calendar.DayNameLength = 1;
            this.PostDatePicker.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("PostDatePicker.Calendar.ImeMode")));
            resources.ApplyResources(this.PostDatePicker, "PostDatePicker");
            this.PostDatePicker.DisableOnNoData = false;
            this.PostDatePicker.Name = "PostDatePicker";
            this.PostDatePicker.Tag = null;
            // 
            // btnSearchMasLoc
            // 
            resources.ApplyResources(this.btnSearchMasLoc, "btnSearchMasLoc");
            this.btnSearchMasLoc.Name = "btnSearchMasLoc";
            this.btnSearchMasLoc.Click += new System.EventHandler(this.btnSearchMasLoc_Click);
            // 
            // SearchReceiveButton
            // 
            resources.ApplyResources(this.SearchReceiveButton, "SearchReceiveButton");
            this.SearchReceiveButton.Name = "SearchReceiveButton";
            this.SearchReceiveButton.Click += new System.EventHandler(this.SearchReceiveButton_Click);
            // 
            // dgrdData
            // 
            resources.ApplyResources(this.dgrdData, "dgrdData");
            this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("dgrdData.Images"))));
            this.dgrdData.Name = "dgrdData";
            this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.dgrdData.PreviewInfo.ZoomFactor = 75;
            this.dgrdData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("dgrdData.PrintInfo.PageSettings")));
            this.dgrdData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
            this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
            this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
            this.dgrdData.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.dgrdData_BeforeDelete);
            this.dgrdData.OnAddNew += new System.EventHandler(this.dgrdData_OnAddNew);
            this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
            this.dgrdData.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
            this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
            this.dgrdData.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.dgrdData_RowColChange);
            this.dgrdData.PropBag = resources.GetString("dgrdData.PropBag");
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ReceiveNoLabel
            // 
            this.ReceiveNoLabel.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.ReceiveNoLabel, "ReceiveNoLabel");
            this.ReceiveNoLabel.Name = "ReceiveNoLabel";
            // 
            // MasLocLabel
            // 
            this.MasLocLabel.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.MasLocLabel, "MasLocLabel");
            this.MasLocLabel.Name = "MasLocLabel";
            // 
            // PostDateLabel
            // 
            this.PostDateLabel.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.PostDateLabel, "PostDateLabel");
            this.PostDateLabel.Name = "PostDateLabel";
            // 
            // CCNLabel
            // 
            resources.ApplyResources(this.CCNLabel, "CCNLabel");
            this.CCNLabel.ForeColor = System.Drawing.Color.Maroon;
            this.CCNLabel.Name = "CCNLabel";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDeleteRow
            // 
            resources.ApplyResources(this.btnDeleteRow, "btnDeleteRow");
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Click += new System.EventHandler(this.btnDeleteRow_Click);
            // 
            // PurchaseOrderReceipts
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboPurpose);
            this.Controls.Add(this.btnBOMShortage);
            this.Controls.Add(this.txtProductionLine);
            this.Controls.Add(this.txtOutside);
            this.Controls.Add(this.txtVendorNo);
            this.Controls.Add(this.txtVendorName);
            this.Controls.Add(this.txtDeliverySlip);
            this.Controls.Add(this.txtInvoice);
            this.Controls.Add(this.lblProductionLine);
            this.Controls.Add(this.btnOutside);
            this.Controls.Add(this.btnDeliverySlip);
            this.Controls.Add(this.btnInvoice);
            this.Controls.Add(this.btnProductionLine);
            this.Controls.Add(this.radOutside);
            this.Controls.Add(this.lblVenderNo);
            this.Controls.Add(this.radBySlip);
            this.Controls.Add(this.CCNCombo);
            this.Controls.Add(this.radByInvoice);
            this.Controls.Add(this.txtMasLoc);
            this.Controls.Add(this.ReceiveNoText);
            this.Controls.Add(this.PostDatePicker);
            this.Controls.Add(this.btnSearchMasLoc);
            this.Controls.Add(this.SearchReceiveButton);
            this.Controls.Add(this.dgrdData);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ReceiveNoLabel);
            this.Controls.Add(this.MasLocLabel);
            this.Controls.Add(this.PostDateLabel);
            this.Controls.Add(this.CCNLabel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnDeleteRow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PurchaseOrderReceipts";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PurchaseOrderReceipts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CCNCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostDatePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPurpose;
        private System.Windows.Forms.Button btnBOMShortage;
        private System.Windows.Forms.TextBox txtProductionLine;
        private System.Windows.Forms.TextBox txtOutside;
        private System.Windows.Forms.TextBox txtVendorNo;
        private System.Windows.Forms.TextBox txtVendorName;
        private System.Windows.Forms.TextBox txtDeliverySlip;
        private System.Windows.Forms.TextBox txtInvoice;
        private System.Windows.Forms.Label lblProductionLine;
        private System.Windows.Forms.Button btnOutside;
        private System.Windows.Forms.Button btnDeliverySlip;
        private System.Windows.Forms.Button btnInvoice;
        private System.Windows.Forms.Button btnProductionLine;
        private System.Windows.Forms.RadioButton radOutside;
        private System.Windows.Forms.Label lblVenderNo;
        private System.Windows.Forms.RadioButton radBySlip;
        private C1.Win.C1List.C1Combo CCNCombo;
        private System.Windows.Forms.RadioButton radByInvoice;
        private System.Windows.Forms.TextBox txtMasLoc;
        private System.Windows.Forms.TextBox ReceiveNoText;
        private C1.Win.C1Input.C1DateEdit PostDatePicker;
        private System.Windows.Forms.Button btnSearchMasLoc;
        private System.Windows.Forms.Button SearchReceiveButton;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label ReceiveNoLabel;
        private System.Windows.Forms.Label MasLocLabel;
        private System.Windows.Forms.Label PostDateLabel;
        private System.Windows.Forms.Label CCNLabel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnDeleteRow;
    }
}