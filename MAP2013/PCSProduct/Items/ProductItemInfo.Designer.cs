using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.C1Command;
using C1.Win.C1Input;
using C1.Win.C1List;

namespace PCSProduct.Items
{
    partial class ProductItemInfo
    {
        private IContainer components;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductItemInfo));
            this.txtPartNumber = new C1.Win.C1Input.C1TextBox();
            this.txtPrimaryVendorID = new System.Windows.Forms.TextBox();
            this.txtLotSize = new C1.Win.C1Input.C1NumericEdit();
            this.cboFormatCodeID = new C1.Win.C1List.C1Combo();
            this.lblFormatCodeID = new System.Windows.Forms.Label();
            this.lblLotSize = new System.Windows.Forms.Label();
            this.cboBinID = new C1.Win.C1List.C1Combo();
            this.cboLocationID = new C1.Win.C1List.C1Combo();
            this.cboMasterLocationID = new C1.Win.C1List.C1Combo();
            this.cboSellingUMID = new C1.Win.C1List.C1Combo();
            this.cboBuyingUMID = new C1.Win.C1List.C1Combo();
            this.cboStockUMID = new C1.Win.C1List.C1Combo();
            this.cboReasonID = new C1.Win.C1List.C1Combo();
            this.cboHazardID = new C1.Win.C1List.C1Combo();
            this.cboFreightClassID = new C1.Win.C1List.C1Combo();
            this.lblBinID = new System.Windows.Forms.Label();
            this.lblLocationID = new System.Windows.Forms.Label();
            this.lblMasterLocationID = new System.Windows.Forms.Label();
            this.lblQAStatus = new System.Windows.Forms.Label();
            this.lblShelfLife = new System.Windows.Forms.Label();
            this.lblAccountCode = new System.Windows.Forms.Label();
            this.lblSellingUMID = new System.Windows.Forms.Label();
            this.dtmSetupDate = new C1.Win.C1Input.C1DateEdit();
            this.cboSourceID = new C1.Win.C1List.C1Combo();
            this.cboCategoryID = new C1.Win.C1List.C1Combo();
            this.lblSourceID = new System.Windows.Forms.Label();
            this.lblStockUMID = new System.Windows.Forms.Label();
            this.lblCategoryID = new System.Windows.Forms.Label();
            this.chkMakeItem = new System.Windows.Forms.CheckBox();
            this.lblSetupDate = new System.Windows.Forms.Label();
            this.lblReasonID = new System.Windows.Forms.Label();
            this.txtOtherInfo2 = new System.Windows.Forms.TextBox();
            this.lblOtherInfo2 = new System.Windows.Forms.Label();
            this.txtOtherInfo1 = new System.Windows.Forms.TextBox();
            this.lblOtherInfo1 = new System.Windows.Forms.Label();
            this.lblBuyingUMID = new System.Windows.Forms.Label();
            this.grpWeightAndSize = new System.Windows.Forms.GroupBox();
            this.txtLength = new C1.Win.C1Input.C1NumericEdit();
            this.cboWidthUMID = new C1.Win.C1List.C1Combo();
            this.cboHeightUMID = new C1.Win.C1List.C1Combo();
            this.cboLengthUMID = new C1.Win.C1List.C1Combo();
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblHeightUMID = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidthUMID = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblLengthUMID = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.cboWeightUMID = new C1.Win.C1List.C1Combo();
            this.lblWeightUMID = new System.Windows.Forms.Label();
            this.txtHeight = new C1.Win.C1Input.C1NumericEdit();
            this.txtWidth = new C1.Win.C1Input.C1NumericEdit();
            this.txtWeight = new C1.Win.C1Input.C1NumericEdit();
            this.lblHazardID = new System.Windows.Forms.Label();
            this.lblFreightClassID = new System.Windows.Forms.Label();
            this.lblPartNumber = new System.Windows.Forms.Label();
            this.chkStock = new System.Windows.Forms.CheckBox();
            this.chkLotControl = new System.Windows.Forms.CheckBox();
            this.txtShelfLife = new C1.Win.C1Input.C1NumericEdit();
            this.cboAccountCode = new C1.Win.C1List.C1Combo();
            this.cboQAStatus = new C1.Win.C1List.C1Combo();
            this.cboDeliveryPolicyID = new C1.Win.C1List.C1Combo();
            this.cboShipToleranceID = new C1.Win.C1List.C1Combo();
            this.cboOrderPolicyID = new C1.Win.C1List.C1Combo();
            this.grpLeadTime = new System.Windows.Forms.GroupBox();
            this.txtLTFixedTime = new C1.Win.C1Input.C1NumericEdit();
            this.lblLTSalesATP = new System.Windows.Forms.Label();
            this.lblLTRequisition = new System.Windows.Forms.Label();
            this.lblLTShippingPrepare = new System.Windows.Forms.Label();
            this.lblLTDockToStock = new System.Windows.Forms.Label();
            this.lblLTFixedTime = new System.Windows.Forms.Label();
            this.txtLTSafetyStock = new C1.Win.C1Input.C1NumericEdit();
            this.txtLTOrderPrepare = new C1.Win.C1Input.C1NumericEdit();
            this.txtLTDockToStock = new C1.Win.C1Input.C1NumericEdit();
            this.txtLTSalesATP = new C1.Win.C1Input.C1NumericEdit();
            this.txtLTRequisition = new C1.Win.C1Input.C1NumericEdit();
            this.txtLTShippingPrepare = new C1.Win.C1Input.C1NumericEdit();
            this.lblLTVariableTime = new System.Windows.Forms.Label();
            this.txtLTVariableTime = new C1.Win.C1Input.C1NumericEdit();
            this.lblLTOrderPrepare = new System.Windows.Forms.Label();
            this.lblLTSafetyStock = new System.Windows.Forms.Label();
            this.grpReplenishment = new System.Windows.Forms.GroupBox();
            this.numRoundUpToMin = new C1.Win.C1Input.C1NumericEdit();
            this.numRoundUpToMultiple = new C1.Win.C1Input.C1NumericEdit();
            this.lblRoundUpToMultiple = new System.Windows.Forms.Label();
            this.lblRoundUpToMin = new System.Windows.Forms.Label();
            this.numMinProduce = new C1.Win.C1Input.C1NumericEdit();
            this.numMaxProduce = new C1.Win.C1Input.C1NumericEdit();
            this.lblMaxProduce = new System.Windows.Forms.Label();
            this.lblMinProduce = new System.Windows.Forms.Label();
            this.numPurchasingPrice = new C1.Win.C1Input.C1NumericEdit();
            this.txtVendorName = new System.Windows.Forms.TextBox();
            this.lblVendorName = new System.Windows.Forms.Label();
            this.txtCurrency = new System.Windows.Forms.TextBox();
            this.btnSearchCurrency = new System.Windows.Forms.Button();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblPurchasingPrice = new System.Windows.Forms.Label();
            this.lblPercentConv = new System.Windows.Forms.Label();
            this.lblPercentVouc = new System.Windows.Forms.Label();
            this.chkRequisition = new System.Windows.Forms.CheckBox();
            this.lblPercent = new System.Windows.Forms.Label();
            this.lblStockUM4 = new System.Windows.Forms.Label();
            this.lblStockUM3 = new System.Windows.Forms.Label();
            this.lblStockUM2 = new System.Windows.Forms.Label();
            this.lblStockUM1 = new System.Windows.Forms.Label();
            this.lblBuyingUM3 = new System.Windows.Forms.Label();
            this.lblBuyingUM2 = new System.Windows.Forms.Label();
            this.lblBuyingUM1 = new System.Windows.Forms.Label();
            this.txtSafetyStock = new C1.Win.C1Input.C1NumericEdit();
            this.txtMaximumStock = new C1.Win.C1Input.C1NumericEdit();
            this.txtScrapPercent = new C1.Win.C1Input.C1NumericEdit();
            this.txtVoucherTolerance = new C1.Win.C1Input.C1NumericEdit();
            this.txtIssueSize = new C1.Win.C1Input.C1NumericEdit();
            this.txtMinimumStock = new C1.Win.C1Input.C1NumericEdit();
            this.txtConversionTolerance = new C1.Win.C1Input.C1NumericEdit();
            this.txtOrderPoint = new C1.Win.C1Input.C1NumericEdit();
            this.txtOrderQuantityMultiple = new C1.Win.C1Input.C1NumericEdit();
            this.txtOrderQuantity = new C1.Win.C1Input.C1NumericEdit();
            this.cboVendorLocationID = new C1.Win.C1List.C1Combo();
            this.cboBuyerID = new C1.Win.C1List.C1Combo();
            this.lblOrderPoint = new System.Windows.Forms.Label();
            this.txtPrimaryVendor = new System.Windows.Forms.TextBox();
            this.btnSearchVendor = new System.Windows.Forms.Button();
            this.lblVoucherTolerance = new System.Windows.Forms.Label();
            this.lblVendorLocationID = new System.Windows.Forms.Label();
            this.lblOrderQuantityMultiple = new System.Windows.Forms.Label();
            this.lblOrderQuantity = new System.Windows.Forms.Label();
            this.lblOrderRuleID = new System.Windows.Forms.Label();
            this.lblPrimaryVendor = new System.Windows.Forms.Label();
            this.lblBuyerID = new System.Windows.Forms.Label();
            this.lblIssueSize = new System.Windows.Forms.Label();
            this.cboOrderRuleID = new C1.Win.C1List.C1Combo();
            this.lblConversionTolerance = new System.Windows.Forms.Label();
            this.lblMinimumStock = new System.Windows.Forms.Label();
            this.lblMaximumStock = new System.Windows.Forms.Label();
            this.lblScrapPercent = new System.Windows.Forms.Label();
            this.lblSafetyStock = new System.Windows.Forms.Label();
            this.lblShipToleranceID = new System.Windows.Forms.Label();
            this.lblOrderPolicyID = new System.Windows.Forms.Label();
            this.lblDeliveryPolicyID = new System.Windows.Forms.Label();
            this.chkAutoConversion = new System.Windows.Forms.CheckBox();
            this.grpPlantype = new System.Windows.Forms.GroupBox();
            this.radPlanTypeMPS = new System.Windows.Forms.RadioButton();
            this.radPlanTypeMRP = new System.Windows.Forms.RadioButton();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCost = new System.Windows.Forms.Button();
            this.btnBOM = new System.Windows.Forms.Button();
            this.btnRouting = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.txtRevision = new System.Windows.Forms.TextBox();
            this.cboCCN = new C1.Win.C1List.C1Combo();
            this.lblCCN = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblRevision = new System.Windows.Forms.Label();
            this.btnSearchProductCode = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.btnSearchProductDescription = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.tabProductInfo = new C1.Win.C1Command.C1DockingTab();
            this.tabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.btnViewImage = new System.Windows.Forms.Button();
            this.chkAllowNegativeQty = new System.Windows.Forms.CheckBox();
            this.txtSetUpPair = new System.Windows.Forms.TextBox();
            this.lblSetUpPair = new System.Windows.Forms.Label();
            this.txtRegisteredCode = new System.Windows.Forms.TextBox();
            this.lblRegisteredCode = new System.Windows.Forms.Label();
            this.btnClearPicture = new System.Windows.Forms.Button();
            this.btnChangePicture = new System.Windows.Forms.Button();
            this.picCategory = new System.Windows.Forms.PictureBox();
            this.lblPicture = new System.Windows.Forms.Label();
            this.cboCostMethod = new C1.Win.C1List.C1Combo();
            this.numQuantitySet = new C1.Win.C1Input.C1NumericEdit();
            this.lblQuantitySet = new System.Windows.Forms.Label();
            this.txtTaxCode = new System.Windows.Forms.TextBox();
            this.lblTaxCode = new System.Windows.Forms.Label();
            this.lblProductType = new System.Windows.Forms.Label();
            this.cboProductType = new C1.Win.C1List.C1Combo();
            this.numLicenseFee = new C1.Win.C1Input.C1NumericEdit();
            this.lblLicenseFee = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCostMethod = new System.Windows.Forms.Label();
            this.txtInventor = new System.Windows.Forms.TextBox();
            this.btnInventor = new System.Windows.Forms.Button();
            this.lblInventor = new System.Windows.Forms.Label();
            this.tabPage2 = new C1.Win.C1Command.C1DockingTabPage();
            this.chkMassOrder = new System.Windows.Forms.CheckBox();
            this.chkAVEG = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new C1.Win.C1Command.C1DockingTabPage();
            this.txtACAdjustment = new System.Windows.Forms.TextBox();
            this.btnACAdjustment = new System.Windows.Forms.Button();
            this.lblACAdjustment = new System.Windows.Forms.Label();
            this.txtVAT = new C1.Win.C1Input.C1NumericEdit();
            this.txtImportTax = new C1.Win.C1Input.C1NumericEdit();
            this.txtExportTax = new C1.Win.C1Input.C1NumericEdit();
            this.txtSpecialTax = new C1.Win.C1Input.C1NumericEdit();
            this.lblExportTax = new System.Windows.Forms.Label();
            this.lblVAT = new System.Windows.Forms.Label();
            this.lblSpecialTax = new System.Windows.Forms.Label();
            this.lblImportTax = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtPartNameVN = new System.Windows.Forms.TextBox();
            this.lblPartNameVN = new System.Windows.Forms.Label();
            this.dlgSelectPicture = new System.Windows.Forms.OpenFileDialog();
            this.txtStockTakingCode = new System.Windows.Forms.TextBox();
            this.lblStockTakingCode = new System.Windows.Forms.Label();
            this.btnTakingCode = new System.Windows.Forms.Button();
            this.lblItemGroup = new System.Windows.Forms.Label();
            this.cboItemGroup = new C1.Win.C1List.C1Combo();
            this.cboClassified = new C1.Win.C1List.C1Combo();
            this.lblClassified = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtPartNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFormatCodeID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBinID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLocationID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMasterLocationID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSellingUMID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBuyingUMID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStockUMID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReasonID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboHazardID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFreightClassID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmSetupDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSourceID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategoryID)).BeginInit();
            this.grpWeightAndSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboWidthUMID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboHeightUMID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLengthUMID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboWeightUMID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShelfLife)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAccountCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboQAStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDeliveryPolicyID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboShipToleranceID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrderPolicyID)).BeginInit();
            this.grpLeadTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTFixedTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTSafetyStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTOrderPrepare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTDockToStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTSalesATP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTRequisition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTShippingPrepare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTVariableTime)).BeginInit();
            this.grpReplenishment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRoundUpToMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRoundUpToMultiple)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinProduce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxProduce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPurchasingPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSafetyStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaximumStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtScrapPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucherTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssueSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinimumStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConversionTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderQuantityMultiple)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVendorLocationID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBuyerID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrderRuleID)).BeginInit();
            this.grpPlantype.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabProductInfo)).BeginInit();
            this.tabProductInfo.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCostMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantitySet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProductType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLicenseFee)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImportTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExportTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecialTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboClassified)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPartNumber
            // 
            resources.ApplyResources(this.txtPartNumber, "txtPartNumber");
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtPartNumber.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtPrimaryVendorID
            // 
            resources.ApplyResources(this.txtPrimaryVendorID, "txtPrimaryVendorID");
            this.txtPrimaryVendorID.Name = "txtPrimaryVendorID";
            this.txtPrimaryVendorID.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtPrimaryVendorID.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtLotSize
            // 
            // 
            // 
            // 
            this.txtLotSize.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtLotSize.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            resources.ApplyResources(this.txtLotSize, "txtLotSize");
            this.txtLotSize.DisableOnNoData = false;
            this.txtLotSize.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtLotSize.MaskInfo.ShowLiterals")));
            this.txtLotSize.Name = "txtLotSize";
            this.txtLotSize.NumericInputKeys = C1.Win.C1Input.NumericInputKeyFlags.None;
            this.txtLotSize.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtLotSize.PostValidation.Intervals")))});
            this.txtLotSize.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtLotSize.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // cboFormatCodeID
            // 
            this.cboFormatCodeID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboFormatCodeID, "cboFormatCodeID");
            this.cboFormatCodeID.CaptionHeight = 17;
            this.cboFormatCodeID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboFormatCodeID.ColumnCaptionHeight = 17;
            this.cboFormatCodeID.ColumnFooterHeight = 17;
            this.cboFormatCodeID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboFormatCodeID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboFormatCodeID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown;
            this.cboFormatCodeID.DropDownWidth = 200;
            this.cboFormatCodeID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboFormatCodeID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFormatCodeID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboFormatCodeID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboFormatCodeID.Images"))));
            this.cboFormatCodeID.ItemHeight = 15;
            this.cboFormatCodeID.MatchEntryTimeout = ((long)(2000));
            this.cboFormatCodeID.MaxDropDownItems = ((short)(5));
            this.cboFormatCodeID.MaxLength = 32767;
            this.cboFormatCodeID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboFormatCodeID.Name = "cboFormatCodeID";
            this.cboFormatCodeID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboFormatCodeID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboFormatCodeID.SelectedValueChanged += new System.EventHandler(this.cboFormatCodeID_SelectedValueChanged);
            this.cboFormatCodeID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboFormatCodeID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboFormatCodeID.PropBag = resources.GetString("cboFormatCodeID.PropBag");
            // 
            // lblFormatCodeID
            // 
            resources.ApplyResources(this.lblFormatCodeID, "lblFormatCodeID");
            this.lblFormatCodeID.Name = "lblFormatCodeID";
            // 
            // lblLotSize
            // 
            resources.ApplyResources(this.lblLotSize, "lblLotSize");
            this.lblLotSize.Name = "lblLotSize";
            // 
            // cboBinID
            // 
            this.cboBinID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboBinID, "cboBinID");
            this.cboBinID.CaptionHeight = 17;
            this.cboBinID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboBinID.ColumnCaptionHeight = 17;
            this.cboBinID.ColumnFooterHeight = 17;
            this.cboBinID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboBinID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboBinID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboBinID.DropDownWidth = 200;
            this.cboBinID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboBinID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBinID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboBinID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboBinID.Images"))));
            this.cboBinID.ItemHeight = 15;
            this.cboBinID.MatchEntryTimeout = ((long)(2000));
            this.cboBinID.MaxDropDownItems = ((short)(5));
            this.cboBinID.MaxLength = 32767;
            this.cboBinID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboBinID.Name = "cboBinID";
            this.cboBinID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboBinID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboBinID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboBinID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboBinID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboBinID.PropBag = resources.GetString("cboBinID.PropBag");
            // 
            // cboLocationID
            // 
            this.cboLocationID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboLocationID, "cboLocationID");
            this.cboLocationID.CaptionHeight = 17;
            this.cboLocationID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboLocationID.ColumnCaptionHeight = 17;
            this.cboLocationID.ColumnFooterHeight = 17;
            this.cboLocationID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboLocationID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboLocationID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboLocationID.DropDownWidth = 200;
            this.cboLocationID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboLocationID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLocationID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboLocationID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboLocationID.Images"))));
            this.cboLocationID.ItemHeight = 15;
            this.cboLocationID.MatchEntryTimeout = ((long)(2000));
            this.cboLocationID.MaxDropDownItems = ((short)(5));
            this.cboLocationID.MaxLength = 32767;
            this.cboLocationID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboLocationID.Name = "cboLocationID";
            this.cboLocationID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboLocationID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboLocationID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboLocationID.ItemChanged += new System.EventHandler(this.cboLocationID_ItemChanged);
            this.cboLocationID.SelectedValueChanged += new System.EventHandler(this.cboLocationID_SelectedValueChanged);
            this.cboLocationID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboLocationID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboLocationID.PropBag = resources.GetString("cboLocationID.PropBag");
            // 
            // cboMasterLocationID
            // 
            this.cboMasterLocationID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboMasterLocationID, "cboMasterLocationID");
            this.cboMasterLocationID.CaptionHeight = 17;
            this.cboMasterLocationID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboMasterLocationID.ColumnCaptionHeight = 17;
            this.cboMasterLocationID.ColumnFooterHeight = 17;
            this.cboMasterLocationID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboMasterLocationID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboMasterLocationID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboMasterLocationID.DropDownWidth = 200;
            this.cboMasterLocationID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboMasterLocationID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMasterLocationID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboMasterLocationID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboMasterLocationID.Images"))));
            this.cboMasterLocationID.ItemHeight = 15;
            this.cboMasterLocationID.MatchEntryTimeout = ((long)(2000));
            this.cboMasterLocationID.MaxDropDownItems = ((short)(5));
            this.cboMasterLocationID.MaxLength = 32767;
            this.cboMasterLocationID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboMasterLocationID.Name = "cboMasterLocationID";
            this.cboMasterLocationID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboMasterLocationID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboMasterLocationID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboMasterLocationID.ItemChanged += new System.EventHandler(this.cboMasterLocationID_ItemChanged);
            this.cboMasterLocationID.SelectedValueChanged += new System.EventHandler(this.cboMasterLocationID_SelectedValueChanged);
            this.cboMasterLocationID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboMasterLocationID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboMasterLocationID.PropBag = resources.GetString("cboMasterLocationID.PropBag");
            // 
            // cboSellingUMID
            // 
            this.cboSellingUMID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboSellingUMID, "cboSellingUMID");
            this.cboSellingUMID.CaptionHeight = 17;
            this.cboSellingUMID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboSellingUMID.ColumnCaptionHeight = 17;
            this.cboSellingUMID.ColumnFooterHeight = 17;
            this.cboSellingUMID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboSellingUMID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboSellingUMID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboSellingUMID.DropDownWidth = 200;
            this.cboSellingUMID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboSellingUMID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSellingUMID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboSellingUMID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboSellingUMID.Images"))));
            this.cboSellingUMID.ItemHeight = 15;
            this.cboSellingUMID.MatchEntryTimeout = ((long)(2000));
            this.cboSellingUMID.MaxDropDownItems = ((short)(5));
            this.cboSellingUMID.MaxLength = 32767;
            this.cboSellingUMID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboSellingUMID.Name = "cboSellingUMID";
            this.cboSellingUMID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboSellingUMID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboSellingUMID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboSellingUMID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboSellingUMID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboSellingUMID.PropBag = resources.GetString("cboSellingUMID.PropBag");
            // 
            // cboBuyingUMID
            // 
            this.cboBuyingUMID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboBuyingUMID, "cboBuyingUMID");
            this.cboBuyingUMID.CaptionHeight = 17;
            this.cboBuyingUMID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboBuyingUMID.ColumnCaptionHeight = 17;
            this.cboBuyingUMID.ColumnFooterHeight = 17;
            this.cboBuyingUMID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboBuyingUMID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboBuyingUMID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboBuyingUMID.DropDownWidth = 200;
            this.cboBuyingUMID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboBuyingUMID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBuyingUMID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboBuyingUMID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboBuyingUMID.Images"))));
            this.cboBuyingUMID.ItemHeight = 15;
            this.cboBuyingUMID.MatchEntryTimeout = ((long)(2000));
            this.cboBuyingUMID.MaxDropDownItems = ((short)(5));
            this.cboBuyingUMID.MaxLength = 32767;
            this.cboBuyingUMID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboBuyingUMID.Name = "cboBuyingUMID";
            this.cboBuyingUMID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboBuyingUMID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboBuyingUMID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboBuyingUMID.TextChanged += new System.EventHandler(this.cboBuyingUMID_TextChanged);
            this.cboBuyingUMID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboBuyingUMID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboBuyingUMID.PropBag = resources.GetString("cboBuyingUMID.PropBag");
            // 
            // cboStockUMID
            // 
            this.cboStockUMID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboStockUMID, "cboStockUMID");
            this.cboStockUMID.CaptionHeight = 17;
            this.cboStockUMID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboStockUMID.ColumnCaptionHeight = 17;
            this.cboStockUMID.ColumnFooterHeight = 17;
            this.cboStockUMID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboStockUMID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboStockUMID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboStockUMID.DropDownWidth = 200;
            this.cboStockUMID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboStockUMID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStockUMID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboStockUMID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboStockUMID.Images"))));
            this.cboStockUMID.ItemHeight = 15;
            this.cboStockUMID.MatchEntryTimeout = ((long)(2000));
            this.cboStockUMID.MaxDropDownItems = ((short)(5));
            this.cboStockUMID.MaxLength = 32767;
            this.cboStockUMID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboStockUMID.Name = "cboStockUMID";
            this.cboStockUMID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboStockUMID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboStockUMID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboStockUMID.TextChanged += new System.EventHandler(this.cboStockUMID_TextChanged);
            this.cboStockUMID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboStockUMID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboStockUMID.PropBag = resources.GetString("cboStockUMID.PropBag");
            // 
            // cboReasonID
            // 
            this.cboReasonID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboReasonID, "cboReasonID");
            this.cboReasonID.CaptionHeight = 17;
            this.cboReasonID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboReasonID.ColumnCaptionHeight = 17;
            this.cboReasonID.ColumnFooterHeight = 17;
            this.cboReasonID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboReasonID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboReasonID.DropDownWidth = 200;
            this.cboReasonID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboReasonID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboReasonID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboReasonID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboReasonID.Images"))));
            this.cboReasonID.ItemHeight = 15;
            this.cboReasonID.MatchEntryTimeout = ((long)(2000));
            this.cboReasonID.MaxDropDownItems = ((short)(5));
            this.cboReasonID.MaxLength = 32767;
            this.cboReasonID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboReasonID.Name = "cboReasonID";
            this.cboReasonID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboReasonID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboReasonID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboReasonID.PropBag = resources.GetString("cboReasonID.PropBag");
            // 
            // cboHazardID
            // 
            this.cboHazardID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboHazardID, "cboHazardID");
            this.cboHazardID.CaptionHeight = 17;
            this.cboHazardID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboHazardID.ColumnCaptionHeight = 17;
            this.cboHazardID.ColumnFooterHeight = 17;
            this.cboHazardID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboHazardID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboHazardID.DropDownWidth = 200;
            this.cboHazardID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboHazardID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHazardID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboHazardID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboHazardID.Images"))));
            this.cboHazardID.ItemHeight = 15;
            this.cboHazardID.MatchEntryTimeout = ((long)(2000));
            this.cboHazardID.MaxDropDownItems = ((short)(5));
            this.cboHazardID.MaxLength = 32767;
            this.cboHazardID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboHazardID.Name = "cboHazardID";
            this.cboHazardID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboHazardID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboHazardID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboHazardID.PropBag = resources.GetString("cboHazardID.PropBag");
            // 
            // cboFreightClassID
            // 
            this.cboFreightClassID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboFreightClassID, "cboFreightClassID");
            this.cboFreightClassID.CaptionHeight = 17;
            this.cboFreightClassID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboFreightClassID.ColumnCaptionHeight = 17;
            this.cboFreightClassID.ColumnFooterHeight = 17;
            this.cboFreightClassID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboFreightClassID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboFreightClassID.DropDownWidth = 200;
            this.cboFreightClassID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboFreightClassID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFreightClassID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboFreightClassID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboFreightClassID.Images"))));
            this.cboFreightClassID.ItemHeight = 15;
            this.cboFreightClassID.MatchEntryTimeout = ((long)(2000));
            this.cboFreightClassID.MaxDropDownItems = ((short)(5));
            this.cboFreightClassID.MaxLength = 32767;
            this.cboFreightClassID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboFreightClassID.Name = "cboFreightClassID";
            this.cboFreightClassID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboFreightClassID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboFreightClassID.PropBag = resources.GetString("cboFreightClassID.PropBag");
            // 
            // lblBinID
            // 
            resources.ApplyResources(this.lblBinID, "lblBinID");
            this.lblBinID.Name = "lblBinID";
            // 
            // lblLocationID
            // 
            resources.ApplyResources(this.lblLocationID, "lblLocationID");
            this.lblLocationID.Name = "lblLocationID";
            // 
            // lblMasterLocationID
            // 
            resources.ApplyResources(this.lblMasterLocationID, "lblMasterLocationID");
            this.lblMasterLocationID.Name = "lblMasterLocationID";
            // 
            // lblQAStatus
            // 
            resources.ApplyResources(this.lblQAStatus, "lblQAStatus");
            this.lblQAStatus.Name = "lblQAStatus";
            // 
            // lblShelfLife
            // 
            resources.ApplyResources(this.lblShelfLife, "lblShelfLife");
            this.lblShelfLife.Name = "lblShelfLife";
            // 
            // lblAccountCode
            // 
            resources.ApplyResources(this.lblAccountCode, "lblAccountCode");
            this.lblAccountCode.Name = "lblAccountCode";
            // 
            // lblSellingUMID
            // 
            this.lblSellingUMID.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblSellingUMID, "lblSellingUMID");
            this.lblSellingUMID.ForeColor = System.Drawing.Color.Maroon;
            this.lblSellingUMID.Name = "lblSellingUMID";
            // 
            // dtmSetupDate
            // 
            this.dtmSetupDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.dtmSetupDate, "dtmSetupDate");
            // 
            // 
            // 
            this.dtmSetupDate.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("dtmSetupDate.Calendar.Font")));
            this.dtmSetupDate.Calendar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dtmSetupDate.Calendar.ImeMode")));
            this.dtmSetupDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmSetupDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmSetupDate.Name = "dtmSetupDate";
            this.dtmSetupDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmSetupDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.dtmSetupDate.Enter += new System.EventHandler(this.OnEnterControl);
            this.dtmSetupDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtmSetupDate_KeyDown);
            this.dtmSetupDate.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // cboSourceID
            // 
            this.cboSourceID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboSourceID, "cboSourceID");
            this.cboSourceID.CaptionHeight = 17;
            this.cboSourceID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboSourceID.ColumnCaptionHeight = 17;
            this.cboSourceID.ColumnFooterHeight = 17;
            this.cboSourceID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboSourceID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboSourceID.DropDownWidth = 200;
            this.cboSourceID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboSourceID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSourceID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboSourceID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboSourceID.Images"))));
            this.cboSourceID.ItemHeight = 15;
            this.cboSourceID.MatchEntryTimeout = ((long)(2000));
            this.cboSourceID.MaxDropDownItems = ((short)(5));
            this.cboSourceID.MaxLength = 32767;
            this.cboSourceID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboSourceID.Name = "cboSourceID";
            this.cboSourceID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboSourceID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboSourceID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboSourceID.PropBag = resources.GetString("cboSourceID.PropBag");
            // 
            // cboCategoryID
            // 
            this.cboCategoryID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboCategoryID, "cboCategoryID");
            this.cboCategoryID.CaptionHeight = 17;
            this.cboCategoryID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCategoryID.ColumnCaptionHeight = 17;
            this.cboCategoryID.ColumnFooterHeight = 17;
            this.cboCategoryID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCategoryID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCategoryID.DropDownWidth = 200;
            this.cboCategoryID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCategoryID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategoryID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCategoryID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCategoryID.Images"))));
            this.cboCategoryID.ItemHeight = 15;
            this.cboCategoryID.MatchEntryTimeout = ((long)(2000));
            this.cboCategoryID.MaxDropDownItems = ((short)(5));
            this.cboCategoryID.MaxLength = 32767;
            this.cboCategoryID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCategoryID.Name = "cboCategoryID";
            this.cboCategoryID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboCategoryID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCategoryID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboCategoryID.PropBag = resources.GetString("cboCategoryID.PropBag");
            // 
            // lblSourceID
            // 
            resources.ApplyResources(this.lblSourceID, "lblSourceID");
            this.lblSourceID.Name = "lblSourceID";
            // 
            // lblStockUMID
            // 
            this.lblStockUMID.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblStockUMID, "lblStockUMID");
            this.lblStockUMID.ForeColor = System.Drawing.Color.Maroon;
            this.lblStockUMID.Name = "lblStockUMID";
            // 
            // lblCategoryID
            // 
            resources.ApplyResources(this.lblCategoryID, "lblCategoryID");
            this.lblCategoryID.Name = "lblCategoryID";
            // 
            // chkMakeItem
            // 
            resources.ApplyResources(this.chkMakeItem, "chkMakeItem");
            this.chkMakeItem.Name = "chkMakeItem";
            this.chkMakeItem.CheckedChanged += new System.EventHandler(this.chkMakeItem_CheckedChanged);
            // 
            // lblSetupDate
            // 
            this.lblSetupDate.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblSetupDate, "lblSetupDate");
            this.lblSetupDate.Name = "lblSetupDate";
            // 
            // lblReasonID
            // 
            resources.ApplyResources(this.lblReasonID, "lblReasonID");
            this.lblReasonID.Name = "lblReasonID";
            // 
            // txtOtherInfo2
            // 
            resources.ApplyResources(this.txtOtherInfo2, "txtOtherInfo2");
            this.txtOtherInfo2.Name = "txtOtherInfo2";
            this.txtOtherInfo2.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtOtherInfo2.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblOtherInfo2
            // 
            resources.ApplyResources(this.lblOtherInfo2, "lblOtherInfo2");
            this.lblOtherInfo2.Name = "lblOtherInfo2";
            // 
            // txtOtherInfo1
            // 
            resources.ApplyResources(this.txtOtherInfo1, "txtOtherInfo1");
            this.txtOtherInfo1.Name = "txtOtherInfo1";
            this.txtOtherInfo1.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtOtherInfo1.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblOtherInfo1
            // 
            resources.ApplyResources(this.lblOtherInfo1, "lblOtherInfo1");
            this.lblOtherInfo1.Name = "lblOtherInfo1";
            // 
            // lblBuyingUMID
            // 
            this.lblBuyingUMID.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblBuyingUMID, "lblBuyingUMID");
            this.lblBuyingUMID.ForeColor = System.Drawing.Color.Maroon;
            this.lblBuyingUMID.Name = "lblBuyingUMID";
            // 
            // grpWeightAndSize
            // 
            this.grpWeightAndSize.Controls.Add(this.txtLength);
            this.grpWeightAndSize.Controls.Add(this.cboWidthUMID);
            this.grpWeightAndSize.Controls.Add(this.cboHeightUMID);
            this.grpWeightAndSize.Controls.Add(this.cboLengthUMID);
            this.grpWeightAndSize.Controls.Add(this.lblWeight);
            this.grpWeightAndSize.Controls.Add(this.lblHeightUMID);
            this.grpWeightAndSize.Controls.Add(this.lblHeight);
            this.grpWeightAndSize.Controls.Add(this.lblWidthUMID);
            this.grpWeightAndSize.Controls.Add(this.lblWidth);
            this.grpWeightAndSize.Controls.Add(this.lblLengthUMID);
            this.grpWeightAndSize.Controls.Add(this.lblLength);
            this.grpWeightAndSize.Controls.Add(this.cboWeightUMID);
            this.grpWeightAndSize.Controls.Add(this.lblWeightUMID);
            this.grpWeightAndSize.Controls.Add(this.txtHeight);
            this.grpWeightAndSize.Controls.Add(this.txtWidth);
            this.grpWeightAndSize.Controls.Add(this.txtWeight);
            resources.ApplyResources(this.grpWeightAndSize, "grpWeightAndSize");
            this.grpWeightAndSize.Name = "grpWeightAndSize";
            this.grpWeightAndSize.TabStop = false;
            // 
            // txtLength
            // 
            // 
            // 
            // 
            this.txtLength.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtLength.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtLength.DisableOnNoData = false;
            resources.ApplyResources(this.txtLength, "txtLength");
            this.txtLength.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtLength.MaskInfo.ShowLiterals")));
            this.txtLength.Name = "txtLength";
            this.txtLength.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)));
            this.txtLength.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtLength.PostValidation.Intervals")))});
            this.txtLength.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtLength.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // cboWidthUMID
            // 
            this.cboWidthUMID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboWidthUMID, "cboWidthUMID");
            this.cboWidthUMID.CaptionHeight = 17;
            this.cboWidthUMID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboWidthUMID.ColumnCaptionHeight = 17;
            this.cboWidthUMID.ColumnFooterHeight = 17;
            this.cboWidthUMID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboWidthUMID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboWidthUMID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftUp;
            this.cboWidthUMID.DropDownWidth = 200;
            this.cboWidthUMID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboWidthUMID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWidthUMID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboWidthUMID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboWidthUMID.Images"))));
            this.cboWidthUMID.ItemHeight = 15;
            this.cboWidthUMID.MatchEntryTimeout = ((long)(2000));
            this.cboWidthUMID.MaxDropDownItems = ((short)(5));
            this.cboWidthUMID.MaxLength = 32767;
            this.cboWidthUMID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboWidthUMID.Name = "cboWidthUMID";
            this.cboWidthUMID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboWidthUMID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboWidthUMID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboWidthUMID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboWidthUMID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboWidthUMID.PropBag = resources.GetString("cboWidthUMID.PropBag");
            // 
            // cboHeightUMID
            // 
            this.cboHeightUMID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboHeightUMID, "cboHeightUMID");
            this.cboHeightUMID.CaptionHeight = 17;
            this.cboHeightUMID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboHeightUMID.ColumnCaptionHeight = 17;
            this.cboHeightUMID.ColumnFooterHeight = 17;
            this.cboHeightUMID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboHeightUMID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboHeightUMID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftUp;
            this.cboHeightUMID.DropDownWidth = 200;
            this.cboHeightUMID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboHeightUMID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHeightUMID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboHeightUMID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboHeightUMID.Images"))));
            this.cboHeightUMID.ItemHeight = 15;
            this.cboHeightUMID.MatchEntryTimeout = ((long)(2000));
            this.cboHeightUMID.MaxDropDownItems = ((short)(5));
            this.cboHeightUMID.MaxLength = 32767;
            this.cboHeightUMID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboHeightUMID.Name = "cboHeightUMID";
            this.cboHeightUMID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboHeightUMID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboHeightUMID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboHeightUMID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboHeightUMID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboHeightUMID.PropBag = resources.GetString("cboHeightUMID.PropBag");
            // 
            // cboLengthUMID
            // 
            this.cboLengthUMID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboLengthUMID, "cboLengthUMID");
            this.cboLengthUMID.CaptionHeight = 17;
            this.cboLengthUMID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboLengthUMID.ColumnCaptionHeight = 17;
            this.cboLengthUMID.ColumnFooterHeight = 17;
            this.cboLengthUMID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboLengthUMID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboLengthUMID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftUp;
            this.cboLengthUMID.DropDownWidth = 200;
            this.cboLengthUMID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboLengthUMID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLengthUMID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboLengthUMID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboLengthUMID.Images"))));
            this.cboLengthUMID.ItemHeight = 15;
            this.cboLengthUMID.MatchEntryTimeout = ((long)(2000));
            this.cboLengthUMID.MaxDropDownItems = ((short)(5));
            this.cboLengthUMID.MaxLength = 32767;
            this.cboLengthUMID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboLengthUMID.Name = "cboLengthUMID";
            this.cboLengthUMID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboLengthUMID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboLengthUMID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboLengthUMID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboLengthUMID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboLengthUMID.PropBag = resources.GetString("cboLengthUMID.PropBag");
            // 
            // lblWeight
            // 
            resources.ApplyResources(this.lblWeight, "lblWeight");
            this.lblWeight.Name = "lblWeight";
            // 
            // lblHeightUMID
            // 
            this.lblHeightUMID.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblHeightUMID, "lblHeightUMID");
            this.lblHeightUMID.Name = "lblHeightUMID";
            // 
            // lblHeight
            // 
            resources.ApplyResources(this.lblHeight, "lblHeight");
            this.lblHeight.Name = "lblHeight";
            // 
            // lblWidthUMID
            // 
            this.lblWidthUMID.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblWidthUMID, "lblWidthUMID");
            this.lblWidthUMID.Name = "lblWidthUMID";
            // 
            // lblWidth
            // 
            resources.ApplyResources(this.lblWidth, "lblWidth");
            this.lblWidth.Name = "lblWidth";
            // 
            // lblLengthUMID
            // 
            this.lblLengthUMID.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblLengthUMID, "lblLengthUMID");
            this.lblLengthUMID.Name = "lblLengthUMID";
            // 
            // lblLength
            // 
            resources.ApplyResources(this.lblLength, "lblLength");
            this.lblLength.Name = "lblLength";
            // 
            // cboWeightUMID
            // 
            this.cboWeightUMID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboWeightUMID, "cboWeightUMID");
            this.cboWeightUMID.CaptionHeight = 17;
            this.cboWeightUMID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboWeightUMID.ColumnCaptionHeight = 17;
            this.cboWeightUMID.ColumnFooterHeight = 17;
            this.cboWeightUMID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboWeightUMID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboWeightUMID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftUp;
            this.cboWeightUMID.DropDownWidth = 200;
            this.cboWeightUMID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboWeightUMID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWeightUMID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboWeightUMID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboWeightUMID.Images"))));
            this.cboWeightUMID.ItemHeight = 15;
            this.cboWeightUMID.MatchEntryTimeout = ((long)(2000));
            this.cboWeightUMID.MaxDropDownItems = ((short)(5));
            this.cboWeightUMID.MaxLength = 32767;
            this.cboWeightUMID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboWeightUMID.Name = "cboWeightUMID";
            this.cboWeightUMID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboWeightUMID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboWeightUMID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboWeightUMID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboWeightUMID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboWeightUMID.PropBag = resources.GetString("cboWeightUMID.PropBag");
            // 
            // lblWeightUMID
            // 
            this.lblWeightUMID.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblWeightUMID, "lblWeightUMID");
            this.lblWeightUMID.Name = "lblWeightUMID";
            // 
            // txtHeight
            // 
            // 
            // 
            // 
            this.txtHeight.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtHeight.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtHeight.DisableOnNoData = false;
            resources.ApplyResources(this.txtHeight, "txtHeight");
            this.txtHeight.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtHeight.MaskInfo.ShowLiterals")));
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)));
            this.txtHeight.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtHeight.PostValidation.Intervals")))});
            this.txtHeight.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtHeight.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtWidth
            // 
            // 
            // 
            // 
            this.txtWidth.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtWidth.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtWidth.DisableOnNoData = false;
            resources.ApplyResources(this.txtWidth, "txtWidth");
            this.txtWidth.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtWidth.MaskInfo.ShowLiterals")));
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)));
            this.txtWidth.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtWidth.PostValidation.Intervals")))});
            this.txtWidth.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtWidth.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtWeight
            // 
            // 
            // 
            // 
            this.txtWeight.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtWeight.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtWeight.DisableOnNoData = false;
            resources.ApplyResources(this.txtWeight, "txtWeight");
            this.txtWeight.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtWeight.MaskInfo.ShowLiterals")));
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)));
            this.txtWeight.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtWeight.PostValidation.Intervals")))});
            this.txtWeight.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtWeight.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblHazardID
            // 
            resources.ApplyResources(this.lblHazardID, "lblHazardID");
            this.lblHazardID.Name = "lblHazardID";
            // 
            // lblFreightClassID
            // 
            resources.ApplyResources(this.lblFreightClassID, "lblFreightClassID");
            this.lblFreightClassID.Name = "lblFreightClassID";
            // 
            // lblPartNumber
            // 
            resources.ApplyResources(this.lblPartNumber, "lblPartNumber");
            this.lblPartNumber.Name = "lblPartNumber";
            // 
            // chkStock
            // 
            resources.ApplyResources(this.chkStock, "chkStock");
            this.chkStock.Name = "chkStock";
            // 
            // chkLotControl
            // 
            resources.ApplyResources(this.chkLotControl, "chkLotControl");
            this.chkLotControl.Name = "chkLotControl";
            // 
            // txtShelfLife
            // 
            // 
            // 
            // 
            this.txtShelfLife.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtShelfLife.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtShelfLife.DisableOnNoData = false;
            resources.ApplyResources(this.txtShelfLife, "txtShelfLife");
            this.txtShelfLife.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtShelfLife.MaskInfo.ShowLiterals")));
            this.txtShelfLife.Name = "txtShelfLife";
            this.txtShelfLife.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtShelfLife.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtShelfLife.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // cboAccountCode
            // 
            this.cboAccountCode.AddItemSeparator = ';';
            resources.ApplyResources(this.cboAccountCode, "cboAccountCode");
            this.cboAccountCode.CaptionHeight = 17;
            this.cboAccountCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboAccountCode.ColumnCaptionHeight = 17;
            this.cboAccountCode.ColumnFooterHeight = 17;
            this.cboAccountCode.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboAccountCode.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboAccountCode.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboAccountCode.DropDownWidth = 200;
            this.cboAccountCode.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboAccountCode.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAccountCode.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboAccountCode.Images.Add(((System.Drawing.Image)(resources.GetObject("cboAccountCode.Images"))));
            this.cboAccountCode.ItemHeight = 15;
            this.cboAccountCode.MatchEntryTimeout = ((long)(2000));
            this.cboAccountCode.MaxDropDownItems = ((short)(5));
            this.cboAccountCode.MaxLength = 32767;
            this.cboAccountCode.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboAccountCode.Name = "cboAccountCode";
            this.cboAccountCode.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboAccountCode.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboAccountCode.PropBag = resources.GetString("cboAccountCode.PropBag");
            // 
            // cboQAStatus
            // 
            this.cboQAStatus.AddItemSeparator = ';';
            resources.ApplyResources(this.cboQAStatus, "cboQAStatus");
            this.cboQAStatus.CaptionHeight = 17;
            this.cboQAStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboQAStatus.ColumnCaptionHeight = 17;
            this.cboQAStatus.ColumnFooterHeight = 17;
            this.cboQAStatus.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboQAStatus.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboQAStatus.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboQAStatus.DropDownWidth = 400;
            this.cboQAStatus.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboQAStatus.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboQAStatus.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboQAStatus.Images.Add(((System.Drawing.Image)(resources.GetObject("cboQAStatus.Images"))));
            this.cboQAStatus.ItemHeight = 15;
            this.cboQAStatus.MatchEntryTimeout = ((long)(2000));
            this.cboQAStatus.MaxDropDownItems = ((short)(5));
            this.cboQAStatus.MaxLength = 32767;
            this.cboQAStatus.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboQAStatus.Name = "cboQAStatus";
            this.cboQAStatus.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboQAStatus.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboQAStatus.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboQAStatus.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboQAStatus.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboQAStatus.PropBag = resources.GetString("cboQAStatus.PropBag");
            // 
            // cboDeliveryPolicyID
            // 
            this.cboDeliveryPolicyID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboDeliveryPolicyID, "cboDeliveryPolicyID");
            this.cboDeliveryPolicyID.CaptionHeight = 17;
            this.cboDeliveryPolicyID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboDeliveryPolicyID.ColumnCaptionHeight = 17;
            this.cboDeliveryPolicyID.ColumnFooterHeight = 17;
            this.cboDeliveryPolicyID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboDeliveryPolicyID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboDeliveryPolicyID.DropDownWidth = 200;
            this.cboDeliveryPolicyID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboDeliveryPolicyID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDeliveryPolicyID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboDeliveryPolicyID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboDeliveryPolicyID.Images"))));
            this.cboDeliveryPolicyID.ItemHeight = 15;
            this.cboDeliveryPolicyID.MatchEntryTimeout = ((long)(2000));
            this.cboDeliveryPolicyID.MaxDropDownItems = ((short)(5));
            this.cboDeliveryPolicyID.MaxLength = 32767;
            this.cboDeliveryPolicyID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboDeliveryPolicyID.Name = "cboDeliveryPolicyID";
            this.cboDeliveryPolicyID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboDeliveryPolicyID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboDeliveryPolicyID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboDeliveryPolicyID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboDeliveryPolicyID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboDeliveryPolicyID.PropBag = resources.GetString("cboDeliveryPolicyID.PropBag");
            // 
            // cboShipToleranceID
            // 
            this.cboShipToleranceID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboShipToleranceID, "cboShipToleranceID");
            this.cboShipToleranceID.CaptionHeight = 17;
            this.cboShipToleranceID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboShipToleranceID.ColumnCaptionHeight = 17;
            this.cboShipToleranceID.ColumnFooterHeight = 17;
            this.cboShipToleranceID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboShipToleranceID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboShipToleranceID.DropDownWidth = 200;
            this.cboShipToleranceID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboShipToleranceID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboShipToleranceID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboShipToleranceID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboShipToleranceID.Images"))));
            this.cboShipToleranceID.ItemHeight = 15;
            this.cboShipToleranceID.MatchEntryTimeout = ((long)(2000));
            this.cboShipToleranceID.MaxDropDownItems = ((short)(5));
            this.cboShipToleranceID.MaxLength = 32767;
            this.cboShipToleranceID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboShipToleranceID.Name = "cboShipToleranceID";
            this.cboShipToleranceID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboShipToleranceID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboShipToleranceID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboShipToleranceID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboShipToleranceID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboShipToleranceID.PropBag = resources.GetString("cboShipToleranceID.PropBag");
            // 
            // cboOrderPolicyID
            // 
            this.cboOrderPolicyID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboOrderPolicyID, "cboOrderPolicyID");
            this.cboOrderPolicyID.CaptionHeight = 17;
            this.cboOrderPolicyID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboOrderPolicyID.ColumnCaptionHeight = 17;
            this.cboOrderPolicyID.ColumnFooterHeight = 17;
            this.cboOrderPolicyID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboOrderPolicyID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboOrderPolicyID.DropDownWidth = 200;
            this.cboOrderPolicyID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboOrderPolicyID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOrderPolicyID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboOrderPolicyID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboOrderPolicyID.Images"))));
            this.cboOrderPolicyID.ItemHeight = 15;
            this.cboOrderPolicyID.MatchEntryTimeout = ((long)(2000));
            this.cboOrderPolicyID.MaxDropDownItems = ((short)(5));
            this.cboOrderPolicyID.MaxLength = 32767;
            this.cboOrderPolicyID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboOrderPolicyID.Name = "cboOrderPolicyID";
            this.cboOrderPolicyID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboOrderPolicyID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboOrderPolicyID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboOrderPolicyID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboOrderPolicyID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboOrderPolicyID.PropBag = resources.GetString("cboOrderPolicyID.PropBag");
            // 
            // grpLeadTime
            // 
            this.grpLeadTime.Controls.Add(this.txtLTFixedTime);
            this.grpLeadTime.Controls.Add(this.lblLTSalesATP);
            this.grpLeadTime.Controls.Add(this.lblLTRequisition);
            this.grpLeadTime.Controls.Add(this.lblLTShippingPrepare);
            this.grpLeadTime.Controls.Add(this.lblLTDockToStock);
            this.grpLeadTime.Controls.Add(this.lblLTFixedTime);
            this.grpLeadTime.Controls.Add(this.txtLTSafetyStock);
            this.grpLeadTime.Controls.Add(this.txtLTOrderPrepare);
            this.grpLeadTime.Controls.Add(this.txtLTDockToStock);
            this.grpLeadTime.Controls.Add(this.txtLTSalesATP);
            this.grpLeadTime.Controls.Add(this.txtLTRequisition);
            this.grpLeadTime.Controls.Add(this.txtLTShippingPrepare);
            this.grpLeadTime.Controls.Add(this.lblLTVariableTime);
            this.grpLeadTime.Controls.Add(this.txtLTVariableTime);
            this.grpLeadTime.Controls.Add(this.lblLTOrderPrepare);
            this.grpLeadTime.Controls.Add(this.lblLTSafetyStock);
            this.grpLeadTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.grpLeadTime, "grpLeadTime");
            this.grpLeadTime.Name = "grpLeadTime";
            this.grpLeadTime.TabStop = false;
            // 
            // txtLTFixedTime
            // 
            // 
            // 
            // 
            this.txtLTFixedTime.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtLTFixedTime.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtLTFixedTime.DisableOnNoData = false;
            resources.ApplyResources(this.txtLTFixedTime, "txtLTFixedTime");
            this.txtLTFixedTime.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtLTFixedTime.MaskInfo.ShowLiterals")));
            this.txtLTFixedTime.Name = "txtLTFixedTime";
            this.txtLTFixedTime.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtLTFixedTime.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtLTFixedTime.PostValidation.Intervals")))});
            this.txtLTFixedTime.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtLTFixedTime.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblLTSalesATP
            // 
            resources.ApplyResources(this.lblLTSalesATP, "lblLTSalesATP");
            this.lblLTSalesATP.Name = "lblLTSalesATP";
            // 
            // lblLTRequisition
            // 
            resources.ApplyResources(this.lblLTRequisition, "lblLTRequisition");
            this.lblLTRequisition.Name = "lblLTRequisition";
            // 
            // lblLTShippingPrepare
            // 
            resources.ApplyResources(this.lblLTShippingPrepare, "lblLTShippingPrepare");
            this.lblLTShippingPrepare.Name = "lblLTShippingPrepare";
            // 
            // lblLTDockToStock
            // 
            resources.ApplyResources(this.lblLTDockToStock, "lblLTDockToStock");
            this.lblLTDockToStock.Name = "lblLTDockToStock";
            // 
            // lblLTFixedTime
            // 
            resources.ApplyResources(this.lblLTFixedTime, "lblLTFixedTime");
            this.lblLTFixedTime.Name = "lblLTFixedTime";
            // 
            // txtLTSafetyStock
            // 
            // 
            // 
            // 
            this.txtLTSafetyStock.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtLTSafetyStock.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtLTSafetyStock.DisableOnNoData = false;
            resources.ApplyResources(this.txtLTSafetyStock, "txtLTSafetyStock");
            this.txtLTSafetyStock.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtLTSafetyStock.MaskInfo.ShowLiterals")));
            this.txtLTSafetyStock.Name = "txtLTSafetyStock";
            this.txtLTSafetyStock.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtLTSafetyStock.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtLTSafetyStock.PostValidation.Intervals")))});
            this.txtLTSafetyStock.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtLTSafetyStock.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtLTOrderPrepare
            // 
            // 
            // 
            // 
            this.txtLTOrderPrepare.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtLTOrderPrepare.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtLTOrderPrepare.DisableOnNoData = false;
            resources.ApplyResources(this.txtLTOrderPrepare, "txtLTOrderPrepare");
            this.txtLTOrderPrepare.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtLTOrderPrepare.MaskInfo.ShowLiterals")));
            this.txtLTOrderPrepare.Name = "txtLTOrderPrepare";
            this.txtLTOrderPrepare.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtLTOrderPrepare.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtLTOrderPrepare.PostValidation.Intervals")))});
            this.txtLTOrderPrepare.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtLTOrderPrepare.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtLTDockToStock
            // 
            // 
            // 
            // 
            this.txtLTDockToStock.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtLTDockToStock.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtLTDockToStock.DisableOnNoData = false;
            resources.ApplyResources(this.txtLTDockToStock, "txtLTDockToStock");
            this.txtLTDockToStock.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtLTDockToStock.MaskInfo.ShowLiterals")));
            this.txtLTDockToStock.Name = "txtLTDockToStock";
            this.txtLTDockToStock.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtLTDockToStock.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtLTDockToStock.PostValidation.Intervals")))});
            this.txtLTDockToStock.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtLTDockToStock.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtLTSalesATP
            // 
            // 
            // 
            // 
            this.txtLTSalesATP.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtLTSalesATP.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtLTSalesATP.DisableOnNoData = false;
            resources.ApplyResources(this.txtLTSalesATP, "txtLTSalesATP");
            this.txtLTSalesATP.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtLTSalesATP.MaskInfo.ShowLiterals")));
            this.txtLTSalesATP.Name = "txtLTSalesATP";
            this.txtLTSalesATP.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtLTSalesATP.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtLTSalesATP.PostValidation.Intervals")))});
            this.txtLTSalesATP.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtLTSalesATP.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtLTRequisition
            // 
            // 
            // 
            // 
            this.txtLTRequisition.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtLTRequisition.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtLTRequisition.DisableOnNoData = false;
            resources.ApplyResources(this.txtLTRequisition, "txtLTRequisition");
            this.txtLTRequisition.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtLTRequisition.MaskInfo.ShowLiterals")));
            this.txtLTRequisition.Name = "txtLTRequisition";
            this.txtLTRequisition.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtLTRequisition.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtLTRequisition.PostValidation.Intervals")))});
            this.txtLTRequisition.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtLTRequisition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLTRequisition_KeyDown);
            this.txtLTRequisition.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtLTShippingPrepare
            // 
            // 
            // 
            // 
            this.txtLTShippingPrepare.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtLTShippingPrepare.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtLTShippingPrepare.DisableOnNoData = false;
            resources.ApplyResources(this.txtLTShippingPrepare, "txtLTShippingPrepare");
            this.txtLTShippingPrepare.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtLTShippingPrepare.MaskInfo.ShowLiterals")));
            this.txtLTShippingPrepare.Name = "txtLTShippingPrepare";
            this.txtLTShippingPrepare.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtLTShippingPrepare.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtLTShippingPrepare.PostValidation.Intervals")))});
            this.txtLTShippingPrepare.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtLTShippingPrepare.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblLTVariableTime
            // 
            resources.ApplyResources(this.lblLTVariableTime, "lblLTVariableTime");
            this.lblLTVariableTime.Name = "lblLTVariableTime";
            // 
            // txtLTVariableTime
            // 
            // 
            // 
            // 
            this.txtLTVariableTime.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtLTVariableTime.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtLTVariableTime.DisableOnNoData = false;
            resources.ApplyResources(this.txtLTVariableTime, "txtLTVariableTime");
            this.txtLTVariableTime.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtLTVariableTime.MaskInfo.ShowLiterals")));
            this.txtLTVariableTime.Name = "txtLTVariableTime";
            this.txtLTVariableTime.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtLTVariableTime.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtLTVariableTime.PostValidation.Intervals")))});
            this.txtLTVariableTime.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtLTVariableTime.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblLTOrderPrepare
            // 
            resources.ApplyResources(this.lblLTOrderPrepare, "lblLTOrderPrepare");
            this.lblLTOrderPrepare.Name = "lblLTOrderPrepare";
            // 
            // lblLTSafetyStock
            // 
            resources.ApplyResources(this.lblLTSafetyStock, "lblLTSafetyStock");
            this.lblLTSafetyStock.Name = "lblLTSafetyStock";
            // 
            // grpReplenishment
            // 
            this.grpReplenishment.Controls.Add(this.numRoundUpToMin);
            this.grpReplenishment.Controls.Add(this.numRoundUpToMultiple);
            this.grpReplenishment.Controls.Add(this.lblRoundUpToMultiple);
            this.grpReplenishment.Controls.Add(this.lblRoundUpToMin);
            this.grpReplenishment.Controls.Add(this.numMinProduce);
            this.grpReplenishment.Controls.Add(this.numMaxProduce);
            this.grpReplenishment.Controls.Add(this.lblMaxProduce);
            this.grpReplenishment.Controls.Add(this.lblMinProduce);
            this.grpReplenishment.Controls.Add(this.numPurchasingPrice);
            this.grpReplenishment.Controls.Add(this.txtVendorName);
            this.grpReplenishment.Controls.Add(this.lblVendorName);
            this.grpReplenishment.Controls.Add(this.txtCurrency);
            this.grpReplenishment.Controls.Add(this.btnSearchCurrency);
            this.grpReplenishment.Controls.Add(this.lblCurrency);
            this.grpReplenishment.Controls.Add(this.lblPurchasingPrice);
            this.grpReplenishment.Controls.Add(this.lblPercentConv);
            this.grpReplenishment.Controls.Add(this.lblPercentVouc);
            this.grpReplenishment.Controls.Add(this.chkRequisition);
            this.grpReplenishment.Controls.Add(this.lblPercent);
            this.grpReplenishment.Controls.Add(this.lblStockUM4);
            this.grpReplenishment.Controls.Add(this.lblStockUM3);
            this.grpReplenishment.Controls.Add(this.lblStockUM2);
            this.grpReplenishment.Controls.Add(this.lblStockUM1);
            this.grpReplenishment.Controls.Add(this.lblBuyingUM3);
            this.grpReplenishment.Controls.Add(this.lblBuyingUM2);
            this.grpReplenishment.Controls.Add(this.lblBuyingUM1);
            this.grpReplenishment.Controls.Add(this.txtSafetyStock);
            this.grpReplenishment.Controls.Add(this.txtMaximumStock);
            this.grpReplenishment.Controls.Add(this.txtScrapPercent);
            this.grpReplenishment.Controls.Add(this.txtVoucherTolerance);
            this.grpReplenishment.Controls.Add(this.txtIssueSize);
            this.grpReplenishment.Controls.Add(this.txtMinimumStock);
            this.grpReplenishment.Controls.Add(this.txtConversionTolerance);
            this.grpReplenishment.Controls.Add(this.txtOrderPoint);
            this.grpReplenishment.Controls.Add(this.txtOrderQuantityMultiple);
            this.grpReplenishment.Controls.Add(this.txtOrderQuantity);
            this.grpReplenishment.Controls.Add(this.cboVendorLocationID);
            this.grpReplenishment.Controls.Add(this.cboBuyerID);
            this.grpReplenishment.Controls.Add(this.lblOrderPoint);
            this.grpReplenishment.Controls.Add(this.txtPrimaryVendor);
            this.grpReplenishment.Controls.Add(this.btnSearchVendor);
            this.grpReplenishment.Controls.Add(this.lblVoucherTolerance);
            this.grpReplenishment.Controls.Add(this.lblVendorLocationID);
            this.grpReplenishment.Controls.Add(this.lblOrderQuantityMultiple);
            this.grpReplenishment.Controls.Add(this.lblOrderQuantity);
            this.grpReplenishment.Controls.Add(this.lblOrderRuleID);
            this.grpReplenishment.Controls.Add(this.lblPrimaryVendor);
            this.grpReplenishment.Controls.Add(this.lblBuyerID);
            this.grpReplenishment.Controls.Add(this.lblIssueSize);
            this.grpReplenishment.Controls.Add(this.cboOrderRuleID);
            this.grpReplenishment.Controls.Add(this.lblConversionTolerance);
            this.grpReplenishment.Controls.Add(this.lblMinimumStock);
            this.grpReplenishment.Controls.Add(this.lblMaximumStock);
            this.grpReplenishment.Controls.Add(this.lblScrapPercent);
            this.grpReplenishment.Controls.Add(this.lblSafetyStock);
            this.grpReplenishment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.grpReplenishment, "grpReplenishment");
            this.grpReplenishment.Name = "grpReplenishment";
            this.grpReplenishment.TabStop = false;
            // 
            // numRoundUpToMin
            // 
            // 
            // 
            // 
            this.numRoundUpToMin.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.numRoundUpToMin.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.numRoundUpToMin.DisableOnNoData = false;
            resources.ApplyResources(this.numRoundUpToMin, "numRoundUpToMin");
            this.numRoundUpToMin.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("numRoundUpToMin.MaskInfo.ShowLiterals")));
            this.numRoundUpToMin.Name = "numRoundUpToMin";
            this.numRoundUpToMin.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.numRoundUpToMin.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("numRoundUpToMin.PostValidation.Intervals")))});
            // 
            // numRoundUpToMultiple
            // 
            // 
            // 
            // 
            this.numRoundUpToMultiple.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.numRoundUpToMultiple.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.numRoundUpToMultiple.DisableOnNoData = false;
            resources.ApplyResources(this.numRoundUpToMultiple, "numRoundUpToMultiple");
            this.numRoundUpToMultiple.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("numRoundUpToMultiple.MaskInfo.ShowLiterals")));
            this.numRoundUpToMultiple.Name = "numRoundUpToMultiple";
            this.numRoundUpToMultiple.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.numRoundUpToMultiple.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("numRoundUpToMultiple.PostValidation.Intervals")))});
            // 
            // lblRoundUpToMultiple
            // 
            resources.ApplyResources(this.lblRoundUpToMultiple, "lblRoundUpToMultiple");
            this.lblRoundUpToMultiple.Name = "lblRoundUpToMultiple";
            // 
            // lblRoundUpToMin
            // 
            resources.ApplyResources(this.lblRoundUpToMin, "lblRoundUpToMin");
            this.lblRoundUpToMin.Name = "lblRoundUpToMin";
            // 
            // numMinProduce
            // 
            // 
            // 
            // 
            this.numMinProduce.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.numMinProduce.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.numMinProduce.DisableOnNoData = false;
            resources.ApplyResources(this.numMinProduce, "numMinProduce");
            this.numMinProduce.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("numMinProduce.MaskInfo.ShowLiterals")));
            this.numMinProduce.Name = "numMinProduce";
            this.numMinProduce.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.numMinProduce.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("numMinProduce.PostValidation.Intervals")))});
            // 
            // numMaxProduce
            // 
            // 
            // 
            // 
            this.numMaxProduce.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.numMaxProduce.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.numMaxProduce.DisableOnNoData = false;
            resources.ApplyResources(this.numMaxProduce, "numMaxProduce");
            this.numMaxProduce.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("numMaxProduce.MaskInfo.ShowLiterals")));
            this.numMaxProduce.Name = "numMaxProduce";
            this.numMaxProduce.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.numMaxProduce.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("numMaxProduce.PostValidation.Intervals")))});
            // 
            // lblMaxProduce
            // 
            resources.ApplyResources(this.lblMaxProduce, "lblMaxProduce");
            this.lblMaxProduce.Name = "lblMaxProduce";
            // 
            // lblMinProduce
            // 
            resources.ApplyResources(this.lblMinProduce, "lblMinProduce");
            this.lblMinProduce.Name = "lblMinProduce";
            // 
            // numPurchasingPrice
            // 
            // 
            // 
            // 
            this.numPurchasingPrice.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.numPurchasingPrice.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.numPurchasingPrice.DisableOnNoData = false;
            resources.ApplyResources(this.numPurchasingPrice, "numPurchasingPrice");
            this.numPurchasingPrice.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("numPurchasingPrice.MaskInfo.ShowLiterals")));
            this.numPurchasingPrice.Name = "numPurchasingPrice";
            this.numPurchasingPrice.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.numPurchasingPrice.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("numPurchasingPrice.PostValidation.Intervals")))});
            this.numPurchasingPrice.Validating += new System.ComponentModel.CancelEventHandler(this.numPurchasingPrice_Validating);
            // 
            // txtVendorName
            // 
            this.txtVendorName.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtVendorName, "txtVendorName");
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.ReadOnly = true;
            // 
            // lblVendorName
            // 
            resources.ApplyResources(this.lblVendorName, "lblVendorName");
            this.lblVendorName.Name = "lblVendorName";
            // 
            // txtCurrency
            // 
            this.txtCurrency.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtCurrency, "txtCurrency");
            this.txtCurrency.Name = "txtCurrency";
            this.txtCurrency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrency_KeyDown);
            this.txtCurrency.Validating += new System.ComponentModel.CancelEventHandler(this.txtCurrency_Validating);
            // 
            // btnSearchCurrency
            // 
            resources.ApplyResources(this.btnSearchCurrency, "btnSearchCurrency");
            this.btnSearchCurrency.Name = "btnSearchCurrency";
            this.btnSearchCurrency.Click += new System.EventHandler(this.btnSearchCurrency_Click);
            // 
            // lblCurrency
            // 
            resources.ApplyResources(this.lblCurrency, "lblCurrency");
            this.lblCurrency.Name = "lblCurrency";
            // 
            // lblPurchasingPrice
            // 
            resources.ApplyResources(this.lblPurchasingPrice, "lblPurchasingPrice");
            this.lblPurchasingPrice.Name = "lblPurchasingPrice";
            // 
            // lblPercentConv
            // 
            resources.ApplyResources(this.lblPercentConv, "lblPercentConv");
            this.lblPercentConv.Name = "lblPercentConv";
            // 
            // lblPercentVouc
            // 
            resources.ApplyResources(this.lblPercentVouc, "lblPercentVouc");
            this.lblPercentVouc.Name = "lblPercentVouc";
            // 
            // chkRequisition
            // 
            resources.ApplyResources(this.chkRequisition, "chkRequisition");
            this.chkRequisition.Name = "chkRequisition";
            // 
            // lblPercent
            // 
            resources.ApplyResources(this.lblPercent, "lblPercent");
            this.lblPercent.Name = "lblPercent";
            // 
            // lblStockUM4
            // 
            resources.ApplyResources(this.lblStockUM4, "lblStockUM4");
            this.lblStockUM4.Name = "lblStockUM4";
            // 
            // lblStockUM3
            // 
            resources.ApplyResources(this.lblStockUM3, "lblStockUM3");
            this.lblStockUM3.Name = "lblStockUM3";
            // 
            // lblStockUM2
            // 
            resources.ApplyResources(this.lblStockUM2, "lblStockUM2");
            this.lblStockUM2.Name = "lblStockUM2";
            // 
            // lblStockUM1
            // 
            resources.ApplyResources(this.lblStockUM1, "lblStockUM1");
            this.lblStockUM1.Name = "lblStockUM1";
            // 
            // lblBuyingUM3
            // 
            resources.ApplyResources(this.lblBuyingUM3, "lblBuyingUM3");
            this.lblBuyingUM3.Name = "lblBuyingUM3";
            // 
            // lblBuyingUM2
            // 
            resources.ApplyResources(this.lblBuyingUM2, "lblBuyingUM2");
            this.lblBuyingUM2.Name = "lblBuyingUM2";
            // 
            // lblBuyingUM1
            // 
            resources.ApplyResources(this.lblBuyingUM1, "lblBuyingUM1");
            this.lblBuyingUM1.Name = "lblBuyingUM1";
            // 
            // txtSafetyStock
            // 
            // 
            // 
            // 
            this.txtSafetyStock.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtSafetyStock.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtSafetyStock.DisableOnNoData = false;
            resources.ApplyResources(this.txtSafetyStock, "txtSafetyStock");
            this.txtSafetyStock.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtSafetyStock.MaskInfo.ShowLiterals")));
            this.txtSafetyStock.Name = "txtSafetyStock";
            this.txtSafetyStock.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtSafetyStock.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtSafetyStock.PostValidation.Intervals")))});
            this.txtSafetyStock.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtSafetyStock.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtMaximumStock
            // 
            // 
            // 
            // 
            this.txtMaximumStock.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtMaximumStock.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtMaximumStock.DisableOnNoData = false;
            resources.ApplyResources(this.txtMaximumStock, "txtMaximumStock");
            this.txtMaximumStock.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtMaximumStock.MaskInfo.ShowLiterals")));
            this.txtMaximumStock.Name = "txtMaximumStock";
            this.txtMaximumStock.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtMaximumStock.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtMaximumStock.PostValidation.Intervals")))});
            this.txtMaximumStock.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtMaximumStock.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtScrapPercent
            // 
            // 
            // 
            // 
            this.txtScrapPercent.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtScrapPercent.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtScrapPercent.DisableOnNoData = false;
            resources.ApplyResources(this.txtScrapPercent, "txtScrapPercent");
            this.txtScrapPercent.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtScrapPercent.MaskInfo.ShowLiterals")));
            this.txtScrapPercent.Name = "txtScrapPercent";
            this.txtScrapPercent.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtScrapPercent.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtScrapPercent.PostValidation.Intervals")))});
            this.txtScrapPercent.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtScrapPercent.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtVoucherTolerance
            // 
            // 
            // 
            // 
            this.txtVoucherTolerance.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtVoucherTolerance.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtVoucherTolerance.DisableOnNoData = false;
            resources.ApplyResources(this.txtVoucherTolerance, "txtVoucherTolerance");
            this.txtVoucherTolerance.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtVoucherTolerance.MaskInfo.ShowLiterals")));
            this.txtVoucherTolerance.Name = "txtVoucherTolerance";
            this.txtVoucherTolerance.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtVoucherTolerance.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtVoucherTolerance.PostValidation.Intervals")))});
            this.txtVoucherTolerance.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtVoucherTolerance.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtIssueSize
            // 
            // 
            // 
            // 
            this.txtIssueSize.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtIssueSize.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtIssueSize.DisableOnNoData = false;
            resources.ApplyResources(this.txtIssueSize, "txtIssueSize");
            this.txtIssueSize.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtIssueSize.MaskInfo.ShowLiterals")));
            this.txtIssueSize.Name = "txtIssueSize";
            this.txtIssueSize.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtIssueSize.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtIssueSize.PostValidation.Intervals")))});
            this.txtIssueSize.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtIssueSize.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtMinimumStock
            // 
            // 
            // 
            // 
            this.txtMinimumStock.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtMinimumStock.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtMinimumStock.DisableOnNoData = false;
            resources.ApplyResources(this.txtMinimumStock, "txtMinimumStock");
            this.txtMinimumStock.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtMinimumStock.MaskInfo.ShowLiterals")));
            this.txtMinimumStock.Name = "txtMinimumStock";
            this.txtMinimumStock.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtMinimumStock.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtMinimumStock.PostValidation.Intervals")))});
            this.txtMinimumStock.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtMinimumStock.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtConversionTolerance
            // 
            // 
            // 
            // 
            this.txtConversionTolerance.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtConversionTolerance.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtConversionTolerance.DisableOnNoData = false;
            resources.ApplyResources(this.txtConversionTolerance, "txtConversionTolerance");
            this.txtConversionTolerance.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtConversionTolerance.MaskInfo.ShowLiterals")));
            this.txtConversionTolerance.Name = "txtConversionTolerance";
            this.txtConversionTolerance.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtConversionTolerance.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtConversionTolerance.PostValidation.Intervals")))});
            this.txtConversionTolerance.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtConversionTolerance.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtOrderPoint
            // 
            // 
            // 
            // 
            this.txtOrderPoint.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtOrderPoint.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtOrderPoint.DisableOnNoData = false;
            resources.ApplyResources(this.txtOrderPoint, "txtOrderPoint");
            this.txtOrderPoint.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtOrderPoint.MaskInfo.ShowLiterals")));
            this.txtOrderPoint.Name = "txtOrderPoint";
            this.txtOrderPoint.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtOrderPoint.PostValidation.Intervals")))});
            this.txtOrderPoint.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtOrderPoint.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtOrderQuantityMultiple
            // 
            // 
            // 
            // 
            this.txtOrderQuantityMultiple.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtOrderQuantityMultiple.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtOrderQuantityMultiple.DisableOnNoData = false;
            resources.ApplyResources(this.txtOrderQuantityMultiple, "txtOrderQuantityMultiple");
            this.txtOrderQuantityMultiple.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtOrderQuantityMultiple.MaskInfo.ShowLiterals")));
            this.txtOrderQuantityMultiple.Name = "txtOrderQuantityMultiple";
            this.txtOrderQuantityMultiple.NumericInputKeys = C1.Win.C1Input.NumericInputKeyFlags.None;
            this.txtOrderQuantityMultiple.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtOrderQuantityMultiple.PostValidation.Intervals")))});
            this.txtOrderQuantityMultiple.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtOrderQuantityMultiple.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtOrderQuantity
            // 
            // 
            // 
            // 
            this.txtOrderQuantity.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtOrderQuantity.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtOrderQuantity.DisableOnNoData = false;
            resources.ApplyResources(this.txtOrderQuantity, "txtOrderQuantity");
            this.txtOrderQuantity.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("txtOrderQuantity.MaskInfo.ShowLiterals")));
            this.txtOrderQuantity.Name = "txtOrderQuantity";
            this.txtOrderQuantity.NumericInputKeys = C1.Win.C1Input.NumericInputKeyFlags.None;
            this.txtOrderQuantity.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtOrderQuantity.PostValidation.Intervals")))});
            this.txtOrderQuantity.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtOrderQuantity.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // cboVendorLocationID
            // 
            this.cboVendorLocationID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboVendorLocationID, "cboVendorLocationID");
            this.cboVendorLocationID.CaptionHeight = 17;
            this.cboVendorLocationID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboVendorLocationID.ColumnCaptionHeight = 17;
            this.cboVendorLocationID.ColumnFooterHeight = 17;
            this.cboVendorLocationID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboVendorLocationID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboVendorLocationID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboVendorLocationID.DropDownWidth = 200;
            this.cboVendorLocationID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboVendorLocationID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVendorLocationID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboVendorLocationID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboVendorLocationID.Images"))));
            this.cboVendorLocationID.ItemHeight = 15;
            this.cboVendorLocationID.MatchEntryTimeout = ((long)(2000));
            this.cboVendorLocationID.MaxDropDownItems = ((short)(5));
            this.cboVendorLocationID.MaxLength = 32767;
            this.cboVendorLocationID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboVendorLocationID.Name = "cboVendorLocationID";
            this.cboVendorLocationID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboVendorLocationID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboVendorLocationID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboVendorLocationID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboVendorLocationID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboVendorLocationID.PropBag = resources.GetString("cboVendorLocationID.PropBag");
            // 
            // cboBuyerID
            // 
            this.cboBuyerID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboBuyerID, "cboBuyerID");
            this.cboBuyerID.CaptionHeight = 17;
            this.cboBuyerID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboBuyerID.ColumnCaptionHeight = 17;
            this.cboBuyerID.ColumnFooterHeight = 17;
            this.cboBuyerID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboBuyerID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboBuyerID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboBuyerID.DropDownWidth = 200;
            this.cboBuyerID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboBuyerID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBuyerID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboBuyerID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboBuyerID.Images"))));
            this.cboBuyerID.ItemHeight = 15;
            this.cboBuyerID.MatchEntryTimeout = ((long)(2000));
            this.cboBuyerID.MaxDropDownItems = ((short)(5));
            this.cboBuyerID.MaxLength = 32767;
            this.cboBuyerID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboBuyerID.Name = "cboBuyerID";
            this.cboBuyerID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboBuyerID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboBuyerID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboBuyerID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboBuyerID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboBuyerID.PropBag = resources.GetString("cboBuyerID.PropBag");
            // 
            // lblOrderPoint
            // 
            resources.ApplyResources(this.lblOrderPoint, "lblOrderPoint");
            this.lblOrderPoint.Name = "lblOrderPoint";
            // 
            // txtPrimaryVendor
            // 
            this.txtPrimaryVendor.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtPrimaryVendor, "txtPrimaryVendor");
            this.txtPrimaryVendor.Name = "txtPrimaryVendor";
            this.txtPrimaryVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrimaryVendor_KeyDown);
            this.txtPrimaryVendor.Validating += new System.ComponentModel.CancelEventHandler(this.txtPrimaryVendor_Validating);
            // 
            // btnSearchVendor
            // 
            resources.ApplyResources(this.btnSearchVendor, "btnSearchVendor");
            this.btnSearchVendor.Name = "btnSearchVendor";
            this.btnSearchVendor.Click += new System.EventHandler(this.btnSearchVendor_Click);
            // 
            // lblVoucherTolerance
            // 
            resources.ApplyResources(this.lblVoucherTolerance, "lblVoucherTolerance");
            this.lblVoucherTolerance.Name = "lblVoucherTolerance";
            // 
            // lblVendorLocationID
            // 
            resources.ApplyResources(this.lblVendorLocationID, "lblVendorLocationID");
            this.lblVendorLocationID.Name = "lblVendorLocationID";
            // 
            // lblOrderQuantityMultiple
            // 
            resources.ApplyResources(this.lblOrderQuantityMultiple, "lblOrderQuantityMultiple");
            this.lblOrderQuantityMultiple.Name = "lblOrderQuantityMultiple";
            // 
            // lblOrderQuantity
            // 
            resources.ApplyResources(this.lblOrderQuantity, "lblOrderQuantity");
            this.lblOrderQuantity.Name = "lblOrderQuantity";
            // 
            // lblOrderRuleID
            // 
            resources.ApplyResources(this.lblOrderRuleID, "lblOrderRuleID");
            this.lblOrderRuleID.Name = "lblOrderRuleID";
            // 
            // lblPrimaryVendor
            // 
            resources.ApplyResources(this.lblPrimaryVendor, "lblPrimaryVendor");
            this.lblPrimaryVendor.Name = "lblPrimaryVendor";
            // 
            // lblBuyerID
            // 
            resources.ApplyResources(this.lblBuyerID, "lblBuyerID");
            this.lblBuyerID.Name = "lblBuyerID";
            // 
            // lblIssueSize
            // 
            resources.ApplyResources(this.lblIssueSize, "lblIssueSize");
            this.lblIssueSize.Name = "lblIssueSize";
            // 
            // cboOrderRuleID
            // 
            this.cboOrderRuleID.AddItemSeparator = ';';
            resources.ApplyResources(this.cboOrderRuleID, "cboOrderRuleID");
            this.cboOrderRuleID.CaptionHeight = 17;
            this.cboOrderRuleID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboOrderRuleID.ColumnCaptionHeight = 17;
            this.cboOrderRuleID.ColumnFooterHeight = 17;
            this.cboOrderRuleID.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboOrderRuleID.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboOrderRuleID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboOrderRuleID.DropDownWidth = 200;
            this.cboOrderRuleID.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboOrderRuleID.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOrderRuleID.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboOrderRuleID.Images.Add(((System.Drawing.Image)(resources.GetObject("cboOrderRuleID.Images"))));
            this.cboOrderRuleID.ItemHeight = 15;
            this.cboOrderRuleID.MatchEntryTimeout = ((long)(2000));
            this.cboOrderRuleID.MaxDropDownItems = ((short)(5));
            this.cboOrderRuleID.MaxLength = 32767;
            this.cboOrderRuleID.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboOrderRuleID.Name = "cboOrderRuleID";
            this.cboOrderRuleID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboOrderRuleID.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboOrderRuleID.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboOrderRuleID.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboOrderRuleID.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboOrderRuleID.PropBag = resources.GetString("cboOrderRuleID.PropBag");
            // 
            // lblConversionTolerance
            // 
            resources.ApplyResources(this.lblConversionTolerance, "lblConversionTolerance");
            this.lblConversionTolerance.Name = "lblConversionTolerance";
            // 
            // lblMinimumStock
            // 
            resources.ApplyResources(this.lblMinimumStock, "lblMinimumStock");
            this.lblMinimumStock.Name = "lblMinimumStock";
            // 
            // lblMaximumStock
            // 
            resources.ApplyResources(this.lblMaximumStock, "lblMaximumStock");
            this.lblMaximumStock.Name = "lblMaximumStock";
            // 
            // lblScrapPercent
            // 
            resources.ApplyResources(this.lblScrapPercent, "lblScrapPercent");
            this.lblScrapPercent.Name = "lblScrapPercent";
            // 
            // lblSafetyStock
            // 
            resources.ApplyResources(this.lblSafetyStock, "lblSafetyStock");
            this.lblSafetyStock.Name = "lblSafetyStock";
            // 
            // lblShipToleranceID
            // 
            resources.ApplyResources(this.lblShipToleranceID, "lblShipToleranceID");
            this.lblShipToleranceID.Name = "lblShipToleranceID";
            // 
            // lblOrderPolicyID
            // 
            resources.ApplyResources(this.lblOrderPolicyID, "lblOrderPolicyID");
            this.lblOrderPolicyID.Name = "lblOrderPolicyID";
            // 
            // lblDeliveryPolicyID
            // 
            resources.ApplyResources(this.lblDeliveryPolicyID, "lblDeliveryPolicyID");
            this.lblDeliveryPolicyID.Name = "lblDeliveryPolicyID";
            // 
            // chkAutoConversion
            // 
            resources.ApplyResources(this.chkAutoConversion, "chkAutoConversion");
            this.chkAutoConversion.Name = "chkAutoConversion";
            // 
            // grpPlantype
            // 
            this.grpPlantype.Controls.Add(this.radPlanTypeMPS);
            this.grpPlantype.Controls.Add(this.radPlanTypeMRP);
            resources.ApplyResources(this.grpPlantype, "grpPlantype");
            this.grpPlantype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpPlantype.Name = "grpPlantype";
            this.grpPlantype.TabStop = false;
            // 
            // radPlanTypeMPS
            // 
            resources.ApplyResources(this.radPlanTypeMPS, "radPlanTypeMPS");
            this.radPlanTypeMPS.Name = "radPlanTypeMPS";
            // 
            // radPlanTypeMRP
            // 
            resources.ApplyResources(this.radPlanTypeMRP, "radPlanTypeMRP");
            this.radPlanTypeMRP.Name = "radPlanTypeMRP";
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
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCost
            // 
            resources.ApplyResources(this.btnCost, "btnCost");
            this.btnCost.Name = "btnCost";
            this.btnCost.Click += new System.EventHandler(this.btnCost_Click);
            // 
            // btnBOM
            // 
            this.btnBOM.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnBOM, "btnBOM");
            this.btnBOM.Name = "btnBOM";
            this.btnBOM.Click += new System.EventHandler(this.btnBOM_Click);
            // 
            // btnRouting
            // 
            resources.ApplyResources(this.btnRouting, "btnRouting");
            this.btnRouting.Name = "btnRouting";
            this.btnRouting.Click += new System.EventHandler(this.btnRouting_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // txtRevision
            // 
            this.txtRevision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtRevision, "txtRevision");
            this.txtRevision.Name = "txtRevision";
            this.txtRevision.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtRevision.Leave += new System.EventHandler(this.OnLeaveControl);
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
            this.cboCCN.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown;
            this.cboCCN.DropDownWidth = 200;
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
            this.cboCCN.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboCCN.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboCCN.PropBag = resources.GetString("cboCCN.PropBag");
            // 
            // lblCCN
            // 
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblCCN, "lblCCN");
            this.lblCCN.Name = "lblCCN";
            // 
            // txtDescription
            // 
            this.txtDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            this.txtDescription.Leave += new System.EventHandler(this.txtDescription_Leave);
            // 
            // lblDescription
            // 
            this.lblDescription.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // lblRevision
            // 
            this.lblRevision.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblRevision, "lblRevision");
            this.lblRevision.Name = "lblRevision";
            // 
            // btnSearchProductCode
            // 
            resources.ApplyResources(this.btnSearchProductCode, "btnSearchProductCode");
            this.btnSearchProductCode.Name = "btnSearchProductCode";
            this.btnSearchProductCode.Click += new System.EventHandler(this.btnSearchProductCode_Click);
            // 
            // txtCode
            // 
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            this.txtCode.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
            // 
            // lblCode
            // 
            this.lblCode.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblCode, "lblCode");
            this.lblCode.Name = "lblCode";
            // 
            // btnSearchProductDescription
            // 
            resources.ApplyResources(this.btnSearchProductDescription, "btnSearchProductDescription");
            this.btnSearchProductDescription.Name = "btnSearchProductDescription";
            this.btnSearchProductDescription.Click += new System.EventHandler(this.btnSearchProductDescription_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // tabProductInfo
            // 
            this.tabProductInfo.Controls.Add(this.tabPage1);
            this.tabProductInfo.Controls.Add(this.tabPage2);
            this.tabProductInfo.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabProductInfo, "tabProductInfo");
            this.tabProductInfo.Name = "tabProductInfo";
            this.tabProductInfo.TabStyle = C1.Win.C1Command.TabStyleEnum.WindowsXP;
            this.tabProductInfo.VisualStyle = C1.Win.C1Command.VisualStyle.System;
            this.tabProductInfo.VisualStyleBase = C1.Win.C1Command.VisualStyle.System;
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.btnViewImage);
            this.tabPage1.Controls.Add(this.chkAllowNegativeQty);
            this.tabPage1.Controls.Add(this.txtSetUpPair);
            this.tabPage1.Controls.Add(this.lblSetUpPair);
            this.tabPage1.Controls.Add(this.txtRegisteredCode);
            this.tabPage1.Controls.Add(this.lblRegisteredCode);
            this.tabPage1.Controls.Add(this.btnClearPicture);
            this.tabPage1.Controls.Add(this.btnChangePicture);
            this.tabPage1.Controls.Add(this.picCategory);
            this.tabPage1.Controls.Add(this.lblPicture);
            this.tabPage1.Controls.Add(this.cboCostMethod);
            this.tabPage1.Controls.Add(this.numQuantitySet);
            this.tabPage1.Controls.Add(this.lblQuantitySet);
            this.tabPage1.Controls.Add(this.txtTaxCode);
            this.tabPage1.Controls.Add(this.txtShelfLife);
            this.tabPage1.Controls.Add(this.lblTaxCode);
            this.tabPage1.Controls.Add(this.lblProductType);
            this.tabPage1.Controls.Add(this.cboProductType);
            this.tabPage1.Controls.Add(this.numLicenseFee);
            this.tabPage1.Controls.Add(this.lblLicenseFee);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lblCostMethod);
            this.tabPage1.Controls.Add(this.cboBuyingUMID);
            this.tabPage1.Controls.Add(this.cboLocationID);
            this.tabPage1.Controls.Add(this.cboBinID);
            this.tabPage1.Controls.Add(this.cboQAStatus);
            this.tabPage1.Controls.Add(this.cboMasterLocationID);
            this.tabPage1.Controls.Add(this.cboStockUMID);
            this.tabPage1.Controls.Add(this.cboSellingUMID);
            this.tabPage1.Controls.Add(this.lblFreightClassID);
            this.tabPage1.Controls.Add(this.lblPartNumber);
            this.tabPage1.Controls.Add(this.chkStock);
            this.tabPage1.Controls.Add(this.chkLotControl);
            this.tabPage1.Controls.Add(this.txtPartNumber);
            this.tabPage1.Controls.Add(this.txtPrimaryVendorID);
            this.tabPage1.Controls.Add(this.txtLotSize);
            this.tabPage1.Controls.Add(this.lblMasterLocationID);
            this.tabPage1.Controls.Add(this.lblSellingUMID);
            this.tabPage1.Controls.Add(this.dtmSetupDate);
            this.tabPage1.Controls.Add(this.txtOtherInfo2);
            this.tabPage1.Controls.Add(this.cboFormatCodeID);
            this.tabPage1.Controls.Add(this.lblFormatCodeID);
            this.tabPage1.Controls.Add(this.lblLotSize);
            this.tabPage1.Controls.Add(this.lblClassified);
            this.tabPage1.Controls.Add(this.lblReasonID);
            this.tabPage1.Controls.Add(this.cboClassified);
            this.tabPage1.Controls.Add(this.cboReasonID);
            this.tabPage1.Controls.Add(this.lblSourceID);
            this.tabPage1.Controls.Add(this.lblStockUMID);
            this.tabPage1.Controls.Add(this.lblCategoryID);
            this.tabPage1.Controls.Add(this.cboItemGroup);
            this.tabPage1.Controls.Add(this.cboHazardID);
            this.tabPage1.Controls.Add(this.chkMakeItem);
            this.tabPage1.Controls.Add(this.cboAccountCode);
            this.tabPage1.Controls.Add(this.lblLocationID);
            this.tabPage1.Controls.Add(this.lblOtherInfo2);
            this.tabPage1.Controls.Add(this.lblSetupDate);
            this.tabPage1.Controls.Add(this.lblAccountCode);
            this.tabPage1.Controls.Add(this.cboSourceID);
            this.tabPage1.Controls.Add(this.cboCategoryID);
            this.tabPage1.Controls.Add(this.txtOtherInfo1);
            this.tabPage1.Controls.Add(this.lblItemGroup);
            this.tabPage1.Controls.Add(this.lblHazardID);
            this.tabPage1.Controls.Add(this.lblQAStatus);
            this.tabPage1.Controls.Add(this.lblOtherInfo1);
            this.tabPage1.Controls.Add(this.lblShelfLife);
            this.tabPage1.Controls.Add(this.cboFreightClassID);
            this.tabPage1.Controls.Add(this.lblBinID);
            this.tabPage1.Controls.Add(this.lblBuyingUMID);
            this.tabPage1.Controls.Add(this.grpWeightAndSize);
            this.tabPage1.Controls.Add(this.txtInventor);
            this.tabPage1.Controls.Add(this.btnInventor);
            this.tabPage1.Controls.Add(this.lblInventor);
            this.tabPage1.Name = "tabPage1";
            // 
            // btnViewImage
            // 
            resources.ApplyResources(this.btnViewImage, "btnViewImage");
            this.btnViewImage.Name = "btnViewImage";
            this.btnViewImage.Click += new System.EventHandler(this.btnViewImage_Click);
            // 
            // chkAllowNegativeQty
            // 
            resources.ApplyResources(this.chkAllowNegativeQty, "chkAllowNegativeQty");
            this.chkAllowNegativeQty.Name = "chkAllowNegativeQty";
            // 
            // txtSetUpPair
            // 
            resources.ApplyResources(this.txtSetUpPair, "txtSetUpPair");
            this.txtSetUpPair.Name = "txtSetUpPair";
            // 
            // lblSetUpPair
            // 
            resources.ApplyResources(this.lblSetUpPair, "lblSetUpPair");
            this.lblSetUpPair.Name = "lblSetUpPair";
            // 
            // txtRegisteredCode
            // 
            resources.ApplyResources(this.txtRegisteredCode, "txtRegisteredCode");
            this.txtRegisteredCode.Name = "txtRegisteredCode";
            // 
            // lblRegisteredCode
            // 
            resources.ApplyResources(this.lblRegisteredCode, "lblRegisteredCode");
            this.lblRegisteredCode.Name = "lblRegisteredCode";
            // 
            // btnClearPicture
            // 
            resources.ApplyResources(this.btnClearPicture, "btnClearPicture");
            this.btnClearPicture.Name = "btnClearPicture";
            this.btnClearPicture.Click += new System.EventHandler(this.btnClearPicture_Click);
            // 
            // btnChangePicture
            // 
            resources.ApplyResources(this.btnChangePicture, "btnChangePicture");
            this.btnChangePicture.Name = "btnChangePicture";
            this.btnChangePicture.Click += new System.EventHandler(this.btnChangePicture_Click);
            // 
            // picCategory
            // 
            this.picCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.picCategory, "picCategory");
            this.picCategory.Name = "picCategory";
            this.picCategory.TabStop = false;
            // 
            // lblPicture
            // 
            resources.ApplyResources(this.lblPicture, "lblPicture");
            this.lblPicture.Name = "lblPicture";
            // 
            // cboCostMethod
            // 
            this.cboCostMethod.AddItemSeparator = ';';
            resources.ApplyResources(this.cboCostMethod, "cboCostMethod");
            this.cboCostMethod.CaptionHeight = 17;
            this.cboCostMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCostMethod.ColumnCaptionHeight = 17;
            this.cboCostMethod.ColumnFooterHeight = 17;
            this.cboCostMethod.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCostMethod.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCostMethod.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.cboCostMethod.DropDownWidth = 200;
            this.cboCostMethod.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCostMethod.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCostMethod.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCostMethod.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCostMethod.Images"))));
            this.cboCostMethod.ItemHeight = 15;
            this.cboCostMethod.MatchEntryTimeout = ((long)(2000));
            this.cboCostMethod.MaxDropDownItems = ((short)(5));
            this.cboCostMethod.MaxLength = 32767;
            this.cboCostMethod.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCostMethod.Name = "cboCostMethod";
            this.cboCostMethod.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboCostMethod.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCostMethod.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboCostMethod.PropBag = resources.GetString("cboCostMethod.PropBag");
            // 
            // numQuantitySet
            // 
            // 
            // 
            // 
            this.numQuantitySet.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.numQuantitySet.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            resources.ApplyResources(this.numQuantitySet, "numQuantitySet");
            this.numQuantitySet.DisableOnNoData = false;
            this.numQuantitySet.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("numQuantitySet.MaskInfo.ShowLiterals")));
            this.numQuantitySet.Name = "numQuantitySet";
            this.numQuantitySet.NumericInputKeys = C1.Win.C1Input.NumericInputKeyFlags.None;
            // 
            // lblQuantitySet
            // 
            resources.ApplyResources(this.lblQuantitySet, "lblQuantitySet");
            this.lblQuantitySet.Name = "lblQuantitySet";
            // 
            // txtTaxCode
            // 
            resources.ApplyResources(this.txtTaxCode, "txtTaxCode");
            this.txtTaxCode.Name = "txtTaxCode";
            this.txtTaxCode.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtTaxCode.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblTaxCode
            // 
            resources.ApplyResources(this.lblTaxCode, "lblTaxCode");
            this.lblTaxCode.Name = "lblTaxCode";
            // 
            // lblProductType
            // 
            resources.ApplyResources(this.lblProductType, "lblProductType");
            this.lblProductType.Name = "lblProductType";
            // 
            // cboProductType
            // 
            this.cboProductType.AddItemSeparator = ';';
            resources.ApplyResources(this.cboProductType, "cboProductType");
            this.cboProductType.CaptionHeight = 17;
            this.cboProductType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboProductType.ColumnCaptionHeight = 17;
            this.cboProductType.ColumnFooterHeight = 17;
            this.cboProductType.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboProductType.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboProductType.DropDownWidth = 200;
            this.cboProductType.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboProductType.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProductType.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboProductType.Images.Add(((System.Drawing.Image)(resources.GetObject("cboProductType.Images"))));
            this.cboProductType.ItemHeight = 15;
            this.cboProductType.MatchEntryTimeout = ((long)(2000));
            this.cboProductType.MaxDropDownItems = ((short)(5));
            this.cboProductType.MaxLength = 32767;
            this.cboProductType.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboProductType.Name = "cboProductType";
            this.cboProductType.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboProductType.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboProductType.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboProductType.PropBag = resources.GetString("cboProductType.PropBag");
            // 
            // numLicenseFee
            // 
            // 
            // 
            // 
            this.numLicenseFee.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.numLicenseFee.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            resources.ApplyResources(this.numLicenseFee, "numLicenseFee");
            this.numLicenseFee.DisableOnNoData = false;
            this.numLicenseFee.MaskInfo.ShowLiterals = ((C1.Win.C1Input.ShowLiteralsEnum)(resources.GetObject("numLicenseFee.MaskInfo.ShowLiterals")));
            this.numLicenseFee.Name = "numLicenseFee";
            this.numLicenseFee.NumericInputKeys = C1.Win.C1Input.NumericInputKeyFlags.None;
            this.numLicenseFee.Enter += new System.EventHandler(this.OnEnterControl);
            this.numLicenseFee.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblLicenseFee
            // 
            resources.ApplyResources(this.lblLicenseFee, "lblLicenseFee");
            this.lblLicenseFee.Name = "lblLicenseFee";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblCostMethod
            // 
            this.lblCostMethod.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblCostMethod, "lblCostMethod");
            this.lblCostMethod.Name = "lblCostMethod";
            // 
            // txtInventor
            // 
            this.txtInventor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtInventor, "txtInventor");
            this.txtInventor.Name = "txtInventor";
            this.txtInventor.Tag = "0";
            this.txtInventor.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtInventor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInventor_KeyDown);
            this.txtInventor.Validating += new System.ComponentModel.CancelEventHandler(this.txtInventor_Validating);
            // 
            // btnInventor
            // 
            resources.ApplyResources(this.btnInventor, "btnInventor");
            this.btnInventor.Name = "btnInventor";
            this.btnInventor.Click += new System.EventHandler(this.btnInventor_Click);
            // 
            // lblInventor
            // 
            this.lblInventor.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblInventor, "lblInventor");
            this.lblInventor.Name = "lblInventor";
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.chkMassOrder);
            this.tabPage2.Controls.Add(this.chkAVEG);
            this.tabPage2.Controls.Add(this.grpPlantype);
            this.tabPage2.Controls.Add(this.chkAutoConversion);
            this.tabPage2.Controls.Add(this.grpLeadTime);
            this.tabPage2.Controls.Add(this.cboOrderPolicyID);
            this.tabPage2.Controls.Add(this.cboDeliveryPolicyID);
            this.tabPage2.Controls.Add(this.grpReplenishment);
            this.tabPage2.Controls.Add(this.cboShipToleranceID);
            this.tabPage2.Controls.Add(this.lblDeliveryPolicyID);
            this.tabPage2.Controls.Add(this.lblOrderPolicyID);
            this.tabPage2.Controls.Add(this.lblShipToleranceID);
            this.tabPage2.Name = "tabPage2";
            // 
            // chkMassOrder
            // 
            resources.ApplyResources(this.chkMassOrder, "chkMassOrder");
            this.chkMassOrder.Name = "chkMassOrder";
            // 
            // chkAVEG
            // 
            resources.ApplyResources(this.chkAVEG, "chkAVEG");
            this.chkAVEG.Name = "chkAVEG";
            // 
            // tabPage3
            // 
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Controls.Add(this.txtACAdjustment);
            this.tabPage3.Controls.Add(this.btnACAdjustment);
            this.tabPage3.Controls.Add(this.lblACAdjustment);
            this.tabPage3.Controls.Add(this.txtVAT);
            this.tabPage3.Controls.Add(this.txtImportTax);
            this.tabPage3.Controls.Add(this.txtExportTax);
            this.tabPage3.Controls.Add(this.txtSpecialTax);
            this.tabPage3.Controls.Add(this.lblExportTax);
            this.tabPage3.Controls.Add(this.lblVAT);
            this.tabPage3.Controls.Add(this.lblSpecialTax);
            this.tabPage3.Controls.Add(this.lblImportTax);
            this.tabPage3.Name = "tabPage3";
            // 
            // txtACAdjustment
            // 
            this.txtACAdjustment.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtACAdjustment, "txtACAdjustment");
            this.txtACAdjustment.Name = "txtACAdjustment";
            this.txtACAdjustment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtACAdjustment_KeyDown);
            this.txtACAdjustment.Validating += new System.ComponentModel.CancelEventHandler(this.txtACAdjustment_Validating);
            // 
            // btnACAdjustment
            // 
            resources.ApplyResources(this.btnACAdjustment, "btnACAdjustment");
            this.btnACAdjustment.Name = "btnACAdjustment";
            this.btnACAdjustment.Click += new System.EventHandler(this.btnACAdjustment_Click);
            // 
            // lblACAdjustment
            // 
            resources.ApplyResources(this.lblACAdjustment, "lblACAdjustment");
            this.lblACAdjustment.Name = "lblACAdjustment";
            // 
            // txtVAT
            // 
            // 
            // 
            // 
            this.txtVAT.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtVAT.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtVAT.DisableOnNoData = false;
            resources.ApplyResources(this.txtVAT, "txtVAT");
            this.txtVAT.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtVAT.ErrorInfo.BeepOnError")));
            this.txtVAT.ErrorInfo.ErrorMessage = resources.GetString("txtVAT.ErrorInfo.ErrorMessage");
            this.txtVAT.ErrorInfo.ErrorMessageCaption = resources.GetString("txtVAT.ErrorInfo.ErrorMessageCaption");
            this.txtVAT.Name = "txtVAT";
            this.txtVAT.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtVAT.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtVAT.PostValidation.Intervals")))});
            this.txtVAT.TextChanged += new System.EventHandler(this.txtVAT_TextChanged);
            this.txtVAT.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtVAT.Leave += new System.EventHandler(this.OnLeaveControl);
            this.txtVAT.Validated += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtImportTax
            // 
            // 
            // 
            // 
            this.txtImportTax.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtImportTax.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtImportTax.DisableOnNoData = false;
            resources.ApplyResources(this.txtImportTax, "txtImportTax");
            this.txtImportTax.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtImportTax.ErrorInfo.BeepOnError")));
            this.txtImportTax.ErrorInfo.ErrorMessage = resources.GetString("txtImportTax.ErrorInfo.ErrorMessage");
            this.txtImportTax.ErrorInfo.ErrorMessageCaption = resources.GetString("txtImportTax.ErrorInfo.ErrorMessageCaption");
            this.txtImportTax.Name = "txtImportTax";
            this.txtImportTax.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtImportTax.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtImportTax.PostValidation.Intervals")))});
            this.txtImportTax.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtImportTax.Leave += new System.EventHandler(this.OnLeaveControl);
            this.txtImportTax.Validated += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtExportTax
            // 
            // 
            // 
            // 
            this.txtExportTax.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtExportTax.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtExportTax.DisableOnNoData = false;
            resources.ApplyResources(this.txtExportTax, "txtExportTax");
            this.txtExportTax.ErrorInfo.ErrorMessage = resources.GetString("txtExportTax.ErrorInfo.ErrorMessage");
            this.txtExportTax.ErrorInfo.ErrorMessageCaption = resources.GetString("txtExportTax.ErrorInfo.ErrorMessageCaption");
            this.txtExportTax.Name = "txtExportTax";
            this.txtExportTax.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtExportTax.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtExportTax.PostValidation.Intervals")))});
            this.txtExportTax.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtExportTax.Leave += new System.EventHandler(this.OnLeaveControl);
            this.txtExportTax.Validated += new System.EventHandler(this.OnLeaveControl);
            // 
            // txtSpecialTax
            // 
            // 
            // 
            // 
            this.txtSpecialTax.Calculator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtSpecialTax.Calculator.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtSpecialTax.DisableOnNoData = false;
            resources.ApplyResources(this.txtSpecialTax, "txtSpecialTax");
            this.txtSpecialTax.ErrorInfo.BeepOnError = ((bool)(resources.GetObject("txtSpecialTax.ErrorInfo.BeepOnError")));
            this.txtSpecialTax.ErrorInfo.ErrorMessage = resources.GetString("txtSpecialTax.ErrorInfo.ErrorMessage");
            this.txtSpecialTax.ErrorInfo.ErrorMessageCaption = resources.GetString("txtSpecialTax.ErrorInfo.ErrorMessageCaption");
            this.txtSpecialTax.Name = "txtSpecialTax";
            this.txtSpecialTax.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Decimal)
            | C1.Win.C1Input.NumericInputKeyFlags.X)));
            this.txtSpecialTax.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
            ((C1.Win.C1Input.ValueInterval)(resources.GetObject("txtSpecialTax.PostValidation.Intervals")))});
            this.txtSpecialTax.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtSpecialTax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSpecialTax_KeyDown);
            this.txtSpecialTax.Leave += new System.EventHandler(this.OnLeaveControl);
            this.txtSpecialTax.Validated += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblExportTax
            // 
            resources.ApplyResources(this.lblExportTax, "lblExportTax");
            this.lblExportTax.Name = "lblExportTax";
            // 
            // lblVAT
            // 
            resources.ApplyResources(this.lblVAT, "lblVAT");
            this.lblVAT.Name = "lblVAT";
            // 
            // lblSpecialTax
            // 
            resources.ApplyResources(this.lblSpecialTax, "lblSpecialTax");
            this.lblSpecialTax.Name = "lblSpecialTax";
            // 
            // lblImportTax
            // 
            resources.ApplyResources(this.lblImportTax, "lblImportTax");
            this.lblImportTax.Name = "lblImportTax";
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtPartNameVN
            // 
            this.txtPartNameVN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtPartNameVN, "txtPartNameVN");
            this.txtPartNameVN.Name = "txtPartNameVN";
            this.txtPartNameVN.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtPartNameVN.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblPartNameVN
            // 
            this.lblPartNameVN.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblPartNameVN, "lblPartNameVN");
            this.lblPartNameVN.Name = "lblPartNameVN";
            // 
            // dlgSelectPicture
            // 
            this.dlgSelectPicture.Multiselect = true;
            this.dlgSelectPicture.RestoreDirectory = true;
            this.dlgSelectPicture.SupportMultiDottedExtensions = true;
            resources.ApplyResources(this.dlgSelectPicture, "dlgSelectPicture");
            // 
            // txtStockTakingCode
            // 
            this.txtStockTakingCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtStockTakingCode, "txtStockTakingCode");
            this.txtStockTakingCode.Name = "txtStockTakingCode";
            this.txtStockTakingCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStockTakingCode_KeyDown);
            this.txtStockTakingCode.Leave += new System.EventHandler(this.txtStockTakingCode_Leave);
            // 
            // lblStockTakingCode
            // 
            this.lblStockTakingCode.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lblStockTakingCode, "lblStockTakingCode");
            this.lblStockTakingCode.Name = "lblStockTakingCode";
            // 
            // btnTakingCode
            // 
            resources.ApplyResources(this.btnTakingCode, "btnTakingCode");
            this.btnTakingCode.Name = "btnTakingCode";
            this.btnTakingCode.Click += new System.EventHandler(this.btnTakingCode_Click);
            // 
            // lblItemGroup
            // 
            resources.ApplyResources(this.lblItemGroup, "lblItemGroup");
            this.lblItemGroup.Name = "lblItemGroup";
            // 
            // cboItemGroup
            // 
            this.cboItemGroup.AddItemSeparator = ';';
            resources.ApplyResources(this.cboItemGroup, "cboItemGroup");
            this.cboItemGroup.CaptionHeight = 17;
            this.cboItemGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboItemGroup.ColumnCaptionHeight = 17;
            this.cboItemGroup.ColumnFooterHeight = 17;
            this.cboItemGroup.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboItemGroup.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboItemGroup.DropDownWidth = 200;
            this.cboItemGroup.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboItemGroup.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboItemGroup.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboItemGroup.Images.Add(((System.Drawing.Image)(resources.GetObject("cboItemGroup.Images"))));
            this.cboItemGroup.ItemHeight = 15;
            this.cboItemGroup.MatchEntryTimeout = ((long)(2000));
            this.cboItemGroup.MaxDropDownItems = ((short)(5));
            this.cboItemGroup.MaxLength = 32767;
            this.cboItemGroup.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboItemGroup.Name = "cboItemGroup";
            this.cboItemGroup.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboItemGroup.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboItemGroup.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboItemGroup.PropBag = resources.GetString("cboItemGroup.PropBag");
            // 
            // cboClassified
            // 
            this.cboClassified.AddItemSeparator = ';';
            resources.ApplyResources(this.cboClassified, "cboClassified");
            this.cboClassified.CaptionHeight = 17;
            this.cboClassified.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboClassified.ColumnCaptionHeight = 17;
            this.cboClassified.ColumnFooterHeight = 17;
            this.cboClassified.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboClassified.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboClassified.DropDownWidth = 200;
            this.cboClassified.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboClassified.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboClassified.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboClassified.Images.Add(((System.Drawing.Image)(resources.GetObject("cboClassified.Images"))));
            this.cboClassified.ItemHeight = 15;
            this.cboClassified.MatchEntryTimeout = ((long)(2000));
            this.cboClassified.MaxDropDownItems = ((short)(5));
            this.cboClassified.MaxLength = 32767;
            this.cboClassified.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboClassified.Name = "cboClassified";
            this.cboClassified.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboClassified.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboClassified.VisualStyle = C1.Win.C1List.VisualStyle.Office2010Blue;
            this.cboClassified.PropBag = resources.GetString("cboClassified.PropBag");
            // 
            // lblClassified
            // 
            resources.ApplyResources(this.lblClassified, "lblClassified");
            this.lblClassified.Name = "lblClassified";
            // 
            // ProductItemInfo
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.btnTakingCode);
            this.Controls.Add(this.txtStockTakingCode);
            this.Controls.Add(this.txtPartNameVN);
            this.Controls.Add(this.tabProductInfo);
            this.Controls.Add(this.txtRevision);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblStockTakingCode);
            this.Controls.Add(this.lblPartNameVN);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnSearchProductDescription);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblRevision);
            this.Controls.Add(this.btnSearchProductCode);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.btnCost);
            this.Controls.Add(this.btnBOM);
            this.Controls.Add(this.btnRouting);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCopy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ProductItemInfo";
            this.Activated += new System.EventHandler(this.ProductItemInfo_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ProductItemInfo_Closing);
            this.Load += new System.EventHandler(this.ProductItemInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPartNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFormatCodeID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBinID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLocationID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMasterLocationID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSellingUMID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBuyingUMID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStockUMID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReasonID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboHazardID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFreightClassID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmSetupDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSourceID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategoryID)).EndInit();
            this.grpWeightAndSize.ResumeLayout(false);
            this.grpWeightAndSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboWidthUMID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboHeightUMID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLengthUMID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboWeightUMID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShelfLife)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAccountCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboQAStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDeliveryPolicyID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboShipToleranceID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrderPolicyID)).EndInit();
            this.grpLeadTime.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLTFixedTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTSafetyStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTOrderPrepare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTDockToStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTSalesATP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTRequisition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTShippingPrepare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLTVariableTime)).EndInit();
            this.grpReplenishment.ResumeLayout(false);
            this.grpReplenishment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRoundUpToMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRoundUpToMultiple)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinProduce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxProduce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPurchasingPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSafetyStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaximumStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtScrapPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucherTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssueSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinimumStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConversionTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderQuantityMultiple)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVendorLocationID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBuyerID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrderRuleID)).EndInit();
            this.grpPlantype.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabProductInfo)).EndInit();
            this.tabProductInfo.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCostMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantitySet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProductType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLicenseFee)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImportTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExportTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecialTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboClassified)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.OpenFileDialog dlgSelectPicture;
        private C1NumericEdit numRoundUpToMultiple;
        private System.Windows.Forms.Label lblRoundUpToMultiple;
        private C1.Win.C1Input.C1NumericEdit numRoundUpToMin;
        private System.Windows.Forms.Label lblRoundUpToMin;
        private System.Windows.Forms.Label lblACAdjustment;
        private System.Windows.Forms.TextBox txtACAdjustment;
        private System.Windows.Forms.Button btnACAdjustment;
        private System.Windows.Forms.TextBox txtRegisteredCode;
        private System.Windows.Forms.Label lblRegisteredCode;
        private System.Windows.Forms.TextBox txtSetUpPair;
        private System.Windows.Forms.Label lblSetUpPair;
        private System.Windows.Forms.CheckBox chkAVEG;
        private System.Windows.Forms.TextBox txtStockTakingCode;
        private System.Windows.Forms.Label lblStockTakingCode;
        private System.Windows.Forms.Button btnTakingCode;
        private System.Windows.Forms.CheckBox chkMassOrder;
        private System.Windows.Forms.CheckBox chkAllowNegativeQty;
        private System.Windows.Forms.Button btnViewImage;
        private System.Windows.Forms.Label lblClassified;
        private C1.Win.C1List.C1Combo cboClassified;
        private C1.Win.C1List.C1Combo cboItemGroup;
        private System.Windows.Forms.Label lblItemGroup;

        private Button btnAdd;
        private Button btnDelete;
        private Button btnSave;
        private Button btnEdit;
        private Button btnClose;
        private Button btnHelp;
        private Button btnCost;
        private Button btnBOM;
        private Button btnRouting;
        private C1Combo cboSourceID;
        private C1Combo cboCategoryID;
        private CheckBox chkMakeItem;
        private CheckBox chkLotControl;
        private TextBox txtRevision;
        private C1Combo cboCCN;
        private TextBox txtDescription;
        private TextBox txtCode;
        private C1Combo cboFormatCodeID;
        private C1Combo cboSellingUMID;
        private C1Combo cboBuyingUMID;
        private C1Combo cboStockUMID;
        private C1Combo cboBinID;
        private C1Combo cboLocationID;
        private C1Combo cboMasterLocationID;
        private C1Combo cboReasonID;
        private C1Combo cboHazardID;
        private C1Combo cboFreightClassID;
        private C1DateEdit dtmSetupDate;
        private TextBox txtOtherInfo2;
        private TextBox txtOtherInfo1;
        private C1Combo cboWidthUMID;
        private C1Combo cboHeightUMID;
        private C1Combo cboLengthUMID;
        private C1Combo cboWeightUMID;
        private CheckBox chkStock;
        private C1Combo cboDeliveryPolicyID;
        private C1Combo cboShipToleranceID;
        private C1Combo cboOrderPolicyID;
        private CheckBox chkAutoConversion;
        private C1Combo cboVendorLocationID;
        private C1Combo cboBuyerID;
        private TextBox txtPrimaryVendor;
        private Button btnSearchVendor;
        private C1Combo cboOrderRuleID;
        private CheckBox chkRequisition;
        private GroupBox grpWeightAndSize;
        private GroupBox grpLeadTime;
        private GroupBox grpReplenishment;
        private GroupBox grpPlantype;
        private Button btnCopy;
        private C1TextBox txtPartNumber;
        private C1Combo cboAccountCode;
        private RadioButton radPlanTypeMPS;
        private RadioButton radPlanTypeMRP;
        private C1Combo cboQAStatus;
        private C1NumericEdit txtVAT;
        private C1NumericEdit txtImportTax;
        private C1NumericEdit txtExportTax;
        private C1NumericEdit txtSpecialTax;
        private C1DockingTab tabProductInfo;
        private C1DockingTabPage tabPage1;
        private C1DockingTabPage tabPage2;
        private C1DockingTabPage tabPage3;
        private Label lblPartNumber;
        private Label lblFormatCodeID;
        private Label lblLotSize;
        private Label lblBinID;
        private Label lblLocationID;
        private Label lblMasterLocationID;
        private Label lblQAStatus;
        private Label lblShelfLife;
        private Label lblAccountCode;
        private Label lblSellingUMID;
        private Label lblSourceID;
        private Label lblStockUMID;
        private Label lblCategoryID;
        private Label lblSetupDate;
        private Label lblReasonID;
        private Label lblOtherInfo2;
        private Label lblOtherInfo1;
        private Label lblBuyingUMID;
        private Label lblWeight;
        private Label lblHeightUMID;
        private Label lblHeight;
        private Label lblWidthUMID;
        private Label lblWidth;
        private Label lblLengthUMID;
        private Label lblLength;
        private Label lblWeightUMID;
        private Label lblHazardID;
        private Label lblFreightClassID;
        private Label lblCCN;
        private Label lblDescription;
        private Label lblRevision;
        private Label lblCode;
        private Label lblLTSalesATP;
        private Label lblLTRequisition;
        private Label lblLTShippingPrepare;
        private Label lblLTOrderPrepare;
        private Label lblLTSafetyStock;
        private Label lblLTDockToStock;
        private Label lblLTVariableTime;
        private Label lblLTFixedTime;
        private Label lblOrderPoint;
        private Label lblVoucherTolerance;
        private Label lblVendorLocationID;
        private Label lblOrderQuantityMultiple;
        private Label lblOrderQuantity;
        private Label lblOrderRuleID;
        private Label lblPrimaryVendor;
        private Label lblBuyerID;
        private Label lblIssueSize;
        private Label lblConversionTolerance;
        private Label lblMinimumStock;
        private Label lblMaximumStock;
        private Label lblScrapPercent;
        private Label lblSafetyStock;
        private Label lblShipToleranceID;
        private Label lblOrderPolicyID;
        private Label lblDeliveryPolicyID;
        private Label lblExportTax;
        private Label lblVAT;
        private Label lblSpecialTax;
        private Label lblImportTax;
        private Label lblCostMethod;
        private Button btnPrint; // Allow user to search for a specific product
        private C1NumericEdit txtSafetyStock;
        private Label label1;
        private Label lblPartNameVN;
        private TextBox txtInventor;
        private Button btnInventor;
        private Label lblInventor;
        private TextBox txtPartNameVN;
        private C1NumericEdit numLicenseFee;
        private Label lblLicenseFee;
        private Label lblProductType;
        private C1Combo cboProductType;
        private Label lblStockUM4;
        private Label lblStockUM3;
        private Label lblStockUM2;
        private Label lblStockUM1;
        private Label lblBuyingUM3;
        private Label lblBuyingUM2;
        private Label lblBuyingUM1;
        private Label lblTaxCode;
        private TextBox txtTaxCode;
        private Label lblPercent;
        private Label lblPercentConv;
        private Label lblPercentVouc;
        private TextBox txtCurrency;
        private Button btnSearchCurrency;
        private Label lblCurrency;
        private Label lblQuantitySet;
        private C1NumericEdit numQuantitySet;
        private Label lblPurchasingPrice;
        private TextBox txtVendorName;
        private Label lblVendorName;
        private C1NumericEdit numPurchasingPrice;
        private Label lblMinProduce;
        private C1NumericEdit numMinProduce;
        private C1NumericEdit numMaxProduce;
        private Label lblMaxProduce;
        private C1Combo cboCostMethod;
        private Button btnClearPicture;
        private Button btnChangePicture;
        private PictureBox picCategory;
        private Label lblPicture;
        private Button btnSearchProductCode;
        private Button btnSearchProductDescription;
        private C1NumericEdit txtLength;
        private C1NumericEdit txtHeight;
        private C1NumericEdit txtWidth;
        private C1NumericEdit txtWeight;
        private C1NumericEdit txtShelfLife;
        private C1NumericEdit txtMaximumStock;
        private C1NumericEdit txtScrapPercent;
        private C1NumericEdit txtIssueSize;
        private C1NumericEdit txtMinimumStock;
        private C1NumericEdit txtConversionTolerance;
        private C1NumericEdit txtLTFixedTime;
        private C1NumericEdit txtLTSafetyStock;
        private C1NumericEdit txtLTOrderPrepare;
        private C1NumericEdit txtLTVariableTime;
        private C1NumericEdit txtLTDockToStock;
        private C1NumericEdit txtLTSalesATP;
        private C1NumericEdit txtLTRequisition;
        private C1NumericEdit txtLTShippingPrepare;
        private C1NumericEdit txtVoucherTolerance;
        private C1NumericEdit txtOrderQuantity;
        private C1NumericEdit txtOrderQuantityMultiple;
        private C1NumericEdit txtOrderPoint;
        private C1NumericEdit txtLotSize;
        private TextBox txtPrimaryVendorID;
    }
}
